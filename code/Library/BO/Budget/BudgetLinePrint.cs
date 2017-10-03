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
    public class BudgetLinePrint : BudgetLineInfo
    {	
		#region Business Methods

        #endregion

        #region Factory Methods

        protected BudgetLinePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static BudgetLinePrint New(BudgetLineInfo source)
        {
            BudgetLinePrint item = new BudgetLinePrint();
            item.Base.CopyValues(source);

            return item;
        }

        #endregion
    }

    /* DEPRECATED */
    [Serializable()]
    public class ConceptoProformaPrint : BudgetLinePrint
    {
        #region Factory Methods

        private ConceptoProformaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static ConceptoProformaPrint New(BudgetLineInfo source)
        {
            ConceptoProformaPrint item = new ConceptoProformaPrint();
            item.Base.CopyValues(source);

            return item;
        }

        #endregion
    }
}
