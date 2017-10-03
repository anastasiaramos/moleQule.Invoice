using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class CobroREAMap : ClassMapping<CobroREARecord>
	{
		public CobroREAMap()
		{
			Table("`CobroREA`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CobroREA_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidCobro, map => { map.Column("`OID_COBRO`"); map.Length(32768); });
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.Length(32768); });
			Property(x => x.Cantidad, map => { map.Column("`CANTIDAD`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidExpedienteRea, map => { map.Column("`OID_EXPEDIENTE_REA`"); map.NotNullable(false); map.Length(32768); });
            Property(x => x.FechaAsignacion, map => { map.Column("`FECHA_ASIGNACION`"); map.NotNullable(true); map.Length(32768); });
		}
	}
}

