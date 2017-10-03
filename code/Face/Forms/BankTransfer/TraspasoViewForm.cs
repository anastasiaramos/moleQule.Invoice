using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
    public partial class TraspasoViewForm : TraspasoForm
    {

        #region Attributes & Properties
		
        public new const string ID = "TraspasoViewForm";
		public new static Type Type { get { return typeof(TraspasoViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private TraspasoInfo _entity;

        public override TraspasoInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public TraspasoViewForm(long oid) 
			: this(oid, null) {}

        public TraspasoViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = TraspasoInfo.Get(oid, true);
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
		
        #endregion

        #region Print

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Events

        #endregion
    }
}
