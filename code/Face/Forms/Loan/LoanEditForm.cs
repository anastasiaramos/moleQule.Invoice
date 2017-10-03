using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class LoanEditForm : LoanUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "PrestamoEditForm";
		public new static Type Type { get { return typeof(LoanEditForm); } }

		#endregion
		
        #region Factory Methods

		public LoanEditForm(long oid, Form parent)
            : base(oid, parent)
		{
			InitializeComponent();
            if (_entity != null) SetFormData();            
			_mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) 
			{
				_entity.CloseSession();
				//_entity = null;
			}
		}
		
        protected override void GetFormSourceData(long oid)
        {
            _entity = Loan.Get(oid, true);
            _entity.BeginEdit();

            _entity.LoadChilds(typeof(Payment), false); 
        }

        #endregion
    }
}