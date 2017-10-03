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
using moleQule.Library.Hipatia;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class BudgetInfo : ReadOnlyBaseEx<BudgetInfo>
	{	
		#region Attributes

		protected BudgetBase _base = new BudgetBase();

		protected BudgetLineList _conceptos = null;

		#endregion
		
		#region Properties

		public BudgetBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidSerie { get { return _base.Record.OidSerie; } }
		public long OidCliente { get { return _base.Record.OidCliente; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public Decimal Subtotal { get { return _base.Record.Subtotal; } }
		public Decimal PDescuento { get { return _base.Record.PDescuento; } }
		public Decimal Impuestos { get { return _base.Record.Impuestos; } }
		public Decimal Total { get { return _base.Record.Total; } }
		public bool Nota { get { return _base.Record.Nota; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public Decimal PIRPF { get { return _base.Record.PIRPF; } }
		public int OidUsuario { get { return _base.Record.OidUsuario; } }
		
		public BudgetLineList Conceptos { get { return _conceptos; } }

		public virtual string VatNumber { get { return _base.VatNumber; } set { _base.VatNumber = value; } }
		public virtual string NumeroSerie { get { return _base.NumeroSerie; } set { _base.NumeroSerie = value; } }
		public virtual string IDCliente { get { return _base.IDCliente; } set { _base.IDCliente = value; } }
		public virtual string NumeroCliente { get { return IDCliente; } } /*DEPRECATED*/
		public virtual string NombreCliente { get { return _base.Cliente; } set { _base.Cliente = value; } }
		public virtual string CodigoPostal { get { return _base.CodigoPostal; } set { _base.CodigoPostal = value; } }
		public virtual Decimal Descuento { get { return _base.Descuento; } /*set { _base.Descuento = value; }*/ }
		public virtual Decimal BaseImponible { get { return _base.BaseImponible; } }
		public virtual Decimal IRPF { get { return _base.IRPF; } }
		public virtual bool NManual { get { return false; } }
		public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }
		public string FileName 
		{ 
			get 
			{
				string cliente = NombreCliente.Replace(".", "");

				return "Proforma_" + AppContext.ActiveSchema.Name.Replace(".", "") + " _" + 
						((cliente.Length > 15) ? cliente.Substring(0, 15) : cliente) + "_" + 
						Fecha.ToString("dd-MM-yyyy") + "_" + NumeroSerie + "_" + Codigo + ".pdf"; 
			} 
		}

		#endregion
		
		#region Business Methods

        public void CopyFrom(Budget source) { _base.CopyValues(source); }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected BudgetInfo() { /* require use of factory methods */ }
		private BudgetInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
		internal BudgetInfo(Budget item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				_conceptos = (item.Conceptos != null) ? BudgetLineList.GetChildList(item.Conceptos) : null;				
			}
		}
	
		public static BudgetInfo GetChild(IDataReader reader) { return GetChild(reader, false); }
		public static BudgetInfo GetChild(IDataReader reader, bool childs) { return new BudgetInfo(reader, childs); }

        public BudgetPrint GetPrintObject()
        {
            return BudgetPrint.New(this,
                                        ClienteInfo.Get(this.OidCliente),
                                        SerieInfo.Get(OidSerie));
        }
        public BudgetPrint GetPrintObject(ClienteInfo cliente, SerieInfo serie)
        {
            return BudgetPrint.New(this, cliente, serie);
        }

		public static BudgetInfo New(long oid = 0) { return new BudgetInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		public static BudgetInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = Budget.GetCriteria(Budget.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = BudgetInfo.SELECT(oid);
	
			BudgetInfo obj = DataPortal.Fetch<BudgetInfo>(criteria);
			Budget.CloseSession(criteria.SessionCode);
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
	                    
						query = BudgetLineList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _conceptos = BudgetLineList.GetChildList(reader);						
                    }
				}
			}
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
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
					
					query = BudgetLineList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _conceptos = BudgetLineList.GetChildList(reader);					
				}
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
        #region SQL

        public static string SELECT(long oid) { return Budget.SELECT(oid, false); }

        #endregion		
	}

    /// <summary>
    /// ReadOnly Root Object
    /// </summary>
    [Serializable()]
    public class SerialProformaInfo : SerialInfo
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Business Methods

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
        protected SerialProformaInfo() { /* require use of factory methods */ }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Obtiene el último serial de la entidad desde la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static SerialProformaInfo Get(long oid_serie, int year)
        {
            CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
            criteria.Childs = false;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(oid_serie, year);

            SerialProformaInfo obj = DataPortal.Fetch<SerialProformaInfo>(criteria);
            OutputInvoice.CloseSession(criteria.SessionCode);
            return obj;
        }

        /// <summary>
        /// Obtiene el siguiente serial para una entidad desde la base de datos
        /// </summary>
        /// <param name="entity">Tipo de Entidad</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static long GetNext(long oid_serie, int year)
        {
            return Get(oid_serie, year).Value + 1;
        }

        #endregion

        #region Root Data Access

        #endregion

        #region SQL

        public static string SELECT(long oid_serie, int year)
        {
            string p = nHManager.Instance.GetSQLTable(typeof(BudgetRecord));
            string s = nHManager.Instance.GetSQLTable(typeof(SerieRecord));
            string query;

            query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
                    " FROM " + p + " AS P" +
                    " INNER JOIN " + s + " AS S ON P.\"OID_SERIE\" = S.\"OID\"" +
                    " WHERE P.\"OID_SERIE\" = " + oid_serie.ToString() +
                    "   AND to_char(\"FECHA\", 'YYYY') = '" + year.ToString() + "'";

            return query;
        }

        #endregion

    }
}
