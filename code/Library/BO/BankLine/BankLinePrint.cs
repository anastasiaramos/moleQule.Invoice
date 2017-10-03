using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class BankLinePrint : BankLineInfo
    {
        #region Attributes & Properties

		public string EEstadoPrintLabel { get { return Common.EnumText<EEstado>.GetPrintLabel(EEstado); } }
		public string EMedioPagoPrintLabel { get { return Common.EnumText<EMedioPago>.GetPrintLabel(EMedioPago); } }
		public string ETipoMovimientoPrintLabel { get { return Invoice.EnumText<EBankLineType>.GetPrintLabel(ETipoMovimientoBanco); } }
		public string ETipoTitularPrintLabel { get { return Common.EnumText<ETipoTitular>.GetPrintLabel(ETipoTitular); } }

		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(BankLineInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);

            _base._titular= source.Titular;
			_base._tipo_titular = source.TipoTitular;
			_base._medio_pago = source.MedioPago;
			_base._cuenta = source.Entidad + Environment.NewLine + source.Cuenta;
			_base._id_mov_contable = source.IDMovimientoContable;
        }

        #endregion

        #region Factory Methods

        protected BankLinePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static BankLinePrint New(BankLineInfo source)
        {
            BankLinePrint item = new BankLinePrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }

    /*DEPRECATED*/
    [Serializable()]
    public class MovimientoBancoPrint : BankLinePrint
    {
        #region Factory Methods

        private MovimientoBancoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static MovimientoBancoPrint New(BankLineInfo source)
        {
            MovimientoBancoPrint item = new MovimientoBancoPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}
