using System;
using System.Globalization;
using System.Text;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;

using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Library.Invoice
{
	public class QRCodePrint : QRCodePrintBase
	{
		#region Business Methods

		public string Encode(QREncodeVersion version, ETipoEntidad tipo, object source)
		{
			String txtEncodeData = string.Empty;

			txtEncodeData = "<l v=" + ((long)version).ToString() + " t=" + ((long)tipo).ToString() + " oid=OID_ITEM>";

			switch (tipo)
			{ 
				case ETipoEntidad.FacturaEmitida:
					{
						OutputInvoicePrint item = (OutputInvoicePrint)source;

						txtEncodeData = txtEncodeData.Replace("OID_ITEM", item.Oid.ToString());

						txtEncodeData += "<p>"										
										+ item.NumeroSerie + "|"
										+ item.Fecha.ToShortDateString() + "|"
										+ item.VatNumber + "|"
										+ item.Codigo + "|"
										+ item.Cliente + "|"
										+ item.BaseImponible.ToString() + "|"
										+ item.Impuestos.ToString() + "|"
										+ item.Total.ToString() + "|"
										+ "</p>";
					}
					break;

				case ETipoEntidad.Cobro:
					{
						CobroPrint item = (CobroPrint)source;

						txtEncodeData = txtEncodeData.Replace("OID_ITEM", item.Oid.ToString());

						txtEncodeData += "<p>"
										+ item.Codigo + "|"
										+ item.Fecha.ToShortDateString() + "|"
										+ item.IDCobroS + "|"
										+ item.EMedioPagoPrintLabel + "|"
										+ item.NCliente + "|"
										+ item.Cliente + "|"
										+ item.Importe + "|"
										+ item.CuentaBancaria + "|"
										+ "</p>";

						if (item.CobroFacturas != null)
						{
							txtEncodeData += "<sl t=" + ((long)ETipoEntidad.CobroFactura).ToString() + ">";

							foreach (CobroFacturaInfo cf in item.CobroFacturas)
								txtEncodeData += "<p>"
												+ cf.CodigoFactura + "|"
												+ cf.Cantidad + "|"
												+ "</p>";

							txtEncodeData += "</sl>";
						}
					}
					break;
			}

			txtEncodeData += "</l>";
			return txtEncodeData;
		}

		#endregion
	}
}
