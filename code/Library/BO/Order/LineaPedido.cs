using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

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
	public class OrderLineRecord : RecordBase
	{
		#region Attributes

		private long _oid_pedido;
		private long _oid_producto;
		private long _estado;
		private string _concepto = string.Empty;
		private Decimal _cantidad_kilos;
		private Decimal _cantidad_bultos;
		private Decimal _precio;
		private Decimal _total;
		private string _observaciones = string.Empty;
		private long _oid_partida;
		private long _oid_expediente;
		private long _oid_kit;
		private bool _facturacion_bultos = false;
		private Decimal _p_impuestos;
		private Decimal _p_descuento;
		private Decimal _gastos;
		private Decimal _subtotal;
		private long _oid_almacen;
		private long _oid_impuesto;

		#endregion

		#region Properties

		public virtual long OidPedido { get { return _oid_pedido; } set { _oid_pedido = value; } }
		public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual string Concepto { get { return _concepto; } set { _concepto = value; } }
		public virtual Decimal CantidadKilos { get { return _cantidad_kilos; } set { _cantidad_kilos = value; } }
		public virtual Decimal CantidadBultos { get { return _cantidad_bultos; } set { _cantidad_bultos = value; } }
		public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }
		public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual long OidPartida { get { return _oid_partida; } set { _oid_partida = value; } }
		public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
		public virtual long OidKit { get { return _oid_kit; } set { _oid_kit = value; } }
		public virtual bool FacturacionBulto { get { return _facturacion_bultos; } set { _facturacion_bultos = value; } }
		public virtual Decimal PImpuestos { get { return _p_impuestos; } set { _p_impuestos = value; } }
		public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
		public virtual Decimal Gastos { get { return _gastos; } set { _gastos = value; } }
		public virtual Decimal Subtotal { get { return _subtotal; } set { _subtotal = value; } }
		public virtual long OidAlmacen { get { return _oid_almacen; } set { _oid_almacen = value; } }
		public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }

		#endregion

		#region Business Methods

		public OrderLineRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_pedido = Format.DataReader.GetInt64(source, "OID_PEDIDO");
			_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_concepto = Format.DataReader.GetString(source, "CONCEPTO");
			_cantidad_kilos = Format.DataReader.GetDecimal(source, "CANTIDAD");
			_cantidad_bultos = Format.DataReader.GetDecimal(source, "CANTIDAD_BULTOS");
			_precio = Format.DataReader.GetDecimal(source, "PRECIO");
			_total = Format.DataReader.GetDecimal(source, "TOTAL");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_oid_partida = Format.DataReader.GetInt64(source, "OID_PARTIDA");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			_oid_kit = Format.DataReader.GetInt64(source, "OID_KIT");
			_facturacion_bultos = Format.DataReader.GetBool(source, "FACTURACION_BULTOS");
			_p_impuestos = Format.DataReader.GetDecimal(source, "P_IMPUESTOS");
			_p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
			_gastos = Format.DataReader.GetDecimal(source, "GASTOS");
			_subtotal = Format.DataReader.GetDecimal(source, "SUBTOTAL");
			_oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
			_oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");

		}
		public virtual void CopyValues(OrderLineRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_pedido = source.OidPedido;
			_oid_producto = source.OidProducto;
			_estado = source.Estado;
			_concepto = source.Concepto;
			_cantidad_kilos = source.CantidadKilos;
			_cantidad_bultos = source.CantidadBultos;
			_precio = source.Precio;
			_total = source.Total;
			_observaciones = source.Observaciones;
			_oid_partida = source.OidPartida;
			_oid_expediente = source.OidExpediente;
			_oid_kit = source.OidKit;
			_facturacion_bultos = source.FacturacionBulto;
			_p_impuestos = source.PImpuestos;
			_p_descuento = source.PDescuento;
			_gastos = source.Gastos;
			_subtotal = source.Subtotal;
			_oid_almacen = source.OidAlmacen;
			_oid_impuesto = source.OidImpuesto;
		}

		#endregion
	}

	[Serializable()]
	public class OrderLineBase
	{
		#region Attributes

		private OrderLineRecord _record = new OrderLineRecord();
		
		public long OidExpedienteOld = 0;

		internal decimal _pendiente;
		internal decimal _pendiente_bultos;
		internal string _expediente = string.Empty;
        protected string _store_id = string.Empty;
        protected string _store = string.Empty;
		internal long _oid_stock;

		#endregion

		#region Properties

		public OrderLineRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
		internal ETipoFacturacion ETipoFacturacion { get { return (FacturacionPeso) ? ETipoFacturacion.Peso : ETipoFacturacion.Unidad; } }
		internal bool FacturacionPeso { get { return !_record.FacturacionBulto; } }
		internal bool IsKitComponent { get { return _record.OidKit> 0; } }
		internal Decimal BaseImponible { get { return _record.Subtotal - Descuento; } }
		internal Decimal Descuento { get { return Decimal.Round((_record.Subtotal * _record.PDescuento) / 100, 2); } }
		internal Decimal Impuestos { get { return Decimal.Round((_record.Subtotal * _record.PImpuestos) / 100, 2); } }
		internal Decimal Beneficio { get { return _record.CantidadKilos * BeneficioKilo; } }
		internal Decimal BeneficioKilo
		{
			get
			{
				if (_record.FacturacionBulto)
					return (_record.CantidadKilos > 0) ? (_record.Precio / (_record.CantidadKilos / _record.CantidadBultos)) - _record.Gastos : 0;
				else
					return _record.Precio - _record.Gastos;
			}
		}
		internal bool IsComplete { get { return (ETipoFacturacion == ETipoFacturacion.Peso) ? (_pendiente == 0) : (_pendiente_bultos == 0); } }
        public string StoreID { get { return _store_id; } set { _store_id = value; } }
        public string Store { get { return _store; } set { _store = value; } }
        public virtual string StoreIDStore { get { return _store_id + " - " + _store; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			OidExpedienteOld = _record.OidExpediente;

			_pendiente = Format.DataReader.GetInt64(source, "CANTIDAD_PENDIENTE");
			_pendiente_bultos = Format.DataReader.GetInt64(source, "CANTIDAD_BULTOS_PENDIENTE");
			_expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
            _store_id = Format.DataReader.GetString(source, "STORE_ID");
            _store = Format.DataReader.GetString(source, "STORE");
			_oid_stock = Format.DataReader.GetInt64(source, "OID_STOCK");
		}
		internal void CopyValues(LineaPedido source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_pendiente = source.Pendiente;
			_pendiente_bultos = source.PendienteBultos;
			_expediente = source.Expediente;
            _store_id = source.StoreID;
            _store = source.Almacen;
			_oid_stock = source.OidStock;
		}
		internal void CopyValues(LineaPedidoInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_pendiente = source.Pendiente;
			_pendiente_bultos = source.PendienteBultos;
			_expediente = source.Expediente;
            _store_id = source.StoreID;
            _store = source.Almacen;
			_oid_stock = source.OidStock;
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class LineaPedido : BusinessBaseEx<LineaPedido>
	{
		#region Attributes

		protected OrderLineBase _base = new OrderLineBase();		

		#endregion
	
		#region Properties

		public OrderLineBase Base { get { return _base; } }

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
		public virtual long OidPedido
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidPedido;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidPedido.Equals(value))
				{
					_base.Record.OidPedido = value;
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
		public virtual long Estado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Estado;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Estado.Equals(value))
				{
					_base.Record.Estado = value;
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
					if (!_base.Record.FacturacionBulto) CalculaTotal();
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
					if (_base.Record.FacturacionBulto) CalculaTotal();
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
					CalculaTotal();
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
		public virtual string Observaciones
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Observaciones;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Observaciones.Equals(value))
				{
					_base.Record.Observaciones = value;
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
					CalculaTotal();
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
					CalculaTotal();
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

        //NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
        public virtual string StoreID { get { return _base.StoreID; } set { _base.StoreID = value; } }
        public virtual string Almacen { get { return _base.Store; } set { _base.Store = value; } }
        public virtual string StoreIDStore { get { return _base.StoreIDStore; } }
		public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
		public virtual long OidStock { get { return _base._oid_stock; } }
		public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } set { PropertyHasChanged(); } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } set { PropertyHasChanged(); } }
		public virtual Decimal Beneficio { get { return _base.Beneficio; } }
		public virtual Decimal BeneficioKilo { get { return _base.BeneficioKilo; } }
		public virtual Decimal Pendiente { get { return _base._pendiente; } set { _base._pendiente = value; } }
		public virtual Decimal PendienteBultos { get { return _base._pendiente_bultos; } set { _base._pendiente_bultos = value; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { FacturacionBulto = !value; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
		public virtual bool IsComplete { get { return _base.IsComplete; } }

		#endregion
		
		#region Business Methods
		
		/// <summary>
		/// Clona la entidad y sus subentidades y las marca como nuevas
		/// </summary>
		/// <returns>Una entidad clon</returns>
		public virtual LineaPedido CloneAsNew()
		{
			LineaPedido clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.SessionCode = LineaPedido.OpenSession();
			LineaPedido.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}

		public virtual void CopyFrom(Pedido source)
		{
			OidPedido = source.Oid;
			OidAlmacen = source.OidAlmacen;
			OidExpediente = source.OidExpediente;

			Almacen = source.Almacen;
			Expediente = source.Expediente;
		}

		public virtual void CopyFrom(BatchInfo partida, ProductInfo producto)
		{
			CopyFrom(producto);
			CopyFrom(partida);
			
			CantidadKilos = 1;
			CantidadBultos = 1;

			if (partida != null) AjustaCantidad(partida);
			else AjustaCantidad(producto);

		}
		public virtual void CopyFrom(ProductInfo source)
		{
			Expediente = string.Empty;
			Concepto = source.Nombre;
			OidProducto = source.Oid;
			OidPartida = 0;
			OidKit = 0;
			OidAlmacen = 0;
			OidExpediente = 0;
			FacturacionPeso = (source.ETipoFacturacion == ETipoFacturacion.Peso);
		}
		public virtual void CopyFrom(BatchInfo source)
		{
			Expediente = source.Expediente;
			Concepto = source.TipoMercancia;
			OidProducto = source.OidProducto;
			OidPartida = source.Oid;
			OidAlmacen = source.OidAlmacen;
			Almacen = source.Almacen;
			OidExpediente = source.OidExpediente;
			OidKit = source.OidKit;

			Almacen = source.Almacen;
			Expediente = source.Expediente;
		}

		public virtual void AjustaCantidad(ProductInfo producto)
		{
			if (producto == null)
			{
				if (FacturacionPeso)
					CantidadBultos = CantidadKilos;
				else
					CantidadKilos = CantidadBultos;
			}
			else
			{
				if (FacturacionPeso)
					CantidadBultos = (producto.KilosBulto == 0) ? CantidadKilos : CantidadKilos / producto.KilosBulto;
				else
					CantidadKilos = (producto.KilosBulto == 0) ? CantidadBultos : CantidadBultos * producto.KilosBulto;
			}
		}
		public virtual void AjustaCantidad(BatchInfo partida)
		{
			if (FacturacionBulto) AjustaCantidadKilos(partida);
			else AjustaCantidadBultos(partida);
		}
		public virtual void AjustaCantidadBultos(BatchInfo partida)
		{
			if (partida.StockKilos == 0) return;

			if (CantidadKilos == partida.StockKilos)
				CantidadBultos = partida.StockBultos;
			else
				CantidadBultos = CantidadKilos / partida.KilosPorBulto;
		}
		public virtual void AjustaCantidadKilos(BatchInfo partida)
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

		public virtual void CalculaTotal()
		{
			Subtotal = (FacturacionBulto) ? CantidadBultos * Precio: CantidadKilos * Precio;
			Total = BaseImponible + Impuestos;

			//Para forzar el refresco en el formulario
			Impuestos = Impuestos;
			Descuento = Descuento;
		}

		public virtual void Reserva(SerieInfo serie, ClienteInfo cliente, ProductInfo producto) { Reserva(serie, cliente, producto, null); }
		public virtual void Reserva(SerieInfo serie, ClienteInfo cliente, ProductInfo producto, BatchInfo partida)
		{
			if (cliente == null)
				throw new iQException(Library.Invoice.Resources.Messages.NO_CLIENTE_SELECTED);
			
			if (cliente.Productos == null)
				cliente.LoadChilds(typeof(ProductoCliente), true);

			ProductoClienteInfo productoCliente = cliente.Productos.GetByProducto(producto.Oid);

			if (partida == null)
				CopyFrom(producto);
			else
				CopyFrom(partida);

			SetTipoFacturacion(productoCliente, producto);
			SetImpuestos(serie, cliente, producto);
			Precio = producto.GetPrecioVenta(productoCliente, partida, ETipoFacturacion);
		}

		public virtual void SetImpuestos(SerieInfo serie, ClienteInfo cliente, ProductInfo producto)
		{
			//Primero el cliente si está EXENTO
			if ((cliente != null) && (cliente.OidImpuesto == 1))
			{
				OidImpuesto = cliente.OidImpuesto;
				PImpuestos = cliente.PImpuesto;
			}
			//Luego el producto
			else if ((producto != null) && (producto.OidImpuestoVenta != 0))
			{
				OidImpuesto = producto.OidImpuestoVenta;
				PImpuestos = producto.PImpuestoVenta;
			}
			else if ((serie != null) && (serie.OidImpuesto != 0))
			{
				OidImpuesto = serie.OidImpuesto;
				PImpuestos = serie.PImpuesto;
			}
		}

		public virtual void SetTipoFacturacion(ClienteInfo cliente, ProductInfo producto)
		{
			if (cliente == null)
				throw new iQException(Library.Invoice.Resources.Messages.NO_CLIENTE_SELECTED);

			if (cliente.Productos == null)
				cliente.LoadChilds(typeof(ProductoCliente), true);

			ProductoClienteInfo pci = cliente.Productos.GetItemByProperty("OidProducto", producto.Oid);
			SetTipoFacturacion(pci, producto);
		}
		public virtual void SetTipoFacturacion(ProductoClienteInfo pci, ProductInfo producto)
		{
			if (pci != null)
				FacturacionBulto = pci.FacturacionBulto;
			else if (producto != null)
				FacturacionBulto = !(producto.ETipoFacturacion == ETipoFacturacion.Peso);
			else
				FacturacionBulto = false;
		}

		public virtual void SetPrecio(ClienteInfo cliente, ProductInfo producto, BatchInfo partida)
		{
			Precio = cliente.GetPrecio(producto, partida, ETipoFacturacion);
			PDescuento = cliente.GetDescuento(producto, partida);
			CalculaTotal();
		}

		#endregion
		 
	    #region Validation Rules

		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CheckValidation, "Oid");
		}

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{
			return true;
		}
		 
		#endregion
		 
		#region Autorization Rules
		
		public static bool CanAddObject()
		{
			return AutorizationRulesControler.CanAddObject(Resources.SecureItems.ALBARAN_EMITIDO);
		}		
		public static bool CanGetObject()
		{
			return AutorizationRulesControler.CanGetObject(Resources.SecureItems.ALBARAN_EMITIDO);
		}		
		public static bool CanDeleteObject()
		{
			return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.ALBARAN_EMITIDO);
		}		
		public static bool CanEditObject()
		{
			return AutorizationRulesControler.CanEditObject(Resources.SecureItems.ALBARAN_EMITIDO);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		public LineaPedido()
		{
			// Si se necesita constructor público para este objeto hay que añadir el MarkAsChild() debido a la interfaz Child
			// y el código que está en el DataPortal_Create debería ir aquí          
             MarkAsChild();
		}		
		private LineaPedido(LineaPedido source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
		private LineaPedido(int sessionCode, IDataReader source, bool childs)
        {
			SessionCode = sessionCode;
            MarkAsChild();
			Childs = childs;
            Fetch(source);
        }

		public static LineaPedido NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			LineaPedido obj = DataPortal.Create<LineaPedido>(new CriteriaCs(-1));
			return obj;
		}
		public static LineaPedido NewChild(Pedido parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			LineaPedido obj = DataPortal.Create<LineaPedido>(new CriteriaCs(-1));
			obj.CopyFrom(parent);
			return obj;
		}
		public static LineaPedido NewChild(Pedido parent, ProductInfo producto) 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			LineaPedido obj = DataPortal.Create<LineaPedido>(new CriteriaCs(-1));
			obj.OidPedido = parent.Oid;
			obj.CopyFrom(producto);

			return obj;
		}
		public static LineaPedido NewChild(Pedido parent, BatchInfo partida)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			LineaPedido obj = DataPortal.Create<LineaPedido>(new CriteriaCs(-1));
			obj.OidPedido = parent.Oid;
			obj.CopyFrom(partida);

			return obj;
		}

		internal static LineaPedido GetChild(LineaPedido source) { return new LineaPedido(source, false); }
		internal static LineaPedido GetChild(LineaPedido source, bool retrieve_childs)
		{
			return new LineaPedido(source, retrieve_childs);
		}
        internal static LineaPedido GetChild(int sessionCode, IDataReader source) { return new LineaPedido(sessionCode, source, false); }
        internal static LineaPedido GetChild(int sessionCode, IDataReader source, bool childs) { return new LineaPedido(sessionCode, source, childs); }
		
		public virtual LineaPedidoInfo GetInfo() { return GetInfo(true); }
		public virtual LineaPedidoInfo GetInfo (bool get_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new LineaPedidoInfo(this, get_childs);
		}
		
		#endregion				
		
		#region Child Factory Methods
		
		/// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LO UTILIZA LA FUNCION DE CREACION DE LA LISTA DEL PADRE
        /// </summary>
        private LineaPedido(PedidoProveedor parent)
        {
            OidPedido = parent.Oid;
            MarkAsChild();
        }
		
		internal static LineaPedido NewChild(PedidoProveedor parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return new LineaPedido(parent);
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
		/// No se debe utilizar esta función para guardar. Hace falta el padre, que
		/// debe utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override LineaPedido Save()
		{
			throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}
		
		#endregion
		
		#region Common Data Access
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			EEstado = EEstado.Abierto;
			CantidadKilos = 1;
			CantidadBultos = 1;	
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(LineaPedido source)
		{
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);
				
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
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

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(LineaPedidos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{	
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(LineaPedidos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				OrderLineRecord obj = Session().Get<OrderLineRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(LineaPedidos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<OrderLineRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}

		#endregion
		
		#region Child Data Access
		
		internal void Insert(Pedido parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidPedido = parent.Oid;

			try
			{
				ValidationRules.CheckRules();
				
				if (!IsValid)
					throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Save de la Linea");
#endif
				Batch partida = null;

				// Actualizamos el stock del Almacen asociado si no es un concepto libre
				if (this.OidPartida > 0)
				{
					Almacen almacen = Store.Almacen.Get(OidAlmacen, false, true, SessionCode);
#if TRACE
					ControlerBase.AppControler.Timer.Record("Obtención del Expediente");
#endif
					//Cargamos las partidas del almacen para actualizar los totales
					almacen.LoadPartidasByProducto(OidProducto, true);

					//La partida se encarga de crear la linea de stock en el almacen
					partida = almacen.Partidas.GetItem(OidPartida);
#if TRACE
				    ControlerBase.AppControler.Timer.Record("Carga de las Partidas");
#endif
					partida.CheckStock(this);

                    almacen.LoadStockByPartida(OidPartida, true, true);
					Stock stock = almacen.Stocks.NewItem(this);
					stock.OidKit = OidKit;
					stock.Fecha = parent.Fecha;
					stock.Observaciones = String.Format(Resources.Messages.RESERVA_POR_PEDIDO, parent.Codigo);
                    almacen.UpdateStocks(partida, true);
				}

				//Para cargarlo en cache y saber que luego hay que actualizarlo
				if (OidExpediente != 0) Store.Expedient.Get(OidExpediente, false, true, SessionCode);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Update(Pedido parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidPedido = parent.Oid;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			OrderLineRecord obj = parent.Session().Get<OrderLineRecord>(Oid);
			obj.CopyValues(Base.Record);
			parent.Session().Update(obj);
#if TRACE
			ControlerBase.AppControler.Timer.Record("Update del Concepto de Albarán");
#endif
            Batch partida = null;

			// Actualizamos el stock del Almacen asociado si no es un concepto libre
			if ((this.OidPartida > 0) && (OidStock != 0))
			{
				//parent.Session().Save(Base.Record);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Save del Concepto de Albarán");
#endif
				Almacen almacen = Store.Almacen.Get(OidAlmacen, false, true, SessionCode);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga del Expediente");
#endif
				//Cargamos las partidas del expediente para actualizar los totales
				almacen.LoadPartidasByProducto(OidProducto, true);
                almacen.LoadStockByPartida(OidPartida, true, true);

				partida = almacen.Partidas.GetItem(OidPartida);
				Stock stock = almacen.Stocks.GetItem(OidStock);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Carga de las Partidas");
#endif
				partida.CheckStock(this, stock);

				stock.CopyFrom(this);
				stock.Observaciones = String.Format(Resources.Messages.RESERVA_POR_PEDIDO, parent.Codigo);
                almacen.UpdateStocks(partida, true);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif
			}

			//Para cargarlo en cache y saber que luego hay que actualizarlo
			if ((OidExpediente != 0) && (OidPartida != 0)) Store.Expedient.Get(OidExpediente, false, true, SessionCode);
			if ((_base.OidExpedienteOld != 0) && (_base.OidExpedienteOld != OidExpediente)) Store.Expedient.Get(_base.OidExpedienteOld, false, true, SessionCode);

			// No hace falta actualizar hijos porque se encarga el almacen


			MarkOld();
		}

		internal void DeleteSelf(Pedido parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<OrderLineRecord>(Oid));

			if ((this.OidPartida > 0) && (OidStock != 0))
			{
				Almacen almacen = Store.Almacen.Get(OidAlmacen, false, true, SessionCode);

				//Cargamos las partidas del almacen para actualizar los totales
				almacen.LoadPartidasByProducto(OidProducto, true);
                almacen.LoadStockByPartida(OidPartida, true, true);

				//Esta función se escarga tb de actualizar el stock
				almacen.RemoveStock(OidStock, OidPartida);
#if TRACE
				ControlerBase.AppControler.Timer.Record("Regularización de Stocks");
#endif
			}

			//Para cargarlo en cache y saber que luego hay que actualizarlo
			if (OidExpediente != 0) Store.Expedient.Get(OidExpediente, false, true, SessionCode);

			MarkNew();
		}
		
		#endregion	
		
		#region SQL

		public new static string SELECT(long oid) { return SELECT(oid, false); }

		internal enum ETipoQuery { BASE = 0, PENDIENTE = 1 }

		internal static string SELECT_FIELDS(ETipoQuery tipoQuery)
		{
			string query;

			query = "SELECT " + (long)tipoQuery + " AS \"QUERY\"" +
					"		,LP.*" +
					"       ,ST.\"OID\" AS \"OID_STOCK\"" +
					"		,EX.\"CODIGO\" AS \"EXPEDIENTE\"" +
                    "		,COALESCE(AL.\"NOMBRE\", '') AS \"STORE\"" +
					"		,COALESCE(AL.\"CODIGO\", '') AS \"STORE_ID\"" +
					"		,(LP.\"CANTIDAD\" - COALESCE(CAC.\"CANTIDAD\", 0)) AS \"CANTIDAD_PENDIENTE\"" +
					"		,(LP.\"CANTIDAD_BULTOS\" - COALESCE(CAC.\"CANTIDAD_BULTOS\",0)) AS \"CANTIDAD_BULTOS_PENDIENTE\"";
	
			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query += " WHERE TRUE";

			query += EntityBase.ESTADO_CONDITION(conditions.Estado, "LP");

			if (conditions.LineaPedido != null) query += " AND LP.\"OID\" = " + conditions.LineaPedido.Oid;
			if (conditions.Pedido != null) query += " AND LP.\"OID_PEDIDO\" = " + conditions.Pedido.Oid;
			if (conditions.Expediente != null) query += " AND LP.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;
			if (conditions.Producto != null) query += " AND LP.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

			return query;
		}

		internal static string SELECT_BASE(QueryConditions conditions, ETipoQuery tipoQuery)
		{
			string lp = nHManager.Instance.GetSQLTable(typeof(OrderLineRecord));
			string st = nHManager.Instance.GetSQLTable(typeof(StockRecord));
			string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
			string cac = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryLineRecord));

			string query;

			query = SELECT_FIELDS(tipoQuery) +
					" FROM " + lp + " AS LP" +
					" LEFT JOIN " + st + " AS ST ON (ST.\"OID_LINEA_PEDIDO\" = LP.\"OID\" AND ST.\"TIPO\" = " + (long)ETipoStock.Reserva + ")" +
					" LEFT JOIN " + ex + " AS EX ON EX.\"OID\" = LP.\"OID_EXPEDIENTE\"" +
					" LEFT JOIN " + al + " AS AL ON AL.\"OID\" = ST.\"OID_ALMACEN\"";

			//Pendiente de Albarán
			query += " LEFT JOIN (SELECT \"OID_LINEA_PEDIDO\"" +
					 "						,SUM(\"CANTIDAD\") AS \"CANTIDAD\"" +
					 "						,SUM(\"CANTIDAD_BULTOS\") AS \"CANTIDAD_BULTOS\"" +
					 "						FROM " + cac + " AS CAC" +
					 "						WHERE CAC.\"OID_LINEA_PEDIDO\" != 0" +
					 "						GROUP BY CAC.\"OID_LINEA_PEDIDO\")" +
					 "	AS CAC ON CAC.\"OID_LINEA_PEDIDO\" = LP.\"OID\"";

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lock_table)
		{
			string query;

			query = SELECT_BASE(conditions, ETipoQuery.BASE) +
					WHERE(conditions);

			if (lock_table) query += " FOR UPDATE OF LP NOWAIT";

			return query;
		}

		internal static string SELECT(long oid, bool lock_table)
		{
			QueryConditions conditions = new QueryConditions { LineaPedido = LineaPedido.NewChild().GetInfo() };
			conditions.LineaPedido.Oid = oid;

			return SELECT(conditions, lock_table);
		}

		internal static string SELECT_PENDIENTES(QueryConditions conditions, bool lock_table)
		{
			string cac = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryLineRecord));

			string query;

			conditions.Estado = EEstado.NoAnulado;

			query = SELECT_BASE(conditions, ETipoQuery.PENDIENTE);
			
			query += WHERE(conditions);

			query += "	AND (LP.\"OID\", LP.\"CANTIDAD\") NOT IN (SELECT \"OID_LINEA_PEDIDO\"" +
					 "													,SUM(\"CANTIDAD\") AS \"CANTIDAD\"" +
					 "												FROM " + cac + " AS CAC" +
					 "												WHERE CAC.\"OID_LINEA_PEDIDO\" != 0" +
					 "												GROUP BY CAC.\"OID_LINEA_PEDIDO\")";

			if (lock_table) query += " FOR UPDATE OF LP NOWAIT";

			return query;
		}

		#endregion
	}
}

