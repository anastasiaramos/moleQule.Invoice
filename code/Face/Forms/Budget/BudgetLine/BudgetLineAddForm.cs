using System;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class BudgetLineAddForm : BudgetLineUIForm
    {
        #region Attributes & Properties

        public const string ID = "ConceptoProformaAddForm";
        public static Type Type { get { return typeof(BudgetLineAddForm); } }

        #endregion
        
        #region Factory Methods

		public BudgetLineAddForm(ETipoProducto tipo, Budget proforma, SerieInfo serie, ClienteInfo cliente, Form parent)
			: base(tipo, proforma, serie, cliente, parent) 
        {
            InitializeComponent();

            this.Text = Resources.Labels.CONCEPTO_NEW_TITLE;
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            _entity = BudgetLine.NewChild(_budget);
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
			_entity.Vende(_budget, _serie, _client, _product, _batch);

			PrecioCliente_NTB.Text = _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());

            //PARCHE PARA BALAÑOS. Necesitan que el concepto contenga el valor del campo Descripcion
            //y no el del producto
            _entity.Concepto = _product.Descripcion;

            base.ActualizaConcepto();
        }

        private void AddProducto()
        {
			_budget.Conceptos.NewItem(_entity);

			if (_batch != null)
			{
				_entity.Expediente = _batch.Expediente;
			}
			else
			{
				_entity.Expediente = string.Empty;
			}
        }

        #endregion

        #region Actions

        protected override void DoSubmitAction()
        {
			/*if (_partida == null) AddProducto();
			else if (_partida.IsKit) AddKit();
            else AddProducto();*/

			AddProducto();
        }

		protected override void SelectProductoAction()
		{
			ProductList lista;

			lista = ProductList.GetListBySerie(_serie.Oid, false, true);

			ProductSelectForm form = new ProductSelectForm(this, lista);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_product = ProductInfo.Get((form.Selected as ProductInfo).Oid, false, true);

				_entity.CopyFrom(_budget, _product);

				AsignaPrecio();

				Datos_Productos.DataSource = _product;

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
                _batch = form.Selected as BatchInfo;
                _product = ProductInfo.Get(_batch.OidProducto, false, true);

				_entity.CopyFrom(_budget, _batch, _product);
				
				AsignaPrecio();

                Batch_BS.DataSource = _batch;

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

