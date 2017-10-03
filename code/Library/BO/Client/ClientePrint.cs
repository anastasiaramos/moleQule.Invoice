using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library.Common;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class ClientePrint : ClienteInfo
    {              
        #region Attributes & Properties

        protected decimal _total_facturado;
        protected decimal _total_cobrado;
        protected decimal _efectos_negociados;
        protected decimal _efectos_devueltos;
        protected decimal _efectos_pendientes_vto;
		protected decimal _dudoso_cobro;
		protected decimal _gastos_demora;
        protected decimal _gastos_cobro;
        protected decimal _condiciones_venta;

        public decimal TotalCobrado { get { return _total_cobrado; } }
        public decimal EfectosNegociados { get { return _efectos_negociados; } }
        public decimal EfectosDevueltos { get { return _efectos_devueltos; } }
        public decimal EfectosPendientesVto { get { return _efectos_pendientes_vto; } }
        public decimal Pendiente { get { return _total_facturado - _total_cobrado - _efectos_negociados - _efectos_devueltos - _efectos_pendientes_vto; } }
		public decimal DudosoCobro { get { return _dudoso_cobro; } }
		public decimal GastosDemora { get { return _gastos_demora; } }
        public decimal GastosCobro { get { return _gastos_cobro; } }
        public decimal CondicionesVenta { get { return _condiciones_venta; } }

		#endregion

		#region Business Methods

        protected void CopyValues(ClienteInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);

            _total_facturado = 0.0m;

			OutputInvoiceList facturas = OutputInvoiceList.GetByClienteList(source, false);

            foreach (OutputInvoiceInfo f in facturas)
                _total_facturado += f.Total;

            _total_cobrado = 0;
            _efectos_negociados = 0;
            _efectos_devueltos = 0;
            _efectos_pendientes_vto = 0;
			_gastos_demora = 0;
            _gastos_cobro = 0;
            _condiciones_venta = 0;

            if (source.Cobros != null)
            {
                foreach (ChargeInfo cobro in source.Cobros)
                {
					if (cobro.EEstado == EEstado.Anulado) continue;

                    if (cobro.EEstadoCobro == EEstado.Charged && cobro.Vencimiento < DateTime.Now)
                        _total_cobrado += cobro.Importe;
                    else if (cobro.EEstadoCobro == EEstado.Charged && cobro.Vencimiento >= DateTime.Now)
                        _efectos_negociados += cobro.Importe;
                    else if (cobro.EEstadoCobro != EEstado.Charged && cobro.Vencimiento >= DateTime.Now)
                        _efectos_pendientes_vto += cobro.Importe;
                    else if (cobro.EEstadoCobro != EEstado.Charged && cobro.Vencimiento < DateTime.Now)
                        _efectos_devueltos += cobro.Importe;

					_gastos_demora += cobro.GastosDemora;
                }

                _base._credito_dispuesto = _total_facturado - _total_cobrado;
            }
        }
		protected void CopyValues(ClienteInfo source, ChargeSummary resumen)
		{
			if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);

			_total_facturado = resumen.TotalFacturado;
			_total_cobrado = resumen.Cobrado;
			_efectos_negociados = resumen.EfectosNegociados;
			_efectos_devueltos = resumen.EfectosDevueltos;
			_efectos_pendientes_vto = resumen.EfectosPendientesVto;
			_dudoso_cobro = resumen.DudosoCobro;
            _gastos_demora = resumen.GastosDemora;
            _gastos_cobro = resumen.GastosCobro;
            _condiciones_venta = resumen.CondicionesVenta;

			if (source.Cobros != null)
			{
				foreach (ChargeInfo cobro in source.Cobros)
					_gastos_demora += cobro.GastosDemora;

				_base._credito_dispuesto = _total_facturado - _total_cobrado;
			}
		}

        #endregion

        #region Factory Methods

        private ClientePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static ClientePrint New(ClienteInfo source)
        {
            ClientePrint item = new ClientePrint();
            item.CopyValues(source);

            return item;
        }
		public static ClientePrint New(ClienteInfo source, ChargeSummary resumen)
		{
			ClientePrint item = new ClientePrint();
			item.CopyValues(source, resumen);

			return item;
		}

        #endregion
    }
}
