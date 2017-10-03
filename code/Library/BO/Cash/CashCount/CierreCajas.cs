using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using NHibernate;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Resources;
using moleQule.Library.Invoice.Resources;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// Editable Business Object With Childs Root Collection  
	/// Editable Business Object With Editable Child Collection
	/// </summary>
    [Serializable()]
    public class CierreCajas : BusinessListBaseEx<CierreCajas, CierreCaja>
    {		
		#region Root Business Methods
        
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public CierreCaja NewItem()
        {
            this.AddItem(CierreCaja.NewChild());
            return this[Count - 1];
        }

        #endregion
		
		#region Child Business Methods
		
		/// <summary>
		/// Crea un nuevo elemento y lo añade a la lista
		/// </summary>
		/// <returns>Nuevo item</returns>
		public CierreCaja NewItem(Cash parent)
		{
			this.NewItem(CierreCaja.NewChild(parent));
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
        private CierreCajas() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static CierreCajas NewList() 
		{ 	
			if (!CierreCaja.CanAddObject())
                throw new System.Security.SecurityException(
                    Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new CierreCajas(); 
		}

        /// <summary>
        /// Obtiene de la base de datos todos los elementos y construye la lista
        /// </summary>
        /// <returns>Lista de los elementos de la tabla en la base de datos</returns>
        /// <remarks>No obtiene los hijos de los elementos de la lista</remarks>
        public static CierreCajas GetList() { return GetList(false); }

        /// <summary>
        /// Obtiene de la base de datos todos los elementos y construye la lista
        /// </summary>
		/// <param name="childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista de los elementos de la tabla en la base de datos</returns>
        public static CierreCajas GetList(bool childs)
        {
            if (!CierreCaja.CanEditObject())
				throw new System.Security.SecurityException(
                    Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = CierreCaja.GetCriteria(CierreCaja.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = CierreCajas.SELECT();
            
            CierreCaja.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<CierreCajas>(criteria);
        }

        #endregion
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private CierreCajas(IList<CierreCaja> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <param name="childs">Flag para indicar si quiere obtener los hijos</param>
        private CierreCajas(IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static CierreCajas NewChildList() 
        { 
            CierreCajas list = new CierreCajas(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static CierreCajas GetChildList(IList<CierreCaja> lista) { return new CierreCajas(lista); }

        /// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        /// <returns>Lista creada</returns>
        /// <remarks>Obtiene los hijos</remarks>
        public static CierreCajas  GetChildList(IDataReader reader) { return GetChildList(reader, true); }

        /// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        /// <param name="childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista creada</returns>
        /// <remarks>Obtiene los hijos</remarks>
        public static CierreCajas GetChildList(IDataReader reader, bool childs) { return new CierreCajas(reader, childs); }

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
                    CierreCaja.DoLOCK(AppContext.ActiveSchema.Code, Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(CierreCaja.GetChild(SessionCode, reader, Childs));
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
            foreach (CierreCaja obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (CierreCaja obj in this)
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
        private void Fetch(IList<CierreCaja> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (CierreCaja item in lista)
				this.AddItem(CierreCaja.GetChild(item, Childs));

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
                this.AddItem(CierreCaja.GetChild(SessionCode, reader, Childs));

            this.RaiseListChangedEvents = true;
        }

		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(Cash parent)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (CierreCaja obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (CierreCaja obj in this)
			{	
				if (!this.Contains(obj))
				{
					if (obj.IsNew)
						obj.Insert(parent);
					else
						obj.Update(parent);
				}
			}

			this.RaiseListChangedEvents = true;
		}
		
		#endregion
        
        #region SQL

        public static string SELECT() { return CierreCaja.SELECT(new QueryConditions(), true); }
		
		#endregion
    }
}

