using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports.Cobro;
using moleQule.Face;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class CobrosActionForm : Skin01.ActionSkinForm
    {
        #region Atributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "CobrosActionForm";
        public static Type Type { get { return typeof(CobrosActionForm); } }

        ClienteInfo _cliente = null;
        SerieInfo _serie = null;

        #endregion

        #region Factory Methods

        public CobrosActionForm()
            : this(null) { }

        public CobrosActionForm(Form parent)
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

            string filtro = GetFilterValues();
            PgMng.Grow();

            OutputInvoiceList facturas = OutputInvoiceList.GetList(conditions, false);
            PgMng.Grow();
            CobroFacturaList cobros = CobroFacturaList.GetList(conditions);
            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
            
            CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema, this.Text, filtro);
            InformeCobrosRpt rpt = reportMng.GetInformeCobrosReport(cobros, facturas);
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

