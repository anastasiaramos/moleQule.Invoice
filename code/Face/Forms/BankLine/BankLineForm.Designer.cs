namespace moleQule.Face.Invoice
{
    partial class BankLineForm
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
            System.Windows.Forms.Label abonadoLabel;
            System.Windows.Forms.Label auditadoLabel;
            System.Windows.Forms.Label observacionesLabel;
            System.Windows.Forms.Label codigoLabel;
            System.Windows.Forms.Label cuentaLabel;
            System.Windows.Forms.Label eMedioPagoLabelLabel;
            System.Windows.Forms.Label eTipoMovimientoBancoLabelLabel;
            System.Windows.Forms.Label titularLabel;
            System.Windows.Forms.Label fechaLabel;
            System.Windows.Forms.Label codigoOperacionLabel;
            System.Windows.Forms.Label auditorLabel1;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label5;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankLineForm));
            this.Importe_TB = new System.Windows.Forms.TextBox();
            this.Auditoria_GB = new System.Windows.Forms.GroupBox();
            this.auditorComboBox = new System.Windows.Forms.ComboBox();
            this.Datos_Usuarios = new System.Windows.Forms.BindingSource(this.components);
            this.Auditado_CkB = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Estado_BT = new System.Windows.Forms.Button();
            this.Estado_TB = new System.Windows.Forms.TextBox();
            this.Codigo_TB = new System.Windows.Forms.TextBox();
            this.Observaciones_TB = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TipoCuenta_BT = new System.Windows.Forms.Button();
            this.Creacion_DTP = new System.Windows.Forms.DateTimePicker();
            this.Entidad_TB = new System.Windows.Forms.TextBox();
            this.Cuenta_BT = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.codigoOperacionTextBox = new System.Windows.Forms.TextBox();
            this.Fecha_DTP = new System.Windows.Forms.DateTimePicker();
            this.Titular_TB = new System.Windows.Forms.TextBox();
            this.eTipoMovimientoBancoLabelTextBox = new System.Windows.Forms.TextBox();
            this.eMedioPagoLabelTextBox = new System.Windows.Forms.TextBox();
            this.Cuenta_TB = new System.Windows.Forms.TextBox();
            abonadoLabel = new System.Windows.Forms.Label();
            auditadoLabel = new System.Windows.Forms.Label();
            observacionesLabel = new System.Windows.Forms.Label();
            codigoLabel = new System.Windows.Forms.Label();
            cuentaLabel = new System.Windows.Forms.Label();
            eMedioPagoLabelLabel = new System.Windows.Forms.Label();
            eTipoMovimientoBancoLabelLabel = new System.Windows.Forms.Label();
            titularLabel = new System.Windows.Forms.Label();
            fechaLabel = new System.Windows.Forms.Label();
            codigoOperacionLabel = new System.Windows.Forms.Label();
            auditorLabel1 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Paneles2)).BeginInit();
            this.Paneles2.Panel1.SuspendLayout();
            this.Paneles2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            this.Auditoria_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Usuarios)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.AutoScroll = true;
            this.PanelesV.Panel1.Controls.Add(this.groupBox3);
            this.PanelesV.Panel1.Controls.Add(this.groupBox2);
            this.PanelesV.Panel1.Controls.Add(this.Auditoria_GB);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(794, 537);
            this.PanelesV.SplitterDistance = 482;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(353, 10);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(449, 10);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Paneles2
            // 
            this.ErrorMng_EP.SetError(this.Paneles2, "F1 Ayuda        ");
            // 
            // Paneles2.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, true);
            // 
            // Paneles2.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Paneles2, true);
            this.Paneles2.Size = new System.Drawing.Size(792, 52);
            this.Paneles2.SplitterDistance = 27;
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Imprimir_Button.Location = new System.Drawing.Point(257, 10);
            this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
            // 
            // Docs_BT
            // 
            this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Docs_BT.Location = new System.Drawing.Point(302, 10);
            this.HelpProvider.SetShowHelp(this.Docs_BT, true);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Invoice.BankLine);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(218, 96);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(794, 537);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(360, 317);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(360, 232);
            // 
            // abonadoLabel
            // 
            abonadoLabel.AutoSize = true;
            abonadoLabel.Location = new System.Drawing.Point(29, 123);
            abonadoLabel.Name = "abonadoLabel";
            abonadoLabel.Size = new System.Drawing.Size(49, 13);
            abonadoLabel.TabIndex = 0;
            abonadoLabel.Text = "Importe:";
            // 
            // auditadoLabel
            // 
            auditadoLabel.AutoSize = true;
            auditadoLabel.Location = new System.Drawing.Point(139, 41);
            auditadoLabel.Name = "auditadoLabel";
            auditadoLabel.Size = new System.Drawing.Size(54, 13);
            auditadoLabel.TabIndex = 4;
            auditadoLabel.Text = "Auditado:";
            // 
            // observacionesLabel
            // 
            observacionesLabel.AutoSize = true;
            observacionesLabel.Location = new System.Drawing.Point(20, 54);
            observacionesLabel.Name = "observacionesLabel";
            observacionesLabel.Size = new System.Drawing.Size(82, 13);
            observacionesLabel.TabIndex = 0;
            observacionesLabel.Text = "Observaciones:";
            // 
            // codigoLabel
            // 
            codigoLabel.AutoSize = true;
            codigoLabel.Location = new System.Drawing.Point(20, 27);
            codigoLabel.Name = "codigoLabel";
            codigoLabel.Size = new System.Drawing.Size(22, 13);
            codigoLabel.TabIndex = 2;
            codigoLabel.Text = "ID:";
            // 
            // cuentaLabel
            // 
            cuentaLabel.AutoSize = true;
            cuentaLabel.Location = new System.Drawing.Point(32, 150);
            cuentaLabel.Name = "cuentaLabel";
            cuentaLabel.Size = new System.Drawing.Size(46, 13);
            cuentaLabel.TabIndex = 2;
            cuentaLabel.Text = "Cuenta:";
            // 
            // eMedioPagoLabelLabel
            // 
            eMedioPagoLabelLabel.AutoSize = true;
            eMedioPagoLabelLabel.Location = new System.Drawing.Point(209, 122);
            eMedioPagoLabelLabel.Name = "eMedioPagoLabelLabel";
            eMedioPagoLabelLabel.Size = new System.Drawing.Size(66, 13);
            eMedioPagoLabelLabel.TabIndex = 4;
            eMedioPagoLabelLabel.Text = "Medio Pago:";
            eMedioPagoLabelLabel.UseWaitCursor = true;
            // 
            // eTipoMovimientoBancoLabelLabel
            // 
            eTipoMovimientoBancoLabelLabel.AutoSize = true;
            eTipoMovimientoBancoLabelLabel.Location = new System.Drawing.Point(274, 31);
            eTipoMovimientoBancoLabelLabel.Name = "eTipoMovimientoBancoLabelLabel";
            eTipoMovimientoBancoLabelLabel.Size = new System.Drawing.Size(31, 13);
            eTipoMovimientoBancoLabelLabel.TabIndex = 6;
            eTipoMovimientoBancoLabelLabel.Text = "Tipo:";
            // 
            // titularLabel
            // 
            titularLabel.AutoSize = true;
            titularLabel.Location = new System.Drawing.Point(37, 96);
            titularLabel.Name = "titularLabel";
            titularLabel.Size = new System.Drawing.Size(41, 13);
            titularLabel.TabIndex = 8;
            titularLabel.Text = "Titular:";
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Location = new System.Drawing.Point(260, 66);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new System.Drawing.Size(40, 13);
            fechaLabel.TabIndex = 10;
            fechaLabel.Text = "Fecha:";
            // 
            // codigoOperacionLabel
            // 
            codigoOperacionLabel.AutoSize = true;
            codigoOperacionLabel.Location = new System.Drawing.Point(47, 30);
            codigoOperacionLabel.Name = "codigoOperacionLabel";
            codigoOperacionLabel.Size = new System.Drawing.Size(96, 13);
            codigoOperacionLabel.TabIndex = 12;
            codigoOperacionLabel.Text = "Codigo Operación:";
            // 
            // auditorLabel1
            // 
            auditorLabel1.AutoSize = true;
            auditorLabel1.Location = new System.Drawing.Point(242, 41);
            auditorLabel1.Name = "auditorLabel1";
            auditorLabel1.Size = new System.Drawing.Size(46, 13);
            auditorLabel1.TabIndex = 5;
            auditorLabel1.Text = "Auditor:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(485, 150);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(95, 13);
            label1.TabIndex = 14;
            label1.Text = "ID Mov. Contable:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(445, 24);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(44, 13);
            label2.TabIndex = 8;
            label2.Text = "Estado:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(32, 177);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(43, 13);
            label3.TabIndex = 49;
            label3.Text = "Entidad";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(25, 68);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(53, 13);
            label5.TabIndex = 53;
            label5.Text = "Creación:";
            // 
            // Importe_TB
            // 
            this.Importe_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Importe", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Importe_TB.Location = new System.Drawing.Point(84, 120);
            this.Importe_TB.Name = "Importe_TB";
            this.Importe_TB.Size = new System.Drawing.Size(95, 21);
            this.Importe_TB.TabIndex = 1;
            this.Importe_TB.TabStop = false;
            this.Importe_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Auditoria_GB
            // 
            this.Auditoria_GB.Controls.Add(auditorLabel1);
            this.Auditoria_GB.Controls.Add(this.auditorComboBox);
            this.Auditoria_GB.Controls.Add(auditadoLabel);
            this.Auditoria_GB.Controls.Add(this.Auditado_CkB);
            this.Auditoria_GB.Location = new System.Drawing.Point(32, 372);
            this.Auditoria_GB.Name = "Auditoria_GB";
            this.Auditoria_GB.Size = new System.Drawing.Size(729, 88);
            this.Auditoria_GB.TabIndex = 4;
            this.Auditoria_GB.TabStop = false;
            this.Auditoria_GB.Text = "Auditoría";
            // 
            // auditorComboBox
            // 
            this.auditorComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Auditor", true));
            this.auditorComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos, "OidAuditor", true));
            this.auditorComboBox.DataSource = this.Datos_Usuarios;
            this.auditorComboBox.DisplayMember = "Texto";
            this.auditorComboBox.Enabled = false;
            this.auditorComboBox.FormattingEnabled = true;
            this.auditorComboBox.Location = new System.Drawing.Point(294, 38);
            this.auditorComboBox.Name = "auditorComboBox";
            this.auditorComboBox.Size = new System.Drawing.Size(295, 21);
            this.auditorComboBox.TabIndex = 6;
            this.auditorComboBox.ValueMember = "Oid";
            // 
            // Datos_Usuarios
            // 
            this.Datos_Usuarios.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
            // 
            // Auditado_CkB
            // 
            this.Auditado_CkB.Location = new System.Drawing.Point(199, 36);
            this.Auditado_CkB.Name = "Auditado_CkB";
            this.Auditado_CkB.Size = new System.Drawing.Size(26, 24);
            this.Auditado_CkB.TabIndex = 0;
            this.Auditado_CkB.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Estado_BT);
            this.groupBox2.Controls.Add(label2);
            this.groupBox2.Controls.Add(this.Estado_TB);
            this.groupBox2.Controls.Add(codigoLabel);
            this.groupBox2.Controls.Add(this.Codigo_TB);
            this.groupBox2.Controls.Add(observacionesLabel);
            this.groupBox2.Controls.Add(this.Observaciones_TB);
            this.groupBox2.Location = new System.Drawing.Point(32, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(729, 116);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos Generales";
            // 
            // Estado_BT
            // 
            this.Estado_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Estado_BT.Location = new System.Drawing.Point(679, 19);
            this.Estado_BT.Name = "Estado_BT";
            this.Estado_BT.Size = new System.Drawing.Size(29, 22);
            this.Estado_BT.TabIndex = 166;
            this.Estado_BT.UseVisualStyleBackColor = true;
            // 
            // Estado_TB
            // 
            this.Estado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "EEstadoLabel", true));
            this.Estado_TB.Location = new System.Drawing.Point(495, 20);
            this.Estado_TB.Name = "Estado_TB";
            this.Estado_TB.ReadOnly = true;
            this.Estado_TB.Size = new System.Drawing.Size(178, 21);
            this.Estado_TB.TabIndex = 9;
            this.Estado_TB.TabStop = false;
            this.Estado_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Codigo_TB
            // 
            this.Codigo_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
            this.Codigo_TB.Location = new System.Drawing.Point(48, 24);
            this.Codigo_TB.Name = "Codigo_TB";
            this.Codigo_TB.ReadOnly = true;
            this.Codigo_TB.Size = new System.Drawing.Size(54, 21);
            this.Codigo_TB.TabIndex = 3;
            this.Codigo_TB.TabStop = false;
            this.Codigo_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Observaciones_TB
            // 
            this.Observaciones_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.Observaciones_TB.Location = new System.Drawing.Point(108, 51);
            this.Observaciones_TB.Multiline = true;
            this.Observaciones_TB.Name = "Observaciones_TB";
            this.Observaciones_TB.Size = new System.Drawing.Size(600, 43);
            this.Observaciones_TB.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TipoCuenta_BT);
            this.groupBox3.Controls.Add(label5);
            this.groupBox3.Controls.Add(this.Creacion_DTP);
            this.groupBox3.Controls.Add(label3);
            this.groupBox3.Controls.Add(this.Entidad_TB);
            this.groupBox3.Controls.Add(this.Cuenta_BT);
            this.groupBox3.Controls.Add(label1);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(codigoOperacionLabel);
            this.groupBox3.Controls.Add(this.codigoOperacionTextBox);
            this.groupBox3.Controls.Add(fechaLabel);
            this.groupBox3.Controls.Add(this.Fecha_DTP);
            this.groupBox3.Controls.Add(titularLabel);
            this.groupBox3.Controls.Add(this.Titular_TB);
            this.groupBox3.Controls.Add(eTipoMovimientoBancoLabelLabel);
            this.groupBox3.Controls.Add(this.eTipoMovimientoBancoLabelTextBox);
            this.groupBox3.Controls.Add(eMedioPagoLabelLabel);
            this.groupBox3.Controls.Add(this.eMedioPagoLabelTextBox);
            this.groupBox3.Controls.Add(cuentaLabel);
            this.groupBox3.Controls.Add(this.Cuenta_TB);
            this.groupBox3.Controls.Add(this.Importe_TB);
            this.groupBox3.Controls.Add(abonadoLabel);
            this.groupBox3.Location = new System.Drawing.Point(32, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(729, 221);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos del Movimiento";
            // 
            // TipoCuenta_BT
            // 
            this.TipoCuenta_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.TipoCuenta_BT.Location = new System.Drawing.Point(495, 28);
            this.TipoCuenta_BT.Name = "TipoCuenta_BT";
            this.TipoCuenta_BT.Size = new System.Drawing.Size(29, 22);
            this.TipoCuenta_BT.TabIndex = 55;
            this.TipoCuenta_BT.UseVisualStyleBackColor = true;
            this.TipoCuenta_BT.Click += new System.EventHandler(this.TipoCuenta_BT_Click);
            // 
            // Creacion_DTP
            // 
            this.Creacion_DTP.CustomFormat = "dd/MM/yyyy HH:mm";
            this.Creacion_DTP.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "FechaCreacion", true));
            this.Creacion_DTP.Enabled = false;
            this.Creacion_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Creacion_DTP.Location = new System.Drawing.Point(84, 62);
            this.Creacion_DTP.Name = "Creacion_DTP";
            this.Creacion_DTP.Size = new System.Drawing.Size(145, 21);
            this.Creacion_DTP.TabIndex = 54;
            this.Creacion_DTP.TabStop = false;
            // 
            // Entidad_TB
            // 
            this.Entidad_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Entidad", true));
            this.Entidad_TB.Location = new System.Drawing.Point(84, 174);
            this.Entidad_TB.Name = "Entidad_TB";
            this.Entidad_TB.ReadOnly = true;
            this.Entidad_TB.Size = new System.Drawing.Size(228, 21);
            this.Entidad_TB.TabIndex = 50;
            this.Entidad_TB.TabStop = false;
            // 
            // Cuenta_BT
            // 
            this.Cuenta_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Cuenta_BT.Location = new System.Drawing.Point(318, 146);
            this.Cuenta_BT.Name = "Cuenta_BT";
            this.Cuenta_BT.Size = new System.Drawing.Size(29, 22);
            this.Cuenta_BT.TabIndex = 48;
            this.Cuenta_BT.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "IDMovimientoContable", true));
            this.textBox1.Location = new System.Drawing.Point(586, 147);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(114, 21);
            this.textBox1.TabIndex = 15;
            this.textBox1.TabStop = false;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // codigoOperacionTextBox
            // 
            this.codigoOperacionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "IDOperacion", true));
            this.codigoOperacionTextBox.Location = new System.Drawing.Point(149, 27);
            this.codigoOperacionTextBox.Name = "codigoOperacionTextBox";
            this.codigoOperacionTextBox.ReadOnly = true;
            this.codigoOperacionTextBox.Size = new System.Drawing.Size(100, 21);
            this.codigoOperacionTextBox.TabIndex = 13;
            this.codigoOperacionTextBox.TabStop = false;
            this.codigoOperacionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Fecha_DTP
            // 
            this.Fecha_DTP.CustomFormat = "dd/MM/yyyy HH:mm";
            this.Fecha_DTP.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "FechaOperacion", true));
            this.Fecha_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Fecha_DTP.Location = new System.Drawing.Point(306, 62);
            this.Fecha_DTP.Name = "Fecha_DTP";
            this.Fecha_DTP.Size = new System.Drawing.Size(145, 21);
            this.Fecha_DTP.TabIndex = 11;
            this.Fecha_DTP.TabStop = false;
            // 
            // Titular_TB
            // 
            this.Titular_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Titular", true));
            this.Titular_TB.Location = new System.Drawing.Point(84, 93);
            this.Titular_TB.Name = "Titular_TB";
            this.Titular_TB.ReadOnly = true;
            this.Titular_TB.Size = new System.Drawing.Size(376, 21);
            this.Titular_TB.TabIndex = 9;
            this.Titular_TB.TabStop = false;
            // 
            // eTipoMovimientoBancoLabelTextBox
            // 
            this.eTipoMovimientoBancoLabelTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "ETipoMovimientoBancoLabel", true));
            this.eTipoMovimientoBancoLabelTextBox.Location = new System.Drawing.Point(311, 28);
            this.eTipoMovimientoBancoLabelTextBox.Name = "eTipoMovimientoBancoLabelTextBox";
            this.eTipoMovimientoBancoLabelTextBox.ReadOnly = true;
            this.eTipoMovimientoBancoLabelTextBox.Size = new System.Drawing.Size(178, 21);
            this.eTipoMovimientoBancoLabelTextBox.TabIndex = 7;
            this.eTipoMovimientoBancoLabelTextBox.TabStop = false;
            this.eTipoMovimientoBancoLabelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // eMedioPagoLabelTextBox
            // 
            this.eMedioPagoLabelTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "EMedioPagoLabel", true));
            this.eMedioPagoLabelTextBox.Location = new System.Drawing.Point(281, 119);
            this.eMedioPagoLabelTextBox.Name = "eMedioPagoLabelTextBox";
            this.eMedioPagoLabelTextBox.ReadOnly = true;
            this.eMedioPagoLabelTextBox.Size = new System.Drawing.Size(252, 21);
            this.eMedioPagoLabelTextBox.TabIndex = 5;
            this.eMedioPagoLabelTextBox.TabStop = false;
            // 
            // Cuenta_TB
            // 
            this.Cuenta_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Cuenta", true));
            this.Cuenta_TB.Location = new System.Drawing.Point(84, 147);
            this.Cuenta_TB.Name = "Cuenta_TB";
            this.Cuenta_TB.ReadOnly = true;
            this.Cuenta_TB.Size = new System.Drawing.Size(228, 21);
            this.Cuenta_TB.TabIndex = 3;
            this.Cuenta_TB.TabStop = false;
            // 
            // MovimientoBancoForm
            // 
            this.ClientSize = new System.Drawing.Size(794, 537);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MovimientoBancoForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "MovimientoBancoForm";
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            this.Paneles2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Paneles2)).EndInit();
            this.Paneles2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            this.ProgressBK_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            this.Auditoria_GB.ResumeLayout(false);
            this.Auditoria_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Usuarios)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox Codigo_TB;
		private System.Windows.Forms.GroupBox Auditoria_GB;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox codigoOperacionTextBox;
        private System.Windows.Forms.TextBox eTipoMovimientoBancoLabelTextBox;
        private System.Windows.Forms.TextBox eMedioPagoLabelTextBox;
        private System.Windows.Forms.TextBox Cuenta_TB;
        private System.Windows.Forms.BindingSource Datos_Usuarios;
        protected System.Windows.Forms.CheckBox Auditado_CkB;
        protected System.Windows.Forms.ComboBox auditorComboBox;
		protected System.Windows.Forms.TextBox Observaciones_TB;
		private System.Windows.Forms.TextBox textBox1;
		protected System.Windows.Forms.Button Cuenta_BT;
		protected System.Windows.Forms.DateTimePicker Fecha_DTP;
		protected System.Windows.Forms.TextBox Importe_TB;
		protected System.Windows.Forms.Button Estado_BT;
		protected System.Windows.Forms.TextBox Estado_TB;
        private System.Windows.Forms.TextBox Entidad_TB;
        protected System.Windows.Forms.DateTimePicker Creacion_DTP;
        protected System.Windows.Forms.TextBox Titular_TB;
        protected System.Windows.Forms.Button TipoCuenta_BT;
		
		

    }
}
