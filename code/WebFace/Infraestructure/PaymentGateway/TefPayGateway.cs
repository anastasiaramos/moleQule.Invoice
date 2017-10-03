using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Renting;
using moleQule.WebFace;
using moleQule.WebFace.Helpers;
using moleQule.WebFace.Invoice.Models;

namespace moleQule.WebFace.Invoice
{
	public class TefPayResponse : PaymentResponse
	{		
	}

	public class TefPayGateway : IPaymentGateway
	{
		const string DATE_FORMAT = "dd/MM/yyyy";
		const string GATEWAY_DATE_FORMAT = "yyMMddHHmmss"; 

		public PaymentGatewaySettings Settings { get; set; }

		public TefPayGateway(PaymentGatewaySettings settings)
		{
			Settings = settings;
		}

		//Obtains the TransactionID from the TransactionIDExt
		public string GetTransactionID(TransactionInfo transaction)
		{
			return transaction.TransactionIDExt.Substring(0, transaction.TransactionIDExt.Length - 3).PadRight(21, '0');
		}

		public ECurrency GetCurrency() { return (ECurrency)Settings.Currency; }
		public string GetCurrency(ECurrency currency)
		{
			switch (currency)
			{
				case ECurrency.Euro: return "978";
				default: return "978";
			}
		}
		public string GetDescription(Transaction transaction, PlanInfo plan, SubscriptionInfo subscription)
		{
			switch (transaction.ETransactionType)
			{
				case ETransactionType.Authentication:
					return String.Format(WebFace.Invoice.Resources.Messages.AUTHENTICATION_DESCRIPTION,
										plan.Name,
										subscription.From.ToString(DATE_FORMAT),
										subscription.Till.ToString(DATE_FORMAT));

				case ETransactionType.Preauthorization:
					return String.Format(WebFace.Invoice.Resources.Messages.PREAUTHORIZATION_DESCRIPTION,
										plan.Name,
										subscription.From.ToString(DATE_FORMAT),
										subscription.Till.ToString(DATE_FORMAT));

				case ETransactionType.PreauthCharge:
					return String.Format(WebFace.Invoice.Resources.Messages.PREAUTHCHARGE_DESCRIPTION,
										plan.Name,
										subscription.From.ToString(DATE_FORMAT),
										subscription.Till.ToString(DATE_FORMAT));

				case ETransactionType.PreauthCancelation:
					return String.Format(WebFace.Invoice.Resources.Messages.PREAUTHCANCELATION_DESCRIPTION,
										plan.Name,
										subscription.From.ToString(DATE_FORMAT),
										subscription.Till.ToString(DATE_FORMAT));

				default: return string.Empty;
			}
		}
		public string GetMessage(HttpRequestBase request)
		{
			string message = string.Empty;

			switch (request["Ds_Code"])
			{
				//OK CODES
				case "100": message = Resources.Labels.PAYMENT_ACCEPTED; break;
				case "102": message = Resources.Labels.PAYMENT_ACCEPTED; break;
			
				//ERROR CODES
				case "200": message = Resources.Labels.PAYMENT_INACCESIBLE_BANK; break;
				case "201": message = Resources.Labels.PAYMENT_DENIED; break;
				case "202": message = Resources.Labels.PAYMENT_AUTH_ERROR; break;
				case "203": message = Resources.Labels.PAYMENT_SYSTEM_ERROR; break;
				case "204": message = Resources.Labels.PAYMENT_AUTH_ERROR; break;
				case "205": message = Resources.Labels.PAYMENT_EXPIRED_CARD; break;
				case "206": message = Resources.Labels.PAYMENT_INVALID_CARD; break;
				case "207": message = Resources.Labels.PAYMENT_UNKOWN_ERROR; break;
				case "208": message = Resources.Labels.PAYMENT_USER_CANCELED; break;

				case "302": message = String.Format(Resources.Labels.PAYMENT_TRANSACTION_NOT_FOUND, request["Ds_Merchant_MatchingData"]); break;

				default: message = request["DS_Message"]; break;
			}
		
			return message;
		}
		public string GetResponseOKURL(HttpRequestBase request)
		{
			return request.Url.GetLeftPart(UriPartial.Authority) + Settings.RedirectionURLOK;
		}
		public string GetResponseKOURL(HttpRequestBase request)
		{
			return request.Url.GetLeftPart(UriPartial.Authority) + Settings.RedirectionURLKO;
		}		
		public string GetSignature(TransactionViewModel transaction)
		{
			return GetSignature(transaction, Settings.MerchantCode, Settings.MerchantKey);
		}
		public string GetSignature(TransactionViewModel transaction, string merchantCode, string merchantKey)
		{
			string token = transaction.Amount.ToString("N2").Replace(".", string.Empty) +
							merchantCode +
							transaction.TransactionIDExt +
							merchantKey;

			string sha2 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(token, FormsAuthPasswordFormat.SHA1.ToString());
			return sha2.ToLower();
		}
		public string GetType(ETransactionType type)
		{
			switch (type)
			{
				case ETransactionType.Authentication: return "3";
				case ETransactionType.Preauthorization: return "16";
				case ETransactionType.PreauthCharge: return "7";
				case ETransactionType.PreauthCancelation: return "18";
				default: return string.Empty;
			}
		}

