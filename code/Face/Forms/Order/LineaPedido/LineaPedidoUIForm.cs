using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
    public partial class LineaPedidoUIForm : Skin01.InputSkinForm
    {

        #region Attributes & Properties

		public override Type EntityType { get { return typeof(LineaPedido); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected LineaPedido _entity;
        protected Pedido _albaran;
        protected BatchInfo _partida;
        protected ProductInfo _producto;
        protected SerieInfo _serie;
        protected ClienteInfo _cliente;

		public LineaPedido Entity { get { return _entity; } }

		public ETipoProducto _tipo = ETipoProducto.Almacen;

        #endregion

        #region Factory Methods

        protected LineaPedidoUIForm() 
            : this(ETipoProducto.Libres, null, null, null, null) {}

		public LineaPedidoUIForm(ETipoProducto tipoProducto, Pedido albaran, SerieInfo serie, ClienteInfo cliente, Form parent)
            : base(true, parent)
        {
            InitializeComponent();

            _albaran = albaran;
            _serie = serie;
            _cliente = cliente;
			_tipo = tipoProducto;
            SetFormData();
        }

        #endregion

        #region Layout

		protected virtual void EnableKilos()
		{
			Pieces_NTB.Enabled = _entity.FacturacionBulto;
			Pieces_NTB.BackColor = !_entity.FacturacionBulto ? PrecioCliente_NTB.BackColor : Color.White;
			Kilos_NTB.Enabled = _entity.FacturacionPeso;
			Kilos_NTB.BackColor = _entity.FacturacionBulto ? PrecioCliente_NTB.BackColor : Color.White;
		}

        public override void FormatControls()
        {
            if (Partida_DGW == null) return;

            base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.ULTIMAS_VENTAS_PRODUCTO, null);

			Precio_NTB.DataBindings[0].FormatString = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
			PrecioProducto_NTB.DataBindings[0].FormatString = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();

			switch (_tipo)
			{
				case ETipoProducto.Almacen:
					{
						Producto_DGW.Visible = false;
						Partida_DGW.Visible = true;

						switch ((ESerie)_serie.Oid)
						{
							case ESerie.GANADO:
							case ESerie.MAQUINARIA:

								BultosIniciales.Visible = false;
								StockBultos.Visible = false;
								KiloPorBulto.Visible = false;

								KilosIniciales.HeaderText = "Stock Inicial";
								StockKilos.HeaderText = "Stock";

								break;
						}
					} 
					break;

				case ETipoProducto.Libres:
					{
						Producto_DGW.Visible = true;
						Partida_DGW.Visible = false;
					}
					break;
			}
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					ShowAction(molAction.CustomAction1);

					break;

				case molView.ReadOnly:

					ShowAction(molAction.CustomAction1);

					break;
			}
		}

		protected void PaintBeneficio()
		{
			if (_entity.Beneficio < 0)
			{
				Beneficio_TB.BackColor = System.Drawing.Color.OrangeRed;
			}
			else if (_entity.Beneficio == 0)
			{
				Beneficio_TB.BackColor = BeneficioKilo_TB.BackColor;
			}
			else
			{
				Beneficio_TB.BackColor = System.Drawing.Color.PaleGreen;
			}
		}

		#endregion

		#region Business Methods

		protected virtual void ActualizaConcepto()
		{
			if (_producto == null) return;

			PrecioProducto_NTB.Text = _producto.PrecioVenta.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());
			_entity.SetTipoFacturacion(_cliente, _producto);
			FacturarPeso_CkB.Checked = _entity.FacturacionPeso;

			EnableKilos();
			PaintBeneficio();
		}

		/// <summary>
		/// Calcula el precio inicial para este cliente
		/// </summary>
		protected void AsignaPrecio()
		{
			_entity.SetPrecio(_cliente, _producto, _partida);

			Precio_NTB.Text = _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());
			PrecioCliente_NTB.Text = _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());
			PrecioProducto_NTB.Text = _producto.PrecioVenta.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());

			Datos.ResetBindings(false);

			PaintBeneficio();
		}

		protected void SetExpediente(ExpedientInfo source)
		{
			if (source != null)
			{
				_entity.Expediente = source.Codigo;
				_entity.OidExpediente = source.Oid;
				Expediente_TB.Text = source.Codigo;
			}
		}

		#endregion

		#region Actions

        protected override void SubmitAction()
        {
            this.Enabled = false;
            
            if (_producto == null)
            {
				PgMng.ShowInfoException(Library.Store.Resources.Messages.NO_PRODUCTO_SELECTED);

                _action_result = DialogResult.Ignore;
                return;
            }

            if ((_entity.CantidadKilos == 0) || (_entity.CantidadBultos == 0))
            {
				PgMng.ShowInfoException(Library.Store.Resources.Messages.NO_KILOS_BULTOS_SELECTED);

                _action_result = DialogResult.Ignore;
                return;
            }

			ProductoClienteInfo pci = _cliente.Productos.GetByProducto(_entity.OidProducto);

            if (pci != null)
            {
                if ((_entity.Precio < pci.Precio) && (pci.ETipoDescuento == ETipoDescuento.Precio))
                {
					PgMng.ShowInfoException(Library.Invoice.Resources.Messages.PRECIO_VENTA_CLIENTE_INFERIOR);
                }
            }
            else if (_entity.Precio < _producto.PrecioVenta)
            {
				PgMng.ShowInfoException(Library.Invoice.Resources.Messages.PRECIO_VENTA_PRODUCTO_INFERIOR);
            }

			if (_entity.OidPartida != 0)
			{
				if (!_entity.FacturacionBulto)
				{
                    if (_partida.StockKilos - _entity.CantidadKilos < 0)
					{
						PgMng.ShowInfoException(Resources.Messages.STOCK_INSUFICIENTE + _partida.StockKilos.ToString());

						_action_result = DialogResult.Ignore;
						return;
					}
				}
				else
				{
					if (_partida.StockBultos - _entity.CantidadBultos < 0)
					{
						PgMng.ShowInfoException(Resources.Messages.BULTOS_INSUFICIENTES + _partida.StockBultos.ToString());
						_action_result = DialogResult.Ignore;
						return;
					}
				}
			}

            if (_entity.Beneficio <= 0)
            {
                if (ProgressInfoMng.ShowQuestion(Resources.Messages.AVISO_PERDIDA) == DialogResult.No)
                {
                    _action_result = DialogResult.Ignore;
                    return;
                }
            }

			if (_entity.OidPartida != 0)
			{
                _partida.StockKilos -= _entity.CantidadKilos;
				_partida.StockBultos -= _entity.CantidadBultos;
			}

            DoSubmitAction();

            _action_result = DialogResult.OK;
        }

        protected virtual void DoSubmitAction() {}

        protected override void CancelAction()
        {
            if (!IsModal) _entity.CancelEdit();
        }

		public override void CustomAction1() { VerUltimasVentasProducto(); }

		protected virtual void SelectExpedienteAction()
		{
			ExpedienteSelectForm form = new ExpedienteSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				SetExpediente((ExpedientInfo)form.Selected);
			}
		}
		protected virtual void SelectPartidaAction() { }
		protected virtual void SelectProductoAction() { }

		protected virtual void SelectImpuestoAction()
		{
			ImpuestoSelectForm form = new ImpuestoSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				ImpuestoInfo source = (ImpuestoInfo)form.Selected;

				_entity.OidImpuesto = source.Oid;
				_entity.PImpuestos = source.Porcentaje;
				_entity.CalculaTotal();
			}
		}

		protected virtual void VerUltimasVentasProducto()
		{
			if (_producto == null) return;

            OutputDeliveryLineList list = OutputDeliveryLineList.GetList(_producto, _cliente, false);

			DeliveryLineMngForm form = new DeliveryLineMngForm(true, this, list);
			form.ShowDialog(this);
		}

		#endregion

		#region Buttons

		private void Productos_BT_Click(object sender, EventArgs e)
		{
			switch (_tipo)
			{
				case ETipoProducto.Almacen: SelectPartidaAction(); break;
				case ETipoProducto.Libres: SelectProductoAction(); break;
			}
		}

		private void Impuestos_BT_Click(object sender, EventArgs e) { SelectImpuestoAction(); }

        private void Detalles_BT_Click(object sender, EventArgs e)
        {
            Detalles_GB.Visible = !Gastos_TB.Visible;
        }

		private void Expediente_BT_Click(object sender, EventArgs e) { SelectExpedienteAction(); }

        #endregion

        #region Events

        protected void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CantidadKilos":

					if (_entity.OidPartida == 0)
						_entity.AjustaCantidad(_producto);
					else if (!_entity.FacturacionBulto) 
						_entity.AjustaCantidadBultos(_partida);
                    
					break;

                case "CantidadBultos":
					if (_entity.OidPartida == 0)
						_entity.AjustaCantidad(_producto);
					else if (_entity.FacturacionBulto) 
						_entity.AjustaCantidadKilos(_partida);
                    break;

				case "Precio":
					_entity.CalculaTotal();
					break;

				case "PDescuento":
					_entity.CalculaTotal();
					break;
            }
        }
        
        private void Datos_Partida_CurrentChanged(object sender, EventArgs e)
        {
            ActualizaConcepto();
        }

        private void FBultos_CKB_CheckedChanged(object sender, EventArgs e)
        {
            _entity.FacturacionPeso = FacturarPeso_CkB.Checked;
            EnableKilos();
        }

        private void Beneficio_TB_TextChanged(object sender, EventArgs e)
        {
            PaintBeneficio();
        }

        #endregion

    }
}

