using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Estructuras
{
    public unsafe class ListaServicios
    {
        private NodoServicio* cabeza;


        //* Método constructor
        public ListaServicios()
        {
            cabeza = null;
        }


        //* Método para insertar un nuevo nodo en la lista
        public void Insertar(int id, int id_repuesto, int id_vehiculo, string detalles, double costo)
        {
            NodoServicio* nuevoNodo = (NodoServicio*)Marshal.AllocHGlobal(sizeof(NodoServicio));
            *nuevoNodo = new NodoServicio(id, id_repuesto, id_vehiculo, detalles, costo);

            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                NodoServicio* actual = cabeza;
                while (actual->Siguiente != null)
                {
                    actual = actual->Siguiente;
                }
                actual->Siguiente = nuevoNodo;
            }
        }



        public void Mostrar()
        {
            NodoServicio* actual = cabeza;
            if (actual == null)
            {
                Console.WriteLine("La lista está vacía");
            }
            while (actual != null)
            {
                string detalles = new string(actual->Detalles);
                Console.WriteLine($"ID: {actual->Id}, ID Repuesto: {actual->Id_Repuesto}, ID Vehiculo: {actual->Id_Vehiculo}, Detalles: {detalles}, Costo: {actual->Costo}");
                actual = actual->Siguiente;
            }
        }

        //* Método para liberar memoria manualmente
        public void LiberarMemoria()
        {
            NodoServicio* actual = cabeza;
            while (actual != null)
            {
                NodoServicio* temp = actual;
                actual = actual->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
            cabeza = null;
        }

        //metodo para vaciar la lista
        public void Vaciar()
        {
            NodoServicio* actual = cabeza;
            while (actual != null)
            {
                NodoServicio* temp = actual;
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
                NodoServicio* actual = cabeza;
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
        public NodoServicio* BuscarNodo(int id)
        {
            if (cabeza == null)
            {   
                
                return null;
            }
            else
            {
                NodoServicio* actual = cabeza;
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
                NodoServicio* temp = cabeza;
                cabeza = cabeza->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
                return;
            }
            NodoServicio* actual = cabeza;
            NodoServicio* prev = null;
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
            graphviz += "    rankdir=LR;\n";
            graphviz += "    subgraph cluster_0 {\n";
            graphviz += "        label = \"Cola servicios\";\n";

            // Iterar sobre los nodos de la lista y construir la representación Graphviz
            NodoServicio* actual = cabeza;
            int index = 0;

            while (actual != null)
            {
                string detalles = new string(actual->Detalles);
                graphviz += $"        n{index} [label = \"ID: {actual->Id} \\n ID Repuesto: {actual->Id_Repuesto} \\n ID Vehiculo: {actual->Id_Vehiculo} \\n Detalles: {detalles} \\n Costo: {actual->Costo} \"];\n";
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

            string filePath = "servicios.dot";

            // Escribir el contenido en el archivo
            File.WriteAllText(filePath, graphviz);

            Console.WriteLine($"Archivo .dot generado: {filePath}");

            //generar la imagen
            ProcessStartInfo startInfo = new ProcessStartInfo("dot")
            {
                Arguments = $"-Tpng servicios.dot -o servicios.png",
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
            Process.Start(new ProcessStartInfo("servicios.png") { UseShellExecute = true });

        }
    }
}