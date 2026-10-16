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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_organize = new System.Windows.Forms.Button();
            this.btn_uncycle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Cmb_sumidero = new System.Windows.Forms.ComboBox();
            this.Cmb_fuente = new System.Windows.Forms.ComboBox();
            this.grb_archivo = new System.Windows.Forms.GroupBox();
            this.btn_revert = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.grb_arista = new System.Windows.Forms.GroupBox();
            this.Btn_arista_aplicar = new System.Windows.Forms.Button();
            this.Lbl_flujo = new System.Windows.Forms.Label();
            this.Lbl_capacidad = new System.Windows.Forms.Label();
            this.Num_flujo = new System.Windows.Forms.NumericUpDown();
            this.Num_capacidad = new System.Windows.Forms.NumericUpDown();
            this.Chk_showAll = new System.Windows.Forms.CheckBox();
            this.Btn_arista_del = new System.Windows.Forms.Button();
            this.Cmb_aristas = new System.Windows.Forms.ComboBox();
            this.grb_node = new System.Windows.Forms.GroupBox();
            this.lbl_pos = new System.Windows.Forms.Label();
            this.Btn_new_node = new System.Windows.Forms.Button();
            this.txb_name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Lbl_cant_nodos = new System.Windows.Forms.Label();
            this.Lbl_cant_aristas = new System.Windows.Forms.Label();
            this.Pnl_toolbox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grb_archivo.SuspendLayout();
            this.grb_arista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_flujo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_capacidad)).BeginInit();
            this.grb_node.SuspendLayout();
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
            this.Pnl_toolbox.Controls.Add(this.groupBox1);
            this.Pnl_toolbox.Controls.Add(this.grb_archivo);
            this.Pnl_toolbox.Controls.Add(this.grb_arista);
            this.Pnl_toolbox.Controls.Add(this.grb_node);
            this.Pnl_toolbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Pnl_toolbox.Location = new System.Drawing.Point(0, 517);
            this.Pnl_toolbox.Name = "Pnl_toolbox";
            this.Pnl_toolbox.Size = new System.Drawing.Size(1122, 100);
            this.Pnl_toolbox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lbl_cant_aristas);
            this.groupBox1.Controls.Add(this.Lbl_cant_nodos);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_organize);
            this.groupBox1.Controls.Add(this.btn_uncycle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Cmb_sumidero);
            this.groupBox1.Controls.Add(this.Cmb_fuente);
            this.groupBox1.Location = new System.Drawing.Point(528, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 93);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Grafo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(313, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cantidad de Nodos: ";
            // 
            // btn_organize
            // 
            this.btn_organize.Location = new System.Drawing.Point(204, 26);
            this.btn_organize.Name = "btn_organize";
            this.btn_organize.Size = new System.Drawing.Size(90, 23);
            this.btn_organize.TabIndex = 5;
            this.btn_organize.Text = "Reorganizar";
            this.btn_organize.UseVisualStyleBackColor = true;
            this.btn_organize.Click += new System.EventHandler(this.btn_organize_Click);
            // 
            // btn_uncycle
            // 
            this.btn_uncycle.Location = new System.Drawing.Point(204, 59);
            this.btn_uncycle.Name = "btn_uncycle";
            this.btn_uncycle.Size = new System.Drawing.Size(90, 23);
            this.btn_uncycle.TabIndex = 4;
            this.btn_uncycle.Text = "Reparar Ciclos";
            this.btn_uncycle.UseVisualStyleBackColor = true;
            this.btn_uncycle.Click += new System.EventHandler(this.btn_uncycle_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Sumidero";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fuente";
            // 
            // Cmb_sumidero
            // 
            this.Cmb_sumidero.FormattingEnabled = true;
            this.Cmb_sumidero.Location = new System.Drawing.Point(59, 63);
            this.Cmb_sumidero.Name = "Cmb_sumidero";
            this.Cmb_sumidero.Size = new System.Drawing.Size(121, 21);
            this.Cmb_sumidero.TabIndex = 1;
            this.Cmb_sumidero.SelectedIndexChanged += new System.EventHandler(this.Cmb_sumidero_SelectedIndexChanged);
            // 
            // Cmb_fuente
            // 
            this.Cmb_fuente.FormattingEnabled = true;
            this.Cmb_fuente.Location = new System.Drawing.Point(59, 28);
            this.Cmb_fuente.Name = "Cmb_fuente";
            this.Cmb_fuente.Size = new System.Drawing.Size(121, 21);
            this.Cmb_fuente.TabIndex = 0;
            this.Cmb_fuente.SelectedIndexChanged += new System.EventHandler(this.Cmb_fuente_SelectedIndexChanged);
            // 
            // grb_archivo
            // 
            this.grb_archivo.Controls.Add(this.btn_revert);
            this.grb_archivo.Controls.Add(this.btn_save);
            this.grb_archivo.Location = new System.Drawing.Point(1023, 3);
            this.grb_archivo.Name = "grb_archivo";
            this.grb_archivo.Size = new System.Drawing.Size(87, 96);
            this.grb_archivo.TabIndex = 5;
            this.grb_archivo.TabStop = false;
            this.grb_archivo.Text = "Archivo";
            // 
            // btn_revert
            // 
            this.btn_revert.ForeColor = System.Drawing.Color.OrangeRed;
            this.btn_revert.Location = new System.Drawing.Point(7, 65);
            this.btn_revert.Name = "btn_revert";
            this.btn_revert.Size = new System.Drawing.Size(75, 23);
            this.btn_revert.TabIndex = 1;
            this.btn_revert.Text = "Revertir";
            this.btn_revert.UseVisualStyleBackColor = true;
            this.btn_revert.Click += new System.EventHandler(this.btn_revert_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(7, 17);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 42);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "Guardar";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // grb_arista
            // 
            this.grb_arista.Controls.Add(this.Btn_arista_aplicar);
            this.grb_arista.Controls.Add(this.Lbl_flujo);
            this.grb_arista.Controls.Add(this.Lbl_capacidad);
            this.grb_arista.Controls.Add(this.Num_flujo);
            this.grb_arista.Controls.Add(this.Num_capacidad);
            this.grb_arista.Controls.Add(this.Chk_showAll);
            this.grb_arista.Controls.Add(this.Btn_arista_del);
            this.grb_arista.Controls.Add(this.Cmb_aristas);
            this.grb_arista.Location = new System.Drawing.Point(148, 4);
            this.grb_arista.Name = "grb_arista";
            this.grb_arista.Size = new System.Drawing.Size(353, 93);
            this.grb_arista.TabIndex = 4;
            this.grb_arista.TabStop = false;
            this.grb_arista.Text = "Arista";
            // 
            // Btn_arista_aplicar
            // 
            this.Btn_arista_aplicar.Location = new System.Drawing.Point(93, 64);
            this.Btn_arista_aplicar.Name = "Btn_arista_aplicar";
            this.Btn_arista_aplicar.Size = new System.Drawing.Size(80, 23);
            this.Btn_arista_aplicar.TabIndex = 7;
            this.Btn_arista_aplicar.Text = "Aplicar";
            this.Btn_arista_aplicar.UseVisualStyleBackColor = true;
            this.Btn_arista_aplicar.Click += new System.EventHandler(this.Btn_arista_aplicar_Click);
            // 
            // Lbl_flujo
            // 
            this.Lbl_flujo.AutoSize = true;
            this.Lbl_flujo.Location = new System.Drawing.Point(270, 53);
            this.Lbl_flujo.Name = "Lbl_flujo";
            this.Lbl_flujo.Size = new System.Drawing.Size(59, 13);
            this.Lbl_flujo.TabIndex = 6;
            this.Lbl_flujo.Text = "Flujo Inicial";
            // 
            // Lbl_capacidad
            // 
            this.Lbl_capacidad.AutoSize = true;
            this.Lbl_capacidad.Location = new System.Drawing.Point(191, 53);
            this.Lbl_capacidad.Name = "Lbl_capacidad";
            this.Lbl_capacidad.Size = new System.Drawing.Size(58, 13);
            this.Lbl_capacidad.TabIndex = 5;
            this.Lbl_capacidad.Text = "Capacidad";
            // 
            // Num_flujo
            // 
            this.Num_flujo.Location = new System.Drawing.Point(273, 67);
            this.Num_flujo.Name = "Num_flujo";
            this.Num_flujo.Size = new System.Drawing.Size(63, 20);
            this.Num_flujo.TabIndex = 4;
            // 
            // Num_capacidad
            // 
            this.Num_capacidad.Location = new System.Drawing.Point(194, 67);
            this.Num_capacidad.Name = "Num_capacidad";
            this.Num_capacidad.Size = new System.Drawing.Size(63, 20);
            this.Num_capacidad.TabIndex = 3;
            // 
            // Chk_showAll
            // 
            this.Chk_showAll.AutoSize = true;
            this.Chk_showAll.Location = new System.Drawing.Point(194, 21);
            this.Chk_showAll.Name = "Chk_showAll";
            this.Chk_showAll.Size = new System.Drawing.Size(94, 17);
            this.Chk_showAll.TabIndex = 2;
            this.Chk_showAll.Text = "Mostrar Todas";
            this.Chk_showAll.UseVisualStyleBackColor = true;
            this.Chk_showAll.CheckedChanged += new System.EventHandler(this.Chk_showAll_CheckedChanged);
            // 
            // Btn_arista_del
            // 
            this.Btn_arista_del.Location = new System.Drawing.Point(6, 64);
            this.Btn_arista_del.Name = "Btn_arista_del";
            this.Btn_arista_del.Size = new System.Drawing.Size(81, 23);
            this.Btn_arista_del.TabIndex = 1;
            this.Btn_arista_del.Text = "Eliminar Arista";
            this.Btn_arista_del.UseVisualStyleBackColor = true;
            this.Btn_arista_del.Click += new System.EventHandler(this.Btn_arista_del_Click);
            // 
            // Cmb_aristas
            // 
            this.Cmb_aristas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_aristas.FormattingEnabled = true;
            this.Cmb_aristas.Location = new System.Drawing.Point(6, 19);
            this.Cmb_aristas.Name = "Cmb_aristas";
            this.Cmb_aristas.Size = new System.Drawing.Size(170, 21);
            this.Cmb_aristas.TabIndex = 0;
            this.Cmb_aristas.SelectedIndexChanged += new System.EventHandler(this.Cmb_aristas_SelectedIndexChanged);
            // 
            // grb_node
            // 
            this.grb_node.Controls.Add(this.lbl_pos);
            this.grb_node.Controls.Add(this.Btn_new_node);
            this.grb_node.Controls.Add(this.txb_name);
            this.grb_node.Location = new System.Drawing.Point(12, 4);
            this.grb_node.Name = "grb_node";
            this.grb_node.Size = new System.Drawing.Size(110, 93);
            this.grb_node.TabIndex = 3;
            this.grb_node.TabStop = false;
            this.grb_node.Text = "Nodo";
            // 
            // lbl_pos
            // 
            this.lbl_pos.Location = new System.Drawing.Point(12, 16);
            this.lbl_pos.Name = "lbl_pos";
            this.lbl_pos.Size = new System.Drawing.Size(85, 24);
            this.lbl_pos.TabIndex = 2;
            this.lbl_pos.Text = "X: ___   Y: ___";
            // 
            // Btn_new_node
            // 
            this.Btn_new_node.Location = new System.Drawing.Point(15, 69);
            this.Btn_new_node.Name = "Btn_new_node";
            this.Btn_new_node.Size = new System.Drawing.Size(76, 23);
            this.Btn_new_node.TabIndex = 0;
            this.Btn_new_node.Text = "Nuevo Nodo";
            this.Btn_new_node.UseVisualStyleBackColor = true;
            this.Btn_new_node.Click += new System.EventHandler(this.Btn_new_node_Click);
            // 
            // txb_name
            // 
            this.txb_name.Location = new System.Drawing.Point(15, 43);
            this.txb_name.MaxLength = 5;
            this.txb_name.Name = "txb_name";
            this.txb_name.Size = new System.Drawing.Size(76, 20);
            this.txb_name.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Cantidad de Aristas: ";
            // 
            // Lbl_cant_nodos
            // 
            this.Lbl_cant_nodos.AutoSize = true;
            this.Lbl_cant_nodos.Location = new System.Drawing.Point(413, 31);
            this.Lbl_cant_nodos.Name = "Lbl_cant_nodos";
            this.Lbl_cant_nodos.Size = new System.Drawing.Size(28, 13);
            this.Lbl_cant_nodos.TabIndex = 8;
            this.Lbl_cant_nodos.Text = "XXX";
            // 
            // Lbl_cant_aristas
            // 
            this.Lbl_cant_aristas.AutoSize = true;
            this.Lbl_cant_aristas.Location = new System.Drawing.Point(413, 67);
            this.Lbl_cant_aristas.Name = "Lbl_cant_aristas";
            this.Lbl_cant_aristas.Size = new System.Drawing.Size(28, 13);
            this.Lbl_cant_aristas.TabIndex = 9;
            this.Lbl_cant_aristas.Text = "XXX";
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grb_archivo.ResumeLayout(false);
            this.grb_arista.ResumeLayout(false);
            this.grb_arista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_flujo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_capacidad)).EndInit();
            this.grb_node.ResumeLayout(false);
            this.grb_node.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Pnl_graph;
        private System.Windows.Forms.Panel Pnl_toolbox;
        private System.Windows.Forms.Button Btn_new_node;
        private System.Windows.Forms.Label lbl_pos;
        private System.Windows.Forms.TextBox txb_name;
        private System.Windows.Forms.GroupBox grb_node;
        private System.Windows.Forms.GroupBox grb_arista;
        private System.Windows.Forms.ComboBox Cmb_aristas;
        private System.Windows.Forms.CheckBox Chk_showAll;
        private System.Windows.Forms.Button Btn_arista_del;
        private System.Windows.Forms.Label Lbl_capacidad;
        private System.Windows.Forms.NumericUpDown Num_flujo;
        private System.Windows.Forms.NumericUpDown Num_capacidad;
        private System.Windows.Forms.Label Lbl_flujo;
        private System.Windows.Forms.Button Btn_arista_aplicar;
        private System.Windows.Forms.GroupBox grb_archivo;
        private System.Windows.Forms.Button btn_revert;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Cmb_sumidero;
        private System.Windows.Forms.ComboBox Cmb_fuente;
        private System.Windows.Forms.Button btn_uncycle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_organize;
        private System.Windows.Forms.Label Lbl_cant_nodos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Lbl_cant_aristas;
    }
}