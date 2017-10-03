using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports.Ventas;
using moleQule.Face;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class SalesActionForm : Skin01.ActionSkinForm
    {
        #region Atributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "SalesActionForm";
        public static Type Type { get { return typeof(SalesActionForm); } }

        ProductInfo _product = null;
        ClienteInfo _client = null;
        ExpedientInfo _expedient = null;
        SerieInfo _serie = null;

        #endregion

        #region Business Methods

        private string GetFilterValues()
        {
            string filtro = string.Empty;

            if (!TodosCliente_CkB.Checked)
                filtro += "Cliente " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _client.Nombre + "; ";

            if (!TodosProducto_CkB.Checked)
                filtro += "Producto " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _product.Nombre + "; ";
            else
                filtro += "Tipo Producto " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + TipoProducto_CB.Text + "; ";

            if (!TodosSerie_CkB.Checked)
                filtro += "Serie " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _serie.Nombre + "; ";

            if (!TodosExpediente_CkB.Checked)
                filtro += "Expediente " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _expedient.Codigo + "; ";
            else
                filtro += "Tipo Expediente " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + TipoExpediente_CB.Text + "; ";            

            if (FInicial_DTP.Checked)
                filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.GreaterOrEqual) + " " + FInicial_DTP.Value.ToShortDateString() + "; ";

            if (FFinal_DTP.Checked)
                filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.LessOrEqual) + " " + FFinal_DTP.Value.ToShortDateString() + "; ";

            return filtro;
        }

        #endregion

        #region Factory Methods

        public SalesActionForm()
            : this(null) { }

        public SalesActionForm(Form parent)
            : base(true, parent)
        {
            InitializeComponent();
            SetFormData();
        }

        #endregion

        #region Layout & Source

        public override void RefreshSecondaryData()
        {
            Datos_TiposExp.DataSource = Library.Store.EnumText<ETipoExpediente>.GetList(false);
            TipoExpediente_CB.SelectedItem = ComboBoxSourceList.Get(Datos_TiposExp.DataSource, (long)ETipoExpediente.Todos);
            PgMng.Grow();

            Datos_TiposPro.DataSource = Library.Store.EnumText<ETipoProducto>.GetList(false);
            TipoExpediente_CB.SelectedItem = ComboBoxSourceList.Get(Datos_TiposPro.DataSource, (long)ETipoProducto.Todos);
            PgMng.Grow();
        }

        #endregion

        #region Actions

        protected override void PrintAction()
        {
            PgMng.Reset(4, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions();

            conditions.Cliente = TodosCliente_CkB.Checked ? null : _client;
            conditions.Producto = TodosProducto_CkB.Checked ? null : _product;
            conditions.TipoProducto = TodosProducto_CkB.Checked ? (ETipoProducto)(long)TipoProducto_CB.SelectedValue : ETipoProducto.Todos;
            conditions.Serie = TodosSerie_CkB.Checked ? null : _serie;
            conditions.Expediente = TodosExpediente_CkB.Checked ? null : _expedient;
            conditions.TipoExpediente = TodosExpediente_CkB.Checked ? (ETipoExpediente)(long)TipoExpediente_CB.SelectedValue : ETipoExpediente.Todos;
            conditions.FechaIni = FInicial_DTP.Checked ? FInicial_DTP.Value : DateTime.MinValue;
            conditions.FechaFin = FFinal_DTP.Checked ? FFinal_DTP.Value : DateTime.MaxValue;

            bool detalle = TipoDetallado_RB.Checked;

            string filtro = GetFilterValues();
            PgMng.Grow();

			CommonReportMng reportMng = new CommonReportMng(AppContext.ActiveSchema, this.Text, filtro);

            if (Cliente_RB.Checked)
            {
                VentasList list = VentasList.GetListByCliente(conditions, detalle);

                PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

                InformeVentasClientesRpt rpt = reportMng.GetVentasClientesReport(list, detalle);
                PgMng.FillUp();

				ShowReport(rpt);
            }
            else if (Producto_RB.Checked)
            {
                VentasList list = VentasList.GetListByProducto(conditions, detalle);

                PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

                InformeVentasProductosRpt rpt = reportMng.GetVentasProductosReport(list, detalle);
                PgMng.FillUp();

				ShowReport(rpt);
            }
            else
            {
                VentasList list = VentasList.GetListByExpediente(conditions, detalle);
                PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
                
                InformeVentasExpedientesRpt rpt = reportMng.GetVentasExpedienteReport(list, detalle);
                PgMng.FillUp();

				ShowReport(rpt);
            }

            _action_result = DialogResult.Ignore;
        }

        #endregion

        #region Events

        private void TodosCliente_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Cliente_BT.Enabled = !TodosCliente_CkB.Checked;
        }

        private void TodosProducto_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Producto_BT.Enabled = !TodosProducto_CkB.Checked;
            TipoProducto_CB.Enabled = TodosProducto_CkB.Checked;
            TipoProducto_CB.SelectedValue = !TodosProducto_CkB.Checked ? (long)ETipoProducto.Todos : TipoProducto_CB.SelectedValue;
        }

        private void TodosSerie_GB_CheckedChanged(object sender, EventArgs e)
        {
            Serie_BT.Enabled = !TodosSerie_CkB.Checked;
        }

        private void TodosExpediente_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Expediente_BT.Enabled = !TodosExpediente_CkB.Checked;
            TipoExpediente_CB.Enabled = TodosExpediente_CkB.Checked;
            TipoExpediente_CB.SelectedValue = !TodosExpediente_CkB.Checked ? (long)ETipoExpediente.Todos : TipoExpediente_CB.SelectedValue;
        }

        #endregion

        #region Buttons

        private void Cliente_BT_Click(object sender, EventArgs e)
        {
            ClientSelectForm form = new ClientSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _client = form.Selected as ClienteInfo;
                Cliente_TB.Text = _client.Nombre;
            }
        }

        private void Producto_BT_Click(object sender, EventArgs e)
        {
            ProductSelectForm form = new ProductSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _product = form.Selected as ProductInfo;
                Producto_TB.Text = _product.Nombre;
            }
        }

        private void Serie_BT_Click(object sender, EventArgs e)
        {
            SerieSelectForm form = new SerieSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _serie = form.Selected as SerieInfo;
                Serie_TB.Text = _serie.Nombre;
            }
        }

        private void Expediente_BT_Click(object sender, EventArgs e)
        {
            ExpedienteSelectForm form = new ExpedienteSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _expedient = form.Selected as ExpedientInfo;
                Expediente_TB.Text = _expedient.Codigo;
            }
        }

        #endregion
    }
}

