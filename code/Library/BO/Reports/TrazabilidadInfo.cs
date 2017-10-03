using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// </summary>
	[Serializable()]
	public class TrazabilidadInfo : ReadOnlyBaseEx<TrazabilidadInfo>
	{
	
		#region Attributes
		
		protected long _oid_ayuda;
        protected long _oid_serie;
        protected long _serie;
        protected string _codigo;
		protected bool _bulto = false;
		protected string _observaciones = string.Empty;
		protected string _nombre = string.Empty;
		protected Decimal _precio_compra;
		protected Decimal _precio_venta;
		protected string _numero_serie;
		protected string _nombre_serie = string.Empty;
        protected string _codigo_aduanero = string.Empty;
		protected Decimal _ayuda_kilo;
        protected long _oid_producto;
        protected long _oid_proveedor = 0;
        protected string _proveedor;
        protected string _n_expediente;
        protected DateTime _fecha_compra;
        protected string _naviera;
        protected string _trans_ori;
        protected string _trans_dest;
        protected string _cliente;
        protected DateTime _fecha_venta;
        protected decimal _kilos_vendidos;
        
        //Ganado
        protected string _id;
        protected string _raza;
        protected string _sexo;
        protected DateTime _fecha_llegada_muelle;
        protected string _codigo_explotacion;

        #endregion

        #region Properties

        public long OidAyuda { get { return _oid_ayuda; } /*set { _oid_ayuda = value; }*/ }
        public long OidSerie { get { return _oid_serie; } /*set { _oid_serie = value; }*/ }
        public long Serial { get { return _serie; } /*set { _oid_serie = value; }*/ }
        public string Codigo { get { return _codigo; } /*set { _oid_serie = value; }*/ }
		public bool Bulto { get { return _bulto; } /*set { _bulto = value; }*/ }
		public string Observaciones { get { return _observaciones; } /*set { _observaciones = value; }*/ }
		public string Nombre { get { return _nombre; } /*set { _nombre = value; }*/ }
		public Decimal PrecioCompra { get { return _precio_compra; } /*set { _precio_compra = value; }*/ }
		public Decimal PrecioVenta { get { return _precio_venta; } /*set { _precio_venta = value; }*/ }
		public string NumeroSerie { get { return _numero_serie; } /*set { _numero_serie = value; }*/ }
		public string NombreSerie { get { return _nombre_serie; } /*set { _nombre_serie = value; }*/ }
        public string CodigoAduanero { get { return _codigo_aduanero; } /*set { _nombre_serie = value; }*/ }
		public Decimal AyudaKilo { get { return _ayuda_kilo; } /*set { _ayuda_kilo = value; }*/ }
        public long OidProducto { get { return _oid_producto; } }
        public long OidProveedor { get { return _oid_proveedor; } }
        public string Proveedor { get { return _proveedor; } }
        public string NExpediente { get { return _n_expediente; } }
        public DateTime FechaCompra { get { return _fecha_compra; } }
        public string Naviera { get { return _naviera; } }
        public string TransporteOri { get { return _trans_ori; } }
        public string TrasporteDes { get { return _trans_dest; } }
        public string Cliente { get { return _cliente; } }
        public DateTime FechaVenta { get { return _fecha_venta; } }
        public decimal KilosVendidos { get { return _kilos_vendidos; } }
        
        // Ganado
        public string ID { get { return _id; } }
        public string Raza { get { return _raza; } }
        public string Sexo { get { return _sexo; } }
        public DateTime FechaLlegadaMuelle { get { return _fecha_llegada_muelle; } }
        public string Destino { get { return _cliente; } }
        public string CodigoExplotacion { get { return _codigo_explotacion; } }

		#endregion
		
		#region Business Methods
		
		/// <summary>
		/// Copia los atributos del objeto
		/// </summary>
		/// <param name="source">Objeto origen</param>
		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

            long tipo = Format.DataReader.GetInt64(source, "TIPO_CONSULTA");

            Oid = Format.DataReader.GetInt64(source, "OID_CONCEPTO");
            _oid_producto = Format.DataReader.GetInt64(source, "OID");
            _oid_proveedor = Format.DataReader.GetInt64(source, "OID_PROVEEDOR");
            _proveedor = Format.DataReader.GetString(source, "PROVEEDOR");
            _oid_ayuda = Format.DataReader.GetInt64(source, "OID_AYUDA");
            //_oid_serie = Format.DataReader.GetInt64(source, "OID_SERIE");
            _serie = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
			_bulto = Format.DataReader.GetBool(source, "BULTO");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_precio_compra = Format.DataReader.GetDecimal(source, "PRECIO_COMPRA");
			_precio_venta = Format.DataReader.GetDecimal(source, "PRECIO_VENTA");
            _numero_serie = Format.DataReader.GetString(source, "ID_SERIE");
			_nombre_serie = Format.DataReader.GetString(source, "SERIE");
            _codigo_aduanero = Format.DataReader.GetString(source, "CODIGO_ADUANERO");
			_ayuda_kilo = Format.DataReader.GetDecimal(source, "AYUDA_KILO");
            _n_expediente = Format.DataReader.GetString(source, "NEXPEDIENTE");
            _fecha_compra = Format.DataReader.GetDateTime(source, "FECHA_COMPRA");
            _naviera = Format.DataReader.GetString(source, "NAVIERA");
            _trans_ori = Format.DataReader.GetString(source, "TRANS_ORIGEN");
            _trans_dest = Format.DataReader.GetString(source, "TRANS_DESTINO");
            _cliente = Format.DataReader.GetString(source, "CLIENTE");
            _fecha_venta = Format.DataReader.GetDateTime(source, "FECHA_VENTA");
            _kilos_vendidos = Format.DataReader.GetDecimal(source, "KILOS_VENDIDOS");

            switch (tipo)
            {
                case 1: //Ganado
                    {
                        _id = Format.DataReader.GetString(source, "ID");
                        _raza = Format.DataReader.GetString(source, "RAZA");
                        _sexo = Format.DataReader.GetString(source, "SEXO");
                        _fecha_llegada_muelle = Format.DataReader.GetDateTime(source, "FECHA_LLEGADA_MUELLE");
                        _codigo_explotacion = Format.DataReader.GetString(source, "CODIGO_EXPLOTACION");
                    }
                    break;
            }
		}
			
		#endregion		
		
		#region Factory Methods

        protected TrazabilidadInfo() { /* require use of factory methods */ }

        public static TrazabilidadInfo Get(IDataReader reader)
        {
            TrazabilidadInfo item = new TrazabilidadInfo();
            item.CopyValues(reader);
            return item;
        }
		
 		#endregion
		
	}
}
