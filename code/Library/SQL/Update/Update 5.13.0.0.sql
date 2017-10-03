/* UPDATE 5.13.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.13.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

CREATE TABLE "Prestamo" 
( 
	"OID" bigserial NOT NULL,
	"OID_CUENTA" int8 DEFAULT 0,
	"FECHA_FIRMA" timestamp without time zone,
	"FECHA_INGRESO" timestamp without time zone,
	"FECHA_VENCIMIENTO" timestamp without time zone,
	"NOMBRE" varchar(255),
	"IMPORTE" numeric(10,2),
	"TIPO_INTERES" numeric(10,2),
	"FORMA_PAGO" int8 DEFAULT 0,
	"DIA_PAGO" int8 DEFAULT 1,
	"OBSERVACIONES" text,
	CONSTRAINT "PK_Prestamo" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Prestamo" OWNER TO moladmin;
GRANT ALL ON TABLE "Prestamo" TO GROUP "MOLEQULE_ADMINISTRATOR";