using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice.Reports.Invoice;
using moleQule.Library.Reports;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    public class OutputInvoiceReportMng : Reports.ReportMng
    {
		#region Factory Methods

		public OutputInvoiceReportMng() { }

		public OutputInvoiceReportMng(ISchemaInfo company)
            : this(company, string.Empty) { }

        public OutputInvoiceReportMng(ISchemaInfo company, string title)
            : this(company, title, string.Empty) { }

        public OutputInvoiceReportMng(ISchemaInfo company, string title, string filter)
            : base(company, title, filter) { }

		#endregion

		#region Business Methods

		public ReportClass GetDetailReport(OutputInvoiceInfo item, 
											SerieInfo serie, 
											ClienteInfo cliente, 
											TransporterInfo transporter,
											FormatConfFacturaAlbaranReport conf)
        {
            if (item == null) return null;

            List<OutputInvoiceLinePrint> conceptos = new List<OutputInvoiceLinePrint>();
            List<OutputInvoicePrint> pList = new List<OutputInvoicePrint>();

            foreach (OutputInvoiceLineInfo cfi in item.ConceptoFacturas)
                conceptos.Add(cfi.GetPrintObject());

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (conceptos.Count <= 0)
                return null;

            pList.Add(OutputInvoicePrint.New(item, cliente, transporter, serie));
            
            ProductList productos = ProductList.GetList(false);
            foreach (OutputInvoiceLinePrint cfp in conceptos)
            {
                if (cfp.OidProducto == 0) continue;
               
				ProductInfo prod = productos.GetItem(cfp.OidProducto);
				if (prod != null) continue;

                if (prod.AyudaKilo > 0) cfp.Concepto += " *";
            }

			List<ImpuestoResumen> impuestos = new List<ImpuestoResumen>();

			foreach (DictionaryEntry impuesto in item.GetImpuestos())
				impuestos.Add((ImpuestoResumen)impuesto.Value);

			ReportClass doc = null;

			try
			{
				doc = GetReportFromName("Invoice", "OutputInvoiceRpt");
			}
			catch
			{
                doc = new OutputInvoiceRpt();
			}

            doc.SetDataSource(pList);
            doc.Subreports["ConceptosRpt"].SetDataSource(conceptos);

			if (doc.Subreports["ImpuestoResumenRpt"] != null)
				doc.Subreports["ImpuestoResumenRpt"].SetDataSource(impuestos);

            Library.Common.CompanyInfo empresa = Library.Common.CompanyInfo.Get(Schema.Oid, false);
            doc.SetParameterValue("nombreEmpresa", empresa.Name);
            doc.SetParameterValue("dirEmpresa", empresa.Direccion);
            doc.SetParameterValue("dir2Empresa", empresa.CodPostal + ". " + empresa.Municipio + ". " + empresa.Provincia);
            doc.SetParameterValue("CIFEmpresa", empresa.VatNumber);
            doc.SetParameterValue("TlfEmpresa", empresa.Telefonos);
            doc.SetParameterValue("FaxEmpresa", empresa.Fax);
			doc.SetParameterValue("WebEmpresa", empresa.Url);
            doc.SetParameterValue("nota", conf.nota);
            doc.SetParameterValue("ACuenta", (item.CobroFacturas != null) ? item.CobroFacturas.GetTotal() : 0);
            doc.SetParameterValue("CPEmpresa", empresa.CodPostal);
            doc.SetParameterValue("municipioEmpresa", empresa.Municipio);

            return doc;
        }

        public ReportClass GetListReport(OutputInvoiceList list, SerieList series)
        {
            if (list.Count == 0) return null;

            OutputInvoiceListRpt doc = new OutputInvoiceListRpt();

            List<OutputInvoicePrint> pList = new List<OutputInvoicePrint>();

            foreach (OutputInvoiceInfo item in list)
            {
                pList.Add(OutputInvoicePrint.New(item, 
                                           null, 
                                           null, 
                                           series.GetItem(item.OidSerie),
										   false));
            }

            doc.SetDataSource(pList);

			FormatHeader(doc);

            return doc;
        }

        public ReportClass GetBenefitsReport(OutputInvoiceList list)
        {
            if (list.Count == 0) return null;

            InformeBeneficioFacturaRpt doc = new InformeBeneficioFacturaRpt();

            List<OutputInvoicePrint> pList = new List<OutputInvoicePrint>();

            foreach (OutputInvoiceInfo item in list)
                pList.Add(OutputInvoicePrint.New(item, null, null, null, false));

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        #endregion
    }
}
