using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// ReadOnly Root Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class InventarioValoradoList : ReadOnlyListBaseEx<InventarioValoradoList, InventarioValoradoInfo>
    {
        #region Business Methods

        #endregion

        #region Factory Methods

        private InventarioValoradoList() { }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns></returns>
        public static InventarioValoradoList GetList(   ProductInfo producto,
                                                        ETipoExpediente tipo,
                                                        ExpedientInfo expediente,
                                                        DateTime fecha)
        {
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

            criteria.Query = InventarioValoradoList.SELECT(producto, tipo, expediente, fecha);

            InventarioValoradoList list = DataPortal.Fetch<InventarioValoradoList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns></returns>
        public static InventarioValoradoList GetListStock(  ETipoExpediente tipo,
                                                            ExpedientInfo expediente,
                                                            DateTime fecha)
        {
            CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

            criteria.Query = InventarioValoradoList.SELECT_WITH_STOCK(tipo, expediente, fecha);

            InventarioValoradoList list = DataPortal.Fetch<InventarioValoradoList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Data Access

        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

                    while (reader.Read())
                    {
                        this.AddItem(InventarioValoradoInfo.Get(reader));
                    }
                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        public static string SELECT_FIELDS()
        {
            string query;

            //LOS PRODUCTOS QUE HAN SIDO INCLUIDOS EN ALGUN EXPEDIENTE
            query = @"
            SELECT PA.""OID"" AS ""OID_PRODUCTO_PROVEEDOR""
                    ,AL.""CODIGO"" AS ""ID_ALMACEN""
                    ,AL.""NOMBRE"" AS ""ALMACEN""
                    ,P.""OID"" AS ""OID_PRODUCTO""
                    ,P.""CODIGO_ADUANERO"" AS ""CODIGO_PRODUCTO""
                    ,P.""NOMBRE"" AS ""PRODUCTO""
                    ,COALESCE(E.""CODIGO"", '') AS ""EXPEDIENTE""
                    ,COALESCE(E.""CONTENEDOR"", '') AS ""CONTENEDOR""
                    ,COALESCE(E.""TIPO_EXPEDIENTE"", 0) AS ""TIPO_EXPEDIENTE""
                    ,PR.""NOMBRE"" AS ""PROVEEDOR""
                    ,PA.""PRECIO_COMPRA_KILO"" AS ""PCD""
                    ,PA.""PRECIO_VENTA_KILO"" AS ""PVP""
                    ,PA.""PRECIO_COMPRA_KILO"" + PA.""GASTO_KILO""
                        - (CASE WHEN PA.""AYUDA"" THEN 
                                (CASE WHEN PA.""AYUDA_RECIBIDA_KILO"" != 0 THEN 
                                    PA.""AYUDA_RECIBIDA_KILO"" 
                                ELSE COALESCE(P.""AYUDA_KILO"", 0) END) 
                            ELSE 0 END)  AS ""COSTE_KILO""
                    ,COALESCE(""ENTRADA"", 0) AS ""ENTRADA""
                    COALESCE(""SALIDA"", 0) AS ""SALIDA""";             

            return query;
        }
        
        public static string SELECT(ProductInfo producto, 
                                    ETipoExpediente tipo,
                                    ExpedientInfo expediente, 
                                    DateTime fecha)
        {
            string ba = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
            string st = nHManager.Instance.GetSQLTable(typeof(StockRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
            string su = nHManager.Instance.GetSQLTable(typeof(SupplierRecord));
            string sr = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));

            //LOS PRODUCTOS QUE HAN SIDO INCLUIDOS EN ALGUN EXPEDIENTE Y TIENEN SALIDAS DE STOCK
            string query = 
            SELECT_FIELDS() + @"
            FROM " + ba + @" AS PA
            INNER JOIN " + sr + @" AS AL ON AL.""OID"" = PA.""OID_ALMACEN""
            INNER JOIN (SELECT ""OID_BATCH"", SUM(""KILOS"") AS ""ENTRADA""
                        FROM " + st + @"
                        WHERE ""KILOS"" > 0 
                            AND ""FECHA"" <= '" + fecha.ToString("MM/dd/yyyy") + @"'
                        GROUP BY ""OID_BATCH"")
                AS SA ON PA.""OID"" = SA.""OID_BATCH""
            INNER JOIN (SELECT ""OID_BATCH"", SUM(""KILOS"") AS ""SALIDA""
                        FROM " + st + @"
                        WHERE ""KILOS"" < 0 
                            AND ""FECHA"" <= '" + fecha.ToString("MM/dd/yyyy") + @"'
                        GROUP BY ""OID_BATCH"")
                AS SB ON PA.""OID"" = SB.""OID_BATCH""
            INNER JOIN " + ex + @" AS E ON E.""OID"" = PA.""OID_EXPEDIENTE""
            INNER JOIN " + pr + @" AS P ON P.""OID"" = PA.""OID_PRODUCTO""
            INNER JOIN " + su + @" AS PR ON PR.""OID"" = PA.""OID_PROVEEDOR""
            WHERE TRUE";
            
            if (producto != null)
                query += @" 
                AND P.""OID"" = " + producto.Oid;

            if (tipo != ETipoExpediente.Todos)
            {
                query += @"
                AND E.""TIPO_EXPEDIENTE"" = " + (long)tipo;

                if (expediente != null)
                    query += @"
                    AND E.""OID"" = " + expediente.Oid;
            }

            query += @"
            UNION ";

            //LOS PRODUCTOS QUE NO TIENEN SALIDAS DE STOCK
            query += 
            SELECT_FIELDS() + @"
            FROM " + ba + @" AS PA
            INNER JOIN " + sr + @" AS AL ON AL.""OID"" = PA.""OID_ALMACEN""
            INNER JOIN " + ex + @" AS E ON E.""OID"" = PA.""OID_EXPEDIENTE""
            INNER JOIN " + pr + @" AS P ON P.""OID"" = PA.""OID_PRODUCTO""
            INNER JOIN " + su + @" AS PR ON PR.""OID"" = PA.""OID_PROVEEDOR""
            INNER JOIN (SELECT ""OID_BATCH"", SUM(""KILOS"") AS ""ENTRADA"", 0 AS ""SALIDA""
                        FROM " + st + @"
                        WHERE ""KILOS"" > 0 
                            AND ""FECHA"" <= '" + fecha.ToString("MM/dd/yyyy") + @"'
                        GROUP BY ""OID_BATCH"")
                AS SA ON PA.""OID"" = SA.""OID_BATCH""
            WHERE PA.""OID"" NOT IN (SELECT DISTINCT ""OID_BATCH""
                                    FROM " + st + @"
                                    WHERE ""KILOS"" < 0 
                                        AND ""FECHA"" <= '" + fecha.ToString("MM/dd/yyyy") + "')";

            if (producto != null)
                query += @"
                AND P.""OID"" = " + producto.Oid;

            if (tipo != ETipoExpediente.Todos)
            {
                query += @"
                AND E.""TIPO_EXPEDIENTE"" = " + (long)tipo;

                if (expediente != null)
                    query += @"
                    AND E.""OID"" = " + expediente.Oid;
            }

            if (tipo == ETipoExpediente.Todos)
            {
                //LOS PRODUCTOS QUE EXISTEN PERO NO HAN SIDO INCLUIDOS EN NINGUN EXPEDIENTE
                query += @"
                UNION ";

                query += @"
                SELECT P.""OID"" AS ""OID_PRODUCTO_PROVEEDOR""
                        ,AL.""CODIGO"" AS ""ID_ALMACEN""
                        ,AL.""NOMBRE"" AS ""ALMACEN""
                        ,P.""OID"" AS ""OID_PRODUCTO""
                        ,P.""CODIGO_ADUANERO"" AS ""CODIGO_PRODUCTO""
                        ,P.""NOMBRE"" AS ""PRODUCTO""
                        ,'' AS ""EXPEDIENTE""
                        ,'' AS ""CONTENEDOR""
                        ,0 AS ""TIPO_EXPEDIENTE""
                        ,PR.""NOMBRE"" AS ""PROVEEDOR""
                        ,P.""PRECIO_COMPRA"" AS ""PCD""
                        ,P.""PRECIO_VENTA"" AS ""PVP""
                        ,0 AS ""COSTE_KILO""
                        0 AS ""ENTRADA"", 0 AS ""SALIDA""
                FROM " + pr + @"AS P
                LEFT JOIN " + ba + @" AS PA ON P.""OID"" = PA.""OID_PRODUCTO""
                INNER JOIN " + sr + @" AS AL ON AL.""OID"" = PA.""OID_ALMACEN""
                LEFT JOIN " + su + @" AS PR ON PR.""OID"" = PA.""OID_PROVEEDOR""
                WHERE P.""OID"" NOT IN (SELECT ""OID_PRODUCTO"" FROM " + ba + ")";

                if (producto != null)
                    query += @"
                    AND P.""OID"" = " + producto.Oid;
            }

            query += @"
            ORDER BY ""PRODUCTO""";

            return query;
        }

        public static string SELECT_WITH_STOCK( ETipoExpediente tipo,
                                                ExpedientInfo expediente,
                                                DateTime fecha)
        {
            string ba = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
            string st = nHManager.Instance.GetSQLTable(typeof(StockRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
            string su = nHManager.Instance.GetSQLTable(typeof(SupplierRecord));
            string sr = nHManager.Instance.GetSQLTable(typeof(AlmacenRecord));

            //LOS PRODUCTOS QUE HAN SIDO INCLUIDOS EN ALGUN EXPEDIENTE Y TIENEN SALIDAS DE STOCK
            string query = 
            SELECT_FIELDS() + @"
            FROM " + ba + " AS PA " + @"
            INNER JOIN " + sr + @" AS AL ON AL.""OID"" = PA.""OID_ALMACEN""
            LEFT JOIN " + ex + @" AS E ON E.""OID"" = PA.""OID_EXPEDIENTE""
            INNER JOIN " + pr + @" AS P ON P.""OID"" = PA.""OID_PRODUCTO""
            INNER JOIN " + su + @" AS PR ON PR.""OID"" = PA.""OID_PROVEEDOR""
            INNER JOIN (SELECT ""OID_BATCH""
                            ,SUM(""KILOS"") AS ""ENTRADA""
                        FROM " + st + @"
                        WHERE ""KILOS"" > 0 AND ""FECHA"" <= '" + fecha.ToString("MM/dd/yyyy") + @"'
                        GROUP BY ""OID_BATCH"")
                AS SA ON PA.""OID"" = SA.""OID_BATCH""
            LEFT JOIN (SELECT ""OID_BATCH""
                            ,SUM(""KILOS"") AS ""SALIDA"" 
                        FROM " + st + @"
                        WHERE ""KILOS"" < 0 AND ""FECHA"" <= '" + fecha.ToString("MM/dd/yyyy") + @"'
                        GROUP BY ""OID_BATCH"")
                AS SB ON SA.""OID_BATCH"" = SB.""OID_BATCH""
            WHERE (COALESCE(""ENTRADA"", 0) + COALESCE(""SALIDA"", 0) > 0)";

            if (tipo != ETipoExpediente.Todos)
            {
                query += @"
                AND COALESCE(E.""TIPO_EXPEDIENTE"", " + (long)ETipoExpediente.Todos + @") = " + (long)tipo;

                if (expediente != null)
                    query += @"
                        AND COALESCE(E.""OID"", 0) = " + expediente.Oid;
            }

            /*query += " UNION ";

            //LOS PRODUCTOS QUE NO TIENEN SALIDAS DE STOCK
            query += SELECT_FIELDS() +
                    " FROM " + pa + " AS PA " +
                    " INNER JOIN " + te + " AS E ON E.\"OID\" = PA.\"OID_EXPEDIENTE\"" +
                    " INNER JOIN " + tp + " AS P ON P.\"OID\" = PA.\"OID_PRODUCTO\"" +
                    " INNER JOIN " + tpr + " AS PR ON PR.\"OID\" = PA.\"OID_PROVEEDOR\"" +
                    " INNER JOIN (SELECT \"OID_BATCH\", SUM(\"KILOS\") AS \"ENTRADA\", 0 AS \"SALIDA\"" +
                    "               FROM " + ts +
                    "               WHERE \"KILOS\" > 0 AND \"FECHA\" <= '" + fecha.ToString("MM/dd/yyyy") + "'" +
                    "               GROUP BY \"OID_BATCH\")" +
                    "       AS SA ON PA.\"OID\" = SA.\"OID_BATCH\"" +
                    " WHERE PA.\"OID\" NOT IN (SELECT DISTINCT \"OID_BATCH\"" +
			        "                           FROM " + ts +               
			        "                           WHERE \"KILOS\" < 0 AND \"FECHA\" <= '" + fecha.ToString("MM/dd/yyyy") + "')";

            if (tipo != ETipoExpediente.Todos)
            {
                query += " AND E.\"TIPO_EXPEDIENTE\" = " + ((long)tipo).ToString();

                if (expediente != null)
                    query += " AND E.\"OID\" = " + expediente.Oid.ToString();
            }*/

            query += @"
            ORDER BY ""PRODUCTO""";

            return query;
        }

        #endregion
    }
}