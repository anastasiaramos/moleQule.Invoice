﻿/* UPDATE 5.7.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.7.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "Factura" ADD COLUMN "ESTADO_COBRO" bigint DEFAULT 0;
