using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WindowsFormsApp1.Forms.Extra;
using MaxFlow.Forms;

namespace WindowsFormsApp1.Forms
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void btn_prueba_Click(object sender, EventArgs e)
        {
            //Run the test form
            this.Hide();
            using (FormPruebaDibujo pruebaForm = new FormPruebaDibujo())
            {
                pruebaForm.ShowDialog();
            }
            this.Show();
        }

        private void btn_graph_editor_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (GraphEditorForm editorForm = new GraphEditorForm())
            {
                editorForm.ShowDialog();
            }
            this.Show();
        }

        private void btn_credits_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (Creditos creditosForm = new Creditos())
            {
                creditosForm.ShowDialog();
            }
            this.Show();
        }

        private void Btn_dijkstra_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (DijkstraForm dijkstraForm = new())
            {
                dijkstraForm.ShowDialog();
            }
            this.Show();
        }
    }
}
