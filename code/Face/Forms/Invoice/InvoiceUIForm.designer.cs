namespace moleQule.Face.Invoice
{
    partial class InvoiceUIForm
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
            ((System.ComponentModel.ISupportInitialize)(this.Lines_BS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentMethod_BS)).BeginInit();
            this.Impresion_GB.SuspendLayout();
            this.General_GB.SuspendLayout();
            this.Cliente_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Client_BS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).BeginInit();
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
            // Datos_Concepto
            // 
            this.Lines_BS.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.Datos_Concepto_ListChanged);
            this.Impresion_GB.Controls.SetChildIndex(this.Nota_TB, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.notaCheckBox, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.Cuenta_TB, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.label5, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.DiasPago_NTB, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.Obs_TB, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.MedioPago_CB, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.label14, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.label15, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.EstadoCobro_TB, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.label4, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.PIRPF_NTB, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.FormaPago_CB, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.label20, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.Albaranes_TB, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.label19, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.EstadoCobro_BT, 0);
            this.Impresion_GB.Controls.SetChildIndex(this.Prevision_TB, 0);
            // 
            // Nota_TB
            // 
            this.Nota_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Nota_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.General_GB.Controls.SetChildIndex(this.NFactura_TB, 0);
            this.General_GB.Controls.SetChildIndex(this.Fecha_DTP, 0);
            this.General_GB.Controls.SetChildIndex(this.Rectificativa_CkB, 0);
            this.General_GB.Controls.SetChildIndex(this.Agrupada_CkB, 0);
            this.General_GB.Controls.SetChildIndex(this.IDManual_CKB, 0);
            this.General_GB.Controls.SetChildIndex(this.Transportista_TB, 0);
            this.General_GB.Controls.SetChildIndex(this.Transportista_BT, 0);
            this.General_GB.Controls.SetChildIndex(this.Usuario_TB, 0);
            this.General_GB.Controls.SetChildIndex(this.Usuario_BT, 0);
            this.General_GB.Controls.SetChildIndex(this.Serie_TB, 0);
            this.General_GB.Controls.SetChildIndex(this.Serie_BT, 0);
            this.General_GB.Controls.SetChildIndex(this.Almacen_TB, 0);
            this.General_GB.Controls.SetChildIndex(this.Expediente_TB, 0);
            this.General_GB.Controls.SetChildIndex(this.Expediente_BT, 0);
            this.General_GB.Controls.SetChildIndex(this.Almacen_BT, 0);
            // 
            // Fecha_DTP
            // 
            this.Fecha_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Fecha_DTP.Checked = true;
            this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Fecha_DTP.ValueChanged += new System.EventHandler(this.Fecha_DTP_ValueChanged);
            // 
            // NFactura_TB
            // 
            this.NFactura_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NFactura_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.NFactura_TB.ForeColor = System.Drawing.Color.Black;
            this.NFactura_TB.TabStop = false;
            // 
            // DescuentoC_NTB
            // 
            this.DescuentoC_NTB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.DescuentoC_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.DescuentoC_NTB.ForeColor = System.Drawing.Color.Black;
            this.DescuentoC_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Cliente_GB.Controls.SetChildIndex(this.DescuentoC_NTB, 0);
            this.Cliente_GB.Controls.SetChildIndex(this.VatNumber_TB, 0);
            this.Cliente_GB.Controls.SetChildIndex(this.IDCliente_TB, 0);
            this.Cliente_GB.Controls.SetChildIndex(this.nombreTextBox, 0);
            this.Cliente_GB.Controls.SetChildIndex(this.DiasPagoC_TB, 0);
            this.Cliente_GB.Controls.SetChildIndex(this.Cliente_BT, 0);
            this.Cliente_GB.Controls.SetChildIndex(this.FormaPagoC_TB, 0);
            this.Cliente_GB.Controls.SetChildIndex(this.MedioPagoC_TB, 0);
            this.Cliente_GB.Controls.SetChildIndex(this.textBox1, 0);
            this.Cliente_GB.Controls.SetChildIndex(this.textBox2, 0);
            this.Cliente_GB.Controls.SetChildIndex(this.numericTextBox3, 0);
            // 
            // FormaPagoC_TB
            // 
            this.FormaPagoC_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormaPagoC_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // Cliente_BT
            // 
            this.Cliente_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cliente_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // DiasPagoC_TB
            // 
            this.DiasPagoC_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.DiasPagoC_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.nombreTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // IDCliente_TB
            // 
            this.IDCliente_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.IDCliente_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.IDCliente_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // VatNumber_TB
            // 
            this.VatNumber_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.VatNumber_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.VatNumber_TB.ForeColor = System.Drawing.Color.Black;
            this.VatNumber_TB.Size = new System.Drawing.Size(72, 21);
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
            // MedioPagoC_TB
            // 
            this.MedioPagoC_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MedioPagoC_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // PDescuento_NTB
            // 
            this.PDescuento_NTB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PDescuento_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.PDescuento_NTB.ForeColor = System.Drawing.Color.Black;
            this.PDescuento_NTB.Size = new System.Drawing.Size(54, 21);
            this.PDescuento_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FormaPago_CB
            // 
            this.FormaPago_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormaPago_CB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.FormaPago_CB.SelectedIndexChanged += new System.EventHandler(this.FormaPago_CB_SelectedIndexChanged);
            // 
            // DiasPago_NTB
            // 
            this.DiasPago_NTB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.DiasPago_NTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.DiasPago_NTB.Validated += new System.EventHandler(this.DiasPago_NTB_TextChanged);
            // 
            // Prevision_TB
            // 
            this.Prevision_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Prevision_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Prevision_TB.ForeColor = System.Drawing.Color.Black;
            this.Prevision_TB.TabStop = false;
            this.Prevision_TB.TextChanged += new System.EventHandler(this.Prevision_TB_TextChanged);
            // 
            // Rectificativa_CkB
            // 
            this.Rectificativa_CkB.CheckedChanged += new System.EventHandler(this.Rectificativa_CkB_CheckedChanged);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F);
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
            // Cuenta_TB
            // 
            this.Cuenta_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Cuenta_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F);
            // 
            // PIRPF_NTB
            // 
            this.PIRPF_NTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.PIRPF_NTB.TextChanged += new System.EventHandler(this.PIRPF_NTB_TextChanged);
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F);
            // 
            // Albaranes_TB
            // 
            this.Albaranes_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Albaranes_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Albaranes_TB.ForeColor = System.Drawing.Color.Black;
            this.Albaranes_TB.TabStop = false;
            // 
            // SubmitPrint_BT
            // 
            this.SubmitPrint_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.SubmitPrint_BT.Location = new System.Drawing.Point(328, 2);
            this.HelpProvider.SetShowHelp(this.SubmitPrint_BT, true);
            this.SubmitPrint_BT.Click += new System.EventHandler(this.SubmitPrint_BT_Click);
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F);
            // 
            // EstadoCobro_TB
            // 
            this.EstadoCobro_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.EstadoCobro_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.EstadoCobro_TB.ForeColor = System.Drawing.Color.Black;
            this.EstadoCobro_TB.TabStop = false;
            // 
            // EstadoCobro_BT
            // 
            this.EstadoCobro_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.EstadoCobro_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.EstadoCobro_BT.Click += new System.EventHandler(this.EstadoCobro_BT_Click);
            // 
            // Expediente_BT
            // 
            this.Expediente_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Expediente_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // Expediente_TB
            // 
            this.Expediente_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Expediente_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Expediente_TB.ForeColor = System.Drawing.Color.Black;
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
            // Transportista_BT
            // 
            this.Transportista_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Transportista_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Transportista_BT.Click += new System.EventHandler(this.Transportista_BT_Click);
            // 
            // Transportista_TB
            // 
            this.Transportista_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Transportista_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Transportista_TB.ForeColor = System.Drawing.Color.Black;
            // 
            // Almacen_BT
            // 
            this.Almacen_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Almacen_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // numericTextBox3
            // 
            this.numericTextBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.numericTextBox3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.numericTextBox3.ForeColor = System.Drawing.Color.Black;
            this.numericTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.Submit_BT.Location = new System.Drawing.Point(62, 2);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(195, 2);
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
            this.Pie_Panel.SplitterDistance = 34;
            // 
            // Content_Panel
            // 
            this.Content_Panel.Size = new System.Drawing.Size(1192, 649);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(418, 114);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(1194, 692);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(560, 394);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(560, 309);
            // 
            // FacturaUIForm
            // 
            this.ClientSize = new System.Drawing.Size(1194, 692);
            this.HelpProvider.SetHelpKeyword(this, "60");
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "FacturaUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Shown += new System.EventHandler(this.FacturaUIForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Lines_BS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaymentMethod_BS)).EndInit();
            this.Impresion_GB.ResumeLayout(false);
            this.Impresion_GB.PerformLayout();
            this.General_GB.ResumeLayout(false);
            this.General_GB.PerformLayout();
            this.Cliente_GB.ResumeLayout(false);
            this.Cliente_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Client_BS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_FormaPago)).EndInit();
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
