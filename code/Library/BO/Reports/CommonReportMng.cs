using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using CrystalDecisions.CrystalReports.Engine;

using Csla;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Reports;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports;
using moleQule.Library.Invoice.Reports.Ventas;
using moleQule.Library.Invoice.Reports.Modelo;
using moleQule.Library.Invoice.Reports.Balance;
using moleQule.Library.Store.Reports.Invoice;

namespace moleQule.Library.Invoice
{
    public class CommonReportMng : BaseReportMng
    {
		#region Factory Methods

		public CommonReportMng() { }

		public CommonReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty) { }

		public CommonReportMng(ISchemaInfo empresa, string title)
			: this(empresa, title, string.Empty) { }

		public CommonReportMng(ISchemaInfo empresa, string title, string filter)
			: base(empresa, title, filter) { }

		#endregion

        #region Ventas

        public InformeVentasClientesRpt GetVentasClientesReport(VentasList list, bool detallado)
        {
            InformeVentasClientesRpt doc = new InformeVentasClientesRpt();

            List<VentasInfo> pList = new List<VentasInfo>();

            foreach (VentasInfo item in list)
                pList.Add(item);

            if (pList.Count == 0) return null;

            doc.SetDataSource(pList);

            FormatHeader(doc);

            doc.EncabezadoDetallado.SectionFormat.EnableSuppress = !detallado;
            doc.Detalles.SectionFormat.EnableSuppress = !detallado;
            doc.PieDetallado.SectionFormat.EnableSuppress = !detallado;
            doc.Resumen.SectionFormat.EnableSuppress = detallado;

            doc.Encabezado.ReportObjects["Producto_LB"].ObjectFormat.EnableSuppress = !detallado;
            doc.Encabezado.ReportObjects["Cliente_LB"].ObjectFormat.EnableSuppress = detallado;
            doc.Encabezado.ReportObjects["Expediente_LB"].ObjectFormat.EnableSuppress = !detallado;
            doc.Encabezado.ReportObjects["Factura_LB"].ObjectFormat.EnableSuppress = !detallado;
            doc.Encabezado.ReportObjects["PMC_LB"].ObjectFormat.EnableSuppress = !detallado;
            //doc.Encabezado.ReportObjects["PMV_LB"].ObjectFormat.EnableSuppress = !detallado;

            return doc;
        }

        public InformeVentasProductosRpt GetVentasProductosReport(VentasList list, bool detallado)
        {
            InformeVentasProductosRpt doc = new InformeVentasProductosRpt();

            List<VentasInfo> pList = new List<VentasInfo>();

            foreach (VentasInfo item in list)
                pList.Add(item);

            doc.SetDataSource(pList);

            FormatHeader(doc);

            doc.EncabezadoDetallado.SectionFormat.EnableSuppress = !detallado;
            doc.Detalles.SectionFormat.EnableSuppress = !detallado;
            doc.PieDetallado.SectionFormat.EnableSuppress = !detallado;
            doc.Resumen.SectionFormat.EnableSuppress = detallado;

            doc.Encabezado.ReportObjects["Producto_LB"].ObjectFormat.EnableSuppress = detallado;
            doc.Encabezado.ReportObjects["Cliente_LB"].ObjectFormat.EnableSuppress = !detallado;
            doc.Encabezado.ReportObjects["Expediente_LB"].ObjectFormat.EnableSuppress = !detallado;
            doc.Encabezado.ReportObjects["Factura_LB"].ObjectFormat.EnableSuppress = !detallado;
            doc.Encabezado.ReportObjects["PMC_LB"].ObjectFormat.EnableSuppress = !detallado;

            return doc;
        }

