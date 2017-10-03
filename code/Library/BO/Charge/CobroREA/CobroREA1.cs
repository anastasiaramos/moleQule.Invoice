using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class CobroREARecord : RecordBase
	{
		#region Attributes

		private long _oid_cobro;
		private long _oid_expediente;
		private Decimal _cantidad;
		private long _oid_expediente_rea;
  
		#endregion
		
		#region Properties
		
		public virtual long OidCobro { get { return _oid_cobro; } set { _oid_cobro = value; } }
public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
public virtual Decimal Cantidad { get { return _cantidad; } set { _cantidad = value; } }
public virtual long OidExpedienteRea { get { return _oid_expediente_rea; } set { _oid_expediente_rea = value; } }

		#endregion
		
		#region Business Methods
		
		public CobroREARecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_cobro = Format.DataReader.GetInt64(source, "OID_COBRO");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			_cantidad = Format.DataReader.GetDecimal(source, "CANTIDAD");
			_oid_expediente_rea = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE_REA");

		}		
		public virtual void CopyValues(CobroREARecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_cobro = source.OidCobro;
			_oid_expediente = source.OidExpediente;
			_cantidad = source.Cantidad;
			_oid_expediente_rea = source.OidExpedienteRea;
		}
		
		#endregion	
	}

    [Serializable()]
	public class CobroREABase 
	{	 
		#region Attributes
		
		private CobroREARecord _record = new CobroREARecord();
		
		#endregion
		
		#region Properties
		
		public CobroREARecord Record { get { return _record; } }
		
		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);
		}		
		internal void CopyValues(CobroREA source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);
		}
		internal void CopyValues(CobroREAInfo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);
		}
		
		#endregion	
	}
		
	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>	
    [Serializable()]
	public class CobroREA : BusinessBaseEx<CobroREA>
	{	 
		#region Attributes
		
		protected CobroREABase _base = new CobroREABase();
		

		private IVCharges _ivcharges = IVCharges.NewChildList();
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
					PropertyHasChanged();
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
		public virtual long OidExpedienteRea
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

		public virtual IVCharges IVCharges
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _ivcharges;
			}
		}
			
		
		
		/// <summary>
        /// Indica si el objeto está validado
        /// </summary>
		/// <remarks>Para añadir una lista: && _lista.IsValid<remarks/>
		public override bool IsValid
		{
			get { return base.IsValid
						 && _ivcharges.IsValid ; }
		}
		
        /// <summary>
        /// Indica si el objeto está "sucio" (se ha modificado) y se debe actualizar en la base de datos
        /// </summary>
		/// <remarks>Para añadir una lista: || _lista.IsDirty<remarks/>
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _ivcharges.IsDirty ; }
		}
		
		#endregion
		
		#region Business Methods
		
		public virtual CobroREA CloneAsNew()
		{
			CobroREA clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.SessionCode = CobroREA.OpenSession();
			CobroREA.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.IVCharges.MarkAsNew();
			
			return clon;
		}
		
		protected virtual void CopyFrom(CobroREAInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			OidCobro = source.OidCobro;
			OidExpediente = source.OidExpediente;
			Cantidad = source.Cantidad;
			OidExpedienteRea = source.OidExpedienteRea;
		}
		
			
		#endregion
		 
	    #region Validation Rules

		/// <summary>
		/// Añade las reglas de validación necesarias para el objeto
		/// </summary>
		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CheckValidation, "Oid");
		}

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{
						
			
			//Propiedad
			/*if (Propiedad <= 0)
			{
				e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "Propiedad");
				throw new iQValidationException(e.Description, string.Empty);
			}*/

			return true;
		}	
		 
		#endregion
		 
		#region Autorization Rules
				
		public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.FACTURA);
        }
        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.FACTURA);
        }
        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.FACTURA);
        }
        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.FACTURA);
        }

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				CobroREA = CobroREAInfo.New(oid),
			};


			ivchargesList ivcharges = ivchargesList.GetList(conditions, false);

			if (ivcharges.Count > 0)
				throw new iQException(Resources.Messages.ASSOCIATED__IVCHARGE_INTERVALS);
		}
		
		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected CobroREA () 
		{
			Oid = (long)(new Random()).Next();
			EStatus = EEstado.Active;	
		}
				
		private CobroREA(CobroREA source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private CobroREA(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();	
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static CobroREA NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CobroREA obj = DataPortal.Create<CobroREA>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">CobroREA con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(CobroREA source, bool childs)
		/// <remarks/>
		internal static CobroREA GetChild(CobroREA source) { return new CobroREA(source, false); }
		internal static CobroREA GetChild(CobroREA source, bool childs) { return new CobroREA(source, childs); }
        internal static CobroREA GetChild(int sessionCode, IDataReader source) { return new CobroREA(sessionCode, source, false); }
        internal static CobroREA GetChild(int sessionCode, IDataReader source, bool childs) { return new CobroREA(sessionCode, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual CobroREAInfo GetInfo() { return GetInfo(true); }	
		public virtual CobroREAInfo GetInfo (bool childs) { return new CobroREAInfo(this, childs); }
		
		#endregion
		
		#region Root Factory Methods
		
		public static CobroREA New(int sessionCode = -1)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CobroREA obj = DataPortal.Create<CobroREA>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
			return obj;
		}
		
		public new static CobroREA Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return BusinessBaseEx<CobroREA>.Get(query, childs, -1);
		}
		
		public static CobroREA Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }
		
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			IsPosibleDelete(oid);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los CobroREA. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = CobroREA.OpenSession();
			ISession sess = CobroREA.Session(sessCode);
			ITransaction trans = CobroREA.BeginTransaction(sessCode);
			
			try
			{	
				sess.Delete("from CobroREA");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				CobroREA.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override CobroREA Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);			
		
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			try
			{
				ValidationRules.CheckRules();
			}
			catch (iQValidationException ex)
			{
				iQExceptionHandler.TreatException(ex);
				return this;
			}

			try
			{	
				base.Save();				

				_ivcharges.Update(this);				
				if (!SharedTransaction) Transaction().Commit();
				return this;
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
				return this;
			}
			finally
			{
				if (!SharedTransaction)
				{
					if (CloseSessions) CloseSession(); 
					else BeginTransaction();
				}
			}
		}
				
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
			
			// El código va al constructor porque los DataGrid no llamana al DataPortal sino directamente al constructor
			
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(CobroREA source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);
			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{

					IVCharge.DoLOCK(Session());
					string query = IVCharges.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_ivcharges = IVCharges.GetChildList(SessionCode, reader);
									}
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
			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{
					
					IVCharge.DoLOCK(Session());
					string query = IVCharges.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_ivcharges = IVCharges.GetChildList(SessionCode, reader);
					
				}

			}   

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(CobroREsA parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			GetNewCode();
		
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(this);
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(CobroREsA parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			CobroREARecord obj = Session().Get<CobroREARecord>(Oid);
			obj._base.CopyValues(this._base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(CobroREsA parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<CobroREARecord>(Oid));
		
			MarkNew(); 
		}

		#endregion
		
		#region Root Data Access
		
		/// <summary>
		/// Obtiene un registro de la base de datos
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>Lo llama el DataPortal tras generar el objeto</remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
			{
				Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					//CobroREA.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);					
					
					if (Childs)
					{
						string query = string.Empty;
					
						//IVCharge.DoLOCK(Session());
						query = IVCharges.SELECT(this);
						reader = nHMng.SQLNativeSelect(query);
						_ivcharges = IVCharges.GetChildList(SessionCode, reader);
								
					}
				}

				MarkOld();
			}
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
		}
		
		/// <summary>
		/// Inserta un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isNew</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			if (!SharedTransaction)
			{
				SessionCode = OpenSession();
				BeginTransaction();
			}			
			
			Session().Save(this);
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;
			
			CobroREARecord obj = Session().Get<CobroREARecord>(Oid);
			obj.CopyValues(this.Base.Record);
			Session().Update(obj);
			MarkOld();
			
		}
		
		/// <summary>
		/// Borrado aplazado, no se ejecuta hasta que se llama al Save
		/// </summary>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}
		
		/// <summary>
		/// Elimina un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal</remarks>
		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();
					
				//Si no hay integridad referencial, aquí se deben borrar las listas hijo
				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
				Session().Delete((CobroREARecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				CloseSession();
			}
		}		
		
		#endregion
		
				
        #region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>() {};
        }
		
        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
        internal static string SELECT_FIELDS()
        {
            string query;

            query = @"
				SELECT CO.*";

            return query;
        }

		internal static string JOIN(QueryConditions conditions)
		{
            string co = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));

			string query;

            query = @"
				FROM " + co + @" AS CO";
				
			return query + " " + conditions.ExtraJoin;
		}
		
		internal static string WHERE(QueryConditions conditions)
		{
			string query;

            query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "SU", ForeignFields());
				
			query = @" 
				AND (CO.""DATE"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";
 
			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "CO");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "CO");
			
            if (conditions.CobroREA != null)
				query += @"
					AND C.""OID"" = " + conditions.CobroREA.Oid;
				
			
			return query + " " + conditions.ExtraWhere;
		}
		
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
			string query = 
				SELECT_FIELDS() + 
				JOIN(conditions) +
				WHERE(conditions) +		
                ORDER(conditions.Orders, "CO", ForeignFields());

            if (conditions.PagingInfo != null) query += LIMIT(conditions.PagingInfo);

			query += Common.EntityBase.LOCK("CO", lockTable);

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
			return SELECT(new QueryConditions { CobroREA = CobroREAInfo.New(oid) }, lockTable);
        }
		
		#endregion
	}
	
	[Serializable()]
	public class CobroREAMap : ClassMapping<CobroREARecord>
	{	
		public CobroREAMap()
		{
			Table("`CobroREA`");
			//Schema("``");
			Lazy(true);	
			
			Id(x => x.Oid, map =>
            {
                map.Generator(Generators.Sequence,
								gmap => gmap.Params(new { max_low = 100 }));
                map.Column("`OID`");
            });
			Property(x => x.OidCobro, map =>
			{
				map.Column("`OID_COBRO`");
				map.Length(32768);
				});
			Property(x => x.OidExpediente, map =>
			{
				map.Column("`OID_EXPEDIENTE`");
				map.Length(32768);
				});
			Property(x => x.Cantidad, map =>
			{
				map.Column("`CANTIDAD`");
				map.NotNullable(false);
				map.Length(32768);
				});
			Property(x => x.OidExpedienteRea, map =>
			{
				map.Column("`OID_EXPEDIENTE_REA`");
				map.NotNullable(false);
				map.Length(32768);
				});
				}
	}
}
