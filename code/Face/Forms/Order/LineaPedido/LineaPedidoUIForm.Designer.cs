namespace moleQule.Face.Invoice
{
    partial class LineaPedidoUIForm
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
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PanelExpedientes = new System.Windows.Forms.SplitContainer();
            this.ProductosPanel = new System.Windows.Forms.SplitContainer();
            this.Producto_DGW = new System.Windows.Forms.DataGridView();
            this.codigoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observacionesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Datos_Productos = new System.Windows.Forms.BindingSource(this.components);
            this.Partida_DGW = new System.Windows.Forms.DataGridView();
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
            this.LineaPedido_GB = new System.Windows.Forms.GroupBox();
            this.PDescuento_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Expediente_BT = new System.Windows.Forms.Button();
            this.Expediente_TB = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericTextBox1 = new moleQule.Face.Controls.NumericTextBox();
            this.Impuestos_BT = new System.Windows.Forms.Button();
            this.FacturarPeso_CkB = new System.Windows.Forms.CheckBox();
            this.Detalles_GB = new System.Windows.Forms.GroupBox();
            this.Gastos_TB = new System.Windows.Forms.TextBox();
            this.BeneficioKilo_TB = new System.Windows.Forms.TextBox();
            this.Beneficio_TB = new System.Windows.Forms.TextBox();
            this.Detalles_BT = new System.Windows.Forms.Button();
            this.PrecioCliente_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.PrecioProducto_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Igic_Aplicado_LB = new System.Windows.Forms.Label();
            this.Pieces_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Total_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.subtotalNumericTextBox = new moleQule.Face.Controls.NumericTextBox();
            this.Precio_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Concepto_TB = new System.Windows.Forms.TextBox();
            this.Kilos_NTB = new moleQule.Face.Controls.NumericTextBox();
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
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelExpedientes)).BeginInit();
            this.PanelExpedientes.Panel2.SuspendLayout();
            this.PanelExpedientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductosPanel)).BeginInit();
            this.ProductosPanel.Panel1.SuspendLayout();
            this.ProductosPanel.Panel2.SuspendLayout();
            this.ProductosPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Producto_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Productos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Partida_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Partida)).BeginInit();
            this.LineaPedido_GB.SuspendLayout();
            this.Detalles_GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Invoice.LineaPedido);
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(519, 3);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            this.Submit_BT.TabIndex = 0;
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(403, 4);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            this.Cancel_BT.TabIndex = 1;
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
            this.PanelesV.Size = new System.Drawing.Size(1034, 493);
            this.PanelesV.SplitterDistance = 452;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(313, 41);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(1034, 493);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(485, 298);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(485, 213);
            // 
            // cantidadLabel
            // 
            cantidadLabel.AutoSize = true;
            cantidadLabel.Location = new System.Drawing.Point(599, 21);
            cantidadLabel.Name = "cantidadLabel";
            cantidadLabel.Size = new System.Drawing.Size(23, 13);
            cantidadLabel.TabIndex = 0;
            cantidadLabel.Text = "Kg:";
            // 
            // conceptoLabel
            // 
            conceptoLabel.AutoSize = true;
            conceptoLabel.Location = new System.Drawing.Point(16, 20);
            conceptoLabel.Name = "conceptoLabel";
            conceptoLabel.Size = new System.Drawing.Size(65, 13);
            conceptoLabel.TabIndex = 4;
            conceptoLabel.Text = "Descripción:";
            // 
            // precioLabel
            // 
            precioLabel.AutoSize = true;
            precioLabel.Location = new System.Drawing.Point(581, 77);
            precioLabel.Name = "precioLabel";
            precioLabel.Size = new System.Drawing.Size(40, 13);
            precioLabel.TabIndex = 10;
            precioLabel.Text = "Precio:";
            // 
            // subtotalLabel
            // 
            subtotalLabel.AutoSize = true;
            subtotalLabel.Location = new System.Drawing.Point(571, 131);
            subtotalLabel.Name = "subtotalLabel";
            subtotalLabel.Size = new System.Drawing.Size(51, 13);
            subtotalLabel.TabIndex = 12;
            subtotalLabel.Text = "Subtotal:";
            // 
            // totalLabel
            // 
            totalLabel.AutoSize = true;
            totalLabel.Location = new System.Drawing.Point(587, 188);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new System.Drawing.Size(35, 13);
            totalLabel.TabIndex = 14;
            totalLabel.Text = "Total:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(566, 48);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(55, 13);
            label2.TabIndex = 18;
            label2.Text = "Unidades:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(744, 53);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(86, 13);
            label5.TabIndex = 27;
            label5.Text = "Precio Producto:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(855, 53);
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
            BeneficioKilo_LB.Size = new System.Drawing.Size(69, 13);
            BeneficioKilo_LB.TabIndex = 26;
            BeneficioKilo_LB.Text = "Beneficio Kg:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(15, 21);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(54, 13);
            label1.TabIndex = 28;
            label1.Text = "Coste Kg:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(589, 104);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(32, 13);
            label4.TabIndex = 37;
            label4.Text = "Dto.:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(79, 237);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(51, 13);
            label7.TabIndex = 40;
            label7.Text = "Almacén:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(65, 265);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(65, 13);
            label8.TabIndex = 42;
            label8.Text = "Expediente:";
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
            this.PanelExpedientes.Size = new System.Drawing.Size(1032, 450);
            this.PanelExpedientes.SplitterDistance = 377;
            this.PanelExpedientes.TabIndex = 2;
            // 
            // ProductosPanel
            // 
            this.ProductosPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProductosPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.ProductosPanel.IsSplitterFixed = true;
            this.ProductosPanel.Location = new System.Drawing.Point(0, 0);
            this.ProductosPanel.Name = "ProductosPanel";
            this.ProductosPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ProductosPanel.Panel1
            // 
            this.ProductosPanel.Panel1.Controls.Add(this.Producto_DGW);
            this.ProductosPanel.Panel1.Controls.Add(this.Partida_DGW);
            this.ProductosPanel.Panel1MinSize = 58;
            // 
            // ProductosPanel.Panel2
            // 
            this.ProductosPanel.Panel2.AutoScroll = true;
            this.ProductosPanel.Panel2.Controls.Add(this.Productos_BT);
            this.ProductosPanel.Panel2.Controls.Add(this.LineaPedido_GB);
            this.ProductosPanel.Size = new System.Drawing.Size(1032, 450);
            this.ProductosPanel.SplitterDistance = 58;
            this.ProductosPanel.TabIndex = 3;
            // 
            // Producto_DGW
            // 
            this.Producto_DGW.AllowUserToAddRows = false;
            this.Producto_DGW.AllowUserToDeleteRows = false;
            this.Producto_DGW.AllowUserToResizeRows = false;
            this.Producto_DGW.AutoGenerateColumns = false;
            this.Producto_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoDataGridViewTextBoxColumn,
            this.descripcionDataGridViewTextBoxColumn,
            this.observacionesDataGridViewTextBoxColumn});
            this.Producto_DGW.DataSource = this.Datos_Productos;
            this.Producto_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Producto_DGW.Enabled = false;
            this.Producto_DGW.Location = new System.Drawing.Point(0, 0);
            this.Producto_DGW.MultiSelect = false;
            this.Producto_DGW.Name = "Producto_DGW";
            this.Producto_DGW.ReadOnly = true;
            this.Producto_DGW.RowHeadersVisible = false;
            this.Producto_DGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Producto_DGW.Size = new System.Drawing.Size(1032, 58);
            this.Producto_DGW.TabIndex = 2;
            // 
            // codigoDataGridViewTextBoxColumn
            // 
            this.codigoDataGridViewTextBoxColumn.DataPropertyName = "Codigo";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codigoDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.codigoDataGridViewTextBoxColumn.HeaderText = "Codigo";
            this.codigoDataGridViewTextBoxColumn.Name = "codigoDataGridViewTextBoxColumn";
            this.codigoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descripcionDataGridViewTextBoxColumn
            // 
            this.descripcionDataGridViewTextBoxColumn.DataPropertyName = "Descripcion";
            this.descripcionDataGridViewTextBoxColumn.HeaderText = "Nombre";
            this.descripcionDataGridViewTextBoxColumn.Name = "descripcionDataGridViewTextBoxColumn";
            this.descripcionDataGridViewTextBoxColumn.ReadOnly = true;
            this.descripcionDataGridViewTextBoxColumn.Width = 500;
            // 
            // observacionesDataGridViewTextBoxColumn
            // 
            this.observacionesDataGridViewTextBoxColumn.DataPropertyName = "Observaciones";
            this.observacionesDataGridViewTextBoxColumn.HeaderText = "Observaciones";
            this.observacionesDataGridViewTextBoxColumn.Name = "observacionesDataGridViewTextBoxColumn";
            this.observacionesDataGridViewTextBoxColumn.ReadOnly = true;
            this.observacionesDataGridViewTextBoxColumn.Width = 428;
            // 
            // Datos_Productos
            // 
            this.Datos_Productos.DataSource = typeof(moleQule.Library.Store.ProductInfo);
            // 
            // Partida_DGW
            // 
            this.Partida_DGW.AllowUserToAddRows = false;
            this.Partida_DGW.AllowUserToDeleteRows = false;
            this.Partida_DGW.AllowUserToResizeRows = false;
            this.Partida_DGW.AutoGenerateColumns = false;
            this.Partida_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.ExpedienteCol,
            this.PrecioVentaKilo,
            this.KilosIniciales,
            this.StockKilos,
            this.BultosIniciales,
            this.StockBultos,
            this.KiloPorBulto});
            this.Partida_DGW.DataSource = this.Datos_Partida;
            this.Partida_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Partida_DGW.Enabled = false;
            this.Partida_DGW.Location = new System.Drawing.Point(0, 0);
            this.Partida_DGW.MultiSelect = false;
            this.Partida_DGW.Name = "Partida_DGW";
            this.Partida_DGW.ReadOnly = true;
            this.Partida_DGW.RowHeadersVisible = false;
            this.Partida_DGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Partida_DGW.Size = new System.Drawing.Size(1032, 58);
            this.Partida_DGW.TabIndex = 1;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C5";
            this.PrecioVentaKilo.DefaultCellStyle = dataGridViewCellStyle2;
            this.PrecioVentaKilo.HeaderText = "Precio Venta Kg";
            this.PrecioVentaKilo.Name = "PrecioVentaKilo";
            this.PrecioVentaKilo.ReadOnly = true;
            this.PrecioVentaKilo.Width = 75;
            // 
            // KilosIniciales
            // 
            this.KilosIniciales.DataPropertyName = "KilosIniciales";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.KilosIniciales.DefaultCellStyle = dataGridViewCellStyle3;
            this.KilosIniciales.HeaderText = "Kg Iniciales";
            this.KilosIniciales.Name = "KilosIniciales";
            this.KilosIniciales.ReadOnly = true;
            this.KilosIniciales.Width = 80;
            // 
            // StockKilos
            // 
            this.StockKilos.DataPropertyName = "StockKilos";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.StockKilos.DefaultCellStyle = dataGridViewCellStyle4;
            this.StockKilos.HeaderText = "Stock Kg";
            this.StockKilos.Name = "StockKilos";
            this.StockKilos.ReadOnly = true;
            this.StockKilos.Width = 80;
            // 
            // BultosIniciales
            // 
            this.BultosIniciales.DataPropertyName = "BultosIniciales";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.BultosIniciales.DefaultCellStyle = dataGridViewCellStyle5;
            this.BultosIniciales.HeaderText = "Bultos Iniciales";
            this.BultosIniciales.Name = "BultosIniciales";
            this.BultosIniciales.ReadOnly = true;
            this.BultosIniciales.Width = 80;
            // 
            // StockBultos
            // 
            this.StockBultos.DataPropertyName = "StockBultos";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N4";
            dataGridViewCellStyle6.NullValue = null;
            this.StockBultos.DefaultCellStyle = dataGridViewCellStyle6;
            this.StockBultos.HeaderText = "Stock Bultos";
            this.StockBultos.Name = "StockBultos";
            this.StockBultos.ReadOnly = true;
            this.StockBultos.Width = 80;
            // 
            // KiloPorBulto
            // 
            this.KiloPorBulto.DataPropertyName = "KilosPorBulto";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.KiloPorBulto.DefaultCellStyle = dataGridViewCellStyle7;
            this.KiloPorBulto.HeaderText = "Kg / Bulto";
            this.KiloPorBulto.Name = "KiloPorBulto";
            this.KiloPorBulto.ReadOnly = true;
            this.KiloPorBulto.Width = 80;
            // 
            // Datos_Partida
            // 
            this.Datos_Partida.DataSource = typeof(moleQule.Library.Store.BatchInfo);
            this.Datos_Partida.CurrentChanged += new System.EventHandler(this.Datos_Partida_CurrentChanged);
            // 
            // Productos_BT
            // 
            this.Productos_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Productos_BT.Location = new System.Drawing.Point(455, 5);
            this.Productos_BT.Name = "Productos_BT";
            this.Productos_BT.Size = new System.Drawing.Size(123, 43);
            this.Productos_BT.TabIndex = 0;
            this.Productos_BT.Text = "Productos";
            this.Productos_BT.UseVisualStyleBackColor = true;
            this.Productos_BT.Click += new System.EventHandler(this.Productos_BT_Click);
            // 
            // LineaPedido_GB
            // 
            this.LineaPedido_GB.Controls.Add(this.PDescuento_NTB);
            this.LineaPedido_GB.Controls.Add(this.Expediente_BT);
            this.LineaPedido_GB.Controls.Add(label8);
            this.LineaPedido_GB.Controls.Add(this.Expediente_TB);
            this.LineaPedido_GB.Controls.Add(label7);
            this.LineaPedido_GB.Controls.Add(this.textBox1);
            this.LineaPedido_GB.Controls.Add(this.numericTextBox1);
            this.LineaPedido_GB.Controls.Add(label4);
            this.LineaPedido_GB.Controls.Add(this.Impuestos_BT);
            this.LineaPedido_GB.Controls.Add(this.FacturarPeso_CkB);
            this.LineaPedido_GB.Controls.Add(this.Detalles_GB);
            this.LineaPedido_GB.Controls.Add(this.Detalles_BT);
            this.LineaPedido_GB.Controls.Add(label6);
            this.LineaPedido_GB.Controls.Add(this.PrecioCliente_NTB);
            this.LineaPedido_GB.Controls.Add(label5);
            this.LineaPedido_GB.Controls.Add(this.PrecioProducto_NTB);
            this.LineaPedido_GB.Controls.Add(this.Igic_Aplicado_LB);
            this.LineaPedido_GB.Controls.Add(label2);
            this.LineaPedido_GB.Controls.Add(this.Pieces_NTB);
            this.LineaPedido_GB.Controls.Add(totalLabel);
            this.LineaPedido_GB.Controls.Add(this.Total_NTB);
            this.LineaPedido_GB.Controls.Add(subtotalLabel);
            this.LineaPedido_GB.Controls.Add(this.subtotalNumericTextBox);
            this.LineaPedido_GB.Controls.Add(precioLabel);
            this.LineaPedido_GB.Controls.Add(this.Precio_NTB);
            this.LineaPedido_GB.Controls.Add(conceptoLabel);
            this.LineaPedido_GB.Controls.Add(this.Concepto_TB);
            this.LineaPedido_GB.Controls.Add(cantidadLabel);
            this.LineaPedido_GB.Controls.Add(this.Kilos_NTB);
            this.LineaPedido_GB.Location = new System.Drawing.Point(28, 52);
            this.LineaPedido_GB.Name = "LineaPedido_GB";
            this.LineaPedido_GB.Size = new System.Drawing.Size(970, 314);
            this.LineaPedido_GB.TabIndex = 2;
            this.LineaPedido_GB.TabStop = false;
            this.LineaPedido_GB.Text = "Datos";
            // 
            // PDescuento_NTB
            // 
            this.PDescuento_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PDescuento", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.PDescuento_NTB.ForeColor = System.Drawing.Color.Navy;
            this.PDescuento_NTB.Location = new System.Drawing.Point(627, 100);
            this.PDescuento_NTB.Name = "PDescuento_NTB";
            this.PDescuento_NTB.Size = new System.Drawing.Size(100, 21);
            this.PDescuento_NTB.TabIndex = 124;
            this.PDescuento_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PDescuento_NTB.TextIsCurrency = false;
            this.PDescuento_NTB.TextIsInteger = false;
            // 
            // Expediente_BT
            // 
            this.Expediente_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Expediente_BT.Location = new System.Drawing.Point(383, 262);
            this.Expediente_BT.Name = "Expediente_BT";
            this.Expediente_BT.Size = new System.Drawing.Size(28, 21);
            this.Expediente_BT.TabIndex = 123;
            this.Expediente_BT.UseVisualStyleBackColor = true;
            this.Expediente_BT.Click += new System.EventHandler(this.Expediente_BT_Click);
            // 
            // Expediente_TB
            // 
            this.Expediente_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Expediente", true));
            this.Expediente_TB.Enabled = false;
            this.Expediente_TB.ForeColor = System.Drawing.Color.Navy;
            this.Expediente_TB.Location = new System.Drawing.Point(136, 262);
            this.Expediente_TB.Multiline = true;
            this.Expediente_TB.Name = "Expediente_TB";
            this.Expediente_TB.ReadOnly = true;
            this.Expediente_TB.Size = new System.Drawing.Size(241, 22);
            this.Expediente_TB.TabIndex = 41;
            this.Expediente_TB.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Almacen", true));
            this.textBox1.Enabled = false;
            this.textBox1.ForeColor = System.Drawing.Color.Navy;
            this.textBox1.Location = new System.Drawing.Point(136, 234);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(241, 22);
            this.textBox1.TabIndex = 39;
            this.textBox1.TabStop = false;
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Impuestos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.numericTextBox1.Enabled = false;
            this.numericTextBox1.Location = new System.Drawing.Point(674, 155);
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.ReadOnly = true;
            this.numericTextBox1.Size = new System.Drawing.Size(53, 21);
            this.numericTextBox1.TabIndex = 38;
            this.numericTextBox1.TabStop = false;
            this.numericTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextBox1.TextIsCurrency = false;
            this.numericTextBox1.TextIsInteger = false;
            // 
            // Impuestos_BT
            // 
            this.Impuestos_BT.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PImpuestos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Impuestos_BT.Location = new System.Drawing.Point(628, 154);
            this.Impuestos_BT.Name = "Impuestos_BT";
            this.Impuestos_BT.Size = new System.Drawing.Size(40, 23);
            this.Impuestos_BT.TabIndex = 6;
            this.Impuestos_BT.Tag = "NO FORMAT";
            this.Impuestos_BT.UseVisualStyleBackColor = true;
            this.Impuestos_BT.Click += new System.EventHandler(this.Impuestos_BT_Click);
            // 
            // FacturarPeso_CkB
            // 
            this.FacturarPeso_CkB.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.Datos, "FacturacionPeso", true));
            this.FacturarPeso_CkB.Location = new System.Drawing.Point(737, 19);
            this.FacturarPeso_CkB.Name = "FacturarPeso_CkB";
            this.FacturarPeso_CkB.Size = new System.Drawing.Size(112, 17);
            this.FacturarPeso_CkB.TabIndex = 2;
            this.FacturarPeso_CkB.Text = "Facturar por Peso";
            this.FacturarPeso_CkB.UseVisualStyleBackColor = true;
            this.FacturarPeso_CkB.CheckedChanged += new System.EventHandler(this.FBultos_CKB_CheckedChanged);
            // 
            // Detalles_GB
            // 
            this.Detalles_GB.Controls.Add(label1);
            this.Detalles_GB.Controls.Add(this.Gastos_TB);
            this.Detalles_GB.Controls.Add(BeneficioKilo_LB);
            this.Detalles_GB.Controls.Add(this.BeneficioKilo_TB);
            this.Detalles_GB.Controls.Add(label3);
            this.Detalles_GB.Controls.Add(this.Beneficio_TB);
            this.Detalles_GB.Location = new System.Drawing.Point(619, 220);
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
            this.Detalles_BT.Location = new System.Drawing.Point(504, 241);
            this.Detalles_BT.Name = "Detalles_BT";
            this.Detalles_BT.Size = new System.Drawing.Size(100, 37);
            this.Detalles_BT.TabIndex = 7;
            this.Detalles_BT.Text = "Detalles";
            this.Detalles_BT.UseVisualStyleBackColor = true;
            this.Detalles_BT.Click += new System.EventHandler(this.Detalles_BT_Click);
            // 
            // PrecioCliente_NTB
            // 
            this.PrecioCliente_NTB.Enabled = false;
            this.PrecioCliente_NTB.Location = new System.Drawing.Point(843, 73);
            this.PrecioCliente_NTB.Name = "PrecioCliente_NTB";
            this.PrecioCliente_NTB.ReadOnly = true;
            this.PrecioCliente_NTB.Size = new System.Drawing.Size(100, 21);
            this.PrecioCliente_NTB.TabIndex = 28;
            this.PrecioCliente_NTB.TabStop = false;
            this.PrecioCliente_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrecioCliente_NTB.TextIsCurrency = false;
            this.PrecioCliente_NTB.TextIsInteger = false;
            // 
            // PrecioProducto_NTB
            // 
            this.PrecioProducto_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Partida, "PrecioVentaKilo", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N5"));
            this.PrecioProducto_NTB.Enabled = false;
            this.PrecioProducto_NTB.Location = new System.Drawing.Point(737, 73);
            this.PrecioProducto_NTB.Name = "PrecioProducto_NTB";
            this.PrecioProducto_NTB.ReadOnly = true;
            this.PrecioProducto_NTB.Size = new System.Drawing.Size(100, 21);
            this.PrecioProducto_NTB.TabIndex = 26;
            this.PrecioProducto_NTB.TabStop = false;
            this.PrecioProducto_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrecioProducto_NTB.TextIsCurrency = false;
            this.PrecioProducto_NTB.TextIsInteger = false;
            // 
            // Igic_Aplicado_LB
            // 
            this.Igic_Aplicado_LB.AutoSize = true;
            this.Igic_Aplicado_LB.Location = new System.Drawing.Point(560, 159);
            this.Igic_Aplicado_LB.Name = "Igic_Aplicado_LB";
            this.Igic_Aplicado_LB.Size = new System.Drawing.Size(61, 13);
            this.Igic_Aplicado_LB.TabIndex = 20;
            this.Igic_Aplicado_LB.Text = "Impuestos:";
            // 
            // Pieces_NTB
            // 
            this.Pieces_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CantidadBultos", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N2"));
            this.Pieces_NTB.Enabled = false;
            this.Pieces_NTB.ForeColor = System.Drawing.Color.Navy;
            this.Pieces_NTB.Location = new System.Drawing.Point(627, 45);
            this.Pieces_NTB.Name = "Pieces_NTB";
            this.Pieces_NTB.Size = new System.Drawing.Size(100, 21);
            this.Pieces_NTB.TabIndex = 3;
            this.Pieces_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Pieces_NTB.TextIsCurrency = false;
            this.Pieces_NTB.TextIsInteger = false;
            // 
            // Total_NTB
            // 
            this.Total_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Total", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Total_NTB.Enabled = false;
            this.Total_NTB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Total_NTB.Location = new System.Drawing.Point(628, 183);
            this.Total_NTB.Name = "Total_NTB";
            this.Total_NTB.ReadOnly = true;
            this.Total_NTB.Size = new System.Drawing.Size(100, 22);
            this.Total_NTB.TabIndex = 5;
            this.Total_NTB.TabStop = false;
            this.Total_NTB.Tag = "NO FORMAT";
            this.Total_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Total_NTB.TextIsCurrency = false;
            this.Total_NTB.TextIsInteger = false;
            // 
            // subtotalNumericTextBox
            // 
            this.subtotalNumericTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BaseImponible", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.subtotalNumericTextBox.Enabled = false;
            this.subtotalNumericTextBox.Location = new System.Drawing.Point(628, 127);
            this.subtotalNumericTextBox.Name = "subtotalNumericTextBox";
            this.subtotalNumericTextBox.ReadOnly = true;
            this.subtotalNumericTextBox.Size = new System.Drawing.Size(100, 21);
            this.subtotalNumericTextBox.TabIndex = 4;
            this.subtotalNumericTextBox.TabStop = false;
            this.subtotalNumericTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.subtotalNumericTextBox.TextIsCurrency = false;
            this.subtotalNumericTextBox.TextIsInteger = false;
            // 
            // Precio_NTB
            // 
            this.Precio_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Precio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "N5"));
            this.Precio_NTB.Location = new System.Drawing.Point(627, 73);
            this.Precio_NTB.Name = "Precio_NTB";
            this.Precio_NTB.Size = new System.Drawing.Size(100, 21);
            this.Precio_NTB.TabIndex = 4;
            this.Precio_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Precio_NTB.TextIsCurrency = false;
            this.Precio_NTB.TextIsInteger = false;
            // 
            // Concepto_TB
            // 
            this.Concepto_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Concepto", true));
            this.Concepto_TB.ForeColor = System.Drawing.Color.Navy;
            this.Concepto_TB.Location = new System.Drawing.Point(19, 37);
            this.Concepto_TB.Multiline = true;
            this.Concepto_TB.Name = "Concepto_TB";
            this.Concepto_TB.Size = new System.Drawing.Size(463, 168);
            this.Concepto_TB.TabIndex = 0;
            // 
            // Kilos_NTB
            // 
            this.Kilos_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CantidadKilos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Kilos_NTB.Enabled = false;
            this.Kilos_NTB.ForeColor = System.Drawing.Color.Navy;
            this.Kilos_NTB.Location = new System.Drawing.Point(628, 17);
            this.Kilos_NTB.Name = "Kilos_NTB";
            this.Kilos_NTB.Size = new System.Drawing.Size(100, 21);
            this.Kilos_NTB.TabIndex = 1;
            this.Kilos_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Kilos_NTB.TextIsCurrency = false;
            this.Kilos_NTB.TextIsInteger = false;
            // 
            // LineaPedidoUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1034, 493);
            this.ControlBox = false;
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "LineaPedidoUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.ShowInTaskbar = false;
            this.Text = "Concepto de Albarán";
            this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
            this.Controls.SetChildIndex(this.PanelesV, 0);
            this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
            this.Controls.SetChildIndex(this.Progress_PB, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            this.ProgressBK_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            this.PanelExpedientes.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelExpedientes)).EndInit();
            this.PanelExpedientes.ResumeLayout(false);
            this.ProductosPanel.Panel1.ResumeLayout(false);
            this.ProductosPanel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProductosPanel)).EndInit();
            this.ProductosPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Producto_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Productos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Partida_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Partida)).EndInit();
            this.LineaPedido_GB.ResumeLayout(false);
            this.LineaPedido_GB.PerformLayout();
            this.Detalles_GB.ResumeLayout(false);
            this.Detalles_GB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private moleQule.Face.Controls.NumericTextBox subtotalNumericTextBox;
        private moleQule.Face.Controls.NumericTextBox Precio_NTB;
        private System.Windows.Forms.TextBox Concepto_TB;
        private moleQule.Face.Controls.NumericTextBox Total_NTB;
		private System.Windows.Forms.Label Igic_Aplicado_LB;
        private moleQule.Face.Controls.NumericTextBox PrecioProducto_NTB;
        private System.Windows.Forms.Button Detalles_BT;
        private System.Windows.Forms.GroupBox Detalles_GB;
        private System.Windows.Forms.TextBox Gastos_TB;
        private System.Windows.Forms.TextBox BeneficioKilo_TB;
        private System.Windows.Forms.TextBox Beneficio_TB;
        protected System.Windows.Forms.SplitContainer PanelExpedientes;
        protected System.Windows.Forms.SplitContainer ProductosPanel;
        protected System.Windows.Forms.GroupBox LineaPedido_GB;
        protected System.Windows.Forms.BindingSource Datos_Partida;
		protected moleQule.Face.Controls.NumericTextBox PrecioCliente_NTB;
        protected System.Windows.Forms.DataGridView Partida_DGW;
        protected System.Windows.Forms.Button Productos_BT;
        protected moleQule.Face.Controls.NumericTextBox Kilos_NTB;
        protected moleQule.Face.Controls.NumericTextBox Pieces_NTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpedienteCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVentaKilo;
        private System.Windows.Forms.DataGridViewTextBoxColumn KilosIniciales;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockKilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn BultosIniciales;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockBultos;
        private System.Windows.Forms.DataGridViewTextBoxColumn KiloPorBulto;
		private System.Windows.Forms.Button Impuestos_BT;
		protected System.Windows.Forms.CheckBox FacturarPeso_CkB;
		protected System.Windows.Forms.BindingSource Datos_Productos;
		protected System.Windows.Forms.DataGridView Producto_DGW;
		private System.Windows.Forms.DataGridViewTextBoxColumn codigoDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn descripcionDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn observacionesDataGridViewTextBoxColumn;
		private Controls.NumericTextBox numericTextBox1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox Expediente_TB;
		protected System.Windows.Forms.Button Expediente_BT;
		protected Controls.NumericTextBox PDescuento_NTB;


    }
}
