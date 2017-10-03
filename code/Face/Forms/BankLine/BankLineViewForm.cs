using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
    public partial class BankLineViewForm : BankLineForm
    {
        #region Attributes & Properties

        public new const string ID = "BankLineViewForm";
		public new static Type Type { get { return typeof(BankLineViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private BankLineInfo _entity;

        public override BankLineInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public BankLineViewForm(BankLineInfo source) 
            : this(source, null) {}

        public BankLineViewForm(BankLineInfo source, Form parent)
            : base(source.Oid, true, parent)
        {
            InitializeComponent();
            SetFormData(source);
            this.Text = ": " + EntityInfo.Codigo;
            _mf_type = ManagerFormType.MFView;
        }

        public void SetFormData(BankLineInfo source)
        {
            _entity = BankLineInfo.Get(source.Oid, source.ETipoMovimientoBanco, source.ETipoTitular);
            base.SetFormData();
        }

        protected override void GetFormSourceData(long oid) {}

        #endregion

        #region Layout & Source

        /// <summary>Da formato visual a los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            Auditado_CkB.Checked = _entity.Auditado;

            base.RefreshMainData();
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