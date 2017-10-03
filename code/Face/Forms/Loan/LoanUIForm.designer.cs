namespace moleQule.Face.Invoice
{
    partial class LoanUIForm
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
            ((System.ComponentModel.ISupportInitialize)(this.Payments_BS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InterestRates_BS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pie_Panel)).BeginInit();
            this.Pie_Panel.Panel1.SuspendLayout();
            this.Pie_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Content_Panel)).BeginInit();
            this.Content_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            this.SuspendLayout();
            // 
            // General_TP
            // 
            this.General_TP.Size = new System.Drawing.Size(926, 600);
            // 
            // Pago_BT
            // 
            this.Pago_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Pago_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Pago_BT.Click += new System.EventHandler(this.Pago_BT_Click);
            // 
            // Periodicidad_TB
            // 
            this.Periodicidad_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Periodicidad_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // Cuotas_TB
            // 
            this.Cuotas_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Cuotas_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // InicioPago_DTP
            // 
            this.InicioPago_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.InicioPago_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.InicioPago_DTP.ShowCheckBox = true;
            this.InicioPago_DTP.Size = new System.Drawing.Size(115, 21);
            // 
            // FechaFirma_DTP
            // 
            this.FechaFirma_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.FechaFirma_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FechaFirma_DTP.ShowCheckBox = true;
            this.FechaFirma_DTP.Size = new System.Drawing.Size(115, 21);
            // 
            // FechaIngreso_DTP
            // 
            this.FechaIngreso_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.FechaIngreso_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FechaIngreso_DTP.ShowCheckBox = true;
            this.FechaIngreso_DTP.Size = new System.Drawing.Size(115, 21);
            // 
            // FechaVencimiento_DTP
            // 
            this.FechaVencimiento_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.FechaVencimiento_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FechaVencimiento_DTP.ShowCheckBox = true;
            this.FechaVencimiento_DTP.Size = new System.Drawing.Size(115, 21);
            // 
            // Gastos_TB
            // 
            this.Gastos_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Gastos_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // CuentaContable_TB
            // 
            this.CuentaContable_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.CuentaContable_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // NoContabilizar_BT
            // 
            this.NoContabilizar_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.NoContabilizar_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // Status_BT
            // 
            this.Status_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Status_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Status_BT.Click += new System.EventHandler(this.Status_BT_Click);
            // 
            // Status_TB
            // 
            this.Status_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Status_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Status_TB.ForeColor = System.Drawing.Color.Black;
            this.Status_TB.TabStop = false;
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
            this.PanelesV.Size = new System.Drawing.Size(936, 722);
            // 
            // Submit_BT
            // 
            this.Submit_BT.Location = new System.Drawing.Point(335, 2);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(468, 2);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Pie_Panel
            // 
            // 
            // Pie_Panel.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Pie_Panel.Panel1, true);
            // 
            // Pie_Panel.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.Pie_Panel.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Pie_Panel, true);
            this.Pie_Panel.Size = new System.Drawing.Size(934, 38);
            // 
            // Content_Panel
            // 
            // 
            // Content_Panel.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Content_Panel.Panel1, true);
            // 
            // Content_Panel.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.Content_Panel.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Content_Panel, true);
            this.Content_Panel.Size = new System.Drawing.Size(934, 679);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(289, 114);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(936, 722);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(431, 409);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(431, 324);
            // 
            // LoanUIForm
            // 
            this.ClientSize = new System.Drawing.Size(936, 722);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "LoanUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "PrestamoUIForm";
            ((System.ComponentModel.ISupportInitialize)(this.Payments_BS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InterestRates_BS)).EndInit();
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            this.Pie_Panel.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pie_Panel)).EndInit();
            this.Pie_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Content_Panel)).EndInit();
            this.Content_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            this.ProgressBK_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
	}
}
