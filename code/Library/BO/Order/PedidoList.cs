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
	/// </summary>
    [Serializable()]
	public class PedidoList : ReadOnlyListBaseEx<PedidoList, PedidoInfo>
	{	
		#region Business Methods

		public PedidoInfo GetItemByCodigo(string value)
		{
			foreach (PedidoInfo item in this)
				if (item.Codigo == value) return item;

			return null;
		}
		
		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private PedidoList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private PedidoList(IList<Pedido> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private PedidoList(IDataReader reader, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private PedidoList(IList<PedidoInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static PedidoList NewList() { return new PedidoList(); }

		public static PedidoList GetList() { return PedidoList.GetList(true); }
		public static PedidoList GetList(bool childs) { return GetList(new QueryConditions(), childs); }
		public static PedidoList GetList(int year, bool childs)
		{
			return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static PedidoList GetList(DateTime f_ini, DateTime f_fin, bool childs)
		{
			return GetList(0, f_ini, f_fin, childs);
		}
		public static PedidoList GetList(long oidCliente,
										DateTime f_ini,
										DateTime f_fin,
										bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Cliente = (oidCliente != 0) ? Cliente.New().GetInfo() : null,
				FechaIni = f_ini,
				FechaFin = f_fin,
			};

			if (oidCliente != 0) conditions.Cliente.Oid = oidCliente;

			return GetList(conditions, childs);
		}

		public static PedidoList GetPendientesList(long oidCliente, long oidSerie, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Cliente = (oidCliente != 0) ? Cliente.New().GetInfo() : null,
			};

			if (oidCliente != 0) conditions.Cliente.Oid = oidCliente;

			return GetList(Pedido.SELECT_PENDIENTES(conditions, false), childs);
		}

		public static PedidoList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(PedidoList.SELECT(conditions), childs);
		}
		private static PedidoList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Pedido.GetCriteria(Pedido.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			PedidoList list = DataPortal.Fetch<PedidoList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		public static PedidoList GetByClienteList(long oid, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Cliente = Cliente.New().GetInfo(),
			};
			conditions.Cliente.Oid = oid;

			return GetList(conditions, childs);
		}

		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static PedidoList GetList(CriteriaEx criteria)
		{
			return PedidoList.RetrieveList(typeof(Pedido), AppContext.ActiveSchema.Code, criteria);
		}
        public static PedidoList GetList(IList<Pedido> list) { return new PedidoList(list,false); }
        public static PedidoList GetList(IList<PedidoInfo> list) { return new PedidoList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<PedidoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<PedidoInfo> sortedList = new SortedBindingList<PedidoInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<PedidoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<PedidoInfo> sortedList = new SortedBindingList<PedidoInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		private void Fetch(IList<Pedido> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Pedido item in lista)
				this.AddItem(item.GetInfo(Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(PedidoInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(PedidoInfo.GetChild(SessionCode, reader, Childs));

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

		protected static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Pedido.SELECT(conditions, false); }

		#endregion
	}
}

