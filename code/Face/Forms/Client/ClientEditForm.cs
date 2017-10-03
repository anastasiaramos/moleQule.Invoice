using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
	public partial class ClientEditForm : ClienteUIForm
    {
        #region Factory Methods

        public ClientEditForm(long oid, Form parent)
            : base(oid, parent)
		{
			InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFEdit;
        }

        public ClientEditForm(Cliente item, Form parent)
            : base(item, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null && _entity.CloseSessions) _entity.CloseSession();

			base.DisposeForm();
		}

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
            _entity = (Cliente)parameters[0];

            if (_entity == null)
            {
                _entity = Cliente.Get(oid, false);
                _entity.BeginEdit();
            }
        }

        #endregion

		#region Layout

        public override void FormatControls()
        {
            base.FormatControls();

            NCliente_NTB.Enabled = true;
            NCliente_NTB.ReadOnly = false;
            NCliente_NTB.BackColor = System.Drawing.Color.White;
        }

		#endregion 

        #region Actions
        
        protected override void LoadPreciosAction()
        {
            if (_entity.Productos.Count == 0)
            {
                try
                {
                    PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);

                    PgMng.Grow();

                    _entity.LoadChilds(typeof(ProductoCliente), false);
                    PgMng.Grow();

                    Datos_ProductoCliente.DataSource = _entity.Productos;
                    PgMng.Grow();
                }
                finally
                {
                    PgMng.FillUp();
                }
            }
        }

        #endregion
    }
}

