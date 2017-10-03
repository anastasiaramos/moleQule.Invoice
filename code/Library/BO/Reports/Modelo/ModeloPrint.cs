using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class ModeloPrint : ModeloInfo
    {
        #region Attributes

        #endregion

        #region Propiedades

        #endregion

        #region Business Methods

		protected void CopyValues(ModeloInfo source)
		{
			_modelo = source.Modelo;

			switch (EModelo)
			{
				case EModelo.Modelo347:
					{
						Oid = source.Oid;
						_tipo_titular = source.TipoTitular;
						_id_titular = source.IDTitular;
						_vat_number_titular = source.VatNumberTitular;
						_titular = source.Titular;
						_total = source.Total;
						_total_1T = source.Total1T;
						_total_2T = source.Total2T;
						_total_3T = source.Total3T;
						_total_4T = source.Total4T;
						_total_efectivo = source.TotalEfectivo;
						_total_efectivo_1T = source.TotalEfectivo1T;
						_total_efectivo_2T = source.TotalEfectivo2T;
						_total_efectivo_3T = source.TotalEfectivo3T;
						_total_efectivo_4T = source.TotalEfectivo4T;
					}
					break;

				case EModelo.Modelo420:
					{
						Oid = source.Oid;

						_total_soportado = source.TotalSoportado;
						_total_soportado_1T = source.TotalSoportado1T;
						_total_soportado_2T = source.TotalSoportado2T;
						_total_soportado_3T = source.TotalSoportado3T;
						_total_soportado_4T = source.TotalSoportado4T;

						_total_soportado_importacion = source.TotalSoportadoImportacion;
						_total_soportado_importacion_1T = source.TotalSoportadoImportacion1T;
						_total_soportado_importacion_2T = source.TotalSoportadoImportacion2T;
						_total_soportado_importacion_3T = source.TotalSoportadoImportacion3T;
						_total_soportado_importacion_4T = source.TotalSoportadoImportacion4T;

						_total_repercutido = source.TotalRepercutido;
						_total_repercutido_1T = source.TotalRepercutido1T;
						_total_repercutido_2T = source.TotalRepercutido2T;
						_total_repercutido_3T = source.TotalRepercutido3T;
						_total_repercutido_4T = source.TotalRepercutido4T;

						_total = source.TotalRepercutido - source.TotalSoportado + source.TotalSoportadoImportacion;
						_total_1T = source.TotalRepercutido1T - source.TotalSoportado1T + source.TotalSoportadoImportacion1T;
						_total_2T = source.TotalRepercutido2T - source.TotalSoportado2T + source.TotalSoportadoImportacion2T;
						_total_3T = source.TotalRepercutido3T - source.TotalSoportado3T + source.TotalSoportadoImportacion3T;
						_total_4T = source.TotalRepercutido4T - source.TotalSoportado4T + source.TotalSoportadoImportacion4T;

						_total = (_total < 0) ? 0 : _total;
						_total_1T = (_total_1T < 0) ? 0 : _total_1T;
						_total_2T = (_total_2T < 0) ? 0 : _total_2T;
						_total_3T = (_total_3T < 0) ? 0 : _total_3T;
						_total_4T = (_total_4T < 0) ? 0 : _total_4T;
					}
					break;

				case EModelo.Modelo111:
					{
						Oid = source.Oid;
						_tipo_titular = source.TipoTitular;
						_id_titular = source.IDTitular;
						_vat_number_titular = source.VatNumberTitular;
						_titular = source.Titular;

						_total = source.Total;
						_total_1T = source.Total1T;
						_total_2T = source.Total2T;
						_total_3T = source.Total3T;
						_total_4T = source.Total4T;

						_base = source.Base;
						_base_1T = source.Base1T;
						_base_2T = source.Base2T;
						_base_3T = source.Base3T;
						_base_4T = source.Base4T;
					}
					break;
			}
		}

        protected void CopyValues(OutputInvoiceInfo source)
        {
            if (source == null) return;

			_oid_titular = source.OidCliente;
			_id_titular = source.IDCliente;
			_vat_number_titular = source.VatNumber;
			_titular = source.Cliente;
			_tipo_titular = (long)ETipoTitular.Cliente;
			_total = source.Total;
			_total_efectivo = source.BaseImponible;
        }

		protected void CopyValues(InputInvoiceInfo source)
        {
            if (source == null) return;

			_oid_titular = source.OidAcreedor;
			_id_titular = source.NumeroAcreedor;
			_vat_number_titular = source.VatNumber;
			_titular = source.Acreedor;
			_tipo_titular = (long)ETipoTitular.Acreedor;
			_total = source.Total;
			_total_efectivo = source.BaseImponible;
        }

        #endregion
        
        #region Factory Methods

		private ModeloPrint() { /* require use of factory methods */ }

		public static ModeloPrint New(ModeloInfo source)
		{
			ModeloPrint item = new ModeloPrint();
			item.CopyValues(source);

			return item;
		}

        #endregion
    }
}



