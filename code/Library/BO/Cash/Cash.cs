using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

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
	public class CashRecord : RecordBase
	{
		#region Attributes

		private string _codigo = string.Empty;
		private string _nombre = string.Empty;
		private string _observaciones = string.Empty;
		private string _cuenta_contable = string.Empty;

		#endregion

		#region Properties

		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual string CuentaContable { get { return _cuenta_contable; } set { _cuenta_contable = value; } }

		#endregion

		#region Business Methods

		public CashRecord() {}

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
		}
		public virtual void CopyValues(CashRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_codigo = source.Codigo;
			_nombre = source.Nombre;
			_observaciones = source.Observaciones;
			_cuenta_contable = source.CuentaContable;
		}

		#endregion
	}

	[Serializable()]
	public class CashBase
	{
		#region Attributes

		private CashRecord _record = new CashRecord();

		private decimal _debe_acumulado = 0;
		private decimal _haber_acumulado = 0;
		private decimal _debe_total = 0;
		private decimal _haber_total = 0;

		#endregion

		#region Properties

		public CashRecord Record { get { return _record; } }

		public virtual decimal DebeAcumulado { get { return _debe_acumulado; } set { _debe_acumulado = value; } }
		public virtual decimal HaberAcumulado { get { return _haber_acumulado; } set { _haber_acumulado = value; } }
		public virtual decimal SaldoAcumulado { get { return _debe_acumulado - _haber_acumulado; } }
		public virtual decimal DebeTotal { get { return _debe_total; } set { _debe_total = value; } }
		public virtual decimal HaberTotal { get { return _haber_total; } set { _haber_total = value; } }
		public virtual decimal SaldoTotal { get { return _debe_total - _haber_total; } }
		public virtual decimal DebeParcial { get { return _debe_total - _debe_acumulado; } }
		public virtual decimal HaberParcial { get { return _haber_total - _haber_acumulado; } }
		public virtual decimal SaldoParcial { get { return SaldoTotal - SaldoAcumulado; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_debe_acumulado = Format.DataReader.GetDecimal(source, "DEBE_ACUMULADO");
			_haber_acumulado = Format.DataReader.GetDecimal(source, "HABER_ACUMULADO");

			decimal debe_parcial = Format.DataReader.GetDecimal(source, "DEBE_ACTUAL");
			decimal haber_parcial = Format.DataReader.GetDecimal(source, "HABER_ACTUAL");

			_debe_total = _debe_acumulado + debe_parcial;
			_haber_total = _haber_acumulado + debe_parcial;
		}
		internal void CopyValues(Cash source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_debe_acumulado = source.DebeAcumulado;
			_haber_acumulado = source.HaberAcumulado;
			_debe_total = source.DebeTotal;
			_haber_total = source.HaberTotal;
		}
		internal void CopyValues(CashInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_debe_acumulado = source.DebeAcumulado;
			_haber_acumulado = source.HaberAcumulado;
			_debe_total = source.DebeTotal;
			_haber_total = source.HaberTotal;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>	
    [Serializable()]
	public class Cash : BusinessBaseEx<Cash>
	{	 
		#region Attributes

		protected CashBase _base = new CashBase();

        private CashLines _lines = CashLines.NewChildList();

        #endregion

        #region Properties

		public CashBase Base { get { return _base; } }

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
		public virtual string Nombre
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Nombre;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Nombre.Equals(value))
				{
					_base.Record.Nombre = value;
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
		public virtual string CuentaContable
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CuentaContable;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CuentaContable.Equals(value))
				{
					_base.Record.CuentaContable = value;
					PropertyHasChanged();
				}
			}
		}
		
		public virtual CashLines Lines
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _lines;
			}
		}

        //NO ENLAZADAS
		public virtual decimal DebeAcumulado { get { return _base.DebeAcumulado; } set { _base.DebeAcumulado = value; } }
        public virtual decimal HaberAcumulado { get { return _base.HaberAcumulado; } set { _base.HaberAcumulado = value; } }
		public virtual decimal SaldoAcumulado { get { return _base.SaldoAcumulado; } }
		public virtual decimal DebeTotal { get { return _base.DebeTotal; } set { _base.DebeTotal = value; } }
		public virtual decimal HaberTotal { get { return _base.HaberTotal; } set { _base.HaberTotal = value; } }
        public virtual decimal SaldoTotal { get { return _base.SaldoTotal; } }
        public virtual decimal DebeParcial { get { return _base.DebeParcial; } }
        public virtual decimal HaberParcial { get { return _base.HaberParcial; } }
        public virtual decimal SaldoParcial { get { return _base.SaldoParcial; } }
		
		public override bool IsValid
		{
			get { return base.IsValid
						 && _lines.IsValid ; }
		}
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _lines.IsDirty ; }
		}
		
		#endregion
		
		#region Business Methods
		
		public virtual Cash CloneAsNew()
		{
			Cash clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.Codigo = (0).ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
			
			clon.SessionCode = Cash.OpenSession();
			Cash.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.Lines.MarkAsNew();
			
			return clon;
		}

		protected virtual void CopyFrom(CashInfo source)
		{
			if (source == null) return;

			_base.CopyValues(source);

            DebeAcumulado = source.DebeAcumulado;
            HaberAcumulado = source.HaberAcumulado;
            DebeTotal = source.DebeTotal;
            HaberTotal = source.HaberTotal;
		}

		public virtual void UpdateSaldo() { UpdateSaldo(DateTime.Now); }
        public virtual void UpdateSaldo(DateTime date)
        {
            DebeTotal = DebeAcumulado;
            HaberTotal = HaberAcumulado;

            if (Lines.Count == 0) return;

			int i = 0;
			for (i = 0; i < Lines.Count; i++)
			{
				CashLine item = Lines[i];

				if (item.Fecha > date) continue;

				if (item.EEstado == EEstado.Anulado)
				{
					item.Saldo = 0;
					continue;
				}

				item.Saldo = SaldoAcumulado + item.Debe - item.Haber;
				DebeTotal += item.Debe;
				HaberTotal += item.Haber;
				break;
			}

			int last_abierto = i;

            for (int j=i + 1 ; j < Lines.Count; j++)
            {
				CashLine item = Lines[j];

				if (item.Fecha > date) 
					continue;

				if (item.EEstado == EEstado.Anulado)
				{
					item.Saldo = 0;
					continue;
				}

				item.Saldo = Lines[last_abierto].Saldo + item.Debe - item.Haber;
				DebeTotal += item.Debe;
				HaberTotal += item.Haber;

				last_abierto = j;
            }            
        }

        public virtual void ReindexarLineas()
        {
            if (_lines.Count == 0) return;

			for (int year = 2012; year <= DateTime.Today.Year; year++)
			{
				long index = SerialLineaCajaInfo.GetNext(Oid, year);

				foreach (CashLine item in _lines)
				{
					if (item.Fecha.Year == year)
					{
						item.Serial = index++;
						item.Codigo = item.Serial.ToString(moleQule.Library.Invoice.Resources.Defaults.LINEACAJA_CODE_FORMAT);
					}
				}
			}
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
		protected Cash () {}		
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private Cash(Cash source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
        private Cash(IDataReader source, bool childs)
        {
            MarkAsChild();	
			Childs = childs;
            Fetch(source);
        }

		public static Cash NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Cash>(new CriteriaCs(-1));
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Caja con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Caja source, bool childs)
		/// <remarks/>
		internal static Cash GetChild(Cash source) { return new Cash(source, false); }
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Caja con los datos para el objeto</param>
		/// <param name="childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
		internal static Cash GetChild(Cash source, bool childs)
		{
			return new Cash(source, childs);
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="reader">DataReader con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(IDataReader source, bool childs)
		/// <remarks/>
        internal static Cash GetChild(IDataReader source) { return new Cash(source, false); }
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">IDataReader con los datos para el objeto</param>
		/// <param name="childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
        internal static Cash GetChild(IDataReader source, bool childs) { return new Cash(source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// También copia los datos de los hijos del objeto.
		/// </summary>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual CashInfo GetInfo() { return GetInfo(true); }
		public virtual CashInfo GetInfo(bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new CashInfo(this, childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static Cash New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Cash>(new CriteriaCs(-1));
		}
		
		public static Cash Get(long oid, bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Cash.GetCriteria(Cash.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Cash.SELECT(oid);

			Cash.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Cash>(criteria);
		}
        public static Cash Get(long oid, int sessionCode) { return Get(oid, true, sessionCode); }
		
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
		/// Elimina todos los Caja. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Cash.OpenSession();
			ISession sess = Cash.Session(sessCode);
			ITransaction trans = Cash.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from CashRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				Cash.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
	    public override Cash Save()
        {
            // Por la posible doble interfaz Root/Child
            if (IsChild)
                throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

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
#if TRACE
				ControlerBase.AppControler.Timer.Record("Caja.Save");
#endif
                UpdateSaldo();
                _lines.Update(this);
#if TRACE
				ControlerBase.AppControler.Timer.Record("LineaCajas.Update");
#endif
                if (!SharedTransaction) Transaction().Commit();
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
				if (!SharedTransaction)
                {
                    if (CloseSessions && (this.IsNew || Transaction().WasCommitted)) CloseSession();
                    else BeginTransaction();
				}
            }
        }

        public override Cash SaveAsChild()
        {
            if (IsDeleted && !CanDeleteObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
            else if (IsNew && !CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
            else if (!CanEditObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                base.SaveAsChild();

                UpdateSaldo();
                _lines.Update(this);

                return this;
            }
            catch (Exception ex)
            {
                //if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally { }
        }

		public static void InsertItem(IBankLine source, int sessionCode) { InsertItem(source, 1, sessionCode); }		
		public static void InsertItem(IBankLine source, long oid_caja, int sessionCode)
        {
            if (source is Payment && ((Payment)source).EEstadoPago != EEstado.Pagado) return;
            else if (source is Charge && ((Charge)source).EEstadoCobro != EEstado.Charged) return;

            switch (source.EMedioPago)
            {
                //Creamos la entrada de caja correspondiente
                case EMedioPago.Efectivo:
                    {
						Cash caja = Cash.Get(oid_caja, sessionCode);
                        CashLine lc = caja.Lines.NewItem(caja);
                        lc.CopyFrom(source);
                        if (source.ETipoMovimientoBanco == EBankLineType.PagoPrestamo)
                        {
                            Payment pago = (Payment)source;
                            lc.Haber = pago.Importe + pago.GastosBancarios;
                        }

						caja.UpdateSaldo();
                        caja.SaveAsChild();
                        //caja.CloseSession();
                    }
                    break;
            }
        }

		public static void EditItem(IBankLine source, IBankLineInfo oldSource, int sessionCode) { EditItem(source, oldSource, 1, sessionCode); }
		public static void EditItem(IBankLine source, IBankLineInfo oldSource, long oid_caja, int sessionCode)
		{
			if (source.EEstado == EEstado.Anulado
                || (source is Payment && (source as Payment).EEstadoPago != EEstado.Pagado)
                || (source is Charge && (source as Charge).EEstadoCobro != EEstado.Charged))
			{
				//Anulamos la salida de caja correspondiente
				AnulaItem(source, oid_caja, sessionCode);
				return;
			}

			switch (source.EMedioPago)
			{
				case EMedioPago.Efectivo:
					{
						Cash caja = Cash.Get(oid_caja, sessionCode);
						CashLine lc = null;

						switch (source.ETipoMovimientoBanco)
						{
							case EBankLineType.PagoFactura:
							case EBankLineType.PagoGasto:
                            case EBankLineType.PagoNomina:
                            case EBankLineType.Prestamo:
                            case EBankLineType.PagoPrestamo:
								
								lc = caja.Lines.GetItemByPayment(source.Oid);
								break;

							case EBankLineType.Cobro:
								
								lc = caja.Lines.GetItemByCharge(source.Oid);
                                break;
						}

                        if (lc == null)
                        {
                            CashLineInfo info = null;

                            switch (source.ETipoMovimientoBanco)
                            {
                                case EBankLineType.PagoFactura:
                                case EBankLineType.PagoGasto:
                                case EBankLineType.PagoNomina:
                                case EBankLineType.Prestamo:

                                    info = CashLineInfo.GetByPago(source.Oid);
                                    break;

                                case EBankLineType.Cobro:

                                    info = CashLineInfo.GetByCobro(source.Oid);
                                    break;
                            }

                            if (info != null && info.OidCierre != 0)
                                throw new iQInfoException(Resources.Messages.LINEA_INCLUIDA_CIERRE, string.Empty, iQExceptionCode.WARNING);

                            if ((source is Payment
                                    && (oldSource as PaymentInfo).EEstadoPago == EEstado.Pendiente
                                    && (source as Payment).EEstadoPago == EEstado.Pagado)
                                || (source is Charge
                                    && (oldSource as ChargeInfo).EEstadoCobro == EEstado.Pendiente
                                    && (source as Charge).EEstadoCobro == EEstado.Charged))
                                InsertItem(source, sessionCode);
                            return;
                        }

						lc.CopyFrom(source);
                        if (source.ETipoMovimientoBanco == EBankLineType.PagoPrestamo)
                        {
                            Payment pago = (Payment)source;
                            lc.Haber = pago.Importe + pago.GastosBancarios;
                        }
						caja.UpdateSaldo();
                        caja.SaveAsChild();
					}
					break;
			}
		}

		public static void AnulaItem(IBankLine source, int sessionCode) { AnulaItem(source, 1, sessionCode); }
		public static void AnulaItem(IBankLine source, long oid_caja, int sessionCode)
		{
			switch (source.EMedioPago)
			{
				//Anulamos la salida de caja correspondiente
				case EMedioPago.Efectivo:
					{
						Cash caja = Cash.Get(oid_caja, sessionCode);
						CashLine lc = null;

						switch (source.ETipoMovimientoBanco)
						{
							case EBankLineType.PagoFactura:
							case EBankLineType.PagoGasto:
                            case EBankLineType.PagoNomina:
                            case EBankLineType.Prestamo:

								lc = caja.Lines.GetItemByPayment(source.Oid);
								break;

							case EBankLineType.Cobro:

								lc = caja.Lines.GetItemByCharge(source.Oid);
                                break;
						}

                        if (lc != null)
                        {
                            lc.EEstado = EEstado.Anulado;
                            caja.UpdateSaldo();
                            caja.SaveAsChild();
                            //caja.CloseSession();
                        }
                        else
                        {
                            CashLineInfo info = null;

                            switch (source.ETipoMovimientoBanco)
                            {
                                case EBankLineType.PagoFactura:
                                case EBankLineType.PagoGasto:
                                case EBankLineType.PagoNomina:
                                case EBankLineType.Prestamo:

                                    info = CashLineInfo.GetByPago(source.Oid);
                                    break;

                                case EBankLineType.Cobro:

                                    info = CashLineInfo.GetByCobro(source.Oid);
                                    break;
                            }

                            if (info != null && info.OidCierre != 0)
                                throw new iQInfoException(Resources.Messages.LINEA_INCLUIDA_CIERRE);

                        }
					}
					break;
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
		private void Fetch(Cash source)
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
                        string query = CashLines.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _lines = CashLines.GetChildList(reader, false);						
                    }
                }
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

                if (Childs)
                {
					if (nHMng.UseDirectSQL)
                    {                        
						CashLine.DoLOCK(Session());
                        string query = CashLines.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _lines = CashLines.GetChildList(reader, false);
                    }
                }
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
		internal void Insert(Cashes parent)
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
		internal void Update(Cashes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				CashRecord obj = Session().Get<CashRecord>(Oid);
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
		internal void DeleteSelf(Cashes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<CashRecord>(Oid));
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
					Cash.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
#if TRACE
					ControlerBase.AppControler.Timer.Record("Caja.CopyValues");
#endif
					if (Childs)
					{
						string query = string.Empty;
						
						CashLine.DoLOCK(Session());
						query = CashLines.SELECT_BY_CAJA(Oid);
						reader = nHMng.SQLNativeSelect(query, Session());
#if TRACE
						ControlerBase.AppControler.Timer.Record("LineaCajas.SQLNativeSelect");
#endif
						_lines = CashLines.GetChildList(reader);
#if TRACE
						ControlerBase.AppControler.Timer.Record("LineaCajas.GetChildList");
#endif
					}

					UpdateSaldo();
#if TRACE
					ControlerBase.AppControler.Timer.Record("UpdateSaldo");
#endif
				}
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
			try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
				//si hay codigo o serial, hay que obtenerlos aquí por si ha habido
				//inserciones de otros usuarios en la tabla
				
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
					CashRecord obj = Session().Get<CashRecord>(Oid);
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
				Session().Delete((CashRecord)(criterio.UniqueResult()));
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

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>() { };
		}

		public new static string SELECT(long oid) { return SELECT(oid, true); }

        internal static string SELECT_FIELDS()
        {
            string query;

			query = "SELECT C.*" +
					"       ,COALESCE(CC.\"DEBE_ACUMULADO\", 0) AS \"DEBE_ACUMULADO\"" +
					"       ,COALESCE(CC.\"HABER_ACUMULADO\", 0) AS \"HABER_ACUMULADO\"" +
					"       ,COALESCE(LC.\"DEBE_ACTUAL\", 0) AS \"DEBE_ACTUAL\"" +
					"       ,COALESCE(LC.\"HABER_ACTUAL\", 0) AS \"HABER_ACTUAL\"";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "C", ForeignFields());

			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "C");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "C");

			if (conditions.Caja != null)
				query += @"
					AND C.""OID"" = " + conditions.Caja.Oid;

			return query + " " + conditions.ExtraWhere;
		}

        internal static string JOIN(QueryConditions conditions)
        {
            string c = nHManager.Instance.GetSQLTable(typeof(CashRecord));
			string cc = nHManager.Instance.GetSQLTable(typeof(CashCountRecord));
			string lc = nHManager.Instance.GetSQLTable(typeof(CashLineRecord));
            
            string query;

			query = @"
				FROM " + c + @" AS C
				LEFT JOIN (SELECT CC.""OID_CAJA"", SUM(CC.""DEBE"") AS ""DEBE_ACUMULADO"", SUM(CC.""HABER"") AS ""HABER_ACUMULADO""
				           FROM " + cc + @" AS CC
							WHERE CC.""FECHA"" < '" + conditions.FechaFinLabel + @"' 
							GROUP BY CC.""OID_CAJA"")
				       AS CC ON CC.""OID_CAJA"" = C.""OID""
				LEFT JOIN (SELECT LC.""OID_CAJA"", SUM(LC.""DEBE"") AS ""DEBE_ACTUAL"", SUM(LC.""HABER"") AS ""HABER_ACTUAL""
				           FROM " + lc + @" AS LC
							WHERE LC.""OID_CIERRE"" = 0
							AND LC.""FECHA"" < '" + conditions.FechaFinLabel + @"'
							GROUP BY LC.""OID_CAJA"")
				       AS LC ON LC.""OID_CAJA"" = C.""OID""";

			return query + " " + conditions.ExtraJoin;
        }

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query =
				SELECT_FIELDS() +
				JOIN(conditions) +
				WHERE(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "C", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			//query += Common.EntityBase.LOCK("C", lockTable);

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			return SELECT(new QueryConditions { Caja = CashInfo.New(oid) }, lockTable);
		}     

        #endregion
	}
}

