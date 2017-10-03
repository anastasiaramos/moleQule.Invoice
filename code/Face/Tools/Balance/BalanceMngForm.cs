using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule.Face.Invoice;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Common.Reports.BankAccount;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Balance;
using moleQule.Library.Invoice.Reports.Cobro;
using moleQule.Library.Invoice.Reports.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Library.Store.Reports.Invoice;
using moleQule.Library.Store.Reports.Expense;
using moleQule.Library.Store.Reports.Payment;

namespace moleQule.Face.Invoice
{
	public partial class BalanceMngForm : moleQule.Face.Common.TreeBaseMngForm
	{
		#region Constants

		protected const string COBRO_IMAGE_KEY = "Cobro";
		protected const string EXTRACTO_IMAGE_KEY = "Extracto";
		protected const string PAGO_IMAGE_KEY = "Pago";
		protected const string TARJETA_IMAGE_KEY = "Tarjeta";
		protected const string WARNING_IMAGE_KEY = "Warning";

		//ARBOL
		const int RESULTADO = 0;
		const int ACTIVO = 0;
		const int PENDIENTE_COBRO = 0;
		const int TESORERIA = 1;
		const int EXISTENCIAS_A = 2;
		const int AYUDAS = 3;
		const int PASIVO = 1;
		const int FACTURAS_PENDIENTES_PAGO = 0;
		const int GASTOS_PENDIENTES = 1;
        const int LOANS = 2;
		const int PAGOS_ESTIMADOS_A = 3;
		const int FACTURAS_ESTIMADAS = 0;
		const int GASTOS_ESTIMADOS = 1;

		const int RIESGO = 1;

		const int MOROSOS = 2;

		//POSICIONES EN EL VECTOR
		const int FACTURAS_EXPLOTACION = 0;
		const int FACTURAS_ACREEDORES = 1;
		const int GASTOS_NOMINAS = 2;
		const int GASTOS_IRPF = 3;
		const int GASTOS_SEGSOCIAL = 4;
		const int OTROS_GASTOS = 5;
		const int PAGOS_ESTIMADOS_PROVEEDOR = 6;
		const int PAGOS_ESTIMADOS_TRANS_ORIGEN = 7;
		const int PAGOS_ESTIMADOS_TRANS_DESTINO = 8;
		const int PAGOS_ESTIMADOS_NAVIERA = 9;
		const int PAGOS_ESTIMADOS_DESPACHANTE = 10;
		const int EFECTOS_PENDIENTES_VTO = 11;
        const int PAGO_PRESTAMOS_PTES_VTO = 12;
        const int COMERCIO_EXTERIOR = 13;

		const int FACTURAS_EMITIDAS = 14;
		const int EFECTOS_PENDIENTES = 15;

		const int EXISTENCIAS = 16;

		const int AYUDAS_REA_PENDIENTES = 17;
		const int AYUDAS_POSEI_PENDIENTES = 18;
		const int AYUDAS_FOMENTO_PENDIENTES = 19;

		const int BANCOS = 20;
		const int CAJA = 21;

		const int AYUDAS_REA_COBRADAS = 22;
		const int EFECTOS_NEGOCIADOS = 23;
		const int FACTURAS_EMITIDAS_MOROSOS = 24;

		#endregion

		#region Attributes & Properties

		public new const string ID = "BalanceMngForm";
		public new static Type Type { get { return typeof(BalanceMngForm); } }

		protected override int BarSteps { get { return base.BarSteps + 15; } }

		new NotifyEntityList List { get; set; }

		DateTime From { get { return DateTime.MinValue; } }
		DateTime Till { get { return _fecha; } }

		public InputInvoiceList FacturaRecibidaExplotacionList { get; set; }
		public InputInvoiceList FacturaRecibidaAcreedorList { get; set; }
		public ExpenseList ExpenseList { get; set; }
		public ExpenseList IRPFList { get; set; }
		public ExpenseList SegSocialList { get; set; }
		public ChargeSummaryList CResumenList { get; set; }
		public PaymentSummaryList PResumenList { get; set; }
		public PaymentSummaryList PagoPendienteVtoList { get; set; }
		public PaymentSummaryList PagoEstimadoProList { get; set; }
		public PaymentSummaryList PagoEstimadoTorList { get; set; }
		public PaymentSummaryList PagoEstimadoTdeList { get; set; }
		public PaymentSummaryList PagoEstimadoNavList { get; set; }
		public PaymentSummaryList PagoEstimadoDesList { get; set; }

		ChartMng _chart_mng;

		#endregion

		#region Factory Methods

		public BalanceMngForm()
			: this(false) { }

		public BalanceMngForm(string schema)
			: this(false, null) { }

		public BalanceMngForm(bool isModal)
			: this(isModal, null) { }

		public BalanceMngForm(Form parent)
			: this(false, parent) { }

		public BalanceMngForm(bool isModal, Form parent)
			: base(isModal, parent, null)
		{
			InitializeComponent();

			SetView(molView.Tree);

			_fecha = DateAndTime.LastDay(DateTime.Today.Month, DateTime.Today.Year);
		}

		#endregion

		#region Layout

		public override void FormatControls()
		{
			this.Text = "Balance";

			base.FormatControls();

			MaximizeForm(new Size(400, 0));
			Left = 0;

			SetActionStyle(molAction.CustomAction1, Resources.Labels.PRINT_DETAIL_TI, Properties.Resources.item_print_detail);
			SetActionStyle(molAction.CustomAction2, Resources.Labels.GRAPH_TI, Properties.Resources.estadistica);
		}
		
