using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class OutputInvoiceLinePrint : OutputInvoiceLineInfo
    {              
        #region Properties

		#endregion

        #region Factory Methods

        protected OutputInvoiceLinePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static OutputInvoiceLinePrint New(OutputInvoiceLineInfo source)
        {
            OutputInvoiceLinePrint item = new OutputInvoiceLinePrint();
            item.Base.CopyValues(source);

            return item;
        }

        #endregion
    }

    /* DEPRECATED */
    [Serializable()]
    public class ConceptoFacturaPrint : OutputInvoiceLinePrint
    {
        #region Properties

        public Decimal Cantidad { get { return _base.Record.CantidadKilos; } }
        public string CodigoExpediente { get { return Expediente; } }

        #endregion

        #region Factory Methods

        private ConceptoFacturaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static ConceptoFacturaPrint New(OutputInvoiceLineInfo source)
        {
            ConceptoFacturaPrint item = new ConceptoFacturaPrint();
            item.Base.CopyValues(source);

            return item;
        }

        #endregion
    }
}
