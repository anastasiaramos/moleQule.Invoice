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
	/// ReadOnly Business Object Child Collection
	/// </summary>
    [Serializable()]
	public class CashLineList : ReadOnlyListBaseEx<CashLineList, CashLineInfo>
	{	
		#region Business Methods

        /// <summary>
        /// Devuelve un elemento 
        /// </summary>
        /// <returns>Elemento</returns>
        public CashLineInfo GetItemByCobro(long oid_cobro)
        {
            foreach (CashLineInfo item in this)
                if (item.OidCobro == oid_cobro)
                    return item;
            return null;
        }

		/// <summary>
		/// Actualiza el saldo de cada línea de caja
		/// </summary>
		public virtual void UpdateSaldo()
		{
			if (Items.Count == 0) return;

			//List<decimal> acumulados = new List<long>();		

			CashLineInfo acumulado = CashLineInfo.GetAcumulado(Items[0].Fecha, Items[0].OidCaja);

			int i = 0;
			for (i = 0; i < Items.Count; i++)
			{
				if (Items[i].EEstado == EEstado.Anulado)
				{
					Items[i].Saldo = 0;
					continue;
				}

				Items[i].Saldo = (acumulado.Debe - acumulado.Haber) + Items[i].Debe - Items[i].Haber;
				break;
			}

			int last_abierto = i;

			for (int j = i + 1; j < Items.Count; j++)
			{
				if (Items[j].EEstado == EEstado.Anulado)
				{
					Items[j].Saldo = 0;
					continue;
				}

				Items[j].Saldo = Items[last_abierto].Saldo + Items[j].Debe - Items[j].Haber;

				last_abierto = j;
			}
		}

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private CashLineList() {}
		private CashLineList(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
		private CashLineList(IList<CashLine> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private CashLineList(IList<CashLineInfo> list, bool childs)
		{
			Childs = childs;
			Fetch(list);
		}
		
		#endregion

        #region Root Factory Methods

		public static CashLineList NewList() { return new CashLineList(); }

        public static CashLineList GetList(QueryConditions conditions, bool childs)
        {
            return GetList(CashLineList.SELECT(conditions), childs);
        }
        public static CashLineList GetList(long oidCash) { return CashLineList.GetList(oidCash, true); }
        public static CashLineList GetList(long oidCash, bool childs) 
		{
            return CashLineList.GetList(oidCash, DateTime.MinValue, DateTime.MaxValue, childs); 
		}
        public static CashLineList GetList(long oidCash, int year, bool childs)
		{
            return GetList(oidCash, DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
        public static CashLineList GetList(long oidCash, DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Caja = Cash.New().GetInfo(),
				FechaIni = from,
				FechaFin = till
			};
            conditions.Caja.Oid = oidCash;
			return GetList(conditions, childs);			
		}

        public static CashLineList GetList(string query, bool childs)
        {
            CriteriaEx criteria = CashLine.GetCriteria(CreditCardStatement.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

            CashLineList list = DataPortal.Fetch<CashLineList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }		

        public static CashLineList GetByCreditCardStatement(long oidStatement, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                CreditCardStatement = (oidStatement) != 0 ? CreditCardStatementInfo.New(oidStatement) : null,
                MedioPago = EMedioPago.Tarjeta
            };

            return GetList(CashLine.SELECT_BY_CREDIT_CARD_STATEMENT(conditions, false), childs);
        }

		public static CashLineList GetList(IList<CashLine> list) { return new CashLineList(list, false); }
		public static CashLineList GetList(IList<CashLineInfo> list) { return new CashLineList(list, false); }

        #endregion

		#region Child Factory Methods
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static CashLineList GetChildList(IList<CashLine> list) { return new CashLineList(list, false); }
		public static CashLineList GetChildList(IList<CashLine> list, bool childs) { return new CashLineList(list, childs); }
		public static CashLineList GetChildList(IDataReader reader) { return new CashLineList(reader, false); } 
        public static CashLineList GetChildList(IDataReader reader, bool childs) { return new CashLineList(reader, childs); }
        public static CashLineList GetChildList(IList<CashLineInfo> list) { return new CashLineList(list, false); }
		
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<CashLine> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (CashLine item in lista)
				this.AddItem(item.GetInfo(Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(CashLineInfo.GetChild(reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion

        #region Root Data Access

        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

                    while (reader.Read())
                        this.AddItem(CashLineInfo.GetChild(reader, true));

					UpdateSaldo();

					IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        public static string SELECT(QueryConditions conditions) { return CashLine.SELECT(conditions, false); }
		public static string SELECT(CashInfo source)
		{
			QueryConditions conditions = new QueryConditions
			{
				Caja = source,
				CierreCaja = CierreCajaInfo.New()
			};

			return CashLine.SELECT(conditions, true);
		}
        public static string SELECT_BY_CAJA(long oid)
        {
			QueryConditions conditions = new QueryConditions 
			{ 
				Caja = Cash.New().GetInfo(),
				CierreCaja = CierreCaja.New().GetInfo()
			};
            conditions.Caja.Oid = oid;
			conditions.CierreCaja.Oid = 0;

            return CashLine.SELECT(conditions, true);
        }
        public static string SELECT_BY_CIERRE(long oid)
        {
            QueryConditions conditions = new QueryConditions();
            conditions.CierreCaja = CierreCaja.New().GetInfo();
            conditions.CierreCaja.Oid = oid;

            return CashLine.SELECT(conditions, true);
        }

        #endregion
	}
}

