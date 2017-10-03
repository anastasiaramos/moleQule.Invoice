using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class CobroFacturaEditForm : CobroFacturaUIForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

		public new const string ID = "CobroFacturaEditForm";
        public new static Type Type { get { return typeof(CobroFacturaEditForm); } }

        private bool _locked = false;

        #endregion

        #region Factory Methods

        public CobroFacturaEditForm(Cliente client, Charge charge, bool locked, Form parent)
            : base(true, client, parent)
        {
            InitializeComponent();
            
            _locked = locked;
            _entity = charge;

            SetFormData();
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            if (Facturas_DGW == null) return;

            if ((_entity.EMedioPago == EMedioPago.CompensacionFactura) ||
				(_entity.EMedioPago == EMedioPago.Efectivo))
                MedioPago_BT.Enabled = false;
            else
				MedioPago_BT.Enabled = !_locked;

            base.FormatControls();
        }

        protected override void RefreshMainData()
        {
			Datos_Facturas.DataSource = OutputInvoiceList.GetByCobroAndNoCobradasByClienteList(_entity.Oid, _cliente.Oid, true).GetSortedList();
            PgMng.Grow();

			base.RefreshMainData();
        }

        #endregion

        #region Actions

        protected override void SubmitAction()
        {
            if (_entity.EMedioPago == EMedioPago.Seleccione)
            {
                MessageBox.Show(Resources.Messages.NO_MEDIO_PAGO_SELECTED);
                _action_result = DialogResult.Ignore;
                return;
            }
            
            if (!_locked)
                if (!Asignar()) return;

            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected override void CancelAction()
        {
            _entity.CancelEdit();
            _action_result = DialogResult.Cancel;
        }

        #endregion 
    }
}

