using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using NHibernate;
using Csla;
using Csla.Validation;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class BankLineRecord : RecordBase
	{
		#region Attributes

		private long _oid_operacion;
		private long _tipo_operacion;
		private long _serial;
		private string _codigo = string.Empty;
		private long _oid_usuario;
		private bool _auditado = false;
		private string _observaciones = string.Empty;
		private long _estado;
		private DateTime _fecha_operacion;
		private string _id_operacion = string.Empty;
		private Decimal _importe;
		private long _tipo_cuenta;
		private long _oid_cuenta_mov;
		private DateTime _fecha_creacion;
		private long _tipo_movimiento;

		#endregion

		#region Properties

		public virtual long OidOperacion { get { return _oid_operacion; } set { _oid_operacion = value; } }
		public virtual long TipoOperacion { get { return _tipo_operacion; } set { _tipo_operacion = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }
		public virtual bool Auditado { get { return _auditado; } set { _auditado = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual DateTime FechaOperacion { get { return _fecha_operacion; } set { _fecha_operacion = value; } }
		public virtual string IDOperacion { get { return _id_operacion; } set { _id_operacion = value; } }
		public virtual Decimal Importe { get { return _importe; } set { _importe = value; } }
		public virtual long TipoCuenta { get { return _tipo_cuenta; } set { _tipo_cuenta = value; } }
		public virtual long OidCuentaMov { get { return _oid_cuenta_mov; } set { _oid_cuenta_mov = value; } }
		public virtual DateTime FechaCreacion { get { return _fecha_creacion; } set { _fecha_creacion = value; } }
		public virtual long TipoMovimiento { get { return _tipo_movimiento; } set { _tipo_movimiento = value; } }

		#endregion

		#region Business Methods

		public BankLineRecord() {}

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_operacion = Format.DataReader.GetInt64(source, "OID_OPERACION");
			_tipo_operacion = Format.DataReader.GetInt64(source, "TIPO_OPERACION");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
			_auditado = Format.DataReader.GetBool(source, "AUDITADO");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_fecha_operacion = Format.DataReader.GetDateTime(source, "FECHA_OPERACION");
			_id_operacion = Format.DataReader.GetString(source, "ID_OPERACION");
			_importe = Format.DataReader.GetDecimal(source, "IMPORTE");
			_tipo_cuenta = Format.DataReader.GetInt64(source, "TIPO_CUENTA");
			_oid_cuenta_mov = Format.DataReader.GetInt64(source, "OID_CUENTA");
			_fecha_creacion = Format.DataReader.GetDateTime(source, "FECHA_CREACION");
			_tipo_movimiento = Format.DataReader.GetInt64(source, "TIPO_MOVIMIENTO");

		}
		public virtual void CopyValues(BankLineRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_operacion = source.OidOperacion;
			_tipo_operacion = source.TipoOperacion;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_oid_usuario = source.OidUsuario;
			_auditado = source.Auditado;
			_observaciones = source.Observaciones;
			_estado = source.Estado;
			_fecha_operacion = source.FechaOperacion;
			_id_operacion = source.IDOperacion;
			_importe = source.Importe;
			_tipo_cuenta = source.TipoCuenta;
			_oid_cuenta_mov = source.OidCuentaMov;
			_fecha_creacion = source.FechaCreacion;
			_tipo_movimiento = source.TipoMovimiento;
		}
		
		#endregion
	}

	[Serializable()]
	public class BankLineBase
	{
		#region Attributes

		private BankLineRecord _record = new BankLineRecord();

		public string _auditor = string.Empty;
		public long _oid_cuenta_ex;
		public string _cuenta = string.Empty;
		public string _entidad = string.Empty;
		public string _titular;
		public long _tipo_titular;
		public long _medio_pago;
		public string _id_mov_contable = string.Empty;
		public Decimal _saldo;
		public long _oid_cuenta_asociada;
		public string _cuenta_asociada = string.Empty;
        protected long _oid_titular;

		#endregion

		#region Properties
		
		public BankLineRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
        public long OidTitular { get { return _oid_titular; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_auditor = Format.DataReader.GetString(source, "AUDITOR");
			_medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
			_titular = Format.DataReader.GetString(source, "TITULAR");
			_tipo_titular = Format.DataReader.GetInt64(source, "TIPO_TITULAR");
			_oid_cuenta_ex = Format.DataReader.GetInt64(source, "OID_CUENTA_EX");
			_cuenta = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
			_entidad = Format.DataReader.GetString(source, "ENTIDAD");
			_id_mov_contable = Format.DataReader.GetString(source, "ID_MOV_CONTABLE");
            _oid_titular = Format.DataReader.GetInt64(source, "OID_TITULAR");
		}
		internal void CopyValues(BankLine source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_auditor = source.Auditor;
			_oid_cuenta_ex = source.OidCuenta;
			_cuenta = source.Cuenta;
			_entidad = source.Entidad;
			_titular = source.Titular;
			_tipo_titular = source.TipoTitular;
			_medio_pago = source.MedioPago;
			_id_mov_contable = source.IDMovimientoContable;
            _oid_titular = source.OidTitular;
		}
		internal void CopyValues(BankLineInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_auditor = source.Auditor;
			_oid_cuenta_ex = source.OidCuenta;
			_cuenta = source.Cuenta;
			_entidad = source.Entidad;
			_titular = source.Titular;
			_tipo_titular = source.TipoTitular;
			_medio_pago = source.MedioPago;
			_id_mov_contable = source.IDMovimientoContable;
            _saldo = source.Saldo;
            _oid_titular = source.OidTitular;
		}

		#endregion
	}

    /// <summary>
    /// Editable Root Business Object
    /// Editable Child Business Object
    /// </summary>	
    [Serializable()]
    public class BankLine : BusinessBaseEx<BankLine>, IEntityBase
    {
        #region IEntityBase

        public virtual DateTime FechaReferencia { get { return _base.Record.FechaOperacion; } }

        public virtual IEntityBase ICloneAsNew() { return CloneAsNew(); }
        public virtual void ICopyValues(IEntityBase source) { _base.CopyValues((BankLine)source); }
        public void DifferentYearChecks()
        {
            if (ETipo != ETipoApunteBancario.Sencillo)
                throw new iQException(Resources.Messages.ERROR_APUNTE_MULTIPLE_EDIT);
        }
        public virtual void DifferentYearTasks(IEntityBase oldItem) { }
        public void SameYearTasks(IEntityBase newItem) { }
        public virtual void IEntityBaseSave(object parent) { Save(); }

        #endregion

        #region Attributes

		public BankLineBase _base = new BankLineBase();

        #endregion

        #region Properties

		public BankLineBase Base { get { return _base; } }

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
		public virtual long OidOperacion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidOperacion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidOperacion.Equals(value))
				{
					_base.Record.OidOperacion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long TipoOperacion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				return _base.Record.TipoOperacion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				if (!_base.Record.TipoOperacion.Equals(value))
				{
					_base.Record.TipoOperacion = value;
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
		public virtual long OidAuditor
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
		public virtual bool Auditado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Auditado;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Auditado.Equals(value))
				{
					_base.Record.Auditado = value;
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
		public virtual DateTime FechaOperacion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaOperacion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaOperacion.Equals(value))
				{
					_base.Record.FechaOperacion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string IDOperacion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.IDOperacion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.IDOperacion.Equals(value))
				{
					_base.Record.IDOperacion = value;
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
		public virtual long TipoCuenta
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoCuenta;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.TipoCuenta.Equals(value))
				{
					_base.Record.TipoCuenta = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidCuentaMov
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCuentaMov;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCuentaMov.Equals(value))
				{
					_base.Record.OidCuentaMov = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FechaCreacion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaCreacion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaCreacion.Equals(value))
				{
					_base.Record.FechaCreacion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Tipo
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

		public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; } } //DEPRECATED
		public virtual string EstadoLabel { get { return _base.StatusLabel; } } //DEPRECATED
		public virtual EEstado EStatus { get { return _base.EStatus; } set { Estado = (long)value; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
        public virtual string Auditor
        {
            get
            {
                return _base._auditor;
            }

            set
            {
                if (value == null) value = string.Empty;

                if (!_base._auditor.Equals(value))
                {
                    _base._auditor = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual long OidCuenta { get { return _base._oid_cuenta_ex; } set { _base._oid_cuenta_ex = value; } }
		public virtual string Cuenta { get { return _base._cuenta; } set { _base._cuenta = value; PropertyHasChanged(); } }
		public virtual string Entidad { get { return _base._entidad; } set { _base._entidad = value; PropertyHasChanged(); } }
		public virtual string Titular { get { return _base._titular; } }
		public virtual long TipoTitular { get { return _base._tipo_titular; } set { _base._tipo_titular = value; } }
		public virtual ETipoTitular ETipoTitular { get { return (ETipoTitular)_base._tipo_titular; } }
        public virtual string ETipoTitularLabel { get { return Library.Invoice.EnumText<ETipoTitular>.GetLabel(ETipoTitular); } }
		public virtual long MedioPago { get { return _base._medio_pago; } set { _base._medio_pago = value; } }
		public virtual EMedioPago EMedioPago { get { return (EMedioPago)_base._medio_pago; } }
        public virtual string EMedioPagoLabel { get { return Common.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
		public virtual EBankLineType ETipoMovimientoBanco { get { return (EBankLineType)TipoOperacion; } set { TipoOperacion = (long)value; } }
        public virtual string ETipoMovimientoBancoLabel { get { return Library.Invoice.EnumText<EBankLineType>.GetLabel(ETipoMovimientoBanco); } }
        public virtual string EEstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
		public virtual string IDMovimientoContable { get { return _base._id_mov_contable; } set { _base._id_mov_contable = value; } }
		public virtual ECuentaBancaria ETipoCuenta { get { return (ECuentaBancaria)TipoCuenta; } set { TipoCuenta = (long)value; } }
		public virtual ETipoApunteBancario ETipo { get { return (ETipoApunteBancario)Tipo; } set { Tipo = (long)value; } }
        public virtual string ETipoLabel { get { return EnumText<ETipoApunteBancario>.GetLabel(ETipo); } }
        public virtual long OidTitular { get { return _base.OidTitular; } }

        #endregion

        #region Business Methods

        public virtual BankLine CloneAsNew()
        {
            BankLine clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.SessionCode = BankLine.OpenSession();
            BankLine.BeginTransaction(clon.SessionCode);

            clon.MarkNew();

            return clon;
        }

        protected virtual void CopyFrom(BankLineInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
            OidOperacion = source.OidOperacion;
			TipoOperacion = source.TipoOperacion;
            Serial = source.Serial;
            Codigo = source.Codigo;
            Auditor = source.Auditor;
            Auditado = source.Auditado;
            Observaciones = source.Observaciones;
            Importe = source.Importe;
            FechaCreacion = source.FechaCreacion;
            Tipo = source.Tipo;

			IDOperacion = source.IDOperacion;
            FechaOperacion = source.FechaOperacion;
            IDMovimientoContable = source.IDMovimientoContable;
        }
        public virtual void CopyFrom(IBankLineInfo source)
        {
            if (source == null) return;

            OidOperacion = source.Oid;
            TipoOperacion = source.TipoMovimiento;
            EEstado = EEstado.Abierto;
			FechaOperacion = new DateTime(source.Fecha.Year, source.Fecha.Month, source.Fecha.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            IDOperacion = source.Codigo;
            Observaciones = source.Observaciones;
            ETipoCuenta = ECuentaBancaria.Principal;
            OidCuentaMov = source.OidCuenta;

            switch (source.ETipoMovimientoBanco)
            {
                case EBankLineType.PagoFactura:
                case EBankLineType.PagoNomina:
                case EBankLineType.PagoGasto:
                case EBankLineType.PagoPrestamo:
                case EBankLineType.ExtractoTarjeta:
                    {
                        PaymentInfo pago = (PaymentInfo)source;
                        Importe = -(pago.Importe + pago.GastosBancarios);
                    }
                    break;
                case EBankLineType.Traspaso:
                case EBankLineType.CancelacionComercioExterior:
                    {
                        TraspasoInfo tr = (TraspasoInfo)source;
                        Importe = -(tr.Importe + tr.GastosBancarios);
                    }
                    break;
                case EBankLineType.EntradaCaja:
                case EBankLineType.Prestamo:
                    Importe = -source.Importe;
                    break;

				case EBankLineType.CobroEfecto:
					FechaOperacion = ((FinancialCashInfo)source).ChargeDate;
					Importe = source.Importe;
					break;

                default:
                    Importe = source.Importe;
                    break;
            }

            switch (source.EMedioPago)
            {
                case EMedioPago.ComercioExterior:
                    FechaOperacion = FechaOperacion = new DateTime(source.Fecha.Year, source.Fecha.Month, source.Fecha.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    break;
            }
        }
        public virtual void CopyFrom(BankAccountInfo source)
        {
            if (source == null) return;

            OidOperacion = source.Oid;
            ETipoMovimientoBanco = EBankLineType.ComisionEstudioApertura;
            EEstado = EEstado.Abierto;
            FechaOperacion = new DateTime(source.FechaFirma.Year, source.FechaFirma.Month, source.FechaFirma.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            FechaCreacion = DateTime.Now;
            Observaciones = source.Observaciones;
            ETipoCuenta = ECuentaBancaria.Principal;
            OidCuentaMov = source.Oid;
            Importe = -source.Comision;
        }
		
        public virtual void GetNewCode(BankLines parent)
        {
            GetNewCode();
            foreach (BankLine item in parent)
            {
                if (item.Oid != Oid && item.OidOperacion == OidOperacion 
                    && item.TipoOperacion == TipoOperacion && item.Serial < Serial)
                {
                    Serial = item.Serial;
                    Codigo = item.Codigo;
                }
            }
        }
        public virtual void GetNewCode()
        {
            Serial = BankLineSerialInfo.GetNext(FechaOperacion.Year);
            Codigo = Serial.ToString(Resources.Defaults.MOVIMIENTO_BANCO_CODE_FORMAT);
        }

		public virtual void ChangeEstado(EEstado estado)
		{
			EntityBase.CheckChangeState(EEstado, estado);
			EEstado = estado;
		}
		public static BankLine ChangeEstado(BankLineInfo entity, EEstado estado)
		{
			BankLine item = null;

			try
			{
				if (!CanChangeState())
					throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

				EntityBase.CheckChangeState(entity.EEstado, estado);

				if (entity.ETipoMovimientoBanco != EBankLineType.Manual && entity.ETipoMovimientoBanco != EBankLineType.Interes)
					throw new iQException(Library.Resources.Messages.ACTION_NOT_ALLOWED);

				if ((entity.EEstado == EEstado.Contabilizado) && (!AutorizationRulesControler.CanEditObject(Resources.SecureItems.CUENTA_CONTABLE)))
					throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
				
				item = Get(entity.Oid, entity.ETipoMovimientoBanco, entity.ETipoTitular);

				item.BeginEdit();
				item.EEstado = estado;

				item.ApplyEdit();
				item.Save();
			}
			finally
			{
				if (item != null) item.CloseSession();
			}

			return item;
		}

		public static BankLine AuditarAction(BankLineInfo entity, bool auditado)
		{
			BankLine item = null;

			try
			{
				if (!CanEditObject())
					throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
                if(!AutorizationRulesControler.CanEditObject(Resources.SecureItems.AUDITAR_MOVIMIENTOS_BANCARIOS))
					throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

				item = Get(entity.Oid, entity.ETipoMovimientoBanco, entity.ETipoTitular);

				item.BeginEdit();
				item.Auditado = auditado;
				item.OidAuditor = AppContext.User.Oid;
				item.Auditor = AppContext.User.Name;

				item.ApplyEdit();
				item.Save();

				entity.CopyFrom(item);
			}
			finally
			{
				if (item != null) item.CloseSession();
			}

			return item;
		}

        public virtual void AuditarAction(bool auditado)
        {
            if (!CanEditObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
            if (!AutorizationRulesControler.CanEditObject(Resources.SecureItems.AUDITAR_MOVIMIENTOS_BANCARIOS))
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            Auditado = auditado;
            OidAuditor = AppContext.User.Oid;
            Auditor = AppContext.User.Name;
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
		public static bool CanChangeState()
		{
			return AutorizationRulesControler.CanGetObject(Library.Common.Resources.SecureItems.ESTADO);
		}

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
        /// pero debe ser protected por exigencia de NHibernate.
        /// </summary>
        protected BankLine() { }

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
        /// </summary>
        private BankLine(BankLine source, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            Fetch(source);
        }

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
        /// </summary>
        private BankLine(IDataReader source, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            Fetch(source);
        }

        public static BankLine NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return DataPortal.Create<BankLine>(new CriteriaCs(-1));
        }

        /// <summary>
        /// Crea un objeto
        /// </summary>
        /// <param name="source">MovimientoBanco con los datos para el objeto</param>
        /// <returns>Objeto creado</returns>
        /// <remarks>
        /// La utiliza la BusinessListBaseEx correspondiente para montar la lista
        /// NO OBTIENE los hijos. Para ello utilice GetChild(MovimientoBanco source, bool childs)
        /// <remarks/>
        internal static BankLine GetChild(BankLine source)
        {
            return new BankLine(source, false);
        }

        /// <summary>
        /// Crea un objeto
        /// </summary>
        /// <param name="source">MovimientoBanco con los datos para el objeto</param>
        /// <param name="childs">Flag para obtener también los hijos</param>
        /// <returns>Objeto creado</returns>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
        internal static BankLine GetChild(BankLine source, bool childs)
        {
            return new BankLine(source, childs);
        }

        /// <summary>
        /// Crea un objeto
        /// </summary>
        /// <param name="reader">DataReader con los datos para el objeto</param>
        /// <returns>Objeto creado</returns>
        /// <remarks>
        /// La utiliza la BusinessListBaseEx correspondiente para montar la lista
        /// NO OBTIENE los hijos. Para ello utilice GetChild(IDataReader source, bool childs)
        /// <remarks/>
        internal static BankLine GetChild(IDataReader source)
        {
            return new BankLine(source, false);
        }

        /// <summary>
        /// Crea un objeto
        /// </summary>
        /// <param name="source">IDataReader con los datos para el objeto</param>
        /// <param name="childs">Flag para obtener también los hijos</param>
        /// <returns>Objeto creado</returns>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
        internal static BankLine GetChild(IDataReader source, bool childs)
        {
            return new BankLine(source, childs);
        }

        public virtual BankLineInfo GetInfo()
        {
            return GetInfo(true);
        }
        public virtual BankLineInfo GetInfo(bool get_childs)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(
                    Library.Resources.Messages.USER_NOT_ALLOWED);

            return new BankLineInfo(this, get_childs);
        }

        #endregion

        #region Root Factory Methods

        public static BankLine New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            BankLine obj = DataPortal.Create<BankLine>(new CriteriaCs(-1));
            //Obtenemos un nuevo código por si cambia el año de la fecha
            obj.GetNewCode();
            obj.FechaCreacion = DateTime.Now;

            return obj;
        }

        public static BankLine New(IBankLine source) { return New(source.IGetInfo(false)); }
        public static BankLine New(IBankLineInfo source)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            BankLine obj = DataPortal.Create<BankLine>(new CriteriaCs(-1));
            obj.CopyFrom(source);
            //Obtenemos un nuevo código por si cambia el año de la fecha
            obj.GetNewCode();
            obj.FechaCreacion = DateTime.Now;

            return obj;
        }

		public static BankLine Get(string query, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return Get(query, childs, -1);
		}

        public static BankLine Get(long oid, EBankLineType bankLineType, ETipoTitular tipo_titular)
        {
            return Get(oid, bankLineType, tipo_titular, true);
        }
        public static BankLine Get(long oid, EBankLineType bankLineType, ETipoTitular tipo_titular, bool childs)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = BankLine.GetCriteria(BankLine.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = BankLine.SELECT(oid, bankLineType, tipo_titular);

            BankLine.BeginTransaction(criteria.Session);

            return DataPortal.Fetch<BankLine>(criteria);
        }
        public static BankLine Get(IBankLine item, bool childs)
        {
            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = item.IGetInfo(false) };
            return Get(conditions, childs);
        }

        public static BankLine GetByTarjeta(CreditCardInfo tarjeta, DateTime vencimiento, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                TarjetaCredito = tarjeta,
                TipoMovimientoBanco = EBankLineType.ExtractoTarjeta,
                FechaIni = vencimiento,
                FechaFin = vencimiento,
                Estado = EEstado.NoAnulado
            };

            return Get(conditions, childs);
        }
        public static BankLine GetByTarjeta(CreditCardInfo tarjeta, DateTime vencimiento, bool childs, int sessionCode)
        {
            QueryConditions conditions = new QueryConditions
            {
                TarjetaCredito = tarjeta,
                TipoMovimientoBanco = EBankLineType.ExtractoTarjeta,
                FechaIni = vencimiento,
                FechaFin = vencimiento,
                Estado = EEstado.NoAnulado
            };

            return Get(conditions, childs, sessionCode);
        }
        public static BankLine GetByTarjeta(CreditCardInfo tarjeta, PaymentInfo source, bool childs, int sessionCode)
        {
            QueryConditions conditions = new QueryConditions
            {
                TarjetaCredito = tarjeta,
                TipoMovimientoBanco = EBankLineType.ExtractoTarjeta,
                FechaIni = source.Vencimiento.Date,
                FechaFin = source.Vencimiento.Date,
                Estado = EEstado.NoAnulado,
                Pago = source
            };

            return Get(conditions, childs, sessionCode);
        }

        public static BankLine GetByCuentaBancaria(BankAccountInfo source, int sessionCode)
        {
            QueryConditions conditions = new QueryConditions
            {
                CuentaBancaria = source,
                TipoMovimientoBanco = EBankLineType.ComisionEstudioApertura,
                Estado = EEstado.NoAnulado
            };

            return Get(conditions, false, sessionCode);
        }

        public static BankLine Get(Library.Invoice.QueryConditions conditions, bool childs)
        {
            return Get(BankLine.SELECT(conditions), childs);
        }
        public static BankLine Get(Library.Invoice.QueryConditions conditions, bool childs, int sessionCode)
        {
            return Get(BankLine.SELECT(conditions), childs, sessionCode);
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
        /// Elimina todos los MovimientoBanco. 
        /// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = BankLine.OpenSession();
            ISession sess = BankLine.Session(sessCode);
            ITransaction trans = BankLine.BeginTransaction(sessCode);

            try
            {
                sess.Delete("from BankLineRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                BankLine.CloseSession(sessCode);
            }
        }

        public override BankLine Save()
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

                if (!SharedTransaction) Transaction().Commit();
                return this;
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
				if (!SharedTransaction)
				{
					if (CloseSessions) CloseSession();
					else BeginTransaction();
				}
            }

            return this;
        }

        public override BankLine SaveAsChild()
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

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                base.SaveAsChild();
            }
            catch (Exception ex)
            {
                //if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
            }

            return this;
        }

        private static void AnulaBasico(IBankLineInfo source, int sessionCode)
        {
            switch (source.ETipoMovimientoBanco)
            {
                case EBankLineType.Cobro:

                    ChargeBankLine.Annul(source, sessionCode);
                    break;

                default:

                    GeneralBankLine.Annul(source, sessionCode);
                    break;
            }
        }

        public static void AnulaItem(IBankLine source, int sessionCode) { AnulaItem(source.IGetInfo(false), sessionCode); }
        public static void AnulaItem(IBankLineInfo source, int sessionCode)
        {
            switch (source.ETipoMovimientoBanco)
            {
                case EBankLineType.Traspaso:
                case EBankLineType.CancelacionComercioExterior:

                    TransactionBankLine.Annul(source, sessionCode);
                    break;

                case EBankLineType.PagoPrestamo:

                    LoanPaymentBankLine.Annul(source, sessionCode);
                    break;

                case EBankLineType.PagoFactura:
                case EBankLineType.PagoGasto:
                case EBankLineType.PagoNomina:
                    {
                        switch (source.EMedioPago)
                        {
                            case EMedioPago.ComercioExterior:

                                MerchantBankLine.Annul(source, sessionCode);
                                break;

                            default:

                                AnulaBasico(source, sessionCode);
                                break;
                        }
                    }
                    break;

                default:

                    AnulaBasico(source, sessionCode);
                    break;
            }
        }

        public static void AnulaItemTarjeta(IBankLine source, CreditCardInfo tarjeta, int sessionCode) { AnulaItemTarjeta(source.IGetInfo(false), tarjeta, sessionCode); }
        public static void AnulaItemTarjeta(IBankLineInfo source, CreditCardInfo tarjeta, int sessionCode)
        {
            if (source.EMedioPago != EMedioPago.Tarjeta) return;

            switch (tarjeta.ETipoTarjeta)
            {
                case ETipoTarjeta.Debito:

                    AnulaBasico(source, sessionCode);
                    break;
            }
        }

        public static void AnulaItemComisiones(BankAccountInfo source, int sessionCode)
        {
            BankLine mv = BankLine.GetByCuentaBancaria(source, sessionCode);
            if (mv != null && mv.Oid != 0)
            {
                mv.EEstado = EEstado.Anulado;
                mv.Observaciones = String.Format(Library.Invoice.Resources.Labels.ANULADO, DateTime.Now.ToString(), AppContext.User.Name, string.Empty, source.Entidad) + Environment.NewLine
                                    + mv.Observaciones;
                mv.SaveAsChild();
            }
        }

        private static void EditBasico(IBankLineInfo source, int sessionCode)
        {
            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions
            {
                IMovimientoBanco = source,
                Estado = EEstado.Abierto,
                TipoTitular = source.ETipoTitular
            };

            BankLine mv = BankLine.Get(conditions, false, sessionCode);

            if (mv != null && mv.Oid != 0)
            {
                if (source is PaymentInfo)
                {
                    mv.CopyFrom(source);
                    PaymentInfo pago = null;

                    if (((PaymentInfo)source).EEstadoPago != EEstado.Pagado)
                    {
                        AnulaItem(source, sessionCode);
                        return;
                    }

                    switch (source.ETipoMovimientoBanco)
                    {
                        case EBankLineType.PagoFactura:

                            if (mv.EMedioPago == EMedioPago.ComercioExterior && source.EMedioPago != EMedioPago.ComercioExterior)
                            {
                                Loans prestamos = Loans.GetList(conditions, sessionCode);
                                Loan pr = prestamos[0];
                                PaymentList pagos = PaymentList.GetListByPrestamo(pr.GetInfo(), false);
                                if (pagos.Count > 0)
                                    throw new iQException("No es posible modificar un préstamo con pagos asociados");

                                pr.CopyFrom((PaymentInfo)source);
                                prestamos.SaveAsChild();
                            }
                            pago = (PaymentInfo)source;
                            mv.Importe = -(pago.Importe + pago.GastosBancarios);
                            break;

                        case EBankLineType.PagoPrestamo:
                        case EBankLineType.PagoGasto:
                        case EBankLineType.PagoNomina:
                            {
                                pago = (PaymentInfo)source;
                                mv.Importe = -(pago.Importe + pago.GastosBancarios);
                            }
                            break;

                        default:
                            break;
                    }

                    mv.SaveAsChild();
                }
                else
                {
                    mv.CopyFrom(source);
                    mv.SaveAsChild();
                }
            }
            else if (source.Confirmado)
            {
                InsertItem(source, sessionCode);
            }
        }

        public static void EditItem(IBankLine source, IBankLineInfo oldSource, int sessionCode) { EditItem(source.IGetInfo(false), oldSource, sessionCode); }
        public static void EditItem(IBankLineInfo source, IBankLineInfo oldSource, int sessionCode)
        {
            if (source is CashLineInfo)
            {
                if ((source as CashLineInfo).EEstado == EEstado.Closed)
                    return;
            }

            if (source.EEstado == EEstado.Anulado
                || (source is Payment && (source as Payment).EEstadoPago != EEstado.Pagado)
                || (source is Charge && (source as Charge).EEstadoCobro != EEstado.Charged))
            {
                AnulaItem(source, sessionCode);
                return;
            }

            //Si se modifica el año de la operación se le da un ID correspondiente al año nuevo
            if (oldSource.Vencimiento.Year != source.Vencimiento.Year)
            {
                AnulaItem(source, sessionCode);
                InsertItem(source, sessionCode);
                return;
            }

            switch (source.ETipoMovimientoBanco)
            {
                case EBankLineType.Cobro:

                    ChargeBankLine.Edit(source, oldSource, sessionCode);
                    break;

                case EBankLineType.CobroEfecto:

                    FinancialCashChargeBankLine.Edit(source, oldSource, sessionCode);
                    break;

                case EBankLineType.Traspaso:
                case EBankLineType.CancelacionComercioExterior:

                    TransactionBankLine.Edit(source, oldSource, sessionCode);
                    break;

                case EBankLineType.PagoPrestamo:

                    LoanPaymentBankLine.Edit(source, oldSource, sessionCode);
                    break;

                case EBankLineType.Prestamo:

                    LoanBankLine.Edit(source, oldSource, sessionCode);
                    break;

                case EBankLineType.PagoFactura:
                case EBankLineType.PagoGasto:
                case EBankLineType.PagoNomina:
                    {
                        switch (source.EMedioPago)
                        {
                            case EMedioPago.ComercioExterior:

                                MerchantBankLine.Edit(source, oldSource, sessionCode);
                                break;

                            default:

                                EditBasico(source, sessionCode);
                                break;
                        }
                    }
                    break;

                default:

                    EditBasico(source, sessionCode);
                    break;
            }
        }

        public static void EditItemTarjeta(Payment source, PaymentInfo oldSource, int sessionCode)
        {
            EditItemTarjeta(source.GetInfo(false), oldSource, sessionCode);
        }
        public static void EditItemTarjeta(PaymentInfo source, PaymentInfo oldSource, int sessionCode)
        {
            if (source.EMedioPago == EMedioPago.Tarjeta)
            {
                CreditCardInfo tarjeta = CreditCardInfo.Get(source.OidTarjetaCredito, false);

                if (source.EEstado == EEstado.Anulado)
                {
                    if ((source.OidTarjetaCredito != oldSource.OidTarjetaCredito) || (source.Vencimiento.Date != oldSource.Vencimiento.Date))
                    {
                        AnulaItemTarjeta(oldSource, tarjeta, sessionCode);
                        InsertItemTarjeta(source, tarjeta, sessionCode);
                    }
                    else
                        AnulaItemTarjeta(oldSource, tarjeta, sessionCode);
                    return;
                }

                switch (tarjeta.ETipoTarjeta)
                {
                    case ETipoTarjeta.Debito:
                        {
                            EditBasico(source, sessionCode);
                        }
                        break;
                }

                if (oldSource.EMedioPago == EMedioPago.Tarjeta)
                {
                    //Editamos el movimento anterior asociado
                    if ((source.OidTarjetaCredito != oldSource.OidTarjetaCredito) || (source.Vencimiento.Date != oldSource.Vencimiento.Date))
                    {
                        CreditCardInfo oldTarjeta = CreditCardInfo.Get(oldSource.OidTarjetaCredito, false);

                        switch (oldTarjeta.ETipoTarjeta)
                        {
                            case ETipoTarjeta.Debito:
                                {
                                    if (tarjeta.ETipoTarjeta == ETipoTarjeta.Credito)
                                        AnulaItemTarjeta(oldSource, oldTarjeta, sessionCode);
                                }
                                break;
                        }
                    }
                }
                else
                {
                    //Antes no era un pago de tarjeta y ahora sí
                    AnulaItem(oldSource, sessionCode);
                }
            }
            //Antes era un pago de tarjeta y ahora no
            else if (oldSource.EMedioPago == EMedioPago.Tarjeta)
            {
                CreditCardInfo tarjeta = CreditCardInfo.Get(oldSource.OidTarjetaCredito, false);
                AnulaItemTarjeta(oldSource, tarjeta, sessionCode);
                InsertItem(source, sessionCode);
            }
        }

        public static void EditItemComisiones(BankAccountInfo source, BankAccountInfo oldSource, int sessionCode)
        {
            BankLine mv = BankLine.GetByCuentaBancaria(source, sessionCode);

            //Si existe el gasto 
            if (mv != null && mv.Oid != 0)
            {
                mv.CopyFrom(source);
                mv.SaveAsChild();
            }
            //Si no existe lo añadimos nuevo
            else
            {
                InsertItemComisiones(source, sessionCode);
            }

        }

        private static BankLine InsertBasico(IBankLineInfo source, bool confirmar)
        {
            BankLine mv = BankLine.New();

            if (source is Payment && ((Payment)source).EEstadoPago != EEstado.Pagado) return mv;
            else if (source is Charge && ((Charge)source).EEstadoCobro != EEstado.Charged) return mv;            

            if (confirmar || source.Confirmado)
            {
                mv = BankLine.New(source);
                mv.Save();
                mv.CloseSession();
            }
            else if (source.Vencimiento.ToShortDateString() == DateTime.Today.ToShortDateString())
            {
                mv = BankLine.New(source);
                mv.Save();
                mv.CloseSession();
            }

            return mv;
        }
        private static BankLine InsertBasico(IBankLineInfo source, bool force, int sessionCode)
        {
            if (source is Payment && ((Payment)source).EEstadoPago != EEstado.Pagado) return BankLine.New();
            else if (source is Charge && ((Charge)source).EEstadoCobro != EEstado.Charged) return BankLine.New();
            else if (source is FinancialCash && ((FinancialCash)source).EEstadoCobro == EEstado.Pendiente) return BankLine.New();
            
            if (force || source.Confirmado)
            {
                switch (source.ETipoMovimientoBanco)
                {
                    case EBankLineType.PagoPrestamo:
                    case EBankLineType.PagoFactura:
                    case EBankLineType.PagoGasto:
                    case EBankLineType.PagoNomina:

                        return PaymentBankLine.Insert(source, sessionCode);

                    case EBankLineType.Cobro:

                        return ChargeBankLine.Insert(source, sessionCode);

                    case EBankLineType.CobroEfecto:

                        return FinancialCashChargeBankLine.Insert(source, sessionCode);

                    default:

                        return GeneralBankLine.Insert(source, sessionCode);
                }
            }
            else if (source.Vencimiento.Date == DateTime.Today.Date)
            {
                switch (source.ETipoMovimientoBanco)
                {
                    case EBankLineType.PagoPrestamo:
                    case EBankLineType.PagoFactura:
                    case EBankLineType.PagoGasto:
                    case EBankLineType.PagoNomina:

                        return PaymentBankLine.Insert(source, sessionCode);

                    default:
                        break;
                }
            }

            return BankLine.New();
        }
        
        public static BankLine InsertItem(IBankLine source, int sessionCode = -1) { return InsertItem(source, false, sessionCode); }
        public static BankLine InsertItem(IBankLine source, bool force, int sessionCode = -1) { return InsertItem(source.IGetInfo(false), force, sessionCode); }
        public static BankLine InsertItem(IBankLineInfo source, int sessionCode = -1) { return InsertItem(source, false, sessionCode); }
        public static BankLine InsertItem(IBankLineInfo source, bool force, int sessionCode = -1)
        {
            if (source is PaymentInfo && ((PaymentInfo)source).EEstadoPago != EEstado.Pagado) return BankLine.New();
            else if (source is ChargeInfo && ((ChargeInfo)source).EEstadoCobro != EEstado.Charged) return BankLine.New();
            else if (source is FinancialCashInfo && ((FinancialCashInfo)source).EEstadoCobro == EEstado.Pendiente) return BankLine.New();

            if (!Common.EnumFunctions.NeedsCuentaBancaria(source.EMedioPago)
                && source.ETipoMovimientoBanco != EBankLineType.PagoPrestamo) return BankLine.New();

            switch (source.ETipoMovimientoBanco)
            {
                case EBankLineType.Traspaso:
                case EBankLineType.CancelacionComercioExterior:

                    return TransactionBankLine.Insert(source, sessionCode);
  
                case EBankLineType.PagoPrestamo:

                    return LoanPaymentBankLine.Insert(source, sessionCode);

                case EBankLineType.PagoFactura:
                case EBankLineType.PagoGasto:
                case EBankLineType.PagoNomina:

                    switch (source.EMedioPago)
                    {
                        case EMedioPago.ComercioExterior:

                            return MerchantBankLine.Insert(source, sessionCode);

                        default:

                            return InsertBasico(source, force, sessionCode);
                    }

                case EBankLineType.Prestamo:

                    return LoanBankLine.Insert(source, sessionCode);

                default:

                    return InsertBasico(source, force, sessionCode);
            }
        }

        public static BankLine InsertItemTarjeta(IBankLine source, CreditCardInfo tarjeta, int sessionCode = -1) 
        { 
            return InsertItemTarjeta(source.IGetInfo(false), tarjeta, sessionCode);
        }
        public static BankLine InsertItemTarjeta(IBankLineInfo source, CreditCardInfo tarjeta, int sessionCode = -1)
        {
            BankLine bank_line = BankLine.New();
            bank_line.Serial = 0;

            if (source.EMedioPago != EMedioPago.Tarjeta) return BankLine.New();

            switch (tarjeta.ETipoTarjeta)
            {
                case ETipoTarjeta.Debito:
                    {
                        return InsertBasico(source, source.Confirmado, sessionCode);
                    }

                default:
                    return BankLine.New();
            }            
        }

        public static bool InsertItemComisiones(BankAccountInfo source, int sessionCode)
        {
            BankLines mvs = BankLines.NewList();
            mvs.SessionCode = sessionCode;

            BankLine mv = mvs.NewItem(source);
            mvs.SaveAsChild();

            return true;
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LO UTILIZA LA FUNCION DE CREACION DE LA LISTA DEL PADRE
        /// </summary>
        private BankLine(IBankLine parent)
        {
            OidOperacion = parent.Oid;
            MarkAsChild();
        }

        /// <summary>
        /// Crea un nuevo objeto hijo
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        /// <returns>Nuevo objeto creado</returns>
        internal static BankLine NewChild(IBankLine parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new BankLine(parent);
        }
        internal static BankLine NewChild(IBankLineInfo parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            BankLine obj = BankLine.New(parent);
            obj.OidOperacion = parent.Oid;
            obj.MarkAsChild();

            return obj;
        }        
        internal static BankLine NewChild(BankAccountInfo parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            BankLine obj = BankLine.New();
            obj.CopyFrom(parent);
            obj.MarkAsChild();

            return obj;
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
            Estado = (long)EEstado.Abierto;
            FechaOperacion = DateTime.Now;
            FechaCreacion = DateTime.Now;
            TipoOperacion = (long)EBankLineType.Manual;
            TipoCuenta = (long)ECuentaBancaria.Principal;
            MedioPago = (long)EMedioPago.Transferencia;
            TipoTitular = (long)ETipoTitular.Varios;
            ETipo = ETipoApunteBancario.Sencillo;

            GetNewCode();
        }

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="source">Objeto fuente</param>
        private void Fetch(BankLine source)
        {
            try
            {
                SessionCode = source.SessionCode;
                _base.CopyValues(source);
            }
            catch (Exception ex) { throw ex; }

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
            catch (Exception ex) { throw ex; }

            MarkOld();
        }

        /// <summary>
        /// Inserta el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
        internal void Insert(BankLines parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                SessionCode = parent.SessionCode;
                GetNewCode(parent);

                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(
                        Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                parent.Session().Save(Base.Record);
            }
            catch (Exception ex) { throw ex; }

            MarkOld();
        }

        /// <summary>
        /// Actualiza el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
        internal void Update(BankLines parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(
                        Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SessionCode = parent.SessionCode;
				BankLineRecord obj = Session().Get<BankLineRecord>(Oid);
                
                if (obj.Auditado &&
                    (obj.FechaOperacion.Date != FechaOperacion.Date ||
                    obj.Importe != Importe ||
                    obj.OidCuentaMov != OidCuentaMov ||
                    obj.TipoOperacion != TipoOperacion ||
                    (obj.Estado != (long)EEstado.Anulado && EEstado == EEstado.Anulado)))
                    throw new iQException(Resources.Messages.MOVIMIENTO_BANCARIO_AUDITADO);

                obj.CopyValues(Base.Record);
                Session().Update(obj);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        /// <summary>
        /// Borra el registro de la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
        internal void DeleteSelf(BankLines parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<BankLineRecord>(Oid));
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
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
                    BankLine.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);
                }

                MarkOld();
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
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
                    BankLineRecord record = Session().Get<BankLineRecord>(Oid);
                    BankLineInfo obj_aux = BankLineInfo.Get(record.Oid, (EBankLineType)record.TipoOperacion, this.ETipoTitular);

                    if (obj_aux.EEstado != EEstado && obj_aux.EEstado == EEstado.Anulado)
                    {
                        if (ETipoMovimientoBanco != EBankLineType.Manual)
                            throw new iQException(Library.Resources.Messages.ACTION_NOT_ALLOWED);
                    }

                    //Es SharedTransaction cuando el movimiento de banco lo guarda alguna otra entidad
                    //Cobro o Pago, si se guarda él mismo no hay que lanzar el error
                    if (record.Auditado && SharedTransaction &&
                        (record.FechaOperacion.Date != FechaOperacion.Date ||
                        record.Importe != Importe ||
                        record.OidCuentaMov != OidCuentaMov ||
                        record.TipoOperacion != TipoOperacion ||
                        (record.Estado != (long)EEstado.Anulado && EEstado == EEstado.Anulado)))
                        throw new iQException(Resources.Messages.MOVIMIENTO_BANCARIO_AUDITADO);

                    //Si se cambia el año del movimiento, se anula y se crea uno nuevo
                    /*if (obj.FechaVencimiento.Year != FechaVencimiento.Year)
                    {
                        if (ETipo != ETipoApunteBancario.Sencillo)
                            throw new iQException(Resources.Messages.ERROR_APUNTE_MULTIPLE_EDIT);

                        obj.EEstado = EEstado.Anulado;
                        MovimientoBanco mv = this.CloneAsNew();
                        mv.Save();
                    }
                    else
                        obj.CopyValues(this);*/

                    BankLine obj = BankLine.Get(record.Oid, false, SessionCode);

                    if (EntityBase.UpdateByYear(obj, this, null))
                    {
                        obj.Save();
                        Transaction().Commit();
                        CloseSession();
                        NewTransaction();
                    }
                    else
                    {
                        record.CopyValues(this.Base.Record);
                        Session().Update(record);
                        //obj.CloseSession();
                    }

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

                //Si no hay integridad referencial, aquí se deben borrar las listas hijo
                CriteriaEx criterio = GetCriteria();
                criterio.AddOidSearch(criteria.Oid);
                Session().Delete((BankLineRecord)(criterio.UniqueResult()));
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

        #region Child Data Access

        /// <summary>
        /// Inserta un registro en la base de datos
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        internal void Insert(IBankLine parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

            try
            {
                OidOperacion = parent.Oid;
                GetNewCode();

                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                parent.Session().Save(Base.Record);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        /// <summary>
        /// Actualiza un registro en la base de datos
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        internal void Update(IBankLine parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

            try
            {
                OidOperacion = parent.Oid;

                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				BankLineRecord obj = parent.Session().Get<BankLineRecord>(Oid);

                if (obj.Auditado &&
                    (obj.FechaOperacion.Date != FechaOperacion.Date ||
                    obj.Importe != Importe ||
                    obj.OidCuentaMov != OidCuentaMov ||
                    obj.TipoOperacion != TipoOperacion ||
                    (obj.Estado != (long)EEstado.Anulado && EEstado == EEstado.Anulado)))
                    throw new iQException(Resources.Messages.MOVIMIENTO_BANCARIO_AUDITADO);

                obj.CopyValues(Base.Record);
                parent.Session().Update(obj);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        /// <summary>
        /// Borra un registro de la base de datos.
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        /// <remarks>Borrado inmediato<remarks/>
        internal void DeleteSelf(IBankLine parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<BankLineRecord>(Oid));
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkNew();
        }

        #endregion

        #region SQL

        public delegate string SelectLocalCaller(EBankLineType bankLineType, ETipoTitular tipo_titular);
        public static SelectLocalCaller local_caller_SELECT = new SelectLocalCaller(SELECT_BASE);

        public new static string SELECT(long oid) { return SELECT(new QueryConditions() { Oid = oid }, true); }
        public static string SELECT(long oid, EBankLineType bankLineType, ETipoTitular tipo_titular) { return SELECT(oid, bankLineType, tipo_titular, true); }
        public static string SELECT(Library.Invoice.QueryConditions conditions) { return SELECT(conditions, true); }
        public static string SELECT_RESUMEN_CUENTA(Library.Common.QueryConditions conditions)
        {
            string query = string.Empty;

            QueryConditions conds = QueryConditions.ConvertTo(conditions);

            query = "(SELECT MV1.\"OID_CUENTA\", SUM(MV1.\"SALDO\") AS \"SALDO\"" +
                    " FROM (" + SELECT_RESUMEN_CUENTA_BUILDER(conds) + ") AS MV1" +
                    " GROUP BY MV1.\"OID_CUENTA\")";

            //if (lock_table) query += " FOR UPDATE OF MV NOWAIT";

            return query;
        }

        public static string SELECT_SALDOS()
        {
            string query = @"
				(SELECT ""OID_CUENTA_EX"" AS ""OID_CUENTA""
						,SUM(""IMPORTE"") AS ""SALDO""
				FROM (" + SELECT(new QueryConditions()) + @") AS MOVS
                WHERE ""ESTADO"" != " + (long)EEstado.Anulado + @"
                GROUP BY ""OID_CUENTA_EX"")";

            return query;
        }

        internal static string FIELDS_BASE()
        {
            string query = @"
			SELECT MV.*
					,US.""NAME"" AS ""AUDITOR""
					,CASE WHEN (MV.""TIPO_CUENTA"" = " + (long)ECuentaBancaria.Principal + @") THEN CB.""OID""
						WHEN (MV.""TIPO_CUENTA"" = " + (long)ECuentaBancaria.Asociada + @") THEN CB2.""OID"" END AS ""OID_CUENTA_EX""
					,COALESCE(CASE WHEN (MV.""TIPO_CUENTA"" = " + (long)ECuentaBancaria.Principal + @") THEN CB.""VALOR""
						WHEN (MV.""TIPO_CUENTA"" = " + (long)ECuentaBancaria.Asociada + @") THEN CB2.""VALOR"" END, '') AS ""CUENTA_BANCARIA""
					,COALESCE(CASE WHEN (MV.""TIPO_CUENTA"" = " + (long)ECuentaBancaria.Principal + @") THEN CB.""ENTIDAD""
						WHEN (MV.""TIPO_CUENTA"" = " + (long)ECuentaBancaria.Asociada + @") THEN CB2.""ENTIDAD"" END, 'NO CUENTA ASOCIADA') AS ""ENTIDAD""";

            return query;
        }

		internal static string FIELDS(ETipoTitular tipoTitular)
        {
			string query = 
			FIELDS_BASE() + @"
                ,OP.""MEDIO_PAGO"" AS ""MEDIO_PAGO""
                ,TL.""NOMBRE"" AS ""TITULAR""
                ,TL.""OID"" AS ""OID_TITULAR""
                ," + (long)tipoTitular + @" AS ""TIPO_TITULAR""
                ,COALESCE(RG.""CODIGO"", '') || '/' || COALESCE(LR.""ID_EXPORTACION"", '') AS ""ID_MOV_CONTABLE""";

            return query;
        }

        internal static string FIELDS_EFECTO(ETipoTitular tipoTitular)
        {
            string query =
            FIELDS_BASE() + @"
                ,CR.""MEDIO_PAGO"" AS ""MEDIO_PAGO""
                ,TL.""NOMBRE"" AS ""TITULAR""
                ,TL.""OID"" AS ""OID_TITULAR""
                ," + (long)tipoTitular + @" AS ""TIPO_TITULAR""
                ,COALESCE(RG.""CODIGO"", '') || '/' || COALESCE(LR.""ID_EXPORTACION"", '') AS ""ID_MOV_CONTABLE""";

            return query;
        }

        internal static string FIELDS_MANUAL()
        {
            string query = 
			FIELDS_BASE() + @"
				,(CASE WHEN (MV.""IMPORTE"" > 0) THEN " + (long)EMedioPago.Ingreso + 
					@" ELSE " + ((long)EMedioPago.Transferencia).ToString() + @" END) AS ""MEDIO_PAGO""
                ,'' AS ""TITULAR""
                ,0 AS ""OID_TITULAR""
                ," + (long)ETipoTitular.Todos + @" AS ""TIPO_TITULAR""
                ,'' AS ""ID_MOV_CONTABLE""";

            return query;
        }

        internal static string FIELDS_CAJA()
        {
            string query = 
			FIELDS_BASE() + @"
				,(CASE WHEN (MV.""IMPORTE"" > 0) THEN " + (long)EMedioPago.Ingreso + " ELSE " + (long)EMedioPago.Cheque + @" END) AS ""MEDIO_PAGO""
                ,'' AS ""TITULAR""
                ,OP.""OID_CAJA"" AS ""OID_TITULAR""
                ," + (long)ETipoTitular.Todos + @" AS ""TIPO_TITULAR""
                ,'' AS ""ID_MOV_CONTABLE""";

            return query;
        }

        internal static string FIELDS_PRESTAMO()
        {
            string query = @"
			SELECT MV.*
					,US.""NAME"" AS ""AUDITOR""
                    ,CB.""OID"" AS ""OID_CUENTA_EX""
                    ,COALESCE(CB.""VALOR"", '') AS ""CUENTA_BANCARIA""
                    ,COALESCE(CB.""ENTIDAD"", 'NO CUENTA ASOCIADA') AS ""ENTIDAD""
                    ," + (long)EMedioPago.Todos + @" AS ""MEDIO_PAGO""
                    ,'' ""TITULAR""
                    ,0 AS ""OID_TITULAR""
                    ," + (long)ETipoTitular.Todos + @" AS ""TIPO_TITULAR""
                    ,'' AS ""ID_MOV_CONTABLE""";

            return query;
        }

        internal static string FIELDS_PAGO_PRESTAMO()
        {
            string query = @"
			SELECT MV.*
					,US.""NAME"" AS ""AUDITOR""
                    ,CB.""OID"" AS ""OID_CUENTA_EX""
                    ,COALESCE(CB.""VALOR"", '') AS ""CUENTA_BANCARIA""
                    ,COALESCE(CB.""ENTIDAD"", 'NO CUENTA ASOCIADA') AS ""ENTIDAD""
                    ," + (long)EMedioPago.Todos + @" AS ""MEDIO_PAGO""
                    ,'' ""TITULAR""
                    ,OP.""OID"" AS ""OID_TITULAR""
                    ," + (long)ETipoTitular.Todos + @" AS ""TIPO_TITULAR""
                    ,'' AS ""ID_MOV_CONTABLE""";

            return query;
        }

        internal static string FIELDS_COMISIONES()
        {
            string query = @"
			SELECT MV.*
                	,US.""NAME"" AS ""AUDITOR""
                	,CB2.""OID"" AS ""OID_CUENTA_EX""
                	,COALESCE(CB2.""VALOR"", '') AS ""CUENTA_BANCARIA""
                	,COALESCE(CB2.""ENTIDAD"", 'NO CUENTA ASOCIADA') AS ""ENTIDAD""
                	," + (long)EMedioPago.Todos + @" AS ""MEDIO_PAGO""
                	,CB.""ENTIDAD"" ""TITULAR""
                    ,CB.""OID"" AS ""OID_TITULAR""
                	," + (long)ETipoTitular.Todos + @" AS ""TIPO_TITULAR""
                	,'' AS ""ID_MOV_CONTABLE""";

            return query;
        }

        internal static string FIELDS_TRASPASO()
        {
            string query = 
			FIELDS_BASE() + @"
					,OP.""MEDIO_PAGO"" AS ""MEDIO_PAGO""
					,'' ""TITULAR""
                    ,OP.""OID"" AS ""OID_TITULAR""
					," + (long)ETipoTitular.Todos + @" AS ""TIPO_TITULAR""
					,'' AS ""ID_MOV_CONTABLE""";

            return query;
        }

        internal static string FIELDS_GASTOS(EBankLineType bankLineType, ETipoTitular tipoTitular)
        {
            string query = 
			FIELDS_BASE() + @"
					,OP.""MEDIO_PAGO"" AS ""MEDIO_PAGO""
					,'' AS ""TITULAR""
                    ,OP.""OID"" AS ""OID_TITULAR""
					," + (long)tipoTitular + @" AS ""TIPO_TITULAR""
					,COALESCE(RG.""CODIGO"", '') || '/' || COALESCE(LR.""ID_EXPORTACION"", '') AS ""ID_MOV_CONTABLE""";

            return query;
        }

        internal static string FIELDS_RESUMEN_CUENTA()
        {
            string query = @"
			SELECT CB.""OID"" AS ""OID_CUENTA""
                	,SUM(MV.""IMPORTE"") AS ""SALDO""";

            return query;
        }

        internal static string JOIN(EBankLineType bankLineType) { return JOIN(bankLineType, ETipoCobro.Todos); }
        internal static string JOIN(EBankLineType bankLineType, ETipoCobro tipo_cobro)
        {
			string bl = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));
            string tr = nHManager.Instance.GetSQLTable(typeof(BankTransferRecord));
            string ch = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(ExpenseRecord));
            string chl = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));
            string ba = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
            string su = nHManager.Instance.GetSQLTable(typeof(SupplierRecord));
            string rl = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
			string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));
            string fc = nHManager.Instance.GetSQLTable(typeof(FinancialCashRecord));

            string query = string.Empty;

            switch (bankLineType)
            {
                case EBankLineType.Manual:
                case EBankLineType.Interes:

                    query += string.Empty;

                    break;

                case EBankLineType.Cobro:

                    query += @"
                    INNER JOIN " + ch + @" AS OP ON OP.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)bankLineType;
                    
                    if (tipo_cobro != ETipoCobro.Todos) 
                        query += @"
                            AND OP.""TIPO_COBRO"" = " + (long)tipo_cobro;
                    
                    query += @"
                    LEFT JOIN " + cl + @" AS TL ON TL.""OID"" = OP.""OID_CLIENTE""
                    LEFT JOIN (SELECT MAX(""ID_EXPORTACION"") AS ""ID_EXPORTACION"", ""OID_ENTIDAD"", MAX(""OID_REGISTRO"") AS ""OID_REGISTRO""
                                FROM " + rl + @" AS LR
                                WHERE LR.""TIPO_ENTIDAD"" = " + (long)ETipoEntidad.Cobro + @"
                                    AND LR.""ESTADO"" = " + (long)EEstado.Contabilizado + @"
                                GROUP BY ""OID_ENTIDAD"")
                        AS LR ON OP.""OID"" = LR.""OID_ENTIDAD""
                    LEFT JOIN " + rg + @" AS RG ON RG.""OID"" = LR.""OID_REGISTRO""";

                    break;

                case EBankLineType.CobroEfecto:

                    string tipos = "(" + (long)EBankLineType.CobroEfecto + ", " +
                                    (long)EBankLineType.PagoGastosEfecto + ", " +
                                    (long)EBankLineType.PagoGastosAdelantoEfecto + ", " +
                                    (long)EBankLineType.PagoGastosDevolucionEfecto + ")";

                    query += @" 
                    INNER JOIN " + fc + @" AS OP ON OP.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" IN " + tipos + @"
                    INNER JOIN " + ch + @" AS CR ON CR.""OID"" = OP.""OID_COBRO"" AND CR.""TIPO_COBRO"" = " + (long)ETipoCobro.Cliente + @"
                    LEFT JOIN " + cl + @" AS TL ON TL.""OID"" = CR.""OID_CLIENTE""
                    LEFT JOIN (SELECT MAX(""ID_EXPORTACION"") AS ""ID_EXPORTACION""
                                        ,""OID_ENTIDAD""
                                        ,MAX(""OID_REGISTRO"") AS ""OID_REGISTRO""
                                FROM " + rl + @" AS LR
                                WHERE LR.""TIPO_ENTIDAD"" = " + (long)ETipoEntidad.Cobro + @"
                                    AND LR.""ESTADO"" = " + (long)EEstado.Contabilizado + @"
                                GROUP BY ""OID_ENTIDAD"")
                        AS LR ON OP.""OID"" = LR.""OID_ENTIDAD""
                    LEFT JOIN " + rg + @" AS RG ON RG.""OID"" = LR.""OID_REGISTRO""";

                    break;

                case EBankLineType.ExtractoTarjeta:

                    string tc = nHManager.Instance.GetSQLTable(typeof(CreditCardRecord));

                    query += @"
                    INNER JOIN " + pa + @" AS OP ON OP.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)bankLineType + @"
                    LEFT JOIN (SELECT ""OID""
                                        ,(""NOMBRE"" || ' ' || ""NUMERACION"") AS ""NOMBRE""
                                FROM " + tc + @") 
                        AS TL ON TL.""OID"" = OP.""OID_TARJETA_CREDITO""
                    LEFT JOIN (SELECT MAX(""ID_EXPORTACION"") AS ""ID_EXPORTACION""
                                        ,""OID_ENTIDAD""
                                        ,MAX(""OID_REGISTRO"") AS ""OID_REGISTRO""
                                FROM " + rl + @" AS LR
                                WHERE LR.""TIPO_ENTIDAD"" = " + (long)ETipoEntidad.Pago + @"
                                    AND LR.""ESTADO"" = " + (long)EEstado.Contabilizado + @"
                                GROUP BY ""OID_ENTIDAD"")
                        AS LR ON OP.""OID_TARJETA_CREDITO"" = LR.""OID_ENTIDAD""
                    LEFT JOIN " + rg + @" AS RG ON RG.""OID"" = LR.""OID_REGISTRO""";

                    break;

                case EBankLineType.PagoFactura:

                    query += @"
                    INNER JOIN " + pa + @" AS OP ON OP.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)bankLineType + @"
                    LEFT JOIN (SELECT MAX(""ID_EXPORTACION"") AS ""ID_EXPORTACION""
                                        ,""OID_ENTIDAD""
                                        ,MAX(""OID_REGISTRO"") AS ""OID_REGISTRO""
                                FROM " + rl + @" AS LR
                                WHERE LR.""TIPO_ENTIDAD"" = " + (long)ETipoEntidad.Pago + @"
                                    AND LR.""ESTADO"" = " + (long)EEstado.Contabilizado + @"
                                GROUP BY ""OID_ENTIDAD"")
                        AS LR ON OP.""OID"" = LR.""OID_ENTIDAD""
                    LEFT JOIN " + rg + @" AS RG ON RG.""OID"" = LR.""OID_REGISTRO""";

                    break;

                case EBankLineType.PagoGasto:
				case EBankLineType.PagoNomina:

                    ETipoPago tipo_pago = Library.Store.EnumConvert.ToETipoPago(bankLineType);

                    query += @"
                    INNER JOIN " + pa + @" AS OP ON OP.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)bankLineType + @"
                    LEFT JOIN (SELECT MAX(""ID_EXPORTACION"") AS ""ID_EXPORTACION""
                                        ,""OID_ENTIDAD""
                                        ,MAX(""OID_REGISTRO"") AS ""OID_REGISTRO""
                                FROM " + rl + @" AS LR
                                WHERE LR.""TIPO_ENTIDAD"" = " + (long)ETipoEntidad.Pago + @"
                                    AND LR.""ESTADO"" = " + (long)EEstado.Contabilizado + @"
                                GROUP BY ""OID_ENTIDAD"")
                        AS LR ON OP.""OID"" = LR.""OID_ENTIDAD""
                    LEFT JOIN " + rg + @" AS RG ON RG.""OID"" = LR.""OID_REGISTRO""";

                    break;

                case EBankLineType.PagoPrestamo:

                    tipo_pago = Library.Store.EnumConvert.ToETipoPago(bankLineType);

                    query += @"
                    INNER JOIN " + pa + @" AS OP ON OP.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)bankLineType;
                    
                    break;

				case EBankLineType.EntradaCaja:
					
					query += @"
                    INNER JOIN (SELECT *, " + (long)EMedioPago.Cheque + @" AS ""MEDIO_PAGO""
                                FROM " + chl + @")
                        AS OP ON OP.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)bankLineType;

                    break;

                case EBankLineType.SalidaCaja:

                    query += @"
                    INNER JOIN (SELECT *, " + (long)EMedioPago.Ingreso + @" AS ""MEDIO_PAGO""
                                FROM " + chl + @")
                        AS OP ON OP.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)bankLineType + @"
                    LEFT JOIN " + pa + @" AS POP ON POP.""OID"" = OP.""OID_PAGO""
                    LEFT JOIN " + ch + @" AS COP ON COP.""OID"" = OP.""OID_COBRO""";

                    break;

                case EBankLineType.Traspaso:

                    query = @"
                    INNER JOIN (SELECT TR.* 
                                        ,CASE WHEN (MV1.""IMPORTE"" > 0) THEN ""OID_CUENTA_DESTINO"" 
                                            ELSE ""OID_CUENTA_ORIGEN"" END AS ""OID_CUENTA_BANCARIA""
                                        ," + (long)EMedioPago.Transferencia + @" AS ""MEDIO_PAGO""
                                FROM " + tr + @" AS TR
                                INNER JOIN " + bl + @" AS MV1 ON MV1.""OID_OPERACION"" = TR.""OID"" AND MV1.""TIPO_OPERACION"" = " + (long)EBankLineType.Traspaso + @")
                        AS OP ON OP.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)EBankLineType.Traspaso;
                    
                    break;

                case EBankLineType.CancelacionComercioExterior:

                    query = @"
                    INNER JOIN (SELECT TR.*
                                        ,CASE WHEN (MV1.""IMPORTE"" > 0) THEN ""OID_CUENTA_DESTINO"" 
                                            ELSE ""OID_CUENTA_ORIGEN"" END AS ""OID_CUENTA_BANCARIA""
                                        ," + (long)EMedioPago.Transferencia + @" AS ""MEDIO_PAGO""
                                FROM " + tr + @" AS TR
                                INNER JOIN " + bl + @" AS MV1 ON MV1.""OID_OPERACION"" = TR.""OID"" AND MV1.""TIPO_OPERACION"" = " + (long)EBankLineType.CancelacionComercioExterior + @")
                        AS OP ON OP.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)EBankLineType.CancelacionComercioExterior;
                    
                    break;

                case EBankLineType.ComisionEstudioApertura:

                    query += @"
                    INNER JOIN " + ba + @" AS OP ON OP.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" = " + (long)bankLineType;

                    break;
            }

            switch (bankLineType)
            {
                case EBankLineType.Manual:
                case EBankLineType.Interes:

                    query += @"
                    LEFT JOIN " + ba + @" AS CB ON CB.""OID"" = MV.""OID_CUENTA""
                    LEFT JOIN " + ba + @" AS CB2 ON CB2.""OID"" = CB.""OID_ASOCIADA""";

                    break;

                case EBankLineType.PagoPrestamo:
                case EBankLineType.Prestamo:

                    query += @"
                    LEFT JOIN " + ba + @" AS CB ON CB.""OID"" = MV.""OID_CUENTA""";

                    break;

                case EBankLineType.SalidaCaja:

                    query += @"
                    LEFT JOIN " + ba + @" AS CB ON (CB.""OID"" = OP.""OID_CUENTA_BANCARIA"" OR CB.""OID"" = POP.""OID_CUENTA_BANCARIA"" OR CB.""OID"" = COP.""OID_CUENTA_BANCARIA"")
                    LEFT JOIN " + ba + @" AS CB2 ON CB2.""OID"" = CB.""OID_ASOCIADA""";

                    break;

                case EBankLineType.ComisionEstudioApertura:

                    query += @"
                    LEFT JOIN " + ba + @" AS CB ON CB.""OID"" = OP.""OID""
                    LEFT JOIN " + ba + @" AS CB2 ON CB2.""OID"" = CB.""OID_ASOCIADA""";

                    break;

                default:

                    query += @"
                    LEFT JOIN " + ba + @" AS CB ON CB.""OID"" = OP.""OID_CUENTA_BANCARIA""
                    LEFT JOIN " + ba + @" AS CB2 ON CB2.""OID"" = CB.""OID_ASOCIADA""";
                    break;
            }

            return query;
        }

        internal static string JOIN_PRESTAMO(QueryConditions conditions)
        {
			string mv = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(LoanRecord));

            string query = " INNER JOIN " + pr + " AS OP ON OP.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)EBankLineType.Prestamo;

            switch (conditions.TipoCuenta)
            {
                case ECuentaBancaria.Principal:
                    {
                        query += " LEFT JOIN " + cb + " AS CB ON CB.\"OID\" = OP.\"OID_CUENTA\"";
                    } break;

                case ECuentaBancaria.Asociada:
                    {
                        query += " LEFT JOIN " + cb + " AS CB2 ON CB2.\"OID\" = OP.\"OID_CUENTA\"" +
                            " LEFT JOIN " + cb + " AS CB ON CB.\"OID\" = CB2.\"OID_ASOCIADA\"";
                    } break;
            }

            return query;
        }

        internal static string JOIN_TRASPASO(QueryConditions conditions, EBankLineType bankLineType)
        {
			string mv = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));
            string tr = nHManager.Instance.GetSQLTable(typeof(BankTransferRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));

            string query;

            if (conditions.IMovimientoBanco.Importe > 0)
            {
                query = " INNER JOIN (SELECT TR.* " +
                        "					,\"OID_CUENTA_DESTINO\" AS \"OID_CUENTA_BANCARIA\"" +
                        "					," + (long)EMedioPago.Transferencia + " AS \"MEDIO_PAGO\"" +
                        "             FROM " + tr + " AS TR)" +
                        "      AS OP ON OP.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)bankLineType + " AND MV.\"IMPORTE\" > 0";
            }
            else if (conditions.IMovimientoBanco.Importe < 0)
            {
                query = " INNER JOIN (SELECT TR.* " +
                        "					,\"OID_CUENTA_ORIGEN\" AS \"OID_CUENTA_BANCARIA\"" +
                        "					," + (long)EMedioPago.Transferencia + " AS \"MEDIO_PAGO\"" +
                        "             FROM " + tr + " AS TR)" +
                        "      AS OP ON OP.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)bankLineType + " AND MV.\"IMPORTE\" < 0";
            }
            else
            {
                query = " INNER JOIN (SELECT TR.* " +
                          "					,\"OID_CUENTA_ORIGEN\" AS \"OID_CUENTA_BANCARIA\"" +
                          "					," + (long)EMedioPago.Transferencia + " AS \"MEDIO_PAGO\"" +
                          "             FROM " + tr + " AS TR)" +
                          "      AS OP ON OP.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)bankLineType;
            }

            query += " LEFT JOIN " + cb + " AS CB ON CB.\"OID\" = OP.\"OID_CUENTA_BANCARIA\"" +
                     " LEFT JOIN " + cb + " AS CB2 ON CB2.\"OID\" = CB.\"OID_ASOCIADA\"";

            return query;
        }

        internal static string JOIN_ACREEDOR(ETipoAcreedor tipo)
        {
            string pv = nHManager.Instance.GetSQLTable(typeof(SupplierRecord));
			string na = nHManager.Instance.GetSQLTable(typeof(ShippingCompanyRecord));
            string de = nHManager.Instance.GetSQLTable(typeof(CustomAgentRecord));
            string tr = nHManager.Instance.GetSQLTable(typeof(TransporterRecord));
			string em = nHManager.Instance.GetSQLTable(typeof(EmployeeRecord));

            string query = string.Empty;

            switch (tipo)
            {
                case ETipoAcreedor.Acreedor:
                case ETipoAcreedor.Proveedor:
                case ETipoAcreedor.Instructor:
                    query += 
					"	LEFT JOIN " + pv + " AS TL ON (TL.\"OID\" = OP.\"OID_AGENTE\"" +
                    "                                   AND OP.\"TIPO_AGENTE\" = " + (long)tipo + ")";
                    break;

                case ETipoAcreedor.Despachante:
                    query += 
					"	LEFT JOIN " + de + " AS TL ON (TL.\"OID\" = OP.\"OID_AGENTE\"" +
                    "                                   AND OP.\"TIPO_AGENTE\" = " + (long)tipo + ")";
                    break;

                case ETipoAcreedor.Naviera:
                    query += 
					"	LEFT JOIN " + na + " AS TL ON (TL.\"OID\" = OP.\"OID_AGENTE\"" +
					"                                   AND OP.\"TIPO_AGENTE\" = " + (long)tipo + ")";
                    break;

                case ETipoAcreedor.TransportistaDestino:
                case ETipoAcreedor.TransportistaOrigen:
                    query += " INNER JOIN " + tr + " AS TL ON (TL.\"OID\" = OP.\"OID_AGENTE\"" +
                             "                                   AND OP.\"TIPO_AGENTE\" = " + (long)tipo + ")";
                    break;

				case ETipoAcreedor.Empleado:
					query += 
					"	LEFT JOIN " + em + " AS TL ON (TL.\"OID\" = OP.\"OID_AGENTE\"" +
					"                                   AND OP.\"TIPO_AGENTE\" = " + (long)tipo + ")";
					break;
            }

            return query;
        }

        internal static string JOIN_CUENTA(EBankLineType bankLineType)
        {
			string mv = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));
            string tr = nHManager.Instance.GetSQLTable(typeof(BankTransferRecord));
            string cr = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string pg = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));
            string gt = nHManager.Instance.GetSQLTable(typeof(ExpenseRecord));
            string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
            string pv = nHManager.Instance.GetSQLTable(typeof(SupplierRecord));
            string lr = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
			string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));
            string ef = nHManager.Instance.GetSQLTable(typeof(FinancialCashRecord));

            string query = string.Empty;

            switch (bankLineType)
            {
                case EBankLineType.Cobro:

                    query += " INNER JOIN " + cr + " AS OP ON OP.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)bankLineType;

                    break;

                case EBankLineType.CobroEfecto:
                    {
                        string tipos = "(" + (long)EBankLineType.CobroEfecto + ", " +
                                        (long)EBankLineType.PagoGastosEfecto + ", " +
                                        (long)EBankLineType.PagoGastosAdelantoEfecto + ", " +
                                        (long)EBankLineType.PagoGastosDevolucionEfecto + ")";

                        query += " INNER JOIN " + ef + " AS OP ON OP.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" IN " + tipos;

                    }
                    break;

                case EBankLineType.PagoFactura:

                    query += " INNER JOIN " + pg + " AS OP ON OP.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)bankLineType;

                    break;

                case EBankLineType.PagoGasto:
                case EBankLineType.PagoNomina:
                case EBankLineType.PagoPrestamo:

                    ETipoPago tipo_pago = Library.Store.EnumConvert.ToETipoPago(bankLineType);

                    query += " INNER JOIN " + pg + " AS OP ON OP.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)bankLineType;

                    break;

                case EBankLineType.SalidaCaja:

                    query += " INNER JOIN (SELECT *, " + (long)EMedioPago.Ingreso + " AS \"MEDIO_PAGO\"" +
                             "              FROM " + lc + ")" +
                             "      AS OP ON OP.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)bankLineType;

                    break;

				case EBankLineType.EntradaCaja:

					query += " INNER JOIN (SELECT *, " + (long)EMedioPago.Cheque + " AS \"MEDIO_PAGO\"" +
							 "              FROM " + lc + ")" +
                             "      AS OP ON OP.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)bankLineType;

					break;

                case EBankLineType.ExtractoTarjeta:

					string tc = nHManager.Instance.GetSQLTable(typeof(CreditCardRecord));

                    query += " INNER JOIN " + tc + " AS OP ON OP.\"OID\" = MV.\"OID_OPERACION\" AND MV.\"TIPO_OPERACION\" = " + (long)bankLineType;

                    break;
            }

            switch (bankLineType)
            {
                case EBankLineType.Manual:
                case EBankLineType.Interes:

                    query += " INNER JOIN " + cb + " AS CB ON CB.\"OID\" = MV.\"OID_CUENTA\"" +
                             " LEFT JOIN " + cb + " AS CB2 ON CB2.\"OID\" = CB.\"OID_ASOCIADA\"";

                    break;

                default:

                    query += " INNER JOIN " + cb + " AS CB ON CB.\"OID\" = OP.\"OID_CUENTA_BANCARIA\"" +
                             " LEFT JOIN " + cb + " AS CB2 ON CB2.\"OID\" = CB.\"OID_ASOCIADA\"";

                    break;
            }

            return query;
        }

        internal static string WHERE(long oid)
        {
            string query = string.Empty;

            if (oid > 0) query += " WHERE MV.\"OID\" = " + oid;

            return query;
        }
        internal static string WHERE(Library.Invoice.QueryConditions conditions, EBankLineType bankLineType)
        {
            return WHERE(conditions, bankLineType, ETipoAcreedor.Todos);
        }
        internal static string WHERE(Library.Invoice.QueryConditions conditions, EBankLineType bankLineType, ETipoAcreedor tipo_acreedor)
        {
            string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string pg = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));
            string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));

            string query = string.Empty;

            query += " WHERE MV.\"FECHA_OPERACION\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

            query += EntityBase.NO_NULL_RECORDS_CONDITION("MV");

            query += EntityBase.ESTADO_CONDITION(conditions.Estado, "MV");

            if (bankLineType != EBankLineType.CobroEfecto)
                query += " AND MV.\"TIPO_OPERACION\" = " + (long)bankLineType;
            else
            {
                string tipos = "(" + (long)EBankLineType.CobroEfecto + ", " +
                                (long)EBankLineType.PagoGastosEfecto + ", " +
                                (long)EBankLineType.PagoGastosAdelantoEfecto + ", " +
                                (long)EBankLineType.PagoGastosDevolucionEfecto + ")";

                query += " AND MV.\"TIPO_OPERACION\" IN " + tipos;
            }

            if (tipo_acreedor != ETipoAcreedor.Todos) query += " AND OP.\"TIPO_AGENTE\" = " + (long)tipo_acreedor;
            if (conditions.IMovimientoBanco != null)
            {
                if (conditions.IMovimientoBanco.Oid != 0)
                    query += " AND MV.\"OID_OPERACION\" = " + conditions.IMovimientoBanco.Oid;

                if (conditions.IMovimientoBanco.ETipoMovimientoBanco != EBankLineType.Todos)
                {
                    if (conditions.IMovimientoBanco.ETipoMovimientoBanco != EBankLineType.CobroEfecto)
                        query += " AND MV.\"TIPO_OPERACION\" = " + (long)conditions.IMovimientoBanco.ETipoMovimientoBanco;
                    else
                    {
                        string tipos = "(" + (long)EBankLineType.CobroEfecto + ", " +
                                        (long)EBankLineType.PagoGastosEfecto + ", " +
                                        (long)EBankLineType.PagoGastosAdelantoEfecto + ", " +
                                        (long)EBankLineType.PagoGastosDevolucionEfecto + ")";

                        query += " AND MV.\"TIPO_OPERACION\" IN " + tipos;
                    }
                }
            }

            if (conditions.Titular != null) query += " AND AG.\"OID\" = " + conditions.Titular;
            if (conditions.MedioPago != EMedioPago.Todos
                && bankLineType != EBankLineType.Prestamo
                && bankLineType != EBankLineType.Manual) query += " AND OP.\"MEDIO_PAGO\" = " + (long)conditions.MedioPago;
            
            if (conditions.TarjetaCredito != null) 
                query += @"
                AND TL.""OID"" = " + conditions.TarjetaCredito.Oid;
            
            if (conditions.TipoMovimientoBanco == EBankLineType.Prestamo) query += " AND MV.\"TIPO_CUENTA\" = " + (long)conditions.TipoCuenta;
            if (conditions.TipoMovimientoBanco == EBankLineType.ExtractoTarjeta)
            {
                if (conditions.Pago != null)
                    query += " AND MV.\"IMPORTE\" = " + -(conditions.Pago.Importe + conditions.Pago.GastosBancarios);
            }

            if (conditions.CuentaBancaria != null)
            {
                switch (bankLineType)
                {
                    case EBankLineType.Prestamo:
                    case EBankLineType.PagoPrestamo:
                        query += " AND CB.\"OID\" = " + conditions.CuentaBancaria.Oid;
                        break;
                    case EBankLineType.ComisionEstudioApertura:
                        query += " AND CB2.\"OID\" = " + conditions.CuentaBancaria.Oid;
                        break;
                    default:
                        query += " AND ((MV.\"TIPO_CUENTA\" = 1 AND CB.\"OID\" = " + conditions.CuentaBancaria.Oid + ") OR (MV.\"TIPO_CUENTA\" = 2 AND CB2.\"OID\" = " + conditions.CuentaBancaria.Oid + "))";
                        break;
                }
            }

            if (conditions.Oid > 0) query += " AND MV.\"OID\" = " + conditions.Oid;

            //if (conditions.Pago != null)
            //    query += " AND P.\"OID\" = " + conditions.Pago.Oid;

            return query;
        }

		internal static string SELECT_BASE(EBankLineType bankLineType, ETipoTitular tipoTitular)
        {
			string mv = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));

            string query = string.Empty;

            switch (bankLineType)
            {
                case EBankLineType.Manual:
                case EBankLineType.Interes:
                    query += FIELDS_MANUAL();
                    break;

				case EBankLineType.EntradaCaja:
                case EBankLineType.SalidaCaja:
                    query += FIELDS_CAJA();
                    break;

                case EBankLineType.Traspaso:
                case EBankLineType.CancelacionComercioExterior:
                    query += FIELDS_TRASPASO();
                    break;
                case EBankLineType.Prestamo:
                    query += FIELDS_PRESTAMO();
                    break;

                case EBankLineType.PagoPrestamo:
                    query += FIELDS_PAGO_PRESTAMO();
                    break;

                case EBankLineType.PagoGasto:
                    query += FIELDS_GASTOS(bankLineType, tipoTitular);
                    break;

                case EBankLineType.ComisionEstudioApertura:
                    query += FIELDS_COMISIONES();
                    break;

                case EBankLineType.CobroEfecto:
                    query += FIELDS_EFECTO(tipoTitular);
                    break;

				default:
					query += FIELDS(tipoTitular);
                    break;
            }

            query += @"
			FROM " + mv + @" AS MV
            LEFT JOIN " + us + @" AS US ON US.""OID"" = MV.""OID_USUARIO""";

            return query;
        }

        internal static string SELECT_RESUMEN_CUENTA_BASE()
        {
			string mv = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));

            string query = string.Empty;

            query = FIELDS_RESUMEN_CUENTA();
            query += " FROM " + mv + " AS MV";

            return query;
        }

		internal static string SELECT(long oid, EBankLineType bankLineType, ETipoTitular tipoTitular, bool lockTable)
        {
			string mv = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string pg = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));
            string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));

            string query = string.Empty;

            switch (bankLineType)
            {
                case EBankLineType.ExtractoTarjeta:

                    query = SELECT_BASE(EBankLineType.ExtractoTarjeta, ETipoTitular.Varios) +
                            JOIN(EBankLineType.ExtractoTarjeta) +
                            WHERE(oid);

					query += EntityBase.LOCK("MV", lockTable);

                    break;

                case EBankLineType.Manual:

                    query = SELECT_BASE(EBankLineType.Manual, ETipoTitular.Varios) +
                            JOIN(EBankLineType.Manual) +
                            WHERE(oid);

					query += EntityBase.LOCK("MV", lockTable);

                    break;

                case EBankLineType.Interes:

                    query = SELECT_BASE(EBankLineType.Interes, ETipoTitular.Varios) +
                            JOIN(EBankLineType.Interes) +
                            WHERE(oid);

                    query += EntityBase.LOCK("MV", lockTable);

                    break;

                case EBankLineType.Cobro:

					if (tipoTitular == ETipoTitular.Cliente)
                    {
                        //COBROS POR BANCO CLIENTES
						query = SELECT_BASE(EBankLineType.Cobro, tipoTitular) +
                                JOIN(EBankLineType.Cobro, ETipoCobro.Cliente) +
                                WHERE(oid);
                    }
					else if (tipoTitular == ETipoTitular.REA)
                    {
                        //COBROS POR BANCO REA
						query += SELECT_BASE(EBankLineType.Cobro, tipoTitular) +
                                JOIN(EBankLineType.Cobro, ETipoCobro.REA) +
                                WHERE(oid);
                    }

					query += EntityBase.LOCK("MV", lockTable);

                    break;

                case EBankLineType.CobroEfecto:
                case EBankLineType.PagoGastosEfecto:
                case EBankLineType.PagoGastosAdelantoEfecto:
                case EBankLineType.PagoGastosDevolucionEfecto:

                    //COBROS POR BANCO DE EFECTOS
                    query = SELECT_BASE(EBankLineType.CobroEfecto, tipoTitular) +
                            JOIN(EBankLineType.CobroEfecto, ETipoCobro.Cliente) +
                            WHERE(oid);

                    break;

                case EBankLineType.PagoFactura:

					switch (tipoTitular)
                    {
						//PAGOS POR BANCO ACREEDORES
						case ETipoTitular.Acreedor:

							query = SELECT_BASE(EBankLineType.PagoFactura, tipoTitular) +
									JOIN(EBankLineType.PagoFactura) +
									JOIN_ACREEDOR(ETipoAcreedor.Acreedor) +
									WHERE(oid);

							query += EntityBase.LOCK("MV", lockTable);

							break;

                        //PAGOS POR BANCO PROVEEDORES
                        case ETipoTitular.Proveedor:

							query = SELECT_BASE(EBankLineType.PagoFactura, tipoTitular) +
                                    JOIN(EBankLineType.PagoFactura) +
                                    JOIN_ACREEDOR(ETipoAcreedor.Proveedor) +
                                    WHERE(oid);

							query += EntityBase.LOCK("MV", lockTable);

                            break;

                        //PAGOS POR BANCO DESPACHANTES
                        case ETipoTitular.Despachante:

							query = SELECT_BASE(EBankLineType.PagoFactura, tipoTitular) +
                                    JOIN(EBankLineType.PagoFactura) +
                                    JOIN_ACREEDOR(ETipoAcreedor.Despachante) +
                                    WHERE(oid);

							query += EntityBase.LOCK("MV", lockTable);

                            break;

                        //PAGOS POR BANCO NAVIERAS
                        case ETipoTitular.Naviera:

							query = SELECT_BASE(EBankLineType.PagoFactura, tipoTitular) +
                                    JOIN(EBankLineType.PagoFactura) +
                                    JOIN_ACREEDOR(ETipoAcreedor.Naviera) +
                                    WHERE(oid);

							query += EntityBase.LOCK("MV", lockTable);

                            break;

                        //PAGOS POR BANCO TRANSPORTISTAS ORIGEN
                        case ETipoTitular.TransportistaOrigen:
							query = SELECT_BASE(EBankLineType.PagoFactura, tipoTitular) +
                                    JOIN(EBankLineType.PagoFactura) +
                                    JOIN_ACREEDOR(ETipoAcreedor.TransportistaOrigen) +
                                    WHERE(oid);
                            break;

						//PAGOS POR BANCO TRANSPORTISTAS DESTINO
						case ETipoTitular.TransportistaDestino:
							query = SELECT_BASE(EBankLineType.PagoFactura, tipoTitular) +
									JOIN(EBankLineType.PagoFactura) +
									JOIN_ACREEDOR(ETipoAcreedor.TransportistaDestino) +
									WHERE(oid);
							break;

                    }
                    break;

                //PAGOS DE NOMINAS
                case EBankLineType.PagoNomina:

                    query = SELECT_BASE(EBankLineType.PagoNomina, ETipoTitular.Empleado) +
                            JOIN(EBankLineType.PagoNomina) +
                            JOIN_ACREEDOR(ETipoAcreedor.Empleado) +
                            WHERE(oid);

					query += EntityBase.LOCK("MV", lockTable);

                    break;

                //PAGOS DE GASTOS
                case EBankLineType.PagoGasto:

                    query = SELECT_BASE(EBankLineType.PagoGasto, ETipoTitular.Varios) +
                            JOIN(EBankLineType.PagoGasto) +
                            JOIN_ACREEDOR(ETipoAcreedor.Todos) +
                            WHERE(oid);

					query += EntityBase.LOCK("MV", lockTable);

                    break;

                //SALIDAS DE CAJA
                case EBankLineType.SalidaCaja:

                    query = SELECT_BASE(EBankLineType.SalidaCaja, ETipoTitular.Todos) +
                            JOIN(EBankLineType.SalidaCaja) +
                            WHERE(oid);

					query += EntityBase.LOCK("MV", lockTable);

                    break;

				//ENTRADAS DE CAJA
				case EBankLineType.EntradaCaja:

					query = SELECT_BASE(EBankLineType.EntradaCaja, ETipoTitular.Todos) +
							JOIN(EBankLineType.EntradaCaja) +
							WHERE(oid);

					query += EntityBase.LOCK("MV", lockTable);

					break;

                //TRASPASOS
                case EBankLineType.Traspaso:

                    query = SELECT_BASE(EBankLineType.Traspaso, ETipoTitular.Todos) +
                            JOIN(EBankLineType.Traspaso) +
                            WHERE(oid);

					query += EntityBase.LOCK("MV", lockTable);

                    break;

                //PRÉSTAMO
                case EBankLineType.Prestamo:

                    query = SELECT_BASE(EBankLineType.Prestamo, ETipoTitular.Todos) +
                            JOIN(EBankLineType.Prestamo) +
                            WHERE(oid);

					query += EntityBase.LOCK("MV", lockTable);
                    break;
                //PAGO DE PRÉSTAMO
                case EBankLineType.PagoPrestamo:

                    query = SELECT_BASE(EBankLineType.PagoPrestamo, ETipoTitular.Todos) +
                            JOIN(EBankLineType.PagoPrestamo) +
                            WHERE(oid);

					query += EntityBase.LOCK("MV", lockTable);
                    break;

                //CANCELACIÓN DE COMERCIO EXTERIOR
                case EBankLineType.CancelacionComercioExterior:

                    query = SELECT_BASE(EBankLineType.CancelacionComercioExterior, ETipoTitular.Todos) +
                            JOIN(EBankLineType.CancelacionComercioExterior) +
                            WHERE(oid);

					query += EntityBase.LOCK("MV", lockTable);

                    break;

                case EBankLineType.ComisionEstudioApertura:

                    query = SELECT_BASE(EBankLineType.ComisionEstudioApertura, ETipoTitular.Todos) +
                            JOIN(EBankLineType.ComisionEstudioApertura) +
                            WHERE(oid);

					query += EntityBase.LOCK("MV", lockTable);

                    break;
            }

            //if (lock_table) query += " FOR UPDATE OF MV NOWAIT";

            return query;
        }

        internal static string SELECT(Library.Invoice.QueryConditions conditions, bool lockTable)
        {
            string query = string.Empty;

            query = SELECT_BUILDER(local_caller_SELECT, conditions);

            if (query != string.Empty)
                query += @"
                ORDER BY ""FECHA_OPERACION"" DESC, ""CODIGO"" DESC";

			//query += EntityBase.LOCK("MV", lockTable);

            return query;
        }

        internal static string SELECT_BY_CREDIT_CARD(Library.Invoice.QueryConditions conditions, bool lockTable)
        {
            string query = @"
            SELECT MIN(""OID"") AS ""OID""
	                ,""OID_OPERACION""
	                ,""TIPO_OPERACION""
	                ,MIN(""SERIAL"") AS ""SERIAL""
	                ,MIN(""CODIGO"") AS ""CODIGO""
	                ,MAX(""OID_USUARIO"") AS ""OID_USUARIO""
	                ,FALSE AS ""AUDITADO""
	                ,'' AS ""OBSERVACIONES""
	                ,""ESTADO""
	                ,'' AS ""ID_OPERACION""
	                ,""TIPO_CUENTA""
	                ,MIN(""FECHA_CREACION"") AS ""FECHA_CREACION""
	                ,""TIPO_MOVIMIENTO""
	                ,'' AS ""AUDITOR""
	                ,""OID_CUENTA_EX""
	                ,""CUENTA_BANCARIA""
	                ,""ENTIDAD""
	                ,""MEDIO_PAGO""
	                ,""TITULAR""
	                ,""OID_TITULAR""
	                ,""TIPO_TITULAR""
	                ,'' AS ""ID_MOV_CONTABLE""
	                ,""FECHA_OPERACION""
	                ,""OID_CUENTA""
	                ,SUM(""IMPORTE"") AS ""IMPORTE""
	        FROM (" + SELECT(conditions, false) + @" ) AS MV 
            GROUP BY ""OID_CUENTA"", ""FECHA_OPERACION"", ""OID_OPERACION"", ""TIPO_OPERACION""
                    ,""ESTADO"",""TIPO_CUENTA"", ""TIPO_MOVIMIENTO"", ""OID_CUENTA_EX"", ""CUENTA_BANCARIA""
                    ,""ENTIDAD"",""MEDIO_PAGO"", ""OID_TITULAR"", ""TIPO_TITULAR"", ""TITULAR""
            ORDER BY ""FECHA_OPERACION"" DESC, ""CODIGO"" DESC";

            return query;
        }

        internal static string SELECT_BUILDER(SelectLocalCaller local_SELECT, Library.Invoice.QueryConditions conditions)
        {
            string query = string.Empty;

            switch (conditions.TipoTitular)
            {
                case ETipoTitular.Cliente:

                    //COBROS POR BANCO CLIENTES
                    query = local_SELECT(EBankLineType.Cobro, ETipoTitular.Cliente) +
                            JOIN(EBankLineType.Cobro, ETipoCobro.Cliente) +
                            WHERE(conditions, EBankLineType.Cobro);

                    //COBROS POR BANCO EFECTOS
                    query += " UNION " +
                            local_SELECT(EBankLineType.CobroEfecto, ETipoTitular.Cliente) +
                            JOIN(EBankLineType.CobroEfecto, ETipoCobro.Cliente) +
                            WHERE(conditions, EBankLineType.CobroEfecto);

                    //COBROS POR BANCO REA
                    query += " UNION " +
                            local_SELECT(EBankLineType.Cobro, ETipoTitular.REA) +
                            JOIN(EBankLineType.Cobro, ETipoCobro.REA) +
                            WHERE(conditions, EBankLineType.Cobro);

                    //COBROS POR BANCO FOMENTO
                    query += " UNION " +
                            local_SELECT(EBankLineType.Cobro, ETipoTitular.Fomento) +
                            JOIN(EBankLineType.Cobro, ETipoCobro.Fomento) +
                            WHERE(conditions, EBankLineType.Cobro);
                    break;

                case ETipoTitular.REA:

                    //COBROS POR BANCO REA
                    query += local_SELECT(EBankLineType.Cobro, ETipoTitular.REA) +
                            JOIN(EBankLineType.Cobro, ETipoCobro.REA) +
                            WHERE(conditions, EBankLineType.Cobro);
                    break;

                case ETipoTitular.Proveedor:

                    //PAGOS POR BANCO PROVEEDORES
                    query = local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Proveedor) +
                            JOIN(EBankLineType.PagoFactura) +
                            JOIN_ACREEDOR(ETipoAcreedor.Proveedor) +
                            WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Proveedor);
                    break;

                case ETipoTitular.Acreedor:

                    //PAGOS POR BANCO ACREEDORES
                    query = local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Acreedor) +
                            JOIN(EBankLineType.PagoFactura) +
                            JOIN_ACREEDOR(ETipoAcreedor.Acreedor) +
                            WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Acreedor);
                    break;

                case ETipoTitular.Despachante:

                    //PAGOS POR BANCO DESPACHANTES
                    query = local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Despachante) +
                            JOIN(EBankLineType.PagoFactura) +
                            JOIN_ACREEDOR(ETipoAcreedor.Despachante) +
                            WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Despachante);
                    break;

                case ETipoTitular.Naviera:

                    //PAGOS POR BANCO NAVIERAS
                    query = local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Naviera) +
                            JOIN(EBankLineType.PagoFactura) +
                            JOIN_ACREEDOR(ETipoAcreedor.Naviera) +
                            WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Naviera);
                    break;

                case ETipoTitular.TransportistaOrigen:

                    //PAGOS POR BANCO TRANSPORTISTA ORIGEN
					query = local_SELECT(EBankLineType.PagoFactura, ETipoTitular.TransportistaOrigen) +
                            JOIN(EBankLineType.PagoFactura) +
                            JOIN_ACREEDOR(ETipoAcreedor.TransportistaOrigen) +
                            WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.TransportistaOrigen);
					break;

				case ETipoTitular.TransportistaDestino:

					//PAGOS POR BANCO TRANSPORTISTA DESTINO
					query = local_SELECT(EBankLineType.PagoFactura, ETipoTitular.TransportistaDestino) +
							JOIN(EBankLineType.PagoFactura) +
							JOIN_ACREEDOR(ETipoAcreedor.TransportistaDestino) +
							WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.TransportistaDestino);
                    break;

                //PAGOS POR BANCO DE NOMINAS
                case ETipoTitular.Empleado:

                    query = local_SELECT(EBankLineType.PagoNomina, ETipoTitular.Empleado) +
                            JOIN(EBankLineType.PagoNomina) +
                            WHERE(conditions, EBankLineType.PagoNomina, ETipoAcreedor.Empleado);

                    break;

                //MANUALES Y PAGOS POR BANCO DE GASTOS
                case ETipoTitular.Varios:

                    //MOVIMIENTOS MANUALES
                    query = local_SELECT(EBankLineType.Manual, ETipoTitular.Varios) +
                    JOIN(EBankLineType.Manual) +
                    WHERE(conditions, EBankLineType.Manual);

                    //INTERESES FONDOS DE INVERSIÓN
                    query = local_SELECT(EBankLineType.Interes, ETipoTitular.Varios) +
                    JOIN(EBankLineType.Interes) +
                    WHERE(conditions, EBankLineType.Interes);

                    //PAGOS POR BANCO DE GASTOS
                    query += " UNION " +
                            local_SELECT(EBankLineType.PagoGasto, ETipoTitular.Varios) +
                            JOIN(EBankLineType.PagoGasto) +
                            WHERE(conditions, EBankLineType.PagoGasto, ETipoAcreedor.Todos);

                    //EXTRACTOS DE TARJETA
                    query += " UNION " +
                            local_SELECT(EBankLineType.ExtractoTarjeta, ETipoTitular.Varios) +
                            JOIN(EBankLineType.ExtractoTarjeta) +
                            WHERE(conditions, EBankLineType.ExtractoTarjeta);
                    break;

                case ETipoTitular.Todos:

                    if ((conditions.IMovimientoBanco != null) || (conditions.TipoMovimientoBanco != EBankLineType.Todos))
                    {
						EBankLineType bankLineType = (conditions.TipoMovimientoBanco != EBankLineType.Todos) ? conditions.TipoMovimientoBanco : conditions.IMovimientoBanco.ETipoMovimientoBanco;

                        switch (bankLineType)
                        {
                            case EBankLineType.Manual:

                                //MOVIMIENTOS MANUALES
                                query = local_SELECT(EBankLineType.Manual, ETipoTitular.Cliente) +
                                JOIN(EBankLineType.Manual) +
                                WHERE(conditions, EBankLineType.Manual);

                                break;

                            case EBankLineType.Interes:

                                //INTERESES DE FONDOS DE INVERSIÓN
                                query = local_SELECT(EBankLineType.Interes, ETipoTitular.Cliente) +
                                JOIN(EBankLineType.Interes) +
                                WHERE(conditions, EBankLineType.Interes);

                                break;

                            case EBankLineType.Cobro:

                                //COBROS POR BANCO CLIENTES
                                query = local_SELECT(EBankLineType.Cobro, ETipoTitular.Cliente) +
                                        JOIN(EBankLineType.Cobro, ETipoCobro.Cliente) +
                                        WHERE(conditions, EBankLineType.Cobro);

                                //COBROS POR BANCO REA
                                query += " UNION " +
                                        local_SELECT(EBankLineType.Cobro, ETipoTitular.REA) +
                                        JOIN(EBankLineType.Cobro, ETipoCobro.REA) +
                                        WHERE(conditions, EBankLineType.Cobro);

                                //COBROS POR BANCO FOMENTO
                                query += " UNION " +
                                        local_SELECT(EBankLineType.Cobro, ETipoTitular.Fomento) +
                                        JOIN(EBankLineType.Cobro, ETipoCobro.Fomento) +
                                        WHERE(conditions, EBankLineType.Cobro);

                                break;

                            case EBankLineType.CobroEfecto:

                                //COBROS POR BANCO EFECTOS
                                query += local_SELECT(EBankLineType.CobroEfecto, ETipoTitular.Cliente) +
                                        JOIN(EBankLineType.CobroEfecto, ETipoCobro.Cliente) +
                                        WHERE(conditions, EBankLineType.CobroEfecto);
                                break;

                            case EBankLineType.PagoFactura:

                                //PAGOS POR BANCO PROVEEDORES
                                query = local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Proveedor) +
                                        JOIN(EBankLineType.PagoFactura) +
                                        JOIN_ACREEDOR(ETipoAcreedor.Proveedor) +
                                        WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Proveedor);

                                //PAGOS POR BANCO ACREEDORES
                                query += " UNION " +
                                        local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Acreedor) +
                                        JOIN(EBankLineType.PagoFactura) +
                                        JOIN_ACREEDOR(ETipoAcreedor.Acreedor) +
                                        WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Acreedor);

                                //PAGOS POR BANCO DESPACHANTES
                                query += " UNION " +
                                        local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Despachante) +
                                        JOIN(EBankLineType.PagoFactura) +
                                        JOIN_ACREEDOR(ETipoAcreedor.Despachante) +
                                        WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Despachante);

                                //PAGOS POR BANCO NAVIERAS
                                query += " UNION " +
                                        local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Naviera) +
                                        JOIN(EBankLineType.PagoFactura) +
                                        JOIN_ACREEDOR(ETipoAcreedor.Naviera) +
                                        WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Naviera);

                                //PAGOS POR BANCO TRANSPORTISTAS ORIGEN
                                query += " UNION " +
                                        local_SELECT(EBankLineType.PagoFactura, ETipoTitular.TransportistaOrigen) +
                                        JOIN(EBankLineType.PagoFactura) +
                                        JOIN_ACREEDOR(ETipoAcreedor.TransportistaOrigen) +
                                        WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.TransportistaOrigen);

                                //PAGOS POR BANCO TRANSPORTISTAS DESTINO
                                query += " UNION " +
                                        local_SELECT(EBankLineType.PagoFactura, ETipoTitular.TransportistaDestino) +
                                        JOIN(EBankLineType.PagoFactura) +
                                        JOIN_ACREEDOR(ETipoAcreedor.TransportistaDestino) +
                                        WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.TransportistaDestino);

                                break;

                            case EBankLineType.PagoGasto:

                                //PAGOS POR BANCO GASTOS
                                query = local_SELECT(EBankLineType.PagoGasto, ETipoTitular.Varios) +
                                        JOIN(EBankLineType.PagoGasto) +
                                        JOIN_ACREEDOR(ETipoAcreedor.Todos) +
                                        WHERE(conditions, EBankLineType.PagoGasto, ETipoAcreedor.Todos);

                                break;

                            case EBankLineType.PagoNomina:

                                //PAGOS POR BANCO NOMINAS
                                query = local_SELECT(EBankLineType.PagoNomina, ETipoTitular.Empleado) +
                                        JOIN(EBankLineType.PagoNomina) +
                                        JOIN_ACREEDOR(ETipoAcreedor.Empleado) +
                                        WHERE(conditions, EBankLineType.PagoNomina, ETipoAcreedor.Empleado);

                                break;

							case EBankLineType.EntradaCaja:

								//ENTRADAS DE CAJA
								query = local_SELECT(EBankLineType.EntradaCaja, ETipoTitular.Todos) +
										JOIN(EBankLineType.EntradaCaja) +
										WHERE(conditions, EBankLineType.EntradaCaja);
								break;

                            case EBankLineType.SalidaCaja:

                                //SALIDAS DE CAJA
                                query = local_SELECT(EBankLineType.SalidaCaja, ETipoTitular.Todos) +
                                        JOIN(EBankLineType.SalidaCaja) +
                                        WHERE(conditions, EBankLineType.SalidaCaja);
                                break;

                            case EBankLineType.Traspaso:

                                //TRASPASO
                                query = local_SELECT(EBankLineType.Traspaso, ETipoTitular.Todos) +
                                        JOIN_TRASPASO(conditions, EBankLineType.Traspaso) +
                                        WHERE(conditions, EBankLineType.Traspaso);

                                break;

                            case EBankLineType.CancelacionComercioExterior:

                                //CANCELACIÓN DE COMERCIO EXTERIOR
                                query = local_SELECT(EBankLineType.CancelacionComercioExterior, ETipoTitular.Todos) +
                                        JOIN_TRASPASO(conditions, EBankLineType.CancelacionComercioExterior) +
                                        WHERE(conditions, EBankLineType.CancelacionComercioExterior);

                                break;

                            case EBankLineType.ExtractoTarjeta:

                                //EXTRACTOS DE TARJETA
                                query = local_SELECT(EBankLineType.ExtractoTarjeta, ETipoTitular.Todos) +
                                JOIN(EBankLineType.ExtractoTarjeta) +
                                WHERE(conditions, EBankLineType.ExtractoTarjeta);

                                break;

                            case EBankLineType.Prestamo:

                                //PRÉSTAMO
                                query = local_SELECT(EBankLineType.Prestamo, ETipoTitular.Todos) +
                                        JOIN_PRESTAMO(conditions) +
                                        WHERE(conditions, EBankLineType.Prestamo);
                                break;

                            case EBankLineType.PagoPrestamo:

                                //PAGOS POR BANCO GASTOS
                                query = local_SELECT(EBankLineType.PagoPrestamo, ETipoTitular.Varios) +
                                        JOIN(EBankLineType.PagoPrestamo) +
                                        WHERE(conditions, EBankLineType.PagoPrestamo, ETipoAcreedor.Todos);

                                break;

                            case EBankLineType.ComisionEstudioApertura:

                                //COMISIONES DE ESTUDIO Y APERTURA (CUENTAS DE COMERCIO EXTERIOR)
                                query = local_SELECT(EBankLineType.ComisionEstudioApertura, ETipoTitular.Todos) +
                                    JOIN(EBankLineType.ComisionEstudioApertura) +
                                    WHERE(conditions, EBankLineType.ComisionEstudioApertura, ETipoAcreedor.Todos);
                                break;
                        }
                    }
                    else
                    {
                        //MOVIMIENTOS MANUALES
                        query = local_SELECT(EBankLineType.Manual, ETipoTitular.Varios) +
                                JOIN(EBankLineType.Manual) +
                                WHERE(conditions, EBankLineType.Manual);

                        //INTERESES DE FONDOS DE INVERSIÓN
                        query += " UNION " +
                                local_SELECT(EBankLineType.Interes, ETipoTitular.Varios) +
                                JOIN(EBankLineType.Interes) +
                                WHERE(conditions, EBankLineType.Interes);

                        //COBROS POR BANCO CLIENTES
                        query += " UNION " + local_SELECT(EBankLineType.Cobro, ETipoTitular.Cliente) +
                                JOIN(EBankLineType.Cobro, ETipoCobro.Cliente) +
                                WHERE(conditions, EBankLineType.Cobro);

                        //COBROS POR BANCO EFECTOS
                        query += " UNION " + local_SELECT(EBankLineType.CobroEfecto, ETipoTitular.Cliente) +
                                JOIN(EBankLineType.CobroEfecto, ETipoCobro.Cliente) +
                                WHERE(conditions, EBankLineType.CobroEfecto);

                        //COBROS POR BANCO REA
                        query += " UNION " +
                                local_SELECT(EBankLineType.Cobro, ETipoTitular.REA) +
                                JOIN(EBankLineType.Cobro, ETipoCobro.REA) +
                                WHERE(conditions, EBankLineType.Cobro);

                        //COBROS POR BANCO FOMENTO
                        query += " UNION " +
                                local_SELECT(EBankLineType.Cobro, ETipoTitular.Fomento) +
                                JOIN(EBankLineType.Cobro, ETipoCobro.Fomento) +
                                WHERE(conditions, EBankLineType.Cobro);

                        //PAGOS POR BANCO PROVEEDORES
                        query += " UNION " +
                                local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Proveedor) +
                                JOIN(EBankLineType.PagoFactura) +
                                JOIN_ACREEDOR(ETipoAcreedor.Proveedor) +
                                WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Proveedor);

                        //PAGOS POR BANCO ACREEDORES
                        query += " UNION " +
                                local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Acreedor) +
                                JOIN(EBankLineType.PagoFactura) +
                                JOIN_ACREEDOR(ETipoAcreedor.Acreedor) +
                                WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Acreedor);

                        //PAGOS POR BANCO INSTRUCTORES
                        query += " UNION " +
                                local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Instructor) +
                                JOIN(EBankLineType.PagoFactura) +
                                JOIN_ACREEDOR(ETipoAcreedor.Instructor) +
                                WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Instructor);

                        //PAGOS POR BANCO DESPACHANTES
                        query += " UNION " +
                                local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Despachante) +
                                JOIN(EBankLineType.PagoFactura) +
                                JOIN_ACREEDOR(ETipoAcreedor.Despachante) +
                                WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Despachante);

                        //PAGOS POR BANCO NAVIERAS
                        query += " UNION " +
                                local_SELECT(EBankLineType.PagoFactura, ETipoTitular.Naviera) +
                                JOIN(EBankLineType.PagoFactura) +
                                JOIN_ACREEDOR(ETipoAcreedor.Naviera) +
                                WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.Naviera);

                        //PAGOS POR BANCO TRANSPORTISTAS ORIGEN
                        query += " UNION " +
                                local_SELECT(EBankLineType.PagoFactura, ETipoTitular.TransportistaOrigen) +
                                JOIN(EBankLineType.PagoFactura) +
                                JOIN_ACREEDOR(ETipoAcreedor.TransportistaOrigen) +
                                WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.TransportistaOrigen);

                        //PAGOS POR BANCO TRANSPORTISTAS DESTINO
                        query += " UNION " +
                                local_SELECT(EBankLineType.PagoFactura, ETipoTitular.TransportistaDestino) +
                                JOIN(EBankLineType.PagoFactura) +
                                JOIN_ACREEDOR(ETipoAcreedor.TransportistaDestino) +
                                WHERE(conditions, EBankLineType.PagoFactura, ETipoAcreedor.TransportistaDestino);

                        //PAGOS POR BANCO GASTOS
                        query += " UNION " +
                                local_SELECT(EBankLineType.PagoGasto, ETipoTitular.Varios) +
                                JOIN(EBankLineType.PagoGasto) +
                                JOIN_ACREEDOR(ETipoAcreedor.Todos) +
                                WHERE(conditions, EBankLineType.PagoGasto, ETipoAcreedor.Todos);

                        //PAGOS POR BANCO NOMINAS
                        query += " UNION " +
                                local_SELECT(EBankLineType.PagoNomina, ETipoTitular.Empleado) +
                                JOIN(EBankLineType.PagoNomina) +
                                JOIN_ACREEDOR(ETipoAcreedor.Empleado) +
                                WHERE(conditions, EBankLineType.PagoNomina, ETipoAcreedor.Empleado);

                        //SALIDAS DE CAJA
                        query += " UNION " +
                                local_SELECT(EBankLineType.SalidaCaja, ETipoTitular.Todos) +
                                JOIN(EBankLineType.SalidaCaja) +
                                WHERE(conditions, EBankLineType.SalidaCaja);

						//ENTRADAS DE CAJA
						query += " UNION " +
								local_SELECT(EBankLineType.EntradaCaja, ETipoTitular.Todos) +
								JOIN(EBankLineType.EntradaCaja) +
								WHERE(conditions, EBankLineType.EntradaCaja);

						//EXTRACTOS DE TARJETA
						query += " UNION " +
								local_SELECT(EBankLineType.ExtractoTarjeta, ETipoTitular.Todos) +
								JOIN(EBankLineType.ExtractoTarjeta) +
								WHERE(conditions, EBankLineType.ExtractoTarjeta);

                        //TRASPASO
                        Traspaso mov = Traspaso.New();
                        mov.Importe = -1;
                        conditions.IMovimientoBanco = mov.IGetInfo(false);
                        query += " UNION " +
                                local_SELECT(EBankLineType.Traspaso, ETipoTitular.Todos) +
                                JOIN_TRASPASO(conditions, EBankLineType.Traspaso) +
                                WHERE(conditions, EBankLineType.Traspaso);

                        mov.Importe = 1;
                        conditions.IMovimientoBanco = mov.IGetInfo(false);
                        query += " UNION " +
                                local_SELECT(EBankLineType.Traspaso, ETipoTitular.Todos) +
                                JOIN_TRASPASO(conditions, EBankLineType.Traspaso) +
                                WHERE(conditions, EBankLineType.Traspaso);

                        conditions.IMovimientoBanco = null;

                        //CANCELACIÓN DE COMERCIO EXTERIOR
                        mov = Traspaso.New();
                        mov.ETipoMovimientoBanco = EBankLineType.CancelacionComercioExterior;
                        mov.Importe = -1;
                        conditions.IMovimientoBanco = mov.IGetInfo(false);
                        query += " UNION " +
                                local_SELECT(EBankLineType.CancelacionComercioExterior, ETipoTitular.Todos) +
                                JOIN_TRASPASO(conditions, EBankLineType.CancelacionComercioExterior) +
                                WHERE(conditions, EBankLineType.CancelacionComercioExterior);

                        mov.Importe = 1;
                        conditions.IMovimientoBanco = mov.IGetInfo(false);
                        query += " UNION " +
                                local_SELECT(EBankLineType.CancelacionComercioExterior, ETipoTitular.Todos) +
                                JOIN_TRASPASO(conditions, EBankLineType.CancelacionComercioExterior) +
                                WHERE(conditions, EBankLineType.CancelacionComercioExterior);

                        conditions.IMovimientoBanco = null;

                        //PRÉSTAMOS
                        conditions.TipoCuenta = ECuentaBancaria.Principal;
                        conditions.TipoMovimientoBanco = EBankLineType.Prestamo;

                        query += " UNION " +
                                local_SELECT(EBankLineType.Prestamo, ETipoTitular.Todos) +
                                JOIN_PRESTAMO(conditions) +
                                WHERE(conditions, EBankLineType.Prestamo);

                        conditions.TipoCuenta = ECuentaBancaria.Asociada;
                        query += " UNION " +
                                local_SELECT(EBankLineType.Prestamo, ETipoTitular.Todos) +
                                JOIN_PRESTAMO(conditions) +
                                WHERE(conditions, EBankLineType.Prestamo);

                        conditions.TipoCuenta = ECuentaBancaria.Principal;

                        //PAGOS DE PRÉSTAMOS
                        query += " UNION " +
                                local_SELECT(EBankLineType.PagoPrestamo, ETipoTitular.Varios) +
                                JOIN(EBankLineType.PagoPrestamo) +
                                WHERE(conditions, EBankLineType.PagoPrestamo, ETipoAcreedor.Todos);

                        //COMISIONES DE ESTUDIO Y APERTURA (CUENTAS DE COMERCIO EXTERIOR)
                        conditions.IMovimientoBanco = null;

                        conditions.TipoCuenta = ECuentaBancaria.Principal;
                        conditions.TipoMovimientoBanco = EBankLineType.ComisionEstudioApertura;

                        query += " UNION " +
                                local_SELECT(EBankLineType.ComisionEstudioApertura, ETipoTitular.Todos) +
                                JOIN(EBankLineType.ComisionEstudioApertura) +
                                WHERE(conditions, EBankLineType.ComisionEstudioApertura, ETipoAcreedor.Todos);
                             
                        break;
                    }
                    break;
            }

            return query;
        }

        internal static string SELECT_RESUMEN_CUENTA_BUILDER(QueryConditions conditions)
        {
            string query = string.Empty;

            string query2 = " AND MV.\"ESTADO\" != " + (long)EEstado.Anulado +
                            " GROUP BY CB.\"OID\"";

            //MANUALES
            query = SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_CUENTA(EBankLineType.Manual) +
                    WHERE(conditions, EBankLineType.Manual) +
                    query2;

            //INTERESES DE FONDOS DE INVERSIÓN
            query += " UNION " +
                    SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_CUENTA(EBankLineType.Interes) +
                    WHERE(conditions, EBankLineType.Interes) +
                    query2;

            //COBROS POR BANCO
            query += " UNION " + SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_CUENTA(EBankLineType.Cobro) +
                    WHERE(conditions, EBankLineType.Cobro) +
                    query2;

            //PAGOS POR BANCO FACTURAS
            query += " UNION " +
                    SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_CUENTA(EBankLineType.PagoFactura) +
                    WHERE(conditions, EBankLineType.PagoFactura) +
                    query2;

            //PAGOS POR BANCO GASTOS
            query += " UNION " +
                    SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_CUENTA(EBankLineType.PagoGasto) +
                    WHERE(conditions, EBankLineType.PagoGasto) +
                    query2;

            //PAGOS POR BANCO NOMINAS
            query += " UNION " +
                    SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_CUENTA(EBankLineType.PagoNomina) +
                    WHERE(conditions, EBankLineType.PagoNomina) +
                    query2;

            //SALIDAS DE CAJA
            query += " UNION " +
                    SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_CUENTA(EBankLineType.SalidaCaja) +
                    WHERE(conditions, EBankLineType.SalidaCaja) +
                    query2;

			//ENTRADAS DE CAJA
			query += " UNION " +
					SELECT_RESUMEN_CUENTA_BASE() +
					JOIN_CUENTA(EBankLineType.EntradaCaja) +
					WHERE(conditions, EBankLineType.EntradaCaja) +
					query2;

			//EXTRACTOS DE TARJETA
			query += " UNION " +
					SELECT_RESUMEN_CUENTA_BASE() +
					JOIN_CUENTA(EBankLineType.ExtractoTarjeta) +
					WHERE(conditions, EBankLineType.ExtractoTarjeta) +
					query2;

            //TRASPASO
            Traspaso mov = Traspaso.New();
            mov.Importe = -1;
            conditions.IMovimientoBanco = mov.IGetInfo(false);
            query += " UNION " +
                    SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_TRASPASO(conditions, EBankLineType.Traspaso) +
                    WHERE(conditions, EBankLineType.Traspaso) +
                    query2;

            mov.Importe = 1;
            conditions.IMovimientoBanco = mov.IGetInfo(false);
            query += " UNION " +
                    SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_TRASPASO(conditions, EBankLineType.Traspaso) +
                    WHERE(conditions, EBankLineType.Traspaso) +
                    query2;

            //CANCELACIÓN DE COMERCIO EXTERIOR
            mov = Traspaso.New();
            mov.ETipoMovimientoBanco = EBankLineType.CancelacionComercioExterior;
            mov.Importe = -1;
            conditions.IMovimientoBanco = mov.IGetInfo(false);
            query += " UNION " +
                    SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_TRASPASO(conditions, EBankLineType.CancelacionComercioExterior) +
                    WHERE(conditions, EBankLineType.CancelacionComercioExterior) +
                    query2;

            mov.Importe = 1;
            conditions.IMovimientoBanco = mov.IGetInfo(false);
            query += " UNION " +
                    SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_TRASPASO(conditions, EBankLineType.CancelacionComercioExterior) +
                    WHERE(conditions, EBankLineType.CancelacionComercioExterior) +
                    query2;

            //PRÉSTAMOS
            conditions.TipoCuenta = ECuentaBancaria.Principal;
            conditions.TipoMovimientoBanco = EBankLineType.Prestamo;
            conditions.IMovimientoBanco = null;
            query += " UNION " +
                    SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_PRESTAMO(conditions) +
                    WHERE(conditions, EBankLineType.Prestamo) +
                    query2;

            conditions.TipoCuenta = ECuentaBancaria.Asociada;
            query += " UNION " +
                    SELECT_RESUMEN_CUENTA_BASE() +
                    JOIN_PRESTAMO(conditions) +
                    WHERE(conditions, EBankLineType.Prestamo) +
                    query2;

            return query;
        }

        internal static string SELECT_BY_PAGO_TARJETA(QueryConditions conditions)
        {
            string pg = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));

            string query = SELECT_BASE(EBankLineType.ExtractoTarjeta, ETipoTitular.Todos) +
                            JOIN(EBankLineType.ExtractoTarjeta) +
                            " INNER JOIN " + pg + " AS P ON P.\"OID_TARJETA_CREDITO\" = MV.\"OID_OPERACION\" AND P.\"VENCIMIENTO\" = CAST(MV.\"FECHA_OPERACION\" AS DATE)" +
                            WHERE(conditions, EBankLineType.ExtractoTarjeta);

            return query;
        }

        #endregion
    }
    
    #region Strategies (Strategy Pattern)

    public static class GeneralBankLine
    {
        public static void Annul(IBankLineInfo source, int sessionCode)
        {
            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
            conditions.Estado = EEstado.Abierto;

            BankLines lines = BankLines.GetList(conditions, false, sessionCode);

            foreach (BankLine mv in lines)
            {
                if (mv != null && mv.Oid != 0)
                {
                    mv.EEstado = EEstado.Anulado;
                    mv.Observaciones = String.Format(Library.Invoice.Resources.Labels.ANULADO, DateTime.Now.ToString(), AppContext.User.Name, source.Titular, source.CuentaBancaria) + Environment.NewLine
                                        + mv.Observaciones;
                }
            }

            lines.SaveAsChild();
        }

        public static BankLine Insert(IBankLineInfo source, int sessionCode)
        {
            BankLine line = BankLine.New();

            BankLines lines = BankLines.NewList();
            lines.SessionCode = sessionCode;

            line = lines.NewItem(source);

            lines.SaveAsChild();

            return line;
        }
    }

    public static class ChargeBankLine
    {
        public static void Annul(IBankLineInfo source, int sessionCode)
        {
            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
            conditions.Estado = EEstado.Abierto;

            BankLines lines = BankLines.GetList(conditions, false, sessionCode);

            switch (source.EMedioPago)
            {
                case EMedioPago.Pagare:
                case EMedioPago.Cheque:
                    {
                        FinancialCash financial_cash = FinancialCash.GetByCobro((ChargeInfo)source, false, sessionCode);

                        financial_cash.EEstado = EEstado.Anulado;
                        financial_cash.Observaciones = String.Format(Library.Invoice.Resources.Labels.ANULADO, DateTime.Now.ToString(), AppContext.User.Name, source.Titular, source.CuentaBancaria) + Environment.NewLine
                                                     + financial_cash.Observaciones;

                        financial_cash.Save();
                    }
                    break;

                default:
                    {
                        foreach (BankLine line in lines)
                        {
                            if (line != null && line.Oid != 0)
                            {
                                line.EEstado = EEstado.Anulado;
                                line.Observaciones = String.Format(Library.Invoice.Resources.Labels.ANULADO, DateTime.Now.ToString(), AppContext.User.Name, source.Titular, source.CuentaBancaria) + Environment.NewLine
                                                    + line.Observaciones;
                            }
                        }
                    }
                    break;
            }

            lines.SaveAsChild();
        }

        public static void Edit(IBankLineInfo source, IBankLineInfo oldSource, int sessionCode)
        {
            switch (source.EMedioPago)
            {
                case EMedioPago.Pagare:
                case EMedioPago.Cheque:
                    {
                        if (oldSource.EMedioPago != EMedioPago.Cheque &&
                            oldSource.EMedioPago != EMedioPago.Pagare)
                        {
                            Annul(oldSource, sessionCode);
                            Insert(source, sessionCode);
                            return;
                        }

                        //Esto no va aqui. Hay que pasarlo al cobro
                        FinancialCash financial_cash = FinancialCash.GetByCobro((ChargeInfo)source, false, sessionCode);

                        if (financial_cash != null)
                        {
                            financial_cash.CopyFrom((ChargeInfo)source);
                            financial_cash.Save();
                        }
                        else
                            Insert(source, sessionCode);
                    }
                    break;

                default:
                    {
                        if (oldSource.EMedioPago == EMedioPago.Cheque ||
                            oldSource.EMedioPago == EMedioPago.Pagare)
                        {
                            Annul(oldSource, sessionCode);
                            Insert(source, sessionCode);
                            return;
                        }

                        Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
                        conditions.Estado = EEstado.Abierto;
                        conditions.TipoTitular = source.ETipoTitular;

                        BankLines lines = BankLines.GetList(conditions, false, sessionCode);
    
                        if (lines != null && lines.Count > 0)
                        {
                            if (((ChargeInfo)source).EEstadoCobro != EEstado.Charged)
                            {
                                Annul(source, sessionCode);
                                return;
                            }
                            //No hay movimiento con los gastos
                            if (lines.Count == 1)
                            {
                                lines[0].CopyFrom(source);

                                if (source.GastosBancarios != 0)
                                {
                                    BankLine line = lines.NewItem(source);

                                    line.Importe = -source.GastosBancarios;
                                    line.ETipo = ETipoApunteBancario.Multiple;
                                    line.FechaOperacion = source.Fecha.AddSeconds(1);
                                }

                                lines.SaveAsChild();
                            }
                            else
                            {
                                //Movimiento de Entrada
                                lines[1].CopyFrom(source);

                                source.Importe = -source.GastosBancarios;

                                //Movimiento de Salida

                                lines[0].CopyFrom(source);
                                lines[0].FechaOperacion = lines[0].FechaOperacion.AddSeconds(1);
                                lines.SaveAsChild();
                            }
                        }
                        else
                        {
                            Insert(source, sessionCode);
                            return;
                        }
                    } break;
            }
        }

        public static BankLine Insert(IBankLineInfo source, int sessionCode)
        {
            if (new EMedioPago[] { EMedioPago.Pagare, EMedioPago.Cheque }.Contains(source.EMedioPago)) return BankLine.New();

            BankLines lines = BankLines.NewList();
            lines.SessionCode = sessionCode;

             BankLine line = lines.NewItem(source);

            if (source.GastosBancarios != 0)
            {
                line.ETipo = ETipoApunteBancario.Multiple;
                line = lines.NewItem(source);
                line.Importe = -source.GastosBancarios;
                line.ETipo = ETipoApunteBancario.Multiple;
                line.FechaOperacion = source.Fecha.AddSeconds(1);
            }

            lines.SaveAsChild();

            return line;
        }       
    }

    public static class MerchantBankLine
    {
        static Library.Invoice.QueryConditions conditions;
        static Loans loans;

        public static void Annul(IBankLineInfo source, int sessionCode)
        {
            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
            conditions.Estado = EEstado.Abierto;
            conditions.Pago = (PaymentInfo)source;
            conditions.LoanType = ELoanType.Merchant;

            BankLines lines = BankLines.GetList(conditions, false, sessionCode);

            foreach (BankLine item in lines)
            {
                item.EEstado = EEstado.Anulado;
                item.Observaciones = String.Format(Library.Invoice.Resources.Labels.ANULADO, DateTime.Now.ToString(), AppContext.User.Name, source.Titular, source.CuentaBancaria) + Environment.NewLine
                                    + item.Observaciones;
            }

            lines.SaveAsChild();

            Loans loans = Loans.GetList(conditions, sessionCode);

            if (loans != null && loans.Count > 0)
            {
                Loan pr = loans[0];
                PaymentList payments = PaymentList.GetListByPrestamo(pr.GetInfo(), false);
                
                if (payments.Count > 0)
                    throw new iQException(Resources.Messages.LOAN_WITH_PAYMENTS);

                loans.RemoveAll();
                loans.SaveAsChild();
            }
        }

        public static void Edit(IBankLineInfo source, IBankLineInfo oldSource, int sessionCode)
        {       
            conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
            conditions.Estado = EEstado.Abierto;
            conditions.TipoTitular = source.ETipoTitular;
            conditions.Pago = (PaymentInfo)source;
            conditions.LoanType = ELoanType.Merchant;

            loans = Loans.GetList(conditions, sessionCode);

            if (loans != null && loans.Count > 0)
            {
                Loan loan = loans[0];
                PaymentList payments = PaymentList.GetListByPrestamo(loan.GetInfo(), false);

                if (payments.Count > 0)
                    throw new iQException(Resources.Messages.LOAN_WITH_PAYMENTS);

                if (((PaymentInfo)source).EEstadoPago != EEstado.Pagado)
                {
                    Annul(source, sessionCode);
                    loan.EEstado = EEstado.Anulado;
                    loans.SaveAsChild();
                    return;
                }

                loan.CopyFrom((PaymentInfo)source);
                loans.SaveAsChild();

                BankLines lines = BankLines.GetList(conditions, false, sessionCode);

                //Movimiento de Salida de la cuenta principal
                BankLine line = lines[0];
                line.CopyFrom(source);
                line.ETipoCuenta = ECuentaBancaria.Asociada;
                line.FechaOperacion = line.FechaOperacion.AddSeconds(2);
                lines.SaveAsChild();
            }
            else
                //Es un pago de factura con comercio exterior antiguo, en el que no se asociaba a un préstamo
                //Hay que buscar los 3 movimientos del tipo Pago de Factura
                EditOldPayment(source, oldSource, sessionCode);
        }

        private static void EditOldPayment(IBankLineInfo source, IBankLineInfo oldSource, int sessionCode)
        {
            if (loans != null && loans.Count > 0) return;

            if (((PaymentInfo)source).EEstadoPago != EEstado.Pagado)
            {
                Annul(source, sessionCode);
                return;
            }

            BankLines lines = BankLines.GetList(conditions, false, sessionCode);

            if (lines != null && lines.Count > 0)
            {
                foreach (BankLine line in lines)
                {
                    switch (line.ETipoCuenta)
                    {
                        //Movimiento de salida de la cuenta de comercio exterior
                        case ECuentaBancaria.Principal:
                            {
                                line.CopyFrom(source);
                                line.ETipoCuenta = ECuentaBancaria.Principal;
                                line.FechaOperacion = line.FechaOperacion;
                                line.Importe = -source.Importe;
                            }
                            break;

                        case ECuentaBancaria.Asociada:
                            {
                                //Movimiento de entrada en la cuenta principal
                                if (line.Importe > 0)
                                {
                                    line.CopyFrom(source);
                                    line.ETipoCuenta = ECuentaBancaria.Asociada;
                                    line.FechaOperacion = line.FechaOperacion.AddSeconds(1);
                                    line.Importe = source.Importe;
                                }
                                //Movimiento de salida de la cuenta principal
                                else
                                {
                                    line.CopyFrom(source);
                                    line.ETipoCuenta = ECuentaBancaria.Asociada;
                                    line.FechaOperacion = line.FechaOperacion.AddSeconds(2);
                                }
                            }
                            break;
                    }
                }

                lines.SaveAsChild();
            }
            else
            {
                Insert(source, sessionCode);
                return;
            }
        }

        public static BankLine Insert(IBankLineInfo source, int sessionCode)
        {
            //Movimiento de Salida de la cuenta asociada
            BankLines lines = BankLines.NewList();
            lines.SessionCode = sessionCode;

            BankLine line = lines.NewItem(source);
            line.ETipoCuenta = ECuentaBancaria.Asociada;
            line.ETipo = ETipoApunteBancario.Mixto;
            line.FechaOperacion = line.FechaOperacion.AddSeconds(3);
            lines.SaveAsChild();

            return line;
        }
    }

    public static class FinancialCashChargeBankLine
    {
        public static void Annul(IBankLineInfo source, int sessionCode)
        {
            GeneralBankLine.Annul(source, sessionCode);
        }

        public static BankLine Insert(IBankLineInfo source, int sessionCode)
        {
            BankLine line = BankLine.New();
            FinancialCashInfo financial_cash = (FinancialCashInfo)source;
            
            BankLines lines = BankLines.NewList();            
            lines.SessionCode = sessionCode;

            if (financial_cash.Importe != 0)
            {
                line = lines.NewItem(source);
                line.ETipo = ETipoApunteBancario.Multiple;
            }

            if (financial_cash.Gastos != 0)
            {
                line = lines.NewItem(source);
                line.ETipo = ETipoApunteBancario.Multiple;

                line.Importe = -financial_cash.Gastos;
                line.ETipoMovimientoBanco = EBankLineType.PagoGastosEfecto;
                line.FechaOperacion = line.FechaOperacion.AddSeconds(1);
            }

            if (financial_cash.GastosAdelanto != 0 && financial_cash.Adelantado)
            {
                line = lines.NewItem(source);
                line.ETipo = ETipoApunteBancario.Multiple;

                line.Importe = -financial_cash.GastosAdelanto;
                line.ETipoMovimientoBanco = EBankLineType.PagoGastosAdelantoEfecto;
                line.FechaOperacion = line.FechaOperacion.AddSeconds(2);
            }

            if (financial_cash.EEstadoCobro == EEstado.Devuelto)
            {
                line = lines.NewItem(source);
                line.ETipo = ETipoApunteBancario.Multiple;

                line.Importe = -(financial_cash.GastosDevolucion + financial_cash.Importe);
                line.ETipoMovimientoBanco = EBankLineType.PagoGastosDevolucionEfecto;
            }

            lines.SaveAsChild();

            return line;
        }

        public static void Edit(IBankLineInfo source, IBankLineInfo oldSource, int sessionCode)
        {
            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
            conditions.Estado = EEstado.Abierto;
            conditions.TipoTitular = source.ETipoTitular;

            BankLines lines = BankLines.GetList(conditions, false, sessionCode);
            FinancialCashInfo financial_cash = (FinancialCashInfo)source;

            if (lines != null && lines.Count > 0)
            {
                if (((FinancialCashInfo)source).EEstadoCobro == EEstado.Pendiente)
                {
                    Annul(source, sessionCode);
                    return;
                }

                //Se modifican los movimientos que ya existían
                foreach (BankLine line in lines)
                {
                    if (line.EEstado == EEstado.Anulado) continue;

                    switch (line.ETipoMovimientoBanco)
                    {
                        case EBankLineType.CobroEfecto:
                            {
                                line.CopyFrom(source);
                                line.ETipo = ETipoApunteBancario.Multiple;
                            }
                            break;

                        case EBankLineType.PagoGastosEfecto:
                            {
                                line.CopyFrom(source);
                                line.ETipo = ETipoApunteBancario.Multiple;

                                line.Importe = -financial_cash.Gastos;
                                line.ETipoMovimientoBanco = EBankLineType.PagoGastosEfecto;
                                line.FechaOperacion = line.FechaOperacion.AddSeconds(1);
                            }
                            break;

                        case EBankLineType.PagoGastosAdelantoEfecto:
                            {
                                line.CopyFrom(source);
                                line.ETipo = ETipoApunteBancario.Multiple;

                                line.Importe = -financial_cash.GastosAdelanto;
                                line.ETipoMovimientoBanco = EBankLineType.PagoGastosAdelantoEfecto;
                                line.FechaOperacion = line.FechaOperacion.AddSeconds(2);

                                if (!financial_cash.Adelantado)
                                    line.EEstado = EEstado.Anulado;
                            }
                            break;

                        case EBankLineType.PagoGastosDevolucionEfecto:
                            {
                                line.CopyFrom(source);
                                line.ETipo = ETipoApunteBancario.Multiple;

                                line.Importe = -(financial_cash.GastosDevolucion + financial_cash.Importe);
                                line.ETipoMovimientoBanco = EBankLineType.PagoGastosDevolucionEfecto;
                                line.FechaOperacion = line.FechaOperacion.AddSeconds(3);

                                if (financial_cash.EEstadoCobro != EEstado.Devuelto)
                                    line.EEstado = EEstado.Anulado;
                            }
                            break;
                    }
                }

                //Ahora se comprueba que cada gasto tenga su movimiento,
                //si no fuera así, se generan los movimientos que falten
                if (financial_cash.Importe != 0)
                {
                    if (!lines.Contains(EBankLineType.CobroEfecto))
                    {
                        BankLine line = lines.NewItem(source);
                        line.ETipo = ETipoApunteBancario.Multiple;
                        line.Serial = lines[0].Serial;
                        line.Codigo = lines[0].Codigo;
                    }
                }
                if (financial_cash.Gastos != 0)
                {
                    if (!lines.Contains(EBankLineType.PagoGastosEfecto))
                    {
                        BankLine line = lines.NewItem(source);
                        line.ETipo = ETipoApunteBancario.Multiple;
                        line.Serial = lines[0].Serial;
                        line.Codigo = lines[0].Codigo;

                        line.Importe = -financial_cash.Gastos;
                        line.ETipoMovimientoBanco = EBankLineType.PagoGastosEfecto;
                        line.FechaOperacion = line.FechaOperacion.AddSeconds(1);
                    }
                }
                if (financial_cash.GastosAdelanto != 0 && financial_cash.Adelantado)
                {
                    if (!lines.Contains(EBankLineType.PagoGastosAdelantoEfecto))
                    {
                        BankLine line = lines.NewItem(source);
                        line.ETipo = ETipoApunteBancario.Multiple;
                        line.Serial = lines[0].Serial;
                        line.Codigo = lines[0].Codigo;

                        line.Importe = -financial_cash.GastosAdelanto;
                        line.ETipoMovimientoBanco = EBankLineType.PagoGastosAdelantoEfecto;
                        line.FechaOperacion = line.FechaOperacion.AddSeconds(2);
                    }
                }
                if (financial_cash.EEstadoCobro == EEstado.Devuelto)
                {
                    if (!lines.Contains(EBankLineType.PagoGastosDevolucionEfecto))
                    {
                        BankLine line = lines.NewItem(source);
                        line.ETipo = ETipoApunteBancario.Multiple;
                        line.Serial = lines[0].Serial;
                        line.Codigo = lines[0].Codigo;

                        line.Importe = -(financial_cash.GastosDevolucion + financial_cash.Importe);
                        line.ETipoMovimientoBanco = EBankLineType.PagoGastosDevolucionEfecto;
                        line.FechaOperacion = line.FechaOperacion.AddSeconds(3);
                    }
                }

                lines.SaveAsChild();
            }
            else
            {
                Insert(source, sessionCode);
                return;
            }
        }
    }

    public static class LoanBankLine
    {
        public static void Edit(IBankLineInfo source, IBankLineInfo oldSource, int sessionCode)
        {
            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
            conditions.Estado = EEstado.Abierto;

            BankLines lines = BankLines.GetList(conditions, false, sessionCode);
            LoanInfo loan = source as LoanInfo;

            if (lines == null || lines.Count == 0) return;

            if (lines.Count > 1)
            {
                //Préstamo de una cuenta de comercio exterior

                int index = 0;

                foreach (BankLine line in lines)
                {
                    if (line.ETipoCuenta == ECuentaBancaria.Principal)
                        line.CopyFrom(source);
                    else
                    {
                        BankAccountInfo cuenta = BankAccountInfo.Get(source.OidCuenta, false);

                        line.CopyFrom(source);
                        line.Importe = -(line.Importe + (loan.GastosInicio ? loan.GastosBancarios : 0));
                        line.ETipoCuenta = ECuentaBancaria.Asociada;
                        line.OidCuentaMov = cuenta.OidCuentaAsociada;
                    }

                    line.FechaOperacion = line.FechaOperacion.AddSeconds(index++);
                }

                lines.SaveAsChild();
            }
            else
            {
                //Préstamo de una cuenta corriente
                BankLine line = lines[0];
                line.CopyFrom(source);
                line.Importe = -(line.Importe + (loan.GastosInicio ? loan.GastosBancarios : 0));
            }

            lines.SaveAsChild();
        }

        public static BankLine Insert(IBankLineInfo source, int sessionCode)
        {
            BankLine line = BankLine.New();

            BankAccountInfo bank_account = BankAccountInfo.Get(source.OidCuenta, false);
            LoanInfo loan = (LoanInfo)source;

            switch (bank_account.ETipoCuenta)
            {
                case Common.ETipoCuenta.ComercioExterior:
                case Common.ETipoCuenta.LineaCredito:
                    {
                        //Movimiento de Salida de la cuenta de crédito
                        BankLines lines = BankLines.NewList();
                        lines.SessionCode = sessionCode;
                        line = lines.NewItem(source);
                        line.ETipoCuenta = ECuentaBancaria.Principal;
                        line.OidCuentaMov = bank_account.Oid;
                        line.ETipo = (loan.OidPago == 0) ? ETipoApunteBancario.Multiple : ETipoApunteBancario.Mixto;
                        lines.SaveAsChild();


                        //Movimiento de Entrada a la cuenta asociada
                        lines = BankLines.NewList();
                        lines.SessionCode = sessionCode;
                        line = lines.NewItem(source);
                        line.ETipoCuenta = ECuentaBancaria.Asociada;
                        line.Importe = -(line.Importe + (loan.GastosInicio ? loan.GastosBancarios : 0));
                        line.OidCuentaMov = bank_account.OidCuentaAsociada;
                        line.ETipo = (loan.OidPago == 0) ? ETipoApunteBancario.Multiple : ETipoApunteBancario.Mixto;
                        line.FechaOperacion = line.FechaOperacion.AddSeconds(1);
                        lines.SaveAsChild();
                    }
                    break;

                case Common.ETipoCuenta.CuentaCorriente:
                    {
                        //Movimiento de Salida de la cuenta de crédito
                        BankLines lines = BankLines.NewList();
                        lines.SessionCode = sessionCode;
                        line = lines.NewItem(source);
                        line.ETipoCuenta = ECuentaBancaria.Principal;
                        line.Importe = -(line.Importe + (loan.GastosInicio ? loan.GastosBancarios : 0));
                        line.OidCuentaMov = bank_account.Oid;
                        lines.SaveAsChild();
                    }
                    break;
            }

            return line;
        }
    }

    public static class LoanPaymentBankLine
    {
        public static void Annul(IBankLineInfo source, int sessionCode)
        {
            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
            conditions.Estado = EEstado.Abierto;

            BankLines lines = BankLines.GetList(conditions, false);

            foreach (BankLine line in lines)
            {
                line.EEstado = EEstado.Anulado;
                line.Observaciones = String.Format(Library.Invoice.Resources.Labels.ANULADO, DateTime.Now.ToString(), AppContext.User.Name, source.Titular, source.CuentaBancaria) + Environment.NewLine
                                    + line.Observaciones;
            }

            lines.Save();
            lines.CloseSession();
        }

        public static void Edit(IBankLineInfo source, IBankLineInfo oldSource, int sessionCode)
        {
            if (((PaymentInfo)source).EEstadoPago != EEstado.Pagado)
            {
                Annul(source, sessionCode);
                return;
            }

            PaymentInfo payment = (PaymentInfo)source;

            LoanInfo loan = LoanInfo.Get(payment.OidAgente);
            BankAccountInfo bank_account = BankAccountInfo.Get(loan.OidCuenta, false);

            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
            conditions.Estado = EEstado.Abierto;
            conditions.TipoTitular = source.ETipoTitular;

            BankLines lines = BankLines.GetList(conditions, false, sessionCode);

            if (lines != null && lines.Count > 0)
            {
                decimal importe = -(payment.Importe + payment.GastosBancarios);

                switch (source.EMedioPago)
                {
                    case EMedioPago.ComercioExterior:
                        {
                            conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
                            conditions.Estado = EEstado.Abierto;
                            conditions.TipoTitular = source.ETipoTitular;

                            lines = BankLines.GetList(conditions, false, sessionCode);

                            //Movimiento de Salida de la cuenta de crédito
                            BankLine line = lines[0];
                            line.CopyFrom(source);
                            line.Importe = importe;

                            //Movimiento de Entrada de la cuenta corriente
                            line = lines[1];
                            source.Importe = -importe;
                            line.CopyFrom(source);
                            line.FechaOperacion = line.FechaOperacion.AddSeconds(1);

                            //Movimiento de Salida de la cuenta de crédito
                            line = lines[2];
                            source.Importe = importe;
                            line.CopyFrom(source);
                            line.FechaOperacion = line.FechaOperacion.AddSeconds(2);

                            lines.SaveAsChild();
                        }
                        break;

                    default:
                        {
                            if (bank_account.ETipoCuenta != moleQule.Library.Common.ETipoCuenta.CuentaCorriente)
                            {
                                foreach (BankLine mv in lines)
                                {
                                    //Movimiento de salida de la cuenta corriente
                                    BankLine line = lines[0];
                                    line.CopyFrom(source);
                                    line.OidCuenta = source.OidCuenta;
                                    line.Importe = importe;

                                    //Movimiento de entrada a la cuenta de crédito
                                    line = lines[1];
                                    line.CopyFrom(source);
                                    line.ETipoCuenta = ECuentaBancaria.Principal;
                                    line.OidCuentaMov = bank_account.Oid;
                                    line.Importe = payment.Importe;
                                    line.FechaOperacion = line.FechaOperacion.AddSeconds(1);
                                }
                            }
                            else
                            {
                                foreach (BankLine mv in lines)
                                {
                                    mv.CopyFrom(source);
                                    mv.OidCuenta = source.OidCuenta;
                                    mv.Importe = importe;
                                }
                            }
                        }
                        break;
                }
                lines.SaveAsChild();
            }
            else
            {
                Insert(source, sessionCode);
                return;
            }
        }

        public static BankLine Insert(IBankLineInfo source, int sessionCode)
        {
            BankLine line = BankLine.New();
            PaymentInfo payment = (PaymentInfo)source;
            LoanInfo loan = LoanInfo.Get(payment.OidAgente);
            BankAccountInfo bank_account = BankAccountInfo.Get(loan.OidCuenta, false);
            
            BankLines lines = BankLines.NewList();
            lines.SessionCode = sessionCode;

            if (bank_account.ETipoCuenta != moleQule.Library.Common.ETipoCuenta.CuentaCorriente)
            {
                //Movimiento de Entrada a la cuenta de crédito
                line = lines.NewItem(source);
                line.ETipoCuenta = ECuentaBancaria.Principal;
                line.OidCuentaMov = bank_account.Oid;
                line.Importe = payment.Importe;
                line.ETipo = ETipoApunteBancario.Multiple;
            }

            if (Common.EnumFunctions.NeedsCuentaBancaria(source.EMedioPago))
            {
                decimal importe = -(payment.GastosBancarios + payment.Importe);

                switch (source.EMedioPago)
                {
                    //Se está pagando una cuota de un préstamo a partir de un comercio exterior
                    case EMedioPago.ComercioExterior:
                        {
                            //Movimiento de Salida de la cuenta de crédito
                            line = lines.NewItem(source);
                            line.Importe = importe;
                            line.ETipoCuenta = ECuentaBancaria.Principal;
                            line.ETipo = ETipoApunteBancario.Multiple;

                            DateTime operationDate = line.FechaOperacion;

                            //Movimiento de Entrada a la cuenta asociada
                            line = lines.NewItem(source);
                            line.ETipoCuenta = ECuentaBancaria.Asociada;
                            line.Importe = -importe;
                            line.FechaOperacion = operationDate.AddSeconds(1);
                            line.ETipo = ETipoApunteBancario.Multiple;

                            //Movimiento de Salida de la cuenta asociada
                            line = lines.NewItem(source);
                            line.Importe = importe;
                            line.ETipoCuenta = ECuentaBancaria.Asociada;
                            line.FechaOperacion = operationDate.AddSeconds(2);
                            line.ETipo = ETipoApunteBancario.Multiple;
                        }
                        break;

                    default:
                        {
                            //Salida de la cuenta corriente (pago)
                            line = lines.NewItem(source);
                            line.OidCuenta = source.OidCuenta;
                            line.Importe = -(payment.Importe + payment.GastosBancarios);
                            if (bank_account.ETipoCuenta != moleQule.Library.Common.ETipoCuenta.CuentaCorriente)
                                line.ETipo = ETipoApunteBancario.Multiple;
                        }
                        break;
                }
            }

            lines.SaveAsChild();

            return line;
        }
    }

    public static class PaymentBankLine
    {
        public static BankLine Insert(IBankLineInfo source, int sessionCode)
        {
            BankLine line = BankLine.New();
            PaymentInfo payment = (PaymentInfo)source;

            BankLines lines = BankLines.NewList();
            lines.SessionCode = sessionCode;

            line = lines.NewItem(source);
            line.Importe = -(payment.Importe + payment.GastosBancarios);

            lines.SaveAsChild();

            return line;
        }
    }

    public static class TransactionBankLine
    {
        public static void Annul(IBankLineInfo source, int sessionCode)
        {
            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
            conditions.Estado = EEstado.Abierto;
            conditions.IMovimientoBanco.Importe = 0;

            BankLines lines = BankLines.GetList(conditions, false, sessionCode);

            foreach (BankLine line in lines)
            {
                line.EEstado = EEstado.Anulado;
                line.Observaciones = String.Format(Library.Invoice.Resources.Labels.ANULADO, DateTime.Now.ToString(), AppContext.User.Name, source.Titular, source.CuentaBancaria) + Environment.NewLine
                                    + line.Observaciones;
            }

            lines.SaveAsChild();
        }

        public static void Edit(IBankLineInfo source, IBankLineInfo oldSource, int sessionCode)
        {
            decimal importe = source.Importe;
            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { IMovimientoBanco = source };
            conditions.Estado = EEstado.Abierto;
            conditions.TipoTitular = source.ETipoTitular;
            conditions.IMovimientoBanco.Importe = 0;

            BankLines lines = BankLines.GetList(conditions, false, sessionCode);
            source.Importe = importe;

            if (lines != null && lines.Count > 0)
            {
                //Movimiento de Entrada
                if (lines[0] != null && lines[0].Oid != 0)
                {
                    lines[0].CopyFrom(source);
                    lines[0].Importe = source.Importe;
                    lines[0].FechaOperacion = ((TraspasoInfo)source).FechaRecepcion.AddSeconds(1);
                }

                //Movimiento de Salida
                if (lines[1] != null && lines[1].Oid != 0)
                {
                    lines[1].CopyFrom(source);
                    lines[1].OidCuentaMov = lines[1].OidCuenta;
                }
                lines.SaveAsChild();
            }
            else
            {
                Insert(source, sessionCode);
                return;
            }
        }

        public static BankLine Insert(IBankLineInfo source, int sessionCode)
        {
            //Movimiento de Salida
            BankLines lines = BankLines.NewList();
            lines.SessionCode = sessionCode;

            BankLine line = lines.NewItem(source);
            line.ETipo = ETipoApunteBancario.Multiple;
            lines.SaveAsChild();

            //Movimiento de Entrada
            lines = BankLines.NewList();
            lines.SessionCode = sessionCode;

            line = lines.NewItem(source);
            line.ETipo = ETipoApunteBancario.Multiple;
            line.Importe = source.Importe;
            line.FechaOperacion = ((TraspasoInfo)source).FechaRecepcion.AddSeconds(1);
            lines.SaveAsChild();

            return line;
        }
    }

    #endregion
}