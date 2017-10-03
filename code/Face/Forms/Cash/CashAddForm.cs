using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class CashAddForm : CashUIForm
    {
        #region Attributes & Properties

        public new const string ID = "CashAddForm";
		public new static Type Type { get { return typeof(CashAddForm); } }

		#endregion
		
        #region Factory Methods

        public CashAddForm() 
			: this((Form)null) {}

        public CashAddForm(Form parent)
            : base(-1, true, parent)
        {
            InitializeComponent();
            SetFormData();
            this.Text = Resources.Labels.CAJA_ADD_TITLE;
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData()
        {
            _entity = Cash.New();
            _entity.BeginEdit();
        }

        #endregion
    }
}