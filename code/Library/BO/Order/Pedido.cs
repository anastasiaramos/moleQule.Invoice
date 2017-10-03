using System;using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class OrderRecord : RecordBase
	{
		#region Attributes

		private long _oid_usuario;
		private long _oid_cliente;
		private long _oid_producto;
		private long _serial;
		private string _codigo = string.Empty;
		private DateTime _fecha;
		private Decimal _total;
		private long _estado;
		private string _observaciones = string.Empty;
		private long _oid_serie;
		private Decimal _base_imponible;
		private Decimal _impuestos;
		private Decimal _p_descuento;
		private Decimal _descuento;
		private long _oid_almacen;
		private long _oid_expediente;
		#endregion

		#region Properties

		public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }
		public virtual long OidCliente { get { return _oid_cliente; } set { _oid_cliente = value; } }
		public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual long OidSerie { get { return _oid_serie; } set { _oid_serie = value; } }
		public virtual Decimal BaseImponible { get { return _base_imponible; } set { _base_imponible = value; } }
		public virtual Decimal Impuestos { get { return _impuestos; } set { _impuestos = value; } }
		public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
		public virtual Decimal Descuento { get { return _descuento; } set { _descuento = value; } }
		public virtual long OidAlmacen { get { return _oid_almacen; } set { _oid_almacen = value; } }
		public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }

		#endregion

		#region Business Methods

		public OrderRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
			_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
			_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_total = Format.DataReader.GetDecimal(source, "TOTAL");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
			_base_imponible = Format.DataReader.GetDecimal(source, "BASE_IMPONIBLE");
			_impuestos = Format.DataReader.GetDecimal(source, "IMPUESTOS");
			_p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
			_descuento = Format.DataReader.GetDecimal(source, "DESCUENTO");
			_oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");

		}
		public virtual void CopyValues(OrderRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_usuario = source.OidUsuario;
			_oid_cliente = source.OidCliente;
			_oid_producto = source.OidProducto;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_fecha = source.Fecha;
			_total = source.Total;
			_estado = source.Estado;
			_observaciones = source.Observaciones;
			_oid_serie = source.OidSerie;
			_base_imponible = source.BaseImponible;
			_impuestos = source.Impuestos;
			_p_descuento = source.PDescuento;
			_descuento = source.Descuento;
			_oid_almacen = source.OidAlmacen;
			_oid_expediente = source.OidExpediente;
		}

		#endregion
	}

	[Serializable()]
	public class OrderBase
	{
		#region Attributes

		private OrderRecord _record = new OrderRecord();

		internal string _usuario = string.Empty;
		internal string _expediente = string.Empty;
		internal string _almacen = string.Empty;
		internal string _id_almacen = string.Empty;
		internal string _n_serie = string.Empty;
		internal string _serie = string.Empty;
		internal long _id_cliente;
		internal string _cliente = string.Empty;
		internal bool _id_manual = false;

		#endregion

		#region Properties

		public OrderRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
		internal Decimal Subtotal { get { return _record.BaseImponible + _record.Descuento; } }
		internal virtual string IDAlmacenAlmacen { get { return _id_almacen + " - " + _almacen; } }
		internal virtual string NSerieSerie { get { return _n_serie + " - " + _serie; } }
		internal virtual string IDCliente { get { return _id_cliente.ToString(Resources.Defaults.CLIENTE_CODE_FORMAT); } set { try { _id_cliente = Convert.ToInt64(value); } catch { }; } }
		
		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_usuario = Format.DataReader.GetString(source, "USUARIO");
			_id_cliente = Format.DataReader.GetInt64(source, "ID_CLIENTE");
			_cliente = Format.DataReader.GetString(source, "CLIENTE");
			_serie = Format.DataReader.GetString(source, "SERIE");
			_n_serie = Format.DataReader.GetString(source, "N_SERIE");
			_expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
			_almacen = Format.DataReader.GetString(source, "ALMACEN");
			_id_almacen = Format.DataReader.GetString(source, "ID_ALMACEN");

			decimal pendiente = Format.DataReader.GetDecimal(source, "PENDIENTE");
			decimal pendiente_bultos = Format.DataReader.GetDecimal(source, "PENDIENTE_BULTOS");

			_record.Estado = (pendiente == 0 && pendiente_bultos == 0) ? (long)EEstado.Closed : (long)EEstado.Abierto;	
		}
		internal void CopyValues(Pedido source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			IDCliente = source.IDCliente;
			_cliente = source.Cliente;
			_usuario = source.Usuario;
			_n_serie = source.NSerie;
			_serie = source.Serie;
			_expediente = source.Expediente;
			_almacen = source.Almacen;
			_id_almacen = source.IDAlmacen;
		}
		internal void CopyValues(PedidoInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			IDCliente = source.IDCliente;
			_cliente = source.Cliente;
			_usuario = source.Usuario;
			_n_serie = source.NSerie;
			_serie = source.Serie;
			_expediente = source.Expediente;
			_almacen = source.Almacen;
			_id_almacen = source.IDAlmacen;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>	
    [Serializable()]
	public class Pedido : BusinessBaseEx<Pedido>
	{	 
		#region Attributes

		protected OrderBase _base = new OrderBase();

		private LineaPedidos _lineas_pedido = LineaPedidos.NewChildList();

		#endregion
		
		#region Properties

		public OrderBase Base { get { return _base; } }

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
		public virtual long OidUsuario
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidUsuario;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidUsuario.Equals(value))
				{
					_base.Record.OidUsuario = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidCliente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCliente;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCliente.Equals(value))
				{
					_base.Record.OidCliente = value;
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
		public virtual long Serial
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Serial;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Serial.Equals(value))
				{
					_base.Record.Serial = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Codigo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Codigo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Codigo.Equals(value))
				{
					_base.Record.Codigo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Fecha
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Fecha;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Fecha.Equals(value))
				{
					_base.Record.Fecha = value;
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
		public virtual long OidSerie
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidSerie;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidSerie.Equals(value))
				{
					_base.Record.OidSerie = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal BaseImponible
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.BaseImponible;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.BaseImponible.Equals(value))
				{
					_base.Record.BaseImponible = Decimal.Round(value, 2);
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Impuestos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Impuestos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Impuestos.Equals(value))
				{
					_base.Record.Impuestos = Decimal.Round(value, 2);
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
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Descuento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Descuento;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Descuento.Equals(value))
				{
					_base.Record.Descuento = Decimal.Round(value, 2);
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

		public virtual LineaPedidos Lineas
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _lineas_pedido;
			}
		}

		//NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EStatus; } set { _base.Record.Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
		public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
		public virtual string IDAlmacen { get { return _base._id_almacen; } set { _base._id_almacen = value; } }
		public virtual string Almacen { get { return _base._almacen; } set { _base._almacen = value; } }
		public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }
		public virtual string NSerie { get { return _base._n_serie; } set { _base._n_serie = value; } }
		public virtual string Serie { get { return _base._serie; } set { _base._serie = value; } }
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
		public virtual string IDCliente { get { return _base.IDCliente; } set { _base.IDCliente = value; } }
		public virtual string Cliente { get { return _base._cliente; } set { _base._cliente = value; } }
		public virtual bool IDManual { get { return _base._id_manual; } set { _base._id_manual = value; } }
		public virtual Decimal Subtotal { get { return _base.Subtotal; } }

		public override bool IsValid
		{
			get { return base.IsValid
						 && _lineas_pedido.IsValid ; }
		}
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _lineas_pedido.IsDirty ; }
		}
		
		#endregion
		
		#region Business Methods
		
		/// <summary>
		/// Clona la entidad y sus subentidades y las marca como nuevas
		/// </summary>
		/// <returns>Una entidad clon</returns>
		public virtual Pedido CloneAsNew()
		{
			Pedido clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.Codigo = (0).ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
			
			clon.SessionCode = Pedido.OpenSession();
			Pedido.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.Lineas.MarkAsNew();
			
			return clon;
		}
	
		public virtual void CopyFrom(ClienteInfo source)
		{
			if (source == null) return;

			OidCliente = source.Oid;
			Cliente = source.Nombre;
			IDCliente = source.NumeroClienteLabel;
		}

		public virtual void GetNewCode()
		{
			// Obtenemos el último serial de servicio
			Serial = SerialInfo.GetNextByYear(typeof(Pedido), Fecha.Year);

			Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
		}

		protected virtual void SetNewCode()
		{
			try
			{
				PedidoList list = PedidoList.GetList(Fecha.Year, false);

				if (list.GetItemByCodigo(Codigo) != null)
					throw new iQException(Resources.Messages.ID_PEDIDO_DUPLICADO);
				
				Serial = Convert.ToInt64(Codigo);
			}
			catch
			{
				throw new iQException(string.Format(Resources.Messages.ID_PEDIDO_INVALIDO, Resources.Defaults.FACTURA_CODE_FORMAT));
			}
		}

		public virtual void ChangeEstado(EEstado estado)
		{
			EntityBase.CheckChangeState(EEstado, estado);

			if (estado == EEstado.Anulado) IsPosibleDelete(Oid);

			EEstado = estado;
		}
		public static Pedido ChangeEstado(long oid, EEstado estado)
		{
			if (!CanChangeState())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			Pedido item = null;

			try
			{
				if (estado == EEstado.Anulado) IsPosibleDelete(oid);

				item = Pedido.Get(oid, false);

				EntityBase.CheckChangeState(item.EEstado, estado);

				item.EEstado = estado;
				item.Save();
			}
			finally
			{
				if (item != null) item.CloseSession();
			}

			return item;
		}
	
		public virtual void CalculateTotal()
		{
			BaseImponible = 0;
			Descuento = 0;
			Impuestos = 0;
			Total = 0;

			foreach (LineaPedido linea in Lineas)
			{
				if (!linea.IsKitComponent)
				{
					linea.CalculaTotal();

					BaseImponible += linea.BaseImponible;
					Impuestos += linea.Impuestos;
				}
			}

			//Descuento = BaseImponible * PDescuento / 100;
			BaseImponible -= Descuento;
			Total = BaseImponible + Impuestos;
		}

		public virtual bool IsComplete()
		{
			foreach (LineaPedido item in Lineas)
				if (!item.IsComplete) return false;

			return true;
		}
		public virtual void SetAlmacen(StoreInfo source)
		{
			foreach (LineaPedido item in Lineas)
			{
				item.OidAlmacen = source.Oid;
				item.Almacen = source.Nombre;
			}
		}
		public virtual void SetExpediente(ExpedientInfo source)
		{
			foreach (LineaPedido item in Lineas)
			{
				item.OidExpediente = source.Oid;
				item.Expediente = source.Codigo;
			}
		}

		#endregion
		 
	    #region Validation Rules

		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CheckValidation, "Oid");
		}

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{
			//Codigo
			if (Codigo == string.Empty)
			{
				e.Description = Resources.Messages.NO_ID_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

			//Serie
			if (OidSerie == 0)
			{
				e.Description = Resources.Messages.NO_SERIE_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

			//Cliente
			if (OidCliente == 0)
			{
				e.Description = Resources.Messages.NO_CLIENTE_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

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
		public static bool CanChangeState()
		{
			return AutorizationRulesControler.CanGetObject(Library.Common.Resources.SecureItems.ESTADO);
		}

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				Pedido = Pedido.New().GetInfo(false),
			};
			conditions.Pedido.Oid = oid;

			OutputDeliveryLineList conceptos = OutputDeliveryLineList.GetList(conditions, false);

			if (conceptos.Count > 0)
				throw new iQException(Resources.Messages.ALBARANES_ASOCIADOS);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Pedido() {}		
		private Pedido(Pedido source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Pedido(int sessionCode, IDataReader source, bool childs)
        {
			SessionCode = sessionCode;
            MarkAsChild();	
			Childs = childs;
            Fetch(source);
        }

		public static Pedido NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Pedido>(new CriteriaCs(-1));
		}
		
		internal static Pedido GetChild(Pedido source)
		{
			return new Pedido(source, false);
		}
		internal static Pedido GetChild(Pedido source, bool childs)
		{
			return new Pedido(source, childs);
		}
        internal static Pedido GetChild(int sessionCode, IDataReader source)
        {
            return new Pedido(sessionCode, source, false);
        }
		internal static Pedido GetChild(int sessionCode, IDataReader source, bool childs)
        {
			return new Pedido(sessionCode, source, childs);
        }
		
		public virtual PedidoInfo GetInfo() { return GetInfo(true); }
		public virtual PedidoInfo GetInfo (bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return new PedidoInfo(this, childs);
		}

		public virtual void LoadPendiente() { LoadPendiente(true); } 
		public virtual void LoadPendiente(bool childs)
		{
			if (IsNew) return;

			_lineas_pedido = LineaPedidos.GetPendientesChildList(this, childs);
		}

		#endregion
		
		#region Root Factory Methods
		
		public static Pedido New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Pedido>(new CriteriaCs(-1));
		}
		
		public static Pedido Get(long oid) { return Get(oid, true); }
		public static Pedido Get(long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Pedido.GetCriteria(Pedido.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Pedido.SELECT(oid);
			
			Pedido.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Pedido>(criteria);
		}
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            IsPosibleDelete(oid);

			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Pedido. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Pedido.OpenSession();
			ISession sess = Pedido.Session(sessCode);
			ITransaction trans = Pedido.BeginTransaction(sessCode);
			
			try
			{	
				sess.Delete("from OrderRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				Pedido.CloseSession(sessCode);
			}
		}
		
		public override Pedido Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                ValidationRules.CheckRules();
            }
            catch (iQValidationException ex)
            {
                iQExceptionHandler.TreatException(ex);
                return this;
            }

            try
            {
                base.Save();				
				
				_lineas_pedido.Update(this);				
				
				Stores almacenes = Cache.Instance.Get(typeof(Stores)) as Stores;
				if (almacenes != null)
				{
					Expedients expedientes = Cache.Instance.Get(typeof(Expedients)) as Expedients;

					if (expedientes != null)
					{
						foreach (Expedient item in expedientes)
						{
							foreach (Almacen almacen in almacenes)
							{
								almacen.LoadPartidasByExpediente(item.Oid, true);
                                item.UpdateTotalesProductos(almacen.Partidas, true);
							}

							if (item.ETipoExpediente == ETipoExpediente.Ganado)
							{
                                LivestockBook libro = LivestockBook.Get(1, true, true, SessionCode);
								if (libro != null) libro.SaveAsChild();
							}

							expedientes.SaveAsChild();
#if TRACE
				ControlerBase.AppControler.Timer.Record("Save de los Expedients");
#endif
						}
					}

					almacenes.SaveAsChild();
				}
#if TRACE
				ControlerBase.AppControler.Timer.Record("almacenes.Save()");
#endif
				Transaction().Commit();
                return this;
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally
            {
				Cache.Instance.Remove(typeof(Stores));
				Cache.Instance.Remove(typeof(Expedients));

                if (CloseSessions) CloseSession(); 
				else BeginTransaction();
            }
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
			GetNewCode();
			Fecha = DateTime.Now;
			EEstado = EEstado.Abierto;
			OidUsuario = AppContext.User.Oid;
			Usuario = AppContext.User.Name;
			OidSerie = Library.Store.ModulePrincipal.GetDefaultSerieSetting();
			OidAlmacen = Library.Store.ModulePrincipal.GetDefaultAlmacenSetting();
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Pedido source)
		{
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);
	
				if (Childs)
                {
					if (nHMng.UseDirectSQL)
                    {
                        
						LineaPedido.DoLOCK(Session());
                        string query = LineaPedidos.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
						_lineas_pedido = LineaPedidos.GetChildList(SessionCode, reader, false);						
                    }
                }
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

                if (Childs)
                {
					if (nHMng.UseDirectSQL)
                    {                        
						LineaPedido.DoLOCK(Session());
                        string query = LineaPedidos.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _lineas_pedido = LineaPedidos.GetChildList(SessionCode, reader, false);						
                    }
                }
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
		internal void Insert(Pedidos parent)
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
		internal void Update(Pedidos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				OrderRecord obj = Session().Get<OrderRecord>(Oid);
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
		internal void DeleteSelf(Pedidos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<OrderRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}

		#endregion
		
		#region Root Data Access
		
		/// <summary>
		/// Obtiene un registro de la base de datos
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>Lo llama el DataPortal tras generar el objeto</remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					Pedido.DoLOCK( Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;
						
						LineaPedido.DoLOCK( Session());
						query = LineaPedidos.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_lineas_pedido = LineaPedidos.GetChildList(SessionCode, reader);						
 					} 
				}
				MarkOld();
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		/// <summary>
		/// Inserta un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isNew</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			try
			{
				if (!IDManual)
					GetNewCode();
				else
					SetNewCode();

                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
				
				Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
					if (IDManual) SetNewCode();

					OrderRecord obj = Session().Get<OrderRecord>(Oid);
					obj.CopyValues(Base.Record);
					Session().Update(obj);
					MarkOld();
				}
				catch (Exception ex)
				{
					iQExceptionHandler.TreatException(ex);
				}
			}
		}
		
		/// <summary>
		/// Borrado aplazado, no se ejecuta hasta que se llama al Save
		/// </summary>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}
		
		/// <summary>
		/// Elimina un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal</remarks>
		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();
					
                Pedido obj = Pedido.Get(criteria.Oid);
                obj.BeginEdit();
                obj.Lineas.Clear();
                obj.ApplyEdit();
                obj.Save();
                obj.CloseSession();				Session().Delete(Session().Get<OrderRecord>(obj.Oid));				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				CloseSession();
			}
		}		
		
		#endregion		
		
		#region SQL

		public new static string SELECT(long oid) { return SELECT(oid, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = "SELECT PC.*" +
					"       ,COALESCE(CL.\"NOMBRE\", '') AS \"CLIENTE\"" +
					"		,COALESCE(CL.\"CODIGO\", '') AS \"ID_CLIENTE\"" +
					"		,US.\"NAME\" AS \"USUARIO\"" +
					"		,SE.\"IDENTIFICADOR\" AS \"N_SERIE\"" +
					"		,SE.\"NOMBRE\" AS \"SERIE\"" +
					"       ,COALESCE(AL.\"CODIGO\", '') AS \"ID_ALMACEN\"" +
					"       ,COALESCE(AL.\"NOMBRE\", '') AS \"ALMACEN\"" +
					"       ,COALESCE(EX.\"CODIGO\", '') AS \"EXPEDIENTE\"" +
					"		,COALESCE(LP.\"CANTIDAD_PENDIENTE\", 0) AS \"PENDIENTE\"" +
					"		,COALESCE(LP.\"CANTIDAD_BULTOS_PENDIENTE\", 0) AS \"PENDIENTE_BULTOS\"";

			return query;
		}

		internal static string WHERE(Library.Invoice.QueryConditions conditions)
		{
			string query = string.Empty;

			query += " WHERE (PC.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			query += EntityBase.ESTADO_CONDITION(conditions.Estado, "PC");

			if (conditions.Pedido != null)
			{
				if (conditions.Pedido.Oid != 0)
					query += " AND PC.\"OID\" = " + conditions.Pedido.Oid;
			}

			if (conditions.User != null) query += " AND US.\"OID\" = " + conditions.User.Oid;
			if (conditions.Cliente != null) query += " AND PC.\"OID_CLIENTE\" = " + conditions.Cliente.Oid;
			if (conditions.Almacen != null) query += " AND PC.\"OID_ALMACEN\" = " + conditions.Almacen.Oid;
			if (conditions.Expediente != null) query += " AND PC.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;

			return query;
		}

		internal static string SELECT_BASE(Library.Invoice.QueryConditions conditions)
		{
			string pc = nHManager.Instance.GetSQLTable(typeof(OrderRecord));
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string se = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
			string lp = nHManager.Instance.GetSQLTable(typeof(OrderLineRecord));
			string cac = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryLineRecord));

			string query = string.Empty;

			query = SELECT_FIELDS() +
					" FROM " + pc + " AS PC" +
					" INNER JOIN " + se + " AS SE ON SE.\"OID\" = PC.\"OID_SERIE\"" +
					" INNER JOIN " + cl + " AS CL ON PC.\"OID_CLIENTE\" = CL.\"OID\"" +
					" INNER JOIN " + us + " AS US ON PC.\"OID_USUARIO\" = US.\"OID\"" +
					" LEFT JOIN " + al + " AS AL ON AL.\"OID\" = PC.\"OID_ALMACEN\"" +
					" LEFT JOIN " + ex + " AS EX ON EX.\"OID\" = PC.\"OID_EXPEDIENTE\"" +
					" LEFT JOIN (SELECT LP.\"OID_PEDIDO\"" +
					"					,SUM((LP.\"CANTIDAD\" - COALESCE(CAC.\"CANTIDAD\", 0))) AS \"CANTIDAD_PENDIENTE\"" +
					"					,SUM((LP.\"CANTIDAD_BULTOS\" - COALESCE(CAC.\"CANTIDAD_BULTOS\",0))) AS \"CANTIDAD_BULTOS_PENDIENTE\"" +
					"				FROM " + lp + " AS LP" +
					"				LEFT JOIN (SELECT \"OID_LINEA_PEDIDO\"" +
					"									,SUM(\"CANTIDAD\") AS \"CANTIDAD\"" +
					"									,SUM(\"CANTIDAD_BULTOS\") AS \"CANTIDAD_BULTOS\"" +
					"							FROM " + cac + " AS CAC" +
					"							WHERE CAC.\"OID_LINEA_PEDIDO\" != 0" +
					"							GROUP BY CAC.\"OID_LINEA_PEDIDO\")" +
					"				AS CAC ON CAC.\"OID_LINEA_PEDIDO\" = LP.\"OID\"" +
					"				GROUP BY LP.\"OID_PEDIDO\")" +
					"	AS LP ON LP.\"OID_PEDIDO\" = PC.\"OID\"" ;

			return query;
		}

		internal static string SELECT(Library.Invoice.QueryConditions conditions, bool lock_table)
		{
			string query = string.Empty;

			query = SELECT_BASE(conditions);

			query += WHERE(conditions);

			query += " ORDER BY PC.\"FECHA\", PC.\"CODIGO\"";

			if (lock_table) query += " FOR UPDATE OF PC NOWAIT";

			return query;
		}

		internal static string SELECT_PENDIENTES(Library.Invoice.QueryConditions conditions, bool lock_table)
		{
			string lpc = nHManager.Instance.GetSQLTable(typeof(OrderLineRecord));
			string cap = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryLineRecord));

			string query = string.Empty;

			conditions.Estado = EEstado.NoAnulado;

			query = SELECT_BASE(conditions);

			query += " INNER JOIN (SELECT LPC.\"OID_PEDIDO\"" +
					 "				FROM " + lpc + "AS LPC" +
					 "				WHERE (LPC.\"OID\", LPC.\"CANTIDAD\") NOT IN (SELECT \"OID_LINEA_PEDIDO\"" +
					 "																	,SUM(\"CANTIDAD\") AS \"CANTIDAD\"" +
					 "																FROM " + cap + " AS CAP" +
					 "																WHERE CAP.\"OID_LINEA_PEDIDO\" != 0" +
					 "																GROUP BY CAP.\"OID_LINEA_PEDIDO\")" +
					 "				GROUP BY LPC.\"OID_PEDIDO\")" +
					 "		AS LPC ON LPC.\"OID_PEDIDO\" = PC.\"OID\"";

			query += WHERE(conditions);

			query += " ORDER BY PC.\"FECHA\", PC.\"CODIGO\"";

			if (lock_table) query += " FOR UPDATE OF PC NOWAIT";

			return query;
		}

		internal static string SELECT(long oid, bool lock_table)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Pedido = Pedido.New().GetInfo(false) };
			conditions.Pedido.Oid = oid;

			query = SELECT_BASE(conditions);

			query += WHERE(conditions);

			if (lock_table) query += " FOR UPDATE OF PC NOWAIT";

			return query;
		}

		#endregion		
	}
}

