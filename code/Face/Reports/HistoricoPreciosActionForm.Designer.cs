namespace moleQule.Face.Invoice
{
	partial class HistoricoPreciosActionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoricoPreciosActionForm));
            this.Tipo_GB = new System.Windows.Forms.GroupBox();
            this.Producto_RB = new System.Windows.Forms.RadioButton();
            this.Cliente_RB = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Descendente_RB = new System.Windows.Forms.RadioButton();
            this.Ascendente_RB = new System.Windows.Forms.RadioButton();
            this.Familias_GB = new System.Windows.Forms.GroupBox();
            this.Familia_TB = new System.Windows.Forms.TextBox();
            this.Familia_BT = new System.Windows.Forms.Button();
            this.TodosFamilia_CkB = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Fechas_GB = new System.Windows.Forms.GroupBox();
            this.FInicial_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.FFinal_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.Producto_GB = new System.Windows.Forms.GroupBox();
            this.Producto_TB = new System.Windows.Forms.TextBox();
            this.Producto_BT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TodosProducto_CkB = new System.Windows.Forms.CheckBox();
            this.Cliente_GB = new System.Windows.Forms.GroupBox();
            this.Cliente_TB = new System.Windows.Forms.TextBox();
            this.Cliente_BT = new System.Windows.Forms.Button();
            this.TodosCliente_CkB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.Tipo_GB.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Familias_GB.SuspendLayout();
            this.Fechas_GB.SuspendLayout();
            this.Producto_GB.SuspendLayout();
            this.Cliente_GB.SuspendLayout();
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
            this.Submit_BT.Text = "&Ver";
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
            this.Source_GB.Controls.Add(this.Cliente_GB);
            this.Source_GB.Controls.Add(this.Producto_GB);
            this.Source_GB.Controls.Add(this.Fechas_GB);
            this.Source_GB.Controls.Add(this.Familias_GB);
            this.Source_GB.Controls.Add(this.groupBox2);
            this.Source_GB.Controls.Add(this.Tipo_GB);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(635, 401);
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
            this.PanelesV.Size = new System.Drawing.Size(637, 443);
            this.PanelesV.SplitterDistance = 403;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(114, 24);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(637, 443);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(286, 273);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(286, 188);
            // 
            // Tipo_GB
            // 
            this.Tipo_GB.Controls.Add(this.Producto_RB);
            this.Tipo_GB.Controls.Add(this.Cliente_RB);
            this.Tipo_GB.Location = new System.Drawing.Point(143, 277);
            this.Tipo_GB.Name = "Tipo_GB";
            this.Tipo_GB.Size = new System.Drawing.Size(129, 100);
            this.Tipo_GB.TabIndex = 0;
            this.Tipo_GB.TabStop = false;
            this.Tipo_GB.Text = "Agrupar por";
            // 
            // Producto_RB
            // 
            this.Producto_RB.AutoSize = true;
            this.Producto_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Producto_RB.Location = new System.Drawing.Point(30, 53);
            this.Producto_RB.Name = "Producto_RB";
            this.Producto_RB.Size = new System.Drawing.Size(68, 17);
            this.Producto_RB.TabIndex = 1;
            this.Producto_RB.Text = "Producto";
            this.Producto_RB.UseVisualStyleBackColor = true;
            this.Producto_RB.CheckedChanged += new System.EventHandler(this.Proveedor_RB_CheckedChanged);
            // 
            // Cliente_RB
            // 
            this.Cliente_RB.AutoSize = true;
            this.Cliente_RB.Checked = true;
            this.Cliente_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cliente_RB.Location = new System.Drawing.Point(30, 30);
            this.Cliente_RB.Name = "Cliente_RB";
            this.Cliente_RB.Size = new System.Drawing.Size(58, 17);
            this.Cliente_RB.TabIndex = 0;
            this.Cliente_RB.TabStop = true;
            this.Cliente_RB.Text = "Cliente";
            this.Cliente_RB.UseVisualStyleBackColor = true;
            this.Cliente_RB.CheckedChanged += new System.EventHandler(this.Cliente_RB_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Descendente_RB);
            this.groupBox2.Controls.Add(this.Ascendente_RB);
            this.groupBox2.Location = new System.Drawing.Point(347, 277);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(144, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Orden";
            // 
            // Descendente_RB
            // 
            this.Descendente_RB.AutoSize = true;
            this.Descendente_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descendente_RB.Location = new System.Drawing.Point(28, 53);
            this.Descendente_RB.Name = "Descendente_RB";
            this.Descendente_RB.Size = new System.Drawing.Size(88, 17);
            this.Descendente_RB.TabIndex = 2;
            this.Descendente_RB.Text = "Descendente";
            this.Descendente_RB.UseVisualStyleBackColor = true;
            // 
            // Ascendente_RB
            // 
            this.Ascendente_RB.AutoSize = true;
            this.Ascendente_RB.Checked = true;
            this.Ascendente_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ascendente_RB.Location = new System.Drawing.Point(28, 30);
            this.Ascendente_RB.Name = "Ascendente_RB";
            this.Ascendente_RB.Size = new System.Drawing.Size(82, 17);
            this.Ascendente_RB.TabIndex = 1;
            this.Ascendente_RB.TabStop = true;
            this.Ascendente_RB.Text = "Ascendente";
            this.Ascendente_RB.UseVisualStyleBackColor = true;
            // 
            // Familias_GB
            // 
            this.Familias_GB.Controls.Add(this.Familia_TB);
            this.Familias_GB.Controls.Add(this.Familia_BT);
            this.Familias_GB.Controls.Add(this.TodosFamilia_CkB);
            this.Familias_GB.Controls.Add(this.label5);
            this.Familias_GB.Location = new System.Drawing.Point(33, 36);
            this.Familias_GB.Name = "Familias_GB";
            this.Familias_GB.Size = new System.Drawing.Size(568, 51);
            this.Familias_GB.TabIndex = 40;
            this.Familias_GB.TabStop = false;
            this.Familias_GB.Text = "Familias";
            // 
            // Familia_TB
            // 
            this.Familia_TB.Location = new System.Drawing.Point(86, 19);
            this.Familia_TB.Name = "Familia_TB";
            this.Familia_TB.ReadOnly = true;
            this.Familia_TB.Size = new System.Drawing.Size(290, 21);
            this.Familia_TB.TabIndex = 18;
            // 
            // Familia_BT
            // 
            this.Familia_BT.Enabled = false;
            this.Familia_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Familia_BT.Location = new System.Drawing.Point(382, 18);
            this.Familia_BT.Name = "Familia_BT";
            this.Familia_BT.Size = new System.Drawing.Size(42, 23);
            this.Familia_BT.TabIndex = 17;
            this.Familia_BT.UseVisualStyleBackColor = true;
            this.Familia_BT.Click += new System.EventHandler(this.Familia_BT_Click);
            // 
            // TodosFamilia_CkB
            // 
            this.TodosFamilia_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TodosFamilia_CkB.AutoSize = true;
            this.TodosFamilia_CkB.Checked = true;
            this.TodosFamilia_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodosFamilia_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosFamilia_CkB.Location = new System.Drawing.Point(455, 21);
            this.TodosFamilia_CkB.Name = "TodosFamilia_CkB";
            this.TodosFamilia_CkB.Size = new System.Drawing.Size(95, 17);
            this.TodosFamilia_CkB.TabIndex = 14;
            this.TodosFamilia_CkB.Text = "Mostrar Todos";
            this.TodosFamilia_CkB.UseVisualStyleBackColor = true;
            this.TodosFamilia_CkB.CheckedChanged += new System.EventHandler(this.TodosFamilia_GB_CheckedChanged);
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
            // Fechas_GB
            // 
            this.Fechas_GB.Controls.Add(this.FInicial_DTP);
            this.Fechas_GB.Controls.Add(this.label4);
            this.Fechas_GB.Controls.Add(this.FFinal_DTP);
            this.Fechas_GB.Controls.Add(this.label3);
            this.Fechas_GB.Location = new System.Drawing.Point(33, 212);
            this.Fechas_GB.Name = "Fechas_GB";
            this.Fechas_GB.Size = new System.Drawing.Size(568, 49);
            this.Fechas_GB.TabIndex = 41;
            this.Fechas_GB.TabStop = false;
            this.Fechas_GB.Text = "Fechas";
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
            // Producto_GB
            // 
            this.Producto_GB.Controls.Add(this.Producto_TB);
            this.Producto_GB.Controls.Add(this.Producto_BT);
            this.Producto_GB.Controls.Add(this.label1);
            this.Producto_GB.Controls.Add(this.TodosProducto_CkB);
            this.Producto_GB.Location = new System.Drawing.Point(33, 93);
            this.Producto_GB.Name = "Producto_GB";
            this.Producto_GB.Size = new System.Drawing.Size(568, 57);
            this.Producto_GB.TabIndex = 42;
            this.Producto_GB.TabStop = false;
            this.Producto_GB.Text = "Producto";
            // 
            // Producto_TB
            // 
            this.Producto_TB.Location = new System.Drawing.Point(87, 22);
            this.Producto_TB.Name = "Producto_TB";
            this.Producto_TB.ReadOnly = true;
            this.Producto_TB.Size = new System.Drawing.Size(290, 21);
            this.Producto_TB.TabIndex = 0;
            // 
            // Producto_BT
            // 
            this.Producto_BT.Enabled = false;
            this.Producto_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Producto_BT.Location = new System.Drawing.Point(383, 21);
            this.Producto_BT.Name = "Producto_BT";
            this.Producto_BT.Size = new System.Drawing.Size(42, 23);
            this.Producto_BT.TabIndex = 1;
            this.Producto_BT.UseVisualStyleBackColor = true;
            this.Producto_BT.Click += new System.EventHandler(this.Producto_BT_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "Selección:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TodosProducto_CkB
            // 
            this.TodosProducto_CkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TodosProducto_CkB.AutoSize = true;
            this.TodosProducto_CkB.Checked = true;
            this.TodosProducto_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TodosProducto_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodosProducto_CkB.Location = new System.Drawing.Point(451, 24);
            this.TodosProducto_CkB.Name = "TodosProducto_CkB";
            this.TodosProducto_CkB.Size = new System.Drawing.Size(95, 17);
            this.TodosProducto_CkB.TabIndex = 2;
            this.TodosProducto_CkB.Text = "Mostrar Todos";
            this.TodosProducto_CkB.UseVisualStyleBackColor = true;
            this.TodosProducto_CkB.CheckedChanged += new System.EventHandler(this.TodosProducto_CkB_CheckedChanged);
            // 
            // Cliente_GB
            // 
            this.Cliente_GB.Controls.Add(this.Cliente_TB);
            this.Cliente_GB.Controls.Add(this.Cliente_BT);
            this.Cliente_GB.Controls.Add(this.TodosCliente_CkB);
            this.Cliente_GB.Controls.Add(this.label2);
            this.Cliente_GB.Location = new System.Drawing.Point(33, 156);
            this.Cliente_GB.Name = "Cliente_GB";
            this.Cliente_GB.Size = new System.Drawing.Size(568, 51);
            this.Cliente_GB.TabIndex = 43;
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
            // HistoricoPreciosActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(637, 443);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HistoricoPreciosActionForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Informe: Histórico de Precios";
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
            this.Tipo_GB.ResumeLayout(false);
            this.Tipo_GB.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Familias_GB.ResumeLayout(false);
            this.Familias_GB.PerformLayout();
            this.Fechas_GB.ResumeLayout(false);
            this.Fechas_GB.PerformLayout();
            this.Producto_GB.ResumeLayout(false);
            this.Producto_GB.PerformLayout();
            this.Cliente_GB.ResumeLayout(false);
            this.Cliente_GB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox Tipo_GB;
        private System.Windows.Forms.RadioButton Descendente_RB;
        private System.Windows.Forms.RadioButton Ascendente_RB;
        private System.Windows.Forms.RadioButton Producto_RB;
		private System.Windows.Forms.RadioButton Cliente_RB;
        private System.Windows.Forms.GroupBox Familias_GB;
        private System.Windows.Forms.TextBox Familia_TB;
        private System.Windows.Forms.Button Familia_BT;
        private System.Windows.Forms.CheckBox TodosFamilia_CkB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox Fechas_GB;
        private moleQule.Face.Controls.mQDateTimePicker FInicial_DTP;
        private System.Windows.Forms.Label label4;
        private moleQule.Face.Controls.mQDateTimePicker FFinal_DTP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox Producto_GB;
        private System.Windows.Forms.TextBox Producto_TB;
        private System.Windows.Forms.Button Producto_BT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox TodosProducto_CkB;
		private System.Windows.Forms.GroupBox Cliente_GB;
		private System.Windows.Forms.TextBox Cliente_TB;
		private System.Windows.Forms.Button Cliente_BT;
		private System.Windows.Forms.CheckBox TodosCliente_CkB;
		private System.Windows.Forms.Label label2;
    }
}
