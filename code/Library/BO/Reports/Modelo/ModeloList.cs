using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class ModeloList : ReadOnlyListBaseEx<ModeloList, ModeloInfo>
    {
        #region Business Methods

		public decimal Total(EPeriodo periodo)
		{
			decimal total = 0;

			foreach (ModeloInfo item in this)
			{
				switch (periodo)
				{
					case EPeriodo.Anual: total += item.Total; break;
					case EPeriodo.Periodo1T: total += item.Total1T; break;
					case EPeriodo.Periodo2T: total += item.Total2T; break;
					case EPeriodo.Periodo3T: total += item.Total3T; break;
					case EPeriodo.Periodo4T: total += item.Total4T; break;
				}
			}

			return total;
		}

		public decimal Base(EPeriodo periodo)
		{
			decimal total = 0;

			foreach (ModeloInfo item in this)
			{
				switch (periodo)
				{
					case EPeriodo.Anual: total += item.Base; break;
					case EPeriodo.Periodo1T: total += item.Base1T; break;
					case EPeriodo.Periodo2T: total += item.Base2T; break;
					case EPeriodo.Periodo3T: total += item.Base3T; break;
					case EPeriodo.Periodo4T: total += item.Base4T; break;
				}
			}

			return total;
		}

		public decimal TotalRepercutido(EPeriodo periodo)
		{ 
			decimal total = 0;

			foreach (ModeloInfo item in this)
			{
				switch (periodo)
				{
					case EPeriodo.Anual: total += item.TotalRepercutido; break;
					case EPeriodo.Periodo1T: total += item.TotalRepercutido1T; break;
					case EPeriodo.Periodo2T: total += item.TotalRepercutido2T; break;
					case EPeriodo.Periodo3T: total += item.TotalRepercutido3T; break;
					case EPeriodo.Periodo4T: total += item.TotalRepercutido4T; break;
				}
			}

			return total;
		}
		
		public decimal TotalSoportado(EPeriodo periodo)
		{
			decimal total = 0;

			foreach (ModeloInfo item in this)
			{
				switch (periodo)
				{
					case EPeriodo.Anual: total += item.TotalSoportado; break;
					case EPeriodo.Periodo1T: total += item.TotalSoportado1T; break;
					case EPeriodo.Periodo2T: total += item.TotalSoportado2T; break;
					case EPeriodo.Periodo3T: total += item.TotalSoportado3T; break;
					case EPeriodo.Periodo4T: total += item.TotalSoportado4T; break;
				}
			}

			return total;
		}

		public decimal TotalSoportadoImportacion(EPeriodo periodo)
		{
			decimal total = 0;

			foreach (ModeloInfo item in this)
			{
				switch (periodo)
				{
					case EPeriodo.Anual: total += item.TotalSoportadoImportacion; break;
					case EPeriodo.Periodo1T: total += item.TotalSoportadoImportacion1T; break;
					case EPeriodo.Periodo2T: total += item.TotalSoportadoImportacion2T; break;
					case EPeriodo.Periodo3T: total += item.TotalSoportadoImportacion3T; break;
					case EPeriodo.Periodo4T: total += item.TotalSoportadoImportacion4T; break;
				}
			}

			return total;
		}
        #endregion

        #region Factory Methods

        private ModeloList() { }

        private ModeloList(IDataReader reader)
        {
            Fetch(reader);
        }

        private ModeloList(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        #endregion

        #region Root Factory Methods

		public static ModeloList GetList(Common.QueryConditions conditions, bool childs) { return GetList(SELECT(conditions), childs); }
		public static ModeloList GetList(QueryConditions conditions, bool childs) { return GetList(SELECT(conditions), childs); }
        private static ModeloList GetList(string query, bool childs)
        {
			CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());
            criteria.Childs = childs;

            criteria.Query = query;
            ModeloList list = DataPortal.Fetch<ModeloList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static ModeloList GetList(IList<ModeloInfo> list)
        {
            ModeloList flist = new ModeloList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ModeloInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }		

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Builds a ModeloList from a IList<!--<ModeloInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>ModeloList</returns>
        public static ModeloList GetChildList(IList<ModeloInfo> list)
        {
            ModeloList flist = new ModeloList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (ModeloInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        public static ModeloList GetChildList(IDataReader reader) { return new ModeloList(reader); }
        public static ModeloList GetChildList(IDataReader reader, bool childs) { return new ModeloList(reader, childs); }

        #endregion

        #region Data Access

        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

					while (reader.Read())
                        AddItem(ModeloInfo.GetChild(reader, Childs));

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        // called to retrieve data from db
        protected void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IsReadOnly = false;

                while (reader.Read())
                {
                    AddItem(ModeloInfo.GetChild(reader, Childs));
                }

                IsReadOnly = true;

            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        public static string SELECT() { return SELECT(new Common.QueryConditions()); }
        public static string SELECT(Common.QueryConditions conditions) { return ModeloInfo.SELECT(conditions); }
		public static string SELECT(QueryConditions conditions) { return ModeloInfo.SELECT(conditions); }

		#endregion
    }
}



