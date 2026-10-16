#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MaxFlow.Model;

namespace MaxFlow.Extra;

public class Dijkstra
{
    public Grafo Grafo { get; private set; }
    public Nodo Origen { get; private set; }
    public Nodo Destino { get; private set; }

    //Estructuras de trabajo
    public Dictionary<int, int> Distancias { get; private set; }
    public Dictionary<int, Nodo?> Predecesores { get; private set; }
    public HashSet<int> Visitados { get; private set; }

    public Nodo? NodoActual { get; private set; }
    public bool Terminado { get; private set; } = false;

    public Dijkstra(Grafo grafo)
    {
        Grafo = grafo;
        Origen = grafo.Nodos.First();
        Destino = grafo.Nodos.Last();

        Distancias = new Dictionary<int, int>();
        Predecesores = new Dictionary<int, Nodo?>();
        Visitados = new HashSet<int>();

        //Init
        foreach (Nodo nodo in grafo.Nodos)
        {
            Distancias[nodo.Id] = int.MaxValue;
            Predecesores[nodo.Id] = null;
        }

        Distancias[Origen.Id] = 0;
    }

    private Nodo? GetNodoMiniDist()
    {
        int min = int.MaxValue;
        Nodo? nodoMin = null;

        foreach (var nodo in Grafo.Nodos)
        {
            if (!Visitados.Contains(nodo.Id) && Distancias[nodo.Id] <= min)
            {
                min = Distancias[nodo.Id];
                nodoMin = nodo;
            }
        }
        return nodoMin;
    }

    public bool Step()
    {
        if (Terminado) { return false; }

        NodoActual = GetNodoMiniDist();

        //Caso 1: No hay mas nodos disponibles
        if (NodoActual == null || Distancias[NodoActual.Id] == int.MaxValue)
        {
            Terminado = true;
            return false;
        }

        //Caso 2: Destino alcanzado
        if (NodoActual == Destino)
        {
            Visitados.Add(NodoActual.Id);
            Terminado = true;
            return false;
        }

        Visitados.Add(NodoActual.Id);

        //Visitar otras aristas
        List<Arista> salientes = Grafo.Aristas.Where(a => a.Origen.Id == NodoActual.Id).ToList();
        foreach(Arista arista in salientes)
        {
            Nodo vecino = arista.Destino;
            
            if(!Visitados.Contains(vecino.Id) && Distancias[NodoActual.Id] != int.MaxValue)
            {
                int newDist = Distancias[NodoActual.Id] + arista.Capacidad; //Se usa capacidad como el peso.

                if (newDist < Distancias[vecino.Id])
                {
                    Distancias[vecino.Id] = newDist;
                    Predecesores[vecino.Id] = NodoActual;
                }
            }
        }
        return true;
    }

    public List<Nodo> GetFastSolve()
    {
        List<Nodo> camino = new List<Nodo>();
        Nodo? actual = Destino;

        if (Predecesores[Destino.Id] == null && Destino.Id != Origen.Id)
        {
            return camino; //No hay camino
        }

        while (actual != null)
        {
            camino.Insert(0, actual);
            actual = Predecesores[actual.Id];
        }

        return camino;
    }
}

