using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class BankLineEditForm : BankLineUIForm
    {
        #region Attributes & Properties

        public new const string ID = "BankLineEditForm";
		public new static Type Type { get { return typeof(BankLineEditForm); } }

		#endregion
		
        #region Factory Methods

        public BankLineEditForm(BankLineInfo item)
            : this(item, null) { }

        public BankLineEditForm(BankLineInfo item, Form parent)
            : base(item.Oid, true, parent)
        {
            InitializeComponent();
            SetFormData(item);
            this.Text += ": " + Entity.Codigo;
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

        public void SetFormData(BankLineInfo source)
        {
            _entity = BankLine.Get(source.Oid, source.ETipoMovimientoBanco, source.ETipoTitular);
            _entity.BeginEdit();
            base.SetFormData();
        }

        protected override void GetFormSourceData(long oid) {}

        #endregion

        #region Layout

        public override void FormatControls()
        {
            Importe_TB.Enabled = _entity.ETipo == ETipoApunteBancario.Sencillo;
            Fecha_DTP.Enabled = _entity.ETipo == ETipoApunteBancario.Sencillo;
            Creacion_DTP.Enabled =_entity.ETipo == ETipoApunteBancario.Sencillo;
            Estado_BT.Enabled = _entity.ETipo == ETipoApunteBancario.Sencillo;
            Cuenta_BT.Enabled = _entity.ETipo == ETipoApunteBancario.Sencillo;

            base.FormatControls();
        }

        #endregion

        #region Actions

        #endregion
    }
}
