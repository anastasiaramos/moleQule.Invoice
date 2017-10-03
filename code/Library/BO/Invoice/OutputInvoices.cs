using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class OutputInvoices : BusinessListBaseEx<OutputInvoices, OutputInvoice>, IEntidadRegistroList
	{
		#region Properties

		protected new Hashtable MaxSerial = new Hashtable();
		protected Hashtable MaxSerialRectificativas = new Hashtable();
		
		#endregion

		#region IEntidadRegistro

		public IEntidadRegistro IGetItem(long oid) { return (IEntidadRegistro)GetItem(oid); }		
		
		public IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }

		public void Update(Registro parent)
		{
			this.RaiseListChangedEvents = false;

			// add/update any current child objects
			foreach (OutputInvoice obj in this)
			{
				obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}		

		#endregion

        #region Business Methods

		public void SetNextCode(OutputInvoice item)
		{
			int index = this.IndexOf(item);

			if ((item.Rectificativa && MaxSerialRectificativas[item.OidSerie] == null)
                || (!item.Rectificativa && MaxSerial[item.OidSerie] == null))
			{
				item.GetNewCode();

				if (item.Rectificativa)
					MaxSerialRectificativas.Add(item.OidSerie, item.Serial);
				else
					MaxSerial.Add(item.OidSerie, item.Serial);
			}
			else
			{							
				if (item.Rectificativa)
				{
					item.Serial = (long)MaxSerialRectificativas[item.OidSerie] + 1;
					item.Codigo = item.Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT + "-R");
					MaxSerialRectificativas[item.OidSerie] = (long)MaxSerialRectificativas[item.OidSerie] + 1;
				}
				else
				{
					item.Serial = (long)MaxSerial[item.OidSerie] + 1;
					item.Codigo = item.Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
					item.Fecha = item.Fecha.AddSeconds(item.Serial - this[0].Serial);
					MaxSerial[item.OidSerie] = (long)MaxSerial[item.OidSerie] + 1;
				}
			}
		}

        public OutputInvoice NewItem()
        {
            this.AddItem(OutputInvoice.NewChild());
            return this[Count - 1];
        }

		public OutputInvoice NewItem(OutputDeliveryInfo albaran)
		{
			OutputInvoice item = NewItem();

			ClienteInfo cliente = ClienteInfo.Get(albaran.OidHolder, false, true);

			item.CopyFrom(cliente);
			item.CopyFrom(albaran);
			item.Insert(albaran);

			SetNextCode(item);

			return item;
		}

		public void NewItems(List<OutputDeliveryInfo> albaranes)
		{
			foreach (OutputDeliveryInfo item in albaranes)
			{
				if (item.EEstado != EEstado.Abierto) continue;
				if (item.Contado) continue;
				OutputInvoice newItem = NewItem(item);
				item.EEstado = EEstado.Billed;
				item.NumeroFactura = newItem.Codigo;
			}
		}

        #endregion

		#region Child Factory Methods

		public static OutputInvoices GetChildList(int sessionCode, List<long> oid_list, bool childs)
		{
			return GetChildList(sessionCode, OutputInvoices.SELECT(new QueryConditions { OidList = oid_list }), childs);
		}
		internal static OutputInvoices GetChildList(int sessionCode, string query, bool childs)
		{
			if (!OutputInvoice.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = OutputInvoice.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			return DataPortal.Fetch<OutputInvoices>(criteria);
		}

		#endregion

		#region Root Factory Methods

		private OutputInvoices() { }

        public static OutputInvoices NewList() 
		{ 
			OutputInvoices list = new OutputInvoices();
			list.SessionCode = OutputInvoice.OpenSession();
			BeginTransaction(list.SessionCode);
			return list;
		}

		public static OutputInvoices GetList() { return GetList(true); }
		public static OutputInvoices GetList(bool childs)
		{
			return GetList(OutputInvoices.SELECT(), childs);
		}
		public static OutputInvoices GetList(List<long> oid_list, bool childs)
		{
			return GetList(OutputInvoices.SELECT(new QueryConditions { OidList = oid_list }), childs);
		}
		public static OutputInvoices GetList(Library.Invoice.QueryConditions conditions, bool childs)
		{
			return GetList(OutputInvoices.SELECT(conditions), childs);
		}
		
		internal static OutputInvoices GetList(string query, bool childs)
		{
			if (!OutputInvoice.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			OutputInvoice.BeginTransaction(criteria.SessionCode);

			return DataPortal.Fetch<OutputInvoices>(criteria);
		}

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    OutputInvoice.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
					{
						OutputInvoice item = OutputInvoice.GetChild(SessionCode, reader, _childs);
						this.AddItem(item);

						if (item.Rectificativa)
						{
							if (MaxSerialRectificativas[item.OidSerie] == null)
								MaxSerialRectificativas.Add(item.OidSerie, item.Serial);
							else if (item.Serial > (long)MaxSerialRectificativas[item.OidSerie])
								MaxSerialRectificativas[item.OidSerie] = item.Serial;
						}
						else
						{
							if (MaxSerial[item.OidSerie] == null)
								MaxSerial.Add(item.OidSerie, item.Serial);
							else if (item.Serial > (long)MaxSerial[item.OidSerie])
								MaxSerial[item.OidSerie] = item.Serial;
						}
					}
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (OutputInvoice obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (OutputInvoice obj in this)
                {
                    if (!this.Contains(obj))
                    {
                        if (obj.IsNew)
                            obj.Insert(this);
                        else
                            obj.Update(this);
                    }
                }

                Transaction().Commit();
            }
            catch (Exception ex)
            {
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(Library.Invoice.QueryConditions conditions) { return OutputInvoice.SELECT(conditions, true); }

		#endregion

    }
}
