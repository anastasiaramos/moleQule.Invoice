/* UPDATE 7.4.3.2 */

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.4.3.2' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('INVOICE_TEMPLATE', '1');