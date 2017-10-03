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
    public class FacREAInfo : ReadOnlyBaseEx<FacREAInfo>
    {
        #region Business Methods

        private long _oid_expediente;
		private long _oid_expediente_rea;
        private string _n_expediente;
        private long _tipo_expediente;
        private string _expediente_rea = string.Empty;
        private DateTime _fecha;
        private DateTime _prevision;
        private decimal _total_ayuda;
        private decimal _cobrado;
        private decimal _asignado;
        private DateTime _fecha_cobro;
        private DateTime _fecha_asignacion;
        private string _activo = Resources.Labels.SET_COBRO;
		private decimal _acumulado = 0;

        public virtual long OidExpediente { get { return _oid_expediente; } }
		public virtual long OidExpedienteREA { get { return _oid_expediente_rea; } }
        public virtual string NExpediente { get { return _n_expediente; } }
        public virtual long TipoExpediente { get { return _tipo_expediente; } }
        public virtual string ExpedienteREA { get { return _expediente_rea; } }
		public virtual DateTime FechaDate { get { return _fecha; } }
		public virtual string Fecha { get { return (_fecha != DateTime.MinValue) ? _fecha.ToShortDateString() : "---"; } }
        public virtual string Prevision { get { return (_prevision != DateTime.MinValue) ? _prevision.ToShortDateString() : "---"; } }
        public virtual decimal TotalAyuda { get { return _total_ayuda; } }
		public virtual decimal Asignado { get { return _asignado; } set { _asignado = value; } }
		public virtual string Vinculado { get { return _activo; } set { _activo = value; } }
        public virtual decimal Pendiente { get { return Decimal.Round(_total_ayuda - CobrosAnteriores - _asignado, 2); } set { } }
		public virtual decimal CobrosAnteriores { get { return Decimal.Round(_cobrado, 2); } }
        public virtual string FechaCobro 
        { 
            get { return (_fecha_cobro != DateTime.MinValue) ? _fecha_cobro.ToShortDateString() : "---"; }
        }
        public virtual string FechaAsignacion 
        { 
            get { return (_fecha_asignacion != DateTime.MinValue) ? _fecha_asignacion.ToShortDateString() : "---"; }
			set { _fecha_asignacion = DateTime.Parse(value); }
        }
        public long DiasTranscurridos 
        { 
            get 
            { 
                DateTime fechaBase = (_fecha_cobro != DateTime.MinValue) ? _fecha_cobro : DateTime.Today;
                DateTime fechaOrigen = (_fecha != DateTime.MinValue) ? _fecha : DateTime.Today;
				return fechaOrigen.Subtract(fechaBase).Days; 
            }
        }
		public decimal Acumulado { get { return _acumulado; } set { _acumulado = value; } }
        public virtual decimal Cobrado { get { return _cobrado; } set { _cobrado = value; } }

        #endregion

		#region Business Methods

		protected void CopyValues(IDataReader source)
		{
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_expediente_rea = Format.DataReader.GetInt64(source, "OID");
			_oid_expediente = Format.DataReader.GetInt64(source, "OID_EXPEDIENTE");
			_n_expediente = Format.DataReader.GetString(source, "CODIGO_EXPEDIENTE");
			_tipo_expediente = Format.DataReader.GetInt64(source, "TIPO_EXPEDIENTE");
			_expediente_rea = Format.DataReader.GetString(source, "EXPEDIENTE_REA");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_asignado = Format.DataReader.GetDecimal(source, "ASIGNADO");
			_cobrado = Format.DataReader.GetDecimal(source, "COBRADO");
			_total_ayuda = Format.DataReader.GetDecimal(source, "AYUDA_ESTIMADA");
			_fecha_cobro = Format.DataReader.GetDateTime(source, "FECHA_COBRO");
			_fecha_asignacion = Format.DataReader.GetDateTime(source, "FECHA_ASIGNACION");

			_prevision = (_fecha != DateTime.MinValue) ? _fecha.AddDays(Convert.ToDouble(Resources.Defaults.PLAZO_REA)) : DateTime.MinValue;
			_activo = (_asignado == 0) ? Resources.Labels.SET_COBRO : Resources.Labels.RESET_COBRO;
			_cobrado = (_cobrado - _asignado) > 0 ? (_cobrado - _asignado) : 0;
		}

		private void CopyFrom(ExpedienteREAInfo source, ExpedientInfo exp, Charge cobro)
		{
			Oid = source.Oid;
			_oid_expediente_rea = source.Oid;
			_oid_expediente = source.Oid;
			_expediente_rea = source.NExpedienteREA;
			_fecha = source.Fecha;
			
			_n_expediente = exp.Codigo;
			_tipo_expediente = exp.Tipo;			
			_prevision = source.FechaCobro;
			_total_ayuda = exp.AyudaEstimada;
			
			CobroREA cobroR = cobro.CobroREAs.GetItemByExpedienteREA(source.Oid);
			_asignado = cobroR.Cantidad;
			_activo = (_asignado == 0) ? Resources.Labels.SET_COBRO : Resources.Labels.RESET_COBRO;
			_fecha_cobro = cobro.Fecha;
		}

		#endregion

		#region Factory Methods

		protected FacREAInfo() {}

		public static FacREAInfo Get(ExpedienteREAInfo source, ExpedientInfo exp, Charge cobro)
        {
			FacREAInfo obj = new FacREAInfo();
			obj.CopyFrom(source, exp, cobro);
			return obj;
        }
        public static FacREAInfo GetChild(IDataReader reader)
        {
            FacREAInfo obj = new FacREAInfo();
			obj.CopyValues(reader);
			return obj;
        }

        #endregion

    }
}
