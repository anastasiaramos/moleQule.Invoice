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
	public class BudgetList : ReadOnlyListBaseEx<BudgetList, BudgetInfo>
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
		private BudgetList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private BudgetList(IList<Budget> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private BudgetList(IDataReader reader, bool childs)
        {
			Childs = childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private BudgetList(IList<BudgetInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static BudgetList NewList() { return new BudgetList(); }

		public static BudgetList GetList() { return BudgetList.GetList(true); }
		public static BudgetList GetList(bool childs)
		{
			return GetList(childs, DateTime.MinValue, DateTime.MaxValue);
		}
		public static BudgetList GetList(bool childs, int year)
		{
			return GetList(childs, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}
		public static BudgetList GetList(bool childs, DateTime f_ini, DateTime f_fin)
		{
			CriteriaEx criteria = Budget.GetCriteria(Budget.OpenSession());
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				FechaIni = f_ini,
				FechaFin = f_fin,
			};
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = BudgetList.SELECT(conditions);
            
			BudgetList list = DataPortal.Fetch<BudgetList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		public static BudgetList GetList(QueryConditions conditions, bool childs)
		{
			CriteriaEx criteria = Budget.GetCriteria(Budget.OpenSession());
			criteria.Childs = childs;

			criteria.Query = BudgetList.SELECT(conditions);

			BudgetList list = DataPortal.Fetch<BudgetList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}

		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static BudgetList GetList(CriteriaEx criteria)
		{
			return BudgetList.RetrieveList(typeof(Budget), AppContext.ActiveSchema.Code, criteria);
		}
        public static BudgetList GetList(IList<Budget> list) { return new BudgetList(list,false); }
        public static BudgetList GetList(IList<BudgetInfo> list) { return new BudgetList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<BudgetInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<BudgetInfo> sortedList = new SortedBindingList<BudgetInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<BudgetInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<BudgetInfo> sortedList = new SortedBindingList<BudgetInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Budget> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Budget item in lista)
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
                this.AddItem(BudgetInfo.GetChild(reader, Childs));

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
						this.AddItem(BudgetInfo.GetChild(reader, Childs));

					IsReadOnly = true;
				}
				else 
				{
					IList list = criteria.List();
					
					if (list.Count > 0)
					{
						IsReadOnly = false;
						foreach(Budget item in list)
							this.Add(item.GetInfo(false));
							
						IsReadOnly = true;
					}
				}
			}
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
			
			this.RaiseListChangedEvents = true;
		}
				
		#endregion
		
        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Budget.SELECT(conditions, false); }

		#endregion		
	}
}

