using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class ChargeOperationRecord : RecordBase
	{
		#region Attributes

		private long _oid_cobro;
		private long _oid_factura;
		private Decimal _cantidad;
		private DateTime _fecha_asignacion;

		#endregion

		#region Properties

		public virtual long OidCobro { get { return _oid_cobro; } set { _oid_cobro = value; } }
		public virtual long OidFactura { get { return _oid_factura; } set { _oid_factura = value; } }
		public virtual Decimal Cantidad { get { return _cantidad; } set { _cantidad = value; } }
		public virtual DateTime FechaAsignacion { get { return _fecha_asignacion; } set { _fecha_asignacion = value; } }

		#endregion

		#region Business Methods

		public ChargeOperationRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_cobro = Format.DataReader.GetInt64(source, "OID_COBRO");
			_oid_factura = Format.DataReader.GetInt64(source, "OID_FACTURA");
			_cantidad = Format.DataReader.GetDecimal(source, "CANTIDAD");
			_fecha_asignacion = Format.DataReader.GetDateTime(source, "FECHA_ASIGNACION");

		}
		public virtual void CopyValues(ChargeOperationRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_cobro = source.OidCobro;
			_oid_factura = source.OidFactura;
			_cantidad = source.Cantidad;
			_fecha_asignacion = source.FechaAsignacion;
		}

		#endregion
	}

	[Serializable()]
	public class ChargeOperationBase
	{
		#region Attributes

		private ChargeOperationRecord _record = new ChargeOperationRecord();

		protected DateTime _fecha_cobro;
		protected Decimal _importe;
		protected Decimal _tipo_interes;
		protected Decimal _gastos_cobro;
		protected long _medio_pago;
		protected DateTime _vencimiento;
		protected bool _cobrado = false;
		protected string _observaciones = string.Empty;
		protected long _oid_cliente;
		protected string _cliente = string.Empty;
		protected long _id_cobro;
		protected string _codigo_cobro = string.Empty;
		protected Decimal _importe_factura;
		protected DateTime _fecha_prevision;
		protected string _codigo_factura;
		protected DateTime _fecha_factura;
		protected long _dias_cobro;
		private string _cuenta_bancaria = string.Empty;

		#endregion

		#region Properties

		public ChargeOperationRecord Record { get { return _record; } }

		public string CodigoFactura { get { return _codigo_factura; } set { _codigo_factura = value; } }
		public virtual DateTime FechaFactura { get { return _fecha_factura; } set { _fecha_factura = value; } }
		public virtual string CodigoCobro { get { return _codigo_cobro; } }
		public long IdCobro { get { return _id_cobro; } set { _id_cobro = value; } }
		public DateTime Fecha { get { return _fecha_cobro; } set { _fecha_cobro = value; } }
		public Decimal Importe { get { return _importe; } set { _importe = value; } }
		public Decimal TipoInteres { get { return _tipo_interes; } }
		public Decimal GastosCobro { get { return _gastos_cobro; } }
		public long MedioPago { get { return _medio_pago; } set { _medio_pago = value; } }
		public string EMedioPagoLabel { get { return Common.EnumText<EMedioPago>.GetLabel((EMedioPago)_medio_pago); } }
		public string Tipo { get { return Common.EnumText<EMedioPago>.GetLabel((EMedioPago)_medio_pago); } }
		public DateTime Vencimiento { get { return _vencimiento; } set { _vencimiento = value; } }
		public bool Cobrado { get { return _cobrado; } set { _cobrado = value; } }
		public string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public long OidCliente { get { return _oid_cliente; } set { _oid_cliente = value; } }
		public string Cliente { get { return _cliente; } set { _cliente = value; } }
		public Decimal ImporteFactura { get { return _importe_factura; } set { _importe_factura = value; } }
		public DateTime FechaPrevision { get { return _fecha_prevision; } set { _fecha_prevision = value; } }
		public virtual long DiasCobro { get { return _dias_cobro; } }
		public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_codigo_cobro = Format.DataReader.GetString(source, "CODIGO_COBRO");
			_id_cobro = Format.DataReader.GetInt64(source, "ID_COBRO");
			_fecha_cobro = Format.DataReader.GetDateTime(source, "FECHA_COBRO");
			_importe = Format.DataReader.GetDecimal(source, "IMPORTE_COBRO");
			_tipo_interes = Format.DataReader.GetDecimal(source, "TIPO_INTERES");
			_medio_pago = Format.DataReader.GetInt64(source, "MEDIO_PAGO");
			_vencimiento = Format.DataReader.GetDateTime(source, "VENCIMIENTO");
			_codigo_factura = Format.DataReader.GetString(source, "CODIGO_FACTURA");
			_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
			_cliente = Format.DataReader.GetString(source, "CLIENTE");
			_fecha_factura = Format.DataReader.GetDateTime(source, "FECHA_FACTURA");
			_fecha_prevision = Format.DataReader.GetDateTime(source, "PREVISION_FACTURA");
			_importe_factura = Format.DataReader.GetDecimal(source, "IMPORTE_FACTURA");
			_dias_cobro = _fecha_cobro.Subtract(_fecha_factura).Days;
			_cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");

			long dias_exposicion = (_fecha_cobro != DateTime.MinValue) ? _fecha_cobro.Subtract(_fecha_prevision).Days
															: DateTime.Today.Subtract(_fecha_prevision).Days;

			if (_tipo_interes == 0) _tipo_interes = moleQule.Library.Invoice.ModulePrincipal.GetTipoInteresPorGastosDemoraSetting();
			_gastos_cobro = _importe_factura * _tipo_interes * ((dias_exposicion > 0) ? dias_exposicion : 0) / 36000;
		}
		internal void CopyValues(CobroFactura source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_fecha_cobro = source.Fecha;
			_importe = source.Importe;
			_medio_pago = source.MedioPago;
			_vencimiento = source.Vencimiento;
			_cobrado = source.Cobrado;
			_observaciones = source.Observaciones;
			_oid_cliente = source.OidCliente;
			_id_cobro = source.IdCobro;
			_fecha_prevision = source.FechaPrevision;
			_tipo_interes = source.TipoInteres;
			_gastos_cobro = source.GastosCobro;
			_importe_factura = source.ImporteFactura;
			_codigo_factura = source.CodigoFactura;
            _dias_cobro = source.DiasCobro;
			_cuenta_bancaria = source.CuentaBancaria;
		}
		internal void CopyValues(CobroFacturaInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_fecha_cobro = source.Fecha;
			_importe = source.Importe;
			_medio_pago = source.MedioPago;
			_vencimiento = source.Vencimiento;
			_cobrado = source.Cobrado;
			_observaciones = source.Observaciones;
			_oid_cliente = source.OidCliente;
			_id_cobro = source.IdCobro;
			_fecha_prevision = source.FechaPrevision;
			_tipo_interes = source.TipoInteres;
			_gastos_cobro = source.GastosCobro;
			_importe_factura = source.ImporteFactura;
			_codigo_factura = source.CodigoFactura;
            _dias_cobro = source.DiasCobro;
			_cuenta_bancaria = source.CuentaBancaria;
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class CobroFactura : BusinessBaseEx<CobroFactura>
	{	
	    #region Attributes

		protected ChargeOperationBase _base = new ChargeOperationBase();

        #endregion

        #region Properties

		public ChargeOperationBase Base { get { return _base; } }

		public override long Oid
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Oid;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Oid.Equals(value))
				{
					_base.Record.Oid = value;
					//PropertyHasChanged();
				}
			}
		}
		public virtual long OidCobro
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCobro;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCobro.Equals(value))
				{
					_base.Record.OidCobro = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidFactura
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidFactura;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidFactura.Equals(value))
				{
					_base.Record.OidFactura = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Cantidad
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Cantidad;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Cantidad.Equals(value))
				{
					_base.Record.Cantidad = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FechaAsignacion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaAsignacion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaAsignacion.Equals(value))
				{
					_base.Record.FechaAsignacion = value;
					PropertyHasChanged();
				}
			}
		}

        //NO ENLAZADAS
		public virtual string CodigoFactura { get { return _base.CodigoFactura; } set { _base.CodigoFactura = value; } }
		public virtual Decimal ImporteFactura { get { return _base.ImporteFactura; } set { _base.ImporteFactura = value; } }
		public virtual DateTime FechaFactura { get { return _base.FechaFactura; } }
		public virtual long IdCobro { get { return _base.IdCobro; } set { _base.IdCobro = value; } }
        public virtual DateTime Fecha { get { return _base.Fecha; } set { _base.Fecha = value; } }
        public virtual Decimal Importe { get { return _base.Importe; } set { _base.Importe = value; } }
		public virtual Decimal TipoInteres { get { return _base.TipoInteres; } }
		public virtual Decimal GastosCobro { get { return _base.GastosCobro; } }
        public virtual long MedioPago { get { return _base.MedioPago; } set { _base.MedioPago = value; } }
		public virtual string Tipo { get { return _base.Tipo; } }
        public virtual DateTime Vencimiento { get { return _base.Vencimiento; } set { _base.Vencimiento = value; } }
        public virtual bool Cobrado { get { return _base.Cobrado; } set { _base.Cobrado = value; } }
        public virtual string Observaciones { get { return _base.Observaciones; } set { _base.Observaciones = value; } }
        public virtual long OidCliente { get { return _base.OidCliente; } set { _base.OidCliente = value; } }
		public virtual string Cliente { get { return _base.Cliente; } }
		public virtual DateTime FechaPrevision { get { return _base.FechaPrevision; } set { _base.FechaPrevision = value; } }
		public virtual long DiasCobro { get { return _base.DiasCobro; } }
		public string EMedioPagoLabel { get { return _base.EMedioPagoLabel; } }
		public string CuentaBancaria { get { return _base.CuentaBancaria; } set { _base.CuentaBancaria = value; } }

		#endregion

		#region Business Methods

		public virtual void CopyFrom(Charge cobro, OutputInvoiceInfo factura)
		{
			OidCobro = cobro.Oid;
			OidFactura = factura.Oid;
			Cantidad = factura.Asignado;
			FechaAsignacion = DateTime.Now;

			FechaPrevision = factura.Prevision;
			ImporteFactura = factura.Total;
		}

		#endregion
		 
	    #region Validation Rules
		 
		//región a rellenar si hay campos requeridos o claves ajenas
		
		//Descomentar en caso de existir reglas de validación
		/*protected override void AddBusinessRules()
        {	
			//Agregar reglas de validación
        }*/
		
		#endregion
		 
		#region Authorization Rules
		 
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.FACTURA_EMITIDA);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.FACTURA_EMITIDA);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.FACTURA_EMITIDA);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.FACTURA_EMITIDA);
		}
		 
		#endregion

		#region Common Factory Methods

		private CobroFactura(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			MarkAsChild();
			Fetch(reader);
		}

		internal static CobroFactura GetChild(int sessionCode, IDataReader reader) { return new CobroFactura(sessionCode, reader); }

		public virtual CobroFacturaInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return new CobroFacturaInfo(this, false);
		}

		#endregion

		#region Child Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public CobroFactura() 
		{ 
			MarkAsChild();
			Random r = new Random();
			Oid = (long)r.Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}	
		
		private CobroFactura(CobroFactura source)
		{
			MarkAsChild();
			Fetch(source);
		}
		
		public static CobroFactura NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
                    Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new CobroFactura();
		}		
		public static CobroFactura NewChild(Charge parent, OutputInvoiceInfo factura)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CobroFactura obj = new CobroFactura();
			obj.CopyFrom(parent, factura);
			
			return obj;
		}		
		public static CobroFactura NewChild(OutputInvoice parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CobroFactura obj = new CobroFactura();
			obj.OidFactura = parent.Oid;
			
			return obj;
		}
        public static CobroFactura NewChild(OutputInvoiceInfo parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CobroFactura obj = new CobroFactura();
            obj.OidFactura = parent.Oid;

            return obj;
        }
		
		internal static CobroFactura GetChild(CobroFactura source)
		{
			return new CobroFactura(source);
		}		
			
		/// <summary>
		/// Borrado aplazado, es posible el undo 
		/// (La función debe ser "no estática")
		/// </summary>
		public override void Delete()
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);			
				
			MarkDeleted();
		}
		
		/// <summary>
		/// No se debe utilizar esta función para guardar. Hace falta el padre.
		/// Utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override CobroFactura Save()
		{
            throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(CobroFactura source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		internal void Insert(Charge parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			SessionCode = parent.SessionCode;
            OidCobro = parent.Oid;

			try
			{	
				Oid = (long)parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			
			MarkOld();
		}

		internal void Update(Charge parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            this.OidCobro = parent.Oid;
			
			try
			{
				SessionCode = parent.SessionCode;
				ChargeOperationRecord obj = Session().Get<ChargeOperationRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Charge parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				ChargeOperationRecord obj = Session().Get<ChargeOperationRecord>(Oid); 
				if (obj != null) Session().Delete(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		
			MarkNew(); 
		}
		
		internal void Insert(OutputInvoice parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{	
				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			
			MarkOld();
		}

		internal void Update(OutputInvoice parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			
			try
			{
				SessionCode = parent.SessionCode;
				ChargeOperationRecord obj = Session().Get<ChargeOperationRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			
			MarkOld();
		}

		internal void DeleteSelf(OutputInvoice parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<ChargeOperationRecord>(Oid));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		
			MarkNew(); 
		}		
		
		#endregion

        #region SQL

        public static string SELECT_FIELDS()
        {
            string query;

            query = @"
				SELECT CF.*
					,C.""CODIGO"" AS ""CODIGO_COBRO""
					,C.""ID_COBRO"" AS ""ID_COBRO""
					,C.""FECHA"" AS ""FECHA_COBRO""
					,C.""IMPORTE"" AS ""IMPORTE_COBRO""
					,C.""MEDIO_PAGO""
					,C.""VENCIMIENTO"" AS ""VENCIMIENTO""
					,F.""OID_CLIENTE"" AS ""OID_CLIENTE""
					,F.""CLIENTE"" AS ""CLIENTE""
					,F.""CODIGO"" AS ""CODIGO_FACTURA""
					,F.""FECHA"" AS ""FECHA_FACTURA""
					,F.""TOTAL"" AS ""IMPORTE_FACTURA""
					,F.""PREVISION_PAGO"" AS ""PREVISION_FACTURA""
					,CL.""TIPO_INTERES"" AS ""TIPO_INTERES""
					,COALESCE(CB.""VALOR"", '') AS ""CUENTA_BANCARIA""";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE (F.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			switch (conditions.Estado)
			{
				case EEstado.Todos:
					break;

				case EEstado.NoAnulado:
					query += " AND C.\"ESTADO\" != " + (long)EEstado.Anulado;
					break;

				default:
					query += " AND C.\"ESTADO\" = " + (long)conditions.Estado;
					break;
			}

			if (conditions.Cliente != null)
				query += " AND F.\"OID_CLIENTE\" = " + conditions.Cliente.Oid;

			if (conditions.Cobro != null)
				query += " AND CF.\"OID_COBRO\" = " + conditions.Cobro.Oid;

			if (conditions.Serie != null)
				query += " AND F.\"OID_SERIE\" = " + conditions.Serie.Oid;

			if (conditions.Factura != null)
				query += " AND CF.\"OID_FACTURA\" = " + conditions.Factura.Oid;

			switch (conditions.MedioPago)
			{
				case EMedioPago.NoEfectivo:
					query += " AND C.\"MEDIO_PAGO\" != " + (long)EMedioPago.Efectivo;
					break;

				default:
					if (conditions.MedioPago != EMedioPago.Todos)
						query += " AND C.\"MEDIO_PAGO\" = " + (long)conditions.MedioPago;
					break;
            }

            if (conditions.MedioPagoList != null && conditions.MedioPagoList.Count > 0)
                query += EntityBase.GET_IN_LIST_CONDITION(conditions.MedioPagoList, "C", "MEDIO_PAGO");

			return query;
		}

		public static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string cf = nHManager.Instance.GetSQLTable(typeof(ChargeOperationRecord));
			string cr = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
			string fe = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string se = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
			
			string query;

			query = 
				SELECT_FIELDS() + @"
					FROM " + cf + @" AS CF
					INNER JOIN " + cr + @" AS C ON CF.""OID_COBRO"" = C.""OID""
					LEFT JOIN " + cb + @" AS CB ON CB.""OID""  = C.""OID_CUENTA_BANCARIA""
					INNER JOIN " + fe + @" AS F ON F.""OID"" = CF.""OID_FACTURA""
					LEFT JOIN " + se + @" AS S ON S.""OID"" = F.""OID_SERIE""
					INNER JOIN " + cl + @" AS CL ON CL.""OID"" = F.""OID_CLIENTE""";

			query += WHERE(conditions);

			query += @"
				ORDER BY S.""IDENTIFICADOR"", F.""CODIGO"", C.""CODIGO""";

			//if (lock_table) query += "FOR UPDATE OF CF NOWAIT";

			return query;
		}

		internal static string SELECT_BY_FACTURA(long oid, bool lockTable)
		{
			string cf = nHManager.Instance.GetSQLTable(typeof(ChargeOperationRecord));
			string cr = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
			string fe = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
			
			string query =
				SELECT_FIELDS() + @"
				FROM " + cf + @" AS CF
				INNER JOIN " + cr + @" AS C ON CF.""OID_COBRO"" = C.""OID"" AND C.""ESTADO"" != " + (long)EEstado.Anulado + @"
				LEFT JOIN " + cb + @" AS CB ON CB.""OID""  = C.""OID_CUENTA_BANCARIA""
				INNER JOIN " + fe + @" AS F ON F.""OID"" = CF.""OID_FACTURA""
				INNER JOIN " + cl + @" AS CL ON CL.""OID"" = F.""OID_CLIENTE""
				WHERE CF.""OID_FACTURA"" = " + oid;

			//if (lock_table) query += "FOR UPDATE OF CF NOWAIT";

			return query;
		}

		internal static string SELECT_BY_COBRO(long oid, bool lockTable)
		{
			string cf = nHManager.Instance.GetSQLTable(typeof(ChargeOperationRecord));
			string cr = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
			string fe = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));

			string query = 
				SELECT_FIELDS() + @"
				FROM " + cf + @" AS CF
				INNER JOIN " + cr + @" AS C ON CF.""OID_COBRO"" = C.""OID""
				LEFT JOIN " + cb + @" AS CB ON CB.""OID""  = C.""OID_CUENTA_BANCARIA""
				INNER JOIN " + fe + @" AS F ON F.""OID"" = CF.""OID_FACTURA""
				INNER JOIN " + cl + @" AS CL ON CL.""OID"" = F.""OID_CLIENTE""
				WHERE CF.""OID_COBRO"" = " + oid;

			//if (lock_table) query += "FOR UPDATE OF CF NOWAIT";

			return query;
		}

        #endregion
    }
}

