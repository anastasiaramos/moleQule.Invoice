using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using CslaEx;

using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class CobroREAPrint : CobroREAInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

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
