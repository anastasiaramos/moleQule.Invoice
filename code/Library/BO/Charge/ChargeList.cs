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
    public class ChargeList : ReadOnlyListBaseEx<ChargeList, ChargeInfo, Charge>
	{
		#region Business Methods

		public decimal Total()
		{
			decimal total = 0;
			foreach (ChargeInfo item in this)
			{
				if (item.EEstado == EEstado.Anulado) continue;
				total += item.Importe;
			}

			return total;
		}

		#endregion

		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private ChargeList() {}
		private ChargeList(IList<ChargeInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion

        #region Child Factory Methods

        public static ChargeList GetChildList(bool childs)
        {
			CriteriaEx criteria = Charge.GetCriteria(Charge.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = ChargeList.SELECT();			
		
			ChargeList list = DataPortal.Fetch<ChargeList>(criteria);
			
			CloseSession(criteria.SessionCode);
			return list;
        }
		
		#endregion

        #region Root Factory Methods

		public static ChargeList NewList() { return new ChargeList(); }

        public static ChargeList GetList()
        {
            return ChargeList.GetList(true);
        }
		public static ChargeList GetList(bool childs)
		{
			return GetList(DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static ChargeList GetList(int year, bool childs)
		{
			return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static ChargeList GetList(DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = from,
				FechaFin = till
			};

			return GetList(SELECT(conditions), childs);
		}

		public static ChargeList GetListREA(bool childs)
		{
			return GetListREA(DateTime.MinValue, DateTime.MaxValue, childs);
		}
		public static ChargeList GetListREA(int year, bool childs)
		{
			return GetListREA(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static ChargeList GetListREA(DateTime from, DateTime till, bool childs)
		{
            List<string> fields = new List<string>();
            fields.Add("Fecha");
            fields.Add("Codigo");

			QueryConditions conditions = new QueryConditions
			{
				TipoCobro = ETipoCobro.REA,
				FechaIni = from,
				FechaFin = till,
                Order = ListSortDirection.Descending,
                OrderFields = fields
			};
            
			return GetList(SELECT(conditions), childs);
		}

        public static ChargeList GetListFomento(bool childs)
        {
            return GetListFomento(DateTime.MinValue, DateTime.MaxValue, childs);
        }
        public static ChargeList GetListFomento(long year, bool childs)
        {
            return GetListFomento(DateAndTime.FirstDay((int)year), DateAndTime.LastDay((int)year), childs);
        }
        public static ChargeList GetListFomento(DateTime from, DateTime till, bool childs)
        {
            List<string> fields = new List<string>();
            fields.Add("Fecha");
            fields.Add("Codigo");

            QueryConditions conditions = new QueryConditions
            {
                TipoCobro = ETipoCobro.Fomento,
                FechaIni = from,
                FechaFin = till,
                Order = ListSortDirection.Descending,
                OrderFields = fields
            };

            return GetList(SELECT(conditions), childs);
        }

		public static ChargeList GetListClientes(bool childs)
		{
			return GetListClientes(new Library.Invoice.QueryConditions(), childs);
		}
		public static ChargeList GetListClientes(int year, bool childs)
		{
			return GetListClientes(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static ChargeList GetListClientes(DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = from,
				FechaFin = till
			};

			return GetListClientes(conditions, childs);
		}
		public static ChargeList GetListClientes(Library.Invoice.QueryConditions conditions, bool childs)
        {
            conditions.TipoCobro = ETipoCobro.Cliente;
            conditions.OrderFields = new List<string>();
            conditions.OrderFields.Add("Fecha");
			return GetList(SELECT(conditions), childs); 
        }

		public static ChargeList GetListPendientes(ETipoCobro tipo, DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoCobro = tipo,
				FechaAuxIni = from,
				FechaAuxFin = till
			};

			return GetList(SELECT_PENDIENTES(conditions), childs);
		}
		public static ChargeList GetListNegociados(ETipoCobro tipo, DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoCobro = tipo,
				FechaAuxIni = from,
				FechaAuxFin = till
			};

			return GetList(SELECT_NEGOCIADOS(conditions), childs);
		}

		public static ChargeList GetByClientList(long oidClient, bool childs)
		{
            QueryConditions conditions = new QueryConditions()
            {
                Cliente = ClienteInfo.New(oidClient)
            };

            return GetListClientes(conditions, childs);
		}
        public static ChargeList GetByClientList(long oidClient, CriteriaEx criteria, bool childs)
        {
            QueryConditions conditions = new QueryConditions()
            {
                PagingInfo = (criteria != null) ? criteria.PagingInfo : null,
                Filters = (criteria != null) ? criteria.Filters : null,
                Orders = (criteria != null) ? criteria.Orders : null,
                Cliente = ClienteInfo.New(oidClient),
                TipoCobro = ETipoCobro.Cliente
            };

            string query = Charge.SELECT(conditions, false);
            if (criteria != null) criteria.PagingInfo = conditions.PagingInfo;

            return GetList(SELECT(conditions), childs); 
        }

		public static ChargeList GetListByVencimientoSinApunte(QueryConditions conditions, bool childs)
		{
			return GetList(Charge.SELECT_VENCIMIENTO_SIN_APUNTE(conditions), childs);
		}

		public static ChargeList GetList(Library.Invoice.QueryConditions conditions, bool childs)
        {
            conditions.OrderFields = new List<string>();
            conditions.OrderFields.Add("Fecha");

			return GetList(ChargeList.SELECT(conditions), childs);
		}
		private static ChargeList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Charge.GetCriteria(Charge.OpenSession());
			criteria.Childs = childs;

			criteria.Query = query;
			ChargeList list = DataPortal.Fetch<ChargeList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        /// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static ChargeList GetList(IList<ChargeInfo> list) { return new ChargeList(list, false); }       
        public static ChargeList GetList(IList<Charge> list, bool get_childs)
        {
            ChargeList flist = new ChargeList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (Charge item in list)
                    flist.AddItem(item.GetInfo(get_childs));

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
        public static SortedBindingList<ChargeInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<ChargeInfo> sortedList = new SortedBindingList<ChargeInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<ChargeInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<ChargeInfo> sortedList = new SortedBindingList<ChargeInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        #endregion

		#region Child Factory Methods

		private ChargeList(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}

        /// <summary>
        /// Default call for GetChildList(bool childs)
        /// </summary>
        /// <returns></returns>
        public static ChargeList GetChildList() { return ChargeList.GetChildList(true); }
		public static ChargeList GetChildList(IList<ChargeInfo> list)
		{
			ChargeList flist = new ChargeList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (ChargeInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}
		public static ChargeList GetChildList(IList<Charge> list)
		{
			ChargeList flist = new ChargeList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (Charge item in list)
					flist.AddItem(item.GetInfo(true));

				flist.IsReadOnly = true;
			}
			
			return flist;
		}
        public static ChargeList GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static ChargeList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new ChargeList(sessionCode, reader, childs); }
       
        public static ChargeList GetChildList(ClienteInfo parent, bool childs)
        {
            CriteriaEx criteria = Charge.GetCriteria(Charge.OpenSession());

            criteria.Query = ChargeList.SELECT(parent);
            criteria.Childs = childs;

            ChargeList list = DataPortal.Fetch<ChargeList>(criteria);
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

			IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

            try
            {
                if (nHMng.UseDirectSQL)
                {   
                    IsReadOnly = false;

                    while (reader.Read())
                        this.AddItem(ChargeInfo.GetChild(reader, Childs));

                    IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Charge.SELECT_COUNT(criteria), criteria.Session);
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
        protected void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IsReadOnly = false;

                while (reader.Read())
                    this.AddItem(ChargeInfo.GetChild(reader,Childs));

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
        public static string SELECT(Library.Invoice.QueryConditions conditions) { return Charge.SELECT(conditions, false); }
		public static string SELECT(ClienteInfo source) 
		{
			QueryConditions conditions = new QueryConditions
			{
				Cliente = source,
				TipoCobro = ETipoCobro.Cliente
			};
			return Charge.SELECT(conditions , false); 
		}
		public static string SELECT_PENDIENTES(Library.Invoice.QueryConditions conditions) { return Charge.SELECT_PENDIENTES(conditions, false); }
		public static string SELECT_NEGOCIADOS(Library.Invoice.QueryConditions conditions) { return Charge.SELECT_NEGOCIADOS(conditions, false); }

        #endregion
    }
}