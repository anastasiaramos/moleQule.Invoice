using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Common;
using moleQule.Face.Store;
using moleQule.Library;
using moleQule.Library.Invoice;
using moleQule.Library.Invoice.Reports.Ventas;
using moleQule.Library.Store;

namespace moleQule.Face.Invoice
{
    public partial class VentasMensualActionForm : Skin04.ReportSkinForm
    {
        #region Properties

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        public const string ID = "VentasMensualActionForm";
        public static Type Type { get { return typeof(VentasMensualActionForm); } }

		VentasList _list;
        ProductInfo _producto = null;
        ClienteInfo _cliente = null;
        SerieInfo _serie = null;
        ExpedientInfo _expediente;
        
        #endregion

        #region Factory Methods

        public VentasMensualActionForm()
            : this(null) { }

        public VentasMensualActionForm(Form parent)
            : base(true, parent)
        {
            InitializeComponent();
            SetFormData();
        }

        #endregion

        #region Source

        public override void RefreshSecondaryData()
        {
            Datos_TiposExp.DataSource = Library.Store.EnumText<ETipoExpediente>.GetList(false);
			TipoExpediente_CB.SelectedItem = ((ComboBoxList<ETipoExpediente>)(Datos_TiposExp.DataSource)).Buscar((long)ETipoExpediente.Todos);
            PgMng.Grow();

			ETipoProducto[] product_type_list = { ETipoProducto.Todos, ETipoProducto.Expediente, ETipoProducto.Libres, ETipoProducto.Almacen };
			Datos_TiposPro.DataSource = Library.Store.EnumText<ETipoProducto>.GetList(product_type_list);
			TipoProducto_CB.SelectedItem = ((ComboBoxList<ETipoProducto>)(Datos_TiposPro.DataSource)).Buscar((long)ETipoProducto.Todos);
            PgMng.Grow();

			ETipoFactura[] invoice_type_list = { ETipoFactura.Todas, ETipoFactura.NoAgrupadas };
			Datos_TipoFactura.DataSource = Library.Store.EnumText<ETipoFactura>.GetList(invoice_type_list);
			TipoFactura_CB.SelectedItem = ((ComboBoxList<ETipoFactura>)(Datos_TipoFactura.DataSource)).Buscar((long)ETipoFactura.NoAgrupadas);
			PgMng.Grow();
        }

        #endregion

		#region Business Methods

		protected override string GetFilterValues()
		{
			string filtro = string.Empty;

			if (!TodosProducto_CkB.Checked)
				filtro += "Producto " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _producto.Nombre + "; ";
			else
				filtro += "Tipo Producto " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + TipoProducto_CB.Text + "; ";

			if (!TodosExpediente_CkB.Checked)
				filtro += "Expediente " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _expediente.Codigo + "; ";
			else
				filtro += "Tipo Expediente " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + TipoExpediente_CB.Text + "; ";

			if (!TodosSerie_CkB.Checked)
				filtro += "Serie " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.Equal) + " " + _serie.Nombre + "; ";

			if (FInicial_DTP.Checked)
				filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.GreaterOrEqual) + " " + FInicial_DTP.Value.ToShortDateString() + "; ";

			if (FFinal_DTP.Checked)
				filtro += "Fecha " + Library.CslaEx.EnumText.GetOperator(Library.CslaEx.Operation.LessOrEqual) + " " + FFinal_DTP.Value.ToShortDateString() + "; ";

