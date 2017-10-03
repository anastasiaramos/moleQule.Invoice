using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class OutputDeliveryLines : BusinessListBaseEx<OutputDeliveryLines, OutputDeliveryLine>
    {
        #region Business Methods
	
        public OutputDeliveryLine NewItem(OutputDelivery parent)
        {
            this.NewItem(OutputDeliveryLine.NewChild(parent));
            return this[Count - 1];
        }
        public OutputDeliveryLine NewItem(OutputDelivery parent, OutputDeliveryLineInfo concepto)
        {
            this.NewItem(OutputDeliveryLine.NewChild(parent, concepto));
            return this[Count - 1];
        }

        public OutputDeliveryLine CopyItem(OutputDelivery parent, OutputDeliveryLineInfo concepto)
        {
			OutputDeliveryLine item;

            this.NewItem(OutputDeliveryLine.NewChild(parent, concepto));
            item = this[Count - 1];
			item.Oid = item.Oid;

			if (concepto.OidPartida != 0)
			{
				//Stock stock = item.Stocks.NewItem(item);
				//stock.Oid = concepto.Stocks[0].Oid;
            }
            item.MarkItemOld();
            item.MarkItemDirty();

			return item;
        }

		public OutputDeliveryLine GetItemByBatch(long oidBatch, ETipoFacturacion saleWay)
		{
			return this.FirstOrDefault(x => x.OidPartida == oidBatch && x.ETipoFacturacion == saleWay);
		}
		public OutputDeliveryLine GetItemByProduct(long oidProduct, ETipoFacturacion saleWay)
		{
			return this.FirstOrDefault(x => x.OidProducto == oidProduct && x.ETipoFacturacion == saleWay);
		}

		public OutputDeliveryLine GetComponente(BatchInfo componente)
		{
			foreach (OutputDeliveryLine item in this)
				if ((item.OidKit == componente.OidKit) && (item.OidPartida == componente.Oid))
					return item;

			return null;
		}

		public void Remove(OutputDeliveryLine item, bool cache)
        {
            base.Remove(item);

            if (cache)
            {
                BatchList list = Cache.Instance.Get(typeof(BatchList)) as BatchList;
                if (list == null) return;

                BatchInfo pExp = list.GetItem(item.OidPartida);
                if (pExp != null) 
                {
                    pExp.StockKilos += item.CantidadKilos;
                    pExp.StockBultos += item.CantidadBultos;
                }
            }
        }

        #endregion

        #region Factory Methods

        private OutputDeliveryLines()
        {
            MarkAsChild();
        }
        private OutputDeliveryLines(IList<OutputDeliveryLine> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
		private OutputDeliveryLines(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(this, reader);
        }

        public static OutputDeliveryLines NewChildList() { return new OutputDeliveryLines(); }

        public static OutputDeliveryLines GetChildList(IList<OutputDeliveryLine> lista) { return new OutputDeliveryLines(lista); }
		public static OutputDeliveryLines GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static OutputDeliveryLines GetChildList(int sessionCode, IDataReader reader, bool childs) { return new OutputDeliveryLines(sessionCode, reader, childs); }

		internal void LoadLibroGanadero()
		{
			Hashtable oidExpedientes = new Hashtable();

			oidExpedientes.Add(0, 0);

			foreach (OutputDeliveryLine item in this)
			{
				if (item.OidExpediente == 0) continue;
				if (oidExpedientes.ContainsKey(item.OidExpediente)) continue;

				oidExpedientes.Add(item.OidExpediente, item.OidExpediente);

				Expedient expediente = Store.Expedient.Get(item.OidExpediente, false, true, SessionCode);
				if (expediente == null) continue;
				if (expediente.ETipoExpediente != ETipoExpediente.Ganado) continue;

                LivestockBook libro = LivestockBook.Get(1, false, true, SessionCode);
				libro.LoadLineasByExpediente(item.OidExpediente, false);
			}

			oidExpedientes.Clear();
		}

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<OutputDeliveryLine> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (OutputDeliveryLine item in lista)
                this.AddItem(OutputDeliveryLine.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(OutputDeliveryLines parent, IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(OutputDeliveryLine.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }
        		
        internal void Update(OutputDelivery parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				LoadLibroGanadero();

				// update (thus deleting) any deleted child objects
				foreach (OutputDeliveryLine obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (OutputDeliveryLine obj in this)
				{
					if (obj.IsNew)
						obj.Insert(parent);
					else
						obj.Update(parent);
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
        }
		
        #endregion

		#region SQL

		public static string SELECT(QueryConditions conditions) { return OutputDeliveryLine.SELECT(conditions, true); }
		public static string SELECT(OutputDelivery albaran) { return SELECT(new QueryConditions { OutputDelivery = albaran.GetInfo(false) }); }

		#endregion
	}
}
