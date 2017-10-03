using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
	public partial class CobroEditForm : CobroUIForm
    {
        #region Factory Methods

        public CobroEditForm(long oid_cliente, ChargeSummary resumen, Form parent)
            : this(oid_cliente, resumen, null, parent) { }

        public CobroEditForm(long oid_cliente, ChargeSummary resumen, ChargeInfo cobro, Form parent)
            : base(oid_cliente, parent)
		{
			InitializeComponent();
            _resumen = resumen;
            _cobro = cobro;
            if (Entity != null)
            {
                SetFormData();
                this.Text = Resources.Labels.COBRO_EDIT_TITLE + " " + Entity.Nombre.ToUpper();
            }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}
		
		protected override void GetFormSourceData(long oid)
		{
			_entity = Cliente.Get(oid, false);
            _entity.LoadChilds(typeof(Charges), true);

			_entity.CloseSessions = false;
			_entity.BeginEdit();
		}

        #endregion
    }
}

