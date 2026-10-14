namespace WindowsFormsApp1.Forms
{
    partial class MenuPrincipal
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
            this.btn_prueba = new System.Windows.Forms.Button();
            this.btn_graph_editor = new System.Windows.Forms.Button();
            this.btn_graph = new System.Windows.Forms.Button();
            this.btn_credits = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_prueba
            // 
            this.btn_prueba.Location = new System.Drawing.Point(676, 399);
            this.btn_prueba.Name = "btn_prueba";
            this.btn_prueba.Size = new System.Drawing.Size(93, 30);
            this.btn_prueba.TabIndex = 0;
            this.btn_prueba.Text = "Prueba de Grafo";
            this.btn_prueba.UseVisualStyleBackColor = true;
            this.btn_prueba.Click += new System.EventHandler(this.btn_prueba_Click);
            // 
            // btn_graph_editor
            // 
            this.btn_graph_editor.Location = new System.Drawing.Point(307, 210);
            this.btn_graph_editor.Name = "btn_graph_editor";
            this.btn_graph_editor.Size = new System.Drawing.Size(193, 53);
            this.btn_graph_editor.TabIndex = 1;
            this.btn_graph_editor.Text = "Editor de grafos";
            this.btn_graph_editor.UseVisualStyleBackColor = true;
            this.btn_graph_editor.Click += new System.EventHandler(this.btn_graph_editor_Click);
            // 
            // btn_graph
            // 
            this.btn_graph.Location = new System.Drawing.Point(307, 140);
            this.btn_graph.Name = "btn_graph";
            this.btn_graph.Size = new System.Drawing.Size(193, 53);
            this.btn_graph.TabIndex = 1;
            this.btn_graph.Text = "Iniciar Ford Fulkerson";
            this.btn_graph.UseVisualStyleBackColor = true;
            // 
            // btn_credits
            // 
            this.btn_credits.Location = new System.Drawing.Point(307, 282);
            this.btn_credits.Name = "btn_credits";
            this.btn_credits.Size = new System.Drawing.Size(193, 53);
            this.btn_credits.TabIndex = 1;
            this.btn_credits.Text = "Creditos";
            this.btn_credits.UseVisualStyleBackColor = true;
            this.btn_credits.Click += new System.EventHandler(this.btn_credits_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Max Flow";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_graph);
            this.Controls.Add(this.btn_credits);
            this.Controls.Add(this.btn_graph_editor);
            this.Controls.Add(this.btn_prueba);
            this.Name = "MenuPrincipal";
            this.Text = "MenuPrincipal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_prueba;
        private System.Windows.Forms.Button btn_graph_editor;
        private System.Windows.Forms.Button btn_graph;
        private System.Windows.Forms.Button btn_credits;
        private System.Windows.Forms.Label label1;
    }
}