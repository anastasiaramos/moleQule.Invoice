using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class CashLineMap : ClassMapping<CashLineRecord>
	{
		public CashLineMap()
		{
			Table("`IVCashLine`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVCashLine_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidCaja, map => { map.Column("`OID_CAJA`"); });
			Property(x => x.OidCierre, map => { map.Column("`OID_CIERRE`"); map.NotNullable(false); });
            Property(x => x.OidLink, map => { map.Column("`OID_LINK`"); map.NotNullable(false); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.NFactura, map => { map.Column("`N_FACTURA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); });
			Property(x => x.Concepto, map => { map.Column("`CONCEPTO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.OidCobro, map => { map.Column("`OID_COBRO`"); map.NotNullable(false); });
			Property(x => x.OidPago, map => { map.Column("`OID_PAGO`"); map.NotNullable(false); });
			//Property(x => x.NCliente, map => { map.Column("`N_CLIENTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.NTercero, map => { map.Column("`N_PROVEEDOR`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Debe, map => { map.Column("`DEBE`"); map.NotNullable(false); });
			Property(x => x.Haber, map => { map.Column("`HABER`"); map.NotNullable(false); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); });
			Property(x => x.OidCuentaBancaria, map => { map.Column("`OID_CUENTA_BANCARIA`"); map.NotNullable(false); });
            Property(x => x.OidCreditCard, map => { map.Column("`OID_CREDIT_CARD`"); map.NotNullable(false); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); });
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); map.NotNullable(false); });
		}
	}
}

