namespace moleQule.Face.Invoice
{
    partial class CobroFacturaUIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CobroFacturaUIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.Label tipoLabel;
            System.Windows.Forms.Label observacionesLabel;
            System.Windows.Forms.Label cuentaLabel;
            System.Windows.Forms.Label importeLabel;
            System.Windows.Forms.Label vencimientoLabel;
            System.Windows.Forms.Label fechaLabel;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label2;
            this.Facturas_Panel = new System.Windows.Forms.SplitContainer();
            this.Facturas_TS = new System.Windows.Forms.ToolStrip();
            this.AddFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.EditFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.ViewFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.DeleteFactura_TI = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.Facturas_DGW = new System.Windows.Forms.DataGridView();
            this.NumeroSerie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiasTranscurridos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cobrado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Acumulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Asignacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaAsignacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vinculado = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Datos_Facturas = new System.Windows.Forms.BindingSource(this.components);
            this.Facturas_SC = new System.Windows.Forms.SplitContainer();
            this.Content_SC = new System.Windows.Forms.SplitContainer();
            this.Facturas_GB = new System.Windows.Forms.GroupBox();
            this.Observaciones_RTB = new System.Windows.Forms.RichTextBox();
            this.Cuenta_TB = new System.Windows.Forms.TextBox();
            this.Cuenta_BT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NoAsignado_TB = new System.Windows.Forms.TextBox();
            this.Importe_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.Vencimiento_DTP = new System.Windows.Forms.DateTimePicker();
            this.Fecha_DTP = new System.Windows.Forms.DateTimePicker();
            this.Liberar_BT = new System.Windows.Forms.Button();
            this.Repartir_BT = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ID_TB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.NCobro_TB = new System.Windows.Forms.TextBox();
            this.TPV_BT = new System.Windows.Forms.Button();
            this.TPV_TB = new System.Windows.Forms.TextBox();
            this.GastosBancarios_NTB = new moleQule.Face.Controls.NumericTextBox();
            this.MedioPago_BT = new System.Windows.Forms.Button();
            this.MedioPago_TB = new System.Windows.Forms.TextBox();
            this.EstadoCobro_TB = new System.Windows.Forms.TextBox();
            this.EstadoCobro_BT = new System.Windows.Forms.Button();
            tipoLabel = new System.Windows.Forms.Label();
            observacionesLabel = new System.Windows.Forms.Label();
            cuentaLabel = new System.Windows.Forms.Label();
            importeLabel = new System.Windows.Forms.Label();
            vencimientoLabel = new System.Windows.Forms.Label();
            fechaLabel = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            this.Source_GB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Facturas_Panel)).BeginInit();
            this.Facturas_Panel.Panel1.SuspendLayout();
            this.Facturas_Panel.Panel2.SuspendLayout();
            this.Facturas_Panel.SuspendLayout();
            this.Facturas_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Facturas_DGW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Facturas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Facturas_SC)).BeginInit();
            this.Facturas_SC.Panel1.SuspendLayout();
            this.Facturas_SC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Content_SC)).BeginInit();
            this.Content_SC.Panel1.SuspendLayout();
            this.Content_SC.Panel2.SuspendLayout();
            this.Content_SC.SuspendLayout();
            this.Facturas_GB.SuspendLayout();
            this.SuspendLayout();
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Invoice.Charge);
            // 
            // Submit_BT
            // 
            resources.ApplyResources(this.Submit_BT, "Submit_BT");
            this.ErrorMng_EP.SetError(this.Submit_BT, resources.GetString("Submit_BT.Error"));
            this.HelpProvider.SetHelpKeyword(this.Submit_BT, resources.GetString("Submit_BT.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Submit_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Submit_BT.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Submit_BT, resources.GetString("Submit_BT.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Submit_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Submit_BT.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Submit_BT, ((int)(resources.GetObject("Submit_BT.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Submit_BT, ((bool)(resources.GetObject("Submit_BT.ShowHelp"))));
            // 
            // Cancel_BT
            // 
            resources.ApplyResources(this.Cancel_BT, "Cancel_BT");
            this.ErrorMng_EP.SetError(this.Cancel_BT, resources.GetString("Cancel_BT.Error"));
            this.HelpProvider.SetHelpKeyword(this.Cancel_BT, resources.GetString("Cancel_BT.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Cancel_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Cancel_BT.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Cancel_BT, resources.GetString("Cancel_BT.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Cancel_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Cancel_BT.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Cancel_BT, ((int)(resources.GetObject("Cancel_BT.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Cancel_BT, ((bool)(resources.GetObject("Cancel_BT.ShowHelp"))));
            // 
            // Source_GB
            // 
            resources.ApplyResources(this.Source_GB, "Source_GB");
            this.Source_GB.Controls.Add(label2);
            this.Source_GB.Controls.Add(this.EstadoCobro_TB);
            this.Source_GB.Controls.Add(this.EstadoCobro_BT);
            this.Source_GB.Controls.Add(this.MedioPago_TB);
            this.Source_GB.Controls.Add(this.MedioPago_BT);
            this.Source_GB.Controls.Add(label6);
            this.Source_GB.Controls.Add(this.GastosBancarios_NTB);
            this.Source_GB.Controls.Add(this.TPV_BT);
            this.Source_GB.Controls.Add(label5);
            this.Source_GB.Controls.Add(this.TPV_TB);
            this.Source_GB.Controls.Add(this.label4);
            this.Source_GB.Controls.Add(this.NCobro_TB);
            this.Source_GB.Controls.Add(this.label3);
            this.Source_GB.Controls.Add(this.ID_TB);
            this.Source_GB.Controls.Add(this.Repartir_BT);
            this.Source_GB.Controls.Add(this.Liberar_BT);
            this.Source_GB.Controls.Add(importeLabel);
            this.Source_GB.Controls.Add(this.Importe_NTB);
            this.Source_GB.Controls.Add(vencimientoLabel);
            this.Source_GB.Controls.Add(this.Vencimiento_DTP);
            this.Source_GB.Controls.Add(fechaLabel);
            this.Source_GB.Controls.Add(this.Fecha_DTP);
            this.Source_GB.Controls.Add(this.label1);
            this.Source_GB.Controls.Add(this.NoAsignado_TB);
            this.Source_GB.Controls.Add(this.Cuenta_BT);
            this.Source_GB.Controls.Add(tipoLabel);
            this.Source_GB.Controls.Add(observacionesLabel);
            this.Source_GB.Controls.Add(this.Observaciones_RTB);
            this.Source_GB.Controls.Add(cuentaLabel);
            this.Source_GB.Controls.Add(this.Cuenta_TB);
            this.ErrorMng_EP.SetError(this.Source_GB, resources.GetString("Source_GB.Error"));
            this.Source_GB.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.HelpProvider.SetHelpKeyword(this.Source_GB, resources.GetString("Source_GB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Source_GB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Source_GB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Source_GB, resources.GetString("Source_GB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Source_GB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Source_GB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Source_GB, ((int)(resources.GetObject("Source_GB.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Source_GB, ((bool)(resources.GetObject("Source_GB.ShowHelp"))));
            // 
            // PanelesV
            // 
            resources.ApplyResources(this.PanelesV, "PanelesV");
            this.ErrorMng_EP.SetError(this.PanelesV, resources.GetString("PanelesV.Error"));
            this.HelpProvider.SetHelpKeyword(this.PanelesV, resources.GetString("PanelesV.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.PanelesV, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("PanelesV.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.PanelesV, resources.GetString("PanelesV.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.PanelesV, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("PanelesV.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.PanelesV, ((int)(resources.GetObject("PanelesV.IconPadding"))));
            // 
            // PanelesV.Panel1
            // 
            resources.ApplyResources(this.PanelesV.Panel1, "PanelesV.Panel1");
            this.PanelesV.Panel1.Controls.Add(this.Content_SC);
            this.ErrorMng_EP.SetError(this.PanelesV.Panel1, resources.GetString("PanelesV.Panel1.Error"));
            this.HelpProvider.SetHelpKeyword(this.PanelesV.Panel1, resources.GetString("PanelesV.Panel1.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.PanelesV.Panel1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("PanelesV.Panel1.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.PanelesV.Panel1, resources.GetString("PanelesV.Panel1.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.PanelesV.Panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("PanelesV.Panel1.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.PanelesV.Panel1, ((int)(resources.GetObject("PanelesV.Panel1.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, ((bool)(resources.GetObject("PanelesV.Panel1.ShowHelp"))));
            // 
            // PanelesV.Panel2
            // 
            resources.ApplyResources(this.PanelesV.Panel2, "PanelesV.Panel2");
            this.ErrorMng_EP.SetError(this.PanelesV.Panel2, resources.GetString("PanelesV.Panel2.Error"));
            this.HelpProvider.SetHelpKeyword(this.PanelesV.Panel2, resources.GetString("PanelesV.Panel2.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.PanelesV.Panel2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("PanelesV.Panel2.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.PanelesV.Panel2, resources.GetString("PanelesV.Panel2.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.PanelesV.Panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("PanelesV.Panel2.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.PanelesV.Panel2, ((int)(resources.GetObject("PanelesV.Panel2.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, ((bool)(resources.GetObject("PanelesV.Panel2.ShowHelp"))));
            this.HelpProvider.SetShowHelp(this.PanelesV, ((bool)(resources.GetObject("PanelesV.ShowHelp"))));
            // 
            // SaveFile_SFD
            // 
            resources.ApplyResources(this.SaveFile_SFD, "SaveFile_SFD");
            // 
            // ErrorMng_EP
            // 
            resources.ApplyResources(this.ErrorMng_EP, "ErrorMng_EP");
            // 
            // HelpProvider
            // 
            resources.ApplyResources(this.HelpProvider, "HelpProvider");
            // 
            // Animation
            // 
            resources.ApplyResources(this.Animation, "Animation");
            this.ErrorMng_EP.SetError(this.Animation, resources.GetString("Animation.Error"));
            this.HelpProvider.SetHelpKeyword(this.Animation, resources.GetString("Animation.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Animation, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Animation.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Animation, resources.GetString("Animation.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Animation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Animation.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Animation, ((int)(resources.GetObject("Animation.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Animation, ((bool)(resources.GetObject("Animation.ShowHelp"))));
            // 
            // CancelBkJob_BT
            // 
            resources.ApplyResources(this.CancelBkJob_BT, "CancelBkJob_BT");
            this.ErrorMng_EP.SetError(this.CancelBkJob_BT, resources.GetString("CancelBkJob_BT.Error"));
            this.HelpProvider.SetHelpKeyword(this.CancelBkJob_BT, resources.GetString("CancelBkJob_BT.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.CancelBkJob_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("CancelBkJob_BT.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.CancelBkJob_BT, resources.GetString("CancelBkJob_BT.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.CancelBkJob_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("CancelBkJob_BT.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.CancelBkJob_BT, ((int)(resources.GetObject("CancelBkJob_BT.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.CancelBkJob_BT, ((bool)(resources.GetObject("CancelBkJob_BT.ShowHelp"))));
            // 
            // ProgressMsg_LB
            // 
            resources.ApplyResources(this.ProgressMsg_LB, "ProgressMsg_LB");
            this.ErrorMng_EP.SetError(this.ProgressMsg_LB, resources.GetString("ProgressMsg_LB.Error"));
            this.HelpProvider.SetHelpKeyword(this.ProgressMsg_LB, resources.GetString("ProgressMsg_LB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.ProgressMsg_LB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("ProgressMsg_LB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.ProgressMsg_LB, resources.GetString("ProgressMsg_LB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.ProgressMsg_LB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("ProgressMsg_LB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.ProgressMsg_LB, ((int)(resources.GetObject("ProgressMsg_LB.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.ProgressMsg_LB, ((bool)(resources.GetObject("ProgressMsg_LB.ShowHelp"))));
            // 
            // Title_LB
            // 
            resources.ApplyResources(this.Title_LB, "Title_LB");
            this.ErrorMng_EP.SetError(this.Title_LB, resources.GetString("Title_LB.Error"));
            this.HelpProvider.SetHelpKeyword(this.Title_LB, resources.GetString("Title_LB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Title_LB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Title_LB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Title_LB, resources.GetString("Title_LB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Title_LB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Title_LB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Title_LB, ((int)(resources.GetObject("Title_LB.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Title_LB, ((bool)(resources.GetObject("Title_LB.ShowHelp"))));
            // 
            // Progress_Panel
            // 
            resources.ApplyResources(this.Progress_Panel, "Progress_Panel");
            this.ErrorMng_EP.SetError(this.Progress_Panel, resources.GetString("Progress_Panel.Error"));
            this.HelpProvider.SetHelpKeyword(this.Progress_Panel, resources.GetString("Progress_Panel.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Progress_Panel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Progress_Panel.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Progress_Panel, resources.GetString("Progress_Panel.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Progress_Panel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Progress_Panel.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Progress_Panel, ((int)(resources.GetObject("Progress_Panel.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Progress_Panel, ((bool)(resources.GetObject("Progress_Panel.ShowHelp"))));
            // 
            // ProgressBK_Panel
            // 
            resources.ApplyResources(this.ProgressBK_Panel, "ProgressBK_Panel");
            this.ErrorMng_EP.SetError(this.ProgressBK_Panel, resources.GetString("ProgressBK_Panel.Error"));
            this.HelpProvider.SetHelpKeyword(this.ProgressBK_Panel, resources.GetString("ProgressBK_Panel.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.ProgressBK_Panel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("ProgressBK_Panel.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.ProgressBK_Panel, resources.GetString("ProgressBK_Panel.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.ProgressBK_Panel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("ProgressBK_Panel.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.ProgressBK_Panel, ((int)(resources.GetObject("ProgressBK_Panel.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.ProgressBK_Panel, ((bool)(resources.GetObject("ProgressBK_Panel.ShowHelp"))));
            // 
            // ProgressInfo_PB
            // 
            resources.ApplyResources(this.ProgressInfo_PB, "ProgressInfo_PB");
            this.ErrorMng_EP.SetError(this.ProgressInfo_PB, resources.GetString("ProgressInfo_PB.Error"));
            this.HelpProvider.SetHelpKeyword(this.ProgressInfo_PB, resources.GetString("ProgressInfo_PB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.ProgressInfo_PB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("ProgressInfo_PB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.ProgressInfo_PB, resources.GetString("ProgressInfo_PB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.ProgressInfo_PB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("ProgressInfo_PB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.ProgressInfo_PB, ((int)(resources.GetObject("ProgressInfo_PB.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.ProgressInfo_PB, ((bool)(resources.GetObject("ProgressInfo_PB.ShowHelp"))));
            // 
            // Progress_PB
            // 
            resources.ApplyResources(this.Progress_PB, "Progress_PB");
            this.ErrorMng_EP.SetError(this.Progress_PB, resources.GetString("Progress_PB.Error"));
            this.HelpProvider.SetHelpKeyword(this.Progress_PB, resources.GetString("Progress_PB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Progress_PB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Progress_PB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Progress_PB, resources.GetString("Progress_PB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Progress_PB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Progress_PB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Progress_PB, ((int)(resources.GetObject("Progress_PB.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Progress_PB, ((bool)(resources.GetObject("Progress_PB.ShowHelp"))));
            // 
            // Facturas_Panel
            // 
            resources.ApplyResources(this.Facturas_Panel, "Facturas_Panel");
            this.ErrorMng_EP.SetError(this.Facturas_Panel, resources.GetString("Facturas_Panel.Error"));
            this.Facturas_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.HelpProvider.SetHelpKeyword(this.Facturas_Panel, resources.GetString("Facturas_Panel.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Facturas_Panel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Facturas_Panel.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Facturas_Panel, resources.GetString("Facturas_Panel.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Facturas_Panel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Facturas_Panel.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Facturas_Panel, ((int)(resources.GetObject("Facturas_Panel.IconPadding"))));
            this.Facturas_Panel.Name = "Facturas_Panel";
            // 
            // Facturas_Panel.Panel1
            // 
            resources.ApplyResources(this.Facturas_Panel.Panel1, "Facturas_Panel.Panel1");
            this.Facturas_Panel.Panel1.Controls.Add(this.Facturas_TS);
            this.ErrorMng_EP.SetError(this.Facturas_Panel.Panel1, resources.GetString("Facturas_Panel.Panel1.Error"));
            this.HelpProvider.SetHelpKeyword(this.Facturas_Panel.Panel1, resources.GetString("Facturas_Panel.Panel1.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Facturas_Panel.Panel1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Facturas_Panel.Panel1.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Facturas_Panel.Panel1, resources.GetString("Facturas_Panel.Panel1.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Facturas_Panel.Panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Facturas_Panel.Panel1.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Facturas_Panel.Panel1, ((int)(resources.GetObject("Facturas_Panel.Panel1.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Facturas_Panel.Panel1, ((bool)(resources.GetObject("Facturas_Panel.Panel1.ShowHelp"))));
            // 
            // Facturas_Panel.Panel2
            // 
            resources.ApplyResources(this.Facturas_Panel.Panel2, "Facturas_Panel.Panel2");
            this.Facturas_Panel.Panel2.Controls.Add(this.Facturas_DGW);
            this.ErrorMng_EP.SetError(this.Facturas_Panel.Panel2, resources.GetString("Facturas_Panel.Panel2.Error"));
            this.HelpProvider.SetHelpKeyword(this.Facturas_Panel.Panel2, resources.GetString("Facturas_Panel.Panel2.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Facturas_Panel.Panel2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Facturas_Panel.Panel2.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Facturas_Panel.Panel2, resources.GetString("Facturas_Panel.Panel2.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Facturas_Panel.Panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Facturas_Panel.Panel2.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Facturas_Panel.Panel2, ((int)(resources.GetObject("Facturas_Panel.Panel2.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Facturas_Panel.Panel2, ((bool)(resources.GetObject("Facturas_Panel.Panel2.ShowHelp"))));
            this.HelpProvider.SetShowHelp(this.Facturas_Panel, ((bool)(resources.GetObject("Facturas_Panel.ShowHelp"))));
            // 
            // Facturas_TS
            // 
            resources.ApplyResources(this.Facturas_TS, "Facturas_TS");
            this.ErrorMng_EP.SetError(this.Facturas_TS, resources.GetString("Facturas_TS.Error"));
            this.HelpProvider.SetHelpKeyword(this.Facturas_TS, resources.GetString("Facturas_TS.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Facturas_TS, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Facturas_TS.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Facturas_TS, resources.GetString("Facturas_TS.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Facturas_TS, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Facturas_TS.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Facturas_TS, ((int)(resources.GetObject("Facturas_TS.IconPadding"))));
            this.Facturas_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Facturas_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddFactura_TI,
            this.EditFactura_TI,
            this.ViewFactura_TI,
            this.DeleteFactura_TI,
            this.toolStripLabel1});
            this.Facturas_TS.Name = "Facturas_TS";
            this.HelpProvider.SetShowHelp(this.Facturas_TS, ((bool)(resources.GetObject("Facturas_TS.ShowHelp"))));
            // 
            // AddFactura_TI
            // 
            resources.ApplyResources(this.AddFactura_TI, "AddFactura_TI");
            this.AddFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddFactura_TI.Image = global::moleQule.Face.Invoice.Properties.Resources.item_add;
            this.AddFactura_TI.Name = "AddFactura_TI";
            // 
            // EditFactura_TI
            // 
            resources.ApplyResources(this.EditFactura_TI, "EditFactura_TI");
            this.EditFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EditFactura_TI.Image = global::moleQule.Face.Invoice.Properties.Resources.item_edit;
            this.EditFactura_TI.Name = "EditFactura_TI";
            this.EditFactura_TI.Click += new System.EventHandler(this.EditFactura_TI_Click);
            // 
            // ViewFactura_TI
            // 
            resources.ApplyResources(this.ViewFactura_TI, "ViewFactura_TI");
            this.ViewFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewFactura_TI.Image = global::moleQule.Face.Invoice.Properties.Resources.item_view;
            this.ViewFactura_TI.Name = "ViewFactura_TI";
            this.ViewFactura_TI.Click += new System.EventHandler(this.ViewFactura_TI_Click);
            // 
            // DeleteFactura_TI
            // 
            resources.ApplyResources(this.DeleteFactura_TI, "DeleteFactura_TI");
            this.DeleteFactura_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeleteFactura_TI.Image = global::moleQule.Face.Invoice.Properties.Resources.item_delete;
            this.DeleteFactura_TI.Name = "DeleteFactura_TI";
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // Facturas_DGW
            // 
            resources.ApplyResources(this.Facturas_DGW, "Facturas_DGW");
            this.Facturas_DGW.AllowUserToAddRows = false;
            this.Facturas_DGW.AllowUserToDeleteRows = false;
            this.Facturas_DGW.AllowUserToOrderColumns = true;
            this.Facturas_DGW.AutoGenerateColumns = false;
            this.Facturas_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumeroSerie,
            this.NFactura,
            this.Fecha,
            this.DiasTranscurridos,
            this.TotalFactura,
            this.Cobrado,
            this.Pendiente,
            this.Acumulado,
            this.Asignacion,
            this.FechaAsignacion,
            this.Vinculado});
            this.Facturas_DGW.DataSource = this.Datos_Facturas;
            this.ErrorMng_EP.SetError(this.Facturas_DGW, resources.GetString("Facturas_DGW.Error"));
            this.HelpProvider.SetHelpKeyword(this.Facturas_DGW, resources.GetString("Facturas_DGW.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Facturas_DGW, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Facturas_DGW.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Facturas_DGW, resources.GetString("Facturas_DGW.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Facturas_DGW, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Facturas_DGW.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Facturas_DGW, ((int)(resources.GetObject("Facturas_DGW.IconPadding"))));
            this.Facturas_DGW.MultiSelect = false;
            this.Facturas_DGW.Name = "Facturas_DGW";
            this.Facturas_DGW.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.Facturas_DGW, ((bool)(resources.GetObject("Facturas_DGW.ShowHelp"))));
            this.Facturas_DGW.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Facturas_DGW_CellClick);
            this.Facturas_DGW.DoubleClick += new System.EventHandler(this.Facturas_DGW_DoubleClick);
            // 
            // NumeroSerie
            // 
            this.NumeroSerie.DataPropertyName = "NumeroSerie";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NumeroSerie.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.NumeroSerie, "NumeroSerie");
            this.NumeroSerie.Name = "NumeroSerie";
            this.NumeroSerie.ReadOnly = true;
            // 
            // NFactura
            // 
            this.NFactura.DataPropertyName = "Codigo";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NFactura.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.NFactura, "NFactura");
            this.NFactura.Name = "NFactura";
            this.NFactura.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.DataPropertyName = "Fecha";
            resources.ApplyResources(this.Fecha, "Fecha");
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // DiasTranscurridos
            // 
            this.DiasTranscurridos.DataPropertyName = "DiasTranscurridos";
            resources.ApplyResources(this.DiasTranscurridos, "DiasTranscurridos");
            this.DiasTranscurridos.Name = "DiasTranscurridos";
            this.DiasTranscurridos.ReadOnly = true;
            // 
            // TotalFactura
            // 
            this.TotalFactura.DataPropertyName = "Total";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N2";
            dataGridViewCellStyle11.NullValue = null;
            this.TotalFactura.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.TotalFactura, "TotalFactura");
            this.TotalFactura.Name = "TotalFactura";
            this.TotalFactura.ReadOnly = true;
            // 
            // Cobrado
            // 
            this.Cobrado.DataPropertyName = "Cobrado";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            dataGridViewCellStyle12.NullValue = null;
            this.Cobrado.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.Cobrado, "Cobrado");
            this.Cobrado.Name = "Cobrado";
            this.Cobrado.ReadOnly = true;
            // 
            // Pendiente
            // 
            this.Pendiente.DataPropertyName = "PendienteVencido";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N2";
            dataGridViewCellStyle13.NullValue = null;
            this.Pendiente.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(this.Pendiente, "Pendiente");
            this.Pendiente.Name = "Pendiente";
            this.Pendiente.ReadOnly = true;
            // 
            // Acumulado
            // 
            this.Acumulado.DataPropertyName = "Acumulado";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            this.Acumulado.DefaultCellStyle = dataGridViewCellStyle14;
            resources.ApplyResources(this.Acumulado, "Acumulado");
            this.Acumulado.Name = "Acumulado";
            this.Acumulado.ReadOnly = true;
            // 
            // Asignacion
            // 
            this.Asignacion.DataPropertyName = "Asignado";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle15.Format = "N2";
            dataGridViewCellStyle15.NullValue = null;
            this.Asignacion.DefaultCellStyle = dataGridViewCellStyle15;
            resources.ApplyResources(this.Asignacion, "Asignacion");
            this.Asignacion.Name = "Asignacion";
            this.Asignacion.ReadOnly = true;
            // 
            // FechaAsignacion
            // 
            this.FechaAsignacion.DataPropertyName = "FechaAsignacion";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle16.Format = "d";
            this.FechaAsignacion.DefaultCellStyle = dataGridViewCellStyle16;
            resources.ApplyResources(this.FechaAsignacion, "FechaAsignacion");
            this.FechaAsignacion.Name = "FechaAsignacion";
            this.FechaAsignacion.ReadOnly = true;
            // 
            // Vinculado
            // 
            this.Vinculado.DataPropertyName = "Vinculado";
            resources.ApplyResources(this.Vinculado, "Vinculado");
            this.Vinculado.Name = "Vinculado";
            this.Vinculado.ReadOnly = true;
            this.Vinculado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Datos_Facturas
            // 
            this.Datos_Facturas.DataSource = typeof(moleQule.Library.Invoice.OutputInvoiceInfo);
            // 
            // tipoLabel
            // 
            resources.ApplyResources(tipoLabel, "tipoLabel");
            this.ErrorMng_EP.SetError(tipoLabel, resources.GetString("tipoLabel.Error"));
            this.HelpProvider.SetHelpKeyword(tipoLabel, resources.GetString("tipoLabel.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(tipoLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("tipoLabel.HelpNavigator"))));
            this.HelpProvider.SetHelpString(tipoLabel, resources.GetString("tipoLabel.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(tipoLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("tipoLabel.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(tipoLabel, ((int)(resources.GetObject("tipoLabel.IconPadding"))));
            tipoLabel.Name = "tipoLabel";
            this.HelpProvider.SetShowHelp(tipoLabel, ((bool)(resources.GetObject("tipoLabel.ShowHelp"))));
            // 
            // observacionesLabel
            // 
            resources.ApplyResources(observacionesLabel, "observacionesLabel");
            this.ErrorMng_EP.SetError(observacionesLabel, resources.GetString("observacionesLabel.Error"));
            this.HelpProvider.SetHelpKeyword(observacionesLabel, resources.GetString("observacionesLabel.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(observacionesLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("observacionesLabel.HelpNavigator"))));
            this.HelpProvider.SetHelpString(observacionesLabel, resources.GetString("observacionesLabel.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(observacionesLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("observacionesLabel.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(observacionesLabel, ((int)(resources.GetObject("observacionesLabel.IconPadding"))));
            observacionesLabel.Name = "observacionesLabel";
            this.HelpProvider.SetShowHelp(observacionesLabel, ((bool)(resources.GetObject("observacionesLabel.ShowHelp"))));
            // 
            // cuentaLabel
            // 
            resources.ApplyResources(cuentaLabel, "cuentaLabel");
            this.ErrorMng_EP.SetError(cuentaLabel, resources.GetString("cuentaLabel.Error"));
            this.HelpProvider.SetHelpKeyword(cuentaLabel, resources.GetString("cuentaLabel.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(cuentaLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("cuentaLabel.HelpNavigator"))));
            this.HelpProvider.SetHelpString(cuentaLabel, resources.GetString("cuentaLabel.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(cuentaLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cuentaLabel.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(cuentaLabel, ((int)(resources.GetObject("cuentaLabel.IconPadding"))));
            cuentaLabel.Name = "cuentaLabel";
            this.HelpProvider.SetShowHelp(cuentaLabel, ((bool)(resources.GetObject("cuentaLabel.ShowHelp"))));
            // 
            // importeLabel
            // 
            resources.ApplyResources(importeLabel, "importeLabel");
            this.ErrorMng_EP.SetError(importeLabel, resources.GetString("importeLabel.Error"));
            this.HelpProvider.SetHelpKeyword(importeLabel, resources.GetString("importeLabel.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(importeLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("importeLabel.HelpNavigator"))));
            this.HelpProvider.SetHelpString(importeLabel, resources.GetString("importeLabel.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(importeLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("importeLabel.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(importeLabel, ((int)(resources.GetObject("importeLabel.IconPadding"))));
            importeLabel.Name = "importeLabel";
            this.HelpProvider.SetShowHelp(importeLabel, ((bool)(resources.GetObject("importeLabel.ShowHelp"))));
            // 
            // vencimientoLabel
            // 
            resources.ApplyResources(vencimientoLabel, "vencimientoLabel");
            this.ErrorMng_EP.SetError(vencimientoLabel, resources.GetString("vencimientoLabel.Error"));
            this.HelpProvider.SetHelpKeyword(vencimientoLabel, resources.GetString("vencimientoLabel.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(vencimientoLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("vencimientoLabel.HelpNavigator"))));
            this.HelpProvider.SetHelpString(vencimientoLabel, resources.GetString("vencimientoLabel.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(vencimientoLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("vencimientoLabel.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(vencimientoLabel, ((int)(resources.GetObject("vencimientoLabel.IconPadding"))));
            vencimientoLabel.Name = "vencimientoLabel";
            this.HelpProvider.SetShowHelp(vencimientoLabel, ((bool)(resources.GetObject("vencimientoLabel.ShowHelp"))));
            // 
            // fechaLabel
            // 
            resources.ApplyResources(fechaLabel, "fechaLabel");
            this.ErrorMng_EP.SetError(fechaLabel, resources.GetString("fechaLabel.Error"));
            this.HelpProvider.SetHelpKeyword(fechaLabel, resources.GetString("fechaLabel.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(fechaLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("fechaLabel.HelpNavigator"))));
            this.HelpProvider.SetHelpString(fechaLabel, resources.GetString("fechaLabel.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(fechaLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("fechaLabel.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(fechaLabel, ((int)(resources.GetObject("fechaLabel.IconPadding"))));
            fechaLabel.Name = "fechaLabel";
            this.HelpProvider.SetShowHelp(fechaLabel, ((bool)(resources.GetObject("fechaLabel.ShowHelp"))));
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            this.ErrorMng_EP.SetError(label5, resources.GetString("label5.Error"));
            this.HelpProvider.SetHelpKeyword(label5, resources.GetString("label5.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(label5, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label5.HelpNavigator"))));
            this.HelpProvider.SetHelpString(label5, resources.GetString("label5.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(label5, ((int)(resources.GetObject("label5.IconPadding"))));
            label5.Name = "label5";
            this.HelpProvider.SetShowHelp(label5, ((bool)(resources.GetObject("label5.ShowHelp"))));
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            this.ErrorMng_EP.SetError(label6, resources.GetString("label6.Error"));
            this.HelpProvider.SetHelpKeyword(label6, resources.GetString("label6.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(label6, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label6.HelpNavigator"))));
            this.HelpProvider.SetHelpString(label6, resources.GetString("label6.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(label6, ((int)(resources.GetObject("label6.IconPadding"))));
            label6.Name = "label6";
            this.HelpProvider.SetShowHelp(label6, ((bool)(resources.GetObject("label6.ShowHelp"))));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            this.ErrorMng_EP.SetError(label2, resources.GetString("label2.Error"));
            this.HelpProvider.SetHelpKeyword(label2, resources.GetString("label2.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(label2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label2.HelpNavigator"))));
            this.HelpProvider.SetHelpString(label2, resources.GetString("label2.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(label2, ((int)(resources.GetObject("label2.IconPadding"))));
            label2.Name = "label2";
            this.HelpProvider.SetShowHelp(label2, ((bool)(resources.GetObject("label2.ShowHelp"))));
            // 
            // Facturas_SC
            // 
            resources.ApplyResources(this.Facturas_SC, "Facturas_SC");
            this.ErrorMng_EP.SetError(this.Facturas_SC, resources.GetString("Facturas_SC.Error"));
            this.HelpProvider.SetHelpKeyword(this.Facturas_SC, resources.GetString("Facturas_SC.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Facturas_SC, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Facturas_SC.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Facturas_SC, resources.GetString("Facturas_SC.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Facturas_SC, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Facturas_SC.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Facturas_SC, ((int)(resources.GetObject("Facturas_SC.IconPadding"))));
            this.Facturas_SC.Name = "Facturas_SC";
            // 
            // Facturas_SC.Panel1
            // 
            resources.ApplyResources(this.Facturas_SC.Panel1, "Facturas_SC.Panel1");
            this.Facturas_SC.Panel1.Controls.Add(this.Facturas_Panel);
            this.ErrorMng_EP.SetError(this.Facturas_SC.Panel1, resources.GetString("Facturas_SC.Panel1.Error"));
            this.HelpProvider.SetHelpKeyword(this.Facturas_SC.Panel1, resources.GetString("Facturas_SC.Panel1.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Facturas_SC.Panel1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Facturas_SC.Panel1.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Facturas_SC.Panel1, resources.GetString("Facturas_SC.Panel1.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Facturas_SC.Panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Facturas_SC.Panel1.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Facturas_SC.Panel1, ((int)(resources.GetObject("Facturas_SC.Panel1.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Facturas_SC.Panel1, ((bool)(resources.GetObject("Facturas_SC.Panel1.ShowHelp"))));
            // 
            // Facturas_SC.Panel2
            // 
            resources.ApplyResources(this.Facturas_SC.Panel2, "Facturas_SC.Panel2");
            this.ErrorMng_EP.SetError(this.Facturas_SC.Panel2, resources.GetString("Facturas_SC.Panel2.Error"));
            this.HelpProvider.SetHelpKeyword(this.Facturas_SC.Panel2, resources.GetString("Facturas_SC.Panel2.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Facturas_SC.Panel2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Facturas_SC.Panel2.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Facturas_SC.Panel2, resources.GetString("Facturas_SC.Panel2.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Facturas_SC.Panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Facturas_SC.Panel2.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Facturas_SC.Panel2, ((int)(resources.GetObject("Facturas_SC.Panel2.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Facturas_SC.Panel2, ((bool)(resources.GetObject("Facturas_SC.Panel2.ShowHelp"))));
            this.Facturas_SC.Panel2Collapsed = true;
            this.HelpProvider.SetShowHelp(this.Facturas_SC, ((bool)(resources.GetObject("Facturas_SC.ShowHelp"))));
            // 
            // Content_SC
            // 
            resources.ApplyResources(this.Content_SC, "Content_SC");
            this.ErrorMng_EP.SetError(this.Content_SC, resources.GetString("Content_SC.Error"));
            this.Content_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.HelpProvider.SetHelpKeyword(this.Content_SC, resources.GetString("Content_SC.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Content_SC, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Content_SC.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Content_SC, resources.GetString("Content_SC.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Content_SC, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Content_SC.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Content_SC, ((int)(resources.GetObject("Content_SC.IconPadding"))));
            this.Content_SC.Name = "Content_SC";
            // 
            // Content_SC.Panel1
            // 
            resources.ApplyResources(this.Content_SC.Panel1, "Content_SC.Panel1");
            this.Content_SC.Panel1.Controls.Add(this.Source_GB);
            this.ErrorMng_EP.SetError(this.Content_SC.Panel1, resources.GetString("Content_SC.Panel1.Error"));
            this.HelpProvider.SetHelpKeyword(this.Content_SC.Panel1, resources.GetString("Content_SC.Panel1.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Content_SC.Panel1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Content_SC.Panel1.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Content_SC.Panel1, resources.GetString("Content_SC.Panel1.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Content_SC.Panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Content_SC.Panel1.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Content_SC.Panel1, ((int)(resources.GetObject("Content_SC.Panel1.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Content_SC.Panel1, ((bool)(resources.GetObject("Content_SC.Panel1.ShowHelp"))));
            // 
            // Content_SC.Panel2
            // 
            resources.ApplyResources(this.Content_SC.Panel2, "Content_SC.Panel2");
            this.Content_SC.Panel2.Controls.Add(this.Facturas_GB);
            this.ErrorMng_EP.SetError(this.Content_SC.Panel2, resources.GetString("Content_SC.Panel2.Error"));
            this.HelpProvider.SetHelpKeyword(this.Content_SC.Panel2, resources.GetString("Content_SC.Panel2.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Content_SC.Panel2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Content_SC.Panel2.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Content_SC.Panel2, resources.GetString("Content_SC.Panel2.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Content_SC.Panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Content_SC.Panel2.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Content_SC.Panel2, ((int)(resources.GetObject("Content_SC.Panel2.IconPadding"))));
            this.HelpProvider.SetShowHelp(this.Content_SC.Panel2, ((bool)(resources.GetObject("Content_SC.Panel2.ShowHelp"))));
            this.HelpProvider.SetShowHelp(this.Content_SC, ((bool)(resources.GetObject("Content_SC.ShowHelp"))));
            // 
            // Facturas_GB
            // 
            resources.ApplyResources(this.Facturas_GB, "Facturas_GB");
            this.Facturas_GB.Controls.Add(this.Facturas_SC);
            this.ErrorMng_EP.SetError(this.Facturas_GB, resources.GetString("Facturas_GB.Error"));
            this.HelpProvider.SetHelpKeyword(this.Facturas_GB, resources.GetString("Facturas_GB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Facturas_GB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Facturas_GB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Facturas_GB, resources.GetString("Facturas_GB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Facturas_GB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Facturas_GB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Facturas_GB, ((int)(resources.GetObject("Facturas_GB.IconPadding"))));
            this.Facturas_GB.Name = "Facturas_GB";
            this.HelpProvider.SetShowHelp(this.Facturas_GB, ((bool)(resources.GetObject("Facturas_GB.ShowHelp"))));
            this.Facturas_GB.TabStop = false;
            // 
            // Observaciones_RTB
            // 
            resources.ApplyResources(this.Observaciones_RTB, "Observaciones_RTB");
            this.Observaciones_RTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.ErrorMng_EP.SetError(this.Observaciones_RTB, resources.GetString("Observaciones_RTB.Error"));
            this.HelpProvider.SetHelpKeyword(this.Observaciones_RTB, resources.GetString("Observaciones_RTB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Observaciones_RTB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Observaciones_RTB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Observaciones_RTB, resources.GetString("Observaciones_RTB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Observaciones_RTB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Observaciones_RTB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Observaciones_RTB, ((int)(resources.GetObject("Observaciones_RTB.IconPadding"))));
            this.Observaciones_RTB.Name = "Observaciones_RTB";
            this.HelpProvider.SetShowHelp(this.Observaciones_RTB, ((bool)(resources.GetObject("Observaciones_RTB.ShowHelp"))));
            // 
            // Cuenta_TB
            // 
            resources.ApplyResources(this.Cuenta_TB, "Cuenta_TB");
            this.Cuenta_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaBancaria", true));
            this.ErrorMng_EP.SetError(this.Cuenta_TB, resources.GetString("Cuenta_TB.Error"));
            this.HelpProvider.SetHelpKeyword(this.Cuenta_TB, resources.GetString("Cuenta_TB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Cuenta_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Cuenta_TB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Cuenta_TB, resources.GetString("Cuenta_TB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Cuenta_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Cuenta_TB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Cuenta_TB, ((int)(resources.GetObject("Cuenta_TB.IconPadding"))));
            this.Cuenta_TB.Name = "Cuenta_TB";
            this.Cuenta_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.Cuenta_TB, ((bool)(resources.GetObject("Cuenta_TB.ShowHelp"))));
            this.Cuenta_TB.TabStop = false;
            // 
            // Cuenta_BT
            // 
            resources.ApplyResources(this.Cuenta_BT, "Cuenta_BT");
            this.ErrorMng_EP.SetError(this.Cuenta_BT, resources.GetString("Cuenta_BT.Error"));
            this.HelpProvider.SetHelpKeyword(this.Cuenta_BT, resources.GetString("Cuenta_BT.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Cuenta_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Cuenta_BT.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Cuenta_BT, resources.GetString("Cuenta_BT.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Cuenta_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Cuenta_BT.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Cuenta_BT, ((int)(resources.GetObject("Cuenta_BT.IconPadding"))));
            this.Cuenta_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.Cuenta_BT.Name = "Cuenta_BT";
            this.HelpProvider.SetShowHelp(this.Cuenta_BT, ((bool)(resources.GetObject("Cuenta_BT.ShowHelp"))));
            this.Cuenta_BT.UseVisualStyleBackColor = true;
            this.Cuenta_BT.Click += new System.EventHandler(this.Cuenta_BT_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.ErrorMng_EP.SetError(this.label1, resources.GetString("label1.Error"));
            this.HelpProvider.SetHelpKeyword(this.label1, resources.GetString("label1.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.label1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label1.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.label1, resources.GetString("label1.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            this.HelpProvider.SetShowHelp(this.label1, ((bool)(resources.GetObject("label1.ShowHelp"))));
            // 
            // NoAsignado_TB
            // 
            resources.ApplyResources(this.NoAsignado_TB, "NoAsignado_TB");
            this.NoAsignado_TB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ErrorMng_EP.SetError(this.NoAsignado_TB, resources.GetString("NoAsignado_TB.Error"));
            this.HelpProvider.SetHelpKeyword(this.NoAsignado_TB, resources.GetString("NoAsignado_TB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.NoAsignado_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("NoAsignado_TB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.NoAsignado_TB, resources.GetString("NoAsignado_TB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.NoAsignado_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("NoAsignado_TB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.NoAsignado_TB, ((int)(resources.GetObject("NoAsignado_TB.IconPadding"))));
            this.NoAsignado_TB.Name = "NoAsignado_TB";
            this.NoAsignado_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.NoAsignado_TB, ((bool)(resources.GetObject("NoAsignado_TB.ShowHelp"))));
            this.NoAsignado_TB.TabStop = false;
            // 
            // Importe_NTB
            // 
            resources.ApplyResources(this.Importe_NTB, "Importe_NTB");
            this.Importe_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Importe", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.ErrorMng_EP.SetError(this.Importe_NTB, resources.GetString("Importe_NTB.Error"));
            this.HelpProvider.SetHelpKeyword(this.Importe_NTB, resources.GetString("Importe_NTB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Importe_NTB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Importe_NTB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Importe_NTB, resources.GetString("Importe_NTB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Importe_NTB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Importe_NTB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Importe_NTB, ((int)(resources.GetObject("Importe_NTB.IconPadding"))));
            this.Importe_NTB.Name = "Importe_NTB";
            this.HelpProvider.SetShowHelp(this.Importe_NTB, ((bool)(resources.GetObject("Importe_NTB.ShowHelp"))));
            this.Importe_NTB.TextIsCurrency = false;
            this.Importe_NTB.TextIsInteger = false;
            this.Importe_NTB.Validated += new System.EventHandler(this.Importe_NTB_Validated);
            // 
            // Vencimiento_DTP
            // 
            resources.ApplyResources(this.Vencimiento_DTP, "Vencimiento_DTP");
            this.ErrorMng_EP.SetError(this.Vencimiento_DTP, resources.GetString("Vencimiento_DTP.Error"));
            this.Vencimiento_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.HelpProvider.SetHelpKeyword(this.Vencimiento_DTP, resources.GetString("Vencimiento_DTP.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Vencimiento_DTP, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Vencimiento_DTP.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Vencimiento_DTP, resources.GetString("Vencimiento_DTP.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Vencimiento_DTP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Vencimiento_DTP.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Vencimiento_DTP, ((int)(resources.GetObject("Vencimiento_DTP.IconPadding"))));
            this.Vencimiento_DTP.Name = "Vencimiento_DTP";
            this.HelpProvider.SetShowHelp(this.Vencimiento_DTP, ((bool)(resources.GetObject("Vencimiento_DTP.ShowHelp"))));
            // 
            // Fecha_DTP
            // 
            resources.ApplyResources(this.Fecha_DTP, "Fecha_DTP");
            this.ErrorMng_EP.SetError(this.Fecha_DTP, resources.GetString("Fecha_DTP.Error"));
            this.Fecha_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HelpProvider.SetHelpKeyword(this.Fecha_DTP, resources.GetString("Fecha_DTP.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Fecha_DTP, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Fecha_DTP.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Fecha_DTP, resources.GetString("Fecha_DTP.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Fecha_DTP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Fecha_DTP.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Fecha_DTP, ((int)(resources.GetObject("Fecha_DTP.IconPadding"))));
            this.Fecha_DTP.Name = "Fecha_DTP";
            this.HelpProvider.SetShowHelp(this.Fecha_DTP, ((bool)(resources.GetObject("Fecha_DTP.ShowHelp"))));
            // 
            // Liberar_BT
            // 
            resources.ApplyResources(this.Liberar_BT, "Liberar_BT");
            this.ErrorMng_EP.SetError(this.Liberar_BT, resources.GetString("Liberar_BT.Error"));
            this.HelpProvider.SetHelpKeyword(this.Liberar_BT, resources.GetString("Liberar_BT.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Liberar_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Liberar_BT.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Liberar_BT, resources.GetString("Liberar_BT.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Liberar_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Liberar_BT.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Liberar_BT, ((int)(resources.GetObject("Liberar_BT.IconPadding"))));
            this.Liberar_BT.Name = "Liberar_BT";
            this.HelpProvider.SetShowHelp(this.Liberar_BT, ((bool)(resources.GetObject("Liberar_BT.ShowHelp"))));
            this.Liberar_BT.Tag = "NO FORMAT";
            this.Liberar_BT.UseVisualStyleBackColor = true;
            this.Liberar_BT.Click += new System.EventHandler(this.Liberar_BT_Click);
            // 
            // Repartir_BT
            // 
            resources.ApplyResources(this.Repartir_BT, "Repartir_BT");
            this.ErrorMng_EP.SetError(this.Repartir_BT, resources.GetString("Repartir_BT.Error"));
            this.HelpProvider.SetHelpKeyword(this.Repartir_BT, resources.GetString("Repartir_BT.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.Repartir_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Repartir_BT.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.Repartir_BT, resources.GetString("Repartir_BT.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.Repartir_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Repartir_BT.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.Repartir_BT, ((int)(resources.GetObject("Repartir_BT.IconPadding"))));
            this.Repartir_BT.Name = "Repartir_BT";
            this.HelpProvider.SetShowHelp(this.Repartir_BT, ((bool)(resources.GetObject("Repartir_BT.ShowHelp"))));
            this.Repartir_BT.Tag = "NO FORMAT";
            this.Repartir_BT.UseVisualStyleBackColor = true;
            this.Repartir_BT.Click += new System.EventHandler(this.Repartir_BT_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.ErrorMng_EP.SetError(this.label3, resources.GetString("label3.Error"));
            this.HelpProvider.SetHelpKeyword(this.label3, resources.GetString("label3.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.label3, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label3.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.label3, resources.GetString("label3.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            this.HelpProvider.SetShowHelp(this.label3, ((bool)(resources.GetObject("label3.ShowHelp"))));
            // 
            // ID_TB
            // 
            resources.ApplyResources(this.ID_TB, "ID_TB");
            this.ID_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
            this.ErrorMng_EP.SetError(this.ID_TB, resources.GetString("ID_TB.Error"));
            this.HelpProvider.SetHelpKeyword(this.ID_TB, resources.GetString("ID_TB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.ID_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("ID_TB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.ID_TB, resources.GetString("ID_TB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.ID_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("ID_TB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.ID_TB, ((int)(resources.GetObject("ID_TB.IconPadding"))));
            this.ID_TB.Name = "ID_TB";
            this.ID_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.ID_TB, ((bool)(resources.GetObject("ID_TB.ShowHelp"))));
            this.ID_TB.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.ErrorMng_EP.SetError(this.label4, resources.GetString("label4.Error"));
            this.HelpProvider.SetHelpKeyword(this.label4, resources.GetString("label4.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.label4, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label4.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.label4, resources.GetString("label4.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
            this.label4.Name = "label4";
            this.HelpProvider.SetShowHelp(this.label4, ((bool)(resources.GetObject("label4.ShowHelp"))));
            // 
            // NCobro_TB
            // 
            resources.ApplyResources(this.NCobro_TB, "NCobro_TB");
            this.NCobro_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "IDCobroLabel", true));
            this.ErrorMng_EP.SetError(this.NCobro_TB, resources.GetString("NCobro_TB.Error"));
            this.HelpProvider.SetHelpKeyword(this.NCobro_TB, resources.GetString("NCobro_TB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.NCobro_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("NCobro_TB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.NCobro_TB, resources.GetString("NCobro_TB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.NCobro_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("NCobro_TB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.NCobro_TB, ((int)(resources.GetObject("NCobro_TB.IconPadding"))));
            this.NCobro_TB.Name = "NCobro_TB";
            this.NCobro_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.NCobro_TB, ((bool)(resources.GetObject("NCobro_TB.ShowHelp"))));
            this.NCobro_TB.TabStop = false;
            // 
            // TPV_BT
            // 
            resources.ApplyResources(this.TPV_BT, "TPV_BT");
            this.ErrorMng_EP.SetError(this.TPV_BT, resources.GetString("TPV_BT.Error"));
            this.HelpProvider.SetHelpKeyword(this.TPV_BT, resources.GetString("TPV_BT.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.TPV_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("TPV_BT.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.TPV_BT, resources.GetString("TPV_BT.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.TPV_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("TPV_BT.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.TPV_BT, ((int)(resources.GetObject("TPV_BT.IconPadding"))));
            this.TPV_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.TPV_BT.Name = "TPV_BT";
            this.HelpProvider.SetShowHelp(this.TPV_BT, ((bool)(resources.GetObject("TPV_BT.ShowHelp"))));
            this.TPV_BT.UseVisualStyleBackColor = true;
            this.TPV_BT.Click += new System.EventHandler(this.TPV_BT_Click);
            // 
            // TPV_TB
            // 
            resources.ApplyResources(this.TPV_TB, "TPV_TB");
            this.TPV_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "TPV", true));
            this.ErrorMng_EP.SetError(this.TPV_TB, resources.GetString("TPV_TB.Error"));
            this.HelpProvider.SetHelpKeyword(this.TPV_TB, resources.GetString("TPV_TB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.TPV_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("TPV_TB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.TPV_TB, resources.GetString("TPV_TB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.TPV_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("TPV_TB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.TPV_TB, ((int)(resources.GetObject("TPV_TB.IconPadding"))));
            this.TPV_TB.Name = "TPV_TB";
            this.TPV_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.TPV_TB, ((bool)(resources.GetObject("TPV_TB.ShowHelp"))));
            this.TPV_TB.TabStop = false;
            // 
            // GastosBancarios_NTB
            // 
            resources.ApplyResources(this.GastosBancarios_NTB, "GastosBancarios_NTB");
            this.GastosBancarios_NTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "GastosBancarios", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation, null, "N2"));
            this.ErrorMng_EP.SetError(this.GastosBancarios_NTB, resources.GetString("GastosBancarios_NTB.Error"));
            this.HelpProvider.SetHelpKeyword(this.GastosBancarios_NTB, resources.GetString("GastosBancarios_NTB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.GastosBancarios_NTB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("GastosBancarios_NTB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.GastosBancarios_NTB, resources.GetString("GastosBancarios_NTB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.GastosBancarios_NTB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("GastosBancarios_NTB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.GastosBancarios_NTB, ((int)(resources.GetObject("GastosBancarios_NTB.IconPadding"))));
            this.GastosBancarios_NTB.Name = "GastosBancarios_NTB";
            this.HelpProvider.SetShowHelp(this.GastosBancarios_NTB, ((bool)(resources.GetObject("GastosBancarios_NTB.ShowHelp"))));
            this.GastosBancarios_NTB.TextIsCurrency = false;
            this.GastosBancarios_NTB.TextIsInteger = false;
            // 
            // MedioPago_BT
            // 
            resources.ApplyResources(this.MedioPago_BT, "MedioPago_BT");
            this.ErrorMng_EP.SetError(this.MedioPago_BT, resources.GetString("MedioPago_BT.Error"));
            this.HelpProvider.SetHelpKeyword(this.MedioPago_BT, resources.GetString("MedioPago_BT.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.MedioPago_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("MedioPago_BT.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.MedioPago_BT, resources.GetString("MedioPago_BT.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.MedioPago_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("MedioPago_BT.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.MedioPago_BT, ((int)(resources.GetObject("MedioPago_BT.IconPadding"))));
            this.MedioPago_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.MedioPago_BT.Name = "MedioPago_BT";
            this.HelpProvider.SetShowHelp(this.MedioPago_BT, ((bool)(resources.GetObject("MedioPago_BT.ShowHelp"))));
            this.MedioPago_BT.UseVisualStyleBackColor = true;
            this.MedioPago_BT.Click += new System.EventHandler(this.MedioPago_BT_Click);
            // 
            // MedioPago_TB
            // 
            resources.ApplyResources(this.MedioPago_TB, "MedioPago_TB");
            this.MedioPago_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "EMedioPagoLabel", true));
            this.ErrorMng_EP.SetError(this.MedioPago_TB, resources.GetString("MedioPago_TB.Error"));
            this.HelpProvider.SetHelpKeyword(this.MedioPago_TB, resources.GetString("MedioPago_TB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.MedioPago_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("MedioPago_TB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.MedioPago_TB, resources.GetString("MedioPago_TB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.MedioPago_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("MedioPago_TB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.MedioPago_TB, ((int)(resources.GetObject("MedioPago_TB.IconPadding"))));
            this.MedioPago_TB.Name = "MedioPago_TB";
            this.MedioPago_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.MedioPago_TB, ((bool)(resources.GetObject("MedioPago_TB.ShowHelp"))));
            this.MedioPago_TB.TabStop = false;
            // 
            // EstadoCobro_TB
            // 
            resources.ApplyResources(this.EstadoCobro_TB, "EstadoCobro_TB");
            this.EstadoCobro_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "EstadoCobroLabel", true));
            this.ErrorMng_EP.SetError(this.EstadoCobro_TB, resources.GetString("EstadoCobro_TB.Error"));
            this.HelpProvider.SetHelpKeyword(this.EstadoCobro_TB, resources.GetString("EstadoCobro_TB.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.EstadoCobro_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("EstadoCobro_TB.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.EstadoCobro_TB, resources.GetString("EstadoCobro_TB.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.EstadoCobro_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("EstadoCobro_TB.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.EstadoCobro_TB, ((int)(resources.GetObject("EstadoCobro_TB.IconPadding"))));
            this.EstadoCobro_TB.Name = "EstadoCobro_TB";
            this.EstadoCobro_TB.ReadOnly = true;
            this.HelpProvider.SetShowHelp(this.EstadoCobro_TB, ((bool)(resources.GetObject("EstadoCobro_TB.ShowHelp"))));
            this.EstadoCobro_TB.TabStop = false;
            // 
            // EstadoCobro_BT
            // 
            resources.ApplyResources(this.EstadoCobro_BT, "EstadoCobro_BT");
            this.ErrorMng_EP.SetError(this.EstadoCobro_BT, resources.GetString("EstadoCobro_BT.Error"));
            this.HelpProvider.SetHelpKeyword(this.EstadoCobro_BT, resources.GetString("EstadoCobro_BT.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this.EstadoCobro_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("EstadoCobro_BT.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this.EstadoCobro_BT, resources.GetString("EstadoCobro_BT.HelpString"));
            this.ErrorMng_EP.SetIconAlignment(this.EstadoCobro_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("EstadoCobro_BT.IconAlignment"))));
            this.ErrorMng_EP.SetIconPadding(this.EstadoCobro_BT, ((int)(resources.GetObject("EstadoCobro_BT.IconPadding"))));
            this.EstadoCobro_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.select_16;
            this.EstadoCobro_BT.Name = "EstadoCobro_BT";
            this.HelpProvider.SetShowHelp(this.EstadoCobro_BT, ((bool)(resources.GetObject("EstadoCobro_BT.ShowHelp"))));
            this.EstadoCobro_BT.UseVisualStyleBackColor = true;
            this.EstadoCobro_BT.Click += new System.EventHandler(this.EstadoCobro_BT_Click);
            // 
            // CobroFacturaUIForm
            // 
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.HelpProvider.SetHelpString(this, resources.GetString("$this.HelpString"));
            this.Name = "CobroFacturaUIForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.ShowInTaskbar = false;
            this.Shown += new System.EventHandler(this.CobroFUIForm_Shown);
            this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
            this.Controls.SetChildIndex(this.PanelesV, 0);
            this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
            this.Controls.SetChildIndex(this.Progress_PB, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.Source_GB.ResumeLayout(false);
            this.Source_GB.PerformLayout();
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
            this.PanelesV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            this.ProgressBK_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            this.Facturas_Panel.Panel1.ResumeLayout(false);
            this.Facturas_Panel.Panel1.PerformLayout();
            this.Facturas_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Facturas_Panel)).EndInit();
            this.Facturas_Panel.ResumeLayout(false);
            this.Facturas_TS.ResumeLayout(false);
            this.Facturas_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Facturas_DGW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Facturas)).EndInit();
            this.Facturas_SC.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Facturas_SC)).EndInit();
            this.Facturas_SC.ResumeLayout(false);
            this.Content_SC.Panel1.ResumeLayout(false);
            this.Content_SC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Content_SC)).EndInit();
            this.Content_SC.ResumeLayout(false);
            this.Facturas_GB.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.RichTextBox Observaciones_RTB;
        protected System.Windows.Forms.TextBox Cuenta_TB;
		private System.Windows.Forms.Button Cuenta_BT;
        private System.Windows.Forms.GroupBox Facturas_GB;
        private System.Windows.Forms.SplitContainer Facturas_SC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NoAsignado_TB;
        protected moleQule.Face.Controls.NumericTextBox Importe_NTB;
        protected System.Windows.Forms.DateTimePicker Vencimiento_DTP;
        protected System.Windows.Forms.DateTimePicker Fecha_DTP;
		protected System.Windows.Forms.BindingSource Datos_Facturas;
        protected System.Windows.Forms.Button Liberar_BT;
		protected System.Windows.Forms.Button Repartir_BT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ID_TB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox NCobro_TB;
        private System.Windows.Forms.Button TPV_BT;
		protected System.Windows.Forms.TextBox TPV_TB;
		protected System.Windows.Forms.SplitContainer Facturas_Panel;
		protected System.Windows.Forms.ToolStrip Facturas_TS;
		protected System.Windows.Forms.ToolStripButton AddFactura_TI;
		protected System.Windows.Forms.ToolStripButton EditFactura_TI;
		protected System.Windows.Forms.ToolStripButton ViewFactura_TI;
		protected System.Windows.Forms.ToolStripButton DeleteFactura_TI;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		protected System.Windows.Forms.DataGridView Facturas_DGW;
		protected Controls.NumericTextBox GastosBancarios_NTB;
		protected System.Windows.Forms.TextBox MedioPago_TB;
		public System.Windows.Forms.Button MedioPago_BT;
		private System.Windows.Forms.SplitContainer Content_SC;
		private System.Windows.Forms.DataGridViewTextBoxColumn NumeroSerie;
		private System.Windows.Forms.DataGridViewTextBoxColumn NFactura;
		private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
		private System.Windows.Forms.DataGridViewTextBoxColumn DiasTranscurridos;
		private System.Windows.Forms.DataGridViewTextBoxColumn TotalFactura;
		private System.Windows.Forms.DataGridViewTextBoxColumn Cobrado;
		private System.Windows.Forms.DataGridViewTextBoxColumn Pendiente;
		private System.Windows.Forms.DataGridViewTextBoxColumn Acumulado;
		private System.Windows.Forms.DataGridViewTextBoxColumn Asignacion;
		private System.Windows.Forms.DataGridViewTextBoxColumn FechaAsignacion;
		private System.Windows.Forms.DataGridViewButtonColumn Vinculado;
        protected System.Windows.Forms.TextBox EstadoCobro_TB;
        public System.Windows.Forms.Button EstadoCobro_BT;

    }
}
