using System;
using System.Windows.Forms;

using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
	public partial class TicketEditForm : TicketUIForm, IBackGroundLauncher
    {
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 1; } }

		#endregion

        #region Factory Methods

        public TicketEditForm(long oid, Form parent)
            : base(oid, parent)
		{
			InitializeComponent();
            if (_entity != null) SetFormData();
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

		protected override void GetFormSourceData(long oid)
		{
			_entity = Ticket.Get(oid);
			_entity.BeginEdit();
		}

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            base.FormatControls();
            
            Tipo_BT.Enabled = (_entity.ConceptoTickets.Count == 0);
        }

		protected override void RefreshMainData()
		{
			if (_entity.OidSerie > 0)
				SetSerie(SerieInfo.Get(_entity.OidSerie, false), false);
			PgMng.Grow();

			base.RefreshMainData();
		}

        #endregion

	}
}

