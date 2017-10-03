using System;
using System.Windows.Forms;

using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class InvoiceSelectForm : InvoiceMngForm
    {
        #region Factory Methods

        public InvoiceSelectForm()
            : this(null) {}

        public InvoiceSelectForm(Form parent)
            : this(parent, null, ETipoFacturas.Todas) {}

        public InvoiceSelectForm(Form parent, OutputInvoiceList lista, ETipoFacturas tipo)
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
