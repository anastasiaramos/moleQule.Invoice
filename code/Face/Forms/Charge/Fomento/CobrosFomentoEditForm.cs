using System;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
	public partial class CobrosFomentoEditForm : CobrosFomentoUIForm
    {
        #region Attributes & Properties

        public const string ID = "CobrosFomentoEditForm";
        public static Type Type { get { return typeof(CobrosFomentoEditForm); } }
        public override Type EntityType { get { return typeof(Charge); } }

        #endregion

        #region Factory Methods

        public CobrosFomentoEditForm(Form parent)
            : this(null, parent) { }
        
        public CobrosFomentoEditForm(ChargeInfo cobro, Form parent)
            : base(cobro, parent)
		{
			InitializeComponent();
            _cobro = cobro;
            _resumen = ChargeSummary.Get(ETipoCobro.Fomento);

            SetFormData();
            this.Text = Resources.Labels.COBROS_FOMENTO_EDIT_TITLE;

            _mf_type = ManagerFormType.MFEdit;
        }

        protected override void GetFormSourceData()
        {
            _cobros = ChargeList.GetListFomento(true);
        }

        #endregion
    }
}