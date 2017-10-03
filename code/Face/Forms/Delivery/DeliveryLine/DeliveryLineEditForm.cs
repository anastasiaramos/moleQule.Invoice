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
    public partial class DeliveryLineEditForm : DeliveryLineUIForm
    {
        #region Attributes & Properties

        public const string ID = "ConceptoAlbaranEditForm";
        public static Type Type { get { return typeof(DeliveryLineEditForm); } }

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        #endregion

        #region Factory Methods

        public DeliveryLineEditForm(ETipoProducto tipo, OutputDelivery delivery, SerieInfo serie, ClienteInfo cliente, OutputDeliveryLine line, Form parent)
            : base(tipo, delivery, serie, cliente, parent)
        {
            InitializeComponent();

            this.Text = Resources.Labels.CONCEPTO_EDIT_TITLE;

            _entity = line;

            RefreshMainData();

            // Va aquí porque si no peta en el padre porque _entity es nulo
            _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            Product_BT.Enabled = false;
            base.FormatControls();

            if (_entity.OidPartida != 0)
                ShowLineGrid(ETipoProducto.Almacen);
            else
                ShowLineGrid(ETipoProducto.Libres);
        }

        protected override void RefreshMainData()
        {
            if (_entity == null) return;

            Datos.DataSource = _entity;
			
			_product = ProductInfo.Get(_entity.OidProducto, false);
			PgMng.Grow();

			switch (_product_type)
			{
				case ETipoProducto.Almacen:
					{
						BatchList lista;

						if (_delivery.Rectificativo)
							lista = BatchList.GetListBySerie(_serie.Oid, false, true);
						else
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

						Products_BS.DataSource = _product;
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

		private void EditKit()
		{
			if (_entity.OidPartida == 0) return;
			if (_entity.IsNew) return;
			if (_entity.Partida == null)
			{
				_entity.LoadChilds(typeof(Batch), true);
				if (_entity.Partida == null) return;
			}

            OutputDeliveryLine concepto;

			foreach (Batch item in _entity.Partida.Componentes)
			{
				concepto = _delivery.Conceptos.GetItem(new FCriteria<long>("OidPartida", item.Oid));
				concepto.FacturacionBulto = false;
				concepto.Precio = item.PrecioVentaKilo * _entity.Precio / _batch.PrecioVentaKilo;
                concepto.CantidadKilos = _entity.CantidadKilos * item.Proporcion / 100;
                concepto.CantidadBultos = concepto.CantidadKilos / item.KilosPorBulto;
				concepto.FacturacionBulto = _entity.FacturacionBulto;
			}
		}

        #endregion

        #region Actions

        protected override void DoSubmitAction()
        {
            _entity.FacturacionPeso = FacturarPeso_CkB.Checked;

            EditKit();
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