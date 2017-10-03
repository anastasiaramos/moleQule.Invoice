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
	public class ChargeViewModel : ViewModelBase<Cobro, CobroInfo>, IViewModel
	{
		#region Attributes

		protected ChargeBase _base = new ChargeBase();

		
		
		#endregion	
	
		#region Properties
		
		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		
		[HiddenInput]
		public long ChargeViewModelID { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }		
		
		[HiddenInput]
		public long OidCliente { get { return _base.Record.OidCliente; } set { _base.Record.OidCliente = value; } }
		
		[HiddenInput]
		public int OidUsuario { get { return _base.Record.OidUsuario; } set { _base.Record.OidUsuario = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CHARGE_ID")]
		public long IdCobro { get { return _base.Record.IdCobro; } set { _base.Record.IdCobro = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CHARGE_TYPE")]
		public int TipoCobro { get { return _base.Record.TipoCobro; } set { _base.Record.TipoCobro = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "DATE")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get { return _base.Record.Fecha; } set { _base.Record.Fecha = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CHARGE_AMOUNT")]
		public Decimal Importe { get { return _base.Record.Importe; } set { _base.Record.Importe = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "PAYMENT_WAY")]
		public long MedioPago { get { return _base.Record.MedioPago; } set { _base.Record.MedioPago = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "EXPIRATION_DATE")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Vencimiento { get { return _base.Record.Vencimiento; } set { _base.Record.Vencimiento = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "OBSERVACIONES")]
		public string Observaciones { get { return _base.Record.Observaciones; } set { _base.Record.Observaciones = value; } }
		
		[HiddenInput]
		public long OidCuentaBancaria { get { return _base.Record.OidCuentaBancaria; } set { _base.Record.OidCuentaBancaria = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "SERIAL")]
		public long Serial { get { return _base.Record.Serial; } set { _base.Record.Serial = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CODIGO")]
		public string Codigo { get { return _base.Record.Codigo; } set { _base.Record.Codigo = value; } }
		
		[HiddenInput]
		public long OidTpv { get { return _base.Record.OidTpv; } set { _base.Record.OidTpv = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
		public long Estado { get { return _base.Record.Estado; } set { _base.Record.Estado = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CHARGE_STATUS")]
		public long EstadoCobro { get { return _base.Record.EstadoCobro; } set { _base.Record.EstadoCobro = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "ACCOUNTANT_MOV_ID")]
		public string IdMovContable { get { return _base.Record.IdMovContable; } set { _base.Record.IdMovContable = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "BANK_CHARGES")]
		public Decimal GastosBancarios { get { return _base.Record.GastosBancarios; } set { _base.Record.GastosBancarios = value; } }		
		
		//UNLINKED PROPERTIES
        [HiddenInput]
        public long Status { get { return Estado; } set { Estado = value; } }

		public virtual EEstado EStatus { get { return _base.EStatus; } set { _base.Record.Estado = (long)value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return _base.StatusLabel; } set { } }

		public virtual EEstado EEstadoCobro { get { return _base.EEstadoCobro; } set { _base.EEstadoCobro = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
		public virtual string EstadoCobroLabel { get { return _base.EstadoCobroLabel; } set { } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CLIENT")]
		public virtual string Cliente { get { return _base.Cliente; } set { } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "PAYMENT_WAY")]
		public virtual string MedioPagoLabel { get { return _base.EMedioPagoLabel; } set { } }

		#endregion
		
		#region Business Methods
		
		public new void CopyFrom(Cobro source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyFrom(CobroInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyTo(Cobro dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);
		}
			
		#endregion		
		
		#region Factory Methods

		public ChargeViewModel() { }

		public static ChargeViewModel New() 
		{
			ChargeViewModel obj = new ChargeViewModel();
            obj.CopyFrom(CobroInfo.New());
			return obj;
		}
        public static ChargeViewModel New(Cobro source) { return New(source.GetInfo(false)); }
        public static ChargeViewModel New(CobroInfo source)
		{
			ChargeViewModel obj = new ChargeViewModel();
			obj.CopyFrom(source);
			return obj;
		}
		
		public static ChargeViewModel Get(long oid)
		{
			ChargeViewModel obj = new ChargeViewModel();
            obj.CopyFrom(CobroInfo.Get(oid, false));
			return obj;
		}

		public static void Add(ChargeViewModel item)
		{
            Cobro newItem = Cobro.New();
			item.CopyTo(newItem);
			newItem.Save();
			item.CopyFrom(newItem);
		}
		public static void Edit(ChargeViewModel source, HttpRequestBase request = null)
		{
            Cobro item = Cobro.Get(source.Oid);
			source.CopyTo(item, request);
			item.Save();
		}
		public static void Remove(long oid)
		{
            Cobro.Delete(oid);
		}
		
		#endregion
	}	
	
		/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class ChargeListViewModel : List<ChargeViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public ChargeListViewModel() { }

		public static ChargeListViewModel Get()
		{
			ChargeListViewModel list = new ChargeListViewModel();

            CobroList sourceList = CobroList.GetList();

			foreach (CobroInfo item in sourceList)
				list.Add(ChargeViewModel.New(item));

			return list;
		}
        public static ChargeListViewModel Get(CobroList sourceList)
		{
			ChargeListViewModel list = new ChargeListViewModel();

            foreach (CobroInfo item in sourceList)
				list.Add(ChargeViewModel.New(item));

			return list;
		}

		#endregion
	}
}
