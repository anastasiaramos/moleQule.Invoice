using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class OutputInvoiceLines : BusinessListBaseEx<OutputInvoiceLines, OutputInvoiceLine>
    {
        #region Business Methods
	
        public OutputInvoiceLine NewItem(OutputInvoice parent)
        {
            this.NewItem(OutputInvoiceLine.NewChild(parent));
            return this[Count - 1];
        }

        /*public bool ContainsPartida(long oid_producto_expediente)
        {
            foreach (ConceptoFactura obj in this)
                if (obj.OidPartida == oid_producto_expediente)
                    return true;

            return false;
        }*/

        public void Remove(OutputDeliveryLineInfo calbaran)
        {
            foreach (OutputInvoiceLine item in this)
                if (item.OidConceptoAlbaran == calbaran.Oid)
                {
                    this.Remove(item.Oid);
                    break;
                }
        }

		public OutputInvoiceLine GetItemByBatch(long oidBatch, ETipoFacturacion saleWay)
		{
			return this.FirstOrDefault(x => x.OidPartida == oidBatch && x.ETipoFacturacion == saleWay);
		}
		public OutputInvoiceLine GetItemByProduct(long oidProduct, ETipoFacturacion saleWay)
		{
			return this.FirstOrDefault(x => x.OidProducto == oidProduct && x.ETipoFacturacion == saleWay);
		}

        /*public ConceptoFactura GetItemByPartida(long oid_partida)
        {
            foreach (ConceptoFactura item in this)
                if (item.OidPartida == oid_partida)
                    return item;

            return null;
        }
		public ConceptoFactura GetItemByProducto(long oid_producto)
		{
			foreach (ConceptoFactura item in this)
				if ((item.OidProducto == oid_producto) && (item.OidPartida == 0))
					return item;

			return null;
		}*/

        #endregion

        #region Factory Methods

        private OutputInvoiceLines()
        {
            MarkAsChild();
        }

        private OutputInvoiceLines(IList<OutputInvoiceLine> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }

        private OutputInvoiceLines(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        public static OutputInvoiceLines NewChildList() { return new OutputInvoiceLines(); }

        public static OutputInvoiceLines GetChildList(IList<OutputInvoiceLine> lista) { return new OutputInvoiceLines(lista); }

		public static OutputInvoiceLines GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static OutputInvoiceLines GetChildList(int sessionCode, IDataReader reader, bool childs) { return new OutputInvoiceLines(sessionCode, reader, childs); }
		
        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<OutputInvoiceLine> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (OutputInvoiceLine item in lista)
                this.AddItem(OutputInvoiceLine.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(OutputInvoiceLine.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }
		
        internal void Update(OutputInvoice parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (OutputInvoiceLine obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (OutputInvoiceLine obj in this)
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

		public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(Library.Invoice.QueryConditions conditions) { return OutputInvoiceLine.SELECT(conditions, true); }
        public static string SELECT(OutputInvoice item) { return SELECT(new Library.Invoice.QueryConditions { Factura = item.GetInfo(false) }); }
		public static string SELECT(Almacen item) { return SELECT(new Library.Invoice.QueryConditions { Almacen = item.GetInfo(false) }); }
		public static string SELECT(Expedient item) { return SELECT(new Library.Invoice.QueryConditions { Expediente = item.GetInfo(false) }); }

        #endregion
    }
}
