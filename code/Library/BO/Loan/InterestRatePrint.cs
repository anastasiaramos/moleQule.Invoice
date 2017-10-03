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
    public class TipoInteresPrint : InterestRateInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(InterestRateInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private TipoInteresPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static TipoInteresPrint New(InterestRateInfo source)
        {
            TipoInteresPrint item = new TipoInteresPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}