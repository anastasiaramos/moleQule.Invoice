using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
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
    public class ChargeSummary : ReadOnlyBaseEx<ChargeSummary>
    {
        #region Attributes

        private long _oid_cliente;
        private string _vat_number = string.Empty;
        private string _nombre = string.Empty;
        private string _observaciones = string.Empty;
        private string _numero_cliente = string.Empty;
        private decimal _total_facturado;
        private decimal _cobrado;
        private decimal _limite_credito;
        private decimal _credito_dispuesto;
        private decimal _credito_pendiente;
        private decimal _efectos_negociados;
        private decimal _efectos_devueltos;
        private decimal _efectos_pendientes_vto;
		private decimal _dudoso_cobro;
        private decimal _gastos_cobro;
        private decimal _gastos_demora;
        private decimal _condiciones_venta;

		#endregion

		#region Properties

		public virtual long OidCliente { get { return _oid_cliente; } set { _oid_cliente = value; } }
        public virtual string Codigo { get { return _vat_number; } set { _vat_number = value; } } /*DEPRECATED*/
		public virtual string VatNumber { get { return _vat_number; } set { _vat_number = value; } }
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual string NumeroCliente { get { return _numero_cliente; } set { _numero_cliente = value; } }
        public virtual decimal TotalFacturado { get { return _total_facturado; } set { _total_facturado = value; } }
        public virtual decimal Cobrado { get { return _cobrado; } set { _cobrado = value; } }
        public virtual decimal Pendiente { get { return _total_facturado - _cobrado - _efectos_negociados /*- _efectos_devueltos */- _efectos_pendientes_vto - _dudoso_cobro; } }
        public virtual decimal LimiteCredito { get { return _limite_credito; } set { _limite_credito = value; } }
        public virtual decimal CreditoDispuesto { get { return _credito_dispuesto; } set { _credito_dispuesto = value; } }
        public virtual decimal CreditoPendiente { get { return _credito_pendiente; } set { _credito_pendiente = value; } }
        public virtual decimal EfectosNegociados { get { return _efectos_negociados; } set { _efectos_negociados = value; } }
        public virtual decimal EfectosDevueltos { get { return _efectos_devueltos; } set { _efectos_devueltos = value; } }
        public virtual decimal EfectosPendientesVto { get { return _efectos_pendientes_vto; } set { _efectos_pendientes_vto = value; } }
		public virtual decimal DudosoCobro { get { return _dudoso_cobro; } set { _dudoso_cobro = value; } }
        public virtual decimal GastosCobro { get { return _gastos_cobro; } set { _gastos_cobro = value; } }
        public virtual decimal GastosDemora { get { return _gastos_demora; } set { _gastos_demora = value; } }
        public virtual decimal CondicionesVenta { get { return _condiciones_venta; } set { _condiciones_venta = value; } }

        #endregion

		#region Business Methods

		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID_CLIENTE");
			_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
			_vat_number = Format.DataReader.GetString(source, "VAT_NUMBER");
			_nombre = Format.DataReader.GetString(source, "CLIENTE");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES_CLIENTE");
			_numero_cliente = Format.DataReader.GetString(source, "ID_CLIENTE");
			_total_facturado = Format.DataReader.GetDecimal(source, "TOTAL_FACTURADO");
			_cobrado = Format.DataReader.GetDecimal(source, "COBRADO");
			_efectos_negociados = Format.DataReader.GetDecimal(source, "EFECTOS_NEGOCIADOS");
			_efectos_devueltos = Format.DataReader.GetDecimal(source, "VENCIDO");
			_efectos_pendientes_vto = Format.DataReader.GetDecimal(source, "EFECTOS_PENDIENTES");
			_dudoso_cobro = Format.DataReader.GetDecimal(source, "DUDOSO_COBRO");
			_limite_credito = Format.DataReader.GetDecimal(source, "LIMITE_CREDITO");
			_credito_dispuesto = Format.DataReader.GetDecimal(source, "CREDITO_DISPUESTO");
			_credito_pendiente = Format.DataReader.GetDecimal(source, "CREDITO_PENDIENTE");
			_credito_pendiente = _credito_pendiente < 0 ? 0 : _credito_pendiente;
            _gastos_demora = Format.DataReader.GetDecimal(source, "GASTOS_DEMORA");
            _gastos_cobro = Format.DataReader.GetDecimal(source, "GASTOS_COBRO");
            _condiciones_venta = Format.DataReader.GetDecimal(source, "CONDICIONES_VENTA");
		}

		#endregion

		#region Factory Methods

		protected ChargeSummary() { /* require use of factory methods */ }
        private ChargeSummary(ClienteInfo cliente)
        {
            _oid_cliente = cliente.Oid;
            Oid = cliente.Oid;
            _vat_number = cliente.VatNumber;
            _nombre = cliente.Nombre;
            _numero_cliente = cliente.Codigo;
            _observaciones = cliente.Observaciones;

            _total_facturado = 0.0m;			

            foreach (OutputInvoiceInfo factura in OutputInvoiceList.GetByClienteList(cliente, false)) 
            {
                _total_facturado += factura.Total;
            }

            _cobrado = 0;
            _efectos_devueltos = 0;
            _efectos_negociados = 0;
            _efectos_pendientes_vto = 0;

            if (cliente.Cobros == null || cliente.Cobros.Count == 0)
                cliente.LoadChilds(typeof(Charge), true);

            foreach (ChargeInfo cobro in cliente.Cobros)
            {
				if (cobro.EEstado == Common.EEstado.Anulado) continue;

                if (cobro.EEstadoCobro == EEstado.Charged && cobro.Vencimiento <= DateTime.Now)
                    _cobrado += cobro.Importe;
                else if (cobro.EEstadoCobro == EEstado.Charged && cobro.Vencimiento > DateTime.Now)
                    _efectos_negociados += cobro.Importe;
                else if (cobro.EEstadoCobro != EEstado.Charged && cobro.Vencimiento > DateTime.Now)
                    _efectos_pendientes_vto += cobro.Importe;
                else if (cobro.EEstadoCobro != EEstado.Charged && cobro.Vencimiento <= DateTime.Now)
                    _efectos_devueltos += cobro.Importe;
            }

            _limite_credito = cliente.LimiteCredito;
            _credito_dispuesto = cliente.CreditoDispuesto;
            _credito_pendiente = _limite_credito - _credito_dispuesto;
            _credito_pendiente = _credito_pendiente < 0 ? 0 : _credito_pendiente;
        }
        private ChargeSummary(IDataReader source)
        {
			CopyValues(source);
        }

        public void Refresh(Cliente cliente)
        {
            _oid_cliente = cliente.Oid;
            Oid = cliente.Oid;
            _vat_number = cliente.VatNumber;
            _nombre = cliente.Nombre;
            _numero_cliente = cliente.Codigo;
            _observaciones = cliente.Observaciones;
            
            _cobrado = 0;
            _efectos_devueltos = 0;
            _efectos_negociados = 0;
            _efectos_pendientes_vto = 0;

            foreach (Charge cobro in cliente.Cobros)
            {
				if (cobro.EEstado == Common.EEstado.Anulado) continue;

                if (cobro.EEstadoCobro == EEstado.Charged && cobro.Vencimiento <= DateTime.Now)
                    _cobrado += cobro.Importe;
                else if (cobro.EEstadoCobro == EEstado.Charged && cobro.Vencimiento > DateTime.Now)
                    _efectos_negociados += cobro.Importe;
                else if (cobro.EEstadoCobro != EEstado.Charged && cobro.Vencimiento > DateTime.Now)
                    _efectos_pendientes_vto += cobro.Importe;
                else if (cobro.EEstadoCobro != EEstado.Charged && cobro.Vencimiento <= DateTime.Now)
                    _efectos_devueltos += cobro.Importe;
            }

			cliente.CreditoDispuesto = cliente.TotalFacturado - _cobrado;
			
            _credito_dispuesto = cliente.CreditoDispuesto;
			_credito_pendiente = cliente.LimiteCredito - _credito_dispuesto;
            _credito_pendiente = (_credito_pendiente < 0) ? 0 : _credito_pendiente;			
        }

        public static ChargeSummary Get(IDataReader source)
        {
            if (source == null) return null;

            return new ChargeSummary(source);
        }
        public static ChargeSummary Get(ClienteInfo cliente)
        {
            return new ChargeSummary(cliente);
        }
        public static ChargeSummary Get(ETipoCobro tipo)
        {
			CriteriaEx criteria = Charge.GetCriteria(Charge.OpenSession());
			criteria.Childs = false;

			//No criteria. Retrieve all de List
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(tipo);

			ChargeSummary obj = DataPortal.Fetch<ChargeSummary>(criteria);

			CloseSession(criteria.SessionCode);
            return obj;
        }

		public static ChargeSummary GetByCliente(ClienteInfo cliente) { return GetByCliente(cliente.Oid); }
		public static ChargeSummary GetByCliente(long oidCliente)
		{
			CriteriaEx criteria = Charge.GetCriteria(Charge.OpenSession());
			criteria.Childs = false;

			//No criteria. Retrieve all de List
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT_BY_CLIENTE(oidCliente);

			ChargeSummary obj = DataPortal.Fetch<ChargeSummary>(criteria);

			CloseSession(criteria.SessionCode);
			return obj;
		}

        #endregion

		#region Autorization Rules

		public static bool CanAddObject()
		{
			return AutorizationRulesControler.CanAddObject(Resources.SecureItems.COBRO);
		}

		public static bool CanGetObject()
		{
			return AutorizationRulesControler.CanGetObject(Resources.SecureItems.COBRO);
		}

		public static bool CanDeleteObject()
		{
			return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.COBRO);
		}

		public static bool CanEditObject()
		{
			return AutorizationRulesControler.CanEditObject(Resources.SecureItems.COBRO);
		}

		#endregion

		#region Root Data Access

		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
			{
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

                IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

				if (reader.Read())
					CopyValues(reader);
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

		#region SQL

		internal enum ETipoQuery { GENERAL = 0, FOMENTO = 1, REA = 2}

		internal static string SELECT_FIELDS(ETipoQuery queryType)
		{
			string query = string.Empty;

			switch (queryType)
			{
				case ETipoQuery.GENERAL:

					query = @"
						SELECT CL.""OID"" AS ""OID_CLIENTE""
							,CL.""VAT_NUMBER"" AS ""VAT_NUMBER""
							,CL.""NOMBRE"" AS ""CLIENTE""
							,CL.""CODIGO"" AS ""ID_CLIENTE""
							,CL.""OBSERVACIONES"" AS ""OBSERVACIONES_CLIENTE""
							,COALESCE(""TOTAL_FACTURADO"", 0) AS ""TOTAL_FACTURADO""
							,COALESCE(""TOTAL_COBROS"", 0) AS ""COBRADO""
							,COALESCE(""TOTAL_EFECTOS_NEGOCIADOS"", 0) AS ""EFECTOS_NEGOCIADOS""
							,COALESCE(""TOTAL_VENCIDO"", 0) AS ""VENCIDO""
							,COALESCE(""TOTAL_NO_COBRADO"", 0) AS ""EFECTOS_PENDIENTES""
							,COALESCE(""TOTAL_DUDOSO_COBRO"", 0) + COALESCE(""TOTAL_DUDOSO_COBRO_VENCIDO"", 0) AS ""DUDOSO_COBRO""
							,COALESCE(""LIMITE_CREDITO"", 0) AS ""LIMITE_CREDITO""
							,(COALESCE(""TOTAL_FACTURADO"", 0) - COALESCE(""TOTAL_COBROS"", 0)) AS ""CREDITO_DISPUESTO""
							,COALESCE(""LIMITE_CREDITO"" - (""TOTAL_FACTURADO"" - COALESCE(""TOTAL_COBROS"", 0)), 0) AS ""CREDITO_PENDIENTE""
							,COALESCE(""GASTOS_COBRO"", 0) AS ""GASTOS_COBRO""
							,COALESCE(""CONDICIONES_VENTA"", 0) AS ""CONDICIONES_VENTA""
							,COALESCE(""GASTOS_DEMORA"", 0) AS ""GASTOS_DEMORA""";
					break;

                case ETipoQuery.FOMENTO:

					query = "SELECT 0 AS \"OID_CLIENTE\"" +
							"		,'' AS \"VAT_NUMBER\"" +
							"		,'COBROS DE FOMENTO' AS \"CLIENTE\"" +
							"		,'' AS \"ID_CLIENTE\"" +
							"       ,'' AS \"OBSERVACIONES_CLIENTE\"" +
							"       ,COALESCE(\"TOTAL_FACTURADO\", 0) AS \"TOTAL_FACTURADO\"" +
							"       ,COALESCE(\"TOTAL_COBROS\", 0) AS \"COBRADO\"" +
							"       ,COALESCE(\"TOTAL_EFECTOS_NEGOCIADOS\", 0) AS \"EFECTOS_NEGOCIADOS\"" +
							"       ,COALESCE(\"TOTAL_VENCIDO\", 0) AS \"VENCIDO\"" +
							"       ,COALESCE(\"TOTAL_NO_COBRADO\", 0) AS \"EFECTOS_PENDIENTES\"" +
							"       ,0 AS \"DUDOSO_COBRO\"" +
							"       ,0 AS \"LIMITE_CREDITO\"" +
							"       ,COALESCE(\"CREDITO_DISPUESTO\", 0) AS \"CREDITO_DISPUESTO\"" +
							"       ,0 AS \"CREDITO_PENDIENTE\"" +
							"       ,COALESCE(\"GASTOS_COBRO\", 0) AS \"GASTOS_COBRO\"" +
							"       ,0 AS \"CONDICIONES_VENTA\"" +
							"       ,0 AS \"GASTOS_DEMORA\"";
                    break;

                case ETipoQuery.REA:

                    query = "SELECT 0 AS \"OID_CLIENTE\"" +
                            "		,'' AS \"VAT_NUMBER\"" +
                            "		,'COBROS DE REA' AS \"CLIENTE\"" +
                            "		,'' AS \"ID_CLIENTE\"" +
                            "       ,'' AS \"OBSERVACIONES_CLIENTE\"" +
                            "       ,COALESCE(\"TOTAL_FACTURADO\", 0) AS \"TOTAL_FACTURADO\"" +
                            "       ,COALESCE(\"TOTAL_COBROS\", 0) AS \"COBRADO\"" +
                            "       ,COALESCE(\"TOTAL_EFECTOS_NEGOCIADOS\", 0) AS \"EFECTOS_NEGOCIADOS\"" +
                            "       ,COALESCE(\"TOTAL_VENCIDO\", 0) AS \"VENCIDO\"" +
                            "       ,COALESCE(\"TOTAL_NO_COBRADO\", 0) AS \"EFECTOS_PENDIENTES\"" +
                            "       ,0 AS \"DUDOSO_COBRO\"" +
                            "       ,0 AS \"LIMITE_CREDITO\"" +
                            "       ,COALESCE(\"CREDITO_DISPUESTO\", 0) AS \"CREDITO_DISPUESTO\"" +
                            "       ,0 AS \"CREDITO_PENDIENTE\"" +
                            "       ,COALESCE(\"GASTOS_COBRO\", 0) AS \"GASTOS_COBRO\"" +
                            "       ,0 AS \"CONDICIONES_VENTA\"" +
                            "       ,0 AS \"GASTOS_DEMORA\"";
                    break;
			}

			return query;
		}

		internal static string INNER_COBROS(QueryConditions conditions)
		{
			string fc = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string ef = nHManager.Instance.GetSQLTable(typeof(FinancialCashRecord));

            string tipos_efectos = "(" + (long)EMedioPago.Pagare + ", " + (long)EMedioPago.Cheque + ")";

			string query = string.Empty;

			query = @"
				LEFT JOIN ( SELECT SUM(""TOTAL"") AS ""TOTAL_FACTURADO"",
					            ""OID_CLIENTE""
					        FROM " + fc + @"
					        WHERE ""FECHA"" BETWEEN '" + conditions.FechaIniLabel + @"' AND '" + conditions.FechaFinLabel + @"'
					        GROUP BY ""OID_CLIENTE"")
					AS F ON F.""OID_CLIENTE"" = CL.""OID""
				LEFT JOIN ( SELECT SUM(""TOTAL_COBROS"") AS ""TOTAL_COBROS"", ""OID_CLIENTE""
                            FROM (  SELECT SUM(""IMPORTE"") AS ""TOTAL_COBROS""
					                    ,""OID_CLIENTE""
					                FROM " + cb + @"
					                WHERE ""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
					                    AND ""ESTADO_COBRO"" = " + (long)EEstado.Charged + @" AND ""VENCIMIENTO"" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + @"'
                                        AND ""ESTADO"" != " + (long)EEstado.Anulado + @" AND ""MEDIO_PAGO"" NOT IN " + tipos_efectos + @"
					                GROUP BY ""OID_CLIENTE""
                                    UNION
                                    SELECT SUM(CH.""IMPORTE"") AS ""TOTAL_COBROS"", CH.""OID_CLIENTE""
                                    FROM " + cb + @" AS CH
                                    INNER JOIN " + ef + @" AS EF ON EF.""OID_COBRO"" = CH.""OID"" AND EF.""ESTADO"" != " + (long)EEstado.Anulado + @"
                                        AND EF.""ESTADO_COBRO"" = " + (long)EEstado.Charged + @" AND EF.""VENCIMIENTO"" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + @"'
                                    WHERE CH.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
                                        AND CH.""ESTADO"" != " + (long)EEstado.Anulado + @" AND CH.""MEDIO_PAGO"" IN " + tipos_efectos + @"
                                    GROUP BY CH.""OID_CLIENTE"") AS CC1
                            GROUP BY ""OID_CLIENTE"")
					AS C1 ON C1.""OID_CLIENTE"" = CL.""OID""
                LEFT JOIN ( SELECT SUM(""TOTAL_EFECTOS_NEGOCIADOS"") AS ""TOTAL_EFECTOS_NEGOCIADOS"", ""OID_CLIENTE""
                            FROM(   SELECT SUM(""IMPORTE"") AS ""TOTAL_EFECTOS_NEGOCIADOS"",
					                    ""OID_CLIENTE""
					                FROM " + cb + @"
					                WHERE ""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
					                    AND ""ESTADO_COBRO"" = " + (long)EEstado.Charged + @" AND ""VENCIMIENTO"" > '" + DateTime.Today.ToString("MM/dd/yyyy") + @"'
					                    AND ""ESTADO"" != " + (long)EEstado.Anulado + @" AND ""MEDIO_PAGO"" NOT IN " + tipos_efectos + @"
					                GROUP BY ""OID_CLIENTE""
                                    UNION
                                    SELECT SUM(CH.""IMPORTE"") AS ""TOTAL_EFECTOS_NEGOCIADOS"", CH.""OID_CLIENTE""
                                    FROM " + cb + @" AS CH
                                    INNER JOIN " + ef + @" AS EF ON EF.""OID_COBRO"" = CH.""OID"" AND EF.""ESTADO"" != " + (long)EEstado.Anulado + @"
                                        AND EF.""ESTADO_COBRO"" = " + (long)EEstado.Charged + @" AND EF.""VENCIMIENTO"" > '" + DateTime.Today.ToString("MM/dd/yyyy") + @"'
                                    WHERE CH.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
                                        AND CH.""ESTADO"" != " + (long)EEstado.Anulado + @" AND CH.""MEDIO_PAGO"" IN " + tipos_efectos + @"
                                    GROUP BY CH.""OID_CLIENTE"") AS CC2
                            GROUP BY ""OID_CLIENTE"")
					AS C2 ON C2.""OID_CLIENTE"" = CL.""OID""
				LEFT JOIN ( SELECT SUM(""TOTAL_VENCIDO"") AS ""TOTAL_VENCIDO"", ""OID_CLIENTE""
                            FROM (  SELECT SUM(""IMPORTE"") AS ""TOTAL_VENCIDO""
					                    ,""OID_CLIENTE""
					                FROM " + cb + @"
					                WHERE ""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
					                    AND ""ESTADO_COBRO"" != " + (long)EEstado.Charged + @" AND ""VENCIMIENTO"" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + @"'
					                    AND ""ESTADO"" != " + (long)EEstado.Anulado + @" AND ""MEDIO_PAGO"" NOT IN " + tipos_efectos + @"
					                GROUP BY ""OID_CLIENTE""
                                    UNION
                                    SELECT SUM(CH.""IMPORTE"") AS ""TOTAL_EFECTOS_NEGOCIADOS"", CH.""OID_CLIENTE""
                                    FROM " + cb + @" AS CH
                                    INNER JOIN " + ef + @" AS EF ON EF.""OID_COBRO"" = CH.""OID"" AND EF.""ESTADO"" != " + (long)EEstado.Anulado + @"
                                        AND EF.""ESTADO_COBRO"" != " + (long)EEstado.Charged + @" AND EF.""VENCIMIENTO"" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + @"'
                                    WHERE CH.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
                                        AND CH.""ESTADO"" != " + (long)EEstado.Anulado + @" AND CH.""MEDIO_PAGO"" IN " + tipos_efectos + @"
                                    GROUP BY CH.""OID_CLIENTE"") AS CC3
                            GROUP BY ""OID_CLIENTE"")
					AS C3 ON C3.""OID_CLIENTE"" = CL.""OID""
				LEFT JOIN ( SELECT SUM(""TOTAL_NO_COBRADO"") AS ""TOTAL_NO_COBRADO"", ""OID_CLIENTE""
                            FROM (  SELECT SUM(""IMPORTE"") AS ""TOTAL_NO_COBRADO"", ""OID_CLIENTE""
					                FROM " + cb + @"
					                WHERE ""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
					                    AND ""ESTADO_COBRO"" != " + (long)EEstado.Charged + @" AND ""VENCIMIENTO"" > '" + DateTime.Today.ToString("MM/dd/yyyy") + @"'
					                    AND ""ESTADO"" != " + (long)EEstado.Anulado + @" AND ""MEDIO_PAGO"" NOT IN " + tipos_efectos + @"
					                GROUP BY ""OID_CLIENTE""
                                    UNION
                                    SELECT SUM(CH.""IMPORTE"") AS ""TOTAL_EFECTOS_NEGOCIADOS"", CH.""OID_CLIENTE""
                                    FROM " + cb + @" AS CH
                                    INNER JOIN " + ef + @" AS EF ON EF.""OID_COBRO"" = CH.""OID"" AND EF.""ESTADO"" != " + (long)EEstado.Anulado + @"
                                        AND EF.""ESTADO_COBRO"" != " + (long)EEstado.Charged + @" AND EF.""VENCIMIENTO"" > '" + DateTime.Today.ToString("MM/dd/yyyy") + @"'
                                    WHERE CH.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
                                        AND CH.""ESTADO"" != " + (long)EEstado.Anulado + @" AND CH.""MEDIO_PAGO"" IN " + tipos_efectos + @"
                                    GROUP BY CH.""OID_CLIENTE"") AS CC4
                            GROUP BY ""OID_CLIENTE"")
					AS C4 ON C4.""OID_CLIENTE"" = CL.""OID""";

			string cf = nHManager.Instance.GetSQLTable(typeof(ChargeOperationRecord));

			//Efectos Vencidos de Facturas de Dudoso Cobro
			query += @"
				LEFT JOIN ( SELECT SUM(""TOTAL_DUDOSO_COBRO_VENCIDO"") AS ""TOTAL_DUDOSO_COBRO_VENCIDO"", ""OID_CLIENTE""
                            FROM (  SELECT SUM(CF.""CANTIDAD"") AS ""TOTAL_DUDOSO_COBRO_VENCIDO""
					                    ,CB.""OID_CLIENTE""
					                FROM " + cf + @" AS CF
					                INNER JOIN " + fc + @" FC ON FC.""OID"" = CF.""OID_FACTURA""
					                INNER JOIN " + cb + @" AS CB ON CF.""OID_COBRO"" = CB.""OID"" AND CB.""ESTADO_COBRO"" != " + (long)EEstado.Charged + @" 
                                        AND CB.""ESTADO"" != " + (long)EEstado.Anulado + @" AND CB.""VENCIMIENTO"" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + @"'
                                        AND CB.""MEDIO_PAGO"" NOT IN " + tipos_efectos + @"
					                WHERE CB.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
					                    AND CB.""ESTADO"" != " + ((long)EEstado.Anulado) + @"
					                    AND  FC.""ESTADO_COBRO"" = " + ((long)EEstado.DudosoCobro) + @"
					                GROUP BY CB.""OID_CLIENTE""
                                    UNION
                                    SELECT SUM(CF.""CANTIDAD"") AS ""TOTAL_DUDOSO_COBRO_VENCIDO""
                                        ,CB.""OID_CLIENTE""
                                    FROM " + cf + @" AS CF
                                    INNER JOIN " + fc + @" AS FC ON FC.""OID"" = CF.""OID_FACTURA""
                                    INNER JOIN " + cb + @" AS CB ON CF.""OID_COBRO"" = CB.""OID"" AND CB.""ESTADO"" != " + (long)EEstado.Anulado + @" 
                                        AND CB.""MEDIO_PAGO"" IN " + tipos_efectos + @"
                                    INNER JOIN " + ef + @" AS EF ON EF.""OID_COBRO"" = CB.""OID"" AND EF.""ESTADO_COBRO"" != " + (long)EEstado.Charged + @"
                                        AND EF.""ESTADO"" != " + (long)EEstado.Anulado + @" AND EF.""VENCIMIENTO"" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + @"'
					                WHERE CB.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
					                    AND CB.""ESTADO"" != " + (long)EEstado.Anulado + @"
					                    AND  FC.""ESTADO_COBRO"" = " + (long)EEstado.DudosoCobro + @"
					                GROUP BY CB.""OID_CLIENTE"") AS CC5
                            GROUP BY ""OID_CLIENTE"")
					AS C5 ON C5.""OID_CLIENTE"" = CL.""OID""";

			//No Cobrado de Facturas de Dudoso Cobro
			query += @" 
				LEFT JOIN ( SELECT SUM(FC.""TOTAL"" - COALESCE(CF.""CANTIDAD"", 0)) AS ""TOTAL_DUDOSO_COBRO""
					            ,FC.""OID_CLIENTE""
					        FROM " + fc + @" AS FC
					        LEFT JOIN ( SELECT SUM(CF.""CANTIDAD"") AS ""CANTIDAD""
					                        ,CF.""OID_FACTURA""
					                    FROM " + cf + @" AS CF
					                    INNER JOIN " + cb + @" AS CB ON CF.""OID_COBRO"" = CB.""OID"" AND CB.""ESTADO"" != " + (long)EEstado.Anulado + @"
					                    GROUP BY CF.""OID_FACTURA"")
					            AS CF ON CF.""OID_FACTURA"" = FC.""OID""
					        WHERE FC.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
					            AND FC.""ESTADO_COBRO"" = " + (long)EEstado.DudosoCobro + @"
					        GROUP BY FC.""OID_CLIENTE"")
					AS C6 ON C6.""OID_CLIENTE"" = CL.""OID""";

			//Gastos Cobro
			query += @"  
				LEFT JOIN ( SELECT SUM(""GASTOS_COBRO"") AS ""GASTOS_COBRO"", ""OID_CLIENTE""
							FROM (  SELECT SUM(""GASTOS_BANCARIOS"") AS ""GASTOS_COBRO""
										, ""OID_CLIENTE""
									FROM " + cb + @"
									WHERE ""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
										AND ""ESTADO"" != " + ((long)EEstado.Anulado).ToString() + @" AND ""MEDIO_PAGO"" NOT IN " + tipos_efectos + @"
									GROUP BY ""OID_CLIENTE""
									UNION
									SELECT SUM(CH.""GASTOS_BANCARIOS"") AS ""GASTOS_COBRO""    
										,CH.""OID_CLIENTE""
									FROM " + cb + @" AS CH
									INNER JOIN " + ef + @" AS EF ON EF.""OID_COBRO"" = CH.""OID"" AND EF.""ESTADO"" != " + (long)EEstado.Anulado + @"
									WHERE CH.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
										AND CH.""ESTADO"" != " + ((long)EEstado.Anulado).ToString() + @" AND CH.""MEDIO_PAGO"" IN " + tipos_efectos + @"
									GROUP BY CH.""OID_CLIENTE"") AS CC7
							GROUP BY ""OID_CLIENTE"")
					AS C7 ON C7.""OID_CLIENTE"" = CL.""OID""";

			string cpf = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
			string pc = nHManager.Instance.GetSQLTable(typeof(ClientProductRecord));

			//Condiciones de venta
			query += @"
				LEFT JOIN (SELECT F.""OID_CLIENTE"" AS ""OID_CLIENTE""
								,SUM((""PRECIO_COMPRA"" - CF.""PRECIO"") * CASE WHEN (CF.""FACTURACION_BULTO"" = TRUE) THEN ""CANTIDAD_BULTOS"" ELSE ""CANTIDAD"" END) AS ""CONDICIONES_VENTA""
							FROM " + fc + @" AS F
							INNER JOIN " + cpf + @" AS CF ON (CF.""OID_FACTURA"" = F.""OID"")
							INNER JOIN " + pc + @" AS PC ON (PC.""OID_PRODUCTO"" = CF.""OID_PRODUCTO"" AND PC.""OID_CLIENTE"" = F.""OID_CLIENTE"")
							WHERE F.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
								AND CF.""PRECIO"" < PC.""PRECIO_COMPRA"" 
								AND CF.""FACTURACION_BULTO"" = PC.""FACTURACION_BULTO"" 
								AND PC.""FECHA_VALIDEZ"" >= F.""FECHA""
							GROUP BY F.""OID_CLIENTE"")
					AS C8 ON C8.""OID_CLIENTE"" = CL.""OID""";

			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			decimal tipo_interes = Library.Invoice.ModulePrincipal.GetTipoInteresPorGastosDemoraSetting();
			
			//Gastos Demora
			query += @"
				LEFT JOIN (SELECT ""OID_CLIENTE""
								,SUM(""GASTOS_DEMORA"") AS ""GASTOS_DEMORA""
							FROM (SELECT F.""OID_CLIENTE""
										,""OID_FACTURA""
										,F.""TOTAL"" AS ""TOTAL_FACTURA""
										,SUM(CF.""CANTIDAD"") AS ""COBRADO""
										,F.""TOTAL"" * (current_date - cast(F.""FECHA"" AS date)) * (CASE WHEN (CL.""TIPO_INTERES"" != 0) THEN CL.""TIPO_INTERES"" ELSE " + tipo_interes.ToString(new CultureInfo("en-US")) + @" END) / 36000 AS ""GASTOS_DEMORA""
									FROM " + fc + @" AS F
									INNER JOIN " + cl + @" AS CL ON (CL.""OID"" = F.""OID_CLIENTE"")
									INNER JOIN " + cf + @" AS CF ON (F.""OID"" = CF.""OID_FACTURA"")
									INNER JOIN " + cb + @" AS C ON (CF.""OID_COBRO"" = C.""OID"" 
										AND C.""ESTADO"" != " + (long)EEstado.Anulado + @"										
										AND C.""ESTADO_COBRO"" = " + (long)EEstado.Charged + @" 										
										AND C.""VENCIMIENTO"" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + @"')
									WHERE F.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"'
									GROUP BY F.""OID_CLIENTE"", ""OID_FACTURA"", F.""TOTAL"", F.""FECHA"", CL.""TIPO_INTERES"")
								AS CF
							WHERE CF.""COBRADO"" < CF.""TOTAL_FACTURA""
							GROUP BY ""OID_CLIENTE"")
					AS C9 ON C9.""OID_CLIENTE"" = CL.""OID""";

			return query;
		}

        internal static string INNER_COBROS_FOMENTO(QueryConditions conditions)
        {
            string lf = nHManager.Instance.GetSQLTable(typeof(LineaFomentoRecord));
            string ap = nHManager.Instance.GetSQLTable(typeof(GrantPeriodRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            long oid_ayuda_fomento = Library.Store.ModulePrincipal.GetAyudaFomentoSetting();

            string query = string.Empty;

            query = " LEFT JOIN (	SELECT SUM(CASE WHEN (COALESCE(AP.\"TIPO_AYUDA\", 2) = 2)" +			
			        "                   THEN (\"TOTAL\" * COALESCE(AP.\"PORCENTAJE\", 0) / 100)" +			
			        "                   ELSE COALESCE(AP.\"CANTIDAD\", 0) END) AS \"TOTAL_FACTURADO\"" +	        
		            "               FROM " + lf + " AS LF" +
		            "               LEFT JOIN ( SELECT AP.\"TIPO_DESCUENTO\" AS \"TIPO_AYUDA\""		+			
					"                               ,AP.\"CANTIDAD\" AS \"CANTIDAD\"" +					
					"                               ,AP.\"PORCENTAJE\" AS \"PORCENTAJE\""	 +				
					"                               ,AP.\"FECHA_INI\" AS \"FECHA_INI\"" +					
					"                               ,AP.\"FECHA_FIN\" AS \"FECHA_FIN\"" +				
				    "                           FROM " + ap + " AS AP" +				
				    "                           WHERE AP.\"OID_AYUDA\" = " + oid_ayuda_fomento + "AND \"ESTADO\" != 4)" +	
			        "                   AS AP ON LF.\"FECHA_CONOCIMIENTO\" BETWEEN AP.\"FECHA_INI\" AND AP.\"FECHA_FIN\")" +       
	                "       AS T1 ON TRUE" +
                    " LEFT JOIN (SELECT SUM(\"IMPORTE\") AS \"TOTAL_COBROS\"" +
                    "               FROM " + cb +
                    "				WHERE \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    "               AND \"ESTADO_COBRO\" = " + (long)EEstado.Charged + " AND \"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "				AND \"TIPO_COBRO\" = " + (long)ETipoCobro.Fomento + " AND \"ESTADO\" != " + ((long)EEstado.Anulado).ToString() + ")" +
                    "       AS T2 ON TRUE" +
                    " LEFT JOIN (SELECT SUM(\"IMPORTE\") AS \"TOTAL_EFECTOS_NEGOCIADOS\"" +
                    "               FROM " + cb +
                    "				WHERE \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    "               AND \"ESTADO_COBRO\" = " + (long)EEstado.Charged + " AND \"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "				AND \"TIPO_COBRO\" = " + (long)ETipoCobro.Fomento + " AND \"ESTADO\" != " + ((long)EEstado.Anulado).ToString() + ")" +
                    "       AS T3 ON TRUE" +
                    " LEFT JOIN (SELECT SUM(\"IMPORTE\") AS \"TOTAL_VENCIDO\"" +
                    "               FROM " + cb +
                    "				WHERE \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    "               AND \"ESTADO_COBRO\" != " + (long)EEstado.Charged + " AND \"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "				AND \"TIPO_COBRO\" = " + (long)ETipoCobro.Fomento + " AND \"ESTADO\" != " + ((long)EEstado.Anulado).ToString() + ")" +
                    "       AS T4 ON TRUE" +
                    " LEFT JOIN (SELECT SUM(\"IMPORTE\") AS \"TOTAL_NO_COBRADO\"" +
                    "               FROM " + cb +
                    "				WHERE \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    "               AND \"ESTADO_COBRO\" != " + (long)EEstado.Charged + " AND \"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "				AND \"TIPO_COBRO\" = " + (long)ETipoCobro.Fomento + " AND \"ESTADO\" != " + ((long)EEstado.Anulado).ToString() + ")" +
                    "       AS T5 ON TRUE";

            //Gastos Cobro
            query += " LEFT JOIN (SELECT SUM(\"GASTOS_BANCARIOS\") AS \"GASTOS_COBRO\"" +
                    "               FROM " + cb +
                    "				WHERE \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    "               AND \"TIPO_COBRO\" = " + (long)ETipoCobro.Fomento + " AND \"ESTADO\" != " + ((long)EEstado.Anulado).ToString() + ")" +
                    "       AS T6 ON TRUE";

            string cr = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));
            //Crédito dispuesto
            query += " LEFT JOIN (	SELECT SUM(CASE WHEN (COALESCE(AP.\"TIPO_AYUDA\", 2) = 2)" +
                    "                   THEN (\"TOTAL\" * COALESCE(AP.\"PORCENTAJE\", 0) / 100)" +
                    "                   ELSE COALESCE(AP.\"CANTIDAD\", 0) END) AS \"CREDITO_DISPUESTO\"" +
                    "               FROM " + lf + " AS LF" +
                    "               LEFT JOIN ( SELECT AP.\"TIPO_DESCUENTO\" AS \"TIPO_AYUDA\"" +
                    "                               ,AP.\"CANTIDAD\" AS \"CANTIDAD\"" +
                    "                               ,AP.\"PORCENTAJE\" AS \"PORCENTAJE\"" +
                    "                               ,AP.\"FECHA_INI\" AS \"FECHA_INI\"" +
                    "                               ,AP.\"FECHA_FIN\" AS \"FECHA_FIN\"" +
                    "                           FROM " + ap + " AS AP" +
                    "                           WHERE AP.\"OID_AYUDA\" = " + oid_ayuda_fomento + "AND \"ESTADO\" != 4)" +
                    "                   AS AP ON LF.\"FECHA_CONOCIMIENTO\" BETWEEN AP.\"FECHA_INI\" AND AP.\"FECHA_FIN\"" +
                    "               WHERE LF.\"OID\" NOT IN (   SELECT CR.\"OID_EXPEDIENTE_REA\""+
                    "                                           FROM " + cr + " AS CR" +
                    "                                           INNER JOIN " + cb + " AS CB ON CB.\"OID\" = CR.\"OID_COBRO\" AND CB.\"ESTADO\" != " + (long)EEstado.Anulado + "))" +
                    "       AS T7 ON TRUE";

            query += " GROUP BY \"TOTAL_FACTURADO\", \"TOTAL_COBROS\", \"TOTAL_EFECTOS_NEGOCIADOS\", \"TOTAL_VENCIDO\", \"TOTAL_NO_COBRADO\", \"GASTOS_COBRO\", \"CREDITO_DISPUESTO\"";

            return query;
        }

        internal static string INNER_COBROS_REA(QueryConditions conditions)
        {
            string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string pa = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
            string er = nHManager.Instance.GetSQLTable(typeof(REAExpedientRecord));
            string cr = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));

            string estado = "(" + (long)EEstado.Anulado + "," + (long)EEstado.Desestimado + "," + (long)EEstado.Charged + ")";
            string estado_no_cobrado = "(" + (long)EEstado.Anulado + "," + (long)EEstado.Desestimado + ")";

            string query = string.Empty;

            query = " LEFT JOIN (	SELECT SUM(PR.\"AYUDA_KILO\" * \"KILOS_INICIALES\") AS \"TOTAL_FACTURADO\"    " +        
		            "               FROM " + pa + " AS PT	" +		 
		            "               INNER JOIN " + pr + " AS PR ON PR.\"OID\" = PT.\"OID_PRODUCTO\"" +
		            "               INNER JOIN " + ex + " AS EX ON EX.\"OID\" = PT.\"OID_EXPEDIENTE\"" +
		            "               INNER JOIN " + er + " AS ER ON ER.\"OID_EXPEDIENTE\" = EX.\"OID\" AND ER.\"CODIGO_ADUANERO\" = PR.\"CODIGO_ADUANERO\"" +
		            "               WHERE ER.\"ESTADO\" NOT IN " + estado_no_cobrado + ") "  +    
	                "       AS T1 ON TRUE" +
                    " LEFT JOIN (SELECT SUM(\"IMPORTE\") AS \"TOTAL_COBROS\"" +
                    "               FROM " + cb +
                    "				WHERE \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    "               AND \"ESTADO_COBRO\" = " + (long)EEstado.Charged + " AND \"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "				AND \"TIPO_COBRO\" = " + (long)ETipoCobro.REA + " AND \"ESTADO\" != " + ((long)EEstado.Anulado).ToString() + ")" +
                    "       AS T2 ON TRUE" +
                    " LEFT JOIN (SELECT SUM(\"IMPORTE\") AS \"TOTAL_EFECTOS_NEGOCIADOS\"" +
                    "               FROM " + cb +
                    "				WHERE \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    "               AND \"ESTADO_COBRO\" = " + (long)EEstado.Charged + " AND \"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "				AND \"TIPO_COBRO\" = " + (long)ETipoCobro.REA + " AND \"ESTADO\" != " + ((long)EEstado.Anulado).ToString() + ")" +
                    "       AS T3 ON TRUE" +
                    " LEFT JOIN (SELECT SUM(\"IMPORTE\") AS \"TOTAL_VENCIDO\"" +
                    "               FROM " + cb +
                    "				WHERE \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    "               AND \"ESTADO_COBRO\" != " + (long)EEstado.Charged + " AND \"VENCIMIENTO\" <= '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "				AND \"TIPO_COBRO\" = " + (long)ETipoCobro.REA + " AND \"ESTADO\" != " + ((long)EEstado.Anulado).ToString() + ")" +
                    "       AS T4 ON TRUE" +
                    " LEFT JOIN (SELECT SUM(\"IMPORTE\") AS \"TOTAL_NO_COBRADO\"" +
                    "               FROM " + cb +
                    "				WHERE \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    "               AND \"ESTADO_COBRO\" != " + (long)EEstado.Charged + " AND \"VENCIMIENTO\" > '" + DateTime.Today.ToString("MM/dd/yyyy") + "'" +
                    "				AND \"TIPO_COBRO\" = " + (long)ETipoCobro.REA + " AND \"ESTADO\" != " + ((long)EEstado.Anulado).ToString() + ")" +
                    "       AS T5 ON TRUE";

            //Gastos Cobro
            query += " LEFT JOIN (SELECT SUM(\"GASTOS_BANCARIOS\") AS \"GASTOS_COBRO\"" +
                    "               FROM " + cb +
                    "				WHERE \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'" +
                    "               AND \"TIPO_COBRO\" = " + (long)ETipoCobro.REA + " AND \"ESTADO\" != " + ((long)EEstado.Anulado).ToString() + ")" +
                    "       AS T6 ON TRUE";

            //Crédito dispuesto
            query += " LEFT JOIN (	SELECT SUM(PR.\"AYUDA_KILO\" * \"KILOS_INICIALES\" - COALESCE(CR.\"CANTIDAD\", 0)) AS \"CREDITO_DISPUESTO\"" +
                     "              FROM "+ pa + " AS PT" + 			 
		             "              INNER JOIN " + pr + " AS PR ON PR.\"OID\" = PT.\"OID_PRODUCTO\"" + 
		             "              INNER JOIN " + ex + " AS EX ON EX.\"OID\" = PT.\"OID_EXPEDIENTE\"" + 
		             "              INNER JOIN " + er + " AS ER ON ER.\"OID_EXPEDIENTE\" = EX.\"OID\" AND ER.\"CODIGO_ADUANERO\" = PR.\"CODIGO_ADUANERO\" AND ER.\"ESTADO\" NOT IN " + estado +  
		             "              LEFT JOIN " + cr + " AS CR ON CR.\"OID_EXPEDIENTE\" = EX.\"OID\" AND CR.\"OID_EXPEDIENTE_REA\" = ER.\"OID\"" + 
		             "              WHERE PR.\"AYUDA_KILO\" * \"KILOS_INICIALES\" > COALESCE(CR.\"CANTIDAD\", 0))" +    
                    "       AS T7 ON TRUE";

            query += " GROUP BY \"TOTAL_FACTURADO\", \"TOTAL_COBROS\", \"TOTAL_EFECTOS_NEGOCIADOS\", \"TOTAL_VENCIDO\", \"TOTAL_NO_COBRADO\", \"GASTOS_COBRO\", \"CREDITO_DISPUESTO\"";

            return query;
        }
		
		public static string INNER_PARTNER(QueryConditions conditions)
		{
			Assembly assembly = Assembly.Load("moleQule.Library.Partner");

			//string pn = nHManager.Instance.GetSQLTable(assembly.GetType("moleQule.Library.Partner.Partner"));
			string br = nHManager.Instance.GetSQLTable(assembly.GetType("moleQule.Library.Partner.Branch"));
			string bc = nHManager.Instance.GetSQLTable(assembly.GetType("moleQule.Library.Partner.BranchCliente"));

			string query =
			"	INNER JOIN " + bc + " AS BC ON BC.\"OID_CLIENT\" = CL.\"OID\"" +
			"	INNER JOIN " + br + " AS BR ON BR.\"OID\" = BC.\"OID_BRANCH\"";

			return query;
		}

		public static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query += @"
				WHERE CL.""ESTADO"" != " + (long)EEstado.Baja;

			if (conditions.Cliente != null) 
				query += @"
					AND CL.""OID"" = " + conditions.Cliente.Oid;

			if (AppContext.User.IsPartner)
				query += EntityBase.GET_IN_BRANCHES_LIST_CONDITION(AppContext.Principal.Branches, "BC");

			return query + " " + conditions.ExtraWhere;
		}

        public static string SELECT(ETipoCobro tipo)
        {
            QueryConditions conditions = new QueryConditions() { TipoCobro = tipo };

            return SELECT(conditions);
        }

		public static string SELECT(QueryConditions conditions)
		{
			string tabla;
			string query = string.Empty;

            switch (conditions.TipoCobro)
            { 
                case ETipoCobro.Fomento:
                    {
                        tabla = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
						query = 
							SELECT_FIELDS(ETipoQuery.FOMENTO) + @"
							FROM " + tabla + " AS CB" +
							INNER_COBROS_FOMENTO(conditions);
                    } 
					break;

                case ETipoCobro.REA:
                    {
                        tabla = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
                        query =
                            SELECT_FIELDS(ETipoQuery.REA) + @"
							FROM " + tabla + " AS CB" +
                            INNER_COBROS_REA(conditions);
                    } 
					break;

                default:
                    {
                        tabla = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
                        query =
                            SELECT_FIELDS(ETipoQuery.GENERAL) + @"
							FROM " + tabla + @" AS CL" +
                            INNER_COBROS(conditions);

                        if (AppContext.User.IsPartner)
                            query += INNER_PARTNER(conditions);

                        query += WHERE(conditions);

						query += @"
							ORDER BY ""GASTOS_DEMORA"" DESC";
                    }
					break;
            }

			return query;
		}

		public static string SELECT_PENDIENTES(QueryConditions conditions)
		{
			conditions.ExtraWhere = @"
				AND (""TOTAL_FACTURADO"" - COALESCE(""TOTAL_COBROS"",0) - COALESCE(""TOTAL_EFECTOS_NEGOCIADOS"",0) - COALESCE(""TOTAL_VENCIDO"",0) - COALESCE(""TOTAL_NO_COBRADO"",0) - COALESCE(""TOTAL_DUDOSO_COBRO"",0)) > 0"; 

			string query = SELECT(conditions);

			return query;
		}

		public static string SELECT_DUDOSO_COBRO(QueryConditions conditions)
		{
			conditions.ExtraWhere = @"
				AND COALESCE(""TOTAL_DUDOSO_COBRO"",0) > 0";

			string query = SELECT(conditions);	

			return query;
		}

		public static string SELECT_BY_CLIENTE(long oidCliente)
		{
			string query;

			QueryConditions conditions = new QueryConditions { Cliente = ClienteInfo.New(oidCliente) };
			query = SELECT(conditions);

			return query;
		}

		#endregion
	}
}
