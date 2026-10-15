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
            this.SuspendLayout();
            // 
            // Pnl_graph
            // 
            this.Pnl_graph.Dock = System.Windows.Forms.DockStyle.Top;
            this.Pnl_graph.Location = new System.Drawing.Point(0, 0);
            this.Pnl_graph.Name = "Pnl_graph";
            this.Pnl_graph.Size = new System.Drawing.Size(1122, 511);
            this.Pnl_graph.TabIndex = 0;
            // 
            // Pnl_toolbox
            // 
            this.Pnl_toolbox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Pnl_toolbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Pnl_toolbox.Location = new System.Drawing.Point(0, 517);
            this.Pnl_toolbox.Name = "Pnl_toolbox";
            this.Pnl_toolbox.Size = new System.Drawing.Size(1122, 100);
            this.Pnl_toolbox.TabIndex = 1;
            // 
            // GraphEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1122, 617);
            this.Controls.Add(this.Pnl_toolbox);
            this.Controls.Add(this.Pnl_graph);
            this.Name = "GraphEditorForm";
            this.Text = "GraphEditorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_graph;
        private System.Windows.Forms.Panel Pnl_toolbox;
    }
}