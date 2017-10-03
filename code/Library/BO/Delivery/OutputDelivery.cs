using System;
using System.ComponentModel;
using System.Collections;
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
	public class OutputDeliveryRecord : RecordBase
	{
		#region Attributes

		private long _oid_serie;
		private long _oid_holder;
		private long _oid_transportista;
		private long _holder_type;
		private long _serial;
		private string _codigo = string.Empty;
		private long _ano;
		private DateTime _fecha;
		private long _forma_pago;
		private long _dias_pago;
		private long _medio_pago;
		private DateTime _prevision_pago;
		private Decimal _base_imponible;
		private Decimal _impuestos;
		private Decimal _p_descuento;
		private Decimal _descuento;
		private Decimal _total;
		private string _cuenta_bancaria = string.Empty;
		private bool _nota = false;
		private string _observaciones = string.Empty;
		private bool _contado = false;
		private bool _rectificativo = false;
		private long _oid_usuario;
		private long _oid_almacen;
		private long _oid_expediente;
		private long _estado;

		#endregion

		#region Properties

		public virtual long OidSerie { get { return _oid_serie; } set { _oid_serie = value; } }
		public virtual long OidHolder { get { return _oid_holder; } set { _oid_holder = value; } }
		public virtual long OidTransportista { get { return _oid_transportista; } set { _oid_transportista = value; } }
		public virtual long HolderType { get { return _holder_type; } set { _holder_type = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long Ano { get { return _ano; } set { _ano = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual long FormaPago { get { return _forma_pago; } set { _forma_pago = value; } }
		public virtual long DiasPago { get { return _dias_pago; } set { _dias_pago = value; } }
		public virtual long MedioPago { get { return _medio_pago; } set { _medio_pago = value; } }
		public virtual DateTime PrevisionPago { get { return _prevision_pago; } set { _prevision_pago = value; } }
		public virtual Decimal BaseImponible { get { return _base_imponible; } set { _base_imponible = value; } }
		public virtual Decimal Impuestos { get { return _impuestos; } set { _impuestos = value; } }
		public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
		public virtual Decimal Descuento { get { return _descuento; } set { _descuento = value; } }
		public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
		public virtual bool Nota { get { return _nota; } set { _nota = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual bool Contado { get { return _contado; } set { _contado = value; } }
		public virtual bool Rectificativo { get { return _rectificativo; } set { _rectificativo = value; } }
		public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }
		public virtual long OidAlmacen { get { return _oid_almacen; } set { _oid_almacen = value; } }
		public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }

		#endregion

		#region Business Methods

		public OutputDeliveryRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
			_oid_holder = Format.DataReader.GetInt64(source, "OID_HOLDER");
			_oid_transportista = Format.DataReader.GetInt64(source, "OID_TRANSPORTISTA");
			_holder_type = Format.DataReader.GetInt64(source, "HOLDER_TYPE");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_ano = Format.DataReader.GetInt64(source, "ANO");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_forma_pago = Format.DataReader.GetInt64(source, "FORMA_PAGO");
			_dias_pago = Format.DataReader.GetInt64(source, "DIAS_PAGO");
			_medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
			_prevision_pago = Format.DataReader.GetDateTime(source, "PREVISION_PAGO");
			_base_imponible = Format.DataReader.GetDecimal(source, "BASE_IMPONIBLE");
			_impuestos = Format.DataReader.GetDecimal(source, "P_IGIC");
			_p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
			_descuento = Format.DataReader.GetDecimal(source, "DESCUENTO");
			_total = Format.DataReader.GetDecimal(source, "TOTAL");
			_cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
			_nota = Format.DataReader.GetBool(source, "NOTA");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_contado = Format.DataReader.GetBool(source, "CONTADO");
			_rectificativo = Format.DataReader.GetBool(source, "RECTIFICATIVO");
			_oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
			_oid_almacen = Format.DataReader.GetInt64(source, "OID_ALMACEN");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");

		}
		public virtual void CopyValues(OutputDeliveryRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_serie = source.OidSerie;
			_oid_holder = source.OidHolder;
			_oid_transportista = source.OidTransportista;
			_holder_type = source.HolderType;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_ano = source.Ano;
			_fecha = source.Fecha;
			_forma_pago = source.FormaPago;
			_dias_pago = source.DiasPago;
			_medio_pago = source.MedioPago;
			_prevision_pago = source.PrevisionPago;
			_base_imponible = source.BaseImponible;
			_impuestos = source.Impuestos;
			_p_descuento = source.PDescuento;
			_descuento = source.Descuento;
			_total = source.Total;
			_cuenta_bancaria = source.CuentaBancaria;
			_nota = source.Nota;
			_observaciones = source.Observaciones;
			_contado = source.Contado;
			_rectificativo = source.Rectificativo;
			_oid_usuario = source.OidUsuario;
			_oid_almacen = source.OidAlmacen;
			_oid_expediente = source.OidExpediente;
			_estado = source.Estado;
		}

		#endregion
	}

	[Serializable()]
	public class OutputDeliveryBase
	{
		#region Attributes

		private OutputDeliveryRecord _record = new OutputDeliveryRecord();

		public bool _id_manual = false;
		public string _owner = string.Empty;
		private string _expedient = string.Empty;
		public string _store = string.Empty;
		public string _store_id = string.Empty;
		public string _numero_serie;
		public string _nombre_serie = string.Empty;
		public string _id_cliente;
		public string _cliente = string.Empty;
		public string _numero_factura = string.Empty;
		public string _numero_ticket = string.Empty;
		public bool _facturado;
		protected long _oid_factura;

		public ImpuestoResumenList _impuestos_list = new ImpuestoResumenList();

		#endregion

		#region Properties

		public OutputDeliveryRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } set { _record.Estado = (long)value; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
		public ETipoEntidad EHolderType { get { return (ETipoEntidad)_record.HolderType ; } set { _record.HolderType = (long)value; } }
		public string HolderTypeLabel { get { return Library.Common.EnumText<ETipoEntidad>.GetLabel(EHolderType); } }
		public EFormaPago EFormaPago { get { return (EFormaPago)_record.FormaPago; } set { _record.FormaPago = (long)value; } }
		public string FormaPagoLabel { get { return Common.EnumText<EFormaPago>.GetLabel(EFormaPago); } }
		public EMedioPago EMedioPago { get { return (EMedioPago)_record.MedioPago; } set { _record.MedioPago = (long)value; } }
		public string MedioPagoLabel { get { return Common.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
		public string IDAlmacenAlmacen { get { return _store_id + " - " + _store; } }
		public string NSerieSerie { get { return _numero_serie + " - " + _nombre_serie; } }
		public Decimal Subtotal { get { return _record.BaseImponible + _record.Descuento; } }
        public string ExpedientID { get { return _expedient; } set { _expedient = value; } }
        public string FileName
		{
			get
			{
				string cliente = _cliente.Replace(".", "");

				return "Albaran_" + AppContext.ActiveSchema.Name.Replace(".", "") + " _" +
						((_cliente.Length > 15) ? cliente.Substring(0, 15) : cliente) + "_" +
						_record.Fecha.ToString("dd-MM-yyyy") + "_" + _numero_serie + "_" + _record.Codigo + ".pdf";
			}
		}

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_owner = Format.DataReader.GetString(source, "USUARIO");
			_store = Format.DataReader.GetString(source, "ALMACEN");
			_store_id = Format.DataReader.GetString(source, "ID_ALMACEN");
			_oid_factura = Format.DataReader.GetInt64(source, "OID_FACTURA");
			_facturado = Format.DataReader.GetDecimal(source, "FACTURAS") > 0;
			_numero_serie = Format.DataReader.GetString(source, "N_SERIE");
			_nombre_serie = Format.DataReader.GetString(source, "SERIE");
			_id_cliente = Format.DataReader.GetString(source, "ID_CLIENTE");
			_cliente = Format.DataReader.GetString(source, "CLIENTE");
			_numero_factura = Format.DataReader.GetString(source, "N_FACTURA");
			_numero_ticket = Format.DataReader.GetString(source, "N_TICKET");

			_record.Estado = (_record.Estado == (long)EEstado.Contabilizado) ? _record.Estado : (_facturado ? (long)EEstado.Billed : (long)EEstado.Abierto);
		}
		public void CopyValues(OutputDelivery source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_owner = source.Usuario;
			_expedient = source.Expediente;
			_store = source.Almacen;
			_store_id = source.IDAlmacen;
			_cliente = source.NombreCliente;
			_id_cliente = source.NumeroCliente;
			_numero_serie = source.NumeroSerie;
			_nombre_serie = source.NombreSerie;
			_numero_factura = source.NumeroFactura;
			_numero_ticket = source.NumeroTicket;
			_facturado = source.Facturado;
		}
		public void CopyValues(OutputDeliveryInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_owner = source.Usuario;
			_expedient = source.Expediente;
			_store = source.Almacen;
			_store_id = source.IDAlmacen;
			_cliente = source.NombreCliente;
			_id_cliente = source.NumeroCliente;
			_numero_serie = source.NumeroSerie;
			_nombre_serie = source.NombreSerie;
			_numero_factura = source.NumeroFactura;
			_numero_ticket = source.NumeroTicket;
			_facturado = source.Facturado;
		}

		private void InsertImpuesto(OutputDeliveryLineInfo item)
		{
			if (item.OidImpuesto == 0) return;
			if (item.Impuestos == 0) return;
            
            ImpuestoInfo impuesto = ImpuestoInfo.Get(item.OidImpuesto, false);

            if (impuesto != null)
            {
                ImpuestoResumen iresumen = new ImpuestoResumen
                {
                    OidImpuesto = item.OidImpuesto,
                    BaseImponible = item.BaseImponible,
                    Importe = item.BaseImponible * impuesto.Porcentaje / 100
                };

                _impuestos_list.Insert(iresumen);
            }
		}
		public Hashtable GetImpuestos(OutputDeliveryLines conceptos)
		{
			try
			{
				_impuestos_list.Clear();

				foreach (OutputDeliveryLine item in conceptos)
					InsertImpuesto(item.GetInfo(false));

				_record.Impuestos = _impuestos_list.TotalizeImpuestos();

				return _impuestos_list;
			}
			catch
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_IMPUESTO, _numero_factura, _cliente));
			}
		}
		public Hashtable GetImpuestos(OutputDeliveryLineList conceptos)
		{
			try
			{
				_impuestos_list.Clear();

				foreach (OutputDeliveryLineInfo item in conceptos)
					InsertImpuesto(item);

				_record.Impuestos = _impuestos_list.TotalizeImpuestos();

				return _impuestos_list;
			}
			catch
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_IMPUESTO, _numero_factura, _cliente));
			}
		}

		#endregion
	}

    /// <summary>
    /// Editable Root Business Object With Editable Child Collection
    /// Editable Child Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
	public class OutputDelivery : BusinessBaseEx<OutputDelivery>, IWorkResource
    {
		#region IWorkResource

		public long EntityType { get { return (long)ETipoEntidad.OutputDelivery; } set { } }
		public ETipoEntidad EEntityType { get { return ETipoEntidad.OutputDelivery; } set { } }
		public string ID { get { return Codigo; } }
		public string Name { get { return Resources.Labels.WORK_DELIVERY; } }
		public decimal Cost { get { return Total; } }

		#endregion

        #region Attributes

		protected OutputDeliveryBase _base = new OutputDeliveryBase();

        private OutputDeliveryLines _conceptos = OutputDeliveryLines.NewChildList();

        #endregion

        #region Properties

		public OutputDeliveryBase Base { get { return _base; } }

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
		public virtual long OidHolder
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidHolder;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidHolder.Equals(value))
				{
					_base.Record.OidHolder = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidTransportista
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidTransportista;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidTransportista.Equals(value))
				{
					_base.Record.OidTransportista = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long HolderType
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.HolderType;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.HolderType.Equals(value))
				{
					_base.Record.HolderType = value;
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
		public virtual long Ano
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Ano;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Ano.Equals(value))
				{
					_base.Record.Ano = value;
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
					Ano = _base.Record.Fecha.Year;
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
		public virtual DateTime Prevision
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
					_base.Record.BaseImponible = value;
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
					_base.Record.Impuestos = value;
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
		public virtual bool Nota
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Nota;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Nota.Equals(value))
				{
					_base.Record.Nota = value;
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
		public virtual bool Contado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Contado;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Contado.Equals(value))
				{
					_base.Record.Contado = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Rectificativo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Rectificativo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Rectificativo.Equals(value))
				{
					_base.Record.Rectificativo = value;
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

        public virtual OutputDeliveryLines Conceptos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _conceptos;
            }

            set
            {
                _conceptos = value;
            }
        }

        //NO ENLAZADOS
		public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual ETipoEntidad EHolderType { get { return _base.EHolderType; } set { HolderType = (long)value; } }
		public virtual string HolderTypeLabel { get { return _base.HolderTypeLabel; } }
		public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } set { FormaPago = (long)value; } }
		public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } set { MedioPago = (long)value; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }		
		public virtual string Usuario { get { return _base._owner; } set { _base._owner = value; } }
        public virtual string Expediente { get { return _base.ExpedientID; } set { _base.ExpedientID = value; } }
		public virtual string IDAlmacen { get { return _base._store_id; } set { _base._store_id = value; } }
		public virtual string Almacen { get { return _base._store; } set { _base._store = value; } }
		public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }
		public virtual string NumeroSerie { get { return _base._numero_serie; } set { _base._numero_serie = value; } }
		public virtual string NombreSerie { get { return _base._nombre_serie; } set { _base._nombre_serie = value; } }
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
		public virtual string NumeroCliente { get { return _base._id_cliente; } set { _base._id_cliente = value; } } /* DEPRECATED */
		public virtual string IDCliente { get { return _base._id_cliente; } set { _base._id_cliente = value; } }
		public virtual string NombreCliente { get { return _base._cliente; } set { _base._cliente = value; } }
		public virtual Decimal Subtotal { get { return BaseImponible + Descuento; } }
		public virtual bool IDManual { get { return _base._id_manual; } set { _base._id_manual = value; } }
		public virtual string NumeroFactura { get { return _base._numero_factura; } set { _base._numero_factura = value; } }
		public virtual string NumeroTicket { get { return _base._numero_ticket; } set { _base._numero_ticket = value; } }
		public virtual bool Facturado { get { return _base._facturado; } set { _base._facturado = value; } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid
                   && _conceptos.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                   || _conceptos.IsDirty;
            }
        }

        #endregion

        #region Business Methods

		public static OutputDelivery CloneAsNew(OutputDeliveryInfo source)
		{
			OutputDelivery clon = OutputDelivery.New();
			clon.Base.CopyValues(source);

			clon.GetNewCode(source.EHolderType, clon.Contado ? ETipoAlbaranes.Agrupados : ETipoAlbaranes.Todos);
			clon.OidUsuario = AppContext.User.Oid;
			clon.Usuario = AppContext.User.Name;
			clon.EEstado = EEstado.Abierto;
			clon.Fecha = DateTime.Now;
			clon.NumeroFactura = string.Empty;

			clon.MarkNew();

			if (source.Conceptos == null) source.LoadChilds(typeof(OutputDeliveryLines), false);

			foreach (OutputDeliveryLineInfo item in source.Conceptos)
				clon.Conceptos.NewItem(clon, item);

			return clon;
		}

        public virtual void GetNewCode(ETipoEntidad holdertype, ETipoAlbaranes deliveryType)
        {
            // Obtenemos el último serial de servicio
			Serial = OutputDeliverySerialInfo.GetNext(holdertype, deliveryType, OidSerie, Fecha.Year, Rectificativo);

            // Devolvemos el siguiente codigo de Albaran para esa serie
			switch (holdertype)
			{
				case ETipoEntidad.WorkReport:
					{
						Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
					}
					break;

				case ETipoEntidad.Cliente:

					switch (deliveryType)
					{
						case ETipoAlbaranes.Agrupados:
							{
								if (Rectificativo)
									Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT + "-C-R");
								else
									Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT + "-C");
							}
							break;

						default:
							{
								if (Rectificativo)
									Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT + "-R");
								else
									Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
							}
							break;
					}

					break;
			}
        }

        protected virtual void SetNewCode(ETipoAlbaranes tipo)
        {
            try
            {
                OutputDeliveryList list = OutputDeliveryList.GetList(false, 
														0,
 														EHolderType,
														OidSerie, 
														tipo, 
														Rectificativo ? ETipoFactura.Rectificativa : ETipoFactura.Ordinaria, 
														Convert.ToInt32(Ano));
                
				if (list.GetItemByProperty("Codigo", Codigo) != null)
                    throw new iQException("Número de Albaran duplicado");
                if (!Contado && !Rectificativo)
                    Serial = Convert.ToInt64(Codigo);
                else
                    Serial = Convert.ToInt64(Codigo.Substring(0, Codigo.Length - 2));
            }
            catch (iQException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw new iQException("Número de Albaran incorrecto." + System.Environment.NewLine +
                                        "Debe tener el formato " + Resources.Defaults.FACTURA_CODE_FORMAT);
            }
        }

		public virtual void CopyFrom(ClienteInfo source)
		{
			if (source == null) return;

			PDescuento = source.PDescuentoPtoPago;
			OidHolder = source.Oid;
			MedioPago = source.MedioPago;
			FormaPago = source.FormaPago;
			DiasPago = source.DiasPago;
			Prevision = Library.Common.EnumFunctions.GetPrevisionPago(EFormaPago, Fecha, DiasPago);
			NombreCliente = source.Nombre;
			NumeroCliente = source.Codigo;
		}
		public virtual void CopyFrom(OutputInvoice source)
		{
			if (source == null) return;

			OidSerie = source.OidSerie;
			OidHolder = source.OidCliente;
			OidTransportista = source.OidTransportista;
			Observaciones = source.Observaciones;
			Fecha = source.Fecha;
			BaseImponible = source.BaseImponible;
			Total = source.Total;
			Descuento = source.Descuento;
			Impuestos = source.Impuestos;
			PDescuento = source.PDescuento;
			CuentaBancaria = source.CuentaBancaria;
			FormaPago = source.FormaPago;
			DiasPago = source.DiasPago;
			MedioPago = source.MedioPago;
			Prevision = source.Prevision;

			NumeroFactura = source.Codigo;
		}
		public virtual void CopyFrom(PedidoInfo source)
		{
			if (source == null) return;

			OidSerie = source.OidSerie;
			Observaciones = source.Observaciones;
		}

		public virtual void AddProductosCliente(ClienteInfo source, SerieInfo serie)
		{
			OutputDeliveryLine newitem;

			if (Conceptos.Count > 0) return; 
			if (serie.SerieFamilias == null) serie.LoadChilds(typeof(SerieFamilia), true);
			if (source.Productos == null) source.LoadChilds(typeof(ProductoCliente), false);

			foreach (ProductoClienteInfo item in source.Productos)
			{
				if (!item.Facturar) continue;
				if (serie.SerieFamilias.GetItemByFamilia(item.OidFamilia) == null) continue;

				newitem = Conceptos.NewItem(this);
				newitem.CopyFrom(this, item);
			}

			CalculateTotal();
		}

        public virtual void CalculateTotal()
        {
			BaseImponible = 0;
			Descuento = 0;
			Total = 0;

			foreach (OutputDeliveryLine item in Conceptos)
			{
				if (!item.IsKitComponent)
				{
					item.CalculateTotal();

					BaseImponible += item.BaseImponible;
				}
			}

			//Esta funcion actualiza la propiedad Impuestos 
			_base.GetImpuestos(_conceptos);

			//Para marcar la propiedad como Dirty
			Impuestos = Impuestos;

			//Descuento = BaseImponible * PDescuento / 100;

			BaseImponible -= Descuento;
			Total = BaseImponible + Impuestos;
        }

        /// <summary>
        /// Crea los conceptos de factura asociados a un albarán
        /// </summary>
        /// <param name="source"></param>
        public virtual void Compact()
        {
            OutputDeliveryInfo source = this.GetInfo();
			
			ClienteInfo cliente = ClienteInfo.Get(source.OidHolder, false, true);
			if (cliente.Productos == null) cliente.LoadChilds(typeof(ProductoCliente), false);

			Almacen almacen = null;
			ProductInfo producto = null;
            Batch partida = null;
			//ProductoClienteInfo productoCliente = null;
			OutputDeliveryLine concepto = null;

			foreach (OutputDeliveryLineInfo item in source.Conceptos)
			{
				//Productos con control de stock
				if (item.OidPartida != 0)
				{
					concepto = Conceptos.GetItemByBatch(item.OidPartida, item.ETipoFacturacion);
					almacen = Store.Almacen.Get(item.OidAlmacen, false, true, SessionCode);
					almacen.LoadPartidasByProducto(item.OidProducto, true);

					partida = almacen.Partidas.GetItem(item.OidPartida);

					if (concepto != null)
					{
						//Si no es nuevo Lo marcamos como sucio para que se actualice el stock al guardar este concepto de albarán
						if (!concepto.IsNew) concepto.MarkItemDirty();

						if (item.Oid != concepto.Oid)
						{
							producto = ProductInfo.Get(item.OidProducto, false, true);

							concepto.CantidadKilos += item.CantidadKilos;
							concepto.CantidadBultos += item.CantidadBultos;

							//productoCliente = cliente.Productos.GetByProducto(item.OidProducto);
							//concepto.FacturacionBulto = (productoCliente != null) ? productoCliente.FacturacionBulto : false;

							concepto.Precio = (concepto.Precio + item.Precio) / 2;
							//concepto.Precio = cliente.GetPrecio(producto, partida.GetInfo(false), concepto.ETipoFacturacion);

							Conceptos.Remove(item.Oid);
						}
					}

					//Actualizamos la linea de stock del Almacen asociado
					//Tenemos que devolver el stock a mano porque lo necesitamos devolver para que este disponible
					//al comprobarlo el ConceptoAlbaran.Update
					almacen.LoadStockByPartida(item.OidPartida, false, true);
					Stock stock = almacen.Stocks.GetItemByOutputDeliveryLine(item.Oid);
					stock.Bultos = 0;
					stock.Kilos = 0;
					almacen.UpdateStocks(partida, true);
				}
				else
				{
					concepto = Conceptos.GetItemByProduct(item.OidProducto, item.ETipoFacturacion);

					if (concepto != null)
					{
						if (item.Oid != concepto.Oid)
						{
							producto = ProductInfo.Get(item.OidProducto, false, true);

							concepto.CantidadKilos += item.CantidadKilos;
							concepto.CantidadBultos += item.CantidadBultos;

							//productoCliente = cliente.Productos.GetByProducto(item.OidProducto);
							//concepto.FacturacionBulto = (productoCliente != null) ? productoCliente.FacturacionBulto : false;

							concepto.Precio = (concepto.Precio + item.Precio) / 2;
							//concepto.Precio = cliente.GetPrecio(producto, null, concepto.ETipoFacturacion);

							Conceptos.Remove(item.Oid);
						}
					}
				}
			}

			CalculateTotal();
        }

		public virtual void Merge(OutputDeliveryInfo source)
		{
			ClienteInfo cliente = ClienteInfo.Get(OidHolder, false, true);
			if (cliente.Productos == null) cliente.LoadChilds(typeof(ProductoCliente), false);

			Almacen almacen = null;
			ProductInfo producto = null;
            Batch partida = null;
			//ProductoClienteInfo productoCliente = null;
			OutputDeliveryLine concepto = null;

			foreach (OutputDeliveryLineInfo item in source.Conceptos)
			{
				//Productos con control de stock
				if (item.OidPartida != 0)
				{
					concepto = Conceptos.GetItemByBatch(item.OidPartida, item.ETipoFacturacion);

					if ((concepto != null) && (item.Oid != concepto.Oid))
					{
						concepto.CantidadKilos += item.CantidadKilos;
						concepto.CantidadBultos += item.CantidadBultos;

						//productoCliente = cliente.Productos.GetByProducto(item.OidProducto);
						//concepto.FacturacionBulto = (productoCliente != null) ? productoCliente.FacturacionBulto : false;

						almacen = Store.Almacen.Get(item.OidAlmacen, false, true, SessionCode);
						almacen.LoadPartidasByProducto(item.OidProducto, true);

						producto = ProductInfo.Get(item.OidProducto, false, true);
						partida = almacen.Partidas.GetItem(item.OidPartida);
						concepto.Precio = cliente.GetPrecio(producto, null, concepto.ETipoFacturacion);

						//Borramos la linea de stock del Almacen asociado
						//Tenemos que devolver el stock a mano porque lo necesitamos devolver para que este disponible
						//al comprobarlo el ConceptoAlbaran.Update
                        almacen.LoadStockByPartida(item.OidPartida, false, true);
                        almacen.RemoveStock(almacen.Stocks.GetItemByOutputDeliveryLine(item.Oid));
					}
					else
					{
						//Lo pasamos de albaran manteniendo el OID porque hay un concepto de factura asociado
						//y lo marcamos como dirty para que modifique el stock asociado y no lo añada nuevo
						concepto = Conceptos.CopyItem(this, item);
					}
				}
				else
				{
					concepto = Conceptos.GetItemByProduct(item.OidProducto, item.ETipoFacturacion);

					if ((concepto != null) && (item.Oid != concepto.Oid))
					{
						concepto.CantidadKilos += item.CantidadKilos;
						concepto.CantidadBultos += item.CantidadBultos;

						//productoCliente = cliente.Productos.GetByProducto(item.OidProducto);
						//concepto.FacturacionBulto = (productoCliente != null) ? productoCliente.FacturacionBulto : false;

						producto = ProductInfo.Get(item.OidProducto, false, true);
						concepto.Precio = cliente.GetPrecio(producto, null, concepto.ETipoFacturacion);
					}
					else
					{
						//Lo pasamos de albaran manteniendo el OID porque hay un concepto de factura asociado
						concepto = Conceptos.CopyItem(this, item);
					}
				}
			}

			CalculateTotal();
		}

		public virtual void Insert(PedidoInfo source)
		{
			OutputDeliveryLine newitem;

			if (source.Lineas == null) source.LoadPendiente();

			foreach (LineaPedidoInfo item in source.Lineas)
			{
				if (item.Pendiente == 0) continue;

				newitem = Conceptos.NewItem(this);
				newitem.CopyFrom(item);
			}

			CalculateTotal();
		}

		public virtual void Extract(PedidoInfo source)
		{
			//if (source.ConceptoAlbaranes == null) source.LoadChilds(typeof(ConceptoAlbaranes), true);

			/*AlbaranFacturas.Remove(this, source);

			foreach (ConceptoAlbaranInfo item in source.ConceptoAlbaranes)
				ConceptoFacturas.Remove(item);

			CalculaTotal();*/
		}

		public virtual void SetAlmacen(StoreInfo source)
		{
			OidAlmacen = (source != null) ? source.Oid : 0;
			IDAlmacen = (source != null) ? source.Codigo : string.Empty;
			Almacen = (source != null) ? source.Nombre : string.Empty;

			foreach (OutputDeliveryLine item in Conceptos)
			{
				item.OidAlmacen = (source != null) ? source.Oid : 0;
				item.Almacen = (source != null) ? source.Codigo : string.Empty;
			}
		}
		public virtual void SetExpediente(ExpedientInfo source)
		{
			OidExpediente = (source != null) ? source.Oid : 0;
			Expediente = (source != null) ? source.Codigo : string.Empty;

			foreach (OutputDeliveryLine item in Conceptos)
			{
				item.OidExpediente = (source != null) ? source.Oid : 0;
				item.Expediente = (source != null) ? source.Codigo : string.Empty;
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

			return true;
		}	

        #endregion

        #region Autorization Rules

        public static bool CanAddObject()
        {
            return OutputInvoice.CanAddObject();
        }
        public static bool CanGetObject()
        {
			return OutputInvoice.CanGetObject();
        }
        public static bool CanDeleteObject()
        {
			return OutputInvoice.CanDeleteObject();
        }        
        public static bool CanEditObject()
        {
			return OutputInvoice.CanEditObject();
        }

		public static void IsPosibleDelete(long oid, ETipoEntidad holderType)
		{
			QueryConditions conditions = new QueryConditions
			{
				OutputDelivery = OutputDelivery.New().GetInfo(false),
				Estado = EEstado.NoAnulado
			};
			conditions.OutputDelivery.Oid = oid;

			OutputDeliveryInfo item = OutputDeliveryInfo.Get(oid, holderType, false);
			
			if (item == null) return;

			if (item.EEstado != EEstado.Abierto)
				throw new iQException(Resources.Messages.ALBARAN_NO_ANULADO);

			TicketInfo ticket = TicketInfo.GetByAlbaran(oid, false);

			if ( ticket != null && (ticket.Oid != 0) && (ticket.EEstado != EEstado.Oculto))
				throw new iQException(Resources.Messages.TICKETS_ASOCIADOS);
		}

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        protected OutputDelivery()
        {
        }

		public virtual OutputDeliveryInfo GetInfo() { return GetInfo(true); }
        public virtual OutputDeliveryInfo GetInfo(bool get_childs) 
		{
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new OutputDeliveryInfo(this, get_childs);
        }

        #endregion

        #region Root Factory Methods

        public static OutputDelivery New(int sessionCode = -1)
        {
			return New(ETipoEntidad.Cliente, sessionCode);
        }
		public static OutputDelivery New(ETipoEntidad holderType, int sessionCode = -1)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            OutputDelivery obj = DataPortal.Create<OutputDelivery>(new CriteriaCs(-1));
			obj.EHolderType = holderType;
			if (holderType == ETipoEntidad.WorkReport) obj.OidSerie = Library.Invoice.ModulePrincipal.GetWorkDeliverySerieSetting();
			obj.SetSharedSession(sessionCode);
			obj.GetNewCode(holderType, ETipoAlbaranes.Todos);
			return obj;
        }
        
		public new static OutputDelivery Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return BusinessBaseEx<OutputDelivery>.Get(query, childs, sessionCode);
		}
		public static OutputDelivery Get(QueryConditions conditions, bool childs)
		{
			return Get(SELECT(conditions, true), childs);
		}

		public static OutputDelivery Get(long oid, ETipoEntidad holderType, bool childs = true, int sessionCode = -1) 
		{
			QueryConditions conditions = new QueryConditions
			{
				OutputDelivery = OutputDeliveryInfo.New(oid),
				TipoEntidad = holderType
			};
			return Get(SELECT(conditions, true), childs, sessionCode); 
		}
		public static OutputDelivery Get(long oid, ETipoEntidad holderType, bool childs, bool cache) { return Get(oid, holderType, childs, cache, -1); }
		public static OutputDelivery Get(long oid, ETipoEntidad holderType, bool childs, bool cache, int sessionCode)
        {
            OutputDelivery item;

			if (!cache) return Get(oid, holderType, childs);

            //No está en la cache de listas
            if (!Cache.Instance.Contains(typeof(OutputDeliveries)))
            {
                OutputDeliveries items = OutputDeliveries.NewList();

				item = sessionCode == -1 ? OutputDelivery.GetChild(oid, holderType, childs) : OutputDelivery.GetChild(sessionCode, oid, childs);
                items.AddItem(item);
                items.SessionCode = item.SessionCode;
                Cache.Instance.Save(typeof(OutputDeliveries), items);
            }
            else
            {
                OutputDeliveries items = Cache.Instance.Get(typeof(OutputDeliveries)) as OutputDeliveries;
                item = items.GetItem(oid);

                //No está en la lista de la cache de listas
                if (item == null)
                {
                    item = OutputDelivery.GetChild(items.SessionCode, oid, childs);
                    items.AddItem(item);
                    Cache.Instance.Save(typeof(OutputDeliveries), items);
                }
            }

            return item;
        }

		public static OutputDelivery GetBySubscription(Library.QueryConditions conditions, bool childs)
		{
			return Get(SELECT_BY_SUBSCRIPTION(conditions, true), childs);
		}

		public static void Delete(long oid, ETipoEntidad holderType, int sessionCode = -1)
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			IsPosibleDelete(oid, holderType);

            DataPortal.Delete(new CriteriaCs(oid, sessionCode));
        }

        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = OutputDelivery.OpenSession();
            ISession sess = OutputDelivery.Session(sessCode);
            ITransaction trans = OutputDelivery.BeginTransaction(sessCode);

            try
            {
				sess.Delete("from OutputDeliveryRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                OutputDelivery.CloseSession(sessCode);
            }
        }

        public static void DeleteFromList(List<OutputDeliveryInfo> list)
        {
			//Obtenemos la sesión
            int sessCode = OutputDelivery.OpenSession();
            ISession sess = OutputDelivery.Session(sessCode);
            ITransaction trans = OutputDelivery.BeginTransaction(sessCode);            

            string oidAlbaranes = "0";

            try
            {
                foreach (OutputDeliveryInfo item in list)
                    oidAlbaranes += "," + item.Oid;

				//El stock debe borrarlo el almacen porque algunos se cambian de albaran y no podemos eliminarlos
				sess.Delete("from OutputDeliveryRecord ab where ab.Oid in (" + oidAlbaranes + ")");

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                OutputDelivery.CloseSession(sessCode);
            }
        }

        public override OutputDelivery Save()
        {
            // Por interfaz Root/Child
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
#if TRACE
				ControlerBase.AppControler.Timer.Start();
#endif
                base.Save();
#if TRACE
				ControlerBase.AppControler.Timer.Record("Albaran.Save()");
#endif
                // Update de las listas.
                _conceptos.Update(this);

				//Actualizacion de Stocks
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
                            ControlerBase.AppControler.Timer.Record("Save de los Albarans");
#endif
                        }
                    }

					almacenes.SaveAsChild();
