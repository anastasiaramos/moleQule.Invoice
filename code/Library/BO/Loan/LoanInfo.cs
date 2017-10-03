using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class LoanInfo : ReadOnlyBaseEx<LoanInfo>, IBankLineInfo, ITransactionPayment, IEntidadRegistroInfo
    {
        #region IEntidadRegistroInfo

        public ETipoEntidad ETipoEntidad { get { return ETipoEntidad.Prestamo; } }
        public string DescripcionRegistro { get { return "PRÉSTAMO Nº " + Codigo + " de " + Fecha.ToShortDateString() + " de " + Total.ToString("C2") + " de " + Entidad; } }

        #endregion

        #region ITransactionPayment

        public decimal TotalPagado { get { return _base._total_pagado; } set { _base._total_pagado = value; } }
        public decimal Asignado { get { return _base._asignado; } set { _base._asignado = value; } }
        public decimal Pendiente { get { return _base._pendiente; } set { _base._pendiente = value; } }
		public decimal PendienteAsignar { get { return Math.Min(_base._pendiente, _base._pendiente_asignar); } set { _base._pendiente_asignar = value; } }
		public decimal Acumulado { get { return _base._pendiente_asignar; } set { } }
        public string FechaAsignacion { get { return (_base._fecha_asignacion != DateTime.MinValue) ? _base._fecha_asignacion.ToShortDateString() : "---"; } set { _base._fecha_asignacion = DateTime.Parse(value); } }
        public string Vinculado { get { return _base._vinculado; } set { _base._vinculado = value; } }
        public decimal Total { get { return _base.Record.ImporteCuota * _base.Record.NCuotas; } set { } }
        public long OidExpediente { get { return 0; } set { } }
        public ETipoAcreedor ETipoAcreedor { get { return ETipoAcreedor.Todos; } set { } }

        public string NFactura { get { return _base.Record.Codigo; } set { _base.Record.Codigo = value; } }
        public string Pago { get { return _base.Pago; } }

        #endregion

        #region IBankLineInfo

        public long TipoMovimiento { get { return (long)EBankLineType.Prestamo; } }
        public EBankLineType ETipoMovimientoBanco { get { return EBankLineType.Prestamo; } }
        public virtual ETipoTitular ETipoTitular { get { return ETipoTitular.Todos; } }
        public virtual string Titular { get { return CuentaBancaria; } set { } }
        public virtual EMedioPago EMedioPago { get { return EMedioPago.Transferencia; } }
        public virtual DateTime Vencimiento { get { return FechaVencimiento; } set { } }
        public virtual bool Confirmado { get { return true; } }
        public virtual DateTime Fecha { get { return FechaIngreso; } }

        #endregion

		#region Attributes

		protected LoanBase _base = new LoanBase();

        protected InterestRateList _interest_rates = null;
        protected PaymentList _payments = null;
        		
		#endregion
		
		#region Properties

		public LoanBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidCuenta { get { return _base.Record.OidCuenta; } }
		public DateTime FechaFirma { get { return _base.Record.FechaFirma; } }
		public DateTime FechaIngreso { get { return _base.Record.FechaIngreso; } }
		public DateTime FechaVencimiento { get { return _base.Record.FechaVencimiento; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public Decimal Importe { get { return _base.Record.Importe; } set { _base.Record.Importe = value; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long OidPago { get { return _base.Record.OidPago; } }
		public long NCuotas { get { return _base.Record.NCuotas; } }
		public DateTime InicioPago { get { return _base.Record.InicioPago; } }
		public long Periodicidad { get { return _base.Record.PeriodoPago; } }
		public Decimal ImporteCuota { get { return Decimal.Round(_base.Record.ImporteCuota, 2); } }
        public string CuentaContable { get { return _base.Record.CuentaContable; } }
        public Decimal GastosBancarios { get { return Decimal.Round(_base.Record.GastosBancarios, 2); } }
        public bool GastosInicio { get { return _base.Record.GastosInicio; } }
        public long Estado { get { return _base.Record.Estado; } }

        public EEstado EEstado { get { return _base.EStatus; } }
        public string EstadoLabel { get { return _base.StatusLabel; } }
        public decimal CapitalAmortizado { get { return _base.CapitalAmortizado; } }
        public decimal CapitalPendiente { get { return _base.CapitalPendiente; } }
        public decimal PartialUnpaid { get { return _base.PartialUnpaid; } set { _base.PartialUnpaid = value; } }
        public string CuentaBancaria { get { return _base.CuentaBancaria; } }
        public string CuentaBancariaAsociada { get { return _base.CuentaBancariaAsociada; } }
        public string Entidad { get { return _base.Entidad; } }

        public virtual InterestRateList InterestRates { get { return _interest_rates; } }
        public virtual PaymentList Payments { get { return _payments; } }
        		
		#endregion
		
		#region Business Methods

        public void CopyFrom(Loan source) { _base.CopyValues(source); }

        public void Vincula()
        {
            Vinculado = Library.Store.Resources.Labels.RESET_PAGO;
            Asignado = Importe;
            Pendiente = 0;
        }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected LoanInfo() { /* require use of factory methods */ }
		private LoanInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
            Childs = childs;
			Fetch(reader);
		}
        internal LoanInfo(Loan item, bool copy_childs)
        {
            _base.CopyValues(item);

            if (copy_childs)
            {
                _interest_rates = (item.InterestRates != null) ? InterestRateList.GetChildList(item.InterestRates) : null;
                _payments = (item.Payments != null) ? PaymentList.GetChildList(item.Payments) : null;
            }
        }

		public static LoanInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new LoanInfo(sessionCode, reader, childs);
		}

		public static LoanInfo New(long oid = 0) { return new LoanInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        public static LoanInfo Get(long oid) { return Get(oid, false); }
		public static LoanInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = Loan.GetCriteria(Loan.OpenSession());
            criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = LoanInfo.SELECT(oid);
	
			LoanInfo obj = DataPortal.Fetch<LoanInfo>(criteria);
			Loan.CloseSession(criteria.SessionCode);
			return obj;
		}
		
		#endregion
					
		#region Common Data Access
								
        /// <summary>
        /// Obtiene un objeto a partir de un <see cref="IDataReader"/>.
        /// Obtiene los hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria"><see cref="IDataReader"/> con los datos</param>
        /// <remarks>
        /// La utiliza el <see cref="ReadOnlyListBaseEx"/> correspondiente para construir los objetos de la lista
        /// </remarks>
		private void Fetch(IDataReader source)
		{
			try
			{
                _base.CopyValues(source);

                if (Childs)
                {
                    string query = string.Empty;
                    IDataReader reader;

                    query = InterestRateList.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _interest_rates = InterestRateList.GetChildList(SessionCode, reader);

                    query = PaymentList.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _payments = PaymentList.GetChildList(SessionCode, reader);
                }
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
		#region Root Data Access
		 
        /// <summary>
        /// Obtiene un registro de la base de datos
        /// </summary>
        /// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
        /// <remarks>
        /// La llama el DataPortal
        /// </remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;
			
			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        query = InterestRateList.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _interest_rates = InterestRateList.GetChildList(SessionCode, reader);

                        query = PaymentList.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _payments = PaymentList.GetChildList(SessionCode, reader);
                    }
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}
		
		#endregion
					
        #region SQL

        public static string SELECT(long oid) { return Loan.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return Loan.SELECT(conditions, false); }
		
        #endregion		
	}
}