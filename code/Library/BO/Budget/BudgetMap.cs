using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class BudgetMap : ClassMapping<BudgetRecord>
	{
		public BudgetMap()
		{
			Table("`IVBudget`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVBudget_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidSerie, map => { map.Column("`OID_SERIE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidCliente, map => { map.Column("`OID_CLIENTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Subtotal, map => { map.Column("`SUBTOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PDescuento, map => { map.Column("`P_DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Impuestos, map => { map.Column("`IMPUESTOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Nota, map => { map.Column("`NOTA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PIRPF, map => { map.Column("`P_IRPF`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}