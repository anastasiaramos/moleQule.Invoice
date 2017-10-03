namespace moleQule.Face.Invoice
{
    partial class TicketEditForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TicketEditForm));
			((System.ComponentModel.ISupportInitialize)(this.Datos_Concepto)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Expediente)).BeginInit();
			this.Impresion_GB.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).BeginInit();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.SuspendLayout();
			// 
			// Impresion_GB
			// 
			this.HelpProvider.SetShowHelp(this.Impresion_GB, ((bool)(resources.GetObject("Impresion_GB.ShowHelp"))));
			// 
			// groupBox3
			// 
			this.HelpProvider.SetShowHelp(this.groupBox3, ((bool)(resources.GetObject("groupBox3.ShowHelp"))));
			this.groupBox3.Controls.SetChildIndex(this.NTicket_TB, 0);
			this.groupBox3.Controls.SetChildIndex(this.Fecha_DTP, 0);
			this.groupBox3.Controls.SetChildIndex(this.NumeroSerie_TB, 0);
			this.groupBox3.Controls.SetChildIndex(this.MedioPago_CB, 0);
			this.groupBox3.Controls.SetChildIndex(this.NTicketManual_CKB, 0);
			this.groupBox3.Controls.SetChildIndex(this.FormaPago_CB, 0);
			this.groupBox3.Controls.SetChildIndex(this.Prevision_TB, 0);
			// 
			// TicketEditForm
			// 
			resources.ApplyResources(this, "$this");
			this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
			this.Name = "TicketEditForm";
			this.Text = "Modificar Ticket";
			this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			((System.ComponentModel.ISupportInitialize)(this.Datos_Concepto)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_MedioPago)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Expediente)).EndInit();
			this.Impresion_GB.ResumeLayout(false);
			this.Impresion_GB.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).EndInit();
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			this.Paneles2.Panel1.PerformLayout();
			this.Paneles2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
    }
}
