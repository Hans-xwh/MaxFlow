#nullable enable
using MaxFlow.Extra;
using MaxFlow.Forms;
using MaxFlow.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp1.Forms.Extra;

public partial class DijkstraForm : Form
{
    Grafo grafo;
    Dijkstra dijkstra;

    //Dictionary<int, int> distancias;
    //Dictionary<int, Nodo> predecesores;

    public DijkstraForm()
    {
        InitializeComponent();

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
            grafo = selector.SelectedGraph;

            if (grafo == null)
            {
                this.BeginInvoke(new Action(() => this.Close()));   //Ciera el form si el anterior no retorno un grafo
                return;
            }
        }

        //Inicializar Dijkstra
        dijkstra = new Dijkstra(grafo);
        Pnl_graph.Invalidate();

        this.Show();
    }
    
    private void Pnl_graph_Paint(object sender, PaintEventArgs e)   //Esto quedo bastante mas horrible de lo que esperaba
    {
        using Font fTag = new Font("Arial", 9, FontStyle.Bold);
        using Font fNodo = new Font("Arial", 12, FontStyle.Bold);
        using Brush bTexto = new SolidBrush(Color.Black);

        using Pen pActual = new Pen(Color.Red, 3);
        using Pen pVisitado = new Pen(Color.DarkGray, 3);

        if (dijkstra.Terminado)
        {
            var camino = dijkstra.GetFastSolve();
            if (camino.Count > 1)
            {
                using (Pen penCamino = new Pen(Color.Red, 4))
                using (System.Drawing.Drawing2D.AdjustableArrowCap flecha = new System.Drawing.Drawing2D.AdjustableArrowCap(6, 6, true))
                {
                    penCamino.CustomEndCap = flecha;

                    for (int i = 0; i < camino.Count - 1; i++)
                    {
                        Nodo u = camino[i];
                        Nodo v = camino[i + 1];

                        // Recortar la línea para que la flecha no se esconda debajo del nodo (Igual que en el editor)
                        double dx = v.Posicion.X - u.Posicion.X;
                        double dy = v.Posicion.Y - u.Posicion.Y;
                        double distancia = Math.Sqrt(dx * dx + dy * dy);

                        Point puntoLlegada = v.Posicion;
                        if (distancia > 0)
                        {
                            int finX = (int)(v.Posicion.X - (dx / distancia) * v.Radio);
                            int finY = (int)(v.Posicion.Y - (dy / distancia) * v.Radio);
                            puntoLlegada = new Point(finX, finY);
                        }

                        e.Graphics.DrawLine(penCamino, u.Posicion, puntoLlegada);
                    }
                }
            }
        }

        // Pintar grafo //
        grafo.Draw(e.Graphics);
        // Esto en medio xd //

        //Pintar nodos
        foreach (Nodo nodo in grafo.Nodos)
        {
            //Resaltar actual
            if (dijkstra.NodoActual != null && dijkstra.NodoActual.Id == nodo.Id)
            {
                e.Graphics.DrawEllipse(pActual, nodo.Posicion.X - nodo.Radio, nodo.Posicion.Y - nodo.Radio, nodo.Radio * 2, nodo.Radio * 2);
            }
            //Resaltar visitados
            else if (dijkstra.Visitados.Contains(nodo.Id))
            {
                e.Graphics.DrawEllipse(pVisitado, nodo.Posicion.X - nodo.Radio, nodo.Posicion.Y - nodo.Radio, nodo.Radio * 2, nodo.Radio * 2);
            }

            //Dibujar etiquetas
            int dist = dijkstra.Distancias[nodo.Id];
            if ((dist != int.MaxValue) || nodo.Id == dijkstra.Origen.Id)
            {
                Nodo? pred = dijkstra.Predecesores[nodo.Id];

                string txtPred = pred != null ? pred.Nombre : "-";
                string txtDist = dist == int.MaxValue ? "inf" : dist.ToString();
                string tag = $"[{txtDist}/{txtPred}]";

                SizeF tagSize = e.Graphics.MeasureString(tag, fTag);

                //Posicion
                PointF posTag = new PointF(
                    nodo.Posicion.X - (tagSize.Width / 2),
                    nodo.Posicion.Y - (tagSize.Height / 2) - nodo.Radio*2 +4
                    );

                e.Graphics.FillRectangle(Brushes.White, posTag.X - 2, posTag.Y - 1, tagSize.Width + 4, tagSize.Height + 2);
                e.Graphics.DrawRectangle(Pens.Blue, posTag.X - 2, posTag.Y - 1, tagSize.Width + 4, tagSize.Height + 2);
                e.Graphics.DrawString(tag, fTag, Brushes.DarkBlue, posTag);
            }
        }
    }

    // Botones //
    private void Btn_reset_Click(object sender, EventArgs e)
    {
        grafo.LoadFromFile(grafo.FilePath);
        Btn_step.Enabled = true;
        dijkstra = new Dijkstra(grafo);

        Pnl_graph.Invalidate();
    }

    private void Btn_step_Click(object sender, EventArgs e)
    {
        bool continuar = dijkstra.Step();

        Btn_step.Enabled = continuar;
        Pnl_graph.Invalidate();
    }
}

