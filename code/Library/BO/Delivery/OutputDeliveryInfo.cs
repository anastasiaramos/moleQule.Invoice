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
using moleQule.Library.Hipatia;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class OutputDeliveryInfo : ReadOnlyBaseEx<OutputDeliveryInfo, OutputDelivery>, IAgenteHipatia, IWorkResource
	{
        #region IAgenteHipatia

        public string IDHipatia { get { return Ano + "/" + Codigo; } }
        public string NombreHipatia { get { return NombreCliente; } }
		public Type TipoEntidad { get { return typeof(OutputDelivery); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion

		#region IWorkResource

		public long EntityType { get { return (long)ETipoEntidad.OutputDelivery; } set { } }
		public ETipoEntidad EEntityType { get { return ETipoEntidad.OutputDelivery; } set { } }
		public string ID { get { return Codigo; } }
		public string Name { get { return Resources.Labels.WORK_DELIVERY; } }
		public decimal Cost { get { return Total; } }

		#endregion

		#region Attributes

		protected OutputDeliveryBase _base = new OutputDeliveryBase();

        protected OutputDeliveryLineList _conceptos = null;

        #endregion

        #region Properties

		public OutputDeliveryBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidSerie { get { return _base.Record.OidSerie; } }
		public long OidHolder { get { return _base.Record.OidHolder; } }
		public long OidTransportista { get { return _base.Record.OidTransportista; } }
		public long HolderType { get { return _base.Record.HolderType; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Ano { get { return _base.Record.Ano; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public long FormaPago { get { return _base.Record.FormaPago; } }
		public long DiasPago { get { return _base.Record.DiasPago; } }
		public long MedioPago { get { return _base.Record.MedioPago; } }
		public DateTime Prevision { get { return _base.Record.PrevisionPago; } }
        public Decimal PDescuento { get { return Decimal.Round(_base.Record.PDescuento, 2); } }
        public Decimal Descuento { get { return _base.Record.Descuento; } }
        public Decimal BaseImponible { get { return _base.Record.BaseImponible; } }
		public Decimal Impuestos { get { return Decimal.Round(_base.Record.Impuestos, 2); } }
		public Decimal Total { get { return Decimal.Round(_base.Record.Total, 2); } }
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } }
		public bool Nota { get { return _base.Record.Nota; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public bool Contado { get { return _base.Record.Contado; } }
		public bool Rectificativo { get { return _base.Record.Rectificativo; } }
		public long OidUsuario { get { return _base.Record.OidUsuario; } }
		public long OidAlmacen { get { return _base.Record.OidAlmacen; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long Estado { get { return _base.Record.Estado; } }
	
		public virtual OutputDeliveryLineList Conceptos { get { return _conceptos; } }

        //CAMPOS NO ENLAZADOS
		public virtual EEstado EEstado { get { return _base.EStatus; } set { _base.EStatus = value; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual ETipoEntidad EHolderType { get { return _base.EHolderType; } }
		public virtual string HolderTypeLabel { get { return _base.HolderTypeLabel; } }
		public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; } }
		public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public virtual string Usuario { get { return _base._owner; } set { _base._owner = value; } }
		public virtual string Expediente { get { return _base.ExpedientID; } set { _base.ExpedientID = value; } }
		public virtual string IDAlmacen { get { return _base._store_id; } set { _base._store_id = value; } }
		public virtual string Almacen { get { return _base._store; } set { _base._store = value; } }
		public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }
		public virtual string NumeroSerie { get { return _base._numero_serie; } set { _base._numero_serie = value; } }
		public virtual string NombreSerie { get { return _base._nombre_serie; } set { _base._nombre_serie = value; } }
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
		public virtual string NumeroCliente { get { return _base._id_cliente; } } /*DEPRECATED*/
		public virtual string IDCliente { get { return _base._id_cliente; } set { _base._id_cliente = value; } }
		public virtual string NombreCliente { get { return _base._cliente; } set { _base._cliente = value; } }
		public virtual Decimal Subtotal { get { return BaseImponible + Descuento; } }
		public virtual bool IDManual { get { return _base._id_manual; } set { _base._id_manual = value; } }
		public virtual string NumeroFactura { get { return _base._numero_factura; } set { _base._numero_factura = value; } }
		public virtual string NumeroTicket { get { return _base._numero_ticket; } set { _base._numero_ticket = value; } }
		public virtual bool Facturado { get { return _base._facturado; } set { _base._facturado = value; } }
		public virtual string FileName { get { return _base.FileName; } }

		#endregion
		
		#region Business Methods

        public void CopyFrom(OutputDelivery source) { _base.CopyValues(source); }
		public void CopyFrom(OutputDeliveryInfo source) { _base.CopyValues(source); }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected OutputDeliveryInfo() { /* require use of factory methods */ }
		private OutputDeliveryInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal OutputDeliveryInfo(OutputDelivery item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				_conceptos = (item.Conceptos != null) ? OutputDeliveryLineList.GetChildList(item.Conceptos) : null;
			}
		}
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        /// <remarks>
		/// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool childs)
		/// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
		/// <remarks/>
		public static OutputDeliveryInfo GetChild(int sessionCode,IDataReader reader) { return GetChild(sessionCode, reader, true); }
		public static OutputDeliveryInfo GetChild(int sessionCode,IDataReader reader, bool childs) { return new OutputDeliveryInfo(sessionCode, reader, childs); }

		public virtual void LoadChilds(Type type, bool childs)
		{
			if (type.Equals(typeof(OutputDeliveryLines)))
			{
				_conceptos = OutputDeliveryLineList.GetChildList(this, childs);
			}
		}

		public static OutputDeliveryInfo New(long oid = 0) { return new OutputDeliveryInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods

		public static OutputDeliveryInfo Get(long oid, ETipoEntidad holderType = ETipoEntidad.Cliente, bool childs = false)
		{
			if (!OutputDelivery.CanGetObject()) throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			QueryConditions conditions = new QueryConditions
			{
				OutputDelivery = OutputDeliveryInfo.New(oid),
				TipoEntidad = holderType
			};
			return Get(OutputDelivery.SELECT(conditions, false), childs); 
		}

        public OutputDeliveryPrint GetPrintObject()
        {
            return OutputDeliveryPrint.New(this, ClienteInfo.Get(OidHolder, false),  TransporterInfo.Get(OidTransportista, ETipoAcreedor.TransportistaDestino, false));
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
						
						query = OutputDeliveryLineList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _conceptos = OutputDeliveryLineList.GetChildList(SessionCode, reader, Childs);
                    }
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
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
			string query = string.Empty;

			try
			{
				_base.CopyValues(source);
				
				if (Childs)
				{				
					IDataReader reader;

					query = OutputDeliveryLineList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _conceptos = OutputDeliveryLineList.GetChildList(SessionCode, reader, Childs);					
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex, new object[] { query });
			}
		}
		
		#endregion

        #region SQL

        public static string SELECT(long oid) { return OutputDelivery.SELECT(oid, false); }

        #endregion
    }
    
    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class OutputDeliverySerialInfo : SerialInfo
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
        protected OutputDeliverySerialInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Obtiene el último serial de la entidad desde la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
		public static OutputDeliverySerialInfo Get(ETipoEntidad holderType, ETipoAlbaranes deliveryType, long oidSerie, int year, bool rectificativo)
        {
            CriteriaEx criteria = OutputDelivery.GetCriteria(OutputDelivery.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(holderType, deliveryType, oidSerie, year, rectificativo);

            OutputDeliverySerialInfo obj = DataPortal.Fetch<OutputDeliverySerialInfo>(criteria);
            OutputInvoice.CloseSession(criteria.SessionCode);
            return obj;
        }

        /// <summary>
        /// Obtiene el siguiente serial para una entidad desde la base de datos
        /// </summary>
        /// <param name="entity">Tipo de Entidad</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
		public static long GetNext(ETipoEntidad holderType, ETipoAlbaranes deliveryType, long oidSerie, int year, bool rectificativo)
        {
			return Get(holderType, deliveryType, oidSerie, year, rectificativo).Value + 1;
        }

        #endregion

        #region Root Data Access

        #endregion

        #region SQL

        public static string SELECT(ETipoEntidad holderType, ETipoAlbaranes deliveryType, long oidSerie, int year, bool rectificativo)
        {
            string a = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryRecord));
            string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
            string query;

            query = @"
				SELECT 0 AS ""OID"", MAX(""SERIAL"") AS ""SERIAL""
				FROM " + a + @" AS A
				INNER JOIN " + s + @" AS S ON A.""OID_SERIE"" = S.""OID""
				WHERE A.""OID_SERIE"" = " + oidSerie + @"
				AND A.""ANO"" = " + year + @"
				AND A.""RECTIFICATIVO"" = " + rectificativo.ToString().ToUpper();

			switch (holderType)
			{
				case ETipoEntidad.WorkReport:
					{
						query += @"
							AND A.""HOLDER_TYPE"" = " + (long)holderType + @"
							AND S.""TIPO"" = " + (long)ETipoSerie.Work;
					}
					break;

				case ETipoEntidad.Cliente:
					{
						query += @"
							AND A.""HOLDER_TYPE"" = " + (long)holderType;

						switch (deliveryType)
						{
							case ETipoAlbaranes.Agrupados:
								query += @"
								AND A.""CONTADO"" = TRUE";
								break;

							case ETipoAlbaranes.Facturados:
								query += @"
								AND A.""CONTADO"" = FALSE";
								break;
						}
					}
					break;
			}

            return query;
        }

        #endregion
    }

}
