using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class PricesActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "PreciosActionForm";
        public static Type Type { get { return typeof(PricesActionForm); } }

        private FamiliaInfo _familia = null;
        private ProductInfo _producto = null;
        ETipoTitular _tipo = ETipoTitular.Cliente;

        #endregion

        #region Business Methods

        private string GetFilterValues()
        {
            string filtro = string.Empty;

            if (!TodosFamilia_CkB.Checked)
                filtro += "Familia " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _familia.Nombre + "; ";

            if (!TodosProducto_CkB.Checked)
                filtro += "Producto " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _producto.Nombre + "; ";

            if (FInicial_DTP.Checked)
                filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.GreaterOrEqual) + " " + FInicial_DTP.Value.ToShortDateString() + "; ";

            if (FFinal_DTP.Checked)
                filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.LessOrEqual) + " " + FFinal_DTP.Value.ToShortDateString() + "; ";

            return filtro;
        }

        #endregion

        #region Factory Methods

        public PricesActionForm() 
			: this(null) { }

		public PricesActionForm(Form parent)
			: this(true, parent) { }

		public PricesActionForm(bool isModal, Form parent)
            : base(isModal)
        {
            InitializeComponent();

            SetFormData();
        }

        #endregion

        #region Layout & Source

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        public override void RefreshSecondaryData()
        {
        }

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SubmitAction()
        {
            PgMng.Reset(4, 1, Face.Resources.Messages.RETRIEVING_DATA, this); 

            _familia = TodosFamilia_CkB.Checked ? null : _familia;
            _producto = TodosProducto_CkB.Checked ? null : _producto;
            DateTime f_ini = FInicial_DTP.Checked ? FInicial_DTP.Value : DateTime.MinValue;
            DateTime f_fin = FFinal_DTP.Checked ? FFinal_DTP.Value : DateTime.MaxValue;

            string filtro = GetFilterValues();
            IDataReader reader;

            if (Cliente_RB.Checked)
            {
                Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions();
                conditions.Familia = _familia;
                conditions.Producto = _producto;
                conditions.FechaIni = f_ini;
                conditions.FechaFin = f_fin;
                conditions.Order = (Ascendente_RB.Checked) ? ListSortDirection.Ascending : ListSortDirection.Descending;

                reader = ClienteList.GetPrices(conditions);
            }
            else
            {
                Library.Store.QueryConditions conditions = new Library.Store.QueryConditions();
                conditions.Familia = _familia;
                conditions.Producto = _producto;
                conditions.FechaIni = f_ini;
                conditions.FechaFin = f_fin;
                conditions.Order = (Ascendente_RB.Checked) ? ListSortDirection.Ascending : ListSortDirection.Descending;

                reader = ProveedorList.GetPrices(conditions);
            }
            PgMng.Grow();

            if (reader.Read())
            {
                PreciosForm form = new PreciosForm(_tipo, reader, filtro);
                PgMng.Grow();
                form.ShowDialog(this);
                PgMng.FillUp();
            }
            else
            {
                MessageBox.Show(moleQule.Face.Resources.Messages.NO_DATA_REPORTS,
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                PgMng.FillUp();
            }

            _action_result = DialogResult.Ignore;
        }

        #endregion

        #region Events

        private void TodosFamilia_GB_CheckedChanged(object sender, EventArgs e)
        {
            Familia_BT.Enabled = !TodosFamilia_CkB.Checked;
            Producto_GB.Enabled = TodosFamilia_CkB.Checked;
            if (TodosFamilia_CkB.Checked) Familia_TB.Text = string.Empty;
        }

        private void TodosProducto_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Producto_BT.Enabled = !TodosProducto_CkB.Checked;
            if (TodosProducto_CkB.Checked) Producto_TB.Text = string.Empty;
        }

        private void Cliente_RB_CheckedChanged(object sender, EventArgs e)
        {
            _tipo = (Cliente_RB.Checked) ? ETipoTitular.Cliente : ETipoTitular.Proveedor;
			Fechas_GB.Enabled = Cliente_RB.Checked;
        }
        
        private void Proveedor_RB_CheckedChanged(object sender, EventArgs e)
        {
            _tipo = (Proveedor_RB.Checked) ? ETipoTitular.Proveedor : ETipoTitular.Cliente;
            Fechas_GB.Enabled = !Proveedor_RB.Checked;
        }

        #endregion

        #region Buttons

        private void Familia_BT_Click(object sender, EventArgs e)
        {
            FamilySelectForm form = new FamilySelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _familia = form.Selected as FamiliaInfo;
                Familia_TB.Text = _familia.Nombre;
            }
        }

        private void Producto_BT_Click(object sender, EventArgs e)
        {
            ProductSelectForm form = new ProductSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _producto = form.Selected as ProductInfo;
                Producto_TB.Text = _producto.Nombre;
            }
        }

        #endregion
    }
}