		private string GetSHA1HashData(string data)
		{
			//create new instance of md5
			SHA1 sha1 = SHA1.Create();

			//convert the input text to array of bytes
			byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

			//create new instance of StringBuilder to save hashed data
			StringBuilder returnValue = new StringBuilder();

			//loop for each byte and add it to StringBuilder
			for (int i = 0; i < hashData.Length; i++)
			{
				returnValue.Append(hashData[i].ToString());
			}

			// return hexadecimal string
			return returnValue.ToString();
		}

		public string NewTransactionID() { return DateTime.Now.ToString("yyyyMMddHHmmss").PadRight(21, '0'); /*201304290404120000000*/ }

		public PaymentResponse ParseHtmlResponse(HttpRequestBase request)
		{
			if (request == null) return null;

			PaymentResponse response = new PaymentResponse();
			response.Transaction = TransactionInfo.New();
			
			StreamReader strReader = new StreamReader(request.InputStream);
			response.Transaction.Response = strReader.ReadToEnd();
			strReader.Close();

			response.Transaction.Amount = Convert.ToDecimal(request["Ds_Amount"]);
			//Tefpay uses amount without . or , that's why we have to divide it by 100
			response.Transaction.Amount = response.Transaction.Amount / 100;
			response.Transaction.TransactionIDExt = request["Ds_Merchant_MatchingData"].ToString();

			string merchantCode = request["Ds_Merchant_MerchantCode"].ToString();

			//TOKEN CHECK
			string url = (Convert.ToInt32(request["Ds_Code"]) < 200) ? GetResponseOKURL(request) : GetResponseKOURL(request);
		
			string signature =  GetSignature(TransactionViewModel.New(response.Transaction), merchantCode, Settings.MerchantKey);

			/*TODO: Check TefPay response signature
			 * if (signature != request["Ds_Signature"]) {	
				throw new Exception("TOKEN DOES NOT MISMATCH!");			
			}*/

			response.Transaction.Resolved = DateTime.ParseExact(request["Ds_Date"], GATEWAY_DATE_FORMAT, CultureInfo.InvariantCulture);
			response.Transaction.AuthCode = request["Ds_AuthorisationCode"].ToString();
			response.Transaction.PanMask = request["Ds_PanMask"].ToString();

			response.Message = GetMessage(request);
			response.ResponseCode = request["Ds_Code"].ToString();

			response.Status = (response.ResponseCode == "100") ? EStatus.OK : EStatus.Error;

			return response;
		}
		public PaymentResponse ParseXMLResponse(HttpWebResponse response)
		{
			TefPayResponse resp = new TefPayResponse();
			resp.Transaction = TransactionInfo.New();
			Stream responsestream = null;

			//If the content is compress by gzip - CompressEnconding gzip is set before in the request
			if (response.ContentEncoding.ToLower().Contains("gzip"))
				responsestream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
			else
				responsestream = response.GetResponseStream();

			StreamReader streamreader = new StreamReader(responsestream);
			resp.ServerResponse = streamreader.ReadToEnd();
			XDocument xdoc = XDocument.Parse(resp.ServerResponse);
			streamreader.Close();

			int responseCode = Convert.ToInt32(xdoc.Root.Element("Ds_Code").Value);

			if (responseCode < 300)
			{
				resp.Transaction.Amount = Convert.ToDecimal(xdoc.Root.Element("Ds_Amount").Value) / 100;
				resp.Transaction.TransactionIDExt = xdoc.Root.Element("Ds_Merchant_MatchingData").Value.ToString();
				resp.Transaction.Type = Convert.ToInt64(xdoc.Root.Element("Ds_Merchant_TransactionType").Value);
				resp.Transaction.Resolved = DateTime.ParseExact(xdoc.Root.Element("Ds_Date").Value.ToString(), GATEWAY_DATE_FORMAT, CultureInfo.InvariantCulture);
				resp.Transaction.PanMask = xdoc.Root.Element("Ds_PanMask").Value.ToString();
				resp.Transaction.AuthCode = xdoc.Root.Element("Ds_AuthorisationCode").Value.ToString();
				resp.ResponseCode = xdoc.Root.Element("Ds_Code").Value.ToString();
				resp.Message = xdoc.Root.Element("Ds_Message").Value.ToString();
				resp.Signature = xdoc.Root.Element("Ds_Signature").Value.ToString();
			}
			else
			{
				resp.ResponseCode = xdoc.Root.Element("Ds_Code").Value.ToString();
				resp.Message = xdoc.Root.Element("Ds_Message").Value.ToString();
				resp.Transaction.TransactionIDExt = xdoc.Root.Element("Ds_Merchant_MatchingData").Value.ToString();
			}

			resp.Status = (resp.ResponseCode == "100") ? EStatus.OK : EStatus.Error;

			return resp;
		}

