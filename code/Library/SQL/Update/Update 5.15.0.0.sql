﻿/* UPDATE 5.15.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.15.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('ENVIAR_FACTURAS_PENDIENTES', 'FALSE');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('PERIODICIDAD_ENVIO_FACTURAS_PENDIENTES', '7');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('PLAZO_ENVIO_FACTURAS_PENDIENTES', '15');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('FECHA_ULTIMO_ENVIO_FACTURAS_PENDIENTES', '01/01/2012');

ALTER TABLE "Cliente" ADD COLUMN "ENVIAR_FACTURA_PENDIENTE" boolean DEFAULT FALSE;