using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
    public partial class PedidoForm : Skin04.ItemMngSkinForm
    {
        #region Attributes & Properties

		public override Type EntityType { get { return typeof(Pedido); } }

        protected override int BarSteps { get { return base.BarSteps + 3; } }
		
        public virtual Pedido Entity { get { return null; } set { } }
        public virtual PedidoInfo EntityInfo { get { return null; } }
		
        #endregion

        #region Factory Methods

        public PedidoForm()
			: this(-1) {}

        public PedidoForm(long oid) 
			: this(oid, true, null) {}

		public PedidoForm(bool isModal) 
			: this(-1, isModal, null) {}

        public PedidoForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

        #region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();

			Concepto.Tag = 1;
			cols.Add(Concepto);

			ControlsMng.MaximizeColumns(Lineas_DGW, cols);
			ControlsMng.MarkGridColumn(Lineas_DGW, ControlsMng.GetCurrentColumn(Lineas_DGW));
		}

		public override void FormatControls()
        {
            if (Lineas_DGW == null) return;

            base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.CREAR_ALBARAN, Properties.Resources.albaran_emitido);

			Precio.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
			Fecha_DTP.Checked = true;

			IDPedido_TB.Mask = Library.Invoice.ModuleController.GetNFacturaMask();

			HideComponentes();
        }

		protected virtual void HideComponentes() { }

		protected virtual void RefreshLineas()
		{
			Datos_Lineas.ResetBindings(true);
			Lineas_DGW.Refresh();
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row == null) return;
			if (row.DataBoundItem == null) return;

			LineaPedido item = row.DataBoundItem as LineaPedido;

            row.Cells[LiKilos.Index].ReadOnly = (item.Pendiente != item.CantidadKilos);
            row.Cells[LiPieces.Index].ReadOnly = (item.Pendiente != item.CantidadKilos);

            row.Cells[LiKilos.Index].Style = (item.Pendiente != item.CantidadKilos) ? Total.DefaultCellStyle : Precio.DefaultCellStyle;
            row.Cells[LiPieces.Index].Style = (item.Pendiente != item.CantidadKilos) ? Total.DefaultCellStyle : Precio.DefaultCellStyle;
			row.Cells[Pendiente.Index].Style = (item.Pendiente != 0) ? moleQule.Face.ControlTools.Instance.CerradoStyle : Total.DefaultCellStyle;
			row.Cells[PendienteBultos.Index].Style = (item.Pendiente != 0) ? moleQule.Face.ControlTools.Instance.CerradoStyle : Total.DefaultCellStyle;
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
					ShowAction(molAction.CustomAction1);
					break;

				case molView.ReadOnly:

					HideAction(molAction.ShowDocuments);
					ShowAction(molAction.CustomAction1);
					break;
			}
		}

		#endregion

        #region Actions

		protected override void CustomAction1() { CrearAlbaranAction(); }

		protected virtual void AddLineaAction() { }
		protected virtual void AddLineaLibreAction() { }
		protected virtual void CrearAlbaranAction() { }
		protected virtual void DeleteLineaAction() { }
		protected virtual void EditarLineaAction() { }
		protected virtual void SelectAlmacenLineaAction() { }
		protected virtual void SelectEstadoLineaAction() { }
		protected virtual void SelectExpedienteLineaAction() { }
		protected virtual void SelectImpuestoLineaAction() { }
		protected virtual void UpdatePedidoAction() { }		

        #endregion

        #region Buttons

		private void AddConcepto_TI_Click(object sender, EventArgs e) { AddLineaAction(); }

		private void Edit_TI_Click(object sender, EventArgs e) { EditarLineaAction();	}

		private void Delete_TI_Click(object sender, EventArgs e) { DeleteLineaAction(); }

		private void ConceptoLibre_BT_Click(object sender, EventArgs e) { AddLineaLibreAction(); }

        #endregion

        #region Events

		private void Lineas_DGW_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			if (e.RowIndex < 0) return;
			if (!_show_colors) return;

			SetRowFormat(Lineas_DGW.Rows[e.RowIndex]);
		}

		private void Lineas_DGW_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			UpdatePedidoAction();
		}

		private void Lineas_DGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lineas_DGW.Columns[e.ColumnIndex].Name == PImpuestos.Name) SelectImpuestoLineaAction();
			else if (Lineas_DGW.Columns[e.ColumnIndex].Name == Almacen.Name) SelectAlmacenLineaAction();
			else if (Lineas_DGW.Columns[e.ColumnIndex].Name == Expediente.Name) SelectExpedienteLineaAction();
		}

		private void Lineas_DGW_DoubleClick(object sender, EventArgs e) { EditarLineaAction(); }

        #endregion
    }
}
