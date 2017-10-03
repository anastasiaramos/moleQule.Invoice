using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class BudgetForm : Skin01.ItemMngSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 2; } }

		public virtual Budget Entity { get { return null; } set { } }
		public virtual BudgetInfo EntityInfo { get { return null; } }

        #endregion

        #region Factory Methods

        public BudgetForm() 
			: this(-1) { }

        public BudgetForm(long oid) 
			: this(oid, true, null) {}

        public BudgetForm(long oid, bool isModal, Form parent)
            : this(oid, isModal, parent, null) {}

        public BudgetForm(long oid, bool isModal, Form parent, Budget source)
			: base(oid, new object[1] { source }, isModal, parent)
		{
			InitializeComponent();
		}

        #endregion

        #region Layout & Source

		public override void FitColumns()
		{
			CurrencyManager cm;
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

			cm = (CurrencyManager)BindingContext[Lineas_DGW.DataSource];
			cm.SuspendBinding();

			LiConcepto.Tag = 1;

			cols.Add(LiConcepto);

			ControlsMng.MaximizeColumns(Lineas_DGW, cols);

			Lineas_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
		}

        public override void FormatControls()
        {
            if (Lineas_DGW == null) return;

            base.MaximizeForm(new Size(1200,0));
            base.FormatControls();

			Fecha_DTP.Checked = true;

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            LiConcepto.Tag = 1;

            cols.Add(LiConcepto);

            ControlsMng.MaximizeColumns(Lineas_DGW, cols);

            HideComponentes();

			Lineas_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			Lineas_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

		protected virtual void RefreshConceptos()
		{
			Datos_Concepto.ResetBindings(true);
			Lineas_DGW.Refresh();
		}

        protected virtual void HideComponentes() { }

        #endregion

		#region Validation & Format

		#endregion

        #region Print

        public override void PrintObject()
        {
            BudgetReportMng reportMng = new BudgetReportMng(AppContext.ActiveSchema);
            FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

			ClienteInfo cliente = ClienteInfo.Get(Entity.OidCliente, false);

			conf.nota = (cliente.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
            conf.nota += Environment.NewLine + (EntityInfo.Nota ? Nota_TB.Text : "");

            ReportClass report = reportMng.GetDetailReport((EntityInfo.Conceptos != null) ? EntityInfo : Entity.GetInfo(true), conf);

            if (SettingsMng.Instance.GetUseDefaultPrinter())
            {
                int n_copias = SettingsMng.Instance.GetDefaultNCopies();
                PrintReport(report, n_copias);
            }
            else
                ShowReport(report);
        }

        #endregion

        #region Actions

        protected override void PrintAction() { PrintObject(); }        
        protected virtual void EditarClienteAction() { }
        protected virtual void NuevoConceptoAction() { }
        protected virtual void NuevoConceptoLibreAction() { }
        protected virtual void EditarConceptoAction() { }
        protected virtual void EliminarConceptoAction() { }
		protected virtual void SelectExpedienteLineaAction() { }
		protected virtual void SelectImpuestoLineaAction() { }

        #endregion

        #region Buttons
        
        private void Cliente_BT_Click(object sender, EventArgs e)
        {
            EditarClienteAction();
        }

        private void Nuevo_BT_Click(object sender, EventArgs e)
        {
            NuevoConceptoAction();
        }

        private void ConceptoLibre_BT_Click(object sender, EventArgs e)
        {
            NuevoConceptoLibreAction();
        }

        private void Editar_Concepto_BT_Click(object sender, EventArgs e)
        {
            EditarConceptoAction();
        }

        private void Eliminar_Concepto_BT_Click(object sender, EventArgs e)
        {
            EliminarConceptoAction();
        }

        #endregion

        #region Events

        private void NProformaManual_CKB_CheckedChanged(object sender, EventArgs e)
        {
            NProforma_TB.ReadOnly = !NAlbaranManual_CKB.Checked;
            NProforma_TB.BackColor = NProforma_TB.ReadOnly ? NumeroSerie_TB.BackColor : Color.White;
            NProforma_TB.ForeColor = NProforma_TB.ReadOnly ? NumeroSerie_TB.ForeColor : Color.Navy;
        }

		private void Tabla_Conceptos_DoubleClick(object sender, EventArgs e)
		{
			EditarConceptoAction();
		}

		private void Conceptos_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lineas_DGW.Columns[e.ColumnIndex].Name == LiPImpuestos.Name) SelectImpuestoLineaAction();
			else if (Lineas_DGW.Columns[e.ColumnIndex].Name == LiExpediente.Name) SelectExpedienteLineaAction();
		}

        #endregion
    }
}

