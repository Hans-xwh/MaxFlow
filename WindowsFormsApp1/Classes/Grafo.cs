#nullable enable

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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

    public Arista(Nodo origen, Nodo destino, int capacidad, int flujo=0, int id=0)
    {
        Origen = origen;
        Destino = destino;
        Capacidad = capacidad;
        CapacidadResidual = capacidad; //Inicialmente la capacidad residual es igual a la capacidad, ni idea si asi funciona esto xd
        Flujo = flujo;
        Id = id;
    }

    public void Draw(Graphics g, bool resaltada = false)
    {
        //Cositas para dibujar
        using Pen Plinea = new Pen(
            resaltada ? Color.Red : Color.DimGray,
            resaltada ? 4 : 2
        );
        using Brush Btexto = new SolidBrush(Color.Black);
        using Font font = new Font("Arial", 12);

        //Calcular Flecha
        using AdjustableArrowCap flecha = new AdjustableArrowCap(6, 6, true);
        Plinea.CustomEndCap = flecha;

        float dx = Destino.Posicion.X - Origen.Posicion.X;
        float dy = Destino.Posicion.Y - Origen.Posicion.Y;
        double distancia = Math.Sqrt(dx * dx + dy * dy);
        Point llegada = Destino.Posicion;

        if (distancia > 0)
        {
            int radioNodo = 20; //Despues mover a que el nodo guarde su radio
            int endX = (int)(Destino.Posicion.X - (dx / distancia) * radioNodo);
            int endY = (int)(Destino.Posicion.Y - (dy / distancia) * radioNodo);    //Im never doing math again :sob:
            llegada = new Point(endX, endY);
        }   

        //Dibujar linea
        g.DrawLine(Plinea, Origen.Posicion, llegada);

        //Dibujar texto
        Point pMedio = new Point((Origen.Posicion.X + Destino.Posicion.X) / 2, (Origen.Posicion.Y + Destino.Posicion.Y) / 2);   //Carajo como odio winforms xd
        string textoCapacidad = $"{Capacidad}/{Flujo}";
        //string textoCapacidad = $"{Capacidad}";

        SizeF size = g.MeasureString(textoCapacidad, font);
        g.FillRectangle(Brushes.White, pMedio.X - (size.Width / 2), pMedio.Y - (size.Height / 2), size.Width, size.Height);
        g.DrawString(textoCapacidad, font, Btexto, pMedio.X - (size.Width / 2), pMedio.Y - (size.Height / 2));
    }

}

public class Grafo
{
    private enum EstadoNodo
    {
        Blanco, //No visitado
        Gris,   //Visitando
        Negro   //Visitado
    }

    public List<Nodo> Nodos { get; private set; } = new List<Nodo>();
    public List<Arista> Aristas { get; private set; } = new List<Arista>();