#if TRACE
					ControlerBase.AppControler.Timer.Record("almacenes.Save()");
#endif
				}

                if (!SharedTransaction) Transaction().Commit();
#if TRACE
				ControlerBase.AppControler.Timer.Record("Albaran.Commit()");
#endif
                return this;
            }
            catch (Exception ex)
            {
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return null;
            }
            finally
            {
				Cache.Instance.Remove(typeof(Stores));
				Cache.Instance.Remove(typeof(Expedients));
                Cache.Instance.Remove(typeof(LivestockBooks));

				if (!SharedTransaction)
				{
					if (CloseSessions /*&& (this.IsNew || Transaction().WasCommitted)*/) CloseSession();
					else BeginTransaction();
				}
            }
        }

        public override OutputDelivery SaveAsChild()
        {
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

                _conceptos.Update(this);

                //Actualizacion de Stocks
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
                                LivestockBook libro = LivestockBook.Get(1, true, true);
                                if (libro != null) libro.SaveAsChild();
                            }

                            expedientes.SaveAsChild();
#if TRACE
							ControlerBase.AppControler.Timer.Record("Save de los Albarans");
#endif
                        }
                    }

                    almacenes.SaveAsChild();
#if TRACE
					ControlerBase.AppControler.Timer.Record("almacenes.Save()");
