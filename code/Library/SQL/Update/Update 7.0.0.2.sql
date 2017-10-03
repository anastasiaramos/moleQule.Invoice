/* UPDATE 7.0.0.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.0.0.2' WHERE "NAME" = 'INVOICE_DB_VERSION';

ALTER TABLE "TipoCliente" RENAME TO "IVClientType";

ALTER TABLE "TipoCliente_OID_seq" RENAME TO "IVClientType_OID_seq";

SET SEARCH_PATH = "0001";

ALTER TABLE "Albaran" RENAME TO "IVDelivery";
ALTER TABLE "Albaran_Factura" RENAME TO "IVDelivery_Invoice";
ALTER TABLE "Albaran_Ticket" RENAME TO "IVDelivery_Ticket";
ALTER TABLE "Caja" RENAME TO "IVCash";
ALTER TABLE "CierreCaja" RENAME TO "IVCashCount";
ALTER TABLE "Cliente" RENAME TO "IVClient";
ALTER TABLE "Cobro" RENAME TO "IVCharge";
ALTER TABLE "Cobro_Factura" RENAME TO "IVCharge_Operation";
ALTER TABLE "ConceptoAlbaran" RENAME TO "IVDeliveryLine";
ALTER TABLE "ConceptoFactura" RENAME TO "IVInvoiceLine";
ALTER TABLE "ConceptoProforma" RENAME TO "IVBudgetLine";
ALTER TABLE "ConceptoTicket" RENAME TO "IVTicketLine";
ALTER TABLE "Factura" RENAME TO "IVInvoice";
ALTER TABLE "LineaCaja" RENAME TO "IVCashLine";
ALTER TABLE "LineaPedido" RENAME TO "IVOrderLine";
ALTER TABLE "MovimientoBanco" RENAME TO "IVBankLine";
ALTER TABLE "Pedido" RENAME TO "IVOrder";
ALTER TABLE "Prestamo" RENAME TO "IVLoan";
ALTER TABLE "Producto_Cliente" RENAME TO "IVClient_Product";
ALTER TABLE "Proforma" RENAME TO "IVBudget";
ALTER TABLE "Ticket" RENAME TO "IVTicket";
ALTER TABLE "TipoInteres" RENAME TO "IVInterestRate";
ALTER TABLE "Traspaso" RENAME TO "IVBankTransfer";

ALTER TABLE "Caja_OID_seq" RENAME TO "IVCash_OID_seq";
ALTER TABLE "CierreCaja_OID_seq" RENAME TO "IVCashCount_OID_seq";
ALTER TABLE "Cliente_OID_seq" RENAME TO "IVClient_OID_seq";
ALTER TABLE "Cobro_OID_seq" RENAME TO "IVCharge_OID_seq";
ALTER TABLE "Cobro_Factura_OID_seq" RENAME TO "IVCharge_Operation_OID_seq";
ALTER TABLE "Albaran_Factura_OID_seq" RENAME TO "IVDelivery_Invoice_OID_seq";
ALTER TABLE "Albaran_Ticket_OID_seq" RENAME TO "IVDelivery_Ticket_OID_seq";
ALTER TABLE "ConceptoAlbaran_OID_seq" RENAME TO "IVDeliveryLine_OID_seq";
ALTER TABLE "ConceptoFactura_OID_seq" RENAME TO "IVInvoiceLine_OID_seq";
ALTER TABLE "ConceptoProforma_OID_seq" RENAME TO "IVBudgetLine_OID_seq";
ALTER TABLE "ConceptoTicket_OID_seq" RENAME TO "IVTicketLine_OID_seq";
ALTER TABLE "Factura_OID_seq" RENAME TO "IVInvoice_OID_seq";
ALTER TABLE "LineaCaja_OID_seq" RENAME TO "IVCashLine_OID_seq";
ALTER TABLE "LineaPedido_OID_seq" RENAME TO "IVOrderLine_OID_seq";
ALTER TABLE "MovimientoBanco_OID_seq" RENAME TO "IVBankLine_OID_seq";
ALTER TABLE "Pedido_OID_seq" RENAME TO "IVOrder_OID_seq";
ALTER TABLE "Prestamo_OID_seq" RENAME TO "IVLoan_OID_seq";
ALTER TABLE "Producto_Cliente_OID_seq" RENAME TO "IVClient_Product_OID_seq";
ALTER TABLE "Proforma_OID_seq" RENAME TO "IVBudget_OID_seq";
ALTER TABLE "Ticket_OID_seq" RENAME TO "IVTicket_OID_seq";
ALTER TABLE "TipoInteres_OID_seq" RENAME TO "IVInterestRate_OID_seq";
ALTER TABLE "Traspaso_OID_seq" RENAME TO "IVBankTransfer_OID_seq";