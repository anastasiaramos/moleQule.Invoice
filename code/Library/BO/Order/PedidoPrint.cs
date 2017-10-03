using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using moleQule.Library;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class PedidoPrint : PedidoInfo
    {

        #region Attributes & Properties
		
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(PedidoInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        private PedidoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static PedidoPrint New(PedidoInfo source)
        {
            PedidoPrint item = new PedidoPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
