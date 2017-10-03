using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Library.Store.Reports.Expedient;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
    public partial class ControlCobrosREAActionForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 1; } }

        public const string ID = "ControlCobrosREAActionForm";
        public static Type Type { get { return typeof(ControlCobrosREAActionForm); } }

        #endregion

        #region Factory Methods

        public ControlCobrosREAActionForm()
            : this(null) { }

        public ControlCobrosREAActionForm(Form parent)
            : base(true, parent)
        {
            InitializeComponent();
            SetFormData();
        }

        #endregion

        #region Layout & Source

		public override void RefreshSecondaryData()
		{
			ETipoExpediente[] list = { ETipoExpediente.Alimentacion, ETipoExpediente.Ganado, ETipoExpediente.Maquinaria, ETipoExpediente.Todos };
			Datos_TiposExp.DataSource = Library.Store.EnumText<ETipoExpediente>.GetList(list);
			TipoExpediente_CB.SelectedItem = (long)ETipoExpediente.Todos;
			PgMng.Grow();

			TipoCobro_CB.SelectedIndex = 0;
			Ordenar_CB.SelectedIndex = 0;
		}

        #endregion

		#region Business Methods

		private string GetFilterValues()
		{
			string filtro = string.Empty;

			filtro += TipoCobro_CB.SelectedItem.ToString() + "; ";

			filtro += "Tipo Expediente " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + TipoExpediente_CB.Text + "; ";

			if (FInicial_DTP.Checked)
				filtro += "Fecha Despacho " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.GreaterOrEqual) + " " + FInicial_DTP.Value.ToShortDateString() + "; ";

			if (FFinal_DTP.Checked)
				filtro += "Fecha Despacho " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.LessOrEqual) + " " + FFinal_DTP.Value.ToShortDateString() + "; ";

			if (PagoInicial_DTP.Checked)
				filtro += "Fecha Pago " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.GreaterOrEqual) + " " + PagoInicial_DTP.Value.ToShortDateString() + "; ";

			if (PagoFinal_DTP.Checked)
				filtro += "Fecha Pago " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.LessOrEqual) + " " + PagoFinal_DTP.Value.ToShortDateString() + "; ";

			return filtro;
		}

		#endregion

		#region Actions

        protected override void PrintAction()
        {
            PgMng.Reset(4, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			try
			{
				PgMng.Grow();

				Library.Store.QueryConditions conditions = new Library.Store.QueryConditions
				{
					TipoExpediente = (ETipoExpediente)(long)TipoExpediente_CB.SelectedValue,
					FechaIni = (FInicial_DTP.Checked) ? FInicial_DTP.Value : DateTime.MinValue,
					FechaFin = (FFinal_DTP.Checked) ? FFinal_DTP.Value : DateTime.MaxValue,
					FechaAuxIni = (PagoInicial_DTP.Checked) ? PagoInicial_DTP.Value : DateTime.MinValue,
					FechaAuxFin = (PagoFinal_DTP.Checked) ? PagoFinal_DTP.Value : DateTime.MaxValue,
				};

				switch (TipoCobro_CB.SelectedItem.ToString())
				{
					default:
					case "Todos los expedientes":
						conditions.Estado = EEstado.Todos;
						break;
					case "Solo expedientes cobrados":
						conditions.Estado = EEstado.Pagado;
						break;
					case "Solo expedientes pendientes de cobro":
						conditions.Estado = EEstado.Pendiente;
						break;
				}

				ReportFormat _format = new ReportFormat();

				_format.CampoOrdenacion = Ordenar_CB.SelectedItem.ToString();
				_format.Orden = (Ascendente_RB.Checked) ? CrystalDecisions.Shared.SortDirection.AscendingOrder
																	: CrystalDecisions.Shared.SortDirection.DescendingOrder;

				ExpedienteREAList expedientes = ExpedienteREAList.GetListByREA(conditions, false);
				PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

				ExpedientReportMng reportMng = new ExpedientReportMng(AppContext.ActiveSchema, this.Text, GetFilterValues());
				InformeControlCobrosREARpt report = reportMng.GetControlCobrosREAReport(expedientes, _format);
				PgMng.FillUp();

				ShowReport(report);

				_action_result = DialogResult.Ignore;
			}
			catch (Exception ex)
			{
				PgMng.FillUp();
				throw ex;
			}				
        }

        #endregion

        #region Events

        #endregion
    }
}

