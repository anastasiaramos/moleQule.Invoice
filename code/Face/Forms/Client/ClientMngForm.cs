using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Csla;
using moleQule.Face;
using moleQule.Face.Hipatia;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cliente;
using moleQule.Library.Hipatia;

namespace moleQule.Face.Invoice
{
	public partial class ClientMngForm : ClientMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "ClientMngForm";
		public static Type Type { get { return typeof(ClientMngForm); } }
		public override Type EntityType { get { return typeof(Cliente); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

		protected Cliente _entity;
		EEstado _estado = EEstado.Todos;

		#endregion

		#region Factory Methods

		public ClientMngForm()
			: this(null) { }

		public ClientMngForm(Form parent)
			: this(parent, EEstado.Todos) { }

		public ClientMngForm(Form parent, EEstado estado)
			: this(false, parent, estado, null) { }

		protected ClientMngForm(bool isModal, Form parent, EEstado estado, ClienteList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = ClienteList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;

			_estado = estado;
		}

		#endregion
				
		#region Layout
		
		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.4;
			Observaciones.Tag = 0.6;

			cols.Add(Nombre);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			base.FormatControls();

            SetActionStyle(molAction.CustomAction1, Face.Common.Resources.Labels.SEND_EMAIL, Face.Common.Properties.Resources.mail);
            SetActionStyle(molAction.CustomAction2, Resources.Labels.COBROS_TI, Properties.Resources.cobro);
		}
		
		protected override void SetRowFormat(DataGridViewRow row)
		{
			if (!row.Displayed) return;
			if (row.IsNewRow) return;

			ClienteInfo item = row.DataBoundItem as ClienteInfo;

			if (item == null) return;

			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);

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
		}
		
		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					Telefonos.Visible = false;
					Movil.Visible = false;
					CuentaBancaria.Visible = false;
					CuentaAsociada.Visible = false;
					MedioPago.Visible = false;
					FormaPago.Visible = false;
					DiasPago.Visible = false;

					ShowAction(molAction.CustomAction1);
                    ShowAction(molAction.CustomAction2);
					ShowAction(molAction.ShowDocuments);

					break;

				case molView.Normal:

					ShowAction(molAction.CustomAction1);
                    ShowAction(molAction.CustomAction2);
					ShowAction(molAction.ShowDocuments);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Clientes");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					List = ClienteList.GetList(_estado, false);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}
			PgMng.Grow(string.Empty, "Lista de Clientes");
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
				case molAction.Copy:
					if (_entity == null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
						ClienteList list = ClienteList.GetList(_filter_results);
						list.AddItem(_entity.GetInfo(false));
						_filter_results = list.GetSortedList();
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
						ClienteList list = ClienteList.GetList(_filter_results);
						list.RemoveItem(ActiveOID);
						_filter_results = list.GetSortedList();
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
			ClientAddForm form = new ClientAddForm(this);
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{
			AddForm(new ClientViewForm(ActiveOID, this));
		}

		public override void OpenEditForm()
		{
			ClientEditForm form = new ClientEditForm(ActiveOID, this);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Cliente.Delete(ActiveOID);
			_action_result = DialogResult.OK;
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

		public override void CustomAction1() { EmailAction(); }

		public virtual void EmailAction()
		{
			if (ActiveItem == null) return;

			PgMng.Reset(3, 1, Face.Resources.Messages.OPENING_EMAIL_CLIENT, this);

			MailParams mail = new MailParams();

			mail.To = ActiveItem.Email;

			try
			{
				PgMng.Grow();

				EMailSender.MailTo(mail);
			}
			catch
			{
				PgMng.ShowInfoException(moleQule.Face.Resources.Messages.NO_EMAIL_CLIENT);
			}
			finally
			{
				PgMng.FillUp();
			}
        }

        public override void CustomAction2() { GotoCobrosAction(); }

        public void GotoCobrosAction()
        {
            if (ActiveItem.EEstado == EEstado.Anulado) return;

            CobroEditForm form = new CobroEditForm(ActiveOID, ChargeSummary.Get(ActiveItem), this);
            form.ShowDialog(this);

        }

		#endregion

		#region Print

		public override void PrintList()
		{
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			
			ClienteReportMng reportMng = new ClienteReportMng(AppContext.ActiveSchema, this.Text, this.FilterValues);
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

			ClienteListRpt report = reportMng.GetListReport(ClienteList.GetList((IList<ClienteInfo>)Datos.List));
			PgMng.FillUp();

			ShowReport(report);
		}
		
		#endregion
	}

	public partial class ClientMngBaseForm : Skin06.EntityMngSkinForm<ClienteList, ClienteInfo>
	{
		public ClientMngBaseForm()
			: this(false, null, null) { }

		public ClientMngBaseForm(bool isModal, Form parent, ClienteList lista)
			: base(isModal, parent, lista) { }
	}
}
