SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.4.7.2' WHERE "NAME" = 'INVOICE_DB_VERSION';

SET SEARCH_PATH = "0001";

UPDATE "IVCashLine" as a 
	SET "N_PROVEEDOR" = trim(to_char("N_CLIENTE", '0000000'))
WHERE a."N_CLIENTE" != 0;