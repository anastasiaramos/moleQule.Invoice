using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class ClientProductMap : ClassMapping<ClientProductRecord>
	{
		public ClientProductMap()
		{
			Table("`IVClient_Product`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVClient_Product_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidProducto, map => { map.Column("`OID_PRODUCTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidCliente, map => { map.Column("`OID_CLIENTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Precio, map => { map.Column("`PRECIO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FacturacionBulto, map => { map.Column("`FACTURACION_BULTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PDescuento, map => { map.Column("`P_DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TipoDescuento, map => { map.Column("`TIPO_DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PrecioCompra, map => { map.Column("`PRECIO_COMPRA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Facturar, map => { map.Column("`FACTURAR`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaValidez, map => { map.Column("`FECHA_VALIDEZ`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

