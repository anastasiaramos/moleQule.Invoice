using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;
using CslaEx;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cobro;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
	public partial class CobroREAMngForm : CobroREAMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "CobroREAMngForm";
		public static Type Type { get { return typeof(CobroREAMngForm); } }
		public override Type EntityType { get { return typeof(Cobro); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

		protected Cobro _entity;

		#endregion

		#region Factory Methods

		public CobroREAMngForm()
			: this(null) { }

		public CobroREAMngForm(Form parent)
			: base(false, parent, null)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

            SetMainDataGridView(Tabla);
            Datos.DataSource = CobroList.NewList().GetSortedList();
            SortProperty = Fecha.DataPropertyName;
            SortDirection = ListSortDirection.Descending;
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

			CobroInfo item = row.DataBoundItem as CobroInfo;

			if (item == null) return;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

			if (item.PendienteAsignacionREA > 0)
				row.Cells[PendienteAsignacion.Name].Style.BackColor = Color.LightGreen;
			else
				row.Cells[PendienteAsignacion.Name].Style.BackColor = Color.White;
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.View);
					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.PrintListQR);

					break;

				case molView.Normal:

					HideAction(molAction.View);
					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.PrintListQR);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "CobroREA");

			_selected_oid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (Library.Common.ModulePrincipal.GetUseActiveYear())
						List = CobroList.GetListREA(Library.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
						List = CobroList.GetListREA(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de CobroREAs");
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
						CobroList listA = CobroList.GetList(_filter_results);
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
						CobroList listD = CobroList.GetList(_filter_results);
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
			CobroREAAddForm form = new CobroREAAddForm(this);
			AddForm(form);
			_entity = form.Entity;
		}

		public override void OpenEditForm()
		{
			CobroREAEditForm form = new CobroREAEditForm(ActiveItem.Oid, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Cobro.Delete(ActiveItem.Oid);
			_action_result = DialogResult.OK;
		}

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			CobroREAListRpt report = reportMng.GetCobroREAListReport(CobroList.GetList((IList<CobroInfo>)Datos.List), null);

			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintQRAction()
		{
			PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);
			CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema, this.Text, FilterValues);
			reportMng.ShowQRCode = true;

			PgMng.Grow();
			CobroREAList cReas = CobroREAList.GetList(false);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			CobroREAListRpt report = reportMng.GetCobroREAListReport(CobroList.GetList((IList<CobroInfo>)Datos.List), cReas);

			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintDetailAction()
		{
			if (ActiveItem == null) return;

			CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			FacREAList expedientes = FacREAList.GetListByCobro(ActiveOID);

			CobroREADetailRpt report = reportMng.GetDetallesCobroREAIndividualReport(ActiveItem, expedientes);

			ShowReport(report);
		}


		#endregion
	}

    public partial class CobroREAMngBaseForm : Skin06.EntityMngSkinForm<CobroList, CobroInfo>
    {
        public CobroREAMngBaseForm()
            : this(false, null, null) { }

        public CobroREAMngBaseForm(bool isModal, Form parent, CobroList lista)
            : base(isModal, parent, lista) { }
    }
}
