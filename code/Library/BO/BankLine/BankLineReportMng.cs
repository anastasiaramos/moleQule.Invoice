using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;
using moleQule.Library.Invoice.Reports.BankLine;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class BankLineReportMng : BaseReportMng
    {
		#region Factory Methods

		public BankLineReportMng() { }

		public BankLineReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty) { }

		public BankLineReportMng(ISchemaInfo empresa, string title)
			: this(empresa, title, string.Empty) { }

		public BankLineReportMng(ISchemaInfo empresa, string title, string filter)
			: base(empresa, title, filter) { }

		#endregion

        #region Business Methods

        public ReportClass GetListReport(BankLineList list)
        {
            if (list == null) return null;

            BankLineListRpt doc = new BankLineListRpt();

            List<BankLineInfo> pList = new List<BankLineInfo>();

            foreach (BankLineInfo item in list)
                pList.Add(BankLinePrint.New(item));

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        #endregion    
    }
}