﻿/* UPDATE 6.0.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.0.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('HORA_ENVIO_FACTURAS_PENDIENTES', '01/01/2012 00:00');