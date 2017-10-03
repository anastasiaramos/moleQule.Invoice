using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class LoanViewForm : LoanForm
    {
        #region Attributes & Properties
		
        public new const string ID = "PrestamoViewForm";
		public new static Type Type { get { return typeof(LoanViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private LoanInfo _entity;

        public override LoanInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public LoanViewForm(long oid, Form parent)
            : base(oid, null, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = LoanInfo.Get(oid);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Layout

        /// <summary>Da formato visual a los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

		#endregion
		
		#region Source
		
        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();	
			
            base.RefreshMainData();
        }

        protected override void UpdatePayments()
        {
            Payments_BS.DataSource = _entity.Payments;
            Payments_BS.ResetBindings(false);
            SetGridColors(Pagos_DGW.Name);
        }
		
        #endregion

        #region Print

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        protected override void ViewPaymentAction()
        {
            if (Payments_BS.Current == null) return;
            LoanPaymentViewForm form = new LoanPaymentViewForm((Payments_BS.Current as PaymentInfo).Oid, ETipoPago.Prestamo, this);
            form.ShowDialog();
        }
        
        #endregion

        #region Events

        #endregion
    }
}