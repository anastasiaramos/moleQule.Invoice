using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    public enum ETipoApunte { General = 1, LibroImpuesto = 2, Efectos = 3, FacturaRecibida = 4, FacturaEmitida = 5 }

    public class ApunteContaWin : IApunteContable
    {
        #region IApunteContable

        public ETipoRegistroApunte TipoRegistro { get; set; }
        public EColumnaApunte Columna { get; set; }
        public EPosicionApunte Posicion { get; set; }
        public DateTime Fecha { get; set; }
        public string CuentaContable { get; set; }
        public string CuentaContableContrapartida { get; set; }
        public string Descripcion { get; set; }
        public string Titular { get; set; }
        public string Vat { get; set; }
        public string NFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public Decimal Importe { get; set; }
        public string Libre1 { get; set; }
        public string Libre2 { get; set; }
        public string Libre3 { get; set; }
        public string Documento { get; set; }
        public string Diario { get; set; }
        public long Asiento { get; set; }

        public Decimal BaseImponible { get; set; }
        public Decimal Total { get; set; }
        public ETipoImpuestoApunte TipoImpuesto { get; set; }
        public Decimal Porcentaje { get; set; }
        public ETipoApunte Tipo { get; set; }

        #endregion
    }

    public class ContaWinExporter: ContabilidadExporterBase, IContabilidadExporter
	{
		#region Attributes & Properties

		const string EXTRA_FILE_NAME = "extra01.csv";
		const string IVA_FILE_NAME = "iva0101.csv";
		const string EFECTOS_FILE_NAME = "efect01.csv";

		StreamWriter _extra_file = null;
        StreamWriter _iva_file = null;
        StreamWriter _efectos_file = null;

		string _extra_file_name = string.Empty;
		string _iva_file_name = string.Empty;
		string _efectos_file_name = string.Empty;

        #endregion

        #region Factory Methods

        public ContaWinExporter() {}

        public override void Init(ContabilidadConfig config)
        {
			base.Init(config);

			_extra_file_name = _config.RutaSalida + EXTRA_FILE_NAME;
			_iva_file_name = _config.RutaSalida + IVA_FILE_NAME;
			_efectos_file_name = _config.RutaSalida + EFECTOS_FILE_NAME;

			if (File.Exists(_extra_file_name)) File.Delete(_extra_file_name);
			if (File.Exists(_iva_file_name)) File.Delete(_iva_file_name);
			if (File.Exists(_efectos_file_name)) File.Delete(_efectos_file_name);
        }

		private void CreateFile(ETipoFile tipo)
		{
			switch (tipo)
			{ 
				case ETipoFile.General:
					if (!File.Exists(_extra_file_name))
						_extra_file = new StreamWriter(_extra_file_name, false, Encoding.GetEncoding(1252));
					break;

				case ETipoFile.Impuestos:
					if (!File.Exists(_iva_file_name))
						_iva_file = new StreamWriter(_iva_file_name, false, Encoding.GetEncoding(1252));
					break;

				case ETipoFile.Efectos:
					if (!File.Exists(_efectos_file_name)) 
						_efectos_file = new StreamWriter(_efectos_file_name, false, Encoding.GetEncoding(1252));
					break;
			}
		}

        public override void SaveFiles()
        {
			if (_extra_file != null) { _extra_file.Close(); _extra_file = null; }
			
			if (_iva_file != null) 
			{
				_iva_file.Close();
				_iva_file = null;

				FileInfo file = new FileInfo(_iva_file_name);
				if (file.Length == 0)
					File.Delete(_iva_file_name);				
			}
			
			if (_efectos_file != null) { _efectos_file.Close(); _efectos_file = null; } 

			base.Close();
		}

        #endregion

        #region Business Methods

		protected override string AddValue(string value) { return value + ";"; }

        public override void ExportBankTransfers()
        {
            CreateFile(ETipoFile.General);

            base.ExportBankTransfers();
        }

        public override void ExportCharges()
        {
            CreateFile(ETipoFile.General);
            CreateFile(ETipoFile.Efectos);

            base.ExportCharges();
        }

        public override void ExportExpenses()
        {
            CreateFile(ETipoFile.General);

            base.ExportExpenses();
        }

        public override void ExportGrants()
        {
            CreateFile(ETipoFile.General);
            CreateFile(ETipoFile.Efectos);

            base.ExportGrants();
        }

		public override void ExportInputInvoices()
		{
			CreateFile(ETipoFile.General);
			CreateFile(ETipoFile.Impuestos);
			CreateFile(ETipoFile.Efectos);

			base.ExportInputInvoices();
		}

        public override void ExportLoans()
        {
            CreateFile(ETipoFile.General);

            base.ExportLoans();
        }

        public override void ExportOutputInvoices()
        {
            CreateFile(ETipoFile.General);
            CreateFile(ETipoFile.Impuestos);
            CreateFile(ETipoFile.Efectos);

            base.ExportOutputInvoices();
        }

		public override void ExportPayments()
		{
			CreateFile(ETipoFile.General);
			CreateFile(ETipoFile.Efectos);

			base.ExportPayments();
		}		

		public override void ExportPayrolls()
		{
			CreateFile(ETipoFile.General);

			base.ExportPayrolls();
		}       

        /// <summary>
        /// FORMATO: 01/01/2007;4300000016;;;1;2;Factura No.  1;1;22475;166.386; 135.62;0;1;22475
        /// </summary>
        /// <param name="invoice"></param>
		protected override void BuildInputInvoiceAccountingEntry(InputInvoiceInfo invoice, LineaRegistro lr) 
        {
			try
			{
				IAcreedorInfo titular = _providers.GetItem(invoice.OidAcreedor, invoice.ETipoAcreedor);

				if (titular.CuentaContable == string.Empty)
					throw new iQException(string.Format(Resources.Messages.PROVIDER_BOOK_ACCOUNT_NOT_FOUND, titular.Codigo, titular.Nombre), new object[2] { Store.EnumConvert.ToETipoEntidad(titular.ETipoAcreedor), titular.OidAcreedor });

				// Apuntes en las cuenta del proveedor

				string apunte = string.Empty;
				string descripcion = "Fra. " + invoice.NFactura + " (" + invoice.Acreedor + ")";

				decimal importe = -invoice.Total;

				BuildAccountingLine(new ApunteContaWin
							{
								Tipo = ETipoApunte.General,
								Columna = (importe > 0) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
								Fecha = invoice.Fecha,
								CuentaContable = titular.CuentaContable,
								Importe = importe,
								Libre1 = lr.IDExportacion,
								Diario = _config.Diario,
								Asiento = _accounting_entry,
								Descripcion = descripcion,
							});

				// Apuntes en las cuentas de compra

				List<CuentaResumen> cuentas = invoice.GetCuentas();
				FamiliaInfo familia;

                //Prorrateo del descuento generico de la factura entre los conceptos
                if (invoice.Descuento != 0)
                {
                    decimal linesTotal = cuentas.Sum(x => x.Importe);
                    decimal discount_applied = 0;

                    for (var i = 0; i < cuentas.Count; i++)
                    {
                        CuentaResumen cr = cuentas[i];
                        cr.Importe -= cuentas[i].Importe * invoice.Descuento / linesTotal;
                        discount_applied += cuentas[i].Importe * invoice.Descuento / linesTotal;
                        cuentas[i] = cr;
                    }

                    decimal linesTotalUpdated = cuentas.Sum(x => x.Importe);

                    //Ajuste por redondeos
                    if (cuentas.Count > 0 && (discount_applied != invoice.Descuento))
                    {
                        CuentaResumen cr = cuentas[0];
                        cr.Importe += invoice.Descuento - discount_applied;
                        cuentas[0] = cr;
                    }
                }

				foreach (CuentaResumen cr in cuentas)
				{
					apunte = string.Empty;

					familia = _families.GetItem(cr.OidFamilia);

					if (familia == null) throw new iQException("Factura " + invoice.NFactura + " con familia a nulo");

					if (cr.CuentaContable == string.Empty)
						throw new iQException("La familia nº " + familia.Codigo + " (" + familia.Nombre + ") no tiene cuenta contable (compra) asociada", new object[2] { ETipoEntidad.Familia, familia.Oid });

					importe = cr.Importe;

					BuildAccountingLine(new ApunteContaWin
								{
									Tipo = ETipoApunte.General,
									Columna = (importe > 0) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
									Fecha = invoice.Fecha,
									CuentaContable = cr.CuentaContable,
									Importe = importe,
									Libre1 = lr.IDExportacion,
									Diario = _config.Diario,
									Asiento = _accounting_entry,
									Descripcion = descripcion,
								});
				}

				// Apuntes en las cuentas de Impuestos

				List<ImpuestoResumen> impuestos = invoice.GetImpuestos();

				foreach (ImpuestoResumen ir in impuestos)
				{
					if (ir.Importe == 0) continue;

					apunte = string.Empty;

					ImpuestoInfo impuesto = _taxes.GetItem(ir.OidImpuesto);

					if (impuesto == null) throw new iQException("Factura " + invoice.NFactura + " con impuesto a nulo");

					if (impuesto.CuentaContableSoportado == string.Empty)
						throw new iQException("El impuesto '" + impuesto.Nombre + "' no tiene cuenta contable (soportado) asociada", new object[2] { ETipoEntidad.Impuesto, impuesto.Oid });

					importe = ir.Importe;

					BuildAccountingLine(new ApunteContaWin
								{
									Tipo = ETipoApunte.General,
									Columna = (importe > 0) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
									Fecha = invoice.Fecha,
									CuentaContable = impuesto.CuentaContableSoportado,
                                    Importe = importe,
                                    Libre1 = lr.IDExportacion,
									Diario = _config.Diario,
									Asiento = _accounting_entry,
									Descripcion = descripcion,
								});
				}

				// Apunte en la cuenta del IRPF

				if (invoice.IRPF != 0)
				{
					if (ModulePrincipal.GetCuentaHaciendaSetting() == string.Empty)
						throw new iQException("No se ha definido la cuenta contable para la Hacienda Pública (IRPF Profesionales)");

					apunte = string.Empty;
					descripcion = "IRPF " + invoice.NFactura + " (" + invoice.Acreedor + ")";

					importe = -invoice.IRPF;

					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = (importe > 0) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
						Fecha = invoice.Fecha,
						CuentaContable = Invoice.ModulePrincipal.GetCuentaHaciendaSetting(),
						Importe = importe,
						Libre1 = lr.IDExportacion,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, invoice.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PAGO, invoice.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}
        }

        /// <summary>
        /// FORMATO: 01/01/2007;4300000016;;;1;2;Factura No.  1;1;22475;166.386; 135.62;0;1;22475
        /// </summary>
        /// <param name="invoice"></param>
		protected override void BuildOutputInvoiceAccountingEntry(OutputInvoiceInfo invoice, LineaRegistro lr)
        {
			try
			{
				ClienteInfo titular = _clients.GetItem(invoice.OidCliente);

				if (titular.CuentaContable == string.Empty)
					throw new iQException(string.Format(Resources.Messages.PROVIDER_BOOK_ACCOUNT_NOT_FOUND, titular.NumeroClienteLabel, titular.Nombre), new object[2] { ETipoEntidad.Cliente, titular.Oid });
				else if (titular.CuentaContable == Resources.Defaults.NO_CONTABILIZAR)
					return;

				string apunte = string.Empty;
				string descripcion = "Fra. " + invoice.NFactura + " (" + invoice.Cliente + ")";

				decimal importe = invoice.Total;

				BuildAccountingLine(new ApunteContaWin
							{
								Tipo = ETipoApunte.General,
								Columna = importe > 0 ? EColumnaApunte.Debe : EColumnaApunte.Haber,
								Fecha = invoice.Fecha,
								CuentaContable = titular.CuentaContable,
								Importe = importe,
								Libre1 = lr.IDExportacion,
								Diario = _config.Diario,
								Asiento = _accounting_entry,
								Descripcion = descripcion,
							});
                
				List<CuentaResumen> cuentas = invoice.GetCuentas();
				FamiliaInfo familia;

				foreach (CuentaResumen cr in cuentas)
				{
					apunte = string.Empty;

					familia = _families.GetItem(cr.OidFamilia);

					if (familia == null) throw new iQException("Factura " + invoice.NFactura + " con familia a nulo");

					if (cr.CuentaContable == string.Empty)
						throw new iQException("La familia nº " + familia.Codigo + " (" + familia.Nombre + ") no tiene cuenta contable (venta) asociada", new object[2] { ETipoEntidad.Familia, familia.Oid });

					importe = -cr.Importe;

					BuildAccountingLine(new ApunteContaWin
								{
									Tipo = ETipoApunte.General,
									Columna = (importe > 0) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
									Fecha = invoice.Fecha,
									CuentaContable = cr.CuentaContable,
									Importe = importe,
									Libre1 = lr.IDExportacion,
									Diario = _config.Diario,
									Asiento = _accounting_entry,
									Descripcion = descripcion,
								});

				}

				// Apuntes en las cuentas de Impuestos

				List<ImpuestoResumen> taxes = new List<ImpuestoResumen>();

				foreach (DictionaryEntry impuesto in invoice.GetImpuestos())
					taxes.Add((ImpuestoResumen)impuesto.Value);

				foreach (ImpuestoResumen ir in taxes)
				{
					if (ir.Importe == 0) continue;

					apunte = string.Empty;

					ImpuestoInfo tax = _taxes.GetItem(ir.OidImpuesto);

					if (tax == null) throw new iQException("Factura " + invoice.NFactura + " con impuesto a nulo");

					if (tax.CuentaContableSoportado == string.Empty)
						throw new iQException("El impuesto '" + tax.Nombre + "' no tiene cuenta contable (repercutido) asociada", new object[2] { ETipoEntidad.Impuesto, tax.Oid });

					importe = -ir.Importe;
					
					BuildAccountingLine(new ApunteContaWin
								{
									Tipo = ETipoApunte.General,
									Columna = (importe > 0) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
									Fecha = invoice.Fecha,
									CuentaContable = tax.CuentaContableRepercutido,
                                    Importe = importe,
                                    Libre1 = lr.IDExportacion,
									Diario = _config.Diario,
									Asiento = _accounting_entry,
									Descripcion = descripcion,
								});
				}
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_FACTURA, invoice.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_FACTURA, invoice.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}
        }

		/// <summary>
		/// FORMATO: 01/01/2007;4300000016;;;1;2;Factura No.  1;1;22475;166.386; 135.62;0;1;22475
		/// </summary>
		/// <param name="factura"></param>
		protected override void BuildInvoicePaymentAccountingEntry(PaymentInfo payment, LineaRegistro lr)
		{
			try
			{
				if (payment.ETipoPago != ETipoPago.Factura) return;

				IAcreedorInfo titular = _providers.GetItem(payment.OidAgente, payment.ETipoAcreedor);
				InputInvoiceInfo factura = null;

				if (titular.CuentaContable == string.Empty)
					throw new iQException(string.Format(Resources.Messages.PROVIDER_BOOK_ACCOUNT_NOT_FOUND, titular.Codigo, titular.Nombre), new object[2] { Store.EnumConvert.ToETipoEntidad(titular.ETipoAcreedor), titular.OidAcreedor });

				string descripcion = string.Empty;
				decimal importe_pago = 0;
				decimal importe_factura = 0;
				bool is_debe = true;

                foreach (TransactionPaymentInfo pf in payment.Operations)
				{
                    factura = _input_invoices.GetItem(pf.OidOperation);
					descripcion = string.Format(Resources.Messages.APUNTE_PAGO_FACTURA, factura.NFactura, factura.Acreedor);

					//Seleccionamos DEBE o HABER en funcion del importe de la factura
					if (pf.Cantidad > 0)
					{
						is_debe = true;
						importe_factura = pf.Cantidad;
					}
					else
					{
						is_debe = false;
						//ES NEGATIVO CUANDO VA AL HABER POR EXIGENCIA DE CONTAWIN
						importe_factura = pf.Cantidad;
					}

					//apunte en la cuenta del acreedor
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = (is_debe) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
						Fecha = payment.Fecha,
						CuentaContable = titular.CuentaContable,
						Importe = importe_factura,
						Libre1 = lr.IDExportacion,
						Libre2 = payment.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}

				descripcion = String.Format(Resources.Messages.APUNTE_PAGO, titular.Nombre);

				//Sumamos los gastos bancarios
				importe_pago = payment.Importe + payment.GastosBancarios;

				//Seleccionamos DEBE o HABER en funcion del importe deL PAGO
				if (importe_pago < 0)
				{
					is_debe = true;
					importe_pago = -importe_pago;
				}
				else
				{
					is_debe = false;
					//ES NEGATIVO CUANDO VA AL HABER POR EXIGENCIA DE CONTAWIN
					importe_pago = -importe_pago;
				}

				//Apunte en la cuenta del pago
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.General,
					Columna = (is_debe) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
					Fecha = payment.Fecha,
					CuentaContable = GetPaymentAccount(payment),
					Importe = importe_pago,
					Libre1 = lr.IDExportacion,
					Libre2 = payment.Codigo,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
                });

                //Si no se pone en la contabilidad hay un descuadre
                if (payment.Pendiente > 0)
                {
                    importe_pago = payment.Pendiente;

                    descripcion = "Pago a cuenta del proveedor: " + titular.Nombre;

                    //apunte en la cuenta del proveedor
                    BuildAccountingLine(new ApunteContaWin
                    {
                        Tipo = ETipoApunte.General,
                        Columna = EColumnaApunte.Debe,
                        Fecha = payment.Fecha,
                        CuentaContable = titular.CuentaContable,
                        Importe = importe_pago,
                        Libre1 = lr.IDExportacion,
                        Libre2 = payment.Codigo,
                        Diario = _config.Diario,
                        Asiento = _accounting_entry,
                        Descripcion = descripcion,
                    });
                }

				if (payment.GastosBancarios != 0)
				{
					descripcion = String.Format(Resources.Messages.APUNTE_GASTOS_BANCARIOS, payment.Codigo + " (" + titular.Nombre + ")");

					//apunte en la cuenta de gastos bancarios
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = EColumnaApunte.Debe,
						Fecha = payment.Fecha,
						CuentaContable = GetBankExpensesAccount(payment),
						Importe = payment.GastosBancarios,
						Libre1 = lr.IDExportacion,
						Libre2 = payment.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}
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
		protected override void BuildPayrollPaymentAccountingEntry(PaymentInfo payment, LineaRegistro lr)
		{
			try
			{
				if (payment.ETipoPago != ETipoPago.Nomina) return;

				string apunte = string.Empty;
				string descripcion = string.Empty;
				decimal importe_pago = 0;

				foreach (ExpenseInfo item in payment.Gastos)
				{
					EmployeeInfo employee = _employees.GetItem(item.OidEmpleado);

					if (employee.CuentaContable == string.Empty)
						throw new iQException("El empleado nº " + employee.Codigo + " (" + employee.NombreCompleto + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Empleado, employee.OidAcreedor });

					descripcion = employee.NombreCompleto;

					decimal importe = item.Asignado;

					//apunte en la cuenta del acreedor
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = EColumnaApunte.Debe,
						Fecha = payment.Fecha,
						CuentaContable = employee.CuentaContable,
						Importe = importe,
						Libre1 = lr.IDExportacion,
						Libre2 = payment.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}

                foreach (TransactionPaymentInfo item in payment.Operations)
                {
                    EmployeeInfo empleado = _employees.GetItem(payment.OidAgente);

                    if (empleado.CuentaContable == string.Empty)
                        throw new iQException("El empleado nº " + empleado.Codigo + " (" + empleado.NombreCompleto + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Empleado, empleado.OidAcreedor });

                    descripcion = empleado.NombreCompleto;

                    decimal importe = item.Cantidad;

                    //apunte en la cuenta del empleado
                    BuildAccountingLine(new ApunteContaWin
                    {
                        Tipo = ETipoApunte.General,
                        Columna = EColumnaApunte.Debe,
                        Fecha = payment.Fecha,
                        CuentaContable = empleado.CuentaContable,
                        Importe = importe,
                        Libre1 = lr.IDExportacion,
                        Libre2 = payment.Codigo,
                        Diario = _config.Diario,
                        Asiento = _accounting_entry,
                        Descripcion = descripcion,
                    });
                }

				//Sumamos los gastos bancarios
				importe_pago += payment.Importe + payment.GastosBancarios;
				descripcion = String.Format(Resources.Messages.APUNTE_REMESA_NOMINA, payment.Codigo);

				//apunte en la cuenta del pago
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.General,
					Columna = EColumnaApunte.Haber,
					Fecha = payment.Fecha,
					CuentaContable = GetPaymentAccount(payment),
					Importe = -importe_pago,
					Libre1 = lr.IDExportacion,
					Libre2 = payment.Codigo,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});

                //Si no se pone en la contabilidad hay un descuadre
                if (payment.Pendiente > 0)
                {
                    importe_pago = payment.Pendiente;

                    EmployeeInfo empleado = _employees.GetItem(payment.OidAgente);

                    if (empleado.CuentaContable == string.Empty)
                        throw new iQException("El empleado nº " + empleado.Codigo + " (" + empleado.NombreCompleto + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Empleado, empleado.OidAcreedor });

                    descripcion = "Pago a cuenta del empleado: " + empleado.NombreCompleto;

                    //apunte en la cuenta del empleado
                    BuildAccountingLine(new ApunteContaWin
                    {
                        Tipo = ETipoApunte.General,
                        Columna = EColumnaApunte.Debe,
                        Fecha = payment.Fecha,
                        CuentaContable = empleado.CuentaContable,
                        Importe = importe_pago,
                        Libre1 = lr.IDExportacion,
                        Libre2 = payment.Codigo,
                        Diario = _config.Diario,
                        Asiento = _accounting_entry,
                        Descripcion = descripcion,
                    });
                }

				if (payment.GastosBancarios != 0)
				{
					apunte = string.Empty;

					descripcion = String.Format(Resources.Messages.APUNTE_GASTOS_BANCARIOS, payment.Codigo);

					//apunte en la cuenta de gastos bancarios
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = EColumnaApunte.Debe,
						Fecha = payment.Fecha,
						CuentaContable = GetBankExpensesAccount(payment),
						Importe = payment.GastosBancarios,
						Libre1 = lr.IDExportacion,
						Libre2 = payment.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}
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
		protected override void BuildExpensePaymentAccountingEntry(PaymentInfo payment, LineaRegistro lr)
		{
			try
			{
				if (payment.ETipoPago != ETipoPago.Gasto) return;

				string descripcion = string.Empty;
				decimal importe_pago = 0;

				foreach (ExpenseInfo item in payment.Gastos)
				{
					TipoGastoInfo tipoGasto = _expense_types.GetItem(item.OidTipo);

					if (tipoGasto.CuentaContable == Resources.Defaults.NO_CONTABILIZAR) return;

					if (tipoGasto.CuentaContable == string.Empty)
						throw new iQException("El tipo de gasto nº " + tipoGasto.Codigo + " (" + tipoGasto.Nombre + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.TipoGasto, tipoGasto.Oid });

					descripcion = item.Descripcion;

					decimal importe = item.Asignado;

					//apunte en la cuenta del gasto
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = EColumnaApunte.Debe,
						Fecha = payment.Fecha,
						CuentaContable = tipoGasto.CuentaContable,
						Importe = importe,
						Libre1 = lr.IDExportacion,
						Libre2 = payment.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}

				//Sumamos los gastos bancarios
				importe_pago += payment.Importe + payment.GastosBancarios;
				descripcion = String.Format(Resources.Messages.APUNTE_PAGO_GASTO, payment.Codigo);

				//apunte en la cuenta del pago
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.General,
					Columna = EColumnaApunte.Haber,
					Fecha = payment.Fecha,
					CuentaContable = GetPaymentAccount(payment),
					Importe = -importe_pago,
					Libre1 = lr.IDExportacion,
					Libre2 = payment.Codigo,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});

				if (payment.GastosBancarios != 0)
				{
					descripcion = String.Format(Resources.Messages.APUNTE_GASTOS_BANCARIOS, payment.Codigo);

					//apunte en la cuenta de gastos bancarios
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = EColumnaApunte.Debe,
						Fecha = payment.Fecha,
						CuentaContable = GetBankExpensesAccount(payment),
						Importe = payment.GastosBancarios,
						Libre1 = lr.IDExportacion,
						Libre2 = payment.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}
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
        protected override void BuildCreditCardStatementPaymentAccountingEntry(PaymentInfo payment, LineaRegistro lr)
        {
            try
            {
                if (payment.ETipoPago != ETipoPago.ExtractoTarjeta) return;

                  //DEBE: Cuenta de la tarjeta de crédito

                //Se suma el total del pago y los gastos asociados
                decimal payment_amount = payment.Importe + payment.GastosBancarios;
                string description = String.Format(Resources.Messages.APUNTE_PAGO_EXTRACTO_TARJETA, payment.Codigo);

                //apunte en la cuenta del pago
                BuildAccountingLine(new ApunteContaWin
                {
                    Tipo = ETipoApunte.General,
                    Columna = EColumnaApunte.Debe,
                    Fecha = payment.Fecha,
                    CuentaContable = GetCreditCardAccount(payment, ETipoEntidad.TarjetaCredito),
                    Importe = payment_amount,
                    Libre1 = lr.IDExportacion,
                    Libre2 = payment.Codigo,
                    Diario = _config.Diario,
                    Asiento = _accounting_entry,
                    Descripcion = description,
                });

                //HABER: Cuenta del banco asociada a la tarjeta de crédito

                //apunte en la cuenta del banco
                BuildAccountingLine(new ApunteContaWin
                {
                    Tipo = ETipoApunte.General,
                    Columna = EColumnaApunte.Haber,
                    Fecha = payment.Fecha,
                    CuentaContable = GetCreditCardAccount(payment, ETipoEntidad.CuentaBancaria),
                    Importe = -payment_amount,
                    Libre1 = lr.IDExportacion,
                    Libre2 = payment.Codigo,
                    Diario = _config.Diario,
                    Asiento = _accounting_entry,
                    Descripcion = description,
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

		/// <summary>
		/// FORMATO: 01/01/2007;4300000016;;;1;2;Factura No.  1;1;22475;166.386; 135.62;0;1;22475
		/// </summary>
		/// <param name="factura"></param>
		protected override void BuildChargeAccountingEntry(ChargeInfo charge, LineaRegistro lr)
		{
			try
			{
				OutputInvoiceInfo factura;
				ClienteInfo titular = _clients.GetItem(charge.OidCliente);

				if (titular.CuentaContable == Resources.Defaults.NO_CONTABILIZAR) return;

				string descripcion = string.Empty;

				if (titular.CuentaContable == string.Empty)
					throw new iQException(string.Format(Resources.Messages.PROVIDER_BOOK_ACCOUNT_NOT_FOUND, titular.NumeroClienteLabel, titular.Nombre), new object[2] { ETipoEntidad.Cliente, titular.Oid });
				else if (titular.CuentaContable == Resources.Defaults.NO_CONTABILIZAR)
					return;

				decimal importe_factura = 0;
				decimal importe_cobro = 0;
				bool is_debe = true;

				foreach (CobroFacturaInfo cf in charge.CobroFacturas)
				{
					factura = _invoices.GetItem(cf.OidFactura);
					descripcion = "Cobro Fra. " + factura.NFactura + " (" + factura.Cliente + ")";

					//Seleccionamos DEBE o HABER en funcion del importe de la factura
					if (cf.Cantidad < 0)
					{
						is_debe = true;
						importe_factura = -cf.Cantidad;
					}
					else
					{
						is_debe = false;
						//ES NEGATIVO CUANDO VA AL HABER POR EXIGENCIA DE CONTAWIN
						importe_factura = -cf.Cantidad;
					}

					//apunte en la cuenta del cliente
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = (is_debe) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
						Fecha = charge.Fecha,
						CuentaContable = titular.CuentaContable,
						Importe = importe_factura,
						Libre1 = lr.IDExportacion,
						Libre2 = charge.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}

				descripcion = String.Format(Resources.Messages.APUNTE_COBRO, titular.Nombre);

				//Sumamos los gastos bancarios
                //No se tienen en cuenta los gastos porque se pasan en una línea que se introduce de forma manual
                //Esto es así porque el banco no siempre los cobra en el momento de la transferencia, puede agrupar varios...
                importe_cobro = charge.Importe;// -cobro.GastosBancarios;

				//Seleccionamos DEBE o HABER en funcion del importe del cobro
				is_debe = importe_cobro > 0;

				//apunte en la cuenta del cobro
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.General,
					Columna = (is_debe) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
					Fecha = charge.Fecha,
					CuentaContable = GetChargeAccount(charge),
					Importe = importe_cobro,
					Libre1 = lr.IDExportacion,
					Libre2 = charge.Codigo,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
                });

                //Si no se pone en la contabilidad hay un descuadre
                if (charge.Pendiente > 0)
                {
                    importe_cobro = -charge.Pendiente;

                    descripcion = "Cobro a cuenta del cliente: " + titular.Nombre;

                    //apunte en la cuenta del proveedor
                    BuildAccountingLine(new ApunteContaWin
                    {
                        Tipo = ETipoApunte.General,
                        Columna = EColumnaApunte.Haber,
                        Fecha = charge.Fecha,
                        CuentaContable = titular.CuentaContable,
                        Importe = importe_cobro,
                        Libre1 = lr.IDExportacion,
                        Libre2 = charge.Codigo,
                        Diario = _config.Diario,
                        Asiento = _accounting_entry,
                        Descripcion = descripcion,
                    });
                }

                //Al no tener en cuenta los gastos en el apunte que va en la cuenta del cobro, tampco se añade el apunte en la cuenta de gastos
				/*if (cobro.GastosBancarios != 0)
				{
					descripcion = String.Format(Resources.Messages.APUNTE_GASTOS_BANCARIOS, cobro.Codigo + " (" + titular.Nombre + ")");

					//apunte en la cuenta de gastos bancarios
					BuildApunte(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = EColumnaApunte.Debe,
						Fecha = cobro.Fecha,
						CuentaContable = GetCuentaGastosBancarios(cobro),
						Importe = cobro.GastosBancarios,
						Libre1 = lr.IDExportacion,
						Libre2 = cobro.Codigo,
						Diario = _config.Diario,
						Asiento = _asiento,
						Descripcion = descripcion,
					});
				}*/
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_COBRO, charge.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}		
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_COBRO, charge.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}
		}
		protected override void BuildREAChargeAccountingEntry(ChargeInfo charge, LineaRegistro lr)
		{
			try
			{
				string descripcion = string.Empty;
				decimal importe_cobro = 0;
				bool is_debe = true;

				foreach (CobroREAInfo cf in charge.CobroREAs)
				{
					ExpedientInfo expediente = _expedients.GetItem(cf.OidExpediente);

					string cuentaREA = GetGrantAccount(ETipoAyuda.REA, expediente);

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
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = (is_debe) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
						Fecha = charge.Fecha,
						CuentaContable = cuentaREA,
						Importe = importe_cobro,
						Libre1 = lr.IDExportacion,
						Libre2 = charge.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}

				descripcion = String.Format(Resources.Messages.APUNTE_COBRO, "Ayuda REA");

				//Restamos los gastos bancarios
                //En realidad debería crearse otra línea para los gastos que iría en el haber pero se unifica, por eso está restando
				importe_cobro = charge.Importe - charge.GastosBancarios;

				//Seleccionamos DEBE o HABER en funcion del importe del cobro
				is_debe = importe_cobro > 0;

				//apunte en la cuenta del cobro
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.General,
					Columna = (is_debe) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
					Fecha = charge.Fecha,
					CuentaContable = GetChargeAccount(charge),
					Importe = importe_cobro,
					Libre1 = lr.IDExportacion,
					Libre2 = charge.Codigo,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});

				if (charge.GastosBancarios != 0)
				{
					descripcion = String.Format(Resources.Messages.APUNTE_GASTOS_BANCARIOS, charge.Codigo + " (Ayuda REA)");

					//apunte en la cuenta de gastos bancarios
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = EColumnaApunte.Debe,
						Fecha = charge.Fecha,
                        CuentaContable = GetBankExpensesAccount(charge),
						Importe = charge.GastosBancarios,
						Libre1 = lr.IDExportacion,
						Libre2 = charge.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_COBRO, charge.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_COBRO, charge.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}
		}

		/// <summary>
		/// FORMATO: 01/01/2007;4300000016;;;1;2;Factura No.  1;1;22475;166.386; 135.62;0;1;22475
		/// </summary>
		/// <param name="factura"></param>
		protected override void BuildREAGrantAccountingEntry(ExpedienteREAInfo grant, LineaRegistro lr)
		{
			try
			{
				string descripcion = string.Empty;
				decimal importe_cobro = 0;
				bool is_debe = true;
				descripcion = "Ayuda REA del Exp. " + grant.CodigoExpediente;

				ExpedientInfo expediente = _expedients.GetItem(grant.OidExpediente);
				string cuentaREA = GetGrantAccount(ETipoAyuda.REA, expediente);

				//Seleccionamos DEBE o HABER en funcion del importe del cobro
                if (grant.AyudaCobrada < 0)
                    is_debe = false;

                importe_cobro = grant.AyudaCobrada;

				//apunte en la cuenta del cliente
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.General,
					Columna = (is_debe) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
					Fecha = grant.Fecha,
					CuentaContable = cuentaREA,
					Importe = importe_cobro,
					Libre1 = lr.IDExportacion,
					Libre2 = grant.Codigo,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});

				descripcion = "Ayuda REA del Exp. " + grant.CodigoExpediente;
                importe_cobro = -importe_cobro;

				//apunte en la cuenta del cobro
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.General,
					Columna = (is_debe) ? EColumnaApunte.Haber : EColumnaApunte.Debe,
					Fecha = grant.Fecha,
					CuentaContable = ModulePrincipal.GetCuentaSubvencionesSetting(),
					Importe = importe_cobro,
					Libre1 = lr.IDExportacion,
					Libre2 = grant.Codigo,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_AYUDA_REA, grant.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_AYUDA_REA, grant.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}
		}

		/// <summary>
		/// FORMATO: 01/01/2007;4300000016;;;1;2;Factura No.  1;1;22475;166.386; 135.62;0;1;22475
		/// </summary>
		/// <param name="factura"></param>
		protected override void BuildFomentoGrantAccountingEntry(LineaFomentoInfo grant, LineaRegistro lr)
		{
			try
			{
				string descripcion = string.Empty;
				decimal importe_cobro = 0;
				bool is_debe = true;
				descripcion = "Ayuda FOMENTO del Exp. " + grant.IDExpediente;

				ExpedientInfo expediente = _expedients.GetItem(grant.OidExpediente);
				string cuentaREA = GetGrantAccount(ETipoAyuda.Fomento, expediente);

				//Seleccionamos DEBE o HABER en funcion del importe del cobro
				if (grant.Subvencion < 0)
				{
					is_debe = true;
					importe_cobro = -grant.Subvencion;
				}
				else
				{
					is_debe = false;
					//ES NEGATIVO CUANDO VA AL HABER POR EXIGENCIA DE CONTAWIN
					importe_cobro = -grant.Subvencion;
				}

				//apunte en la cuenta de la REA
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.General,
					Columna = (is_debe) ? EColumnaApunte.Debe : EColumnaApunte.Haber,
					Fecha = grant.FechaConocimiento,
					CuentaContable = cuentaREA,
					Importe = importe_cobro,
					Libre1 = lr.IDExportacion,
					Libre2 = grant.Codigo,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});

				descripcion = "Ayuda FOMENTO del Exp. " + grant.IDExpediente;

				//apunte en la cuenta de la subvencion
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.General,
					Columna = (is_debe) ? EColumnaApunte.Haber : EColumnaApunte.Debe,
					Fecha = grant.FechaConocimiento,
					CuentaContable = ModulePrincipal.GetCuentaSubvencionesSetting(),
					Importe = -importe_cobro,
					Libre1 = lr.IDExportacion,
					Libre2 = grant.Codigo,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_AYUDA_FOMENTO, grant.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_AYUDA_FOMENTO, grant.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}
		}

		/// <summary>
		/// FORMATO: 01/01/2007;4300000016;;;1;2;Factura No.  1;1;22475;166.386; 135.62;0;1;22475
		/// </summary>
		/// <param name="factura"></param>
		protected override void BuildExpenseAccountingEntry(PaymentInfo payment, LineaRegistro lr)
		{
			try
			{
                if (payment.EEstadoPago != EEstado.Pagado) return;

				IAcreedorInfo titular = _providers.GetItem(payment.OidAgente, payment.ETipoAcreedor);
				//MovimientoBancoInfo mov = _movs.GetItemByOperacion(pago.Oid, EBankLineType.Pago);
				InputInvoiceInfo factura = null;

				if (titular.CuentaContable == string.Empty)
					throw new iQException("El deudor nº " + titular.Codigo + " (" + titular.Nombre + ") no tiene cuenta contable asociada", new object[2] { Store.EnumConvert.ToETipoEntidad(titular.ETipoAcreedor), titular.Oid });

				string apunte = string.Empty;
				string descripcion = string.Empty;
				int pos = 1;

                foreach (TransactionPaymentInfo pf in payment.Operations)
				{
                    factura = _input_invoices.GetItem(pf.OidOperation);
					descripcion = "Pago Fra. " + factura.NFactura + " (" + factura.Acreedor + ")";

					//Sumamos los gastos bancarios solo al primer pagofactura 
					decimal importe = (pos++ == 1) ? pf.Cantidad + payment.GastosBancarios : pf.Cantidad;

					//apunte en la cuenta del acreedor
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = EColumnaApunte.Debe,
						Fecha = payment.Fecha,
						CuentaContable = titular.CuentaContable,
						Importe = importe,
						Libre1 = lr.IDExportacion,
						Libre2 = payment.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});

					apunte = string.Empty;

					//apunte en la cuenta del pago
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = EColumnaApunte.Haber,
						Fecha = payment.Fecha,
						CuentaContable = GetPaymentAccount(payment),
						Importe = -pf.Cantidad,
						Libre1 = lr.IDExportacion,
						Libre2 = payment.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}

				if (payment.GastosBancarios != 0)
				{
					apunte = string.Empty;

					descripcion = "Gtos. Banco. " + payment.Codigo + " (" + titular.Nombre + ")";

					//apunte en la cuenta de gastos bancarios
					BuildAccountingLine(new ApunteContaWin
					{
						Tipo = ETipoApunte.General,
						Columna = EColumnaApunte.Haber,
						Fecha = payment.Fecha,
						CuentaContable = GetBankExpensesAccount(payment),
						Importe = -payment.GastosBancarios,
						Libre1 = lr.IDExportacion,
						Libre2 = payment.Codigo,
						Diario = _config.Diario,
						Asiento = _accounting_entry,
						Descripcion = descripcion,
					});
				}
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_GASTO, payment.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_GASTO, payment.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}
		}

		/// <summary>
		/// FORMATO: 01/01/2007;4300000016;;;1;2;Factura No.  1;1;22475;166.386; 135.62;0;1;22475
		/// </summary>
		/// <param name="factura"></param>
		protected override void BuildPayrollBatchAccountingEntry(PayrollBatchInfo payrollBatch, LineaRegistro lr)
		{
			try
			{
				//if (!pago.Pagado) return;

				string apunte = string.Empty;
				string descripcion = string.Empty;
				string cuenta = string.Empty;
				
				//Calculo del importe del apunte 
				decimal importe = payrollBatch.Neto + payrollBatch.IRPF + payrollBatch.SeguroPersonal;

				descripcion = payrollBatch.Descripcion;
				cuenta = Invoice.ModulePrincipal.GetCuentaNominasSetting();

				//apunte en la cuenta de nóminas
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.General,
					Columna = EColumnaApunte.Debe,
					Fecha = payrollBatch.Fecha,
					CuentaContable = Invoice.ModulePrincipal.GetCuentaNominasSetting(),
					Importe = importe,
					Libre1 = lr.IDExportacion,
					Libre2 = payrollBatch.Codigo,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});
				
				//Calculo del importe del apunte 
				importe = payrollBatch.SeguroEmpresa;

				descripcion = "Seg. Social a cargo de la empresa";
				cuenta = Invoice.ModulePrincipal.GetCuentaSegurosSocialesSetting();

				//apunte en la cuenta de la seguridad social
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.General,
					Columna = EColumnaApunte.Debe,
					Fecha = payrollBatch.Fecha,
					CuentaContable = Invoice.ModulePrincipal.GetCuentaSegurosSocialesSetting(),
					Importe = importe,
					Libre1 = lr.IDExportacion,
					Libre2 = payrollBatch.Codigo,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});

				TipoGastoInfo tipo = null;
				EmployeeInfo empleado = null;

				foreach (ExpenseInfo item in payrollBatch.Gastos)
				{
					tipo = _expense_types.GetItem(item.OidTipo);
					apunte = string.Empty;

					switch (item.ECategoriaGasto)
					{
						case ECategoriaGasto.SeguroSocial:
							{
								if (tipo.CuentaContable == string.Empty)
									throw new iQException("El tipo de gasto nº " + tipo.Codigo + " (" + tipo.Nombre + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.TipoGasto, tipo.Oid });

								descripcion = "SEGUROS SOCIALES " + payrollBatch.Fecha.ToString("MMMM - yyyy").ToUpper();

								//Calculo del importe del apunte 
								importe = item.Total;

								//apunte en la cuenta de la Hacienda Pública
								BuildAccountingLine(new ApunteContaWin
								{
									Tipo = ETipoApunte.General,
									Columna = EColumnaApunte.Haber,
									Fecha = payrollBatch.Fecha,
									CuentaContable = tipo.CuentaContable,
									Importe = -importe,
									Libre1 = lr.IDExportacion,
									Libre2 = item.Codigo,
									Diario = _config.Diario,
									Asiento = _accounting_entry,
									Descripcion = descripcion,
								});
							}
							break;

						case ECategoriaGasto.Impuesto:
							{
								if (tipo.CuentaContable == string.Empty)
									throw new iQException("El tipo de gasto nº " + tipo.Codigo + " (" + tipo.Nombre + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.TipoGasto, tipo.Oid });

								descripcion = "HACIENDA PUBLICA ACREEDORA " + payrollBatch.Fecha.ToString("MMMM - yyyy").ToUpper();

								//Calculo del importe del apunte 
								importe = item.Total;

								//apunte en la cuenta de la Hacienda Pública
								BuildAccountingLine(new ApunteContaWin
								{
									Tipo = ETipoApunte.General,
									Columna = EColumnaApunte.Haber,
									Fecha = payrollBatch.Fecha,
									CuentaContable = tipo.CuentaContable,
									Importe = -importe,
									Libre1 = lr.IDExportacion,
									Libre2 = item.Codigo,
									Diario = _config.Diario,
									Asiento = _accounting_entry,
									Descripcion = descripcion,
								});
							}
							break;

						case ECategoriaGasto.Nomina:
							{
								empleado = _employees.GetItem(item.OidEmpleado);
								descripcion = "NOMINA (" + empleado.NombreCompleto + ")";

								if (empleado.CuentaContable == string.Empty)
									throw new iQException("El empleado nº " + empleado.Codigo + " (" + empleado.NombreCompleto + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Empleado, empleado.Oid });

								//Calculo del importe del apunte 
								importe = item.Total;

								//apunte en la cuenta del empleado
								BuildAccountingLine(new ApunteContaWin
								{
									Tipo = ETipoApunte.General,
									Columna = EColumnaApunte.Haber,
									Fecha = payrollBatch.Fecha,
									CuentaContable = empleado.CuentaContable,
									Importe = -importe,
									Libre1 = lr.IDExportacion,
									Libre2 = item.Codigo,
									Diario = _config.Diario,
									Asiento = _accounting_entry,
									Descripcion = descripcion,
								});
							}
							break;
					}
                } 
                
                foreach (NominaInfo item in payrollBatch.Nominas)
                {
                    tipo = _expense_types.GetItem(item.OidTipo);
                    apunte = string.Empty;

                    empleado = _employees.GetItem(item.OidEmpleado);
                    descripcion = "NOMINA (" + empleado.NombreCompleto + ")";

                    if (empleado.CuentaContable == string.Empty)
                        throw new iQException("El empleado nº " + empleado.Codigo + " (" + empleado.NombreCompleto + ") no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Empleado, empleado.Oid });

                    //Calculo del importe del apunte 
                    importe = item.Total;

                    //apunte en la cuenta del empleado
                    BuildAccountingLine(new ApunteContaWin
                    {
                        Tipo = ETipoApunte.General,
                        Columna = EColumnaApunte.Haber,
                        Fecha = payrollBatch.Fecha,
                        CuentaContable = empleado.CuentaContable,
                        Importe = -importe,
                        Libre1 = lr.IDExportacion,
                        Libre2 = item.Codigo,
                        Diario = _config.Diario,
                        Asiento = _accounting_entry,
                        Descripcion = descripcion,
                    });
                }
			}
			catch (iQException ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_NOMINA, payrollBatch.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
			}
			catch (Exception ex)
			{
				throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_NOMINA, payrollBatch.Codigo, iQExceptionHandler.GetAllMessages(ex)));
			}
		}

        protected override void BuildBankTransferAccountingEntry(TraspasoInfo bankTransfer, LineaRegistro lr)
        {
            try
            {
                BankAccountInfo cuentaO = _bank_accounts.GetItem(bankTransfer.OidCuentaOrigen);

                if (cuentaO.CuentaContable == string.Empty)
                    throw new iQException("La cuenta bancaria " + cuentaO.Entidad + " no tiene cuenta contable asociada", new object[2] { ETipoEntidad.CuentaBancaria, cuentaO.Oid });

                BankAccountInfo cuentaD = _bank_accounts.GetItem(bankTransfer.OidCuentaDestino);

                if (cuentaD.CuentaContable == string.Empty)
                    throw new iQException("La cuenta bancaria " + cuentaD.Entidad + " no tiene cuenta contable asociada", new object[2] { ETipoEntidad.CuentaBancaria, cuentaD.Oid });

                //Apunte de la salida de la cuenta de origen
                BuildAccountingLine(new ApunteContaWin
                {
                    Tipo = ETipoApunte.General,
                    Columna = bankTransfer.Importe < 0 ? EColumnaApunte.Debe : EColumnaApunte.Haber,
                    Fecha = bankTransfer.Fecha,
                    CuentaContable = cuentaO.CuentaContable,
                    Importe = -bankTransfer.Importe,
                    Libre1 = lr.IDExportacion,
                    Libre2 = bankTransfer.Codigo,
                    Diario = _config.Diario,
                    Asiento = _accounting_entry,
                    Descripcion = "Traspaso " + bankTransfer.Codigo + ": " + cuentaO.Entidad + " a " + cuentaD.Entidad
                });

                //Apunte de entrada a la cuenta destino
                BuildAccountingLine(new ApunteContaWin
                {
                    Tipo = ETipoApunte.General,
                    Columna = bankTransfer.Importe < 0 ? EColumnaApunte.Haber : EColumnaApunte.Debe,
                    Fecha = bankTransfer.Fecha,
                    CuentaContable = cuentaD.CuentaContable,
                    Importe = bankTransfer.Importe,
                    Libre1 = lr.IDExportacion,
                    Libre2 = bankTransfer.Codigo,
                    Diario = _config.Diario,
                    Asiento = _accounting_entry,
                    Descripcion = "Traspaso " + bankTransfer.Codigo + ": " + cuentaO.Entidad + " a " + cuentaD.Entidad
                });

                if (bankTransfer.GastosBancarios != 0)
                {
                    //Debe en la cuenta contable de gastos del banco de origen
                    BuildAccountingLine(new ApunteContaWin
                    {
                        Tipo = ETipoApunte.General,
                        Columna = bankTransfer.GastosBancarios < 0 ? EColumnaApunte.Haber : EColumnaApunte.Debe,
                        Fecha = bankTransfer.Fecha,
                        CuentaContable = cuentaO.CuentaContableGastos,
                        Importe = bankTransfer.GastosBancarios,
                        Libre1 = lr.IDExportacion,
                        Libre2 = bankTransfer.Codigo,
                        Diario = _config.Diario,
                        Asiento = _accounting_entry,
                        Descripcion = "Gastos Bancarios del Traspaso " + bankTransfer.Codigo
                    });


                    //Haber en la cuenta contable del banco de origen
                    BuildAccountingLine(new ApunteContaWin
                    {
                        Tipo = ETipoApunte.General,
                        Columna = bankTransfer.GastosBancarios < 0 ? EColumnaApunte.Debe : EColumnaApunte.Haber,
                        Fecha = bankTransfer.Fecha,
                        CuentaContable = cuentaO.CuentaContable,
                        Importe = -bankTransfer.GastosBancarios,
                        Libre1 = lr.IDExportacion,
                        Libre2 = bankTransfer.Codigo,
                        Diario = _config.Diario,
                        Asiento = _accounting_entry,
                        Descripcion = "Gastos Bancarios del Traspaso " + bankTransfer.Codigo
                    });

                }
            }
            catch (iQException ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_TRASPASO, bankTransfer.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
            }
            catch (Exception ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_TRASPASO, bankTransfer.Codigo, iQExceptionHandler.GetAllMessages(ex)));
            }
        }
        
		/// <summary>
		/// FORMATO: 4300000016;Juan Alberto Acosta Guill;B55223655;1;19375;;;-2;4770000001;S;01/01/2007;;16;0;22475;3100;0;16;01/01/2007;0;0;0;C
		/// </summary>
		/// <param name="invoice"></param>
		protected override void BuildTaxBookSoportadoAccountingEntry(InputInvoiceInfo invoice)
		{
			List<ImpuestoResumen> impuestos = invoice.GetImpuestos(true);
			IAcreedorInfo titular = _providers.GetItem(invoice.OidAcreedor, invoice.ETipoAcreedor);
			string apunte;

			foreach (ImpuestoResumen ir in impuestos)
			{
				apunte = string.Empty;

				ImpuestoInfo impuesto = _taxes.GetItem(ir.OidImpuesto);

				if (impuesto.CuentaContableSoportado == string.Empty)
					throw new iQException("El impuesto '" + impuesto.Nombre + "' no tiene cuenta contable (soportado) asociada");

				//string tipo_iva = (impuestos.Count == 1) ? string.Empty : "C";

				//apunte en la cuenta del impuesto
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.LibroImpuesto,
					Fecha = invoice.Fecha,
					CuentaContable = titular.CuentaContable,
					CuentaContableContrapartida = impuesto.CuentaContableSoportado,
					Titular = invoice.Acreedor,
					Vat = invoice.VatNumber,
					NFactura = invoice.NFactura,
					FechaFactura = invoice.FechaRegistro,
					Importe = ir.Importe,
					BaseImponible = ir.BaseImponible,
					Total = ir.Total,
					Porcentaje = impuesto.Porcentaje,
					TipoImpuesto = ETipoImpuestoApunte.Soportado,
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = impuesto.Nombre,
				});
			}
		}

        /// <summary>
        /// FORMATO: 4300000016;Juan Alberto Acosta Guill;B55223655;1;19375;;;-2;4770000001;R;01/01/2007;;16;0;22475;3100;0;16;01/01/2007;0;0;0;C
        /// </summary>
        /// <param name="factura"></param>
		protected override void BuildTaxBookRepercutidoAccountingEntry(OutputInvoiceInfo factura) 
        {
			List<ImpuestoResumen> impuestos = new List<ImpuestoResumen>();

			foreach (DictionaryEntry impuesto in factura.GetImpuestos())
				impuestos.Add((ImpuestoResumen)impuesto.Value);

            ClienteInfo titular = _clients.GetItem(factura.OidCliente);
            string apunte = string.Empty;            
          
            foreach (ImpuestoResumen ir in impuestos)
            {
				apunte = string.Empty;

				ImpuestoInfo impuesto = _taxes.GetItem(ir.OidImpuesto);

				if (impuesto.CuentaContableRepercutido == string.Empty)
					throw new iQException("El impuesto '" + impuesto.Nombre + "' no tiene cuenta contable (repercutido) asociada");

                //string tipo_iva = (impuestos.Count == 1) ? string.Empty : "C";

                //apunte en la cuenta del impuesto
				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.LibroImpuesto,
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
					Diario = _config.Diario,
					Asiento = _accounting_entry,
					Descripcion = impuesto.Nombre,
				});
            }
        }

        /// <summary>
        /// FORMATO: 03/02/2007;4300000041;5720000000;1;Fra.3;12083;Alberto Albertos Cortéz;B112125465;-1;3;Opc1;Opc2;Opc3;03/01/2007
        /// </summary>
        /// <param name="payment"></param>
		protected override void BuildFinancialCashBookPaymentAccountingEntry(PaymentInfo payment)
		{
			IAcreedorInfo titular = _providers.GetItem(payment.OidAgente, payment.ETipoAcreedor);
			InputInvoiceInfo factura = null;

			if (titular.CuentaContable == string.Empty)
				throw new iQException("El deudor nº " + titular.Codigo + " (" + titular.Nombre + ") no tiene cuenta contable asociada");

			string apunte = string.Empty;
			string descripcion = string.Empty;
			int pos = 1;

            foreach (TransactionPaymentInfo pf in payment.Operations)
			{
				apunte = string.Empty;

                factura = _input_invoices.GetItem(pf.OidOperation);
				descripcion = "Pago Fra. " + factura.NFactura + " (" + factura.Acreedor + ")";

				//Sumamos los gastos bancarios solo al primer pagofactura 
				decimal importe = (pos++ == 1) ? pf.Cantidad + payment.GastosBancarios : pf.Cantidad;

				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.Efectos,
					Columna = EColumnaApunte.Debe,
					Fecha = payment.Vencimiento,
					CuentaContable = titular.CuentaContable,
					CuentaContableContrapartida = GetPaymentAccount(payment),
					Importe = importe,
					Titular = titular.Nombre,
					Vat = titular.Codigo,
					NFactura = factura != null ? factura.NFactura : string.Empty,
					FechaFactura = factura != null ? factura.Fecha : DateTime.MinValue,
					Libre1 = payment.IDPagoLabel,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});
			}

			if (payment.GastosBancarios != 0)
			{
				apunte = string.Empty;

				descripcion = "Gtos. Banco. " + payment.Codigo + " (" + titular.Nombre + ")";

				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.Efectos,
					Columna = EColumnaApunte.Debe,
					Fecha = payment.Vencimiento,
					CuentaContable = GetBankExpensesAccount(payment),
					CuentaContableContrapartida = GetPaymentAccount(payment),
					Importe = payment.GastosBancarios,
					Titular = titular.Nombre,
                    Vat = titular.Codigo,
                    NFactura = factura != null ? factura.NFactura : string.Empty,
                    FechaFactura = factura != null ? factura.Fecha : DateTime.MinValue,
					Libre1 = payment.IDPagoLabel,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});
			}
        }

        /// <summary>
        /// FORMATO: 03/02/2007;4300000041;5720000000;1;Fra.3;12083;Alberto Albertos Cortéz;B112125465;-1;3;Opc1;Opc2;Opc3;03/01/2007
        /// </summary>
        /// <param name="charge"></param>
		protected override void BuildFinalcialCashBookChargeAccountingEntry(ChargeInfo charge)
        {
            ClienteInfo titular = _clients.GetItem(charge.OidCliente);
			OutputInvoiceInfo factura;
 			
			if (titular.CuentaContable == string.Empty)
				throw new iQException("El cliente nº " + titular.NumeroClienteLabel + " (" + titular.Nombre + ") no tiene cuenta contable asociada");

			string apunte = string.Empty;
			string descripcion = string.Empty;
			int pos = 1;

			foreach (CobroFacturaInfo cf in charge.CobroFacturas)
			{
				apunte = string.Empty;

				factura = _invoices.GetItem(cf.OidFactura);
				descripcion = "Cobro Fra. " + factura.NFactura + " (" + factura.Cliente + ")";

				//Sumamos los gastos bancarios solo al primer pagofactura 
				decimal importe = (pos++ == 1) ? cf.Cantidad - charge.GastosBancarios : cf.Cantidad;

				BuildAccountingLine(new ApunteContaWin
				{
					Tipo = ETipoApunte.Efectos,
					Columna = EColumnaApunte.Haber,
					Fecha = charge.Vencimiento,
					CuentaContable = titular.CuentaContable,
					CuentaContableContrapartida = GetChargeAccount(charge),
					Importe = importe,
					Titular = factura.Cliente,
					Vat = factura.VatNumber,
					NFactura = factura.NFactura,
					FechaFactura = factura.Fecha,
					Libre1 = charge.IDCobroLabel,
					Asiento = _accounting_entry,
					Descripcion = descripcion,
				});
			}            
        }

        protected override void BuildLoanAccountingEntry(LoanInfo loan, LineaRegistro lr)
        {
            try
            {                
                BankAccountInfo cuenta = _bank_accounts.GetItem(loan.OidCuenta);

                BankAccountInfo cuentaO = null;
                BankAccountInfo cuentaD = null;

                if (cuenta.ETipoCuenta != ETipoCuenta.CuentaCorriente)
                {
                    cuentaO = cuenta;
                    cuentaD = _bank_accounts.GetItem(cuenta.OidCuentaAsociada); 
                    
                    if (cuentaO.CuentaContable == string.Empty)
                        throw new iQException("La cuenta bancaria " + cuentaO.Entidad + " no tiene cuenta contable asociada", new object[2] { typeof(BankAccount), cuentaO.Oid });

                }
                else
                {
                    cuentaD = cuenta;

                    if (loan.CuentaContable == string.Empty)
                        throw new iQException("El préstamo " + loan.Codigo + " no tiene cuenta contable asociada", new object[2] { ETipoEntidad.Prestamo, loan.Oid });
                
                }

                if (cuentaD.CuentaContable == string.Empty)
                    throw new iQException("La cuenta bancaria " + cuentaD.Entidad + " no tiene cuenta contable asociada", new object[2] { typeof(BankAccount), cuentaD.Oid });

                
                if (cuentaO != null)
                {
                    //Apunte de la salida de la cuenta de origen
                    BuildAccountingLine(new ApunteContaWin
                    {
                        Tipo = ETipoApunte.General,
                        Columna = loan.Importe < 0 ? EColumnaApunte.Debe : EColumnaApunte.Haber,
                        Fecha = loan.Fecha,
                        CuentaContable = cuentaO.CuentaContable,
                        Importe = -loan.Importe,
                        Libre1 = lr.IDExportacion,
                        Libre2 = loan.Codigo,
                        Diario = _config.Diario,
                        Asiento = _accounting_entry,
                        Descripcion = "Préstamo " + loan.Codigo + ": " + cuentaO.Entidad + " a " + cuentaD.Entidad
                    });
                }
                else
                {
                    //Apunte de la salida de la cuenta del préstamo
                    BuildAccountingLine(new ApunteContaWin
                    {
                        Tipo = ETipoApunte.General,
                        Columna = loan.Importe < 0 ? EColumnaApunte.Debe : EColumnaApunte.Haber,
                        Fecha = loan.Fecha,
                        CuentaContable = loan.CuentaContable,
                        Importe = -loan.Importe,
                        Libre1 = lr.IDExportacion,
                        Libre2 = loan.Codigo,
                        Diario = _config.Diario,
                        Asiento = _accounting_entry,
                        Descripcion = "Préstamo " + loan.Codigo + ": " + cuentaD.Entidad
                    });
                }

                //Apunte de entrada a la cuenta destino
                BuildAccountingLine(new ApunteContaWin
                {
                    Tipo = ETipoApunte.General,
                    Columna = loan.Importe < 0 ? EColumnaApunte.Haber : EColumnaApunte.Debe,
                    Fecha = loan.Fecha,
                    CuentaContable = cuentaD.CuentaContable,
                    Importe = loan.Importe,
                    Libre1 = lr.IDExportacion,
                    Libre2 = loan.Codigo,
                    Diario = _config.Diario,
                    Asiento = _accounting_entry,
                    Descripcion = "Préstamo " + loan.Codigo + ": " + (cuentaO != null ? cuentaO.Entidad + " a " + cuentaD.Entidad : cuentaD.Entidad)
                });

                if (loan.GastosInicio && loan.GastosBancarios != 0)
                {                     
                    //Debe en la cuenta contable de gastos del banco de destino
                    BuildAccountingLine(new ApunteContaWin
                    {
                        Tipo = ETipoApunte.General,
                        Columna = loan.GastosBancarios < 0 ? EColumnaApunte.Haber : EColumnaApunte.Debe,
                        Fecha = loan.Fecha,
                        CuentaContable = cuentaD.CuentaContableGastos,
                        Importe = loan.GastosBancarios,
                        Libre1 = lr.IDExportacion,
                        Libre2 = loan.Codigo,
                        Diario = _config.Diario,
                        Asiento = _accounting_entry,
                        Descripcion = "Gastos Bancarios del Préstamo " + loan.Codigo
                    });


                    //Haber en la cuenta contable del banco de origen
                    BuildAccountingLine(new ApunteContaWin
                    {
                        Tipo = ETipoApunte.General,
                        Columna = loan.GastosBancarios < 0 ? EColumnaApunte.Debe : EColumnaApunte.Haber,
                        Fecha = loan.Fecha,
                        CuentaContable = cuentaD.CuentaContable,
                        Importe = -loan.GastosBancarios,
                        Libre1 = lr.IDExportacion,
                        Libre2 = loan.Codigo,
                        Diario = _config.Diario,
                        Asiento = _accounting_entry,
                        Descripcion = "Gastos Bancarios del Traspaso " + loan.Codigo
                    });
                }
            }
            catch (iQException ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PRESTAMO, loan.Codigo, iQExceptionHandler.GetAllMessages(ex)), ex.Args);
            }
            catch (Exception ex)
            {
                throw new iQException(string.Format(Resources.Messages.ERROR_EXPORTANDO_PRESTAMO, loan.Codigo, iQExceptionHandler.GetAllMessages(ex)));
            }
        }

		protected override void BuildAccountingLine(IApunteContable iApunteC)
		{
			string apunte = string.Empty;

            ApunteContaWin apunteContable = (ApunteContaWin)iApunteC;

			switch (apunteContable.Tipo)
			{
				case ETipoApunte.General:
					{						
						/// FORMATO: 01/01/2007;4300000016;;;1;2;Factura No.  1;1;22475;166.386; 135.62;0;1;22475

						apunte += apunteContable.Fecha.ToShortDateString();		/*FECHA*/
						apunte += ";" + apunteContable.CuentaContable;			/*CUENTA*/
						apunte += ";";											/*DOCUMENTO*/
						apunte += ";";											/*SIN USO*/
						apunte += ";" + apunteContable.Diario;					/*DIARIO*/
						apunte += ";" + apunteContable.Asiento.ToString();		/*ASIENTO*/
						apunte += ";" + apunteContable.Descripcion;				/*DESCRIPCION*/
						apunte += ";" + GetColumnCode(apunteContable.Columna);	/*TIPO: DEBE=1, HABER=2*/
						apunte += ";" + apunteContable.Importe.ToString();		/*DEBE/HABER*/
						apunte += ";" + "";										/*VALOR DE CAMBIO 2ª DIVISA*/
						apunte += ";" + apunteContable.Libre1;					/*OPC. 1*/
						apunte += ";" + apunteContable.Libre2; 					/*OPC. 2*/
						apunte += ";" + "";										/*OPC. 3*/
						apunte += ";" + "";										/*SIN USO*/
						apunte += ";" + "";										/*CANAL*/

						_extra_file.WriteLine(apunte);
					}
					break;

				case ETipoApunte.Efectos:
					{
						/// FORMATO: 03/02/2007;4300000041;5720000000;1;Fra.3;12083;Alberto Albertos Cortéz;B112125465;-1;3;Opc1;Opc2;Opc3;03/01/2007
						
						apunte += apunteContable.Fecha;										/*FECHA VTO*/
						apunte += ";" + apunteContable.CuentaContable;						/*CUENTA CONTRAPARTIDA*/
						apunte += ";" + apunteContable.CuentaContableContrapartida;			/*CUENTA PAGO/COBRO*/
						apunte += ";" + GetColumnCode(apunteContable.Columna);				/*TIPO: COMPRAS=01, VENTAS=02*/
						apunte += ";" + apunteContable.Descripcion;							/*OBSERVACIONES*/
						apunte += ";" + apunteContable.Importe;								/*IMPORTE*/
						apunte += ";" + apunteContable.Titular;								/*TITULAR*/
						apunte += ";" + apunteContable.Vat;									/*NIF*/
						apunte += ";" + apunteContable.Asiento;								/*ASIENTO: SIN ENLACE AL DIARIO=-1*/
						apunte += ";" + apunteContable.NFactura;							/*Nº FACTURA*/
						apunte += ";" + apunteContable.Libre1;								/*OPC. 1*/
						apunte += ";" + "";													/*OPC. 2*/
						apunte += ";" + "";													/*OPC. 3*/
						apunte += ";" + apunteContable.FechaFactura.ToShortDateString();	/*FECHA FACTURA*/

						_efectos_file.WriteLine(apunte);
					}
					break;

				case ETipoApunte.LibroImpuesto:
					{
						/// FORMATO: 4300000016;Juan Alberto Acosta Guill;B55223655;1;19375;;;-2;4770000001;S;01/01/2007;;16;0;22475;3100;0;16;01/01/2007;0;0;0;C

						apunte += apunteContable.CuentaContable;						/*CUENTA CLIENTE/DEUDOR*/
						apunte += ";" + apunteContable.Titular;							/*CLIENTE/DEUDOR*/
						apunte += ";" + apunteContable.Vat;								/*NIF*/
						apunte += ";" + apunteContable.NFactura;						/*Nº FACTURA*/
						apunte += ";" + apunteContable.BaseImponible.ToString("N2");	/*BASE IMPONIBLE*/
						apunte += ";";													/*SIN USO*/
						apunte += ";";													/*SIN USO*/
						apunte += ";" + apunteContable.Asiento;	 						/*ASIENTO*/
						apunte += ";" + apunteContable.CuentaContableContrapartida;		/*CUENTA IVA*/
						apunte += ";" + GetTaxCode(apunteContable.TipoImpuesto);	/*TIPO: SOPORTADO=S, REPERCUTIDO=R*/
						apunte += ";" + apunteContable.Fecha.ToShortDateString();		/*FECHA*/
						apunte += ";";													/*SIN USO*/
						apunte += ";" + apunteContable.Porcentaje.ToString("N2");		/*%IVA*/
						apunte += ";" + "";												/*%RE*/
						apunte += ";" + apunteContable.Total.ToString("N2");			/*TOTAL*/
						apunte += ";" + apunteContable.Importe.ToString("N2");			/*CUOTA IVA*/
						apunte += ";" + "";												/*CUOTA RE*/
						apunte += ";" + apunteContable.Descripcion;						/*TIPO IVA*/
						apunte += ";" + apunteContable.FechaFactura.ToString("d");		/*FECHA CONTABLE*/
						apunte += ";" + apunteContable.Diario;							/*DIARIO*/
						apunte += ";" + "0";											/*CANAL*/
						apunte += ";" + "";												/*FORMA DE PAGO*/
						apunte += ";" + "";												/*TIPO 340: PREDETERMINADO=VACIO, MAS DE UN TIPO DE IVA:C, ABONO=D*/
						apunte += ";" + "";												/*FECHA OPERACION*/
                        apunte += ";" + "";                                             /*CUENTA BASE*/
						apunte += ";" + "";												/*NO 347*/
						apunte += ";" + "";												/*DATO 340_1*/
						apunte += ";" + "";												/*DATO 340_2*/
						apunte += ";" + "";												/*DATO 340_3*/
						apunte += ";" + "";												/*DATO 340_4: Factura a Rectificar*/

						_iva_file.WriteLine(apunte);
					}
					break;

			}
		}

        protected virtual string GetAccountingLineCode(ETipoApunte value) { return string.Empty; }
		protected override string GetColumnCode(EColumnaApunte value) { return (value == EColumnaApunte.Debe) ? "1" : "2"; }
		protected override string GetTaxCode(ETipoImpuestoApunte value) { return (value == ETipoImpuestoApunte.Soportado) ? "S" : "R"; }

        #endregion
    }
}
