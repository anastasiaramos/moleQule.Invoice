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
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class AlbaranTicketList : ReadOnlyListBaseEx<AlbaranTicketList, AlbaranTicketInfo>
    {
        #region Business Methods

        public AlbaranTicketInfo GetItemByTicket(long oid_ticket)
        {
            foreach (AlbaranTicketInfo obj in this)
            {
                if (obj.OidTicket == oid_ticket)
                    return obj;
            }
            return null;
        }

        public AlbaranTicketInfo GetItemByAlbaran(long oid_albaran)
        {
            foreach (AlbaranTicketInfo obj in this)
            {
                if (obj.OidAlbaran == oid_albaran)
                    return obj;
            }
            return null;
        }
        
        public AlbaranTicketInfo GetItem(long oid_albaran, long oid_ticket)
        {
            foreach (AlbaranTicketInfo obj in this)
            {
                if (obj.OidAlbaran == oid_albaran && obj.OidTicket == oid_ticket)
                    return obj;
            }
            return null;
        }

        #endregion

		#region Child Factory Methods

		private AlbaranTicketList(IList<AlbaranTicket> lista)
		{
			Fetch(lista);
		}

		public static AlbaranTicketList GetChildList(IList<AlbaranTicketInfo> list)
		{
			AlbaranTicketList flist = new AlbaranTicketList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (AlbaranTicketInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}
		public static AlbaranTicketList GetChildList(IList<AlbaranTicket> list) { return new AlbaranTicketList(list); }
		public static AlbaranTicketList GetChildList(int sessionCode, IDataReader reader) { return new AlbaranTicketList(sessionCode, reader); }

		#endregion 
		
		#region Root Factory Methods

		private AlbaranTicketList() {}		
        private AlbaranTicketList(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			Fetch(reader);
		}

		public static AlbaranTicketList GetList() { return AlbaranTicketList.GetList(true); }
		public static AlbaranTicketList GetList(bool childs)
		{
			CriteriaEx criteria = AlbaranTicket.GetCriteria(AlbaranTicket.OpenSession());
            criteria.Childs = childs;
			
			criteria.Query = AlbaranTicketList.SELECT();

			AlbaranTicketList list = DataPortal.Fetch<AlbaranTicketList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

		public static AlbaranTicketList GetList(List<long> oid_list, bool childs)
		{
			CriteriaEx criteria = AlbaranTicket.GetCriteria(AlbaranTicket.OpenSession());
			criteria.Childs = childs;

			criteria.Query = AlbaranTicket.SELECT_BY_LIST(oid_list, false);

			AlbaranTicketList list = DataPortal.Fetch<AlbaranTicketList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		public List<AlbaranTicketInfo> GetListByAlbaran(AlbaranFacturas afs)
		{
			List<AlbaranTicketInfo> list = new List<AlbaranTicketInfo>();

			foreach (AlbaranTicketInfo item in this)
			{
				if (afs.GetItemByAlbaran(item.OidAlbaran) != null)
					list.Add(item);
			}

			return list;
		}

		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<AlbaranTicket> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (AlbaranTicket item in lista)
                this.AddItem(item.GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(AlbaranTicketInfo.GetChild(SessionCode, reader));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
		// called to retrieve data from db
		protected override void Fetch(CriteriaEx criteria)
		{
			this.RaiseListChangedEvents = false;

			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
			{
				if (nHMng.UseDirectSQL)
				{
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					IsReadOnly = false;

					while (reader.Read())
					{
						this.AddItem(AlbaranTicketInfo.GetChild(SessionCode, reader,Childs));
					}

					IsReadOnly = true;
				}
				else
				{
					IList list = criteria.List();

					if (list.Count > 0)
					{
						IsReadOnly = false;

						foreach (AlbaranTicket item in list)
							this.AddItem(item.GetInfo());

						IsReadOnly = true;
					}
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return AlbaranTicket.SELECT(conditions, false); }
		public static string SELECT(OutputDeliveryInfo source) { return SELECT(new QueryConditions { OutputDelivery = source }); }
		public static string SELECT(TicketInfo source) { return SELECT(new QueryConditions { Ticket = source }); }

        #endregion
	
	}
}

