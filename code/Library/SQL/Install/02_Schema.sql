-- INVOICE MODULE DETAIL SCHEMA SCRIPT
-- Requires STORE MODULE

DROP TABLE IF EXISTS "IVBankLine";
CREATE TABLE "IVBankLine" 
( 
	"OID" bigserial  NOT NULL,
	"OID_OPERACION" bigint NOT NULL,
    "TIPO_OPERACION" bigint DEFAULT 0,
    "SERIAL" bigint,
    "CODIGO" character varying(255),
    "OID_USUARIO" bigint,
    "AUDITADO" boolean DEFAULT false,
    "OBSERVACIONES" text,
    "ESTADO" bigint DEFAULT 1,
    "FECHA_OPERACION" timestamp without time zone,
    "ID_OPERACION" character varying(255),
    "IMPORTE" numeric(10,2),
    "TIPO_CUENTA" bigint DEFAULT 1,
    "OID_CUENTA" bigint DEFAULT 0,
    "FECHA_CREACION" timestamp without time zone,
    "TIPO_MOVIMIENTO" bigint DEFAULT 1,
	CONSTRAINT "PK_IVBankLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVBankLine" OWNER TO moladmin;
GRANT ALL ON TABLE "IVBankLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVBankTransfer" CASCADE;
CREATE TABLE "IVBankTransfer" 
( 
	"OID" bigserial  NOT NULL,
	"OID_CUENTA_ORIGEN" bigint NOT NULL,
    "OID_CUENTA_DESTINO" bigint NOT NULL,
    "OID_USUARIO" bigint,
    "SERIAL" bigint,
    "CODIGO" character varying(255),
    "ESTADO" bigint,
    "FECHA" timestamp without time zone,
    "OBSERVACIONES" text,
    "IMPORTE" numeric(10,2),
    "TIPO" bigint DEFAULT 7,
    "GASTOS_BANCARIOS" numeric(10,2) DEFAULT 0,
    "FECHA_RECEPCION" timestamp without time zone,
	CONSTRAINT "PK_IVBankTransfer" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVBankTransfer" OWNER TO moladmin;
GRANT ALL ON TABLE "IVBankTransfer" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVBudget" CASCADE;
CREATE TABLE "IVBudget" 
( 
	"OID" bigserial NOT NULL,
	"OID_SERIE" bigint,
    "OID_CLIENTE" bigint,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "FECHA" date,
    "SUBTOTAL" numeric(10,2),
    "P_DESCUENTO" numeric(10,2),
    "IMPUESTOS" numeric(10,2),
    "TOTAL" numeric(10,2),
    "NOTA" boolean,
    "OBSERVACIONES" text,
    "P_IRPF" numeric(10,2),
    "OID_USUARIO" integer DEFAULT 0,
	CONSTRAINT "PK_IVBudget" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVBudget" OWNER TO moladmin;
GRANT ALL ON TABLE "IVBudget" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVBudgetLine";
CREATE TABLE "IVBudgetLine" 
( 
	"OID" bigserial NOT NULL,
	"OID_PROFORMA" bigint,
    "OID_BATCH" bigint DEFAULT 0 NOT NULL,
    "OID_EXPEDIENTE" bigint DEFAULT 0 NOT NULL,
    "OID_PRODUCTO" bigint,
    "CODIGO_EXPEDIENTE" character varying(255),
    "CONCEPTO" character varying(255),
    "FACTURACION_BULTO" boolean DEFAULT false NOT NULL,
    "CANTIDAD" numeric(10,2),
    "CANTIDAD_BULTOS" numeric(10,4),
    "P_IMPUESTOS" numeric(10,2),
    "P_DESCUENTO" numeric(10,2),
    "TOTAL" numeric(10,2),
    "PRECIO" numeric(10,5),
    "SUBTOTAL" numeric(10,2),
    "OID_IMPUESTO" bigint DEFAULT 0,
	CONSTRAINT "PK_IVBudgetLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVBudgetLine" OWNER TO moladmin;
GRANT ALL ON TABLE "IVBudgetLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVCash" CASCADE;
CREATE TABLE "IVCash" 
( 
	"OID" bigserial NOT NULL,
	"CODIGO" character varying(255),
    "NOMBRE" character varying(255),
    "OBSERVACIONES" text,
    "CUENTA_CONTABLE" text,
	CONSTRAINT "PK_IVCash" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVCash" OWNER TO moladmin;
GRANT ALL ON TABLE "IVCash" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVCashCount" CASCADE;
CREATE TABLE "IVCashCount" 
( 
	"OID" bigserial NOT NULL,
	"OID_CAJA" bigint NOT NULL,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "DEBE" numeric(10,2) DEFAULT 0,
    "HABER" numeric(10,2) DEFAULT 0,
    "FECHA" timestamp without time zone,
    "OBSERVACIONES" text,
    "OID_USUARIO" bigint DEFAULT 5,
	CONSTRAINT "PK_IVCashCount" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVCashCount" OWNER TO moladmin;
GRANT ALL ON TABLE "IVCashCount" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "IVCashLine" CASCADE;
CREATE TABLE "IVCashLine" ( 
	"OID" bigserial  NOT NULL,
	"OID_CAJA" bigint NOT NULL,
    "OID_CIERRE" bigint DEFAULT 0,
	"OID_LINK" bigint DEFAULT 0,
    "SERIAL" bigint,
    "CODIGO" character varying(255),
    "N_FACTURA" character varying(255),
    "FECHA" timestamp without time zone,
    "CONCEPTO" character varying(255),
    "OID_COBRO" bigint,
    "OID_PAGO" bigint,
    "N_CLIENTE" bigint,
    "N_PROVEEDOR" character varying(255),
    "DEBE" numeric(10,2),
    "HABER" numeric(10,2),
    "OBSERVACIONES" text,
    "OID_CUENTA_BANCARIA" bigint DEFAULT 0,
    "ESTADO" bigint DEFAULT 1,
    "TIPO" bigint DEFAULT 5,
	CONSTRAINT "PK_IVCashLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVCashLine" OWNER TO moladmin;
GRANT ALL ON TABLE "IVCashLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVClient" CASCADE;
CREATE TABLE "IVClient" 
( 
	"OID" bigserial NOT NULL,
	"OID_EXT" int8 DEFAULT 0,
	"SERIAL" bigint DEFAULT 0 NOT NULL,
	"CODIGO" character varying(255),
    "ESTADO" bigint DEFAULT 10,
    "TIPO_ID" bigint,
    "VAT_NUMBER" character varying(255),
    "NOMBRE" character varying(255),
    "NOMBRE_COMERCIAL" character varying(255),
    "TITULAR" character varying(255),
    "DIRECCION" character varying(255),
    "POBLACION" character varying(255),
    "CODIGO_POSTAL" character varying(255),
    "PROVINCIA" character varying(255),
	"PAIS" character varying(255),
	"PREFIX" character varying(255),
    "TELEFONOS" character varying(255),
    "FAX" character varying(255),
    "MOVIL" character varying(255),
    "MUNICIPIO" character varying(255),
	"COUNTRY" character varying(255),
    "EMAIL" character varying(255),
	"BIRTH_DATE" timestamp without time zone,
    "OBSERVACIONES" text,
    "HISTORIA" text,
    "LIMITE_CREDITO" numeric(10,2),
    "CONTACTO" character varying(255),
    "MEDIO_PAGO" bigint DEFAULT 1,
    "FORMA_PAGO" bigint,
    "DIAS_PAGO" bigint,
    "CODIGO_EXPLOTACION" character varying(255),
    "CUENTA_BANCARIA" character varying(255),
	"SWIFT" character varying(255),
    "OID_CUENTA_BANCARIA_ASOCIADA" bigint DEFAULT 0,
    "DESCUENTO" numeric(10,2) DEFAULT 0,
    "PRECIO_TRANSPORTE" numeric(10,2),
    "OID_TRANSPORTE" bigint DEFAULT 0 NOT NULL,
    "CUENTA_CONTABLE" text,
    "OID_IMPUESTO" bigint,
    "TIPO_INTERES" numeric(10,2) DEFAULT 0,
    "P_DESCUENTO_PTO_PAGO" numeric(10,2) DEFAULT 0,
    "PRIORIDAD_PRECIO" bigint DEFAULT 3,
    "ENVIAR_FACTURA_PENDIENTE" boolean DEFAULT false,
	CONSTRAINT "PK_IVClient" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVClient" OWNER TO moladmin;
GRANT ALL ON TABLE "IVClient" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVClient_Product" CASCADE;
CREATE TABLE "IVClient_Product" 
( 
	"OID" bigserial NOT NULL,
	"OID_PRODUCTO" bigint,
    "OID_CLIENTE" bigint,
    "PRECIO" numeric(10,5),
    "FACTURACION_BULTO" boolean,
    "P_DESCUENTO" numeric(10,2) DEFAULT 0,
    "TIPO_DESCUENTO" bigint DEFAULT 1,
    "PRECIO_COMPRA" numeric(10,2) DEFAULT 0,
    "FACTURAR" boolean DEFAULT false,
    "FECHA_VALIDEZ" timestamp without time zone,
	CONSTRAINT "PK_IVClient_Product" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVClient_Product" OWNER TO moladmin;
GRANT ALL ON TABLE "IVClient_Product" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVCharge" CASCADE;
CREATE TABLE "IVCharge" 
( 
	"OID" bigserial NOT NULL,
	"OID_CLIENTE" bigint,
	"OID_USUARIO" integer DEFAULT 0,
    "ID_COBRO" bigint,
    "TIPO_COBRO" integer DEFAULT 0 NOT NULL,
    "FECHA" timestamp without time zone,
    "IMPORTE" numeric(10,2),
    "MEDIO_PAGO" bigint DEFAULT 1,
    "VENCIMIENTO" date,
    "OBSERVACIONES" text,
    "OID_CUENTA_BANCARIA" bigint DEFAULT 0,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "OID_TPV" bigint,
    "ESTADO" bigint DEFAULT 1,
	"ESTADO_COBRO" bigint DEFAULT 7,
    "ID_MOV_CONTABLE" character varying(255),
    "GASTOS_BANCARIOS" numeric(10,2) DEFAULT 0,
	CONSTRAINT "PK_IVCharge" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVCharge" OWNER TO moladmin;
GRANT ALL ON TABLE "IVCharge" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVCharge_Operation";
CREATE TABLE "IVCharge_Operation" 
( 
	"OID" bigserial NOT NULL,
	"OID_COBRO" bigint NOT NULL,
    "OID_FACTURA" bigint NOT NULL,
    "CANTIDAD" numeric(10,2),
    "FECHA_ASIGNACION" date,
	CONSTRAINT "PK_IVCharge_Operation" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVCharge_Operation" OWNER TO moladmin;
GRANT ALL ON TABLE "IVCharge_Operation" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVDelivery" CASCADE;
CREATE TABLE "IVDelivery" 
( 
	"OID" bigserial NOT NULL,
	"OID_SERIE" bigint,
    "OID_CLIENTE" bigint,
    "OID_TRANSPORTISTA" bigint,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "ANO" bigint,
    "FECHA" timestamp without time zone,
    "FORMA_PAGO" bigint DEFAULT 1,
    "DIAS_PAGO" bigint DEFAULT 0,
    "MEDIO_PAGO" bigint DEFAULT 1,
    "PREVISION_PAGO" date,
    "BASE_IMPONIBLE" numeric(10,2),
    "P_IGIC" numeric(10,2),
    "P_DESCUENTO" numeric(10,2),
    "DESCUENTO" numeric(10,2) DEFAULT 0,
    "TOTAL" numeric(10,2),
    "CUENTA_BANCARIA" character varying(255),
    "NOTA" boolean,
    "OBSERVACIONES" text,
    "CONTADO" boolean DEFAULT false NOT NULL,
    "RECTIFICATIVO" boolean DEFAULT false,
    "OID_USUARIO" bigint DEFAULT 0,
    "OID_ALMACEN" bigint DEFAULT 0,
    "OID_EXPEDIENTE" bigint DEFAULT 0,
	"ESTADO" bigint DEFAULT 1,
	CONSTRAINT "PK_IVDelivery" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVDelivery" OWNER TO moladmin;
GRANT ALL ON TABLE "IVDelivery" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVDeliveryLine";
CREATE TABLE "IVDeliveryLine" 
( 
	"OID" bigserial NOT NULL,
	"OID_ALBARAN" bigint,
    "OID_BATCH" bigint,
    "OID_EXPEDIENTE" bigint,
    "OID_PRODUCTO" bigint,
    "OID_KIT" bigint DEFAULT 0 NOT NULL,
    "CODIGO_EXPEDIENTE" character varying(255),
    "CONCEPTO" character varying(255),
    "FACTURACION_BULTO" boolean,
    "CANTIDAD" numeric(10,2),
    "CANTIDAD_BULTOS" numeric(10,4),
    "P_IGIC" numeric(10,2),
    "P_DESCUENTO" numeric(10,2),
    "TOTAL" numeric(10,2),
    "PRECIO" numeric(10,5),
    "SUBTOTAL" numeric(10,2),
    "GASTOS" numeric(10,5),
    "OID_IMPUESTO" bigint,
    "OID_LINEA_PEDIDO" bigint DEFAULT 0,
    "OID_ALMACEN" bigint DEFAULT 0,
    "CODIGO_PRODUCTO_CLIENTE" character varying(255),
	CONSTRAINT "PK_IVDeliveryLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVDeliveryLine" OWNER TO moladmin;
GRANT ALL ON TABLE "IVDeliveryLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVDelivery_Invoice" CASCADE;
CREATE TABLE "IVDelivery_Invoice" 
( 
	"OID" bigserial NOT NULL,
	"OID_ALBARAN" bigint NOT NULL,
    "OID_FACTURA" bigint NOT NULL,
    "FECHA_ASIGNACION" date,
	CONSTRAINT "PK_IVDelivery_Invoice" PRIMARY KEY ("OID"),
	CONSTRAINT "UQ_IVDelivery_Invoice" UNIQUE ("OID_ALBARAN", "OID_FACTURA")
) WITHOUT OIDS;

ALTER TABLE "IVDelivery_Invoice" OWNER TO moladmin;
GRANT ALL ON TABLE "IVDelivery_Invoice" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVDelivery_Ticket" CASCADE;
CREATE TABLE "IVDelivery_Ticket" 
( 
	"OID" bigserial NOT NULL,
	"OID_ALBARAN" bigint NOT NULL,
    "OID_TICKET" bigint NOT NULL,
    "FECHA_ASIGNACION" timestamp without time zone,
	CONSTRAINT "PK_IVDelivery_Ticket" PRIMARY KEY ("OID"),
	CONSTRAINT "UQ_IVDelivery_Ticket" UNIQUE ("OID_ALBARAN", "OID_TICKET")
) WITHOUT OIDS;

ALTER TABLE "IVDelivery_Ticket" OWNER TO moladmin;
GRANT ALL ON TABLE "IVDelivery_Ticket" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVEffect" CASCADE;
CREATE TABLE "IVEffect" 
( 
	"OID" bigserial  NOT NULL,
	"OID_COBRO" bigint NOT NULL,
	"FECHA_EMISION" timestamp without time zone,
	"VENCIMIENTO" timestamp without time zone,
	"CHARGE_DATE" timestamp without time zone,
	"RETURN_DATE" timestamp without time zone,
	"ADELANTADO" boolean,
	"GASTOS_ADELANTO" numeric(10,2) DEFAULT 0,
	"GASTOS_DEVOLUCION" numeric(10,2) DEFAULT 0,
	"GASTOS" numeric(10,2) DEFAULT 0,
	"ESTADO" bigint DEFAULT 1,
	"ESTADO_COBRO" bigint DEFAULT 7,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "OBSERVACIONES" character varying(255),
	"OID_CUENTA_BANCARIA" bigint DEFAULT 0,
	CONSTRAINT PK_IVEffect PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVEffect" OWNER TO moladmin;
GRANT ALL ON TABLE "IVEffect" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVInterestRate" CASCADE;
CREATE TABLE "IVInterestRate" 
( 
	"OID" bigserial  NOT NULL,
	"OID_PRESTAMO" bigint NOT NULL,
	"TIPO_INTERES" numeric(10,2) NOT NULL,
	"FECHA_INICIO" date,
	"FECHA_FIN" date,
	"IMPORTE_CUOTA" numeric(10,2),
	CONSTRAINT "PK_IVInterestRate" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVInterestRate" OWNER TO moladmin;
GRANT ALL ON TABLE "IVInterestRate" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVInvoice" CASCADE;
CREATE TABLE "IVInvoice" 
( 
	"OID" bigserial NOT NULL,
	"OID_SERIE" bigint,
    "OID_CLIENTE" bigint,
    "OID_TRANSPORTISTA" bigint,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "VAT_NUMBER" character varying(255),
    "CLIENTE" character varying(255),
    "DIRECCION" character varying(255),
    "CODIGO_POSTAL" character varying(255),
    "PROVINCIA" character varying(255),
    "MUNICIPIO" character varying(255),
    "ANO" bigint,
    "FECHA" date,
    "FORMA_PAGO" bigint DEFAULT 1,
    "DIAS_PAGO" bigint DEFAULT 0,
    "MEDIO_PAGO" bigint DEFAULT 1,
    "PREVISION_PAGO" date,
    "BASE_IMPONIBLE" numeric(10,2),
    "P_IGIC" numeric(10,2),
    "P_DESCUENTO" numeric(10,2),
    "DESCUENTO" numeric(10,2) DEFAULT 0,
    "TOTAL" numeric(10,2),
    "CUENTA_BANCARIA" character varying(255),
    "NOTA" boolean,
    "OBSERVACIONES" text,
    "ALBARAN" boolean,
    "RECTIFICATIVA" boolean,
    "ESTADO" bigint DEFAULT 1,
    "P_IRPF" numeric(10,2),
    "ALBARANES" text,
    "ID_MOV_CONTABLE" character varying(255),
    "ESTADO_COBRO" bigint DEFAULT 0,
    "OID_USUARIO" bigint DEFAULT 0,
	CONSTRAINT "PK_IVInvoice" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVInvoice" OWNER TO moladmin;
GRANT ALL ON TABLE "IVInvoice" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVInvoiceLine";
CREATE TABLE "IVInvoiceLine" 
( 
	"OID" bigserial NOT NULL,
	"OID_FACTURA" bigint,
    "OID_BATCH" bigint,
    "OID_EXPEDIENTE" bigint,
    "OID_PRODUCTO" bigint,
    "OID_KIT" bigint DEFAULT 0 NOT NULL,
    "OID_CONCEPTO_ALBARAN" bigint,
    "CODIGO_EXPEDIENTE" character varying(255),
    "CONCEPTO" character varying(255),
    "FACTURACION_BULTO" boolean,
    "CANTIDAD" numeric(10,2),
    "CANTIDAD_BULTOS" numeric(10,4),
    "P_IGIC" numeric(10,2),
    "P_DESCUENTO" numeric(10,2),
    "TOTAL" numeric(10,2),
    "PRECIO" numeric(10,5),
    "SUBTOTAL" numeric(10,2),
    "GASTOS" numeric(10,5),
    "OID_IMPUESTO" bigint,
    "OID_ALMACEN" bigint DEFAULT 0,
    "CODIGO_PRODUCTO_CLIENTE" character varying(255),
	CONSTRAINT "PK_IVInvoiceLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVInvoiceLine" OWNER TO moladmin;
GRANT ALL ON TABLE "IVInvoiceLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVLoan";
CREATE TABLE "IVLoan" 
( 
	"OID" bigserial NOT NULL,
	"OID_CUENTA" bigint DEFAULT 0,
    "FECHA_FIRMA" timestamp without time zone,
    "FECHA_INGRESO" timestamp without time zone,
    "FECHA_VENCIMIENTO" timestamp without time zone,
    "NOMBRE" character varying(255),
    "IMPORTE" numeric(10,2),
    "OBSERVACIONES" text,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "OID_PAGO" bigint DEFAULT 0,
	"N_CUOTAS" bigint DEFAULT 1,
	"INICIO_PAGO" date,
	"PERIODO_PAGO" bigint DEFAULT 1,
	"IMPORTE_CUOTA" numeric(10,2),
    "CUENTA_CONTABLE" character varying(255),
	"GASTOS_BANCARIOS" numeric(10,2),
	"GASTOS_INICIO" boolean DEFAULT FALSE,
	"ESTADO" bigint DEFAULT 1,
	CONSTRAINT "PK_IVLoan" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVLoan" OWNER TO moladmin;
GRANT ALL ON TABLE "IVLoan" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "IVOrder" CASCADE;
CREATE TABLE "IVOrder" 
( 
	"OID" bigserial  NOT NULL,
	"OID_USUARIO" bigint DEFAULT 0,
    "OID_CLIENTE" bigint DEFAULT 0,
    "OID_PRODUCTO" bigint DEFAULT 0,
    "SERIAL" bigint NOT NULL,
    "CODIGO" character varying(255) NOT NULL,
    "FECHA" timestamp without time zone,
    "TOTAL" numeric(10,2),
    "ESTADO" bigint,
    "OBSERVACIONES" text,
    "OID_SERIE" bigint DEFAULT 0,
    "BASE_IMPONIBLE" numeric(10,2),
    "IMPUESTOS" numeric(10,2),
    "P_DESCUENTO" numeric(10,2),
    "DESCUENTO" numeric(10,2) DEFAULT 0,
    "OID_ALMACEN" bigint DEFAULT 0,
    "OID_EXPEDIENTE" bigint DEFAULT 0,
	CONSTRAINT "PK_IVOrder" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVOrder" OWNER TO moladmin;
GRANT ALL ON TABLE "IVOrder" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE  IF EXISTS "IVOrderLine" CASCADE;
CREATE TABLE "IVOrderLine" 
( 
	"OID" bigserial  NOT NULL,
	"OID_PEDIDO" bigint NOT NULL,
    "OID_PRODUCTO" bigint NOT NULL,
    "ESTADO" bigint,
    "CONCEPTO" character varying(255),
    "CANTIDAD" numeric(10,2) DEFAULT 0 NOT NULL,
    "CANTIDAD_BULTOS" numeric(10,4),
    "PRECIO" numeric(10,5),
    "TOTAL" numeric(10,2),
    "OBSERVACIONES" text,
    "OID_PARTIDA" bigint DEFAULT 0,
    "OID_EXPEDIENTE" bigint DEFAULT 0,
    "OID_KIT" bigint DEFAULT 0,
    "FACTURACION_BULTOS" boolean DEFAULT false,
    "P_IMPUESTOS" numeric(10,2) DEFAULT 0,
    "P_DESCUENTO" numeric(10,2) DEFAULT 0,
    "GASTOS" numeric(10,2) DEFAULT 0,
    "SUBTOTAL" numeric(10,2) DEFAULT 0,
    "OID_ALMACEN" bigint DEFAULT 0,
    "OID_IMPUESTO" bigint DEFAULT 0,
	CONSTRAINT "PK_IVOrderLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVOrderLine" OWNER TO moladmin;
GRANT ALL ON TABLE "IVOrderLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVTicket" CASCADE;
CREATE TABLE "IVTicket" 
( 
	"OID" bigserial NOT NULL,
	"OID_SERIE" bigint,
    "OID_TPV" bigint,
    "SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODIGO" character varying(255),
    "ESTADO" bigint DEFAULT 1,
    "TIPO" bigint DEFAULT 1,
    "FECHA" timestamp without time zone,
    "BASE_IMPONIBLE" numeric(10,2),
    "IMPUESTOS" numeric(10,2),
    "DESCUENTO" numeric(10,2),
    "TOTAL" numeric(10,2),
    "FORMA_PAGO" bigint DEFAULT 1,
    "DIAS_PAGO" bigint DEFAULT 0,
    "MEDIO_PAGO" bigint DEFAULT 1,
    "PREVISION_PAGO" timestamp without time zone,
    "OBSERVACIONES" text,
    "ALBARANES" text,
    "OID_USUARIO" integer DEFAULT 0,
	CONSTRAINT "PK_IVTicket" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVTicket" OWNER TO moladmin;
GRANT ALL ON TABLE "IVTicket" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVTicketLine";
CREATE TABLE "IVTicketLine" 
( 
	"OID" bigserial NOT NULL,
	"OID_TICKET" bigint,
    "OID_BATCH" bigint,
    "OID_EXPEDIENTE" bigint,
    "OID_PRODUCTO" bigint,
    "OID_KIT" bigint DEFAULT 0 NOT NULL,
    "OID_CONCEPTO_ALBARAN" bigint,
    "OID_IMPUESTO" bigint,
    "CODIGO_EXPEDIENTE" character varying(255),
    "CONCEPTO" character varying(255),
    "FACTURACION_BULTO" boolean,
    "CANTIDAD" numeric(10,2),
    "CANTIDAD_BULTOS" numeric(10,4),
    "P_IMPUESTOS" numeric(10,2),
    "P_DESCUENTO" numeric(10,2),
    "TOTAL" numeric(10,2),
    "PRECIO" numeric(10,5),
    "SUBTOTAL" numeric(10,2),
    "GASTOS" numeric(10,5),
	CONSTRAINT "PK_IVTicketLine" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IVTicketLine" OWNER TO moladmin;
GRANT ALL ON TABLE "IVTicketLine" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "IVTransaction" CASCADE;
CREATE TABLE "IVTransaction" 
( 
	"OID" bigserial NOT NULL,
	"OID_ENTITY" int8 NOT NULL,
	"ENTITY_TYPE" int8 NOT NULL,
	"SERIAL" int8 DEFAULT 0 NOT NULL,
	"CODE" character varying(255),
	"TYPE" int8 DEFAULT 1,
	"STATUS" int8 DEFAULT 1,
	"CREATED" timestamp without time zone,
	"RESOLVED" timestamp without time zone,	
	"TRANSACTIONID" character varying(255) NOT NULL,
	"TRANSACTIONID_EXT" character varying(255),
	"AUTH_CODE" character varying(255),
	"PAN_MASK" character varying(255),
	"AMOUNT" numeric(10,2),
	"CURRENCY" int8 NOT NULL DEFAULT 1,
	"GATEWAY" int8 NOT NULL DEFAULT 1,
	"DESCRIPTION" text,
	"RESPONSE" text,
    CONSTRAINT "IVTransaction_PK" PRIMARY KEY ("OID")
)WITHOUT OIDS;

ALTER TABLE "IVTransaction" OWNER TO moladmin;
GRANT ALL ON TABLE "IVTransaction" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "Alumno_Cliente" CASCADE;
CREATE TABLE "Alumno_Cliente" 
( 
	"OID" bigserial  NOT NULL,
	"OID_ALUMNO" bigint NOT NULL,
    "OID_CLIENTE" bigint NOT NULL,
	CONSTRAINT "PK_Alumno_Cliente" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Alumno_Cliente" OWNER TO moladmin;
GRANT ALL ON TABLE "Alumno_Cliente" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "CobroREA";
CREATE TABLE "CobroREA"
(
	"OID" bigserial,
	"OID_COBRO" bigint NOT NULL,
    "OID_EXPEDIENTE" bigint NOT NULL,
    "CANTIDAD" numeric(10,2),
    "OID_EXPEDIENTE_REA" bigint,
	"FECHA_ASIGNACION" date,
	CONSTRAINT "PK_CobroREA" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "CobroREA" OWNER TO moladmin;
GRANT ALL ON TABLE "CobroREA" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "Curso_Cliente";
CREATE TABLE "Curso_Cliente" 
( 
	"OID" bigserial NOT NULL,
	"OID_CURSO" bigint NOT NULL,
    "OID_CLIENTE" bigint NOT NULL,
	CONSTRAINT "PK_Curso_Cliente" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Curso_Cliente" OWNER TO moladmin;
GRANT ALL ON TABLE "Curso_Cliente" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "ReciboAlumno" CASCADE;
CREATE TABLE "ReciboAlumno" 
( 
	"OID" bigserial NOT NULL,
	"OID_ALUMNO" bigint NOT NULL,
    "FECHA" timestamp without time zone,
    "TOTAL" real,
    "FORMA_PAGO" character varying(255),
    "DOCUMENTO_ASOCIADO" bigint,
    "OBSERVACIONES" text,
	CONSTRAINT "PK_ReciboAlumno" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "ReciboAlumno" OWNER TO moladmin;
GRANT ALL ON TABLE "ReciboAlumno" TO GROUP "MOLEQULE_ADMINISTRATOR";

-- FOREIGN KEYS

ALTER TABLE ONLY "IVBankTransfer"
    ADD CONSTRAINT "FK_IVBankTransfer_BankAccount" FOREIGN KEY ("OID_CUENTA_DESTINO") REFERENCES "CMBankAccount"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "IVBankTransfer"
    ADD CONSTRAINT "FK_IVBankTransfer_BankAccount2" FOREIGN KEY ("OID_CUENTA_ORIGEN") REFERENCES "CMBankAccount"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "IVBankTransfer"
    ADD CONSTRAINT "FK_Traspaso_User" FOREIGN KEY ("OID_USUARIO") REFERENCES "COMMON"."User"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "IVBudget"
    ADD CONSTRAINT "FK_IVBudget_Client" FOREIGN KEY ("OID_CLIENTE") REFERENCES "IVClient"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "IVBudget"
    ADD CONSTRAINT "FK_IVBudget_Serie" FOREIGN KEY ("OID_SERIE") REFERENCES "STSerie"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "IVBudgetLine"
    ADD CONSTRAINT "FK_IVBudgetLine_Budget" FOREIGN KEY ("OID_PROFORMA") REFERENCES "IVBudget"("OID") ON UPDATE CASCADE ON DELETE CASCADE;
	
ALTER TABLE ONLY "IVCashCount"
    ADD CONSTRAINT "FK_IVCashCount_Cash" FOREIGN KEY ("OID_CAJA") REFERENCES "IVCash"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;
	
ALTER TABLE ONLY "IVCashLine"
    ADD CONSTRAINT "FK_IVCashLine_Cash" FOREIGN KEY ("OID_CAJA") REFERENCES "IVCash"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;
	
ALTER TABLE ONLY "IVCharge_Operation"
    ADD CONSTRAINT "FK_IVCharge_Operation_Charge" FOREIGN KEY ("OID_COBRO") REFERENCES "IVCharge"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "IVCharge_Operation"
    ADD CONSTRAINT "FK_IVCharge_Operation_Invoice" FOREIGN KEY ("OID_FACTURA") REFERENCES "IVInvoice"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "CobroREA"
    ADD CONSTRAINT "FK_CobroREA_Cobro" FOREIGN KEY ("OID_COBRO") REFERENCES "IVCharge"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "CobroREA"
    ADD CONSTRAINT "FK_CobroREA_Expediente" FOREIGN KEY ("OID_EXPEDIENTE") REFERENCES "Expediente"("OID") ON UPDATE CASCADE ON DELETE CASCADE;
	
ALTER TABLE ONLY "IVClient_Product"
    ADD CONSTRAINT "FK_Producto_Cliente_Client" FOREIGN KEY ("OID_CLIENTE") REFERENCES "IVClient"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "IVClient_Product"
    ADD CONSTRAINT "FK_IVClient_Product_Product" FOREIGN KEY ("OID_PRODUCTO") REFERENCES "Producto"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "IVDelivery"
    ADD CONSTRAINT "FK_IVDelivery_Serie" FOREIGN KEY ("OID_SERIE") REFERENCES "STSerie"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;
	
ALTER TABLE ONLY "IVDelivery"
    ADD CONSTRAINT "FK_IVDelivery_Client" FOREIGN KEY ("OID_CLIENTE") REFERENCES "IVClient"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "IVDeliveryLine"
    ADD CONSTRAINT "FK_IVDeliveryLine_Delivery" FOREIGN KEY ("OID_ALBARAN") REFERENCES "IVDelivery"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "IVDelivery_Invoice"
    ADD CONSTRAINT "FK_IVDelivery_Invoice_Delivery" FOREIGN KEY ("OID_ALBARAN") REFERENCES "IVDelivery"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

 ALTER TABLE ONLY "IVDelivery_Invoice"
    ADD CONSTRAINT "FK_IVDelivery_Invoice_Invoice" FOREIGN KEY ("OID_FACTURA") REFERENCES "IVInvoice"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "IVDelivery_Ticket"
    ADD CONSTRAINT "FK_IVDelivery_Ticket_Ticket" FOREIGN KEY ("OID_TICKET") REFERENCES "IVTicket"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "IVInvoice"
    ADD CONSTRAINT "FK_IVInvoice_Client" FOREIGN KEY ("OID_CLIENTE") REFERENCES "IVClient"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "IVInvoiceLine"
    ADD CONSTRAINT "FK_IVInvoiceLine_IVInvoice" FOREIGN KEY ("OID_FACTURA") REFERENCES "IVInvoice"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "IVInvoiceLine"
    ADD CONSTRAINT "FK_IVInvoiceLine_IVDeliveryLine" FOREIGN KEY ("OID_CONCEPTO_ALBARAN") REFERENCES "IVDeliveryLine"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;
	
ALTER TABLE ONLY "IVOrder"
    ADD CONSTRAINT "FK_IVOrder_Client" FOREIGN KEY ("OID_CLIENTE") REFERENCES "IVClient"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;	

ALTER TABLE ONLY "IVOrderLine"
    ADD CONSTRAINT "FK_IVOrderLine_Order" FOREIGN KEY ("OID_PEDIDO") REFERENCES "IVOrder"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;