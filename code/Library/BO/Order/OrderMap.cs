using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{	
	[Serializable()]
	public class OrderMap : ClassMapping<OrderRecord>
	{
		public OrderMap()
		{
			Table("`IVOrder`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVOrder_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidCliente, map => { map.Column("`OID_CLIENTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidProducto, map => { map.Column("`OID_PRODUCTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.Length(255); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidSerie, map => { map.Column("`OID_SERIE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.BaseImponible, map => { map.Column("`BASE_IMPONIBLE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Impuestos, map => { map.Column("`IMPUESTOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PDescuento, map => { map.Column("`P_DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descuento, map => { map.Column("`DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidAlmacen, map => { map.Column("`OID_ALMACEN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

