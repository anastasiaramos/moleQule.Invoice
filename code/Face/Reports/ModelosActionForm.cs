using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Modelo;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Invoice;

namespace moleQule.Face.Invoice
{
    public partial class ModelosActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

		public const string ID = "ModelosActionForm";
		public static Type Type { get { return typeof(ModelosActionForm); } }

        private EModelo _modelo = EModelo.Modelo347;
		private EPeriodo _periodo = EPeriodo.Anual;
		private Modelo _t_modelo = new Modelo();
		
        #endregion

        #region Factory Methods

        public ModelosActionForm () 
            : this(null) {}

		public ModelosActionForm(Form parent)
			: base(true, parent)
        {
            this.InitializeComponent();
            base.SetFormData();
        }

        #endregion

        #region Layout & Source

		public override void RefreshSecondaryData()
		{
			EModelo[] list = { EModelo.Modelo347, EModelo.Modelo420, EModelo.Modelo111 };
			Datos_Modelos.DataSource = Library.Common.EnumText<EModelo>.GetList(list);
			Modelo_CB.SelectedValue = (long)EModelo.Modelo347;
			_modelo = (EModelo)((long)Modelo_CB.SelectedValue);
			PgMng.Grow();

			Datos_Periodo.DataSource = Library.Common.EnumText<EPeriodo>.GetList(false);
			Periodo_CB.SelectedValue = (long)EPeriodo.Anual;

			FInicial_DTP.Checked = true;
			FFinal_DTP.Checked = true;

			FInicial_DTP.Value = DateAndTime.FirstDay(DateTime.Today.Year);
			FFinal_DTP.Value = DateAndTime.LastDay(DateTime.Today.Year);

			AnoActivo_DTP.Value = DateTime.Today;
		}

        #endregion

        #region Business Methods

        private bool GetSettings()
        {
			_t_modelo.MinImporte = 0;
			_t_modelo.MinEfectivo = 0;

			switch (_modelo)
			{ 
				case EModelo.Modelo347:
					_t_modelo.EModelo = EModelo.Modelo347;
					try { _t_modelo.MinImporte = Convert.ToDecimal(MinImporte_TB.Text); } catch {}
					try { _t_modelo.MinEfectivo = Convert.ToDecimal(MinEfectivo_TB.Text); } catch {}
					break;

				case EModelo.Modelo420:
					_t_modelo.EModelo = EModelo.Modelo420;
					break;

				case EModelo.Modelo111:
					_t_modelo.EModelo = EModelo.Modelo111;
					break;
			}

			return true;
        }

		private string GetFilterValues()
		{
			string filtro = string.Empty;

			if (FInicial_DTP.Checked)
				filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.GreaterOrEqual) + " " + FInicial_DTP.Value.ToShortDateString() + "; ";

