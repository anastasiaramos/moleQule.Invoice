using System;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
	public partial class CobrosREAEditForm : CobrosREAUIForm
    {
        #region Attributes & Properties

        public const string ID = "CobrosREAEditForm";
        public static Type Type { get { return typeof(CobrosREAEditForm); } }
        public override Type EntityType { get { return typeof(Charge); } }

        #endregion

        #region Factory Methods

        public CobrosREAEditForm(Form parent)
            : this(null, parent) { }
        
        public CobrosREAEditForm(ChargeInfo cobro, Form parent)
            : base(cobro, parent)
		{
			InitializeComponent();
            _cobro = cobro;
            _resumen = ChargeSummary.Get(ETipoCobro.REA);

            SetFormData();
            this.Text = Resources.Labels.COBRO_REA_EDIT_TITLE;

            _mf_type = ManagerFormType.MFEdit;
        }

        protected override void GetFormSourceData()
        {
            _cobros = ChargeList.GetListREA(true);
        }

        #endregion
    }
}

