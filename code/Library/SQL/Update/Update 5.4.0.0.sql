﻿/* UPDATE 5.4.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.4.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "CierreCaja" ADD COLUMN "OID_USUARIO" bigint DEFAULT 5;
