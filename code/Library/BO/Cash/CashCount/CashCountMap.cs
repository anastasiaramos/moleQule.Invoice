using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class CashCountMap : ClassMapping<CashCountRecord>
	{
		public CashCountMap()
		{
			Table("`IVCashCount`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVCashCount_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidCaja, map => { map.Column("`OID_CAJA`"); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Debe, map => { map.Column("`DEBE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Haber, map => { map.Column("`HABER`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

