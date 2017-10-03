using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class ChargeMap : ClassMapping<ChargeRecord>
	{
		public ChargeMap()
		{
			Table("`IVCharge`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVCharge_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidCliente, map => { map.Column("`OID_CLIENTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.IdCobro, map => { map.Column("`ID_COBRO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TipoCobro, map => { map.Column("`TIPO_COBRO`"); map.Length(32768); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Importe, map => { map.Column("`IMPORTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.MedioPago, map => { map.Column("`MEDIO_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Vencimiento, map => { map.Column("`VENCIMIENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidCuentaBancaria, map => { map.Column("`OID_CUENTA_BANCARIA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.OidTpv, map => { map.Column("`OID_TPV`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.EstadoCobro, map => { map.Column("`ESTADO_COBRO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.IdMovContable, map => { map.Column("`ID_MOV_CONTABLE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.GastosBancarios, map => { map.Column("`GASTOS_BANCARIOS`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

