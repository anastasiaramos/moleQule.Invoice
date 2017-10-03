using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class CobroFacturaPrint : CobroFacturaInfo
    {

        #region Attributes & Properties

        protected string _numero_serie = string.Empty;
        protected decimal _cantidad_cobrada;
        protected decimal _pendiente;
        protected string _id_cobro_label = string.Empty;
        protected decimal _total_factura;
        protected DateTime _prevision;

        protected string _codigo_cliente = string.Empty;
        
        public string IDCobroLabel { get { return _id_cobro_label; } }
        public string CodigoCliente { get { return _codigo_cliente; } }
        public decimal TotalFactura { get { return _total_factura; } }
        public string NumeroSerie { get { return _numero_serie; } }
        public decimal CantidadCobrada { get { return _cantidad_cobrada; } }
        public decimal Pendiente { get { return _pendiente; } }
        public DateTime Prevision { get { return _prevision; } }
		public string EMedioPagoLabelPrint { get { return Common.EnumText<EMedioPago>.GetPrintLabel((EMedioPago)_base.MedioPago); } }

		#endregion

		#region Business Methods

        protected void CopyValues(CobroFacturaInfo source, ClienteInfo cliente, OutputInvoiceInfo factura)
        {
            if (source == null) return;

            CopyValues(source);

            if (factura != null)
            {
                SerieInfo serie = SerieInfo.Get(factura.OidSerie, false);

                _base.CodigoFactura = serie.Identificador + "/" + factura.Codigo;
                _total_factura = factura.Total;
                _numero_serie = serie.Identificador;
                _base.FechaFactura = factura.Fecha;
                _prevision = factura.Prevision;
                _base.Cliente = factura.Cliente;

                if (factura.CobroFacturas != null)
                {
                    _cantidad_cobrada = 0;
                    _pendiente = _total_factura - source.Cantidad;

                    foreach (CobroFacturaInfo item in factura.CobroFacturas)
                    {
                        if (item.Fecha < source.Fecha)
                        {
                            _cantidad_cobrada += item.Cantidad;
                            _pendiente -= item.Cantidad;

                        }
                    }
                }
                else
                {
                    _cantidad_cobrada = factura.Cobrado;
                    _pendiente = factura.Pendiente;
                }

                _codigo_cliente = factura.IDCliente;
				_id_cobro_label = _codigo_cliente + "/" + _base.IdCobro.ToString(Resources.Defaults.COBRO_ID_FORMAT);
            }

            if (cliente != null)
            {
                _codigo_cliente = cliente.Codigo;
                _base.Cliente = cliente.Nombre;
				_id_cobro_label = _codigo_cliente + "/" + _base.IdCobro.ToString(Resources.Defaults.COBRO_ID_FORMAT);
            }
        }
        protected void CopyValues(CobroFacturaInfo source)
        {
            if (source == null) return;

			_base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        private CobroFacturaPrint() { /* require use of factory methods */ }

        public static CobroFacturaPrint New(CobroFacturaInfo source, ClienteInfo cliente, OutputInvoiceInfo factura)
        {
            CobroFacturaPrint item = new CobroFacturaPrint();
            item.CopyValues(source, cliente, factura);

            return item;
        }

        public static CobroFacturaPrint New(CobroFacturaInfo source)
        {
            CobroFacturaPrint item = new CobroFacturaPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
