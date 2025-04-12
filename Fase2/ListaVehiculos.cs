using System;
using System.Diagnostics;
namespace Estructuras
{
    public class ListaVehiculos
    {
        // Atributo que guarda el primer nodo de la lista
        private NodoVehiculo? primero;
        private NodoVehiculo? ultimo;

        // Constructor de la lista de usuarios
        public ListaVehiculos()
        {
            primero = null;
            ultimo = null;
        }

        // Método para agregar un nuevo nodo al final de la lista
        public void AgregarVehiculoFinal(int id, int id_usuario, string marca, int modelo, string placa)
        {
            NodoVehiculo nuevo = new NodoVehiculo(id, id_usuario, marca, modelo, placa);
            if (primero == null && ultimo == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                ultimo.Siguiente = nuevo;
                nuevo.Anterior = ultimo;
                ultimo = nuevo;
            }
        }

        public void AgregarVehiculoInicio(int id, int id_usuario, string marca, int modelo, string placa)
        {
            NodoVehiculo nuevo = new NodoVehiculo(id, id_usuario, marca, modelo, placa);
            if (primero == null && ultimo == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                nuevo.Siguiente = primero;
                primero.Anterior = nuevo;
                primero = nuevo;
            }
        }

        // Método mostrar
        public void Mostrar()
        {
            NodoVehiculo? actual = primero;
            while (actual != null)
            {
                Console.WriteLine("ID: " + actual.Id);
                Console.WriteLine("ID Usuario: " + actual.Id_Usuario);
                Console.WriteLine("Marca: " + actual.Marca);
                Console.WriteLine("Modelo: " + actual.Modelo);
                Console.WriteLine("Placa: " + actual.Placa);
                Console.WriteLine();
                actual = actual.Siguiente;
            }
        }

        //Metodo buscar nodo por id
        public NodoVehiculo? BuscarVehiculoId(int id)
        {
            NodoVehiculo? actual = primero;
            while (actual != null)
            {
                if (actual.Id == id)
                {
                    return actual;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        public void Vaciar()
        {
            NodoVehiculo? actual = primero;
            while (actual != null)
            {
                EliminarVehiculoId(actual.Id);
                actual = actual.Siguiente;
            }
        }

        // Metodo booleano para buscar un nodo por id
        public bool ExisteVehiculoId(int id)
        {
            NodoVehiculo? actual = primero;
            while (actual != null)
            {
                if (actual.Id == id)
                {
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        // Metodo para eliminar un nodo por id  
        public void EliminarVehiculoId(int id)
        {
            if (primero == null)
            {
                return;
            }

            if (primero.Id == id)
            {
                primero = primero.Siguiente;
                return;
            }

            NodoVehiculo? actual = primero;
            while (actual.Siguiente != null)
            {
                if (actual.Siguiente.Id == id)
                {
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    return;
                }
                actual = actual.Siguiente;
            }
        }


        public void GenerarGraphviz()
        {
            // Si la lista está vacía, generamos un solo nodo con "NULL"
            if (primero == null)
            {
                Console.WriteLine("La lista está vacía");
            }

            // Iniciamos el código Graphviz
            var graphviz = "digraph G {\n";
            graphviz += "    node [shape=ellipse];\n";
            graphviz += "    rankdir=LR;\n";
            graphviz += "    subgraph cluster_0 {\n";
            graphviz += "        label = \"Lista Vehiculos\";\n";
            graphviz += "        lalbelloc = \"t\";\n";
            graphviz += "        fontsize = 24;\n";
            graphviz += "        fontname = \"Helvetica-Bold\";\n";

            // Iterar sobre los nodos de la lista y construir la representación Graphviz
            NodoVehiculo? actual = primero;
            int index = 0;

            while (actual != null)
            {
                graphviz += $"n{index} [label = \"ID: {actual.Id} \\n ID Usuario: {actual.Id_Usuario} \\n Marca: {actual.Marca} \\n Modelo: {actual.Modelo} \\n Placa: {actual.Placa}\"style=\"filled\" fillcolor=\"#96cbb0\" fontname=\"Helvetica-Bold\" fontsize = 18];\n";

                actual = actual.Siguiente;
                index++;
            }

            // Conectar los nodos
            actual = primero;
            for (int i = 0; actual != null && actual.Siguiente != null; i++)
            {
                graphviz += $"        n{i} -> n{i + 1};\n";
                graphviz += $"        n{i + 1} -> n{i};\n";
                actual = actual.Siguiente;
            }

            graphviz += "    }\n";
            graphviz += "}\n";
            string filePath = "Reportes/vehiculos.dot";

            // Escribir el contenido en el archivo
            File.WriteAllText(filePath, graphviz);

            Console.WriteLine($"Archivo .dot generado: {filePath}");

            //generar la imagen
            ProcessStartInfo startInfo = new ProcessStartInfo("dot")
            {
                Arguments = $"-Tpng Reportes/vehiculos.dot -o Reportes/vehiculos.png",
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
            Process.Start(new ProcessStartInfo("C:\\Users\\SALOJ\\Desktop\\[EDD-25]\\-EDD-1S2025_201900081\\Fase2\\Reportes\\vehiculos.png") { UseShellExecute = true });
        }





    }

}