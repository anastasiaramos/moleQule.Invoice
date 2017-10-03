using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;
using moleQule.Library.Common;

namespace moleQule.Face.Invoice
{
    public partial class TraspasoAddForm : TraspasoUIForm
    {

        #region Attributes & Properties
		
        public new const string ID = "TraspasoAddForm";
		public new static Type Type { get { return typeof(TraspasoAddForm); } }

		#endregion
		
        #region Factory Methods

        public TraspasoAddForm() 
			: this((Form)null) { }

        public TraspasoAddForm(Form parent)
            : base(parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        public TraspasoAddForm(Traspaso source)
            : base()
        {
            InitializeComponent();
            _entity = source.Clone();
            _entity.BeginEdit();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        public TraspasoAddForm(BankAccount source)
            : base()
        {
            InitializeComponent();
            _entity = Traspaso.New();
            _entity.BeginEdit();
            _entity.OidCuentaOrigen = source.OidCuentaAsociada;
            _entity.CuentaOrigen = source.Valor;
            _entity.OidCuentaDestino = source.Oid;
            _entity.CuentaDestino = source.CuentaAsociada;
            _entity.Observaciones = "CANCELACIÓN DE COMERCIO EXTERIOR";
            _entity.ETipoMovimientoBanco = EBankLineType.CancelacionComercioExterior;

            SetFormData();
            _mf_type = ManagerFormType.MFAdd;

        }

        protected override void GetFormSourceData()
        {
            _entity = Traspaso.New();
            _entity.BeginEdit();
        }

        #endregion

        #region Actions

        #endregion
    }
}
