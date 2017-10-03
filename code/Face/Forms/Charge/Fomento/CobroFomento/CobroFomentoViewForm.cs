using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face.Common;

namespace moleQule.Face.Invoice
{
    public partial class CobroFomentoViewForm : CobroFomentoForm
    {
        #region Attributes & Properties

        public new const string ID = "CobroFomentoViewForm";
        public new static Type Type { get { return typeof(CobroFomentoViewForm); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual y que se va a editar.
        /// </summary>
        protected ChargeInfo _entity;

        public override ChargeInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected CobroFomentoViewForm() 
			: this(-1, false, null) { }

        public CobroFomentoViewForm(Form parent) 
			: this(-1, true, parent) { }

        public CobroFomentoViewForm(long oid) 
			: this(oid, true, null) { }

        public CobroFomentoViewForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = ChargeInfo.Get(oid, ETipoCobro.Fomento, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            _facturas_todas = LineaFomentoList.GetByCobroList(_entity.Oid);
            _facturas = _facturas_todas.GetSortedList();
            Datos_Facturas.DataSource = _facturas;
            SetComboYears();

            PgMng.Grow();

            UpdateAsignado();

            base.RefreshMainData();
        }

        #endregion

		#region Business Methods

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
            DialogResult = DialogResult.Cancel;
        }

        #endregion


        #region Events

        #endregion 
    }
}