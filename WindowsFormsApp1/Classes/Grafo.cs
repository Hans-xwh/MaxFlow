using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxFlow.Model;

public class Nodo
{
    public int Id { get; set; }
    //public int Capacidad { get; set; } //Capacidad del nodo
    public string Nombre { get; set; } //Nombre del nodo    (Recomendado usar una letra)
    public Point Posicion { get; set; } //Posicion en que se dibuja

    private int Radio = 20; //Radio en pixeles

    public Nodo(int id, string nombre, Point posicion)
    {
        Id = id;
        Nombre = nombre;
        Posicion = posicion;
    }

    public void Draw(Graphics g)
    {
        //Cositas para dibujar
        using Brush Brelleno = new SolidBrush(Color.LightBlue);
        using Pen Pborde = new Pen(Color.Black, 2);
        using Brush Btexto = new SolidBrush(Color.Black);
        using Font font = new Font("Arial", 12);

        //Dibujar circulito
        Rectangle boundingBox = new Rectangle(Posicion.X - Radio, Posicion.Y - Radio, 2 * Radio, 2 * Radio);
        g.FillEllipse(Brelleno, boundingBox);   //Por que esta funcion no puede tomar solo el radio es un misterio misterioso
        g.DrawEllipse(Pborde, boundingBox);

        //Dibujar texto (aun no implementado xd|)
        SizeF textSize = g.MeasureString(Nombre, font); //Cajita autotamaño para el texto
        g.DrawString(Nombre, font, Btexto, Posicion.X - textSize.Width / 2, Posicion.Y - textSize.Height / 2);
    }
}

public class Arista
{
    public int Id { get; set; }
    public Nodo Origen { get; set; }
    public Nodo Destino { get; set; }
    public int Flujo { get; set; } = 0; //Flujo de la arista
    public int Capacidad { get; set; } //Capacidad de la arista
    public int CapacidadResidual { get; set; } //Capacidad residual de la arista

    public Arista(Nodo origen, Nodo destino, int capacidad)
    {
        Origen = origen;
        Destino = destino;
        Capacidad = capacidad;
        CapacidadResidual = capacidad; //Inicialmente la capacidad residual es igual a la capacidad, ni idea si asi funciona esto xd
    }

    public void Draw(Graphics g)
    {
        //Cositas para dibujar
        using Pen Plinea = new Pen(Color.DimGray, 2);
        using Brush Btexto = new SolidBrush(Color.Black);
        using Font font = new Font("Arial", 12);

        //Dibujar linea
        g.DrawLine(Plinea, Origen.Posicion, Destino.Posicion);

        //Dibujar texto
        Point pMedio = new Point((Origen.Posicion.X + Destino.Posicion.X) / 2, (Origen.Posicion.Y + Destino.Posicion.Y) / 2);   //Carajo como odio winforms xd
        //string textoCapacidad = $"{Flujo}/{Capacidad}";
        string textoCapacidad = $"{Capacidad}";

        SizeF size = g.MeasureString(textoCapacidad, font);
        g.FillRectangle(Brushes.White, pMedio.X - (size.Width / 2), pMedio.Y - (size.Height / 2), size.Width, size.Height);
        g.DrawString(textoCapacidad, font, Btexto, pMedio.X - (size.Width / 2), pMedio.Y - (size.Height / 2));
    }

}

public class Grafo
{
    public List<Nodo> Nodos { get; private set; } = new List<Nodo>();
    public List<Arista> Aristas { get; private set; } = new List<Arista>();

    public bool CheckLoops()
    {
        //TODO: Implementar la verificación de bucles en el grafo xd
        return false;
    }

    public void Draw(Graphics g)
    {
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; //Holly nutz!

        //Aristas primero por q empiezan desde el centro del nodo y si no se ve raro
        foreach(Arista arista in Aristas)
        {
            arista.Draw(g);
        }

        foreach (Nodo nodo in Nodos)
        {
            nodo.Draw(g);
        }

    }

}
