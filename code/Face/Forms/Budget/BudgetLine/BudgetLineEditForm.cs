using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class BudgetLineEditForm : BudgetLineUIForm
    {
        #region Attributes & Properties

        public const string ID = "ConceptoProformaEditForm";
        public static Type Type { get { return typeof(BudgetLineEditForm); } }

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        #endregion

        #region Factory Methods

        public BudgetLineEditForm(ETipoProducto productType, Budget budget, SerieInfo serie, ClienteInfo client, BudgetLine line, Form parent)
			: base(productType, budget, serie, client, parent)
        {
            InitializeComponent();

            _entity = line;

            RefreshMainData();
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            Productos_BT.Enabled = false;
            base.FormatControls();
        }

        protected override void RefreshMainData()
        {
            if (_entity == null) return;

            Datos.DataSource = _entity;
			
			_product = ProductInfo.Get(_entity.OidProducto, false);
			PgMng.Grow();

			switch (_tipo)
			{
				case ETipoProducto.Almacen:
					{
						BatchList lista;

						lista = BatchList.GetListBySerieAndStock(_serie.Oid, false, true);

						_batch = lista.GetItem(_entity.OidPartida);

						//Caso extraño de que se vaya a modificar un concepto de un producto con stock 0
						if (_batch == null)
							_batch = BatchInfo.Get(_entity.OidPartida, false);

						Batch_BS.DataSource = _batch;

						_batch.StockBultos += _entity.CantidadBultos;
						_batch.StockKilos += _entity.CantidadKilos;
						PgMng.Grow();
					}
					break;

				case ETipoProducto.Libres:
					{
						_batch = null;

						Datos_Productos.DataSource = _product;
						PgMng.Grow();
					}
					break;
			}

            base.RefreshMainData();

            EnableKilos();
        }

        #endregion

        #region Business Methods

        protected override void ActualizaConcepto()
        {
			PrecioCliente_NTB.Text = _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());

            base.ActualizaConcepto();
        }

        #endregion

        #region Actions

        protected override void DoSubmitAction()
        {
            _entity.FacturacionPeso = FacturarPeso_CkB.Checked;

            //EditKit();
        }

        protected override void CancelAction()
        {
            _entity.CancelEdit();

			if (_batch != null)
			{
				_batch.StockBultos -= _entity.CantidadBultos;
                _batch.StockKilos -= _entity.CantidadKilos;
			}
        }

        #endregion
    }
}

