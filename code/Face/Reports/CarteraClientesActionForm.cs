using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports.Cliente;
using moleQule.Face;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class CarteraClientesActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "CarteraClientesActionForm";
        public static Type Type { get { return typeof(CarteraClientesActionForm); } }

        private ClienteInfo _cliente;
        SerieInfo _serie = null;

        #endregion

        #region Factory Methods

        public CarteraClientesActionForm()
            : this(null) {}

        public CarteraClientesActionForm(Form parent)
            : base(true, parent)
        {
            InitializeComponent();
            SetFormData();
        }

        #endregion

        #region Source

		public override void RefreshSecondaryData()
		{
			ETipoFacturas[] list = { ETipoFacturas.Todas, ETipoFacturas.Cobradas, ETipoFacturas.Pendientes };
			Datos_TiposFactura.DataSource = Library.Store.EnumText<ETipoFacturas>.GetList(list);
			TipoFactura_CB.SelectedItem = (long)ETipoFacturas.Todas;
			PgMng.Grow();

			Ordenar_CB.SelectedIndex = 0;
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

			filtro += TipoFactura_CB.Text + "; ";

			return filtro;
		}

		#endregion

        #region Actions

        protected override void PrintAction()
        {
            PgMng.Reset(4, 1, Face.Resources.Messages.RETRIEVING_DATA, this); 
            ClienteInfo cliente = TodosCliente_CkB.Checked ? null : _cliente;
            SerieInfo serie = TodosSerie_CkB.Checked ? null : _serie;

            FormatConfCarteraClientesReport conf = new FormatConfCarteraClientesReport();

			conf.tipo = (ETipoFacturas)(long)TipoFactura_CB.SelectedValue;
            conf.orden_ascendente = Ascendente_RB.Checked;
            conf.resumido = Resumido_RB.Checked;
			conf.verCobros = Detallado_RB.Checked;
            conf.campo_ordenacion = Ordenar_CB.SelectedItem.ToString();
            
            if (FInicial_DTP.Checked)
                conf.inicio = FInicial_DTP.Value;
            else
                conf.inicio = DateTime.MinValue;

            if (FFinal_DTP.Checked)
                conf.final = FFinal_DTP.Value;
            else
                conf.final = DateTime.MaxValue;

            string filtro = GetFilterValues();
            PgMng.Grow();

            ClienteList clientes = ClienteList.GetList(false);
            
            //Quitamos el cliente CONSUMO PROPIO por peticion del Balaños
            ClienteInfo cp = clientes.GetItemByProperty("Nombre", "CONSUMO PROPIO");
            if (cp != null) clientes.RemoveItem(cp.Oid);

            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

            ClienteReportMng reportMng = new ClienteReportMng(AppContext.ActiveSchema, this.Text, filtro);

            if (!TodosCliente_CkB.Checked)
            {
                InformeCarteraClientesRpt rpt = reportMng.GetCarteraClientesReport(_cliente, _serie, conf);
                
				PgMng.FillUp();
				ShowReport(rpt);
            }
            else
            {
                InformeCarteraClientesRpt rpt = reportMng.GetCarteraClientesReport(clientes, _serie, conf);
                
				PgMng.FillUp();
				ShowReport(rpt);
            }

            _action_result = DialogResult.Ignore;
        }

        #endregion

        #region Buttons

        private void Cliente_BT_Click(object sender, EventArgs e)
        {
            ClientSelectForm form = new ClientSelectForm(this);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _cliente = form.Selected as ClienteInfo;
                Cliente_TB.Text = _cliente.Codigo + " - " + _cliente.Nombre + " - " + _cliente.VatNumber;
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

        #region Events

        private void Todos_CKB_CheckedChanged(object sender, EventArgs e)
        {
            if (!TodosCliente_CkB.Checked)
            {
                Cliente_BT.Enabled = true;
                if (_cliente != null)
                    Cliente_TB.Text = _cliente.Codigo + " - " + _cliente.Nombre + " - " + _cliente.VatNumber;
            }
            else
            {
                Cliente_BT.Enabled = false;
                Cliente_TB.Text = "";
            }

			if (Detallado_RB.Checked) DetalladoSinCobros_RB.Checked = true;
			Detallado_RB.Enabled = !TodosCliente_CkB.Checked;
        }

        private void TodosSerie_GB_CheckedChanged(object sender, EventArgs e)
        {
            Serie_BT.Enabled = !TodosSerie_CkB.Checked;
        }

        #endregion
    }
}

