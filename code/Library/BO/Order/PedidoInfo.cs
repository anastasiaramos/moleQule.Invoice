using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class PedidoInfo : ReadOnlyBaseEx<PedidoInfo>
	{	
		#region Attributes

		protected OrderBase _base = new OrderBase();

		protected LineaPedidoList _lineas_pedido = null;

		#endregion
		
		#region Properties

		public OrderBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidUsuario { get { return _base.Record.OidUsuario; } }
		public long OidCliente { get { return _base.Record.OidCliente; } }
		public long OidProducto { get { return _base.Record.OidProducto; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public long OidSerie { get { return _base.Record.OidSerie; } }
		public Decimal BaseImponible { get { return _base.Record.BaseImponible; } }
		public Decimal Impuestos { get { return _base.Record.Impuestos; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public Decimal Descuento { get { return _base.Record.Descuento; } }
		public long OidAlmacen { get { return _base.Record.OidAlmacen; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		
		public LineaPedidoList Lineas { get { return _lineas_pedido; } }

        //NO ENLAZADAS
		public virtual EEstado EEstado { get { return _base.EStatus; } set { _base.Record.Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.StatusLabel; } }
		public virtual string Usuario { get { return _base._usuario; } set { _base._usuario = value; } }
		public virtual string Expediente { get { return _base._expediente; } set { _base._expediente = value; } }
		public virtual string IDAlmacen { get { return _base._id_almacen; } set { _base._id_almacen = value; } }
		public virtual string Almacen { get { return _base._almacen; } set { _base._almacen = value; } }
		public virtual string IDAlmacenAlmacen { get { return _base.IDAlmacenAlmacen; } }
		public virtual string NSerie { get { return _base._n_serie; } set { _base._n_serie = value; } }
		public virtual string Serie { get { return _base._serie; } set { _base._serie = value; } }
		public virtual string NSerieSerie { get { return _base.NSerieSerie; } }
		public virtual string IDCliente { get { return _base.IDCliente; } set { _base.IDCliente = value; } }
		public virtual string Cliente { get { return _base._cliente; } set { _base._cliente = value; } }
		public virtual bool IDManual { get { return _base._id_manual; } set { _base._id_manual = value; } }
		public virtual Decimal Subtotal { get { return _base.Subtotal; } }

		#endregion
		
		#region Business Methods

        public void CopyFrom(Pedido source) { _base.CopyValues(source); }

		public virtual bool IsComplete()
		{
			foreach (LineaPedidoInfo item in Lineas)
				if (!item.IsComplete) return false;

			return true;
		}

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected PedidoInfo() { /* require use of factory methods */ }
		private PedidoInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal PedidoInfo(Pedido item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				_lineas_pedido = (item.Lineas != null) ? LineaPedidoList.GetChildList(item.Lineas) : null;
				
			}
		}
	
		public static PedidoInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false);	}
		public static PedidoInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new PedidoInfo(sessionCode, reader, childs); }

		public virtual void LoadPendiente() { LoadPendiente(true); }
		public virtual void LoadPendiente(bool childs)
		{
			_lineas_pedido = LineaPedidoList.GetPendientesChildList(this, childs);
		}

 		#endregion
		
		#region Root Factory Methods
		
        public static PedidoInfo Get(long oid) { return Get(oid, true); }
		public static PedidoInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = Pedido.GetCriteria(Pedido.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = PedidoInfo.SELECT(oid);
	
			PedidoInfo obj = DataPortal.Fetch<PedidoInfo>(criteria);
			Pedido.CloseSession(criteria.SessionCode);
			return obj;
		}

		public static PedidoInfo Get(long oid, bool childs, bool cache)
		{
			PedidoInfo item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(PedidoList)))
			{
				PedidoList items = PedidoList.NewList();

				item = PedidoInfo.Get(oid, childs);
				items.AddItem(item);
				Cache.Instance.Save(typeof(PedidoList), items);
			}
			else
			{
				PedidoList items = Cache.Instance.Get(typeof(PedidoList)) as PedidoList;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = PedidoInfo.Get(oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(PedidoList), items);
				}
			}

			return item;
		}

		#endregion
		
		#region Root Data Access
		 
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
	                    
						query = LineaPedidoList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _lineas_pedido = LineaPedidoList.GetChildList(SessionCode, reader, Childs);						
                    }
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
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
					
					query = LineaPedidoList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _lineas_pedido = LineaPedidoList.GetChildList(SessionCode, reader);					
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

		#region SQL

		public static string SELECT(long oid) { return Pedido.SELECT(oid, false); }

		#endregion
	}
}
