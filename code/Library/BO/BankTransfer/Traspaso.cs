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
	public class BankTransferRecord : RecordBase
	{
		#region Attributes

		private long _oid_cuenta_origen;
		private long _oid_cuenta_destino;
		private long _oid_usuario;
		private long _serial;
		private string _codigo = string.Empty;
		private long _estado;
		private DateTime _fecha;
		private string _observaciones = string.Empty;
		private Decimal _importe;
		private long _tipo;
		private Decimal _gastos_bancarios;
		private DateTime _fecha_recepcion;
		#endregion

		#region Properties

		public virtual long OidCuentaOrigen { get { return _oid_cuenta_origen; } set { _oid_cuenta_origen = value; } }
		public virtual long OidCuentaDestino { get { return _oid_cuenta_destino; } set { _oid_cuenta_destino = value; } }
		public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual Decimal Importe { get { return _importe; } set { _importe = value; } }
		public virtual long TipoMovimiento { get { return _tipo; } set { _tipo = value; } }
		public virtual Decimal GastosBancarios { get { return _gastos_bancarios; } set { _gastos_bancarios = value; } }
		public virtual DateTime FechaRecepcion { get { return _fecha_recepcion; } set { _fecha_recepcion = value; } }

		#endregion

		#region Business Methods

		public BankTransferRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_cuenta_origen = Format.DataReader.GetInt64(source, "OID_CUENTA_ORIGEN");
			_oid_cuenta_destino = Format.DataReader.GetInt64(source, "OID_CUENTA_DESTINO");
			_oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_importe = Format.DataReader.GetDecimal(source, "IMPORTE");
			_tipo = Format.DataReader.GetInt64(source, "TIPO");
			_gastos_bancarios = Format.DataReader.GetDecimal(source, "GASTOS_BANCARIOS");
			_fecha_recepcion = Format.DataReader.GetDateTime(source, "FECHA_RECEPCION");

		}
		public virtual void CopyValues(BankTransferRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_cuenta_origen = source.OidCuentaOrigen;
			_oid_cuenta_destino = source.OidCuentaDestino;
			_oid_usuario = source.OidUsuario;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_estado = source.Estado;
			_fecha = source.Fecha;
			_observaciones = source.Observaciones;
			_importe = source.Importe;
			_tipo = source.TipoMovimiento;
			_gastos_bancarios = source.GastosBancarios;
			_fecha_recepcion = source.FechaRecepcion;
		}

		#endregion
	}

	[Serializable()]
	public class BankTransferBase
	{
		#region Attributes

		private BankTransferRecord _record = new BankTransferRecord();
		
		private string _cuenta_origen = string.Empty;
		private string _cuenta_destino = string.Empty;
		private string _usuario = string.Empty;

		#endregion

		#region Properties

		public BankTransferRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
        public virtual EBankLineType ETipoMovimientoBanco { get { return (EBankLineType)_record.TipoMovimiento; } set { _record.TipoMovimiento = (long)value; } }
		public virtual string CuentaOrigen { get { return _cuenta_origen; } set { _cuenta_origen = value; } }
		public virtual string CuentaDestino { get { return _cuenta_destino; } set { _cuenta_destino = value; } }
		public virtual string Usuario { get { return _usuario; } set { _usuario = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_cuenta_origen = Format.DataReader.GetString(source, "CUENTA_ORIGEN");
			_cuenta_destino = Format.DataReader.GetString(source, "CUENTA_DESTINO");
			_usuario = Format.DataReader.GetString(source, "USUARIO");
		}
		internal void CopyValues(Traspaso source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_cuenta_origen = source.CuentaOrigen;
			_cuenta_destino = source.CuentaDestino;
			_usuario = source.Usuario;	
		}
		internal void CopyValues(TraspasoInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_cuenta_origen = source.CuentaOrigen;
			_cuenta_destino = source.CuentaDestino;
			_usuario = source.Usuario;	
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class Traspaso : BusinessBaseEx<Traspaso>, IBankLine, IEntidadRegistro
	{
		#region IMovimientoBanco

		public virtual ETipoTitular ETipoTitular { get { return ETipoTitular.Todos; } }
		public virtual string CodigoTitular { get { return Codigo; } set { } }
		public virtual string Titular { get { return CuentaOrigen; } set { } }
        public virtual long OidCuenta { get { return _base.Record.OidCuentaOrigen; } set { } }
		public virtual string CuentaBancaria { get { return _base.CuentaOrigen; } set { } }
		public virtual EMedioPago EMedioPago { get { return EMedioPago.Transferencia; } }
		public virtual DateTime Vencimiento { get { return _base.Record.Fecha; } set { } }
		public virtual bool Confirmado { get { return true; } } 

		public virtual IBankLineInfo IGetInfo(bool childs) { return (IBankLineInfo)GetInfo(childs); }

        #endregion

        #region IEntidadRegistro

        public virtual ETipoEntidad ETipoEntidad { get { return ETipoEntidad.Traspaso; } }
        public string DescripcionRegistro { get { return "TRASPASO Nº " + Codigo + " de " + Fecha.ToShortDateString() + " de " + Importe.ToString("C2") + " de " + CuentaBancaria; } }

        public virtual IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }
        public virtual IEntidadRegistro IGet(long oid, bool childs) { return (IEntidadRegistro)Get(oid, childs); }

        public void Update(Registro parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            ValidationRules.CheckRules();

            SessionCode = parent.SessionCode;
			BankTransferRecord obj = Session().Get<BankTransferRecord>(Oid);
            obj.CopyValues(Base.Record);
            Session().Update(obj);

            MarkOld();
        }

        #endregion

		#region Attributes

		protected BankTransferBase _base = new BankTransferBase();

		#endregion
		
		#region Properties

		public BankTransferBase Base { get { return _base; } }

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
		public virtual long OidCuentaOrigen
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCuentaOrigen;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCuentaOrigen.Equals(value))
				{
					_base.Record.OidCuentaOrigen = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidCuentaDestino
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCuentaDestino;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCuentaDestino.Equals(value))
				{
					_base.Record.OidCuentaDestino = value;
					PropertyHasChanged();
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
		public virtual Decimal Importe
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Importe;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Importe.Equals(value))
				{
					_base.Record.Importe = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long TipoMovimiento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoMovimiento;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.TipoMovimiento.Equals(value))
				{
					_base.Record.TipoMovimiento = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal GastosBancarios
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.GastosBancarios;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.GastosBancarios.Equals(value))
				{
					_base.Record.GastosBancarios = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FechaRecepcion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaRecepcion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaRecepcion.Equals(value))
				{
					_base.Record.FechaRecepcion = value;
					PropertyHasChanged();
				}
			}
		}

        public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
        public virtual EBankLineType ETipoMovimientoBanco { get { return _base.ETipoMovimientoBanco; } set { TipoMovimiento = (long)value; } }
		public virtual string CuentaOrigen { get { return _base.CuentaOrigen; } set { _base.CuentaOrigen = value; } }
		public virtual string CuentaDestino { get { return _base.CuentaDestino; } set { _base.CuentaDestino = value; } }
		public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }

		#endregion
		
		#region Business Methods
		
		public virtual Traspaso CloneAsNew()
		{
			Traspaso clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.GetNewCode();
			
			clon.SessionCode = Traspaso.OpenSession();
			Traspaso.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}

		protected virtual void CopyFrom(TraspasoInfo source)
		{
			if (source == null) return;
			
			_base.Record.Oid = source.Oid;
			_base.Record.OidCuentaOrigen = source.OidCuentaOrigen;
			_base.Record.OidCuentaDestino = source.OidCuentaDestino;
			_base.Record.OidUsuario = source.OidUsuario;
			_base.Record.Serial = source.Serial;
			_base.Record.Codigo = source.Codigo;
			_base.Record.Estado = source.Estado;
			_base.Record.Fecha = source.Fecha;
			_base.Record.Observaciones = source.Observaciones;
			_base.Record.Importe = source.Importe;
            _base.Record.TipoMovimiento = source.TipoMovimiento;
            _base.Record.GastosBancarios = source.GastosBancarios;
            _base.Record.FechaRecepcion = source.FechaRecepcion;
		}		
		
        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(Traspaso));
            Codigo = Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
        }

		public virtual void ChangeEstado(EEstado estado)
		{
			EntityBase.CheckChangeState(EEstado, estado);
			EEstado = estado;
		}
		public static Traspaso ChangeEstado(long oid, EEstado estado)
		{
			Traspaso item = null;

			try
			{
				item = Traspaso.Get(oid, false);
                TraspasoInfo oldItem = item.GetInfo(false);

				EntityBase.CheckChangeState(item.EEstado, estado);

				item.BeginEdit();
				item.EEstado = estado;

				if (estado == EEstado.Anulado)
				{
					BankLine.EditItem(item, oldItem, item.SessionCode);
				}

				item.ApplyEdit();
				item.Save();
			}
			finally
			{
				if (item != null) item.CloseSession();
			}

			return item;
		}

		#endregion
		 
	    #region Validation Rules

		/// <summary>
		/// Añade las reglas de validación necesarias para el objeto
		/// </summary>
		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CheckValidation, "Oid");
		}

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{	
			//Fecha
			if (Fecha == DateTime.MinValue)
			{
				e.Description = string.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "Fecha");
				throw new iQValidationException(e.Description, string.Empty);
            }

            //FechaRecepcion
            if (FechaRecepcion == DateTime.MinValue)
            {
                e.Description = string.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "FechaRecepcion");
                throw new iQValidationException(e.Description, string.Empty);
            }

			//Importe
			if (Importe <= 0)
			{
				e.Description = string.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "Importe");
				throw new iQValidationException(e.Description, string.Empty);
			}

			return true;
		}	
		 
		#endregion
		 
		#region Autorization Rules
				
		public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.MOVIMIENTO_BANCO);
        }

        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.MOVIMIENTO_BANCO);
        }

        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.MOVIMIENTO_BANCO);
        }

        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.MOVIMIENTO_BANCO);
        }

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Traspaso () {}		
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private Traspaso(Traspaso source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Traspaso(int sessionCode, IDataReader source, bool childs)
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
		public static Traspaso NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Traspaso obj = DataPortal.Create<Traspaso>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Traspaso con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Traspaso source, bool childs)
		/// <remarks/>
		internal static Traspaso GetChild(Traspaso source) { return new Traspaso(source, false); }
		internal static Traspaso GetChild(Traspaso source, bool childs) { return new Traspaso(source, childs); }
        internal static Traspaso GetChild(int session_code, IDataReader source) { return new Traspaso(session_code, source, false); }
        internal static Traspaso GetChild(int session_code, IDataReader source, bool childs) { return new Traspaso(session_code, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual TraspasoInfo GetInfo() { return GetInfo(true); }	
		public virtual TraspasoInfo GetInfo (bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new TraspasoInfo(this, childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static Traspaso New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Traspaso>(new CriteriaCs(-1));
		}
		
		public static Traspaso Get(long oid) { return Get(oid, true); }
		public static Traspaso Get(long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Traspaso.GetCriteria(Traspaso.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Traspaso.SELECT(oid);
				
			Traspaso.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Traspaso>(criteria);
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
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Traspaso. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Traspaso.OpenSession();
			ISession sess = Traspaso.Session(sessCode);
			ITransaction trans = Traspaso.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from BankTransferRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				Traspaso.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Traspaso Save()
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
			EEstado = EEstado.Abierto;
			Fecha = DateTime.Now;
            FechaRecepcion = DateTime.Now;
			OidUsuario = AppContext.User.Oid;
			Usuario = AppContext.User.Name;
            ETipoMovimientoBanco = EBankLineType.Traspaso;
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Traspaso source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);			 

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
        private void Fetch(IDataReader source)
        {
			_base.CopyValues(source);			   

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Traspasos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			GetNewCode();
		
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);

			//Insertamos el movimiento de banco asociado
			BankLine.InsertItem(this, SessionCode);

			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(Traspasos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidUsuario = AppContext.User.Oid;
			Usuario = AppContext.User.Name;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			BankTransferRecord obj = Session().Get<BankTransferRecord>(Oid);

			Traspaso old = Traspaso.New();
			old.Base.Record.CopyValues(obj);
            TraspasoInfo oldItem = old.GetInfo(false);
			
			obj.CopyValues(Base.Record);
			Session().Update(obj);

            //Si no se ha modificado el estado del traspaso es porque se ha modificado algún otro campo 
            // y hay que modificar el movimiento asociado
            //En caso de haber modificado el estado, si el nuevo no es Exportado es porque se ha modificado 
            //algún otro campo y hay que modificar el movimiento asociado
            if (EEstado == oldItem.EEstado || (EEstado != oldItem.EEstado && EEstado != EEstado.Exportado))
            {
                //Editamos el Movimiento de Banco asociado si lo hubiera
                BankLine.EditItem(this, oldItem, SessionCode);
            }

			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Traspasos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<BankTransferRecord>(Oid));

			//Anulamos el Movimiento de Banco asociado
			BankLine.AnulaItem(this, SessionCode);

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
					Traspaso.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
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
            if (!SharedTransaction)
            {
                if (SessionCode < 0) SessionCode = OpenSession();
                BeginTransaction();
            }
			
			GetNewCode();

			Session().Save(Base.Record);

			//Insertamos el movimiento de banco asociado
			BankLine.InsertItem(this, SessionCode);
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;

			BankTransferRecord obj = Session().Get<BankTransferRecord>(Oid);

			Traspaso old = Traspaso.New();
			old.Base.Record.CopyValues(obj);
			TraspasoInfo oldItem = old.GetInfo(false);

			obj.CopyValues(Base.Record);
			Session().Update(obj);

			//Editamos el Movimiento de Banco asociado si lo hubiera
			BankLine.EditItem(this, oldItem, SessionCode);

			MarkOld();			
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
				Traspaso obj = Traspaso.Get(Oid);

				//Nunca se borran, se anulan
				obj.EEstado = EEstado.Anulado;

				//Anulamos el Movimiento de Banco asociado
                BankLine.AnulaItem(obj, SessionCode);

                obj.Save();
                obj.CloseSession();
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

		internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>()
            {
               /* { 
                    "Client", 
                    new ForeignField() {                        
						Property = "Client", 
                        TableAlias = "CL", 
                        Column = nHManager.Instance.GetTableColumn(typeof(ClientRecord), "Nombre")
                    }
                },*/
            };
        }

        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT TR.*" +
					"		,CB1.\"VALOR\" AS \"CUENTA_ORIGEN\"" +
					"		,CB2.\"VALOR\" AS \"CUENTA_DESTINO\"" +
					"		,US.\"NAME\" AS \"USUARIO\"";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE (TR.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			query += EntityBase.NO_NULL_RECORDS_CONDITION("TR");

            if (conditions.Traspaso != null)
		       if (conditions.Traspaso.Oid != 0)
                   query += " AND TR.\"OID\" = " + conditions.Traspaso.Oid;

            query += EntityBase.ESTADO_CONDITION(conditions.Estado, "TR");

			return query;
		}

	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string tr = nHManager.Instance.GetSQLTable(typeof(BankTransferRecord));
			string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
            
			string query;

			query = SELECT_FIELDS() +
					" FROM " + tr + " AS TR" +
					" LEFT JOIN " + cb + " AS CB1 ON CB1.\"OID\" = TR.\"OID_CUENTA_ORIGEN\"" +
					" LEFT JOIN " + cb + " AS CB2 ON CB2.\"OID\" = TR.\"OID_CUENTA_DESTINO\"" +
					" LEFT JOIN " + us + " AS US ON US.\"OID\" = TR.\"OID_USUARIO\"";
					
			query += WHERE(conditions);

			if (conditions.Orders == null)
			{
				conditions.Orders = new OrderList();
				conditions.Orders.NewOrder("Fecha", ListSortDirection.Ascending, typeof(Traspaso));
			}
			
			query += ORDER(conditions.Orders, "TR", ForeignFields());
			query += LIMIT(conditions.PagingInfo);
			query += EntityBase.LOCK("TR", lockTable);

            return query;
        }

		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Traspaso = TraspasoInfo.New(oid) };

			query = SELECT(conditions, lockTable);

			return query;
		}

		#endregion
	}
}
