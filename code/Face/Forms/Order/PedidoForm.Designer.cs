namespace moleQule.Face.Invoice
{
    partial class PedidoForm
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
            System.Windows.Forms.Label numeroClienteLabel;
            System.Windows.Forms.Label codigoLabel;
            System.Windows.Forms.Label nombreLabel;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label fechaLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label baseImponibleLabel;
            System.Windows.Forms.Label totalLabel;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label pDescuentoLabel;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PedidoForm));
            this.Main_Panel = new System.Windows.Forms.SplitContainer();
            this.PDescuento_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.numericTextBox2 = new moleQule.Face.Controls.NumericTextBox();
            this.Descuento_TB = new moleQule.Face.Controls.NumericTextBox();
            this.numericTextBox1 = new moleQule.Face.Controls.NumericTextBox();
            this.Base_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Total_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Cliente_GB = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Datos_Cliente = new System.Windows.Forms.BindingSource(this.components);
            this.DescuentoC_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Cliente_BT = new System.Windows.Forms.Button();
            this.Cliente_TB = new System.Windows.Forms.TextBox();
            this.IDCliente_TB = new System.Windows.Forms.TextBox();
            this.VatNumberTB = new System.Windows.Forms.TextBox();
            this.Impresion_GB = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Observaciones_TB = new System.Windows.Forms.TextBox();
            this.Estado_TB = new System.Windows.Forms.TextBox();
            this.Estado_BT = new System.Windows.Forms.Button();
            this.General_GB = new System.Windows.Forms.GroupBox();
            this.Expediente_BT = new System.Windows.Forms.Button();
            this.Expediente_TB = new System.Windows.Forms.TextBox();
            this.Almacen_BT = new System.Windows.Forms.Button();
            this.Almacen_TB = new System.Windows.Forms.TextBox();
            this.Serie_BT = new System.Windows.Forms.Button();
            this.Serie_TB = new System.Windows.Forms.TextBox();
            this.Usuario_BT = new System.Windows.Forms.Button();
            this.Usuario_TB = new System.Windows.Forms.TextBox();
            this.Fecha_DTP = new System.Windows.Forms.DateTimePicker();
            this.IDManual_CkB = new System.Windows.Forms.CheckBox();
            this.IDPedido_TB = new System.Windows.Forms.MaskedTextBox();
            this.Conceptos_Panel = new System.Windows.Forms.SplitContainer();
            this.Conceptos_TS = new System.Windows.Forms.ToolStrip();
            this.AddConcepto_TI = new System.Windows.Forms.ToolStripButton();
            this.AddStock_TI = new System.Windows.Forms.ToolStripButton();
            this.Edit_TI = new System.Windows.Forms.ToolStripButton();
            this.View_TI = new System.Windows.Forms.ToolStripButton();
            this.Delete_TI = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.Lineas_DGW = new System.Windows.Forms.DataGridView();
            this.Datos_Lineas = new System.Windows.Forms.BindingSource(this.components);
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expediente = new System.Windows.Forms.DataGridViewButtonColumn();
            this.FacturacionPeso = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LiPieces = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LiKilos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PendienteBultos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PImpuestos = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Impuestos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            numeroClienteLabel = new System.Windows.Forms.Label();
            codigoLabel = new System.Windows.Forms.Label();
            nombreLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            fechaLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            baseImponibleLabel = new System.Windows.Forms.Label();
            totalLabel = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            pDescuentoLabel = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pie_Panel)).BeginInit();
            this.Pie_Panel.Panel1.SuspendLayout();
            this.Pie_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Content_Panel)).BeginInit();
            this.Content_Panel.Panel2.SuspendLayout();
            this.Content_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_Panel)).BeginInit();
            this.Main_Panel.Panel1.SuspendLayout();
            this.Main_Panel.Panel2.SuspendLayout();
            this.Main_Panel.SuspendLayout();
            this.Cliente_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Cliente)).BeginInit();
            this.Impresion_GB.SuspendLayout();
            this.General_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Conceptos_Panel)).BeginInit();
            this.Conceptos_Panel.Panel1.SuspendLayout();
            this.Conceptos_Panel.Panel2.SuspendLayout();
            this.Conceptos_Panel.SuspendLayout();
            this.Conceptos_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lineas_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).BeginInit();
            this.SuspendLayout();
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
            this.PanelesV.Size = new System.Drawing.Size(1194, 722);
            this.PanelesV.SplitterDistance = 682;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Submit_BT.Location = new System.Drawing.Point(153, 2);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Cancel_BT.Location = new System.Drawing.Point(286, 2);
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
            this.Pie_Panel.Size = new System.Drawing.Size(1192, 37);
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
            this.Content_Panel.Panel2.Controls.Add(this.Main_Panel);
            this.HelpProvider.SetShowHelp(this.Content_Panel.Panel2, true);
            this.HelpProvider.SetShowHelp(this.Content_Panel, true);
            this.Content_Panel.Size = new System.Drawing.Size(1192, 680);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Invoice.Pedido);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(418, 103);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(1194, 722);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(560, 409);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(560, 324);
            // 
            // numeroClienteLabel
            // 
            numeroClienteLabel.AutoSize = true;
            numeroClienteLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            numeroClienteLabel.Location = new System.Drawing.Point(15, 23);
            numeroClienteLabel.Name = "numeroClienteLabel";
            numeroClienteLabel.Size = new System.Drawing.Size(50, 13);
            numeroClienteLabel.TabIndex = 35;
            numeroClienteLabel.Text = "Código*:";
            // 
            // codigoLabel
            // 
            codigoLabel.AutoSize = true;
            codigoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            codigoLabel.Location = new System.Drawing.Point(9, 50);
            codigoLabel.Name = "codigoLabel";
            codigoLabel.Size = new System.Drawing.Size(56, 13);
            codigoLabel.TabIndex = 18;
            codigoLabel.Text = "DNI / CIF:";
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nombreLabel.Location = new System.Drawing.Point(17, 76);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new System.Drawing.Size(48, 13);
            nombreLabel.TabIndex = 24;
            nombreLabel.Text = "Nombre:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(256, 30);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(22, 13);
            label2.TabIndex = 9;
            label2.Text = "ID:";
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            fechaLabel.Location = new System.Drawing.Point(9, 30);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new System.Drawing.Size(40, 13);
            fechaLabel.TabIndex = 8;
            fechaLabel.Text = "Fecha:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(10, 128);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(44, 13);
            label1.TabIndex = 81;
            label1.Text = "Estado:";
            label1.Visible = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label11.Location = new System.Drawing.Point(63, 194);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(51, 13);
            label11.TabIndex = 85;
            label11.Text = "Subtotal:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label10.Location = new System.Drawing.Point(246, 194);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(76, 13);
            label10.TabIndex = 75;
            label10.Text = "% Descuento:";
            label10.Visible = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label9.Location = new System.Drawing.Point(407, 194);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(62, 13);
            label9.TabIndex = 83;
            label9.Text = "Descuento:";
            label9.Visible = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.Location = new System.Drawing.Point(805, 194);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(61, 13);
            label8.TabIndex = 81;
            label8.Text = "Impuestos:";
            // 
            // baseImponibleLabel
            // 
            baseImponibleLabel.AutoSize = true;
            baseImponibleLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            baseImponibleLabel.Location = new System.Drawing.Point(589, 194);
            baseImponibleLabel.Name = "baseImponibleLabel";
            baseImponibleLabel.Size = new System.Drawing.Size(83, 13);
            baseImponibleLabel.TabIndex = 80;
            baseImponibleLabel.Text = "Base Imponible:";
            // 
            // totalLabel
            // 
            totalLabel.AutoSize = true;
            totalLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            totalLabel.Location = new System.Drawing.Point(977, 194);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new System.Drawing.Size(39, 13);
            totalLabel.TabIndex = 79;
            totalLabel.Text = "Total:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(7, 104);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(61, 13);
            label5.TabIndex = 45;
            label5.Text = "Impuestos:";
            // 
            // pDescuentoLabel
            // 
            pDescuentoLabel.AutoSize = true;
            pDescuentoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pDescuentoLabel.Location = new System.Drawing.Point(207, 104);
            pDescuentoLabel.Name = "pDescuentoLabel";
            pDescuentoLabel.Size = new System.Drawing.Size(50, 13);
            pDescuentoLabel.TabIndex = 44;
            pDescuentoLabel.Text = "Dto (%):";
            pDescuentoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.Location = new System.Drawing.Point(9, 131);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(65, 13);
            label7.TabIndex = 112;
            label7.Text = "Expediente:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(9, 104);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(51, 13);
            label3.TabIndex = 109;
            label3.Text = "Almacén:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(9, 54);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(35, 13);
            label4.TabIndex = 104;
            label4.Text = "Serie:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label6.Location = new System.Drawing.Point(9, 78);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(47, 13);
            label6.TabIndex = 103;
            label6.Text = "Usuario:";
            // 
            // Main_Panel
            // 
            this.Main_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Main_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Main_Panel.IsSplitterFixed = true;
            this.Main_Panel.Location = new System.Drawing.Point(0, 0);
            this.Main_Panel.Name = "Main_Panel";
            this.Main_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Main_Panel.Panel1
            // 
            this.Main_Panel.Panel1.Controls.Add(label11);
            this.Main_Panel.Panel1.Controls.Add(this.PDescuento_NTB);
            this.Main_Panel.Panel1.Controls.Add(label10);
            this.Main_Panel.Panel1.Controls.Add(this.numericTextBox2);
            this.Main_Panel.Panel1.Controls.Add(label9);
            this.Main_Panel.Panel1.Controls.Add(this.Descuento_TB);
            this.Main_Panel.Panel1.Controls.Add(label8);
            this.Main_Panel.Panel1.Controls.Add(this.numericTextBox1);
            this.Main_Panel.Panel1.Controls.Add(baseImponibleLabel);
            this.Main_Panel.Panel1.Controls.Add(this.Base_NTB);
            this.Main_Panel.Panel1.Controls.Add(totalLabel);
            this.Main_Panel.Panel1.Controls.Add(this.Total_NTB);
            this.Main_Panel.Panel1.Controls.Add(this.Cliente_GB);
            this.Main_Panel.Panel1.Controls.Add(this.Impresion_GB);
            this.Main_Panel.Panel1.Controls.Add(this.General_GB);
            this.Main_Panel.Panel1MinSize = 220;
            // 
            // Main_Panel.Panel2
            // 
            this.Main_Panel.Panel2.Controls.Add(this.Conceptos_Panel);
            this.Main_Panel.Size = new System.Drawing.Size(1192, 639);
            this.Main_Panel.SplitterDistance = 220;
            this.Main_Panel.TabIndex = 0;
            // 
            // PDescuento_NTB
            // 
            this.PDescuento_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PDescuento", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.PDescuento_NTB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PDescuento_NTB.ForeColor = System.Drawing.Color.Navy;
            this.PDescuento_NTB.Location = new System.Drawing.Point(332, 189);
            this.PDescuento_NTB.Name = "PDescuento_NTB";
            this.PDescuento_NTB.Size = new System.Drawing.Size(54, 23);
            this.PDescuento_NTB.TabIndex = 74;
            this.PDescuento_NTB.TabStop = false;
            this.PDescuento_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PDescuento_NTB.TextIsCurrency = false;
            this.PDescuento_NTB.TextIsInteger = false;
            this.PDescuento_NTB.Visible = false;
            // 
            // numericTextBox2
            // 
            this.numericTextBox2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Subtotal", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.numericTextBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericTextBox2.Location = new System.Drawing.Point(120, 189);
            this.numericTextBox2.Name = "numericTextBox2";
            this.numericTextBox2.ReadOnly = true;
            this.numericTextBox2.Size = new System.Drawing.Size(105, 23);
            this.numericTextBox2.TabIndex = 84;
            this.numericTextBox2.TabStop = false;
            this.numericTextBox2.Tag = "NO FORMAT";
            this.numericTextBox2.Text = "0.00";
            this.numericTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextBox2.TextIsCurrency = false;
            this.numericTextBox2.TextIsInteger = false;
            // 
            // Descuento_TB
            // 
            this.Descuento_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Descuento", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Descuento_TB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descuento_TB.Location = new System.Drawing.Point(472, 189);
            this.Descuento_TB.Name = "Descuento_TB";
            this.Descuento_TB.ReadOnly = true;
            this.Descuento_TB.Size = new System.Drawing.Size(96, 23);
            this.Descuento_TB.TabIndex = 82;
            this.Descuento_TB.TabStop = false;
            this.Descuento_TB.Tag = "NO FORMAT";
            this.Descuento_TB.Text = "0.00";
            this.Descuento_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Descuento_TB.TextIsCurrency = false;
            this.Descuento_TB.TextIsInteger = false;
            this.Descuento_TB.Visible = false;
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Impuestos", true));
            this.numericTextBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericTextBox1.Location = new System.Drawing.Point(872, 189);
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.ReadOnly = true;
            this.numericTextBox1.Size = new System.Drawing.Size(82, 23);
            this.numericTextBox1.TabIndex = 77;
            this.numericTextBox1.TabStop = false;
            this.numericTextBox1.Tag = "NO FORMAT";
            this.numericTextBox1.Text = "0.00";
            this.numericTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTextBox1.TextIsCurrency = false;
            this.numericTextBox1.TextIsInteger = false;
            // 
            // Base_NTB
            // 
            this.Base_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "BaseImponible", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "C2"));
            this.Base_NTB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Base_NTB.Location = new System.Drawing.Point(678, 189);
            this.Base_NTB.Name = "Base_NTB";
            this.Base_NTB.ReadOnly = true;
            this.Base_NTB.Size = new System.Drawing.Size(105, 23);
            this.Base_NTB.TabIndex = 76;
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
            this.Total_NTB.Location = new System.Drawing.Point(1022, 189);
            this.Total_NTB.Name = "Total_NTB";
            this.Total_NTB.ReadOnly = true;
            this.Total_NTB.Size = new System.Drawing.Size(108, 23);
            this.Total_NTB.TabIndex = 78;
            this.Total_NTB.TabStop = false;
            this.Total_NTB.Tag = "NO FORMAT";
            this.Total_NTB.Text = "0.00";
            this.Total_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Total_NTB.TextIsCurrency = false;
            this.Total_NTB.TextIsInteger = false;
            // 
            // Cliente_GB
            // 
            this.Cliente_GB.Controls.Add(this.textBox1);
            this.Cliente_GB.Controls.Add(label5);
            this.Cliente_GB.Controls.Add(this.DescuentoC_NTB);
            this.Cliente_GB.Controls.Add(pDescuentoLabel);
            this.Cliente_GB.Controls.Add(this.Cliente_BT);
            this.Cliente_GB.Controls.Add(numeroClienteLabel);
            this.Cliente_GB.Controls.Add(this.Cliente_TB);
            this.Cliente_GB.Controls.Add(this.IDCliente_TB);
            this.Cliente_GB.Controls.Add(this.VatNumberTB);
            this.Cliente_GB.Controls.Add(codigoLabel);
            this.Cliente_GB.Controls.Add(nombreLabel);
            this.Cliente_GB.Location = new System.Drawing.Point(445, 13);
            this.Cliente_GB.Name = "Cliente_GB";
            this.Cliente_GB.Size = new System.Drawing.Size(310, 170);
            this.Cliente_GB.TabIndex = 73;
            this.Cliente_GB.TabStop = false;
            this.Cliente_GB.Text = "Datos del Cliente";
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Cliente, "Impuesto", true));
            this.textBox1.Location = new System.Drawing.Point(71, 100);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(128, 21);
            this.textBox1.TabIndex = 46;
            this.textBox1.TabStop = false;
            // 
            // Datos_Cliente
            // 
            this.Datos_Cliente.DataSource = typeof(moleQule.Library.Invoice.ClienteInfo);
            // 
            // DescuentoC_NTB
            // 
            this.DescuentoC_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "PDescuento", true));
            this.DescuentoC_NTB.Location = new System.Drawing.Point(261, 100);
            this.DescuentoC_NTB.Name = "DescuentoC_NTB";
            this.DescuentoC_NTB.ReadOnly = true;
            this.DescuentoC_NTB.Size = new System.Drawing.Size(43, 21);
            this.DescuentoC_NTB.TabIndex = 43;
            this.DescuentoC_NTB.TabStop = false;
            this.DescuentoC_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DescuentoC_NTB.TextIsCurrency = false;
            this.DescuentoC_NTB.TextIsInteger = false;
            // 
            // Cliente_BT
            // 
            this.Cliente_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Cliente_BT.Location = new System.Drawing.Point(166, 19);
            this.Cliente_BT.Name = "Cliente_BT";
            this.Cliente_BT.Size = new System.Drawing.Size(28, 21);
            this.Cliente_BT.TabIndex = 0;
            this.Cliente_BT.UseVisualStyleBackColor = true;
            // 
            // Cliente_TB
            // 
            this.Cliente_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Cliente, "Nombre", true));
            this.Cliente_TB.Location = new System.Drawing.Point(71, 73);
            this.Cliente_TB.Name = "Cliente_TB";
            this.Cliente_TB.ReadOnly = true;
            this.Cliente_TB.Size = new System.Drawing.Size(233, 21);
            this.Cliente_TB.TabIndex = 1;
            this.Cliente_TB.TabStop = false;
            // 
            // IDCliente_TB
            // 
            this.IDCliente_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Cliente, "Codigo", true));
            this.IDCliente_TB.Location = new System.Drawing.Point(71, 19);
            this.IDCliente_TB.Name = "IDCliente_TB";
            this.IDCliente_TB.ReadOnly = true;
            this.IDCliente_TB.Size = new System.Drawing.Size(89, 21);
            this.IDCliente_TB.TabIndex = 3;
            this.IDCliente_TB.TabStop = false;
            this.IDCliente_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // VatNumberTB
            // 
            this.VatNumberTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_Cliente, "VatNumber", true));
            this.VatNumberTB.Location = new System.Drawing.Point(71, 46);
            this.VatNumberTB.Name = "VatNumberTB";
            this.VatNumberTB.ReadOnly = true;
            this.VatNumberTB.Size = new System.Drawing.Size(89, 21);
            this.VatNumberTB.TabIndex = 0;
            this.VatNumberTB.TabStop = false;
            this.VatNumberTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Impresion_GB
            // 
            this.Impresion_GB.Controls.Add(this.label14);
            this.Impresion_GB.Controls.Add(this.Observaciones_TB);
            this.Impresion_GB.Controls.Add(this.Estado_TB);
            this.Impresion_GB.Controls.Add(label1);
            this.Impresion_GB.Controls.Add(this.Estado_BT);
            this.Impresion_GB.Location = new System.Drawing.Point(763, 13);
            this.Impresion_GB.Name = "Impresion_GB";
            this.Impresion_GB.Size = new System.Drawing.Size(420, 170);
            this.Impresion_GB.TabIndex = 72;
            this.Impresion_GB.TabStop = false;
            this.Impresion_GB.Text = "Otros Datos";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(8, 24);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 52;
            this.label14.Text = "Observaciones:";
            // 
            // Observaciones_TB
            // 
            this.Observaciones_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.Observaciones_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Observaciones_TB.Location = new System.Drawing.Point(8, 41);
            this.Observaciones_TB.Multiline = true;
            this.Observaciones_TB.Name = "Observaciones_TB";
            this.Observaciones_TB.Size = new System.Drawing.Size(399, 75);
            this.Observaciones_TB.TabIndex = 3;
            // 
            // Estado_TB
            // 
            this.Estado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "EstadoLabel", true));
            this.Estado_TB.Location = new System.Drawing.Point(60, 125);
            this.Estado_TB.Name = "Estado_TB";
            this.Estado_TB.ReadOnly = true;
            this.Estado_TB.Size = new System.Drawing.Size(186, 21);
            this.Estado_TB.TabIndex = 80;
            this.Estado_TB.TabStop = false;
            this.Estado_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Estado_TB.Visible = false;
            // 
            // Estado_BT
            // 
            this.Estado_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Estado_BT.Location = new System.Drawing.Point(252, 125);
            this.Estado_BT.Name = "Estado_BT";
            this.Estado_BT.Size = new System.Drawing.Size(28, 21);
            this.Estado_BT.TabIndex = 79;
            this.Estado_BT.UseVisualStyleBackColor = true;
            this.Estado_BT.Visible = false;
            // 
            // General_GB
            // 
            this.General_GB.Controls.Add(this.Expediente_BT);
            this.General_GB.Controls.Add(label7);
            this.General_GB.Controls.Add(this.Expediente_TB);
            this.General_GB.Controls.Add(this.Almacen_BT);
            this.General_GB.Controls.Add(label3);
            this.General_GB.Controls.Add(this.Almacen_TB);
            this.General_GB.Controls.Add(this.Serie_BT);
            this.General_GB.Controls.Add(this.Serie_TB);
            this.General_GB.Controls.Add(label4);
            this.General_GB.Controls.Add(this.Usuario_BT);
            this.General_GB.Controls.Add(label6);
            this.General_GB.Controls.Add(this.Usuario_TB);
            this.General_GB.Controls.Add(this.Fecha_DTP);
            this.General_GB.Controls.Add(this.IDManual_CkB);
            this.General_GB.Controls.Add(label2);
            this.General_GB.Controls.Add(fechaLabel);
            this.General_GB.Controls.Add(this.IDPedido_TB);
            this.General_GB.Location = new System.Drawing.Point(7, 13);
            this.General_GB.Name = "General_GB";
            this.General_GB.Size = new System.Drawing.Size(430, 170);
            this.General_GB.TabIndex = 71;
            this.General_GB.TabStop = false;
            this.General_GB.Text = "Datos del Pedido";
            // 
            // Expediente_BT
            // 
            this.Expediente_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Expediente_BT.Location = new System.Drawing.Point(316, 129);
            this.Expediente_BT.Name = "Expediente_BT";
            this.Expediente_BT.Size = new System.Drawing.Size(28, 21);
            this.Expediente_BT.TabIndex = 110;
            this.Expediente_BT.UseVisualStyleBackColor = true;
            // 
            // Expediente_TB
            // 
            this.Expediente_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Expediente", true));
            this.Expediente_TB.Location = new System.Drawing.Point(80, 128);
            this.Expediente_TB.Name = "Expediente_TB";
            this.Expediente_TB.ReadOnly = true;
            this.Expediente_TB.Size = new System.Drawing.Size(230, 21);
            this.Expediente_TB.TabIndex = 111;
            this.Expediente_TB.TabStop = false;
            this.Expediente_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Almacen_BT
            // 
            this.Almacen_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Almacen_BT.Location = new System.Drawing.Point(316, 105);
            this.Almacen_BT.Name = "Almacen_BT";
            this.Almacen_BT.Size = new System.Drawing.Size(28, 21);
            this.Almacen_BT.TabIndex = 107;
            this.Almacen_BT.UseVisualStyleBackColor = true;
            // 
            // Almacen_TB
            // 
            this.Almacen_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Almacen", true));
            this.Almacen_TB.Location = new System.Drawing.Point(80, 104);
            this.Almacen_TB.Name = "Almacen_TB";
            this.Almacen_TB.ReadOnly = true;
            this.Almacen_TB.Size = new System.Drawing.Size(230, 21);
            this.Almacen_TB.TabIndex = 108;
            this.Almacen_TB.TabStop = false;
            this.Almacen_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Serie_BT
            // 
            this.Serie_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Serie_BT.Location = new System.Drawing.Point(316, 50);
            this.Serie_BT.Name = "Serie_BT";
            this.Serie_BT.Size = new System.Drawing.Size(28, 21);
            this.Serie_BT.TabIndex = 106;
            this.Serie_BT.UseVisualStyleBackColor = true;
            // 
            // Serie_TB
            // 
            this.Serie_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "NSerieSerie", true));
            this.Serie_TB.Enabled = false;
            this.Serie_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Serie_TB.Location = new System.Drawing.Point(80, 51);
            this.Serie_TB.Name = "Serie_TB";
            this.Serie_TB.ReadOnly = true;
            this.Serie_TB.Size = new System.Drawing.Size(230, 21);
            this.Serie_TB.TabIndex = 105;
            this.Serie_TB.TabStop = false;
            this.Serie_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Usuario_BT
            // 
            this.Usuario_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Usuario_BT.Location = new System.Drawing.Point(316, 78);
            this.Usuario_BT.Name = "Usuario_BT";
            this.Usuario_BT.Size = new System.Drawing.Size(28, 21);
            this.Usuario_BT.TabIndex = 101;
            this.Usuario_BT.UseVisualStyleBackColor = true;
            // 
            // Usuario_TB
            // 
            this.Usuario_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Usuario", true));
            this.Usuario_TB.Location = new System.Drawing.Point(80, 78);
            this.Usuario_TB.Name = "Usuario_TB";
            this.Usuario_TB.ReadOnly = true;
            this.Usuario_TB.Size = new System.Drawing.Size(230, 21);
            this.Usuario_TB.TabIndex = 102;
            this.Usuario_TB.TabStop = false;
            this.Usuario_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Fecha_DTP
            // 
            this.Fecha_DTP.CalendarFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fecha_DTP.CalendarTitleForeColor = System.Drawing.Color.Navy;
            this.Fecha_DTP.Checked = false;
            this.Fecha_DTP.CustomFormat = "dd/MM/yyyy HH:mm";
            this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fecha_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Fecha_DTP.Location = new System.Drawing.Point(61, 27);
            this.Fecha_DTP.Name = "Fecha_DTP";
            this.Fecha_DTP.ShowCheckBox = true;
            this.Fecha_DTP.Size = new System.Drawing.Size(142, 21);
            this.Fecha_DTP.TabIndex = 78;
            // 
            // IDManual_CkB
            // 
            this.IDManual_CkB.AutoSize = true;
            this.IDManual_CkB.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.Datos, "IDManual", true));
            this.IDManual_CkB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDManual_CkB.Location = new System.Drawing.Point(359, 28);
            this.IDManual_CkB.Name = "IDManual_CkB";
            this.IDManual_CkB.Size = new System.Drawing.Size(60, 17);
            this.IDManual_CkB.TabIndex = 46;
            this.IDManual_CkB.Text = "Manual";
            this.IDManual_CkB.UseVisualStyleBackColor = true;
            // 
            // IDPedido_TB
            // 
            this.IDPedido_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
            this.IDPedido_TB.Location = new System.Drawing.Point(285, 26);
            this.IDPedido_TB.Name = "IDPedido_TB";
            this.IDPedido_TB.ReadOnly = true;
            this.IDPedido_TB.Size = new System.Drawing.Size(68, 21);
            this.IDPedido_TB.TabIndex = 0;
            this.IDPedido_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Conceptos_Panel
            // 
            this.Conceptos_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Conceptos_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Conceptos_Panel.Location = new System.Drawing.Point(0, 0);
            this.Conceptos_Panel.Name = "Conceptos_Panel";
            this.Conceptos_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Conceptos_Panel.Panel1
            // 
            this.Conceptos_Panel.Panel1.Controls.Add(this.Conceptos_TS);
            this.Conceptos_Panel.Panel1MinSize = 39;
            // 
            // Conceptos_Panel.Panel2
            // 
            this.Conceptos_Panel.Panel2.Controls.Add(this.Lineas_DGW);
            this.Conceptos_Panel.Panel2MinSize = 40;
            this.Conceptos_Panel.Size = new System.Drawing.Size(1192, 415);
            this.Conceptos_Panel.SplitterDistance = 39;
            this.Conceptos_Panel.SplitterWidth = 1;
            this.Conceptos_Panel.TabIndex = 4;
            // 
            // Conceptos_TS
            // 
            this.Conceptos_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Conceptos_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddConcepto_TI,
            this.AddStock_TI,
            this.Edit_TI,
            this.View_TI,
            this.Delete_TI,
            this.toolStripLabel1});
            this.Conceptos_TS.Location = new System.Drawing.Point(0, 0);
            this.Conceptos_TS.Name = "Conceptos_TS";
            this.HelpProvider.SetShowHelp(this.Conceptos_TS, true);
            this.Conceptos_TS.Size = new System.Drawing.Size(1192, 39);
            this.Conceptos_TS.TabIndex = 6;
            // 
            // AddConcepto_TI
            // 
            this.AddConcepto_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddConcepto_TI.Image = global::moleQule.Face.Invoice.Properties.Resources.item_add;
            this.AddConcepto_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddConcepto_TI.Name = "AddConcepto_TI";
            this.AddConcepto_TI.Size = new System.Drawing.Size(36, 36);
            this.AddConcepto_TI.Text = "Nuevo Concepto sin control de Stock";
            this.AddConcepto_TI.Click += new System.EventHandler(this.ConceptoLibre_BT_Click);
            // 
            // AddStock_TI
            // 
            this.AddStock_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddStock_TI.Image = global::moleQule.Face.Invoice.Properties.Resources.item_add_stock;
            this.AddStock_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddStock_TI.Name = "AddStock_TI";
            this.AddStock_TI.Size = new System.Drawing.Size(36, 36);
            this.AddStock_TI.Text = "Nuevo Concepto (con control de stock)";
            this.AddStock_TI.Click += new System.EventHandler(this.AddConcepto_TI_Click);
            // 
            // Edit_TI
            // 
            this.Edit_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Edit_TI.Image = global::moleQule.Face.Invoice.Properties.Resources.item_edit;
            this.Edit_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Edit_TI.Name = "Edit_TI";
            this.Edit_TI.Size = new System.Drawing.Size(36, 36);
            this.Edit_TI.Text = "Editar Concepto";
            this.Edit_TI.Click += new System.EventHandler(this.Edit_TI_Click);
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
            this.Delete_TI.Click += new System.EventHandler(this.Delete_TI_Click);
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
            this.Lineas_DGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Lineas_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Concepto,
            this.Almacen,
            this.Expediente,
            this.FacturacionPeso,
            this.LiPieces,
            this.LiKilos,
            this.PendienteBultos,
            this.Pendiente,
            this.Precio,
            this.PDescuento,
            this.Descuento,
            this.PImpuestos,
            this.Impuestos,
            this.Total});
            this.Lineas_DGW.DataSource = this.Datos_Lineas;
            this.Lineas_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lineas_DGW.Location = new System.Drawing.Point(0, 0);
            this.Lineas_DGW.MultiSelect = false;
            this.Lineas_DGW.Name = "Lineas_DGW";
            this.Lineas_DGW.RowHeadersWidth = 25;
            this.Lineas_DGW.Size = new System.Drawing.Size(1192, 375);
            this.Lineas_DGW.TabIndex = 3;
            this.Lineas_DGW.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Lineas_DGW_CellContentClick);
            this.Lineas_DGW.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.Lineas_DGW_CellValidated);
            this.Lineas_DGW.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.Lineas_DGW_RowPrePaint);
            this.Lineas_DGW.DoubleClick += new System.EventHandler(this.Lineas_DGW_DoubleClick);
            // 
            // Datos_Lineas
            // 
            this.Datos_Lineas.AllowNew = true;
            // 
            // Concepto
            // 
            this.Concepto.DataPropertyName = "Concepto";
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Concepto.DefaultCellStyle = dataGridViewCellStyle1;
            this.Concepto.HeaderText = "Concepto";
            this.Concepto.MinimumWidth = 255;
            this.Concepto.Name = "Concepto";
            this.Concepto.Width = 255;
            // 
            // Almacen
            // 
            this.Almacen.DataPropertyName = "Almacen";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Almacen.DefaultCellStyle = dataGridViewCellStyle2;
            this.Almacen.HeaderText = "Almacén";
            this.Almacen.Name = "Almacen";
            this.Almacen.ReadOnly = true;
            this.Almacen.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Almacen.Width = 75;
            // 
            // Expediente
            // 
            this.Expediente.DataPropertyName = "Expediente";
            this.Expediente.HeaderText = "Expediente";
            this.Expediente.Name = "Expediente";
            this.Expediente.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Expediente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // FacturacionPeso
            // 
            this.FacturacionPeso.DataPropertyName = "FacturacionPeso";
            this.FacturacionPeso.HeaderText = "Fac. Peso";
            this.FacturacionPeso.Name = "FacturacionPeso";
            this.FacturacionPeso.Width = 35;
            // 
            // LiPieces
            // 
            this.LiPieces.DataPropertyName = "CantidadBultos";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.LiPieces.DefaultCellStyle = dataGridViewCellStyle3;
            this.LiPieces.HeaderText = "Uds.";
            this.LiPieces.Name = "LiPieces";
            this.LiPieces.Width = 70;
            // 
            // LiKilos
            // 
            this.LiKilos.DataPropertyName = "CantidadKilos";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.LiKilos.DefaultCellStyle = dataGridViewCellStyle4;
            this.LiKilos.HeaderText = "Kgs.";
            this.LiKilos.Name = "LiKilos";
            this.LiKilos.Width = 80;
            // 
            // PendienteBultos
            // 
            this.PendienteBultos.DataPropertyName = "PendienteBultos";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.PendienteBultos.DefaultCellStyle = dataGridViewCellStyle5;
            this.PendienteBultos.HeaderText = "Pendiente";
            this.PendienteBultos.Name = "PendienteBultos";
            this.PendienteBultos.ReadOnly = true;
            this.PendienteBultos.Width = 70;
            // 
            // Pendiente
            // 
            this.Pendiente.DataPropertyName = "Pendiente";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.Pendiente.DefaultCellStyle = dataGridViewCellStyle6;
            this.Pendiente.HeaderText = "Pendiente Kg";
            this.Pendiente.Name = "Pendiente";
            this.Pendiente.ReadOnly = true;
            this.Pendiente.Width = 70;
            // 
            // Precio
            // 
            this.Precio.DataPropertyName = "Precio";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N5";
            dataGridViewCellStyle7.NullValue = null;
            this.Precio.DefaultCellStyle = dataGridViewCellStyle7;
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.Width = 65;
            // 
            // PDescuento
            // 
            this.PDescuento.DataPropertyName = "PDescuento";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.PDescuento.DefaultCellStyle = dataGridViewCellStyle8;
            this.PDescuento.HeaderText = "% Dto.";
            this.PDescuento.Name = "PDescuento";
            this.PDescuento.Width = 40;
            // 
            // Descuento
            // 
            this.Descuento.DataPropertyName = "Descuento";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            this.Descuento.DefaultCellStyle = dataGridViewCellStyle9;
            this.Descuento.HeaderText = "Descuento";
            this.Descuento.Name = "Descuento";
            this.Descuento.ReadOnly = true;
            this.Descuento.Width = 60;
            // 
            // PImpuestos
            // 
            this.PImpuestos.DataPropertyName = "PImpuestos";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N2";
            this.PImpuestos.DefaultCellStyle = dataGridViewCellStyle10;
            this.PImpuestos.HeaderText = "% Imp.";
            this.PImpuestos.Name = "PImpuestos";
            this.PImpuestos.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PImpuestos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PImpuestos.Width = 40;
            // 
            // Impuestos
            // 
            this.Impuestos.DataPropertyName = "Impuestos";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N2";
            dataGridViewCellStyle11.NullValue = null;
            this.Impuestos.DefaultCellStyle = dataGridViewCellStyle11;
            this.Impuestos.HeaderText = "Impuestos";
            this.Impuestos.Name = "Impuestos";
            this.Impuestos.ReadOnly = true;
            this.Impuestos.Width = 65;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            dataGridViewCellStyle12.NullValue = null;
            this.Total.DefaultCellStyle = dataGridViewCellStyle12;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 70;
            // 
            // PedidoForm
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1194, 722);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PedidoForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "PedidoForm";
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            this.Pie_Panel.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pie_Panel)).EndInit();
            this.Pie_Panel.ResumeLayout(false);
            this.Content_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Content_Panel)).EndInit();
            this.Content_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            this.ProgressBK_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            this.Main_Panel.Panel1.ResumeLayout(false);
            this.Main_Panel.Panel1.PerformLayout();
            this.Main_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Main_Panel)).EndInit();
            this.Main_Panel.ResumeLayout(false);
            this.Cliente_GB.ResumeLayout(false);
            this.Cliente_GB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Cliente)).EndInit();
            this.Impresion_GB.ResumeLayout(false);
            this.Impresion_GB.PerformLayout();
            this.General_GB.ResumeLayout(false);
            this.General_GB.PerformLayout();
            this.Conceptos_Panel.Panel1.ResumeLayout(false);
            this.Conceptos_Panel.Panel1.PerformLayout();
            this.Conceptos_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Conceptos_Panel)).EndInit();
            this.Conceptos_Panel.ResumeLayout(false);
            this.Conceptos_TS.ResumeLayout(false);
            this.Conceptos_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Lineas_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Lineas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.BindingSource Datos_Lineas;
		private System.Windows.Forms.SplitContainer Main_Panel;
		protected System.Windows.Forms.DataGridView Lineas_DGW;
		protected System.Windows.Forms.BindingSource Datos_Cliente;
		protected System.Windows.Forms.GroupBox Cliente_GB;
		protected System.Windows.Forms.Button Cliente_BT;
		protected System.Windows.Forms.TextBox Cliente_TB;
		protected System.Windows.Forms.TextBox IDCliente_TB;
		protected System.Windows.Forms.TextBox VatNumberTB;
		protected System.Windows.Forms.GroupBox Impresion_GB;
		protected System.Windows.Forms.Label label14;
		protected System.Windows.Forms.TextBox Observaciones_TB;
		protected System.Windows.Forms.GroupBox General_GB;
		protected System.Windows.Forms.DateTimePicker Fecha_DTP;
		protected System.Windows.Forms.CheckBox IDManual_CkB;
		protected System.Windows.Forms.MaskedTextBox IDPedido_TB;
		protected System.Windows.Forms.Button Estado_BT;
		protected System.Windows.Forms.TextBox Estado_TB;
		private System.Windows.Forms.SplitContainer Conceptos_Panel;
		protected System.Windows.Forms.ToolStrip Conceptos_TS;
		protected System.Windows.Forms.ToolStripButton AddConcepto_TI;
		protected System.Windows.Forms.ToolStripButton Edit_TI;
		protected System.Windows.Forms.ToolStripButton View_TI;
		protected System.Windows.Forms.ToolStripButton Delete_TI;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		protected Controls.NumericTextBox PDescuento_NTB;
		protected Controls.NumericTextBox numericTextBox2;
		protected Controls.NumericTextBox Descuento_TB;
		protected Controls.NumericTextBox numericTextBox1;
		protected Controls.NumericTextBox Base_NTB;
		protected Controls.NumericTextBox Total_NTB;
		protected System.Windows.Forms.TextBox textBox1;
		protected Controls.NumericTextBox DescuentoC_NTB;
		private System.Windows.Forms.ToolStripButton AddStock_TI;
		protected System.Windows.Forms.Button Expediente_BT;
		protected System.Windows.Forms.TextBox Expediente_TB;
		protected System.Windows.Forms.Button Almacen_BT;
		protected System.Windows.Forms.TextBox Almacen_TB;
		protected System.Windows.Forms.Button Serie_BT;
		protected System.Windows.Forms.TextBox Serie_TB;
		protected System.Windows.Forms.Button Usuario_BT;
        protected System.Windows.Forms.TextBox Usuario_TB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Almacen;
        private System.Windows.Forms.DataGridViewButtonColumn Expediente;
        private System.Windows.Forms.DataGridViewCheckBoxColumn FacturacionPeso;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiPieces;
        private System.Windows.Forms.DataGridViewTextBoxColumn LiKilos;
        private System.Windows.Forms.DataGridViewTextBoxColumn PendienteBultos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pendiente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descuento;
        private System.Windows.Forms.DataGridViewButtonColumn PImpuestos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Impuestos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
    }
}
