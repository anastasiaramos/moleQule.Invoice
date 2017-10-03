-- INVOICE MODULE COMMON DATA SCRIPT

-- INSERTS

INSERT INTO "COMMON"."Variable" ("NAME", "VALUE") VALUES ('INVOICE_DB_VERSION', '7.4.6.2');

INSERT INTO "Setting" ("NAME", "COPY") VALUES ('SALIDA_CONTABILIDAD_FOLDER', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('DEFAULT_SERIE_VENTA', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('NOTIFY_FACTURAS_EMITIDAS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('NOTIFY_COBROS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('NOTIFY_PLAZO_FACTURAS_EMITIDAS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('NOTIFY_PLAZO_COBROS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_FACTURAS_EXPLOTACION', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_FACTURAS_ACREEDORES', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_OTROS_GASTOS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_GASTOS_NOMINAS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_EFECTOS_PENDIENTES_VTO', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_PAGOS_ESTIMADOS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_FACTURAS_EMITIDAS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_EXISTENCIAS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_EFECTOS_NEGOCIADOS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_EFECTOS_PENDIENTES', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_AYUDAS_PENDIENTES', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_AYUDAS_COBRADAS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('BALANCE_PRINT_BANCOS', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('DEFAULT_TICKET_PRINTER', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('DEFAULT_ALMACEN', TRUE);

INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", '' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'SALIDA_CONTABILIDAD_FOLDER');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", '' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'DEFAULT_SERIE_VENTA');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", '7' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'NOTIFY_FACTURAS_EMITIDAS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", '7' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'NOTIFY_COBROS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", '7' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'NOTIFY_PLAZO_FACTURAS_EMITIDAS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", '7' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'NOTIFY_PLAZO_COBROS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_FACTURAS_EXPLOTACION');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_FACTURAS_ACREEDORES');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_OTROS_GASTOS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_GASTOS_NOMINAS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_EFECTOS_PENDIENTES_VTO');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_PAGOS_ESTIMADOS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_FACTURAS_EMITIDAS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_EXISTENCIAS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_EFECTOS_NEGOCIADOS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_EFECTOS_PENDIENTES');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_AYUDAS_PENDIENTES');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_AYUDAS_COBRADAS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'BALANCE_PRINT_BANCOS');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", '' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'DEFAULT_TICKET_PRINTER');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", '1' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'DEFAULT_ALMACEN');

INSERT INTO "COMMON"."IVClientType" ("VALOR") VALUES ('EMPRESA');
INSERT INTO "COMMON"."IVClientType" ("VALOR") VALUES ('FUNDACION');
INSERT INTO "COMMON"."IVClientType" ("VALOR") VALUES ('PARTICULAR');
INSERT INTO "COMMON"."IVClientType" ("VALOR") VALUES ('ORGANISMO');
INSERT INTO "COMMON"."IVClientType" ("VALOR") VALUES ('INSTITUTO');

-- HIPATIA INSERTS

INSERT INTO "COMMON"."HPEntityType" ("VALOR", "USER_CREATED", "COMMON_SCHEMA") VALUES ('Cliente', FALSE, TRUE);
INSERT INTO "COMMON"."HPEntityType" ("VALOR", "USER_CREATED", "COMMON_SCHEMA") VALUES ('Cobro', FALSE, FALSE);
INSERT INTO "COMMON"."HPEntityType" ("VALOR", "USER_CREATED", "COMMON_SCHEMA") VALUES ('Factura', FALSE, FALSE);

INSERT INTO "COMMON"."HPEntity" ("TIPO", "OBSERVACIONES") VALUES ('Cliente', 'Clientes');
INSERT INTO "COMMON"."HPEntity" ("TIPO", "OBSERVACIONES") VALUES ('Cobro', 'Cobros');
INSERT INTO "COMMON"."HPEntity" ("TIPO", "OBSERVACIONES") VALUES ('Factura', 'Facturas Emitidas');

-- SECURE ITEMS INSERTS

INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Clientes', '301', 'CLIENTE');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Facturas Emitidas', '302', 'FACTURA_EMITIDA');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Movimientos a Bancos', '303', 'MOVIMIENTO_BANCO');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Facturas Proforma', '304', 'PROFORMA');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Cajas', '305', 'CAJA');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Albaranes Emitidos', '306', 'ALBARAN_EMITIDO');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Cobros', '307', 'COBRO');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Cuentas Contables', '308', 'CUENTA_CONTABLE');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Transaction', '309', 'TRANSACTION');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Auditar Movimientos Bancarios', '310', 'AUDITAR_MOVIMIENTOS');

