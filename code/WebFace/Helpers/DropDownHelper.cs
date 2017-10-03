using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;

namespace moleQule.WebFace.Invoice.HtmlHelpers
{
	public static class DropDownHelper
	{
		public static MvcHtmlString TaxesDropDown(this System.Web.Mvc.HtmlHelper helper, string name, string optionLabel, object selectedValue)
		{
			ImpuestoList taxes = ImpuestoList.GetList();

			StringBuilder b = new StringBuilder();
			b.Append(string.Format("<select class=\"input-small\" name=\"{0}\" id=\"{0}\">", name));
			
			if (!string.IsNullOrEmpty(optionLabel))
				b.Append(string.Format("<option value=\"\">{0}</option>", optionLabel));

			string selected = string.Empty;

			foreach (ImpuestoInfo item in taxes)
			{
				selected = (item.Oid == Convert.ToInt64(selectedValue)) ? "selected=\"selected\"" : string.Empty;

				b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", item.Oid, selected, item.Porcentaje));
			}
			b.Append("</select>");

			return MvcHtmlString.Create(b.ToString());
		}

		public static MvcHtmlString PaymentGatewaysDropDown(this System.Web.Mvc.HtmlHelper helper, string name, object selectedValue)
		{
			ComboBoxList<EPaymentGateway> list = Library.Invoice.EnumText<EPaymentGateway>.GetList(true, false);

			StringBuilder b = new StringBuilder();
			b.Append(string.Format("<select name=\"{0}\" id=\"{0}\" class=\"input-small\">", name));

			string selected = string.Empty;

			foreach (ComboBoxSource item in list)
			{
				selected = (item.Oid == Convert.ToInt64(selectedValue)) ? "selected=\"selected\"" : string.Empty;

				b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", item.Oid, selected, item.Texto));
			}
			b.Append("</select>");

			return MvcHtmlString.Create(b.ToString());
		}
	}
}