using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class TraspasoForm : Skin01.ItemMngSkinForm
    {

        #region Attributes & Properties
		
        public const string ID = "TraspasoForm";
		public static Type Type { get { return typeof(TraspasoForm); } }

        protected override int BarSteps { get { return base.BarSteps + 0; } }
		
        public virtual Traspaso Entity { get { return null; } set { } }
        public virtual TraspasoInfo EntityInfo { get { return null; } }

		
        #endregion

        #region Factory Methods

        public TraspasoForm() 
			: this(-1) {}

        public TraspasoForm(long oid) 
			: this(oid, true, null) {}

		public TraspasoForm(bool is_modal) 
		: this(-1, is_modal, null) {}

        public TraspasoForm(long oid, bool is_modal, Form parent)
            : base(oid, is_modal, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout

        /// <summary>Da formato a los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();
        }

		#endregion
		
		#region Source

        /// <summary>
        /// Asigna el objeto principal al origen de datos principal
		/// y las listas hijas a los origenes de datos correspondientes
        /// </summary>
        protected override void RefreshMainData()
        {
			
        }

        /// <summary>
        /// Asigna los datos a los origenes de datos secundarios
        /// </summary>
        public override void RefreshSecondaryData()
		{
			
        }
		
		#endregion

        #region Validation & Format

        #endregion

        #region Print

        //public override void PrintObject()
        //{
        //    TraspasoReportMng reportMng = new TraspasoReportMng(AppContext.ActiveSchema);
        //    ReportViewer.SetReport(reportMng.GetReport(EntityInfo);
        //    ReportViewer.ShowDialog();
        //}

        #endregion

        #region Actions

        #endregion

        #region Events

        private void Datos_DataSourceChanged(object sender, EventArgs e)
        {
            //SetDependentControlSource(ID_GB.Name);
        }

		
        #endregion

    }
}
