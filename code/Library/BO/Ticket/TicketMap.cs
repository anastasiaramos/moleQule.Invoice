using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class TicketMap : ClassMapping<TicketRecord>
	{
		public TicketMap()
		{
			Table("`IVTicket`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVTicket_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidSerie, map => { map.Column("`OID_SERIE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidTpv, map => { map.Column("`OID_TPV`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.BaseImponible, map => { map.Column("`BASE_IMPONIBLE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Impuestos, map => { map.Column("`IMPUESTOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descuento, map => { map.Column("`DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FormaPago, map => { map.Column("`FORMA_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.DiasPago, map => { map.Column("`DIAS_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.MedioPago, map => { map.Column("`MEDIO_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PrevisionPago, map => { map.Column("`PREVISION_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Albaranes, map => { map.Column("`ALBARANES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

