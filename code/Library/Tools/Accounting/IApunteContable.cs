using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace moleQule.Library.Invoice
{
    public interface IApunteContable
    {
        DateTime Fecha { get; set; }
        string CuentaContable { get; set; }
        string CuentaContableContrapartida { get; set; }
        string Descripcion { get; set; }
        string Titular { get; set; }
        string Vat { get; set; }
        string NFactura { get; set; }
        DateTime FechaFactura { get; set; }
        Decimal Importe { get; set; }
        string Documento { get; set; }

        Decimal BaseImponible { get; set; }
        Decimal Total { get; set; }
        ETipoImpuestoApunte TipoImpuesto { get; set; }
        Decimal Porcentaje { get; set; }
    }
}
