using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using CslaEx;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
    public partial class ConceptoProformaUIForm : Skin01.InputSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected ConceptoProforma _entity;
        protected Proforma _proforma;
        protected PartidaInfo _partida;
        protected ProductoInfo _producto;
        protected ProductoClienteInfo _pci;
        protected SerieInfo _serie;
		protected ClienteInfo _cliente;
       
        #endregion

        #region Factory Methods

        protected ConceptoProformaUIForm() 
            : this(null, null, null, null) { }

		public ConceptoProformaUIForm(Form parent, Proforma proforma, SerieInfo serie, ClienteInfo cliente)
            : base(true, parent)
        {
            InitializeComponent();

            _proforma = proforma;
            _serie = serie;
			_cliente = cliente;
            SetFormData();
        }

        #endregion

        #region Style & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            if (Tabla_Productos == null) return;

            base.FormatControls();

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

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData() {}

        protected void EnableKilos()
        {
            Bultos_NTB.Enabled = FBultos_CKB.Checked;
            Bultos_NTB.BackColor = !FBultos_CKB.Checked ? PrecioCliente_NTB.BackColor : Color.White;
            Kilos_NTB.Enabled = !FBultos_CKB.Checked;
            Kilos_NTB.BackColor = FBultos_CKB.Checked ? PrecioCliente_NTB.BackColor : Color.White;
        }

        void PaintBeneficio()
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

        #region Actions

        protected override void SubmitAction()
        {
			this.Enabled = false;

			if (_partida == null)
            {
                MessageBox.Show("Debe elegir un producto.",
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                _action_result = DialogResult.Ignore;
                return;
            }

            if ((_entity.Cantidad == 0) || (_entity.CantidadBultos == 0))
            {
                MessageBox.Show("Es necesario indicar el número de bultos y el de kilos.",
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                _action_result = DialogResult.Ignore;
                return;
            }

            if (_pci != null)
            {
                if (_entity.Precio < _pci.Precio)
                {
                    MessageBox.Show("El precio de venta es inferior al precio de referencia para este cliente.",
                                    moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            else if (_entity.Precio < _producto.PrecioVenta)
            {
                MessageBox.Show("El precio de venta es inferior al precio de referencia de este producto.",
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

            if (_entity.Beneficio <= 0)
            {
                if (MessageBox.Show(Resources.Messages.AVISO_PERDIDA,
                                    moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                    MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Question) == DialogResult.No)

                    _action_result = DialogResult.Ignore;
                    return;
            }

            DoSubmitAction();

            _action_result = DialogResult.OK;
        }

        protected virtual void DoSubmitAction() {}

        protected override void CancelAction()
        {
            if (!IsModal) _entity.CancelEdit();

            _action_result = DialogResult.Cancel;
        }

		protected virtual void SelectProductoAction() {}

        #endregion

        #region Buttons

		private void Productos_BT_Click(object sender, EventArgs e)
		{
			SelectProductoAction();
		}

        private void Detalles_BT_Click(object sender, EventArgs e)
        {
            Detalles_GB.Visible = !Gastos_TB.Visible;
        }

        #endregion

        #region Business Methods

		protected virtual void ActualizaConcepto()
		{
			PrecioProducto_NTB.Text = _producto.PrecioVenta.ToString("N5");
			FBultos_CKB.Checked = _entity.FacturacionBulto;

			EnableKilos();
			PaintBeneficio();
		}

		/// <summary>
		/// Recalcula el precio del producto en base a si se albaranea por bultos o por kilos
		/// </summary>
		private void RecalculaPrecio()
		{
			if (_partida == null) return;

			_entity.UpdatePrecio(_producto, _partida, _pci);

			PrecioCliente_NTB.Text = (_pci != null) ? _pci.Precio.ToString("N5") : _producto.PrecioVenta.ToString("N5");
			PrecioProducto_NTB.Text = _producto.PrecioVenta.ToString("N5");

			Datos.ResetBindings(false);

			PaintBeneficio();
		}

        #endregion

        #region Events

        protected void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Cantidad":
					if (_partida == null) return;
					if (!_entity.FacturacionBulto) _entity.AjustaCantidadBultos(_partida);
                    break;

                case "CantidadBultos":
					if (_partida == null) return;
					if (_entity.FacturacionBulto) _entity.AjustaCantidadKilos(_partida);
                    break;

				case "PDescuento":
					_entity.CalculateTotal();
					break;
            }
        }
       
        private void Datos_Partida_CurrentChanged(object sender, EventArgs e)
        {
            ActualizaConcepto();
        }

        private void FBultos_CKB_CheckedChanged(object sender, EventArgs e)
        {
            _entity.FacturacionBulto = FBultos_CKB.Checked;
            EnableKilos();
            RecalculaPrecio();
        }

        private void Beneficio_TB_TextChanged(object sender, EventArgs e)
        {
            PaintBeneficio();
        }

        #endregion

    }
}

