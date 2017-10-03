using System;
using System.Windows.Forms;

using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class CashCountSelectForm : CashCountMngForm
    {
        #region Factory Methods

        public CashCountSelectForm()
            : this(null) {}

        public CashCountSelectForm(Form parent)
            : this(parent, null) {}
		
		public CashCountSelectForm(Form parent, CierreCajaList list)
            : base(true, parent, list, 0)
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