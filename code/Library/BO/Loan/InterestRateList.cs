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
	/// ReadOnly Business Object Child Collection
	/// </summary>
    [Serializable()]
	public class InterestRateList : ReadOnlyListBaseEx<InterestRateList, InterestRateInfo>
	{	
		#region Business Methods
			
		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private InterestRateList() {}
		private InterestRateList(IList<InterestRate> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private InterestRateList(IList<InterestRateInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Child Factory Methods
						
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private InterestRateList(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static InterestRateList GetChildList(IList<InterestRate> list) { return new InterestRateList(list, false); }
		public static InterestRateList GetChildList(IList<InterestRate> list, bool childs) { return new InterestRateList(list, childs); }

		/// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static InterestRateList GetChildList(int sessionCode, IDataReader reader) { return new InterestRateList(sessionCode, reader, false); } 
		public static InterestRateList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new InterestRateList(sessionCode, reader, childs); }
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static InterestRateList GetChildList(IList<InterestRateInfo> list) { return new InterestRateList(list, false); }
		
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<InterestRate> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (InterestRate item in lista)
				this.AddItem(item.GetInfo(Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(InterestRateInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion
		
        #region SQL

        public static string SELECT() { return InterestRateInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return InterestRate.SELECT(conditions, false); }
		
		public static string SELECT(LoanInfo parent) { return  InterestRate.SELECT(new QueryConditions{ Prestamo = parent }, true); }
		
		#endregion		
	}
}