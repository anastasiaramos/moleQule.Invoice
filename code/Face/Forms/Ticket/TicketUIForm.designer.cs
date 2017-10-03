namespace moleQule.Face.Invoice
{
    partial class TicketUIForm
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
			// Datos_Concepto
			// 
			this.Datos_Concepto.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.Datos_Concepto_ListChanged);
			this.groupBox3.Controls.SetChildIndex(this.Tipo_TB, 0);
			this.groupBox3.Controls.SetChildIndex(this.label5, 0);
			this.groupBox3.Controls.SetChildIndex(this.Tipo_BT, 0);
			this.groupBox3.Controls.SetChildIndex(this.TPV_TB, 0);
			this.groupBox3.Controls.SetChildIndex(this.label3, 0);
			this.groupBox3.Controls.SetChildIndex(this.TPV_BT, 0);
			this.groupBox3.Controls.SetChildIndex(this.Serie_TB, 0);
			this.groupBox3.Controls.SetChildIndex(this.Serie_BT, 0);
			this.groupBox3.Controls.SetChildIndex(this.NTicket_TB, 0);
			this.groupBox3.Controls.SetChildIndex(this.Fecha_DTP, 0);
			this.groupBox3.Controls.SetChildIndex(this.NumeroSerie_TB, 0);
			this.groupBox3.Controls.SetChildIndex(this.MedioPago_CB, 0);
			this.groupBox3.Controls.SetChildIndex(this.NTicketManual_CKB, 0);
			this.groupBox3.Controls.SetChildIndex(this.FormaPago_CB, 0);
			this.groupBox3.Controls.SetChildIndex(this.Prevision_TB, 0);
			// 
			// NumeroSerie_TB
			// 
			this.NumeroSerie_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.NumeroSerie_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.NumeroSerie_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// Fecha_DTP
			// 
			this.Fecha_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.Fecha_DTP.Checked = true;
			this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Fecha_DTP.ValueChanged += new System.EventHandler(this.Fecha_DTP_ValueChanged);
			// 
			// NTicket_TB
			// 
			this.NTicket_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.NTicket_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.NTicket_TB.ForeColor = System.Drawing.Color.Black;
			this.NTicket_TB.TabStop = false;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// MedioPago_CB
			// 
			this.MedioPago_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.MedioPago_CB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.MedioPago_CB.SelectedIndexChanged += new System.EventHandler(this.MedioPago_CB_SelectedIndexChanged);
			// 
			// PDescuento_NTB
			// 
			this.PDescuento_NTB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.PDescuento_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.PDescuento_NTB.ForeColor = System.Drawing.Color.Black;
			this.PDescuento_NTB.Size = new System.Drawing.Size(54, 21);
			this.PDescuento_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// Descuento_TB
			// 
			this.Descuento_TB.TextChanged += new System.EventHandler(this.Descuento_NTB_TextChanged);
			// 
			// FormaPago_CB
			// 
			this.FormaPago_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.FormaPago_CB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.FormaPago_CB.SelectedIndexChanged += new System.EventHandler(this.FormaPago_CB_SelectedIndexChanged);
			// 
			// Prevision_TB
			// 
			this.Prevision_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Prevision_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Prevision_TB.ForeColor = System.Drawing.Color.Black;
			this.Prevision_TB.TabStop = false;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// Obs_TB
			// 
			this.Obs_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Obs_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// Tipo_TB
			// 
			this.Tipo_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Tipo_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Tipo_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.Tipo_TB.TabStop = false;
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// Albaranes_TB
			// 
			this.Albaranes_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Albaranes_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// Serie_TB
			// 
			this.Serie_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Serie_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Serie_TB.ForeColor = System.Drawing.Color.Black;
			// 
			// Serie_BT
			// 
			this.Serie_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Serie_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Serie_BT.Click += new System.EventHandler(this.Serie_BT_Click);
			// 
			// Tipo_BT
			// 
			this.Tipo_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Tipo_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			// 
			// TPV_BT
			// 
			this.TPV_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.TPV_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.TPV_BT.Click += new System.EventHandler(this.TPV_BT_Click);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F);
			// 
			// TPV_TB
			// 
			this.TPV_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.TPV_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.TPV_TB.ForeColor = System.Drawing.Color.Black;
			this.TPV_TB.TabStop = false;
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
			this.PanelesV.Size = new System.Drawing.Size(1194, 692);
			this.PanelesV.SplitterDistance = 651;
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(-47, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(49, 7);
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
			this.Paneles2.Size = new System.Drawing.Size(1192, 38);
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Location = new System.Drawing.Point(241, 7);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Location = new System.Drawing.Point(145, 7);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Location = new System.Drawing.Point(418, 264);
			// 
			// TicketUIForm
			// 
			this.ClientSize = new System.Drawing.Size(1194, 692);
			this.HelpProvider.SetHelpKeyword(this, "60");
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "TicketUIForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "TicketUIForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TicketUIForm_FormClosing);
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
			this.ProgressBK_Panel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
    }
}
