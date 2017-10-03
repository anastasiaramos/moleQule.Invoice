using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
    public partial class BudgetLineUIForm : Skin01.InputSkinForm
    {
        #region Attributes & Properties

        public override Type EntityType { get { return typeof(BudgetLine); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected BudgetLine _entity;
        protected Budget _budget;
        protected BatchInfo _batch;
        protected ProductInfo _product;
        protected SerieInfo _serie;
        protected ClienteInfo _client;

        public BudgetLine Entity { get { return _entity; } }

		public ETipoProducto _tipo = ETipoProducto.Almacen;

        #endregion

        #region Factory Methods

        protected BudgetLineUIForm() 
            : this(ETipoProducto.Libres, null, null, null, null) {}

        public BudgetLineUIForm(ETipoProducto productType, Budget budget, SerieInfo serie, ClienteInfo client, Form parent)
            : base(true, parent)
        {
            InitializeComponent();

            _budget = budget;
            _serie = serie;
            _client = client;
			_tipo = productType;
            SetFormData();
        }

        #endregion

        #region Layout & Source

		protected void EnableKilos()
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

		#endregion

		#region Source

        #endregion

		#region Business Methods

		protected virtual void ActualizaConcepto()
		{
			if (_product == null) return;

			PrecioProducto_NTB.Text = _product.PrecioVenta.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());
			_entity.SetTipoFacturacion(_client, _product);
			FacturarPeso_CkB.Checked = _entity.FacturacionPeso;

			EnableKilos();
			PaintBeneficio();
		}

		/// <summary>
		/// Calcula el precio inicial para este cliente
		/// </summary>
		protected void AsignaPrecio()
		{
			_entity.SetPrecio(_client, _product, _batch);

			Precio_NTB.Text = _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());
			PrecioCliente_NTB.Text = _entity.Precio.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());
			PrecioProducto_NTB.Text = _product.PrecioVenta.ToString("N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting());

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
            
            if (_product == null)
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

			ProductoClienteInfo pci = _client.Productos.GetByProducto(_entity.OidProducto);

            if (pci != null)
            {
                if ((_entity.Precio < pci.Precio) && (pci.ETipoDescuento == ETipoDescuento.Precio))
                {
					PgMng.ShowInfoException(Library.Invoice.Resources.Messages.PRECIO_VENTA_CLIENTE_INFERIOR);
                }
            }
            else if (_entity.Precio < _product.PrecioVenta)
            {
				PgMng.ShowInfoException(Library.Invoice.Resources.Messages.PRECIO_VENTA_PRODUCTO_INFERIOR);
            }

			if (_entity.OidPartida != 0)
			{
				if (!_entity.FacturacionBulto)
				{
                    if (_batch.StockKilos - _entity.CantidadKilos < 0)
					{
						PgMng.ShowInfoException(Resources.Messages.STOCK_INSUFICIENTE + _batch.StockKilos.ToString());

						_action_result = DialogResult.Ignore;
						return;
					}
				}
				else
				{
					if (_batch.StockBultos - _entity.CantidadBultos < 0)
					{
						PgMng.ShowInfoException(Resources.Messages.BULTOS_INSUFICIENTES + _batch.StockBultos.ToString());
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
                _batch.StockKilos -= _entity.CantidadKilos;
				_batch.StockBultos -= _entity.CantidadBultos;
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
				_entity.CalculateTotal();
			}
		}

		protected virtual void VerUltimasVentasProducto()
		{
			if (_product == null) return;

            OutputDeliveryLineList list = OutputDeliveryLineList.GetList(_product, _client, false);

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

        private void Datos_Partida_CurrentChanged(object sender, EventArgs e)
        {
            ActualizaConcepto();
        }

        private void Beneficio_TB_TextChanged(object sender, EventArgs e)
        {
            PaintBeneficio();
        }

        private void FBultos_CKB_CheckedChanged(object sender, EventArgs e)
        {
            _entity.FacturacionPeso = FacturarPeso_CkB.Checked;
            EnableKilos();
        }
        
        private void Kilos_NTB_Validated(object sender, EventArgs e)
        {
            if (_entity.ETipoFacturacion != ETipoFacturacion.Peso) return;

            try
            {
                _entity.AjustaCantidad(_product, _batch);
            }
            catch { }
        }

        private void PDescuento_NTB_Validated(object sender, EventArgs e)
        {
            try
            {
                _entity.CalculateTotal();
            }
            catch { }
        }

        private void Pieces_NTB_Validated(object sender, EventArgs e)
        {
            if (_entity.ETipoFacturacion == ETipoFacturacion.Peso)

                try
                {
                    _entity.AjustaCantidad(_product, _batch, Pieces_NTB.DecimalValue);
                    Kilos_NTB.Refresh();
                }
                catch { }
        }

        private void Precio_NTB_Validated(object sender, EventArgs e)
        {
            try
            {
                _entity.CalculateTotal();
            }
            catch { }
        }

        #endregion
    }
}