using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class AlbaranTicketPrint : AlbaranTicketInfo
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
        protected long _numero_cliente;
        protected long _dias_pago;
        protected decimal _Albarans_anteriores;

        public string CodigoCliente { get { return _codigo_cliente; } }
        public string Nombre { get { return _nombre; } }
        public string Telefonos { get { return _telefonos; } }
        public string Movil { get { return _movil; } }
        public string NumeroCliente { get { return CodigoCliente; } } /*DEPRECATED*/
        public decimal TotalTicket { get { return _total_factura; } }
        public string NumeroSerie { get { return _numero_serie; } }
        public DateTime FechaTicket { get { return _fecha_factura; } }
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
        protected void CopyValues(AlbaranTicketInfo source, ClienteInfo cliente, TicketInfo ticket, OutputDeliveryInfo Albaran)
        {
            if (source == null) return;

            Oid = source.Oid;
            _base.Record.OidAlbaran = source.OidAlbaran;
            _base.Record.OidTicket = source.OidTicket;

            SerieInfo serie = SerieInfo.Get(ticket.OidSerie, false);

            _base.CodigoTicket = ticket.Codigo;
            _total_factura = ticket.Total;
            _numero_serie = serie.Identificador;
            _fecha_factura = ticket.Fecha;
            _prevision = ticket.Prevision;

            //INNER JOIN
            _codigo_cliente = cliente.Codigo;
            _nombre = cliente.Nombre;
            _telefonos = cliente.Telefonos;
            _movil = cliente.Movil;
            _dias_pago = Albaran.Fecha.Subtract(ticket.Fecha).Days;
        }

        #endregion

        #region Factory Methods

        private AlbaranTicketPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static AlbaranTicketPrint New(AlbaranTicketInfo source, ClienteInfo cliente, TicketInfo factura, OutputDeliveryInfo Albaran)
        {
            AlbaranTicketPrint item = new AlbaranTicketPrint();
            item.CopyValues(source, cliente, factura, Albaran);

            return item;
        }

        #endregion

    }
}
