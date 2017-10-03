using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Reports;
using moleQule.Library.Invoice.Reports.Cobro;

namespace moleQule.Library.Invoice
{
    public class CobroReportMng : BaseReportMng
	{
		#region Attributes & Properties

		public bool ShowQRCode { get; set; }

		#endregion

		#region Factory Methods

		public CobroReportMng() { }

		public CobroReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty, string.Empty) { }

		public CobroReportMng(ISchemaInfo empresa, string title, string filtro)
			: base(empresa, title, filtro) 
		{ 
			ShowQRCode = false;
		}

		#endregion

        #region Business Methods

        public ChargeListRpt GetListReport(ChargeList list, CobroFacturaList c_facturas)
        {
            if (list == null) return null;

            ChargeListRpt doc = new ChargeListRpt();

            List<ChargeInfo> pList = new List<ChargeInfo>();

			foreach (ChargeInfo cobro in list)
			{
				if (ShowQRCode)
					cobro.LoadChilds(c_facturas.GetSubList(new FCriteria<long>("OidCobro", cobro.Oid, Operation.Equal)));

				pList.Add(CobroPrint.New(cobro, ShowQRCode));
			}

            doc.SetDataSource(pList);

            FormatHeader(doc);

			doc.QRCodeSection.SectionFormat.EnableSuppress = !ShowQRCode;

            return doc;
        }

        public CobroClienteListRpt GetCobroClienteListReport(ChargeSummaryList list)
        {
            if (list == null) return null;

            CobroClienteListRpt doc = new CobroClienteListRpt();

            List<ChargeSummary> pList = new List<ChargeSummary>();

            foreach (ChargeSummary cobro in list)
                pList.Add(cobro);

            doc.SetDataSource(pList);
            //doc.SetParameterValue("Empresa", Schema.Name);
            //doc.SetParameterValue("Title", Title);
            //doc.SetParameterValue("Filter", Filter);

            FormatHeader(doc);

            return doc;
        }

		public CobroREAListRpt GetCobroREAListReport(ChargeList list, CobroREAList c_reas)
        {
            if (list == null) return null;

            CobroREAListRpt doc = new CobroREAListRpt();

            List<CobroPrint> pList = new List<CobroPrint>();

			foreach (ChargeInfo cobro in list)
			{
				if (c_reas != null && ShowQRCode)
					cobro.LoadChilds(c_reas.GetSubList(new FCriteria<long>("OidCobro", cobro.Oid, Operation.Equal)));

				pList.Add(CobroPrint.New(cobro, ShowQRCode));
			}

            doc.SetDataSource(pList);

			FormatHeader(doc);

            return doc;
        }

        public CobroClienteDetailRpt GetCobroClienteDetailReport(ClienteInfo item)
        {
            if (item == null) return null;

			CobroClienteDetailRpt doc = new CobroClienteDetailRpt();

            List<CobroPrint> cobros = new List<CobroPrint>();
            List<ClientePrint> pList = new List<ClientePrint>();

			foreach (ChargeInfo cobro in item.Cobros)
			{
				if ((cobro.EEstado != Common.EEstado.Anulado))
					cobros.Add(CobroPrint.New(cobro, ShowQRCode));
			}

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (cobros.Count <= 0) return null;

            pList.Add(item.GetPrintObject());
            doc.SetDataSource(pList);
            doc.Subreports["Cuerpo"].SetDataSource(cobros);
			
			FormatHeader(doc);

            return doc;
        }
		public CobroClienteDetailRpt GetCobroClienteDetailReport(ClienteInfo item, ChargeSummary resumen)
		{
			if (item == null) return null;

			CobroClienteDetailRpt doc = new CobroClienteDetailRpt();

			List<CobroPrint> cobros = new List<CobroPrint>();
			List<ClientePrint> pList = new List<ClientePrint>();

			foreach (ChargeInfo cobro in item.Cobros)
			{
				if ((cobro.EEstado != Common.EEstado.Anulado))
					cobros.Add(CobroPrint.New(cobro, ShowQRCode));
			}

			//Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
			if (cobros.Count <= 0) return null;

			pList.Add(ClientePrint.New(item, resumen));
			doc.SetDataSource(pList);
			doc.Subreports["Cuerpo"].SetDataSource(cobros);

			FormatHeader(doc);

			return doc;
		}

        public CobroDetailRpt GetDetallesCobroIndividualReport(ChargeInfo item)
        {
            if (item == null) return null;

			CobroDetailRpt doc = new CobroDetailRpt();

            List<CobroFacturaPrint> cobros = new List<CobroFacturaPrint>();
            List<CobroPrint> pList = new List<CobroPrint>();

            ChargeInfo cob;
            if (item.CobroFacturas == null)
                cob = ChargeInfo.Get(item.Oid, true);
            else
                cob = item;

            foreach (CobroFacturaInfo cfi in cob.CobroFacturas)
                cobros.Add(cfi.GetPrintObject());

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (cobros.Count <= 0)
                return null;

			pList.Add(CobroPrint.New(item, ShowQRCode));
            doc.Subreports["Cuerpo"].SetDataSource(cobros);

            doc.SetDataSource(pList);
            doc.SetParameterValue("Empresa", Schema.Name);

            return doc;
        }

        public CobroREADetailRpt GetDetallesCobroREAIndividualReport(ChargeInfo item, FacREAList expedientes)
        {
            if (item == null) return null;

			CobroREADetailRpt doc = new CobroREADetailRpt();

            List<FacREAInfo> cobros = new List<FacREAInfo>();
            List<CobroPrint> pList = new List<CobroPrint>();

            ChargeInfo cob;
            if (item.CobroFacturas == null)
                cob = ChargeInfo.Get(item.Oid, true);
            else
                cob = item;

            foreach (FacREAInfo frea in expedientes)
                cobros.Add(frea);

            //Si no existen conceptos, no tiene sentido un informe detallado. Además, falla en Crystal Reports
            if (cobros.Count <= 0)
                return null;

			pList.Add(CobroPrint.New(item, ShowQRCode));
            doc.Subreports["Cuerpo"].SetDataSource(cobros);

            doc.SetDataSource(pList);
            doc.SetParameterValue("Empresa", Schema.Name);

            return doc;
        }

        #endregion

        #region Informe de Cobros

        public InformeCobrosRpt GetInformeCobrosReport(CobroFacturaList list, OutputInvoiceList facturas)
        {
            InformeCobrosRpt doc = new InformeCobrosRpt();

            List<CobroFacturaPrint> pList = new List<CobroFacturaPrint>();

            foreach (CobroFacturaInfo item in list)
                pList.Add(CobroFacturaPrint.New(item, null, facturas.GetItem(item.OidFactura)));

            if (pList.Count == 0) return null;

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        #endregion


    }
}
