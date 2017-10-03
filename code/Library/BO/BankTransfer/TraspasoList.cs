using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;

namespace moleQule.Library.Invoice
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
	/// </summary>
    [Serializable()]
	public class TraspasoList : ReadOnlyListBaseEx<TraspasoList, TraspasoInfo>
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
		private TraspasoList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private TraspasoList(IList<Traspaso> list, bool childs)
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
		private TraspasoList(IList<TraspasoInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static TraspasoList NewList() { return new TraspasoList(); }

		public static TraspasoList GetList() { return TraspasoList.GetList(true); }
		public static TraspasoList GetList(bool childs)
		{
			CriteriaEx criteria = Traspaso.GetCriteria(Traspaso.OpenSession());
			criteria.Childs = childs;
			
			//No criteria. Retrieve all de List
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = TraspasoList.SELECT();
            
			TraspasoList list = DataPortal.Fetch<TraspasoList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		
		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static TraspasoList GetList(CriteriaEx criteria)
		{
			return TraspasoList.RetrieveList(typeof(Traspaso), AppContext.ActiveSchema.Code, criteria);
		}

        public static TraspasoList GetList(QueryConditions conditions) { return GetList(conditions, false); }
        public static TraspasoList GetList(QueryConditions conditions, bool childs)
        {
            CriteriaEx criteria = Traspaso.GetCriteria(Traspaso.OpenSession());
            criteria.Childs = childs;

            //No criteria. Retrieve all de List

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = TraspasoList.SELECT(conditions);

            TraspasoList list = DataPortal.Fetch<TraspasoList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static TraspasoList GetList(IList<Traspaso> list) { return new TraspasoList(list,false); }
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static TraspasoList GetList(IList<TraspasoInfo> list) { return new TraspasoList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<TraspasoInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<TraspasoInfo> sortedList = new SortedBindingList<TraspasoInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		
		/// <summary>
        /// Devuelve una lista ordenada de todos los elementos y sus hijos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <param name="childs">Traer hijos</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<TraspasoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<TraspasoInfo> sortedList = new SortedBindingList<TraspasoInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Traspaso> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Traspaso item in lista)
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
                this.AddItem(TraspasoInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(TraspasoInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;
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

        public static string SELECT() { return TraspasoInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Traspaso.SELECT(conditions, false); }
		public static string SELECT(TraspasoInfo parent) { return  Traspaso.SELECT(new QueryConditions{ Traspaso = parent }, true); }
		
		#endregion		
	}
}
