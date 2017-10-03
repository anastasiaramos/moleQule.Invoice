using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cash;

namespace moleQule.Face.Invoice
{
    public partial class CashCountForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "CashCountForm";
		public static Type Type { get { return typeof(CashCountForm); } }

        protected override int BarSteps { get { return base.BarSteps + 0; } }
		
        public virtual CierreCaja Entity { get { return null; } set { } }
        public virtual CierreCajaInfo EntityInfo { get { return null; } }
		
        #endregion

        #region Factory Methods

        public CashCountForm() : this(-1) {}

        public CashCountForm(long oid) : this(oid, true, null) {}

		public CashCountForm(bool isModal) : this(-1, isModal, null) {}

        public CashCountForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

        #region Layout

        public override void FitColumns()
        {
            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Observaciones.Tag = 0.6;
            Concepto.Tag = 0.4;

            cols.Add(Concepto);
            cols.Add(Observaciones);

            ControlsMng.MaximizeColumns(CashLine_DGW, cols);
        }

        public override void FormatControls()
        {
			SetGridFormat(CashLine_DGW);

            MaximizeForm(new Size(0, 0));
			base.FormatControls();
        }
		
		#endregion

        #region Validation & Format

        #endregion

        #region Print

        public override void PrintObject()
        {
            CashReportMng reportMng = new CashReportMng(AppContext.ActiveSchema);
            
            ReportViewer.SetReport(reportMng.GetDetailReport(EntityInfo));
            ReportViewer.ShowDialog();
        }

        #endregion

        #region Actions

        protected override void PrintAction()
        {
            PrintObject();
        }

        #endregion

        #region Events

        private void Datos_DataSourceChanged(object sender, EventArgs e)
        {
            //SetDependentControlSource(ID_GB.Name);
        }
		
        #endregion
    }
}
