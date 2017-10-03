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
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class CobroREARecord : RecordBase
	{
		#region Attributes

		private long _oid_cobro;
		private long _oid_expediente;
		private Decimal _cantidad;
		private long _oid_expediente_rea;
		private DateTime _fecha_asignacion;

		#endregion

		#region Properties

		public virtual long OidCobro { get { return _oid_cobro; } set { _oid_cobro = value; } }
		public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
		public virtual Decimal Cantidad { get { return _cantidad; } set { _cantidad = value; } }
		public virtual long OidExpedienteRea { get { return _oid_expediente_rea; } set { _oid_expediente_rea = value; } }
		public virtual DateTime FechaAsignacion { get { return _fecha_asignacion; } set { _fecha_asignacion = value; } }

		#endregion

		#region Business Methods

		public CobroREARecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_cobro = Format.DataReader.GetInt64(source, "OID_COBRO");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			_cantidad = Format.DataReader.GetDecimal(source, "CANTIDAD");
			_oid_expediente_rea = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE_REA");
			_fecha_asignacion = Format.DataReader.GetDateTime(source, "FECHA_ASIGNACION");

		}
		public virtual void CopyValues(CobroREARecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_cobro = source.OidCobro;
			_oid_expediente = source.OidExpediente;
			_cantidad = source.Cantidad;
			_oid_expediente_rea = source.OidExpedienteRea;
			_fecha_asignacion = source.FechaAsignacion;
		}

		#endregion
	}

	[Serializable()]
	public class CobroREABase
	{
		#region Attributes

		private CobroREARecord _record = new CobroREARecord();

		protected string _expediente;

		#endregion

		#region Properties

		public CobroREARecord Record { get { return _record; } }

		public string Expediente { get { return _expediente; } set { _expediente = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_expediente = Format.DataReader.GetString(source, "CODIGO_EXPEDIENTE");
		}
		internal void CopyValues(CobroREA source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_expediente = source.CodigoExpediente;
		}
		internal void CopyValues(CobroREAInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_expediente = source.CodigoExpediente;
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class CobroREA : BusinessBaseEx<CobroREA>
	{	
	    #region Attributes

		protected CobroREABase _base = new CobroREABase();

		#endregion

		#region Properties

		public CobroREABase Base { get { return _base; } }

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
		public virtual long OidExpediente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidExpediente;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidExpediente.Equals(value))
				{
					_base.Record.OidExpediente = value;
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
		public virtual long OidExpedienteREA
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidExpedienteRea;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidExpedienteRea.Equals(value))
				{
					_base.Record.OidExpedienteRea = value;
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
		public string CodigoExpediente { get { return _base.Expediente; } set { _base.Expediente = value; } }

        #endregion

        #region Business Methods

        public virtual void CopyFrom(Charge cobro, LineaFomentoInfo linea)
        {
            OidCobro = cobro.Oid;
            OidExpediente = linea.OidExpediente;
            OidExpedienteREA = linea.Oid;
            CodigoExpediente = linea.IDExpediente;
            Cantidad = linea.Asignado;
            FechaAsignacion = DateTime.Now;
        }

		public virtual void CopyFrom(Charge cobro, FacREAInfo factura)
		{
			OidCobro = cobro.Oid;
			OidExpediente = factura.OidExpediente;
			OidExpedienteREA = factura.OidExpedienteREA;
			CodigoExpediente = factura.ExpedienteREA;
			Cantidad = factura.Asignado;
			FechaAsignacion = DateTime.Now;
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
		 
		#region Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public CobroREA() 
		{ 
			MarkAsChild();
			Random r = new Random();
			Oid = (long)r.Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}			
		private CobroREA(CobroREA source)
		{
			MarkAsChild();
			Fetch(source);
		}		
		private CobroREA(int sessionCode, IDataReader reader)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static CobroREA NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
					Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new CobroREA();
		}

        public static CobroREA NewChild(Charge parent, LineaFomentoInfo linea)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CobroREA obj = new CobroREA();
            obj.CopyFrom(parent, linea);

            return obj;
        }		
		
		public static CobroREA NewChild(Charge parent, FacREAInfo factura)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CobroREA obj = new CobroREA();
			obj.CopyFrom(parent, factura);
			
			return obj;
		}		
		public static CobroREA NewChild(REAExpedient parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CobroREA obj = new CobroREA();
			obj.OidExpedienteREA = parent.Oid;
			obj.OidExpediente = parent.OidExpediente;
			
			return obj;
		}
        public static CobroREA NewChild(ExpedienteREAInfo parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CobroREA obj = new CobroREA();
			obj.OidExpedienteREA = parent.Oid;
			obj.OidExpediente = parent.OidExpediente;

            return obj;
        }		
		
		internal static CobroREA GetChild(CobroREA source)
		{
			return new CobroREA(source);
		}		
		internal static CobroREA GetChild(int sessionCode, IDataReader reader) { return new CobroREA(sessionCode, reader); }
		
		public virtual CobroREAInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new CobroREAInfo(this, false);
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
		public override CobroREA Save()
		{
			throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(CobroREA source)
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

            OidCobro = parent.Oid;

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

		internal void Update(Charge parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            this.OidCobro = parent.Oid;
			
			try
			{
				SessionCode = parent.SessionCode;
				CobroREARecord obj = Session().Get<CobroREARecord>(Oid);
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
				Session().Delete(Session().Get<CobroREARecord>(Oid));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		
			MarkNew(); 
		}
	
		#endregion	
	}
}

