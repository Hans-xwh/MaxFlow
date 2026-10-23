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

namespace MaxFlow.Forms;

public partial class FordFulkersonForm : Form
{
    private Grafo Grafo;

    public FordFulkersonForm()
    {
        InitializeComponent();
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
}