    public Grafo(string? filePath=null) //Si no se pasa ningun parametro, se crea un grafo en blanco
    {
        string RutaPorDefecto = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "grafo.csv");
        if (filePath != null && File.Exists(filePath))
        {
            LoadFromFile(filePath);
        }
        else if (filePath != null)
        {
            LoadFromFile(RutaPorDefecto); //Intentar cargar el grafo por defecto
        }
    }

    private bool LoadFromFile(string filePath)
    {
        // Nodos: Id,Nombre,X,Y
        // Aristas: Id,OrigenId,DestinoId,Capacidad,Flujo

        if (!File.Exists(filePath))
        {
            GenDefault();
            SaveToFile(filePath);

            return false;   //Retorna false en caso se haya tenido que generar el grafo por defecto.
        }

        Nodos.Clear();
        Aristas.Clear();

        //Reading code stolen from MakeAuth3d_DB :P    
        string mode = "[NODOS]";

        using StreamReader reader = new(filePath);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            //Skip empty lines or comments
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
            line = line.Trim();

            //Split
            string[] parts = line.Split(',');

            if (parts.Length == 1) {
                mode = parts[0].ToUpper();
                continue;
            }

            //Nodos
            if (mode == "[NODOS]" && parts.Length >= 4)
            {
                int id = int.Parse(parts[0]);
                string nombre = parts[1];
                int x = int.Parse(parts[2]);
                int y = int.Parse(parts[3]);
                Nodo nodo = new Nodo(id, nombre, new Point(x, y));
                Nodos.Add(nodo);
            }

            else if (mode == "[ARISTAS]" && parts.Length >= 5)
            {
                int id = int.Parse(parts[0]);
                int origenId = int.Parse(parts[1]);
                int destinoId = int.Parse(parts[2]);
                int capacidad = int.Parse(parts[3]);
                int flujo = int.Parse(parts[4]);

                //Conectar nodos
                Nodo? origen = Nodos.FirstOrDefault(n => n.Id == origenId);
                Nodo? destino = Nodos.FirstOrDefault(n => n.Id == destinoId);

                if (origen != null && destino != null)  //Esto skipea aristas con conexiones invalidas.
                {
                    Arista arista = new Arista(origen, destino, capacidad, flujo, id);
                    Aristas.Add(arista);
                }
            }
        }
        return true;
    }

    public bool SaveToFile(string filePath)
    {
        // Nodos: Id,Nombre,X,Y
        // Aristas: Id,OrigenId,DestinoId,Capacidad,Flujo

        using StreamWriter writer = new(filePath, append:false);    //C# es divertido xd

        // Escribir Nodos //
        writer.WriteLine("[NODOS]");
        writer.WriteLine("# Id,Nombre,X,Y");

        foreach (Nodo nodo in Nodos)
        {
            writer.WriteLine($"{nodo.Id},{nodo.Nombre},{nodo.Posicion.X},{nodo.Posicion.Y}");
        }

        // Escribir Aristas //
        writer.WriteLine("[ARISTAS]");
        writer.WriteLine("# Id,OrigenId,DestinoId,Capacidad,Flujo");
        foreach (Arista arista in Aristas)
        {
            writer.WriteLine($"{arista.Id},{arista.Origen.Id},{arista.Destino.Id},{arista.Capacidad},{arista.Flujo}");
        }
            return true;
    }

    public bool CheckLoops(bool fix=false)
    {
        Dictionary<int, EstadoNodo> estados = Nodos.ToDictionary(n => n.Id, n => EstadoNodo.Blanco);

        //Iterar nodos
        foreach(Nodo nodo in Nodos)
        {
            if (estados[nodo.Id] == EstadoNodo.Blanco)
            {
                if (DFS(nodo, estados, fix))
                {
                    return true;    //Se encontro un bucle
                }
            }
        }

        return false;
    }

    public void GenDefault()
    {
        //Crear 8 Nodos distribuidos en el lienzo
        Nodo n1 = new Nodo(1, "S", new Point(80, 200));   // Fuente
        Nodo n2 = new Nodo(2, "A", new Point(220, 100));
        Nodo n3 = new Nodo(3, "B", new Point(220, 300));
        Nodo n4 = new Nodo(4, "C", new Point(380, 100));
        Nodo n5 = new Nodo(5, "D", new Point(380, 300));
        Nodo n6 = new Nodo(6, "E", new Point(540, 100));
        Nodo n7 = new Nodo(7, "F", new Point(540, 300));
        Nodo n8 = new Nodo(8, "T", new Point(680, 200));   // Sumidero

        this.Nodos.AddRange(new[] { n1, n2, n3, n4, n5, n6, n7, n8 });

        //Crear Aristas con capacidades arbitrarias
        this.Aristas.Add(new Arista(n1, n2, 10, id:1));
        this.Aristas.Add(new Arista(n1, n3, 8, id:2));
        this.Aristas.Add(new Arista(n2, n4, 5, id:3));
        this.Aristas.Add(new Arista(n2, n5, 7, id:4));
        this.Aristas.Add(new Arista(n3, n5, 10, id:5));
        this.Aristas.Add(new Arista(n4, n6, 8, id:6));
        this.Aristas.Add(new Arista(n5, n4, 4, id:7));
        this.Aristas.Add(new Arista(n5, n7, 9, id:8));
        this.Aristas.Add(new Arista(n6, n8, 12, id:9));
        this.Aristas.Add(new Arista(n7, n8, 10, id:10));
    }

    public void GenRandom(int numNodos)
    {
        numNodos = Math.Max(7, numNodos); //Asegurarse de que haya al menos 7 nodos
        numNodos = Math.Min(16, numNodos); //Limitar a 16 nodos 

        //TODO implementar esto xd
    }

    public void Draw(Graphics g, IEnumerable<Arista>? aristasResaltadas = null)
    {
        g.SmoothingMode =
            System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // Convertimos las aristas resaltadas en un conjunto
        // para poder buscarlas rápidamente.
        HashSet<Arista> resaltadas =
            aristasResaltadas != null
                ? new HashSet<Arista>(aristasResaltadas)
                : new HashSet<Arista>();


        // Dibujar aristas primero
        foreach (Arista arista in Aristas)
        {
            bool resaltada =
                resaltadas.Contains(arista);

            arista.Draw(g, resaltada);
        }


        // Dibujar nodos después
        foreach (Nodo nodo in Nodos)
        {
            nodo.Draw(g);
        }
    }

    private bool DFS(Nodo nodo, Dictionary<int, EstadoNodo> estados, bool fix)
    {
        estados[nodo.Id] = EstadoNodo.Gris; //Como extraño mis enums en python

        //Lista de aristas salientes al nodo actual
        List<Arista> salientes = Aristas.Where(a => a.Origen.Id == nodo.Id).ToList();

        foreach(Arista arista in salientes)
        {
            Nodo vecino = arista.Destino;

            //Caso vecino gris, ciclo encontrado
            if (estados[vecino.Id] == EstadoNodo.Gris)
            {
                if (fix)
                {
                    Aristas.Remove(arista);
                }
                return true;
            }

            //Caso vecino blanco, nodo sin explorar
            else if (estados[vecino.Id] == EstadoNodo.Blanco)
            {
                if (DFS(vecino, estados, fix))
                {
                    return true;
                }
            }
        }

        //No se encontraron ciclos
        estados[nodo.Id] = EstadoNodo.Negro;
        return false;
    }

}
