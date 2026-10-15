namespace MaxFlow.Forms
{
    partial class GraphEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Pnl_graph = new System.Windows.Forms.Panel();
            this.Pnl_toolbox = new System.Windows.Forms.Panel();
            this.lbl_pos = new System.Windows.Forms.Label();
            this.txb_name = new System.Windows.Forms.TextBox();
            this.Btn_new_node = new System.Windows.Forms.Button();
            this.Pnl_toolbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_graph
            // 
            this.Pnl_graph.Dock = System.Windows.Forms.DockStyle.Top;
            this.Pnl_graph.Location = new System.Drawing.Point(0, 0);
            this.Pnl_graph.Name = "Pnl_graph";
            this.Pnl_graph.Size = new System.Drawing.Size(1122, 511);
            this.Pnl_graph.TabIndex = 0;
            this.Pnl_graph.TabStop = true;
            this.Pnl_graph.Paint += new System.Windows.Forms.PaintEventHandler(this.Pnl_graph_Paint);
            this.Pnl_graph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Pnl_graph_MouseClick);
            this.Pnl_graph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pnl_graph_MouseDown);
            this.Pnl_graph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pnl_graph_MouseMove);
            this.Pnl_graph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Pnl_graph_MouseUp);
            // 
            // Pnl_toolbox
            // 
            this.Pnl_toolbox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Pnl_toolbox.Controls.Add(this.lbl_pos);
            this.Pnl_toolbox.Controls.Add(this.txb_name);
            this.Pnl_toolbox.Controls.Add(this.Btn_new_node);
            this.Pnl_toolbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Pnl_toolbox.Location = new System.Drawing.Point(0, 517);
            this.Pnl_toolbox.Name = "Pnl_toolbox";
            this.Pnl_toolbox.Size = new System.Drawing.Size(1122, 100);
            this.Pnl_toolbox.TabIndex = 1;
            // 
            // lbl_pos
            // 
            this.lbl_pos.Location = new System.Drawing.Point(12, 12);
            this.lbl_pos.Name = "lbl_pos";
            this.lbl_pos.Size = new System.Drawing.Size(85, 24);
            this.lbl_pos.TabIndex = 2;
            this.lbl_pos.Text = "X: ___   Y: ___";
            // 
            // txb_name
            // 
            this.txb_name.Location = new System.Drawing.Point(12, 39);
            this.txb_name.MaxLength = 5;
            this.txb_name.Name = "txb_name";
            this.txb_name.Size = new System.Drawing.Size(76, 20);
            this.txb_name.TabIndex = 1;
            // 
            // Btn_new_node
            // 
            this.Btn_new_node.Location = new System.Drawing.Point(12, 65);
            this.Btn_new_node.Name = "Btn_new_node";
            this.Btn_new_node.Size = new System.Drawing.Size(76, 23);
            this.Btn_new_node.TabIndex = 0;
            this.Btn_new_node.Text = "Nuevo Nodo";
            this.Btn_new_node.UseVisualStyleBackColor = true;
            this.Btn_new_node.Click += new System.EventHandler(this.Btn_new_node_Click);
            // 
            // GraphEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1122, 617);
            this.Controls.Add(this.Pnl_toolbox);
            this.Controls.Add(this.Pnl_graph);
            this.KeyPreview = true;
            this.Name = "GraphEditorForm";
            this.Text = "GraphEditorForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GraphEditorForm_KeyDown);
            this.Pnl_toolbox.ResumeLayout(false);
            this.Pnl_toolbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_graph;
        private System.Windows.Forms.Panel Pnl_toolbox;
        private System.Windows.Forms.Button Btn_new_node;
        private System.Windows.Forms.Label lbl_pos;
        private System.Windows.Forms.TextBox txb_name;
    }
}