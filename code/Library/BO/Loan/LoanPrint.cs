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
    public class LoanPrint : LoanInfo
    {	
		#region Business Methods

        protected void CopyValues(LoanInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);				
        }

        #endregion

        #region Factory Methods

        protected LoanPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static LoanPrint New(LoanInfo source)
        {
            LoanPrint item = new LoanPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }

    [Serializable()]
    public class PrestamoPrint : LoanPrint
    {
        #region Factory Methods

        private PrestamoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static PrestamoPrint New(LoanInfo source)
        {
            PrestamoPrint item = new PrestamoPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}