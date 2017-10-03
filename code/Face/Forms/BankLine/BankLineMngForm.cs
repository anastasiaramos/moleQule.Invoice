using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Face;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports.BankLine;

namespace moleQule.Face.Invoice
{
    public partial class BankLineMngForm : BankLineMngBaseForm
	{
		#region Attributes & Properties

        public const string ID = "BankLineMngForm";
		public static Type Type { get { return typeof(BankLineMngForm); } }
        public override Type EntityType { get { return typeof(BankLine); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

        public BankLine _entity;
        public BankAccountInfo _cuenta;

		#endregion

		#region Factory Methods

		public BankLineMngForm()
			: this(null) { }

		public BankLineMngForm(Form parent)
			: this(false, parent, null, null) { }

		public BankLineMngForm(bool isModal, Form parent)
			: this(isModal, parent, null, null) { }

        public BankLineMngForm(bool isModal, Form parent, BankLineList list)
            : this(isModal, parent, list, null) { }

        public BankLineMngForm(bool isModal, Form parent, BankAccountInfo cuenta)
            : this(isModal, parent, null, cuenta) { }

        public BankLineMngForm(bool isModal, Form parent, BankLineList list, BankAccountInfo cuenta)
            : base(isModal, parent, list)
        {
            InitializeComponent();

            SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;

            SetMainDataGridView(Tabla);
            Datos.DataSource = BankLineList.NewList().GetSortedList();
            SortProperty = Fecha.DataPropertyName;
            SortDirection = ListSortDirection.Descending;

            _cuenta = cuenta;
        }

		#endregion

		#region Layout & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Observaciones.Tag = 1;

			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();

            SetActionStyle(molAction.CustomAction1, Resources.Labels.AUDITAR_TI, Properties.Resources.check);
            SetActionStyle(molAction.CustomAction2, Resources.Labels.ORIGEN_TI, Properties.Resources.goToSource);

            SetColumnActive(ControlsMng.GetColumn(Tabla, Fecha.DataPropertyName));
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

            BankLineInfo item = row.DataBoundItem as BankLineInfo;

            Tabla.SuspendLayout();

			Face.Common.ControlTools.Instance.SetRowColor(row, item.Auditado ? EEstado.Auditado : item.EEstado);

			if (item.EEstado == EEstado.Anulado) return;

			row.Cells[Importe.Name].Style.ForeColor = (item.Importe > 0) ? Face.Common.ControlTools.Instance.AbiertoStyle.ForeColor : Color.Red;
			row.Cells[Saldo.Name].Style.ForeColor = (item.Saldo > 0) ? Face.Common.ControlTools.Instance.AbiertoStyle.ForeColor : Color.Red;

            Tabla.ResumeLayout();
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateAnulado);
					HideAction(molAction.CustomAction1);
                    HideAction(molAction.CustomAction2);
					HideAction(molAction.Delete);

					break;

				case molView.Normal:
					
					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateAnulado);
					ShowAction(molAction.CustomAction1);
                    ShowAction(molAction.CustomAction2);
					HideAction(molAction.Delete);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "MovimientoBanco");

			long oid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (Library.Common.ModulePrincipal.GetUseActiveYear())
                        List = BankLineList.GetByCuentaList(_cuenta, Library.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
                        List = BankLineList.GetByCuentaList(_cuenta, false);					
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de MovimientoBancos");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
					if (_entity == null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
						BankLineList listA = BankLineList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						listA.UpdateSaldo();
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.Lock:
				case molAction.Unlock:
				case molAction.ChangeStateAnulado:
				case molAction.CustomAction1:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
					if (FilterType == IFilterType.Filter)
					{
						BankLineList listD = BankLineList.GetList(_filter_results);
						listD.RemoveItem(ActiveOID);
						listD.UpdateSaldo();
						_filter_results = listD.GetSortedList();
					}
					break;
			}

			RefreshSources();
			if (_entity != null) Select(_entity.Oid);
			_entity = null;
		}

		#endregion

		#region Actions

		public override void OpenAddForm()
		{
			BankLineAddForm form = new BankLineAddForm(_cuenta, this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new BankLineViewForm(ActiveItem, this));
		}

