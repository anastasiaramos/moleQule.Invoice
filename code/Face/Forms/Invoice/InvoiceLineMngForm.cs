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
    public partial class InvoiceLineMngForm : InvoiceLineMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "ConceptoFacturaMngForm";
		public static Type Type { get { return typeof(InvoiceLineMngForm); } }
        public override Type EntityType { get { return typeof(OutputInvoiceLine); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected OutputInvoiceLine _entity;

		protected long _oid_cliente = 0;
		protected long _oid_serie = 0;

		#endregion

		#region Factory Methods

		public InvoiceLineMngForm()
			: this(null) { }

		public InvoiceLineMngForm(Form parent)
			: this(false, parent) { }

		public InvoiceLineMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public InvoiceLineMngForm(bool isModal, Form parent, OutputInvoiceLineList lista)
			: base(isModal, parent, lista)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = OutputInvoiceLineList.NewList().GetSortedList();
            SortProperty = FechaFactura.DataPropertyName;
			SortDirection = ListSortDirection.Descending;
		}

		#endregion

		#region Autorizacion

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules()
		{
            Tabla.Visible = OutputInvoiceLine.CanGetObject();
            Add_Button.Enabled = OutputInvoiceLine.CanAddObject();
            Edit_Button.Enabled = OutputInvoiceLine.CanEditObject();
            Delete_Button.Enabled = OutputInvoiceLine.CanDeleteObject();
            Print_Button.Enabled = OutputInvoiceLine.CanGetObject();
            View_Button.Enabled = OutputInvoiceLine.CanGetObject();
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

            OutputInvoiceLineInfo item = row.DataBoundItem as OutputInvoiceLineInfo;

			//Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Copy);
					HideAction(molAction.Delete);
                    HideAction(molAction.Print);

					break;

				case molView.Normal:

					HideAction(molAction.Add);
					HideAction(molAction.Copy);
					HideAction(molAction.Delete);
                    HideAction(molAction.Print);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "ConceptoFactura");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (Library.Common.ModulePrincipal.GetUseActiveYear())
						List = OutputInvoiceLineList.GetList(Library.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
						List = OutputInvoiceLineList.GetList(false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de OutputInvoiceLines");
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
						OutputInvoiceLineList listA = OutputInvoiceLineList.GetList(_filter_results);
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
						List<OutputInvoiceLineInfo> entities = (List<OutputInvoiceLineInfo>)_selected;
						foreach (OutputInvoiceLineInfo item in entities)
						{
							List.GetItem(item.Oid).CopyFrom(item);
							if (FilterType == IFilterType.Filter)
							{
								OutputInvoiceLineList list = OutputInvoiceLineList.GetList(_filter_results);
								OutputInvoiceLineInfo entity = list.GetItem(item.Oid);
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
						OutputInvoiceLineList listD = OutputInvoiceLineList.GetList(_filter_results);
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

        public override void OpenViewForm() { AddForm(new InvoiceViewForm(ActiveItem.OidFactura, this)); }

        public override void OpenEditForm()
        {
            try
            {
                OutputInvoiceInfo factura = OutputInvoiceInfo.Get(ActiveItem.OidFactura, false);
                EntityBase.CheckEditAllowedEstado(factura.EEstado, EEstado.Abierto);
            }
            catch (iQException ex)
            {
                PgMng.ShowInfoException(ex);
                _action_result = DialogResult.Ignore;
                return;
            }

            InvoiceEditForm form = new InvoiceEditForm(ActiveItem.OidFactura, this);
            if (form.Entity != null)
            {
                AddForm(form);
                _action_result = DialogResult.OK;
            }
        }

		public override void PrintList()
		{
		}

		#endregion
	}

    public partial class InvoiceLineMngBaseForm : Skin06.EntityMngSkinForm<OutputInvoiceLineList, OutputInvoiceLineInfo>
	{
		public InvoiceLineMngBaseForm()
			: this(false, null, null) { }

        public InvoiceLineMngBaseForm(bool isModal, Form parent, OutputInvoiceLineList lista)
			: base(isModal, parent, lista) { }
	}
}
