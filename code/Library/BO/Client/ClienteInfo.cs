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
using moleQule.Library.Hipatia;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class ClienteInfo : ReadOnlyBaseEx<ClienteInfo, Cliente>, IAgenteHipatia, ITitular, IEntidadRegistroInfo
	{
        #region ITitular

        public virtual ETipoTitular ETipoTitular { get { return ETipoTitular.Proveedor; } }

        #endregion

		#region IEntidadRegistroInfo

		public ETipoEntidad ETipoEntidad { get { return ETipoEntidad.Cliente; } }
		public string DescripcionRegistro { get { return "CLIENTE Nº " + Codigo; } }

		#endregion

        #region IAgenteHipatia

        public string NombreHipatia { get { return Nombre; } }
        public string IDHipatia { get { return Codigo; } }
		public Type TipoEntidad { get { return typeof(Cliente); } }
        public string ObservacionesHipatia { get { return Observaciones; } }

        #endregion

		#region Attributes

		protected ClientBase _base = new ClientBase();
		
        protected ChargeList _cobros = null;
		protected ClientProductList _producto_clientes = null;
        protected LineaRegistroList _emails = null;

        #endregion

        #region Properties

		public ClientBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidExt { get { return _base.Record.OidExt; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Estado { get { return _base.Record.Estado; } }
		public long TipoId { get { return _base.Record.TipoId; } }
		public string VatNumber { get { return _base.Record.VatNumber; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public string NombreComercial { get { return _base.Record.NombreComercial; } }
		public string Titular { get { return _base.Record.Titular; } }
		public string Direccion { get { return _base.Record.Direccion; } }
		public string Poblacion { get { return _base.Record.Poblacion; } }
		public string CodigoPostal { get { return _base.Record.CodigoPostal; } }
		public string Provincia { get { return _base.Record.Provincia; } }
		public string Telefonos { get { return _base.Record.Telefonos; } }
		public string Fax { get { return _base.Record.Fax; } }
		public string Prefix { get { return _base.Record.Prefix; } }
		public string Movil { get { return _base.Record.Movil; } }
		public string Municipio { get { return _base.Record.Municipio; } }
		public string Country { get { return _base.Record.Country; } }
		public string Email { get { return _base.Record.Email; } }
		public DateTime BirthDate { get { return _base.Record.BirthDate; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public string Historia { get { return _base.Record.Historia; } }
		public Decimal LimiteCredito { get { return _base.Record.LimiteCredito; } }
		public string Contacto { get { return _base.Record.Contacto; } }
		public long MedioPago { get { return _base.Record.MedioPago; } }
		public long FormaPago { get { return _base.Record.FormaPago; } }
		public long DiasPago { get { return _base.Record.DiasPago; } }
		public string CodigoExplotacion { get { return _base.Record.CodigoExplotacion; } }
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } }
		public string Swift { get { return _base.Record.Swift; } }
		public long OidCuentaBAsociada { get { return _base.Record.OidCuentaBancariaAsociada; } }
		public Decimal PDescuento { get { return _base.Record.Descuento; } }
		public Decimal PrecioTransporte { get { return _base.Record.PrecioTransporte; } }
		public long OidTransporte { get { return _base.Record.OidTransporte; } }
		public string CuentaContable { get { return _base.Record.CuentaContable; } }
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
		public Decimal TipoInteres { get { return _base.Record.TipoInteres; } }
		public Decimal PDescuentoPtoPago { get { return _base.Record.PDescuentoPtoPago; } }
		public long PrioridadPrecio { get { return _base.Record.PrioridadPrecio; } }
		public bool EnviarFacturaPendiente { get { return _base.Record.EnviarFacturaPendiente; } }

        public virtual ChargeList Cobros { get { return _cobros; } set { _cobros = value; } }
        public virtual ClientProductList Productos { get { return _producto_clientes; } set { _producto_clientes = value; } }
        public virtual LineaRegistroList Emails { get { return _emails; } set { _emails = value; } }

		//NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EStatus; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual long OidUser { get { return _base.OidUser; } set { _base.OidUser = value; } }
		public virtual string Username { get { return _base.Username; } }
		public virtual EEstadoItem EUserStatus { get { return _base.EUserStatus; } set { _base.EUserStatus = value; } }
		public virtual string UserStatusLabel { get { return _base.UserStatusLabel; } }
		public virtual DateTime CreationDate { get { return _base.CreationDate; } set { _base.CreationDate = value; } }
		public virtual DateTime LastLoginDate { get { return _base.LastLoginDate; } set { _base.LastLoginDate = value; } }
		public virtual EPrioridadPrecio EPrioridadPrecio { get { return _base.EPrioridadPrecio; } set { _base.EPrioridadPrecio = value; } }
		public virtual string PrioridadPrecioLabel { get { return _base.PrioridadPrecioLabel; } }
		public virtual EMedioPago EMedioPago { get { return _base.EMedioPago; } }
		public virtual string MedioPagoLabel { get { return _base.MedioPagoLabel; } }
		public virtual EFormaPago EFormaPago { get { return _base.EFormaPago; }  }
		public virtual string FormaPagoLabel { get { return _base.FormaPagoLabel; } }
		public virtual Decimal TotalFacturado { get { return _base._total_facturado; } set { _base._total_facturado = value; } }
		public virtual Decimal CreditoDispuesto { get { return _base._credito_dispuesto; } set { _base._credito_dispuesto = value; } }
		public virtual string CuentaAsociada { get { return _base._cuenta_asociada; } }
		public virtual string Impuesto { get { return _base.Impuesto; } set { _base.Impuesto = value; } }
		public virtual Decimal PImpuesto { get { return _base._p_impuesto; } set { _base._p_impuesto = value; } }
		public virtual string NumeroClienteLabel { get { return _base.NumeroClienteLabel; } }
		public virtual string Photo { get { return _base.GetCrypFileName(Oid, EFile.Photo); } }
		public virtual string Passport { get { return _base.GetCrypFileName(Oid, EFile.Passport); } }
		public virtual string DrivingLicense { get { return _base.GetCrypFileName(Oid, EFile.DrivingLicense); } }
		
		#endregion
		
		#region Business Methods
		
        public void CopyFrom(Cliente source) { _base.CopyValues(source); }

        public ClientePrint GetPrintObject() { return ClientePrint.New(this); }

        public ChargeSummary GetResumen() { return ChargeSummary.GetByCliente(this); }
        public CarteraClientesPrint GetCarteraPrintObject(OutputInvoiceInfo factura)
        {
            return CarteraClientesPrint.New(this, factura);
        }

		public Decimal GetPrecio(ProductInfo producto, BatchInfo partida, ETipoFacturacion tipo)
		{
			if (Productos == null) LoadChilds(typeof(ProductoCliente), false);

			long oid_producto = (producto != null) ? producto.Oid : partida.OidProducto;

			producto = (producto != null) ? producto : ProductInfo.Get(oid_producto, false, true);
			ProductoClienteInfo producto_cliente = Productos.GetByProducto(oid_producto);
			
			Decimal precio = producto.GetPrecioVenta(producto_cliente, partida, tipo);

			return precio;
		}
		public Decimal GetDescuento(ProductInfo producto, BatchInfo partida)
		{
			if (Productos == null) LoadChilds(typeof(ProductoCliente), false);

			long oid_producto = (producto != null) ? producto.Oid : partida.Oid;
			ProductoClienteInfo producto_cliente = Productos.GetByProducto(oid_producto);

			return ProductoClienteInfo.GetDescuentoCliente(producto_cliente, PDescuento);
		}

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected ClienteInfo() { /* require use of factory methods */ }
		private ClienteInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal ClienteInfo(Cliente item, bool childs)
		{
			_base.CopyValues(item);

			if (childs)
			{
				_cobros = (item.Cobros != null) ? ChargeList.GetChildList(item.Cobros) : null;
                _producto_clientes = (item.Productos != null) ? ClientProductList.GetChildList(item.Productos) : null;
			}
		}

		public static ClienteInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static ClienteInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new ClienteInfo(sessionCode, reader, childs); }

		public static ClienteInfo New(long oid = 0) { return new ClienteInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		public static ClienteInfo Get(long oid, bool childs = false)
		{
			if (!Cliente.CanGetObject()) throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			return Get(Cliente.SELECT(oid, false), childs);
		}
        public static ClienteInfo Get(long oid, bool childs, bool cache)
        {
            ClienteInfo item;

            //No está en la cache de listas
            if (!Cache.Instance.Contains(typeof(ClienteList)))
            {
				ClienteList items = ClienteList.NewList();

				item = ClienteInfo.Get(oid, childs);
				items.AddItem(item);
				Cache.Instance.Save(typeof(ClienteList), items);
            }
            else
            {
                ClienteList items = Cache.Instance.Get(typeof(ClienteList)) as ClienteList;
                item = items.GetItem(oid);

                //No está en la lista de la cache de listas
                if (item == null)
                {
                    item = ClienteInfo.Get(oid, childs);
                    items.AddItem(item);
                    Cache.Instance.Save(typeof(ClienteList), items);
                }
            }

            return item;
        }

		public static ClienteInfo GetUser(long oid, bool childs = false)
		{
			if (!Cliente.CanGetObject()) throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			QueryConditions conditions = new QueryConditions()
			{
				Cliente = ClienteInfo.New(oid),
			};
			
			return Get(Cliente.SELECT_USERS(conditions, false), childs);
		}

        public void LoadChilds(Type type, bool getChilds)
        {
            if (type.Equals(typeof(ProductoCliente)))
            {
                _producto_clientes = ClientProductList.GetChildList(this, getChilds);
            }
            else if (type.Equals(typeof(Charge)))
                _cobros = ChargeList.GetChildList(this, getChilds);
            if (type.Equals(typeof(LineaRegistro)))
                _emails = LineaRegistroList.GetRegistroEmailsByCliente(this.Oid, getChilds);
        }

		#endregion
		
		#region Root Data Access
		 
		private void DataPortal_Fetch(CriteriaEx criteria)
		{		
			try
			{
				_base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);
					
                    if (Childs)
					{
						string query = string.Empty;
	                    
						query = ChargeList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _cobros = ChargeList.GetChildList(SessionCode, reader);
						
						query = ClientProductList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _producto_clientes = ClientProductList.GetChildList(reader);
                    }
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
			}
		}
		
		#endregion
		
		#region Child Data Access
		
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;
					
					query = ChargeList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
					_cobros = ChargeList.GetChildList(SessionCode, reader);
					
					query = ClientProductList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _producto_clientes = ClientProductList.GetChildList(reader);
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion
	}
}
