namespace moleQule.Face.Invoice
{
    partial class PreciosForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreciosForm));
            this.Datos_Precioss = new System.Windows.Forms.BindingSource(this.components);
            this.Excel_BT = new System.Windows.Forms.Button();
            this.Superior_SP = new System.Windows.Forms.SplitContainer();
            this.Precios_Tabla = new System.Windows.Forms.DataGridView();
            this.Leyenda_Panel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Up_LB = new System.Windows.Forms.Label();
            this.Down_LB = new System.Windows.Forms.Label();
            this.EQ_LB = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Precioss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Superior_SP)).BeginInit();
            this.Superior_SP.Panel1.SuspendLayout();
            this.Superior_SP.Panel2.SuspendLayout();
            this.Superior_SP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Precios_Tabla)).BeginInit();
            this.Leyenda_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Print_BT
            // 
            this.Print_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ErrorMng_EP.SetIconAlignment(this.Print_BT, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.Print_BT.Location = new System.Drawing.Point(251, 2);
            this.HelpProvider.SetShowHelp(this.Print_BT, true);
            // 
            // Submit_BT
            // 
            this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ErrorMng_EP.SetIconAlignment(this.Submit_BT, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.Submit_BT.Location = new System.Drawing.Point(136, 2);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            this.Submit_BT.Visible = false;
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ErrorMng_EP.SetIconAlignment(this.Cancel_BT, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.Cancel_BT.Location = new System.Drawing.Point(21, 2);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Source_GB
            // 
            this.Source_GB.Controls.Add(this.Superior_SP);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.Source_GB.Margin = new System.Windows.Forms.Padding(0);
            this.Source_GB.Padding = new System.Windows.Forms.Padding(0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(732, 271);
            this.Source_GB.Text = "";
            // 
            // PanelesV
            // 
            this.PanelesV.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.PanelesV.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.PanelesV.Panel2.Controls.Add(this.Excel_BT);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(734, 316);
            this.PanelesV.SplitterDistance = 276;
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(163, 24);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(734, 316);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(335, 209);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(335, 124);
            // 
            // Excel_BT
            // 
            this.Excel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Excel_BT.Image = global::moleQule.Face.Invoice.Properties.Resources.table_24;
            this.Excel_BT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Excel_BT.Location = new System.Drawing.Point(369, 2);
            this.Excel_BT.Name = "Excel_BT";
            this.Excel_BT.Size = new System.Drawing.Size(112, 32);
            this.Excel_BT.TabIndex = 204;
            this.Excel_BT.Text = "Excel";
            this.Excel_BT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Excel_BT.UseVisualStyleBackColor = true;
            this.Excel_BT.Click += new System.EventHandler(this.Excel_BT_Click);
            // 
            // Superior_SP
            // 
            this.Superior_SP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Superior_SP.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Superior_SP.Location = new System.Drawing.Point(0, 14);
            this.Superior_SP.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.Superior_SP.Name = "Superior_SP";
            this.Superior_SP.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Superior_SP.Panel1
            // 
            this.Superior_SP.Panel1.Controls.Add(this.Precios_Tabla);
            // 
            // Superior_SP.Panel2
            // 
            this.Superior_SP.Panel2.Controls.Add(this.Leyenda_Panel);
            this.Superior_SP.Panel2MinSize = 24;
            this.Superior_SP.Size = new System.Drawing.Size(732, 257);
            this.Superior_SP.SplitterDistance = 228;
            this.Superior_SP.TabIndex = 1;
            // 
            // Precios_Tabla
            // 
            this.Precios_Tabla.AllowUserToAddRows = false;
            this.Precios_Tabla.AllowUserToDeleteRows = false;
            this.Precios_Tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Precios_Tabla.DefaultCellStyle = dataGridViewCellStyle1;
            this.Precios_Tabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Precios_Tabla.GridColor = System.Drawing.SystemColors.Control;
            this.Precios_Tabla.Location = new System.Drawing.Point(0, 0);
            this.Precios_Tabla.Margin = new System.Windows.Forms.Padding(0);
            this.Precios_Tabla.MultiSelect = false;
            this.Precios_Tabla.Name = "Precios_Tabla";
            this.Precios_Tabla.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Precios_Tabla.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Precios_Tabla.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.Precios_Tabla.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Precios_Tabla.Size = new System.Drawing.Size(732, 228);
            this.Precios_Tabla.TabIndex = 1;
            // 
            // Leyenda_Panel
            // 
            this.Leyenda_Panel.Controls.Add(this.label3);
            this.Leyenda_Panel.Controls.Add(this.label2);
            this.Leyenda_Panel.Controls.Add(this.label1);
            this.Leyenda_Panel.Controls.Add(this.Up_LB);
            this.Leyenda_Panel.Controls.Add(this.Down_LB);
            this.Leyenda_Panel.Controls.Add(this.EQ_LB);
            this.Leyenda_Panel.Location = new System.Drawing.Point(34, 0);
            this.Leyenda_Panel.Name = "Leyenda_Panel";
            this.Leyenda_Panel.Size = new System.Drawing.Size(664, 24);
            this.Leyenda_Panel.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(502, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Precio superior al del producto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(307, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Precio del producto";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(69, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Precio inferior al del producto";
            // 
            // Up_LB
            // 
            this.Up_LB.BackColor = System.Drawing.Color.Red;
            this.Up_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Up_LB.Location = new System.Drawing.Point(476, 2);
            this.Up_LB.Name = "Up_LB";
            this.Up_LB.Size = new System.Drawing.Size(20, 20);
            this.Up_LB.TabIndex = 4;
            // 
            // Down_LB
            // 
            this.Down_LB.BackColor = System.Drawing.Color.LightGreen;
            this.Down_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Down_LB.Location = new System.Drawing.Point(43, 2);
            this.Down_LB.Name = "Down_LB";
            this.Down_LB.Size = new System.Drawing.Size(20, 20);
            this.Down_LB.TabIndex = 3;
            // 
            // EQ_LB
            // 
            this.EQ_LB.BackColor = System.Drawing.Color.White;
            this.EQ_LB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EQ_LB.Location = new System.Drawing.Point(281, 2);
            this.EQ_LB.Name = "EQ_LB";
            this.EQ_LB.Size = new System.Drawing.Size(20, 20);
            this.EQ_LB.TabIndex = 2;
            // 
            // PreciosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(734, 316);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PreciosForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Informe: Listado de Precios";
            this.Source_GB.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Precioss)).EndInit();
            this.Superior_SP.Panel1.ResumeLayout(false);
            this.Superior_SP.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Superior_SP)).EndInit();
            this.Superior_SP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Precios_Tabla)).EndInit();
            this.Leyenda_Panel.ResumeLayout(false);
            this.Leyenda_Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource Datos_Precioss;
        private System.Windows.Forms.Button Excel_BT;
        private System.Windows.Forms.SplitContainer Superior_SP;
        private System.Windows.Forms.DataGridView Precios_Tabla;
        private System.Windows.Forms.Panel Leyenda_Panel;
        private System.Windows.Forms.Label Up_LB;
        private System.Windows.Forms.Label Down_LB;
        private System.Windows.Forms.Label EQ_LB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
