using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class BudgetLineMap : ClassMapping<BudgetLineRecord>
	{
		public BudgetLineMap()
		{
			Table("`IVBudgetLine`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVBudgetLine_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidProforma, map => { map.Column("`OID_PROFORMA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidPartida, map => { map.Column("`OID_BATCH`"); map.Length(32768); });
			Property(x => x.OidExpediente, map => { map.Column("`OID_EXPEDIENTE`"); map.Length(32768); });
			Property(x => x.OidProducto, map => { map.Column("`OID_PRODUCTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Concepto, map => { map.Column("`CONCEPTO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.FacturacionBulto, map => { map.Column("`FACTURACION_BULTO`"); map.Length(32768); });
			Property(x => x.CantidadKilos, map => { map.Column("`CANTIDAD`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CantidadBultos, map => { map.Column("`CANTIDAD_BULTOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PImpuestos, map => { map.Column("`P_IMPUESTOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PDescuento, map => { map.Column("`P_DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Precio, map => { map.Column("`PRECIO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Subtotal, map => { map.Column("`SUBTOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidImpuesto, map => { map.Column("`OID_IMPUESTO`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}