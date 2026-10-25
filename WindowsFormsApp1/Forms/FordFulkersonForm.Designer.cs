namespace MaxFlow.Forms
{
    partial class FordFulkersonForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel Pnl_graph;

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
            this.SuspendLayout();

            // Pnl_graph
            this.Pnl_graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pnl_graph.Location = new System.Drawing.Point(0, 0);
            this.Pnl_graph.Name = "Pnl_graph";
            this.Pnl_graph.Size = new System.Drawing.Size(1122, 617);
            this.Pnl_graph.TabIndex = 0;
            this.Pnl_graph.Paint += new System.Windows.Forms.PaintEventHandler(
                this.Pnl_graph_Paint
            );

            // FordFulkersonForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 617);
            this.Controls.Add(this.Pnl_graph);
            this.Name = "FordFulkersonForm";
            this.Text = "Ford-Fulkerson";
            this.ResumeLayout(false);


        }

        #endregion
    }
}