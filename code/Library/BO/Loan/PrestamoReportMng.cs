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
    public class PrestamoReportMng : BaseReportMng
    {
	
        #region Factory Methods

        public PrestamoReportMng() {}

        public PrestamoReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public PrestamoReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public PrestamoReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(PrestamoRpt rpt, string logo)
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

        #region Business Methods Prestamo
		
        public PrestamoRpt GetDetailReport(PrestamoInfo item)
        {
            if (item == null) return null;
			
            PrestamoRpt doc = new PrestamoRpt();
            
            List<PrestamoPrint> pList = new List<PrestamoPrint>();

            pList.Add(PrestamoPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			
			List<PagoPrint> pPagos = new List<PagoPrint>();
            
			foreach (PagoInfo child in item.Pagos)
			{
				pPagos.Add(PagoPrint.New(child));
			}

			doc.Subreports["PagoSubRpt"].SetDataSource(pPagos);
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public PrestamoListRpt GetListReport(PrestamoList list)
		{
			if (list.Count == 0) return null;

			PrestamoListRpt doc = new ClienteListRpt();

			List<PrestamoPrint> pList = new List<PrestamoPrint>();
			
			foreach (PrestamoInfo item in list)
			{
				pList.Add(PrestamoPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion

    }
}
