namespace MaxFlow.Forms
{
    partial class SelectorForm
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
            this.lst_files = new System.Windows.Forms.ListBox();
            this.btn_random = new System.Windows.Forms.Button();
            this.btn_examinar = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // lst_files
            // 
            this.lst_files.FormattingEnabled = true;
            this.lst_files.Location = new System.Drawing.Point(12, 54);
            this.lst_files.Name = "lst_files";
            this.lst_files.Size = new System.Drawing.Size(164, 290);
            this.lst_files.TabIndex = 0;
            this.lst_files.DoubleClick += new System.EventHandler(this.lst_files_DoubleClick);
            // 
            // btn_random
            // 
            this.btn_random.Location = new System.Drawing.Point(332, 118);
            this.btn_random.Name = "btn_random";
            this.btn_random.Size = new System.Drawing.Size(129, 59);
            this.btn_random.TabIndex = 1;
            this.btn_random.Text = "Generar Aleatorio";
            this.btn_random.UseVisualStyleBackColor = true;
            // 
            // btn_examinar
            // 
            this.btn_examinar.Location = new System.Drawing.Point(207, 230);
            this.btn_examinar.Name = "btn_examinar";
            this.btn_examinar.Size = new System.Drawing.Size(254, 51);
            this.btn_examinar.TabIndex = 1;
            this.btn_examinar.Text = "Examinar";
            this.btn_examinar.UseVisualStyleBackColor = true;
            this.btn_examinar.Click += new System.EventHandler(this.btn_examinar_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(213, 150);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(104, 20);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Numero de vertices";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cargar desde un archivo";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 376);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.btn_examinar);
            this.Controls.Add(this.btn_random);
            this.Controls.Add(this.lst_files);
            this.Name = "SelectorForm";
            this.Text = "SelectorForm";
            this.Load += new System.EventHandler(this.SelectorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lst_files;
        private System.Windows.Forms.Button btn_random;
        private System.Windows.Forms.Button btn_examinar;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}