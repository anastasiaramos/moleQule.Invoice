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
	public partial class CashMngForm : CashMngBaseForm
	{
		#region Attributes & Properties

        public const string ID = "CashMngForm";
		public static Type Type { get { return typeof(CashMngForm); } }
		public override Type EntityType { get { return typeof(Cash); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected Cash _entity;

		#endregion

		#region Factory Methods

		public CashMngForm()
			: this(null) { }

		public CashMngForm(Form parent)
			: this(false, parent) { }

		public CashMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public CashMngForm(bool isModal, Form parent, CashList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = CashList.NewList().GetSortedList();
			SortProperty = Codigo.DataPropertyName;
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

			CashInfo item = (CashInfo)row.DataBoundItem;

			if (item.SaldoAcumulado < 0)
				row.Cells[SaldoInicial.Name].Style.ForeColor = System.Drawing.Color.Red;

			if (item.SaldoParcial < 0)
				row.Cells[Saldo.Name].Style.ForeColor = System.Drawing.Color.Red;

			if (item.SaldoTotal < 0)
				row.Cells[SaldoTotal.Name].Style.ForeColor = System.Drawing.Color.Red;
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					ShowAction(molAction.PrintDetail);
					HideAction(molAction.Add);
					HideAction(molAction.Delete);
					HideAction(molAction.Edit);
					HideAction(molAction.Print);
					HideAction(molAction.PrintDetail);

					break;

				case molView.Normal:

					ShowAction(molAction.PrintDetail);
					HideAction(molAction.Add);
					HideAction(molAction.Delete);
					HideAction(molAction.Edit);
					HideAction(molAction.Print);
					HideAction(molAction.PrintDetail);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Caja");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = CashList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Cajas");
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
						CashList listA = CashList.GetList(_filter_results);
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
						CashList listD = CashList.GetList(_filter_results);
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
			CashAddForm form = new CashAddForm();
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new CashViewForm(ActiveOID, this));
		}

		public override void OpenEditForm()
		{
			CashEditForm form = new CashEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteObject(long oid)
		{
			/*if (ProgressInfoMng.ShowQuestion(moleQule.Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
			{
				Caja.Delete(oid);
				_action_result = DialogResult.OK;

				//Se eliminan todos los formularios de ese objeto
				foreach (ItemMngBaseForm form in _list_active_form)
				{
					if (form.Oid == oid)
					{
						form.Dispose();
						break;
					}
				}
			}*/
		}

		public override void PrintList()
		{
			/*PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

			CashReportMng reportMng = new CashReportMng(AppContext.ActiveSchema, this.Text, FilterValues);
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

			CajaListRpt report = reportMng.GetListReport(CashList.GetList(Datos.DataSource as IList<CashInfo>));
			PgMng.FillUp();

			ShowReport(report);*/
		}

		public override void PrintDetailAction()
		{
			/*if (ActiveItem == null) return;

			PgMng.Reset(3, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			CashReportMng reportMng = new CashReportMng(AppContext.ActiveSchema);
			PgMng.Grow();

			CajaRpt report = reportMng.GetDetailReport(CashInfo.Get(ActiveOID, true));
			PgMng.FillUp();

			ShowReport(report);*/
		}
		#endregion
	}

	public partial class CashMngBaseForm : Skin06.EntityMngSkinForm<CashList, CashInfo>
	{
		public CashMngBaseForm()
			: this(false, null, null) { }

		public CashMngBaseForm(bool isModal, Form parent, CashList lista)
			: base(isModal, parent, lista) { }
	}
}
