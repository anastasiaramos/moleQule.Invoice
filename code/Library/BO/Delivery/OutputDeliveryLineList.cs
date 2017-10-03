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
    public class OutputDeliveryLineList : ReadOnlyListBaseEx<OutputDeliveryLineList, OutputDeliveryLineInfo>
    {
        #region Factory Methods

        private OutputDeliveryLineList() { }
		
        #endregion

        #region Root Factory Methods

		public static OutputDeliveryLineList NewList() { return new OutputDeliveryLineList(); }

		public static OutputDeliveryLineList GetList() { return OutputDeliveryLineList.GetList(true); }
		public static OutputDeliveryLineList GetList(bool childs)
		{
			return GetList(OutputDeliveryLineList.SELECT(), childs);
		}
		public static OutputDeliveryLineList GetList(int year, bool childs)
		{
			return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static OutputDeliveryLineList GetList(DateTime f_ini, DateTime f_fin, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = f_ini,
				FechaFin = f_fin,
			};
		
			return GetList(SELECT(conditions), childs);
		}
		public static OutputDeliveryLineList GetList(ProductInfo producto, ClienteInfo cliente, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Producto = producto,
				Cliente = cliente
			};

			return GetList(conditions, childs);
		}

		public static OutputDeliveryLineList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(OutputDeliveryLineList.SELECT(conditions), childs);
		}
		private static OutputDeliveryLineList GetList(string query, bool childs)
		{
			CriteriaEx criteria = OutputDeliveryLine.GetCriteria(OutputDeliveryLine.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			OutputDeliveryLineList list = DataPortal.Fetch<OutputDeliveryLineList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        /// <summary>
        /// Builds a ConceptoAlbaranList from a IList<!--<ConceptoAlbaranInfo>-->.
        /// Doesnt retrieve child data from DB.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static OutputDeliveryLineList GetList(IList<OutputDeliveryLineInfo> list)
        {
            OutputDeliveryLineList flist = new OutputDeliveryLineList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (OutputDeliveryLineInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
		public static OutputDeliveryLineList GetList(IList<OutputDeliveryLine> list)
		{
			OutputDeliveryLineList flist = new OutputDeliveryLineList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (OutputDeliveryLine item in list)
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
        public static SortedBindingList<OutputDeliveryLineInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<OutputDeliveryLineInfo> sortedList = new SortedBindingList<OutputDeliveryLineInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<OutputDeliveryLineInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<OutputDeliveryLineInfo> sortedList = new SortedBindingList<OutputDeliveryLineInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        public static OutputDeliveryLineList GetListByExpediente(long oidExpedient, bool childs)
        {
            CriteriaEx criteria = OutputDeliveryLine.GetCriteria(OutputDeliveryLine.OpenSession());
            criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions { Expediente = ExpedientInfo.New(oidExpedient) };

            criteria.Query = OutputDeliveryLineList.SELECT(conditions);
            OutputDeliveryLineList list = DataPortal.Fetch<OutputDeliveryLineList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Child Factory Methods

		private OutputDeliveryLineList(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}

        public static OutputDeliveryLineList GetChildList(IList<OutputDeliveryLineInfo> list)
        {
            OutputDeliveryLineList flist = new OutputDeliveryLineList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (OutputDeliveryLineInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static OutputDeliveryLineList GetChildList(IList<OutputDeliveryLine> list)
        {
            OutputDeliveryLineList flist = new OutputDeliveryLineList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (OutputDeliveryLine item in list)
                    flist.AddItem(item.GetInfo());
	
				flist.IsReadOnly = true;
            }

            return flist;
        }

		public static OutputDeliveryLineList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new OutputDeliveryLineList(sessionCode, reader, childs); }

		public static OutputDeliveryLineList GetChildList(OutputDeliveryInfo parent, bool childs)
		{
			CriteriaEx criteria = OutputDeliveryLine.GetCriteria(OutputDeliveryLine.OpenSession());

			criteria.Query = OutputDeliveryLineList.SELECT(parent);
			criteria.Childs = childs;

			OutputDeliveryLineList list = DataPortal.Fetch<OutputDeliveryLineList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
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
                        this.AddItem(OutputDeliveryLineInfo.Get(SessionCode, reader, Childs));

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

                    foreach (OutputDeliveryLine item in list)
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
                    this.AddItem(OutputDeliveryLineInfo.Get(SessionCode, reader, Childs));

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
		public static string SELECT(QueryConditions conditions) { return OutputDeliveryLine.SELECT(conditions, false); }
		public static string SELECT(OutputDeliveryInfo albaran) { return SELECT(new QueryConditions { OutputDelivery = albaran }); }

        #endregion
    }
}