		public override void OpenEditForm()
		{
			BankLineEditForm form = new BankLineEditForm(ActiveItem, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }

		public override void ChangeStateAction(EEstadoItem estado)
		{
            _entity = BankLine.ChangeEstado(ActiveItem, Library.Common.EnumConvert.ToEEstado(estado));

			_action_result = DialogResult.OK;
		}

		public override void CustomAction1() { AuditarAction(); }

        public override void CustomAction2() { GoToSourceAction(); }

		public void AuditarAction()
		{
            BankLine.AuditarAction(ActiveItem, true);
		}

        public void GoToSourceAction()
        {
            if (ActiveItem == null) return;
            if (ActiveItem.EEstado == EEstado.Anulado) return;

            switch (ActiveItem.ETipoMovimientoBanco)
            {
                case EBankLineType.Cobro:
                    {
                        switch (ActiveItem.ETipoTitular)
                        {
                            case ETipoTitular.Cliente:
                                {
                                    ClienteInfo cliente = ClienteInfo.Get(ActiveItem.OidTitular, true);
                                    ChargeSummary item = ChargeSummary.Get(cliente);
                                    CobroEditForm form = new CobroEditForm(cliente.Oid, item, null, this);
                                    form.ShowDialog(this);
                                }
                                break;

                            case ETipoTitular.Fomento:
                                {
                                    ChargeInfo cobro = ChargeInfo.Get(ActiveItem.OidTitular, ETipoCobro.Fomento, false);
                                    CobrosFomentoEditForm form = new CobrosFomentoEditForm(cobro, this);
                                    form.ShowDialog(this);
                                }
                                break;

                            case ETipoTitular.REA:
                                {
                                    ChargeInfo cobro = ChargeInfo.Get(ActiveItem.OidTitular, ETipoCobro.REA, false);
                                    CobrosREAEditForm form = new CobrosREAEditForm(cobro, this);
                                    form.ShowDialog();
                                }
                                break;
                        }
                    }
                    break;

                case EBankLineType.PagoFactura:
                    {
                        ETipoAcreedor tipo = moleQule.Library.Store.EnumConvert.ToETipoAcreedor(ActiveItem.ETipoTitular);

                        if ((new List<ETipoTitular> { ETipoTitular.TransportistaOrigen, ETipoTitular.TransportistaOrigen }).Contains(ActiveItem.ETipoTitular))
                        {
                            TransporterInfo transporter = TransporterInfo.Get(ActiveItem.OidTitular,  moleQule.Library.Store.EnumConvert.ToETipoAcreedor(ActiveItem.ETipoTitular), false);
                            tipo = transporter.ETipoAcreedor;
                        }

                        PaymentSummary item = PaymentSummary.Get(tipo, ActiveItem.OidTitular);
                        PaymentEditForm form = new PaymentEditForm(this, ActiveItem.OidTitular, item);
                        form.ShowDialog(this);
                    }
                    break;

                case EBankLineType.PagoGasto:
                    {
                        ExpensePaymentEditForm form = new ExpensePaymentEditForm(ActiveItem.OidTitular, ETipoPago.Gasto, this);
                        form.ShowDialog();
                    }
                    break;

                case EBankLineType.PagoNomina:
                    {
                        PaymentSummary item = PaymentSummary.Get(ETipoAcreedor.Empleado, ActiveItem.OidTitular);
                        EmployeePaymentEditForm form = new EmployeePaymentEditForm(this, ActiveItem.OidTitular, item);
                        form.ShowDialog();
                    }
                    break;

                case EBankLineType.PagoPrestamo:
                    {
                        LoanPaymentEditForm form = new LoanPaymentEditForm(ActiveItem.OidTitular, ETipoPago.Prestamo, this);
                        form.ShowDialog();
                    }
                    break;

                case EBankLineType.Prestamo:
                    {
                        LoanEditForm form = new LoanEditForm(ActiveItem.OidTitular, this);
                        form.ShowDialog();
                    }
                    break;

                case EBankLineType.EntradaCaja:
                case EBankLineType.SalidaCaja:
                    {
                        CashLineInfo linea = CashLineInfo.Get(ActiveItem.OidOperacion);

                        if (linea.OidCierre != 0)
                        {
                            CashCountEditForm form = new CashCountEditForm(linea.OidCierre, this);
                            form.ShowDialog();
                        }
                        else
                        {
                            CashEditForm form = new CashEditForm(linea.OidCaja, this);
                            form.ShowDialog();
                        }
                    }
                    break;

                case EBankLineType.Traspaso:
                    {
                        TraspasoEditForm form = new TraspasoEditForm(ActiveItem.OidTitular, this);
                        form.ShowDialog();
                    }
                    break;

                case EBankLineType.ExtractoTarjeta:
                    {
                        CreditCardPaymentEditForm form = new CreditCardPaymentEditForm(ActiveItem.OidOperacion, ETipoPago.ExtractoTarjeta, this);
                        form.Show();
                    }
                    break;
            }
        }

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
            BankLineReportMng reportMng = new BankLineReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ReportClass report = reportMng.GetListReport((BankLineList.GetList(Datos.DataSource as IList<BankLineInfo>)));

			PgMng.FillUp();

			ShowReport(report);
		}

		#endregion
	}
    
    public partial class BankLineMngBaseForm : Skin06.EntityMngSkinForm<BankLineList, BankLineInfo>
    {
        public BankLineMngBaseForm()
            : this(false, null, null) { }

        public BankLineMngBaseForm(bool isModal, Form parent, BankLineList lista)
            : base(isModal, parent, lista) { }
    }
}

