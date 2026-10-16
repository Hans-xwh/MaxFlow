namespace WindowsFormsApp1.Forms.Extra
{
    partial class DijkstraForm
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
            this.Pnl_toolbox = new System.Windows.Forms.Panel();
            this.Btn_step = new System.Windows.Forms.Button();
            this.Pnl_graph = new System.Windows.Forms.Panel();
            this.Btn_reset = new System.Windows.Forms.Button();
            this.Pnl_toolbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pnl_toolbox
            // 
            this.Pnl_toolbox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Pnl_toolbox.Controls.Add(this.Btn_reset);
            this.Pnl_toolbox.Controls.Add(this.Btn_step);
            this.Pnl_toolbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Pnl_toolbox.Location = new System.Drawing.Point(0, 491);
            this.Pnl_toolbox.Name = "Pnl_toolbox";
            this.Pnl_toolbox.Size = new System.Drawing.Size(1121, 86);
            this.Pnl_toolbox.TabIndex = 3;
            // 
            // Btn_step
            // 
            this.Btn_step.Location = new System.Drawing.Point(1023, 12);
            this.Btn_step.Name = "Btn_step";
            this.Btn_step.Size = new System.Drawing.Size(86, 62);
            this.Btn_step.TabIndex = 0;
            this.Btn_step.Text = "Siguiente paso";
            this.Btn_step.UseVisualStyleBackColor = true;
            this.Btn_step.Click += new System.EventHandler(this.Btn_step_Click);
            // 
            // Pnl_graph
            // 
            this.Pnl_graph.Dock = System.Windows.Forms.DockStyle.Top;
            this.Pnl_graph.Location = new System.Drawing.Point(0, 0);
            this.Pnl_graph.Name = "Pnl_graph";
            this.Pnl_graph.Size = new System.Drawing.Size(1121, 485);
            this.Pnl_graph.TabIndex = 2;
            this.Pnl_graph.TabStop = true;
            this.Pnl_graph.Paint += new System.Windows.Forms.PaintEventHandler(this.Pnl_graph_Paint);
            // 
            // Btn_reset
            // 
            this.Btn_reset.Location = new System.Drawing.Point(931, 12);
            this.Btn_reset.Name = "Btn_reset";
            this.Btn_reset.Size = new System.Drawing.Size(86, 62);
            this.Btn_reset.TabIndex = 1;
            this.Btn_reset.Text = "Reiniciar";
            this.Btn_reset.UseVisualStyleBackColor = true;
            this.Btn_reset.Click += new System.EventHandler(this.Btn_reset_Click);
            // 
            // DijkstraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 577);
            this.Controls.Add(this.Pnl_toolbox);
            this.Controls.Add(this.Pnl_graph);
            this.Name = "DijkstraForm";
            this.Text = "Dijkstra";
            this.Pnl_toolbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel Pnl_toolbox;
        private System.Windows.Forms.Panel Pnl_graph;
        private System.Windows.Forms.Button Btn_step;
        private System.Windows.Forms.Button Btn_reset;
    }
}