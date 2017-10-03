namespace moleQule.Face.Invoice
{
	partial class ClientViewForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Pages_TP.SuspendLayout();
            this.General_TP.SuspendLayout();
            this.Precios_TP.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Observaciones_TP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_ProductoCliente)).BeginInit();
            this.Products_SC.Panel1.SuspendLayout();
            this.Products_SC.SuspendLayout();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            this.SuspendLayout();
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Invoice.ClienteInfo);
            // 
            // ClienteViewForm
            // 
            this.CancelButton = this.Submit_BT;
            this.HelpProvider.SetHelpKeyword(this, "60");
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "ClienteViewForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "ClienteViewForm";
            this.Pages_TP.ResumeLayout(false);
            this.General_TP.ResumeLayout(false);
            this.General_TP.PerformLayout();
            this.Precios_TP.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Observaciones_TP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos_ProductoCliente)).EndInit();
            this.Products_SC.Panel1.ResumeLayout(false);
            this.Products_SC.ResumeLayout(false);
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
	}
}
