using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Cash;

namespace moleQule.Face.Invoice
{
	public partial class CashActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public const string ID = "CashActionForm";
        public static Type Type { get { return typeof(CashActionForm); } }
        
        private Cash _caja;

        #endregion

        #region Factory Methods

        public CashActionForm(Cash caja)
            : this(null, caja) { }

        public CashActionForm(Form parent, Cash caja)
            : base(true, parent)
        {
            InitializeComponent();
            SetFormData();
            _caja = caja;
        }

        #endregion

        #region Business Methods

        private string GetFilterValues()
        {
            string filtro = string.Empty;

            if (FInicial_DTP.Checked)
                filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.GreaterOrEqual) + " " + FInicial_DTP.Value.ToString("g") + "; ";

            if (FFinal_DTP.Checked)
                filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.LessOrEqual) + " " + FFinal_DTP.Value.ToString("g") + "; ";

            return filtro;
        }

        #endregion

        #region Layout & Source
       
        public override void RefreshSecondaryData()
        {
            FInicial_DTP.Checked = true;
            FFinal_DTP.Checked = true;
            FInicial_DTP.Value = DateTime.Today;
            FFinal_DTP.Value = DateTime.Today.AddDays(1);
        }

        #endregion

        #region Actions

        protected override void PrintAction()
        {
            List<CashLine> subLista = _caja.Lines.GetSubList(new Library.CslaEx.FCriteria<DateTime>("Fecha", 
                                                                                FInicial_DTP.Value,
                                                                                FFinal_DTP.Value, 
                                                                                Library.CslaEx.Operation.Between));

            CashLineList lista = CashLineList.GetChildList(subLista);

            PrintListAction(_caja, lista);

            _action_result = DialogResult.OK;
        }

        protected void PrintListAction(Cash caja, CashLineList lista)
        {
            string filtro = GetFilterValues();

            CashReportMng reportMng = new CashReportMng(AppContext.ActiveSchema, Resources.Labels.CAJA_REPORT_TITLE, filtro);
            CashRpt report = reportMng.GetDetailReport(caja.GetInfo(), lista, FInicial_DTP.Value, FFinal_DTP.Value);

			ShowReport(report);
        }
        
        #endregion

        #region Events

        #endregion
    }
}