#endif
                }

                return this;
            }
            catch (Exception ex)
            {
                //if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally { }
        }

        #endregion

		#region Child Factory Methods

		private OutputDelivery(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(source);
        }

		public static OutputDelivery NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			OutputDelivery obj = DataPortal.Create<OutputDelivery>(new CriteriaCs(-1));
			obj.MarkAsChild();

			return obj;
		}

		internal static OutputDelivery GetChild(long oid, ETipoEntidad holderType, bool childs)
		{
			OutputDelivery obj = Get(oid, holderType, childs);
			obj.MarkAsChild();

			return obj;
		}
		internal static OutputDelivery GetChild(int sessionCode, IDataReader source) { return GetChild(sessionCode, source, false); }
		internal static OutputDelivery GetChild(int sessionCode, IDataReader reader, bool childs) { return new OutputDelivery(sessionCode, reader, childs); }

		public static OutputDelivery GetChild(int sessionCode, long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(oid);

			OutputDelivery obj = DataPortal.Fetch<OutputDelivery>(criteria);
			obj.MarkAsChild();

			return obj;
		}

		#endregion

		#region Common Data Access

		[RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
			Oid = Convert.ToInt64((new Random()).Next());
			OidSerie = Library.Invoice.ModulePrincipal.GetDefaultSerieSetting();
			OidAlmacen = Library.Store.ModulePrincipal.GetDefaultAlmacenSetting();
			EEstado = EEstado.Abierto;
			OidUsuario = (AppContext.User != null) ? AppContext.User.Oid : 0;
			Usuario = (AppContext.User != null) ? AppContext.User.Name : string.Empty;
			EEstado = EEstado.Abierto;
            Contado = false;
            EMedioPago = EMedioPago.Efectivo;
			EFormaPago = EFormaPago.Contado;
			Fecha = DateTime.Now;
			GetNewCode(ETipoEntidad.Cliente, ETipoAlbaranes.Todos);
        }

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(OutputDelivery source)
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

						OutputDeliveryLine.DoLOCK(Session());
						query = OutputDeliveryLines.SELECT(this);
						reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_conceptos = OutputDeliveryLines.GetChildList(SessionCode, reader);
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
					IDataReader reader;
					string query;

					OutputDeliveryLine.DoLOCK(Session());
					query = OutputDeliveryLines.SELECT(this);
					reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_conceptos = OutputDeliveryLines.GetChildList(SessionCode, reader);
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Insert(OutputDeliveries parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);

				_conceptos.Update(this);

			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Update(OutputDeliveries parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				OutputDeliveryRecord obj = Session().Get<OutputDeliveryRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);

				_conceptos.Update(this);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void DeleteSelf(OutputDeliveries parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<OutputDeliveryRecord>(Oid));
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

                if (nHMng.UseDirectSQL)
                {
                    DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        OutputDeliveryLine.DoLOCK(Session());
                        query = OutputDeliveryLines.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _conceptos = OutputDeliveryLines.GetChildList(SessionCode, reader);
                    }
                }
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
            if (!IDManual)
            {       
                if (Contado)
					GetNewCode(EHolderType, ETipoAlbaranes.Agrupados); 
                else
					GetNewCode(EHolderType, ETipoAlbaranes.Todos);
            }
            else if 
				(Contado) SetNewCode(ETipoAlbaranes.Agrupados);
            else 
				SetNewCode(ETipoAlbaranes.Todos);

            if (!SharedTransaction)
            {
                if (SessionCode < 0) SessionCode = OpenSession();
                BeginTransaction();
            }

            Session().Save(Base.Record);

			if (EHolderType == ETipoEntidad.WorkReport)
			{
				WorkReport w_report = WorkReport.Get(Oid, true, true, SessionCode);
				
				if (w_report == null) return;

				if (w_report.Lines.GetItem(this) == null)
				{
					w_report.Lines.NewItem(w_report, this);

					//Si es nuevo o ya existía en caché viene desde la creación de un WorkReport
					if (w_report.IsNew) return;
					if (!w_report.SharedTransaction) return;

					w_report.Save();
				}
			}
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Update()
        {
            if (IsDirty)
            {
				if (IDManual)
					if (Contado) SetNewCode(ETipoAlbaranes.Agrupados);
                    else SetNewCode(ETipoAlbaranes.Todos);

                try
                {
					OutputDeliveryRecord obj = Session().Get<OutputDeliveryRecord>(Oid);
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
				if (criterio.SessionCode >= 0)
				{
					SessionCode = criterio.SessionCode;
					SharedTransaction = true;
				}
				else
				{
					SessionCode = OpenSession();
					BeginTransaction();
				}

				OutputDelivery obj = OutputDelivery.Get(criterio.Oid, EHolderType, true, SessionCode);

				if (obj == null) return;

                obj.BeginEdit();
                obj.Conceptos.Clear();
                obj.ApplyEdit();
                obj.Save();

                //Si no hay integridad referencial, aqui se deben borrar las listas hijo
				Session().Delete(Session().Get<OutputDeliveryRecord>(obj.Oid));

				if (!SharedTransaction) Transaction().Commit();
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
				if (!SharedTransaction) CloseSession();
            }
        }

        #endregion

        #region SQL

		internal enum EQueryType { GENERAL = 0, CLUSTERED = 1, WORK = 2 }

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

		internal static string SELECT_FIELDS(EQueryType queryType, QueryConditions conditions)
        {
			string query = @"
				SELECT " + (long)queryType + @" AS ""QUERY_TYPE""
					,A.*
					,S.""IDENTIFICADOR"" AS ""N_SERIE""
					,S.""NOMBRE"" AS ""SERIE""
					,COALESCE(US.""NAME"", '') AS ""USUARIO""
					,COALESCE(AL.""NOMBRE"", '') AS ""ALMACEN""
					,COALESCE(AL.""CODIGO"", '') AS ""ID_ALMACEN""
					,AF.""FACTURAS"" AS ""FACTURAS""
					,AF.""OID_FACTURA"" AS ""OID_FACTURA""
					,FC.""CODIGO"" AS ""N_FACTURA""";

			switch (queryType)
			{
				case EQueryType.GENERAL:

					query += @"
						,C.""NOMBRE"" AS ""CLIENTE""
						,C.""CODIGO"" AS ""ID_CLIENTE""
						,AT.""TICKETS"" AS ""TICKETS""
						,AT.""OID_TICKET"" AS ""OID_TICKET""
						,TK.""CODIGO"" AS ""N_TICKET""";
					break;

				case EQueryType.WORK:

					query += @"
						,EX.""TIPO_MERCANCIA"" AS ""CLIENTE""
						,EX.""CODIGO"" AS ""ID_CLIENTE""
						,0 AS ""TICKETS""
						,0 AS ""OID_TICKET""
						,'' AS ""N_TICKET""";
					break;

				case EQueryType.CLUSTERED:

					query = @"    
						SELECT " + (long)queryType + @" AS ""QUERY_TYPE""
							,A.*                    
							,DATE_TRUNC('" + conditions.Step.ToString() + @"', A.""DATE"") AS ""STEP""
							,SUM(A.""TOTAL"") AS ""TOTAL""";
					break;
			}

            return query;
        }

		internal static string INNER(QueryConditions conditions)
		{
			string a = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryRecord));
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
			string se = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string af = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryInvoiceRecord));
			string at = nHManager.Instance.GetSQLTable(typeof(DeliveryTicketRecord));
			string ca = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryLineRecord));
			string fc = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string tk = nHManager.Instance.GetSQLTable(typeof(TicketRecord));
			string al = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));

			string query;

			query = @"
				FROM " + a + @" AS A
				INNER JOIN " + se + @" AS S ON S.""OID"" = A.""OID_SERIE""
				LEFT JOIN " + us + @" AS US ON US.""OID"" = A.""OID_USUARIO""
				LEFT JOIN " + al + @" AS AL ON AL.""OID"" = A.""OID_ALMACEN""
				LEFT JOIN (	SELECT ""OID_ALBARAN"", ""OID_FACTURA"", COUNT(""OID_FACTURA"") AS ""FACTURAS""
							FROM " + af + @" GROUP BY ""OID_ALBARAN"", ""OID_FACTURA"")
					AS AF ON AF.""OID_ALBARAN"" = A.""OID""
				LEFT JOIN " + fc + @" AS FC ON FC.""OID"" = AF.""OID_FACTURA""
				LEFT JOIN (	SELECT ""OID_ALBARAN"", MIN(""OID_TICKET"") AS ""OID_TICKET"", COUNT(""OID_TICKET"") AS ""TICKETS""
							FROM " + at + @" GROUP BY ""OID_ALBARAN"")
					AS AT ON AT.""OID_ALBARAN"" = A.""OID""
				LEFT JOIN " + tk + @" AS TK ON TK.""OID"" = AT.""OID_TICKET""";

			if (conditions.Producto != null)
				query += @"
					INNER JOIN " + ca + @" AS CA ON CA.""OID_PRODUCTO"" = " + conditions.Producto.Oid;

			if (conditions.Partida != null)
				query += @"
					INNER JOIN " + ca + @" AS CA ON CA.""OID_BATCH"" = " + conditions.Partida.Oid;

			if (conditions.ConceptoAlbaran != null)
				query += @"
					INNER JOIN " + ca + @" AS CA ON CA.""OID"" = " + conditions.ConceptoAlbaran.Oid;

			switch (conditions.TipoAlbaranes)
			{
				case ETipoAlbaranes.Facturados:
					query += @"
						INNER JOIN " + af + @" AF1 ON AF1.""OID_ALBARAN"" = A.""OID""";
					break;

				case ETipoAlbaranes.Ticket:
					query += @"
						INNER JOIN " + at + @" AT1 ON AT1.""OID_ALBARAN"" = A.""OID""";
					break;
			}

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "A", ForeignFields());

			query += @"
                 AND (A.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			query += EntityBase.ESTADO_CONDITION(conditions.Estado, "A");
			query += EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "	A");

			if (conditions.Usuario != null) 
				query += @"
					AND A.""OID_USUARIO"" = " + conditions.Usuario.Oid;

			if (conditions.OutputDelivery != null)
				if (conditions.OutputDelivery.Oid != 0)
					query += @"
						AND A.""OID"" = " + conditions.OutputDelivery.Oid;

			if (conditions.Serie != null) 
				query += @"
					AND A.""OID_SERIE"" = " + conditions.Serie.Oid;

            if (conditions.MedioPago != EMedioPago.Todos) 
				query += @"
					AND A.""MEDIO_PAGO"" = " + (long)conditions.MedioPago;

            if (conditions.MedioPagoList != null && conditions.MedioPagoList.Count > 0)
                query += EntityBase.GET_IN_LIST_CONDITION(conditions.MedioPagoList, "A", "MEDIO_PAGO");

			if (conditions.Factura != null) 
				query += @"
					AND AF.""OID_FACTURA"" = " + conditions.Factura.Oid;

			if (conditions.Ticket != null) 
				query += @"
					AND AT.""OID_TICKET"" = " + conditions.Ticket.Oid;
			
			if (conditions.TipoAlbaranes != ETipoAlbaranes.Todos)
			{
				switch (conditions.TipoAlbaranes)
				{
					case ETipoAlbaranes.Agrupados:
						query += @"
							AND A.""CONTADO"" = TRUE";
						break;

					case ETipoAlbaranes.NoFacturados:
						{
							string af = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryInvoiceRecord));

							query += @"
								AND A.""OID"" NOT IN (SELECT TAF1.""OID_ALBARAN"" FROM " + af + @" AS TAF1)";
						}
						break;

					case ETipoAlbaranes.NoTicket:
						{
							string at = nHManager.Instance.GetSQLTable(typeof(DeliveryTicketRecord));
							string af = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryInvoiceRecord));

							query += @"
								AND A.""OID"" NOT IN (SELECT AF.""OID_ALBARAN"" FROM " + af + @" AS AF)
								AND A.""OID"" NOT IN (SELECT AT.""OID_ALBARAN"" FROM " + at + @" AS AT)";
						}
						break;
				}
			}

			if (conditions.TipoFactura != ETipoFactura.Todas)
			{
				bool value = (conditions.TipoFactura == ETipoFactura.Ordinaria) ? false : true;
				query += @"
					AND A.""RECTIFICATIVO"" = " + value.ToString().ToUpper();
			}

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query = string.Empty;

			switch (conditions.TipoEntidad)
			{
				case ETipoEntidad.WorkReport:
					{
						query = SELECT_WORK(conditions);
					}
					break;

				case ETipoEntidad.Todos:
					{
						query =
							SELECT_CLIENT(conditions) + @" 
							UNION " +
							SELECT_WORK(conditions);
					}
					break;

				case ETipoEntidad.Cliente:
				default:
					{
						query = SELECT_CLIENT(conditions);
					}
					break;
			}

			if (conditions != null)
			{
				if (conditions.Step != EStepGraph.None)
				{
					query += @"
					GROUP BY ""STEP""
					ORDER BY ""STEP""";
				}
				else
				{
					if (conditions.Orders != null)
						query += ORDER(conditions.Orders, string.Empty, ForeignFields());
					else
						query += @"
							ORDER BY ""FECHA"", ""N_SERIE"", ""CODIGO""";
					query += LIMIT(conditions.PagingInfo);
				}
			}
			else
				query += @"
					ORDER BY ""FECHA"", ""N_SERIE"", ""CODIGO""";

			//if (lock_table) query += " FOR UPDATE OF A NOWAIT";

			return query;
		}
        
		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters
			};
			return SELECT(conditions, lockTable);
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { OutputDelivery = OutputDeliveryInfo.New(oid) };

			query = SELECT(conditions, false);

			query += EntityBase.LOCK("A", lockTable);

			return query;
		}

		//Arreglar esta funcion para que haga un union entre los tipos de entidad
		public static string SELECT_BY_LIST(List<long> oidList, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions()
			{
				TipoEntidad = ETipoEntidad.Cliente,
				OidList = oidList
			};

			string query =
				SELECT_CLIENT(conditions);

			query += EntityBase.LOCK("A", lockTable);

			return query;
		}

		internal static string SELECT_PENDIENTES_CONTADO(QueryConditions conditions, bool lockTable)
		{
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string af = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryInvoiceRecord));

			string query = string.Empty;

			conditions.ExtraJoin = @"
				LEFT JOIN " + cl + @" AS C ON C.""OID"" = A.""OID_HOLDER"" AND A.""HOLDER_TYPE"" = " + (long)ETipoEntidad.Cliente + @"";

			conditions.TipoAlbaranes = ETipoAlbaranes.Agrupados;

			conditions.ExtraWhere = @"
				AND A.""OID"" NOT IN (SELECT TAF1.""OID_ALBARAN"" FROM " + af + @" AS TAF1)";

			query = 
				SELECT_FIELDS(EQueryType.GENERAL, conditions) +	
				INNER(conditions) +
				WHERE(conditions);

			query += @"
				ORDER BY A.""FECHA"", S.""IDENTIFICADOR"", A.""CODIGO""";

			//if (lockTable) query += " FOR UPDATE OF A NOWAIT";

			return query;
		}

		public static string SELECT_BY_SUBSCRIPTION(Library.QueryConditions conditions, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conds = new QueryConditions();

			query =
				SELECT_FIELDS(EQueryType.GENERAL, conds) +
				INNER(conds) + 
				conditions.Query +
				WHERE(conds);

			query += EntityBase.LOCK("A", lockTable);

			return query;
		}

		internal static string SELECT_CLIENT(QueryConditions conditions)
		{
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));

			string query = string.Empty;

			conditions.ExtraJoin = @"
				LEFT JOIN " + cl + @" AS C ON C.""OID"" = A.""OID_HOLDER"" AND A.""HOLDER_TYPE"" = " + (long)ETipoEntidad.Cliente + @"";

			conditions.ExtraWhere = @"
				AND (S.""TIPO"" IN (" + (long)ETipoSerie.Venta + @"," + (long)ETipoSerie.CompraVenta + "))";

			if (conditions.Cliente != null)
				conditions.ExtraWhere += @"
					AND A.""OID_HOLDER"" = " + conditions.Cliente.Oid + @" AND A.""HOLDER_TYPE"" = " + (long)ETipoEntidad.Cliente;

			query = SELECT_FIELDS(EQueryType.GENERAL, conditions) +
					INNER(conditions) +
					WHERE(conditions);

			return query;
		}

		internal static string SELECT_WORK(QueryConditions conditions)
		{
			string wr = nHManager.Instance.GetSQLTable(typeof(WorkReportRecord));
			string ws = nHManager.Instance.GetSQLTable(typeof(WorkReportResourceRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query = string.Empty;

			conditions.ExtraJoin = @"	
				INNER JOIN " + wr + @" AS WR ON WR.""OID"" = A.""OID_HOLDER"" AND A.""HOLDER_TYPE"" = " + (long)ETipoEntidad.WorkReport + @"
				INNER JOIN " + ex + @" AS EX ON EX.""OID"" = WR.""OID_EXPEDIENT""";

			conditions.ExtraWhere = @"
				AND S.""TIPO"" = " + (long)ETipoSerie.Work;

			if (conditions.WorkReport != null)
				conditions.ExtraWhere = @"
					AND A.""OID_HOLDER"" = " + conditions.WorkReport.Oid + @" AND A.""HOLDER_TYPE"" = " + (long)ETipoEntidad.WorkReport;

			query = SELECT_FIELDS(EQueryType.WORK, conditions) +
					INNER(conditions) +
					WHERE(conditions);

			return query;
		}

        #endregion
    }
}