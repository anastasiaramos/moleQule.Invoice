using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class AlbaranFacturas : BusinessListBaseEx<AlbaranFacturas, AlbaranFactura>
    {
        #region Properties

        public List<OutputDeliveryInfo> ToDelete = null;

        #endregion

        #region Business Methods

        public AlbaranFactura NewItem(OutputInvoice factura, OutputDeliveryInfo albaran)
        {
            this.AddItem(AlbaranFactura.NewChild(factura, albaran));
			factura.SetAlbaranes();
            return this[Count - 1];
        }

        public bool AlbaranExists(long oid)
        {
            foreach (AlbaranFactura obj in this)
                if (obj.OidAlbaran == oid)
                    return true;
            return false;
        }

        public AlbaranFactura GetItemByFactura(long oid)
        {
            foreach (AlbaranFactura obj in this)
            {
                if (obj.OidFactura == oid)
                    return obj;
            }
            return null;
        }
		public AlbaranFactura GetItemByAlbaran(long oid)
		{
			foreach (AlbaranFactura obj in this)
			{
				if (obj.OidAlbaran == oid)
					return obj;
			}
			return null;
		}
        
		public bool GetItem(long oid_factura, long oid_albaran)
        {
            foreach (AlbaranFactura obj in this)
            {
                if (obj.OidFactura == oid_factura && obj.OidAlbaran == oid_albaran)
                    return true;
            }
            return false;
        }

        public void Remove(OutputInvoice factura, OutputDeliveryInfo albaran)
        {
            foreach (AlbaranFactura item in this)
                if (item.OidFactura == factura.Oid && item.OidAlbaran == albaran.Oid)
                {
                    this.Remove(item.Oid);
					factura.SetAlbaranes();
                    break;
                }
        }

		/// <summary>
		/// Crea los conceptos de factura asociados a un albarán
		/// </summary>
		/// <param name="source"></param>
		public virtual void Compact(OutputInvoice invoice)
		{
			if (this.Count == 0) return;

			OutputDelivery main_albaran = null;
			Cash caja = null;

			try
			{
				List<long> oid_list = new List<long>();

				foreach (AlbaranFactura item in this)
					oid_list.Add(item.OidAlbaran);

				OutputDeliveryList albaranes = OutputDeliveryList.GetList(oid_list, true);
                main_albaran = OutputDelivery.Get(oid_list[0], true, invoice.SessionCode);                

				foreach (AlbaranFactura af in this)
				{
					if (af.OidAlbaran == main_albaran.Oid) continue;

					OutputDeliveryInfo source = albaranes.GetItem(af.OidAlbaran);
					main_albaran.Merge(source);
				}

				main_albaran.Compact();

				SortedBindingList<AlbaranFactura> sorted_list = this.GetSortedList("CodigoAlbaran", ListSortDirection.Ascending);
				OutputDeliveryInfo first_albaran = OutputDeliveryInfo.Get(sorted_list[0].OidAlbaran, ETipoEntidad.Cliente, false);

				main_albaran.CopyFrom(invoice);
				main_albaran.Codigo = first_albaran.Codigo;
				main_albaran.Serial = first_albaran.Serial;
                main_albaran.Save();
                                
				ToDelete = new List<OutputDeliveryInfo>();

				for (int i = 1; i < oid_list.Count; i++)
				{
					Remove(GetItemByAlbaran(oid_list[i]).Oid);
					ToDelete.Add(albaranes.GetItem(oid_list[i]));
				}

				CashLine.DeleteByAlbaranList(albaranes.GetListInfo(), ModulePrincipal.GetCajaTicketsSetting());
				Ticket.DeleteFromList(albaranes.GetListInfo());

				//Actualizamos la caja de Tickets
				caja = Cash.Get(ModulePrincipal.GetCajaTicketsSetting(), true, invoice.SessionCode);
				caja.UpdateSaldo();
                caja.SaveAsChild();

				AlbaranFactura ab = GetItemByAlbaran(main_albaran.Oid);
				ab.CodigoAlbaran = main_albaran.Codigo;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally 
			{
				Cache.Instance.Remove(typeof(ClienteList));
				Cache.Instance.Remove(typeof(ProductList));
			}
		}

        #endregion

        #region Child Factory Methods

        private AlbaranFacturas()
        {
            MarkAsChild();
        }
        private AlbaranFacturas(IList<AlbaranFactura> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
		private AlbaranFacturas(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

        public static AlbaranFacturas NewChildList() { return new AlbaranFacturas(); }

        public static AlbaranFacturas GetChildList(IList<AlbaranFactura> lista) { return new AlbaranFacturas(lista); }
		public static AlbaranFacturas GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static AlbaranFacturas GetChildList(int sessionCode, IDataReader reader, bool childs) { return new AlbaranFacturas(sessionCode, reader, childs); }
		public static AlbaranFacturas GetChildList(OutputInvoice parent, bool childs)
		{
			CriteriaEx criteria = AlbaranFactura.GetCriteria(parent.SessionCode);

			criteria.Query = AlbaranFacturas.SELECT(parent);
			criteria.Childs = childs;

			return DataPortal.Fetch<AlbaranFacturas>(criteria);
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
					AlbaranFactura.DoLOCK(Session());
					IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
					{
						AlbaranFactura obj = AlbaranFactura.GetChild(SessionCode, reader);
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
        private void Fetch(IList<AlbaranFactura> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (AlbaranFactura item in lista)
                this.AddItem(AlbaranFactura.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(AlbaranFactura.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }

        internal void Update(OutputDelivery parent)
        {
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (AlbaranFactura obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (AlbaranFactura obj in this)
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
				foreach (AlbaranFactura obj in DeletedList)
				{
					if (!GetItem(obj.OidFactura, obj.OidAlbaran))
						obj.DeleteSelf(parent);
				}

				// add/update any current child objects
				foreach (AlbaranFactura obj in this)
				{
					bool existe = false;

					if (obj.IsNew)
					{
						//Si el albarán se ha eliminado y se ha vuelto a insertar no hay que volver a guardarlo
						foreach (AlbaranFactura albaran in DeletedList)
						{
							if (albaran.OidAlbaran == obj.OidAlbaran)
							{
								existe = true;
								break;
							}
						}
						if (existe)
							continue;
						obj.Insert(parent);
					}
					else
						obj.Update(parent);

				}

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
        }

        #endregion

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return AlbaranFactura.SELECT(conditions, true); }
		public static string SELECT(OutputDelivery source) { return SELECT(new QueryConditions { OutputDelivery = source.GetInfo(false) }); }
		public static string SELECT(OutputInvoice source) { return SELECT(new QueryConditions { Factura = source.GetInfo(false) }); }

		#endregion
    }
}
