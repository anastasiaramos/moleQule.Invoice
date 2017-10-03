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
    public partial class ConceptoProformaLibreUIForm : Skin01.InputSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected ConceptoProforma _entity;
        protected Proforma _proforma;
        protected ProductoInfo _producto;
        protected ProductoClienteInfo _pci;
        protected SerieInfo _serie;
		protected ClienteInfo _cliente;

        protected ProductoInfo Producto { get { return _producto; } }
        
        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        protected ConceptoProformaLibreUIForm()
			: this(null, null, null, null) { }

        /// Constructor
        /// </summary>
		public ConceptoProformaLibreUIForm(Form parent, Proforma proforma, SerieInfo serie, ClienteInfo cliente)
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
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData() {}

        void PaintBeneficio()
        {
            if (_entity.Beneficio < 0)
            {
                Beneficio_TB.BackColor = System.Drawing.Color.OrangeRed;
            }
            else if (_entity.Beneficio == 0)
            {
                Beneficio_TB.BackColor = PrecioCliente_NTB.BackColor;
            }
            else
            {
                Beneficio_TB.BackColor = System.Drawing.Color.PaleGreen;
            }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SubmitAction()
        {
            if (_producto == null)
            {
                MessageBox.Show("Debe elegir un producto.",
                                Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                _action_result = DialogResult.Ignore;
                return;
            }

            if ((_entity.Cantidad == 0))
            {
                MessageBox.Show("Es necesario indicar una cantidad.",
                                Face.Resources.Labels.ADVISE_TITLE,
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
                {
                    _action_result = DialogResult.Ignore;
                    return;
                }
            }

            DoSubmitAction();

            _action_result = DialogResult.OK;
        }

        protected virtual void DoSubmitAction() {}

        /// <summary>
        /// Implementa Undo_button_Click
        /// </summary>
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
            PaintBeneficio();
        }

        /// <summary>
        /// Recalcula el precio del producto en base a si se Proforma por 
        /// bultos o por kilos
        /// </summary>
        private void RecalculaPrecio()
        {
            if (_producto == null) return;
            
            PrecioCliente_NTB.Text = _entity.Precio.ToString("N5");
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
                    _entity.CantidadBultos = _entity.Cantidad;
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

        private void Beneficio_TB_TextChanged(object sender, EventArgs e)
        {
            PaintBeneficio();
        }

        #endregion
    }
}

