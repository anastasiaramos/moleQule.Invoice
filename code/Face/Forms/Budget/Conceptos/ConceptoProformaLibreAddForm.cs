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
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class ConceptoProformaLibreAddForm : ConceptoProformaLibreUIForm
    {
        #region Attributes & Properties

        public const string ID = "ConceptoProformaLibreAddForm";
        public static Type Type { get { return typeof(ConceptoProformaLibreAddForm); } }

        #endregion
        
        #region Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public ConceptoProformaLibreAddForm(Form parent, Proforma Proforma, SerieInfo serie, ClienteInfo cliente)
            : base(parent, Proforma, serie, cliente) 
        {
            InitializeComponent();

            // Va aquí porque si no peta en el padre porque _entity es nulo
            _entity.PropertyChanged += new PropertyChangedEventHandler(Entity_PropertyChanged);
        }

        #endregion

        #region Style & Source

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            _entity = ConceptoProforma.NewChild(_proforma);
            _entity.PImpuestos = _serie.PImpuesto;

            Datos.DataSource = _entity;
            PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion

        #region Business Methods

        protected override void ActualizaConcepto()
        {
            if (_producto == null) return;

            _entity.CopyFrom(_producto);

            //Aplicamos precios especiales y condiciones particulares
            _entity.Vende(_serie, _cliente, _producto);
            
            PrecioCliente_NTB.Text = _entity.Precio.ToString("N5");

            base.ActualizaConcepto();
        }

        private void AddProducto()
        {
            _entity.CodigoExpediente = string.Empty;
            _proforma.ConceptoProformas.NewItem(_entity);
        }

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void DoSubmitAction()
        {
            AddProducto();
        }

        protected override void SelectProductoAction()
        {
            ProductoList lista;

            lista = ProductoList.GetListBySerie(_serie.Oid, false, true);

            ProductoSelectForm form = new ProductoSelectForm(this, lista);
            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                _producto = ProductoInfo.Get((form.Selected as ProductoInfo).Oid, true);
                
                Datos_Productos.DataSource = _producto;
                _entity.Cantidad = 1;
                Cantidad_NTB.Focus();
            }
        }

        #endregion

        #region Events

        #endregion
    }
}

