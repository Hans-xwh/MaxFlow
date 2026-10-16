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
    private Nodo? SelNode = null;

    private bool IsDragging = false;

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
        if (SelNode == null)
        {
            String newName = txb_name.Text != "" ? txb_name.Text : "N"; 
            Nodo newNodo = new(50, newName, new Point(100, 100));
            Grafo?.Nodos.Add(newNodo);
            SelNode = newNodo;
        }
        else
        {
            SelNode.Nombre = txb_name.Text;
        }
        UpdateComboAristas();
        this.Pnl_graph.Invalidate();
    }

    private void Btn_arista_aplicar_Click(object sender, EventArgs e)
    {
        if (Cmb_aristas.SelectedItem is Arista arista)
        {
            arista.Capacidad = (int)Num_capacidad.Value;
            arista.Flujo = Math.Min(arista.Capacidad, (int)Num_flujo.Value);

            UpdateComboAristas();
            Pnl_graph.Invalidate(); //Esto se puso feo muy rapido xd
        }
    }

    private void Btn_arista_del_Click(object sender, EventArgs e)
    {
        if (Cmb_aristas.SelectedItem is Arista arista)
        {
            Grafo.Aristas.Remove(arista);
            UpdateComboAristas();
            Pnl_graph.Invalidate();
        }
    }

    // Panel Grafo //
    private void Pnl_graph_Paint(object sender, PaintEventArgs e)
    {
        Grafo?.Draw(e.Graphics);

        if (SelNode != null)
        {
            using (Pen pen = new Pen(Color.Red, 3))
            {
                e.Graphics.DrawEllipse(pen, SelNode.Posicion.X - SelNode.Radio, SelNode.Posicion.Y - SelNode.Radio, SelNode.Radio * 2, SelNode.Radio * 2);
            }

            //Update UI
            lbl_pos.Text = $"X: {SelNode.Posicion.X}  Y: {SelNode.Posicion.Y}";
            txb_name.Text = SelNode.Nombre;
            Btn_new_node.Text = "Aplicar";
        }
        else
        {
            //Update UI
            lbl_pos.Text = $"X: ___  Y: ___";
            Btn_new_node.Text = "Nuevo Nodo";
        }
    }

    private void Pnl_graph_MouseDown(object sender, MouseEventArgs e)   //Click en el panel
    {
        if (e.Button == MouseButtons.Left)
        {
            foreach (Nodo nodo in Grafo.Nodos)
            {
                if (nodo.IsInside(e.Location))
                {
                    SelNode = nodo;
                    offsetArrastre = new Point(e.X - nodo.Posicion.X, e.Y - nodo.Posicion.Y);
                    IsDragging = true;
                    UpdateComboAristas();
                    return;
                }
            }

            SelNode = null;
        }
        else if (e.Button == MouseButtons.Right)
        {
            Nodo? hitNode = Grafo.Nodos.Find(n => n.IsInside(e.Location));

            if (SelNode == null)
            {
                if (hitNode != null)
                {
                    //OrgNode = hitNode;
                    SelNode = hitNode;
                }
            }
            else
            {
                if (hitNode != null && hitNode != SelNode)
                {
                    int newId = Grafo.Aristas.Count + 1;
                    Arista newArista = new(SelNode, hitNode, 1, 0, newId);
                    Grafo.Aristas.Add(newArista);
                    UpdateComboAristas();
                    Cmb_aristas.SelectedItem = newArista;
                }
                //SelNode = null;
            }
            //UpdateComboAristas();
            Pnl_graph.Invalidate();
        }
        
    }

    private void Pnl_graph_MouseUp(object sender, MouseEventArgs e)     //Mover el mouse en el panel
    {
        if (e.Button == MouseButtons.Left)
        {
            //SelNode = null;
            IsDragging = false;
        }
    }

    private void Pnl_graph_MouseMove(object sender, MouseEventArgs e)   //Soltar el click en el panel
    {
        if (SelNode != null && IsDragging)
        {
            int nX = e.X - offsetArrastre.X;
            int nY = e.Y - offsetArrastre.Y;

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                nX = (int)Math.Round(nX / 10.0) * 10;
                nY = (int)Math.Round(nY / 10.0) * 10;
            }

            nX = Math.Max(30, Math.Min(nX, Pnl_graph.Size.Width - 30));
            nY = Math.Max(30, Math.Min(nY, Pnl_graph.Size.Height - 30));

            SelNode.Posicion = new Point(nX, nY);
            Pnl_graph.Invalidate();
        }
        else if (SelNode != null)
        {
            this.Invalidate();
        }
    }

    private void Pnl_graph_MouseClick(object sender, MouseEventArgs e)
    {
        Pnl_graph.Invalidate();
        if (e.Button == MouseButtons.Left)
        {
            foreach (Nodo nodo in Grafo.Nodos)
            {
                if (nodo.IsInside(e.Location))
                {
                    SelNode = nodo;
                    
                    return;
                }
            }
            SelNode = null;
            UpdateComboAristas();
            txb_name.Text = "";
        }
    }

    // Keys //
    private void GraphEditorForm_KeyDown(object sender, KeyEventArgs e)
    {
        if(SelNode != null && e.KeyCode == Keys.Delete)
        {
            Grafo.PopNode(SelNode.Id);
            SelNode = null;
            UpdateComboAristas();
            Pnl_graph.Invalidate();
        }     
    }

    // Aristas //
    private void UpdateComboAristas()
    {
        Arista? aristaPrevia = Cmb_aristas.SelectedItem as Arista;
        List<Arista> listaAristas;

        if (Chk_showAll.Checked)
        {
            listaAristas = Grafo.Aristas.ToList();
        }
        else if (SelNode != null)
        {
            listaAristas = Grafo.Aristas.Where(a => a.Origen.Id == SelNode.Id || a.Destino.Id == SelNode.Id).ToList();
        }
        else
        {
            listaAristas = new List<Arista>();
        }

        Cmb_aristas.DataSource = null; //Importante limpiar binding
        Cmb_aristas.DataSource = listaAristas;
        Cmb_aristas.DisplayMember = "GetText";

        //Restaurar seleccion
        if (aristaPrevia != null && listaAristas.Contains(aristaPrevia))
        {
            Cmb_aristas.SelectedItem = aristaPrevia;
        }
        else if (listaAristas.Count > 0)
        {
            Cmb_aristas.SelectedIndex = 0;
        }

        updateAristas();
    }

    private void updateAristas()
    {
        //Actualizar datos
        if (Cmb_aristas.SelectedItem is Arista arista)
        {
            Num_capacidad.Value = arista.Capacidad;
            Num_flujo.Value = arista.Flujo;
        }
    }

    private void Chk_showAll_CheckedChanged(object sender, EventArgs e)
    {
        UpdateComboAristas();
    }

    private void Cmb_aristas_SelectedIndexChanged(object sender, EventArgs e)
    {
        updateAristas();
    }
}

