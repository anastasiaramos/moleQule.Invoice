﻿/* UPDATE 6.4.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.4.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "Traspaso" ADD COLUMN "GASTOS_BANCARIOS" numeric(10,2) DEFAULT 0;