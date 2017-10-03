using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class InvoiceUIForm : InvoiceForm, IBackGroundLauncher
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 6; } }

        protected OutputInvoice _entity = null;
        protected AlbaranFacturas _albaranes_factura = null;
        protected OutputInvoiceLines _conceptos_factura = null;

		protected List<OutputDeliveryInfo> _out_deliveries = new List<OutputDeliveryInfo>();
		protected List<OutputDeliveryInfo> _results = new List<OutputDeliveryInfo>();

        public override OutputInvoice Entity { get { return _entity; } set { _entity = value; } }
        public override OutputInvoiceInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        public ClienteInfo Cliente { get { return (Client_BS.Current != null) ? Client_BS.Current as ClienteInfo : null; } }

        #endregion

        #region Factory Methods

		public InvoiceUIForm()
			: this((Form)null) {}

        public InvoiceUIForm(Form parent)
            : this(-1, parent) {}

        public InvoiceUIForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

        public InvoiceUIForm(OutputInvoice invoice)
            : base(null)
        {
            InitializeComponent();
            _entity = invoice.Clone();
            _entity.BeginEdit();
            SetFormData();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            // do the save
            try
            {
				if (_entity.AlbaranContado) PgMng.Message = Resources.Messages.COMPACTANDO_ALBARANES;

                OutputInvoice temp = _entity.Clone();
                temp.ApplyEdit();
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

		#region Cache

		protected override void CleanCache()
		{
			//Por posibles fallos en medio no llega a limpiar la cache, aqui eliminamos la sesion y la cache
			Expedients exps = Cache.Instance.Get(typeof(Expedients)) as Expedients;
			if ((exps != null) && (exps.Session() != null)) exps.CloseSession();

			Cache.Instance.Remove(typeof(Expedients));
            Cache.Instance.Remove(typeof(BatchList));
			Cache.Instance.Remove(typeof(ProductList));
			Cache.Instance.Remove(typeof(ClienteList));
		}
		
		#endregion

		#region Layout

		public override void FormatControls()
        {
			//IDE Compatibility
			if (_entity == null) return;

            base.FormatControls();

			Serie_BT.Enabled = (_entity != null) ? (_entity.Conceptos.Count == 0) : true;
			DiasPago_NTB.Enabled = (_entity != null) ? (_entity.EFormaPago != EFormaPago.Contado) : true;

            //IDE Compatibility
            try
            {
                if (!_entity.Rectificativa)
                {
                    if (!_entity.AlbaranContado)
                        NFactura_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask();
                    else
                        NFactura_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask() + "-\\C";
                }
                else
                    NFactura_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask() + "-R";
            }
            catch { }
        }

		protected override void HideComponents()
		{
			OutputInvoiceLine concepto;

			foreach (DataGridViewRow row in Lines_DGW.Rows)
			{
                concepto = (row.DataBoundItem as OutputInvoiceLine);

				if (concepto.IsKitComponent)
					row.Visible = false;

				row.ReadOnly = (concepto.OidPartida != 0);	
			}
		}

		#endregion

		#region Source
		
		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            _albaranes_factura = _entity.AlbaranFacturas.Clone();
			_conceptos_factura = _entity.Conceptos.Clone();

			Lines_BS.DataSource = _entity.Conceptos;
            PgMng.Grow();

            _out_deliveries = OutputDeliveryList.GetListByFactura(true, _entity.Oid).GetListInfo();
			PgMng.Grow();

            Fecha_DTP.Value = _entity.Fecha;

            base.RefreshMainData();
        }

		public override void RefreshSecondaryData()
		{
			base.RefreshSecondaryData();

			if (_entity.OidCliente != 0) SetClient(ClienteInfo.Get(_entity.OidCliente, false));
			PgMng.Grow();

			if (_entity.OidSerie != 0) SetSerie(SerieInfo.Get(_entity.OidSerie, false), false);
			PgMng.Grow();

			if (_entity.OidTransportista > 0) SetTransporter(TransporterInfo.Get(_entity.OidTransportista, ETipoAcreedor.TransportistaDestino, false));
			PgMng.Grow();
		}

        #endregion

        #region Validation & Format

        #endregion

		#region Business Methods

		private bool AddAlbaran()
		{
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return false;
			}

			OutputDeliveryList list = null;

			if (!_entity.AlbaranContado)
			{
				if (_entity.OidCliente != 0)
					list = OutputDeliveryList.GetNoFacturados(true, _entity.OidCliente, _entity.OidSerie, (_entity.Rectificativa ? ETipoFactura.Rectificativa : ETipoFactura.Ordinaria));
				else
					list = OutputDeliveryList.GetNoFacturados(true, 0, _entity.OidSerie, (_entity.Rectificativa ? ETipoFactura.Rectificativa : ETipoFactura.Ordinaria));
			}
			else
				list = OutputDeliveryList.GetNoFacturadosAgrupados(_entity.OidSerie, true);

			//Quitamos de la lista los ya añadidos
			List<OutputDeliveryInfo> lista = new List<OutputDeliveryInfo>();
			foreach (OutputDeliveryInfo item in list)
				if (_entity.AlbaranFacturas.GetItemByAlbaran(item.Oid) == null)
					lista.Add(item);

			OutputDeliveryList lista_completa = OutputDeliveryList.GetListByCliente(true, _entity.OidCliente);
			//Añadimos a lista los eliminados
			foreach (AlbaranFactura item in _albaranes_factura)
				if (_entity.AlbaranFacturas.GetItemByAlbaran(item.OidAlbaran) == null)
					lista.Add(lista_completa.GetItem(item.OidAlbaran));

			DeliverySelectForm form = new DeliverySelectForm(this, ETipoEntidad.Cliente, OutputDeliveryList.GetList(lista));
			form.ShowDialog(this);
			if (form.DialogResult == DialogResult.OK)
			{
				_results = form.Selected as List<OutputDeliveryInfo>;

				if (_entity.Rectificativa && (_results.Count > 1))
				{
					PgMng.ShowInfoException("No es posible asignar varios albaranes a una factura rectificativa.");
					return false;
				}

				foreach (OutputDeliveryInfo item in _results)
				{
					if (item.OidHolder != _results[0].OidHolder)
					{
						PgMng.ShowInfoException("No es posible asignar albaranes de clientes distintos a una misma Factura.");
						return false;
					}
				}

				_back_job = BackJob.AddAlbaran;
				//PgMng.StartBackJob(this);

				DoAddAlbaran(null);

				if (Result == BGResult.OK)
				{
					Serie_BT.Enabled = false;
					Datos.ResetBindings(false);
				}
			}

			if ((_entity.AlbaranContado) && (_entity.Conceptos.Count < 0)) Agrupada_CkB.Enabled = false;

			return false;
		}

		protected void SetClient(ClienteInfo client)
		{
			Client_BS.DataSource = client;

            if (_entity.OidCliente != client.Oid)
                _entity.CopyFrom(client);

			SetBankAccount(client);
		}

		protected void SetBankAccount(ClienteInfo client)
		{
			if (client != null)
			{
				if (_entity.EMedioPago == EMedioPago.Giro)
					_entity.CuentaBancaria = client.CuentaBancaria;
				else if (client.CuentaAsociada != string.Empty)
					_entity.CuentaBancaria = client.CuentaAsociada;
				else
					_entity.CuentaBancaria = _company.CuentaBancaria;
			}
		}

		protected void SetSerie(SerieInfo source, bool newCode)
		{
			if (source == null) return;

			_serie = source;

			_entity.OidSerie = _serie.Oid;
			_entity.NumeroSerie = _serie.Identificador;
			_entity.Serie = _serie.Nombre;
			Serie_TB.Text = _entity.NSerieSerie;
			Nota_TB.Text = _serie.Cabecera;

			if (newCode) _entity.GetNewCode();

			/*Cache.Instance.Remove(typeof(ProductList));

			ProductList.GetListBySerie(_serie.Oid, false, true);*/
		}

		protected void SetTransporter(TransporterInfo source)
		{
			if (source == null) return;

			_transporter = source;

			_entity.OidTransportista = _transporter.Oid;
			Transportista_TB.Text = _transporter.Nombre;
		}

		#endregion

        #region Actions

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		public override void PrintObject()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;

            if (_action_result == DialogResult.OK)
            {
                base.PrintObject();

                _entity.SessionCode = OutputInvoice.OpenSession();
                _entity.BeginEdit();
                _entity.BeginTransaction();

                _entity.EEstado = EEstado.Emitido;

                _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
                DialogResult = _action_result;

                ExecuteAction(molAction.Close);
            }
        }

		protected override void CustomAction1() { AddDeliveryAction(); }

		protected virtual void SelectChargeStatusAction()
		{
			SelectEnumInputForm form = new SelectEnumInputForm(true);

			EEstado[] list = { EEstado.DudosoCobro, EEstado.Pendiente };
			form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;
				_entity.UpdateEstadoCobro((EEstado)estado.Oid);
				
				EstadoCobro_TB.Text = _entity.EstadoCobroLabel;				
			}
		}

		protected virtual void SelectSerieAction()
		{
			SerieSelectForm form = new SerieSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetSerie(form.Selected as SerieInfo, true);
			}
		}

		protected virtual void SelectTransporterAction()
		{
			TransporterSelectForm form = new TransporterSelectForm(this, EEstado.Active);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetTransporter(form.Selected as TransporterInfo);
			}
		}

		protected virtual void SelectOwnerAction()
		{
			UserList list = UserList.GetList(AppContext.ActiveSchema, false);

			UserSelectForm form = new UserSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				UserInfo user = form.Selected as UserInfo;
				_entity.OidUsuario = user.Oid;
				_entity.Usuario = user.Name;
				Usuario_TB.Text = _entity.Usuario;
			}
		}

        protected override void EditAccountAction()
        {
			BankAccountSelectForm form = new BankAccountSelectForm(this, BankAccountList.GetList(ETipoCuenta.CuentaCorriente, EEstado.Active, false));

            if (form.ShowDialog(this) == DialogResult.OK)
            {
				BankAccountInfo cuenta = form.Selected as BankAccountInfo;
                Cuenta_TB.Text = cuenta.Valor;
				_entity.CuentaBancaria = cuenta.Valor;
            }            
        }

        protected override void EditClientAction()
        {
            //if (_entity.AlbaranFacturas.Count > 0)
            //{
            //    PgMng.ShowInfoException(Resources.Messages.FACTURA_CON_ALBARANES);
            //    return;
            //}

            ClientSelectForm form = new ClientSelectForm(this, EEstado.Active);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                ClienteInfo cliente = form.Selected as ClienteInfo;
                SetClient(cliente);

				CleanError(IDCliente_TB);
            }
        }

        protected override void DeleteDeliveryAction()
        {
            if (Entity.AlbaranFacturas.Count == 0) return;

			DeliverySelectForm form = new DeliverySelectForm(this, ETipoEntidad.Cliente, OutputDeliveryList.GetList(_out_deliveries));
            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
				List<OutputDeliveryInfo> results = form.Selected as List<OutputDeliveryInfo>;

				foreach (OutputDeliveryInfo item in results)
                {
                    _entity.Extract(item);
                    _out_deliveries.Remove(item);
                }
            }

            if (_entity.AlbaranFacturas.Count == 0)
            {
				Serie_BT.Enabled = true;
            }

            Lines_BS.ResetBindings(false);
        }

        protected override void AddDeliveryAction()
        {
			_entity.AlbaranContado = Agrupada_CkB.Checked;

            AddAlbaran();

            if (Result == BGResult.OK)
                Lines_BS.ResetBindings(false);
        }

		protected virtual void AmendmentInvoiceAction() {}

		protected override void SelectLineTaxAction()
		{
			if (Lines_DGW.CurrentRow == null) return;
			if (Lines_DGW.CurrentRow.DataBoundItem == null) return;

            OutputInvoiceLine item = Lines_DGW.CurrentRow.DataBoundItem as OutputInvoiceLine;

			ImpuestoSelectForm form = new ImpuestoSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo source = (ImpuestoInfo)form.Selected;

				item.OidImpuesto = source.Oid;
				item.PImpuestos = source.Porcentaje;

				_entity.CalculateTotal();
			}
		}

		protected override void UpdateInvoiceAction()
		{
			try
			{
                OutputInvoiceLine item = Lines_DGW.CurrentRow.DataBoundItem as OutputInvoiceLine;
                BatchInfo batch = BatchInfo.Get(item.OidPartida, false, true);
                ProductInfo producto = ProductInfo.Get(item.OidProducto, false, false);

                item.AjustaCantidad(producto, batch);
                _entity.CalculateTotal();

                ControlsMng.UpdateBinding(Lines_BS);
			}
			catch { }
		}

        #endregion

        #region Buttons

		private void Serie_BT_Click(object sender, EventArgs e) { SelectSerieAction(); }
		private void Usuario_BT_Click(object sender, EventArgs e) { SelectOwnerAction(); }
		private void Transportista_BT_Click(object sender, EventArgs e) { SelectTransporterAction(); }
		private void EstadoCobro_BT_Click(object sender, EventArgs e) { SelectChargeStatusAction(); }
		private void SubmitPrint_BT_Click(object sender, EventArgs e) { PrintAction(); }

        #endregion

        #region IBackGroundLauncher

        new enum BackJob { GetFormData, AddAlbaran }
        new BackJob _back_job = BackJob.GetFormData;

        /// <summary>
        /// La llama el backgroundworker para ejecutar codigo en segundo plano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public new void BackGroundJob(BackgroundWorker bk)
        {
            switch (_back_job)
            {
                case BackJob.AddAlbaran:
                    DoAddAlbaran(bk);
                    break;

                default:
                    base.BackGroundJob(bk);
                    return;
            }

            if (Result == BGResult.OK)
            {
				Serie_BT.Enabled = false;
            }
        }

        protected void DoAddAlbaran(BackgroundWorker bk)
        {
			Datos.RaiseListChangedEvents = false;
			Lines_BS.RaiseListChangedEvents = false;

            try
            {
                PgMng.Reset(_results.Count + 1, 1, Resources.Messages.IMPORTANDO_ALBARANES, this);

				//Asignamos el cliente
				if (_entity.OidCliente == 0)
				{
					_entity.CopyFrom(_results[0]);
					SetClient(ClienteInfo.Get(_results[0].OidHolder, false, true));
				}

				foreach (OutputDeliveryInfo item in _results)
                {
                    _entity.Insert(item);
                    _out_deliveries.Add(item);
                    PgMng.Grow(string.Empty, "Insertar el Albarán");
                }

                Result = BGResult.OK;
            }
            catch (Exception ex)
            {
				CleanCache();
                throw ex;				
            }
            finally
            {
                PgMng.FillUp();

				Datos.RaiseListChangedEvents = true;
				Lines_BS.RaiseListChangedEvents = true;
#if TRACE
                PgMng.ShowCronos();
#endif
            }
        }
        
        #endregion

        #region Events

        private void FacturaUIForm_Shown(object sender, EventArgs e)
        {
            Conceptos_SC.Refresh();
            Application.DoEvents();
        }

		private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
		{
			_entity.Fecha = Fecha_DTP.Value;
			Prevision_TB.Text = _entity.Prevision.ToShortDateString();
		}

		private void Rectificativa_CkB_CheckedChanged(object sender, EventArgs e) { AmendmentInvoiceAction(); }

		private void PIRPF_NTB_TextChanged(object sender, EventArgs e)
		{
			_entity.PIRPF = PIRPF_NTB.DecimalValue;
			_entity.CalculateTotal();
			Datos.ResetBindings(false);
		}

        private void MedioPago_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MedioPago_CB.SelectedValue == null) return;
            SetBankAccount(Cliente);
        }

        private void FormaPago_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormaPago_CB.SelectedValue == null) return;

			FormaPago_CB.SuspendLayout();
			_entity.EFormaPago = (EFormaPago)(long)FormaPago_CB.SelectedValue;

			_entity.Prevision = Library.Common.EnumFunctions.GetPrevisionPago(_entity.EFormaPago, _entity.Fecha, _entity.DiasPago);
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();
			DiasPago_NTB.Enabled = (_entity.EFormaPago != EFormaPago.Contado);
			FormaPago_CB.ResumeLayout();
        }

        private void DiasPago_NTB_TextChanged(object sender, EventArgs e)
        {
            try { _entity.DiasPago = DiasPago_NTB.LongValue; }
            catch { _entity.DiasPago = DiasPago_NTB.LongValue; }

			_entity.Prevision = Library.Common.EnumFunctions.GetPrevisionPago(_entity.EFormaPago, _entity.Fecha, _entity.DiasPago);
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();
        }

        private void Datos_Concepto_ListChanged(object sender, ListChangedEventArgs e)
        {
            Datos.ResetBindings(false);
            HideComponents();
        }

		private void Prevision_TB_TextChanged(object sender, EventArgs e)
		{
			Datos.ResetBindings(false);
		}

        #endregion
    }
}

