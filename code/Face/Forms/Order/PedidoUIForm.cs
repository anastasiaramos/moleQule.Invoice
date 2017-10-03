using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class PedidoUIForm : PedidoForm
    {
        #region Attributes & Properties
		
		protected override int BarSteps { get { return base.BarSteps + 7; } }

        protected Pedido _entity;
		protected StoreInfo _almacen = null;
		protected SerieInfo _serie = null;
		protected ClienteInfo _cliente = null;
		protected ExpedientInfo _expediente = null;

        public override Pedido Entity { get { return _entity; } set { _entity = value; } }
        public override PedidoInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo(false) : null; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Declarado por exigencia del entorno. No Utilizar.
        /// </summary>
        protected PedidoUIForm() 
			: this(-1) { }
		
        public PedidoUIForm(long oid) 
			: this(oid, true) {}

		public PedidoUIForm(long oid, bool isModal) 
			: this(oid, isModal, null) {}

        public PedidoUIForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();
        }

		public override void DisposeForm()
		{
			Expedients exps = (Expedients)Cache.Instance.Get(typeof(Expedients));

			Cache.Instance.Remove(typeof(Expedients));
			Cache.Instance.Remove(typeof(BatchList));
			Cache.Instance.Remove(typeof(ProductList));

			if (exps != null) exps.CloseSession();
		}

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            this.Datos.RaiseListChangedEvents = false;

            try
            {
				//Creamos la cache con los expedientes implicados
				//Expedients exps = Expedients.GetListFromList(_oidExpedientes, false);
				PgMng.Grow();

				/*foreach (Expediente item in exps)
				{
					item.LoadChildsFromList(typeof(Batch), _oidPartidas, false);
					item.LoadChildsFromList(typeof(Stock), _oidPartidas, false);
				}
				PgMng.Grow();*/

				//Cache.Instance.Save(typeof(Expedients), exps);
				PgMng.Grow();

				Pedido temp = _entity.Clone();
				temp.ApplyEdit();

				_entity = temp.Save();
                _entity.ApplyEdit();
					
                return true;
            }
            catch (Exception ex)
            {
                PgMng.ShowInfoException(ex);
                return false;
            }
            finally
            {
                this.Datos.RaiseListChangedEvents = true;
            }
        }

        #endregion

		#region Cache

		protected HashOidList _oidAlmacenes = new HashOidList();
		protected HashOidList _oidExpedientes = new HashOidList();
		protected HashOidList _oidProductos = new HashOidList();
		protected HashOidList _oidPartidas = new HashOidList();

		protected void AddCacheItem()
		{
			if (Datos_Lineas.Current == null) return;
			AddCacheItem((LineaPedido)Datos_Lineas.Current);
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
		protected void AddCacheItem(LineaPedido item)
		{
			if (item == null) return;

			_oidAlmacenes.Add(item.OidAlmacen);
			_oidExpedientes.Add(item.OidExpediente);
			_oidPartidas.Add(item.OidPartida);
			_oidProductos.Add(item.OidProducto);
		}

		protected override void BuildCache()
		{
			foreach (LineaPedido item in _entity.Lineas)
				AddCacheItem(item);
		}

		protected override void CleanCache()
		{
			Stores almacenes = (Stores)Cache.Instance.Get(typeof(Stores));
			if (almacenes != null) almacenes.CloseSession();

			Expedients expedientes = (Expedients)Cache.Instance.Get(typeof(Expedients));
			if (expedientes != null) expedientes.CloseSession();

			Cache.Instance.Remove(typeof(Stores));
			Cache.Instance.Remove(typeof(Expedients));
			Cache.Instance.Remove(typeof(ProductList));
			Cache.Instance.Remove(typeof(ClienteList));
		}

		#endregion

        #region Layout & Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

			Datos_Lineas.DataSource = _entity.Lineas;
			PgMng.Grow();

			Fecha_DTP.Value = _entity.Fecha;

            base.RefreshMainData();
        }

		public override void RefreshSecondaryData()
		{
			if (_entity.OidCliente != 0) SetCliente(ClienteInfo.Get(_entity.OidCliente, false));
			PgMng.Grow();

			if (_entity.OidSerie != 0) SetSerie(SerieInfo.Get(_entity.OidSerie, false), false);
			PgMng.Grow();

			if (_entity.OidAlmacen != 0) SetAlmacen(StoreInfo.Get(_entity.OidAlmacen, false, true));
			PgMng.Grow();

			if (_entity.OidExpediente != 0) SetExpediente(ExpedientInfo.Get(_entity.OidExpediente, false, true));
			PgMng.Grow();
		}

		protected override void HideComponentes()
		{
			foreach (DataGridViewRow row in Lineas_DGW.Rows)
				if ((row.DataBoundItem as LineaPedido).IsKitComponent)
					row.Visible = false;
		}

        #endregion

		#region Business Methods

		private void DeleteKit(BatchInfo partida)
		{
			LineaPedido linea;

			foreach (BatchInfo item in partida.Componentes)
			{
				linea = _entity.Lineas.GetComponente(item);
				_entity.Lineas.Remove(linea);
			}
		}

		protected void SetAlmacen(StoreInfo source)
		{
			_almacen = source;

			if (_almacen != null)
			{
				_entity.Almacen = _almacen.Nombre;
				_entity.OidAlmacen = _almacen.Oid;
				Almacen_TB.Text = _almacen.Nombre;
			}
		}

		protected void SetCliente(ClienteInfo source)
		{
			if (source == null) return;

			Datos_Cliente.DataSource = source;

			_entity.CopyFrom(source);

			//Cargamos los precios especiales del cliente
			if (source.Productos == null)
				source.LoadChilds(typeof(ProductoCliente), false);
		}

		protected void SetExpediente(ExpedientInfo source)
		{
			_expediente = source;

			if (_expediente != null)
			{
				_entity.Expediente = _expediente.Codigo;
				_entity.OidExpediente = _expediente.Oid;
				Expediente_TB.Text = _expediente.Codigo;
			}
		}

		protected void SetSerie(SerieInfo source, bool new_code)
		{
			if (source == null) return;

			_serie = source;

			_entity.OidSerie = source.Oid;
			_entity.NSerie = source.Identificador;
			_entity.Serie = source.Nombre;
			Serie_TB.Text = _entity.NSerieSerie;

			if (new_code) _entity.GetNewCode();

			Cache.Instance.Remove(typeof(ProductList));
			ProductList.GetListBySerie(_entity.OidSerie, false, true);
		}

		#endregion

		#region Actions

		protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

		protected override void DefaultAction() { EditarLineaAction(); }

		protected override void CrearAlbaranAction()
		{
			if (_entity.EEstado != EEstado.Abierto) return;
			if (_entity.IsComplete()) throw new iQException(Resources.Messages.PEDIDO_COMPLETO);

			ExecuteAction(molAction.Save);

			if (_action_result == DialogResult.OK)
			{
				_cliente = Datos_Cliente.DataSource as ClienteInfo;

				DeliveryAddForm form = new DeliveryAddForm(_cliente, _entity.GetInfo(), this);
				form.ShowDialog();

				//_entity.EEstado = EEstado.Cerrado;
			}
		}
		
		protected virtual void SelectAlmacenAction()
		{
			AlmacenSelectForm form = new AlmacenSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetAlmacen((StoreInfo)form.Selected);
				//_entity.SetAlmacen(_almacen);
				RefreshLineas();
			}
		}

		protected virtual void SelectClienteAction()
		{
			ClientSelectForm form = new ClientSelectForm(this, EEstado.Active);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ClienteInfo cliente = form.Selected as ClienteInfo;
				SetCliente(cliente);
			}
		}

		protected virtual void SelectEstadoAction()
		{
			EEstado[] list = { EEstado.Anulado, EEstado.Abierto, EEstado.Closed, EEstado.Cancelado };

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;

				_entity.Estado = estado.Oid;
				Estado_TB.Text = estado.Texto;
			}
		}
		
		protected virtual void SelectExpedienteAction()
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetExpediente((ExpedientInfo)form.Selected);
				_entity.SetExpediente(_expediente);
				RefreshLineas();		
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

		protected override void UpdatePedidoAction()
		{
			LineaPedido item = Datos_Lineas.Current as LineaPedido;
			ProductInfo producto = ProductInfo.Get(item.OidProducto, false, true);

			item.AjustaCantidad(producto);
			_entity.CalculateTotal();

			ControlsMng.UpdateBinding(Datos_Lineas);
		}

		protected override void AddLineaAction()
		{
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return;
			}

			if (_entity.OidCliente == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_CLIENT_SELECTED);
				return;
			}

			_cliente = Datos_Cliente.Current as ClienteInfo;

			LineaPedidoAddForm form = new LineaPedidoAddForm(ETipoProducto.Almacen, _entity, _serie, _cliente, this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_entity.CalculateTotal();

				if (_entity.Lineas.Count > 0)
					Serie_BT.Enabled = false;

				ControlsMng.UpdateBinding(Datos_Lineas);

				HideComponentes();

				AddCacheItem(form.Entity);
			}
		}

		protected override void EditarLineaAction()
		{
			if (Datos_Lineas.Current == null) return;

			LineaPedido item = (LineaPedido)Lineas_DGW.CurrentRow.DataBoundItem;
			if (item == null) return;

			_cliente = Datos_Cliente.Current as ClienteInfo;

			if (item.OidExpediente == 0)
			{
				LineaPedidoEditForm form = new LineaPedidoEditForm(ETipoProducto.Libres, _entity, _serie, _cliente, item, this);

				if (form.ShowDialog(this) == DialogResult.OK)
					_entity.CalculateTotal();
			}
			else
			{
				LineaPedidoEditForm form = new LineaPedidoEditForm(ETipoProducto.Almacen, _entity, _serie, _cliente, item, this);

				if (form.ShowDialog(this) == DialogResult.OK)
					_entity.CalculateTotal();
			}
		}

		protected override void DeleteLineaAction()
		{
			if (Lineas_DGW.CurrentRow.DataBoundItem == null) return;
			LineaPedido item = (LineaPedido)Lineas_DGW.CurrentRow.DataBoundItem;
			if (item == null) return;

			if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
			{
				PgMng.Reset(4, 1, Store.Resources.Messages.UPDATING_STOCK, this);

				BatchInfo partida = BatchInfo.Get(item.OidPartida, true);
				PgMng.Grow();

				if (partida.IsKit) DeleteKit(partida);
				PgMng.Grow();

				_entity.Lineas.Remove(item, _entity);

				ControlsMng.UpdateBinding(Datos_Lineas);
				ControlsMng.UpdateBinding(Datos);
				PgMng.FillUp();
			}
		}

		protected override void AddLineaLibreAction()
		{
			if (_entity.OidSerie == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_SERIE_SELECTED);
				return;
			}

			if (_entity.OidCliente == 0)
			{
				PgMng.ShowInfoException(Resources.Messages.NO_CLIENT_SELECTED);
				return;
			}

			_cliente = Datos_Cliente.Current as ClienteInfo;

			LineaPedidoAddForm form = new LineaPedidoAddForm(ETipoProducto.Libres, _entity, _serie, _cliente, this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_entity.CalculateTotal();
			}
		}

		protected override void SelectAlmacenLineaAction()
		{
			if (Datos_Lineas.Current == null) return;

			LineaPedido item = Datos_Lineas.Current as LineaPedido;

			AlmacenSelectForm form = new AlmacenSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				StoreInfo source = (StoreInfo)form.Selected;

				item.OidAlmacen = source.Oid;
				item.Almacen = source.Nombre;

				ControlsMng.UpdateBinding(Lineas_DGW);
			}
		}

		protected override void SelectExpedienteLineaAction()
		{
			if (Datos_Lineas.Current == null) return;

			LineaPedido item = Datos_Lineas.Current as LineaPedido;

			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ExpedientInfo source = (ExpedientInfo)form.Selected;

				item.OidExpediente = source.Oid;
				item.Expediente = source.Codigo;

				ControlsMng.UpdateBinding(Lineas_DGW);
			}
		}

		protected override void SelectEstadoLineaAction()
		{
			if (Lineas_DGW.CurrentRow.DataBoundItem == null) return;

			LineaPedido item = (LineaPedido)Lineas_DGW.CurrentRow.DataBoundItem;
			if (item == null) return;

			EEstado[] list = { EEstado.Anulado, EEstado.Abierto, EEstado.Solicitado, EEstado.Closed };

			SelectEnumInputForm form = new SelectEnumInputForm(true);

			form.SetDataSource(Library.Common.EnumText<EEstado>.GetList(list));

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ComboBoxSource estado = form.Selected as ComboBoxSource;

				item.Estado = estado.Oid;

				ControlsMng.UpdateBinding(Datos_Lineas);
			}
		}

		protected override void SelectImpuestoLineaAction()
		{
			if (Datos_Lineas.Current == null) return;

			LineaPedido item = Datos_Lineas.Current as LineaPedido;

			ImpuestoSelectForm form = new ImpuestoSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo source = (ImpuestoInfo)form.Selected;

				item.OidImpuesto = source.Oid;
				item.PImpuestos = source.Porcentaje;
				_entity.CalculateTotal();

				ControlsMng.UpdateBinding(Lineas_DGW);
			}
		}

        #endregion

		#region Buttons

		private void Serie_BT_Click(object sender, EventArgs e) { SelectSerieAction(); }
		private void Cliente_BT_Click(object sender, EventArgs e) { SelectClienteAction(); }
		private void Estado_BT_Click(object sender, EventArgs e) { SelectEstadoAction(); }
		private void Usuario_BT_Click(object sender, EventArgs e) { SelectUsuarioAction(); }
		private void Almacen_BT_Click(object sender, EventArgs e) { SelectAlmacenAction(); }
		private void Expediente_BT_Click(object sender, EventArgs e) { SelectExpedienteAction(); }

		#endregion

	}
}
