namespace MaxFlow.Forms
{
    partial class FordFulkersonForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel Pnl_graph;
        private System.Windows.Forms.Label Lbl_info;
        private System.Windows.Forms.Label Lbl_flujo_maximo;
        private System.Windows.Forms.Button Btn_siguiente_paso;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.Pnl_graph = new System.Windows.Forms.Panel();
            this.Lbl_info = new System.Windows.Forms.Label();
            this.Lbl_flujo_maximo = new System.Windows.Forms.Label();
            this.Btn_siguiente_paso = new System.Windows.Forms.Button();
            this.ckb_details = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Pnl_graph
            // 
            this.Pnl_graph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pnl_graph.Location = new System.Drawing.Point(12, 12);
            this.Pnl_graph.Name = "Pnl_graph";
            this.Pnl_graph.Size = new System.Drawing.Size(1098, 500);
            this.Pnl_graph.TabIndex = 0;
            this.Pnl_graph.Paint += new System.Windows.Forms.PaintEventHandler(this.Pnl_graph_Paint);
            // 
            // Lbl_info
            // 
            this.Lbl_info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Lbl_info.Location = new System.Drawing.Point(12, 525);
            this.Lbl_info.Name = "Lbl_info";
            this.Lbl_info.Size = new System.Drawing.Size(650, 65);
            this.Lbl_info.TabIndex = 1;
            this.Lbl_info.Text = "Presiona Siguiente paso para comenzar.";
            this.Lbl_info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Lbl_flujo_maximo
            // 
            this.Lbl_flujo_maximo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Lbl_flujo_maximo.AutoSize = true;
            this.Lbl_flujo_maximo.Location = new System.Drawing.Point(700, 540);
            this.Lbl_flujo_maximo.Name = "Lbl_flujo_maximo";
            this.Lbl_flujo_maximo.Size = new System.Drawing.Size(96, 13);
            this.Lbl_flujo_maximo.TabIndex = 2;
            this.Lbl_flujo_maximo.Text = "Flujo acumulado: 0";
            // 
            // Btn_siguiente_paso
            // 
            this.Btn_siguiente_paso.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_siguiente_paso.Location = new System.Drawing.Point(900, 525);
            this.Btn_siguiente_paso.Name = "Btn_siguiente_paso";
            this.Btn_siguiente_paso.Size = new System.Drawing.Size(200, 45);
            this.Btn_siguiente_paso.TabIndex = 3;
            this.Btn_siguiente_paso.Text = "Siguiente paso";
            this.Btn_siguiente_paso.UseVisualStyleBackColor = true;
            this.Btn_siguiente_paso.Click += new System.EventHandler(this.Btn_siguiente_paso_Click);
            // 
            // ckb_details
            // 
            this.ckb_details.AutoSize = true;
            this.ckb_details.Checked = true;
            this.ckb_details.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_details.Location = new System.Drawing.Point(909, 576);
            this.ckb_details.Name = "ckb_details";
            this.ckb_details.Size = new System.Drawing.Size(168, 17);
            this.ckb_details.TabIndex = 4;
            this.ckb_details.Text = "Mostrar detalles de cada paso";
            this.ckb_details.UseVisualStyleBackColor = true;
            this.ckb_details.CheckedChanged += new System.EventHandler(this.ckb_details_CheckedChanged);
            // 
            // FordFulkersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 617);
            this.Controls.Add(this.ckb_details);
            this.Controls.Add(this.Pnl_graph);
            this.Controls.Add(this.Lbl_info);
            this.Controls.Add(this.Lbl_flujo_maximo);
            this.Controls.Add(this.Btn_siguiente_paso);
            this.Name = "FordFulkersonForm";
            this.Text = "Buen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ckb_details;
    }
}