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
	/// ReadOnly Business Object Root Collection
	/// </summary>
    [Serializable()]
	public class BankLineList : ReadOnlyListBaseEx<BankLineList, BankLineInfo>
	{	
		#region Business Methods

		public BankLineInfo GetItemByOperacion(long oid, EBankLineType bankLineType)
		{
            return this.FirstOrDefault<BankLineInfo>(item => (item.OidOperacion == oid && item.ETipoMovimientoBanco == bankLineType));
		}

		public void UpdateSaldoDesc()
		{
			if (Items.Count == 0) return;

            BankAccountList cuentas = BankAccountList.GetList(false);
            BankAccountInfo cuenta;

            foreach (BankAccountInfo item in cuentas)
				item.SaldoParcial = item.Saldo;

			foreach (BankLineInfo item in this)
			{
				if (item.EEstado == EEstado.Anulado)
				{
					item.Saldo = 0;
				}
				else
				{
					cuenta = cuentas.GetItem(item.OidCuenta);
					item.Saldo = (cuenta != null) ? cuenta.SaldoParcial : item.Importe;
					if (cuenta != null) cuenta.SaldoParcial -= item.Importe;
				}
			}
		}

		public void UpdateSaldo()
		{
			if (Items.Count == 0) return;

            BankAccountList cuentas = BankAccountList.GetList(false);
            BankAccountInfo cuenta;

            foreach (BankAccountInfo item in cuentas)
				item.SaldoParcial = item.SaldoInicial;

			foreach (BankLineInfo item in Items.Reverse())
			{
				if (item.EEstado == EEstado.Anulado)
				{
					item.Saldo = 0;
				}
				else
				{
					cuenta = cuentas.GetItem(item.OidCuenta);
					if (cuenta != null) cuenta.SaldoParcial += item.Importe;
					item.Saldo = (cuenta != null) ? cuenta.SaldoParcial : item.Importe;
				}
			}
		}

        public void UpdateSaldos(BankLineList list)
        {
            foreach (BankLineInfo item in this)
                item.Saldo = list.GetItem(item.Oid).Saldo;
        }

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private BankLineList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private BankLineList(IList<BankLine> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private BankLineList(IDataReader reader, bool childs)
        {
			Childs = childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private BankLineList(IList<BankLineInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static BankLineList NewList() { return new BankLineList(); }

		public static BankLineList GetList() { return BankLineList.GetList(new Library.Invoice.QueryConditions(), false);	}		
		public static BankLineList GetList(bool childs) { return GetList(new Library.Invoice.QueryConditions(), childs); }
		public static BankLineList GetList(int year, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};

			return GetList(conditions, childs);
		}
		public static BankLineList GetList(DateTime date, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = date,
				FechaFin = date
			};

			return GetList(conditions, childs);
		}

        public static BankLineList GetByCuentaList(BankAccountInfo cuenta, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                CuentaBancaria = cuenta
            };

            return GetList(conditions, childs); 
        }
        public static BankLineList GetByCuentaList(BankAccountInfo cuenta, int year, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                FechaIni = DateAndTime.FirstDay(year),
                FechaFin = DateAndTime.LastDay(year),
                CuentaBancaria = cuenta
            };

            BankLineList list = GetByCuentaList(cuenta, childs);
            BankLineList filtered_list = GetList(conditions, childs);

            filtered_list.UpdateSaldos(list);

            return filtered_list;
        }
        
		public static BankLineList GetByCreditCardList(CreditCardInfo crediCard, DateTime date, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				TarjetaCredito = crediCard,
				TipoMovimientoBanco = EBankLineType.ExtractoTarjeta,
				//MedioPago = EMedioPago.Tarjeta,
				FechaFin = date,
				Estado = EEstado.NoAnulado
			};

            CriteriaEx criteria = BankLine.GetCriteria(BankLine.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = BankLine.SELECT_BY_CREDIT_CARD(conditions, false);

            BankLineList list = DataPortal.Fetch<BankLineList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
		}
		
		public static BankLineList GetList(Library.Invoice.QueryConditions conditions, bool childs)
        {
            CriteriaEx criteria = BankLine.GetCriteria(BankLine.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = BankLineList.SELECT(conditions);

            BankLineList list = DataPortal.Fetch<BankLineList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }
		public static BankLineList GetList(CriteriaEx criteria)
		{
			return BankLineList.RetrieveList(typeof(BankLine), AppContext.ActiveSchema.Code, criteria);
		}
        public static BankLineList GetList(IList<BankLine> list) { return new BankLineList(list,false); }
        public static BankLineList GetList(IList<BankLineInfo> list) { return new BankLineList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<BankLineInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<BankLineInfo> sortedList = new SortedBindingList<BankLineInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<BankLineInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<BankLineInfo> sortedList = new SortedBindingList<BankLineInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<BankLine> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (BankLine item in lista)
				this.AddItem(item.GetInfo(Childs));

			UpdateSaldo();

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
                this.AddItem(BankLineInfo.GetChild(SessionCode, reader, Childs));

			UpdateSaldo();

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
						this.AddItem(BankLineInfo.GetChild(SessionCode, reader, Childs));

					UpdateSaldo();

					IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(BankLine.SELECT_COUNT(criteria), criteria.Session);
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
				
		#endregion
		
        #region SQL

        public static string SELECT() { return BankLineInfo.SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return BankLineInfo.SELECT(conditions); }
		
		#endregion		
	}
}