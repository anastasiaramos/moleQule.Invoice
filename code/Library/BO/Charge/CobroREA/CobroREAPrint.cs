using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class CobroREAPrint : CobroREAInfo
    {
        #region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(CobroREAInfo source)
        {
            if (source == null) return;

			_base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        private CobroREAPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static CobroREAPrint New(CobroREAInfo source)
        {
            CobroREAPrint item = new CobroREAPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