        public InformeVentasExpedientesRpt GetVentasExpedienteReport(VentasList list, bool detallado)
        {
            if (list.Count == 0) return null;

            InformeVentasExpedientesRpt doc = new InformeVentasExpedientesRpt();

            List<VentasInfo> pList = new List<VentasInfo>();

            foreach (VentasInfo item in list)
                pList.Add(item);

            doc.SetDataSource(pList);

            FormatHeader(doc);

            doc.EncabezadoDetallado.SectionFormat.EnableSuppress = !detallado;
            doc.Detalles.SectionFormat.EnableSuppress = !detallado;
            doc.PieDetallado.SectionFormat.EnableSuppress = !detallado;
            doc.Resumen.SectionFormat.EnableSuppress = detallado;

            doc.Encabezado.ReportObjects["Producto_LB"].ObjectFormat.EnableSuppress = !detallado;
            doc.Encabezado.ReportObjects["Cliente_LB"].ObjectFormat.EnableSuppress = !detallado;
            doc.Encabezado.ReportObjects["Expediente_LB"].ObjectFormat.EnableSuppress = detallado;
            doc.Encabezado.ReportObjects["Factura_LB"].ObjectFormat.EnableSuppress = !detallado;
            doc.Encabezado.ReportObjects["PMC_LB"].ObjectFormat.EnableSuppress = !detallado;

            return doc;
        }

