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
    public class OutputInvoicePrint : OutputInvoiceInfo
    {              
        #region Attributes & Properties 

		private string _poblacion = string.Empty;
		private string _telefonos = string.Empty;
		private string _fax = string.Empty;
        private string _nombre_transportista = string.Empty;
        private string _serie = string.Empty;
        private string _nombre_serie = string.Empty;
		private QRCodePrint _qr_code_print = new QRCodePrint();

        public string Poblacion { get { return _poblacion; } }
        public string Telefonos { get { return _telefonos; } }
        public string Fax { get { return _fax; } }
        public string NombreTransportista { get { return _nombre_transportista; } }
        public new string Serie { get { return NumeroSerie; } }
        public string NombreSerie { get { return _nombre_serie; } }
		public byte[] QRCode { get { return (_qr_code_print.QRCode != null) ? _qr_code_print.QRCode : new byte[1]; } }

		#endregion

		#region Business Methods

        protected void CopyValues(OutputInvoiceInfo factura, ClienteInfo cliente, TransporterInfo transporte, SerieInfo serie, bool get_QRCode)
        {
            if (factura == null) return;

			_base.CopyValues(factura);

            if (cliente != null)
            {
                _base._id_cliente = cliente.Codigo;
                _telefonos = cliente.Telefonos;
                _fax = cliente.Fax;
            }

            if (transporte != null)
            {
                _nombre_transportista = transporte.Nombre;
            }

            if (serie != null)
            {
                _serie = serie.Identificador;
                _nombre_serie = serie.Nombre;
                _base._n_serie = serie.Identificador;
            }

			_conceptos = factura.ConceptoFacturas;

			if (get_QRCode)
			{
				_qr_code_print.LoadQRCode(_qr_code_print.Encode(QREncodeVersion.v1, ETipoEntidad.FacturaEmitida, this), QRCodeVersion.v8);
			}
        }
        
		#endregion

        #region Factory Methods

        protected OutputInvoicePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static OutputInvoicePrint New(OutputInvoiceInfo factura, ClienteInfo cliente, TransporterInfo transporter, SerieInfo serie)
        {
            OutputInvoicePrint item = new OutputInvoicePrint();
            item.CopyValues(factura, cliente, transporter, serie, true);

            return item;
        }

		public static OutputInvoicePrint New(OutputInvoiceInfo factura, ClienteInfo cliente, TransporterInfo transporter, SerieInfo serie, bool get_QRCode)
		{
			OutputInvoicePrint item = new OutputInvoicePrint();
			item.CopyValues(factura, cliente, transporter, serie, get_QRCode);

			return item;
		}

        #endregion
    }

    /* DEPRECATED */
    [Serializable()]
    public class FacturaPrint : OutputInvoicePrint
    {
        #region Factory Methods

        private FacturaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static FacturaPrint New(OutputInvoiceInfo factura, ClienteInfo cliente, TransporterInfo transporter, SerieInfo serie)
        {
            FacturaPrint item = new FacturaPrint();
            item.CopyValues(factura, cliente, transporter, serie, true);

            return item;
        }

        public new static FacturaPrint New(OutputInvoiceInfo factura, ClienteInfo cliente, TransporterInfo transporter, SerieInfo serie, bool get_QRCode)
        {
            FacturaPrint item = new FacturaPrint();
            item.CopyValues(factura, cliente, transporter, serie, get_QRCode);

            return item;
        }

        #endregion
    }
}
