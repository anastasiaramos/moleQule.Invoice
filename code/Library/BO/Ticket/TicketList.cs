using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class TicketList : ReadOnlyListBaseEx<TicketList, TicketInfo>
    {
        #region Business Methods

		public decimal Total()
		{
			decimal total = 0;
			foreach (TicketInfo item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;
				total += item.Total;
			}

			return total;
		}

        #endregion

        #region Common Factory Methods

        private TicketList() { }

        private TicketList(IDataReader reader)
        {
            Fetch(reader);
        }

        private TicketList(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>TicketList</returns>
        public static TicketList GetChildList(bool childs)
        {
            CriteriaEx criteria = Ticket.GetCriteria(Ticket.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = TicketList.SELECT();

            TicketList list = DataPortal.Fetch<TicketList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Root Factory Methods

		public static TicketList NewList() { return new TicketList(); }

		public static TicketList GetList() { return GetList(true); }
        public static TicketList GetList(bool childs) 
		{ 
			return GetList(DateTime.MinValue, DateTime.MaxValue, childs); 
		}
		public static TicketList GetList(EEstado estado, bool childs)
		{
			return GetList(DateTime.MinValue, DateTime.MaxValue, estado, childs);
		}
		public static TicketList GetList(int year, bool childs)
		{
			return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static TicketList GetList(int year, EEstado estado, bool childs)
		{
			return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), estado, childs);
		}
		public static TicketList GetList(DateTime f_ini, DateTime f_fin, bool childs)
		{
			return GetList(f_ini, f_fin, EEstado.Todos, childs);
		}
		public static TicketList GetList(DateTime f_ini, DateTime f_fin, EEstado estado, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoFactura = ETipoFactura.Todas,
				FechaIni = f_ini,
				FechaFin = f_fin,
				Estado = estado,
			};

			return GetList(conditions, childs);
		}
		public static TicketList GetList(string list) { return GetList(true); }

		public static TicketList GetList(QueryConditions conditions, bool childs)
		{
			CriteriaEx criteria = Ticket.GetCriteria(Ticket.OpenSession());
			criteria.Childs = childs;

			criteria.Query = TicketList.SELECT(conditions);

			TicketList list = DataPortal.Fetch<TicketList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}

		public static TicketList GetBySerieAndYearList(long oid_serie, int year, ETipoFactura tipo)
		{
			CriteriaEx criteria = Ticket.GetCriteria(Ticket.OpenSession());
			criteria.Childs = false;

			QueryConditions conditions = new QueryConditions
			{
				Serie = Serie.New().GetInfo(false),
				TipoFactura = tipo,
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year),
			};
			conditions.Serie.Oid = oid_serie;

			return GetList(TicketList.SELECT(conditions), false);
		}

        private static TicketList GetList(string query, bool childs)
        {
            CriteriaEx criteria = Ticket.GetCriteria(Ticket.OpenSession());
            criteria.Childs = childs;

            criteria.Query = query;
            TicketList list = DataPortal.Fetch<TicketList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static SortedBindingList<TicketInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<TicketInfo> sortedList = new SortedBindingList<TicketInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<TicketInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<TicketInfo> sortedList = new SortedBindingList<TicketInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        public static TicketList GetList(IList<Ticket> list)
        {
            TicketList flist = new TicketList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (Ticket item in list)
                    flist.AddItem(item.GetInfo());

                flist.IsReadOnly = true;
            }

            return flist;
        }
		public static TicketList GetList(IList<TicketInfo> list)
		{
			TicketList flist = new TicketList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (TicketInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}

        #endregion

        #region Child Factory Methods

        public static TicketList GetChildList() { return TicketList.GetChildList(true); }
        public static TicketList GetChildList(IList<TicketInfo> list)
        {
            TicketList flist = new TicketList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (TicketInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static TicketList GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static TicketList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new TicketList(sessionCode, reader, childs); }

        #endregion

        #region Data Access

        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;
#if TRACE
					ControlerBase.AppControler.Timer.Record("TicketList::Fetch - SELECT");
#endif
                    while (reader.Read())
                        AddItem(TicketInfo.GetChild(SessionCode, reader, Childs));
 #if TRACE
					ControlerBase.AppControler.Timer.Record("TicketList::Fetch - Generacion de objetos");
#endif
                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        // called to retrieve data from db
        protected override void Fetch(string hql)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IList list = nHMng.HQLSelect(hql);

                if (list.Count > 0)
                {
                    IsReadOnly = false;

                    foreach (Ticket item in list)
                        this.AddItem(item.GetInfo(false));

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        // called to retrieve data from db
        protected void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IsReadOnly = false;

                while (reader.Read())
                {
                    AddItem(TicketInfo.GetChild(SessionCode, reader, Childs));
                }

                IsReadOnly = true;

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
        public static string SELECT(QueryConditions conditions) { return Ticket.SELECT(conditions, false); }

		#endregion
    }
}



