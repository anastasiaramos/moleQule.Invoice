using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Library.Invoice
{

	public static class ContabilidadExporterMng
	{
		public static IContabilidadExporterMng GetExporter(ContabilidadConfig config)
        {
            switch (config.TipoExportacion)
			{
				case ETipoExportacion.ContaWin:
					return new ContabilidadExporter<ContaWinExporter>(config);

				case ETipoExportacion.Tinfor:
					return new ContabilidadExporter<TinforExporter>(config);
                case ETipoExportacion.A3:
                    return new ContabilidadExporter<A3Exporter>(config);
			}

			return null;
		}
	}

    public class ContabilidadExporter<T> : IContabilidadExporterMng
        where T : IContabilidadExporter, new()
    {

        T _exporter;

		public Registro Registro { get { return _exporter.Registro; } }

        public ContabilidadExporter(ContabilidadConfig config)
        {
            _exporter = new T();
			_exporter.Init(config);
        }

        public void Export(EElementoExportacion item)
        {
            switch (item)
            {
                case EElementoExportacion.FacturaRecibida:
                    _exporter.ExportInputInvoices();
                    break;

                case EElementoExportacion.FacturaEmitida:
                    _exporter.ExportOutputInvoices();
                    break;

                case EElementoExportacion.Pago:
                    _exporter.ExportPayments();
                    break;

                case EElementoExportacion.Cobro:
                    _exporter.ExportCharges();
                    break;

				case EElementoExportacion.Gasto:
					_exporter.ExportExpenses();
					break;

				case EElementoExportacion.Nomina:
					_exporter.ExportPayrolls();
					break;

				case EElementoExportacion.Ayuda:
					_exporter.ExportGrants();
					break;

                case EElementoExportacion.Traspaso:
                    _exporter.ExportBankTransfers();
                    break;
                case EElementoExportacion.Prestamo:
                    _exporter.ExportLoans();
                    break;

            }
        }

		public void Close() { _exporter.SaveFiles(); }
    }
}

