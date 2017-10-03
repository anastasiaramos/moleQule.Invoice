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
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class TraspasoInfo : ReadOnlyBaseEx<TraspasoInfo>, IBankLineInfo, IEntidadRegistroInfo
	{
		#region IBankLineInfo

		public virtual ETipoTitular ETipoTitular { get { return ETipoTitular.Todos; } }
		public virtual string Titular { get { return _base.CuentaOrigen; } set { } }
		public virtual string CuentaBancaria { get { return _base.CuentaDestino; } set { } }
        public virtual long OidCuenta { get { return _base.Record.OidCuentaOrigen; } set { } }
		public virtual EMedioPago EMedioPago { get { return EMedioPago.Transferencia; } }
		public virtual DateTime Vencimiento { get { return _base.Record.Fecha; } set { } }
		public virtual bool Confirmado { get { return true; } }

        #endregion

        #region IEntidadRegistroInfo

        public ETipoEntidad ETipoEntidad { get { return ETipoEntidad.Traspaso; } }
        public string DescripcionRegistro { get { return "TRASPASO Nº " + Codigo + " de " + Fecha.ToShortDateString() + " de " + Importe.ToString("C2") + " de " + CuentaBancaria; } }

        #endregion

		#region Attributes

		protected BankTransferBase _base = new BankTransferBase();

		#endregion
		
		#region Properties

		public BankTransferBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidCuentaOrigen { get { return _base.Record.OidCuentaOrigen; } }
		public long OidCuentaDestino { get { return _base.Record.OidCuentaDestino; } }
		public long OidUsuario { get { return _base.Record.OidUsuario; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Estado { get { return _base.Record.Estado; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public Decimal Importe { get { return _base.Record.Importe; } set { _base.Record.Importe = value; } }
		public long TipoMovimiento { get { return _base.Record.TipoMovimiento; } }
		public Decimal GastosBancarios { get { return _base.Record.GastosBancarios; } }
		public DateTime FechaRecepcion { get { return _base.Record.FechaRecepcion; } }

		public virtual EEstado EEstado { get { return _base.EStatus; } set { _base.Record.Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual EBankLineType ETipoMovimientoBanco { get { return _base.ETipoMovimientoBanco; } set { _base.Record.TipoMovimiento = (long)value; } }
		public virtual string CuentaOrigen { get { return _base.CuentaOrigen; } set { _base.CuentaOrigen = value; } }
		public virtual string CuentaDestino { get { return _base.CuentaDestino; } set { _base.CuentaDestino = value; } }
		public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }

		#endregion
		
		#region Business Methods
			
		public void CopyFrom(Traspaso source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected TraspasoInfo() { /* require use of factory methods */ }
		private TraspasoInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal TraspasoInfo(Traspaso item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}

		public static TraspasoInfo GetChild(int sessioCode, IDataReader reader, bool childs = false)
        {
			return new TraspasoInfo(sessioCode, reader, childs);
		}

		public static TraspasoInfo New(long oid = 0) { return new TraspasoInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods

		public static TraspasoInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = Traspaso.GetCriteria(Traspaso.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = TraspasoInfo.SELECT(oid);
	
			TraspasoInfo obj = DataPortal.Fetch<TraspasoInfo>(criteria);
			Traspaso.CloseSession(criteria.SessionCode);
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
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}
		
		#endregion
					
        #region SQL

        public static string SELECT(long oid) { return Traspaso.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return Traspaso.SELECT(conditions, false); }
		
        #endregion		
	}
}
