namespace moleQule.Face.Invoice
{
    partial class BudgetForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label pDescuentoLabel;
            System.Windows.Forms.Label formaPagoLabel;
            System.Windows.Forms.Label diasPagoLabel;
            System.Windows.Forms.Label numeroClienteLabel;
            System.Windows.Forms.Label codigoLabel;
            System.Windows.Forms.Label nombreLabel;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label baseImponibleLabel;
            System.Windows.Forms.Label totalLabel;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label fechaLabel;
            System.Windows.Forms.Label serieLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BudgetForm));
            this.Datos_Transportista = new System.Windows.Forms.BindingSource(this.components);
            this.Datos_Concepto = new System.Windows.Forms.BindingSource(this.components);
            this.Datos_CobroProforma = new System.Windows.Forms.BindingSource(this.components);
            this.Datos_Expediente = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PDescuento_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.numericTextBox2 = new moleQule.Face.Controls.NumericTextBox();
            this.Descuento_TB = new moleQule.Face.Controls.NumericTextBox();
            this.numericTextBox1 = new moleQule.Face.Controls.NumericTextBox();
            this.Base_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Total_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Impresion_GB = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.Nota_TB = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Obs_TB = new System.Windows.Forms.TextBox();
            this.Nota_CkB = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Serie_BT = new System.Windows.Forms.Button();
            this.Serie_TB = new System.Windows.Forms.TextBox();
            this.NumeroSerie_TB = new System.Windows.Forms.TextBox();
            this.NAlbaranManual_CKB = new System.Windows.Forms.CheckBox();
            this.NProforma_TB = new System.Windows.Forms.TextBox();
            this.Fecha_DTP = new moleQule.Face.Controls.mQDateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MedioPagoC_TB = new System.Windows.Forms.TextBox();
            this.Datos_Cliente = new System.Windows.Forms.BindingSource(this.components);
            this.FormaPagoC_TB = new System.Windows.Forms.TextBox();
            this.Cliente_BT = new System.Windows.Forms.Button();
            this.DiasPagoC_TB = new System.Windows.Forms.TextBox();
            this.nombreTextBox = new System.Windows.Forms.TextBox();
            this.IDCliente_TB = new System.Windows.Forms.TextBox();
            this.VatNumber_TB = new System.Windows.Forms.TextBox();
            this.DescuentoC_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Conceptos_Panel = new System.Windows.Forms.SplitContainer();
            this.Conceptos_TS = new System.Windows.Forms.ToolStrip();
            this.Add_TI = new System.Windows.Forms.ToolStripButton();
            this.Edit_TI = new System.Windows.Forms.ToolStripButton();
            this.View_TI = new System.Windows.Forms.ToolStripButton();
            this.Delete_TI = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.Lineas_DGW = new System.Windows.Forms.DataGridView();
            this.LiConcepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiExpediente = new System.Windows.Forms.DataGridViewButtonColumn();
            this.LiFacturacionPeso = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LiPieces = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiKilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiPrecio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiPDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiPImpuestos = new System.Windows.Forms.DataGridViewButtonColumn();
            this.LiImpuestos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            pDescuentoLabel = new System.Windows.Forms.Label();
            formaPagoLabel = new System.Windows.Forms.Label();
            diasPagoLabel = new System.Windows.Forms.Label();
            numeroClienteLabel = new System.Windows.Forms.Label();
            codigoLabel = new System.Windows.Forms.Label();
            nombreLabel = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            baseImponibleLabel = new System.Windows.Forms.Label();
            totalLabel = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            fechaLabel = new System.Windows.Forms.Label();
            serieLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Paneles2)).BeginInit();
            this.Paneles2.Panel1.SuspendLayout();
            this.Paneles2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Transportista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Concepto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_CobroProforma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Expediente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.Impresion_GB.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Cliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Conceptos_Panel)).BeginInit();
            this.Conceptos_Panel.Panel1.SuspendLayout();
            this.Conceptos_Panel.Panel2.SuspendLayout();
            this.Conceptos_Panel.SuspendLayout();
            this.Conceptos_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lineas_DGW)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.Controls.Add(this.splitContainer1);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(1194, 760);
            this.PanelesV.SplitterDistance = 705;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(461, 7);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(550, 7);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Paneles2
            // 
            this.ErrorMng_EP.SetError(this.Paneles2, "F1 Ayuda        ");
            // 
            // Paneles2.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, true);
            // 
            // Paneles2.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Paneles2, true);
            this.Paneles2.Size = new System.Drawing.Size(1192, 52);
            this.Paneles2.SplitterDistance = 30;
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.AutoSize = true;
            this.Imprimir_Button.Enabled = true;
            this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Imprimir_Button.Location = new System.Drawing.Point(686, 6);
            this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
            this.Imprimir_Button.Visible = true;
            // 
            // Docs_BT
            // 
            this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Docs_BT.Location = new System.Drawing.Point(300, 6);
            this.HelpProvider.SetShowHelp(this.Docs_BT, true);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Invoice.Budget);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(418, 113);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(1194, 760);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(560, 428);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(560, 343);
            // 
            // pDescuentoLabel
            // 
            pDescuentoLabel.AutoSize = true;
            pDescuentoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pDescuentoLabel.Location = new System.Drawing.Point(15, 187);
            pDescuentoLabel.Name = "pDescuentoLabel";
            pDescuentoLabel.Size = new System.Drawing.Size(76, 13);
            pDescuentoLabel.TabIndex = 10;
            pDescuentoLabel.Text = "% Descuento:";
            // 
            // formaPagoLabel
            // 
            formaPagoLabel.AutoSize = true;
            formaPagoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            formaPagoLabel.Location = new System.Drawing.Point(23, 106);
            formaPagoLabel.Name = "formaPagoLabel";
            formaPagoLabel.Size = new System.Drawing.Size(68, 13);
            formaPagoLabel.TabIndex = 36;
            formaPagoLabel.Text = "Forma Pago:";
            // 
            // diasPagoLabel
            // 
            diasPagoLabel.AutoSize = true;
            diasPagoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            diasPagoLabel.Location = new System.Drawing.Point(33, 133);
            diasPagoLabel.Name = "diasPagoLabel";
            diasPagoLabel.Size = new System.Drawing.Size(58, 13);
            diasPagoLabel.TabIndex = 35;
            diasPagoLabel.Text = "Días Pago:";
            // 
            // numeroClienteLabel
            // 
            numeroClienteLabel.AutoSize = true;
            numeroClienteLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            numeroClienteLabel.Location = new System.Drawing.Point(32, 25);
            numeroClienteLabel.Name = "numeroClienteLabel";
            numeroClienteLabel.Size = new System.Drawing.Size(59, 13);
            numeroClienteLabel.TabIndex = 35;
            numeroClienteLabel.Text = "Nº Cliente:";
            // 
            // codigoLabel
            // 
            codigoLabel.AutoSize = true;
            codigoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            codigoLabel.Location = new System.Drawing.Point(35, 53);
            codigoLabel.Name = "codigoLabel";
            codigoLabel.Size = new System.Drawing.Size(56, 13);
            codigoLabel.TabIndex = 18;
            codigoLabel.Text = "DNI / CIF:";
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nombreLabel.Location = new System.Drawing.Point(43, 79);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new System.Drawing.Size(48, 13);
            nombreLabel.TabIndex = 24;
            nombreLabel.Text = "Nombre:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.Location = new System.Drawing.Point(25, 160);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(66, 13);
            label7.TabIndex = 38;
            label7.Text = "Medio Pago:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label10.Location = new System.Drawing.Point(246, 235);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(76, 13);
            label10.TabIndex = 41;
            label10.Text = "% Descuento:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label9.Location = new System.Drawing.Point(415, 235);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(62, 13);
            label9.TabIndex = 65;
            label9.Text = "Descuento:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.Location = new System.Drawing.Point(830, 235);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(33, 13);
            label8.TabIndex = 63;
            label8.Text = "IGIC:";
            // 
            // baseImponibleLabel
            // 
            baseImponibleLabel.AutoSize = true;
            baseImponibleLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            baseImponibleLabel.Location = new System.Drawing.Point(608, 235);
            baseImponibleLabel.Name = "baseImponibleLabel";
            baseImponibleLabel.Size = new System.Drawing.Size(83, 13);
            baseImponibleLabel.TabIndex = 62;
            baseImponibleLabel.Text = "Base Imponible:";
            // 
            // totalLabel
            // 
            totalLabel.AutoSize = true;
            totalLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            totalLabel.Location = new System.Drawing.Point(983, 235);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new System.Drawing.Size(35, 13);
            totalLabel.TabIndex = 61;
            totalLabel.Text = "Total:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label11.Location = new System.Drawing.Point(56, 235);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(51, 13);
            label11.TabIndex = 67;
            label11.Text = "Subtotal:";
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            fechaLabel.Location = new System.Drawing.Point(8, 45);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new System.Drawing.Size(40, 13);
            fechaLabel.TabIndex = 8;
            fechaLabel.Text = "Fecha:";
            // 
            // serieLabel
            // 
            serieLabel.AutoSize = true;
            serieLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            serieLabel.Location = new System.Drawing.Point(244, 80);
            serieLabel.Name = "serieLabel";
            serieLabel.Size = new System.Drawing.Size(46, 13);
            serieLabel.TabIndex = 78;
            serieLabel.Text = "Nº Serie";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(8, 80);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(31, 13);
            label1.TabIndex = 80;
            label1.Text = "Serie";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(336, 81);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 13);
            label2.TabIndex = 76;
            label2.Text = "Nº Proforma:";
            // 
            // Datos_Transportista
            // 
            this.Datos_Transportista.DataSource = typeof(moleQule.Library.Store.TransporterInfo);
            // 
            // Datos_Concepto
            // 
            this.Datos_Concepto.DataSource = typeof(moleQule.Library.Invoice.BudgetLine);
            // 
            // Datos_Expediente
            // 
            this.Datos_Expediente.DataSource = typeof(moleQule.Library.Store.ExpedientInfo);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(label11);
            this.splitContainer1.Panel1.Controls.Add(this.PDescuento_NTB);
            this.splitContainer1.Panel1.Controls.Add(label10);
            this.splitContainer1.Panel1.Controls.Add(this.numericTextBox2);
            this.splitContainer1.Panel1.Controls.Add(label9);
            this.splitContainer1.Panel1.Controls.Add(this.Descuento_TB);
            this.splitContainer1.Panel1.Controls.Add(label8);
            this.splitContainer1.Panel1.Controls.Add(this.numericTextBox1);
            this.splitContainer1.Panel1.Controls.Add(baseImponibleLabel);
            this.splitContainer1.Panel1.Controls.Add(this.Base_NTB);
            this.splitContainer1.Panel1.Controls.Add(totalLabel);
            this.splitContainer1.Panel1.Controls.Add(this.Total_NTB);
            this.splitContainer1.Panel1.Controls.Add(this.Impresion_GB);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Conceptos_Panel);
            this.splitContainer1.Size = new System.Drawing.Size(1192, 703);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.TabIndex = 0;
            // 
            // PDescuento_NTB
            // 
            this.PDescuento_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PDescuento", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.PDescuento_NTB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PDescuento_NTB.ForeColor = System.Drawing.Color.Navy;
            this.PDescuento_NTB.Location = new System.Drawing.Point(332, 230);
            this.PDescuento_NTB.Name = "PDescuento_NTB";
            this.PDescuento_NTB.Size = new System.Drawing.Size(54, 23);
            this.PDescuento_NTB.TabIndex = 40;
            this.PDescuento_NTB.TabStop = false;
            this.PDescuento_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PDescuento_NTB.TextIsCurrency = false;
            this.PDescuento_NTB.TextIsInteger = false;
            // 
            // numericTextBox2
            // 
            this.numericTextBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Subtotal", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.numericTextBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericTextBox2.Location = new System.Drawing.Point(113, 230);
            this.numericTextBox2.Name = "numericTextBox2";
            this.numericTextBox2.ReadOnly = true;
            this.numericTextBox2.Size = new System.Drawing.Size(105, 23);
            this.numericTextBox2.TabIndex = 66;
            this.numericTextBox2.TabStop = false;
            this.numericTextBox2.Tag = "NO FORMAT";
            this.numericTextBox2.Text = "0.00";
            this.numericTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextBox2.TextIsCurrency = false;
            this.numericTextBox2.TextIsInteger = false;
            // 
            // Descuento_TB
            // 
            this.Descuento_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Descuento", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Descuento_TB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descuento_TB.Location = new System.Drawing.Point(480, 230);
            this.Descuento_TB.Name = "Descuento_TB";
            this.Descuento_TB.ReadOnly = true;
            this.Descuento_TB.Size = new System.Drawing.Size(96, 23);
            this.Descuento_TB.TabIndex = 64;
            this.Descuento_TB.TabStop = false;
            this.Descuento_TB.Tag = "NO FORMAT";
            this.Descuento_TB.Text = "0.00";
            this.Descuento_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Descuento_TB.TextIsCurrency = false;
            this.Descuento_TB.TextIsInteger = false;
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Impuestos", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.numericTextBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericTextBox1.Location = new System.Drawing.Point(869, 230);
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.ReadOnly = true;
            this.numericTextBox1.Size = new System.Drawing.Size(82, 23);
            this.numericTextBox1.TabIndex = 59;
            this.numericTextBox1.TabStop = false;
            this.numericTextBox1.Tag = "NO FORMAT";
            this.numericTextBox1.Text = "0.00";
            this.numericTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextBox1.TextIsCurrency = false;
            this.numericTextBox1.TextIsInteger = false;
            // 
            // Base_NTB
            // 
            this.Base_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BaseImponible", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.Base_NTB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Base_NTB.Location = new System.Drawing.Point(697, 230);
            this.Base_NTB.Name = "Base_NTB";
            this.Base_NTB.ReadOnly = true;
            this.Base_NTB.Size = new System.Drawing.Size(105, 23);
            this.Base_NTB.TabIndex = 58;
            this.Base_NTB.TabStop = false;
            this.Base_NTB.Tag = "NO FORMAT";
            this.Base_NTB.Text = "0.00";
            this.Base_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Base_NTB.TextIsCurrency = false;
            this.Base_NTB.TextIsInteger = false;
            // 
            // Total_NTB
            // 
            this.Total_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Total", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Total_NTB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Total_NTB.Location = new System.Drawing.Point(1028, 230);
            this.Total_NTB.Name = "Total_NTB";
            this.Total_NTB.ReadOnly = true;
            this.Total_NTB.Size = new System.Drawing.Size(108, 23);
            this.Total_NTB.TabIndex = 60;
            this.Total_NTB.TabStop = false;
            this.Total_NTB.Tag = "NO FORMAT";
            this.Total_NTB.Text = "0.00";
            this.Total_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Total_NTB.TextIsCurrency = false;
            this.Total_NTB.TextIsInteger = false;
            // 
            // Impresion_GB
            // 
            this.Impresion_GB.Controls.Add(this.label15);
            this.Impresion_GB.Controls.Add(this.Nota_TB);
            this.Impresion_GB.Controls.Add(this.label14);
            this.Impresion_GB.Controls.Add(this.Obs_TB);
            this.Impresion_GB.Controls.Add(this.Nota_CkB);
            this.Impresion_GB.Location = new System.Drawing.Point(765, 4);
            this.Impresion_GB.Name = "Impresion_GB";
            this.Impresion_GB.Size = new System.Drawing.Size(420, 220);
            this.Impresion_GB.TabIndex = 57;
            this.Impresion_GB.TabStop = false;
            this.Impresion_GB.Text = "Otros Datos";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(57, 134);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 55;
            this.label15.Text = "Nota:";
            // 
            // Nota_TB
            // 
            this.Nota_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nota_TB.Location = new System.Drawing.Point(97, 131);
            this.Nota_TB.Multiline = true;
            this.Nota_TB.Name = "Nota_TB";
            this.Nota_TB.Size = new System.Drawing.Size(314, 36);
            this.Nota_TB.TabIndex = 54;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(9, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 53;
            this.label14.Text = "Observaciones:";
            // 
            // Obs_TB
            // 
            this.Obs_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.Obs_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Obs_TB.Location = new System.Drawing.Point(97, 26);
            this.Obs_TB.Multiline = true;
            this.Obs_TB.Name = "Obs_TB";
            this.Obs_TB.Size = new System.Drawing.Size(314, 89);
            this.Obs_TB.TabIndex = 52;
            // 
            // Nota_CkB
            // 
            this.Nota_CkB.AutoSize = true;
            this.Nota_CkB.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.Datos, "Nota", true));
            this.Nota_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nota_CkB.Location = new System.Drawing.Point(201, 177);
            this.Nota_CkB.Name = "Nota_CkB";
            this.Nota_CkB.Size = new System.Drawing.Size(89, 17);
            this.Nota_CkB.TabIndex = 0;
            this.Nota_CkB.Text = "Mostrar Nota";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Serie_BT);
            this.groupBox3.Controls.Add(this.Serie_TB);
            this.groupBox3.Controls.Add(this.NumeroSerie_TB);
            this.groupBox3.Controls.Add(serieLabel);
            this.groupBox3.Controls.Add(label1);
            this.groupBox3.Controls.Add(this.NAlbaranManual_CKB);
            this.groupBox3.Controls.Add(label2);
            this.groupBox3.Controls.Add(this.NProforma_TB);
            this.groupBox3.Controls.Add(this.Fecha_DTP);
            this.groupBox3.Controls.Add(fechaLabel);
            this.groupBox3.Location = new System.Drawing.Point(7, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(429, 220);
            this.groupBox3.TabIndex = 55;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos de la Proforma";
            // 
            // Serie_BT
            // 
            this.Serie_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Serie_BT.Location = new System.Drawing.Point(301, 98);
            this.Serie_BT.Name = "Serie_BT";
            this.Serie_BT.Size = new System.Drawing.Size(28, 21);
            this.Serie_BT.TabIndex = 82;
            this.Serie_BT.UseVisualStyleBackColor = true;
            // 
            // Serie_TB
            // 
            this.Serie_TB.Enabled = false;
            this.Serie_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Serie_TB.Location = new System.Drawing.Point(8, 98);
            this.Serie_TB.Name = "Serie_TB";
            this.Serie_TB.ReadOnly = true;
            this.Serie_TB.Size = new System.Drawing.Size(230, 21);
            this.Serie_TB.TabIndex = 81;
            this.Serie_TB.TabStop = false;
            // 
            // NumeroSerie_TB
            // 
            this.NumeroSerie_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "NumeroSerie", true));
            this.NumeroSerie_TB.Location = new System.Drawing.Point(244, 98);
            this.NumeroSerie_TB.Name = "NumeroSerie_TB";
            this.NumeroSerie_TB.ReadOnly = true;
            this.NumeroSerie_TB.Size = new System.Drawing.Size(51, 21);
            this.NumeroSerie_TB.TabIndex = 79;
            this.NumeroSerie_TB.TabStop = false;
            this.NumeroSerie_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NAlbaranManual_CKB
            // 
            this.NAlbaranManual_CKB.AutoSize = true;
            this.NAlbaranManual_CKB.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.Datos, "NManual", true));
            this.NAlbaranManual_CKB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NAlbaranManual_CKB.Location = new System.Drawing.Point(348, 125);
            this.NAlbaranManual_CKB.Name = "NAlbaranManual_CKB";
            this.NAlbaranManual_CKB.Size = new System.Drawing.Size(60, 17);
            this.NAlbaranManual_CKB.TabIndex = 77;
            this.NAlbaranManual_CKB.Text = "Manual";
            this.NAlbaranManual_CKB.UseVisualStyleBackColor = true;
            this.NAlbaranManual_CKB.CheckedChanged += new System.EventHandler(this.NProformaManual_CKB_CheckedChanged);
            // 
            // NProforma_TB
            // 
            this.NProforma_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
            this.NProforma_TB.Location = new System.Drawing.Point(336, 98);
            this.NProforma_TB.Name = "NProforma_TB";
            this.NProforma_TB.ReadOnly = true;
            this.NProforma_TB.Size = new System.Drawing.Size(84, 21);
            this.NProforma_TB.TabIndex = 75;
            this.NProforma_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Fecha_DTP
            // 
            this.Fecha_DTP.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fecha_DTP.CalendarTitleForeColor = System.Drawing.Color.Navy;
            this.Fecha_DTP.Checked = false;
            this.Fecha_DTP.CustomFormat = "dd/MM/yyyy HH:mm";
            this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fecha_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Fecha_DTP.Location = new System.Drawing.Point(54, 41);
            this.Fecha_DTP.Name = "Fecha_DTP";
            this.Fecha_DTP.ShowCheckBox = true;
            this.Fecha_DTP.Size = new System.Drawing.Size(143, 21);
            this.Fecha_DTP.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(label7);
            this.groupBox1.Controls.Add(this.MedioPagoC_TB);
            this.groupBox1.Controls.Add(formaPagoLabel);
            this.groupBox1.Controls.Add(this.FormaPagoC_TB);
            this.groupBox1.Controls.Add(this.Cliente_BT);
            this.groupBox1.Controls.Add(diasPagoLabel);
            this.groupBox1.Controls.Add(this.DiasPagoC_TB);
            this.groupBox1.Controls.Add(numeroClienteLabel);
            this.groupBox1.Controls.Add(this.nombreTextBox);
            this.groupBox1.Controls.Add(this.IDCliente_TB);
            this.groupBox1.Controls.Add(this.VatNumber_TB);
            this.groupBox1.Controls.Add(codigoLabel);
            this.groupBox1.Controls.Add(nombreLabel);
            this.groupBox1.Controls.Add(this.DescuentoC_NTB);
            this.groupBox1.Controls.Add(pDescuentoLabel);
            this.groupBox1.Location = new System.Drawing.Point(442, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 220);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Cliente";
            // 
            // MedioPagoC_TB
            // 
            this.MedioPagoC_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Cliente, "MedioPagoLabel", true));
            this.MedioPagoC_TB.Location = new System.Drawing.Point(101, 157);
            this.MedioPagoC_TB.Name = "MedioPagoC_TB";
            this.MedioPagoC_TB.ReadOnly = true;
            this.MedioPagoC_TB.Size = new System.Drawing.Size(202, 21);
            this.MedioPagoC_TB.TabIndex = 39;
            this.MedioPagoC_TB.TabStop = false;
            // 
            // Datos_Cliente
            // 
            this.Datos_Cliente.DataSource = typeof(moleQule.Library.Invoice.ClienteInfo);
            // 
            // FormaPagoC_TB
            // 
            this.FormaPagoC_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Cliente, "FormaPagoLabel", true));
            this.FormaPagoC_TB.Location = new System.Drawing.Point(101, 103);
            this.FormaPagoC_TB.Name = "FormaPagoC_TB";
            this.FormaPagoC_TB.ReadOnly = true;
            this.FormaPagoC_TB.Size = new System.Drawing.Size(202, 21);
            this.FormaPagoC_TB.TabIndex = 37;
            this.FormaPagoC_TB.TabStop = false;
            // 
            // Cliente_BT
            // 
            this.Cliente_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Cliente_BT.Location = new System.Drawing.Point(196, 22);
            this.Cliente_BT.Name = "Cliente_BT";
            this.Cliente_BT.Size = new System.Drawing.Size(34, 21);
            this.Cliente_BT.TabIndex = 0;
            this.Cliente_BT.UseVisualStyleBackColor = true;
            this.Cliente_BT.Click += new System.EventHandler(this.Cliente_BT_Click);
            // 
            // DiasPagoC_TB
            // 
            this.DiasPagoC_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Cliente, "DiasPago", true));
            this.DiasPagoC_TB.Location = new System.Drawing.Point(101, 130);
            this.DiasPagoC_TB.Name = "DiasPagoC_TB";
            this.DiasPagoC_TB.ReadOnly = true;
            this.DiasPagoC_TB.Size = new System.Drawing.Size(51, 21);
            this.DiasPagoC_TB.TabIndex = 36;
            this.DiasPagoC_TB.TabStop = false;
            this.DiasPagoC_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Cliente, "Nombre", true));
            this.nombreTextBox.Location = new System.Drawing.Point(101, 76);
            this.nombreTextBox.Name = "nombreTextBox";
            this.nombreTextBox.ReadOnly = true;
            this.nombreTextBox.Size = new System.Drawing.Size(202, 21);
            this.nombreTextBox.TabIndex = 1;
            this.nombreTextBox.TabStop = false;
            // 
            // IDCliente_TB
            // 
            this.IDCliente_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Cliente, "Codigo", true));
            this.IDCliente_TB.Location = new System.Drawing.Point(101, 22);
            this.IDCliente_TB.Name = "IDCliente_TB";
            this.IDCliente_TB.ReadOnly = true;
            this.IDCliente_TB.Size = new System.Drawing.Size(89, 21);
            this.IDCliente_TB.TabIndex = 3;
            this.IDCliente_TB.TabStop = false;
            this.IDCliente_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // VatNumber_TB
            // 
            this.VatNumber_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Cliente, "VatNumber", true));
            this.VatNumber_TB.Location = new System.Drawing.Point(101, 49);
            this.VatNumber_TB.Name = "VatNumber_TB";
            this.VatNumber_TB.ReadOnly = true;
            this.VatNumber_TB.Size = new System.Drawing.Size(89, 21);
            this.VatNumber_TB.TabIndex = 0;
            this.VatNumber_TB.TabStop = false;
            this.VatNumber_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // DescuentoC_NTB
            // 
            this.DescuentoC_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Cliente, "PDescuento", true));
            this.DescuentoC_NTB.Location = new System.Drawing.Point(101, 184);
            this.DescuentoC_NTB.Name = "DescuentoC_NTB";
            this.DescuentoC_NTB.ReadOnly = true;
            this.DescuentoC_NTB.Size = new System.Drawing.Size(51, 21);
            this.DescuentoC_NTB.TabIndex = 2;
            this.DescuentoC_NTB.TabStop = false;
            this.DescuentoC_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DescuentoC_NTB.TextIsCurrency = false;
            this.DescuentoC_NTB.TextIsInteger = false;
            // 
            // Conceptos_Panel
            // 
            this.Conceptos_Panel.BackColor = System.Drawing.Color.Gainsboro;
            this.Conceptos_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Conceptos_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Conceptos_Panel.Location = new System.Drawing.Point(0, 0);
            this.Conceptos_Panel.Name = "Conceptos_Panel";
            this.Conceptos_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Conceptos_Panel.Panel1
            // 
            this.Conceptos_Panel.Panel1.Controls.Add(this.Conceptos_TS);
            this.Conceptos_Panel.Panel1MinSize = 36;
            // 
            // Conceptos_Panel.Panel2
            // 
            this.Conceptos_Panel.Panel2.Controls.Add(this.Lineas_DGW);
            this.Conceptos_Panel.Panel2MinSize = 40;
            this.Conceptos_Panel.Size = new System.Drawing.Size(1192, 439);
            this.Conceptos_Panel.SplitterDistance = 36;
            this.Conceptos_Panel.SplitterWidth = 1;
            this.Conceptos_Panel.TabIndex = 1;
            // 
            // Conceptos_TS
            // 
            this.Conceptos_TS.BackColor = System.Drawing.Color.Gainsboro;
            this.Conceptos_TS.GripMargin = new System.Windows.Forms.Padding(0);
            this.Conceptos_TS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Conceptos_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Conceptos_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_TI,
            this.Edit_TI,
            this.View_TI,
            this.Delete_TI,
            this.toolStripLabel1});
            this.Conceptos_TS.Location = new System.Drawing.Point(0, 0);
            this.Conceptos_TS.Name = "Conceptos_TS";
            this.HelpProvider.SetShowHelp(this.Conceptos_TS, true);
            this.Conceptos_TS.Size = new System.Drawing.Size(1192, 39);
            this.Conceptos_TS.TabIndex = 5;
            // 
            // Add_TI
            // 
            this.Add_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Add_TI.Image = global::moleQule.Face.Invoice.Properties.Resources.item_add;
            this.Add_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Add_TI.Name = "Add_TI";
            this.Add_TI.Size = new System.Drawing.Size(36, 36);
            this.Add_TI.Text = "Nuevo Concepto (sin control de stock)";
            this.Add_TI.Click += new System.EventHandler(this.ConceptoLibre_BT_Click);
            // 
            // Edit_TI
            // 
            this.Edit_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Edit_TI.Image = global::moleQule.Face.Invoice.Properties.Resources.item_edit;
            this.Edit_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Edit_TI.Name = "Edit_TI";
            this.Edit_TI.Size = new System.Drawing.Size(36, 36);
            this.Edit_TI.Text = "Editar Concepto";
            this.Edit_TI.Click += new System.EventHandler(this.Editar_Concepto_BT_Click);
            // 
            // View_TI
            // 
            this.View_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.View_TI.Image = global::moleQule.Face.Invoice.Properties.Resources.item_view;
            this.View_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.View_TI.Name = "View_TI";
            this.View_TI.Size = new System.Drawing.Size(36, 36);
            this.View_TI.Text = "Ver";
            this.View_TI.Visible = false;
            // 
            // Delete_TI
            // 
            this.Delete_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Delete_TI.Image = global::moleQule.Face.Invoice.Properties.Resources.item_delete;
            this.Delete_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Delete_TI.Name = "Delete_TI";
            this.Delete_TI.Size = new System.Drawing.Size(36, 36);
            this.Delete_TI.Text = "Borrar Concepto";
            this.Delete_TI.Click += new System.EventHandler(this.Eliminar_Concepto_BT_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 36);
            // 
            // Lineas_DGW
            // 
            this.Lineas_DGW.AllowUserToAddRows = false;
            this.Lineas_DGW.AllowUserToDeleteRows = false;
            this.Lineas_DGW.AllowUserToOrderColumns = true;
            this.Lineas_DGW.AutoGenerateColumns = false;
            this.Lineas_DGW.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Lineas_DGW.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Lineas_DGW.ColumnHeadersHeight = 35;
            this.Lineas_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LiConcepto,
            this.LiExpediente,
            this.LiFacturacionPeso,
            this.LiPieces,
            this.LiKilos,
            this.LiPrecio,
            this.LiPDescuento,
            this.LiDescuento,
            this.LiPImpuestos,
            this.LiImpuestos,
            this.LiTotal});
            this.Lineas_DGW.DataSource = this.Datos_Concepto;
            this.Lineas_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lineas_DGW.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.Lineas_DGW.Location = new System.Drawing.Point(0, 0);
            this.Lineas_DGW.MultiSelect = false;
            this.Lineas_DGW.Name = "Lineas_DGW";
            this.Lineas_DGW.RowHeadersWidth = 25;
            this.Lineas_DGW.Size = new System.Drawing.Size(1192, 402);
            this.Lineas_DGW.TabIndex = 62;
            this.Lineas_DGW.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Conceptos_DGW_CellContentClick);
            // 
            // LiConcepto
            // 
            this.LiConcepto.DataPropertyName = "Concepto";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LiConcepto.DefaultCellStyle = dataGridViewCellStyle1;
            this.LiConcepto.HeaderText = "Concepto";
            this.LiConcepto.Name = "LiConcepto";
            this.LiConcepto.Width = 200;
            // 
            // LiExpediente
            // 
            this.LiExpediente.DataPropertyName = "Expediente";
            this.LiExpediente.HeaderText = "Expediente";
            this.LiExpediente.Name = "LiExpediente";
            this.LiExpediente.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LiExpediente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // LiFacturacionPeso
            // 
            this.LiFacturacionPeso.DataPropertyName = "FacturacionPeso";
            this.LiFacturacionPeso.HeaderText = "Fac. Peso";
            this.LiFacturacionPeso.Name = "LiFacturacionPeso";
            this.LiFacturacionPeso.Width = 35;
            // 
            // LiPieces
            // 
            this.LiPieces.DataPropertyName = "CantidadBultos";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N4";
            dataGridViewCellStyle2.NullValue = null;
            this.LiPieces.DefaultCellStyle = dataGridViewCellStyle2;
            this.LiPieces.HeaderText = "Unidades";
            this.LiPieces.Name = "LiPieces";
            this.LiPieces.Width = 75;
            // 
            // LiKilos
            // 
            this.LiKilos.DataPropertyName = "CantidadKilos";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.LiKilos.DefaultCellStyle = dataGridViewCellStyle3;
            this.LiKilos.HeaderText = "Kg";
            this.LiKilos.Name = "LiKilos";
            this.LiKilos.Width = 80;
            // 
            // LiPrecio
            // 
            this.LiPrecio.DataPropertyName = "Precio";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "C5";
            dataGridViewCellStyle4.NullValue = null;
            this.LiPrecio.DefaultCellStyle = dataGridViewCellStyle4;
            this.LiPrecio.HeaderText = "Precio";
            this.LiPrecio.Name = "LiPrecio";
            this.LiPrecio.Width = 75;
            // 
            // LiPDescuento
            // 
            this.LiPDescuento.DataPropertyName = "PDescuento";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            this.LiPDescuento.DefaultCellStyle = dataGridViewCellStyle5;
            this.LiPDescuento.HeaderText = "% Dto.";
            this.LiPDescuento.Name = "LiPDescuento";
            this.LiPDescuento.Width = 40;
            // 
            // LiDescuento
            // 
            this.LiDescuento.DataPropertyName = "Descuento";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "C2";
            this.LiDescuento.DefaultCellStyle = dataGridViewCellStyle6;
            this.LiDescuento.HeaderText = "Descuento";
            this.LiDescuento.Name = "LiDescuento";
            this.LiDescuento.ReadOnly = true;
            this.LiDescuento.Width = 75;
            // 
            // LiPImpuestos
            // 
            this.LiPImpuestos.DataPropertyName = "PImpuestos";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            this.LiPImpuestos.DefaultCellStyle = dataGridViewCellStyle7;
            this.LiPImpuestos.HeaderText = "% Imp.";
            this.LiPImpuestos.Name = "LiPImpuestos";
            this.LiPImpuestos.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LiPImpuestos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.LiPImpuestos.Width = 40;
            // 
            // LiImpuestos
            // 
            this.LiImpuestos.DataPropertyName = "Impuestos";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "C2";
            this.LiImpuestos.DefaultCellStyle = dataGridViewCellStyle8;
            this.LiImpuestos.HeaderText = "Impuestos";
            this.LiImpuestos.Name = "LiImpuestos";
            this.LiImpuestos.ReadOnly = true;
            this.LiImpuestos.Width = 75;
            // 
            // LiTotal
            // 
            this.LiTotal.DataPropertyName = "Total";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "C2";
            dataGridViewCellStyle9.NullValue = null;
            this.LiTotal.DefaultCellStyle = dataGridViewCellStyle9;
            this.LiTotal.HeaderText = "Total";
            this.LiTotal.Name = "LiTotal";
            this.LiTotal.ReadOnly = true;
            this.LiTotal.Width = 75;
            // 
            // BudgetForm
            // 
            this.ClientSize = new System.Drawing.Size(1194, 760);
            this.HelpProvider.SetHelpKeyword(this, "60");
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BudgetForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Factura Proforma";
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            this.Paneles2.Panel1.ResumeLayout(false);
            this.Paneles2.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Paneles2)).EndInit();
            this.Paneles2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            this.ProgressBK_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Transportista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Concepto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_CobroProforma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Expediente)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.Impresion_GB.ResumeLayout(false);
            this.Impresion_GB.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Cliente)).EndInit();
            this.Conceptos_Panel.Panel1.ResumeLayout(false);
            this.Conceptos_Panel.Panel1.PerformLayout();
            this.Conceptos_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Conceptos_Panel)).EndInit();
            this.Conceptos_Panel.ResumeLayout(false);
            this.Conceptos_TS.ResumeLayout(false);
            this.Conceptos_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lineas_DGW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.DataGridViewTextBoxColumn oidProformaDataGridViewTextBoxColumn;
		protected System.Windows.Forms.BindingSource Datos_Transportista;
        protected System.Windows.Forms.BindingSource Datos_Concepto;
        protected System.Windows.Forms.BindingSource Datos_Expediente;
        protected System.Windows.Forms.BindingSource Datos_CobroProforma;
        protected System.Windows.Forms.DataGridViewCheckBoxColumn ConceptosBulto;
        private System.Windows.Forms.SplitContainer splitContainer1;
        protected System.Windows.Forms.GroupBox Impresion_GB;
        protected System.Windows.Forms.CheckBox Nota_CkB;
        protected moleQule.Face.Controls.NumericTextBox DescuentoC_NTB;
        protected System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.TextBox FormaPagoC_TB;
        protected System.Windows.Forms.Button Cliente_BT;
        protected System.Windows.Forms.TextBox DiasPagoC_TB;
        protected System.Windows.Forms.TextBox nombreTextBox;
        protected System.Windows.Forms.TextBox IDCliente_TB;
        protected System.Windows.Forms.TextBox VatNumber_TB;
        protected System.Windows.Forms.TextBox MedioPagoC_TB;
        protected System.Windows.Forms.BindingSource Datos_Cliente;
        protected moleQule.Face.Controls.NumericTextBox PDescuento_NTB;
        protected moleQule.Face.Controls.NumericTextBox Descuento_TB;
        protected moleQule.Face.Controls.NumericTextBox numericTextBox1;
        protected moleQule.Face.Controls.NumericTextBox Base_NTB;
        protected moleQule.Face.Controls.NumericTextBox Total_NTB;
		protected moleQule.Face.Controls.NumericTextBox numericTextBox2;
		protected System.Windows.Forms.GroupBox groupBox3;
		protected moleQule.Face.Controls.mQDateTimePicker Fecha_DTP;
        protected System.Windows.Forms.Label label14;
        protected System.Windows.Forms.TextBox Obs_TB;
        protected System.Windows.Forms.Label label15;
		protected System.Windows.Forms.TextBox Nota_TB;
		protected System.Windows.Forms.Button Serie_BT;
		protected System.Windows.Forms.TextBox Serie_TB;
		protected System.Windows.Forms.TextBox NumeroSerie_TB;
		protected System.Windows.Forms.CheckBox NAlbaranManual_CKB;
		protected System.Windows.Forms.TextBox NProforma_TB;
		private System.Windows.Forms.SplitContainer Conceptos_Panel;
		protected System.Windows.Forms.ToolStrip Conceptos_TS;
		protected System.Windows.Forms.ToolStripButton Add_TI;
		protected System.Windows.Forms.ToolStripButton Edit_TI;
		protected System.Windows.Forms.ToolStripButton View_TI;
		protected System.Windows.Forms.ToolStripButton Delete_TI;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        protected System.Windows.Forms.DataGridView Lineas_DGW;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiConcepto;
        private System.Windows.Forms.DataGridViewButtonColumn LiExpediente;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LiFacturacionPeso;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiPieces;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiKilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiPDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiDescuento;
        private System.Windows.Forms.DataGridViewButtonColumn LiPImpuestos;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiImpuestos;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiTotal;

    }
}
