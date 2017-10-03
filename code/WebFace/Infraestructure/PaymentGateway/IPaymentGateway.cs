using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Renting;
using moleQule.WebFace.Invoice.Models;

namespace moleQule.WebFace.Invoice
{
	public interface IPaymentGateway
	{
		PaymentGatewaySettings Settings { get; set; }

		string GetTransactionID(TransactionInfo transaction);

		string GetCurrency(ECurrency currency);
		string GetDescription(Transaction transaction, PlanInfo plan, SubscriptionInfo subscription);
		string GetMessage(HttpRequestBase request);
		string GetResponseOKURL(HttpRequestBase request);
		string GetResponseKOURL(HttpRequestBase request);
		string GetSignature(TransactionViewModel transaction);
		string GetSignature(TransactionViewModel transaction, string merchantCode, string merchantKey);
		string GetType(ETransactionType type);

		PaymentResponse ParseHtmlResponse(HttpRequestBase request);
		PaymentResponse ParseXMLResponse(HttpWebResponse response);
		PaymentResponse PreAuthorize(Transaction authTrans);
		PaymentResponse PreAuthorizeRenew(Transaction authTrans);
		PaymentResponse PreAuthorizationCharge(Transaction preauthenTrans, Transaction chargeTrans);
		PaymentResponse PreAuthorizationRelease(Transaction preauthenTrans);
	}

	public class PaymentResponse
	{
		public EStatus Status { get; set; }
		public TransactionInfo Transaction { get; set; }
		public string ResponseCode { get; set; }
		public string Message { get; set; }
		public string Signature { get; set; }
		public string ServerResponse { get; set; }
	}
}
