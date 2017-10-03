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
	public partial class CashCountMngForm : CashCountMngBaseForm
	{
		#region Attributes & Properties

        public const string ID = "CashCountMngForm";
		public static Type Type { get { return typeof(CashCountMngForm); } }
		public override Type EntityType { get { return typeof(CierreCaja); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected CierreCaja _entity;
		protected long _oid_caja;

		#endregion

		#region Factory Methods

		public CashCountMngForm()
			: this(null, 1) { }

		public CashCountMngForm(Form parent, long oid_caja)
			: this(false, parent, oid_caja) { }

		public CashCountMngForm(bool isModal, Form parent, long oid_caja)
			: this(isModal, parent, null, oid_caja) { }

		public CashCountMngForm(bool isModal, Form parent, CierreCajaList list, long oid_caja)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = CierreCajaList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

			_oid_caja = oid_caja;
		}

		#endregion

		#region Layout & Format
		
		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Observaciones.Tag = 1;

			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;
			CierreCajaInfo item = (CierreCajaInfo)row.DataBoundItem;

			if (item.SaldoAcumulado < 0)
				row.Cells[SaldoAcumulado.Name].Style.ForeColor = System.Drawing.Color.Red;

			if (item.Saldo < 0)
				row.Cells[Saldo.Name].Style.ForeColor = System.Drawing.Color.Red;
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					ShowAction(molAction.PrintDetail);

					break;

				case molView.Normal:

					ShowAction(molAction.PrintDetail);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "CierreCaja");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = CierreCajaList.GetListByCaja(_oid_caja, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de CierreCajas");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
					if (_entity == null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
						CierreCajaList listA = CierreCajaList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.Lock:
				case molAction.Unlock:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
					if (FilterType == IFilterType.Filter)
					{
						CierreCajaList listD = CierreCajaList.GetList(_filter_results);
						listD.RemoveItem(ActiveOID);
						_filter_results = listD.GetSortedList();
					}
					break;
			}

			RefreshSources();
			if (_entity != null) Select(_entity.Oid);
			_entity = null;
		}

		#endregion

		#region Actions

		public override void OpenAddForm()
		{
			CashCountAddForm form = new CashCountAddForm(_oid_caja ,this);
			AddForm(form);
			_entity = form.Entity;
		}

		public override void OpenViewForm()
		{
            AddForm(new CashCountViewForm(ActiveOID, this));
		}

		public override void OpenEditForm()
		{
			CashCountEditForm form = new CashCountEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			CierreCaja.Delete(ActiveItem.Oid);
			_action_result = DialogResult.OK;
		}

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

			CashReportMng reportMng = new CashReportMng(AppContext.ActiveSchema, this.Text, FilterValues);
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

			CierreCajaListRpt report = reportMng.GetListReport(CierreCajaList.GetList(Datos.DataSource as IList<CierreCajaInfo>));
			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintDetailAction()
		{
			if (ActiveItem == null) return;

			PgMng.Reset(3, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			CashReportMng reportMng = new CashReportMng(AppContext.ActiveSchema);
			PgMng.Grow();

			CierreCajaRpt report = reportMng.GetDetailReport(CierreCajaInfo.Get(ActiveOID, true));
			PgMng.FillUp();

			ShowReport(report);
		}

		#endregion
	}

	public partial class CashCountMngBaseForm : Skin06.EntityMngSkinForm<CierreCajaList, CierreCajaInfo>
	{
		public CashCountMngBaseForm()
			: this(false, null, null) { }

		public CashCountMngBaseForm(bool isModal, Form parent, CierreCajaList list)
			: base(isModal, parent, list) { }
	}
}