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
    private readonly Grafo grafo;
    private readonly FordFulkerson algoritmo;

    private List<Arista> aristasResaltadas = new();
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
        grafo.Draw(e.Graphics, aristasResaltadas);
    }

    private void Btn_siguiente_paso_Click(object? sender, EventArgs e)
    {
        if (algoritmoTerminado)
            return;

        bool haySiguientePaso = algoritmo.SiguientePaso();

        if (!haySiguientePaso)
        {
            algoritmoTerminado = true;

            Lbl_info.Text =
                "ALGORITMO TERMINADO\n" +
                "No existen más caminos aumentantes.";

            Lbl_flujo_maximo.Text =
                $"Flujo máximo: {algoritmo.FlujoMaximo}";

            Btn_siguiente_paso.Enabled = false;
            Pnl_graph.Invalidate();
            return;
        }

        var paso = algoritmo.UltimoPaso;

        if (paso == null)
            return;

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
}