using System;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class LineaPedidoAddForm : LineaPedidoUIForm
    {
        #region Attributes & Properties

        public const string ID = "LineaPedidoAddForm";
        public static Type Type { get { return typeof(LineaPedidoAddForm); } }

        #endregion
        
        #region Factory Methods

		public LineaPedidoAddForm(ETipoProducto tipo, Pedido albaran, SerieInfo serie, ClienteInfo cliente, Form parent)
            : base(tipo, albaran, serie, cliente, parent) 
        {
            InitializeComponent();

            this.Text = Resources.Labels.CONCEPTO_NEW_TITLE;

            // Va aquí porque si no peta en el padre porque _entity es nulo
            _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            _entity = LineaPedido.NewChild(_albaran);
            _entity.PImpuestos = _serie.PImpuesto;

            Datos.DataSource = _entity;
            PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion

        #region Business Methods

        protected override void ActualizaConcepto()
        {
            if (_partida == null) return;
                       
            //Aplicamos precios especiales y condiciones particulares
			_entity.Reserva(_serie, _cliente, _producto, _partida);

            PrecioCliente_NTB.Text = _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());

            //PARCHE PARA BALAÑOS. Necesitan que el concepto contenga el valor del campo Descripcion
            //y no el del producto
            _entity.Concepto = _producto.Descripcion;

            base.ActualizaConcepto();
        }

        private void AddProducto()
        {
			_albaran.Lineas.NewItem(_entity);

			if (_partida != null)
			{
				_entity.Almacen = _partida.Almacen;
				_entity.Expediente = _partida.Expediente;
			}
			else
			{
				_entity.Almacen = string.Empty;
				_entity.Expediente = string.Empty;
			}
        }

        private void AddKit()
        {
			if (_partida == null) return;

            _entity.Expediente = _partida.Expediente;
			_albaran.Lineas.NewItem(_entity);
            LineaPedido concepto;

			foreach (BatchInfo item in _partida.Componentes)
            {
                concepto = LineaPedido.NewChild(_albaran);
                concepto.CopyFrom(item);
                concepto.OidKit = _partida.Oid;
                concepto.OidExpediente = _partida.OidExpediente;
                concepto.PImpuestos = _serie.PImpuesto;
                concepto.FacturacionBulto = false;
                concepto.Precio = item.PrecioVentaKilo;
                concepto.CantidadKilos = _entity.CantidadKilos * item.Proporcion / 100;
                concepto.CantidadBultos = concepto.CantidadKilos / item.KilosPorBulto;
                concepto.Expediente = _partida.Expediente;
                concepto.FacturacionBulto = _entity.FacturacionBulto;
				_albaran.Lineas.NewItem(concepto);
            }
        }

        #endregion

        #region Actions

        protected override void DoSubmitAction()
        {
			if (_partida == null) AddProducto();
			else if (_partida.IsKit) AddKit();
            else AddProducto();
        }

		protected override void SelectProductoAction()
		{
			ProductList lista;

			lista = ProductList.GetListBySerie(_serie.Oid, false, true);

			ProductSelectForm form = new ProductSelectForm(this, lista);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_producto = ProductInfo.Get((form.Selected as ProductInfo).Oid, false, true);

				_entity.CopyFrom(_producto);

				AsignaPrecio();

				Datos_Productos.DataSource = _producto;

				EnableKilos();

				if (_entity.FacturacionBulto)
					Pieces_NTB.Focus();
				else
					Kilos_NTB.Focus();
			}
		}

        protected override void SelectPartidaAction()
        {
            BatchList lista;

            lista = BatchList.GetListBySerieAndStock(_serie.Oid, true, false, true);

			BatchSelectForm form = new BatchSelectForm(this, _serie, lista);

			if (form.ShowDialog(this) == DialogResult.OK)
            {
                _partida = form.Selected as BatchInfo;
                _producto = ProductInfo.Get(_partida.OidProducto, false, true);

				_entity.CopyFrom(_partida, _producto);
				
				AsignaPrecio();

                Datos_Partida.DataSource = _partida;

                EnableKilos();

                if (_entity.FacturacionBulto)
                    Pieces_NTB.Focus();
                else
                    Kilos_NTB.Focus();
            }
        }

        #endregion
    }
}

