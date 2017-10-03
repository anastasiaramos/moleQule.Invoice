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
    public class TraspasoReportMng : BaseReportMng
    {
	
        #region Factory Methods

        public TraspasoReportMng() {}

        public TraspasoReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public TraspasoReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public TraspasoReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(TraspasoRpt rpt, string logo)
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

        #region Business Methods Traspaso
		
        /*public TraspasoRpt GetDetailReport(TraspasoInfo item)
        {
            if (item == null) return null;
			
            TraspasoRpt doc = new TraspasoRpt();
            
            List<TraspasoPrint> pList = new List<TraspasoPrint>();

            pList.Add(TraspasoPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public TraspasoListRpt GetListReport(TraspasoList list)
		{
			if (list.Count == 0) return null;

			TraspasoListRpt doc = new ClienteListRpt();

			List<TraspasoPrint> pList = new List<TraspasoPrint>();
			
			foreach (TraspasoInfo item in list)
			{
				pList.Add(TraspasoPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}*/
		
        #endregion

    }
}
