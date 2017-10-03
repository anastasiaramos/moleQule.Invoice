using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    public class ExporterBase: IExporter
    {
        #region Attributes & Properties

		protected ExporterConfig _config;

		protected Registro _registry;

		public Registro Registry { get { return _registry; } set { _registry = null; } }

        #endregion

        #region Factory Methods

		protected ExporterBase() { }

		public string GetConditions()
		{
			string filtro = string.Empty;

			//if (_invoice_conditions.FechaIni != DateTime.MinValue) filtro += "Fecha Inicial: " + _invoice_conditions.FechaIni.ToShortDateString() + "; ";
			//if (_invoice_conditions.FechaFin != DateTime.MinValue) filtro += "Fecha Final: " + _invoice_conditions.FechaFin.ToShortDateString() + "; ";
			//if (_invoice_conditions.Familia != null) filtro += "Familia: " + _invoice_conditions.Familia.Nombre + "; ";
			//if (_invoice_conditions.Producto != null) filtro += "Producto: " + _invoice_conditions.Producto.Nombre + "; ";
			//if (_invoice_conditions.Serie != null) filtro += "Serie: " + _invoice_conditions.Serie.Nombre + "; ";
            //filtro += "Tipo de Acreedor: " + Common.EnumText<ETipoAcreedor>.GetLabel(_invoice_conditions.TipoAcreedor) + "; ";
			//filtro += "Medio de Pago: " + Common.EnumText<EMedioPago>.GetLabel(_invoice_conditions.MedioPagoList) + "; ";
			//filtro += "Estado: " + Common.EnumText<EEstado>.GetLabel(_invoice_conditions.Estado) + "; ";
			//filtro += "Ayudas: " + Invoice.EnumText<ETipoAyudaContabilidad>.GetLabel(_invoice_conditions.TipoAyudas) + ";";

			return filtro;
		}

		public virtual void Init(ExporterConfig config)
        {
			_config = config;

			_registry = Registro.New(ETipoRegistro.CompanyExport);
			_registry.Nombre = Resources.Labels.COMPANY_EXPORT_REGISTRY;
			_registry.Observaciones = GetConditions();

			InitDestinationPath();
        }

		protected virtual void InitDestinationPath()
		{
			if (string.IsNullOrEmpty(_config.DestinationPath)) return;

			if (!_config.DestinationPath.EndsWith("\\")) _config.DestinationPath += "\\";

			if (!Directory.Exists(_config.DestinationPath))
				Directory.CreateDirectory(_config.DestinationPath);
		}
		
		public virtual void Close()
        {
			if (_registry != null)
			{
				_registry.Save();
				_registry.CloseSession();
				_registry = null;
			}
        }

        #endregion

        #region Business Methods

		public virtual void Export() { }

		#endregion
    }

	#region Enums

	#endregion
}
