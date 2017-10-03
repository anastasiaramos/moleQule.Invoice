using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Child Business Object with ReadOnly Childs
	/// </summary>
	[Serializable()]
	public class ConceptoTicketInfo : ReadOnlyBaseEx<ConceptoTicketInfo>
	{
		#region Attributes

		protected TicketLineBase _base = new TicketLineBase();
			
		protected BatchList _producto_expedientes = null;
		protected StockList _stocks = null;

        #endregion

        #region Properties

		public TicketLineBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidTicket { get { return _base.Record.OidTicket; } }
		public long OidPartida { get { return _base.Record.OidPartida; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidProducto { get { return _base.Record.OidProducto; } }
		public long OidKit { get { return _base.Record.OidKit; } }
		public long OidConceptoAlbaran { get { return _base.Record.OidConceptoAlbaran; } }
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } }
		public string CodigoExpediente { get { return _base.Record.CodigoExpediente; } }
		public string Concepto { get { return _base.Record.Concepto; } }
		public bool FacturacionBulto { get { return _base.Record.FacturacionBulto; } }
		public Decimal Cantidad { get { return _base.Record.Cantidad; } }
		public Decimal CantidadBultos { get { return _base.Record.CantidadBultos; } }
		public Decimal PImpuestos { get { return _base.Record.PImpuestos; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public Decimal Precio { get { return _base.Record.Precio; } }
		public Decimal Subtotal { get { return _base.Record.Subtotal; } }
		public Decimal Gastos { get { return _base.Record.Gastos; } }

        public virtual BatchList Partidas { get { return _producto_expedientes; } set { _producto_expedientes = value; } }
        //public virtual StockList Stocks { get { return _stocks; } set { _stocks = value; } }

        //Campos no enlazados
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } }
		public virtual Decimal Beneficio { get { return _base.Beneficio; } }
		public virtual Decimal BeneficioKilo { get { return _base.BeneficioKilo; } }
		public virtual bool IsKitComponent { get { return _base.IsKitComponent; } }

        #endregion

        #region Business Methods

        public ConceptoTicketPrint GetPrintObject()
        {
            return ConceptoTicketPrint.New(this);
        }

		#endregion

		#region Factory Methods

		protected ConceptoTicketInfo() { /* require use of factory methods */ }

		private ConceptoTicketInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}

        internal ConceptoTicketInfo(ConceptoTicket item, bool childs)
		{
            _base.CopyValues(item);			
			
			if (childs)
			{
				if (item.Partidas != null)
					_producto_expedientes = BatchList.GetChildList(item.Partidas);
				/*if (item.Stocks != null)
					_stocks = StockList.GetChildList(item.Stocks);*/				
			}
		}

        public static ConceptoTicketInfo Get(long oid) { return Get(oid, false); }
        public static ConceptoTicketInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = ConceptoTicket.GetCriteria(ConceptoTicket.OpenSession());
            criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = ConceptoTicketInfo.SELECT(oid);
						
			ConceptoTicketInfo obj = DataPortal.Fetch<ConceptoTicketInfo>(criteria);
			ConceptoTicket.CloseSession(criteria.SessionCode);
			
            return obj;
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static ConceptoTicketInfo Get(int sessionCode, IDataReader reader, bool childs)
		{
            return new ConceptoTicketInfo(sessionCode, reader, childs);
		}

		#endregion

		#region Data Access

		// called to retrieve data from db
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
						
                        query = BatchInfo.SELECT(OidPartida);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
						_producto_expedientes = BatchList.GetChildList(SessionCode, reader, false);
						
						/*query = StockList.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _stocks = StockList.GetChildList(reader);*/						
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
					string query = string.Empty;
					IDataReader reader;

                    query = BatchInfo.SELECT(OidPartida);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
					_producto_expedientes = BatchList.GetChildList(SessionCode, reader, false);
					
					/*query = StockList.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _stocks = StockList.GetChildList(reader);	*/			
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

        #region SQL

        public static string SELECT(long oid) { return ConceptoTicket.SELECT(oid, false); }

        #endregion
    }
}



