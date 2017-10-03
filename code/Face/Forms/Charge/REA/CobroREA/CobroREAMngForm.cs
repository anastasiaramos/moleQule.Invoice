using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Common;
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
        public override Type EntityType { get { return typeof(Charge); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

        protected Charge _entity;

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
            Datos.DataSource = ChargeList.NewList().GetSortedList();
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

			ChargeInfo item = row.DataBoundItem as ChargeInfo;

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

					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.PrintListQR);

					break;

				case molView.Normal:

					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.PrintListQR);
                    ShowAction(molAction.ChangeStateAnulado);
                    ShowAction(molAction.ChangeStateContabilizado);
                    ShowAction(molAction.Unlock);
                    HideAction(molAction.Delete);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "CobroREA");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (Library.Common.ModulePrincipal.GetUseActiveYear())
						List = ChargeList.GetListREA(Library.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
						List = ChargeList.GetListREA(false);
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
						ChargeList listA = ChargeList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.Lock:
				case molAction.Unlock:
                case molAction.ChangeStateAnulado:
                case molAction.ChangeStateContabilizado:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
					if (FilterType == IFilterType.Filter)
					{
						ChargeList listD = ChargeList.GetList(_filter_results);
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
			REAChargeAddForm form = new REAChargeAddForm(this);
			AddForm(form);
			_entity = form.Entity;
		}

		public override void OpenEditForm()
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
			REAChargeEditForm form = new REAChargeEditForm(ActiveItem.Oid, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

        public override void OpenViewForm()
        {
            AddForm(new CobroREAViewForm(ActiveItem.Oid, true, this));
        }

		public override void DeleteAction()
		{
            Charge.Delete(ActiveItem.Oid);
			_action_result = DialogResult.OK;
		}

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			CobroReportMng reportMng = new CobroReportMng(AppContext.ActiveSchema, this.Text, FilterValues);

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			CobroREAListRpt report = reportMng.GetCobroREAListReport(ChargeList.GetList((IList<ChargeInfo>)Datos.List), null);

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
			CobroREAListRpt report = reportMng.GetCobroREAListReport(ChargeList.GetList((IList<ChargeInfo>)Datos.List), cReas);

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

        public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }
        public override void ChangeStateAction(EEstadoItem estado)
        {
            switch (estado)
            {
                case EEstadoItem.Unlock:
                    _entity = Charge.ChangeEstado(ActiveOID, ActiveItem.ETipoCobro, EEstado.Abierto);
                    break;

                case EEstadoItem.Contabilizado:
                    _entity = Charge.ChangeEstado(ActiveOID, ActiveItem.ETipoCobro, EEstado.Contabilizado);
                    break;

                case EEstadoItem.Anulado:
                    {
                        if (ProgressInfoMng.ShowQuestion(Face.Resources.Messages.NULL_CONFIRM) != DialogResult.Yes)
                        {
                            return;
                        }
                        _entity = Charge.ChangeEstado(ActiveOID, ActiveItem.ETipoCobro, EEstado.Anulado);
                    } break;
            }

            _action_result = DialogResult.OK;
        }


		#endregion
	}

    public partial class CobroREAMngBaseForm : Skin06.EntityMngSkinForm<ChargeList, ChargeInfo>
    {
        public CobroREAMngBaseForm()
            : this(false, null, null) { }

        public CobroREAMngBaseForm(bool isModal, Form parent, ChargeList lista)
            : base(isModal, parent, lista) { }
    }
}
