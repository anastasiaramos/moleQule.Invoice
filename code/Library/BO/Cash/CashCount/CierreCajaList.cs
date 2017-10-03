using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

using NHibernate;

namespace moleQule.Library.Invoice
{	
	/// <summary>
	/// ReadOnly Business Object With Childs Root Collection  
	/// ReadOnly Business Object With Childs Child Collection
	/// </summary>
    [Serializable()]
	public class CierreCajaList : ReadOnlyListBaseEx<CierreCajaList, CierreCajaInfo>
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
		private CierreCajaList() {}
		private CierreCajaList(IList<CierreCaja> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private CierreCajaList(IDataReader reader, bool childs)
        {
			Childs = childs;
            Fetch(reader);
        }
		private CierreCajaList(IList<CierreCajaInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static CierreCajaList NewList() { return new CierreCajaList(); }

		public static CierreCajaList GetList() { return CierreCajaList.GetList(true); }
		public static CierreCajaList GetList(bool childs) { return GetList(CierreCajaList.SELECT(), childs); }

		public static CierreCajaList GetListByCaja(long oid_caja, bool childs) 
		{
			QueryConditions conditions = new QueryConditions
			{
				Caja = Cash.New().GetInfo()
			};
			conditions.Caja.Oid = oid_caja;

			return GetList(CierreCajaList.SELECT(conditions), childs); 
		}

		public static CierreCajaList GetList(string query, bool childs)
		{
			CriteriaEx criteria = CierreCaja.GetCriteria(CierreCaja.OpenSession());
			criteria.Childs = childs;
			
			//No criteria. Retrieve all de List
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;
            
			CierreCajaList list = DataPortal.Fetch<CierreCajaList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		public static CierreCajaList GetList(CriteriaEx criteria)
		{
			return CierreCajaList.RetrieveList(typeof(CierreCaja), AppContext.ActiveSchema.Code, criteria);
		}
        public static CierreCajaList GetList(IList<CierreCaja> list) { return new CierreCajaList(list,false); }
        public static CierreCajaList GetList(IList<CierreCajaInfo> list) { return new CierreCajaList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<CierreCajaInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<CierreCajaInfo> sortedList = new SortedBindingList<CierreCajaInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<CierreCajaInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<CierreCajaInfo> sortedList = new SortedBindingList<CierreCajaInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Child Factory Methods
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static CierreCajaList GetChildList(IList<CierreCaja> list) { return new CierreCajaList(list, false); }

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
		/// <param name="childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		public static CierreCajaList GetChildList(IList<CierreCaja> list, bool childs) { return new CierreCajaList(list, childs); }

		/// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static CierreCajaList GetChildList(IDataReader reader) { return new CierreCajaList(reader, false); } 
		
		/// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        public static CierreCajaList GetChildList(IDataReader reader, bool childs) { return new CierreCajaList(reader, childs); }

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static CierreCajaList GetChildList(IList<CierreCajaInfo> list) { return new CierreCajaList(list, false); }
		
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<CierreCaja> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (CierreCaja item in lista)
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
                this.AddItem(CierreCajaInfo.GetChild(reader, Childs));

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
						this.AddItem(CierreCajaInfo.GetChild(reader, Childs));

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
		public static string SELECT(QueryConditions conditions)
		{
			string query = string.Empty;

			query = CierreCaja.SELECT(conditions, false);
			query += " ORDER BY \"FECHA\"";

			return query;
		}
		
		#endregion		
	}
}