		protected override void SetView(molView view)
		{
			base.SetView(view);
			
			ShowAction(molAction.CustomAction1);
			ShowAction(molAction.CustomAction2);
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			BatchList ExistenciasList;
			FinancialCashList EfectosNegociados;
			ChargeList CobroPendienteList;
			ExpedienteREAList AyudaPendienteList;
			LineaFomentoList LineaFomentoList;
			BankAccountList CuentaList;
			CashList CashList;
            LoanList Loans;
            PaymentSummaryList Payrolls;

			PgMng.Message = Resources.Messages.RETRIEVING_BALANCE;

			if (List == null) List = new NotifyEntityList();
			else List.Clear();

			Date_TI.Text = Till.ToShortDateString();

			List = new NotifyEntityList();            

			//PASIVO ----------------------------------------------------------------------------------

			PResumenList = PaymentSummaryList.GetExplotacionPendientesList(From, Till);
			List.Add(new NotifyEntity(ETipoNotificacion.Gastos
										, ETipoEntidad.PResumen
										, PResumenList.Count
										, PResumenList.TotalPendiente()
										, Resources.Labels.FACTURAS_EXPLOTACION_PENDIENTES
										, PResumenList));
			PgMng.Grow();

			PResumenList = PaymentSummaryList.GetExpedientesPendientesList(From, Till);
			List.Add(new NotifyEntity(ETipoNotificacion.Gastos
										, ETipoEntidad.PResumen
										, PResumenList.Count
										, PResumenList.TotalPendiente()
										, Resources.Labels.FACTURAS_EXPEDIENTES_PENDIENTES
										, PResumenList));
			PgMng.Grow();

            Payrolls = PaymentSummaryList.GetUnpaidPayrollList(From, Till);
			List.Add(new NotifyEntity(ETipoNotificacion.Gastos
                                        , ETipoEntidad.Nomina
                                        , Payrolls.Count
                                        , Payrolls.TotalPendiente()
										, Resources.Labels.NOMINAS
                                        , Payrolls));
			PgMng.Grow();

			IRPFList = ExpenseList.GetPendientesLiquidacionList(ECategoriaGasto.IRPFNominas, From, Till, false);
			List.Add(new NotifyEntity(ETipoNotificacion.Gastos
										, ETipoEntidad.Gasto
										, IRPFList.Count
										, IRPFList.TotalPendienteLiquidacion()
										, Resources.Labels.GASTOS_IRPF
										, IRPFList));
			PgMng.Grow();

			SegSocialList = ExpenseList.GetPendientesLiquidacionList(ECategoriaGasto.SeguroSocialNominas, From, Till, false);
			List.Add(new NotifyEntity(ETipoNotificacion.Gastos
										, ETipoEntidad.Gasto
										, SegSocialList.Count
										, SegSocialList.TotalPendienteLiquidacion()
										, Resources.Labels.GASTOS_SEGURIDAD_SOCIAL
										, SegSocialList));
			PgMng.Grow();

			ExpenseList = ExpenseList.GetPendientesLiquidacionList(ECategoriaGasto.OtrosBalance, From, Till, false);
			List.Add(new NotifyEntity(ETipoNotificacion.Gastos
										, ETipoEntidad.Gasto
										, ExpenseList.Count
										, ExpenseList.TotalPendienteLiquidacion()
										, Resources.Labels.OTROS_GASTOS
										, ExpenseList));
			PgMng.Grow();

			PagoEstimadoProList = PaymentSummaryList.GetEstimadoList(ETipoAcreedor.Proveedor);
			List.Add(new NotifyEntity(ETipoNotificacion.GastoEstimado
										, ETipoEntidad.PResumen
										, PagoEstimadoProList.Count
										, PagoEstimadoProList.TotalEstimado()
										, Resources.Labels.ESTIMADO_PROVEEDOR
										, PagoEstimadoProList));
			PgMng.Grow();

			PagoEstimadoTorList = PaymentSummaryList.GetEstimadoList(ETipoAcreedor.TransportistaOrigen);
			List.Add(new NotifyEntity(ETipoNotificacion.GastoEstimado
										, ETipoEntidad.PResumen
										, PagoEstimadoTorList.Count
										, PagoEstimadoTorList.TotalEstimado()
										, Resources.Labels.ESTIMADO_TRANS_ORIGEN
										, PagoEstimadoTorList));
			PgMng.Grow();

			PagoEstimadoTdeList = PaymentSummaryList.GetEstimadoList(ETipoAcreedor.TransportistaDestino);
			List.Add(new NotifyEntity(ETipoNotificacion.GastoEstimado
										, ETipoEntidad.PResumen
										, PagoEstimadoTdeList.Count
										, PagoEstimadoTdeList.TotalEstimado()
										, Resources.Labels.ESTIMADO_TRANS_DESTINO
										, PagoEstimadoTdeList));
			PgMng.Grow();

			PagoEstimadoNavList = PaymentSummaryList.GetEstimadoList(ETipoAcreedor.Naviera);
			List.Add(new NotifyEntity(ETipoNotificacion.GastoEstimado
										, ETipoEntidad.PResumen
										, PagoEstimadoNavList.Count
										, PagoEstimadoNavList.TotalEstimado()
										, Resources.Labels.ESTIMADO_NAVIERA
										, PagoEstimadoNavList));
			PgMng.Grow();

			PagoEstimadoDesList = PaymentSummaryList.GetEstimadoList(ETipoAcreedor.Despachante);
			List.Add(new NotifyEntity(ETipoNotificacion.GastoEstimado
										, ETipoEntidad.PResumen
										, PagoEstimadoDesList.Count
										, PagoEstimadoDesList.TotalEstimado()
										, Resources.Labels.ESTIMADO_DESPACHANTE
										, PagoEstimadoDesList));
			PgMng.Grow();

			PagoPendienteVtoList = PaymentSummaryList.GetPendientesVtoList(From, Till);
			List.Add(new NotifyEntity(ETipoNotificacion.PagoBancoPendiente
										, ETipoEntidad.PResumen
										, PagoPendienteVtoList.Count
										, PagoPendienteVtoList.TotalPendienteVto()
										, Resources.Messages.EFECTOS_PENDIENTES_VTO
										, PagoPendienteVtoList));

            PgMng.Grow();

            Loans = LoanList.GetUnpaidList(ELoanType.Bank, From, Till, false);
            List.Add(new NotifyEntity(ETipoNotificacion.Loans
                                        , ETipoEntidad.Prestamo
                                        , Loans.Count
                                        , Loans.TotalPartialUnpaid()
                                        , Resources.Messages.PAGO_PRESTAMOS_PTES_VTO
                                        , Loans));
			PgMng.Grow();

            Loans = LoanList.GetUnpaidList(ELoanType.Merchant, From, Till, false);
            List.Add(new NotifyEntity(ETipoNotificacion.Loans
                                        , ETipoEntidad.Prestamo
                                        , Loans.Count
                                        , Loans.TotalPartialUnpaid()
                                        , Resources.Labels.PRESTAMOS_COMERCIO_EXTERIOR
                                        , Loans));
            PgMng.Grow();

            //ACTIVO ----------------------------------------------------------------------------------

            CResumenList = Library.Invoice.ChargeSummaryList.GetPendientesList();
            List.Add(new NotifyEntity(ETipoNotificacion.IngresoPendiente
                                        , ETipoEntidad.CResumen
                                        , CResumenList.Count
                                        , CResumenList.TotalPendiente()
                                        , Resources.Labels.FACTURAS_EMITIDAS_PENDIENTES
                                        , CResumenList));
            PgMng.Grow();

            CobroPendienteList = ChargeList.GetListPendientes(ETipoCobro.Todos, From, Till, false);
            List.Add(new NotifyEntity(ETipoNotificacion.IngresoPendiente
                                        , ETipoEntidad.Cobro
                                        , CobroPendienteList.Count
                                        , CobroPendienteList.Total()
                                        , Resources.Messages.EFECTOS_PENDIENTES
                                        , CobroPendienteList));
            PgMng.Grow();

            ExistenciasList = BatchList.GetListBySerieAndStockAgrupado(0, false, true);
            List.Add(new NotifyEntity(ETipoNotificacion.Ingresos
                                        , ETipoEntidad.Partida
                                        , ExistenciasList.Count
                                        , ExistenciasList.TotalValorado()
                                        , Resources.Messages.EXISTENCIAS
                                        , ExistenciasList));
            PgMng.Grow();

            Library.Store.QueryConditions conditions;
            conditions = new Library.Store.QueryConditions { Estado = EEstado.Pendiente, TipoExpediente = ETipoExpediente.Alimentacion };
            AyudaPendienteList = ExpedienteREAList.GetListByREA(conditions, false);
            List.Add(new NotifyEntity(ETipoNotificacion.IngresoPendiente
                                        , ETipoEntidad.Expediente
                                        , AyudaPendienteList.Count
                                        , AyudaPendienteList.TotalPendiente()
                                        , Resources.Labels.AYUDAS_REA_PENDIENTES
                                        , AyudaPendienteList));
            PgMng.Grow();

            conditions = new Library.Store.QueryConditions { Estado = EEstado.Pendiente, TipoExpediente = ETipoExpediente.Ganado };
            AyudaPendienteList = ExpedienteREAList.GetListByREA(conditions, false);
            List.Add(new NotifyEntity(ETipoNotificacion.IngresoPendiente
                                        , ETipoEntidad.Expediente
                                        , AyudaPendienteList.Count
                                        , AyudaPendienteList.TotalPendiente()
                                        , Resources.Labels.AYUDAS_POSEI_PENDIENTES
                                        , AyudaPendienteList));
            PgMng.Grow();

            LineaFomentoList = LineaFomentoList.GetPendientesList(Till, false);
            List.Add(new NotifyEntity(ETipoNotificacion.Ingresos
                                        , ETipoEntidad.LineaFomento
                                        , LineaFomentoList.Count
                                        , LineaFomentoList.TotalPendiente()
                                        , Resources.Labels.AYUDAS_FOMENTO_PENDIENTES
                                        , LineaFomentoList
                                        , true));
            PgMng.Grow();

            CuentaList = BankAccountList.GetList(EEstado.Active, Till, false);
            List.Add(new NotifyEntity(ETipoNotificacion.Ingresos
                                        , ETipoEntidad.CuentaBancaria
                                        , CuentaList.Count
                                        , CuentaList.TotalSaldo()
                                        , Resources.Messages.BANCOS
                                        , CuentaList));
            PgMng.Grow();

            CashList = CashList.GetList(Till, false);
            List.Add(new NotifyEntity(ETipoNotificacion.Ingresos
                                        , ETipoEntidad.Caja
                                        , CashList.Count
                                        , CashList.TotalSaldo()
                                        , Resources.Messages.CAJA
                                        , CashList));
            PgMng.Grow();

			//RIESGO ----------------------------------------------------------------------------------

            EfectosNegociados = FinancialCashList.GetNegociadosList(DateTime.Today, Till, false);
			List.Add(new NotifyEntity(ETipoNotificacion.Riesgo
										, ETipoEntidad.FinancialCash
										, EfectosNegociados.Count
										, EfectosNegociados.TotalNegociado()
										, Resources.Messages.EFECTOS_NEGOCIADOS
										, EfectosNegociados
										, true));
			PgMng.Grow();

			//MOROSOS ---------------------------------------------------------------------------------

			DateTime from;
			DateTime till;

			for (int year = 2011; year <= Till.Year; year++)
			{
				from = DateAndTime.FirstDay(year);
				till = DateAndTime.LastDay(year);

				CResumenList = ChargeSummaryList.GetDudosoCobroList(year);

				if (CResumenList.Count == 0) continue;

				List.Add(new NotifyEntity(ETipoNotificacion.Morosos
											, ETipoEntidad.CResumen
											, CResumenList.Count
											, CResumenList.TotalDudosoCobro()
											, Resources.Labels.MOROSOS + " - " + year.ToString("0000")
											, CResumenList));

				List[List.Count - 1].Tag = year;
			}
			PgMng.Grow();

			base.RefreshMainData();
		}

