using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Read Only Child Collection of Business Objects
    /// </summary>
    [Serializable()]
    public class ClientProductList : ReadOnlyListBaseEx<ClientProductList, ProductoClienteInfo, ProductoCliente>
    {
        #region Bussines Methods
       
        public ProductoClienteInfo GetByProducto(long oidProduct)
        {
			return this.FirstOrDefault(x => x.OidProducto == oidProduct);
        }

		public ProductoClienteInfo SearchByParents(long oidProduct, long oidClient)
        {
			return this.FirstOrDefault(x => x.OidProducto == oidProduct && x.OidCliente == oidClient);
        }

        #endregion

        #region Factory Methods

        private ClientProductList() { }
        private ClientProductList(IList<ProductoCliente> lista)
        {
            Fetch(lista);
        }
        private ClientProductList(IDataReader reader)
        {
            Fetch(reader);
        }

        private static ClientProductList GetList(string query, bool childs)
        {
            CriteriaEx criteria = ProductoCliente.GetCriteria(ProductoCliente.OpenSession());
            criteria.Childs = childs;

            criteria.Query = query;
            ClientProductList list = DataPortal.Fetch<ClientProductList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
		public static ClientProductList GetList(bool childs = true)
        {
            CriteriaEx criteria = ProductoCliente.GetCriteria(ProductoCliente.OpenSession());
            criteria.Childs = childs;

            criteria.Query = SELECT();

            ClientProductList list = DataPortal.Fetch<ClientProductList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
        public static ClientProductList GetList(QueryConditions conditions, bool childs)
        {
            string query = ClientProductList.SELECT(conditions);
            return GetList(query, childs);
        }

        public static ClientProductList GetByClientList(long oidClient, bool childs = true)
        {
            QueryConditions conditions = new QueryConditions { Cliente = ClienteInfo.New(oidClient) };
            return GetList(conditions, childs);
        }

        /// <summary>
        /// Builds a ProductoClienteList from a IList<!--<ProductoClienteInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>ProductoClienteList</returns>
        public static ClientProductList GetChildList(IList<ProductoClienteInfo> list)
        {
            ClientProductList flist = new ClientProductList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ProductoClienteInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static ClientProductList GetChildList(IList<ProductoCliente> list) { return new ClientProductList(list); }
        public static ClientProductList GetChildList(IDataReader reader) { return new ClientProductList(reader); }
        public static ClientProductList GetChildList(ClienteInfo parent, bool childs)
        {
            CriteriaEx criteria = ProductoCliente.GetCriteria(ProductoCliente.OpenSession());

            criteria.Query = ClientProductList.SELECT(parent);
            criteria.Childs = childs;

            ClientProductList list = DataPortal.Fetch<ClientProductList>(criteria);
            CloseSession(criteria.SessionCode);

            return list;
        }

        #endregion

        #region Data Access

        // called to copy objects data from list
        private void Fetch(IList<ProductoCliente> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (ProductoCliente item in lista)
                this.AddItem(item.GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(ProductoCliente.GetChild(SessionCode, reader).GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to retrieve data from db
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
						this.AddItem(ProductoClienteInfo.GetChild(SessionCode, reader, Childs));

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

		public static string SELECT() { return ProductoCliente.SELECT(new QueryConditions(), false); }
        public static string SELECT(QueryConditions conditions) { return ProductoCliente.SELECT(conditions, false); }
        public static string SELECT(ClienteInfo source)
        {
            QueryConditions conditions = new QueryConditions { Cliente = source };
            return ClientProductList.SELECT(conditions);
        }
        public static string SELECT(ProductInfo source)
        {
            QueryConditions conditions = new QueryConditions { Producto = source };
            return ClientProductList.SELECT(conditions);
        }

        #endregion
    }
}

