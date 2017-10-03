using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Face;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports.Budget;

namespace moleQule.Face.Invoice
{
    public partial class BudgetMngForm : BudgetMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "ProformaMngForm";
		public static Type Type { get { return typeof(BudgetMngForm); } }
        public override Type EntityType { get { return typeof(Budget); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected Budget _entity;

		protected long _oid_cliente = 0;
		protected long _oid_serie = 0;

		#endregion

		#region Factory Methods

		public BudgetMngForm()
			: this(null) { }

		public BudgetMngForm(Form parent)
			: this(false, parent) { }

        public BudgetMngForm(bool isModal, Form parent)
            : this(isModal, parent, null) { }

		public BudgetMngForm(bool isModal, Form parent, BudgetList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = BudgetList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;
		}

		#endregion

		#region Layout

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					Seleccionar.Visible = true;
					Seleccionar.ReadOnly = false;

					break;

				case molView.Normal:

					ShowAction(molAction.Copy);
					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.ExportPDF);

					break;
			}
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();

			ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla), ControlTools.Instance.HeaderSelectedStyle);

			Fields_CB.Text = NombreCliente.HeaderText;
		}

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			NombreCliente.Tag = 1;

			cols.Add(NombreCliente);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;
			if (!row.Displayed) return;

			BudgetInfo item = row.DataBoundItem as BudgetInfo;

			//Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Proforma");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (Library.Common.ModulePrincipal.GetUseActiveYear())
						List = BudgetList.GetList(false, Library.Common.ModulePrincipal.GetActiveYear().Year);
					else
						List = BudgetList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Proformas");
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
						BudgetList listA = BudgetList.GetList(_filter_results);
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
						BudgetList listD = BudgetList.GetList(_filter_results);
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
			BudgetAddForm form = new BudgetAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new BudgetViewForm(ActiveOID));
		}

		public override void OpenEditForm()
		{
			BudgetEditForm form = new BudgetEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

        public override void DeleteAction()
        {
            Budget.Delete(ActiveOID);
            _action_result = DialogResult.OK;
        }

		public override void CopyObjectAction(long oid)
		{
            Budget source = Budget.Get(oid);
			source.CloseSession();

			BudgetEditForm form = new BudgetEditForm(source, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

			BudgetReportMng reportMng = new BudgetReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

			ReportClass report = reportMng.GetListReport(BudgetList.GetList(Datos.DataSource as IList<BudgetInfo>),
																null,
																SerieList.GetList(false));
			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintDetailAction()
		{
			if (ActiveItem == null) return;

			PgMng.Reset(6, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			BudgetReportMng reportMng = new BudgetReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

			SerieInfo serie = SerieInfo.Get(ActiveItem.OidSerie, false);
			PgMng.Grow();

			ClienteInfo cliente = ClienteInfo.Get(ActiveItem.OidCliente, false);
			PgMng.Grow();

			FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

			conf.nota = (cliente.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
			conf.nota += Environment.NewLine + (ActiveItem.Nota ? serie.Cabecera : "");
			conf.cuenta_bancaria = string.Empty;
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

			BudgetInfo item = BudgetInfo.Get(ActiveOID, true);
			PgMng.Grow();

			ReportClass report = reportMng.GetDetailReport(item, conf);
            PgMng.FillUp();

            if (SettingsMng.Instance.GetUseDefaultPrinter())
            {
                int n_copias = SettingsMng.Instance.GetDefaultNCopies();
                PrintReport(report, n_copias);
            }
            else
                ShowReport(report);
		}

		public override void ExportPDFAction()
		{
			if (ActiveItem == null) return;

			try
			{
				PgMng.Reset(7, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

				BudgetReportMng reportMng = new BudgetReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

				SerieInfo serie = SerieInfo.Get(ActiveItem.OidSerie, false);
				PgMng.Grow();

				ClienteInfo cliente = ClienteInfo.Get(ActiveItem.OidCliente, false);
				PgMng.Grow();

				FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

				conf.nota = (cliente.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
				conf.nota += Environment.NewLine + (ActiveItem.Nota ? serie.Cabecera : "");
				conf.cuenta_bancaria = string.Empty;
				PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

				BudgetInfo item = BudgetInfo.Get(ActiveOID, true);
				PgMng.Grow();

				ReportClass report = reportMng.GetDetailReport(item, conf);
				PgMng.Grow();

				ExportPDF(report, ActiveItem.FileName);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		#endregion
	}

    public partial class BudgetMngBaseForm : Skin06.EntityMngSkinForm<BudgetList, BudgetInfo>
    {
        public BudgetMngBaseForm()
            : this(false, null, null) { }

        public BudgetMngBaseForm(bool isModal, Form parent, BudgetList lista)
            : base(isModal, parent, lista) { }
    }
}