			if (FFinal_DTP.Checked)
				filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.LessOrEqual) + " " + FFinal_DTP.Value.ToShortDateString() + "; ";

			return filtro;
		}

		private string GetTitle()
		{
			string title = string.Empty;

			title += "Modelo: " + Library.Common.EnumText<EModelo>.GetLabel(_modelo);

			return title;
		}

		protected void UpdateFechas()
		{ 
			switch (_periodo)
			{
				case EPeriodo.Anual:

					FInicial_DTP.Value = DateAndTime.FirstDay(AnoActivo_DTP.Value.Year);
					FFinal_DTP.Value = DateAndTime.LastDay(AnoActivo_DTP.Value.Year);
					break;

				case EPeriodo.Periodo1T:

					FInicial_DTP.Value = DateAndTime.FirstDay(1, AnoActivo_DTP.Value.Year);
					FFinal_DTP.Value = DateAndTime.LastDay(3, AnoActivo_DTP.Value.Year);
					break;

				case EPeriodo.Periodo2T:

					FInicial_DTP.Value = DateAndTime.FirstDay(4, AnoActivo_DTP.Value.Year);
					FFinal_DTP.Value = DateAndTime.LastDay(6, AnoActivo_DTP.Value.Year);
					break;

				case EPeriodo.Periodo3T:
										
					FInicial_DTP.Value = DateAndTime.FirstDay(7, AnoActivo_DTP.Value.Year);
					FFinal_DTP.Value = DateAndTime.LastDay(9, AnoActivo_DTP.Value.Year);
					break;

				case EPeriodo.Periodo4T:
										
					FInicial_DTP.Value = DateAndTime.FirstDay(10, AnoActivo_DTP.Value.Year);
					FFinal_DTP.Value = DateAndTime.LastDay(12, AnoActivo_DTP.Value.Year);
					break;
			}
			
		}

        #endregion

        #region Actions

        public void DoSubmit() { SubmitAction(); }

        protected override void SubmitAction()
        {
			if (!GetSettings())
			{
				_action_result = DialogResult.Ignore;
				return;
			}

			PgMng.Reset(3, 1, Face.Resources.Messages.RETRIEVING_DATA, this);
			Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions();
			conditions.Year = AnoActivo_DTP.Value.Year;
			conditions.FechaIni = FInicial_DTP.Value;
            conditions.FechaFin = FFinal_DTP.Value;
			conditions.Modelo = _t_modelo;

			string title = GetTitle();
			string filtro = GetFilterValues();

            try
            {
				switch (_modelo)
				{ 				
					case EModelo.Modelo420:
						{
							conditions.Producto = ProductInfo.New();
							conditions.Producto.Oid = Library.Invoice.ModulePrincipal.GetImpuestosImportacion();

							conditions.Modelo.ETipoModelo= ETipoModelo.Soportado;
							ModeloList soportado = ModeloList.GetList(conditions, false);

							conditions.Modelo.ETipoModelo = ETipoModelo.Repercutido;
							ModeloList repercutido = ModeloList.GetList(conditions, false);

							PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

							CommonReportMng rptMng = new CommonReportMng(AppContext.ActiveSchema, title, filtro);

							Modelo420Rpt report = rptMng.GetModelo420Report(soportado, repercutido);

							PgMng.FillUp();
							ShowReport(report);
						}

						break;

					case EModelo.Modelo111:
						{
							conditions.Modelo.ETipoModelo = ETipoModelo.EmpleadosTrabajo;
							ModeloList empleados_trabajo = ModeloList.GetList(conditions, false);

							conditions.Modelo.ETipoModelo = ETipoModelo.EmpleadosEspecie;
							ModeloList empleados_especie = ModeloList.GetList(conditions, false);

							conditions.Modelo.ETipoModelo = ETipoModelo.Profesionales;
							ModeloList profesionales = ModeloList.GetList(conditions, false);

							InputInvoiceList facturas = InputInvoiceList.GetListByModelo(EModelo.Modelo111, conditions.FechaIni, conditions.FechaFin, false);

							PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

							CommonReportMng rptMng = new CommonReportMng(AppContext.ActiveSchema, title, filtro);

							Modelo111Rpt report = rptMng.GetModelo111Report(empleados_trabajo, empleados_especie, profesionales, facturas);

							PgMng.FillUp();
							ShowReport(report);
						}

						break;

					case EModelo.Modelo347:
						{

							decimal efectivo = conditions.Modelo.MinEfectivo;
							conditions.Modelo.MinEfectivo = 0;
							ModeloList f_emitidas = ModeloList.GetList(Library.Invoice.QueryConditions.ConvertTo(conditions), false);

							decimal importe = conditions.Modelo.MinImporte;
							conditions.Modelo.MinImporte = 0;
							conditions.Modelo.MinEfectivo = efectivo;
							ModeloList f_emitidas_efectivo = ModeloList.GetList(Library.Invoice.QueryConditions.ConvertTo(conditions), false);

							PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

							CommonReportMng rptMng = new CommonReportMng(AppContext.ActiveSchema, title, filtro);

							_t_modelo.MinImporte = importe;
							_t_modelo.MinEfectivo = efectivo;

							Modelo347Rpt report = rptMng.GetModelo347Report(f_emitidas, f_emitidas_efectivo, _t_modelo);

							PgMng.FillUp();
							ShowReport(report);
						}
						break;
				}	

                _action_result = DialogResult.Ignore;
            }
            catch (Exception ex)
            {
				PgMng.FillUp();
				MessageBox.Show(iQExceptionHandler.GetAllMessages(ex));							

                _action_result = DialogResult.Ignore;
            }     
        }
        
        #endregion

		#region Buttons

		#endregion

		#region Events
		
		private void Modelo_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			_modelo = (EModelo)((long)Modelo_CB.SelectedValue);

			switch (_modelo)
			{ 
				case EModelo.Modelo347:
					Settings_TC.Enabled = true;
					Settings_TC.SelectedTab = Modelo347_TP;
					break;

				case EModelo.Modelo420:
					Settings_TC.Enabled = false;
					break;
			}
		}
		
		private void Periodo_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			_periodo = (EPeriodo)((long)Periodo_CB.SelectedValue);
			UpdateFechas();
		}

		private void AnoActivo_DTP_ValueChanged(object sender, EventArgs e)
		{
			UpdateFechas();
		}

        #endregion
	}
}

