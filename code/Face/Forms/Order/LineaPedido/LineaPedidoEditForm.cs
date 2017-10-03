using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
    public partial class LineaPedidoEditForm : LineaPedidoUIForm
    {

        #region Attributes & Properties

        public const string ID = "LineaPedidoEditForm";
        public static Type Type { get { return typeof(LineaPedidoEditForm); } }

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        #endregion

        #region Factory Methods

		public LineaPedidoEditForm(ETipoProducto tipo, Pedido albaran, SerieInfo serie, ClienteInfo cliente, LineaPedido concepto, Form parent)
            : base(tipo, albaran, serie, cliente, parent)
        {
            InitializeComponent();

            this.Text = Resources.Labels.CONCEPTO_EDIT_TITLE;

            _entity = concepto;

            RefreshMainData();

            // Va aquí porque si no peta en el padre porque _entity es nulo
            _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
        }

        #endregion

        #region Layout

		protected override void EnableKilos()
		{
            if (_entity.Pendiente != _entity.CantidadKilos)
			{
				Kilos_NTB.Enabled = false;
				Pieces_NTB.Enabled = false;

				Kilos_NTB.BackColor = PrecioCliente_NTB.BackColor;
				Pieces_NTB.BackColor = PrecioCliente_NTB.BackColor;

				FacturarPeso_CkB.Enabled = false;
			}
			else
				base.EnableKilos();
		}

		public override void FormatControls()
        {
            Productos_BT.Enabled = false;
            base.FormatControls();
        }

        #endregion

		#region Source

		protected override void RefreshMainData()
		{
			if (_entity == null) return;

			Datos.DataSource = _entity;

			_producto = ProductInfo.Get(_entity.OidProducto, false);
			PgMng.Grow();

			switch (_tipo)
			{
				case ETipoProducto.Almacen:
					{
						BatchList lista;

						lista = BatchList.GetListBySerieAndStock(_serie.Oid, false, true);

						_partida = lista.GetItem(_entity.OidPartida);

						//Caso extraño de que se vaya a modificar un concepto de un producto con stock 0
						if (_partida == null)
							_partida = BatchInfo.Get(_entity.OidPartida, false);

						Datos_Partida.DataSource = _partida;

						_partida.StockBultos += _entity.CantidadBultos;
                        _partida.StockKilos += _entity.CantidadKilos;
						PgMng.Grow();
					}
					break;

				case ETipoProducto.Libres:
					{
						_partida = null;

						Datos_Productos.DataSource = _producto;
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
			/*if (_entity.Partida == null)
			{
				_entity.LoadChilds(typeof(Batch), true);
				if (_entity.Partida == null) return;
			}

			LineaPedido concepto;

			foreach (Partida item in _entity.Partida.Componentes)
			{
				concepto = _albaran.Conceptos.GetItem(new FCriteria<long>("OidPartida", item.Oid));
				concepto.FacturacionBulto = false;
				concepto.Precio = item.PrecioVentaKilo * _entity.Precio / _partida.PrecioVentaKilo;
				concepto.CantidadKilos = _entity.CantidadKilos * item.Proporcion / 100;
				concepto.CantidadBultos = concepto.CantidadKilos / item.KilosPorBulto;
				concepto.FacturacionBulto = _entity.FacturacionBulto;
			}*/
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

			if (_partida != null)
			{
				_partida.StockBultos -= _entity.CantidadBultos;
                _partida.StockKilos -= _entity.CantidadKilos;
			}
        }

        #endregion
    }
}

