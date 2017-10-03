using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// ReadOnly Root Business Object with ReadOnly Childs
    /// </summary>
    [Serializable()]
    public class BalanceInfo : ReadOnlyBaseEx<BalanceInfo>
    {
        #region Attributes

        protected DateTime _fecha;
        protected Decimal _pendiente_rea;
        protected Decimal _pendiente_clientes;
        protected Decimal _efectos_pte_vto_clientes;
        protected Decimal _efectos_negociados_clientes;
        protected Decimal _efectos_devueltos_clientes;
        protected SortedBindingList<PaymentSummary> _pendientes_pago;
        protected Decimal _stock_alimentacion_valorado;
        protected Decimal _stock_ganado_valorado;
        protected Decimal _stock_maquinaria_valorado;
        protected Decimal _saldo_caja;

        #endregion

        #region Propiedades

        public DateTime Fecha { get { return _fecha; } }
        public Decimal PendienteRea { get { return _pendiente_rea; } }
        public Decimal PendienteClientes { get { return _pendiente_clientes; } }
        public Decimal EfectosPendienteVtoClientes { get { return _efectos_pte_vto_clientes; } }
        public Decimal EfectosNegociadosClientes { get { return _efectos_negociados_clientes; } }
        public Decimal EfectosDevueltosClientes { get { return _efectos_devueltos_clientes; } }
		public SortedBindingList<PaymentSummary> PendientesPago { get { return _pendientes_pago; } }
        public Decimal StockAlimentacionValorado { get { return _stock_alimentacion_valorado; } }
        public Decimal StockGanadoValorado { get { return _stock_ganado_valorado; } }
        public Decimal StockMaquinariaValorado { get { return _stock_maquinaria_valorado; } }
        public Decimal SaldoCaja { get { return _saldo_caja; } }

        #endregion

        #region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;
        }

        #endregion
        
        #region Factory Methods

        protected BalanceInfo() 
        {
            _fecha = DateTime.Today;
        }

        public static BalanceInfo Get()
        {
            BalanceInfo obj = new BalanceInfo();

			obj._pendientes_pago = PaymentSummaryList.SortList(PaymentSummaryList.GetPendientesList(), "Nombre", ListSortDirection.Ascending);

			Library.Store.QueryConditions conditions = new Library.Store.QueryConditions
			{
				TipoExpediente = ETipoExpediente.Todos,
				Estado = EEstado.Pendiente
			};

            ExpedienteREAList expedientes = ExpedienteREAList.GetListByREA(conditions, false);
            foreach (ExpedienteREAInfo item in expedientes)
            {                
                obj._pendiente_rea +=  item.AyudaPendiente;
            }

            OutputInvoiceList facturas = OutputInvoiceList.GetNoCobradasList(false);

            obj._pendiente_clientes = facturas.TotalPendiente();
            obj._efectos_pte_vto_clientes = facturas.TotalPendienteVencimiento();
            obj._efectos_negociados_clientes = facturas.TotalNegociado();
            obj._efectos_devueltos_clientes = facturas.TotalDevuelto();

            InventarioValoradoList inventario = InventarioValoradoList.GetListStock(ETipoExpediente.Todos, null, DateTime.Today);

            foreach (InventarioValoradoInfo item in inventario)
            {
                switch (item.TipoExpediente)
                { 
                    case (long)(ETipoExpediente.Alimentacion):
                        obj._stock_alimentacion_valorado += item.PVP;
                        break;

                    case (long)(ETipoExpediente.Ganado):
                        obj._stock_ganado_valorado += item.PVP;
                        break;

                    case (long)(ETipoExpediente.Maquinaria):
                        obj._stock_maquinaria_valorado += item.PVP;
                        break;
                }                
            }

            obj._saldo_caja = CashInfo.Get(1, true).SaldoTotal;

            return obj;
        }

        #endregion
    }
}



