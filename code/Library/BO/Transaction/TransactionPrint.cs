using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class TransactionPrint : TransactionInfo
    {

        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(TransactionInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private TransactionPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static TransactionPrint New(TransactionInfo source)
        {
            TransactionPrint item = new TransactionPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
