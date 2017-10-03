/* UPDATE 7.1.0.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.1.0.1' WHERE "NAME" = 'INVOICE_DB_VERSION';

ALTER TABLE "IVClientType"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"COMMON"."IVClientType_OID_seq"'::text)::regclass);

SET SEARCH_PATH = "0001";

ALTER TABLE "Albaran_OID_seq" RENAME TO "IVCollection_OID_seq";
ALTER TABLE "IVDelivery"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVDelivery_OID_seq"'::text)::regclass);
ALTER TABLE "IVCash"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVCash_OID_seq"'::text)::regclass);
ALTER TABLE "IVCashCount"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVCashCount_OID_seq"'::text)::regclass);
ALTER TABLE "IVClient"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVClient_OID_seq"'::text)::regclass);
ALTER TABLE "IVCharge"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVCharge_OID_seq"'::text)::regclass);
ALTER TABLE "IVCharge_Operation"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVCharge_Operation_OID_seq"'::text)::regclass);
ALTER TABLE "IVDelivery_Invoice"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVDelivery_Invoice_OID_seq"'::text)::regclass);
ALTER TABLE "IVDelivery_Ticket"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVDelivery_Ticket_OID_seq"'::text)::regclass);
ALTER TABLE "IVDeliveryLine"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVDeliveryLine_OID_seq"'::text)::regclass);
ALTER TABLE "IVInvoiceLine"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVInvoiceLine_OID_seq"'::text)::regclass);
ALTER TABLE "IVBudgetLine"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVBudgetLine_OID_seq"'::text)::regclass);
ALTER TABLE "IVTicketLine"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVTicketLine_OID_seq"'::text)::regclass);
ALTER TABLE "IVInvoice"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVInvoice_OID_seq"'::text)::regclass);
ALTER TABLE "IVCashLine"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVCashLine_OID_seq"'::text)::regclass);
ALTER TABLE "IVOrderLine"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVOrderLine_OID_seq"'::text)::regclass);
ALTER TABLE "IVBankLine"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVBankLine_OID_seq"'::text)::regclass);
ALTER TABLE "IVOrder"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVOrder_OID_seq"'::text)::regclass);
ALTER TABLE "IVLoan"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVLoan_OID_seq"'::text)::regclass);
ALTER TABLE "IVClient_Product"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVClient_Product_OID_seq"'::text)::regclass);
ALTER TABLE "IVBudget"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVBudget_OID_seq"'::text)::regclass);
ALTER TABLE "IVTicket"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVTicket_OID_seq"'::text)::regclass);
ALTER TABLE "IVInterestRate"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVInterestRate_OID_seq"'::text)::regclass);
ALTER TABLE "IVBankTransfer"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."IVBankTransfer_OID_seq"'::text)::regclass);
	