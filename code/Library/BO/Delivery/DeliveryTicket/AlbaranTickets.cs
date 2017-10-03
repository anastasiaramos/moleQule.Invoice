using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

using NHibernate;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class AlbaranTickets : BusinessListBaseEx<AlbaranTickets, AlbaranTicket>
    {
        #region Business Methods

        public AlbaranTicket NewItem(Ticket factura, OutputDeliveryInfo albaran)
        {
            this.AddItem(AlbaranTicket.NewChild(factura, albaran));
			factura.SetAlbaranes();
            return this[Count - 1];
        }

        public bool AlbaranExists(long oid)
        {
            foreach (AlbaranTicket obj in this)
                if (obj.OidAlbaran == oid)
                    return true;
            return false;
        }

        public AlbaranTicket GetItemByTicket(long oid_factura)
        {
            foreach (AlbaranTicket obj in this)
            {
                if (obj.OidTicket == oid_factura)
                    return obj;
            }
            return null;
        }

        public bool GetItem(long oid_factura, long oid_albaran)
        {
            foreach (AlbaranTicket obj in this)
            {
                if (obj.OidTicket == oid_factura && obj.OidAlbaran == oid_albaran)
                    return true;
            }
            return false;
        }

        public AlbaranTicket GetItemByAlbaran(long oid_Albaran)
        {
            foreach (AlbaranTicket obj in this)
            {
                if (obj.OidAlbaran == oid_Albaran)
                    return obj;
            }
            return null;
        }

        public void Remove(Ticket factura, OutputDeliveryInfo albaran)
        {
            foreach (AlbaranTicket item in this)
                if (item.OidTicket == factura.Oid && item.OidAlbaran == albaran.Oid)
                {
                    this.Remove(item.Oid);
					factura.SetAlbaranes();
                    break;
                }
        }

        #endregion

        #region Factory Methods

        private AlbaranTickets()
        {
            MarkAsChild();
        }
        private AlbaranTickets(IList<AlbaranTicket> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
		private AlbaranTickets(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        public static AlbaranTickets NewChildList() { return new AlbaranTickets(); }

        public static AlbaranTickets GetChildList(IList<AlbaranTicket> lista) { return new AlbaranTickets(lista); }

		public static AlbaranTickets GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static AlbaranTickets GetChildList(int sessionCode, IDataReader reader, bool childs) { return new AlbaranTickets(sessionCode, reader, childs); }

		public static AlbaranTickets GetChildList(Ticket parent, bool childs)
		{
			CriteriaEx criteria = AlbaranTicket.GetCriteria(parent.SessionCode);

			criteria.Query = AlbaranTickets.SELECT(parent);
			criteria.Childs = childs;

			return DataPortal.Fetch<AlbaranTickets>(criteria);
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
					AlbaranTicket.DoLOCK(Session());
					IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
					{
						AlbaranTicket obj = AlbaranTicket.GetChild(SessionCode, reader);
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
        private void Fetch(IList<AlbaranTicket> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (AlbaranTicket item in lista)
                this.AddItem(AlbaranTicket.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(AlbaranTicket.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }

        internal void Update(OutputDelivery parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (AlbaranTicket obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (AlbaranTicket obj in this)
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

        internal void Update(Ticket parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (AlbaranTicket obj in DeletedList)
				{
					if (!GetItem(obj.OidTicket, obj.OidAlbaran))
						obj.DeleteSelf(parent);
				}

				// add/update any current child objects
				foreach (AlbaranTicket obj in this)
				{
					bool existe = false;

					if (obj.IsNew)
					{
						//Si el albarán se ha eliminado y se ha vuelto a insertar no hay que volver a guardarlo
						foreach (AlbaranTicket albaran in DeletedList)
						{
							if (albaran.OidAlbaran == obj.OidAlbaran)
							{
								existe = true;
								break;
							}
						}
						if (existe)
							continue;
						obj.Insert(parent);
					}
					else
						obj.Update(parent);

				}

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
        }

        #endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return AlbaranTicket.SELECT(conditions, true); }
		public static string SELECT(OutputDelivery source) { return SELECT(new QueryConditions { OutputDelivery = source.GetInfo(false) }); }
		public static string SELECT(Ticket source) { return SELECT(new QueryConditions { Ticket = source.GetInfo(false) }); }

        #endregion
    }
}
