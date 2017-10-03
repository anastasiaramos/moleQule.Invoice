using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class REAResumenList : ReadOnlyListBaseEx<REAResumenList, REAResumen>
    {

        #region Factory Methods

        private REAResumenList() { }

        private REAResumenList(IDataReader reader)
        {
            Fetch(reader);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>PResumenList</returns>
        public static REAResumenList GetChildList(bool childs)
        {
            CriteriaEx criteria = Charge.GetCriteria(Charge.OpenSession());
            criteria.Childs = childs;

            //No criteria. Retrieve all de List
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = REAResumenList.SELECT_RESUMEN();

            //else -> No criteria. Retrieve all the list.

            REAResumenList list = DataPortal.Fetch<REAResumenList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Builds a PResumenList from a IList<!--<PResumen>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>PResumenList</returns>
        public static REAResumenList GetChildList(IList<REAResumen> list)
        {
            REAResumenList flist = new REAResumenList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (REAResumen item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        public static REAResumenList GetChildList(IDataReader reader) { return new REAResumenList(reader); }

        /// <summary>
        /// Builds a ExpedienteList from a IList<!--<Expediente>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>ExpedienteList</returns>
        public static REAResumenList GetList(IList<REAResumen> list)
        {
            REAResumenList flist = new REAResumenList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (REAResumen item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
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
                IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                IsReadOnly = false;

                while (reader.Read())
                {
                    this.AddItem(REAResumen.Get(reader));
                }

                IsReadOnly = true;

            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            this.RaiseListChangedEvents = true;
        }

        // called to retrieve data from db
        protected void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IsReadOnly = false;

                while (reader.Read())
                {
                    this.AddItem(REAResumen.Get(reader));
                }

                IsReadOnly = true;

            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        /// <summary>
        /// Construye el SELECT para traer una lista de clientes con informacion general
        /// sobre sus cobros.
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public static string SELECT_RESUMEN()
        {
            string texp = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
            string tcob = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string tpexp = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
            string crea = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));
            string query = string.Empty;

            query = " SELECT ex.*, \"TOTAL_FACTURADO\", \"TOTAL_COBRADO\" " +
                    " FROM " + texp + " as ex " +
                    " LEFT JOIN (SELECT \"OID_EXPEDIENTE\", SUM(\"AYUDA_RECIBIDA_KILO\") * SUM(\"KILOS_INICIALES\") AS \"TOTAL_FACTURADO\" FROM " + tpexp + " GROUP BY \"OID_EXPEDIENTE\") as pe ON pe.\"OID_EXPEDIENTE\" = ex.\"OID\"" +
                    " LEFT JOIN (SELECT \"OID_EXPEDIENTE\", \"OID_COBRO\", SUM(\"CANTIDAD\") AS \"TOTAL_COBRADO\" FROM " + crea + " GROUP BY \"OID_EXPEDIENTE\", \"OID_COBRO\") AS cr ON cr.\"OID_EXPEDIENTE\" = ex.\"OID\"" +
                    " LEFT JOIN " + tcob + " as c ON c.\"OID\" = cr.\"OID_COBRO\"" +
                    " WHERE ex.\"OID\" > 1";
            
            return query;
        }

        #endregion
    }
}



