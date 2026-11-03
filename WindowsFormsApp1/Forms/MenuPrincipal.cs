
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using MaxFlow.Model;
using WindowsFormsApp1.Forms.Extra;
using MaxFlow.Forms;

namespace WindowsFormsApp1.Forms
{
    public partial class MenuPrincipal : Form
    {
        private Label lblSubtitulo = new Label();

        public MenuPrincipal()
        {
            InitializeComponent();
            ConfigurarMenu();
        }

        private void ConfigurarMenu()
        {
            // Configuración general de la ventana.
            this.Text = "MAX FLOW";
            this.ClientSize = new Size(900, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.DoubleBuffered = true;
            this.ResizeRedraw = true;

            // =====================================
            // TÍTULO PRINCIPAL
            // =====================================

            label1.Text = "MAX FLOW";

            label1.Font = new Font(
                "Segoe UI", 35F, FontStyle.Bold);

            label1.ForeColor = Color.White;
            label1.BackColor = Color.Transparent;
            label1.TextAlign = ContentAlignment.MiddleCenter;

            label1.SetBounds(0, 45, 900, 65);

            label1.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            // =====================================
            // SUBTÍTULO
            // =====================================

            lblSubtitulo.Text = "ALGORITMOS DE FLUJO EN REDES";

            lblSubtitulo.Font = new Font(
                "Segoe UI", 12F, FontStyle.Regular);

            lblSubtitulo.ForeColor =
                Color.FromArgb(115, 205, 225);

            lblSubtitulo.BackColor = Color.Transparent;
            lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter;

            lblSubtitulo.SetBounds(0, 112, 900, 30);

            lblSubtitulo.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            this.Controls.Add(lblSubtitulo);

            // =====================================
            // ESTILO DE LOS BOTONES
            // =====================================

            AplicarEstiloBoton(btn_graph, true);
            AplicarEstiloBoton(btn_graph_editor);
            AplicarEstiloBoton(Btn_dijkstra);
            AplicarEstiloBoton(btn_credits);

            // =====================================
            // DISTRIBUCIÓN DE BOTONES PRINCIPALES
            // =====================================

            int anchoBoton = 340;
            int altoBoton = 54;

            int x = (this.ClientSize.Width - anchoBoton) / 2;

            btn_graph.SetBounds(
                x, 190, anchoBoton, altoBoton);

            btn_graph_editor.SetBounds(
                x, 260, anchoBoton, altoBoton);

            Btn_dijkstra.SetBounds(
                x, 330, anchoBoton, altoBoton);

            btn_credits.SetBounds(
                x, 400, anchoBoton, altoBoton);

            // Textos.
            btn_graph.Text = "INICIAR FORD-FULKERSON";
            btn_graph_editor.Text = "EDITOR DE GRAFOS";
            Btn_dijkstra.Text = "BONUS: DIJKSTRA";
            btn_credits.Text = "CRÉDITOS";

            // =====================================
            // BOTÓN DE PRUEBA
            // =====================================

            AplicarEstiloBoton(btn_prueba);

            btn_prueba.Text = "Prueba de grafo";

            btn_prueba.Font = new Font(
                "Segoe UI", 8.5F, FontStyle.Regular);

            btn_prueba.SetBounds(
                755, 510, 130, 30);

            btn_prueba.Anchor =
                AnchorStyles.Bottom |
                AnchorStyles.Right;

            // Solicitar redibujado.
            this.Invalidate();
        }

        // =====================================
        // ESTILO DE BOTONES
        // =====================================

        private void AplicarEstiloBoton(
            Button boton, bool principal = false)
        {
            boton.FlatStyle = FlatStyle.Flat;

            boton.FlatAppearance.BorderSize = 1;

            boton.FlatAppearance.BorderColor = principal
                ? Color.FromArgb(75, 205, 230)
                : Color.FromArgb(43, 120, 160);

            boton.FlatAppearance.MouseOverBackColor = principal
                ? Color.FromArgb(22, 100, 140)
                : Color.FromArgb(20, 65, 95);

            boton.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(25, 115, 150);

            boton.BackColor = principal
                ? Color.FromArgb(15, 75, 110)
                : Color.FromArgb(12, 43, 70);

            boton.ForeColor = Color.White;

            boton.Font = new Font(
                "Segoe UI", 11F, FontStyle.Bold);

            boton.UseVisualStyleBackColor = false;
            boton.Cursor = Cursors.Hand;
        }

        // =====================================
        // FONDO DEGRADADO
        // =====================================

        protected override void OnPaintBackground(
            PaintEventArgs e)
        {
            if (this.ClientSize.Width <= 0 ||
                this.ClientSize.Height <= 0)
            {
                base.OnPaintBackground(e);
                return;
            }

            using LinearGradientBrush fondo =
                new LinearGradientBrush(
                    this.ClientRectangle,
                    Color.FromArgb(4, 17, 36),
                    Color.FromArgb(8, 43, 70),
                    45F);

            e.Graphics.FillRectangle(
                fondo, this.ClientRectangle);
        }

        // =====================================
        // DECORACIONES Y MARCO
        // =====================================

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            float ancho = this.ClientSize.Width;
            float alto = this.ClientSize.Height;

            // ---------------------------------
            // RED DECORATIVA IZQUIERDA Y DERECHA
            // ---------------------------------

            PointF[] puntos =
            {
                new PointF(ancho * 0.05F, alto * 0.20F),
                new PointF(ancho * 0.17F, alto * 0.12F),
                new PointF(ancho * 0.24F, alto * 0.32F),
                new PointF(ancho * 0.08F, alto * 0.78F),
                new PointF(ancho * 0.22F, alto * 0.87F),

                new PointF(ancho * 0.77F, alto * 0.13F),
                new PointF(ancho * 0.92F, alto * 0.25F),
                new PointF(ancho * 0.82F, alto * 0.72F),
                new PointF(ancho * 0.95F, alto * 0.86F)
            };

            int[,] conexiones =
            {
                { 0, 1 }, { 0, 2 }, { 1, 2 },
                { 0, 3 }, { 3, 4 }, { 2, 4 },
                { 5, 6 }, { 5, 7 }, { 6, 7 },
                { 7, 8 }, { 6, 8 }
            };

            using Pen linea = new Pen(
                Color.FromArgb(45, 95, 190, 220), 1.5F);

            using Brush nodos = new SolidBrush(
                Color.FromArgb(90, 70, 190, 215));

            for (int i = 0; i < conexiones.GetLength(0); i++)
            {
                PointF a = puntos[conexiones[i, 0]];
                PointF b = puntos[conexiones[i, 1]];

                e.Graphics.DrawLine(linea, a, b);
            }

            foreach (PointF punto in puntos)
            {
                e.Graphics.FillEllipse(
                    nodos,
                    punto.X - 4,
                    punto.Y - 4,
                    8,
                    8);
            }

            // ---------------------------------
            // MARCO DE LOS CUATRO BOTONES
            // ---------------------------------

            using Pen marco = new Pen(
                Color.FromArgb(55, 130, 165), 1.5F);

            using Pen acento = new Pen(
                Color.FromArgb(65, 205, 230), 2.5F);

            // Marco exterior.
            e.Graphics.DrawRectangle(
                marco, 245, 165, 410, 320);

            // Esquina superior izquierda.
            e.Graphics.DrawLine(
                acento, 245, 165, 275, 165);

            e.Graphics.DrawLine(
                acento, 245, 165, 245, 195);

            // Esquina superior derecha.
            e.Graphics.DrawLine(
                acento, 655, 165, 625, 165);

            e.Graphics.DrawLine(
                acento, 655, 165, 655, 195);

            // Esquina inferior izquierda.
            e.Graphics.DrawLine(
                acento, 245, 485, 275, 485);

            e.Graphics.DrawLine(
                acento, 245, 485, 245, 455);

            // Esquina inferior derecha.
            e.Graphics.DrawLine(
                acento, 655, 485, 625, 485);

            e.Graphics.DrawLine(
                acento, 655, 485, 655, 455);
        }

        // =====================================
        // EVENTOS DE LOS BOTONES
        // =====================================

        private void btn_prueba_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (FormPruebaDibujo pruebaForm =
                   new FormPruebaDibujo())
            {
                pruebaForm.ShowDialog();
            }

            this.Show();
        }

        private void btn_graph_editor_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (GraphEditorForm editorForm =
                   new GraphEditorForm())
            {
                editorForm.ShowDialog();
            }

            this.Show();
        }

        private void btn_credits_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (Creditos creditosForm =
                   new Creditos())
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

        private void btn_graph_Click(object sender, EventArgs e)
        {
            this.Hide();

            try
            {
                using (SelectorForm selector = new SelectorForm())
                {
                    if (selector.ShowDialog() != DialogResult.OK)
                        return;

                    Grafo grafoSeleccionado =
                        selector.SelectedGraph;

                    using (FordFulkersonForm fulkersonForm =
                           new FordFulkersonForm(grafoSeleccionado))
                    {
                        fulkersonForm.ShowDialog();
                    }
                }
            }
            finally
            {
                this.Show();
            }
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
        }
    }
}
