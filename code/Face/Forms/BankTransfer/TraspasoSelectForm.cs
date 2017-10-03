using System;
using System.Windows.Forms;

using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class TraspasoSelectForm : TraspasoMngForm
    {

        #region Factory Methods

        public TraspasoSelectForm()
            : this(null) {}

        public TraspasoSelectForm(Form parent)
            : this(parent, null) {}
		
		public TraspasoSelectForm(Form parent, TraspasoList list)
            : base(true, parent, list)
        {
            InitializeComponent();
			
			SetView(molView.Select);
			
            DialogResult = DialogResult.Cancel;
        }
		
        #endregion

        #region Layout & Source

        /// <summary>Formatea los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            SetSelectView();
            base.FormatControls();
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
