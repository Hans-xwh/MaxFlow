using MaxFlow.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaxFlow.Forms;

public partial class FordFulkersonForm : Form
{
    private Grafo Grafo;

    public FordFulkersonForm()
    {
        InitializeComponent();

        // Permitir dibujar el grafo sin parpadeos.
        typeof(Panel).InvokeMember(
            "DoubleBuffered",
            System.Reflection.BindingFlags.SetProperty |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.NonPublic,
            null,
            Pnl_graph,
            new object[] { true }
        );
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        Hide();

        using (SelectorForm selector = new SelectorForm())
        {
            selector.ShowDialog();
            Grafo = selector.SelectedGraph;

            if (Grafo == null)
            {
                Close();
                return;
            }
        }

        // Mostrar el grafo una vez seleccionado.
        Pnl_graph.Invalidate();
        Show();
    }

    private void Pnl_graph_Paint(object sender, PaintEventArgs e)
    {
        if (Grafo == null)
            return;

        Grafo.Draw(e.Graphics);
    }
}
