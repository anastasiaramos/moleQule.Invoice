using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
	public partial class TraspasoMngForm : TraspasoMngBaseForm
    {
        #region Attributes & Properties
		
        public const string ID = "TraspasoMngForm";
		public static Type Type { get { return typeof(TraspasoMngForm); } }
		public override Type EntityType { get { return typeof(Traspaso); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }		
		
		public Traspaso _entity;		

		#endregion
		
		#region Factory Methods

		public TraspasoMngForm()
            : this(null) {}
			
		public TraspasoMngForm(Form parent)
			: this(false, parent) {}
		
		public TraspasoMngForm(bool is_modal, Form parent)
			: this(is_modal, parent, null) {}
		
		public TraspasoMngForm(bool is_modal, Form parent, TraspasoList list)
			: base(is_modal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla); 
			Datos.DataSource = TraspasoList.NewList().GetSortedList();			
			SortProperty = Fecha.DataPropertyName;
			SortDirection = ListSortDirection.Descending;
        }
		
		#endregion
	
		#region Autorizacion

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules() {}

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
			if (!row.Displayed) return;
			if (row.IsNewRow) return;
            
			TraspasoInfo item = (TraspasoInfo)row.DataBoundItem;
			
			Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
        }

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Delete);
					HideAction(molAction.Print);
					ShowAction(molAction.ChangeStateAnulado);

					break;

				case molView.Normal:

					HideAction(molAction.Delete);
					HideAction(molAction.Print);
					ShowAction(molAction.ChangeStateAnulado);
                    ShowAction(molAction.ChangeStateContabilizado);
                    ShowAction(molAction.Unlock);

					break;
			}
		}
		
		#endregion

		#region Source
		
		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Traspaso");
			
			long oid = ActiveOID;			
			
			switch (DataType)
            { 
                case EntityMngFormTypeData.Default:
                    List = TraspasoList.GetList(false);
                    break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de Traspasos");
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
                        TraspasoList listA = TraspasoList.GetList(_filter_results);
                        listA.AddItem(_entity.GetInfo(false));
                        _filter_results = listA.GetSortedList();
                    }
                    break;

                case molAction.Edit:
                case molAction.Unlock:
				case molAction.Lock:
				case molAction.ChangeStateAnulado:
                    if (_entity == null) return;
                    ActiveItem.CopyFrom(_entity);
                    break;

                case molAction.Delete:
                    if (ActiveItem == null) return;
                    List.RemoveItem(ActiveOID);
                    if (FilterType == IFilterType.Filter)
                    {
                        TraspasoList listD = TraspasoList.GetList(_filter_results);
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
			TraspasoAddForm form = new TraspasoAddForm();
			AddForm(form);
			if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
		}

		public override void OpenViewForm()
		{			
			AddForm(new TraspasoViewForm(ActiveOID));
		}

        public override void OpenEditForm() 
        {   
          	if (ActiveItem.EEstado == EEstado.Anulado)
			{
				ExecuteAction(molAction.View);
				return;
			}

			TraspasoEditForm form = new TraspasoEditForm(ActiveOID);
			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{	
			Traspaso.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		/// <summary>Duplica un objeto y abre el formulario para editar item
		/// <returns>void</returns>
		/// </summary>
		/*public override void DuplicateObject(long oid) 
		{
			Traspaso old = Traspaso.Get(oid);
			Traspaso dup = old.CloneAsNew();
			old.CloseSession();
			
			AddForm(new TraspasoAddForm(dup));

		}*/

		public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }

		public override void ChangeStateAction(EEstadoItem estado)
		{
            _entity = Traspaso.ChangeEstado(ActiveOID, Library.Common.EnumConvert.ToEEstado(estado));

			_action_result = DialogResult.OK;
		}

		/// <summary>Imprime la lista del objetos
		/// <returns>void</returns>
		/// </summary>
		public override void PrintList() 
		{
			/*TraspasoReportMng reportMng = new TraspasoReportMng(AppContext.ActiveSchema);
			
			TraspasoListRpt report = reportMng.GetListReport(List);
			
			if (report != null)
			{
				ReportViewer.SetReport(report);
				ReportViewer.ShowDialog();
			}
			else
			{
				MessageBox.Show(moleQule.Face.Resources.Messages.NO_DATA_REPORTS,
								moleQule.Face.Resources.Labels.ADVISE_TITLE,
								MessageBoxButtons.OK,
								MessageBoxIcon.Exclamation);
			}*/
		}

		#endregion
    }

	public partial class TraspasoMngBaseForm : Skin06.EntityMngSkinForm<TraspasoList, TraspasoInfo>
	{
		public TraspasoMngBaseForm()
			: this(false, null, null) { }

		public TraspasoMngBaseForm(bool isModal, Form parent, TraspasoList lista)
			: base(isModal, parent, lista) { }
	}
}
