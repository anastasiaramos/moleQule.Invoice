using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class OutputInvoiceList : ReadOnlyListBaseEx<OutputInvoiceList, OutputInvoiceInfo, OutputInvoice>
    {
        #region Business Methods

		public decimal Total()
		{
            return Items.Where(x => x.EEstado != EEstado.Anulado).Sum(x => x.Total);
		}

		public decimal TotalExpediente()
		{
            return Items.Where(x => x.EEstado != EEstado.Anulado).Sum(x => x.TotalExpediente);
		}

        public decimal TotalPendiente()
        {
            return Items.Where(x => x.EEstado != EEstado.Anulado).Sum(x => x.Pendiente);
        }

        public decimal TotalPendienteVencimiento()
        {
            return Items.Where(x => x.EEstado != EEstado.Anulado).Sum(x => x.EfectosPendientesVto);
        }

        public decimal TotalNegociado()
        {
             return Items.Where(x => x.EEstado != EEstado.Anulado).Sum(x => x.NoVencido);
        }

        public decimal TotalDevuelto()
        {
            return Items.Where(x => x.EEstado != EEstado.Anulado).Sum(x => x.Vencido);
        }

		public decimal TotalDudosoCobro()
		{
			return Items.Where(x => x.EEstado != EEstado.Anulado).Sum(x => x.DudosoCobro);
		}

		public void UpdateCobroValues(Charge cobro)
		{
			OutputInvoiceInfo item;
			decimal acumulado;
			CobroFactura cobroFactura;

			for (int i = 0; i < Items.Count; i++)
			{
				item = Items[i];

				cobroFactura = cobro.CobroFacturas.GetItemByFactura(item.Oid);

				if (cobroFactura != null)
				{
					item.FechaAsignacion = cobroFactura.FechaAsignacion.ToShortDateString();
					item.Asignado = cobroFactura.Cantidad;
				}
				else
					item.FechaAsignacion = (item.Asignado != 0) ? DateTime.Now.ToShortDateString() : DateTime.MinValue.ToShortDateString();

				if (i == 0) acumulado = 0;
				else acumulado = Items[i - 1].Acumulado;

				item.Acumulado = acumulado + item.PendienteVencido;
				item.Vinculado = (item.Asignado == 0) ? Resources.Labels.SET_COBRO : Resources.Labels.RESET_COBRO;
			}
		}

        #endregion

        #region Common Factory Methods

        private OutputInvoiceList() { }

        private OutputInvoiceList(IDataReader reader)
        {
            Fetch(reader);
        }

        private OutputInvoiceList(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>FacturaList</returns>
        public static OutputInvoiceList GetChildList(bool childs)
        {
            CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            OutputInvoiceList list = DataPortal.Fetch<OutputInvoiceList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Root Factory Methods

		public static OutputInvoiceList NewList() { return new OutputInvoiceList(); }

		public static OutputInvoiceList GetList() { return GetList(true); }
        public static OutputInvoiceList GetList(bool childs) 
		{ 
			return GetList(DateTime.MinValue, DateTime.MaxValue, childs); 
		}
		public static OutputInvoiceList GetList(int year, bool childs)
		{
			return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static OutputInvoiceList GetList(DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoFacturas = ETipoFacturas.Todas,
				TipoFactura = ETipoFactura.Todas,
				FechaIni = from,
				FechaFin = till,
			};

			return GetList(conditions, childs);
		}

		public static OutputInvoiceList GetList(List<long> oidList, bool childs) 
		{ 
			return GetList(OutputInvoice.SELECT_BY_LIST(oidList, false), childs); 
		}
		public static OutputInvoiceList GetList(QueryConditions conditions, bool childs)
		{
			CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
			criteria.Childs = childs;

			criteria.Query = OutputInvoiceList.SELECT(conditions);

			OutputInvoiceList list = DataPortal.Fetch<OutputInvoiceList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}
		public static OutputInvoiceList GetList(long oidClient, CriteriaEx criteria, bool childs)
		{
			return GetList(oidClient, DateTime.MinValue, DateTime.MaxValue, EStepGraph.None, criteria, childs);
		}
		public static OutputInvoiceList GetList(long oidClient, DateTime from, DateTime till, EStepGraph step, CriteriaEx criteria, bool childs)
		{
			QueryConditions conditions = new QueryConditions()
			{
				PagingInfo = (criteria != null) ? criteria.PagingInfo : null,
				Filters = (criteria != null) ? criteria.Filters : null,
				Orders = (criteria != null) ? criteria.Orders : null,
				Cliente = ClienteInfo.New(oidClient),
				FechaIni = from,
				FechaFin = till,
				Step = step,
			};

			string query = OutputInvoice.SELECT(conditions, false);
			if (criteria != null) criteria.PagingInfo = conditions.PagingInfo;

			return GetList(query, criteria, childs);
		}

		public static OutputInvoiceList GetCobradasList(bool childs)
		{
			return GetCobradasList(DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static OutputInvoiceList GetCobradasList(int year, bool childs)
		{
			return GetCobradasList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static OutputInvoiceList GetCobradasList(DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoFactura = ETipoFactura.Todas,
				FechaIni = from,
				FechaFin = till,
			};

			return GetCobradasList(conditions, childs);
		}
		public static OutputInvoiceList GetCobradasList(QueryConditions conditions, bool childs)
		{
			conditions.TipoFacturas = ETipoFacturas.Cobradas;
			return GetList(OutputInvoiceList.SELECT(conditions), false);
		}

		public static OutputInvoiceList GetCobradasByClienteList(long oidClient, bool childs)
		{
			QueryConditions conditions = new QueryConditions();
			conditions.Cliente = Cliente.New().GetInfo();
			conditions.Cliente.Oid = oidClient;

			return GetCobradasList(conditions, childs);
		}

		public static OutputInvoiceList GetNoCobradasList(bool childs) 
		{
			return GetNoCobradasList(DateTime.MinValue, DateTime.MaxValue, childs); 
		}
		public static OutputInvoiceList GetNoCobradasList(int year, bool childs)
		{
			return GetNoCobradasList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static OutputInvoiceList GetNoCobradasList(DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoFactura = ETipoFactura.Todas,
				FechaIni = from,
				FechaFin = till,
			};

			return GetNoCobradasList(conditions, childs);
		}
		public static OutputInvoiceList GetNoCobradasList(QueryConditions conditions, bool childs)
		{
			conditions.TipoFacturas = ETipoFacturas.Pendientes;
			return GetList(conditions, childs);
		}

		public static OutputInvoiceList GetNoCobradasByClienteList(long oidClient, bool childs)
		{
			QueryConditions conditions = new QueryConditions();
			conditions.Cliente = Cliente.New().GetInfo();
			conditions.Cliente.Oid = oidClient;

			return GetNoCobradasList(conditions, childs);
		}

		public static OutputInvoiceList GetDudosoCobroList(bool childs)
		{
			return GetDudosoCobroList(DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static OutputInvoiceList GetDudosoCobroList(int year, bool childs)
		{
			return GetDudosoCobroList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static OutputInvoiceList GetDudosoCobroList(DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoFactura = ETipoFactura.Todas,
				FechaIni = from,
				FechaFin = till,
			};

			return GetDudosoCobroList(conditions, childs);
		}
		public static OutputInvoiceList GetDudosoCobroList(QueryConditions conditions, bool childs)
		{
			conditions.TipoFacturas = ETipoFacturas.DudosoCobro;
			return GetList(conditions, childs);
		}

		public static OutputInvoiceList GetBySerieAndYearList(long oidSerie, int year, ETipoFactura tipo)
		{
			CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
			criteria.Childs = false;

			QueryConditions conditions = new QueryConditions
			{
				Serie = Serie.New().GetInfo(false),
				TipoFactura = tipo,
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year),
			};
			conditions.Serie.Oid = oidSerie;

			return GetList(OutputInvoiceList.SELECT(conditions), false);
		}

		public static OutputInvoiceList GetByBranchList(long oidPartner, long oidBranch, DateTime from, DateTime till, EStepGraph step, CriteriaEx criteria, bool childs)
        {
            QueryConditions conditions = new QueryConditions () 
			{
				PagingInfo = (criteria != null) ? criteria.PagingInfo : null,
				Filters = (criteria != null) ? criteria.Filters : null,
				Orders = (criteria != null) ? criteria.Orders : null,
				IAcreedor = ProviderBaseInfo.New(oidPartner, ETipoAcreedor.Partner),
				OidEntity = oidBranch,
				FechaIni = from,
				FechaFin = till,
				Step = step,
			};

			string query = OutputInvoice.SELECT_BY_BRANCH(conditions, false);
			if (criteria != null) criteria.PagingInfo = conditions.PagingInfo;

			return GetList(query, criteria, childs);
        }

		public static OutputInvoiceList GetByClienteList(long oidClient, bool childs)
		{
			QueryConditions conditions = new QueryConditions();
			conditions.Cliente = Cliente.New().GetInfo();
			conditions.Cliente.Oid = oidClient;

			return GetList(conditions, childs);
		}
		public static OutputInvoiceList GetByClienteList(ClienteInfo cliente, bool childs)
		{
			QueryConditions conditions = new QueryConditions();
			conditions.Cliente = cliente;

			return GetList(conditions, childs);
		}

		public static OutputInvoiceList GetByCobroList(ChargeInfo cobro, bool childs)
		{
			QueryConditions conditions = new QueryConditions();
			conditions.Cobro = cobro;

			return GetList(OutputInvoice.SELECT_BY_COBRO(conditions, false), childs);
		}
		public static OutputInvoiceList GetByCobroList(long oid_cobro, bool childs)
		{
			QueryConditions conditions = new QueryConditions();
			conditions.Cobro = Charge.New(ETipoCobro.Todos).GetInfo();
			conditions.Cobro.Oid = oid_cobro;

			return GetList(OutputInvoice.SELECT_BY_COBRO(conditions, false), childs);
		}
        public static OutputInvoiceList GetBeneficioList(QueryConditions conditions, bool childs)
        {
            return GetList(OutputInvoice.SELECT_BENEFICIO(conditions, false), childs);
        }

		public static OutputInvoiceList GetByCobroAndNoCobradasByClienteList(long oidCharge, long oidClient, bool childs)
		{
			OutputInvoiceList byCobro = GetByCobroList(oidCharge, childs);
			OutputInvoiceList pendientes = GetNoCobradasByClienteList(oidClient, childs);

			OutputInvoiceList list = new OutputInvoiceList();
			list.IsReadOnly = false;

			foreach(OutputInvoiceInfo item in byCobro)
				list.AddItem(item);

			foreach(OutputInvoiceInfo item in pendientes)
				if (list.GetItem(item.Oid) == null) list.AddItem(item);

			list.IsReadOnly = true;

			return list;
		}

		public static OutputInvoiceList GetByExpedienteList(long oidExpediente, bool childs)
		{
			QueryConditions conditions = new QueryConditions { Expediente = ExpedientInfo.New(oidExpediente) };
			return GetList(conditions, childs);
		}
		public static OutputInvoiceList GetByExpedienteList(ExpedientInfo expediente, bool childs)
		{
			QueryConditions conditions = new QueryConditions { Expediente = expediente };
			return GetList(conditions, childs);
		}
        public static OutputInvoiceList GetByExpedienteList(List<long> oidExpedients, bool childs)
        {
            QueryConditions conditions = new QueryConditions { OidList = oidExpedients };
            return GetList(OutputInvoice.SELECT_BY_EXPEDIENTS(conditions, false), childs);
        }

        private static OutputInvoiceList GetList(string query, bool childs)
        {
            CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
            criteria.Childs = childs;

            criteria.Query = query;
            OutputInvoiceList list = DataPortal.Fetch<OutputInvoiceList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static SortedBindingList<OutputInvoiceInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<OutputInvoiceInfo> sortedList = new SortedBindingList<OutputInvoiceInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<OutputInvoiceInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<OutputInvoiceInfo> sortedList = new SortedBindingList<OutputInvoiceInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        public static OutputInvoiceList GetList(IList<OutputInvoice> list)
        {
            OutputInvoiceList flist = new OutputInvoiceList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (OutputInvoice item in list)
                    flist.AddItem(item.GetInfo());

                flist.IsReadOnly = true;
            }

            return flist;
        }
		public static OutputInvoiceList GetList(IList<OutputInvoiceInfo> list)
		{
			OutputInvoiceList flist = new OutputInvoiceList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (OutputInvoiceInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Default call for GetChildList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static OutputInvoiceList GetChildList()
        {
            return OutputInvoiceList.GetChildList(true);
        }

        /// <summary>
        /// Builds a FacturaList from a IList<!--<FacturaInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>FacturaList</returns>
        public static OutputInvoiceList GetChildList(IList<OutputInvoiceInfo> list)
        {
            OutputInvoiceList flist = new OutputInvoiceList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (OutputInvoiceInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        public static OutputInvoiceList GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static OutputInvoiceList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new OutputInvoiceList(sessionCode, reader, childs); }

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
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;
#if TRACE
					ControlerBase.AppControler.Timer.Record("FacturaList::Fetch - SELECT");
#endif
                    while (reader.Read())
                        AddItem(OutputInvoiceInfo.GetChild(SessionCode, reader, Childs));
 #if TRACE
					ControlerBase.AppControler.Timer.Record("FacturaList::Fetch - Generacion de objetos");
#endif
                    IsReadOnly = true;

                    if (criteria.PagingInfo != null)
                    {
                        reader = nHManager.Instance.SQLNativeSelect(OutputInvoice.SELECT_COUNT(criteria), criteria.Session);
                        if (reader.Read()) criteria.PagingInfo.TotalItems = Format.DataReader.GetInt32(reader, "TOTAL_ROWS");
                    }
                }
            }
            catch (Exception ex)
            {
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
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

                    foreach (OutputInvoice item in list)
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
                    AddItem(OutputInvoiceInfo.GetChild(SessionCode, reader, Childs));
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
        public static string SELECT(QueryConditions conditions) { return OutputInvoice.SELECT(conditions, false); }

		#endregion
    }
}