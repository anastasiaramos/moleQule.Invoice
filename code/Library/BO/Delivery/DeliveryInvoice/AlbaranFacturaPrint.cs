using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class AlbaranFacturaPrint : AlbaranFacturaInfo
    {

        #region Business Methods

        protected string _numero_serie = string.Empty;
        protected DateTime _fecha_factura;
        protected decimal _cantidad_cobrada;
        protected decimal _pendiente;
        protected string _id_Albaran_2 = string.Empty;
        protected decimal _total_factura;
        protected DateTime _prevision;

        protected string _codigo_cliente = string.Empty;
        protected string _nombre = string.Empty;
        protected string _telefonos = string.Empty;
        protected string _movil = string.Empty;
        protected long _dias_pago;
        protected decimal _Albarans_anteriores;

        public string CodigoCliente { get { return _codigo_cliente; } }
		public string NumeroCliente { get { return CodigoCliente; } } /*DEPRECATED*/
        public string Nombre { get { return _nombre; } }
        public string Telefonos { get { return _telefonos; } }
        public string Movil { get { return _movil; } }
        public decimal TotalFactura { get { return _total_factura; } }
        public string NumeroSerie { get { return _numero_serie; } }
        public DateTime FechaFactura { get { return _fecha_factura; } }
        public decimal CantidadCobrada { get { return _cantidad_cobrada; } }
        public decimal Pendiente { get { return _pendiente; } }
        public string IdAlbaran2 { get { return _id_Albaran_2; } }
        public decimal AlbaransAnteriores { get { return _Albarans_anteriores; } }
        public long DiasPago { get { return _dias_pago; } }
        public DateTime Prevision { get { return _prevision; } }

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(AlbaranFacturaInfo source, ClienteInfo cliente, OutputInvoiceInfo factura, OutputDeliveryInfo Albaran)
        {
            if (source == null) return;

            Oid = source.Oid;
            _base.Record.OidAlbaran = source.OidAlbaran;
			_base.Record.OidFactura = source.OidFactura;

            SerieInfo serie = SerieInfo.Get(factura.OidSerie, false);

			_base.CodigoFactura = factura.Codigo;
            _total_factura = factura.Total;
            _numero_serie = serie.Identificador;
            _fecha_factura = factura.Fecha;
            _prevision = factura.Prevision;

            //INNER JOIN
            _codigo_cliente = cliente.Codigo;
            _nombre = cliente.Nombre;
            _telefonos = cliente.Telefonos;
            _movil = cliente.Movil;
            _dias_pago = Albaran.Fecha.Subtract(factura.Fecha).Days;
        }

        #endregion

        #region Factory Methods

        private AlbaranFacturaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static AlbaranFacturaPrint New(AlbaranFacturaInfo source, ClienteInfo cliente, OutputInvoiceInfo factura, OutputDeliveryInfo Albaran)
        {
            AlbaranFacturaPrint item = new AlbaranFacturaPrint();
            item.CopyValues(source, cliente, factura, Albaran);

            return item;
        }

        #endregion

    }
}
