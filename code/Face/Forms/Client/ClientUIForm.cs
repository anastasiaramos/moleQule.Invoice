using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cliente;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class ClienteUIForm : ClientForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata de la Cliente actual y que se va a editar.
        /// </summary>
        protected Cliente _entity;

        public override Cliente Entity { get { return _entity; } set { _entity = value; } }
        public override ClienteInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        #endregion

        #region Factory Methods

        public ClienteUIForm()
            : this(-1, null) {}

        public ClienteUIForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
        }

        public ClienteUIForm(Cliente item, Form parent)
            : base(item.Oid, item, parent)
        {
            InitializeComponent();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            Cliente temp = _entity.Clone();
            temp.ApplyEdit();

            // do the save
            try
            {
                _entity = temp.Save();
                _entity.ApplyEdit();

                return true;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

            Datos_ProductoCliente.DataSource = _entity.Productos;
            PgMng.Grow();

            SelectTipoIDAction();
            SelectFormaPagoAction();

            base.RefreshMainData();
        }

        #endregion

        #region Actions

        protected override void CustomAction1() { PrintHistoriaAction(); }

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected override void PrintAction()
        {
            base.PrintAction();
        }

        protected override void PrintHistoriaAction()
        {
            PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
            ClienteReportMng reportMng = new ClienteReportMng(AppContext.ActiveSchema, "Historia de Cliente");

            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
            HistoriaClienteRpt report = reportMng.GetHistoriaClienteRpt(_entity.GetInfo(false));

            PgMng.FillUp();

            ShowReport(report);
        }

        protected override void AplicarAction()
        {
            try
            {
                ValidateInput();
            }
            catch (iQValidationException ex)
            {
                PgMng.ShowInfoException(ex.Message);
                
                _action_result = DialogResult.Ignore;
                return;
            }

            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected override void SelectFormaPagoAction()
		{
			if (FormaPago_CB.SelectedItem == null) return;

			EFormaPago fPago = (EFormaPago)(long)FormaPago_CB.SelectedValue;
			switch (fPago)
			{
				case EFormaPago.Contado:
					DiasPago_NTB.Enabled = false;
					_entity.DiasPago = 0;
					break;
				case EFormaPago.XDiasFechaFactura:
					DiasPago_NTB.Enabled = true;
					break;
				case EFormaPago.XDiasMes:
					DiasPago_NTB.Enabled = true;
					break;
			}
		}

		protected virtual void SelectPrioridadPrecioAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(Library.Store.EnumText<EPrioridadPrecio>.GetList(false));
			
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource tipo = form.Selected as ComboBoxSource;
				_entity.EPrioridadPrecio = (EPrioridadPrecio)tipo.Oid;
				PrioridadPrecio_TB.Text = _entity.PrioridadPrecioLabel;
			}
		}

		protected override void SelectTipoIDAction()
		{
			if (TipoID_CB.SelectedItem == null) return;

			ETipoID tipo = (ETipoID)(long)TipoID_CB.SelectedValue;
			MascaraID_Label.Text = AgenteBase.GetTipoIDMask(tipo);
		}

        protected override void NuevoProductoAction() 
        {
			ProductSelectForm form = new ProductSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ProductInfo item = form.Selected as ProductInfo;

				_entity.Productos.NewItem(_entity, item);

				ControlsMng.UpdateBinding(Datos_ProductoCliente);
			}
        }

        protected override void BorrarProductoAction() 
        {
            if (Datos_ProductoCliente.Current != null)
            {
                if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
                {
                    ProductoCliente producto = (ProductoCliente)Datos_ProductoCliente.Current;
					_entity.Productos.Remove(producto);
                }
            }
        }

		protected override void SelectTipoDescuentoLineaAction() 
		{
			ProductoCliente item = Productos_DGW.CurrentRow.DataBoundItem as ProductoCliente;

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(Library.Common.EnumText<ETipoDescuento>.GetList(false));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource tipo = form.Selected as ComboBoxSource;
				item.ETipoDescuento = (ETipoDescuento)tipo.Oid;

				ControlsMng.UpdateBinding(Datos_ProductoCliente);
			}
		}

        protected override void LoadRegistroEmailsAction()
        {
            if (_entity.Emails == null)
            {
                try
                {
                    PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);

                    PgMng.Grow();

                    _entity.LoadChilds(typeof(LineaRegistro), false);
                    PgMng.Grow();

                    Datos_Emails.DataSource = _entity.Emails;
                    PgMng.Grow();
                }
                finally
                {
                    PgMng.FillUp();
                }
            }
        }

        #endregion

        #region Buttons

		private void Estado_BT_Click(object sender, EventArgs e)
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			EEstado[] list = { EEstado.Active, EEstado.Baja };
			form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.Estado = estado.Oid;
			}
		}

        private void Localidad_BT_Click(object sender, EventArgs e)
        {
            MunicipioSelectForm form = new MunicipioSelectForm(this);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                MunicipioInfo item = (MunicipioInfo)form.Selected;

                if (item == null) return;

                _entity.Poblacion = item.Localidad;
                _entity.CodigoPostal = item.CodPostal;
                _entity.Municipio = item.Nombre;
                _entity.Provincia = item.Provincia;
				//_entity.Pais = item.Pais;
            }
        }

        private void Cuenta_BT_Click(object sender, EventArgs e)
        {
			BankAccountSelectForm form = new BankAccountSelectForm(this, BankAccountList.GetList(ETipoCuenta.CuentaCorriente, EEstado.Active, false));
            
			if (form.ShowDialog(this) == DialogResult.OK)
            {
				BankAccountInfo cuenta = form.Selected as BankAccountInfo;

				_entity.OidCuentaBancariaAsociada = cuenta.Oid;
				_entity.CuentaAsociada = cuenta.Valor;
            }
        }

        private void Impuesto_BT_Click(object sender, EventArgs e)
        {
            ImpuestoSelectForm form = new ImpuestoSelectForm(this);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ImpuestoInfo item = form.Selected as ImpuestoInfo;
                _entity.SetImpuesto(item);
                Impuesto_TB.Text = _entity.Impuesto;
            }
        }

        private void Defecto_BT_Click(object sender, EventArgs e)
        {
            _entity.SetImpuesto(null);
            Impuesto_TB.Text = _entity.Impuesto;
        }
		
		private void NoContabilizar_BT_Click(object sender, EventArgs e)
		{
			CuentaContable_TB.Mask = string.Empty;
			_entity.CuentaContable = string.Empty.PadLeft(Library.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0');
		}

		private void PrioridadPrecio_BT_Click(object sender, EventArgs e) { SelectPrioridadPrecioAction(); }

        #endregion

		#region Events

		private void FormaPago_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (FormaPago_CB.SelectedItem == null) return;
			SelectFormaPagoAction();
		}

		#endregion
	}
}

