using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class ConceptoTicketList : ReadOnlyListBaseEx<ConceptoTicketList, ConceptoTicketInfo>
    {
        #region Business Methods

        public ConceptoTicketInfo GetItemByOidPartida(long oid_pexp)
        {
            foreach (ConceptoTicketInfo item in this)
                if (item.OidPartida == oid_pexp)
                    return item;

            return null;
        }

        #endregion

        #region Factory Methods

        private ConceptoTicketList() { }

        private ConceptoTicketList(int sessionCode, IDataReader reader)
        {
			SessionCode = sessionCode;
            Fetch(reader);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>ConceptoTicketList</returns>
        public static ConceptoTicketList GetChildList(bool childs)
        {
            CriteriaEx criteria = ConceptoTicket.GetCriteria(ConceptoTicket.OpenSession());
            criteria.Childs = childs;

            //No criteria. Retrieve all de List
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            ConceptoTicketList list = DataPortal.Fetch<ConceptoTicketList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
		public static ConceptoTicketList GetChildList(int sessionCode, IDataReader reader) { return new ConceptoTicketList(sessionCode, reader); }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns></returns>
        public static ConceptoTicketList GetList(bool childs)
        {
            CriteriaEx criteria = ConceptoTicket.GetCriteria(ConceptoTicket.OpenSession());
            criteria.Childs = childs;

            //No criteria. Retrieve all de List
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = ConceptoTicketList.SELECT();

            ConceptoTicketList list = DataPortal.Fetch<ConceptoTicketList>(criteria);
            
            CloseSession(criteria.SessionCode);

            return list;
        }

        /// <summary>
        /// Default call for GetList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static ConceptoTicketList GetList()
        {
            return ConceptoTicketList.GetList(true);
        }

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static ConceptoTicketList GetList(CriteriaEx criteria)
        {
            return ConceptoTicketList.RetrieveList(typeof(ConceptoTicket), AppContext.ActiveSchema.Code, criteria);
        }

        /// <summary>
        /// Builds a ConceptoTicketList from a IList<!--<ConceptoTicketInfo>-->.
        /// Doesnt retrieve child data from DB.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static ConceptoTicketList GetList(IList<ConceptoTicketInfo> list)
        {
            ConceptoTicketList flist = new ConceptoTicketList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ConceptoTicketInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<ConceptoTicketInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<ConceptoTicketInfo> sortedList = new SortedBindingList<ConceptoTicketInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos y sus hijos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <param name="childs">Traer hijos</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<ConceptoTicketInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<ConceptoTicketInfo> sortedList = new SortedBindingList<ConceptoTicketInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        /// <summary>
        /// Builds a ConceptoTicketList from a IList<!--<ConceptoTicket>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>ConceptoTicketList</returns>
        public static ConceptoTicketList GetList(IList<ConceptoTicket> list)
        {
            ConceptoTicketList flist = new ConceptoTicketList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (ConceptoTicket item in list)
                    flist.AddItem(item.GetInfo());

                flist.IsReadOnly = true;
            }

            return flist;
        }

        public static long GetCountByExpediente(long oid_exp)
        {
            CriteriaEx criteria = ConceptoTicket.GetCriteria(ConceptoTicket.OpenSession());
            criteria.Childs = false;

            criteria.Query = ConceptoTicket.COUNT_BY_EXPEDIENTE(oid_exp);
            IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, criteria.Session);
            reader.Read();
            long cuenta = Convert.ToInt64(reader["CUENTA"]);

            CloseSession(criteria.SessionCode);
            return cuenta;
        }

        public static ConceptoTicketList GetListByExpediente(long oid, bool childs)
        {
            CriteriaEx criteria = ConceptoTicket.GetCriteria(ConceptoTicket.OpenSession());
            criteria.Childs = childs;

            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { Expediente = ExpedientInfo.New(oid) };
            criteria.Query = ConceptoTicketList.SELECT(conditions);
            
            ConceptoTicketList list = DataPortal.Fetch<ConceptoTicketList>(criteria);
            CloseSession(criteria.SessionCode);

            return list;
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Default call for GetChildList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static ConceptoTicketList GetChildList()
        {
            return ConceptoTicketList.GetChildList(true);
        }

        public static ConceptoTicketList GetChildList(IList<ConceptoTicketInfo> list)
        {
            ConceptoTicketList flist = new ConceptoTicketList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ConceptoTicketInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        public static ConceptoTicketList GetChildList(IList<ConceptoTicket> list)
        {
            ConceptoTicketList flist = new ConceptoTicketList();

            if (list != null)
            {
                //int sessionCode = ConceptoTicket.OpenSession();
                //CriteriaEx criteria = null;

                flist.IsReadOnly = false;

                foreach (ConceptoTicket item in list)
                {
                    /*criteria = Partida.GetCriteria(sessionCode);
                    criteria.AddEq("OidExpediente", item.OidExpediente);
                    criteria.AddEq("OidProducto", item.OidProducto);
                    item.Partidas = Partidas.GetChildList(criteria.List<Partida>());*/

                    flist.AddItem(item.GetInfo());
                }

                flist.IsReadOnly = true;

                //ConceptoTicket.CloseSession(sessionCode);
            }

            return flist;
        }

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

                    while (reader.Read())
                    {
                        this.AddItem(ConceptoTicketInfo.Get(SessionCode, reader, Childs));
                    }

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

                    foreach (ConceptoTicket item in list)
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
                    this.AddItem(ConceptoTicketInfo.Get(SessionCode, reader, Childs));
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

        public static string SELECT() { return ConceptoTicket.SELECT(0, false); }
        public static string SELECT(Library.Invoice.QueryConditions conditions) { return ConceptoTicket.SELECT(conditions, false); }
        public static string SELECT(TicketInfo item) { return SELECT(new Library.Invoice.QueryConditions { Ticket = item }); }
        public static string SELECT(ExpedientInfo item) { return SELECT(new Library.Invoice.QueryConditions { Expediente = item }); }

        #endregion
    }
}



