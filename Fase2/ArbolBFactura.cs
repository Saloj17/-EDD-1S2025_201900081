using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace Estructuras
{
    public class ArbolBFactura
    {
        private NodoFactura raiz;
        private int orden;


        public ArbolBFactura()
        {
            raiz = new NodoFactura();
            orden = 5; // Orden del árbol B
        }


        public void Insertar(int id, int idServicio, double total)
        {
            Llave nuevaRaiz = InsertarEnHoja(id, idServicio, total, raiz);
            if (nuevaRaiz != null)
            {
                raiz = new NodoFactura();
                raiz.InsertarLlave(nuevaRaiz);
                raiz.SetHoja(false);
            }
        }

        private Llave InsertarEnHoja(int id, int idServicio, double total, NodoFactura raiz)
        {
            if (raiz.EsHoja())
            {
                raiz.InsertarLlave(new Llave(id, idServicio, total));
                return (raiz.NumeroLlaves == orden) ? Dividir(raiz) : null;
            }
            else
            {
                Llave puntero = raiz.Primero;
                if (id == puntero.Id) return null;
                else if (id < puntero.Id)
                {
                    Llave llaveRaiz = InsertarEnHoja(id, idServicio, total, puntero.Izquierda);
                    if (llaveRaiz != null)
                    {
                        raiz.InsertarLlave(llaveRaiz);
                        return (raiz.NumeroLlaves == this.orden) ? Dividir(raiz) : null;
                    }
                    return null;
                }
                else
                {
                    do
                    {
                        if (id == puntero.Id) return null;
                        else if (id < puntero.Id)
                        {
                            Llave llaveRaiz = InsertarEnHoja(id, idServicio, total, puntero.Izquierda);
                            if (llaveRaiz != null)
                            {
                                raiz.InsertarLlave(llaveRaiz);
                                return (raiz.NumeroLlaves == orden) ? Dividir(raiz) : null;
                            }
                            return null;
                        }
                        else if (puntero.Sig == null)
                        {
                            Llave llaveRaiz = InsertarEnHoja(id, idServicio, total, puntero.Derecha);
                            if (llaveRaiz != null)
                            {
                                raiz.InsertarLlave(llaveRaiz);
                                return (raiz.NumeroLlaves == this.orden) ? Dividir(raiz) : null;
                            }
                            return null;
                        }

                        puntero = puntero.Sig;
                    } while (puntero != null);
                }
            }
            return null;
        }

        private Llave Dividir(NodoFactura nodo)
        {
            int contador = 1;
            int valorClaveRaiz = 0;
            int tempIdServicio = 0;
            double tempTotal = 0;
            Llave temp = nodo.Primero;
            NodoFactura izquierda = new NodoFactura();
            NodoFactura derecha = new NodoFactura();

            while (temp != null)
            {
                if (contador < 3)
                {
                    // Insertar en el nodo izquierdo
                    Llave nuevaLlave = new Llave(temp.Id, temp.Id_Servicio, temp.Total);
                    nuevaLlave.Derecha = temp.Derecha;
                    nuevaLlave.Izquierda = temp.Izquierda;
                    izquierda.InsertarLlave(nuevaLlave);

                    if (nuevaLlave.TieneHijos())
                    {
                        izquierda.SetHoja(false);
                    }
                }
                else if (contador == 3)
                {
                    // Guardar la clave mediana
                    valorClaveRaiz = temp.Id;
                    tempIdServicio = temp.Id_Servicio;
                    tempTotal = temp.Total;
                }
                else
                {
                    // Insertar en el nodo derecho
                    Llave nuevaLlave = new Llave(temp.Id, temp.Id_Servicio, temp.Total);
                    nuevaLlave.Derecha = temp.Derecha;
                    nuevaLlave.Izquierda = temp.Izquierda;
                    derecha.InsertarLlave(nuevaLlave);

                    if (nuevaLlave.TieneHijos())
                    {
                        derecha.SetHoja(false);
                    }
                }
                contador++;
                temp = temp.Sig;
            }

            // Crear la nueva llave raíz
            Llave llaveRaiz = new Llave(valorClaveRaiz, tempIdServicio, tempTotal); // Id_Servicio y Total se inicializan a 0
            llaveRaiz.Derecha = derecha;
            llaveRaiz.Izquierda = izquierda;

            // Limpiar el nodo original para evitar duplicados
            nodo.Primero = null;
            nodo.NumeroLlaves = 0;

            return llaveRaiz;
        }

        public void MostrarPorNiveles()
        {
            if (raiz == null) return;

            Queue<NodoFactura> cola = new Queue<NodoFactura>();
            cola.Enqueue(raiz);

            int nivel = 0; // Contador para el nivel actual

            // Se recorre el árbol B por niveles
            while (cola.Count > 0)
            {
                int nivelTamaño = cola.Count;
                Console.Write("Nivel: ");

                // Procesar todos los nodos en el nivel actual
                while (nivelTamaño-- > 0)
                {
                    NodoFactura nodo = cola.Dequeue();

                    // Imprimir las claves de este nodo
                    Console.Write("[");
                    Llave temp = nodo.Primero;
                    while (temp != null)
                    {
                        Console.Write($"Id: {temp.Id}");
                        if (temp.Sig != null) Console.Write(", ");
                        temp = temp.Sig;
                    }
                    Console.Write("] ");

                    // Si el nodo no es una hoja, añadir los nodos hijos a la cola
                    if (!nodo.EsHoja())
                    {
                        Llave puntero = nodo.Primero;
                        while (puntero != null)
                        {
                            if (puntero.Izquierda != null) cola.Enqueue(puntero.Izquierda);
                            if (puntero.Derecha != null) cola.Enqueue(puntero.Derecha);
                            puntero = puntero.Sig;
                        }
                    }
                }

                Console.WriteLine(); // Separar los niveles con una nueva línea
                nivel++;
            }
        }


        public void GenerarGraphviz()
        {
            string codigoDot = GenerarCodigoDot();

            string filePath = "Reportes/facturas.dot";

            // Escribir el contenido en el archivo
            File.WriteAllText(filePath, codigoDot);

            Console.WriteLine($"Archivo .dot generado: {filePath}");

            //generar la imagen
            ProcessStartInfo startInfo = new ProcessStartInfo("dot")
            {
                Arguments = $"-Tpng Reportes/facturas.dot -o Reportes/facturas.png",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            process.WaitForExit();

            //Mostrar la imagen generada
            Process.Start(new ProcessStartInfo("C:/Users/SALOJ/Desktop/[EDD-25]/-EDD-1S2025_201900081/Fase2/Reportes/facturas.png") { UseShellExecute = true });

        }
        private string GenerarCodigoDot()
        {
            StringBuilder dotBuilder = new StringBuilder();
            dotBuilder.AppendLine("digraph ArbolB {");
            dotBuilder.AppendLine("  node [shape=record, height=0.1];");
            dotBuilder.AppendLine("  label = \"Arbol B Facturas\n\n\";");
            dotBuilder.AppendLine("  labelloc = \"t\";");
            dotBuilder.AppendLine("  fontsize = 24;");
            dotBuilder.AppendLine("  fontname = \"Helvetica-Bold\";");
            GenerarNodoDot(dotBuilder, raiz);
            dotBuilder.AppendLine("}");

            return dotBuilder.ToString();
        }

        private void GenerarNodoDot(StringBuilder dotBuilder, NodoFactura nodo)
        {
            if (nodo == null) return;

            // Generar el nombre del nodo
            string nombreNodo = "nodo_" + nodo.GetHashCode();
            dotBuilder.Append($"  {nombreNodo} [label=\"");

            // Generar las claves del nodo
            Llave temp = nodo.Primero;
            while (temp != null)
            {
                // Mostrar Id, Id_Servicio y Total para todas las claves
                dotBuilder.Append($" |Id: {temp.Id}\\n Id Servicio: {temp.Id_Servicio}\\n Total: {temp.Total}|");
                temp = temp.Sig;
            }
            dotBuilder.AppendLine("\" style=\"filled\" fillcolor=\"#96cbb0\" fontname=\"Helvetica-Bold\" fontsize = 18];");

            // Generar las conexiones con los hijos
            temp = nodo.Primero;
            while (temp != null)
            {
                if (temp.Izquierda != null)
                {
                    GenerarNodoDot(dotBuilder, temp.Izquierda);
                    dotBuilder.AppendLine($"  {nombreNodo} -> nodo_{temp.Izquierda.GetHashCode()};");
                }
                if (temp.Sig == null && temp.Derecha != null)
                {
                    GenerarNodoDot(dotBuilder, temp.Derecha);
                    dotBuilder.AppendLine($"  {nombreNodo} -> nodo_{temp.Derecha.GetHashCode()};");
                }

                temp = temp.Sig;
            }
        }
        // Metodo booleano para buscar una factura por ID
        public bool BuscarFacturaBool(int id)
        {
            return BuscarEnNodoBool(id, raiz);
        }

        private bool BuscarEnNodoBool(int id, NodoFactura nodo)
        {
            if (nodo == null) return false;

            // Buscar en las claves del nodo
            Llave temp = nodo.Primero;
            while (temp != null)
            {
                if (temp.Id == id) return true; // Encontrada
                temp = temp.Sig;
            }

            // Si no se encuentra, buscar en los hijos
            temp = nodo.Primero;
            while (temp != null)
            {
                bool resultado = BuscarEnNodoBool(id, temp.Izquierda);
                if (resultado) return true; // Encontrada en el hijo izquierdo

                temp = temp.Sig;
            }

            return false; // No encontrada
        }



    }
}