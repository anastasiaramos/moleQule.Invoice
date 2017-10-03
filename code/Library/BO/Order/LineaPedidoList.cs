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
	/// ReadOnly Business Object Root Collection
	/// ReadOnly Business Object Child Collection
	/// </summary>
    [Serializable()]
	public class LineaPedidoList : ReadOnlyListBaseEx<LineaPedidoList, LineaPedidoInfo>
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
		private LineaPedidoList() {}
		private LineaPedidoList(IList<LineaPedido> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private LineaPedidoList(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
			Childs = childs;
            Fetch(reader);
        }
		private LineaPedidoList(IList<LineaPedidoInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Default call for GetList(bool retrieve_childs)
		/// </summary>
		/// <returns></returns>
		public static LineaPedidoList GetList() { return LineaPedidoList.GetList(true); }
		public static LineaPedidoList GetList(bool childs)
		{
			CriteriaEx criteria = LineaPedido.GetCriteria(LineaPedido.OpenSession());
			criteria.Childs = childs;
			
			//No criteria. Retrieve all de List
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = LineaPedidoList.SELECT();
            
			LineaPedidoList list = DataPortal.Fetch<LineaPedidoList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		
		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static LineaPedidoList GetList(CriteriaEx criteria)
		{
			return LineaPedidoList.RetrieveList(typeof(LineaPedido), AppContext.ActiveSchema.Code, criteria);
		}
        public static LineaPedidoList GetList(IList<LineaPedido> list) { return new LineaPedidoList(list,false); }
        public static LineaPedidoList GetList(IList<LineaPedidoInfo> list) { return new LineaPedidoList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<LineaPedidoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<LineaPedidoInfo> sortedList = new SortedBindingList<LineaPedidoInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<LineaPedidoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<LineaPedidoInfo> sortedList = new SortedBindingList<LineaPedidoInfo>(GetList(childs));

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
		public static LineaPedidoList GetChildList(IList<LineaPedido> list) { return new LineaPedidoList(list, false); }
		public static LineaPedidoList GetChildList(IList<LineaPedido> list, bool childs) { return new LineaPedidoList(list, childs); }
		public static LineaPedidoList GetChildList(int sessionCode, IDataReader reader) { return new LineaPedidoList(sessionCode, reader, false); }
		public static LineaPedidoList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new LineaPedidoList(sessionCode, reader, childs); }
        public static LineaPedidoList GetChildList(IList<LineaPedidoInfo> list) { return new LineaPedidoList(list, false); }
		public static LineaPedidoList GetPendientesChildList(PedidoInfo parent, bool childs)
		{
			CriteriaEx criteria = LineaPedido.GetCriteria(LineaPedido.OpenSession());
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				Pedido = parent
			};
			criteria.Query = SELECT_PENDIENTES(conditions);

			LineaPedidoList list = DataPortal.Fetch<LineaPedidoList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<LineaPedido> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (LineaPedido item in lista)
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
                this.AddItem(LineaPedidoInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(LineaPedidoInfo.GetChild(SessionCode, reader, Childs));

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
		public static string SELECT(QueryConditions conditions) { return LineaPedido.SELECT(conditions, false); }
		public static string SELECT(PedidoInfo pedido) { return SELECT(new QueryConditions { Pedido = pedido }); }
		public static string SELECT_PENDIENTES(QueryConditions conditions) { return LineaPedido.SELECT_PENDIENTES(conditions, false); }

		#endregion
	}
}

