using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class TraspasoEditForm : TraspasoUIForm
    {

        #region Attributes & Properties
		
        public new const string ID = "TraspasoEditForm";
		public new static Type Type { get { return typeof(TraspasoEditForm); } }

		#endregion
		
        #region Factory Methods

        public TraspasoEditForm(long oid)
            : this(oid, null) {}

        public TraspasoEditForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = Traspaso.Get(oid);
            _entity.BeginEdit();
        }

        #endregion

        #region Actions

        #endregion

    }
}
