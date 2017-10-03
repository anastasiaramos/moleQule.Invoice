using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Reports;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Reports.Cliente;

namespace moleQule.Library.Invoice
{
    public class ClienteReportMng : BaseReportMng
    {
		#region Factory Methods

		public ClienteReportMng()
		{ }

		public ClienteReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty) { }

		public ClienteReportMng(ISchemaInfo empresa, string title)
			: this(empresa, title, string.Empty) { }

		public ClienteReportMng(ISchemaInfo empresa, string title, string filter)
			: base(empresa, title, filter) { }

		#endregion

        #region Business Methods Cliente

        public ClienteListRpt GetListReport(ClienteList list)
        {
            if (list.Count == 0) return null;

            ClienteListRpt doc = new ClienteListRpt();

            List<ClientePrint> pList = new List<ClientePrint>();

            foreach (ClienteInfo item in list)
            {
                pList.Add(ClientePrint.New(item));
            }

            doc.SetDataSource(pList);

			FormatHeader(doc);

            return doc;
        }

        public HistoriaClienteRpt GetHistoriaClienteRpt(ClienteInfo item)
        {
            if (item == null) return null;

            HistoriaClienteRpt doc = new HistoriaClienteRpt();

            List<ClientePrint> pList = new List<ClientePrint>();

            pList.Add(ClientePrint.New(item));

            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;

        }

        #endregion

        #region Cartera de Clientes

		private void GetFacturas(ClienteInfo cliente, 
								QueryConditions conditions,
								FormatConfCarteraClientesReport conf, 
								List<CarteraClientesPrint> pList,
								List<CobroFacturaPrint> pCobroList)
		{

			OutputInvoiceList facturas;

			conditions.Cliente = cliente;

			switch (conf.tipo)
			{
				case ETipoFacturas.Cobradas:
					{
						facturas = OutputInvoiceList.GetCobradasList(conditions, false);

						if (facturas.Count > 0)
						{
							foreach (OutputInvoiceInfo f in facturas)
							{
								f.LoadChilds(typeof(CobroFacturaInfo), false);

								pList.Add(cliente.GetCarteraPrintObject(f));

								foreach (CobroFacturaInfo cobro in f.CobroFacturas)
									pCobroList.Add(CobroFacturaPrint.New(cobro));
							}
						}
					} break;

				case ETipoFacturas.Pendientes:
					{
						facturas = OutputInvoiceList.GetNoCobradasList(conditions, false);

						if (facturas.Count > 0)
						{
							foreach (OutputInvoiceInfo f in facturas)
							{
								f.LoadChilds(typeof(CobroFacturaInfo), false);

								pList.Add(cliente.GetCarteraPrintObject(f));

								foreach (CobroFacturaInfo cobro in f.CobroFacturas)
									pCobroList.Add(CobroFacturaPrint.New(cobro));
							}
						}
					}
					break;

				case ETipoFacturas.Todas:
					{
						facturas = OutputInvoiceList.GetList(conditions, false);

						if (facturas.Count == 0)
						{
							pList.Add(cliente.GetCarteraPrintObject(null));
						}
						else
						{
							foreach (OutputInvoiceInfo f in facturas)
							{
								f.LoadChilds(typeof(CobroFacturaInfo), false);

								if (f.CobroFacturas.Count > 0)
								{
									pList.Add(cliente.GetCarteraPrintObject(f));
									
									foreach (CobroFacturaInfo cobro in f.CobroFacturas)
										pCobroList.Add(CobroFacturaPrint.New(cobro));
								}
								else
									pList.Add(cliente.GetCarteraPrintObject(f));
							}
						}
					}
					break;
			}
		}

		private void FormatReport(InformeCarteraClientesRpt rpt, FormatConfCarteraClientesReport conf)
		{
			switch (conf.campo_ordenacion)
			{
				case "Nombre":
					{
						rpt.GHNumeroCliente.SectionFormat.EnableSuppress = true;
						rpt.GFNumero.SectionFormat.EnableSuppress = true;
						rpt.GHNombre.SectionFormat.EnableSuppress = false;
						rpt.GFNombre.SectionFormat.EnableSuppress = false;
						FieldDefinition fdes = rpt.Database.Tables[0].Fields["Nombre"];
						rpt.DataDefinition.SortFields[0].Field = fdes;
						rpt.DataDefinition.SortFields[0].SortDirection =
							conf.orden_ascendente ? CrystalDecisions.Shared.SortDirection.AscendingOrder : CrystalDecisions.Shared.SortDirection.DescendingOrder;
					}
					break;

				case "Numero de Cliente":
					{
						rpt.GHNumeroCliente.SectionFormat.EnableSuppress = false;
						rpt.GFNumero.SectionFormat.EnableSuppress = false;
						rpt.GHNombre.SectionFormat.EnableSuppress = true;
						rpt.GFNombre.SectionFormat.EnableSuppress = true;
						FieldDefinition fdes = rpt.Database.Tables[0].Fields["Codigo"];
						rpt.DataDefinition.SortFields[0].Field = fdes;
						rpt.DataDefinition.SortFields[0].SortDirection = conf.orden_ascendente ?
																	CrystalDecisions.Shared.SortDirection.AscendingOrder :
																	CrystalDecisions.Shared.SortDirection.DescendingOrder;
					}
					break;
			}

			rpt.Detalles.SectionFormat.EnableSuppress = conf.resumido;
		}

        public InformeCarteraClientesRpt GetCarteraClientesReport(ClienteList lista, SerieInfo serie, FormatConfCarteraClientesReport conf)
        {
            if (lista == null || lista.Count == 0) return null;

            InformeCarteraClientesRpt doc = new InformeCarteraClientesRpt();
            List<CarteraClientesPrint> pList = new List<CarteraClientesPrint>();
			List<CobroFacturaPrint> pCobroList = new List<CobroFacturaPrint>();

			QueryConditions conditions = new QueryConditions
			{
				Serie = serie,
				FechaIni = conf.inicio,
				FechaFin = conf.final
			};

            foreach (ClienteInfo item in lista)
            {
                conditions.Cliente = item;

				GetFacturas(item, conditions, conf, pList, pCobroList);
            }

            doc.SetDataSource(pList);
			if (pCobroList.Count != 0)
				doc.Subreports["CobroFacturasRpt"].SetDataSource(pCobroList);
			else
				conf.verCobros = false;

			doc.SetParameterValue("VerCobros", conf.verCobros);

			FormatReport(doc, conf);
			FormatHeader(doc);			

            return doc;
        }

        public InformeCarteraClientesRpt GetCarteraClientesReport(ClienteInfo cliente, SerieInfo serie, FormatConfCarteraClientesReport conf)
        {

            InformeCarteraClientesRpt doc = new InformeCarteraClientesRpt();
            List<CarteraClientesPrint> pList = new List<CarteraClientesPrint>();
			List<CobroFacturaPrint> pCobroList = new List<CobroFacturaPrint>();

			QueryConditions conditions = new QueryConditions
			{
				Cliente = cliente,
				Serie = serie,
				FechaIni = conf.inicio,
				FechaFin = conf.final
			};

			GetFacturas(cliente, conditions, conf, pList, pCobroList);

            doc.SetDataSource(pList);
			if (pCobroList.Count != 0)
				doc.Subreports["CobroFacturasRpt"].SetDataSource(pCobroList);
			else
				conf.verCobros = false;

			doc.SetParameterValue("VerCobros", conf.verCobros);

			FormatReport(doc, conf);
			FormatHeader(doc);

            return doc;
        }

        #endregion
    }
}
