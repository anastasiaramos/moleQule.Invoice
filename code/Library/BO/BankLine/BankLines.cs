using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// Editable Business Object Root Collection
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class BankLines : BusinessListBaseEx<BankLines, BankLine>
    {		
		#region Root Business Methods
        
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public BankLine NewItem()
        {
            this.AddItem(BankLine.NewChild());
            return this[Count - 1];
        }
		
		/// <summary>
		/// Crea un nuevo elemento y lo añade a la lista
		/// </summary>
		/// <returns>Nuevo item</returns>
		public BankLine NewItem(IBankLine parent)
		{
			this.NewItem(BankLine.NewChild(parent));
			return this[Count - 1];
		}

        /// <summary>
        /// Crea un nuevo elemento y lo añade a la lista
        /// </summary>
        /// <returns>Nuevo item</returns>
        public BankLine NewItem(IBankLineInfo parent)
        {
            this.NewItem(BankLine.NewChild(parent));
            return this[Count - 1];
        }

        public BankLine NewItem(BankAccountInfo parent)
        {
            this.NewItem(BankLine.NewChild(parent));
            return this[Count - 1];
        }

        public bool Contains(EBankLineType bankLineType)
        {
            foreach (BankLine item in this)
                if (item.ETipoMovimientoBanco == bankLineType) return true;

            return false;
        }

        public bool ContainsNotNull(EBankLineType bankLineType)
        {
            return Items.FirstOrDefault(x => x.ETipoMovimientoBanco == bankLineType && x.EEstado != EEstado.Anulado) != default(BankLine);
        }

		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private BankLines() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static BankLines NewList() 
		{ 	
			if (!BankLine.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new BankLines(); 
		}

        public static BankLines GetList() { return GetList(false); }
        public static BankLines GetList(bool childs)
        {
			return GetList(new QueryConditions(), childs);
        }
		public static BankLines GetList(QueryConditions conditions, bool childs)
		{
			if (!BankLine.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = BankLine.GetCriteria(BankLine.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = BankLines.SELECT(conditions);

			BankLine.BeginTransaction(criteria.SessionCode);

			return DataPortal.Fetch<BankLines>(criteria);
		}
        public static BankLines GetList(QueryConditions conditions, bool childs, int sessionCode)
        {
            if (!BankLine.CanEditObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = BankLine.GetCriteria(sessionCode);
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = BankLines.SELECT(conditions);

            BankLine.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<BankLines>(criteria);
        }

        #endregion
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private BankLines(IList<BankLine> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <param name="childs">Flag para indicar si quiere obtener los hijos</param>
        private BankLines(IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static BankLines NewChildList() 
        { 
            BankLines list = new BankLines(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static BankLines GetChildList(IList<BankLine> lista) { return new BankLines(lista); }

        /// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        /// <returns>Lista creada</returns>
        /// <remarks>Obtiene los hijos</remarks>
        public static BankLines  GetChildList(IDataReader reader) { return GetChildList(reader, true); }

        /// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        /// <param name="childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista creada</returns>
        /// <remarks>Obtiene los hijos</remarks>
        public static BankLines GetChildList(IDataReader reader, bool childs) { return new BankLines(reader, childs); }

		#endregion
		
		#region Root Data Access

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        /// <remarks>LA UTILIZA EL DATAPORTAL</remarks>
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

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
                    BankLine.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(BankLine.GetChild(reader, Childs));
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
            foreach (BankLine obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (BankLine obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
							obj.Insert(this);
						else
							obj.Update(this);
					}
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
		
		#region Child Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
        private void Fetch(IList<BankLine> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (BankLine item in lista)
				this.AddItem(BankLine.GetChild(item, Childs));

			this.RaiseListChangedEvents = true;
		}
		
        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen con los elementos a insertar</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(BankLine.GetChild(reader, Childs));

            this.RaiseListChangedEvents = true;
        }

		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(IBankLine parent)
		{
			this.RaiseListChangedEvents = false;

            SessionCode = parent.SessionCode;
			// update (thus deleting) any deleted child objects
			foreach (BankLine obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (BankLine obj in this)
			{	
				if (!this.Contains(obj))
				{
					if (obj.IsNew)
						obj.Insert(parent);
					else
						obj.Update(parent);
				}
			}

			this.RaiseListChangedEvents = true;
		}
		
		#endregion
			
        #region SQL

        public static string SELECT() { return BankLine.SELECT(0, EBankLineType.Todos, ETipoTitular.Todos); }
        public static string SELECT(QueryConditions conditions) { return BankLine.SELECT(conditions, true); }
		
		#endregion
    }
}