		public PaymentResponse PreAuthorize(Transaction authTrans)
		{ 	        	
			string signature =  GetSignature(TransactionViewModel.New(authTrans), Settings.MerchantCode, Settings.MerchantKey);

			Dictionary<string,object> postData = new Dictionary<string,object>();

			postData.Add("Ds_Merchant_TransactionType", GetType(ETransactionType.Preauthorization));
			postData.Add("Ds_Merchant_MatchingData", authTrans.TransactionIDExt);
			postData.Add("Ds_Merchant_MerchantCode", Settings.MerchantCode);
			postData.Add("Ds_Merchant_Amount", authTrans.Amount.ToString("N2").Replace(".", string.Empty));
			postData.Add("Ds_Date",  authTrans.Resolved.ToString(GATEWAY_DATE_FORMAT));
			postData.Add("Ds_Merchant_PanMask", authTrans.PanMask);
			postData.Add("Ds_Merchant_MerchantSignature", signature);
			postData.Add("Ds_Merchant_InitialAmount", authTrans.Amount.ToString("N2").Replace(".", string.Empty));
			postData.Add("Ds_Merchant_Currency", GetCurrency(authTrans.ECurrency));		
 
			string url = Settings.ApiOperationHost; 
	
			PaymentResponse response = Remote(url, postData);

			return response;
		}

