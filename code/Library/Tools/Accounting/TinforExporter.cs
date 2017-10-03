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
    public class ApunteTinfor : IApunteContable
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

        public ETipoApunte Tipo { get; set; }
        public Decimal BaseImponible { get; set; }
        public Decimal Total { get; set; }
        public ETipoImpuestoApunte TipoImpuesto { get; set; }
        public Decimal Porcentaje { get; set; }

        #endregion

        #region Enums

        public enum ETipoApunte { General = 1, LibroImpuesto = 2, Efectos = 3, FacturaRecibida = 4, FacturaEmitida = 5 }

        #endregion
    }

    public class TinforExporter: ContabilidadExporterBase, IContabilidadExporter
    {
        #region Attributes & Properties

        StreamWriter _export_file;
        
        #endregion

        #region Factory Methods

		public TinforExporter() { }

        public override void Init(ContabilidadConfig config)
        {
			base.Init(config);

			string fileName = _config.RutaSalida + "VENTAS1.txt";
            
			if (!File.Exists(fileName)) File.Delete(fileName);
			_export_file = new StreamWriter(fileName, false, Encoding.GetEncoding(1252));
        }

		public override void SaveFiles()
        {
            _export_file.Close();
 
			base.Close();
        }

        #endregion

        #region Business Methods

		protected override string AddValue(string value) { return value + "¦"; }

        /// <summary>
        /// FORMATO: 9999999|G|01/10/2009||VENTAS DIA|4300000001|||||||||VENTAS P.S. DEL DIA|D| |37800| |7050000001|00000|00000|00000|4|001| |36000|500| |1800| |0|4777000000|0| | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | | | | | | | |1234| | | |0
        /// </summary>
        /// <param name="factura"></param>
		protected override void BuildInputInvoiceAccountingEntry(InputInvoiceInfo factura, LineaRegistro lr) 
        {
            IAcreedorInfo titular = _providers.GetItem(factura.OidAcreedor, factura.ETipoAcreedor);

			if (titular.CuentaContable == string.Empty)
				throw new iQException("El deudor nº " + titular.Codigo + " (" + titular.Nombre + ") no tiene cuenta contable asociada");

			string apunte = string.Empty;
            string descripcion = "Fra. " + factura.NFactura + " (" + factura.Acreedor + ")";

			string tipo = (factura.Rectificativa) ? "D" : "H";
			string signo = (factura.Rectificativa) ? "+" : "-";
			string importe = factura.Total.ToString().Replace(".", string.Empty);

            //apunte en la cuenta del acreedor
			apunte += AddValue(_config.Empresa);						/*CENTRO DE TRABAJO*/
			apunte += AddValue("S");								/*TIPO DE REGISTRO*/
			apunte += AddValue(factura.Fecha.ToShortDateString());	/*FECHA*/
			apunte += AddValue(factura.NSerie);						/*Nº SERIE DOCUMENTO*/
			apunte += AddValue(factura.NFactura);					/*DOCUMENTO*/
            apunte += AddValue(titular.CuentaContable);				/*CUENTA*/
			apunte += AddEmptyValue(8);								/*SIN USO*/
			apunte += AddValue(descripcion);						/*DESCRIPCION*/
			apunte += AddValue(tipo.ToString());					/*TIPO: DEBE=1, HABER=2*/
			apunte += AddValue(signo);								/*SIGNO*/
			apunte += AddValue(importe);							/*IMPORTE*/
			apunte += AddValue(_accounting_entry.ToString());				/*ASIENTO*/
			apunte += AddEmptyValue(73);							/*SIN USO*/
			apunte += AddValue(_config.CentroTrabajo);				/*CENTRO TRABAJO*/
			apunte += AddEmptyValue(3);								/*SIN USO*/

            _export_file.WriteLine(apunte);

			// Apuntes en las cuentas de Compra

            List<CuentaResumen> cuentas = factura.GetCuentas();
            FamiliaInfo familia;

            foreach (CuentaResumen cr in cuentas)
            {
				apunte = string.Empty;

                familia = _families.GetItem(cr.OidFamilia);

				if (familia == null) throw new iQException("Factura " + factura.NFactura + " con familia a nulo");

				if (cr.CuentaContable  == string.Empty)
					throw new iQException("La familia nº " + familia.Codigo + " (" + familia.Nombre + ") no tiene cuenta contable (compra) asociada");

				tipo = (factura.Rectificativa) ? "H" : "D";
				signo = (factura.Rectificativa) ? "-" : "+";
				importe = cr.Importe.ToString().Replace(".", string.Empty);

				//apunte en la cuenta de compra
				apunte += AddValue(_config.Empresa);							/*CENTRO DE TRABAJO*/
				apunte += AddValue("G");								/*TIPO DE REGISTRO*/
				apunte += AddValue(factura.Fecha.ToShortDateString());	/*FECHA*/
				apunte += AddValue(factura.NSerie);						/*Nº SERIE DOCUMENTO*/
				apunte += AddValue(factura.NFactura);					/*DOCUMENTO*/
				apunte += AddValue(cr.CuentaContable);					/*CUENTA*/
				apunte += AddEmptyValue(8);								/*SIN USO*/
				apunte += AddValue(descripcion);						/*DESCRIPCION*/
				apunte += AddValue(tipo.ToString());					/*TIPO: DEBE=1, HABER=2*/
				apunte += AddValue(signo);								/*SIGNO*/
				apunte += AddValue(importe);							/*IMPORTE*/
				apunte += AddValue(_accounting_entry.ToString());				/*ASIENTO*/
				apunte += AddEmptyValue(73);							/*SIN USO*/
				apunte += AddValue(_config.CentroTrabajo);				/*CENTRO TRABAJO*/
				apunte += AddEmptyValue(3);								/*SIN USO*/

                _export_file.WriteLine(apunte);
            }

			// Apuentes en las cuentas de Impuestos

			List<ImpuestoResumen> impuestos = factura.GetImpuestos();

			foreach (ImpuestoResumen ir in impuestos)
			{
				if (ir.Importe == 0) continue;

				apunte = string.Empty;

				ImpuestoInfo impuesto = _taxes.GetItem(ir.OidImpuesto);

				if (impuesto == null) throw new iQException("Factura " + factura.NFactura + " con impuesto a nulo");

				if (impuesto.CuentaContableSoportado == string.Empty)
					throw new iQException("El impuesto '" + impuesto.Nombre + "' no tiene cuenta contable (soportado) asociada");

				tipo = (factura.Rectificativa) ? "H" : "D";
				signo = (factura.Rectificativa) ? "-" : "+";
				importe = ir.Importe.ToString().Replace(".", string.Empty); 			

				//apunte en la cuenta del impuesto
				apunte += AddValue(_config.Empresa);					/*CENTRO DE TRABAJO*/
				apunte += AddValue("G");								/*TIPO DE REGISTRO*/
				apunte += AddValue(factura.Fecha.ToShortDateString());	/*FECHA*/
				apunte += AddValue(factura.NSerie);						/*Nº SERIE DOCUMENTO*/
				apunte += AddValue(factura.NFactura);					/*DOCUMENTO*/
				apunte += AddValue(impuesto.CuentaContableSoportado);	/*CUENTA*/
				apunte += AddEmptyValue(8);								/*SIN USO*/
				apunte += AddValue(descripcion);						/*DESCRIPCION*/
				apunte += AddValue(tipo.ToString());					/*TIPO: DEBE=1, HABER=2*/
				apunte += AddValue("+");								/*SIGNO*/
				apunte += AddValue(importe);							/*IMPORTE*/
				apunte += AddValue(_accounting_entry.ToString());				/*ASIENTO*/
				apunte += AddEmptyValue(73);							/*SIN USO*/
				apunte += AddValue(_config.CentroTrabajo);				/*CENTRO TRABAJO*/
				apunte += AddEmptyValue(3);								/*SIN USO*/

				_export_file.WriteLine(apunte);
			}
        }

        /// <summary>
		/// FORMATO: 9999999|G|01/10/2009||VENTAS DIA|4300000001|||||||||VENTAS P.S. DEL DIA|D| |37800| |7050000001|00000|00000|00000|4|001| |36000|500| |1800| |0|4777000000|0| | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | | | | | | | |1234| | | |0
        /// </summary>
        /// <param name="factura"></param>
		protected override void BuildOutputInvoiceAccountingEntry(OutputInvoiceInfo factura, LineaRegistro lr)
        {
            ClienteInfo titular = _clients.GetItem(factura.OidCliente);

			if (titular.CuentaContable == string.Empty)
				throw new iQException("El cliente nº " + titular.NumeroClienteLabel + " (" + titular.Nombre + ") no tiene cuenta contable asociada");

            string apunte = string.Empty;
            string descripcion  = "Fra. " + factura.NFactura + " (" + factura.Cliente + ")";

			string tipo = (factura.Rectificativa) ? "H" : "D";
			string signo = (factura.Rectificativa) ? "-" : "+";
			string importe = factura.Total.ToString().Replace(".", string.Empty);

            //apunte en la cuenta del cliente
			apunte += AddValue(_config.Empresa);							/*CENTRO DE TRABAJO*/
			apunte += AddValue("S");								/*TIPO DE REGISTRO*/
			apunte += AddValue(factura.Fecha.ToShortDateString());	/*FECHA*/
			apunte += AddValue(factura.NumeroSerie);				/*Nº SERIE DOCUMENTO*/
			apunte += AddValue(factura.NFactura);					/*DOCUMENTO*/
			apunte += AddValue(titular.CuentaContable);				/*CUENTA*/
			apunte += AddEmptyValue(8);								/*SIN USO*/
			apunte += AddValue(descripcion);						/*DESCRIPCION*/
			apunte += AddValue(tipo.ToString());					/*TIPO: DEBE=1, HABER=2*/
			apunte += AddValue(signo);								/*SIGNO*/
			apunte += AddValue(importe);							/*IMPORTE*/
			apunte += AddValue(_accounting_entry.ToString());				/*ASIENTO*/
			apunte += AddEmptyValue(73);							/*SIN USO*/
			apunte += AddValue(_config.CentroTrabajo);				/*CENTRO TRABAJO*/
			apunte += AddEmptyValue(3);								/*SIN USO*/

			_export_file.WriteLine(apunte);

            List<CuentaResumen> cuentas = factura.GetCuentas();
            FamiliaInfo familia;

            foreach (CuentaResumen cr in cuentas)
            {
				apunte = string.Empty;

                familia = _families.GetItem(cr.OidFamilia);

				if (familia == null) throw new iQException("Factura " + factura.NFactura + " con familia a nulo");

				if (cr.CuentaContable == string.Empty)
					throw new iQException("La familia nº " + familia.Codigo + " (" + familia.Nombre + ") no tiene cuenta contable (venta) asociada");

				tipo = (factura.Rectificativa) ? "D" : "H";
				signo = (factura.Rectificativa) ? "+" : "-";
				importe = cr.Importe.ToString().Replace(".", string.Empty); 				

                //apunte en la cuenta de venta
				apunte += AddValue(_config.Empresa);							/*CODIGO DE EMPRESA*/
				apunte += AddValue("G");								/*TIPO DE REGISTRO*/
				apunte += AddValue(factura.Fecha.ToShortDateString());	/*FECHA*/
				apunte += AddValue(factura.NumeroSerie);				/*Nº SERIE DOCUMENTO*/
				apunte += AddValue(factura.NFactura);					/*DOCUMENTO*/
				apunte += AddValue(cr.CuentaContable);					/*CUENTA*/
				apunte += AddEmptyValue(8);								/*SIN USO*/
				apunte += AddValue(descripcion);						/*DESCRIPCION*/
				apunte += AddValue(tipo.ToString());					/*TIPO: DEBE=1, HABER=2*/
				apunte += AddValue(signo);								/*SIGNO*/
				apunte += AddValue(importe);							/*IMPORTE*/
				apunte += AddValue(_accounting_entry.ToString());				/*ASIENTO*/
				apunte += AddEmptyValue(73);							/*SIN USO*/
				apunte += AddValue(_config.CentroTrabajo);				/*CENTRO TRABAJO*/
				apunte += AddEmptyValue(3);								/*SIN USO*/

				_export_file.WriteLine(apunte);
            }

			// Apuntes en las cuentas de Impuestos

			List<ImpuestoResumen> impuestos = new List<ImpuestoResumen>();

			foreach (DictionaryEntry impuesto in factura.GetImpuestos())
				impuestos.Add((ImpuestoResumen)impuesto.Value);

			foreach (ImpuestoResumen ir in impuestos)
			{
				if (ir.Importe == 0) continue;

				apunte = string.Empty;

				ImpuestoInfo impuesto = _taxes.GetItem(ir.OidImpuesto);

				if (impuesto == null) throw new iQException("Factura " + factura.NFactura + " con impuesto a nulo");

				if (impuesto.CuentaContableSoportado == string.Empty)
					throw new iQException("El impuesto '" + impuesto.Nombre + "' no tiene cuenta contable (repercutido) asociada");

				tipo = (factura.Rectificativa) ? "D" : "H";
				signo = (factura.Rectificativa) ? "+" : "-";
				importe = ir.Importe.ToString().Replace(".", string.Empty);

				//apunte en la cuenta del impuesto
				apunte += AddValue(_config.Empresa);							/*CODIGO DE EMPRESA*/
				apunte += AddValue("G");								/*TIPO DE REGISTRO*/
				apunte += AddValue(factura.Fecha.ToShortDateString());	/*FECHA*/
				apunte += AddValue(factura.NumeroSerie);				/*Nº SERIE DOCUMENTO*/
				apunte += AddValue(factura.NFactura);					/*DOCUMENTO*/
				apunte += AddValue(impuesto.CuentaContableSoportado);	/*CUENTA*/
				apunte += AddEmptyValue(8);								/*SIN USO*/
				apunte += AddValue(descripcion);						/*DESCRIPCION*/
				apunte += AddValue(tipo.ToString());					/*TIPO: DEBE=1, HABER=2*/
				apunte += AddValue(signo);								/*SIGNO*/
				apunte += AddValue(importe);							/*IMPORTE*/
				apunte += AddValue(_accounting_entry.ToString());				/*ASIENTO*/
				apunte += AddEmptyValue(73);							/*SIN USO*/
				apunte += AddValue(_config.CentroTrabajo);				/*CENTRO TRABAJO*/
				apunte += AddEmptyValue(3);								/*SIN USO*/

				_export_file.WriteLine(apunte);
			}
        }

		/// <summary>
		/// FORMATO: 9999999|G|01/10/2009||VENTAS DIA|4300000001|||||||||VENTAS P.S. DEL DIA|D| |37800| |7050000001|00000|00000|00000|4|001| |36000|500| |1800| |0|4777000000|0| | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | | | | | | | |1234| | | |0
		/// </summary>
		/// <param name="factura"></param>
		protected override void BuildInvoicePaymentAccountingEntry(PaymentInfo pago, LineaRegistro lr)
		{
			if (pago.ETipoPago != ETipoPago.Factura) return;
            if (pago.EEstadoPago != EEstado.Pagado) return;

			IAcreedorInfo titular = _providers.GetItem(pago.OidAgente, pago.ETipoAcreedor);
			InputInvoiceInfo factura = null;

			if (titular.CuentaContable == string.Empty)
				throw new iQException("El deudor nº " + titular.Codigo + " (" + titular.Nombre + ") no tiene cuenta contable asociada");

			string apunte = string.Empty;
			string descripcion = string.Empty;
			int pos = 1;
			string tipo = "D";
			string signo = "+";
			string importe = string.Empty;

			foreach (TransactionPaymentInfo pf in pago.Operations)
			{
				factura = _input_invoices.GetItem(pf.OidOperation);
				tipo = "D";
				signo = "+";
				descripcion = "Pago Fra. " + factura.NFactura + " (" + factura.Acreedor + ")";

				//Sumamos los gastos bancarios solo al primer pagofactura 
				decimal valor = (pos++ == 1) ? pf.Cantidad + pago.GastosBancarios : pf.Cantidad;
				importe = valor.ToString().Replace(".", string.Empty);

				//apunte en la cuenta del acreedor
				apunte += AddValue(_config.Empresa);					/*CODIGO DE EMPRESA*/
				apunte += AddValue("S");								/*TIPO DE REGISTRO*/
				apunte += AddValue(pago.Fecha.ToShortDateString());		/*FECHA*/
				apunte += AddEmptyValue(1);								/*Nº SERIE DOCUMENTO*/
				apunte += AddValue(pago.Codigo);						/*DOCUMENTO*/
				apunte += AddValue(titular.CuentaContable);				/*CUENTA*/
				apunte += AddEmptyValue(8);								/*SIN USO*/
				apunte += AddValue(descripcion);						/*DESCRIPCION*/
				apunte += AddValue(tipo.ToString());					/*TIPO: DEBE=1, HABER=2*/
				apunte += AddValue(signo);								/*SIGNO*/
				apunte += AddValue(importe);							/*IMPORTE*/
				apunte += AddValue(_accounting_entry.ToString());				/*ASIENTO*/
				apunte += AddEmptyValue(73);							/*SIN USO*/
				apunte += AddValue(_config.CentroTrabajo);				/*CENTRO TRABAJO*/
				apunte += AddEmptyValue(2);								/*SIN USO*/
				apunte += AddValue(lr.IDExportacion);					/*OPC. 1*/

				_export_file.WriteLine(apunte);

				apunte = string.Empty;
				tipo = "H"; 
				signo = "-";
				importe = pf.Cantidad.ToString().Replace(".", string.Empty);

				//apunte en la cuenta del pago
				apunte += AddValue(_config.Empresa);					/*CODIGO DE EMPRESA*/
				apunte += AddValue("G");								/*TIPO DE REGISTRO*/
				apunte += AddValue(pago.Fecha.ToShortDateString());		/*FECHA*/
				apunte += AddEmptyValue(1);								/*Nº SERIE DOCUMENTO*/
				apunte += AddValue(pago.Codigo);						/*DOCUMENTO*/
				apunte += AddValue(GetPaymentAccount(pago));				/*CUENTA*/
				apunte += AddEmptyValue(8);								/*SIN USO*/
				apunte += AddValue(descripcion);						/*DESCRIPCION*/
				apunte += AddValue(tipo.ToString());					/*TIPO: DEBE=1, HABER=2*/
				apunte += AddValue(signo);								/*SIGNO*/
				apunte += AddValue(importe);							/*IMPORTE*/
				apunte += AddValue(_accounting_entry.ToString());				/*ASIENTO*/
				apunte += AddEmptyValue(73);							/*SIN USO*/
				apunte += AddValue(_config.CentroTrabajo);				/*CENTRO TRABAJO*/
				apunte += AddEmptyValue(2);								/*SIN USO*/
				apunte += AddValue(lr.IDExportacion);					/*OPC. 1*/

				_export_file.WriteLine(apunte);

				apunte = string.Empty;
			}

			if (pago.GastosBancarios != 0)
			{
				apunte = string.Empty;
				tipo = "H";
				signo = "-";
				descripcion = "Gtos. Banco. " + pago.Codigo + " (" + titular.Nombre + ")";
				importe = pago.GastosBancarios.ToString().Replace(".", string.Empty);

				//apunte en la cuenta de gastos bancarios
				apunte += AddValue(_config.Empresa);					/*CODIGO DE EMPRESA*/
				apunte += AddValue("G");								/*TIPO DE REGISTRO*/
				apunte += AddValue(pago.Fecha.ToShortDateString());		/*FECHA*/
				apunte += AddEmptyValue(1);								/*Nº SERIE DOCUMENTO*/
				apunte += AddValue(pago.Codigo);						/*DOCUMENTO*/
				apunte += AddValue(GetBankExpensesAccount(pago));		/*CUENTA*/
				apunte += AddEmptyValue(8);								/*SIN USO*/
				apunte += AddValue(descripcion);						/*DESCRIPCION*/
				apunte += AddValue(tipo.ToString());					/*TIPO: DEBE=1, HABER=2*/
				apunte += AddValue(signo);								/*SIGNO*/
				apunte += AddValue(importe);							/*IMPORTE*/
				apunte += AddValue(_accounting_entry.ToString());				/*ASIENTO*/
				apunte += AddEmptyValue(73);							/*SIN USO*/
				apunte += AddValue(_config.CentroTrabajo);				/*CENTRO TRABAJO*/
				apunte += AddEmptyValue(2);								/*SIN USO*/
				apunte += AddValue(lr.IDExportacion);					/*OPC. 1*/

				_export_file.WriteLine(apunte);
			}
		}
		
		/// <summary>
		/// FORMATO: 9999999|G|01/10/2009||VENTAS DIA|4300000001|||||||||VENTAS P.S. DEL DIA|D| |37800| |7050000001|00000|00000|00000|4|001| |36000|500| |1800| |0|4777000000|0| | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | |0|0| |0| |0| | | | | | | | | | |1234| | | |0
		/// </summary>
		/// <param name="factura"></param>
		protected override void BuildChargeAccountingEntry(ChargeInfo cobro, LineaRegistro lr)
		{
            if (cobro.EEstadoCobro != EEstado.Charged) return;

			ClienteInfo titular = _clients.GetItem(cobro.OidCliente);
			OutputInvoiceInfo factura;

			string apunte = string.Empty;
			string descripcion = string.Empty;
			string tipo = "H"; 
			string signo = "-";
			string importe = string.Empty;

			if (titular.CuentaContable == string.Empty)
				throw new iQException("El cliente nº " + titular.NumeroClienteLabel + " (" + titular.Nombre + ") no tiene cuenta contable asociada");

			foreach (CobroFacturaInfo cf in cobro.CobroFacturas)
			{
				factura = _invoices.GetItem(cf.OidFactura);
				tipo = "H";
				signo = "-";
     			descripcion = "Cobro Fra. " + factura.NFactura + " (" + factura.Cliente + ")";
				importe = cf.Cantidad.ToString().Replace(".", string.Empty);

				//apunte en la cuenta del cliente
				apunte += AddValue(_config.Empresa);					/*CODIGO DE EMPRESA*/
				apunte += AddValue("S");								/*TIPO DE REGISTRO*/
				apunte += AddValue(cobro.Fecha.ToShortDateString());	/*FECHA*/
				apunte += AddEmptyValue(1);								/*Nº SERIE DOCUMENTO*/
				apunte += AddValue(cobro.Codigo);						/*DOCUMENTO*/
				apunte += AddValue(titular.CuentaContable);				/*CUENTA*/
				apunte += AddEmptyValue(8);								/*SIN USO*/
				apunte += AddValue(descripcion);						/*DESCRIPCION*/
				apunte += AddValue(tipo.ToString());					/*TIPO: DEBE=1, HABER=2*/
				apunte += AddValue(signo);								/*SIGNO*/
				apunte += AddValue(importe);							/*IMPORTE*/
				apunte += AddValue(_accounting_entry.ToString());				/*ASIENTO*/
				apunte += AddEmptyValue(73);							/*SIN USO*/
				apunte += AddValue(_config.CentroTrabajo);				/*CENTRO TRABAJO*/
				apunte += AddEmptyValue(2);								/*SIN USO*/
				apunte += AddValue(lr.IDExportacion);					/*OPC. 1*/

				_export_file.WriteLine(apunte);

				apunte = string.Empty;
				tipo = "D";
				signo = "+";
				importe = cf.Cantidad.ToString().Replace(".", string.Empty);

				//apunte en la cuenta del pago
				apunte += AddValue(_config.Empresa);					/*CODIGO DE EMPRESA*/
				apunte += AddValue("G");								/*TIPO DE REGISTRO*/
				apunte += AddValue(cobro.Fecha.ToShortDateString());	/*FECHA*/
				apunte += AddEmptyValue(1);								/*Nº SERIE DOCUMENTO*/
				apunte += AddValue(cobro.Codigo);						/*DOCUMENTO*/
				apunte += AddValue(GetChargeAccount(cobro));				/*CUENTA*/
				apunte += AddEmptyValue(8);								/*SIN USO*/
				apunte += AddValue(descripcion);						/*DESCRIPCION*/
				apunte += AddValue(tipo.ToString());					/*TIPO: DEBE=1, HABER=2*/
				apunte += AddValue(signo);								/*SIGNO*/
				apunte += AddValue(importe);							/*IMPORTE*/
				apunte += AddValue(_accounting_entry.ToString());				/*ASIENTO*/
				apunte += AddEmptyValue(73);							/*SIN USO*/
				apunte += AddValue(_config.CentroTrabajo);				/*CENTRO TRABAJO*/
				apunte += AddEmptyValue(2);								/*SIN USO*/
				apunte += AddValue(lr.IDExportacion);					/*OPC. 1*/

				_export_file.WriteLine(apunte);

				apunte = string.Empty;
			}
		}

		/// <summary>
		/// FORMATO: 
		/// </summary>
		/// <param name="factura"></param>
		protected override void BuildTaxBookSoportadoAccountingEntry(InputInvoiceInfo factura) {}

        /// <summary>
        /// FORMATO: 
        /// </summary>
        /// <param name="factura"></param>
        protected override void BuildTaxBookRepercutidoAccountingEntry(OutputInvoiceInfo factura) {}

        /// <summary>
        /// FORMATO: 
        /// </summary>
        /// <param name="pago"></param>
        protected override void BuildFinancialCashBookPaymentAccountingEntry(PaymentInfo pago) {}

        /// <summary>
        /// FORMATO: 
        /// </summary>
        /// <param name="cobro"></param>
		protected override void BuildFinalcialCashBookChargeAccountingEntry(ChargeInfo cobro) {}

        #endregion
    }
}