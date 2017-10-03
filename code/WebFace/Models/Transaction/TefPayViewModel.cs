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

namespace moleQule.WebFace.Invoice.Models
{
	/// <summary>
	/// ViewModel
	/// </summary>
	[Serializable()]
	public class TefPayViewModel : TransactionViewModel
	{
		#region Attributes

		#endregion
		
		#region Properties
				
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

		public TefPayViewModel() { }

		public new static TefPayViewModel New() 
		{
			TefPayViewModel obj = new TefPayViewModel();
			obj.CopyFrom(Transaction.New().GetInfo());
			return obj;
		}
		public new static TefPayViewModel New(Transaction source) { return New(source.GetInfo(false)); }
		public new static TefPayViewModel New(TransactionInfo source)
		{
			TefPayViewModel obj = new TefPayViewModel();
			obj.CopyFrom(source);
			return obj;
		}

		public new static TefPayViewModel Get(long oid)
		{
			TefPayViewModel obj = new TefPayViewModel();
			obj.CopyFrom(TransactionInfo.Get(oid, false));
			return obj;
		}

		public static void Add(TefPayViewModel item)
		{
			Transaction newItem = Transaction.New();
			item.CopyTo(newItem);
			newItem.Save();
		}
		public static void Edit(TefPayViewModel source, HttpRequestBase request = null)
		{
			Transaction item = Transaction.Get(source.Oid);
			source.CopyTo(item, request);
			item.Save();
		}
		public new static void Remove(long oid)
		{
			Transaction.Delete(oid);
		}

		#endregion
	}

	/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class TefPayListViewModel : List<TefPayViewModel>
	{
		#region Properties

		protected PagingInfo PagingInfo { get; set; }

		#endregion

		#region Business Objects

		#endregion

		#region Factory Methods

		public TefPayListViewModel() { }

		public static TefPayListViewModel Get()
		{
			TefPayListViewModel list = new TefPayListViewModel();

			TransactionList sourceList = TransactionList.GetList();

			foreach (TransactionInfo item in sourceList)
				list.Add(TefPayViewModel.New(item));

			return list;
		}
		public static TefPayListViewModel Get(TransactionList sourceList)
		{
			TefPayListViewModel list = new TefPayListViewModel();

			foreach (TransactionInfo item in sourceList)
				list.Add(TefPayViewModel.New(item));

			return list;
		}

		#endregion
	}
}
