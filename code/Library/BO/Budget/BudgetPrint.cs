using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx;
using moleQule.Library.Invoice.Resources;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class BudgetPrint : BudgetInfo
    {
        #region Attributes & Properties

        protected string _codigo_cliente = string.Empty;
        protected string _direccion = string.Empty;
        protected string _poblacion = string.Empty;
        protected string _provincia = string.Empty;
        protected string _telefonos = string.Empty;
        protected string _fax = string.Empty;
        protected string _municipio = string.Empty;
        private string _serie = string.Empty;
        private string _nombre_serie = string.Empty;

        public string CodigoCliente { get { return _codigo_cliente; } }
        public string Direccion { get { return _direccion; } }
        public string Poblacion { get { return _poblacion; } }
        public string Provincia { get { return _provincia; } }
        public string Telefonos { get { return _telefonos; } }
        public string Fax { get { return _fax; } }
        public string Municipio { get { return _municipio; } }
        public string NombreSerie { get { return _nombre_serie; } }
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(BudgetInfo source, ClienteInfo cliente, SerieInfo serie)
        {
            if (source == null) return;

            Oid = source.Oid;

			_base.CopyValues(source);

			_base.NumeroSerie = source.NumeroSerie;
			_base.VatNumber = source.VatNumber;
            _base.Cliente = source.NombreCliente;
            _base.IDCliente = source.NumeroCliente;
			_base.CodigoPostal = source.CodigoPostal;

            if (cliente != null)
            {
				_base.VatNumber = cliente.VatNumber;
                _base.Cliente = cliente.Nombre;
                _codigo_cliente = cliente.VatNumber;
                _direccion = cliente.Direccion;
                _poblacion = cliente.Poblacion;
                _provincia = cliente.Provincia;
                _telefonos = cliente.Telefonos;
                _fax = cliente.Fax;
                _municipio = cliente.Municipio;
				_base.CodigoPostal = source.CodigoPostal;
            }

            if (serie != null)
            {
				_base.NumeroSerie = serie.Identificador;
                _nombre_serie = serie.Nombre;
            }			
        }

        #endregion

        #region Factory Methods

        protected BudgetPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static BudgetPrint New(BudgetInfo source, ClienteInfo cliente, SerieInfo serie)
        {
            BudgetPrint item = new BudgetPrint();
            item.CopyValues(source, cliente, serie);

            return item;
        }

        #endregion
    }

    [Serializable()]
    public class ProformaPrint : BudgetPrint
    {
        #region Factory Methods

        private ProformaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public new static ProformaPrint New(BudgetInfo source, ClienteInfo cliente, SerieInfo serie)
        {
            ProformaPrint item = new ProformaPrint();
            item.CopyValues(source, cliente, serie);

            return item;
        }

        #endregion
    }
}
