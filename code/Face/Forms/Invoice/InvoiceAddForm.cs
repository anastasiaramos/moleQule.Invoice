using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class InvoiceAddForm : InvoiceUIForm, IBackGroundLauncher
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

		private OutputDeliveryInfo _out_delivery = null;

        #endregion

        #region Factory Methods

        public InvoiceAddForm(Form parent) 
			: base(parent)
        {
            InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		public InvoiceAddForm(ClienteInfo client, OutputDeliveryInfo delivery, Form parent)
			: this(parent)
		{
			SetClient(client);
			SetSerie(SerieInfo.Get(delivery.OidSerie, false), true);
			_entity.AlbaranContado = delivery.Contado;
			_entity.Rectificativa = delivery.Rectificativo;
            _out_delivery = delivery;
		}

		protected override void GetFormSourceData()
		{
            _entity = OutputInvoice.New();
			_entity.BeginEdit();
		}

		#endregion

		#region Layout & Source

		protected override void RefreshMainData()
		{
			base.RefreshMainData();

			_entity.PIRPF = _company.PIRPF;
		}

		#endregion	
		
		#region Actions

		private bool AddAlbaran(OutputDeliveryInfo outDelivery)
		{
			List<OutputDeliveryInfo> list = new List<OutputDeliveryInfo>();
			list.Add(outDelivery);

			_results = list;

			DoAddAlbaran(null);

			if (Result == BGResult.OK)
			{
				Serie_BT.Enabled = false;
				Datos.ResetBindings(false);
			}

			if ((_entity.AlbaranContado) && (_entity.Conceptos.Count < 0)) Agrupada_CkB.Enabled = false;

			return false;
		}

		protected override void AmendmentInvoiceAction()
		{
			Entity.Rectificativa = Rectificativa_CkB.Checked;
			if (Entity.Rectificativa)
			{
				Agrupada_CkB.Checked = false;
				Agrupada_CkB.CheckState = CheckState.Unchecked;
			}

			Agrupada_CkB.Enabled = !Entity.Rectificativa;
			_entity.GetNewCode();
		}

		#endregion

        #region Events

        private void FacturaAddForm_Shown(object sender, EventArgs e)
        {
            if (_out_delivery != null)
            {
                AddAlbaran(_out_delivery);
                Lines_BS.ResetBindings(true);
                Lines_DGW.Refresh();
            }

        }

        #endregion
    }
}

