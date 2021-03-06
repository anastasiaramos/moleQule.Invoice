using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// Editable Business Object Root Collection
	/// </summary>
    [Serializable()]
    public class Transactiones : BusinessListBaseEx<Transactiones, Transaction>
    {
		#region Business Methods

		public void SetMaxSerial()
		{
			foreach (Transaction item in this)
				if (item.Serial > _max_serial) MaxSerial = item.Serial;
		}
		
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public Transaction NewItem()
        {
            this.AddItem(Invoice.Transaction.NewChild());
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
        private Transactiones() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static Transactiones NewList() 
		{
			if (!Invoice.Transaction.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new Transactiones(); 
		}

        public static Transactiones GetList() { return GetList(true); }		
		public static Transactiones GetList(bool childs) 
		{
			return GetList(Transactiones.SELECT(), childs);
		}
		public static Transactiones GetList(QueryConditions conditions, bool childs)
		{
			return GetList(Transactiones.SELECT(conditions), childs);
		}
		
		private static Transactiones GetList(string query, bool childs)
		{
			if (!Invoice.Transaction.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Invoice.Transaction.GetCriteria(Invoice.Transaction.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

			Invoice.Transaction.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<Transactiones>(criteria);
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
					Invoice.Transaction.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
						this.AddItem(Invoice.Transaction.GetChild(SessionCode, reader, Childs));
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
            foreach (Transaction obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (Transaction obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
							obj.Insert(this);
						else
							obj.Update(this);
					}
				}

                if (!SaveAsChildList) Transaction().Commit();
            }
            catch (Exception ex)
            {
                if (!SaveAsChildList) if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                if (!SaveAsChildList) BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion		
			
        #region SQL

		public static string SELECT() { return Invoice.Transaction.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Invoice.Transaction.SELECT(conditions, true); }
			
		#endregion
    }
}

