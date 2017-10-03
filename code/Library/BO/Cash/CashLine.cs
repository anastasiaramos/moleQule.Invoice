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
	public class CashLineRecord : RecordBase
	{
		#region Attributes

		private long _oid_caja;
		private long _oid_cierre;
        private long _oid_link;
		private long _serial;
		private string _codigo = string.Empty;
		private string _n_factura = string.Empty;
		private DateTime _fecha;
		private string _concepto = string.Empty;
		private long _oid_cobro;
		private long _oid_pago;
		private string _n_tercero = string.Empty;
		private Decimal _debe;
		private Decimal _haber;
		private string _observaciones = string.Empty;
		private long _oid_cuenta_bancaria;
        private long _oid_credit_card;
		private long _estado;
		private long _tipo;  

		#endregion
		
		#region Properties
		
		public virtual long OidCaja { get { return _oid_caja; } set { _oid_caja = value; } }
		public virtual long OidCierre { get { return _oid_cierre; } set { _oid_cierre = value; } }
        public virtual long OidLink { get { return _oid_link; } set { _oid_link = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual string NFactura { get { return _n_factura; } set { _n_factura = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual string Concepto { get { return _concepto; } set { _concepto = value; } }
		public virtual long OidCobro { get { return _oid_cobro; } set { _oid_cobro = value; } }
		public virtual long OidPago { get { return _oid_pago; } set { _oid_pago = value; } }
		public virtual string NTercero { get { return _n_tercero; } set { _n_tercero = value; } }
		public virtual Decimal Debe { get { return _debe; } set { _debe = value; } }
		public virtual Decimal Haber { get { return _haber; } set { _haber = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual long OidCuentaBancaria { get { return _oid_cuenta_bancaria; } set { _oid_cuenta_bancaria = value; } }
        public virtual long OidCreditCard { get { return _oid_credit_card; } set { _oid_credit_card = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual long Tipo { get { return _tipo; } set { _tipo = value; } }

		#endregion
		
		#region Business Methods
		
		public CashLineRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			long queryType = Oid = Format.DataReader.GetInt64(source, "QUERY");

			if (queryType == (long)CashLine.ETipoQuery.GENERAL)
			{
				Oid = Format.DataReader.GetInt64(source, "OID");
				_oid_caja = Format.DataReader.GetInt64(source, "OID_CAJA");
				_oid_cierre = Format.DataReader.GetInt64(source, "OID_CIERRE");
                _oid_link = Format.DataReader.GetInt64(source, "OID_LINK");
				_serial = Format.DataReader.GetInt64(source, "SERIAL");
				_codigo = Format.DataReader.GetString(source, "CODIGO");
				_n_factura = Format.DataReader.GetString(source, "N_FACTURA");
				_fecha = Format.DataReader.GetDateTime(source, "FECHA");
				_concepto = Format.DataReader.GetString(source, "CONCEPTO");
				_oid_cobro = Format.DataReader.GetInt64(source, "OID_COBRO");
				_oid_pago = Format.DataReader.GetInt64(source, "OID_PAGO");
				_n_tercero = Format.DataReader.GetString(source, "N_PROVEEDOR");
				_debe = Format.DataReader.GetDecimal(source, "DEBE");
				_haber = Format.DataReader.GetDecimal(source, "HABER");
				_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
				_oid_cuenta_bancaria = Format.DataReader.GetInt64(source, "OID_CUENTA_BANCARIA");
                _oid_credit_card = Format.DataReader.GetInt64(source, "OID_CREDIT_CARD");
				_estado = Format.DataReader.GetInt64(source, "ESTADO");
				_tipo = Format.DataReader.GetInt64(source, "TIPO");
			}
			else if (queryType == (long)CashLine.ETipoQuery.ACUMULADO)
			{
				_debe = Format.DataReader.GetDecimal(source, "DEBE");
				_haber = Format.DataReader.GetDecimal(source, "HABER");
			}
		}		
		public virtual void CopyValues(CashLineRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_caja = source.OidCaja;
			_oid_cierre = source.OidCierre;
            _oid_link = source.OidLink;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_n_factura = source.NFactura;
			_fecha = source.Fecha;
			_concepto = source.Concepto;
			_oid_cobro = source.OidCobro;
			_oid_pago = source.OidPago;
			_n_tercero = source.NTercero;
			_debe = source.Debe;
			_haber = source.Haber;
			_observaciones = source.Observaciones;
			_oid_cuenta_bancaria = source.OidCuentaBancaria;
            _oid_credit_card = source.OidCreditCard;
			_estado = source.Estado;
			_tipo = source.Tipo;
		}
		
		#endregion	
	}

	[Serializable()]
	public class CashLineBase
	{
		#region Attributes

		private CashLineRecord _record = new CashLineRecord();

		protected string _caja = string.Empty;
		private decimal _saldo;
		private string _n_cobro;
		private string _n_pago;
		private string _cuenta_bancaria = string.Empty;
        private string _credit_card = string.Empty;
		private string _id_mov_banco = string.Empty;
		protected string _id_cierre = string.Empty;

		#endregion

		#region Properties

		public CashLineRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
		public ETipoLineaCaja ETipoLinea { get { return (ETipoLineaCaja)_record.Tipo; } set { _record.Tipo = (long)value; _record.Concepto = EnumText<ETipoLineaCaja>.GetLabel((ETipoLineaCaja)value); } }
		public string TipoLineaLabel { get { return Library.Invoice.EnumText<ETipoLineaCaja>.GetLabel(ETipoLinea); } }
		public decimal Saldo { get { return _saldo; } set { _saldo = value; } }
		public string NCobro { get { return _n_cobro; } set { _n_cobro = value; } }
		public string NPago { get { return _n_pago; } set { _n_pago = value; } }
		public string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
        public string CreditCard { get { return _credit_card; } set { _credit_card = value; } }
		public string IDMovimientoBanco { get { return _id_mov_banco; } set { _id_mov_banco = value; } }
		public bool Locked { get { return (_record.NTercero != string.Empty); } }
		public string IDCierre { get { return _id_cierre; } }
		public string Caja { get { return _caja; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			long queryType = Format.DataReader.GetInt64(source, "QUERY");

			if (queryType == (long)CashLine.ETipoQuery.GENERAL)
			{
				_caja = Format.DataReader.GetString(source, "CAJA");
				_n_pago = Format.DataReader.GetString(source, "ID_PAGO");
				_n_cobro = Format.DataReader.GetString(source, "ID_COBRO");
				_cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
                _credit_card = Format.DataReader.GetString(source, "CREDIT_CARD");
				_id_cierre = Format.DataReader.GetString(source, "ID_CIERRE");
				_id_mov_banco = Format.DataReader.GetString(source, "ID_MOVIMIENTO_BANCO");
			}
		}
		internal void CopyValues(CashLine source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_n_cobro = source.NCobro;
			_n_pago = source.NPago;
			_saldo = source.Saldo;
			_cuenta_bancaria = source.CuentaBancaria;
            _credit_card = source.CreditCard;
			_id_mov_banco = source.IDMovimientoBanco;
		}
		internal void CopyValues(CashLineInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_n_cobro = source.NCobro;
			_n_pago = source.NPago;
			_saldo = source.Saldo;
			_cuenta_bancaria = source.CuentaBancaria;
            _credit_card = source.CreditCard;
			_id_mov_banco = source.IDMovimientoBanco;
		}

		#endregion
	}	

	/// <summary>
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class CashLine : BusinessBaseEx<CashLine>, IBankLine, IEntityBase
    {
        #region IEntityBase

        public virtual DateTime FechaReferencia { get { return _base.Record.Fecha; } }

        public virtual IEntityBase ICloneAsNew() { return CloneAsNew(); }
        public virtual void ICopyValues(IEntityBase source) { _base.CopyValues((CashLine)source); }
        public void DifferentYearChecks() { }
        public void SameYearTasks(IEntityBase newItem)
        {
            CashLine obj = (CashLine)this;

            switch (ETipoLinea)
            {
                case ETipoLineaCaja.SalidaPorIngreso:
                case ETipoLineaCaja.EntradaPorTraspaso:
                    //Editamos el Movimiento de Banco asociado si lo hubiera
                    BankLine.EditItem((CashLine)newItem, obj.GetInfo(true), newItem.SessionCode);

                    break;
            }
        }
        public virtual void DifferentYearTasks(IEntityBase oldItem)
        {
            //Editamos el Movimiento de Banco asociado si lo hubiera
            BankLine.EditItem(this, ((CashLine)oldItem).GetInfo(false), SessionCode);
        }
        public virtual void IEntityBaseSave(object parent)
        {
            if (parent != null)
            {
                if (parent.GetType() == typeof(Cash))
                    Insert((Cash)parent);
                else if (parent.GetType() == typeof(CierreCaja))
                    Insert((CierreCaja)parent);
                else if (parent.GetType() == typeof(CashLines))
                    Insert((CashLines)parent);
                else
                    Save();
            }
            else
                Save();
        }

        #endregion

        #region IBankLine

		public virtual long TipoMovimiento { get { return (Debe != 0) ? (long)EBankLineType.EntradaCaja : (long)EBankLineType.SalidaCaja; } }
        public virtual EBankLineType ETipoMovimientoBanco { get { return (EBankLineType)TipoMovimiento; } }
		public virtual ETipoTitular ETipoTitular { get { return ETipoTitular.Todos; } }
        public virtual string CodigoTitular { get; set; }
        public virtual string Titular { get { return Concepto; } set {} }
        public virtual decimal Importe { get { return (Debe != 0) ? Debe : Haber; } set {} }
        public decimal GastosBancarios { get { return 0; } set { } }
		public virtual EMedioPago EMedioPago 
		{ 
			get 
			{
				return
						(Debe != 0)
							? EMedioPago.Cheque
							: (OidCuentaBancaria != 0) ? EMedioPago.Ingreso : EMedioPago.Efectivo;						 
			} 
		}
		public virtual DateTime Vencimiento { get { return _base.Record.Fecha; } set { } }
		public virtual bool Confirmado { get { return true; } }
        public virtual long OidCuenta { get { return 0; } set { } }

        public virtual IBankLineInfo IGetInfo(bool childs) { return (IBankLineInfo)GetInfo(childs); }

        #endregion

		#region Attributes
		
		protected CashLineBase _base = new CashLineBase();

        #endregion

        #region Properties

		public CashLineBase Base { get { return _base; } }
		
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
		public virtual long OidCaja
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCaja;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidCaja.Equals(value))
				{
					_base.Record.OidCaja = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidCierre
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCierre;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidCierre.Equals(value))
				{
					_base.Record.OidCierre = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual long OidLink
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidLink;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidLink.Equals(value))
                {
                    _base.Record.OidLink = value;
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
		public virtual string NFactura
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.NFactura;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.NFactura.Equals(value))
				{
					_base.Record.NFactura = value;
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
		public virtual long OidCobro
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCobro;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidCobro.Equals(value))
				{
					_base.Record.OidCobro = value;
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
        public virtual string NTercero
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.NTercero;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.NTercero.Equals(value))
                {
                    _base.Record.NTercero = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual Decimal Debe
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Debe;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Debe.Equals(value))
				{
					_base.Record.Debe = value;					
                    
					if (_base.Record.Debe > 0)
                    {
                        _base.Record.Haber = 0;
                    }
                    PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Haber
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Haber;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Haber.Equals(value))
				{
					_base.Record.Haber = value;				
                    if (_base.Record.Haber > 0)
                    {
                        _base.Record.Debe = 0;
                    }
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
        public virtual long OidCreditCard
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidCreditCard;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidCreditCard.Equals(value))
                {
                    _base.Record.OidCreditCard = value;
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
		public virtual long TipoLinea
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Tipo;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Tipo.Equals(value))
				{
					_base.Record.Tipo = value;
					PropertyHasChanged();
				}
			}
		}

        //NO ENLAZADAS
		public virtual string IDCliente { get { return _base.Record.NTercero; } }
		public virtual ETipoLineaCaja ETipoLinea { get { return _base.ETipoLinea; } set { TipoLinea = (long)value;} }
		public virtual string TipoLineaLabel { get { return _base.TipoLineaLabel; } }
		public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual decimal Saldo { get { return _base.Saldo; } set { _base.Saldo = value; } }
		public virtual string NCobro { get { return _base.NCobro; } set { _base.NCobro = value; } }
		public virtual string NPago { get { return _base.NPago; } set { _base.NPago = value; } }
		public virtual string CuentaBancaria { get { return _base.CuentaBancaria; } set { _base.CuentaBancaria = value; } }
        public virtual string CreditCard { get { return _base.CreditCard; } set { _base.CreditCard = value; } }
		public virtual string IDMovimientoBanco { get { return _base.IDMovimientoBanco; } set { _base.IDMovimientoBanco = value; } }
		public virtual bool Locked { get { return _base.Locked; } }
		public string IDCierre { get { return _base.IDCierre; } }
		public string Caja { get { return _base.Caja; } }

		#endregion
		
		#region Business Methods
		
		public virtual CashLine CloneAsNew()
		{
			CashLine clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			clon.EEstado = EEstado.Abierto;

			clon.GetNewCode(OidCaja);

			clon.SessionCode = CashLine.OpenSession();
			CashLine.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}

		protected virtual void CopyFrom(CashLineInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			OidCaja = source.OidCaja;
            OidCierre = source.OidCierre;
            OidCobro = source.OidCobro;
            OidPago = source.OidPago;
            OidLink = source.OidLink;
            Serial = source.Serial;
            Codigo = source.Codigo;
			TipoLinea = source.TipoLinea;
			Estado = source.Estado;
			Fecha = source.Fecha;
			Concepto = source.Concepto;
			Debe = source.Debe;
			Haber = source.Haber;
			Observaciones = source.Observaciones;
			NFactura = source.NFactura;

            NTercero = source.NTercero;
			NCobro = source.NCobro;
            NPago = source.NPago;
			OidCuentaBancaria = source.OidCuentaBancaria;
			CuentaBancaria = source.CuentaBancaria;
            OidCreditCard = source.OidCreditCard;
            CreditCard = source.CreditCard;
			IDMovimientoBanco = source.IDMovimientoBanco;
		}
		public virtual void CopyFrom(IBankLine source)
		{
			if (source == null) return;

			switch (source.ETipoMovimientoBanco)
			{
				case EBankLineType.PagoFactura:
								
					OidPago = source.Oid;
					ETipoLinea = ETipoLineaCaja.SalidaPorPago;
                    NTercero = source.CodigoTitular;
					Fecha = source.Vencimiento;
					Debe = 0;
					Haber = source.Importe;
					Concepto = Resources.Labels.PAGO_EN_CAJA;
					Observaciones = source.Titular;

					break;

				case EBankLineType.PagoGasto:
                case EBankLineType.Prestamo:
                case EBankLineType.PagoPrestamo:
					
					OidPago = source.Oid;
					ETipoLinea = ETipoLineaCaja.SalidaPorPago;
                    NTercero = string.Empty;
					Fecha = source.Vencimiento;
					Debe = 0;
					Haber = source.Importe;
					Concepto = Resources.Labels.GASTO_EN_CAJA;
					Observaciones = source.Observaciones;

					break;

				case EBankLineType.PagoNomina:

					OidPago = source.Oid;
					ETipoLinea = ETipoLineaCaja.SalidaPorPago;
                    NTercero = source.CodigoTitular;
					Fecha = source.Vencimiento;
					Debe = 0;
					Haber = source.Importe;
					Concepto = Resources.Labels.NOMINA_EN_CAJA;
					Observaciones = source.Titular;

					break;

				case EBankLineType.Cobro:

					OidCobro = source.Oid;
					ETipoLinea = ETipoLineaCaja.EntradaPorCobro;
                    NTercero = source.CodigoTitular;
					Fecha = source.Fecha;
					Debe = source.Importe;
					Haber = 0;
					Concepto = Resources.Labels.COBRO_EN_CAJA;
					Observaciones = source.Titular;

					break;

				case EBankLineType.Ticket:

					OidCobro = source.Oid;
					ETipoLinea = ETipoLineaCaja.EntradaPorTicket;
                    NTercero = source.CodigoTitular;
					Fecha = source.Fecha;
					Debe = source.Importe;
					Haber = 0;
					Concepto = Resources.Labels.TICKET_EN_CAJA;
					Observaciones = source.Titular;

                    break;
			}
		}

        public virtual void GetNewCode(long oidCaja)
        {
			Serial = SerialLineaCajaInfo.GetNext(oidCaja, Fecha.Year);
            Codigo = Serial.ToString(Resources.Defaults.LINEACAJA_CODE_FORMAT);
        }

		public virtual void SetLineType(ETipoLineaCaja tipo)
		{ 
			ETipoLinea = tipo;
			Concepto = EnumText<ETipoLineaCaja>.GetLabel(tipo);
		}

        public void SetLink(CashLineRecord oldCashLine)
        {
            switch (ETipoLinea)
            {
                case ETipoLineaCaja.EntradaPorTarjetaCredito:

                    //No tiene extracto asociado
                    if (OidLink == 0)
                    {
                        CreditCardStatement st = CreditCardService.GetOrCreateStatementFromOperationDate(OidCreditCard, Fecha, SessionCode);

                        if (st.OidCreditCard == 0) return;

                        st.Amount = st.Amount + Debe;
                        st.Save();

                        OidLink = st.Oid;
                    }
                    else
                    {
                        CreditCardStatement st = CreditCardService.GetOrCreateStatementFromOperationDate(OidCreditCard, Fecha, SessionCode);

                        if (st.OidCreditCard == 0) return;

                        //Se ha eliminado el pago
                        if (oldCashLine == null)
                        {
                            st.Amount = st.Amount - Debe;
                            st.Save();
                        }
                        else
                        {
                            //No ha cambiado el extracto asociado
                            if (OidLink == st.Oid)
                            {
                                st.Amount = st.Amount - oldCashLine.Debe + Debe;
                                st.Save();
                            }
                            else
                            {
                                st.Amount = st.Amount + Importe;
                                st.Save();

                                CreditCardStatement old_st = CreditCardService.GetOrCreateStatementFromOperationDate(oldCashLine.OidCreditCard, oldCashLine.Fecha, SessionCode);

                                if (st.OidCreditCard == 0) return;

                                old_st.Amount = old_st.Amount - oldCashLine.Debe;
                                old_st.Save();

                                OidLink = st.Oid;
                            }
                        }
                    }

                    break;
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
			//CuentaBancaria
			switch (ETipoLinea)
			{
				case ETipoLineaCaja.SalidaPorIngreso:
				case ETipoLineaCaja.EntradaPorTraspaso:

					if (OidCuentaBancaria == 0)
					{
						e.Description = String.Format(Resources.Messages.NO_CUENTA_BANCARIA_SELECTED_LC, Codigo);
						throw new iQValidationException(e.Description, string.Empty);
					}
					break;
			}

			return true;
		}	

		#endregion
		 
		#region Autorization Rules
		
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.FACTURA_EMITIDA);
		}		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.FACTURA_EMITIDA);
		}		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.FACTURA_EMITIDA);
		}		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.FACTURA_EMITIDA);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		protected CashLine()
		{
            MarkAsChild();

            Fecha = DateTime.Now;
			EEstado = EEstado.Abierto;
			ETipoLinea = ETipoLineaCaja.Otros;
			// Si se necesita constructor público para este objeto hay que añadir el MarkAsChild() debido a la interfaz Child
			// y el código que está en el DataPortal_Create debería ir aquí
		}

		public virtual CashLineInfo GetInfo() { return GetInfo(true); }
		public virtual CashLineInfo GetInfo (bool get_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new CashLineInfo(this, get_childs);
		}
		
		#endregion				
		
		#region Root Factory Methods

		public static void DeleteByAlbaranList(List<OutputDeliveryInfo> list, long oid_caja)
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = AlbaranTicket.OpenSession();
			ISession sess = AlbaranTicket.Session(sessCode);
			ITransaction trans = AlbaranTicket.BeginTransaction(sessCode);

			try
			{
				List<long> oids = new List<long>();

				oids.Add(0);

				foreach (OutputDeliveryInfo item in list)
					oids.Add(item.Oid);
				
				nHManager.Instance.SQLNativeExecute(DELETE_BY_ALBARAN(oids, oid_caja));

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
        public static CashLine Get(long oid, bool childs = false)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = CashLine.GetCriteria(CashLine.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = CashLine.SELECT(oid);

            CashLine.BeginTransaction(criteria.Session);

            return DataPortal.Fetch<CashLine>(criteria);
        }

		#endregion

		#region Child Factory Methods

        private CashLine(Cash parent)
        {
			MarkAsChild();

            OidCaja = parent.Oid;
			Fecha = DateTime.Now;
			EEstado = EEstado.Abierto;
			ETipoLinea = ETipoLineaCaja.Otros;
        }
		private CashLine(CashLine source, bool childs)
		{
			MarkAsChild();
			Childs = childs;
			Fetch(source);
		}
		private CashLine(int sessionCode, IDataReader source, bool childs)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(source);
		}

		public static CashLine NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return DataPortal.Create<CashLine>(new CriteriaCs(-1));
		}
		internal static CashLine NewChild(Cash parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return new CashLine(parent);
		}

		internal static CashLine GetChild(CashLine source) { return new CashLine(source, false); }
		internal static CashLine GetChild(CashLine source, bool childs) { return new CashLine(source, childs); }
		internal static CashLine GetChild(int sessionCode, IDataReader source) { return new CashLine(sessionCode, source, false); }
		internal static CashLine GetChild(int sessionCode, IDataReader source, bool childs)	{ return new CashLine(sessionCode, source, childs); }
		
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
		public override CashLine Save()
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
			// El código va al constructor porque los DataGrid no llamana al DataPortal sino directamente al constructor		
		}

        // called to retrieve data from the database
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                _base.Record.Oid = 0;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
        }
		
		private void Fetch(CashLine source)
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

		internal void Insert(CashLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
                GetNewCode(1);

				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SetLink(Base.Record);

				parent.Session().Save(Base.Record);

				//Insertamos el movimiento de banco asociado
				BankLine.InsertItem(this, SessionCode);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
	
		internal void Update(CashLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				CashLineRecord record = Session().Get<CashLineRecord>(Oid);

                SetLink(record);

				CashLine old = CashLine.NewChild();
				old.Base.Record.CopyValues(record);
				CashLineInfo oldItem = GetInfo(false);

				record.CopyValues(Base.Record);
				Session().Update(record);

				//Editamos el Movimiento de Banco asociado si lo hubiera
				BankLine.EditItem(this, oldItem, SessionCode);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
		
		internal void DeleteSelf(CashLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				CashLine obj = parent.GetItem(Oid);
				Session().Delete(Session().Get<CashLineRecord>(Oid));

                SetLink(null);

				//Anulamos el Movimiento de Banco si lo hubiera
				BankLine.AnulaItem(obj, SessionCode);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}

		#endregion
		
		#region Child Data Access
		
		internal void Insert(Cash parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidCaja = parent.Oid;	
			
			try
			{
				ValidationRules.CheckRules();
				
				if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SetLink(Base.Record);

				parent.Session().Save(Base.Record);

				switch (ETipoLinea)
				{
					case ETipoLineaCaja.SalidaPorIngreso:
					case ETipoLineaCaja.EntradaPorTraspaso:
						//Editamos el Movimiento de Banco asociado si lo hubiera
						BankLine.InsertItem(this, SessionCode);

						break;
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
#if TRACE
			ControlerBase.AppControler.Timer.Record("LineaCaja.Insert");
#endif	
		}

		internal void Update(Cash parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidCaja = parent.Oid;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                CashLine obj = null;

                try
                {
                    CashLineRecord record = parent.Session().Get<CashLineRecord>(this.Oid);

                    SetLink(record);

                    obj = CashLine.Get(this.Oid, false, SessionCode);

                    if (EntityBase.UpdateByYear(obj, this, parent))
                    {
                        obj.Save();
                        parent.Transaction().Commit();
                        //parent.CloseSession();
                        parent.NewTransaction();
                    }
                    else
                    {
                        record.CopyValues(this.Base.Record);
                        parent.Session().Update(record);
                        //obj.CloseSession();
                    }
                }
                catch (Exception ex)
                {
                    //if (obj != null) obj.CloseSession();
                    throw ex;
                }
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();

#if TRACE
			ControlerBase.AppControler.Timer.Record("LineaCaja.Update");
#endif	
		}

		internal void DeleteSelf(Cash parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				CashLine obj = parent.Lines.GetItem(Oid);

                SetLink(null);

				Session().Delete(Session().Get<CashLineRecord>(Oid));

				//Anulamos el Movimiento de Banco si lo hubiera
				BankLine.AnulaItem(obj, SessionCode);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkNew();
		}

        internal void Insert(CierreCaja parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

            OidCierre = parent.Oid;

			if (EEstado != EEstado.Anulado) EEstado = EEstado.Closed;

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
#if TRACE
			ControlerBase.AppControler.Timer.Record("LineaCaja.Insert");
#endif
        }

        internal void Update(CierreCaja parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

			OidCierre = parent.Oid;

			if (EEstado != EEstado.Anulado) EEstado = EEstado.Closed;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				CashLineRecord obj = parent.Session().Get<CashLineRecord>(Oid);
                obj.CopyValues(Base.Record);
                parent.Session().Update(obj);

                //Editamos el Movimiento de Banco asociado si lo hubiera
                BankLine.EditItem(this, GetInfo(false), SessionCode);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();

#if TRACE
			ControlerBase.AppControler.Timer.Record("LineaCaja.Update");
#endif
        }

        internal void DeleteSelf(CierreCaja parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
				CashLine obj = parent.LineaCajas.GetItem(Oid);
                Session().Delete(Session().Get<CashLineRecord>(Oid));

				//Anulamos el Movimiento de Banco si lo hubiera
				BankLine.AnulaItem(obj, SessionCode);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }
		
		#endregion		

        #region SQL

        public new static string SELECT(long oid) { return SELECT(new QueryConditions { Oid = oid }); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
		public static string UPDATE_LIBERA_LINEAS_A(QueryConditions conditions)
		{
			string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));

			string query = @"
            UPDATE " + lc + @" AS LC SET ""ESTADO"" = " + (long)EEstado.Abierto +
			WHERE(conditions) + @"
			    AND LC.""ESTADO"" != " + (long)EEstado.Anulado + @";";

			return query;
		}
		public static string UPDATE_LIBERA_LINEAS_B(QueryConditions conditions)
		{
			string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));

			string query = @"
            UPDATE " + lc + @" AS LC SET ""OID_CIERRE"" = 0" +
			WHERE(conditions) + @";";

			return query;
		}

		internal enum ETipoQuery { GENERAL = 0, ACUMULADO = 1 }

		internal static string FIELDS()
        {
            string query;

			query = @"
            SELECT " + (long)ETipoQuery.GENERAL + @" AS ""QUERY""
                    ,LC.*
                    ,CA.""NOMBRE"" AS ""CAJA""
                    ,COALESCE(CB.""CODIGO"", COALESCE(TK.""CODIGO"", '')) AS ""ID_COBRO""
                    ,COALESCE(PG.""CODIGO"", '') AS ""ID_PAGO""
                    ,COALESCE(CCO.""CODIGO"", '') AS ""ID_CIERRE""
                    ,COALESCE(CTB.""VALOR"", '') AS ""CUENTA_BANCARIA""
                    ,COALESCE(CC.""NOMBRE"", '') AS ""CREDIT_CARD""
                    ,COALESCE(MV.""CODIGO"", '') AS ""ID_MOVIMIENTO_BANCO""";

            return query;
        }

		internal static string FIELDS_ACUMULADO()
		{
			string query;

			query = @"
            SELECT " + (long)ETipoQuery.ACUMULADO + @" AS ""QUERY""
                    ,SUM (LC.""DEBE"") AS ""DEBE""
                    ,SUM (LC.""HABER"") AS ""HABER""";

			return query;
		}

        internal static string WHERE(QueryConditions conditions)
        {
            string query;

            query = @"
            WHERE (LC.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            query += EntityBase.NO_NULL_RECORDS_CONDITION("LC");

            if (conditions.Cobro != null)
                query += @"
                    AND LC.""OID_COBRO"" = " + conditions.Cobro.Oid;

            if (conditions.Pago != null)
                query += @"
                    AND LC.""OID_PAGO"" = " + conditions.Pago.Oid;

            if (conditions.Caja != null)
                query += @"
                    AND LC.""OID_CAJA"" = " + conditions.Caja.Oid;

            if (conditions.CierreCaja != null)
                query += @"
                    AND LC.""OID_CIERRE"" = " + conditions.CierreCaja.Oid;

            if (conditions.Oid != 0)
                query += @"
                    AND LC.""OID"" = " + conditions.Oid;

            return query + " " + conditions.ExtraWhere;
        }

        internal static string JOIN(QueryConditions conditions, bool lockTable)
        {
			string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));
			string ca = nHManager.Instance.GetSQLTable(typeof(CashRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
			string tk = nHManager.Instance.GetSQLTable(typeof(TicketRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));
            string ctb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
			string cco = nHManager.Instance.GetSQLTable(typeof(CashCountRecord));
			string bl = nHManager.Instance.GetSQLTable(typeof(BankLineRecord));
            string cc = nHManager.Instance.GetSQLTable(typeof(CreditCardRecord));

            string query;

			query = @"
            FROM " + lc + @" AS LC
            INNER JOIN " + ca + @" AS CA ON CA.""OID"" = LC.""OID_CAJA""
            LEFT JOIN " + cco + @" AS CCO ON CCO.""OID"" = LC.""OID_CIERRE""
            LEFT JOIN " + cb + @" AS CB ON CB.""OID"" = LC.""OID_COBRO"" AND LC.""TIPO"" = " + (long)ETipoLineaCaja.EntradaPorCobro + @"
            LEFT JOIN " + tk + @" AS TK ON TK.""OID"" = LC.""OID_COBRO"" AND LC.""TIPO"" = " + (long)ETipoLineaCaja.EntradaPorTicket + @"
            LEFT JOIN " + pa + @" AS PG ON PG.""OID"" = LC.""OID_PAGO""
            LEFT JOIN " + ctb + @" AS CTB ON CTB.""OID"" = LC.""OID_CUENTA_BANCARIA""
            LEFT JOIN " + cc + @" AS CC ON CC.""OID"" = LC.""OID_CREDIT_CARD""
            LEFT JOIN " + bl + @" AS MV ON MV.""OID_OPERACION"" = LC.""OID"" AND MV.""TIPO_OPERACION"" IN (" + (long)EBankLineType.EntradaCaja + "," + (long)EBankLineType.SalidaCaja + @")";

            return query + " " + conditions.ExtraJoin;
        }

        public static string JOIN_CREDIT_CARD_STATEMENTS(Library.QueryConditions conditions, string tableAlias)
        {
            string cal = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));

            string query = @"
			LEFT JOIN (SELECT CAL.""OID_LINK""
							,SUM(CAL.""DEBE"") AS ""CASH_AMOUNT""
						FROM " + cal + @" AS CAL
						WHERE CAL.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY CAL.""OID_LINK"")
                AS CAL ON CAL.""OID_LINK"" = " + tableAlias + @".""OID""";

            return query;
        }
        
        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query =
            FIELDS() +
            JOIN(conditions, true) +
            WHERE(conditions);

            query += @"
            ORDER BY LC.""FECHA"", LC.""CODIGO""";

            //query += EntityBase.LOCK("LC", lockTable);

            return query;
        }

        internal static string SELECT_BY_CREDIT_CARD_STATEMENT(QueryConditions conditions, bool lockTable)
        {
            string css = nHManager.Instance.GetSQLTable(typeof(CreditCardStatementRecord));

            conditions.ExtraJoin = @"
            INNER JOIN " + css + @" AS CSS ON CSS.""OID"" = LC.""OID_LINK""";

            if (conditions.CreditCardStatement != null)
                conditions.ExtraWhere = @"
                    AND LC.""OID_LINK"" = " + conditions.CreditCardStatement.Oid;

            string query =
            FIELDS() +
            JOIN(conditions, true) +
            WHERE(conditions);

            query += @"
            ORDER BY LC.""FECHA"", LC.""CODIGO""";

            //query += EntityBase.LOCK("LC", lockTable);

            return query;
        }
		
        internal static string SELECT_ACUMULADO(QueryConditions conditions)
		{
            string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));
			string ca = nHManager.Instance.GetSQLTable(typeof(CashRecord));
            
            string query = 
            FIELDS_ACUMULADO() + @"
            FROM " + lc + @" AS LC
            INNER JOIN " + ca + @" AS CA ON LC.""OID_CAJA"" = CA.""OID""";

			query += @"
            WHERE (LC.""FECHA"" < '" + conditions.FechaIniLabel + @"')
                AND LC.""OID_CAJA"" = " + conditions.Caja.Oid + @"
                AND LC.""ESTADO"" != " + (long)EEstado.Anulado;

			return query;
		}

		internal static string DELETE_BY_ALBARAN(List<long> oidList, long oidCaja)
		{
			string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));
			string at = nHManager.Instance.GetSQLTable(typeof(DeliveryTicketRecord));

			string query = @"
            DELETE FROM " + lc + @" AS LC
            WHERE LC.""OID_COBRO"" IN (SELECT AT.""OID_TICKET""
                                        FROM " + at + @" AS AT
                                        WHERE AT.""OID_ALBARAN"" IN " + EntityBase.GET_IN_STRING(oidList) + @")
                AND LC.""OID_CAJA"" = " + oidCaja + @"
                AND LC.""OID_CIERRE"" = 0";

			return query;
		}
        
		#endregion
    }
}