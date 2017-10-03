using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library.Common;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class CarteraClientesPrint : ClienteInfo
    {
        #region Business Method

		protected long _oid_factura;
        protected string _numero_factura = string.Empty;
        protected string _numero_serie = string.Empty;
        protected DateTime _fecha_factura = DateTime.MinValue;
        protected DateTime _fecha_vto = DateTime.MinValue;
        protected DateTime _prevision = DateTime.MinValue;
        protected string _tipo = string.Empty;
        protected decimal _total_factura = 0;
        protected decimal _cantidad_cobrada = 0;
        protected decimal _pendiente = 0;
        protected decimal _efectos_negociados;
        protected decimal _efectos_devueltos;
        protected decimal _efectos_pendientes_vto;
		protected decimal _gastos_cobro = 0;

		public long OidFactura { get { return _oid_factura; } }
        public string NumeroFactura { get { return _numero_factura; } }
        public string NumeroSerie { get { return _numero_serie; } }
        public string FechaFactura { get { return (_fecha_factura != DateTime.MinValue) ? _fecha_factura.ToShortDateString() : "---"; } }
        public string FechaPrevision { get { return (_prevision != DateTime.MinValue) ? _prevision.ToShortDateString() : "---"; } }
        public string FechaVto { get { return (_fecha_vto != DateTime.MinValue) ? _fecha_vto.ToShortDateString() : "---"; } }
        public string Tipo { get { return _tipo; } }
        public decimal TotalFactura { get { return _total_factura; } }
        public decimal CantidadCobrada { get { return _cantidad_cobrada; } }
        public decimal Pendiente { get { return _pendiente; } }
		public decimal GastosCobro { get { return _gastos_cobro; } }
        public virtual decimal EfectosNegociados { get { return _efectos_negociados; } }
        public virtual decimal EfectosDevueltos { get { return _efectos_devueltos; } set { _efectos_devueltos = value; } }
        public virtual decimal EfectosPendientesVto { get { return _efectos_pendientes_vto; } set { _efectos_pendientes_vto = value; } }
		public string EMedioPagoPrintLabel { get { return Common.EnumText<EMedioPago>.GetPrintLabel(EMedioPago); } }
        
        protected void CopyValues(ClienteInfo source, OutputInvoiceInfo factura)
        {
            if (source == null) return;

            if (factura != null)
            {
				_oid_factura = factura.Oid;
                _numero_factura = factura.Codigo;
                _numero_serie = factura.NumeroSerie;
                _fecha_factura = factura.Fecha;
                _total_factura = factura.Total;
                _prevision = factura.Prevision;
				_cantidad_cobrada = factura.Cobrado;
				_pendiente = factura.Pendiente;
				_efectos_negociados = factura.EfectosNegociados;
				_efectos_devueltos = factura.EfectosDevueltos;
				_efectos_pendientes_vto = factura.EfectosPendientesVto;
				_gastos_cobro = factura.GastosCobro;
				_base.Record.TipoInteres = factura.TipoInteres;
            }
            else
            {
                _numero_factura = "";
                _numero_serie = "";
                _total_factura = 0;
				_base.Record.TipoInteres = 0;
            }

            Oid = source.Oid;
			_base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        private CarteraClientesPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static CarteraClientesPrint New(ClienteInfo source, OutputInvoiceInfo factura)
        {
            CarteraClientesPrint item = new CarteraClientesPrint();
            item.CopyValues(source, factura);

            return item;
        }

        #endregion
    }
}
