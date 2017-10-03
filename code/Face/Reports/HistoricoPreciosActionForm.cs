using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Ventas;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class HistoricoPreciosActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

		public const string ID = "HistoricoPreciosActionForm";
		public static Type Type { get { return typeof(HistoricoPreciosActionForm); } }

		private ClienteInfo _cliente = null;
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

        public HistoricoPreciosActionForm() 
			: this(null) { }

		public HistoricoPreciosActionForm(Form parent)
			: this(true, parent) { }

		public HistoricoPreciosActionForm(bool isModal, Form parent)
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

        protected override void PrintAction()
        {
            PgMng.Reset(4, 1, Face.Resources.Messages.RETRIEVING_DATA, this); 

			Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions();
			conditions.Cliente = TodosCliente_CkB.Checked ? null : _cliente;
			conditions.Familia = TodosFamilia_CkB.Checked ? null : _familia;
			conditions.Producto = TodosProducto_CkB.Checked ? null : _producto;;
			conditions.FechaIni = FInicial_DTP.Checked ? FInicial_DTP.Value : DateTime.MinValue;;
			conditions.FechaFin = FFinal_DTP.Checked ? FFinal_DTP.Value : DateTime.MaxValue;
			conditions.Order = (Ascendente_RB.Checked) ? ListSortDirection.Ascending : ListSortDirection.Descending;

            string filtro = GetFilterValues();

			if (Cliente_RB.Checked)
            {
				VentasList ventas = VentasList.GetHistoricoPreciosClientesList(conditions);
				PgMng.Grow();

				CommonReportMng rptMng = new CommonReportMng(AppContext.ActiveSchema, this.Text, filtro);
				InformeHistoricoPreciosClientesRpt rpt = rptMng.GetInformeHistoricoPreciosClientesReport(ventas);
				PgMng.FillUp();

				ShowReport(rpt);
            }
            else
            {
				VentasList ventas = VentasList.GetHistoricoPreciosProductosList(conditions);
				PgMng.Grow();

				CommonReportMng rptMng = new CommonReportMng(AppContext.ActiveSchema, this.Text, filtro);
				InformeHistoricoPreciosProductosRpt rpt = rptMng.GetInformeHistoricoPreciosProductosReport(ventas);
				PgMng.FillUp();

				ShowReport(rpt);
            }
            
            _action_result = DialogResult.Ignore;
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

		private void Cliente_BT_Click(object sender, EventArgs e)
		{
			ClientSelectForm form = new ClientSelectForm(this);

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				_cliente = form.Selected as ClienteInfo;
				Cliente_TB.Text = _cliente.Nombre;
			}
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

		private void TodosCliente_CkB_CheckedChanged(object sender, EventArgs e)
		{
			Cliente_BT.Enabled = !TodosCliente_CkB.Checked;
		}

		private void Cliente_RB_CheckedChanged(object sender, EventArgs e)
		{
			_tipo = (Cliente_RB.Checked) ? ETipoTitular.Cliente : ETipoTitular.Proveedor;
			Fechas_GB.Enabled = Cliente_RB.Checked;
		}

		private void Proveedor_RB_CheckedChanged(object sender, EventArgs e)
		{
			_tipo = (Producto_RB.Checked) ? ETipoTitular.Proveedor : ETipoTitular.Cliente;
			Fechas_GB.Enabled = !Producto_RB.Checked;
		}

		#endregion

    }
}

