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
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports.Delivery;

namespace moleQule.Face.Invoice
{
    public partial class DetalleAlbaranesActionForm : Skin01.ActionSkinForm
    {
        #region Attributes $ Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public const string ID = "DetalleAlbaranesActionForm";
        public static Type Type { get { return typeof(DetalleAlbaranesActionForm); } }
        
        private ClienteInfo _cliente;
        private SerieInfo _serie;
		private OutputDeliveryList _lista;
        private ETipoAlbaranes _tipo;
        private string _titulo = "Listado de Albaranes";
        private string _filtro = string.Empty;

        public ClienteInfo Cliente { get { return _cliente; } }
        public String Titulo { get { return _titulo; } set { _titulo = value; } }
        public String Filtro { get { return _filtro; } set { _filtro = value; } }

        #endregion

        #region Factory Methods

		public DetalleAlbaranesActionForm(OutputDeliveryList list)
			: this(true, list) {}

		public DetalleAlbaranesActionForm(bool IsModal, OutputDeliveryList list)
			: base(IsModal)
        {
            this.InitializeComponent();
            base.SetFormData();
            this.Text = "Informe: Detalle de Albaranes";
            this._lista = list;
        }

        #endregion

        #region Layout & Source

        public override void RefreshSecondaryData()
        {
            this.Datos_Tipos.DataSource = Enum.GetNames(typeof(ETipoAlbaranes));
            
            this.TipoAlbaran_CB.SelectedItem = ((ETipoAlbaranes)4).ToString();
            PgMng.Grow();
            
            this.FInicial_DTP.Value = DateTime.Today;
            this.FFinal_DTP.Value = DateTime.Today;
        }

        #endregion

        #region Buttons

        public void DoSubmit() { SubmitAction(); }

        protected override void SubmitAction()
        {
            _tipo = (ETipoAlbaranes)Enum.Parse(typeof(ETipoAlbaranes), this.TipoAlbaran_CB.Text);
            long serie = TodosSerie_CkB.Checked ? 0 : _serie.Oid;
			OutputDeliveryList lista = null;

            switch (this._tipo)
            {
                case ETipoAlbaranes.Todos:
                case ETipoAlbaranes.Lista:
					lista = OutputDeliveryList.GetList(true, 0, ETipoEntidad.Cliente, serie, this.FInicial_DTP.Value, this.FFinal_DTP.Value);
                    break;

                case ETipoAlbaranes.Facturados:
					lista = OutputDeliveryList.GetFacturados(true, 0, serie, this.FInicial_DTP.Value, this.FFinal_DTP.Value);
                    break;

                case ETipoAlbaranes.NoFacturados:
					lista = OutputDeliveryList.GetNoFacturados(true, 0, serie, this.FInicial_DTP.Value, this.FFinal_DTP.Value);
                    break;

				case ETipoAlbaranes.Agrupados:
					lista = OutputDeliveryList.GetNoFacturadosAgrupados(serie, this.FInicial_DTP.Value, this.FFinal_DTP.Value, true);
                    break;
            }

            if (this.Listado_RB.Checked)
            {
                if (this.Detallado_RB.Checked)
                {
                    this.PrintDetail(this._lista);
                }
                else
                {
                    this.PrintList(this._lista);
                }
            }
            else if (this.Detallado_RB.Checked)
            {
                this.PrintDetail(lista);
            }
            else
            {
                this.PrintList(lista);
            }

            _action_result = DialogResult.OK;
        }

        protected void PrintDetail(OutputDeliveryList lista)
        {
			PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);

            ClienteList clientes = ClienteList.GetList(false);
			PgMng.Grow();

            OutputDeliveryReportMng rptMng = new OutputDeliveryReportMng(AppContext.ActiveSchema, this.Text, string.Empty);
			ReportClass report = rptMng.GetDetailListReport(lista, clientes, this._tipo, this.FInicial_DTP.Value, this.FFinal_DTP.Value);
			PgMng.FillUp();

			ShowReport(report);
        }

        protected void PrintList(OutputDeliveryList lista)
        {
			PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);
            
			ClienteList clientes = ClienteList.GetList(false);
			PgMng.Grow();

            OutputDeliveryReportMng rptMng = new OutputDeliveryReportMng(AppContext.ActiveSchema, Titulo, Filtro);
            
			ReportClass report = rptMng.GetListReport(lista, clientes);
			PgMng.FillUp();

			ShowReport(report);
        }
        
        #endregion

        #region Events

        private void Cliente_BT_Click(object sender, EventArgs e)
        {
            ClientSelectForm form = new ClientSelectForm(this);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _cliente = form.Selected as ClienteInfo;
                Cliente_TB.Text = Cliente.Codigo + " - " + Cliente.Nombre + " - " + Cliente.VatNumber;
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

        private void Condiciones_RB_CheckedChanged(object sender, EventArgs e)
        {
            this.Condiciones_GB.Enabled = this.Condiciones_RB.Checked;
            this.Detallado_RB.Enabled = this.Condiciones_RB.Checked;
        }

        private void Listado_RB_CheckedChanged(object sender, EventArgs e)
        {
            this.Condiciones_GB.Enabled = !this.Listado_RB.Checked;
            this.Detallado_RB.Enabled = !this.Listado_RB.Checked;
        }

        private void TodosCliente_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Cliente_BT.Enabled = !TodosCliente_CkB.Checked;
        }

        private void TodosSerie_GB_CheckedChanged(object sender, EventArgs e)
        {
            Serie_BT.Enabled = !TodosSerie_CkB.Checked;
        }

        #endregion
    }
}

