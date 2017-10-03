using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class ProductoClientes : BusinessListBaseEx<ProductoClientes, ProductoCliente>
    {
        #region Business Methods

        public ProductoCliente GetByProduct(long oid_producto)
        {
            foreach (ProductoCliente obj in this)
            {
                if (obj.OidProducto == oid_producto)
                    return obj;
            }
            return null;
        }

        public ProductoCliente NewItem(Cliente parent)
        {
            this.NewItem(ProductoCliente.NewChild(parent));
            return this[Count - 1];
        }

        public ProductoCliente NewItem(Product parent)
        {
            this.NewItem(ProductoCliente.NewChild(parent));
            return this[Count - 1];
        }

		public ProductoCliente NewItem(Cliente parent1, ProductInfo parent2)
		{
			this.NewItem(ProductoCliente.NewChild(parent1, parent2));
			return this[Count - 1];
		}

        /// <summary>
        /// Crea un nuevo item a partir de un ProductInfo
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        // Creado para que no tengamos que acceder a la base de datos continuamente,
        // para obtener el Oid del producto.
        public ProductoCliente NewItem(ProductInfo parent)
        {
            this.NewItem(ProductoCliente.NewChild(parent));
            return this[Count - 1];
        }

        public ProductoCliente SearchByParents(long oid_produco, long oid_cliente)
        {
            foreach (ProductoCliente obj in this)
                if (obj.OidCliente == oid_cliente &&
                    obj.OidProducto == oid_produco)
                    return obj;

            return null;
        }

        internal void UpdateHistoria(Cliente parent)
        {
            foreach (ProductoCliente item in this)
            {
                if (item.IsNew)
                {
                    parent.Historia += Environment.NewLine +
                                        DateTime.Now.ToString() + " - " +
                                        String.Format(Resources.Messages.NUEVO_PRODUCTO_ASOCIADO,
                                                    item.Producto,
                                                    item.Precio.ToString("C5"),
                                                    AppContext.Principal.Identity.Name);
                }
                else if (item.IsDirty)
                {
					ClientProductRecord obj = Session().Get<ClientProductRecord>(item.Oid);
                    if (item.Precio != obj.Precio)
                    {
                        parent.Historia += Environment.NewLine +
                                            DateTime.Now.ToString() + " - " +
                                            String.Format(Resources.Messages.EDITADO_PRODUCTO_ASOCIADO,
                                                        item.Producto,
                                                        item.Precio.ToString("C5"),
														AppContext.Principal.Identity.Name);
                    }
                }
            }

            foreach (ProductoCliente item in DeletedList)
            {
                parent.Historia += Environment.NewLine +
                                    DateTime.Now.ToString() + " - " +
                                    String.Format(Resources.Messages.BORRADO_PRODUCTO_ASOCIADO,
                                                item.Producto,
                                                item.Precio.ToString("C5"),
												AppContext.Principal.Identity.Name);
            }
        }

        #endregion

        #region Factory Methods

        private ProductoClientes()
        {
            MarkAsChild();
        }

        private ProductoClientes(IList<ProductoCliente> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }

        private ProductoClientes(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        public static ProductoClientes NewChildList() { return new ProductoClientes(); }

        public static ProductoClientes GetChildList(IList<ProductoCliente> lista) { return new ProductoClientes(lista); }

        public static ProductoClientes GetChildList(IDataReader reader, bool childs) { return new ProductoClientes(reader, childs); }

        public static ProductoClientes GetChildList(IDataReader reader) { return GetChildList(reader, true); }

        public static ProductoClientes GetChildList(Cliente parent, bool childs)
        {
            CriteriaEx criteria = ProductoCliente.GetCriteria(parent.SessionCode);

            criteria.Query = ProductoClientes.SELECT(parent);
            criteria.Childs = childs;

            return DataPortal.Fetch<ProductoClientes>(criteria);
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            try
            {
                this.RaiseListChangedEvents = false;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    ProductoCliente.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
						ProductoCliente obj = ProductoCliente.GetChild(SessionCode, reader);
                        this.AddItem(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<ProductoCliente> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (ProductoCliente item in lista)
                this.AddItem(ProductoCliente.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(ProductoCliente.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }

        internal void Update(Cliente parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (ProductoCliente obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (ProductoCliente obj in this)
				{
					if (obj.IsNew)
						obj.Insert(parent);
					else
						obj.Update(parent);
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
        }

        public void Update(Product parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (ProductoCliente obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (ProductoCliente obj in this)
				{
					if (obj.IsNew)
						obj.Insert(parent);
					else
						obj.Update(parent);
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
        }

        #endregion

        #region SQL

        public static string SELECT(QueryConditions conditions) { return ProductoCliente.SELECT(conditions, true); }
        public static string SELECT(Cliente source)
        {
            return ProductoClientes.SELECT(new QueryConditions { Cliente = source.GetInfo(false) });
        }
        public static string SELECT(Product source)
        {
            return ProductoClientes.SELECT(new QueryConditions { Producto = source.GetInfo(false) });
        }

        #endregion
    }
}
