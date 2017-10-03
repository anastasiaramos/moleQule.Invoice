using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class OutputDeliveryList : ReadOnlyListBaseEx<OutputDeliveryList, OutputDeliveryInfo, OutputDelivery>
    {
        #region Business Methods

        public decimal TotalPendiente()
        {
            decimal pendiente = 0;

            foreach (OutputDeliveryInfo item in this)
                pendiente += item.Facturado ? item.Total : 0;

            return pendiente;
        }

        #endregion

        #region Factory Methods

        private OutputDeliveryList() { }

		private OutputDeliveryList(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>AlbaranList</returns>
        public static OutputDeliveryList GetChildList(bool childs)
        {
            CriteriaEx criteria = OutputDelivery.GetCriteria(OutputDelivery.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            OutputDeliveryList list = DataPortal.Fetch<OutputDeliveryList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Root Factory Methods

		public static OutputDeliveryList NewList() { return new OutputDeliveryList(); }

		public static OutputDeliveryList GetList(ETipoEntidad holderType) { return OutputDeliveryList.GetList(true, holderType); }
		public static OutputDeliveryList GetList(bool childs, ETipoEntidad holderType)
		{
			return GetList(childs, holderType, DateTime.MinValue, DateTime.MaxValue);
		}
		public static OutputDeliveryList GetList(bool childs, OutputInvoice factura)
		{
			return GetList(childs, factura.OidCliente, ETipoEntidad.Cliente, factura.OidSerie);
		}
		public static OutputDeliveryList GetList(bool childs, ETipoEntidad holderType, int year)
		{
			return GetList(childs, holderType, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}
		public static OutputDeliveryList GetList(bool childs, ETipoEntidad holderType, DateTime from, DateTime till)
		{
			return GetList(childs, 0, holderType, 0, from, till);
		}
		public static OutputDeliveryList GetList(bool childs, long oidSerie, ETipoFactura invoiceType)
		{
			return GetList(childs, 0, ETipoEntidad.Todos, oidSerie, invoiceType);
		}
		public static OutputDeliveryList GetList(bool childs, long oidSerie, DateTime from, DateTime till)
		{
			return GetList(childs, 0, ETipoEntidad.Todos, oidSerie, from, till);
		}
		public static OutputDeliveryList GetList(bool childs, long oidHolder, ETipoEntidad holderType, long oidSerie)
		{
			return GetList(childs, oidHolder, holderType, oidSerie, ETipoFactura.Todas);
		}
		public static OutputDeliveryList GetList(bool childs, long oidHolder, ETipoEntidad holderType, long oidSerie, ETipoFactura invoiceType)
		{
			return GetList(childs, oidHolder, holderType, oidSerie, ETipoAlbaranes.Todos, invoiceType, DateTime.MinValue, DateTime.MaxValue);
		}
		public static OutputDeliveryList GetList(bool childs, long oidHolder, ETipoEntidad holderType, long oidSerie, DateTime from, DateTime till)
		{
			return GetList(childs, oidHolder, holderType, oidSerie, ETipoAlbaranes.Todos, ETipoFactura.Todas, from, till);
		}
		public static OutputDeliveryList GetList(bool childs, long oidHolder, ETipoEntidad holderType, long oidSerie, ETipoAlbaranes deliveryType, ETipoFactura invoiceType, int year)
		{
			return GetList(childs, oidHolder, holderType, oidSerie, deliveryType, invoiceType, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}
		
		public static OutputDeliveryList GetList(bool childs,
											long oidHolder,
											ETipoEntidad holderType,
											long oidSerie,
											ETipoAlbaranes deliveryType,
											ETipoFactura invoiceType,
											DateTime from,
											DateTime till)
		{
			QueryConditions conditions = new QueryConditions
			{
				Serie = (oidSerie != 0) ? SerieInfo.New(oidSerie) : null,
				TipoEntidad = holderType,
				TipoAlbaranes = deliveryType,
				TipoFactura = invoiceType,
				FechaIni = from,
				FechaFin = till,
			};

			switch (holderType)
			{
				case ETipoEntidad.Cliente:
					conditions.Cliente = (oidHolder != 0) ? ClienteInfo.New(oidHolder) : null;
					break;

				case ETipoEntidad.WorkReport:
					conditions.WorkReport = (oidHolder != 0) ? WorkReportInfo.New(oidHolder) : null;
					break;
			}

			return GetList(childs, SELECT(conditions));
		}

		public static OutputDeliveryList GetList(QueryConditions conditions, bool childs)
		{
			CriteriaEx criteria = OutputDelivery.GetCriteria(OutputDelivery.OpenSession());
			criteria.Childs = childs;

			criteria.Query = OutputDeliveryList.SELECT(conditions);

			OutputDeliveryList list = DataPortal.Fetch<OutputDeliveryList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}

		public static OutputDeliveryList GetListBySerie(bool childs, long oid)
		{
			return GetListBySerie(childs, oid, DateTime.MinValue.Year, ETipoAlbaranes.Todos, ETipoFactura.Todas);
		}
        public static OutputDeliveryList GetListBySerie(bool childs, long oidSerie, int year, ETipoAlbaranes tipo, ETipoFactura invoiceType)
        {
			QueryConditions conditions = new QueryConditions 
			{ 
				Serie = SerieInfo.New(oidSerie),
				TipoAlbaranes = tipo,
				TipoFactura = invoiceType,
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};

			return GetList(childs, SELECT(conditions));
		}

		public static OutputDeliveryList GetByClientList(long oidClient, CriteriaEx criteria = null, bool childs = false)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoEntidad = ETipoEntidad.Cliente,
				Cliente = ClienteInfo.New(oidClient),
				PagingInfo = (criteria != null) ? criteria.PagingInfo : null,
				Filters = (criteria != null) ? criteria.Filters : null
			};
			return GetList(false, SELECT(conditions));
		}
        public static OutputDeliveryList GetListByCliente(bool childs, long oid)
        {
            return GetListByCliente(childs, oid, ETipoAlbaranes.Todos, DateTime.MinValue, DateTime.MaxValue);
        }
        public static OutputDeliveryList GetListByCliente(bool childs, long oidClient, ETipoAlbaranes tipo, DateTime from, DateTime till)
        {
			QueryConditions conditions = new QueryConditions 
			{
				TipoEntidad = ETipoEntidad.Cliente,
				Cliente = ClienteInfo.New(oidClient),
 				TipoAlbaranes = tipo,
				FechaIni = from,
				FechaFin = till
			};

			return GetList(false, SELECT(conditions));
        }

        public static OutputDeliveryList GetListByProducto(bool childs, long oidProduct)
        {
			QueryConditions conditions = new QueryConditions
			{
				TipoEntidad = ETipoEntidad.Cliente,
				Producto = ProductInfo.New(oidProduct)
			};

			return GetList(false, SELECT(conditions));
        }

        public static OutputDeliveryList GetListByPartida(bool childs, long oidBatch)
        {
			QueryConditions conditions = new QueryConditions
			{
				TipoEntidad = ETipoEntidad.Cliente,
				Partida = BatchInfo.New(oidBatch)
			};

			return GetList(false, SELECT(conditions));
        }

		public static OutputDeliveryList GetListByFactura(bool childs, long oidInvoice)
		{
			return GetListByFactura(childs, oidInvoice, DateTime.MinValue, DateTime.MaxValue);
		}
		public static OutputDeliveryList GetListByFactura(bool childs, long oidInvoice, DateTime from, DateTime till)
        {
			QueryConditions conditions = new QueryConditions
			{
				TipoEntidad = ETipoEntidad.Cliente,
				Factura = OutputInvoiceInfo.New(oidInvoice),
				FechaIni = from,
				FechaFin = till
			};

			return GetList(false, SELECT(conditions));
        }
		public static OutputDeliveryList GetListByTicket(bool childs, long oid)
		{
			return GetListByTicket(childs, oid, DateTime.MinValue, DateTime.MaxValue);
		}
		public static OutputDeliveryList GetListByTicket(bool childs, long oid, DateTime from, DateTime till)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoEntidad = ETipoEntidad.Cliente,
				Ticket = Ticket.New().GetInfo(false),
				FechaIni = from,
				FechaFin = till
			};
			conditions.Ticket.Oid = oid;

			return GetList(false, SELECT(conditions));
		}
		
		public static OutputDeliveryList GetNoFacturados(bool childs)
		{
			return GetNoFacturados(childs, DateTime.MinValue, DateTime.MaxValue);
		}
		public static OutputDeliveryList GetNoFacturados(bool childs, int year)
		{
			return GetNoFacturados(childs, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}
		public static OutputDeliveryList GetNoFacturados(bool childs, DateTime from, DateTime till)
		{
			return GetNoFacturados(childs, 0, 0, from, till);
		}
		public static OutputDeliveryList GetNoFacturados(bool childs, long oidClient, long oidSerie)
		{
			return GetNoFacturados(childs, oidClient, oidSerie, ETipoFactura.Todas);
		}
		public static OutputDeliveryList GetNoFacturados(bool childs, long oidClient, long oidSerie, ETipoFactura tipo_albaran)
		{
			return GetList(childs, oidClient, ETipoEntidad.Cliente, oidSerie, ETipoAlbaranes.NoFacturados, tipo_albaran, DateTime.MinValue, DateTime.MaxValue);
		}
		public static OutputDeliveryList GetNoFacturados(bool childs, long oidClient, long oidSerie, int year)
		{
			return GetNoFacturados(childs, oidClient, oidSerie, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}
		public static OutputDeliveryList GetNoFacturados(bool childs, long oidClient, long oidSerie, DateTime from, DateTime till)
		{
			return GetList(childs, oidClient, ETipoEntidad.Cliente, oidSerie, ETipoAlbaranes.NoFacturados, ETipoFactura.Todas, from, till);
		}

		public static OutputDeliveryList GetNoTicketList(long oidSerie, bool childs)
		{
			return GetNoTicketList(oidSerie, DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static OutputDeliveryList GetNoTicketList(long oidSerie, DateTime from, DateTime till, bool childs)
		{
			return GetList(childs, 0, ETipoEntidad.Cliente, oidSerie, ETipoAlbaranes.NoTicket, ETipoFactura.Todas, from, till);
		}

        public static OutputDeliveryList GetFacturados(bool childs)
        {
			return GetFacturados(childs, DateTime.MinValue, DateTime.MaxValue);
        }
		public static OutputDeliveryList GetFacturados(bool childs, int year)
		{
			return GetFacturados(childs, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}
		public static OutputDeliveryList GetFacturados(bool childs, DateTime from, DateTime till)
		{
			return GetFacturados(childs, 0, 0, from, till);
		}
		public static OutputDeliveryList GetFacturados(bool childs, long oidClient, long oidSerie, DateTime from, DateTime till)
        {
			return GetList(childs, oidClient, ETipoEntidad.Cliente, oidSerie, ETipoAlbaranes.Facturados, ETipoFactura.Todas, from, till);
        }

		public static OutputDeliveryList GetWorkList(bool childs)
		{
			return GetWorkList(childs, DateTime.MinValue, DateTime.MaxValue);
		}
		public static OutputDeliveryList GetWorkList(bool childs, int year)
		{
			return GetWorkList(childs, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}
		public static OutputDeliveryList GetWorkList(bool childs, DateTime from, DateTime till)
		{
			return GetWorkList(childs, 0, 0, from, till);
		}
		public static OutputDeliveryList GetWorkList(bool childs, long oidWorkReport, long oidSerie, DateTime from, DateTime till)
		{
			return GetList(childs, oidWorkReport, ETipoEntidad.WorkReport, oidSerie, ETipoAlbaranes.Work, ETipoFactura.Todas, from, till);
		}

        public static OutputDeliveryList GetAgrupados(bool childs)
        {
			return GetList(childs, 0, ETipoEntidad.Cliente, 0, ETipoAlbaranes.Agrupados, ETipoFactura.Todas, DateTime.MinValue, DateTime.MaxValue);
        }
		public static OutputDeliveryList GetAgrupados(bool childs, int year)
		{
			return GetList(childs, 0, ETipoEntidad.Cliente, 0, ETipoAlbaranes.Agrupados, ETipoFactura.Todas, DateAndTime.FirstDay(year), DateAndTime.LastDay(year));
		}

		public static OutputDeliveryList GetNoFacturadosAgrupados(long oidSerie, bool childs)
        {
			return GetNoFacturadosAgrupados(oidSerie, DateTime.MinValue, DateTime.MaxValue, childs);
        }
		public static OutputDeliveryList GetNoFacturadosAgrupados(long oidSerie, DateTime from, DateTime till, bool childs)
        {
			QueryConditions conditions = new QueryConditions
			{
				TipoEntidad = ETipoEntidad.Cliente,
				Serie = (oidSerie != 0) ? SerieInfo.New(oidSerie) : null,
				TipoAlbaranes = ETipoAlbaranes.Agrupados,
				FechaIni = from,
				FechaFin = till,
			};

			return GetList(childs, OutputDelivery.SELECT_PENDIENTES_CONTADO(conditions, childs));
        }

		public static OutputDeliveryList GetList(List<long> oid_list, bool childs)
		{
			return GetList(childs, OutputDelivery.SELECT_BY_LIST(oid_list, false));
		}

		private static OutputDeliveryList GetList(bool childs, string query)
        {
            CriteriaEx criteria = OutputDelivery.GetCriteria(OutputDelivery.OpenSession());
            criteria.Childs = childs;

            criteria.Query = query;
            OutputDeliveryList list = DataPortal.Fetch<OutputDeliveryList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static OutputDeliveryList GetList(IList<OutputDeliveryInfo> list)
        {
            OutputDeliveryList flist = new OutputDeliveryList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (OutputDeliveryInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
		public static OutputDeliveryList GetList(IList<OutputDelivery> list)
		{
			OutputDeliveryList flist = new OutputDeliveryList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (OutputDelivery item in list)
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
        public static SortedBindingList<OutputDeliveryInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<OutputDeliveryInfo> sortedList = new SortedBindingList<OutputDeliveryInfo>(GetList(ETipoEntidad.Cliente));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<OutputDeliveryInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
			SortedBindingList<OutputDeliveryInfo> sortedList = new SortedBindingList<OutputDeliveryInfo>(GetList(childs, ETipoEntidad.Cliente));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        /// <summary>
        /// Builds a AlbaranList from a IList<!--<Albaran>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>AlbaranList</returns>
        public List<OutputDeliveryInfo> GetListInfo()
        {
            List<OutputDeliveryInfo> flist = new List<OutputDeliveryInfo>();

            foreach (OutputDeliveryInfo item in this)
                flist.Add(item);

            return flist;
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Default call for GetChildList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static OutputDeliveryList GetChildList() { return OutputDeliveryList.GetChildList(true); }

        /// <summary>
        /// Builds a AlbaranList from a IList<!--<AlbaranInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>AlbaranList</returns>
        public static OutputDeliveryList GetChildList(IList<OutputDeliveryInfo> list)
        {
            OutputDeliveryList flist = new OutputDeliveryList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (OutputDeliveryInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static OutputDeliveryList GetChildList(IList<OutputDelivery> list)
        {
            OutputDeliveryList flist = new OutputDeliveryList();

            if (list != null)
            {
                int sessionCode = OutputDelivery.OpenSession();
                CriteriaEx criteria = null;

                flist.IsReadOnly = false;

                foreach (OutputDelivery item in list)
                {
                    criteria = OutputDeliveryLine.GetCriteria(sessionCode);
                    criteria.AddEq("OidAlbaran", item.Oid);
                    item.Conceptos = OutputDeliveryLines.GetChildList(criteria.List<OutputDeliveryLine>());

                    flist.AddItem(item.GetInfo());
                }

                flist.IsReadOnly = true;

                OutputDelivery.CloseSession(sessionCode);
            }

            return flist;
        }

		public static OutputDeliveryList GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static OutputDeliveryList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new OutputDeliveryList(sessionCode, reader, childs); }

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
                        this.AddItem(OutputDeliveryInfo.GetChild(SessionCode, reader, Childs));

                    IsReadOnly = true;
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

                    foreach (OutputDelivery item in list)
                        this.AddItem(item.GetInfo(false));

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
				iQExceptionHandler.TreatException(ex, new object[] { hql });
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
                    this.AddItem(OutputDeliveryInfo.GetChild(SessionCode, reader, Childs));

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
		public static string SELECT(QueryConditions conditions) { return OutputDelivery.SELECT(conditions, false); }

        #endregion
    }
}



