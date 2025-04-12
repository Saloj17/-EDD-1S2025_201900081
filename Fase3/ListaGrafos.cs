using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
namespace Estructuras
{

    class ListaGrafos
    {
        public NodoGrafo Cabecera { get; set; } = null;
        public NodoGrafo Cola { get; set; } = null;

        public void Insertar(int indice, int valor)
        {
            Datos.vehiculosLista.BuscarVehiculoId(indice);
            if (Datos.vehiculosLista.BuscarVehiculoId(indice) == null)
            {
                Console.WriteLine("El vehiculo no existe.");
                return;
            }
            Datos.repuestosArbol.BuscarPorId(valor);
            if (Datos.repuestosArbol.BuscarPorId(valor) == null)
            {
                Console.WriteLine("El repuesto no existe.");
                return;
            }

            NodoGrafo nuevoNodo = new NodoGrafo();
            nuevoNodo.Indice = indice;

            if (Cabecera == null)
            {
                Cabecera = nuevoNodo;
                Cola = nuevoNodo;
                nuevoNodo.Agregar(valor);
            }
            else
            {
                if (indice < Cabecera.Indice)
                {
                    Cabecera.Anterior = nuevoNodo;
                    nuevoNodo.Siguiente = Cabecera;
                    Cabecera = nuevoNodo;
                    nuevoNodo.Agregar(valor);
                }
                else
                {
                    NodoGrafo aux = Cabecera;
                    while (aux.Siguiente != null && indice > aux.Siguiente.Indice)
                    {
                        aux = aux.Siguiente;
                    }

                    if (indice == aux.Indice)
                    {
                        aux.Agregar(valor);
                    }
                    else
                    {
                        nuevoNodo.Siguiente = aux.Siguiente;
                        nuevoNodo.Anterior = aux;

                        if (aux.Siguiente != null)
                        {
                            aux.Siguiente.Anterior = nuevoNodo;
                        }
                        else
                        {
                            Cola = nuevoNodo;
                        }

                        aux.Siguiente = nuevoNodo;
                        nuevoNodo.Agregar(valor);
                    }
                }
            }
        }

        public void ImprimirLista()
        {
            NodoGrafo aux = Cabecera;
            while (aux != null)
            {
                Console.WriteLine($"Indice: {aux.Indice}");
                aux.Imprimir();
                aux = aux.Siguiente;
            }
        }

        public string ObtenerListaCadena()
        {
            StringBuilder sb = new StringBuilder();
            NodoGrafo aux = Cabecera;
            while (aux != null)
            {
                sb.AppendLine($"Indice: {aux.Indice}");
                sb.AppendLine(aux.ObtenerCadena());
                aux = aux.Siguiente;
            }
            return sb.ToString();
        }

        private void GenerarDot()
        {
            using (StreamWriter file = new StreamWriter("Reportes/grafo.dot"))
            {
                file.WriteLine("graph G {");
                file.WriteLine("rankdir =\"LR\";");
                file.WriteLine("label = \"Grafos\n\n\";\n");
                file.WriteLine("labelloc = \"t\";");
                file.WriteLine("fontsize = 24;");
                file.WriteLine("fontname = \"Helvetica-Bold\";");

                NodoGrafo nodoActual = Cabecera;
                while (nodoActual != null)
                {
                    file.WriteLine($"V{nodoActual.Indice}[label=\"V{nodoActual.Indice} \n Id Usuario: {Datos.vehiculosLista.BuscarVehiculoId(nodoActual.Indice).Id_Usuario}\n Marca: {Datos.vehiculosLista.BuscarVehiculoId(nodoActual.Indice).Marca}\n Modelo: {Datos.vehiculosLista.BuscarVehiculoId(nodoActual.Indice).Modelo} \n Placa: {Datos.vehiculosLista.BuscarVehiculoId(nodoActual.Indice).Placa}\"  style=\"filled\" fillcolor=\"#96cbb0\" fontname=\"Helvetica-Bold\" fontsize = 18];");


                    SubNodo subNodoActual = nodoActual.Lista;

                    file.WriteLine($"R{subNodoActual.Valor} [label=\"R{subNodoActual.Valor} \n Repuesto: {Datos.repuestosArbol.BuscarPorId(subNodoActual.Valor).Repuesto} \n Detalles: {Datos.repuestosArbol.BuscarPorId(subNodoActual.Valor).Detalle} \n Costo: {Datos.repuestosArbol.BuscarPorId(subNodoActual.Valor).Costo}\"   style=\"filled\" fillcolor=\"#96cbb0\" fontname=\"Helvetica-Bold\" fontsize = 18];");
                    while (subNodoActual != null)
                    {
                        file.WriteLine($"V{nodoActual.Indice} -- R{subNodoActual.Valor} [dir=normal];");
                        subNodoActual = subNodoActual.Siguiente;
                    }

                    nodoActual = nodoActual.Siguiente;
                }

                file.WriteLine("}");
            }

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dot",
                    Arguments = "-Tpng Reportes/grafo.dot -o Reportes/grafo.png",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        Console.WriteLine("Error al crear la imagen.");
                    }
                    else
                    {
                        Console.WriteLine("Imagen creada con éxito.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar la imagen: {ex.Message}");
            }
        }
        public void GenerarGraphviz()
        {
            GenerarDot();
            Process.Start(new ProcessStartInfo("Reportes\\grafo.png") { UseShellExecute = true });
        }
    }

}