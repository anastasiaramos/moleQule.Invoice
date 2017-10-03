using System;
using System.Collections.Generic;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	#region Querys

	public class QueryConditions : moleQule.Library.QueryConditions
    {
		public long Oid = 0;
		public ETipoEntidad EntityType = ETipoEntidad.Todos;
		public EEstado[] Status = null;

		public StoreInfo Almacen = null;
        public CashInfo Caja = null;
        public CierreCajaInfo CierreCaja = null;
        public ClienteInfo Cliente = null;
        public CreditCardStatementInfo CreditCardStatement = null;
		public OutputDeliveryLineInfo ConceptoAlbaran = null;
		public OutputInvoiceLineInfo ConceptoFactura = null;
		public BudgetLineInfo ConceptoProforma = null;
        public BankAccountInfo CuentaBancaria = null;
        public ChargeInfo Cobro = null;
        public FinancialCashInfo Effect = null;
		public ExpedientInfo Expediente = null;
        public OutputInvoiceInfo Factura = null;
        public FamiliaInfo Familia = null;
        public IAcreedorInfo IAcreedor = null;
        public IBankLineInfo IMovimientoBanco = null;
        public ITitular Titular = null;
		public LineaPedidoInfo LineaPedido = null;
        public Modelo Modelo = null;
		public OutputDeliveryInfo OutputDelivery = null;
        public PaymentInfo Pago = null;
		public BatchInfo Partida = null;
		public PedidoInfo Pedido = null;
        public LoanInfo Prestamo = null;
		public ProductInfo Producto = null;
		public BudgetInfo Proforma = null;
		public SerieInfo Serie = null;
		public CreditCardInfo TarjetaCredito = null;
		public TicketInfo Ticket = null;
		public TipoClienteInfo TipoCliente = null;
		public TransactionInfo Transaction = null;
		public TraspasoInfo Traspaso = null;
		public UserInfo Usuario = null;
		public WorkReportInfo WorkReport = null;

		public EEstado Estado = EEstado.Todos;
        public ELoanType LoanType = ELoanType.All;
		public EMedioPago MedioPago = EMedioPago.Todos;
		public ETipoAcreedor TipoAcreedor = ETipoAcreedor.Todos;
		public ETipoAlbaranes TipoAlbaranes = ETipoAlbaranes.Todos;
        public ETipoAyudaContabilidad TipoAyudas = ETipoAyudaContabilidad.Todas;
		public ETipoCobro TipoCobro = ETipoCobro.Todos;
		public ETipoExpediente TipoExpediente = ETipoExpediente.Todos;
		public ETipoEntidad TipoEntidad = ETipoEntidad.Todos;
		public ETipoFactura TipoFactura = ETipoFactura.Todas;
		public ETipoFacturas TipoFacturas = ETipoFacturas.Todas;
        public EBankLineType TipoMovimientoBanco = EBankLineType.Todos;
		public ETipoPago TipoPago = ETipoPago.Todos;
        public ETipoProducto TipoProducto = ETipoProducto.Todos;
        public ETipoTitular TipoTitular = ETipoTitular.Todos;
        public ECuentaBancaria TipoCuenta = ECuentaBancaria.Principal;

        public List<EMedioPago> MedioPagoList = null;

		public static Common.QueryConditions ConvertTo(Invoice.QueryConditions conditions)
		{
			Common.QueryConditions conds = new Common.QueryConditions
			{
				Oid = conditions.Oid,
				EntityType = conditions.EntityType,
				Status = conditions.Status,

				Year = conditions.Year,
				FechaIni = conditions.FechaIni,
				FechaFin = conditions.FechaFin,
				FechaAuxIni = conditions.FechaAuxIni,
				FechaAuxFin = conditions.FechaAuxFin,
				Estado = conditions.Estado,
				TipoEntidad = conditions.TipoEntidad,
				Modelo = conditions.Modelo,
			};

			return conds;
		}
		public static Invoice.QueryConditions ConvertTo(Common.QueryConditions conditions)
		{
			Invoice.QueryConditions conds = new Invoice.QueryConditions
			{
				FechaIni = conditions.FechaIni,
				FechaFin = conditions.FechaFin,
				FechaAuxIni = conditions.FechaAuxIni,
				FechaAuxFin = conditions.FechaAuxFin,
				Estado = conditions.Estado
			};

			return conds;
		}

		public static class EnumConvert
		{
			public static EEstado ToEEstado(ECobro source)
			{
				switch (source)
				{
					case ECobro.Cobrado: return EEstado.Pagado;
					case ECobro.Pendiente: return EEstado.Pendiente;
					case ECobro.Todos: return EEstado.Todos;
				}

				return EEstado.Todos;
			}
		}
    }

	public delegate string SelectCaller(QueryConditions conditions);

    #endregion

    #region Enums

    public enum EBankLineType
    {
        Todos = 0,
        Cobro = 1, SalidaCaja = 2, PagoFactura = 3, EntradaCaja = 4, PagoGasto = 5,
        PagoNomina = 6, Traspaso = 7, Manual = 8, Ticket = 9, ExtractoTarjeta = 10,
        Prestamo = 11, PagoPrestamo = 12, CancelacionComercioExterior = 13, ComisionEstudioApertura = 14,
        Interes = 15, CobroEfecto = 16, PagoGastosEfecto = 17, PagoGastosAdelantoEfecto = 18, PagoGastosDevolucionEfecto = 19
    }

    public enum ECobro { Todos, Cobrado, Pendiente }

	public enum ECuentaBancaria { Principal = 1, Asociada = 2 }

    public enum EElementoExportacion { FacturaRecibida, FacturaEmitida, Pago, Cobro, Gasto, Nomina, Ayuda, Traspaso, Prestamo }

	public enum EPaymentGateway { All = 0, TefPay = 1, PayPal = 2 }

    public enum ETipoAyudaContabilidad { Todas = 0, REA = 1, POSEI = 2 }

    public enum ETipoCobro { Todos = 0, Cliente = 1, REA = 2, Fomento = 3 }
	
	public enum ETipoLineaCaja 
	{ 
		Todos = 0, EntradaPorCobro = 1, EntradaPorTraspaso = 2, SalidaPorPago = 3, 
		SalidaPorIngreso = 4, Otros = 5, EntradaPorTicket = 6, EntradaPorTarjetaCredito = 7 
	}

    public enum ELoanType { All = 0, Bank = 1, Merchant = 2 }

    public enum ETipoApunteBancario
    { 
        Sencillo = 1, Multiple = 2, Mixto = 3
    }

	public enum ETransactionType { All = 0, Authentication = 1, Preauthorization = 2, PreauthCharge = 3, PreauthCancelation = 4}

    public static class EnumConvert
    {
        public static ETipoTitular ToETipoTitular(ETipoCobro source)
        {
            switch (source)
            {
                case ETipoCobro.Cliente: return ETipoTitular.Cliente;
                case ETipoCobro.REA: return ETipoTitular.REA;
            }

            return ETipoTitular.Todos;
        }
    }

    public class EnumText<T> : EnumTextBase<T>
    {
		public static ComboBoxList<T> GetList(bool emptyValue = true, bool allValue = false, bool specialValues = false)
        {
			return GetList(Resources.Enums.ResourceManager, emptyValue, specialValues, allValue);
        }

		public static ComboBoxList<T> GetList(T[] list)
		{
			return GetList(Resources.Enums.ResourceManager, list);
		}

        public static string GetLabel(object value)
        {
            return GetLabel(Resources.Enums.ResourceManager, value);
        }

		public static string GetPrintLabel(object value)
		{
			return GetPrintLabel(Resources.Enums.ResourceManager, value);
		}
    }

    #endregion
    	
	#region Formats & Reports

	public struct ReportFilter
	{
		public EReportVista Vista;
		public DateTime FechaIni, FechaFin;

		public object objeto_detallado;
		public EPagos tipo;
		public DateTime fecha_fac_inicio, fecha_fac_final, fecha_pago_inicio, fecha_pago_final, prevision_ini, prevision_fin;
		public ETipoExpediente tipo_expediente;
		public string exp_inicial, exp_final;
		public ETipoInforme tipo_informe;
		public bool SoloMermas;
		public bool SoloStock;
		public bool SoloIncompletos;
	}

    public struct FormatConfCarteraClientesReport
    {
		public ETipoFacturas tipo;
		public DateTime inicio, final;
        public bool orden_ascendente;
        public bool resumido;
		public bool verCobros;
        public string campo_ordenacion;
        public ETipoExpediente tipo_expediente;
    }

    public struct FormatConfInformeFomentoReport
    {
        public string puerto_origen, puerto_destino;
        public DateTime finicial, ffinal;
        public NavieraInfo naviera;
    }

    public struct FormatConfFacturaAlbaranReport
    {
        public string nota;
		public string copia;
		public string cabecera;
		public string cuenta_bancaria;
    }

    #endregion
}
