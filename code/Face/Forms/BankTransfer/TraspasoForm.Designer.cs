namespace moleQule.Face.Invoice
{
    partial class TraspasoForm
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label codigoLabel;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label Usuario_LB;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TraspasoForm));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Observaciones_TB = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Usuario_TB = new System.Windows.Forms.TextBox();
            this.Estado_BT = new System.Windows.Forms.Button();
            this.CuentaDestino_BT = new System.Windows.Forms.Button();
            this.CuentaDestino_TB = new System.Windows.Forms.TextBox();
            this.CuentaOrigen_BT = new System.Windows.Forms.Button();
            this.FEcha_DTP = new System.Windows.Forms.DateTimePicker();
            this.CuentaOrigen_TB = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.Estado_TB = new System.Windows.Forms.TextBox();
            this.Codigo_TB = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            label2 = new System.Windows.Forms.Label();
            codigoLabel = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            Usuario_LB = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
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
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.Controls.Add(this.groupBox3);
            this.PanelesV.Panel1.Controls.Add(this.groupBox2);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(868, 436);
            this.PanelesV.SplitterDistance = 381;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(252, 6);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(348, 6);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Paneles2
            // 
            // 
            // Paneles2.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, true);
            // 
            // Paneles2.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Paneles2, true);
            this.Paneles2.Size = new System.Drawing.Size(866, 52);
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Imprimir_Button.Location = new System.Drawing.Point(156, 6);
            this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
            // 
            // Docs_BT
            // 
            this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Docs_BT.Location = new System.Drawing.Point(190, 8);
            this.HelpProvider.SetShowHelp(this.Docs_BT, true);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Invoice.Traspaso);
            this.Datos.DataSourceChanged += new System.EventHandler(this.Datos_DataSourceChanged);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(255, 96);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(868, 436);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(397, 266);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(397, 181);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(246, 31);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(44, 13);
            label2.TabIndex = 8;
            label2.Text = "Estado:";
            // 
            // codigoLabel
            // 
            codigoLabel.AutoSize = true;
            codigoLabel.Location = new System.Drawing.Point(58, 31);
            codigoLabel.Name = "codigoLabel";
            codigoLabel.Size = new System.Drawing.Size(22, 13);
            codigoLabel.TabIndex = 2;
            codigoLabel.Text = "ID:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(52, 76);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(69, 13);
            label3.TabIndex = 16;
            label3.Text = "Fecha Envío:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(14, 140);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(81, 13);
            label4.TabIndex = 14;
            label4.Text = "Cuenta Origen:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(511, 76);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(49, 13);
            label5.TabIndex = 12;
            label5.Text = "Importe:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(425, 140);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(85, 13);
            label1.TabIndex = 19;
            label1.Text = "Cuenta Destino:";
            // 
            // Usuario_LB
            // 
            Usuario_LB.AutoSize = true;
            Usuario_LB.Location = new System.Drawing.Point(602, 31);
            Usuario_LB.Name = "Usuario_LB";
            Usuario_LB.Size = new System.Drawing.Size(47, 13);
            Usuario_LB.TabIndex = 23;
            Usuario_LB.Text = "Usuario:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(467, 105);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(93, 13);
            label6.TabIndex = 25;
            label6.Text = "Gastos Bancarios:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Observaciones_TB);
            this.groupBox3.Location = new System.Drawing.Point(29, 212);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(819, 150);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Observaciones";
            // 
            // Observaciones_TB
            // 
            this.Observaciones_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.Observaciones_TB.Location = new System.Drawing.Point(41, 27);
            this.Observaciones_TB.Multiline = true;
            this.Observaciones_TB.Name = "Observaciones_TB";
            this.Observaciones_TB.Size = new System.Drawing.Size(736, 101);
            this.Observaciones_TB.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(label7);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(label6);
            this.groupBox2.Controls.Add(Usuario_LB);
            this.groupBox2.Controls.Add(this.Usuario_TB);
            this.groupBox2.Controls.Add(this.Estado_BT);
            this.groupBox2.Controls.Add(this.CuentaDestino_BT);
            this.groupBox2.Controls.Add(label1);
            this.groupBox2.Controls.Add(this.CuentaDestino_TB);
            this.groupBox2.Controls.Add(this.CuentaOrigen_BT);
            this.groupBox2.Controls.Add(label3);
            this.groupBox2.Controls.Add(this.FEcha_DTP);
            this.groupBox2.Controls.Add(label4);
            this.groupBox2.Controls.Add(this.CuentaOrigen_TB);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(label5);
            this.groupBox2.Controls.Add(label2);
            this.groupBox2.Controls.Add(this.Estado_TB);
            this.groupBox2.Controls.Add(codigoLabel);
            this.groupBox2.Controls.Add(this.Codigo_TB);
            this.groupBox2.Location = new System.Drawing.Point(29, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(819, 173);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos Generales";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "GastosBancarios", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.textBox1.Location = new System.Drawing.Point(566, 102);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(194, 21);
            this.textBox1.TabIndex = 26;
            this.textBox1.TabStop = false;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Usuario_TB
            // 
            this.Usuario_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Usuario", true));
            this.Usuario_TB.Location = new System.Drawing.Point(655, 27);
            this.Usuario_TB.Name = "Usuario_TB";
            this.Usuario_TB.ReadOnly = true;
            this.Usuario_TB.Size = new System.Drawing.Size(139, 21);
            this.Usuario_TB.TabIndex = 24;
            this.Usuario_TB.TabStop = false;
            this.Usuario_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Estado_BT
            // 
            this.Estado_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Estado_BT.Location = new System.Drawing.Point(480, 26);
            this.Estado_BT.Name = "Estado_BT";
            this.Estado_BT.Size = new System.Drawing.Size(28, 22);
            this.Estado_BT.TabIndex = 22;
            this.Estado_BT.UseVisualStyleBackColor = true;
            // 
            // CuentaDestino_BT
            // 
            this.CuentaDestino_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.CuentaDestino_BT.Location = new System.Drawing.Point(766, 135);
            this.CuentaDestino_BT.Name = "CuentaDestino_BT";
            this.CuentaDestino_BT.Size = new System.Drawing.Size(28, 22);
            this.CuentaDestino_BT.TabIndex = 21;
            this.CuentaDestino_BT.UseVisualStyleBackColor = true;
            // 
            // CuentaDestino_TB
            // 
            this.CuentaDestino_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaDestino", true));
            this.CuentaDestino_TB.Location = new System.Drawing.Point(513, 136);
            this.CuentaDestino_TB.Name = "CuentaDestino_TB";
            this.CuentaDestino_TB.ReadOnly = true;
            this.CuentaDestino_TB.Size = new System.Drawing.Size(247, 21);
            this.CuentaDestino_TB.TabIndex = 20;
            this.CuentaDestino_TB.TabStop = false;
            this.CuentaDestino_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CuentaOrigen_BT
            // 
            this.CuentaOrigen_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.CuentaOrigen_BT.Location = new System.Drawing.Point(355, 135);
            this.CuentaOrigen_BT.Name = "CuentaOrigen_BT";
            this.CuentaOrigen_BT.Size = new System.Drawing.Size(28, 22);
            this.CuentaOrigen_BT.TabIndex = 18;
            this.CuentaOrigen_BT.UseVisualStyleBackColor = true;
            // 
            // FEcha_DTP
            // 
            this.FEcha_DTP.Checked = false;
            this.FEcha_DTP.CustomFormat = "dd/MM/yyyy HH:mm";
            this.FEcha_DTP.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "Fecha", true));
            this.FEcha_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FEcha_DTP.Location = new System.Drawing.Point(127, 72);
            this.FEcha_DTP.Name = "FEcha_DTP";
            this.FEcha_DTP.ShowCheckBox = true;
            this.FEcha_DTP.Size = new System.Drawing.Size(142, 21);
            this.FEcha_DTP.TabIndex = 17;
            this.FEcha_DTP.TabStop = false;
            // 
            // CuentaOrigen_TB
            // 
            this.CuentaOrigen_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaOrigen", true));
            this.CuentaOrigen_TB.Location = new System.Drawing.Point(102, 136);
            this.CuentaOrigen_TB.Name = "CuentaOrigen_TB";
            this.CuentaOrigen_TB.ReadOnly = true;
            this.CuentaOrigen_TB.Size = new System.Drawing.Size(247, 21);
            this.CuentaOrigen_TB.TabIndex = 15;
            this.CuentaOrigen_TB.TabStop = false;
            this.CuentaOrigen_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Importe", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.textBox4.Location = new System.Drawing.Point(566, 73);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(194, 21);
            this.textBox4.TabIndex = 13;
            this.textBox4.TabStop = false;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Estado_TB
            // 
            this.Estado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "EstadoLabel", true));
            this.Estado_TB.Location = new System.Drawing.Point(296, 27);
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
            this.Codigo_TB.Location = new System.Drawing.Point(86, 27);
            this.Codigo_TB.Name = "Codigo_TB";
            this.Codigo_TB.ReadOnly = true;
            this.Codigo_TB.Size = new System.Drawing.Size(54, 21);
            this.Codigo_TB.TabIndex = 3;
            this.Codigo_TB.TabStop = false;
            this.Codigo_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(29, 105);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(92, 13);
            label7.TabIndex = 27;
            label7.Text = "Fecha Recepción:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dateTimePicker1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "FechaRecepcion", true));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(127, 99);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(142, 21);
            this.dateTimePicker1.TabIndex = 28;
            this.dateTimePicker1.TabStop = false;
            // 
            // TraspasoForm
            // 
            this.ClientSize = new System.Drawing.Size(868, 436);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TraspasoForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "TraspasoForm";
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
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.GroupBox groupBox3;
		protected System.Windows.Forms.TextBox Observaciones_TB;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DateTimePicker FEcha_DTP;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.TextBox Codigo_TB;
		private System.Windows.Forms.TextBox Usuario_TB;
		protected System.Windows.Forms.TextBox Estado_TB;
		protected System.Windows.Forms.TextBox CuentaOrigen_TB;
		protected System.Windows.Forms.Button CuentaDestino_BT;
		protected System.Windows.Forms.TextBox CuentaDestino_TB;
		protected System.Windows.Forms.Button CuentaOrigen_BT;
		protected System.Windows.Forms.Button Estado_BT;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
		
		

    }
}
