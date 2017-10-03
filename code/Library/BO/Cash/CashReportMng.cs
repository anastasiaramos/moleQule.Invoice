using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;
using moleQule.Library.Invoice.Reports.Cash;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class CashReportMng : BaseReportMng
    {
        #region Business Methods Caja
		
        public CashRpt GetDetailReport(CashInfo item, 
                                        CashLineList list,
                                        DateTime f_ini,
                                        DateTime f_fin)
        {
            if (item == null) return null;
            if (list.Count == 0) return null;

            CashRpt doc = new CashRpt();
            
            List<CajaPrint> pList = new List<CajaPrint>();

			int index = item.Lines.IndexOf(list[0]);
			item.DebeAcumulado = item.GetDebeAcumulado(index);
			item.HaberAcumulado = item.GetHaberAcumulado(index);

			CajaPrint obj = CajaPrint.New(item);

			List<LineaCajaPrint> pLineaCajas = new List<LineaCajaPrint>();

            obj.HaberTotal = obj.HaberAcumulado;
            obj.DebeTotal = obj.DebeAcumulado;

			foreach (CashLineInfo child in list)
			{
				pLineaCajas.Add(LineaCajaPrint.New(child));
                if (child.EEstado != Common.EEstado.Anulado)
                {
                    obj.HaberTotal += child.Haber;
                    obj.DebeTotal += child.Debe;
                }
			}

			pList.Add(obj);
            doc.SetDataSource(pList);
            doc.Subreports["SubLineasCaja"].SetDataSource(pLineaCajas);

            FormatHeader(doc);

            return doc;
        }

		public CashLineListRpt GetListReport(CashLineList list)
        {
            if (list.Count == 0) return null;

            CashLineListRpt doc = new CashLineListRpt();

            List<LineaCajaPrint> pList = new List<LineaCajaPrint>();
			
            foreach (CashLineInfo item in list)
                pList.Add(LineaCajaPrint.New(item));;
			
            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        #endregion

        #region Business Methods CierreCaja

        public CierreCajaRpt GetDetailReport(CierreCajaInfo item)
        {
            if (item == null) return null;

            CierreCajaRpt doc = new CierreCajaRpt();

            List<CierreCajaPrint> pList = new List<CierreCajaPrint>();

            CierreCajaPrint obj = CierreCajaPrint.New(item);

            List<LineaCajaPrint> pLineaCajas = new List<LineaCajaPrint>();

            foreach (CashLineInfo child in item.LineaCajas)
            {
                pLineaCajas.Add(LineaCajaPrint.New(child));
            }

            pList.Add(obj);
            doc.SetDataSource(pList);
            doc.Subreports["SubLineasCaja"].SetDataSource(pLineaCajas);

            doc.SetParameterValue("Empresa", Schema.Name);

            return doc;
        }

        public CierreCajaListRpt GetListReport(CierreCajaList list)
        {
            if (list.Count == 0) return null;

            CierreCajaListRpt doc = new CierreCajaListRpt();

            List<CierreCajaPrint> pList = new List<CierreCajaPrint>();
			
            foreach (CierreCajaInfo item in list)
            {
                pList.Add(CierreCajaPrint.New(item));;
            }
			
            doc.SetDataSource(pList);

            FormatHeader(doc);

            return doc;
        }

        #endregion

		#region Factory Methods

		public CashReportMng() {}

        public CashReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) {}

        public CashReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public CashReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }

        #endregion
        
        #region Style

        private static void FormatReport(CashRpt rpt, string logo)
        {
            /*string path = Images.GetRootPath() + "\\" + Resources.Paths.LOGO_EMPRESAS + logo;

            if (File.Exists(path))
            {
                Image image = Image.FromFile(path);
                int width = rpt.Section1.ReportObjects["Logo"].Width;
                int height = rpt.Section1.ReportObjects["Logo"].Height;

                rpt.Section1.ReportObjects["Logo"].Width = 15 * image.Width;
                rpt.Section1.ReportObjects["Logo"].Height = 15 * image.Height;
                rpt.Section1.ReportObjects["Logo"].Left += (width - 15 * image.Width) / 2;
                rpt.Section1.ReportObjects["Logo"].Top += (height - 15 * image.Height) / 2;
            }*/
        }

        #endregion
    }
}
