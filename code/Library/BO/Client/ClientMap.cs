using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class ClientMap : ClassMapping<ClientRecord>
	{
		public ClientMap()
		{
			Table("`IVClient`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVClient_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidExt, map => { map.Column("`OID_EXT`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); });
			Property(x => x.TipoId, map => { map.Column("`TIPO_ID`"); map.NotNullable(false); });
			Property(x => x.VatNumber, map => { map.Column("`VAT_NUMBER`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.NombreComercial, map => { map.Column("`NOMBRE_COMERCIAL`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Titular, map => { map.Column("`TITULAR`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Direccion, map => { map.Column("`DIRECCION`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Poblacion, map => { map.Column("`POBLACION`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CodigoPostal, map => { map.Column("`CODIGO_POSTAL`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Provincia, map => { map.Column("`PROVINCIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Telefonos, map => { map.Column("`TELEFONOS`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Fax, map => { map.Column("`FAX`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Prefix, map => { map.Column("`PREFIX`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Movil, map => { map.Column("`MOVIL`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Municipio, map => { map.Column("`MUNICIPIO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Country, map => { map.Column("`COUNTRY`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Email, map => { map.Column("`EMAIL`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.BirthDate, map => { map.Column("`BIRTH_DATE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Historia, map => { map.Column("`HISTORIA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.LimiteCredito, map => { map.Column("`LIMITE_CREDITO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Contacto, map => { map.Column("`CONTACTO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.MedioPago, map => { map.Column("`MEDIO_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FormaPago, map => { map.Column("`FORMA_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.DiasPago, map => { map.Column("`DIAS_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CodigoExplotacion, map => { map.Column("`CODIGO_EXPLOTACION`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CuentaBancaria, map => { map.Column("`CUENTA_BANCARIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Swift, map => { map.Column("`SWIFT`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.OidCuentaBancariaAsociada, map => { map.Column("`OID_CUENTA_BANCARIA_ASOCIADA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descuento, map => { map.Column("`DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PrecioTransporte, map => { map.Column("`PRECIO_TRANSPORTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidTransporte, map => { map.Column("`OID_TRANSPORTE`"); map.Length(32768); });
			Property(x => x.CuentaContable, map => { map.Column("`CUENTA_CONTABLE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidImpuesto, map => { map.Column("`OID_IMPUESTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TipoInteres, map => { map.Column("`TIPO_INTERES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PDescuentoPtoPago, map => { map.Column("`P_DESCUENTO_PTO_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PrioridadPrecio, map => { map.Column("`PRIORIDAD_PRECIO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.EnviarFacturaPendiente, map => { map.Column("`ENVIAR_FACTURA_PENDIENTE`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}