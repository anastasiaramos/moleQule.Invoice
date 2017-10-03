using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class CashList : ReadOnlyListBaseEx<CashList, CashInfo>
	{	
		#region Business Methods

		public decimal TotalSaldo()
		{
			decimal total = 0;
			foreach (CashInfo item in this)
			{
				total += item.SaldoTotal;
			}

			return total;
		}

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private CashList() {}
		private CashList(IList<Cash> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private CashList(IList<CashInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion

		#region Child Factory Methods

		private CashList(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}

		#endregion

		#region Root Factory Methods

		public static CashList NewList() { return new CashList(); }

		public static CashList GetList() { return CashList.GetList(true); }
		public static CashList GetList(bool childs) { return GetList(DateTime.MaxValue, childs); }
		public static CashList GetList(DateTime f_fin, bool childs)
		{
			QueryConditions conditions = new QueryConditions { FechaFin = f_fin };
			return GetList(conditions, childs);
		}

		public static CashList GetList(QueryConditions conditions, bool childs)
		{
			CriteriaEx criteria = Cash.GetCriteria(Cash.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = CashList.SELECT(conditions);

			CashList list = DataPortal.Fetch<CashList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		public static CashList GetList(CriteriaEx criteria)
		{
			return CashList.RetrieveList(typeof(Cash), AppContext.ActiveSchema.Code, criteria);
		}
        public static CashList GetList(IList<Cash> list) { return new CashList(list,false); }
        public static CashList GetList(IList<CashInfo> list) { return new CashList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<CashInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<CashInfo> sortedList = new SortedBindingList<CashInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<CashInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<CashInfo> sortedList = new SortedBindingList<CashInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Cash> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Cash item in lista)
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
                this.AddItem(CashInfo.GetChild(reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion

		#region Root Data Access
		 
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
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
						this.AddItem(CashInfo.GetChild(reader, Childs));

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

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Cash.SELECT(conditions, false); }

		#endregion				
	}
}