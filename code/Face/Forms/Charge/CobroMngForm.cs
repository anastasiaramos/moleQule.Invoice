using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cobro;
using moleQule.Face.Hipatia;

namespace moleQule.Face.Invoice
{
    public partial class CobroMngForm : CobroMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "CobroMngForm";
		public static Type Type { get { return typeof(CobroMngForm); } }
        public override Type EntityType { get { return typeof(Charge); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected Charge _entity;

		ETipoCobro _tipo = ETipoCobro.Todos;

		#endregion

		#region Factory Methods

		public CobroMngForm()
			: this(null) {}

		public CobroMngForm(Form parent)
			: this(parent, ETipoCobro.Todos) {}

		public CobroMngForm(Form parent, ETipoCobro tipo)
			: this(false, parent, tipo) {}

		public CobroMngForm(bool is_modal, Form parent, ETipoCobro tipo)
			: this(is_modal, parent, null, tipo) {}

		public CobroMngForm(bool is_modal, Form parent, ChargeList list, ETipoCobro tipo)
			: base(is_modal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = ChargeList.NewList().GetSortedList();

			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

			_tipo = tipo;

			switch (tipo)
			{
				case ETipoCobro.Todos:
					this.Text = Resources.Labels.COBRO_TODOS;
					break;
				case ETipoCobro.Cliente:
					this.Text = Resources.Labels.COBRO_CLIENTES;
					break;
			}
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

			ChargeInfo item = (ChargeInfo)row.DataBoundItem;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

			if (item.EEstado == EEstado.Anulado) return;

			if ((item.PendienteAsignacionREA != 0) || (item.PendienteAsignacion != 0))
				row.Cells[PendienteAsignacion.Name].Style = Common.ControlTools.Instance.CobradoStyle;
			else
				row.Cells[PendienteAsignacion.Name].Style = row.Cells[Importe.Index].Style;
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Edit);
					HideAction(molAction.Delete);
					HideAction(molAction.View);
					HideAction(molAction.Copy);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateContabilizado);
					HideAction(molAction.ChangeStateAnulado);
					HideAction(molAction.ChangeStateEmitido);
					ShowAction(molAction.ShowDocuments);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.PrintListQR);
					HideAction(molAction.CustomAction1);

					break;

				case molView.Normal:

