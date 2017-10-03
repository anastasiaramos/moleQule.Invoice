using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class BankTransferMap : ClassMapping<BankTransferRecord>
	{
		public BankTransferMap()
		{
			Table("`IVBankTransfer`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVBankTransfer_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidCuentaOrigen, map => { map.Column("`OID_CUENTA_ORIGEN`"); map.Length(32768); });
			Property(x => x.OidCuentaDestino, map => { map.Column("`OID_CUENTA_DESTINO`"); map.Length(32768); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Importe, map => { map.Column("`IMPORTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TipoMovimiento, map => { map.Column("`TIPO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.GastosBancarios, map => { map.Column("`GASTOS_BANCARIOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaRecepcion, map => { map.Column("`FECHA_RECEPCION`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}
