using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class FinancialCashPrint : FinancialCashInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        #endregion

        #region Factory Methods

        private FinancialCashPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static FinancialCashPrint New(FinancialCashInfo source)
        {
            FinancialCashPrint item = new FinancialCashPrint();
            item.Base.CopyValues(source);

            return item;
        }

        #endregion
    }
}