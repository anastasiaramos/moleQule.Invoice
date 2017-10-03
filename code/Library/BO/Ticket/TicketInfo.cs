using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;
using moleQule.Library.Hipatia;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
    [Serializable()]
    public class TicketInfo : ReadOnlyBaseEx<TicketInfo>, IAgenteHipatia, IEntidadRegistroInfo, IBankLineInfo
    {
        #region IAgenteHipatia

        public string IDHipatia { get { return Fecha.Year + "/" + Codigo; } }
        public string NombreHipatia { get { return "Ticket de Venta " + IDHipatia; } }
		public Type TipoEntidad { get { return typeof(Ticket); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion

		#region IEntidadRegistroInfo

		public ETipoEntidad ETipoEntidad { get { return ETipoEntidad.Ticket; } }
		public string DescripcionRegistro { get { return "TICKET Nº " + NTicket + " de " + Fecha.ToShortDateString(); } }

		#endregion

		#region IBankLineInfo

		public long TipoMovimiento { get { return (long)EBankLineType.Ticket; } }
		public EBankLineType ETipoMovimientoBanco { get { return EBankLineType.Ticket; } }
		public ETipoTitular ETipoTitular { get { return ETipoTitular.Cliente; } }
		public string CodigoTitular { get { return string.Empty; } set { } }
		public string Titular { get { return string.Empty; } set { } }
        public long OidCuenta { get { return 0; } set { } }
		public string CuentaBancaria { get { return string.Empty; } set { } }
		public decimal Importe { get { return _base.Record.Total; } set { } }
        public decimal GastosBancarios { get { return 0; } }
		public DateTime Vencimiento { get { return _base.Record.PrevisionPago; } set { } }
		public bool Confirmado { get { return true; } } 

		#endregion

        #region Attributes

		protected TicketBase _base = new TicketBase();

        protected ConceptoTicketList _concepto_tickets = null;
		protected AlbaranTicketList _albaran_tickets = null;

        protected string _numero_serie;
		protected string _serie = string.Empty;
		protected string _tpv = string.Empty;
        protected string _id_mov_contable = string.Empty;
        protected string _usuario = string.Empty;

        #endregion

        #region Properties

		public TicketBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidSerie { get { return _base.Record.OidSerie; } }
		public long OidTpv { get { return _base.Record.OidTpv; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Estado { get { return _base.Record.Estado; } }
		public long Tipo { get { return _base.Record.Tipo; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public Decimal BaseImponible { get { return _base.Record.BaseImponible; } }
		public Decimal Impuestos { get { return _base.Record.Impuestos; } }
		public Decimal Descuento { get { return _base.Record.Descuento; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public long FormaPago { get { return _base.Record.FormaPago; } }
		public long DiasPago { get { return _base.Record.DiasPago; } }
		public long MedioPago { get { return _base.Record.MedioPago; } }
		public DateTime Prevision { get { return _base.Record.PrevisionPago; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public string Albaranes { get { return _base.Record.Albaranes; } }
		public int OidUsuario { get { return _base.Record.OidUsuario; } }

        public virtual ConceptoTicketList ConceptoTickets { get { return _concepto_tickets; } }
		public virtual AlbaranTicketList AlbaranTickets { get { return _albaran_tickets; } }

        //CAMPOS NO ENLAZADOS
		public virtual EEstado EEstado { get { return _base.EStatus; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual ETipoFactura ETipoFactura { get { return _base.ETipoFactura; } }
		public virtual string TipoFacturaLabel { get { return _base.TipoFacturaLabel; } }
		public virtual string NTicket { get { return _base.NTicket; } }
		public virtual string NumeroSerie { get { return _base.NumeroSerie; } set { _base.NumeroSerie = value; } }
		public virtual string Serie { get { return _base.Serie; } set { _base.Serie = value; } }
		public virtual string TPV { get { return _base.TPV; } set { _base.TPV = value; ; } }
		public virtual Decimal Subtotal { get { return _base.Subtotal; } }
		public virtual bool NTicketManual { get { return _base.NTicketManual; } set { _base.NTicketManual = value; } }
		public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } }
		public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public virtual string IDMovimientoContable { get { return _base.IDMovimientoContable; } }
		public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }

        #endregion

        #region Business Methods

        public void CopyFrom(Ticket source) { _base.CopyValues(source); }

        public List<CuentaResumen> GetCuentas()
        {
			try
			{
				List<CuentaResumen> list = new List<CuentaResumen>();
				ProductList productos = ProductList.GetList(false, true);
				FamiliaList familias = FamiliaList.GetList(false, true);
				bool nuevo;
				ProductInfo producto;
				FamiliaInfo familia;
				string cuenta;

				foreach (ConceptoTicketInfo item in _concepto_tickets)
				{
					nuevo = true;
					producto = productos.GetItem(item.OidProducto);
					familia = familias.GetItem(producto.OidFamilia);

					cuenta = (producto.CuentaContableVenta == string.Empty) ? familia.CuentaContableVenta : producto.CuentaContableVenta;

					//Agrupamos los conceptos por cuentas contables
					for (int i = 0; i < list.Count; i++)
					{
						CuentaResumen cr = list[i];

						//Tiene prioridad la cuenta contable del producto
						if (producto.CuentaContableVenta != string.Empty)
						{
							if (cr.CuentaContable == producto.CuentaContableVenta)
							{
								cr.Importe += item.BaseImponible;
								list[i] = cr;
								nuevo = false;
								break;
							}
						}
						//Luego la de la familia
						else if (cr.CuentaContable == familia.CuentaContableVenta)
						{
							cr.Importe += item.BaseImponible;
							list[i] = cr;
							nuevo = false;
							break;
						}
					}

					if (nuevo)
						list.Add(new CuentaResumen { OidFamilia = producto.OidFamilia, Importe = item.BaseImponible, CuentaContable = cuenta });
				}

				return list;
			}
			catch 
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_CUENTA, NTicket, string.Empty));
			}
		}

        public List<ImpuestoResumen> GetImpuestos()
        {
			try
			{
				ImpuestoList impuestos = ImpuestoList.GetList(false);
				List<ImpuestoResumen> list = new List<ImpuestoResumen>();
				bool nuevo;

				foreach (ConceptoTicketInfo item in _concepto_tickets)
				{
					if (item.Impuestos == 0) continue;

					nuevo = true;

					//Agrupamos los conceptos por tipo de impuesto devengado
					for (int i = 0; i < list.Count; i++)
					{
						ImpuestoResumen cr = list[i];

						if (cr.OidImpuesto == item.OidImpuesto)
						{
							cr.Importe += item.Impuestos;
							cr.BaseImponible += item.BaseImponible;
							list[i] = cr;
							nuevo = false;
							break;
						}
					}

					if (nuevo)
						list.Add(new ImpuestoResumen { 
														Nombre = impuestos.GetItem(item.OidImpuesto).Nombre,
														OidImpuesto = item.OidImpuesto, 
														Importe = item.Impuestos, 
														BaseImponible = item.BaseImponible 
						});
				}

				return list;
			}
			catch 
			{
				throw new iQException(String.Format(Resources.Messages.ERROR_FACTURA_IMPUESTO, NTicket, string.Empty));
			}
        }

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
        protected TicketInfo() { /* require use of factory methods */ }
        internal TicketInfo(Ticket item, bool copy_childs)
        {
            _base.CopyValues(item);

            if (copy_childs)
            {
                _concepto_tickets = (item.ConceptoTickets != null) ? ConceptoTicketList.GetChildList(item.ConceptoTickets) : null;
				_albaran_tickets = (item.AlbaranTickets != null) ? AlbaranTicketList.GetChildList(item.AlbaranTickets) : null;
            }
        }
        
		public virtual void LoadChilds(Type type, bool get_childs)
		{
			/*if (type.Equals(typeof(AlbaranTicketInfo)))
			{
				_albaran_tickets = AlbaranTicketList.GetChildList(this, get_childs);
			}*/
		}

        #endregion

		#region Child Factory Methods

		private TicketInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}

		public static TicketInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static TicketInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new TicketInfo(sessionCode, reader, childs); }

		#endregion

		#region Root Factory Methods

		public static TicketInfo Get(long oid) { return Get(oid, false); }
        public static TicketInfo Get(long oid, bool childs)
        {
            CriteriaEx criteria = Ticket.GetCriteria(Ticket.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = TicketInfo.SELECT(oid);

            TicketInfo obj = DataPortal.Fetch<TicketInfo>(criteria);
            Ticket.CloseSession(criteria.SessionCode);
            return obj;
        }
		public static TicketInfo GetByAlbaran(long oid, bool childs)
		{
			CriteriaEx criteria = Ticket.GetCriteria(Ticket.OpenSession());
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions { OutputDelivery = OutputDelivery.New().GetInfo(false) };
			conditions.OutputDelivery.Oid = oid;

			criteria.Query = TicketInfo.SELECT(conditions);

			TicketInfo obj = DataPortal.Fetch<TicketInfo>(criteria);
			Ticket.CloseSession(criteria.SessionCode);
			return obj;
		}
		public static TicketInfo GetByCode(string code, long oid_serie, int year, bool childs)
		{
			CriteriaEx criteria = Ticket.GetCriteria(Ticket.OpenSession());
			criteria.Childs = childs;

			Ticket fac = Ticket.New();
			Serie ser = moleQule.Library.Store.Serie.New();
			fac.Codigo = code;
			ser.Oid = oid_serie;
			
			QueryConditions conditions = new QueryConditions
			{
				Ticket = fac.GetInfo(false),
				Serie = ser.GetInfo(false),
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = TicketInfo.SELECT_BY_CODE(conditions);

			TicketInfo obj = DataPortal.Fetch<TicketInfo>(criteria);
			Ticket.CloseSession(criteria.SessionCode);
			return obj;
		}

        #endregion

        #region Root Data Access

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

                        query = ConceptoTicketList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _concepto_tickets = ConceptoTicketList.GetChildList(SessionCode, reader);

						query = AlbaranTicketList.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_albaran_tickets = AlbaranTicketList.GetChildList(SessionCode, reader);
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

                    query = ConceptoTicketList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
					_concepto_tickets = ConceptoTicketList.GetChildList(SessionCode, reader);

					query = AlbaranTicketList.SELECT(this);
					reader = nHMng.SQLNativeSelect(query, Session());
					_albaran_tickets = AlbaranTicketList.GetChildList(SessionCode, reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

        #region SQL

        public static string SELECT(long oid) { return Ticket.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return Ticket.SELECT(conditions, false); }
		public static string SELECT_BY_CODE(QueryConditions conditions) { return Ticket.SELECT_BY_CODE(conditions, false); }

        #endregion

    }

    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class SerialTicketInfo : SerialInfo
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
        protected SerialTicketInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods

        public static SerialTicketInfo Get(long oid_serie, int year, ETipoFactura tipo)
        {
            CriteriaEx criteria = Ticket.GetCriteria(Ticket.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(oid_serie, year, tipo);

            SerialTicketInfo obj = DataPortal.Fetch<SerialTicketInfo>(criteria);
            Ticket.CloseSession(criteria.SessionCode);
            return obj;
        }

		public static long GetNext(long oid_serie, int year, ETipoFactura tipo)
        {
            return Get(oid_serie, year, tipo).Value + 1;
        }
        
        #endregion

        #region Root Data Access

        #endregion

        #region SQL

		public static string SELECT(long oidSerie, int year, ETipoFactura tipo)
        {
            string tk = nHManager.Instance.GetSQLTable(typeof(TicketRecord));
            string se = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
            string query;

			QueryConditions conditions = new QueryConditions
			{
				Serie = Serie.New().GetInfo(false),
				TipoFactura = tipo,
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};
			conditions.Serie.Oid = oidSerie;			

			query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
					" FROM " + tk + " AS TK" +
					" INNER JOIN " + se + " AS SE ON TK.\"OID_SERIE\" = SE.\"OID\"" +
					Ticket.WHERE(conditions);

            return query;
        }

        #endregion

    }
}
