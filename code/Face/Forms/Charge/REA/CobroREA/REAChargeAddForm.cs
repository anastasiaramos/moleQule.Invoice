using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class REAChargeAddForm : REAChargeUIForm
    {
        #region Attributes & Properties

        public new const string ID = "REAChargeAddForm";
		public new static Type Type { get { return typeof(REAChargeAddForm); } }

		#endregion
		
        #region Factory Methods

        public REAChargeAddForm() 
			: this(null) {}

        public REAChargeAddForm(Form parent)
            : base(parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData()
        {
            _entity = Charge.New(ETipoCobro.REA);
            _entity.BeginEdit();

            _rea_expedients = REAExpedients.GetUnlinkedList(false);
        }

        #endregion

        #region Layout & Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            _facturas_todas = FacREAList.GetNoCobradas();
            _facturas = FacREAList.GetSortedList(_facturas_todas, "NExpediente", ListSortDirection.Ascending);
            REAExpedients_BS.DataSource = _facturas;
            SetGridColors(Facturas_DGW);
            PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion

        #region Events

        private void Importe_NTB_TextChanged(object sender, EventArgs e)
        {
            LiberarTodoAction();
        }

        private void Importe_NTB_Validated(object sender, EventArgs e)
        {
            UpdateAsignado();
        }

        #endregion
    }
}
