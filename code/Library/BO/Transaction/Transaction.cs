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
using moleQule.Library.Common;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class TransactionRecord : RecordBase
	{
		#region Attributes

		private long _oid_entity;
		private long _entity_type;
		private long _serial;
		private string _code = string.Empty;
		private long _type;
		private long _status;
		private DateTime _created;
		private DateTime _resolved;
		private string _transaction_id = string.Empty;
		private string _transaction_id_ext = string.Empty;
		private string _auth_code = string.Empty;
		private string _pan_mask = string.Empty;
		private Decimal _amount;
		private long _currency;
		private long _gateway;
		private string _description = string.Empty;
		private string _response = string.Empty;

		#endregion

		#region Properties

		public virtual long OidEntity { get { return _oid_entity; } set { _oid_entity = value; } }
		public virtual long EntityType { get { return _entity_type; } set { _entity_type = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Code { get { return _code; } set { _code = value; } }
		public virtual long Type { get { return _type; } set { _type = value; } }
		public virtual long Status { get { return _status; } set { _status = value; } }
		public virtual DateTime Created { get { return _created; } set { _created = value; } }
		public virtual DateTime Resolved { get { return _resolved; } set { _resolved = value; } }
		public virtual string TransactionID { get { return _transaction_id; } set { _transaction_id = value; } }
		public virtual string TransactionIDExt { get { return _transaction_id_ext; } set { _transaction_id_ext = value; } }
		public virtual string AuthCode { get { return _auth_code; } set { _auth_code = value; } }
		public virtual string PanMask { get { return _pan_mask; } set { _pan_mask = value; } }
		public virtual Decimal Amount { get { return _amount; } set { _amount = value; } }
		public virtual long Currency { get { return _currency; } set { _currency = value; } }
		public virtual long Gateway { get { return _gateway; } set { _gateway = value; } }
		public virtual string Description { get { return _description; } set { _description = value; } }
		public virtual string Response { get { return _response; } set { _response = value; } }

		#endregion

		#region Business Methods

		public TransactionRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_entity = Format.DataReader.GetInt64(source, "OID_ENTITY");
			_entity_type = Format.DataReader.GetInt64(source, "ENTITY_TYPE");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_code = Format.DataReader.GetString(source, "CODE");
			_type = Format.DataReader.GetInt64(source, "TYPE");
			_status = Format.DataReader.GetInt64(source, "STATUS");
			_created = Format.DataReader.GetDateTime(source, "CREATED");
			_resolved = Format.DataReader.GetDateTime(source, "RESOLVED");
			_transaction_id = Format.DataReader.GetString(source, "TRANSACTIONID");
			_transaction_id_ext = Format.DataReader.GetString(source, "TRANSACTIONID_EXT");
			_auth_code = Format.DataReader.GetString(source, "AUTH_CODE");
			_pan_mask = Format.DataReader.GetString(source, "PAN_MASK");
			_amount = Format.DataReader.GetDecimal(source, "AMOUNT");
			_currency = Format.DataReader.GetInt64(source, "CURRENCY");
			_gateway = Format.DataReader.GetInt64(source, "GATEWAY");
			_description = Format.DataReader.GetString(source, "DESCRIPTION");
			_response = Format.DataReader.GetString(source, "RESPONSE");

		}
		public virtual void CopyValues(TransactionRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_entity = source.OidEntity;
			_entity_type = source.EntityType;
			_serial = source.Serial;
			_code = source.Code;
			_type = source.Type;
			_status = source.Status;
			_created = source.Created;
			_resolved = source.Resolved;
			_transaction_id = source.TransactionID;
			_transaction_id_ext = source.TransactionIDExt;
			_auth_code = source.AuthCode;
			_pan_mask = source.PanMask;
			_amount = source.Amount;
			_currency = source.Currency;
			_gateway = source.Gateway;
			_description = source.Description;
			_response = source.Response;
		}

		#endregion
	}

	[Serializable()]
	public class TransactionBase
	{
		#region Attributes

		private TransactionRecord _record = new TransactionRecord();

		#endregion

		#region Properties

		public TransactionRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Status; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
		public ETipoEntidad EEntityType { get { return (ETipoEntidad)_record.EntityType; } set { _record.EntityType = (long)value; } }
		public string EntityTypeLabel { get { return Library.Common.EnumText<ETipoEntidad>.GetLabel(EEntityType); } }
		public EPaymentGateway EPaymentGateway { get { return (EPaymentGateway)_record.Gateway; } }
		public string PaymentGatewayLabel { get { return Library.Common.EnumText<EPaymentGateway>.GetLabel(EPaymentGateway); } }
		public ECurrency ECurrency { get { return (ECurrency)_record.Currency; } set { _record.Currency = (long)value; } }
		public string CurrencyLabel { get { return Library.Common.EnumText<ECurrency>.GetLabel(ECurrency); } }
		public ETransactionType ETransactionType { get { return (ETransactionType)_record.Type; } set { _record.Type = (long)value; } }
		public string TransactionTypeLabel { get { return Library.Common.EnumText<ETransactionType>.GetLabel(ETransactionType); } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);
		}
		public void CopyValues(Transaction source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}
		public void CopyValues(TransactionInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}

		#endregion
	}
		
	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class Transaction : BusinessBaseEx<Transaction>
	{	 
		#region Attributes

		protected TransactionBase _base = new TransactionBase();	

		#endregion
		
		#region Properties

		public TransactionBase Base { get { return _base; } }

		public override long Oid
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				return _base.Record.Oid;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				if (!_base.Record.Oid.Equals(value))
				{
					_base.Record.Oid = value;
				}
			}
		}
		public virtual long OidEntity
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidEntity;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidEntity.Equals(value))
				{
					_base.Record.OidEntity = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long EntityType
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.EntityType;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.EntityType.Equals(value))
				{
					_base.Record.EntityType = value;
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
		public virtual string Code
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Code;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Code.Equals(value))
				{
					_base.Record.Code = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Type
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Type;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Type.Equals(value))
				{
					_base.Record.Type = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Status
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Status;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Status.Equals(value))
				{
					_base.Record.Status = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Created
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Created;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Created.Equals(value))
				{
					_base.Record.Created = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Resolved
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Resolved;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Resolved.Equals(value))
				{
					_base.Record.Resolved = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string TransactionID
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TransactionID;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.TransactionID.Equals(value))
				{
					_base.Record.TransactionID = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string TransactionIDExt
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TransactionIDExt;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.TransactionIDExt.Equals(value))
				{
					_base.Record.TransactionIDExt = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string AuthCode
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.AuthCode;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.AuthCode.Equals(value))
				{
					_base.Record.AuthCode = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string PanMask
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PanMask;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.PanMask.Equals(value))
				{
					_base.Record.PanMask = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Amount
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Amount;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Amount.Equals(value))
				{
					_base.Record.Amount = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Currency
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Currency;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Currency.Equals(value))
				{
					_base.Record.Currency = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Gateway
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Gateway;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Gateway.Equals(value))
				{
					_base.Record.Gateway = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Description
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Description;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Description.Equals(value))
				{
					_base.Record.Description = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Response
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Response;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Response.Equals(value))
				{
					_base.Record.Response = value;
					PropertyHasChanged();
				}
			}
		}
		
		//LINKED
		public virtual EEstado EStatus { get { return _base.EStatus; } set { Status = (long)value; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
		public virtual ETipoEntidad EEntityType { get { return _base.EEntityType; } set { EntityType = (long)value; } }
		public virtual string EntityTypeLabel { get { return _base.EntityTypeLabel; } }
		public virtual EPaymentGateway EPaymentGateway { get { return _base.EPaymentGateway; } set { Gateway = (long)value; } }
		public virtual string PaymentGatewayLabel { get { return _base.PaymentGatewayLabel; } }
		public virtual ECurrency ECurrency { get { return _base.ECurrency; } set { Currency = (long)value; } }
		public virtual string CurrencyLabel { get { return _base.CurrencyLabel; } }
		public virtual ETransactionType ETransactionType { get { return _base.ETransactionType; } set { Type = (long)value; } }
		public virtual string TransactionTypeLabel { get { return _base.TransactionTypeLabel; } }
		
		#endregion
		
		#region Business Methods
		
		public virtual Transaction CloneAsNew()
		{
			Transaction clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.GetNewCode();
			
			clon.SessionCode = Invoice.Transaction.OpenSession();
			Invoice.Transaction.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}		
	
		protected void CopyFrom(TransactionInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			OidEntity = source.OidEntity;
			EntityType = source.EntityType;
			Serial = source.Serial;
			Code = source.Code;
			Type = source.Type;
			Status = source.Status;
			Created = source.Created;
			Resolved = source.Resolved;
			TransactionID = source.TransactionID;
			AuthCode = source.AuthCode;
			PanMask = source.PanMask;
			Amount = source.Amount;
			Currency = source.Currency;
			Gateway = source.Gateway;
		}		
		
        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(Transaction));
            Code = Serial.ToString(Resources.Defaults.TRANSACTION_CODE_FORMAT);
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
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.TRANSACTION);
        }
        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.TRANSACTION);
        }
        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.TRANSACTION);
        }
        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.TRANSACTION);
        }

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				Transaction = TransactionInfo.New(),
			};
			conditions.Transaction.Oid = oid;
		}
		
		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Transaction()
		{
			_base.Record.Oid = (long)(new Random()).Next();
			Created = DateTime.Now;
			EStatus = EEstado.Active;
			EPaymentGateway = EPaymentGateway.TefPay;
			ECurrency = ECurrency.Euro;
		}
				
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private Transaction(Transaction source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Transaction(int sessionCode, IDataReader source, bool childs)
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
		public static Transaction NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Transaction obj = DataPortal.Create<Transaction>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Transaction con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Transaction source, bool childs)
		/// <remarks/>
		internal static Transaction GetChild(Transaction source) { return new Transaction(source, false); }
		internal static Transaction GetChild(Transaction source, bool childs) { return new Transaction(source, childs); }
        internal static Transaction GetChild(int sessionCode, IDataReader source) { return new Transaction(sessionCode, source, false); }
        internal static Transaction GetChild(int sessionCode, IDataReader source, bool childs) { return new Transaction(sessionCode, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual TransactionInfo GetInfo() { return GetInfo(true); }	
		public virtual TransactionInfo GetInfo(bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new TransactionInfo(this, childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		public static Transaction New(int sessionCode = -1)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Transaction obj = DataPortal.Create<Transaction>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
			return obj;
		}

		public new static Transaction Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return BusinessBaseEx<Transaction>.Get(query, childs, -1);
		}

		public static Transaction Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }

		public static Transaction GetBySubscription(long oidSuscription, ETransactionType transType, EEstado[] status, bool childs, int sessionCode = -1)
		{
			QueryConditions conditions = new QueryConditions
			{
				Oid = oidSuscription,
				EntityType = ETipoEntidad.Subscription,
				Transaction = TransactionInfo.New(),
                Status = status
			};

			conditions.Transaction.ETransactionType = transType;

			return Get(SELECT(conditions), childs, -1);
		}
		public static Transaction GetByTransactionID(string transactionID, ETipoEntidad entityType, ETransactionType transType, EEstado[] status, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				EntityType = entityType,
				Transaction = TransactionInfo.New(),
				Status = status
			};
			conditions.Transaction.TransactionID = transactionID;
			conditions.Transaction.ETransactionType = transType;

			return Get(SELECT(conditions), childs);
		}

		public static Transaction GetLastBySubscription(long oidSuscription, ETransactionType transType, EEstado[] status, 
														bool childs, int sessionCode = -1)
		{
			CriteriaEx criteria = new CriteriaEx();
			criteria.Orders = new OrderList();
			criteria.Orders.NewOrder("Created", ListSortDirection.Descending, typeof(Transaction));

			TransactionList list = TransactionList.GetBySubscription(oidSuscription, transType, status, criteria, childs);

			return (list.Count > 0) ? Get(list[0].Oid, childs, sessionCode) : null;
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
			
			IsPosibleDelete(oid);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Transaction. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Invoice.Transaction.OpenSession();
			ISession sess = Invoice.Transaction.Session(sessCode);
			ITransaction trans = Invoice.Transaction.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from TransactionRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				Invoice.Transaction.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Transaction Save()
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
				
				if (!SharedTransaction) Transaction().Commit();
				return this;
			}
			catch (Exception ex)
			{
				if (!SharedTransaction) if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
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
		
		private void Fetch(Transaction source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);			 

			MarkOld();
		}
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
		internal void Insert(Transactiones parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			GetNewCode();
		
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
		internal void Update(Transactiones parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			TransactionRecord obj = Session().Get<TransactionRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Transactiones parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<TransactionRecord>(Oid));
		
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
					Invoice.Transaction.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);	
				}

				MarkOld();
			}
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
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
                if (SessionCode < 0) SessionCode = OpenSession();
                BeginTransaction();
            }
			//Borrar si no hay código
			GetNewCode();

			Session().Save(Base.Record);
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;

			TransactionRecord obj = Session().Get<TransactionRecord>(Oid);
			obj.CopyValues(Base.Record);
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
				Session().Delete((TransactionRecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
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
			return new Dictionary<String, ForeignField>()
            {                
            };
		}

        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
        internal static string SELECT_FIELDS()
        {
            string query;

            query = @"
				SELECT TR.*";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE (TR.\"CREATED\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";
			
            query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "TR", ForeignFields());
			
            query += @"
				AND (TR.""CREATED"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

 			query += EntityBase.STATUS_LIST_CONDITION(conditions.Status, "TR");
			query += EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "TR");

			if (conditions.Transaction != null)
			{
				if (conditions.Transaction.Oid != 0) 
					query += @"
						AND TR.""OID"" = " + conditions.Transaction.Oid;
				if (conditions.Transaction.TransactionID != string.Empty) 
					query += @"
						AND TR.""TRANSACTIONID"" = '" + conditions.Transaction.TransactionID + "'";
				if (conditions.Transaction.ETransactionType != ETransactionType.All) 
					query += @"
						AND TR.""TYPE"" = " + (long)conditions.Transaction.ETransactionType;
			}

			if (conditions.EntityType != ETipoEntidad.Todos)
			{
				query += @"
					AND TR.""ENTITY_TYPE"" = " + (long)conditions.EntityType;

				if (conditions.Oid != 0)
					query += @"
						AND TR.""OID_ENTITY"" = " + conditions.Oid;
			}

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string tr = nHManager.Instance.GetSQLTable(typeof(TransactionRecord));
            
			string query;

            query = 
				SELECT_FIELDS() + @"
				FROM " + tr + " AS TR" +
				WHERE(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "TR", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += EntityBase.LOCK("TR", lockTable);
			
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

		public static string SELECT_COUNT() { return SELECT_COUNT(new QueryConditions()); }
		public static string SELECT_COUNT(QueryConditions conditions)
		{
			string query;

			query = @"
                SELECT COUNT(*) AS ""TOTAL_ROWS""" +
				SELECT(conditions) +
				WHERE(conditions);

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions { Transaction = TransactionInfo.New(oid) };
			return SELECT(conditions, lockTable);
		}

		#endregion
	}
}
