using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class InvoiceEditForm : InvoiceUIForm, IBackGroundLauncher
    {
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps; } }

		#endregion

        #region Factory Methods

        public InvoiceEditForm(long oid, Form parent)
            : base(oid, parent)
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

		protected override void GetFormSourceData(long oid)
		{
            _entity = OutputInvoice.Get(oid);
			_entity.BeginEdit();
		}

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
            base.FormatControls();

			Rectificativa_CkB.Enabled = (_entity.Conceptos.Count == 0);
            Agrupada_CkB.Enabled = false;
        }

        #endregion

		#region Actions

		public override void PrintObject()
        {
			_entity.LoadChilds(typeof(CobroFactura), false);
			base.PrintObject();
        }					

		#endregion
	}
}

