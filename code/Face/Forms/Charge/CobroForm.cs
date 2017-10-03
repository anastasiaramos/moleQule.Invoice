using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule.Face.Invoice;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cobro;
using moleQule.Library.Invoice.Reports.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class CobroForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

		public virtual Cliente Entity { get { return null; } set { } }
        public virtual ChargeSummary Resumen { get { return null; } set { } }

        protected Charge Cobro { get { return Datos_Cobros.Current != null ? Datos_Cobros.Current as Charge : null; } }
        protected OutputInvoiceInfo Factura { get { return Datos_FCobro.Current != null ? Datos_FCobro.Current as OutputInvoiceInfo: null; } }
		
		protected OutputInvoiceList _facturas_cliente;

        protected SerieList _series;
        protected ChargeSummary _resumen;
        protected ChargeInfo _cobro;

        #endregion

        #region Factory Methods

		public CobroForm()
			: this(null) { }

        public CobroForm(Form parent)  
			: this(-1, parent) {}

        public CobroForm(bool is_modal)
            : base(-1, is_modal)
        {
            InitializeComponent();
        }

        public CobroForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
        }

        #endregion

		#region Authorization

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules()
		{
			UnlockItem_TMI.Visible = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
			UnlockItem_TMI.Visible = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);

			LockItem_TMI.Visible = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
			LockItem_TMI.Visible = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
		}

		#endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Observaciones.Tag = 1;

			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Cobros_DGW, cols);
		}

        public override void FormatControls()
        {
            if (Cobros_DGW == null) return;

			int maxWidth = (Screen.PrimaryScreen.WorkingArea.Width > 1500) ? 1500 : Screen.PrimaryScreen.WorkingArea.Width;

			MaximizeForm(new Size(maxWidth, 0));
			Content_SC.SplitterDistance = maxWidth;
			Pendientes_SC.SplitterDistance = maxWidth;

			base.FormatControls();

			HideAction(molAction.ShowDocuments);

			Cobros_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 
			Facturas_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			Pendientes_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

		protected virtual void SetGridColors(string grid_name)
		{
			if (grid_name == Cobros_DGW.Name)
			{
                Charge item;

				foreach (DataGridViewRow row in Cobros_DGW.Rows)
				{
					if (row.IsNewRow) return;

                    item = row.DataBoundItem as Charge;
					if (item == null) continue;

					Face.Common.ControlTools.Instance.SetRowColorIM(row, item.EEstado);

					if (item.EEstado != EEstado.Anulado)
						row.Cells[PendienteAsignacion.Index].Style.BackColor = (item.Pendiente > 0) ? Color.LightGreen : row.Cells[CobroID.Index].Style.BackColor;
				}
			}
			else if (grid_name == Pendientes_DGW.Name)
			{
				OutputInvoiceInfo item;

				foreach (DataGridViewRow row in Pendientes_DGW.Rows)
				{
					if (row.IsNewRow) return;

					item = row.DataBoundItem as OutputInvoiceInfo;

					if (item == null) continue;

					if (item.Cobrada)
					{
						row.Cells[DiasCobroPendiente.Name].Style.BackColor = Color.LightGreen;
						row.Cells[DiasCobroPendiente.Name].Style.SelectionBackColor = Color.LightGreen;
					}
					else if (0 <= item.DiasTranscurridos && item.DiasTranscurridos < 15)
					{
						row.Cells[DiasCobroPendiente.Name].Style.BackColor = Color.FromArgb(255, 255, 192);
						row.Cells[DiasCobroPendiente.Name].Style.SelectionBackColor = Color.FromArgb(255, 255, 192);
					}
					else if (15 <= item.DiasTranscurridos && item.DiasTranscurridos < 31)
					{
						row.Cells[DiasCobroPendiente.Name].Style.BackColor = Color.FromArgb(255, 192, 128);
						row.Cells[DiasCobroPendiente.Name].Style.SelectionBackColor = Color.FromArgb(255, 192, 128);
					}
					else if (31 <= item.DiasTranscurridos && item.DiasTranscurridos < 45)
					{
						row.Cells[DiasCobroPendiente.Name].Style.BackColor = Color.Orange;
						row.Cells[DiasCobroPendiente.Name].Style.SelectionBackColor = Color.Orange;
					}
					else if (45 <= item.DiasTranscurridos && item.DiasTranscurridos < 60)
					{
						row.Cells[DiasCobroPendiente.Name].Style.BackColor = Color.OrangeRed;
						row.Cells[DiasCobroPendiente.Name].Style.SelectionBackColor = Color.OrangeRed;
					}
					else
					{
						row.Cells[DiasCobroPendiente.Name].Style.BackColor = Color.Red;
						row.Cells[DiasCobroPendiente.Name].Style.SelectionBackColor = Color.Red;
					}
				}
			}

			if (Resumen != null)
			{
				if (Resumen.LimiteCredito < Resumen.CreditoDispuesto)
					CreditoDispuesto_TB.BackColor = Color.Red;
				else if (Resumen.CreditoDispuesto > 0)
					CreditoDispuesto_TB.BackColor = Color.FromArgb(255, 192, 128);
				else
					CreditoDispuesto_TB.BackColor = CreditoDisponible_TB.BackColor;

				if (Resumen.DudosoCobro != 0)
					DudosoCobro_TB.BackColor = Color.Red;
				else
					DudosoCobro_TB.BackColor = CreditoDisponible_TB.BackColor;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			UpdateFacturasPendientes();
			PgMng.Grow(string.Empty, "Facturas Pendientes");
		}

		protected override void SetUnlinkedGridValues(string grid_name)
		{
			OutputInvoiceInfo item;

			if (grid_name == Facturas_DGW.Name)
			{
                Charge cobro = (Charge)Datos_Cobros.Current;

				foreach (DataGridViewRow row in Facturas_DGW.Rows)
				{
					if (row.IsNewRow) return;

					item = row.DataBoundItem as OutputInvoiceInfo;
					if (item == null) continue;

					row.Cells[FacturaSerie.Index].Value = _series.GetItem(item.OidSerie).Identificador;
					row.Cells[Asignado.Index].Value = cobro.CobroFacturas.GetItemByFactura(item.Oid).Cantidad;
					row.Cells[FechaAsignacion.Index].Value = cobro.CobroFacturas.GetItemByFactura(item.Oid).FechaAsignacion;
					row.Cells[Dias.Index].Value = cobro.Fecha.Subtract(item.Fecha).Days;
                    row.Cells[FacturaAnteriores.Index].Value = item.Cobrado - cobro.CobroFacturas.GetItemByFactura(item.Oid).Cantidad;
				}
			}
		}

		protected virtual void UpdateFacturasCobro() {}

		protected virtual void UpdateFacturasPendientes() {}

		#endregion

		#region Validation & Format

		#endregion

		#region Business Methods

		#endregion

		#region Actions

		protected override void DocumentsAction()
        {
            try
            {
                AgenteEditForm form = new AgenteEditForm(typeof(Charge), Cobro.GetInfo() as IAgenteHipatia);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
                    AgenteAddForm form = new AgenteAddForm(typeof(Charge), Cobro.GetInfo() as IAgenteHipatia);
                    form.ShowDialog(this);
                }
                else
                    MessageBox.Show(ex.Message, moleQule.Face.Resources.Labels.ERROR_TITLE);
            }
        }

        protected override void PrintAction()
        {
            PrintObject();
        }

		protected virtual void CobrosDefaultAction() { EditCobroAction(); }
		protected virtual void NewCobroAction() {}
		protected virtual void ViewCobroAction() {}
		protected virtual void EditCobroAction() {}
		protected virtual void DeleteCobroAction() {}
		protected virtual void LockCobroAction() { }
		protected virtual void UnlockCobroAction() { }
		protected virtual void PrintCobroAction() {}

        protected virtual void EditClienteAction() 
        {
            ClientEditForm form = new ClientEditForm(Entity, this);
            form.ShowDialog(this);

            _resumen.Refresh(Entity);
            Datos_Resumen.DataSource = _resumen;
            Datos_Resumen.ResetBindings(false);
        }

		protected virtual void FacturasDefaultAction() 
		{
			if (Factura == null) return;

			InvoiceViewForm form = new InvoiceViewForm(Factura.Oid, this);
			form.ShowDialog(this);
		}

		protected virtual void EditPendienteAction()
		{
			if (Pendientes_DGW.CurrentRow == null) return;

			OutputInvoiceInfo factura = Pendientes_DGW.CurrentRow.DataBoundItem as OutputInvoiceInfo;

			InvoiceEditForm form = new InvoiceEditForm(factura.Oid, this);
			form.ShowDialog(this);

			UpdateFacturasPendientes();
		}

		protected virtual void VerPendienteAction()
		{
			if (Pendientes_DGW.CurrentRow == null) return;

			OutputInvoiceInfo factura = Pendientes_DGW.CurrentRow.DataBoundItem as OutputInvoiceInfo;

			InvoiceViewForm form = new InvoiceViewForm(factura.Oid, this);
			form.ShowDialog(this);
		}

		protected virtual void PrintPendienteAction()
		{
			if (Pendientes_DGW.CurrentRow == null) return;

			OutputInvoiceInfo in_invoice = Pendientes_DGW.CurrentRow.DataBoundItem as OutputInvoiceInfo;

			PgMng.Reset(6, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

            OutputInvoiceReportMng reportMng = new OutputInvoiceReportMng(AppContext.ActiveSchema, this.Text, string.Empty);

			SerieInfo serie = SerieInfo.Get(in_invoice.OidSerie, false);
			PgMng.Grow();

			ClienteInfo client = ClienteInfo.Get(in_invoice.OidCliente, false);
			PgMng.Grow();

			TransporterInfo transporter = TransporterInfo.Get(in_invoice.OidTransportista, ETipoAcreedor.TransportistaDestino, false);
			PgMng.Grow();

			FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

			conf.nota = (client.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
			conf.nota += Environment.NewLine + (in_invoice.Nota ? serie.Cabecera : "");
			conf.cuenta_bancaria = in_invoice.CuentaBancaria;
			PgMng.Grow();

			OutputInvoiceInfo item = OutputInvoiceInfo.Get(in_invoice.Oid, true);
			PgMng.Grow();

			ReportClass report = reportMng.GetDetailReport(item, serie, client, transporter, conf);
			PgMng.FillUp();

			ShowReport(report);
		}

		protected virtual void PrintPendienteListAction()
		{
            OutputInvoiceReportMng reportMng = new OutputInvoiceReportMng(AppContext.ActiveSchema, Resources.Labels.FACTURA_PENDIENTES, "Cliente = " + Entity.Nombre);

			ReportClass report = reportMng.GetListReport(OutputInvoiceList.GetList(Datos_FPendientes.DataSource as IList<OutputInvoiceInfo>),
															SerieList.GetList(false));

			ShowReport(report);
		}

		public override void PrintObject()
		{
			CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema, Resources.Labels.COBRO_CLIENTES, "Cliente: " + Entity.Nombre);
			CobroClienteDetailRpt rpt = reportMng.GetCobroClienteDetailReport(Entity.GetInfo(), Resumen);

			ShowReport(rpt);
		}

		#endregion

        #region Buttons

		private void AddCobro_TI_Click(object sender, EventArgs e) 
		{
			NewCobroAction();
		}

		private void EditCobro_TI_Click(object sender, EventArgs e)
		{
			EditCobroAction();
		}

		private void ViewCobro_TI_Click(object sender, EventArgs e)
		{
			ViewCobroAction();
		}

		private void UnlockItem_TMI_Click(object sender, EventArgs e)
		{
			UnlockCobroAction();
		}

		private void LockItem_TMI_Click(object sender, EventArgs e)
		{
			LockCobroAction();
		}

		private void NullItem_TMI_Click(object sender, EventArgs e)
		{
			DeleteCobroAction();
		}

		private void PrintCobro_TI_Click(object sender, EventArgs e)
		{
			PrintCobroAction();
		}

		private void EditPendiente_TI_Click(object sender, EventArgs e)
		{
			EditPendienteAction();
		}

		private void VerPendiente_TI_Click(object sender, EventArgs e)
		{
			VerPendienteAction();
		}

		private void PrintPendiente_TI_Click(object sender, EventArgs e)
		{
			PrintPendienteAction();
		}

		private void PrintListPendiente_TI_Click(object sender, EventArgs e)
		{
			PrintPendienteListAction();
		}

        #endregion

        #region Events

        private void CobroForm_Shown(object sender, EventArgs e)
        {
            SetUnlinkedGridValues(Facturas_DGW.Name);
			SetGridColors(Pendientes_DGW.Name);
			SetGridColors(Cobros_DGW.Name);
        }
        
        private void Cobros_DGW_DoubleClick(object sender, EventArgs e)
        {
            CobrosDefaultAction();
        }

        private void Facturas_DGW_DoubleClick(object sender, EventArgs e)
        {
			FacturasDefaultAction();
        }

        private void Datos_Cobros_CurrentItemChanged(object sender, EventArgs e)
        {
            UpdateFacturasCobro();
        }

        private void Cliente_BT_Click(object sender, EventArgs e)
        {
            EditClienteAction();
        }

        private void Cobros_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EditCobroAction();
        }

        #endregion
    }
}

