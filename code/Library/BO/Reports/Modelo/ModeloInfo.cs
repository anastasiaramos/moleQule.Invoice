using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;
using moleQule.Library.Store;

namespace moleQule.Library.Invoice
{
	/// <summary>
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
    [Serializable()]
    public class ModeloInfo : ReadOnlyBaseEx<ModeloInfo>
    {
        #region Attributes

		protected long _modelo;
		protected long _oid_titular;
		protected string _id_titular = string.Empty;
		protected string _vat_number_titular = string.Empty;
		protected string _titular = string.Empty;
		protected long _tipo_titular;

		protected decimal _base;
		protected decimal _base_1T;
		protected decimal _base_2T;
		protected decimal _base_3T;
		protected decimal _base_4T;

		protected decimal _total;
		protected decimal _total_1T;
		protected decimal _total_2T;
		protected decimal _total_3T;
		protected decimal _total_4T;

		protected decimal _total_efectivo;
		protected decimal _total_efectivo_1T;
		protected decimal _total_efectivo_2T;
		protected decimal _total_efectivo_3T;
		protected decimal _total_efectivo_4T;

		protected decimal _total_soportado;
		protected decimal _total_soportado_1T;
		protected decimal _total_soportado_2T;
		protected decimal _total_soportado_3T;
		protected decimal _total_soportado_4T;

		protected decimal _total_soportado_importacion;
		protected decimal _total_soportado_importacion_1T;
		protected decimal _total_soportado_importacion_2T;
		protected decimal _total_soportado_importacion_3T;
		protected decimal _total_soportado_importacion_4T;

		protected decimal _total_repercutido;
		protected decimal _total_repercutido_1T;
		protected decimal _total_repercutido_2T;
		protected decimal _total_repercutido_3T;
		protected decimal _total_repercutido_4T;

        #endregion

        #region Properties

		public long Modelo { get { return _modelo; } }
		public long OidTitular { get { return _oid_titular; } }
		public string IDTitular { get { return _id_titular; } }
		public string VatNumberTitular { get { return _vat_number_titular; } }
		public string Titular { get { return _titular; } }
		public long TipoTitular { get { return _tipo_titular; } }

		public Decimal Base { get { return _base; } }
		public Decimal Base1T { get { return _base_1T; } }
		public Decimal Base2T { get { return _base_2T; } }
		public Decimal Base3T { get { return _base_3T; } }
		public Decimal Base4T { get { return _base_4T; } }

		public Decimal Total { get { return _total; } }
		public Decimal Total1T { get { return _total_1T; } }
		public Decimal Total2T { get { return _total_2T; } }
		public Decimal Total3T { get { return _total_3T; } }
		public Decimal Total4T { get { return _total_4T; } }

		public Decimal TotalEfectivo { get { return _total_efectivo; } }
		public Decimal TotalEfectivo1T { get { return _total_efectivo_1T; } }
		public Decimal TotalEfectivo2T { get { return _total_efectivo_2T; } }
		public Decimal TotalEfectivo3T { get { return _total_efectivo_3T; } }
		public Decimal TotalEfectivo4T { get { return _total_efectivo_4T; } }
		public EModelo EModelo { get { return (EModelo)_modelo; } }

		public Decimal TotalSoportado { get { return _total_soportado; } }
		public Decimal TotalSoportado1T { get { return _total_soportado_1T; } }
		public Decimal TotalSoportado2T { get { return _total_soportado_2T; } }
		public Decimal TotalSoportado3T { get { return _total_soportado_3T; } }
		public Decimal TotalSoportado4T { get { return _total_soportado_4T; } }

		public Decimal TotalSoportadoImportacion { get { return _total_soportado_importacion; } }
		public Decimal TotalSoportadoImportacion1T { get { return _total_soportado_importacion_1T; } }
		public Decimal TotalSoportadoImportacion2T { get { return _total_soportado_importacion_2T; } }
		public Decimal TotalSoportadoImportacion3T { get { return _total_soportado_importacion_3T; } }
		public Decimal TotalSoportadoImportacion4T { get { return _total_soportado_importacion_4T; } }

