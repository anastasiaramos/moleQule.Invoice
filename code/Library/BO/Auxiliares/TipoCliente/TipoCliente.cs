using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class ClientTypeRecord : RecordBase
	{
		#region Attributes

		private string _valor = string.Empty;
		#endregion

		#region Properties

		public virtual string Valor { get { return _valor; } set { _valor = value; } }

		#endregion

		#region Business Methods

		public ClientTypeRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_valor = Format.DataReader.GetString(source, "VALOR");

		}
		public virtual void CopyValues(ClientTypeRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_valor = source.Valor;
		}

		#endregion
	}

	[Serializable()]
	public class ClientTypeBase
	{
		#region Attributes

		private ClientTypeRecord _record = new ClientTypeRecord();

		#endregion

		#region Properties

		public ClientTypeRecord Record { get { return _record; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);
		}
		internal void CopyValues(TipoCliente source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}
		internal void CopyValues(TipoClienteInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}

		#endregion
	}

	/// <summary>
	/// </summary>	
    [Serializable()]
	public class TipoCliente : BusinessBaseEx<TipoCliente>
	{	 
		#region Attributes
		
		protected ClientTypeBase _base = new ClientTypeBase();		

		#endregion
		
		#region Properties
		
		public ClientTypeBase Base { get { return _base; } }
		
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
		public virtual string Valor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Valor;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Valor.Equals(value))
				{
					_base.Record.Valor = value;
					PropertyHasChanged();
				}
			}
		}		
		
		#endregion
		
		#region Business Methods
		
		public virtual TipoCliente CloneAsNew()
		{
			TipoCliente clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.SessionCode = TipoCliente.OpenSession();
			TipoCliente.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}

		#endregion
		 
	    #region Validation Rules

		/// <summary>
		/// Añade las reglas de validación necesarias para el objeto
		/// </summary>
		protected override void AddBusinessRules()
		{
			
			ValidationRules.AddRule(
                Csla.Validation.CommonRules.StringRequired, "Valor");
			
		}
		 
		#endregion
		 
		#region Autorization Rules
		
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Common.Resources.SecureItems.AUXILIARES);
		}		
		public static bool CanGetObject()
		{
			return AutorizationRulesControler.CanGetObject(Common.Resources.SecureItems.AUXILIARES);
		}		
		public static bool CanDeleteObject()
		{
			return AutorizationRulesControler.CanDeleteObject(Common.Resources.SecureItems.AUXILIARES);
		}		
		public static bool CanEditObject()
		{
			return AutorizationRulesControler.CanEditObject(Common.Resources.SecureItems.AUXILIARES);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		public TipoCliente()
		{
			MarkAsChild();
			_base.Record.Oid = (long)(new Random()).Next();			
			
			//Si fuera necesario hay que inicializar las listas hijos aquí		
		}		
		private TipoCliente(TipoCliente source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private TipoCliente(IDataReader source, bool childs)
        {
            MarkAsChild();	
			Childs = childs;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static TipoCliente NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<TipoCliente>(new CriteriaCs(-1));
		}		
	
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">TipoCliente con los datos para el objeto</param>
		/// <param name="childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
		internal static TipoCliente GetChild(TipoCliente source, bool childs = false)
		{
			return new TipoCliente(source, childs);
		}
        internal static TipoCliente GetChild(IDataReader source, bool childs = false)
        {
            return new TipoCliente(source, childs);
        }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// También copia los datos de los hijos del objeto.
		/// </summary>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual TipoClienteInfo GetInfo(bool childs = true) { return new TipoClienteInfo(this, childs); }

		#endregion				
		
		#region Common Data Access
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
		
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(TipoCliente source)
		{
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);				
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);                
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(TipoClientes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{	
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(TipoClientes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				ClientTypeRecord obj = Session().Get<ClientTypeRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(TipoClientes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<ClientTypeRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}

		#endregion			
						
		#region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>() { };
		}

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = @"
				SELECT CL.*";

			return query;
		}

		internal static string JOIN(QueryConditions conditions)
		{
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientTypeRecord));

			string query;

			query = @"
				FROM " + cl + @" AS CL";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "SU", ForeignFields());

			query = @" 
				AND (CL.""DATE"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "CL");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "CL");

			if (conditions.TipoCliente != null)
				query += @"
					AND C.""OID"" = " + conditions.TipoCliente.Oid;

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query;

			query =
				SELECT_FIELDS() +
				JOIN(conditions) +
				WHERE(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "CL", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += Common.EntityBase.LOCK("CL", lockTable);

			return query;
		}

		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};
			return SELECT(conditions, lockTable);
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			return SELECT(new QueryConditions { TipoCliente = TipoClienteInfo.New(oid) }, lockTable);
		}

		#endregion
	}
}

