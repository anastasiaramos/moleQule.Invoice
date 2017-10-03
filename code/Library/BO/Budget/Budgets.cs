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
	/// </summary>
    [Serializable()]
    public class Budgets : BusinessListBaseEx<Budgets, Budget>
    {		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private Budgets() { }

		#endregion				
			
        #region SQL

        public static string SELECT() { return Budget.SELECT(new QueryConditions(), true); }
		
		#endregion
    }
}