		public Decimal TotalRepercutido { get { return _total_repercutido; } }
		public Decimal TotalRepercutido1T { get { return _total_repercutido_1T; } }
		public Decimal TotalRepercutido2T { get { return _total_repercutido_2T; } }
		public Decimal TotalRepercutido3T { get { return _total_repercutido_3T; } }
		public Decimal TotalRepercutido4T { get { return _total_repercutido_4T; } }

		public ETipoTitular ETipoTitular { get { return (ETipoTitular)_tipo_titular; } }
		public string TipoTitularLabel { get { return Library.Common.EnumText<ETipoTitular>.GetLabel(ETipoTitular); } }

        #endregion

        #region Business Methods

		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

			string field = string.Empty;

			_modelo = Format.DataReader.GetInt64(source, "MODELO");

			switch (EModelo)
			{
				case EModelo.Modelo347:
					{
						long tipo_entidad = Format.DataReader.GetInt64(source, "TIPO_ENTIDAD");

						string oid = ((long)(tipo_entidad + 1)).ToString("00") + "00000" + Format.DataReader.GetInt64(source, "OID").ToString();
						Oid = Convert.ToInt64(oid);

						_tipo_titular = Format.DataReader.GetInt64(source, "TIPO_TITULAR");
						_id_titular = Format.DataReader.GetString(source, "ID_TITULAR");
						_vat_number_titular = Format.DataReader.GetString(source, "VAT_NUMBER");
						_titular = Format.DataReader.GetString(source, "TITULAR");

						field = "TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Anual).ToUpper();
						_total = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
						_total_1T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
						_total_2T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
						_total_3T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
						_total_4T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Anual).ToUpper();
						_total_efectivo = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
						_total_efectivo_1T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
						_total_efectivo_2T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
						_total_efectivo_3T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
						_total_efectivo_4T = Format.DataReader.GetDecimal(source, field);
					}
					break;

				case EModelo.Modelo420:
					{
						long tipo_entidad = Format.DataReader.GetInt64(source, "TIPO_ENTIDAD");

						string oid = ((long)(tipo_entidad + 1)).ToString("00") + "00000" + Format.DataReader.GetInt64(source, "OID").ToString();
						Oid = Convert.ToInt64(oid);

						field = "TOTAL_SOPORTADO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
						_total_soportado_1T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_SOPORTADO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
						_total_soportado_2T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_SOPORTADO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
						_total_soportado_3T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_SOPORTADO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
						_total_soportado_4T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_SOPORTADO_IMPORTACION_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
						_total_soportado_importacion_1T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_SOPORTADO_IMPORTACION_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
						_total_soportado_importacion_2T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_SOPORTADO_IMPORTACION_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
						_total_soportado_importacion_3T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_SOPORTADO_IMPORTACION_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
						_total_soportado_importacion_4T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_REPERCUTIDO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
						_total_repercutido_1T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_REPERCUTIDO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
						_total_repercutido_2T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_REPERCUTIDO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
						_total_repercutido_3T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_REPERCUTIDO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
						_total_repercutido_4T = Format.DataReader.GetDecimal(source, field);

						_total_soportado = _total_soportado_1T + _total_soportado_2T + _total_soportado_3T + _total_soportado_4T;
						_total_soportado_importacion = _total_soportado_importacion_1T + _total_soportado_importacion_2T + _total_soportado_importacion_3T + _total_soportado_importacion_4T;
						_total_repercutido = _total_repercutido_1T + _total_repercutido_2T + _total_repercutido_3T + _total_repercutido_4T;
					}
					break;

