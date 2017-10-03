using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class TransactionMap : ClassMapping<TransactionRecord>
	{
		public TransactionMap()
		{
			Table("`IVTransaction`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVTransaction_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidEntity, map => { map.Column("`OID_ENTITY`"); map.Length(32768); });
			Property(x => x.EntityType, map => { map.Column("`ENTITY_TYPE`"); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Code, map => { map.Column("`CODE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Type, map => { map.Column("`TYPE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Status, map => { map.Column("`STATUS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Created, map => { map.Column("`CREATED`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Resolved, map => { map.Column("`RESOLVED`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TransactionID, map => { map.Column("`TRANSACTIONID`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.TransactionIDExt, map => { map.Column("`TRANSACTIONID_EXT`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.AuthCode, map => { map.Column("`AUTH_CODE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.PanMask, map => { map.Column("`PAN_MASK`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Amount, map => { map.Column("`AMOUNT`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Currency, map => { map.Column("`CURRENCY`"); map.Length(32768); });
			Property(x => x.Gateway, map => { map.Column("`GATEWAY`"); map.Length(32768); });
			Property(x => x.Description, map => { map.Column("`DESCRIPTION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Response, map => { map.Column("`RESPONSE`"); map.NotNullable(false); map.Length(32768);	});
		}
	}
}
