using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
	public partial class TicketAddForm : TicketUIForm, IBackGroundLauncher
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

		private OutputDeliveryInfo _albaran = null;

        #endregion

        #region Factory Methods

        public TicketAddForm(Form parent) 
			: base(parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		public TicketAddForm(OutputDeliveryInfo delivery, Form parent)
			: this(parent)
		{
			SetSerie(SerieInfo.Get(delivery.OidSerie, false), false);
            _albaran = delivery;
		}

		protected override void GetFormSourceData()
		{
			_entity = Ticket.New();
			_entity.BeginEdit();
		}

		#endregion

		#region Layout & Source

		protected override void RefreshMainData()
		{
			if (Library.Invoice.ModulePrincipal.GetDefaultSerieSetting() > 0)
				SetSerie(SerieInfo.Get(Library.Invoice.ModulePrincipal.GetDefaultSerieSetting(), false), true);
			PgMng.Grow();

			base.RefreshMainData();
		}

		#endregion	
		
		#region Actions

		private bool AddAlbaran(OutputDeliveryInfo albaran)
		{
			List<OutputDeliveryInfo> list = new List<OutputDeliveryInfo>();
			list.Add(albaran);

			_results = list;

			DoAddAlbaran(null);

			if (Result == BGResult.OK)
			{
				Serie_BT.Enabled = false;
				Datos.ResetBindings(false);
			}

			return false;
		}

		#endregion

		#region Events

		private void Rectificativa_CkB_CheckedChanged(object sender, EventArgs e)
        {
            /*Entity.Rectificativa = Rectificativa_CkB.Checked;
            if (Entity.Rectificativa)
            {
                AlbaranContado_CB.Checked = false;
                AlbaranContado_CB.CheckState = CheckState.Unchecked;
            }

            AlbaranContado_CB.Enabled = !Entity.Rectificativa;
            _entity.GetNewCode();*/
        }

        private void TicketAddForm_Shown(object sender, EventArgs e)
        {
            if (_albaran != null)
            {
                AddAlbaran(_albaran);
                Datos_Concepto.ResetBindings(true);
                Conceptos_DGW.Refresh();
            }

        }

        #endregion
    }
}

