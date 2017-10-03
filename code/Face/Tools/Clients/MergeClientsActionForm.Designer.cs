namespace moleQule.Face.Invoice
{
    partial class MergeClientsActionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergeClientsActionForm));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Source_TB = new System.Windows.Forms.TextBox();
            this.Source_BT = new System.Windows.Forms.Button();
            this.Browser = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Destiny_TB = new System.Windows.Forms.TextBox();
            this.Destiny_BT = new System.Windows.Forms.Button();
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
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.Submit_BT.Location = new System.Drawing.Point(220, 7);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            this.Submit_BT.Text = "&Exportar";
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(310, 7);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Source_GB
            // 
            this.Source_GB.Controls.Add(this.groupBox1);
            this.Source_GB.Controls.Add(this.groupBox4);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(617, 145);
            this.Source_GB.Text = " ";
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
            this.PanelesV.Size = new System.Drawing.Size(619, 187);
            this.PanelesV.SplitterDistance = 147;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(105, 24);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(619, 187);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(277, 145);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(277, 60);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Source_TB);
            this.groupBox4.Controls.Add(this.Source_BT);
            this.groupBox4.Location = new System.Drawing.Point(24, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(568, 50);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Copiar Datos del Cliente";
            // 
            // Source_TB
            // 
            this.Source_TB.Location = new System.Drawing.Point(23, 19);
            this.Source_TB.Name = "Source_TB";
            this.Source_TB.ReadOnly = true;
            this.Source_TB.Size = new System.Drawing.Size(477, 21);
            this.Source_TB.TabIndex = 18;
            // 
            // Source_BT
            // 
            this.Source_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Source_BT.Location = new System.Drawing.Point(506, 17);
            this.Source_BT.Name = "Source_BT";
            this.Source_BT.Size = new System.Drawing.Size(42, 23);
            this.Source_BT.TabIndex = 17;
            this.Source_BT.UseVisualStyleBackColor = true;
            this.Source_BT.Click += new System.EventHandler(this.Source_BT_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Destiny_TB);
            this.groupBox1.Controls.Add(this.Destiny_BT);
            this.groupBox1.Location = new System.Drawing.Point(24, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(568, 50);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "En el Cliente";
            // 
            // Destiny_TB
            // 
            this.Destiny_TB.Location = new System.Drawing.Point(23, 19);
            this.Destiny_TB.Name = "Destiny_TB";
            this.Destiny_TB.ReadOnly = true;
            this.Destiny_TB.Size = new System.Drawing.Size(477, 21);
            this.Destiny_TB.TabIndex = 18;
            // 
            // Destiny_BT
            // 
            this.Destiny_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Destiny_BT.Location = new System.Drawing.Point(506, 17);
            this.Destiny_BT.Name = "Destiny_BT";
            this.Destiny_BT.Size = new System.Drawing.Size(42, 23);
            this.Destiny_BT.TabIndex = 17;
            this.Destiny_BT.UseVisualStyleBackColor = true;
            this.Destiny_BT.Click += new System.EventHandler(this.Destiny_BT_Click);
            // 
            // MergeClientsActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(619, 187);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MergeClientsActionForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Unir Clientes";
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox Source_TB;
        private System.Windows.Forms.Button Source_BT;
        private System.Windows.Forms.FolderBrowserDialog Browser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Destiny_TB;
        private System.Windows.Forms.Button Destiny_BT;
    }
}
