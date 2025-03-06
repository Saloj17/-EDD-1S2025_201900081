using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Estructuras
{
    public unsafe class ListaFacturas
    {
        private NodoFactura* cabeza;


        //* Método constructor
        public ListaFacturas()
        {
            cabeza = null;
        }


        //* Método para insertar un nuevo nodo en la lista
        public void Insertar(int id, int id_orden, double total)
        {
            NodoFactura* nuevoNodo = (NodoFactura*)Marshal.AllocHGlobal(sizeof(NodoFactura));
            *nuevoNodo = new NodoFactura(id, id_orden, total);

            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                NodoFactura* actual = cabeza;
                while (actual->Siguiente != null)
                {
                    actual = actual->Siguiente;
                }
                actual->Siguiente = nuevoNodo;
            }
        }

        //metodo para insertar al inicio
        public void InsertarInicio(int id, int id_orden, double total)
        {
            NodoFactura* nuevoNodo = (NodoFactura*)Marshal.AllocHGlobal(sizeof(NodoFactura));
            *nuevoNodo = new NodoFactura(id, id_orden, total);

            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                nuevoNodo->Siguiente = cabeza;
                cabeza = nuevoNodo;
            }
        }



        public void Mostrar()
        {
            NodoFactura* actual = cabeza;
            if (actual == null)
            {
                Console.WriteLine("La lista está vacía");
            }
            while (actual != null)
            {
                Console.WriteLine($"ID: {actual->Id}, ID Orden: {actual->Id_Orden}, Total: {actual->Total}");
                actual = actual->Siguiente;
            }
        }

        //* Método para liberar memoria manualmente
        public void LiberarMemoria()
        {
            NodoFactura* actual = cabeza;
            while (actual != null)
            {
                NodoFactura* temp = actual;
                actual = actual->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
            cabeza = null;
        }

        //metodo para vaciar la lista
        public void Vaciar()
        {
            NodoFactura* actual = cabeza;
            while (actual != null)
            {
                NodoFactura* temp = actual;
                actual = actual->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
            cabeza = null;
        }
        public bool Buscar(int id)
        //validar si no hay nada false

        {
            if (cabeza == null)
            {
                return false;
            }
            else
            {
                NodoFactura* actual = cabeza;
                while (actual != null)
                {
                    if (actual->Id == id)
                    {
                        return true;
                    }
                    actual = actual->Siguiente;
                }
                return false;
            }
        }

        //metodo para buscar por id y devolver el nodo
        public NodoFactura* BuscarNodo(int id)
        {
            if (cabeza == null)
            {   
                
                return null;
            }
            else
            {
                NodoFactura* actual = cabeza;
                while (actual != null)
                {
                    if (actual->Id == id)
                    {
                        return actual;
                    }
                    actual = actual->Siguiente;
                }
                return null;
            }
        }

        //metodo para eliminar un nodo
        public void Eliminar(int id)
        {
            if (cabeza == null)
            {
                return;
            }
            if (cabeza->Id == id)
            {
                NodoFactura* temp = cabeza;
                cabeza = cabeza->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
                return;
            }
            NodoFactura* actual = cabeza;
            NodoFactura* prev = null;
            while (actual != null)
            {
                if (actual->Id == id)
                {
                    prev->Siguiente = actual->Siguiente;
                    Marshal.FreeHGlobal((IntPtr)actual);
                    return;
                }
                prev = actual;
                actual = actual->Siguiente;
            }
        }

     

        
        //* Método para generar el código Graphviz
        public void GenerarGraphviz()
        {
            // Si la lista está vacía, generamos un solo nodo con "NULL"
            if (cabeza == null)
            {
                Console.WriteLine("La lista está vacía");
            }

            // Iniciamos el código Graphviz
            var graphviz = "digraph G {\n";
            graphviz += "    node [shape=ellipse];\n";
            graphviz += "    rankdir=L;\n";
            graphviz += "    subgraph cluster_0 {\n";
            graphviz += "        label = \"Cola servicios\";\n";

            // Iterar sobre los nodos de la lista y construir la representación Graphviz
            NodoFactura* actual = cabeza;
            int index = 0;

            while (actual != null)
            {
                graphviz += $"        n{index} [label = \"ID: {actual->Id} \\n ID Orden: {actual->Id_Orden} \\n Total: {actual->Total}  \"];\n";
                actual = actual->Siguiente;
                index++;
            }

            // Conectar los nodos
            actual = cabeza;
            for (int i = 0; actual != null && actual->Siguiente != null; i++)
            {
                graphviz += $"        n{i} -> n{i + 1};\n";
                actual = actual->Siguiente;
            }

            graphviz += "    }\n";
            graphviz += "}\n";

            string filePath = "facturas.dot";

            // Escribir el contenido en el archivo
            File.WriteAllText(filePath, graphviz);

            Console.WriteLine($"Archivo .dot generado: {filePath}");

            //generar la imagen
            ProcessStartInfo startInfo = new ProcessStartInfo("dot")
            {
                Arguments = $"-Tpng facturas.dot -o facturas.png",
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
            Process.Start(new ProcessStartInfo("facturas.png") { UseShellExecute = true });

        }
    }
}