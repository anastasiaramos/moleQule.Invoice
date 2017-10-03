using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class BudgetLineInfo : ReadOnlyBaseEx<BudgetLineInfo>
	{	
		#region Attributes

		protected BudgetLineBase _base = new BudgetLineBase();
		
		#endregion
		
		#region Properties

		public BudgetLineBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidProforma { get { return _base.Record.OidProforma; } }
		public long OidPartida { get { return _base.Record.OidPartida; } }
		public long OidExpediente { get { return _base.Record.OidExpediente; } }
		public long OidProducto { get { return _base.Record.OidProducto; } }
		public string Concepto { get { return _base.Record.Concepto; } set { _base.Record.Concepto = value; } }
		public bool FacturacionBulto { get { return _base.Record.FacturacionBulto; } }
		public Decimal CantidadKilos { get { return _base.Record.CantidadKilos; } }
		public Decimal CantidadBultos { get { return _base.Record.CantidadBultos; } }
		public Decimal PImpuestos { get { return _base.Record.PImpuestos; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public Decimal Precio { get { return _base.Record.Precio; } }
		public Decimal Subtotal { get { return _base.Record.Subtotal; } }
		public long OidImpuesto { get { return _base.Record.OidImpuesto; } }

		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } }
		public virtual Decimal Impuestos { get { return _base.Impuestos; } }
		public virtual Decimal Beneficio { get { return _base.Beneficio; } }
		public virtual Decimal BeneficioKilo { get { return _base.BeneficioKilo; } }
		public virtual string Expediente { get { return _base._expediente; } } 
		public virtual bool FacturacionPeso { get { return _base.FacturacionPeso; } set { _base.Record.FacturacionBulto = !value; } }
		public virtual ETipoFacturacion ETipoFacturacion { get { return _base.ETipoFacturacion; } }

		#endregion
		
		#region Business Methods

		public void CopyFrom(BudgetLine source) { _base.CopyValues(source); }
		public void CopyFrom(BudgetLineInfo source) { _base.CopyValues(source); }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected BudgetLineInfo() { /* require use of factory methods */ }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> origen de los datos</param>
        /// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		private BudgetLineInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
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
		internal BudgetLineInfo(BudgetLine item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				
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
		public static BudgetLineInfo GetChild(IDataReader reader)
        {
			return GetChild(reader, false);
		}
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
		/// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		/// <remarks>La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista<remarks/>
		public static BudgetLineInfo GetChild(IDataReader reader, bool childs)
        {
			return new BudgetLineInfo(reader, childs);
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
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
        #region SQL

        public static string SELECT(long oid) { return BudgetLine.SELECT(oid, false); }

        #endregion		
	}
}