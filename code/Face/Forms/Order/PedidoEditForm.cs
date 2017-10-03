using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class PedidoEditForm : PedidoUIForm
    {
        #region Attributes & Properties
		
        public const string ID = "PedidoEditForm";
		public static Type Type { get { return typeof(PedidoEditForm); } }

		protected override int BarSteps { get { return base.BarSteps + 2; } }

		#endregion
		
        #region Factory Methods

        public PedidoEditForm(long oid)
            : this(oid, null) { }

		public PedidoEditForm(long oid, Form parent)
			: base(oid, true, parent)
        {
            InitializeComponent();

            if (Entity != null)
                SetFormData();

            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();

			base.DisposeForm();
		}

		protected override void GetFormSourceData(long oid)
		{
            _entity = Pedido.Get(oid);
            _entity.BeginEdit();
        }

        #endregion

    }
}
