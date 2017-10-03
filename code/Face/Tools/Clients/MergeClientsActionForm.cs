using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class MergeClientsActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        public const string ID = "MergeClientsActionForm";
        public static Type Type { get { return typeof(MergeClientsActionForm); } }

        protected long _oid_source = 0;
        protected long _oid_destiny = 0;
        
        #endregion

        #region Factory Methods

        public MergeClientsActionForm() 
            : this(null) {}

        public MergeClientsActionForm(Form parent)
			: base(true, parent) 
		{
            this.InitializeComponent();
            base.SetFormData();
        }

        #endregion

        #region Layout & Source

        #endregion

        #region Business Methods

		private void OpenForm(ETipoEntidad entityType, long oid)
		{ 
			switch (entityType)
			{
				case ETipoEntidad.Cliente:
					{
						ClientEditForm form = new ClientEditForm(oid, this);
						form.ShowDialog();
					} break;

				case ETipoEntidad.Despachante:
					{
						DespachanteEditForm form = new DespachanteEditForm(oid, this);
						form.ShowDialog();
					} break;

				case ETipoEntidad.Empleado:
					{
                        EmployeeEditForm form = new EmployeeEditForm(oid, this);
                        form.ShowDialog();
					} break;

				case ETipoEntidad.Familia:
					{
                        FamiliaEditForm form = new FamiliaEditForm(oid, this);
                        form.ShowDialog();
					} break;

				case ETipoEntidad.Impuesto:
					{
						ImpuestoUIForm form = new ImpuestoUIForm(this);
						form.ShowDialog();
					} break;

				case ETipoEntidad.Naviera:
					{
						NavieraEditForm form = new NavieraEditForm(oid, this);
						form.ShowDialog();
					} break;

				case ETipoEntidad.Acreedor:
				case ETipoEntidad.Proveedor:
					{
						ProveedorEditForm form = new ProveedorEditForm(oid, moleQule.Library.Store.EnumConvert.ToETipoAcreedor(entityType), this);
						form.ShowDialog();
					} break;

				case ETipoEntidad.TipoGasto:
					{
						TipoGastoEditForm form = new TipoGastoEditForm(oid);
						form.ShowDialog();
					} break;

				case ETipoEntidad.TransportistaDestino:
				case ETipoEntidad.TransportistaOrigen:
					{
						TransporterEditForm form = new TransporterEditForm(oid, moleQule.Library.Store.EnumConvert.ToETipoAcreedor(entityType), this);
						form.ShowDialog();
					} break;
                case ETipoEntidad.Prestamo:
                    {
                        LoanEditForm form = new LoanEditForm(oid, this);
                        form.ShowDialog();
                    } break;
			}
		}

        #endregion

        #region Actions

        public void DoSubmit() { SubmitAction(); }

        protected override void SubmitAction()
        {
			PgMng.Reset(3, 1, Resources.Messages.UNIENDO_CLIENTES, this);
            
            try
            {
                if (_oid_source == 0)
                {
                    PgMng.ShowWarningException(Resources.Messages.NO_SOURCE_CLIENT_SELECTED);
                    _action_result = DialogResult.Ignore;

                    return;
                }
                PgMng.Grow();

                if (_oid_destiny == 0)
                {
                    PgMng.ShowWarningException(Resources.Messages.NO_DESTINY_CLIENT_SELECTED);
                    _action_result = DialogResult.Ignore;

                    return;
                }
                PgMng.Grow();

                Cliente.Merge(_oid_source, _oid_destiny);
				PgMng.FillUp();

                _action_result = DialogResult.OK;
            }
            catch (Exception ex)
            {
				PgMng.FillUp();
				PgMng.ShowInfoException(ex);

                _action_result = DialogResult.Ignore;
            }
        }
        
        #endregion

        #region Buttons

        private void Source_BT_Click(object sender, EventArgs e)
        {
            ClientSelectForm form = new ClientSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ClienteInfo cliente = form.Selected as ClienteInfo;
                Source_TB.Text = cliente.Nombre;
                _oid_source = cliente.Oid;
            }
        }

        private void Destiny_BT_Click(object sender, EventArgs e)
        {
            ClientSelectForm form = new ClientSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ClienteInfo cliente = form.Selected as ClienteInfo;
                Destiny_TB.Text = cliente.Nombre;
                _oid_destiny = cliente.Oid;
            }
        }
        
		#endregion
	}
}

