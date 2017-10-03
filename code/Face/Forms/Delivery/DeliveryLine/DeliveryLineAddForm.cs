using System;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class DeliveryLinenAddForm : DeliveryLineUIForm
    {
        #region Attributes & Properties

        public const string ID = "ConceptoAlbaranAddForm";
        public static Type Type { get { return typeof(DeliveryLinenAddForm); } }

        #endregion
        
        #region Factory Methods

		public DeliveryLinenAddForm(ETipoProducto productType, OutputDelivery delivery, SerieInfo serie, ClienteInfo client, Form parent)
            : base(productType, delivery, serie, client, parent) 
        {
            InitializeComponent();

            this.Text = Resources.Labels.CONCEPTO_NEW_TITLE;

            // Va aquí porque si no peta en el padre porque _entity es nulo
            _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();

            ShowLineGrid(_product_type);
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            _entity = OutputDeliveryLine.NewChild(_delivery);
            _entity.PImpuestos = _serie.PImpuesto;

            Datos.DataSource = _entity;
            PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion

        #region Business Methods

        protected override void ActualizaConcepto()
        {
            if (_batch == null) return;
                       
            //Aplicamos precios especiales y condiciones particulares
			_entity.Sell(_delivery, _serie, _client, _product, _batch);

			PrecioCliente_NTB.Text = _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());

            //PARCHE PARA BALAÑOS. Necesitan que el concepto contenga el valor del campo Descripcion
            //y no el del producto excepto cuando se trata de animales, para que se copie el crotal
            if (_product.ETipoFacturacion != ETipoFacturacion.Unitaria)
                _entity.Concepto = _product.Descripcion;

            base.ActualizaConcepto();
        }

        private void AddProducto()
        {
            if (!_product.NoStockSale)
            {
                ProductInfo no_stock_product = null;
                Boolean stock = true;
                switch (_entity.ETipoFacturacion)
                {
                    case ETipoFacturacion.Peso:
                        stock = _product.CheckStock(_entity.ETipoFacturacion, _entity.CantidadKilos, out no_stock_product);
                        break;

                    case ETipoFacturacion.Unidad:
                        stock = _product.CheckStock(_entity.ETipoFacturacion, _entity.CantidadBultos, out no_stock_product);
                        break;
                }

                if (stock)
                    _delivery.Conceptos.NewItem(_entity);
                else
                    PgMng.ShowInfoException(String.Format(Resources.Messages.STOCK_INSUFICIENTE_COMPONENTE, no_stock_product.Nombre));
            }
            else
            {
                _delivery.Conceptos.NewItem(_entity);

                if (_batch != null)
                {
                    _entity.Almacen = _batch.Almacen;
                    _entity.Expediente = _batch.Expediente;
                }
                else
                {
                    _entity.Almacen = string.Empty;
                    _entity.Expediente = string.Empty;
                }
            }
        }

        private void AddMix()
        {
			if (_batch == null) return;

            _entity.Expediente = _batch.Expediente;
			_delivery.Conceptos.NewItem(_entity);
            OutputDeliveryLine concepto;

			foreach (BatchInfo item in _batch.Componentes)
            {
                concepto = OutputDeliveryLine.NewChild(_delivery);
                concepto.CopyFrom(item);
                concepto.OidKit = _batch.Oid;
                concepto.OidExpediente = _batch.OidExpediente;
                concepto.PImpuestos = _serie.PImpuesto;
                concepto.FacturacionBulto = false;
                concepto.Precio = item.PrecioVentaKilo;
                concepto.CantidadKilos = _entity.CantidadKilos * item.Proporcion / 100;
                concepto.CantidadBultos = concepto.CantidadKilos / item.KilosPorBulto;
                concepto.Expediente = _batch.Expediente;
                concepto.FacturacionBulto = _entity.FacturacionBulto;
				_delivery.Conceptos.NewItem(concepto);
            }
        }

        private bool CheckStock()
        {
            ProductInfo producto = ProductInfo.Get(_entity.OidProducto);

            if (producto.AvisarStock)
            {
                BatchInfo partida = BatchInfo.Get(_entity.OidPartida, false);

                if (!partida.CheckStock(_entity, producto))
                {
                    DialogResult result = PgMng.ShowWarningException(Resources.Messages.AVISO_STOCK_MINIMO);

                    if (result == DialogResult.Yes)
                        return true;
                    else
                        return false;
                }
                else return true;
            }
            else 
                return true;
        }

        #endregion

        #region Actions

        protected override void DoSubmitAction()
        {
            if (_batch == null) 
                AddProducto();
            else
            {
                if (CheckStock())
                {
                    if (_batch.IsKit) AddMix();
                    else AddProducto();
                }
            }
        }

		protected override void SelectProductoAction()
		{
			ProductList lista;

			lista = ProductList.GetListBySerie(_serie.Oid, false, true);

			ProductSelectForm form = new ProductSelectForm(this, lista);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_product = ProductInfo.Get((form.Selected as ProductInfo).Oid, false, true);

				_entity.CopyFrom(_delivery, _product);
                _entity.SetTaxes(_client, _product, _serie);
				_entity.AjustaCantidad(_product, null, 1);
                                
                SetStore(StoreInfo.Get(_delivery.OidAlmacen != 0 ? _delivery.OidAlmacen : Library.Store.ModulePrincipal.GetDefaultAlmacenSetting(), false));

				AsignaPrecio();

				Products_BS.DataSource = _product;

				EnableKilos();

				if (_entity.FacturacionBulto)
					Pieces_NTB.Focus();
				else
					Kilos_NTB.Focus();
			}

            ShowLineGrid(ETipoProducto.Libres);
		}

        protected override void SelectPartidaAction()
        {
            BatchList lista;

            if (_delivery.Rectificativo)
                lista = BatchList.GetListBySerie(_serie.Oid, false, true);
            else
                lista = BatchList.GetListBySerieAndStock(_serie.Oid, true, false, true);

			BatchSelectForm form = new BatchSelectForm(this, _serie, lista);

			if (form.ShowDialog(this) == DialogResult.OK)
            {
                 _batch = form.Selected as BatchInfo;
                _product = ProductInfo.Get(_batch.OidProducto, false, true);

				_entity.CopyFrom(_delivery, _batch, _product);
                _entity.SetTaxes(_client, _product, _serie);

				AsignaPrecio();

                Batch_BS.DataSource = _batch;

                EnableKilos();

                if (_entity.FacturacionBulto)
                    Pieces_NTB.Focus();
                else
                    Kilos_NTB.Focus();
            }

            ShowLineGrid(ETipoProducto.Almacen);
        }

        #endregion
    }
}