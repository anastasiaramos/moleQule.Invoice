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
    public class CierreCajaPrint : CierreCajaInfo
    {
        #region Attributes & Properties
		
		#endregion
		
		#region Business Methods
        protected void CopyValues(CierreCajaInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        private CierreCajaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static CierreCajaPrint New(CierreCajaInfo source)
        {
            CierreCajaPrint item = new CierreCajaPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
