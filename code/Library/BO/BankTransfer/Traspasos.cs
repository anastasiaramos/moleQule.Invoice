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
	/// Editable Business Object Root Collection
	/// </summary>
    [Serializable()]
    public class Traspasos : BusinessListBaseEx<Traspasos, Traspaso>, IEntidadRegistroList
    {
		#region IEntidadRegistro

		public IEntidadRegistro IGetItem(long oid) { return (IEntidadRegistro)GetItem(oid); }		
		
		public IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }

		public void Update(Registro parent)
		{
			this.RaiseListChangedEvents = false;

			// add/update any current child objects
			foreach (Traspaso obj in this)
			{
				obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}		

		#endregion
		
		#region Root Business Methods
        
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public Traspaso NewItem()
        {
            this.AddItem(Traspaso.NewChild());
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
        private Traspasos() { }

		public static Traspasos GetChildList(int sessionCode, List<long> oid_list, bool childs)
		{
			return GetChildList(sessionCode, Traspasos.SELECT(new QueryConditions { OidList = oid_list }), childs);
		}
		internal static Traspasos GetChildList(int sessionCode, string query, bool childs)
		{
			if (!Charge.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Traspaso.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			return DataPortal.Fetch<Traspasos>(criteria);
		}

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static Traspasos NewList() 
		{ 	
			if (!Traspaso.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new Traspasos(); 
		}

        public static Traspasos GetList() { return GetList(true); }		
		public static Traspasos GetList(bool childs) 
		{
			return GetList(childs, Traspasos.SELECT());
		}
		public static Traspasos GetList(QueryConditions conditions, bool childs)
		{
			return GetList(childs, Traspasos.SELECT(conditions));
		}

		internal static Traspasos GetList(bool childs, string query)
		{
            if (!Traspaso.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Traspaso.GetCriteria(Traspaso.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;
				
			Traspaso.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<Traspasos>(criteria);
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
                    Traspaso.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Traspaso.GetChild(SessionCode, reader, Childs));
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
            foreach (Traspaso obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (Traspaso obj in this)
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
		
			
        #region SQL

        public static string SELECT() { return Traspaso.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Traspaso.SELECT(conditions, true); }
		public static string SELECT(Traspaso parent) { return Traspaso.SELECT(new QueryConditions{ Traspaso = parent.GetInfo(false) }, true); }
				
		#endregion
    }
}

