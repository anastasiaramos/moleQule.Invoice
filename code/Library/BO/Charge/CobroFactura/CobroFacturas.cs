using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class CobroFacturas : BusinessListBaseEx<CobroFacturas, CobroFactura>
    {
        #region Business Methods

        public CobroFactura NewItem(Charge parent, OutputInvoiceInfo factura)
        {
            this.AddItem(CobroFactura.NewChild(parent, factura));
            return this[Count - 1];
        }
        public CobroFactura NewItem(OutputInvoice parent)
        {
            this.AddItem(CobroFactura.NewChild(parent));
            return this[Count - 1];
        }
        public CobroFactura NewItem(OutputInvoiceInfo parent)
        {
            this.AddItem(CobroFactura.NewChild(parent));
            return this[Count - 1];
        }

        public bool CobroExists(long oid)
        {
            foreach (CobroFactura obj in this)
                if (obj.OidCobro == oid)
                    return true;
            return false;
        }

        public decimal GetTotal()
        {
            decimal suma = 0.0m;

            foreach (CobroFactura c in this)
                suma += c.Cantidad;

            return suma;
        }

		public decimal GastosDemora()
		{
			decimal total = 0;

			foreach (CobroFactura c in this)
				total += c.GastosCobro;

			return total;
		}

        public CobroFactura GetItemByFactura(long oid_factura)
        {
            foreach (CobroFactura obj in this)
            {
                if (obj.OidFactura == oid_factura)
                    return obj;
            }
            return null;
        }

        public CobroFactura GetItemByCobro(long oid_cobro)
        {
            foreach (CobroFactura obj in this)
            {
                if (obj.OidCobro == oid_cobro)
                    return obj;
            }
            return null;
        }

        #endregion

        #region Factory Methods

        private CobroFacturas()
        {
            MarkAsChild();
        }
        private CobroFacturas(IList<CobroFactura> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private CobroFacturas(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        public static CobroFacturas NewChildList() { return new CobroFacturas(); }

        public static CobroFacturas GetChildList(IList<CobroFactura> lista) { return new CobroFacturas(lista); }
		public static CobroFacturas GetChildList(int sessionCode, IDataReader reader, bool childs = true) { return new CobroFacturas(sessionCode, reader, childs); }
        public static CobroFacturas GetChildList(OutputInvoice parent, bool childs)
        {
            CriteriaEx criteria = CobroFactura.GetCriteria(parent.SessionCode);

			criteria.Query = CobroFacturas.SELECT_BY_FACTURA(parent.Oid);
            criteria.Childs = childs;

            return DataPortal.Fetch<CobroFacturas>(criteria);
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            try
            {
                this.RaiseListChangedEvents = false;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    CobroFactura.DoLOCK(Session());
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
						CobroFactura obj = CobroFactura.GetChild(SessionCode, reader);
                        this.AddItem(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<CobroFactura> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (CobroFactura item in lista)
                this.AddItem(CobroFactura.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
				this.AddItem(CobroFactura.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }
        
        internal void Update(Charge parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (CobroFactura obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (CobroFactura obj in this)
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

        internal void Update(OutputInvoice parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (CobroFactura obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (CobroFactura obj in this)
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

		public static string SELECT(QueryConditions conditions) { return CobroFactura.SELECT(conditions, true); }
		public static string SELECT(Charge source) { return SELECT(new QueryConditions() { Cobro = source.GetInfo(false) }); }
		public static string SELECT_BY_FACTURA(long oid) { return CobroFactura.SELECT_BY_FACTURA(oid, true); }
		public static string SELECT_BY_COBRO(long oid) { return CobroFactura.SELECT_BY_COBRO(oid, true); }

        #endregion
    }
}
