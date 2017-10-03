using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Threading;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;
using moleQule.Library.Invoice.Properties;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class ModuleController
	{
		#region Attributes & Properties

		public static string GetFacturaWebScript()
		{
			return Settings.Default.FACTURA_WEB_SCRIPT;
		}

		public static string GetCuentasMask()
		{
			return Common.ModuleController.GetCuentasMask();
		}
		public static string GetNFacturaMask()
		{
			string mask = string.Empty;

			try
			{
				for (int i = 1; i <= Invoice.ModulePrincipal.GetNDigitosNFacturaSetting(); i++)
					mask += "0";
			}
			catch { mask = string.Empty; }

			return mask;
		}

		#endregion

		#region Factory Methods

		/// <summary>
        /// Única instancia de la clase ControlerBase (Singleton)
        /// </summary>
        protected static ModuleController _main;

        /// <summary>
        /// Unique Controler Class Instance
        /// </summary>
		public static ModuleController Instance { get { return (_main != null) ? _main : new ModuleController(); } }
        
        /// <summary>
        /// Contructor 
        /// </summary>
		protected ModuleController()
        {
            // Singleton
            _main = this;
        }

		public void AutoPilot()
		{
			DateTime f_envio = DateTime.Parse(	DateTime.Now.ToString("dd/MM/yyyy") + " " + ModulePrincipal.GetHoraEnvioFacturasPendientes().ToString("HH:mm"),
												CultureInfo.CreateSpecificCulture("es-ES"));

			if (DateTime.Now < f_envio) return;

			if (ModulePrincipal.GetSendFacturasPendientes() &&
				(ModulePrincipal.GetFechaUltimoEnvioFacturasPendientes().AddDays(ModulePrincipal.GetPlazoEnvioFacturasPendientes()) < DateTime.Today))
			{
				Thread thread = new Thread(SendMailsFacturasPendientes);
				thread.Start();
			}
		}

		public static void CheckDBVersion()
		{
			ApplicationSettingInfo dbVersion = ApplicationSettingInfo.Get(Settings.Default.DB_VERSION_VARIABLE);

			//Version de base de datos equivalente o no existe la variable
			if ((dbVersion.Value == string.Empty) ||
				(String.CompareOrdinal(dbVersion.Value, ModulePrincipal.GetDBVersion()) == 0))
			{
				return;
			}
			//Version de base de datos superior
			else if (String.CompareOrdinal(dbVersion.Value, ModulePrincipal.GetDBVersion()) > 0)
			{
				throw new iQException(String.Format(Library.Resources.Messages.DB_VERSION_HIGHER,
													dbVersion.Value,
													ModulePrincipal.GetDBVersion(),
													Settings.Default.NAME),
													iQExceptionCode.DB_VERSION_MISSMATCH);
			}
			//Version de base de datos inferior
			else if (String.CompareOrdinal(dbVersion.Value, ModulePrincipal.GetDBVersion()) < 0)
			{
				throw new iQException(String.Format(Library.Resources.Messages.DB_VERSION_LOWER,
													dbVersion.Value,
													ModulePrincipal.GetDBVersion(),
													Settings.Default.NAME),
													iQExceptionCode.DB_VERSION_MISSMATCH);
			}
		}

		public static void UpgradeSettings() { ModulePrincipal.UpgradeSettings(); }

		#endregion

        #region Variables

		public static string GetDefaultSerieVariableName()
		{
			return Settings.Default.SETTING_NAME_DEFAULT_SERIE_VENTA;
		}
		public static string GetLineaCajaLibreVariableName()
		{
			return Settings.Default.SETTING_NAME_LINEA_CAJA_LIBRE;
		}
		public static string GetLastAsientoVariableName()
		{
			return Settings.Default.SETTING_NAME_LAST_ENTRY;
		}
		public static string GetJournalVariableName()
		{
			return Settings.Default.SETTING_NAME_JOURNAL_VARIABLE_NAME;
		}
		public static string GetUseTPVCountVariableName()
		{
			return Settings.Default.SETTING_NAME_USE_TPV_COUNT;
		}
		public static string GetNDigitosCuentasContablesVariableName()
		{
			return Settings.Default.SETTING_NAME_N_DIGITOS_CUENTAS_CONTABLES;
		}
		public static string GetCuentaNominasVariableName()
		{
			return Settings.Default.SETTING_NAME_CUENTA_NOMINAS;
		}
		public static string GetCuentaSegurosSocialesVariableName()
		{
			return Settings.Default.SETTING_NAME_CUENTA_SEGUROS_SOCIALES;
		}
		public static string GetCuentaHaciendaVariableName()
		{
			return Settings.Default.SETTING_NAME_CUENTA_IRPF;
		}
		public static string GetCuentaRemuneracionesVariableName()
		{
			return Settings.Default.SETTING_NAME_CUENTA_REMUNERACIONES;
		}
		public static string GetCuentaEfectosAPagarVariableName()
		{
			return Settings.Default.SETTING_NAME_CUENTA_EFECTOS_A_PAGAR;
		}
		public static string GetCuentaEfectosACobrarVariableName()
		{
			return Settings.Default.SETTING_NAME_CUENTA_EFECTOS_A_COBRAR;
		}

        #endregion

		#region Business Methods

		#endregion

		#region Scripts

		public static void CreateApuntesBancarios(PaymentList pagos)
		{
			List<PaymentInfo> list = new List<PaymentInfo>();

			CreditCardList tarjetas = CreditCardList.GetList();
			Payments pagos_tarjeta = Payments.NewList();

			foreach (PaymentInfo item in pagos)
			{
				if (!Common.EnumFunctions.NeedsCuentaBancaria(item.EMedioPago)) continue;
				if (item.Vencimiento > DateTime.Today) continue;

				if (item.EMedioPago != EMedioPago.Tarjeta)
				{
					//Apunte bancario del pagaré, talón, etc..
					BankLine.InsertItem(item, true);

					list.Add(item);
				}
				else
				{
                    Payment pago_tarjeta = pagos_tarjeta.GetItemByTarjetaCredito(item.OidTarjetaCredito, item.Vencimiento);

					if (pago_tarjeta == null)
					{
						pago_tarjeta = pagos_tarjeta.NewItem(item, ETipoPago.ExtractoTarjeta);
                        TransactionPayment pf = pago_tarjeta.Operations.NewItem(pago_tarjeta, item, item.ETipoPago);
                        pf.Cantidad = item.Total;
                        pago_tarjeta.EEstadoPago = EEstado.Pagado;
					}
					else
					{
						pago_tarjeta.Importe += item.Importe;
                        pago_tarjeta.GastosBancarios += item.GastosBancarios;
                        TransactionPayment pf = pago_tarjeta.Operations.NewItem(pago_tarjeta, item, item.ETipoPago);
                        pf.Cantidad = item.Total;
					}

					list.Add(item);
				}
			}

            Payments pagos_fraccionados = Payments.NewList();
            pagos_fraccionados.OpenNewSession();

			//Apunte bancario de la tarjeta
            foreach (Payment item in pagos_tarjeta)
			{
                Payment root = pagos_fraccionados.NewItem(item.GetInfo(false), ETipoPago.FraccionadoTarjeta);
                root.Pagos.AddItem(item);

                //if (item.Importe != 0)
                //    MovimientoBanco.InsertItemTarjeta(item, tarjetas.GetItem(item.OidTarjetaCredito));						
			}

            pagos_fraccionados.BeginTransaction();
            pagos_fraccionados.Save();

            Payment.UpdatePagadoFromList(list, true);
		}
		public static void CreateApuntesBancarios(ChargeList cobros)
		{
			//MovimientoBancoList movs = MovimientoBancoList.GetList(DateTime.Today, false);
			List<ChargeInfo> list = new List<ChargeInfo>();

			foreach (ChargeInfo item in cobros)
			{
				if (!Common.EnumFunctions.NeedsCuentaBancaria(item.EMedioPago)) continue;

				//if (movs.GetItemByOperacion(item.Oid, item.ETipoMovimientoBanco) != null) continue;

				//Apunte bancario del pagaré, talón, etc..
				BankLine.InsertItem(item, true);

				list.Add(item);
			}

			Charge.UpdateCobradoFromList(list, true);
		}
		
		public static ChargeList GetCobrosPendientes()
		{
			DateTime f_fin = DateTime.Today.AddDays((double)Library.Invoice.ModulePrincipal.GetNotifyPlazoCobros());
			ChargeList list = ChargeList.GetListPendientes(ETipoCobro.Todos, DateTime.MinValue, f_fin, false);

			return list;
		}

		public static ChargeList GetCobrosVencidosSinApunte(DateTime fecha)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaAuxFin = fecha
			};
			ChargeList list = ChargeList.GetListByVencimientoSinApunte(conditions, false);

			return list;
		}

		public static OutputInvoiceList GetFacturasPendientes()
		{
			DateTime f_fin = DateTime.Today.AddDays((double)Library.Invoice.ModulePrincipal.GetNotifyPlazoFacturasEmitidas());
			OutputInvoiceList list = OutputInvoiceList.GetNoCobradasList(DateTime.MinValue, f_fin, false);

			return list;
		}

		public static void ReindexarLineaCajasAbiertas()
		{
			Cash caja = Cash.Get(1, true);
			caja.ReindexarLineas();
			caja.Save();
			caja.CloseSession();
		}

		public static void ReindexarLineaCajas()
		{
			CierreCajaList cierres = CierreCajaList.GetList();
			CierreCaja cierre;
			int index = 1;

			foreach (CierreCajaInfo item in cierres)
			{
				cierre = CierreCaja.Get(item.Oid);
				foreach (CashLine item2 in cierre.LineaCajas)
				{
					item2.Serial = index++;
					item2.Codigo = item2.Serial.ToString(Library.Invoice.Resources.Defaults.LINEACAJA_CODE_FORMAT);
				}

				cierre.Save();
				cierre.CloseSession();
			}

			Cash caja = Cash.Get(1, true);
			caja.ReindexarLineas();
			caja.Save();
			caja.CloseSession();
		}

		public static void RellenaAlbaranesEnFacturas()
		{
			Library.Store.QueryConditions conditions = new Library.Store.QueryConditions
			{
				FechaIni = DateAndTime.FirstDay(2010),
				FechaFin = DateAndTime.LastDay(2010)
			};

            InputInvoices list_p = InputInvoices.GetList(conditions, false);

			foreach (InputInvoice item in list_p)
			{
				item.LoadChilds(typeof(AlbaranFacturaProveedor), false);
				item.SetAlbaranes();
			}

			list_p.Save();
			list_p.CloseSession();
		}

		public static void RenameClientes()
		{
			Clientes clientes = Clientes.GetList(false);

			foreach (Cliente item in clientes)
			{
				item.Nombre = item.Nombre.Replace("María", "Laura");
				item.Nombre = item.Nombre.Replace("Antonio", "Javier");
				item.Nombre = item.Nombre.Replace("Rosendo", "Jaime");
				item.Nombre = item.Nombre.Replace("Angel", "Julio");
				item.Nombre = item.Nombre.Replace("Ángel", "Julio");
				item.Nombre = item.Nombre.Replace("Agustín", "Angel");
				item.Nombre = item.Nombre.Replace("Juan", "José");
				item.Nombre = item.Nombre.Replace("José", "Juan");
				item.Nombre = item.Nombre.Replace("Jose", "Juan");
				item.Nombre = item.Nombre.Replace("Francisco", "Manuel");
				item.Nombre = item.Nombre.Replace("Manuel", "Antonio");
				item.Nombre = item.Nombre.Replace("Carmelo", "Francisco");
				item.Nombre = item.Nombre.Replace("Alejandro", "Alberto");
				item.Nombre = item.Nombre.Replace("Alberto", "Ricardo");
			}

			clientes.Save();
			clientes.CloseSession();

			OutputInvoices facturas = OutputInvoices.GetList(false);

			foreach (OutputInvoice item in facturas)
			{
				item.Cliente = item.Cliente.Replace("María", "Laura");
				item.Cliente = item.Cliente.Replace("Antonio", "Javier");
				item.Cliente = item.Cliente.Replace("Rosendo", "Jaime");
				item.Cliente = item.Cliente.Replace("Angel", "Julio");
				item.Cliente = item.Cliente.Replace("Ángel", "Julio");
				item.Cliente = item.Cliente.Replace("Agustín", "Angel");
				item.Cliente = item.Cliente.Replace("José", "Juan");
				item.Cliente = item.Cliente.Replace("José", "Juan");
				item.Cliente = item.Cliente.Replace("Francisco", "Manuel");
				item.Cliente = item.Cliente.Replace("Manuel", "Antonio");
				item.Cliente = item.Cliente.Replace("Carmelo", "Francisco");
				item.Cliente = item.Cliente.Replace("Alejandro", "Alberto");
				item.Cliente = item.Cliente.Replace("Alberto", "Ricardo");
			}

			facturas.Save();
			facturas.CloseSession();
		}

		public static void SendMailDelegate(object mail) 
        {
            try
            {
                EMailClient.Instance.SmtpCliente.Send((System.Net.Mail.MailMessage)mail);
            }
            catch { }
        }
		public static void SendMailsFacturasPendientes()
		{
            try
            {
                OutputInvoiceList facturas = OutputInvoiceList.GetNoCobradasList(true);
                ClienteList clientes = ClienteList.GetList(false);
                SerieList series = SerieList.GetList(false);
                CompanyInfo empresa = CompanyInfo.Get(AppContext.ActiveSchema.Oid);

                SerieInfo serie;
                ClienteInfo cliente;

				Registro registro = Registro.New(ETipoRegistro.Email);
				registro.Nombre = "Envio automático de Facturas";
				registro.Observaciones = "Envio automático de Facturas pendientes de pago";

                foreach (OutputInvoiceInfo item in facturas)
                {
                    if (item.Prevision.AddDays(ModulePrincipal.GetPeriodicidadEnvioFacturasPendientes()) > DateTime.Today) continue;

                    cliente = clientes.GetItem(item.OidCliente);
                    if (!cliente.EnviarFacturaPendiente) continue;

                    serie = series.GetItem(item.OidSerie);

                    FormatConfFacturaAlbaranReport conf = new FormatConfFacturaAlbaranReport();

                    conf.nota = (cliente.OidImpuesto == 1) ? Resources.Messages.NOTA_EXENTO_IGIC : string.Empty;
                    conf.nota += Environment.NewLine + (item.Nota ? serie.Cabecera : "");

                    OutputInvoiceReportMng reportMng = new OutputInvoiceReportMng(AppContext.ActiveSchema, string.Empty, string.Empty);
                    ReportClass report = reportMng.GetDetailReport(item, serie, cliente, null, conf);

                    if (report != null)
                    {
						LineaRegistro linea = registro.LineaRegistros.NewItem(registro, cliente);

                        ExportOptions options = new ExportOptions();
                        DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();

                        string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        fileName += "\\" + item.FileName;

                        diskFileDestinationOptions.DiskFileName = fileName;
                        options.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;
                        options.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;
                        options.ExportDestinationOptions = diskFileDestinationOptions;

                        report.Export(options);

                        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                        mail.To.Add(new MailAddress(cliente.Email, cliente.Nombre));
                        mail.From = new MailAddress(SettingsMng.Instance.GetSMTPMail(), empresa.Name);
                        mail.Body = String.Format(Resources.Messages.FACTURA_EMAIL_ATTACHMENT_BODY, empresa.Name);
                        mail.Subject = Resources.Messages.FACTURA_EMAIL_SUBJECT;
                        mail.Attachments.Add(new Attachment(fileName));

						try
                        {
                            Thread mailThread = new Thread(SendMailDelegate);
                            mailThread.Start(mail);
                            while (mailThread.IsAlive);

							linea.Observaciones = linea.Descripcion;
							linea.Descripcion = String.Format(Resources.Messages.FACTURA_EMAIL_OK, item.NFactura);
                        }
                        catch (Exception ex)
                        {
							linea.Observaciones = linea.Descripcion;
							linea.Descripcion = String.Format(Resources.Messages.FACTURA_EMAIL_ERROR, item.NFactura);
							registro.Save();
                            throw new iQException(ex.Message + Environment.NewLine + Environment.NewLine + moleQule.Library.Resources.Errors.SMTP_SETTINGS);
                        }
                        finally
                        {
                            mail.Dispose();
                            try { File.Delete(fileName); }
                            catch (Exception ex) { string a = ex.Message; }
                        }
                    }
                }

				registro.Save();

                ModulePrincipal.SetFechaUltimoEnvioFacturasPendientes(DateTime.Now);
                AppContext.Principal.SaveSettings();
            }
            catch { }
		}

		public static void UpdateSaldosCajas()
		{
			CierreCajaList cierres = CierreCajaList.GetList();
			CierreCaja cierre;

			foreach (CierreCajaInfo item in cierres)
			{
				cierre = CierreCaja.Get(item.Oid, true);
				cierre.UpdateSaldo();
				cierre.Save();
				cierre.CloseSession();
			}

			Cash caja = Cash.Get(1, true);
			caja.UpdateSaldo();
			caja.Save();
			caja.CloseSession();
		}

		#endregion
	}

	public class ModuleDef : IModuleDef
	{
		public string Name { get { return "Invoice"; } }
		public Type Type { get { return typeof(Invoice.ModuleController); } }
		public Type[] Mappings
		{
			get
			{
				return new Type[] 
                {   
					typeof(BankLineMap),
					typeof(BankTransferMap),
					typeof(BudgetMap),
					typeof(BudgetLineMap),
					typeof(CashMap),
                    typeof(CashLineMap),
                    typeof(CashCountMap),
					typeof(ChargeMap),
					typeof(ChargeOperationMap),
					typeof(ClientMap),
					typeof(ClientProductMap),
                    typeof(ClientTypeMap),
					typeof(CobroREAMap),
					typeof(OutputDeliveryMap),
					typeof(OutputDeliveryInvoiceMap),
					typeof(OutputDeliveryLineMap),
					typeof(DeliveryTicketMap),
                    typeof(FinancialCashMap),
					typeof(InterestRateMap),
					typeof(OutputInvoiceMap),
					typeof(InvoiceLineMap),
					typeof(LoanMap),
					typeof(OrderMap),
					typeof(OrderLineMap),
					typeof(TicketMap),
					typeof(TicketLineMap),
					typeof(TransactionMap),
                };
			}
		}

		public void GetEntities(Dictionary<Type, Type> recordEntities)
		{
			if (recordEntities.ContainsKey(typeof(BankLine))) return;

			recordEntities.Add(typeof(BankLine), typeof(BankLineRecord));
			recordEntities.Add(typeof(Traspaso), typeof(BankTransferRecord));
			recordEntities.Add(typeof(Budget), typeof(BudgetRecord));
			recordEntities.Add(typeof(BudgetLine), typeof(BudgetLineRecord));
			recordEntities.Add(typeof(Cash), typeof(CashRecord));
			recordEntities.Add(typeof(CashLine), typeof(CashLineRecord));
			recordEntities.Add(typeof(CierreCaja), typeof(CashCountRecord));
			recordEntities.Add(typeof(Charge), typeof(ChargeRecord));
			recordEntities.Add(typeof(CobroFactura), typeof(ChargeOperationRecord));
			recordEntities.Add(typeof(Cliente), typeof(ClientRecord));
			recordEntities.Add(typeof(ProductoCliente), typeof(ClientProductRecord));
			recordEntities.Add(typeof(TipoCliente), typeof(ClientTypeRecord));
			recordEntities.Add(typeof(CobroREA), typeof(CobroREARecord));
			recordEntities.Add(typeof(OutputDelivery), typeof(OutputDeliveryRecord));
			recordEntities.Add(typeof(AlbaranFactura), typeof(OutputDeliveryInvoiceRecord));
			recordEntities.Add(typeof(OutputDeliveryLine), typeof(OutputDeliveryLineRecord));
			recordEntities.Add(typeof(AlbaranTicket), typeof(DeliveryTicketRecord));
			recordEntities.Add(typeof(InterestRate), typeof(InterestRateRecord));
			recordEntities.Add(typeof(OutputInvoice), typeof(OutputInvoiceRecord));
			recordEntities.Add(typeof(OutputInvoiceLine), typeof(OutputInvoiceLineRecord));
			recordEntities.Add(typeof(Loan), typeof(LoanRecord));
			recordEntities.Add(typeof(Pedido), typeof(OrderRecord));
			recordEntities.Add(typeof(LineaPedido), typeof(OrderLineRecord));
			recordEntities.Add(typeof(Ticket), typeof(TicketRecord));
			recordEntities.Add(typeof(ConceptoTicket), typeof(TicketLineRecord));
			recordEntities.Add(typeof(Transaction), typeof(TransactionRecord));
            recordEntities.Add(typeof(FinancialCash), typeof(FinancialCashRecord));
		}
	}
}
