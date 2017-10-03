using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class TransactionInfo : ReadOnlyBaseEx<TransactionInfo>
	{	
		#region Attributes

		protected TransactionBase _base = new TransactionBase();
		
		#endregion
		
		#region Properties

		public TransactionBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidEntity { get { return _base.Record.OidEntity; } }
		public long EntityType { get { return _base.Record.EntityType; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Code { get { return _base.Record.Code; } }
		public long Type { get { return _base.Record.Type; } set { _base.Record.Type = value; } }
		public long Status { get { return _base.Record.Status; } }
		public DateTime Created { get { return _base.Record.Created; } }
		public DateTime Resolved { get { return _base.Record.Resolved; } set { _base.Record.Resolved = value; } }
		public string TransactionID { get { return _base.Record.TransactionID; } set { _base.Record.TransactionID = value; } }
		public string TransactionIDExt { get { return _base.Record.TransactionIDExt; } set { _base.Record.TransactionIDExt = value; } }
		public string AuthCode { get { return _base.Record.AuthCode; } set { _base.Record.AuthCode = value; } }
		public string PanMask { get { return _base.Record.PanMask; } set { _base.Record.PanMask = value; } }
		public Decimal Amount { get { return _base.Record.Amount; } set { _base.Record.Amount = value; } }
		public long Currency { get { return _base.Record.Currency; } }
		public long Gateway { get { return _base.Record.Gateway; } }
		public string Description { get { return _base.Record.Description; } }
		public string Response { get { return _base.Record.Response; } set { _base.Record.Response = value; } }
		
		//LINKED
		public virtual EEstado EStatus { get { return _base.EStatus; } set { _base.Record.Status = (long)value; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
		public virtual ETipoEntidad EEntityType { get { return _base.EEntityType; } }
		public virtual string EntityTypeLabel { get { return _base.EntityTypeLabel; } }
		public virtual EPaymentGateway EPaymentGateway { get { return _base.EPaymentGateway; } }
		public virtual string PaymentGatewayLabel { get { return _base.PaymentGatewayLabel; } }
		public virtual ECurrency ECurrency { get { return _base.ECurrency; } }
		public virtual string CurrencyLabel { get { return _base.CurrencyLabel; } }
		public virtual ETransactionType ETransactionType { get { return _base.ETransactionType; } set { _base.Record.Type = (long)value; } }
		public virtual string TransactionTypeLabel { get { return _base.TransactionTypeLabel; } }
		
		#endregion
		
		#region Business Methods
				
		public void CopyFrom(Transaction source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected TransactionInfo() { /* require use of factory methods */ }
		private TransactionInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal TransactionInfo(Transaction item, bool childs)
		{
			_base.CopyValues(item);

			if (childs)
			{				
			}
		}

		public static TransactionInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static TransactionInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new TransactionInfo(sessionCode, reader, childs);
		}

		public static TransactionInfo New(long oid = 0) { return new TransactionInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
        public static TransactionInfo Get(long oid) { return Get(oid, false); }
		public static TransactionInfo Get(long oid, bool childs) { return Get(SELECT(oid), childs); }

		private static TransactionInfo Get(string query, bool childs)
		{
			CriteriaEx criteria = Invoice.Transaction.GetCriteria(Invoice.Transaction.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			TransactionInfo obj = DataPortal.Fetch<TransactionInfo>(criteria);
			Invoice.Transaction.CloseSession(criteria.SessionCode);
			return obj;
		}

		public static TransactionInfo GetBySubscription(long oidSuscription, ETransactionType transType, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Oid = oidSuscription,
				EntityType = ETipoEntidad.Subscription,
				Transaction = TransactionInfo.New()
			};

			conditions.Transaction.ETransactionType = transType;

			return Get(SELECT(conditions), childs);
		}
		public static TransactionInfo GetByTransactionID(string transactionID, ETipoEntidad entityType, ETransactionType transType, EEstado[] status, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				EntityType = ETipoEntidad.Subscription,
				Transaction = TransactionInfo.New(),
				Status = status,
			};
			conditions.Transaction.TransactionID = transactionID;
			conditions.Transaction.ETransactionType = transType;

			return Get(SELECT(conditions), childs);
		}
		public static TransactionInfo GetLastBySubscription(long oidSuscription, ETransactionType transType, EEstado[] status, bool childs)
		{
			CriteriaEx criteria = new CriteriaEx();
			criteria.Orders = new OrderList();
			criteria.Orders.NewOrder("Created", ListSortDirection.Descending, typeof(Transaction));
	
			TransactionList list = TransactionList.GetBySubscription(oidSuscription, transType, status, criteria, childs);

			return (list.Count > 0) ? list[0] : null;
		}

		#endregion
					
		#region Common Data Access
								
        /// <summary>
        /// Obtiene un objeto a partir de un <see cref="IDataReader"/>.
        /// Obtiene los hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria"><see cref="IDataReader"/> con los datos</param>
        /// <remarks>
        /// La utiliza el <see cref="ReadOnlyListBaseEx"/> correspondiente para construir los objetos de la lista
        /// </remarks>
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
		#region Root Data Access
		 
        /// <summary>
        /// Obtiene un registro de la base de datos
        /// </summary>
        /// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
        /// <remarks>
        /// La llama el DataPortal
        /// </remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;
			
			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);					
				}
			}
			catch (Exception ex) { iQExceptionHandler.TreatException(ex, new object[] { criteria.Query }); }
		}
		
		#endregion
					
        #region SQL

        public static string SELECT(long oid) { return Invoice.Transaction.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return Invoice.Transaction.SELECT(conditions, false); }
		
        #endregion		
	}
}
