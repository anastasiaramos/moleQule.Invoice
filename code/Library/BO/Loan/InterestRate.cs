using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;
using moleQule.Library;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class InterestRateRecord : RecordBase
	{
		#region Attributes

		private long _oid_prestamo;
		private Decimal _tipo_interes;
		private DateTime _fecha_inicio;
		private DateTime _fecha_fin;
		private Decimal _importe_cuota;
		#endregion

		#region Properties

		public virtual long OidPrestamo { get { return _oid_prestamo; } set { _oid_prestamo = value; } }
		public virtual Decimal TipoInteres { get { return _tipo_interes; } set { _tipo_interes = value; } }
		public virtual DateTime FechaInicio { get { return _fecha_inicio; } set { _fecha_inicio = value; } }
		public virtual DateTime FechaFin { get { return _fecha_fin; } set { _fecha_fin = value; } }
		public virtual Decimal ImporteCuota { get { return _importe_cuota; } set { _importe_cuota = value; } }

		#endregion

		#region Business Methods

		public InterestRateRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_prestamo = Format.DataReader.GetInt64(source, "OID_PRESTAMO");
			_tipo_interes = Format.DataReader.GetDecimal(source, "TIPO_INTERES");
			_fecha_inicio = Format.DataReader.GetDateTime(source, "FECHA_INICIO");
			_fecha_fin = Format.DataReader.GetDateTime(source, "FECHA_FIN");
			_importe_cuota = Format.DataReader.GetDecimal(source, "IMPORTE_CUOTA");

		}
		public virtual void CopyValues(InterestRateRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_prestamo = source.OidPrestamo;
			_tipo_interes = source.TipoInteres;
			_fecha_inicio = source.FechaInicio;
			_fecha_fin = source.FechaFin;
			_importe_cuota = source.ImporteCuota;
		}

		#endregion
	}

	[Serializable()]
	public class InterestRateBase
	{
		#region Attributes

		private InterestRateRecord _record = new InterestRateRecord();

		#endregion

		#region Properties

		public InterestRateRecord Record { get { return _record; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);
		}
		internal void CopyValues(InterestRate source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}
		internal void CopyValues(InterestRateInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}

		#endregion
	}

    /// <summary>
    /// Editable Child Business Object
    /// </summary>	
    [Serializable()]
    public class InterestRate : BusinessBaseEx<InterestRate>
    {
        #region Attributes

		protected InterestRateBase _base = new InterestRateBase();

        #endregion

        #region Properties

		public InterestRateBase Base { get { return _base; } }

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
		public virtual long OidPrestamo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidPrestamo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidPrestamo.Equals(value))
				{
					_base.Record.OidPrestamo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Tipo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoInteres;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.TipoInteres.Equals(value))
				{
					_base.Record.TipoInteres = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FechaInicio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaInicio;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaInicio.Equals(value))
				{
					_base.Record.FechaInicio = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FechaFin
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaFin;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaFin.Equals(value))
				{
					_base.Record.FechaFin = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal ImporteCuota
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ImporteCuota;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.ImporteCuota.Equals(value))
				{
					_base.Record.ImporteCuota = value;
					PropertyHasChanged();
				}
			}
		}

        #endregion

        #region Business Methods

        public virtual InterestRate CloneAsNew()
        {
            InterestRate clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            
            clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.SessionCode = Invoice.InterestRate.OpenSession();
			Invoice.InterestRate.BeginTransaction(clon.SessionCode);

            clon.MarkNew();

            return clon;
        }

        protected virtual void CopyFrom(InterestRateInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
            OidPrestamo = source.OidPrestamo;
            Tipo = source.Tipo;
            FechaInicio = source.FechaInicio;
            FechaFin = source.FechaFin;
            ImporteCuota = source.ImporteCuota;
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
            if (OidPrestamo <= 0)
            {
                e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "OidPrestamo");
                throw new iQValidationException(e.Description, string.Empty);
            }

            return true;
        }

        #endregion

        #region Autorization Rules

        public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.MOVIMIENTO_BANCO);
        }

        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.MOVIMIENTO_BANCO);
        }

        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.MOVIMIENTO_BANCO);
        }

        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.MOVIMIENTO_BANCO);
        }

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
        /// pero debe ser protected por exigencia de NHibernate.
        /// Debe ser public para que funcionen los DataGridView
        /// </summary>
        public InterestRate()
        {
            MarkAsChild();
        }

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
        /// </summary>
        private InterestRate(InterestRate source, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            Fetch(source);
        }
        private InterestRate(int sessionCode, IDataReader source, bool childs)
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
        public static InterestRate NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            InterestRate obj = DataPortal.Create<InterestRate>(new CriteriaCs(-1));
            obj.MarkAsChild();
            return obj;
        }

        /// <summary>
        /// Crea un objeto
        /// </summary>
        /// <param name="source">TipoInteres con los datos para el objeto</param>
        /// <returns>Objeto creado</returns>
        /// <remarks>
        /// La utiliza la BusinessListBaseEx correspondiente para montar la lista
        /// NO OBTIENE los hijos. Para ello utilice GetChild(TipoInteres source, bool childs)
        /// <remarks/>
        internal static InterestRate GetChild(InterestRate source) { return new InterestRate(source, false); }
        internal static InterestRate GetChild(InterestRate source, bool childs) { return new InterestRate(source, childs); }
        internal static InterestRate GetChild(int sessionCode, IDataReader source) { return new InterestRate(sessionCode, source, false); }
        internal static InterestRate GetChild(int sessionCode, IDataReader source, bool childs) { return new InterestRate(sessionCode, source, childs); }

        /// <summary>
        /// Construye y devuelve un objeto de solo lectura copia de si mismo.
        /// </summary>
        /// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
        /// <returns>Réplica de solo lectura del objeto</returns>
        public virtual InterestRateInfo GetInfo() { return GetInfo(true); }
        public virtual InterestRateInfo GetInfo(bool childs)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new InterestRateInfo(this, childs);
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LO UTILIZA LA FUNCION DE CREACION DE LA LISTA DEL PADRE
        /// </summary>
        private InterestRate(Loan parent)
        {
            OidPrestamo = parent.Oid;
            MarkAsChild();
        }

        /// <summary>
        /// Crea un nuevo objeto hijo
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        /// <returns>Nuevo objeto creado</returns>
        internal static InterestRate NewChild(Loan parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new InterestRate(parent);
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
        /// No se debe utilizar esta función para guardar. Hace falta el padre, que
        /// debe utilizar Insert o Update en sustitución de Save.
        /// </summary>
        /// <returns></returns>
        public override InterestRate Save()
        {
            throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
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
        private void Fetch(InterestRate source)
        {
            SessionCode = source.SessionCode;

            _base.CopyValues(source);

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

            MarkOld();
        }

        /// <summary>
        /// Inserta el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
        internal void Insert(InterestRates parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;
            
            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            parent.Session().Save(Base.Record);

            MarkOld();
        }

        /// <summary>
        /// Actualiza el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
        internal void Update(InterestRates parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            SessionCode = parent.SessionCode;
			InterestRateRecord obj = Session().Get<InterestRateRecord>(Oid);
            obj.CopyValues(Base.Record);
            Session().Update(obj);

            MarkOld();
        }

        /// <summary>
        /// Borra el registro de la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
        internal void DeleteSelf(InterestRates parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;
            Session().Delete(Session().Get<InterestRateRecord>(Oid));

            MarkNew();
        }

        #endregion

        #region Child Data Access

        /// <summary>
        /// Inserta un registro en la base de datos
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        internal void Insert(Loan parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

            OidPrestamo = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            parent.Session().Save(Base.Record);

            MarkOld();
        }

        /// <summary>
        /// Actualiza un registro en la base de datos
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        internal void Update(Loan parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

            OidPrestamo = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			InterestRateRecord obj = parent.Session().Get<InterestRateRecord>(Oid);
            obj.CopyValues(Base.Record);
            parent.Session().Update(obj);

            MarkOld();
        }

        /// <summary>
        /// Borra un registro de la base de datos.
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        /// <remarks>Borrado inmediato<remarks/>
        internal void DeleteSelf(Loan parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<InterestRateRecord>(Oid));

            MarkNew();
        }

        #endregion

        #region SQL

        //public new static string SELECT(long oid) { return SELECT(oid, true); }
        public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

        public static string SELECT(Loan item)
        {
            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions { Prestamo = item.GetInfo(false) };
            return SELECT(conditions, false);
        }

        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT T.*";

            return query;
        }

        internal static string WHERE(QueryConditions conditions)
        {
            string query;

            query = " WHERE (T.\"FECHA_INICIO\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            //if (conditions.TipoInteres != null)
            //    if (conditions.TipoInteres.Oid != 0)
            //        query += " AND T.\"OID\" = " + conditions.TipoInteres.Oid;


            if (conditions.Prestamo != null) query += " AND T.\"OID_PRESTAMO\" = " + conditions.Prestamo.Oid;



            return query;
        }

        //internal static string SELECT(long oid, bool lock_table)
        //{
        //    string query = string.Empty;

        //    QueryConditions conditions = new QueryConditions { TipoInteres = TipoInteres.New().GetInfo(false) };
        //    conditions.TipoInteres.Oid = oid;

        //    query = SELECT(conditions, lock_table);

        //    return query;
        //}

        internal static string SELECT(QueryConditions conditions, bool lock_table)
        {
			string t = nHManager.Instance.GetSQLTable(typeof(InterestRateRecord));

            string query;

            query = SELECT_FIELDS() +
                    " FROM " + t + " AS T";

            query += WHERE(conditions);

            //if (lock_table) query += " FOR UPDATE OF T NOWAIT";

            return query;
        }

        #endregion
    }
}