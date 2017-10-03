using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class PedidoAddForm : PedidoUIForm
    {
        #region Attributes & Properties
		
        public const string ID = "PedidoAddForm";
		public static Type Type { get { return typeof(PedidoAddForm); } }

		protected override int BarSteps { get { return base.BarSteps + 1; } }

		#endregion
		
        #region Factory Methods

        public PedidoAddForm() 
			: this((Form)null) {}

        public PedidoAddForm(Form parent)
            : base(-1, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        public PedidoAddForm(Pedido source)
            : base()
        {
            InitializeComponent();
            _entity = source.Clone();
            _entity.BeginEdit();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

		protected override void GetFormSourceData()
		{           
			_entity = Pedido.New();
            _entity.BeginEdit();
        }

		#endregion

    }
}
