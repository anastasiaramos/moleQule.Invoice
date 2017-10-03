using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class LoanMap : ClassMapping<LoanRecord>
	{
		public LoanMap()
		{
			Table("`IVLoan`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`IVLoan_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidCuenta, map => { map.Column("`OID_CUENTA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaFirma, map => { map.Column("`FECHA_FIRMA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaIngreso, map => { map.Column("`FECHA_INGRESO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaVencimiento, map => { map.Column("`FECHA_VENCIMIENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Importe, map => { map.Column("`IMPORTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.OidPago, map => { map.Column("`OID_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.NCuotas, map => { map.Column("`N_CUOTAS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.InicioPago, map => { map.Column("`INICIO_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PeriodoPago, map => { map.Column("`PERIODO_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.ImporteCuota, map => { map.Column("`IMPORTE_CUOTA`"); map.NotNullable(false); map.Length(32768); });
            Property(x => x.CuentaContable, map => { map.Column("`CUENTA_CONTABLE`"); map.NotNullable(false); map.Length(255); });
            Property(x => x.GastosBancarios, map => { map.Column("`GASTOS_BANCARIOS`"); map.NotNullable(false); map.Length(32768); });
            Property(x => x.GastosInicio, map => { map.Column("`GASTOS_INICIO`"); map.NotNullable(false); map.Length(32768); });
            Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}
