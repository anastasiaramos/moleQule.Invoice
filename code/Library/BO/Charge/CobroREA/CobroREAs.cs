using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class CobroREAs : BusinessListBaseEx<CobroREAs, CobroREA>
    {
        #region Business Methods

        public CobroREA NewItem(Charge parent, FacREAInfo fac)
        {
            this.AddItem(CobroREA.NewChild(parent, fac));
            return this[Count - 1];
        }

        public CobroREA NewItem(Charge parent, LineaFomentoInfo line)
        {
            this.AddItem(CobroREA.NewChild(parent, line));
            return this[Count - 1];
        }

        public CobroREA NewItem(REAExpedient parent)
        {
            this.AddItem(CobroREA.NewChild(parent));
            return this[Count - 1];
        }

        public CobroREA NewItem(ExpedienteREAInfo parent)
        {
            this.AddItem(CobroREA.NewChild(parent));
            return this[Count - 1];
        }

        public bool CobroExists(long oid)
        {
            foreach (CobroREA obj in this)
                if (obj.OidCobro == oid)
                    return true;
            return false;
        }

        public decimal GetTotal()
        {
            decimal suma = 0.0m;

            foreach (CobroREA c in this)
                suma += c.Cantidad;

            return suma;
        }

        public CobroREA GetItemByExpedienteREA(long oid)
        {
            foreach (CobroREA obj in this)
            {
				if (obj.OidExpedienteREA == oid)
                    return obj;
            }
            return null;
        }

        public CobroREA GetItemByCobro(long oid_cobro)
        {
            foreach (CobroREA obj in this)
            {
                if (obj.OidCobro == oid_cobro)
                    return obj;
            }
            return null;
        }

        #endregion

        #region Factory Methods

        private CobroREAs()
        {
            MarkAsChild();
        }
        private CobroREAs(IList<CobroREA> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private CobroREAs(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

        public static CobroREAs NewChildList() { return new CobroREAs(); }

        public static CobroREAs GetChildList(IList<CobroREA> lista) { return new CobroREAs(lista); }
		public static CobroREAs GetChildList(int sessionCode, IDataReader reader, bool childs) { return new CobroREAs(sessionCode, reader, childs); }
		public static CobroREAs GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<CobroREA> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (CobroREA item in lista)
                this.AddItem(CobroREA.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
				this.AddItem(CobroREA.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }


        internal void Update(Charge parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (CobroREA obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (CobroREA obj in this)
				{
					if (obj.IsNew)
						obj.Insert(parent);
					else
						obj.Update(parent);
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
        }

        #endregion

        #region SQL

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
					" INNER JOIN " + ex+ " AS EX ON CF.\"OID_EXPEDIENTE\" = EX.\"OID\"" +
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
