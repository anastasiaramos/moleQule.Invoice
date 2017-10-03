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
	public class OutputInvoiceLineViewModel : ViewModelBase<OutputInvoiceLine, OutputInvoiceLineInfo>, IViewModel
	{
		#region Attributes

        protected OutputInvoiceLineBase _base = new OutputInvoiceLineBase();

		#endregion	
	
		#region Properties
		
		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		
		[HiddenInput]
		public long OidFactura { get { return _base.Record.OidFactura; } set { _base.Record.OidFactura = value; } }
		
		[HiddenInput]
        public long OidPartida { get { return _base.Record.OidPartida; } set { _base.Record.OidPartida = value; } }
		
		[HiddenInput]
		public long OidExpediente { get { return _base.Record.OidExpediente; } set { _base.Record.OidExpediente = value; } }
		
		[HiddenInput]
		public long OidProducto { get { return _base.Record.OidProducto; } set { _base.Record.OidProducto = value; } }
		
		[HiddenInput]
		public long OidKit { get { return _base.Record.OidKit; } set { _base.Record.OidKit = value; } }
		
		[HiddenInput]
		public long OidConceptoAlbaran { get { return _base.Record.OidConceptoAlbaran; } set { _base.Record.OidConceptoAlbaran = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "CONCEPT")]
		public string Concepto { get { return _base.Record.Concepto; } set { _base.Record.Concepto = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "IS_PACK")]
		public bool FacturacionBulto { get { return _base.Record.FacturacionBulto; } set { _base.Record.FacturacionBulto = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "KG_AMOUNT")]
		public Decimal Cantidad { get { return _base.Record.CantidadKilos; } set { _base.Record.CantidadKilos = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "AMOUNT")]
		public Decimal CantidadBultos { get { return _base.Record.CantidadBultos; } set { _base.Record.CantidadBultos = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "TAXES_PERCENT")]
		public Decimal PIgic { get { return _base.Record.PImpuestos; } set { _base.Record.PImpuestos = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "DISCOUNT_PERCENT")]
		public Decimal PDescuento { get { return _base.Record.PDescuento; } set { _base.Record.PDescuento = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "TOTAL")]
		public Decimal Total { get { return _base.Record.Total; } set { _base.Record.Total = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "PRICE")]
		public Decimal Precio { get { return _base.Record.Precio; } set { _base.Record.Precio = value; } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "GROSS")]
		public Decimal Subtotal { get { return _base.Record.Subtotal; } set { _base.Record.Subtotal = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "EXPENSES")]
		public Decimal Gastos { get { return _base.Record.Gastos; } set { _base.Record.Gastos = value; } }
		
		[HiddenInput]
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } set { _base.Record.OidImpuesto = value; } }
		
		[Display(ResourceType = typeof(Resources.Labels), Name = "CODIGO_PRODUCTO_CLIENTE")]
		public string CodigoProductoCliente { get { return _base.Record.CodigoProductoCliente; } set { _base.Record.CodigoProductoCliente = value; } }
				
		//UNLINKED PROPERTIES
        public virtual long Status { get { return (long)EStatus; } set { } }

        public virtual EEstado EStatus { get { return EEstado.Active; } set { } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
        public virtual string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(Status); } set { } }

        [Display(ResourceType = typeof(Resources.Labels), Name = "EXPEDIENT")]
        public string Expediente { get { return _base.Expediente; } set { _base.Expediente = value; } }
		
		#endregion
		
		#region Business Methods
		
		public new void CopyFrom(OutputInvoiceLine source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyFrom(OutputInvoiceLineInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyTo(OutputInvoiceLine dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);
		}
			
		#endregion		
		
		#region Factory Methods

		public OutputInvoiceLineViewModel() { }

		public static OutputInvoiceLineViewModel New() 
		{
			OutputInvoiceLineViewModel obj = new OutputInvoiceLineViewModel();
			obj.CopyFrom(OutputInvoiceLineInfo.New());
			return obj;
		}
		public static OutputInvoiceLineViewModel New(OutputInvoiceLine  source) { return New(source.GetInfo(false)); }
		public static OutputInvoiceLineViewModel New(OutputInvoiceLineInfo source)
		{
			OutputInvoiceLineViewModel obj = new OutputInvoiceLineViewModel();
			obj.CopyFrom(source);
			return obj;
		}
		
		public static OutputInvoiceLineViewModel Get(long oid)
		{
			OutputInvoiceLineViewModel obj = new OutputInvoiceLineViewModel();
			obj.CopyFrom(OutputInvoiceLineInfo.Get(oid, false));
			return obj;
		}

		public static void Add(OutputInvoiceLineViewModel item)
		{
			/*OutputInvoiceLine newItem = OutputInvoiceLine.New();
			item.CopyTo(newItem);
			newItem.Save();
			item.CopyFrom(newItem);*/
		}
		public static void Edit(OutputInvoiceLineViewModel source, HttpRequestBase request = null)
		{
			/*OutputInvoiceLine item = OutputInvoiceLine.Get(source.Oid);
			source.CopyTo(item, request);
			item.Save();*/
		}
		public static void Remove(long oid)
		{
			//OutputInvoiceLine.Delete(oid);
		}
		
		#endregion
	}	
	
		/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class OutputInvoiceLineListViewModel : List<OutputInvoiceLineViewModel>
	{
		#region Business Objects

		#endregion

		#region Factory Methods

		public OutputInvoiceLineListViewModel() { }

		public static OutputInvoiceLineListViewModel Get()
		{
			OutputInvoiceLineListViewModel list = new OutputInvoiceLineListViewModel();

			OutputInvoiceLineList sourceList = OutputInvoiceLineList.GetList();

			foreach (OutputInvoiceLineInfo item in sourceList)
				list.Add(OutputInvoiceLineViewModel.New(item));

			return list;
		}
		public static OutputInvoiceLineListViewModel Get(OutputInvoiceLineList sourceList)
		{
			OutputInvoiceLineListViewModel list = new OutputInvoiceLineListViewModel();

			foreach (OutputInvoiceLineInfo item in sourceList)
				list.Add(OutputInvoiceLineViewModel.New(item));

			return list;
		}

		#endregion

        #region Business Methods

        public OutputInvoiceLineViewModel GetItem(long oid)
        {
            return this.FirstOrDefault(x => x.Oid == oid);
        }

        #endregion
	}
}
