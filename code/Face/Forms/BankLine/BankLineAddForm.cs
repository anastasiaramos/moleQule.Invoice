using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Common;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class BankLineAddForm : BankLineUIForm
    {
        #region Attributes & Properties

        public new const string ID = "BankLineAddForm";
		public new static Type Type { get { return typeof(BankLineAddForm); } }

		#endregion
		
        #region Factory Methods

        public BankLineAddForm(Form parent)
			: this((BankAccountInfo)null, parent) {}

		public BankLineAddForm(BankAccountInfo cuenta, Form parent) 
			: this(new object[1] { cuenta }, parent) {}

        public BankLineAddForm(object[] parameters, Form parent)
			: base(-1, parameters, true, parent)
		{
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData(object [] parameters)
		{
            _entity = BankLine.New();

            if (parameters[0] != null)
            {
                BankAccountInfo cuenta = parameters[0] as BankAccountInfo;
				SetCuentaBancaria(cuenta);
            }
			_entity.BeginEdit();
		}

        #endregion
    }
}
