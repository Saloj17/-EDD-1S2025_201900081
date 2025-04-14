using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace Estructuras
{
    public class ArbolBinarioServicio
    {
        public NodoServicio? Raiz;

        public ArbolBinarioServicio()
        {
            Raiz = null;
        }

        // Método para insertar un nodo en el árbol
        public void Insertar(NodoServicio nuevoNodo)
        {
            Raiz = InsertarRec(Raiz, nuevoNodo);
        }

        // Función recursiva para insertar un nodo
        private NodoServicio? InsertarRec(NodoServicio? raiz, NodoServicio nuevoNodo)
        {
            // Si el árbol está vacío, crear un nuevo nodo
            if (raiz == null)
            {
                raiz = nuevoNodo;
                return raiz;
            }

            // Si el Id del nuevo nodo es menor, va a la izquierda, si es mayor va a la derecha
            if (nuevoNodo.Id < raiz.Id)
            {
                raiz.Izquierda = InsertarRec(raiz.Izquierda, nuevoNodo);
            }
            else if (nuevoNodo.Id > raiz.Id)
            {
                raiz.Derecha = InsertarRec(raiz.Derecha, nuevoNodo);
            }

            // Retornar el nodo sin cambios
            return raiz;
        }

        // Método para recorrer el árbol en orden (inorder)
        public void RecorridoEnOrden()
        {
            RecorridoEnOrdenRec(Raiz);
        }

        // Función recursiva para el recorrido en orden
        private void RecorridoEnOrdenRec(NodoServicio? raiz)
        {
            if (raiz != null)
            {
                RecorridoEnOrdenRec(raiz.Izquierda);
                Console.WriteLine($"ID: {raiz.Id}, Repuesto: {raiz.Id_Repuesto}, Vehículo: {raiz.Id_Vehiculo}, Detalles: {raiz.Detalles}, Costo: {raiz.Costo}");
                RecorridoEnOrdenRec(raiz.Derecha);
            }
        }

        // Método para recorrer el árbol en preorden (preorder)
        public void RecorridoPreOrden()
        {
            RecorridoPreOrdenRec(Raiz);
        }

        // Función recursiva para el recorrido en preorden
        private void RecorridoPreOrdenRec(NodoServicio? raiz)
        {
            if (raiz != null)
            {
                // Primero visitamos el nodo actual
                Console.WriteLine($"ID: {raiz.Id}, Repuesto: {raiz.Id_Repuesto}, Vehículo: {raiz.Id_Vehiculo}, Detalles: {raiz.Detalles}, Costo: {raiz.Costo}");
                // Luego recorremos el subárbol izquierdo
                RecorridoPreOrdenRec(raiz.Izquierda);
                // Finalmente, recorremos el subárbol derecho
                RecorridoPreOrdenRec(raiz.Derecha);
            }
        }

        // Método para recorrer el árbol en postorden (postorder)
        public void RecorridoPostOrden()
        {
            RecorridoPostOrdenRec(Raiz);
        }

        // Función recursiva para el recorrido en postorden
        private void RecorridoPostOrdenRec(NodoServicio? raiz)
        {
            if (raiz != null)
            {
                // Primero recorremos el subárbol izquierdo
                RecorridoPostOrdenRec(raiz.Izquierda);
                // Luego recorremos el subárbol derecho
                RecorridoPostOrdenRec(raiz.Derecha);
                // Finalmente, visitamos el nodo actual
                Console.WriteLine($"ID: {raiz.Id}, Repuesto: {raiz.Id_Repuesto}, Vehículo: {raiz.Id_Vehiculo}, Detalles: {raiz.Detalles}, Costo: {raiz.Costo}");
            }
        }

        //Metodo pre orden que devuelve un arreglo de nodos
        public List<NodoServicio> PreOrdenList()
        {
            List<NodoServicio> lista = new List<NodoServicio>();
            PreOrdenRec(Raiz, lista);
            return lista;
        }
        // Función recursiva para el recorrido en preorden y llenar la lista
        private void PreOrdenRec(NodoServicio? raiz, List<NodoServicio> lista)
        {
            if (raiz != null)
            {
                // Primero visitamos el nodo actual
                lista.Add(raiz);
                // Luego recorremos el subárbol izquierdo
                PreOrdenRec(raiz.Izquierda, lista);
                // Finalmente, recorremos el subárbol derecho
                PreOrdenRec(raiz.Derecha, lista);
            }
        }

        //Metodo in orden que devuelve un arreglo de nodos
        public List<NodoServicio> InOrdenList()
        {
            List<NodoServicio> lista = new List<NodoServicio>();
            InOrdenRec(Raiz, lista);
            return lista;
        }
        // Función recursiva para el recorrido en inorden y llenar la lista
        private void InOrdenRec(NodoServicio? raiz, List<NodoServicio> lista)
        {
            if (raiz != null)
            {
                // Primero recorremos el subárbol izquierdo
                InOrdenRec(raiz.Izquierda, lista);
                // Luego visitamos el nodo actual
                lista.Add(raiz);
                // Finalmente, recorremos el subárbol derecho
                InOrdenRec(raiz.Derecha, lista);
            }
        }

        //Metodo post orden que devuelve un arreglo de nodos
        public List<NodoServicio> PostOrdenList()
        {
            List<NodoServicio> lista = new List<NodoServicio>();
            PostOrdenRec(Raiz, lista);
            return lista;
        }
        // Función recursiva para el recorrido en postorden y llenar la lista
        private void PostOrdenRec(NodoServicio? raiz, List<NodoServicio> lista)
        {
            if (raiz != null)
            {
                // Primero recorremos el subárbol izquierdo
                PostOrdenRec(raiz.Izquierda, lista);
                // Luego recorremos el subárbol derecho
                PostOrdenRec(raiz.Derecha, lista);
                // Finalmente, visitamos el nodo actual
                lista.Add(raiz);
            }
        }
        // Método para generar el código DOT del árbol
        public void GenerarGraphviz()
        {
            string codigoDot = GenerarCodigoDot();

            string filePath = "Reportes/servicios.dot";

            // Escribir el contenido en el archivo
            File.WriteAllText(filePath, codigoDot);

            Console.WriteLine($"Archivo .dot generado: {filePath}");

            //generar la imagen
            ProcessStartInfo startInfo = new ProcessStartInfo("dot")
            {
                Arguments = $"-Tpng Reportes/servicios.dot -o Reportes/servicios.png",
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
            Process.Start(new ProcessStartInfo("Reportes\\servicios.png") { UseShellExecute = true });

        }

        public string GenerarCodigoDot()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("digraph G {");
            sb.AppendLine("label = \"Arbol Servicios\n\n\";\n");
            sb.AppendLine("labelloc = \"t\";");
            sb.AppendLine("fontsize = 24;");
            sb.AppendLine("fontname = \"Helvetica-Bold\";");
            GenerarCodigoDotRec(Raiz, sb);
            sb.AppendLine("}");
            return sb.ToString();
        }
        private void GenerarCodigoDotRec(NodoServicio? raiz, StringBuilder sb)
        {
            if (raiz != null)
            {
                // Crear un nodo para el nodo actual
                //Mostrar todos los datos del nodo
                sb.AppendLine($" {raiz.Id} [label=\"Id: {raiz.Id} \\n Repuesto: {raiz.Id_Repuesto}  |  Vehículo: {raiz.Id_Vehiculo} \\n Detalles: {raiz.Detalles} \\n Costo: {raiz.Costo}\" style=\"filled\" fillcolor=\"#96cbb0\" fontname=\"Helvetica-Bold\" fontsize = 18 ];");

                // Si hay subárbol izquierdo, crear una conexión
                if (raiz.Izquierda != null)
                {
                    sb.AppendLine($"  {raiz.Id} -> {raiz.Izquierda.Id};");
                    GenerarCodigoDotRec(raiz.Izquierda, sb);
                }

                // Si hay subárbol derecho, crear una conexión
                if (raiz.Derecha != null)
                {
                    sb.AppendLine($"  {raiz.Id} -> {raiz.Derecha.Id};");
                    GenerarCodigoDotRec(raiz.Derecha, sb);
                }
            }
        }

        // Método para buscar por id
        public NodoServicio? Buscar(int id)
        {
            return BuscarRec(Raiz, id);
        }
        // Función recursiva para buscar un nodo por id
        private NodoServicio? BuscarRec(NodoServicio? raiz, int id)
        {
            // Si el árbol está vacío o el id es igual al id del nodo actual, retornar el nodo
            if (raiz == null || raiz.Id == id)
            {
                return raiz;
            }

            // Si el id es menor, buscar en el subárbol izquierdo
            if (id < raiz.Id)
            {
                return BuscarRec(raiz.Izquierda, id);
            }

            // Si el id es mayor, buscar en el subárbol derecho
            return BuscarRec(raiz.Derecha, id);
        }

        // metodo para verificar si existe un nodo por id
        public bool Existe(int id)
        {
            return Buscar(id) != null;
        }
        
    }
}