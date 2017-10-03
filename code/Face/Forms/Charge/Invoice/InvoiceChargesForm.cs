using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Hipatia;

namespace moleQule.Face.Invoice
{
    public partial class InvoiceChargesForm : Skin04.ItemMngSkinForm
    {
        #region Attributes & Properties

        public override Type EntityType { get { return typeof(Cliente); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected OutputInvoiceInfo _entity;
		public virtual OutputInvoiceInfo Entity { get { return _entity; } set { _entity = value; } }

        #endregion

        #region Factory Methods

        public InvoiceChargesForm()
			: this(null) { }

        public InvoiceChargesForm(Form parent) 
			: this(-1, parent) { }

        public InvoiceChargesForm(long oid, Form parent)
			: this(oid, new object[1] { null },  parent) { }

		public InvoiceChargesForm(OutputInvoiceInfo source, Form parent)
			: this(-1, new object[1] { source }, parent) {}

		protected InvoiceChargesForm(long oid, object[] parameters, Form parent)
			: base(oid, parameters, true, parent)
		{
			InitializeComponent();
			SetFormData();
		}

		protected override void GetFormSourceData(object[] parameters) { GetFormSourceData(-1, parameters);	}

		protected override void GetFormSourceData(long oid, object[] parameters)
		{
			if (parameters[0] == null)
				_entity = OutputInvoiceInfo.Get(oid);
			else
				_entity = (OutputInvoiceInfo)(parameters[0]);

			_entity.LoadChilds(typeof(CobroFactura), false);
		}

        #endregion

        #region Authorization

        #endregion

        #region Layout

        public override void FitColumns()
        {
            CurrencyManager cm;
            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

            cm = (CurrencyManager)BindingContext[InvoiceCharges_DGW.DataSource];
            cm.SuspendBinding();

            Observaciones.Tag = 1;

			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(InvoiceCharges_DGW, cols);
        }

        public override void FormatControls()
        {
            base.FormatControls();

			Cancel_TI.Visible = false;
			Print_TI.Visible = false;
			Cancel_BT.Visible = false;			
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
					HideAction(molAction.CustomAction4);
					HideAction(molAction.ShowDocuments);
					break;
			}
		}

        #endregion

		#region Source

		protected override void RefreshMainData()
		{
			Datos.DataSource = _entity;
			PgMng.Grow();

			InvoiceCharge_BS.DataSource = _entity.CobroFacturas;
			PgMng.Grow();

			base.RefreshMainData();
		}

		#endregion

		#region Print

		public override void PrintObject()
        {
            //ReportMng reportMng = new ReportMng(AppContext.ActiveSchema);
            //ReportViewer.SetReport(reportMng.GetClienteReport(EntityInfo));
            //ReportViewer.ShowDialog();
        }

        #endregion

        #region Actions

		protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Buttons

        private void AddCharge_TI_Click(object sender, EventArgs e)
        {
			ClienteInfo cliente = ClienteInfo.Get(_entity.OidCliente, true);
			ChargeSummary item = ChargeSummary.Get(cliente);
			CobroEditForm form = new CobroEditForm(cliente.Oid, item, null, this);
			form.ShowDialog(this);
        }

		private void EditCharge_TI_Click(object sender, EventArgs e)
		{
			ClienteInfo cliente = ClienteInfo.Get(_entity.OidCliente, true);
			ChargeSummary item = ChargeSummary.Get(cliente);
			CobroEditForm form = new CobroEditForm(cliente.Oid, item, null, this);
			form.ShowDialog(this);
		}

        private void DeleteCharge_TI_Click(object sender, EventArgs e)
        {
			ClienteInfo cliente = ClienteInfo.Get(_entity.OidCliente, true);
			ChargeSummary item = ChargeSummary.Get(cliente);
			CobroEditForm form = new CobroEditForm(cliente.Oid, item, null, this);
			form.ShowDialog(this);
        }

        #endregion

        #region Events

        #endregion
    }
}

