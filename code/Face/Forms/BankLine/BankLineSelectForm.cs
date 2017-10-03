using System;
using System.Windows.Forms;

using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class BankLineSelectForm : BankLineMngForm
    {
        #region Factory Methods

        public BankLineSelectForm()
            : this(null) {}

        public BankLineSelectForm(Form parent)
            : this(parent, null) {}
		
		public BankLineSelectForm(Form parent, BankLineList list)
            : base(true, parent, list)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }
		
        #endregion

        #region Actions

        /// <summary>
        /// Accion por defecto. Se usa para el Double_Click del Grid
        /// <returns>void</returns>
        /// </summary>
        protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        #endregion
    }
}