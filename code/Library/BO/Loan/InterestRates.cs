using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class InterestRates : BusinessListBaseEx<InterestRates, InterestRate>
    {
		#region Business Methods

		
		/// <summary>
		/// Crea un nuevo elemento y lo añade a la lista
		/// </summary>
		/// <returns>Nuevo item</returns>
		public InterestRate NewItem(Loan parent)
		{
			this.NewItem(InterestRate.NewChild(parent));
			InterestRate item = this[Count - 1];
			return item;
		}
		
		
		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private InterestRates() { }

		#endregion		
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private InterestRates(IList<InterestRate> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <param name="retrieve_childs">Flag para indicar si quiere obtener los hijos</param>
		
        private InterestRates(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
		
		
		/// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static InterestRates NewChildList() 
        { 
            InterestRates list = new InterestRates(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static InterestRates GetChildList(IList<InterestRate> lista) { return new InterestRates(lista); }

        /// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        /// <returns>Lista creada</returns>
        /// <remarks>Obtiene los hijos</remarks>
		
        public static InterestRates GetChildList(int sessionCode,IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static InterestRates GetChildList(int sessionCode,IDataReader reader, bool childs) { return new InterestRates(sessionCode, reader, childs); }
		
		#endregion
		
		#region Child Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
        private void Fetch(IList<InterestRate> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (InterestRate item in lista)
				this.AddItem(InterestRate.GetChild(item, Childs));

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
                this.AddItem(InterestRate.GetChild(SessionCode, reader, Childs));

            this.RaiseListChangedEvents = true;
        }

		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(Loan parent)
		{
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;
				
				// update (thus deleting) any deleted child objects
				foreach (InterestRate obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (InterestRate obj in this)
				{	
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
							obj.Insert(parent);
						else
							obj.Update(parent);
					}
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
		}
		
		#endregion
			
        #region SQL

        public static string SELECT() { return InterestRate.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return InterestRate.SELECT(conditions, true); }		
		public static string SELECT(Loan parent) { return InterestRate.SELECT(new QueryConditions{ Prestamo = parent.GetInfo(false) }, true); }
			
		#endregion
    }
}