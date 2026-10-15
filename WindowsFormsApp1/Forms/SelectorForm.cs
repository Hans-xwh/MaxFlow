using MaxFlow.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaxFlow.Forms;

public partial class SelectorForm : Form
{
    public Grafo SelectedGraph { get; private set; }

    public SelectorForm()
    {
        InitializeComponent();
        CargarListaCsvLocales();
    }

    private void CargarListaCsvLocales()
    {
        string carpeta = AppDomain.CurrentDomain.BaseDirectory;
        string[] archivos = Directory.GetFiles(carpeta, "*.csv");
        lst_files.DataSource = archivos.Select(Path.GetFileName).ToList();
    }
    private void CargarGrafo(string ruta)
    {
        if (lst_files.SelectedItem != null)
        {
            SelectedGraph = new Grafo(ruta);
            //SelectedGraph.LoadFromFile(ruta);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

    private void lst_files_DoubleClick(object sender, EventArgs e)
    {
        string nombreArchivo = lst_files.SelectedItem.ToString();
        string rutaCompleta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombreArchivo);
        CargarGrafo(rutaCompleta);
    }

    private void btn_examinar_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "Archivos CSV (*.csv)|*.csv";
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                CargarGrafo(openFileDialog.FileName);
            }
        }
    }

    private void SelectorForm_Load(object sender, EventArgs e)
    {

    }
}



