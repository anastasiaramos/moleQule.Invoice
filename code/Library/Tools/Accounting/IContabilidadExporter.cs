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
    /// <summary>
	/// Agente Acreedor
	/// </summary>
	public interface IContabilidadExporter
	{
		Registro Registro { get; }

        void ExportInputInvoices();
        void ExportOutputInvoices();
        void ExportPayments();
        void ExportCharges();
		void ExportExpenses();
		void ExportPayrolls();
		void ExportGrants();
        void ExportBankTransfers();
        void ExportLoans();

		void Init(ContabilidadConfig _config);
        void SaveFiles();
	}

	public interface IContabilidadExporterMng
	{
		Registro Registro { get; }

		void Export(EElementoExportacion items);
		void Close();
	}

	public struct ContabilidadConfig
	{
		public ETipoExportacion TipoExportacion;
		public Library.Invoice.QueryConditions Conditions;
		public string RutaSalida;
		public string AsientoInicial;
		public string Diario;
		public string Empresa;
		public string CentroTrabajo;
	}
}

