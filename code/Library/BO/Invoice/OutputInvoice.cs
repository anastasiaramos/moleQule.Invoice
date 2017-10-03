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
	public class OutputInvoiceRecord : RecordBase
	{
		#region Attributes

		private long _oid_serie;
		private long _oid_cliente;
		private long _oid_transportista;
		private long _serial;
		private string _codigo = string.Empty;
		private string _vat_number = string.Empty;
		private string _cliente = string.Empty;
		private string _direccion = string.Empty;
		private string _codigo_postal = string.Empty;
		private string _provincia = string.Empty;
		private string _municipio = string.Empty;
		private long _ano = DateTime.Today.Year;
		private DateTime _fecha = DateTime.Today;
		private long _forma_pago;
		private long _dias_pago;
		private long _medio_pago;
		private DateTime _prevision_pago = DateTime.Today;
		private Decimal _base_imponible;
		private Decimal _impuestos;
		private Decimal _p_descuento;
		private Decimal _descuento;
		private Decimal _total;
		private string _cuenta_bancaria = string.Empty;
		private bool _nota = false;
		private string _observaciones = string.Empty;
		private bool _albaran = false;
		private bool _rectificativa = false;
		private long _estado;
		private Decimal _p_irpf;
		private string _albaranes = string.Empty;
		private string _id_mov_contable = string.Empty;
		private long _estado_cobro = (long)EEstado.Pendiente;
		private long _oid_usuario;

		#endregion

		#region Properties

		public virtual long OidSerie { get { return _oid_serie; } set { _oid_serie = value; } }
		public virtual long OidCliente { get { return _oid_cliente; } set { _oid_cliente = value; } }
		public virtual long OidTransportista { get { return _oid_transportista; } set { _oid_transportista = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual string VatNumber { get { return _vat_number; } set { _vat_number = value; } }
		public virtual string Cliente { get { return _cliente; } set { _cliente = value; } }
		public virtual string Direccion { get { return _direccion; } set { _direccion = value; } }
		public virtual string CodigoPostal { get { return _codigo_postal; } set { _codigo_postal = value; } }
		public virtual string Provincia { get { return _provincia; } set { _provincia = value; } }
		public virtual string Municipio { get { return _municipio; } set { _municipio = value; } }
		public virtual long Ano { get { return _ano; } set { _ano = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual long FormaPago { get { return _forma_pago; } set { _forma_pago = value; } }
		public virtual long DiasPago { get { return _dias_pago; } set { _dias_pago = value; } }
		public virtual long MedioPago { get { return _medio_pago; } set { _medio_pago = value; } }
		public virtual DateTime Prevision { get { return _prevision_pago; } set { _prevision_pago = value; } }
		public virtual Decimal BaseImponible { get { return Decimal.Round(_base_imponible, 2); } set { _base_imponible = value; } }
		public virtual Decimal Impuestos { get { return Decimal.Round(_impuestos, 2); } set { _impuestos = value; } }
		public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
		public virtual Decimal Descuento { get { return Decimal.Round(_descuento, 2); } set { _descuento = value; } }
        public virtual Decimal Total { get { return Decimal.Round(_total, 2); } set { _total = value; } }
		public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
		public virtual bool Nota { get { return _nota; } set { _nota = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual bool Agrupada { get { return _albaran; } set { _albaran = value; } }
		public virtual bool Rectificativa { get { return _rectificativa; } set { _rectificativa = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual Decimal PIrpf { get { return _p_irpf; } set { _p_irpf = value; } }
		public virtual string Albaranes { get { return _albaranes; } set { _albaranes = value; } }
		public virtual string IdMovContable { get { return _id_mov_contable; } set { _id_mov_contable = value; } }
		public virtual long EstadoCobro { get { return _estado_cobro; } set { _estado_cobro = value; } }
		public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }

		#endregion

		#region Business Methods

		public OutputInvoiceRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
			_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
			_oid_transportista = Format.DataReader.GetInt64(source, "OID_TRANSPORTISTA");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_vat_number = Format.DataReader.GetString(source, "VAT_NUMBER");
			_cliente = Format.DataReader.GetString(source, "CLIENTE");
			_direccion = Format.DataReader.GetString(source, "DIRECCION");
			_codigo_postal = Format.DataReader.GetString(source, "CODIGO_POSTAL");
			_provincia = Format.DataReader.GetString(source, "PROVINCIA");
			_municipio = Format.DataReader.GetString(source, "MUNICIPIO");
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
			_albaran = Format.DataReader.GetBool(source, "ALBARAN");
			_rectificativa = Format.DataReader.GetBool(source, "RECTIFICATIVA");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_p_irpf = Format.DataReader.GetDecimal(source, "P_IRPF");
			_albaranes = Format.DataReader.GetString(source, "ALBARANES");
			_id_mov_contable = Format.DataReader.GetString(source, "ID_MOV_CONTABLE");
			_estado_cobro = Format.DataReader.GetInt64(source, "ESTADO_COBRO");
			_oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");

		}
		public virtual void CopyValues(OutputInvoiceRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_serie = source.OidSerie;
			_oid_cliente = source.OidCliente;
			_oid_transportista = source.OidTransportista;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_vat_number = source.VatNumber;
			_cliente = source.Cliente;
			_direccion = source.Direccion;
			_codigo_postal = source.CodigoPostal;
			_provincia = source.Provincia;
			_municipio = source.Municipio;
			_ano = source.Ano;
			_fecha = source.Fecha;
			_forma_pago = source.FormaPago;
			_dias_pago = source.DiasPago;
			_medio_pago = source.MedioPago;
			_prevision_pago = source.Prevision;
			_base_imponible = source.BaseImponible;
			_impuestos = source.Impuestos;
			_p_descuento = source.PDescuento;
			_descuento = source.Descuento;
			_total = source.Total;
			_cuenta_bancaria = source.CuentaBancaria;
			_nota = source.Nota;
			_observaciones = source.Observaciones;
			_albaran = source.Agrupada;
			_rectificativa = source.Rectificativa;
			_estado = source.Estado;
			_p_irpf = source.PIrpf;
			_albaranes = source.Albaranes;
			_id_mov_contable = source.IdMovContable;
			_estado_cobro = source.EstadoCobro;
			_oid_usuario = source.OidUsuario;
		}

		#endregion
	}

	[Serializable()]
	public class OutputInvoiceBase
	{
		#region Attributes

		private OutputInvoiceRecord _record = new OutputInvoiceRecord();

		internal bool _id_manual;
		internal string _n_serie = string.Empty;
		internal string _serie = string.Empty;
		internal string _usuario = string.Empty;
		internal string _id_cliente;
		internal decimal _cobrado = 0;
		protected decimal _pendiente = 0;
		internal decimal _efectos_negociados;
		internal decimal _efectos_devueltos;
		internal decimal _efectos_pendientes_vto;
		internal decimal _pendiente_vencido;
		internal decimal _dudoso_cobro;
		internal decimal _tipo_interes;
		internal decimal _asignado;
		internal DateTime _fecha_asignacion;
		internal DateTime _fecha_cobro;
		internal string _id_pago = string.Empty;
		internal string _activo = Resources.Labels.SET_COBRO;
		internal decimal _acumulado;
		internal string _id_mov_contable = string.Empty;
		internal decimal _total_expediente = 0;
		internal decimal _gastos_demora;
		internal decimal _condiciones_venta;
		internal decimal _precio_coste;
		internal decimal _beneficio;
		internal decimal _p_beneficio;
        internal DateTime _step_date;

		internal ImpuestoResumenList _impuestos_list = new ImpuestoResumenList();

		#endregion

		#region Properties

		public OutputInvoiceRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Estado; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
		public EFormaPago EFormaPago { get { return (EFormaPago)_record.FormaPago; } }
		public string FormaPagoLabel { get { return Common.EnumText<EFormaPago>.GetLabel(EFormaPago); } }
		public EMedioPago EMedioPago { get { return (EMedioPago)_record.MedioPago; } }
		public string MedioPagoLabel { get { return Common.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
		public EEstado EEstadoCobro { get { return (EEstado)_record.EstadoCobro; } set { _record.EstadoCobro = (long)value; } }
		public string EstadoCobroLabel { get { return Common.EnumText<EEstado>.GetLabel(EEstadoCobro); } }

		public string Usuario { get { return _usuario; } set { _usuario = value; } }
		public string NSerieSerie { get { return _n_serie + " - " + _serie; } }
		public string NFactura { get { return _n_serie + "/" + _record.Codigo; } }
		public Decimal Subtotal { get { return Decimal.Round(_record.BaseImponible + _record.Descuento, 2); } }
		public Decimal IRPF { get { return Decimal.Round((_record.BaseImponible * _record.PIrpf) / 100, 2); } }
		public bool Cobrada { get { return (_cobrado >= _record.Total); } }
		public decimal Pendiente { get { return _pendiente; } set { _pendiente = value; } }
		public long DiasTranscurridos
		{
			get
			{
				if (Cobrada)
					if (_fecha_cobro != DateTime.MinValue)
						return _fecha_cobro.Subtract(_record.Fecha).Days;
					else
						return 0;
				else
					return DateTime.Today.Subtract(_record.Fecha).Days;
			}
		}
		public string FileName
		{
			get
			{
				string[] file_name = {"FAC",
                                        AppContext.ActiveSchema.Name.Replace(".", ""),
                                        _record.Cliente.Replace(".","").PadLeft(15).Substring(0,15),
                                        _record.Fecha.ToString("dd-MM-yyyy"),
                                        _n_serie,
                                        _record.Codigo};


				return String.Join<string>("_", file_name) + ".pdf";
			}
		}
		public string Link { get { return ClassMD5.getMd5Hash(FileName + SettingsMng.Instance.GetLinkSeed()); } }
		public string FechaCobro { get { return (_fecha_cobro != DateTime.MinValue) ? _fecha_cobro.ToShortDateString() : "---"; } }
		public string FechaAsignacion { get { return (_fecha_asignacion != DateTime.MinValue) ? _fecha_asignacion.ToShortDateString() : "---"; } set { _fecha_asignacion = DateTime.Parse(value); } }
        public DateTime StepDate { get { return _step_date; } set { _step_date = value; } }
		
		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			long tipo_query = Format.DataReader.GetInt64(source, "TIPO_QUERY");

			switch ((OutputInvoice.ETipoQuery)tipo_query)
			{
				case OutputInvoice.ETipoQuery.BENEFICIO:

					_precio_coste = Format.DataReader.GetDecimal(source, "TOTAL_COSTE");
					_beneficio = Format.DataReader.GetDecimal(source, "BENEFICIO");
					_p_beneficio = Format.DataReader.GetDecimal(source, "P_BENEFICIO");

					_record.CopyValues(source);

					_usuario = Format.DataReader.GetString(source, "USUARIO");
					_n_serie = Format.DataReader.GetString(source, "N_SERIE");
					_serie = Format.DataReader.GetString(source, "SERIE");
					_id_cliente = Format.DataReader.GetString(source, "ID_CLIENTE");
					_cobrado = Format.DataReader.GetDecimal(source, "COBRADO");
					_pendiente = Format.DataReader.GetDecimal(source, "PENDIENTE");
					_efectos_negociados = Format.DataReader.GetDecimal(source, "NO_VENCIDO");
					_efectos_devueltos = Format.DataReader.GetDecimal(source, "VENCIDO");
					_efectos_pendientes_vto = Format.DataReader.GetDecimal(source, "PENDIENTE_VTO");

					_pendiente_vencido = _pendiente + _efectos_devueltos;

					_fecha_cobro = Format.DataReader.GetDateTime(source, "FECHA_COBRO");
					_id_mov_contable = Format.DataReader.GetString(source, "ID_MOVIMIENTO_CONTABLE");

					_id_mov_contable = (_id_mov_contable == "/") ? string.Empty : _id_mov_contable;

					_tipo_interes = Format.DataReader.GetInt64(source, "TIPO_INTERES");

					long dias_exposicion = (_fecha_cobro != DateTime.MinValue) ? _fecha_cobro.Subtract(_record.Prevision).Days
																				: DateTime.Today.Subtract(_record.Prevision).Days;

					_gastos_demora = _record.Total * _tipo_interes * ((dias_exposicion > 0) ? dias_exposicion : 0) / 36000;
					_gastos_demora = _gastos_demora > 0 ? _gastos_demora : 0;
					_condiciones_venta = Format.DataReader.GetDecimal(source, "CONDICIONES_VENTA");

					if (EEstadoCobro != EEstado.DudosoCobro)
						EEstadoCobro = (Cobrada) ? EEstado.Charged : EEstado.Pendiente;
					else
					{
						_dudoso_cobro = _pendiente_vencido;
						_pendiente_vencido = 0;
					}

					_total_expediente = Format.DataReader.GetDecimal(source, "TOTAL_EXPEDIENTE");

					break;

                case OutputInvoice.ETipoQuery.GENERAL:

                    _record.CopyValues(source);

                    _usuario = Format.DataReader.GetString(source, "USUARIO");
                    _n_serie = Format.DataReader.GetString(source, "N_SERIE");
                    _serie = Format.DataReader.GetString(source, "SERIE");
                    _id_cliente = Format.DataReader.GetString(source, "ID_CLIENTE");
                    _cobrado = Format.DataReader.GetDecimal(source, "COBRADO");
                    _pendiente = Format.DataReader.GetDecimal(source, "PENDIENTE");
                    _efectos_negociados = Format.DataReader.GetDecimal(source, "NO_VENCIDO");
                    _efectos_devueltos = Format.DataReader.GetDecimal(source, "VENCIDO");
                    _efectos_pendientes_vto = Format.DataReader.GetDecimal(source, "PENDIENTE_VTO");

                    _pendiente_vencido = _pendiente + _efectos_devueltos;

                    _fecha_cobro = Format.DataReader.GetDateTime(source, "FECHA_COBRO");
                    _id_mov_contable = Format.DataReader.GetString(source, "ID_MOVIMIENTO_CONTABLE");

                    _id_mov_contable = (_id_mov_contable == "/") ? string.Empty : _id_mov_contable;

                    _tipo_interes = Format.DataReader.GetInt64(source, "TIPO_INTERES");

                    dias_exposicion = (_fecha_cobro != DateTime.MinValue) ? _fecha_cobro.Subtract(_record.Prevision).Days
                                                                                : DateTime.Today.Subtract(_record.Prevision).Days;

                    _gastos_demora = _record.Total * _tipo_interes * ((dias_exposicion > 0) ? dias_exposicion : 0) / 36000;
                    _gastos_demora = _gastos_demora > 0 ? _gastos_demora : 0;
                    _condiciones_venta = Format.DataReader.GetDecimal(source, "CONDICIONES_VENTA");

                    if (EEstadoCobro != EEstado.DudosoCobro)
                        EEstadoCobro = (Cobrada) ? EEstado.Charged : EEstado.Pendiente;
                    else
                    {
                        _dudoso_cobro = _pendiente_vencido;
                        _pendiente_vencido = 0;
                    }

                    _total_expediente = Format.DataReader.GetDecimal(source, "TOTAL_EXPEDIENTE");

                    break;

                case OutputInvoice.ETipoQuery.AGRUPADO:

                    _record.Oid = Format.DataReader.GetDateTime(source, "STEP").ToBinary();
                    _record.Total = Format.DataReader.GetDecimal(source, "TOTAL");
                    _step_date = Format.DataReader.GetDateTime(source, "STEP");

                    break;

				case OutputInvoice.ETipoQuery.BY_COBRO:

					_record.CopyValues(source);

					_usuario = Format.DataReader.GetString(source, "USUARIO");
					_n_serie = Format.DataReader.GetString(source, "N_SERIE");
					_serie = Format.DataReader.GetString(source, "SERIE");
					_id_cliente = Format.DataReader.GetString(source, "ID_CLIENTE");
					_cobrado = Format.DataReader.GetDecimal(source, "COBRADO");
					_pendiente = Format.DataReader.GetDecimal(source, "PENDIENTE");
					_efectos_negociados = Format.DataReader.GetDecimal(source, "NO_VENCIDO");
					_efectos_devueltos = Format.DataReader.GetDecimal(source, "VENCIDO");
					_efectos_pendientes_vto = Format.DataReader.GetDecimal(source, "PENDIENTE_VTO");

					_pendiente_vencido = _pendiente + _efectos_devueltos;

					_fecha_cobro = Format.DataReader.GetDateTime(source, "FECHA_COBRO");
					_id_mov_contable = Format.DataReader.GetString(source, "ID_MOVIMIENTO_CONTABLE");

					_id_mov_contable = (_id_mov_contable == "/") ? string.Empty : _id_mov_contable;

					_tipo_interes = Format.DataReader.GetInt64(source, "TIPO_INTERES");

					dias_exposicion = (_fecha_cobro != DateTime.MinValue) ? _fecha_cobro.Subtract(_record.Prevision).Days
																				: DateTime.Today.Subtract(_record.Prevision).Days;

					_gastos_demora = _record.Total * _tipo_interes * ((dias_exposicion > 0) ? dias_exposicion : 0) / 36000;
					_gastos_demora = _gastos_demora > 0 ? _gastos_demora : 0;

					if (EEstadoCobro != EEstado.DudosoCobro)
						EEstadoCobro = (Cobrada) ? EEstado.Charged : EEstado.Pendiente;
					else
					{
						_dudoso_cobro = _pendiente_vencido;
						_pendiente_vencido = 0;
					}

					_total_expediente = Format.DataReader.GetDecimal(source, "TOTAL_EXPEDIENTE");

					break;

				default:

                    _record.CopyValues(source);

					_record.VatNumber = Format.DataReader.GetString(source, "VAT_NUMBER");
					_id_cliente = Format.DataReader.GetString(source, "ID_CLIENTE");
					_record.Cliente = Format.DataReader.GetString(source, "CLIENTE");
					_record.Total = Format.DataReader.GetDecimal(source, "TOTAL");
					_record.BaseImponible = Format.DataReader.GetDecimal(source, "TOTAL_EFECTIVO");

					break;
			}
		}
        public void CopyValues(OutputInvoice source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_usuario = source.Usuario;
			_n_serie = source.NumeroSerie;
			_serie = source.Serie;
			_id_cliente = source.NumeroCliente;
			_id_mov_contable = source.IDMovimientoContable;
			_pendiente = source.Pendiente;
			_cobrado = source.Cobrado;
			_dudoso_cobro = source.DudosoCobro;
			_pendiente_vencido = source.PendienteVencido;
			_total_expediente = source.TotalExpediente;
			_id_manual = source.IDManual;
			_id_mov_contable = source.IDMovimientoContable;
		}
		public void CopyValues(OutputInvoiceInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_usuario = source.Usuario;
			_n_serie = source.NumeroSerie;
			_serie = source.Serie;
			_id_cliente = source.NumeroCliente;
			_id_mov_contable = source.IDMovimientoContable;
			_tipo_interes = source.TipoInteres;
			//_gastos_cobro = source.GastosCobro;
			_gastos_demora = source.GastosDemora;
			_condiciones_venta = source.CondicionesVenta;
			_pendiente = source.Pendiente;
			_cobrado = source.Cobrado;
			_dudoso_cobro = source.DudosoCobro;
			_pendiente_vencido = source.PendienteVencido;
			_total_expediente = source.TotalExpediente;

			_precio_coste = source.PrecioCoste;
			_beneficio = source.Beneficio;
			_p_beneficio = source.PBeneficio;
            _step_date = source.StepDate;
		}

		private void InsertImpuesto(OutputInvoiceLineInfo item)
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
		public Hashtable GetImpuestos(OutputInvoiceLines conceptos)
		{
			try
			{
				_impuestos_list.Clear();
				_record.Impuestos = 0;

				foreach (OutputInvoiceLine item in conceptos)
				{
					InsertImpuesto(item.GetInfo(false));
					_record.Impuestos += item.Impuestos;
				}

				/*_impuestos = */
				_impuestos_list.TotalizeImpuestos();

				return _impuestos_list;
			}
			catch
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_IMPUESTO, NFactura, _record.Cliente));
			}
		}
		public Hashtable GetImpuestos(OutputInvoiceLineList conceptos)
		{
			try
			{
				_impuestos_list.Clear();
				_record.Impuestos = 0;

				foreach (OutputInvoiceLineInfo item in conceptos)
				{
					InsertImpuesto(item);
					_record.Impuestos += item.Impuestos;
				}

				/*_impuestos = */
				_impuestos_list.TotalizeImpuestos();

				return _impuestos_list;
			}
			catch
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_IMPUESTO, NFactura, _record.Cliente));
			}
		}

		#endregion
	}

    /// <summary>
    /// Editable Root Business Object With Editable Child Collection
    /// Editable Child Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
    public class OutputInvoice : BusinessBaseEx<OutputInvoice>, IEntity, IEntidadRegistro
	{
		#region IEntity

		public long EntityType { get { return (long)ETipoEntidad.FacturaEmitida; } }

		#endregion

		#region IEntidadRegistro

		public virtual ETipoEntidad ETipoEntidad { get { return ETipoEntidad.FacturaEmitida; } }
		public virtual string DescripcionRegistro { get { return "FACTURA EMITIDA Nº " + NFactura + " de " + Fecha.ToShortDateString() + " de " + Total.ToString("C2") + " de " + Cliente; } }

		public virtual IEntidadRegistro ISave() { return (IEntidadRegistro)SharedSave(); }
		public virtual IEntidadRegistro IGet(long oid, bool childs) { return (IEntidadRegistro)Get(oid, childs); }

		public void Update(Registro parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
			OutputInvoiceRecord obj = Session().Get<OutputInvoiceRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			MarkOld();
		}

		#endregion

		#region Attributes

		protected OutputInvoiceBase _base = new OutputInvoiceBase();

        private AlbaranFacturas _albaran_facturas = AlbaranFacturas.NewChildList();
        private OutputInvoiceLines _conceptos = OutputInvoiceLines.NewChildList();
        private CobroFacturas _cobro_facturas = CobroFacturas.NewChildList();

        #endregion

        #region Properties

		public OutputInvoiceBase Base { get { return _base; } }

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
		public virtual string Cliente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Cliente;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Cliente.Equals(value))
				{
					_base.Record.Cliente = value;
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
					Prevision = Library.Common.EnumFunctions.GetPrevisionPago(EFormaPago, Fecha, DiasPago);
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
					Prevision = Library.Common.EnumFunctions.GetPrevisionPago(EFormaPago, Fecha, DiasPago);
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
					Prevision = Library.Common.EnumFunctions.GetPrevisionPago(EFormaPago, Fecha, DiasPago);
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
				return _base.Record.Prevision;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Prevision.Equals(value))
				{
					_base.Record.Prevision = value;
					CalculateTotal();
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
					_base.Record.PDescuento = value;
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
					_base.Record.Descuento = value;
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
					_base.Record.Total = value;
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
		public virtual bool AlbaranContado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Agrupada;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Agrupada.Equals(value))
				{
					_base.Record.Agrupada = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Rectificativa
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Rectificativa;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Rectificativa.Equals(value))
				{
					_base.Record.Rectificativa = value;
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
		public virtual Decimal PIRPF
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PIrpf;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.PIrpf.Equals(value))
				{
					_base.Record.PIrpf = Decimal.Round(value, 2);
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

        public virtual AlbaranFacturas AlbaranFacturas
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _albaran_facturas;
            }

            set
            {
                _albaran_facturas = value;
            }

        }
        public virtual OutputInvoiceLines Conceptos
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

        //NO ENLAZADOS
		public virtual EEstado EEstado { get { return _base.EStatus; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual EEstado EEstadoCobro { get { return _base.EEstadoCobro; } set { _base.EEstadoCobro = value; } }
		public virtual string EstadoCobroLabel { get { return _base.EstadoCobroLabel; } }
		public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } set { FormaPago = (long)value; } }
		public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } set { MedioPago = (long)value; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }

		public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
		public virtual string NFactura { get { return _base.NFactura; } }
		public virtual string IDCliente { get { return _base._id_cliente; } set { _base._id_cliente = value; } }
		public virtual string NumeroCliente { get { return _base._id_cliente; } set { _base._id_cliente = value; } } /*DEPRECATED*/
		public virtual string NumeroSerie { get { return _base._n_serie; } set { _base._n_serie = value; PropertyHasChanged(); } }
		public virtual string Serie { get { return _base._serie; } set { _base._serie = value; PropertyHasChanged(); } }
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
		public virtual Decimal Subtotal { get { return _base.Subtotal; } }
		public virtual Decimal IRPF { get { return _base.IRPF; } }
		public virtual Decimal IGIC { get { return Impuestos; } } /* DEPRECATED */
		public virtual bool IDManual { get { return _base._id_manual; } set { _base._id_manual = value; } }
		public virtual string IDMovimientoContable { get { return _base._id_mov_contable; } }
		public virtual bool Cobrada { get { return _base.Cobrada; } }
		public virtual decimal Cobrado { get { return _base._cobrado; } set { _base._cobrado = value; } }
		public virtual decimal Pendiente { get { return _base.Pendiente; } set { _base.Pendiente = value; } }
		public virtual decimal EfectosNegociados { get { return _base._efectos_negociados; } }
		public virtual decimal EfectosDevueltos { get { return _base._efectos_devueltos; } }
		public virtual decimal EfectosPendientesVto { get { return _base._efectos_pendientes_vto; } }
		public virtual decimal PendienteVencido { get { return _base._pendiente_vencido; } set { _base._pendiente_vencido = value; } }
		public virtual decimal DudosoCobro { get { return _base._dudoso_cobro; } set { _base._dudoso_cobro = value; } }
		public virtual Decimal TotalExpediente { get { return _base._total_expediente; } set { _base._total_expediente = value; } } 

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

        public virtual OutputInvoice CloneAsNew()
        {
            OutputInvoice clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.GetNewCode();
            clon.SessionCode = OutputInvoice.OpenSession();
            OutputInvoice.BeginTransaction(clon.SessionCode);

            clon.MarkNew();
            clon.Conceptos.MarkAsNew();

            return clon;
        }

		public virtual void GetNewCode()
		{
			// Obtenemos el último serial de servicio
			ETipoFactura tipo = (_base.Record.Rectificativa) ? ETipoFactura.Rectificativa : ETipoFactura.Ordinaria;
 
			Serial = SerialFacturaInfo.GetNext(this.OidSerie, this.Fecha.Year, tipo);

			// Devolvemos el siguiente codigo de Factura para esa serie
			if (_base.Record.Rectificativa)
				Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT + "-R");
			else
				Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
		}

		protected virtual void SetNewCode()
		{
			try
			{
				OutputInvoiceInfo factura = OutputInvoiceInfo.GetByCode(_base.Record.Codigo, _base.Record.OidSerie, Convert.ToInt32(_base.Record.Ano), false);
				if (factura != null && (factura.Oid != 0) && (factura.Oid != Oid)) throw new iQException(Resources.Messages.DUPLICATE_INVOICE_CODE);
				_base.Record.Serial = Convert.ToInt64(_base.Record.Codigo);
			}
			catch
			{
				throw new iQException(String.Format(Resources.Messages.INVALID_INVOICE_CODE, Resources.Defaults.FACTURA_CODE_FORMAT));
			}
		}

		public virtual void CopyFrom(ClienteInfo source)
		{
			if (source == null) return;

			OidCliente = source.Oid;
			IDCliente = source.Codigo;
			Cliente = source.Nombre;
			VatNumber = source.VatNumber;
			Direccion = source.Direccion;
			CodigoPostal = source.CodigoPostal;
			Municipio = source.Municipio;
			Provincia = source.Provincia;
			PDescuento = source.PDescuentoPtoPago;
			MedioPago = source.MedioPago;
			FormaPago = source.FormaPago;
			DiasPago = source.DiasPago;
			Prevision = Library.Common.EnumFunctions.GetPrevisionPago(EFormaPago, Fecha, DiasPago);
			CuentaBancaria = source.CuentaAsociada;
		}
		public virtual void CopyFrom(OutputDeliveryInfo source)
        {
            if (source == null) return;

            OidSerie = source.OidSerie;
            OidTransportista = source.OidTransportista;
            Observaciones = source.Observaciones;
            Nota = source.Nota;
            CuentaBancaria = source.CuentaBancaria;
        }

		public virtual void CalculateTotal()
		{
			BaseImponible = 0;
			Descuento = 0;
			Impuestos = 0;
			Total = 0;

			foreach (OutputInvoiceLine item in _conceptos)
			{
				if (!item.IsKitComponent)
				{
					item.CalculateTotal();
					BaseImponible += item.BaseImponible;
				}
			}

			if (Prevision == Fecha)
				Descuento = BaseImponible * PDescuento / 100;
			else
				Descuento = 0;

			//Esta funcion actualiza la propiedad Impuestos 
			_base.GetImpuestos(_conceptos);

			//Para marcar la propiedad como Dirty
            //Impuestos = _base._impuestos_list.TotalizeImpuestos();
            _base._impuestos_list.TotalizeImpuestos();
            Impuestos = Impuestos;

			BaseImponible -= Descuento;
			Total = BaseImponible + Impuestos - IRPF;
		}

		public static OutputInvoice ChangeEstado(long oid, EEstado estado)
		{
			if (!CanChangeState())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			OutputInvoice item = null;

			try
			{
				item = OutputInvoice.Get(oid, false);

				if ((item.EEstado == EEstado.Contabilizado || item.EEstado == EEstado.Exportado) && (!AutorizationRulesControler.CanEditObject(Resources.SecureItems.CUENTA_CONTABLE)))
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

		/// <summary>
		/// Crea los conceptos de factura asociados a un albarán agrupado
		/// </summary>
		/// <param name="source"></param>
		public virtual void Compact(OutputDeliveryInfo source)
		{
			OutputInvoiceLine newitem;

			AlbaranFacturas.NewItem(this, source);

			ClienteInfo cliente = ClienteInfo.Get(OidCliente, false, true);
			if (cliente.Productos == null) cliente.LoadChilds(typeof(ProductoCliente), false);

			//ProductoClienteInfo productoCliente = null;
			ProductInfo producto = null;
			BatchInfo partida = null;
			OutputInvoiceLine concepto = null;
			StoreInfo almacen = null;

			foreach (OutputDeliveryLineInfo item in source.Conceptos)
			{
				//Productos con control de stock
				if (item.OidPartida != 0)
				{
					concepto = Conceptos.GetItemByBatch(item.OidPartida, item.ETipoFacturacion);

					//Ya existe un concepto asociado a esta Partida
					if (concepto != null)
					{
						concepto.CantidadKilos += item.CantidadKilos;
						concepto.CantidadBultos += item.CantidadBultos;

						almacen = StoreInfo.Get(item.OidAlmacen, false, true);
						almacen.LoadPartidasByProducto(item.OidProducto, true);

						//productoCliente = cliente.Productos.GetByProducto(item.OidProducto);
						//concepto.FacturacionBulto = (productoCliente != null) ? productoCliente.FacturacionBulto : false;

						producto = ProductInfo.Get(item.OidProducto, false, true);
						partida = almacen.Partidas.GetItem(item.OidPartida);
						concepto.Precio = (concepto.Precio + item.Precio) / 2;
					}
					//Nuevo concepto. No existe concepto previo asociado a esta Partida
					else
					{
						newitem = Conceptos.NewItem(this);
						newitem.CopyFrom(item);
					}
				}
				else
				{
					concepto = Conceptos.GetItemByProduct(item.OidProducto, item.ETipoFacturacion);

					//Ya existe un concepto asociado a este Producto
					if (concepto != null)
					{
						concepto.CantidadKilos += item.CantidadKilos;
						concepto.CantidadBultos += item.CantidadBultos;

						//productoCliente = cliente.Productos.GetByProducto(item.OidProducto);
						//concepto.FacturacionBulto = (productoCliente != null) ? productoCliente.FacturacionBulto : false;

						producto = ProductInfo.Get(item.OidProducto, false, true);
						concepto.Precio = (concepto.Precio + item.Precio) / 2;
						//concepto.SetPrecio(producto, null, cliente);
					}
					//Nuevo concepto. No existe concepto previo asociado a este Producto
					else
					{
						newitem = Conceptos.NewItem(this);
						newitem.CopyFrom(item);
					}
				}
			}

			CalculateTotal();
		}

		/// <summary>
		/// Elimina los conceptos de factura asociados a un albarán
		/// </summary>
		/// <param name="source"></param>
		public virtual void Extract(OutputDeliveryInfo source)
		{
			if (source.Conceptos == null) source.LoadChilds(typeof(OutputDeliveryLines), true);

			AlbaranFacturas.Remove(this, source);

			foreach (OutputDeliveryLineInfo item in source.Conceptos)
				Conceptos.Remove(item);

			CalculateTotal();
		}

		public virtual HashOidList GetAlbaranes()
		{
			HashOidList albaranes = new HashOidList();

			foreach (AlbaranFactura item in AlbaranFacturas)
				albaranes.Add(item.OidAlbaran);

			return albaranes;
		}

		/// <summary>
        /// Crea los conceptos de factura asociados a un albarán
        /// </summary>
        /// <param name="source"></param>
        public virtual void Insert(OutputDeliveryInfo source)
        {
            OutputInvoiceLine newitem;

			if (_base.Record.Agrupada)
			{
				Compact(source);
				return;
			}

            AlbaranFacturas.NewItem(this, source);
            foreach (OutputDeliveryLineInfo item in source.Conceptos)
            {
                newitem = Conceptos.NewItem(this);
                newitem.CopyFrom(item);
            }

            CalculateTotal();
        }

		public virtual void UpdateEstadoCobro(EEstado estado)
		{
			EEstadoCobro = estado;

			if (EEstadoCobro == EEstado.DudosoCobro)
			{
				DudosoCobro = PendienteVencido;
				PendienteVencido = 0;
			}
			else
			{
				DudosoCobro = 0;
				PendienteVencido = Pendiente + EfectosDevueltos;
			}
		}
		
        public virtual void UpdateInfoAlbaranes()
        {
            Observaciones = Resources.Defaults.OBSERVACIONES_FACTURA;

            foreach (AlbaranFactura item in AlbaranFacturas)
            {
                Observaciones += " " + item.CodigoAlbaran + ",";
            }

            Observaciones = Observaciones.Substring(0, Observaciones.Length - 1);
        }

        private void UpdateAlbaranes()
        {
            foreach (AlbaranFactura item in AlbaranFacturas)
            {
                OutputDelivery albaran = OutputDelivery.Get(item.OidAlbaran, ETipoEntidad.Cliente, false, SessionCode);
                albaran.OidHolder = OidCliente;
                albaran.Save();
            }
        }
		
		public virtual void SetAlbaranes()
		{
			Albaranes = string.Empty;

			foreach (AlbaranFactura item in AlbaranFacturas)
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
				throw new iQValidationException(e.Description, string.Empty, "Codigo");
			}

			//Serie
			if (OidSerie == 0)
			{
				e.Description = Resources.Messages.NO_SERIE_SELECTED;
				throw new iQValidationException(e.Description, string.Empty, "Serie");
			}

			//Cliente
			if (OidCliente == 0)
			{
				e.Description = Resources.Messages.NO_CLIENTE_SELECTED;
				throw new iQValidationException(e.Description, string.Empty, "IDCliente");
			}

			return true;
		}	

        #endregion

        #region Autorization Rules

        public static bool CanAddObject()
        {
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanAddObject(Resources.SecureItems.FACTURA_EMITIDA);
        }
        public static bool CanGetObject()
        {
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanGetObject(Resources.SecureItems.FACTURA_EMITIDA);  
		}
        public static bool CanDeleteObject()
        {
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.FACTURA_EMITIDA); 
        }
        public static bool CanEditObject()
        {
			if (AppContext.User == null) return false;
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanEditObject(Resources.SecureItems.FACTURA_EMITIDA); 
        }
		public static bool CanChangeState()
		{
			return AppContext.User.IsService
					|| AutorizationRulesControler.CanGetObject(Library.Common.Resources.SecureItems.ESTADO);
		}

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				Factura = OutputInvoice.New().GetInfo(false),
				Estado = EEstado.NoAnulado
			};
			conditions.Factura.Oid = oid;

			OutputInvoiceInfo item = OutputInvoiceInfo.Get(oid, false);

			if (item.EEstado != EEstado.Abierto)
				throw new iQException(Resources.Messages.FACTURA_NO_ANULADA);

			CobroFacturaList cobros = CobroFacturaList.GetList(conditions);

			if (cobros.Count > 0)
				throw new iQException(Resources.Messages.COBROS_ASOCIADOS);
		}

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        protected OutputInvoice() { }

        public static OutputInvoice NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            OutputInvoice obj = DataPortal.Create<OutputInvoice>(new CriteriaCs(-1));
            obj.MarkAsChild();
            return obj;
        }

        public virtual OutputInvoiceInfo GetInfo(bool childs = true)
        {
            return new OutputInvoiceInfo(this, childs);
        }

        public virtual void LoadChilds(Type type, bool childs)
        {
            if (type.Equals(typeof(CobroFactura)))
            {
                _cobro_facturas = CobroFacturas.GetChildList(this, childs);
            }
            if (type.Equals(typeof(AlbaranFactura)))
            {
                _albaran_facturas = AlbaranFacturas.GetChildList(this, childs);
            }
        }

        #endregion

        #region Child Factory Methods

		private OutputInvoice(int sessionCode, IDataReader source, bool childs)
		{
			MarkAsChild();
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(source);
		}

		internal static OutputInvoice GetChild(int sessionCode, IDataReader source, bool childs) { return new OutputInvoice(sessionCode, source, childs); }

		#endregion

        #region Root Factory Methods

        public static OutputInvoice New(int sessionCode = -1)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            OutputInvoice obj = DataPortal.Create<OutputInvoice>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
            return obj;
        }
		public static OutputInvoice New(OutputDeliveryInfo albaran, int sessionCode = -1)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            OutputInvoice obj = DataPortal.Create<OutputInvoice>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
            obj.CopyFrom(albaran);
            return obj;
        }

		public new static OutputInvoice Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return BusinessBaseEx<OutputInvoice>.Get(query, childs, -1);
		}

        public static OutputInvoice Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }

        public static void Delete(long oid)
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            IsPosibleDelete(oid);

            DataPortal.Delete(new CriteriaCs(oid));
        }

        /// <summary>
        /// Elimina todas los Facturas
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = OutputInvoice.OpenSession();
            ISession sess = OutputInvoice.Session(sessCode);
            ITransaction trans = OutputInvoice.BeginTransaction(sessCode);

            try
            {
                sess.Delete("from InvoiceRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                OutputInvoice.CloseSession(sessCode);
            }
        }

        public override OutputInvoice Save()
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

				OutputDeliveries albaranes = Cache.Instance.Get(typeof(OutputDeliveries)) as OutputDeliveries;
                if (albaranes != null) albaranes.SaveAsChild();

                // Update de las listas.
                _albaran_facturas.Update(this);
                _conceptos.Update(this);

                //la condición es necesaria para que sólo modifique el usuario en caso de ser una factura normal
                //si se trata de una factura agrupada y llamamos a la función que cambia el cliente en los albaranes
                //además de cambiar al cliente, conserva el código antiguo del albarán y ya no serán correlativos
                if (!_base.Record.Agrupada)
                    UpdateAlbaranes();

                if (!SharedTransaction) Transaction().Commit();

                //Hay que hacerlo aquí y fuera de la transacción general porque al borra los albaranes es posible
                //que se intenten borrar conceptos que han sido cambiados de albarán y al hacerlo el albarán original 
                //se lo cepilla por integridad referencial y luego el nuevo concepto de factura no lo encuentra por
                //lo que falla la integridad referencial.
                if (_albaran_facturas.ToDelete != null && _albaran_facturas.ToDelete.Count > 0)
                    OutputDelivery.DeleteFromList(_albaran_facturas.ToDelete);

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
				Cache.Instance.Remove(typeof(OutputDeliveries));

				if (!SharedTransaction)
				{
					if (CloseSessions) CloseSession();
					else BeginTransaction();
				}
            }
        }

        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
            Oid = Convert.ToInt64((new Random()).Next());
			OidSerie = Library.Invoice.ModulePrincipal.GetDefaultSerieSetting();
			OidUsuario = AppContext.User != null ? AppContext.User.Oid : 0;
			Usuario = AppContext.User != null ? AppContext.User.Name : string.Empty;
            Fecha = DateTime.Now;
			Ano = DateTime.Today.Year;
            EMedioPago = EMedioPago.Efectivo;
            EFormaPago = EFormaPago.Contado;
            EEstado = EEstado.Abierto;
			EEstadoCobro = EEstado.Pendiente;
			GetNewCode();
		}

		private void Fetch(OutputInvoice source)
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

						AlbaranFactura.DoLOCK(Session());
						query = AlbaranFacturas.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_albaran_facturas = AlbaranFacturas.GetChildList(SessionCode, reader);

						OutputInvoiceLine.DoLOCK(Session());
						query = OutputInvoiceLines.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_conceptos = OutputInvoiceLines.GetChildList(SessionCode, reader);
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

						AlbaranFactura.DoLOCK(Session());
						query = AlbaranFacturas.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_albaran_facturas = AlbaranFacturas.GetChildList(SessionCode, reader);

						OutputInvoiceLine.DoLOCK(Session());
						query = OutputInvoiceLines.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_conceptos = OutputInvoiceLines.GetChildList(SessionCode, reader);
					}
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Insert(OutputInvoices parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;

			parent.Session().Save(Base.Record);

			_albaran_facturas.Update(this);
			_conceptos.Update(this);

			MarkOld();
		}

		internal void Update(OutputInvoices parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			SessionCode = parent.SessionCode;
			OutputInvoiceRecord obj = Session().Get<OutputInvoiceRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			_albaran_facturas.Update(this);
			_conceptos.Update(this);

			MarkOld();
		}

		internal void DeleteSelf(OutputInvoices parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<OutputInvoiceRecord>(Oid));
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
                    OutputInvoice.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        AlbaranFactura.DoLOCK(Session());
                        query = AlbaranFacturas.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
						_albaran_facturas = AlbaranFacturas.GetChildList(SessionCode, reader);

						SetAlbaranes();

                        OutputInvoiceLine.DoLOCK(Session());
                        query = OutputInvoiceLines.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
						_conceptos = OutputInvoiceLines.GetChildList(SessionCode, reader);
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
            try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
                
				if (!IDManual)
                    GetNewCode();
                else
                    SetNewCode();

				if (_base.Record.Agrupada)
				{
					AlbaranFacturas.Compact(this);
                }

				SetAlbaranes();
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
            if (IsDirty)
            {
                if (IDManual)
                    SetNewCode();

                try
                {
					OutputInvoiceRecord obj = Session().Get<OutputInvoiceRecord>(Oid);
                    obj.CopyValues(Base.Record);

					if (_base.Record.Agrupada)
					{
						AlbaranFacturas.Compact(this);
					}

					SetAlbaranes();
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
                //Iniciamos la conexion y la transaccion
                SessionCode = OpenSession();
                BeginTransaction();

                //Si no hay integridad referencial, aqui se deben borrar las listas hijo
                CriteriaEx criteria = GetCriteria();
                criteria.AddOidSearch(criterio.Oid);

                // Obtenemos el objeto
				OutputInvoiceRecord obj = (OutputInvoiceRecord)(criteria.UniqueResult());
				Session().Delete(Session().Get<OutputInvoiceRecord>(obj.Oid));

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

		internal enum ETipoQuery { GENERAL = 0, BY_COBRO = 2, BENEFICIO = 3, AGRUPADO = 4 }
        
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
		
        internal static string SELECT_FIELDS(ETipoQuery queryType, QueryConditions conditions)
        {
            string query;

            decimal tipo_interes = moleQule.Library.Invoice.ModulePrincipal.GetTipoInteresPorGastosDemoraSetting();
            
            query = @"
			SELECT " + (long)queryType + @" AS ""TIPO_QUERY""
				,F.*
				,COALESCE(US.""NAME"", '') AS ""USUARIO""
				,S.""IDENTIFICADOR"" AS ""N_SERIE""
				,S.""NOMBRE"" AS ""SERIE""
				,CL.""CODIGO"" AS ""ID_CLIENTE""
				,CASE WHEN (CL.""TIPO_INTERES"" > 0) THEN CL.""TIPO_INTERES"" ELSE " + tipo_interes.ToString(System.Globalization.CultureInfo.InvariantCulture) + @" END AS ""TIPO_INTERES""
				,COALESCE(CF1.""COBRADO"", 0) AS ""COBRADO""
				,COALESCE(RG.""CODIGO"", '') || '/' || COALESCE(LR.""ID_EXPORTACION"", '') AS ""ID_MOVIMIENTO_CONTABLE""";

			switch (queryType)
			{
				case ETipoQuery.GENERAL:

					query += @"
						,F.""TOTAL"" - COALESCE(CF1.""COBRADO"", 0) - COALESCE(""NO_VENCIDO"", 0) - COALESCE(""VENCIDO"", 0) - COALESCE(""PENDIENTE_VTO"", 0) AS ""PENDIENTE""
						,COALESCE(""NO_VENCIDO"", 0) AS ""NO_VENCIDO""
						,COALESCE(""VENCIDO"", 0) AS ""VENCIDO""
						,COALESCE(""PENDIENTE_VTO"", 0) AS ""PENDIENTE_VTO""
						,CF1.""FECHA"" AS ""FECHA_COBRO""
						,COALESCE(""CONDICIONES_VENTA"", 0) AS ""CONDICIONES_VENTA""";

                    if (conditions.Expediente != null || conditions.EntityType == ETipoEntidad.Expediente)
                        query += @"
							,CF.""TOTAL_EXPEDIENTE"" AS ""TOTAL_EXPEDIENTE""";
                    else
                        query += @"
							,0 AS ""TOTAL_EXPEDIENTE""";

					break;

				case ETipoQuery.BY_COBRO:

					query += @"
						,F.""TOTAL"" - COALESCE(CF1.""COBRADO"", 0) AS ""PENDIENTE""
						,0 AS ""NO_VENCIDO""
						,0 AS ""VENCIDO""
						,0 AS ""PENDIENTE_VTO""
						,C.""FECHA"" AS ""FECHA_COBRO""";

                    if (conditions.Expediente != null || conditions.EntityType == ETipoEntidad.Expediente)
                        query += @"
							,CF.""TOTAL_EXPEDIENTE"" AS ""TOTAL_EXPEDIENTE""";
                    else
                        query += @"
							,0 AS ""TOTAL_EXPEDIENTE""";

					break;

                case ETipoQuery.BENEFICIO:

                    query += @"
						,F.""TOTAL"" - COALESCE(CF1.""COBRADO"", 0) - COALESCE(""NO_VENCIDO"", 0) - COALESCE(""VENCIDO"", 0) - COALESCE(""PENDIENTE_VTO"", 0) AS ""PENDIENTE""
						,COALESCE(""NO_VENCIDO"", 0) AS ""NO_VENCIDO""
						,COALESCE(""VENCIDO"", 0) AS ""VENCIDO""
						,COALESCE(""PENDIENTE_VTO"", 0) AS ""PENDIENTE_VTO""
						,CF1.""FECHA"" AS ""FECHA_COBRO""
						,COALESCE(""CONDICIONES_VENTA"", 0) AS ""CONDICIONES_VENTA""
						,COALESCE(PRC.""PRECIO_COSTE"", 0) AS ""TOTAL_COSTE""
						,F.""BASE_IMPONIBLE"" - COALESCE(PRC.""PRECIO_COSTE"", 0) AS ""BENEFICIO""
						,CASE WHEN COALESCE(PRC.""PRECIO_COSTE"", 0) = 0 THEN 0 ELSE (F.""BASE_IMPONIBLE"" - COALESCE(PRC.""PRECIO_COSTE"", 0)) * 100 / COALESCE(PRC.""PRECIO_COSTE"", 0) END AS ""P_BENEFICIO""";

                    if (conditions.Expediente != null || conditions.EntityType == ETipoEntidad.Expediente)
                        query += @"
							,CF.""TOTAL_EXPEDIENTE"" AS ""TOTAL_EXPEDIENTE""";
                    else
                        query += @"
							,0 AS ""TOTAL_EXPEDIENTE""";

                    break;

                case ETipoQuery.AGRUPADO:
                    query = @"
                        SELECT " + (long)queryType + @" AS ""TIPO_QUERY"" 
                            ,DATE_TRUNC('" + conditions.Step.ToString() + @"', F.""FECHA"") AS ""STEP""
							,SUM(F.""TOTAL"") AS ""TOTAL""";

                    break;
			}

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

            query = @" 
			WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "F", ForeignFields());

			query += @"
                 AND (F.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			query += EntityBase.ESTADO_CONDITION(conditions.Estado, "F");

			if (conditions.Usuario != null) query += " AND F.\"OID_USUARIO\" = " + conditions.Usuario.Oid;

			if (conditions.Factura != null) 
				if (conditions.Factura.Oid != 0) 
					query += @"
						AND F.""OID"" = " + conditions.Factura.Oid;

			if (conditions.OidList != null)
				query += @"
					AND F.""OID"" IN " + EntityBase.GET_IN_STRING(conditions.OidList);

			if (conditions.Cliente != null && (conditions.Cliente.Oid != 0)) 
				query += @"
					AND F.""OID_CLIENTE"" = " + conditions.Cliente.Oid;

			if (conditions.Serie != null) 
				query += @"
					AND F.""OID_SERIE"" = " + conditions.Serie.Oid;

			if (conditions.Cobro != null) 
				query += @"
					AND C.""OID"" = " + conditions.Cobro;

			if (conditions.MedioPago != EMedioPago.Todos) 
				query += @"
					AND F.""MEDIO_PAGO"" = " + (long)conditions.MedioPago;
            
            if (conditions.MedioPagoList != null && conditions.MedioPagoList.Count > 0)
                query += EntityBase.GET_IN_LIST_CONDITION(conditions.MedioPagoList, "F", "MEDIO_PAGO"); 
			
			if (conditions.Expediente != null) 
				query += @"
					AND CF.""OID_EXPEDIENTE"" = " + conditions.Expediente.Oid;

			if (conditions.TipoFactura != ETipoFactura.Todas)
			{
				bool value = (conditions.TipoFactura == ETipoFactura.Ordinaria) ? false : true;
				query += @"
					AND F.""RECTIFICATIVA"" = " + value.ToString().ToUpper();
			}

            /*if (AppContext.User.IsPartner)
                query += EntityBase.GET_IN_BRANCHES_LIST_CONDITION(AppContext.Principal.Branches, "F");*/

			return query + conditions.ExtraWhere;
		}

		internal static string SELECT_BASE(QueryConditions conditions)
		{
			string cf = nHManager.Instance.GetSQLTable(typeof(ChargeOperationRecord));
			string cr = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string cpf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
            string pc = nHManager.Instance.GetSQLTable(typeof(ClientProductRecord));

			string query;

			if (conditions.Step != EStepGraph.None)
			{
				query =
					SELECT_FIELDS(ETipoQuery.AGRUPADO, conditions) +
					JOIN_BASE(conditions);
			}
			else
			{
				query = SELECT_FIELDS(ETipoQuery.GENERAL, conditions) +
						JOIN_BASE(conditions) +
						" LEFT JOIN (SELECT CF11.\"OID_FACTURA\"" +
						"					,SUM(CF11.\"CANTIDAD\") AS \"COBRADO\"" +
						"					,MAX(CF11.\"OID_COBRO\") AS \"OID_COBRO\"" +
						"					,MAX(C1.\"FECHA\") AS \"FECHA\"" +
						"           FROM " + cf + " AS CF11" +
						"           INNER JOIN " + cr + " AS C1 ON CF11.\"OID_COBRO\" = C1.\"OID\" AND C1.\"ESTADO_COBRO\" = " + (long)EEstado.Charged + " AND C1.\"ESTADO\" != " + (long)EEstado.Anulado +
						"				AND C1.\"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
						"			WHERE C1.\"ESTADO\" != " + (long)EEstado.Anulado +
						"           GROUP BY \"OID_FACTURA\") " +
						"	AS CF1 ON CF1.\"OID_FACTURA\" = F.\"OID\"" +
						" LEFT JOIN (SELECT \"OID_FACTURA\"" +
						"					,SUM(\"CANTIDAD\") AS \"NO_VENCIDO\"" +
						"           FROM " + cf + " AS CF21" +
						"           INNER JOIN " + cr + " AS C2 ON CF21.\"OID_COBRO\" = C2.\"OID\" AND C2.\"ESTADO_COBRO\" = " + (long)EEstado.Charged + " AND C2.\"ESTADO\" != " + (long)EEstado.Anulado +
						"				AND C2.\"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
						"			WHERE  C2.\"ESTADO\" != " + (long)EEstado.Anulado +
						"           GROUP BY \"OID_FACTURA\") AS CF2 ON CF2.\"OID_FACTURA\" = F.\"OID\"" +
						" LEFT JOIN (SELECT \"OID_FACTURA\"" +
						"					,SUM(\"CANTIDAD\") AS \"VENCIDO\"" +
						"           FROM " + cf + " AS CF31" +
						"           INNER JOIN " + cr + " AS C3 ON CF31.\"OID_COBRO\" = C3.\"OID\" AND C3.\"ESTADO_COBRO\" != " + (long)EEstado.Charged + " AND C3.\"ESTADO\" != " + (long)EEstado.Anulado +
						"				AND C3.\"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
						"			WHERE  C3.\"ESTADO\" != " + (long)EEstado.Anulado +
						"           GROUP BY \"OID_FACTURA\") AS CF3 ON CF3.\"OID_FACTURA\" = F.\"OID\"" +
						" LEFT JOIN (SELECT \"OID_FACTURA\"" +
						"					,SUM(\"CANTIDAD\") AS \"PENDIENTE_VTO\"" +
						"           FROM " + cf + " AS CF41" +
						"           INNER JOIN " + cr + " AS C4 ON CF41.\"OID_COBRO\" = C4.\"OID\" AND C4.\"ESTADO_COBRO\" != " + (long)EEstado.Charged + " AND C4.\"ESTADO\" != " + (long)EEstado.Anulado +
						"				AND C4.\"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
						"			WHERE  C4.\"ESTADO\" != " + (long)EEstado.Anulado +
						"           GROUP BY \"OID_FACTURA\") AS CF4 ON CF4.\"OID_FACTURA\" = F.\"OID\"" +
						" LEFT JOIN " + cr + " AS C ON C.\"OID\" = CF1.\"OID_COBRO\"" +
						" LEFT JOIN (SELECT \"OID_FACTURA\"" +
						"                   , SUM((\"PRECIO_COMPRA\" - CF51.\"PRECIO\") * (CASE WHEN (CF51.\"FACTURACION_BULTO\" = TRUE) THEN \"CANTIDAD_BULTOS\" ELSE \"CANTIDAD\" END)) AS \"CONDICIONES_VENTA\"" +
						"                   , \"FECHA_VALIDEZ\"" +
						"                   , \"OID_CLIENTE\"" +
						"           FROM " + cpf + " AS CF51" +
						"           INNER JOIN " + pc + " AS PC ON CF51.\"OID_PRODUCTO\" = PC.\"OID_PRODUCTO\"" +
						"           WHERE CF51.\"PRECIO\" < PC.\"PRECIO_COMPRA\" AND CF51.\"FACTURACION_BULTO\" = PC.\"FACTURACION_BULTO\"" +
						"           GROUP BY \"OID_FACTURA\", \"FECHA_VALIDEZ\", \"OID_CLIENTE\") AS CF5 ON CF5.\"OID_FACTURA\" = F.\"OID\" AND CF5.\"OID_CLIENTE\" = CL.\"OID\"" +
						"               AND CF5.\"FECHA_VALIDEZ\" >= F.\"FECHA\"";
			}

			return query;
		}

        internal static string SELECT_BASE_BENEFICIO(QueryConditions conditions)
        {
            string cf = nHManager.Instance.GetSQLTable(typeof(ChargeOperationRecord));
            string cr = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string cpf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
            string pc = nHManager.Instance.GetSQLTable(typeof(ClientProductRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
            string er = nHManager.Instance.GetSQLTable(typeof(REAExpedientRecord));
            string cbr = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));

            string query;

            query = SELECT_FIELDS(ETipoQuery.BENEFICIO, conditions) +
                JOIN_BASE(conditions) +
                    " LEFT JOIN (SELECT CF11.\"OID_FACTURA\"" +
                    "					,SUM(CF11.\"CANTIDAD\") AS \"COBRADO\"" +
                    "					,MAX(CF11.\"OID_COBRO\") AS \"OID_COBRO\"" +
                    "					,MAX(C1.\"FECHA\") AS \"FECHA\"" +
                    "           FROM " + cf + " AS CF11" +
                    "           INNER JOIN " + cr + " AS C1 ON CF11.\"OID_COBRO\" = C1.\"OID\" AND C1.\"ESTADO_COBRO\" = " + (long)EEstado.Charged + " AND C1.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "				AND C1.\"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "			WHERE C1.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "           GROUP BY \"OID_FACTURA\") " +
                    "	AS CF1 ON CF1.\"OID_FACTURA\" = F.\"OID\"" +
                    " LEFT JOIN (SELECT \"OID_FACTURA\"" +
                    "					,SUM(\"CANTIDAD\") AS \"NO_VENCIDO\"" +
                    "           FROM " + cf + " AS CF21" +
                    "           INNER JOIN " + cr + " AS C2 ON CF21.\"OID_COBRO\" = C2.\"OID\" AND C2.\"ESTADO_COBRO\" = " + (long)EEstado.Charged + " AND C2.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "				AND C2.\"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "			WHERE  C2.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "           GROUP BY \"OID_FACTURA\") AS CF2 ON CF2.\"OID_FACTURA\" = F.\"OID\"" +
                    " LEFT JOIN (SELECT \"OID_FACTURA\"" +
                    "					,SUM(\"CANTIDAD\") AS \"VENCIDO\"" +
                    "           FROM " + cf + " AS CF31" +
                    "           INNER JOIN " + cr + " AS C3 ON CF31.\"OID_COBRO\" = C3.\"OID\" AND C3.\"ESTADO_COBRO\" != " + (long)EEstado.Charged + " AND C3.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "				AND C3.\"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "			WHERE  C3.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "           GROUP BY \"OID_FACTURA\") AS CF3 ON CF3.\"OID_FACTURA\" = F.\"OID\"" +
                    " LEFT JOIN (SELECT \"OID_FACTURA\"" +
                    "					,SUM(\"CANTIDAD\") AS \"PENDIENTE_VTO\"" +
                    "           FROM " + cf + " AS CF41" +
                    "           INNER JOIN " + cr + " AS C4 ON CF41.\"OID_COBRO\" = C4.\"OID\" AND C4.\"ESTADO_COBRO\" != " + (long)EEstado.Charged + " AND C4.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "				AND C4.\"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "			WHERE  C4.\"ESTADO\" != " + (long)EEstado.Anulado +
                    "           GROUP BY \"OID_FACTURA\") AS CF4 ON CF4.\"OID_FACTURA\" = F.\"OID\"" +
                    " LEFT JOIN " + cr + " AS C ON C.\"OID\" = CF1.\"OID_COBRO\"" +
                    " LEFT JOIN (SELECT \"OID_FACTURA\"" +
                    "                   , SUM((\"PRECIO_COMPRA\" - CF51.\"PRECIO\") * (CASE WHEN (CF51.\"FACTURACION_BULTO\" = TRUE) THEN \"CANTIDAD_BULTOS\" ELSE \"CANTIDAD\" END)) AS \"CONDICIONES_VENTA\"" +
                    "                   , \"FECHA_VALIDEZ\"" +
                    "                   , \"OID_CLIENTE\"" +
                    "           FROM " + cpf + " AS CF51" +
                    "           INNER JOIN " + pc + " AS PC ON CF51.\"OID_PRODUCTO\" = PC.\"OID_PRODUCTO\"" +
                    "           WHERE CF51.\"PRECIO\" < PC.\"PRECIO_COMPRA\" AND CF51.\"FACTURACION_BULTO\" = PC.\"FACTURACION_BULTO\"" +
                    "           GROUP BY \"OID_FACTURA\", \"FECHA_VALIDEZ\", \"OID_CLIENTE\") AS CF5 ON CF5.\"OID_FACTURA\" = F.\"OID\" AND CF5.\"OID_CLIENTE\" = CL.\"OID\"" +
                    "               AND CF5.\"FECHA_VALIDEZ\" >= F.\"FECHA\"" +
                    " LEFT JOIN (   SELECT CF.\"OID_FACTURA\"" +
                    "                   , SUM(CF.\"CANTIDAD\" * " +
                    "                       (CASE WHEN (COALESCE(PA.\"PRECIO_COMPRA_KILO\", 0) != 0)" +
                    "                           THEN (PA.\"PRECIO_COMPRA_KILO\" + COALESCE(PA.\"GASTO_KILO\", 0) - " +
                    "                               (CASE WHEN COALESCE(PA.\"AYUDA\", 'FALSE') = TRUE" +
                    "                                   THEN (CASE WHEN COALESCE(PA2.\"AYUDA_RECIBIDA\", 0) != 0" +
                    "                                       THEN (PA2.\"AYUDA_RECIBIDA\")" +
                    "                                       ELSE (PR.\"AYUDA_KILO\") END)" +
                    "                                   ELSE 0 END))" +
                    "                           ELSE PR.\"PRECIO_COMPRA\" END)) AS \"PRECIO_COSTE\"" +
                    "               FROM " + cpf + " AS CF" +
                    "               INNER JOIN " + pr + " AS PR ON PR.\"OID\" = CF.\"OID_PRODUCTO\"" +
                    "               LEFT JOIN " + pa + " AS PA ON PA.\"OID\" = CF.\"OID_BATCH\"" +
                    "               LEFT JOIN (	SELECT PA.\"OID\" AS \"OID_PARTIDA\"" +
					"                               , SUM(CR.\"CANTIDAD\" / PA.\"KILOS_INICIALES\") AS \"AYUDA_RECIBIDA\"" +
					"                               , ER.\"CODIGO_ADUANERO\"" +
				    "                           FROM " + pa + " AS PA" + 
				    "                           INNER JOIN " + er + " AS ER ON ER.\"OID_EXPEDIENTE\" = PA.\"OID_EXPEDIENTE\"  AND ER.\"ESTADO\" != " + (long)EEstado.Anulado +
				    "                           INNER JOIN " + cbr + " AS CR ON CR.\"OID_EXPEDIENTE_REA\" = ER.\"OID\" AND PA.\"OID_EXPEDIENTE\" = CR.\"OID_EXPEDIENTE\" " +
				    "                           GROUP BY PA.\"OID\", ER.\"CODIGO_ADUANERO\")" +
			        "                       AS PA2 ON PA2.\"OID_PARTIDA\" = PA.\"OID\" AND PA2.\"CODIGO_ADUANERO\" = PR.\"CODIGO_ADUANERO\"" + 
                    "               GROUP BY CF.\"OID_FACTURA\")" +
                    "       AS PRC ON PRC.\"OID_FACTURA\" = F.\"OID\"";

            return query;

        }

		internal static string JOIN_BRANCH(QueryConditions conditions)
        {
            return JOIN_BASE(conditions);
        }

		internal static string SELECT_BASE_BY_COBRO(QueryConditions conditions)
		{
			string cf = nHManager.Instance.GetSQLTable(typeof(ChargeOperationRecord));
			string cr = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));

			string query =
			SELECT_FIELDS(ETipoQuery.BY_COBRO, conditions) +
			JOIN_BASE(conditions) + @"
			LEFT JOIN (SELECT ""OID_FACTURA""
							,SUM(""CANTIDAD"") AS ""COBRADO""
						FROM " + cf + @" AS CF11
						INNER JOIN " + cr + @" AS C1 ON CF11.""OID_COBRO"" = C1.""OID""
						GROUP BY ""OID_FACTURA"")
				AS CF1 ON CF1.""OID_FACTURA"" = F.""OID""
			INNER JOIN " + cf + @" AS CF ON CF.""OID_FACTURA"" = F.""OID""
			INNER JOIN " + cr + @" AS C ON C.""OID"" = CF.""OID_COBRO""";

			return query;
		}

		internal static string JOIN_BASE(QueryConditions conditions)
		{
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
			string fc = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string se = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string lr = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
			string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));

			string query = @"
            FROM " + fc + @" AS F
			LEFT JOIN " + us + @" AS US ON US.""OID"" = F.""OID_USUARIO""
			INNER JOIN " + se + @" AS S ON F.""OID_SERIE"" = S.""OID""
			INNER JOIN " + cl + @" AS CL ON CL.""OID"" = F.""OID_CLIENTE""
			LEFT JOIN (SELECT MAX(""ID_EXPORTACION"") AS ""ID_EXPORTACION"", ""OID_ENTIDAD"", MAX(""OID_REGISTRO"") AS ""OID_REGISTRO""
						FROM " + lr + @" AS LR
						WHERE LR.""TIPO_ENTIDAD"" = " + (long)ETipoEntidad.FacturaEmitida + @"
							AND LR.""ESTADO"" = " + (long)EEstado.Contabilizado + @"
						GROUP BY ""OID_ENTIDAD"")
					AS LR ON F.""OID"" = LR.""OID_ENTIDAD""
			LEFT JOIN " + rg + @" AS RG ON RG.""OID"" = LR.""OID_REGISTRO""";

			if (conditions.Expediente != null)
			{
				string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));

				query += @"
				INNER JOIN (SELECT CF.""OID_FACTURA""
								,CF.""OID_EXPEDIENTE""
								,SUM(""SUBTOTAL"" - (""SUBTOTAL"" * ""P_DESCUENTO"" * 100)) AS ""TOTAL_EXPEDIENTE""
							FROM " + cf + @" AS CF
							WHERE CF.""OID_EXPEDIENTE"" = " + conditions.Expediente.Oid + @"
							GROUP BY ""OID_FACTURA"", ""OID_EXPEDIENTE"")
					AS CF ON CF.""OID_FACTURA"" = F.""OID""";
			}

			return query + " " + conditions.ExtraJoin;
		}

        public static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query = string.Empty;

			switch (conditions.TipoFacturas)
			{
				case ETipoFacturas.Todas:
					{
						query = SELECT_BASE(conditions) +
								WHERE(conditions);
					}
					break;

				case ETipoFacturas.Pendientes:
					{
						query = SELECT_PENDIENTES(conditions);
					}
					break;

				case ETipoFacturas.Cobradas:
					{
						query = SELECT_COBRADAS(conditions);
					}
					break;

				case ETipoFacturas.DudosoCobro:
					{
						query = SELECT_DUDOSO_COBRO(conditions);
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
						query += ORDER(conditions.Orders, "F", ForeignFields());
					else
						query += " ORDER BY F.\"FECHA\", S.\"IDENTIFICADOR\", F.\"CODIGO\"";
					query += LIMIT(conditions.PagingInfo);
				}  
            }
            else
                query += " ORDER BY F.\"FECHA\", S.\"IDENTIFICADOR\", F.\"CODIGO\"";			

			//if (lock_table) query += " FOR UPDATE OF F NOWAIT";

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
                JOIN_BASE(conditions) +
                WHERE(conditions);

            return query;
        }

        public static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Factura = OutputInvoiceInfo.New(oid) };
			query = SELECT(conditions, lockTable);

			return query;
		}

        internal static string SELECT_BY_EXPEDIENTS(QueryConditions conditions, bool lockTable)
        {
            conditions.ExtraWhere += @"
                AND CF.""OID_EXPEDIENTE"" IN " + EntityBase.GET_IN_STRING(conditions.OidList);

            string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));

            conditions.ExtraJoin += @"
			INNER JOIN (SELECT CF.""OID_FACTURA""
							,CF.""OID_EXPEDIENTE""
							,SUM(""SUBTOTAL"" - (""SUBTOTAL"" * ""P_DESCUENTO"" * 100)) AS ""TOTAL_EXPEDIENTE""
						FROM " + cf + @" AS CF
						WHERE CF.""OID_EXPEDIENTE""IN " + EntityBase.GET_IN_STRING(conditions.OidList) + @"
						GROUP BY ""OID_FACTURA"", ""OID_EXPEDIENTE"")
				AS CF ON CF.""OID_FACTURA"" = F.""OID""";

            conditions.OidList = null;
            conditions.EntityType = ETipoEntidad.Expediente;

            string query =
            SELECT(conditions, lockTable);

            return query;
        }

		internal static string SELECT_BY_LIST(List<long> oidList, bool lockTable)
		{
			string query = 
			SELECT_BASE(new QueryConditions());

			query += @"
			WHERE F.""OID"" IN " + EntityBase.GET_IN_STRING(oidList);

			query += EntityBase.LOCK("F", lockTable);

			return query;
		}

        internal static string SELECT_BENEFICIO(QueryConditions conditions, bool lockTable)
        {
            string query = 
			SELECT_BASE_BENEFICIO(conditions) +
            WHERE(conditions);

			query += EntityBase.LOCK("F", lockTable);

            return query;
        }

		internal static string SELECT_BY_COBRO(QueryConditions conditions, bool lockTable)
		{
			string query = 
			SELECT_BASE_BY_COBRO(conditions) +
			WHERE(conditions);

			query += EntityBase.LOCK("F", lockTable);

			return query;
		}

		internal static string SELECT_BY_CODE(QueryConditions conditions, bool lockTable)
		{
			conditions.ExtraWhere += @"
				AND F.""CODIGO"" = '" + conditions.Factura.Codigo + "'";

			string query =
			SELECT_BASE(conditions) +
			WHERE(conditions);			

			query += EntityBase.LOCK("F", lockTable);

			return query;
		}

        internal static string SELECT_BY_BRANCH(QueryConditions conditions, bool lockTable)
        {
			string di = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryInvoiceRecord));

			Assembly assembly = Assembly.Load("moleQule.Library.Partner");
			Type schematype = assembly.GetType("moleQule.Library.Partner.BranchRecord");

			string br = nHManager.Instance.GetSQLTable(schematype);

			assembly = Assembly.Load("moleQule.Library.Store");
			schematype = assembly.GetType("moleQule.Library.Store.SupplierRecord");
			string pv = nHManager.Instance.GetSQLTable(schematype);

			assembly = Assembly.Load("moleQule.Library.Renting");
			schematype = assembly.GetType("moleQule.Library.Renting.SubscriptionRecord");

			string su = nHManager.Instance.GetSQLTable(schematype);

			conditions.ExtraJoin += @"                
            INNER JOIN " + di + @" AS AF ON AF.""OID_FACTURA"" = F.""OID""
            INNER JOIN " + su + @" AS SU ON SU.""OID_DELIVERY"" = AF.""OID_ALBARAN""
            INNER JOIN " + br + @" AS BR ON BR.""OID"" = SU.""OID_BRANCH""
			INNER JOIN " + pv + @" AS PV ON PV.""OID"" = BR.""OID_PARTNER""";

			if (conditions.OidEntity != 0)
				conditions.ExtraWhere += @"
                    AND BR.""OID"" = " + conditions.OidEntity;

			if (conditions.IAcreedor != null && conditions.IAcreedor.Oid != 0)
				conditions.ExtraWhere += @"
                    AND PV.""OID"" = " + conditions.IAcreedor.Oid;

			string query = string.Empty;

			if (conditions.Step != EStepGraph.None)
			{
				query +=
					SELECT_FIELDS(ETipoQuery.AGRUPADO, conditions) +
					JOIN_BASE(conditions) +
					WHERE(conditions);
			}
			else
			{
				query += 
					SELECT_BASE(conditions) +
					WHERE(conditions);
			}				

			if (conditions.Step != EStepGraph.None)
			{
				query += @"
					GROUP BY ""STEP""
					ORDER BY ""STEP""";
			}
			else
			{
				if (conditions.Orders != null)
					query += ORDER(conditions.Orders, "F", ForeignFields());
				else
					query += " ORDER BY F.\"FECHA\", S.\"IDENTIFICADOR\", F.\"CODIGO\"";
				query += LIMIT(conditions.PagingInfo);
			}

            if (lockTable) query += " FOR UPDATE OF F NOWAIT";

            return query;
        }

		internal static string SELECT_BY_DELIVERY(QueryConditions conditions, bool lockTable)
		{
			string af = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryInvoiceRecord));

			string query =
			SELECT_BASE(conditions) + @"
			INNER JOIN" + af + @" AS AF ON AF.""OID_FACTURA"" = F.""OID"" AND AF.""OID_ALBARAN"" = " + conditions.OutputDelivery.Oid +
			WHERE(conditions);

			query += EntityBase.LOCK("F", lockTable);

			return query;
		}

		private static string SELECT_COBRADAS(QueryConditions conditions)
		{
			conditions.ExtraWhere += @"
				AND ""TOTAL"" != 0 AND COALESCE(CF1.""COBRADO"", 0) + COALESCE(CF2.""NO_VENCIDO"", 0) = F.""TOTAL""";

			string query =
			SELECT_BASE(conditions) +
			WHERE(conditions);			

			return query;
		}

		private static string SELECT_PENDIENTES(QueryConditions conditions)
		{
			conditions.ExtraWhere += @"
				AND ""TOTAL"" != 0 AND COALESCE(CF1.""COBRADO"", 0) + COALESCE(CF2.""NO_VENCIDO"", 0) != F.""TOTAL""
				AND F.""ESTADO_COBRO"" != " + (long)EEstado.DudosoCobro;

			string query = 
			SELECT_BASE(conditions) +
			WHERE(conditions);

			return query;
		}

		private static string SELECT_DUDOSO_COBRO(QueryConditions conditions)
		{
			conditions.ExtraWhere += @"
				AND ""TOTAL"" != 0
				AND F.""ESTADO_COBRO"" = " + (long)EEstado.DudosoCobro;

			string query = 
			SELECT_BASE(conditions) +
			WHERE(conditions);

			return query;
		}

        #endregion
    }
}