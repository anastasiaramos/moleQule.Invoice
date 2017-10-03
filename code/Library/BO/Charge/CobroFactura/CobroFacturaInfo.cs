using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class CobroFacturaInfo : ReadOnlyBaseEx<CobroFacturaInfo>
	{	
		#region Attributes

		protected ChargeOperationBase _base = new ChargeOperationBase();

		#endregion
		
		#region Properties

		public ChargeOperationBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidCobro { get { return _base.Record.OidCobro; } }
		public long OidFactura { get { return _base.Record.OidFactura; } }
		public Decimal Cantidad { get { return _base.Record.Cantidad; } }
		public DateTime FechaAsignacion { get { return _base.Record.FechaAsignacion; } }

		public virtual string CodigoFactura { get { return _base.CodigoFactura; } set { _base.CodigoFactura = value; } }
		public virtual Decimal ImporteFactura { get { return _base.ImporteFactura; } }
		public virtual DateTime FechaFactura { get { return _base.FechaFactura; } }
		public virtual string CodigoCobro { get { return _base.CodigoCobro; } }
		public virtual long IdCobro { get { return _base.IdCobro; } set { _base.IdCobro = value; } }
		public virtual DateTime Fecha { get { return _base.Fecha; } set { _base.Fecha = value; } }
		public virtual Decimal Importe { get { return _base.Importe; } set { _base.Importe = value; } }
		public virtual Decimal TipoInteres { get { return _base.TipoInteres; } }
		public virtual Decimal GastosCobro { get { return _base.GastosCobro; } }
		public virtual long MedioPago { get { return _base.MedioPago; } set { _base.MedioPago = value; } }
		public virtual string Tipo { get { return _base.Tipo; } }
		public virtual DateTime Vencimiento { get { return _base.Vencimiento; } set { _base.Vencimiento = value; } }
		public virtual bool Cobrado { get { return _base.Cobrado; } set { _base.Cobrado = value; } }
		public virtual string Observaciones { get { return _base.Observaciones; } set { _base.Observaciones = value; } }
		public virtual long OidCliente { get { return _base.OidCliente; } set { _base.OidCliente = value; } }
		public virtual string Cliente { get { return _base.Cliente; } }
		public virtual DateTime FechaPrevision { get { return _base.FechaPrevision; } }
        public virtual long DiasCobro { get { return _base.DiasCobro; } }
		public string EMedioPagoLabel { get { return _base.EMedioPagoLabel; } }
		public string CuentaBancaria { get { return _base.CuentaBancaria; } set { _base.CuentaBancaria = value; } }

		#endregion
		
		#region Business Methods
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected CobroFacturaInfo() { /* require use of factory methods */ }
		private CobroFacturaInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal CobroFacturaInfo(CobroFactura item, bool childs)
		{
			_base.CopyValues(item);
		}
	
		public static CobroFacturaInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new CobroFacturaInfo(sessionCode, reader, childs);
		}

        public CobroFacturaPrint GetPrintObject(OutputInvoiceInfo factura, ClienteInfo cliente)
        {
            return CobroFacturaPrint.New(this, cliente, factura);
        }
        public CobroFacturaPrint GetPrintObject()
        {
            OutputInvoiceInfo f = OutputInvoiceInfo.Get(this.OidFactura, true);
            return CobroFacturaPrint.New(this, ClienteInfo.Get(f.OidCliente), f);
        }
		
 		#endregion
		
		#region Child Data Access
		
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
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		}
		
		#endregion		
	}
}
