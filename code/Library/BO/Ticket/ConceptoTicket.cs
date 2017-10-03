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
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class TicketLineRecord : RecordBase
	{
		#region Attributes

		private long _oid_ticket;
		private long _oid_partida;
		private long _oid_expediente;
		private long _oid_producto;
		private long _oid_kit;
		private long _oid_concepto_albaran;
		private long _oid_impuesto;
		private string _codigo_expediente = string.Empty;
		private string _concepto = string.Empty;
		private bool _facturacion_bulto = false;
		private Decimal _cantidad;
		private Decimal _cantidad_bultos;
		private Decimal _p_impuestos;
		private Decimal _p_descuento;
		private Decimal _total;
		private Decimal _precio;
		private Decimal _subtotal;
		private Decimal _gastos;
		#endregion

		#region Properties

		public virtual long OidTicket { get { return _oid_ticket; } set { _oid_ticket = value; } }
		public virtual long OidPartida { get { return _oid_partida; } set { _oid_partida = value; } }
		public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
		public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
		public virtual long OidKit { get { return _oid_kit; } set { _oid_kit = value; } }
		public virtual long OidConceptoAlbaran { get { return _oid_concepto_albaran; } set { _oid_concepto_albaran = value; } }
		public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
		public virtual string CodigoExpediente { get { return _codigo_expediente; } set { _codigo_expediente = value; } }
		public virtual string Concepto { get { return _concepto; } set { _concepto = value; } }
		public virtual bool FacturacionBulto { get { return _facturacion_bulto; } set { _facturacion_bulto = value; } }
		public virtual Decimal Cantidad { get { return _cantidad; } set { _cantidad = value; } }
		public virtual Decimal CantidadBultos { get { return _cantidad_bultos; } set { _cantidad_bultos = value; } }
		public virtual Decimal PImpuestos { get { return _p_impuestos; } set { _p_impuestos = value; } }
		public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
		public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }
		public virtual Decimal Subtotal { get { return _subtotal; } set { _subtotal = value; } }
		public virtual Decimal Gastos { get { return _gastos; } set { _gastos = value; } }

		#endregion

		#region Business Methods

		public TicketLineRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_ticket = Format.DataReader.GetInt64(source, "OID_TICKET");
			_oid_partida = Format.DataReader.GetInt64(source, "OID_BATCH");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
			_oid_kit = Format.DataReader.GetInt64(source, "OID_KIT");
			_oid_concepto_albaran = Format.DataReader.GetInt64(source, "OID_CONCEPTO_ALBARAN");
			_oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
			_codigo_expediente = Format.DataReader.GetString(source, "CODIGO_EXPEDIENTE");
			_concepto = Format.DataReader.GetString(source, "CONCEPTO");
			_facturacion_bulto = Format.DataReader.GetBool(source, "FACTURACION_BULTO");
			_cantidad = Format.DataReader.GetDecimal(source, "CANTIDAD");
			_cantidad_bultos = Format.DataReader.GetDecimal(source, "CANTIDAD_BULTOS");
			_p_impuestos = Format.DataReader.GetDecimal(source, "P_IMPUESTOS");
			_p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
			_total = Format.DataReader.GetDecimal(source, "TOTAL");
			_precio = Format.DataReader.GetDecimal(source, "PRECIO");
			_subtotal = Format.DataReader.GetDecimal(source, "SUBTOTAL");
			_gastos = Format.DataReader.GetDecimal(source, "GASTOS");

		}
		public virtual void CopyValues(TicketLineRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_ticket = source.OidTicket;
			_oid_partida = source.OidPartida;
			_oid_expediente = source.OidExpediente;
			_oid_producto = source.OidProducto;
			_oid_kit = source.OidKit;
			_oid_concepto_albaran = source.OidConceptoAlbaran;
			_oid_impuesto = source.OidImpuesto;
			_codigo_expediente = source.CodigoExpediente;
			_concepto = source.Concepto;
			_facturacion_bulto = source.FacturacionBulto;
			_cantidad = source.Cantidad;
			_cantidad_bultos = source.CantidadBultos;
			_p_impuestos = source.PImpuestos;
			_p_descuento = source.PDescuento;
			_total = source.Total;
			_precio = source.Precio;
			_subtotal = source.Subtotal;
			_gastos = source.Gastos;
		}

		#endregion
	}

	[Serializable()]
	public class TicketLineBase
	{
		#region Attributes

		private TicketLineRecord _record = new TicketLineRecord();

		#endregion

		#region Properties

		public TicketLineRecord Record { get { return _record; } }

		public virtual Decimal BaseImponible { get { return _record.Subtotal - Descuento; } }
		public virtual Decimal Descuento { get { return Decimal.Round((_record.Subtotal * _record.PDescuento) / 100, 2); } }
		public virtual Decimal Impuestos { get { return Decimal.Round((_record.Subtotal * _record.PImpuestos) / 100, 2); } }
		public virtual Decimal Beneficio { get { return _record.Cantidad * BeneficioKilo; } }
		public virtual Decimal BeneficioKilo
		{
			get
			{
				if (_record.FacturacionBulto)
					return (_record.Cantidad > 0) ? (_record.Precio / (_record.Cantidad / _record.CantidadBultos)) - _record.Gastos : 0;
				else
					return _record.Precio - _record.Gastos;
			}
		}
		public virtual bool IsKitComponent { get { return _record.OidKit > 0; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);
		}
		internal void CopyValues(ConceptoTicket source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}
		internal void CopyValues(ConceptoTicketInfo source)
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
    public class ConceptoTicket : BusinessBaseEx<ConceptoTicket>
    {
        #region Attributes

		protected TicketLineBase _base = new TicketLineBase();

		private Batchs _batchs = Batchs.NewChildList();

        #endregion

        #region Properties

		public TicketLineBase Base { get { return _base; } }

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
		public virtual long OidPartida
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidPartida;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidPartida.Equals(value))
				{
					_base.Record.OidPartida = value;
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
		public virtual long OidProducto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidProducto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidProducto.Equals(value))
				{
					_base.Record.OidProducto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidKit
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidKit;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidKit.Equals(value))
				{
					_base.Record.OidKit = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidConceptoAlbaran
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidConceptoAlbaran;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidConceptoAlbaran.Equals(value))
				{
					_base.Record.OidConceptoAlbaran = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidImpuesto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidImpuesto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidImpuesto.Equals(value))
				{
					_base.Record.OidImpuesto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CodigoExpediente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CodigoExpediente;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CodigoExpediente.Equals(value))
				{
					_base.Record.CodigoExpediente = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Concepto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Concepto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Concepto.Equals(value))
				{
					_base.Record.Concepto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool FacturacionBulto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FacturacionBulto;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FacturacionBulto.Equals(value))
				{
					_base.Record.FacturacionBulto = value;
					_base.Record.FacturacionBulto = value;
					Subtotal = (FacturacionBulto) ? CantidadBultos * Precio : CantidadBultos * Precio;
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
					_base.Record.Cantidad = Decimal.Round(value, 2);
					if (!FacturacionBulto)
					{
						if (_base.Record.Cantidad == 0) _base.Record.CantidadBultos = 0;
						CalculaTotal();
					}
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal CantidadBultos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CantidadBultos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.CantidadBultos.Equals(value))
				{
					_base.Record.CantidadBultos = value;
					_base.Record.CantidadBultos = Decimal.Round(value, 4);
					if (FacturacionBulto)
					{
						if (_base.Record.CantidadBultos == 0) _base.Record.Cantidad = 0;
						CalculaTotal();
					}
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal PImpuestos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PImpuestos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PImpuestos.Equals(value))
				{
					_base.Record.PImpuestos = Decimal.Round(value, 2);
					CalculaTotal();
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
					_base.Record.PDescuento = Decimal.Round(value, 2);
					CalculaTotal();
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
					_base.Record.Total = Decimal.Round(value, 2);
					PropertyHasChanged();
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Precio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Precio;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Precio.Equals(value))
				{
					_base.Record.Precio = Decimal.Round(value, 5);
					CalculaTotal();
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
					_base.Record.Subtotal = Decimal.Round(value, 2);
					PropertyHasChanged();
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Gastos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Gastos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Gastos.Equals(value))
				{
					_base.Record.Gastos = value;
					PropertyHasChanged();
				}
			}
		}

		public virtual Batchs Partidas
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _batchs;
            }

            set
            {
                _batchs = value;
            }
        }

        //Campos no enlazados
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
        public virtual Decimal Descuento { get { return _base.Descuento; } }
        public virtual Decimal Impuestos { get { return _base.Impuestos; } }
        public virtual Decimal Beneficio { get { return _base.Beneficio; } }
		public virtual Decimal BeneficioKilo { get { return _base.BeneficioKilo; } }
		public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid && _batchs.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty || _batchs.IsDirty;
            }
        }
       
        #endregion

        #region Business Methods

        public virtual void CopyFrom(OutputDeliveryLineInfo source)
        {
            if (source == null) return;

            OidConceptoAlbaran = source.Oid;
            OidExpediente = source.OidExpediente;
            OidPartida = source.OidPartida;
            OidProducto = source.OidProducto;
            OidKit = source.OidKit;
            OidImpuesto = source.OidImpuesto;
            Concepto = source.Concepto;
            CantidadBultos = source.CantidadBultos;
            Cantidad = source.CantidadKilos;
            PImpuestos = source.PImpuestos;
            PDescuento = source.PDescuento;
            Total = source.Total;
            Precio = source.Precio;
			FacturacionBulto = source.FacturacionBulto;
            Subtotal = source.Subtotal;
            CodigoExpediente = source.Expediente;
            Gastos = source.Gastos;

			CalculaTotal();
        }

        public virtual void CalculaTotal()
        {
			Subtotal = (FacturacionBulto) ? CantidadBultos * Precio : Cantidad * Precio;
			Total = BaseImponible + Impuestos;
        }

        /// <summary>
        /// Actualiza el precio en base a si se factura por kilo o bulto
        /// </summary>
        public virtual void UpdatePrecio(   ProductInfo p, 
                                            BatchInfo pexp, 
                                            ProductoClienteInfo pci)
        {
			if (FacturacionBulto)
            {
				if (pci != null && pci.FacturacionBulto)
                    Precio = pci.Precio;
                else if (pci == null)
                    Precio = p.PrecioVenta * pexp.KilosPorBulto;
				else if (!pci.FacturacionBulto)
                    Precio = p.PrecioVenta;
            }
            else
            {
				if (pci != null && pci.FacturacionBulto)
                    Precio = pci.Precio / pexp.KilosPorBulto;
                else
                    Precio = p.PrecioVenta;
            }
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
            return Ticket.CanAddObject();
        }

        public static bool CanGetObject()
        {
            return Ticket.CanGetObject();
        }

        public static bool CanDeleteObject()
        {
            return Ticket.CanDeleteObject();
        }

        public static bool CanEditObject()
        {
            return Ticket.CanEditObject();
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        public ConceptoTicket()
        {
            MarkAsChild();
            Random r = new Random();
            Oid = (long)r.Next();
            //Rellenar si hay más campos que deban ser inicializados aquí
        }

        private ConceptoTicket(ConceptoTicket source)
        {
            MarkAsChild();
            Fetch(source);
        }

        private ConceptoTicket(int sessionCode, IDataReader reader)
        {
			SessionCode = sessionCode;
            MarkAsChild();
            Fetch(reader);
        }
        
        public virtual ConceptoTicketInfo GetInfo() { return GetInfo(false); }
        public virtual ConceptoTicketInfo GetInfo(bool childs)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new ConceptoTicketInfo(this, childs);
        }

        public static ConceptoTicket NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new ConceptoTicket();
        }

        public static ConceptoTicket NewChild(Ticket parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            ConceptoTicket obj = new ConceptoTicket();
            obj.OidTicket = parent.Oid;

            return obj;
        }

        internal static ConceptoTicket GetChild(ConceptoTicket source)
        {
            return new ConceptoTicket(source);
        }

        internal static ConceptoTicket GetChild(int sessionCode, IDataReader reader)
        {
            return new ConceptoTicket(sessionCode, reader);
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
        public override ConceptoTicket Save()
        {
            throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
        }
        
        #endregion

        #region Child Data Access

        private void Fetch(ConceptoTicket source)
        {
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

                if (Childs)
                {
                    string query;
                    IDataReader reader;

                    query = Batch.SELECT(OidPartida);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _batchs = Batchs.GetChildList(SessionCode, reader);
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
                    string query;
                    IDataReader reader;

                    query = Batch.SELECT(OidPartida);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_batchs = Batchs.GetChildList(SessionCode, reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();

        }

        internal void Insert(Ticket parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            this.OidTicket = parent.Oid;

			ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            parent.Session().Save(Base.Record);

            MarkOld();
        }

        internal void Update(Ticket parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            this.OidTicket = parent.Oid;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            SessionCode = parent.SessionCode;
			TicketLineRecord obj = Session().Get<TicketLineRecord>(Oid);
            obj.CopyValues(Base.Record);
            Session().Update(obj);

            MarkOld();
        }

        internal void DeleteSelf(Ticket parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<TicketLineRecord>(Oid));

            MarkNew();
        }

        #endregion

        #region SQL

        public new static string SELECT(long oid) { return SELECT(oid, true); }
        
		public static string COUNT_BY_EXPEDIENTE(long oid)
        {
            string ct = nHManager.Instance.GetSQLTable(typeof(TicketLineRecord));
            string query;

            query = "SELECT COUNT(*) AS \"CUENTA\" " +
                    " FROM " + ct + " AS CT " +
                    " WHERE CT.\"OID_EXPEDIENTE\" = " + oid;

            return query;
        }

        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT CT.*";

            return query;
        }

        internal static string SELECT(long oid, bool lock_table)
        {
			string cf = nHManager.Instance.GetSQLTable(typeof(TicketLineRecord));

            string query = string.Empty;

            query = SELECT_FIELDS() +
                    " FROM " + cf + " AS CT" +
                    " WHERE TRUE";

            if (oid > 0) query += " AND CF.\"OID\" = " + oid;

            //if (lock_table) query += " FOR UPDATE OF CF NOWAIT";

            return query;
        }

        internal static string SELECT(QueryConditions conditions, bool lock_table)
        {
            string query;

            query = SELECT(0, lock_table);

            if (conditions.Ticket != null) query += " AND CT.\"OID_TICKET\" = " + conditions.Ticket.Oid;
            if (conditions.Expediente != null) query += " AND CT.\"OID_EXPEDIENTE\" = " + conditions.Ticket.Oid;

            //if (lock_table) query += " FOR UPDATE OF PP NOWAIT";

            return query;
        }

        #endregion 
    }
}

