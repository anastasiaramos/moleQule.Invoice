using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Globalization;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// ReadOnly Root Business Object with ReadOnly Childs
    /// </summary>
    [Serializable()]
    public class VentasInfo : ReadOnlyBaseEx<VentasInfo>
    {
        #region Attributes

        //Cliente
        protected long _oid_cliente;
        protected string _codigo_cliente = string.Empty;
        protected string _cliente = string.Empty;

        //Producto
        protected long _oid_producto;
        protected string _codigo_producto = string.Empty;
        protected string _producto = string.Empty;

        //Partida
        protected long _oid_partida;
        protected decimal _precio_compra;
        protected decimal _coste;
		protected decimal _gasto;
		protected decimal _ayudas;
        protected decimal _beneficio;
        protected decimal _beneficio_neto;
       
        //Factura
        protected long _oid_factura;
        protected string _factura;
        protected decimal _total_factura;
        protected decimal _kilos;
        protected decimal _total;
        protected decimal _precio_medio_venta;
		protected decimal _precio_venta;

        //Expediente
        protected string _expediente;

		//Stock
		protected decimal _kg_merma;
		protected decimal _uds_merma;
		protected decimal _coste_merma;
		protected decimal _gasto_merma;
		protected decimal _ayuda_merma;
		protected decimal _kg_consumo;
		protected decimal _uds_consumo;
		protected decimal _coste_consumo;
		protected decimal _gasto_consumo;
		protected decimal _ayuda_consumo;

        protected string _anio = string.Empty;
		protected string _mes = string.Empty; 
		protected long _mes_d;
		protected long _anio_d;

		protected DateTime _fecha;

		protected decimal _venta_total;
		protected decimal _p_venta_absoluto;
		protected decimal _p_beneficio_absoluto;
        protected decimal _p_beneficio_absoluto_neto;
        protected decimal _p_beneficio;
        protected decimal _p_beneficio_neto;
        protected decimal _gastos_demora;
        protected decimal _gastos_cobro;

        #endregion

        #region Propiedades

        public long OidCliente { get { return _oid_cliente; } }
        public string CodigoCliente { get { return _codigo_cliente; } }
        public string Cliente { get { return _cliente; } }

        public long OidProducto { get { return _oid_producto; } }
        public string CodigoProducto { get { return _codigo_producto; } }
        public string Producto { get { return _producto; } }

		public long OidPartida { get { return _oid_partida; } }
        public long OidFactura { get { return _oid_factura; } }
        public string Factura { get { return _factura; } }
        public Decimal TotalFactura { get { return Decimal.Round(_total_factura, 2); } }
        public Decimal PrecioCompra { get { return _precio_compra; } }
		public Decimal PrecioVenta { get { return _precio_venta; } }
		public Decimal Coste { get { return Decimal.Round(_coste, 2); } }
		public Decimal Gasto { get { return Decimal.Round(_gasto, 2); } }
		public Decimal Ayudas { get { return Decimal.Round(_ayudas, 2); } }
		public Decimal TotalIngresos { get { return Decimal.Round(_total + _ayudas, 2); } }
		public Decimal TotalGasto { get { return Decimal.Round(_coste + _gasto, 2); } }
        public Decimal Beneficio { get { return Decimal.Round(_beneficio, 2); } }
		public Decimal PBenificio { get { return Decimal.Round(_p_beneficio, 2); } }
        public Decimal BeneficioNeto { get { return Decimal.Round(_beneficio_neto, 2); } }
        public Decimal PBeneficioNeto { get { return Decimal.Round(_p_beneficio_neto, 2); } }
        public Decimal PrecioMedioVenta { get { return (_kilos != 0) ? _total / _kilos : 0; } }

        public Decimal Kilos { get { return _kilos; } }
        public Decimal Total { get { return _total; } }

		public Decimal KgMerma { get { return Decimal.Round(_kg_merma, 2); } }
		public Decimal UdsMerma { get { return Decimal.Round(_uds_merma, 2); } }
		public Decimal CosteMerma { get { return Decimal.Round(_coste_merma, 2); } }
		public Decimal GastoMerma { get { return Decimal.Round(_gasto_merma, 2); } }
		public Decimal AyudaMerma { get { return Decimal.Round(_ayuda_merma, 2); } }
		public Decimal KgConsumo { get { return Decimal.Round(_kg_consumo, 2); } }
		public Decimal UdsConsumo { get { return Decimal.Round(_uds_consumo, 2); } }
		public Decimal CosteConsumo { get { return Decimal.Round(_coste_consumo, 2); } }
		public Decimal GastoConsumo { get { return Decimal.Round(_gasto_consumo, 2); } }
		public Decimal AyudaConsumo { get { return Decimal.Round(_ayuda_consumo, 2); } }

        public string Expediente { get { return _expediente; } }

        public string Anio { get { return _anio; } }
        public string Mes { get { return _mes; } }
		public long MesD { get { return _mes_d; } }
		public long AnioD { get { return _anio_d; } }
		public DateTime Fecha { get { return _fecha; } }

		public decimal VentaTotal { get { return _venta_total; } }
		public decimal PVentaAbsoluto { get { return _p_venta_absoluto; } }
		public decimal PBeneficioAbsoluto { get { return _p_beneficio_absoluto; } }
        public decimal PBeneficioAbsolutoNeto { get { return _p_beneficio_absoluto_neto; } }
        public decimal GastosDemora { get { return _gastos_demora; } }
        public decimal GastosCobro { get { return _gastos_cobro; } }

        #endregion

        #region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;

			long tipo_query = Format.DataReader.GetInt64(source, "TIPO_QUERY");

			switch ((VentasInfo.ETipoQuery)tipo_query)
			{
				case VentasInfo.ETipoQuery.VENTA:
				case VentasInfo.ETipoQuery.VENTA_DETALLADA_CLIENTE:
				case VentasInfo.ETipoQuery.VENTA_DETALLADA_PRODUCTO:
					{
						Oid = Convert.ToInt64(Format.DataReader.GetDouble(source, "OID"));
						_oid_factura = Format.DataReader.GetInt64(source, "OID_FACTURA");
						_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
						_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
						_oid_partida = Format.DataReader.GetInt64(source, "OID_BATCH");
						_precio_compra = Format.DataReader.GetDecimal(source, "PCD");
						_factura = Format.DataReader.GetString(source, "N_FACTURA");
						_expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
						_producto = Format.DataReader.GetString(source, "PRODUCTO");
						_codigo_cliente = Format.DataReader.GetString(source, "CODIGO_CLIENTE");
						_cliente = Format.DataReader.GetString(source, "CLIENTE");
						_kilos = Format.DataReader.GetDecimal(source, "KILOS");
						_coste = Format.DataReader.GetDecimal(source, "COSTE_TOTAL");
						_gasto = Format.DataReader.GetDecimal(source, "GASTO_TOTAL");
						_total = Format.DataReader.GetDecimal(source, "VENTA_TOTAL");
						_ayudas = Format.DataReader.GetDecimal(source, "AYUDA_TOTAL");
                        _beneficio = Decimal.Round(Decimal.Round(_total + _ayudas, 2) - Decimal.Round(_coste + _gasto, 2), 2); //Format.DataReader.GetDecimal(source, "BENEFICIO");
                        _p_beneficio = Decimal.Round((Decimal.Round(_total + _ayudas, 2) != 0) ? (_beneficio / Decimal.Round(_total + _ayudas, 2) * 100) : 0, 2);
						_kg_merma = Format.DataReader.GetDecimal(source, "KG_MERMA");
						_uds_merma = Format.DataReader.GetDecimal(source, "UDS_MERMA");
						_coste_merma = Format.DataReader.GetDecimal(source, "COSTE_MERMA");
						_gasto_merma = Format.DataReader.GetDecimal(source, "GASTO_MERMA");
						_ayuda_merma = Format.DataReader.GetDecimal(source, "AYUDA_MERMA");
						_kg_consumo = Format.DataReader.GetDecimal(source, "KG_CONSUMO");
						_uds_consumo = Format.DataReader.GetDecimal(source, "UDS_CONSUMO");
						_coste_consumo = Format.DataReader.GetDecimal(source, "COSTE_CONSUMO");
						_gasto_consumo = Format.DataReader.GetDecimal(source, "GASTO_CONSUMO");
						_ayuda_consumo = Format.DataReader.GetDecimal(source, "AYUDA_CONSUMO");

						_venta_total = Total;
					}
					break;

				case VentasInfo.ETipoQuery.VENTA_MENSUAL:
				case VentasInfo.ETipoQuery.VENTA_CLIENTE_MENSUAL:
				case VentasInfo.ETipoQuery.VENTA_PRODUCTO_MENSUAL:
					{
						Oid = Convert.ToInt64(Format.DataReader.GetDouble(source, "OID"));
						_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
						_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
						_producto = Format.DataReader.GetString(source, "PRODUCTO");
						_codigo_cliente = Format.DataReader.GetString(source, "CODIGO_CLIENTE");
						_cliente = Format.DataReader.GetString(source, "CLIENTE");
						_kilos = Format.DataReader.GetDecimal(source, "KILOS");
						_coste = Format.DataReader.GetDecimal(source, "COSTE_TOTAL");
						_gasto = Format.DataReader.GetDecimal(source, "GASTO_TOTAL");
						_total = Format.DataReader.GetDecimal(source, "VENTA_TOTAL");
						_ayudas = Format.DataReader.GetDecimal(source, "AYUDA_TOTAL");
                        _beneficio = Decimal.Round(Decimal.Round(_total + _ayudas, 2) - Decimal.Round(_coste + _gasto, 2), 2); //Format.DataReader.GetDecimal(source, "BENEFICIO");
                        _p_beneficio = Decimal.Round((Decimal.Round(_total + _ayudas, 2) != 0) ? (_beneficio / Decimal.Round(_total + _ayudas, 2) * 100) : 0, 2);
						_mes = Format.DataReader.GetString(source, "MES");
						_anio = Format.DataReader.GetString(source, "ANIO");
						
						_mes_d = Convert.ToInt64(_mes);
						_anio_d = Convert.ToInt64(_anio);
						_mes = _mes + "/" + _anio;
					}
					break;

				case VentasInfo.ETipoQuery.HISTORICO_PRECIOS:
					{
						Oid = Convert.ToInt64(Format.DataReader.GetDouble(source, "OID"));
						_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
						_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
						_producto = Format.DataReader.GetString(source, "PRODUCTO");
						_codigo_cliente = Format.DataReader.GetInt64(source, "CODIGO_CLIENTE").ToString(Resources.Defaults.CLIENTE_CODE_FORMAT);
						_codigo_producto = Format.DataReader.GetString(source, "CODIGO_PRODUCTO");
						_cliente = Format.DataReader.GetString(source, "CLIENTE");
						_kilos = Format.DataReader.GetDecimal(source, "KILOS");
						_total = Format.DataReader.GetDecimal(source, "VENTA_TOTAL");
						_precio_venta = Format.DataReader.GetDecimal(source, "PRECIO_VENTA");
						_fecha = Format.DataReader.GetDateTime(source, "FECHA");
					}
					break;

				case VentasInfo.ETipoQuery.VENTA_PORCENTUAL:
				case VentasInfo.ETipoQuery.VENTA_CLIENTE_PORCENTUAL_MENSUAL:
				case VentasInfo.ETipoQuery.VENTA_PRODUCTO_PORCENTUAL:
					{
						Oid = Convert.ToInt64(Format.DataReader.GetDouble(source, "OID"));

						_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
						_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");

						_mes = Format.DataReader.GetString(source, "MES");
						_anio = Format.DataReader.GetString(source, "ANIO");
						_mes_d = Convert.ToInt64(_mes);
						_anio_d = Convert.ToInt64(_anio);
						_mes = _mes + "/" + _anio;

						_producto = Format.DataReader.GetString(source, "PRODUCTO");
						_codigo_cliente = Format.DataReader.GetString(source, "CODIGO_CLIENTE");
						_cliente = Format.DataReader.GetString(source, "CLIENTE");
						_kilos = Format.DataReader.GetDecimal(source, "KILOS");
						_coste = Format.DataReader.GetDecimal(source, "COSTE_TOTAL");
						_gasto = Format.DataReader.GetDecimal(source, "GASTO_TOTAL");
						_total = Format.DataReader.GetDecimal(source, "VENTA_TOTAL");
						_ayudas = Format.DataReader.GetDecimal(source, "AYUDA_TOTAL");
                        _beneficio = Format.DataReader.GetDecimal(source, "BENEFICIO");
                        _p_beneficio = Decimal.Round((_total != 0) ? (_beneficio / _total * 100) : 0, 2);
                        _gastos_demora = Format.DataReader.GetDecimal(source, "GASTOS_DEMORA");
                        _gastos_cobro = Format.DataReader.GetDecimal(source, "GASTOS_COBRO");
                        _beneficio_neto = Decimal.Round(Format.DataReader.GetDecimal(source, "BENEFICIO_NETO"), 2);
                        _p_beneficio_neto = Decimal.Round((_total != 0) ? (_beneficio_neto / _total * 100) : 0, 2);
						_venta_total = Format.DataReader.GetDecimal(source, "VENTA_ABSOLUTA");
						_p_venta_absoluto = Format.DataReader.GetDecimal(source, "P_VENTA_ABSOLUTA");
						_p_beneficio_absoluto = Decimal.Round(Format.DataReader.GetDecimal(source, "P_BENEFICIO_ABSOLUTO"), 2);
                        _p_beneficio_absoluto_neto = Decimal.Round(Format.DataReader.GetDecimal(source, "P_BENEFICIO_ABSOLUTO_NETO"), 2);
					}
                    break;

                case VentasInfo.ETipoQuery.VENTA_CLIENTE_PORCENTUAL_PERIODO:
                    {
                        Oid = Convert.ToInt64(Format.DataReader.GetDouble(source, "OID"));

                        _oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
                        _oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");

                        _producto = Format.DataReader.GetString(source, "PRODUCTO");
                        _codigo_cliente = Format.DataReader.GetString(source, "CODIGO_CLIENTE");
                        _cliente = Format.DataReader.GetString(source, "CLIENTE");
                        _kilos = Format.DataReader.GetDecimal(source, "KILOS");
                        _coste = Format.DataReader.GetDecimal(source, "COSTE_TOTAL");
                        _gasto = Format.DataReader.GetDecimal(source, "GASTO_TOTAL");
                        _total = Format.DataReader.GetDecimal(source, "VENTA_TOTAL");
                        _ayudas = Format.DataReader.GetDecimal(source, "AYUDA_TOTAL");
                        _beneficio = Format.DataReader.GetDecimal(source, "BENEFICIO");
                        _p_beneficio = Decimal.Round((_total != 0) ? (_beneficio / _total * 100) : 0, 2);
                        _gastos_demora = Format.DataReader.GetDecimal(source, "GASTOS_DEMORA_CLIENTE");
                        _gastos_cobro = Format.DataReader.GetDecimal(source, "GASTOS_COBRO_CLIENTE");
                        _beneficio_neto = Decimal.Round(Format.DataReader.GetDecimal(source, "BENEFICIO_NETO"), 2);
                        _p_beneficio_neto = Decimal.Round((_total != 0) ? (_beneficio_neto / _total * 100) : 0, 2);
                        _venta_total = Format.DataReader.GetDecimal(source, "VENTA_ABSOLUTA");
                        _p_venta_absoluto = Format.DataReader.GetDecimal(source, "P_VENTA_ABSOLUTA");
                        _p_beneficio_absoluto = Decimal.Round(Format.DataReader.GetDecimal(source, "P_BENEFICIO_ABSOLUTO"), 2);
                        _p_beneficio_absoluto_neto = Decimal.Round(Format.DataReader.GetDecimal(source, "P_BENEFICIO_ABSOLUTO_NETO"), 2);
                    }
                    break;
			}
        }

        #endregion
        
        #region Factory Methods

        protected VentasInfo() { /* require use of factory methods */ }

        public static VentasInfo Get(IDataReader reader)
        {
            VentasInfo item = new VentasInfo();
            item.CopyValues(reader);
            return item;
        }

        #endregion

		#region SQL

		internal enum ETipoQuery
		{
			VENTA = 0, VENTA_DETALLADA_CLIENTE = 3, VENTA_DETALLADA_PRODUCTO = 4,
			VENTA_MENSUAL = 5, VENTA_CLIENTE_MENSUAL = 6, VENTA_PRODUCTO_MENSUAL = 7,
			HISTORICO_PRECIOS = 1, 
			VENTA_PORCENTUAL = 2, VENTA_CLIENTE_PORCENTUAL_MENSUAL = 8, VENTA_PRODUCTO_PORCENTUAL = 9,
            VENTA_CLIENTE_PORCENTUAL_PERIODO = 10
		}

		internal static string SELECT_FIELDS(ETipoQuery tipo)
		{
			string query = string.Empty;

			query = "SELECT " + (long)tipo + " AS \"TIPO_QUERY\"" +
					"		,random() * 10000000000 AS \"OID\"";

			string p_compra = string.Empty;
			string beneficio = string.Empty;

            //p_compra = "CASE WHEN (COALESCE(P.\"BENEFICIO_CERO\", FALSE) = TRUE)" +
            //            " THEN COALESCE(PC.\"PRECIO_COMPRA\", COALESCE(CF.\"PRECIO_VENTA\",0))" +
            //            " ELSE COALESCE(PE.\"PRECIO_COMPRA_KILO\", COALESCE(PC.\"PRECIO_COMPRA\", COALESCE(P.\"PRECIO_COMPRA\",0))) END";
            p_compra = "COALESCE(PE.\"PRECIO_COMPRA_KILO\", COALESCE(PC.\"PRECIO_COMPRA\", COALESCE(P.\"PRECIO_COMPRA\",0)))";
            
			beneficio = "SUM(\"SUBTOTAL\" - (" + p_compra + " - COALESCE(PE.\"GASTO_KILO\",0) + COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\")";

			switch (tipo)
			{
				case ETipoQuery.VENTA:

					query +=
							"		,C.\"OID\" AS \"OID_CLIENTE\"" +
							"       ,C.\"CODIGO\" AS \"CODIGO_CLIENTE\"" +
							"       ,C.\"NOMBRE\" AS \"CLIENTE\"" +
							"       ,P.\"OID\" AS \"OID_PRODUCTO\"" +
							"       ,P.\"NOMBRE\" AS \"PRODUCTO\"" +
							"       ,CF.\"OID_BATCH\" AS \"OID_BATCH\"" +
							"       ,E.\"CODIGO\" AS \"EXPEDIENTE\"" +
							"       ,F.\"OID\" AS \"OID_FACTURA\"" +
							"       ,(S.\"IDENTIFICADOR\" || '/' || F.\"CODIGO\") AS \"N_FACTURA\"" +
							"       ,SUM(CF.\"KILOS\") AS \"KILOS\"" +
							"       ,AVG(" + p_compra + ") AS \"PCD\"" +
							"       ,SUM(" + p_compra + " * CF.\"KILOS\") AS \"COSTE_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"GASTO_KILO\",0) * CF.\"KILOS\") AS \"GASTO_TOTAL\"" +
							"       ,SUM(\"SUBTOTAL\") AS \"VENTA_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"AYUDA_KILO\",0) * CF.\"KILOS\") AS \"AYUDA_TOTAL\"" +
							"		," + beneficio + " AS \"BENEFICIO\"" +
							"       ,MAX(ABS(ST1.\"KG_MERMA\")) AS \"KG_MERMA\"" +
							"       ,MAX(ABS(ST1.\"UDS_MERMA\")) AS \"UDS_MERMA\"" +
							"       ,MAX(ABS(ST1.\"COSTE_MERMA\")) AS \"COSTE_MERMA\"" +
							"		,MAX(ABS(ST1.\"GASTO_MERMA\")) AS \"GASTO_MERMA\"" +
							"		,MAX(ABS(ST1.\"AYUDA_MERMA\")) AS \"AYUDA_MERMA\"" +
							"       ,MAX(ABS(ST2.\"KG_CONSUMO\")) AS \"KG_CONSUMO\"" +
							"       ,MAX(ABS(ST2.\"UDS_CONSUMO\")) AS \"UDS_CONSUMO\"" +
							"       ,MAX(ABS(ST2.\"COSTE_CONSUMO\")) AS \"COSTE_CONSUMO\"" +
							"		,MAX(ABS(ST2.\"GASTO_CONSUMO\")) AS \"GASTO_CONSUMO\"" +
							"		,MAX(ABS(ST2.\"AYUDA_CONSUMO\")) AS \"AYUDA_CONSUMO\"";
					break;

				case ETipoQuery.VENTA_DETALLADA_CLIENTE:

					query +=
							"		,C.\"OID\" AS \"OID_CLIENTE\"" +
							"       ,C.\"CODIGO\" AS \"CODIGO_CLIENTE\"" +
							"       ,C.\"NOMBRE\" AS \"CLIENTE\"" +
							"       ,P.\"OID\" AS \"OID_PRODUCTO\"" +
							"       ,CASE WHEN PE2.\"TIPO_MERCANCIA\" <> '' THEN P.\"NOMBRE\" || ' (' || PE2.\"TIPO_MERCANCIA\" || ')' ELSE P.\"NOMBRE\" END AS \"PRODUCTO\"" +
							"       ,CF.\"OID_BATCH\" AS \"OID_BATCH\"" +
							"       ,E.\"CODIGO\" AS \"EXPEDIENTE\"" +
							"       ,F.\"OID\" AS \"OID_FACTURA\"" +
							"		,(S.\"IDENTIFICADOR\" || '/' || F.\"CODIGO\") AS \"N_FACTURA\"" +
							"       ,SUM(CF.\"KILOS\") AS \"KILOS\"" +
							"       ,AVG(" + p_compra + ") AS \"PCD\"" +
							"       ,SUM(" + p_compra + " * CF.\"KILOS\") AS \"COSTE_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"GASTO_KILO\",0) * CF.\"KILOS\") AS \"GASTO_TOTAL\"" +
							"       ,SUM(\"SUBTOTAL\") AS \"VENTA_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"AYUDA_KILO\",0) * CF.\"KILOS\") AS \"AYUDA_TOTAL\"" +
							"		," + beneficio + " AS \"BENEFICIO\"" +
							"       ,MAX(ABS(ST1.\"KG_MERMA\")) AS \"KG_MERMA\"" +
							"       ,MAX(ABS(ST1.\"UDS_MERMA\")) AS \"UDS_MERMA\"" +
							"       ,MAX(ABS(ST1.\"COSTE_MERMA\")) AS \"COSTE_MERMA\"" +
							"		,MAX(ABS(ST1.\"GASTO_MERMA\")) AS \"GASTO_MERMA\"" +
							"		,MAX(ABS(ST1.\"AYUDA_MERMA\")) AS \"AYUDA_MERMA\"" +
							"       ,MAX(ABS(ST2.\"KG_CONSUMO\")) AS \"KG_CONSUMO\"" +
							"       ,MAX(ABS(ST2.\"UDS_CONSUMO\")) AS \"UDS_CONSUMO\"" +
							"       ,MAX(ABS(ST2.\"COSTE_CONSUMO\")) AS \"COSTE_CONSUMO\"" +
							"		,MAX(ABS(ST2.\"GASTO_CONSUMO\")) AS \"GASTO_CONSUMO\"" +
							"		,MAX(ABS(ST2.\"AYUDA_CONSUMO\")) AS \"AYUDA_CONSUMO\"";


					break;

				case ETipoQuery.VENTA_DETALLADA_PRODUCTO:

					query +=
							"		,C.\"OID\" AS \"OID_CLIENTE\"" +
							"		,C.\"CODIGO\" AS \"CODIGO_CLIENTE\"" +
							"       ,CASE WHEN PE2.\"TIPO_MERCANCIA\" <> '' THEN C.\"NOMBRE\" || ' (ESCANDALLO)' ELSE C.\"NOMBRE\" END AS \"CLIENTE\"" +
							"       ,P.\"OID\" AS \"OID_PRODUCTO\"" +
							"		,P.\"NOMBRE\" AS \"PRODUCTO\"" +
							"       ,CF.\"OID_BATCH\" AS \"OID_BATCH\"" +
							"       ,E.\"CODIGO\" AS \"EXPEDIENTE\"" +
							"       ,F.\"OID\" AS \"OID_FACTURA\"" +
							"		,(S.\"IDENTIFICADOR\" || '/' || F.\"CODIGO\") AS \"N_FACTURA\"" +
							"       ,SUM(\"KILOS\") AS \"KILOS\"" +
							"       ,AVG(" + p_compra + ") AS \"PCD\"" +
							"       ,SUM(" + p_compra + " * CF.\"KILOS\") AS \"COSTE_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"GASTO_KILO\",0) * CF.\"KILOS\") AS \"GASTO_TOTAL\"" +
							"       ,SUM(\"SUBTOTAL\") AS \"VENTA_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"AYUDA_KILO\",0) * CF.\"KILOS\") AS \"AYUDA_TOTAL\"" +
							"		," + beneficio + " AS \"BENEFICIO\"" +
							"       ,MAX(ABS(ST1.\"KG_MERMA\")) AS \"KG_MERMA\"" +
							"       ,MAX(ABS(ST1.\"UDS_MERMA\")) AS \"UDS_MERMA\"" +
							"       ,MAX(ABS(ST1.\"COSTE_MERMA\")) AS \"COSTE_MERMA\"" +
							"		,MAX(ABS(ST1.\"GASTO_MERMA\")) AS \"GASTO_MERMA\"" +
							"		,MAX(ABS(ST1.\"AYUDA_MERMA\")) AS \"AYUDA_MERMA\"" +
							"       ,MAX(ABS(ST2.\"KG_CONSUMO\")) AS \"KG_CONSUMO\"" +
							"       ,MAX(ABS(ST2.\"UDS_CONSUMO\")) AS \"UDS_CONSUMO\"" +
							"       ,MAX(ABS(ST2.\"COSTE_CONSUMO\")) AS \"COSTE_CONSUMO\"" +
							"		,MAX(ABS(ST2.\"GASTO_CONSUMO\")) AS \"GASTO_CONSUMO\"" +
							"		,MAX(ABS(ST2.\"AYUDA_CONSUMO\")) AS \"AYUDA_CONSUMO\"";


					break;

				case ETipoQuery.VENTA_MENSUAL:

					query +=
							"       ,0 AS \"OID_CLIENTE\"" +
							"       ,'' AS \"CODIGO_CLIENTE\"" +
							"       ,'' AS \"CLIENTE\"" +
							"       ,0 AS \"OID_PRODUCTO\"" +
							"       ,'' AS \"PRODUCTO\"" +
							"       ,SUM(\"KILOS\") AS \"KILOS\"" +
							"       ,SUM(" + p_compra + " * CF.\"KILOS\") AS \"COSTE_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"GASTO_KILO\",0) * CF.\"KILOS\") AS \"GASTO_TOTAL\"" +
							"       ,SUM(\"SUBTOTAL\") AS \"VENTA_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"AYUDA_KILO\",0) * CF.\"KILOS\") AS \"AYUDA_TOTAL\"" +
							"       ,SUM(\"SUBTOTAL\" + (" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\") AS \"BENEFICIO\"" +
							"       ,F.\"MES\" AS \"MES\"" +
							"		,F.\"ANIO\" AS \"ANIO\"";

					break;

				case ETipoQuery.VENTA_CLIENTE_MENSUAL:

					query +=
							"		,C.\"OID\" AS \"OID_CLIENTE\"" +
							"       ,C.\"CODIGO\" AS \"CODIGO_CLIENTE\"" +
							"       ,C.\"NOMBRE\" AS \"CLIENTE\"" +
							"       ,0 AS \"OID_PRODUCTO\"" +
							"       ,'' AS \"PRODUCTO\"" +
							"       ,SUM(\"KILOS\") AS \"KILOS\"" +
							"       ,SUM(" + p_compra + " * CF.\"KILOS\") AS \"COSTE_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"GASTO_KILO\",0) * CF.\"KILOS\") AS \"GASTO_TOTAL\"" +
							"       ,SUM(\"SUBTOTAL\") AS \"VENTA_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"AYUDA_KILO\",0) * CF.\"KILOS\") AS \"AYUDA_TOTAL\"" +
							"       ,SUM(\"SUBTOTAL\" - (" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\") AS \"BENEFICIO\"" +
							"       ,F.\"MES\" AS \"MES\"" +
							"		,F.\"ANIO\" AS \"ANIO\"";

					break;

				case ETipoQuery.VENTA_PRODUCTO_MENSUAL:

					query +=
							"		,0 AS \"OID_CLIENTE\"" +
							"       ,'' AS \"CODIGO_CLIENTE\"" +
							"       ,'' AS \"CLIENTE\"" +
							"       ,P.\"OID\" AS \"OID_PRODUCTO\"" +
							"       ,P.\"NOMBRE\" AS \"PRODUCTO\"" +
							"       ,SUM(\"KILOS\") AS \"KILOS\"" +
							"       ,SUM(" + p_compra + " * CF.\"KILOS\") AS \"COSTE_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"GASTO_KILO\",0) * CF.\"KILOS\") AS \"GASTO_TOTAL\"" +
							"       ,SUM(\"SUBTOTAL\") AS \"VENTA_TOTAL\"" +
							"		,SUM(COALESCE(PE.\"AYUDA_KILO\",0) * CF.\"KILOS\") AS \"AYUDA_TOTAL\"" +
							"       ,SUM(\"SUBTOTAL\" - (COALESCE(PE.\"PRECIO_COMPRA_KILO\",0) + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\") AS \"BENEFICIO\"" +
							"       ,F.\"MES\" AS \"MES\"" +
							"		,F.\"ANIO\" AS \"ANIO\"";

					break;

				case ETipoQuery.VENTA_CLIENTE_PORCENTUAL_MENSUAL:

                    query +=
                        "		,C.\"OID\" AS \"OID_CLIENTE\"" +
                        "       ,C.\"CODIGO\" AS \"CODIGO_CLIENTE\"" +
                        "       ,C.\"NOMBRE\" AS \"CLIENTE\"" +
                        "       ,0 AS \"OID_PRODUCTO\"" +
                        "       ,'' AS \"PRODUCTO\"" +
                        "		,F.\"MES\" AS \"MES\"" +
                        "		,F.\"ANIO\" AS \"ANIO\"" +
                        "       ,SUM(CF.\"KILOS\") AS \"KILOS\"" +
                        "       ,SUM(CF.\"SUBTOTAL\") AS \"VENTA_TOTAL\"" +
                        "       ,SUM(COALESCE(PE.\"COSTE_KILO\",0) * CF.\"KILOS\") AS \"COSTE_TOTAL\"" +
                        "		,SUM(COALESCE(PE.\"AYUDA_KILO\",0) * CF.\"KILOS\") AS \"AYUDA_TOTAL\"" +
                        "		,SUM(COALESCE(PE.\"GASTO_KILO\",0) * CF.\"KILOS\") AS \"GASTO_TOTAL\"" +
                        "       ,COALESCE(GD.\"GASTOS_DEMORA\", 0) AS \"GASTOS_DEMORA_CLIENTE\"" +
                        "       ,COALESCE(GC.\"GASTOS_COBRO\", 0) AS \"GASTOS_COBRO_CLIENTE\"" +
                        "       ,SUM(CF.\"SUBTOTAL\" - (" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\") AS \"BENEFICIO\"" +
                        "		,CASE WHEN (VAB.\"VENTA_ABSOLUTA\" != 0) THEN (SUM(CF.\"SUBTOTAL\") / VAB.\"VENTA_ABSOLUTA\"  * 100) ELSE 0 END AS \"P_VENTA_ABSOLUTA\"" +
                        "		,VAB.\"BENEFICIO_ABSOLUTO\" AS \"BENEFICIO_ABSOLUTO\"" +
                        "       ,VAN.\"BENEFICIO_ABSOLUTO_NETO\" AS \"BENEFICIO_ABSOLUTO_NETO\"" +
                        "		,(SUM(CF.\"SUBTOTAL\" - ((" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\")) / VAB.\"BENEFICIO_ABSOLUTO\" * 100) AS \"P_BENEFICIO_ABSOLUTO\"" +
                        "       ,SUM(CF.\"SUBTOTAL\" - (" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\") - COALESCE(GC.\"GASTOS_COBRO\",0) - COALESCE(GD.\"GASTOS_DEMORA\",0) AS \"BENEFICIO_NETO\"" +
                        "       ,((SUM(CF.\"SUBTOTAL\" - (" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\") - COALESCE(GC.\"GASTOS_COBRO\",0) - COALESCE(GD.\"GASTOS_DEMORA\",0)) / VAN.\"BENEFICIO_ABSOLUTO_NETO\" * 100) AS \"P_BENEFICIO_ABSOLUTO_NETO\"" +
                        "       ,VAB.\"VENTA_ABSOLUTA\" AS \"VENTA_ABSOLUTA\"" +
                        "       ,COALESCE(GD.\"GASTOS_DEMORA\", 0) AS \"GASTOS_DEMORA\"" +
                        "       ,COALESCE(GC.\"GASTOS_COBRO\", 0) AS \"GASTOS_COBRO\"";

                    break;

                case ETipoQuery.VENTA_CLIENTE_PORCENTUAL_PERIODO:

					query +=
                        "		,C.\"OID\" AS \"OID_CLIENTE\"" +
                        "       ,C.\"CODIGO\" AS \"CODIGO_CLIENTE\"" +
                        "       ,C.\"NOMBRE\" AS \"CLIENTE\"" +
                        "       ,0 AS \"OID_PRODUCTO\"" +
                        "       ,'' AS \"PRODUCTO\"" +
                        "       ,SUM(CF.\"KILOS\") AS \"KILOS\"" +
                        "       ,SUM(CF.\"SUBTOTAL\") AS \"VENTA_TOTAL\"" +
                        "       ,SUM(COALESCE(PE.\"COSTE_KILO\",0) * CF.\"KILOS\") AS \"COSTE_TOTAL\"" +
                        "		,SUM(COALESCE(PE.\"AYUDA_KILO\",0) * CF.\"KILOS\") AS \"AYUDA_TOTAL\"" +
                        "		,SUM(COALESCE(PE.\"GASTO_KILO\",0) * CF.\"KILOS\") AS \"GASTO_TOTAL\"" +
						"       ,SUM(COALESCE(GD.\"GASTOS_DEMORA\", 0)) AS \"GASTOS_DEMORA_CLIENTE\"" +
						"       ,SUM(COALESCE(GC.\"GASTOS_COBRO\", 0)) AS \"GASTOS_COBRO_CLIENTE\"" +
                        "       ,SUM(CF.\"SUBTOTAL\" - (" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\") AS \"BENEFICIO\"" +
                        "		,VAB.\"VENTA_ABSOLUTA\" AS \"VENTA_ABSOLUTA\"" +
                        "		,CASE WHEN (VAB.\"VENTA_ABSOLUTA\" != 0) THEN (SUM(CF.\"SUBTOTAL\") / VAB.\"VENTA_ABSOLUTA\"  * 100) ELSE 0 END AS \"P_VENTA_ABSOLUTA\"" +
                        "		,VAB.\"BENEFICIO_ABSOLUTO\" AS \"BENEFICIO_ABSOLUTO\"" +
                        "       ,VAN.\"BENEFICIO_ABSOLUTO_NETO\" AS \"BENEFICIO_ABSOLUTO_NETO\"" +
                        "		,(SUM(CF.\"SUBTOTAL\" - ((" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\")) / VAB.\"BENEFICIO_ABSOLUTO\" * 100) AS \"P_BENEFICIO_ABSOLUTO\"" +
                        "       ,SUM(CF.\"SUBTOTAL\" - (" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\") - COALESCE(GC.\"GASTOS_COBRO\",0) - COALESCE(GD.\"GASTOS_DEMORA\",0) AS \"BENEFICIO_NETO\"" +
                        "       ,((SUM(CF.\"SUBTOTAL\" - (" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\") - COALESCE(GC.\"GASTOS_COBRO\",0) - COALESCE(GD.\"GASTOS_DEMORA\",0)) / VAN.\"BENEFICIO_ABSOLUTO_NETO\" * 100) AS \"P_BENEFICIO_ABSOLUTO_NETO\"";

                    break;

				case ETipoQuery.VENTA_PRODUCTO_PORCENTUAL:

                    query +=
                    "		,0 AS \"OID_CLIENTE\"" +
                    "       ,'' AS \"CODIGO_CLIENTE\"" +
                    "       ,'' AS \"CLIENTE\"" +
                    "       ,P.\"OID\" AS \"OID_PRODUCTO\"" +
                    "       ,P.\"NOMBRE\" AS \"PRODUCTO\"" +
                    "       ,SUM(CF.\"KILOS\") AS \"KILOS\"" +
                    "       ,SUM(\"SUBTOTAL\") AS \"VENTA_TOTAL\"" +
                    "       ,SUM(COALESCE(PE.\"COSTE_KILO\",0) * CF.\"KILOS\") AS \"COSTE_TOTAL\"" +
                    "		,SUM(COALESCE(PE.\"AYUDA_KILO\",0) * CF.\"KILOS\") AS \"AYUDA_TOTAL\"" +
                    "		,SUM(COALESCE(PE.\"GASTO_KILO\",0) * CF.\"KILOS\") AS \"GASTO_TOTAL\"" +
                    "       ,SUM(\"SUBTOTAL\" - (" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\") AS \"BENEFICIO\"" +
                    "       ,F.\"MES\" AS \"MES\"" +
                    "		,F.\"ANIO\" AS \"ANIO\"" +
                    "		,VAB.\"VENTA_ABSOLUTA\" AS \"VENTA_ABSOLUTA\"" +
                    "		,CASE WHEN (VAB.\"VENTA_ABSOLUTA\" != 0) THEN (SUM(CF.\"SUBTOTAL\") / VAB.\"VENTA_ABSOLUTA\"  * 100) ELSE 0 END AS \"P_VENTA_ABSOLUTA\"" +
                    "		,VAB.\"BENEFICIO_ABSOLUTO\" AS \"BENEFICIO_ABSOLUTO\"" +
                    "		,(SUM(\"SUBTOTAL\" - ((" + p_compra + " + COALESCE(PE.\"GASTO_KILO\",0) - COALESCE(PE.\"AYUDA_KILO\",0)) * CF.\"KILOS\")) / VAB.\"BENEFICIO_ABSOLUTO\" * 100) AS \"P_BENEFICIO_ABSOLUTO\"" +
                    "       ,0 AS \"GASTOS_DEMORA\"" +
                    "       ,0 AS \"GASTOS_COBRO\"" +
                    "       ,0 AS \"BENEFICIO_NETO\"" +
                    "       ,0 AS \"P_BENEFICIO_ABSOLUTO_NETO\"";

					break;
			}

			return query;
		}

		internal static string JOIN_PARTIDA()
		{
			string pa = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));

			string query = string.Empty;

			query = " LEFT JOIN (SELECT PA.*" +
					"					,CASE WHEN (PA.\"AYUDA\" = TRUE) THEN (CASE WHEN (PA.\"AYUDA_RECIBIDA_KILO\" != 0) THEN PA.\"AYUDA_RECIBIDA_KILO\" ELSE PR.\"AYUDA_KILO\" END)" +
					"						ELSE 0" +
					"						END AS \"AYUDA_KILO\"" +
					"			FROM " + pa + " AS PA " +
					"			INNER JOIN " + pr + " AS PR ON PR.\"OID\" = PA.\"OID_PRODUCTO\")";

			return query;
		}
		internal static string JOIN_MERMAS()
		{
			string st = nHManager.Instance.GetSQLTable(typeof(StockRecord));
			string pa = nHManager.Instance.GetSQLTable(typeof(BatchRecord));

			string query = string.Empty;

			query = " LEFT JOIN (SELECT ST.\"OID_EXPEDIENTE\"" +
					"					,SUM(ST.\"KILOS\") AS \"KG_MERMA\"" +
					"					,SUM(ST.\"BULTOS\") AS \"UDS_MERMA\"" +
					"					,SUM(ST.\"KILOS\" * PA.\"PRECIO_COMPRA_KILO\") AS \"COSTE_MERMA\"" +
					"					,SUM(ST.\"KILOS\" * PA.\"GASTO_KILO\") AS \"GASTO_MERMA\"" +
					"					,SUM(ST.\"KILOS\" * PA.\"AYUDA_KILO\") AS \"AYUDA_MERMA\"" +
					"			FROM " + st + " AS ST " +
								JOIN_PARTIDA() + " AS PA ON PA.\"OID\" = ST.\"OID_BATCH\"" +
					"			WHERE ST.\"TIPO\" = " + (long)ETipoStock.Merma +
					"			GROUP BY ST.\"OID_EXPEDIENTE\")";

			return query;
		}
		internal static string JOIN_CONSUMO()
		{
			string st = nHManager.Instance.GetSQLTable(typeof(StockRecord));
			string pa = nHManager.Instance.GetSQLTable(typeof(BatchRecord));

			string query = string.Empty;

			query = " LEFT JOIN (SELECT ST.\"OID_EXPEDIENTE\"" +
					"					,SUM(ST.\"KILOS\") AS \"KG_CONSUMO\"" +
					"					,SUM(ST.\"BULTOS\") AS \"UDS_CONSUMO\"" +
					"					,SUM(ST.\"KILOS\" * PA.\"PRECIO_COMPRA_KILO\") AS \"COSTE_CONSUMO\"" +
					"					,SUM(ST.\"KILOS\" * PA.\"GASTO_KILO\") AS \"GASTO_CONSUMO\"" +
					"					,SUM(ST.\"KILOS\" * PA.\"AYUDA_KILO\") AS \"AYUDA_CONSUMO\"" +
					"			FROM " + st + " AS ST " +
								JOIN_PARTIDA() + " AS PA ON PA.\"OID\" = ST.\"OID_BATCH\"" +
					"			WHERE ST.\"TIPO\" = " + (long)ETipoStock.Consumo +
					"			GROUP BY ST.\"OID_EXPEDIENTE\")";

			return query;
		}

		internal static string JOIN_PRODUCTO_CLIENTE()
		{
			string pc = nHManager.Instance.GetSQLTable(typeof(ClientProductRecord));

			string query = string.Empty;

			query = " LEFT JOIN (SELECT PC.*" +
					"			FROM " + pc + " AS PC)";

			return query;
		}

		internal static string JOIN_BASE()
		{
			string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string p = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string pe = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string e = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			query = " FROM " + c + " AS C" +
					" INNER JOIN " + f + " AS F ON F.\"OID_CLIENTE\" = C.\"OID\"" +
					" INNER JOIN " + s + " AS S ON S.\"OID\" = F.\"OID_SERIE\"" +
					" INNER JOIN (SELECT CF.\"OID_FACTURA\"" +
					"                   ,CF.\"OID_BATCH\"" +
					"                   ,CF.\"OID_PRODUCTO\"" +
					"                   ,SUM(CF.\"CANTIDAD\") AS \"KILOS\"" +
					"                   ,SUM(CF.\"SUBTOTAL\" - (CF.\"SUBTOTAL\" * CF.\"P_DESCUENTO\" / 100)) AS \"SUBTOTAL\"" +
					"					,AVG(CF.\"PRECIO\") AS \"PRECIO_VENTA\"" +
					"               FROM " + cf + " AS CF" +
					"               WHERE \"OID_KIT\" = 0" +
					"               GROUP BY \"OID_FACTURA\", \"OID_BATCH\", \"OID_PRODUCTO\")" +
					"       AS CF ON F.\"OID\" = CF.\"OID_FACTURA\"" +
					JOIN_PARTIDA() + " AS PE ON PE.\"OID\" = CF.\"OID_BATCH\"" +
					" LEFT JOIN " + p + " AS P ON P.\"OID\" = CF.\"OID_PRODUCTO\"" +
					JOIN_PRODUCTO_CLIENTE() + " AS PC ON (PC.\"OID_PRODUCTO\" = P.\"OID\") AND (PC.\"OID_CLIENTE\" = C.\"OID\") AND (PC.\"FECHA_VALIDEZ\" >= F.\"FECHA\")" +
					" LEFT JOIN " + e + " AS E ON E.\"OID\" = PE.\"OID_EXPEDIENTE\"" +
					JOIN_MERMAS() + " AS ST1 ON ST1.\"OID_EXPEDIENTE\" = E.\"OID\"" +
					JOIN_CONSUMO() + " AS ST2 ON ST2.\"OID_EXPEDIENTE\" = E.\"OID\"";

			return query;
		}

		internal static string JOIN_BASE_DETALLADO()
		{
			string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string p = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string pe = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string e = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			query = " FROM " + c + " AS C" +
					" INNER JOIN " + f + " AS F ON C.\"OID\" = F.\"OID_CLIENTE\"" +
					" INNER JOIN " + s + " AS S ON S.\"OID\" = F.\"OID_SERIE\"" +
					" INNER JOIN (SELECT \"OID_FACTURA\"" +
					"                   ,\"OID_BATCH\"" +
					"                   ,\"OID_PRODUCTO\"" +
					"                   ,SUM(\"CANTIDAD\") AS \"KILOS\"" +
					"                   ,SUM(\"SUBTOTAL\" - (\"SUBTOTAL\" * \"P_DESCUENTO\" / 100)) AS \"SUBTOTAL\"" +
					"					,AVG(CF.\"PRECIO\") AS \"PRECIO_VENTA\"" +
					"               FROM " + cf + " AS CF" +
                    "               WHERE \"OID_BATCH\" NOT IN (SELECT \"OID_KIT\" FROM " + pe + " WHERE \"OID_KIT\" != 0)" +
					"               GROUP BY \"OID_FACTURA\", \"OID_BATCH\", \"OID_PRODUCTO\")" +
					"       AS CF ON F.\"OID\" = CF.\"OID_FACTURA\"" +
					JOIN_PARTIDA() + " AS PE ON PE.\"OID\" = CF.\"OID_BATCH\"" +
					" LEFT JOIN " + pe + " AS PE2 ON PE2.\"OID\" = PE.\"OID_KIT\"" +
					" LEFT JOIN " + p + " AS P ON P.\"OID\" = CF.\"OID_PRODUCTO\"" +
					JOIN_PRODUCTO_CLIENTE() + " AS PC ON (PC.\"OID_PRODUCTO\" = P.\"OID\") AND (PC.\"OID_CLIENTE\" = C.\"OID\") AND (PC.\"FECHA_VALIDEZ\" >= F.\"FECHA\")" +
					" LEFT JOIN " + e + " AS E ON E.\"OID\" = PE.\"OID_EXPEDIENTE\"" +
					JOIN_MERMAS() + " AS ST1 ON ST1.\"OID_EXPEDIENTE\" = E.\"OID\"" +
					JOIN_CONSUMO() + " AS ST2 ON ST2.\"OID_EXPEDIENTE\" = E.\"OID\"";

			return query;
		}

		internal static string SUBQUERY_FACTURA_MENSUAL()
		{
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));

			string query;

			query =
				"	(SELECT *" +
				"           ,to_char(\"FECHA\", 'MM') AS \"MES\"" +
				"           ,to_char(\"FECHA\", 'YYYY') AS \"ANIO\"" +
				"    FROM " + f + ")";

			return query;
		}
		
		internal static string SUBQUERY_FACTURA_GASTOS_DEMORA_PERIODO(QueryConditions conditions)
		{
			string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string cbf = nHManager.Instance.GetSQLTable(typeof(ChargeOperationRecord));
			string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));

            string tipo_interes = Library.Invoice.ModulePrincipal.GetTipoInteresPorGastosDemoraSetting().ToString(new CultureInfo("en-US"));

			string query = "SELECT F.\"OID_CLIENTE\"" +
			"                           ,F.\"OID\" AS \"OID_FACTURA\"" +
			"                           ,F.\"TOTAL\" * (current_date - cast(F.\"FECHA\" AS date)) * CASE WHEN (CL.\"TIPO_INTERES\" != 0) THEN CL.\"TIPO_INTERES\" ELSE " + tipo_interes + " END / 36000 AS \"GASTOS_DEMORA\"" +
			"                FROM " + f + " AS F" +
			"                INNER JOIN " + c + " AS CL ON (CL.\"OID\" = F.\"OID_CLIENTE\")" +
			"                INNER JOIN (   SELECT DISTINCT CF.\"OID_FACTURA\" AS \"OID_FACTURA\"" +
			"                                           , CF.\"CANTIDAD\" AS \"CANTIDAD\"" +
			"								FROM " + cbf + " AS CF" +
			"                               INNER JOIN " + cb + " AS C ON (CF.\"OID_COBRO\" = C.\"OID\" AND (C.\"ESTADO_COBRO\" != " + (long)EEstado.Charged + " OR C.\"ESTADO\" = " + ((long)EEstado.Anulado) + ") AND C.\"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "')" + ")" +
			"                                   AS CF ON CF.\"OID_FACTURA\" = F.\"OID\"" +
			"								WHERE TRUE";

			if (conditions.Producto != null)
				query += "						AND CF.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

            //if (conditions.Expediente != null)
            //    query += "						AND CF.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;

			if (conditions.TipoExpediente != ETipoExpediente.Todos)
				query += "						AND E.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;

			if (conditions.TipoFactura == ETipoFactura.NoAgrupadas)
				query += "						AND F.\"ALBARAN\" = FALSE";

			query +=
			"								GROUP BY F.\"OID_CLIENTE\", F.\"OID\", F.\"FECHA\", CL.\"TIPO_INTERES\", F.\"TOTAL\"" +
			"                       UNION" +
			"                       SELECT CL.\"OID\" AS \"OID_CLIENTE\"" +
			"                           , F.\"OID\" AS \"OID_FACTURA\"" +
			"                           , F.\"TOTAL\" * (current_date - cast(F.\"FECHA\" AS date)) * CASE WHEN (CL.\"TIPO_INTERES\" != 0) THEN CL.\"TIPO_INTERES\" ELSE " + tipo_interes + " END / 36000 AS \"GASTOS_DEMORA\"" +
			"                       FROM " + f + " AS F" +
			"                       INNER JOIN " + c + " AS CL ON (CL.\"OID\" = F.\"OID_CLIENTE\")" +
			"                       WHERE F.\"OID\" NOT IN (    SELECT CF.\"OID_FACTURA\"" +
			"                                                   FROM " + cbf + " AS CF)";

			if (conditions.Producto != null)
				query += "              AND CF.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

            //if (conditions.Expediente != null)
            //    query += "              AND CF.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;

			if (conditions.TipoExpediente != ETipoExpediente.Todos)
				query += "              AND E.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;

			if (conditions.TipoFactura == ETipoFactura.NoAgrupadas)
				query += "              AND F.\"ALBARAN\" = FALSE";

			query +=
			"                       GROUP BY CL.\"OID\", F.\"OID\", F.\"FECHA\", CL.\"TIPO_INTERES\", F.\"TOTAL\"";

			return query;
		}
		internal static string SUBQUERY_FACTURA_GASTOS_DEMORA_MES(QueryConditions conditions)
		{
			string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string cbf = nHManager.Instance.GetSQLTable(typeof(ChargeOperationRecord));
			string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));

            string tipo_interes = Library.Invoice.ModulePrincipal.GetTipoInteresPorGastosDemoraSetting().ToString(new CultureInfo("en-US"));

			string query = "SELECT CL.\"OID\" AS \"OID_CLIENTE\"" +
				"					,F.\"OID\" AS \"OID_FACTURA\"" +
				"                   ,F.\"TOTAL\" * (current_date - cast(F.\"FECHA\" AS date)) * CASE WHEN (CL.\"TIPO_INTERES\" != 0) THEN CL.\"TIPO_INTERES\" ELSE " + tipo_interes + " END / 36000 AS \"GASTOS_DEMORA\"" +
				"					,F.\"MES\" AS \"MES\"" +
				"					,F.\"ANIO\" AS \"ANIO\"" +
				"			FROM (" + SUBQUERY_FACTURA_MENSUAL() + ") AS F" +
				"           INNER JOIN " + c + " AS CL ON (CL.\"OID\" = F.\"OID_CLIENTE\")" +
				"           INNER JOIN (SELECT DISTINCT CF.\"OID_FACTURA\" AS \"OID_FACTURA\"" +
				"								,CF.\"CANTIDAD\" AS \"CANTIDAD\"" +
				"						FROM " + cbf + " AS CF" +
				"                       INNER JOIN " + cb + " AS C ON (CF.\"OID_COBRO\" = C.\"OID\" AND (C.\"ESTADO_COBRO\" != " + (long)EEstado.Charged + " OR C.\"ESTADO\" = " + ((long)EEstado.Anulado) + ") AND C.\"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "')" + ")" +
				"				AS CF ON CF.\"OID_FACTURA\" = F.\"OID\"" +
				"			WHERE TRUE";

			if (conditions.Producto != null)
				query += "  AND CF.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

            //if (conditions.Expediente != null)
            //    query += "  AND CF.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;

			if (conditions.TipoExpediente != ETipoExpediente.Todos)
				query += "  AND E.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;

			if (conditions.TipoFactura == ETipoFactura.NoAgrupadas)
				query += "  AND F.\"ALBARAN\" = FALSE";

			query +=
				"			GROUP BY CL.\"OID\", F.\"OID\", F.\"FECHA\", F.\"MES\", F.\"ANIO\", CL.\"TIPO_INTERES\", F.\"TOTAL\"" +
				"           UNION" +
				"           SELECT CL.\"OID\" AS \"OID_CLIENTE\"" +
				"					,F.\"OID\" AS \"OID_FACTURA\"" +
                "                   ,F.\"TOTAL\" * (current_date - cast(F.\"FECHA\" AS date)) * CASE WHEN (CL.\"TIPO_INTERES\" != 0) THEN CL.\"TIPO_INTERES\" ELSE " + tipo_interes + " END / 36000 AS \"GASTOS_DEMORA\"" +
				"					,F.\"MES\" AS \"MES\"" +
				"					,F.\"ANIO\" AS \"ANIO\"" +
				"			FROM (" + SUBQUERY_FACTURA_MENSUAL() + ") AS F" +
				"           INNER JOIN " + c + " AS CL ON (CL.\"OID\" = F.\"OID_CLIENTE\")" +
				"           WHERE F.\"OID\" NOT IN (SELECT CF.\"OID_FACTURA\"" +
				"									FROM " + cbf + " AS CF)";

			if (conditions.Producto != null)
				query += "	AND CF.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

            //if (conditions.Expediente != null)
            //    query += "	AND CF.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;

			if (conditions.TipoExpediente != ETipoExpediente.Todos)
				query += "  AND E.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;

			if (conditions.TipoFactura == ETipoFactura.NoAgrupadas)
				query += "  AND F.\"ALBARAN\" = FALSE";

			query +=
				"			GROUP BY CL.\"OID\", F.\"OID\", F.\"FECHA\", F.\"MES\", F.\"ANIO\", CL.\"TIPO_INTERES\", F.\"TOTAL\"";

			return query;
		}

		internal static string SUBQUERY_CLIENTE_GASTOS_BANCARIOS_PERIODO(QueryConditions conditions)
		{
			string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));

			string query = "SELECT SUM(CB.\"GASTOS_BANCARIOS\") AS \"GASTOS_COBRO\"" +
			"                   , CB.\"OID_CLIENTE\"" +
			"               FROM " + cb + " AS CB" +
			"               WHERE CB.\"ESTADO\" != " + (long)EEstado.Anulado +
			"                   AND CB.\"GASTOS_BANCARIOS\" != 0" +
			"                   AND CB.\"VENCIMIENTO\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
			"               GROUP BY CB.\"OID_CLIENTE\"";

			return query;
		}
		internal static string SUBQUERY_CLIENTE_GASTOS_BANCARIOS_MES(QueryConditions conditions)
		{
			string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));

			string query = "SELECT SUM(CB.\"GASTOS_BANCARIOS\") AS \"GASTOS_COBRO\"" +
			"						,CB.\"OID_CLIENTE\"" +
			"						,to_char(CB.\"VENCIMIENTO\", 'MM') AS \"MES\"" +
			"						,to_char(CB.\"VENCIMIENTO\", 'YYYY') AS \"ANIO\"" +
			"               FROM " + cb + " AS CB" +
			"               WHERE CB.\"ESTADO\" != " + (long)EEstado.Anulado +
			"                   AND CB.\"GASTOS_BANCARIOS\" != 0" +
			"                   AND CB.\"VENCIMIENTO\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
			"               GROUP BY CB.\"OID_CLIENTE\", \"MES\", \"ANIO\"";

			return query;
		}
		
		internal static string JOIN_BASE_MENSUAL()
		{
			string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string p = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string pe = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string e = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			query = " FROM " + c + " AS C" +
					" INNER JOIN (SELECT *" +
			        "                   ,to_char(\"FECHA\", 'MM') AS \"MES\"" +
					"                   ,to_char(\"FECHA\", 'YYYY') AS \"ANIO\"" +
					"               FROM " + f + ")" +
					"       AS F ON F.\"OID_CLIENTE\" = C.\"OID\"" +
					" INNER JOIN " + s + " AS S ON S.\"OID\" = F.\"OID_SERIE\"" +
					" INNER JOIN (SELECT \"OID_FACTURA\"" +
					"					,\"OID_BATCH\"" +
					"					,\"OID_PRODUCTO\", SUM(\"CANTIDAD\") AS \"KILOS\"" +
					"                   ,SUM(\"SUBTOTAL\" - (\"SUBTOTAL\" * \"P_DESCUENTO\" / 100)) AS \"SUBTOTAL\"" +
					"					,AVG(CF.\"PRECIO\") AS \"PRECIO_VENTA\"" +
					"               FROM " + cf + " AS CF" +
                    "               WHERE \"OID_KIT\" = 0" +
					"               GROUP BY \"OID_FACTURA\", \"OID_BATCH\", \"OID_PRODUCTO\")" +
					"       AS CF ON F.\"OID\" = CF.\"OID_FACTURA\"" +
					JOIN_PARTIDA() + " AS PE ON PE.\"OID\" = CF.\"OID_BATCH\"" +
					" LEFT JOIN " + p + " AS P ON P.\"OID\" = CF.\"OID_PRODUCTO\"" +
					JOIN_PRODUCTO_CLIENTE() + " AS PC ON (PC.\"OID_PRODUCTO\" = P.\"OID\") AND (PC.\"OID_CLIENTE\" = C.\"OID\") AND (PC.\"FECHA_VALIDEZ\" >= F.\"FECHA\")" +
					" LEFT JOIN " + e + " AS E ON E.\"OID\" = PE.\"OID_EXPEDIENTE\"" +
					JOIN_MERMAS() + " AS ST1 ON ST1.\"OID_EXPEDIENTE\" = E.\"OID\"" +
					JOIN_CONSUMO() + " AS ST2 ON ST2.\"OID_EXPEDIENTE\" = E.\"OID\"";

			return query;
		}

		internal static string JOIN_BASE_BY_CLIENTE_PORCENTUAL_MENSUAL(QueryConditions conditions)
		{
			string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string p = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string pa = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string e = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query = string.Empty;
			string p_compra;

            p_compra = "COALESCE(PA.\"PRECIO_COMPRA_KILO\", COALESCE(PC.\"PRECIO_COMPRA\", COALESCE(PR.\"PRECIO_COMPRA\",0)))";

			query = 
				" FROM " + c + " AS C" +
				" INNER JOIN (" + SUBQUERY_FACTURA_MENSUAL() + ") AS F ON F.\"OID_CLIENTE\" = C.\"OID\"" +
				" INNER JOIN " + s + " AS S ON S.\"OID\" = F.\"OID_SERIE\"";
			query +=
				//Cálculo de TOTALES POR FACTURA
				" INNER JOIN (SELECT CF.\"OID_FACTURA\"" +
				"					,CF.\"OID_PRODUCTO\"" +
				"					,CF.\"OID_BATCH\"" +
				"					,SUM(CF.\"CANTIDAD\") AS \"KILOS\"" +
				"					,SUM(CF.\"SUBTOTAL\" - (CF.\"SUBTOTAL\" * CF.\"P_DESCUENTO\" / 100)) AS \"SUBTOTAL\"" +
				"					,AVG(CF.\"PRECIO\") AS \"PRECIO_VENTA\"" +
				"               FROM " + cf + " AS CF" +
				"				INNER JOIN " + f + " AS F ON F.\"OID\" = CF.\"OID_FACTURA\"" +
				"				INNER JOIN " + p + " AS PR ON PR.\"OID\" = CF.\"OID_PRODUCTO\"" +
				"				WHERE (F.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')" +
				"               AND CF.\"OID_KIT\" = 0";

			if (conditions.Producto != null)
				query += "			AND CF.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

			if (conditions.Expediente != null)
				query += "			AND CF.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;

			if (conditions.TipoExpediente != ETipoExpediente.Todos)
				query += "			AND E.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;

			if (conditions.TipoFactura == ETipoFactura.NoAgrupadas)
				query += "			AND F.\"ALBARAN\" = FALSE";

			query +=
				"				GROUP BY \"OID_FACTURA\", \"OID_BATCH\", \"OID_PRODUCTO\")" +
				"       AS CF ON CF.\"OID_FACTURA\" = F.\"OID\"";

			query +=
				//Costes del producto
				JOIN_PARTIDA() + " AS PE ON PE.\"OID\" = CF.\"OID_BATCH\"" +
				" LEFT JOIN " + p + " AS P ON P.\"OID\" = CF.\"OID_PRODUCTO\"" +
				JOIN_PRODUCTO_CLIENTE() + " AS PC ON (PC.\"OID_PRODUCTO\" = P.\"OID\") AND (PC.\"OID_CLIENTE\" = C.\"OID\") AND (PC.\"FECHA_VALIDEZ\" >= F.\"FECHA\")";

			query +=
				//Calculo de VALORES ABSOLUTOS POR MESES
				" INNER JOIN (SELECT SUM(CF.\"SUBTOTAL\" - (CF.\"SUBTOTAL\" * CF.\"P_DESCUENTO\" / 100)) AS \"VENTA_ABSOLUTA\"" +
				"					,AVG(COALESCE(PA.\"COSTE_KILO\", 0)) AS \"COSTE_KILO_MEDIO\"" +
				"					,SUM(CF.\"SUBTOTAL\" - (CF.\"SUBTOTAL\" * CF.\"P_DESCUENTO\" / 100)) - SUM((" + p_compra + " + COALESCE(PA.\"GASTO_KILO\",0) - COALESCE(PA.\"AYUDA_KILO\",0)) * CF.\"CANTIDAD\") AS \"BENEFICIO_ABSOLUTO\"" +
				"					,SUM(CF.\"CANTIDAD\") AS \"KILOS_ABSOLUTO\"" +
				"                   ,F.\"MES\" AS \"MES\"" +
				"                   ,F.\"ANIO\"AS \"ANIO\"" +
				"               FROM (" + SUBQUERY_FACTURA_MENSUAL() + ") AS F" +
				"				INNER JOIN " + cf + " AS CF ON F.\"OID\" = CF.\"OID_FACTURA\"" +
				"				INNER JOIN " + p + " AS PR ON PR.\"OID\" = CF.\"OID_PRODUCTO\"" +
								JOIN_PARTIDA() + " AS PA ON PA.\"OID\" = CF.\"OID_BATCH\"" +
								JOIN_PRODUCTO_CLIENTE() + " AS PC ON (PC.\"OID_PRODUCTO\" = PR.\"OID\") AND (PC.\"OID_CLIENTE\" = F.\"OID_CLIENTE\") AND (PC.\"FECHA_VALIDEZ\" >= F.\"FECHA\")" +
				"				WHERE (F.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')" +
				"               AND CF.\"OID_KIT\" = 0";
			if (conditions.Producto != null)
				query += "		AND CF.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

			if (conditions.Expediente != null)
				query += "		AND CF.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;

			if (conditions.TipoExpediente != ETipoExpediente.Todos)
				query += "		AND E.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;

			if (conditions.TipoFactura == ETipoFactura.NoAgrupadas)
				query += "		AND F.\"ALBARAN\" = FALSE";

			query += 
				"				GROUP BY F.\"ANIO\", F.\"MES\")" +
				"       AS VAB ON VAB.\"MES\" = F.\"MES\" AND VAB.\"ANIO\" = F.\"ANIO\"";

			query +=
				//Calculo del BENEFICIO ABSOLUTO NETO POR MES
				"LEFT JOIN (    SELECT CB2.\"BENEFICIO_ABSOLUTO_NETO\" - SUM(COALESCE(GC.\"GASTOS_COBRO\", 0)) AS \"BENEFICIO_ABSOLUTO_NETO\"" +
				"						,CB2.\"MES\" AS \"MES\"" +
				"						,CB2.\"ANIO\" AS \"ANIO\"" +
				"               FROM (  SELECT SUM(CF.\"SUBTOTAL\" - (CF.\"SUBTOTAL\" * CF.\"P_DESCUENTO\" / 100)) - SUM(COALESCE(GD.\"GASTOS_DEMORA\", 0)) - SUM((" + p_compra + " + COALESCE(PA.\"GASTO_KILO\",0) - COALESCE(PA.\"AYUDA_KILO\",0)) * CF.\"CANTIDAD\") AS \"BENEFICIO_ABSOLUTO_NETO\"" +
				"							,F.\"MES\" AS \"MES\"" +
				"							,F.\"ANIO\" AS \"ANIO\"" +
				"                       FROM (" + SUBQUERY_FACTURA_MENSUAL() + ") AS F" +
				"                       INNER JOIN " + c + " AS C ON C.\"OID\" = F.\"OID_CLIENTE\"" +
				"                       INNER JOIN " + cf + " AS CF ON F.\"OID\" = CF.\"OID_FACTURA\"" +
				"                       INNER JOIN " + p + " AS PR ON PR.\"OID\" = CF.\"OID_PRODUCTO\"" +
										JOIN_PARTIDA() + " AS PA ON PA.\"OID\" = CF.\"OID_BATCH\"" +
				"                       LEFT JOIN ( " + SUBQUERY_FACTURA_GASTOS_DEMORA_MES(conditions) + " )" + 
				"							AS GD ON GD.\"OID_FACTURA\" = F.\"OID\" AND GD.\"MES\" = F.\"MES\" AND GD.\"ANIO\" = F.\"ANIO\"" +
										JOIN_PRODUCTO_CLIENTE() + " AS PC ON (PC.\"OID_PRODUCTO\" = PR.\"OID\") AND (PC.\"OID_CLIENTE\" = C.\"OID\") AND (PC.\"FECHA_VALIDEZ\" >= F.\"FECHA\")" +
				"				        WHERE (F.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')" +
				"                           AND CF.\"OID_KIT\" = 0";

			if (conditions.Producto != null)
				query += "			AND CF.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

			if (conditions.Expediente != null)
				query += "			AND CF.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;

			if (conditions.TipoExpediente != ETipoExpediente.Todos)
				query += "			AND E.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;

			if (conditions.TipoFactura == ETipoFactura.NoAgrupadas)
				query += "			AND F.\"ALBARAN\" = FALSE";

			query +=
				"					GROUP BY F.\"ANIO\", F.\"MES\")	AS CB2" +
				"				LEFT JOIN ( " + SUBQUERY_CLIENTE_GASTOS_BANCARIOS_MES(conditions) + " )" +
				"					AS GC ON GC.\"MES\" = CB2.\"MES\" AND GC.\"ANIO\" = CB2.\"ANIO\"" +
				"               GROUP BY CB2.\"BENEFICIO_ABSOLUTO_NETO\", CB2.\"ANIO\", CB2.\"MES\")" +
				"       AS VAN ON VAN.\"MES\" = F.\"MES\" AND VAN.\"ANIO\" = F.\"ANIO\"";

			query +=
				//Calculo de GASTOS DE DEMORA DEL CLIENTE POR MES
				" LEFT JOIN (   SELECT C.\"OID\" AS \"OID_CLIENTE\" " +
				"                   ,COALESCE(GD.\"GASTOS_DEMORA\",0) AS \"GASTOS_DEMORA\"" +
				"					,COALESCE(GD.\"MES\", '') AS \"MES\"" +
				"					,COALESCE(GD.\"ANIO\",'') AS \"ANIO\"" +
				"               FROM " + c + " AS C" +
				"               LEFT JOIN ( SELECT FD.\"OID_CLIENTE\" AS \"OID_CLIENTE\"" +
				"                               ,SUM(FD.\"GASTOS_DEMORA\") AS \"GASTOS_DEMORA\"" +
				"								,FD.\"MES\" AS \"MES\"" +
				"								,FD.\"ANIO\" AS \"ANIO\"" +
				"                           FROM (" + SUBQUERY_FACTURA_GASTOS_DEMORA_MES(conditions) + ") AS FD" +
				"                           GROUP BY FD.\"OID_CLIENTE\", FD.\"ANIO\", FD.\"MES\")" +
				"					AS GD ON GD.\"OID_CLIENTE\" = C.\"OID\")" +
				"		AS GD ON GD.\"OID_CLIENTE\" = C.\"OID\" AND GD.\"MES\" = F.\"MES\" AND GD.\"ANIO\" = F.\"ANIO\"";

			query +=
				//Calculo de GASTOS DEL COBRO DEL CLIENTE POR MES
				" LEFT JOIN (   SELECT C.\"OID\" AS \"OID_CLIENTE\" " +
				"                   ,COALESCE(GC.\"GASTOS_COBRO\",0) AS \"GASTOS_COBRO\"" +
				"					,GC.\"MES\" AS \"MES\"" +
				"					,GC.\"ANIO\" AS \"ANIO\"" +
				"               FROM " + c + " AS C" +
				"               LEFT JOIN ( " + SUBQUERY_CLIENTE_GASTOS_BANCARIOS_MES(conditions) + " )" +
				"                   AS GC ON GC.\"OID_CLIENTE\" = C.\"OID\"" +
				"				GROUP BY C.\"OID\", GC.\"GASTOS_COBRO\", GC.\"MES\", GC.\"ANIO\"" +
				"			)" +
				"		AS GC ON GC.\"OID_CLIENTE\" = C.\"OID\" AND GC.\"MES\" = F.\"MES\" AND GC.\"ANIO\" = F.\"ANIO\"";

            return query;
		}
        internal static string JOIN_BASE_BY_CLIENTE_PORCENTUAL_PERIODO(QueryConditions conditions)
        {
            string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
            string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
            string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
            string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
            string p = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
            string e = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

            string query;
            string p_compra;

            p_compra = "COALESCE(PA.\"PRECIO_COMPRA_KILO\", COALESCE(PC.\"PRECIO_COMPRA\", COALESCE(PR.\"PRECIO_COMPRA\",0)))";

			query = " FROM " + c + " AS C" +
					" INNER JOIN " + f + " AS F ON F.\"OID_CLIENTE\" = C.\"OID\"" +
					" INNER JOIN " + s + " AS S ON S.\"OID\" = F.\"OID_SERIE\"";
            query +=      
					//Cálculo de TOTALES POR FACTURA POR PERIODO
                    " INNER JOIN (SELECT CF.\"OID_FACTURA\"" +
                    "					,CF.\"OID_PRODUCTO\"" +
                    "					,CF.\"OID_BATCH\"" +
                    "					,SUM(CF.\"CANTIDAD\") AS \"KILOS\"" +
                    "					,SUM(CF.\"SUBTOTAL\" - (CF.\"SUBTOTAL\" * CF.\"P_DESCUENTO\" / 100)) AS \"SUBTOTAL\"" +
                    "					,AVG(CF.\"PRECIO\") AS \"PRECIO_VENTA\"" +
                    "               FROM " + cf + " AS CF" +
                    "				INNER JOIN " + f + " AS F ON F.\"OID\" = CF.\"OID_FACTURA\"" +
                    "				INNER JOIN " + p + " AS PR ON PR.\"OID\" = CF.\"OID_PRODUCTO\"" +
                    "				WHERE (F.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')" +
                    "					AND CF.\"OID_KIT\" = 0";
            if (conditions.Producto != null)
                query += "				AND CF.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

            if (conditions.Expediente != null)
                query += "				AND CF.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;

            if (conditions.TipoExpediente != ETipoExpediente.Todos)
                query += "				AND E.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;

            if (conditions.TipoFactura == ETipoFactura.NoAgrupadas)
                query += "				AND F.\"ALBARAN\" = FALSE";

			query +=
				"				GROUP BY \"OID_FACTURA\", \"OID_BATCH\", \"OID_PRODUCTO\")" +
				"       AS CF ON F.\"OID\" = CF.\"OID_FACTURA\"";
			query +=
				//Costes del producto
				JOIN_PARTIDA() + " AS PE ON PE.\"OID\" = CF.\"OID_BATCH\"" +
                " LEFT JOIN " + p + " AS P ON P.\"OID\" = CF.\"OID_PRODUCTO\"" +
                JOIN_PRODUCTO_CLIENTE() + " AS PC ON (PC.\"OID_PRODUCTO\" = P.\"OID\") AND (PC.\"OID_CLIENTE\" = C.\"OID\") AND (PC.\"FECHA_VALIDEZ\" >= F.\"FECHA\")";

			query +=
				//Calculo de VALORES ABSOLUTOS POR PERIODO
				" INNER JOIN (SELECT SUM(CF.\"SUBTOTAL\" - (CF.\"SUBTOTAL\" * CF.\"P_DESCUENTO\" / 100)) AS \"VENTA_ABSOLUTA\"" +
				"					,AVG(COALESCE(PA.\"COSTE_KILO\", 0)) AS \"COSTE_KILO_MEDIO\"" +
				"					,SUM(CF.\"SUBTOTAL\" - (CF.\"SUBTOTAL\" * CF.\"P_DESCUENTO\" / 100)) - SUM((" + p_compra + " + COALESCE(PA.\"GASTO_KILO\",0) - COALESCE(PA.\"AYUDA_KILO\",0)) * CF.\"CANTIDAD\") AS \"BENEFICIO_ABSOLUTO\"" +
				"					,SUM(CF.\"CANTIDAD\") AS \"KILOS_ABSOLUTO\"" +
				"               FROM " + f + " AS F" +
				"				INNER JOIN " + cf + " AS CF ON F.\"OID\" = CF.\"OID_FACTURA\"" +
				"				INNER JOIN " + p + " AS PR ON PR.\"OID\" = CF.\"OID_PRODUCTO\"" +
								JOIN_PARTIDA() + " AS PA ON PA.\"OID\" = CF.\"OID_BATCH\"" +
								JOIN_PRODUCTO_CLIENTE() + " AS PC ON (PC.\"OID_PRODUCTO\" = PR.\"OID\") AND (PC.\"OID_CLIENTE\" = F.\"OID_CLIENTE\") AND (PC.\"FECHA_VALIDEZ\" >= F.\"FECHA\")" +
				"				WHERE (F.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')" +
				"					AND CF.\"OID_KIT\" = 0";

			if (conditions.Producto != null)
				query += "			AND CF.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

			if (conditions.Expediente != null)
				query += "			AND CF.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;

			if (conditions.TipoExpediente != ETipoExpediente.Todos)
				query += "			AND E.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;

			if (conditions.TipoFactura == ETipoFactura.NoAgrupadas)
				query += "			AND F.\"ALBARAN\" = FALSE";

			query += 
				"			)       AS VAB ON TRUE";

            query +=
				//Calculo del BENFICIO ABSOLUTO NETO POR PERIODO
				" LEFT JOIN (	SELECT CB2.\"BENEFICIO_ABSOLUTO_NETO\" - SUM(COALESCE(GB.\"GASTOS_COBRO\", 0)) AS \"BENEFICIO_ABSOLUTO_NETO\"" +
				"				FROM (  SELECT SUM(CF.\"SUBTOTAL\" - (CF.\"SUBTOTAL\" * CF.\"P_DESCUENTO\" / 100)) - SUM(COALESCE(GD.\"GASTOS_DEMORA\", 0)) - SUM((" + p_compra + " + COALESCE(PA.\"GASTO_KILO\",0) - COALESCE(PA.\"AYUDA_KILO\",0)) * CF.\"CANTIDAD\") AS \"BENEFICIO_ABSOLUTO_NETO\"" +
				"                       FROM " + f + " AS F" +
				"                       INNER JOIN " + c + " AS C ON C.\"OID\" = F.\"OID_CLIENTE\"" +
				"                       INNER JOIN " + cf + " AS CF ON F.\"OID\" = CF.\"OID_FACTURA\"" +
				"                       INNER JOIN " + p + " AS PR ON PR.\"OID\" = CF.\"OID_PRODUCTO\"" +
										JOIN_PARTIDA() + " AS PA ON PA.\"OID\" = CF.\"OID_BATCH\"" +
				"                       LEFT JOIN ( " + SUBQUERY_FACTURA_GASTOS_DEMORA_PERIODO(conditions) + " ) AS GD ON GD.\"OID_FACTURA\" = F.\"OID\"" +
										JOIN_PRODUCTO_CLIENTE() + " AS PC ON (PC.\"OID_PRODUCTO\" = PR.\"OID\") AND (PC.\"OID_CLIENTE\" = C.\"OID\") AND (PC.\"FECHA_VALIDEZ\" >= F.\"FECHA\")" +
				"				        WHERE (F.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')" +
				"                           AND CF.\"OID_KIT\" = 0";                 

            if (conditions.Producto != null)
                query += "			AND CF.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;

            if (conditions.Expediente != null)
                query += "			AND CF.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;

            if (conditions.TipoExpediente != ETipoExpediente.Todos)
                query += "			AND E.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;

            if (conditions.TipoFactura == ETipoFactura.NoAgrupadas)
                query += "			AND F.\"ALBARAN\" = FALSE";

            query += 
				"                   ) AS CB2" +
				"               LEFT JOIN ( " + SUBQUERY_CLIENTE_GASTOS_BANCARIOS_PERIODO(conditions) + " ) AS GB ON TRUE" +
				"               GROUP BY CB2.\"BENEFICIO_ABSOLUTO_NETO\"" +
				"           )" +
				"	AS VAN ON TRUE";

			query +=
				//Calculo de GASTOS DE DEMORA DEL CLIENTE
				" LEFT JOIN (   SELECT C.\"OID\" AS \"OID_CLIENTE\"" +
				"                   ,COALESCE(GD.\"GASTOS_DEMORA\", 0) AS \"GASTOS_DEMORA\"" +
				"               FROM " + c + " AS C" +
				"               LEFT JOIN ( SELECT FD.\"OID_CLIENTE\"" +
				"                               ,SUM(FD.\"GASTOS_DEMORA\") AS \"GASTOS_DEMORA\"" +
				"                           FROM ( " + SUBQUERY_FACTURA_GASTOS_DEMORA_PERIODO(conditions) + " ) AS FD" +
				"                           GROUP BY FD.\"OID_CLIENTE\")" +
				"                   AS GD ON GD.\"OID_CLIENTE\" = C.\"OID\")" +
				"		AS GD ON GD.\"OID_CLIENTE\" = C.\"OID\"";

			query +=
				//Calculo de GASTOS DEL COBRO DEL CLIENTE
				" LEFT JOIN (   SELECT C.\"OID\" AS \"OID_CLIENTE\"" +
				"                   ,COALESCE(GC.\"GASTOS_COBRO\", 0) AS \"GASTOS_COBRO\"" +
				"               FROM " + c + " AS C" +
				"               LEFT JOIN ( " + SUBQUERY_CLIENTE_GASTOS_BANCARIOS_PERIODO(conditions) + " )" +
				"                   AS GC ON GC.\"OID_CLIENTE\" = C.\"OID\"" +
				"				GROUP BY C.\"OID\", GC.\"GASTOS_COBRO\")" +
				"		AS GC ON GC.\"OID_CLIENTE\" = C.\"OID\"";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query = " WHERE F.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

			//query += " AND P.\"BENEFICIO_CERO\" = FALSE";	

			if (conditions.Cliente != null)
				query += " AND C.\"OID\" = " + conditions.Cliente.Oid;

			if (conditions.Producto != null)
				query += " AND P.\"OID\" = " + conditions.Producto.Oid;

			if (conditions.Serie != null)
				query += " AND S.\"OID\" = " + conditions.Serie.Oid;

            //if (conditions.Expediente != null)
            //    query += " AND E.\"OID\" = " + conditions.Expediente.Oid;

			if (conditions.Familia != null)
				query += " AND FM.\"OID\" = " + conditions.Familia.Oid;

			switch (conditions.TipoProducto)
			{
				case ETipoProducto.Expediente:
					query += " AND CF.\"OID_BATCH\" != 0";
					break;

				case ETipoProducto.Libres:
					query += " AND CF.\"OID_BATCH\" = 0";
					break;

				/*case ETipoProducto.Almacen:
					query += " AND CF.\"OID_ALMACEN\" != 0";
					break;*/
			}

			if (conditions.TipoExpediente != ETipoExpediente.Todos)
			{
				query += " AND E.\"TIPO_EXPEDIENTE\" = " + (long)conditions.TipoExpediente;
			}

            if (conditions.Expediente != null)
                query += " AND E.\"OID\" = " + (long)conditions.Expediente.Oid;

			if (conditions.TipoFactura == ETipoFactura.NoAgrupadas)
			{
				query += " AND F.\"ALBARAN\" = FALSE";
			}

			return query;
		}

		public static string SELECT(QueryConditions conditions)
		{
			string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string p = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string pe = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string e = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			//LOS PRODUCTOS QUE HAN SIDO VENDIDOS EN ALGUNA FACTURA
			query = SELECT_FIELDS(ETipoQuery.VENTA) +
					JOIN_BASE() +
					WHERE(conditions);
            
			query += " GROUP BY C.\"OID\", C.\"CODIGO\", C.\"NOMBRE\", P.\"OID\", P.\"NOMBRE\", CF.\"OID_BATCH\", E.\"CODIGO\", F.\"OID\", F.\"CODIGO\", S.\"IDENTIFICADOR\"";

			return query;
		}

		public static string SELECT_DETALLADO_BY_CLIENTE(QueryConditions conditions)
		{
			string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string p = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string pe = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string e = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			query = SELECT_FIELDS(ETipoQuery.VENTA_DETALLADA_CLIENTE) +
					JOIN_BASE_DETALLADO() +
					WHERE(conditions);

			query += " GROUP BY C.\"OID\", C.\"CODIGO\", C.\"NOMBRE\", P.\"OID\", P.\"NOMBRE\", PE2.\"TIPO_MERCANCIA\", CF.\"OID_BATCH\", E.\"CODIGO\", F.\"OID\", F.\"CODIGO\", S.\"IDENTIFICADOR\", P.\"BENEFICIO_CERO\"";

			return query;
		}
		public static string SELECT_DETALLADO_BY_PRODUCTO(QueryConditions conditions)
		{
			string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string p = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string pe = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string e = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			query = SELECT_FIELDS(ETipoQuery.VENTA_DETALLADA_PRODUCTO) +
					JOIN_BASE_DETALLADO() +
					WHERE(conditions);

			query += " GROUP BY C.\"OID\", C.\"CODIGO\", C.\"NOMBRE\", P.\"OID\", P.\"NOMBRE\", PE2.\"TIPO_MERCANCIA\", CF.\"OID_BATCH\", E.\"CODIGO\", F.\"OID\", F.\"CODIGO\", S.\"IDENTIFICADOR\", P.\"BENEFICIO_CERO\"";

			return query;
		}

		public static string SELECT_BY_CLIENTE(QueryConditions conditions, bool detallado)
		{
			string query = !detallado ? SELECT(conditions) : SELECT_DETALLADO_BY_CLIENTE(conditions);
			query += " ORDER BY C.\"NOMBRE\", P.\"NOMBRE\"";

			return query;
		}
		public static string SELECT_BY_PRODUCTO(QueryConditions conditions, bool detallado)
		{
			string query = !detallado ? SELECT(conditions) : SELECT_DETALLADO_BY_PRODUCTO(conditions);
			query += " ORDER BY P.\"NOMBRE\", C.\"NOMBRE\"";

			return query;
		}
		public static string SELECT_BY_EXPEDIENTE(QueryConditions conditions, bool detallado)
		{
			string query = !detallado ? SELECT(conditions) : SELECT_DETALLADO_BY_CLIENTE(conditions);
			query += " ORDER BY E.\"CODIGO\", P.\"NOMBRE\", C.\"NOMBRE\"";

			return query;
		}

		public static string SELECT_MENSUAL(QueryConditions conditions)
		{
			string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string p = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string pe = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string e = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			query = SELECT_FIELDS(ETipoQuery.VENTA_MENSUAL) +
					JOIN_BASE_MENSUAL() +
					WHERE(conditions);

			query += " GROUP BY F.\"ANIO\", F.\"MES\"";
			query += " ORDER BY F.\"ANIO\", F.\"MES\"";

			return query;
		}
		public static string SELECT_BY_CLIENTE_MENSUAL(QueryConditions conditions)
		{
			string c = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string f = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string p = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string pe = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string e = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			query = SELECT_FIELDS(ETipoQuery.VENTA_CLIENTE_MENSUAL) +
					JOIN_BASE_MENSUAL() +
					WHERE(conditions);

			query += " GROUP BY C.\"OID\", C.\"CODIGO\", C.\"NOMBRE\", F.\"ANIO\", F.\"MES\"";
			query += " ORDER BY C.\"NOMBRE\", F.\"ANIO\", F.\"MES\"";

			return query;
		}
		public static string SELECT_BY_PRODUCTO_MENSUAL(QueryConditions conditions)
		{
			string query;

			query = SELECT_FIELDS(ETipoQuery.VENTA_PRODUCTO_MENSUAL) +
					JOIN_BASE_MENSUAL() +
					WHERE(conditions);

			query += " GROUP BY P.\"OID\", P.\"NOMBRE\", F.\"ANIO\", F.\"MES\"";
			query += " ORDER BY P.\"NOMBRE\", F.\"ANIO\", F.\"MES\"";

			return query;
		}

		public static string SELECT_BY_CLIENTE_PORCENTUAL_MENSUAL(QueryConditions conditions)
		{
			string query;

			query = SELECT_FIELDS(ETipoQuery.VENTA_CLIENTE_PORCENTUAL_MENSUAL) +
					JOIN_BASE_BY_CLIENTE_PORCENTUAL_MENSUAL(conditions) +
					WHERE(conditions);

			query += " AND VAB.\"VENTA_ABSOLUTA\" != 0";

			query += " GROUP BY C.\"OID\", C.\"CODIGO\", C.\"NOMBRE\", F.\"ANIO\", F.\"MES\", VAB.\"VENTA_ABSOLUTA\", VAB.\"BENEFICIO_ABSOLUTO\", GD.\"GASTOS_DEMORA\", GC.\"GASTOS_COBRO\", VAN.\"BENEFICIO_ABSOLUTO_NETO\"";

			return query;
		}
        public static string SELECT_BY_CLIENTE_PORCENTUAL_PERIODO(QueryConditions conditions)
        {
            string query;

            query = SELECT_FIELDS(ETipoQuery.VENTA_CLIENTE_PORCENTUAL_PERIODO) +
                    JOIN_BASE_BY_CLIENTE_PORCENTUAL_PERIODO(conditions) +
                    WHERE(conditions);

			query += " AND VAB.\"VENTA_ABSOLUTA\" != 0";

			query += " GROUP BY C.\"OID\", C.\"CODIGO\", C.\"NOMBRE\", VAB.\"VENTA_ABSOLUTA\", VAB.\"BENEFICIO_ABSOLUTO\", VAN.\"BENEFICIO_ABSOLUTO_NETO\", GD.\"GASTOS_DEMORA\", GC.\"GASTOS_COBRO\"";

            return query;
        }
		public static string SELECT_BY_CLIENTE_PORCENTUAL_BENEFICIO_MENSUAL(QueryConditions conditions)
		{
			string query;

			query = SELECT_BY_CLIENTE_PORCENTUAL_MENSUAL(conditions);

			query += " ORDER BY F.\"ANIO\", F.\"MES\", \"P_BENEFICIO_ABSOLUTO_NETO\" DESC";

			return query;
		}
        public static string SELECT_BY_CLIENTE_PORCENTUAL_BENEFICIO_PERIODO(QueryConditions conditions)
        {
            string query;

            query = SELECT_BY_CLIENTE_PORCENTUAL_PERIODO(conditions);

            query += " ORDER BY \"P_BENEFICIO_ABSOLUTO_NETO\" DESC";

            return query;
        }
		public static string SELECT_BY_CLIENTE_PORCENTUAL_VENTA_MENSUAL(QueryConditions conditions)
		{
			string query;

			query = SELECT_BY_CLIENTE_PORCENTUAL_MENSUAL(conditions);

			query += " ORDER BY F.\"ANIO\", F.\"MES\", \"P_VENTA_ABSOLUTA\" DESC";

			return query;
		}
        public static string SELECT_BY_CLIENTE_PORCENTUAL_VENTA_PERIODO(QueryConditions conditions)
        {
            string query;

            query = SELECT_BY_CLIENTE_PORCENTUAL_PERIODO(conditions);

            query += " ORDER BY \"P_VENTA_ABSOLUTA\" DESC";

            return query;
        }

		public static string SELECT_BY_PRODUCTO_PORCENTUAL_MENSUAL(QueryConditions conditions)
		{
			string query;

			query = SELECT_FIELDS(ETipoQuery.VENTA_PRODUCTO_PORCENTUAL) +
					JOIN_BASE_BY_CLIENTE_PORCENTUAL_MENSUAL(conditions) +
					WHERE(conditions);

			query += " GROUP BY P.\"OID\", P.\"NOMBRE\", F.\"ANIO\", F.\"MES\", VAB.\"VENTA_ABSOLUTA\", VAB.\"BENEFICIO_ABSOLUTO\"";

			return query;
		}
		public static string SELECT_BY_PRODUCTO_PORCENTUAL_VENTA_MENSUAL(QueryConditions conditions)
		{
			string query;

			query = SELECT_BY_PRODUCTO_PORCENTUAL_MENSUAL(conditions);

			query += " ORDER BY F.\"ANIO\", F.\"MES\", \"P_VENTA_ABSOLUTA\" DESC";

			return query;
		}
		public static string SELECT_BY_PRODUCTO_PORCENTUAL_BENEFICIO_MENSUAL(QueryConditions conditions)
		{
			string query;

			query = SELECT_BY_PRODUCTO_PORCENTUAL_MENSUAL(conditions);

			query += " ORDER BY F.\"ANIO\", F.\"MES\", \"P_BENEFICIO_ABSOLUTO\" DESC";

			return query;
		}

		#endregion

		#region SQL HISTORICO

		public static string SELECT_FIELDS_HISTORICO()
		{
			string query;

			query = "SELECT " + (long)ETipoQuery.HISTORICO_PRECIOS + " AS \"TIPO_QUERY\"" +
					"		,random() * 100000000 AS \"OID\"" +
					"		,CL.\"OID\" AS \"OID_CLIENTE\"" +
					"       ,CL.\"CODIGO\" AS \"CODIGO_CLIENTE\"" +
					"       ,CL.\"NOMBRE\" AS \"CLIENTE\"" +
					"       ,PR.\"OID\" AS \"OID_PRODUCTO\"" +
					"       ,PR.\"CODIGO\" AS \"CODIGO_PRODUCTO\"" +
					"       ,PR.\"NOMBRE\" AS \"PRODUCTO\"" +
					"       ,CF.\"PRECIO_VENTA\" AS \"PRECIO_VENTA\"" +
					"       ,CF.\"KILOS\" AS \"KILOS\"" +
					"       ,CF.\"SUBTOTAL\" AS \"VENTA_TOTAL\"" +
					"       ,CF.\"FECHA\" AS \"FECHA\"";

			return query;
		}

		internal static string JOIN_BASE_HISTORICO(QueryConditions conditions)
		{
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string fe = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string cf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string fm = nHManager.Instance.GetSQLTable(typeof(FamilyRecord));

			string query;

			query = " FROM " + cl + " AS CL" +
					" INNER JOIN (SELECT FE.\"OID_CLIENTE\" AS \"OID_CLIENTE\"" +
					"                   ,CF.\"OID_PRODUCTO\" AS \"OID_PRODUCTO\"" +
					"					,CF.\"PRECIO\" AS \"PRECIO_VENTA\"" +
					"                   ,SUM(CF.\"CANTIDAD\") AS \"KILOS\"" +
					"                   ,SUM(CF.\"SUBTOTAL\") AS \"SUBTOTAL\"" +
					"					,MAX(FE.\"FECHA\") AS \"FECHA\"" +
					"               FROM " + cf + " AS CF" +
					"				INNER JOIN " + fe + " AS FE ON FE.\"OID\" = CF.\"OID_FACTURA\"" +
                    "               WHERE CF.\"OID_KIT\" = 0" +
					"				AND FE.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
					"               GROUP BY FE.\"OID_CLIENTE\", CF.\"OID_PRODUCTO\", CF.\"PRECIO\"" +
					"				ORDER BY \"FECHA\")" +
					"       AS CF ON CF.\"OID_CLIENTE\" = CL.\"OID\"" +
					" INNER JOIN " + pr + " AS PR ON PR.\"OID\" = CF.\"OID_PRODUCTO\"" +
					" INNER JOIN " + fm + " AS FM ON FM.\"OID\" = PR.\"OID_FAMILIA\"";

			return query;
		}

		public static string SELECT_HISTORICO(QueryConditions conditions)
		{
			string query;

			query = SELECT_FIELDS_HISTORICO() +
					JOIN_BASE_HISTORICO(conditions) +
					WHERE_HISTORICO(conditions);

			return query;
		}

		internal static string WHERE_HISTORICO(QueryConditions conditions)
		{
			string query = string.Empty;

			query = " WHERE TRUE ";

			query += " AND PR.\"BENEFICIO_CERO\" = FALSE";	

			if (conditions.Cliente != null)
				query += " AND CL.\"OID\" = " + conditions.Cliente.Oid;

			if (conditions.Producto != null)
				query += " AND PR.\"OID\" = " + conditions.Producto.Oid;

			if (conditions.Familia != null)
				query += " AND FM.\"OID\" = " + conditions.Familia.Oid;

			return query;
		}

		public static string SELECT_HISTORICO_CLIENTES(QueryConditions conditions)
		{
			string query;

			query = SELECT_HISTORICO(conditions);

			string order = ((conditions.Order == ListSortDirection.Ascending) ? " ASC" : " DESC");

			query += " ORDER BY \"CLIENTE\"" + order + ", \"PRODUCTO\"" + order;

			return query;
		}

		public static string SELECT_HISTORICO_PRODUCTOS(QueryConditions conditions)
		{
			string query;

			query = SELECT_HISTORICO(conditions);

			string order = ((conditions.Order == ListSortDirection.Ascending) ? " ASC" : " DESC");

			query += " ORDER BY \"PRODUCTO\"" + order + ", \"CLIENTE\"" + order;

			return query;
		}

		#endregion
    }
}



