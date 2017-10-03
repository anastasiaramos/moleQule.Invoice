using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class OutputDeliveryLinePrint : OutputDeliveryLineInfo
    {              
        #region Business Methods

        public new string Concepto
        {
            get { return _base.Record.Concepto; }
            set { _base.Record.Concepto = value; }
        }
		public string CodigoExpediente { get { return Expediente; } }

        #endregion

        #region Factory Methods

        protected OutputDeliveryLinePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static OutputDeliveryLinePrint New(OutputDeliveryLineInfo source)
        {
            OutputDeliveryLinePrint item = new OutputDeliveryLinePrint();
            item.Base.CopyValues(source);

            return item;
        }

        #endregion
    }

    /* DEPRECATED */
    [Serializable()]
    public class ConceptoAlbaranPrint : OutputDeliveryLinePrint
    {
        #region Factory Methods

        private ConceptoAlbaranPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static ConceptoAlbaranPrint New(OutputDeliveryLineInfo source)
        {
            ConceptoAlbaranPrint item = new ConceptoAlbaranPrint();
            item.Base.CopyValues(source);

            return item;
        }

        #endregion
    }
}
