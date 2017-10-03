namespace moleQule.Face.Invoice
{
    partial class DetalleAlbaranesActionForm
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
            this.TipoAlbaran_CB = new System.Windows.Forms.ComboBox();
            this.Datos_Tipos = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FInicial_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.FFinal_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Resumido_RB = new System.Windows.Forms.RadioButton();
            this.Detallado_RB = new System.Windows.Forms.RadioButton();
            this.Origen_GB = new System.Windows.Forms.GroupBox();
            this.Condiciones_RB = new System.Windows.Forms.RadioButton();
            this.Listado_RB = new System.Windows.Forms.RadioButton();
            this.Condiciones_GB = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Serie_TB = new System.Windows.Forms.TextBox();
            this.Serie_BT = new System.Windows.Forms.Button();
            this.TodosSerie_CkB = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Cliente_GB = new System.Windows.Forms.GroupBox();
            this.Cliente_TB = new System.Windows.Forms.TextBox();
            this.Cliente_BT = new System.Windows.Forms.Button();
            this.TodosCliente_CkB = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Tipos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.Origen_GB.SuspendLayout();
            this.Condiciones_GB.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ErrorMng_EP.SetIconAlignment(this.Submit_BT, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.Submit_BT.Location = new System.Drawing.Point(136, 2);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
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
            this.Source_GB.Controls.Add(this.groupBox4);
            this.Source_GB.Controls.Add(this.Cliente_GB);
            this.Source_GB.Controls.Add(this.Condiciones_GB);
            this.Source_GB.Controls.Add(this.Origen_GB);
            this.Source_GB.Controls.Add(this.groupBox1);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(617, 369);
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
            this.PanelesV.Size = new System.Drawing.Size(619, 411);
            this.PanelesV.SplitterDistance = 371;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(105, 24);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(619, 411);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(277, 257);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(277, 172);
            // 
            // TipoAlbaran_CB
            // 
            this.TipoAlbaran_CB.DataSource = this.Datos_Tipos;
            this.TipoAlbaran_CB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TipoAlbaran_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoAlbaran_CB.FormattingEnabled = true;
            this.TipoAlbaran_CB.Location = new System.Drawing.Point(213, 31);
            this.TipoAlbaran_CB.Name = "TipoAlbaran_CB";
            this.TipoAlbaran_CB.Size = new System.Drawing.Size(234, 21);
            this.TipoAlbaran_CB.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(121, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tipo de Albarán:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(195, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Fecha Final:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(190, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Fecha Inicial:";
            // 
            // FInicial_DTP
            // 
            this.FInicial_DTP.Checked = false;
            this.FInicial_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FInicial_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FInicial_DTP.Location = new System.Drawing.Point(266, 71);
            this.FInicial_DTP.Name = "FInicial_DTP";
            this.FInicial_DTP.ShowCheckBox = true;
            this.FInicial_DTP.Size = new System.Drawing.Size(112, 21);
            this.FInicial_DTP.TabIndex = 10;
            // 
            // FFinal_DTP
            // 
            this.FFinal_DTP.Checked = false;
            this.FFinal_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FFinal_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FFinal_DTP.Location = new System.Drawing.Point(266, 98);
            this.FFinal_DTP.Name = "FFinal_DTP";
            this.FFinal_DTP.ShowCheckBox = true;
            this.FFinal_DTP.Size = new System.Drawing.Size(112, 21);
            this.FFinal_DTP.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Resumido_RB);
            this.groupBox1.Controls.Add(this.Detallado_RB);
            this.groupBox1.Location = new System.Drawing.Point(344, 282);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 63);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo";
            // 
            // Resumido_RB
            // 
            this.Resumido_RB.AutoSize = true;
            this.Resumido_RB.Checked = true;
            this.Resumido_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Resumido_RB.Location = new System.Drawing.Point(18, 15);
            this.Resumido_RB.Name = "Resumido_RB";
            this.Resumido_RB.Size = new System.Drawing.Size(71, 17);
            this.Resumido_RB.TabIndex = 1;
            this.Resumido_RB.TabStop = true;
            this.Resumido_RB.Text = "Resumido";
            this.Resumido_RB.UseVisualStyleBackColor = true;
            // 
            // Detallado_RB
            // 
            this.Detallado_RB.AutoSize = true;
            this.Detallado_RB.Enabled = false;
            this.Detallado_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Detallado_RB.Location = new System.Drawing.Point(18, 38);
            this.Detallado_RB.Name = "Detallado_RB";
            this.Detallado_RB.Size = new System.Drawing.Size(70, 17);
            this.Detallado_RB.TabIndex = 0;
            this.Detallado_RB.Text = "Detallado";
            this.Detallado_RB.UseVisualStyleBackColor = true;
            // 
            // Origen_GB
            // 
            this.Origen_GB.Controls.Add(this.Condiciones_RB);
            this.Origen_GB.Controls.Add(this.Listado_RB);
            this.Origen_GB.Location = new System.Drawing.Point(181, 282);
            this.Origen_GB.Name = "Origen_GB";
            this.Origen_GB.Size = new System.Drawing.Size(138, 63);
            this.Origen_GB.TabIndex = 21;
            this.Origen_GB.TabStop = false;
            this.Origen_GB.Text = "Origen";
            // 
            // Condiciones_RB
            // 
            this.Condiciones_RB.AutoSize = true;
            this.Condiciones_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Condiciones_RB.Location = new System.Drawing.Point(28, 38);
            this.Condiciones_RB.Name = "Condiciones_RB";
            this.Condiciones_RB.Size = new System.Drawing.Size(82, 17);
            this.Condiciones_RB.TabIndex = 1;
            this.Condiciones_RB.Text = "Condiciones";
            this.Condiciones_RB.UseVisualStyleBackColor = true;
            this.Condiciones_RB.CheckedChanged += new System.EventHandler(this.Condiciones_RB_CheckedChanged);
            // 
            // Listado_RB
            // 
            this.Listado_RB.AutoSize = true;
            this.Listado_RB.Checked = true;
            this.Listado_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Listado_RB.Location = new System.Drawing.Point(28, 15);
            this.Listado_RB.Name = "Listado_RB";
            this.Listado_RB.Size = new System.Drawing.Size(59, 17);
            this.Listado_RB.TabIndex = 0;
            this.Listado_RB.TabStop = true;
            this.Listado_RB.Text = "Listado";
            this.Listado_RB.UseVisualStyleBackColor = true;
            this.Listado_RB.CheckedChanged += new System.EventHandler(this.Listado_RB_CheckedChanged);
            // 
            // Condiciones_GB
            // 
            this.Condiciones_GB.Controls.Add(this.TipoAlbaran_CB);
            this.Condiciones_GB.Controls.Add(this.FFinal_DTP);
            this.Condiciones_GB.Controls.Add(this.label2);
            this.Condiciones_GB.Controls.Add(this.FInicial_DTP);
            this.Condiciones_GB.Controls.Add(this.label3);
            this.Condiciones_GB.Controls.Add(this.label4);
            this.Condiciones_GB.Enabled = false;
            this.Condiciones_GB.Location = new System.Drawing.Point(23, 138);
            this.Condiciones_GB.Name = "Condiciones_GB";
            this.Condiciones_GB.Size = new System.Drawing.Size(569, 138);
            this.Condiciones_GB.TabIndex = 24;
            this.Condiciones_GB.TabStop = false;
            this.Condiciones_GB.Text = "Condiciones";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Serie_TB);
            this.groupBox4.Controls.Add(this.Serie_BT);
            this.groupBox4.Controls.Add(this.TodosSerie_CkB);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(23, 81);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(568, 51);
            this.groupBox4.TabIndex = 37;
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
            // Cliente_GB
            // 
            this.Cliente_GB.Controls.Add(this.Cliente_TB);
            this.Cliente_GB.Controls.Add(this.Cliente_BT);
            this.Cliente_GB.Controls.Add(this.TodosCliente_CkB);
            this.Cliente_GB.Controls.Add(this.label7);
            this.Cliente_GB.Location = new System.Drawing.Point(24, 24);
            this.Cliente_GB.Name = "Cliente_GB";
            this.Cliente_GB.Size = new System.Drawing.Size(568, 51);
            this.Cliente_GB.TabIndex = 36;
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
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Selección:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DetalleAlbaranesActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(619, 411);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "DetalleAlbaranesActionForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Informe de Albaranes";
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Tipos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Origen_GB.ResumeLayout(false);
            this.Origen_GB.PerformLayout();
            this.Condiciones_GB.ResumeLayout(false);
            this.Condiciones_GB.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.Cliente_GB.ResumeLayout(false);
            this.Cliente_GB.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.GroupBox Condiciones_GB;
        private System.Windows.Forms.RadioButton Condiciones_RB;
        private System.Windows.Forms.BindingSource Datos_Tipos;
        private System.Windows.Forms.RadioButton Detallado_RB;
        private moleQule.Face.Controls.mQDateTimePicker FFinal_DTP;
        private moleQule.Face.Controls.mQDateTimePicker FInicial_DTP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton Listado_RB;
        private System.Windows.Forms.GroupBox Origen_GB;
        private System.Windows.Forms.RadioButton Resumido_RB;
        private System.Windows.Forms.ComboBox TipoAlbaran_CB;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox Serie_TB;
        private System.Windows.Forms.Button Serie_BT;
        private System.Windows.Forms.CheckBox TodosSerie_CkB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox Cliente_GB;
        private System.Windows.Forms.TextBox Cliente_TB;
        private System.Windows.Forms.Button Cliente_BT;
        private System.Windows.Forms.CheckBox TodosCliente_CkB;
        private System.Windows.Forms.Label label7;
    }
}
