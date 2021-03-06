/* UPDATE 6.14.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.14.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';
	
SET SEARCH_PATH = "0001";

UPDATE "Cobro" SET "ID_COBRO" = C."NROW"
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "FECHA" ) AS "NROW"
     FROM "Cobro" 
     WHERE "FECHA" >= '2013-01-01 00:00:00' AND "TIPO_COBRO" = 2) AS C
WHERE "Cobro"."OID" = C."OID" AND "Cobro"."TIPO_COBRO" = 2;

UPDATE "Cobro" SET "ID_COBRO" = C."NROW"
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "FECHA" ) AS "NROW"
     FROM "Cobro" 
     WHERE "FECHA" between '2012-01-01 00:00:00' and '2012-12-31 23:59:59' AND "TIPO_COBRO" = 2) AS C
WHERE "Cobro"."OID" = C."OID" AND "Cobro"."TIPO_COBRO" = 2;

UPDATE "Cobro" SET "ID_COBRO" = C."NROW"
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "FECHA" ) AS "NROW"
     FROM "Cobro" 
     WHERE "FECHA" between '2011-01-01 00:00:00' and '2011-12-31 23:59:59' AND "TIPO_COBRO" = 2) AS C
WHERE "Cobro"."OID" = C."OID" AND "Cobro"."TIPO_COBRO" = 2;
