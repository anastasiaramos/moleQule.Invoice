﻿/* UPDATE 5.12.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.12.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "Cobro" ADD COLUMN "OID_USUARIO" integer DEFAULT 0;
ALTER TABLE "Ticket" ADD COLUMN "OID_USUARIO" integer DEFAULT 0;
ALTER TABLE "Proforma" ADD COLUMN "OID_USUARIO" integer DEFAULT 0;