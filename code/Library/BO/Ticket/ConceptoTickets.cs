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
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class ConceptoTickets : BusinessListBaseEx<ConceptoTickets, ConceptoTicket>
    {
        #region Business Methods
	
        public ConceptoTicket NewItem(Ticket parent)
        {
            this.NewItem(ConceptoTicket.NewChild(parent));
            return this[Count - 1];
        }

        public bool ContainsPartida(long oid_producto_expediente)
        {
            foreach (ConceptoTicket obj in this)
                if (obj.OidPartida == oid_producto_expediente)
                    return true;

            return false;
        }

        public void Remove(OutputDeliveryLineInfo calbaran)
        {
            foreach (ConceptoTicket item in this)
                if (item.OidConceptoAlbaran == calbaran.Oid)
                {
                    this.Remove(item.Oid);
                    break;
                }
        }

        public ConceptoTicket GetItemByOidPartida(long oid_pexp)
        {
            foreach (ConceptoTicket item in this)
                if (item.OidPartida == oid_pexp)
                    return item;

            return null;
        }

        #endregion

        #region Factory Methods

        private ConceptoTickets()
        {
            MarkAsChild();
        }

        private ConceptoTickets(IList<ConceptoTicket> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }

        private ConceptoTickets(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        public static ConceptoTickets NewChildList() { return new ConceptoTickets(); }

        public static ConceptoTickets GetChildList(IList<ConceptoTicket> lista) { return new ConceptoTickets(lista); }

		public static ConceptoTickets GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static ConceptoTickets GetChildList(int sessionCode, IDataReader reader, bool childs) { return new ConceptoTickets(sessionCode, reader, childs); }
		
        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<ConceptoTicket> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (ConceptoTicket item in lista)
                this.AddItem(ConceptoTicket.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(ConceptoTicket.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }
		
        internal void Update(Ticket parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (ConceptoTicket obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (ConceptoTicket obj in this)
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

        public static string SELECT(Library.Invoice.QueryConditions conditions) { return ConceptoTicket.SELECT(conditions, true); }
        public static string SELECT(Ticket item) { return SELECT(new Library.Invoice.QueryConditions { Ticket = item.GetInfo() }); }

        #endregion
    }
}
