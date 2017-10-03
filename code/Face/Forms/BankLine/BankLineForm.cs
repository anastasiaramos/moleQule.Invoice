using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class BankLineForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "BankLineForm";
		public static Type Type { get { return typeof(BankLineForm); } }

        protected override int BarSteps { get { return base.BarSteps + 1; } }
		
        public virtual BankLine Entity { get { return null; } set { } }
        public virtual BankLineInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public BankLineForm() : this(-1) {}

        public BankLineForm(long oid) : this(oid, true, null) {}

		public BankLineForm(bool isModal) : this(-1, isModal, null) {}

        public BankLineForm(long oid, bool isModal, Form parent)
            : this(oid, null, isModal, parent) { }

        public BankLineForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}
		
        #endregion

        #region Source

        public override void RefreshSecondaryData()
		{
            Datos_Usuarios.DataSource = new HComboBoxSourceList(UserList.GetList());			
            PgMng.Grow();
        }
		
		#endregion				

        #region Actions

        protected virtual void SelectTipoCuentaAction() { }

        #endregion

        #region Buttons

        private void TipoCuenta_BT_Click(object sender, EventArgs e)
        {
            SelectTipoCuentaAction();
        }

        #endregion
    }
}
