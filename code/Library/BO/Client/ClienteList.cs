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
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{	
	/// <summary>
	/// ReadOnly Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class ClienteList : ReadOnlyListBaseEx<ClienteList, ClienteInfo, Cliente>
	{	
		#region Business Methods

        public static IDataReader GetPrices() { return GetPrices(new Library.Invoice.QueryConditions()); }

        public static IDataReader GetPrices(Library.Invoice.QueryConditions conditions)
        {
            CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

            criteria.Query = ClienteList.SELECT_PRICES(conditions);
            IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, criteria.Session);
            CloseSession(criteria.SessionCode);
            return reader;
        }

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private ClienteList() {}
		private ClienteList(IList<Cliente> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private ClienteList(IList<ClienteInfo> list, bool childs)
		{
			Childs = childs;
			Fetch(list);
		}
		private ClienteList(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
			Childs = childs;
            Fetch(reader);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static ClienteList NewList() { return new ClienteList(); }

		private static ClienteList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			ClienteList list = DataPortal.Fetch<ClienteList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		public static ClienteList GetList(bool childs = true) { return GetList(EEstado.Todos, childs); }
		public static ClienteList GetList(EEstado estado, bool childs)
		{
			return GetList(SELECT(new QueryConditions { Status = new EEstado[] { estado } }), childs);
		}

		public static ClienteList GetListBySignUp(DateTime iniDate, DateTime endDate, CriteriaEx criteria, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = (criteria != null) ? criteria.PagingInfo : null,
				Filters = (criteria != null) ? criteria.Filters : null,
				Orders = (criteria != null) ? criteria.Orders : null,
				FechaIni = iniDate, 
				FechaFin = endDate
			};

			string query = Cliente.SELECT_USERS(conditions, false);
			if (criteria != null) criteria.PagingInfo = conditions.PagingInfo;

			return GetList(query, criteria, childs);
		}		

        public static ClienteList GetListByList(List<string> oid_list, bool childs)
        {
			return GetList(Cliente.SELECT_BY_LIST(oid_list, false), childs);
        }

		public static ClienteList GetUsersList(CriteriaEx criteria, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders,
				Schema = AppContext.ActiveSchema
			};

			string query = Cliente.SELECT_USERS(conditions, false);
			if (criteria != null) criteria.PagingInfo = conditions.PagingInfo;

			return GetList(query, criteria, childs);
		}
		public static ClienteList GetLockedOutList(CriteriaEx criteria, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};

			string query = Cliente.SELECT_LOCKEDOUT(conditions, false);
			if (criteria != null) criteria.PagingInfo = conditions.PagingInfo;

			return GetList(query, criteria, childs);
		}
		public static ClienteList GetPendingApprovalList(CriteriaEx criteria, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};

			string query = Cliente.SELECT_PENDING_APPROVAL(conditions, false);
			if (criteria != null) criteria.PagingInfo = conditions.PagingInfo;

			return GetList(query, criteria, childs);
		}

        public static ClienteList GetList(IList<Cliente> list) { return new ClienteList(list, false); }
        public static ClienteList GetList(IList<ClienteInfo> list) { return new ClienteList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<ClienteInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<ClienteInfo> sortedList = new SortedBindingList<ClienteInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<ClienteInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<ClienteInfo> sortedList = new SortedBindingList<ClienteInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Cliente> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Cliente item in lista)
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
                this.AddItem(ClienteInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(ClienteInfo.GetChild(SessionCode, reader, Childs));

  					IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Cliente.SELECT_COUNT(criteria), criteria.Session);
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

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Cliente.SELECT(Cliente.EQueryType.GENERAL, conditions, false); }

        /// <summary>
        /// Construye el SELECT para traer todos los precios de un producto
        /// asociados a todos los clientes
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static string SELECT_PRICES(Library.Invoice.QueryConditions conditions)
        {
            string t = nHManager.Instance.GetSQLTable(typeof(ClientProductRecord));
            string tc = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
            string fa = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
            string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
            string query;

            query = "SELECT PC.*," +
                    "       PC.\"PRECIO\" AS \"PRECIO_ASOCIADO\"," +
                    "       CL.\"CODIGO\" || ' - ' || CL.\"NOMBRE\" AS \"TITULAR\"," +
                    "       P.\"NOMBRE\" as \"PRODUCTO\"," +
                    "       P.\"PRECIO_VENTA\" AS \"PRECIO_PRODUCTO\"," +
                    "       CF1.\"PRECIO_MEDIO\" AS \"PRECIO_MEDIO\"" +
                    " FROM " + t + " AS PC " +
                    " INNER JOIN " + tc + " AS CL ON PC.\"OID_CLIENTE\" = CL.\"OID\"" +
                    " INNER JOIN " + pr + " AS P ON PC.\"OID_PRODUCTO\" = P.\"OID\"" +
                    " INNER JOIN (SELECT CF.\"OID_PRODUCTO\", AVG(CF.\"PRECIO\") AS \"PRECIO_MEDIO\"" +
                    "               FROM " + cf + " AS CF " +
                    "               INNER JOIN " + fa + " AS FA ON CF.\"OID_FACTURA\" = FA.\"OID\"" +
                    "               WHERE CF.\"FACTURACION_BULTO\" = FALSE" +
                    "               AND FA.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    "               GROUP BY CF.\"OID_PRODUCTO\", FA.\"OID_CLIENTE\")" +
                    "               AS CF1 ON (CF1.\"OID_PRODUCTO\" = P.\"OID\")" +
                    " WHERE TRUE";

            if (conditions.Familia != null)
                query += " AND P.\"OID_FAMILIA\" = " + conditions.Familia.Oid.ToString();

            if (conditions.Producto != null)
                query += " AND P.\"OID\" = " + conditions.Producto.Oid.ToString();

            query += " ORDER BY CL.\"NOMBRE\" " + ((conditions.Order == ListSortDirection.Ascending) ? "ASC" : "DESC");

            return query;
        }

        #endregion
	}
}

