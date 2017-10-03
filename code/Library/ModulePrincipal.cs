using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Globalization;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice.Properties;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class ModulePrincipal
	{
		#region Application Settings

		public static void SaveSettings() { Settings.Default.Save(); }

		public static void UpgradeSettings()
		{
			Assembly ensamblado = System.Reflection.Assembly.GetExecutingAssembly();
			Version ver = ensamblado.GetName().Version;

			/*if (Properties.Settings.Default.MODULE_VERSION != ver.ToString())
			{
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.MODULE_VERSION = ver.ToString();
			}**/
		}

		public static string GetDBVersion() { return Settings.Default.DB_VERSION; }

		#endregion

		#region Schema Settings

		//CAJA
		public static long GetCajaTicketsSetting()
		{
			return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CAJA_TICKETS));
		}
		public static void SetCajaTicketsSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CAJA_TICKETS, value.ToString());
		}
		public static bool GetLineaCajaLibreSetting()
		{
			return Convert.ToBoolean(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_LINEA_CAJA_LIBRE));
		}
		public static void SetLineaCajaLibreSetting(bool value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_LINEA_CAJA_LIBRE, value.ToString());
		}

		// CONTABILIDAD
		public static string GetCuentaEfectosACobrarSetting()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CUENTA_EFECTOS_A_COBRAR);
		}
		public static void SetCuentaEfectosACobrarSetting(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CUENTA_EFECTOS_A_COBRAR, value);
		}

		public static string GetCuentaEfectosAPagarSetting()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CUENTA_EFECTOS_A_PAGAR);
		}
		public static void SetCuentaEfectosAPagarSetting(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CUENTA_EFECTOS_A_PAGAR, value);
		}

		public static string GetCuentaHaciendaSetting()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CUENTA_IRPF);
		}
		public static void SetCuentaHaciendaSetting(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CUENTA_IRPF, value);
		}
		
		public static string GetCuentaNominasSetting()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CUENTA_NOMINAS);
		}
		public static void SetCuentaNominasSetting(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CUENTA_NOMINAS, value);
		}

		public static string GetCuentaRemuneracionesSetting()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CUENTA_REMUNERACIONES);
		}
		public static void SetCuentaRemuneracionesSetting(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CUENTA_REMUNERACIONES, value);
		}

		public static string GetCuentaSegurosSocialesSetting()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CUENTA_SEGUROS_SOCIALES);
		}
		public static void SetCuentaSegurosSocialesSetting(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CUENTA_SEGUROS_SOCIALES, value);
		}

		public static string GetCuentaSubvencionesSetting()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CUENTA_SUBVENCIONES);
		}
		public static void SetCuentaSubvencionesSetting(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CUENTA_SUBVENCIONES, value);
		}

        //Libro Diario de ContaWin
		public static string GetJournalSetting()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_JOURNAL_VARIABLE_NAME);
		}
		public static void SetJournalSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_JOURNAL_VARIABLE_NAME, value.ToString());
		}
		
        //Asiento Inicial de ContaWin
		public static string GetLastAsientoSetting()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_LAST_ENTRY);
		}
		public static void SetLastAsientoSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_LAST_ENTRY, value.ToString());
        }

        //Código de Empresa de A3
        public static string GetEmpresaA3Setting()
        {
            return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CODIGO_EMPRESA_A3);
        }
        public static void SetEmpresaA3Setting(long value)
        {
            SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CODIGO_EMPRESA_A3, value.ToString());
        }

        //Libro diario de Tinfor
        public static string GetJournalTinforSetting()
        {
            return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_JOURNAL_TINFOR);
        }
        public static void SetJournalTinforSetting(long value)
        {
            SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_JOURNAL_TINFOR, value.ToString());
        }

        //Asiento Inicial de Tinfor
        public static string GetLastAsientoTinforSetting()
        {
            return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_LAST_ENTRY_TINFOR);
        }
        public static void SetLasAsientoTinforSetting(long value)
        {
            SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_LAST_ENTRY_TINFOR, value.ToString());
        }

        //Empresa de Tinfor
        public static string GetEmpresaTinforSetting()
        {
            return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_EMPRESA_TINFOR);
        }
        public static void SetEmpresaTinforSetting(string value)
        {
            SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_EMPRESA_TINFOR, value.ToString());
        }

        //Centro de Trabajo de Tinfor
        public static string GetCentroTrabajoTinforSetting()
        {
            return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_CENTRO_TRABAJO_TINFOR);
        }
        public static void SetCentroTrabajoTinforSetting(string value)
        {
            SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_CENTRO_TRABAJO_TINFOR, value.ToString());
        }

        //Tipo de Exportación por defecto
        public static ETipoExportacion GetDefaultTipoExportacionSetting()
        {
            return (ETipoExportacion)(Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_TIPO_EXPORTACION)));
        }
        public static void SetDefaultTipoExportacionSetting(ETipoExportacion value)
        {
            SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_TIPO_EXPORTACION, ((long)value).ToString());
        }

		public static bool GetUseTPVCountSetting()
		{
			return Convert.ToBoolean(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_USE_TPV_COUNT));
		}
		public static void SetUseTPVCountSetting(bool value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_USE_TPV_COUNT, value.ToString());
		}

		public static long GetImpuestosImportacion()
		{
			try { return Convert.ToInt32(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_IMPUESTOS_IMPORTACION)); }
			catch { return 0; }
		}
		public static void SetImpuestosImportacion(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_IMPUESTOS_IMPORTACION, value.ToString());
		}

		// FACTURACION
		public static string GetInvoiceTemplateSetting()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_INVOICE_TEMPLATE);
		}
		public static void SetInvoiceTemplateSetting(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_INVOICE_TEMPLATE, value.ToString());
		}

		public static int GetNDigitosNFacturaSetting()
		{
			return Convert.ToInt32(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_N_DIGITOS_N_FACTURA));
		}
		public static void setNDigitosNFacturaSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_N_DIGITOS_N_FACTURA, value.ToString());
		}

		public static int GetNDecimalesPreciosSetting()
		{
			return Convert.ToInt32(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_N_DECIMALES_PRECIOS));
		}
		public static void SetNDecimalesPreciosSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_N_DECIMALES_PRECIOS, value.ToString());
		}

        public static decimal GetTipoInteresPorGastosDemoraSetting()
        {
			return Convert.ToDecimal(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_TIPO_INTERES_GASTOS_DEMORA), 
										new NumberFormatInfo() { NumberDecimalSeparator = "." });
        }

        public static void SetTipoInteresPorGastosDemoraSetting(decimal value)
        {
            SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_TIPO_INTERES_GASTOS_DEMORA, value.ToString("N2"));
        }

		public static long GetWorkDeliverySerieSetting()
		{
			try { return Convert.ToInt32(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_WORK_DELIVERY_SERIE)); }
			catch { return 0; }
		}
		public static void SetWorkDeliverySerieSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_WORK_DELIVERY_SERIE, value.ToString());
		}

		// NOTIFICACIONES
		public static void SetSendFacturasPendientes(bool value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_ENVIAR_FACTURAS_PENDIENTES, value.ToString());
		}
		public static bool GetSendFacturasPendientes()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_ENVIAR_FACTURAS_PENDIENTES)); }
			catch { return false; }
		}

		public static void SetPlazoEnvioFacturasPendientes(decimal value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_PLAZO_ENVIO_FACTURAS, value.ToString());
		}
		public static int GetPlazoEnvioFacturasPendientes()
		{
			try { return Convert.ToInt32(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_PLAZO_ENVIO_FACTURAS)); }
			catch { return 0; }
		}

		public static void SetPeriodicidadEnvioFacturasPendientes(decimal value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_PERIODICIDAD_ENVIO_FACTURAS, value.ToString());
		}
		public static int GetPeriodicidadEnvioFacturasPendientes()
		{
			try { return Convert.ToInt32(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_PERIODICIDAD_ENVIO_FACTURAS)); }
			catch { return 0; }
		}

		public static void SetFechaUltimoEnvioFacturasPendientes(DateTime value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_FECHA_ULTIMO_ENVIO_FACTURAS, value.ToString("dd/MM/yyyy HH:mm"));
		}
		public static DateTime GetFechaUltimoEnvioFacturasPendientes()
		{
			try { return Convert.ToDateTime(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_FECHA_ULTIMO_ENVIO_FACTURAS)); }
			catch { return DateTime.MinValue; }
		}

		public static void SetHoraEnvioFacturasPendientes(DateTime value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_HORA_ENVIO_FACTURAS, value.ToString("HH:mm"));
		}
		public static DateTime GetHoraEnvioFacturasPendientes()
		{
			try { return Convert.ToDateTime(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_HORA_ENVIO_FACTURAS)); }
			catch { return DateTime.MinValue; }
		}

		//PAYMENT GATEWAY
		public static void SetPaymentGatewayAPIHost(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_API_HOST, value);
		}
		public static string GetPaymentGatewayAPIHost()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_API_HOST);
		}

		public static void SetPaymentGatewayCode(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_CODE, value.ToString());
		}
		public static long GetPaymentGatewayCode()
		{
			return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_CODE));
		}

		public static void SetPaymentGatewayCurLCA(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_CURL_CA, value);
		}
		public static string GetPaymentGatewayCurLCA()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_CURL_CA);
		}

		public static void SetPaymentGatewayCurrency(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_CURRENCY, value.ToString());
		}
		public static long GetPaymentGatewayCurrency()
		{
			return Convert.ToInt64(SettingsMng.Instance.SchemaSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_CURRENCY));
		}

		public static void SetPaymentGatewayKOResponseURL(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_KO_RESPONSE_URL, value);
		}
		public static string GetPaymentGatewayKOResponseURL()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_KO_RESPONSE_URL);
		}

		public static void SetPaymentGatewayMerchantCode(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_MERCHANT_CODE, value);
		}
		public static string GetPaymentGatewayMerchantCode()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_MERCHANT_CODE);
		}

		public static void SetPaymentGatewayMerchantName(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_MERCHANT_NAME, value);
		}
		public static string GetPaymentGatewayMerchantName()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_MERCHANT_NAME);
		}

		public static void SetPaymentGatewayName(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_NAME, value);
		}
		public static string GetPaymentGatewayName()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_NAME);
		}

		public static void SetPaymentGatewayOKResponseURL(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_OK_RESPONSE_URL, value);
		}
		public static string GetPaymentGatewayOKResponseURL()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_OK_RESPONSE_URL);
		}

		public static void SetPaymentGatewayOperationHost(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_OPERATION_HOST, value);
		}
		public static string GetPaymentGatewayOperationHost()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_OPERATION_HOST);
		}

		public static void SetPaymentGatewaySignatureKey(string value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_SIGNATURE_KEY, value);
		}
		public static string GetPaymentGatewaySignatureKey()
		{
			return SettingsMng.Instance.SchemaSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PAYMENT_GATEWAY_SIGNATURE_KEY);
		}

		#endregion

		#region User Settings

		public static long GetDefaultSerieSetting()
		{
			try { return Convert.ToInt32(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_SERIE_VENTA)); }
			catch 
			{
				try { return Convert.ToInt32(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_SERIE_VENTA)); }
				catch { return 0; }
			}
		}
		public static void SetDefaultSerieSetting(long value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_SERIE_VENTA, value.ToString());
		}

		public static string GetContabilidadFolder()
		{
			string folder = SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_SALIDA_CONTABILIDAD_FOLDER);
			return (Directory.Exists(folder) ? folder : System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop));
		}
		public static void SetContabilidadFolder(string value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_SALIDA_CONTABILIDAD_FOLDER, value);
		}

		public static void SetNotifyCobros(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_NOTIFY_COBROS, value.ToString());
		}
		public static bool GetNotifyCobros()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_NOTIFY_COBROS)); }
			catch { return false; }
		}

		public static void SetNotifyFacturasEmitidas(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_NOTIFY_FACTURAS_EMITIDAS, value.ToString());
		}
		public static bool GetNotifyFacturasEmitidas()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_NOTIFY_FACTURAS_EMITIDAS)); }
			catch { return false; }
		}

		public static void SetNotifyPlazoCobros(decimal value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_NOTIFY_PLAZO_COBROS, value.ToString());
		}
		public static int GetNotifyPlazoCobros()
		{
			try { return Convert.ToInt32(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_NOTIFY_PLAZO_COBROS)); }
			catch { return 0; }
		}

		public static void SetNotifyPlazoFacturasEmitidas(decimal value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_NOTIFY_PLAZO_FACTURAS_EMITIDAS, value.ToString());
		}
		public static int GetNotifyPlazoFacturasEmitidas()
		{
			try { return Convert.ToInt32(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_NOTIFY_PLAZO_FACTURAS_EMITIDAS)); }
			catch { return 0; }
		}

		public static void SetBalancePrintFacturasExplotacion(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_FACTURAS_EXPLOTACION, value.ToString());
		}
		public static bool GetBalancePrintFacturasExplotacion()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_FACTURAS_EXPLOTACION)); }
			catch { return false; }
		}

		public static void SetBalancePrintFacturasAcreeedores(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_FACTURAS_ACREEDORES, value.ToString());
		}
		public static bool GetBalancePrintFacturasAcreeedores()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_FACTURAS_ACREEDORES)); }
			catch { return false; }
		}

		public static void SetBalancePrintOtrosGastos(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_OTROS_GASTOS, value.ToString());
		}
		public static bool GetBalancePrintOtrosGastos()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_OTROS_GASTOS)); }
			catch { return false; }
		}

		public static void SetBalancePrintGastosNominas(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_GASTOS_NOMINAS, value.ToString());
		}
		public static bool GetBalancePrintGastosNominas()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_GASTOS_NOMINAS)); }
			catch { return false; }
		}

		public static void SetBalancePrintEfectosPendientesVto(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_EFECTOS_PENDIENTES_VTO, value.ToString());
		}
		public static bool GetBalancePrintEfectosPendientesVto()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_EFECTOS_PENDIENTES_VTO)); }
			catch { return false; }
		}

		public static void SetBalancePrintPagosEstimados(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_PAGOS_ESTIMADOS, value.ToString());
		}
		public static bool GetBalancePrintPagosEstimados()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_PAGOS_ESTIMADOS)); }
			catch { return false; }
		}

		public static void SetBalancePrintFacturasEmitidas(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_FACTURAS_EMITIDAS, value.ToString());
		}
		public static bool GetBalancePrintFacturasEmitidas()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_FACTURAS_EMITIDAS)); }
			catch { return false; }
		}

		public static void SetBalancePrintExistencias(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_EXISTENCIAS, value.ToString());
		}
		public static bool GetBalancePrintExistencias()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_EXISTENCIAS)); }
			catch { return false; }
		}

		public static void SetBalancePrintEfectosNegociados(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_EFECTOS_NEGOCIADOS, value.ToString());
		}
		public static bool GetBalancePrintEfectosNegociados()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_EFECTOS_NEGOCIADOS)); }
			catch { return false; }
		}

		public static void SetBalancePrintEfectosPendientes(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_EFECTOS_PENDIENTES, value.ToString());
		}
		public static bool GetBalancePrintEfectosPendientes()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_EFECTOS_PENDIENTES)); }
			catch { return false; }
		}

		public static void SetBalancePrintAyudasPendientes(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_AYUDAS_PENDIENTES, value.ToString());
		}
		public static bool GetBalancePrintAyudasPendientes()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_AYUDAS_PENDIENTES)); }
			catch { return false; }
		}

		public static void SetBalancePrintAyudasCobradas(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_AYUDAS_COBRADAS, value.ToString());
		}
		public static bool GetBalancePrintAyudasCobradas()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_AYUDAS_COBRADAS)); }
			catch { return false; }
		}

		public static void SetBalancePrintBancos(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_BANCOS, value.ToString());
		}
		public static bool GetBalancePrintBancos()
		{
			try { return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_BALANCE_PRINT_BANCOS)); }
			catch { return false; }
        }

        public static string GetDefaultTicketPrinter()
        {
            return SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_DEFAULT_TICKET_PRINTER);
        }

        public static void SetDefaultTicketPrinter(string value)
        {
            SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_DEFAULT_TICKET_PRINTER, value.ToString());
        }

		#endregion
    }
}