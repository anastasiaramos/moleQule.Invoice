namespace moleQule.Face.Invoice
{
    partial class ConceptoProformaUIForm
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Label cantidadLabel;
			System.Windows.Forms.Label conceptoLabel;
			System.Windows.Forms.Label precioLabel;
			System.Windows.Forms.Label subtotalLabel;
			System.Windows.Forms.Label totalLabel;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.Label label6;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label BeneficioKilo_LB;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.Label label4;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConceptoProformaUIForm));
			this.PanelExpedientes = new System.Windows.Forms.SplitContainer();
			this.ProductosPanel = new System.Windows.Forms.SplitContainer();
			this.Tabla_Productos = new System.Windows.Forms.DataGridView();
			this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ExpedienteCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PrecioVentaKilo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.KilosIniciales = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.StockKilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.BultosIniciales = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.StockBultos = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.KiloPorBulto = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Datos_Partida = new System.Windows.Forms.BindingSource(this.components);
			this.Productos_BT = new System.Windows.Forms.Button();
			this.ConceptoProforma_GB = new System.Windows.Forms.GroupBox();
			this.FBultos_CKB = new System.Windows.Forms.CheckBox();
			this.Detalles_GB = new System.Windows.Forms.GroupBox();
			this.Gastos_TB = new System.Windows.Forms.TextBox();
			this.BeneficioKilo_TB = new System.Windows.Forms.TextBox();
			this.Beneficio_TB = new System.Windows.Forms.TextBox();
			this.Detalles_BT = new System.Windows.Forms.Button();
			this.PrecioCliente_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.PrecioProducto_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.IGIC_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.Igic_Aplicado_LB = new System.Windows.Forms.Label();
			this.Bultos_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.totalNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.subtotalNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.precioNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
			this.Concepto_TB = new System.Windows.Forms.TextBox();
			this.Kilos_NTB = new moleQule.Face.Controls.NumericTextBox();
			this.PDescuento_NTB = new moleQule.Face.Controls.NumericTextBox();
			cantidadLabel = new System.Windows.Forms.Label();
			conceptoLabel = new System.Windows.Forms.Label();
			precioLabel = new System.Windows.Forms.Label();
			subtotalLabel = new System.Windows.Forms.Label();
			totalLabel = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			BeneficioKilo_LB = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Progress_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.PanelExpedientes.Panel2.SuspendLayout();
			this.PanelExpedientes.SuspendLayout();
			this.ProductosPanel.Panel1.SuspendLayout();
			this.ProductosPanel.Panel2.SuspendLayout();
			this.ProductosPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Tabla_Productos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Partida)).BeginInit();
			this.ConceptoProforma_GB.SuspendLayout();
			this.Detalles_GB.SuspendLayout();
			this.SuspendLayout();
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Library.Invoice.ConceptoProforma);
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(428, 8);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(518, 8);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Source_GB
			// 
			this.Source_GB.Enabled = false;
			this.Source_GB.Location = new System.Drawing.Point(3, 3);
			this.HelpProvider.SetShowHelp(this.Source_GB, true);
			this.Source_GB.Size = new System.Drawing.Size(95, 58);
			this.Source_GB.Text = "Expedientes";
			this.Source_GB.Visible = false;
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.AutoScroll = true;
			this.PanelesV.Panel1.Controls.Add(this.PanelExpedientes);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(1034, 466);
			this.PanelesV.SplitterDistance = 425;
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(308, 151);
			// 
			// cantidadLabel
			// 
			cantidadLabel.AutoSize = true;
			cantidadLabel.Location = new System.Drawing.Point(324, 18);
			cantidadLabel.Name = "cantidadLabel";
			cantidadLabel.Size = new System.Drawing.Size(93, 13);
			cantidadLabel.TabIndex = 0;
			cantidadLabel.Text = "Cantidad en Kilos:";
			// 
			// conceptoLabel
			// 
			conceptoLabel.AutoSize = true;
			conceptoLabel.Location = new System.Drawing.Point(52, 18);
			conceptoLabel.Name = "conceptoLabel";
			conceptoLabel.Size = new System.Drawing.Size(65, 13);
			conceptoLabel.TabIndex = 4;
			conceptoLabel.Text = "Descripción:";
			// 
			// precioLabel
			// 
			precioLabel.AutoSize = true;
			precioLabel.Location = new System.Drawing.Point(324, 58);
			precioLabel.Name = "precioLabel";
			precioLabel.Size = new System.Drawing.Size(71, 13);
			precioLabel.TabIndex = 10;
			precioLabel.Text = "Precio Venta:";
			// 
			// subtotalLabel
			// 
			subtotalLabel.AutoSize = true;
			subtotalLabel.Location = new System.Drawing.Point(327, 98);
			subtotalLabel.Name = "subtotalLabel";
			subtotalLabel.Size = new System.Drawing.Size(51, 13);
			subtotalLabel.TabIndex = 12;
			subtotalLabel.Text = "Subtotal:";
			// 
			// totalLabel
			// 
			totalLabel.AutoSize = true;
			totalLabel.Location = new System.Drawing.Point(570, 98);
			totalLabel.Name = "totalLabel";
			totalLabel.Size = new System.Drawing.Size(35, 13);
			totalLabel.TabIndex = 14;
			totalLabel.Text = "Total:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(448, 18);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(101, 13);
			label2.TabIndex = 18;
			label2.Text = "Cantidad en Bultos:";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(445, 58);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(86, 13);
			label5.TabIndex = 27;
			label5.Text = "Precio Producto:";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(566, 58);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(76, 13);
			label6.TabIndex = 29;
			label6.Text = "Precio Cliente:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(227, 21);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(54, 13);
			label3.TabIndex = 24;
			label3.Text = "Beneficio:";
			// 
			// BeneficioKilo_LB
			// 
			BeneficioKilo_LB.AutoSize = true;
			BeneficioKilo_LB.Location = new System.Drawing.Point(124, 21);
			BeneficioKilo_LB.Name = "BeneficioKilo_LB";
			BeneficioKilo_LB.Size = new System.Drawing.Size(73, 13);
			BeneficioKilo_LB.TabIndex = 26;
			BeneficioKilo_LB.Text = "Beneficio Kilo:";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(15, 21);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(58, 13);
			label1.TabIndex = 28;
			label1.Text = "Coste Kilo:";
			// 
			// PanelExpedientes
			// 
			this.PanelExpedientes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelExpedientes.Location = new System.Drawing.Point(0, 0);
			this.PanelExpedientes.Name = "PanelExpedientes";
			// 
			// PanelExpedientes.Panel1
			// 
			this.PanelExpedientes.Panel1.AutoScroll = true;
			this.PanelExpedientes.Panel1Collapsed = true;
			// 
			// PanelExpedientes.Panel2
			// 
			this.PanelExpedientes.Panel2.AutoScroll = true;
			this.PanelExpedientes.Panel2.Controls.Add(this.ProductosPanel);
			this.PanelExpedientes.Size = new System.Drawing.Size(1032, 423);
			this.PanelExpedientes.SplitterDistance = 377;
			this.PanelExpedientes.TabIndex = 2;
			// 
			// ProductosPanel
			// 
			this.ProductosPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProductosPanel.Location = new System.Drawing.Point(0, 0);
			this.ProductosPanel.Name = "ProductosPanel";
			this.ProductosPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// ProductosPanel.Panel1
			// 
			this.ProductosPanel.Panel1.Controls.Add(this.Tabla_Productos);
			// 
			// ProductosPanel.Panel2
			// 
			this.ProductosPanel.Panel2.AutoScroll = true;
			this.ProductosPanel.Panel2.Controls.Add(label4);
			this.ProductosPanel.Panel2.Controls.Add(this.PDescuento_NTB);
			this.ProductosPanel.Panel2.Controls.Add(this.Productos_BT);
			this.ProductosPanel.Panel2.Controls.Add(this.ConceptoProforma_GB);
			this.ProductosPanel.Size = new System.Drawing.Size(1032, 423);
			this.ProductosPanel.SplitterDistance = 58;
			this.ProductosPanel.TabIndex = 3;
			// 
			// Tabla_Productos
			// 
			this.Tabla_Productos.AllowUserToAddRows = false;
			this.Tabla_Productos.AllowUserToDeleteRows = false;
			this.Tabla_Productos.AllowUserToResizeRows = false;
			this.Tabla_Productos.AutoGenerateColumns = false;
			this.Tabla_Productos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.ExpedienteCol,
            this.PrecioVentaKilo,
            this.KilosIniciales,
            this.StockKilos,
            this.BultosIniciales,
            this.StockBultos,
            this.KiloPorBulto});
			this.Tabla_Productos.DataSource = this.Datos_Partida;
			this.Tabla_Productos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Tabla_Productos.Enabled = false;
			this.Tabla_Productos.Location = new System.Drawing.Point(0, 0);
			this.Tabla_Productos.MultiSelect = false;
			this.Tabla_Productos.Name = "Tabla_Productos";
			this.Tabla_Productos.ReadOnly = true;
			this.Tabla_Productos.RowHeadersVisible = false;
			this.Tabla_Productos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.Tabla_Productos.Size = new System.Drawing.Size(1032, 58);
			this.Tabla_Productos.TabIndex = 1;
			// 
			// Nombre
			// 
			this.Nombre.DataPropertyName = "TipoMercancia";
			this.Nombre.HeaderText = "Producto";
			this.Nombre.Name = "Nombre";
			this.Nombre.ReadOnly = true;
			this.Nombre.Width = 403;
			// 
			// ExpedienteCol
			// 
			this.ExpedienteCol.DataPropertyName = "Expediente";
			this.ExpedienteCol.HeaderText = "Expediente";
			this.ExpedienteCol.Name = "ExpedienteCol";
			this.ExpedienteCol.ReadOnly = true;
			this.ExpedienteCol.Width = 150;
			// 
			// PrecioVentaKilo
			// 
			this.PrecioVentaKilo.DataPropertyName = "PrecioVentaKilo";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle1.Format = "C5";
			this.PrecioVentaKilo.DefaultCellStyle = dataGridViewCellStyle1;
			this.PrecioVentaKilo.HeaderText = "Precio Venta Kg";
			this.PrecioVentaKilo.Name = "PrecioVentaKilo";
			this.PrecioVentaKilo.ReadOnly = true;
			this.PrecioVentaKilo.Width = 75;
			// 
			// KilosIniciales
			// 
			this.KilosIniciales.DataPropertyName = "KilosIniciales";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle2.Format = "N2";
			dataGridViewCellStyle2.NullValue = null;
			this.KilosIniciales.DefaultCellStyle = dataGridViewCellStyle2;
			this.KilosIniciales.HeaderText = "Kg Iniciales";
			this.KilosIniciales.Name = "KilosIniciales";
			this.KilosIniciales.ReadOnly = true;
			this.KilosIniciales.Width = 80;
			// 
			// StockKilos
			// 
			this.StockKilos.DataPropertyName = "StockKilos";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle3.Format = "N2";
			dataGridViewCellStyle3.NullValue = null;
			this.StockKilos.DefaultCellStyle = dataGridViewCellStyle3;
			this.StockKilos.HeaderText = "Stock Kg";
			this.StockKilos.Name = "StockKilos";
			this.StockKilos.ReadOnly = true;
			this.StockKilos.Width = 80;
			// 
			// BultosIniciales
			// 
			this.BultosIniciales.DataPropertyName = "BultosIniciales";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle4.Format = "N2";
			dataGridViewCellStyle4.NullValue = null;
			this.BultosIniciales.DefaultCellStyle = dataGridViewCellStyle4;
			this.BultosIniciales.HeaderText = "Bultos Iniciales";
			this.BultosIniciales.Name = "BultosIniciales";
			this.BultosIniciales.ReadOnly = true;
			this.BultosIniciales.Width = 80;
			// 
			// StockBultos
			// 
			this.StockBultos.DataPropertyName = "StockBultos";
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle5.Format = "N4";
			dataGridViewCellStyle5.NullValue = null;
			this.StockBultos.DefaultCellStyle = dataGridViewCellStyle5;
			this.StockBultos.HeaderText = "Stock Bultos";
			this.StockBultos.Name = "StockBultos";
			this.StockBultos.ReadOnly = true;
			this.StockBultos.Width = 80;
			// 
			// KiloPorBulto
			// 
			this.KiloPorBulto.DataPropertyName = "KilosPorBulto";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle6.Format = "N2";
			dataGridViewCellStyle6.NullValue = null;
			this.KiloPorBulto.DefaultCellStyle = dataGridViewCellStyle6;
			this.KiloPorBulto.HeaderText = "Kg / Bulto";
			this.KiloPorBulto.Name = "KiloPorBulto";
			this.KiloPorBulto.ReadOnly = true;
			this.KiloPorBulto.Width = 80;
			// 
			// Datos_Partida
			// 
			this.Datos_Partida.DataSource = typeof(moleQule.Library.Store.PartidaInfo);
			this.Datos_Partida.CurrentChanged += new System.EventHandler(this.Datos_Partida_CurrentChanged);
			// 
			// Productos_BT
			// 
			this.Productos_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Productos_BT.Location = new System.Drawing.Point(455, 18);
			this.Productos_BT.Name = "Productos_BT";
			this.Productos_BT.Size = new System.Drawing.Size(123, 23);
			this.Productos_BT.TabIndex = 3;
			this.Productos_BT.Text = "Productos";
			this.Productos_BT.UseVisualStyleBackColor = true;
			this.Productos_BT.Click += new System.EventHandler(this.Productos_BT_Click);
			// 
			// ConceptoProforma_GB
			// 
			this.ConceptoProforma_GB.Controls.Add(this.FBultos_CKB);
			this.ConceptoProforma_GB.Controls.Add(this.Detalles_GB);
			this.ConceptoProforma_GB.Controls.Add(this.Detalles_BT);
			this.ConceptoProforma_GB.Controls.Add(label6);
			this.ConceptoProforma_GB.Controls.Add(this.PrecioCliente_NTB);
			this.ConceptoProforma_GB.Controls.Add(label5);
			this.ConceptoProforma_GB.Controls.Add(this.PrecioProducto_NTB);
			this.ConceptoProforma_GB.Controls.Add(this.IGIC_NTB);
			this.ConceptoProforma_GB.Controls.Add(this.Igic_Aplicado_LB);
			this.ConceptoProforma_GB.Controls.Add(label2);
			this.ConceptoProforma_GB.Controls.Add(this.Bultos_NTB);
			this.ConceptoProforma_GB.Controls.Add(totalLabel);
			this.ConceptoProforma_GB.Controls.Add(this.totalNumericTextBox);
			this.ConceptoProforma_GB.Controls.Add(subtotalLabel);
			this.ConceptoProforma_GB.Controls.Add(this.subtotalNumericTextBox);
			this.ConceptoProforma_GB.Controls.Add(precioLabel);
			this.ConceptoProforma_GB.Controls.Add(this.precioNumericTextBox);
			this.ConceptoProforma_GB.Controls.Add(conceptoLabel);
			this.ConceptoProforma_GB.Controls.Add(this.Concepto_TB);
			this.ConceptoProforma_GB.Controls.Add(cantidadLabel);
			this.ConceptoProforma_GB.Controls.Add(this.Kilos_NTB);
			this.ConceptoProforma_GB.Location = new System.Drawing.Point(155, 62);
			this.ConceptoProforma_GB.Name = "ConceptoProforma_GB";
			this.ConceptoProforma_GB.Size = new System.Drawing.Size(723, 271);
			this.ConceptoProforma_GB.TabIndex = 2;
			this.ConceptoProforma_GB.TabStop = false;
			this.ConceptoProforma_GB.Text = "Datos";
			// 
			// FBultos_CKB
			// 
			this.FBultos_CKB.AutoSize = true;
			this.FBultos_CKB.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.Datos, "FacturacionBulto", true));
			this.FBultos_CKB.Location = new System.Drawing.Point(569, 34);
			this.FBultos_CKB.Name = "FBultos_CKB";
			this.FBultos_CKB.Size = new System.Drawing.Size(97, 17);
			this.FBultos_CKB.TabIndex = 32;
			this.FBultos_CKB.Text = "Facturar Bultos";
			this.FBultos_CKB.UseVisualStyleBackColor = true;
			this.FBultos_CKB.CheckedChanged += new System.EventHandler(this.FBultos_CKB_CheckedChanged);
			// 
			// Detalles_GB
			// 
			this.Detalles_GB.Controls.Add(label1);
			this.Detalles_GB.Controls.Add(this.Gastos_TB);
			this.Detalles_GB.Controls.Add(BeneficioKilo_LB);
			this.Detalles_GB.Controls.Add(this.BeneficioKilo_TB);
			this.Detalles_GB.Controls.Add(label3);
			this.Detalles_GB.Controls.Add(this.Beneficio_TB);
			this.Detalles_GB.Location = new System.Drawing.Point(327, 175);
			this.Detalles_GB.Name = "Detalles_GB";
			this.Detalles_GB.Size = new System.Drawing.Size(343, 78);
			this.Detalles_GB.TabIndex = 31;
			this.Detalles_GB.TabStop = false;
			this.Detalles_GB.Text = "Detalles";
			this.Detalles_GB.Visible = false;
			// 
			// Gastos_TB
			// 
			this.Gastos_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Gastos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N5"));
			this.Gastos_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Gastos_TB.Location = new System.Drawing.Point(18, 37);
			this.Gastos_TB.Name = "Gastos_TB";
			this.Gastos_TB.ReadOnly = true;
			this.Gastos_TB.Size = new System.Drawing.Size(100, 21);
			this.Gastos_TB.TabIndex = 27;
			this.Gastos_TB.Text = "0.00000";
			this.Gastos_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// BeneficioKilo_TB
			// 
			this.BeneficioKilo_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BeneficioKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N5"));
			this.BeneficioKilo_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BeneficioKilo_TB.Location = new System.Drawing.Point(124, 37);
			this.BeneficioKilo_TB.Name = "BeneficioKilo_TB";
			this.BeneficioKilo_TB.ReadOnly = true;
			this.BeneficioKilo_TB.Size = new System.Drawing.Size(100, 21);
			this.BeneficioKilo_TB.TabIndex = 25;
			this.BeneficioKilo_TB.Text = "0.00";
			this.BeneficioKilo_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// Beneficio_TB
			// 
			this.Beneficio_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Beneficio", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.Beneficio_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Beneficio_TB.Location = new System.Drawing.Point(230, 37);
			this.Beneficio_TB.Name = "Beneficio_TB";
			this.Beneficio_TB.ReadOnly = true;
			this.Beneficio_TB.Size = new System.Drawing.Size(100, 21);
			this.Beneficio_TB.TabIndex = 23;
			this.Beneficio_TB.Text = "0.00";
			this.Beneficio_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Beneficio_TB.TextChanged += new System.EventHandler(this.Beneficio_TB_TextChanged);
			// 
			// Detalles_BT
			// 
			this.Detalles_BT.Location = new System.Drawing.Point(448, 142);
			this.Detalles_BT.Name = "Detalles_BT";
			this.Detalles_BT.Size = new System.Drawing.Size(100, 23);
			this.Detalles_BT.TabIndex = 30;
			this.Detalles_BT.Text = "Detalles";
			this.Detalles_BT.UseVisualStyleBackColor = true;
			this.Detalles_BT.Click += new System.EventHandler(this.Detalles_BT_Click);
			// 
			// PrecioCliente_NTB
			// 
			this.PrecioCliente_NTB.Location = new System.Drawing.Point(569, 74);
			this.PrecioCliente_NTB.Name = "PrecioCliente_NTB";
			this.PrecioCliente_NTB.ReadOnly = true;
			this.PrecioCliente_NTB.Size = new System.Drawing.Size(100, 21);
			this.PrecioCliente_NTB.TabIndex = 28;
			this.PrecioCliente_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PrecioCliente_NTB.TextIsCurrency = false;
			this.PrecioCliente_NTB.TextIsInteger = false;
			// 
			// PrecioProducto_NTB
			// 
			this.PrecioProducto_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Partida, "PrecioVentaKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N5"));
			this.PrecioProducto_NTB.Location = new System.Drawing.Point(448, 74);
			this.PrecioProducto_NTB.Name = "PrecioProducto_NTB";
			this.PrecioProducto_NTB.ReadOnly = true;
			this.PrecioProducto_NTB.Size = new System.Drawing.Size(100, 21);
			this.PrecioProducto_NTB.TabIndex = 26;
			this.PrecioProducto_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PrecioProducto_NTB.TextIsCurrency = false;
			this.PrecioProducto_NTB.TextIsInteger = false;
			// 
			// IGIC_NTB
			// 
			this.IGIC_NTB.BackColor = System.Drawing.SystemColors.Control;
			this.IGIC_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PImpuestos", true));
			this.IGIC_NTB.Location = new System.Drawing.Point(448, 113);
			this.IGIC_NTB.Name = "IGIC_NTB";
			this.IGIC_NTB.Size = new System.Drawing.Size(100, 21);
			this.IGIC_NTB.TabIndex = 25;
			this.IGIC_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.IGIC_NTB.TextIsCurrency = false;
			this.IGIC_NTB.TextIsInteger = false;
			// 
			// Igic_Aplicado_LB
			// 
			this.Igic_Aplicado_LB.AutoSize = true;
			this.Igic_Aplicado_LB.Location = new System.Drawing.Point(445, 98);
			this.Igic_Aplicado_LB.Name = "Igic_Aplicado_LB";
			this.Igic_Aplicado_LB.Size = new System.Drawing.Size(90, 13);
			this.Igic_Aplicado_LB.TabIndex = 20;
			this.Igic_Aplicado_LB.Text = "% IGIC Aplicado:";
			// 
			// Bultos_NTB
			// 
			this.Bultos_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CantidadBultos", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N4"));
			this.Bultos_NTB.Enabled = false;
			this.Bultos_NTB.ForeColor = System.Drawing.Color.Navy;
			this.Bultos_NTB.Location = new System.Drawing.Point(448, 34);
			this.Bultos_NTB.Name = "Bultos_NTB";
			this.Bultos_NTB.Size = new System.Drawing.Size(100, 21);
			this.Bultos_NTB.TabIndex = 2;
			this.Bultos_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Bultos_NTB.TextIsCurrency = false;
			this.Bultos_NTB.TextIsInteger = false;
			// 
			// totalNumericTextBox
			// 
			this.totalNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Total", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.totalNumericTextBox.Location = new System.Drawing.Point(570, 113);
			this.totalNumericTextBox.Name = "totalNumericTextBox";
			this.totalNumericTextBox.ReadOnly = true;
			this.totalNumericTextBox.Size = new System.Drawing.Size(100, 21);
			this.totalNumericTextBox.TabIndex = 5;
			this.totalNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.totalNumericTextBox.TextIsCurrency = false;
			this.totalNumericTextBox.TextIsInteger = false;
			// 
			// subtotalNumericTextBox
			// 
			this.subtotalNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Subtotal", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
			this.subtotalNumericTextBox.Location = new System.Drawing.Point(327, 113);
			this.subtotalNumericTextBox.Name = "subtotalNumericTextBox";
			this.subtotalNumericTextBox.ReadOnly = true;
			this.subtotalNumericTextBox.Size = new System.Drawing.Size(100, 21);
			this.subtotalNumericTextBox.TabIndex = 4;
			this.subtotalNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.subtotalNumericTextBox.TextIsCurrency = false;
			this.subtotalNumericTextBox.TextIsInteger = false;
			// 
			// precioNumericTextBox
			// 
			this.precioNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Precio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N5"));
			this.precioNumericTextBox.Location = new System.Drawing.Point(327, 74);
			this.precioNumericTextBox.Name = "precioNumericTextBox";
			this.precioNumericTextBox.Size = new System.Drawing.Size(100, 21);
			this.precioNumericTextBox.TabIndex = 3;
			this.precioNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.precioNumericTextBox.TextIsCurrency = false;
			this.precioNumericTextBox.TextIsInteger = false;
			// 
			// Concepto_TB
			// 
			this.Concepto_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Concepto", true));
			this.Concepto_TB.ForeColor = System.Drawing.Color.Navy;
			this.Concepto_TB.Location = new System.Drawing.Point(55, 34);
			this.Concepto_TB.Multiline = true;
			this.Concepto_TB.Name = "Concepto_TB";
			this.Concepto_TB.Size = new System.Drawing.Size(253, 219);
			this.Concepto_TB.TabIndex = 0;
			// 
			// Kilos_NTB
			// 
			this.Kilos_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Cantidad", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N2"));
			this.Kilos_NTB.Enabled = false;
			this.Kilos_NTB.ForeColor = System.Drawing.Color.Navy;
			this.Kilos_NTB.Location = new System.Drawing.Point(327, 34);
			this.Kilos_NTB.Name = "Kilos_NTB";
			this.Kilos_NTB.Size = new System.Drawing.Size(100, 21);
			this.Kilos_NTB.TabIndex = 1;
			this.Kilos_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Kilos_NTB.TextIsCurrency = false;
			this.Kilos_NTB.TextIsInteger = false;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(740, 39);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(32, 13);
			label4.TabIndex = 39;
			label4.Text = "Dto.:";
			// 
			// PDescuento_NTB
			// 
			this.PDescuento_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PDescuento", true));
			this.PDescuento_NTB.Location = new System.Drawing.Point(778, 35);
			this.PDescuento_NTB.Name = "PDescuento_NTB";
			this.PDescuento_NTB.Size = new System.Drawing.Size(100, 21);
			this.PDescuento_NTB.TabIndex = 38;
			this.PDescuento_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PDescuento_NTB.TextIsCurrency = false;
			this.PDescuento_NTB.TextIsInteger = false;
			// 
			// ConceptoProformaUIForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(1034, 466);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ConceptoProformaUIForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.ShowInTaskbar = false;
			this.Text = "Concepto de Proforma";
			this.Controls.SetChildIndex(this.Progress_Panel, 0);
			this.Controls.SetChildIndex(this.PanelesV, 0);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.PanelExpedientes.Panel2.ResumeLayout(false);
			this.PanelExpedientes.ResumeLayout(false);
			this.ProductosPanel.Panel1.ResumeLayout(false);
			this.ProductosPanel.Panel2.ResumeLayout(false);
			this.ProductosPanel.Panel2.PerformLayout();
			this.ProductosPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Tabla_Productos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Partida)).EndInit();
			this.ConceptoProforma_GB.ResumeLayout(false);
			this.ConceptoProforma_GB.PerformLayout();
			this.Detalles_GB.ResumeLayout(false);
			this.Detalles_GB.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private moleQule.Face.Controls.NumericTextBox subtotalNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox precioNumericTextBox;
        private System.Windows.Forms.TextBox Concepto_TB;
        private moleQule.Face.Controls.NumericTextBox totalNumericTextBox;
        private System.Windows.Forms.Label Igic_Aplicado_LB;
        private moleQule.Face.Controls.NumericTextBox IGIC_NTB;
        private moleQule.Face.Controls.NumericTextBox PrecioProducto_NTB;
        private System.Windows.Forms.Button Detalles_BT;
        private System.Windows.Forms.GroupBox Detalles_GB;
        private System.Windows.Forms.TextBox Gastos_TB;
        private System.Windows.Forms.TextBox BeneficioKilo_TB;
        private System.Windows.Forms.TextBox Beneficio_TB;
        protected System.Windows.Forms.SplitContainer PanelExpedientes;
        protected System.Windows.Forms.SplitContainer ProductosPanel;
        protected System.Windows.Forms.GroupBox ConceptoProforma_GB;
        protected System.Windows.Forms.BindingSource Datos_Partida;
        protected moleQule.Face.Controls.NumericTextBox PrecioCliente_NTB;
        protected System.Windows.Forms.CheckBox FBultos_CKB;
        protected System.Windows.Forms.DataGridView Tabla_Productos;
        protected System.Windows.Forms.Button Productos_BT;
        protected moleQule.Face.Controls.NumericTextBox Kilos_NTB;
        protected moleQule.Face.Controls.NumericTextBox Bultos_NTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpedienteCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVentaKilo;
        private System.Windows.Forms.DataGridViewTextBoxColumn KilosIniciales;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockKilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn BultosIniciales;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockBultos;
        private System.Windows.Forms.DataGridViewTextBoxColumn KiloPorBulto;
		protected Controls.NumericTextBox PDescuento_NTB;


    }
}
