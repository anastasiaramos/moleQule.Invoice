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
    public class TraspasoPrint : TraspasoInfo
    {

        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(TraspasoInfo source)
        {
            if (source == null) return;

           Oid = source.Oid;
		   _base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        private TraspasoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static TraspasoPrint New(TraspasoInfo source)
        {
            TraspasoPrint item = new TraspasoPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
