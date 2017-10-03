using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class ChargeSummaryList : ReadOnlyListBaseEx<ChargeSummaryList, ChargeSummary>
    {
		#region Business Methods

		public decimal TotalFacturado()
		{
			decimal total = 0;

			foreach (ChargeSummary item in this)
				total += item.TotalFacturado;

			return total;
		}

		public decimal TotalPendiente()
		{
			decimal total = 0;

			foreach (ChargeSummary item in this)
				total += item.Pendiente;

			return total;
		}

		public decimal TotalPendienteVto()
		{
			decimal total = 0;

			foreach (ChargeSummary item in this)
				total += item.EfectosPendientesVto;

			return total;
		}

		public decimal TotalNegociado()
		{
			decimal total = 0;

			foreach (ChargeSummary item in this)
				total += item.EfectosNegociados;

			return total;
		}

		public decimal TotalDudosoCobro()
		{
			decimal total = 0;

			foreach (ChargeSummary item in this)
				total += item.DudosoCobro;

			return total;
		}

		#endregion

        #region Common Factory Methods

        private ChargeSummaryList() { }

		#endregion

		#region Root Factory Methods

		public static ChargeSummaryList NewList() { return new ChargeSummaryList(); }

		public static ChargeSummaryList GetList() { return GetList(SELECT()); }
		public static ChargeSummaryList GetList(int year) 
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year),
			};

			return GetList(SELECT(conditions));
		}

		public static ChargeSummaryList GetPendientesList() { return GetList(SELECT_PENDIENTES(new QueryConditions())); }
		public static ChargeSummaryList GetPendientesList(int year) 
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year),
			};

			return GetList(SELECT_PENDIENTES(new QueryConditions())); 
		}

		public static ChargeSummaryList GetDudosoCobroList() { return GetList(SELECT_DUDOSO_COBRO(new QueryConditions())); }
		public static ChargeSummaryList GetDudosoCobroList(int year)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year),
			};

			return GetList(SELECT_DUDOSO_COBRO(conditions));
		}

		public static ChargeSummaryList GetList(string query)
		{
			CriteriaEx criteria = Charge.GetCriteria(Charge.OpenSession());
			criteria.Childs = false;

			criteria.Query = query;

			ChargeSummaryList list = DataPortal.Fetch<ChargeSummaryList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		#endregion		

		#region Child Factory Methods

		private ChargeSummaryList(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			Fetch(reader);
		}

		public static ChargeSummaryList GetChildList(IList<ClienteInfo> list)
        {
            ChargeSummaryList flist = new ChargeSummaryList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ClienteInfo item in list)
                    flist.AddItem(item.GetResumen());

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static ChargeSummaryList GetChildList(IList<ChargeSummary> list)
        {
            ChargeSummaryList flist = new ChargeSummaryList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ChargeSummary item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static ChargeSummaryList GetChildList(int sessionCode, IDataReader reader) { return new ChargeSummaryList(sessionCode, reader); }

        public static ChargeSummaryList GetList(IList<ChargeSummary> list)
        {
            ChargeSummaryList flist = new ChargeSummaryList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (ChargeSummary item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        #endregion

        #region Root Data Access

        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                IsReadOnly = false;

                while (reader.Read())
                    this.AddItem(ChargeSummary.Get(reader));

                IsReadOnly = true;
            }
            catch (Exception ex)
            {
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
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
                    this.AddItem(ChargeSummary.Get(reader));

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
		public static string SELECT(QueryConditions conditions) { return ChargeSummary.SELECT(conditions); }
		public static string SELECT_PENDIENTES(QueryConditions conditions) { return ChargeSummary.SELECT_PENDIENTES(conditions); }
		public static string SELECT_DUDOSO_COBRO(QueryConditions conditions) { return ChargeSummary.SELECT_DUDOSO_COBRO(conditions); }
		public static string SELECT_BY_CLIENTE(long oidCliente) { return ChargeSummary.SELECT_BY_CLIENTE(oidCliente); }

        #endregion
    }
}



