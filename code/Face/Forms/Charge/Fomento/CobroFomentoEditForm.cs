using System;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports.Cobro;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
    public partial class CobroFomentoEditForm : CobroFomentoUIForm
    {

        #region Attributes & Properties
		
        public new const string ID = "CobroFomentoEditForm";
		public new static Type Type { get { return typeof(CobroFomentoEditForm); } }

        public override CobroInfo EntityInfo { get { return _entity.GetInfo(); } }

		#endregion
		
        #region Factory Methods

        public CobroFomentoEditForm()
            : this(-1, null) { }

        public CobroFomentoEditForm(long oid)
            : this(oid, null) { }

        public CobroFomentoEditForm(long oid, Form parent)
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
            _entity = Cobro.Get(oid, ETipoCobro.Fomento);
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

            _facturas_todas = LineaFomentoList.GetByCobroAndPendientesList(_entity.Oid);
            //_facturas = LineaFomentoList.GetSortedList(_facturas_todas, "NExpediente", ListSortDirection.Ascending);
            _facturas = _facturas_todas.GetSortedList();
            Datos_Facturas.DataSource = _facturas;
            SetComboYears();
  
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
