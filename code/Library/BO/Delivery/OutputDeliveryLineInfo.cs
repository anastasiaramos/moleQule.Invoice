using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Child Business Object with ReadOnly Childs
	/// </summary>
	[Serializable()]
    public class OutputDeliveryLineInfo : ReadOnlyBaseEx<OutputDeliveryLineInfo>
	{
		#region Attributes

		protected OutputDeliveryLineBase _base = new OutputDeliveryLineBase();
			
		protected BatchList _partidas = null;
		protected StockList _stocks = null;

        #endregion

        #region Properties

		public OutputDeliveryLineBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidAlbaran { get { return _base.Record.OidAlbaran; } }
		public long OidPartida { get { return _base.Record.OidPartida; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidProducto { get { return _base.Record.OidProducto; } }
		public long OidKit { get { return _base.Record.OidKit; } }
		public string Concepto { get { return _base.Record.Concepto; } }
		public bool FacturacionBulto { get { return _base.Record.FacturacionBulto; } }
		public Decimal CantidadKilos { get { return _base.Record.CantidadKilos; } }
		public Decimal CantidadBultos { get { return _base.Record.CantidadBultos; } }
        public Decimal Precio { get { return _base.Record.Precio; } }
        public Decimal Subtotal { get { return _base.Record.Subtotal; } }
        public Decimal PImpuestos { get { return _base.Record.PImpuestos; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public Decimal Gastos { get { return _base.Record.Gastos; } }
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
		public long OidLineaPedido { get { return _base.Record.OidLineaPedido; } }
		public long OidAlmacen { get { return _base.Record.OidAlmacen; } }
		public string CodigoProductoCliente { get { return _base.Record.CodigoProductoCliente; } }

        public BatchList Partidas { get { return _partidas; } set { _partidas = value; } }
        public StockList Stocks { get { return _stocks; } set { _stocks = value; } }

		public virtual string IDAlmacen { get { return _base.StoreID; } set { _base.StoreID = value; } }
		public virtual string Almacen { get { return _base.Store; } set { _base.Store = value; } }
		public virtual string IDAlmacenAlmacen { get { return _base.StoreIDStore; } }
		public virtual string Expediente { get { return _base.Expediente; } set { _base.Expediente = value; } }
		public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } }
		public virtual Decimal AyudaKilo { get { return _base.AyudaKilo; } }
		public virtual Decimal Beneficio { get { return _base.Beneficio; } }
        public virtual Decimal BeneficioKilo { get { return _base.BeneficioKilo; } }
        public virtual Decimal PBeneficio { get { return _base.PBeneficio; } }
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } }
		public virtual string Ubicacion { get { return _base.Ubicacion; } }
		public virtual long OidStock { get { return _base.OidStock; } }
		public virtual long OidPedido { get { return _base.OidPedido; } }
		public virtual string IDAlbaran { get { return _base.IDAlbaran; } }
		public virtual DateTime Fecha { get { return _base.Fecha; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }
		public virtual long HolderType { get { return _base.HolderType; } set { _base.HolderType = value; } }
		public virtual ETipoEntidad EHolderType { get { return _base.EHolderType; } set { _base.EHolderType = value; } }
		public virtual string HolderTypeLabel { get { return _base.HolderTypeLabel; } }

        #endregion

        #region Business Methods

		public void CopyFrom(OutputDeliveryLine source) { _base.CopyValues(source); }
		public void CopyFrom(OutputDeliveryLineInfo source) { _base.CopyValues(source); }

		#endregion

		#region Common Factory Methods

		protected OutputDeliveryLineInfo() { /* require use of factory methods */ }
		private OutputDeliveryLineInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal OutputDeliveryLineInfo(OutputDeliveryLine source, bool childs)
		{
            _base.CopyValues(source);		
			
			if (childs)
			{
                if (source.Partidas != null)
                    _partidas = BatchList.GetChildList(source.Partidas);
                
                if (source.Stocks != null)
                    _stocks = StockList.GetChildList(source.Stocks);				
			}
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static OutputDeliveryLineInfo Get(int sessionCode, IDataReader reader, bool childs)
		{
			return new OutputDeliveryLineInfo(sessionCode, reader, childs);
		}

		public static OutputDeliveryLineInfo New(long oid = 0) { return new OutputDeliveryLineInfo() { Oid = oid }; }

		#endregion

		#region Root Factory Methods

		/// <summary>
		/// Devuelve un ClienteInfo tras consultar la base de datos
		/// </summary>
		/// <param name="oid"></param>
		/// <returns></returns>
		public static OutputDeliveryLineInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = OutputDeliveryLine.GetCriteria(OutputDeliveryLine.OpenSession());
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = OutputDeliveryLine.SELECT(oid);
			else
				criteria.AddOidSearch(oid);
				
			criteria.Childs = childs;
			OutputDeliveryLineInfo obj = DataPortal.Fetch<OutputDeliveryLineInfo>(criteria);
			OutputDeliveryLine.CloseSession(criteria.SessionCode);

			return obj;
		}

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
						string query = string.Empty;
						
                        query = BatchInfo.SELECT(OidPartida);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_partidas = BatchList.GetChildList(SessionCode, reader, false);

                        query = StockList.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _stocks = StockList.GetChildList(reader);
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
			string query = string.Empty;

			try
			{
				_base.CopyValues(source);

				if (Childs)
				{
					IDataReader reader;

                    query = BatchInfo.SELECT(OidPartida);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_partidas = BatchList.GetChildList(SessionCode, reader, false);
					
					query = StockList.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_stocks = StockList.GetChildList(reader);
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex, new object[] { query });
			}
		}

		#endregion

        #region SQL

        public static string SELECT(long oid) { return OutputDeliveryLine.SELECT(oid, false); }

        #endregion
	}
}



