using System;
using System.Collections;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class OutputDeliveryPrint : OutputDeliveryInfo
    {              
        #region Business Methods

		protected string _direccion = string.Empty;
		protected string _poblacion = string.Empty;
		protected string _provincia = string.Empty;
		protected string _telefonos = string.Empty;
		protected string _fax = string.Empty;
		protected string _municipio = string.Empty;
        private string _nombre_transportista = string.Empty;
		private string _codigo_postal = string.Empty;

        public string Direccion { get { return _direccion; } }
        public string Poblacion { get { return _poblacion; } }
        public string Provincia { get { return _provincia; } }
		public string CodigoPostal { get { return _codigo_postal; } }
        public string Telefonos { get { return _telefonos; } }
        public string Fax { get { return _fax; } }
        public string Municipio { get { return _municipio; } }
        public string NombreTransportista { get { return _nombre_transportista; } }

        protected void CopyValues(OutputDeliveryInfo delivery, ClienteInfo client, TransporterInfo transporter)
        {
            if (delivery == null) return;

			Oid = delivery.Oid;
			_base.CopyValues(delivery);

            if (client != null)
            {
                _base._id_cliente = client.Codigo;
				_base._cliente = client.Nombre;
                _direccion = client.Direccion;
                _poblacion = client.Poblacion;
                _provincia = client.Provincia;
                _telefonos = client.Telefonos;
                _fax = client.Fax;
                _municipio = client.Municipio;
				_codigo_postal = client.CodigoPostal;
            }

            if (transporter != null)
            {
                _nombre_transportista = transporter.Nombre;
            }
        }        
        protected void CopyValues(OutputDeliveryInfo delivery, ExpedientInfo expedient)
        {
            if (delivery == null) return;

            Oid = delivery.Oid;
            _base.CopyValues(delivery);

            if (expedient != null)
            {
                _base.ExpedientID = expedient.Codigo;
            }
        }

        #endregion

        #region Factory Methods

        protected OutputDeliveryPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static OutputDeliveryPrint New(OutputDeliveryInfo delivery, ClienteInfo client, TransporterInfo transporter)
        {
            OutputDeliveryPrint item = new OutputDeliveryPrint();
            item.CopyValues(delivery, client, transporter);

            return item;
        }

        // called to load data from source
        public static OutputDeliveryPrint New(OutputDeliveryInfo delivery, ExpedientInfo expedient)
        {
            OutputDeliveryPrint item = new OutputDeliveryPrint();
            item.CopyValues(delivery, expedient);

            return item;
        }

        #endregion
    }

    /* DEPRECATED */
    [Serializable()]
    public class AlbaranPrint : OutputDeliveryPrint
    {
       #region Factory Methods

        private AlbaranPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static AlbaranPrint New(OutputDeliveryInfo Albaran, ClienteInfo client, TransporterInfo transporter)
        {
            AlbaranPrint item = new AlbaranPrint();
            item.CopyValues(Albaran, client, transporter);

            return item;
        }

        #endregion
    }
}
