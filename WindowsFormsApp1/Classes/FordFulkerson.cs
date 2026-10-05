#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using MaxFlow.Model;

namespace MaxFlow.Fulkerson;

// Representa un movimiento dentro de la red residual
internal class MovimientoResidual{
    public Arista Arista { get; set; }
    public bool HaciaAdelante { get; set; }
    public int CapacidadResidual { get; set; }

    public MovimientoResidual(
        Arista arista,
        bool haciaAdelante,
        int capacidadResidual)
    {
        Arista = arista;
        HaciaAdelante = haciaAdelante;
        CapacidadResidual = capacidadResidual;
    }
}

internal class AristaResidual{
    public Nodo Origen { get; set; }
    public Nodo Destino { get; set; }
    public int Capacidad { get; set; }
    public bool EsInversa { get; set; }

    public AristaResidual(
        Nodo origen,
        Nodo destino,
        int capacidad,
        bool esInversa)
    {
        Origen = origen;
        Destino = destino;
        Capacidad = capacidad;
        EsInversa = esInversa;
    }
}
// Guarda toda la información de una iteración
internal class PasoFordFulkerson
{
    public int NumeroPaso { get; set; }

    public List<Nodo> Camino { get; set; } =
        new List<Nodo>();

    public List<MovimientoResidual> Movimientos { get; set; } =
        new List<MovimientoResidual>();

    public int Incremento { get; set; }

    public int FlujoAcumulado { get; set; }
}
internal class FordFulkerson{
    private Grafo grafo;
    private Nodo fuente;
    private Nodo sumidero;

    // Resultado actual
    public int FlujoMaximo { get; private set; } = 0;

    public List<Nodo> UltimoCamino { get; private set; } =
        new List<Nodo>();

    public int UltimoIncremento { get; private set; } = 0;

    public bool Terminado { get; private set; } = false;

    // Información completa del último paso
    public PasoFordFulkerson? UltimoPaso { get; private set; }

    // Historial de todos los pasos
    public List<PasoFordFulkerson> Historial { get; private set; } = new List<PasoFordFulkerson>();
    public List<AristaResidual> RedResidual { get; private set; } = new List<AristaResidual>();
    public FordFulkerson(
        Grafo grafo,
        Nodo fuente,
        Nodo sumidero)
    {
        this.grafo = grafo;
        this.fuente = fuente;
        this.sumidero = sumidero;

        // Actualizar capacidades residuales iniciales
        foreach (Arista arista in grafo.Aristas)
        {
            arista.CapacidadResidual =
                arista.Capacidad - arista.Flujo;
        }
    }


