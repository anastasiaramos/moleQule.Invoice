﻿/* UPDATE 6.6.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.6.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "Cliente" ADD COLUMN "OID_EXT" int8 DEFAULT 0;
UPDATE "Cliente" SET "OID_EXT" = 0;