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
	public class CobroFacturaList : ReadOnlyListBaseEx<CobroFacturaList, CobroFacturaInfo>
    {
        #region Business Methods

        public CobroFacturaInfo GetItemByFactura(long oid_factura)
        {
            foreach (CobroFacturaInfo obj in this)
            {
                if (obj.OidFactura == oid_factura)
                    return obj;
            }
            return null;
        }

        public CobroFacturaInfo GetItemByCobro(long oid_cobro)
        {
            foreach (CobroFacturaInfo obj in this)
            {
                if (obj.OidCobro == oid_cobro)
                    return obj;
            }
            return null;
        }

        /*public bool CobroExists(long oid)
        {
            foreach (CobroFacturaInfo obj in this)
                if (obj.OidCobro == oid)
                    return true;
            return false;
        }*/
        
        public CobroFacturaInfo GetItem(long oid_cobro, long oid_factura)
        {
            foreach (CobroFacturaInfo obj in this)
            {
                if (obj.OidCobro == oid_cobro && obj.OidFactura == oid_factura)
                    return obj;
            }
            return null;
        }

        public decimal GetTotal()
        {
            decimal suma = 0.0m;

            foreach (CobroFacturaInfo c in this)
            {
                suma += c.Cantidad;
            }
            return suma;
        }

		public decimal GastosDemora()
		{
			decimal total = 0;

			foreach (CobroFacturaInfo c in this)
				total += c.GastosCobro;

			return total;
		}

        #endregion

        #region Factory Methods

        private CobroFacturaList() {}		
		private CobroFacturaList(IList<CobroFactura> lista)
		{
            Fetch(lista);
        }
		private CobroFacturaList(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			Fetch(reader);
		}

		public static CobroFacturaList GetList(bool childs = false) { return GetList(new QueryConditions(), childs); }
        public static CobroFacturaList GetList(QueryConditions conditions, bool childs = false)
        {
            CriteriaEx criteria = CobroFactura.GetCriteria(CobroFactura.OpenSession());
            criteria.Childs = childs;

            criteria.Query = CobroFacturaList.SELECT(conditions);

            CobroFacturaList list = DataPortal.Fetch<CobroFacturaList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static CobroFacturaList GetList(CriteriaEx criteria)
        {
            return CobroFacturaList.RetrieveList(typeof(CobroFactura), AppContext.ActiveSchema.Code, criteria);
        }
		
		/// <summary>
        /// Builds a CobroFacturaList from a IList<!--<CobroFacturaInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>CobroFacturaList</returns>
        public static CobroFacturaList GetChildList(IList<CobroFacturaInfo> list)
        {
            CobroFacturaList flist = new CobroFacturaList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (CobroFacturaInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static CobroFacturaList GetChildList(IList<CobroFactura> list) { return new CobroFacturaList(list); }
		public static CobroFacturaList GetChildList(int sessionCode, IDataReader reader) { return new CobroFacturaList(sessionCode, reader); }
		public static CobroFacturaList GetChildList(OutputInvoiceInfo parent, bool childs)
		{
			CriteriaEx criteria = CobroFactura.GetCriteria(OutputInvoice.OpenSession());

			criteria.Query = CobroFacturaList.SELECT_BY_FACTURA(parent.Oid);
			criteria.Childs = childs;

			CobroFacturaList list = DataPortal.Fetch<CobroFacturaList>(criteria);
			list.CloseSession();

			return list;
		}

		public void SetChildList(IList<CobroFacturaInfo> list)
		{
			if (list.Count > 0)
			{
				IsReadOnly = false;

				foreach (CobroFacturaInfo item in list)
					AddItem(item);

				IsReadOnly = true;
			}
		}

		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<CobroFactura> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (CobroFactura item in lista)
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
                this.AddItem(CobroFacturaInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(CobroFacturaInfo.GetChild(SessionCode, reader, Childs));

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

		public static string SELECT(QueryConditions conditions) { return CobroFactura.SELECT(conditions, false); }
		public static string SELECT(ChargeInfo source) { return SELECT(new QueryConditions() { Cobro = source }); }
		public static string SELECT_BY_FACTURA(long oid) { return CobroFactura.SELECT_BY_FACTURA(oid, false); }
        public static string SELECT_BY_COBRO(long oid) { return CobroFactura.SELECT_BY_COBRO(oid, false); }

        #endregion
	
	}
}

