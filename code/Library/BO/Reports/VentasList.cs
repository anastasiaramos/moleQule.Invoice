using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library.CslaEx; 
using moleQule.Library;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// ReadOnly Root Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class VentasList : ReadOnlyListBaseEx<VentasList, VentasInfo>
    {
        #region Business Methods

        #endregion

        #region Factory Methods

        private VentasList() {}

        public static VentasList GetListByCliente(QueryConditions conditions, bool detallado)
        {
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

            criteria.Query = VentasInfo.SELECT_BY_CLIENTE(conditions, detallado);

            VentasList list = DataPortal.Fetch<VentasList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static VentasList GetListByProducto(QueryConditions conditions, bool detallado)
        {
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

			criteria.Query = VentasInfo.SELECT_BY_PRODUCTO(conditions, detallado);

            VentasList list = DataPortal.Fetch<VentasList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static VentasList GetListByExpediente(QueryConditions conditions, bool detallado)
        {
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

			criteria.Query = VentasInfo.SELECT_BY_EXPEDIENTE(conditions, detallado);

            VentasList list = DataPortal.Fetch<VentasList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static VentasList GetListByClienteMensual(QueryConditions conditions)
        {
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

			criteria.Query = VentasInfo.SELECT_BY_CLIENTE_MENSUAL(conditions);

            VentasList list = DataPortal.Fetch<VentasList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static VentasList GetListByProductoMensual(QueryConditions conditions)
        {
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

			criteria.Query = VentasInfo.SELECT_BY_PRODUCTO_MENSUAL(conditions);

            VentasList list = DataPortal.Fetch<VentasList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static VentasList GetListMensual(QueryConditions conditions)
        {
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

			criteria.Query = VentasInfo.SELECT_MENSUAL(conditions);

            VentasList list = DataPortal.Fetch<VentasList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		public static VentasList GetHistoricoPreciosClientesList(QueryConditions conditions)
		{
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
			criteria.Childs = false;

			criteria.Query = VentasInfo.SELECT_HISTORICO_CLIENTES(conditions);

			VentasList list = DataPortal.Fetch<VentasList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		public static VentasList GetHistoricoPreciosProductosList(QueryConditions conditions)
		{
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
			criteria.Childs = false;

			criteria.Query = VentasInfo.SELECT_HISTORICO_PRODUCTOS(conditions);

			VentasList list = DataPortal.Fetch<VentasList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		public static VentasList GetListByClientePorcentualVenta(QueryConditions conditions)
		{
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
			criteria.Childs = false;

			criteria.Query = VentasInfo.SELECT_BY_CLIENTE_PORCENTUAL_VENTA_MENSUAL(conditions);

			VentasList list = DataPortal.Fetch<VentasList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
		public static VentasList GetListByClientePorcentualBeneficio(QueryConditions conditions)
		{
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
			criteria.Childs = false;

			criteria.Query = VentasInfo.SELECT_BY_CLIENTE_PORCENTUAL_BENEFICIO_MENSUAL(conditions);

			VentasList list = DataPortal.Fetch<VentasList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
        public static VentasList GetListByClientePorcentualVentaPeriodo(QueryConditions conditions)
        {
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

            criteria.Query = VentasInfo.SELECT_BY_CLIENTE_PORCENTUAL_VENTA_PERIODO(conditions);

            VentasList list = DataPortal.Fetch<VentasList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
        public static VentasList GetListByClientePorcentualBeneficioPeriodo(QueryConditions conditions)
        {
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
            criteria.Childs = false;

            criteria.Query = VentasInfo.SELECT_BY_CLIENTE_PORCENTUAL_BENEFICIO_PERIODO(conditions);

            VentasList list = DataPortal.Fetch<VentasList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		public static VentasList GetListByProductoPorcentualVenta(QueryConditions conditions)
		{
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
			criteria.Childs = false;

			criteria.Query = VentasInfo.SELECT_BY_PRODUCTO_PORCENTUAL_VENTA_MENSUAL(conditions);

			VentasList list = DataPortal.Fetch<VentasList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
		public static VentasList GetListByProductoPorcentualBeneficio(QueryConditions conditions)
		{
			CriteriaEx criteria = Cliente.GetCriteria(Cliente.OpenSession());
			criteria.Childs = false;

			criteria.Query = VentasInfo.SELECT_BY_PRODUCTO_PORCENTUAL_BENEFICIO_MENSUAL(conditions);

			VentasList list = DataPortal.Fetch<VentasList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
        
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
                        this.AddItem(VentasInfo.Get(reader));
                   
                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion
    }
}