			return filtro;
		}

		protected void ShowVentasChart()
		{
			Chart chart = NewChart();
			bool isIn = false;

			foreach (VentasInfo item in _list)
			{
				foreach (System.Windows.Forms.DataVisualization.Charting.Series sItem in chart.Series)
					if (sItem.Name == item.Anio) isIn = true;

				if (!isIn) chart.Series.Add(
					new System.Windows.Forms.DataVisualization.Charting.Series
					{
						Name = item.Anio,
						ChartType = SeriesChartType.Column,
						Color = Color.FromArgb(100 + (int)((chart.Series.Count * 50) % 155), 150 + (int)((chart.Series.Count * 50) % 105), 255),
						IsValueShownAsLabel = true
					});

				isIn = false;
			}

			DataPoint point = null;


			foreach (VentasInfo item in _list)
			{
				foreach (System.Windows.Forms.DataVisualization.Charting.Series sItem in chart.Series)
				{
					if (sItem.Name == item.Anio)
					{
						point = new DataPoint(Convert.ToDouble(item.MesD), (double)item.Total);
						sItem.Points.Add(point);
					}
				}
			}

			chart.Legends.Add(new Legend { Title = "Año" });

			ShowChart();
		}

		protected void ShowClientesChart()
		{
			Chart chart = NewChart();
			bool isIn = false;
			Random rd = new Random();

			foreach (VentasInfo item in _list)
			{
				foreach (System.Windows.Forms.DataVisualization.Charting.Series sItem in chart.Series)
					if (sItem.Name == item.Cliente) isIn = true;

				if (!isIn) chart.Series.Add(
					new System.Windows.Forms.DataVisualization.Charting.Series
					{
						Name = item.Cliente,
						ChartType = SeriesChartType.Spline,
						Color = Color.FromArgb(Convert.ToInt32(rd.Next() % 255), Convert.ToInt32(rd.Next() % 255), Convert.ToInt32(rd.Next() % 255)),
						IsValueShownAsLabel = true,
						BorderWidth = 2
					});

				isIn = false;
			}

			DataPoint point = null;

			foreach (VentasInfo item in _list)
			{
				foreach (System.Windows.Forms.DataVisualization.Charting.Series sItem in chart.Series)
				{
					if (sItem.Name == item.Cliente)
					{
						point = new DataPoint(Convert.ToDouble(item.MesD), (double)item.Total);
						sItem.Points.Add(point);
					}
				}
			}

			chart.Legends.Add(new Legend { Title = "Cliente" });

			ShowChart();
		}

		protected void ShowProductosChart()
		{
			Chart chart = NewChart();
			bool isIn = false;
			Random rd = new Random();

			foreach (VentasInfo item in _list)
			{
				foreach (System.Windows.Forms.DataVisualization.Charting.Series sItem in chart.Series)
					if (sItem.Name == item.Producto) isIn = true;

				if (!isIn) chart.Series.Add(
					new System.Windows.Forms.DataVisualization.Charting.Series
					{
						Name = item.Producto,
						ChartType = SeriesChartType.Column,
						Color = Color.FromArgb(Convert.ToInt32(rd.Next() % 255), Convert.ToInt32(rd.Next() % 255), Convert.ToInt32(rd.Next() % 255)),
						IsValueShownAsLabel = true,
						BorderWidth = 2
					});

				isIn = false;
			}

			DataPoint point = null;

			foreach (VentasInfo item in _list)
			{
				foreach (System.Windows.Forms.DataVisualization.Charting.Series sItem in chart.Series)
				{
					if (sItem.Name == item.Producto)
					{
						point = new DataPoint(Convert.ToDouble(item.MesD), (double)item.Total);
						sItem.Points.Add(point);
					}
				}
			}

			chart.Legends.Add(new Legend { Title = "Producto" });

			ShowChart();
		}

		protected override void ShowChart()
		{
			Chart chart = Chart;

			chart.ChartAreas[0].AxisY.Title = "Importe (€)";
			chart.ChartAreas[0].AxisX.Title = "Mes";
			chart.Titles[0].Text = this.Text;

			base.ShowChart();
		}

		#endregion

        #region Actions

        protected override void  PrintAction()
        {
            PgMng.Reset(4, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

            Library.Invoice.QueryConditions conditions = new Library.Invoice.QueryConditions();
            
            conditions.Cliente = TodosCliente_CkB.Checked ? null : _cliente;
            conditions.Producto = TodosProducto_CkB.Checked ? null : _producto;
            conditions.TipoProducto = !TodosProducto_CkB.Checked ? ETipoProducto.Todos : (ETipoProducto)(long)TipoProducto_CB.SelectedValue;
            conditions.Serie = TodosSerie_CkB.Checked ? null : _serie;
            conditions.Expediente = TodosExpediente_CkB.Checked ? null : _expediente;
            conditions.TipoExpediente = !TodosExpediente_CkB.Checked ? ETipoExpediente.Todos : (ETipoExpediente)(long)TipoExpediente_CB.SelectedValue;
            conditions.FechaIni = FInicial_DTP.Checked ? FInicial_DTP.Value : DateTime.MinValue;
            conditions.FechaFin = FFinal_DTP.Checked ? FFinal_DTP.Value : DateTime.MaxValue;
			conditions.TipoFactura = (ETipoFactura)(long)TipoFactura_CB.SelectedValue;

            bool detalle = Detallado_RB.Checked;

            string filtro = GetFilterValues();
            PgMng.Grow();

			if (General_RB.Checked)
				GeneralAction(conditions, filtro);
			else if (PorcentualVentas_RB.Checked)
				PorcentualAction(conditions, filtro);
			else if (PorcentualBeneficios_RB.Checked)
				PorcentualAction(conditions, filtro);

            _action_result = DialogResult.Ignore;
        }

		protected void GeneralAction(Library.Invoice.QueryConditions conditions, string filtro)
		{
			bool detalle = Detallado_RB.Checked;
			CommonReportMng reportMng = new CommonReportMng(AppContext.ActiveSchema, this.Text, filtro);

			if (!detalle)
			{
				_list = VentasList.GetListMensual(conditions);
				PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

				InformeVentasMensualRpt rpt = reportMng.GetVentasMensualReport(_list);
				PgMng.FillUp();

				if (Informe_RB.Checked)
					ShowReport(rpt);
				else
				{
					ShowVentasChart();
				}
			}
			else if (Cliente_RB.Checked)
			{
				if (detalle)
				{
					_list = VentasList.GetListByClienteMensual(conditions);
					PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

					InformeVentasMensualxClienteRpt rpt = reportMng.GetVentasMensualxClienteReport(_list);
					PgMng.FillUp();

					if (Informe_RB.Checked)
						ShowReport(rpt);
					else
						ShowClientesChart();
				}

			}
			else if (Producto_RB.Checked)
			{
				_list = VentasList.GetListByProductoMensual(conditions);
				PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

				InformeVentasMensualxProductoRpt rpt = reportMng.GetVentasMensualxProductoReport(_list);
				PgMng.FillUp();

				if (Informe_RB.Checked)
					ShowReport(rpt);
				else
					ShowProductosChart();
			}
		}

		protected void PorcentualAction(Library.Invoice.QueryConditions conditions, string filtro)
		{
			bool detalle = Detallado_RB.Checked;
            string title = this.Text;
            if (PorcentualBeneficios_RB.Checked)
                title = "Informe Estadístico: Porcentual de Beneficios";
            else if (PorcentualVentas_RB.Checked)
                title = "Informe Estadísitico: Porcentual de Ventas";
            if (!AgruparMeses_CB.Checked)
                title += " por Periodo";
			CommonReportMng reportMng = new CommonReportMng(AppContext.ActiveSchema, title, filtro);

			ReportFormat format = new ReportFormat();

			format.Orden = CrystalDecisions.Shared.SortDirection.DescendingOrder;	
			
			if (Cliente_RB.Checked)
			{
				if (detalle)
				{
                    if (PorcentualVentas_RB.Checked)
                    {
                        if (AgruparMeses_CB.Checked)
                            _list = VentasList.GetListByClientePorcentualVenta(conditions);
                        else
                            _list = VentasList.GetListByClientePorcentualVentaPeriodo(conditions);
                    }
                    else if (PorcentualBeneficios_RB.Checked)
                    {
                        if (AgruparMeses_CB.Checked)
                            _list = VentasList.GetListByClientePorcentualBeneficio(conditions);
                        else
                            _list = VentasList.GetListByClientePorcentualBeneficioPeriodo(conditions);
                    }

                    if (AgruparMeses_CB.Checked)
                    {
                        PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

                        InformeVentasPorcentualxClienteRpt rpt = reportMng.GetVentasPorcentualxClienteReport(_list, conditions);
                        PgMng.FillUp();

                        if (Informe_RB.Checked)
                            ShowReport(rpt);
                        else
                            ShowClientesChart();
                    }
                    else
                    {
                        PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

                        InformeVentasPorcentualPeriodoxClienteRpt rpt = reportMng.GetVentasPorcentualPeriodoxClienteReport(_list, conditions);
                        PgMng.FillUp();

                        if (Informe_RB.Checked)
                            ShowReport(rpt);
                    }
				}
			}
			else if (Producto_RB.Checked)
			{
				if (PorcentualVentas_RB.Checked)
					_list = VentasList.GetListByProductoPorcentualVenta(conditions);
				else if (PorcentualBeneficios_RB.Checked)
					_list = VentasList.GetListByProductoPorcentualBeneficio(conditions);
				
				PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);

				InformeVentasPorcentualxProductoRpt rpt = reportMng.GetVentasPorcentualxProductoReport(_list, conditions);
				PgMng.FillUp();

				if (Informe_RB.Checked)
					ShowReport(rpt);
				else
					ShowProductosChart();
			}
		}		

		#endregion

        #region Events

        private void Cliente_BT_Click(object sender, EventArgs e)
        {
            ClientSelectForm form = new ClientSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _cliente = form.Selected as ClienteInfo;
                Cliente_TB.Text = _cliente.Nombre;
            }
        }

        private void Producto_BT_Click(object sender, EventArgs e)
        {
            ProductSelectForm form = new ProductSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _producto = form.Selected as ProductInfo;
                Producto_TB.Text = _producto.Nombre;
            }
        }

        private void Expediente_BT_Click(object sender, EventArgs e)
        {
            ExpedienteSelectForm form = new ExpedienteSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _expediente = form.Selected as ExpedientInfo;
                Expediente_TB.Text = _expediente.Codigo;
            }
        }

        private void Serie_BT_Click(object sender, EventArgs e)
        {
            SerieSelectForm form = new SerieSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                _serie = form.Selected as SerieInfo;
                Serie_TB.Text = _serie.Nombre;
            }
        }

        private void TodosCliente_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Cliente_BT.Enabled = !TodosCliente_CkB.Checked;
        }

        private void TodosProducto_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Producto_BT.Enabled = !TodosProducto_CkB.Checked;
            TipoProducto_CB.Enabled = TodosProducto_CkB.Checked;
            TipoProducto_CB.SelectedValue = !TodosProducto_CkB.Checked ? (long)ETipoProducto.Todos : TipoProducto_CB.SelectedValue;
        }

        private void TodosExpediente_CkB_CheckedChanged(object sender, EventArgs e)
        {
            Expediente_BT.Enabled = !TodosExpediente_CkB.Checked;
            TipoExpediente_CB.Enabled = TodosExpediente_CkB.Checked;
            TipoExpediente_CB.SelectedValue = !TodosExpediente_CkB.Checked ? (long)ETipoExpediente.Todos : TipoExpediente_CB.SelectedValue;
        }

        private void TodosSerie_GB_CheckedChanged(object sender, EventArgs e)
        {
            Serie_BT.Enabled = !TodosSerie_CkB.Checked;
        }

		private void General_RB_CheckedChanged(object sender, EventArgs e)
		{
			Tipo_GB.Enabled = true;
			TipoProducto_CB.Enabled = true;
			Grafica_RB.Enabled = true;
		}

		private void Porcentual_RB_CheckedChanged(object sender, EventArgs e)
		{
			TipoProducto_CB.Enabled = false;
			TipoProducto_CB.SelectedItem = ((ComboBoxList<ETipoProducto>)(Datos_TiposPro.DataSource)).Buscar((long)ETipoProducto.Todos);

			Detallado_RB.Checked = true;
			Tipo_GB.Enabled = false;
			Grafica_RB.Enabled = false;
		}

		private void Detallado_RB_CheckedChanged(object sender, EventArgs e)
		{
			Agrupar_GB.Enabled = Detallado_RB.Checked;
		}

		private void Resumido_RB_CheckedChanged(object sender, EventArgs e)
		{
			Agrupar_GB.Enabled = !Resumido_RB.Checked;
		}

        #endregion
    }
}

