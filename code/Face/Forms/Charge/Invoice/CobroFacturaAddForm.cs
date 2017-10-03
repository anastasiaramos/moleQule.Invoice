
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class CobroFacturaAddForm : CobroFacturaUIForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

		public new const string ID = "CobroFacturaAddForm";
        public new static Type Type { get { return typeof(CobroFacturaAddForm); } }

        #endregion

        #region Factory Methods

        public CobroFacturaAddForm(Cliente cliente, Form parent)
            : this(true, cliente, parent) { }
                    
        public CobroFacturaAddForm(bool is_modal, Cliente cliente, Form parent)
            : base(is_modal, cliente, parent)
        {
            InitializeComponent();

            SetFormData(cliente);
        }
        
        protected void SetFormData(Cliente source)
        {
            _entity = source.Cobros.NewItem(source, ETipoCobro.Cliente);
            _entity.CopyFrom(source.GetInfo(false));
            
            base.SetFormData();
        }

        #endregion

        #region Layout & Source

        protected override void RefreshMainData()
        {
            Datos_Facturas.DataSource = OutputInvoiceList.GetNoCobradasByClienteList(_cliente.Oid, true).GetSortedList();
            PgMng.Grow();

			base.RefreshMainData();
        }

        #endregion

        #region Business Methods

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

            if (!Asignar()) return;

            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected override void CancelAction()
        {
            _cliente.Cobros.Remove(_entity.Oid);

            _entity.CancelEdit();
            _action_result = DialogResult.Cancel;
        }

        #endregion 
    }
}

