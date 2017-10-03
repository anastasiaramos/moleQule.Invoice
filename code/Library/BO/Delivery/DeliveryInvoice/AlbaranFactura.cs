using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class OutputDeliveryInvoiceRecord : RecordBase
	{
		#region Attributes

		private long _oid_albaran;
		private long _oid_factura;
		private DateTime _fecha_asignacion;
		#endregion

		#region Properties

		public virtual long OidAlbaran { get { return _oid_albaran; } set { _oid_albaran = value; } }
		public virtual long OidFactura { get { return _oid_factura; } set { _oid_factura = value; } }
		public virtual DateTime FechaAsignacion { get { return _fecha_asignacion; } set { _fecha_asignacion = value; } }

		#endregion

		#region Business Methods

		public OutputDeliveryInvoiceRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_albaran = Format.DataReader.GetInt64(source, "OID_ALBARAN");
			_oid_factura = Format.DataReader.GetInt64(source, "OID_FACTURA");
			_fecha_asignacion = Format.DataReader.GetDateTime(source, "FECHA_ASIGNACION");

		}
		public virtual void CopyValues(OutputDeliveryInvoiceRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_albaran = source.OidAlbaran;
			_oid_factura = source.OidFactura;
			_fecha_asignacion = source.FechaAsignacion;
		}

		#endregion
	}

	[Serializable()]
	public class OutputDeliveryInvoiceBase
	{
		#region Attributes

		private OutputDeliveryInvoiceRecord _record = new OutputDeliveryInvoiceRecord();

		protected Decimal _importe;
		protected string _codigo_factura;
		protected string _codigo_albaran;

		#endregion

		#region Properties

		public OutputDeliveryInvoiceRecord Record { get { return _record; } }

		public Decimal Importe { get { return _importe; } set { _importe = value; } }
		public string CodigoFactura { get { return _codigo_factura; } set { _codigo_factura = value; } }
		public string CodigoAlbaran { get { return _codigo_albaran; } set { _codigo_albaran = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_importe = Format.DataReader.GetDecimal(source, "IMPORTE");
			_codigo_factura = Format.DataReader.GetString(source, "CODIGO_FACTURA");
			_codigo_albaran = Format.DataReader.GetString(source, "CODIGO_ALBARAN");
		}
		internal void CopyValues(AlbaranFactura source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_importe = source.Importe;
			_codigo_factura = source.CodigoFactura;
			_codigo_albaran = source.CodigoAlbaran;
		}
		internal void CopyValues(AlbaranFacturaInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_importe = source.Importe;
			_codigo_factura = source.CodigoFactura;
			_codigo_albaran = source.CodigoAlbaran;
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class AlbaranFactura : BusinessBaseEx<AlbaranFactura>
	{	
	    #region Attributes

		protected OutputDeliveryInvoiceBase _base = new OutputDeliveryInvoiceBase();

        #endregion

        #region Properties

		public OutputDeliveryInvoiceBase Base { get { return _base; } }

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
					//PropertyHasChanged();
				}
			}
		}*/
		public virtual DateTime FechaAsignacion { get { return _base.Record.FechaAsignacion; } set { _base.Record.FechaAsignacion = value; } }

        // CAMPOS NO ENLAZADOS
        public virtual Decimal Importe { get { return _base.Importe; } set { _base.Importe = value; } }
		public virtual string CodigoFactura { get { return _base.CodigoFactura; } set { _base.CodigoFactura = value; } }
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
            return OutputInvoice.CanAddObject();
		}
		
		public static bool CanGetObject()
		{
			return OutputInvoice.CanGetObject();
		}
		
		public static bool CanDeleteObject()
		{
			return OutputInvoice.CanGetObject();
		}
		
		public static bool CanEditObject()
		{
			return OutputInvoice.CanEditObject();
		}
		 
		#endregion
		 
		#region Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public AlbaranFactura() 
		{ 
			MarkAsChild();
			Random r = new Random();
			Oid = (long)r.Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}			
		private AlbaranFactura(AlbaranFactura source)
		{
			MarkAsChild();
			Fetch(source);
		}		
		private AlbaranFactura(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static AlbaranFactura NewChild()
		{
			if (!CanAddObject())
                throw new System.Security.SecurityException(
                    Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new AlbaranFactura();
		}
		
		public static AlbaranFactura NewChild(OutputInvoice factura, OutputDeliveryInfo albaran)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
                    Library.Resources.Messages.USER_NOT_ALLOWED);
			
			AlbaranFactura obj = new AlbaranFactura();
			obj.OidFactura = factura.Oid;
            obj.OidAlbaran = albaran.Oid;
            obj.CodigoFactura = factura.Codigo;
            obj.CodigoAlbaran = albaran.Codigo;
            obj.FechaAsignacion = DateTime.Today;
			
			return obj;
		}

		internal static AlbaranFactura GetChild(AlbaranFactura source)
		{
			return new AlbaranFactura(source);
		}

		internal static AlbaranFactura GetChild(int sessionCode, IDataReader reader)
		{
			return new AlbaranFactura(sessionCode, reader);
		}
		
		public virtual AlbaranFacturaInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new AlbaranFacturaInfo(this, false);
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
		public override AlbaranFactura Save()
		{
            throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(AlbaranFactura source)
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
				OutputDeliveryInvoiceRecord obj = Session().Get<OutputDeliveryInvoiceRecord>(Oid);
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
				Session().Delete(Session().Get<OutputDeliveryInvoiceRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}
		
		internal void Insert(OutputInvoice parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidFactura = parent.Oid;

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

		internal void Update(OutputInvoice parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;			
			
			try
			{
				SessionCode = parent.SessionCode;
				OutputDeliveryInvoiceRecord obj = Session().Get<OutputDeliveryInvoiceRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
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
				Session().Delete(Session().Get<OutputDeliveryInvoiceRecord>(Oid));
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

			query = "SELECT AF.*," +
					"       AE.\"CODIGO\" AS \"CODIGO_ALBARAN\"," +
					"       FE.\"CODIGO\" AS \"CODIGO_FACTURA\"," +
					"       FE.\"TOTAL\" AS \"IMPORTE\"";
	
			return query;
		}

		internal static string SELECT_BASE(QueryConditions conditions)
		{
			string af = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryInvoiceRecord));
			string ae = nHManager.Instance.GetSQLTable(typeof(OutputDeliveryRecord));
			string fe = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string query;

			query = SELECT_FIELDS() +
					" FROM " + af + " AS AF" +
					" INNER JOIN " + ae + " AS AE ON AE.\"OID\" = AF.\"OID_ALBARAN\"" +
					" INNER JOIN " + fe + " AS FE ON FE.\"OID\" = AF.\"OID_FACTURA\"";

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE TRUE";

			if (conditions.OutputDelivery != null) query += " AND AF.\"OID_ALBARAN\" = " + conditions.OutputDelivery.Oid;
			if (conditions.Factura != null) query += " AND AF.\"OID_FACTURA\" = " + conditions.Factura.Oid;

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

		#endregion
	}
}

