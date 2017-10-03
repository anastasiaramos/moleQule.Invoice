using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class OutputDeliveryLineRecord : RecordBase
	{
		#region Attributes

		private long _oid_albaran;
		private long _oid_partida;
		private long _oid_expediente;
		private long _oid_producto;
		private long _oid_kit;
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
		private long _oid_linea_pedido;
		private long _oid_almacen;
		private string _codigo_producto_cliente = string.Empty;

		#endregion

		#region Properties

		public virtual long OidAlbaran { get { return _oid_albaran; } set { _oid_albaran = value; } }
		public virtual long OidPartida { get { return _oid_partida; } set { _oid_partida = value; } }
		public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
		public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
		public virtual long OidKit { get { return _oid_kit; } set { _oid_kit = value; } }
		public virtual string Concepto { get { return _concepto; } set { _concepto = value; } }
		public virtual bool FacturacionBulto { get { return _facturacion_bulto; } set { _facturacion_bulto = value; } }
		public virtual Decimal CantidadKilos { get { return _cantidad_kilos; } set { _cantidad_kilos = value; } }
		public virtual Decimal CantidadBultos { get { return _cantidad_bultos; } set { _cantidad_bultos = value; } }
		public virtual Decimal PImpuestos { get { return _p_impuestos; } set { _p_impuestos = value; } }
		public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
		public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }
		public virtual Decimal Subtotal { get { return _subtotal; } set { _subtotal = value; } }
		public virtual Decimal Gastos { get { return _gastos; } set { _gastos = value; } }
		public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
		public virtual long OidLineaPedido { get { return _oid_linea_pedido; } set { _oid_linea_pedido = value; } }
		public virtual long OidAlmacen { get { return _oid_almacen; } set { _oid_almacen = value; } }
		public virtual string CodigoProductoCliente { get { return _codigo_producto_cliente; } set { _codigo_producto_cliente = value; } }

		#endregion

		#region Business Methods

		public OutputDeliveryLineRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_albaran = Format.DataReader.GetInt64(source, "OID_ALBARAN");
			_oid_partida = Format.DataReader.GetInt64(source, "OID_BATCH");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
			_oid_kit = Format.DataReader.GetInt64(source, "OID_KIT");
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
			_oid_linea_pedido = Format.DataReader.GetInt64(source, "OID_LINEA_PEDIDO");
			_oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
			_codigo_producto_cliente = Format.DataReader.GetString(source, "CODIGO_PRODUCTO_CLIENTE");
		}
		public virtual void CopyValues(OutputDeliveryLineRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_albaran = source.OidAlbaran;
			_oid_almacen = source.OidAlmacen;
			_oid_expediente = source.OidExpediente;
			_oid_partida = source.OidPartida;
			_oid_linea_pedido = source.OidLineaPedido;
			_oid_producto = source.OidProducto;
			_oid_kit = source.OidKit;
			_oid_impuesto = source.OidImpuesto;
			_concepto = source.Concepto;
			//Importante, es necesario que este campo se asigne antes de las cantidades
			_facturacion_bulto = source.FacturacionBulto;
			_cantidad_bultos = source.CantidadBultos;
			_cantidad_kilos = source.CantidadKilos;
			_p_impuestos = source.PImpuestos;
			_p_descuento = source.PDescuento;
			_total = source.Total;
			_precio = source.Precio;
			_subtotal = source.Subtotal;
			_gastos = source.Gastos;
			_codigo_producto_cliente = source.CodigoProductoCliente;
		}

		#endregion
	}

	[Serializable()]
	public class OutputDeliveryLineBase
	{
		#region Attributes

		private OutputDeliveryLineRecord _record = new OutputDeliveryLineRecord();

		protected string _store_id = string.Empty;
		protected string _store = string.Empty;
		protected string _expedient = string.Empty;
		protected Decimal _ayuda_kilo;
		protected string _ubicacion = string.Empty;
		protected long _oid_stock;
		protected long _oid_pedido;
		protected string _id_albaran = string.Empty;
		protected DateTime _fecha;
		protected long _holder_type;

		public long OidExpedienteOld = 0;

		#endregion

		#region Properties

		public OutputDeliveryLineRecord Record { get { return _record; } }

		public virtual bool IsKitComponent { get { return _record.OidKit > 0; } }
		public virtual Decimal Descuento { get { return Decimal.Round((_record.Subtotal * _record.PDescuento) / 100, 2); } }
		public virtual Decimal BaseImponible { get { return _record.Subtotal - Descuento; } }
		public virtual Decimal Impuestos { get { return Decimal.Round((BaseImponible * _record.PImpuestos) / 100, 5); } }
		public virtual Decimal AyudaKilo { get { return Decimal.Round(_ayuda_kilo, 5); } set { _ayuda_kilo = Decimal.Round(value, 5); } }
		public virtual Decimal Beneficio { get { return _record.CantidadKilos * BeneficioKilo; } }
		public virtual Decimal BeneficioKilo
		{
			get
			{
				if (_record.FacturacionBulto)
					return (_record.CantidadKilos > 0) ? ((_record.Precio / (_record.CantidadKilos / _record.CantidadBultos)) - _record.Gastos + _ayuda_kilo) * (1 - _record.PDescuento / 100) : 0;
				else
					return (_record.Precio - _record.Gastos + _ayuda_kilo) * (1 - _record.PDescuento / 100);
			}
		}
        public virtual Decimal PBeneficio { get { return Decimal.Round(_record.Subtotal > 0 ? Beneficio * 100 / _record.Subtotal : 0, 2); } }
		public virtual bool FacturacionPeso { get { return !_record.FacturacionBulto; } }
		public ETipoFacturacion ETipoFacturacion { get { return (FacturacionPeso) ? ETipoFacturacion.Peso : ETipoFacturacion.Unidad; } }
		public virtual string StoreID { get { return _store_id; } set { _store_id = value; } }
		public virtual string Store { get { return _store; } set { _store = value; } }
		public virtual string StoreIDStore { get { return _store_id + " - " + _store; } }
		public virtual string Expediente { get { return _expedient; } set { _expedient = value; } }
		public virtual string Ubicacion { get { return _ubicacion; } set { _ubicacion = value; } }
		public virtual long OidStock { get { return _oid_stock; } set { _oid_stock = value; } }
		public virtual long OidPedido { get { return _oid_pedido; } set { _oid_pedido = value; } }
		public virtual string IDAlbaran { get { return _id_albaran; } }
		public virtual DateTime Fecha { get { return _fecha; } }
		public virtual long HolderType { get { return _holder_type; } set { _holder_type = value; } }
		public ETipoEntidad EHolderType { get { return (ETipoEntidad)HolderType; } set { HolderType = (long)value; } }
		public string HolderTypeLabel { get { return Library.Common.EnumText<ETipoEntidad>.GetLabel(EHolderType); } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			OidExpedienteOld = _record.OidExpediente;

			_ayuda_kilo = Format.DataReader.GetDecimal(source, "AYUDA_KILO");
			_oid_pedido = Format.DataReader.GetInt64(source, "OID_PEDIDO");
			_oid_stock = Format.DataReader.GetInt64(source, "OID_STOCK");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_id_albaran = Format.DataReader.GetString(source, "ID_ALBARAN");
			_store_id = Format.DataReader.GetString(source, "STORE_ID");
			_store = Format.DataReader.GetString(source, "STORE");
			_expedient = Format.DataReader.GetString(source, "EXPEDIENTE");
			_holder_type = Format.DataReader.GetInt64(source, "HOLDER_TYPE");
		}
		public void CopyValues(OutputDeliveryLine source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_store_id = source.IDAlmacen;
			_store = source.Almacen;
			_expedient = source.Expediente;
			_ayuda_kilo = source.AyudaKilo;
			_oid_stock = source.OidStock;
			_oid_pedido = source.OidPedido;
			_holder_type = source.HolderType;
		}
		public void CopyValues(OutputDeliveryLineInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_store_id = source.IDAlmacen;
			_store = source.Almacen;
			_expedient = source.Expediente;
			_ayuda_kilo = source.AyudaKilo;
			_oid_stock = source.OidStock;
			_oid_pedido = source.OidPedido;
			_holder_type = source.HolderType;
		}

		#endregion
	}

    /// <summary>
    /// Editable Child Business Object with Child
    /// Este objeto tiene Stocks solo para consulta, pues es el expediente quien se encarga de actualizar los stocks
    /// </summary>
    [Serializable()]
    public class OutputDeliveryLine : BusinessBaseEx<OutputDeliveryLine>
    {
        #region Attributes

		protected OutputDeliveryLineBase _base = new OutputDeliveryLineBase();

		private Batchs _batchs = Batchs.NewChildList();
        private Stocks _stocks = Stocks.NewChildList();

        #endregion

        #region Properties

		public OutputDeliveryLineBase Base { get { return _base; } }

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
		public virtual long OidAlbaran
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAlbaran;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidAlbaran.Equals(value))
				{
					_base.Record.OidAlbaran = value;
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
		public virtual long OidLineaPedido
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidLineaPedido;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidLineaPedido.Equals(value))
				{
					_base.Record.OidLineaPedido = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidAlmacen
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAlmacen;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidAlmacen.Equals(value))
				{
					_base.Record.OidAlmacen = value;
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
					_base.Record.CantidadKilos = Decimal.Round(value, 2);
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
					_base.Record.CantidadBultos = Decimal.Round(value, 4);
					if (FacturacionBulto) CalculateTotal();
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
					_base.Record.PImpuestos = Decimal.Round(value, 2);
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
					_base.Record.PDescuento = Decimal.Round(value, 2);
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
					_base.Record.Total = Decimal.Round(value, 2);
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
					_base.Record.Precio = Decimal.Round(value, 5);
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
					_base.Record.Subtotal = Decimal.Round(value, 2);
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
					_base.Record.Gastos = Decimal.Round(value, 2);
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

        public virtual Stocks Stocks
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _stocks;
            }
            set
            {
                _stocks = value;
            }
        }
		public virtual Batchs Partidas
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _batchs;
            }

            set
            {
                _batchs = value;
            }
        }

        //Campos no enlazados
		public virtual string IDAlmacen { get { return _base.StoreID; } set { _base.StoreID = value; } }
		public virtual string Almacen { get { return _base.Store; } set { _base.Store = value; } }
		public virtual string IDAlmacenAlmacen { get { return _base.StoreIDStore; } }
		public virtual string CodigoExpediente { get { return _base.Expediente; } } /*DEPRECATED*/
		public virtual string Expediente { get { return _base.Expediente; } set { _base.Expediente = value; } }
		public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } set { PropertyHasChanged(); } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } set { PropertyHasChanged(); } }
		public virtual Decimal AyudaKilo { get { return _base.AyudaKilo; } set { _base.AyudaKilo = value; PropertyHasChanged(); } }
		public virtual Decimal Beneficio { get { return _base.Beneficio; } }
		public virtual Decimal BeneficioKilo { get { return _base.BeneficioKilo; } }
        public virtual Decimal PBeneficio { get { return _base.PBeneficio; } }
		public virtual Batch Partida { get { return _batchs.Count > 0 ? _batchs[0] : null; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { FacturacionBulto = !value; } }
		public virtual string Ubicacion { get { return _base.Ubicacion; } set { _base.Ubicacion = value; } }
		public virtual long OidStock { get { return _base.OidStock; } }
		public virtual long OidPedido { get { return _base.OidPedido; } set { _base.OidPedido = value; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
		public virtual long HolderType { get { return _base.HolderType; } set { _base.HolderType = value; } }
		public virtual ETipoEntidad EHolderType { get { return _base.EHolderType; } set { _base.EHolderType = value; } }
		public virtual string HolderTypeLabel { get { return _base.HolderTypeLabel; } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid && _stocks.IsValid && _batchs.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty || _stocks.IsDirty || _batchs.IsDirty;
            }
        }
       
        #endregion

        #region Business Methods

		public virtual void CopyFrom(OutputDelivery source)
		{
			OidAlbaran = source.Oid;
			OidAlmacen = source.OidAlmacen;
			OidExpediente = source.OidExpediente;

			IDAlmacen = source.IDAlmacen;
            Almacen = source.Almacen;
			Expediente = source.Expediente;
		}
        public virtual void CopyFrom(OutputDelivery delivery, BatchInfo batch, ProductInfo product)
        {
			CopyFrom(delivery, product);
			CopyFrom(batch);

			CantidadKilos = 1;
			CantidadBultos = 1;

			AjustaCantidad(product, batch);
        }
		public virtual void CopyFrom(BatchInfo source)
		{
			if (source == null) return;

			OidAlmacen = source.OidAlmacen;
			OidExpediente = source.OidExpediente;
			OidPartida = source.Oid;
			OidProducto = source.OidProducto;
			Gastos = source.CosteKilo;
			Concepto = source.TipoMercancia;
			AyudaKilo = (source.Ayuda) ? ((source.AyudaRecibidaKilo != 0) ? source.AyudaRecibidaKilo : source.AyudaKiloEstimada) : 0;

            Almacen = source.Almacen;
			IDAlmacen = source.IDAlmacen;
			Expediente = source.Expediente;
		}
        public virtual void CopyFrom(OutputDelivery delivery, ProductInfo product)
        {
			OidAlbaran = delivery.Oid;
            OidAlmacen = 0;
            OidPartida = 0;
            OidProducto = product.Oid;
            OidImpuesto = product.OidImpuestoVenta;
            OidLineaPedido = 0;
			Concepto = product.Descripcion;
			PImpuestos = product.PImpuestoVenta;
            Gastos = product.PrecioCompra;
			FacturacionPeso = (product.ETipoFacturacion == ETipoFacturacion.Peso);
            IDAlmacen = delivery.IDAlmacen;
            Almacen = delivery.Almacen;
            CodigoProductoCliente = product.CodigoArticuloAcreedor != string.Empty ? product.CodigoArticuloAcreedor : product.Codigo;
        }
		public virtual void CopyFrom(OutputDelivery delivery, ProductoClienteInfo source)
		{
			Almacen = string.Empty;
			OidAlbaran = delivery.Oid;
			OidAlmacen = 0;
			OidPartida = 0;
			OidProducto = source.OidProducto;
			OidImpuesto = source.OidImpuesto;
			OidLineaPedido = 0;
			CantidadKilos = 1;
			CantidadBultos = 1;
			Precio = source.Precio;
			PImpuestos = source.PImpuesto;
			Gastos = source.PrecioCompra;
			Concepto = source.Producto;
			FacturacionPeso = (source.ETipoFacturacion == ETipoFacturacion.Peso);
            Almacen = delivery.IDAlmacen;
            Almacen = delivery.Almacen;
		}
		public virtual void CopyFrom(LineaPedidoInfo source)
		{
			OidAlmacen = source.OidAlmacen;
			OidExpediente = source.OidExpediente;
            IDAlmacen = source.StoreID;
            Almacen = source.Almacen;
			Expediente = source.Expediente;
			OidLineaPedido = source.Oid;
			OidPedido = source.Oid;
			OidProducto = source.OidProducto;
			OidPartida = source.OidPartida;
			OidExpediente = source.OidExpediente;
			FacturacionBulto = source.FacturacionBulto;
			CantidadKilos = source.Pendiente;
			CantidadBultos = source.PendienteBultos;
			Precio = source.Precio;
			Concepto = source.Concepto;
		}
		public virtual void CopyFrom(OutputInvoiceLine source)
		{
			OidExpediente = source.OidExpediente;
			Expediente = source.Expediente;

			Concepto = source.Concepto;
			CantidadKilos = source.CantidadKilos;
			CantidadBultos = source.CantidadBultos;
			Precio = source.Precio;
			PDescuento = source.PDescuento;
			OidImpuesto = source.OidImpuesto;
			PImpuestos = source.PImpuestos;
			Total = source.Total;
            CodigoProductoCliente = source.CodigoProductoCliente;
		}

		public virtual void AjustaCantidad(ProductInfo producto, BatchInfo partida) { AjustaCantidad(producto, partida, 0); }
		public virtual void AjustaCantidad(ProductInfo producto, BatchInfo partida, decimal cantidad)
		{
			if (cantidad != 0)
			{
				if (FacturacionPeso) CantidadKilos = cantidad;
				else CantidadBultos = cantidad;
			}

			if (partida != null)
			{
				if (FacturacionPeso)
					AjustaCantidadBultos(partida);
				else
					AjustaCantidadKilos(partida);
			}
			else if (producto != null)
			{
				if (FacturacionPeso)
					CantidadBultos = (producto.KilosBulto == 0) ? CantidadKilos : CantidadKilos / producto.KilosBulto;
				else
					CantidadKilos = (producto.KilosBulto == 0) ? CantidadBultos : CantidadBultos * producto.KilosBulto;
			}
			else
			{
				if (FacturacionPeso)
					CantidadBultos = CantidadKilos;
				else
					CantidadKilos = CantidadBultos;
			}
		}
		
		internal void AjustaCantidadBultos(BatchInfo partida)
		{
			if (partida.StockKilos == 0) return;

			if (CantidadKilos == partida.StockKilos)
				CantidadBultos = partida.StockBultos;
			else
				CantidadBultos = CantidadKilos / partida.KilosPorBulto;
		}
        internal void AjustaCantidadKilos(BatchInfo partida)
		{
			if (partida.StockBultos == 0) return;

			if (CantidadBultos == partida.StockBultos)
				CantidadKilos = partida.StockKilos;
			else
				CantidadKilos = CantidadBultos * partida.KilosPorBulto;
		}
		
		public virtual void AjustaPrecio()
		{
			if (FacturacionBulto)
				Precio = Precio * CantidadKilos;
			else
				Precio = Precio / CantidadKilos;
		}

		public virtual void CalculateTotal()
        {
            Subtotal = (FacturacionBulto) ? CantidadBultos * Precio : CantidadKilos * Precio;
            Total = BaseImponible + Impuestos;

			//Para forzar el refresco en el formulario
			Impuestos = Impuestos;
			Descuento = Descuento;
        }

		public virtual void Sell(OutputDelivery albaran, SerieInfo serie, ClienteInfo client, ProductInfo product) { Sell(albaran, serie, client, product, null); }
        public virtual void Sell(OutputDelivery delivery, SerieInfo serie, ClienteInfo client, ProductInfo product, BatchInfo partida)
        {
			if (client != null)
				SaleToClient.Sell(this, delivery, serie, client, product, partida);
			else
				SaleToNobody.Sell(this, delivery, serie, product, partida);
        }

		public virtual void SetInvoicingType(ClienteInfo client, ProductInfo product)
		{
			if (client != null)
				SaleToClient.SetInvoicingType(this, client, product);
			else
				SaleToNobody.SetInvoicingType(this, product);
		}
        public virtual void SetTaxes(ClienteInfo client, ProductInfo product, SerieInfo serie)
        {
            //Primero el cliente si está EXENTO
            if ((client != null) && (client.OidImpuesto == 1))
            {
                OidImpuesto = client.OidImpuesto;
                PImpuestos = client.PImpuesto;
            }
            //Luego el producto
            else if ((product != null) && (product.OidImpuestoVenta != 0))
            {
                OidImpuesto = product.OidImpuestoVenta;
                PImpuestos = product.PImpuestoVenta;
            }
            //Luego la familia
            else
            {
                FamiliaInfo family = FamiliaInfo.Get(product.OidFamilia, false);
                if ((family != null) && (family.OidImpuesto != 0))
                {
                    OidImpuesto = family.OidImpuesto;
                    PImpuestos = family.PImpuesto;
                }
                //Luego la serie
                else if ((serie != null) && (serie.OidImpuesto != 0))
                {
                    OidImpuesto = serie.OidImpuesto;
                    PImpuestos = serie.PImpuesto;
                }
            }
        }

        /// <summary>
        /// Actualiza el precio en base a si se Albaran por kilo o bulto
        /// y si el cliente tiene un precio especial para el producto
        /// </summary>
        public virtual void SetPrice(ClienteInfo client)
        {
			if (client != null)
				SaleToClient.SetPrice(this, client);
			else
				SaleToNobody.SetPrice(this);
        }
		public virtual void SetPrice(ClienteInfo client, ProductInfo product, BatchInfo batch)
		{
			if (client != null)
				SaleToClient.SetPrice(this, client, product, batch);
			else
				SaleToNobody.SetPrice(this, product, batch);
		}
		
		#endregion

        #region Validation Rules

        #endregion

        #region Authorization Rules

        public static bool CanAddObject()
        {
            return AppContext.User.IsService 
					|| AutorizationRulesControler.CanAddObject(Resources.SecureItems.FACTURA_EMITIDA);
        }

        public static bool CanGetObject()
        {
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanGetObject(Resources.SecureItems.FACTURA_EMITIDA);
        }

        public static bool CanDeleteObject()
        {
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.FACTURA_EMITIDA);
        }

        public static bool CanEditObject()
        {
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanEditObject(Resources.SecureItems.FACTURA_EMITIDA);
        }

        #endregion

		#region Common Factory Methods

		public virtual OutputDeliveryLineInfo GetInfo(bool childs = false)	{ return new OutputDeliveryLineInfo(this, childs); }

		public virtual void LoadChilds(Type type, bool childs)
		{
            if (type.Equals(typeof(Batch)))
			{
				_batchs = Batchs.GetChildList(this, childs);
			}
			else if (type.Equals(typeof(Stock)))
			{
				_stocks = Stocks.GetChildList(this, childs);
			}
		}

		#endregion

		#region Child Factory Methods

		/// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        public OutputDeliveryLine()
        {
            MarkAsChild();
            _base.Record.Oid = (long)(new Random()).Next();
        }
        private OutputDeliveryLine(OutputDeliveryLine source)
        {
            MarkAsChild();
            Fetch(source);
        }
		private OutputDeliveryLine(int sessionCode, IDataReader reader)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Fetch(reader);
		}

        //Por cada padre que tenga la clase
        public static OutputDeliveryLine NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new OutputDeliveryLine();
        }
        public static OutputDeliveryLine NewChild(OutputDelivery parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            OutputDeliveryLine obj = new OutputDeliveryLine();
			obj.CopyFrom(parent);

            return obj;
        }
        public static OutputDeliveryLine NewChild(OutputDelivery parent, OutputDeliveryLine line)
        {
            return NewChild(parent, line.GetInfo(false));
        }
        public static OutputDeliveryLine NewChild(OutputDelivery parent, OutputDeliveryLineInfo line)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            OutputDeliveryLine obj = new OutputDeliveryLine();
            obj.OidAlbaran = parent.Oid;
            obj.Base.CopyValues(line);

            return obj;
        }

        internal static OutputDeliveryLine GetChild(OutputDeliveryLine source)
        {
            return new OutputDeliveryLine(source);
        }
		internal static OutputDeliveryLine GetChild(int sessionCode, IDataReader reader)
		{
			return new OutputDeliveryLine(sessionCode, reader);
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
        public override OutputDeliveryLine Save()
        {
            throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
        }
		
        #endregion

        #region Child Data Access

        private void Fetch(OutputDeliveryLine source)
        {
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

                if (Childs)
                {
                    IDataReader reader;
                    string query = string.Empty;

                    query = Stocks.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _stocks = Stocks.GetChildList(SessionCode, reader, Childs);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);

                if (Childs)
                {
                    string query;
                    IDataReader reader;

                    query = Stocks.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _stocks = Stocks.GetChildList(SessionCode, reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void Insert(OutputDelivery parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;
#if TRACE
            ControlerBase.AppControler.Timer.Record("ConceptoAlbaran.Insert()");
#endif
            this.OidAlbaran = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);
#if TRACE
			ControlerBase.AppControler.Timer.Record("Save del Concepto de Albarán");
#endif		
            //Para cargarlo en cache y saber que luego hay que actualizarlo
            Expedient expedient = (OidExpediente != 0)
                                    ? Store.Expedient.Get(OidExpediente, false, true, parent.SessionCode)
                                    : null;

            if (OidPartida != 0)
            {
                Batch batch = SaleFromBatch.Insert(this, parent);
                SellCattle.Insert(this, parent, batch, expedient);
            }
            else
            {
                ProductInfo product = ProductInfo.Get(OidProducto, false, true);
                if (!product.NoStockSale)
                {
                    if (product.IsKit)
                    {
                        product.LoadChilds(typeof(Kit), false);
                        SaleAutoBatch.InsertKit(this, parent, product);
                    }
                    else
                        SaleAutoBatch.Insert(this, parent);
                }
            }               

            MarkOld();
        }

        internal void Update(OutputDelivery parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;
#if TRACE
			ControlerBase.AppControler.Timer.Record("ConceptoAlbaran.Update()");
#endif
            this.OidAlbaran = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            SessionCode = parent.SessionCode;
            OutputDeliveryLineRecord record = Session().Get<OutputDeliveryLineRecord>(Oid);
            record.CopyValues(Base.Record);
            Session().Update(record);
#if TRACE
			ControlerBase.AppControler.Timer.Record("Update del Concepto de Albarán");
#endif

            Batch batch = null;

            // Actualizamos el stock del almacén asociado si tiene partida 
            if (this.OidPartida > 0)
            {
                batch = SaleFromBatch.Update(this, parent, record);
            }
            else
            {
                ProductInfo product = ProductInfo.Get(OidProducto, false, true);
                if (!product.NoStockSale)
                {
                    if (product.IsKit)
                    {
                        product.LoadChilds(typeof(Kit), false);
                        SaleAutoBatch.UpdateKit(this, parent, record, product);
                    }
                    else
                        SaleAutoBatch.Update(this, parent, record);
                }
            }

            //Se vuelve a actualizar por si han cambiado las cantidades
            record.CopyValues(Base.Record);
            Session().Update(record);

            //Para cargarlo en cache y saber que luego hay que actualizarlo
            Expedient expedient = (OidExpediente != 0) 
                                        ? Store.Expedient.Get(OidExpediente, false, true, parent.SessionCode)
                                        : null;

            SellCattle.Update(this, parent, batch, expedient);

			/*if ((_base.OidExpedienteOld != 0) && (OidExpediente != _base.OidExpedienteOld))
			{
				Store.Expedient.Get(_base.OidExpedienteOld, false, true, parent.SessionCode);
			}*/

            MarkOld();
        }

        internal void DeleteSelf(OutputDelivery parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;
			OutputDeliveryLineRecord obj = Session().Get<OutputDeliveryLineRecord>(Oid);
#if TRACE
			ControlerBase.AppControler.Timer.Record("Borrado del Concepto de Albarán");
#endif
            //Para cargarlo en cache y saber que luego hay que actualizarlo
            Expedient expedient = (OidExpediente != 0)
                                    ? Store.Expedient.Get(OidExpediente, false, true, parent.SessionCode)
                                    : null;

            // Actualizamos el stock del almacén asociado si tiene partida 
            if (this.OidPartida > 0)
            {
                SaleFromBatch.Delete(this, parent);
                SellCattle.Delete(this, parent, expedient);
            }
            else
            {
                ProductInfo product = ProductInfo.Get(OidProducto, false, true);
                if (!product.NoStockSale)
                {
                    if (product.IsKit)
                    {
                        product.LoadChilds(typeof(Kit), false);
                        SaleAutoBatch.DeleteKit(this, parent, product);
                    }
                    else
                        SaleAutoBatch.Delete(this, parent);
                }
            }
#if TRACE
			ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif
			Session().Delete(obj);

            MarkNew();
        }

        #endregion

        #region SQL

		public new static string SELECT(long oid) { return SELECT(oid, false); }
		
        internal static string SELECT_FIELDS()
        {
            string query;

			query = @"
				SELECT CA.*
						,COALESCE(AL.""CODIGO"", '') AS ""STORE_ID""
						,COALESCE(AL.""NOMBRE"", '') AS ""STORE""
						,COALESCE(EX.""CODIGO"", '') AS ""EXPEDIENTE""
						,COALESCE(ST.""OID"", 0) AS ""OID_STOCK""
						,COALESCE(LPC.""OID_PEDIDO"", 0) AS ""OID_PEDIDO""
						,AC.""FECHA"" AS ""FECHA""
						,AC.""CODIGO"" AS ""ID_ALBARAN""
						,AC.""HOLDER_TYPE"" AS ""HOLDER_TYPE""
						,CASE WHEN (COALESCE(PA.""AYUDA"") = TRUE) THEN
								(CASE WHEN (COALESCE(PA.""AYUDA_RECIBIDA_KILO"", 0) != 0) THEN
									COALESCE(PA.""AYUDA_RECIBIDA_KILO"", 0)
								ELSE COALESCE(PT.""AYUDA_KILO"", 0)
								END)
							ELSE 0
							END AS ""AYUDA_KILO""";

            return query;
        }

		internal static string	WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query = @"
            WHERE (AC.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			if (conditions.ConceptoAlbaran != null) 
                query += @"
                    AND CA.""OID"" = " + conditions.ConceptoAlbaran.Oid;
			if (conditions.OutputDelivery != null) 
                query += @"
                    AND CA.""OID_ALBARAN"" = " + conditions.OutputDelivery.Oid;
			if (conditions.Almacen != null) 
                query += @"
                    AND CA.""OID_ALMACEN"" = " + conditions.Almacen.Oid;
			if (conditions.Expediente != null) 
                query += @"
                    AND CA.""OID_EXPEDIENTE"" = " + conditions.Expediente.Oid;
			if (conditions.Producto != null) 
                query += @" 
                    AND CA.""OID_PRODUCTO"" = " + conditions.Producto.Oid;
			if (conditions.Pedido != null) 
                query += @"
                    AND PC.""OID"" = " + conditions.Pedido.Oid;
			if (conditions.Cliente != null) 
				query += @"
					AND AC.""OID_HOLDER"" = " + conditions.Cliente.Oid + @" AND AC.""HOLDER_TYPE"" = " + (long)ETipoEntidad.Cliente;

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT_BASE(QueryConditions conditions)
		{
			string ca = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryLineRecord));
			string ac = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryRecord));
			string st = nHManager.Instance.GetSQLTable(typeof(StockRecord));
			string lpc = nHManager.Instance.GetSQLTable(typeof(OrderLineRecord));
			string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
			string pa = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string pt = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string stockType = "(" + (long)ETipoStock.Venta + "," + (long)ETipoStock.Consumo + ")";

			string query = 
			SELECT_FIELDS() + @"
			FROM " + ca + @" AS CA
			INNER JOIN " + ac + @" AS AC ON AC.""OID"" = CA.""OID_ALBARAN""
			LEFT JOIN (SELECT MAX(ST.""OID"") AS ""OID"", ST.""OID_CONCEPTO_ALBARAN"" 
                        FROM " + st + @" AS ST 
                        WHERE ST.""TIPO"" IN " + stockType + @"
                        GROUP BY ""OID_CONCEPTO_ALBARAN"")
                AS ST ON ST.""OID_CONCEPTO_ALBARAN"" = CA.""OID""
			LEFT JOIN " + lpc + @" AS LPC ON LPC.""OID"" = CA.""OID_LINEA_PEDIDO""
			LEFT JOIN " + al + @" AS AL ON AL.""OID"" = CA.""OID_ALMACEN""
			LEFT JOIN " + pa + @" AS PA ON PA.""OID"" = CA.""OID_BATCH""
			LEFT JOIN " + pt + @" AS PT ON PT.""OID"" = CA.""OID_PRODUCTO""
			LEFT JOIN " + ex + @" AS EX ON EX.""OID"" = CA.""OID_EXPEDIENTE""";

			if (conditions.Pedido != null)
			{
				string pc = nHManager.Instance.GetSQLTable(typeof(OrderRecord));
				query += @"
				LEFT JOIN " + pc + @" AS PC ON PC.""OID"" = LPC.""OID_PEDIDO""";
			}

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
        {
			return SELECT(new QueryConditions { ConceptoAlbaran = OutputDeliveryLineInfo.New(oid) }, lockTable); 
        }

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query =
            SELECT_BASE(conditions) +
            WHERE(conditions) +
            EntityBase.LOCK("CA", lockTable);

			return query;
		}

        #endregion
    }

    #region Strategies (Strategy Pattern)

    public static class SaleToClient
    {
        public static void Sell(OutputDeliveryLine obj, OutputDelivery delivery, SerieInfo serie, ClienteInfo client, ProductInfo product, BatchInfo batch)
        {
            if (client == null)
                throw new iQException(Library.Invoice.Resources.Messages.NO_CLIENTE_SELECTED);

            if (client.Productos == null)
                client.LoadChilds(typeof(ProductoCliente), true);

            ProductoClienteInfo productoCliente = client.Productos.GetByProducto(product.Oid);

            if (batch == null)
                obj.CopyFrom(delivery, product);
            else
                obj.CopyFrom(batch);

            SetInvoicingType(obj, productoCliente, product);
            obj.SetTaxes(client, product, serie);
            obj.Precio = product.GetPrecioVenta(productoCliente, batch, obj.ETipoFacturacion);
        }

        public static void SetInvoicingType(OutputDeliveryLine obj, ClienteInfo client, ProductInfo product)
        {
            if (client == null)
                throw new iQException(Library.Invoice.Resources.Messages.NO_CLIENTE_SELECTED);

            if (client.Productos == null)
                client.LoadChilds(typeof(ProductoCliente), true);

            ProductoClienteInfo pci = client.Productos.GetItemByProperty("OidProducto", product.Oid);
            SetInvoicingType(obj, pci, product);
        }
        public static void SetInvoicingType(OutputDeliveryLine obj, ProductoClienteInfo pci, ProductInfo product)
        {
            if (pci != null)
                obj.FacturacionBulto = pci.FacturacionBulto;
            else if (product != null)
                obj.FacturacionBulto = !(product.ETipoFacturacion == ETipoFacturacion.Peso);
            else
                obj.FacturacionBulto = false;
        }

        public static void SetPrice(OutputDeliveryLine obj, ClienteInfo client)
        {
            ProductInfo product = ProductInfo.Get(obj.OidProducto, false, true);
            BatchInfo batch = BatchInfo.Get(obj.OidPartida, false, true);
            SetPrice(obj, client, product, batch);
        }
        public static void SetPrice(OutputDeliveryLine obj, ClienteInfo client, ProductInfo product, BatchInfo batch)
        {
            obj.Precio = client.GetPrecio(product, batch, obj.ETipoFacturacion);
            obj.PDescuento = client.GetDescuento(product, batch);
            obj.CalculateTotal();
        }
    }

    public static class SaleToNobody
    {
        public static void Sell(OutputDeliveryLine obj, OutputDelivery delivery, SerieInfo serie, ProductInfo product, BatchInfo batch)
        {
            if (batch == null)
                obj.CopyFrom(delivery, product);
            else
                obj.CopyFrom(batch);

            SetInvoicingType(obj, product);
            obj.SetTaxes(null, product, serie);
            obj.Precio = product.GetPrecioVenta(null, batch, obj.ETipoFacturacion);
        }

        public static void SetInvoicingType(OutputDeliveryLine obj, ProductInfo product)
        {
            if (product != null)
                obj.FacturacionBulto = !(product.ETipoFacturacion == ETipoFacturacion.Peso);
            else
                obj.FacturacionBulto = false;
        }

        public static void SetPrice(OutputDeliveryLine obj)
        {
            ProductInfo product = ProductInfo.Get(obj.OidProducto, false, true);
            BatchInfo batch = BatchInfo.Get(obj.OidPartida, false, true);

            SetPrice(obj, product, batch);
        }
        public static void SetPrice(OutputDeliveryLine obj, ProductInfo product, BatchInfo batch)
        {
            obj.Precio = product.GetPrecioVenta(null, batch, obj.ETipoFacturacion);
            obj.CalculateTotal();
        }
    }

    //Sale managent looking for batchs with available stock
    public static class SaleAutoBatch
    {
        public static void Insert(OutputDeliveryLine obj, OutputDelivery delivery)
        {
            if (obj.OidPartida != 0) return;

            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Obtención del Almacen");
#endif
            //Cargamos las partidas del almacen para actualizar los totales
            store.LoadPartidasByProducto(obj.OidProducto, true);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga de las Partidas");
#endif
            //Cargamos el stock de las partidas del almacen 
            store.LoadStockByProducto(obj.OidProducto, true, true);

            //Stock reservado si lo hubiere
            Stocks reserved_stocks = store.Stocks.GetByOutputOrderLineList(obj.OidLineaPedido);

            switch (obj.ETipoFacturacion)
            {
                case ETipoFacturacion.Peso:
                    if (!store.CheckStock(obj.ETipoFacturacion, obj.OidProducto, obj.CantidadKilos, reserved_stocks.TotalKgs()))
                        throw new iQException(string.Format(Library.Store.Resources.Messages.STOCK_INSUFICIENTE_PRODUCTO, obj.Concepto));
                    break;

                default:
                    if (!store.CheckStock(obj.ETipoFacturacion, obj.OidProducto, obj.CantidadBultos, reserved_stocks.TotalUds()))
                        throw new iQException(string.Format(Library.Store.Resources.Messages.STOCK_INSUFICIENTE_PRODUCTO, obj.Concepto));
                    break;
            }

            //Actualización de stocks sin pedido previo
            if (obj.OidLineaPedido == 0)
            {
                Batchs product_batchs = store.Partidas.GetByProductList(obj.OidProducto);

                OutputDeliveryLine partial_line = OutputDeliveryLine.NewChild(delivery, obj);
                partial_line.OidPartida = 0;

                decimal rest_ud = 0;
                decimal rest_kg = 0;

                foreach (Batch batch in product_batchs)
                {
                    rest_ud = partial_line.CantidadBultos;
                    rest_kg = partial_line.CantidadKilos;

                    if (obj.ETipoFacturacion == ETipoFacturacion.Peso)
                    {
                        partial_line.CantidadKilos = batch.Extract(partial_line, null, null);
                        partial_line.AjustaCantidadBultos(batch.GetInfo());

                        //La partida tiene algo en stock
                        if (partial_line.CantidadKilos != 0)
                        {
                            Stock stock = store.Stocks.NewItem(partial_line);
                            stock.OidAlmacen = batch.OidAlmacen;
                            stock.OidPartida = batch.Oid;
                            stock.Fecha = delivery.Fecha;
                            stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);

                            store.UpdateStocks(batch, true);
                        }

                        //La partida no tiene stock suficiente
                        if (partial_line.CantidadKilos != rest_ud)
                        {
                            partial_line.CantidadKilos = rest_ud - partial_line.CantidadKilos;
                        }
                        else break;
                    }
                    else
                    {
                        rest_ud = partial_line.CantidadBultos;

                        partial_line.CantidadBultos = batch.Extract(partial_line, null, null);
                        partial_line.AjustaCantidadKilos(batch.GetInfo());

                        //La partida tiene algo en stock
                        if (partial_line.CantidadBultos != 0)
                        {
                            Stock stock = store.Stocks.NewItem(partial_line);
                            stock.OidAlmacen = batch.OidAlmacen;
                            stock.OidPartida = batch.Oid;
                            stock.Fecha = delivery.Fecha;
                            stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);

                            store.UpdateStocks(batch, true);
                        }

                        //La partida no tiene stock suficiente
                        if (partial_line.CantidadBultos != rest_ud)
                        {
                            partial_line.CantidadBultos = rest_ud - partial_line.CantidadBultos;
                        }
                        else break;
                    }
                }
            }
            //Actualización de stocks según pedido previo
            else
            {
                decimal line_amount = (obj.ETipoFacturacion == ETipoFacturacion.Peso) ? obj.CantidadKilos : obj.CantidadBultos;

                OutputDeliveryLine partial_line = OutputDeliveryLine.NewChild(delivery, obj);
                partial_line.OidPartida = 0;

                foreach (Stock reserved_stock in reserved_stocks)
                {
                    decimal reserved_amount = (obj.ETipoFacturacion == ETipoFacturacion.Peso) ? reserved_stock.Kilos : reserved_stock.Bultos;

                    //Esta linea de stock es menor que la cantidad de la linea del albaran
                    if (reserved_amount < line_amount)
                    {
                        partial_line.CantidadBultos = reserved_stock.Bultos;
                        partial_line.CantidadKilos = reserved_stock.Kilos;

                        //Convertimos la linea de stock de pedido a linea de albaran

                        reserved_stock.CopyFrom(partial_line);
                        reserved_stock.Fecha = delivery.Fecha;
                        reserved_stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);

                        //Actualizamos la cantidad restante pendiente de convertir
                        line_amount -= (obj.ETipoFacturacion == ETipoFacturacion.Peso) ? partial_line.CantidadKilos : partial_line.CantidadBultos;
                    }
                    else
                    {
                        partial_line.CantidadBultos = obj.CantidadBultos;
                        partial_line.CantidadKilos = obj.CantidadKilos;

                        //Creamos una nueva linea de stock de linea de albaran
                        Stock new_stock = store.Stocks.NewItem(partial_line);
                        new_stock.OidPartida = reserved_stock.OidPartida;
                        new_stock.OidAlmacen = reserved_stock.OidAlmacen;
                        new_stock.OidLineaPedido = obj.OidLineaPedido;
                        new_stock.Fecha = delivery.Fecha;
                        new_stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);

                        //Actualizamos lo que queda del pedido en la linea de pedido
                        reserved_stock.Kilos -= new_stock.Kilos;
                        reserved_stock.Bultos -= new_stock.Bultos;
                    }

                    Batch batch = store.Partidas.GetItem(reserved_stock.OidPartida);
                    store.UpdateStocks(batch, true);
                }
            }
        }

        public static void InsertKit(OutputDeliveryLine obj, OutputDelivery delivery, ProductInfo product)
        {
            if (obj.OidPartida != 0) return;

            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Obtención del Almacen");
#endif
            foreach (KitInfo kit in product.Components)
            {
                //Cargamos las partidas del almacen para actualizar los totales
                store.LoadPartidasByProducto(kit.OidProduct, true);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga de las Partidas");
#endif
                //Cargamos el stock de las partidas del almacen 
                store.LoadStockByProducto(kit.OidProduct, true, true);

                //Stock reservado si lo hubiere
                Stocks reserved_stocks = store.Stocks.GetByOutputOrderLineList(obj.OidLineaPedido).GetByProductList(kit.OidProduct);

                switch (obj.ETipoFacturacion)
                {
                    case ETipoFacturacion.Peso:
                        if (!store.CheckStock(obj.ETipoFacturacion, kit.OidProduct, kit.Amount * obj.CantidadKilos + reserved_stocks.TotalKgs()))
                            throw new iQException(String.Format(Resources.Messages.STOCK_INSUFICIENTE_COMPONENTE, kit.Product));
                        break;

                    default:
                        if (!store.CheckStock(obj.ETipoFacturacion, kit.OidProduct, kit.Amount * obj.CantidadBultos + reserved_stocks.TotalUds()))
                            throw new iQException(String.Format(Resources.Messages.STOCK_INSUFICIENTE_COMPONENTE, kit.Product));
                        break;
                }

                OutputDeliveryLine partial_kit_line = OutputDeliveryLine.NewChild(delivery, obj);
                partial_kit_line.OidPartida = 0;
                partial_kit_line.OidProducto = kit.OidProduct;
                partial_kit_line.Concepto = kit.Product;

                //Actualización de stocks según pedido
                if (obj.OidLineaPedido == 0)
                {
                    Batchs product_batchs = store.Partidas.GetByProductList(kit.OidProduct);

                    partial_kit_line.CantidadBultos = obj.CantidadBultos * kit.Amount;
                    partial_kit_line.CantidadKilos = obj.CantidadKilos * kit.Amount;

                    decimal rest_kg = 0;
                    decimal rest_ud = 0;

                    foreach (Batch batch in product_batchs)
                    {
                        rest_ud = partial_kit_line.CantidadBultos;
                        rest_kg = partial_kit_line.CantidadKilos;

                        if (obj.ETipoFacturacion == ETipoFacturacion.Peso)
                        {
                            partial_kit_line.CantidadKilos = batch.Extract(partial_kit_line, null, null);
                            partial_kit_line.AjustaCantidadBultos(batch.GetInfo());

                            //La partida tiene algo en stock
                            if (partial_kit_line.CantidadKilos != 0)
                            {
                                Stock stock = store.Stocks.NewItem(partial_kit_line);
                                stock.OidAlmacen = batch.OidAlmacen;
                                stock.OidPartida = batch.Oid;
                                stock.Fecha = delivery.Fecha;
                                stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN_KIT, delivery.Codigo, product.Codigo);

                                store.UpdateStocks(batch, true);
                            }

                            //La partida no tiene stock suficiente
                            if (partial_kit_line.CantidadKilos != rest_kg)
                            {
                                partial_kit_line.CantidadKilos = rest_kg - partial_kit_line.CantidadBultos;
                            }
                            else break;
                        }
                        else
                        {
                            partial_kit_line.CantidadBultos = batch.Extract(partial_kit_line, null, null);
                            partial_kit_line.AjustaCantidadKilos(batch.GetInfo());

                            //La partida tiene algo en stock
                            if (partial_kit_line.CantidadBultos != 0)
                            {
                                Stock stock = store.Stocks.NewItem(partial_kit_line);
                                stock.OidAlmacen = batch.OidAlmacen;
                                stock.OidPartida = batch.Oid;
                                stock.Fecha = delivery.Fecha;
                                stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN_KIT, delivery.Codigo, product.Codigo);

                                store.UpdateStocks(batch, true);
                            }

                            //La partida no tiene stock suficiente
                            if (partial_kit_line.CantidadBultos != rest_ud)
                            {
                                partial_kit_line.CantidadBultos = rest_ud - partial_kit_line.CantidadBultos;
                            }
                            else break;
                        }
                    }
                }
                //Actualización de stocks según pedido previo
                else
                {
                    decimal line_kit_amount = (obj.ETipoFacturacion == ETipoFacturacion.Peso) ? kit.Amount * obj.CantidadKilos : kit.Amount * obj.CantidadBultos;

                    foreach (Stock reserved_stock in reserved_stocks)
                    {
                        decimal reserved_amount = (obj.ETipoFacturacion == ETipoFacturacion.Peso) ? reserved_stock.Kilos : reserved_stock.Bultos;

                        //Esta linea de stock es menor que la cantidad de la linea del albaran
                        if (reserved_amount < line_kit_amount)
                        {
                            partial_kit_line.CantidadBultos = reserved_stock.Bultos;
                            partial_kit_line.CantidadKilos = reserved_stock.Kilos;

                            //Convertimos la linea de stock de pedido a linea de albaran

                            reserved_stock.CopyFrom(partial_kit_line);
                            reserved_stock.Fecha = delivery.Fecha;
                            reserved_stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);

                            //Actualizamos la cantidad restante pendiente de convertir
                            line_kit_amount -= (obj.ETipoFacturacion == ETipoFacturacion.Peso) ? partial_kit_line.CantidadKilos : partial_kit_line.CantidadBultos;
                        }
                        else
                        {
                            partial_kit_line.CantidadBultos = kit.Amount * obj.CantidadBultos;
                            partial_kit_line.CantidadKilos = kit.Amount * obj.CantidadKilos;

                            //Creamos una nueva linea de stock de linea de albaran
                            Stock new_stock = store.Stocks.NewItem(partial_kit_line);
                            new_stock.OidPartida = reserved_stock.OidPartida;
                            new_stock.OidAlmacen = reserved_stock.OidAlmacen;
                            new_stock.OidLineaPedido = obj.OidLineaPedido;
                            new_stock.Fecha = delivery.Fecha;
                            new_stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);

                            //Actualizamos lo que queda del pedido en la linea de pedido
                            reserved_stock.Kilos -= new_stock.Kilos;
                            reserved_stock.Bultos -= new_stock.Bultos;
                        }

                        Batch batch = store.Partidas.GetItem(reserved_stock.OidPartida);
                        store.UpdateStocks(batch, true);
                    }
                }
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del Stock");
#endif
            }
        }

        public static void Update(OutputDeliveryLine obj, OutputDelivery delivery, OutputDeliveryLineRecord oldObj)
        {
#if TRACE
				ControlerBase.AppControler.Timer.Record("Save del Concepto de Albarán");
#endif
            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del Almacen");
#endif
            //Cargamos las partidas del expediente para actualizar los totales
            store.LoadPartidasByProducto(obj.OidProducto, true);
            Batch batch = store.Partidas.GetItem(obj.OidPartida);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga de las Partidas");
#endif
            //Cargamos el stock del producto
            store.LoadStockByProducto(obj.OidProducto, true, true);

            //Stock reservado si lo hubiere
            Stocks reserved_stocks = store.Stocks.GetByOutputOrderLineList(obj.OidLineaPedido);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del Stock");
#endif
            switch (obj.ETipoFacturacion)
            {
                case ETipoFacturacion.Peso:
                    if (!store.CheckStock(obj.ETipoFacturacion, obj.OidProducto, obj.CantidadKilos, oldObj.CantidadKilos + reserved_stocks.TotalKgs()))
                        throw new iQException(string.Format(Library.Store.Resources.Messages.STOCK_INSUFICIENTE_PRODUCTO, obj.Concepto));
                    break;

                default:
                    if (!store.CheckStock(obj.ETipoFacturacion, obj.OidProducto, obj.CantidadBultos, oldObj.CantidadBultos + reserved_stocks.TotalUds()))
                        throw new iQException(string.Format(Library.Store.Resources.Messages.STOCK_INSUFICIENTE_PRODUCTO, obj.Concepto));
                    break;
            }
#if TRACE
				ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif
            Delete(obj, delivery);
            Insert(obj, delivery);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif
            // No hace falta actualizar hijos porque se encarga el expediente               
        }

        public static void UpdateKit(OutputDeliveryLine obj, OutputDelivery delivery, OutputDeliveryLineRecord oldObj, ProductInfo product)
        {
#if TRACE
				ControlerBase.AppControler.Timer.Record("OutputDeliveryLine::UpdateKit START");
#endif
            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);

            //Stock reservado si lo hubiere
            Stocks reserved_stocks = store.Stocks.GetByOutputOrderLineList(obj.OidLineaPedido);

            foreach (KitInfo kit in product.Components)
            {
                //Cargamos las partidas del expediente para actualizar los totales
                store.LoadPartidasByProducto(kit.OidProduct, true);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga de las partidas del componente");
#endif
                //Cargamos el stock del producto
                store.LoadStockByProducto(kit.OidProduct, true, true);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del stock del componente");
#endif
                switch (obj.ETipoFacturacion)
                {
                    case ETipoFacturacion.Peso:
                        if (!store.CheckStock(obj.ETipoFacturacion, kit.OidProduct, kit.Amount * obj.CantidadKilos, kit.Amount * oldObj.CantidadKilos /*+ line_stock.TotalKg()*/))
                            throw new iQException(String.Format(Resources.Messages.STOCK_INSUFICIENTE_COMPONENTE, kit.Product));
                        break;

                    default:
                        if (!store.CheckStock(obj.ETipoFacturacion, kit.OidProduct, kit.Amount * obj.CantidadBultos, kit.Amount * oldObj.CantidadBultos /*+ line_stock.TotalUd()*/))
                            throw new iQException(String.Format(Resources.Messages.STOCK_INSUFICIENTE_COMPONENTE, kit.Product));
                        break;
                }
            }

            DeleteKit(obj, delivery, product);
            InsertKit(obj, delivery, product);
#if TRACE
				ControlerBase.AppControler.Timer.Record("OutputDeliveryLine::UpdateKit END");
#endif
            // No hace falta actualizar hijos porque se encarga el expediente               
        }

        public static void Delete(OutputDeliveryLine obj, OutputDelivery delivery)
        {
            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);

            //Cargamos las partidas del expediente para actualizar los totales
            store.LoadPartidasByProducto(obj.OidProducto, true);
            store.LoadStockByProducto(obj.OidProducto, true, true);

            //Stock asociado
            Stocks line_stocks = store.Stocks.GetByOutputDeliveryLineList(obj.Oid);

            if (line_stocks.Count > 0)
            {
                Stocks reserved_stocks = store.Stocks.GetByOutputOrderLineList(obj.OidLineaPedido);

                //Eliminamos las lineas de stock asociadas a la linea de albaran

                foreach (Stock line_stock in line_stocks)
                {
                    if (obj.OidLineaPedido != 0)
                    {
                        Stock reserved_stock = reserved_stocks.GetItemByBatch(line_stock.OidPartida);

                        if (reserved_stock != null)
                        {
                            //Devolvemos el stock a la reserva

                            reserved_stock.Bultos += line_stock.Bultos;
                            reserved_stock.Kilos += line_stock.Kilos;

                            //Eliminamos la linea

                            //Esta función se escarga tb de actualizar el stock
                            store.RemoveStock(line_stock);
                        }
                        else
                        {
                            //Convertimos la línea de stock de venta a reserva

                            PedidoInfo pedido = PedidoInfo.Get(obj.OidPedido, false, true);

                            line_stock.OidAlbaran = 0;
                            line_stock.OidConceptoAlbaran = 0;
                            line_stock.OidLineaPedido = obj.OidLineaPedido;
                            line_stock.ETipoStock = ETipoStock.Reserva;
                            line_stock.Fecha = pedido.Fecha;
                            line_stock.Observaciones = String.Format(Resources.Messages.RESERVA_POR_PEDIDO, pedido.Codigo);

                            store.UpdateStocks(store.Partidas.GetItem(line_stock.OidPartida), true);
                        }
                    }
                    else
                    {
                        //Eliminamos la linea

                        //Esta función se escarga tb de actualizar el stock
                        store.RemoveStock(line_stock);
                    }
                }
            }
        }

        public static void DeleteKit(OutputDeliveryLine obj, OutputDelivery delivery, ProductInfo product)
        {
            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);

            foreach (KitInfo kit in product.Components)
            {
                //Cargamos las partidas del expediente para actualizar los totales
                store.LoadPartidasByProducto(kit.OidProduct, true);
                store.LoadStockByProducto(kit.OidProduct, true, true);

                //Stock asociado
                Stocks line_stocks = store.Stocks.GetByOutputDeliveryLineList(obj.Oid);

                if (line_stocks.Count > 0)
                {
                    Stocks reserved_stocks = store.Stocks.GetByOutputOrderLineList(obj.OidLineaPedido);

                    //Eliminamos las lineas de stock asociadas a la linea de albaran

                    foreach (Stock line_stock in line_stocks)
                    {
                        if (line_stock.OidProducto != kit.OidProduct) continue;

                        if (obj.OidLineaPedido != 0)
                        {
                            Stock reserved_stock = reserved_stocks.GetItemByBatch(line_stock.OidPartida);

                            if (reserved_stock != null)
                            {
                                //Devolvemos el stock a la reserva

                                reserved_stock.Bultos += line_stock.Bultos;
                                reserved_stock.Kilos += line_stock.Kilos;

                                //Eliminamos la linea

                                //Esta función se escarga tb de actualizar el stock
                                store.RemoveStock(line_stock);
                            }
                            else
                            {
                                //Convertimos la línea de stock de venta a reserva

                                PedidoInfo pedido = PedidoInfo.Get(obj.OidPedido, false, true);

                                line_stock.OidAlbaran = 0;
                                line_stock.OidConceptoAlbaran = 0;
                                line_stock.OidLineaPedido = obj.OidLineaPedido;
                                line_stock.ETipoStock = ETipoStock.Reserva;
                                line_stock.Fecha = pedido.Fecha;
                                line_stock.Observaciones = String.Format(Resources.Messages.RESERVA_POR_PEDIDO, pedido.Codigo);

                                store.UpdateStocks(store.Partidas.GetItem(line_stock.OidPartida), true);
                            }
                        }
                        else
                        {
                            //Eliminamos la linea

                            //Esta función se escarga tb de actualizar el stock
                            store.RemoveStock(line_stock);
                        }
                    }
                }
            }
        }
    }

    //Sale managent from a selected batch
    public static class SaleFromBatch
    {
        public static Batch Insert(OutputDeliveryLine obj, OutputDelivery delivery)
        {
            if (obj.OidPartida == 0) return null;

            // Actualizamos el stock del almacen asociado si no es un concepto libre

            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Obtención del Almacen");
#endif
            //Cargamos las partidas del almacen para actualizar los totales
            store.LoadPartidasByProducto(obj.OidProducto, true);
            Batch line_batch = store.Partidas.GetItem(obj.OidPartida);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga de las Partidas");
#endif
            //Cargamos el stock de las partidas del almacen 
            store.LoadStockByPartida(obj.OidPartida, true, true);
            Stock line_stock = store.Stocks.GetItemByOutputOrderLine(obj.OidLineaPedido);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del Stock");
#endif
            if (!line_batch.CheckStock(obj, line_stock))
                throw new iQException(string.Format(Library.Store.Resources.Messages.STOCK_INSUFICIENTE_PRODUCTO, obj.Concepto));

            //Actualizacion de stocks según pedido
            if (obj.OidLineaPedido == 0)
            {
                Stock stock = store.Stocks.NewItem(obj);
                stock.Fecha = delivery.Fecha;
                stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);
            }
            else
            {
                bool parcial = (obj.FacturacionBulto) ? (Math.Abs(line_stock.Bultos) > obj.CantidadBultos) : (Math.Abs(line_stock.Kilos) > obj.CantidadKilos);

                if (parcial)
                {
                    //Creamos una nueva linea stock de albaran
                    Stock new_stock = store.Stocks.NewItem(obj);
                    new_stock.OidLineaPedido = obj.OidLineaPedido;
                    new_stock.Fecha = delivery.Fecha;
                    new_stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);

                    //Actualizamos lo que queda del pedido en la linea de pedido
                    line_stock.Kilos -= new_stock.Kilos;
                    line_stock.Bultos -= new_stock.Bultos;
                }
                else
                {
                    //Convertimos la linea de stock de pedido a linea de albaran
                    line_stock.CopyFrom(obj);
                    line_stock.Fecha = delivery.Fecha;
                    line_stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);
                }
            }

            store.UpdateStocks(line_batch, true);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif
            // No hace falta actualizar hijos porque se encarga el expediente

            return line_batch;
        }

        public static Batch Update(OutputDeliveryLine obj, OutputDelivery delivery, OutputDeliveryLineRecord oldObj)
        {
            if (obj.OidPartida == 0) return null;
#if TRACE
				ControlerBase.AppControler.Timer.Record("Save del Concepto de Albarán");
#endif
            Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del Almacen");
#endif
            //Cargamos las partidas del expediente para actualizar los totales
            store.LoadPartidasByProducto(obj.OidProducto, true);
            Batch line_batch = store.Partidas.GetItem(obj.OidPartida);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga de las Partidas");
#endif
            store.LoadStockByPartida(obj.OidPartida, true, true);

            Stock line_stock = store.Stocks.GetItem(obj.OidStock);
            Stock reserved_stocks = store.Stocks.GetItemByOutputOrderLine(obj.OidLineaPedido);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del Stock");
#endif
            if (!line_batch.CheckStock(obj, line_stock, reserved_stocks))
                throw new iQException(string.Format(Library.Store.Resources.Messages.STOCK_INSUFICIENTE_PRODUCTO, obj.Concepto));

            line_stock.CopyFrom(obj);
            line_stock.Observaciones = String.Format(Resources.Messages.RESERVA_POR_PEDIDO, delivery.Codigo);
            store.UpdateStocks(line_batch, true);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif
            //No existe pedido asociado
            if (obj.OidLineaPedido == 0)
            {
                line_stock.CopyFrom(obj);
                line_stock.Fecha = delivery.Fecha;
                line_stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);
            }
            else
            {
                //¿Se está devolviendo stock?
                decimal diferencia = (obj.FacturacionBulto) ? (oldObj.CantidadBultos - obj.CantidadBultos) : (oldObj.CantidadKilos - obj.CantidadKilos);

                //Se está devolviendo stock
                if (diferencia > 0)
                {
                    PedidoInfo pedido = PedidoInfo.Get(obj.OidPedido, false, true);

                    if (reserved_stocks != null)
                    {
                        //Actualizamos la linea de stock de pedido

                        reserved_stocks.Bultos += -(oldObj.CantidadBultos - obj.CantidadBultos);
                        reserved_stocks.Kilos += -(oldObj.CantidadKilos - obj.CantidadKilos);
                    }
                    else
                    {
                        //Creamos la linea de stock de pedido
                        reserved_stocks = store.Stocks.NewItem(obj);

                        reserved_stocks.Bultos = -(oldObj.CantidadBultos - obj.CantidadBultos);
                        reserved_stocks.Kilos = -(oldObj.CantidadKilos - obj.CantidadKilos);
                        reserved_stocks.OidAlbaran = 0;
                        reserved_stocks.OidConceptoAlbaran = 0;
                        reserved_stocks.OidLineaPedido = obj.OidLineaPedido;
                        reserved_stocks.ETipoStock = ETipoStock.Reserva;
                        reserved_stocks.Fecha = pedido.Fecha;
                        reserved_stocks.Observaciones = String.Format(Resources.Messages.RESERVA_POR_PEDIDO, pedido.Codigo);
                    }


                    line_stock.Fecha = delivery.Fecha;
                    line_stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);
                }
                //Se está sacando stock
                if (diferencia < 0)
                {
                    if (reserved_stocks != null)
                    {
                        reserved_stocks.Kilos += obj.CantidadBultos - oldObj.CantidadBultos;
                        reserved_stocks.Bultos += obj.CantidadKilos - oldObj.CantidadKilos;

                        if (reserved_stocks.Kilos >= 0) reserved_stocks.Bultos = 0;
                        if (reserved_stocks.Bultos >= 0) reserved_stocks.Kilos = 0;
                    }

                    //Se ha sacado todo o más
                    if (reserved_stocks.Kilos == 0)
                    {
                        //Convertimos la linea de stock de pedido a linea de albaran
                        reserved_stocks.CopyFrom(obj);
                        reserved_stocks.Fecha = delivery.Fecha;
                        reserved_stocks.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);

                        //Eliminamos la antigua linea de stock de albaran
                        store.RemoveStock(line_stock);
                    }
                    else
                    {
                        line_stock.CopyFrom(obj);
                        line_stock.Fecha = delivery.Fecha;
                        line_stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);
                    }
                }
                //Se han cambiado otras cosas (Precio, concepto...)
                else
                {
                    line_stock.CopyFrom(obj);
                    line_stock.Fecha = delivery.Fecha;
                    line_stock.Observaciones = String.Format(Resources.Messages.SALIDA_POR_ALBARAN, delivery.Codigo);
                }
            }

            store.UpdateStocks(line_batch, true);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif
            return line_batch;

            // No hace falta actualizar hijos porque se encarga el expediente               
        }

        public static void Delete(OutputDeliveryLine obj, OutputDelivery delivery)
        {
            if ((obj.OidPartida > 0) && (obj.OidStock != 0))
            {
                Almacen store = Store.Almacen.Get(obj.OidAlmacen, false, true, delivery.SessionCode);

                //Cargamos las partidas del expediente para actualizar los totales
                store.LoadPartidasByProducto(obj.OidProducto, true);
                store.LoadStockByPartida(obj.OidPartida, true, true);

                Batch partida = store.Partidas.GetItem(obj.OidPartida);
                Stock stock = store.Stocks.GetItem(obj.OidStock);

                if (obj.OidLineaPedido != 0)
                {
                    Stock stock_linea = store.Stocks.GetItemByOutputOrderLine(obj.OidLineaPedido);
                    PedidoInfo pedido = PedidoInfo.Get(obj.OidPedido, false, true);

                    if (stock_linea != null)
                    {
                        stock_linea.Bultos += -obj.CantidadBultos;
                        stock_linea.Kilos += -obj.CantidadKilos;

                        store.RemoveStock(stock);
                    }
                    else
                    {
                        //Convertimos la linea de stock de albaran en linea de pedido
                        stock_linea = store.Stocks.GetItem(obj.OidStock);
                        if (stock_linea != null)
                        {
                            stock_linea.Bultos = -obj.CantidadBultos;
                            stock_linea.Kilos = -obj.CantidadKilos;
                            stock_linea.OidAlbaran = 0;
                            stock_linea.OidConceptoAlbaran = 0;
                            stock_linea.OidLineaPedido = obj.OidLineaPedido;
                            stock_linea.ETipoStock = ETipoStock.Reserva;
                            stock_linea.Fecha = pedido.Fecha;
                            stock_linea.Observaciones = String.Format(Resources.Messages.RESERVA_POR_PEDIDO, pedido.Codigo);
                        }
                    }
                }
                else
                {
                    //Esta función se escarga tb de actualizar el stock
                    store.RemoveStock(obj.OidStock, obj.OidPartida);
                }
            }
        }
    }

    public static class SellCattle
    {
        public static void Delete(OutputDeliveryLine obj, OutputDelivery delivery, Expedient expedient)
        {
            if (obj.OidPartida == 0) return;
            if (expedient == null) return;
            if (expedient.ETipoExpediente != ETipoExpediente.Ganado) return;

            LivestockBook livestock_book = LivestockBook.Get(1, true, true, delivery.SessionCode);
            LivestockBookLine livestock_line = livestock_book.Lineas.GetItemByPartidaByConceptoAlbaran(obj.OidPartida, obj.Oid, ETipoLineaLibroGanadero.Venta);
            if (livestock_line != null) livestock_book.Lineas.Remove(livestock_line);
        }

        public static void Insert(OutputDeliveryLine obj, OutputDelivery delivery, Batch batch, Expedient expedient)
        {
            if (batch == null) return;
            if (expedient == null) return;
            if (expedient.ETipoExpediente != ETipoExpediente.Ganado) return;

            LivestockBook livestock_book = LivestockBook.Get(1, false, true, delivery.SessionCode);
            LivestockBookLine source_livestock_line = livestock_book.Lineas.GetItemByBatch(obj.OidPartida, ETipoLineaLibroGanadero.Importacion);

            if (source_livestock_line == null)
                throw new iQException(String.Format(Library.Store.Resources.Messages.LIVESTOCKLINE_NOT_FOUND, obj.Concepto));

            LivestockBookLine livestock_line = livestock_book.Lineas.NewItem(livestock_book);

            livestock_line.CopyFromPair(source_livestock_line.GetInfo());

            livestock_line.ETipo = ETipoLineaLibroGanadero.Venta;
            livestock_line.EEstado = EEstado.Baja;
            livestock_line.OidConceptoAlbaran = obj.Oid;
            livestock_line.Causa = Library.Store.Resources.Labels.LIBRO_GANADERO_CAUSA_BAJA_DEFECTO;
            livestock_line.Fecha = delivery.Fecha.AddSeconds(livestock_line.Serial);
            livestock_line.OidPartida = batch.Oid;

            ClienteInfo cliente = ClienteInfo.Get(delivery.OidHolder, false, true);
            if (cliente != null) livestock_line.Procedencia = cliente.Nombre;
#if TRACE
			ControlerBase.AppControler.Timer.Record("Inserción en el Libro");
#endif
        }

        public static void Update(OutputDeliveryLine obj, OutputDelivery delivery, Batch batch, Expedient expedient)
        {
            if (obj.OidPartida == 0) return;
            if (expedient == null) return;
            if (expedient.ETipoExpediente != ETipoExpediente.Ganado) return;

            LivestockBook livestock_book = LivestockBook.Get(1, false, true, delivery.SessionCode);

            LivestockBookLine source_livestock_line = livestock_book.Lineas.GetItemByBatch(obj.OidPartida, ETipoLineaLibroGanadero.Importacion);

            if (source_livestock_line == null)
                throw new iQException(String.Format(Library.Store.Resources.Messages.LIVESTOCKLINE_NOT_FOUND, obj.Concepto));

            LivestockBookLine livestock_line = livestock_book.Lineas.GetItemByPartidaByConceptoAlbaran(obj.OidPartida, obj.Oid, ETipoLineaLibroGanadero.Venta);

            if (livestock_line != null)
            {
                livestock_line.CopyFromPair(source_livestock_line.GetInfo());
                livestock_line.OidConceptoAlbaran = obj.Oid;
                livestock_line.EEstado = EEstado.Baja;
                livestock_line.Fecha = delivery.Fecha.AddSeconds(livestock_line.Serial);

                ClienteInfo cliente = ClienteInfo.Get(delivery.OidHolder, false, true);
#if TRACE
			ControlerBase.AppControler.Timer.Record("Carga del Libro");
#endif
            }
            else
                Insert(obj, delivery, batch, expedient);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del Libro");
#endif
        }
    }

    #endregion
}

