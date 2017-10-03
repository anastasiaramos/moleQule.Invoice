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
	public class BudgetLineRecord : RecordBase
	{
		#region Attributes

		private long _oid_proforma;
		private long _oid_partida;
		private long _oid_expediente;
		private long _oid_producto;
		private string _concepto = string.Empty;
		private bool _facturacion_bulto = false;
		private Decimal _cantidad;
		private Decimal _cantidad_bultos;
		private Decimal _p_impuestos;
		private Decimal _p_descuento;
		private Decimal _total;
		private Decimal _precio;
		private Decimal _subtotal;
		private long _oid_impuesto;

		#endregion

		#region Properties

		public virtual long OidProforma { get { return _oid_proforma; } set { _oid_proforma = value; } }
		public virtual long OidPartida { get { return _oid_partida; } set { _oid_partida = value; } }
		public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
		public virtual long OidProducto { get { return _oid_producto; } set { _oid_producto = value; } }
		public virtual string Concepto { get { return _concepto; } set { _concepto = value; } }
		public virtual bool FacturacionBulto { get { return _facturacion_bulto; } set { _facturacion_bulto = value; } }
		public virtual Decimal CantidadKilos { get { return _cantidad; } set { _cantidad = value; } }
		public virtual Decimal CantidadBultos { get { return _cantidad_bultos; } set { _cantidad_bultos = value; } }
		public virtual Decimal PImpuestos { get { return _p_impuestos; } set { _p_impuestos = value; } }
		public virtual Decimal PDescuento { get { return _p_descuento; } set { _p_descuento = value; } }
		public virtual Decimal Total { get { return _total; } set { _total = value; } }
		public virtual Decimal Precio { get { return _precio; } set { _precio = value; } }
		public virtual Decimal Subtotal { get { return _subtotal; } set { _subtotal = value; } }
		public virtual long OidImpuesto { get { return _oid_impuesto; } set { _oid_impuesto = value; } }

		#endregion

		#region Business Methods

		public BudgetLineRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_proforma = Format.DataReader.GetInt64(source, "OID_PROFORMA");
			_oid_partida = Format.DataReader.GetInt64(source, "OID_BATCH");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			_oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
			_concepto = Format.DataReader.GetString(source, "CONCEPTO");
			_facturacion_bulto = Format.DataReader.GetBool(source, "FACTURACION_BULTO");
			_cantidad = Format.DataReader.GetDecimal(source, "CANTIDAD");
			_cantidad_bultos = Format.DataReader.GetDecimal(source, "CANTIDAD_BULTOS");
			_p_impuestos = Format.DataReader.GetDecimal(source, "P_IMPUESTOS");
			_p_descuento = Format.DataReader.GetDecimal(source, "P_DESCUENTO");
			_total = Format.DataReader.GetDecimal(source, "TOTAL");
			_precio = Format.DataReader.GetDecimal(source, "PRECIO");
			_subtotal = Format.DataReader.GetDecimal(source, "SUBTOTAL");
			_oid_impuesto = Format.DataReader.GetInt64(source, "OID_IMPUESTO");

		}
		public virtual void CopyValues(BudgetLineRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_proforma = source.OidProforma;
			_oid_partida = source.OidPartida;
			_oid_expediente = source.OidExpediente;
			_oid_producto = source.OidProducto;
			_concepto = source.Concepto;
			_facturacion_bulto = source.FacturacionBulto;
			_cantidad = source.CantidadKilos;
			_cantidad_bultos = source.CantidadBultos;
			_p_impuestos = source.PImpuestos;
			_p_descuento = source.PDescuento;
			_total = source.Total;
			_precio = source.Precio;
			_subtotal = source.Subtotal;
			_oid_impuesto = source.OidImpuesto;
		}

		#endregion
	}

	[Serializable()]
	public class BudgetLineBase
	{
		#region Attributes

		private BudgetLineRecord _record = new BudgetLineRecord();

		internal string _expediente = string.Empty;
		internal Decimal _ayuda_kilo;
		internal Decimal _gastos;
		internal string _ubicacion = string.Empty;
		internal string _id_albaran = string.Empty;

		#endregion

		#region Properties

		public BudgetLineRecord Record { get { return _record; } }

		//internal bool IsKitComponent { get { return _record.OidKit > 0; } }
		internal Decimal Descuento { get { return Decimal.Round((_record.Subtotal * _record.PDescuento) / 100, 2); } }
		internal Decimal BaseImponible { get { return _record.Subtotal - Descuento; } }
		internal Decimal Impuestos { get { return Decimal.Round((_record.Subtotal * _record.PImpuestos) / 100, 2); } }
		internal Decimal AyudaKilo { get { return Decimal.Round(_ayuda_kilo, 5); } set { _ayuda_kilo = Decimal.Round(value, 5); } }
		internal Decimal Beneficio { get { return _record.CantidadKilos * BeneficioKilo; } }
		internal Decimal BeneficioKilo
		{
			get
			{
				if (_record.FacturacionBulto)
					return (_record.CantidadKilos > 0) ? (_record.Precio / (_record.CantidadKilos / _record.CantidadBultos)) - _gastos : 0;
				else
					return _record.Precio - _gastos;
			}
		}
		internal bool FacturacionPeso { get { return !_record.FacturacionBulto; } }
		internal ETipoFacturacion ETipoFacturacion { get { return (FacturacionPeso) ? ETipoFacturacion.Peso : ETipoFacturacion.Unidad; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
		}
		internal void CopyValues(BudgetLine source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_expediente = source.Expediente;
		}
		internal void CopyValues(BudgetLineInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_expediente = source.Expediente;
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class BudgetLine : BusinessBaseEx<BudgetLine>
	{	 
		#region Attributes

		protected BudgetLineBase _base = new BudgetLineBase();

		#endregion
		
		#region Properties

		public BudgetLineBase Base { get { return _base; } }

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
		public virtual long OidProforma
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidProforma;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidProforma.Equals(value))
				{
					_base.Record.OidProforma = value;
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
					Subtotal = (_base.Record.FacturacionBulto) ? _base.Record.CantidadBultos * _base.Record.Precio : _base.Record.CantidadKilos * _base.Record.Precio;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal CantidadKilos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CantidadKilos;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.CantidadKilos.Equals(value))
				{
					_base.Record.CantidadKilos = Decimal.Round(value, 2);
					if (!_base.Record.FacturacionBulto) CalculateTotal();
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
					_base.Record.CantidadBultos = Decimal.Round(value, 4);
					if (_base.Record.FacturacionBulto) CalculateTotal();
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
					CalculateTotal();
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
					CalculateTotal();
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
					CalculateTotal();
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
					if (!_base.Record.FacturacionBulto) CalculateTotal();
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
				}
			}
		}

		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } set { PropertyHasChanged(); } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } set { PropertyHasChanged(); } }
		public virtual Decimal Gastos { get { return _base._gastos; } set { _base._gastos = value; } }
		public virtual Decimal Beneficio { get { return _base.Beneficio; } }
		public virtual Decimal BeneficioKilo { get { return _base.BeneficioKilo; } }
		public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { FacturacionBulto = !value; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }

		#endregion
		
		#region Business Methods
		
		/// <summary>
		/// Clona la entidad y sus subentidades y las marca como nuevas
		/// </summary>
		/// <returns>Una entidad clon</returns>
		public virtual BudgetLine CloneAsNew()
		{
			BudgetLine clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.SessionCode = BudgetLine.OpenSession();
			BudgetLine.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}

		/// <summary>
		/// Copia los atributos del objeto
		/// </summary>
		/// <param name="source">Objeto origen de solo lectura</param>
		protected virtual void CopyFrom(BudgetLineInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			OidProforma = source.OidProforma;
			OidProducto = source.OidProducto;
			OidExpediente = source.OidExpediente;
			OidPartida = source.OidPartida;
			OidImpuesto = source.OidImpuesto;
			Concepto = source.Concepto;
			FacturacionBulto = source.FacturacionBulto;
			CantidadKilos = source.CantidadKilos;
            CantidadBultos = source.CantidadBultos;
            PImpuestos = source.PImpuestos;
			PDescuento = source.PDescuento;
			Total = source.Total;
			Precio = source.Precio;
			Subtotal = source.Subtotal;
		}
        public virtual void CopyFrom(BatchInfo item)
        {
            OidPartida = item.OidPartida;
            OidProducto = item.OidProducto;
            Concepto = item.TipoMercancia;
            Gastos = item.CosteKilo;
        }
        public virtual void CopyFrom(ProductInfo parent)
        {
            Expediente = string.Empty;
            OidExpediente = 0;
            OidPartida = 0;
            OidProducto = parent.OidProducto;
            Gastos = parent.PrecioCompra;
            Concepto = parent.Nombre;
            FacturacionBulto = false;

            //PARCHE PARA BALAÑOS. Necesitan que el concepto contenga el valor del campo Descripcion
            //y no el del producto
            Concepto = parent.Descripcion;
        }
        public virtual void CopyFrom(ClienteInfo parent)
        {
            Expediente = "Concepto Libre";
            Concepto = "Transporte";
            CantidadKilos = 1;
            Precio = parent.PrecioTransporte;
            PImpuestos = 5;
        }
		public virtual void CopyFrom(Budget budget, ProductInfo source)
		{
			OidProforma = budget.Oid;
			OidPartida = 0;
			OidProducto = source.Oid;
			OidImpuesto = source.OidImpuestoVenta;
			PImpuestos = source.PImpuestoVenta;
			Gastos = source.PrecioCompra;
			Concepto = source.Nombre;
			FacturacionPeso = (source.ETipoFacturacion == ETipoFacturacion.Peso);

			//PARCHE PARA BALAÑOS. Necesitan que el concepto contenga el valor del campo Descripcion
			//y no el del producto
			Concepto = source.Descripcion;
		}
		public virtual void CopyFrom(Budget budget, BatchInfo batch, ProductInfo product)
		{
			CopyFrom(budget, product);
			CopyFrom(batch);

			CantidadKilos = 1;
			CantidadBultos = 1;

            AjustaCantidad(product, batch);
		}

        public virtual void AjustaCantidad(ProductInfo producto, BatchInfo partida) { AjustaCantidad(producto, partida, 0); }
        public virtual void AjustaCantidad(ProductInfo producto, BatchInfo partida, decimal cantidad)
        {
            if (cantidad != 0)
            {
                if (FacturacionPeso) CantidadKilos = cantidad;
                else CantidadBultos = cantidad;
            }

            if (partida != null)
            {
                if (FacturacionPeso)
                    AjustaCantidadBultos(partida);
                else
                    AjustaCantidadKilos(partida);
            }
            else if (producto != null)
            {
                if (FacturacionPeso)
                    CantidadBultos = (producto.KilosBulto == 0) ? CantidadKilos : CantidadKilos / producto.KilosBulto;
                else
                    CantidadKilos = (producto.KilosBulto == 0) ? CantidadBultos : CantidadBultos * producto.KilosBulto;
            }
            else
            {
                if (FacturacionPeso)
                    CantidadBultos = CantidadKilos;
                else
                    CantidadKilos = CantidadBultos;
            }
        }

        private void AjustaCantidadBultos(BatchInfo partida)
        {
            if (partida.StockKilos == 0) return;

            if (CantidadKilos == partida.StockKilos)
                CantidadBultos = partida.StockBultos;
            else
                CantidadBultos = CantidadKilos / partida.KilosPorBulto;
        }
        private void AjustaCantidadKilos(BatchInfo partida)
        {
            if (partida.StockBultos == 0) return;

            if (CantidadBultos == partida.StockBultos)
                CantidadKilos = partida.StockKilos;
            else
                CantidadKilos = CantidadBultos * partida.KilosPorBulto;
        }

		public virtual void AjustaPrecio()
		{
			if (FacturacionBulto)
				Precio = Precio * CantidadKilos;
			else
				Precio = Precio / CantidadKilos;
		}

		public virtual void CalculateTotal()
		{
			Subtotal = (FacturacionBulto) ? CantidadBultos * Precio : CantidadKilos * Precio;
			Total = BaseImponible + Impuestos;

			//Para forzar el refresco en el formulario
			Impuestos = Impuestos;
			Descuento = Descuento;
		}

		public virtual void Vende(Budget proforma, SerieInfo serie, ClienteInfo cliente, ProductInfo producto) { Vende(proforma, serie, cliente, producto, null); }
		public virtual void Vende(Budget proforma, SerieInfo serie, ClienteInfo cliente, ProductInfo producto, BatchInfo partida)
		{
			if (cliente == null)
				throw new iQException(Library.Invoice.Resources.Messages.NO_CLIENTE_SELECTED);

			if (cliente.Productos == null)
				cliente.LoadChilds(typeof(ProductoCliente), true);

			ProductoClienteInfo productoCliente = cliente.Productos.GetByProducto(producto.Oid);

			if (partida == null)
				CopyFrom(proforma, producto);
			else
				CopyFrom(partida);

			SetTipoFacturacion(productoCliente, producto);
			SetImpuestos(serie, cliente, producto);
			Precio = producto.GetPrecioVenta(productoCliente, partida, ETipoFacturacion);
		}

		public virtual void SetTipoFacturacion(ClienteInfo client, ProductInfo product)
		{
			if (client == null)
				throw new iQException(Library.Invoice.Resources.Messages.NO_CLIENTE_SELECTED);

			if (client.Productos == null)
				client.LoadChilds(typeof(ProductoCliente), true);

			ProductoClienteInfo pci = client.Productos.GetItemByProperty("OidProducto", product.Oid);
			SetTipoFacturacion(pci, product);
		}
		public virtual void SetTipoFacturacion(ProductoClienteInfo pci, ProductInfo product)
		{
			if (pci != null)
				FacturacionBulto = pci.FacturacionBulto;
			else if (product != null)
				FacturacionBulto = !(product.ETipoFacturacion == ETipoFacturacion.Peso);
			else
				FacturacionBulto = false;
		}

		public virtual void SetImpuestos(SerieInfo serie, ClienteInfo client, ProductInfo product)
		{
			//Primero el cliente si está EXENTO
			if ((client != null) && (client.OidImpuesto == 1))
			{
				OidImpuesto = client.OidImpuesto;
				PImpuestos = client.PImpuesto;
			}
			//Luego el producto
			else if ((product != null) && (product.OidImpuestoVenta != 0))
			{
				OidImpuesto = product.OidImpuestoVenta;
				PImpuestos = product.PImpuestoVenta;
			}
			else if ((serie != null) && (serie.OidImpuesto != 0))
			{
				OidImpuesto = serie.OidImpuesto;
				PImpuestos = serie.PImpuesto;
			}
		}

		/// <summary>
		/// Actualiza el precio en base a si se Albaran por kilo o bulto
		/// y si el cliente tiene un precio especial para el producto
		/// </summary>
		public virtual void SetPrecio(ClienteInfo client, ProductInfo product, BatchInfo batch)
		{
			Precio = client.GetPrecio(product, batch, ETipoFacturacion);
			PDescuento = client.GetDescuento(product, batch);
			CalculateTotal();
		}

		#endregion
		 
	    #region Validation Rules

		/// <summary>
		/// Añade las reglas de validación necesarias para el objeto
		/// </summary>
		protected override void AddBusinessRules()
		{
			
			//Código para valores requeridos o que haya que validar
			
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
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		 protected BudgetLine ()
		{
			// Si se necesita constructor público para este objeto hay que añadir el MarkAsChild() debido a la interfaz Child
			// y el código que está en el DataPortal_Create debería ir aquí		
		}		
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private BudgetLine(BudgetLine source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
        private BudgetLine(IDataReader source, bool childs)
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
		public static BudgetLine NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
				  Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<BudgetLine>(new CriteriaCs(-1));
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">ConceptoProforma con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(ConceptoProforma source, bool childs)
		/// <remarks/>
		internal static BudgetLine GetChild(BudgetLine source)
		{
			return new BudgetLine(source, false);
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">ConceptoProforma con los datos para el objeto</param>
		/// <param name="childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
		internal static BudgetLine GetChild(BudgetLine source, bool childs)
		{
			return new BudgetLine(source, childs);
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
        internal static BudgetLine GetChild(IDataReader source)
        {
            return new BudgetLine(source, false);
        }
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">IDataReader con los datos para el objeto</param>
		/// <param name="childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
        internal static BudgetLine GetChild(IDataReader source, bool childs)
        {
            return new BudgetLine(source, childs);
        }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// También copia los datos de los hijos del objeto.
		/// </summary>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual BudgetLineInfo GetInfo()
		{
			return GetInfo(true);
		}
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual BudgetLineInfo GetInfo(bool get_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(
					Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new BudgetLineInfo(this, get_childs);
		}
		
		#endregion				
		
		#region Child Factory Methods
		
		/// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LO UTILIZA LA FUNCION DE CREACION DE LA LISTA DEL PADRE
        /// </summary>
        private BudgetLine(Budget parent)
        {
            OidProforma = parent.Oid;
            MarkAsChild();
        }
		
		/// <summary>
		/// Crea un nuevo objeto hijo
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <returns>Nuevo objeto creado</returns>
		public static BudgetLine NewChild(Budget parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
					Library.Resources.Messages.USER_NOT_ALLOWED);

			return new BudgetLine(parent);
		}
				
		/// <summary>
        /// Borrado aplazado, es posible el undo 
        /// (La función debe ser "no estática")
        /// </summary>
        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(
					Library.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
        }
		
		/// <summary>
		/// No se debe utilizar esta función para guardar. Hace falta el padre, que
		/// debe utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override BudgetLine Save()
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
		private void Fetch(BudgetLine source)
		{
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);
				
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
            }
            catch (Exception ex) { throw ex; }

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(BudgetLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{	
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(
						Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

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
		internal void Update(BudgetLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(
						Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				BudgetLineRecord obj = Session().Get<BudgetLineRecord>(Oid);
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
		internal void DeleteSelf(BudgetLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<BudgetLineRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		
			MarkNew(); 
		}

		#endregion
		
		#region Child Data Access
		
		/// <summary>
		/// Inserta un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Insert(Budget parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidProforma = parent.Oid;				

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
		internal void Update(Budget parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidProforma = parent.Oid;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			BudgetLineRecord obj = parent.Session().Get<BudgetLineRecord>(Oid);
			obj.CopyValues(Base.Record);
			parent.Session().Update(obj);				

			MarkOld();
		}

		/// <summary>
		/// Borra un registro de la base de datos.
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <remarks>Borrado inmediato<remarks/>
		internal void DeleteSelf(Budget parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<BudgetLineRecord>(Oid));

			MarkNew();
		}
		
		#endregion		
		
        #region SQL

		public new static string SELECT(long oid) { return SELECT(oid, false); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = "SELECT CP.*" +
					"       ,COALESCE(EX.\"CODIGO\", '') AS \"EXPEDIENTE\"" +
					"		,PF.\"FECHA\" AS \"FECHA\"";

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query = " WHERE (PF.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			if (conditions.ConceptoProforma != null) query += " AND CP.\"OID\" = " + conditions.ConceptoProforma.Oid;
			if (conditions.Proforma != null) query += " AND CP.\"OID_PROFORMA\" = " + conditions.Proforma.Oid;
			if (conditions.Expediente != null) query += " AND CP.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid;
			if (conditions.Producto != null) query += " AND CP.\"OID_PRODUCTO\" = " + conditions.Producto.Oid;
			if (conditions.Cliente != null) query += " AND PF.\"OID_CLIENTE\" = " + conditions.Cliente.Oid;

			return query;
		}

		internal static string SELECT_BASE(QueryConditions conditions)
		{
			string cp = nHManager.Instance.GetSQLTable(typeof(BudgetLineRecord));
			string pf = nHManager.Instance.GetSQLTable(typeof(BudgetRecord));
			string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));

			string query;

			query = SELECT_FIELDS() +
					" FROM " + cp + " AS CP" +
					" INNER JOIN " + pf + " AS PF ON PF.\"OID\" = CP.\"OID_PROFORMA\"" +
					" LEFT JOIN " + ex + " AS EX ON EX.\"OID\" = CP.\"OID_EXPEDIENTE\"";

			return query;
		}

		internal static string SELECT(long oid, bool lock_table)
		{
			QueryConditions conditions = new QueryConditions { ConceptoProforma = BudgetLine.NewChild().GetInfo(false) };
			conditions.ConceptoAlbaran.Oid = oid;

			return SELECT(conditions, lock_table);
		}

		internal static string SELECT(QueryConditions conditions, bool lock_table)
		{
			string query;

			query = SELECT_BASE(conditions) +
					WHERE(conditions);

			if (lock_table) query += " FOR UPDATE OF CP NOWAIT";

			return query;
		}
	
		#endregion
	}
}

