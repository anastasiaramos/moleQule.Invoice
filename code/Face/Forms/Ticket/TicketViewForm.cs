using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class TicketViewForm : TicketForm
	{
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata de la Ticket actual y que se va a editar.
        /// </summary>
        private TicketInfo _entity;

        public override TicketInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        public TicketViewForm(long oid) 
			: this(oid, null) {}

        public TicketViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFView;
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = TicketInfo.Get(oid, true);
        }

        #endregion

        #region Layout & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
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

            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
            NTicketManual_CKB.Visible = false;

			base.FormatControls();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Datos_Concepto.DataSource = _entity.ConceptoTickets;
            PgMng.Grow();

			if (_entity.OidSerie > 0)
			{
				SerieInfo serie = SerieInfo.Get(_entity.OidSerie, false);
				Serie_TB.Text = serie.Nombre;				
			}
			PgMng.Grow();

            Fecha_DTP.Value = _entity.Fecha;
            Prevision_TB.Text = _entity.Prevision.ToShortDateString();            

            base.RefreshMainData();
        }

        protected override void HideComponentes()
        {
            foreach (DataGridViewRow row in Conceptos_DGW.Rows)
                if ((row.DataBoundItem as ConceptoTicketInfo).IsKitComponent)
                    row.Visible = false;
        }

        #endregion

		#region Validation & Format

		#endregion

		#region Actions

		protected override void SaveAction() { _action_result = DialogResult.Cancel; }

		#endregion

        #region Events

        #endregion
	}
}

