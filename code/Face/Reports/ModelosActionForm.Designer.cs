namespace moleQule.Face.Invoice
{
    partial class ModelosActionForm
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
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label6;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModelosActionForm));
            this.Datos_Tipos = new System.Windows.Forms.BindingSource(this.components);
            this.Origen_GB = new System.Windows.Forms.GroupBox();
            this.Modelo_CB = new System.Windows.Forms.ComboBox();
            this.Datos_Modelos = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.AnoActivo_DTP = new System.Windows.Forms.DateTimePicker();
            this.Periodo_CB = new System.Windows.Forms.ComboBox();
            this.Datos_Periodo = new System.Windows.Forms.BindingSource(this.components);
            this.label15 = new System.Windows.Forms.Label();
            this.Fechas_GB = new System.Windows.Forms.GroupBox();
            this.FInicial_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.FFinal_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.Browser = new System.Windows.Forms.FolderBrowserDialog();
            this.Datos_TiposAcreedor = new System.Windows.Forms.BindingSource(this.components);
            this.RutaSalida_GB = new System.Windows.Forms.GroupBox();
            this.Settings_TC = new System.Windows.Forms.TabControl();
            this.Modelo347_TP = new System.Windows.Forms.TabPage();
            this.MinImporte_TB = new System.Windows.Forms.TextBox();
            this.MinEfectivo_TB = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Examinar_BT = new System.Windows.Forms.Button();
            this.RutaSalida_TB = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
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
            this.Origen_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Modelos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Periodo)).BeginInit();
            this.Fechas_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposAcreedor)).BeginInit();
            this.RutaSalida_GB.SuspendLayout();
            this.Settings_TC.SuspendLayout();
            this.Modelo347_TP.SuspendLayout();
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
            this.Source_GB.Controls.Add(this.RutaSalida_GB);
            this.Source_GB.Controls.Add(this.Fechas_GB);
            this.Source_GB.Controls.Add(this.Origen_GB);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(617, 429);
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
            this.PanelesV.Size = new System.Drawing.Size(619, 471);
            this.PanelesV.SplitterDistance = 431;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(105, 24);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(619, 471);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(277, 287);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(277, 202);
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(447, 46);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(30, 13);
            label5.TabIndex = 42;
            label5.Text = "Año:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(178, 43);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(85, 13);
            label2.TabIndex = 47;
            label2.Text = "Efectivo Mínimo:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Location = new System.Drawing.Point(179, 17);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(84, 13);
            label6.TabIndex = 49;
            label6.Text = "Importe Mínimo:";
            // 
            // Origen_GB
            // 
            this.Origen_GB.Controls.Add(this.Modelo_CB);
            this.Origen_GB.Controls.Add(this.label1);
            this.Origen_GB.Controls.Add(label5);
            this.Origen_GB.Controls.Add(this.AnoActivo_DTP);
            this.Origen_GB.Controls.Add(this.Periodo_CB);
            this.Origen_GB.Controls.Add(this.label15);
            this.Origen_GB.Location = new System.Drawing.Point(24, 17);
            this.Origen_GB.Name = "Origen_GB";
            this.Origen_GB.Size = new System.Drawing.Size(568, 89);
            this.Origen_GB.TabIndex = 21;
            this.Origen_GB.TabStop = false;
            this.Origen_GB.Text = "Modelos";
            // 
            // Modelo_CB
            // 
            this.Modelo_CB.DataSource = this.Datos_Modelos;
            this.Modelo_CB.DisplayMember = "Texto";
            this.Modelo_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Modelo_CB.FormattingEnabled = true;
            this.Modelo_CB.Location = new System.Drawing.Point(33, 42);
            this.Modelo_CB.Name = "Modelo_CB";
            this.Modelo_CB.Size = new System.Drawing.Size(188, 21);
            this.Modelo_CB.TabIndex = 43;
            this.Modelo_CB.ValueMember = "Oid";
            this.Modelo_CB.SelectedIndexChanged += new System.EventHandler(this.Modelo_CB_SelectedIndexChanged);
            // 
            // Datos_Modelos
            // 
            this.Datos_Modelos.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Modelo:";
            // 
            // AnoActivo_DTP
            // 
            this.AnoActivo_DTP.CustomFormat = "yyyy";
            this.AnoActivo_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.AnoActivo_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.AnoActivo_DTP.Location = new System.Drawing.Point(483, 42);
            this.AnoActivo_DTP.Name = "AnoActivo_DTP";
            this.AnoActivo_DTP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.AnoActivo_DTP.ShowUpDown = true;
            this.AnoActivo_DTP.Size = new System.Drawing.Size(55, 21);
            this.AnoActivo_DTP.TabIndex = 41;
            this.AnoActivo_DTP.Tag = "NO FORMAT";
            this.AnoActivo_DTP.Value = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            this.AnoActivo_DTP.ValueChanged += new System.EventHandler(this.AnoActivo_DTP_ValueChanged);
            // 
            // Periodo_CB
            // 
            this.Periodo_CB.DataSource = this.Datos_Periodo;
            this.Periodo_CB.DisplayMember = "Texto";
            this.Periodo_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Periodo_CB.FormattingEnabled = true;
            this.Periodo_CB.Location = new System.Drawing.Point(256, 42);
            this.Periodo_CB.Name = "Periodo_CB";
            this.Periodo_CB.Size = new System.Drawing.Size(155, 21);
            this.Periodo_CB.TabIndex = 14;
            this.Periodo_CB.ValueMember = "Oid";
            this.Periodo_CB.SelectedIndexChanged += new System.EventHandler(this.Periodo_CB_SelectedIndexChanged);
            // 
            // Datos_Periodo
            // 
            this.Datos_Periodo.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(253, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "Periodo:";
            // 
            // Fechas_GB
            // 
            this.Fechas_GB.Controls.Add(this.FInicial_DTP);
            this.Fechas_GB.Controls.Add(this.label4);
            this.Fechas_GB.Controls.Add(this.FFinal_DTP);
            this.Fechas_GB.Controls.Add(this.label3);
            this.Fechas_GB.Location = new System.Drawing.Point(24, 112);
            this.Fechas_GB.Name = "Fechas_GB";
            this.Fechas_GB.Size = new System.Drawing.Size(568, 50);
            this.Fechas_GB.TabIndex = 42;
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
            this.FInicial_DTP.Tag = "NO FORMAT";
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
            this.FFinal_DTP.Tag = "NO FORMAT";
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
            // Datos_TiposAcreedor
            // 
            this.Datos_TiposAcreedor.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
            // 
            // RutaSalida_GB
            // 
            this.RutaSalida_GB.Controls.Add(this.Settings_TC);
            this.RutaSalida_GB.Controls.Add(this.label11);
            this.RutaSalida_GB.Controls.Add(this.Examinar_BT);
            this.RutaSalida_GB.Controls.Add(this.RutaSalida_TB);
            this.RutaSalida_GB.Location = new System.Drawing.Point(24, 168);
            this.RutaSalida_GB.Name = "RutaSalida_GB";
            this.RutaSalida_GB.Size = new System.Drawing.Size(568, 179);
            this.RutaSalida_GB.TabIndex = 44;
            this.RutaSalida_GB.TabStop = false;
            this.RutaSalida_GB.Text = "Configuración";
            // 
            // Settings_TC
            // 
            this.Settings_TC.Controls.Add(this.Modelo347_TP);
            this.Settings_TC.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Settings_TC.Location = new System.Drawing.Point(13, 29);
            this.Settings_TC.Name = "Settings_TC";
            this.Settings_TC.SelectedIndex = 0;
            this.Settings_TC.Size = new System.Drawing.Size(540, 100);
            this.Settings_TC.TabIndex = 54;
            this.Settings_TC.Tag = "NO FORMAT";
            // 
            // Modelo347_TP
            // 
            this.Modelo347_TP.Controls.Add(this.MinImporte_TB);
            this.Modelo347_TP.Controls.Add(this.MinEfectivo_TB);
            this.Modelo347_TP.Controls.Add(label2);
            this.Modelo347_TP.Controls.Add(label6);
            this.Modelo347_TP.Location = new System.Drawing.Point(4, 22);
            this.Modelo347_TP.Name = "Modelo347_TP";
            this.Modelo347_TP.Padding = new System.Windows.Forms.Padding(3);
            this.Modelo347_TP.Size = new System.Drawing.Size(532, 74);
            this.Modelo347_TP.TabIndex = 0;
            this.Modelo347_TP.Text = "Modelo 347";
            this.Modelo347_TP.UseVisualStyleBackColor = true;
            // 
            // MinImporte_TB
            // 
            this.MinImporte_TB.Location = new System.Drawing.Point(269, 14);
            this.MinImporte_TB.Name = "MinImporte_TB";
            this.MinImporte_TB.Size = new System.Drawing.Size(70, 21);
            this.MinImporte_TB.TabIndex = 48;
            this.MinImporte_TB.TabStop = false;
            this.MinImporte_TB.Text = "3,005.06";
            this.MinImporte_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MinEfectivo_TB
            // 
            this.MinEfectivo_TB.Location = new System.Drawing.Point(269, 40);
            this.MinEfectivo_TB.Name = "MinEfectivo_TB";
            this.MinEfectivo_TB.Size = new System.Drawing.Size(70, 21);
            this.MinEfectivo_TB.TabIndex = 46;
            this.MinEfectivo_TB.TabStop = false;
            this.MinEfectivo_TB.Text = "6,000.00";
            this.MinEfectivo_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.Enabled = false;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(15, 144);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 16);
            this.label11.TabIndex = 19;
            this.label11.Text = "Ruta Salida:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Examinar_BT
            // 
            this.Examinar_BT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Examinar_BT.Enabled = false;
            this.Examinar_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Examinar_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.browse_16;
            this.Examinar_BT.Location = new System.Drawing.Point(528, 140);
            this.Examinar_BT.Name = "Examinar_BT";
            this.Examinar_BT.Size = new System.Drawing.Size(25, 25);
            this.Examinar_BT.TabIndex = 33;
            this.Examinar_BT.UseVisualStyleBackColor = true;
            // 
            // RutaSalida_TB
            // 
            this.RutaSalida_TB.Enabled = false;
            this.RutaSalida_TB.Location = new System.Drawing.Point(89, 142);
            this.RutaSalida_TB.Name = "RutaSalida_TB";
            this.RutaSalida_TB.ReadOnly = true;
            this.RutaSalida_TB.Size = new System.Drawing.Size(433, 21);
            this.RutaSalida_TB.TabIndex = 32;
            // 
            // ModelosActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(619, 471);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModelosActionForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Modelos";
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
            this.Origen_GB.ResumeLayout(false);
            this.Origen_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Modelos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Periodo)).EndInit();
            this.Fechas_GB.ResumeLayout(false);
            this.Fechas_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TiposAcreedor)).EndInit();
            this.RutaSalida_GB.ResumeLayout(false);
            this.RutaSalida_GB.PerformLayout();
            this.Settings_TC.ResumeLayout(false);
            this.Modelo347_TP.ResumeLayout(false);
            this.Modelo347_TP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.BindingSource Datos_Tipos;
		private System.Windows.Forms.GroupBox Origen_GB;
        private System.Windows.Forms.GroupBox Fechas_GB;
        private Controls.mQDateTimePicker FInicial_DTP;
        private System.Windows.Forms.Label label4;
        private Controls.mQDateTimePicker FFinal_DTP;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.FolderBrowserDialog Browser;
		private System.Windows.Forms.BindingSource Datos_TiposAcreedor;
		private System.Windows.Forms.BindingSource Datos_Modelos;
		private System.Windows.Forms.BindingSource Datos_Periodo;
		private System.Windows.Forms.ComboBox Periodo_CB;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.DateTimePicker AnoActivo_DTP;
		private System.Windows.Forms.ComboBox Modelo_CB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox RutaSalida_GB;
		private System.Windows.Forms.TabControl Settings_TC;
		private System.Windows.Forms.TabPage Modelo347_TP;
		protected System.Windows.Forms.TextBox MinImporte_TB;
		protected System.Windows.Forms.TextBox MinEfectivo_TB;
		private System.Windows.Forms.Label label11;
		protected System.Windows.Forms.Button Examinar_BT;
		private System.Windows.Forms.TextBox RutaSalida_TB;
    }
}
