#nullable enable

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

namespace MaxFlow.Model;

public class Nodo
{
    public int Id { get; set; }
    //public int Capacidad { get; set; } //Capacidad del nodo
    public string Nombre { get; set; } //Nombre del nodo    (Recomendado usar una letra)
    public Point Posicion { get; set; } //Posicion en que se dibuja

    public int Radio { get; private set; } = 20; //Radio en pixeles

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

    //Utils
    internal bool IsInside(Point p)
    {
        double dx = p.X - Posicion.X;
        double dy = p.Y - Posicion.Y;
        return (dx * dx + dy * dy) <= (Radio * Radio);  
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

    public void Draw(
        Graphics g,
        bool resaltada = false,
        float desplazamiento = 0,
        float ladoEtiqueta = 1,
        PointF? centroEtiqueta = null
        )
    {
        using Pen Plinea = new Pen(
            resaltada ? Color.Red : Color.DimGray,
            resaltada ? 4 : 2);

        using Brush Btexto = new SolidBrush(Color.Black);
        using Font font = new Font("Arial", 12);

        using AdjustableArrowCap flecha =
            new AdjustableArrowCap(6, 6, true);

        Plinea.CustomEndCap = flecha;

        float dx = Destino.Posicion.X - Origen.Posicion.X;
        float dy = Destino.Posicion.Y - Origen.Posicion.Y;

        double distancia = Math.Sqrt(dx * dx + dy * dy);

        if (distancia <= 0)
            return;

        float ux = (float)(dx / distancia);
        float uy = (float)(dy / distancia);

        // Vector perpendicular a la arista.
        float nx = -uy;
        float ny = ux;


        PointF inicio;
        PointF llegada;

        if (Math.Abs(desplazamiento) < 0.5f)
        {
            // Arista recta: termina en el borde de cada nodo.
            inicio = new PointF(
                Origen.Posicion.X + ux * 20,
                Origen.Posicion.Y + uy * 20);

            llegada = new PointF(
                Destino.Posicion.X - ux * 20,
                Destino.Posicion.Y - uy * 20);

            g.DrawLine(Plinea, inicio, llegada);
        }
        else
        {
            // Curvatura reforzada.
            float curva = desplazamiento * 1.8f;

            // Puntos de control preliminares.
            PointF control1 = new PointF(
                Origen.Posicion.X + ux * (float)distancia / 3
                    + nx * curva,
                Origen.Posicion.Y + uy * (float)distancia / 3
                    + ny * curva);

            PointF control2 = new PointF(
                Destino.Posicion.X - ux * (float)distancia / 3
                    + nx * curva,
                Destino.Posicion.Y - uy * (float)distancia / 3
                    + ny * curva);

            // Dirección de salida desde el centro del origen.
            float sx = control1.X - Origen.Posicion.X;
            float sy = control1.Y - Origen.Posicion.Y;
            float longitudSalida = (float)Math.Sqrt(sx * sx + sy * sy);

            if (longitudSalida < 0.001f)
            {
                sx = ux;
                sy = uy;
                longitudSalida = 1;
            }

            inicio = new PointF(
                Origen.Posicion.X + sx / longitudSalida * 20,
                Origen.Posicion.Y + sy / longitudSalida * 20);

            // Dirección de llegada hacia el centro del destino.
            float ex = control2.X - Destino.Posicion.X;
            float ey = control2.Y - Destino.Posicion.Y;
            float longitudLlegada = (float)Math.Sqrt(ex * ex + ey * ey);

            if (longitudLlegada < 0.001f)
            {
                ex = -ux;
                ey = -uy;
                longitudLlegada = 1;
            }

            llegada = new PointF(
                Destino.Posicion.X + ex / longitudLlegada * 20,
                Destino.Posicion.Y + ey / longitudLlegada * 20);

            g.DrawBezier(
                Plinea, inicio, control1, control2, llegada);
        }


        // Posición de la etiqueta: centro aproximado de la curva,
        // con un desplazamiento adicional para evitar la línea.
        float desplazamientoEtiqueta =
            desplazamiento * 0.75f + 14 * ladoEtiqueta;

        float centroX;
        float centroY;

        if (centroEtiqueta.HasValue)
        {
            centroX = centroEtiqueta.Value.X;
            centroY = centroEtiqueta.Value.Y;
        }
        else
        {
            float desplazamientoEtiquetaLocal =
                desplazamiento * 0.75f + 14 * ladoEtiqueta;

            centroX =
                (Origen.Posicion.X + Destino.Posicion.X) / 2f
                + nx * desplazamientoEtiquetaLocal;

            centroY =
                (Origen.Posicion.Y + Destino.Posicion.Y) / 2f
                + ny * desplazamientoEtiquetaLocal;
        }

        string textoCapacidad = $"{Capacidad}/{Flujo}";
        SizeF size = g.MeasureString(textoCapacidad, font);

        float textoX = centroX - size.Width / 2;
        float textoY = centroY - size.Height / 2;

        g.FillRectangle(Brushes.White, textoX - 2, textoY - 1, size.Width + 4, size.Height + 2);
        g.DrawString( textoCapacidad, font, Btexto, textoX, textoY);
    }

    public override String ToString()
    {
        return $"{Origen.Nombre} --> {Destino.Nombre}  [{Flujo}/{Capacidad}]";
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

    public String FilePath { get; set; } = "";

    public List<Nodo> Nodos { get; private set; } = new List<Nodo>();
    public List<Arista> Aristas { get; private set; } = new List<Arista>();

    public Grafo(string? filePath = null, bool generate_blank=false) //Si no se pasa ningun parametro, se crea un grafo en blanco
    {
        if (filePath == null || filePath == "")
        {
            FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "grafo.csv");
        }
        else
        {
            FilePath = filePath;
        }
        LoadFromFile(FilePath, generate_blank);
    }

    public Grafo()
    {

    }

    public bool LoadFromFile(string? filePath, bool generate_blank=false)
    {
        FilePath = filePath ?? FilePath;

        // Nodos: Id,Nombre,X,Y
        // Aristas: Id,OrigenId,DestinoId,Capacidad,Flujo

        if (!File.Exists(FilePath))
        {
            if (!generate_blank) { GenDefault(); }
            SaveToFile(FilePath);

            return false;   //Retorna false en caso se haya tenido que generar el grafo por defecto.
        }

        Nodos.Clear();
        Aristas.Clear();

        //Reading code stolen from MakeAuth3d_DB :P    
        string mode = "[NODOS]";

        using StreamReader reader = new(FilePath);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            //Skip empty lines or comments
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
            line = line.Trim();

            //Split
            string[] parts = line.Split(',');

            if (parts.Length == 1)
            {
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

        using StreamWriter writer = new(filePath, append: false);    //C# es divertido xd

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

    public bool CheckLoops(bool fix = false)
    {
        Dictionary<int, EstadoNodo> estados = Nodos.ToDictionary(n => n.Id, n => EstadoNodo.Blanco);

        //Iterar nodos
        foreach (Nodo nodo in Nodos)
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
        this.Aristas.Add(new Arista(n1, n2, 10, id: 1));
        this.Aristas.Add(new Arista(n1, n3, 8, id: 2));
        this.Aristas.Add(new Arista(n2, n4, 5, id: 3));
        this.Aristas.Add(new Arista(n2, n5, 7, id: 4));
        this.Aristas.Add(new Arista(n3, n5, 10, id: 5));
        this.Aristas.Add(new Arista(n4, n6, 8, id: 6));
        this.Aristas.Add(new Arista(n5, n4, 4, id: 7));
        this.Aristas.Add(new Arista(n5, n7, 9, id: 8));
        this.Aristas.Add(new Arista(n6, n8, 12, id: 9));
        this.Aristas.Add(new Arista(n7, n8, 10, id: 10));
    }



    private void OrganizarNodosPorNiveles(int ancho, int alto)
    {
        if (Nodos.Count < 3)
            return;

        int margenX = 55;
        int margenY = 45;

        Nodo origen = Nodos[0];
        Nodo destino = Nodos[Nodos.Count - 1];

        int cantidadIntermedios = Nodos.Count - 2;

        int cantidadNiveles =
            cantidadIntermedios <= 6 ? 2 :
            cantidadIntermedios <= 10 ? 3 : 4;

        List<List<Nodo>> niveles = new List<List<Nodo>>();

        for (int i = 0; i < cantidadNiveles; i++)
            niveles.Add(new List<Nodo>());

        // Distribuir los nodos intermedios en columnas.
        for (int i = 1; i < Nodos.Count - 1; i++)
        {
            int indiceNivel =
                (i - 1) * cantidadNiveles / cantidadIntermedios;

            indiceNivel = Math.Min(
                indiceNivel, cantidadNiveles - 1);

            niveles[indiceNivel].Add(Nodos[i]);
        }

        int separacionX =
            (ancho - 2 * margenX) / (cantidadNiveles + 1);

        origen.Posicion = new Point(margenX, alto / 2);
        destino.Posicion = new Point(ancho - margenX, alto / 2);

        // Posicionar inicialmente los nodos.
        for (int i = 0; i < cantidadNiveles; i++)
        {
            List<Nodo> grupo = niveles[i];

            int posX = margenX + (i + 1) * separacionX;

            for (int j = 0; j < grupo.Count; j++)
            {
                int posY = margenY +
                    (int)((j + 1.0) * (alto - 2 * margenY)
                    / (grupo.Count + 1));

                grupo[j].Posicion = new Point(posX, posY);
            }
        }

        // Reordenar varias veces según las conexiones.
        for (int pasada = 0; pasada < 8; pasada++)
        {
            // Barrido de izquierda a derecha.
            for (int i = 0; i < cantidadNiveles; i++)
            {
                niveles[i].Sort((a, b) =>
                {
                    double centroA = CentroVecinos(a, niveles[i + 1 < cantidadNiveles ? i + 1 : i]);
                    double centroB = CentroVecinos(b, niveles[i + 1 < cantidadNiveles ? i + 1 : i]);

                    return centroA.CompareTo(centroB);
                });

                ActualizarAlturas(
                    niveles[i], margenY, alto);
            }

            // Barrido de derecha a izquierda.
            for (int i = cantidadNiveles - 1; i >= 0; i--)
            {
                niveles[i].Sort((a, b) =>
                {
                    int indiceAdyacente = i > 0 ? i - 1 : i;

                    double centroA = CentroVecinos(
                        a, niveles[indiceAdyacente]);

                    double centroB = CentroVecinos(
                        b, niveles[indiceAdyacente]);

                    return centroA.CompareTo(centroB);
                });

                ActualizarAlturas(
                    niveles[i], margenY, alto);
            }
        }
    }


    private double CentroVecinos(
        Nodo nodo, List<Nodo> nivelAdyacente)
    {
        double suma = 0;
        int cantidad = 0;

        foreach (Arista arista in Aristas)
        {
            Nodo vecino = null;

            if (arista.Origen == nodo &&
                nivelAdyacente.Contains(arista.Destino))
            {
                vecino = arista.Destino;
            }
            else if (arista.Destino == nodo &&
                     nivelAdyacente.Contains(arista.Origen))
            {
                vecino = arista.Origen;
            }

            if (vecino != null)
            {
                suma += vecino.Posicion.Y;
                cantidad++;
            }
        }

        return cantidad > 0
            ? suma / cantidad
            : nodo.Posicion.Y;
    }

    private void ActualizarAlturas(
        List<Nodo> grupo, int margenY, int alto)
    {
        int alturaDisponible = alto - 2 * margenY;

        for (int j = 0; j < grupo.Count; j++)
        {
            int posY = margenY +
                (int)((j + 1.0) * alturaDisponible
                / (grupo.Count + 1));

            grupo[j].Posicion = new Point(
                grupo[j].Posicion.X, posY);
        }
    }

    private double CentroPredecesores(
        Nodo nodo, List<Nodo> nivelAnterior)
    {
        double suma = 0;
        int cantidad = 0;

        foreach (Arista arista in Aristas)
        {
            if (arista.Destino == nodo &&
                nivelAnterior.Contains(arista.Origen))
            {
                suma += arista.Origen.Posicion.Y;
                cantidad++;
            }
        }

        return cantidad > 0
            ? suma / cantidad
            : nodo.Posicion.Y;
    }

    public void GenRandom(int numNodos)
    {
        // Limitar la cantidad de nodos al rango permitido.
        numNodos = Math.Max(7, Math.Min(16, numNodos));

        // Limpiar el grafo anterior.
        Nodos.Clear();
        Aristas.Clear();

        Random random = new Random();


        int margenX = 60;
        int margenY = 60;
        int ancho = 1000;
        int alto = 430;

        // S y T estarán en extremos opuestos.
        int xInicio = margenX;
        int xFin = ancho - margenX;

        // Los nodos intermedios se distribuirán en columnas.
        int cantidadIntermedios = numNodos - 2;
        int columnas = Math.Min(4,
            (int)Math.Ceiling(Math.Sqrt(cantidadIntermedios)));

        int filas = (int)Math.Ceiling(
            (double)cantidadIntermedios / columnas);

        int separacionX = (xFin - xInicio) / (columnas + 1);
        int separacionY = filas > 1
            ? (alto - 2 * margenY) / (filas - 1)
            : 0;

        // Crear el nodo inicial S.
        Nodos.Add(new Nodo(1, "S",
            new Point(xInicio, alto / 2)));

        // Crear los nodos intermedios.
        for (int i = 0; i < cantidadIntermedios; i++)
        {
            int columna = i % columnas;
            int fila = i / columnas;

            int x = xInicio + (columna + 1) * separacionX;

            int y;
            if (filas == 1)
                y = alto / 2;
            else
                y = margenY + fila * separacionY;

            char nombre = (char)('A' + i);

            Nodos.Add(new Nodo(i + 2, nombre.ToString(),
                new Point(x, y)));
        }

        // Crear el nodo final T.
        Nodos.Add(new Nodo(numNodos, "T",
            new Point(xFin, alto / 2)));


        int idArista = 1;

        // Crear un camino garantizado desde S hasta T.
        for (int i = 0; i < numNodos - 1; i++)
        {
            int capacidad = random.Next(1, 21);

            Arista arista = new Arista(
                Nodos[i],
                Nodos[i + 1],
                capacidad,
                id: idArista++
            );

            Aristas.Add(arista);
        }


        // Definir límites de aristas según la cantidad de nodos.
        int minimoAristas = numNodos + 1;
        int maximoAristas = Math.Min(
            numNodos + 4,
            numNodos * (numNodos - 1) / 2
        );

        // Crear conexiones candidatas sin formar ciclos.
        List<(int origen, int destino)> candidatas =
            new List<(int origen, int destino)>();

        for (int i = 0; i < numNodos - 2; i++)
        {
            for (int j = i + 2; j < numNodos; j++)
            {
                candidatas.Add((i, j));
            }
        }

        // Mezclar las conexiones candidatas.
        for (int i = candidatas.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);

            var temporal = candidatas[i];
            candidatas[i] = candidatas[j];
            candidatas[j] = temporal;
        }

        // Elegir aleatoriamente la cantidad total de aristas.
        int objetivoAristas = random.Next(
            minimoAristas,
            maximoAristas + 1
        );

        int adicionalesNecesarias =
            objetivoAristas - Aristas.Count;

        // Agregar las conexiones seleccionadas.
        for (int i = 0;
             i < candidatas.Count && adicionalesNecesarias > 0;
             i++)
        {
            int origen = candidatas[i].origen;
            int destino = candidatas[i].destino;

            int capacidad = random.Next(1, 21);

            Arista arista = new Arista(
                Nodos[origen],
                Nodos[destino],
                capacidad,
                id: idArista++
            );

            Aristas.Add(arista);
            adicionalesNecesarias--;
        }


        // Inicializar los flujos y las capacidades residuales.
        foreach (Arista arista in Aristas)
        {
            arista.Flujo = 0;
            arista.CapacidadResidual = arista.Capacidad;
        }

        // Organizar nodos por niveles
        OrganizarNodosPorNiveles(1000,600);

        //Guardar grafo
        FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Grafo Random.csv");
        this.SaveToFile(FilePath);
    }


    private Dictionary<Arista, (float offset, float ladoEtiqueta)>
        CalcularDesplazamientosAristas()
    {
        var resultado =
            new Dictionary<Arista, (float offset, float ladoEtiqueta)>();

        foreach (Arista arista in Aristas)
            resultado[arista] = (0, 1);

        List<List<Arista>> grupos = new List<List<Arista>>();

        foreach (Arista actual in Aristas)
        {
            List<Arista> grupoEncontrado = null;

            foreach (List<Arista> grupo in grupos)
            {
                if (grupo.Any(otra =>
                    SonCasiParalelasYCercanas(actual, otra)))
                {
                    grupoEncontrado = grupo;
                    break;
                }
            }

            if (grupoEncontrado == null)
            {
                grupos.Add(new List<Arista> { actual });
            }
            else
            {
                grupoEncontrado.Add(actual);
            }
        }

        foreach (List<Arista> grupo in grupos)
        {
            if (grupo.Count < 2)
                continue;

            for (int i = 0; i < grupo.Count; i++)
            {
                float offset =
                    (i - (grupo.Count - 1) / 2f) * 26f;

                float ladoEtiqueta = offset < 0 ? -1f : 1f;

                resultado[grupo[i]] = (offset, ladoEtiqueta);
            }
        }

        foreach (Arista arista in Aristas)
        {
            PointF inicio = new PointF(
                arista.Origen.Posicion.X,
                arista.Origen.Posicion.Y);

            PointF fin = new PointF(
                arista.Destino.Posicion.X,
                arista.Destino.Posicion.Y);

            float dx = fin.X - inicio.X;
            float dy = fin.Y - inicio.Y;

            foreach (Nodo nodo in Nodos)
            {
                if (nodo == arista.Origen ||
                    nodo == arista.Destino)
                    continue;

                PointF centroNodo = new PointF(
                    nodo.Posicion.X,
                    nodo.Posicion.Y);

                double distancia = DistanciaPuntoSegmento(
                    centroNodo, inicio, fin);

                if (distancia < nodo.Radio + 10)
                {
                    double cruz =
                        dx * (nodo.Posicion.Y - inicio.Y) -
                        dy * (nodo.Posicion.X - inicio.X);

                    float lado = cruz >= 0 ? -1f : 1f;

                    var ajuste = resultado[arista];

                    float magnitud = Math.Max(
                        36f, Math.Abs(ajuste.offset) + 24f);

                    resultado[arista] = (
                        lado * magnitud,
                        ajuste.ladoEtiqueta);

                    break;
                }
            }
        }
        return resultado;
    }

    private bool SonCasiParalelasYCercanas(
        Arista a, Arista b)
    {
        double ax = a.Destino.Posicion.X - a.Origen.Posicion.X;
        double ay = a.Destino.Posicion.Y - a.Origen.Posicion.Y;

        double bx = b.Destino.Posicion.X - b.Origen.Posicion.X;
        double by = b.Destino.Posicion.Y - b.Origen.Posicion.Y;

        double largoA = Math.Sqrt(ax * ax + ay * ay);
        double largoB = Math.Sqrt(bx * bx + by * by);

        if (largoA < 1 || largoB < 1)
            return false;

        // Considerar paralelas las aristas con dirección muy similar.
        double coseno = Math.Abs(
            (ax * bx + ay * by) / (largoA * largoB));

        if (coseno < 0.985)
            return false;

        double medioAX =
            (a.Origen.Posicion.X + a.Destino.Posicion.X) / 2.0;

        double medioAY =
            (a.Origen.Posicion.Y + a.Destino.Posicion.Y) / 2.0;

        double medioBX =
            (b.Origen.Posicion.X + b.Destino.Posicion.X) / 2.0;

        double medioBY =
            (b.Origen.Posicion.Y + b.Destino.Posicion.Y) / 2.0;

        // Distancia perpendicular entre los puntos medios
        // y las líneas que contienen las aristas.
        double distanciaAB = Math.Abs(
            ax * (medioBY - a.Origen.Posicion.Y)
            - ay * (medioBX - a.Origen.Posicion.X))
            / largoA;

        double distanciaBA = Math.Abs(
            bx * (medioAY - b.Origen.Posicion.Y)
            - by * (medioAX - b.Origen.Posicion.X))
            / largoB;

        if (distanciaAB > 22 || distanciaBA > 22)
            return false;

        // Comprobar que sus recorridos se superponen
        // longitudinalmente al menos 45 píxeles.
        double ux = ax / largoA;
        double uy = ay / largoA;

        double proyeccionB1 =
            (b.Origen.Posicion.X - a.Origen.Posicion.X) * ux
            + (b.Origen.Posicion.Y - a.Origen.Posicion.Y) * uy;

        double proyeccionB2 =
            (b.Destino.Posicion.X - a.Origen.Posicion.X) * ux
            + (b.Destino.Posicion.Y - a.Origen.Posicion.Y) * uy;

        double inicioB = Math.Min(proyeccionB1, proyeccionB2);
        double finB = Math.Max(proyeccionB1, proyeccionB2);

        double solapamiento =
            Math.Min(largoA, finB) - Math.Max(0, inicioB);

        return solapamiento >= 45;
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

        // Calcular separaciones y posiciones de las etiquetas.
        var desplazamientos = CalcularDesplazamientosAristas();

        var posicionesEtiquetas =
            CalcularPosicionesEtiquetas(g, desplazamientos);

        // Dibujar aristas con sus ajustes visuales.
        foreach (Arista arista in Aristas)
        {
            bool resaltada = resaltadas.Contains(arista);
            var ajuste = desplazamientos[arista];

            arista.Draw(
                g,
                resaltada,
                ajuste.offset,
                ajuste.ladoEtiqueta,
                posicionesEtiquetas[arista]);
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

        foreach (Arista arista in salientes)
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

    public Nodo AddNode(string name, Point p)
    {
        int id = Nodos.Count() + 1;
        Nodo newNodo = new(id, name, p);
        Nodos.Add(newNodo);

        if (Nodos.Count >= 2)
        {
            (Nodos[id - 2], Nodos[id - 1]) = (Nodos[id - 1], Nodos[id - 2]);
        }
        return newNodo;
    }

    public void PopNode(int nid)
    {
        Nodo? n = Nodos.FirstOrDefault(x => x.Id == nid);   //C# es lo mas hermoso del mundo xd

        if (n != null)
        {
            Aristas.RemoveAll(a => a.Origen == n || a.Destino == n);

            Nodos.Remove(n);
        }
    }

    public void AutoSort(Nodo? f = null, Nodo? s = null)
    {
        if (Nodos.Count <= 1) return;

        Nodo fuente = f ?? Nodos.First();
        Nodo sumidero = s ?? Nodos.Last();

        //Caso sumidero = fuente
        if (fuente == sumidero && Nodos.Count > 1)
        {
            sumidero = Nodos.Last();
        }

        List<Nodo> tmpNodos = Nodos.Where(n => n != fuente && n != sumidero).ToList();

        //Reconstruir
        Nodos.Clear();
        Nodos.Add(fuente);
        Nodos.AddRange(tmpNodos);
        Nodos.Add(sumidero);

        //IDs
        for (int i = 0; i < Nodos.Count; i++)
        {
            Nodos[i].Id = i + 1;
        }

        for (int i = 0; i < Aristas.Count; i++)
        {
            Aristas[i].Id = i + 1;
        }
    }

    private Dictionary<Arista, PointF> CalcularPosicionesEtiquetas(
        Graphics g,
        Dictionary<Arista, (float offset, float ladoEtiqueta)> ajustes)
    {
        const float fuerzaCurva = 1.8f;

        using Font font = new Font("Arial", 12);

        Dictionary<Arista, PointF> resultado =
            new Dictionary<Arista, PointF>();

        List<RectangleF> etiquetasOcupadas =
            new List<RectangleF>();

        List<RectangleF> rectangulosNodos = Nodos
            .Select(n => new RectangleF(
                n.Posicion.X - n.Radio - 5,
                n.Posicion.Y - n.Radio - 5,
                2 * (n.Radio + 5),
                2 * (n.Radio + 5)))
            .ToList();

        RectangleF area = g.VisibleClipBounds;

        // Priorizar las aristas largas, que suelen tener
        // más posibilidades de recibir etiquetas legibles.
        List<Arista> orden = Aristas
            .OrderByDescending(a =>
            {
                double dx = a.Destino.Posicion.X -
                            a.Origen.Posicion.X;
                double dy = a.Destino.Posicion.Y -
                            a.Origen.Posicion.Y;

                return dx * dx + dy * dy;
            })
            .ToList();

        foreach (Arista arista in orden)
        {
            float dx = arista.Destino.Posicion.X -
                       arista.Origen.Posicion.X;

            float dy = arista.Destino.Posicion.Y -
                       arista.Origen.Posicion.Y;

            double longitud = Math.Sqrt(dx * dx + dy * dy);

            if (longitud < 1)
            {
                resultado[arista] = arista.Origen.Posicion;
                continue;
            }

            float ux = (float)(dx / longitud);
            float uy = (float)(dy / longitud);

            float nx = -uy;
            float ny = ux;

            float medioX =
                (arista.Origen.Posicion.X +
                 arista.Destino.Posicion.X) / 2f;

            float medioY =
                (arista.Origen.Posicion.Y +
                 arista.Destino.Posicion.Y) / 2f;

            var ajuste = ajustes[arista];

            // El centro de la curva se desplaza respecto
            // al centro de la línea original.
            float centroCurva =
                ajuste.offset * fuerzaCurva * 0.75f;

            string texto = $"{arista.Capacidad}/{arista.Flujo}";
            SizeF tam = g.MeasureString(texto, font);

            float[] posicionesPerpendiculares =
            {
            16, -16, 30, -30, 44, -44,
            58, -58, 74, -74, 92, -92,
            112, -112
        };

            float[] posicionesLongitudinales =
            {
            0, 26, -26, 52, -52,
            78, -78, 104, -104
        };

            double mejorPuntuacion = double.MaxValue;
            PointF mejorPosicion = new PointF(medioX, medioY);
            RectangleF mejorRectangulo = RectangleF.Empty;

            foreach (float avance in posicionesLongitudinales)
            {
                foreach (float desplazamientoTexto
                         in posicionesPerpendiculares)
                {
                    float cx = medioX + ux * avance
                        + nx * (centroCurva + desplazamientoTexto);

                    float cy = medioY + uy * avance
                        + ny * (centroCurva + desplazamientoTexto);

                    RectangleF rect = new RectangleF(
                        cx - tam.Width / 2 - 2,
                        cy - tam.Height / 2 - 1,
                        tam.Width + 4,
                        tam.Height + 2);

                    double puntuacion =
                        Math.Abs(avance) * 0.05
                        + Math.Abs(desplazamientoTexto) * 0.02;

                    // Evitar los bordes del área de dibujo.
                    if (!area.Contains(rect))
                        puntuacion += 20000;

                    // Evitar superponerse con los nodos.
                    foreach (RectangleF nodoRect in rectangulosNodos)
                    {
                        if (rect.IntersectsWith(nodoRect))
                            puntuacion += 10000;
                    }

                    // Evitar etiquetas ya colocadas.
                    foreach (RectangleF ocupada in etiquetasOcupadas)
                    {
                        if (rect.IntersectsWith(ocupada))
                            puntuacion += 15000;
                    }

                    // Evitar colocar el texto encima de otra arista.
                    double radioColision = Math.Sqrt(
                        Math.Pow(tam.Width / 2 + 3, 2) +
                        Math.Pow(tam.Height / 2 + 3, 2));

                    foreach (Arista otra in Aristas)
                    {
                        if (otra == arista)
                            continue;

                        double distancia = DistanciaPuntoSegmento(
                            new PointF(cx, cy),
                            otra.Origen.Posicion,
                            otra.Destino.Posicion);

                        if (distancia < radioColision)
                            puntuacion += 5000;
                    }

                    if (puntuacion < mejorPuntuacion)
                    {
                        mejorPuntuacion = puntuacion;
                        mejorPosicion = new PointF(cx, cy);
                        mejorRectangulo = rect;
                    }

                    // Si encontramos una posición libre, basta.
                    if (puntuacion < 1)
                        break;
                }

                if (mejorPuntuacion < 1)
                    break;
            }

            resultado[arista] = mejorPosicion;

            // Añadir margen para mantener separación entre textos.
            etiquetasOcupadas.Add(new RectangleF(
                mejorRectangulo.X - 3,
                mejorRectangulo.Y - 2,
                mejorRectangulo.Width + 6,
                mejorRectangulo.Height + 4));
        }

        return resultado;
    }

    private double DistanciaPuntoSegmento(
        PointF punto, PointF inicio, PointF fin)
    {
        double dx = fin.X - inicio.X;
        double dy = fin.Y - inicio.Y;

        double longitudCuadrada = dx * dx + dy * dy;

        if (longitudCuadrada < 0.0001)
        {
            double ex = punto.X - inicio.X;
            double ey = punto.Y - inicio.Y;
            return Math.Sqrt(ex * ex + ey * ey);
        }

        double t =
            ((punto.X - inicio.X) * dx +
             (punto.Y - inicio.Y) * dy) / longitudCuadrada;

        t = Math.Max(0, Math.Min(1, t));

        double px = inicio.X + t * dx;
        double py = inicio.Y + t * dy;

        double vx = punto.X - px;
        double vy = punto.Y - py;

        return Math.Sqrt(vx * vx + vy * vy);
    }

}
