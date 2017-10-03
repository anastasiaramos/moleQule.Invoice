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
	public interface IExporter
	{
		Registro Registry { get; set; }

		void Close();
		void Export();
		void Init(ExporterConfig _config);
	}

	public interface IExporterMng
	{
		Registro Registry { get; }

		void Close();
		void Export();
	}

	public struct ExporterConfig
	{
		public ETipoEntidad DestinationEntityType;
		public ISchemaInfo DestinationCompany;
		public object DestinationHolder;
		public string DestinationPath;

		public ETipoEntidad SourceEntityType;
		public IList SourceEntityList;
	}
}

