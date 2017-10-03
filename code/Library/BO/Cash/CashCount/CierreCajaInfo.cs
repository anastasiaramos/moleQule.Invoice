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

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class CierreCajaInfo : ReadOnlyBaseEx<CierreCajaInfo>
	{	
		#region Attributes

		protected CashCountBase _base = new CashCountBase();

		protected CashLineList _lineas = null;

		#endregion
		
		#region Properties

		public CashCountBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidCaja { get { return _base.Record.OidCaja; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public Decimal Debe { get { return _base.Record.Debe; } }
		public Decimal Haber { get { return _base.Record.Haber; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public long OidUsuario { get { return _base.Record.OidUsuario; } }
		
		public CashLineList LineaCajas { get { return _lineas; } }

        //NO ENLAZADAS
		public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }
		public virtual string Caja { get { return _base.Caja; } set { _base.Caja = value; } }
		public virtual decimal DebeAcumulado { get { return _base.DebeAcumulado; } set { _base.DebeAcumulado = value; } }
		public virtual decimal HaberAcumulado { get { return _base.HaberAcumulado; } set { _base.HaberAcumulado = value; } }
		public virtual decimal SaldoInicial { get { return _base.SaldoInicial; } set { _base.SaldoInicial = value; } }
		public virtual decimal Saldo { get { return _base.Saldo; } }
		public virtual decimal SaldoAcumulado { get { return _base.SaldoAcumulado; } }
		public virtual decimal SaldoFinal { get { return _base.SaldoFinal; } set { _base.SaldoFinal = value; } }

        #endregion
		
		#region Business Methods

        public void CopyFrom(CierreCaja source) { _base.CopyValues(source); }

		public virtual void UpdateSaldo()
		{
			if (LineaCajas == null) return;
			if (LineaCajas.Count == 0) return;

			SaldoInicial = SaldoAcumulado;

			int i = 0;
			for (i = 0; i < LineaCajas.Count; i++)
			{
				if (LineaCajas[i].EEstado == EEstado.Anulado)
				{
					LineaCajas[i].Saldo = 0;
					continue;
				}

				LineaCajas[i].Saldo =  SaldoInicial + LineaCajas[i].Debe - LineaCajas[i].Haber;
				break;
			}

			int last_abierto = i;

			for (int j = i + 1; j < LineaCajas.Count; j++)
			{
				if (LineaCajas[j].EEstado == EEstado.Anulado)
				{
					LineaCajas[j].Saldo = 0;
					continue;
				}

				LineaCajas[j].Saldo = LineaCajas[last_abierto].Saldo + LineaCajas[j].Debe - LineaCajas[j].Haber;

				last_abierto = j;
			}
		}

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected CierreCajaInfo() { /* require use of factory methods */ }
		private CierreCajaInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
		internal CierreCajaInfo(CierreCaja item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				_lineas = (item.LineaCajas != null) ? CashLineList.GetChildList(item.LineaCajas) : null;				
			}
		}
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        /// <remarks>
		/// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool childs)
		/// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
		/// <remarks/>
		public static CierreCajaInfo GetChild(IDataReader reader)
        {
			return GetChild(reader, false);
		}
		public static CierreCajaInfo GetChild(IDataReader reader, bool childs)
        {
			return new CierreCajaInfo(reader, childs);
		}

		public static CierreCajaInfo New(long oid = 0) { return new CierreCajaInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static CierreCajaInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = CierreCaja.GetCriteria(CierreCaja.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = CierreCajaInfo.SELECT(oid);
	
			CierreCajaInfo obj = DataPortal.Fetch<CierreCajaInfo>(criteria);
			CierreCaja.CloseSession(criteria.SessionCode);
			return obj;
		}

        public static CierreCajaInfo GetByDate(DateTime fecha, bool childs)
        {
            CriteriaEx criteria = CierreCaja.GetCriteria(CierreCaja.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = CierreCajaInfo.SELECT_BY_FECHA(fecha);

            CierreCajaInfo obj = DataPortal.Fetch<CierreCajaInfo>(criteria);
            CierreCaja.CloseSession(criteria.SessionCode);
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
			try
			{
				_base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);
					
                    if (Childs)
					{
						string query = string.Empty;
	                    
						query = CashLineList.SELECT_BY_CIERRE(Oid);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _lineas = CashLineList.GetChildList(reader);						
                    }
				}

                UpdateSaldo();
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
					string query = string.Empty;
					IDataReader reader;
					
					query = CashLineList.SELECT_BY_CIERRE(Oid);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _lineas = CashLineList.GetChildList(reader);
                }

                UpdateSaldo();
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion
		
        #region SQL

        public static string SELECT(long oid) { return CierreCaja.SELECT(oid, false); }
        
        public static string SELECT_BY_FECHA(DateTime fecha) { return CierreCaja.SELECT_BY_FECHA(fecha, false); }

        #endregion		
	}

    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class SerialCierreCajaInfo : SerialInfo
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
        protected SerialCierreCajaInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Obtiene el último serial de la entidad desde la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static SerialCierreCajaInfo Get(long oid_caja)
        {
            CriteriaEx criteria = CashLine.GetCriteria(CierreCaja.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(oid_caja);

            SerialCierreCajaInfo obj = DataPortal.Fetch<SerialCierreCajaInfo>(criteria);
            CashLine.CloseSession(criteria.SessionCode);
            return obj;
        }

        /// <summary>
        /// Obtiene el último serial de la entidad desde la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static SerialCierreCajaInfo Get(long oid_caja, int year)
        {
            CriteriaEx criteria = CashLine.GetCriteria(CierreCaja.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(oid_caja, year);

            SerialCierreCajaInfo obj = DataPortal.Fetch<SerialCierreCajaInfo>(criteria);
            CashLine.CloseSession(criteria.SessionCode);
            return obj;
        }

        /// <summary>
        /// Obtiene el siguiente serial para una entidad desde la base de datos
        /// </summary>
        /// <param name="entity">Tipo de Entidad</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static long GetNext(long oid_caja)
        {
            return Get(oid_caja).Value + 1;
        }

        public static long GetNext(long oid_caja, int year)
        {
            return Get(oid_caja, year).Value + 1;
        }

        #endregion

        #region Root Data Access

        #endregion

        #region SQL

		public static string SELECT(long oidCaja)
        {
            string cc = nHManager.Instance.GetSQLTable(typeof(CashCountRecord));
            string query;

            query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
                    " FROM " + cc + " AS CC" +
					" WHERE CC.\"OID_CAJA\" = " + oidCaja;

            return query;
        }

        public static string SELECT(long oidCaja, int year)
        {
            string cc = nHManager.Instance.GetSQLTable(typeof(CashCountRecord));
            string query;

            QueryConditions conditions = new QueryConditions
			{
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};

            query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
                    " FROM " + cc + " AS CC" +
                    " WHERE CC.\"OID_CAJA\" = " + oidCaja +
                    " AND \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

            return query;
        }

        #endregion

    }
}
