using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// ReadOnly Root Business Object with ReadOnly Childs
    /// </summary>
    [Serializable()]
    public class InventarioValoradoInfo : ReadOnlyBaseEx<InventarioValoradoInfo>
    {
        #region Attributes

        //Producto
        protected long _oid_producto;
        protected string _codigo_producto = string.Empty;
        protected string _producto = string.Empty;
        protected decimal _coste;
        protected decimal _pvp;
        protected decimal _gasto;

        //Expediente
        protected string _contenedor = string.Empty;
        protected string _expediente = string.Empty;
        protected long _tipo_expediente;
        
        //Stock
        protected decimal _entrada;
        protected decimal _salida;
        protected decimal _stock;

        //Proveedor
        protected string _proveedor = string.Empty;

        #endregion

        #region Propiedades

        public long OidProducto { get { return _oid_producto; } }
        public string CodigoProducto { get { return _codigo_producto; } }
        public string Producto { get { return _producto; } }
        public string Proveedor { get { return _proveedor; } }
        public Decimal PVD { get { return _coste; } }
        public Decimal PVP { get { return _pvp; } }
        public Decimal Gasto { get { return _gasto; } }
        public string Contenedor { get { return _contenedor; } }
        public string Expediente { get { return _expediente; } }
        public long TipoExpediente { get { return _tipo_expediente; } }
        public Decimal Entrada { get { return _entrada; } }
        public Decimal Salida { get { return _salida; } }
        public Decimal Stock { get { return _stock; } }

        #endregion

        #region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(InventarioValoradoInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_producto = source.OidProducto;
            _codigo_producto = source.CodigoProducto;
            _producto = source.Producto;
            _proveedor = source.Proveedor;
            _coste = source.PVD;
            _pvp = source.PVP;
            _gasto = source.Gasto;
            _contenedor = source.Contenedor;
            _expediente = source.Expediente;
            _tipo_expediente = source.TipoExpediente;
            _entrada = source.Entrada;
            _salida = source.Salida;
            _stock = source.Stock;
        }

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID_PRODUCTO_PROVEEDOR");
            _oid_producto = Format.DataReader.GetInt64(source, "OID_PRODUCTO");
            _codigo_producto = Format.DataReader.GetString(source, "CODIGO_PRODUCTO");
            _producto = Format.DataReader.GetString(source, "PRODUCTO");
            _proveedor = Format.DataReader.GetString(source, "PROVEEDOR");
            _contenedor = Format.DataReader.GetString(source, "CONTENEDOR");
            _expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
            _tipo_expediente = Format.DataReader.GetInt64(source, "TIPO_EXPEDIENTE");
            _entrada = Format.DataReader.GetDecimal(source, "ENTRADA");
            _salida = Format.DataReader.GetDecimal(source, "SALIDA");
            _stock = _entrada + _salida;
            _gasto = Format.DataReader.GetDecimal(source, "COSTE_KILO");
            _coste = Format.DataReader.GetDecimal(source, "PCD");
            _pvp = Format.DataReader.GetDecimal(source, "PVP");

            _gasto = _gasto * _stock;
            _coste = _coste * _stock;
            _pvp = _pvp * _stock;
        }

        #endregion
        
        #region Factory Methods

        protected InventarioValoradoInfo() { /* require use of factory methods */ }

        public static InventarioValoradoInfo Get(IDataReader reader)
        {
            InventarioValoradoInfo item = new InventarioValoradoInfo();
            item.CopyValues(reader);
            return item;
        }

        #endregion
    }
}



