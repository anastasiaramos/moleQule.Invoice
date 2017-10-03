using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.WebFace.Models;

namespace moleQule.WebFace.Invoice.Models
{
	/// <summary>
	/// ViewModel
	/// </summary>
	[Serializable()]
	public class OutputDeliveryLineViewModel : ViewModelBase<OutputDeliveryLine, OutputDeliveryLineInfo>, IViewModel
	{
		#region Attributes

        protected OutputDeliveryLineBase _base = new OutputDeliveryLineBase();

		#endregion	
	
		#region Properties
	
		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }

		[HiddenInput]
		public long DeliveryLineViewModelID { get { return Oid; } set { Oid = value; } }		
		
		[HiddenInput]
		public long OidDelivery { get { return _base.Record.OidAlbaran; } set { _base.Record.OidAlbaran= value; } }
		
		[HiddenInput]
		public long OidBatch { get { return _base.Record.OidPartida; } set { _base.Record.OidPartida= value; } }
		
		[HiddenInput]
		public long OidExpedient { get { return _base.Record.OidExpediente; } set { _base.Record.OidExpediente= value; } }
		
		[HiddenInput]
		public long OidProduct { get { return _base.Record.OidProducto; } set { _base.Record.OidProducto= value; } }
		
		[HiddenInput]
		public long OidKit { get { return _base.Record.OidKit; } set { _base.Record.OidKit = value; } }

		[HiddenInput]
		public long OidTax { get { return _base.Record.OidImpuesto; } set { _base.Record.OidImpuesto= value; } }

		[HiddenInput]
		public long OidOrderLine { get { return _base.Record.OidLineaPedido; } set { _base.Record.OidLineaPedido= value; } }

		[HiddenInput]
		public long OidStore { get { return _base.Record.OidAlmacen; } set { _base.Record.OidAlmacen = value; } }	

		[Display(ResourceType = typeof(Resources.Labels), Name = "CONCEPT")]
		public string Concept { get { return _base.Record.Concepto; } set { _base.Record.Concepto= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "IS_PACK")]
		public bool IsPack { get { return _base.Record.FacturacionBulto; } set { _base.Record.FacturacionBulto= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "KG_AMOUNT")]
		public Decimal KgAmount { get { return _base.Record.CantidadKilos; } set { _base.Record.CantidadKilos = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "AMOUNT")]
		public Decimal Amount { get { return Math.Round(_base.Record.CantidadBultos, 2); } set { _base.Record.CantidadBultos= value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "PRICE")]
		public Decimal Price { get { return Math.Round(_base.Record.Precio, 2); } set { _base.Record.Precio= value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "TAXES_PERCENT")]
		public Decimal PTax { get { return _base.Record.PImpuestos; } set { _base.Record.PImpuestos= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "DISCOUNT_PERCENT")]
		public Decimal PDiscount { get { return _base.Record.PDescuento; } set { _base.Record.PDescuento= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "GROSS")]
		public Decimal Gross { get { return _base.Record.Subtotal; } set { _base.Record.Subtotal= value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "TOTAL")]
		public Decimal Total { get { return _base.Record.Total; } set { _base.Record.Total= value; } }
		
		//UNLINKED PROPERTIES
		public virtual long Status { get { return (long)EStatus; } set { } }

		public virtual EEstado EStatus { get { return EEstado.Active; } set { } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(Status); } set { } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "TAXES")]
		public Decimal Taxes { get { return Math.Round(_base.Impuestos, 2); } set { } }

		#endregion
		
		#region Business Methods
		
		public new void CopyFrom(OutputDeliveryLine source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyFrom(OutputDeliveryLineInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);			
		}
		public new void CopyTo(OutputDeliveryLine dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);

			dest.OidImpuesto = OidTax;
			dest.Concepto = Concept;
			dest.CantidadKilos = KgAmount;
			dest.CantidadBultos = Amount;
			dest.Precio = Price;
			dest.PDescuento = PDiscount;
			dest.PImpuestos = PTax;
			dest.Subtotal = Gross;
			dest.FacturacionBulto = IsPack;
		}
			
		#endregion		
		
		#region Factory Methods

		public OutputDeliveryLineViewModel() { }

		public static OutputDeliveryLineViewModel New() 
		{
			OutputDeliveryLineViewModel obj = new OutputDeliveryLineViewModel();
			obj.CopyFrom(OutputDeliveryLine.NewChild().GetInfo(false));
			return obj;
		}
		public static OutputDeliveryLineViewModel New(OutputDeliveryLine source) { return New(source.GetInfo(false)); }
		public static OutputDeliveryLineViewModel New(OutputDeliveryLineInfo source)
		{
			OutputDeliveryLineViewModel obj = new OutputDeliveryLineViewModel();
			obj.CopyFrom(source);
			return obj;
		}
		
		public static OutputDeliveryLineViewModel Get(long oid)
		{
			OutputDeliveryLineViewModel obj = new OutputDeliveryLineViewModel();
			obj.CopyFrom(OutputDeliveryLineInfo.Get(oid, false));
			return obj;
		}

		public static void Add(OutputDeliveryLineViewModel item)
		{
			OutputDeliveryLine newItem = OutputDeliveryLine.NewChild();
			item.CopyTo(newItem);
			newItem.Save();
			item.CopyFrom(newItem);
		}
		public static void Edit(OutputDeliveryLineViewModel source)
		{
			/*OutputDeliveryLine item = OutputDeliveryLine.Get(source.Oid);
			source.CopyTo(item);
			item.Save();*/
		}
		public static void Remove(long oid)
		{
			/*OutputDeliveryLine.Delete(oid);*/
		}
		
		#endregion
	}	
	
	/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class OutputDeliveryLineListViewModel : List<OutputDeliveryLineViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public OutputDeliveryLineListViewModel() { }

		public static OutputDeliveryLineListViewModel Get()
		{
			OutputDeliveryLineListViewModel list = new OutputDeliveryLineListViewModel();

			OutputDeliveryLineList sourceList = OutputDeliveryLineList.GetList();

			foreach (OutputDeliveryLineInfo item in sourceList)
				list.Add(OutputDeliveryLineViewModel.New(item));

			return list;
		}
		public static OutputDeliveryLineListViewModel Get(OutputDeliveryLineList sourceList)
		{
			OutputDeliveryLineListViewModel list = new OutputDeliveryLineListViewModel();

			foreach (OutputDeliveryLineInfo item in sourceList)
				list.Add(OutputDeliveryLineViewModel.New(item));

			return list;
		}
		public static OutputDeliveryLineListViewModel Get(OutputDeliveryLines sourceList)
		{
			OutputDeliveryLineListViewModel list = new OutputDeliveryLineListViewModel();

			foreach (OutputDeliveryLine item in sourceList)
				list.Add(OutputDeliveryLineViewModel.New(item));

			return list;
		}

		#endregion

		#region Business Methods

		public OutputDeliveryLineViewModel GetItem(long oid)
		{
			return this.FirstOrDefault(x => x.Oid == oid);
		}

		#endregion
	}
}
