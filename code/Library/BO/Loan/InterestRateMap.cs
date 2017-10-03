using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{  
	[Serializable()]
	public class InterestRateMap : ClassMapping<InterestRateRecord>
	{
		public InterestRateMap()
		{
			Table("`IVInterestRate`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVInterestRate_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidPrestamo, map => { map.Column("`OID_PRESTAMO`"); map.Length(32768); });
			Property(x => x.TipoInteres, map => { map.Column("`TIPO_INTERES`"); map.Length(32768); });
			Property(x => x.FechaInicio, map => { map.Column("`FECHA_INICIO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaFin, map => { map.Column("`FECHA_FIN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.ImporteCuota, map => { map.Column("`IMPORTE_CUOTA`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}