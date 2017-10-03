using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Child Business Object with ReadOnly Childs
	/// </summary>
	[Serializable()]
	public class OutputInvoiceLineInfo : ReadOnlyBaseEx<OutputInvoiceLineInfo>
	{
		#region Attributes

		protected OutputInvoiceLineBase _base = new OutputInvoiceLineBase();
			
		protected BatchList _partidas = null;
		protected StockList _stocks = null;

        #endregion

        #region Properties

		public OutputInvoiceLineBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidFactura { get { return _base.Record.OidFactura; } }
		public long OidPartida { get { return _base.Record.OidPartida; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidProducto { get { return _base.Record.OidProducto; } }
		public long OidKit { get { return _base.Record.OidKit; } }
		public long OidConceptoAlbaran { get { return _base.Record.OidConceptoAlbaran; } }
        public string Concepto { get { return _base.Record.Concepto; } set { _base.Record.Concepto = value; } }
		public bool FacturacionBulto { get { return _base.Record.FacturacionBulto; } }
		public Decimal CantidadKilos { get { return _base.Record.CantidadKilos; } }
		public Decimal CantidadBultos { get { return _base.Record.CantidadBultos; } }
		public Decimal PImpuestos { get { return _base.Record.PImpuestos; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public Decimal Precio { get { return _base.Record.Precio; } }
		public Decimal Subtotal { get { return _base.Record.Subtotal; } }
		public Decimal Gastos { get { return _base.Record.Gastos; } }
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
		public string CodigoProductoCliente { get { return _base.Record.CodigoProductoCliente; } }

		//NO ENLAZADAS
		public virtual long OidAlmacen { get { return _base._oid_almacen; } set { _base._oid_almacen = value; } }
        public virtual string StoreID { get { return _base.StoreID; } set { _base.StoreID = value; } }
        public virtual string Almacen { get { return _base.Store; } set { _base.Store = value; } }
        public virtual string StoreIDStore { get { return _base.StoreIDStore; } }
		public virtual string Expediente { get { return _base.Expediente; } set { _base.Expediente = value; } }
		public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { _base.Record.FacturacionBulto = !value; } }
		public virtual string CuentaContable { get { return _base._cuenta_contable; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
		public virtual string NFactura { get { return _base._n_factura; } set { _base._n_factura = value; } }
		public virtual DateTime FechaFactura { get { return _base._fecha_factura; } set { _base._fecha_factura = value; } }
		public virtual string Cliente { get { return _base._cliente; } set { _base._cliente = value; } }

        #endregion

        #region Business Methods

        public void CopyFrom(OutputInvoiceLineInfo source) { _base.CopyValues(source); }
        public void CopyFrom(OutputInvoiceLine source) { _base.CopyValues(source); }
		
        public OutputInvoiceLinePrint GetPrintObject() { return OutputInvoiceLinePrint.New(this); }

		#endregion

		#region Factory Methods

		protected OutputInvoiceLineInfo() { /* require use of factory methods */ }
		private OutputInvoiceLineInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
        internal OutputInvoiceLineInfo(OutputInvoiceLine item, bool childs)
		{
            _base.CopyValues(item);			
			
			if (childs)
			{
			}
		}

        public static OutputInvoiceLineInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = OutputInvoiceLine.GetCriteria(OutputInvoiceLine.OpenSession());
            criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = OutputInvoiceLineInfo.SELECT(oid);
						
			OutputInvoiceLineInfo obj = DataPortal.Fetch<OutputInvoiceLineInfo>(criteria);
			OutputInvoiceLine.CloseSession(criteria.SessionCode);
			
            return obj;
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static OutputInvoiceLineInfo Get(int sessionCode, IDataReader reader, bool childs)
		{
            return new OutputInvoiceLineInfo(sessionCode, reader, childs);
		}

		public static OutputInvoiceLineInfo New(long oid = 0) { return new OutputInvoiceLineInfo() { Oid = oid }; }

		#endregion

		#region Data Access

		// called to retrieve data from db
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;
			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					if (reader.Read())
						_base.CopyValues(reader);
					
                    if (Childs)
					{		
                    }
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		//called to copy data from IDataReader
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);

				if (Childs)
				{		
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

        #region SQL

        public static string SELECT(long oid) { return OutputInvoiceLine.SELECT(oid, false); }

        #endregion
    }
}



