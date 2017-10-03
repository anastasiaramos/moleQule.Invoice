using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Tickets : BusinessListBaseEx<Tickets, Ticket>, IEntidadRegistroList
    {
		#region IEntidadRegistro

		public IEntidadRegistro IGetItem(long oid) { return (IEntidadRegistro)GetItem(oid); }		
		
		public IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }

		public void Update(Registro parent)
		{
			this.RaiseListChangedEvents = false;

			// add/update any current child objects
			foreach (Ticket obj in this)
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
        public Ticket NewItem()
        {
            this.AddItem(Ticket.NewChild());
            return this[Count - 1];
        }

        #endregion

		#region Child Factory Methods

		public static Tickets GetChildList(int sessionCode, List<long> oid_list, bool childs)
		{
			return GetChildList(sessionCode, Tickets.SELECT(new QueryConditions { OidList = oid_list }), childs);
		}
		internal static Tickets GetChildList(int sessionCode, string query, bool childs)
		{
			if (!Ticket.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Ticket.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			return DataPortal.Fetch<Tickets>(criteria);
		}

		#endregion

		#region Factory Methods

		private Tickets() { }

        public static Tickets NewList() { return new Tickets(); }

		public static Tickets GetList() { return GetList(true); }
		public static Tickets GetList(bool childs)
		{
			return GetList(Tickets.SELECT(), childs);
		}
		public static Tickets GetList(List<long> oid_list, bool childs)
		{
			return GetList(Tickets.SELECT(new QueryConditions { OidList = oid_list }), childs);
		}
		public static Tickets GetList(Library.Invoice.QueryConditions conditions, bool childs)
		{
			return GetList(Tickets.SELECT(conditions), childs);
		}
		
		internal static Tickets GetList(string query, bool childs)
		{
			if (!Ticket.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Ticket.GetCriteria(Ticket.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			Ticket.BeginTransaction(criteria.SessionCode);

			return DataPortal.Fetch<Tickets>(criteria);
		}

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    Ticket.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Ticket.GetChild(SessionCode, reader, _childs));
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

        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Ticket obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (Ticket obj in this)
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

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(Library.Invoice.QueryConditions conditions) { return Ticket.SELECT(conditions, true); }

		#endregion

    }
}
