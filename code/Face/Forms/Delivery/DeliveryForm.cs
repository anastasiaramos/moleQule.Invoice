using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;
using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class DeliveryForm : Skin04.ItemMngSkinForm
    {
        #region Business Methods

		public override Type EntityType { get { return typeof(OutputDelivery); } }

        protected override int BarSteps { get { return base.BarSteps + 2; } }

		public virtual OutputDelivery Entity { get { return null; } set { } }
		public virtual OutputDeliveryInfo EntityInfo { get { return null; } }

		protected ETipoAlbaranes _delivery_type = ETipoAlbaranes.Todos;

        #endregion

        #region Factory Methods

        public DeliveryForm() 
			: this(-1, null, true, null) {}

		public DeliveryForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
            InitializeComponent();
		}

        #endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			LiConcepto.Tag = 1;

            cols.Add(LiConcepto);

			ControlsMng.MaximizeColumns(Lineas_DGW, cols);

			Lineas_DGW.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
			Lineas_DGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
		}

        public override void FormatControls()
        {
			//IDE Compatibility
			if (AppContext.User == null) return;

            if (Lineas_DGW == null) return;

            base.MaximizeForm(new Size(1200,0));
            base.FormatControls();

			switch (EntityInfo.EHolderType)
			{
				case ETipoEntidad.WorkReport:
					{
						Cliente_GB.Visible = false;
						ExtraData_GB.Visible = false;

						Rectificativo_CKB.Visible = false;
						Contado_CB.Visible = false;
						Expediente_LB.Visible = false;
						Expediente_TB.Visible = false;
						Expediente_BT.Visible = false;
						NoExpediente_BT.Visible = false;

                        LiExpediente.Visible = false;
						LiExpediente.ReadOnly = true;
					}
					break;

				default:
					{
						WorkReport_GB.Visible = false;
						Comments_GB.Visible = false;					

						SetActionStyle(molAction.CustomAction1, Resources.Labels.IMPORTAR_PROFORMA, Properties.Resources.proforma);
						SetActionStyle(molAction.CustomAction2, Resources.Labels.IMPORTAR_PEDIDO, Properties.Resources.pedido_cliente);
						SetActionStyle(molAction.CustomAction3, Resources.Labels.CREAR_FACTURA, Properties.Resources.factura_emitida);
						SetActionStyle(molAction.CustomAction4, Resources.Labels.NUEVO_TICKET, Properties.Resources.ticket);

						LiPrecio.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
						Fecha_DTP.Checked = true;

						NAlbaran_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask();

						HideComponentes();
					}
					break;
			}
        }

        protected virtual void HideComponentes() { }

		protected virtual void RefreshConceptos()
		{
			Lines_BS.ResetBindings(true);
			Lineas_DGW.Refresh();
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					HideAction(molAction.ShowDocuments);
					HideAction(molAction.CustomAction1);

                    switch (EntityInfo.EHolderType)
                    {
                        case ETipoEntidad.WorkReport:
                            {
                                HideAction(molAction.CustomAction2);
                                HideAction(molAction.CustomAction3);
                                HideAction(molAction.CustomAction4);
                            }
                            break;

                        default:
                            {
                                ShowAction(molAction.CustomAction2);
                                ShowAction(molAction.CustomAction3);
                                ShowAction(molAction.CustomAction4);
                            }
                            break;
                    }

					break;

				case molView.ReadOnly:

					HideAction(molAction.ShowDocuments);
					break;
			}
		}
   
		#endregion

        #region Source

        public override void RefreshSecondaryData()
        {
            PaymentWay_BS.DataSource = Library.Common.EnumText<EMedioPago>.GetList();
            PgMng.Grow();

            EFormaPago[] formas_pago = { EFormaPago.Contado, EFormaPago.XDiasFechaFactura, EFormaPago.XDiasMes, EFormaPago.Trimestral};

            PaymentMethod_BS.DataSource = Library.Common.EnumText<EFormaPago>.GetList(formas_pago, false);
            PgMng.Grow();
        }

        #endregion

        #region Validation & Format

        #endregion

        #region Print

        public override void PrintObject()
        {
            PgMng.Reset(5, 1, Face.Resources.Messages.LOADING_DATA, this);

            OutputDeliveryReportMng reportMng = new OutputDeliveryReportMng(AppContext.ActiveSchema);

            OutputDeliveryInfo item = OutputDeliveryInfo.Get(EntityInfo.Oid, ETipoEntidad.WorkReport, true);
            PgMng.Grow();

            WorkReportInfo work_report = WorkReportInfo.Get(item.OidHolder, false);
            PgMng.Grow();

            ExpedientInfo work = ExpedientInfo.Get((work_report != null) ? work_report.OidExpedient : 0, false);
            PgMng.Grow();

            ReportClass report = reportMng.GetWorkDelivery(item, work);
            PgMng.FillUp();

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
        protected virtual void ClusteredAction() {}
        protected virtual void RectificativoAction() {}
        protected virtual void NewLineAction() { }
        protected virtual void NewFreeLineAction() { }
        protected virtual void EditLineAction() { }
        protected virtual void DeleteLineAction() { }
		protected virtual void SelectLineStoreAction() { }
		protected virtual void SelectLineExpedientAction() { }
		protected virtual void SelectLineTaxAction() { }
		protected virtual void UpdateDeliveryAction() { }

        #endregion

        #region Buttons

        private void NewLine_TI_Click(object sender, EventArgs e) { NewLineAction(); }
        private void NewFreeLine_TI_Click(object sender, EventArgs e) { NewFreeLineAction(); }
        private void EditLine_TI_Click(object sender, EventArgs e) { EditLineAction(); }
		private void DeleteLine_TI_Click(object sender, EventArgs e) { DeleteLineAction(); }

        #endregion

        #region Events

        private void IDManual_CKB_CheckedChanged(object sender, EventArgs e)
        {
            NAlbaran_TB.ReadOnly = !IDManual_CKB.Checked;
			NAlbaran_TB.BackColor = NAlbaran_TB.ReadOnly ? DiasPago_NTB.BackColor : Color.White;
			NAlbaran_TB.ForeColor = NAlbaran_TB.ReadOnly ? DiasPago_NTB.ForeColor : Color.Navy;
        }

		private void Conceptos_DGW_CellValidated(object sender, DataGridViewCellEventArgs e) { UpdateDeliveryAction(); }

		private void Conceptos_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lineas_DGW.Columns[e.ColumnIndex].Name == LiPImpuestos.Name) SelectLineTaxAction();
			else if (Lineas_DGW.Columns[e.ColumnIndex].Name == LiAlmacen.Name) SelectLineStoreAction();
			else if (Lineas_DGW.Columns[e.ColumnIndex].Name == LiExpediente.Name) SelectLineExpedientAction();
		}

		private void Conceptos_DGW_DoubleClick(object sender, EventArgs e) { EditLineAction(); }

        #endregion
    }
}

