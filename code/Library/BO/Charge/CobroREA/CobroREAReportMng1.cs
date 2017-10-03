using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class CobroREAReportMng : BaseReportMng
    {
	
        #region Factory Methods

        public CobroREAReportMng() {}

        public CobroREAReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public CobroREAReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public CobroREAReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(CobroREARpt rpt, string logo)
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

        #region Business Methods CobroREA
		
        public CobroREARpt GetDetailReport(CobroREAInfo item)
        {
            if (item == null) return null;
			
            CobroREARpt doc = new CobroREARpt();
            
            List<CobroREAPrint> pList = new List<CobroREAPrint>();

            pList.Add(CobroREAPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			
			List<IVChargePrint> pIVCharges = new List<IVChargePrint>();
            
			foreach (IVChargeInfo child in item.IVCharges)
			{
				pIVCharges.Add(IVChargePrint.New(child));
			}

			doc.Subreports["IVChargeSubRpt"].SetDataSource(pIVCharges);
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public CobroREAListRpt GetListReport(CobroREAList list)
		{
			if (list.Count == 0) return null;

			CobroREAListRpt doc = new ClienteListRpt();

			List<CobroREAPrint> pList = new List<CobroREAPrint>();
			
			foreach (CobroREAInfo item in list)
			{
				pList.Add(CobroREAPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion

    }
}
