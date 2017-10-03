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
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class BankLineInfo : ReadOnlyBaseEx<BankLineInfo>
	{	
		#region Attributes

		protected BankLineBase _base = new BankLineBase();
	
		#endregion
		
		#region Properties

		public BankLineBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidOperacion { get { return _base.Record.OidOperacion; } }
		public long TipoOperacion { get { return _base.Record.TipoOperacion; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long OidAuditor { get { return _base.Record.OidUsuario; } }
		public bool Auditado { get { return _base.Record.Auditado; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public long Estado { get { return _base.Record.Estado; } }
		public DateTime FechaOperacion { get { return _base.Record.FechaOperacion; } }
		public string IDOperacion { get { return _base.Record.IDOperacion; } }
		public Decimal Importe { get { return _base.Record.Importe; } }
		public long TipoCuenta { get { return _base.Record.TipoCuenta; } }
		public long OidCuentaMov { get { return _base.Record.OidCuentaMov; } }
		public DateTime FechaCreacion { get { return _base.Record.FechaCreacion; } }
		public long Tipo { get { return _base.Record.TipoMovimiento; } }

		//LINKED
		public virtual EEstado EEstado { get { return _base.EStatus; } } //DEPRECTATED
		public virtual string EstadoLabel { get { return _base.StatusLabel; } } //DEPRECTATED
		public virtual EEstado EStatus { get { return _base.EStatus; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
        public string Auditor { get { return _base._auditor; } }
		public long OidCuenta { get { return _base._oid_cuenta_ex; } }
		public string Cuenta { get { return _base._cuenta; } }
		public string Entidad { get { return _base._entidad; } }
		public string Titular { get { return _base._titular; } }
		public long TipoTitular { get { return _base._tipo_titular; } }
		public ETipoTitular ETipoTitular { get { return (ETipoTitular)_base._tipo_titular; } }
        public string ETipoTitularLabel { get { return Library.Common.EnumText<ETipoTitular>.GetLabel(ETipoTitular); } }
		public long MedioPago { get { return _base._medio_pago; } }
		public EMedioPago EMedioPago { get { return (EMedioPago)_base._medio_pago; } }
		public string EMedioPagoLabel { get { return Common.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
        public EBankLineType ETipoMovimientoBanco { get { return (EBankLineType)TipoOperacion; } }
        public string ETipoMovimientoBancoLabel { get { return Library.Invoice.EnumText<EBankLineType>.GetLabel(ETipoMovimientoBanco); } }
		public string IDMovimientoContable { get { return _base._id_mov_contable; } }
		public Decimal Saldo { get { return _base._saldo; } set { _base._saldo = value; } }
		public virtual ECuentaBancaria ETipoCuenta { get { return (ECuentaBancaria)TipoCuenta; } }
		public virtual ETipoApunteBancario ETipo { get { return (ETipoApunteBancario)Tipo; } }
        public virtual string ETipoLabel { get { return EnumText<ETipoApunteBancario>.GetLabel(ETipo); } }
        public virtual long OidTitular { get { return _base.OidTitular; } }

		#endregion
		
		#region Business Methods

        public void CopyFrom(BankLine source) { _base.CopyValues(source); }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected BankLineInfo() { /* require use of factory methods */ }
		internal BankLineInfo(BankLine item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
			}
		}

		public static BankLineInfo New(long oid = 0) { return new BankLineInfo() { Oid = oid }; }

 		#endregion

		#region Child Factory Methods

		private BankLineInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}

		public static BankLineInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static BankLineInfo GetChild(int sessionCode, IDataReader reader, bool childs)
		{
			return new BankLineInfo(sessionCode, reader, childs);
		}

		#endregion

		#region Root Factory Methods
		
        public static BankLineInfo Get(long oid,
                                                EBankLineType bankLineType,
                                                ETipoTitular tipo_titular)
        {
            return Get(oid, bankLineType, tipo_titular, false);
        }
		
		public static BankLineInfo Get(long oid, 
                                                EBankLineType bankLineType,
                                                ETipoTitular tipo_titular,
                                                bool childs)
		{
			CriteriaEx criteria = BankLine.GetCriteria(BankLine.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
                criteria.Query = BankLineInfo.SELECT(oid, bankLineType, tipo_titular);
	
			BankLineInfo obj = DataPortal.Fetch<BankLineInfo>(criteria);
			BankLine.CloseSession(criteria.SessionCode);

			return obj;
		}

        public static BankLineInfo GetByIMovimiento(IBankLine item)
        {
            CriteriaEx criteria = BankLine.GetCriteria(BankLine.OpenSession());
            criteria.Childs = false;

            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = item.IGetInfo(false) };
            
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = BankLineInfo.SELECT(conditions);

            BankLineInfo obj = DataPortal.Fetch<BankLineInfo>(criteria);
            BankLine.CloseSession(criteria.SessionCode);

            return obj;
        }

        public static BankLineInfo GetByPagoTarjeta(PaymentInfo source)
        {
            QueryConditions conditions = new QueryConditions
            {
                Pago = source,
                TipoMovimientoBanco = EBankLineType.ExtractoTarjeta,
                Estado = EEstado.NoAnulado
            };

            CriteriaEx criteria = BankLine.GetCriteria(BankLine.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = BankLineInfo.SELECT_BY_PAGO_TARJETA(conditions);

            BankLineInfo obj = DataPortal.Fetch<BankLineInfo>(criteria);
            BankLine.CloseSession(criteria.SessionCode);

            return obj;
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
					
                    if (Childs)
					{
						string query = string.Empty;						
                    }
				}
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
		}
		
		#endregion
		
		#region Child Data Access
		
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
				
				if (Childs)
				{
				}
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
        #region SQL

        public static string SELECT(long oid, EBankLineType bankLineType, ETipoTitular tipo_titular) { return BankLine.SELECT(oid, bankLineType, tipo_titular, false); }
        public static string SELECT(QueryConditions conditions) { return BankLine.SELECT(conditions, false); }
        public static string SELECT_BY_TARJETA(QueryConditions conditions) { return BankLine.SELECT_BY_CREDIT_CARD(conditions, false); }
        public static string SELECT_BY_PAGO_TARJETA(QueryConditions conditions) { return BankLine.SELECT_BY_PAGO_TARJETA(conditions); }

        #endregion		
	}

	/// <summary>
	/// ReadOnly Root Object
	/// </summary>
	[Serializable()]
	public class BankLineSerialInfo : SerialInfo
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
		protected BankLineSerialInfo() { /* require use of factory methods */ }

		#endregion

		#region Root Factory Methods

		/// <summary>
		/// Obtiene el último serial de la entidad desde la base de datos
		/// </summary>
		/// <param name="oid">Oid del objeto</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
		public static BankLineSerialInfo Get(int year)
		{
			CriteriaEx criteria = Charge.GetCriteria(Charge.OpenSession());
			criteria.Childs = false;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(year);

			BankLineSerialInfo obj = DataPortal.Fetch<BankLineSerialInfo>(criteria);
			Charge.CloseSession(criteria.SessionCode);
			return obj;
		}

		/// <summary>
		/// Obtiene el siguiente serial para una entidad desde la base de datos
		/// </summary>
		/// <param name="entity">Tipo de Entidad</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
		public static long GetNext(int year)
		{
			return Get(year).Value + 1;
		}

		#endregion

		#region Root Data Access

		#endregion

		#region SQL
		
		public static BankLine.SelectLocalCaller local_caller_SELECT = new BankLine.SelectLocalCaller(SELECT_BASE);
		
		internal static string SELECT_BASE(EBankLineType bankLineType, ETipoTitular tipo_titular)
        {
            string mv = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));

            string query;

            query = "SELECT 0 AS \"OID\", MAX(MV.\"SERIAL\") AS \"SERIAL\"" +
                    " FROM " + mv + " AS MV";
            
            return query;
        }

		public static string SELECT(int year)
		{
			string mv = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));

			string query;

			QueryConditions conditions = new QueryConditions
			{
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)			
			};

			/*query = "SELECT 0 AS \"OID\", MAX(MVB.\"SERIAL\") AS \"SERIAL\"" +
					" FROM (";
			
            query += MovimientoBanco.SELECT_BUILDER(local_caller_SELECT, conditions);			

			query += ") AS MVB";*/

			query = 
			"	SELECT 0 AS \"OID\", MAX(MV.\"SERIAL\") AS \"SERIAL\"" +
			"	FROM " + mv + " AS MV" +
			"	WHERE (MV.\"FECHA_OPERACION\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            return query;
		}

		#endregion

	}
}