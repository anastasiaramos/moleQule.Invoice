using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class ClientTypeMap : ClassMapping<ClientTypeRecord>
	{
		public ClientTypeMap()
		{
			Table("`IVClientType`");
			Schema("\"COMMON\"");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVClientType_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Valor, map => { map.Column("`VALOR`"); map.Length(255); });
		}
	}
}

