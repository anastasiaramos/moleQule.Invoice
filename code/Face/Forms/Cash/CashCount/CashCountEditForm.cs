using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class CashCountEditForm : CashCountUIForm
    {
        #region Attributes & Properties

        public new const string ID = "CashCountEditForm";
		public new static Type Type { get { return typeof(CashCountEditForm); } }

		#endregion
		
        #region Factory Methods

        public CashCountEditForm(long oid)
            : this(oid, null) { }

        public CashCountEditForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            if (Entity != null)
            {
                SetFormData();
            }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
			if (_caja != null) _caja.CloseSession();
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = CierreCaja.Get(oid);
            _entity.BeginEdit();
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();

            MaximizeForm(new System.Drawing.Size(this.Width, 0));

            Fecha_DTP.Enabled = false;
            CashLine_DGW.ReadOnly = true;
        }

		#endregion

		#region Source

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            CashLine_BS.DataSource = _entity.LineaCajas;
            PgMng.Grow();

            base.RefreshMainData();
        }
        
        #endregion

        #region Actions

        #endregion
    }
}