/* UPDATE 6.12.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.12.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';
	
SET SEARCH_PATH = "0001";

ALTER TABLE "IVTransaction" ADD COLUMN "RESPONSE" text;
