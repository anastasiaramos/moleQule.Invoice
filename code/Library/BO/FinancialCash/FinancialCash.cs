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

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class FinancialCashRecord : RecordBase
	{
		#region Attributes

        private long _serial;
        private string _codigo = string.Empty;
		private long _oid_cobro;
        private long _oid_cuenta_bancaria;
		private DateTime _fecha_emision;
		private DateTime _vencimiento;
        private DateTime _return_date;
		private bool _negociado = false;
		private Decimal _gastos_adelanto;
		private Decimal _gastos_devolucion;
		private Decimal _gastos;
		private long _estado;
		private long _estado_cobro;
        private string _observaciones = string.Empty;
		private DateTime _charge_date;
  
		#endregion
		
		#region Properties

        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long OidCobro { get { return _oid_cobro; } set { _oid_cobro = value; } }
        public virtual long OidCuentaBancaria { get { return _oid_cuenta_bancaria; } set { _oid_cuenta_bancaria = value; } }
		public virtual DateTime FechaEmision { get { return _fecha_emision; } set { _fecha_emision = value; } }
		public virtual DateTime Vencimiento { get { return _vencimiento; } set { _vencimiento = value; } }
        public virtual DateTime ReturnDate { get { return _return_date; } set { _return_date = value; } }
		public virtual bool Negociado { get { return _negociado; } set { _negociado = value; } }
		public virtual Decimal GastosNegociado { get { return _gastos_adelanto; } set { _gastos_adelanto = value; } }
		public virtual Decimal GastosDevolucion { get { return _gastos_devolucion; } set { _gastos_devolucion = value; } }
		public virtual Decimal Gastos { get { return _gastos; } set { _gastos = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual long EstadoCobro { get { return _estado_cobro; } set { _estado_cobro = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual DateTime ChargeDate { get { return _charge_date; } set { _charge_date = value; } }

		#endregion
		
		#region Business Methods
		
		public FinancialCashRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
			_oid_cobro = Format.DataReader.GetInt64(source, "OID_COBRO");
            _oid_cuenta_bancaria = Format.DataReader.GetInt64(source, "OID_CUENTA_BANCARIA");
			_fecha_emision = Format.DataReader.GetDateTime(source, "FECHA_EMISION");
			_vencimiento = Format.DataReader.GetDateTime(source, "VENCIMIENTO");
			_negociado = Format.DataReader.GetBool(source, "ADELANTADO");
			_gastos_adelanto = Format.DataReader.GetDecimal(source, "GASTOS_ADELANTO");
			_gastos_devolucion = Format.DataReader.GetDecimal(source, "GASTOS_DEVOLUCION");
			_gastos = Format.DataReader.GetDecimal(source, "GASTOS");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_estado_cobro = Format.DataReader.GetInt64(source, "ESTADO_COBRO");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_charge_date = Format.DataReader.GetDateTime(source, "CHARGE_DATE");
            _return_date = Format.DataReader.GetDateTime(source, "RETURN_DATE");
		}		
		public virtual void CopyValues(FinancialCashRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
            _serial = source.Serial;
            _codigo = source.Codigo;
			_oid_cobro = source.OidCobro;
            _oid_cuenta_bancaria = source.OidCuentaBancaria;
			_fecha_emision = source.FechaEmision;
			_vencimiento = source.Vencimiento;
			_negociado = source.Negociado;
			_gastos_adelanto = source.GastosNegociado;
			_gastos_devolucion = source.GastosDevolucion;
			_gastos = source.Gastos;
			_estado = source.Estado;
			_estado_cobro = source.EstadoCobro;
            _observaciones = source.Observaciones;
			_charge_date = source.ChargeDate;
            _return_date = source.ReturnDate;
		}

		#endregion	
	}

    [Serializable()]
	public class FinancialCashBase 
	{	 
		#region Attributes
		
		private FinancialCashRecord _record = new FinancialCashRecord();

        private string _id_cobro = string.Empty;
        private Decimal _importe;
        private string _id_cliente = string.Empty;
        private string _cliente = string.Empty;
        private long _tipo_cobro;
        private string _cuenta_bancaria = string.Empty;
        private string _entidad = string.Empty;
        private string _id_mov_banco = string.Empty;
		
		#endregion
		
		#region Properties

        public FinancialCashRecord Record { get { return _record; } set { _record = value; } }

        public string IdCobro { get { return _id_cobro; } set { _id_cobro = value; } }
        public Decimal Importe { get { return Decimal.Round(_importe, 2); } set { _importe = value; } }
        public string Cliente { get { return _cliente; } set { _cliente = value; } }
        public string IdCliente { get { return _id_cliente; } set { _id_cliente = value; } }
        public long TipoCobro { get { return _tipo_cobro; } set { _tipo_cobro = value; } }
        public string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
        public string Entidad { get { return _entidad; } set { _entidad = value; } }
        public string IdMovBanco { get { return _id_mov_banco; } set { _id_mov_banco = value; } }

        public EEstado EStatus { get { return (EEstado)_record.Estado; } }
        public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
        public EEstado EEstadoCobro { get { return (EEstado)_record.EstadoCobro; } set { _record.EstadoCobro = (long)value; } }
        public string EstadoCobroLabel { get { return Common.EnumText<EEstado>.GetLabel(EEstadoCobro); } }
        public EMedioPago ETipoCobro { get { return (EMedioPago)_tipo_cobro; } set { TipoCobro = (long)value; } }
        public string TipoCobroLabel { get { return Library.Common.EnumText<EMedioPago>.GetLabel(ETipoCobro); } }
		
		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);

            _id_cobro = Format.DataReader.GetString(source, "ID_COBRO");
            _importe = Format.DataReader.GetDecimal(source, "IMPORTE");
            _cliente = Format.DataReader.GetString(source, "CLIENTE");
            _id_cliente = Format.DataReader.GetString(source, "ID_CLIENTE");
            _tipo_cobro = Format.DataReader.GetInt64(source, "TIPO_COBRO");
            _cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
            _entidad = Format.DataReader.GetString(source, "ENTIDAD");
            _id_mov_banco = Format.DataReader.GetString(source, "ID_MOVIMIENTO_BANCO");
		}
		
		public void CopyValues(FinancialCash source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base._record);

            _id_cobro = source.IdCobro;
            _importe = source.Importe;
            _cliente = source.Cliente;
            _id_cliente = source.IdCliente;
            _tipo_cobro = source.TipoCobro;
            _cuenta_bancaria = source.CuentaBancaria;
            _entidad = source.Entidad;
            _id_mov_banco = source.IdMovBanco;
		}
		public void CopyValues(FinancialCashInfo source)
		{
			if (source == null) return;

            _record.CopyValues(source.Base._record);

            _id_cobro = source.IdCobro;
            _importe = source.Importe;
            _cliente = source.Cliente;
            _id_cliente = source.IdCliente;
            _tipo_cobro = source.TipoCobro;
            _cuenta_bancaria = source.CuentaBancaria;
            _entidad = source.Entidad;
            _id_mov_banco = source.IdMovBanco;
		}
		#endregion	
	}
		
	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class FinancialCash : BusinessBaseEx<FinancialCash>, IBankLine, IEntityBase
    {
        #region IEntityBase

        public virtual DateTime FechaReferencia { get { return Vencimiento; } }

        public virtual IEntityBase ICloneAsNew() { return CloneAsNew(); }
        public virtual void ICopyValues(IEntityBase source) { _base.CopyValues((FinancialCash)source); }
        public void DifferentYearChecks() { }
        public void SameYearTasks(IEntityBase newItem)
        {
            FinancialCash obj = (FinancialCash)this;

            //El único cambio que se ha producido es que se ha exportado el efecto, no hay que 
            //realizar cambios en los movimientos
            if (newItem.EEstado == EEstado.Exportado &&
                EEstado != EEstado.Exportado)
                return;

            //Editamos o Anulamos los Movimientos de Banco si los hubiera
            BankLine.EditItem((FinancialCash)newItem, obj.GetInfo(true), newItem.SessionCode);
        }

        public void DifferentYearTasks(IEntityBase oldItem)
        {
            BankLine.EditItem((FinancialCash)this, ((FinancialCash)oldItem).GetInfo(false), SessionCode);
        }
        public virtual void IEntityBaseSave(object parent = null)
        {
            Save();
        }

        #endregion

        #region IBankLine

        public long TipoMovimiento { get { return (long)EBankLineType.CobroEfecto; } }
        public EBankLineType ETipoMovimientoBanco { get { return EBankLineType.CobroEfecto; } }
        public ETipoTitular ETipoTitular { get { return EnumConvert.ToETipoTitular(Invoice.ETipoCobro.Cliente); } }
        public string CodigoTitular { get { return IdCliente; } set { } }
        public string Titular { get { return Cliente; } set { } }
        public decimal GastosBancarios { get { return 0; } set { } }
        public long OidCuenta { get { return OidCuentaBancaria; } set { } }
        public bool Confirmado { get { return EEstadoCobro == EEstado.Charged; } set { } }
        public DateTime Fecha { get { return Vencimiento; } set { Vencimiento = value; } }
        public EMedioPago EMedioPago { get { return ETipoCobro; } }

        public virtual IBankLineInfo IGetInfo(bool childs) { return (IBankLineInfo)GetInfo(childs); }

        #endregion

		#region Attributes
		
		private FinancialCashBase _base = new FinancialCashBase();		

		#endregion
		
		#region Properties

        public FinancialCashBase Base { get { return _base; } set { _base = value; } }

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
        public virtual long Serial
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Serial;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

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
		public virtual long OidCobro
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
                return _base.Record.OidCobro;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

                if (!_base.Record.OidCobro.Equals(value))
				{
                    _base.Record.OidCobro = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual long OidCuentaBancaria
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.OidCuentaBancaria;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.OidCuentaBancaria.Equals(value))
                {
                    _base.Record.OidCuentaBancaria = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual DateTime FechaEmision
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
                return _base.Record.FechaEmision;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

                if (!_base.Record.FechaEmision.Equals(value))
				{
                    _base.Record.FechaEmision = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Vencimiento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
                return _base.Record.Vencimiento;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

                if (!_base.Record.Vencimiento.Equals(value))
				{
                    _base.Record.Vencimiento = value;
					if (!Adelantado) ChargeDate = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Adelantado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
                return _base.Record.Negociado;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

                if (!_base.Record.Negociado.Equals(value))
				{
                    _base.Record.Negociado = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal GastosAdelanto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
                return _base.Record.GastosNegociado;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

                if (!_base.Record.GastosNegociado.Equals(value))
				{
                    _base.Record.GastosNegociado = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal GastosDevolucion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
                return _base.Record.GastosDevolucion;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

                if (!_base.Record.GastosDevolucion.Equals(value))
				{
                    _base.Record.GastosDevolucion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Gastos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
                return _base.Record.Gastos;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

                if (!_base.Record.Gastos.Equals(value))
				{
                    _base.Record.Gastos = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Estado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
                return _base.Record.Estado;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

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
				CanReadProperty(true);
                return _base.Record.EstadoCobro;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

                if (!_base.Record.EstadoCobro.Equals(value))
				{
                    _base.Record.EstadoCobro = value;
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
		public virtual DateTime ChargeDate
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.Record.ChargeDate;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

				if (!_base.Record.ChargeDate.Equals(value))
				{
					_base.Record.ChargeDate = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual DateTime ReturnDate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.ReturnDate;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.ReturnDate.Equals(value))
                {
                    _base.Record.ReturnDate = value;
                    PropertyHasChanged();
                }
            }
        }

        public virtual string IdCobro
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.IdCobro;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.IdCobro.Equals(value))
                {
                    _base.IdCobro = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Importe
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Importe;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Importe.Equals(value))
                {
                    _base.Importe = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Cliente
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Cliente;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Cliente.Equals(value))
                {
                    _base.Cliente = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string IdCliente
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.IdCliente;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.IdCliente.Equals(value))
                {
                    _base.IdCliente = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long TipoCobro
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.TipoCobro;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.TipoCobro.Equals(value))
                {
                    _base.TipoCobro = value;
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
                return _base.CuentaBancaria;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.CuentaBancaria.Equals(value))
                {
                    _base.CuentaBancaria = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Entidad
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Entidad;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Entidad.Equals(value))
                {
                    _base.Entidad = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string IdMovBanco
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.IdMovBanco;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.IdMovBanco.Equals(value))
                {
                    _base.IdMovBanco = value;
                    PropertyHasChanged();
                }
            }
        }

        public EEstado EEstado { get { return (EEstado)Estado; } set { Estado = (long)value; } }
        public string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
        public EEstado EEstadoCobro { get { return (EEstado)EstadoCobro; } set { EstadoCobro = (long)value; } }
        public string EstadoCobroLabel { get { return Common.EnumText<EEstado>.GetLabel(EEstadoCobro); } }
        public EMedioPago ETipoCobro { get { return (EMedioPago)TipoCobro; } set { TipoCobro = (long)value; } }
        public string TipoCobroLabel { get { return Library.Common.EnumText<EMedioPago>.GetLabel(ETipoCobro); } }			
		
		#endregion
		
		#region Business Methods
		
		public virtual FinancialCash CloneAsNew()
		{
			FinancialCash clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.SessionCode = FinancialCash.OpenSession();
			FinancialCash.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}

        public virtual void GetNewCode()
        {
            Serial = SerialEffectInfo.GetNext(Vencimiento.Year);

            Codigo = Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
        }
		
		protected void CopyValues(FinancialCashInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
		}
		
		protected virtual void CopyFrom(FinancialCashInfo source)
		{
			if (source == null) return;

            _base.CopyValues(source);
		}
        public virtual void CopyFrom(ChargeInfo source)
        {
            OidCobro = source.Oid;
            Importe = source.Importe;
            Gastos = source.GastosBancarios;
            FechaEmision = source.Fecha;
            Vencimiento = source.Vencimiento;
            Cliente = source.Cliente;
            IdCliente = source.IDCliente;
            IdCobro = source.Codigo;
            ETipoCobro = source.EMedioPago;
            OidCuentaBancaria = source.OidCuentaBancaria;
            CuentaBancaria = source.CuentaBancaria;
            Entidad = source.Entidad;
            EEstadoCobro = Common.EEstado.Pendiente;
			ChargeDate = source.Vencimiento;
        }

        public static FinancialCash ChangeEstado(long oid, EEstado estado)
        {
            if (!CanChangeState())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            FinancialCash item = null;

            try
            {
                item = FinancialCash.Get(oid);
                FinancialCashInfo oldItem = item.GetInfo();

                if ((item.EEstado == EEstado.Contabilizado || item.EEstado == EEstado.Exportado) && (!AutorizationRulesControler.CanEditObject(Resources.SecureItems.CUENTA_CONTABLE)))
                    throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

                EntityBase.CheckChangeState(item.EEstado, estado);

                item.BeginEdit();
                item.EEstado = estado;

                if (estado == EEstado.Anulado)
                    BankLine.EditItem(item, oldItem, item.SessionCode);

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

            //Cuenta Bancaria
            if (EEstadoCobro != EEstado.Pendiente && OidCuentaBancaria == 0)
            {
                e.Description = Resources.Messages.NO_CUENTA_BANCARIA_SELECTED;
                throw new iQValidationException(e.Description, string.Empty);
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
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected FinancialCash () {}
				
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private FinancialCash(FinancialCash source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private FinancialCash(int sessionCode, IDataReader source, bool childs)
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
		public static FinancialCash NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			FinancialCash obj = DataPortal.Create<FinancialCash>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">IVEffect con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(IVEffect source, bool childs)
		/// <remarks/>
		internal static FinancialCash GetChild(FinancialCash source) { return new FinancialCash(source, false); }
		internal static FinancialCash GetChild(FinancialCash source, bool childs) { return new FinancialCash(source, childs); }
        internal static FinancialCash GetChild(int sessionCode, IDataReader source) { return new FinancialCash(sessionCode, source, false); }
        internal static FinancialCash GetChild(int sessionCode, IDataReader source, bool childs) { return new FinancialCash(sessionCode, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual FinancialCashInfo GetInfo() { return GetInfo(true); }	
		public virtual FinancialCashInfo GetInfo (bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new FinancialCashInfo(this, childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static FinancialCash New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<FinancialCash>(new CriteriaCs(-1));
		}

        public static FinancialCash New(ChargeInfo source, int sessionCode)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            FinancialCash obj = DataPortal.Create<FinancialCash>(new CriteriaCs(-1));
            obj.CopyFrom(source);
            obj.SessionCode = sessionCode;
            obj.SharedTransaction = true;
            //Obtenemos un nuevo código por si cambia el año de la fecha
            obj.GetNewCode();

            return obj;
        }
		
		public static FinancialCash Get(long oid) { return Get(oid, true); }
		public static FinancialCash Get(long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = GetCriteria(OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(oid);
				
			BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<FinancialCash>(criteria);
		}
        public static FinancialCash GetByCobro(ChargeInfo cobro, bool childs = false, int sessionCode = -1)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
            
            return Get(SELECT(new QueryConditions() { Cobro = cobro }, true), childs, sessionCode);
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
		/// Elimina todos los IVEffect. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = FinancialCash.OpenSession();
			ISession sess = FinancialCash.Session(sessCode);
			ITransaction trans = FinancialCash.BeginTransaction(sessCode);
			
			try
			{
				sess.Delete("from EffectRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				FinancialCash.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override FinancialCash Save()
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
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(FinancialCash source)
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
		internal void Insert(FinancialCashes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			GetNewCode();
		
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);

            //Insertamos la linea de caja y el movimiento de banco asociado
            BankLine.InsertItem(this, SessionCode);
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(FinancialCashes parent)
		{
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            ValidationRules.CheckRules();

            SessionCode = parent.SessionCode;

            FinancialCash obj = null;

            try
            {
                FinancialCashRecord record = Session().Get<FinancialCashRecord>(this.Oid);
                obj = FinancialCash.Get(Oid, true, SessionCode);

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
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(FinancialCashes parent)
		{
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                FinancialCashRecord obj = parent.Session().Get<FinancialCashRecord>(Oid);
                obj.CopyValues(Base.Record);

                parent.Session().Update(obj);

                //Nunca se borran, se anulan
                EEstado = EEstado.Anulado;

                //Anulamos los Movimientos de Banco si los hubiera
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
					//IVEffect.DoLOCK(Session());
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

			//Borrar si no hay código
            GetNewCode();

            Session().Save(Base.Record);

            //Insertamos los movimientos de banco asociados
            BankLine.InsertItem(this, SessionCode);
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
                FinancialCash obj = null;

                try
                {
                    FinancialCashRecord record = Session().Get<FinancialCashRecord>(this.Oid);
                    obj = FinancialCash.Get(Oid, true, SessionCode);

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
                FinancialCash obj = FinancialCash.Get(criteria.Oid);

                //Nunca se borran, se anulan
                obj.EEstado = EEstado.Anulado;

                //Anulamos los Movimientos de Banco si los hubiera
                BankLine.AnulaItem(obj, obj.SessionCode);

                obj.Save();
                obj.CloseSession();
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

        public new static string SELECT(long oid) { return SELECT(oid, true); }
        public static string SELECT(long oid, bool lockTable)
        {
            QueryConditions conditions = new QueryConditions { Effect = FinancialCashInfo.New(oid) };

            string query =
            SELECT(conditions, lockTable);

            return query;
        }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
        internal static string FIELDS()
        {
            string query;

            query = @"
            SELECT EF.*
                    ,COALESCE(CH.""CODIGO"", '') AS ""ID_COBRO""
                    ,COALESCE(CH.""IMPORTE"", 0) AS ""IMPORTE""
                    ,COALESCE(CH.""MEDIO_PAGO"", 0) AS ""TIPO_COBRO""
                    ,COALESCE(CB.""VALOR"", '') AS ""CUENTA_BANCARIA""
                    ,COALESCE(CB.""ENTIDAD"", '') AS ""ENTIDAD""
                    ,COALESCE(CL.""CODIGO"", '') AS ""ID_CLIENTE""
			        ,COALESCE(CL.""NOMBRE"", '') AS ""CLIENTE""
			        ,COALESCE(MV.""CODIGO"", '') AS ""ID_MOVIMIENTO_BANCO""";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = @"
            WHERE (EF.""FECHA_EMISION"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            if (conditions.Effect != null)
                if (conditions.Effect.Oid != 0)
                    query += @"
                    AND EF.""OID"" = " + conditions.Effect.Oid;

            if (conditions.Cobro != null && conditions.Cobro.Oid != 0)
                query += @"
                AND EF.""OID_COBRO"" = " + conditions.Cobro.Oid;					

			return query + " " + conditions.ExtraWhere;
		}

        internal static string SELECT_BASE(QueryConditions conditions)
        {
            string query;

            query = FIELDS() +
                    JOIN_BASE(conditions);

            return query;
        }

        internal static string JOIN_BASE(QueryConditions conditions)
        {
            string ef = nHManager.Instance.GetSQLTable(typeof(FinancialCashRecord));
            string ch = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
            string mv = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));

            string query;

            query = @"
                FROM " + ef + @" AS EF
				LEFT JOIN " + ch + @" AS CH ON CH.""OID"" = EF.""OID_COBRO""
                LEFT JOIN " + cl + @" AS CL ON CL.""OID"" = CH.""OID_CLIENTE""
                LEFT JOIN " + cb + @" AS CB ON CB.""OID"" = EF.""OID_CUENTA_BANCARIA""
                LEFT JOIN " + mv + @" AS MV ON MV.""OID_OPERACION"" = EF.""OID"" AND MV.""ESTADO"" != " + (long)EEstado.Anulado + @" AND MV.""TIPO_OPERACION"" = " + (long)EBankLineType.CobroEfecto;

            return query + conditions.ExtraJoin;
        }
	
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {            
			string query;

            query = 
            SELECT_BASE(conditions) +
            WHERE(conditions) + @"
            ORDER BY ""VENCIMIENTO"", ""CODIGO""";
		
			//if (lock_table) query += " FOR UPDATE OF I NOWAIT";

            return query;
        }

        internal static string SELECT_NEGOCIADOS(QueryConditions conditions, bool lockTable)
        {
            conditions.ExtraWhere = @"
                AND EF.""ADELANTADO"" = TRUE
                AND EF.""VENCIMIENTO"" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + @"'
                AND EF.""ESTADO_COBRO"" != " + (long)EEstado.Devuelto;

            string query =
            SELECT_BASE(conditions) +
            WHERE(conditions) + @"
            ORDER BY ""VENCIMIENTO"", ""CODIGO""";

            //if (lock_table) query += " FOR UPDATE OF I NOWAIT";

            conditions.ExtraWhere = string.Empty;

            return query;
        }

		#endregion
	}
}