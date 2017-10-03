using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
	public partial class ClientAddForm : ClienteUIForm
    {
        #region Factory Methods

        public ClientAddForm(Form parent) 
			: base(-1, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData(object[] parameters)
		{
			_entity = Cliente.New();
			_entity.BeginEdit();
			_entity.GetNewCode();
		}

		#endregion
	}
}

