using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class OutputInvoiceLineRecord : RecordBase
	{
		#region Attributes

		private long _oid_factura;
		private long _oid_partida;
		private long _oid_expediente;
		private long _oid_producto;
		private long _oid_kit;
		private long _oid_concepto_albaran;
		private string _concepto = string.Empty;
		private bool _facturacion_bulto = false;
		private Decimal _cantidad_kilos;
		private Decimal _cantidad_bultos;
		private Decimal _p_impuestos;
		private Decimal _p_descuento;
		private Decimal _total;
		private Decimal _precio;
		private Decimal _subtotal;
		private Decimal _gastos;
		private long _oid_impuesto;
		//private long _oid_almacen;
		private string _codigo_producto_cliente = string.Empty;

		#endregion

		#region Properties

		public virtual long OidFactura { get { return _oid_factura; } set { _oid_factura = value; } }
		public virtual long OidPartida { get { return _oid_partida; } set { _oid_partida = value; } }
		public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
		public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
		public virtual long OidKit { get { return _oid_kit; } set { _oid_kit = value; } }
		public virtual long OidConceptoAlbaran { get { return _oid_concepto_albaran; } set { _oid_concepto_albaran = value; } }
		public virtual string Concepto { get { return _concepto; } set { _concepto = value; } }
		public virtual bool FacturacionBulto { get { return _facturacion_bulto; } set { _facturacion_bulto = value; } }
		public virtual Decimal CantidadKilos { get { return _cantidad_kilos; } set { _cantidad_kilos = value; } }
		public virtual Decimal CantidadBultos { get { return _cantidad_bultos; } set { _cantidad_bultos = value; } }
		public virtual Decimal PImpuestos { get { return _p_impuestos; } set { _p_impuestos = value; } }
		public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
		public virtual Decimal Total { get { return Decimal.Round(_total, 2); } set { _total = value; } }
		public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }
		public virtual Decimal Subtotal { get { return Decimal.Round(_subtotal, 2); } set { _subtotal = value; } }
		public virtual Decimal Gastos { get { return _gastos; } set { _gastos = value; } }
		public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
		//public virtual long OidAlmacen { get { return _oid_almacen; } set { _oid_almacen = value; } }
		public virtual string CodigoProductoCliente { get { return _codigo_producto_cliente; } set { _codigo_producto_cliente = value; } }

		#endregion

		#region Business Methods

		public OutputInvoiceLineRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_factura = Format.DataReader.GetInt64(source, "OID_FACTURA");
			_oid_partida = Format.DataReader.GetInt64(source, "OID_BATCH");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
			_oid_kit = Format.DataReader.GetInt64(source, "OID_KIT");
			_oid_concepto_albaran = Format.DataReader.GetInt64(source, "OID_CONCEPTO_ALBARAN");
			_concepto = Format.DataReader.GetString(source, "CONCEPTO");
			_facturacion_bulto = Format.DataReader.GetBool(source, "FACTURACION_BULTO");
			_cantidad_kilos = Format.DataReader.GetDecimal(source, "CANTIDAD");
			_cantidad_bultos = Format.DataReader.GetDecimal(source, "CANTIDAD_BULTOS");
			_p_impuestos = Format.DataReader.GetDecimal(source, "P_IGIC");
			_p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
			_total = Format.DataReader.GetDecimal(source, "TOTAL");
			_precio = Format.DataReader.GetDecimal(source, "PRECIO");
			_subtotal = Format.DataReader.GetDecimal(source, "SUBTOTAL");
			_gastos = Format.DataReader.GetDecimal(source, "GASTOS");
			_oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
			//_oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
			_codigo_producto_cliente = Format.DataReader.GetString(source, "CODIGO_PRODUCTO_CLIENTE");

		}
		public virtual void CopyValues(OutputInvoiceLineRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_factura = source.OidFactura;
			_oid_partida = source.OidPartida;
			_oid_expediente = source.OidExpediente;
			_oid_producto = source.OidProducto;
			_oid_kit = source.OidKit;
			_oid_concepto_albaran = source.OidConceptoAlbaran;
			_concepto = source.Concepto;
			_facturacion_bulto = source.FacturacionBulto;
			_cantidad_kilos = source.CantidadKilos;
			_cantidad_bultos = source.CantidadBultos;
			_p_impuestos = source.PImpuestos;
			_p_descuento = source.PDescuento;
			_total = source.Total;
			_precio = source.Precio;
			_subtotal = source.Subtotal;
			_gastos = source.Gastos;
			_oid_impuesto = source.OidImpuesto;
			//_oid_almacen = source.OidAlmacen;
			_codigo_producto_cliente = source.CodigoProductoCliente;
		}

		#endregion
	}

	[Serializable()]
	public class OutputInvoiceLineBase
	{
		#region Attributes

		private OutputInvoiceLineRecord _record = new OutputInvoiceLineRecord();

		internal long _oid_almacen;
        protected string _store_id = string.Empty;
        protected string _store = string.Empty;
        protected string _expediente = string.Empty;
		internal string _cuenta_contable = string.Empty;
		internal string _n_factura = string.Empty;
		internal DateTime _fecha_factura;
		internal string _cliente = string.Empty;

		#endregion

		#region Properties

		public OutputInvoiceLineRecord Record { get { return _record; } }

        public bool IsKitComponent { get { return _record.OidKit > 0; } }
        public Decimal BaseImponible { get { return _record.Subtotal - Descuento; } }
        public Decimal Descuento { get { return Decimal.Round(_record.Subtotal * _record.PDescuento / 100, 2); } }
        public Decimal Impuestos { get { return Decimal.Round(BaseImponible * _record.PImpuestos / 100, 2); } }
        public bool FacturacionPeso { get { return !_record.FacturacionBulto; } }
        public ETipoFacturacion ETipoFacturacion { get { return (FacturacionPeso) ? ETipoFacturacion.Peso : ETipoFacturacion.Unidad; } }
        public virtual string StoreID { get { return _store_id; } set { _store_id = value; } }
        public virtual string Store { get { return _store; } set { _store = value; } }
        public virtual string StoreIDStore { get { return _store_id + " - " + _store; } }
        public string Expediente { get { return _expediente; } set { _expediente = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			long tipo_query = Format.DataReader.GetInt64(source, "TIPO_QUERY");

			//switch ((ConceptoFactura.ETipoQuery)tipo_query)
			//{
			//    case ConceptoFactura.ETipoQuery.GENERAL:
			//        {
			//            _oid_factura = Format.DataReader.GetInt64(source, "OID_FACTURA");
			//            _oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			//            _oid_partida = Format.DataReader.GetInt64(source, "OID_BATCH");
			//            _oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
			//            _oid_concepto_albaran = Format.DataReader.GetInt64(source, "OID_CONCEPTO_ALBARAN");
			//            _oid_kit = Format.DataReader.GetInt64(source, "OID_KIT");
			//            _oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
			//            _concepto = Format.DataReader.GetString(source, "CONCEPTO");
			//            _cantidad = Format.DataReader.GetDecimal(source, "CANTIDAD");
			//            _cantidad_bultos = Format.DataReader.GetDecimal(source, "CANTIDAD_BULTOS");
			//            _facturacion_bulto = Format.DataReader.GetBool(source, "FACTURACION_BULTO");
			//            _p_impuestos = Format.DataReader.GetDecimal(source, "P_IGIC");
			//            _p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
			//            _total = Format.DataReader.GetDecimal(source, "TOTAL");
			//            _precio = Format.DataReader.GetDecimal(source, "PRECIO");
			//            _subtotal = Format.DataReader.GetDecimal(source, "SUBTOTAL");
			//            _gastos = Format.DataReader.GetDecimal(source, "GASTOS");
			//            _codigo_producto_cliente = Format.DataReader.GetString(source, "CODIGO_PRODUCTO_CLIENTE");

			//            _oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
			//            _almacen = Format.DataReader.GetString(source, "ALMACEN");
			//            _expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
			//            _cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
			//        }
			//        break;
			//    case ConceptoFactura.ETipoQuery.BY_EXPEDIENTE:
			//        { 

			_record.CopyValues(source);

			_oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
			_store = Format.DataReader.GetString(source, "STORE");
            _store_id = Format.DataReader.GetString(source, "STORE_ID");
			_expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
			_cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
			_n_factura = Format.DataReader.GetString(source, "N_FACTURA");
			_fecha_factura = Format.DataReader.GetDateTime(source, "FECHA_FACTURA");
			_cliente = Format.DataReader.GetString(source, "CLIENTE");
			//        } 
			//        break;
			//}
		}
        public void CopyValues(OutputInvoiceLine source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_oid_almacen = source.OidAlmacen;
			_store = source.Almacen;
            _store_id = source.StoreID;
			_expediente = source.Expediente;
			_cuenta_contable = source.CuentaContable;
			_n_factura = source.NFactura;
			_fecha_factura = source.FechaFactura;
			_cliente = source.Cliente;
		}
        public void CopyValues(OutputInvoiceLineInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_oid_almacen = source.OidAlmacen;
			_store = source.Almacen;
            _store_id = source.StoreID;
			_expediente = source.Expediente;
			_cuenta_contable = source.CuentaContable;
			_n_factura = source.NFactura;
			_fecha_factura = source.FechaFactura;
			_cliente = source.Cliente;
		}

		#endregion
	}

    /// <summary>
    /// Editable Child Business Object
    /// </summary>
    [Serializable()]
    public class OutputInvoiceLine : BusinessBaseEx<OutputInvoiceLine>
    {
        #region Attributes

		protected OutputInvoiceLineBase _base = new OutputInvoiceLineBase();

        #endregion

        #region Properties

		public OutputInvoiceLineBase Base { get { return _base; } }

		public override long Oid
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Oid;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Oid.Equals(value))
				{
					_base.Record.Oid = value;
					//PropertyHasChanged();
				}
			}
		}
		public virtual long OidFactura
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidFactura;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidFactura.Equals(value))
				{
					_base.Record.OidFactura = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidPartida
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidPartida;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidPartida.Equals(value))
				{
					_base.Record.OidPartida = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidExpediente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidExpediente;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidExpediente.Equals(value))
				{
					_base.Record.OidExpediente = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidProducto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidProducto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidProducto.Equals(value))
				{
					_base.Record.OidProducto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidKit
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidKit;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidKit.Equals(value))
				{
					_base.Record.OidKit = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidConceptoAlbaran
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidConceptoAlbaran;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidConceptoAlbaran.Equals(value))
				{
					_base.Record.OidConceptoAlbaran = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Concepto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Concepto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Concepto.Equals(value))
				{
					_base.Record.Concepto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool FacturacionBulto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FacturacionBulto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FacturacionBulto.Equals(value))
				{
					_base.Record.FacturacionBulto = value;
					Subtotal = (_base.Record.FacturacionBulto) ? _base.Record.CantidadBultos * _base.Record.Precio : _base.Record.CantidadKilos * _base.Record.Precio;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal CantidadKilos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CantidadKilos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.CantidadKilos.Equals(value))
				{
					_base.Record.CantidadKilos = value;
					if (!_base.Record.FacturacionBulto) CalculateTotal();
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal CantidadBultos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CantidadBultos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.CantidadBultos.Equals(value))
				{
					_base.Record.CantidadBultos = value;
					if (_base.Record.FacturacionBulto) CalculateTotal();
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal PImpuestos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PImpuestos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PImpuestos.Equals(value))
				{
					_base.Record.PImpuestos = value;
					CalculateTotal();
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal PDescuento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PDescuento;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PDescuento.Equals(value))
				{
					_base.Record.PDescuento = value;
					CalculateTotal();
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Total
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Total;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Total.Equals(value))
				{
					_base.Record.Total = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Precio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Precio;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Precio.Equals(value))
				{
					_base.Record.Precio = value;
					CalculateTotal();
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Subtotal
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Subtotal;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Subtotal.Equals(value))
				{
					_base.Record.Subtotal = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Gastos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Gastos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Gastos.Equals(value))
				{
					_base.Record.Gastos = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidImpuesto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidImpuesto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidImpuesto.Equals(value))
				{
					_base.Record.OidImpuesto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CodigoProductoCliente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CodigoProductoCliente;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CodigoProductoCliente.Equals(value))
				{
					_base.Record.CodigoProductoCliente = value;
					PropertyHasChanged();
				}
			}
		}
       
        //NO ENLAZADAS
		public virtual long OidAlmacen { get { return _base._oid_almacen; } set { _base._oid_almacen = value; } }
        public virtual string StoreID { get { return _base.StoreID; } set { _base.StoreID = value; } }
        public virtual string Almacen { get { return _base.Store; } set { _base.Store = value; } }
        public virtual string StoreIDStore { get { return _base.StoreIDStore; } }
		public virtual string Expediente { get { return _base.Expediente; } set { _base.Expediente = value; } }
		public virtual string CodigoExpediente { get { return Expediente; } } /* DEPRECATED */
		public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } set { PropertyHasChanged(); } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } set { PropertyHasChanged(); } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { FacturacionBulto = !value; } }
		public virtual string CuentaContable { get { return _base._cuenta_contable; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
		public virtual string NFactura { get { return _base._n_factura; } set { _base._n_factura = value; } }
		public virtual DateTime FechaFactura { get { return _base._fecha_factura; } set { _base._fecha_factura = value; } }
		public virtual string Cliente { get { return _base._cliente; } set { _base._cliente = value; } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty;
            }
        }
       
        #endregion

        #region Business Methods

        public virtual void CopyFrom(OutputDeliveryLineInfo source)
        {
            if (source == null) return;

            OidConceptoAlbaran = source.Oid;
			OidAlmacen = source.OidAlmacen;
            OidExpediente = source.OidExpediente;
            OidPartida = source.OidPartida;
            OidProducto = source.OidProducto;
            OidKit = source.OidKit;
            OidImpuesto = source.OidImpuesto;
            Concepto = source.Concepto;
            CantidadBultos = source.CantidadBultos;
            CantidadKilos = source.CantidadKilos;
            PImpuestos = source.PImpuestos;
            PDescuento = source.PDescuento;
            Total = source.Total;
            Precio = source.Precio;
            FacturacionBulto = source.FacturacionBulto;
            Subtotal = source.Subtotal;
            Gastos = source.Gastos;
            CodigoProductoCliente = source.CodigoProductoCliente;

			Almacen = source.Almacen;
            StoreID = source.IDAlmacen;
			Expediente = source.Expediente;

			CalculateTotal();
        }

		public virtual void AjustaCantidad(ProductInfo product, BatchInfo batch)
		{
			if (batch != null)
			{
				if (FacturacionPeso)
					AjustaCantidadBultos(batch);
				else
					AjustaCantidadKilos(batch);
			}
			else if (product != null)
			{
                if (FacturacionPeso)
                    CantidadBultos = (product.KilosBulto == 0) ? CantidadKilos : CantidadKilos / product.KilosBulto;
                else
                    CantidadKilos = (product.KilosBulto == 0) ? CantidadBultos : CantidadBultos * product.KilosBulto;
			}
			else
			{
                if (FacturacionPeso)
                    CantidadBultos = CantidadKilos;
                else
                    CantidadKilos = CantidadBultos;
            }
		}

        private void AjustaCantidadBultos(BatchInfo batch)
		{
            //if (batch.StockKilos == 0) return;

            if ((CantidadKilos == batch.StockKilos) && (batch.StockKilos != 0))
                CantidadBultos = batch.StockBultos;
			else
                CantidadBultos = CantidadKilos / batch.KilosPorBulto;
		}
        private void AjustaCantidadKilos(BatchInfo batch)
		{
			//if (partida.StockBultos == 0) return;

            if ((CantidadBultos == batch.StockBultos) && (batch.StockBultos != 0))
                CantidadKilos = batch.StockKilos;
			else
                CantidadKilos = CantidadBultos * batch.KilosPorBulto;
		}

        public virtual void CalculateTotal()
        {
			Subtotal = (FacturacionBulto) ? CantidadBultos * Precio : CantidadKilos * Precio;
			Total = BaseImponible + Impuestos;

			//Forzamos el refresco del form
			Impuestos = Impuestos;
			Descuento = Descuento;
        }

		/// <summary>
		/// Actualiza el precio en base a si se Albaran por kilo o bulto
		/// y si el cliente tiene un precio especial para el producto
		/// </summary>
		public virtual void SetPrecio(ProductInfo producto,
										BatchInfo partida,
										ClienteInfo cliente)
		{
			Precio = cliente.GetPrecio(producto, partida, ETipoFacturacion);
			PDescuento = cliente.GetDescuento(producto, partida);
			CalculateTotal();
		}

        #endregion

        #region Validation Rules

        #endregion

        #region Authorization Rules

        public static bool CanAddObject()
        {
            return OutputInvoice.CanAddObject();
        }

        public static bool CanGetObject()
        {
            return OutputInvoice.CanGetObject();
        }

        public static bool CanDeleteObject()
        {
            return OutputInvoice.CanDeleteObject();
        }

        public static bool CanEditObject()
        {
            return OutputInvoice.CanEditObject();
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        public OutputInvoiceLine()
        {
            MarkAsChild();
            Oid = (long)(new Random().Next());
        }

        private OutputInvoiceLine(OutputInvoiceLine source)
        {
            MarkAsChild();
            Fetch(source);
        }

        private OutputInvoiceLine(int sessionCode, IDataReader reader)
        {
			SessionCode = sessionCode;
            MarkAsChild();
            Fetch(reader);
        }
        
        public virtual OutputInvoiceLineInfo GetInfo() { return GetInfo(false); }
        public virtual OutputInvoiceLineInfo GetInfo(bool childs)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new OutputInvoiceLineInfo(this, childs);
        }

        public static OutputInvoiceLine NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new OutputInvoiceLine();
        }

        public static OutputInvoiceLine NewChild(OutputInvoice parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            OutputInvoiceLine obj = new OutputInvoiceLine();
            obj.OidFactura = parent.Oid;

            return obj;
        }

        internal static OutputInvoiceLine GetChild(OutputInvoiceLine source)
        {
            return new OutputInvoiceLine(source);
        }

        internal static OutputInvoiceLine GetChild(int sessionCode, IDataReader reader)
        {
            return new OutputInvoiceLine(sessionCode, reader);
        }

        /// <summary>
        /// Borrado aplazado, es posible el undo 
        /// (La función debe ser "no estática")
        /// </summary>
        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
        }

        /// <summary>
        /// No se debe utilizar esta función para guardar. Hace falta el padre.
        /// Utilizar Insert o Update en sustitución de Save.
        /// </summary>
        /// <returns></returns>
        public override OutputInvoiceLine Save()
        {
            throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
        }
        
        #endregion

        #region Child Data Access

        private void Fetch(OutputInvoiceLine source)
        {
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

                if (Childs)
                {
                }

				MarkOld();
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }            
        }

        private void Fetch(IDataReader source)
        {
            try
            {
				_base.CopyValues(source);

                if (Childs)
                {
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void Insert(OutputInvoice parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            this.OidFactura = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            parent.Session().Save(Base.Record);

            MarkOld();
        }

        internal void Update(OutputInvoice parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            this.OidFactura = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            SessionCode = parent.SessionCode;
			OutputInvoiceLineRecord obj = Session().Get<OutputInvoiceLineRecord>(Oid);
            obj.CopyValues(Base.Record);
            Session().Update(obj);

			if (OidPartida == 0)
			{
				OutputDeliveries albaranes = OutputDeliveries.GetList(parent.GetAlbaranes(), true, true, parent.SessionCode);

				OutputDeliveryLine concepto = null;
				OutputDelivery albaran = null;

				foreach (OutputDelivery item in albaranes)
				{
					albaran = item;

					foreach (OutputDeliveryLine ca in item.Conceptos)
						if (ca.Oid == OidConceptoAlbaran)
						{
							concepto = ca;
							break;
						}

					if (concepto != null) break;
				}

				if (concepto != null)
				{
					concepto.CopyFrom(this);
					albaran.CalculateTotal();
				}
			}

            MarkOld();
        }

        internal void DeleteSelf(OutputInvoice parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;
            Session().Delete(Session().Get<OutputInvoiceLineRecord>(Oid));

            MarkNew();
        }

        #endregion

        #region SQL

        internal enum ETipoQuery { GENERAL = 0, BY_EXPEDIENTE = 1 }

        public new static string SELECT(long oid) { return SELECT(oid, true); }
        
		public static string COUNT_BY_EXPEDIENTE(long oid)
        {
            string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
            string query;

            query = "SELECT COUNT(*) AS \"CUENTA\" " +
                    " FROM " + cf + " AS CF " +
                    " WHERE CF.\"OID_EXPEDIENTE\" = " + oid;

            return query;
        }

		internal static string SELECT_FIELDS(long tipo, QueryConditions conditions)
        {
            string query;

            query = "SELECT " + (long)tipo + " AS \"TIPO_QUERY\"" +
                    "		,CF.*" +
					"       ,COALESCE(AL.\"OID\", 0) AS \"OID_ALMACEN\"" +
					"       ,COALESCE(AL.\"NOMBRE\", '') AS \"STORE\"" +
                    "       ,COALESCE(AL.\"CODIGO\", '') AS \"STORE_ID\"" +
					"       ,COALESCE(EX.\"CODIGO\", '') AS \"EXPEDIENTE\"" +
					"		,COALESCE(PR.\"CUENTA_CONTABLE_VENTA\", '') AS \"CUENTA_CONTABLE\"";

			//if (conditions.Expediente != null)
			//{
				query += "		,(SE.\"IDENTIFICADOR\" || '/' || FC.\"CODIGO\") AS \"N_FACTURA\"" +
						 "		,FC.\"FECHA\" AS \"FECHA_FACTURA\"" +
						 "		,CL.\"NOMBRE\" AS \"CLIENTE\"";
			//}

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query += " WHERE TRUE";

			if (conditions.ConceptoFactura != null) query += " AND CF.\"OID\" = " + conditions.ConceptoFactura.Oid;
			if (conditions.Factura != null) query += " AND CF.\"OID_FACTURA\" = " + conditions.Factura.Oid;
			if (conditions.Expediente != null) query += " AND CF.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;
			if (conditions.Almacen != null) query += " AND CA.\"OID_ALMACEN\" = " + conditions.Almacen.Oid;

			return query;
		}

        internal static string SELECT_BASE(QueryConditions conditions)
        {
            string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string ca = nHManager.Instance.GetSQLTable(typeof(InputDeliveryLineRecord));
			string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

            string query = string.Empty;
            long tipo = (long)ETipoQuery.GENERAL;

            if (conditions.Expediente != null)
                tipo = (long)ETipoQuery.BY_EXPEDIENTE;

            
            query = SELECT_FIELDS(tipo, conditions) +
                    " FROM " + cf + " AS CF" +
					" LEFT JOIN " + pr + " AS PR ON PR.\"OID\" = CF.\"OID_PRODUCTO\"" +
					" LEFT JOIN " + ca + " AS CA ON CA.\"OID\" = CF.\"OID_CONCEPTO_ALBARAN\"" +
					" LEFT JOIN " + al + " AS AL ON AL.\"OID\" = CA.\"OID_ALMACEN\"" +
					" LEFT JOIN " + ex + " AS EX ON EX.\"OID\" = CF.\"OID_EXPEDIENTE\"";

			//if (conditions.Expediente != null)
			//{
				string fc = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
				string se = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
				string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));

				query += " INNER JOIN " + fc + " AS FC ON FC.\"OID\" = CF.\"OID_FACTURA\"" +
						 " INNER JOIN " + se + " AS SE ON SE.\"OID\" = FC.\"OID_SERIE\"" +
						 " INNER JOIN " + cl + " AS CL ON CL.\"OID\" = FC.\"OID_CLIENTE\""; 
			//}

            return query;
        }

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query = 
				SELECT_BASE(conditions) +
				WHERE(conditions);

			if (lockTable) 
				query += @"
					FOR UPDATE OF CF NOWAIT";

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions { ConceptoFactura = OutputInvoiceLineInfo.New(oid) };
			return SELECT(conditions, lockTable);
		}

        #endregion 
    }
}

