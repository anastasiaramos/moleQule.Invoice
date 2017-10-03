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
	/// ReadOnly Business Object Root Collection
	/// </summary>
    [Serializable()]
	public class TransactionList : ReadOnlyListBaseEx<TransactionList, TransactionInfo, Transaction>
	{	
		#region Business Methods
			
		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private TransactionList() {}
		private TransactionList(IList<Transaction> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private TransactionList(IList<TransactionInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods
		
		public static TransactionList NewList() { return new TransactionList(); }

		private static TransactionList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Invoice.Transaction.GetCriteria(Invoice.Transaction.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			TransactionList list = DataPortal.Fetch<TransactionList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		public static TransactionList GetList(bool childs=true)	{ return GetList(SELECT(), childs); }
		public static TransactionList GetList(QueryConditions conditions, bool childs) {	return GetList(SELECT(conditions), childs); }

		public static TransactionList GetBySubscription(long oidSuscription, ETransactionType transType, EEstado[] status, CriteriaEx criteria, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Oid = oidSuscription,
				EntityType = ETipoEntidad.Subscription,
				Transaction = TransactionInfo.New(),
				Status = status,
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};

			conditions.Transaction.ETransactionType = transType;

			return GetList(SELECT(conditions), childs);
		}

		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static TransactionList GetList(CriteriaEx criteria)
		{
			return TransactionList.RetrieveList(typeof(Transaction), AppContext.ActiveSchema.Code, criteria);
		}
        public static TransactionList GetList(IList<Transaction> list) { return new TransactionList(list,false); }
        public static TransactionList GetList(IList<TransactionInfo> list) { return new TransactionList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<TransactionInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<TransactionInfo> sortedList = new SortedBindingList<TransactionInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<TransactionInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<TransactionInfo> sortedList = new SortedBindingList<TransactionInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Transaction> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Transaction item in lista)
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
                this.AddItem(TransactionInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(TransactionInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Invoice.Transaction.SELECT_COUNT(criteria), criteria.Session);
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
				
		#endregion
		
        #region SQL

        public static string SELECT() { return TransactionInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Invoice.Transaction.SELECT(conditions, false); }
		
		#endregion		
	}
}
