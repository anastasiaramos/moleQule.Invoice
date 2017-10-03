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
	/// Editable Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
    public class Loans : BusinessListBaseEx<Loans, Loan>, IEntidadRegistroList
    {
        #region IEntidadRegistro

        public IEntidadRegistro IGetItem(long oid) { return (IEntidadRegistro)GetItem(oid); }

        public IEntidadRegistro ISave() { return (IEntidadRegistro)Save(); }

        public void Update(Registro parent)
        {
            this.RaiseListChangedEvents = false;

            // add/update any current child objects
            foreach (Loan obj in this)
            {
                obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

		#region Business Methods

        //public void SetMaxSerial()
        //{
        //    foreach (Prestamo item in this)
        //        if (item.Serial > _max_serial) MaxSerial = item.Serial;
        //}
		
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public Loan NewItem()
        {
            this.AddItem(Loan.NewChild());
            return this[Count - 1];
        }
        public Loan NewItem(PaymentInfo source)
        {
            this.AddItem(Loan.NewChild(source));
            return this[Count-1];
        }

        public void UpdatePagoValues(Payment pago)
        {
            Loan item;
            decimal acumulado;

            for (int i = 0; i < Items.Count; i++)
            {
                item = Items[i];

                /*if (item.OidPago != pago.Oid)
                    item.Asignado = 0;*/

                if (i == 0) acumulado = 0;
                else acumulado = Items[i - 1].Acumulado;

                item.Acumulado = acumulado + item.Pendiente;
                item.Vinculado = (item.Asignado == 0) ? moleQule.Library.Store.Resources.Labels.SET_PAGO : moleQule.Library.Store.Resources.Labels.RESET_PAGO;
            }
        }

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private Loans() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static Loans NewList() 
		{ 	
			if (!Loan.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new Loans(); 
		}
	
		public static Loans GetList() 
		{
			return GetList(Loans.SELECT());
		}
		public static Loans GetList(QueryConditions conditions)
		{
			return GetList(Loans.SELECT(conditions));
		}
        public static Loans GetList(QueryConditions conditions, int sessionCode)
        {
            return GetList(Loans.SELECT(conditions), sessionCode);
        }
		
		private static Loans GetList(string query)
		{
            if (!Loan.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Loan.GetCriteria(Loan.OpenSession());
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;
				
			Loan.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<Loans>(criteria);
		}

        public static Loans GetList(string query, int sessionCode)
        {
            if (!Loan.CanEditObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Loan.GetCriteria(sessionCode);

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

            Loan.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<Loans>(criteria);
        }

        public static Loans GetChildList(int sessionCode, List<long> oid_list, bool childs)
        {
            return GetChildList(sessionCode, Loans.SELECT(new QueryConditions { OidList = oid_list }), childs);
        }
        internal static Loans GetChildList(int sessionCode, string query, bool childs)
        {
            if (!Loan.CanEditObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = OutputInvoice.GetCriteria(sessionCode);
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

            return DataPortal.Fetch<Loans>(criteria);
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
                    Loan.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Loan.GetChild(SessionCode, reader));
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

        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Loan obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (Loan obj in this)
				{
					if (obj.IsNew)
						obj.Insert(this);
					else
						obj.Update(this);
				}

                if (!SaveAsChildList) Transaction().Commit();
            }
            catch (Exception ex)
            {
                if (!SaveAsChildList) if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                if (!SaveAsChildList) BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion		
			
        #region SQL

        public static string SELECT() { return Loan.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Loan.SELECT(conditions, true); }
			
		#endregion
    }
}