namespace moleQule.Face.Invoice
{
    partial class CobroFomentoAddForm
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
			this.Facturas_SC.SuspendLayout();
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
			this.Importe_NTB.TextChanged += new System.EventHandler(this.Importe_NTB_TextChanged);
			this.Importe_NTB.Validated += new System.EventHandler(this.Importe_NTB_Validated);
			// 
			// CuentaBancaria_TB
			// 
			this.CuentaBancaria_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// mQDateTimePicker1
			// 
			this.Vencimiento_DTP.Size = new System.Drawing.Size(115, 21);
			// 
			// mQDateTimePicker2
			// 
			this.mQDateTimePicker2.Size = new System.Drawing.Size(115, 21);
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
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(-35, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(55, 7);
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
			this.Imprimir_Button.Location = new System.Drawing.Point(631, 7);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Location = new System.Drawing.Point(541, 7);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// CobroFomentoAddForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(1074, 626);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "CobroFomentoAddForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Nuevo Cobro Fomento";
			((System.ComponentModel.ISupportInitialize)(this.Datos_Facturas)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Tipo)).EndInit();
			this.Facturas_SC.ResumeLayout(false);
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
