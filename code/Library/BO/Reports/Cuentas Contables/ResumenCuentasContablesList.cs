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
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    /// <summary>
    /// ReadOnly Root Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class ResumenCuentasContablesList : ReadOnlyListBaseEx<ResumenCuentasContablesList, ResumenCuentasContablesInfo>
    {
        #region Business Methods

        #endregion

        #region Factory Methods

        private ResumenCuentasContablesList() { }

		private ResumenCuentasContablesList(IList<ResumenCuentasContablesInfo> list)
		{
			Fetch(list);
		}

        public static ResumenCuentasContablesList NewList() { return new ResumenCuentasContablesList(); }
        
        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns></returns>
        public static ResumenCuentasContablesList GetList()
        {
            CriteriaEx criteria = Impuesto.GetCriteria(Impuesto.OpenSession());
            criteria.Childs = false;

            criteria.Query = ResumenCuentasContablesList.SELECT();

            ResumenCuentasContablesList list = DataPortal.Fetch<ResumenCuentasContablesList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        public static ResumenCuentasContablesList GetList(IList<ResumenCuentasContablesInfo> list) { return new ResumenCuentasContablesList(list); }

        #endregion

        #region Data Access

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
        /// </summary>
        /// <param name="lista">IList origen</param>
        private new void Fetch(IList<ResumenCuentasContablesInfo> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (ResumenCuentasContablesInfo item in lista)
                this.AddItem(item);

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

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
                    {
                        this.AddItem(ResumenCuentasContablesInfo.Get(reader));
                    }
                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        public static string SELECT()
        {
            string query;

            string ay = nHManager.Instance.GetSQLTable(typeof(GrantRecord));
            string ca = nHManager.Instance.GetSQLTable(typeof(CashRecord));
            string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));
            string de = nHManager.Instance.GetSQLTable(typeof(CustomAgentRecord));
            string em = nHManager.Instance.GetSQLTable(typeof(EmployeeRecord));
            string fa = nHManager.Instance.GetSQLTable(typeof(FamilyRecord));
            string im = nHManager.Instance.GetSQLTable(typeof(TaxRecord));
			string na = nHManager.Instance.GetSQLTable(typeof(ShippingCompanyRecord));
			string pr = nHManager.Instance.GetSQLTable(typeof(ProductRecord));
            string pv = nHManager.Instance.GetSQLTable(typeof(SupplierRecord));
            string tg = nHManager.Instance.GetSQLTable(typeof(ExpenseTypeRecord));
            string tr = nHManager.Instance.GetSQLTable(typeof(TransporterRecord));
            string se = nHManager.Instance.GetSQLTable(typeof(SchemaSettingRecord));
            string lo = nHManager.Instance.GetSQLTable(typeof(LoanRecord));

            query = " SELECT ROW_NUMBER() OVER (ORDER BY \"ENTIDAD\", \"CODIGO\", \"NOMBRE\") AS \"OID\", *" +
                    " FROM (" +

                    //Ayudas
                    "   SELECT 'Ayuda' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"NOMBRE\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + ay +

                    "   UNION" +

                    //Cajas
                    "   SELECT 'Caja' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"NOMBRE\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + ca +

                    "   UNION" +

                    //Clientes
                    "   SELECT 'Cliente' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       CASE WHEN \"NOMBRE_COMERCIAL\" = '' THEN \"NOMBRE\" ELSE \"NOMBRE_COMERCIAL\" END AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + cl +

                    "   UNION" +

                    //Cuentas Bancarias
                    "   SELECT 'Cuenta Bancaria' AS \"ENTIDAD\"," +
                    "       '--' AS \"CODIGO\"," +
                    "       \"ENTIDAD\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + cb +

                    "   UNION" +
                    "   SELECT 'Cuenta Bancaria (Gastos)' AS \"ENTIDAD\"," +
                    "       '--' AS \"CODIGO\"," +
                    "       \"ENTIDAD\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE_GASTOS\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + cb +

                    "   UNION" +

                    //Despachantes
                    "   SELECT 'Despachante' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"ALIAS\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + de +

                    "   UNION" +

                    //Empleados
                    "   SELECT 'Empleado' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"NOMBRE\" || ' ' || \"APELLIDOS\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + em +

                    "   UNION" +

                    //Familias
                    "   SELECT 'Familia (Compras)' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"NOMBRE\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE_COMPRA\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + fa +
                    "   UNION" +
                    "   SELECT 'Familia (Ventas)' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"NOMBRE\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE_VENTA\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + fa +

                    "   UNION" +

                    //Impuestos
                    "   SELECT 'Impuesto (Soportado)' AS \"ENTIDAD\"," +
                    "       '--' AS \"CODIGO\"," +
                    "       \"NOMBRE\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE_SOPORTADO\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + im +
                    "   UNION" +
                    "   SELECT 'Impuesto (Repercutido)' AS \"ENTIDAD\"," +
                    "       '--' AS \"CODIGO\"," +
                    "       \"NOMBRE\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE_REPERCUTIDO\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + im +

                    "   UNION" +

                    //Navieras
                    "   SELECT 'Naviera' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"ALIAS\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + na +

                    "   UNION" +

                    //Productos
                    "   SELECT 'Producto (Compras)' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"NOMBRE\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE_COMPRA\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + pr +
                    "   UNION" +
                    "   SELECT 'Producto (Ventas)' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"NOMBRE\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE_VENTA\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + pr +

                    "   UNION" +

                    //Proveedores
                    "   SELECT 'Proveedor' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"ALIAS\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + pv +
                    "   WHERE \"TIPO\" = " + (long)ETipoAcreedor.Proveedor +

                    "   UNION" +

                    //Acreedores
                    "   SELECT 'Acreedor' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"ALIAS\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + pv +
                    "   WHERE \"TIPO\" = " + (long)ETipoAcreedor.Acreedor +

                    "   UNION" +

                    //Tipos de Gastos
                    "   SELECT 'Tipo de Gasto' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"NOMBRE\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + tg +

                    "   UNION" +

                    //Transportistas
                    "   SELECT 'Transportista' AS \"ENTIDAD\"," +
                    "       \"CODIGO\" AS \"CODIGO\"," +
                    "       \"ALIAS\" AS \"NOMBRE\"," +
                    "       \"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + tr +

                    "   UNION" +

                    //Préstamos
                    "   SELECT 'Préstamo' AS \"ENTIDAD\"," +
                    "       PR.\"CODIGO\" AS \"CODIGO\"," +
                    "       CB.\"ENTIDAD\" AS \"NOMBRE\"," +
                    "       PR.\"CUENTA_CONTABLE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + lo + " AS PR" +
                    "   INNER JOIN " + cb + " AS CB ON CB.\"OID\" = PR.\"OID_CUENTA\"" +

                    "   UNION" +

                    //Settings
                    "   SELECT 'Varios' AS \"ENTIDAD\"," +
                    "       '01' AS \"CODIGO\"," +
                    "       'Nóminas' AS \"NOMBRE\"," +
                    "       \"VALUE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + se +
                    "   WHERE \"NAME\" = 'CUENTA_NOMINAS'" +
                    "   UNION" +
                    "   SELECT 'Varios' AS \"ENTIDAD\"," +
                    "       '02' AS \"CODIGO\"," +
                    "       'Seguros Sociales' AS \"NOMBRE\"," +
                    "       \"VALUE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + se +
                    "   WHERE \"NAME\" = 'CUENTA_SEGUROS_SOCIALES'" +
                    "   UNION" +
                    "   SELECT 'Varios' AS \"ENTIDAD\"," +
                    "       '03' AS \"CODIGO\"," +
                    "       'Hacienda Pública' AS \"NOMBRE\"," +
                    "       \"VALUE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + se +
                    "   WHERE \"NAME\" = 'CUENTA_HACIENDA_PUBLICA'" +
                    "   UNION" +
                    "   SELECT 'Varios' AS \"ENTIDAD\"," +
                    "       '04' AS \"CODIGO\"," +
                    "       'Remuneraciones' AS \"NOMBRE\"," +
                    "       \"VALUE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + se +
                    "   WHERE \"NAME\" = 'CUENTA_REMUNERACIONES'" +
                    "   UNION" +
                    "   SELECT 'Varios' AS \"ENTIDAD\"," +
                    "       '05' AS \"CODIGO\"," +
                    "       'Efectos A Cobrar' AS \"NOMBRE\"," +
                    "       \"VALUE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + se +
                    "   WHERE \"NAME\" = 'CUENTA_EFECTOS_A_COBRAR'" +
                    "   UNION" +
                    "   SELECT 'Varios' AS \"ENTIDAD\"," +
                    "       '06' AS \"CODIGO\"," +
                    "       'Efectos A Pagar' AS \"NOMBRE\"," +
                    "       \"VALUE\" AS \"CUENTA_CONTABLE\"" +
                    "   FROM " + se +
                    "   WHERE \"NAME\" = 'CUENTA_EFECTOS_A_PAGAR'" +
                    " ) AS Q";

            return query;
        }

        #endregion
    }
}

