using System;
using System.Windows.Forms;

using moleQule.Library.Common;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class ClientSelectForm : ClientMngForm
    {
        #region Factory Methods

        public ClientSelectForm(Form parent)
            : this(parent, null) {}

		public ClientSelectForm(Form parent, EEstado estado)
			: this(parent, estado, null) {}

        public ClientSelectForm(Form parent, ClienteList lista)
            : this(parent, EEstado.Todos, lista) {}

		protected ClientSelectForm(Form parent, EEstado estado, ClienteList lista)
			: base(true, parent, estado, lista) 
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

