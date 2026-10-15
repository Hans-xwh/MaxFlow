#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Forms;

using MaxFlow.Model;

namespace MaxFlow.Forms;

public partial class GraphEditorForm : Form
{
    private Grafo Grafo;
    private Nodo? SelectedNodo = null;

    private Point offsetArrastre;

    public GraphEditorForm()
    {
        InitializeComponent();

        //doble buffer form
        this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        this.UpdateStyles();

        //Habilitar Doble Búfer en el panelLienzo
        typeof(Panel).InvokeMember("DoubleBuffered",    //Cortecia de Gemini
            System.Reflection.BindingFlags.SetProperty |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.NonPublic,
            null, Pnl_graph, new object[] { true });
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        this.Hide();

        using (SelectorForm selector = new())
        {
            selector.ShowDialog();
            Grafo = selector.SelectedGraph;

            if (Grafo == null)
            {
                this.BeginInvoke(new Action(() => this.Close()));   //Ciera el form si el anterior no retorno un grafo
                return;
            }
        }

        this.Show();
    }

    // Botones //

    private void Btn_new_node_Click(object sender, EventArgs e)
    {
        Nodo newNodo = new(50, "B", new Point(100, 100));
        Grafo?.Nodos.Add(newNodo);
        this.Pnl_graph.Invalidate();
    }

    // Panel Grafo //
    private void Pnl_graph_Paint(object sender, PaintEventArgs e)
    {
        Grafo?.Draw(e.Graphics);
    }

    private void Pnl_graph_MouseDown(object sender, MouseEventArgs e)   //Click en el panel
    {
        if (e.Button == MouseButtons.Left)
        {
            foreach (Nodo nodo in Grafo.Nodos)
            {
                if (nodo.IsInside(e.Location))
                {
                    SelectedNodo = nodo;
                    offsetArrastre = new Point(e.X - nodo.Posicion.X, e.Y - nodo.Posicion.Y);
                    break;
                }
            }
        }
    }

    private void Pnl_graph_MouseUp(object sender, MouseEventArgs e)     //Mover el mouse en el panel
    {
        if (e.Button == MouseButtons.Left && SelectedNodo != null)
        {
            SelectedNodo = null;
            
        }
    }

    private void Pnl_graph_MouseMove(object sender, MouseEventArgs e)   //Soltar el click en el panel
    {
        if (SelectedNodo != null)
        {
            int nX = e.X - offsetArrastre.X;
            int nY = e.Y - offsetArrastre.Y;

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                nX = (int)Math.Round(nX / 10.0) * 10;
                nY = (int)Math.Round(nY / 10.0) * 10;
            }

            SelectedNodo.Posicion = new Point(nX, nY);
            Pnl_graph.Invalidate();
        }
    }
}

