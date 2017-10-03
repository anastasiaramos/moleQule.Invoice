using System;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cobro;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
    public partial class CobroREAEditForm : CobroREAUIForm
    {

        #region Attributes & Properties
		
        public new const string ID = "CobroREAEditForm";
		public new static Type Type { get { return typeof(CobroREAEditForm); } }

        public override CobroInfo EntityInfo { get { return _entity.GetInfo(); } }

		#endregion
		
        #region Factory Methods

        public CobroREAEditForm()
            : this(-1, null) { }

        public CobroREAEditForm(long oid)
            : this(oid, null) { }

        public CobroREAEditForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            if (Entity != null)
            {
                SetFormData();
                this.Text = Resources.Labels.COBRO_REA_EDIT_TITLE + " " + Entity.IdCobro;
            }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = Cobro.Get(oid, ETipoCobro.REA);
            _entity.BeginEdit();
        }

        #endregion

        #region Layout

		public override void FormatControls()
        {
            Imprimir_Button.Enabled = true;
            Imprimir_Button.Visible = true;

			if ((_entity.EMedioPago == EMedioPago.CompensacionFactura) ||
				(_entity.EMedioPago == EMedioPago.Efectivo))
				MedioPago_BT.Enabled = false;

            base.FormatControls();
        }

		#endregion

		#region Source
		
		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            _facturas_todas = FacREAList.GetListByCobroAndPendientes(_entity.Oid);
            _facturas = FacREAList.GetSortedList(_facturas_todas, "NExpediente", ListSortDirection.Ascending);
            Datos_Facturas.DataSource = _facturas;
  
            PgMng.Grow();

			UpdateAsignado();

            base.RefreshMainData();
        }

        #endregion

        #region Actions

        protected override void PrintAction()
        {
            CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema);

            FacREAList expedientes = FacREAList.GetListByCobro(Entity.Oid);

            CobroREADetailRpt report = reportMng.GetDetallesCobroREAIndividualReport(EntityInfo, expedientes);

			ShowReport(report);
        }

        #endregion

    }
}
