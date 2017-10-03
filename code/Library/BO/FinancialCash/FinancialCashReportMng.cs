using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;
using moleQule.Library.Invoice.Reports.FinancialCash;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class FinancialCashReportMng : BaseReportMng
    {	
        #region Factory Methods

        public FinancialCashReportMng() {}

        public FinancialCashReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public FinancialCashReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public FinancialCashReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(IVEffectRpt rpt, string logo)
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

        #region Business Methods IVEffect
		
        //public IVEffectRpt GetDetailReport(EffectInfo item)
        //{
        //    if (item == null) return null;
			
        //    IVEffectRpt doc = new IVEffectRpt();
            
        //    List<IVEffectPrint> pList = new List<IVEffectPrint>();

        //    pList.Add(IVEffectPrint.New(item));
        //    doc.SetDataSource(pList);
        //    doc.SetParameterValue("Empresa", Schema.Name);
			
			

        //    //FormatReport(doc, empresa.Logo);

        //    return doc;
        //}

        public ReportClass GetListReport(FinancialCashList list)
        {
            if (list.Count == 0) return null;

            FinancialCashListRpt doc = new FinancialCashListRpt();

            List<FinancialCashPrint> pList = new List<FinancialCashPrint>();

            foreach (FinancialCashInfo item in list)
            {
                pList.Add(FinancialCashPrint.New(item));
            }

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }
		
        #endregion
    }
}
