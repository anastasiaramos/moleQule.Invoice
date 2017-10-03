using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using NHibernate;
using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx;
using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class CashCountRecord : RecordBase
	{
		#region Attributes

		private long _oid_caja;
		private long _serial;
		private string _codigo = string.Empty;
		private Decimal _debe;
		private Decimal _haber;
		private DateTime _fecha;
		private string _observaciones = string.Empty;
		private long _oid_usuario;

		#endregion

		#region Properties

		public virtual long OidCaja { get { return _oid_caja; } set { _oid_caja = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual Decimal Debe { get { return _debe; } set { _debe = value; } }
		public virtual Decimal Haber { get { return _haber; } set { _haber = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }

		#endregion

		#region Business Methods

		public CashCountRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_caja = Format.DataReader.GetInt64(source, "OID_CAJA");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_debe = Format.DataReader.GetDecimal(source, "DEBE");
			_haber = Format.DataReader.GetDecimal(source, "HABER");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");

		}
		public virtual void CopyValues(CashCountRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_caja = source.OidCaja;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_debe = source.Debe;
			_haber = source.Haber;
			_fecha = source.Fecha;
			_observaciones = source.Observaciones;
			_oid_usuario = source.OidUsuario;
		}

		#endregion
	}

	[Serializable()]
	public class CashCountBase
	{
		#region Attributes

		private CashCountRecord _record = new CashCountRecord();

		protected string _usuario = string.Empty;
		protected decimal _saldo_inicial = 0;
		protected decimal _saldo_final = 0;
		protected decimal _debe_acumulado;
		protected decimal _haber_acumulado;
		protected string _caja = string.Empty;

		#endregion

		#region Properties

		public CashCountRecord Record { get { return _record; } }

		public virtual string Usuario { get { return _usuario; } set { _usuario = value; } }
		public virtual string Caja { get { return _caja; } set { _caja = value; } }
		public virtual decimal DebeAcumulado { get { return _debe_acumulado; } set { _debe_acumulado = value; } }
		public virtual decimal HaberAcumulado { get { return _haber_acumulado; } set { _haber_acumulado = value; } }
		public virtual decimal SaldoInicial { get { return _saldo_inicial; } set { _saldo_inicial = value; } }
		public virtual decimal Saldo { get { return _record.Debe - _record.Haber; } }
		public virtual decimal SaldoAcumulado { get { return _debe_acumulado - _haber_acumulado; } }
		public virtual decimal SaldoFinal { get { return _saldo_final; } set { _saldo_final = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_debe_acumulado = Format.DataReader.GetDecimal(source, "DEBE_ACUMULADO");
			_haber_acumulado = Format.DataReader.GetDecimal(source, "HABER_ACUMULADO");
			_caja = Format.DataReader.GetString(source, "CAJA");
			_usuario = Format.DataReader.GetString(source, "USUARIO");

			_saldo_final = SaldoAcumulado;
			//_saldo_inicial = Format.DataReader.GetDecimal(source, "SALDO_INICIAL");
		}
		internal void CopyValues(CierreCaja source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_debe_acumulado = source.DebeAcumulado;
			_haber_acumulado = source.HaberAcumulado;
			_caja = source.Caja;
			_saldo_final = source.SaldoFinal;
			_usuario = source.Usuario;
		}
		internal void CopyValues(CierreCajaInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_debe_acumulado = source.DebeAcumulado;
			_haber_acumulado = source.HaberAcumulado;
			_caja = source.Caja;
			_saldo_final = source.SaldoFinal;
			_usuario = source.Usuario;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// Editable Child Business Object With Editable Child Collection
	/// </summary>	
    [Serializable()]
	public class CierreCaja : BusinessBaseEx<CierreCaja>
	{
		#region Attributes

		protected CashCountBase _base = new CashCountBase();

		private CashLines _lineas = CashLines.NewChildList();

		#endregion

		#region Properties

		public CashCountBase Base { get { return _base; } }

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
		public virtual long OidCaja
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCaja;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCaja.Equals(value))
				{
					_base.Record.OidCaja = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Serial
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Serial;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Serial.Equals(value))
				{
					_base.Record.Serial = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Codigo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Codigo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Codigo.Equals(value))
				{
					_base.Record.Codigo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Debe
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Debe;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Debe.Equals(value))
				{
					_base.Record.Debe = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Haber
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Haber;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Haber.Equals(value))
				{
					_base.Record.Haber = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Fecha
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Fecha;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Fecha.Equals(value))
				{
					_base.Record.Fecha = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Observaciones
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Observaciones;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Observaciones.Equals(value))
				{
					_base.Record.Observaciones = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidUsuario
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidUsuario;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidUsuario.Equals(value))
				{
					_base.Record.OidUsuario = value;
					PropertyHasChanged();
				}
			}
		}
		
		public virtual CashLines LineaCajas
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _lineas;
			}
		}

        //NO ENLAZADAS
		public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }
		public virtual string Caja { get { return _base.Caja; } set { _base.Caja = value; PropertyHasChanged(); } }
		public virtual decimal DebeAcumulado { get { return _base.DebeAcumulado; } set { _base.DebeAcumulado = value; } }
		public virtual decimal HaberAcumulado { get { return _base.HaberAcumulado; } set { _base.HaberAcumulado = value; } }
		public virtual decimal SaldoInicial { get { return _base.SaldoInicial; } set { _base.SaldoInicial = value; } }
		public virtual decimal Saldo { get { return _base.Saldo; } }
		public virtual decimal SaldoAcumulado { get { return _base.SaldoAcumulado; } }
		public virtual decimal SaldoFinal { get { return _base.SaldoFinal; } set { _base.SaldoFinal = value; } }

		/// <summary>
        /// Indica si el objeto está validado
        /// </summary>
		/// <remarks>Para añadir una lista: && _lista.IsValid<remarks/>
		public override bool IsValid
		{
			get { return base.IsValid
						 && _lineas.IsValid ; }
		}
		
        /// <summary>
        /// Indica si el objeto está "sucio" (se ha modificado) y se debe actualizar en la base de datos
        /// </summary>
		/// <remarks>Para añadir una lista: || _lista.IsDirty<remarks/>
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _lineas.IsDirty ; }
		}
		
		#endregion
		
		#region Business Methods
		
		public virtual CierreCaja CloneAsNew()
		{
			CierreCaja clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.Codigo = (0).ToString(Resources.Defaults.CIERRCAJA_CODE_FORMAT);
			
			clon.SessionCode = CierreCaja.OpenSession();
			CierreCaja.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.LineaCajas.MarkAsNew();
			
			return clon;
		}
		
		protected virtual void CopyFrom(CierreCajaInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			OidCaja = source.OidCaja;
			Codigo = source.Codigo;
			Fecha = source.Fecha;
			Observaciones = source.Observaciones;

			DebeAcumulado = source.DebeAcumulado;
			HaberAcumulado = source.HaberAcumulado;
			Caja = source.Caja;
			Usuario = source.Usuario;
		}
		public virtual void CopyFrom(Cash source)
		{
			if (source == null) return;
			CopyFrom(source.GetInfo(false));
		}
		public virtual void CopyFrom(CashInfo source)
		{
			if (source == null) return;

			OidCaja = source.Oid;
			Debe = source.DebeParcial;
			Haber = source.HaberParcial;
			Caja = source.Nombre;

			SaldoFinal = source.SaldoTotal;
		}

		public virtual void GetNewCode(long oid_caja)
		{
			Serial = SerialCierreCajaInfo.GetNext(oid_caja, Fecha.Year);
			Codigo = Serial.ToString(Resources.Defaults.CIERRCAJA_CODE_FORMAT);
		}

        public virtual void UpdateSaldo()
        {
            if (LineaCajas.Count == 0) return;

			SaldoInicial = SaldoAcumulado - Saldo;
			Debe = 0;
			Haber = 0;

 			int i = 0;
			for (i = 0; i < LineaCajas.Count; i++)
			{
				CashLine item = LineaCajas[i];

				if (item.EEstado == EEstado.Anulado)
				{
					LineaCajas[i].Saldo = 0;
					continue;
				}

				item.Saldo = SaldoInicial + item.Debe - item.Haber;
				Debe += item.Debe;
				Haber += item.Haber;
				break;
			}

			int last_abierto = i;

			for (int j = i + 1; j < LineaCajas.Count; j++)
			{
				CashLine item = LineaCajas[j];

				if (item.EEstado == EEstado.Anulado)
				{
					item.Saldo = 0;
					continue;
				}

				item.Saldo = LineaCajas[last_abierto].Saldo + item.Debe - item.Haber;
				Debe += item.Debe;
				Haber += item.Haber;
				last_abierto = j;
			}

			SaldoFinal = SaldoInicial + Saldo;
        }

        public virtual void SetLineasCierre(Cash caja, DateTime fecha)
        {
            OidCaja = caja.Oid;
            Debe = 0;
            Haber = 0;

            List<CashLine> lineas_cierre = caja.Lines.GetSubList(new FCriteria<DateTime>("Fecha", fecha, Operation.LessOrEqual));

            if (lineas_cierre.Count == 0)
                throw new iQException(Resources.Messages.NO_LINEAS_CAJA);

            foreach (CashLine item in lineas_cierre)
            {
                item.OidCierre = Oid;
				
				if (item.EEstado != EEstado.Anulado)
				{
					Debe += item.Debe;
					Haber += item.Haber;
				}

                LineaCajas.MoveItem(item);
            }

			SaldoFinal = SaldoInicial + Saldo;
        }
	
        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            return true;
        }
		 
		#endregion
		 
		#region Autorization Rules
		
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
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected CierreCaja() {}		
		private CierreCaja(CierreCaja source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private CierreCaja(int sessionCode, IDataReader source, bool childs)
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
		public static CierreCaja NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
                  Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<CierreCaja>(new CriteriaCs(-1));
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">CierreCaja con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(CierreCaja source, bool childs)
		/// <remarks/>
		internal static CierreCaja GetChild(CierreCaja source)
		{
			return new CierreCaja(source, false);
		}
		internal static CierreCaja GetChild(CierreCaja source, bool childs)
		{
			return new CierreCaja(source, childs);
		}
		internal static CierreCaja GetChild(int sessionCode, IDataReader source) { return new CierreCaja(sessionCode, source, false); }
		internal static CierreCaja GetChild(int sessionCode, IDataReader source, bool childs)
        {
            return new CierreCaja (sessionCode, source, childs);
        }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// También copia los datos de los hijos del objeto.
		/// </summary>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual CierreCajaInfo GetInfo()
		{
			return GetInfo(true);
		}
		public virtual CierreCajaInfo GetInfo (bool get_childs)
		{
			if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new CierreCajaInfo(this, get_childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static CierreCaja New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<CierreCaja>(new CriteriaCs(-1));
		}
		
		/// <summary>
		/// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
		/// </summary>
		/// <param name="oid"></param>
		/// <returns>Objeto con los valores del registro</returns>
		public static CierreCaja Get(long oid) { return Get(oid, true); }
		public static CierreCaja Get(long oid, bool childs)
		{
			if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = CierreCaja.GetCriteria(CierreCaja.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = CierreCaja.SELECT(oid);
			
			CierreCaja.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<CierreCaja>(criteria);
		}
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los CierreCaja. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = CierreCaja.OpenSession();
			ISession sess = CierreCaja.Session(sessCode);
			ITransaction trans = CierreCaja.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from CashCountRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				CierreCaja.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override CierreCaja Save()
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

                _lineas.Update(this);

				Transaction().Commit();

				return this;
			}
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally
            {
                if (CloseSessions) CloseSession();
				else BeginTransaction();
            }
        }
				
		#endregion				
		
		#region Child Factory Methods
		
		/// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LO UTILIZA LA FUNCION DE CREACION DE LA LISTA DEL PADRE
        /// </summary>
        private CierreCaja(Cash parent)
        {
            OidCaja = parent.Oid;
            MarkAsChild();
        }
		
		/// <summary>
		/// Crea un nuevo objeto hijo
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <returns>Nuevo objeto creado</returns>
		internal static CierreCaja NewChild(Cash parent)
		{
			if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return new CierreCaja(parent);
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
            Random r = new Random();
            Oid = (long)r.Next();
			CopyFrom(CashInfo.Get(1, false));
			GetNewCode(1);
			OidUsuario = AppContext.User.Oid;
			Usuario = AppContext.User.Name;
            Fecha = DateTime.Now;
		}
		
		private void Fetch(CierreCaja source)
		{
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

				if (Childs)
                {
					if (nHMng.UseDirectSQL)
                    {                        
						CashLine.DoLOCK(Session());
                        string query = CashLines.SELECT_BY_CIERRE(this.Oid);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _lineas = CashLines.GetChildList(reader, false);						
                    }
                    else
					{					    
						CriteriaEx criteria = CashLine.GetCriteria(Session());
						criteria.AddEq("OidCierre", this.Oid);
						_lineas = CashLines.GetChildList(criteria.List<CashLine>());						
					}

                    UpdateSaldo();
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

			MarkOld();
		}

        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);

                if (Childs)
                {
					if (nHMng.UseDirectSQL)
                    {                        
						CashLine.DoLOCK(Session());
                        string query = CashLines.SELECT_BY_CIERRE(this.Oid);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _lineas = CashLines.GetChildList(reader, false);						
                    }

                    UpdateSaldo();
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

		internal void Insert(CierreCajas parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{	
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);

				DebeAcumulado += Debe;
				HaberAcumulado += Haber;
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
	
		internal void Update(CierreCajas parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				CashCountRecord obj = Session().Get<CashCountRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}
		
		internal void DeleteSelf(CierreCajas parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<CashCountRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
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
				_base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					CierreCaja.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;
						
						CashLine.DoLOCK(Session());
						query = CashLines.SELECT_BY_CIERRE(this.Oid);
						reader = nHMng.SQLNativeSelect(query, Session());
						_lineas = CashLines.GetChildList(reader);						
 					} 
				}

                UpdateSaldo();
				MarkOld();
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		/// <summary>
		/// Inserta un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isNew</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }

                GetNewCode(OidCaja);

				Session().Save(Base.Record);

				DebeAcumulado += Debe;
				HaberAcumulado += Haber;
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
					CashCountRecord obj = Session().Get<CashCountRecord>(Oid);
					obj.CopyValues(Base.Record);
					Session().Update(obj);
					MarkOld();
				}
				catch (Exception ex)
				{
					iQExceptionHandler.TreatException(ex);
				}
			}
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
            CierreCaja obj = null;

			try
			{
                // Iniciamos la conexion y la transaccion
                obj = CierreCaja.Get(criteria.Oid, true);

				//Liberamos las lineas de caja del cierre
				QueryConditions conditions = new QueryConditions
				{
					CierreCaja = obj.GetInfo(false),
				};

				nHMng.SQLNativeExecute(CashLine.UPDATE_LIBERA_LINEAS_A(conditions), obj.Session());
				nHMng.SQLNativeExecute(CashLine.UPDATE_LIBERA_LINEAS_B(conditions), obj.Session());

                obj.Session().Delete(obj.Base.Record);
				obj.Transaction().Commit();
 		    }
			catch (Exception ex)
			{
				if ((obj != null) && (obj.Transaction() != null)) obj.Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				if (obj != null) obj.CloseSession();
			}
		}		
		
		#endregion
		
		#region Child Data Access
		
		/// <summary>
		/// Inserta un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Insert(Cash parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidCaja = parent.Oid;				

			try
			{
				ValidationRules.CheckRules();
				
				if (!IsValid)
					throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);					
				
				_lineas.Update(this);
				
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		/// <summary>
		/// Actualiza un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Update(Cash parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidCaja = parent.Oid;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				CashCountRecord obj = parent.Session().Get<CashCountRecord>(Oid);
				obj.CopyValues(Base.Record);
				parent.Session().Update(obj);

				_lineas.Update(this);
				
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		/// <summary>
		/// Borra un registro de la base de datos.
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <remarks>Borrado inmediato<remarks/>
		internal void DeleteSelf(Cash parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<CashCountRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkNew();
		}
		
		#endregion

        #region SQL

		public new static string SELECT(long oid) { return SELECT(oid, true); }

        public static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT CC.*" +
                    "       ,SUM(CC1.\"DEBE\") AS \"DEBE_ACUMULADO\"" +
					"		,SUM(CC1.\"HABER\") AS \"HABER_ACUMULADO\"" +
                    "       ,C.\"NOMBRE\" AS \"CAJA\"" +
					"		,US.\"NAME\" AS \"USUARIO\"";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE (CC.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			if (conditions.CierreCaja != null) query += " AND CC.\"OID\" = " + conditions.CierreCaja.Oid;
			if (conditions.Caja != null) query += " AND CC.\"OID_CAJA\" = " + conditions.Caja.Oid;

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string cc = nHManager.Instance.GetSQLTable(typeof(CashCountRecord));
			string c = nHManager.Instance.GetSQLTable(typeof(CashRecord));
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));

			string query;

			query = SELECT_FIELDS() +
					" FROM " + cc + " AS CC" +
					" LEFT JOIN " + cc + " AS CC1 ON CC1.\"OID_CAJA\" = CC.\"OID_CAJA\" AND CC1.\"FECHA\" <= CC.\"FECHA\"" +
					" INNER JOIN " + c + " AS C ON CC.\"OID_CAJA\" = C.\"OID\"" +
					" LEFT JOIN " + us + " AS US ON US.\"OID\" = CC.\"OID_USUARIO\"";

			query += WHERE(conditions);

			query += " GROUP BY CC.\"OID\", CC.\"OID_CAJA\", CC.\"SERIAL\", CC.\"CODIGO\", CC.\"DEBE\"," +
					 "           CC.\"HABER\", CC.\"FECHA\", CC.\"OBSERVACIONES\", C.\"NOMBRE\", CC.\"OID_USUARIO\", US.\"NAME\"";

			//if (lock_table) query += " FOR UPDATE OF CC NOWAIT";

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			return SELECT(new QueryConditions { CierreCaja = CierreCajaInfo.New(oid) }, lockTable);
		}

        internal static string SELECT_BY_FECHA(DateTime fecha, bool lockTable)
        {
            string cc = nHManager.Instance.GetSQLTable(typeof(CashCountRecord));
            string c = nHManager.Instance.GetSQLTable(typeof(CashRecord));
			string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));

            string query;

            query = SELECT_FIELDS() +
                    " FROM " + cc + " AS CC" +
                    " LEFT JOIN " + cc + " AS CC1 ON CC1.\"OID_CAJA\" = CC.\"OID_CAJA\" AND CC1.\"FECHA\" <= CC.\"FECHA\"" +
                    " INNER JOIN " + c + " AS C ON CC.\"OID_CAJA\" = C.\"OID\"" +
					" LEFT JOIN " + us + " AS US ON US.\"OID\" = CC.\"OID_USUARIO\"" +
                    " WHERE CC.\"FECHA\" >= '" + fecha.ToString("MM/dd/yyyy") + "'" +
                    " GROUP BY CC.\"OID\", CC.\"OID_CAJA\", CC.\"SERIAL\", CC.\"CODIGO\", CC.\"DEBE\"," +
					"           CC.\"HABER\", CC.\"FECHA\", CC.\"OBSERVACIONES\", C.\"NOMBRE\", CC.\"OID_USUARIO\", US.\"NAME\"";

            //if (lock_table) query += " FOR UPDATE OF CC NOWAIT";

            return query;
        }

		#endregion
	}
}

