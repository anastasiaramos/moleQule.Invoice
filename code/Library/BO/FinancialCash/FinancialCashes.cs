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
    public class FinancialCashes : BusinessListBaseEx<FinancialCashes, FinancialCash>
    {
		#region Business Methods

		public void SetMaxSerial()
		{
			foreach (FinancialCash item in this)
				if (item.Serial > _max_serial) MaxSerial = item.Serial;
		}
		
		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private FinancialCashes() { }

		#endregion				
			
        #region SQL

        public static string SELECT() { return FinancialCash.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return FinancialCash.SELECT(conditions, true); }
			
		#endregion
    }
}

