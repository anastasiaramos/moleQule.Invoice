namespace moleQule.Face.Invoice
{
    partial class BankLinesActionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankLinesActionForm));
            this.Cuenta_GB = new System.Windows.Forms.GroupBox();
            this.Cuenta_TB = new System.Windows.Forms.TextBox();
            this.Cuenta_BT = new System.Windows.Forms.Button();
            this.TodosCuentas_CkB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FFinal_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.FInicial_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.Fechas_GB = new System.Windows.Forms.GroupBox();
            this.Datos_MedioPago = new System.Windows.Forms.BindingSource(this.components);
            this.Datos_TipoMovimiento = new System.Windows.Forms.BindingSource(this.components);
            this.Cliente_GB = new System.Windows.Forms.GroupBox();
            this.Cliente_TB = new System.Windows.Forms.TextBox();
            this.Cliente_BT = new System.Windows.Forms.Button();
            this.TodosCliente_CkB = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Acreedor_GB = new System.Windows.Forms.GroupBox();
            this.TodosAcreedor_CkB = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Acreedor_TB = new System.Windows.Forms.TextBox();
            this.Acreedor_BT = new System.Windows.Forms.Button();
            this.Datos_TipoTitular = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TipoTitular_CB = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.MedioPago_CB = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TipoMovimiento_CB = new System.Windows.Forms.ComboBox();
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
            this.Cuenta_GB.SuspendLayout();
            this.Fechas_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TipoMovimiento)).BeginInit();
            this.Cliente_GB.SuspendLayout();
            this.Acreedor_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TipoTitular)).BeginInit();
            this.groupBox3.SuspendLayout();
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
            this.Source_GB.Controls.Add(this.groupBox3);
            this.Source_GB.Controls.Add(this.Acreedor_GB);
            this.Source_GB.Controls.Add(this.Cliente_GB);
            this.Source_GB.Controls.Add(this.Fechas_GB);
            this.Source_GB.Controls.Add(this.Cuenta_GB);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(661, 404);
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
            this.PanelesV.Size = new System.Drawing.Size(663, 446);
            this.PanelesV.SplitterDistance = 406;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(127, 24);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(663, 446);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(299, 274);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(299, 189);
            // 
            // Cuenta_GB
            // 
            this.Cuenta_GB.Controls.Add(this.Cuenta_TB);
            this.Cuenta_GB.Controls.Add(this.Cuenta_BT);
            this.Cuenta_GB.Controls.Add(this.TodosCuentas_CkB);
            this.Cuenta_GB.Controls.Add(this.label2);
            this.Cuenta_GB.Location = new System.Drawing.Point(46, 148);
            this.Cuenta_GB.Name = "Cuenta_GB";
            this.Cuenta_GB.Size = new System.Drawing.Size(568, 51);
            this.Cuenta_GB.TabIndex = 24;
            this.Cuenta_GB.TabStop = false;
            this.Cuenta_GB.Text = "Cuentas";
            // 
            // Cuenta_TB
            // 
            this.Cuenta_TB.Location = new System.Drawing.Point(86, 19);
            this.Cuenta_TB.Name = "Cuenta_TB";
            this.Cuenta_TB.ReadOnly = true;
            this.Cuenta_TB.Size = new System.Drawing.Size(290, 21);
            this.Cuenta_TB.TabIndex = 16;
            // 
            // Cuenta_BT
            // 
            this.Cuenta_BT.Enabled = false;
            this.Cuenta_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Cuenta_BT.Location = new System.Drawing.Point(382, 18);
            this.Cuenta_BT.Name = "Cuenta_BT";
            this.Cuenta_BT.Size = new System.Drawing.Size(42, 23);
            this.Cuenta_BT.TabIndex = 15;
            this.Cuenta_BT.UseVisualStyleBackColor = true;
            this.Cuenta_BT.Click += new System.EventHandler(this.Cuenta_BT_Click);
            // 
            // TodosCuentas_CkB
            // 
            this.TodosCuentas_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TodosCuentas_CkB.AutoSize = true;
            this.TodosCuentas_CkB.Checked = true;
            this.TodosCuentas_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodosCuentas_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosCuentas_CkB.Location = new System.Drawing.Point(455, 21);
            this.TodosCuentas_CkB.Name = "TodosCuentas_CkB";
            this.TodosCuentas_CkB.Size = new System.Drawing.Size(95, 17);
            this.TodosCuentas_CkB.TabIndex = 14;
            this.TodosCuentas_CkB.Text = "Mostrar Todos";
            this.TodosCuentas_CkB.UseVisualStyleBackColor = true;
            this.TodosCuentas_CkB.CheckedChanged += new System.EventHandler(this.TodosCuenta_CkB_CheckedChanged);
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
            // Fechas_GB
            // 
            this.Fechas_GB.Controls.Add(this.FInicial_DTP);
            this.Fechas_GB.Controls.Add(this.label4);
            this.Fechas_GB.Controls.Add(this.FFinal_DTP);
            this.Fechas_GB.Controls.Add(this.label3);
            this.Fechas_GB.Location = new System.Drawing.Point(46, 319);
            this.Fechas_GB.Name = "Fechas_GB";
            this.Fechas_GB.Size = new System.Drawing.Size(568, 49);
            this.Fechas_GB.TabIndex = 37;
            this.Fechas_GB.TabStop = false;
            this.Fechas_GB.Text = "Fecha";
            // 
            // Datos_MedioPago
            // 
            this.Datos_MedioPago.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
            // 
            // Datos_TipoMovimiento
            // 
            this.Datos_TipoMovimiento.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
            // 
            // Cliente_GB
            // 
            this.Cliente_GB.Controls.Add(this.Cliente_TB);
            this.Cliente_GB.Controls.Add(this.Cliente_BT);
            this.Cliente_GB.Controls.Add(this.TodosCliente_CkB);
            this.Cliente_GB.Controls.Add(this.label1);
            this.Cliente_GB.Location = new System.Drawing.Point(46, 205);
            this.Cliente_GB.Name = "Cliente_GB";
            this.Cliente_GB.Size = new System.Drawing.Size(568, 51);
            this.Cliente_GB.TabIndex = 39;
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
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Selección:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Acreedor_GB
            // 
            this.Acreedor_GB.Controls.Add(this.TodosAcreedor_CkB);
            this.Acreedor_GB.Controls.Add(this.label11);
            this.Acreedor_GB.Controls.Add(this.Acreedor_TB);
            this.Acreedor_GB.Controls.Add(this.Acreedor_BT);
            this.Acreedor_GB.Location = new System.Drawing.Point(46, 262);
            this.Acreedor_GB.Name = "Acreedor_GB";
            this.Acreedor_GB.Size = new System.Drawing.Size(568, 51);
            this.Acreedor_GB.TabIndex = 42;
            this.Acreedor_GB.TabStop = false;
            this.Acreedor_GB.Text = "Acreedores";
            // 
            // TodosAcreedor_CkB
            // 
            this.TodosAcreedor_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TodosAcreedor_CkB.AutoSize = true;
            this.TodosAcreedor_CkB.Checked = true;
            this.TodosAcreedor_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodosAcreedor_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosAcreedor_CkB.Location = new System.Drawing.Point(455, 20);
            this.TodosAcreedor_CkB.Name = "TodosAcreedor_CkB";
            this.TodosAcreedor_CkB.Size = new System.Drawing.Size(95, 17);
            this.TodosAcreedor_CkB.TabIndex = 15;
            this.TodosAcreedor_CkB.Text = "Mostrar Todos";
            this.TodosAcreedor_CkB.UseVisualStyleBackColor = true;
            this.TodosAcreedor_CkB.CheckedChanged += new System.EventHandler(this.TodosAcreedor_CkB_CheckedChanged);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(17, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 16);
            this.label11.TabIndex = 6;
            this.label11.Text = "Selección:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Acreedor_TB
            // 
            this.Acreedor_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Acreedor_TB.Location = new System.Drawing.Point(86, 19);
            this.Acreedor_TB.Name = "Acreedor_TB";
            this.Acreedor_TB.ReadOnly = true;
            this.Acreedor_TB.Size = new System.Drawing.Size(290, 21);
            this.Acreedor_TB.TabIndex = 0;
            // 
            // Acreedor_BT
            // 
            this.Acreedor_BT.Enabled = false;
            this.Acreedor_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Acreedor_BT.Location = new System.Drawing.Point(382, 17);
            this.Acreedor_BT.Name = "Acreedor_BT";
            this.Acreedor_BT.Size = new System.Drawing.Size(42, 23);
            this.Acreedor_BT.TabIndex = 1;
            this.Acreedor_BT.UseVisualStyleBackColor = true;
            this.Acreedor_BT.Click += new System.EventHandler(this.Acreedor_BT_Click);
            // 
            // Datos_TipoTitular
            // 
            this.Datos_TipoTitular.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TipoTitular_CB);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.MedioPago_CB);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.TipoMovimiento_CB);
            this.groupBox3.Location = new System.Drawing.Point(46, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(568, 114);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Movimiento";
            // 
            // TipoTitular_CB
            // 
            this.TipoTitular_CB.DataSource = this.Datos_TipoTitular;
            this.TipoTitular_CB.DisplayMember = "Texto";
            this.TipoTitular_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoTitular_CB.FormattingEnabled = true;
            this.TipoTitular_CB.Location = new System.Drawing.Point(189, 74);
            this.TipoTitular_CB.Name = "TipoTitular_CB";
            this.TipoTitular_CB.Size = new System.Drawing.Size(277, 21);
            this.TipoTitular_CB.TabIndex = 25;
            this.TipoTitular_CB.ValueMember = "Oid";
            this.TipoTitular_CB.SelectedIndexChanged += new System.EventHandler(this.TipoTitular_CB_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(119, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Tipo Titular:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(102, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Medio de Pago:";
            // 
            // MedioPago_CB
            // 
            this.MedioPago_CB.DataSource = this.Datos_MedioPago;
            this.MedioPago_CB.DisplayMember = "Texto";
            this.MedioPago_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MedioPago_CB.FormattingEnabled = true;
            this.MedioPago_CB.Location = new System.Drawing.Point(189, 47);
            this.MedioPago_CB.Name = "MedioPago_CB";
            this.MedioPago_CB.Size = new System.Drawing.Size(277, 21);
            this.MedioPago_CB.TabIndex = 23;
            this.MedioPago_CB.ValueMember = "Oid";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(152, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Tipo:";
            // 
            // TipoMovimiento_CB
            // 
            this.TipoMovimiento_CB.DataSource = this.Datos_TipoMovimiento;
            this.TipoMovimiento_CB.DisplayMember = "Texto";
            this.TipoMovimiento_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoMovimiento_CB.FormattingEnabled = true;
            this.TipoMovimiento_CB.Location = new System.Drawing.Point(189, 20);
            this.TipoMovimiento_CB.Name = "TipoMovimiento_CB";
            this.TipoMovimiento_CB.Size = new System.Drawing.Size(277, 21);
            this.TipoMovimiento_CB.TabIndex = 21;
            this.TipoMovimiento_CB.ValueMember = "Oid";
            this.TipoMovimiento_CB.SelectedIndexChanged += new System.EventHandler(this.TipoMovimiento_CB_SelectedIndexChanged);
            // 
            // MovsBancosActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(663, 446);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BankLinesActionForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Informe: Movimientos de Bancos";
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
            this.Cuenta_GB.ResumeLayout(false);
            this.Cuenta_GB.PerformLayout();
            this.Fechas_GB.ResumeLayout(false);
            this.Fechas_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TipoMovimiento)).EndInit();
            this.Cliente_GB.ResumeLayout(false);
            this.Cliente_GB.PerformLayout();
            this.Acreedor_GB.ResumeLayout(false);
            this.Acreedor_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TipoTitular)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Cuenta_GB;
        private System.Windows.Forms.CheckBox TodosCuentas_CkB;
        private System.Windows.Forms.Label label2;
        private moleQule.Face.Controls.mQDateTimePicker FFinal_DTP;
        private System.Windows.Forms.Label label3;
        private moleQule.Face.Controls.mQDateTimePicker FInicial_DTP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Cuenta_TB;
        private System.Windows.Forms.Button Cuenta_BT;
        private System.Windows.Forms.GroupBox Fechas_GB;
        private System.Windows.Forms.BindingSource Datos_TipoMovimiento;
        private System.Windows.Forms.GroupBox Cliente_GB;
        private System.Windows.Forms.TextBox Cliente_TB;
        private System.Windows.Forms.Button Cliente_BT;
        private System.Windows.Forms.CheckBox TodosCliente_CkB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox TipoMovimiento_CB;
        private System.Windows.Forms.GroupBox Acreedor_GB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Acreedor_TB;
        private System.Windows.Forms.Button Acreedor_BT;
        private System.Windows.Forms.BindingSource Datos_TipoTitular;
        private System.Windows.Forms.BindingSource Datos_MedioPago;
        private System.Windows.Forms.CheckBox TodosAcreedor_CkB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox MedioPago_CB;
        private System.Windows.Forms.ComboBox TipoTitular_CB;
        private System.Windows.Forms.Label label5;
    }
}
