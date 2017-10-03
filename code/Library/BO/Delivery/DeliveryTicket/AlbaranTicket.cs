using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class DeliveryTicketRecord : RecordBase
	{
		#region Attributes

		private long _oid_albaran;
		private long _oid_ticket;
		private DateTime _fecha_asignacion;
		#endregion

		#region Properties

		public virtual long OidAlbaran { get { return _oid_albaran; } set { _oid_albaran = value; } }
		public virtual long OidTicket { get { return _oid_ticket; } set { _oid_ticket = value; } }
		public virtual DateTime FechaAsignacion { get { return _fecha_asignacion; } set { _fecha_asignacion = value; } }

		#endregion

		#region Business Methods

		public DeliveryTicketRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_albaran = Format.DataReader.GetInt64(source, "OID_ALBARAN");
			_oid_ticket = Format.DataReader.GetInt64(source, "OID_TICKET");
			_fecha_asignacion = Format.DataReader.GetDateTime(source, "FECHA_ASIGNACION");

		}
		public virtual void CopyValues(DeliveryTicketRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_albaran = source.OidAlbaran;
			_oid_ticket = source.OidTicket;
			_fecha_asignacion = source.FechaAsignacion;
		}

		#endregion
	}

	[Serializable()]
	public class DeliveryTicketBase
	{
		#region Attributes

		private DeliveryTicketRecord _record = new DeliveryTicketRecord();

		protected Decimal _importe;
		protected string _codigo_ticket;
		protected string _codigo_albaran;

		#endregion

		#region Properties

		public DeliveryTicketRecord Record { get { return _record; } }

		public Decimal Importe { get { return _importe; } set { _importe = value; } }
		public string CodigoTicket { get { return _codigo_ticket; } set { _codigo_ticket = value; } }
		public string CodigoAlbaran { get { return _codigo_albaran; } set { _codigo_albaran = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_importe = Format.DataReader.GetDecimal(source, "IMPORTE");
			_codigo_ticket = Format.DataReader.GetString(source, "CODIGO_TICKET");
			_codigo_albaran = Format.DataReader.GetString(source, "CODIGO_ALBARAN");
		}
		internal void CopyValues(AlbaranTicket source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_importe = source.Importe;
			_codigo_ticket = source.CodigoTicket;
			_codigo_albaran = source.CodigoAlbaran;
		}
		internal void CopyValues(AlbaranTicketInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_importe = source.Importe;
			_codigo_ticket = source.CodigoTicket;
			_codigo_albaran = source.CodigoAlbaran;
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class AlbaranTicket : BusinessBaseEx<AlbaranTicket>
	{	
	    #region Attributes

		protected DeliveryTicketBase _base = new DeliveryTicketBase();

		#endregion

		#region Properties

		public DeliveryTicketBase Base { get { return _base; } }

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
		public virtual long OidAlbaran
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAlbaran;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidAlbaran.Equals(value))
				{
					_base.Record.OidAlbaran = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidTicket
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidTicket;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidTicket.Equals(value))
				{
					_base.Record.OidTicket = value;
					PropertyHasChanged();
				}
			}
		}
		/*public virtual DateTime FechaAsignacion
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
		}*/
		public virtual DateTime FechaAsignacion { get { return _base.Record.FechaAsignacion; } set { _base.Record.FechaAsignacion = value; } }

        // CAMPOS NO ENLAZADOS
		public virtual Decimal Importe { get { return _base.Importe; } set { _base.Importe = value; } }
		public virtual string CodigoTicket { get { return _base.CodigoTicket; } set { _base.CodigoTicket = value; } }
		public virtual string CodigoAlbaran { get { return _base.CodigoAlbaran; } set { _base.CodigoAlbaran = value; } }

        #endregion

        #region Business Methods
			
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

		#region Root Factory Methods

		public static void DeleteFromList(List<AlbaranTicketInfo> list)
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = AlbaranTicket.OpenSession();
			ISession sess = AlbaranTicket.Session(sessCode);
			ITransaction trans = AlbaranTicket.BeginTransaction(sessCode);

			string oids = "0";

			try
			{
				foreach (AlbaranTicketInfo item in list)
					oids += "," + item.Oid.ToString();

                sess.Delete("from DeliveryTicketRecord ab where ab.Oid in (" + oids + ")");

				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				AlbaranTicket.CloseSession(sessCode);
			}
		}

		public static void DeleteFromList(List<OutputDeliveryInfo> list)
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = AlbaranTicket.OpenSession();
			ISession sess = AlbaranTicket.Session(sessCode);
			ITransaction trans = AlbaranTicket.BeginTransaction(sessCode);

			string oids = "0";

			try
			{
				foreach (OutputDeliveryInfo item in list)
					oids += "," + item.Oid.ToString();

                sess.Delete("from DeliveryTicketRecord ab where ab.OidAlbaran in (" + oids + ")");

				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				AlbaranTicket.CloseSession(sessCode);
			}
		}

		public static void UpdateFromList(List<OutputDeliveryInfo> list, long oid_albaran)
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = AlbaranTicket.OpenSession();
			ITransaction trans = AlbaranTicket.BeginTransaction(AlbaranTicket.Session(sessCode));

			try
			{
				List<long> oids = new List<long>();

				oids.Add(0);

				foreach (OutputDeliveryInfo item in list)
					oids.Add(item.Oid);

				nHManager.Instance.SQLNativeExecute(UPDATE_ALBARAN(oids, oid_albaran));

				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				AlbaranTicket.CloseSession(sessCode);
			}
		}

		#endregion

		#region Child Factory Methods

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public AlbaranTicket() 
		{ 
			MarkAsChild();
			_base.Record.Oid = (long)(new Random()).Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}			
		private AlbaranTicket(AlbaranTicket source)
		{
			MarkAsChild();
			Fetch(source);
		}		
		private AlbaranTicket(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static AlbaranTicket NewChild()
		{
			if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new AlbaranTicket();
		}		
		public static AlbaranTicket NewChild(Ticket ticket, OutputDeliveryInfo albaran)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			AlbaranTicket obj = new AlbaranTicket();
			obj.OidTicket = ticket.Oid;
            obj.OidAlbaran = albaran.Oid;
            obj.CodigoTicket = ticket.Codigo;
            obj.CodigoAlbaran = albaran.Codigo;
            obj.FechaAsignacion = DateTime.Today;
			
			return obj;
		}

		internal static AlbaranTicket GetChild(AlbaranTicket source)
		{
			return new AlbaranTicket(source);
		}
		internal static AlbaranTicket GetChild(int sessionCode, IDataReader reader)
		{
			return new AlbaranTicket(sessionCode, reader);
		}
		
		public virtual AlbaranTicketInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new AlbaranTicketInfo(this, false);
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
		public override AlbaranTicket Save()
		{
            throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(AlbaranTicket source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		internal void Insert(OutputDelivery parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidAlbaran = parent.Oid;

			try
			{	
				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void Update(OutputDelivery parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidAlbaran = parent.Oid;
			
			try
			{
				SessionCode = parent.SessionCode;
				DeliveryTicketRecord obj = Session().Get<DeliveryTicketRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(OutputDelivery parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<DeliveryTicketRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}
		
		internal void Insert(Ticket parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidTicket = parent.Oid;

			try
			{	
				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void Update(Ticket parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;			
			
			try
			{
				SessionCode = parent.SessionCode;
				DeliveryTicketRecord obj = Session().Get<DeliveryTicketRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Ticket parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<DeliveryTicketRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}		
		
		#endregion

		#region SQL

		internal static string SELECT_FIELDS()
		{
			string query;

			query = "SELECT AT.*," +
					"       AE.\"CODIGO\" AS \"CODIGO_ALBARAN\"," +
					"       TK.\"CODIGO\" AS \"CODIGO_TICKET\"," +
					"       TK.\"TOTAL\" AS \"IMPORTE\"";

			return query;
		}

		internal static string SELECT_BASE(QueryConditions conditions)
		{
			string at = nHManager.Instance.GetSQLTable(typeof(DeliveryTicketRecord));
			string ae = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryRecord));
			string tk = nHManager.Instance.GetSQLTable(typeof(TicketRecord));
			string query;

			query = SELECT_FIELDS() +
					" FROM " + at + " AS AT" +
					" INNER JOIN " + ae + " AS AE ON AE.\"OID\" = AT.\"OID_ALBARAN\"" +
					" INNER JOIN " + tk + " AS TK ON TK.\"OID\" = AT.\"OID_TICKET\"";

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE TRUE";

			if (conditions.OutputDelivery != null) query += " AND AT.\"OID_ALBARAN\" = " + conditions.OutputDelivery.Oid;			
			if (conditions.Ticket != null) query += " AND AT.\"OID_TICKET\" = " + conditions.Ticket.Oid;

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lock_table)
		{
			string query = string.Empty;

			query = SELECT_BASE(conditions) +
					WHERE(conditions);

			//if (lock_table) query += " FOR UPDATE OF AT NOWAIT";

			return query;
		}

		public static string SELECT_BY_LIST(List<long> oid_list, bool lock_table)
		{
			string query = SELECT_BASE(new QueryConditions());

			query += " WHERE AT.\"OID\" IN " + EntityBase.GET_IN_STRING(oid_list);

			if (lock_table) query += " FOR UPDATE OF AT NOWAIT";

			return query;
		}

		internal static string UPDATE_ALBARAN(List<long> oid_list, long oid_albaran)
		{
			string tk = nHManager.Instance.GetSQLTable(typeof(TicketRecord));
			string ab = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryRecord));
			string at = nHManager.Instance.GetSQLTable(typeof(DeliveryTicketRecord));

			string query = string.Empty;

			query = " UPDATE " + at + " SET \"OID_ALBARAN\" = " + oid_albaran +
					" WHERE \"OID_ALBARAN\" IN " + EntityBase.GET_IN_STRING(oid_list) + ";" ;

			query = " UPDATE " + tk + " SET \"ALBARANES\" = AE.\"CODIGO\"" +
					" FROM (SELECT AT.\"OID_TICKET\", AB.\"CODIGO\"" + 
					"		FROM " + at + " AS AT" +
					"		INNER JOIN " + ab + " AS AB ON AB.\"OID\" = AT.\"OID_ALBARAN\"" +
					"		WHERE AB.\"OID\" = " + oid_albaran + ") AS AE" +
					" WHERE " + tk + ".\"OID\" = AE.\"OID_TICKET\"";

			return query;
		}

		#endregion
	}
}

