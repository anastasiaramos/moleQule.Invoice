using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Face;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cash;

namespace moleQule.Face.Invoice
{
	public partial class CashLineMngForm : CashLineMngBaseForm
	{
		#region Attributes & Properties

        public const string ID = "CashLineMngForm";
		public static Type Type { get { return typeof(CashLineMngForm); } }
        public override Type EntityType { get { return typeof(CashLine); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected CashLine _entity;
		protected long _oid_caja;

		#endregion

		#region Factory Methods

		public CashLineMngForm()
			: this(null, 1) { }

        public CashLineMngForm(Form parent, long oidCash)
            : this(false, parent, oidCash) { }

		public CashLineMngForm(bool isModal, Form parent, long oidCash)
            : this(isModal, parent, null, oidCash) { }

        public CashLineMngForm(bool isModal, Form parent, CashLineList list, long oidCash)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = CashLineList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

            _oid_caja = oidCash;
		}

		#endregion
		
		#region Autorizacion

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules()
		{
            Tabla.Visible = CashLine.CanGetObject();
            Add_Button.Enabled = CashLine.CanAddObject();
            Edit_Button.Enabled = CashLine.CanEditObject();
            Delete_Button.Enabled = CashLine.CanDeleteObject();
            Print_Button.Enabled = CashLine.CanGetObject();
            View_Button.Enabled = CashLine.CanGetObject();
		}

		#endregion

		#region Layout & Format

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.View);
					HideAction(molAction.Delete);

					break;

				case molView.Normal:

					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.View);
					HideAction(molAction.Delete);

					break;

                case molView.Enbebbed:

                    HideAction(molAction.Add);
                    HideAction(molAction.Edit);
                    HideAction(molAction.View);
                    HideAction(molAction.Delete);

                    IDCobro.Visible = false;
                    IDPago.Visible = false;
                    NTercero.Visible = false;
                    Haber.Visible = false;
                    Saldo.Visible = false;
                    TipoLineaLabel.Visible = false;

                    break;
			}
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();
		}

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Concepto.Tag = 0.3;
			Observaciones.Tag = 0.7;

			cols.Add(Concepto);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;
			CashLineInfo item = (CashLineInfo)row.DataBoundItem;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "LineaCaja");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (Library.Common.ModulePrincipal.GetUseActiveYear())
						List = CashLineList.GetList(_oid_caja, Library.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
						List = CashLineList.GetList(_oid_caja, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de LineaCajas");
		}

		#endregion

		#region Actions

		public override void PrintList()
		{
			CashReportMng reportMng = new CashReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

            CashLineListRpt report = reportMng.GetListReport(CashLineList.GetList(Datos.DataSource as IList<CashLineInfo>));

			PgMng.FillUp();

			ShowReport(report);
		}

		#endregion
	}

    public partial class CashLineMngBaseForm : Skin06.EntityMngSkinForm<CashLineList, CashLineInfo>
	{
		public CashLineMngBaseForm()
			: this(false, null, null) { }

		public CashLineMngBaseForm(bool isModal, Form parent, CashLineList list)
			: base(isModal, parent, list) { }
	}
}
