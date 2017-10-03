using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
	public static class ExporterMng
	{
		public static IExporterMng GetExporter(ExporterConfig config)
        {
            switch (config.SourceEntityType)
			{
				case ETipoEntidad.OutputDelivery:
					return new Exporter<OutputDeliveryExporter>(config);

				/*case ETipoEntidad.FacturaEmitida:
					return new Exporter<OutputInvoiceExporter>(config);*/
			}

			return null;
		}
	}

    public class Exporter<T> : IExporterMng
        where T : IExporter, new()
    {
        T _exporter;

		public Registro Registry { get { return _exporter.Registry; } }

        public Exporter(ExporterConfig config)
        {
            _exporter = new T();
			_exporter.Init(config);
        }

		public void Export() 
		{
			try
			{
				if (_exporter != null)
					_exporter.Export();
			}
			catch (Exception ex)
			{
				_exporter.Registry.CloseSession();
				_exporter.Registry = null;

				throw ex;
			}
			finally 
			{
				Close();
			}
		}

		public void Close() { if (_exporter != null) _exporter.Close(); }
    }
}

