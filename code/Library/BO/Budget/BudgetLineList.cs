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
	public class BudgetLineList : ReadOnlyListBaseEx<BudgetLineList, BudgetLineInfo>
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
		private BudgetLineList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private BudgetLineList(IList<BudgetLine> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private BudgetLineList(IDataReader reader, bool childs)
        {
			Childs = childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private BudgetLineList(IList<BudgetLineInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Child Factory Methods
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static BudgetLineList GetChildList(IList<BudgetLine> list) { return new BudgetLineList(list, false); }

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
		/// <param name="childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		public static BudgetLineList GetChildList(IList<BudgetLine> list, bool childs) { return new BudgetLineList(list, childs); }

		/// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static BudgetLineList GetChildList(IDataReader reader) { return new BudgetLineList(reader, false); } 
		
		/// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <param name="childs">Flag para indicar si quiere obtener los hijos</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        public static BudgetLineList GetChildList(IDataReader reader, bool childs) { return new BudgetLineList(reader, childs); }

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static BudgetLineList GetChildList(IList<BudgetLineInfo> list) { return new BudgetLineList(list, false); }
		
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<BudgetLine> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (BudgetLine item in lista)
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
                this.AddItem(BudgetLineInfo.GetChild(reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion
		
		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return BudgetLine.SELECT(conditions, false); }
		public static string SELECT(BudgetInfo proforma) { return SELECT(new QueryConditions { Proforma = proforma }); }

		#endregion
	}
}