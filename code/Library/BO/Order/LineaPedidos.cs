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
	/// Editable Business Object Root Collection
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class LineaPedidos : BusinessListBaseEx<LineaPedidos, LineaPedido>
    {		
		#region Root Business Methods

		public LineaPedido NewItem(Pedido parent)
		{
			this.AddItem(LineaPedido.NewChild(parent));
			return this[Count - 1];
		}
        public LineaPedido NewItem(Pedido parent, ProductInfo producto)
        {
            this.AddItem(LineaPedido.NewChild(parent, producto));
            return this[Count - 1];
        }
		public LineaPedido NewItem(Pedido parent, BatchInfo partida)
		{
			this.AddItem(LineaPedido.NewChild(parent, partida));
			return this[Count - 1];
		}

		public void Remove(LineaPedido item, Pedido parent)
		{
			Remove(item);
			parent.CalculateTotal();
		}

		public LineaPedido GetComponente(BatchInfo componente)
		{
			foreach (LineaPedido item in this)
				if ((item.OidKit == componente.OidKit) && (item.OidPartida == componente.Oid))
					return item;

			return null;
		}

        #endregion
		
		#region Child Business Methods
		
		/// <summary>
		/// Crea un nuevo elemento y lo añade a la lista
		/// </summary>
		/// <returns>Nuevo item</returns>
		public LineaPedido NewItem(PedidoProveedor parent)
		{
			this.NewItem(LineaPedido.NewChild(parent));
			return this[Count - 1];
		}
		
		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private LineaPedidos() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static LineaPedidos NewList() 
		{ 	
			if (!LineaPedido.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new LineaPedidos(); 
		}

        /// <summary>
        /// Obtiene de la base de datos todos los elementos y construye la lista
        /// </summary>
        /// <returns>Lista de los elementos de la tabla en la base de datos</returns>
        /// <remarks>No obtiene los hijos de los elementos de la lista</remarks>
        public static LineaPedidos GetList() { return GetList(false); }
        public static LineaPedidos GetList(bool retrieve_childs)
        {
            if (!LineaPedido.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = LineaPedido.GetCriteria(LineaPedido.OpenSession());
			criteria.Childs = retrieve_childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = LineaPedidos.SELECT();
            
            LineaPedido.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<LineaPedidos>(criteria);
        }

        #endregion
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private LineaPedidos(IList<LineaPedido> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
        private LineaPedidos(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
			SessionCode = sessionCode;
			Childs = childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static LineaPedidos NewChildList() 
        { 
            LineaPedidos list = new LineaPedidos(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static LineaPedidos GetChildList(IList<LineaPedido> lista) { return new LineaPedidos(lista); }
        public static LineaPedidos GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static LineaPedidos GetChildList(int sessionCode, IDataReader reader, bool childs) { return new LineaPedidos(sessionCode, reader, childs); }
		
		public static LineaPedidos GetPendientesChildList(Pedido parent, bool childs)
		{
			CriteriaEx criteria = LineaPedido.GetCriteria(parent.SessionCode);
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				Pedido = parent.GetInfo()
			};
			criteria.Query = SELECT_PENDIENTES(conditions);

			return DataPortal.Fetch<LineaPedidos>(criteria);
		}

		#endregion
		
		#region Root Data Access

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        /// <remarks>LA UTILIZA EL DATAPORTAL</remarks>
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        private void Fetch(CriteriaEx criteria)
        {
            try
            {
				this.RaiseListChangedEvents = false;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
                {
                    LineaPedido.DoLOCK( Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(LineaPedido.GetChild(SessionCode, reader, Childs));
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

        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (LineaPedido obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (LineaPedido obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
							obj.Insert(this);
						else
							obj.Update(this);
					}
				}

                Transaction().Commit();
            }
            catch (Exception ex)
            {
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion
		
		#region Child Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
        private void Fetch(IList<LineaPedido> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (LineaPedido item in lista)
				this.AddItem(LineaPedido.GetChild(item, Childs));

			this.RaiseListChangedEvents = true;
		}
		
        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen con los elementos a insertar</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(LineaPedido.GetChild(SessionCode, reader, Childs));

            this.RaiseListChangedEvents = true;
        }
		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(Pedido parent)
		{
			try
			{
				this.RaiseListChangedEvents = false;
				
				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (LineaPedido obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (LineaPedido obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
							obj.Insert(parent);
						else
							obj.Update(parent);
					}
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
		}
		
		#endregion

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return LineaPedido.SELECT(conditions, true); }
		public static string SELECT(Pedido pedido) { return SELECT(new QueryConditions { Pedido = pedido.GetInfo(false) }); }
		public static string SELECT_PENDIENTES(QueryConditions conditions) { return LineaPedido.SELECT_PENDIENTES(conditions, true); }

		#endregion
    }
}

