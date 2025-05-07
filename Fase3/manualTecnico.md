# Universidad de San Carlos de Guatemala
# Facultad de Ingenieria
# Escuela de Ciencias y Sistemas
# Estructura de Datos
# Sección C
# José Luis Saloj
# 201900081
---
---
# Manual Técnico - AutoGest Pro
---
---
## 1. Introducción
Sistema de gestión para talleres vehiculares desarrollado en **C#** con **GTK** para interfaces gráficas. Combina estructuras de datos avanzadas para optimizar el almacenamiento y consulta de información.

## Descripción General
Este sistema está desarrollado en C# con interfaz gráfica GTK#, que gestiona usuarios, vehículos, repuestos y servicios mediante estructuras de datos avanzadas como Blockchain, Árboles AVL, Árboles Binarios y Grafos. Proporciona funcionalidades de CRUD, generación de reportes y backups.

---

## Estructuras Principales

### 1. `ArbolBinarioServicio` (`ArbolServicio.cs`)
**Propósito**: Gestionar servicios en un árbol binario de búsqueda.
- **Métodos Clave**:
  - `Insertar(NodoServicio)`: Inserta un nodo ordenado por ID.
  - `Recorridos (EnOrden, PreOrden, PostOrden)`: Muestra nodos en consola o devuelve listas.
  - `GenerarGraphviz()`: Genera gráficos DOT del árbol.
  - `Buscar(int id)`: Búsqueda por ID con complejidad O(log n).

### 2. `AVLRepuesto` (`AVLRepuestos.cs`)
**Propósito**: Almacenar repuestos en un árbol AVL auto-balanceado.
- **Métodos Clave**:
  - `Insert(NodoRepuesto)`: Inserta y balancea el árbol.
  - `Rotaciones (Izquierda/Derecha)`: Mantienen el balance.
  - `GenerarGraphviz()`: Similar al árbol binario pero con balance AVL.

### 3. `Blockchain` (`Blockchain.cs`)
**Propósito**: Gestionar usuarios con tecnología blockchain.
- **Características**:
  - `AgregarUsuario(Usuario)`: Crea bloques con Proof-of-Work (hash que empieza con "0000").
  - `EncriptacionSHA256()`: Hash para contraseñas.
  - `GenerarDot()`: Visualiza la cadena de bloques.

### 4. `CargaMasivaWindow` (`cargaMasiva.cs`)
**Interfaz GTK#** para cargar datos desde JSON:
- **Funciones**:
  - `CargarUsuarios()`, `CargarVehiculos()`, etc.: Deserializan JSON y validan datos.
  - Integración con estructuras como `Datos.blockchain` y `Datos.repuestosArbol`.

### 5. `MerkleTree` (`MerkleTree.cs`)
**Propósito**: Generar facturas y almacenar datos de servicios.
- **Métodos Clave**:
  - `InsertarFactura(Factura)`: Inserta facturas en el árbol.
  - `GenerarHash()`: Crea un hash único para cada factura.
  - `GenerarGraphviz()`: Visualiza el árbol de Merkle.
- **Estructura**: Cada factura es un nodo con ID, fecha y hash.

### 6. `ListaVehiculos` (`ListaVehiculos.cs`)

**Propósito**: Almacenar vehículos en una lista enlazada.
- **Métodos Clave**:
  - `Insertar(Vehiculo)`: Inserta vehículos en la lista.
  - `Buscar(int id)`: Busca vehículos por ID.
  - `Eliminar(int id)`: Elimina vehículos de la lista.

- **Estructura**: Cada nodo contiene un vehículo y un puntero al siguiente nodo.
- **Complejidad**: O(n) para búsqueda y eliminación.

### 7. `GrafoNoDirigido` (`GrafoNoDirigido.cs`)
**Propósito**: Representar relaciones entre vehículos y repuestos.
- **Métodos Clave**:
  - `AgregarVertice(Vehiculo)`: Agrega un vehículo al grafo.
  - `AgregarArista(Vehiculo, Repuesto)`: Conecta vehículos con repuestos.
  - `GenerarGraphviz()`: Genera un gráfico de la relación entre vehículos y repuestos.
- **Estructura**: Lista de adyacencia para representar conexiones.
- **Complejidad**: O(V + E) para agregar vértices y aristas, donde V es el número de vehículos y E es el número de repuestos.
- **Visualización**: Utiliza Graphviz para representar gráficamente las relaciones.

### 8. Interfaz Gráfica
**GTK#**: Interfaz gráfica multiplataforma para facilitar la interacción del usuario.
- **Ventanas**: Carga masiva, gestión de usuarios, visualización de repuestos y servicios.
- **Eventos**: Botones y entradas de texto para interactuar con el sistema.
- **Ejemplo de carga masiva**:  
  ```csharp
  CargaMasivaWindow carga = new CargaMasivaWindow();
  carga.CargarUsuarios("usuarios.json");
  carga.CargarRepuestos("repuestos.json");
  carga.CargarServicios("servicios.json");
    carga.CargarVehiculos("vehiculos.json");

  ```
---

## Funcionalidades Clave

### Gestión de Usuarios (`insertarUsuarios.cs`)
- **Validación**: Campos obligatorios y unicidad de ID.
- **Encriptación**: Contraseñas con SHA-256 antes de almacenar en blockchain.

### Servicios (`generarServicio.cs`)
- **Validación**: Verifica existencia de repuestos y vehículos.
- **Facturación**: Genera IDs únicos y registra en `MerkleTree`.

### Backup y Carga de Datos (`backup.cs`)
- **Backup**: Comprime datos de usuarios, vehículos, repuestos y servicios en un archivo ZIP.

- **Carga**: Descomprime y carga datos desde un archivo ZIP.

### Visualización de Datos (`visualizar.cs`)
- **Árboles y Grafos**: Genera imágenes de estructuras de datos (AVL, Merkle, Grafo) para análisis visual.

### Reportes
- **Graphviz**: Todos los módulos generan imágenes PNG desde código DOT.
  - Ejemplo:  
    ```csharp
    Process.Start("dot", "-Tpng archivo.dot -o imagen.png");
    ```

---

## Librerías Utilizadas
| Librería | Uso |
|----------|-----|
| `GtkSharp` | Interfaz gráfica multiplataforma. |
| `System.Text.Json` | Serialización/deserialización JSON. |
| `System.Security.Cryptography` | Hash SHA-256 para blockchain y contraseñas. |
| `System.Diagnostics` | Ejecución de Graphviz desde código. |

---

## Flujo de Datos
```mermaid
graph TD
    A[JSON Carga Masiva] --> B[Árbol AVL (Repuestos)]
    A --> C[Blockchain (Usuarios)]
    D[Interfaz GTK#] --> E[Generar Servicio]
    E --> F[Árbol Binario (Servicios)]
    F --> G[Factura en MerkleTree]