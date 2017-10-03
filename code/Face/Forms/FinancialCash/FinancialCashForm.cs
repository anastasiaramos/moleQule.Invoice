using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class FinancialCashForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "EffectForm";
        public static Type Type { get { return typeof(FinancialCashForm); } }

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public virtual FinancialCash Entity { get { return null; } set { } }
        public virtual FinancialCashInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public FinancialCashForm() : this(-1) {}

        public FinancialCashForm(long oid) : this(oid, true, null) {}

		public FinancialCashForm(bool isModal) : this(-1, isModal, null) {}

        public FinancialCashForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

        #region Actions

        protected virtual void SetCuentaAction() { }
        
        #endregion

        #region Buttons
        
        private void Cuenta_BT_Click(object sender, EventArgs e)
        {
            SetCuentaAction();
        }

        #endregion
    }
}
