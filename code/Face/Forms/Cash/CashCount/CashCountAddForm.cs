using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class CashCountAddForm : CashCountUIForm
    {
        #region Attributes & Properties

        public new const string ID = "CashCountAddForm";
		public new static Type Type { get { return typeof(CashCountAddForm); } }

		#endregion
		
        #region Factory Methods

        public CashCountAddForm() 
			: this(-1, (Form)null) {}

        public CashCountAddForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        public CashCountAddForm(CierreCaja source)
            : base()
        {
            InitializeComponent();
            _entity = source.Clone();
            _entity.BeginEdit();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = CierreCaja.New();
            _entity.OidCaja = oid;
            _entity.BeginEdit();
        }

		public override void DisposeForm()
		{
			if (_caja != null) _caja.CloseSession();
		}

        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();
        }

		#endregion

		#region Source
		
		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            _caja = Cash.Get(_entity.OidCaja); 
			PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion

        #region Actions

        protected override void SaveAction()
        {
            try
            {
                _entity.SetLineasCierre(_caja, Fecha_DTP.Value);
            }
            catch (Exception ex)
            {
				PgMng.ShowInfoException(ex);

                _action_result = DialogResult.Ignore;
                return;
            }

            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected override void SetCajaAction()
		{
			CashList list = CashList.GetList(false);
			CashSelectForm form = new CashSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				CashInfo caja = form.Selected as CashInfo;

				if (_caja != null) _caja.CloseSession();

                _caja = Cash.Get(caja.Oid, false);

				UpdateSaldo();
			}
		}

        #endregion
    }
}