using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

using NHibernate;
using Csla;
using Csla.Validation;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class ChargeRecord : RecordBase
	{
		#region Attributes

		private long _oid_cliente;
		private int _oid_usuario;
		private long _id_cobro;
		private int _tipo_cobro;
		private DateTime _fecha;
		private Decimal _importe;
		private long _medio_pago;
		private DateTime _vencimiento;
		private string _observaciones = string.Empty;
		private long _oid_cuenta_bancaria;
		private long _serial;
		private string _codigo = string.Empty;
		private long _oid_tpv;
		private long _estado;
		private long _estado_cobro;
		private string _id_mov_contable = string.Empty;
		private Decimal _gastos_bancarios;

		#endregion

		#region Properties

		public virtual long OidCliente { get { return _oid_cliente; } set { _oid_cliente = value; } }
		public virtual int OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }
		public virtual long IdCobro { get { return _id_cobro; } set { _id_cobro = value; } }
		public virtual int TipoCobro { get { return _tipo_cobro; } set { _tipo_cobro = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual Decimal Importe { get { return _importe; } set { _importe = value; } }
		public virtual long MedioPago { get { return _medio_pago; } set { _medio_pago = value; } }
		public virtual DateTime Vencimiento { get { return _vencimiento; } set { _vencimiento = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual long OidCuentaBancaria { get { return _oid_cuenta_bancaria; } set { _oid_cuenta_bancaria = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long OidTpv { get { return _oid_tpv; } set { _oid_tpv = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual long EstadoCobro { get { return _estado_cobro; } set { _estado_cobro = value; } }
		public virtual string IdMovContable { get { return _id_mov_contable; } set { _id_mov_contable = value; } }
		public virtual Decimal GastosBancarios { get { return _gastos_bancarios; } set { _gastos_bancarios = value; } }

		#endregion

		#region Business Methods

		public ChargeRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
			_oid_usuario = Format.DataReader.GetInt32(source, "OID_USUARIO");
			_id_cobro = Format.DataReader.GetInt64(source, "ID_COBRO");
			_tipo_cobro = Format.DataReader.GetInt32(source, "TIPO_COBRO");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_importe = Format.DataReader.GetDecimal(source, "IMPORTE");
			_medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
			_vencimiento = Format.DataReader.GetDateTime(source, "VENCIMIENTO");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_oid_cuenta_bancaria = Format.DataReader.GetInt64(source, "OID_CUENTA_BANCARIA");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_oid_tpv = Format.DataReader.GetInt64(source, "OID_TPV");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_estado_cobro = Format.DataReader.GetInt64(source, "ESTADO_COBRO");
			_id_mov_contable = Format.DataReader.GetString(source, "ID_MOV_CONTABLE");
			_gastos_bancarios = Format.DataReader.GetDecimal(source, "GASTOS_BANCARIOS");
		}
		public virtual void CopyValues(ChargeRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_cliente = source.OidCliente;
			_oid_usuario = source.OidUsuario;
			_id_cobro = source.IdCobro;
			_tipo_cobro = source.TipoCobro;
			_fecha = source.Fecha;
			_importe = source.Importe;
			_medio_pago = source.MedioPago;
			_vencimiento = source.Vencimiento;
			_observaciones = source.Observaciones;
			_oid_cuenta_bancaria = source.OidCuentaBancaria;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_oid_tpv = source.OidTpv;
			_estado = source.Estado;
			_estado_cobro = source.EstadoCobro;
			_id_mov_contable = source.IdMovContable;
			_gastos_bancarios = source.GastosBancarios;
		}

		#endregion
	}

	[Serializable()]
	public class ChargeBase
	{
		#region Attributes

		private ChargeRecord _record = new ChargeRecord();

		private string _cuenta_bancaria = string.Empty;
		private string _entidad = string.Empty;
		private string _id_mov_banco = string.Empty;
		private string _tpv = string.Empty;
		private string _cliente = string.Empty;
		private string _id_cliente;
		private string _id_linea_caja;
		private string _id_mov_contable;
		private decimal _gastos_demora;
		private string _usuario = string.Empty;
        private decimal _pendiente;
        private string _id_efecto = string.Empty;

		#endregion

		#region Properties

		public ChargeRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } set { _record.Estado = (long)value; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
		public virtual EEstado EEstadoCobro { get { return (EEstado)_record.EstadoCobro; } set { _record.EstadoCobro = (long)value; } }
		public virtual string EstadoCobroLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstadoCobro); } }
		public virtual string IDCobroLabel { get { return ((NCliente != string.Empty) ? (NCliente + "/") : string.Empty) + _record.IdCobro.ToString(Resources.Defaults.COBRO_ID_FORMAT); } }
		public virtual ETipoCobro ETipoCobro { get { return (ETipoCobro)_record.TipoCobro; } set { _record.TipoCobro = (int)value; } }
		public virtual string ETipoCobroLabel { get { return EnumText<ETipoCobro>.GetLabel(ETipoCobro); } }
		public virtual EMedioPago EMedioPago { get { return (EMedioPago)_record.MedioPago; } set { _record.MedioPago = (long)value; } }
		public virtual string EMedioPagoLabel { get { return Common.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
		public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
		public virtual string Entidad { get { return _entidad; } set { _entidad = value; } }
		public virtual string TPV { get { return _tpv; } set { _tpv = value; } }
		public virtual string IDCliente { get { return _id_cliente; } set { _id_cliente = value; } } /*DEPRECATED*/
		public virtual string Cliente { get { return _cliente; } set { _cliente = value; } }
		public virtual string NumCliente { get { return _id_cliente; } set { _id_cliente = value; } } /*DEPRECATED*/
		public virtual string NCliente { get { return _id_cliente; } } /*DEPRECATED*/
		public virtual string IDLineaCaja { get { return (_id_linea_caja != null) ? String.Format(_id_linea_caja, Resources.Defaults.LINEACAJA_CODE_FORMAT) : string.Empty; } set { _id_linea_caja = value; } }
		public virtual string IDMovimientoBanco { get { return _id_mov_banco; } set { _id_mov_banco = value; } }
		public virtual string IDMovimientoContable { get { return _id_mov_contable; } set { _id_mov_contable = value; } }
		public virtual decimal GastosDemora { get { return _gastos_demora; } set { _gastos_demora = value; } }
		public virtual string Usuario { get { return _usuario; } set { _usuario = value; } }
        public virtual decimal Pendiente { get { return _pendiente; } set { _pendiente = value; } }
        public virtual string IDEfecto { get { return _id_efecto; } set { _id_efecto = value; } }

		#endregion

		#region Business Methods

		public void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_pendiente = Format.DataReader.GetDecimal(source, "PENDIENTE");
			_cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
			_entidad = Format.DataReader.GetString(source, "ENTIDAD");
			_tpv = Format.DataReader.GetString(source, "TPV");
			_id_cliente = Format.DataReader.GetString(source, "ID_CLIENTE");
			_cliente = Format.DataReader.GetString(source, "CLIENTE");
			_id_linea_caja = Format.DataReader.GetString(source, "ID_LINEA_CAJA");
			_id_mov_banco = Format.DataReader.GetString(source, "ID_MOVIMIENTO_BANCO");
			_usuario = Format.DataReader.GetString(source, "USUARIO");
            _id_mov_contable = Format.DataReader.GetString(source, "ID_MOVIMIENTO_CONTABLE");
            _id_efecto = Format.DataReader.GetString(source, "ID_EFECTO");

			_id_mov_contable = (_id_mov_contable == "/") ? string.Empty : _id_mov_contable;
		}
        public void CopyValues(Charge source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_id_mov_banco = source.IDMovimientoBanco;
			_id_linea_caja = source.IDLineaCaja;
			_id_mov_contable = source.IDMovimientoContable;
			_cuenta_bancaria = source.CuentaBancaria;
			_entidad = source.Entidad;
			_cliente = source.Cliente;
			_usuario = source.Usuario;
            _pendiente = (source.ETipoCobro == ETipoCobro.REA) ? source.PendienteREA : source.Pendiente;
            _id_efecto = source.IDEfecto;
		}
        public void CopyValues(ChargeInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_id_mov_banco = source.IDMovimientoBanco;
			_id_linea_caja = source.IDLineaCaja;
			_id_mov_contable = source.IDMovimientoContable;
			_cuenta_bancaria = source.CuentaBancaria;
			_entidad = source.Entidad;
			_id_cliente = source.IDCliente;
			_cliente = source.Cliente;
			_usuario = source.Usuario;
            _pendiente = (source.ETipoCobro == ETipoCobro.REA) ? source.PendienteREA : source.Pendiente;
            _id_efecto = source.IDEfecto;
			_tpv = source.TPV;
		}

		#endregion
	}

    /// <summary>
    /// Hijo con hijos
    /// Editable Child Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Charge : BusinessBaseEx<Charge>, IBankLine, IEntidadRegistro, IEntityBase
    {
        #region IEntityBase

        public virtual DateTime FechaReferencia { get { return Fecha; } }

        public virtual IEntityBase ICloneAsNew() { return CloneAsNew(); }
        public virtual void ICopyValues(IEntityBase source) { _base.CopyValues((Charge)source); }
        public void DifferentYearChecks() { }
        public void SameYearTasks(IEntityBase newItem)
        {
            Charge obj = (Charge)this;

            //El único cambio que se ha producido es que se ha exportado el cobro, no hay que 
            //realizar cambios en los movimientos
            if (newItem.EEstado == EEstado.Exportado &&
                EEstado != EEstado.Exportado)
                return;

            //Editamos o Anulamos la Linea de Caja o el Movimiento de Banco si los hubiera
            Cash.EditItem((Charge)newItem, obj.GetInfo(true), newItem.SessionCode);
            BankLine.EditItem((Charge)newItem, obj.GetInfo(true), newItem.SessionCode);
        }

        public void DifferentYearTasks(IEntityBase oldItem)
        {
            Cash.EditItem((Charge)this, ((Charge)oldItem).GetInfo(false), SessionCode);
            BankLine.EditItem((Charge)this, ((Charge)oldItem).GetInfo(false), SessionCode);
        }
        public virtual void IEntityBaseSave(object parent) 
        {
            if (parent != null)
            {
                if (parent.GetType() == typeof(Cliente))
                    Insert((Cliente)parent);
                else if (parent.GetType() == typeof(Charges))
                    Insert((Charges)parent);
                else
                    Save();
            }
            else
                Save();
        }

        #endregion

        #region IMovimientoBanco

        public virtual long TipoMovimiento { get { return (long)EBankLineType.Cobro; } }
        public virtual EBankLineType ETipoMovimientoBanco { get { return EBankLineType.Cobro; } }
		public virtual ETipoTitular ETipoTitular { get { return EnumConvert.ToETipoTitular(ETipoCobro); } }
		public virtual string CodigoTitular { get { return NCliente; } set {} }
        public virtual string Titular { get { return Cliente; } set {} }
        public virtual long OidCuenta { get { return 0; } set { } }
        public virtual bool Confirmado { get { return EEstadoCobro == EEstado.Charged; } set { } }

		public virtual IBankLineInfo IGetInfo(bool childs) { return (IBankLineInfo)GetInfo(childs); }

        #endregion

		#region IEntidadRegistro

		public virtual ETipoEntidad ETipoEntidad { get { return ETipoEntidad.Cobro; } }
		public string DescripcionRegistro { get { return "COBRO Nº " + IDCobroLabel + " de " + Fecha.ToShortDateString() + " de " + Importe.ToString("C2") + " de " + Cliente; } }

		public virtual IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }
		public virtual IEntidadRegistro IGet(long oid, bool childs) { return (IEntidadRegistro)Get(oid, childs); }

		public void Update(Registro parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
			ChargeRecord obj = Session().Get<ChargeRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			MarkOld();
		}

		#endregion

        #region Attributes

		protected ChargeBase _base = new ChargeBase();

        private CobroFacturas _cobro_facturas = CobroFacturas.NewChildList();
        private CobroREAs _cobro_reas = CobroREAs.NewChildList();

        #endregion

		#region Properties

		public ChargeBase Base { get { return _base; } }

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
		public virtual int OidUsuario
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
		public virtual long IdCobro
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.IdCobro;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.IdCobro.Equals(value))
				{
					_base.Record.IdCobro = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual int TipoCobro
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoCobro;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.TipoCobro.Equals(value))
				{
					_base.Record.TipoCobro = value;
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
		public virtual DateTime Vencimiento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Vencimiento;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Vencimiento.Equals(value))
				{
					_base.Record.Vencimiento = value;
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
		public virtual long OidCuentaBancaria
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCuentaBancaria;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCuentaBancaria.Equals(value))
				{
					_base.Record.OidCuentaBancaria = value;
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
		public virtual long OidTPV
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidTpv;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidTpv.Equals(value))
				{
					_base.Record.OidTpv = value;
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
		public virtual long EstadoCobro
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.EstadoCobro;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.EstadoCobro.Equals(value))
				{
					_base.Record.EstadoCobro = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string IdMovContable
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.IdMovContable;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.IdMovContable.Equals(value))
				{
					_base.Record.IdMovContable = value;
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

        public virtual CobroFacturas CobroFacturas
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _cobro_facturas;
            }

            set
            {
                _cobro_facturas = value;
            }
        }
		public virtual CobroREAs CobroREAs
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _cobro_reas;
			}

			set
			{
				_cobro_reas = value;
			}
		}

		public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; } }
        public virtual string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
        public virtual EEstado EEstadoCobro { get { return _base.EEstadoCobro; } set { EstadoCobro = (long)value; } }
        public virtual string EstadoCobroLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstadoCobro); } }
        public virtual string IDCobroLabel { get { return _base.IDCobroLabel; } }
		public virtual ETipoCobro ETipoCobro { get { return (ETipoCobro)TipoCobro; } }
		public virtual string ETipoCobroLabel { get { return EnumText<ETipoCobro>.GetLabel(ETipoCobro); } }
        public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } set { MedioPago = (long)value; } }
		public virtual string EMedioPagoLabel { get { return Common.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
        public virtual decimal Pendiente { get { return (CobroFacturas == null) ? Importe : Importe - CobroFacturas.GetTotal(); } }
        public virtual decimal PendienteREA { get { return (CobroREAs == null) ? Importe : Importe - CobroREAs.GetTotal(); } }
		public virtual Decimal PendienteAsignacion { get { return Pendiente; } }
		public virtual string CuentaBancaria { get { return _base.CuentaBancaria; } set { _base.CuentaBancaria = value; } }
		public virtual string Entidad { get { return _base.Entidad; } set { _base.Entidad = value; } }
		public virtual string TPV { get { return _base.TPV; } set { _base.TPV = value; } }
		public virtual string IDCliente { get { return _base.IDCliente; } set { _base.IDCliente = value; } } /*DEPRECATED*/
		public virtual string Cliente { get { return _base.Cliente; } set { _base.Cliente = value; } }
		public virtual string NumCliente { get { return _base.IDCliente; } set { _base.IDCliente = value; } } /*DEPRECATED*/
		public virtual string NCliente { get { return _base.IDCliente; } } /*DEPRECATED*/
		public virtual string IDLineaCaja { get { return _base.IDLineaCaja; } }
		public virtual string IDMovimientoBanco { get { return _base.IDMovimientoBanco; } }
		public virtual string IDMovimientoContable { get { return _base.IDMovimientoContable; } }
		public virtual decimal GastosDemora { get { return _base.GastosDemora; } set { _base.GastosDemora = value; } }
        public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }
        public virtual string IDEfecto { get { return _base.IDEfecto; } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid
                    && _cobro_facturas.IsValid
                    && _cobro_reas.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                   || _cobro_facturas.IsDirty
                    || _cobro_reas.IsDirty;
            }
        }

        #endregion

        #region Business Methods

        public virtual Charge CloneAsNew()
        {
            Charge clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();
			clon.EEstado = EEstado.Abierto;
            clon.EEstadoCobro = EEstado.Pendiente;

            clon.SessionCode = Charge.OpenSession();
            Charge.BeginTransaction(clon.SessionCode);

            clon.MarkNew();
            clon.CobroFacturas.MarkAsNew();

            return clon;
        }

        public virtual void CopyFrom(ClienteInfo source)
        {
            if (source == null) return;

            OidCliente = source.Oid;
            IDCliente = source.Codigo;
            
			SetMedioPago(source.MedioPago);

			if (Library.Common.EnumFunctions.NeedsCuentaBancaria(EMedioPago))
			{
				if (EMedioPago != EMedioPago.Tarjeta)
				{
					OidCuentaBancaria = source.OidCuentaBAsociada;
					CuentaBancaria = source.CuentaAsociada;
				}
			}
        }

        public virtual void GetNewCode(long oidCliente)
        {
            Serial = SerialInfo.GetNextByYear(typeof(Charge), Fecha.Year);
            Codigo = Serial.ToString(Resources.Defaults.COBRO_CODE_FORMAT);

            switch (ETipoCobro)
            {
                case ETipoCobro.Cliente:
                    IdCobro = SerialCobroInfo.GetNext(oidCliente);
                    break;
                case ETipoCobro.REA:
                case ETipoCobro.Fomento:
                    IdCobro = SerialCobroInfo.GetNext(oidCliente, Fecha.Year);
                    break;
            }
        }

		public virtual void ChangeEstado(EEstado estado)
		{
			EntityBase.CheckChangeState(EEstado, estado);
			EEstado = estado;
		}
		public static Charge ChangeEstado(long oid, ETipoCobro tipo, EEstado estado)
		{
			if (!CanChangeState())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			Charge item = null;

			try
			{
				item = Charge.Get(oid, tipo, false);
                ChargeInfo oldItem = item.GetInfo(false);

                if ((item.EEstado == EEstado.Contabilizado || item.EEstado == EEstado.Exportado) && (!AutorizationRulesControler.CanEditObject(Resources.SecureItems.CUENTA_CONTABLE)))
					throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

				EntityBase.CheckChangeState(item.EEstado, estado);

				item.BeginEdit();
				item.EEstado = estado;

				if (estado == EEstado.Anulado)
				{
					Cash.EditItem(item, oldItem, item.SessionCode);
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

		public virtual void CalculaGastos(TPVInfo tpv)
		{
			GastosBancarios = Importe * tpv.PComision / 100;
		}

		public virtual void SetCobrado()
		{
			switch (EMedioPago)
			{
				case EMedioPago.Efectivo:
				case EMedioPago.Ingreso:
				case EMedioPago.Domiciliacion:
				case EMedioPago.Transferencia:
				case EMedioPago.Tarjeta:
					{
                        EEstadoCobro = (Vencimiento <= DateTime.Today) ? EEstado.Charged : EEstado.Pendiente;
					}
					break;

                default:
                    EEstadoCobro = EEstado.Charged;
                    break;
			}
		}
		public virtual void SetFechas(DateTime fecha, TPVInfo tpv)
		{
			Fecha = fecha;

			Vencimiento = (tpv != null)
							? Library.Common.EnumFunctions.GetPrevisionPago(EFormaPago.Contado, Fecha, 0)
							: Fecha;

			SetCobrado();
		}
		public virtual void SetMedioPago(long medioPago)
		{
			MedioPago = medioPago;

			switch (EMedioPago)
			{
				case EMedioPago.Ingreso:
				case EMedioPago.Domiciliacion:
				case EMedioPago.Transferencia:
				case EMedioPago.Tarjeta:
					{
                        EEstadoCobro = (Vencimiento <= DateTime.Today) ? EEstado.Charged : EEstado.Pendiente;
					}
					break;

                case EMedioPago.Cheque:
                case EMedioPago.Pagare:
				case EMedioPago.ComercioExterior:
                case EMedioPago.Efectivo:
					{
                        EEstadoCobro = EEstado.Charged;
					}
					break;

				case EMedioPago.CompensacionFactura:
					{
						Importe = 0;
                        EEstadoCobro = EEstado.Charged;
					}
					break;
			}

			if (!Library.Common.EnumFunctions.NeedsCuentaBancaria(EMedioPago))
			{
				OidCuentaBancaria = 0;
				CuentaBancaria = string.Empty;
			}
		}
		public virtual void SetVencimiento(DateTime vencimiento)
		{
			Vencimiento = vencimiento;
			SetCobrado();
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

			//Cliente
			if ((OidCliente == 0) && (ETipoCobro == ETipoCobro.Cliente))
			{
				e.Description = Resources.Messages.NO_CLIENTE_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

			//Cuenta Bancaria
			if (OidCuentaBancaria == 0)
			{
				switch (EMedioPago)
				{
					case EMedioPago.Giro:
					case EMedioPago.Ingreso:
					case EMedioPago.Tarjeta:
					case EMedioPago.Transferencia:
					case EMedioPago.Domiciliacion:
					case EMedioPago.ComercioExterior:
						e.Description = Resources.Messages.NO_CUENTA_BANCARIA_SELECTED;
						throw new iQValidationException(e.Description, string.Empty);
				}
			}

			//TPV
			if (OidTPV  == 0)
			{
				switch (EMedioPago)
				{
					case EMedioPago.Tarjeta:
						e.Description = Resources.Messages.NO_TPV_SELECTED;
						throw new iQValidationException(e.Description, string.Empty);
				}
			}

			return true;
		}	

        #endregion

        #region Autorization Rules

        public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.COBRO);
        }
        public static bool CanGetObject()
        {
			return AutorizationRulesControler.CanGetObject(Resources.SecureItems.COBRO);
        }
        public static bool CanDeleteObject()
        {
			return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.COBRO);
        }
        public static bool CanEditObject()
        {
			return AutorizationRulesControler.CanEditObject(Resources.SecureItems.COBRO);
        }
		public static bool CanChangeState()
		{
			return AutorizationRulesControler.CanGetObject(Library.Common.Resources.SecureItems.ESTADO);
		}

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        protected Charge()
        {
            _base.Record.Oid = (long)(new Random()).Next();
            Fecha = DateTime.Now;
            Vencimiento = DateTime.Today;
            MedioPago = (long)EMedioPago.Seleccione;
			EEstado = EEstado.Abierto;
            EEstadoCobro = EEstado.Pendiente;
        }

		public virtual ChargeInfo GetInfo(bool childs = true) { return new ChargeInfo(this, childs); }

        private void InsertRelateds()
        {
            switch (EMedioPago)
            {
                case EMedioPago.Pagare:
                case EMedioPago.Cheque:
                    {
                        FinancialCash financial_cash = FinancialCash.New(GetInfo(false), SessionCode);
                        financial_cash.Save();
                    }
                    break;

                default:

                    //Insertamos la linea de caja o el movimiento de banco asociado
                    Cash.InsertItem(this, SessionCode);
                    BankLine.InsertItem(this, SessionCode);

                    break;
            }
        }

        #endregion

        #region Root Factory Methods

        public static Charge New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return DataPortal.Create<Charge>(new CriteriaCs(-1));
        }
        public static Charge New(ETipoCobro tipo, int sessionCode = -1)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            Charge obj = DataPortal.Create<Charge>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
			obj.TipoCobro = (int)tipo;
            obj.GetNewCode(obj.OidCliente);
            return obj;
        }

		public static Charge Get(string query, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return Get(query, childs, -1);
		}
		public static Charge Get(long oid) { return Get(oid, ETipoCobro.Todos, true); }
		public static Charge Get(long oid, bool childs) { return Get(oid, ETipoCobro.Todos, childs); }
        public static Charge Get(long oid, ETipoCobro tipo) { return Get(oid, tipo, true); }
        public static Charge Get(long oid, ETipoCobro tipo, bool childs) { return Get(Charge.SELECT(oid, tipo), childs); }

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
        /// Elimina todos los Despachante. 
        /// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = Charge.OpenSession();
            ISession sess = Charge.Session(sessCode);
            ITransaction trans = Charge.BeginTransaction(sessCode);

            try
            {
                sess.Delete("from ChargeRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                Charge.CloseSession(sessCode);
            }
        }

        /// <summary>
        /// Guarda en la base de datos todos los cambios del objeto.
        /// También guarda los cambios de los hijos si los tiene
        /// </summary>
        /// <returns>Objeto actualizado y con los flags reseteados</returns>
        public override Charge Save()
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

				if (ETipoCobro == ETipoCobro.REA)
					_cobro_reas.Update(this);
				else
					_cobro_facturas.Update(this);                

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

        #endregion				

        #region Child Factory Methods

        private Charge(Charge source)
        {
            MarkAsChild();
            Fetch(source);
        }
        private Charge(Cliente parent)
            : this()
        {
            MarkAsChild();

            OidCliente = parent.Oid;
            Cliente = parent.Nombre;
            IDCliente = parent.Codigo;

            GetNewCode(parent.Oid);

            _cobro_facturas = CobroFacturas.NewChildList();
            _cobro_reas = CobroREAs.NewChildList();
        }
        private Charge(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

        public static Charge NewChild(Cliente parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            Charge obj = new Charge(parent);

            if (parent.Cobros.Count > 0)
            {
                SortedBindingList<ChargeInfo> sortedList = new SortedBindingList<ChargeInfo>(parent.GetInfo().Cobros);
                sortedList.ApplySort("IdCobro", ListSortDirection.Ascending);
                Int64 lastid = sortedList[sortedList.Count - 1].IdCobro;
                obj.IdCobro = lastid + 1;
            }
            else
                obj.IdCobro = 1;

            return obj;
        }

        internal static Charge GetChild(Charge source) { return new Charge(source); }
		internal static Charge GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, true); }
        internal static Charge GetChild(int sessionCode, IDataReader reader, bool childs) { return new Charge(sessionCode, reader, childs); }

        /// <summary>
        /// Borrado aplazado, es posible el undo 
        /// (La funci�n debe ser "no est�tica")
        /// </summary>
        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
        }

        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
            Oid = (long)new Random().Next();
            GetNewCode(OidCliente);
            Fecha = DateTime.Now;
            Vencimiento = DateTime.Today;
            EMedioPago = EMedioPago.Seleccione;
			EEstado = EEstado.Abierto;
            EEstadoCobro = EEstado.Charged;

            _cobro_facturas = CobroFacturas.NewChildList();
            _cobro_reas = CobroREAs.NewChildList();
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

					CobroFactura.DoLOCK(Session());
					query = CobroFacturas.SELECT_BY_COBRO(this.Oid);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_cobro_facturas = CobroFacturas.GetChildList(SessionCode, reader, false);

					CobroREA.DoLOCK(Session());
					query = CobroREAs.SELECT_BY_COBRO(this.Oid);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _cobro_reas = CobroREAs.GetChildList(SessionCode, reader, false);

					GastosDemora = _cobro_facturas.GastosDemora();
				}
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Insert(Charges parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            ValidationRules.CheckRules();
            OidUsuario = (AppContext.User != null) ? (int)AppContext.User.Oid : 0;
            Usuario = (AppContext.User != null) ? AppContext.User.Name : string.Empty;

			parent.Session().Save(Base.Record);
            _cobro_facturas.Update(this);

            //Insertamos la linea de caja y el movimiento de banco asociado
            Cash.InsertItem(this, SessionCode);
            BankLine.InsertItem(this, SessionCode);

            MarkOld();
		}

		internal void Update(Charges parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;

            Charge obj = null;

            try
            {
                ChargeRecord record = Session().Get<ChargeRecord>(this.Oid);
                obj = Charge.Get(Oid, true, SessionCode);

                if (EntityBase.UpdateByYear(obj, this, parent))
                {
                    obj.Save();
                    //Transaction().Commit();
                    //CloseSession();
                    NewTransaction();
                }
                else
                {
                    record.CopyValues(this.Base.Record);
                    Session().Update(record);
                    //obj.CloseSession();
                }
            }
            catch (Exception ex)
            {
                //if (obj != null) obj.CloseSession();
                throw ex;
            }

			MarkOld();
		}

		internal void DeleteSelf(Charges parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				ChargeRecord obj = parent.Session().Get<ChargeRecord>(Oid);
				obj.CopyValues(Base.Record);

				parent.Session().Update(obj);

				//Nunca se borran, se anulan
				EEstado = EEstado.Anulado;

				//Anulamos la Linea de Caja o el Movimiento de Banco si los hubiera
				Cash.AnulaItem(this, SessionCode);
				BankLine.AnulaItem(this, SessionCode);
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
                    Charge.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

						if (ETipoCobro == ETipoCobro.REA)
						{
							CobroREA.DoLOCK(Session());
							query = CobroREAs.SELECT_BY_COBRO(this.Oid);
							reader = nHMng.SQLNativeSelect(query, Session());
							_cobro_reas = CobroREAs.GetChildList(SessionCode, reader);
						}
						else
						{
							CobroFactura.DoLOCK(Session());
							query = CobroFacturas.SELECT(this);
							reader = nHMng.SQLNativeSelect(query, Session());
							_cobro_facturas = CobroFacturas.GetChildList(SessionCode, reader);
						}

						GastosDemora = _cobro_facturas.GastosDemora();
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
                _base.Record.Oid = 0;

                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }

                GetNewCode(OidCliente);
                OidUsuario = (AppContext.User != null) ? (int)AppContext.User.Oid : 0;
                Usuario = (AppContext.User != null) ? AppContext.User.Name : string.Empty;
                Session().Save(Base.Record);

                InsertRelateds();
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
            {
                Charge obj = null;

                try
                {
                    ChargeRecord record = Session().Get<ChargeRecord>(this.Oid);
                    obj = Charge.Get(Oid, true, SessionCode);

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
                }
                catch (Exception ex)
                {
                    //if (obj != null) obj.CloseSession();
                    throw ex;
                }
			}
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
				Charge obj = Charge.Get(criteria.Oid);

				//Nunca se borran, se anulan
				obj.EEstado = EEstado.Anulado;

				//Anulamos la Linea de Caja o el Movimiento de Banco si los hubiera
				Cash.AnulaItem(this, obj.SessionCode);
                BankLine.AnulaItem(obj, obj.SessionCode);

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

        #region Child Data Access

        private void Fetch(Charge source)
        {
            try
            {
                SessionCode = source.SessionCode;

				_base.CopyValues(source);

                IDataReader reader;
                string query;

				if (ETipoCobro == ETipoCobro.REA)
				{
					CobroREA.DoLOCK(Session());
					query = CobroREAs.SELECT_BY_COBRO(this.Oid);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_cobro_reas = CobroREAs.GetChildList(SessionCode, reader, false);
				}
				else
				{
					CobroFactura.DoLOCK(Session());
					query = CobroFacturas.SELECT_BY_COBRO(this.Oid);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_cobro_facturas = CobroFacturas.GetChildList(SessionCode, reader, false);
				}

				GastosDemora = _cobro_facturas.GastosDemora();
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            MarkOld();
        }

        internal void Insert(Cliente parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

            OidCliente = parent.Oid;

            GetNewCode(OidCliente);
            OidUsuario = (int)AppContext.User.Oid;
            Usuario = AppContext.User.Name;

            ValidationRules.CheckRules();

            parent.Session().Save(Base.Record);
            _cobro_facturas.Update(this);

            InsertRelateds();
            
            MarkOld();
        }

        internal void Update(Cliente parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

			OidCliente = parent.Oid;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                Charge obj = null;

                try
                {
                    ChargeRecord record = parent.Session().Get<ChargeRecord>(this.Oid);
                    obj = Charge.Get(Oid, true, SessionCode);

                    if (EntityBase.UpdateByYear(obj, this, parent))
                    {
                        obj.Save();

                        _cobro_facturas.Update(this);
                        _cobro_reas.Update(this);

                        parent.Transaction().Commit();
                        //parent.CloseSession();
                        parent.NewTransaction();
                    }
                    else
                    {
                        record.CopyValues(this.Base.Record);
                        parent.Session().Update(record);
                        _cobro_facturas.Update(this);
                        _cobro_reas.Update(this);
                        //obj.CloseSession();
                    }
                }
                catch (Exception ex)
                {
                    //if (obj != null) obj.CloseSession();
                    throw ex;
                }

                MarkOld();
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        internal void DeleteSelf(Cliente parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
				ChargeRecord obj = parent.Session().Get<ChargeRecord>(Oid);
				obj.CopyValues(Base.Record);

				//Nunca se borran, se anulan
				Base.Record.CopyValues(obj);
				EEstado = EEstado.Anulado;

				parent.Session().Update(obj);				

				//Anulamos la Linea de Caja o el Movimiento de Banco si los hubiera
				Cash.AnulaItem(this, SessionCode);
				BankLine.AnulaItem(this, SessionCode);
            }
            catch (Exception ex)
            {
				iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }

		public static void UpdateCobradoFromList(List<ChargeInfo> list, bool cobrado)
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Charge.OpenSession();
			ITransaction trans = Charge.BeginTransaction(AlbaranTicket.Session(sessCode));

			try
			{
				List<long> oids = new List<long>();

				oids.Add(0);

				foreach (ChargeInfo item in list)
					oids.Add(item.Oid);

				nHManager.Instance.SQLNativeExecute(UPDATE_COBRADO(oids, cobrado));

				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				AlbaranTicket.CloseSession(sessCode);
			}
		}

        #endregion

        #region SQL

		internal enum ETipoQuery { GENERAL = 0, CLIENTE = 1, REA = 2, FOMENTO = 3 }

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>()
			{
				 { 
					 "Cliente", 
					 new ForeignField() {                        
						 Property = "Cliente", 
						 TableAlias = "CL", 
						 Column = nHManager.Instance.GetTableColumn(typeof(ClientRecord), "Nombre")
					 }
				 },
			};
		}

        public new static string SELECT(long oid) { return SELECT(oid, ETipoCobro.Todos); }
        public static string SELECT(long oid, ETipoCobro tipo) { return SELECT(oid, tipo, true); }

        internal static string SELECT_FIELDS(ETipoQuery tipo)
        {
            string query;

            query = @"
				SELECT C.*
					,COALESCE(US.""NAME"", '') AS ""USUARIO""
					,COALESCE(CB.""VALOR"", '') AS ""CUENTA_BANCARIA""
					,COALESCE(TP.""NOMBRE"", '') AS ""TPV""
					,COALESCE(MV.""CODIGO"", '') AS ""ID_MOVIMIENTO_BANCO""
					,COALESCE(RG.""CODIGO"", '') || '/' || COALESCE(LR.""ID_EXPORTACION"", '') AS ""ID_MOVIMIENTO_CONTABLE""
                    ,COALESCE(CB.""ENTIDAD"", '') AS ""ENTIDAD""";

			switch (tipo)
			{ 
				case ETipoQuery.CLIENTE:
					query += @"
						,COALESCE(LC.""CODIGO"", '') AS ""ID_LINEA_CAJA""
                        ,COALESCE(EF.""CODIGO"", '') AS ""ID_EFECTO""
					    ,CL.""CODIGO"" AS ""ID_CLIENTE""
					    ,CL.""NOMBRE"" AS ""CLIENTE""
					    ,C.""IMPORTE"" - COALESCE(""ASIGNADO"", 0) AS ""PENDIENTE""";
					break;

				case ETipoQuery.REA:
					query += @"
						,'' AS ""ID_LINEA_CAJA""
                        ,'' AS ""ID_EFECTO""
						,'' AS ""ID_CLIENTE""
						,'' AS ""CLIENTE""
						,C.""IMPORTE"" - COALESCE(""ASIGNADO"", 0) AS ""PENDIENTE""";
                    break;

                case ETipoQuery.FOMENTO:
                    query += @"
						,'' AS ""ID_LINEA_CAJA""
                        ,'' AS ""ID_EFECTO""
						,'' AS ""ID_CLIENTE""
						,'' AS ""CLIENTE""
						,C.""IMPORTE"" - COALESCE(""ASIGNADO"", 0) AS ""PENDIENTE""";
                    break;
			}

            return query;
        }

        internal static string JOIN(QueryConditions conditions)
        {
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
            string cr = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            string tp = nHManager.Instance.GetSQLTable(typeof(TPVRecord));
			string mv = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));
			string lr = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
			string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));
            string ef = nHManager.Instance.GetSQLTable(typeof(FinancialCashRecord));

            string query;
			string tipo = "(" + (long)EBankLineType.Cobro + ")";

            query = @"
				FROM " + cr + @" AS C
                LEFT JOIN " + us + @" AS US ON US.""OID"" = C.""OID_USUARIO""
				LEFT JOIN " + cb + @" AS CB ON C.""OID_CUENTA_BANCARIA"" = CB.""OID""
				LEFT JOIN " + tp + @" AS TP ON C.""OID_TPV"" = TP.""OID""
				LEFT JOIN (SELECT MIN(MV.""CODIGO"") AS ""CODIGO"", MV.""OID_OPERACION"", MV.""TIPO_OPERACION""
							FROM " + mv + @" AS MV
							WHERE MV.""ESTADO"" != " + (long)EEstado.Anulado + @"
							GROUP BY MV.""OID_OPERACION"", MV.""TIPO_OPERACION"")
					AS MV ON C.""OID"" = MV.""OID_OPERACION"" AND MV.""TIPO_OPERACION"" IN " + tipo + @"
                LEFT JOIN ( SELECT EF.""CODIGO"", EF.""OID_COBRO"", EF.""ESTADO_COBRO""
                            FROM " + ef + @" AS EF 
                            WHERE EF.""ESTADO"" != " + (long)EEstado.Anulado + @")
                    AS EF ON EF.""OID_COBRO"" = C.""OID""
				LEFT JOIN (SELECT MAX(""ID_EXPORTACION"") AS ""ID_EXPORTACION"", ""OID_ENTIDAD"", MAX(""OID_REGISTRO"") AS ""OID_REGISTRO""
							FROM " + lr + @" AS LR
							WHERE LR.""TIPO_ENTIDAD"" = " + (long)ETipoEntidad.Cobro + @"
								AND LR.""ESTADO"" = " + (long)EEstado.Contabilizado + @"
							GROUP BY ""OID_ENTIDAD"")
					AS LR ON C.""OID"" = LR.""OID_ENTIDAD""
				 LEFT JOIN " + rg + @" AS RG ON RG.""OID"" = LR.""OID_REGISTRO""";

            return query + conditions.ExtraJoin;
        }

		public static string JOIN_PARTNER(QueryConditions conditions)
		{
			Assembly assembly = Assembly.Load("moleQule.Library.Partner");

			//string pn = nHManager.Instance.GetSQLTable(assembly.GetType("moleQule.Library.Partner.Partner"));
			string br = nHManager.Instance.GetSQLTable(assembly.GetType("moleQule.Library.Partner.BranchRecord"));
			string bc = nHManager.Instance.GetSQLTable(assembly.GetType("moleQule.Library.Partner.BranchClienteRecord"));

			string query = @"
				INNER JOIN " + bc + @" AS BC ON BC.""OID_CLIENT"" = C.""OID_CLIENTE""
				INNER JOIN " + br + @" AS BR ON BR.""OID"" = BC.""OID_BRANCH""";

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = @"
				WHERE (C.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";
            query += @"
                AND (C.""VENCIMIENTO"" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + "')";

			query += EntityBase.NO_NULL_RECORDS_CONDITION("C");

			if (conditions.Cobro != null)
				if (conditions.Cobro.Oid != 0) 
					query += @"
						AND C.""OID"" = " + conditions.Cobro.Oid;

			if (conditions.OidList != null)
				query += @"
					AND C.""OID"" IN " + EntityBase.GET_IN_STRING(conditions.OidList);

			query += EntityBase.ESTADO_CONDITION(conditions.Estado, "C");

			if (conditions.TipoCobro != ETipoCobro.Todos) 
				query += @"
					AND C.""TIPO_COBRO"" = " + (long)conditions.TipoCobro;
			
			if (conditions.Cliente != null) 
				query += @"
					AND C.""OID_CLIENTE"" = " + conditions.Cliente.Oid;
			
			if (conditions.Expediente != null) 
				query += @"
					AND CR2.""OID_EXPEDIENTE"" = " + conditions.Expediente.Oid;

			if (conditions.MedioPago != EMedioPago.Todos) 
			{
				switch (conditions.MedioPago)
				{
					case EMedioPago.NoEfectivo:
						query += @"
							AND C.""MEDIO_PAGO"" != " + (long)EMedioPago.Efectivo;
						break;

					case EMedioPago.NoTarjeta:
						query += @"
							AND C.""MEDIO_PAGO"" != " + (long)EMedioPago.Tarjeta;
						break;

					default:
						query += @"
							AND C.""MEDIO_PAGO"" = " + (long)conditions.MedioPago;
						break;
				}
            }

            if (conditions.MedioPagoList != null && conditions.MedioPagoList.Count > 0)
                query += EntityBase.GET_IN_LIST_CONDITION(conditions.MedioPagoList, "C", "MEDIO_PAGO");

			if (AppContext.User.IsPartner)
				query += EntityBase.GET_IN_BRANCHES_LIST_CONDITION(AppContext.Principal.Branches, "BC");

			return query + conditions.ExtraWhere;
		}

        internal static string SELECT(Library.Invoice.QueryConditions conditions, bool lockTable)
        {
            string query = 
			SELECT_BASE(conditions) +
            ORDER_BASE(conditions);			

			//query += EntityBase.LOCK("C", lockTable);

            return query;
        }

        internal static string SELECT_COBRADOS(Library.Invoice.QueryConditions conditions, bool lockTable)
        {
            string query = SELECT_BASE(conditions) + @"
                            AND C.""ESTADO_COBRO"" = " + (long)EEstado.Charged + @"
                            AND COALESCE(EF.""ESTADO_COBRO"", " + (long)EEstado.Charged + ") = " + (long)EEstado.Charged +
                            ORDER_BASE(conditions);

            //query += EntityBase.LOCK("C", lockTable);

            return query;
        }   

		internal static string SELECT_BASE(Library.Invoice.QueryConditions conditions)
		{
			string query = string.Empty;

			switch (conditions.TipoCobro)
			{
				case ETipoCobro.Todos:
					{
                        query = SELECT_FACTURAS(conditions) + @" 
								UNION" +
                                SELECT_REA(conditions) + @"
								UNION" +
                                SELECT_FOMENTO(conditions);
					} 
					break;

				case ETipoCobro.Cliente:
					query = SELECT_FACTURAS(conditions);
					break;

				case ETipoCobro.REA:
					query = SELECT_REA(conditions);
					break;
                case ETipoCobro.Fomento:
                    query = SELECT_FOMENTO(conditions);
                    break;
			}

			return query;
		}

        internal static string ORDER_BASE(Library.Invoice.QueryConditions conditions)
        {
            string query = string.Empty;

            if (conditions != null)
            {
                if (conditions.OrderFields == null)
                {
                    if (conditions.Orders != null)
                        query += ORDER(conditions.Orders, string.Empty, ForeignFields());
                    else
                    {
                        query += @"
							ORDER BY ""ID_COBRO""" + ((conditions.Order == ListSortDirection.Ascending) ? "ASC" : "DESC");
                    }
                }
                else
                {
                    string subquery_order = string.Empty;

                    foreach (string property in conditions.OrderFields)
                    {
                        try
                        {
                            string field = nHManager.Instance.GetTableField(typeof(ChargeRecord), property);
                            subquery_order += "\"" + field + "\"";

                            if (conditions.Order == ListSortDirection.Descending)
                                subquery_order += " DESC";

                            subquery_order += ",";
                        }
                        catch { }
                    }

                    if (subquery_order != string.Empty)
                        query += @"
							ORDER BY " + subquery_order.Substring(0, subquery_order.Length - 1);
                }

                query += LIMIT(conditions.PagingInfo);
            }
            else
            {
                query += @"
					ORDER BY ""ID_COBRO""" + ((conditions.Order == ListSortDirection.Ascending) ? "ASC" : "DESC");
            }

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
			return SELECT(conditions, lockTable);
		}

		public static string SELECT_COUNT() { return SELECT_COUNT(new QueryConditions()); }
		public static string SELECT_COUNT(QueryConditions conditions)
		{
			string query;

			query = @"
                SELECT COUNT(*) AS ""TOTAL_ROWS""" +
				JOIN(conditions) +
				WHERE(conditions);

			return query;
		}

		internal static string SELECT(long oid, ETipoCobro tipo, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions
			{
				Cobro = ChargeInfo.New(oid, tipo),
				TipoCobro = tipo
			};

			return SELECT(conditions, lockTable);
		}

		private static string SELECT_BASE_FACTURAS(Library.Invoice.QueryConditions conditions)
		{
			string cf = nHManager.Instance.GetSQLTable(typeof(ChargeOperationRecord));
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));
			
			string query;

			query = 
            SELECT_FIELDS(ETipoQuery.CLIENTE) +
			JOIN(conditions) + @"
            LEFT JOIN " + cl + @" AS CL ON C.""OID_CLIENTE"" = CL.""OID""
            LEFT JOIN (SELECT ""OID_COBRO"", SUM(""CANTIDAD"") AS ""ASIGNADO""
				            FROM " + cf + @" AS CF1 
				            GROUP BY ""OID_COBRO"")
		            AS CF ON CF.""OID_COBRO"" = C.""OID""
            LEFT JOIN " + lc + @" AS LC ON LC.""OID_COBRO"" = C.""OID"" AND LC.""OID_CAJA"" = 1 AND LC.""ESTADO"" != " + (long)EEstado.Anulado;

			if (AppContext.User.IsPartner)
				query += JOIN_PARTNER(conditions);

			return query;
		}

        private static string SELECT_FACTURAS(Library.Invoice.QueryConditions conditions)
        {
			string query;

			ETipoCobro tipo = conditions.TipoCobro;
			conditions.TipoCobro = ETipoCobro.Cliente;

			query = 
				SELECT_BASE_FACTURAS(conditions) +	
				WHERE(conditions);

			conditions.TipoCobro = tipo;

            return query;
        }

        private static string SELECT_BASE_FOMENTO(Library.Invoice.QueryConditions conditions)
        {
            string cr = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));
            string query;

            query = 
            SELECT_FIELDS(ETipoQuery.FOMENTO) +
            JOIN(conditions) + @"
            LEFT JOIN (SELECT ""OID_COBRO"", SUM(""CANTIDAD"") AS ""ASIGNADO""
			            FROM " + cr + @" AS CR1
			            GROUP BY ""OID_COBRO"")
                    AS CR ON CR.""OID_COBRO"" = C.""OID""";

            if (conditions.Expediente != null)
                query += @"
					INNER JOIN (SELECT ""OID_EXPEDIENTE"" FROM " + cr + @" GROUP BY ""OID_EXPEDIENTE"") AS CR2 ON CR2.""OID_COBRO"" = C.""OID""";

            return query;
        }

		private static string SELECT_BASE_REA(Library.Invoice.QueryConditions conditions)
		{
			string cr = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));
			string query;

			query = 
            SELECT_FIELDS(ETipoQuery.REA) +
            JOIN(conditions) + @"
            LEFT JOIN (SELECT ""OID_COBRO"", SUM(""CANTIDAD"") AS ""ASIGNADO""
				            FROM " + cr + @" AS CR1
				            GROUP BY ""OID_COBRO"")
		            AS CR ON CR.""OID_COBRO"" = C.""OID""";

			if (conditions.Expediente != null)
				query += @"
					INNER JOIN (SELECT ""OID_EXPEDIENTE"" FROM " + cr + @" GROUP BY ""OID_EXPEDIENTE"") AS CR2 ON CR2.""OID_COBRO"" = C.""OID""";

			return query;
		}

        private static string SELECT_FOMENTO(Library.Invoice.QueryConditions conditions)
        {
            string query;

            ETipoCobro tipo = conditions.TipoCobro;
            conditions.TipoCobro = ETipoCobro.Fomento;

            query = 
				SELECT_BASE_FOMENTO(conditions) +
				WHERE(conditions);

            conditions.TipoCobro = tipo;

            return query;
        }
		
		private static string SELECT_REA(Library.Invoice.QueryConditions conditions)
        {
            string query;

			ETipoCobro tipo = conditions.TipoCobro; 
			conditions.TipoCobro = ETipoCobro.REA;

			query = 
				SELECT_BASE_REA(conditions) +
				WHERE(conditions);

			conditions.TipoCobro = tipo;

            return query;
        }

		internal static string SELECT_PENDIENTES(QueryConditions conditions, bool lock_table)
		{
			string query = string.Empty;

			string condition = @"
				AND C.""ESTADO_COBRO"" != " + (long)EEstado.Charged + @"
				AND (C.""VENCIMIENTO"" BETWEEN '" + conditions.FechaAuxIniLabel + @"' AND '" + conditions.FechaAuxFinLabel + @"')
				AND C.""ESTADO"" != " + (long)EEstado.Anulado;

			switch (conditions.TipoCobro)
			{
				case ETipoCobro.Todos:
					{
						query = 
							SELECT_FACTURAS(conditions) +
							condition + @"
							UNION" +
							SELECT_REA(conditions) +
							condition;
					}
					break;

				case ETipoCobro.Cliente:
					query = 
						SELECT_FACTURAS(conditions) +
						condition;
					break;

				case ETipoCobro.REA:
					query = 
						SELECT_REA(conditions) +
						condition;
					break;
			}

			return query;
		}

		internal static string SELECT_NEGOCIADOS(QueryConditions conditions, bool lock_table)
		{
			string query = string.Empty;

            string condition = @"
				AND C.""ESTADO_COBRO"" = " + (long)EEstado.Charged + @"
				AND C.""VENCIMIENTO"" > '" + DateTime.Today.ToString("MM/dd/yyyy") + @"'
				AND C.""ESTADO"" != " + (long)EEstado.Anulado;

			switch (conditions.TipoCobro)
			{
				case ETipoCobro.Todos:
					{
						query = 
							SELECT_FACTURAS(conditions) +
							condition + @"
							UNION " +
							SELECT_REA(conditions) +
							condition;
					}
					break;

				case ETipoCobro.Cliente:
					query = 
						SELECT_FACTURAS(conditions) +
						condition;
					break;

				case ETipoCobro.REA:
					query = 
						SELECT_REA(conditions) +
						condition;
					break;
			}

			return query;
		}

		internal static string SELECT_VENCIMIENTO_SIN_APUNTE(QueryConditions conditions)
		{
			string mv = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));

			string query = string.Empty;

            string condition = @"
				AND C.""ESTADO_COBRO"" != " + (long)EEstado.Charged + @"
				AND (C.""VENCIMIENTO"" BETWEEN '" + conditions.FechaAuxIniLabel + @"' AND '" + conditions.FechaAuxFinLabel + @"')
				AND C.""ESTADO"" != " + (long)EEstado.Anulado + @"
				AND C.""OID"" NOT IN (SELECT ""OID_OPERACION""
										FROM " + mv + @" AS MV
										WHERE ""TIPO_OPERACION"" = " + (long)EBankLineType.Cobro + @")
				AND C.""MEDIO_PAGO"" NOT IN " + Common.EnumFunctions.SQL_IN_MEDIO_PAGO_NOT_NEEDS_CUENTA_BANCARIA(); 

			switch (conditions.TipoCobro)
			{
				case ETipoCobro.Todos:
					{
						query = 
							SELECT_FACTURAS(conditions) +
							condition + @"
							UNION" +
							SELECT_REA(conditions) +
							condition;
					}
					break;

				case ETipoCobro.Cliente:
					query = 
						SELECT_FACTURAS(conditions) +
						condition;
					break;

				case ETipoCobro.REA:
					query = 
						SELECT_REA(conditions) +
						condition;
					break;
			}

			return query;
		}

		internal static string UPDATE_COBRADO(List<long> oid_list, bool cobrado)
		{
			string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));

			string query = string.Empty;

			query = @"
				UPDATE " + cb + @" SET ""ESTADO_COBRO"" = " + (cobrado ? (long)EEstado.Charged : (long)EEstado.Pendiente) + @"
				WHERE ""OID"" IN " + EntityBase.GET_IN_STRING(oid_list);

			return query;
		}
        
		#endregion
    }
}