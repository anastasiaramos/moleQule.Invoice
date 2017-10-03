using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Csla;
using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cobro;

namespace moleQule.Face.Invoice
{
	public partial class ClientChargeMngForm : ClientChargeMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "ClientChargeMngForm";
		public static Type Type { get { return typeof(ClientChargeMngForm); } }
		public override Type EntityType { get { return typeof(ChargeSummary); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

		#endregion

		#region Factory Methods

		public ClientChargeMngForm()
			: this(null) { }

		public ClientChargeMngForm(Form parent)
			: this(false, parent, null) {}

		public ClientChargeMngForm(bool isModal, Form parent, ChargeSummaryList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = ChargeSummaryList.NewList().GetSortedList();
			SortProperty = Cliente.DataPropertyName;
		}

		#endregion

		#region Layout

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Cliente.Tag = 0.25;
			Observaciones.Tag = 0.75;

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

			ChargeSummary item = row.DataBoundItem as ChargeSummary;

			if (item == null) return;

			if (item.LimiteCredito < item.CreditoDispuesto)
			{
				row.Cells[CreditoDispuesto.Name].Style.BackColor = Color.Red;
				row.Cells[CreditoDispuesto.Name].Style.SelectionBackColor = Color.Red;
			}
			else if (item.CreditoDispuesto > 0)
			{
				row.Cells[CreditoDispuesto.Name].Style.BackColor = Color.FromArgb(255, 192, 128);
				row.Cells[CreditoDispuesto.Name].Style.SelectionBackColor = Color.FromArgb(255, 192, 128);
			}
			else
			{
				row.Cells[CreditoDispuesto.Name].Style.BackColor = Color.White;
				row.Cells[CreditoDispuesto.Name].Style.SelectionBackColor = row.Cells[LimiteCredito.Name].Style.SelectionBackColor;
			}

			if (item.DudosoCobro > 0)
			{
				row.Cells[DudosoCobro.Name].Style.BackColor = Color.Red;
				row.Cells[DudosoCobro.Name].Style.SelectionBackColor = Color.Red;
			}
			else
			{
				row.Cells[DudosoCobro.Name].Style.BackColor = Color.White;
				row.Cells[DudosoCobro.Name].Style.SelectionBackColor = row.Cells[LimiteCredito.Name].Style.SelectionBackColor;
			}
		}
		
		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.CustomAction1);

					break;

				case molView.Normal:

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
					List = ChargeSummaryList.GetList();
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Cobros");
		}

		public override void UpdateList()
		{
			RefreshMainData();
			RefreshSources();
		}

		#endregion

		#region Actions

		public override void OpenAddForm() { OpenEditForm(); }

		public override void OpenEditForm()
		{
			CobroEditForm form = new CobroEditForm(ActiveOID, (ChargeSummary)Datos.Current, this);
			if (form.Entity != null)
				AddForm(form);
		}

		public override void OpenViewForm() { OpenEditForm(); }

		public override void DeleteObject(long oid) { OpenEditForm(); }

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

			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
			CobroClienteListRpt report = reportMng.GetCobroClienteListReport(ChargeSummaryList.GetList((IList<ChargeSummary>)Datos.List));

			PgMng.FillUp();
			ShowReport(report);
		}

		#endregion
	}

	public partial class ClientChargeMngBaseForm : Skin06.EntityMngSkinForm<ChargeSummaryList, ChargeSummary>
	{
		public ClientChargeMngBaseForm()
			: this(false, null, null) { }

		public ClientChargeMngBaseForm(bool isModal, Form parent, ChargeSummaryList lista)
			: base(isModal, parent, lista) { }
	}
}