        public InformeVentasMensualRpt GetVentasMensualReport(VentasList list)
        {
            List<VentasInfo> pList = new List<VentasInfo>();

            foreach (VentasInfo item in list)
                pList.Add(item);

            if (pList.Count == 0) return null;

            InformeVentasMensualRpt doc = new InformeVentasMensualRpt();

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        public InformeVentasMensualxClienteRpt GetVentasMensualxClienteReport(VentasList list)
        {
            List<VentasInfo> pList = new List<VentasInfo>();

            foreach (VentasInfo item in list)
                pList.Add(item);

            if (pList.Count == 0) return null;

            InformeVentasMensualxClienteRpt doc = new InformeVentasMensualxClienteRpt();

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        public InformeVentasMensualxProductoRpt GetVentasMensualxProductoReport(VentasList list)
        {
            List<VentasInfo> pList = new List<VentasInfo>();

            foreach (VentasInfo item in list)
                pList.Add(item);

            if (pList.Count == 0) return null;

            InformeVentasMensualxProductoRpt doc = new InformeVentasMensualxProductoRpt();

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

		public InformeVentasPorcentualxClienteRpt GetVentasPorcentualxClienteReport(VentasList list, QueryConditions conditions)
		{
			List<VentasInfo> pList = new List<VentasInfo>();

			foreach (VentasInfo item in list)
				pList.Add(item);

			if (pList.Count == 0) return null;

			InformeVentasPorcentualxClienteRpt doc = new InformeVentasPorcentualxClienteRpt();

			if (conditions.Cliente != null)
				doc.ResumenPorCliente.SectionFormat.EnableNewPageAfter = false;

			doc.SetDataSource(pList);

			FormatHeader(doc);

			return doc;
		}

        public InformeVentasPorcentualPeriodoxClienteRpt GetVentasPorcentualPeriodoxClienteReport(VentasList list, QueryConditions conditions)
        {
            List<VentasInfo> pList = new List<VentasInfo>();

            foreach (VentasInfo item in list)
                pList.Add(item);

            if (pList.Count == 0) return null;

            InformeVentasPorcentualPeriodoxClienteRpt doc = new InformeVentasPorcentualPeriodoxClienteRpt();
            
            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

		public InformeVentasPorcentualxProductoRpt GetVentasPorcentualxProductoReport(VentasList list, QueryConditions conditions)
		{
			List<VentasInfo> pList = new List<VentasInfo>();

			foreach (VentasInfo item in list)
				pList.Add(item);

			if (pList.Count == 0) return null;

			InformeVentasPorcentualxProductoRpt doc = new InformeVentasPorcentualxProductoRpt();
			
			if (conditions.Producto != null)
				doc.ResumenPorProducto.SectionFormat.EnableNewPageAfter = false;

			doc.SetDataSource(pList);

			FormatHeader(doc);

			return doc;
		}

        #endregion

		#region Modelos

		public Modelo347Rpt GetModelo347Report(ModeloList list_c,
												ModeloList list_c_efectivo,
												Modelo modelo)
		{
			Modelo347Rpt doc = new Modelo347Rpt();

			List<ModeloPrint> pList = new List<ModeloPrint>();
			List<ModeloPrint> pList_efectivo = new List<ModeloPrint>();

			foreach (ModeloInfo item in list_c)
				pList.Add(ModeloPrint.New(item));

			foreach (ModeloInfo item in list_c_efectivo)
				pList_efectivo.Add(ModeloPrint.New(item));

			doc.Subreports["Operaciones"].SetDataSource(pList);
			doc.Subreports["Operaciones_Efectivo"].SetDataSource(pList_efectivo);
			doc.SetParameterValue("MinOperaciones", modelo.MinImporte);
			doc.SetParameterValue("MinOperacionesEfectivo", modelo.MinEfectivo);

			FormatHeader(doc);

			return doc;
		}

		public Modelo420Rpt GetModelo420Report(ModeloList soportado, ModeloList repercutido)
		{
			Modelo420Rpt doc = new Modelo420Rpt();

			List<ModeloPrint> pList_soportado = new List<ModeloPrint>();
			List<ModeloPrint> pList_repercutido = new List<ModeloPrint>();

			foreach (ModeloInfo item in soportado)
				pList_soportado.Add(ModeloPrint.New(item));

			foreach (ModeloInfo item in repercutido)
				pList_repercutido.Add(ModeloPrint.New(item));

			decimal totalModelo = repercutido.TotalRepercutido(EPeriodo.Anual) - soportado.TotalSoportado(EPeriodo.Anual) - soportado.TotalSoportadoImportacion(EPeriodo.Anual);
			decimal totalModelo1T = repercutido.TotalRepercutido(EPeriodo.Periodo1T) - soportado.TotalSoportado(EPeriodo.Periodo1T) - soportado.TotalSoportadoImportacion(EPeriodo.Periodo1T);
			decimal totalModelo2T = repercutido.TotalRepercutido(EPeriodo.Periodo2T) - soportado.TotalSoportado(EPeriodo.Periodo2T) - soportado.TotalSoportadoImportacion(EPeriodo.Periodo2T);
			decimal totalModelo3T = repercutido.TotalRepercutido(EPeriodo.Periodo3T) - soportado.TotalSoportado(EPeriodo.Periodo3T) - soportado.TotalSoportadoImportacion(EPeriodo.Periodo3T);
			decimal totalModelo4T = repercutido.TotalRepercutido(EPeriodo.Periodo4T) - soportado.TotalSoportado(EPeriodo.Periodo4T) - soportado.TotalSoportadoImportacion(EPeriodo.Periodo4T);

			doc.Subreports["Soportado"].SetDataSource(pList_soportado);
			doc.Subreports["SoportadoImportacion"].SetDataSource(pList_soportado);
			doc.Subreports["Repercutido"].SetDataSource(pList_repercutido);
			
			doc.SetParameterValue("TotalModelo", (totalModelo < 0) ? 0 : totalModelo);
			doc.SetParameterValue("TotalModelo1T", (totalModelo1T < 0) ? 0 : totalModelo1T);
			doc.SetParameterValue("TotalModelo2T", (totalModelo2T < 0) ? 0 : totalModelo2T);
			doc.SetParameterValue("TotalModelo3T", (totalModelo3T < 0) ? 0 : totalModelo3T);
			doc.SetParameterValue("TotalModelo4T", (totalModelo4T < 0) ? 0 : totalModelo4T);

			FormatHeader(doc);

			return doc;
		}

		public Modelo111Rpt GetModelo111Report(ModeloList empleados_trabajo, 
												ModeloList empleados_especie, 
												ModeloList profesionales,
                                                InputInvoiceList facturas)
		{
			Modelo111Rpt doc = new Modelo111Rpt();

			List<ModeloPrint> pList_empleados_trabajo = new List<ModeloPrint>();
			List<ModeloPrint> pList_empleados_especie = new List<ModeloPrint>();
			List<ModeloPrint> pList_profesionales = new List<ModeloPrint>();
			List<FacturaRecibidaPrint> pList_facturas = new List<FacturaRecibidaPrint>();

			foreach (ModeloInfo item in empleados_trabajo)
				pList_empleados_trabajo.Add(ModeloPrint.New(item));

			foreach (ModeloInfo item in empleados_especie)
				pList_empleados_especie.Add(ModeloPrint.New(item));

			foreach (ModeloInfo item in profesionales)
				pList_profesionales.Add(ModeloPrint.New(item));

			foreach (InputInvoiceInfo item in facturas)
				pList_facturas.Add(FacturaRecibidaPrint.New(item));

			decimal totalModelo = empleados_trabajo.Total(EPeriodo.Anual) + empleados_especie.Total(EPeriodo.Anual) + profesionales.Total(EPeriodo.Anual);
			decimal totalModelo1T = empleados_trabajo.Total(EPeriodo.Periodo1T) + empleados_especie.Total(EPeriodo.Periodo1T) + profesionales.Total(EPeriodo.Periodo1T);
			decimal totalModelo2T = empleados_trabajo.Total(EPeriodo.Periodo2T) + empleados_especie.Total(EPeriodo.Periodo2T) + profesionales.Total(EPeriodo.Periodo2T);
			decimal totalModelo3T = empleados_trabajo.Total(EPeriodo.Periodo3T) + empleados_especie.Total(EPeriodo.Periodo3T) + profesionales.Total(EPeriodo.Periodo3T);
			decimal totalModelo4T = empleados_trabajo.Total(EPeriodo.Periodo4T) + empleados_especie.Total(EPeriodo.Periodo4T) + profesionales.Total(EPeriodo.Periodo4T);

			doc.Subreports["EmpleadosTrabajo"].SetDataSource(pList_empleados_trabajo);
			doc.Subreports["EmpleadosEspecie"].SetDataSource(pList_empleados_especie);
			doc.Subreports["Profesionales"].SetDataSource(pList_profesionales);
			doc.Subreports["Facturas"].SetDataSource(pList_facturas);

			doc.SetParameterValue("TotalModelo", totalModelo);
			doc.SetParameterValue("TotalModelo1T", totalModelo1T);
			doc.SetParameterValue("TotalModelo2T", totalModelo2T);
			doc.SetParameterValue("TotalModelo3T", totalModelo3T);
			doc.SetParameterValue("TotalModelo4T", totalModelo4T);

			FormatHeader(doc);

			return doc;
		}

		#endregion

		#region Balance

		public BalanceRpt GetBalanceReport(NotifyEntityList list)
		{
			List<NotifyEntity> pList = new List<NotifyEntity>();

			foreach (NotifyEntity item in list)
				pList.Add(item);

			if (pList.Count == 0) return null;

			BalanceRpt doc = new BalanceRpt();

			doc.SetDataSource(pList);

			FormatHeader(doc);

			return doc;
		}

		#endregion

		#region Historico Precios

		public InformeHistoricoPreciosClientesRpt GetInformeHistoricoPreciosClientesReport(VentasList list)
		{
			InformeHistoricoPreciosClientesRpt doc = new InformeHistoricoPreciosClientesRpt();

			List<VentasInfo> pList = new List<VentasInfo>();

			if (list.Count == 0) return null;

			foreach (VentasInfo item in list)
				pList.Add(item);

			doc.SetDataSource(pList);

			FormatHeader(doc);

			return doc;
		}

		public InformeHistoricoPreciosProductosRpt GetInformeHistoricoPreciosProductosReport(VentasList list)
		{
			InformeHistoricoPreciosProductosRpt doc = new InformeHistoricoPreciosProductosRpt();

			List<VentasInfo> pList = new List<VentasInfo>();

			if (list.Count == 0) return null;

			foreach (VentasInfo item in list)
				pList.Add(item);

			doc.SetDataSource(pList);

			FormatHeader(doc);

			return doc;
		}

		#endregion

        #region Cuentas Contables

        public CuentasContablesListRpt GetListReport(ResumenCuentasContablesList list)
        {
            if (list.Count == 0) return null;

            CuentasContablesListRpt doc = new CuentasContablesListRpt();

            List<ResumenCuentasContablesInfo> pList = new List<ResumenCuentasContablesInfo>();

            //Los ordena por número de cuenta contable antes de mostrarlo
            SortedBindingList<ResumenCuentasContablesInfo> sorted = list.ToSortedList("CuentaContable", ListSortDirection.Ascending);

            foreach (ResumenCuentasContablesInfo item in sorted)
                pList.Add(item);

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        #endregion
    }
}
