using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class ChargeOperationMap : ClassMapping<ChargeOperationRecord>
	{
		public ChargeOperationMap()
		{
			Table("`IVCharge_Operation`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVCharge_Operation_OID_seq`" })); map.Column("`OID`"); });  
			Property(x => x.OidCobro, map =>    {     map.Column("`OID_COBRO`");     map.Length(32768);    });   
			Property(x => x.OidFactura, map =>    {     map.Column("`OID_FACTURA`");     map.Length(32768);    });   
			Property(x => x.Cantidad, map =>    {     map.Column("`CANTIDAD`");     map.NotNullable(false);     map.Length(32768);    });   
			Property(x => x.FechaAsignacion, map =>    {     map.Column("`FECHA_ASIGNACION`");     map.NotNullable(false);     map.Length(32768);    });
		}
	}
}

