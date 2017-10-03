using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    public class ContabilidadExporterBase: IContabilidadExporter
    {
        #region Attributes & Properties

		protected ContabilidadConfig _config;

        protected InputInvoiceList _input_invoices = null;
		protected OutputInvoiceList _invoices = null;
		protected ClienteList _clients;
		protected ProviderBaseList _providers = null;
		protected PaymentList _payments = null;

		protected AyudaList _ayudas = null;
		protected ImpuestoList _taxes = null;
		protected FamiliaList _families = null;
        protected BankAccountList _bank_accounts = null;
		protected TipoGastoList _expense_types = null;
        protected EmployeeList _employees = null;
		protected ExpedienteList _expedients = null;
        protected PayrollList _payrolls = null;

		protected Registro _registry;

		protected long _accounting_entry;

		protected Library.Invoice.QueryConditions _invoice_conditions = new Library.Invoice.QueryConditions();
		protected Library.Store.QueryConditions _store_conditions = new Library.Store.QueryConditions();

		public Registro Registro { get { return _registry; } }

        #endregion

        #region Factory Methods

		protected ContabilidadExporterBase() {}

		public void SetConditions(Library.Invoice.QueryConditions conditions)
		{
			_invoice_conditions = conditions;
			_invoice_conditions.Orders = new OrderList();

			_store_conditions.Serie = conditions.Serie;
			_store_conditions.FechaIni = conditions.FechaIni;
			_store_conditions.FechaFin = conditions.FechaFin;
			_store_conditions.Familia = conditions.Familia;
			_store_conditions.Producto = conditions.Producto;
			_store_conditions.TipoAcreedor = new ETipoAcreedor [1] { conditions.TipoAcreedor };
			_store_conditions.MedioPago = conditions.MedioPago;
			_store_conditions.Estado = conditions.Estado;
			_store_conditions.TipoPago = conditions.TipoPago;
            _store_conditions.TipoAyudasContabilidad = conditions.TipoAyudas;
            _store_conditions.MedioPagoList = conditions.MedioPagoList;

			_store_conditions.Orders = new OrderList();
		}
		public string GetConditions()
		{
			string filtro = string.Empty;

			if (_invoice_conditions.FechaIni != DateTime.MinValue) filtro += "Fecha Inicial: " + _invoice_conditions.FechaIni.ToShortDateString() + "; ";
			if (_invoice_conditions.FechaFin != DateTime.MinValue) filtro += "Fecha Final: " + _invoice_conditions.FechaFin.ToShortDateString() + "; "; 
			if (_invoice_conditions.Familia!= null) filtro += "Familia: " + _invoice_conditions.Familia.Nombre + "; ";
			if (_invoice_conditions.Producto!= null) filtro += "Producto: " + _invoice_conditions.Producto.Nombre + "; ";
			if (_invoice_conditions.Serie != null) filtro += "Serie: " + _invoice_conditions.Serie.Nombre + "; ";
			filtro += "Tipo de Acreedor: " + Common.EnumText<ETipoAcreedor>.GetLabel(_invoice_conditions.TipoAcreedor) + "; " ;
			filtro += "Medio de Pago: " + Common.EnumText<EMedioPago>.GetLabel(_invoice_conditions.MedioPagoList) + "; ";
			filtro += "Estado: " + Common.EnumText<EEstado>.GetLabel(_invoice_conditions.Estado) + "; " ;
            filtro += "Ayudas: " + Invoice.EnumText<ETipoAyudaContabilidad>.GetLabel(_invoice_conditions.TipoAyudas) + ";";

			return filtro;
		}

		public virtual void Init(ContabilidadConfig config)
        {
			_config = config;
			SetConditions(config.Conditions);

			_accounting_entry = Convert.ToInt64(_config.AsientoInicial);

            if (!_config.RutaSalida.EndsWith("\\")) _config.RutaSalida += "\\";

            if (!Directory.Exists(_config.RutaSalida))
				Directory.CreateDirectory(_config.RutaSalida);

			_ayudas = AyudaList.GetList(false);
			_taxes = ImpuestoList.GetList(false);
            _families = FamiliaList.GetList(false, true);
            _bank_accounts = BankAccountList.GetList(false);
			_expense_types = TipoGastoList.GetList(false);

			_registry = Registro.New(ETipoRegistro.Contabilidad);
			_registry.Nombre = Resources.Labels.REGISTRO_CONTABILIDAD;
            _registry.ETipoExportacion = config.TipoExportacion;
			_registry.Observaciones = GetConditions();
        }

		public virtual void SaveFiles() {}

		public virtual void Close()
        {
			if (_registry != null)
			{
				_registry.Save();
				_registry.CloseSession();
			}

			Cache.Instance.Remove(typeof(FamiliaList));
			Cache.Instance.Remove(typeof(ProductList));
        }

        #endregion

        #region Business Methods
		
		protected string AddEmptyValue(long number)
		{
			string values = string.Empty;
			for (int i = 1; i <= number; i++) values += AddValue(string.Empty);
			return values;
		}

        public virtual void ExportInputInvoices()
        {
			InputInvoices invoices = null;

			try
			{	
				_store_conditions.Orders.Clear();
                _store_conditions.Orders.NewOrder("Acreedor", System.ComponentModel.ListSortDirection.Ascending, typeof(InputInvoice));
                _store_conditions.Orders.NewOrder("Fecha", System.ComponentModel.ListSortDirection.Ascending, typeof(InputInvoice));

                _input_invoices = InputInvoiceList.GetList(_store_conditions, true);
				_providers = (_providers == null) ? ProviderBaseList.GetList(false) : _providers;

				foreach (InputInvoiceInfo item in _input_invoices)
				{
					LineaRegistro lr = _registry.LineaRegistros.NewItem(_registry, item);

					BuildInputInvoiceAccountingEntry(item, lr);
					BuildTaxBookSoportadoAccountingEntry(item);

					_accounting_entry++;
				}

				//Cambiamos el estado de las facturas contabilizadas
                invoices = InputInvoices.GetList(_store_conditions, false);

                foreach (InputInvoice item in invoices)
                {
                    item.LoadChilds(typeof(InputInvoiceLine), false);
                }

				foreach (InputInvoice item in invoices)
                {
                    if (item.EEstado == EEstado.Anulado) continue;

                    if (item.EEstado != EEstado.Exportado)
                        item.EEstado = EEstado.Exportado;
                }

                invoices.Save();
                
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
				if (invoices != null) invoices.CloseSession();
			}
        }

		public virtual void ExportPayments()
        {
			Payments payments2 = null;

			try
			{
				_store_conditions.Orders.Clear();
                _store_conditions.Orders.NewOrder("Vencimiento", System.ComponentModel.ListSortDirection.Ascending, typeof(Payment));

				PaymentList payments = PaymentList.GetOrderedByFechaList(_store_conditions, true);

				//if (pagos.Count == 0) return; // throw new iQException(Library.Resources.Messages.NO_RESULTS);

				//Hacen falta todas porque un pago puede estar asociado a una factura que no este en las condiciones del filtro
                _input_invoices = InputInvoiceList.GetList(false);
				_providers = (_providers == null) ? ProviderBaseList.GetList(false) : _providers;
                _employees = (_employees == null) ? EmployeeList.GetList(false) : _employees;
				_expense_types = (_expense_types == null) ? TipoGastoList.GetList(false) : _expense_types;
				_payrolls = (_payrolls == null) ? PayrollList.GetList(false) : _payrolls;

				foreach (PaymentInfo item in payments)
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

                        case ETipoPago.ExtractoTarjeta:
                            {
                                BuildCreditCardStatementPaymentAccountingEntry(item, lr);
                            }
                            break;
					}

					_accounting_entry++;
				}

				//Cambiamos el estado de los pagos contabilizados
				payments2 = Payments.GetList(_store_conditions, false);

                foreach (Payment item in payments2)
				{
					if (item.EEstado == EEstado.Anulado) continue;

					if (item.EEstado != EEstado.Exportado)
                        /*if (item.EEstadoPago == EEstado.Pagado)*/ item.EEstado = EEstado.Exportado;
				}

				payments2.Save();
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
				if (payments2 != null) payments2.CloseSession();
			}
        }

		public virtual void ExportOutputInvoices()
        {
			OutputInvoices invoices = null;

			try
			{
				_invoice_conditions.Orders.Clear();
				_invoice_conditions.Orders.NewOrder("Fecha", System.ComponentModel.ListSortDirection.Ascending, typeof(OutputInvoice));

				_invoices = OutputInvoiceList.GetList(_invoice_conditions, true);
				_clients = (_clients == null) ? ClienteList.GetList(false) : _clients;

				foreach (OutputInvoiceInfo item in _invoices)
				{
					if (item.EEstado == EEstado.Anulado) continue;

					LineaRegistro lr = _registry.LineaRegistros.NewItem(_registry, item);

					BuildOutputInvoiceAccountingEntry(item, lr);
					BuildTaxBookRepercutidoAccountingEntry(item);

					_accounting_entry++;
				}

				//Cambiamos el estado de las facturas contabilizadas
				invoices = OutputInvoices.GetList(_invoice_conditions, false);

				foreach (OutputInvoice item in invoices)
				{
					if (item.EEstado == EEstado.Anulado) continue;

					if (item.EEstado != EEstado.Exportado)
						item.EEstado = EEstado.Exportado;
				}

				invoices.Save();
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
				if (invoices != null) invoices.CloseSession();
			}
        }

		public virtual void ExportCharges()
        {
			Charges charges2 = null;

			try
			{
				_invoice_conditions.Orders.Clear();
				_invoice_conditions.Orders.NewOrder("Vencimiento", System.ComponentModel.ListSortDirection.Ascending, typeof(Charge));

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
				charges2 = Charges.GetList(_invoice_conditions, false);
                FinancialCashList efectos = FinancialCashList.GetList(false);

				foreach (Charge item in charges2)
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

				charges2.Save();
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
				if (charges2 != null) charges2.CloseSession();
			}
        }

		public virtual void ExportExpenses()
		{
			Payments pagos2 = null;

			try
			{
				_store_conditions.TipoPago = ETipoPago.Gasto;
				_store_conditions.CategoriaGasto = ECategoriaGasto.Generales;
				_store_conditions.Orders.Clear();
                _store_conditions.Orders.NewOrder("Vencimiento", System.ComponentModel.ListSortDirection.Ascending, typeof(Payment));
				
				PaymentList pagos = PaymentList.GetList(_store_conditions, true);

				foreach (PaymentInfo item in pagos)
				{
					if (item.EEstado == EEstado.Anulado) continue;

					LineaRegistro lr = _registry.LineaRegistros.NewItem(_registry, item);

					BuildExpenseAccountingEntry(item, lr);

					_accounting_entry++;
				}

				//Cambiamos el estado de los elementos contabilizados
				pagos2 = Payments.GetList(_store_conditions, false);

                foreach (Payment item in pagos2)
				{
					if (item.EEstado == EEstado.Anulado) continue;

					if (item.EEstado != EEstado.Exportado)
                        if (item.EEstadoPago == EEstado.Pagado) item.EEstado = EEstado.Exportado;
				}

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

		public virtual void ExportPayrolls()
		{
			PayrollBatchs payrollbatchs2 = null;

			try
			{
				if (Invoice.ModulePrincipal.GetCuentaNominasSetting() == string.Empty)
					throw new iQException("No se ha definido la cuenta contable asociada para las Nóminas");

				if (Invoice.ModulePrincipal.GetCuentaSegurosSocialesSetting() == string.Empty)
					throw new iQException("No se ha definido la cuenta contable asociada para los seguros sociales");

				if (Invoice.ModulePrincipal.GetCuentaHaciendaSetting() == string.Empty)
					throw new iQException("No se ha definido la cuenta contable asociada para la Hacienda Pública");

				_store_conditions.Orders.Clear();
                _store_conditions.Orders.NewOrder("Vencimiento", System.ComponentModel.ListSortDirection.Ascending, typeof(Payment));

				_payments = PaymentList.GetListInNomina(_store_conditions, true);
                _employees = EmployeeList.GetList(false);

				_store_conditions.Orders.Clear();
				_store_conditions.Orders.NewOrder("Fecha", System.ComponentModel.ListSortDirection.Ascending, typeof(PayrollBatch));

				PayrollBatchList payrollbatchs = PayrollBatchList.GetList(_store_conditions, true);

				foreach (PayrollBatchInfo item in payrollbatchs)
				{
					if (item.EEstado == EEstado.Anulado) continue;

					LineaRegistro lr = _registry.LineaRegistros.NewItem(_registry, item);

					BuildPayrollBatchAccountingEntry(item, lr);
					_accounting_entry++;
				}

				//Cambiamos el estado de las nominas contabilizadas
				payrollbatchs2 = PayrollBatchs.GetList(_store_conditions, true);

				foreach (PayrollBatch item in payrollbatchs2)
				{
					if (item.EEstado == EEstado.Anulado) continue;

					if (item.EEstado != EEstado.Exportado)
						item.EEstado = EEstado.Exportado;
				}

				payrollbatchs2.Save();
			}
			catch (Exception ex)
			{
				_registry = null;
				throw ex;
			}
			finally
			{
				if (payrollbatchs2 != null) payrollbatchs2.CloseSession();
			}
		}

		public virtual void ExportGrants()
		{
			REAExpedients reas2 = null;
			//LineasFomento fomentos2 = null;

			try
			{
				//AYUDAS REA

				_store_conditions.Orders.Clear();
				_store_conditions.Orders.NewOrder("Fecha", System.ComponentModel.ListSortDirection.Ascending, typeof(REAExpedient));

				ExpedienteREAList reas = ExpedienteREAList.GetList(_store_conditions, true);
				_expedients = ExpedienteList.GetList(false);
				
				foreach (ExpedienteREAInfo item in reas)
				{
					if (item.EEstado == EEstado.Anulado) continue;
					if (item.EEstado == EEstado.Desestimado) continue;

					LineaRegistro lr = _registry.LineaRegistros.NewItem(_registry, item);
					
					BuildREAGrantAccountingEntry(item, lr);

					_accounting_entry++;
				}

				//Cambiamos el estado de las ayudas contabilizadas
				reas2 = REAExpedients.GetList(_store_conditions, false);

                foreach (REAExpedient item in reas2)
				{
					if (item.EEstado == EEstado.Anulado) continue;
					if (item.EEstado == EEstado.Desestimado) continue;

					if (item.EEstado != EEstado.Exportado) item.EEstado = EEstado.Exportado;
				}

				reas2.Save();

				//AYUDAS FOMENTO

				/*LineaFomentoList fomentos = LineaFomentoList.GetList(_store_conditions, true);

				foreach (LineaFomentoInfo item in fomentos)
				{
					if (item.EEstado == EEstado.Anulado) continue;
					if (item.EEstado == EEstado.Desestimado) continue;

					LineaRegistro lr = _registro.LineaRegistros.NewItem(_registro, item);

					BuildAsientoAyudaFomento(item, lr);

					_asiento++;
				}

				//Cambiamos el estado de las cobros contabilizados
				fomentos2 = LineasFomento.GetList(_store_conditions, false);

				foreach (LineaFomento item in fomentos2)
				{
					if (item.EEstado == EEstado.Anulado) continue;
					if (item.EEstado == EEstado.Desestimado) continue;

					if (item.EEstado != EEstado.Exportado) item.EEstado = EEstado.Exportado;
				}

				fomentos2.Save();*/
			}
			catch (Exception ex)
			{
				_registry = null;
				throw ex;
			}
			finally
			{
				if (reas2 != null) reas2.CloseSession();
				//if (fomentos2 != null) fomentos2.CloseSession();
			}
		}

        public virtual void ExportBankTransfers()
        {
            Traspasos banktransfers2 = null;

            try
            {
				_invoice_conditions.Orders.Clear();
				_invoice_conditions.Orders.NewOrder("Fecha", System.ComponentModel.ListSortDirection.Ascending, typeof(Traspaso));

				TraspasoList banktransfers = TraspasoList.GetList(_invoice_conditions, false);

				foreach (TraspasoInfo item in banktransfers)
                {
                    if (item.EEstado == EEstado.Anulado) continue;

                    LineaRegistro lr = _registry.LineaRegistros.NewItem(_registry, item);

                    BuildBankTransferAccountingEntry(item, lr);

                    _accounting_entry++;
                }

                //Cambiamos el estado de las ayudas contabilizadas
				banktransfers2 = Traspasos.GetList(_invoice_conditions, false);

				foreach (Traspaso item in banktransfers2)
                {
                    if (item.EEstado == EEstado.Anulado) continue;

                    if (item.EEstado != EEstado.Exportado) item.EEstado = EEstado.Exportado;
                }

				banktransfers2.Save();
            }
            catch (Exception ex)
            {
                _registry = null;
                throw ex;
            }
            finally
            {
                if (banktransfers2 != null) banktransfers2.CloseSession();
            }
        }

        public virtual void ExportLoans()
        {
            Loans prestamos2 = null;

            try
            {
				_invoice_conditions.Orders.Clear();
				_invoice_conditions.Orders.NewOrder("Fecha", System.ComponentModel.ListSortDirection.Ascending, typeof(Loan));

                LoanList prestamos = LoanList.GetOrderedByFechaList(_invoice_conditions);

                foreach (LoanInfo item in prestamos)
                {
                    if (item.EEstado == EEstado.Anulado) continue;

                    LineaRegistro lr = _registry.LineaRegistros.NewItem(_registry, item);

                    BuildLoanAccountingEntry(item, lr);

                    _accounting_entry++;
                }

                //Cambiamos el estado de las ayudas contabilizadas
                prestamos2 = Loans.GetList(_invoice_conditions);

                foreach (Loan item in prestamos2)
                {
                    if (item.EEstado == EEstado.Anulado) continue;

                    if (item.EEstado != EEstado.Exportado) item.EEstado = EEstado.Exportado;
                }

                prestamos2.Save();
            }
            catch (Exception ex)
            {
                _registry = null;
                throw ex;
            }
            finally
            {
                if (prestamos2 != null) prestamos2.CloseSession();
            }
        }

        protected virtual void BuildInputInvoiceAccountingEntry(InputInvoiceInfo factura, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildInputInvoiceAccountingEntry"); }
        protected virtual void BuildOutputInvoiceAccountingEntry(OutputInvoiceInfo factura, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildOutputInvoiceAccountingEntry"); }
        protected virtual void BuildInvoicePaymentAccountingEntry(PaymentInfo pago, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildInvoicePaymentAccountingEntry"); }
        protected virtual void BuildPayrollPaymentAccountingEntry(PaymentInfo pago, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildPayrollPaymentAccountingEntry"); }
        protected virtual void BuildExpensePaymentAccountingEntry(PaymentInfo pago, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildExpensePaymentAccountingEntry"); }
        protected virtual void BuildCreditCardStatementPaymentAccountingEntry(PaymentInfo pago, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildCreditCardStatementPaymentAccountingEntry"); }
        protected virtual void BuildChargeAccountingEntry(ChargeInfo cobro, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildChargeAccountingEntry"); }
        protected virtual void BuildREAChargeAccountingEntry(ChargeInfo cobro, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildREAChargeAccountingEntry"); }
		protected virtual void BuildAsientoCobroAyudaFomento(ChargeInfo cobro, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildAsientoCobroAyudaFomento"); }
        protected virtual void BuildREAGrantAccountingEntry(ExpedienteREAInfo expediente, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildREAGrantAccountingEntry"); }
        protected virtual void BuildFomentoGrantAccountingEntry(LineaFomentoInfo linea, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildFomentoGrantAccountingEntry"); }
        protected virtual void BuildExpenseAccountingEntry(PaymentInfo pago, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildExpenseAccountingEntry"); }
        protected virtual void BuildPayrollBatchAccountingEntry(PayrollBatchInfo pago, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildPayrollBatchAccountingEntry"); }
        protected virtual void BuildTaxBookSoportadoAccountingEntry(InputInvoiceInfo factura) { throw new iQImplementationException("ContabilidadExporterBase::BuildTaxBookSoportadoAccountingEntry"); }
        protected virtual void BuildTaxBookRepercutidoAccountingEntry(OutputInvoiceInfo factura) { throw new iQImplementationException("ContabilidadExporterBase::BuildTaxBookRepercutidoAccountingEntry"); }
        protected virtual void BuildFinancialCashBookPaymentAccountingEntry(PaymentInfo pago) { throw new iQImplementationException("ContabilidadExporterBase::BuildFinancialCashBookPaymentAccountingEntry"); }
        protected virtual void BuildFinalcialCashBookChargeAccountingEntry(ChargeInfo cobro) { throw new iQImplementationException("ContabilidadExporterBase::BuildFinalcialCashBookChargeAccountingEntry"); }
        protected virtual void BuildBankTransferAccountingEntry(TraspasoInfo traspaso, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildBankTransferAccountingEntry"); }
        protected virtual void BuildLoanAccountingEntry(LoanInfo traspaso, LineaRegistro lr) { throw new iQImplementationException("ContabilidadExporterBase::BuildLoanAccountingEntry"); }

		protected virtual void BuildAccountingLine(IApunteContable apunteContable) { }

		protected string GetPaymentAccount(PaymentInfo payment)
		{
			string cuenta = string.Empty;
			string info = string.Empty;

			try
			{
                switch (payment.EMedioPago)
                {
                    case EMedioPago.Efectivo:
                        {
                            CashInfo caja = CashInfo.Get(1);
                            cuenta = caja.CuentaContable;
                            info = String.Format(Resources.Messages.CASH_BOOK_ACCOUNT_NOT_FOUND, caja.Codigo, caja.Nombre);
                        }
                        break;

                    case EMedioPago.Tarjeta:
                        {
                            CreditCardInfo tarjeta = CreditCardInfo.Get(payment.OidTarjetaCredito);
                            BankAccountInfo cb = _bank_accounts.GetItem(payment.OidCuentaBancaria);
                            cuenta = (tarjeta.ETipoTarjeta == ETipoTarjeta.Credito) ? tarjeta.CuentaContable : cb.CuentaContable;
                            info = String.Format(Resources.Messages.CREDIT_CARD_BOOK_ACCOUNT_NOT_FOUND, tarjeta.Numeracion, tarjeta.Nombre);
                        }
                        break;

                    case EMedioPago.CompensacionFactura:
                        {
                            IAcreedorInfo provider = _providers.GetItem(payment.OidAgente, payment.ETipoAcreedor);
                            cuenta = provider.CuentaContable;
                            info = String.Format(Resources.Messages.PROVIDER_BOOK_ACCOUNT_NOT_FOUND, provider.Codigo, provider.Nombre);
                        }
                        break;

                    case EMedioPago.Pagare:

                        if (payment.EEstadoPago == EEstado.Pagado)
                        {
                            BankAccountInfo cb = _bank_accounts.GetItem(payment.OidCuentaBancaria);
                            cuenta = cb.CuentaContable;
                            info = String.Format(Resources.Messages.BANK_ACCOUNT_BOOK_ACCOUNT_NOT_FOUND, cb.Valor, cb.Entidad);
                        }
                        else
                        {
                            cuenta = GetFinancialCashPaymentsAccount(payment.Vencimiento);
                        }
                        break;

                    default:
                        {
                            BankAccountInfo cb = _bank_accounts.GetItem(payment.OidCuentaBancaria);
                            cuenta = cb.CuentaContable;
                            info = String.Format(Resources.Messages.BANK_ACCOUNT_BOOK_ACCOUNT_NOT_FOUND, cb.Valor, cb.Entidad);
                        }
                        break;
                }
			}
			catch (Exception ex)
			{
				if (ex is iQException)
					throw ex;
				else
					throw new iQException("El pago nº " + payment.Codigo + " no tiene cuenta bancaria asociada");
			}

			if (cuenta == string.Empty)
				throw new iQException(info);

			return Convert.ToInt64(cuenta).ToString();//Por algún motivo hay cuentas guardadas con un salto de línea al final y da problemas al exportar a contawin
		}

		protected string GetChargeAccount(ChargeInfo charge)
		{
			string cuenta = string.Empty;
			string info = string.Empty;

			try
			{
				switch (charge.EMedioPago)
				{
					case EMedioPago.Efectivo:
						CashInfo caja = CashInfo.Get(1); 
						cuenta = caja.CuentaContable;
						info = String.Format(Resources.Messages.CASH_BOOK_ACCOUNT_NOT_FOUND, caja.Codigo, caja.Nombre);
						break;

					case EMedioPago.Tarjeta:
						if (Invoice.ModulePrincipal.GetUseTPVCountSetting())
						{
							TPVInfo tpv = TPVInfo.Get(charge.OidTPV);
							cuenta = tpv.CuentaContable;
							info = tpv.Nombre;
							info = String.Format(Resources.Messages.TPV_BOOK_ACCOUNT_NOT_FOUND, tpv.Nombre);
						}
						else
						{
							BankAccountInfo cb = _bank_accounts.GetItem(charge.OidCuentaBancaria);
							cuenta = cb.CuentaContable;
							info = String.Format(Resources.Messages.BANK_ACCOUNT_BOOK_ACCOUNT_NOT_FOUND, cb.Valor, cb.Entidad);
						}
						break;

					case EMedioPago.CompensacionFactura:
						
						ClienteInfo client = _clients.GetItem(charge.OidCliente);
						cuenta = client.CuentaContable;
						info = String.Format(Resources.Messages.CLIENT_BOOK_ACCOUNT_NOT_FOUND, client.Codigo, client.Nombre);
						break;

                    case EMedioPago.Pagare:
                    case EMedioPago.Cheque:
                        {
                            FinancialCashInfo efecto = FinancialCashInfo.GetByCobro(charge, false);
                            if (efecto.EEstadoCobro == EEstado.Charged)
                            {
                                BankAccountInfo cb = _bank_accounts.GetItem(efecto.OidCuentaBancaria);
                                cuenta = cb.CuentaContable;
								info = String.Format(Resources.Messages.BANK_ACCOUNT_BOOK_ACCOUNT_NOT_FOUND, cb.Valor, cb.Entidad);
                            }
                            else
                            {
                                cuenta = GetFinancialCashChargesAccount(efecto.Vencimiento);
                            }
                        }
                        break;

					default:
						{
							BankAccountInfo cb = _bank_accounts.GetItem(charge.OidCuentaBancaria);
							cuenta = cb.CuentaContable;
							info = String.Format(Resources.Messages.BANK_ACCOUNT_BOOK_ACCOUNT_NOT_FOUND, cb.Valor, cb.Entidad);
						}
						break;
				}
			}
			catch (Exception ex)
			{
				if (ex is iQException)
					throw ex;
				else
					throw new iQException("El cobro nº " + charge.Codigo + " no tiene cuenta bancaria asociada");
			}

			if (cuenta == string.Empty)
				throw new iQException(info);

			return cuenta;
		}

        protected string GetCreditCardAccount(PaymentInfo payment, ETipoEntidad accountType)
        {
            string cuenta = string.Empty;
            string info = string.Empty;
            CreditCardInfo credit_card = null;
            
            try
            {
                credit_card = CreditCardInfo.Get(payment.OidTarjetaCredito);

                if (accountType == ETipoEntidad.CuentaBancaria)
                {
                    BankAccountInfo cb = _bank_accounts.GetItem(credit_card.OidCuentaBancaria);
                    cuenta = cb.CuentaContable;
                    info = String.Format(Resources.Messages.BANK_ACCOUNT_BOOK_ACCOUNT_NOT_FOUND, cb.Valor, cb.Entidad);
                }
                else
                {
                    cuenta = credit_card.CuentaContable;
                    info = String.Format(Resources.Messages.CREDIT_CARD_BOOK_ACCOUNT_NOT_FOUND, credit_card.Numeracion, credit_card.Nombre);
                }
			}
			catch (Exception ex)
			{
				if (ex is iQException)
					throw ex;
				else
					throw new iQException("La tarjeta nº " + credit_card.Nombre + " no tiene cuenta bancaria asociada");
			}

			if (cuenta == string.Empty)
				throw new iQException(info);

			return cuenta;
        }

		protected string GetGrantAccount(ETipoAyuda grantType, ExpedientInfo expedient)
		{
			AyudaInfo ayuda = null;
			string cuenta = string.Empty;
			string info = string.Empty;

			try
			{
				switch (grantType)
				{
					case ETipoAyuda.REA:

						switch (expedient.ETipoExpediente)
						{
							case ETipoExpediente.Alimentacion: ayuda = _ayudas.GetItem(Store.ModulePrincipal.GetAyudaREASetting()); break;
							case ETipoExpediente.Ganado: ayuda = _ayudas.GetItem(Store.ModulePrincipal.GetAyudaPOSEISetting()); break;
						}

						cuenta = ayuda.CuentaContable;
						info = ayuda.Nombre;
						break;

					case ETipoAyuda.Fomento:
						ayuda = _ayudas.GetItem(Store.ModulePrincipal.GetAyudaFomentoSetting());
						cuenta = ayuda.CuentaContable;
						info = ayuda.Nombre;
						break;
				}
			}
			catch (Exception ex)
			{
				if (ex is iQException)
					throw ex;
				else
					throw new iQException(string.Format(Resources.Messages.NO_AYUDA_ACCOUNT, ayuda.Nombre));
			}

			if (cuenta == string.Empty)
				throw new iQException(string.Format(Resources.Messages.NO_AYUDA_ACCOUNT, ayuda.Nombre));

			return cuenta;
		}

		protected string GetBankExpensesAccount(PaymentInfo payment)
		{
			string cuenta = string.Empty;
			string info = string.Empty;

			try
			{
				BankAccountInfo cb = _bank_accounts.GetItem(payment.OidCuentaBancaria);
				cuenta = cb.CuentaContableGastos;
				info = String.Format(Resources.Messages.BANK_ACCOUNT_BOOK_ACCOUNT_NOT_FOUND, cb.Valor, cb.Entidad) + " (gastos bancarios)";
			}
			catch (Exception ex)
			{
				throw new iQException(Resources.Messages.BOOK_ACCOUNT_ERROR, iQExceptionHandler.GetAllMessages(ex));
			}

			if (cuenta == string.Empty)
				throw new iQException(info);

			return cuenta;
		}

		protected string GetBankExpensesAccount(ChargeInfo charge)
		{
			string cuenta = string.Empty;
			string info = string.Empty;

			try
			{
				BankAccountInfo cb = _bank_accounts.GetItem(charge.OidCuentaBancaria);
				cuenta = cb.CuentaContableGastos;
				info = String.Format(Resources.Messages.BANK_ACCOUNT_BOOK_ACCOUNT_NOT_FOUND, cb.Valor, cb.Entidad) + " (gastos bancarios)";
			}
			catch (Exception ex)
			{
				throw new iQException(Resources.Messages.BOOK_ACCOUNT_ERROR, iQExceptionHandler.GetAllMessages(ex));
			}

			if (cuenta == string.Empty)
				throw new iQException(info);

			return cuenta;
		}

		protected string GetFinancialCashPaymentsAccount(DateTime date)
		{
			string cuenta = string.Empty;
			string mes = string.Empty;

			try
			{
				cuenta = Invoice.ModulePrincipal.GetCuentaEfectosAPagarSetting();

				if (cuenta == string.Empty) throw new Exception();

				mes = date.ToString("MM");

				cuenta = cuenta.Substring(0, cuenta.Length - 2) + mes;
			}
			catch (Exception ex)
			{
				throw new iQException(Resources.Messages.PAYMENT_EFFECTS_BOOK_ACCOUNT_ERROR, iQExceptionHandler.GetAllMessages(ex));
			}

			if (cuenta == string.Empty)
				throw new iQException(Resources.Messages.PAYMENT_EFFECTS_BOOK_ACCOUNT_NOT_FOUND);

			return cuenta;
		}

		protected string GetFinancialCashChargesAccount(DateTime date)
		{
			string cuenta = string.Empty;
			string mes = string.Empty;

			try
			{
				cuenta = Invoice.ModulePrincipal.GetCuentaEfectosACobrarSetting();

				if (cuenta == string.Empty) throw new Exception();

				mes = date.ToString("MM");

				cuenta = cuenta.Substring(0, cuenta.Length - 2) + mes;
			}
			catch (Exception ex)
			{
				throw new iQException(Resources.Messages.CHARGE_EFFECTS_BOOK_ACCOUNT_ERROR, iQExceptionHandler.GetAllMessages(ex));
			}

			if (cuenta == string.Empty)
				throw new iQException(Resources.Messages.CHARGE_EFFECTS_BOOK_ACCOUNT_NOT_FOUND);

			return cuenta;
		}

		protected virtual string AddValue(string value) { return string.Empty; }

		protected virtual string GetColumnCode(EColumnaApunte value) { return string.Empty; }
		protected virtual string GetTaxCode(ETipoImpuestoApunte value) { return string.Empty; }
		protected virtual string GetPositionCode(EPosicionApunte value) { return string.Empty; }
		protected virtual string GetRegistryTypeCode(ETipoRegistroApunte value) { return string.Empty; }

        #endregion
    }

	#region Enums

	public enum EColumnaApunte { Debe = 1, Haber = 2}

	public enum EPosicionApunte { Inicial = 1, Medio = 2, Final = 3 }

	public enum ETipoFile { General = 1, Impuestos = 2, Efectos = 3, Activos = 5, Estimaciones = 6 }

	public enum ETipoImpuestoApunte { Soportado = 1, Repercutido = 2 }

	public enum ETipoRegistroApunte
	{ 
		ApunteSinImpuesto = 0,			// 0 Alta de Apuntes sin IVA
		ApunteConImpuesto = 1,			// 1 Alta Cabecera de apuntes con IVA (Formato para Facturas) 
		ApunteAbonoConImpuesto = 2,		// 2 Alta Cabecera de apuntes con IVA (Formatos para Rectificativas/Abonos)
		DetalleImpuesto = 3,			// 9 Detalle de apuntes con IVA (Facturas y Rectificativas)
		AltaVencimiento = 4,			// V Alta de Vencimientos
		AltaVencimientoAmpliado = 5,	// V (A posición 69) Alta de Vencimientos. Registro de Ampliación.
		BajaVencimiento = 6,			// B Baja de Vencimientos
		AltaEdicionCuenta = 7,			// C Alta y modificación de cuentas y/o clientes o proveedores
		CuentaCorriente = 8,			// C (B posición 73) Alta de CCC de clientes o proveedores.
		AltaDistribucion = 9,			// A Alta tabla de niveles de la distribución
	}

	#endregion
}