using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class FinancialCashMap : ClassMapping<FinancialCashRecord>
	{	
		public FinancialCashMap()
		{
			Table("`IVEffect`");
			//Schema("``");
			Lazy(true);

            Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVEffect_OID_seq`" })); map.Column("`OID`"); });
            Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
            Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
            Property(x => x.OidCobro, map => { map.Column("`OID_COBRO`"); map.Length(32768); });
            Property(x => x.OidCuentaBancaria, map => { map.Column("`OID_CUENTA_BANCARIA`"); map.Length(32768); });
			Property(x => x.FechaEmision, map => { map.Column("`FECHA_EMISION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Vencimiento, map => { map.Column("`VENCIMIENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Negociado, map => { map.Column("`ADELANTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.GastosNegociado, map => { map.Column("`GASTOS_ADELANTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.GastosDevolucion, map => { map.Column("`GASTOS_DEVOLUCION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Gastos, map => { map.Column("`GASTOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.EstadoCobro, map => { map.Column("`ESTADO_COBRO`"); map.NotNullable(false); map.Length(32768); });
            Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.ChargeDate, map => { map.Column("`CHARGE_DATE`"); map.NotNullable(false); });
            Property(x => x.ReturnDate, map => { map.Column("`RETURN_DATE`"); map.NotNullable(false); });
		}
	}
}
