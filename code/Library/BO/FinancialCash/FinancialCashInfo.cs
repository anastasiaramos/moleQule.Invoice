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

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class FinancialCashInfo : ReadOnlyBaseEx<FinancialCashInfo>, IBankLineInfo
    {
        #region IBankLineInfo

        public DateTime Fecha { get { return Vencimiento; } }
        public long TipoMovimiento { get { return (long)EBankLineType.CobroEfecto; } }
        public EBankLineType ETipoMovimientoBanco { get { return EBankLineType.CobroEfecto; } }
        public ETipoTitular ETipoTitular { get { return EnumConvert.ToETipoTitular(Invoice.ETipoCobro.Cliente); } }
        public string Titular { get { return Cliente; } set { } }
        public decimal GastosBancarios { get { return 0; } set { } }
        public bool Confirmado { get { return EEstadoCobro == EEstado.Charged; } }
        public long OidCuenta { get { return 0; } }
        public EMedioPago EMedioPago { get { return ETipoCobro; } }

        #endregion

		#region Attributes

		private FinancialCashBase _base = new FinancialCashBase();

		
		#endregion
		
		#region Properties

        public FinancialCashBase Base { get { return _base; } }

        public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long Serial { get { return _base.Record.Serial; } }
        public string Codigo { get { return _base.Record.Codigo; } }
		public long OidCobro { get { return _base.Record.OidCobro; } }
        public long OidCuentaBancaria { get { return _base.Record.OidCuentaBancaria; } }
		public DateTime FechaEmision { get { return _base.Record.FechaEmision; } }
		public DateTime Vencimiento { get { return _base.Record.Vencimiento; } }
		public bool Adelantado { get { return _base.Record.Negociado; } }
		public Decimal GastosAdelanto { get { return _base.Record.GastosNegociado; } }
		public Decimal GastosDevolucion { get { return _base.Record.GastosDevolucion; } }
		public Decimal Gastos { get { return _base.Record.Gastos; } }
		public long Estado { get { return _base.Record.Estado; } }
        public long EstadoCobro { get { return _base.Record.EstadoCobro; } }
        public string Observaciones { get { return _base.Record.Observaciones; } }
		public DateTime ChargeDate { get { return _base.Record.ChargeDate; } }

        public string IdCobro { get { return _base.IdCobro; } }
        public Decimal Importe { get { return Decimal.Round(_base.Importe, 2); } set { _base.Importe = value; } }
        public string Cliente { get { return _base.Cliente; } }
        public string IdCliente { get { return _base.IdCliente; } }
        public long TipoCobro { get { return _base.TipoCobro; } }
        public string CuentaBancaria { get { return _base.CuentaBancaria; } }
        public string Entidad { get { return _base.Entidad; } }
        public string IdMovBanco { get { return _base.IdMovBanco; } }

        public EEstado EEstado { get { return (EEstado)Estado; } }
        public string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
        public EEstado EEstadoCobro { get { return (EEstado)EstadoCobro; } }
        public string EstadoCobroLabel { get { return Common.EnumText<EEstado>.GetLabel(EEstadoCobro); } }
        public EMedioPago ETipoCobro { get { return (EMedioPago)TipoCobro; } }
        public string TipoCobroLabel { get { return Library.Common.EnumText<EMedioPago>.GetLabel(ETipoCobro); } }
				
		#endregion
		
		#region Business Methods
						
		public void CopyFrom(FinancialCash source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected FinancialCashInfo() { /* require use of factory methods */ }
		private FinancialCashInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
        private FinancialCashInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
		internal FinancialCashInfo(FinancialCash item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}
		
		public static FinancialCashInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static FinancialCashInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new FinancialCashInfo(sessionCode, reader, childs);
        }

        public static FinancialCashInfo GetChild(IDataReader reader) { return GetChild(reader, false); }
        public static FinancialCashInfo GetChild(IDataReader reader, bool childs) { return new FinancialCashInfo(reader, childs); }

        public static FinancialCashInfo New(long oid = 0) { return new FinancialCashInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        public static FinancialCashInfo Get(long oid) { return Get(oid, false); }
		public static FinancialCashInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = FinancialCash.GetCriteria(FinancialCash.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = FinancialCashInfo.SELECT(oid);
	
			FinancialCashInfo obj = DataPortal.Fetch<FinancialCashInfo>(criteria);
			FinancialCash.CloseSession(criteria.SessionCode);
			return obj;
		}
        public static FinancialCashInfo GetByCobro(ChargeInfo cobro, bool childs = false)
        {
            CriteriaEx criteria = FinancialCash.GetCriteria(FinancialCash.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = FinancialCashInfo.SELECT(cobro);

            FinancialCashInfo obj = DataPortal.Fetch<FinancialCashInfo>(criteria);
            FinancialCash.CloseSession(criteria.SessionCode);
            return obj;
        }
		
		#endregion
					
		#region Common Data Access
								
        /// <summary>
        /// Obtiene un objeto a partir de un <see cref="IDataReader"/>.
        /// Obtiene los hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria"><see cref="IDataReader"/> con los datos</param>
        /// <remarks>
        /// La utiliza el <see cref="ReadOnlyListBaseEx"/> correspondiente para construir los objetos de la lista
        /// </remarks>
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
		#region Root Data Access
		 
        /// <summary>
        /// Obtiene un registro de la base de datos
        /// </summary>
        /// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
        /// <remarks>
        /// La llama el DataPortal
        /// </remarks>
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
            catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}
		
		#endregion
					
        #region SQL

        public static string SELECT(long oid) { return FinancialCash.SELECT(oid, false); }
        public static string SELECT(ChargeInfo cobro) { return FinancialCash.SELECT(new QueryConditions() { Cobro = cobro }, false); }
		public static string SELECT(QueryConditions conditions) { return FinancialCash.SELECT(conditions, false); }
		
        #endregion		
	}

    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class SerialEffectInfo : SerialInfo
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
        protected SerialEffectInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods

        public static SerialEffectInfo Get() { return Get(DateTime.MinValue.Year); }
        public static SerialEffectInfo Get(int year)
        {
            CriteriaEx criteria = Charge.GetCriteria(FinancialCash.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(year);

            SerialEffectInfo obj = DataPortal.Fetch<SerialEffectInfo>(criteria);
            FinancialCash.CloseSession(criteria.SessionCode);
            return obj;
        }

        public static long GetNext() { return Get().Value + 1; }
        public static long GetNext(int year) { return Get(year).Value + 1; }

        #endregion

        #region Root Data Access

        #endregion

        #region SQL

        public static string SELECT(int year)
        {
            string ef = nHManager.Instance.GetSQLTable(typeof(FinancialCashRecord));
            string query;

            QueryConditions conditions = new QueryConditions
            {
                FechaIni = DateAndTime.FirstDay(year),
                FechaFin = DateAndTime.LastDay(year)
            };

            query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
                    " FROM " + ef + " AS EF"; ;

            if (year != DateTime.MinValue.Year)
                query += " WHERE EF.\"VENCIMIENTO\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

            return query + ";";
        }

        #endregion
    }
}
