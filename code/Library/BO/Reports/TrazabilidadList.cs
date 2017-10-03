using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{	
	/// <summary>
	/// ReadOnly Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class TrazabilidadList : ReadOnlyListBaseEx<TrazabilidadList, TrazabilidadInfo>
	{	

		#region Business Methods
			
		#endregion
		 
		#region Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private TrazabilidadList() {}
		
        public static TrazabilidadList GetList( ProductInfo producto,
                                                ProveedorInfo proveedor,
                                                DateTime fc_ini, DateTime fc_fin,
                                                DateTime fv_ini, DateTime fv_fin)
        {
            CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

            //No criteria. Retrieve all de List

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = TrazabilidadList.SELECT(producto, proveedor, fc_ini, fc_fin, fv_ini, fv_fin);

            TrazabilidadList list = DataPortal.Fetch<TrazabilidadList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }

        public static TrazabilidadList GetCabezasList(ProductInfo producto,
                                                        ProveedorInfo proveedor,
                                                        LivestockBookLineInfo bookLine,
                                                        DateTime fc_ini, DateTime fc_fin,
                                                        DateTime fv_ini, DateTime fv_fin)
        {
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

            criteria.Query = TrazabilidadList.SELECT_CABEZA(producto, proveedor, bookLine, fc_ini, fc_fin, fv_ini, fv_fin);

            TrazabilidadList list = DataPortal.Fetch<TrazabilidadList>(criteria);
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
                        this.AddItem(TrazabilidadInfo.Get(reader));
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

        /// <summary>
        /// Construye el SELECT para traer la trazabilidad de un producto
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static string SELECT (ProductInfo producto,
                                     ProveedorInfo proveedor,
                                     DateTime fc_ini, DateTime fc_fin,
                                     DateTime fv_ini, DateTime fv_fin)
        {
            string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
            string pv = nHManager.Instance.GetSQLTable(typeof(SupplierRecord));
            string pe = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string idl = nHManager.Instance.GetSQLTable(typeof(InputDeliveryLineRecord));
			string id = nHManager.Instance.GetSQLTable(typeof(InputDeliveryRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
			string nv = nHManager.Instance.GetSQLTable(typeof(ShippingCompanyRecord));
            string tr = nHManager.Instance.GetSQLTable(typeof(TransporterRecord));
            string cfc = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
            string fc = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
            string se = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
            string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));

            string query;

            // PRODUCTOS VENDIDOS
            query = "SELECT 0 AS \"TIPO_CONSULTA\"" +
                    "       ,P.*" +
                    "       ,E.\"CODIGO\" AS \"NEXPEDIENTE\"" +
					"       ,COALESCE(AP.\"FECHA\", PE.\"FECHA_COMPRA\") AS \"FECHA_COMPRA\"" +
                    //"       ,PE.\"FECHA_COMPRA\" AS \"FECHA_COMPRA\"" +
                    "       ,PE.\"OID\" AS \"OID_BATCH\"" +
                    "       ,N.\"NOMBRE\" AS \"NAVIERA\"" +
                    "       ,O.\"NOMBRE\" AS \"TRANS_ORIGEN\"" +
                    "       ,D.\"NOMBRE\" AS \"TRANS_DESTINO\"" +
                    "       ,PR.\"OID\" AS \"OID_PROVEEDOR\"" +
					"		,PR.\"NOMBRE\" AS \"PROVEEDOR\"" +
                    "       ,CF.\"OID\" AS \"OID_CONCEPTO\"" +
					"		,CF.\"CANTIDAD\" AS \"KILOS_VENDIDOS\"" +
                    "       ,F.\"FECHA\" AS \"FECHA_VENTA\"" +
                    "       ,S.\"IDENTIFICADOR\" AS \"ID_SERIE\"" +
					"		,S.\"NOMBRE\" AS \"SERIE\"" +
                    "       ,C.\"OID\" AS \"OID_CLIENTE\"" +
					"		,C.\"NOMBRE\" AS \"CLIENTE\"" +
                    " FROM " + pr + " AS P " +
                    " INNER JOIN " + pe + " AS PE ON PE.\"OID_PRODUCTO\" = P.\"OID\"" +
					" LEFT JOIN " + idl + " AS CAP ON CAP.\"OID_BATCH\" = PE.\"OID\"" +
					" LEFT JOIN " + id + " AS AP ON AP.\"OID\" = CAP.\"OID_ALBARAN\"" +
                    " INNER JOIN " + ex + " AS E ON E.\"OID\" = PE.\"OID_EXPEDIENTE\"" +
                    " LEFT JOIN " + nv + " AS N ON N.\"OID\" = E.\"OID_NAVIERA\"" +
                    " LEFT JOIN " + tr + " AS O ON O.\"OID\" = E.\"OID_TRANS_ORIGEN\"" +
                    " LEFT JOIN " + tr + " AS D ON D.\"OID\" = E.\"OID_TRANS_DESTINO\"" +
                    " INNER JOIN " + pv + " AS PR ON PR.\"OID\" = PE.\"OID_PROVEEDOR\"" +
                    " INNER JOIN " + cfc + " AS CF ON CF.\"OID_BATCH\" = PE.\"OID\"" +
                    " INNER JOIN " + fc + " AS F ON F.\"OID\" = CF.\"OID_FACTURA\"" +
                    " INNER JOIN " + se + " AS S ON F.\"OID_SERIE\" = S.\"OID\"" +
                    " INNER JOIN " + cl + " AS C ON C.\"OID\" = F.\"OID_CLIENTE\"" +
                    " WHERE (1 = 1)";

            if (producto != null)
                query += " AND P.\"OID\" = " + producto.Oid;

            if (proveedor != null)
                query += " AND PR.\"OID\" = " + proveedor.Oid;

            query += " AND \"FECHA_COMPRA\" >= '" + fc_ini.ToString("MM/dd/yyyy") + "' AND \"FECHA_COMPRA\" <= '" + fc_fin.ToString("MM/dd/yyyy") + "'";
            query += " AND F.\"FECHA\" >= '" + fv_ini.ToString("MM/dd/yyyy") + "' AND F.\"FECHA\" <= '" + fv_fin.ToString("MM/dd/yyyy") + "'";

            query += " ORDER BY P.\"NOMBRE\", PR.\"NOMBRE\", E.\"CODIGO\"";

            return query;
        }

        /// <summary>
        /// Construye el SELECT para traer la trazabilidad de las cabezas de ganado
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static string SELECT_CABEZA(ProductInfo product,
                                            ProveedorInfo supplier,
                                            LivestockBookLineInfo bookLine,
                                            DateTime fc_ini, DateTime fc_fin,
                                            DateTime fv_ini, DateTime fv_fin)
        {
            string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
            string su = nHManager.Instance.GetSQLTable(typeof(SupplierRecord));
            string pe = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string idl = nHManager.Instance.GetSQLTable(typeof(InputDeliveryLineRecord));
			string id = nHManager.Instance.GetSQLTable(typeof(InputDeliveryRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
            string lbl = nHManager.Instance.GetSQLTable(typeof(LivestockBookLineRecord));
			string nv = nHManager.Instance.GetSQLTable(typeof(ShippingCompanyRecord));
            string tr = nHManager.Instance.GetSQLTable(typeof(TransporterRecord));
            string cfc = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceLineRecord));
            string fc = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
            string se = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
            string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));

            string query;

            // PRODUCTOS VENDIDOS
            query = "SELECT 1 AS \"TIPO_CONSULTA\"" +
                    "       ,P.*" +
                    "       ,E.\"CODIGO\" AS \"NEXPEDIENTE\"" +
					"       ,COALESCE(AP.\"FECHA\", BA.\"FECHA_COMPRA\") AS \"FECHA_COMPRA\"" +
                    //"       ,PE.\"FECHA_COMPRA\" AS \"FECHA_COMPRA\"" +
                    "       ,BA.\"OID\" AS \"OID_BATCH\"" +
                    "       ,N.\"NOMBRE\" AS \"NAVIERA\"" +
                    "       ,O.\"NOMBRE\" AS \"TRANS_ORIGEN\"" +
                    "       ,D.\"NOMBRE\" AS \"TRANS_DESTINO\"" +
                    "       ,PR.\"OID\" AS \"OID_PROVEEDOR\"" +
					"		,PR.\"NOMBRE\" AS \"PROVEEDOR\"" +
                    "       ,CF.\"OID\" AS \"OID_CONCEPTO\"" +
					"		,CF.\"CANTIDAD\" AS \"KILOS_VENDIDOS\"" +
                    "       ,F.\"FECHA\" AS \"FECHA_VENTA\"" +
                    "       ,S.\"IDENTIFICADOR\" AS \"ID_SERIE\"" +
					"		,S.\"NOMBRE\" AS \"SERIE\"" +
                    "       ,C.\"OID\" AS \"OID_CLIENTE\"" +
					"		,C.\"NOMBRE\" AS \"CLIENTE\"" +
                    "       ,LL.\"IDENTIFICADOR\" AS \"ID\"" +
                    "       ,LL.\"RAZA\" AS \"RAZA\"" +
                    "       ,LL.\"SEXO\" AS \"SEXO\"" +
                    "       ,E.\"FECHA_LLEGADA_MUELLE\" AS \"FECHA_LLEGADA_MUELLE\"" +
                    "       ,C.\"CODIGO_EXPLOTACION\" AS \"CODIGO_EXPLOTACION\"" +
                    " FROM " + pr + " AS P " +
                    " INNER JOIN " + pe + " AS BA ON BA.\"OID_PRODUCTO\" = P.\"OID\"" +
					" LEFT JOIN " + idl + " AS CAP ON CAP.\"OID_BATCH\" = BA.\"OID\"" +
					" LEFT JOIN " + id + " AS AP ON AP.\"OID\" = CAP.\"OID_ALBARAN\"" +
                    " INNER JOIN " + ex + " AS E ON E.\"OID\" = BA.\"OID_EXPEDIENTE\"" +
                    " INNER JOIN " + lbl + " AS LL ON LL.\"OID_BATCH\" = BA.\"OID\"" +
                    " LEFT JOIN " + nv + " AS N ON N.\"OID\" = E.\"OID_NAVIERA\"" +
                    " LEFT JOIN " + tr + " AS O ON O.\"OID\" = E.\"OID_TRANS_ORIGEN\"" +
                    " LEFT JOIN " + tr + " AS D ON D.\"OID\" = E.\"OID_TRANS_DESTINO\"" +
                    " INNER JOIN " + su + " AS PR ON PR.\"OID\" = BA.\"OID_PROVEEDOR\"" +
                    " INNER JOIN " + cfc + " AS CF ON CF.\"OID_BATCH\" = BA.\"OID\"" +
                    " INNER JOIN " + fc + " AS F ON F.\"OID\" = CF.\"OID_FACTURA\"" +
                    " INNER JOIN " + se + " AS S ON F.\"OID_SERIE\" = S.\"OID\"" +
                    " INNER JOIN " + cl + " AS C ON C.\"OID\" = F.\"OID_CLIENTE\"" +
                    " WHERE (1 = 1)";

            if (product != null)
                query += " AND P.\"OID\" = " + product.Oid;

            if (supplier != null)
                query += " AND PR.\"OID\" = " + supplier.Oid;

            if (bookLine != null)
                query += " AND LL.\"OID\" = " + bookLine.Oid;

            query += " AND \"FECHA_COMPRA\" >= '" + fc_ini.ToString("MM/dd/yyyy") + "' AND \"FECHA_COMPRA\" <= '" + fc_fin.ToString("MM/dd/yyyy") + "'";
            query += " AND F.\"FECHA\" >= '" + fv_ini.ToString("MM/dd/yyyy") + "' AND F.\"FECHA\" <= '" + fv_fin.ToString("MM/dd/yyyy") + "'";

            query += " ORDER BY P.\"NOMBRE\", PR.\"NOMBRE\", E.\"CODIGO\"";

            return query;
        }

        #endregion
    }
}

