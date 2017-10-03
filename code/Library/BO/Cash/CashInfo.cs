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
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class CashInfo : ReadOnlyBaseEx<CashInfo>
	{	
		#region Attributes

		protected CashBase _base = new CashBase();

		protected CashLineList _lines = null;

		#endregion
		
		#region Properties

		public CashBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public string CuentaContable { get { return _base.Record.CuentaContable; } }

		public CashLineList Lines { get { return _lines; } }

        //NO ENLAZADAS
		public virtual decimal DebeAcumulado { get { return _base.DebeAcumulado; } set { _base.DebeAcumulado = value; } }
		public virtual decimal HaberAcumulado { get { return _base.HaberAcumulado; } set { _base.HaberAcumulado = value; } }
		public virtual decimal SaldoAcumulado { get { return _base.SaldoAcumulado; } }
		public virtual decimal DebeTotal { get { return _base.DebeTotal; } set { _base.DebeTotal = value; } }
		public virtual decimal HaberTotal { get { return _base.HaberTotal; } set { _base.HaberTotal = value; } }
		public virtual decimal SaldoTotal { get { return _base.SaldoTotal; } }
		public virtual decimal DebeParcial { get { return _base.DebeParcial; } }
		public virtual decimal HaberParcial { get { return _base.HaberParcial; } }
		public virtual decimal SaldoParcial { get { return _base.SaldoParcial; } }

		#endregion
		
		#region Business Methods

        public void CopyFrom(Cash source) { _base.CopyValues(source); }

        /// <summary>
        /// Actualiza el saldo de cada línea de caja
        /// </summary>
        public virtual void UpdateSaldo(CashLineList lista)
        {
			DebeTotal = DebeAcumulado;
			HaberTotal = HaberAcumulado;
            
            if (lista.Count == 0) return;

            lista[0].Saldo = SaldoAcumulado + lista[0].Debe - lista[0].Haber;
			DebeTotal += lista[0].Debe;
			HaberTotal += lista[0].Haber;

            for (int i = 1; i < lista.Count; i++)
            {
                lista[i].Saldo = lista[i - 1].Saldo + lista[i].Debe - lista[i].Haber;
				DebeTotal += lista[i].Debe;
				HaberTotal += lista[i].Haber;
            }
        }

		public virtual decimal GetDebeAcumulado(int index)
		{
			decimal debe = DebeAcumulado;

			if (Lines == null) return debe;

			for (int i = 1; i < index; i++)
				debe += Lines[i].Debe;

			return debe;
		}
		public virtual decimal GetHaberAcumulado(int index)
		{
			decimal haber = HaberAcumulado;

			if (Lines == null) return haber;

			for (int i = 1; i < index; i++)
				haber += Lines[i].Haber;

			return haber;
		}

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected CashInfo() { /* require use of factory methods */ }
		private CashInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
		internal CashInfo(Cash item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				_lines = (item.Lines != null) ? CashLineList.GetChildList(item.Lines) : null;				
			}
		}
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        /// <remarks>
		/// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool childs)
		/// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
		/// <remarks/>
		public static CashInfo GetChild(IDataReader reader)
        {
			return GetChild(reader, false);
		}
		public static CashInfo GetChild(IDataReader reader, bool childs)
        {
			return new CashInfo(reader, childs);
		}

		public static CashInfo New(long oid = 0) { return new CashInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		public static CashInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = Cash.GetCriteria(Cash.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = CashInfo.SELECT(oid);
	
			CashInfo obj = DataPortal.Fetch<CashInfo>(criteria);
			Cash.CloseSession(criteria.SessionCode);
			return obj;
		}
		
		#endregion
		
		#region Root Data Access
		 
        /// <summary>
        /// Obtiene un registro de la base de datos
        /// </summary>
        /// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
        /// <remarks>
        /// La llama el DataPortal
        /// </remarks>
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
	                    
						query = CashLineList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _lines = CashLineList.GetChildList(reader);

                        UpdateSaldo(_lines);
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
		
        /// <summary>
        /// Obtiene un objeto a partir de un <see cref="IDataReader"/>.
        /// Obtiene los hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria"><see cref="IDataReader"/> con los datos</param>
        /// <remarks>
        /// La utiliza el <see cref="ReadOnlyListBaseEx"/> correspondiente para construir los objetos de la lista
        /// </remarks>
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;
					
					query = CashLineList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _lines = CashLineList.GetChildList(reader);

                    UpdateSaldo(_lines);
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

        #region SQL

        public static string SELECT(long oid) { return Cash.SELECT(oid, false); }

        #endregion	
	}
}
