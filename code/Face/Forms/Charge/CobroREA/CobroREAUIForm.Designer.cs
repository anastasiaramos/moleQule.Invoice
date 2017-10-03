namespace moleQule.Face.Invoice
{
    partial class CobroREAUIForm
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
			((System.ComponentModel.ISupportInitialize)(this.Datos_Facturas)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Tipo)).BeginInit();
			this.Facturas_Panel.SuspendLayout();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.SuspendLayout();
			// 
			// Importe_NTB
			// 
			this.Importe_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Importe_NTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.Importe_NTB.Validated += new System.EventHandler(this.Importe_NTB_Validated);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// CuentaBancaria_TB
			// 
			this.CuentaBancaria_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.CuentaBancaria_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.CuentaBancaria_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// mQDateTimePicker1
			// 
			this.mQDateTimePicker1.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.mQDateTimePicker1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.mQDateTimePicker1.Size = new System.Drawing.Size(100, 21);
			// 
			// mQDateTimePicker2
			// 
			this.mQDateTimePicker2.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.mQDateTimePicker2.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.mQDateTimePicker2.Size = new System.Drawing.Size(100, 21);
			// 
			// NoAsignado_TB
			// 
			this.NoAsignado_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.NoAsignado_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.NoAsignado_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// Cuenta_BT
			// 
			this.Cuenta_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Cuenta_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Cuenta_BT.Click += new System.EventHandler(this.Cuenta_BT_Click);
			// 
			// Ninguno_BT
			// 
			this.Ninguno_BT.Click += new System.EventHandler(this.Liberar_BT_Click);
			// 
			// Todos_BT
			// 
			this.Todos_BT.Click += new System.EventHandler(this.Repartir_BT_Click);
			// 
			// TipoExpediente_CB
			// 
			this.TipoExpediente_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.TipoExpediente_CB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.TipoExpediente_CB.SelectedIndexChanged += new System.EventHandler(this.TipoExpediente_CB_SelectedIndexChanged);
			// 
			// MedioPago_TB
			// 
			this.MedioPago_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.MedioPago_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MedioPago_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// MedioPago_BT
			// 
			this.MedioPago_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.MedioPago_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.MedioPago_BT.Click += new System.EventHandler(this.MedioPago_BT_Click);
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
			this.PanelesV.Size = new System.Drawing.Size(1074, 626);
			this.PanelesV.SplitterDistance = 585;
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(496, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(607, 7);
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
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Location = new System.Drawing.Point(235, 7);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Location = new System.Drawing.Point(145, 7);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Location = new System.Drawing.Point(328, 229);
			// 
			// CobroREAUIForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1074, 626);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "CobroREAUIForm";
			this.HelpProvider.SetShowHelp(this, true);
			((System.ComponentModel.ISupportInitialize)(this.Datos_Facturas)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Tipo)).EndInit();
			this.Facturas_Panel.ResumeLayout(false);
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel1.PerformLayout();
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			this.Paneles2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.ProgressBK_Panel.ResumeLayout(false);
			this.ProgressBK_Panel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
    }
}
