using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Invoice
{	
	/// <summary>
	/// </summary>
    [Serializable()]
	public class FinancialCashList : ReadOnlyListBaseEx<FinancialCashList, FinancialCashInfo>
	{	
		#region Business Methods

        public FinancialCashInfo GetItemByCobro(long oidCharge)
        {
            return Items.FirstOrDefault(x => x.OidCobro == oidCharge && 
                                            x.EEstado != EEstado.Anulado);
        }

        public decimal TotalNegociado()
        {
            return Items.Where(x => x.Adelantado == true && 
                                    x.EEstado != EEstado.Anulado &&
                                    x.EEstadoCobro != EEstado.Devuelto).Sum(x => x.Importe);
        }

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private FinancialCashList() {}
		private FinancialCashList(IList<FinancialCash> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private FinancialCashList(IList<FinancialCashInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }

        #endregion

        #region Root Factory Methods

        public static FinancialCashList NewList() { return new FinancialCashList(); }

        private static FinancialCashList GetList(string query, bool childs)
        {
            CriteriaEx criteria = FinancialCash.GetCriteria(FinancialCash.OpenSession());
            criteria.Childs = childs;

            criteria.Query = query;
            FinancialCashList list = DataPortal.Fetch<FinancialCashList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
        public static FinancialCashList GetList(bool childs = true)
        {
            return GetList(DateTime.MinValue, DateTime.MaxValue, childs);
        }
        public static FinancialCashList GetList(int year, bool childs)
        {
            return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
        }
        public static FinancialCashList GetList(DateTime from, DateTime till, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                FechaIni = from,
                FechaFin = till
            };

            return GetList(SELECT(conditions), childs);
        }

        public static FinancialCashList GetNegociadosList(DateTime dueDateFrom, DateTime dueDateTill, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                FechaAuxIni = dueDateFrom,
                FechaAuxFin = dueDateTill
            };

            return GetList(FinancialCash.SELECT_NEGOCIADOS(conditions, false), childs);
        }

        /// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static FinancialCashList GetList(IList<FinancialCashInfo> list) { return new FinancialCashList(list, false); }
        public static FinancialCashList GetList(IList<FinancialCash> list, bool get_childs)
        {
            FinancialCashList flist = new FinancialCashList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (FinancialCash item in list)
                    flist.AddItem(item.GetInfo(get_childs));

                flist.IsReadOnly = true;
            }

            return flist;
        }
		
		#endregion

        #region Common Data Access

        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IsReadOnly = false;

                    while (reader.Read())
                        this.AddItem(FinancialCashInfo.GetChild(reader, Childs));

                    IsReadOnly = true;

                    if (criteria.PagingInfo != null)
                    {
                        reader = nHManager.Instance.SQLNativeSelect(FinancialCash.SELECT_COUNT(criteria), criteria.Session);
                        if (reader.Read()) criteria.PagingInfo.TotalItems = Format.DataReader.GetInt32(reader, "TOTAL_ROWS");
                    }
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }

            this.RaiseListChangedEvents = true;
        }

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<FinancialCash> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (FinancialCash item in lista)
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
                this.AddItem(FinancialCashInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion
		
        #region SQL

        public static string SELECT() { return FinancialCashInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return FinancialCash.SELECT(conditions, false); }
		
		#endregion		
	}
}
