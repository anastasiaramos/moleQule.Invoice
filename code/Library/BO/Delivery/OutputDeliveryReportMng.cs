using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;
using moleQule.Library.Store;
using moleQule.Library.Common;
using moleQule.Library.Invoice.Reports.Delivery;

namespace moleQule.Library.Invoice
{
    public class OutputDeliveryReportMng : Reports.ReportMng
    {
		#region Factory Methods

		public OutputDeliveryReportMng() { }

		public OutputDeliveryReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty, string.Empty) { }

		public OutputDeliveryReportMng(ISchemaInfo empresa, string title, string filtro)
			: base(empresa, title, filtro) { }

		#endregion

        #region Business Methods

		public ReportClass GetDetailReport(OutputDeliveryInfo item, FormatConfFacturaAlbaranReport conf)
		{
			if (item == null) return null;

			List<OutputDeliveryLinePrint> conceptos = new List<OutputDeliveryLinePrint>();
			List<OutputDeliveryPrint> pList = new List<OutputDeliveryPrint>();

			foreach (OutputDeliveryLineInfo cfi in item.Conceptos)
				conceptos.Add(OutputDeliveryLinePrint.New(cfi));

			//Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
			if (conceptos.Count <= 0)
				return null;

			pList.Add(item.GetPrintObject());

			ReportClass doc = null;

			try
			{
				doc = GetReportFromName("Delivery", "OutputDeliveryRpt");
			}
			catch
			{
                doc = new OutputDeliveryRpt();
			}

			doc.SetDataSource(pList);
            doc.Subreports["LinesClientCopy"].SetDataSource(conceptos);
            //doc.Subreports["LinesCompanyCopy"].SetDataSource(conceptos);

			CompanyInfo empresa = CompanyInfo.Get(Schema.Oid);
			doc.SetParameterValue("nombreEmpresa", empresa.Name);
			doc.SetParameterValue("dirEmpresa", empresa.Direccion);
			doc.SetParameterValue("CIFEmpresa", empresa.VatNumber);
            doc.SetParameterValue("nota", conf.nota); 
            doc.SetParameterValue("CPEmpresa", empresa.CodPostal);
            doc.SetParameterValue("municipioEmpresa", empresa.Municipio);
            doc.SetParameterValue("TlfEmpresa", empresa.Telefonos);
            doc.SetParameterValue("WebEmpresa", empresa.Url);

			return doc;
		}

        public ReportClass GetDetailListReport(OutputDeliveryList list,
                                                ClienteList clientes,
                                                ETipoAlbaranes tipo,
                                                DateTime fini,
                                                DateTime ffin)
        {
            if (list.Count == 0) return null;

            AlbaranDetailListRpt doc = new AlbaranDetailListRpt();

            List<OutputDeliveryPrint> pList = new List<OutputDeliveryPrint>();
            List<OutputDeliveryLinePrint> conceptos = new List<OutputDeliveryLinePrint>();

            foreach (OutputDeliveryInfo item in list)
            {
                pList.Add(OutputDeliveryPrint.New(item,
                                           clientes.GetItem(item.OidHolder),
                                           null));

                foreach (OutputDeliveryLineInfo cp in item.Conceptos)
                    conceptos.Add(OutputDeliveryLinePrint.New(cp));
            }

            doc.SetDataSource(pList);
            doc.Subreports["Conceptos"].SetDataSource(conceptos);
            doc.SetParameterValue("Empresa", Schema.Name);
            doc.SetParameterValue("Tipo", tipo.ToString());
            doc.SetParameterValue("FIni", fini);
            doc.SetParameterValue("FFin", ffin);

            return doc;
        }


        public ReportClass GetAlbaran(OutputDeliveryInfo item, FormatConfFacturaAlbaranReport conf)
		{
			if (item == null) return null;

			List<OutputDeliveryLinePrint> conceptos = new List<OutputDeliveryLinePrint>();
			List<OutputDeliveryPrint> pList = new List<OutputDeliveryPrint>();

			foreach (OutputDeliveryLineInfo cfi in item.Conceptos)
				conceptos.Add(OutputDeliveryLinePrint.New(cfi));

			//Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
			if (conceptos.Count <= 0)
				return null;

			pList.Add(item.GetPrintObject());

			ProductList productos = ProductList.GetList(false);
			foreach (OutputDeliveryLinePrint cfp in conceptos)
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
                doc = GetReportFromName("Delivery", "OutputDeliveryRpt");
			}
			catch
			{
				doc = new OutputDeliveryRpt();
			}

