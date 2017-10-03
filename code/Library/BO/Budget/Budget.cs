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
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class BudgetRecord : RecordBase
	{
		#region Attributes

		private long _oid_serie;
		private long _oid_cliente;
		private long _serial;
		private string _codigo = string.Empty;
		private DateTime _fecha;
		private Decimal _subtotal;
		private Decimal _p_descuento;
		private Decimal _impuestos;
		private Decimal _total;
		private bool _nota = false;
		private string _observaciones = string.Empty;
		private Decimal _p_irpf;
		private int _oid_usuario;

		#endregion

		#region Properties

		public virtual long OidSerie { get { return _oid_serie; } set { _oid_serie = value; } }
		public virtual long OidCliente { get { return _oid_cliente; } set { _oid_cliente = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual Decimal Subtotal { get { return _subtotal; } set { _subtotal = value; } }
		public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
		public virtual Decimal Impuestos { get { return _impuestos; } set { _impuestos = value; } }
		public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual bool Nota { get { return _nota; } set { _nota = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual Decimal PIRPF { get { return _p_irpf; } set { _p_irpf = value; } }
		public virtual int OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }

		#endregion

		#region Business Methods

		public BudgetRecord() {}

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
			_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_subtotal = Format.DataReader.GetDecimal(source, "SUBTOTAL");
			_p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
			_impuestos = Format.DataReader.GetDecimal(source, "IMPUESTOS");
			_total = Format.DataReader.GetDecimal(source, "TOTAL");
			_nota = Format.DataReader.GetBool(source, "NOTA");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_p_irpf = Format.DataReader.GetDecimal(source, "P_IRPF");
			_oid_usuario = Format.DataReader.GetInt32(source, "OID_USUARIO");

		}
		public virtual void CopyValues(BudgetRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_serie = source.OidSerie;
			_oid_cliente = source.OidCliente;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_fecha = source.Fecha;
			_subtotal = source.Subtotal;
			_p_descuento = source.PDescuento;
			_impuestos = source.Impuestos;
			_total = source.Total;
			_nota = source.Nota;
			_observaciones = source.Observaciones;
			_p_irpf = source.PIRPF;
			_oid_usuario = source.OidUsuario;
		}

		#endregion
	}

	[Serializable()]
	public class BudgetBase
	{
		#region Attributes

		private BudgetRecord _record = new BudgetRecord();

		protected string _numero_serie = string.Empty;
		protected string _vat_number = string.Empty;
		protected string _id_cliente;
		protected string _cliente = string.Empty;
		protected string _codigo_postal = string.Empty;
		protected string _usuario = string.Empty;

		#endregion

		#region Properties

		public BudgetRecord Record { get { return _record; } }

		public virtual string VatNumber { get { return _vat_number; } set { _vat_number = value; } }
		public virtual string NumeroSerie { get { return _numero_serie; } set { _numero_serie = value; } }
		public virtual string IDCliente { get { return _id_cliente; } set { _id_cliente = value; } }
		public virtual string Cliente { get { return _cliente; } set { _cliente = value; } }
		public virtual string CodigoPostal { get { return _codigo_postal; } set { _codigo_postal = value; } }
		public virtual Decimal Descuento { get { return Decimal.Round(_record.Subtotal * _record.PDescuento / 100, 2); } }
		public virtual Decimal BaseImponible { get { return Decimal.Round(_record.Subtotal - Descuento, 2); } }
		public virtual Decimal IRPF { get { return Decimal.Round((BaseImponible * _record.PIRPF) / 100, 2); } }
		public virtual string Usuario { get { return _usuario; } set { _usuario = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_numero_serie = Format.DataReader.GetString(source, "SERIE");
			_vat_number = Format.DataReader.GetString(source, "VAT_NUMBER");
			_id_cliente = Format.DataReader.GetString(source, "ID_CLIENTE");
			_cliente = Format.DataReader.GetString(source, "CLIENTE");
			_codigo_postal = Format.DataReader.GetString(source, "CODIGO_POSTAL");
			_usuario = Format.DataReader.GetString(source, "USUARIO");
		}
		internal void CopyValues(Budget source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_vat_number = source.VatNumber;
			_id_cliente = source.NumeroCliente;
			_numero_serie = source.NumeroSerie;
			_cliente = source.NombreCliente;
			_codigo_postal = source.CodigoPostal;
			_usuario = source.Usuario;
		}
		internal void CopyValues(BudgetInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_vat_number = source.VatNumber;
			_id_cliente = source.NumeroCliente;
			_numero_serie = source.NumeroSerie;
			_cliente = source.NombreCliente;
			_codigo_postal = source.CodigoPostal;
			_usuario = source.Usuario;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>	
    [Serializable()]
	public class Budget : BusinessBaseEx<Budget>
	{	 
		#region Attributes

		protected BudgetBase _base = new BudgetBase();

		private BudgetLines _conceptos = BudgetLines.NewChildList();

        private bool _n_manual = false;
		protected decimal _descuento;

		#endregion
		
		#region Properties

		public BudgetBase Base { get { return _base; } }

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
		public virtual long OidSerie
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidSerie;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidSerie.Equals(value))
				{
					_base.Record.OidSerie = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidCliente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCliente;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidCliente.Equals(value))
				{
					_base.Record.OidCliente = value;
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
		public virtual Decimal Subtotal
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Subtotal;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Subtotal.Equals(value))
				{
					_base.Record.Subtotal = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal PDescuento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PDescuento;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PDescuento.Equals(value))
				{
					_base.Record.PDescuento = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Impuestos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Impuestos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Impuestos.Equals(value))
				{
					_base.Record.Impuestos = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Total
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Total;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Total.Equals(value))
				{
					_base.Record.Total = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Nota
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Nota;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Nota.Equals(value))
				{
					_base.Record.Nota = value;
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
		public virtual Decimal PIRPF
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PIRPF;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PIRPF.Equals(value))
				{
					_base.Record.PIRPF = Decimal.Round(value, 2);
					PropertyHasChanged();
				}
			}
		}
		public virtual int OidUsuario
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

		public virtual BudgetLines Conceptos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _conceptos;
			}
		}

		public virtual string VatNumber { get { return _base.VatNumber; } set { _base.VatNumber = value; } }
		public virtual string NumeroSerie { get { return _base.NumeroSerie; } set { _base.NumeroSerie = value; } }
		public virtual string IDCliente { get { return _base.IDCliente; } set { _base.IDCliente = value; } }
		public virtual string NumeroCliente { get { return IDCliente; } } /*DEPRECATED*/
		public virtual string NombreCliente { get { return _base.Cliente; } set { _base.Cliente = value; } }
		public virtual string CodigoPostal { get { return _base.CodigoPostal; } set { _base.CodigoPostal = value; } }
		public virtual Decimal Descuento { get { return _descuento; } set { _descuento = value; } }
        public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal IRPF { get { return _base.IRPF; } }
        public virtual bool NManual { get { return _n_manual; } set { _n_manual = value; } }
		public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }

		public override bool IsValid
		{
			get { return base.IsValid
						 && _conceptos.IsValid ; }
		}
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _conceptos.IsDirty ; }
		}
		
		#endregion
		
		#region Business Methods
		
		/// <summary>
		/// Clona la entidad y sus subentidades y las marca como nuevas
		/// </summary>
		/// <returns>Una entidad clon</returns>
		public virtual Budget CloneAsNew()
		{
			Budget clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();

			clon.GetNewCode();
			clon.Fecha = DateTime.Now;

			clon.SessionCode = Budget.OpenSession();
			Budget.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.Conceptos.MarkAsNew();
			
			return clon;
		}

		protected virtual void CopyFrom(BudgetInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			OidSerie = source.OidSerie;
			OidCliente = source.OidCliente;
			Serial = source.Serial;
			Codigo = source.Codigo;
            Nota = source.Nota;
			Observaciones = source.Observaciones;
			Fecha = source.Fecha;
            PDescuento = source.PDescuento;
			PIRPF = source.PIRPF;
            Subtotal = source.Subtotal;
            Impuestos = source.Impuestos;
			Total = source.Total;

			NumeroSerie = source.NumeroSerie;
		}
		public virtual void CopyFrom(ClienteInfo source)
		{
			if (source == null) return;

			PDescuento = source.PDescuento;
			OidCliente = source.Oid;
			IDCliente = source.Codigo;
			NombreCliente = source.Nombre;
			VatNumber = source.VatNumber;
			CodigoPostal = source.CodigoPostal;
		}
			
        public virtual void GetNewCode()
        {
            // Obtenemos el último serial de servicio
            Serial = SerialProformaInfo.GetNext(this.OidSerie, this.Fecha.Year);
            Codigo = Serial.ToString(Resources.Defaults.FACTURA_CODE_FORMAT);
        }

        public virtual void CalculaTotal()
        {
			Subtotal = 0;
			Impuestos = 0;
			Total = 0;

			foreach (BudgetLine item in Conceptos)
			{
				item.CalculateTotal();

				Subtotal += item.Subtotal;
				Impuestos += item.Impuestos;
			}

			Descuento = Subtotal * PDescuento / 100;
			Total = BaseImponible + Impuestos;
        }

		#endregion
		 
	    #region Validation Rules

		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CheckValidation, "Oid");
		}

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{
			//Codigo
			if (Codigo == string.Empty)
			{
				e.Description = Resources.Messages.NO_ID_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

			//Serie
			if (OidSerie == 0)
			{
				e.Description = Resources.Messages.NO_SERIE_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

			//Cliente
			if (OidCliente == 0)
			{
				e.Description = Resources.Messages.NO_CLIENTE_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

			return true;
		}
		 
		#endregion
		 
		#region Autorization Rules
		
		public static bool CanAddObject()
		{
			return AutorizationRulesControler.CanAddObject(Resources.SecureItems.PROFORMA)
					&& Serie.CanGetObject()
					&& Cliente.CanGetObject()
					&& Proveedor.CanGetObject()
					&& Product.CanGetObject();
		}
		
		public static bool CanGetObject()
		{
			return AutorizationRulesControler.CanGetObject(Resources.SecureItems.PROFORMA)
					&& Serie.CanGetObject()
					&& Cliente.CanGetObject()
					&& Proveedor.CanGetObject()
					&& Product.CanGetObject();
		}
		
		public static bool CanDeleteObject()
		{
			return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.PROFORMA)
					&& Serie.CanGetObject()
					&& Cliente.CanGetObject()
					&& Proveedor.CanGetObject()
					&& Product.CanGetObject();
		}
		
		public static bool CanEditObject()
		{
			return AutorizationRulesControler.CanEditObject(Resources.SecureItems.PROFORMA)
					&& Serie.CanGetObject()
					&& Cliente.CanGetObject()
					&& Proveedor.CanGetObject()
					&& Product.CanGetObject();
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Budget () {}		
		private Budget(Budget source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Budget(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();
			SessionCode = sessionCode;
			Childs = childs;
            Fetch(source);
        }

		public static Budget NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Budget>(new CriteriaCs(-1));
		}
		
		internal static Budget GetChild(Budget source) { return new Budget(source, false); }
		internal static Budget GetChild(Budget source, bool childs)
		{
			return new Budget(source, childs);
		}
        internal static Budget GetChild(int sessionCode, IDataReader source)
        {
            return new Budget(sessionCode, source, false);
        }
        internal static Budget GetChild(int sessionCode, IDataReader source, bool childs)
        {
            return new Budget(sessionCode, source, childs);
        }
		
		public virtual BudgetInfo GetInfo() { return GetInfo(true); }
		public virtual BudgetInfo GetInfo (bool get_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new BudgetInfo(this, get_childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static Budget New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Budget>(new CriteriaCs(-1));
		}
		
		/// <summary>
		/// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
		/// </summary>
		/// <param name="oid"></param>
		/// <returns>Objeto con los valores del registro</returns>
		public static Budget Get(long oid) { return Get(oid, true); }
		public static Budget Get(long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Budget.GetCriteria(Budget.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Budget.SELECT(oid);
			else
				criteria.AddOidSearch(oid);
			
			Budget.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Budget>(criteria);
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
		/// Elimina todos los Proforma. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Budget.OpenSession();
			ISession sess = Budget.Session(sessCode);
			ITransaction trans = Budget.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from BudgetRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				Budget.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Budget Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException( Library.Resources.Messages.USER_NOT_ALLOWED);
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

                // Update de las listas.
                _conceptos.Update(this);

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
		
		#region Common Data Access
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
            _base.Record.Oid = (long)(new Random()).Next();
            GetNewCode();
            Fecha = DateTime.Now;
			OidUsuario = (AppContext.User != null) ? (int)AppContext.User.Oid : 0;
			Usuario = (AppContext.User != null) ? AppContext.User.Name : string.Empty;

            _conceptos = BudgetLines.NewChildList();
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Budget source)
		{
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

				if (Childs)
                {
					if (nHMng.UseDirectSQL)
                    {                        
						BudgetLine.DoLOCK(Session());
                        string query = BudgetLines.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _conceptos = BudgetLines.GetChildList(reader, false);						
                    }
                }
            }
            catch (Exception ex) { throw ex; }

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

                if (Childs)
                {
					if (nHMng.UseDirectSQL)
                    {                        
						BudgetLine.DoLOCK(Session());
                        string query = BudgetLines.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _conceptos = BudgetLines.GetChildList(reader, false);						
                    }
                }
            }
            catch (Exception ex) { throw ex; }

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Budgets parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
                GetNewCode();
				OidUsuario = (AppContext.User != null) ? (int)AppContext.User.Oid : 0;
				Usuario = (AppContext.User != null) ? AppContext.User.Name : string.Empty;
			
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
			}
			catch (Exception ex) { throw ex; }
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(Budgets parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				BudgetRecord obj = Session().Get<BudgetRecord>(Oid);
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
		internal void DeleteSelf(Budgets parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<BudgetRecord>(Oid));
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
					Budget.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;
						
						BudgetLine.DoLOCK(Session());
						query = BudgetLines.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_conceptos = BudgetLines.GetChildList(reader);						
 					} 
				}

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

                GetNewCode();
                OidUsuario = (AppContext.User !=  null) ? (int)AppContext.User.Oid : 0;
                Usuario = (AppContext.User !=  null) ? AppContext.User.Name : string.Empty;

				Session().Save(Base.Record);
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
					BudgetRecord obj = Session().Get<BudgetRecord>(Oid);
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
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();
					
				//Si no hay integridad referencial, aquí se deben borrar las listas hijo
				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
				Session().Delete((BudgetRecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				CloseSession();
			}
		}		
		
		#endregion			
		
        #region SQL

		public new static string SELECT(long oid) { return SELECT(oid, true); }

        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT P.*" +
                    "		,COALESCE(US.\"NAME\", '') AS \"USUARIO\"" +
					"       ,C.\"CODIGO\" AS \"ID_CLIENTE\"" +
                    "       ,C.\"NOMBRE\" AS \"CLIENTE\"" +
					"		,C.\"VAT_NUMBER\" AS \"VAT_NUMBER\"" +
					"		,C.\"CODIGO_POSTAL\" AS \"CODIGO_POSTAL\"" +		
                    "       ,S.\"IDENTIFICADOR\" AS \"SERIE\"";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE (P.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			if (conditions.Proforma != null)
				query += " AND P.\"OID\" = " + conditions.Proforma.Oid;

			if (conditions.Cliente != null) query += " AND P.\"OID_CLIENTE\" = " + conditions.Cliente.Oid;
			if (conditions.Serie != null) query += " AND P.\"OID_SERIE\" = " + conditions.Serie.Oid;
			
			return query;
		}

		internal static string SELECT_BASE(QueryConditions conditions)
		{
			string p = nHManager.Instance.GetSQLTable(typeof(BudgetRecord));
			string ts = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
			string tc = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));

			string query;

			query = SELECT_FIELDS() +
                    " FROM " + p + " AS P" +
                    " LEFT JOIN " + us + " AS US ON US.\"OID\" = P.\"OID_USUARIO\"" +
					" INNER JOIN " + tc + " AS C ON C.\"OID\" = P.\"OID_CLIENTE\"" +
					" INNER JOIN " + ts + " AS S ON S.\"OID\" = P.\"OID_SERIE\"";
			
			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query = string.Empty;

			query = SELECT_BASE(conditions) +
					WHERE(conditions);

			query += " ORDER BY P.\"FECHA\", S.\"IDENTIFICADOR\", P.\"CODIGO\"";

			query += Common.EntityBase.LOCK("P", lockTable);

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			query = SELECT(new QueryConditions { Proforma = BudgetInfo.New(oid) }, lockTable);

			return query;
		}
		
		#endregion
	}
}

