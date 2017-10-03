using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Web.Mail;
using System.Net.Mail;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports.Ticket;

namespace moleQule.Face.Invoice
{
    public partial class TicketMngForm : TicketMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "TicketMngForm";
		public static Type Type { get { return typeof(TicketMngForm); } }
		public override Type EntityType { get { return typeof(Ticket); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Ticket _entity;
		protected ETipoFacturas _tipo;

		#endregion

		#region Factory Methods

		public TicketMngForm()
			: this(null, ETipoFacturas.Todas) { }

		public TicketMngForm(Form parent, ETipoFacturas tipo)
			: this(false, parent, tipo) { }

		public TicketMngForm(bool is_modal, Form parent, ETipoFacturas tipo)
			: this(is_modal, parent, null, tipo) { }

		public TicketMngForm(bool is_modal, Form parent, TicketList list, ETipoFacturas tipo)
			: base(is_modal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = TicketList.NewList().GetSortedList();
			SortProperty = Codigo.DataPropertyName;

			_tipo = tipo;
		}

		#endregion

		#region Layout

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateEmitido);
					//HideAction(molAction.ChangeStateContabilizado);
					HideAction(molAction.PrintDetail);
					//HideAction(molAction.UserDefined1);

					break;

				case molView.Normal:

					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateEmitido);
					//ShowAction(molAction.ChangeStateContabilizado);
					ShowAction(molAction.PrintDetail);
					//ShowAction(molAction.UserDefined1);

					break;
			}
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;
			
			base.FormatControls();

			//SetActionStyle(molAction.UserDefined1, Resources.Labels.COBROS_TI, Properties.Resources.cobro);

			ControlsMng.OrderByColumn(Tabla, Fecha, ListSortDirection.Descending);
			ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla), ControlTools.Instance.HeaderSelectedStyle);
			
			Fields_CB.Text = Codigo.HeaderText;
		}

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Observaciones.Tag = 1;

			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;
			if (!row.Displayed) return;

			TicketInfo item = row.DataBoundItem as TicketInfo;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Lista de Tickets - Begin");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					switch (_tipo)
					{
						case ETipoFacturas.Todas:
							if (Library.Common.ModulePrincipal.GetUseActiveYear())
								if (AppContext.User.IsSuperUser)
									List = TicketList.GetList(Library.Common.ModulePrincipal.GetActiveYear().Year, EEstado.Todos, false);
								else
								List = TicketList.GetList(Library.Common.ModulePrincipal.GetActiveYear().Year, EEstado.NoOculto, false);
							else
								List = TicketList.GetList(EEstado.NoOculto, false);
							break;
					}
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Tickets - End");
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
						TicketList listA = TicketList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.ChangeStateContabilizado:
				case molAction.ChangeStateEmitido:
				case molAction.Unlock:
				case molAction.PrintDetail:
				case molAction.ExportPDF:
				case molAction.EmailPDF:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
					if (FilterType == IFilterType.Filter)
					{
						TicketList listD = TicketList.GetList(_filter_results);
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
			TicketAddForm form = new TicketAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public void OpenAddForm(OutputDeliveryInfo albaran)
		{
			TicketAddForm form = new TicketAddForm(albaran, this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new TicketViewForm(ActiveOID, this));
		}

		public override void OpenEditForm()
		{
			try
			{
				EntityBase.CheckEditAllowedEstado(ActiveItem.EEstado, EEstado.Abierto);
			}
			catch (iQException ex)
			{
				PgMng.ShowInfoException(ex);
				_action_result = DialogResult.Ignore;
				return;
			}

			TicketEditForm form = new TicketEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Ticket.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		public override void ChangeStateAction(EEstadoItem estado)
		{
            _entity = Ticket.ChangeEstado(ActiveOID, Library.Common.EnumConvert.ToEEstado(estado));

			_action_result = DialogResult.OK;
		}

		public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }

		public override void CustomAction1()
		{
			/*ClienteInfo cliente = ClienteInfo.Get(ActiveItem.OidCliente, true);
			ChargeSummary item = ChargeSummary.Get(cliente);
			CobroEditForm form = new CobroEditForm(cliente.Oid, item, null, this);
			form.ShowDialog(this);*/
		}

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);

			TicketReportMng reportMng = new TicketReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

			TicketListRpt report = reportMng.GetListReport(TicketList.GetList(Datos.DataSource as IList<TicketInfo>));
			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintDetailAction()
		{
			if (ActiveItem == null) return;

			PgMng.Reset(4, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			TicketReportMng reportMng = new TicketReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

			SerieInfo serie = SerieInfo.Get(ActiveItem.OidSerie, false);
			PgMng.Grow();

			TicketInfo item = TicketInfo.Get(ActiveOID, true);			
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

			ReportClass report = reportMng.GetTicketReport(item);
            PgMng.FillUp();

            if (report != null)
            {
                if (SettingsMng.Instance.GetUseDefaultPrinter())
                {
                    string impresora = moleQule.Library.Invoice.ModulePrincipal.GetDefaultTicketPrinter();
                    PrintReport(report, impresora);
                }
                else
                    ShowReport(report);

                if (item.EEstado == EEstado.Abierto)
                    ChangeStateAction(EEstadoItem.Emitido);
            }
		}

		#endregion
	}

    public partial class TicketMngBaseForm : Skin06.EntityMngSkinForm<TicketList, TicketInfo>
    {
        public TicketMngBaseForm()
            : this(false, null, null) { }

        public TicketMngBaseForm(bool isModal, Form parent, TicketList lista)
            : base(isModal, parent, lista) { }
    }
}

