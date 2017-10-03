using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Renting;
using moleQule.WebFace.Models;

namespace moleQule.WebFace.Invoice.Models
{
	/// <summary>
	/// ViewModel
	/// </summary>
	[Serializable()]
	public class TransactionViewModel : ViewModelBase<Transaction, TransactionInfo>, IViewModel
	{
		#region Attributes

		protected TransactionBase _base = new TransactionBase();

		#endregion
		
		#region Properties

		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }

		[HiddenInput]
		public long TransactionViewModelID { get { return Oid; } set { Oid = value; } }

		[HiddenInput]
		public long OidEntity{ get { return _base.Record.OidEntity; } set { _base.Record.OidEntity= value; } }

		public long EntityType{ get { return _base.Record.EntityType; } set { _base.Record.EntityType= value; } }

		public long Serial{ get { return _base.Record.Serial; } set { _base.Record.Serial= value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "ID")]
		public string Code{ get { return _base.Record.Code; } set { _base.Record.Code= value; } }

		public long Type{ get { return _base.Record.Type; } set { _base.Record.Type= value; } }

		public long Status{ get { return _base.Record.Status; } set { _base.Record.Status= value; } }

		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)][Display(ResourceType = typeof(Resources.Labels), Name = "CREATED")]
		public DateTime Created{ get { return _base.Record.Created; } set { _base.Record.Created= value; } }

		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)][Display(ResourceType = typeof(Resources.Labels), Name = "RESOLVED")]
		public DateTime Resolved{ get { return _base.Record.Resolved; } set { _base.Record.Resolved= value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "TRANSACTION_ID")]
		public string TransactionID{ get { return _base.Record.TransactionID; } set { _base.Record.TransactionID= value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "TRANSACTION_ID_EXT")]
		public string TransactionIDExt { get { return _base.Record.TransactionIDExt; } set { _base.Record.TransactionIDExt = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "AUTHCODE")]
		public string AuthCode{ get { return _base.Record.AuthCode; } set { _base.Record.AuthCode= value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "PANMASK")]
		public string PanMask{ get { return _base.Record.PanMask; } set { _base.Record.PanMask= value; } }

		[DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(Resources.Labels), Name = "AMOUNT")]
		public Decimal Amount{ get { return _base.Record.Amount; } set { _base.Record.Amount= value; } }

		public long Currency{ get { return _base.Record.Currency; } set { _base.Record.Currency= value; } }

		public long Gateway{ get { return _base.Record.Gateway; } set { _base.Record.Gateway= value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "DESCRIPTION")]
		public string Description { get { return _base.Record.Description; } set { _base.Record.Description= value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "RESPONSE")]
		public string Response { get { return _base.Record.Response; } set { _base.Record.Response= value; } }

		public virtual EEstado EStatus { get { return _base.EStatus; } set { } }

  		[Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return _base.StatusLabel; } set { } }

		public virtual ETipoEntidad EEntityType { get { return _base.EEntityType; } set { EntityType = (long)value; } }

		public virtual string EntityTypeLabel { get { return _base.EntityTypeLabel; } }

		public virtual EPaymentGateway EPaymentGateway { get { return _base.EPaymentGateway; } set { Gateway = (long)value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "GATEWAY")]
		public virtual string PaymentGatewayLabel { get { return _base.PaymentGatewayLabel; } }

		public virtual ECurrency ECurrency { get { return _base.ECurrency; } set { Currency = (long)value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "CURRENCY")]
		public virtual string CurrencyLabel { get { return _base.CurrencyLabel; } }

		public virtual ETransactionType ETransactionType { get { return _base.ETransactionType; } set { Type = (long)value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "TRANSACTION_TYPE")]
		public virtual string TransactionTypeLabel { get { return _base.TransactionTypeLabel; } }
		
		#endregion
		
		#region Business Methods
		
		public new void CopyFrom(Transaction source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyFrom(TransactionInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyTo(Transaction dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);
		}

		#endregion
		
		#region Factory Methods

		public TransactionViewModel() { }

		public static TransactionViewModel New() 
		{
			TransactionViewModel obj = new TransactionViewModel();
			obj.CopyFrom(Transaction.New().GetInfo());
			return obj;
		}
		public static TransactionViewModel New(Transaction source) { return New(source.GetInfo(false)); }
		public static TransactionViewModel New(TransactionInfo source)
		{
			TransactionViewModel obj = new TransactionViewModel();
			obj.CopyFrom(source);
			return obj;
		}

		public static TransactionViewModel Get(long oid)
		{
			TransactionViewModel obj = new TransactionViewModel();
			obj.CopyFrom(TransactionInfo.Get(oid, false));
			return obj;
		}

		public static void Add(TransactionViewModel item)
		{
			Transaction newItem = Transaction.New();
			item.CopyTo(newItem);
			newItem.Save();
		}
		public static void Edit(TransactionViewModel source, HttpRequestBase request = null)
		{
			Transaction item = Transaction.Get(source.Oid);
			source.CopyTo(item, request);
			item.Save();
		}
		public static void Remove(long oid)
		{
			Transaction.Delete(oid);
		}

		#endregion
	}

	/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class TransactionListViewModel : List<TransactionViewModel>
	{
		#region Properties

		protected PagingInfo PagingInfo { get; set; }

		#endregion

		#region Business Objects

		#endregion

		#region Factory Methods

		public TransactionListViewModel() { }

		public static TransactionListViewModel Get()
		{
			TransactionListViewModel list = new TransactionListViewModel();

			TransactionList sourceList = TransactionList.GetList();

			foreach (TransactionInfo item in sourceList)
				list.Add(TransactionViewModel.New(item));

			return list;
		}
		public static TransactionListViewModel Get(TransactionList sourceList)
		{
			TransactionListViewModel list = new TransactionListViewModel();

			foreach (TransactionInfo item in sourceList)
				list.Add(TransactionViewModel.New(item));

			return list;
		}

		#endregion
	}
}
