using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// Editable Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
    public class OutputDeliveries : BusinessListBaseEx<OutputDeliveries, OutputDelivery>
    {		
		#region Root Business Methods
        
        public OutputDelivery NewItem()
        {
            this.AddItem(OutputDelivery.NewChild());
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
        private OutputDeliveries() { }

		#endregion		
		
		#region Root Factory Methods
		
        public static OutputDeliveries NewList() 
		{ 	
			if (!OutputDelivery.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			OutputDeliveries list = new OutputDeliveries();
            list.SessionCode = OutputDelivery.OpenSession();
            list.BeginTransaction();

            return list;
		}

		public static OutputDeliveries GetList(string query, bool childs)
		{
			if (!OutputDelivery.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return GetList(query, childs, -1);
		}

		public static OutputDeliveries GetList(bool childs = false)
		{
			return GetList(SELECT(), childs);
		}
		public static OutputDeliveries GetList(HashOidList oidList, bool childs, int sessionCode)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoEntidad = Common.ETipoEntidad.Cliente,
				OidList = oidList.ToList()
			};

			return GetList(OutputDelivery.SELECT(conditions, false), childs, sessionCode);
		}
		public static OutputDeliveries GetList(HashOidList oidList, bool childs, bool cache, int sessionCode)
		{
			OutputDeliveries list;

			if (!Cache.Instance.Contains(typeof(OutputDeliveries)))
			{
				list = OutputDeliveries.GetList(oidList, childs, sessionCode);
				Cache.Instance.Save(typeof(OutputDeliveries), list);
			}
			else
				list = Cache.Instance.Get(typeof(OutputDeliveries)) as OutputDeliveries;

			return list;
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
                    OutputDelivery.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(OutputDelivery.GetChild(SessionCode, reader, Childs));
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

        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (OutputDelivery obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (OutputDelivery obj in this)
				{
					if (obj.IsNew)
						obj.Insert(this);
					else
						obj.Update(this);
				}

				if (!SharedTransaction) Transaction().Commit();
            }
            catch (Exception ex)
            {
				if (!SharedTransaction) if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
				if (!SharedTransaction) BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return OutputDelivery.SELECT(conditions, true); }

		#endregion
    }
}

