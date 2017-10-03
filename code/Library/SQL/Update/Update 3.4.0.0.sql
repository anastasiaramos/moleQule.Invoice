/* UPDATE 3.4.0.0*/

SET SEARCH_PATH = "COMMON";

SET SEARCH_PATH = "0001";

ALTER TABLE "MovimientoBanco" DROP CONSTRAINT "MovimientoBanco_CODIGO_key";
ALTER TABLE "MovimientoBanco" DROP CONSTRAINT "MovimientoBanco_SERIAL_key";

