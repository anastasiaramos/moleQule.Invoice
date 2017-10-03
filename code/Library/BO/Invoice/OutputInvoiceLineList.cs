using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class OutputInvoiceLineList : ReadOnlyListBaseEx<OutputInvoiceLineList, OutputInvoiceLineInfo>
    {
        #region Business Methods

        public OutputInvoiceLineInfo GetItemByOidPartida(long oid_pexp)
        {
            foreach (OutputInvoiceLineInfo item in this)
                if (item.OidPartida == oid_pexp)
                    return item;

            return null;
        }

        #endregion

        #region Factory Methods

        public static OutputInvoiceLineList NewList() { return new OutputInvoiceLineList(); }

        private OutputInvoiceLineList() { }

        private OutputInvoiceLineList(int sessionCode, IDataReader reader)
        {
			SessionCode = sessionCode;
            Fetch(reader);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>ConceptoFacturaList</returns>
        public static OutputInvoiceLineList GetChildList(bool childs)
        {
            CriteriaEx criteria = OutputInvoiceLine.GetCriteria(OutputInvoiceLine.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

             OutputInvoiceLineList list = DataPortal.Fetch<OutputInvoiceLineList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
		public static OutputInvoiceLineList GetChildList(int sessionCode, IDataReader reader) { return new OutputInvoiceLineList(sessionCode, reader); }
        public static OutputInvoiceLineList GetChildList(OutputInvoiceInfo parent, bool childs)
        {
            CriteriaEx criteria = OutputInvoiceLine.GetCriteria(OutputInvoiceLine.OpenSession());

            criteria.Query = SELECT(parent);
            criteria.Childs = childs;

            OutputInvoiceLineList list = DataPortal.Fetch<OutputInvoiceLineList>(criteria);
            list.CloseSession();

            return list;
        }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns></returns>
		public static OutputInvoiceLineList GetList(bool childs = true)
		{
			CriteriaEx criteria = OutputInvoiceLine.GetCriteria(OutputInvoiceLine.OpenSession());
			criteria.Childs = childs;

			//No criteria. Retrieve all de List
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = OutputInvoiceLineList.SELECT();

			OutputInvoiceLineList list = DataPortal.Fetch<OutputInvoiceLineList>(criteria);

			CloseSession(criteria.SessionCode);

			return list;
        }
        public static OutputInvoiceLineList GetList(int year, bool childs)
        {
            return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
        }
        public static OutputInvoiceLineList GetList(DateTime f_ini, DateTime f_fin, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                FechaIni = f_ini,
                FechaFin = f_fin,
            };

            return GetList(SELECT(conditions), childs);
        }
		public static OutputInvoiceLineList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(OutputInvoiceLineList.SELECT(conditions), childs);
		}

		public static OutputInvoiceLineList GetByExpedienteList(long oidExpediente, bool childs)
		{
			QueryConditions conditions = new QueryConditions { Expediente = ExpedientInfo.New(oidExpediente) };
			return GetList(conditions, childs);
		}
		public static OutputInvoiceLineList GetByExpedienteList(ExpedientInfo expediente, bool childs)
		{
			QueryConditions conditions = new QueryConditions { Expediente = expediente };
			return GetList(conditions, childs);
		}

		private static OutputInvoiceLineList GetList(string query, bool childs)
		{
			CriteriaEx criteria = OutputInvoiceLine.GetCriteria(OutputInvoiceLine.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			OutputInvoiceLineList list = DataPortal.Fetch<OutputInvoiceLineList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        public static OutputInvoiceLineList GetList(IList<OutputInvoiceLineInfo> list)
        {
            OutputInvoiceLineList flist = new OutputInvoiceLineList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (OutputInvoiceLineInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
		public static OutputInvoiceLineList GetList(IList<OutputInvoiceLine> list)
		{
			OutputInvoiceLineList flist = new OutputInvoiceLineList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (OutputInvoiceLine item in list)
					flist.AddItem(item.GetInfo());

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
        public static SortedBindingList<OutputInvoiceLineInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<OutputInvoiceLineInfo> sortedList = new SortedBindingList<OutputInvoiceLineInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<OutputInvoiceLineInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<OutputInvoiceLineInfo> sortedList = new SortedBindingList<OutputInvoiceLineInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        public static long GetCountByExpediente(long oid_exp)
        {
            CriteriaEx criteria = OutputInvoiceLine.GetCriteria(OutputInvoiceLine.OpenSession());
            criteria.Childs = false;

            criteria.Query = OutputInvoiceLine.COUNT_BY_EXPEDIENTE(oid_exp);
            IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, criteria.Session);
            reader.Read();
            long cuenta = Convert.ToInt64(reader["CUENTA"]);

            CloseSession(criteria.SessionCode);
            return cuenta;
        }

        public static OutputInvoiceLineList GetListByExpediente(long oidExpedient, bool childs)
        {
            CriteriaEx criteria = OutputInvoiceLine.GetCriteria(OutputInvoiceLine.OpenSession());
            criteria.Childs = childs;

			Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { Expediente = ExpedientInfo.New(oidExpedient) };
            criteria.Query = OutputInvoiceLineList.SELECT(conditions);
            
            OutputInvoiceLineList list = DataPortal.Fetch<OutputInvoiceLineList>(criteria);
            CloseSession(criteria.SessionCode);

            return list;
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Default call for GetChildList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static OutputInvoiceLineList GetChildList()
        {
            return OutputInvoiceLineList.GetChildList(true);
        }
        public static OutputInvoiceLineList GetChildList(IList<OutputInvoiceLineInfo> list)
        {
            OutputInvoiceLineList flist = new OutputInvoiceLineList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (OutputInvoiceLineInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static OutputInvoiceLineList GetChildList(IList<OutputInvoiceLine> list)
        {
            OutputInvoiceLineList flist = new OutputInvoiceLineList();

            if (list != null)
            {
                //int sessionCode = ConceptoFactura.OpenSession();
                //CriteriaEx criteria = null;

                flist.IsReadOnly = false;

                foreach (OutputInvoiceLine item in list)
                {
                    /*criteria = Partida.GetCriteria(sessionCode);
                    criteria.AddEq("OidExpediente", item.OidExpediente);
                    criteria.AddEq("OidProducto", item.OidProducto);
                    item.Partidas = Partidas.GetChildList(criteria.List<Partida>());*/

                    flist.AddItem(item.GetInfo());
                }

                flist.IsReadOnly = true;

                //ConceptoFactura.CloseSession(sessionCode);
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
                        this.AddItem(OutputInvoiceLineInfo.Get(SessionCode, reader, Childs));

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }
        protected override void Fetch(string hql)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IList list = nHMng.HQLSelect(hql);

                if (list.Count > 0)
                {
                    IsReadOnly = false;

                    foreach (OutputInvoiceLine item in list)
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
        protected void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IsReadOnly = false;

                while (reader.Read())
                {
                    this.AddItem(OutputInvoiceLineInfo.Get(SessionCode, reader, Childs));
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

        public static string SELECT() { return OutputInvoiceLine.SELECT(new QueryConditions(), false); }
        public static string SELECT(Library.Invoice.QueryConditions conditions) { return OutputInvoiceLine.SELECT(conditions, false); }
        public static string SELECT(OutputInvoiceInfo item) { return SELECT(new Library.Invoice.QueryConditions { Factura = item }); }
		public static string SELECT(StoreInfo item) { return SELECT(new Library.Invoice.QueryConditions { Almacen = item }); }
		public static string SELECT(ExpedientInfo item) { return SELECT(new Library.Invoice.QueryConditions { Expediente = item }); }

        #endregion
    }
}



