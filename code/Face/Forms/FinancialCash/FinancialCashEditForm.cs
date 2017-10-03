using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class FinancialCashEditForm : FinancialCashUIForm
    {
        #region Attributes & Properties

        public new const string ID = "EffectEditForm";
        public new static Type Type { get { return typeof(FinancialCashEditForm); } }

        #endregion

        #region Factory Methods

        public FinancialCashEditForm(FinancialCashInfo item)
            : this(item, null) { }

        public FinancialCashEditForm(FinancialCashInfo item, Form parent)
            : base(item.Oid, true, parent)
        {
            InitializeComponent();
            SetFormData(item);
            this.Text = Resources.Labels.EFFECT_EDIT_TITLE + " " + Entity.Codigo;
            _mf_type = ManagerFormType.MFEdit;
        }

        public override void DisposeForm()
        {
            if (_entity != null) _entity.CloseSession();
        }

        public void SetFormData(FinancialCashInfo source)
        {
            _entity = FinancialCash.Get(source.Oid, false);
            _entity.BeginEdit();
            base.SetFormData();
        }

        protected override void GetFormSourceData(long oid) { }

        #endregion

        #region Layout
        
        #endregion

        #region Actions

        #endregion
    }
}
