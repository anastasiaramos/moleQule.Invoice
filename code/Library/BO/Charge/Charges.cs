using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Editable Root Collection of Businees Objects With Child Collection
    /// Editable Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class Charges : BusinessListBaseEx<Charges, Charge>, IEntidadRegistroList
    {
		#region IEntidadRegistro

		public IEntidadRegistro IGetItem(long oid) { return (IEntidadRegistro)GetItem(oid); }		
		
		public IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }

		public void Update(Registro parent)
		{
			this.RaiseListChangedEvents = false;

			// add/update any current child objects
			foreach (Charge obj in this)
			{
				obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}		

		#endregion

        #region Business Methods

        /// <summary>
        /// Crea y añade un elemento a la lista
        /// </summary>
        /// <returns>Nuevo item</returns>
        public Charge NewItem(Cliente parent, ETipoCobro tipo)
        {
            this.NewItem(Charge.NewChild(parent));
            this[Count - 1].TipoCobro = (int)tipo;
            return this[Count - 1];
        }

        public override void Remove(long oid)
        {
            Charge obj = GetItem(oid);
            if (obj != null)
            {
                obj.CobroFacturas.Clear();
                base.Remove(oid);
            }
        }

		public new void Remove(Charge item)
		{
			throw new iQException("Función no admitida. Utilice Remove(Cobro item, Cliente parent)");
		}

		public void Remove(Charge item, Cliente parent)
		{
			base.Remove(item);
			parent.UpdateCredito();
		}

		public void ChangeState(EEstado estado, Charge item, Cliente parent)
		{
			Charge obj = GetItem(item.Oid);
			if (obj != null)
			{
				obj.ChangeEstado(estado);
				parent.UpdateCredito();
			}
		}

        public override void MarkAsNew()
        {
            foreach (Charge item in Items)
            {
                item.MarkItemNew();
				item.CobroFacturas.MarkAsNew();
                item.CobroREAs.MarkAsNew();
            }
        }

        public decimal GetTotalREA()
        {
            decimal total = 0;
            foreach (Charge item in Items)
            {
                total += item.CobroREAs.GetTotal();
            }

            return total;       
        }

        #endregion

		#region Common Factory Methods

		private Charges() {}
		
		#endregion
		
		#region Child Factory Methods

        private Charges(IList<Charge> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private Charges(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

        public static Charges NewChildList() { return new Charges(); }

        public static Charges GetChildList(IList<Charge> lista) 
		{ 
			Charges list = new Charges(lista);
			list.MarkAsChild();
			return list;
		}
        public static Charges GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static Charges GetChildList(int sessionCode, IDataReader reader, bool childs) { return new Charges(sessionCode, reader, childs); }

		public static Charges GetChildList(int sessionCode, List<long> oid_list, bool childs)
		{
			return GetChildList(sessionCode, Charges.SELECT(new QueryConditions { OidList = oid_list }), childs);
		}
		internal static Charges GetChildList(int sessionCode, string query, bool childs)
		{
			if (!Charge.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Charge.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			return DataPortal.Fetch<Charges>(criteria);
		}

		#endregion

        #region Root Factory Methods

        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static Charges NewList()
        {
            if (!Charge.CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new Charges();
        }

		public static Charges GetList() { return GetList(true); }
		public static Charges GetList(bool childs)
		{
			return GetList(childs, SELECT());
		}

        public static Charges GetList(QueryConditions conditions, bool childs)
		{
            conditions.Order = System.ComponentModel.ListSortDirection.Descending;
			return GetList(childs, Charges.SELECT(conditions));
		}

		public static Charges GetListByExpediente(long oidExpedient, bool childs)
		{
			Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions 
			{
				Expediente = ExpedientInfo.New(oidExpedient), 
				TipoCobro = ETipoCobro.REA
			};

			return GetList(childs, SELECT(conditions));
		}

		internal static Charges GetList(bool childs, string query)
		{
			if (!Charge.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Charge.GetCriteria(Charge.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			Charge.BeginTransaction(criteria.SessionCode);

			return DataPortal.Fetch<Charges>(criteria);
		}

		public static Charges GetChildList(Cliente parent, bool childs)
		{
			CriteriaEx criteria = Charge.GetCriteria(parent.SessionCode);
			criteria.Query = Charges.SELECT(parent);
			criteria.Childs = childs;

			return DataPortal.Fetch<Charges>(criteria);
		}

        #endregion

        #region Root Data Access

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        /// <remarks>LA UTILIZA EL DATAPORTAL</remarks>
        private void DataPortal_Fetch(CriteriaEx criteria) { Fetch(criteria); }

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        private void Fetch(CriteriaEx criteria)
        {
            try
            {
                this.RaiseListChangedEvents = false;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    Charge.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
						this.AddItem(Charge.GetChild(SessionCode, reader, Childs));
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

        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
        /// de los flags de cada objeto de la lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Charge obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (Charge obj in this)
                {
                    if (!this.Contains(obj))
                    {
                        if (obj.IsNew)
                            obj.Insert(this);
                        else
                            obj.Update(this);
                    }
                }

                if (!SharedTransaction) Transaction().Commit();
            }
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				if (!SharedTransaction)
					BeginTransaction();

				this.RaiseListChangedEvents = true;
			}
        }

        #endregion

        #region Child Data Access

        // called to copy objects data from list and to retrieve child data from db
        private void Fetch(IList<Charge> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (Charge item in lista)
                this.AddItem(Charge.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list and to retrieve child data from db
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(Charge.GetChild(SessionCode, reader, Childs));

            this.RaiseListChangedEvents = true;
        }

        internal void Update(Cliente parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Charge obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Charge obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

		public static string SELECT() { return Charge.SELECT(new QueryConditions(), true); }
        public static string SELECT(Library.Invoice.QueryConditions conditions) { return Charge.SELECT(conditions, true); }
        public static string SELECT(Cliente source) 
		{
			QueryConditions conditions = new QueryConditions
			{
				Cliente = source.GetInfo(false),
				TipoCobro = ETipoCobro.Cliente,
                Order = System.ComponentModel.ListSortDirection.Descending
			};
			return Charge.SELECT(conditions, false); 
		}
        
		#endregion
    }
}
