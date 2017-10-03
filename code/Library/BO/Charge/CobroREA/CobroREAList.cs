using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class CobroREAList : ReadOnlyListBaseEx<CobroREAList, CobroREAInfo>
    {
        #region Business Methods

        public CobroREAInfo GetItemByExpediente(long oid_expediente, long oid_expediente_rea)
        {
            foreach (CobroREAInfo obj in this)
            {
                if (obj.OidExpediente == oid_expediente && obj.OidExpedienteREA == oid_expediente_rea)
                    return obj;
            }
            return null;
        }

        public CobroREAInfo GetItemByExpediente(long oid_expediente)
        {
            foreach (CobroREAInfo obj in this)
            {
                if (obj.OidExpediente == oid_expediente)
                    return obj;
            }
            return null;
        }

        public CobroREAInfo GetItemByCobro(long oid_cobro)
        {
            foreach (CobroREAInfo obj in this)
            {
                if (obj.OidCobro == oid_cobro)
                    return obj;
            }
            return null;
        }

        /*public bool CobroExists(long oid)
        {
            foreach (CobroREAInfo obj in this)
                if (obj.OidCobro == oid)
                    return true;
            return false;
        }*/
        
        public CobroREAInfo GetItem(long oid_cobro, long oid_expediente)
        {
            foreach (CobroREAInfo obj in this)
            {
                if (obj.OidCobro == oid_cobro && obj.OidExpediente == oid_expediente)
                    return obj;
            }
            return null;
        }

        public decimal GetTotal()
        {
            decimal suma = 0.0m;

            foreach (CobroREAInfo c in this)
            {
                suma += c.Cantidad;
            }
            return suma;
        }


        #endregion

        #region Factory Methods

        private CobroREAList() {}		
		private CobroREAList(IList<CobroREA> lista)
		{
            Fetch(lista);
        }
        private CobroREAList(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			Fetch(reader);
		}

		public static CobroREAList NewList() { return new CobroREAList(); }

		public static CobroREAList GetList() { return CobroREAList.GetList(true); }
		public static CobroREAList GetList(bool childs)
		{
			CriteriaEx criteria = CobroREA.GetCriteria(CobroREA.OpenSession());
            criteria.Childs = childs;
			
			criteria.Query = CobroREAList.SELECT();

			CobroREAList list = DataPortal.Fetch<CobroREAList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

        public static CobroREAList GetChildList(IList<CobroREAInfo> list)
        {
            CobroREAList flist = new CobroREAList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (CobroREAInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static CobroREAList GetChildList(IList<CobroREA> list) { return new CobroREAList(list); }
		public static CobroREAList GetChildList(int sessionCode, IDataReader reader) { return new CobroREAList(sessionCode, reader); }
		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<CobroREA> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (CobroREA item in lista)
                this.AddItem(item.GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
				this.AddItem(CobroREAInfo.GetChild(SessionCode, reader));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
		// called to retrieve data from db
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
						this.AddItem(CobroREAInfo.GetChild(SessionCode, reader, Childs));
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

		public static string SELECT()
		{
			string cbr = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));
			string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			query = "SELECT *" +
					"		,EX.\"CODIGO\" AS \"CODIGO_EXPEDIENTE\"" +
					" FROM " + cbr + " AS CF" +
					" INNER JOIN " + cb + " AS C ON CF.\"OID_COBRO\" = C.\"OID\"" +
					" INNER JOIN " + ex+ " AS EX ON CF.\"OID_EXPEDIENTE\" = EX.\"OID\"";

			return query;
		}

        public static string SELECT_BY_EXPEDIENTE(long oid)
        {
            string cbr = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

            string query;

            query = "SELECT *" +
					"		,EX.\"CODIGO\" AS \"CODIGO_EXPEDIENTE\"" +
                    " FROM " + cbr + " AS CF" +
                    " INNER JOIN " + cb + " AS C ON CF.\"OID_COBRO\" = C.\"OID\"" +
					" INNER JOIN " + ex + " AS EX ON CF.\"OID_EXPEDIENTE\" = EX.\"OID\"" +
                    " WHERE CF.\"OID_EXPEDIENTE\" = " + oid.ToString();

            return query;
        }

        public static string SELECT_BY_COBRO(long oid)
        {
            string tabla = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));
            string tabla_cobro = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

            string query;

            query = "SELECT *" +
					"		,EX.\"CODIGO\" AS \"CODIGO_EXPEDIENTE\"" +
                    " FROM " + tabla + " AS CF" +
                    " INNER JOIN " + tabla_cobro + " AS F ON F.\"OID\" = CF.\"OID_COBRO\"" +
					" INNER JOIN " + ex+ " AS EX ON CF.\"OID_EXPEDIENTE\" = EX.\"OID\"" +
                    " WHERE CF.\"OID_COBRO\" = " + oid.ToString();

            return query;
        }

        #endregion
	
	}
}

