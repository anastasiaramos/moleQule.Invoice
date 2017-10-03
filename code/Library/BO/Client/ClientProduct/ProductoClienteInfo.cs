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
    /// ReadOnly Child Business Object
    /// </summary>
    [Serializable()]
    public class ProductoClienteInfo : ReadOnlyBaseEx<ProductoClienteInfo>
    {
        #region Attributes

		protected ClientProductBase _base = new ClientProductBase();

		#endregion

		#region Properties

		public ClientProductBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidProducto { get { return _base.Record.OidProducto; } }
		public long OidCliente { get { return _base.Record.OidCliente; } }
		public Decimal Precio { get { return _base.Record.Precio; } }
		public bool FacturacionBulto { get { return _base.Record.FacturacionBulto; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public long TipoDescuento { get { return _base.Record.TipoDescuento; } }
		public Decimal PrecioCompra { get { return _base.Record.PrecioCompra; } }
		public bool Facturar { get { return _base.Record.Facturar; } }
		public DateTime FechaValidez { get { return _base.Record.FechaValidez; } }

		public virtual Common.ETipoDescuento ETipoDescuento { get { return _base.ETipoDescuento; } }
		public virtual string TipoDescuentoLabel { get { return _base.TipoDescuentoLabel; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
		public virtual string Producto { get { return _base.Producto; } set { _base.Producto = value; } }
		public virtual long OidFamilia { get { return _base.OidFamilia; } set { _base.OidFamilia = value; } }
		public virtual long OidImpuesto { get { return _base.OidImpuesto; } set { _base.OidImpuesto = value; } }
		public virtual decimal PImpuesto { get { return _base.PImpuesto; } set { _base.PImpuesto = value; } }

		#endregion

		#region Business Methods

		public void CopyFrom(ProductoCliente source) { _base.CopyValues(source); }
		public void CopyFrom(ProductoClienteInfo source) { _base.CopyValues(source); }

		public static Decimal GetPrecioCliente(ProductoClienteInfo productoCliente, ProductInfo producto, BatchInfo partida, ETipoFacturacion tipo)
		{
			return ClientProductBase.GetPrecioCliente(productoCliente, partida, producto, tipo); 
		}
		public static Decimal GetDescuentoCliente(ProductoClienteInfo productoCliente, Decimal pDescuento)
		{
			return ClientProductBase.GetDescuentoCliente(productoCliente, pDescuento); 
		}

        #endregion

        #region Child Factory Methods

        protected ProductoClienteInfo() { /* require use of factory methods */ }
        private ProductoClienteInfo(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }
        internal ProductoClienteInfo(ProductoCliente source)
        {
			_base.CopyValues(source);
        }

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static ProductoClienteInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new ProductoClienteInfo(sessionCode, reader, childs); }

        #endregion

        #region Child Data Access

        //called to copy data from IDataReader
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

