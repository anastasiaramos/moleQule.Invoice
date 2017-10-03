using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class CashLineInfo : ReadOnlyBaseEx<CashLineInfo>, IBankLineInfo
	{
        #region IBankLineInfo

		public long TipoMovimiento { get { return (Debe != 0) ? (long)EBankLineType.EntradaCaja : (long)EBankLineType.SalidaCaja; } }
        public EBankLineType ETipoMovimientoBanco { get { return (EBankLineType)TipoMovimiento; } }
		public ETipoTitular ETipoTitular { get { return ETipoTitular.Todos; } }
		public string Titular { get { return Concepto; } set {} }
		public decimal Importe { get { return (Debe != 0) ? Debe : Haber; } set { } }
		public EMedioPago EMedioPago
		{
			get
			{
				return
						(Debe != 0)
							? EMedioPago.Cheque
							: (OidCuentaBancaria != 0) ? EMedioPago.Ingreso : EMedioPago.Efectivo;
			}
		}
		public DateTime Vencimiento { get { return Fecha; } set { } }
		public bool Confirmado { get { return true; } }
        public decimal GastosBancarios { get { return 0; } }
        public long OidCuenta { get { return 0; } }

        #endregion

		#region Attributes

		protected CashLineBase _base = new CashLineBase();

		#endregion
		
		#region Properties

		public CashLineBase Base { get { return _base; } }

        public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidCaja { get { return _base.Record.OidCaja; } }
        public long OidCierre { get { return _base.Record.OidCierre; } }
        public long OidLink { get { return _base.Record.OidLink; } }
        public long Serial { get { return _base.Record.Serial; } }
        public string Codigo { get { return _base.Record.Codigo; } }
        public string NFactura { get { return _base.Record.NFactura; } }
        public DateTime Fecha { get { return _base.Record.Fecha; } }
        public string Concepto { get { return _base.Record.Concepto; } }
        public long OidCobro { get { return _base.Record.OidCobro; } }
        public long OidPago { get { return _base.Record.OidPago; } }
        public string NTercero { get { return _base.Record.NTercero; } }
        public Decimal Debe { get { return _base.Record.Debe; } }
        public Decimal Haber { get { return _base.Record.Haber; } }
        public string Observaciones { get { return _base.Record.Observaciones; } }
        public long OidCuentaBancaria { get { return _base.Record.OidCuentaBancaria; } }
        public long OidCreditCard { get { return _base.Record.OidCreditCard; } }
        public long Estado { get { return _base.Record.Estado; } }
        public long TipoLinea { get { return _base.Record.Tipo; } }

        //No enlazados
        public virtual string IDCliente { get { return _base.Record.NTercero; } }
		public virtual ETipoLineaCaja ETipoLinea { get { return _base.ETipoLinea; } }
		public virtual string TipoLineaLabel { get { return _base.TipoLineaLabel; } }
		public virtual EEstado EEstado { get { return _base.EStatus; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual decimal Saldo { get { return _base.Saldo; } set { _base.Saldo = value; } }
		public virtual string NCobro { get { return _base.NCobro; } set { _base.NCobro = value; } }
		public virtual string NPago { get { return _base.NPago; } set { _base.NPago = value; } }
		public virtual string CuentaBancaria { get { return _base.CuentaBancaria; } set { _base.CuentaBancaria = value; } }
        public virtual string CreditCard { get { return _base.CreditCard; } set { _base.CreditCard = value; } }
		public virtual string IDMovimientoBanco { get { return _base.IDMovimientoBanco; } set { _base.IDMovimientoBanco = value; } }
		public virtual bool Locked { get { return _base.Locked; } }
		public string IDCierre { get { return _base.IDCierre; } }
		public string Caja { get { return _base.Caja; } }
		
		#endregion
		
		#region Business Methods
		
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected CashLineInfo() { /* require use of factory methods */ }
		private CashLineInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}		
		internal CashLineInfo(CashLine item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}
	
		public static CashLineInfo GetChild(IDataReader reader)
        {
			return GetChild(reader, false);
		}
		public static CashLineInfo GetChild(IDataReader reader, bool childs)
        {
			return new CashLineInfo(reader, childs);
		}
		
 		#endregion

        #region Root Factory Methods

		public static CashLineInfo GetByCobro(long oid_cobro) { return GetByCobro(oid_cobro, 1); }
        public static CashLineInfo GetByCobro(long oid_cobro, long oid_caja)
        {
            CriteriaEx criteria = CashLine.GetCriteria(CashLine.OpenSession());
            criteria.Childs = false;

			QueryConditions conditions = new QueryConditions
			{
				Cobro = Charge.New(ETipoCobro.Todos).GetInfo(false),
				Caja = Invoice.Cash.New().GetInfo(false)
			};
			conditions.Caja.Oid = oid_caja;
            conditions.Cobro.Oid = oid_cobro;
            
            criteria.Query = CashLineInfo.SELECT(conditions);

            CashLineInfo obj = DataPortal.Fetch<CashLineInfo>(criteria);
            CashLine.CloseSession(criteria.SessionCode);
            return obj;
        }
        public static CashLineInfo GetByPago(long oid_pago) { return GetByPago(oid_pago, 1); }
        public static CashLineInfo GetByPago(long oid_pago, long oid_caja)
        {
            CriteriaEx criteria = CashLine.GetCriteria(CashLine.OpenSession());
            criteria.Childs = false;

            QueryConditions conditions = new QueryConditions
            {
                Pago = Payment.New(ETipoPago.Todos).GetInfo(false),
                Caja = Invoice.Cash.New().GetInfo(false)
            };
            conditions.Caja.Oid = oid_caja;
            conditions.Pago.Oid = oid_pago;

            criteria.Query = CashLineInfo.SELECT(conditions);

            CashLineInfo obj = DataPortal.Fetch<CashLineInfo>(criteria);
            CashLine.CloseSession(criteria.SessionCode);
            return obj;
        }
		public static CashLineInfo GetAcumulado(DateTime fecha, long oid_caja)
		{
			CriteriaEx criteria = CashLine.GetCriteria(CashLine.OpenSession());
			criteria.Childs = false;

			QueryConditions conditions = new QueryConditions
			{
				Caja = Invoice.Cash.New().GetInfo(),
				FechaIni = fecha
			};
			conditions.Caja.Oid = oid_caja;

			criteria.Query = CashLine.SELECT_ACUMULADO(conditions);

			CashLineInfo obj = DataPortal.Fetch<CashLineInfo>(criteria);
			CashLine.CloseSession(criteria.SessionCode);
			return obj;
		}

        public static CashLineInfo Get(long oid)
        {
            CriteriaEx criteria = CashLine.GetCriteria(CashLine.OpenSession());
            criteria.Childs = false;
            
            criteria.Query = CashLine.SELECT(oid);

            CashLineInfo obj = DataPortal.Fetch<CashLineInfo>(criteria);
            CashLine.CloseSession(criteria.SessionCode);
            return obj;
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

		#region Child Data Access
		
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);				
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

        #region SQL

        public static string SELECT(QueryConditions conditions) { return CashLine.SELECT(conditions, false); }

        #endregion
    }

    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class SerialLineaCajaInfo : SerialInfo
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Business Methods

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
        protected SerialLineaCajaInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods
        
        public static SerialLineaCajaInfo Get(long oid_caja, int year)
        {
            CriteriaEx criteria = CashLine.GetCriteria(CashLine.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(oid_caja, year);

            SerialLineaCajaInfo obj = DataPortal.Fetch<SerialLineaCajaInfo>(criteria);
            CashLine.CloseSession(criteria.SessionCode);
            return obj;
        }

        public static SerialLineaCajaInfo GetByCierre(long oid_caja, int year)
        {
            CriteriaEx criteria = CashLine.GetCriteria(CashLine.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT_BY_CIERRE(oid_caja, year);

            SerialLineaCajaInfo obj = DataPortal.Fetch<SerialLineaCajaInfo>(criteria);
            CashLine.CloseSession(criteria.SessionCode);
            return obj;
        }

        public static long GetNext(long oid_caja, int year)
        {
            return Get(oid_caja, year).Value + 1;
        }

        public static long GetNextByCierre(long oid_caja, int year)
        {
            return GetByCierre(oid_caja, year).Value + 1;
        }

        #endregion

        #region Root Data Access

        #endregion

        #region SQL

        public static string SELECT(long oid_caja, int year)
        {
            string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));
            string query;

			QueryConditions conditions = new QueryConditions 
			{ 
				Caja = Cash.New().GetInfo(false),
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};
			conditions.Caja.Oid = oid_caja;

            query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
                    " FROM " + lc + " AS LC" +
                    " WHERE LC.\"OID_CAJA\" = " + conditions.Caja.Oid +
					" AND \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

            return query;
        }

        public static string SELECT_BY_CIERRE(long oid_caja, int year)
        {
            string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));
            string query;

			QueryConditions conditions = new QueryConditions
			{
				Caja = Cash.New().GetInfo(false),
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};
			conditions.Caja.Oid = oid_caja;

            query = "SELECT 0 AS \"OID\",  MAX(\"SERIAL\") AS \"SERIAL\"" +
                    " FROM " + lc + " AS LC" +
					" WHERE LC.\"OID_CAJA\" = " + conditions.Caja.Oid.ToString() +
					" AND \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    " AND LC.\"OID_CIERRE\" != 0";

            return query;
        }

        #endregion
    }
}
