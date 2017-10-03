using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace moleQule.Library.Invoice
{
	public class PaymentGatewaySettings : ConfigurationSection
	{
		[ConfigurationProperty("GatewayName", DefaultValue = "TefPay", IsRequired = true)]
		public string GatewayName
		{
			get { return ModulePrincipal.GetPaymentGatewayName(); }
			set { ModulePrincipal.SetPaymentGatewayName(value); }
		}

		[ConfigurationProperty("GatewayCode", DefaultValue = "1", IsRequired = true)]
		public long GatewayCode
		{
			get { return ModulePrincipal.GetPaymentGatewayCode(); }
			set { ModulePrincipal.SetPaymentGatewayCode(value); }
		}

		[ConfigurationProperty("ApiHost", DefaultValue = "https://clientes.tefpay.com/index.php", IsRequired = true)]
		public string ApiHost
		{
			get { return ModulePrincipal.GetPaymentGatewayAPIHost(); }
			set { ModulePrincipal.SetPaymentGatewayAPIHost(value); }
		}

		[ConfigurationProperty("ApiOperationHost", DefaultValue = "https://secure.tef-ip.net/paywebrc16/INPUT.php", IsRequired = true)]
		public string ApiOperationHost
		{
			get { return ModulePrincipal.GetPaymentGatewayOperationHost(); }
			set { ModulePrincipal.SetPaymentGatewayOperationHost(value); }
		}

		[ConfigurationProperty("MerchantKey", DefaultValue = "")]
		public string MerchantKey
		{
			get { return ModulePrincipal.GetPaymentGatewaySignatureKey(); }
			set { ModulePrincipal.SetPaymentGatewaySignatureKey(value); }
		}

		[ConfigurationProperty("MerchantName", DefaultValue = "")]
		public string MerchantName
		{
			get { return ModulePrincipal.GetPaymentGatewayMerchantName(); }
			set { ModulePrincipal.SetPaymentGatewayMerchantName(value); }
		}

		[ConfigurationProperty("MerchantCode", DefaultValue = "")]
		public string MerchantCode
		{
			get { return ModulePrincipal.GetPaymentGatewayMerchantCode(); }
			set { ModulePrincipal.SetPaymentGatewayMerchantCode(value); }
		}

		[ConfigurationProperty("Currency", DefaultValue = "")]
		public long Currency
		{
			get { return ModulePrincipal.GetPaymentGatewayCurrency(); }
			set { ModulePrincipal.SetPaymentGatewayCurrency(value); }
		}

		[ConfigurationProperty("RedirectionURLOK", DefaultValue = "")]
		public string RedirectionURLOK
		{
			get { return ModulePrincipal.GetPaymentGatewayOKResponseURL(); }
			set { ModulePrincipal.SetPaymentGatewayOKResponseURL(value); }
		}

		[ConfigurationProperty("RedirectionURLKO", DefaultValue = "")]
		public string RedirectionURLKO
		{
			get { return ModulePrincipal.GetPaymentGatewayKOResponseURL(); }
			set { ModulePrincipal.SetPaymentGatewayKOResponseURL(value); }
		}

		[ConfigurationProperty("Curl_CA", DefaultValue = "Resources\\cacert.pem")]
		public string Curl_CA
		{
			get { return ModulePrincipal.GetPaymentGatewayCurLCA(); }
			set { ModulePrincipal.SetPaymentGatewayCurLCA(value); }
		}
	}	

	/*public class PaymentGatewaySettings : ConfigurationSection
	{
		[ConfigurationProperty("GatewayName", DefaultValue = "TefPay", IsRequired = true)]
		public string GatewayName
		{
			get
			{
				return (string)this["GatewayName"];
			}

			set
			{
				this["GatewayName"] = value;
			}
		}

		[ConfigurationProperty("GatewayCode", DefaultValue = "1", IsRequired = true)]
		public long GatewayCode
		{
			get
			{
				return (long)this["GatewayCode"];
			}

			set
			{
				this["GatewayCode"] = value;
			}
		}

		[ConfigurationProperty("ApiHost", DefaultValue = "https://clientes.tefpay.com/index.php", IsRequired = true)]
		public string ApiHost
		{
			get
			{
				return (string)this["ApiHost"];
			}

			set
			{
				this["ApiHost"] = value;
			}
		}

		[ConfigurationProperty("ApiOperationHost", DefaultValue = "https://secure.tef-ip.net/paywebrc16/INPUT.php", IsRequired = true)]
		public string ApiOperationHost
		{
			get
			{
				return (string)this["ApiOperationHost"];
			}

			set
			{
				this["ApiOperationHost"] = value;
			}
		}

		[ConfigurationProperty("MerchantKey", DefaultValue = "qwertyasdf0popolkmnb")]
		public string MerchantKey
		{
			get
			{
				return (string)this["MerchantKey"];
			}

			set
			{
				this["MerchantKey"] = value;
			}
		}

		[ConfigurationProperty("MerchantName", DefaultValue = "interactiveRent")]
		public string MerchantName
		{
			get
			{
				return (string)this["MerchantName"];
			}

			set
			{
				this["MerchantName"] = value;
			}
		}

		[ConfigurationProperty("MerchantCode", DefaultValue = "V99000246")]
		public string MerchantCode
		{
			get
			{
				return (string)this["MerchantCode"];
			}

			set
			{
				this["MerchantCode"] = value;
			}
		}

		[ConfigurationProperty("Currency", DefaultValue = "978")]
		public long Currency
		{
			get
			{
				return (long)this["Currency"];
			}

			set
			{
				this["Currency"] = value;
			}
		}
		
		[ConfigurationProperty("RedirectionURLOK", DefaultValue = "interactiveRent")]
		public string RedirectionURLOK
		{
			get
			{
				return (string)this["RedirectionURLOK"];
			}

			set
			{
				this["RedirectionURLOK"] = value;
			}
		}

		[ConfigurationProperty("RedirectionURLKO", DefaultValue = "interactiveRent")]
		public string RedirectionURLKO
		{
			get
			{
				return (string)this["RedirectionURLKO"];
			}

			set
			{
				this["RedirectionURLKO"] = value;
			}
		}

		[ConfigurationProperty("Curl_CA", DefaultValue = "Resources\\cacert.pem")]
		public string Curl_CA
		{
			get
			{
				return (string)this["Curl_CA"];
			}

			set
			{
				this["Curl_CA"] = value;
			}
		}
	}	*/
}
