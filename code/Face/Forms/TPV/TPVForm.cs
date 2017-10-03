using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Ticket;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class TPVForm : Skin01.ItemMngSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 3; } }

		public virtual Ticket Entity { get { return null; } set { } }
		public virtual TicketInfo EntityInfo { get { return null; } }

        public CompanyInfo _company;

        #endregion

        #region Factory Methods

		public TPVForm()
			: this(null) {}

        public TPVForm(Form parent) 
			: this(-1, true, parent) {}

        public TPVForm(long oid)
			: this(oid, false, null) {}

        public TPVForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            if (Conceptos_DGW == null) return;

            base.MaximizeForm(new Size(1000,0));
            base.FormatControls();

			Fecha_DTP.Checked = true;

            CurrencyManager cm;
            cm = (CurrencyManager)BindingContext[Conceptos_DGW.DataSource];
            cm.SuspendBinding();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            ConceptosConcepto.Tag = 1;

            cols.Add(ConceptosConcepto);

            ControlsMng.MaximizeColumns(Conceptos_DGW, cols);

			Conceptos_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            _company = CompanyInfo.Get(AppContext.ActiveSchema.Oid);
            PgMng.Grow();
        }

		public override void RefreshSecondaryData()
		{
			EMedioPago[] list = { EMedioPago.Efectivo, EMedioPago.Tarjeta };
			Datos_MedioPago.DataSource = Library.Common.EnumText<EMedioPago>.GetList(list);
			PgMng.Grow(string.Empty, "Medios de Pago");

			EFormaPago[] list2 = { EFormaPago.Contado };
			Datos_FormaPago.DataSource = Library.Common.EnumText<EFormaPago>.GetList(list2);
			PgMng.Grow(string.Empty, "Formas de Pago");
		}

        protected virtual void HideComponentes() {}

        #endregion

		#region Validation & Format

		#endregion

        #region Print

        public override void PrintObject()
        {
            TicketReportMng reportMng = new TicketReportMng(AppContext.ActiveSchema);
            
            ReportClass report = reportMng.GetTicketReport((EntityInfo.ConceptoTickets != null) ? EntityInfo : Entity.GetInfo(true));
            
            if (SettingsMng.Instance.GetUseDefaultPrinter())
            {
                string impresora = moleQule.Library.Invoice.ModulePrincipal.GetDefaultTicketPrinter();
                PrintReport(report, impresora);
            }
            else
                ShowReport(report);
        }

        #endregion

        #region Actions

        protected virtual void SetTipoAction() {}
        protected virtual void EliminarAlbaranAction() {}
        protected virtual void NuevoAlbaranAction() {}

        #endregion

        #region Buttons

        protected override void PrintAction()
        {
            PrintObject();
        }

        private void AddAlbaran_BT_Click(object sender, EventArgs e)
        {
            NuevoAlbaranAction();
        }

        private void EliminarAlbaran_BT_Click(object sender, EventArgs e)
        {
            EliminarAlbaranAction();
        }

        private void Tipo_BT_Click(object sender, EventArgs e)
        {
            SetTipoAction();
        }

        #endregion

        #region Events

        private void NTicketManual_CKB_CheckedChanged(object sender, EventArgs e)
        {
            NTicket_TB.ReadOnly = !NTicketManual_CKB.Checked;
            NTicket_TB.BackColor = NTicket_TB.ReadOnly ? NumeroSerie_TB.BackColor : Color.White;
            NTicket_TB.ForeColor = NTicket_TB.ReadOnly ? NumeroSerie_TB.ForeColor : Color.Navy;
        }

        #endregion
    }
}