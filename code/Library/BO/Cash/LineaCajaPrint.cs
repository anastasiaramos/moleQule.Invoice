using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class LineaCajaPrint : CashLineInfo
    {
        #region Attributes & Properties
		
        public string NProveedor { get { return _base.Record.NTercero; } }
		public string EEstadoPrintLabel { get { return Library.Common.EnumText<EEstado>.GetPrintLabel(EEstado); } }

		#endregion
		
		#region Business Methods

        protected void CopyValues(CashLineInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        private LineaCajaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static LineaCajaPrint New(CashLineInfo source)
        {
            LineaCajaPrint item = new LineaCajaPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}