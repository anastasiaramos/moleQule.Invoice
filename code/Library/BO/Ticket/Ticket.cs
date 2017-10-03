using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

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
	public class TicketRecord : RecordBase
	{
		#region Attributes

		private long _oid_serie;
		private long _oid_tpv;
		private long _serial;
		private string _codigo = string.Empty;
		private long _estado;
		private long _tipo;
		private DateTime _fecha;
		private Decimal _base_imponible;
		private Decimal _impuestos;
		private Decimal _descuento;
		private Decimal _total;
		private long _forma_pago;
		private long _dias_pago;
		private long _medio_pago;
		private DateTime _prevision_pago;
		private string _observaciones = string.Empty;
		private string _albaranes = string.Empty;
		private int _oid_usuario;

		#endregion

		#region Properties

		public virtual long OidSerie { get { return _oid_serie; } set { _oid_serie = value; } }
		public virtual long OidTpv { get { return _oid_tpv; } set { _oid_tpv = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual long Tipo { get { return _tipo; } set { _tipo = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual Decimal BaseImponible { get { return _base_imponible; } set { _base_imponible = value; } }
		public virtual Decimal Impuestos { get { return _impuestos; } set { _impuestos = value; } }
		public virtual Decimal Descuento { get { return _descuento; } set { _descuento = value; } }
		public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual long FormaPago { get { return _forma_pago; } set { _forma_pago = value; } }
		public virtual long DiasPago { get { return _dias_pago; } set { _dias_pago = value; } }
		public virtual long MedioPago { get { return _medio_pago; } set { _medio_pago = value; } }
		public virtual DateTime PrevisionPago { get { return _prevision_pago; } set { _prevision_pago = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual string Albaranes { get { return _albaranes; } set { _albaranes = value; } }
		public virtual int OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }

		#endregion

		#region Business Methods

		public TicketRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			long tipo_query = Format.DataReader.GetInt64(source, "TIPO_QUERY");

			if (tipo_query == 0)
			{
				Oid = Format.DataReader.GetInt64(source, "OID");
				_oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
				_oid_tpv = Format.DataReader.GetInt64(source, "OID_TPV");
				_serial = Format.DataReader.GetInt64(source, "SERIAL");
				_codigo = Format.DataReader.GetString(source, "CODIGO");
				_estado = Format.DataReader.GetInt64(source, "ESTADO");
				_tipo = Format.DataReader.GetInt64(source, "TIPO");
				_fecha = Format.DataReader.GetDateTime(source, "FECHA");
				_base_imponible = Format.DataReader.GetDecimal(source, "BASE_IMPONIBLE");
				_impuestos = Format.DataReader.GetDecimal(source, "IMPUESTOS");
				_descuento = Format.DataReader.GetDecimal(source, "DESCUENTO");
				_total = Format.DataReader.GetDecimal(source, "TOTAL");
				_forma_pago = Format.DataReader.GetInt64(source, "FORMA_PAGO");
				_dias_pago = Format.DataReader.GetInt64(source, "DIAS_PAGO");
				_medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
				_prevision_pago = Format.DataReader.GetDateTime(source, "PREVISION_PAGO");
				_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
				_albaranes = Format.DataReader.GetString(source, "ALBARANES");
				_oid_usuario = Format.DataReader.GetInt32(source, "OID_USUARIO");
			}
		}
		public virtual void CopyValues(TicketRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_serie = source.OidSerie;
			_oid_tpv = source.OidTpv;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_estado = source.Estado;
			_tipo = source.Tipo;
			_fecha = source.Fecha;
			_base_imponible = source.BaseImponible;
			_impuestos = source.Impuestos;
			_descuento = source.Descuento;
			_total = source.Total;
			_forma_pago = source.FormaPago;
			_dias_pago = source.DiasPago;
			_medio_pago = source.MedioPago;
			_prevision_pago = source.PrevisionPago;
			_observaciones = source.Observaciones;
			_albaranes = source.Albaranes;
			_oid_usuario = source.OidUsuario;
		}

		#endregion
	}

	[Serializable()]
	public class TicketBase
	{
		#region Attributes

		private TicketRecord _record = new TicketRecord();

		private bool _n_ticket_manual = false;
		private string _numero_serie = string.Empty;
		private string _serie = string.Empty;
		private string _tpv = string.Empty;
		private string _id_mov_contable = string.Empty;
		private string _usuario = string.Empty;

		#endregion

		#region Properties

		public TicketRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
		public virtual ETipoFactura ETipoFactura { get { return (ETipoFactura)_record.Tipo; } }
		public virtual string TipoFacturaLabel { get { return Library.Store.EnumText<ETipoFactura>.GetLabel(ETipoFactura); } }
		public virtual string NTicket { get { return _numero_serie + "/" + _record.Codigo; } }
		public virtual string NumeroSerie { get { return _numero_serie; } set { _numero_serie = value; } }
		public virtual string Serie { get { return _serie; } set { _serie = value; } }
		public virtual string TPV { get { return _tpv; } set { _tpv = value; } }
		public virtual Decimal Subtotal { get { return _record.BaseImponible + _record.Descuento; } }
		public virtual bool NTicketManual { get { return _n_ticket_manual; } set { _n_ticket_manual = value; } }
		public virtual EFormaPago EFormaPago { get { return (EFormaPago)_record.FormaPago; } }
		public virtual string FormaPagoLabel { get { return Common.EnumText<EFormaPago>.GetLabel(EFormaPago); } }
		public virtual EMedioPago EMedioPago { get { return (EMedioPago)_record.MedioPago; } }
		public virtual string MedioPagoLabel { get { return Common.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
		public virtual string IDMovimientoContable { get { return _id_mov_contable; } }
		public virtual string Usuario { get { return _usuario; } set { _usuario = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			long tipo_query = Format.DataReader.GetInt64(source, "TIPO_QUERY");

			if (tipo_query == 0)
			{
				_numero_serie = Format.DataReader.GetString(source, "N_SERIE");
				_serie = Format.DataReader.GetString(source, "SERIE");
				_tpv = Format.DataReader.GetString(source, "TPV");
				_id_mov_contable = Format.DataReader.GetString(source, "ID_MOVIMIENTO_CONTABLE");

				_id_mov_contable = (_id_mov_contable == "/") ? string.Empty : _id_mov_contable;

				_usuario = Format.DataReader.GetString(source, "USUARIO");
			}
		}
		internal void CopyValues(Ticket source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_numero_serie = source.NumeroSerie;
			_serie = source.Serie;
			_tpv = source.TPV;
			_id_mov_contable = source.IDMovimientoContable;
			_usuario = source.Usuario;
		}
		internal void CopyValues(TicketInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_numero_serie = source.NumeroSerie;
			_serie = source.Serie;
			_tpv = source.TPV;
			_id_mov_contable = source.IDMovimientoContable;
			_usuario = source.Usuario;
		}

		#endregion
	}

    /// <summary>
    /// Editable Root Business Object With Editable Child Collection
    /// Editable Child Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
	public class Ticket : BusinessBaseEx<Ticket>, IEntity, IEntidadRegistro, IBankLine
	{
		#region IEntity

		public long EntityType { get { return (long)ETipoEntidad.Ticket; } }

		#endregion

		#region IEntidadRegistro

		public virtual ETipoEntidad ETipoEntidad { get { return ETipoEntidad.Ticket; } }
		public virtual string DescripcionRegistro { get { return "TICKET Nº " + NTicket + " de " + Fecha.ToShortDateString() + " de " + Total.ToString("C2"); } }

		public virtual IEntidadRegistro ISave() { return (IEntidadRegistro)SharedSave(); }
		public virtual IEntidadRegistro IGet(long oid, bool childs) { return (IEntidadRegistro)Get(oid, childs); }

		public void Update(Registro parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
			TicketRecord obj = Session().Get<TicketRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			MarkOld();
		}

		#endregion

		#region IBankLine

		public long TipoMovimiento { get { return (long)EBankLineType.Ticket; } }
        public EBankLineType ETipoMovimientoBanco { get { return EBankLineType.Ticket; } }
		public ETipoTitular ETipoTitular { get { return ETipoTitular.Cliente; } }
		public string CodigoTitular { get { return string.Empty; } set { } }
        public string Titular { get { return string.Empty; } set { } }
        public long OidCuenta { get { return 0; } set { } }
		public string CuentaBancaria { get { return string.Empty; } set { } }
		public decimal Importe { get { return _base.Record.Total; } set { } }
        public decimal GastosBancarios { get { return 0; } set { } }
		public DateTime Vencimiento { get { return _base.Record.PrevisionPago; } set { } }
		public bool Confirmado { get { return true; } } 

		public virtual IBankLineInfo IGetInfo(bool childs) { return (IBankLineInfo)GetInfo(childs); }

		#endregion

		#region Attributes

		protected TicketBase _base = new TicketBase();

        private AlbaranTickets _albaran_tickets = AlbaranTickets.NewChildList();
        private ConceptoTickets _concepto_facturas = ConceptoTickets.NewChildList();

        #endregion

        #region Properties

		public TicketBase Base { get { return _base; } }

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
		public virtual long OidTpv
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
		public virtual long Tipo
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
		public virtual DateTime PrevisionPago
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PrevisionPago;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PrevisionPago.Equals(value))
				{
					_base.Record.PrevisionPago = value;
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
		public virtual string Albaranes
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Albaranes;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Albaranes.Equals(value))
				{
					_base.Record.Albaranes = value;
					PropertyHasChanged();
				}
			}
		}

        public virtual AlbaranTickets AlbaranTickets
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _albaran_tickets;
            }

            set
            {
                _albaran_tickets = value;
            }

        }
        public virtual ConceptoTickets ConceptoTickets
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _concepto_facturas;
            }

            set
            {
                _concepto_facturas = value;
            }

        }

        //NO ENLAZADOS
		public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual ETipoFactura ETipoFactura { get { return _base.ETipoFactura; } set { Tipo = (long)value; } }
		public virtual string TipoFacturaLabel { get { return _base.TipoFacturaLabel; } }
		public virtual string NTicket { get { return _base.NTicket; } }
		public virtual string NumeroSerie { get { return _base.NumeroSerie; } set { _base.NumeroSerie = value; PropertyHasChanged(); } }
		public virtual string Serie { get { return _base.Serie; } set { _base.Serie = value; } }
		public virtual string TPV { get { return _base.TPV; } set { _base.TPV = value; PropertyHasChanged(); } }
        public virtual Decimal Subtotal { get { return _base.Subtotal; } }
		public virtual bool NTicketManual { get { return _base.NTicketManual; } set { _base.NTicketManual = value; } }
		public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } set { FormaPago = (long)value; } }
		public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } set { MedioPago = (long)value; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
        public virtual string IDMovimientoContable { get { return _base.IDMovimientoContable; } }
        public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid
                   && _concepto_facturas.IsValid
				   && _albaran_tickets.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                   || _concepto_facturas.IsDirty
				   || _albaran_tickets.IsDirty;
            }
        }

        #endregion

        #region Business Methods

        public virtual Ticket CloneAsNew()
        {
            Ticket clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.GetNewCode();
            clon.SessionCode = Ticket.OpenSession();
            Ticket.BeginTransaction(clon.SessionCode);

            clon.MarkNew();
            clon.ConceptoTickets.MarkAsNew();
			clon.AlbaranTickets.MarkAsNew();

            return clon;
        }

		public virtual void GetNewCode()
		{
			Serial = SerialTicketInfo.GetNext(this.OidSerie, this.Fecha.Year, ETipoFactura);

			// Devolvemos el siguiente codigo de Ticket para esa serie
			if (ETipoFactura == ETipoFactura.Rectificativa)
				Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT + "-R");
			else
				Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
		}

		protected virtual void SetNewCode()
		{
			try
			{
				TicketInfo factura = TicketInfo.GetByCode(Codigo, OidSerie, Fecha.Year, false);
				if (factura != null && (factura.Oid != 0) && (factura.Oid != Oid)) throw new iQException("Número de Ticket duplicado");
				Serial = Convert.ToInt64(Codigo);
			}
			catch
			{
				throw new iQException("Nº de Ticket incorrecto." + System.Environment.NewLine +
										"Debe tener el formato " + Resources.Defaults.FACTURA_CODE_FORMAT);
			}
		}

		public virtual void CopyFrom(OutputDeliveryInfo source)
        {
            if (source == null) return;

            OidSerie = source.OidSerie;
            Observaciones = source.Observaciones;
			Descuento = source.Descuento;
        }

        /// <summary>
        /// Crea los conceptos de factura asociados a un albarán
        /// </summary>
        /// <param name="source"></param>
        public virtual void Insert(OutputDeliveryInfo source)
        {
            ConceptoTicket newitem;

            AlbaranTickets.NewItem(this, source);
            foreach (OutputDeliveryLineInfo item in source.Conceptos)
            {
                newitem = ConceptoTickets.NewItem(this);
                newitem.CopyFrom(item);
            }
            OidUsuario = (AppContext.User != null) ? (int)AppContext.User.Oid : 0;
            Usuario = (AppContext.User != null) ? AppContext.User.Name : string.Empty;

            CalculaTotal();
        }

        /// <summary>
        /// Elimina los conceptos de factura asociados a un albarán
        /// </summary>
        /// <param name="source"></param>
        public virtual void Extract(OutputDeliveryInfo source)
        {
			if (source.Conceptos == null) source.LoadChilds(typeof(OutputDeliveryLines), true);

            AlbaranTickets.Remove(this, source);

            foreach (OutputDeliveryLineInfo item in source.Conceptos)
                ConceptoTickets.Remove(item);

            CalculaTotal();
        }

        /// <summary>
        /// Crea los conceptos de factura asociados a un albarán
        /// </summary>
        /// <param name="source"></param>
        public virtual void Compact(OutputDeliveryInfo source)
        {
            /*ConceptoTicket newitem;

            AlbaranTickets.NewItem(this, source);

            ClienteInfo cl = ClienteInfo.Get(OidCliente, true);
            ExpedientInfo exp = null;
            ExpedientInfo exp2 = null;
            ProductInfo p = null;
            ProductInfo p2 = null;

            foreach (ConceptoAlbaranInfo item in source.ConceptoAlbaranes)
            {
                ConceptoTicket cf = ConceptoTickets.GetItemByOidPartida(item.OidPartida);
                
                //Ya existe un concepto asociado a esta Partida
                if (cf != null)
                {
                    cf.Cantidad += item.Cantidad;
                    cf.CantidadBultos += item.CantidadBultos;
                    ProductoClienteInfo pcl = cl.ProductoClientes.GetByProduct(item.OidProducto);
                    p2 = (p != null && p.Oid == item.OidProducto) ? p : ProductInfo.Get(item.OidProducto);
                    p = p2;
                    exp2 = (exp != null && exp.Oid == item.OidExpediente) ? exp2 = exp : ExpedientInfo.Get(item.OidExpediente, true);
                    exp = exp2;
                    BatchInfo pexp = exp2.Partidas.GetItem(item.OidPartida);
                    cf.TicketcionBulto = (pcl != null) ? pcl.TicketcionBulto : false;
                    cf.UpdatePrecio(p2, pexp, pcl);
                }
                //Nuevo concepto. No existe concepto previo asociado a esta Partida
                else
                {
                    newitem = ConceptoTickets.NewItem(this);
                    newitem.CopyFrom(item);
                }
            }

            CalculaTotal();*/
        }

        public virtual void CalculaTotal()
        {
			BaseImponible = 0;
			Descuento = 0;
			Impuestos = 0;
			Total = 0;

			foreach (ConceptoTicket cf in ConceptoTickets)
			{
				if (!cf.IsKitComponent)
				{
					cf.CalculaTotal();

					BaseImponible += cf.BaseImponible;
					Descuento += cf.Descuento;
					Impuestos += cf.Impuestos;
					Total += cf.Total;
				}
			}			
        }

        public virtual void UpdateInfoAlbaranes()
        {
            Observaciones = Resources.Defaults.OBSERVACIONES_FACTURA;

            foreach (AlbaranTicket item in AlbaranTickets)
            {
                Observaciones += " " + item.CodigoAlbaran + ",";
            }

            Observaciones = Observaciones.Substring(0, Observaciones.Length - 1);
        }
		
		public static Ticket ChangeEstado(long oid, EEstado estado)
		{
			if (!CanChangeState())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			Ticket item = null;

			try
			{
				item = Ticket.Get(oid, false);

                if ((item.EEstado == EEstado.Contabilizado || item.EEstado == EEstado.Exportado) && (!AutorizationRulesControler.CanGetObject(Resources.SecureItems.CUENTA_CONTABLE)))
					throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

				EntityBase.CheckChangeState(item.EEstado, estado);

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

		public virtual void SetAlbaranes()
		{
			Albaranes = string.Empty;

			foreach (AlbaranTicket item in AlbaranTickets)
				Albaranes += item.CodigoAlbaran + "; ";
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

			return true;
		}	

        #endregion

        #region Autorization Rules

        public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.FACTURA_EMITIDA)
				&& Library.Store.Serie.CanGetObject()
				&& Stock.CanAddObject();
        }
        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.FACTURA_EMITIDA)
				&& Library.Store.Serie.CanGetObject()
				&& Library.Invoice.Cliente.CanGetObject();  
		}
        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.FACTURA_EMITIDA)
				&& Library.Store.Serie.CanGetObject()
				&& Stock.CanDeleteObject(); 
        }
        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.FACTURA_EMITIDA)
				&& Library.Store.Serie.CanGetObject()
				&& Stock.CanAddObject()
				&& Stock.CanEditObject()
				&& Stock.CanDeleteObject(); 
        }
		public static bool CanChangeState()
		{
			return AutorizationRulesControler.CanGetObject(Library.Common.Resources.SecureItems.ESTADO);
		}

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				Ticket = Ticket.New().GetInfo(false),
				Estado = EEstado.NoAnulado
			};
			conditions.Ticket.Oid = oid;

			TicketInfo item = TicketInfo.Get(oid, false);

			if (item.EEstado != EEstado.Abierto)
				throw new iQException(Resources.Messages.FACTURA_NO_ANULADA);
		}

        #endregion

		#region Common Factory Methods

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		protected Ticket() { }

		public virtual TicketInfo GetInfo() { return GetInfo(true); }
		public virtual TicketInfo GetInfo(bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return new TicketInfo(this, childs);
		}

		public virtual void LoadChilds(Type type, bool get_childs)
		{
			if (type.Equals(typeof(AlbaranTicket)))
			{
				_albaran_tickets = AlbaranTickets.GetChildList(this, get_childs);
			}
		}

		#endregion

		#region Child Factory Methods

		private Ticket(int sessionCode, IDataReader source, bool childs)
		{
			MarkAsChild();
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(source);
		}

		public static Ticket NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			Ticket obj = DataPortal.Create<Ticket>(new CriteriaCs(-1));
			obj.MarkAsChild();
			return obj;
		}

		internal static Ticket GetChild(int sessionCode, IDataReader source, bool childs) { return new Ticket(sessionCode, source, childs); }

		#endregion

        #region Root Factory Methods

        public static Ticket New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            Ticket obj = DataPortal.Create<Ticket>(new CriteriaCs(-1));
            return obj;
        }

        public static Ticket New(OutputDeliveryInfo albaran)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            Ticket obj = DataPortal.Create<Ticket>(new CriteriaCs(-1));
            obj.CopyFrom(albaran);
            return obj;
        }

        public static Ticket Get(long oid) { return Get(oid, true); }
        public static Ticket Get(long oid, bool childs) 
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Ticket.GetCriteria(Ticket.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Ticket.SELECT(oid);

			Ticket.BeginTransaction(criteria.Session);
			return DataPortal.Fetch<Ticket>(criteria);
		}

        public static Ticket Get(CriteriaEx criteria)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            Ticket.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<Ticket>(criteria);
        }

        public static void Delete(long oid)
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            DataPortal.Delete(new CriteriaCs(oid));
        }

        /// <summary>
        /// Elimina todas los Tickets
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = Ticket.OpenSession();
            ISession sess = Ticket.Session(sessCode);
            ITransaction trans = Ticket.BeginTransaction(sessCode);

            try
            {
                sess.Delete("from TicketRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                Ticket.CloseSession(sessCode);
            }
        }

		public static void DeleteFromList(List<OutputDeliveryInfo> list)
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = AlbaranTicket.OpenSession();
			ITransaction trans = AlbaranTicket.BeginTransaction(AlbaranTicket.Session(sessCode));

			try
			{
				List<long> oids = new List<long>();

				oids.Add(0);

				foreach (OutputDeliveryInfo item in list)
					oids.Add(item.Oid);

				nHManager.Instance.SQLNativeExecute(DELETE(oids));

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
		
		public static void ChangeStateFromList(List<OutputDeliveryInfo> list, EEstado estado)
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = AlbaranTicket.OpenSession();
			ITransaction trans = AlbaranTicket.BeginTransaction(AlbaranTicket.Session(sessCode));

			try
			{
				List<long> oids = new List<long>();

				oids.Add(0);

				foreach (OutputDeliveryInfo item in list)
					oids.Add(item.Oid);

				nHManager.Instance.SQLNativeExecute(CHANGE_STATE(oids, estado));

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

        public override Ticket Save()
        {
            // Por interfaz Root/Child
            if (IsChild) 
                throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

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

                // Update de las listas.
                _albaran_tickets.Update(this);
                _concepto_facturas.Update(this);

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

		protected override Ticket SharedSave()
		{
			// Por interfaz Root/Child
			if (IsChild)
				throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			try
			{
				ValidationRules.CheckRules();

				base.Save();

				// Update de las listas.
				_albaran_tickets.Update(this);
				_concepto_facturas.Update(this);

				return this;
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
				return this;
			}
		}

        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
            Random r = new Random();
            Oid = (long)r.Next();
            Fecha = DateTime.Now;
            EMedioPago = EMedioPago.Efectivo;
			EFormaPago = EFormaPago.Contado;
            EEstado = EEstado.Abierto;
			ETipoFactura = ETipoFactura.Ordinaria;

			GetNewCode();
        }

		private void Fetch(Ticket source)
		{
			try
			{
				SessionCode = source.SessionCode;

				_base.CopyValues(source);

				if (Childs)
				{
					if (nHMng.UseDirectSQL)
					{
						IDataReader reader;
						string query;

						AlbaranTicket.DoLOCK(Session());
						query = AlbaranTickets.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_albaran_tickets = AlbaranTickets.GetChildList(SessionCode, reader);

						ConceptoTicket.DoLOCK(Session());
						query = ConceptoTickets.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_concepto_facturas = ConceptoTickets.GetChildList(SessionCode, reader);
					}
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
					if (nHMng.UseDirectSQL)
					{
						IDataReader reader;
						string query;

						AlbaranTicket.DoLOCK(Session());
						query = AlbaranTickets.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_albaran_tickets = AlbaranTickets.GetChildList(SessionCode, reader);

						ConceptoTicket.DoLOCK(Session());
						query = ConceptoTickets.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_concepto_facturas = ConceptoTickets.GetChildList(SessionCode, reader);
					}
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Insert(Tickets parent)
		{
			// if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            OidUsuario = (int)AppContext.User.Oid;
            Usuario = AppContext.User.Name;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);

			MarkOld();
		}

		internal void Update(Tickets parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
			TicketRecord obj = Session().Get<TicketRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			MarkOld();
		}

		internal void DeleteSelf(Tickets parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<TicketRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkNew();
		}

        #endregion   

        #region Root Data Access

        // called to retrieve data from the database
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                _base.Record.Oid = 0;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

				MarkOld();

                if (nHMng.UseDirectSQL)
                {
                    Ticket.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        AlbaranTicket.DoLOCK(Session());
                        query = AlbaranTickets.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
						_albaran_tickets = AlbaranTickets.GetChildList(SessionCode, reader);

						SetAlbaranes();

                        ConceptoTicket.DoLOCK(Session());
                        query = ConceptoTickets.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
						_concepto_facturas = ConceptoTickets.GetChildList(SessionCode, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Insert()
        {
            try
            {
				long oid_caja = ModulePrincipal.GetCajaTicketsSetting();
				if (oid_caja == 0) throw new iQException("No se ha definido la caja de Tickets");

                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
                
				if (!NTicketManual)
                    GetNewCode();
                else
                    SetNewCode();

				OidUsuario = (AppContext.User != null) ? (int)AppContext.User.Oid : 0;
				Usuario = (AppContext.User != null) ? AppContext.User.Name : string.Empty;

                Session().Save(Base.Record);

				//Insertamos la linea de caja y el movimiento de banco asociado
				Cash.InsertItem(this, ModulePrincipal.GetCajaTicketsSetting(), SessionCode);
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
                if (NTicketManual)
                    SetNewCode();

                try
                {
					long oid_caja = ModulePrincipal.GetCajaTicketsSetting();
					if (oid_caja == 0) throw new iQException("No se ha definido la caja de Tickets");

					TicketRecord obj = Session().Get<TicketRecord>(Oid);
                    TicketInfo oldItem = TicketInfo.Get(Oid);
                    obj.CopyValues(Base.Record);
                    Session().Update(obj);

					//Editamos o Anulamos la Linea de Caja o el Movimiento de Banco si los hubiera
					Cash.EditItem(this, oldItem, ModulePrincipal.GetCajaTicketsSetting(), SessionCode);

                    MarkOld();
                }
                catch (Exception ex)
                {
                    iQExceptionHandler.TreatException(ex);
                }
            }
        }

        // deferred deletion
        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new CriteriaCs(Oid));
        }

        // inmediate deletion
        [Transactional(TransactionalTypes.Manual)]
        private void DataPortal_Delete(CriteriaCs criterio)
        {
            try
            {
                //Iniciamos la conexion y la transaccion
                SessionCode = OpenSession();
                BeginTransaction();

                //Si no hay integridad referencial, aqui se deben borrar las listas hijo
                CriteriaEx criteria = GetCriteria();
                criteria.AddOidSearch(criterio.Oid);

                // Obtenemos el objeto
				TicketRecord obj = (TicketRecord)(criteria.UniqueResult());
				Session().Delete(Session().Get<TicketRecord>(obj.Oid));

				long oid_caja = ModulePrincipal.GetCajaTicketsSetting();
				if (oid_caja == 0) throw new iQException("No se ha definido la caja de Tickets");

				//Anulamos la Linea de Caja o el Movimiento de Banco si los hubiera
				Cash.AnulaItem(this, ModulePrincipal.GetCajaTicketsSetting(), SessionCode);

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

		public new static string SELECT(long oid) { return SELECT(oid, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = "SELECT 0 AS \"TIPO_QUERY\"" +
                    "		,TK.*" +
                    "		,COALESCE(US.\"NAME\", '') AS \"USUARIO\"" +
					"       ,SE.\"IDENTIFICADOR\" AS \"N_SERIE\"" +
					"       ,SE.\"NOMBRE\" AS \"SERIE\"" +
					"       ,TP.\"NOMBRE\" AS \"TPV\"" +
					"		,COALESCE(RG.\"CODIGO\", '') || '/' || COALESCE(LR.\"ID_EXPORTACION\", '') AS \"ID_MOVIMIENTO_CONTABLE\""; 

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE (TK.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			if (conditions.Ticket != null) 
				if (conditions.Ticket.Oid != 0) 
					query += " AND TK.\"OID\" = " + conditions.Ticket.Oid.ToString();

			if (conditions.OidList != null)
				query += " AND TK.\"OID\" IN " + EntityBase.GET_IN_STRING(conditions.OidList);

			switch (conditions.Estado)
			{
				case EEstado.Todos:
					break;

				case EEstado.NoAnulado:
					query += " AND TK.\"ESTADO\" != " + (long)EEstado.Anulado;
					break;

				case EEstado.NoOculto:
					query += " AND TK.\"ESTADO\" != " + (long)EEstado.Oculto;
					break;

				default:
					query += " AND TK.\"ESTADO\" = " + (long)conditions.Estado;
					break;
			}

			if (conditions.Serie != null) query += " AND TK.\"OID_SERIE\" = " + conditions.Serie.Oid;
			if (conditions.MedioPago != EMedioPago.Todos) query += " AND TK.\"MEDIO_PAGO\" = " + (long)conditions.MedioPago;
            
            if (conditions.MedioPagoList != null && conditions.MedioPagoList.Count > 0)
                query += EntityBase.GET_IN_LIST_CONDITION(conditions.MedioPagoList, "TK", "MEDIO_PAGO"); 
            if (conditions.TipoFactura != ETipoFactura.Todas) query += " AND TK.\"TIPO\" = " + (long)conditions.TipoFactura;
			if (conditions.OutputDelivery != null) query += " AND AT.\"OID_ALBARAN\" = " + conditions.OutputDelivery.Oid;

			return query;
		}

		internal static string INNER_BASE(QueryConditions conditions)
		{
			string tk = nHManager.Instance.GetSQLTable(typeof(TicketRecord));
			string at = nHManager.Instance.GetSQLTable(typeof(DeliveryTicketRecord));
			string se = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string tp = nHManager.Instance.GetSQLTable(typeof(TPVRecord));
			string lr = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
			string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));

			string query;

            query = " FROM " + tk + " AS TK" +
                    " LEFT JOIN " + us + " AS US ON US.\"OID\" = TK.\"OID_USUARIO\"" +
					" INNER JOIN " + se + " AS SE ON SE.\"OID\" = TK.\"OID_SERIE\"" +
					" LEFT JOIN " + tp + " AS TP ON TP.\"OID\" = TK.\"OID_TPV\"" +
					" LEFT JOIN (SELECT MAX(\"ID_EXPORTACION\") AS \"ID_EXPORTACION\", \"OID_ENTIDAD\", MAX(\"OID_REGISTRO\") AS \"OID_REGISTRO\"" +
					"			FROM " + lr + " AS LR" +
					"			WHERE LR.\"TIPO_ENTIDAD\" = " + (long)ETipoEntidad.Ticket +
					"				AND LR.\"ESTADO\" = " + (long)EEstado.Contabilizado +
					"			GROUP BY \"OID_ENTIDAD\")" +
					"			AS LR ON LR.\"OID_ENTIDAD\" = TK.\"OID\"" +
					" LEFT JOIN " + rg + " AS RG ON RG.\"OID\" = LR.\"OID_REGISTRO\"";

			if (conditions.OutputDelivery != null)
				query += " LEFT JOIN " + at + " AS AT ON AT.\"OID_TICKET\" = TK.\"OID\"";

			return query;
		}

		internal static string SELECT_BASE(QueryConditions conditions)
		{
			string query;

			query = SELECT_FIELDS() +
					INNER_BASE(conditions);

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query = string.Empty;

			query = SELECT_BASE(conditions) +
					WHERE(conditions);

			query += " ORDER BY TK.\"FECHA\", SE.\"IDENTIFICADOR\", TK.\"CODIGO\"";

			if (lockTable) query += " FOR UPDATE OF TK NOWAIT";

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Ticket = Ticket.New().GetInfo(false) };
			conditions.Ticket.Oid = oid;

			query = SELECT(conditions, lockTable);

			return query;
		}

		internal static string SELECT_BY_CODE(QueryConditions conditions, bool lockTable)
		{
			string query = SELECT_BASE(conditions) +
							WHERE(conditions);

			query += " AND TK.\"CODIGO\" = '" + conditions.Ticket.Codigo + "'";

			if (lockTable) query += " FOR UPDATE OF TK NOWAIT";

			return query;
		}

		internal static string DELETE(QueryConditions conditions)
		{
			string tk = nHManager.Instance.GetSQLTable(typeof(TicketRecord));

			string query = string.Empty;

			query = " DELETE FROM " + tk + " AS TK" +
					WHERE(conditions);

			return query;
		}

		internal static string DELETE(List<long> oid_list)
		{
			string tk = nHManager.Instance.GetSQLTable(typeof(TicketRecord));
			string at = nHManager.Instance.GetSQLTable(typeof(DeliveryTicketRecord));

			string query = string.Empty;

			query = " DELETE FROM " + tk + " AS TK" +
					" WHERE TK.\"OID\" IN (SELECT AT.\"OID_TICKET\"" +
					"						FROM " + at + " AS AT" +
					"						WHERE AT.\"OID_ALBARAN\" IN " + EntityBase.GET_IN_STRING(oid_list) + ")"; 

			return query;
		}

		internal static string CHANGE_STATE(List<long> oid_list, EEstado estado)
		{
			string tk = nHManager.Instance.GetSQLTable(typeof(TicketRecord));
			string at = nHManager.Instance.GetSQLTable(typeof(DeliveryTicketRecord));

			string query = string.Empty;

			query = " UPDATE " + tk + " SET \"ESTADO\" = " + (long)estado +
					" WHERE " + tk + ".\"OID\" IN (SELECT AT.\"OID_TICKET\"" +
					"											FROM " + at + " AS AT" +
					"											WHERE AT.\"OID_ALBARAN\" IN " + EntityBase.GET_IN_STRING(oid_list) + ")";

			return query;
		}

		#endregion
    }
}

