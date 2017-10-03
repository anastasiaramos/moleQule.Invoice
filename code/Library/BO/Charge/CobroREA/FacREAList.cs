using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// Read Only Root Collection of Business Objects With Child Collection
	/// </summary>
    [Serializable()]
    public class FacREAList : ReadOnlyListBaseEx<FacREAList, FacREAInfo>, IBindingList
    {
        #region Business Methods

        public new FacREAList GetSubList(FCriteria criteria)
        {
            List<FacREAInfo> list = base.GetSubList(criteria);
            FacREAList subList = new FacREAList();

            subList.IsReadOnly = false;

            foreach (FacREAInfo item in list)
                subList.AddItem(item);

            subList.IsReadOnly = true;

            return subList;
        }

		public void UpdateCobroValues(ChargeInfo cobro)
		{
			FacREAInfo item;
			decimal acumulado;
			CobroREAInfo cobroFactura;

			for (int i = 0; i < Items.Count; i++)
			{
				item = Items[i];

				cobroFactura = cobro.CobroREAs.GetItemByExpediente(item.OidExpediente, item.OidExpedienteREA);

				if (cobroFactura != null)
				{
					item.FechaAsignacion = cobroFactura.FechaAsignacion.ToShortDateString();
					item.Asignado = cobroFactura.Cantidad;
				}
				else
					item.FechaAsignacion = (item.Asignado != 0) ? DateTime.Now.ToShortDateString() : DateTime.MinValue.ToShortDateString();

				if (i == 0) acumulado = 0;
				else acumulado = Items[i - 1].Acumulado;

				item.Acumulado = acumulado + item.Pendiente;
				item.Vinculado = (item.Asignado == 0) ? Resources.Labels.SET_COBRO : Resources.Labels.RESET_COBRO;
			}
		}

		public decimal TotalPendiente()
		{
			decimal pendiente = 0;

			foreach (FacREAInfo item in this)
				pendiente += item.Pendiente;

			return pendiente;
		}

        #endregion

        #region Root Factory Methods

        public FacREAList() 
        {
            AllowEdit = true;
        }

        public static FacREAList GetListByCobro(long oid_cobro)
        {
			CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
            criteria.Childs = false;

            //No criteria. Retrieve all de List
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = FacREAList.SELECT_BY_COBRO(oid_cobro);

            FacREAList list = DataPortal.Fetch<FacREAList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static FacREAList GetListByCobroAndPendientes(long oid_cobro)
        {
			CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
            criteria.Childs = false;

            //No criteria. Retrieve all de List
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = FacREAList.SELECT_BY_COBRO_AND_PENDIENTES(oid_cobro);

            FacREAList list = DataPortal.Fetch<FacREAList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static FacREAList GetNoCobradas()
        {
			CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = FacREAList.SELECT_NO_COBRADAS();

            FacREAList list = DataPortal.Fetch<FacREAList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		public static FacREAList GetList(IList<FacREAInfo> list)
		{
			FacREAList flist = new FacREAList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (FacREAInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}

		#endregion
		
        #region Root Data Access

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
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

                    while (reader.Read())
                    {
                        //Para evitar duplicados porque la consulta es demasiado compleja para
                        //hacerlo ahí
                        if (this.Contains(Format.DataReader.GetInt64(reader, "OID"))) continue;
                        this.AddItem(FacREAInfo.GetChild(reader));
                    }

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

		public static string SELECT_FIELDS_COBRADAS()
		{
			string query;

			query = "SELECT ER.*" +
					"		,EX.\"OID\" AS \"OID_EXPEDIENTE\"" +
					"		,EX.\"CODIGO\" AS \"CODIGO_EXPEDIENTE\"" +
					"		,EX.\"TIPO_EXPEDIENTE\" AS \"TIPO_EXPEDIENTE\"" +
					"       ,PT.\"AYUDA_ESTIMADA\"" +
					"       ,CR.\"COBRADO\"" +
					"       ,CR1.\"CANTIDAD\" AS \"ASIGNADO\"" +
					"		,CR1.\"FECHA_ASIGNACION\"" +
					"       ,C.\"FECHA\" AS \"FECHA_COBRO\"";

			return query;
		}

		public static string SELECT_FIELDS_PARCIAL_PENDIENTE()
		{
			string query;

			query = "SELECT ER.*" +
					"		,EX.\"OID\" AS \"OID_EXPEDIENTE\"" +
					"		,EX.\"CODIGO\" AS \"CODIGO_EXPEDIENTE\"" +
					"		,EX.\"TIPO_EXPEDIENTE\" AS \"TIPO_EXPEDIENTE\"" +
					"       ,PT.\"AYUDA_ESTIMADA\"" +
					"       ,CR.\"COBRADO\"" +
					"       ,0 AS \"ASIGNADO\"" +
					"		,NULL AS \"FECHA_ASIGNACION\"" +
					"       ,NULL AS \"FECHA_COBRO\"";

			return query;
		}

		public static string SELECT_FIELDS_PENDIENTE()
		{
			string query;

			query = "SELECT ER.*" +
					"		,EX.\"OID\" AS \"OID_EXPEDIENTE\"" +
					"		,EX.\"CODIGO\" AS \"CODIGO_EXPEDIENTE\"" +
					"		,EX.\"TIPO_EXPEDIENTE\" AS \"TIPO_EXPEDIENTE\"" +
					"       ,PT.\"AYUDA_ESTIMADA\"" +
					"       ,0 AS \"COBRADO\"" +
					"       ,0 AS \"ASIGNADO\"" +
					"		,NULL AS \"FECHA_ASIGNACION\"" +
					"       ,NULL AS \"FECHA_COBRO\"";

			return query;
		}

        public static string SELECT_BY_COBRO(long oid_cobro)
        {
            string er = nHManager.Instance.GetSQLTable(typeof(REAExpedientRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
            string cr = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));
            string pt = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
            string c = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));

            string query = string.Empty;

			query = SELECT_FIELDS_COBRADAS() +
                    " FROM " + er + " AS ER" +
					" INNER JOIN " + ex + " AS EX ON EX.\"OID\" = ER.\"OID_EXPEDIENTE\"" +
                    " INNER JOIN (SELECT PT1.\"OID_EXPEDIENTE\"" +
 					"					,PR.\"CODIGO_ADUANERO\"" +
					"					,CAST(SUM(PR.\"AYUDA_KILO\" * PT1.\"KILOS_INICIALES\") AS NUMERIC(10,2)) AS \"AYUDA_ESTIMADA\"" +
                    "             FROM " + pt + " AS PT1" +
					"			  INNER JOIN " + pr + " AS PR ON PR.\"OID\" = PT1.\"OID_PRODUCTO\"" +
                    "             WHERE PT1.\"AYUDA\" = TRUE" +
					"			  GROUP BY PT1.\"OID_EXPEDIENTE\", PR.\"CODIGO_ADUANERO\")" +
					"		AS PT ON PT.\"OID_EXPEDIENTE\" = ER.\"OID_EXPEDIENTE\" AND PT.\"CODIGO_ADUANERO\" = ER.\"CODIGO_ADUANERO\"" +
                    " INNER JOIN (SELECT CR.\"OID_EXPEDIENTE_REA\", SUM(CR.\"CANTIDAD\") AS \"COBRADO\"" +
                    "             FROM " + cr + " AS CR" +
                    "             INNER JOIN " + c + " AS CB ON CB.\"OID\" = CR.\"OID_COBRO\" AND CB.\"TIPO_COBRO\" = " + (long)ETipoCobro.REA +
                    "			  GROUP BY CR.\"OID_EXPEDIENTE_REA\")" +
					"		AS CR ON CR.\"OID_EXPEDIENTE_REA\" = ER.\"OID\"" +
					" INNER JOIN " + cr + " AS CR1 ON CR1.\"OID_EXPEDIENTE_REA\" = CR.\"OID_EXPEDIENTE_REA\"" +
                    " INNER JOIN " + c + " AS C ON C.\"OID\" = CR1.\"OID_COBRO\"" +
                    " WHERE CR1.\"OID_COBRO\" = " + oid_cobro;

			query += " ORDER BY \"CODIGO_EXPEDIENTE\"";

            return query;
        }

        public static string SELECT_NO_COBRADAS()
        {
			string er = nHManager.Instance.GetSQLTable(typeof(REAExpedientRecord));
            string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
            string cr = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));
            string pt = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
            string ch = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));

			string query = string.Empty;

			string estado_pendiente = @"
                NOT IN (" + (long)EEstado.Charged + @"
				        ," + (long)EEstado.Desestimado + @"
						," + (long)EEstado.Anulado + @"
						)";

            query = 
            SELECT_FIELDS_PARCIAL_PENDIENTE() + @"
            FROM " + er + @" AS ER
            INNER JOIN " + ex + @" AS EX ON EX.""OID"" = ER.""OID_EXPEDIENTE""
            INNER JOIN (SELECT PT1.""OID_EXPEDIENTE""
                                ,PR.""CODIGO_ADUANERO""
                                ,CAST(SUM(PR.""AYUDA_KILO"" * PT1.""KILOS_INICIALES"") AS NUMERIC(10,2)) AS ""AYUDA_ESTIMADA""
                        FROM " + pt + @" AS PT1
                        INNER JOIN " + pr + @" AS PR ON PR.""OID"" = PT1.""OID_PRODUCTO""
                        WHERE PT1.""AYUDA"" = TRUE
                        GROUP BY PT1.""OID_EXPEDIENTE"", PR.""CODIGO_ADUANERO"")
                AS PT ON PT.""OID_EXPEDIENTE"" = ER.""OID_EXPEDIENTE"" AND PT.""CODIGO_ADUANERO"" = ER.""CODIGO_ADUANERO""
            INNER JOIN (SELECT CR.""OID_EXPEDIENTE_REA""
                                ,SUM(CR.""CANTIDAD"") AS ""COBRADO""
                        FROM " + cr + @" AS CR
                        INNER JOIN " + ch + @" AS CH ON CH.""OID"" = CR.""OID_COBRO"" AND CH.""TIPO_COBRO"" = " + (long)ETipoCobro.REA + @"
                        WHERE CH.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        GROUP BY CR.""OID_EXPEDIENTE_REA"")
                AS CR ON CR.""OID_EXPEDIENTE_REA"" = ER.""OID""
            WHERE ((CR.""COBRADO"" < PT.""AYUDA_ESTIMADA"" AND CR.""COBRADO"" > 0) OR (CR.""COBRADO"" = 0))
                AND PT.""AYUDA_ESTIMADA"" > 0
                AND EX.""AYUDA"" = TRUE
                AND ER.""ESTADO""" + estado_pendiente + @"
            UNION " +
            SELECT_FIELDS_PENDIENTE() + @"
            FROM " + er + @" AS ER
            INNER JOIN " + ex + @" AS EX ON EX.""OID"" = ER.""OID_EXPEDIENTE""
            INNER JOIN (SELECT PT1.""OID_EXPEDIENTE""
                                ,PR.""CODIGO_ADUANERO""
                                ,CAST(SUM(PR.""AYUDA_KILO"" * PT1.""KILOS_INICIALES"") AS NUMERIC(10,2)) AS ""AYUDA_ESTIMADA""
                        FROM " + pt + @" AS PT1
                        INNER JOIN " + pr + @" AS PR ON PR.""OID"" = PT1.""OID_PRODUCTO""
                        GROUP BY PT1.""OID_EXPEDIENTE"", PR.""CODIGO_ADUANERO"")
                AS PT ON PT.""OID_EXPEDIENTE"" = ER.""OID_EXPEDIENTE"" AND PT.""CODIGO_ADUANERO"" = ER.""CODIGO_ADUANERO""
            WHERE PT.""AYUDA_ESTIMADA"" > 0
                AND EX.""AYUDA"" = TRUE
                AND ER.""ESTADO""" + estado_pendiente + @"
                AND ER.""OID"" NOT IN (SELECT ""OID_EXPEDIENTE_REA""
                                        FROM " + cr + @" AS CR
                                        INNER JOIN " + ch + @" AS CH ON CH.""OID"" = CR.""OID_COBRO"" AND CH.""TIPO_COBRO"" = " + (long)ETipoCobro.REA + @"
                                        WHERE ER.""OID"" = CR.""OID_EXPEDIENTE_REA""
                                            AND CH.""ESTADO"" != " + (long)EEstado.Anulado + ")";

			query += @"
            ORDER BY ""CODIGO_EXPEDIENTE""";

            return query;
        }

        public static string SELECT_BY_COBRO_AND_PENDIENTES(long oidCharge)
        {
			string er = nHManager.Instance.GetSQLTable(typeof(REAExpedientRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
			string cr = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));
			string pt = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
			string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string c = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));

            string query = string.Empty;

			string estado_pendiente = " NOT IN (" + (long)EEstado.Charged
										+ "," + (long)EEstado.Desestimado
										+ "," + (long)EEstado.Anulado
										+ ")";

            query = SELECT_FIELDS_COBRADAS() +
                    " FROM " + er + " AS ER" +
                    " INNER JOIN " + ex + " AS EX ON EX.\"OID\" = ER.\"OID_EXPEDIENTE\"" +
                    " INNER JOIN (SELECT PT1.\"OID_EXPEDIENTE\"" +
                    "					,PR.\"CODIGO_ADUANERO\"" +
                    "					,CAST(SUM(PR.\"AYUDA_KILO\" * PT1.\"KILOS_INICIALES\") AS NUMERIC(10,2)) AS \"AYUDA_ESTIMADA\"" +
                    "             FROM " + pt + " AS PT1" +
                    "			  INNER JOIN " + pr + " AS PR ON PR.\"OID\" = PT1.\"OID_PRODUCTO\"" +
                    "             WHERE PT1.\"AYUDA\" = TRUE" +
                    "			  GROUP BY PT1.\"OID_EXPEDIENTE\", PR.\"CODIGO_ADUANERO\")" +
                    "		AS PT ON PT.\"OID_EXPEDIENTE\" = ER.\"OID_EXPEDIENTE\" AND PT.\"CODIGO_ADUANERO\" = ER.\"CODIGO_ADUANERO\"" +
                    " INNER JOIN (SELECT CR.\"OID_EXPEDIENTE_REA\"" +
                    "					,SUM(CR.\"CANTIDAD\") AS \"COBRADO\"" +
                    "               FROM " + cr + " AS CR" +
                    "               INNER JOIN " + c + " AS CB ON CB.\"OID\" = CR.\"OID_COBRO\" AND CB.\"TIPO_COBRO\" = " + (long)ETipoCobro.REA +
                    "               WHERE CB.\"ESTADO\" != " + (long)EEstado.Anulado + 
                    "			  GROUP BY CR.\"OID_EXPEDIENTE_REA\")" +
                    "		AS CR ON CR.\"OID_EXPEDIENTE_REA\" = ER.\"OID\"" +
                    " INNER JOIN " + cr + " AS CR1 ON CR1.\"OID_EXPEDIENTE_REA\" = CR.\"OID_EXPEDIENTE_REA\"" +
                    " INNER JOIN " + c + " AS C ON C.\"OID\" = CR1.\"OID_COBRO\"";
            if (oidCharge > 0) query += " WHERE CR1.\"OID_COBRO\" = " + oidCharge;
            query +=
                  " UNION " +
                  SELECT_FIELDS_PARCIAL_PENDIENTE() +
                  " FROM " + er + " AS ER" +
                  " INNER JOIN " + ex + " AS EX ON EX.\"OID\" = ER.\"OID_EXPEDIENTE\"" +
                  " INNER JOIN (SELECT PT1.\"OID_EXPEDIENTE\"" +
                  "					,PR.\"CODIGO_ADUANERO\"" +
                  "					,CAST(SUM(PR.\"AYUDA_KILO\" * PT1.\"KILOS_INICIALES\") AS NUMERIC(10,2)) AS \"AYUDA_ESTIMADA\"" +
                  "             FROM " + pt + " AS PT1" +
                  "			  INNER JOIN " + pr + " AS PR ON PR.\"OID\" = PT1.\"OID_PRODUCTO\"" +
                  "             WHERE PT1.\"AYUDA\" = TRUE" +
                  "			  GROUP BY PT1.\"OID_EXPEDIENTE\", PR.\"CODIGO_ADUANERO\")" +
                  "		AS PT ON PT.\"OID_EXPEDIENTE\" = ER.\"OID_EXPEDIENTE\" AND PT.\"CODIGO_ADUANERO\" = ER.\"CODIGO_ADUANERO\"" +
                  " INNER JOIN (SELECT CR.\"OID_EXPEDIENTE_REA\", SUM(CR.\"CANTIDAD\") AS \"COBRADO\"" +
                  "             FROM " + cr + " AS CR" +
                  "             INNER JOIN " + c + " AS CB ON CB.\"OID\" = CR.\"OID_COBRO\" AND CB.\"TIPO_COBRO\" = " + (long)ETipoCobro.REA +
                  "             WHERE CR.\"OID_COBRO\" != " + oidCharge +
                  "               AND CB.\"ESTADO\" != " + (long)EEstado.Anulado +
                  "             GROUP BY CR.\"OID\", CR.\"OID_EXPEDIENTE_REA\")" +
                  "		AS CR ON CR.\"OID_EXPEDIENTE_REA\" = ER.\"OID\"" +
                  " INNER JOIN " + cr + " AS CR1 ON CR1.\"OID_EXPEDIENTE_REA\" = CR.\"OID_EXPEDIENTE_REA\"" +
                  " WHERE CR1.\"OID_COBRO\" != " + oidCharge +
                  " AND PT.\"AYUDA_ESTIMADA\" > 0" +
                  " AND CR.\"COBRADO\" < PT.\"AYUDA_ESTIMADA\"" +
                  " AND ER.\"ESTADO\"" + estado_pendiente +
                  " UNION " +
                  SELECT_FIELDS_PENDIENTE() +
                  " FROM " + er + " AS ER" +
                  " INNER JOIN " + ex + " AS EX ON EX.\"OID\" = ER.\"OID_EXPEDIENTE\"" +
                  " LEFT JOIN (SELECT \"OID_EXPEDIENTE\"" +
                  "					,PR.\"CODIGO_ADUANERO\"" +
                  "					,CAST(SUM(PR.\"AYUDA_KILO\" * PT1.\"KILOS_INICIALES\") AS NUMERIC(10,2)) AS \"AYUDA_ESTIMADA\"" +
                  "             FROM " + pt + " AS PT1" +
                  "			  INNER JOIN " + pr + " AS PR ON PR.\"OID\" = PT1.\"OID_PRODUCTO\"" +
                  "             WHERE PT1.\"AYUDA\" = TRUE" +
                  "			  GROUP BY PT1.\"OID_EXPEDIENTE\", PR.\"CODIGO_ADUANERO\")" +
                  "		AS PT ON PT.\"OID_EXPEDIENTE\" = ER.\"OID_EXPEDIENTE\" AND PT.\"CODIGO_ADUANERO\" = ER.\"CODIGO_ADUANERO\"" +
                  " WHERE PT.\"AYUDA_ESTIMADA\" > 0" +
                  " AND ER.\"ESTADO\"" + estado_pendiente +
                  " AND ER.\"OID\" NOT IN (SELECT \"OID_EXPEDIENTE_REA\"" +
                  "                        FROM " + cr + " AS CR" +
                  "                        INNER JOIN " + c + " AS CB ON CB.\"OID\" = CR.\"OID_COBRO\" AND CB.\"TIPO_COBRO\" = " + (long)ETipoCobro.REA +
                  "                        WHERE ER.\"OID\" = CR.\"OID_EXPEDIENTE_REA\"" +
                  "                           AND CB.\"ESTADO\" != " + (long)EEstado.Anulado + ")";

			query += " ORDER BY \"CODIGO_EXPEDIENTE\"";

            return query;
        }

        #endregion		
    }
}