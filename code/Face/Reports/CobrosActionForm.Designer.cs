namespace moleQule.Face.Invoice
{
    partial class CobrosActionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CobrosActionForm));
            this.Cliente_GB = new System.Windows.Forms.GroupBox();
            this.Cliente_TB = new System.Windows.Forms.TextBox();
            this.Cliente_BT = new System.Windows.Forms.Button();
            this.TodosCliente_CkB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FFinal_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.FInicial_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Serie_TB = new System.Windows.Forms.TextBox();
            this.Serie_BT = new System.Windows.Forms.Button();
            this.TodosSerie_CkB = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Fechas_GB = new System.Windows.Forms.GroupBox();
            this.MedioPago_GB = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MedioPago_CB = new System.Windows.Forms.ComboBox();
            this.Datos_MedioPago = new System.Windows.Forms.BindingSource(this.components);
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
            this.groupBox4.SuspendLayout();
            this.Fechas_GB.SuspendLayout();
            this.MedioPago_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).BeginInit();
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
            this.Source_GB.Controls.Add(this.MedioPago_GB);
            this.Source_GB.Controls.Add(this.Fechas_GB);
            this.Source_GB.Controls.Add(this.groupBox4);
            this.Source_GB.Controls.Add(this.Cliente_GB);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(661, 291);
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
            this.PanelesV.Size = new System.Drawing.Size(663, 333);
            this.PanelesV.SplitterDistance = 293;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(127, 24);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(663, 333);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(299, 218);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(299, 133);
            // 
            // Cliente_GB
            // 
            this.Cliente_GB.Controls.Add(this.Cliente_TB);
            this.Cliente_GB.Controls.Add(this.Cliente_BT);
            this.Cliente_GB.Controls.Add(this.TodosCliente_CkB);
            this.Cliente_GB.Controls.Add(this.label2);
            this.Cliente_GB.Location = new System.Drawing.Point(46, 33);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Serie_TB);
            this.groupBox4.Controls.Add(this.Serie_BT);
            this.groupBox4.Controls.Add(this.TodosSerie_CkB);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(46, 90);
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
            // Fechas_GB
            // 
            this.Fechas_GB.Controls.Add(this.FInicial_DTP);
            this.Fechas_GB.Controls.Add(this.label4);
            this.Fechas_GB.Controls.Add(this.FFinal_DTP);
            this.Fechas_GB.Controls.Add(this.label3);
            this.Fechas_GB.Location = new System.Drawing.Point(46, 147);
            this.Fechas_GB.Name = "Fechas_GB";
            this.Fechas_GB.Size = new System.Drawing.Size(568, 49);
            this.Fechas_GB.TabIndex = 37;
            this.Fechas_GB.TabStop = false;
            this.Fechas_GB.Text = "Fecha de Factura";
            // 
            // MedioPago_GB
            // 
            this.MedioPago_GB.Controls.Add(this.label7);
            this.MedioPago_GB.Controls.Add(this.MedioPago_CB);
            this.MedioPago_GB.Location = new System.Drawing.Point(46, 202);
            this.MedioPago_GB.Name = "MedioPago_GB";
            this.MedioPago_GB.Size = new System.Drawing.Size(568, 55);
            this.MedioPago_GB.TabIndex = 38;
            this.MedioPago_GB.TabStop = false;
            this.MedioPago_GB.Text = "Medio de Pago";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(107, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Medio de Pago:";
            // 
            // MedioPago_CB
            // 
            this.MedioPago_CB.DataSource = this.Datos_MedioPago;
            this.MedioPago_CB.DisplayMember = "Texto";
            this.MedioPago_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MedioPago_CB.FormattingEnabled = true;
            this.MedioPago_CB.Location = new System.Drawing.Point(194, 20);
            this.MedioPago_CB.Name = "MedioPago_CB";
            this.MedioPago_CB.Size = new System.Drawing.Size(267, 21);
            this.MedioPago_CB.TabIndex = 21;
            this.MedioPago_CB.ValueMember = "Oid";
            // 
            // Datos_MedioPago
            // 
            this.Datos_MedioPago.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
            // 
            // CobrosActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(663, 333);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CobrosActionForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Informe: Cobros de Facturas";
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.Fechas_GB.ResumeLayout(false);
            this.Fechas_GB.PerformLayout();
            this.MedioPago_GB.ResumeLayout(false);
            this.MedioPago_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).EndInit();
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
        private System.Windows.Forms.TextBox Cliente_TB;
        private System.Windows.Forms.Button Cliente_BT;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox Serie_TB;
        private System.Windows.Forms.Button Serie_BT;
        private System.Windows.Forms.CheckBox TodosSerie_CkB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox Fechas_GB;
        private System.Windows.Forms.GroupBox MedioPago_GB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox MedioPago_CB;
        private System.Windows.Forms.BindingSource Datos_MedioPago;
    }
}
