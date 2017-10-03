using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice.Reports.Budget;
using moleQule.Library.Reports;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class BudgetReportMng : Reports.ReportMng
    {
		#region Factory Methods

		public BudgetReportMng() { }

		public BudgetReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty) { }

		public BudgetReportMng(ISchemaInfo empresa, string title)
			: this(empresa, title, string.Empty) { }

		public BudgetReportMng(ISchemaInfo empresa, string title, string filter)
			: base(empresa, title, filter) { }

		#endregion

		#region Style

		/*private static void FormatReport(ProformaRpt rpt, string logo)
        {
            /*string path = Images.GetRootPath() + "\\" + Resources.Paths.LOGO_EMPRESAS + logo;

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

        #region Business Methods

		public ReportClass GetDetailReport(BudgetInfo item, FormatConfFacturaAlbaranReport conf)
        {
            if (item == null) return null;
			
            List<BudgetPrint> pList = new List<BudgetPrint>();
            pList.Add(item.GetPrintObject());
			
			List<BudgetLinePrint> pConceptos = new List<BudgetLinePrint>();

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (item.Conceptos.Count <= 0)
                return null;

			foreach (BudgetLineInfo concepto in item.Conceptos)
				pConceptos.Add(BudgetLinePrint.New(concepto));

            ProductList productos = ProductList.GetList(false);
            foreach (BudgetLinePrint cfp in pConceptos)
            {
                if (cfp.OidProducto == 0) continue;
                ProductInfo prod = productos.GetItem(cfp.OidProducto);
                if (prod != null)
                {
                    if (prod.AyudaKilo > 0)
                        cfp.Concepto += " *";
                }
            }

			ReportClass doc = null;

			try
			{
                doc = GetReportFromName("Budget", "BudgetRpt");
			}
			catch
			{
				doc = new BudgetRpt();
			}

            doc.SetDataSource(pList);
			doc.Subreports["ConceptosRpt"].SetDataSource(pConceptos);

            CompanyInfo empresa = CompanyInfo.Get(Schema.Oid, false);
            doc.SetParameterValue("nombreEmpresa", empresa.Name);
            doc.SetParameterValue("dirEmpresa", empresa.Direccion);
			doc.SetParameterValue("CIFEmpresa", empresa.VatNumber);
            doc.SetParameterValue("TlfEmpresa", empresa.Telefonos);
            doc.SetParameterValue("CPEmpresa", empresa.CodPostal);
            doc.SetParameterValue("municipioEmpresa", empresa.Municipio);
            doc.SetParameterValue("nota", conf.nota);
            doc.SetParameterValue("WebEmpresa", empresa.Url);
		
            return doc;
        }

		public ReportClass GetListReport(BudgetList list, ClienteList clientes, SerieList series)
		{
			if (list.Count == 0) return null;

            BudgetListRpt doc = new BudgetListRpt();

			List<BudgetPrint> pList = new List<BudgetPrint>();
			
			foreach (BudgetInfo item in list)
			{
				pList.Add(item.GetPrintObject((clientes == null) ? null : clientes.GetItem(item.OidCliente),
                                                series.GetItem(item.OidSerie)));
			}
			
			doc.SetDataSource(pList);

			FormatHeader(doc);

			return doc;
		}
		
        #endregion
    }
}