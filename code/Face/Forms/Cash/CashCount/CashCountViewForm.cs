using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class CashCountViewForm : CashCountForm
    {
        #region Attributes & Properties
		
        public new const string ID = "CashCountViewForm";
		public new static Type Type { get { return typeof(CashCountViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private CierreCajaInfo _entity;

        public override CierreCajaInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public CashCountViewForm(long oid) : this(oid, null) {}

        public CashCountViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = CierreCajaInfo.Get(oid, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Layout & Source

        /// <summary>Da formato visual a los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            SetReadOnlyControls(this.Controls);
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;

            base.FormatControls();

            MaximizeForm(new System.Drawing.Size(this.Width, 0));
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            CashLine_BS.DataSource = _entity.LineaCajas;
			
            base.RefreshMainData();
        }

        protected override void SetRowFormat(DataGridViewRow row)
        {
            if (row.IsNewRow) return;
            CashLineInfo item = (CashLineInfo)row.DataBoundItem;
            
            Face.Common.ControlTools.Instance.SetRowColorIM(row, item.EEstado);
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