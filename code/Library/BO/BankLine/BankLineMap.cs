using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class BankLineMap : ClassMapping<BankLineRecord>
	{
		public BankLineMap()
		{
			Table("`IVBankLine`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVBankLine_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidOperacion, map => { map.Column("`OID_OPERACION`"); map.Length(32768); });
			Property(x => x.TipoOperacion, map => { map.Column("`TIPO_OPERACION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Auditado, map => { map.Column("`AUDITADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaOperacion, map => { map.Column("`FECHA_OPERACION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.IDOperacion, map => { map.Column("`ID_OPERACION`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Importe, map => { map.Column("`IMPORTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TipoCuenta, map => { map.Column("`TIPO_CUENTA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidCuentaMov, map => { map.Column("`OID_CUENTA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaCreacion, map => { map.Column("`FECHA_CREACION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TipoMovimiento, map => { map.Column("`TIPO_MOVIMIENTO`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