		protected override void BuildTree()
		{
			if (List == null) return;

			TreeNode node = null;

			node = new TreeNode();
			node.Text = Resources.Labels.RESULTADO_BALANCE;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text);
			node.ImageKey = OPEN_IMAGE_KEY;
			node.SelectedImageKey = OPEN_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.Nodes.Add(node);

			node = new TreeNode();
			node.Text = Resources.Labels.ACTIVO;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
			node.ImageKey = COBRO_IMAGE_KEY;
			node.SelectedImageKey = COBRO_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.TopNode.Nodes.Add(node);

			node = new TreeNode();
			node.Text = Resources.Labels.PENDIENTE_COBRO;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
			node.ImageKey= COBRO_IMAGE_KEY;
			node.SelectedImageKey= COBRO_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.TopNode.Nodes[ACTIVO].Nodes.Add(node);

			node = new TreeNode();
			node.Text = Resources.Labels.TESORERIA;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
			node.ImageKey = COBRO_IMAGE_KEY;
			node.SelectedImageKey = COBRO_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.TopNode.Nodes[ACTIVO].Nodes.Add(node);

			node = new TreeNode();
			node.Text = Resources.Labels.EXISTENCIAS;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
			node.ImageKey = COBRO_IMAGE_KEY;
			node.SelectedImageKey = COBRO_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.TopNode.Nodes[ACTIVO].Nodes.Add(node);

