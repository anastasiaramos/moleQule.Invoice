using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;

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
	public class ClientRecord : RecordBase
	{
		#region Attributes

		private long _oid_ext;
		private long _serial;
		private string _codigo = string.Empty;
		private long _estado;
		private long _tipo_id;
		private string _vat_number = string.Empty;
		private string _nombre = string.Empty;
		private string _nombre_comercial = string.Empty;
		private string _titular = string.Empty;
		private string _direccion = string.Empty;
		private string _poblacion = string.Empty;
		private string _codigo_postal = string.Empty;
		private string _provincia = string.Empty;
		private string _telefonos = string.Empty;
		private string _fax = string.Empty;
		private string _prefix = string.Empty;
		private string _movil = string.Empty;
		private string _municipio = string.Empty;
		private string _country = string.Empty;
		private string _email = string.Empty;
		private DateTime _birth_date;
		private string _observaciones = string.Empty;
		private string _historia = string.Empty;
		private Decimal _limite_credito;
		private string _contacto = string.Empty;
		private long _medio_pago;
		private long _forma_pago;
		private long _dias_pago;
		private string _codigo_explotacion = string.Empty;
		private string _cuenta_bancaria = string.Empty;
		private string _swift = string.Empty;
		private long _oid_cuenta_bancaria_asociada;
		private Decimal _descuento;
		private Decimal _precio_transporte;
		private long _oid_transporte;
		private string _cuenta_contable = string.Empty;
		private long _oid_impuesto;
		private Decimal _tipo_interes;
		private Decimal _p_descuento_pto_pago;
		private long _prioridad_precio;
		private bool _enviar_factura_pendiente = false;

		#endregion

		#region Properties

		public virtual long OidExt { get { return _oid_ext; } set { _oid_ext = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual long TipoId { get { return _tipo_id; } set { _tipo_id = value; } }
		public virtual string VatNumber { get { return _vat_number; } set { _vat_number = value; } }
		public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
		public virtual string NombreComercial { get { return _nombre_comercial; } set { _nombre_comercial = value; } }
		public virtual string Titular { get { return _titular; } set { _titular = value; } }
		public virtual string Direccion { get { return _direccion; } set { _direccion = value; } }
		public virtual string Poblacion { get { return _poblacion; } set { _poblacion = value; } }
		public virtual string CodigoPostal { get { return _codigo_postal; } set { _codigo_postal = value; } }
		public virtual string Provincia { get { return _provincia; } set { _provincia = value; } }
		public virtual string Telefonos { get { return _telefonos; } set { _telefonos = value; } }
		public virtual string Fax { get { return _fax; } set { _fax = value; } }
		public virtual string Prefix { get { return _prefix; } set { _prefix = value; } }
		public virtual string Movil { get { return _movil; } set { _movil = value; } }
		public virtual string Municipio { get { return _municipio; } set { _municipio = value; } }
		public virtual string Country { get { return _country; } set { _country = value; } }
		public virtual string Email { get { return _email; } set { _email = value; } }
		public virtual DateTime BirthDate { get { return _birth_date; } set { _birth_date = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual string Historia { get { return _historia; } set { _historia = value; } }
		public virtual Decimal LimiteCredito { get { return _limite_credito; } set { _limite_credito = value; } }
		public virtual string Contacto { get { return _contacto; } set { _contacto = value; } }
		public virtual long MedioPago { get { return _medio_pago; } set { _medio_pago = value; } }
		public virtual long FormaPago { get { return _forma_pago; } set { _forma_pago = value; } }
		public virtual long DiasPago { get { return _dias_pago; } set { _dias_pago = value; } }
		public virtual string CodigoExplotacion { get { return _codigo_explotacion; } set { _codigo_explotacion = value; } }
		public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
		public virtual string Swift { get { return _swift; } set { _swift = value; } }
		public virtual long OidCuentaBancariaAsociada { get { return _oid_cuenta_bancaria_asociada; } set { _oid_cuenta_bancaria_asociada = value; } }
		public virtual Decimal Descuento { get { return _descuento; } set { _descuento = value; } }
		public virtual Decimal PrecioTransporte { get { return _precio_transporte; } set { _precio_transporte = value; } }
		public virtual long OidTransporte { get { return _oid_transporte; } set { _oid_transporte = value; } }
		public virtual string CuentaContable { get { return _cuenta_contable; } set { _cuenta_contable = value; } }
		public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
		public virtual Decimal TipoInteres { get { return _tipo_interes; } set { _tipo_interes = value; } }
		public virtual Decimal PDescuentoPtoPago { get { return _p_descuento_pto_pago; } set { _p_descuento_pto_pago = value; } }
		public virtual long PrioridadPrecio { get { return _prioridad_precio; } set { _prioridad_precio = value; } }
		public virtual bool EnviarFacturaPendiente { get { return _enviar_factura_pendiente; } set { _enviar_factura_pendiente = value; } }

		#endregion

		#region Business Methods

		public ClientRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_ext = Format.DataReader.GetInt64(source, "OID_EXT");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_tipo_id = Format.DataReader.GetInt64(source, "TIPO_ID");
			_vat_number = Format.DataReader.GetString(source, "VAT_NUMBER");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_nombre_comercial = Format.DataReader.GetString(source, "NOMBRE_COMERCIAL");
			_titular = Format.DataReader.GetString(source, "TITULAR");
			_direccion = Format.DataReader.GetString(source, "DIRECCION");
			_poblacion = Format.DataReader.GetString(source, "POBLACION");
			_codigo_postal = Format.DataReader.GetString(source, "CODIGO_POSTAL");
			_provincia = Format.DataReader.GetString(source, "PROVINCIA");
			_country = Format.DataReader.GetString(source, "COUNTRY");
			_telefonos = Format.DataReader.GetString(source, "TELEFONOS");
			_fax = Format.DataReader.GetString(source, "FAX");
			_prefix = Format.DataReader.GetString(source, "PREFIX");
			_movil = Format.DataReader.GetString(source, "MOVIL");
			_municipio = Format.DataReader.GetString(source, "MUNICIPIO");
			_email = Format.DataReader.GetString(source, "EMAIL");
			_birth_date = Format.DataReader.GetDateTime(source, "BIRTH_DATE");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_historia = Format.DataReader.GetString(source, "HISTORIA");
			_limite_credito = Format.DataReader.GetDecimal(source, "LIMITE_CREDITO");
			_contacto = Format.DataReader.GetString(source, "CONTACTO");
			_medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
			_forma_pago = Format.DataReader.GetInt64(source, "FORMA_PAGO");
			_dias_pago = Format.DataReader.GetInt64(source, "DIAS_PAGO");
			_codigo_explotacion = Format.DataReader.GetString(source, "CODIGO_EXPLOTACION");
			_cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
			_swift = Format.DataReader.GetString(source, "SWIFT");
			_oid_cuenta_bancaria_asociada = Format.DataReader.GetInt64(source, "OID_CUENTA_BANCARIA_ASOCIADA");
			_descuento = Format.DataReader.GetDecimal(source, "DESCUENTO");
			_precio_transporte = Format.DataReader.GetDecimal(source, "PRECIO_TRANSPORTE");
			_oid_transporte = Format.DataReader.GetInt64(source, "OID_TRANSPORTE");
			_cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
			_oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
			_tipo_interes = Format.DataReader.GetDecimal(source, "TIPO_INTERES");
			_p_descuento_pto_pago = Format.DataReader.GetDecimal(source, "P_DESCUENTO_PTO_PAGO");
			_prioridad_precio = Format.DataReader.GetInt64(source, "PRIORIDAD_PRECIO");
			_enviar_factura_pendiente = Format.DataReader.GetBool(source, "ENVIAR_FACTURA_PENDIENTE");

		}
		public virtual void CopyValues(ClientRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_ext = source.OidExt;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_estado = source.Estado;
			_tipo_id = source.TipoId;
			_vat_number = source.VatNumber;
			_nombre = source.Nombre;
			_nombre_comercial = source.NombreComercial;
			_titular = source.Titular;
			_direccion = source.Direccion;
			_poblacion = source.Poblacion;
			_codigo_postal = source.CodigoPostal;
			_provincia = source.Provincia;
			_telefonos = source.Telefonos;
			_fax = source.Fax;
			_prefix = source.Prefix;
			_movil = source.Movil;
			_municipio = source.Municipio;
			_country = source.Country;
			_email = source.Email;
			_birth_date = source.BirthDate;
			_observaciones = source.Observaciones;
			_historia = source.Historia;
			_limite_credito = source.LimiteCredito;
			_contacto = source.Contacto;
			_medio_pago = source.MedioPago;
			_forma_pago = source.FormaPago;
			_dias_pago = source.DiasPago;
			_codigo_explotacion = source.CodigoExplotacion;
			_cuenta_bancaria = source.CuentaBancaria;
			_swift = source.Swift;
			_oid_cuenta_bancaria_asociada = source.OidCuentaBancariaAsociada;
			_descuento = source.Descuento;
			_precio_transporte = source.PrecioTransporte;
			_oid_transporte = source.OidTransporte;
			_cuenta_contable = source.CuentaContable;
			_oid_impuesto = source.OidImpuesto;
			_tipo_interes = source.TipoInteres;
			_p_descuento_pto_pago = source.PDescuentoPtoPago;
			_prioridad_precio = source.PrioridadPrecio;
			_enviar_factura_pendiente = source.EnviarFacturaPendiente;
		}

		#endregion
	}

	[Serializable()]
	public class ClientBase
	{
		#region Attributes

		private ClientRecord _record = new ClientRecord();
		private User _user = User.New();

		public Decimal _total_facturado;
		public Decimal _credito_dispuesto;
		public string _cuenta_asociada = string.Empty;
		public string _impuesto = string.Empty;
		public Decimal _p_impuesto;

		#endregion

		#region Properties

		public ClientRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
		public EMedioPago EMedioPago { get { return (EMedioPago)_record.MedioPago; } }
		public string MedioPagoLabel { get { return Common.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
		public EFormaPago EFormaPago { get { return (EFormaPago)_record.FormaPago; } }
		public string FormaPagoLabel { get { return Common.EnumText<EFormaPago>.GetLabel(EFormaPago); } }
		public EPrioridadPrecio EPrioridadPrecio { get { return (EPrioridadPrecio)_record.PrioridadPrecio; } set { _record.PrioridadPrecio = (long)value; } }
		public string PrioridadPrecioLabel { get { return Library.Store.EnumText<EPrioridadPrecio>.GetLabel(EPrioridadPrecio); } }
		public string Impuesto { get { return (_record.OidImpuesto != 0) ? _impuesto : Library.Common.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto); } set { _impuesto = value; } }
		public string NumeroClienteLabel { get { return _record.Codigo; } } /*DEPRECATED*/

		// IUser
		public virtual long OidUser { get { return _user.Oid; } set { _user.Oid = value; } }
		public virtual string Username { get { return _user.Name; } set { _user.Name = value; } }
		public virtual EEstadoItem EUserStatus { get { return _user.EEstado; } set { _user.EEstado = value; } }
		public virtual string UserStatusLabel { get { return _user.EstadoLabel; } }
		public virtual DateTime CreationDate { get { return _user.CreationDate; } set { _user.CreationDate = value; } }
		public virtual DateTime LastLoginDate { get { return _user.LastLoginDate; } set { _user.LastLoginDate = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			//IUser
			_user.Oid = Format.DataReader.GetInt64(source, "OID_USER");
			_user.Name = Format.DataReader.GetString(source, "USERNAME");
			_user.Estado = Format.DataReader.GetInt64(source, "USER_STATUS");
			_user.CreationDate = Format.DataReader.GetDateTime(source, "CREATION_DATE");
			_user.LastLoginDate = Format.DataReader.GetDateTime(source, "LAST_LOGIN_DATE");
			
			_total_facturado = Format.DataReader.GetDecimal(source, "TOTAL_FACTURADO");
			_cuenta_asociada = Format.DataReader.GetString(source, "CUENTA_ASOCIADA");
			_credito_dispuesto = Format.DataReader.GetDecimal(source, "CREDITO_DISPUESTO");
			_impuesto = Format.DataReader.GetString(source, "IMPUESTO");
			_p_impuesto = Format.DataReader.GetDecimal(source, "P_IMPUESTO");
		}
		public void CopyValues(Cliente source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_cuenta_asociada = source.CuentaAsociada;
			_impuesto = source.Impuesto;
			_p_impuesto = source.PImpuesto;
			_credito_dispuesto = source.CreditoDispuesto;
			_total_facturado = source.TotalFacturado;

			//IUser
			_user.Oid = source.OidUser;
			_user.Name = source.Username;
			_user.EEstado = source.EUserStatus;
			_user.CreationDate = source.CreationDate;
			_user.LastLoginDate = source.LastLoginDate;
		}
		public void CopyValues(ClienteInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_cuenta_asociada = source.CuentaAsociada;
			_impuesto = source.Impuesto;
			_p_impuesto = source.PImpuesto;
			_credito_dispuesto = source.CreditoDispuesto;
			_total_facturado = source.TotalFacturado;

			//IUser
			_user.Oid = source.OidUser;
			_user.Name = source.Username;
			_user.EEstado = source.EUserStatus;
			_user.CreationDate = source.CreationDate;
			_user.LastLoginDate = source.LastLoginDate;
		}

		public string GetCrypFileName(long oid, EFile file)
		{
			return AppControllerBase.GetCryptFileName(oid, file.ToString() + "_" + oid.ToString()) + ".jpg";
		}

		public string GetAbsolutePath(long oid)
		{
			return Path.Combine(new string[] {	
									AppContext.StartUpPath,
									SettingsMng.Instance.GetDataPath(),
									Resources.Paths.CLIENTS_PATH,
									AppContext.ActiveSchema.SchemaCode,
									oid.ToString(Library.Resources.Defaults.PATH_FORMAT) 
								});
		}
		public string GetRelativePath(long oid)
		{
			return Path.Combine(new string[] {	
									SettingsMng.Instance.GetDataPath(),
									Resources.Paths.CLIENTS_PATH,
									AppContext.ActiveSchema.SchemaCode,
									oid.ToString(Library.Resources.Defaults.PATH_FORMAT) 
								});
		}

		#endregion
	}
	
	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>
    [Serializable()]
	public class Cliente : BusinessBaseEx<Cliente>, IUser, ITitular, IEntidadRegistro
	{
		#region IUser

		public virtual long OidUser { get { return _base.OidUser; } set { _base.OidUser = value; } }
		public virtual string Username { get { return _base.Username; } set { _base.Username = value; } }
		public virtual EEstadoItem EUserStatus { get { return _base.EUserStatus; } set { _base.EUserStatus = value; } }
		public virtual string UserStatusLabel { get { return _base.UserStatusLabel; } }
		public virtual DateTime CreationDate { get { return _base.CreationDate; } set { _base.CreationDate = value; } }
		public virtual DateTime LastLoginDate { get { return _base.LastLoginDate; } set { _base.LastLoginDate = value; } }

		#endregion

		#region ITitular

		public virtual ETipoTitular ETipoTitular { get { return ETipoTitular.Cliente; } }

        #endregion

		#region IEntidadRegistro

		public virtual ETipoEntidad ETipoEntidad { get { return ETipoEntidad.Cliente; } }
		public string DescripcionRegistro { get { return "CLIENTE Nº " + Codigo; } }

		public virtual IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }
		public virtual IEntidadRegistro IGet(long oid, bool childs) { return (IEntidadRegistro)Get(oid, childs); }

		public void Update(Registro parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
			ClientRecord obj = Session().Get<ClientRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			MarkOld();
		}

		#endregion

        #region Attributes

		protected ClientBase _base = new ClientBase();

        private Charges _cobros = Charges.NewChildList();
		private ProductoClientes _producto_clientes = ProductoClientes.NewChildList();

        private LineaRegistroList _emails = null;

        #endregion

        #region Properties

		public ClientBase Base { get { return _base; } }

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
		public virtual long OidExt
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidExt;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidExt.Equals(value))
				{
					_base.Record.OidExt = value;
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
		public virtual long TipoId
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoId;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.TipoId.Equals(value))
				{
					_base.Record.TipoId = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string VatNumber
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.VatNumber;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.VatNumber.Equals(value))
				{
					_base.Record.VatNumber = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Nombre
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Nombre;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Nombre.Equals(value))
				{
					_base.Record.Nombre = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string NombreComercial
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.NombreComercial;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.NombreComercial.Equals(value))
				{
					_base.Record.NombreComercial = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Titular
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Titular;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Titular.Equals(value))
				{
					_base.Record.Titular = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Direccion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Direccion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Direccion.Equals(value))
				{
					_base.Record.Direccion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Poblacion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Poblacion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Poblacion.Equals(value))
				{
					_base.Record.Poblacion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CodigoPostal
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CodigoPostal;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CodigoPostal.Equals(value))
				{
					_base.Record.CodigoPostal = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Provincia
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Provincia;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Provincia.Equals(value))
				{
					_base.Record.Provincia = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Telefonos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Telefonos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Telefonos.Equals(value))
				{
					_base.Record.Telefonos = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Fax
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Fax;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Fax.Equals(value))
				{
					_base.Record.Fax = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Prefix
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Prefix;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Prefix.Equals(value))
				{
					_base.Record.Prefix = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Movil
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Movil;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Movil.Equals(value))
				{
					_base.Record.Movil = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Municipio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Municipio;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Municipio.Equals(value))
				{
					_base.Record.Municipio = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Country
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Country;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Country.Equals(value))
				{
					_base.Record.Country = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Email
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Email;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Email.Equals(value))
				{
					_base.Record.Email = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime BirthDate
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.BirthDate;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.BirthDate.Equals(value))
				{
					_base.Record.BirthDate = value;
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
		public virtual string Historia
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Historia;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Historia.Equals(value))
				{
					_base.Record.Historia = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal LimiteCredito
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.LimiteCredito;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.LimiteCredito.Equals(value))
				{
					_base.Record.LimiteCredito = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Contacto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Contacto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Contacto.Equals(value))
				{
					_base.Record.Contacto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long MedioPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.MedioPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.MedioPago.Equals(value))
				{
					_base.Record.MedioPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long FormaPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FormaPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FormaPago.Equals(value))
				{
					_base.Record.FormaPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long DiasPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.DiasPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.DiasPago.Equals(value))
				{
					_base.Record.DiasPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CodigoExplotacion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CodigoExplotacion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CodigoExplotacion.Equals(value))
				{
					_base.Record.CodigoExplotacion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaBancaria
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CuentaBancaria;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CuentaBancaria.Equals(value))
				{
					_base.Record.CuentaBancaria = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Swift
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Swift;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Swift.Equals(value))
				{
					_base.Record.Swift = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidCuentaBancariaAsociada
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCuentaBancariaAsociada;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCuentaBancariaAsociada.Equals(value))
				{
					_base.Record.OidCuentaBancariaAsociada = value;
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
				return _base.Record.Descuento;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Descuento.Equals(value))
				{
					_base.Record.Descuento = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal PrecioTransporte
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PrecioTransporte;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PrecioTransporte.Equals(value))
				{
					_base.Record.PrecioTransporte = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidTransporte
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidTransporte;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidTransporte.Equals(value))
				{
					_base.Record.OidTransporte = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaContable
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CuentaContable;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CuentaContable.Equals(value))
				{
					_base.Record.CuentaContable = value;
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
		public virtual Decimal TipoInteres
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoInteres;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.TipoInteres.Equals(value))
				{
					_base.Record.TipoInteres = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal PDescuentoPtoPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PDescuentoPtoPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PDescuentoPtoPago.Equals(value))
				{
					_base.Record.PDescuentoPtoPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long PrioridadPrecio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PrioridadPrecio;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PrioridadPrecio.Equals(value))
				{
					_base.Record.PrioridadPrecio = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool EnviarFacturaPendiente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.EnviarFacturaPendiente;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.EnviarFacturaPendiente.Equals(value))
				{
					_base.Record.EnviarFacturaPendiente = value;
					PropertyHasChanged();
				}
			}
		}

        public virtual Charges Cobros
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _cobros;
			}
		}		
		public virtual ProductoClientes Productos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _producto_clientes;
			}
		}
        public virtual LineaRegistroList Emails { get { return _emails; } set { _emails = value; } }

		//UNMAPPED
		public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; PropertyHasChanged(); } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual EPrioridadPrecio EPrioridadPrecio { get { return _base.EPrioridadPrecio; } set { _base.EPrioridadPrecio = value; } }
		public virtual string PrioridadPrecioLabel { get { return _base.PrioridadPrecioLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } set { MedioPago = (long)value; PropertyHasChanged(); } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } set { FormaPago = (long)value; PropertyHasChanged(); } }
		public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public virtual Decimal TotalFacturado { get { return _base._total_facturado; } set { _base._total_facturado = value; } }
		public virtual Decimal CreditoDispuesto { get { return _base._credito_dispuesto; } set { _base._credito_dispuesto = value; } }
		public virtual string CuentaAsociada { get { return _base._cuenta_asociada; } set { _base._cuenta_asociada = value; PropertyHasChanged(); } }
		public virtual string Impuesto { get { return _base.Impuesto; } set { _base.Impuesto = value; } }
		public virtual Decimal PImpuesto { get { return _base._p_impuesto; } set { _base._p_impuesto = value; } }
		public virtual string NumeroClienteLabel { get { return _base.NumeroClienteLabel; } }
		public virtual string Photo { get { return _base.GetCrypFileName(Oid, EFile.Photo); } }
		public virtual string Passport { get { return _base.GetCrypFileName(Oid, EFile.Passport); } }
		public virtual string DrivingLicense { get { return _base.GetCrypFileName(Oid, EFile.DrivingLicense); } }

		public override bool IsValid
		{
            get
            {
                return base.IsValid
                       && _cobros.IsValid
                       && _producto_clientes.IsValid;
            }
		}		
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _cobros.IsDirty
						 || _producto_clientes.IsDirty;
            }
		}
		
        #endregion

        #region Business Methods
			
		public virtual Cliente CloneAsNew()
		{
			Cliente clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.GetNewCode();
			clon.SessionCode = Cliente.OpenSession();
			Cliente.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.Cobros.MarkAsNew();
			clon.Productos.MarkAsNew();
			
			return clon;
		}

        public virtual void SetImpuesto(ImpuestoInfo source)
        {
            if (source == null)
            {
                OidImpuesto = 0;
                Impuesto = Library.Common.EnumText<ETipoImpuesto>.GetLabel(ETipoImpuesto.Defecto);
                PImpuesto = 0;
            }
            else
            {
                OidImpuesto = source.Oid;
                Impuesto = source.Nombre;
                PImpuesto = source.Porcentaje;
            }
        }

        public virtual void GetNewCode()
        {
			Serial = SerialInfo.GetNext(typeof(Cliente), SessionCode);
            Codigo = Serial.ToString(Resources.Defaults.CLIENTE_CODE_FORMAT);
        }

        public virtual void SetNewCode()
        {
			SerialInfo.Check(typeof(Cliente), Oid, Codigo);
			try { Serial = Convert.ToInt64(Codigo); } catch { Serial = SerialInfo.GetNext(typeof(Cliente), SessionCode); }
        }

		public virtual void UpdateCredito()
		{ 
			decimal cobrado = 0;

			foreach (Charge item in Cobros)
				cobrado += item.Importe;

			CreditoDispuesto = TotalFacturado - cobrado;
		}

        public static void Merge(long oid_source, long oid_destiny)
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = Cliente.OpenSession();
            ITransaction trans = Cliente.BeginTransaction(Cliente.Session(sessCode));

            try
            {
                nHManager.Instance.SQLNativeExecute(MERGE(oid_source, oid_destiny));

                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                Cliente.CloseSession(sessCode);
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
				throw new iQValidationException(e.Description, string.Empty, "Codigo");
			}

			//Nombre
			if (Nombre == string.Empty)
			{
				e.Description = Resources.Messages.FIELD_REQUIRED;
				throw new iQValidationException(e.Description, string.Empty, "Nombre");
			}

			//VatNumber
			AgenteBase.ValidateInput((ETipoID)TipoId, "VatNumber", VatNumber);
			return true;
		} 

		#endregion
		 
		#region Autorization Rules
		 
		public static bool CanAddObject()
		{
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanAddObject(Resources.SecureItems.CLIENTE);
		}		
		public static bool CanGetObject()
		{
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanGetObject(Resources.SecureItems.CLIENTE);
		}		
		public static bool CanDeleteObject()
		{
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.CLIENTE);
		}		
		public static bool CanEditObject()
		{
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanEditObject(Resources.SecureItems.CLIENTE);
		}

		public static bool CanEditCuentaContable()
		{
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanEditObject(Resources.SecureItems.CUENTA_CONTABLE);
		}

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				Cliente = Cliente.New().GetInfo(false),
				Estado = EEstado.NoAnulado
			};
			conditions.Cliente.Oid = oid;

			OutputDeliveryList albaranes = OutputDeliveryList.GetList(conditions, false);

			if (albaranes.Count > 0)
				throw new iQException(Resources.Messages.ALBARANES_ASOCIADOS);

			BudgetList proformas = BudgetList.GetList(conditions, false);

			if (proformas.Count > 0)
				throw new iQException(Resources.Messages.PROFORMAS_ASOCIADAS);

			conditions.TipoCobro = ETipoCobro.Cliente;

			ChargeList cobros = ChargeList.GetList(conditions, false);

			if (cobros.Count > 0)
				throw new iQException(Resources.Messages.COBROS_ASOCIADOS);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Cliente () {}		
		private Cliente(Cliente source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
			SessionCode = source.SessionCode;
            Fetch(source);
        }
        private Cliente(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();	
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static Cliente NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Cliente obj = DataPortal.Create<Cliente>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Informe con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Informe source, bool childs)
		/// <remarks/>
		internal static Cliente GetChild(Cliente source, bool childs = false)
		{
			return new Cliente(source, childs);
		}
        internal static Cliente GetChild(int sessionCode, IDataReader source, bool childs = false) { return new Cliente(sessionCode, source, childs); }
		
		public virtual ClienteInfo GetInfo(bool childs = true) { return new ClienteInfo(this, childs); }
		
		#endregion

		#region Root Factory Methods
		
		public static Cliente New(int sessionCode = -1)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Cliente obj = DataPortal.Create<Cliente>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
			return obj;
		}

        public static Cliente NewRegistration()
        {
            return DataPortal.Create<Cliente>(new CriteriaCs(-1));
        }

		public new static Cliente Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return BusinessBaseEx<Cliente>.Get(query, childs, -1);
		}

		public static Cliente Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }

		public static Cliente GetUser(long oid, bool childs = false)
		{
			return Get(Cliente.SELECT_USERS(new QueryConditions() { Cliente = ClienteInfo.New(oid) }, false), childs);
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
		/// Elimina todos los Cliente. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Cliente.OpenSession();
			ISession sess = Cliente.Session(sessCode);
			ITransaction trans = Cliente.BeginTransaction(sessCode);
			
			try
			{
				sess.Delete("from ClientRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				Cliente.CloseSession(sessCode);
			}
		}

        /// <summary>
        /// Se utiliza para guardar usuarios que no han sido logueados
        /// </summary>
        /// <returns></returns>
        public virtual Cliente Register()
        {
            // Por la posible doble interfaz Root/Child
            if (IsChild) throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

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
                _producto_clientes.UpdateHistoria(this);
                base.Save();

                _cobros.Update(this);
                _producto_clientes.Update(this);

                if (!SharedTransaction) Transaction().Commit();
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
				if (!SharedTransaction)
				{
					if (CloseSessions) CloseSession();
					else BeginTransaction();
				}
            }
        }

		public override Cliente Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) 
                throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return Register();
        }

		public virtual void LoadChilds(Type type, bool getChilds)
		{
			if (type.Equals(typeof(Charges)))
			{
				_cobros = Charges.GetChildList(this, getChilds);
			}
            if (type.Equals(typeof(ProductoCliente)))
                _producto_clientes = ProductoClientes.GetChildList(this, getChilds);
            if (type.Equals(typeof(LineaRegistro)))
                _emails = LineaRegistroList.GetRegistroEmailsByCliente(this.Oid, getChilds);
		}

		#endregion
		
		#region Common Data Access
		 
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			_base.Record.Oid = (long)(new Random()).Next();
			EFormaPago = EFormaPago.Contado;
			EMedioPago = EMedioPago.Efectivo;
			EEstado = EEstado.Active;
			Codigo = (0).ToString(Resources.Defaults.CLIENTE_CODE_FORMAT);
			BirthDate = DateTime.Today;

			_cobros = Charges.NewChildList();	
			_producto_clientes = ProductoClientes.NewChildList();
		}

		private void Fetch(IDataReader source)
		{
			_base.CopyValues(source);

			if (Childs)
			{
				string query = string.Empty;
				IDataReader reader;

				Charge.DoLOCK(Session());
				query = Charges.SELECT(this);
				reader = nHManager.Instance.SQLNativeSelect(query, Session());
				_cobros = Charges.GetChildList(SessionCode, reader);

				ProductoCliente.DoLOCK(Session());
				query = ProductoClientes.SELECT(this);
				reader = nHManager.Instance.SQLNativeSelect(query, Session());
				_producto_clientes = ProductoClientes.GetChildList(reader);
			}

			MarkOld();
		}

		private void Fetch(Cliente source)
		{
			_base.CopyValues(source);

			if (Childs)
			{
				string query = string.Empty;
				IDataReader reader;

				Charge.DoLOCK(Session());
				query = Charges.SELECT(this);
				reader = nHManager.Instance.SQLNativeSelect(query, Session());
				_cobros = Charges.GetChildList(SessionCode, reader);

				ProductoCliente.DoLOCK(Session());
				query = ProductoClientes.SELECT(this);
				reader = nHManager.Instance.SQLNativeSelect(query, Session());
				_producto_clientes = ProductoClientes.GetChildList(reader);
			}

			MarkOld();
		}

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Clientes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			GetNewCode();

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);

			MarkOld();
		}

		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(Clientes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			ClientRecord obj = Session().Get<ClientRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			MarkOld();
		}

		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Clientes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<ClientRecord>(Oid));

			MarkNew();
		}

		#endregion
		 
		#region Root Data Access
		 
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
			{
				Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					Cliente.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;
						
						Charge.DoLOCK(Session());
						query = Charges.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_cobros = Charges.GetChildList(SessionCode, reader);
						
						ProductoCliente.DoLOCK(Session());
						query = ProductoClientes.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_producto_clientes = ProductoClientes.GetChildList(reader);
 					}
				}

				MarkOld();
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
			}
		}
		 
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }

                GetNewCode();
				Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;

			ClientRecord obj = Session().Get<ClientRecord>(Oid);
            if (obj.Codigo != this.Codigo) SetNewCode();
            obj.CopyValues(Base.Record);
			Session().Update(obj);
		}
		
		//Deferred deletion
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}

		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				if (!SharedTransaction)
				{
					// Iniciamos la conexion y la transaccion
					SessionCode = OpenSession();
					BeginTransaction();
				}
					
				//Si no hay integridad referencial, aquí se deben borrar las listas hijo
				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
				Session().Delete((ClientRecord)(criterio.UniqueResult()));
				Transaction().Commit();
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

		internal enum EQueryType { GENERAL = 0, USER = 1 }

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>()
            {
                { 
                    "Username", 
                    new ForeignField() { 
                        Property = "Username", 
                        TableAlias = "US", 
                        Column = nHManager.Instance.GetTableColumn(typeof(UserRecord), "Name")
                    } 
                },
                { 
                    "LastLoginDate", 
                    new ForeignField() { 
                        Property = "LastLoginDate", 
                        TableAlias = "US", 
                        Column = nHManager.Instance.GetTableColumn(typeof(UserRecord), "LastLoginDate")
                    }
                },
				{	
                    "CreationDate", 
                    new ForeignField() { 
                        Property = "CreationDate", 
                        TableAlias = "US", 
                        Column = nHManager.Instance.GetTableColumn(typeof(UserRecord), "CreationDate")
                    }
                }
            };
		}

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(EQueryType.GENERAL, conditions, true); }

        internal static string SELECT_FIELDS(EQueryType queryType)
        {
            string query = string.Empty;

			switch (queryType)
			{
				case EQueryType.GENERAL:

					query = @"
						SELECT CL.*
								,0 AS ""OID_USER""
								,'' AS ""USERNAME""
								,0 AS ""USER_STATUS""
								,NULL AS ""CREATION_DATE""
								,NULL AS ""LAST_LOGIN_DATE""
								,COALESCE(CB.""VALOR"",'') AS ""CUENTA_ASOCIADA""
								,COALESCE(IP.""NOMBRE"",'') AS ""IMPUESTO""
								,COALESCE(IP.""PORCENTAJE"",0) AS ""P_IMPUESTO""
								,A.""TOTAL_FACTURADO"" AS ""TOTAL_FACTURADO""
								,(A.""TOTAL_FACTURADO"" - C.""TOTAL_COBRADO"") AS ""CREDITO_DISPUESTO""";
					break;

				case EQueryType.USER:

					query = @"
						SELECT CL.*
								,COALESCE(US.""OID"",0) AS ""OID_USER""
								,COALESCE(US.""NAME"",'') AS ""USERNAME""
								,COALESCE(US.""ESTADO"",0) AS ""USER_STATUS""
								,COALESCE(US.""CREATION_DATE"",NULL) AS ""CREATION_DATE""
								,COALESCE(US.""LAST_LOGIN_DATE"",NULL) AS ""LAST_LOGIN_DATE""
								,COALESCE(CB.""VALOR"",'') AS ""CUENTA_ASOCIADA""
								,COALESCE(IP.""NOMBRE"",'') AS ""IMPUESTO""
								,COALESCE(IP.""PORCENTAJE"",0) AS ""P_IMPUESTO""
								,A.""TOTAL_FACTURADO"" AS ""TOTAL_FACTURADO""
								,(A.""TOTAL_FACTURADO"" - C.""TOTAL_COBRADO"") AS ""CREDITO_DISPUESTO""";
					break;
			}

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = @"
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "CL", ForeignFields());

			if (conditions.OidList != null)
				query += @"
					AND CL.""OID"" IN " + EntityBase.GET_IN_STRING(conditions.OidList);

			query += EntityBase.STATUS_LIST_CONDITION(conditions.Status, "CL").Replace("STATUS", "ESTADO");

			if (conditions.Cliente != null) 
				query += @"
					AND CL.""OID"" = " + conditions.Cliente.Oid;

			if (conditions.User != null) 
				query += @"
					AND US.""OID"" = " + conditions.User.Oid;

			return query + " " + conditions.ExtraWhere;
		}

		internal static string JOIN(QueryConditions conditions)
		{
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
			string de = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryRecord));
			string ch = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
			string ip = nHManager.Instance.GetSQLTable(typeof(TaxRecord));

			string query;

			query = @"
				FROM " + cl + @" AS CL
				LEFT JOIN " + cb + @" AS CB ON CL.""OID_CUENTA_BANCARIA_ASOCIADA"" = CB.""OID""
				LEFT JOIN " + ip + @" AS IP ON IP.""OID"" = CL.""OID_IMPUESTO""
				LEFT JOIN (SELECT ""OID_HOLDER"", SUM(""TOTAL"") AS ""TOTAL_FACTURADO""
							FROM " + de + @"
							WHERE ""HOLDER_TYPE"" = " + (long)ETipoEntidad.Cliente + @"
							GROUP BY ""OID_HOLDER"") 
					AS A ON A.""OID_HOLDER"" = CL.""OID""
				LEFT JOIN (SELECT ""OID_CLIENTE"", SUM(""IMPORTE"") AS ""TOTAL_COBRADO""
			               FROM " + ch + @"
			               WHERE ""ESTADO_COBRO"" = " + (long)EEstado.Charged + @" AND ""ESTADO"" != " + (long)EEstado.Anulado + @"
			               GROUP BY ""OID_CLIENTE"") 
					AS C ON C.""OID_CLIENTE"" = CL.""OID"" AND C.""OID_CLIENTE"" <> 0";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string SELECT(EQueryType queryType, QueryConditions conditions, bool lockTable)
		{
			string query;

			query =
				SELECT_FIELDS(queryType) +
				JOIN(conditions) +
				WHERE(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "CL", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += EntityBase.LOCK("CL", lockTable);

			return query;
		}

		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};
			return SELECT(EQueryType.GENERAL, conditions, lockTable);
		}

		public static string SELECT_COUNT() { return SELECT_COUNT(new QueryConditions()); }
		public static string SELECT_COUNT(QueryConditions conditions)
		{
			string query;

			query = @"
                SELECT COUNT(*) AS ""TOTAL_ROWS""" +
				SELECT(conditions) +
				WHERE(conditions);

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Cliente = ClienteInfo.New(oid) };

			query = SELECT(EQueryType.GENERAL, conditions, lockTable);

			return query;
		}

		public static string SELECT_BY_LIST(List<string> oid_list, bool lockTable)
		{
			string query = string.Empty;
			string list = string.Empty;

			foreach (string item in oid_list)
				list += item + ",";

			list = list.Substring(0, list.Length - 1);

			QueryConditions conditions = new QueryConditions();

			conditions.ExtraWhere += @"
				AND CL.""OID"" IN (" + list + ")";

			query = SELECT(EQueryType.GENERAL, conditions, lockTable);

			return query;
		}

		internal static string SELECT_USERS(QueryConditions conditions, bool lockTable)
		{
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
			string su = nHManager.Instance.GetSQLTable(typeof(SchemaUserRecord));

			conditions.ExtraJoin += @"
				LEFT JOIN (SELECT US.*
							FROM " + us + @" AS US 
							INNER JOIN " + su + @" AS SU ON SU.""OID_USER"" = US.""OID"" AND SU.""OID_SCHEMA"" = " + AppContext.ActiveSchema.Oid + @"
							WHERE US.""ENTITY_TYPE"" = " + (long)ETipoEntidad.Cliente + @"
								AND US.""CREATION_DATE"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"')
					AS US ON US.""ENTITY_TYPE"" = " + (long)ETipoEntidad.Cliente + @" AND CL.""OID"" = US.""OID_ENTITY"""; 
		
			return SELECT(EQueryType.USER, conditions, lockTable);
		}

		internal static string SELECT_LOCKEDOUT(QueryConditions conditions, bool lockTable)
		{
			string query = string.Empty;

			conditions.ExtraWhere += @"
				AND US.""ESTADO"" IN (" + (long)EEstadoItem.LockedOut + ")";

			query = SELECT_USERS(conditions, lockTable);

			return query;
		}
		internal static string SELECT_PENDING_APPROVAL(QueryConditions conditions, bool lockTable)
		{
			string query = string.Empty;

			conditions.ExtraWhere += @"
				AND US.""ESTADO"" IN (" + (long)EEstadoItem.Registered + "," + (long)EEstadoItem.Inactive + ")";

			query = SELECT_USERS(conditions, lockTable);

			return query;
		}

        internal static string MERGE(long oid_source, long oid_destiny)
        {
            string query;

            string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
            string bu = nHManager.Instance.GetSQLTable(typeof(BudgetRecord));
            string ch = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string cp = nHManager.Instance.GetSQLTable(typeof(ClientProductRecord));
            string de = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryRecord));
            string iv = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
            string or = nHManager.Instance.GetSQLTable(typeof(OrderRecord));
            string pd = nHManager.Instance.GetSQLTable(typeof(PrecioDestinoRecord));

            //PROFORMAS
            query = "UPDATE " + bu + @" SET ""OID_CLIENTE"" = " + oid_destiny + @"
                    WHERE " + bu + @".""OID_CLIENTE"" = " + oid_source + ";";

            //COBROS
            query += " UPDATE " + ch + @" SET ""OID_CLIENTE"" = " + oid_destiny + @"
                    WHERE " + ch + @".""OID_CLIENTE"" = " + oid_source + ";";

            //PRODUCTOS DE CLIENTES
            query += " UPDATE " + cp + @" SET ""OID_CLIENTE"" = " + oid_destiny + @"
                    WHERE " + cp + @".""OID_CLIENTE"" = " + oid_source + ";";

            //ALBARANES
            query += " UPDATE " + de + @" SET ""OID_HOLDER"" = " + oid_destiny + @"
                    WHERE ""OID_HOLDER"" = " + oid_source + @" AND ""HOLDER_TYPE"" = " + (long)ETipoEntidad.Cliente + @";";

            //FACTURAS
            query += " UPDATE " + iv + @" SET ""OID_CLIENTE"" = " + oid_destiny + @", ""VAT_NUMBER"" = CL.""VAT_NUMBER"", 
                            ""CLIENTE"" = CL.""NOMBRE"", ""DIRECCION"" = CL.""DIRECCION"", ""CODIGO_POSTAL"" = CL.""CODIGO_POSTAL"", 
                            ""PROVINCIA"" = CL.""PROVINCIA"", ""MUNICIPIO"" = CL.""MUNICIPIO""
                        FROM (  SELECT ""VAT_NUMBER"", ""NOMBRE"", ""DIRECCION"", ""CODIGO_POSTAL"", ""PROVINCIA"", ""MUNICIPIO""
                                FROM " + cl + @"
                                WHERE ""OID"" = " + oid_destiny + @") AS CL
                    WHERE " + iv + @".""OID_CLIENTE"" = " + oid_source + ";";

            //PEDIDOS
            query += " UPDATE " + or + @" SET ""OID_CLIENTE"" = " + oid_destiny + @"
                    WHERE " + or + @".""OID_CLIENTE"" = " + oid_source + ";";

            //PRECIOS DESTINO
            query += " UPDATE " + pd + @" SET ""OID_CLIENTE"" = " + oid_destiny + @"
                    WHERE " + pd + @".""OID_CLIENTE"" = " + oid_source + ";";
            
            return query;
        }

        #endregion
	}
}