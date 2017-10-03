using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class TransactionReportMng : BaseReportMng
    {
	
        #region Factory Methods

        public TransactionReportMng() {}

        public TransactionReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public TransactionReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public TransactionReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(TransactionRpt rpt, string logo)
        {
            string path = Images.GetRootPath() + "\\" + Resources.Paths.LOGO_EMPRESAS + logo;

            if (File.Exists(path))
            {
                Image image = Image.FromFile(path);
                int width = rpt.Section1.ReportObjects["Logo"].Width;
                int height = rpt.Section1.ReportObjects["Logo"].Height;

                rpt.Section1.ReportObjects["Logo"].Width = 15 * image.Width;
                rpt.Section1.ReportObjects["Logo"].Height = 15 * image.Height;
                rpt.Section1.ReportObjects["Logo"].Left += (width - 15 * image.Width) / 2;
                rpt.Section1.ReportObjects["Logo"].Top += (height - 15 * image.Height) / 2;
            }
        }*/

        #endregion

        #region Business Methods Transaction
		
       /* public TransactionRpt GetDetailReport(TransactionInfo item)
        {
            if (item == null) return null;
			
            TransactionRpt doc = new TransactionRpt();
            
            List<TransactionPrint> pList = new List<TransactionPrint>();

            pList.Add(TransactionPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public TransactionListRpt GetListReport(TransactionList list)
		{
			if (list.Count == 0) return null;

			TransactionListRpt doc = new ClienteListRpt();

			List<TransactionPrint> pList = new List<TransactionPrint>();
			
			foreach (TransactionInfo item in list)
			{
				pList.Add(TransactionPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}*/
		
        #endregion

    }
}
