namespace moleQule.Face.Invoice
{
    partial class PedidoUIForm
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
			// Cliente_BT
			// 
			this.Cliente_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Cliente_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Cliente_BT.Click += new System.EventHandler(this.Cliente_BT_Click);
			// 
			// Cliente_TB
			// 
			this.Cliente_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Cliente_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Cliente_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// IDCliente_TB
			// 
			this.IDCliente_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.IDCliente_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.IDCliente_TB.ForeColor = System.Drawing.Color.Black;
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
			// 
			// Observaciones_TB
			// 
			this.Observaciones_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Observaciones_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
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
			this.Fecha_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.Fecha_DTP.Checked = true;
			this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// IDPedido_TB
			// 
			this.IDPedido_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.IDPedido_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.IDPedido_TB.ForeColor = System.Drawing.Color.Black;
			this.IDPedido_TB.Mask = "00000";
			this.IDPedido_TB.TabStop = false;
			// 
			// Estado_BT
			// 
			this.Estado_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Estado_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Estado_BT.Click += new System.EventHandler(this.Estado_BT_Click);
			// 
			// Estado_TB
			// 
			this.Estado_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Estado_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Estado_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// PDescuento_NTB
			// 
			this.PDescuento_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.PDescuento_NTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.PDescuento_NTB.Size = new System.Drawing.Size(54, 21);
			this.PDescuento_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.textBox1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.textBox1.ForeColor = System.Drawing.Color.Black;
			// 
			// DescuentoC_NTB
			// 
			this.DescuentoC_NTB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.DescuentoC_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.DescuentoC_NTB.ForeColor = System.Drawing.Color.Black;
			this.DescuentoC_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// Expediente_BT
			// 
			this.Expediente_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Expediente_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Expediente_BT.Click += new System.EventHandler(this.Expediente_BT_Click);
			// 
			// Expediente_TB
			// 
			this.Expediente_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Expediente_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Expediente_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// Almacen_BT
			// 
			this.Almacen_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Almacen_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Almacen_BT.Click += new System.EventHandler(this.Almacen_BT_Click);
			// 
			// Almacen_TB
			// 
			this.Almacen_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Almacen_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Almacen_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// Serie_BT
			// 
			this.Serie_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Serie_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Serie_BT.Click += new System.EventHandler(this.Serie_BT_Click);
			// 
			// Serie_TB
			// 
			this.Serie_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Serie_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Serie_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// Usuario_BT
			// 
			this.Usuario_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Usuario_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Usuario_BT.Click += new System.EventHandler(this.Usuario_BT_Click);
			// 
			// Usuario_TB
			// 
			this.Usuario_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Usuario_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Usuario_TB.ForeColor = System.Drawing.Color.Black;
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
			this.Submit_BT.Location = new System.Drawing.Point(468, 2);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(601, 2);
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
			// PedidoUIForm
			// 
			this.ClientSize = new System.Drawing.Size(1194, 722);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "PedidoUIForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "PedidoProveedorUIForm";
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
