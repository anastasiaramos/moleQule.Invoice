using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using CslaEx;

using moleQule.Library;
using moleQule.Face;
using moleQule.Face.Skin01;

using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class ConceptoProformaEditForm : ConceptoProformaUIForm
    {

        #region Attributes & Properties

        public const string ID = "ConceptoProformaEditForm";
        public static Type Type { get { return typeof(ConceptoProformaEditForm); } }

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        #endregion

        #region Factory Methods

		public ConceptoProformaEditForm(Form parent, Proforma proforma, SerieInfo serie, ClienteInfo cliente, ConceptoProforma concepto)
            : base(parent, proforma, serie, cliente)
        {
            InitializeComponent();

            this.Text = Resources.Labels.CONCEPTO_EDIT_TITLE;

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

            PartidaList lista;

             lista = PartidaList.GetListBySerie(_serie.Oid, false, true);

            _partida = lista.GetItem(_entity.OidPartida);

            Datos_Partida.DataSource = _partida;

            PgMng.Grow();

            base.RefreshMainData();

            EnableKilos();
        }

        #endregion

        #region Business Methods

        protected override void ActualizaConcepto()
        {
			if (_partida == null) return;

            _producto = ProductoInfo.Get(_entity.OidProducto, true);
            _pci = _producto.ProductoClientes.GetItemByProperty("OidCliente", _proforma.OidCliente);

            decimal precio = (_pci != null) ? _pci.Precio : _producto.PrecioVenta;
            PrecioCliente_NTB.Text = precio.ToString("N5");

            base.ActualizaConcepto();
        }

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void DoSubmitAction()
        {
            _entity.FacturacionBulto = FBultos_CKB.Checked;
        }

        /// <summary>
        /// Implementa Undo_button_Click
        /// </summary>
        protected override void CancelAction()
        {
            _entity.CancelEdit();
            _action_result = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region Business Methods

        #endregion

        #region Events

        #endregion
    }
}

