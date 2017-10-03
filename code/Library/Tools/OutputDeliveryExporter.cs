using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    public class OutputDeliveryExporter: ExporterBase, IExporter
    {
        #region Attributes & Properties

        #endregion

        #region Factory Methods

		public OutputDeliveryExporter() { }

        #endregion

        #region Business Methods

		public override void Export()
		{
			switch (_config.DestinationEntityType)
			{
				case ETipoEntidad.InputDelivery:
					ExportToInputDelivery();
					break;

				case ETipoEntidad.OutputDelivery:
					ExportToOutputDelivery();
					break;
			}
		}

		private void ExportToInputDelivery()
		{
			InputDeliveries in_deliveries = null;
			ISchemaInfo current_schema = AppContext.ActiveSchema;

			try
			{
				List<OutputDeliveryInfo> list = _config.SourceEntityList as List<OutputDeliveryInfo>;

				List<long> oids = new List<long>
									(
										from item in list
										select (item as OutputDeliveryInfo).Oid
									);

				OutputDeliveryList out_deliveries = OutputDeliveryList.GetList(oids, true);
				ProductList source_products = ProductList.GetList(false);

				string fromCurrencyIso = (AppContext.ActiveSchema as CompanyInfo).CurrencyIso;
				string fromCurrency = (AppContext.ActiveSchema as CompanyInfo).Currency;

				//Move to new schema to create destination entities

				AppContext.Principal.ChangeUserSchema(_config.DestinationCompany);

				string toCurrencyIso = (AppContext.ActiveSchema as CompanyInfo).CurrencyIso;
				string toCurrency = (AppContext.ActiveSchema as CompanyInfo).Currency;
								
				decimal currencyRate = 1;
				
				if (fromCurrencyIso != toCurrencyIso) currencyRate = CurrencyExchange.GetCurrencyRate(fromCurrencyIso, toCurrencyIso);

				if (currencyRate == 0)
					throw new iQException(string.Format(Resources.Messages.NO_CURRENCY_EXCHANGE_RATE, fromCurrency, toCurrency), new object[2] { ETipoEntidad.CurrencyExchange, 0 });

				in_deliveries = InputDeliveries.NewList();
				ProductList dest_products = ProductList.GetList(false);

				foreach (OutputDeliveryInfo item in out_deliveries)
				{
					InputDelivery delivery = in_deliveries.NewItem();
					delivery.CopyFrom(item, _config.DestinationHolder as IAcreedorInfo);

					foreach (OutputDeliveryLineInfo line in item.Conceptos)
					{
						ProductInfo source_product = source_products.GetItem(line.OidProducto);

						if (string.IsNullOrEmpty(source_product.ExternalCode))
							throw new iQException(string.Format(Resources.Messages.NO_PRODUCT_EXTERNAL_CODE, source_product.Codigo, source_product.Nombre), new object[2] { ETipoEntidad.Producto, source_product.Oid });

						List<ProductInfo> dest_product = new List<ProductInfo>
																(
																	from p in dest_products
																	where p.ExternalCode == source_product.ExternalCode
																	select p
																);

						if (dest_product.Count() == 0)
							throw new iQException(string.Format(Resources.Messages.PRODUCT_EXTERNAL_CODE_MISSMATCH, source_product.Codigo, source_product.Nombre), new object[2] { ETipoEntidad.Producto, source_product.Oid });

						delivery.Conceptos.NewItem(delivery, line, dest_product[0], currencyRate);
					}

					delivery.CalculateTotal();
				}

				in_deliveries.Save();
			}
			catch (Exception ex)
			{
				if (in_deliveries != null) in_deliveries.CloseSession();
				throw ex;
			}
			finally
			{
				//Move back to  active schema
				AppContext.Principal.ChangeUserSchema(current_schema);
			}
		}

		private void ExportToOutputDelivery()
		{
		}

		#endregion
    }
}
