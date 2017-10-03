using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class ConceptoProformaLibreEditForm : ConceptoProformaLibreUIForm
    {
        #region Attributes & Properties

        public const string ID = "ConceptoProformaLibreEditForm";
        public static Type Type { get { return typeof(ConceptoProformaLibreEditForm); } }

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        #endregion

        #region Factory Methods

        public ConceptoProformaLibreEditForm(Form parent, Proforma proforma, SerieInfo serie, ClienteInfo cliente, ConceptoProforma concepto)
            : base(parent, proforma, serie, cliente)
        {
            InitializeComponent();

            _entity = concepto;

            RefreshMainData();

            // Va aquí porque si no peta en el padre porque _entity es nulo
            _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
        }

        #endregion

        #region Style & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            Productos_BT.Enabled = false;
            base.FormatControls();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            if (_entity == null) return;

            Datos.DataSource = _entity;

            _producto = ProductoInfo.Get(_entity.OidProducto, false);

            Datos_Productos.DataSource = _producto;
            PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion

        #region Business Methods

        protected override void ActualizaConcepto()
        {
            if (_producto == null) return;

            PrecioCliente_NTB.Text = _entity.Precio.ToString("N5");

            base.ActualizaConcepto();
        }

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Undo_button_Click
        /// </summary>
        protected override void CancelAction()
        {
            _entity.CancelEdit();
            Cerrar();
        }

        #endregion

        #region Business Methods

        #endregion

        #region Events

        #endregion
    }
}

