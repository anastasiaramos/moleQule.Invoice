﻿/* UPDATE 6.0.0.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.0.0.1' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "Traspaso" ADD COLUMN "TIPO" int8 DEFAULT 7;