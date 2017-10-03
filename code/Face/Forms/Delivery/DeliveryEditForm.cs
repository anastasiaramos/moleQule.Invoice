using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class DeliveryEditForm : DeliveryUIForm
    {
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 2; } }

		#endregion

        #region Factory Methods

        public DeliveryEditForm()
            : this(-1, null) {}

		public DeliveryEditForm(long oid, Form parent)
			: this(oid, ETipoEntidad.Cliente, parent) {}

		public DeliveryEditForm(long oid, ETipoEntidad holderType, Form parent)
			: base(oid, new object[1] { holderType }, true, parent)
		{
			InitializeComponent();
            if (_entity != null) SetFormData();            
			_mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();

			base.DisposeForm();
		}

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
			ETipoEntidad holder_type = (ETipoEntidad)parameters[0];

			_entity = OutputDelivery.Get(oid, holder_type);			
			_entity.BeginEdit();
		}

        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();

            Contado_CB.Enabled = false;
            Rectificativo_CKB.Enabled = false;
        }

		#endregion

		#region Source
		
		protected override void RefreshMainData()
        {
			if (_entity.OidSerie > 0)
				SetSerie(SerieInfo.Get(_entity.OidSerie, false), false);
			PgMng.Grow();

			if (_entity.OidTransportista > 0)
				SetTransportista(TransporterInfo.Get(_entity.OidTransportista, ETipoAcreedor.TransportistaDestino, false));
			PgMng.Grow();

            base.RefreshMainData();
        }

        #endregion
	}
}

