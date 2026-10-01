using MaxFlow.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MaxFlow.Forms
{
    public partial class FormPruebaDibujo : Form
    {
        private Grafo grafoPrueba;

        public FormPruebaDibujo()
        {
            //InitializeComponent();

            this.Text = "Prueba de grafos";
            this.Size = new Size(800, 450);


            // ACTIVAR DOUBLE BUFFERING PARA EVITAR PARPADEO
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();


            //Inicializar y cargar un grafo de 8 nodos
            //CargarGrafoPrueba();
            grafoPrueba = new Grafo("");    //String vacio carga el archivo por defecto
        }

        private void CargarGrafoPrueba()
        {
            grafoPrueba = new Grafo();

            //Crear 8 Nodos distribuidos en el lienzo
            Nodo n1 = new Nodo(1, "S", new Point(80, 200));   // Fuente
            Nodo n2 = new Nodo(2, "A", new Point(220, 100));
            Nodo n3 = new Nodo(3, "B", new Point(220, 300));
            Nodo n4 = new Nodo(4, "C", new Point(380, 100));
            Nodo n5 = new Nodo(5, "D", new Point(380, 300));
            Nodo n6 = new Nodo(6, "E", new Point(540, 100));
            Nodo n7 = new Nodo(7, "F", new Point(540, 300));
            Nodo n8 = new Nodo(8, "T", new Point(680, 200));   // Sumidero

            grafoPrueba.Nodos.AddRange(new[] { n1, n2, n3, n4, n5, n6, n7, n8 });

            //Crear Aristas con capacidades arbitrarias
            grafoPrueba.Aristas.Add(new Arista(n1, n2, 10));
            grafoPrueba.Aristas.Add(new Arista(n1, n3, 8));
            grafoPrueba.Aristas.Add(new Arista(n2, n4, 5));
            grafoPrueba.Aristas.Add(new Arista(n2, n5, 7));
            grafoPrueba.Aristas.Add(new Arista(n3, n5, 10));
            grafoPrueba.Aristas.Add(new Arista(n4, n6, 8));
            grafoPrueba.Aristas.Add(new Arista(n5, n4, 4));
            grafoPrueba.Aristas.Add(new Arista(n5, n7, 9));
            grafoPrueba.Aristas.Add(new Arista(n6, n8, 12));
            grafoPrueba.Aristas.Add(new Arista(n7, n8, 10));
        }

        //Evento Paint del propio Formulario (o de un Panel/PictureBox)
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (grafoPrueba != null)
            {
                grafoPrueba.Draw(e.Graphics);
            }
        }
    }
}