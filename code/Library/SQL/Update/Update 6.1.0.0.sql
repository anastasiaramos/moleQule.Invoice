﻿/* UPDATE 6.1.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.1.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "Prestamo" ADD COLUMN "OID_PAGO" int8 DEFAULT 0;
ALTER TABLE "MovimientoBanco" ADD COLUMN "FECHA_CREACION" timestamp without time zone;
ALTER TABLE "MovimientoBanco" ADD COLUMN "FECHA_VENCIMIENTO" date;

UPDATE "MovimientoBanco" SET "FECHA_CREACION" = "FECHA";
UPDATE "MovimientoBanco" SET "FECHA_VENCIMIENTO" = "FECHA";