				case EModelo.Modelo111:
					{
						long tipo_entidad = Format.DataReader.GetInt64(source, "TIPO_ENTIDAD");

						string oid = ((long)(tipo_entidad + 1)).ToString("00") + "00000" + Format.DataReader.GetInt64(source, "OID");
						Oid = Convert.ToInt64(oid);

						field = "BASE_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
						_base_1T = Format.DataReader.GetDecimal(source, field);

						field = "BASE_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
						_base_2T = Format.DataReader.GetDecimal(source, field);

						field = "BASE_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
						_base_3T = Format.DataReader.GetDecimal(source, field);

						field = "BASE_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
						_base_4T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
						_total_1T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
						_total_2T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
						_total_3T = Format.DataReader.GetDecimal(source, field);

						field = "TOTAL_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
						_total_4T = Format.DataReader.GetDecimal(source, field);

						_total = _total_1T + _total_2T + _total_3T + _total_4T;
						_base = _base_1T + _base_2T + _base_3T + _base_4T;						
					}
					break;
			}
		}

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
        public ModeloInfo() { /* require use of factory methods */ }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> origen de los datos</param>
        /// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
        private ModeloInfo(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }
       
        public static ModeloInfo GetChild(IDataReader reader) { return GetChild(reader, false); }
        public static ModeloInfo GetChild(IDataReader reader, bool childs)
        {
            return new ModeloInfo(reader, childs);
        }

        #endregion

        #region Root Factory Methods

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Oid = 0;
            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        CopyValues(reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
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
                CopyValues(source);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

        #region SQL

		public static Common.SelectCaller _local_caller = new Common.SelectCaller(SELECT_BASE_COMMON);

		internal static string SELECT_FIELDS_MODELO(Common.QueryConditions conditions)
		{
			string query = string.Empty;

			query = @"
			SELECT DISTINCT " + 
				(long)conditions.Modelo.EModelo + @" AS ""MODELO""
				," + (long)conditions.TipoEntidad + @" AS ""TIPO_ENTIDAD""";

			switch (conditions.Modelo.EModelo)
			{
				case EModelo.Modelo347:

					query += SELECT_FIELDS_MODELO347(conditions);

					break;

				case EModelo.Modelo420:

					query += SELECT_FIELDS_MODELO420(conditions);

					break;

				case EModelo.Modelo111:

					query += SELECT_FIELDS_MODELO111(conditions);

					break;
			}

			return query;
		}

		internal static string SELECT_FIELDS_MODELO347(Common.QueryConditions conditions)
		{
			string query = string.Empty;

			switch (conditions.TipoEntidad)
			{
				case ETipoEntidad.Cliente:
					query +=
							"		," + (long)ETipoTitular.Cliente + " AS \"TIPO_TITULAR\"" +
							"		,F.\"OID_CLIENTE\" AS \"OID\"" +
							"       ,CL.\"CODIGO\" AS \"ID_TITULAR\"" +
							"       ,CL.\"NOMBRE\" AS \"TITULAR\"" +
							"       ,CL.\"VAT_NUMBER\" AS \"VAT_NUMBER\"";

					break;

				case ETipoEntidad.Acreedor:
				case ETipoEntidad.Proveedor:
				case ETipoEntidad.Despachante:
				case ETipoEntidad.Naviera:
				case ETipoEntidad.TransportistaOrigen:
				case ETipoEntidad.TransportistaDestino:
					query +=
							"		," + (long)ETipoTitular.Acreedor + " AS \"TIPO_TITULAR\"" +
							"		,F.\"OID_ACREEDOR\" AS \"OID\"" +
							"       ,A.\"CODIGO\" AS \"ID_TITULAR\"" +
							"       ,A.\"NOMBRE\" AS \"TITULAR\"" +
							"       ,A.\"ID\" AS \"VAT_NUMBER\"";
					break;
			}

			string totalField = string.Empty;

			totalField = "TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Anual).ToUpper();
			query += "       ,COALESCE(\"" + totalField + "\", 0) AS \"" + totalField + "\"";

			totalField = "TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
			query += "       ,COALESCE(\"" + totalField + "\", 0) AS \"" + totalField + "\"";

			totalField = "TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
			query += "       ,COALESCE(\"" + totalField + "\", 0) AS \"" + totalField + "\"";

			totalField = "TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
			query += "       ,COALESCE(\"" + totalField + "\", 0) AS \"" + totalField + "\"";

			totalField = "TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
			query += "       ,COALESCE(\"" + totalField + "\", 0) AS \"" + totalField + "\"";

			totalField = "TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Anual).ToUpper();
			query += "       ,COALESCE(\"" + totalField + "\", 0) AS \"" + totalField + "\"";

			totalField = "TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
			query += "       ,COALESCE(\"" + totalField + "\", 0) AS \"" + totalField + "\"";

			totalField = "TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
			query += "       ,COALESCE(\"" + totalField + "\", 0) AS \"" + totalField + "\"";

			totalField = "TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
			query += "       ,COALESCE(\"" + totalField + "\", 0) AS \"" + totalField + "\"";

			totalField = "TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
			query += "       ,COALESCE(\"" + totalField + "\", 0) AS \"" + totalField + "\"";

			return query;
		}

		internal static string SELECT_FIELDS_MODELO420(Common.QueryConditions conditions)
		{
			string query = string.Empty;
			string totalField = string.Empty;

			totalField = @"TOTAL_SOPORTADO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			totalField = @"TOTAL_SOPORTADO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			totalField = @"TOTAL_SOPORTADO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			totalField = @"TOTAL_SOPORTADO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			totalField = @"TOTAL_SOPORTADO_IMPORTACION_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			totalField = @"TOTAL_SOPORTADO_IMPORTACION_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			totalField = @"TOTAL_SOPORTADO_IMPORTACION_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			totalField = @"TOTAL_SOPORTADO_IMPORTACION_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			totalField = @"TOTAL_REPERCUTIDO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			totalField = @"TOTAL_REPERCUTIDO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			totalField = @"TOTAL_REPERCUTIDO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			totalField = @"TOTAL_REPERCUTIDO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
			query += @"
				,SUM(COALESCE(""" + totalField + @""", 0)) AS """ + totalField + @"""";

			return query;
		}

		internal static string SELECT_FIELDS_MODELO111(Common.QueryConditions conditions)
		{
			string query = string.Empty;
			string sufix = string.Empty;

			sufix = Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo1T).ToUpper();
			query += @"
				,SUM(COALESCE(""TOTAL_" + sufix + @""", 0)) AS ""TOTAL_" + sufix + @"""
				,SUM(COALESCE(""BASE_" + sufix + @""", 0)) AS ""BASE_" + sufix + @"""";

			sufix = Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo2T).ToUpper();
			query += @"
				,SUM(COALESCE(""TOTAL_" + sufix + @""", 0)) AS ""TOTAL_" + sufix + @"""
				,SUM(COALESCE(""BASE_" + sufix + @""", 0)) AS ""BASE_" + sufix + @"""";

			sufix = Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo3T).ToUpper();
			query += @"
				,SUM(COALESCE(""TOTAL_" + sufix + @""", 0)) AS ""TOTAL_" + sufix + @"""
				,SUM(COALESCE(""BASE_" + sufix + @""", 0)) AS ""BASE_" + sufix + @"""";

			sufix = Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Periodo4T).ToUpper();
			query += @"
				,SUM(COALESCE(""TOTAL_" + sufix + @""", 0)) AS ""TOTAL_" + sufix + @"""
				,SUM(COALESCE(""BASE_" + sufix + @""", 0)) AS ""BASE_" + sufix + @"""";

			return query;
		}

		internal static void GetPeriodo(Common.QueryConditions conditions, EPeriodo period)
		{
			switch (period)
			{
				case EPeriodo.Anual:

					conditions.FechaIni = DateAndTime.FirstDay(conditions.Year);
					conditions.FechaFin = DateAndTime.LastDay(conditions.Year);
					break;

				case EPeriodo.Periodo1T:

					conditions.FechaIni = DateAndTime.FirstDay(1, conditions.Year);
					conditions.FechaFin = DateAndTime.LastDay(3, conditions.Year);
					break;

				case EPeriodo.Periodo2T:

					conditions.FechaIni = DateAndTime.FirstDay(4, conditions.Year);
					conditions.FechaFin = DateAndTime.LastDay(6, conditions.Year);
					break;

				case EPeriodo.Periodo3T:

					conditions.FechaIni = DateAndTime.FirstDay(7, conditions.Year);
					conditions.FechaFin = DateAndTime.LastDay(9, conditions.Year);
					break;

				case EPeriodo.Periodo4T:

					conditions.FechaIni = DateAndTime.FirstDay(10, conditions.Year);
					conditions.FechaFin = DateAndTime.LastDay(12, conditions.Year);
					break;
			}
		}

		internal static string WHERE(Common.QueryConditions conditions)
		{
			string query = string.Empty;

			switch (conditions.Modelo.EModelo)
			{
				case EModelo.Modelo347:
					{
						string totalField = @"TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Anual).ToUpper();
						string totalEfectivoField = @"TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(EPeriodo.Anual).ToUpper();

						query += @"
						WHERE TRUE ";

						if (conditions.Modelo != null)
						{
							if (conditions.Modelo.MinImporte != 0)
								query += @"
									AND """ + totalField + @""" > " + conditions.Modelo.MinImporte;

							if (conditions.Modelo.MinEfectivo != 0)
								query += @"
									AND """ + totalEfectivoField + @""" > " + conditions.Modelo.MinEfectivo;
						}
					}
					break;
			}

			return query;
		}

		internal static string INNER_347(Common.QueryConditions conditions, EPeriodo period)
		{
			string fc = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));
			string cr = nHManager.Instance.GetSQLTable(typeof(ChargeRecord));
			string cf = nHManager.Instance.GetSQLTable(typeof(ChargeOperationRecord));

			GetPeriodo(conditions, period);

			string query = string.Empty;
			string totalField = "TOTAL_OPERACIONES_" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();
			string totalEfectivoField = "TOTAL_OPERACIONES_EFECTIVO_" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();

			switch (conditions.TipoEntidad)
			{
				case ETipoEntidad.Cliente:
					{
						string alias = "FC" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();

						query = @"
                        LEFT JOIN (SELECT ""OID_CLIENTE"", SUM(""TOTAL"") AS """ + totalField + @"""
                        			FROM " + fc + @" AS FC
                        			WHERE (""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"')
                        			GROUP BY ""OID_CLIENTE"")
                            AS " + alias + @"1 ON " + alias + @"1.""OID_CLIENTE"" = F.""OID_CLIENTE""
                        LEFT JOIN (SELECT CR.""OID_CLIENTE"", SUM(""CANTIDAD"") AS """ + totalEfectivoField + @"""
                        			FROM " + cf + @" AS CF
                        			INNER JOIN " + cr + @" AS CR ON CR.""OID"" = CF.""OID_COBRO"" AND CR.""MEDIO_PAGO"" = " + (long)(EMedioPago.Efectivo) + @"
                        			WHERE (CR.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"')
                                        AND CR.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        			GROUP BY CR.""OID_CLIENTE"")
                        	AS " + alias + "2 ON " + alias + @"2.""OID_CLIENTE"" = F.""OID_CLIENTE""";
					}
					break;

				case ETipoEntidad.Acreedor:
				case ETipoEntidad.Proveedor:
				case ETipoEntidad.Despachante:
				case ETipoEntidad.Naviera:
				case ETipoEntidad.TransportistaOrigen:
				case ETipoEntidad.TransportistaDestino:
					{
						string fp = nHManager.Instance.GetSQLTable(typeof(InputInvoiceRecord));
						string pg = nHManager.Instance.GetSQLTable(typeof(PaymentRecord));
						string pf = nHManager.Instance.GetSQLTable(typeof(TransactionPaymentRecord));

						ETipoAcreedor tipoAcreedor = Library.Store.EnumConvert.ToETipoAcreedor(conditions.TipoEntidad);
						string alias = "FP" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();

						query = @"
                        LEFT JOIN (SELECT FP.""OID_ACREEDOR"", SUM(""TOTAL"") AS """ + totalField + @"""
                        			FROM " + fp + @" AS FP
                        			WHERE (""FECHA"" BETWEEN '" + conditions.FechaIniLabel + @"' AND '" + conditions.FechaFinLabel + @"')
                        				AND FP.""TIPO_ACREEDOR"" = " + (long)tipoAcreedor + @"
                                        AND FP.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        			GROUP BY FP.""OID_ACREEDOR"", FP.""TIPO_ACREEDOR"")
                            AS " + alias + @"1 ON " + alias + @"1.""OID_ACREEDOR"" = F.""OID_ACREEDOR"" AND F.""TIPO_ACREEDOR"" = " + (long)tipoAcreedor + @"
                        LEFT JOIN (SELECT PG.""OID_AGENTE"" AS ""OID_ACREEDOR"", SUM(""CANTIDAD"") AS """ + totalEfectivoField + @"""
                        			FROM " + pf + @" AS PF
                        			INNER JOIN " + pg + @" AS PG ON PG.""OID"" = PF.""OID_PAGO"" AND PF.""TIPO_PAGO"" = " + (long)ETipoPago.Factura + @" AND PG.""MEDIO_PAGO"" = " + (long)EMedioPago.Efectivo + @"
                        			WHERE (PG.""FECHA"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + @"')
                        				AND PG.""TIPO_AGENTE"" = " + (long)tipoAcreedor + @"
                                        AND PG.""ESTADO"" != " + (long)EEstado.Anulado + @"
                        			GROUP BY PG.""OID_AGENTE"", PG.""TIPO_AGENTE"")
                            AS " + alias + "2 ON " + alias + @"2.""OID_ACREEDOR"" = F.""OID_ACREEDOR"" AND F.""TIPO_ACREEDOR"" = " + (long)tipoAcreedor;
					}
					break;
			}

			return query;
		}

		internal static string INNER_420(QueryConditions conditions, EPeriodo period)
		{
			Library.Common.QueryConditions conds = QueryConditions.ConvertTo(conditions);
			GetPeriodo(conds, period);

			string query = string.Empty;

			string totalSoportadoField = "TOTAL_SOPORTADO_" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();
			string totalImportacionField = "TOTAL_SOPORTADO_IMPORTACION_" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();
			string totalRepercutidoField = "TOTAL_REPERCUTIDO_" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();

			switch (conditions.Modelo.ETipoModelo)
			{
				case ETipoModelo.Soportado:
					{
					string fr = nHManager.Instance.GetSQLTable(typeof(InputInvoiceRecord));
					string cf = nHManager.Instance.GetSQLTable(typeof(InputInvoiceLineRecord));

					string aliasFC = "FR" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();
					string aliasCF = "CC" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();

					query = @"
					LEFT JOIN (SELECT ""IMPUESTOS"" AS """ + totalSoportadoField + @""", 0 AS """ + totalRepercutidoField + @"""
										,FR.""OID"" AS ""OID""
								FROM " + fr + @" AS FR
								WHERE (FR.""FECHA"" BETWEEN '" + conds.FechaIniLabel + @"' AND '" + conds.FechaFinLabel + @"'))
						AS " + aliasFC + @"1 ON " + aliasFC + @"1.""OID"" = F.""OID""
					LEFT JOIN (SELECT SUM(CF.""TOTAL"") AS """ + totalImportacionField + @"""
										,CF.""OID_FACTURA"" AS ""OID_FACTURA""
								FROM " + cf + @" AS CF
								INNER JOIN " + fr + @" AS FR ON FR.""OID"" = CF.""OID_FACTURA""
								WHERE (FR.""FECHA"" BETWEEN '" + conds.FechaIniLabel + @"' AND '" + conds.FechaFinLabel + @"')
									AND CF.""OID_PRODUCTO"" = " + conditions.Producto.Oid + @"
								GROUP BY CF.""OID_FACTURA"")
						AS " + aliasCF + @"2 ON " + aliasCF + @"2.""OID_FACTURA"" = F.""OID""";
					}
					break;

				case ETipoModelo.Repercutido:
					{
						string fe = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));

						string alias = @"FC" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();

						query = @"
						LEFT JOIN (SELECT ""P_IGIC"" AS """ + totalRepercutidoField + @""", 0 AS """ + totalSoportadoField + @""", 0 AS """ + totalImportacionField + @"""
											,FE.""OID"" AS ""OID""
									FROM " + fe + @" AS FE
									WHERE (FE.""FECHA"" BETWEEN '" + conds.FechaIniLabel + @"' AND '" + conds.FechaFinLabel + @"'))
							AS " + alias + "1 ON " + alias + @"1.""OID"" = F.""OID""";
					}
					break;
			}

			return query;
		}

		internal static string INNER_111(QueryConditions conditions, EPeriodo period)
		{
			Library.Common.QueryConditions conds = QueryConditions.ConvertTo(conditions);
			GetPeriodo(conds, period);

			string query = string.Empty;

			string sufix = Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();

			switch (conditions.Modelo.ETipoModelo)
			{
				case ETipoModelo.Profesionales:
					{
						string fr = nHManager.Instance.GetSQLTable(typeof(InputInvoiceRecord));

						string alias = "MD" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();

						query = @"
							LEFT JOIN (SELECT (FR.""BASE_IMPONIBLE"" * FR.""P_IRPF"" / 100) AS ""TOTAL_" + sufix + @"""
												,FR.""BASE_IMPONIBLE"" AS ""BASE_" + sufix + @"""
												,FR.""OID"" AS ""OID""
										FROM " + fr + @" AS FR
										WHERE (FR.""FECHA"" BETWEEN '" + conds.FechaIniLabel + "' AND '" + conds.FechaFinLabel + @"')
											AND FR.""P_IRPF"" != 0)
								AS " + alias + " ON " + alias + @".""OID"" = F.""OID""";
					}
					break;

				case ETipoModelo.EmpleadosTrabajo:
					{
						string nm = nHManager.Instance.GetSQLTable(typeof(PayrollRecord));
						string rn = nHManager.Instance.GetSQLTable(typeof(PayrollBatchRecord));

						string alias = "FR" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();

						query = @"
							LEFT JOIN (SELECT ((NM.""BASE_IRPF"" - NM.""DESCUENTOS"") * NM.""P_IRPF"" / 100) AS ""TOTAL_" + sufix + @"""
												,(NM.""BASE_IRPF"" - NM.""DESCUENTOS"") AS ""BASE_" + sufix  + @"""
												,NM.""OID"" AS ""OID""
										FROM " + nm + @" AS NM
										INNER JOIN " + rn + @" AS RN ON RN.""OID"" = NM.""OID_REMESA""
										WHERE (RN.""FECHA"" BETWEEN '" + conds.FechaIniLabel + @"' AND '" + conds.FechaFinLabel + @"'))
								AS " + alias + @" ON " + alias + @".""OID"" = F.""OID""";
						}
					break;

				case ETipoModelo.EmpleadosEspecie:
					{
						string nm = nHManager.Instance.GetSQLTable(typeof(PayrollRecord));
						string rn = nHManager.Instance.GetSQLTable(typeof(PayrollBatchRecord));

						string alias = "FR" + Common.EnumText<EPeriodo>.GetLabel(period).ToUpper();

						query = @"
							LEFT JOIN (SELECT (NM.""DESCUENTOS"" * NM.""P_IRPF"" / 100) AS ""TOTAL_" + sufix + @"""
												,NM.""DESCUENTOS"" AS ""BASE_" + sufix + @"""
												,NM.""OID"" AS ""OID""
										FROM " + nm + @" AS NM
										INNER JOIN " + rn + @" AS RN ON RN.""OID"" = NM.""OID_REMESA""
										WHERE (RN.""FECHA"" BETWEEN '" + conds.FechaIniLabel + @"' AND '" + conds.FechaFinLabel + @"'))
								AS " + alias + " ON " + alias + @".""OID"" = F.""OID""";
					}
					break;
			}

			return query;
		}

		internal static string SELECT_BASE_COMMON(Common.QueryConditions conditions)
		{
			string query = string.Empty;
			
			switch (conditions.Modelo.EModelo)
			{
				case EModelo.Modelo347:
					query = SELECT_BASE_MODELO347(conditions);
					break;
			}

			return query;
		}

		internal static string SELECT_BASE_INVOICE(QueryConditions conditions)
		{
			string query = string.Empty;

			switch (conditions.Modelo.EModelo)
			{
				case EModelo.Modelo420:
					query = SELECT_BASE_MODELO420(conditions);
					break;

				case EModelo.Modelo111:
					query = SELECT_BASE_MODELO111(conditions);
					break;
			}

			return query;
		}

		internal static string SELECT_BASE_MODELO347(Common.QueryConditions conditions)
		{
			string query = string.Empty;

				switch (conditions.TipoEntidad)
				{
					case ETipoEntidad.Cliente:
						{
							string fc = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
							string cl = nHManager.Instance.GetSQLTable(typeof(ClientRecord));

							query = 
                            SELECT_FIELDS_MODELO(conditions) + @"
							FROM " + fc + @" AS F
							INNER JOIN " + cl + @" AS CL ON CL.""OID"" = F.""OID_CLIENTE""" +
							INNER_347(conditions, EPeriodo.Anual) +
							INNER_347(conditions, EPeriodo.Periodo1T) +
							INNER_347(conditions, EPeriodo.Periodo2T) +
							INNER_347(conditions, EPeriodo.Periodo3T) +
							INNER_347(conditions, EPeriodo.Periodo4T) +
                            WHERE(conditions);
						}
						break;

					case ETipoEntidad.Acreedor:
					case ETipoEntidad.Proveedor:
					case ETipoEntidad.Despachante:
					case ETipoEntidad.Naviera:
					case ETipoEntidad.TransportistaOrigen:
					case ETipoEntidad.TransportistaDestino:
						{
							string fp = nHManager.Instance.GetSQLTable(typeof(InputInvoiceRecord));

							ETipoAcreedor tipoAcreedor = Library.Store.EnumConvert.ToETipoAcreedor(conditions.TipoEntidad);

							query = 
                            SELECT_FIELDS_MODELO(conditions) + @"
							FROM " + fp + @" AS F" +
							InputInvoiceSQL.JOIN_ACREEDOR(tipoAcreedor) +
							INNER_347(conditions, EPeriodo.Anual) +
							INNER_347(conditions, EPeriodo.Periodo1T) +
							INNER_347(conditions, EPeriodo.Periodo2T) +
							INNER_347(conditions, EPeriodo.Periodo3T) +
							INNER_347(conditions, EPeriodo.Periodo4T) +
                            WHERE(conditions);
						}
						break;
				}

			return query;
		}

		internal static string SELECT_BASE_MODELO420(QueryConditions conditions)
		{
			string query = string.Empty;

			string fe = nHManager.Instance.GetSQLTable(typeof(OutputInvoiceRecord));
			string fr = nHManager.Instance.GetSQLTable(typeof(InputInvoiceRecord));

			string f = (conditions.Modelo.ETipoModelo == ETipoModelo.Soportado) ? fr : fe;

			query = 
			SELECT_FIELDS_MODELO(QueryConditions.ConvertTo(conditions)) + @"
			FROM " + f + @" AS F" +
			INNER_420(conditions, EPeriodo.Periodo1T) +
			INNER_420(conditions, EPeriodo.Periodo2T) +
			INNER_420(conditions, EPeriodo.Periodo3T) +
			INNER_420(conditions, EPeriodo.Periodo4T);

			query += WHERE(QueryConditions.ConvertTo(conditions));

			return query;
		}

		internal static string SELECT_BASE_MODELO111(QueryConditions conditions)
		{
			string query = string.Empty;

			query = SELECT_FIELDS_MODELO(QueryConditions.ConvertTo(conditions));

			switch (conditions.Modelo.ETipoModelo)
			{
				case ETipoModelo.Profesionales:
					{
						string fr = nHManager.Instance.GetSQLTable(typeof(InputInvoiceRecord));

						query += @"
							FROM " + fr + " AS F";
					}
					break;

				case ETipoModelo.EmpleadosEspecie:
				case ETipoModelo.EmpleadosTrabajo:
					{
						string nm = nHManager.Instance.GetSQLTable(typeof(PayrollRecord));
						query += @"
							FROM " + nm + " AS F";
					}
					break;
			}

			query +=
				INNER_111(conditions, EPeriodo.Periodo1T) +
				INNER_111(conditions, EPeriodo.Periodo2T) +
				INNER_111(conditions, EPeriodo.Periodo3T) +
				INNER_111(conditions, EPeriodo.Periodo4T);

			query += WHERE(QueryConditions.ConvertTo(conditions));

			return query;
		}

		internal static string SELECT(Common.QueryConditions conditions)
		{
			string query = string.Empty;

			query = SELECT_BUILDER(_local_caller, conditions);

			query += @" 
			ORDER BY ""ID_TITULAR""";

			return query;
		}

		internal static string SELECT(QueryConditions conditions)
		{
			string query = string.Empty;

			query = SELECT_BASE_INVOICE(conditions);

			return query;
		}

		internal static string SELECT_BUILDER(Common.SelectCaller localCaller, Common.QueryConditions conditions)
		{
			return Common.EntityBase.SELECT_BUILDER(localCaller, conditions);
		}

        #endregion
    }
}
