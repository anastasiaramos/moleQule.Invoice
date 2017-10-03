using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class CashLines : BusinessListBaseEx<CashLines, CashLine>
	{
		#region Business Methods

		public void SetMaxSerial()
		{
			foreach (CashLine item in this)
				if (item.Serial > MaxSerial) MaxSerial = item.Serial;
		}

		public void ChageState(EEstado estado)
		{ 
		
		}

		public void SetNextCode(Cash parent, CashLine item)
		{
			int index = this.IndexOf(item);

			if (index == 0)
			{
				item.GetNewCode(parent.Oid);
				MaxSerial = item.Serial;
			}
			else
			{
				item.Serial = MaxSerial + 1;
				item.Codigo = item.Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);
				MaxSerial++;
			}
		}

		/// <summary>
		/// Crea un nuevo elemento y lo añade a la lista
		/// </summary>
		/// <returns>Nuevo item</returns>
		public CashLine NewItem(Cash parent)
		{
			CashLine obj = CashLine.NewChild(parent); 
			this.NewItem(obj);
            SetNextCode(parent, obj);
			return obj;
		}

		public override void Remove(long oid)
		{
 			CashLine item = GetItem(oid);
            RemoveItem(item);
		}
		public bool RemoveItem(CashLine line)
		{
            line.EEstado = EEstado.Anulado;
            line.SetLink(null);
			return true;
		}
		protected override void RemoveItem(int index)
		{
            RemoveItem(Items[index]);
		}

        public CashLine GetItemByCharge(long oidCharge)
        {
            return Items.FirstOrDefault(x => x.OidCobro == oidCharge && x.EEstado != EEstado.Anulado);
        }

        public CashLine GetItemByPayment(long oidPayment)
        {
            return Items.FirstOrDefault(x => x.OidPago == oidPayment && x.EEstado != EEstado.Anulado);
        }

        public CashLine GetItemByCobroInCierre(long oidCharge)
        {
            return Items.FirstOrDefault(x => x.OidCobro == oidCharge && (x.OidCierre != 0) && x.EEstado != EEstado.Anulado);
        }

		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private CashLines() { }

		#endregion		
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private CashLines(IList<CashLine> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
        private CashLines(IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            Fetch(reader);
        }

		/// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static CashLines NewChildList() 
        { 
            CashLines list = new CashLines(); 
            list.MarkAsChild(); 
            return list; 
        }
        public static CashLines NewChildList(Cash parent)
        {
            CashLines list = new CashLines();
            list.MarkAsChild();
            return list;
        }

		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static CashLines GetChildList(IList<CashLine> lista) { return new CashLines(lista); }
        public static CashLines GetChildList(IDataReader reader) { return GetChildList(reader, true); }
        public static CashLines GetChildList(IDataReader reader, bool childs) { return new CashLines(reader, childs); }

		#endregion
		
		#region Child Data Access

        private void Fetch(IList<CashLine> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (CashLine item in lista)
			{
				CashLine obj = CashLine.GetChild(item, Childs);
				this.AddItem(obj);
				if (item.Serial > MaxSerial) MaxSerial = item.Serial;
			}

			this.RaiseListChangedEvents = true;
		}
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

			MaxSerial = 0;

			while (reader.Read())
			{
				CashLine item = CashLine.GetChild(SessionCode, reader, Childs);
				this.AddItem(item);
				if (item.Serial > MaxSerial) MaxSerial = item.Serial;
			}

            this.RaiseListChangedEvents = true;
        }
		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(Cash parent)
		{
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (CashLine obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (CashLine obj in this)
				{
					if (obj.IsNew)
					{
						//SetNextCode(parent, obj);
						obj.Insert(parent);
					}
					else
						obj.Update(parent);
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
		}

        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
        /// de los flags de cada objeto de la lista
        /// </summary>
        /// <param name="parent">BusinessBaseEx padre de la lista</param>
        internal void Update(CierreCaja parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (CashLine obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (CashLine obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }
		
		#endregion

        #region SQL

        public static string SELECT(QueryConditions conditions) { return CashLine.SELECT(conditions, true); }
		public static string SELECT(Cash source) 
		{
			QueryConditions conditions = new QueryConditions
			{
				Caja = source.GetInfo(false),
				CierreCaja = CierreCajaInfo.New()
			};

			return CashLine.SELECT(conditions, true);
		}
        public static string SELECT_BY_CAJA(long oid)
        {
			QueryConditions conditions = new QueryConditions
			{
				Caja = Cash.New().GetInfo(),
				CierreCaja = CierreCaja.New().GetInfo()
			};
			conditions.Caja.Oid = oid;
			conditions.CierreCaja.Oid = 0;

            return CashLine.SELECT(conditions, true);
        }
        public static string SELECT_BY_CIERRE(long oid)
        {
            QueryConditions conditions = new QueryConditions();
            conditions.CierreCaja = CierreCaja.New().GetInfo();
            conditions.CierreCaja.Oid = oid;

            return CashLine.SELECT(conditions, true);
        }

        #endregion
    }
}