			node = new TreeNode();
			node.Text = Resources.Labels.AYUDAS;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
			node.ImageKey= COBRO_IMAGE_KEY;
			node.SelectedImageKey= COBRO_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.TopNode.Nodes[ACTIVO].Nodes.Add(node);

			node = new TreeNode();
			node.Text = Resources.Labels.PASIVO;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
			node.ImageKey= PAGO_IMAGE_KEY;
			node.SelectedImageKey= PAGO_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.TopNode.Nodes.Add(node);

			node = new TreeNode();
			node.Text = Resources.Labels.FACTURAS_PENDIENTES_PAGO;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
			node.ImageKey= PAGO_IMAGE_KEY;
			node.SelectedImageKey= PAGO_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.TopNode.Nodes[PASIVO].Nodes.Add(node);

			node = new TreeNode();
			node.Text = Resources.Labels.GASTOS_PENDIENTES;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
			node.ImageKey= PAGO_IMAGE_KEY;
			node.SelectedImageKey= PAGO_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.TopNode.Nodes[PASIVO].Nodes.Add(node);

            node = new TreeNode();
            node.Text = Resources.Labels.LOANS;
            node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
            node.ImageKey = PAGO_IMAGE_KEY;
            node.SelectedImageKey = PAGO_IMAGE_KEY;
            node.Checked = true;
            Tree_TV.TopNode.Nodes[PASIVO].Nodes.Add(node);

			node = new TreeNode();
			node.Text = Resources.Labels.FACTURAS_ESTIMADAS;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
			node.ImageKey= PAGO_IMAGE_KEY;
			node.SelectedImageKey= PAGO_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.TopNode.Nodes[PASIVO].Nodes.Add(node);

			/*node = new TreeNode();
			node.Text = Resources.Labels.FACTURAS_ESTIMADAS;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
			node.ImageKey= 2;
			node.SelectedImageKey= 2;
			node.Checked = true;
			Tree_TV.TopNode.Nodes[PASIVO].Nodes[PAGOS_ESTIMADOS_A].Nodes.Add(node);

			node = new TreeNode();
			node.Text = Resources.Labels.GASTOS_ESTIMADOS;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text, true);
			node.ImageKey= 2;
			node.SelectedImageKey= 2;
			node.Checked = true;
			Tree_TV.TopNode.Nodes[PASIVO].Nodes[PAGOS_ESTIMADOS_A].Nodes.Add(node);*/

			node = new TreeNode();
			node.Text = Resources.Labels.RIESGO;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text);
			node.ImageKey= OPEN_IMAGE_KEY;
			node.SelectedImageKey= OPEN_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.Nodes.Add(node);

			node = new TreeNode();
			node.Text = Resources.Labels.MOROSOS;
			node.Tag = new NotifyEntity(ETipoNotificacion.Node, node.Text);
			node.ImageKey= OPEN_IMAGE_KEY;
			node.SelectedImageKey = OPEN_IMAGE_KEY;
			node.Checked = true;
			Tree_TV.Nodes.Add(node);

