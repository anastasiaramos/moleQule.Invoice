using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class OutputDeliveryInvoiceMap : ClassMapping<OutputDeliveryInvoiceRecord>
	{
		public OutputDeliveryInvoiceMap()
		{
			Table("`IVDelivery_Invoice`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVDelivery_Invoice_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidAlbaran, map => { map.Column("`OID_ALBARAN`"); map.Length(32768); });
			Property(x => x.OidFactura, map => { map.Column("`OID_FACTURA`"); map.Length(32768); });
			Property(x => x.FechaAsignacion, map => { map.Column("`FECHA_ASIGNACION`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

