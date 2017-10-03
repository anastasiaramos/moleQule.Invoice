using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;

using CslaEx;

using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
    public partial class CobroREAAddForm : CobroREAUIForm
    {

        #region Attributes & Properties
		
        public new const string ID = "CobroREAAddForm";
		public new static Type Type { get { return typeof(CobroREAAddForm); } }

		#endregion
		
        #region Factory Methods

        public CobroREAAddForm() 
			: this(null) {}

        public CobroREAAddForm(Form parent)
            : base(parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData()
        {
            _entity = Cobro.New(ETipoCobro.REA);
            _entity.BeginEdit();
        }

        #endregion

        #region Layout & Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            _facturas_todas = FacREAList.GetNoCobradas();
            _facturas = FacREAList.GetSortedList(_facturas_todas, "NExpediente", ListSortDirection.Ascending);
            Datos_Facturas.DataSource = _facturas;
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
