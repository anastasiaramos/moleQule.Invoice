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
	public partial class DeliveryViewForm : DeliveryForm
	{
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 4; } }

		private OutputDeliveryInfo _entity;

		public override OutputDeliveryInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        public DeliveryViewForm() 
			: this(-1, ETipoEntidad.Cliente, null) {}

		public DeliveryViewForm(long oid, ETipoEntidad holderType, Form parent)
			: base(oid, new object[1] { holderType}, true, parent)
        {
            InitializeComponent();
			SetView(molView.ReadOnly);
            SetFormData();
			_mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid, object[] parameters)
        {
			ETipoEntidad holder_type = (ETipoEntidad)parameters[0];
			_entity = OutputDeliveryInfo.Get(oid, holder_type, true);
		}

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);

            this.ExtraData_GB.Enabled = true;
            foreach (Control ctl in ExtraData_GB.Controls)
            {
                switch (ctl.GetType().Name)
                {
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

            Contado_CB.Enabled = false;
            Rectificativo_CKB.Enabled = false;
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
            IDManual_CKB.Visible = false;

            base.FormatControls();

            if (!_entity.Rectificativo)
            {
                if (!_entity.Contado)
                    NAlbaran_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask();
                else
                    NAlbaran_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask() + "-\\C";
            }
            else
                NAlbaran_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask() + "-R";

			Lineas_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
			Lines_BS.DataSource = _entity.Conceptos;
            PgMng.Grow();

            if (_entity.OidHolder > 0)
				Client_BS.DataSource = ClienteInfo.Get(_entity.OidHolder, false);
            PgMng.Grow();

			if (_entity.OidSerie > 0)
			{
				SerieInfo serie = SerieInfo.Get(_entity.OidSerie, false);
				Serie_TB.Text = serie.Nombre;
				Nota_TB.Text = serie.Cabecera;
			}
			PgMng.Grow();

			if (_entity.OidTransportista > 0)
			{
				TransporterInfo tr = TransporterInfo.Get(_entity.OidTransportista, ETipoAcreedor.TransportistaDestino, false);
				Transporter_TB.Text = tr.Nombre;
			}
			PgMng.Grow();

            DiasPago_NTB.Text = _entity.DiasPago.ToString();
            Fecha_DTP.Value = _entity.Fecha;
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();
            PgMng.Grow();

            base.RefreshMainData();
        }

        protected override void HideComponentes()
        {
            foreach (DataGridViewRow row in Lineas_DGW.Rows)
				if ((row.DataBoundItem as OutputDeliveryLineInfo).IsKitComponent)
                    row.Visible = false;
        }

        #endregion

		#region Validation & Format

		#endregion

		#region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        protected override void EditLineAction() {}

		#endregion

        #region Events

        #endregion
	}
}

