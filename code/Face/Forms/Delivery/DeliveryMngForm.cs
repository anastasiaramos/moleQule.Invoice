using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Delivery;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class DeliveryMngForm : DeliveryMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "DeliveryMngForm";
		public static Type Type { get { return typeof(DeliveryMngForm); } }
		public override Type EntityType { get { return typeof(OutputDelivery); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected OutputDelivery _entity;

		protected ETipoEntidad _holder_type;
		protected ETipoAlbaranes _delivery_type;
		protected long _oid_client = 0;
		protected long _oid_serie = 0;

		#endregion

		#region Factory Methods

		public DeliveryMngForm()
			: this(null, ETipoEntidad.Cliente, ETipoAlbaranes.Todos) { }

		public DeliveryMngForm(Form parent, ETipoEntidad holderType, ETipoAlbaranes deliveryType)
			: this(false, parent, holderType, deliveryType) { }

		public DeliveryMngForm(bool isModal, Form parent, ETipoEntidad holderType, ETipoAlbaranes deliveryType)
			: this(isModal, parent, holderType, deliveryType, null) { }

		public DeliveryMngForm(bool isModal, Form parent, ETipoEntidad holderType, ETipoAlbaranes tipo, OutputDeliveryList lista)
			: base(isModal, parent, lista)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = OutputDeliveryList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

			_holder_type = holderType;
			_delivery_type = tipo;
		}

		#endregion

		#region Business Methods

		protected void SeleccionaLinea()
		{
			if (Tabla.CurrentRow != null) Tabla.CurrentRow.Cells[Seleccionar.Index].Value = "True";
		}

		protected bool IsSelected(DataGridViewRow row)
		{
			if (row == null) return false;
			if (row.Cells[Seleccionar.Index].Value == null) return false;

			return (((DataGridViewCheckBoxCell)row.Cells[Seleccionar.Index]).Value.ToString() == "True");
		}		

		#endregion

		#region Autorizacion

		protected override void ApplyAuthorizationRules()
		{
			Tabla.Visible = OutputDelivery.CanGetObject();
			Add_Button.Enabled = OutputDelivery.CanAddObject();
			Edit_Button.Enabled = OutputDelivery.CanEditObject();
			Delete_Button.Enabled = OutputDelivery.CanDeleteObject();
			Print_Button.Enabled = OutputDelivery.CanGetObject();
			View_Button.Enabled = OutputDelivery.CanGetObject();
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			NombreCliente.Tag = 0.2;
			Observaciones.Tag = 0.8;

			cols.Add(NombreCliente);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			base.FormatControls();

			switch (_holder_type)
			{
				case ETipoEntidad.WorkReport:
					{
						Icon = Properties.Resources.output_delivery_work;
						IDCliente.HeaderText = Library.Store.Resources.Labels.WORK_ID;
						IDCliente.Width = 100;
						NombreCliente.HeaderText = Library.Store.Resources.Labels.WORK;
					}
					break;

				case ETipoEntidad.Cliente:
					{
						SetActionStyle(molAction.CustomAction1, Resources.Labels.CLIENTE_TI, Properties.Resources.cliente);
						SetActionStyle(molAction.CustomAction2, Resources.Labels.COBROS_TI, Properties.Resources.cobro);
						SetActionStyle(molAction.CustomAction3, Resources.Labels.CREAR_FACTURA, Properties.Resources.factura_emitida);
						SetActionStyle(molAction.CustomAction4, Resources.Labels.EXPORT_TO_COMPANY, Properties.Resources.data_out.ToBitmap());
					}
					break;
			}
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;
			if (!row.Displayed) return;

			OutputDeliveryInfo item = row.DataBoundItem as OutputDeliveryInfo;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Copy);
					HideAction(molAction.Edit);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.ExportPDF);
					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
					HideAction(molAction.CustomAction4);

					Seleccionar.Visible = true;
					Seleccionar.ReadOnly = false;

					break;

				case molView.Normal:

					ShowAction(molAction.Copy);
					ShowAction(molAction.Edit);
					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.ExportPDF);

					switch (_holder_type)
					{
						case ETipoEntidad.WorkReport:
							{
								HideAction(molAction.CustomAction1);
								HideAction(molAction.CustomAction2);
								HideAction(molAction.CustomAction3);
								HideAction(molAction.CustomAction4);

								Seleccionar.Visible = false;
								Seleccionar.ReadOnly = false;
							}
							break;

						case ETipoEntidad.Cliente:
							{
								ShowAction(molAction.CustomAction1);
								ShowAction(molAction.CustomAction2);
								ShowAction(molAction.CustomAction3);
								ShowAction(molAction.CustomAction4);

								Seleccionar.Visible = false;
								Seleccionar.ReadOnly = false;
							}
							break;
					}

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "OutputDelivery");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:					

					switch (_delivery_type)
					{
						case ETipoAlbaranes.Todos:

							if (Library.Common.ModulePrincipal.GetUseActiveYear())
								List = OutputDeliveryList.GetList(false, _holder_type, Library.Common.ModulePrincipal.GetActiveYear().Year);
							else
								List = OutputDeliveryList.GetList(false, _holder_type);
							break;

						case ETipoAlbaranes.Facturados:

							if (Library.Common.ModulePrincipal.GetUseActiveYear())
								List = OutputDeliveryList.GetFacturados(false, Library.Common.ModulePrincipal.GetActiveYear().Year);
							else
								List = OutputDeliveryList.GetFacturados(false);
							break;

						case ETipoAlbaranes.NoFacturados:

							if (Library.Common.ModulePrincipal.GetUseActiveYear())
								List = OutputDeliveryList.GetNoFacturados(true, _oid_client, _oid_serie, Library.Common.ModulePrincipal.GetActiveYear().Year);
							else
								List = OutputDeliveryList.GetNoFacturados(true, _oid_client, _oid_serie);
							break;

						case ETipoAlbaranes.Agrupados:
 
							if (Library.Common.ModulePrincipal.GetUseActiveYear())
								List = OutputDeliveryList.GetAgrupados(false, Library.Common.ModulePrincipal.GetActiveYear().Year);
							else
								List = OutputDeliveryList.GetAgrupados(false);
							break;

						case ETipoAlbaranes.Work:

							if (Library.Common.ModulePrincipal.GetUseActiveYear())
								List = OutputDeliveryList.GetWorkList(false, Library.Common.ModulePrincipal.GetActiveYear().Year);
							else
								List = OutputDeliveryList.GetWorkList(false);
							break;
					}
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Albaranes");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
				case molAction.Copy:
					if (_entity == null) return;
					if (List.GetItem(_entity.Oid) != null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
						OutputDeliveryList listA = OutputDeliveryList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.CustomAction1:
				case molAction.CustomAction2:
				case molAction.Edit:
				case molAction.Lock:
				case molAction.Unlock:

					if (_selected != null)
					{
						List<OutputDeliveryInfo> entities = (List<OutputDeliveryInfo>)_selected;
						foreach (OutputDeliveryInfo item in entities)
						{
							List.GetItem(item.Oid).CopyFrom(item);
							if (FilterType == IFilterType.Filter)
							{
								OutputDeliveryList list = OutputDeliveryList.GetList(_filter_results);
								OutputDeliveryInfo entity = list.GetItem(item.Oid);
								if (entity != null) entity.CopyFrom(item);
								_filter_results = list.GetSortedList();
							}
						}
					}

					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
							
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
					if (FilterType == IFilterType.Filter)
					{
						OutputDeliveryList listD = OutputDeliveryList.GetList(_filter_results);
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
			DeliveryAddForm form = new DeliveryAddForm(_holder_type, this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm() { AddForm(new DeliveryViewForm(ActiveOID, ActiveItem.EHolderType, this)); }

		public override void OpenEditForm()
		{
			if (ActiveItem.Facturado)
			{
				PgMng.ShowInfoException("No es posible modificar un albarán facturado.");

				_action_result = DialogResult.Ignore;
				return;
			}

			DeliveryEditForm form = new DeliveryEditForm(ActiveOID, ActiveItem.EHolderType, this);

			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void CopyObjectAction(long oid)
		{
			DeliveryAddForm form = new DeliveryAddForm(OutputDelivery.CloneAsNew(ActiveItem), this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void DeleteAction()
		{
			OutputDelivery.Delete(ActiveOID, ActiveItem.EHolderType);
			_action_result = DialogResult.OK;
		}

		public override void CustomAction1() { ShowClienteAction(); }
		public override void CustomAction2() { ShowCobrosAction(); }
		public override void CustomAction3() { CrearFacturaAction(); }
		public override void CustomAction4() { ExportToCompanyAction(); }

		public virtual void CrearFacturaAction()
		{
			DeliverySelectForm form = new DeliverySelectForm(this, ETipoEntidad.Cliente, OutputDeliveryList.GetNoFacturados(true));
			form.ShowDialog(this);
			
			if (form.DialogResult == DialogResult.OK)
			{
				try
				{
					PgMng.Reset(4, 1, Resources.Messages.GENERANDO_FACTURAS, this);
					List<OutputDeliveryInfo> albaranes = form.Selected as List<OutputDeliveryInfo>;	
					PgMng.Grow();

					OutputInvoices facturas = OutputInvoices.NewList(); 
					facturas.NewItems(albaranes);
					PgMng.Grow();

					facturas.Save();
					facturas.CloseSession();

					_selected = albaranes;
					_action_result = DialogResult.OK;
				}
				catch (Exception ex)
				{
					PgMng.ShowInfoException(ex);
				}
				finally
				{
					PgMng.FillUp();
				}
			}
		}

		public virtual void ExportToCompanyAction()
		{
			CompanyExporterActionForm form = new CompanyExporterActionForm(this, ActiveItem);
			form.ShowDialog(this);
		}

		public virtual void ShowClienteAction()
		{
			ClientEditForm form = new ClientEditForm(ActiveItem.OidHolder, this);
			form.ShowDialog(this);
		}

		public virtual void ShowCobrosAction()
		{
			ClienteInfo cliente = ClienteInfo.Get(ActiveItem.OidHolder, true);
			ChargeSummary item = ChargeSummary.Get(cliente);
			CobroEditForm form = new CobroEditForm(cliente.Oid, item, null, this);
			form.ShowDialog(this);
		}

		#endregion

		#region Print

		public override void PrintList()
		{
			DetalleAlbaranesActionForm form = new DetalleAlbaranesActionForm(OutputDeliveryList.GetList(Datos.DataSource as IList<OutputDeliveryInfo>));
			form.Titulo = this.Text;
			form.Filtro = this.FilterValues;
			form.DoSubmit();
			PgMng.FillUp();
		}

		public override void PrintDetailAction()
		{
			if (ActiveItem == null) return;

			PgMng.Reset(6, 1, Face.Resources.Messages.LOADING_DATA, this);

            OutputDeliveryReportMng reportMng = new OutputDeliveryReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

			SerieInfo serie = SerieInfo.Get(ActiveItem.OidSerie, false);
			PgMng.Grow();

			ClienteInfo cliente = ClienteInfo.Get(ActiveItem.OidHolder, false);
			PgMng.Grow();

			FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

			conf.nota = (cliente.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
			conf.nota += Environment.NewLine + (ActiveItem.Nota ? serie.Cabecera : "");
			conf.cuenta_bancaria = ActiveItem.CuentaBancaria;
			PgMng.Grow();

			OutputDeliveryInfo item = OutputDeliveryInfo.Get(ActiveOID, ActiveItem.EEntityType, true);
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

                OutputDeliveryReportMng reportMng = new OutputDeliveryReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

				SerieInfo serie = SerieInfo.Get(ActiveItem.OidSerie, false);
				PgMng.Grow();

				ClienteInfo cliente = ClienteInfo.Get(ActiveItem.OidHolder, false);
				PgMng.Grow();

				FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

				conf.nota = (cliente.OidImpuesto == 1) ? Library.Invoice.Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
				conf.nota += Environment.NewLine + (ActiveItem.Nota ? serie.Cabecera : "");
				conf.cuenta_bancaria = ActiveItem.CuentaBancaria;
				PgMng.Grow();

				OutputDeliveryInfo item = OutputDeliveryInfo.Get(ActiveOID, ActiveItem.EHolderType, true);
				PgMng.Grow();

				ReportClass rpt = reportMng.GetDetailReport(item, conf);
				PgMng.Grow();

				ExportPDF(rpt, ActiveItem.FileName); 
			}
			finally 
			{ 
				PgMng.FillUp(); 
			}
		}

		#endregion
	}

	public partial class DeliveryMngBaseForm : Skin06.EntityMngSkinForm<OutputDeliveryList, OutputDeliveryInfo>
	{
		public DeliveryMngBaseForm()
			: this(false, null, null) { }

		public DeliveryMngBaseForm(bool isModal, Form parent, OutputDeliveryList lista)
			: base(isModal, parent, lista) { }
	}

	public class DeliveryAllMngForm : DeliveryMngForm
	{
		public new const string ID = "DeliveryAllMngForm";
		public new static Type Type { get { return typeof(DeliveryAllMngForm); } }

		public DeliveryAllMngForm(Form parent)
			: base(parent, ETipoEntidad.Cliente, ETipoAlbaranes.Todos)
		{
			this.Text = "Albaranes Emitidos: Lista Completa";
		}
	}

	public class DeliveryFacturadosMngForm : DeliveryMngForm
	{
		public new const string ID = "DeliveryFacturadosMngForm";
		public new static Type Type { get { return typeof(DeliveryFacturadosMngForm); } }

		public DeliveryFacturadosMngForm(Form parent)
			: base(parent, ETipoEntidad.Cliente, ETipoAlbaranes.Facturados)
		{
			this.Text = "Albaranes Emitidos: Facturados";
		}
	}

	public class DeliveryNoFacturadosMngForm : DeliveryMngForm
	{
		public new const string ID = "DeliveryNoFacturadosMngForm";
		public new static Type Type { get { return typeof(DeliveryNoFacturadosMngForm); } }

		public DeliveryNoFacturadosMngForm(Form parent)
			: base(parent, ETipoEntidad.Cliente, ETipoAlbaranes.NoFacturados)
		{
			this.Text = "Albaranes Emitidos: Pendientes de Facturación";
		}
	}

	public class DeliveryAgrupadoMngForm : DeliveryMngForm
	{
		public new const string ID = "DeliveryAgrupadoMngForm";
		public new static Type Type { get { return typeof(DeliveryAgrupadoMngForm); } }

		public DeliveryAgrupadoMngForm(Form parent)
			: base(parent, ETipoEntidad.Cliente, ETipoAlbaranes.Agrupados)
		{
			this.Text = "Albaranes Emitidos: Agrupados";
		}
	}

	public class WorkDeliveryMngForm : DeliveryMngForm
	{
		public new const string ID = "WorkDeliveryMngForm";
		public new static Type Type { get { return typeof(WorkDeliveryMngForm); } }

		public WorkDeliveryMngForm(Form parent)
			: base(parent, ETipoEntidad.WorkReport, ETipoAlbaranes.Todos)
		{
			this.Text = Library.Invoice.Resources.Labels.WORK_DELIVERIES;
        }

        #region Print

        public override void PrintDetailAction()
        {
            if (ActiveItem == null) return;

            PgMng.Reset(5, 1, Face.Resources.Messages.LOADING_DATA, this);

            OutputDeliveryReportMng reportMng = new OutputDeliveryReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);

            OutputDeliveryInfo item = OutputDeliveryInfo.Get(ActiveOID, ETipoEntidad.WorkReport, true);
            PgMng.Grow();

            WorkReportInfo work_report = WorkReportInfo.Get(item.OidHolder, false);
            PgMng.Grow();

            ExpedientInfo work = ExpedientInfo.Get((work_report != null) ? work_report.OidExpedient : 0, false);
            PgMng.Grow();

            ReportClass report = reportMng.GetWorkDelivery(item, work);
            PgMng.FillUp();

            if (SettingsMng.Instance.GetUseDefaultPrinter())
            {
                int n_copias = SettingsMng.Instance.GetDefaultNCopies();
                PrintReport(report, n_copias);
            }
            else
                ShowReport(report);
        }

        public override void PrintList()
        {
            PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);

            OutputDeliveryReportMng reportMng = new OutputDeliveryReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);
            PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

            ExpedienteList work = ExpedienteList.GetList(ETipoExpediente.Work, false);
            PgMng.Grow();

            ReportClass report = reportMng.GetWorkDeliveryList(OutputDeliveryList.GetList(Datos.DataSource as IList<OutputDeliveryInfo>),
                                                                work);
            PgMng.FillUp();

            ShowReport(report);
        }

        #endregion
    }
}
