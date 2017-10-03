using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class LoanAddForm : LoanUIForm
    {
        #region Attributes & Properties

        public new const string ID = "LoanAddForm";
		public new static Type Type { get { return typeof(LoanAddForm); } }

		#endregion
		
        #region Factory Methods

		public LoanAddForm(Form parent)
            : this((Loan)null, parent) { }

        public LoanAddForm(Loan entity, Form parent) 
			: this(new object[1] { entity }, parent) {}

		public LoanAddForm(object[] parameters, Form parent)
			: base(-1, parameters, true, parent)
		{
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFAdd;
		}		

        protected override void GetFormSourceData(object[] parameters)
        {
			if (parameters[0] == null)
			{
                _entity = Loan.New();
				_entity.BeginEdit();
			}
			else
			{
                _entity = (Loan)parameters[0];
				_entity.BeginEdit();
			}
        }

        #endregion
    }
}