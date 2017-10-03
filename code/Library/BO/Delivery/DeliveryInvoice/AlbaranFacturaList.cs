using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 

using moleQule.Library;

using NHibernate;

namespace moleQule.Library.Invoice
{

	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class AlbaranFacturaList : ReadOnlyListBaseEx<AlbaranFacturaList, AlbaranFacturaInfo>
    {
        #region Business Methods

        public AlbaranFacturaInfo GetItemByFactura(long oid_factura)
        {
            foreach (AlbaranFacturaInfo obj in this)
            {
                if (obj.OidFactura == oid_factura)
                    return obj;
            }
            return null;
        }

        public AlbaranFacturaInfo GetItemByAlbaran(long oid_Albaran)
        {
            foreach (AlbaranFacturaInfo obj in this)
            {
                if (obj.OidAlbaran == oid_Albaran)
                    return obj;
            }
            return null;
        }

        /*public bool AlbaranExists(long oid)
        {
            foreach (AlbaranFacturaInfo obj in this)
                if (obj.OidAlbaran == oid)
                    return true;
            return false;
        }*/
        
        public AlbaranFacturaInfo GetItem(long oid_albaran, long oid_factura)
        {
            foreach (AlbaranFacturaInfo obj in this)
            {
                if (obj.OidAlbaran == oid_albaran && obj.OidFactura == oid_factura)
                    return obj;
            }
            return null;
        }

        #endregion

		#region Child Factory Methods

		private AlbaranFacturaList(IList<AlbaranFactura> lista)
		{
			Fetch(lista);
		}

		public static AlbaranFacturaList GetChildList(IList<AlbaranFacturaInfo> list)
		{
			AlbaranFacturaList flist = new AlbaranFacturaList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (AlbaranFacturaInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}
		public static AlbaranFacturaList GetChildList(IList<AlbaranFactura> list) { return new AlbaranFacturaList(list); }
		public static AlbaranFacturaList GetChildList(int sessionCode, IDataReader reader) { return new AlbaranFacturaList(sessionCode, reader); }

		#endregion

		#region Root Factory Methods

		private AlbaranFacturaList() { }		
        private AlbaranFacturaList(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			Fetch(reader);
		}

		public static AlbaranFacturaList GetList() { return AlbaranFacturaList.GetList(true); }
		public static AlbaranFacturaList GetList(bool childs)
		{
			CriteriaEx criteria = AlbaranFactura.GetCriteria(AlbaranFactura.OpenSession());
            criteria.Childs = childs;
			
			criteria.Query = AlbaranFacturaList.SELECT();

			AlbaranFacturaList list = DataPortal.Fetch<AlbaranFacturaList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}
				
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<AlbaranFactura> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (AlbaranFactura item in lista)
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
                this.AddItem(AlbaranFacturaInfo.GetChild(SessionCode, reader));

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
						this.AddItem(AlbaranFacturaInfo.GetChild(SessionCode, reader, Childs));

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

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return AlbaranFactura.SELECT(conditions, false); }
		public static string SELECT(OutputDeliveryInfo source) { return SELECT(new QueryConditions { OutputDelivery = source }); }
		public static string SELECT(OutputInvoiceInfo source) { return SELECT(new QueryConditions { Factura = source }); }

		#endregion	
	}
}

