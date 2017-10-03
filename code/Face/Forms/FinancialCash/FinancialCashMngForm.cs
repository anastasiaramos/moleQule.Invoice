using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Face.Hipatia;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports;

namespace moleQule.Face.Invoice
{
    public partial class FinancialCashMngForm : FinancialCashBaseForm
	{
		#region Attributes & Properties

        public const string ID = "FinancialCashMngForm";
		public static Type Type { get { return typeof(FinancialCashMngForm); } }
		public override Type EntityType { get { return typeof(FinancialCash); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected FinancialCash _entity;

		#endregion

		#region Factory Methods

		public FinancialCashMngForm()
			: this(null) {}

		public FinancialCashMngForm(Form parent)
			: this(false, parent) {}

		public FinancialCashMngForm(bool is_modal, Form parent)
			: this(is_modal, parent, null) {}

		public FinancialCashMngForm(bool is_modal, Form parent, FinancialCashList list)
			: base(is_modal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = FinancialCashList.NewList().GetSortedList();

			SortProperty = Vencimiento.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

		}

		#endregion

		#region Authorization

		protected override void ActivateAction(molAction action, bool state)
		{
			if (EntityType == null) return;

			switch (action)
			{
				case molAction.ChangeStateContabilizado:

					if ((AppContext.User != null) && (state))
						base.ActivateAction(action, Library.AutorizationRulesControler.CanEditObject(Library.Invoice.Resources.SecureItems.CUENTA_CONTABLE));
					else
						base.ActivateAction(action, state);

					break;

				default:
					base.ActivateAction(action, state);
					break;
			}
		}

		#endregion

		#region Layout & Format

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Cliente.Tag = 0.4;
			Observaciones.Tag = 0.6;

			cols.Add(Cliente);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			base.FormatControls();

			SetActionStyle(molAction.CustomAction1, Resources.Labels.CLIENTE_TI, Properties.Resources.cliente);
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
            if (!row.Displayed) return;
            if (row.IsNewRow) return;

            FinancialCashInfo item = (FinancialCashInfo)row.DataBoundItem;

            Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado != EEstado.Anulado ? item.EEstadoCobro : item.EEstado);

		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					break;

				case molView.Normal:

					HideAction(molAction.Add);
					ShowAction(molAction.Edit);
					HideAction(molAction.Delete);
					ShowAction(molAction.View);
					HideAction(molAction.Copy);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateContabilizado);
					HideAction(molAction.ChangeStateAnulado);
					HideAction(molAction.ChangeStateEmitido);
					ShowAction(molAction.ShowDocuments);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Effect");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (Library.Common.ModulePrincipal.GetUseActiveYear())
						List = FinancialCashList.GetList(Library.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
						List = FinancialCashList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Effects");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
					break;

				case molAction.ChangeStateContabilizado:
				case molAction.ChangeStateAnulado:
				case molAction.Unlock:
                case molAction.Edit:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					break;
			}

			RefreshSources();
			if (_entity != null) Select(_entity.Oid);
			_entity = null;
		}

		#endregion

		#region Actions

		public override void OpenEditForm()
		{
            FinancialCashEditForm form = new FinancialCashEditForm(ActiveItem, this);
            if (form.Entity != null)
            {
                AddForm(form);
                _entity = form.Entity;
            }
		}

		public override void OpenViewForm() { AddForm(new FinancialCashViewForm(ActiveItem, this)); }

		public override void ShowDocumentsAction()
		{
            //try
            //{
            //    AgenteInfo agent = AgenteInfo.Get(ActiveItem.TipoEntidad, ActiveItem);
            //    AgenteEditForm form = new AgenteEditForm(ActiveItem.TipoEntidad, ActiveItem, this);
            //    AddForm(form);
            //}
            //catch (HipatiaException ex)
            //{
            //    if (ex.Code == HipatiaCode.NO_AGENTE)
            //    {
            //        AgenteAddForm form = new AgenteAddForm(ActiveItem.TipoEntidad, ActiveItem, this);
            //        AddForm(form);
            //    }
            //}
		}
                
		#endregion

		#region Print

		public override void PrintList()
		{
            PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
            FinancialCashReportMng reportMng = new FinancialCashReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
            ReportClass report = reportMng.GetListReport(FinancialCashList.GetList((IList<FinancialCashInfo>)Datos.List));

            PgMng.FillUp();

            ShowReport(report);
		}

		#endregion
	}

    public partial class FinancialCashBaseForm : Skin06.EntityMngSkinForm<FinancialCashList, FinancialCashInfo>
    {
        public FinancialCashBaseForm()
            : this(false, null, null) { }

        public FinancialCashBaseForm(bool isModal, Form parent, FinancialCashList list)
            : base(isModal, parent, list) { }
    }
}
