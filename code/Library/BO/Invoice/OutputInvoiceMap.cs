using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{	[Serializable()]
	public class OutputInvoiceMap : ClassMapping<OutputInvoiceRecord>
	{
		public OutputInvoiceMap()
		{
			Table("`IVInvoice`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVInvoice_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidSerie, map => { map.Column("`OID_SERIE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidCliente, map => { map.Column("`OID_CLIENTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidTransportista, map => { map.Column("`OID_TRANSPORTISTA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.VatNumber, map => { map.Column("`VAT_NUMBER`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Cliente, map => { map.Column("`CLIENTE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Direccion, map => { map.Column("`DIRECCION`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CodigoPostal, map => { map.Column("`CODIGO_POSTAL`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Provincia, map => { map.Column("`PROVINCIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Municipio, map => { map.Column("`MUNICIPIO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Ano, map => { map.Column("`ANO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FormaPago, map => { map.Column("`FORMA_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.DiasPago, map => { map.Column("`DIAS_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.MedioPago, map => { map.Column("`MEDIO_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Prevision, map => { map.Column("`PREVISION_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.BaseImponible, map => { map.Column("`BASE_IMPONIBLE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Impuestos, map => { map.Column("`P_IGIC`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PDescuento, map => { map.Column("`P_DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descuento, map => { map.Column("`DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Total, map => { map.Column("`TOTAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CuentaBancaria, map => { map.Column("`CUENTA_BANCARIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Nota, map => { map.Column("`NOTA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Agrupada, map => { map.Column("`ALBARAN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Rectificativa, map => { map.Column("`RECTIFICATIVA`");  map.NotNullable(false); map.Length(32768); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PIrpf, map => { map.Column("`P_IRPF`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Albaranes, map => { map.Column("`ALBARANES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.IdMovContable, map => { map.Column("`ID_MOV_CONTABLE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.EstadoCobro, map => { map.Column("`ESTADO_COBRO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

