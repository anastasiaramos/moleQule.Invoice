using System;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;

using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class ConceptoProformaAddForm : ConceptoProformaUIForm
    {
        #region Attributes & Properties

        public const string ID = "ConceptoProformaAddForm";
        public static Type Type { get { return typeof(ConceptoProformaAddForm); } }

        #endregion
        
        #region Factory Methods

        public ConceptoProformaAddForm(Form parent, Proforma proforma, SerieInfo serie, ClienteInfo cliente)
            : base(parent, proforma, serie, cliente) 
        {
            InitializeComponent();

			this.MdiParent = parent;

            this.Text = Resources.Labels.CONCEPTO_NEW_TITLE;

            // Va aquí porque si no peta en el padre porque _entity es nulo
            _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
        }

        #endregion

        #region Style & Source

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            _entity = ConceptoProforma.NewChild(_proforma);
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

			_entity.OidExpediente = _partida.OidExpediente;
			_entity.CopyFrom(_partida);

			//Aplicamos precios especiales y condiciones particulares
			_entity.Vende(_serie, _cliente, _producto);

			//PARCHE PARA BALAÑOS. EL CLIENTE CONSUMO PROPIO PONE LOS PRECIOS A CERO
			if (_proforma.NombreCliente == "CONSUMO PROPIO")
			{
				_entity.OidImpuesto = 0;
				_entity.PImpuestos = 0;
				_entity.Precio = 0;
			}

			PrecioCliente_NTB.Text = _entity.Precio.ToString("N5");

			//PARCHE PARA BALAÑOS. Necesitan que el concepto contenga el valor del campo Descripcion
			//y no el del producto
			_entity.Concepto = _producto.Descripcion;

			base.ActualizaConcepto();
        }

        private void AddProducto()
        {
            _entity.CodigoExpediente = _partida.Expediente;
            _proforma.ConceptoProformas.NewItem(_entity);
        }

        private void AddKit()
        {
			_entity.CodigoExpediente = _partida.Expediente;
            _proforma.ConceptoProformas.NewItem(_entity);
            ConceptoProforma concepto;

			foreach (PartidaInfo item in _partida.Componentes)
            {
                concepto = ConceptoProforma.NewChild(_proforma);
                concepto.CopyFrom(item);
				concepto.OidExpediente = _partida.OidExpediente;
                concepto.PImpuestos = _serie.PImpuesto;
                concepto.FacturacionBulto = false;
                concepto.Precio = item.PrecioVentaKilo;
                concepto.Cantidad = _entity.Cantidad * item.Proporcion / 100;
                concepto.CantidadBultos = concepto.Cantidad / item.KilosPorBulto;
				concepto.CodigoExpediente = _partida.Expediente;
                concepto.FacturacionBulto = _entity.FacturacionBulto;
                _proforma.ConceptoProformas.NewItem(concepto);
            }
        }

        #endregion

        #region Actions

        protected override void DoSubmitAction()
        {
			if (_partida.IsKit)
                AddKit();
            else
                AddProducto();
        }

        protected override void SelectProductoAction()
        {
            PartidaList lista;

            lista = PartidaList.GetListBySerie(_serie.Oid, false, true);

			PartidaSelectForm form = new PartidaSelectForm(this, _serie, lista);
            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
				_partida = form.Selected as PartidaInfo;
				_producto = ProductoInfo.Get(_partida.OidProducto, false);
				_pci = _cliente.Productos.GetItemByProperty("OidProducto", _producto.Oid);

                Datos_Partida.DataSource = _partida;

                EnableKilos();

                if (_entity.FacturacionBulto)
                    Bultos_NTB.Focus();
                else
                    Kilos_NTB.Focus();
            }
        }

        #endregion

        #region Buttons

        #endregion

        #region Events

        #endregion
    }
}

