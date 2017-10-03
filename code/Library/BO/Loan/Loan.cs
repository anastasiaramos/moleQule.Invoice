using System;
using System.Collections;
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
	public class LoanRecord : RecordBase
	{
		#region Attributes

		private long _oid_cuenta;
		private DateTime _fecha_firma;
		private DateTime _fecha_ingreso;
		private DateTime _fecha_vencimiento;
		private string _nombre = string.Empty;
		private Decimal _importe;
		private string _observaciones = string.Empty;
		private long _serial;
		private string _codigo = string.Empty;
		private long _oid_pago;
		private long _n_cuotas;
		private DateTime _inicio_pago;
		private long _periodo_pago;
		private Decimal _importe_cuota;
        private string _cuenta_contable = string.Empty;
        private Decimal _gastos_bancarios;
        private bool _gastos_inicio = false;
        private long _estado;

		#endregion

		#region Properties

		public virtual long OidCuenta { get { return _oid_cuenta; } set { _oid_cuenta = value; } }
		public virtual DateTime FechaFirma { get { return _fecha_firma; } set { _fecha_firma = value; } }
		public virtual DateTime FechaIngreso { get { return _fecha_ingreso; } set { _fecha_ingreso = value; } }
		public virtual DateTime FechaVencimiento { get { return _fecha_vencimiento; } set { _fecha_vencimiento = value; } }
		public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
		public virtual Decimal Importe { get { return _importe; } set { _importe = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long OidPago { get { return _oid_pago; } set { _oid_pago = value; } }
		public virtual long NCuotas { get { return _n_cuotas; } set { _n_cuotas = value; } }
		public virtual DateTime InicioPago { get { return _inicio_pago; } set { _inicio_pago = value; } }
		public virtual long PeriodoPago { get { return _periodo_pago; } set { _periodo_pago = value; } }
		public virtual Decimal ImporteCuota { get { return _importe_cuota; } set { _importe_cuota = value; } }
        public virtual string CuentaContable { get { return _cuenta_contable; } set { _cuenta_contable = value; } }
        public virtual Decimal GastosBancarios { get { return _gastos_bancarios; } set { _gastos_bancarios = value; } }
        public virtual bool GastosInicio { get { return _gastos_inicio; } set { _gastos_inicio = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }

		#endregion

		#region Business Methods

		public LoanRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_cuenta = Format.DataReader.GetInt64(source, "OID_CUENTA");
			_fecha_firma = Format.DataReader.GetDateTime(source, "FECHA_FIRMA");
			_fecha_ingreso = Format.DataReader.GetDateTime(source, "FECHA_INGRESO");
			_fecha_vencimiento = Format.DataReader.GetDateTime(source, "FECHA_VENCIMIENTO");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_importe = Format.DataReader.GetDecimal(source, "IMPORTE");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_oid_pago = Format.DataReader.GetInt64(source, "OID_PAGO");
			_n_cuotas = Format.DataReader.GetInt64(source, "N_CUOTAS");
			_inicio_pago = Format.DataReader.GetDateTime(source, "INICIO_PAGO");
			_periodo_pago = Format.DataReader.GetInt64(source, "PERIODO_PAGO");
			_importe_cuota = Format.DataReader.GetDecimal(source, "IMPORTE_CUOTA");
            _cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
            _gastos_bancarios = Format.DataReader.GetDecimal(source, "GASTOS_BANCARIOS");
            _gastos_inicio = Format.DataReader.GetBool(source, "GASTOS_INICIO");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");

		}
		public virtual void CopyValues(LoanRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_cuenta = source.OidCuenta;
			_fecha_firma = source.FechaFirma;
			_fecha_ingreso = source.FechaIngreso;
			_fecha_vencimiento = source.FechaVencimiento;
			_nombre = source.Nombre;
			_importe = source.Importe;
			_observaciones = source.Observaciones;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_oid_pago = source.OidPago;
			_n_cuotas = source.NCuotas;
			_inicio_pago = source.InicioPago;
			_periodo_pago = source.PeriodoPago;
			_importe_cuota = source.ImporteCuota;
            _cuenta_contable = source.CuentaContable;
            _gastos_bancarios = source.GastosBancarios;
            _gastos_inicio = source.GastosInicio;
            _estado = source.Estado;
		}

		#endregion
	}

	[Serializable()]
	public class LoanBase
	{
		#region Attributes

		private LoanRecord _record = new LoanRecord();

        protected decimal _capital_amortizado;
        protected decimal _capital_pendiente;
		protected string _cuenta_bancaria = string.Empty;
        protected string _cuenta_bancaria_asociada = string.Empty;
        protected string _entidad = string.Empty;
        protected string _pago = string.Empty;
        protected Decimal _partial_unpaid;

        internal string _vinculado = moleQule.Library.Store.Resources.Labels.SET_PAGO;
        internal Decimal _asignado;
        internal Decimal _pendiente_asignar;
        internal Decimal _pendiente;
        internal Decimal _total_pagado;
        internal DateTime _fecha_asignacion;

		#endregion

		#region Properties

        public LoanRecord Record { get { return _record; } }

        public EEstado EStatus { get { return (EEstado)_record.Estado; } }
        public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }

		public virtual decimal CapitalAmortizado { get { return Decimal.Round(_capital_amortizado, 2); } set { _capital_amortizado = value; } }
		public virtual decimal CapitalPendiente { get { return Decimal.Round(_capital_pendiente, 2); } set { _capital_pendiente = value; } }
        public virtual decimal PartialUnpaid { get { return Decimal.Round(_partial_unpaid, 2); } set { _partial_unpaid = value; } }
		public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
		public virtual string CuentaBancariaAsociada { get { return _cuenta_bancaria_asociada; } set {	_cuenta_bancaria_asociada = value; }	}
		public virtual string Entidad {	get	{ return _entidad; } set {	_entidad = value; }	}
		public virtual string Pago	{ get {	return _pago; }	set	{ _pago = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_capital_amortizado = Format.DataReader.GetDecimal(source, "CAPITAL_AMORTIZADO");
			_capital_pendiente = Format.DataReader.GetDecimal(source, "CAPITAL_PENDIENTE");
			_pendiente = _capital_pendiente == 0 ? 0 : _pendiente;

			_cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
			_cuenta_bancaria_asociada = Format.DataReader.GetString(source, "CUENTA_BANCARIA_ASOCIADA");
			_entidad = Format.DataReader.GetString(source, "ENTIDAD");
			_pago = Format.DataReader.GetString(source, "PAGO");

			_asignado = Format.DataReader.GetDecimal(source, "ASIGNADO");
			_pendiente_asignar = Format.DataReader.GetDecimal(source, "PENDIENTE_ASIGNAR");
			_pendiente = Format.DataReader.GetDecimal(source, "PENDIENTE");
			_total_pagado = Format.DataReader.GetDecimal(source, "TOTAL_PAGADO");
            _partial_unpaid = Format.DataReader.GetDecimal(source, "PENDIENTE_PAGO_PARCIAL");

			_vinculado = (_asignado == 0) ? moleQule.Library.Store.Resources.Labels.SET_PAGO : moleQule.Library.Store.Resources.Labels.RESET_PAGO;
		}
		internal void CopyValues(Loan source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_capital_amortizado = source.CapitalAmortizado;
			_capital_pendiente = source.CapitalPendiente;

			_cuenta_bancaria = source.CuentaBancaria;
			_cuenta_bancaria_asociada = source.CuentaBancariaAsociada;
			_entidad = source.Entidad;
			_pendiente_asignar = source.PendienteAsignar;
			_pago = source.Pago;
			_asignado = source.Asignado;
			_pendiente = source.Pendiente;
			_total_pagado = source.TotalPagado;
		}
		internal void CopyValues(LoanInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_capital_amortizado = source.CapitalAmortizado;
			_capital_pendiente = source.CapitalPendiente;

			_cuenta_bancaria = source.CuentaBancaria;
			_cuenta_bancaria_asociada = source.CuentaBancariaAsociada;
			_entidad = source.Entidad;
			_pendiente_asignar = source.PendienteAsignar;
			_pago = source.Pago;
			_asignado = source.Asignado;
			_pendiente = source.Pendiente;
			_total_pagado = source.TotalPagado;
		}

		#endregion
	}

    /// <summary>
    /// Editable Root Business Object With Editable Child Collection
    /// </summary>	
    [Serializable()]
    public class Loan : BusinessBaseEx<Loan>, IBankLine, ITransactionPayment, IEntidadRegistro
	{
		#region IEntidadRegistro

		public virtual ETipoEntidad ETipoEntidad { get { return ETipoEntidad.Prestamo; } }
		public virtual string DescripcionRegistro { get { return "PRÉSTAMO Nº " + Codigo + " de " + Fecha.ToShortDateString() + " de " + Total.ToString("C2") + " de " + Entidad; } }

		public virtual IEntidadRegistro ISave() { return (IEntidadRegistro)SharedSave(); }
		public virtual IEntidadRegistro IGet(long oid, bool childs) { return (IEntidadRegistro)Get(oid, childs); }

		public void Update(Registro parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
			LoanRecord obj = Session().Get<LoanRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			MarkOld();
		}

		#endregion

        #region ITransactionPayment

        public virtual decimal TotalPagado { get { return _base._total_pagado; } set { _base._total_pagado = value; } }
        public virtual decimal Asignado { get { return _base._asignado; } set { _base._asignado = value; } }
        public virtual decimal Pendiente { get { return _base._pendiente; } set { _base._pendiente = value; } }
		public virtual decimal PendienteAsignar { get { return Math.Min(_base._pendiente, _base._pendiente_asignar); } set { _base._pendiente_asignar = value; } }
		public virtual decimal Acumulado { get { return _base._pendiente_asignar; } set { } }
        public virtual string FechaAsignacion { get { return (_base._fecha_asignacion != DateTime.MinValue) ? _base._fecha_asignacion.ToShortDateString() : "---"; } set { _base._fecha_asignacion = DateTime.Parse(value); } }
        public virtual string Vinculado { get { return _base._vinculado; } set { _base._vinculado = value; } }
        public virtual decimal Total { get { return _base.Record.NCuotas * _base.Record.ImporteCuota; } set { } }
        public virtual long OidExpediente { get { return 0; } set { } }
        public virtual ETipoAcreedor ETipoAcreedor { get { return ETipoAcreedor.Todos; } set { } }

        public virtual string NFactura { get { return _base.Record.Codigo; } set { _base.Record.Codigo = value; } }

        #endregion

        #region IBankLine

        public virtual long TipoMovimiento { get { return (long)EBankLineType.Prestamo; } }
        public virtual EBankLineType ETipoMovimientoBanco { get { return EBankLineType.Prestamo; } }
        public virtual ETipoTitular ETipoTitular { get { return ETipoTitular.Todos; } }
        public virtual string CodigoTitular { get { return Codigo; } set { } }
        public virtual string Titular { get { return CuentaBancaria; } set { } }
        public virtual EMedioPago EMedioPago { get { return EMedioPago.Transferencia; } }
        public virtual DateTime Vencimiento { get { return FechaVencimiento; } set { FechaVencimiento = value; } }
        public virtual DateTime Fecha { get { return FechaIngreso; } set { } }
        public virtual bool Confirmado { get { return true; } }

        public virtual IBankLineInfo IGetInfo(bool childs) { return (IBankLineInfo)GetInfo(); }

        #endregion

        #region Attributes

		protected LoanBase _base = new LoanBase();

        private InterestRates _interest_rates = InterestRates.NewChildList();
        private Payments _payments = Payments.NewChildList();
        
        #endregion

        #region Properties

		public LoanBase Base { get { return _base; } }

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
		public virtual long OidCuenta
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCuenta;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCuenta.Equals(value))
				{
					_base.Record.OidCuenta = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FechaFirma
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaFirma;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaFirma.Equals(value))
				{
					_base.Record.FechaFirma = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FechaIngreso
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaIngreso;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaIngreso.Equals(value))
				{
					_base.Record.FechaIngreso = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FechaVencimiento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaVencimiento;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaVencimiento.Equals(value))
				{
					_base.Record.FechaVencimiento = value;
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
		public virtual long OidPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidPago.Equals(value))
				{
					_base.Record.OidPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long NCuotas
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.NCuotas;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.NCuotas.Equals(value))
				{
					_base.Record.NCuotas = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime InicioPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.InicioPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.InicioPago.Equals(value))
				{
					_base.Record.InicioPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Periodicidad
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PeriodoPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PeriodoPago.Equals(value))
				{
					_base.Record.PeriodoPago = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal ImporteCuota
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ImporteCuota;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.ImporteCuota.Equals(value))
				{
					_base.Record.ImporteCuota = value;
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
        public virtual bool GastosInicio
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.GastosInicio;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.GastosInicio.Equals(value))
                {
                    _base.Record.GastosInicio = value;
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

        //No enlazadas
        public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; } }
        public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual decimal CapitalAmortizado { get { return _base.CapitalAmortizado; } set { _base.CapitalAmortizado = value; } }
        public virtual decimal CapitalPendiente { get { return _base.CapitalPendiente; } set { _base.CapitalPendiente = value; } }
        public virtual decimal PartialUnpaid { get { return _base.PartialUnpaid; } set { _base.PartialUnpaid = value; } }
        public virtual string CuentaBancaria
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.CuentaBancaria;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.CuentaBancaria.Equals(value))
                {
                    _base.CuentaBancaria = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string CuentaBancariaAsociada
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.CuentaBancariaAsociada;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.CuentaBancariaAsociada.Equals(value))
                {
                    _base.CuentaBancariaAsociada = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Entidad
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Entidad;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Entidad.Equals(value))
                {
                    _base.Entidad = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Pago
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Pago;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Pago.Equals(value))
                {
                    _base.Pago = value;
                    PropertyHasChanged();
                }
            }
        }

        public virtual InterestRates InterestRates
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _interest_rates;
            }

            set
            {
                _interest_rates = value;
            }

        }
        public virtual Payments Payments
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _payments;
            }

            set
            {
                _payments = value;
            }

        }

        public override bool IsValid
        {
            get
            {
                return base.IsValid
                   && _interest_rates.IsValid && _payments.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                   || _interest_rates.IsDirty || _payments.IsDirty;
            }
        }

        #endregion

        #region Business Methods

        public virtual Loan CloneAsNew()
        {
            Loan clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.SessionCode = Loan.OpenSession();
            Loan.BeginTransaction(clon.SessionCode);

            clon.MarkNew();
            clon.InterestRates.MarkAsNew();
            clon.Payments.MarkAsNew();

            return clon;
        }

        public virtual void CopyFrom(LoanInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
            OidCuenta = source.OidCuenta;
            OidPago = source.OidPago;
            FechaFirma = source.FechaFirma;
            FechaIngreso = source.FechaIngreso;
            FechaVencimiento = source.FechaVencimiento;
            Nombre = source.Nombre;
            Importe = source.Importe;
            Observaciones = source.Observaciones;
            CuentaContable = source.CuentaContable;
            GastosBancarios = source.GastosBancarios;
            GastosInicio = source.GastosInicio;
        }
        public virtual void CopyFrom(PaymentInfo source)
        {
            if (source == null) return;

            OidCuenta = source.OidCuentaBancaria;
            OidPago = source.Oid;
            FechaFirma = source.Fecha;
            FechaIngreso = source.Fecha;
            Importe = source.Importe;
            Nombre = source.DescripcionRegistro;

            NCuotas = 1;
            InicioPago = source.Fecha;
            Periodicidad = 1;   

            BankAccountInfo cuenta = BankAccountInfo.Get(OidCuenta, false);
            FechaVencimiento = source.Fecha.AddDays(cuenta.DiasCredito);
            InicioPago = FechaVencimiento;
            ImporteCuota = Importe * ( 1 + cuenta.TipoInteres * cuenta.DiasCredito / 36000);
            GastosInicio = cuenta.PagoGastosInicio;
            GastosBancarios = Importe * (cuenta.TipoInteres * cuenta.DiasCredito / 36000);
        }

        public virtual void GetNewCode()
        {
            // Obtenemos el último serial de servicio
            Serial = SerialInfo.GetNext(typeof(Loan));
            Codigo = Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
        }

        public virtual bool CheckPeriodoTipoInteres()
        {
            for (int i = 0; i < InterestRates.Count; i++)
            {
                //La fecha de inicio debe ser anterior a la de finalización del periodo
                if (InterestRates[i].FechaInicio > InterestRates[i].FechaFin)
                    return false;

                for (int j = 0; j < InterestRates.Count; j++)
                {
                    if (i == j) continue;

                    //Los periodos no se pueden solapar entre ellos
                    if ((InterestRates[i].FechaInicio >= InterestRates[j].FechaInicio
                        && InterestRates[i].FechaInicio <= InterestRates[j].FechaFin)
                        || (InterestRates[i].FechaFin >= InterestRates[j].FechaInicio
                        && InterestRates[i].FechaFin <= InterestRates[j].FechaFin))
                        return false;
                }
            }
            return true;
        }

        public virtual void ChangeEstado(EEstado estado)
        {
            EntityBase.CheckChangeState(EEstado, estado);
            EEstado = estado;
        }
        public static Loan ChangeEstado(long oid, EEstado estado)
        {
            if (!CanChangeState())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            Loan item = null;

            try
            {
                item = Loan.Get(oid, false);
                LoanInfo oldItem = item.GetInfo(false);

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

        public virtual void GeneraPagosFraccionados(PaymentList pagos, bool deleteOld)
        {
            if (deleteOld)
            {
                //Se eliminan todos los pagos creados con anterioridad si existía alguno
                foreach (PaymentInfo pago in pagos)
                    Library.Store.Payment.ChangeEstado(pago.Oid, EEstado.Anulado, -1);
            }

            Payments pagos_fraccionados = Payments.NewList();
            pagos_fraccionados.NewTransaction();
            
            for (int i = 0; i < NCuotas; i++)
            {
                Payment fraccion = pagos_fraccionados.NewItem(GetInfo(true));
                fraccion.Fecha = InicioPago.AddMonths((int)Periodicidad * i);
                fraccion.Vencimiento = InicioPago.AddMonths((int)Periodicidad * i).Date;
                fraccion.ETipoPago = ETipoPago.Prestamo;
                fraccion.Observaciones = Observaciones + " -  PLAZO " + (i + 1).ToString() + "/" + NCuotas.ToString();
                fraccion.SetPagado();

                TransactionPayment operation = TransactionPayment.NewChild();
                operation.MarkItemChild();
                operation.CopyFrom(fraccion.GetInfo(false), (int)NCuotas - i);
                operation.ETipoPago = ETipoPago.Prestamo;
                operation.OidOperation = this.Oid;
                operation.Cantidad = fraccion.Importe;
                fraccion.Operations.NewItem(operation);

                fraccion.Pendiente = 0;
            }

            pagos_fraccionados.Save();
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
            //OidCuenta
            if (OidCuenta <= 0)
            {
                e.Description = String.Format(Library.Resources.Messages.NO_FIELD_SELECTED, "OidCuenta");
                throw new iQValidationException(e.Description, string.Empty);
            }

            //Nombre
            if (Nombre == string.Empty)
            {
                e.Description = String.Format(Library.Resources.Messages.NO_FIELD_FILLED, "Nombre");
                throw new iQValidationException(e.Description, string.Empty);
            }

            //Codigo
            if (Codigo == string.Empty)
            {
                e.Description = Resources.Messages.NO_ID_SELECTED;
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
        protected Loan() {}
        private Loan(Loan source)
        {
            MarkAsChild();
            Fetch(source);
        }
        private Loan(int sessionCode, IDataReader source, bool childs)
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
        public static Loan NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            Loan obj = DataPortal.Create<Loan>(new CriteriaCs(-1));
            obj.MarkAsChild();
            return obj;
        }
        public static Loan NewChild(PaymentInfo source)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            Loan obj = DataPortal.Create<Loan>(new CriteriaCs(-1));
            obj.CopyFrom(source);
            obj.MarkAsChild();
            return obj;
        }

        /// <summary>
        /// Crea un objeto
        /// </summary>
        /// <param name="source">Prestamo con los datos para el objeto</param>
        /// <returns>Objeto creado</returns>
        /// <remarks>
        /// La utiliza la BusinessListBaseEx correspondiente para montar la lista
        /// NO OBTIENE los hijos. Para ello utilice GetChild(Prestamo source, bool childs)
        /// <remarks/>
        internal static Loan GetChild(Loan source) { return new Loan(source); }
        internal static Loan GetChild(int sessionCode, IDataReader source, bool childs = false) { return new Loan(sessionCode, source, childs); }

        /// <summary>
        /// Construye y devuelve un objeto de solo lectura copia de si mismo.
        /// </summary>
        /// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
        /// <returns>Réplica de solo lectura del objeto</returns>
        public LoanInfo GetInfo(bool childs = true)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new LoanInfo(this, childs);
        }

        public void LoadChilds(Type type, bool childs)
        {
            if (type.Equals(typeof(Payment)))
            {
                _payments = Payments.GetChildList(this, childs);
            }
            else if (type.Equals(typeof(InterestRate)))
            {
                //_interest_rates = InterestRates.GetChildList(this, childs);
            }
        }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Crea un nuevo objeto
        /// </summary>
        /// <returns>Nuevo objeto creado</returns>
        public static Loan New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return DataPortal.Create<Loan>(new CriteriaCs(-1));
        }
        public static Loan New(PaymentInfo source)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            Loan obj = DataPortal.Create<Loan>(new CriteriaCs(-1));
            obj.CopyFrom(source);
            return obj;
        }

        public static Loan Get(long oid, bool childs = false)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = GetCriteria(OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(oid);

            BeginTransaction(criteria.Session);

            return DataPortal.Fetch<Loan>(criteria);
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
        /// Elimina todos los Prestamo. 
        /// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = Loan.OpenSession();
            ISession sess = Loan.Session(sessCode);
            ITransaction trans = Loan.BeginTransaction(sessCode);

            try
            {
                sess.Delete("from LoanRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                Loan.CloseSession(sessCode);
            }
        }

        /// <summary>
        /// Guarda en la base de datos todos los cambios del objeto.
        /// También guarda los cambios de los hijos si los tiene
        /// </summary>
        /// <returns>Objeto actualizado y con los flags reseteados</returns>
        public override Loan Save()
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

                _interest_rates.Update(this);
                _payments.Update(this);

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

        /// <summary>
        /// Guarda en la base de datos todos los cambios del objeto.
        /// También guarda los cambios de los hijos si los tiene
        /// </summary>
        /// <returns>Objeto actualizado y con los flags reseteados</returns>
        public override Loan SaveAsChild()
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

                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                base.Save();

                _interest_rates.Update(this);
                _payments.Update(this);
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
            FechaFirma = DateTime.Today;
            FechaIngreso = DateTime.Today;
            FechaVencimiento = DateTime.Today;
            NCuotas = 1;
            InicioPago = DateTime.Today;
            Periodicidad = 1;
            EEstado = EEstado.Abierto;
            GetNewCode();
        }

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="source">Objeto fuente</param>
        private void Fetch(Loan source)
        {
            SessionCode = source.SessionCode;

            _base.CopyValues(source);

            if (Childs)
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader;
                    string query;

                    Library.Invoice.InterestRate.DoLOCK(Session());
                    query = InterestRates.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _interest_rates = InterestRates.GetChildList(SessionCode, reader);

                    Library.Store.Payment.DoLOCK(Session());
                    query = Payments.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _payments = Payments.GetChildList(SessionCode, reader);
                }
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
            _base.CopyValues(source);

            if (Childs)
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader;
                    string query;

                    Library.Invoice.InterestRate.DoLOCK(Session());
                    query = InterestRates.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _interest_rates = InterestRates.GetChildList(SessionCode, reader);

                    Library.Store.Payment.DoLOCK(Session());
                    query = Payments.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _payments = Payments.GetChildList(SessionCode, reader);
                }
            }

            MarkOld();
        }

        /// <summary>
        /// Inserta el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
        internal void Insert(Loans parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            SessionCode = parent.SessionCode;

            GetNewCode();

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            Session().Save(Base.Record);

            //Insertamos el movimiento de banco asociado
            BankLine.InsertItem(this, parent.SessionCode);

            _interest_rates.Update(this);
            _payments.Update(this);

            MarkOld();
        }

        /// <summary>
        /// Actualiza el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
        internal void Update(Loans parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            SessionCode = parent.SessionCode;
			LoanRecord obj = Session().Get<LoanRecord>(Oid);

			Loan old = Loan.New();
			old.Base.Record.CopyValues(obj);
			LoanInfo oldItem = old.GetInfo();

            obj.CopyValues(Base.Record);
            Session().Update(obj);

            //Insertamos el movimiento de banco asociado
            BankLine.EditItem(this, oldItem, parent.SessionCode);

            _interest_rates.Update(this);
            _payments.Update(this);

            MarkOld();
        }

        /// <summary>
        /// Borra el registro de la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
        internal void DeleteSelf(Loans parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;
            Session().Delete(Session().Get<LoanRecord>(Oid));

            BankLine.AnulaItem(this, parent.SessionCode);

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
                    Loan.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        Library.Invoice.InterestRate.DoLOCK(Session());
                        query = InterestRates.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _interest_rates = InterestRates.GetChildList(SessionCode, reader);

                        //Library.Store.Pago.DoLOCK(Session());
                        //query = Payments.SELECT(this);
                        //reader = nHMng.SQLNativeSelect(query, Session());
                        //_pagos = Payments.GetChildList(SessionCode, reader);
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
            if (!SharedTransaction)
            {
                if (SessionCode < 0) SessionCode = OpenSession();
                BeginTransaction();
            }
            //Borrar si no hay código
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

			LoanRecord obj = Session().Get<LoanRecord>(Oid);

			Loan old = Loan.New();
			old.Base.Record.CopyValues(obj);
            LoanInfo oldItem = old.GetInfo();
            
			obj.CopyValues(Base.Record);
            
			Session().Update(obj);
            MarkOld();

            BankLine.EditItem(this, oldItem, SessionCode);
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
                LoanInfo obj = LoanInfo.Get(Oid);

                // Iniciamos la conexion y la transaccion
                SessionCode = OpenSession();
                BeginTransaction();

                //Anulamos el Movimiento de Banco asociado
                BankLine.AnulaItem(obj, SessionCode);

                //Si no hay integridad referencial, aquí se deben borrar las listas hijo
                CriteriaEx criterio = GetCriteria();
                criterio.AddOidSearch(criteria.Oid);
				Session().Delete((LoanRecord)(criterio.UniqueResult()));

                Transaction().Commit();
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                CloseSession();
            }
        }

        #endregion
        
        #region SQL

        internal enum ETipoQuery { GENERAL = 0, AGRUPADO = 1, PAYMENT = 2 }
        public new static string SELECT(long oid) { return SELECT(oid, true); }
        internal static string SELECT(long oid, bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions { Prestamo = LoanInfo.New(oid) };

            query = SELECT(conditions, lockTable);

            return query;
        }
        public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

        internal static string FIELDS(ETipoQuery tipo = ETipoQuery.GENERAL)
        {
            string query = @"
            SELECT LO.*
                ,BK.""VALOR"" AS ""CUENTA_BANCARIA""                
                ,BK.""ENTIDAD"" AS ""ENTIDAD""
                ,COALESCE(BKA.""VALOR"", '') AS ""CUENTA_BANCARIA_ASOCIADA""
                ,COALESCE(PR.""TOTAL_PAGADO"", 0) AS ""TOTAL_PAGADO""
                ,LO.""IMPORTE_CUOTA"" * (LO.""N_CUOTAS"" - COALESCE(PR.""N_CUOTAS_PAGADAS"", 0)) AS ""PENDIENTE""
                ,COALESCE(TP1.""ASIGNADO"", 0) AS ""ASIGNADO""
                ,LO.""IMPORTE"" - COALESCE(TP3.""TOTAL_ASIGNADO"", 0) AS ""PENDIENTE_ASIGNAR""
                ,COALESCE(TP2.""CAPITAL_AMORTIZADO"", COALESCE(PR.""TOTAL_PAGADO"", 0)) AS ""CAPITAL_AMORTIZADO""
                ,LO.""IMPORTE"" - COALESCE(TP2.""CAPITAL_AMORTIZADO"", COALESCE(PR.""TOTAL_PAGADO"", 0)) AS ""CAPITAL_PENDIENTE""
                ,COALESCE(TP4.""PENDIENTE_PAGO_PARCIAL"", 0) AS ""PENDIENTE_PAGO_PARCIAL""";

            switch (tipo)
            {
                case ETipoQuery.GENERAL:

                    query += @"                        
                        ,COALESCE(PG.""CODIGO"", '') AS ""PAGO""
                        ,COALESCE(PG.""OID"", 0) AS ""OID_PAGO""
                        ,COALESCE(PG.""ID_PAGO"", '0') AS ""ID_PAGO""
                        ,COALESCE(PG.""FECHA"", NULL) AS ""FECHA_PAGO""
                        ,COALESCE(PG.""MEDIO_PAGO"", 0) AS ""MEDIO_PAGO""";
                    break;

                case ETipoQuery.PAYMENT:
                    
                    query += @"
                        ,COALESCE(PA.""CODIGO"", COALESCE(PG.""CODIGO"", '')) AS ""PAGO""
                        ,COALESCE(PA.""OID"", COALESCE(PG.""OID"", 0)) AS ""OID_PAGO""
                        ,COALESCE(PA.""ID_PAGO"", COALESCE(PG.""ID_PAGO"", '0')) AS ""ID_PAGO""
                        ,COALESCE(PA.""FECHA"", COALESCE(PG.""FECHA"", NULL)) AS ""FECHA_PAGO""
                        ,COALESCE(PA.""MEDIO_PAGO"", COALESCE(PG.""MEDIO_PAGO"", 0)) AS ""MEDIO_PAGO""";
                    
                    break;
            }

            return query;
        }

        internal static string JOIN_BASE(QueryConditions conditions)
        {
            string tipo = "(" + (long)ETipoPago.Prestamo + ")";

            string lo = nHManager.Instance.GetSQLTable(typeof(LoanRecord));
            string bk = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));
            string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

            string query = @"
			FROM " + lo + @" AS LO
            INNER JOIN " + bk + @" AS BK ON BK.""OID"" = LO.""OID_CUENTA""
            LEFT JOIN " + bk + @" AS BKA ON BKA.""OID"" = BK.""OID_ASOCIADA""
			LEFT JOIN " + pa + @" AS PG ON PG.""OID"" = LO.""OID_PAGO""";

            // IMPORTE TOTAL PAGADO DE ESTE PRESTAMO
            query += @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                                ,SUM(PF.""CANTIDAD"") AS ""TOTAL_PAGADO""
                                ,PF.""TIPO_PAGO""
                                ,COUNT(PF.""OID"") AS ""N_CUOTAS_PAGADAS""
                        FROM " + tp + @" AS PF
                        INNER JOIN " + pa + @" AS PG ON PG.""OID"" = PF.""OID_PAGO""
                        WHERE PF.""TIPO_PAGO"" IN " + tipo + @"
                            AND PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                            AND PG.""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @"
                        GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
                AS PR ON PR.""OID_OPERACION"" = LO.""OID""";

            // IMPORTE TOTAL ASIGNADO A ESTE PRESTAMO POR TODOS LOS PAGOS
            query += @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                            ,SUM(PF.""CANTIDAD"") AS ""TOTAL_ASIGNADO""
                            ,COUNT(PF.""OID"") AS ""N_PAGOS""
                        FROM " + tp + @" AS PF
                        INNER JOIN " + pa + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" IN " + tipo + @"
                        WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY PF.""OID_OPERACION"", PF.""TIPO_PAGO"")
                AS TP3 ON TP3.""OID_OPERACION"" = LO.""OID""";

            switch (conditions.LoanType)
            {
                case ELoanType.Merchant:

                    // IMPORTE PENDIENTE DE PAGO
                    query += @"
                    LEFT JOIN (SELECT ""OID"" AS ""OID_LOAN""
                                    ,""IMPORTE"" AS ""PENDIENTE_PAGO_PARCIAL""
                                FROM " + lo + @" AS LO
                                WHERE LO.""FECHA_VENCIMIENTO"" BETWEEN '" + conditions.FechaAuxIniLabel + @"' AND '" + conditions.FechaAuxFinLabel + @"'
                                GROUP BY ""OID"")
                        AS TP4 ON TP4.""OID_LOAN"" = LO.""OID""";
                    break;

                case ELoanType.Bank:
                default:
                    // IMPORTE PENDIENTE DE PAGO
                    query += @"
                    LEFT JOIN (SELECT TP.""OID_OPERACION""
                                        ,SUM(TP.""CANTIDAD"") AS ""PENDIENTE_PAGO_PARCIAL""
                                FROM " + tp + @" AS TP
                                INNER JOIN " + pa + @" AS PA ON PA.""OID"" = TP.""OID_PAGO""
                                WHERE TP.""TIPO_PAGO"" IN " + tipo + @"
                                    AND PA.""ESTADO"" != " + (long)EEstado.Anulado + @"
                                    AND PA.""ESTADO_PAGO"" != " + (long)EEstado.Pagado + @"
                                    AND (PA.""VENCIMIENTO"" BETWEEN '" + conditions.FechaAuxIniLabel + @"' AND '" + conditions.FechaAuxFinLabel + @"')
                                GROUP BY TP.""OID_OPERACION"", TP.""TIPO_PAGO"")
                        AS TP4 ON TP4.""OID_OPERACION"" = LO.""OID""";
                    break;
            }

            return query + " " + conditions.ExtraJoin;
        }

        internal static string JOIN_BASE_ASIGNADO_PAGO(QueryConditions conditions)
        {
            long oid_pago = (conditions.Pago != null) ? conditions.Pago.Oid : 0;

            string tipo = "(" + (long)ETipoPago.Prestamo + ")";
            string pa = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));
            string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
            string ir = nHManager.Instance.GetSQLTable(typeof(InterestRateRecord));
            
			// IMPORTE PARCIAL DEL PAGO ASIGNADO A ESTE PRESTAMO
            string query = @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                                ,SUM(PF.""CANTIDAD"") AS ""ASIGNADO""
                                ,PG.""OID"" AS ""OID_PAGO""
                        FROM " + tp + @" AS PF
                        INNER JOIN " + pa + @" AS PG ON PG.""OID"" = PF.""OID_PAGO""
                                                    AND PF.""TIPO_PAGO"" IN " + tipo + @"
                        WHERE TRUE";

            if (oid_pago != 0) 
                query += @"
                            AND PG.""OID"" = " + oid_pago;
            query += @"
                            AND PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY PF.""OID_OPERACION"", PG.""OID"")
                AS TP1 ON TP1.""OID_OPERACION"" = LO.""OID""";

			// CAPITAL AMORTIZADO DE ESTE PRESTAMO
            query += @"
            LEFT JOIN ( SELECT PF.""OID_OPERACION""
                            ,SUM(PF.""CANTIDAD"") AS ""CAPITAL_AMORTIZADO""
                        FROM " + tp + @" AS PF
                        INNER JOIN " + pa + @" AS PG ON PG.""OID"" = PF.""OID_PAGO""
                        WHERE PF.""TIPO_PAGO"" IN " + tipo + @"
                            AND PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                            AND PG.""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @" 
                        GROUP BY PF.""OID_OPERACION"")
                AS TP2 ON TP2.""OID_OPERACION"" = LO.""OID""";

            return query;
        }

        internal static string JOIN_BASE_ASIGNADO_PRESTAMO(QueryConditions conditions)
        {
            string tipo = "(" + (long)ETipoPago.Prestamo + ")";
			string pa = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));
			string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));
			string it = nHManager.Instance.GetSQLTable(typeof(InterestRateRecord));

            // IMPORTE PARCIAL DEL PAGO ASIGNADO A ESTE PRESTAMO
            string query = @"
            LEFT JOIN (SELECT PF.""OID_OPERACION""
                            ,SUM(PF.""CANTIDAD"") AS ""ASIGNADO""
                        FROM " + tp + @" AS PF
                        INNER JOIN " + pa + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" 
                                                    AND PF.""TIPO_PAGO"" IN " + tipo + @"
                        WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY PF.""OID_OPERACION"")
                AS TP1 ON TP1.""OID_OPERACION"" = LO.""OID""";

            // CAPITAL AMORTIZADO DE ESTE PRESTAMO
            query += @"
            LEFT JOIN ( SELECT PF.""OID_OPERACION""
                                ,SUM(PF.""CANTIDAD"") AS ""CAPITAL_AMORTIZADO""
                        FROM " + tp + @" AS PF
                        INNER JOIN " + pa + @" AS PG ON PG.""OID"" = PF.""OID_PAGO""
                                                    AND PF.""TIPO_PAGO"" IN " + tipo + @"
                        WHERE PG.""ESTADO"" != " + (long)EEstado.Anulado + @" 
                            AND PG.""ESTADO_PAGO"" = " + (long)EEstado.Pagado + @"
                        GROUP BY PF.""OID_OPERACION"")
                AS TP2 ON TP2.""OID_OPERACION"" = LO.""OID""";

            return query;
        }

        internal static string WHERE(QueryConditions conditions)
        {
            string query = @"
            WHERE (LO.""FECHA_FIRMA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            query += EntityBase.STATUS_LIST_CONDITION(conditions.Status, "LO", "ESTADO");
            query += EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "LO");

            if (conditions.Prestamo != null)
            {
                if (conditions.Prestamo.Oid != 0)
                    query += @"
                    AND LO.""OID"" = " + conditions.Prestamo.Oid;
                
                if (conditions.Prestamo.OidPago != 0)
                    query += @"
                    AND LO.""OID_PAGO"" = " + conditions.Prestamo.OidPago;
            }

            if (conditions.TipoPago == ETipoPago.Prestamo)
                query += @"
                AND (LO.""FECHA_VENCIMIENTO"" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + "')";

            switch (conditions.LoanType)
            { 
                case ELoanType.Bank:

                    query += @"
                    AND LO.""OID_PAGO"" = 0";

                    break;

                case ELoanType.Merchant:

                    query += @"
                    AND LO.""OID_PAGO"" != 0";

                    if (conditions.Pago != null)
                        query += @"
                        AND LO.""OID_PAGO"" = " + conditions.Pago.Oid;

                    break;
            }
            
            return query + " " + conditions.ExtraWhere;
        }

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string pa = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));

            string query;

            query = 
			FIELDS(ETipoQuery.GENERAL) +
			JOIN_BASE(conditions) +
			JOIN_BASE_ASIGNADO_PRESTAMO(conditions) +
			WHERE(conditions);

			if (conditions.OrderFields != null)
			{
				string subquery_order = string.Empty;

				foreach (string field in conditions.OrderFields)
				{
					try
					{
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
			else
			{
				query += @"
				ORDER BY ""FECHA_VENCIMIENTO"" DESC, ""CAPITAL_PENDIENTE"" DESC";
			}

			query += LIMIT(conditions.PagingInfo);
			//query += EntityBase.LOCK("TR", lockTable);

            return query;
        }

        internal static string SELECT_BY_PAGO(QueryConditions conditions, bool lockTable)
        {
            string pa = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));
			string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

            string tipo = "(" + (long)ETipoPago.Prestamo + ")";

            conditions.ExtraJoin = @"
            INNER JOIN " + tp + @" AS TP ON TP.""OID_OPERACION"" = LO.""OID"" AND TP.""TIPO_PAGO"" IN " + tipo + @"
            INNER JOIN " + pa + @" AS PA ON PA.""OID"" = TP.""OID_PAGO""";

            conditions.ExtraWhere = @"           
                AND PA.""OID"" = " + conditions.Pago.Oid;

            string query = 
	        FIELDS(ETipoQuery.PAYMENT) +
	        JOIN_BASE(conditions) +
            JOIN_BASE_ASIGNADO_PAGO(conditions) +
			WHERE(conditions);

            //if (lock_table) query += " FOR UPDATE OF P NOWAIT";

            return query;
        }

        internal static string SELECT_PENDIENTES(QueryConditions conditions, bool lockTable)
        {
			string lo = nHManager.Instance.GetSQLTable(typeof(LoanRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));
            string tp = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

            string query = 
            FIELDS(ETipoQuery.PAYMENT) +
            JOIN_BASE(conditions) +
            JOIN_BASE_ASIGNADO_PAGO(conditions) + @"
            LEFT JOIN " + pa + @" AS PA ON PA.""OID"" = LO.""OID_PAGO""";

            conditions.TipoPago = ETipoPago.Prestamo;
            long oid_pago = conditions.Pago.Oid;
            conditions.Pago = null;

            query += 
			WHERE(conditions) + @"
            AND LO.""OID"" NOT IN (SELECT LO.""OID""
                                    FROM " + lo + @" AS LO
                                    INNER JOIN " + tp + @" AS PF ON PF.""OID_OPERACION"" = LO.""OID""
                                    WHERE PF.""OID_PAGO"" = " + oid_pago + ")";

            //if (lock_table) query += " FOR UPDATE OF P NOWAIT";

            return query;
        }

        internal static string SELECT_UNPAID(QueryConditions conditions, bool lockTable)
        {
            conditions.ExtraWhere = @"
                AND COALESCE(""CAPITAL_AMORTIZADO"", 0) < LO.""IMPORTE""";

            if (conditions.LoanType == ELoanType.Merchant)
                conditions.ExtraWhere += @"
                AND (LO.""FECHA_VENCIMIENTO"" BETWEEN '" + conditions.FechaAuxIniLabel + @"' AND '" + conditions.FechaAuxFinLabel + @"')";

            string query =
            SELECT(conditions, lockTable);

            conditions.ExtraWhere = string.Empty;

            return query;
        }

        #endregion
    }
}