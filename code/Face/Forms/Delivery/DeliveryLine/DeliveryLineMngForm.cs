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
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
	public partial class DeliveryLineMngForm : DeliveryLineMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "ConceptoAlbaranMngForm";
		public static Type Type { get { return typeof(DeliveryLineMngForm); } }
        public override Type EntityType { get { return typeof(OutputDeliveryLine); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected OutputDeliveryLine _entity;

		protected long _oid_cliente = 0;
		protected long _oid_serie = 0;

		#endregion

		#region Factory Methods

		public DeliveryLineMngForm()
			: this(null) { }

		public DeliveryLineMngForm(Form parent)
			: this(false, parent) { }

		public DeliveryLineMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public DeliveryLineMngForm(bool isModal, Form parent, OutputDeliveryLineList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
            Datos.DataSource = OutputDeliveryLineList.NewList().GetSortedList();
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;
		}

		#endregion

		#region Autorizacion

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules()
		{
            Tabla.Visible = OutputDeliveryLine.CanGetObject();
            Add_Button.Enabled = OutputDeliveryLine.CanAddObject();
            Edit_Button.Enabled = OutputDeliveryLine.CanEditObject();
            Delete_Button.Enabled = OutputDeliveryLine.CanDeleteObject();
            Print_Button.Enabled = OutputDeliveryLine.CanGetObject();
            View_Button.Enabled = OutputDeliveryLine.CanGetObject();
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Concepto.Tag = 1;

			cols.Add(Concepto);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();

			Precio.DefaultCellStyle.Format = "N" + Library.Invoice.ModulePrincipal.GetNDecimalesPreciosSetting();
		}

		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (row.IsNewRow) return;

			OutputDeliveryLineInfo item = row.DataBoundItem as OutputDeliveryLineInfo;

			//Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.View);
					HideAction(molAction.Copy);
					HideAction(molAction.Edit);
					HideAction(molAction.Delete);

					Seleccionar.Visible = true;
					Seleccionar.ReadOnly = false;

					break;

				case molView.Normal:

					HideAction(molAction.Add);
					HideAction(molAction.View);
					HideAction(molAction.Copy);
					HideAction(molAction.Edit);
					HideAction(molAction.Delete);

					Seleccionar.Visible = false;
					Seleccionar.ReadOnly = false;

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "ConceptoAlbaran");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (Library.Common.ModulePrincipal.GetUseActiveYear())
                        List = OutputDeliveryLineList.GetList(Library.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
                        List = OutputDeliveryLineList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de ConceptoAlbaranes");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
					if (_entity == null) return;
					if (List.GetItem(_entity.Oid) != null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
                        OutputDeliveryLineList listA = OutputDeliveryLineList.GetList(_filter_results);
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
						List<OutputDeliveryLineInfo> entities = (List<OutputDeliveryLineInfo>)_selected;
						foreach (OutputDeliveryLineInfo item in entities)
						{
							List.GetItem(item.Oid).CopyFrom(item);
							if (FilterType == IFilterType.Filter)
							{
                                OutputDeliveryLineList list = OutputDeliveryLineList.GetList(_filter_results);
								OutputDeliveryLineInfo entity = list.GetItem(item.Oid);
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
                        OutputDeliveryLineList listD = OutputDeliveryLineList.GetList(_filter_results);
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

		public override void PrintList()
		{
		}

		#endregion
	}

    public partial class DeliveryLineMngBaseForm : Skin06.EntityMngSkinForm<OutputDeliveryLineList, OutputDeliveryLineInfo>
	{
		public DeliveryLineMngBaseForm()
			: this(false, null, null) { }

        public DeliveryLineMngBaseForm(bool isModal, Form parent, OutputDeliveryLineList list)
			: base(isModal, parent, list) { }
	}
}
