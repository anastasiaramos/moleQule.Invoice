using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class TicketPrint : TicketInfo
    {              
        #region Attributes & Properties 

		private QRCodePrint _qr_code_print = new QRCodePrint();

		public string EEstadoPrintLabel { get { return Common.EnumText<EEstado>.GetPrintLabel(EEstado); } }
		public string MedioPagoPrintLabel { get { return Common.EnumText<EMedioPago>.GetPrintLabel(EMedioPago); } }
		public byte[] QRCode { get { return (_qr_code_print.QRCode != null) ? _qr_code_print.QRCode : new byte[1]; } }

		#endregion

		#region Business Methods

		protected void CopyValues(TicketInfo factura, bool get_QRCode)
		{
			if (factura == null) return;

			Oid = factura.Oid;
			_base.CopyValues(factura);

			_concepto_tickets = factura.ConceptoTickets;

			if (get_QRCode)
			{
				_qr_code_print.LoadQRCode(_qr_code_print.Encode(QREncodeVersion.v1, ETipoEntidad.Ticket, this), QRCodeVersion.v8);
			}
		}

		#endregion

		#region Factory Methods

		private TicketPrint() { /* require use of factory methods */ }

		// called to load data from source
		public static TicketPrint New(TicketInfo factura)
		{
			TicketPrint item = new TicketPrint();
			item.CopyValues(factura, true);

			return item;
		}

		public static TicketPrint New(TicketInfo factura, bool get_QRCode)
		{
			TicketPrint item = new TicketPrint();
			item.CopyValues(factura, get_QRCode);

			return item;
		}

		#endregion

    }
}
