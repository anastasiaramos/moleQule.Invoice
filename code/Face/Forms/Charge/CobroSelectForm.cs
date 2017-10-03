using System;
using System.Windows.Forms;

namespace moleQule.Face.Invoice
{
    public partial class CobroSelectForm : ClientChargeMngForm
    {

        #region Factory Methods

        public CobroSelectForm(Form parent)
            : base(parent)
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
