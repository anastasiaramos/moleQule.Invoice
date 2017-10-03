using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class CobroREAInfo : ReadOnlyBaseEx<CobroREAInfo>
	{	
		#region Attributes

		protected CobroREABase _base = new CobroREABase();

		#endregion
		
		#region Properties

		public CobroREABase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidCobro { get { return _base.Record.OidCobro; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public Decimal Cantidad { get { return _base.Record.Cantidad; } }
		public long OidExpedienteREA { get { return _base.Record.OidExpedienteRea; } }
		public DateTime FechaAsignacion { get { return _base.Record.FechaAsignacion; } }

        //Campos no enlazados
		public string CodigoExpediente { get { return _base.Expediente; } set { _base.Expediente = value; } }

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
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"><see cref="BusinessBaseEx"/> origen</param>
        /// <param name="copy_childs">Flag para copiar los hijos</param>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		internal CobroREAInfo(CobroREA item, bool copy_childs)
		{
			_base.CopyValues(item);
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
		public static CobroREAInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static CobroREAInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new CobroREAInfo(sessionCode, reader, childs);
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
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		}
		
		#endregion
		
	}
}
