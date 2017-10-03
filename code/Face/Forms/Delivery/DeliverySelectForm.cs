using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using moleQule.Face;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class DeliverySelectForm : DeliveryMngForm
    {
        #region Factory Methods

        public DeliverySelectForm()
            : this(null, ETipoEntidad.Cliente) {}

        public DeliverySelectForm(Form parent, ETipoEntidad holderType)
            : this(parent, holderType, ETipoAlbaranes.Todos, 0, 0) {}

        public DeliverySelectForm(Form parent, ETipoEntidad holderType, ETipoAlbaranes deliveryType, long oidSerie, long oidHolder)
			: base(true, parent, holderType, deliveryType)
        {
            InitializeComponent();
			_view_mode = molView.Select;

            _oid_client = oidHolder;
            _oid_serie = oidSerie;

			_action_result = DialogResult.Cancel;
        }

        public DeliverySelectForm(Form parent, ETipoEntidad holderType, OutputDeliveryList list)
			: base(true, parent, holderType, ETipoAlbaranes.Todos, list)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        #endregion

        #region Actions

        protected override void DefaultAction() 
        {
			SeleccionaLinea();
            ExecuteAction(molAction.Select); 
        }

        public override void SelectObject()
        {
            Datos.MoveFirst();
            Datos.MoveLast();

			List<OutputDeliveryInfo> list = new List<OutputDeliveryInfo>();

            foreach (DataGridViewRow row in Tabla.Rows)
            {
				if (IsSelected(row)) list.Add(row.DataBoundItem as OutputDeliveryInfo);
            }

            _selected = list;
            _action_result = list.Count > 0 ? DialogResult.OK : DialogResult.Cancel;
        }

        public override void SelectAll()
        {
            Datos.MoveFirst();
            Datos.MoveLast();

			List<OutputDeliveryInfo> list = new List<OutputDeliveryInfo>();

            foreach (DataGridViewRow row in Tabla.Rows)
            {
				list.Add(row.DataBoundItem as OutputDeliveryInfo);
            }

            _selected = list;
            this.DialogResult = list.Count > 0 ? DialogResult.OK : DialogResult.Cancel;
            Close();
        }

        #endregion
    }
}
