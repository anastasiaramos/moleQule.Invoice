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
	public class ChargeInfo : ReadOnlyBaseEx<ChargeInfo, Charge>, Hipatia.IAgenteHipatia, IBankLineInfo, IEntidadRegistroInfo
	{
        #region IAgenteHipatia

        public string IDHipatia { get { return IdCobro.ToString(Resources.Defaults.COBRO_CODE_FORMAT); } }
        public string NombreHipatia { get { return "Cobro nº " + IDHipatia + " a " + Cliente; } }
		public Type TipoEntidad { get { return typeof(Charge); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion

        #region IBankLineInfo

        public long TipoMovimiento { get { return (long)EBankLineType.Cobro; } }
        public EBankLineType ETipoMovimientoBanco { get { return EBankLineType.Cobro; } }
		public ETipoTitular ETipoTitular { get { return EnumConvert.ToETipoTitular(ETipoCobro); } }
		public string Titular { get { return Cliente; } set { } }
		public bool Confirmado { get { return EEstadoCobro == EEstado.Charged; } }
        public long OidCuenta { get { return 0; } }

        #endregion

		#region IEntidadRegistroInfo

		public ETipoEntidad ETipoEntidad { get { return ETipoEntidad.Cobro; } }
		public string DescripcionRegistro { get { return "COBRO Nº " + IDCobroLabel + " de " + Fecha.ToShortDateString() + " de " + Importe.ToString("C2") + " de " + Cliente; } }

		#endregion

		#region Attributes

		protected ChargeBase _base = new ChargeBase();

		protected CobroFacturaList _cobro_facturas = null;
        protected CobroREAList _cobro_reas = null;
		
		#endregion
		
		#region Properties

		public ChargeBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidCliente { get { return _base.Record.OidCliente; } }
		public int OidUsuario { get { return _base.Record.OidUsuario; } }
		public long IdCobro { get { return _base.Record.IdCobro; } }
		public int TipoCobro { get { return _base.Record.TipoCobro; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public Decimal Importe { get { return _base.Record.Importe; } set { _base.Record.Importe = value; } }
		public long MedioPago { get { return _base.Record.MedioPago; } }
		public DateTime Vencimiento { get { return _base.Record.Vencimiento; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public long OidCuentaBancaria { get { return _base.Record.OidCuentaBancaria; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long OidTPV { get { return _base.Record.OidTpv; } }
		public long Estado { get { return _base.Record.Estado; } }
		public long EstadoCobro { get { return _base.Record.EstadoCobro; } }
		public string IdMovContable { get { return _base.Record.IdMovContable; } }
		public Decimal GastosBancarios { get { return _base.Record.GastosBancarios; } }

		public CobroFacturaList CobroFacturas { get { return _cobro_facturas; } }
        public CobroREAList CobroREAs { get { return _cobro_reas; } }

		public EEstado EEstado { get { return _base.EStatus; } set { _base.EStatus = value; } }
		public string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
		public EEstado EEstadoCobro { get { return _base.EEstadoCobro; } set { _base.EEstadoCobro = value; } }
		public string EstadoCobroLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstadoCobro); } }
		public string IDCobroLabel { get { return _base.IDCobroLabel; } }
		public ETipoCobro ETipoCobro { get { return (ETipoCobro)TipoCobro; } set { _base.ETipoCobro = value; } }
		public string ETipoCobroLabel { get { return EnumText<ETipoCobro>.GetLabel(ETipoCobro); } }
		public EMedioPago EMedioPago { get { return _base.EMedioPago; } set { _base.Record.MedioPago = (long)value; } }
		public string EMedioPagoLabel { get { return Common.EnumText<EMedioPago>.GetLabel(EMedioPago); } }
		public string CuentaBancaria { get { return _base.CuentaBancaria; } set { _base.CuentaBancaria = value; } }
		public string Entidad { get { return _base.Entidad; } set { _base.Entidad = value; } }
		public string TPV { get { return _base.TPV; } set { _base.TPV = value; } }
		public string IDCliente { get { return _base.IDCliente; } set { _base.IDCliente = value; } } /*DEPRECATED*/
		public string Cliente { get { return _base.Cliente; } set { _base.Cliente = value; } }
		public string NumCliente { get { return _base.IDCliente; } set { _base.IDCliente = value; } } /*DEPRECATED*/
		public string NCliente { get { return _base.IDCliente; } } /*DEPRECATED*/
		public string IDLineaCaja { get { return _base.IDLineaCaja; } }
		public string IDMovimientoBanco { get { return _base.IDMovimientoBanco; } }
		public string IDMovimientoContable { get { return _base.IDMovimientoContable; } }
		public string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }
		public Decimal EfectoPendienteVto { get { return Decimal.Round(((Vencimiento > DateTime.Today) && (EEstadoCobro == EEstado.Charged)) ? Importe : 0, 2); } }
		public decimal Pendiente { get { return (CobroFacturas == null) ? Importe : Importe - CobroFacturas.GetTotal(); } }
		public decimal PendienteREA { get { return (CobroREAs == null) ? Importe : Importe - CobroREAs.GetTotal(); } }
		public Decimal PendienteAsignacion { get { return (_cobro_facturas == null) ? _base.Pendiente : Importe - _cobro_facturas.GetTotal(); } }
		public Decimal PendienteAsignacionREA { get { return (_cobro_reas == null) ? _base.Pendiente : Importe - _cobro_reas.GetTotal(); } }
		public decimal GastosDemora { get { return _base.GastosDemora; } set { _base.GastosDemora = value; } }
        public string IDEfecto { get { return _base.IDEfecto; } }

		#endregion
		
		#region Business Methods

        public void CopyFrom(Charge source) { _base.CopyValues(source); }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected ChargeInfo() { /* require use of factory methods */ }
		private ChargeInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
		internal ChargeInfo(Charge item, bool childs)
		{
			_base.CopyValues(item);

			if (childs)
			{
                if (OidCliente > 0)
                    _cobro_facturas = (item.CobroFacturas != null) ? CobroFacturaList.GetChildList(item.CobroFacturas) : null;
                else
                    _cobro_reas = (item.CobroREAs != null) ? CobroREAList.GetChildList(item.CobroREAs) : null;
			}
		}
	
		public static ChargeInfo GetChild(IDataReader reader) { return GetChild(reader, false); }
		public static ChargeInfo GetChild(IDataReader reader, bool childs) {	return new ChargeInfo(reader, childs); }

		public virtual void LoadChilds(IList<CobroFacturaInfo> list) { _cobro_facturas = CobroFacturaList.GetChildList(list); }
		public virtual void LoadChilds(IList<CobroREAInfo> list) { _cobro_reas = CobroREAList.GetChildList(list); }

		public static ChargeInfo New(long oid = 0, ETipoCobro tipo = ETipoCobro.Todos) { return new ChargeInfo() { Oid = oid, ETipoCobro = tipo }; }

 		#endregion
		
		#region Root Factory Methods

		public new static ChargeInfo Get(string query, bool childs = false)
		{
			if (!Charge.CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return ReadOnlyBaseEx<ChargeInfo, Charge>.Get(query, childs);
		}

		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static ChargeInfo Get(long oid, bool childs = false) { return Get(ChargeInfo.SELECT(oid, ETipoCobro.Todos), childs); }
		public static ChargeInfo Get(long oid, ETipoCobro tipo, bool childs) { return Get(ChargeInfo.SELECT(oid, tipo), childs); }
		
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
			try
			{
				_base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);
					
                    if (Childs)
					{
						string query = string.Empty;

                        if (OidCliente > 0)
                        {
                            query = CobroFacturaList.SELECT_BY_COBRO(this.Oid);
                            reader = nHMng.SQLNativeSelect(query, Session());
							_cobro_facturas = CobroFacturaList.GetChildList(SessionCode, reader);

							GastosDemora = _cobro_facturas.GastosDemora();
                        }
                        else
                        {
                            query = CobroREAList.SELECT_BY_COBRO(this.Oid);
                            reader = nHMng.SQLNativeSelect(query, Session());
                            _cobro_reas = CobroREAList.GetChildList(SessionCode, reader);
                        }
                    }
				}
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
		}
		
		#endregion
		
		#region Child Data Access
		
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

                    if (OidCliente > 0)
                    {
                        query = CobroFacturaList.SELECT_BY_COBRO(this.Oid);
                        reader = nHMng.SQLNativeSelect(query, Session());
						_cobro_facturas = CobroFacturaList.GetChildList(SessionCode, reader);

						GastosDemora = _cobro_facturas.GastosDemora();
                    }
                    else
                    {
                        query = CobroREAList.SELECT_BY_COBRO(this.Oid);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _cobro_reas = CobroREAList.GetChildList(SessionCode, reader);
                    }
				}
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
		}
		
		#endregion

        #region SQL
        
        public static string SELECT(long oid, ETipoCobro tipo) { return Charge.SELECT(oid, tipo, false); }

        #endregion
    }

    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class SerialCobroInfo : SerialInfo
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Business Methods

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
        protected SerialCobroInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods

		public static SerialCobroInfo Get(long oid_cliente) { return Get(oid_cliente, DateTime.MinValue.Year); }
        public static SerialCobroInfo Get(long oid_cliente, int year)
        {
            CriteriaEx criteria = Charge.GetCriteria(Charge.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(oid_cliente, year);

            SerialCobroInfo obj = DataPortal.Fetch<SerialCobroInfo>(criteria);
            Charge.CloseSession(criteria.SessionCode);
            return obj;
        }

		public static long GetNext(long oid_cliente) { return Get(oid_cliente).Value + 1; }
        public static long GetNext(long oid_cliente, int year) { return Get(oid_cliente, year).Value + 1; }

        #endregion

        #region Root Data Access

        #endregion

        #region SQL

        public static string SELECT(long oidCliente, int year)
        {
            string c = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string query;

			QueryConditions conditions = new QueryConditions
			{
				Cliente = ClienteInfo.New(oidCliente),
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};

			query = "SELECT 0 AS \"OID\", MAX(\"ID_COBRO\") AS \"SERIAL\"" +
					" FROM " + c + " AS C" +
					" WHERE C.\"OID_CLIENTE\" = " + conditions.Cliente.Oid;

			if (year != DateTime.MinValue.Year)
				query += " AND \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

            return query + ";";
        }

        #endregion
    }
}
