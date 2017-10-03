namespace moleQule.Face.Invoice
{
    partial class SalesActionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesActionForm));
            this.Cliente_GB = new System.Windows.Forms.GroupBox();
            this.Cliente_TB = new System.Windows.Forms.TextBox();
            this.Cliente_BT = new System.Windows.Forms.Button();
            this.TodosCliente_CkB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FFinal_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.FInicial_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TipoProducto_CB = new System.Windows.Forms.ComboBox();
            this.Datos_TiposPro = new System.Windows.Forms.BindingSource(this.components);
            this.Producto_TB = new System.Windows.Forms.TextBox();
            this.Producto_BT = new System.Windows.Forms.Button();
            this.TodosProducto_CkB = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Expediente_RB = new System.Windows.Forms.RadioButton();
            this.Producto_RB = new System.Windows.Forms.RadioButton();
            this.Cliente_RB = new System.Windows.Forms.RadioButton();
            this.Detalle_GB = new System.Windows.Forms.GroupBox();
            this.Detalles_RB = new System.Windows.Forms.RadioButton();
            this.Mezclas_RB = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Serie_TB = new System.Windows.Forms.TextBox();
            this.Serie_BT = new System.Windows.Forms.Button();
            this.TodosSerie_CkB = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Tipo_GB = new System.Windows.Forms.GroupBox();
            this.TipoResumido_RB = new System.Windows.Forms.RadioButton();
            this.TipoDetallado_RB = new System.Windows.Forms.RadioButton();
            this.Fechas_GB = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TipoExpediente_CB = new System.Windows.Forms.ComboBox();
            this.Datos_TiposExp = new System.Windows.Forms.BindingSource(this.components);
            this.Expediente_TB = new System.Windows.Forms.TextBox();
            this.Expediente_BT = new System.Windows.Forms.Button();
            this.TodosExpediente_CkB = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Source_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            this.Cliente_GB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposPro)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.Detalle_GB.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.Tipo_GB.SuspendLayout();
            this.Fechas_GB.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposExp)).BeginInit();
            this.SuspendLayout();
            // 
            // Print_BT
            // 
            this.Print_BT.Enabled = true;
            this.Print_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ErrorMng_EP.SetIconAlignment(this.Print_BT, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.Print_BT.Location = new System.Drawing.Point(251, 2);
            this.HelpProvider.SetShowHelp(this.Print_BT, true);
            this.Print_BT.Visible = true;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ErrorMng_EP.SetIconAlignment(this.Submit_BT, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.Submit_BT.Location = new System.Drawing.Point(136, 2);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            this.Submit_BT.Visible = false;
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ErrorMng_EP.SetIconAlignment(this.Cancel_BT, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.Cancel_BT.Location = new System.Drawing.Point(21, 2);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Source_GB
            // 
            this.Source_GB.Controls.Add(this.groupBox5);
            this.Source_GB.Controls.Add(this.Fechas_GB);
            this.Source_GB.Controls.Add(this.Tipo_GB);
            this.Source_GB.Controls.Add(this.groupBox4);
            this.Source_GB.Controls.Add(this.Detalle_GB);
            this.Source_GB.Controls.Add(this.groupBox3);
            this.Source_GB.Controls.Add(this.groupBox1);
            this.Source_GB.Controls.Add(this.Cliente_GB);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(661, 513);
            this.Source_GB.Text = "";
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(663, 555);
            this.PanelesV.SplitterDistance = 515;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(127, 24);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(663, 555);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(299, 329);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(299, 244);
            // 
            // Cliente_GB
            // 
            this.Cliente_GB.Controls.Add(this.Cliente_TB);
            this.Cliente_GB.Controls.Add(this.Cliente_BT);
            this.Cliente_GB.Controls.Add(this.TodosCliente_CkB);
            this.Cliente_GB.Controls.Add(this.label2);
            this.Cliente_GB.Location = new System.Drawing.Point(46, 30);
            this.Cliente_GB.Name = "Cliente_GB";
            this.Cliente_GB.Size = new System.Drawing.Size(568, 51);
            this.Cliente_GB.TabIndex = 24;
            this.Cliente_GB.TabStop = false;
            this.Cliente_GB.Text = "Clientes";
            // 
            // Cliente_TB
            // 
            this.Cliente_TB.Location = new System.Drawing.Point(86, 19);
            this.Cliente_TB.Name = "Cliente_TB";
            this.Cliente_TB.ReadOnly = true;
            this.Cliente_TB.Size = new System.Drawing.Size(290, 21);
            this.Cliente_TB.TabIndex = 16;
            // 
            // Cliente_BT
            // 
            this.Cliente_BT.Enabled = false;
            this.Cliente_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Cliente_BT.Location = new System.Drawing.Point(382, 18);
            this.Cliente_BT.Name = "Cliente_BT";
            this.Cliente_BT.Size = new System.Drawing.Size(42, 23);
            this.Cliente_BT.TabIndex = 15;
            this.Cliente_BT.UseVisualStyleBackColor = true;
            this.Cliente_BT.Click += new System.EventHandler(this.Cliente_BT_Click);
            // 
            // TodosCliente_CkB
            // 
            this.TodosCliente_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TodosCliente_CkB.AutoSize = true;
            this.TodosCliente_CkB.Checked = true;
            this.TodosCliente_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodosCliente_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosCliente_CkB.Location = new System.Drawing.Point(455, 21);
            this.TodosCliente_CkB.Name = "TodosCliente_CkB";
            this.TodosCliente_CkB.Size = new System.Drawing.Size(95, 17);
            this.TodosCliente_CkB.TabIndex = 14;
            this.TodosCliente_CkB.Text = "Mostrar Todos";
            this.TodosCliente_CkB.UseVisualStyleBackColor = true;
            this.TodosCliente_CkB.CheckedChanged += new System.EventHandler(this.TodosCliente_CkB_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Selección:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FFinal_DTP
            // 
            this.FFinal_DTP.Checked = false;
            this.FFinal_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FFinal_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FFinal_DTP.Location = new System.Drawing.Point(382, 18);
            this.FFinal_DTP.Name = "FFinal_DTP";
            this.FFinal_DTP.ShowCheckBox = true;
            this.FFinal_DTP.Size = new System.Drawing.Size(112, 21);
            this.FFinal_DTP.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(311, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Fecha Final:";
            // 
            // FInicial_DTP
            // 
            this.FInicial_DTP.Checked = false;
            this.FInicial_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FInicial_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FInicial_DTP.Location = new System.Drawing.Point(150, 18);
            this.FInicial_DTP.Name = "FInicial_DTP";
            this.FInicial_DTP.ShowCheckBox = true;
            this.FInicial_DTP.Size = new System.Drawing.Size(112, 21);
            this.FInicial_DTP.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(74, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Fecha Inicial:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.TipoProducto_CB);
            this.groupBox1.Controls.Add(this.Producto_TB);
            this.groupBox1.Controls.Add(this.Producto_BT);
            this.groupBox1.Controls.Add(this.TodosProducto_CkB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(46, 186);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 86);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Productos";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(123, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Tipo de Producto:";
            // 
            // TipoProducto_CB
            // 
            this.TipoProducto_CB.DataSource = this.Datos_TiposPro;
            this.TipoProducto_CB.DisplayMember = "Texto";
            this.TipoProducto_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TipoProducto_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoProducto_CB.FormattingEnabled = true;
            this.TipoProducto_CB.Location = new System.Drawing.Point(221, 52);
            this.TipoProducto_CB.Name = "TipoProducto_CB";
            this.TipoProducto_CB.Size = new System.Drawing.Size(223, 21);
            this.TipoProducto_CB.TabIndex = 21;
            this.TipoProducto_CB.ValueMember = "Oid";
            // 
            // Datos_TiposPro
            // 
            this.Datos_TiposPro.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
            // 
            // Producto_TB
            // 
            this.Producto_TB.Location = new System.Drawing.Point(86, 19);
            this.Producto_TB.Name = "Producto_TB";
            this.Producto_TB.ReadOnly = true;
            this.Producto_TB.Size = new System.Drawing.Size(290, 21);
            this.Producto_TB.TabIndex = 18;
            // 
            // Producto_BT
            // 
            this.Producto_BT.Enabled = false;
            this.Producto_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Producto_BT.Location = new System.Drawing.Point(382, 18);
            this.Producto_BT.Name = "Producto_BT";
            this.Producto_BT.Size = new System.Drawing.Size(42, 23);
            this.Producto_BT.TabIndex = 17;
            this.Producto_BT.UseVisualStyleBackColor = true;
            this.Producto_BT.Click += new System.EventHandler(this.Producto_BT_Click);
            // 
            // TodosProducto_CkB
            // 
            this.TodosProducto_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TodosProducto_CkB.AutoSize = true;
            this.TodosProducto_CkB.Checked = true;
            this.TodosProducto_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodosProducto_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosProducto_CkB.Location = new System.Drawing.Point(455, 21);
            this.TodosProducto_CkB.Name = "TodosProducto_CkB";
            this.TodosProducto_CkB.Size = new System.Drawing.Size(95, 17);
            this.TodosProducto_CkB.TabIndex = 14;
            this.TodosProducto_CkB.Text = "Mostrar Todos";
            this.TodosProducto_CkB.UseVisualStyleBackColor = true;
            this.TodosProducto_CkB.CheckedChanged += new System.EventHandler(this.TodosProducto_CkB_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Selección:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Expediente_RB);
            this.groupBox3.Controls.Add(this.Producto_RB);
            this.groupBox3.Controls.Add(this.Cliente_RB);
            this.groupBox3.Location = new System.Drawing.Point(103, 399);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(100, 93);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Agrupar por";
            // 
            // Expediente_RB
            // 
            this.Expediente_RB.AutoSize = true;
            this.Expediente_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Expediente_RB.Location = new System.Drawing.Point(11, 66);
            this.Expediente_RB.Name = "Expediente_RB";
            this.Expediente_RB.Size = new System.Drawing.Size(79, 17);
            this.Expediente_RB.TabIndex = 2;
            this.Expediente_RB.Text = "Expediente";
            this.Expediente_RB.UseVisualStyleBackColor = true;
            // 
            // Producto_RB
            // 
            this.Producto_RB.AutoSize = true;
            this.Producto_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Producto_RB.Location = new System.Drawing.Point(11, 43);
            this.Producto_RB.Name = "Producto_RB";
            this.Producto_RB.Size = new System.Drawing.Size(68, 17);
            this.Producto_RB.TabIndex = 1;
            this.Producto_RB.Text = "Producto";
            this.Producto_RB.UseVisualStyleBackColor = true;
            // 
            // Cliente_RB
            // 
            this.Cliente_RB.AutoSize = true;
            this.Cliente_RB.Checked = true;
            this.Cliente_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cliente_RB.Location = new System.Drawing.Point(11, 20);
            this.Cliente_RB.Name = "Cliente_RB";
            this.Cliente_RB.Size = new System.Drawing.Size(58, 17);
            this.Cliente_RB.TabIndex = 0;
            this.Cliente_RB.TabStop = true;
            this.Cliente_RB.Text = "Cliente";
            this.Cliente_RB.UseVisualStyleBackColor = true;
            // 
            // Detalle_GB
            // 
            this.Detalle_GB.Controls.Add(this.Detalles_RB);
            this.Detalle_GB.Controls.Add(this.Mezclas_RB);
            this.Detalle_GB.Location = new System.Drawing.Point(245, 411);
            this.Detalle_GB.Name = "Detalle_GB";
            this.Detalle_GB.Size = new System.Drawing.Size(170, 69);
            this.Detalle_GB.TabIndex = 34;
            this.Detalle_GB.TabStop = false;
            this.Detalle_GB.Text = "Detalle";
            // 
            // Detalles_RB
            // 
            this.Detalles_RB.AutoSize = true;
            this.Detalles_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Detalles_RB.Location = new System.Drawing.Point(21, 43);
            this.Detalles_RB.Name = "Detalles_RB";
            this.Detalles_RB.Size = new System.Drawing.Size(129, 17);
            this.Detalles_RB.TabIndex = 1;
            this.Detalles_RB.Text = "Detallar componentes";
            this.Detalles_RB.UseVisualStyleBackColor = true;
            // 
            // Mezclas_RB
            // 
            this.Mezclas_RB.AutoSize = true;
            this.Mezclas_RB.Checked = true;
            this.Mezclas_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mezclas_RB.Location = new System.Drawing.Point(21, 20);
            this.Mezclas_RB.Name = "Mezclas_RB";
            this.Mezclas_RB.Size = new System.Drawing.Size(123, 17);
            this.Mezclas_RB.TabIndex = 0;
            this.Mezclas_RB.TabStop = true;
            this.Mezclas_RB.Text = "Agrupar por Mezclas";
            this.Mezclas_RB.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Serie_TB);
            this.groupBox4.Controls.Add(this.Serie_BT);
            this.groupBox4.Controls.Add(this.TodosSerie_CkB);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(46, 278);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(568, 51);
            this.groupBox4.TabIndex = 35;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Series";
            // 
            // Serie_TB
            // 
            this.Serie_TB.Location = new System.Drawing.Point(86, 19);
            this.Serie_TB.Name = "Serie_TB";
            this.Serie_TB.ReadOnly = true;
            this.Serie_TB.Size = new System.Drawing.Size(290, 21);
            this.Serie_TB.TabIndex = 18;
            // 
            // Serie_BT
            // 
            this.Serie_BT.Enabled = false;
            this.Serie_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Serie_BT.Location = new System.Drawing.Point(382, 18);
            this.Serie_BT.Name = "Serie_BT";
            this.Serie_BT.Size = new System.Drawing.Size(42, 23);
            this.Serie_BT.TabIndex = 17;
            this.Serie_BT.UseVisualStyleBackColor = true;
            this.Serie_BT.Click += new System.EventHandler(this.Serie_BT_Click);
            // 
            // TodosSerie_CkB
            // 
            this.TodosSerie_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TodosSerie_CkB.AutoSize = true;
            this.TodosSerie_CkB.Checked = true;
            this.TodosSerie_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodosSerie_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosSerie_CkB.Location = new System.Drawing.Point(455, 21);
            this.TodosSerie_CkB.Name = "TodosSerie_CkB";
            this.TodosSerie_CkB.Size = new System.Drawing.Size(95, 17);
            this.TodosSerie_CkB.TabIndex = 14;
            this.TodosSerie_CkB.Text = "Mostrar Todos";
            this.TodosSerie_CkB.UseVisualStyleBackColor = true;
            this.TodosSerie_CkB.CheckedChanged += new System.EventHandler(this.TodosSerie_GB_CheckedChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Selección:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Tipo_GB
            // 
            this.Tipo_GB.Controls.Add(this.TipoResumido_RB);
            this.Tipo_GB.Controls.Add(this.TipoDetallado_RB);
            this.Tipo_GB.Location = new System.Drawing.Point(457, 411);
            this.Tipo_GB.Name = "Tipo_GB";
            this.Tipo_GB.Size = new System.Drawing.Size(100, 69);
            this.Tipo_GB.TabIndex = 36;
            this.Tipo_GB.TabStop = false;
            this.Tipo_GB.Text = "Tipo";
            // 
            // TipoResumido_RB
            // 
            this.TipoResumido_RB.AutoSize = true;
            this.TipoResumido_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoResumido_RB.Location = new System.Drawing.Point(15, 41);
            this.TipoResumido_RB.Name = "TipoResumido_RB";
            this.TipoResumido_RB.Size = new System.Drawing.Size(71, 17);
            this.TipoResumido_RB.TabIndex = 1;
            this.TipoResumido_RB.Text = "Resumido";
            this.TipoResumido_RB.UseVisualStyleBackColor = true;
            // 
            // TipoDetallado_RB
            // 
            this.TipoDetallado_RB.AutoSize = true;
            this.TipoDetallado_RB.Checked = true;
            this.TipoDetallado_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoDetallado_RB.Location = new System.Drawing.Point(15, 22);
            this.TipoDetallado_RB.Name = "TipoDetallado_RB";
            this.TipoDetallado_RB.Size = new System.Drawing.Size(70, 17);
            this.TipoDetallado_RB.TabIndex = 0;
            this.TipoDetallado_RB.TabStop = true;
            this.TipoDetallado_RB.Text = "Detallado";
            this.TipoDetallado_RB.UseVisualStyleBackColor = true;
            // 
            // Fechas_GB
            // 
            this.Fechas_GB.Controls.Add(this.FInicial_DTP);
            this.Fechas_GB.Controls.Add(this.label4);
            this.Fechas_GB.Controls.Add(this.FFinal_DTP);
            this.Fechas_GB.Controls.Add(this.label3);
            this.Fechas_GB.Location = new System.Drawing.Point(46, 339);
            this.Fechas_GB.Name = "Fechas_GB";
            this.Fechas_GB.Size = new System.Drawing.Size(568, 49);
            this.Fechas_GB.TabIndex = 37;
            this.Fechas_GB.TabStop = false;
            this.Fechas_GB.Text = "Fechas";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.TipoExpediente_CB);
            this.groupBox5.Controls.Add(this.Expediente_TB);
            this.groupBox5.Controls.Add(this.Expediente_BT);
            this.groupBox5.Controls.Add(this.TodosExpediente_CkB);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Location = new System.Drawing.Point(46, 87);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(568, 86);
            this.groupBox5.TabIndex = 38;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Expedientes";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(117, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Tipo de Expediente:";
            // 
            // TipoExpediente_CB
            // 
            this.TipoExpediente_CB.DataSource = this.Datos_TiposExp;
            this.TipoExpediente_CB.DisplayMember = "Texto";
            this.TipoExpediente_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TipoExpediente_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoExpediente_CB.FormattingEnabled = true;
            this.TipoExpediente_CB.Location = new System.Drawing.Point(226, 52);
            this.TipoExpediente_CB.Name = "TipoExpediente_CB";
            this.TipoExpediente_CB.Size = new System.Drawing.Size(223, 21);
            this.TipoExpediente_CB.TabIndex = 19;
            this.TipoExpediente_CB.ValueMember = "Oid";
            // 
            // Datos_TiposExp
            // 
            this.Datos_TiposExp.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
            // 
            // Expediente_TB
            // 
            this.Expediente_TB.Location = new System.Drawing.Point(86, 19);
            this.Expediente_TB.Name = "Expediente_TB";
            this.Expediente_TB.ReadOnly = true;
            this.Expediente_TB.Size = new System.Drawing.Size(290, 21);
            this.Expediente_TB.TabIndex = 18;
            // 
            // Expediente_BT
            // 
            this.Expediente_BT.Enabled = false;
            this.Expediente_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Expediente_BT.Location = new System.Drawing.Point(382, 18);
            this.Expediente_BT.Name = "Expediente_BT";
            this.Expediente_BT.Size = new System.Drawing.Size(42, 23);
            this.Expediente_BT.TabIndex = 17;
            this.Expediente_BT.UseVisualStyleBackColor = true;
            this.Expediente_BT.Click += new System.EventHandler(this.Expediente_BT_Click);
            // 
            // TodosExpediente_CkB
            // 
            this.TodosExpediente_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TodosExpediente_CkB.AutoSize = true;
            this.TodosExpediente_CkB.Checked = true;
            this.TodosExpediente_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodosExpediente_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosExpediente_CkB.Location = new System.Drawing.Point(455, 21);
            this.TodosExpediente_CkB.Name = "TodosExpediente_CkB";
            this.TodosExpediente_CkB.Size = new System.Drawing.Size(95, 17);
            this.TodosExpediente_CkB.TabIndex = 14;
            this.TodosExpediente_CkB.Text = "Mostrar Todos";
            this.TodosExpediente_CkB.UseVisualStyleBackColor = true;
            this.TodosExpediente_CkB.CheckedChanged += new System.EventHandler(this.TodosExpediente_CkB_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Selección:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SalesActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(663, 555);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SalesActionForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Informe Estadístico: Ventas";
            this.Source_GB.ResumeLayout(false);
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
            this.Cliente_GB.ResumeLayout(false);
            this.Cliente_GB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposPro)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.Detalle_GB.ResumeLayout(false);
            this.Detalle_GB.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.Tipo_GB.ResumeLayout(false);
            this.Tipo_GB.PerformLayout();
            this.Fechas_GB.ResumeLayout(false);
            this.Fechas_GB.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposExp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Cliente_GB;
        private System.Windows.Forms.CheckBox TodosCliente_CkB;
        private System.Windows.Forms.Label label2;
        private moleQule.Face.Controls.mQDateTimePicker FFinal_DTP;
        private System.Windows.Forms.Label label3;
        private moleQule.Face.Controls.mQDateTimePicker FInicial_DTP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox TodosProducto_CkB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Producto_TB;
        private System.Windows.Forms.Button Producto_BT;
        private System.Windows.Forms.TextBox Cliente_TB;
        private System.Windows.Forms.Button Cliente_BT;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton Producto_RB;
        private System.Windows.Forms.RadioButton Cliente_RB;
        private System.Windows.Forms.GroupBox Detalle_GB;
        private System.Windows.Forms.RadioButton Detalles_RB;
        private System.Windows.Forms.RadioButton Mezclas_RB;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox Serie_TB;
        private System.Windows.Forms.Button Serie_BT;
        private System.Windows.Forms.CheckBox TodosSerie_CkB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox Fechas_GB;
        private System.Windows.Forms.GroupBox Tipo_GB;
        private System.Windows.Forms.RadioButton TipoResumido_RB;
        private System.Windows.Forms.RadioButton TipoDetallado_RB;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox Expediente_TB;
        private System.Windows.Forms.Button Expediente_BT;
        private System.Windows.Forms.CheckBox TodosExpediente_CkB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton Expediente_RB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox TipoProducto_CB;
        private System.Windows.Forms.BindingSource Datos_TiposPro;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox TipoExpediente_CB;
        private System.Windows.Forms.BindingSource Datos_TiposExp;
    }
}
