using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cobro;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule.Face.Invoice;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class CobrosREAForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public virtual ChargeSummary Resumen { get { return null; } set { } }

        protected ChargeInfo Cobro { get { return Datos_Cobros.Current != null ? Datos_Cobros.Current as ChargeInfo : null; } }
        protected FacREAInfo Factura { get { return Datos_Facturas.Current != null ? Datos_Facturas.Current as FacREAInfo: null; } }
		
		protected FacREAList _facturas_cliente;

        protected SerieList _series;
        protected ChargeSummary _resumen;
        protected ChargeInfo _cobro;
        protected ChargeList _cobros;

        #endregion

        #region Factory Methods

		public CobrosREAForm()
			: this(null) { }

        public CobrosREAForm(Form parent)  
			: this(-1, parent) {}

        public CobrosREAForm(bool is_modal)
            : base(-1, is_modal)
        {
            InitializeComponent();
        }

        public CobrosREAForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
        }

        #endregion

		#region Authorization

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules()
		{
			UnlockItem_TMI.Visible = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
			UnlockItem_TMI.Visible = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);

			LockItem_TMI.Visible = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
			LockItem_TMI.Visible = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
		}

		#endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Observaciones.Tag = 1;

			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Cobros_DGW, cols);
		}

        public override void FormatControls()
        {
            if (Cobros_DGW == null) return;

			int maxWidth = (Screen.PrimaryScreen.WorkingArea.Width > 1500) ? 1500 : Screen.PrimaryScreen.WorkingArea.Width;

			MaximizeForm(new Size(maxWidth, 0));
			Content_SC.SplitterDistance = maxWidth;
			Pendientes_SC.SplitterDistance = maxWidth;

			base.FormatControls();

			HideAction(molAction.ShowDocuments);

			Cobros_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 
			LineasFomento_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			Pendientes_DGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

			Imprimir_Button.Text = Resources.Labels.PRINT_COBROS_LIST;
            Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
        }

		protected virtual void SetGridColors(string grid_name)
		{
			if (grid_name == Cobros_DGW.Name)
			{
				ChargeInfo item;

				foreach (DataGridViewRow row in Cobros_DGW.Rows)
				{
					if (row.IsNewRow) return;

					item = row.DataBoundItem as ChargeInfo;
					if (item == null) continue;

					Face.Common.ControlTools.Instance.SetRowColorIM(row, item.EEstado);

					if (item.EEstado != EEstado.Anulado)
						row.Cells[PendienteAsignacion.Index].Style.BackColor = (item.Pendiente > 0) ? Color.LightGreen : row.Cells[Codigo.Index].Style.BackColor;
				}
			}
			else if (grid_name == Pendientes_DGW.Name)
			{
				FacREAInfo item;

				foreach (DataGridViewRow row in Pendientes_DGW.Rows)
				{
					if (row.IsNewRow) return;

					item = row.DataBoundItem as FacREAInfo;

					if (item == null) continue;

					if (item.Cobrado > 0)
					{
                        row.Cells[DiasTranscurridos.Index].Style.BackColor = Color.LightGreen;
                        row.Cells[DiasTranscurridos.Index].Style.SelectionBackColor = Color.LightGreen;
					}
					else if (0 <= item.DiasTranscurridos && item.DiasTranscurridos < 15)
					{
                        row.Cells[DiasTranscurridos.Index].Style.BackColor = Color.FromArgb(255, 255, 192);
                        row.Cells[DiasTranscurridos.Index].Style.SelectionBackColor = Color.FromArgb(255, 255, 192);
					}
					else if (15 <= item.DiasTranscurridos && item.DiasTranscurridos < 31)
					{
                        row.Cells[DiasTranscurridos.Index].Style.BackColor = Color.FromArgb(255, 192, 128);
                        row.Cells[DiasTranscurridos.Index].Style.SelectionBackColor = Color.FromArgb(255, 192, 128);
					}
					else if (31 <= item.DiasTranscurridos && item.DiasTranscurridos < 45)
					{
                        row.Cells[DiasTranscurridos.Index].Style.BackColor = Color.Orange;
                        row.Cells[DiasTranscurridos.Index].Style.SelectionBackColor = Color.Orange;
					}
					else if (45 <= item.DiasTranscurridos && item.DiasTranscurridos < 60)
					{
                        row.Cells[DiasTranscurridos.Index].Style.BackColor = Color.OrangeRed;
                        row.Cells[DiasTranscurridos.Index].Style.SelectionBackColor = Color.OrangeRed;
					}
					else
					{
                        row.Cells[DiasTranscurridos.Index].Style.BackColor = Color.Red;
                        row.Cells[DiasTranscurridos.Index].Style.SelectionBackColor = Color.Red;
					}
				}
			}

			if (Resumen != null)
			{
				if (Resumen.CreditoDispuesto > 0)
                    CreditoDispuesto_TB.BackColor = Color.FromArgb(255, 192, 128);
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			UpdateFacturasPendientes();
			PgMng.Grow(string.Empty, "Facturas Pendientes");
		}

		protected override void SetUnlinkedGridValues(string grid_name)
		{
			FacREAInfo item;

			if (grid_name == LineasFomento_DGW.Name)
			{
				ChargeInfo cobro = (ChargeInfo)Datos_Cobros.Current;

				foreach (DataGridViewRow row in LineasFomento_DGW.Rows)
				{
					if (row.IsNewRow) return;

					item = row.DataBoundItem as FacREAInfo;
					if (item == null) continue;

					//row.Cells[FacturaSerie.Index].Value = _series.GetItem(item.OidSerie).Identificador;
					row.Cells[Asignado.Index].Value = cobro.CobroREAs.GetItemByExpediente(item.OidExpediente, item.OidExpedienteREA).Cantidad;
					row.Cells[FechaAsignacion.Index].Value = cobro.CobroREAs.GetItemByExpediente(item.OidExpediente, item.OidExpedienteREA).FechaAsignacion;
				}
			}
		}

		protected virtual void UpdateFacturasCobro() {}

		protected virtual void UpdateFacturasPendientes() {}

		#endregion

		#region Validation & Format

		#endregion

		#region Business Methods

		#endregion

		#region Actions

		protected override void DocumentsAction()
        {
            try
            {
                AgenteEditForm form = new AgenteEditForm(typeof(Charge), Cobro as IAgenteHipatia);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
                    AgenteAddForm form = new AgenteAddForm(typeof(Charge), Cobro as IAgenteHipatia);
                    form.ShowDialog(this);
                }
                else
                    MessageBox.Show(ex.Message, moleQule.Face.Resources.Labels.ERROR_TITLE);
            }
        }

        protected override void PrintAction()
        {
            PrintObject();
        }

		protected virtual void CobrosDefaultAction() { EditCobroAction(); }
		protected virtual void NewCobroAction() {}
		protected virtual void ViewCobroAction() {}
		protected virtual void EditCobroAction() {}
		protected virtual void DeleteCobroAction() {}
		protected virtual void LockCobroAction() { }
		protected virtual void UnlockCobroAction() { }
		protected virtual void PrintCobroAction() {}

		protected virtual void FacturasDefaultAction() 
		{
			if (Factura == null) return;

			InvoiceViewForm form = new InvoiceViewForm(Factura.Oid, this);
			form.ShowDialog(this);
		}

		protected virtual void EditPendienteAction()
		{
			if (Pendientes_DGW.CurrentRow == null) return;

			FacREAInfo factura = Pendientes_DGW.CurrentRow.DataBoundItem as FacREAInfo;
            
            switch (factura.TipoExpediente)
            {
                case (long)ETipoExpediente.Alimentacion:
                case (long)ETipoExpediente.Ganado:
                case (long)ETipoExpediente.Maquinaria:
                    ContenedorEditForm cform = new ContenedorEditForm(factura.OidExpediente, this);
                    cform.ShowDialog(this);
                    break;
                case (long)ETipoExpediente.Almacen:
                    ExpedienteAlmacenEditForm eform = new ExpedienteAlmacenEditForm(factura.OidExpediente, this);
                    eform.ShowDialog(this);
                    break;
                case (long)ETipoExpediente.Work:
					WorkEditForm oform = new WorkEditForm(factura.OidExpediente, this);
                    oform.ShowDialog(this);
                    break;
            }

			UpdateFacturasPendientes();
		}

		protected virtual void VerPendienteAction()
		{
			if (Pendientes_DGW.CurrentRow == null) return;

			FacREAInfo factura = Pendientes_DGW.CurrentRow.DataBoundItem as FacREAInfo;

            switch (factura.TipoExpediente)
            {
                case (long)ETipoExpediente.Alimentacion:
                case (long)ETipoExpediente.Ganado:
                case (long)ETipoExpediente.Maquinaria:
                    ContenedorViewForm cform = new ContenedorViewForm(factura.OidExpediente, this);
                    cform.ShowDialog(this);
                    break;

                case (long)ETipoExpediente.Almacen:
                    ExpedienteAlmacenViewForm eform = new ExpedienteAlmacenViewForm(factura.OidExpediente, this);
                    eform.ShowDialog(this);
                    break;

                case (long)ETipoExpediente.Work:
                    WorkViewForm oform = new WorkViewForm(factura.OidExpediente, this);
                    oform.ShowDialog(this);
                    break;
            }
		}

		protected virtual void PrintPendienteAction()
		{
			if (Pendientes_DGW.CurrentRow == null) return;

            FacREAInfo factura = Pendientes_DGW.CurrentRow.DataBoundItem as FacREAInfo;

			PgMng.Reset(6, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

            ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, string.Empty);

            ReportClass report = reportMng.GetExpedienteREAListReport(ExpedienteREAList.GetList());
			PgMng.FillUp();

			ShowReport(report);
		}

		protected virtual void PrintPendienteListAction()
        {
            PrintPendienteAction();
		}

		public override void PrintObject()
		{
            //CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema, Resources.Labels.COBRO_CLIENTES, "Cliente: " + Entity.Nombre);
            //CobroClienteDetailRpt rpt = reportMng.GetCobroClienteDetailReport(Entity.GetInfo(), Resumen);

            //ShowReport(rpt);
		}

		#endregion

        #region Buttons

		private void AddCobro_TI_Click(object sender, EventArgs e) 
		{
			NewCobroAction();
		}

		private void EditCobro_TI_Click(object sender, EventArgs e)
		{
			EditCobroAction();
		}

		private void ViewCobro_TI_Click(object sender, EventArgs e)
		{
			ViewCobroAction();
		}

		private void UnlockItem_TMI_Click(object sender, EventArgs e)
		{
			UnlockCobroAction();
		}

		private void LockItem_TMI_Click(object sender, EventArgs e)
		{
			LockCobroAction();
		}

		private void NullItem_TMI_Click(object sender, EventArgs e)
		{
			DeleteCobroAction();
		}

		private void PrintCobro_TI_Click(object sender, EventArgs e)
		{
			PrintCobroAction();
		}

		private void EditPendiente_TI_Click(object sender, EventArgs e)
		{
			EditPendienteAction();
		}

		private void VerPendiente_TI_Click(object sender, EventArgs e)
		{
			VerPendienteAction();
		}

		private void PrintPendiente_TI_Click(object sender, EventArgs e)
		{
			PrintPendienteAction();
		}

		private void PrintListPendiente_TI_Click(object sender, EventArgs e)
		{
			PrintPendienteListAction();
		}

        #endregion

        #region Events

        private void CobroForm_Shown(object sender, EventArgs e)
        {
            SetUnlinkedGridValues(LineasFomento_DGW.Name);
			SetGridColors(Pendientes_DGW.Name);
			SetGridColors(Cobros_DGW.Name);
        }
        
        private void Cobros_DGW_DoubleClick(object sender, EventArgs e)
        {
            CobrosDefaultAction();
        }

        private void Facturas_DGW_DoubleClick(object sender, EventArgs e)
        {
			FacturasDefaultAction();
        }

        private void Datos_Cobros_CurrentItemChanged(object sender, EventArgs e)
        {
            UpdateFacturasCobro();
        }

        #endregion
    }
}

