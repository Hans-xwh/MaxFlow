using MaxFlow.Model;
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.Classes;
using System.Linq;
using System.Text;

namespace MaxFlow.Forms
{
    public partial class FormPruebaDibujo : Form
    {
        private Grafo grafoPrueba;
        private FordFulkerson? algoritmo;
        private Button botonSiguiente;
        private Label labelRedResidual;
        private Label etiquetaPaso;

        public FormPruebaDibujo()
        {
            //InitializeComponent();

            this.Text = "Prueba de grafos";
            this.Size = new Size(900, 650);


            // ACTIVAR DOUBLE BUFFERING PARA EVITAR PARPADEO
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();


            //Inicializar y cargar un grafo de 8 nodos
            CargarGrafoPrueba();
            //grafoPrueba = new Grafo("");    //String vacio carga el archivo por defecto

            // Buscar fuente y sumidero
            Nodo? fuente = grafoPrueba.Nodos.Find(n => n.Nombre == "S");
            Nodo? sumidero = grafoPrueba.Nodos.Find(n => n.Nombre == "T");

            // Crear el algoritmo de Ford-Fulkerson
            if (fuente != null && sumidero != null)
            {
                algoritmo = new FordFulkerson(grafoPrueba, fuente, sumidero);
            }

            // Crear botón para avanzar una iteración
            botonSiguiente = new Button();

            botonSiguiente.Text = "Siguiente paso";
            botonSiguiente.Location = new Point(720, 520);
            botonSiguiente.Size = new Size(130, 40);

            // Asociar el botón con su evento Click
            botonSiguiente.Click += BotonSiguiente_Click;

            // Información del paso actual
            etiquetaPaso = new Label();

            etiquetaPaso.Location = new Point(20, 325);
            etiquetaPaso.Size = new Size(550, 170);

            etiquetaPaso.Font = new Font(
                "Arial",
                10,
                FontStyle.Regular
            );

            etiquetaPaso.Text = "Presiona 'Siguiente paso' para comenzar.";

            this.Controls.Add(etiquetaPaso);
            // Agregar el botón al formulario
            this.Controls.Add(botonSiguiente);

            labelRedResidual = new Label();

            labelRedResidual.Location = new Point(580, 325);
            labelRedResidual.Size = new Size(280, 260);

            labelRedResidual.AutoSize = false;
            labelRedResidual.Font = new Font("Arial", 10);

            labelRedResidual.Text = "Red residual:";

            this.Controls.Add(labelRedResidual);
        }

        private void BotonSiguiente_Click(object sender, EventArgs e)
        {
            if (algoritmo == null)
                return;

            bool pudoAvanzar = algoritmo.SiguientePaso();

            // Si no existe otro camino, terminamos
            if (!pudoAvanzar)
            {
                etiquetaPaso.Text =
                    "ALGORITMO TERMINADO\n" +
                    "Flujo máximo: " +
                    algoritmo.FlujoMaximo;

                botonSiguiente.Enabled = false;

                this.Invalidate();

                return;
            }

            // Obtener información del último paso
            PasoFordFulkerson? paso =
                algoritmo.UltimoPaso;

            if (paso == null)
                return;

            // Construir texto del camino
            string camino =
                string.Join(
                    " → ",
                    paso.Camino.Select(n => n.Nombre)
                );

            // Construir texto de capacidades residuales
            string residuales = "";

            foreach (MovimientoResidual movimiento
                     in paso.Movimientos)
            {
                Arista arista =
                    movimiento.Arista;

                string origen;
                string destino;

                if (movimiento.HaciaAdelante)
                {
                    origen = arista.Origen.Nombre;
                    destino = arista.Destino.Nombre;
                }
                else
                {
                    origen = arista.Destino.Nombre;
                    destino = arista.Origen.Nombre;
                }

                residuales +=
                    $"{origen} → {destino}: " +
                    $"{movimiento.CapacidadResidual}\n";
            }

            // Mostrar información
            etiquetaPaso.Text =
                $"Paso: {paso.NumeroPaso}\n" +
                $"Camino: {camino}\n" +
                $"Incremento (Δ): {paso.Incremento}\n" +
                $"Flujo acumulado: {paso.FlujoAcumulado}\n" +
                $"Capacidades residuales:\n{residuales}";

            MostrarRedResidual();
            // Redibujar el grafo
            this.Invalidate();
        }
        private void MostrarRedResidual()
        {
            if (algoritmo == null)
                return;
            StringBuilder texto = new StringBuilder();
            texto.AppendLine("RED RESIDUAL:");
            foreach (AristaResidual arista in algoritmo.RedResidual)
            {
                texto.AppendLine(
                    $"{arista.Origen.Nombre} → " +
                    $"{arista.Destino.Nombre} : " +
                    $"{arista.Capacidad}"
                );
            }

            labelRedResidual.Text = texto.ToString();
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

            if (grafoPrueba == null)
                return;


            // Si ya existe un paso del algoritmo,
            // obtenemos las aristas utilizadas en ese camino.
            if (algoritmo != null &&
                algoritmo.UltimoPaso != null)
            {
                var aristasCamino =
                    algoritmo.UltimoPaso.Movimientos
                    .Select(m => m.Arista)
                    .ToList();

                grafoPrueba.Draw(
                    e.Graphics,
                    aristasCamino
                );
            }
            else
            {
                // Antes de comenzar el algoritmo,
                // todas las aristas se dibujan normalmente.
                grafoPrueba.Draw(e.Graphics);
            }
        }
    }
}