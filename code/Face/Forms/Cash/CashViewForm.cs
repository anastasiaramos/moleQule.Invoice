using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class CashViewForm : CashForm
    {
        #region Attributes & Properties

        public new const string ID = "CashViewForm";
		public new static Type Type { get { return typeof(CashViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private CashInfo _entity;

        public override CashInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar
        /// </summary>
        private CashViewForm() 
			: this(-1, null) { }

        public CashViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            SetFormData();
            this.Text = Resources.Labels.CAJA_DETAIL_TITLE;// +" " + EntityInfo.Nombre.ToUpper();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = CashInfo.Get(oid, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            SetReadOnlyControls(this.Controls);
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
            base.FormatControls();
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();
			
			Lines_BS.DataSource = CashLineList.SortList(_entity.Lines, "Fecha", ListSortDirection.Ascending);
            PgMng.Grow();			
			
            base.RefreshMainData();
        }
		
        protected override void SetUnlinkedGridValues(string gridName)
        {
        }
		
        #endregion

        #region Validation & Format

        /// <summary>
        /// Asigna formato deseado a los controles del objeto cuando éste es modificado
        /// </summary>
        protected override void FormatData()
        {
        }

        #endregion

        #region Print

        #endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Events

        #endregion
    }
}