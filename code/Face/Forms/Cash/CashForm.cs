using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class CashForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties
		
        public const string ID = "CajaForm";
		public static Type Type { get { return typeof(CashForm); } }

        protected override int BarSteps { get { return base.BarSteps + 1; } }
		
        public virtual Cash Entity { get { return null; } set { } }
        public virtual CashInfo EntityInfo { get { return null; } }

		
        #endregion

        #region Factory Methods

        public CashForm() 
			: this(-1, true, null) { }

        public CashForm(long oid, bool ismodal, Form parent)
            : base(oid, ismodal, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout

        public override void FitColumns()
        {
            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Observaciones.Tag = 0.5;
            Concepto.Tag = 0.5;

            cols.Add(Concepto);
            cols.Add(Observaciones);

            ControlsMng.MaximizeColumns(Lines_DGW, cols);
        }

        public override void FormatControls()
        {
            if (Lines_DGW == null) return;

            base.MaximizeForm(new Size(0, 0));
            base.FormatControls();

			Saldo_Panel.Left = (PanelPrincipal.Width - Saldo_Panel.Width) / 2;
			Saldo_Panel.Left = Saldo_Panel.Left < 0 ? 0 : Saldo_Panel.Left;

            if (EntityInfo.Oid == Library.Invoice.ModulePrincipal.GetCajaTicketsSetting())
                NCobro.HeaderText = "Nº Ticket";

			Lines_BS.MoveLast();
        }

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;
            CashLine item = (CashLine)row.DataBoundItem;

			if (item.Locked) row.ReadOnly = true;

			switch (item.ETipoLinea)
				{
					case ETipoLineaCaja.SalidaPorIngreso:
						row.Cells[Debe.Index].ReadOnly = true;
						row.Cells[Haber.Index].ReadOnly = false;
						row.Cells[Haber.Index].Style.BackColor = Face.ControlTools.Instance.BasicStyle.BackColor;
						row.Cells[Haber.Index].Style.ForeColor = Face.ControlTools.Instance.BasicStyle.ForeColor;
						item.Debe = 0;
					break;

					case ETipoLineaCaja.EntradaPorTraspaso:
                    case ETipoLineaCaja.EntradaPorTarjetaCredito:
						row.Cells[Debe.Index].ReadOnly = false;
						row.Cells[Haber.Index].ReadOnly = true;
						row.Cells[Debe.Index].Style.BackColor = Face.ControlTools.Instance.BasicStyle.BackColor;
						row.Cells[Debe.Index].Style.ForeColor = Face.ControlTools.Instance.BasicStyle.ForeColor;
						item.Haber = 0;
					break;

					case ETipoLineaCaja.Otros:
						row.Cells[Debe.Index].ReadOnly = false;
						row.Cells[Haber.Index].ReadOnly = false;
						row.Cells[Debe.Index].Style.BackColor = Face.ControlTools.Instance.BasicStyle.BackColor;
						row.Cells[Haber.Index].Style.BackColor = Face.ControlTools.Instance.BasicStyle.BackColor;
						row.Cells[Debe.Index].Style.ForeColor = Face.ControlTools.Instance.BasicStyle.ForeColor;
						row.Cells[Haber.Index].Style.ForeColor = Face.ControlTools.Instance.BasicStyle.ForeColor;

						break;
				}

			Face.Common.ControlTools.Instance.SetRowColorIM(row, item.EEstado);
		}

		#endregion

		#region Source
	
		#endregion

        #region Actions

        protected override void PrintAction()
        {
            PrintObject();
        }

        protected virtual void AddLineaAction() {}
		protected virtual void AnulaLineaAction() {}
        protected virtual void SetBankAccountAction() { }
        protected virtual void SetCreditCardAction() { }
        protected virtual void SetLineTypeAction() { }		
		protected override void RefreshAction() {}

        #endregion

		#region Buttons

		private void AddLinea_TI_Click(object sender, EventArgs e)
		{
			AddLineaAction();
		}

		private void AnulaLinea_TI_Click(object sender, EventArgs e)
		{
			AnulaLineaAction();
		}

		private void Refresh_TI_Click(object sender, EventArgs e)
		{
			RefreshAction();
		}

		#endregion

		#region Events

		private void LineaCajas_DGW_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (Lines_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if ((e.ColumnIndex == CuentaBancaria.Index))
			{
				SetBankAccountAction();
			}
            if ((e.ColumnIndex == CreditCard.Index))
            {
                SetCreditCardAction();
            }
			else if (e.ColumnIndex == TipoLineaLabel.Index)
			{
				SetLineTypeAction();
			}
		}

		private void LineaCajas_DGW_CellValidated(object sender, DataGridViewCellEventArgs e)
		{
			if (Lines_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

			if ((e.ColumnIndex == Debe.Index) || (e.ColumnIndex == Haber.Index))
			{
				RefreshAction();
			}
		}

		private void LineaCajas_DGW_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			if (e.RowIndex < 0) return;
			if (!_show_colors) return;

			SetRowFormat(Lines_DGW.Rows[e.RowIndex]);
		}

        #endregion
    }
}