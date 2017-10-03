using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
//using Microsoft.PointOfService;
//using Microsoft.PointOfService.BaseServiceObjects;

using Csla;

using moleQule.Library;
using moleQule.Library.Reports;
using moleQule.Library.Common;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports.Ticket;

namespace moleQule.Library.Invoice
{
    public class TicketReportMng : Reports.ReportMng
    {
		#region Factory Methods

		public TicketReportMng() { }

		public TicketReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty) { }

		public TicketReportMng(ISchemaInfo empresa, string title)
			: this(empresa, title, string.Empty) { }

		public TicketReportMng(ISchemaInfo empresa, string title, string filter)
			: base(empresa, title, filter) { }

		#endregion

		#region Business Methods

		public ReportClass GetTicketReport(TicketInfo item)
        {
            if (item == null) return null;

            List<ConceptoTicketPrint> conceptos = new List<ConceptoTicketPrint>();
            List<TicketPrint> pList = new List<TicketPrint>();

            foreach (ConceptoTicketInfo cfi in item.ConceptoTickets)
                conceptos.Add(cfi.GetPrintObject());

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (conceptos.Count <= 0)
                return null;

            pList.Add(TicketPrint.New(item));
            
            ProductList productos = ProductList.GetList(false);

			ReportClass doc = null;

			try
			{
				doc = GetReportFromName("Ticket", "TicketRpt");
			}
			catch
			{
				doc = new TicketRpt();
			}

            doc.SetDataSource(pList);
            doc.Subreports["ConceptosRpt"].SetDataSource(conceptos);

            Library.Common.CompanyInfo empresa = Library.Common.CompanyInfo.Get(Schema.Oid, false);
            doc.SetParameterValue("nombreEmpresa", empresa.Name);
            doc.SetParameterValue("dirEmpresa", empresa.Direccion);
            doc.SetParameterValue("dir2Empresa", empresa.CodPostal + ". " + empresa.Municipio + ". " + empresa.Provincia);
            doc.SetParameterValue("CIFEmpresa", empresa.VatNumber);
            doc.SetParameterValue("TlfEmpresa", empresa.Telefonos);
            doc.SetParameterValue("FaxEmpresa", empresa.Fax);
			
            return doc;
        }

        public TicketListRpt GetListReport(TicketList list)
        {
            if (list.Count == 0) return null;

			TicketListRpt doc = new TicketListRpt();

            List<TicketPrint> pList = new List<TicketPrint>();

            foreach (TicketInfo item in list)
                pList.Add(TicketPrint.New(item, false));

            doc.SetDataSource(pList);

			FormatHeader(doc);

            return doc;
        }
        
		#endregion
   }

	/*public class CashDrawer : CashDrawerBase
	{

		public CashDrawer()
		{
		}

		protected override void  OpenDrawerImpl()
		{
 			
		}

		public void OpenCashDrawer()
		{

		}
	}*/
}
