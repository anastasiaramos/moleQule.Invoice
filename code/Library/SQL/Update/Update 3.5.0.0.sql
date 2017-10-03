/* UPDATE 3.5.0.0*/

SET SEARCH_PATH = "COMMON";

SET SEARCH_PATH = "0001";

ALTER TABLE "MovimientoBanco" ADD COLUMN "FECHA" timestamp without time zone;
ALTER TABLE "MovimientoBanco" ADD COLUMN "ID_OPERACION" varchar(255);
ALTER TABLE "MovimientoBanco" ADD COLUMN "IMPORTE" decimal(10,2);

