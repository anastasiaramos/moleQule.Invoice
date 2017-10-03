using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class InvoiceViewForm : InvoiceForm
	{
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 4; } }

        /// <summary>
        /// Se trata de la Factura actual y que se va a editar.
        /// </summary>
        private OutputInvoiceInfo _entity;

        public override OutputInvoiceInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        public InvoiceViewForm(long oid) 
			: this(oid, null) {}

        public InvoiceViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = OutputInvoiceInfo.Get(oid, true);
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);
            this.Impresion_GB.Enabled = true;
            foreach (Control ctl in Impresion_GB.Controls)
            {
                switch (ctl.GetType().Name)
                {
                    case "CheckBox":
                        ((CheckBox)ctl).Enabled = true;
                        break;
                    case "TextBox":
                        ((TextBox)ctl).Enabled = true;
                        ((TextBox)ctl).ReadOnly = false;
                        break;
                    case "ComboBox":
                        ((ComboBox)ctl).Enabled = true;
                        break;
                    case "Button":
                        ((Button)ctl).Enabled = true;
                        break;
                    default:
                        ctl.Enabled = true;
                        break;
                }
            }

            Agrupada_CkB.Enabled = false;
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
            IDManual_CKB.Visible = false;

            if (!_entity.Rectificativa)
            {
                if (!_entity.Albaran)
                    NFactura_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask();
                else
                    NFactura_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask() + "-\\C";
            }
            else
                NFactura_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask() + "-R";

			base.FormatControls();
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Lines_BS.DataSource = _entity.ConceptoFacturas;
            PgMng.Grow();

            if (_entity.OidCliente > 0)
                Client_BS.DataSource = ClienteInfo.Get(_entity.OidCliente, true);
			PgMng.Grow();

			if (_entity.OidSerie > 0)
			{
				_serie = SerieInfo.Get(_entity.OidSerie, false);
				Serie_TB.Text = _serie.Nombre;
				Nota_TB.Text = _serie.Cabecera;
			}
			PgMng.Grow();

			if (_entity.OidTransportista > 0)
			{
				_transporter = TransporterInfo.Get(_entity.OidTransportista, ETipoAcreedor.TransportistaDestino, false);
				Transportista_TB.Text = _transporter.Nombre;
			}
			PgMng.Grow();

            DiasPago_NTB.Text = _entity.DiasPago.ToString();
            Fecha_DTP.Value = _entity.Fecha;
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();            

            base.RefreshMainData();
        }

        protected override void HideComponents()
        {
            foreach (DataGridViewRow row in Lines_DGW.Rows)
                if ((row.DataBoundItem as OutputInvoiceLineInfo).IsKitComponent)
                    row.Visible = false;
        }

        protected override void SetReadOnlyCellsValue() {}

        #endregion

		#region Actions

		protected override void SaveAction() { _action_result = DialogResult.Cancel; }

		#endregion
	}
}

