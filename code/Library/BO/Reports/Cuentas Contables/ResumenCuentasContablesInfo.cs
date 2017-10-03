using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using moleQule.Library.CslaEx; 

namespace moleQule.Library.Invoice
{

    [Serializable()]
    public class ResumenCuentasContablesInfo : ReadOnlyBaseEx<ResumenCuentasContablesInfo>
    {
        #region Attributes

        private string _entidad = string.Empty;
        private string _codigo = string.Empty;
        private string _nombre = string.Empty;
        private string _cuenta_contable = string.Empty;

        #endregion

        #region Properties

        public string Entidad { get { return _entidad; } }
        public string Codigo { get { return _codigo; } }
        public string Nombre { get { return _nombre; } }
        public string CuentaContable { get { return _cuenta_contable; } }

        #endregion

        #region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _entidad = Format.DataReader.GetString(source, "ENTIDAD");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");            

        }

        #endregion
        
        #region Factory Methods

        protected ResumenCuentasContablesInfo() { /* require use of factory methods */ }

        public static ResumenCuentasContablesInfo Get(IDataReader reader)
        {
            ResumenCuentasContablesInfo item = new ResumenCuentasContablesInfo();
            item.CopyValues(reader);
            return item;
        }

        #endregion

    }
}
