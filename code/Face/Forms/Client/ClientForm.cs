using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule.Face.Common;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Hipatia;

namespace moleQule.Face.Invoice
{
    public partial class ClientForm : Skin04.ItemMngSkinForm
    {
        #region Attributes & Properties

        public override Type EntityType { get { return typeof(Cliente); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        public virtual Cliente Entity { get { return null; } set { } }
        public virtual ClienteInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public ClientForm()
			: this(null) { }

        public ClientForm(Form parent) 
			: this(-1, parent) { }

        public ClientForm(long oid, Form parent)
            : base(oid, new object[1]{null}, true, parent) 
        {
            InitializeComponent();
        }

        public ClientForm(long oid, Cliente cliente, Form parent)
            : base(oid, new object[1] {cliente}, true, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Authorization

        protected override void ApplyAuthorizationRules()
        {
			CuentaContable_TB.Enabled = Cliente.CanEditCuentaContable();
			CuentaContable_TB.ReadOnly = !Cliente.CanEditCuentaContable();
			NoContabilizar_BT.Enabled = Cliente.CanEditCuentaContable();
        }

        #endregion

        #region Layout

		public override void FitColumns()
		{
			CurrencyManager cm;
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

			cm = (CurrencyManager)BindingContext[Productos_DGW.DataSource];
			cm.SuspendBinding();

			Producto.Tag = 1;

			cols.Add(Producto);

			ControlsMng.MaximizeColumns(Productos_DGW, cols);
		}

		public override void FormatControls()
		{
			//IDE Compatibility
			if (AppContext.User == null) return;

			MaximizeForm(new Size(950, 725));
			base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.IMPRIMIR_HISTORIA, Properties.Resources.item_print);

			NCliente_NTB.TextAlign = HorizontalAlignment.Center;

			CuentaContable_TB.Mask = (EntityInfo.CuentaContable != Library.Invoice.Resources.Defaults.NO_CONTABILIZAR)
										? Library.Invoice.ModuleController.GetCuentasMask()
										: string.Empty;

			PrecioProducto.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
			PrecioCompra.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					ShowAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
					HideAction(molAction.CustomAction4);
					ShowAction(molAction.ShowDocuments);
					break;
			}
		}

        #endregion

		#region Source

		public override void RefreshSecondaryData()
		{
			Datos_FormaPago.DataSource = Library.Common.EnumText<EFormaPago>.GetList();
			PgMng.Grow();

			Datos_MedioPago.DataSource = Library.Common.EnumText<EMedioPago>.GetList();
			PgMng.Grow();

			Datos_TipoID.DataSource = Library.Common.EnumText<ETipoID>.GetList();
			PgMng.Grow();
		}

		#endregion

		#region Print

		public override void PrintObject()
        {
            //ReportMng reportMng = new ReportMng(AppContext.ActiveSchema);
            //ReportViewer.SetReport(reportMng.GetClienteReport(EntityInfo));
            //ReportViewer.ShowDialog();
        }

        #endregion

        #region Actions

        protected virtual void AplicarAction() { throw new iQImplementationException("Aplicar"); }
        protected virtual void PrintHistoriaAction() { throw new iQImplementationException("PrintHistoria"); }

        protected override void PrintAction()
        {
            PrintObject();
        }

        protected override void DocumentsAction()
        {
            try
            {
                AgenteEditForm form = new AgenteEditForm(EntityInfo.TipoEntidad, EntityInfo as IAgenteHipatia, this);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
					AgenteAddForm form = new AgenteAddForm(EntityInfo.TipoEntidad, EntityInfo as IAgenteHipatia, this);
                    form.ShowDialog(this);
                }
            }
        }

        protected virtual void SelectFormaPagoAction() { }
        protected virtual void SelectTipoIDAction() { }
        protected virtual void NuevoProductoAction() { }
        protected virtual void BorrarProductoAction() { }
        protected virtual void LoadPreciosAction() { }
        protected virtual void LoadRegistroEmailsAction() {}

        protected virtual void SelectTipoDescuentoLineaAction() { }

        protected void SendMailAction()
        {
            PgMng.Reset(3, 1, moleQule.Face.Resources.Messages.OPENING_EMAIL_CLIENT, this);

            MailParams mail = new MailParams();

            mail.To = EntityInfo.Email;

            try
            {
                PgMng.Grow();

                EMailSender.MailTo(mail);
                PgMng.Grow();
            }
            catch
            {
                MessageBox.Show(moleQule.Face.Resources.Messages.NO_EMAIL_CLIENT);
            }
            finally
            {
                PgMng.FillUp();
            }
        }

        #endregion

        #region Buttons

        private void SendMail_BT_Click(object sender, EventArgs e)
        {
            SendMailAction();
        }

        private void AddProducto_TI_Click(object sender, EventArgs e)
        {
            NuevoProductoAction();
        }

        private void DeleteProducto_TI_Click(object sender, EventArgs e)
        {
            BorrarProductoAction();
        }

        #endregion

        #region Events

        private void TipoID_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectTipoIDAction();
        }

        private void Productos_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Productos_DGW.Columns[e.ColumnIndex].Name == TipoDescuentoLabel.Name) SelectTipoDescuentoLineaAction();
        }

        private void Pages_TP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Pages_TP.SelectedTab == Precios_TP)
            {
                LoadPreciosAction();
            }
            else if (Pages_TP.SelectedTab == Emails_TP)
            {
                LoadRegistroEmailsAction();
            }
        }

        #endregion
    }
}

