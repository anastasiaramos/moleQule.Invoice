using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{	
	/// <summary>
	/// ReadOnly Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class LoanList : ReadOnlyListBaseEx<LoanList, LoanInfo>
	{
        #region Business Methods

        public decimal TotalPendiente()
        {
            return Items.Where(x => x.EEstado != EEstado.Anulado).Sum(x => x.Pendiente);       
        }
        public decimal TotalPartialUnpaid()
        {
            return Items.Where(x => x.EEstado != EEstado.Anulado).Sum(x => x.PartialUnpaid);
        }

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private LoanList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private LoanList(IList<Loan> list)
        {
            Fetch(list);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private LoanList(IDataReader reader)
        {
            Fetch(reader);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private LoanList(IList<LoanInfo> list)
        {
            Fetch(list);
        }
		
		#endregion

        #region Root Factory Methods

        public static LoanList NewList() { return new LoanList(); }
        public static LoanList NewList(Loan item) 
        {
            List<Loan> list = new List<Loan>();
            list.Add(item);

            return new LoanList(list);
        }
        public static LoanList NewList(LoanInfo item)
        {
            List<LoanInfo> list = new List<LoanInfo>();
            list.Add(item);

            return new LoanList(list);
        }
		
		public static LoanList GetList()
		{
			CriteriaEx criteria = Loan.GetCriteria(Loan.OpenSession());
			
			//No criteria. Retrieve all de List
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = LoanList.SELECT();
            
			LoanList list = DataPortal.Fetch<LoanList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}		
		public static LoanList GetList(bool cache)
		{
			LoanList list;

			if (!Cache.Instance.Contains(typeof(LoanList)))
			{
				list = LoanList.GetList();
				Cache.Instance.Save(typeof(LoanList), list);
			}
			else
				list = Cache.Instance.Get(typeof(LoanList)) as LoanList;

			return list;
		}
        public static LoanList GetList(string query, bool childs = false)
        {
            CriteriaEx criteria = Loan.GetCriteria(Loan.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

            LoanList list = DataPortal.Fetch<LoanList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }
        public static LoanList GetList(QueryConditions conditions)
        {
            return GetList(SELECT(conditions));
        }
        public static LoanList GetList(ELoanType loanType, bool childs)
        {
            return GetList(loanType, DateTime.MinValue, DateTime.MaxValue, childs);
        }
        public static LoanList GetList(ELoanType loanType, int year, bool childs)
        {
            return GetList(loanType, DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
        }
        public static LoanList GetList(ELoanType loanType, DateTime from, DateTime till, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                FechaIni = from,
                FechaFin = till,
                LoanType = loanType
            };

            return GetList(Loan.SELECT(conditions, false), childs);
        }
		public static LoanList GetList(CriteriaEx criteria)
		{
			return LoanList.RetrieveList(typeof(Loan), AppContext.ActiveSchema.Code, criteria);
		}
        public static LoanList GetList(IList<Loan> list) { return new LoanList(list); }
        public static LoanList GetList(IList<LoanInfo> list) { return new LoanList(list); }

        public static LoanList GetPendientesList()
        {
            return GetPendientesList(null, DateTime.MinValue, DateTime.MaxValue);
        }
        public static LoanList GetPendientesList(LoanInfo loan)
        {
            return GetPendientesList(null, loan);
        }
        public static LoanList GetPendientesList(PaymentInfo payment)
        {
            return GetPendientesList(payment, null);
        }
        public static LoanList GetPendientesList(PaymentInfo payment, LoanInfo loan)
        {
            return GetPendientesList(payment, loan, DateTime.MinValue, DateTime.MaxValue);
        }
        public static LoanList GetPendientesList(DateTime from, DateTime till)
        {
            return GetPendientesList(null, DateTime.MinValue, DateTime.MaxValue);
        }
        public static LoanList GetPendientesList(PaymentInfo payment, DateTime from, DateTime till)
        {
            return GetPendientesList(payment, null, from, till);
        }
        public static LoanList GetPendientesList(PaymentInfo payment, LoanInfo loan, DateTime from, DateTime till)
        {
            QueryConditions conditions = new QueryConditions
            {
                Pago = payment,
                FechaAuxIni = from,
                FechaAuxFin = till,
                Prestamo = loan
            };

            if (conditions.Prestamo == null)
            {
                conditions.Prestamo = Loan.New().GetInfo(false);
                conditions.Prestamo.Oid = payment.OidAgente;
            }

            return GetPendientesList(conditions);
        }
        public static LoanList GetPendientesList(QueryConditions conditions)
        {
            return GetList(SELECT_PENDIENTES(conditions));
        }
        public static LoanList GetByPagoAndPendientesList(PaymentInfo payment)
        {
            return GetByPagoAndPendientesList(payment, null);
        }

        public static LoanList GetByPagoAndPendientesList(PaymentInfo payment, LoanInfo loan)
        {
            LoanList byPago = GetByPagoList(payment, loan);
            LoanList pendientes = loan != null ? GetPendientesList(payment, loan) : GetPendientesList(payment);

            LoanList list = new LoanList();
            list.IsReadOnly = false;

            foreach (LoanInfo item in byPago)
                list.AddItem(item);

            foreach (LoanInfo item in pendientes)
                if (list.GetItem(item.Oid) == null) list.AddItem(item);

            list.IsReadOnly = true;

            return list;
        }
        public static LoanList GetByPagoList(PaymentInfo payment)
        {
            return GetByPagoList(payment, null);
        }
        public static LoanList GetByPagoList(PaymentInfo payment, LoanInfo loan)
        {
            QueryConditions conditions = new QueryConditions { Pago = payment, Prestamo = loan };

            return GetList(SELECT_BY_PAGO(conditions));
        }
        
        public static LoanList GetOrderedByFechaList(QueryConditions conditions)
        {
            conditions.OrderFields = new List<string>();

            conditions.OrderFields.Add("FECHA_INGRESO");
            conditions.OrderFields.Add("CODIGO");

            return GetList(SELECT(conditions));
        }

        public static LoanList GetUnpaidList(ELoanType loanType, DateTime from, DateTime till, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                FechaAuxIni = from,
                FechaAuxFin = till,
                LoanType = loanType
            };

            return GetList(Loan.SELECT_UNPAID(conditions, false), childs);
        }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<LoanInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<LoanInfo> sortedList = new SortedBindingList<LoanInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Loan> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Loan item in lista)
				this.AddItem(item.GetInfo());

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(LoanInfo.GetChild(SessionCode, reader));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion

		#region Root Data Access
		 
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
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
						this.AddItem(LoanInfo.GetChild(SessionCode, reader));

					IsReadOnly = true;
				}
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
			
			this.RaiseListChangedEvents = true;
		}
				
		#endregion
		
        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return Loan.SELECT(conditions, false); }
        public static string SELECT_BY_PAGO(QueryConditions conditions) { return Loan.SELECT_BY_PAGO(conditions, false);}
		public static string SELECT_PENDIENTES(QueryConditions conditions) { return Loan.SELECT_PENDIENTES(conditions, false); }
		
		#endregion		
	}
}