using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Invoice;
using moleQule.Library.Store;
using moleQule.Face;

namespace moleQule.Face.Invoice
{
    public partial class PreciosForm : Skin01.ActionSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps; } }

        public const string ID = "PreciosForm";
        public static Type Type { get { return typeof(PreciosForm); } }

        // Datasource del datagridview
        IDataReader _reader;
        private DataTable _pivoted_table;
        private DataTable _original_table;
        private ETipoTitular _tipo;
        private string _filtro;
        //private ExcelExporter _exporter;

        #endregion

        #region Factory Methods

        public PreciosForm( ETipoTitular tipo, 
                            IDataReader reader,
                            string filtro)
            : base(true)
        {
            InitializeComponent();

            _tipo = tipo;
            _reader = reader;
            _filtro = filtro;

            if (tipo == ETipoTitular.Cliente)
                this.Text = Resources.Labels.INFORME_PRECIOS_CLIENTES;
            else
                this.Text = Resources.Labels.INFORME_PRECIOS_PROVEEDORES;

            SetFormData();
        }

        #endregion

        #region Layout

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            this.MaximizeForm();
            base.FormatControls();

            Leyenda_Panel.Left = (Superior_SP.Width - Leyenda_Panel.Width) / 2;
            Up_LB.BackColor = (_tipo == ETipoTitular.Proveedor) ? Color.Red : Color.LightGreen;
            EQ_LB.BackColor = Color.White;
            Down_LB.BackColor = (_tipo == ETipoTitular.Proveedor) ? Color.LightGreen : Color.Red;

            int max_rows = 0;
            foreach (DataGridViewRow row in Precios_Tabla.Rows)
            {
                foreach (DataGridViewColumn col in Precios_Tabla.Columns)
                {
                    int rows = col.HeaderText.Split(Convert.ToChar("\n")).Length;
                    max_rows = (max_rows < rows) ? rows : max_rows;
                }
            }

            int h = TextRenderer.MeasureText("PP", Precios_Tabla.Font).Height;
            Precios_Tabla.ColumnHeadersHeight = h * (max_rows + 1);

            string max_name = string.Empty;
            foreach (DataGridViewRow row in Precios_Tabla.Rows)
            {
                foreach (DataGridViewColumn col in Precios_Tabla.Columns)
                {
                    string name = col.HeaderText;

                    if (name.IndexOf("\nPP:") != -1)
                    {
                        Precios_Tabla[col.Index, row.Index].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        if (Precios_Tabla[col.Index, row.Index].Value.ToString().Equals("-"))
                        {
                            Precios_Tabla[col.Index, row.Index].Style.BackColor = Color.Gainsboro;
                            continue;
                        }

                        int pos_ini = name.IndexOf("\nPP:") + 5;
                        int pos_fin = name.IndexOf("\nPM:", pos_ini);
                        decimal precio = Convert.ToDecimal(name.Substring(pos_ini, (pos_fin - pos_ini) + 1));
                        if (precio > Convert.ToDecimal(Precios_Tabla[col.Index, row.Index].Value))
                        {
                            Precios_Tabla[col.Index, row.Index].Style.Format = "########0.,##### €";
                            Precios_Tabla[col.Index, row.Index].Style.BackColor = _tipo == ETipoTitular.Cliente ? Color.Red : Color.LightGreen;
                        }
                        else if (precio == Convert.ToDecimal(Precios_Tabla[col.Index, row.Index].Value))
                        {
                            Precios_Tabla[col.Index, row.Index].Style.BackColor = Color.White;
                        }
                        else
                        {
                            Precios_Tabla[col.Index, row.Index].Style.BackColor = _tipo == ETipoTitular.Cliente ? Color.LightGreen : Color.Red;
                            Precios_Tabla[col.Index, row.Index].Style.Format = "########0.,##### €";
                        }
                    }
                    else
                        Precios_Tabla[col.Index, row.Index].Style.BackColor = Color.LightGray;
                }

                if (max_name.Length < row.Cells["TITULAR"].Value.ToString().Length)
                    max_name = row.Cells["TITULAR"].Value.ToString();
            }

            if (_tipo == ETipoTitular.Cliente)
                Precios_Tabla.Columns[0].HeaderText = "Nombre Cliente";
            else if (_tipo == ETipoTitular.Proveedor)
                Precios_Tabla.Columns[0].HeaderText = "Nombre Proveedor";

            //Precios_Tabla.Columns["TITULAR"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //Precios_Tabla.Columns["TITULAR"].Width = TextRenderer.MeasureText(max_name, Precios_Tabla.Font).Width;
        }

        #endregion

        #region Source

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            _original_table = new DataTable();

            CustomAdapter custom = new CustomAdapter();
            custom.FillFromReader(_original_table, _reader);
            _pivoted_table = PivotTable.GetDataTable(_original_table);

            Precios_Tabla.DataSource = _pivoted_table;
        }

        #endregion

        #region Actions

		protected override void SubmitAction()
        {
            PgMng.Reset(3, 1, Face.Resources.Messages.RETRIEVING_DATA, this);

			try
			{
				PreciosExporter _exporter = new PreciosExporter();

				PgMng.Grow(Face.Resources.Messages.BUILDING_REPORT);
				if (_tipo == ETipoTitular.Cliente)
					_exporter.ExportInformePrecios(_pivoted_table, _tipo, _filtro);
				else if (_tipo == ETipoTitular.Proveedor)
					_exporter.ExportInformePrecios(_pivoted_table, _tipo, _filtro);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, moleQule.Face.Resources.Labels.ADVISE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			finally
			{
				PgMng.FillUp();
			}

            _action_result = DialogResult.Ignore;
        }

        #endregion

        #region Buttons

        private void Excel_BT_Click(object sender, EventArgs e)
        {
            SubmitAction();
        }

        #endregion

        #region Events

        #endregion
    }
}

