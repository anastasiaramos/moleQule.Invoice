using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using CslaEx;
using NHibernate;

using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Library.Common
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class CobroREAInfo : ReadOnlyBaseEx<CobroREAInfo, CobroREA>
	{	
		#region Attributes

		protected CobroREABase _base = new CobroREABase();

		protected IVChargeList_ivcharges = null;
		
		#endregion
		
		#region Properties
		
		public CobroREABase Base { get { return _base; } }
		
		public long OidCobro { get { return _base.Record.OidCobro; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public Decimal Cantidad { get { return _base.Record.Cantidad; } }
		public long OidExpedienteRea { get { return _base.Record.OidExpedienteRea; } }
		
		public IVChargeList IVCharges { get { return _ivcharges; } }
		
		
		#endregion
		
		#region Business Methods
						
		public void CopyFrom(CobroREA source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected CobroREAInfo() { /* require use of factory methods */ }
		private CobroREAInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal CobroREAInfo(CobroREA item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				_ivcharges = (item.IVCharges != null) ? IVChargeList.GetChildList(item.IVCharges) : null;
				
			}
		}
		
		public static CobroREAInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new CobroREAInfo(sessionCode, reader, childs);
		}
		
		public static CobroREAInfo New(long oid = 0) { return new CobroREAInfo(){ Oid = oid}; }
		
 		#endregion
		
		#region Root Factory Methods
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static CobroREAInfo Get(long oid, bool childs = false) 
		{ 
            if (!CobroREA.CanGetObject()) throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			return Get(CobroREA.SELECT(oid, false), childs); 
		}
		
		#endregion
					
		#region Common Data Access
								
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
					
					query = IVChargeList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _ivcharges = IVChargeList.GetChildList(SessionCode, reader);
					
				}
			}
            catch (Exception ex) { throw ex; }
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
			try
			{
				Oid = 0;
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
	                    
						query = IVChargeList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _ivcharges = IVChargeList.GetChildList(SessionCode, reader);
						
                    }
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex, new object[] { criteria.Query }); }
		}
		
		#endregion
					
        #region SQL
		
        #endregion		
	}
}
