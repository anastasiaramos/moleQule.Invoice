using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
	public class OutputDeliveryViewModel : ViewModelBase<OutputDelivery, OutputDeliveryInfo>, IViewModel
	{
		#region Attributes

        protected OutputDeliveryBase _base = new OutputDeliveryBase();

		#endregion	
	
		#region Properties
	
		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }

		[HiddenInput]
		public long DeliveryViewModelID { get { return Oid; } set { Oid = value; } }		
		
		[HiddenInput]
		public long OidSerie { get { return _base.Record.OidSerie; } set { _base.Record.OidSerie = value; } }
		
		[HiddenInput]
		public long OidHolder { get { return _base.Record.OidHolder; } set { _base.Record.OidHolder = value; } }
		
		[HiddenInput]
		public long OidTransportista { get { return _base.Record.OidTransportista; } set { _base.Record.OidTransportista= value; } }

		[HiddenInput]
		public long OidUsuario { get { return _base.Record.OidUsuario; } set { _base.Record.OidUsuario= value; } }

		[HiddenInput]
		public long OidAlmacen { get { return _base.Record.OidAlmacen; } set { _base.Record.OidAlmacen = value; } }

		[HiddenInput]
		public long OidExpediente { get { return _base.Record.OidExpediente; } set { _base.Record.OidExpediente= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "ID")]
		public string Codigo { get { return _base.Record.Codigo; } set { _base.Record.Codigo = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "DATE")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime Fecha { get { return _base.Record.Fecha; } set { _base.Record.Fecha = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "PAYMENT_WAY")]
		public long FormaPago { get { return _base.Record.FormaPago; } set { _base.Record.FormaPago = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "PAY_DAYS")]
		public long DiasPago { get { return _base.Record.DiasPago; } set { _base.Record.DiasPago = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "PAYMENT_METHOD")]
		public long MedioPago { get { return _base.Record.MedioPago; } set { _base.Record.MedioPago = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "DUE_DATE")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime PrevisionPago { get { return _base.Record.PrevisionPago; } set { _base.Record.PrevisionPago = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "TAX_BASE")]
		public Decimal BaseImponible { get { return _base.Record.BaseImponible; } set { _base.Record.BaseImponible = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "TAXES")]
		public Decimal Impuestos { get { return _base.Record.Impuestos; } set { _base.Record.Impuestos= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "DISCOUNT")]
		public Decimal PDescuento { get { return _base.Record.PDescuento; } set { _base.Record.PDescuento = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "DISCOUNT_PERCENT")]
		public Decimal Descuento { get { return _base.Record.Descuento; } set { _base.Record.Descuento= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "TOTAL")]
		public Decimal Total { get { return _base.Record.Total; } set { _base.Record.Total = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "BANK_ACCOUNT")]
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } set { _base.Record.CuentaBancaria = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "NOTE")]
		public bool Nota { get { return _base.Record.Nota; } set { _base.Record.Nota = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "COMMENTS")]
		[UIHint("MultilineText")]
		public string Observaciones { get { return _base.Record.Observaciones; } set { _base.Record.Observaciones= value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "GROUPPED")]
		public bool Agrupado { get { return _base.Record.Contado; } set { _base.Record.Contado = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "RECTIFICATION_INVOICE")]
		public bool Rectificativa { get { return _base.Record.Rectificativo; } set { _base.Record.Rectificativo = value; } }

		[HiddenInput]
		public long Status { get { return _base.Record.Estado; } set { _base.Record.Estado = value; } }

		//UNLINKED PROPERTIES
		public virtual EEstado EStatus { get { return _base.EStatus; } set { _base.Record.Estado = (long)value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return _base.StatusLabel; } set { } }

		public virtual bool IsBilled { get { return _base._facturado; } set { _base._facturado = value; } }

		public virtual OutputDeliveryLineListViewModel Lines { get; set; }
		
		#endregion
		
		#region Business Methods
		
		public new void CopyFrom(OutputDelivery source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyFrom(OutputDeliveryInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyTo(OutputDelivery dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);
			dest.EEstado = EStatus;			
		}
			
		#endregion		
		
		#region Factory Methods

		public OutputDeliveryViewModel() { }

		public static OutputDeliveryViewModel New() 
		{
			OutputDeliveryViewModel obj = new OutputDeliveryViewModel();
			obj.CopyFrom(OutputDeliveryInfo.New());
			obj.Lines = new OutputDeliveryLineListViewModel();

			return obj;
		}
		public static OutputDeliveryViewModel New(OutputDelivery  source) { return New(source.GetInfo(false)); }
		public static OutputDeliveryViewModel New(OutputDeliveryInfo source)
		{
			OutputDeliveryViewModel obj = new OutputDeliveryViewModel();
			obj.CopyFrom(source);
			return obj;
		}
		
		public static OutputDeliveryViewModel Get(long oid, bool childs = false)
		{
			OutputDeliveryViewModel obj = new OutputDeliveryViewModel();
			OutputDeliveryInfo delivery = OutputDeliveryInfo.Get(oid, ETipoEntidad.Cliente, childs);
			
			if (delivery == null) return null;

			obj.CopyFrom(delivery);

			if (childs)
				obj.Lines = OutputDeliveryLineListViewModel.Get(delivery.Conceptos);

			return obj;
		}

		public static void Add(OutputDeliveryViewModel item)
		{
			OutputDelivery newItem = OutputDelivery.New();
			item.CopyTo(newItem);
			newItem.Save();
			item.CopyFrom(newItem);
		}
		public static void Edit(OutputDeliveryViewModel source, HttpRequestBase request = null)
		{
			OutputDelivery item = OutputDelivery.Get(source.Oid, ETipoEntidad.Cliente);
			source.CopyTo(item, request);
			item.Save();
		}
		public static void Remove(long oid)
		{
            OutputDelivery.Delete(oid, ETipoEntidad.Cliente);
		}
		
		#endregion
	}	
	
		/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class OutputDeliveryListViewModel : List<OutputDeliveryViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public OutputDeliveryListViewModel() { }

		public static OutputDeliveryListViewModel Get()
		{
			OutputDeliveryListViewModel list = new OutputDeliveryListViewModel();

            OutputDeliveryList sourceList = OutputDeliveryList.GetList(ETipoEntidad.Cliente);

            foreach (OutputDeliveryInfo item in sourceList)
				list.Add(OutputDeliveryViewModel.New(item));

			return list;
		}
		public static OutputDeliveryListViewModel Get(OutputDeliveryList sourceList)
		{
			OutputDeliveryListViewModel list = new OutputDeliveryListViewModel();

			foreach (OutputDeliveryInfo item in sourceList)
				list.Add(OutputDeliveryViewModel.New(item));

			return list;
		}

		#endregion
	}
}
