using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace moleQule.Library.Invoice.Reports
{
    [Serializable()]
    public abstract class ReportMng : moleQule.Library.Reports.BaseReportMng
	{
		#region Attributes & Properties
		
		#endregion

		#region Factory Methods

		public ReportMng() : this (null) {}

		public ReportMng(ISchemaInfo schema)
            : this(schema, string.Empty) {}

        public ReportMng(ISchemaInfo schema, string title)
            : this(schema, title, string.Empty) {}

		public ReportMng(ISchemaInfo schema, string title, string filter)
			: base(schema, title, filter) {}

		#endregion

		#region Business Methods

		protected override ReportClass GetReportFromName(string folder, string className)
        {
			Assembly assembly = null;
			string pattern = string.Empty;

			try
			{
				assembly = Assembly.Load("moleQule.Library.App");
				pattern = "moleQule.Library.App.Modules.Invoice.Reports.{0}.{1}.{2}";
			}
			catch
			{
				assembly = Assembly.Load("moleQule.Library.Application");
				pattern = "moleQule.Library.Application.Modules.Invoice.Reports.{0}.{1}.{2}";
			}

            List<string> reports = new List<string>();
            
            if (ModulePrincipal.GetInvoiceTemplateSetting() != ((long)EValue.Default).ToString())
                reports.Add(String.Format(pattern, folder, ModulePrincipal.GetInvoiceTemplateSetting(), className));

            reports.Add(String.Format(pattern, folder, "s" + AppContext.ActiveSchema.SchemaCode, className));
            reports.Add(String.Format(pattern.Substring(0, pattern.IndexOf("{2}") - 1), folder, className)); 
            
            ObjectHandle object_handle = null;

            // Trying to load custom reports
            foreach (string report in reports)
            {
                try
                {
                    object_handle = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, report);
                    return (ReportClass)object_handle.Unwrap();
                }
                catch {}
            }

            // Load the module template report
            if (object_handle == null)
            {
                assembly = Assembly.Load("moleQule.Library.Invoice");
                pattern = "moleQule.Library.Invoice.Reports.{0}.{1}";

                object_handle = AppDomain.CurrentDomain.CreateInstance(
                                                        assembly.FullName,
                                                        String.Format(pattern, folder, className));
            }

            return (ReportClass)object_handle.Unwrap();
		}

		protected ReportClass GetReportFromNameDeprecated(string folder, string className)
		{
			Assembly assembly = Assembly.Load("moleQule.Library.Application");

			string basic_pattern = "moleQule.Library.Invoice.Reports.{0}.{1}";
			string custom_pattern = "moleQule.Library.Application.Modules.Invoice.Reports.{0}.{1}.{2}";

			try
			{
				if (ModulePrincipal.GetInvoiceTemplateSetting() != ((long)EValue.Default).ToString())
				{
					ObjectHandle object_handle = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, String.Format(custom_pattern, folder, ModulePrincipal.GetInvoiceTemplateSetting(), className));
					return (ReportClass)object_handle.Unwrap();
				}
				else
				{
					ObjectHandle object_handle = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, String.Format(custom_pattern, folder, "s" + AppContext.ActiveSchema.SchemaCode, className));
					return (ReportClass)object_handle.Unwrap();
				}
			}
			catch
			{
				ObjectHandle object_handle = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, String.Format(basic_pattern, folder, className));
				return (ReportClass)object_handle.Unwrap();
			}
		}
		
		#endregion
    }
}