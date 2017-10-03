using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class CashEditForm : CashUIForm
    {
        #region Attributes & Properties

        public new const string ID = "CashEditForm";
		public new static Type Type { get { return typeof(CashEditForm); } }

		#endregion
		
        #region Factory Methods

        public CashEditForm(long oid)
            : this(oid, null) {}

        public CashEditForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();

			if (_entity != null)
			{
				SetFormData();
				Text = Resources.Labels.CAJA_EDIT_TITLE + _entity.Nombre;
			}

			_mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = Cash.Get(oid);
            _entity.BeginEdit();
        }

        #endregion
    }
}
