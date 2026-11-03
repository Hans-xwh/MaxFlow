using MaxFlow.Model;
using MaxFlow.Fulkerson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MaxFlow.Forms;

public partial class FordFulkersonForm : Form
{
    private bool showDetails = true;
    private readonly Grafo grafo;
    private readonly FordFulkerson algoritmo;

    private List<Arista> aristasResaltadas = new();
    private List<Arista> aristasCorteMinimo = new List<Arista>();
    private bool algoritmoTerminado = false;

    public FordFulkersonForm(Grafo grafoSeleccionado)
    {
        InitializeComponent();

        grafo = grafoSeleccionado
            ?? throw new ArgumentNullException(nameof(grafoSeleccionado));

        // Buscar el origen S y el destino T.
        Nodo? fuente = grafo.Nodos.FirstOrDefault(n => n.Nombre == "S")
            ?? grafo.Nodos.FirstOrDefault();

        Nodo? sumidero = grafo.Nodos.FirstOrDefault(n => n.Nombre == "T")
            ?? grafo.Nodos.LastOrDefault();

        if (fuente == null || sumidero == null || fuente == sumidero)
        {
            throw new InvalidOperationException(
                "El grafo debe tener un origen y un destino distintos.");
        }

        algoritmo = new FordFulkerson(grafo, fuente, sumidero);

        // Reducir el parpadeo del panel.
        typeof(Panel).InvokeMember(
            "DoubleBuffered",
            System.Reflection.BindingFlags.SetProperty |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.NonPublic,
            null,
            Pnl_graph,
            new object[] { true });

        Lbl_info.Text = "Presiona 'Siguiente paso' para comenzar.";
        Lbl_flujo_maximo.Text = "Flujo acumulado: 0";


        Pnl_graph.Invalidate();
    }

    private void Pnl_graph_Paint(object sender, PaintEventArgs e)
    {
        grafo.Draw(e.Graphics, aristasResaltadas, aristasCorteMinimo);
    }

    private void Btn_siguiente_paso_Click(object? sender, EventArgs e)
    {
        if (algoritmoTerminado)
            return;

        bool haySiguientePaso = algoritmo.SiguientePaso();


        if (!haySiguientePaso)
        {
            algoritmoTerminado = true;

            // Calcular el corte mínimo.
            algoritmo.CalcularCorteMinimo();

            // Resaltar las aristas que forman el corte.
            aristasCorteMinimo =
                   algoritmo.AristasCorteMinimo.ToList();

            // Quitar el resaltado del último camino aumentante.
            aristasResaltadas.Clear();

            // Obtener los nodos de ambos conjuntos.
            string ladoFuente = string.Join(
                ", ",
                algoritmo.NodosLadoFuente.Select(n => n.Nombre));

            string ladoSumidero = string.Join(
                ", ",
                algoritmo.NodosLadoSumidero.Select(n => n.Nombre));

            // Obtener las aristas del corte.
            string detalleAristas = algoritmo.AristasCorteMinimo.Count > 0
                ? string.Join(
                    Environment.NewLine,
                    algoritmo.AristasCorteMinimo.Select(a =>
                        $"{a.Origen.Nombre} → {a.Destino.Nombre} " +
                        $"(Capacidad: {a.Capacidad})"))
                : "No hay aristas en el corte.";

            // Comprobar el teorema de flujo máximo - corte mínimo.
            bool correcto =
                algoritmo.CapacidadCorteMinimo == algoritmo.FlujoMaximo;

            string verificacion = correcto
                ? "CORRECTO: ambas cantidades coinciden."
                : "ADVERTENCIA: los valores no coinciden.";

            Lbl_info.Text =
                "ALGORITMO TERMINADO\n" +
                "No existen más caminos aumentantes.\n" +
                "Aristas del corte mínimo resaltadas en azul.";
            
            Lbl_flujo_maximo.Text =
                $"Flujo máximo: {algoritmo.FlujoMaximo} | " +
                $"Capacidad del corte: {algoritmo.CapacidadCorteMinimo}";

            Btn_siguiente_paso.Enabled = false;

            // Actualizar el grafo antes de mostrar los resultados.
            Pnl_graph.Invalidate();

            MessageBox.Show(
                $"RESULTADO DEL CORTE MÍNIMO\n\n" +
                $"Conjunto del origen (S):\n{ladoFuente}\n\n" +
                $"Conjunto del destino (T):\n{ladoSumidero}\n\n" +
                $"Aristas del corte:\n{detalleAristas}\n\n" +
                $"Capacidad total del corte: " +
                $"{algoritmo.CapacidadCorteMinimo}\n\n" +
                $"Flujo máximo: {algoritmo.FlujoMaximo}\n\n" +
                verificacion,
                "Corte mínimo",
                MessageBoxButtons.OK,
                correcto ? MessageBoxIcon.Information : MessageBoxIcon.Warning
            );

            return;
        }


        var paso = algoritmo.UltimoPaso;

        if (paso == null)
            return;

        if (showDetails)
        {

            // Mostrar el flujo anterior, el flujo actual y el incremento.
            string detalleAristas = string.Join(
                Environment.NewLine,
                paso.Movimientos.Select(m =>
                    $"{m.Arista.Origen.Nombre} → {m.Arista.Destino.Nombre}\n" +
                    $"  Capacidad: {m.Arista.Capacidad}\n" +
                    $"  Flujo anterior: {m.FlujoAnterior}\n" +
                    $"  Flujo actual: {m.Arista.Flujo}\n" +
                    $"  Incremento del paso: {paso.Incremento}\n" +
                    $"  Movimiento: {(m.HaciaAdelante ? "Directo" : "Inverso")}"
                )
            );

            MessageBox.Show(
                detalleAristas,
                $"Detalle del paso {paso.NumeroPaso}",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // Resaltar las aristas del camino.
        aristasResaltadas = paso.Movimientos
            .Select(m => m.Arista)
            .Distinct()
            .ToList();

        string caminoTexto = string.Join(
            " → ",
            paso.Camino.Select(n => n.Nombre)
        );

        Lbl_info.Text =
            $"Paso: {paso.NumeroPaso}\n" +
            $"Camino: {caminoTexto}\n" +
            $"Incremento (Δ): {paso.Incremento}\n" +
            $"Flujo acumulado: {paso.FlujoAcumulado}";

        Lbl_flujo_maximo.Text =
            $"Flujo acumulado: {algoritmo.FlujoMaximo}";

        Pnl_graph.Invalidate();
    }

    private void ckb_details_CheckedChanged(object sender, EventArgs e)
    {
        showDetails = ckb_details.Checked;
    }
}