			foreach (NotifyEntity item in List)
			{
				node = new TreeNode();
				node.Name = item.ETipoEntidad.ToString();
				node.Text = item.FullTitle;
				node.Tag = item;
				node.SelectedImageKey= Tree_TV.SelectedImageKey;
				node.Checked = item.Checked;

				switch (item.ETipoNotificacion)
				{
					case ETipoNotificacion.Ingresos:
						{
							node.ImageKey= COBRO_IMAGE_KEY;

							switch (item.ETipoEntidad)
							{
								case ETipoEntidad.Caja: Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO].Nodes[TESORERIA].Nodes.Add(node); break;
								case ETipoEntidad.CuentaBancaria: Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO].Nodes[TESORERIA].Nodes.Add(node); break;
								case ETipoEntidad.Expediente: Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO].Nodes[AYUDAS].Nodes.Add(node); break;
								case ETipoEntidad.LineaFomento: Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO].Nodes[AYUDAS].Nodes.Add(node); break;
								case ETipoEntidad.Partida: Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO].Nodes[EXISTENCIAS_A].Nodes.Add(node); break;
								default: Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO].Nodes.Add(node); break;
							}
						}
						break;

					case ETipoNotificacion.IngresoPendiente:
						{
							node.ImageKey= COBRO_IMAGE_KEY;

							switch (item.ETipoEntidad)
							{
								case ETipoEntidad.FacturaEmitida: Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO].Nodes[PENDIENTE_COBRO].Nodes.Add(node); break;
								case ETipoEntidad.CResumen: Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO].Nodes[PENDIENTE_COBRO].Nodes.Add(node); break;
								case ETipoEntidad.Cobro: Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO].Nodes[PENDIENTE_COBRO].Nodes.Add(node); break;
								case ETipoEntidad.Expediente: Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO].Nodes[AYUDAS].Nodes.Add(node); break;
								default: Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO].Nodes.Add(node); break;
							}
						}
						break;

					case ETipoNotificacion.Gastos:
						{
							node.ImageKey= PAGO_IMAGE_KEY;

							switch (item.ETipoEntidad)
							{
								case ETipoEntidad.FacturaRecibida: Tree_TV.Nodes[RESULTADO].Nodes[PASIVO].Nodes[FACTURAS_PENDIENTES_PAGO].Nodes.Add(node); break;
								case ETipoEntidad.PResumen: Tree_TV.Nodes[RESULTADO].Nodes[PASIVO].Nodes[FACTURAS_PENDIENTES_PAGO].Nodes.Add(node); break;
								case ETipoEntidad.Gasto: Tree_TV.Nodes[RESULTADO].Nodes[PASIVO].Nodes[GASTOS_PENDIENTES].Nodes.Add(node); break;
                                case ETipoEntidad.Nomina: Tree_TV.Nodes[RESULTADO].Nodes[PASIVO].Nodes[GASTOS_PENDIENTES].Nodes.Add(node); break;
                                default: Tree_TV.Nodes[RESULTADO].Nodes[PASIVO].Nodes.Add(node); break;
							}
						}
						break;

					case ETipoNotificacion.GastoEstimado:

						node.ImageKey= PAGO_IMAGE_KEY;

						switch (item.ETipoEntidad)
						{
							case ETipoEntidad.PResumen: Tree_TV.Nodes[RESULTADO].Nodes[PASIVO].Nodes[PAGOS_ESTIMADOS_A].Nodes.Add(node); break;
							//Este caso FALTA POR CALCULAR Y ES LOS GASTOS DE NOMINAS, IRPF Y SEG SOCIAL ESTIMADOS
							//case ETipoEntidad.PResumen: Tree_TV.TopNode.Nodes[PASIVO].Nodes[PAGOS_ESTIMADOS_A].Nodes[FACTURAS_ESTIMADAS].Nodes.Add(node); break;
							//case ETipoEntidad.Gasto: Tree_TV.TopNode.Nodes[PASIVO].Nodes[PAGOS_ESTIMADOS_A].Nodes[GASTOS_ESTIMADOS].Nodes.Add(node); break;
							default: Tree_TV.Nodes[RESULTADO].Nodes[PASIVO].Nodes.Add(node); break;
						}

						break;

                    case ETipoNotificacion.PagoBancoPendiente:
                        {
                            node.ImageKey = PAGO_IMAGE_KEY;

                            switch (item.ETipoEntidad)
                            {
                                default: Tree_TV.Nodes[RESULTADO].Nodes[PASIVO].Nodes.Add(node); break;
                            }
                        }
                        break;


                    case ETipoNotificacion.Loans:
                        {
                            node.ImageKey = PAGO_IMAGE_KEY;

                            switch (item.ETipoEntidad)
                            {
                                case ETipoEntidad.Prestamo: Tree_TV.Nodes[RESULTADO].Nodes[PASIVO].Nodes[LOANS].Nodes.Add(node); break;
                                default: Tree_TV.Nodes[RESULTADO].Nodes[PASIVO].Nodes.Add(node); break;
                            }
                        }
                        break;

					case ETipoNotificacion.Riesgo:

						node.ImageKey= WARNING_IMAGE_KEY;

						switch (item.ETipoEntidad)
						{
							case ETipoEntidad.FinancialCash:
								{
									CResumenList = Library.Invoice.ChargeSummaryList.GetList();
									decimal total_facturado = CResumenList.TotalFacturado();

									Decimal porcentaje = total_facturado != 0 ? ((FinancialCashList)item.List).TotalNegociado() / total_facturado : 0;
									node.Text += porcentaje.ToString(" (#0.00%)");
									Tree_TV.Nodes[RIESGO].Nodes.Add(node);
								}
								break;
						}
						break;

					case ETipoNotificacion.Morosos:

						node.ImageKey= OPEN_IMAGE_KEY;

						switch (item.ETipoEntidad)
						{
							case ETipoEntidad.CResumen:
								{
									CResumenList = Library.Invoice.ChargeSummaryList.GetList((int)item.Tag);
                                    decimal total_facturado = CResumenList.TotalFacturado();

									Decimal porcentaje = total_facturado != 0 ? ((ChargeSummaryList)item.List).TotalDudosoCobro() / total_facturado : 0;
									node.Text += porcentaje.ToString(" (#0.00%)");
									Tree_TV.Nodes[MOROSOS].Nodes.Add(node);
								} break;
						}
						break;
				}
			}

			SetTotales();

			Tree_TV.ExpandAll();
		}

		#endregion

		#region Business Methods

		protected void AddPrintingNodes(NotifyEntityList list, TreeNode node)
		{
			if (node.Nodes.Count > 0)
			{
				list.Add(new NotifyEntity(ETipoNotificacion.Node));
				foreach (TreeNode item in node.Nodes)
				{
					AddPrintingNodes(list, item);
					list.Add((NotifyEntity)(item.Tag));
				}
				list.Add(new NotifyEntity(ETipoNotificacion.Node));
			}
		}

		protected NotifyEntityList GetPrintingList()
		{
			NotifyEntityList printing_list = new NotifyEntityList();

			foreach (TreeNode node in Tree_TV.Nodes)
			{
				AddPrintingNodes(printing_list, node);
				printing_list.Add((NotifyEntity)node.Tag);
				printing_list.Add(new NotifyEntity(ETipoNotificacion.Node));
			}

			return printing_list;
		}

		protected DataPoint BuildPoint(NotifyEntity entity, int index)
		{
			DataPoint point = new DataPoint();
			point.XValue = index;
			point.YValues = entity.Checked ? new double[1] { (double)entity.Total } : new double[1] { 0 };
			point.AxisLabel = entity.Title;
			point.Label = entity.Checked ? entity.Total.ToString("C2") : string.Empty;

			return point;
		}

		protected Chart GetChart()
		{
			if (_chart_mng == null)
			{
				_chart_mng = new ChartMng(MainBaseForm.Instance, typeof(Skin05.ChartSkinForm));
				_chart_mng.ChartForm.Detallado_RB.CheckedChanged += new EventHandler(Detallado_RB_CheckedChanged);
				_chart_mng.ChartForm.Agrupado_RB.CheckedChanged += new EventHandler(Agrupado_RB_CheckedChanged);
			}

			return _chart_mng.NewChart();
		}

		private void SetTotales()
		{
			TreeNode balance = Tree_TV.Nodes[RESULTADO];
			TreeNode pasivo = Tree_TV.Nodes[RESULTADO].Nodes[PASIVO];
			TreeNode activo = Tree_TV.Nodes[RESULTADO].Nodes[ACTIVO];

			SetSubtotales(balance);

			((NotifyEntity)balance.Tag).Total = ((NotifyEntity)activo.Tag).Total - ((NotifyEntity)pasivo.Tag).Total;
			balance.Text = ((NotifyEntity)balance.Tag).FullTitle;

			TreeNode riesgo = Tree_TV.Nodes[RIESGO];
			SetSubtotales(riesgo);

			TreeNode morosos = Tree_TV.Nodes[MOROSOS];
			SetSubtotales(morosos);
		}

		private void SetSubtotales(TreeNode node)
		{
			if (((NotifyEntity)node.Tag).ETipoNotificacion != ETipoNotificacion.Node) return;

			if (node.Nodes.Count > 0) ((NotifyEntity)node.Tag).Total = 0;

			foreach (TreeNode item in node.Nodes)
			{
				SetSubtotales(item);
				if (item.Checked) ((NotifyEntity)node.Tag).Total += ((NotifyEntity)item.Tag).Total;
			}

			node.Text = ((NotifyEntity)node.Tag).FullTitle;
		}
		
		private int Redondea(double valor)
		{
			if ((int)valor / 100000000 > 0) return ((int)valor / 100000000) * 100000000;
			if ((int)valor / 10000000 > 0) return ((int)valor / 10000000) * 10000000;
			if ((int)valor / 1000000 > 0) return ((int)valor / 1000000) * 1000000;
			if ((int)valor / 100000 > 0) return ((int)valor / 100000) * 100000;
			if ((int)valor / 10000 > 0) return ((int)valor / 10000) * 10000;
			if ((int)valor / 1000 > 0) return ((int)valor / 1000) * 1000;
			if ((int)valor / 100 > 0) return ((int)valor / 100) * 100;
			if ((int)valor / 10 > 0) return ((int)valor / 10) * 10;

			return (int)valor;
		}

		#endregion

		#region Actions

		protected override void OpenMngFormAction(NotifyEntity item)
		{
			switch (item.ETipoEntidad)
			{
				case ETipoEntidad.Caja:
					{
						/*CajaMngForm form = new CajaMngForm(false, MainForm.Instance, (CashList)item.List);
						FormMng.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();*/
					}
					break;

				case ETipoEntidad.Cobro:
					{
						CobroMngForm form = new CobroMngForm(false, _parent, (ChargeList)item.List, ETipoCobro.Todos);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
						OpenForm = form;
					}
					break;

				case ETipoEntidad.CResumen:
					{
						ClientChargeMngForm form = new ClientChargeMngForm(false, _parent, (ChargeSummaryList)item.List);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
						OpenForm = form;
					}
					break;

				case ETipoEntidad.CuentaBancaria:
					{
						BankAccountMngForm form = new BankAccountMngForm(false, _parent, (BankAccountList)item.List);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
						OpenForm = form;
					}
					break;

				case ETipoEntidad.Expediente:
					{
						ExpedienteREAMngForm form = new ExpedienteREAMngForm(false, _parent, (ExpedienteREAList)item.List);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
						OpenForm = form;
					}
					break;

				case ETipoEntidad.FacturaEmitida:
					{
						InvoiceMngForm form = new InvoiceMngForm(false, _parent, (OutputInvoiceList)item.List, ETipoFacturas.Todas);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
						OpenForm = form;
					}
					break;

				case ETipoEntidad.FacturaRecibida:
					{
						InputInvoiceMngForm form = new InputInvoiceMngForm(false, _parent, ETipoFacturas.Todas, (InputInvoiceList)item.List);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
						OpenForm = form;
					}
					break;

                case ETipoEntidad.FinancialCash:
                    {
                        FinancialCashMngForm form = new FinancialCashMngForm(false, _parent, (FinancialCashList)item.List);
                        FormMngBase.Instance.ShowFormulario(form, this);
                        form.ViewMode = molView.Enbebbed;
                        form.Text = item.Title;
                        form.Left = this.Right + 1;
                        form.Width -= this.Width;
                        form.FitColumns();
                    }
                    break;

				case ETipoEntidad.Gasto:
					{
                        ExpenseMngForm form = new ExpenseMngForm(false, _parent, ECategoriaGasto.Todos, (ExpenseList)item.List);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
					}
					break;

				case ETipoEntidad.LineaFomento:
					{
						LineaFomentoMngForm form = new LineaFomentoMngForm(false, _parent, (LineaFomentoList)item.List);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = "Informe: Balance - " + item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
						OpenForm = form;
					}
					break;

                case ETipoEntidad.Nomina:
                    {
                        EmployeePaymentMngForm form = new EmployeePaymentMngForm(false, _parent, (PaymentSummaryList)item.List);
                        FormMngBase.Instance.ShowFormulario(form, this);
                        form.ViewMode = molView.Enbebbed;
                        form.Text = item.Title;
                        form.Left = this.Right + 1;
                        form.Width -= this.Width;
                        form.FitColumns();
                        OpenForm = form;
                    }
                    break;

				case ETipoEntidad.Pago:
					{
						PaymentMngForm form = new PaymentMngForm(false, _parent, ETipoPago.Todos, (PaymentList)item.List);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
						OpenForm = form;
					}
					break;

				case ETipoEntidad.Partida:
					{
						BatchMngForm form = new BatchMngForm(false, _parent, (BatchList)item.List);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
					}
					break;

				case ETipoEntidad.PResumen:
					{
						ProviderPaymentMngForm form = new ProviderPaymentMngForm(false, _parent, (PaymentSummaryList)item.List);
						FormMngBase.Instance.ShowFormulario(form, this);
						form.ViewMode = molView.Enbebbed;
						form.Text = item.Title;
						form.Left = this.Right + 1;
						form.Width -= this.Width;
						form.FitColumns();
						OpenForm = form;
					}
					break;

                case ETipoEntidad.Prestamo:
                    {
                        LoanList list = (LoanList)item.List;

                        if (list.Count == 0) return;

                        LoanMngForm form;

                        if (list[0].OidPago == 0)
                            form = new BankLoanMngForm(false, _parent, (LoanList)item.List);
                        else
                            form = new MerchantLoanMngForm(false, _parent, (LoanList)item.List);

                        FormMngBase.Instance.ShowFormulario(form, this);
                        form.ViewMode = molView.Enbebbed;
                        form.Text = item.Title;
                        form.Left = this.Right + 1;
                        form.Width -= this.Width;
                        form.FitColumns();
                    }
                    break;
			}
		}

		public override void CustomAction1()
		{
			PrintList();
		}

		public override void CustomAction2()
		{
			ShowChartAction(ETipoChart.Detallado);
		}

		public override void RefreshAction()
		{
			try
			{
				ResetTree();

				PgMng.Reset(BarSteps, 1,string.Empty, this);

				RefreshMainData();
				BuildTree();

				FormMngBase.Instance.CloseAllForms(this);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected override void PrintAction()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

			string filter = "Fecha = " + Date_TI.Text;

			CommonReportMng reportMng = new CommonReportMng(AppContext.ActiveSchema, this.Text, filter);
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

			BalanceRpt report = reportMng.GetBalanceReport(GetPrintingList());
			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintList()
		{
			try
			{
				PgMng.Reset(25, 1, Face.Resources.Messages.LOADING_DATA, this);

				string filter = "Fecha = " + Date_TI.Text;

				CommonReportMng reportMng = new CommonReportMng(AppContext.ActiveSchema, this.Text, filter);
				PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

				BalanceRpt report = reportMng.GetBalanceReport(GetPrintingList());
				PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

				ShowReport(report);

				if (Library.Invoice.ModulePrincipal.GetBalancePrintFacturasExplotacion())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[FACTURAS_EXPLOTACION];

					PaymentReportMng rptMng = new PaymentReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					PagoAcreedorListRpt rpt = rptMng.GetPagoAcreedorListReport((PaymentSummaryList)entity.List);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintFacturasAcreeedores())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[FACTURAS_ACREEDORES];

					PaymentReportMng rptMng = new PaymentReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					PagoAcreedorListRpt rpt = rptMng.GetPagoAcreedorListReport((PaymentSummaryList)entity.List);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintEfectosPendientesVto())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[EFECTOS_PENDIENTES_VTO];

					PaymentReportMng rptMng = new PaymentReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					PagoAcreedorListRpt rpt = rptMng.GetPagoAcreedorListReport((PaymentSummaryList)entity.List);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintGastosNominas())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[GASTOS_NOMINAS];

                    ExpenseReportMng rptMng = new ExpenseReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					ExpenseListRpt rpt = rptMng.GetListReport((ExpenseList)entity.List);

					ShowReport(rpt);
				}

				if (true)
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[GASTOS_IRPF];

                    ExpenseReportMng rptMng = new ExpenseReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					ExpenseListRpt rpt = rptMng.GetListReport((ExpenseList)entity.List);

					ShowReport(rpt);
				}

				if (true)
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[GASTOS_SEGSOCIAL];

                    ExpenseReportMng rptMng = new ExpenseReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					ExpenseListRpt rpt = rptMng.GetListReport((ExpenseList)entity.List);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintOtrosGastos())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[OTROS_GASTOS];

                    ExpenseReportMng rptMng = new ExpenseReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					ExpenseListRpt rpt = rptMng.GetListReport((ExpenseList)entity.List);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintPagosEstimados())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[PAGOS_ESTIMADOS_PROVEEDOR];

					PaymentReportMng rptMng = new PaymentReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					PagoAcreedorListRpt rpt = rptMng.GetPagoAcreedorListReport((PaymentSummaryList)entity.List);

					ShowReport(rpt);
				}

				if (true)
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[PAGOS_ESTIMADOS_TRANS_ORIGEN];

					PaymentReportMng rptMng = new PaymentReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					PagoAcreedorListRpt rpt = rptMng.GetPagoAcreedorListReport((PaymentSummaryList)entity.List);

					ShowReport(rpt);
				}

				if (true)
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[PAGOS_ESTIMADOS_TRANS_DESTINO];

					PaymentReportMng rptMng = new PaymentReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					PagoAcreedorListRpt rpt = rptMng.GetPagoAcreedorListReport((PaymentSummaryList)entity.List);

					ShowReport(rpt);
				}

				if (true)
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[PAGOS_ESTIMADOS_NAVIERA];

					PaymentReportMng rptMng = new PaymentReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					PagoAcreedorListRpt rpt = rptMng.GetPagoAcreedorListReport((PaymentSummaryList)entity.List);

					ShowReport(rpt);
				}

				if (true)
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[PAGOS_ESTIMADOS_DESPACHANTE];

					PaymentReportMng rptMng = new PaymentReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					PagoAcreedorListRpt rpt = rptMng.GetPagoAcreedorListReport((PaymentSummaryList)entity.List);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintFacturasEmitidas())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[FACTURAS_EMITIDAS];

					CobroReportMng rptMng = new CobroReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					CobroClienteListRpt rpt = rptMng.GetCobroClienteListReport((ChargeSummaryList)entity.List);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintExistencias())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[EXISTENCIAS];

					ExpedientReportMng rptMng = new ExpedientReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					PartidaListRpt rpt = rptMng.GetPartidaListReport((BatchList)entity.List);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintEfectosNegociados())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[EFECTOS_NEGOCIADOS];

					CobroReportMng rptMng = new CobroReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					CobroClienteListRpt rpt = rptMng.GetCobroClienteListReport((ChargeSummaryList)entity.List);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintEfectosPendientes())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[EFECTOS_PENDIENTES];

					CobroReportMng rptMng = new CobroReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					ChargeListRpt rpt = rptMng.GetListReport((ChargeList)entity.List, null);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintAyudasCobradas())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[AYUDAS_REA_COBRADAS];

					CobroReportMng rptMng = new CobroReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					//ReportFormat format = new ReportFormat();
					//format.CampoOrdenacion = "Expediente";
					//format.Orden = CrystalDecisions.Shared.SortDirection.AscendingOrder;

					CobroREAListRpt rpt = rptMng.GetCobroREAListReport((ChargeList)entity.List, (CobroREAList)null);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintAyudasPendientes())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[AYUDAS_REA_PENDIENTES];

					ExpedientReportMng rptMng = new ExpedientReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					ReportFormat format = new ReportFormat();
					format.CampoOrdenacion = "Expediente";
					format.Orden = CrystalDecisions.Shared.SortDirection.AscendingOrder;

					InformeControlCobrosREARpt rpt = rptMng.GetControlCobrosREAReport((ExpedienteREAList)entity.List, format);

					ShowReport(rpt);
				}

				if (Library.Invoice.ModulePrincipal.GetBalancePrintBancos())
				{
					PgMng.Grow(String.Format(Resources.Messages.PRINTING, this.Text));

					NotifyEntity entity = List[BANCOS];

                    BankAccountReportMng rptMng = new BankAccountReportMng(AppContext.ActiveSchema, entity.Title, filter);

					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

                    ShowReport(rptMng.GetListReport((BankAccountList)entity.List));
				}
			}
			catch (Exception ex)
			{
				PgMng.ShowInfoException(ex);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		protected void ShowChartAction(ETipoChart tipo)
		{
			Chart chart = GetChart();

			System.Windows.Forms.DataVisualization.Charting.Series pasivo = new System.Windows.Forms.DataVisualization.Charting.Series
			{
				Name = "Pasivo",
				ChartType = SeriesChartType.Column,
				Color = Color.FromArgb(216, 43, 43),
				BorderColor = Color.Gray,
				//BorderWidth = 2,
				IsValueShownAsLabel = false
			};

			System.Windows.Forms.DataVisualization.Charting.Series activo = new System.Windows.Forms.DataVisualization.Charting.Series
			{
				Name = "Activo",
				ChartType = SeriesChartType.Column,
				Color = Color.FromArgb(65, 140, 240),
				BorderColor = Color.Gray,
				//BorderWidth = 2,
				IsValueShownAsLabel = false
			};

			chart.Series.Add(activo);
			chart.Series.Add(pasivo);

			int index = 1;
			NotifyEntity entity = null;

			switch (tipo)
			{
				case ETipoChart.Detallado:
					{
						//ACTIVO
						entity = List[FACTURAS_EMITIDAS];
						if (entity.Checked) activo.Points.Add(BuildPoint(entity, index++));

						entity = List[EFECTOS_PENDIENTES];
						if (entity.Checked) activo.Points.Add(BuildPoint(entity, index++));

						entity = List[BANCOS];
						if (entity.Checked) activo.Points.Add(BuildPoint(entity, index++));

						entity = List[CAJA];
						if (entity.Checked) activo.Points.Add(BuildPoint(entity, index++));

						entity = List[EXISTENCIAS];
						if (entity.Checked) activo.Points.Add(BuildPoint(entity, index++));

						entity = List[AYUDAS_REA_PENDIENTES];
						if (entity.Checked) activo.Points.Add(BuildPoint(entity, index++));

						entity = List[AYUDAS_POSEI_PENDIENTES];
						if (entity.Checked) activo.Points.Add(BuildPoint(entity, index++));

						entity = List[AYUDAS_FOMENTO_PENDIENTES];
						if (entity.Checked) activo.Points.Add(BuildPoint(entity, index++));

						entity = List[AYUDAS_REA_COBRADAS];
						if (entity.Checked) activo.Points.Add(BuildPoint(entity, index++));

						entity = List[RIESGO];
						if (entity.Checked) activo.Points.Add(BuildPoint(entity, index++));

						//PASIVO
						entity = List[FACTURAS_EXPLOTACION];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));

						entity = List[FACTURAS_ACREEDORES];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));

						entity = List[EFECTOS_PENDIENTES_VTO];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));

						entity = List[GASTOS_NOMINAS];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));

						entity = List[GASTOS_IRPF];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));

						entity = List[GASTOS_SEGSOCIAL];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));

						entity = List[OTROS_GASTOS];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));

						entity = List[PAGOS_ESTIMADOS_PROVEEDOR];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));

						entity = List[PAGOS_ESTIMADOS_TRANS_ORIGEN];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));

						entity = List[PAGOS_ESTIMADOS_TRANS_DESTINO];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));

						entity = List[PAGOS_ESTIMADOS_NAVIERA];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));

						entity = List[PAGOS_ESTIMADOS_DESPACHANTE];
						if (entity.Checked) pasivo.Points.Add(BuildPoint(entity, index++));
					}
					break;

				case ETipoChart.Agrupado:
					{
						entity = (NotifyEntity)Tree_TV.TopNode.Nodes[ACTIVO].Tag;
						activo.Points.Add(BuildPoint(entity, index++));

						entity = (NotifyEntity)Tree_TV.TopNode.Nodes[PASIVO].Tag;
						pasivo.Points.Add(BuildPoint(entity, index++));
					}
					break;
			}

			double max_value1 = pasivo.Points.FindMaxByValue("Y").YValues[0];
			double max_value2 = activo.Points.FindMaxByValue("Y").YValues[0];

			chart.ChartAreas[0].AxisY.Maximum = Math.Max(max_value1, max_value2) * 1.1;
			chart.ChartAreas[0].AxisY.Interval = Redondea(Math.Max(max_value1, max_value2) / 4);

			chart.Legends.Add(new Legend { Title = "Leyenda" });

			chart.ChartAreas[0].AxisY.Title = "IMPORTE (€)";
			chart.ChartAreas[0].AxisX.Title = string.Empty;
			chart.Titles[0].Text = "Balance a " + Date_TI.Text + "         Resultado: " + ((NotifyEntity)Tree_TV.TopNode.Tag).Total.ToString("C2");

			_chart_mng.ShowChart();
		}

		#endregion

		#region Events

		private void Tree_TV_AfterCheck(object sender, TreeViewEventArgs e)
		{
			((NotifyEntity)e.Node.Tag).Checked = e.Node.Checked;
			SetTotales();
		}

		private void Detallado_RB_CheckedChanged(object sender, EventArgs e)
		{
			ShowChartAction(ETipoChart.Detallado);
		}

		private void Agrupado_RB_CheckedChanged(object sender, EventArgs e)
		{
			ShowChartAction(ETipoChart.Agrupado);
		}

		#endregion
	}
}