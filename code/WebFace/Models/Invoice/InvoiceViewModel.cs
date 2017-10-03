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
	public class OutputInvoiceViewModel : ViewModelBase<OutputInvoice, OutputInvoiceInfo>, IViewModel
	{
		#region Attributes

        protected OutputInvoiceBase _base = new OutputInvoiceBase();

		#endregion	
	
		#region Properties
		
		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		
		[HiddenInput]
		public long InvoiceViewModelID { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }		
		
		[HiddenInput]
		public long OidSerie { get { return _base.Record.OidSerie; } set { _base.Record.OidSerie = value; } }
		
		[HiddenInput]
		public long OidCliente { get { return _base.Record.OidCliente; } set { _base.Record.OidCliente = value; } }
		
		[HiddenInput]
		public long OidTransportista { get { return _base.Record.OidTransportista; } set { _base.Record.OidTransportista = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "SERIAL")]
		public long Serial { get { return _base.Record.Serial; } set { _base.Record.Serial = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "ID")]
		public string Codigo { get { return _base.Record.Codigo; } set { _base.Record.Codigo = value; } }

		public virtual EEstado EStatus { get { return _base.EStatus; } set { _base.Record.Estado = (long)value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "VAT_NUMBER")]
		public string VatNumber { get { return _base.Record.VatNumber; } set { _base.Record.VatNumber = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CLIENT")]
		public string Cliente { get { return _base.Record.Cliente; } set { _base.Record.Cliente = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "ADDRESS")]
		public string Direccion { get { return _base.Record.Direccion; } set { _base.Record.Direccion = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "ZIP_CODE")]
		public string CodigoPostal { get { return _base.Record.CodigoPostal; } set { _base.Record.CodigoPostal = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "PROVINCE")]
		public string Provincia { get { return _base.Record.Provincia; } set { _base.Record.Provincia = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "MUNICIPALITY")]
		public string Municipio { get { return _base.Record.Municipio; } set { _base.Record.Municipio = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "YEAR")]
		public long Ano { get { return _base.Record.Ano; } set { _base.Record.Ano = value; } }

        [DataType(DataType.Date)]
		[Display(ResourceType = typeof(Resources.Labels), Name = "DATE")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime Fecha { get { return _base.Record.Fecha; } set { _base.Record.Fecha = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "PAYMENT_METHOD")]
		public long FormaPago { get { return _base.Record.FormaPago; } set { _base.Record.FormaPago = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "PAY_DAYS")]
		public long DiasPago { get { return _base.Record.DiasPago; } set { _base.Record.DiasPago = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "PAYMENT_WAY")]
		public long MedioPago { get { return _base.Record.MedioPago; } set { _base.Record.MedioPago = value; } }

        [DataType(DataType.Date)]
        [Display(ResourceType = typeof(Resources.Labels), Name = "DUE_DATE")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime PrevisionPago { get { return _base.Record.Prevision; } set { _base.Record.Prevision = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "TAX_BASE")]
		public Decimal BaseImponible { get { return _base.Record.BaseImponible; } set { _base.Record.BaseImponible = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "TAXES")]
		public Decimal PIgic { get { return _base.Record.Impuestos; } set { _base.Record.Impuestos = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "DISCOUNT_PERCENT")]
		public Decimal PDescuento { get { return _base.Record.PDescuento; } set { _base.Record.PDescuento = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "DISCOUNT")]
		public Decimal Descuento { get { return _base.Record.Descuento; } set { _base.Record.Descuento = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "TOTAL")]
		public Decimal Total { get { return _base.Record.Total; } set { _base.Record.Total = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "BANK_ACCOUNT")]
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } set { _base.Record.CuentaBancaria = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "NOTE")]
		public bool Nota { get { return _base.Record.Nota; } set { _base.Record.Nota = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "COMMENTS")]
		public string Observaciones { get { return _base.Record.Observaciones; } set { _base.Record.Observaciones = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "DELIVERY")]
		public bool Agrupada { get { return _base.Record.Agrupada; } set { _base.Record.Agrupada = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "RECTIFICATION_INVOICE")]
		public bool Rectificativa { get { return _base.Record.Rectificativa; } set { _base.Record.Rectificativa = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
        public long Estado { get { return _base.Record.Estado; } set { _base.Record.Estado = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "TAXES_PERCENT")]
		public Decimal PIrpf { get { return _base.Record.PIrpf; } set { _base.Record.PIrpf = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "DELIVERY_LINES_LIST")]
		public string Albaranes { get { return _base.Record.Albaranes; } set { _base.Record.Albaranes = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "TRANSACTION_ID")]
		public string IdMovContable { get { return _base.Record.IdMovContable; } set { _base.Record.IdMovContable = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "CHARGE_STATUS")]
		public long EstadoCobro { get { return _base.Record.EstadoCobro; } set { _base.Record.EstadoCobro = value; } }
		
		[HiddenInput]
		public long OidUsuario { get { return _base.Record.OidUsuario; } set { _base.Record.OidUsuario = value; } }

        public OutputInvoiceLineListViewModel Lines { get; set; }

		//UNLINKED PROPERTIES

        [HiddenInput]
        public long Status { get { return Estado; } set { Estado = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return _base.StatusLabel; } set { } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "USER")]
		public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "SERIE")]
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "DUE_OF_PAYMENT")]
		public virtual decimal Pendiente { get { return _base.Pendiente; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "PAYMENT_WAY")]
        public EMedioPago MedioPagoList { get; set; }

		#endregion
		
		#region Business Methods
		
		public new void CopyFrom(OutputInvoice source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyFrom(OutputInvoiceInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyTo(OutputInvoice dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);
		}
			
		#endregion		
		
		#region Factory Methods

		public OutputInvoiceViewModel() { }

		public static OutputInvoiceViewModel New() 
		{
			OutputInvoiceViewModel obj = new OutputInvoiceViewModel();
			obj.CopyFrom(OutputInvoiceInfo.New());
			return obj;
		}
		public static OutputInvoiceViewModel New(OutputInvoice source) { return New(source.GetInfo(false)); }
		public static OutputInvoiceViewModel New(OutputInvoiceInfo source)
		{
			OutputInvoiceViewModel obj = new OutputInvoiceViewModel();
			obj.CopyFrom(source);
			return obj;
		}
		
		public static OutputInvoiceViewModel Get(long oid)
		{
			OutputInvoiceViewModel obj = new OutputInvoiceViewModel();
			obj.CopyFrom(OutputInvoiceInfo.Get(oid, false));
			return obj;
		}
        public static OutputInvoiceViewModel Get(long oid, bool childs = false)
        {
            OutputInvoiceViewModel obj = new OutputInvoiceViewModel();
            OutputInvoiceInfo invoice = OutputInvoiceInfo.Get(oid, childs);

            if (invoice == null) return null;

            obj.CopyFrom(invoice);

            if (childs)
                obj.Lines = OutputInvoiceLineListViewModel.Get(invoice.ConceptoFacturas);

            return obj;
        }

		public static void Add(OutputInvoiceViewModel item)
		{
			OutputInvoice newItem = OutputInvoice.New();
			item.CopyTo(newItem);
			newItem.Save();
			item.CopyFrom(newItem);
		}
		public static void Edit(OutputInvoiceViewModel source)
		{
			OutputInvoice item = OutputInvoice.Get(source.Oid);
			source.CopyTo(item);
			item.Save();
		}
		public static void Remove(long oid)
		{
			OutputInvoice.Delete(oid);
		}
		
		#endregion
	}	
	
		/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class OutputInvoiceListViewModel : List<OutputInvoiceViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public OutputInvoiceListViewModel() { }

		public static OutputInvoiceListViewModel Get()
		{
			OutputInvoiceListViewModel list = new OutputInvoiceListViewModel();

			OutputInvoiceList sourceList = OutputInvoiceList.GetList();

			foreach (OutputInvoiceInfo item in sourceList)
				list.Add(OutputInvoiceViewModel.New(item));

			return list;
		}
		public static OutputInvoiceListViewModel Get(OutputInvoiceList sourceList)
		{
			OutputInvoiceListViewModel list = new OutputInvoiceListViewModel();

			foreach (OutputInvoiceInfo item in sourceList)
				list.Add(OutputInvoiceViewModel.New(item));

			return list;
		}

		#endregion
	}
}
