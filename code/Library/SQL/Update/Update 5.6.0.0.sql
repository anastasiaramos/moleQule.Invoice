﻿/* UPDATE 5.5.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.6.0.0' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "LineaPedido" ADD COLUMN "OID_PARTIDA" bigint DEFAULT 0;
ALTER TABLE "LineaPedido" ADD COLUMN "OID_EXPEDIENTE" bigint DEFAULT 0;

ALTER TABLE "ConceptoAlbaran" ADD COLUMN "OID_LINEA_PEDIDO" bigint DEFAULT 0;

ALTER TABLE "Stock" ADD COLUMN "OID_LINEA_PEDIDO" bigint DEFAULT 0;

ALTER TABLE "LineaPedido" ADD COLUMN "OID_KIT" bigint DEFAULT 0;
ALTER TABLE "LineaPedido" ADD COLUMN "FACTURACION_BULTOS" boolean DEFAULT FALSE;
ALTER TABLE "LineaPedido" ADD COLUMN "P_IMPUESTOS" numeric(10,2) DEFAULT 0;
ALTER TABLE "LineaPedido" ADD COLUMN "P_DESCUENTO" numeric(10,2) DEFAULT 0;
ALTER TABLE "LineaPedido" ADD COLUMN "GASTOS" numeric(10,2) DEFAULT 0;

ALTER TABLE "Pedido" ADD COLUMN "OID_SERIE" bigint DEFAULT 0;
ALTER TABLE "Pedido" ADD COLUMN "BASE_IMPONIBLE" numeric(10,2);
ALTER TABLE "Pedido" ADD COLUMN "IMPUESTOS" numeric(10,2);
ALTER TABLE "Pedido" ADD COLUMN "P_DESCUENTO" numeric(10,2);
ALTER TABLE "Pedido" ADD COLUMN "DESCUENTO" numeric(10,2) default 0;

ALTER TABLE "LineaPedido" ADD COLUMN "SUBTOTAL" numeric(10,2) DEFAULT 0;

ALTER TABLE "Stock" ALTER COLUMN "FECHA" TYPE timestamp without time zone;
ALTER TABLE "AlbaranProveedor" ALTER COLUMN "FECHA" TYPE timestamp without time zone;
ALTER TABLE "AlbaranProveedor" ALTER COLUMN "FECHA_REGISTRO" TYPE timestamp without time zone;