    public bool SiguientePaso()
    {
        // Buscar un camino aumentante en la red residual
        List<MovimientoResidual> movimientos =
            BuscarCaminoAumentante();

        // Si no existe camino, el algoritmo terminó
        if (movimientos.Count == 0)
        {
            Terminado = true;

            UltimoCamino.Clear();
            UltimoIncremento = 0;
            UltimoPaso = null;

            return false;
        }


        // Construir la lista de nodos del camino
        List<Nodo> camino =
            ConstruirCamino(movimientos);


        // Calcular el incremento Δ
        int incremento =
            CalcularIncremento(movimientos);


        // Actualizar los flujos
        ActualizarFlujo(movimientos, incremento);

        ConstruirRedResidual();
        // Actualizar información
        UltimoCamino = camino;

        UltimoIncremento = incremento;

        FlujoMaximo += incremento;


        // Crear registro del paso
        PasoFordFulkerson paso =
            new PasoFordFulkerson();

        paso.NumeroPaso = Historial.Count + 1;

        paso.Camino = new List<Nodo>(camino);

        paso.Movimientos = new List<MovimientoResidual>(movimientos);

        paso.Incremento = incremento;

        paso.FlujoAcumulado = FlujoMaximo;

        // Guardar el paso
        UltimoPaso = paso;
        Historial.Add(paso);

        return true;
    }
    private List<MovimientoResidual> BuscarCaminoAumentante()
    {
        Queue<Nodo> cola = new Queue<Nodo>();

        HashSet<Nodo> visitados = new HashSet<Nodo>();

        Dictionary<Nodo, MovimientoResidual> anterior = new Dictionary<Nodo, MovimientoResidual>();


        // Comenzamos desde la fuente
        cola.Enqueue(fuente);
        visitados.Add(fuente);


        while (cola.Count > 0)
        {
            Nodo actual = cola.Dequeue();

            // ==========================================
            // 1. MOVIMIENTOS HACIA ADELANTE
            // ==========================================

            foreach (Arista arista in grafo.Aristas)
            {
                if (arista.Origen == actual &&
                    arista.Flujo < arista.Capacidad)
                {
                    Nodo siguiente = arista.Destino;

                    if (!visitados.Contains(siguiente))
                    {
                        int residual = arista.Capacidad - arista.Flujo;

                        MovimientoResidual movimiento =
                            new MovimientoResidual(arista, true, residual);
                        anterior[siguiente] = movimiento;

                        visitados.Add(siguiente);
                        cola.Enqueue(siguiente);
                    }
                }
            }


            // ==========================================
            // 2. MOVIMIENTOS HACIA ATRÁS
            // ==========================================

            foreach (Arista arista in grafo.Aristas)
            {
                if (arista.Destino == actual &&
                    arista.Flujo > 0)
                {
                    Nodo siguiente =
                        arista.Origen;

                    if (!visitados.Contains(siguiente))
                    {
                        int residual =
                            arista.Flujo;

                        MovimientoResidual movimiento =
                            new MovimientoResidual(
                                arista,
                                false,
                                residual);

                        anterior[siguiente] =
                            movimiento;

                        visitados.Add(siguiente);
                        cola.Enqueue(siguiente);
                    }
                }
            }


            // Si llegamos al sumidero,
            // ya tenemos un camino aumentante
            if (visitados.Contains(sumidero))
                break;
        }


        // No existe camino aumentante
        if (!visitados.Contains(sumidero))
        {
            return new List<MovimientoResidual>();
        }


        // ==========================================
        // RECONSTRUIR CAMINO
        // ==========================================

        List<MovimientoResidual> camino =
            new List<MovimientoResidual>();

        Nodo actualNodo =
            sumidero;


        while (actualNodo != fuente)
        {
            MovimientoResidual movimiento =
                anterior[actualNodo];

            camino.Add(movimiento);


            if (movimiento.HaciaAdelante)
            {
                actualNodo =
                    movimiento.Arista.Origen;
            }
            else
            {
                actualNodo =
                    movimiento.Arista.Destino;
            }
        }
        // Actualmente está:
        // sumidero → fuente
        // Lo invertimos:
        camino.Reverse();

        return camino;
    }


    private List<Nodo> ConstruirCamino(
        List<MovimientoResidual> movimientos)
    {
        List<Nodo> camino =
            new List<Nodo>();


        camino.Add(fuente);


        Nodo actual =
            fuente;


        foreach (MovimientoResidual movimiento
                 in movimientos)
        {
            if (movimiento.HaciaAdelante)
            {
                actual =
                    movimiento.Arista.Destino;
            }
            else
            {
                actual =
                    movimiento.Arista.Origen;
            }

            camino.Add(actual);
        }


        return camino;
    }


    private int CalcularIncremento(
        List<MovimientoResidual> movimientos)
    {
        int incremento =
            int.MaxValue;


        foreach (MovimientoResidual movimiento
                 in movimientos)
        {
            incremento =
                Math.Min(
                    incremento,
                    movimiento.CapacidadResidual);
        }

        return incremento;
    }
    private void ActualizarFlujo(
        List<MovimientoResidual> movimientos,
        int incremento)
    {
        foreach (MovimientoResidual movimiento
                 in movimientos)
        {
            Arista arista =
                movimiento.Arista;


            // ==========================================
            // MOVIMIENTO HACIA ADELANTE
            // ==========================================

            if (movimiento.HaciaAdelante)
            {
                arista.Flujo += incremento;
            }

            // ==========================================
            // MOVIMIENTO HACIA ATRÁS
            // ==========================================

            else
            {
                arista.Flujo -= incremento;
            }
            // Actualizar capacidad residual
            arista.CapacidadResidual =
                arista.Capacidad -
                arista.Flujo;
        }
    }

    private void ConstruirRedResidual()
    {
        RedResidual.Clear();

        foreach (Arista arista in grafo.Aristas)
        {
            // Capacidad residual hacia adelante
            int capacidadAdelante =
                arista.Capacidad - arista.Flujo;

            if (capacidadAdelante > 0)
            {
                RedResidual.Add(
                    new AristaResidual(
                        arista.Origen,
                        arista.Destino,
                        capacidadAdelante,
                        false
                    )
                );
            }

            // Capacidad residual hacia atrás
            int capacidadAtras =
                arista.Flujo;

            if (capacidadAtras > 0)
            {
                RedResidual.Add(
                    new AristaResidual(
                        arista.Destino,
                        arista.Origen,
                        capacidadAtras,
                        true
                    )
                );
            }
        }
    }
}