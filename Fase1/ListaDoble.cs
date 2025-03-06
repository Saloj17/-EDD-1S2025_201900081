using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace Estructuras
{
    public unsafe class ListaDoble
    {
        private NodoDoble* cabeza;
        private NodoDoble* ultimo;


        //* Método constructor
        public ListaDoble()
        {
            cabeza = null;
            ultimo = null;
        }


        //* Método para insertar un nuevo nodo en la lista
        public void InsertarFinal(int id, int id_usuario, string marca, int modelo, string placa)
        {
            NodoDoble* nuevoNodo = (NodoDoble*)Marshal.AllocHGlobal(sizeof(NodoDoble));
            *nuevoNodo = new NodoDoble(id, id_usuario, marca, modelo, placa);

            if (cabeza == null&&ultimo==null)
            {
                cabeza = nuevoNodo;
                ultimo = nuevoNodo;
            }
            else
            {
                NodoDoble* actual = ultimo;
                while (actual->Siguiente != null)
                {
                    actual = actual->Siguiente;
                }
                actual->Siguiente = nuevoNodo;
                nuevoNodo->Anterior = actual;
                ultimo = nuevoNodo;
            }
        }

        public void InsertarInicio(int id, int id_usuario, string marca, int modelo, string placa)
        {
            NodoDoble* nuevoNodo = (NodoDoble*)Marshal.AllocHGlobal(sizeof(NodoDoble));
            *nuevoNodo = new NodoDoble(id, id_usuario, marca, modelo, placa);

            if (cabeza == null&&ultimo==null)
            {
                cabeza = nuevoNodo;
                ultimo = nuevoNodo;
            }
            else
            {
                NodoDoble* actual = cabeza;
                while (actual->Anterior != null)
                {
                    actual = actual->Anterior;
                }
                actual->Anterior = nuevoNodo;
                nuevoNodo->Siguiente = actual;
                cabeza = nuevoNodo;
            }
        }


        public void Mostrar()
        {
            NodoDoble* actual = cabeza;
            while (actual != null)
            {
                string marca = new string(actual->Marca);
                string placa = new string(actual->Placa);
                Console.WriteLine($"ID: {actual->Id}, ID Usuario: {actual->Id_Usuario}, Marca: {marca}, Modelo: {actual->Modelo}, Placa: {placa}");
                actual = actual->Siguiente;
            }
        }

        //Metodo para vaciar la lista
        public void Vaciar()
        {
            NodoDoble* actual = cabeza;
            while (actual != null)
            {
                NodoDoble* temp = actual;
                actual = actual->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
            cabeza = null;
            ultimo = null;
        }

        //Metodo para buscar un nodo en la lista
        public NodoDoble* BuscarNodo(int id)
        {
            NodoDoble* actual = cabeza;
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

        //Metodo para eliminar un nodo de la lista
        public void Eliminar(int id)
        {
            // si la lista está vacía, no hay nada que eliminar
            if (cabeza == null) return;
            // si el nodo a eliminar es la cabeza de la lista
            if (cabeza->Id == id && cabeza->Siguiente == null)
            {
                Marshal.FreeHGlobal((IntPtr)cabeza);
                cabeza = null;
                ultimo = null;
                return;
            }

            NodoDoble* temp = cabeza;
            NodoDoble* prev = null;
            do
            {
                // si el nodo a eliminar es la cabeza de la lista
                if (temp->Id == id)
                {
                    if (prev != null)
                    {
                        prev->Siguiente = temp->Siguiente;
                        temp->Siguiente->Anterior = prev;
                    }
                    else
                    {
                        cabeza = cabeza->Siguiente;
                        cabeza->Anterior = null;
                    }
                    Marshal.FreeHGlobal((IntPtr)temp);
                    return;
                }
                prev = temp;
                temp = temp->Siguiente;
            } while (temp != null);
        }

        //Metodo booleano para buscar por id
        public bool Buscar(int id)
        {
            if (cabeza == null)
            {
                return false;
            }
            else
            {
                NodoDoble* actual = cabeza;
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


        //Metodo para generar el código Graphviz de los primeros 5 nodos
        public void GenerarGraphvizPrimeros5()
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
            graphviz += "        label = \"Lista Doble\";\n";

            // Iterar sobre los nodos de la lista y construir la representación Graphviz
            NodoDoble* actual = cabeza;
            int index = 0;

            while (actual != null && index < 5)
            {
                string marca = new string(actual->Marca);
                string placa = new string(actual->Placa);
                graphviz += $"        n{index} [label = \"ID: {actual->Id} \\n ID Usuario: {actual->Id_Usuario} \\n Marca: {marca} \\n Modelo: {actual->Modelo} \\n Placa: {placa}\"];\n";
                Console.WriteLine($"ID: {actual->Id}, ID Usuario: {actual->Id_Usuario}, Marca: {marca}, Modelo: {actual->Modelo}, Placa: {placa}");
                actual = actual->Siguiente;
                index++;
            }

            // Conectar los nodos
            actual = cabeza;
            for (int i = 0; actual != null && actual->Siguiente != null && i < 4; i++)
            {
                graphviz += $"        n{i} -> n{i + 1};\n";
                graphviz += $"        n{i+1} -> n{i};\n";
                actual = actual->Siguiente;
            }

            graphviz += "    }\n";
            graphviz += "}\n";
            string filePath = "antiguos.dot";

            // Escribir el contenido en el archivo
            File.WriteAllText(filePath, graphviz);

            Console.WriteLine($"Archivo .dot generado: {filePath}");

            //generar la imagen
            ProcessStartInfo startInfo = new ProcessStartInfo("dot")
            {
                Arguments = $"-Tpng antiguos.dot -o antiguos.png",
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
            Process.Start(new ProcessStartInfo("antiguos.png") { UseShellExecute = true });
        }


        //* Método para liberar memoria manualmente
        public void LiberarMemoria()
        {
            NodoDoble* actual = cabeza;
            while (actual != null)
            {
                NodoDoble* temp = actual;
                actual = actual->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
            cabeza = null;
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
            graphviz += "        label = \"Lista Doble\";\n";

            // Iterar sobre los nodos de la lista y construir la representación Graphviz
            NodoDoble* actual = cabeza;
            int index = 0;

            while (actual != null)
            {
                string marca = new string(actual->Marca);
                string placa = new string(actual->Placa);
                graphviz += $"        n{index} [label = \"ID: {actual->Id} \\n ID Usuario: {actual->Id_Usuario} \\n Marca: {marca} \\n Modelo: {actual->Modelo} \\n Placa: {placa}\"];\n";
                Console.WriteLine($"ID: {actual->Id}, ID Usuario: {actual->Id_Usuario}, Marca: {marca}, Modelo: {actual->Modelo}, Placa: {placa}");
                actual = actual->Siguiente;
                index++;
            }

            // Conectar los nodos
            actual = cabeza;
            for (int i = 0; actual != null && actual->Siguiente != null; i++)
            {
                graphviz += $"        n{i} -> n{i + 1};\n";
                graphviz += $"        n{i+1} -> n{i};\n";
                actual = actual->Siguiente;
            }

            graphviz += "    }\n";
            graphviz += "}\n";
            string filePath = "vehiculos.dot";

            // Escribir el contenido en el archivo
            File.WriteAllText(filePath, graphviz);

            Console.WriteLine($"Archivo .dot generado: {filePath}");

            //generar la imagen
            ProcessStartInfo startInfo = new ProcessStartInfo("dot")
            {
                Arguments = $"-Tpng vehiculos.dot -o vehiculos.png",
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
            Process.Start(new ProcessStartInfo("vehiculos.png") { UseShellExecute = true });
        }
    }
}