					ShowAction(molAction.Add);
					ShowAction(molAction.Edit);
					HideAction(molAction.Delete);
					ShowAction(molAction.View);
					HideAction(molAction.Copy);
					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateContabilizado);
					ShowAction(molAction.ChangeStateAnulado);
					HideAction(molAction.ChangeStateEmitido);
					ShowAction(molAction.ShowDocuments);
					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.PrintListQR);
					ShowAction(molAction.CustomAction1);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Cobro");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					switch (_tipo)
					{
						case ETipoCobro.Todos:
							if (Library.Common.ModulePrincipal.GetUseActiveYear())
								List = ChargeList.GetList(Library.Common.ModulePrincipal.GetActiveYear().Year, false);
							else
								List = ChargeList.GetList(false);
							break;
						case ETipoCobro.Cliente:
							if (Library.Common.ModulePrincipal.GetUseActiveYear())
								List = ChargeList.GetListClientes(Library.Common.ModulePrincipal.GetActiveYear().Year, false);
							else
								List = ChargeList.GetListClientes(false);
							break;
					}
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Cobros");
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

		public override void OpenAddForm()
		{
			switch (_tipo)
			{
				case ETipoCobro.Cliente:
					{
						OpenEditForm();
					}
					break;

				case ETipoCobro.REA:
					{
						REAChargeAddForm form = new REAChargeAddForm(this);
						if (form.Entity != null)
							AddForm(form);
					}
                    break;

                case ETipoCobro.Fomento:
                    {
                        CobroFomentoAddForm form = new CobroFomentoAddForm(this);
                        if (form.Entity != null)
                            AddForm(form);
                    }
                    break;
			}
            RefreshList();
		}

		public override void OpenEditForm()
		{
			switch (ActiveItem.ETipoCobro)
			{
				case ETipoCobro.Cliente:
					{
						ChargeSummary item = ChargeSummary.GetByCliente(ActiveItem.OidCliente);
						CobroEditForm form = new CobroEditForm(ActiveItem.OidCliente, item, ActiveItem, this);

						if (form.Entity != null)
						{
							form.Select(ActiveItem);
							AddForm(form);
						}
					}
					break;

				case ETipoCobro.REA:
					{
						try
						{
							Library.Common.EntityBase.CheckEditAllowedEstado(ActiveItem.EEstado, EEstado.Abierto);
						}
						catch (iQException ex)
						{
							PgMng.ShowInfoException(ex);
							_action_result = DialogResult.Ignore;
							return;
						}

						REAChargeEditForm form = new REAChargeEditForm(ActiveOID, this);
						if (form.Entity != null)
							AddForm(form);
					}
                    break;

                case ETipoCobro.Fomento:
                    {
                        try
                        {
                            Library.Common.EntityBase.CheckEditAllowedEstado(ActiveItem.EEstado, EEstado.Abierto);
                        }
                        catch (iQException ex)
                        {
                            PgMng.ShowInfoException(ex);
                            _action_result = DialogResult.Ignore;
                            return;
                        }

                        CobroFomentoEditForm form = new CobroFomentoEditForm(ActiveOID, this);
                        if (form.Entity != null)
                            AddForm(form);
                    }
                    break;
			}
		}

		public override void OpenViewForm()
		{
			switch (ActiveItem.ETipoCobro)
			{
				case ETipoCobro.Cliente:
					OpenEditForm();
					break;

				case ETipoCobro.REA:
					//AddForm(new CobroReaViewForm(ActiveOID, this));
					break;
			}
		}

		public override void ShowDocumentsAction()
		{
			try
			{
				AgenteInfo agent = AgenteInfo.Get(ActiveItem.TipoEntidad, ActiveItem);
				AgenteEditForm form = new AgenteEditForm(ActiveItem.TipoEntidad, ActiveItem, this);
				AddForm(form);
			}
			catch (HipatiaException ex)
			{
				if (ex.Code == HipatiaCode.NO_AGENTE)
				{
					AgenteAddForm form = new AgenteAddForm(ActiveItem.TipoEntidad, ActiveItem, this);
					AddForm(form);
				}
			}
		}

		public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }

		public override void ChangeStateAction(EEstadoItem estado)
		{
            _entity = Charge.ChangeEstado(ActiveOID, ActiveItem.ETipoCobro, Library.Common.EnumConvert.ToEEstado(estado));

			_action_result = DialogResult.OK;
		}

		public override void CustomAction1() { ShowClienteAction(); }

		public virtual void ShowClienteAction()
		{
			ClientEditForm form = new ClientEditForm(ActiveItem.OidCliente, this);
			form.ShowDialog(this);
		}

		#endregion

		#region Print

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema, this.Text, FilterValues);
			reportMng.ShowQRCode = false;

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ChargeListRpt report = reportMng.GetListReport(ChargeList.GetList((IList<ChargeInfo>)Datos.List), null);

			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintQRAction()
		{
			PgMng.Reset(4, 1, Face.Resources.Messages.LOADING_DATA, this);
			CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema, this.Text, FilterValues);
			reportMng.ShowQRCode = true;

			PgMng.Grow();
            CobroFacturaList cfacturas = CobroFacturaList.GetList(false);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			ChargeListRpt report = reportMng.GetListReport(ChargeList.GetList((IList<ChargeInfo>)Datos.List), cfacturas);

			PgMng.FillUp();

			ShowReport(report);
		}

		public override void PrintDetailAction()
		{
			CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			ChargeInfo cobro = ChargeInfo.Get(ActiveItem.Oid, ActiveItem.ETipoCobro, true);

			switch (ActiveItem.ETipoCobro)
			{
				case ETipoCobro.Cliente:
					{
						CobroDetailRpt report = reportMng.GetDetallesCobroIndividualReport(cobro);

						ShowReport(report);
					}
					break;

				case ETipoCobro.REA:
					{
						FacREAList expedientes = FacREAList.GetListByCobro(cobro.Oid);

						CobroREADetailRpt report = reportMng.GetDetallesCobroREAIndividualReport(cobro, expedientes);

						ShowReport(report);
					}
					break;
			}

		}

		#endregion
	}

    public partial class CobroMngBaseForm : Skin06.EntityMngSkinForm<ChargeList, ChargeInfo>
    {
        public CobroMngBaseForm()
            : this(false, null, null) { }

        public CobroMngBaseForm(bool isModal, Form parent, ChargeList lista)
            : base(isModal, parent, lista) { }
    }

	public class CobroAClienteMngForm : CobroMngForm
	{
		public new const string ID = "CobroAClienteMngForm";
		public new static Type Type { get { return typeof(CobroAClienteMngForm); } }

		public CobroAClienteMngForm()
			: this(null) { }

		public CobroAClienteMngForm(Form parent)
			: base(parent, ETipoCobro.Cliente) { }
	}
}