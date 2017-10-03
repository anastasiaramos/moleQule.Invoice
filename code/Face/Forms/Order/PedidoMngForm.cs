using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
//using moleQule.Library.Store.Reports.Pedido;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
	public partial class PedidoMngForm : PedidoMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "PedidoMngForm";
		public static Type Type { get { return typeof(PedidoMngForm); } }
		public override Type EntityType { get { return typeof(Pedido); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Pedido _entity;
		protected EEstado _estado = EEstado.Todos;

		#endregion

		#region Factory Methods

		public PedidoMngForm()
			: this(null) { }

		public PedidoMngForm(Form parent)
			: this(false, parent, EEstado.Todos) { }

		public PedidoMngForm(bool isModal, Form parent, EEstado estado)
			: this(isModal, parent, estado, null) { }

		public PedidoMngForm(bool isModal, Form parent, EEstado estado, PedidoList lista)
			: base(isModal, parent, lista)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = PedidoList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;

			_estado = estado;
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

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Cliente.Tag = 0.3;
			Observaciones.Tag = 0.7;

			cols.Add(Cliente);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();

			ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla), ControlTools.Instance.HeaderSelectedStyle);

			Fields_CB.Text = Fecha.HeaderText;
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;
			if (!row.Displayed) return;

			PedidoInfo item = row.DataBoundItem as PedidoInfo;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					ShowAction(molAction.Refresh);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateAnulado);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.PrintListQR);

					Seleccionar.Visible = true;
					Seleccionar.ReadOnly = false;

					break;

				case molView.Normal:

					ShowAction(molAction.Refresh);
					ShowAction(molAction.Unlock);
					ShowAction(molAction.ChangeStateAnulado);
					ShowAction(molAction.PrintDetail);
					ShowAction(molAction.PrintListQR);

					Seleccionar.Visible = false;
					Seleccionar.ReadOnly = false;

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Pedido");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = PedidoList.GetList(Library.Common.ModulePrincipal.GetActiveYear().Year, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Pedidos");
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
						PedidoList listA = PedidoList.GetList(_filter_results);
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
						PedidoList listD = PedidoList.GetList(_filter_results);
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
			PedidoAddForm form = new PedidoAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new PedidoViewForm(ActiveItem.Oid, this));
		}

		public override void OpenEditForm()
		{
			PedidoEditForm form = new PedidoEditForm(ActiveItem.Oid, this);
			if (form.EntityInfo != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Pedido.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }

		public override void ChangeStateAction(EEstadoItem estado)
		{
            _entity = Pedido.ChangeEstado(ActiveOID, Library.Common.EnumConvert.ToEEstado(estado));

			_action_result = DialogResult.OK;
		}

		public override void PrintList()
		{
			/*PedidoReportMng reportMng = new PedidoReportMng(AppContext.ActiveSchema);
			
			PedidoListRpt report = reportMng.GetListReport(List);
			
			ShowReport(report);
			*/
		}

		#endregion
	}

	public partial class PedidoMngBaseForm : Skin06.EntityMngSkinForm<PedidoList, PedidoInfo>
	{
		public PedidoMngBaseForm()
			: this(false, null, null) { }

		public PedidoMngBaseForm(bool isModal, Form parent, PedidoList lista)
			: base(isModal, parent, lista) { }
	}
}
