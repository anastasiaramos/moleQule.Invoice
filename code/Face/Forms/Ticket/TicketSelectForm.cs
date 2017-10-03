using System;
using System.Windows.Forms;

using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class TicketSelectForm : TicketMngForm
    {

        #region Factory Methods

        public TicketSelectForm()
            : this(null) {}

        public TicketSelectForm(Form parent)
            : this(parent, null, ETipoFacturas.Todas) {}

        public TicketSelectForm(Form parent, TicketList lista, ETipoFacturas tipo)
            : base(true, parent, lista, tipo)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }
		
        #endregion

        #region Actions

        protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        #endregion
    }
}
