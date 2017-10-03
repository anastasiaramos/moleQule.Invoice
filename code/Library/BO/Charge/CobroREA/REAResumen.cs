using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
    [Serializable()]
    public class REAResumen : ReadOnlyBaseEx<REAResumen>
    {
        #region Business Methods

        private long _oid_expediente;
        private string _codigo = string.Empty;
        private string _cuenta = string.Empty;
        private string _expediente_rea = string.Empty;
        private string _certificado_rea = string.Empty;
        private DateTime _fecha;
        private DateTime _fecha_cobro_rea;
        private decimal _ayuda_estimada;
        private decimal _cobrado;
        private decimal _pendiente;

        public virtual long OidExpediente { get { return _oid_expediente; } set { _oid_expediente = value; } }
        public virtual string Expediente { get { return _codigo; } set { _codigo = value; } }
        public virtual string CuentaRea { get { return _cuenta; } set { _cuenta = value; } }
        public virtual string ExpedienteRea { get { return _expediente_rea; } set { _expediente_rea = value; } }
        public virtual string CertificadoRea { get { return _certificado_rea; } set { _certificado_rea = value; } }
        public virtual DateTime Fecha { get { return _fecha_cobro_rea; } set { _fecha = value; } }
        public virtual DateTime Prevision { get { return _fecha_cobro_rea; } set { _fecha_cobro_rea = value; } }
        public virtual decimal TotalFacturado { get { return _ayuda_estimada; } set { _ayuda_estimada = value; } }
        public virtual decimal Pagado { get { return _cobrado; } set { _cobrado = value; } }
        public virtual decimal Cobrado { get { return _cobrado; } set { _cobrado = value; } }
        public virtual decimal Pendiente { get { return _ayuda_estimada - _cobrado; } }

		protected void CopyValues(IDataReader source)
		{
            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_expediente = Format.DataReader.GetInt64(source, "OID");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _cuenta = Format.DataReader.GetString(source, "CUENTA_REA");
            _expediente_rea = Format.DataReader.GetString(source, "EXPEDIENTE_REA");
            _certificado_rea = Format.DataReader.GetString(source, "CERTIFICADO_REA");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA_DESPACHO_DESTINO");
            _fecha_cobro_rea = Format.DataReader.GetDateTime(source, "COBRO_REA");
            _ayuda_estimada = Format.DataReader.GetDecimal(source, "TOTAL_FACTURADO");
            _cobrado = Format.DataReader.GetDecimal(source, "TOTAL_COBRADO");
		}

        #endregion

        #region Factory Methods

        private REAResumen() { }

        private REAResumen(long oid_expediente)
        {
            ExpedientInfo p = ExpedientInfo.Get(oid_expediente);
            Charges cobros = Charges.GetListByExpediente(oid_expediente, true);
            Oid = p.Oid;
            _oid_expediente = p.Oid;
            _codigo = p.Codigo;
            //_cuenta = p.CuentaRea;
            //_expediente_rea = p.ExpedienteRea;
            //_certificado_rea = p.CertificadoRea;
            //_fecha_cobro_rea = p.CobroRea;
            _cobrado = cobros.GetTotalREA();
            _ayuda_estimada = p.AyudaEstimada;
            _pendiente = _ayuda_estimada - _cobrado;
        }

        private REAResumen(IDataReader source)
        {
            CopyValues(source);
        }

        public void Refresh(Charges cobros)
        {
            _cobrado = cobros.GetTotalREA();
            _pendiente = _ayuda_estimada - _cobrado;
        }

        public static REAResumen Get(IDataReader source)
        {
            if (source == null) return null;
            return new REAResumen(source);
        }

        public static REAResumen Get(long oid_expediente)
        {
            return new REAResumen(oid_expediente);
        }

        public static REAResumen Get(Expedient expediente)
        {
			CriteriaEx criteria = OutputInvoice.GetCriteria(OutputInvoice.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = REAResumen.SELECT(expediente);

            OutputInvoice.BeginTransaction(criteria.Session);

            REAResumen obj = DataPortal.Fetch<REAResumen>(criteria);
            OutputInvoice.CloseSession(criteria.SessionCode);
            return obj;
        }

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                SessionCode = criteria.SessionCode;

                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        CopyValues(reader);
                }
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

        public static string SELECT(Expedient expedient)
        {
            string ex = nHManager.Instance.GetSQLTable(typeof(ExpedientRecord));
            string ch = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
            string ba = nHManager.Instance.GetSQLTable(typeof(BatchRecord));
            string rea = nHManager.Instance.GetSQLTable(typeof(CobroREARecord));
            
			string query = @"
			SELECT EX.*
					,""TOTAL_FACTURADO""
					,""TOTAL_COBRADO""
			FROM " + ex + @" AS EX 
			LEFT JOIN (SELECT ""OID_EXPEDIENTE""
								,SUM(""AYUDA_RECIBIDA_KILO"") * SUM(""KILOS_INICIALES"") AS ""TOTAL_FACTURADO"" 
						FROM " + ba + @" 
						GROUP BY ""OID_EXPEDIENTE"") 
				AS PE ON PE.""OID_EXPEDIENTE"" = EX.""OID""
			LEFT JOIN (SELECT ""OID_EXPEDIENTE""
								,""OID_COBRO""
								,SUM(""CANTIDAD"") AS ""TOTAL_COBRADO"" 
						FROM " + rea + @"
						GROUP BY ""OID_EXPEDIENTE"", ""OID_COBRO"") 
				AS CR ON CR.""OID_EXPEDIENTE"" = EX.""OID""
			LEFT JOIN " + ch + @" AS C ON C.""OID"" = CR.""OID_COBRO""
			WHERE EX.""OID"" = " + expedient.Oid;

            return query;
        }

    }
}
