using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class DeliveryUIForm : DeliveryForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata de la Albaran actual y que se va a editar.
        /// </summary>
		protected OutputDelivery _entity = null;

		protected PedidoList _pedidos = PedidoList.NewList();
		protected PedidoList _pedidos_cliente = null;
		protected StoreInfo _almacen = null;
        protected ExpedientInfo _expediente = null;
		protected WorkReportInfo _work_report = null;
        protected TransporterInfo _transportista = null;
        protected SerieInfo _serie = null;
        protected ClienteInfo _cliente = null;
		protected List<PedidoInfo> _results = new List<PedidoInfo>();

		public override OutputDelivery Entity { get { return _entity; } set { _entity = value; } }
        public override OutputDeliveryInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        protected OutputDeliveryLine Concepto { get { return Lines_BS.Current != null ? Lines_BS.Current as OutputDeliveryLine : null; } }

		#endregion

        #region Factory Methods

        public DeliveryUIForm() 
			: this(-1, null) {}

		public DeliveryUIForm(long oid, Form parent)
			: this(oid, null, true, parent) { }

		public DeliveryUIForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

		protected override bool SaveObject()
		{
			this.Datos.RaiseListChangedEvents = false;

			// do the save
			try
			{
				PgMng.Reset(6, 1, Library.Store.Resources.Messages.ACTUALIZANDO_STOCKS, this);

				OutputDelivery temp = _entity.Clone();
				temp.ApplyEdit();
				PgMng.Grow();

				_entity = temp.Save();
				_entity.ApplyEdit();
				PgMng.Grow();

				return true;
			}
			catch (Exception ex)
			{
				CleanCache();
				PgMng.ShowInfoException(ex);
				return false;
			}
			finally
			{
				this.Datos.RaiseListChangedEvents = true;
				PgMng.FillUp();
			}
		}

		#endregion

		#region Cache

		protected HashOidList _oidAlmacenes = new HashOidList();
		protected HashOidList _oidExpedientes = new HashOidList();
		protected HashOidList _oidProductos = new HashOidList();
		protected HashOidList _oidPartidas = new HashOidList();
		protected HashOidList _oidPedidos = new HashOidList();

		protected void AddCacheItem()
		{
			if (Lines_BS.Current == null) return;
            AddCacheItem((OutputDeliveryLine)Lines_BS.Current);
		}
		protected void AddCacheItem(StoreInfo item)
		{
			if (item == null) return;
			_oidAlmacenes.Add(item.Oid);
		}
		protected void AddCacheItem(ExpedientInfo item)
		{
			if (item == null) return;
			_oidExpedientes.Add(item.Oid);
		}
        protected void AddCacheItem(OutputDeliveryLine item)
		{
			if (item == null) return;

			_oidAlmacenes.Add(item.OidAlmacen);
			_oidExpedientes.Add(item.OidExpediente);
			_oidPartidas.Add(item.OidPartida);
			_oidProductos.Add(item.OidProducto);
			_oidPedidos.Add(item.OidPedido);
		}

		protected override void BuildCache()
		{
            foreach (OutputDeliveryLine ca in _entity.Conceptos)
				AddCacheItem(ca);
		}

		protected override void CleanCache()
		{
			Cache.Instance.Remove(typeof(Stores));
			Cache.Instance.Remove(typeof(Expedients));
            Cache.Instance.Remove(typeof(ExpedienteList));
			Cache.Instance.Remove(typeof(ProductList));
			Cache.Instance.Remove(typeof(BatchList));
			Cache.Instance.Remove(typeof(ClienteList));
		}

		#endregion

		#region Layout

        public override void FormatControls()
        {
			Action3_TI.Enabled = (_entity.EEstado == EEstado.Abierto);
			Action4_TI.Enabled = (_entity.EEstado == EEstado.Abierto);
			Fecha_DTP.Checked = true;
			if (_entity != null) Serie_BT.Enabled = (_entity.Conceptos.Count == 0);
            
			base.FormatControls();

            if (!_entity.Rectificativo)
            {
                if (!_entity.Contado)
                    NAlbaran_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask();
                else
                    NAlbaran_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask() + "-\\C";
            }
            else
                NAlbaran_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask() + "-R";

			WorkReport_BT.Enabled = (_work_report == null);
        }

		protected override void HideComponentes()
		{
			foreach (DataGridViewRow row in Lineas_DGW.Rows)
                if ((row.DataBoundItem as OutputDeliveryLine).IsKitComponent)
					row.Visible = false;
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
			Lines_BS.DataSource = _entity.Conceptos;
            PgMng.Grow();

            DiasPago_NTB.Text = _entity.DiasPago.ToString();
            Fecha_DTP.Value = _entity.Fecha;

            base.RefreshMainData();
        }

		public override void RefreshSecondaryData()
		{
			if (_entity.OidSerie != 0) SetSerie(SerieInfo.Get(_entity.OidSerie, false), false);
			PgMng.Grow();

			if (_entity.OidAlmacen != 0) SetAlmacen(StoreInfo.Get(_entity.OidAlmacen, false));
			PgMng.Grow();

			switch (_entity.EHolderType)
			{
				case ETipoEntidad.WorkReport:
					{
						if (_work_report == null) _work_report = WorkReportInfo.GetByResource(_entity.Oid, ETipoEntidad.OutputDelivery, false);
						SetWorkReport(_work_report);
					}
					break;

				case ETipoEntidad.Cliente:
					{
						if (_entity.OidHolder != 0) SetCliente(ClienteInfo.Get(_entity.OidHolder, false));
						PgMng.Grow();

						if (_entity.OidExpediente != 0) SetExpediente(ExpedientInfo.Get(_entity.OidExpediente, false));
						PgMng.Grow();
					}
					break;
			}

            base.RefreshSecondaryData();
		}

        #endregion

		#region Business Methods

		private void DeleteKit(BatchInfo partida)
		{
            OutputDeliveryLine concepto;

			foreach (BatchInfo item in partida.Componentes)
			{
				concepto = _entity.Conceptos.GetComponente(item);
				_entity.Conceptos.Remove(concepto);
			}
		}

		protected void DoAddPedido(BackgroundWorker bk)
		{
			Datos.RaiseListChangedEvents = false;
			Lines_BS.RaiseListChangedEvents = false;

			try
			{
				PgMng.Reset(_results.Count + 1, 1, Resources.Messages.IMPORTANDO_ALBARANES, this);

				//Asignamos el cliente
				if (_entity.OidHolder == 0)
				{
					_entity.CopyFrom(_results[0]);
					SetCliente(ClienteInfo.Get(_results[0].OidCliente));
				}

				foreach (PedidoInfo item in _results)
				{
					_entity.Insert(item);
					_pedidos.RemoveItem(item.Oid);
				}

				PgMng.Grow(string.Empty, "Insertar el Pedido");

				Result = BGResult.OK;
			}
			catch (Exception ex)
			{
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

		protected void SetSerie(SerieInfo source, bool newCode)
		{
			if (source == null) return;

			_serie = source;

			_delivery_type = (_entity.Contado) ? ETipoAlbaranes.Agrupados : ETipoAlbaranes.Todos;

			_entity.OidSerie = source.Oid;
			_entity.NumeroSerie = source.Identificador;
			_entity.NombreSerie = source.Nombre;
			Serie_TB.Text = _entity.NSerieSerie;
			Nota_TB.Text = source.Cabecera;

			if (newCode) _entity.GetNewCode(_entity.EHolderType, _delivery_type);

			Cache.Instance.Remove(typeof(BatchList));
			Cache.Instance.Remove(typeof(ProductList));

			ProductList.GetListBySerie(_serie.Oid, false, true);
		}

		protected virtual void SetCliente(ClienteInfo source)
		{
			if (source == null) return;

			Client_BS.DataSource = source;

			_entity.CopyFrom(source);

			//Cargamos los precios especiales del cliente
			if (source.Productos == null) source.LoadChilds(typeof(ProductoCliente), false);

			_pedidos_cliente = PedidoList.GetByClienteList(_entity.OidHolder, false);
            
            if (_cliente != null && _entity.Conceptos.Count > 0)
            {
                foreach (OutputDeliveryLine item in _entity.Conceptos)
                    item.SetPrice(_cliente);
            }
		}

		protected void SetAlmacen(StoreInfo source)
		{
			_almacen = source;

			if (_almacen != null)
			{
				_entity.OidAlmacen = _almacen.Oid;
				_entity.IDAlmacen = _almacen.Codigo;
				_entity.Almacen = _almacen.Nombre;
				Almacen_TB.Text = _entity.IDAlmacenAlmacen;

				AddCacheItem(source);
			}
			else
			{
				_entity.OidAlmacen = 0;
				_entity.IDAlmacen = string.Empty;
				_entity.Almacen = string.Empty;
				Almacen_TB.Text = string.Empty;
			}
		}

		protected void SetExpediente(ExpedientInfo source)
		{
			_expediente = source;

			if (_expediente != null)
			{
				_entity.Expediente = _expediente.Codigo;
				_entity.OidExpediente = _expediente.Oid;
				Expediente_TB.Text = _entity.Expediente;

				AddCacheItem(source);
			}
			else
			{
				_entity.OidExpediente = 0;
				_entity.Expediente = string.Empty;
				Expediente_TB.Text = _entity.Expediente;
			}				
		}

		protected void SetTransportista(TransporterInfo source)
		{
			if (source == null) return;

			_entity.OidTransportista = source.Oid;
			Transporter_TB.Text = source.Nombre;
		}

		protected void SetWorkReport(WorkReportInfo source)
		{
			if (source == null) return;

			_entity.OidHolder = source.Oid;

			WorkReport_BS.DataSource = source;

			ExpedientInfo expedient = ExpedientInfo.Get(source.OidExpedient, false); 
			Expedient_BS.DataSource = expedient;

			_entity.IDCliente = (expedient != null) ? expedient.Codigo : string.Empty;
			_entity.NombreCliente = (expedient != null) ? expedient.TipoMercancia : string.Empty;
		}

		#endregion

        #region Actions

        protected override void SaveAction()
        {
            ClienteInfo cliente = Client_BS.DataSource as ClienteInfo;

            if (cliente != null)
            {
                if (cliente.CreditoDispuesto > 0)
                    if ((cliente.CreditoDispuesto + _entity.Total) > cliente.LimiteCredito)
                    {
                        string deuda = Convert.ToString(cliente.CreditoDispuesto + _entity.Total);
                        if (ProgressInfoMng.ShowQuestion(String.Format(Resources.Messages.CREDITO_DISPUESTO, deuda, cliente.LimiteCredito.ToString())) == DialogResult.No)
                        {
                            _action_result = DialogResult.Ignore;
                            return;
                        }
                    }
            }

			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		public override void PrintObject()
		{
			_action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;

			if (_action_result == DialogResult.OK)
			{
				FormMngBase.Instance.RefreshFormsData();

                OutputDeliveryReportMng reportMng = new OutputDeliveryReportMng(AppContext.ActiveSchema);
				FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();
				conf.nota = EntityInfo.Nota ? Nota_TB.Text : "";
				conf.cabecera = "ALBARAN";
				conf.copia = "";
				conf.cuenta_bancaria = "";

				ReportViewer.SetReport(reportMng.GetDetailReport(EntityInfo, conf));
				ReportViewer.ShowDialog();

                ExecuteAction(molAction.Close);
			}
		}

		protected override void CustomAction1() { }

		protected override void CustomAction2() { AddPedidoAction(null); }

		protected override void CustomAction3() { CrearFacturaAction(); }

		protected override void CustomAction4() { CrearTicketAction(); }

		protected virtual void AddPedidoAction(List<PedidoInfo> albaranes)
		{
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return;
			}

			if (_pedidos.Count == 0)
			{
				if (_entity.OidHolder != 0)
					_pedidos = PedidoList.GetPendientesList(_entity.OidHolder, _entity.OidSerie, false);
				else
					_pedidos = PedidoList.GetPendientesList(0, _entity.OidSerie, false);
			}

			if (albaranes == null)
			{
				PedidoSelectForm form = new PedidoSelectForm(this, PedidoList.GetList(_pedidos));
				form.ShowDialog(this);
				if (form.DialogResult == DialogResult.OK)
				{
					_results = form.Selected as List<PedidoInfo>;
				}
				else
					_results.Clear();
			}
			else
				_results = albaranes;

			if (_results.Count > 0)
			{
				foreach (PedidoInfo item in _results)
				{
					if (item.OidCliente != _results[0].OidCliente)
					{
						PgMng.ShowInfoException("No es posible asignar pedidos de clientes distintos a un mismo Albarán.");
						return;
					}
				}

				DoAddPedido(null);
			}

			if (Result == BGResult.OK)
			{
				Serie_BT.Enabled = false;
				Datos.ResetBindings(false);
			}

			if (Result == BGResult.OK)
				Lines_BS.ResetBindings(false);
		}

		protected virtual void CrearFacturaAction()
		{
			if (_entity.EEstado != EEstado.Abierto) return;

			ExecuteAction(molAction.Save, true);

			if (_action_result == DialogResult.OK)
			{
                if (_cliente == null) _cliente = Client_BS.Current as ClienteInfo;

				InvoiceAddForm form = new InvoiceAddForm(_cliente, _entity.GetInfo(), this);
				form.ShowDialog();

				_entity.EEstado = EEstado.Billed;
				_entity.NumeroFactura = form.Entity.Codigo;
				_entity.NumeroCliente = form.Entity.NumeroCliente;
			}
		}
		
		protected virtual void CrearTicketAction()
		{
			if (_entity.EEstado != EEstado.Abierto) return;

			TicketInfo ticket = TicketInfo.GetByAlbaran(_entity.Oid, false);

			if (ticket != null && ticket.Oid != 0)
			{
				PgMng.ShowInfoException(Resources.Messages.ALBARAN_TICKET_EXISTS);
				return;
			}

			ExecuteAction(molAction.Save, true);

			if (_action_result == DialogResult.OK)
			{
				TicketAddForm form = new TicketAddForm(_entity.GetInfo(), this);
				form.ShowDialog();

				_entity.NumeroTicket = form.Entity.Codigo;
			}
		}

		protected virtual void DeselectAlmacenAction()
		{
			SetAlmacen(null);
			_entity.SetAlmacen(null);
		}

		protected virtual void DeselectExpedienteAction() 
		{
			SetExpediente(null);
			_entity.SetExpediente(null); 
		}

		protected virtual void SelectAlmacenAction()
		{
			AlmacenSelectForm form = new AlmacenSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetAlmacen((StoreInfo)form.Selected);
				//_entity.SetAlmacen(_almacen);
				ControlsMng.UpdateBinding(Lines_BS);
			}
		}

		protected virtual void SelectClienteAction()
		{
            //if ((_entity.Conceptos.Count > 0) && (!_entity.Contado))
            //{
            //    PgMng.ShowInfoException("No es posible cambiar el cliente a un albarán con conceptos asociados.");
            //    return;
            //}

            if (_entity.OidSerie == 0)
            {
                PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
                return;
            }

			ClientSelectForm form = new ClientSelectForm(this, EEstado.Active);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_cliente = form.Selected as ClienteInfo;
				if (_cliente.Oid != _entity.OidHolder)
				    SetCliente(_cliente);
			}
		}

		protected virtual void SelectExpedienteAction()
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetExpediente((ExpedientInfo)form.Selected);
				_entity.SetExpediente(_expediente);
				RefreshConceptos();
			}
		}

		protected virtual void SelectSerieAction()
		{
			SerieList list = null;
			
			switch (_entity.EHolderType)
			{
				case ETipoEntidad.WorkReport:
					{
						list = SerieList.GetList(false, ETipoSerie.Work);
					}
					break;

				case ETipoEntidad.Cliente:
					{
						list = SerieList.GetList(false, ETipoSerie.Venta);
					}
					break;
			}

			SerieSelectForm form = new SerieSelectForm(this, list);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetSerie(form.Selected as SerieInfo, true);
			}
		}

		protected virtual void SelectTransportistaAction()
		{
			TransporterSelectForm form = new TransporterSelectForm(this, EEstado.Active);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetTransportista(form.Selected as TransporterInfo);
			}
		}

		protected virtual void SelectUsuarioAction()
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

		protected virtual void SelectWorkReportAction()
		{
			WorkReportSelectForm form = new WorkReportSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_work_report = form.Selected as WorkReportInfo;
				SetWorkReport(_work_report);
			}
		}

        protected override void ClusteredAction() 
        {
			if (Contado_CB.Checked && _entity.OidHolder != 139)
            {
                _cliente = ClienteInfo.Get(139);
				SetCliente(_cliente);
                _entity.Contado = true;
            }
        }

        protected override void EditLineAction()
        {
            if (Lines_BS.Current == null) return;

            OutputDeliveryLine cf = (OutputDeliveryLine)Lines_BS.Current;
            _cliente = Client_BS.Current as ClienteInfo;

            if (cf.OidPartida == 0)
            {
                DeliveryLineEditForm form = new DeliveryLineEditForm(ETipoProducto.Libres, _entity, _serie,_cliente, cf, this);

                if (form.ShowDialog(this) == DialogResult.OK)
                    _entity.CalculateTotal();
            }
            else
            {
				DeliveryLineEditForm form = new DeliveryLineEditForm(ETipoProducto.Almacen, _entity, _serie, _cliente, cf, this);
                
				if (form.ShowDialog(this) == DialogResult.OK)
                    _entity.CalculateTotal();
            }
        }
        protected override void DeleteLineAction()
        {
            if (Lines_BS.Current == null) return;

            if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
            {
				if (Concepto.OidPedido == 0)
				{
					PgMng.Reset(4, 1, Store.Resources.Messages.UPDATING_STOCK, this);

					if (Concepto.OidPartida != 0)
					{
						BatchInfo partida = BatchInfo.Get(Concepto.OidPartida, true);
						PgMng.Grow();

						if (partida.IsKit) DeleteKit(partida);
						PgMng.Grow();
					}

					_entity.Conceptos.Remove(Concepto, true);
					_entity.CalculateTotal();
					PgMng.Grow();
				}
				else
				{
					long oidPedido = Concepto.OidPedido;

					_entity.Conceptos.Remove(Concepto, true);
					_entity.CalculateTotal();

					bool free_pedido = true;

                    foreach (OutputDeliveryLine item in _entity.Conceptos)
						if (item.OidPedido == Concepto.OidPedido) free_pedido = false;

					//Actualizamos la lista de pedidos disponibles
					if (free_pedido) _pedidos.AddItem(_pedidos_cliente.GetItem(oidPedido));
				}

                ControlsMng.UpdateBinding(Lines_BS);
                ControlsMng.UpdateBinding(Datos);
				PgMng.FillUp();
            }

			Serie_BT.Enabled = (_entity.Conceptos.Count > 0);
        }
		protected override void NewLineAction()
		{
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return;
			}

			switch (_entity.EHolderType)
			{
				case ETipoEntidad.WorkReport:

					if (_work_report == null)
					{
						PgMng.ShowInfoException(Resources.Messages.NO_WORK_REPORT_SELECTED);
						return;
					}
					break;

				case ETipoEntidad.Cliente:

					if (_entity.OidHolder == 0)
					{
						PgMng.ShowInfoException(Resources.Messages.NO_CLIENT_SELECTED);
						return;
					}

					_cliente = Client_BS.Current as ClienteInfo;

					break;
			}			

			DeliveryLinenAddForm form = new DeliveryLinenAddForm(ETipoProducto.Almacen, _entity, _serie, _cliente, this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_entity.CalculateTotal();

				if (_entity.Conceptos.Count > 0)
					Serie_BT.Enabled = false;

				ControlsMng.UpdateBinding(Datos);

				HideComponentes();

				AddCacheItem(form.Entity);
			}
		}
		protected override void NewFreeLineAction()
		{
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return;
			}

			switch (_entity.EHolderType)
			{
				case ETipoEntidad.WorkReport:

					if (_work_report == null)
					{
						PgMng.ShowInfoException(Resources.Messages.NO_WORK_REPORT_SELECTED);
						return;
					}
					break;

				case ETipoEntidad.Cliente:

					if (_entity.OidHolder == 0)
					{
						PgMng.ShowInfoException(Resources.Messages.NO_CLIENT_SELECTED);
						return;
					}

					_cliente = Client_BS.Current as ClienteInfo;

					break;
			}	

			DeliveryLinenAddForm form = new DeliveryLinenAddForm(ETipoProducto.Libres, _entity, _serie, _cliente, this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_entity.CalculateTotal();
			}
		}
		protected override void SelectLineExpedientAction()
		{
			if (_entity.EHolderType == ETipoEntidad.WorkReport) return;

			if (Lines_BS.Current == null) return;

            OutputDeliveryLine item = Lines_BS.Current as OutputDeliveryLine;

			if (item.OidPartida != 0)
			{
				ProgressInfoMng.ShowInfo(Resources.Messages.DELIVERY_LINE_EXPEDIENT_WARNING);
				return;
			}

            ExpedienteList expedientes = ExpedienteList.GetListByStockProducto(item.GetInfo(false));

			ExpedienteSelectForm form = new ExpedienteSelectForm(this, expedientes);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ExpedientInfo source = (ExpedientInfo)form.Selected;

                if (source.StockKilos < item.CantidadKilos)
                {
                    PgMng.ShowErrorException(Resources.Messages.STOCK_INSUFICIENTE + " " + source.StockKilos.ToString());
                    return;
                }

				item.OidExpediente = source.Oid;
				item.Expediente = source.Codigo;

				AddCacheItem(source);
			}
		}

		protected override void SelectLineTaxAction()
		{
			if (Lines_BS.Current == null) return;

            OutputDeliveryLine item = Lines_BS.Current as OutputDeliveryLine;

			ImpuestoSelectForm form = new ImpuestoSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo source = (ImpuestoInfo)form.Selected;

				item.OidImpuesto = source.Oid;
				item.PImpuestos = source.Porcentaje;

				_entity.CalculateTotal();
			}
		}

		protected override void UpdateDeliveryAction()
		{
            OutputDeliveryLine item = Lines_BS.Current as OutputDeliveryLine;
			ProductInfo producto = ProductInfo.Get(item.OidProducto, false, false);
			BatchInfo partida = BatchInfo.Get(item.OidPartida, false, false);

			item.AjustaCantidad(producto, (partida.Oid == 0) ? null : partida);
			_entity.CalculateTotal();

			ControlsMng.UpdateBinding(Lines_BS);
		}
        
        #endregion

		#region Buttons

		private void Almacen_BT_Click(object sender, EventArgs e) { SelectAlmacenAction(); }
		private void Cliente_BT_Click(object sender, EventArgs e) { SelectClienteAction(); }
		private void Expediente_BT_Click(object sender, EventArgs e) { SelectExpedienteAction(); }
		private void NoAlmacen_BT_Click(object sender, EventArgs e) { DeselectAlmacenAction(); }
		private void NoExpediente_BT_Click(object sender, EventArgs e) { DeselectExpedienteAction(); }
		private void Serie_BT_Click(object sender, EventArgs e) { SelectSerieAction(); }
		private void Transportista_BT_Click(object sender, EventArgs e) { SelectTransportistaAction(); }
		private void Usuario_BT_Click(object sender, EventArgs e) { SelectUsuarioAction(); }
		private void WorkReport_BT_Click(object sender, EventArgs e) { SelectWorkReportAction(); }
		private void SubmitPrint_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

		#endregion

        #region Events

        private void FormaPago_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FormaPago_CB.SelectedValue == null) return;
            EFormaPago forma_pago = (EFormaPago)(long)FormaPago_CB.SelectedValue;
            _entity.EFormaPago = forma_pago;

            _entity.Prevision = Library.Common.EnumFunctions.GetPrevisionPago(forma_pago, _entity.Fecha, _entity.DiasPago);
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();
        }

        private void DiasPago_NTB_TextChanged(object sender, EventArgs e)
        {
            try { _entity.DiasPago = DiasPago_NTB.LongValue; }
            catch { _entity.DiasPago = DiasPago_NTB.LongValue; }

			_entity.Prevision = Library.Common.EnumFunctions.GetPrevisionPago(_entity.EFormaPago, _entity.Fecha, _entity.DiasPago);
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();
        }

        private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
        {
            _entity.Fecha = Fecha_DTP.Value;
			_entity.Prevision = Library.Common.EnumFunctions.GetPrevisionPago(_entity.EFormaPago, Fecha_DTP.Value, _entity.DiasPago);
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();
        }

        private void Contado_CB_CheckedChanged(object sender, EventArgs e)
        {
            ClusteredAction();
        }

        private void Rectificativo_CKB_CheckedChanged(object sender, EventArgs e)
        {
            RectificativoAction();
        }

        private void Descuento_NTB_TextChanged(object sender, EventArgs e)
        {
            _entity.PDescuento = PDescuento_NTB.DecimalValue;
            _entity.CalculateTotal();
            Lines_BS.ResetBindings(false);
            Datos.ResetBindings(false);
        }

        #endregion
    }
}

