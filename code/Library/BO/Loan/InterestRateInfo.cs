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
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class InterestRateInfo : ReadOnlyBaseEx<InterestRateInfo>
	{	
		#region Attributes

		protected InterestRateBase _base = new InterestRateBase();
		
		#endregion
		
		#region Properties

		public InterestRateBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidPrestamo { get { return _base.Record.OidPrestamo; } }
		public Decimal Tipo { get { return _base.Record.TipoInteres; } }
		public DateTime FechaInicio { get { return _base.Record.FechaInicio; } }
		public DateTime FechaFin { get { return _base.Record.FechaFin; } }
		public Decimal ImporteCuota { get { return _base.Record.ImporteCuota; } }		
		
		#endregion
		
		#region Business Methods
			
		public void CopyFrom(InterestRate source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected InterestRateInfo() { /* require use of factory methods */ }
		private InterestRateInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal InterestRateInfo(InterestRate item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}
		
		public static InterestRateInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static InterestRateInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new InterestRateInfo(sessionCode, reader, childs);
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
				
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
					
        #region SQL

        //public static string SELECT(long oid) { return TipoInteres.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return InterestRate.SELECT(conditions, false); }		
		public static string SELECT(LoanInfo item) { return SELECT(new Library.Invoice.QueryConditions { Prestamo = item }); }
			
		
        #endregion		
	}
}