-- ITEM MAP INSERTS

-- ALBARAN_EMITIDO 	-> VARIABLE
--					-> EMPRESA

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'ALBARAN_EMITIDO' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'ALBARAN_EMITIDO' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'ALBARAN_EMITIDO' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'ALBARAN_EMITIDO' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'ALBARAN_EMITIDO' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'ALBARAN_EMITIDO' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'ALBARAN_EMITIDO' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'ALBARAN_EMITIDO' AND ASI."DESCRIPTOR" = 'EMPRESA';
	
-- CAJA 	-> VARIABLE
--			-> EMPRESA

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CAJA' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CAJA' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CAJA' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CAJA' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CAJA' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CAJA' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CAJA' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CAJA' AND ASI."DESCRIPTOR" = 'EMPRESA';
	
-- CLIENTE 	-> FACTURA_EMITIDA 
--			-> (RCM) -> AUXILIARES(R), PRODUCTO(R)
--			-> (CM) -> DOCUMENTO(R)
-- 			-> VARIABLE
--			-> EMPRESA

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'AUXILIARES';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'PRODUCTO';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'AUXILIARES';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'PRODUCTO';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'DOCUMENTO';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'AUXILIARES';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'PRODUCTO';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'DOCUMENTO';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'FACTURA_EMITIDA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '2' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'FACTURA_EMITIDA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'FACTURA_EMITIDA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '4' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'FACTURA_EMITIDA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'CLIENTE' AND ASI."DESCRIPTOR" = 'EMPRESA';

-- COBRO 	-> VARIABLE
--			-> EMPRESA
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'COBRO' AND ASI."DESCRIPTOR" = 'VARIABLE';	

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'COBRO' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'COBRO' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'COBRO' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'COBRO' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'COBRO' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'COBRO' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'COBRO' AND ASI."DESCRIPTOR" = 'EMPRESA';
	
-- FACTURA_EMITIDA 	-> VARIABLE
--					-> EMPRESA
-- 					-> EXPEDIENTE 
-- 					-> MOVIMIENTO_BANCO (C -> C, M -> M, D -> M)
-- 					-> (C, M) -> EXPEDIENTE (R, M), CLIENTE(R), AUXILIARES(R), PRODUCTO(R), FACTURA_RECIBIDA(R)
-- 					-> (M) -> PROVEEDOR(R), CLIENTE(M)
-- 					-> (R) -> CLIENTE(R), PROVEEDOR(R), FACTURA_RECIBIDA(R), AUXILIARES(R)

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'EMPRESA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'EMPRESA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'EMPRESA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'EMPRESA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'PROVEEDOR'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'CLIENTE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'FACTURA_RECIBIDA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'AUXILIARES'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'PROVEEDOR'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'CLIENTE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'PROVEEDOR'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'CLIENTE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'AUXILIARES'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'AUXILIARES'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'PRODUCTO'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'PRODUCTO'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'FACTURA_RECIBIDA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'FACTURA_RECIBIDA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'EXPEDIENTE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'EXPEDIENTE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'EXPEDIENTE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '2' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'MOVIMIENTO_BANCO'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'MOVIMIENTO_BANCO'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'MOVIMIENTO_BANCO'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'EXPEDIENTE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '2' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'EXPEDIENTE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '3' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'EXPEDIENTE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '4' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'FACTURA_EMITIDA' AND ASI."DESCRIPTOR" = 'EXPEDIENTE';
	
-- MOVIMIENTO_BANCO	-> VARIABLE
--					-> EMPRESA

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'MOVIMIENTO_BANCO' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'MOVIMIENTO_BANCO' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'MOVIMIENTO_BANCO' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'MOVIMIENTO_BANCO' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'MOVIMIENTO_BANCO' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'MOVIMIENTO_BANCO' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'MOVIMIENTO_BANCO' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'MOVIMIENTO_BANCO' AND ASI."DESCRIPTOR" = 'EMPRESA';

-- PROFORMA	-> VARIABLE
--			-> EMPRESA

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'PROFORMA' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'PROFORMA' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'PROFORMA' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'PROFORMA' AND ASI."DESCRIPTOR" = 'VARIABLE';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'PROFORMA' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'PROFORMA' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'PROFORMA' AND ASI."DESCRIPTOR" = 'EMPRESA';

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'PROFORMA' AND ASI."DESCRIPTOR" = 'EMPRESA';