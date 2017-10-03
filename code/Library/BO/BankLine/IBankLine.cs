using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	//DEPRECATED: Only for previous versions compatibilty
	public interface IMovimientoBanco : IBankLine{ }
	public interface IMovimientoBancoInfo : IBankLineInfo { }

	public interface IBankLine 
	{
        long Oid { get; }
		EEstado EEstado { get; }
        long Serial { get; }
        string Codigo { get; }
        long TipoMovimiento { get; }
        EBankLineType ETipoMovimientoBanco { get; }
		ETipoTitular ETipoTitular { get; }
		string Titular { get; set; }
        string CodigoTitular { get; set; }
        DateTime Fecha { get; set; }
		DateTime Vencimiento { get; set; }
		EMedioPago EMedioPago { get; }
		decimal Importe { get; set; }
        decimal GastosBancarios { get; set; }
        long OidCuenta { get; set; }
		string CuentaBancaria { get; }
		string Observaciones { get; }
		bool Confirmado { get; }

        int SessionCode { get; }
        ISession Session();

		IBankLineInfo IGetInfo(bool childs);

        /*void BeginEdit();
        void ApplyEdit();
        void CancelEdit();
        void CloseSession();
        IMovimientoBanco IClone();
        IMovimientoBanco ISave();*/
	}	

	public interface IBankLineInfo
    {
        long Oid { get; }
		EEstado EEstado { get; }
        long TipoMovimiento { get; }
        EBankLineType ETipoMovimientoBanco { get; }
        long Serial { get; }
        string Codigo { get; }
		ETipoTitular ETipoTitular { get; }
		string Titular { get; set; }
		EMedioPago EMedioPago { get; }		
		decimal Importe { get; set; }
        decimal GastosBancarios { get; }
		DateTime Fecha { get; }
		DateTime Vencimiento { get;  }
        long OidCuenta { get; }
		string CuentaBancaria { get; }
		string Observaciones { get; }
		bool Confirmado { get; }
    }

    /// <summary>
    /// Agente Acreedor
    /// </summary>
    public interface IMovimientoBancoPrint
    {
        long Oid { get; }
        long TipoOperacion { get; }
        EBankLineType ETipoMovimientoBanco { get; set; }
        long Serial { get; }
        string Codigo { get; }
    }
}