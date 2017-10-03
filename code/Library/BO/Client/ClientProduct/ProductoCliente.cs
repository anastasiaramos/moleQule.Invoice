using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	[Serializable()]
	public class ClientProductRecord : RecordBase
	{
		#region Attributes

		private long _oid_producto;
		private long _oid_cliente;
		private Decimal _precio;
		private bool _facturacion_bulto = false;
		private Decimal _p_descuento;
		private long _tipo_descuento;
		private Decimal _precio_compra;
		private bool _facturar = false;
		private DateTime _fecha_validez;
		#endregion

		#region Properties

		public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
		public virtual long OidCliente { get { return _oid_cliente; } set { _oid_cliente = value; } }
		public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }
		public virtual bool FacturacionBulto { get { return _facturacion_bulto; } set { _facturacion_bulto = value; } }
		public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
		public virtual long TipoDescuento { get { return _tipo_descuento; } set { _tipo_descuento = value; } }
		public virtual Decimal PrecioCompra { get { return _precio_compra; } set { _precio_compra = value; } }
		public virtual bool Facturar { get { return _facturar; } set { _facturar = value; } }
		public virtual DateTime FechaValidez { get { return _fecha_validez; } set { _fecha_validez = value; } }

		#endregion

		#region Business Methods

		public ClientProductRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
			_oid_cliente = Format.DataReader.GetInt64(source, "OID_CLIENTE");
			_precio = Format.DataReader.GetDecimal(source, "PRECIO");
			_facturacion_bulto = Format.DataReader.GetBool(source, "FACTURACION_BULTO");
			_p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
			_tipo_descuento = Format.DataReader.GetInt64(source, "TIPO_DESCUENTO");
			_precio_compra = Format.DataReader.GetDecimal(source, "PRECIO_COMPRA");
			_facturar = Format.DataReader.GetBool(source, "FACTURAR");
			_fecha_validez = Format.DataReader.GetDateTime(source, "FECHA_VALIDEZ");

		}
		public virtual void CopyValues(ClientProductRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_producto = source.OidProducto;
			_oid_cliente = source.OidCliente;
			_precio = source.Precio;
			_facturacion_bulto = source.FacturacionBulto;
			_p_descuento = source.PDescuento;
			_tipo_descuento = source.TipoDescuento;
			_precio_compra = source.PrecioCompra;
			_facturar = source.Facturar;
			_fecha_validez = source.FechaValidez;
		}

		#endregion
	}

	[Serializable()]
	public class ClientProductBase
	{
		#region Attributes

		private ClientProductRecord _record = new ClientProductRecord();

		internal string _producto = string.Empty;
		internal long _oid_familia;
		internal long _oid_impuesto;
		internal decimal _p_impuesto;

		#endregion

		#region Properties

		public ClientProductRecord Record { get { return _record; } }

		public Common.ETipoDescuento ETipoDescuento { get { return (Common.ETipoDescuento)_record.TipoDescuento; } }
		public string TipoDescuentoLabel { get { return Library.Common.EnumText<Common.ETipoDescuento>.GetLabel(ETipoDescuento); } }
		public bool FacturacionPeso { get { return !_record.FacturacionBulto; } }
		public ETipoFacturacion ETipoFacturacion { get { return (FacturacionPeso) ? ETipoFacturacion.Peso : ETipoFacturacion.Unidad; } }
		public string Producto { get { return _producto; } set { _producto = value; } }
		public long OidFamilia { get { return _oid_familia; } set { _oid_familia = value; } }
		public long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }
		public decimal PImpuesto { get { return _p_impuesto; } set { _p_impuesto = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_producto = Format.DataReader.GetString(source, "PRODUCTO");
			_oid_familia = Format.DataReader.GetInt64(source, "OID_FAMILIA");
			_oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");
			_p_impuesto = Format.DataReader.GetDecimal(source, "P_IMPUESTO");
		}
		internal void CopyValues(ProductoCliente source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_producto = source.Producto;
			_oid_familia = source.OidFamilia;
			_oid_impuesto = source.OidImpuesto;
			_p_impuesto = source.PImpuesto;
		}
		internal void CopyValues(ProductoClienteInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_producto = source.Producto;
			_oid_familia = source.OidFamilia;
			_oid_impuesto = source.OidImpuesto;
			_p_impuesto = source.PImpuesto;
		}

		internal static Decimal GetPrecioCliente(ProductoClienteInfo productoCliente, BatchInfo partida, ProductInfo producto, ETipoFacturacion tipo)
		{
			Decimal precio = 0;
			ETipoFacturacion tipoFacturacion = tipo;

			if (productoCliente != null)
			{
				precio = (productoCliente.ETipoDescuento == Common.ETipoDescuento.Precio) ? productoCliente.Precio : producto.PrecioVenta;
				tipoFacturacion = productoCliente.ETipoFacturacion;
			}
			else
			{
				precio = producto.PrecioVenta;
				tipoFacturacion = producto.ETipoFacturacion;
			}

			Decimal kilosBulto = (partida != null) ? partida.KilosPorBulto : producto.KilosBulto;
			kilosBulto = (kilosBulto == 0) ? 1 : kilosBulto;

			switch (tipo)
			{
				case ETipoFacturacion.Peso:

					if (tipoFacturacion != ETipoFacturacion.Peso)
						precio = precio * kilosBulto;

					break;

				default:

					if (tipoFacturacion == ETipoFacturacion.Peso)
						precio = precio / kilosBulto;

					break;
			}

			return Decimal.Round(precio, ModulePrincipal.GetNDecimalesPreciosSetting());
		}

		internal static Decimal GetDescuentoCliente(ProductoClienteInfo productoCliente, Decimal pDescuento)
		{
			Decimal p_descuento = pDescuento;

			if (productoCliente != null)
				p_descuento = (productoCliente.ETipoDescuento == ETipoDescuento.Porcentaje) ? productoCliente.PDescuento : pDescuento;

			return Decimal.Round(p_descuento, 2);
		}

		#endregion
	}
	
	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
    public class ProductoCliente : BusinessBaseEx<ProductoCliente>
	{	
	    #region Attributes

		protected ClientProductBase _base = new ClientProductBase();

        #endregion

        #region Properties

		public ClientProductBase Base { get { return _base; } }

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
		public virtual Decimal Precio
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return Decimal.Round(_base.Record.Precio, 5);
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Precio.Equals(value))
				{
					_base.Record.Precio = value;
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
				return Decimal.Round(_base.Record.PDescuento, 2);
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
		public virtual long TipoDescuento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoDescuento;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.TipoDescuento.Equals(value))
				{
					_base.Record.TipoDescuento = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal PrecioCompra
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return Decimal.Round(_base.Record.PrecioCompra, 5);
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.PrecioCompra.Equals(value))
				{
					_base.Record.PrecioCompra = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Facturar
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Facturar;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Facturar.Equals(value))
				{
					_base.Record.Facturar = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime FechaValidez
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaValidez;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaValidez.Equals(value))
				{
					_base.Record.FechaValidez = value;
					PropertyHasChanged();
				}
			}
		}

		public virtual Common.ETipoDescuento ETipoDescuento { get { return _base.ETipoDescuento; } set { TipoDescuento = (long)value; } }
		public virtual string TipoDescuentoLabel { get { return _base.TipoDescuentoLabel; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { FacturacionBulto = !value; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
		public virtual string Producto { get { return _base.Producto; } set { _base.Producto = value; PropertyHasChanged(); } }
		public virtual long OidFamilia { get { return _base.OidFamilia; } set { _base.OidFamilia = value; } }
		public virtual long OidImpuesto { get { return _base.OidImpuesto; } set { _base.OidImpuesto = value; } }
		public virtual decimal PImpuesto { get { return _base.PImpuesto; } set { _base.PImpuesto = value; } }

        #endregion

        #region Business Methods

		protected void CopyFrom(Cliente cliente, ProductInfo producto)
		{
			OidCliente = cliente.Oid;
			PDescuento = cliente.PDescuento;

			if (producto != null)
			{
				OidProducto = producto.Oid;
				Producto = producto.Nombre;
			}			
		}

		public static Decimal GetPrecioCliente(ProductoCliente productoCliente, ProductInfo producto, BatchInfo partida, ETipoFacturacion tipo) 
		{ 
			return ClientProductBase.GetPrecioCliente(productoCliente.GetInfo(), partida, producto, tipo); 
		}
		public static Decimal GetDescuentoCliente(ProductoCliente productoCliente, Decimal pDescuento)
		{
			return ClientProductBase.GetDescuentoCliente(productoCliente.GetInfo(), pDescuento); 
		}

		#endregion
		 
	    #region Validation Rules

		#endregion
		 
		#region Authorization Rules
		 
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.CLIENTE);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.CLIENTE);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.CLIENTE);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.CLIENTE);
		}
		 
		#endregion

		#region Common Factory Methods

		public virtual ProductoClienteInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return new ProductoClienteInfo(this);
		}

		#endregion

		#region Root Factory Methods

		public static ProductoCliente Get(long oid)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = ProductoCliente.GetCriteria(ProductoCliente.OpenSession());

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = ProductoCliente.SELECT(oid);

			ProductoCliente.BeginTransaction(criteria.Session);
			return DataPortal.Fetch<ProductoCliente>(criteria);
		}

		#endregion

		#region Child Factory Methods

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public ProductoCliente() 
		{ 
			MarkAsChild();
			_base.Record.Oid = (long)(new Random()).Next();
			ETipoDescuento = Store.ModulePrincipal.GetDefaultTipoDescuentoSetting();
		}			
		private ProductoCliente(ProductoCliente source)
		{
			MarkAsChild();
			Fetch(source);
		}		
		private ProductoCliente(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			MarkAsChild();
			Fetch(reader);
		}
		
		public static ProductoCliente NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new ProductoCliente();
		}		
		public static ProductoCliente NewChild(Cliente parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			ProductoCliente obj = new ProductoCliente();
			obj.OidCliente = parent.Oid;
			
			return obj;
		}		
		public static ProductoCliente NewChild(Product parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			ProductoCliente obj = new ProductoCliente();
			obj.OidProducto = parent.Oid;
            obj.Producto = parent.Nombre;

            return obj;
		}
		public static ProductoCliente NewChild(Cliente cliente, ProductInfo producto)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			ProductoCliente obj = new ProductoCliente();
			obj.CopyFrom(cliente, producto);

			return obj;
		}
        public static ProductoCliente NewChild(ProductInfo parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            ProductoCliente obj = new ProductoCliente();
            obj.OidProducto = parent.Oid;

            return obj;
        }
		
		internal static ProductoCliente GetChild(ProductoCliente source) { return new ProductoCliente(source); }		
		internal static ProductoCliente GetChild(int sessionCode, IDataReader reader) { return new ProductoCliente(sessionCode, reader); }
		
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
		
		public override ProductoCliente Save()
		{
			throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}
	
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(ProductoCliente source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}
		
		internal void Insert(Cliente parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
                OidCliente = parent.Oid;
                parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void Update(Cliente parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
                OidCliente = parent.Oid;

				SessionCode = parent.SessionCode;
				ClientProductRecord obj = Session().Get<ClientProductRecord>(Oid);
			    obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Cliente parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<ClientProductRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}
		
		internal void Insert(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
            
            OidProducto = parent.Oid;

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

		internal void Update(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidProducto = parent.Oid;
			
			try
			{
				SessionCode = parent.SessionCode;
				ClientProductRecord obj = Session().Get<ClientProductRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
			
			MarkOld();
		}

		internal void DeleteSelf(Product parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<ClientProductRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}		
		
		#endregion

        #region SQL

        public new static string SELECT(long oid) { return SELECT(oid, new QueryConditions(), true); }

        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT PC.*" +
                    "       ,PR.\"NOMBRE\" AS \"PRODUCTO\"" +
					"       ,PR.\"OID_FAMILIA\" AS \"OID_FAMILIA\"" +
					"		,IM.\"OID\" AS \"OID_IMPUESTO\"" +
					"		,IM.\"PORCENTAJE\" AS \"P_IMPUESTO\"";

            return query;
        }

        internal static string SELECT(long oid, QueryConditions conditions, bool lock_table)
        {
            string pc = nHManager.Instance.GetSQLTable(typeof(ClientProductRecord));
            string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
			string im = nHManager.Instance.GetSQLTable(typeof(TaxRecord));

            string query;

            query = SELECT_FIELDS() +
                    " FROM " + pc + " AS PC" +
                    " LEFT JOIN " + pr + " AS PR ON PR.\"OID\" = PC.\"OID_PRODUCTO\"" +
					" LEFT JOIN " + im + " AS IM ON IM.\"OID\" = PR.\"OID_IMPUESTO_VENTA\"" +
                    " WHERE TRUE";

            if (oid > 0) query += " AND PC.\"OID\" = " + oid.ToString();

            if (conditions.Cliente != null) query += " AND PC.\"OID_CLIENTE\" = " + conditions.Cliente.Oid.ToString();
            if (conditions.Producto != null) query += " AND PC.\"OID_PRODUCTO\" = " + conditions.Producto.Oid.ToString();

            if (lock_table) query += " FOR UPDATE OF PC NOWAIT";

            return query;
        }

        internal static string SELECT(QueryConditions conditions, bool lock_table) { return SELECT(0, conditions, lock_table); }

        #endregion	
	}
}