		public PaymentResponse PreAuthorizeRenew(Transaction authTrans)
		{
			try
			{
				string signature = GetSignature(TransactionViewModel.New(authTrans), Settings.MerchantCode, Settings.MerchantKey);

				Dictionary<string, object> postData = new Dictionary<string, object>();

				postData.Add("Ds_Merchant_TransactionType", GetType(ETransactionType.Preauthorization));
				postData.Add("Ds_Merchant_MatchingData", authTrans.TransactionIDExt);
				postData.Add("Ds_Merchant_MerchantCode", Settings.MerchantCode);
				postData.Add("Ds_Merchant_Amount", authTrans.Amount.ToString("N2").Replace(".", string.Empty));
				postData.Add("Ds_Date", authTrans.Resolved.ToString(GATEWAY_DATE_FORMAT));
				postData.Add("Ds_Merchant_PanMask", authTrans.PanMask);
				postData.Add("Ds_Merchant_MerchantSignature", signature);
				postData.Add("Ds_Merchant_InitialAmount", authTrans.Amount.ToString("N2").Replace(".", string.Empty));
				postData.Add("Ds_Merchant_Currency", GetCurrency(authTrans.ECurrency));

				string url = Settings.ApiOperationHost;

				PaymentResponse response = Remote(url, postData);

				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public PaymentResponse PreAuthorizationCharge(Transaction preauthenTrans, Transaction chargeTrans)
		{ 
			try 
			{
				string signature = GetSignature(TransactionViewModel.New(chargeTrans), Settings.MerchantCode, Settings.MerchantKey);

				Dictionary<string,object> postData = new Dictionary<string,object>();

				postData.Add("Ds_Merchant_TransactionType", GetType(ETransactionType.PreauthCharge));
				postData.Add("Ds_Merchant_MatchingData", preauthenTrans.TransactionIDExt);
				postData.Add("Ds_Merchant_MerchantCode", Settings.MerchantCode);
				postData.Add("Ds_Merchant_Amount", chargeTrans.Amount.ToString("N2").Replace(".", string.Empty));
				postData.Add("Ds_Date", preauthenTrans.Resolved.ToString(GATEWAY_DATE_FORMAT));
				postData.Add("Ds_Merchant_PanMask", preauthenTrans.PanMask);
				postData.Add("Ds_Merchant_MerchantSignature", signature);
				postData.Add("Ds_Merchant_Currency", GetCurrency(chargeTrans.ECurrency));		
 
				string url = Settings.ApiOperationHost; 
	
				PaymentResponse response = Remote(url, postData);

				return response;
			} 
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		public PaymentResponse PreAuthorizationRelease(Transaction preauthenTrans)
		{
			try
			{
				string signature = GetSignature(TransactionViewModel.New(preauthenTrans), Settings.MerchantCode, Settings.MerchantKey);

				Dictionary<string,object> postData = new Dictionary<string,object>();

				postData.Add("Ds_Merchant_TransactionType", GetType(ETransactionType.PreauthCancelation));
				postData.Add("Ds_Merchant_MatchingData", preauthenTrans.TransactionIDExt);
				postData.Add("Ds_Merchant_MerchantCode", Settings.MerchantCode);
				postData.Add("Ds_Merchant_Amount", preauthenTrans.Amount.ToString("N2").Replace(".", string.Empty));
				postData.Add("Ds_Date", preauthenTrans.Resolved.ToString(GATEWAY_DATE_FORMAT));
				postData.Add("Ds_Merchant_PanMask", preauthenTrans.PanMask);
				postData.Add("Ds_Merchant_MerchantSignature", signature);
				postData.Add("Ds_Merchant_Currency", GetCurrency(preauthenTrans.ECurrency));

				string url = Settings.ApiOperationHost;

				PaymentResponse response = Remote(url, postData);

				return response;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected PaymentResponse Remote(string url, Dictionary<string, object> postParameters)
		{
			HttpWebResponse response = null;

			try
			{
				HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

				ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckCertification);

				StringBuilder postParams = WebFace.Helpers.PostCaller.GetPostDataString(postParameters);

				request.Method = "POST";
				request.ContentLength = postParams.Length;
				request.ContentType = "application/x-www-form-urlencoded";
				request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";

				request.Headers.Add("CURLOPT_POST", "1");
				request.Headers.Add("CURLOPT_HEADER", "false");
				request.Headers.Add("CURLOPT_USERAGENT", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)");
				request.Headers.Add("CURLOPT_RETURNTRANSFER", "true");
				request.Headers.Add("CURLOPT_TIMEOUT", "45");
				request.Headers.Add("CURLOPT_FOLLOWLOCATION", "false");
				request.Headers.Add("CURLOPT_SSL_VERIFYPEER", "true");

				request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
				request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
				request.Headers.Add("Accept-Language", "es-ES,es;q=0.8");
				request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.3");
				
				// send the request
				StreamWriter oStreamWriter = new StreamWriter(request.GetRequestStream());
				oStreamWriter.Write(postParams.ToString());
				oStreamWriter.Close();

				// get the response
				response = (HttpWebResponse)request.GetResponse();
			}
			catch (WebException ex)
			{
				StreamReader str = new StreamReader(ex.Response.GetResponseStream());

				PaymentResponse presponse =  new PaymentResponse()
					{
						Status = EStatus.Error,
						ServerResponse = str.ReadToEnd()
					};

				str.Close();

				string message = String.Format("URL: {0}", url) + System.Environment.NewLine +
								String.Format("RESPONSE: {0}", presponse.ServerResponse) + System.Environment.NewLine +
								String.Format("PARAMETERS: {0}", postParameters.ToString());

				presponse.Message = message;

				molLogger.LogError(ex, message);

				return presponse;
			}

			return ParseXMLResponse(response);
		}

		public bool CheckCertification(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			// If the certificate is a valid, signed certificate, return true.
			if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
			{
				return true;
			}

			// If there are errors in the certificate chain, look at each error to determine the cause.
			if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
			{
				if (chain != null && chain.ChainStatus != null)
				{
					foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
					{
						if ((certificate.Subject == certificate.Issuer) &&
						   (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
						{
							// Self-signed certificates with an untrusted root are valid.
							continue;
						}
						else
						{
							if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
							{
								// If there are any other errors in the certificate chain, the certificate is invalid,
								// so the method returns false.
								return false;
							}
						}
					}
				}
				// When processing reaches this line, the only errors in the certificate chain are
				// untrusted root errors for self-signed certificates. These certificates are valid
				// for default Exchange Server installations, so return true.
				return true;
			}
			else
			{
				// In all other cases, return false.
				return false;
			}
		}
	}
}
