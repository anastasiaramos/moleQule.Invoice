using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

using NHibernate;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class AlbaranFacturaInfo : ReadOnlyBaseEx<AlbaranFacturaInfo>
	{	
		#region Attributes

		protected OutputDeliveryInvoiceBase _base = new OutputDeliveryInvoiceBase();

		#endregion
		
		#region Properties

		public OutputDeliveryInvoiceBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidAlbaran { get { return _base.Record.OidAlbaran; } }
		public long OidFactura { get { return _base.Record.OidFactura; } }
		public DateTime FechaAsignacion { get { return _base.Record.FechaAsignacion; } }
		
		public Decimal Importe { get { return _base.Importe; } set { _base.Importe = value; } }
		public string CodigoFactura { get { return _base.CodigoFactura; } set { _base.CodigoFactura = value; } }
		public string CodigoAlbaran { get { return _base.CodigoAlbaran; } set { _base.CodigoAlbaran = value; } }

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
		protected AlbaranFacturaInfo() { /* require use of factory methods */ }
		private AlbaranFacturaInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal AlbaranFacturaInfo(AlbaranFactura item, bool copy_childs)
		{
			_base.CopyValues(item);
		}
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        /// <remarks>
		/// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool childs)
		/// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
		/// <remarks/>
		public static AlbaranFacturaInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static AlbaranFacturaInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new AlbaranFacturaInfo(sessionCode, reader, childs);
		}

        public AlbaranFacturaPrint GetPrintObject(OutputInvoiceInfo factura, ClienteInfo cliente, OutputDeliveryInfo Albaran)
        {
            return AlbaranFacturaPrint.New(this, cliente, factura, Albaran);
        }

        public AlbaranFacturaPrint GetPrintObject()
        {
            OutputInvoiceInfo f = OutputInvoiceInfo.Get(this.OidFactura);
            return AlbaranFacturaPrint.New(this, ClienteInfo.Get(f.OidCliente), f, OutputDeliveryInfo.Get(this.OidAlbaran));
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
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion
		
	}
}
