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
	/// Editable Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class Clientes : BusinessListBaseEx<Clientes, Cliente>, IEntidadRegistroList
    {
		#region IEntidadRegistro

		public IEntidadRegistro IGetItem(long oid) { return (IEntidadRegistro)GetItem(oid); }

		public IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }

		public void Update(Registro parent)
		{
			this.RaiseListChangedEvents = false;

			// add/update any current child objects
			foreach (Cliente obj in this)
			{
				obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

		#region Business Methods
        
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public Cliente NewItem()
        {
            this.AddItem(Cliente.NewChild());
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
        private Clientes() { }

		#endregion		
		
		#region Child Factory Methods

		private Clientes(IList<Cliente> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private Clientes(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

		public static Clientes NewChildList() { return new Clientes(); }

		public static Clientes GetChildList(IList<Cliente> lista) 
		{
			Clientes list = new Clientes(lista);
			list.MarkAsChild();
			return list;
		}
		public static Clientes GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static Clientes GetChildList(int sessionCode, IDataReader reader, bool childs) { return new Clientes(sessionCode, reader, childs); }

		public static Clientes GetChildList(int sessionCode, List<long> oid_list, bool childs)
		{
			return GetChildList(sessionCode, Cliente.SELECT(Cliente.EQueryType.GENERAL, new QueryConditions { OidList = oid_list }, false), childs);
		}
		internal static Clientes GetChildList(int sessionCode, string query, bool childs)
		{
			if (!Cliente.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Cliente.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			return DataPortal.Fetch<Clientes>(criteria);
		}

		#endregion

		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static Clientes NewList() 
		{ 	
			if (!Cliente.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new Clientes(); 
		}

		public static Clientes GetList(bool childs= true) { return GetList(Clientes.SELECT(), childs); }
		public static Clientes GetList(QueryConditions conditions, bool childs)	 { return GetList(Clientes.SELECT(conditions), childs); }

		internal static Clientes GetList(string query, bool childs)
		{
            if (!Cliente.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;
				
			Cliente.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<Clientes>(criteria);
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
                    Cliente.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Cliente.GetChild(SessionCode, reader, Childs));

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Cliente.SELECT_COUNT(criteria), criteria.Session);
						if (reader.Read()) criteria.PagingInfo.TotalItems = Format.DataReader.GetInt32(reader, "TOTAL_ROWS");
					}
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
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
            foreach (Cliente obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (Cliente obj in this)
				{
					if (obj.IsNew)
						obj.Insert(this);
					else
						obj.Update(this);
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

		// called to copy objects data from list and to retrieve child data from db
		private void Fetch(IList<Cliente> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (Cliente item in lista)
				this.AddItem(Cliente.GetChild(item));

			this.RaiseListChangedEvents = true;
		}

		// called to copy objects data from list and to retrieve child data from db
		private void Fetch(IDataReader reader)
		{
			this.RaiseListChangedEvents = false;

			while (reader.Read())
				this.AddItem(Cliente.GetChild(SessionCode, reader, Childs));

			this.RaiseListChangedEvents = true;
		}

		#endregion
			
        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Cliente.SELECT(Cliente.EQueryType.GENERAL, conditions, true); }
		
		#endregion
    }
}

