using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;
using moleQule.Face.Store;

namespace moleQule.Face.Invoice
{
    public partial class CobroREAForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "CobroREAForm";
        public static Type Type { get { return typeof(CobroREAForm); } }

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        protected FacREAList _facturas_todas;
        protected SortedBindingList<FacREAInfo> _facturas;
		protected decimal _no_asignado = 0;

        public virtual Charge Entity { get { return null; } set { } }
        public virtual ChargeInfo EntityInfo { get { return null; } }

        protected FacREAInfo FacturaActual { get { return REAExpedients_BS.Current as FacREAInfo; } }
		protected decimal NoAsignado { get { return _no_asignado; } }

        protected decimal AsignacionFactura(DataGridViewRow row)
        {
            try { return Convert.ToDecimal(row.Cells[Asignacion.Index].Value); } catch { return 0; }
        }
        protected void AsignacionFactura(DataGridViewRow row, decimal value)
        {
            string valor = "0.00"; 
            try { valor = value.ToString("N2"); } catch { } 
            row.Cells[Asignacion.Index].Value = valor;
            
        }

        protected bool FacturaActiva(DataGridViewRow row)
        {
            if (row == null) return false;
            if (row.Cells[Asociar.Index].Value == null) return false;
            return (row.Cells[Asociar.Index].Value.ToString() == "True");
        }

        #endregion

        #region Factory Methods

        public CobroREAForm() : this(-1) { }

        public CobroREAForm(long oid) : this(oid, true, null) { }

        public CobroREAForm(bool isModal) : this(-1, isModal, null) { }

        public CobroREAForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
            Asignacion.ReadOnly = false;
            Asociar.ReadOnly = false;
            base.FormatControls();

			MaximizeForm(new Size(this.Width, 0));
        }

        protected virtual void SetGridColors(Control grid)
        {
            if (grid.Name == Facturas_DGW.Name)
            {
                FacREAInfo item;
                
                foreach (DataGridViewRow row in Facturas_DGW.Rows)
                {
                    if (row.IsNewRow) return;

                    item = row.DataBoundItem as FacREAInfo;
                    if (item == null) continue;

                    // Si ya estaba asignado entonces lo marcamos como asignado
                    if (item.Vinculado == Library.Invoice.Resources.Labels.SET_COBRO) MarkAsActiva(row);
                    else MarkAsNoActiva(row);
                }
            }
        }
        
        protected void MarkAsNoActiva(DataGridViewRow row)
        {
			FacREAInfo item = row.DataBoundItem as FacREAInfo;
			item.Vinculado = Library.Invoice.Resources.Labels.RESET_COBRO;
			row.Cells[Asignacion.Index].Style.BackColor = Color.LightGreen; 
        }

        protected void MarkAsActiva(DataGridViewRow row)
        {
			FacREAInfo item = row.DataBoundItem as FacREAInfo;
			item.Vinculado = Library.Invoice.Resources.Labels.SET_COBRO;
			row.Cells[Asignacion.Index].Style.BackColor = row.Cells[Pendiente.Index].Style.BackColor;
        }

		private void MarkControl(Control ctl)
		{
			if (ctl.Name == NoAsignado_TB.Name)
			{
				if (EntityInfo.Importe > 0)
					NoAsignado_TB.BackColor = (NoAsignado == 0) ? Color.LightGray : (NoAsignado > 0) ? Color.LightGreen : Color.Red;
				else
					NoAsignado_TB.BackColor = (NoAsignado == 0) ? Color.LightGray : (NoAsignado < 0) ? Color.LightGreen : Color.Red;
			}
		}

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            SetUnlinkedGridValues(Facturas_DGW.Name);
            PgMng.Grow();
        }

        public override void RefreshSecondaryData()
        {
			ETipoExpediente[] list = { ETipoExpediente.Todos, ETipoExpediente.Alimentacion, ETipoExpediente.Ganado, ETipoExpediente.Maquinaria };
            Datos_Tipo.DataSource = Library.Store.EnumText<ETipoExpediente>.GetList(list);
            TipoExpediente_CB.SelectedValue = (long)ETipoExpediente.Todos;
            PgMng.Grow();
        }

        protected override void SetUnlinkedGridValues(string grid_name)
        {
			if (_facturas != null)
			{
				FacREAList list = FacREAList.GetList(_facturas);
				list.UpdateCobroValues(EntityInfo);
			}
			UpdateAsignado();
        }

        #endregion

        #region Business Methods
       
        protected void UpdateAsignado()
        {
			if (_facturas == null) return;

            decimal _asignado = 0;

            foreach (FacREAInfo item in _facturas)
				_asignado += item.Asignado;

            _no_asignado = EntityInfo.Importe - _asignado;

            NoAsignado_TB.Text = _no_asignado.ToString("N2");
            MarkControl(NoAsignado_TB);
        }

        #endregion

        #region Actions

		protected virtual void LinkREAExpedientAction(DataGridViewRow row) {}
		protected virtual void UnlinkREAExpedientAction(DataGridViewRow row) {}
        protected virtual void DeleteChargeAction(DataGridViewRow row) {}
		protected virtual void EditAmountAction(DataGridViewRow row) {}
        protected virtual void SetDueDateAction() { }

        #endregion

		#region Buttons

		private void EditFactura_TI_Click(object sender, EventArgs e)
		{
			if (Facturas_DGW.CurrentRow == null) return;
			EditAmountAction(Facturas_DGW.CurrentRow);
		}

		private void ViewFactura_TI_Click(object sender, EventArgs e)
		{
			if (FacturaActual == null) return;

			ContenedorViewForm form = new ContenedorViewForm(FacturaActual.OidExpediente, this);
			form.ShowDialog(this);
		}

		#endregion

		#region Events

		private void CobroREAForm_Shown(object sender, EventArgs e)
		{
			SetUnlinkedGridValues(Facturas_DGW.Name);
			SetGridColors(Facturas_DGW);
		}

		private void Facturas_DGW_DoubleClick(object sender, EventArgs e)
		{
			if (Facturas_DGW.CurrentRow == null) return;
			EditAmountAction(Facturas_DGW.CurrentRow);
		}

		private void Facturas_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Facturas_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if (Facturas_DGW.Columns[e.ColumnIndex].Name == Asociar.Name)
			{
				DataGridViewRow row = Facturas_DGW.CurrentRow;
				FacREAInfo item = row.DataBoundItem as FacREAInfo;

				if (row.Cells[Asociar.Index].Value.ToString() == Library.Invoice.Resources.Labels.SET_COBRO)
					LinkREAExpedientAction(row);
				else
					UnlinkREAExpedientAction(row);

				SetUnlinkedGridValues(Facturas_DGW.Name);
				SetGridColors(Facturas_DGW);
			}
		}

		private void Facturas_DGW_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (_facturas != null)
			{
				_facturas_todas = FacREAList.GetList(_facturas);
				_facturas_todas.UpdateCobroValues(EntityInfo);
				SetGridColors(Facturas_DGW);
			}
		}

        private void Vencimiento_DTP_ValueChanged(object sender, EventArgs e)
        {
            SetDueDateAction();
        }

		#endregion
    }
}