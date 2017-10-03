using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class CobroPrint : ChargeInfo
	{
		#region Attributes & Properties

		protected decimal _limite_credito;
        protected decimal _total_facturado;
        protected decimal _total_cobrado;
        protected decimal _pendiente_vencimiento;
        protected long _dias_pago;
		private QRCodePrint _qr_code_print = new QRCodePrint();

        public string IDCobroS { get { return _base.Record.IdCobro.ToString(Resources.Defaults.COBRO_CODE_FORMAT); } }
        public decimal PendienteVencimiento { get { return _pendiente_vencimiento; } }
        public decimal TotalFacturado { get { return _total_facturado; } }
        public decimal TotalCobrado { get { return _total_cobrado; } }
        public decimal LimiteCredito { get { return _limite_credito; } }
        public long DiasPago { get { return _dias_pago; } }
		public string EEstadoPrintLabel{ get { return Common.EnumText<EEstado>.GetPrintLabel(EEstado); } }
		public string EMedioPagoPrintLabel { get { return Common.EnumText<EMedioPago>.GetPrintLabel(EMedioPago); } }
		public string ETipoCobroPrintLabel { get { return EnumText<ETipoCobro>.GetPrintLabel(ETipoCobro); } }
		public byte[] QRCode { get { return _qr_code_print.QRCode; } }
		public bool Cobrado { get { return EEstadoCobro == EEstado.Charged; } }

		#endregion

		#region Business Methods
		
		protected void CopyValues(ChargeInfo source, bool get_QRCode)
        {
            if (source == null) return;

			_base.CopyValues(source);

			/*
			_base.Pendiente = (source.PendienteAsignacion > 0) ? source.PendienteAsignacion : source.PendienteAsignacionREA;
			*/

			_total_cobrado = _base.Record.Importe - _base.Pendiente;
			_base.CuentaBancaria = source.Entidad != string.Empty || source.CuentaBancaria != string.Empty 
										? source.Entidad + Environment.NewLine + source.CuentaBancaria 
										: string.Empty;
			_pendiente_vencimiento = source.EfectoPendienteVto;
			_base.GastosDemora = source.GastosDemora;

			if (get_QRCode)
			{
				_cobro_facturas = source.CobroFacturas;
				_qr_code_print.LoadQRCode(_qr_code_print.Encode(QREncodeVersion.v1, ETipoEntidad.Cobro, this), QRCodeVersion.v15);
			}
        }

        #endregion

        #region Factory Methods

        private CobroPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static CobroPrint New(ChargeInfo source, bool get_QRCode)
        {
            CobroPrint item = new CobroPrint();
            item.CopyValues(source, get_QRCode);

            return item;
        }

        #endregion
    }
}
