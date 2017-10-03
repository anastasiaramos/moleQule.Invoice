namespace moleQule.Face.Invoice
{
	partial class CompanyExporterActionForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompanyExporterActionForm));
			this.Datos_Tipos = new System.Windows.Forms.BindingSource(this.components);
			this.Origen_GB = new System.Windows.Forms.GroupBox();
			this.SourceOutInvoice_RB = new System.Windows.Forms.RadioButton();
			this.SourceOutDelivery_RB = new System.Windows.Forms.RadioButton();
			this.Datos_TipoAyuda = new System.Windows.Forms.BindingSource(this.components);
			this.Datos_TipoCobro = new System.Windows.Forms.BindingSource(this.components);
			this.Datos_TipoPago = new System.Windows.Forms.BindingSource(this.components);
			this.Datos_Estado = new System.Windows.Forms.BindingSource(this.components);
			this.Sources_GB = new System.Windows.Forms.GroupBox();
			this.Codes_BT = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.SourceCodes_TB = new System.Windows.Forms.TextBox();
			this.SourceHolder_TB = new System.Windows.Forms.TextBox();
			this.SourceHolder_BT = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.Browser = new System.Windows.Forms.FolderBrowserDialog();
			this.Datos_TiposAcreedor = new System.Windows.Forms.BindingSource(this.components);
			this.Datos_MedioPago = new System.Windows.Forms.BindingSource(this.components);
			this.Destination_GB = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.DestinationInInvoice_RB = new System.Windows.Forms.RadioButton();
			this.DestinationInDelivery_RB = new System.Windows.Forms.RadioButton();
			this.DestinationOutInvoice_RB = new System.Windows.Forms.RadioButton();
			this.DestinationOutDelivery_RB = new System.Windows.Forms.RadioButton();
			this.Company_TB = new System.Windows.Forms.TextBox();
			this.Company_BT = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.DestinationHolder_TB = new System.Windows.Forms.TextBox();
			this.DestinationHolder_BT = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.Icon_PB = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
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
			((System.ComponentModel.ISupportInitialize)(this.Datos_TipoAyuda)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_TipoCobro)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_TipoPago)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Estado)).BeginInit();
			this.Sources_GB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_TiposAcreedor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).BeginInit();
			this.Destination_GB.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Icon_PB)).BeginInit();
			this.SuspendLayout();
			// 
			// Print_BT
			// 
			this.Print_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Print_BT.Location = new System.Drawing.Point(208, 60);
			this.HelpProvider.SetShowHelp(this.Print_BT, true);
			this.Print_BT.Size = new System.Drawing.Size(87, 23);
			// 
			// Submit_BT
			// 
			this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Submit_BT.Location = new System.Drawing.Point(307, 3);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			this.Submit_BT.Text = "&Exportar";
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Cancel_BT.Location = new System.Drawing.Point(201, 3);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Source_GB
			// 
			this.Source_GB.Location = new System.Drawing.Point(17, 1);
			this.HelpProvider.SetShowHelp(this.Source_GB, true);
			this.Source_GB.Size = new System.Drawing.Size(10, 10);
			this.Source_GB.Text = " ";
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.Controls.Add(this.label1);
			this.PanelesV.Panel1.Controls.Add(this.Icon_PB);
			this.PanelesV.Panel1.Controls.Add(this.Sources_GB);
			this.PanelesV.Panel1.Controls.Add(this.Destination_GB);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(619, 593);
			this.PanelesV.SplitterDistance = 552;
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(105, 24);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(619, 593);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(277, 348);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(277, 263);
			// 
			// Origen_GB
			// 
			this.Origen_GB.Controls.Add(this.SourceOutInvoice_RB);
			this.Origen_GB.Controls.Add(this.SourceOutDelivery_RB);
			this.Origen_GB.Location = new System.Drawing.Point(54, 20);
			this.Origen_GB.Name = "Origen_GB";
			this.Origen_GB.Size = new System.Drawing.Size(460, 66);
			this.Origen_GB.TabIndex = 21;
			this.Origen_GB.TabStop = false;
			this.Origen_GB.Text = "Elementos a Exportar";
			// 
			// SourceOutInvoice_RB
			// 
			this.SourceOutInvoice_RB.AutoSize = true;
			this.SourceOutInvoice_RB.Enabled = false;
			this.SourceOutInvoice_RB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.SourceOutInvoice_RB.Location = new System.Drawing.Point(247, 26);
			this.SourceOutInvoice_RB.Name = "SourceOutInvoice_RB";
			this.SourceOutInvoice_RB.Size = new System.Drawing.Size(109, 17);
			this.SourceOutInvoice_RB.TabIndex = 4;
			this.SourceOutInvoice_RB.Text = "Facturas Emitidas";
			this.SourceOutInvoice_RB.UseVisualStyleBackColor = true;
			this.SourceOutInvoice_RB.CheckedChanged += new System.EventHandler(this.SourceOutInvoice_RB_CheckedChanged);
			// 
			// SourceOutDelivery_RB
			// 
			this.SourceOutDelivery_RB.AutoSize = true;
			this.SourceOutDelivery_RB.Checked = true;
			this.SourceOutDelivery_RB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.SourceOutDelivery_RB.Location = new System.Drawing.Point(105, 26);
			this.SourceOutDelivery_RB.Name = "SourceOutDelivery_RB";
			this.SourceOutDelivery_RB.Size = new System.Drawing.Size(115, 17);
			this.SourceOutDelivery_RB.TabIndex = 3;
			this.SourceOutDelivery_RB.TabStop = true;
			this.SourceOutDelivery_RB.Text = "Albaranes Emitidos";
			this.SourceOutDelivery_RB.UseVisualStyleBackColor = true;
			this.SourceOutDelivery_RB.CheckedChanged += new System.EventHandler(this.SourceOutDelivery_RB_CheckedChanged);
			// 
			// Sources_GB
			// 
			this.Sources_GB.Controls.Add(this.Codes_BT);
			this.Sources_GB.Controls.Add(this.Origen_GB);
			this.Sources_GB.Controls.Add(this.label3);
			this.Sources_GB.Controls.Add(this.SourceCodes_TB);
			this.Sources_GB.Controls.Add(this.SourceHolder_TB);
			this.Sources_GB.Controls.Add(this.SourceHolder_BT);
			this.Sources_GB.Controls.Add(this.label6);
			this.Sources_GB.Location = new System.Drawing.Point(24, 71);
			this.Sources_GB.Name = "Sources_GB";
			this.Sources_GB.Size = new System.Drawing.Size(568, 245);
			this.Sources_GB.TabIndex = 37;
			this.Sources_GB.TabStop = false;
			this.Sources_GB.Text = "Origen";
			// 
			// Codes_BT
			// 
			this.Codes_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
			this.Codes_BT.Location = new System.Drawing.Point(475, 149);
			this.Codes_BT.Name = "Codes_BT";
			this.Codes_BT.Size = new System.Drawing.Size(30, 23);
			this.Codes_BT.TabIndex = 21;
			this.Codes_BT.UseVisualStyleBackColor = true;
			this.Codes_BT.Click += new System.EventHandler(this.Codes_BT_Click);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(63, 149);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 16);
			this.label3.TabIndex = 20;
			this.label3.Text = "Códigos:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// SourceCodes_TB
			// 
			this.SourceCodes_TB.Enabled = false;
			this.SourceCodes_TB.Location = new System.Drawing.Point(130, 149);
			this.SourceCodes_TB.Multiline = true;
			this.SourceCodes_TB.Name = "SourceCodes_TB";
			this.SourceCodes_TB.ReadOnly = true;
			this.SourceCodes_TB.Size = new System.Drawing.Size(339, 58);
			this.SourceCodes_TB.TabIndex = 19;
			// 
			// SourceHolder_TB
			// 
			this.SourceHolder_TB.Location = new System.Drawing.Point(130, 115);
			this.SourceHolder_TB.Name = "SourceHolder_TB";
			this.SourceHolder_TB.ReadOnly = true;
			this.SourceHolder_TB.Size = new System.Drawing.Size(339, 21);
			this.SourceHolder_TB.TabIndex = 18;
			// 
			// SourceHolder_BT
			// 
			this.SourceHolder_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
			this.SourceHolder_BT.Location = new System.Drawing.Point(475, 114);
			this.SourceHolder_BT.Name = "SourceHolder_BT";
			this.SourceHolder_BT.Size = new System.Drawing.Size(30, 23);
			this.SourceHolder_BT.TabIndex = 17;
			this.SourceHolder_BT.UseVisualStyleBackColor = true;
			this.SourceHolder_BT.Click += new System.EventHandler(this.SourceHolder_BT_Click);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(63, 116);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(61, 16);
			this.label6.TabIndex = 6;
			this.label6.Text = "Titular:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Destination_GB
			// 
			this.Destination_GB.Controls.Add(this.groupBox1);
			this.Destination_GB.Controls.Add(this.Company_TB);
			this.Destination_GB.Controls.Add(this.Company_BT);
			this.Destination_GB.Controls.Add(this.label4);
			this.Destination_GB.Controls.Add(this.DestinationHolder_TB);
			this.Destination_GB.Controls.Add(this.DestinationHolder_BT);
			this.Destination_GB.Controls.Add(this.label2);
			this.Destination_GB.Location = new System.Drawing.Point(24, 333);
			this.Destination_GB.Name = "Destination_GB";
			this.Destination_GB.Size = new System.Drawing.Size(568, 194);
			this.Destination_GB.TabIndex = 38;
			this.Destination_GB.TabStop = false;
			this.Destination_GB.Text = "Destino";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.DestinationInInvoice_RB);
			this.groupBox1.Controls.Add(this.DestinationInDelivery_RB);
			this.groupBox1.Controls.Add(this.DestinationOutInvoice_RB);
			this.groupBox1.Controls.Add(this.DestinationOutDelivery_RB);
			this.groupBox1.Location = new System.Drawing.Point(54, 15);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(460, 83);
			this.groupBox1.TabIndex = 24;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Exportar como";
			// 
			// DestinationInInvoice_RB
			// 
			this.DestinationInInvoice_RB.AutoSize = true;
			this.DestinationInInvoice_RB.Enabled = false;
			this.DestinationInInvoice_RB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.DestinationInInvoice_RB.Location = new System.Drawing.Point(244, 24);
			this.DestinationInInvoice_RB.Name = "DestinationInInvoice_RB";
			this.DestinationInInvoice_RB.Size = new System.Drawing.Size(115, 17);
			this.DestinationInInvoice_RB.TabIndex = 6;
			this.DestinationInInvoice_RB.Text = "Facturas Recibidas";
			this.DestinationInInvoice_RB.UseVisualStyleBackColor = true;
			this.DestinationInInvoice_RB.CheckedChanged += new System.EventHandler(this.DestinationInInvoice_RB_CheckedChanged);
			// 
			// DestinationInDelivery_RB
			// 
			this.DestinationInDelivery_RB.AutoSize = true;
			this.DestinationInDelivery_RB.Checked = true;
			this.DestinationInDelivery_RB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.DestinationInDelivery_RB.Location = new System.Drawing.Point(102, 24);
			this.DestinationInDelivery_RB.Name = "DestinationInDelivery_RB";
			this.DestinationInDelivery_RB.Size = new System.Drawing.Size(121, 17);
			this.DestinationInDelivery_RB.TabIndex = 5;
			this.DestinationInDelivery_RB.TabStop = true;
			this.DestinationInDelivery_RB.Text = "Albaranes Recibidos";
			this.DestinationInDelivery_RB.UseVisualStyleBackColor = true;
			this.DestinationInDelivery_RB.CheckedChanged += new System.EventHandler(this.DestinationInDelivery_RB_CheckedChanged);
			// 
			// DestinationOutInvoice_RB
			// 
			this.DestinationOutInvoice_RB.AutoSize = true;
			this.DestinationOutInvoice_RB.Enabled = false;
			this.DestinationOutInvoice_RB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.DestinationOutInvoice_RB.Location = new System.Drawing.Point(244, 47);
			this.DestinationOutInvoice_RB.Name = "DestinationOutInvoice_RB";
			this.DestinationOutInvoice_RB.Size = new System.Drawing.Size(109, 17);
			this.DestinationOutInvoice_RB.TabIndex = 4;
			this.DestinationOutInvoice_RB.Text = "Facturas Emitidas";
			this.DestinationOutInvoice_RB.UseVisualStyleBackColor = true;
			this.DestinationOutInvoice_RB.CheckedChanged += new System.EventHandler(this.DestinationOutInvoice_RB_CheckedChanged);
			// 
			// DestinationOutDelivery_RB
			// 
			this.DestinationOutDelivery_RB.AutoSize = true;
			this.DestinationOutDelivery_RB.Enabled = false;
			this.DestinationOutDelivery_RB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.DestinationOutDelivery_RB.Location = new System.Drawing.Point(102, 47);
			this.DestinationOutDelivery_RB.Name = "DestinationOutDelivery_RB";
			this.DestinationOutDelivery_RB.Size = new System.Drawing.Size(115, 17);
			this.DestinationOutDelivery_RB.TabIndex = 3;
			this.DestinationOutDelivery_RB.Text = "Albaranes Emitidos";
			this.DestinationOutDelivery_RB.UseVisualStyleBackColor = true;
			this.DestinationOutDelivery_RB.CheckedChanged += new System.EventHandler(this.DestinationOutDelivery_RB_CheckedChanged);
			// 
			// Company_TB
			// 
			this.Company_TB.Location = new System.Drawing.Point(130, 119);
			this.Company_TB.Name = "Company_TB";
			this.Company_TB.ReadOnly = true;
			this.Company_TB.Size = new System.Drawing.Size(339, 21);
			this.Company_TB.TabIndex = 23;
			// 
			// Company_BT
			// 
			this.Company_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
			this.Company_BT.Location = new System.Drawing.Point(475, 118);
			this.Company_BT.Name = "Company_BT";
			this.Company_BT.Size = new System.Drawing.Size(30, 23);
			this.Company_BT.TabIndex = 22;
			this.Company_BT.UseVisualStyleBackColor = true;
			this.Company_BT.Click += new System.EventHandler(this.Company_BT_Click);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(63, 121);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(61, 16);
			this.label4.TabIndex = 21;
			this.label4.Text = "Empresa:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// DestinationHolder_TB
			// 
			this.DestinationHolder_TB.Location = new System.Drawing.Point(130, 151);
			this.DestinationHolder_TB.Name = "DestinationHolder_TB";
			this.DestinationHolder_TB.ReadOnly = true;
			this.DestinationHolder_TB.Size = new System.Drawing.Size(339, 21);
			this.DestinationHolder_TB.TabIndex = 18;
			// 
			// DestinationHolder_BT
			// 
			this.DestinationHolder_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
			this.DestinationHolder_BT.Location = new System.Drawing.Point(475, 150);
			this.DestinationHolder_BT.Name = "DestinationHolder_BT";
			this.DestinationHolder_BT.Size = new System.Drawing.Size(30, 23);
			this.DestinationHolder_BT.TabIndex = 17;
			this.DestinationHolder_BT.UseVisualStyleBackColor = true;
			this.DestinationHolder_BT.Click += new System.EventHandler(this.DestinationHolder_BT_Click);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(63, 153);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 16);
			this.label2.TabIndex = 6;
			this.label2.Text = "Titular:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Icon_PB
			// 
			this.Icon_PB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.Icon_PB.Image = global::moleQule.Face.Invoice.Properties.Resources.data_out_48;
			this.Icon_PB.Location = new System.Drawing.Point(168, 12);
			this.Icon_PB.Name = "Icon_PB";
			this.Icon_PB.Size = new System.Drawing.Size(48, 48);
			this.Icon_PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.Icon_PB.TabIndex = 39;
			this.Icon_PB.TabStop = false;
			this.Icon_PB.Tag = "NO FORMAT";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(222, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(226, 22);
			this.label1.TabIndex = 40;
			this.label1.Tag = "NO FORMAT";
			this.label1.Text = "Exportar datos a otra empresa";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CompanyExporterActionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(619, 593);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CompanyExporterActionForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Exportar a Empresa";
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel1.PerformLayout();
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
			((System.ComponentModel.ISupportInitialize)(this.Datos_TipoAyuda)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_TipoCobro)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_TipoPago)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Estado)).EndInit();
			this.Sources_GB.ResumeLayout(false);
			this.Sources_GB.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_TiposAcreedor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).EndInit();
			this.Destination_GB.ResumeLayout(false);
			this.Destination_GB.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Icon_PB)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.BindingSource Datos_Tipos;
		private System.Windows.Forms.GroupBox Origen_GB;
        private System.Windows.Forms.GroupBox Sources_GB;
        private System.Windows.Forms.TextBox SourceHolder_TB;
		private System.Windows.Forms.Button SourceHolder_BT;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.FolderBrowserDialog Browser;
		private System.Windows.Forms.BindingSource Datos_TiposAcreedor;
		private System.Windows.Forms.BindingSource Datos_MedioPago;
		private System.Windows.Forms.BindingSource Datos_Estado;
		private System.Windows.Forms.BindingSource Datos_TipoPago;
		private System.Windows.Forms.BindingSource Datos_TipoCobro;
		private System.Windows.Forms.BindingSource Datos_TipoAyuda;
		private System.Windows.Forms.RadioButton SourceOutInvoice_RB;
		private System.Windows.Forms.RadioButton SourceOutDelivery_RB;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox SourceCodes_TB;
		private System.Windows.Forms.GroupBox Destination_GB;
		private System.Windows.Forms.TextBox Company_TB;
		private System.Windows.Forms.Button Company_BT;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox DestinationHolder_TB;
		private System.Windows.Forms.Button DestinationHolder_BT;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button Codes_BT;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton DestinationOutInvoice_RB;
		private System.Windows.Forms.RadioButton DestinationOutDelivery_RB;
		private System.Windows.Forms.RadioButton DestinationInInvoice_RB;
		private System.Windows.Forms.RadioButton DestinationInDelivery_RB;
		private System.Windows.Forms.PictureBox Icon_PB;
		private System.Windows.Forms.Label label1;
    }
}
