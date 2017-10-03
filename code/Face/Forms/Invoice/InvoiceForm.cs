using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using CrystalDecisions.CrystalReports.Engine;
using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class InvoiceForm : Skin04.ItemMngSkinForm
    {
        #region Business Methods

        public override Type EntityType { get { return typeof(OutputInvoice); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        public virtual OutputInvoice Entity { get { return null; } set { } }
		public virtual OutputInvoiceInfo EntityInfo { get { return null; } }

		protected TransporterInfo _transporter = null;
		protected SerieInfo _serie = null;
        public CompanyInfo _company;

        #endregion

        #region Factory Methods

		public InvoiceForm()
			: this(null) {}

        public InvoiceForm(Form parent) 
			: this(-1, true, parent) {}

        public InvoiceForm(long oid)
			: this(oid, false, null) {}

        public InvoiceForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout

    	public override void FitColumns()
		{
			CurrencyManager cm;
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

			cm = (CurrencyManager)BindingContext[Lines_DGW.DataSource];
			cm.SuspendBinding();

			ConceptosConcepto.Tag = 1;

			cols.Add(ConceptosConcepto);

			ControlsMng.MaximizeColumns(Lines_DGW, cols);

			Lines_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
		}

        public override void FormatControls()
        {
            if (Lines_DGW == null) return;

            base.MaximizeForm(new Size(1200,0));
            base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.ALBARAN_NO_FACTURADOS, Properties.Resources.albaran_emitido_abierto);

            //IDE Compatibility
            try
            {
                LiPrecio.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
            }
            catch { }
			Fecha_DTP.Checked = true;
			DiasPago_NTB.TextAlign = HorizontalAlignment.Center;	
        }

		protected virtual void HideComponents() { }

        protected virtual void SetReadOnlyCellsValue()
        {
            if (Lines_DGW.Columns[Lines_DGW.CurrentCell.ColumnIndex].DataPropertyName != LiStoreID.DataPropertyName
                && Lines_DGW.Columns[Lines_DGW.CurrentCell.ColumnIndex].DataPropertyName != LiDiscount.DataPropertyName
                && Lines_DGW.Columns[Lines_DGW.CurrentCell.ColumnIndex].DataPropertyName != LiTaxes.DataPropertyName
                && Lines_DGW.Columns[Lines_DGW.CurrentCell.ColumnIndex].DataPropertyName != LiTotal.DataPropertyName)
            {
                if (Lines_DGW.CurrentRow != null) Lines_DGW.CurrentRow.ReadOnly = false;
                if (Lines_DGW.CurrentCell != null) Lines_DGW.CurrentCell.ReadOnly = false;
            }
        }

        protected override void SetView(molView view)
        {
            base.SetView(view);

            switch (_view_mode)
            {
                case molView.Select:
                case molView.Normal:
                case molView.Enbebbed:

                    ShowAction(molAction.CustomAction1);
                    break;
            }
        }

        #endregion

        #region Source

		public override void RefreshSecondaryData()
		{
			_company = CompanyInfo.Get(AppContext.ActiveSchema.Oid);
			PgMng.Grow();

			PaymentMethod_BS.DataSource = Library.Common.EnumText<EMedioPago>.GetList(false);
			PgMng.Grow(string.Empty, "Medios de Pago");

            EFormaPago[] formas_pago = { EFormaPago.Contado, EFormaPago.XDiasFechaFactura, EFormaPago.XDiasMes, EFormaPago.Trimestral};

			Datos_FormaPago.DataSource = Library.Common.EnumText<EFormaPago>.GetList(formas_pago, false);
			PgMng.Grow(string.Empty, "Formas de Pago");
		}

        #endregion

        #region Actions

		protected override void PrintAction() { PrintObject(); }

        public override void PrintObject()
        {
            if (Client_BS.Current == null) return;

            OutputInvoiceReportMng reportMng = new OutputInvoiceReportMng(AppContext.ActiveSchema);
            FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

            ClienteInfo client = Client_BS.Current as ClienteInfo;

            conf.nota = (client.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
            conf.nota += (conf.nota != string.Empty) ? Environment.NewLine : string.Empty;
            conf.nota += EntityInfo.Nota ? Nota_TB.Text : "";

            conf.cuenta_bancaria = Cuenta_TB.Text;

            ReportClass report = reportMng.GetDetailReport((EntityInfo.ConceptoFacturas != null) ? EntityInfo : Entity.GetInfo(true), _serie, client, _transporter, conf);

            if (SettingsMng.Instance.GetUseDefaultPrinter())
            {
                int n_copias = SettingsMng.Instance.GetDefaultNCopies();
                PrintReport(report, n_copias);
            }
            else
                ShowReport(report);

        }

        protected virtual void EditAccountAction() {}
        protected virtual void EditClientAction() {}
        protected virtual void DeleteDeliveryAction() {}
        protected virtual void AddDeliveryAction() {}
		protected virtual void SelectLineTaxAction() { }
		protected virtual void UpdateInvoiceAction() { }

        #endregion

        #region Buttons

		private void AddDelivery_BT_Click(object sender, EventArgs e) { AddDeliveryAction(); }
		private void BankAccount_BT_Click(object sender, EventArgs e) { EditAccountAction(); }
		private void Client_BT_Click(object sender, EventArgs e) { EditClientAction(); }
        private void DeleteDelivery_BT_Click(object sender, EventArgs e) { DeleteDeliveryAction(); }

        #endregion

        #region Events

        private void NFacturaManual_CKB_CheckedChanged(object sender, EventArgs e)
        {
            NFactura_TB.ReadOnly = !IDManual_CKB.Checked;
            NFactura_TB.BackColor = NFactura_TB.ReadOnly ? DiasPago_NTB.BackColor : Color.White;
			NFactura_TB.ForeColor = NFactura_TB.ReadOnly ? DiasPago_NTB.ForeColor : Color.Navy;
        }

		private void Lines_DGW_CellValidated(object sender, DataGridViewCellEventArgs e) { UpdateInvoiceAction(); }

		private void Lines_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lines_DGW.Columns[e.ColumnIndex].Name == LiTaxes.Name) SelectLineTaxAction();
		}

        private void Lines_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SetReadOnlyCellsValue();
        }

        #endregion
    }
}

