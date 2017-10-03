/* UPDATE 3.0.0.0*/

SET SEARCH_PATH = "COMMON";

INSERT INTO "Variable" ("NAME", "VALUE") VALUES ('LAST_ENTRY', '1000');
INSERT INTO "Variable" ("NAME", "VALUE") VALUES ('JOURNAL', '1');
INSERT INTO "Variable" ("NAME", "VALUE") VALUES ('USE_TPV_COUNT', 'TRUE');
INSERT INTO "Variable" ("NAME", "VALUE") VALUES ('DEFAULT_SERIE_VENTA', '1');

UPDATE "Cliente" SET "TIPO_ID" = 1 WHERE "TIPO_ID" = 0; 
UPDATE "Cliente" SET "TIPO_ID" = 0 WHERE "TIPO_ID" = 16; 

SET SEARCH_PATH = "0001";

ALTER TABLE "Caja" ADD COLUMN "CUENTA_CONTABLE" text;

ALTER TABLE "Cobro" ADD COLUMN "ESTADO" bigint DEFAULT 1;
ALTER TABLE "ConceptoProforma" ADD COLUMN "OID_IMPUESTO" bigint DEFAULT 0;

UPDATE "ConceptoFactura" SET "OID_IMPUESTO" = IP."OID" 
FROM "Impuesto" AS IP
WHERE "ConceptoFactura"."P_IGIC" = IP."IGIC";