			doc.Subreports["ConceptosRpt"].SetDataSource(conceptos);

			doc.SetDataSource(pList);
			CompanyInfo empresa = CompanyInfo.Get(Schema.Oid);
			doc.SetParameterValue("nombreEmpresa", empresa.Name);
			doc.SetParameterValue("dirEmpresa", empresa.Direccion);
			doc.SetParameterValue("CIFEmpresa", empresa.VatNumber);
			doc.SetParameterValue("TlfEmpresa", empresa.Telefonos);
			doc.SetParameterValue("nota", (conf.nota != null) ? conf.nota : string.Empty);
            
			return doc;
		}

        public ReportClass GetListReport(OutputDeliveryList list,                                                                           
                                                    ClienteList clientes)
        {
            if (list.Count == 0) return null;

            OutputDeliveryListRpt doc = new OutputDeliveryListRpt();

            List<OutputDeliveryPrint> pList = new List<OutputDeliveryPrint>();

            foreach (OutputDeliveryInfo item in list)
            {
                pList.Add(OutputDeliveryPrint.New(item,
                                           clientes.GetItem(item.OidHolder), 
                                           null));
            }

            doc.SetDataSource(pList);

			FormatHeader(doc);

            return doc;
        }

        public ReportClass GetWorkDelivery(OutputDeliveryInfo item, ExpedientInfo work)
        {
            if (item == null) return null;

            List<OutputDeliveryLinePrint> conceptos = new List<OutputDeliveryLinePrint>();
            List<OutputDeliveryPrint> pList = new List<OutputDeliveryPrint>();

            foreach (OutputDeliveryLineInfo line in item.Conceptos)
                conceptos.Add(OutputDeliveryLinePrint.New(line));

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (conceptos.Count <= 0)
                return null;

            pList.Add(item.GetPrintObject());

            ReportClass doc = null;

            try
            {
                doc = GetReportFromName("Delivery", "WorkDeliveryRpt");
            }
            catch
            {
                doc = new WorkDeliveryRpt();
            }

            doc.SetDataSource(pList);
            doc.Subreports["LinesClientCopySubRpt"].SetDataSource(conceptos);
            doc.Subreports["LinesCompanyCopySubRpt"].SetDataSource(conceptos);

            CompanyInfo empresa = CompanyInfo.Get(Schema.Oid);
            doc.SetParameterValue("nombreEmpresa", empresa.Name);
            doc.SetParameterValue("dirEmpresa", empresa.Direccion);
            doc.SetParameterValue("CIFEmpresa", empresa.VatNumber);
            doc.SetParameterValue("TlfEmpresa", empresa.Telefonos);
            doc.SetParameterValue("WebEmpresa", empresa.Url);
            doc.SetParameterValue("WorkCode", (work != null) ? work.Codigo : string.Empty);
            doc.SetParameterValue("WorkDescription", (work != null) ? work.Description : string.Empty);

            return doc;
        }

        public ReportClass GetWorkDeliveryList(OutputDeliveryList list, ExpedienteList expedients)
        {
            if (list.Count == 0) return null;

            OutputDeliveryListRpt doc = new OutputDeliveryListRpt();

            List<OutputDeliveryPrint> pList = new List<OutputDeliveryPrint>();

            foreach (OutputDeliveryInfo item in list)
            {
                pList.Add(OutputDeliveryPrint.New(item, expedients.GetItem(item.OidHolder)));
            }

            doc.SetDataSource(pList);

            FormatHeader(doc);

            ((TextObject)doc.Section2.ReportObjects["IDHolder_LB"]).Text = Library.Store.Resources.Labels.WORK_ID;
            ((TextObject)doc.Section2.ReportObjects["Holder_LB"]).Text = Library.Store.Resources.Labels.WORK;
            doc.Section2.ReportObjects["NFactura_LB"].ObjectFormat.EnableSuppress = true;

            return doc;
        }

		#endregion
    }
}
