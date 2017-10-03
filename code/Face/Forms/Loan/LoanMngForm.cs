using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

using Csla;
using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports;

namespace moleQule.Face.Invoice
{
    public partial class LoanMngForm : LoanMngBaseForm
    {
        #region Attributes & Properties

        public const string ID = "LoanMngForm";
        public static Type Type { get { return typeof(LoanMngForm); } }
        public override Type EntityType { get { return typeof(Loan); } }

		protected override int BarSteps { get { return base.BarSteps + 5; } }

        public Loan _entity;

        protected ELoanType _loan_type;
		
		#endregion
		
		#region Factory Methods

		public LoanMngForm()
            : this(null) {}
			
		public LoanMngForm(Form parent)
			: this(false, parent) {}
		
		public LoanMngForm(bool isModal, Form parent)
			: this(isModal, parent, null, ELoanType.All) {}
		
		public LoanMngForm(bool isModal, Form parent, LoanList list, ELoanType loanType)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;
			
			SetMainDataGridView(Tabla); 
			Datos.DataSource = LoanList.NewList().GetSortedList();
            SortProperty = FechaVencimiento.DataPropertyName;
            SortDirection = ListSortDirection.Descending;

            _loan_type = loanType;
        }
		
		#endregion
		
		#region Authorization

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules() {}
		
		protected override void ActivateAction(molAction action, bool state)
		{
			if (EntityType == null) return;

			switch (action)
			{
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
            Nombre.Tag = 0.4;
            Observaciones.Tag = 0.6;

            cols.Add(Nombre);
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
            
			LoanInfo item = (LoanInfo)row.DataBoundItem;

            Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado != EEstado.Anulado && item.CapitalPendiente == 0 ? EEstado.Pagado : item.EEstado);

        }
		
		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					break;

				case molView.Normal:

					break;
			}
		}
		
		#endregion

		#region Source
		
		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Prestamo");
			
			long oid = ActiveOID;			
			
			switch (DataType)
            { 
                case EntityMngFormTypeData.Default:
   					if (Library.Common.ModulePrincipal.GetUseActiveYear())
                        List = LoanList.GetList(_loan_type, Library.Common.ModulePrincipal.GetActiveYear().Year, false);
					else
                        List = LoanList.GetList(_loan_type, false);
					break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de Prestamos");
		}
		
        public override void UpdateList()
        {
            switch (_current_action)
            {
                case molAction.Add:
				case molAction.Copy:
                    if (_entity == null) return;
                    List.AddItem(_entity.GetInfo());
                    if (FilterType == IFilterType.Filter)
                    {
                        LoanList listA = LoanList.GetList(_filter_results);
                        listA.AddItem(_entity.GetInfo());
                        _filter_results = listA.GetSortedList();
                    }					
                    break;

                case molAction.Edit:
				case molAction.Lock:
                case molAction.Unlock:
				case molAction.ChangeStateAnulado:
                    if (_entity == null) return;
                    ActiveItem.CopyFrom(_entity);
                    break;

                case molAction.Delete:
                    if (ActiveItem == null) return;
                    List.RemoveItem(ActiveOID);
                    if (FilterType == IFilterType.Filter)
                    {
                        LoanList listD = LoanList.GetList(_filter_results);
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
            LoanAddForm form = new LoanAddForm(this);
            AddForm(form);
            if (form.ActionResult == DialogResult.OK) _entity = form.Entity;
        }

        public override void OpenViewForm() { AddForm(new LoanViewForm(ActiveOID, this)); }

        public override void OpenEditForm()
        {
            LoanEditForm form = new LoanEditForm(ActiveOID, this);
            if (form.Entity != null)
            {
                AddForm(form);
                _entity = form.Entity;
            }
        }

		public override void DeleteObject(long oid)
		{
            LoanInfo prestamo = LoanInfo.Get(ActiveOID, false);
            PaymentList pagos = PaymentList.GetListByPrestamo(prestamo, false);

            PaymentInfo p = pagos.FirstOrDefault(x => x.EEstado != EEstado.Anulado);

            if (pagos != null && (pagos.FirstOrDefault(x => x.EEstado != EEstado.Anulado) != default(PaymentInfo)))
            {
                PgMng.ShowErrorException(Resources.Messages.PRESTAMO_CON_PAGOS_ASOCIADOS);
                _action_result = DialogResult.Cancel;
            }
            else
            {
                if (prestamo.OidPago != 0)
                {
                    PgMng.ShowErrorException(Resources.Messages.PRESTAMO_ASOCIADO_PAGO_FACTURA);
                    _action_result = DialogResult.Cancel;
                }
                else
                {
                    Loan.Delete(oid);
                    _action_result = DialogResult.OK;
                }
            }
		}

        public override void UnlockAction() { ChangeStateAction(EEstadoItem.Unlock); }

        public override void ChangeStateAction(EEstadoItem estado)
        {
            _entity = Loan.ChangeEstado(ActiveOID, Library.Common.EnumConvert.ToEEstado(estado));

            _action_result = DialogResult.OK;
        }

		/// <summary>Imprime la lista del objetos
		/// <returns>void</returns>
		/// </summary>
		public override void PrintList() 
		{
			/*
			PgMng.Reset(3, 1, Face.Resources.Messages.LOADING_DATA, this);
			
			PrestamoReportMng reportMng = new PrestamoReportMng(AppContext.ActiveSchema);
			
			PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);			
			LoanListRpt report = reportMng.GetListReport(List);
			
			PgMng.FillUp();
			
			ShowReport(report);
			*/
        }

        #endregion
    }

    public partial class BankLoanMngForm : LoanMngForm
    {
        #region Attributes & Properties

        public new const string ID = "BankLoanMngForm";
        public new static Type Type { get { return typeof(BankLoanMngForm); } }

        #endregion

        #region Factory Methods

        public BankLoanMngForm(Form parent)
            : this(false, parent, null) { }

        public BankLoanMngForm(bool isModal, Form parent, LoanList list)
			: base(isModal, parent, list, ELoanType.Bank) {}

        #endregion
    }

    public partial class MerchantLoanMngForm : LoanMngForm
    {
        #region Attributes & Properties

        public new const string ID = "MerchantLoanMngForm";
        public new static Type Type { get { return typeof(MerchantLoanMngForm); } }

        #endregion

        #region Factory Methods

        public MerchantLoanMngForm(Form parent)
            : this(false, parent, null) { }

        public MerchantLoanMngForm(bool isModal, Form parent, LoanList list)
            : base(isModal, parent, list, ELoanType.Merchant) 
        {
            Text = Resources.Labels.PRESTAMOS_COMERCIO_EXTERIOR;
        }

        #endregion
    }

	public partial class LoanMngBaseForm : Skin06.EntityMngSkinForm<LoanList, LoanInfo>
	{
		public LoanMngBaseForm()
			: this(false, null, null) { }

		public LoanMngBaseForm(bool isModal, Form parent, LoanList list)
			: base(isModal, parent, list) { }
	}
}
