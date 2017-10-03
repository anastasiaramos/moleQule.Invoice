namespace moleQule.Face.Invoice
{
    partial class PedidoAddForm
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
			((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Cliente)).BeginInit();
			this.Cliente_GB.SuspendLayout();
			this.Impresion_GB.SuspendLayout();
			this.General_GB.SuspendLayout();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Pie_Panel.Panel1.SuspendLayout();
			this.Pie_Panel.SuspendLayout();
			this.Content_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.SuspendLayout();
			this.Cliente_GB.Controls.SetChildIndex(this.VatNumberTB, 0);
			this.Cliente_GB.Controls.SetChildIndex(this.IDCliente_TB, 0);
			this.Cliente_GB.Controls.SetChildIndex(this.Cliente_TB, 0);
			this.Cliente_GB.Controls.SetChildIndex(this.Cliente_BT, 0);
			this.Cliente_GB.Controls.SetChildIndex(this.DescuentoC_NTB, 0);
			this.Cliente_GB.Controls.SetChildIndex(this.textBox1, 0);
			// 
			// Codigo_TB
			// 
			this.VatNumberTB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.VatNumberTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.VatNumberTB.ForeColor = System.Drawing.Color.Black;
			this.Impresion_GB.Controls.SetChildIndex(this.Estado_BT, 0);
			this.Impresion_GB.Controls.SetChildIndex(this.Estado_TB, 0);
			this.Impresion_GB.Controls.SetChildIndex(this.Observaciones_TB, 0);
			this.Impresion_GB.Controls.SetChildIndex(this.label14, 0);
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.General_GB.Controls.SetChildIndex(this.IDPedido_TB, 0);
			this.General_GB.Controls.SetChildIndex(this.IDManual_CkB, 0);
			this.General_GB.Controls.SetChildIndex(this.Fecha_DTP, 0);
			this.General_GB.Controls.SetChildIndex(this.Usuario_TB, 0);
			this.General_GB.Controls.SetChildIndex(this.Usuario_BT, 0);
			this.General_GB.Controls.SetChildIndex(this.Serie_TB, 0);
			this.General_GB.Controls.SetChildIndex(this.Serie_BT, 0);
			this.General_GB.Controls.SetChildIndex(this.Almacen_TB, 0);
			this.General_GB.Controls.SetChildIndex(this.Almacen_BT, 0);
			this.General_GB.Controls.SetChildIndex(this.Expediente_TB, 0);
			this.General_GB.Controls.SetChildIndex(this.Expediente_BT, 0);
			// 
			// Fecha_DTP
			// 
			this.Fecha_DTP.Checked = true;
			// 
			// IDPedido_TB
			// 
			this.IDPedido_TB.Mask = "00000";
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
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
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
			// 
			// PedidoAddForm
			// 
			this.ClientSize = new System.Drawing.Size(1194, 722);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "PedidoAddForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Nuevo Pedido";
			((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Cliente)).EndInit();
			this.Cliente_GB.ResumeLayout(false);
			this.Cliente_GB.PerformLayout();
			this.Impresion_GB.ResumeLayout(false);
			this.Impresion_GB.PerformLayout();
			this.General_GB.ResumeLayout(false);
			this.General_GB.PerformLayout();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.Pie_Panel.Panel1.ResumeLayout(false);
			this.Pie_Panel.ResumeLayout(false);
			this.Content_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.ProgressBK_Panel.ResumeLayout(false);
			this.ProgressBK_Panel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
	}
}
