using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    public enum ETipoApunteA3 
    {   
        AltaApuntesSinIVA = 0, 
        CabeceraFactura = 1, 
        CabeceraRectificativaAbono = 2, 
        FacturasyRectificativas = 9,
        AltaDeVencimientos = 10,
        AmpliacionAltaDeVencimientos = 11
    }

    public enum ETipoFacturaA3 { Ventas = 1, Compras = 2, BienesInversion = 3 }

    public enum ETipoImporte { Cargo = 1, Abono = 2, Debe = 3, Haber = 4 }

    public enum ETipoVencimiento { Cobro = 1, Pago = 2 }

    public class ApunteA3 : IApunteContable
    {
        #region IApunteContable

        public EColumnaApunte Columna { get; set; }
        public DateTime Fecha { get; set; }
        public string CuentaContable { get; set; }
        public string CuentaContableContrapartida { get; set; }
        public string Descripcion { get; set; }
        public string Titular { get; set; }
        public string Vat { get; set; }
        public string NFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public Decimal Importe { get; set; }
        public string Documento { get; set; }

        public Decimal BaseImponible { get; set; }
        public Decimal Total { get; set; }
        public ETipoImpuestoApunte TipoImpuesto { get; set; }
        public Decimal Porcentaje { get; set; }
        public ETipoApunteA3 Tipo { get; set; }
        public ETipoFacturaA3 TipoFactura { get; set; }
        public ETipoImporte TipoImporte { get; set; }
        public ETipoVencimiento TipoVencimiento { get; set; }
        public string TipoCobroPago { get; set; }
        public string Estado { get; set; }
        public string CuentaBancaria { get; set; }
        public string Entidad { get; set; }

        #endregion

        #region Properties

        public ETipoRegistroApunte TipoRegistro { get; set; }
        public EPosicionApunte Posicion { get; set; }
        public string NumeroDocumento { get; set; }
        public string SubtipoFactura { get; set; }
        public decimal P_IVA { get; set; }
        public decimal IVA { get; set; }
        public decimal PRecargo { get; set; }
        public decimal Recargo { get; set; }
        public decimal PRetencion { get; set; }
        public decimal Retencion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string NombreTitular { get; set; }
        public string NIFTitular { get; set; }
        public string CodPostalTitular { get; set; }

        #endregion
    }

	public class A3Exporter : ContabilidadExporterBase, IContabilidadExporter
	{
		#region Attributes & Properties

		const string ENLACE_FILE_NAME = "SUENLACE.DAT";
		const string ACTIVOS_FILE_NAME = "ACTIVOS.DAT";
		const string DATOSEOS_FILE_NAME = "DATOSEOS.DAT";

		StreamWriter _enlace_file = null;
		StreamWriter _activos_file = null;
		StreamWriter _datoseos_file = null;

		string _enlace_file_name = string.Empty;
		string _activos_file_name = string.Empty;
		string _datoseos_file_name = string.Empty;

		#endregion

		#region Factory Methods

		public A3Exporter() { }

		public override void Init(ContabilidadConfig config)
		{
			base.Init(config);

			_enlace_file_name = _config.RutaSalida + ENLACE_FILE_NAME;
			_activos_file_name = _config.RutaSalida + ACTIVOS_FILE_NAME;
			_datoseos_file_name = _config.RutaSalida + DATOSEOS_FILE_NAME;

			if (File.Exists(_enlace_file_name)) File.Delete(_enlace_file_name);
			if (File.Exists(_activos_file_name)) File.Delete(_activos_file_name);
			if (File.Exists(_datoseos_file_name)) File.Delete(_datoseos_file_name);
		}

		private void CreateFile(ETipoFile tipo)
		{
			switch (tipo)
			{
				case ETipoFile.General:
					if (!File.Exists(_enlace_file_name))
						_enlace_file = new StreamWriter(_enlace_file_name, false, Encoding.GetEncoding(1252));
					break;

				case ETipoFile.Activos:
					if (!File.Exists(_activos_file_name))
						_activos_file = new StreamWriter(_activos_file_name, false, Encoding.GetEncoding(1252));
					break;

				case ETipoFile.Estimaciones:
					if (!File.Exists(_datoseos_file_name))
						_datoseos_file = new StreamWriter(_datoseos_file_name, false, Encoding.GetEncoding(1252));
					break;
			}
		}

		public override void SaveFiles()
		{
			if (_enlace_file != null) { _enlace_file.Close(); _enlace_file = null; }

			if (_activos_file != null)
			{
				_activos_file.Close();
				_activos_file = null;

				FileInfo file = new FileInfo(_activos_file_name);
				if (file.Length == 0)
					File.Delete(_activos_file_name);
			}

			if (_datoseos_file != null) { _datoseos_file.Close(); _datoseos_file = null; }

			base.Close();
		}

		#endregion

		#region Business Methods

		protected override string AddValue(string value) { return value + ";"; }

		public override void ExportInputInvoices()
		{
			CreateFile(ETipoFile.General);

			base.ExportInputInvoices();
		}

		public override void ExportPayments()
		{
			CreateFile(ETipoFile.General);

			Payments pagos2 = null;

			try
            {
                _store_conditions.FechaAuxIni = _store_conditions.FechaIni;
                _store_conditions.FechaAuxFin = _store_conditions.FechaFin;

                _store_conditions.FechaIni = DateTime.MinValue;
                _store_conditions.FechaFin = DateTime.MaxValue; 

				PaymentList pagos = PaymentList.GetOrderedByFechaList(_store_conditions, true);

				//if (pagos.Count == 0) return; // throw new iQException(Library.Resources.Messages.NO_RESULTS);

				//Hacen falta todas porque un pago puede estar asociado a una factura que no este en las condiciones del filtro
                _input_invoices = InputInvoiceList.GetList(false);
				_providers = (_providers == null) ? ProviderBaseList.GetList(false) : _providers;
                _employees = (_employees == null) ? EmployeeList.GetList(false) : _employees;
				_expense_types = (_expense_types == null) ? TipoGastoList.GetList(false) : _expense_types;
				_payrolls = (_payrolls == null) ? PayrollList.GetList(false) : _payrolls;

				foreach (PaymentInfo item in pagos)
				{
					if (item.EEstado == EEstado.Anulado) continue;

					LineaRegistro lr = _registry.LineaRegistros.NewItem(_registry, item);

					switch (item.ETipoPago)
					{
						case ETipoPago.Factura:
							{
								BuildInvoicePaymentAccountingEntry(item, lr);
								BuildFinancialCashBookPaymentAccountingEntry(item);
							}
							break;

						case ETipoPago.Nomina:
							{
								BuildPayrollPaymentAccountingEntry(item, lr);
							}
							break;

						case ETipoPago.Gasto:
							{
                                BuildExpensePaymentAccountingEntry(item, lr);
							}
							break;
					}

					_accounting_entry++;
				}

				//Cambiamos el estado de los pagos contabilizados
				pagos2 = Payments.GetList(_store_conditions, false);

                foreach (Payment item in pagos2)
				{
					if (item.EEstado == EEstado.Anulado) continue;

					if (item.EEstado != EEstado.Exportado)
                        /*if (item.EEstadoPago == EEstado.Pagado)*/ item.EEstado = EEstado.Exportado;
                }

                _store_conditions.FechaIni = _store_conditions.FechaAuxIni;
                _store_conditions.FechaFin = _store_conditions.FechaAuxFin;

                _store_conditions.FechaAuxIni = DateTime.MinValue;
                _store_conditions.FechaAuxFin = DateTime.MaxValue;

				pagos2.Save();
			}
			catch (iQException ex)
			{
				_registry = null;
				throw ex;
			}
			catch (Exception ex)
			{
				_registry = null;
				throw ex;
			}
			finally
			{
				if (pagos2 != null) pagos2.CloseSession();
			}
		}

		public override void ExportOutputInvoices()
		{
			CreateFile(ETipoFile.General);

			base.ExportOutputInvoices();
		}

		public override void ExportCharges()
		{
			CreateFile(ETipoFile.General);
            

			Charges cobros2 = null;

			try
			{
                _invoice_conditions.FechaAuxIni = _invoice_conditions.FechaIni;
                _invoice_conditions.FechaAuxFin = _invoice_conditions.FechaFin;

                _invoice_conditions.FechaIni = DateTime.MinValue;
                _invoice_conditions.FechaFin = DateTime.MaxValue;

                ChargeList cobros = ChargeList.GetList(_invoice_conditions, true);
				//Hacen falta todas porque un cobro puede estar asociado a una factura que no este en las condiciones del filtro
				_invoices = OutputInvoiceList.GetList(false);
				_clients = (_clients == null) ? ClienteList.GetList(false) : _clients;

				_expedients = ExpedienteList.GetList(false);

				foreach (ChargeInfo item in cobros)
				{				
					if (item.EEstado == EEstado.Anulado) continue;

					LineaRegistro lr = _registry.LineaRegistros.NewItem(_registry, item);

					switch (item.ETipoCobro)
					{
						case ETipoCobro.Cliente:
							{
								BuildChargeAccountingEntry(item, lr);
								BuildFinalcialCashBookChargeAccountingEntry(item);
							}
							break;

						case ETipoCobro.REA:
							{
								BuildREAChargeAccountingEntry(item, lr);
							}
							break;
					}

					_accounting_entry++;
				}

				//Cambiamos el estado de las cobros contabilizados
                cobros2 = Charges.GetList(_invoice_conditions, false);
                FinancialCashList efectos = FinancialCashList.GetList(false);

				foreach (Charge item in cobros2)
				{
					if (item.EEstado == EEstado.Anulado) continue;

					if (item.EEstado != EEstado.Exportado)
                        if (item.EEstadoCobro == EEstado.Charged)
                        {
                            if (item.EMedioPago != EMedioPago.Cheque &&
                                item.EMedioPago != EMedioPago.Pagare)
                                item.EEstado = EEstado.Exportado;
                            else
                            {
                                FinancialCashInfo efecto = efectos.GetItemByCobro(item.Oid);
                                if (efecto != null && efecto.EEstadoCobro == EEstado.Charged)
                                    item.EEstado = EEstado.Exportado;
                            }
                        }
                }

                _invoice_conditions.FechaIni = _invoice_conditions.FechaAuxIni;
                _invoice_conditions.FechaFin = _invoice_conditions.FechaAuxFin;

                _invoice_conditions.FechaAuxIni = DateTime.MinValue;
                _invoice_conditions.FechaAuxFin = DateTime.MaxValue;

				cobros2.Save();
			}
			catch (iQException ex)
			{
				_registry = null;
				throw ex;
			}
			catch (Exception ex)
			{
				_registry = null;
				throw ex;
			}
			finally
			{
				if (cobros2 != null) cobros2.CloseSession();
			}
		}

		public override void ExportExpenses()
		{
			CreateFile(ETipoFile.General);

			base.ExportExpenses();
		}

		public override void ExportPayrolls()
		{
			CreateFile(ETipoFile.General);

			base.ExportPayrolls();
		}

		public override void ExportGrants()
		{
			CreateFile(ETipoFile.General);

			base.ExportGrants();
		}

        public override void ExportBankTransfers()
        {
            CreateFile(ETipoFile.General);

            base.ExportBankTransfers();
        }

		protected override void BuildInputInvoiceAccountingEntry(InputInvoiceInfo factura, LineaRegistro lr)
		{
			try
			{
                if (factura.Total == 0) return;

				IAcreedorInfo titular = _providers.GetItem(factura.OidAcreedor, factura.ETipoAcreedor);
                
				if (titular.CuentaContable == string.Empty)
					throw new iQException("El deudor nº " + titular.Codigo + " (" + titular.Nombre + ") no tiene cuenta contable asociada", new object[2] { Store.EnumConvert.ToETipoEntidad(titular.ETipoAcreedor), titular.OidAcreedor });
                else
                {
					if (titular.CuentaContable == Resources.Defaults.NO_CONTABILIZAR
						|| titular.CuentaContable == string.Empty.PadLeft(Library.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0'))
					{
						lr.EEstado = EEstado.Desestimado;
						lr.Observaciones = "Este proveedor ha sido configurado para no tenerlo en cuenta en la exportación";
						return;
					}
                }

				// Apuntes en las cuentas de compra
				List<CuentaResumen> cuentas = factura.GetCuentasAndImpuestos(true);

				foreach (CuentaResumen cr in cuentas)
				{
					if (cr.CuentaContable == Resources.Defaults.NO_CONTABILIZAR
						|| cr.CuentaContable == string.Empty.PadLeft(Library.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0'))
					{
						lr.EEstado = EEstado.Desestimado;
						lr.Observaciones = "La factura " + factura.NFactura + " (" + factura.Acreedor + ") contiene conceptos que no se van a contabilizar";
						return;
					}
				}

				// Apuntes en la cuenta del proveedor
				string descripcion = "Fra. " + factura.NFactura + " (" + factura.Acreedor + ")";

                BuildAccountingLine(new ApunteA3
                            {
                                Tipo = ETipoApunteA3.CabeceraFactura,
                                TipoRegistro = (!factura.Rectificativa) ? ETipoRegistroApunte.ApunteConImpuesto : ETipoRegistroApunte.ApunteAbonoConImpuesto,
                                Posicion = EPosicionApunte.Inicial,
                                Fecha = factura.Fecha,
                                CuentaContable = titular.CuentaContable,
                                Titular = titular.Nombre,
                                Total = factura.Rectificativa ? -factura.Total : factura.Total,
                                NumeroDocumento = factura.NFactura,
                                Descripcion = descripcion,
                                TipoFactura = ETipoFacturaA3.Compras,
                                NombreTitular = titular.Nombre,
                                NIFTitular = titular.ID,
                                CodPostalTitular = titular.CodPostal
                            });


				FamiliaInfo familia;

                decimal total_factura = 0;

                foreach (CuentaResumen item in cuentas)
                    total_factura += item.Importe + (item.Impuesto != null ? item.Impuesto.Importe : 0);

                if (total_factura != factura.Total + factura.IRPF)
                    throw new iQException("Fra. " + factura.NFactura + " (" + factura.Acreedor + ") descuadrada");

				foreach (CuentaResumen cr in cuentas)
				{
					familia = _families.GetItem(cr.OidFamilia);

					if (familia == null) throw new iQException("Factura " + factura.NFactura + " con familia a nulo");

					if (cr.CuentaContable == string.Empty)
						throw new iQException("La familia nº " + familia.Codigo + " (" + familia.Nombre + ") no tiene cuenta contable (compra) asociada", new object[2] { ETipoEntidad.Familia, familia.Oid });

					BuildAccountingLine(new ApunteA3
								{
									Tipo = ETipoApunteA3.FacturasyRectificativas,
									TipoRegistro = ETipoRegistroApunte.DetalleImpuesto,
									Posicion = ((cuentas.IndexOf(cr) < (cuentas.Count - 1)) ? EPosicionApunte.Medio : EPosicionApunte.Final),
                                    Fecha = factura.Fecha,
                                    TipoImporte = (cr.Importe < 0 && !factura.Rectificativa) ? ETipoImporte.Abono : ETipoImporte.Cargo,
									CuentaContable = cr.CuentaContable,
									Total = cr.Importe < 0 ? -cr.Importe : cr.Importe,
									Descripcion = descripcion,
                                    SubtipoFactura = cr.Impuesto != null && cr.Impuesto.SubtipoFacturaRecibida != null ? cr.Impuesto.SubtipoFacturaRecibida : string.Empty,
                                    P_IVA = cr.Impuesto != null ? cr.Impuesto.Porcentaje : 0,
                                    IVA = cr.Impuesto != null ? (cr.Impuesto.Importe < 0 ? -cr.Impuesto.Importe : cr.Impuesto.Importe) : 0,
                                    PRecargo = 0,
                                    Recargo = 0,
                                    PRetencion = factura.PIRPF,
                                    Retencion = cr.Importe < 0 ? -cr.Importe * factura.PIRPF / 100 : cr.Importe * factura.PIRPF / 100,
                                    Titular = cr.Nombre,
                                    NumeroDocumento = factura.NFactura,
                                    TipoFactura = ETipoFacturaA3.Compras,
								});
				}                
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_FACTURA, factura.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_FACTURA, factura.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}
		}

		protected override void BuildOutputInvoiceAccountingEntry(OutputInvoiceInfo factura, LineaRegistro lr)
        {
            try
            {
                if (factura.Total == 0) return;

                ClienteInfo titular = _clients.GetItem(factura.OidCliente);

                if (titular.CuentaContable == string.Empty)
                    throw new iQException("El cliente nº " + titular.NumeroClienteLabel + " (" + titular.Nombre + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Cliente, titular.Oid });
                else
                {
					if (titular.CuentaContable == Resources.Defaults.NO_CONTABILIZAR
						|| titular.CuentaContable == string.Empty.PadLeft(Library.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0'))
					{
						lr.EEstado = EEstado.Desestimado;
						lr.Observaciones = "Este cliente ha sido configurado para no tenerlo en cuenta en la exportación";
						return;
					}
                }

				// Apuntes en las cuentas de venta
				List<CuentaResumen> cuentas = factura.GetCuentasAndImpuestosA3();

				foreach (CuentaResumen cr in cuentas)
				{
					if (cr.CuentaContable == Resources.Defaults.NO_CONTABILIZAR
						|| cr.CuentaContable == string.Empty.PadLeft(Library.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0'))
					{
						lr.EEstado = EEstado.Desestimado;
						lr.Observaciones = "La factura " + factura.NFactura + " (" + factura.Cliente + ") contiene conceptos que no se van a contabilizar";
						return;
					}
				}

                string apunte = string.Empty;
                string descripcion = "Fra. " + factura.NFactura + " (" + factura.Cliente + ")";

                bool is_debe = !factura.Rectificativa;
                decimal importe = factura.Total;
               
                BuildAccountingLine(new ApunteA3
                {
                    Tipo = ETipoApunteA3.CabeceraFactura,
                    TipoRegistro = (!factura.Rectificativa) ? ETipoRegistroApunte.ApunteConImpuesto : ETipoRegistroApunte.ApunteAbonoConImpuesto,
                    Posicion = EPosicionApunte.Inicial,
                    Fecha = factura.Fecha,
                    CuentaContable = titular.CuentaContable,
                    Titular = titular.Nombre,
                    Total = factura.Rectificativa ? -factura.Total : factura.Total,
                    NumeroDocumento = factura.NFactura,
                    Descripcion = descripcion,
                    TipoFactura = ETipoFacturaA3.Ventas,
                    NombreTitular = titular.Nombre,
                    NIFTitular = titular.VatNumber,
                    CodPostalTitular=titular.CodigoPostal
                });

                decimal total_factura = 0;

                foreach (CuentaResumen item in cuentas)
                    total_factura += item.Importe + (item.Impuesto != null ? item.Impuesto.Importe : 0);

                if (total_factura != factura.Total + factura.IRPF)
                    throw new iQException("Fra. " + factura.NFactura + " (" + factura.Cliente + ") descuadrada");


                FamiliaInfo familia;

                foreach (CuentaResumen cr in cuentas)
                {
                    familia = _families.GetItem(cr.OidFamilia);

                    if (familia == null) throw new iQException("Factura " + factura.NFactura + " con familia a nulo");

                    if (cr.CuentaContable == string.Empty)
                        throw new iQException("La familia nº " + familia.Codigo + " (" + familia.Nombre + ") no tiene cuenta contable (venta) asociada", new object[2] { ETipoEntidad.Familia, familia.Oid });
                    else
                    {
                        if (cr.CuentaContable == Resources.Defaults.NO_CONTABILIZAR
                            || cr.CuentaContable == string.Empty.PadLeft(Library.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0'))
                            throw new iQException("En la factura " + factura.NFactura + " (" + factura.Cliente + ") se han incluido conceptos que no se van a contabilizar");
                    }

                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.FacturasyRectificativas,
                        TipoRegistro = ETipoRegistroApunte.DetalleImpuesto,
                        Posicion = ((cuentas.IndexOf(cr) < (cuentas.Count - 1)) ? EPosicionApunte.Medio : EPosicionApunte.Final),
                        Fecha = factura.Fecha,
                        TipoImporte = (cr.Importe < 0 && !factura.Rectificativa) ? ETipoImporte.Abono : ETipoImporte.Cargo,
                        CuentaContable = cr.CuentaContable,
                        Total = cr.Importe < 0 ? -cr.Importe : cr.Importe,
                        Descripcion = descripcion,
                        SubtipoFactura = cr.Impuesto != null && cr.Impuesto.SubtipoFacturaEmitida != null ? cr.Impuesto.SubtipoFacturaEmitida : string.Empty,
                        P_IVA = cr.Impuesto != null ? cr.Impuesto.Porcentaje : 0,
                        IVA = cr.Impuesto != null ? (cr.Impuesto.Importe < 0 ? -cr.Impuesto.Importe : cr.Impuesto.Importe) : 0,
                        PRecargo = 0,
                        Recargo = 0,
                        PRetencion = factura.PIRPF,
                        Retencion = cr.Importe < 0 ? -cr.Importe * factura.PIRPF / 100 : cr.Importe * factura.PIRPF / 100,
                        Titular = cr.Nombre,
                        NumeroDocumento = factura.NFactura,
                        TipoFactura = ETipoFacturaA3.Ventas,
                    });
                }
            }
            catch (iQException ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_FACTURA, factura.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
            }
            catch (Exception ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_FACTURA, factura.Codigo, iQExceptionHandler.GetAllMessages(ex)));
            }
		}

		protected override void BuildInvoicePaymentAccountingEntry(PaymentInfo pago, LineaRegistro lr)
		{
            try
            {
                if (pago.EMedioPago == EMedioPago.CompensacionFactura) return;

                if (pago.Importe == 0) return;

                if (pago.ETipoPago != ETipoPago.Factura) return;

                IAcreedorInfo titular = _providers.GetItem(pago.OidAgente, pago.ETipoAcreedor);
                
                InputInvoiceInfo factura = null;

                if (titular.CuentaContable == string.Empty)
                    throw new iQException("El deudor nº " + titular.Codigo + " (" + titular.Nombre + ") no tiene cuenta contable asociada", new object[2] { Store.EnumConvert.ToETipoEntidad(titular.ETipoAcreedor), titular.OidAcreedor });

                string descripcion = string.Empty;

                BuildAccountingLine(new ApunteA3
                {
                    Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                    Fecha = pago.Fecha,
                    TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                    CuentaContable = GetPaymentAccount(pago),
                    TipoImporte = pago.Importe < 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                    NFactura = pago.Fecha.ToString("yyyyMMdd"),
                    Posicion = EPosicionApunte.Inicial,
                    Descripcion = "P.Fra " + titular.Nombre,
                    Total = pago.Importe < 0 ? -pago.Importe : pago.Importe
                });

                int apuntes = 0;

                foreach (TransactionPaymentInfo pf in pago.Operations)
                {
                    factura = _input_invoices.GetItem(pf.OidOperation);
                    descripcion = string.Format(Resources.Messages.APUNTE_PAGO_FACTURA, factura.NFactura, factura.Acreedor);

                    //apunte en la cuenta del proveedor
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = titular.CuentaContable,
                        TipoImporte = pf.Cantidad < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                        NFactura = factura.Codigo,
                        Posicion = pago.Operations.IndexOf(pf) == pago.Operations.Count - 1 && pago.GastosBancarios == 0 && pago.Pendiente == 0 ? EPosicionApunte.Final : EPosicionApunte.Medio,
                        Descripcion = descripcion,
                        Total = pf.Cantidad < 0 ? -pf.Cantidad : pf.Cantidad
                    });
                    apuntes++;
                }

                if (pago.Pendiente != 0)
                {
                    //apunte en la cuenta del proveedor
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = titular.CuentaContable,
                        TipoImporte = pago.Pendiente < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                        NFactura = string.Empty,
                        Posicion = pago.GastosBancarios == 0 ? EPosicionApunte.Final : EPosicionApunte.Medio,
                        Descripcion = "A CUENTA",
                        Total = pago.Pendiente < 0 ? -pago.Pendiente : pago.Pendiente
                    });
                    apuntes++;
                }

                if (pago.GastosBancarios != 0)
                {
                    //Debe en la cuenta contable de gastos del banco
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = GetBankExpensesAccount(pago),
                        TipoImporte = pago.GastosBancarios < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                        NFactura = pago.Fecha.ToString("yyyyMMdd"),
                        Posicion = EPosicionApunte.Medio,
                        Descripcion = "Gastos Bancarios " + pago.IDPagoLabel,
                        Total = pago.GastosBancarios < 0 ? -pago.GastosBancarios : pago.GastosBancarios
                    });
                    apuntes++;


                    //Haber en la cuenta contable del banco
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = GetPaymentAccount(pago),
                        TipoImporte = pago.GastosBancarios < 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                        NFactura = string.Empty,
                        Posicion = EPosicionApunte.Final,
                        Descripcion = "Gastos Bancarios " + pago.IDPagoLabel,
                        Total = pago.GastosBancarios < 0 ? -pago.GastosBancarios : pago.GastosBancarios
                    });
                    apuntes++;
                }

                if (apuntes == 0) 
                    throw new iQException("Apunte no válido");
            }
            catch (iQException ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
            }
            catch (Exception ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)));
            }
		}
		protected override void BuildPayrollPaymentAccountingEntry(PaymentInfo pago, LineaRegistro lr)
        {
            try
            {
                if (pago.ETipoPago != ETipoPago.Nomina) return;

                if (pago.Importe == 0) return;

                string apunte = string.Empty;
                string descripcion = string.Empty;

                BuildAccountingLine(new ApunteA3
                {
                    Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                    Fecha = pago.Fecha,
                    TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                    CuentaContable = GetPaymentAccount(pago),
                    TipoImporte = pago.Importe < 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                    NFactura = pago.Fecha.ToString("yyyyMMdd"),
                    Posicion = EPosicionApunte.Inicial,
                    Descripcion = "P.Nómina ",
                    Total = pago.Importe < 0 ? -pago.Importe : pago.Importe
                });

                int apuntes = 0;

                foreach (ExpenseInfo item in pago.Gastos)
                {
                    EmployeeInfo empleado = _employees.GetItem(item.OidEmpleado);

                    if (empleado.CuentaContable == string.Empty)
                        throw new iQException("El empleado nº " + empleado.Codigo + " (" + empleado.NombreCompleto + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Empleado, empleado.OidAcreedor });

                    descripcion = empleado.NombreCompleto;

                    //apunte en la cuenta del tipo de gasto
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = empleado.CuentaContable,
                        TipoImporte = item.Asignado < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                        NFactura = item.IDRemesaNomina,
                        Posicion = pago.Operations.Count == 0 && pago.Gastos.IndexOf(item) == pago.Gastos.Count - 1 && pago.GastosBancarios == 0 && pago.Pendiente == 0 ? EPosicionApunte.Final : EPosicionApunte.Medio,
                        Descripcion = descripcion,
                        Total = item.Asignado < 0 ? -item.Asignado : item.Asignado
                    });

                    apuntes++;
                }

                foreach (TransactionPaymentInfo pf in pago.Operations)
                {
                    NominaInfo nomina = _payrolls.GetItem(pf.OidOperation);
                    EmployeeInfo empleado = _employees.GetItem(nomina.OidEmpleado);

                    if (empleado.CuentaContable == string.Empty)
                        throw new iQException("El empleado nº " + empleado.Codigo + " (" + empleado.NombreCompleto + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Empleado, empleado.OidAcreedor });

                    descripcion = empleado.NombreCompleto;
                    //apunte en la cuenta del proveedor
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = empleado.CuentaContable,
                        TipoImporte = pf.Cantidad < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                        NFactura = nomina.IDRemesa,
                        Posicion = pago.Operations.IndexOf(pf) == pago.Operations.Count - 1 && pago.GastosBancarios == 0 && pago.Pendiente == 0 ? EPosicionApunte.Final : EPosicionApunte.Medio,
                        Descripcion = descripcion,
                        Total = pf.Cantidad < 0 ? -pf.Cantidad : pf.Cantidad
                    });
                    apuntes++;
                }

                if (pago.Pendiente != 0)
                {
                    EmployeeInfo empleado = _employees.GetItem(pago.OidAgente); 

                    //apunte en la cuenta del empleado
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = empleado.CuentaContable,
                        TipoImporte = pago.Pendiente < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                        NFactura = string.Empty,
                        Posicion = pago.GastosBancarios == 0 ? EPosicionApunte.Final : EPosicionApunte.Medio,
                        Descripcion = "A CUENTA",
                        Total = pago.Pendiente < 0 ? -pago.Pendiente : pago.Pendiente
                    });
                    apuntes++;
                }

                if (pago.GastosBancarios != 0)
                {
                    //Debe en la cuenta contable de gastos del banco
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = GetBankExpensesAccount(pago),
                        TipoImporte = pago.GastosBancarios < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                        NFactura = pago.Fecha.ToString("yyyyMMdd"),
                        Posicion = EPosicionApunte.Medio,
                        Descripcion = "Gastos Bancarios " + pago.IDPagoLabel,
                        Total = pago.GastosBancarios < 0 ? -pago.GastosBancarios : pago.GastosBancarios
                    });

                    apuntes++;

                    //Haber en la cuenta contable del banco
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = GetPaymentAccount(pago),
                        TipoImporte = pago.GastosBancarios < 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                        NFactura = string.Empty,
                        Posicion = EPosicionApunte.Final,
                        Descripcion = "Gastos Bancarios " + pago.IDPagoLabel,
                        Total = pago.GastosBancarios < 0 ? -pago.GastosBancarios : pago.GastosBancarios
                    });

                    apuntes++;
                }

                if (apuntes == 0) 
                    throw new iQException("Apunte no válido");
            }
            catch (iQException ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
            }
            catch (Exception ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)));
            }
			/*try
			{
				if (pago.ETipoPago != ETipoPago.Nomina) return;

				string apunte = string.Empty;
				string descripcion = string.Empty;
				decimal importe_pago = 0;

				foreach (ExpenseInfo item in pago.Gastos)
				{
					EmployeeInfo empleado = _empleados.GetItem(item.OidEmpleado);

					if (empleado.CuentaContable == string.Empty)
						throw new iQException("El empleado nº " + empleado.Codigo + " (" + empleado.NombreCompleto + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Empleado, empleado.OidAcreedor });

					descripcion = empleado.NombreCompleto;

					decimal importe = item.Asignado;

					//apunte en la cuenta del acreedor
					BuildApunte(new ApunteA3
					{
						Tipo = ETipoApunteA3.General,
						Columna = EColumnaApunte.Debe,
						Fecha = pago.Fecha,
						CuentaContable = empleado.CuentaContable,
						Importe = importe,
						//Libre2 = pago.Codigo,
						Descripcion = descripcion,
					});
				}

				//Sumamos los gastos bancarios
				importe_pago += pago.Importe + pago.GastosBancarios;
				descripcion = String.Format(Resources.Messages.APUNTE_REMESA_NOMINA, pago.Codigo);

				//apunte en la cuenta del pago
				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.General,
					Columna = EColumnaApunte.Haber,
					Fecha = pago.Fecha,
					CuentaContable = GetCuentaPago(pago),
					Importe = -importe_pago,
					//Libre2 = pago.Codigo,
					Descripcion = descripcion,
				});

				if (pago.GastosBancarios != 0)
				{
					apunte = string.Empty;

					descripcion = String.Format(Resources.Messages.APUNTE_GASTOS_BANCARIOS, pago.Codigo);

					//apunte en la cuenta de gastos bancarios
					BuildApunte(new ApunteA3
					{
						Tipo = ETipoApunteA3.General,
						Columna = EColumnaApunte.Debe,
						Fecha = pago.Fecha,
						CuentaContable = GetCuentaGastosBancarios(pago),
						Importe = pago.GastosBancarios,
						//Libre2 = pago.Codigo,
						Descripcion = descripcion,
					});
				}
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}*/
		}
        protected override void BuildExpensePaymentAccountingEntry(PaymentInfo pago, LineaRegistro lr)
        {
            try
            {
                if (pago.ETipoPago != ETipoPago.Gasto) return;

                if (pago.Importe == 0) return;

                foreach (ExpenseInfo item in pago.Gastos)
                {
                    TipoGastoInfo tipoGasto = _expense_types.GetItem(item.OidTipo);

                    if (tipoGasto.CuentaContable == string.Empty)
                        throw new iQException("El tipo de gasto nº " + tipoGasto.Codigo + " (" + tipoGasto.Nombre + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.TipoGasto, tipoGasto.Oid });
                    else
                    {
                        if (tipoGasto.CuentaContable == Resources.Defaults.NO_CONTABILIZAR
                            || tipoGasto.CuentaContable == string.Empty.PadLeft(Library.Common.ModulePrincipal.GetNDigitosCuentasContablesSetting(), '0'))
                            return;
                    }
                }

                string apunte = string.Empty;
				string descripcion = string.Empty;

                BuildAccountingLine(new ApunteA3
                {
                    Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                    Fecha = pago.Fecha,
                    TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                    CuentaContable = GetPaymentAccount(pago),
                    TipoImporte = pago.Importe < 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                    NFactura = pago.Fecha.ToString("yyyyMMdd"),
                    Posicion = EPosicionApunte.Inicial,
                    Descripcion = "P.Gasto ",
                    Total = pago.Importe < 0 ? -pago.Importe : pago.Importe
                });

                int apuntes = 0;
                
                foreach (ExpenseInfo item in pago.Gastos)
                {
                    TipoGastoInfo tipoGasto = _expense_types.GetItem(item.OidTipo);

                    if (tipoGasto.CuentaContable == Resources.Defaults.NO_CONTABILIZAR) return;

                    if (tipoGasto.CuentaContable == string.Empty)
                        throw new iQException("El tipo de gasto nº " + tipoGasto.Codigo + " (" + tipoGasto.Nombre + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.TipoGasto, tipoGasto.Oid });

                    descripcion = item.Descripcion;
                    
                    //apunte en la cuenta del tipo de gasto
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = tipoGasto.CuentaContable,
                        TipoImporte = item.Asignado < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                        NFactura = tipoGasto.Codigo,
                        Posicion = pago.Gastos.IndexOf(item) == pago.Gastos.Count - 1 && pago.GastosBancarios == 0 ? EPosicionApunte.Final : EPosicionApunte.Medio,
                        Descripcion = descripcion,
                        Total = item.Asignado < 0 ? -item.Asignado : item.Asignado
                    });

                    apuntes++;
                }

                if (pago.GastosBancarios != 0)
                {
                    //Debe en la cuenta contable de gastos del banco
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = GetBankExpensesAccount(pago),
                        TipoImporte = pago.GastosBancarios < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                        NFactura = pago.Fecha.ToString("yyyyMMdd"),
                        Posicion = EPosicionApunte.Medio,
                        Descripcion = "Gastos Bancarios " + pago.IDPagoLabel,
                        Total = pago.GastosBancarios < 0 ? -pago.GastosBancarios : pago.GastosBancarios
                    });

                    apuntes++;


                    //Haber en la cuenta contable del banco
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = pago.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = GetPaymentAccount(pago),
                        TipoImporte = pago.GastosBancarios < 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                        NFactura = string.Empty,
                        Posicion = EPosicionApunte.Final,
                        Descripcion = "Gastos Bancarios " + pago.IDPagoLabel,
                        Total = pago.GastosBancarios < 0 ? -pago.GastosBancarios : pago.GastosBancarios
                    });

                    apuntes++;
                }

                if (apuntes == 0)
                    throw new iQException("Apunte no válido");
            }
            catch (iQException ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
            }
            catch (Exception ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)));
            }
			/*try
			{
				if (pago.ETipoPago != ETipoPago.Gasto) return;

				string apunte = string.Empty;
				string descripcion = string.Empty;
				decimal importe_pago = 0;

				foreach (ExpenseInfo item in pago.Gastos)
				{
					TipoGastoInfo tipoGasto = _tipos_gastos.GetItem(item.OidTipo);

					if (tipoGasto.CuentaContable == Resources.Defaults.NO_CONTABILIZAR) return;

					if (tipoGasto.CuentaContable == string.Empty)
						throw new iQException("El tipo de gasto nº " + tipoGasto.Codigo + " (" + tipoGasto.Nombre + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.TipoGasto, tipoGasto.Oid });

					descripcion = item.Descripcion;

					decimal importe = item.Total;

					//apunte en la cuenta del gasto
					BuildApunte(new ApunteA3
					{
						Tipo = ETipoApunteA3.General,
						Columna = EColumnaApunte.Debe,
						Fecha = pago.Fecha,
						CuentaContable = tipoGasto.CuentaContable,
						Importe = importe,
						//Libre2 = pago.Codigo,
						Descripcion = descripcion,
					});
				}

				//Sumamos los gastos bancarios
				importe_pago += pago.Importe + pago.GastosBancarios;
				descripcion = String.Format(Resources.Messages.APUNTE_PAGO_GASTO, pago.Codigo);

				//apunte en la cuenta del pago
				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.General,
					Columna = EColumnaApunte.Haber,
					Fecha = pago.Fecha,
					CuentaContable = GetCuentaPago(pago),
					Importe = -importe_pago,
					//Libre2 = pago.Codigo,
					Descripcion = descripcion,
				});

				if (pago.GastosBancarios != 0)
				{
					apunte = string.Empty;

					descripcion = String.Format(Resources.Messages.APUNTE_GASTOS_BANCARIOS, pago.Codigo);

					//apunte en la cuenta de gastos bancarios
					BuildApunte(new ApunteA3
					{
						Tipo = ETipoApunteA3.General,
						Columna = EColumnaApunte.Debe,
						Fecha = pago.Fecha,
						CuentaContable = GetCuentaGastosBancarios(pago),
						Importe = pago.GastosBancarios,
						//Libre2 = pago.Codigo,
						Descripcion = descripcion,
					});
				}
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}*/
		}
        protected override void BuildCreditCardStatementPaymentAccountingEntry(PaymentInfo payment, LineaRegistro lr)
        {
            try
            {
                if (payment.ETipoPago != ETipoPago.ExtractoTarjeta) return;           

                //DEBE: Cuenta de la tarjeta de crédito

                //Se suma el total del pago y los gastos asociados
                decimal payment_amount = payment.Importe + payment.GastosBancarios;

                //apunte en la cuenta del pago
                BuildAccountingLine(new ApunteA3
                {
                    Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                    Fecha = payment.Fecha,
                    TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                    CuentaContable = GetCreditCardAccount(payment, ETipoEntidad.TarjetaCredito),
                    TipoImporte = payment_amount > 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                    NFactura = payment.Fecha.ToString("yyyyMMdd"),
                    Posicion = EPosicionApunte.Inicial,
                    Descripcion = String.Format(Resources.Messages.APUNTE_PAGO_EXTRACTO_TARJETA, payment.Codigo),
                    Total = payment_amount
                });

                //HABER: Cuenta del banco asociada a la tarjeta de crédito

                //apunte en la cuenta del banco
                BuildAccountingLine(new ApunteA3
                {
                    Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                    Fecha = payment.Fecha,
                    TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                    CuentaContable = GetCreditCardAccount(payment, ETipoEntidad.CuentaBancaria),
                    TipoImporte = payment_amount > 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                    NFactura = payment.Fecha.ToString("yyyyMMdd"),
                    Posicion = EPosicionApunte.Final,
                    Descripcion = String.Format(Resources.Messages.APUNTE_PAGO_EXTRACTO_TARJETA, payment.Codigo),
                    Total = payment_amount
                });
            }
            catch (iQException ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, payment.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
            }
            catch (Exception ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, payment.Codigo, iQExceptionHandler.GetAllMessages(ex)));
            }
        }

		protected override void BuildChargeAccountingEntry(ChargeInfo cobro, LineaRegistro lr)
		{
			try
			{
                if (cobro.EMedioPago == EMedioPago.CompensacionFactura) return;

				OutputInvoiceInfo factura;
				ClienteInfo titular = _clients.GetItem(cobro.OidCliente);

				if (titular.CuentaContable == Resources.Defaults.NO_CONTABILIZAR) return;

				string descripcion = string.Empty;

				if (titular.CuentaContable == string.Empty)
					throw new iQException("El cliente nº " + titular.NumeroClienteLabel + " (" + titular.Nombre + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Cliente, titular.Oid });
				else if (titular.CuentaContable == Resources.Defaults.NO_CONTABILIZAR)
					return;

                BuildAccountingLine(new ApunteA3 
                {
                    Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                    Fecha = cobro.Fecha,
                    TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                    CuentaContable = GetChargeAccount(cobro),
                    TipoImporte = cobro.Importe < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                    NFactura = cobro.Fecha.ToString("yyyyMMdd"),
                    Posicion = EPosicionApunte.Inicial,
                    Descripcion = "C/Fra. " + titular.Nombre,
                    Total = cobro.Importe < 0 ? -cobro.Importe : cobro.Importe
                });

                int apuntes = 0;

                foreach (CobroFacturaInfo cf in cobro.CobroFacturas)
                {
                    factura = _invoices.GetItem(cf.OidFactura);
                    descripcion = "Cobro Fra. " + factura.NFactura + " (" + factura.Cliente + ")";

                    //apunte en la cuenta del cliente
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = cobro.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = titular.CuentaContable,
                        TipoImporte = cf.Cantidad < 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                        NFactura = factura.Codigo,
                        Posicion = cobro.CobroFacturas.IndexOf(cf) == cobro.CobroFacturas.Count - 1 && cobro.GastosBancarios == 0 && cobro.Pendiente == 0 ? EPosicionApunte.Final : EPosicionApunte.Medio,
                        Descripcion = descripcion,
                        Total = cf.Cantidad < 0 ? -cf.Cantidad : cf.Cantidad
                    });

                    apuntes++;
                }

                if (cobro.Pendiente != 0)
                {
                    //apunte en la cuenta del cliente
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = cobro.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = titular.CuentaContable,
                        TipoImporte = cobro.Pendiente < 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                        NFactura = string.Empty,
                        Posicion = cobro.GastosBancarios == 0 ? EPosicionApunte.Final : EPosicionApunte.Medio,
                        Descripcion = "A CUENTA",
                        Total = cobro.Pendiente < 0 ? -cobro.Pendiente : cobro.Pendiente
                    });

                    apuntes++;
                }

                if (cobro.GastosBancarios != 0)
                {
                    //Debe en la cuenta contable de gastos del banco
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = cobro.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = GetBankExpensesAccount(cobro),
                        TipoImporte = cobro.GastosBancarios < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                        NFactura = cobro.Fecha.ToString("yyyyMMdd"),
                        Posicion = EPosicionApunte.Medio,
                        Descripcion = "Gastos Bancarios " + cobro.IDCobroLabel,
                        Total = cobro.GastosBancarios < 0 ? -cobro.GastosBancarios : cobro.GastosBancarios
                    });

                    apuntes++;


                    //Haber en la cuenta contable del banco
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = cobro.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = GetChargeAccount(cobro),
                        TipoImporte = cobro.GastosBancarios < 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                        NFactura = string.Empty,
                        Posicion = EPosicionApunte.Final,
                        Descripcion = "Gastos Bancarios " + cobro.IDCobroLabel,
                        Total = cobro.GastosBancarios < 0 ? -cobro.GastosBancarios : cobro.GastosBancarios
                    });

                    apuntes++;
                }

                if (apuntes == 0) 
                    throw new iQException("Apunte no válido");
                                
				/*foreach (CobroFacturaInfo cf in cobro.CobroFacturas)
				{
					factura = _fact_clientes.GetItem(cf.OidFactura);
					descripcion = "Cobro Fra. " + factura.NFactura + " (" + factura.Cliente + ")";
                    
					//apunte en la cuenta del cliente
					BuildApunte(new ApunteA3
					{
						Tipo = ETipoApunteA3.AltaDeVencimientos,
						Fecha = cobro.Fecha,
                        FechaVencimiento = cobro.Vencimiento,
                        TipoRegistro = ETipoRegistroApunte.AltaVencimiento,
						CuentaContable = titular.CuentaContable,
						Descripcion = descripcion,
                        TipoVencimiento = ETipoVencimiento.Cobro,
                        NumeroDocumento = cobro.Codigo,
                        Total = cobro.Importe,
                        FechaFactura = factura.Fecha,
                        CuentaContableContrapartida = GetCuentaCobro(cobro),
                        NFactura = factura.Codigo
					});

                    BuildApunte(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AmpliacionAltaDeVencimientos,
                        Fecha = cobro.Fecha,
                        FechaVencimiento = cobro.Vencimiento,
                        TipoRegistro = ETipoRegistroApunte.AltaVencimientoAmpliado,
                        CuentaContable = titular.CuentaContable,
                        Descripcion = descripcion,
                        TipoVencimiento = ETipoVencimiento.Cobro,
                        NumeroDocumento = cobro.Codigo,
                        Total = cobro.Importe,
                        FechaFactura = factura.Fecha,
                        CuentaContableContrapartida = GetCuentaCobro(cobro),
                        NFactura = factura.Codigo,
                        CuentaBancaria = string.Concat(cobro.CuentaBancaria.Split(' ')).PadLeft(20).Substring(0, 20),
                        Entidad = cobro.Entidad,
                        TipoCobroPago = GetFormaPagoCobro(cobro.EMedioPago),
                        Estado = GetEstadoCobro(cobro)
                    });
				}*/
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_COBRO, cobro.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_COBRO, cobro.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}
		}
		protected override void BuildREAChargeAccountingEntry(ChargeInfo cobro, LineaRegistro lr)
		{
			/*try
			{
				string descripcion = string.Empty;
				decimal importe_cobro = 0;
				bool is_debe = true;

				foreach (CobroREAInfo cf in cobro.CobroREAs)
				{
					ExpedientInfo expediente = _expedientes.GetItem(cf.OidExpediente);

					string cuentaREA = GetCuentaAyuda(ETipoAyuda.REA, expediente);

					if (cuentaREA == Resources.Defaults.NO_CONTABILIZAR) return;

					descripcion = "Cobro Ayuda REA del Exp. " + cf.CodigoExpediente;

					//Seleccionamos DEBE o HABER en funcion del importe del cobro
					if (cf.Cantidad < 0)
					{
						is_debe = true;
						importe_cobro = -cf.Cantidad;
					}
					else
					{
						is_debe = false;
						//ES NEGATIVO CUANDO VA AL HABER POR EXIGENCIA DE CONTAWIN
						importe_cobro = -cf.Cantidad;
					}

					//apunte en la cuenta del cliente
					BuildApunte(new ApunteA3
					{
						Tipo = ETipoApunteA3.General,
						Columna = (is_debe) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
						Fecha = cobro.Fecha,
						CuentaContable = cuentaREA,
						Importe = importe_cobro,
						//Libre2 = cobro.Codigo,
						Descripcion = descripcion,
					});
				}

				descripcion = String.Format(Resources.Messages.APUNTE_COBRO, "Ayuda REA");

				//Sumamos los gastos bancarios
				importe_cobro = cobro.Importe + cobro.GastosBancarios;

				//Seleccionamos DEBE o HABER en funcion del importe del cobro
				is_debe = importe_cobro > 0;

				//apunte en la cuenta del cobro
				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.General,
					Columna = (is_debe) ? EColumnaApunte.Haber : EColumnaApunte.Debe,
					Fecha = cobro.Fecha,
					CuentaContable = GetCuentaCobro(cobro),
					Importe = importe_cobro,
					//Libre2 = cobro.Codigo,
					Descripcion = descripcion,
				});

				if (cobro.GastosBancarios != 0)
				{
					descripcion = String.Format(Resources.Messages.APUNTE_GASTOS_BANCARIOS, cobro.Codigo + " (Ayuda REA)");

					//apunte en la cuenta de gastos bancarios
					BuildApunte(new ApunteA3
					{
						Tipo = ETipoApunteA3.General,
						Columna = EColumnaApunte.Debe,
						Fecha = cobro.Fecha,
						CuentaContable = GetCuentaGastosBancarios(cobro),
						Importe = cobro.GastosBancarios,
						//Libre2 = cobro.Codigo,
						Descripcion = descripcion,
					});
				}
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_COBRO, cobro.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_COBRO, cobro.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}*/
		}

		protected override void BuildREAGrantAccountingEntry(ExpedienteREAInfo ayuda, LineaRegistro lr)
		{
			/*try
			{
				string descripcion = string.Empty;
				decimal importe_cobro = 0;
				bool is_debe = true;
				descripcion = "Ayuda REA del Exp. " + ayuda.CodigoExpediente;

				ExpedientInfo expediente = _expedientes.GetItem(ayuda.OidExpediente);
				string cuentaREA = GetCuentaAyuda(ETipoAyuda.REA, expediente);

				//Seleccionamos DEBE o HABER en funcion del importe del cobro
				if (ayuda.AyudaCobrada < 0)
				{
					is_debe = true;
					importe_cobro = -ayuda.AyudaCobrada;
				}
				else
				{
					is_debe = false;
					//ES NEGATIVO CUANDO VA AL HABER POR EXIGENCIA DE CONTAWIN
					importe_cobro = -ayuda.AyudaCobrada;
				}

				//apunte en la cuenta del cliente
				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.General,
					Columna = (is_debe) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
					Fecha = ayuda.Fecha,
					CuentaContable = cuentaREA,
					Importe = importe_cobro,
					//Libre2 = ayuda.Codigo,
					Descripcion = descripcion,
				});

				descripcion = "Ayuda REA del Exp. " + ayuda.CodigoExpediente;

				//apunte en la cuenta del cobro
				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.General,
					Columna = (is_debe) ? EColumnaApunte.Haber : EColumnaApunte.Debe,
					Fecha = ayuda.Fecha,
					CuentaContable = ModulePrincipal.GetCuentaSubvencionesSetting(),
					Importe = importe_cobro,
					//Libre2 = ayuda.Codigo,
					Descripcion = descripcion,
				});
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_AYUDA_REA, ayuda.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_AYUDA_REA, ayuda.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}*/
		}

		protected override void BuildFomentoGrantAccountingEntry(LineaFomentoInfo ayuda, LineaRegistro lr)
		{
			/*try
			{
				string descripcion = string.Empty;
				decimal importe_cobro = 0;
				bool is_debe = true;
				descripcion = "Ayuda FOMENTO del Exp. " + ayuda.IDExpediente;

				ExpedientInfo expediente = _expedientes.GetItem(ayuda.OidExpediente);
				string cuentaREA = GetCuentaAyuda(ETipoAyuda.Fomento, expediente);

				//Seleccionamos DEBE o HABER en funcion del importe del cobro
				if (ayuda.Subvencion < 0)
				{
					is_debe = true;
					importe_cobro = -ayuda.Subvencion;
				}
				else
				{
					is_debe = false;
					//ES NEGATIVO CUANDO VA AL HABER POR EXIGENCIA DE CONTAWIN
					importe_cobro = -ayuda.Subvencion;
				}

				//apunte en la cuenta de la REA
				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.General,
					Columna = (is_debe) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
					Fecha = ayuda.FechaConocimiento,
					CuentaContable = cuentaREA,
					Importe = importe_cobro,
					//Libre2 = ayuda.Codigo,
					Descripcion = descripcion,
				});

				descripcion = "Ayuda FOMENTO del Exp. " + ayuda.IDExpediente;

				//apunte en la cuenta de la subvencion
				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.General,
					Columna = (is_debe) ? EColumnaApunte.Haber : EColumnaApunte.Debe,
					Fecha = ayuda.FechaConocimiento,
					CuentaContable = ModulePrincipal.GetCuentaSubvencionesSetting(),
					Importe = -importe_cobro,
					//Libre2 = ayuda.Codigo,
					Descripcion = descripcion,
				});
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_AYUDA_FOMENTO, ayuda.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_AYUDA_FOMENTO, ayuda.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}*/
		}

		protected override void BuildExpenseAccountingEntry(PaymentInfo pago, LineaRegistro lr)
		{
			/*try
			{
				if (!pago.Pagado) return;

				IAcreedorInfo titular = _acreedores.GetItem(pago.OidAgente, pago.ETipoAcreedor);
				//MovimientoBancoInfo mov = _movs.GetItemByOperacion(pago.Oid, EBankLineType.Pago);
				InputInvoiceInfo factura = null;

				if (titular.CuentaContable == string.Empty)
					throw new iQException("El deudor nº " + titular.Codigo + " (" + titular.Nombre + ") no tiene cuenta contable asociada", new object[2] { Store.EnumConvert.ToETipoEntidad(titular.ETipoAcreedor), titular.Oid });

				string apunte = string.Empty;
				string descripcion = string.Empty;
				int pos = 1;

				foreach (TransactionPaymentInfo pf in pago.PagoFacturas)
				{
					factura = _fact_proveedores.GetItem(pf.OidFactura);
					descripcion = "Pago Fra. " + factura.NFactura + " (" + factura.Acreedor + ")";

					//Sumamos los gastos bancarios solo al primer pagofactura 
					decimal importe = (pos++ == 1) ? pf.Cantidad + pago.GastosBancarios : pf.Cantidad;

					//apunte en la cuenta del acreedor
					BuildApunte(new ApunteA3
					{
						Tipo = ETipoApunteA3.General,
						Columna = EColumnaApunte.Debe,
						Fecha = pago.Fecha,
						CuentaContable = titular.CuentaContable,
						Importe = importe,
						//Libre2 = pago.Codigo,
						Descripcion = descripcion,
					});

					apunte = string.Empty;

					//apunte en la cuenta del pago
					BuildApunte(new ApunteA3
					{
						Tipo = ETipoApunteA3.General,
						Columna = EColumnaApunte.Haber,
						Fecha = pago.Fecha,
						CuentaContable = GetCuentaPago(pago),
						Importe = -pf.Cantidad,
						//Libre2 = pago.Codigo,
						Descripcion = descripcion,
					});
				}

				if (pago.GastosBancarios != 0)
				{
					apunte = string.Empty;

					descripcion = "Gtos. Banco. " + pago.Codigo + " (" + titular.Nombre + ")";

					//apunte en la cuenta de gastos bancarios
					BuildApunte(new ApunteA3
					{
						Tipo = ETipoApunteA3.General,
						Columna = EColumnaApunte.Haber,
						Fecha = pago.Fecha,
						CuentaContable = GetCuentaGastosBancarios(pago),
						Importe = -pago.GastosBancarios,
						//Libre2 = pago.Codigo,
						Descripcion = descripcion,
					});
				}
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_GASTO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_GASTO, pago.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}*/
		}

		protected override void BuildPayrollBatchAccountingEntry(PayrollBatchInfo nomina, LineaRegistro lr)
		{
			/*try
			{
				//if (!pago.Pagado) return;

				string apunte = string.Empty;
				string descripcion = string.Empty;
				string cuenta = string.Empty;

				//Calculo del importe del apunte 
				decimal importe = nomina.Neto + nomina.IRPF + nomina.SeguroPersonal;

				descripcion = nomina.Descripcion;
				cuenta = Invoice.ModulePrincipal.GetCuentaNominasSetting();

				//apunte en la cuenta de nóminas
				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.General,
					Columna = EColumnaApunte.Debe,
					Fecha = nomina.Fecha,
					CuentaContable = Invoice.ModulePrincipal.GetCuentaNominasSetting(),
					Importe = importe,
					//Libre2 = nomina.Codigo,
					Descripcion = descripcion,
				});

				//Calculo del importe del apunte 
				importe = nomina.SeguroEmpresa;

				descripcion = "Seg. Social a cargo de la empresa";
				cuenta = Invoice.ModulePrincipal.GetCuentaSegurosSocialesSetting();

				//apunte en la cuenta de la seguridad social
				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.General,
					Columna = EColumnaApunte.Debe,
					Fecha = nomina.Fecha,
					CuentaContable = Invoice.ModulePrincipal.GetCuentaSegurosSocialesSetting(),
					Importe = importe,
					//Libre2 = nomina.Codigo,
					Descripcion = descripcion,
				});

				TipoGastoInfo tipo = null;
				EmployeeInfo empleado = null;

				foreach (ExpenseInfo item in nomina.Gastos)
				{
					tipo = _tipos_gastos.GetItem(item.OidTipo);
					apunte = string.Empty;

					switch (item.ECategoriaGasto)
					{
						case ECategoriaGasto.SeguroSocial:
							{
								if (tipo.CuentaContable == string.Empty)
									throw new iQException("El tipo de gasto nº " + tipo.Codigo + " (" + tipo.Nombre + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.TipoGasto, tipo.Oid });

								descripcion = "SEGUROS SOCIALES " + nomina.Fecha.ToString("MMMM - yyyy").ToUpper();

								//Calculo del importe del apunte 
								importe = item.Total;

								//apunte en la cuenta de la Hacienda Pública
								BuildApunte(new ApunteA3
								{
									Tipo = ETipoApunteA3.General,
									Columna = EColumnaApunte.Haber,
									Fecha = nomina.Fecha,
									CuentaContable = tipo.CuentaContable,
									Importe = importe,
									//Libre2 = item.Codigo,
									Descripcion = descripcion,
								});
							}
							break;

						case ECategoriaGasto.Impuesto:
							{
								if (tipo.CuentaContable == string.Empty)
									throw new iQException("El tipo de gasto nº " + tipo.Codigo + " (" + tipo.Nombre + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.TipoGasto, tipo.Oid });

								descripcion = "HACIENDA PUBLICA ACREEDORA " + nomina.Fecha.ToString("MMMM - yyyy").ToUpper();

								//Calculo del importe del apunte 
								importe = item.Total;

								//apunte en la cuenta de la Hacienda Pública
								BuildApunte(new ApunteA3
								{
									Tipo = ETipoApunteA3.General,
									Columna = EColumnaApunte.Haber,
									Fecha = nomina.Fecha,
									CuentaContable = tipo.CuentaContable,
									Importe = -importe,
									//Libre2 = item.Codigo,
									Descripcion = descripcion,
								});
							}
							break;

						case ECategoriaGasto.Nomina:
							{
								empleado = _empleados.GetItem(item.OidEmpleado);
								descripcion = "NOMINA (" + empleado.NombreCompleto + ")";

								if (empleado.CuentaContable == string.Empty)
									throw new iQException("El empleado nº " + empleado.Codigo + " (" + empleado.NombreCompleto + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Empleado, empleado.Oid });

								//Calculo del importe del apunte 
								importe = item.Total;

								//apunte en la cuenta del empleado
								BuildApunte(new ApunteA3
								{
									Tipo = ETipoApunteA3.General,
									Columna = EColumnaApunte.Haber,
									Fecha = nomina.Fecha,
									CuentaContable = empleado.CuentaContable,
									Importe = -importe,
									//Libre2 = item.Codigo,
									Descripcion = descripcion,
								});
							}
							break;
					}
				}
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_NOMINA, nomina.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_NOMINA, nomina.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}*/
		}

        protected override void BuildBankTransferAccountingEntry(TraspasoInfo traspaso, LineaRegistro lr)
        {
            try
            {
                BankAccountInfo cuentaO = _bank_accounts.GetItem(traspaso.OidCuentaOrigen);

                if (cuentaO.CuentaContable == string.Empty)
					throw new iQException("La cuenta bancaria " + cuentaO.Entidad + " no tiene cuenta contable asociada", new object[2] { ETipoEntidad.CuentaBancaria, cuentaO.Oid });

                BankAccountInfo cuentaD = _bank_accounts.GetItem(traspaso.OidCuentaDestino);

                if (cuentaD.CuentaContable == string.Empty)
					throw new iQException("La cuenta bancaria " + cuentaD.Entidad + " no tiene cuenta contable asociada", new object[2] { ETipoEntidad.CuentaBancaria, cuentaD.Oid });

                //Apunte de la salida de la cuenta de origen
                BuildAccountingLine(new ApunteA3
                {
                    Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                    Fecha = traspaso.Fecha,
                    TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                    CuentaContable = cuentaO.CuentaContable,
                    TipoImporte = traspaso.Importe < 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                    NFactura = traspaso.Codigo,
                    Posicion = EPosicionApunte.Inicial,
                    Descripcion = "Traspaso " + cuentaO.Entidad + " a " + cuentaD.Entidad,
                    Total = traspaso.Importe < 0 ? -traspaso.Importe : traspaso.Importe
                });

                //Apunte de entrada a la cuenta destino
                BuildAccountingLine(new ApunteA3
                {
                    Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                    Fecha = traspaso.Fecha,
                    TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                    CuentaContable = cuentaD.CuentaContable,
                    TipoImporte = traspaso.GastosBancarios < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                    NFactura = traspaso.Codigo,
                    Posicion = traspaso.GastosBancarios != 0 ? EPosicionApunte.Medio : EPosicionApunte.Final,
                    Descripcion = "Traspaso " + cuentaO.Entidad + " a " + cuentaD.Entidad,
                    Total = traspaso.Importe < 0 ? -traspaso.Importe : traspaso.Importe
                });

                if (traspaso.GastosBancarios != 0)
                {
                    //Debe en la cuenta contable de gastos del banco de origen
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = traspaso.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = cuentaO.CuentaContableGastos,
                        TipoImporte = traspaso.GastosBancarios < 0 ? ETipoImporte.Haber : ETipoImporte.Debe,
                        NFactura = traspaso.Fecha.ToString("yyyyMMdd"),
                        Posicion = EPosicionApunte.Medio,
                        Descripcion = "Gastos Bancarios " + traspaso.Codigo,
                        Total = traspaso.GastosBancarios < 0 ? -traspaso.GastosBancarios : traspaso.GastosBancarios
                    });


                    //Haber en la cuenta contable del banco de origen
                    BuildAccountingLine(new ApunteA3
                    {
                        Tipo = ETipoApunteA3.AltaApuntesSinIVA,
                        Fecha = traspaso.Fecha,
                        TipoRegistro = ETipoRegistroApunte.ApunteSinImpuesto,
                        CuentaContable = cuentaO.CuentaContable,
                        TipoImporte = traspaso.GastosBancarios < 0 ? ETipoImporte.Debe : ETipoImporte.Haber,
                        NFactura = string.Empty,
                        Posicion = EPosicionApunte.Final,
                        Descripcion = "Gastos Bancarios " + traspaso.Codigo,
                        Total = traspaso.GastosBancarios < 0 ? -traspaso.GastosBancarios : traspaso.GastosBancarios
                    });
 
                }
            }
            catch (iQException ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_TRASPASO, traspaso.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
            }
            catch (Exception ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_TRASPASO, traspaso.Codigo, iQExceptionHandler.GetAllMessages(ex)));
            }
        }

		protected override void BuildTaxBookSoportadoAccountingEntry(InputInvoiceInfo factura)
		{
			/*List<ImpuestoResumen> impuestos = factura.GetImpuestos(true);
			IAcreedorInfo titular = _acreedores.GetItem(factura.OidAcreedor, factura.ETipoAcreedor);
			string apunte;

			foreach (ImpuestoResumen ir in impuestos)
			{
				apunte = string.Empty;

				ImpuestoInfo impuesto = _impuestos.GetItem(ir.OidImpuesto);

				if (impuesto.CuentaContableSoportado == string.Empty)
					throw new iQException("El impuesto '" + impuesto.Nombre + "' no tiene cuenta contable (soportado) asociada");

				//string tipo_iva = (impuestos.Count == 1) ? string.Empty : "C";

				//apunte en la cuenta del impuesto
				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.LibroImpuesto,
					Fecha = factura.Fecha,
					CuentaContable = titular.CuentaContable,
					CuentaContableContrapartida = impuesto.CuentaContableSoportado,
					Titular = factura.Acreedor,
					Vat = factura.VatNumber,
					NFactura = factura.NFactura,
					FechaFactura = factura.FechaRegistro,
					Importe = ir.Importe,
					BaseImponible = ir.BaseImponible,
					Total = ir.Total,
					Porcentaje = impuesto.Porcentaje,
					TipoImpuesto = ETipoImpuestoApunte.Soportado,
					Descripcion = impuesto.Nombre,
				});
			}*/
		}

		protected override void BuildTaxBookRepercutidoAccountingEntry(OutputInvoiceInfo factura)
		{
			/*List<ImpuestoResumen> impuestos = new List<ImpuestoResumen>();

			foreach (DictionaryEntry impuesto in factura.GetImpuestos())
				impuestos.Add((ImpuestoResumen)impuesto.Value);

			ClienteInfo titular = _clientes.GetItem(factura.OidCliente);
			string apunte = string.Empty;

			foreach (ImpuestoResumen ir in impuestos)
			{
				apunte = string.Empty;

				ImpuestoInfo impuesto = _impuestos.GetItem(ir.OidImpuesto);

				if (impuesto.CuentaContableRepercutido == string.Empty)
					throw new iQException("El impuesto '" + impuesto.Nombre + "' no tiene cuenta contable (repercutido) asociada");

				//string tipo_iva = (impuestos.Count == 1) ? string.Empty : "C";

				//apunte en la cuenta del impuesto
				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.LibroImpuesto,
					Fecha = factura.Fecha,
					CuentaContable = titular.CuentaContable,
					CuentaContableContrapartida = impuesto.CuentaContableRepercutido,
					Titular = factura.Cliente,
					Vat = factura.VatNumber,
					NFactura = factura.NFactura,
					FechaFactura = factura.Fecha,
					Importe = ir.Importe,
					BaseImponible = ir.BaseImponible,
					Total = ir.Total,
					Porcentaje = impuesto.Porcentaje,
					TipoImpuesto = ETipoImpuestoApunte.Repercutido,
					Descripcion = impuesto.Nombre,
				});

				_activos_file.WriteLine(apunte);
			}*/
		}

		protected override void BuildFinancialCashBookPaymentAccountingEntry(PaymentInfo pago)
		{
			/*IAcreedorInfo titular = _acreedores.GetItem(pago.OidAgente, pago.ETipoAcreedor);
			InputInvoiceInfo factura = null;

			if (titular.CuentaContable == string.Empty)
				throw new iQException("El deudor nº " + titular.Codigo + " (" + titular.Nombre + ") no tiene cuenta contable asociada");

			string apunte = string.Empty;
			string descripcion = string.Empty;
			int pos = 1;

			foreach (TransactionPaymentInfo pf in pago.PagoFacturas)
			{
				apunte = string.Empty;

				factura = _fact_proveedores.GetItem(pf.OidFactura);
				descripcion = "Pago Fra. " + factura.NFactura + " (" + factura.Acreedor + ")";

				//Sumamos los gastos bancarios solo al primer pagofactura 
				decimal importe = (pos++ == 1) ? pf.Cantidad + pago.GastosBancarios : pf.Cantidad;

				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.Efectos,
					Columna = EColumnaApunte.Debe,
					Fecha = pago.Vencimiento,
					CuentaContable = titular.CuentaContable,
					CuentaContableContrapartida = GetCuentaPago(pago),
					Importe = importe,
					Titular = titular.Nombre,
                    Vat = titular.Codigo,
                    NFactura = factura != null ? factura.NFactura : string.Empty,
                    FechaFactura = factura != null ? factura.Fecha : DateTime.MinValue,
					//Libre1 = pago.IDPagoLabel,
					Descripcion = descripcion,
				});
			}

			if (pago.GastosBancarios != 0)
			{
				apunte = string.Empty;

				descripcion = "Gtos. Banco. " + pago.Codigo + " (" + titular.Nombre + ")";

				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.Efectos,
					Columna = EColumnaApunte.Debe,
					Fecha = pago.Vencimiento,
					CuentaContable = GetCuentaGastosBancarios(pago),
					CuentaContableContrapartida = GetCuentaPago(pago),
					Importe = pago.GastosBancarios,
					Titular = titular.Nombre,
                    Vat = titular.Codigo,
                    NFactura = factura != null ? factura.NFactura : string.Empty,
                    FechaFactura = factura != null ? factura.Fecha : DateTime.MinValue,
					//Libre1 = pago.IDPagoLabel,
					Descripcion = descripcion,
				});
			}*/
		}

		protected override void BuildFinalcialCashBookChargeAccountingEntry(ChargeInfo cobro)
		{
			/*ClienteInfo titular = _clientes.GetItem(cobro.OidCliente);
			FacturaInfo factura;

			if (titular.CuentaContable == string.Empty)
				throw new iQException("El cliente nº " + titular.NumeroClienteLabel + " (" + titular.Nombre + ") no tiene cuenta contable asociada");

			string apunte = string.Empty;
			string descripcion = string.Empty;
			int pos = 1;

			foreach (CobroFacturaInfo cf in cobro.CobroFacturas)
			{
				apunte = string.Empty;

				factura = _fact_clientes.GetItem(cf.OidFactura);
				descripcion = "Cobro Fra. " + factura.NFactura + " (" + factura.Cliente + ")";

				//Sumamos los gastos bancarios solo al primer pagofactura 
				decimal importe = (pos++ == 1) ? cf.Cantidad + cobro.GastosBancarios : cf.Cantidad;

				BuildApunte(new ApunteA3
				{
					Tipo = ETipoApunteA3.Efectos,
					Columna = EColumnaApunte.Haber,
					Fecha = cobro.Vencimiento,
					CuentaContable = titular.CuentaContable,
					CuentaContableContrapartida = GetCuentaCobro(cobro),
					Importe = importe,
					Titular = factura.Cliente,
					Vat = factura.VatNumber,
					NFactura = factura.NFactura,
					FechaFactura = factura.Fecha,
					//Libre1 = cobro.IDCobroLabel,
					Descripcion = descripcion,
				});
			}*/
		}

		protected override void BuildAccountingLine(IApunteContable iApunteC)
		{
			string apunte = string.Empty;

            ApunteA3 apunteC = (ApunteA3)iApunteC;

			switch (apunteC.Tipo)
			{
				case ETipoApunteA3.CabeceraFactura:
					{
						// FORMATO: 
						// Ejemplo de una factura de compras (Factura recibida)
						// 3 00001 20020107 1 400000020000 CUENTA DE PROVEEDOR	2 FAC Nº5 I DESCRIPCION COMPRA		+0000001160.00 EN
						// 3 00001 20020107 9 600000000000 CUENTA DE COMPRAS	C FAC Nº5 U DESCRIPCION BASE	01	+0000001000.00 16.00 +0000000160.00 01S EN

						apunte += GetApunteCode(apunteC.Tipo);								/*TIPO DE FICHERO*/
                        apunte += _config.Empresa.PadLeft(5, '0');					        /*CODIGO DE EMPRESA*/
						apunte += apunteC.Fecha.ToString("yyyyMMdd");						/*FECHA*/
						apunte += GetRegistryTypeCode(apunteC.TipoRegistro);				/*TIPO REGISTRO*/
                        apunte += apunteC.CuentaContable.PadRight(12, '0');			        /*CUENTA*/
                        apunte += apunteC.Titular.PadRight(30).Substring(0,30);             /*DESCRIPCION CUENTA*/
                        apunte += Convert.ToString((long)apunteC.TipoFactura);              /*TIPO DE FACTURA*/
                        apunte += apunteC.NumeroDocumento.PadRight(10).Substring(0,10);		/*Nº DE FACTURA O DOCUMENTO*/
						apunte += GetPositionCode(apunteC.Posicion);						/*POSICION REGISTRO*/
                        apunte += apunteC.Descripcion.PadRight(30).Substring(0,30);			/*DESCRIPCION CUENTA*/
                        
						apunte += apunteC.Total.ToString("0000000000.00").PadLeft(14, '+');			/*TOTAL FACTURA*/
                        apunte += string.Empty.PadLeft(62);
                        apunte += apunteC.NIFTitular.PadRight(14).Substring(0,14);           /*NIF DEL TITULAR*/
                        apunte += apunteC.NombreTitular.PadRight(40).Substring(0,40);        /*NOMBRE DEL TITULAR*/
                        apunte += apunteC.CodPostalTitular.PadRight(5).Substring(0, 5);      /*CODIGO POSTAL DEL TITULAR*/
                        apunte += string.Empty.PadLeft(2);
                        apunte += string.Empty.PadLeft(8);
                        apunte += string.Empty.PadLeft(8);

						apunte += "EN";

						_enlace_file.WriteLine(apunte);
					}
					break;

                case ETipoApunteA3.FacturasyRectificativas:
                    {
                        // FORMATO: 
                        // Ejemplo de una factura de compras (Factura recibida)
                        // 3 00001 20020107 1 400000020000 CUENTA DE PROVEEDOR	2 FAC Nº5 I DESCRIPCION COMPRA		+0000001160.00 EN
                        // 3 00001 20020107 9 600000000000 CUENTA DE COMPRAS	C FAC Nº5 U DESCRIPCION BASE	01	+0000001000.00 16.00 +0000000160.00 01S EN

                        apunte += GetApunteCode(apunteC.Tipo);								/*TIPO DE FICHERO*/
                        apunte += _config.Empresa.PadLeft(5, '0');					        /*CODIGO DE EMPRESA*/
                        apunte += apunteC.Fecha.ToString("yyyyMMdd");						/*FECHA*/
                        apunte += GetRegistryTypeCode(apunteC.TipoRegistro);				/*TIPO REGISTRO*/
                        apunte += apunteC.CuentaContable.PadRight(12, '0');			        /*CUENTA*/
                        apunte += apunteC.Titular.PadRight(30).Substring(0,30);             /*DESCRIPCION CUENTA*/
                        apunte += GetTipoImporte(apunteC.TipoImporte);                                  /*TIPO DE FACTURA*/
                        apunte += apunteC.NumeroDocumento.PadRight(10).Substring(0,10);		/*Nº DE FACTURA O DOCUMENTO*/
                        apunte += GetPositionCode(apunteC.Posicion);						/*POSICION REGISTRO*/
                        apunte += apunteC.Descripcion.PadRight(30).Substring(0, 30);			/*DESCRIPCION DEL APUNTE*/

                        //Si no existe subtipo de factura asociado al concepto es porque no tiene impuesto asociado, 
                        //el subtipo de factura que hay que indicar es el 8.
                        apunte += apunteC.SubtipoFactura != string.Empty ? apunteC.SubtipoFactura.PadLeft(2, ' ') : "08";                                   /*SUBTIPO DE FACTURA*/

                        apunte += apunteC.Total.ToString("0000000000.00").PadLeft(14, '+');;	    /*TOTAL FACTURA*/
                        apunte += apunteC.P_IVA.ToString("00.00");                                                     /*PORCENTAJE DE IVA*/
                        apunte += apunteC.IVA.ToString("0000000000.00").PadLeft(14, '+');            /*CUOTA DE IVA*/
                        apunte += apunteC.PRecargo.ToString("00.00");                                                  /*PORCENTAJE DE RECARGO*/
                        apunte += apunteC.Recargo.ToString("0000000000.00").PadLeft(14, '+');    /*CUOTA DE RECARGO*/
                        apunte += apunteC.PRetencion.ToString("00.00");                                                /*PORCENTAJE DE RETENCIÓN*/
                        apunte += apunteC.Retencion.ToString("0000000000.00").PadLeft(14, '+');/*CUOTA DE RETENCIÓN*/
                        apunte += (apunteC.P_IVA > 0) ? "01" : string.Empty.PadLeft(2);                                                          /*IMPRESO*/
                        
                        //Indica si la operación está sujeta a IVA o no, en principio los conceptos que no tienen impuesto deberían 
                        //tener una N pero se va a poner S a todos y 8 el subtipo de factura que indica que es exento.
                        apunte += "S";// GetSujetoIVA(apunteC);                                    /*OPERACIÓN SUJETA A IVA*/
                        apunte += string.Empty.PadLeft(1);                                  /*AFECTA AL MODELO 415*/
                        apunte += string.Empty.PadLeft(75);                                 /*RESERVA*/
                        apunte += string.Empty.PadLeft(1);                                  /*TIENE REGISTRO ANALÍTICO*/

                        apunte += "EN";

                        _enlace_file.WriteLine(apunte);
                    }
                    break;

                case ETipoApunteA3.AltaDeVencimientos:
                    {
                        apunte += GetApunteCode(apunteC.Tipo);								/*TIPO DE FICHERO*/
                        apunte += _config.Empresa.PadLeft(5, '0');					        /*CODIGO DE EMPRESA*/
                        apunte += apunteC.FechaVencimiento.ToString("yyyyMMdd");			/*FECHA*/
                        apunte += GetRegistryTypeCode(apunteC.TipoRegistro);				/*TIPO REGISTRO*/
                        apunte += apunteC.CuentaContable.PadRight(12, '0');	                /*CUENTA DE CLIENTE O PROVEEDOR*/
                        apunte += apunteC.Descripcion.PadRight(30).Substring(0, 30);        /*DESCRIPCION CUENTA*/
                        apunte += GetTipoVencimiento(apunteC.TipoVencimiento);              /*TIPO DE VENCIMIENTO*/
                        apunte += apunteC.NFactura.PadRight(10).Substring(0, 10);	        /*Nº DE FACTURA O DOCUMENTO*/
                        apunte += string.Empty.PadLeft(1);                                  /*INDICADOR DE AMPLIACIÓN " " */
                        apunte += apunteC.Descripcion.PadRight(30).Substring(0, 30);		/*DESCRIPCION DEL APUNTE*/
                        apunte += apunteC.Total.ToString("0000000000.00").PadLeft(14, '+'); /*IMPORTE DEL VENCIMIENTO*/
                        apunte += apunteC.FechaFactura.ToString("yyyyMMdd");				/*FECHA FACTURA*/
                        apunte += apunteC.CuentaContableContrapartida.PadRight(12, '0');	                /*CUENTA DE VENCIMIENTO*/
                        apunte += string.Empty.PadLeft(2);                                  /*FORMA DE PAGO*/
                        apunte += apunteC.NumeroDocumento.PadRight(2).Substring(0, 2);
                        apunte += string.Empty.PadLeft(115);                                /*RESERVA*/

                        apunte += "EN";

                        _enlace_file.WriteLine(apunte);
                    }
                    break;

                case ETipoApunteA3.AmpliacionAltaDeVencimientos:
                    {
                        apunte += GetApunteCode(apunteC.Tipo);								/*TIPO DE FICHERO*/
                        apunte += _config.Empresa.PadLeft(5, '0');					        /*CODIGO DE EMPRESA*/
                        apunte += apunteC.FechaVencimiento.ToString("yyyyMMdd");						/*FECHA*/
                        apunte += GetRegistryTypeCode(apunteC.TipoRegistro);				/*TIPO REGISTRO*/
                        apunte += string.Empty.PadLeft(43);                                 /*RESERVA*/
                        apunte += apunteC.TipoCobroPago.PadLeft(2);    	                            /*FORMA DE COBRO O PAGO*/
                        apunte += apunteC.Fecha.ToString("yyyyMMdd");						/*FECHA DEL COBRO O PAGO*/
                        apunte += "A";                                                      /*INDICADOR DE AMPLIACIÓN "A" */
                        apunte += apunteC.Estado.PadLeft(1, ' ');                           /*ESTADO DEL COBRO O PAGO*/
                        apunte += string.Empty.PadLeft(15);                                 /*NÚMERO DE EFECTO*/
                        apunte += string.Empty.PadLeft(8);                                  /*RESERVA*/
                        apunte += apunteC.CuentaBancaria;                                   /*CUENTA BANCARIA*/
                        apunte += apunteC.Entidad.PadLeft(20).Substring(0, 20);             /*NOMBRE DE LA OFICINA*/
                        apunte += string.Empty.PadLeft(25);                                 /*DOMICILIO DE LA OFICINA*/
                        apunte += string.Empty.PadLeft(2);                                  /*CÓDIGO DE NOTA*/
                        apunte += string.Empty.PadLeft(40);                                 /*TÍTULO DE LA NOTA*/
                        apunte += string.Empty.PadLeft(52);                                 /*RESERVA*/

                        apunte += "EN";

                        _enlace_file.WriteLine(apunte);
                    }
                    break;

                case ETipoApunteA3.AltaApuntesSinIVA:
                    {
                        apunte += GetApunteCode(apunteC.Tipo);								/*TIPO DE FICHERO*/
                        apunte += _config.Empresa.PadLeft(5, '0');					        /*CODIGO DE EMPRESA*/
                        apunte += apunteC.Fecha.ToString("yyyyMMdd");			/*FECHA*/
                        apunte += GetRegistryTypeCode(apunteC.TipoRegistro);				/*TIPO REGISTRO*/
                        apunte += apunteC.CuentaContable.PadRight(12, '0');	                /*CUENTA DE CLIENTE O PROVEEDOR*/
                        apunte += string.Empty.PadLeft(30);        /*DESCRIPCION CUENTA*/
                        apunte += GetTipoImporte(apunteC.TipoImporte);                    /*TIPO DE VENCIMIENTO*/
                        apunte += apunteC.NFactura.PadRight(10).Substring(0, 10);	        /*REFERENCIA DEL DOCUMENTO*/
                        apunte += GetPositionCode(apunteC.Posicion);						/*POSICION REGISTRO*/
                        apunte += apunteC.Descripcion.PadRight(30).Substring(0, 30);		/*DESCRIPCION DEL APUNTE*/
                        apunte += apunteC.Total.ToString("0000000000.00").PadLeft(14, '+'); /*IMPORTE DEL VENCIMIENTO*/
                        apunte += string.Empty.PadLeft(138);                                /*RESERVA*/
                        apunte += string.Empty.PadLeft(1);                                  /*TIENE REGISTRO ANALÍTICO*/

                        apunte += "EN";

                        _enlace_file.WriteLine(apunte);
                    }
                    break;

                //case ETipoApunteA3.Efectos:
                //    {
                //        /// FORMATO: 03/02/2007;4300000041;5720000000;1;Fra.3;12083;Alberto Albertos Cortéz;B112125465;-1;3;Opc1;Opc2;Opc3;03/01/2007

                //        //apunte += GetColumnaCode(apunteC.Columna);						/*TIPO: DEBE=D, HABER=H*/

                //        //apunte += apunteC.Fecha;										/*FECHA VTO*/
                //        //apunte += ";" + apunteC.CuentaContable;							/*CUENTA CONTRAPARTIDA*/
                //        //apunte += ";" + apunteC.CuentaContableContrapartida;			/*CUENTA PAGO/COBRO*/
                //        //apunte += ";" + GetColumnaCode(apunteC.Columna);				/*TIPO: COMPRAS=01, VENTAS=02*/
                //        //apunte += ";" + apunteC.Descripcion;							/*OBSERVACIONES*/
                //        //apunte += ";" + apunteC.Importe;								/*IMPORTE*/
                //        //apunte += ";" + apunteC.Titular;								/*TITULAR*/
                //        //apunte += ";" + apunteC.Vat;									/*NIF*/
                //        //apunte += ";" + apunteC.Asiento;								/*ASIENTO: SIN ENLACE AL DIARIO=-1*/
                //        //apunte += ";" + apunteC.NFactura;								/*Nº FACTURA*/
                //        //apunte += ";" + apunteC.Libre1;									/*OPC. 1*/
                //        //apunte += ";" + "";												/*OPC. 2*/
                //        //apunte += ";" + "";												/*OPC. 3*/
                //        //apunte += ";" + apunteC.FechaFactura.ToShortDateString();		/*FECHA FACTURA*/

                //        _datoseos_file.WriteLine(apunte);
                //    }
                //    break;

                //case ETipoApunteA3.LibroImpuesto:
                //    {
                //        /// FORMATO: 4300000016;Juan Alberto Acosta Guill;B55223655;1;19375;;;-2;4770000001;S;01/01/2007;;16;0;22475;3100;0;16;01/01/2007;0;0;0;C

                //        //apunte += apunteC.CuentaContable;								/*CUENTA CLIENTE/DEUDOR*/
                //        //apunte += ";" + apunteC.Titular;								/*CLIENTE/DEUDOR*/
                //        //apunte += ";" + apunteC.Vat;									/*NIF*/
                //        //apunte += ";" + apunteC.NFactura;								/*Nº FACTURA*/
                //        //apunte += ";" + apunteC.BaseImponible.ToString("N2");			/*BASE IMPONIBLE*/
                //        //apunte += ";";													/*SIN USO*/
                //        //apunte += ";";													/*SIN USO*/
                //        //apunte += ";" + apunteC.Asiento;	 							/*ASIENTO*/
                //        //apunte += ";" + apunteC.CuentaContableContrapartida;			/*CUENTA IVA*/
                //        //apunte += ";" + GetImpuestoCode(apunteC.TipoImpuesto);			/*TIPO: SOPORTADO=S, REPERCUTIDO=R*/
                //        //apunte += ";" + apunteC.Fecha.ToShortDateString();				/*FECHA*/
                //        //apunte += ";";													/*SIN USO*/
                //        //apunte += ";" + apunteC.Porcentaje.ToString("N2");				/*%IVA*/
                //        //apunte += ";" + "";												/*%RE*/
                //        //apunte += ";" + apunteC.Total.ToString("N2");					/*TOTAL*/
                //        //apunte += ";" + apunteC.Importe.ToString("N2");					/*CUOTA IVA*/
                //        //apunte += ";" + "";												/*CUOTA RE*/
                //        //apunte += ";" + apunteC.Descripcion;							/*TIPO IVA*/
                //        //apunte += ";" + apunteC.FechaFactura.ToString("d");				/*FECHA CONTABLE*/
                //        //apunte += ";" + apunteC.Diario;									/*DIARIO*/
                //        //apunte += ";" + "0";											/*CANAL*/
                //        //apunte += ";" + "";												/*FORMA DE PAGO*/
                //        //apunte += ";" + "";												/*TIPO 340: PREDETERMINADO=VACIO, MAS DE UN TIPO DE IVA:C, ABONO=D*/
                //        //apunte += ";" + "";												/*FECHA OPERACION*/
                //        //apunte += ";" + "";												/*NO 347*/
                //        //apunte += ";" + "";												/*DATO 340_1*/
                //        //apunte += ";" + "";												/*DATO 340_2*/
                //        //apunte += ";" + "";												/*DATO 340_3*/
                //        //apunte += ";" + "";												/*DATO 340_4: Factura a Rectificar*/

                //        _activos_file.WriteLine(apunte);
                //    }
                //    break;
			}
		}

		protected string GetApunteCode(ETipoApunteA3 value)
		{
			switch (value)
			{
				case ETipoApunteA3.AltaApuntesSinIVA:
                case ETipoApunteA3.CabeceraFactura:
                case ETipoApunteA3.CabeceraRectificativaAbono:
                case ETipoApunteA3.FacturasyRectificativas:
                case ETipoApunteA3.AltaDeVencimientos:
                case ETipoApunteA3.AmpliacionAltaDeVencimientos:
                    return "3";
			}

			return string.Empty;
		}
		protected override string GetColumnCode(EColumnaApunte value) { return (value == EColumnaApunte.Debe) ? "D" : "H"; }
		protected override string GetTaxCode(ETipoImpuestoApunte value) { return (value == ETipoImpuestoApunte.Soportado) ? "S" : "R"; }
		protected override string GetPositionCode(EPosicionApunte value)
		{
			switch (value)
			{
				case EPosicionApunte.Inicial: return "I"; 
				case EPosicionApunte.Medio: return "M"; 
				case EPosicionApunte.Final: return "U";
			}

			return string.Empty;
		}
		protected override string GetRegistryTypeCode(ETipoRegistroApunte value)
		{
			switch (value)
			{
				case ETipoRegistroApunte.ApunteSinImpuesto: return "0";			// 0 Alta de Apuntes sin IVA
				case ETipoRegistroApunte.ApunteConImpuesto: return "1";			// 1 Alta Cabecera de apuntes con IVA (Formato para Facturas) 
				case ETipoRegistroApunte.ApunteAbonoConImpuesto: return "2";	// 2 Alta Cabecera de apuntes con IVA (Formatos para Rectificativas/Abonos)
				case ETipoRegistroApunte.DetalleImpuesto: return "9";			// 9 Detalle de apuntes con IVA (Facturas y Rectificativas)
				case ETipoRegistroApunte.AltaVencimiento: return "V";			// V Alta de Vencimientos
				case ETipoRegistroApunte.AltaVencimientoAmpliado: return "V";	// V (A posición 69) Alta de Vencimientos. Registro de Ampliación.
				case ETipoRegistroApunte.BajaVencimiento: return "B";			// B Baja de Vencimientos
				case ETipoRegistroApunte.AltaEdicionCuenta: return "C";			// C Alta y modificación de cuentas y/o clientes o proveedores
				case ETipoRegistroApunte.CuentaCorriente: return "C";			// C (B posición 73) Alta de CCC de clientes o proveedores.
				case ETipoRegistroApunte.AltaDistribucion: return "A";			// A Alta tabla de niveles de la distribución
			}

			return string.Empty;
		}

        protected string GetSujetoIVA(ApunteA3 apunte)
        {
            if (apunte.P_IVA == 0 && apunte.PRecargo == 0 && apunte.PRetencion == 0)
                return "N";
            else
                return "S";
        }

        protected string GetTipoImporte(ETipoImporte value)
        {
            switch (value)
            {
                case ETipoImporte.Cargo: return "C";
                case ETipoImporte.Abono: return "A";
                case ETipoImporte.Debe: return "D";
                case ETipoImporte.Haber: return "H";
            }

            return string.Empty;
        }

        protected string GetTipoVencimiento(ETipoVencimiento value)
        {
            switch (value)
            {
                case ETipoVencimiento.Cobro: return "C";
                case ETipoVencimiento.Pago: return "P";
            }

            return string.Empty;        
        }

        protected string GetFormaPagoCobro(EMedioPago value)
        {
            switch (value)
            {
                case EMedioPago.Domiciliacion:
                case EMedioPago.Ingreso:
                    return "DO";
                case EMedioPago.Transferencia:
                    return "TR";
                case EMedioPago.Cheque:
                    return "CH";
                case EMedioPago.Pagare:
                    return "PA";
                case EMedioPago.Giro:
                    return "GI";
                case EMedioPago.Efectivo:
                    return "ME";
                //case EMedioPago.LetraAceptada:
                //    return "LA";
                //case EMedioPago.LetraNoAceptada:
                //    return "LE";
            }

            return string.Empty;
        }

        protected string GetEstadoCobro(ChargeInfo cobro)
        {
            switch (cobro.EEstadoCobro)
            {
                case EEstado.Pendiente:
                    return "P";
                case EEstado.Devuelto:
                    return "D";
                case EEstado.Charged:
                    return "C";
            }

            return string.Empty;
        }
        protected string GetEstadoPago(PaymentInfo pago)
        {
            switch (pago.EEstadoPago)
            {
                case EEstado.Pendiente:
                    return "P";
                case EEstado.Devuelto:
                    return "D";
                case EEstado.Pagado:
                    return "G";
            }

            return string.Empty;
        }

        #endregion
    }
}