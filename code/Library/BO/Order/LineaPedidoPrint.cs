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
    public class LineaPedidoPrint : LineaPedidoInfo
    {
        #region Attributes & Properties

        public Decimal Cantidad { get { return _base.Record.CantidadKilos; } }

        #endregion
		
		#region Business Methods

		protected void CopyValues(LineaPedidoInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
		}

        #endregion

        #region Factory Methods

        private LineaPedidoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static LineaPedidoPrint New(LineaPedidoInfo source)
        {
            LineaPedidoPrint item = new LineaPedidoPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}
