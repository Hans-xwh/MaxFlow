# MaxFlow

Aplicación de escritorio en C# (Windows Forms) que resuelve el **problema del flujo máximo** con el algoritmo de **Ford-Fulkerson** y permite seguir la ejecución **paso a paso**.

Proyecto del curso **Matemática Computacional**, tema 3: Problema del flujo máximo.

## Qué hace

- Trabaja con **grafos dirigidos sin ciclos** y capacidades enteras en las aristas.
- Permite **cargar** un grafo desde un archivo CSV, **crearlo/editarlo** a mano o **generarlo de forma aleatoria** (entre 7 y 16 nodos).
- Ejecuta Ford-Fulkerson **una iteración por clic** en «Siguiente paso»:
  - resalta en **rojo** el camino aumentante,
  - muestra el número de paso, el camino, el incremento Δ y el flujo acumulado,
  - muestra, con la casilla «Mostrar detalles de cada paso», la capacidad, el flujo anterior, el flujo actual y el tipo de movimiento (directo o inverso) de cada arista del camino.
- Al terminar, calcula y resalta en **azul** el **corte mínimo**, y verifica que su capacidad coincide con el flujo máximo.
- Incluye un módulo adicional con el algoritmo de **Dijkstra** («Bonus: Dijkstra»), que no forma parte del tema 3 pero fue divertido de programar.

## Requisitos

- Windows.
- Visual Studio (el proyecto usa la solución `MaxFlow.slnx`).
- .NET Framework 4.7.2 y C# 10.

## Cómo usarlo

1. En el menú principal elige **Iniciar Ford-Fulkerson**.
2. En el selector de grafos:
   - haz doble clic en un archivo de la lista (CSV de la carpeta del programa) o usa **Examinar**,
   - **Crea Nuevo Grafo** para empezar uno vacío,
   - o elige la cantidad de nodos (7 a 16) y pulsa **Generar Aleatorio**.
3. En la pantalla de ejecución pulsa **Siguiente paso** hasta que el algoritmo termine. Al final se muestran el flujo máximo, los conjuntos S y T del corte, las aristas del corte y su capacidad.

También puedes abrir el **Editor de grafos** desde el menú principal. Allí puedes agregar nodos, crear y modificar aristas (capacidad y flujo inicial), reorganizar el dibujo, elegir **Fuente** y **Sumidero**, usar **Reparar Ciclos** y **Guardar** o **Revertir** los cambios.

## Formato del archivo CSV

El archivo tiene dos secciones. Las líneas vacías y las que empiezan con `#` se ignoran.

```csv
[NODOS]
# Id,Nombre,X,Y
1,S,80,200
2,A,220,100
...
[ARISTAS]
# Id,OrigenId,DestinoId,Capacidad,Flujo
1,1,2,10,0
2,1,3,8,0
...
```

- Si no existe `grafo.csv` junto al ejecutable, el programa genera y guarda un **grafo de ejemplo** (8 nodos: S, A, B, C, D, E, F, T; 10 aristas).
- Los grafos aleatorios se guardan en `Grafo Random.csv` junto al ejecutable.

## Ejemplo

Con el grafo de ejemplo (fuente S, sumidero T), el algoritmo encuentra cuatro caminos aumentantes:

| Iteración | Camino aumentante     | Δ | Flujo acumulado |
|-----------|-----------------------|---|-----------------|
| 1         | S → A → C → E → T     | 5 | 5               |
| 2         | S → A → D → F → T     | 5 | 10              |
| 3         | S → B → D → F → T     | 4 | 14              |
| 4         | S → B → D → C → E → T | 3 | 17              |

**Flujo máximo: 17.** El corte mínimo lo forman las aristas C → E (8) y D → F (9), con capacidad total 17.

## Estructura del proyecto

```
MaxFlow.slnx
WindowsFormsApp1/
├── Program.cs
├── Classes/
│   ├── Grafo.cs           # Nodo, Arista y Grafo: lectura/escritura CSV, generación aleatoria, detección de ciclos y dibujo
│   ├── FordFulkerson.cs   # Algoritmo paso a paso, red residual, historial y corte mínimo
│   └── Dijkstra.cs        # Módulo adicional
└── Forms/
    ├── MenuPrincipal.cs       # Menú principal
    ├── SelectorForm.cs        # Selector de grafos (archivo, nuevo o aleatorio)
    ├── GraphEditorForm.cs     # Editor de grafos
    ├── FordFulkersonForm.cs   # Ejecución paso a paso
    ├── TestForm.cs            # Pantalla de prueba
    ├── DijkstraForm.cs        # Módulo adicional
    └── Creditos.cs
```
