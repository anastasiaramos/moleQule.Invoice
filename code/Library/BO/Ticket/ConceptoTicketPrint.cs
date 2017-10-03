using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class ConceptoTicketPrint : ConceptoTicketInfo
    {
              
        #region Business Methods

        public new string Concepto
        {
            get { return _base.Record.Concepto; }
			set { _base.Record.Concepto = value; }
        }

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(ConceptoTicketInfo source)
        {
            if (source == null) return;
			_base.CopyValues(source);          
        }

        #endregion

        #region Factory Methods

        private ConceptoTicketPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static ConceptoTicketPrint New(ConceptoTicketInfo source)
        {
            ConceptoTicketPrint item = new ConceptoTicketPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
