using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class LoanForm : Skin04.ItemMngSkinForm
    {
        #region Attributes & Properties

        public override Type EntityType { get { return typeof(Loan); } }

        public const string ID = "LoanForm";
		public static Type Type { get { return typeof(LoanForm); } }

        protected override int BarSteps { get { return base.BarSteps + 0; } }

        public virtual Loan Entity { get { return null; } set { } }
        public virtual LoanInfo EntityInfo { get { return null; } }

		
        #endregion

        #region Factory Methods

        public LoanForm() 
			: this(-1, null) { }

		public LoanForm(long oid, Form parent)
			: this(oid, null, true, parent) { }

		public LoanForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}
		
        #endregion

        #region Layout

        protected override void SetView(molView view)
        {
            base.SetView(view);

            switch (_view_mode)
            {
                case molView.Select:
                case molView.Normal:
                case molView.Enbebbed:

                    ShowAction(molAction.CustomAction1);
                    HideAction(molAction.CustomAction2);
                    HideAction(molAction.CustomAction3);
                    HideAction(molAction.CustomAction4);
                    HideAction(molAction.ShowDocuments);
                    HideAction(molAction.PrintDetail);
                    break;
            }
        }

        public override void FormatControls()
        {
            if (Pagos_DGW == null) return;

            base.MaximizeForm(new Size(1200, 0));
            base.FormatControls();

            ControlsMng.CenterLeft(Remarks_GB);
            General_GB.Left = Remarks_GB.Left;
            InterestRate_GB.Left = General_GB.Right + 6;

            SetActionStyle(molAction.CustomAction1, Resources.Labels.GENERAR_PAGOS, Properties.Resources.pago);

            Pagos_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        protected virtual void SetGridColors(string grid_name)
        {
            if (grid_name == Pagos_DGW.Name)
            {
                PaymentInfo item;

                foreach (DataGridViewRow row in Pagos_DGW.Rows)
                {
                    if (row.IsNewRow) return;

                    item = row.DataBoundItem as PaymentInfo;
                    if (item == null) continue;

                    Face.Common.ControlTools.Instance.SetRowColorIM(row, item.EEstado);

                    row.Cells[Pendiente.Index].Style.BackColor = (item.Pendiente > 0) ? Color.LightGreen : row.Cells[IdPago.Index].Style.BackColor;
                }
            }
        }

		#endregion
		
		#region Source

        /// <summary>
        /// Asigna el objeto principal al origen de datos principal
		/// y las listas hijas a los origenes de datos correspondientes
        /// </summary>
        protected override void RefreshMainData()
        {
            UpdatePayments();
        }

        /// <summary>
        /// Asigna los datos a los origenes de datos secundarios
        /// </summary>
        public override void RefreshSecondaryData()
        {
            EFormaPago[] list_fp = {EFormaPago.Contado, EFormaPago.Trimestral, EFormaPago.XDiasMes};
            Datos_FormaPago.DataSource = Library.Common.EnumText<EFormaPago>.GetList(list_fp, true);

            PgMng.Grow();
        }

        protected virtual void UpdatePayments() { }

		#endregion

        #region Validation & Format

        #endregion

        #region Print

        //public override void PrintObject()
        //{
        //    PrestamoReportMng reportMng = new PrestamoReportMng(AppContext.ActiveSchema);
        //    ReportViewer.SetReport(reportMng.GetReport(EntityInfo);
        //    ReportViewer.ShowDialog();
        //}

        #endregion

        #region Actions

        protected virtual void NoContabilizarAction() { }
        protected virtual void SetBankAccountAction() { }
        protected virtual void SelectPaymentWayAction() { }
        protected virtual void AddAllPaymentsAction() { }
        protected virtual void NewPaymentAction() { }
        protected virtual void EditPaymentAction() { }
        protected virtual void ViewPaymentAction() { }
        protected virtual void BorrarPagoAction() { }
        protected virtual void DeletePagoAction() { }
        protected virtual void LockPaymentAction() { }
        protected virtual void UnlockPaymentAction() { }
        protected virtual void AddPeriodoTipoInteresAction() { }
        protected virtual void DeletePeriodoTipoInteresAction() { }

        protected override void PrintAction()
        {
            PrintObject();
        }

        protected override void DocumentsAction()
        {
            try
            {
                AgenteEditForm form = new AgenteEditForm(typeof(Loan), EntityInfo as IAgenteHipatia);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
                    AgenteAddForm form = new AgenteAddForm(typeof(Loan), EntityInfo as IAgenteHipatia);
                    form.ShowDialog(this);
                }
            }
        }

        protected virtual void UpdateImportes() { }

        #endregion

        #region Buttons

        private void CuentaBancaria_BT_Click(object sender, EventArgs e)
        {
            SetBankAccountAction();
        }

        protected virtual void LoadData()
        {
            if (Pestanas_TP.SelectedTab == Pagos_TP)
            {
                //LoadPagosAction();
            }
        }

        private void AddPeriodo_TI_Click(object sender, EventArgs e)
        {
            AddPeriodoTipoInteresAction();
        }

        private void DeletePeriodo_TI_Click(object sender, EventArgs e)
        {
            DeletePeriodoTipoInteresAction();
        }

        private void AddAll_TI_Click(object sender, EventArgs e) { AddAllPaymentsAction(); }

        #endregion

        #region Events

        private void Datos_DataSourceChanged(object sender, EventArgs e)
        {
            //SetDependentControlSource(ID_GB.Name);
        }

        private void Pagos_DGW_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditPaymentAction();
        }

        private void Pagos_DGW_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            SetUnlinkedGridValues(Pagos_DGW.Name);
        }

        private void Pestanas_TP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void AddPago_TI_Click(object sender, EventArgs e)
        {
            NewPaymentAction();
        }

        private void EditPago_TI_Click(object sender, EventArgs e)
        {
            EditPaymentAction();
        }

        private void ViewPago_TI_Click(object sender, EventArgs e)
        {
            ViewPaymentAction();
        }

        private void LoanForm_Shown(object sender, EventArgs e)
        {
            SetGridColors(Pagos_DGW.Name);
        }

        private void UnlockItem_TMI_Click(object sender, EventArgs e)
        {
            UnlockPaymentAction();
        }

        private void LockItem_TMI_Click(object sender, EventArgs e)
        {
            LockPaymentAction();
        }

        private void NullItem_TMI_Click(object sender, EventArgs e)
        {
            DeletePagoAction();
        }

        private void TiposInteres_DGW_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void TiposInteres_DGW_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateImportes();
        }

        private void Importe_NTB_Validated(object sender, EventArgs e)
        {
            UpdateImportes();
        }

        private void Cuota_NTB_Validated(object sender, EventArgs e)
        {
            UpdateImportes();
        }

        private void Cuotas_TB_Validated(object sender, EventArgs e)
        {
            UpdateImportes();
        }

        private void NoContabilizar_BT_Click(object sender, EventArgs e)
        {
            NoContabilizarAction();
        }
		
        #endregion	
    }
}
