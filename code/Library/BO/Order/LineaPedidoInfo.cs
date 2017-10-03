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
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class LineaPedidoInfo : ReadOnlyBaseEx<LineaPedidoInfo>
	{	
		#region Attributes

		protected OrderLineBase _base = new OrderLineBase();
		
		#endregion
		
		#region Properties

		public OrderLineBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidPedido { get { return _base.Record.OidPedido; } }
		public long OidProducto { get { return _base.Record.OidProducto; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string Concepto { get { return _base.Record.Concepto; } }
		public Decimal CantidadKilos { get { return _base.Record.CantidadKilos; } }
		public Decimal CantidadBultos { get { return _base.Record.CantidadBultos; } }
		public Decimal Precio { get { return _base.Record.Precio; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public long OidPartida { get { return _base.Record.OidPartida; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidKit { get { return _base.Record.OidKit; } }
		public bool FacturacionBulto { get { return _base.Record.FacturacionBulto; } }
		public Decimal PImpuestos { get { return _base.Record.PImpuestos; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public Decimal Gastos { get { return _base.Record.Gastos; } }
		public Decimal Subtotal { get { return _base.Record.Subtotal; } }
		public long OidAlmacen { get { return _base.Record.OidAlmacen; } }
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } }

        //NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EStatus; } }
        public virtual string EstadoLabel { get { return _base.StatusLabel; } }
        public virtual string StoreID { get { return _base.StoreID; } set { _base.StoreID = value; } }
        public virtual string Almacen { get { return _base.Store; } set { _base.Store = value; } }
        public virtual string StoreIDStore { get { return _base.StoreIDStore; } }
		public virtual string Expediente { get { return _base._expediente; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } }
		public virtual long OidStock { get { return _base._oid_stock; } }
		public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } }
		public virtual Decimal Beneficio { get { return _base.Beneficio; } }
		public virtual Decimal BeneficioKilo { get { return _base.BeneficioKilo; } }
		public virtual Decimal Pendiente { get { return _base._pendiente; } }
		public virtual Decimal PendienteBultos { get { return _base._pendiente_bultos; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
		public virtual bool IsComplete { get { return _base.IsComplete; } }

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
		protected LineaPedidoInfo() { /* require use of factory methods */ }
		private LineaPedidoInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal LineaPedidoInfo(LineaPedido item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        /// <remarks>
		/// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool retrieve_childs)
		/// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
		/// <remarks/>
		public static LineaPedidoInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static LineaPedidoInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new LineaPedidoInfo(sessionCode, reader, childs); }
		
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
