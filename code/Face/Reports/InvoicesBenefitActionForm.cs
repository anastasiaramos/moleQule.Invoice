using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Face;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class InvoicesBenefitActionForm : Skin01.ActionSkinForm
    {
        #region Atributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "InvoicesBenefitActionForm";
        public static Type Type { get { return typeof(InvoicesBenefitActionForm); } }

        ClienteInfo _cliente = null;
        SerieInfo _serie = null;

        #endregion

        #region Factory Methods

        public InvoicesBenefitActionForm()
            : this(null) { }

        public InvoicesBenefitActionForm(Form parent)
            : base(true, parent)
        {
            InitializeComponent();
            SetFormData();
        }

        #endregion

        #region Business Methods

        private string GetFilterValues()
        {
            string filtro = string.Empty;

            if (!TodosCliente_CkB.Checked)
                filtro += "Cliente " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _cliente.Nombre + "; ";

            if (!TodosSerie_CkB.Checked)
                filtro += "Serie " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _serie.Nombre + "; ";

            if (FInicial_DTP.Checked)
                filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.GreaterOrEqual) + " " + FInicial_DTP.Value.ToShortDateString() + "; ";

            if (FFinal_DTP.Checked)
                filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.LessOrEqual) + " " + FFinal_DTP.Value.ToShortDateString() + "; ";

            if (MedioPago_CB.SelectedIndex != (long)EValue.Empty)
                filtro += "Medio de pago " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + MedioPago_CB.Text + "; ";

            return filtro;
        }

        #endregion

        #region Layout & Source

        public override void RefreshSecondaryData ()
        {
			Datos_MedioPago.DataSource = Library.Common.EnumText<EMedioPago>.GetList(false, true);
            MedioPago_CB.SelectedValue = (long)EMedioPago.Todos;
            PgMng.Grow();
        }

        #endregion

        #region Actions

        protected override void PrintAction()
        {
            PgMng.Reset(5, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions();

            conditions.Cliente = TodosCliente_CkB.Checked ? null : _cliente;
            conditions.Serie = TodosSerie_CkB.Checked ? null : _serie;
            conditions.MedioPago = MedioPago_CB.SelectedValue != null ? (EMedioPago)(long)MedioPago_CB.SelectedValue : EMedioPago.Todos;
            conditions.FechaIni = FInicial_DTP.Checked ? FInicial_DTP.Value : DateTime.MinValue;
            conditions.FechaFin = FFinal_DTP.Checked ? FFinal_DTP.Value : DateTime.MaxValue;

            string filter = GetFilterValues();
            PgMng.Grow();

            OutputInvoiceList in_invoices = OutputInvoiceList.GetList(conditions, false);
            PgMng.Grow();
            ProductList products = ProductList.GetList(false);
            PgMng.Grow();
            ExpedienteList expedients = ExpedienteList.GetList(false);
            PgMng.FillUp();

            PgMng.Reset(in_invoices.Count + 2, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

            Dictionary<long, ClientProductList> client_products = new Dictionary<long, ClientProductList>();

            foreach (OutputInvoiceInfo in_invoice in in_invoices)
            {
                in_invoice.LoadChilds(typeof(OutputInvoiceLine), false);

                decimal invoice_cost = 0;

                foreach (OutputInvoiceLineInfo line in in_invoice.ConceptoFacturas)
                {
                    ProductInfo product = products.GetItem(line.OidProducto);

                    if (product.BeneficioCero)
                    {
                        if (!client_products.ContainsKey(in_invoice.OidCliente))
                        {
                            client_products.Add(in_invoice.OidCliente, ClientProductList.GetByClientList(in_invoice.OidCliente, false));
#if TRACE
    AppControllerBase.AppControler.Timer.Record("Productos del Cliente");
#endif
                        }
                        ProductoClienteInfo product_client = client_products[in_invoice.OidCliente].GetByProducto(line.OidProducto);

                        if (product_client != null)
                            invoice_cost += product_client.PrecioCompra;
                        else
                            invoice_cost += line.Subtotal;                           
                    }
                    else
                    {
                        if (line.OidPartida != 0)
                        {
                            ExpedientInfo expedient = expedients.GetItem(line.OidExpediente);

                            if (expedient != null)
                            {
                                if (expedient.Partidas == null)
                                {
                                    expedient.LoadExpenses(Estimated_CB.Checked);                                   
#if TRACE
    AppControllerBase.AppControler.Timer.Record("Gastos del Expediente");
#endif
                                }

                                BatchInfo batch = expedient.Partidas.GetItem(line.OidPartida);
                                invoice_cost += line.CantidadKilos * batch.CosteNetoKg;
                            }
                            else
                            {
                                invoice_cost += line.CantidadKilos * product.PrecioCompra;
                            }
                        }
                        else
                        {
                            invoice_cost += (product != null) ? line.CantidadKilos * product.PrecioCompra : 0;
                        }
                    }
                }

                in_invoice.PrecioCoste = invoice_cost;
                in_invoice.Beneficio = Decimal.Round(in_invoice.BaseImponible - in_invoice.PrecioCoste, 2);
                in_invoice.PBeneficio = in_invoice.PrecioCoste != 0 ? Decimal.Round(((in_invoice.BaseImponible - in_invoice.PrecioCoste) * 100)/ in_invoice.PrecioCoste, 2) : 0;

                PgMng.Grow();
            }

            client_products.Clear();

            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

            OutputInvoiceReportMng reportMng = new OutputInvoiceReportMng(AppContext.ActiveSchema, this.Text, filter);
            ReportClass rpt = reportMng.GetBenefitsReport(in_invoices);
            PgMng.FillUp();

			ShowReport(rpt);

            _action_result = DialogResult.Ignore;
        }

        #endregion

        #region Events

        private void TodosCliente_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Cliente_BT.Enabled = !TodosCliente_CkB.Checked;
        }

        private void TodosSerie_GB_CheckedChanged(object sender, EventArgs e)
        {
            Serie_BT.Enabled = !TodosSerie_CkB.Checked;
        }

        #endregion

        #region Buttons

        private void Cliente_BT_Click(object sender, EventArgs e)
        {
            ClientSelectForm form = new ClientSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _cliente = form.Selected as ClienteInfo;
                Cliente_TB.Text = _cliente.Nombre;
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

        #endregion
    }
}

