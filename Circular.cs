using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ListaCircular
{
    public unsafe class ListaCircular
    {
        private Node* head;

        public ListaCircular()
        {
            head = null;
        }

        public void Insertar(int id, string repuesto, string detalle, double costo)
        {
            Node* nuevoNodo = (Node*)Marshal.AllocHGlobal(sizeof(Node));
            *nuevoNodo = new Node(id, repuesto, detalle, costo);

            if (head == null)
            {
                head = nuevoNodo;
                head->Next = head; // Apunta a sí mismo
            }
            else
            {
                Node* temp = head;
                // Recorremos la lista hasta llegar al último nodo
                while (temp->Next != head)
                {
                    temp = temp->Next;
                }
                // Insertamos el nuevo nodo al final de la lista
                temp->Next = nuevoNodo;
                nuevoNodo->Next = head;
            }
        }

        public void Eliminar(int id)
        {
            // si la lista está vacía, no hay nada que eliminar
            if (head == null) return;
            // si el nodo a eliminar es la cabeza de la lista
            if (head->Id == id && head->Next == head)
            {
                Marshal.FreeHGlobal((IntPtr)head);
                head = null;
                return;
            }

            Node* temp = head;
            Node* prev = null;
            do
            {
                // si el nodo a eliminar es la cabeza de la lista
                if (temp->Id == id)
                {
                    if (prev != null)
                    {
                        prev->Next = temp->Next;
                    }
                    else
                    {
                        Node* last = head;
                        while (last->Next != head)
                        {
                            last = last->Next;
                        }
                        head = head->Next;
                        last->Next = head;
                    }
                    // liberamos la memoria del nodo eliminado
                    Marshal.FreeHGlobal((IntPtr)temp);
                    return;
                }
                // avanzamos al siguiente nodo
                prev = temp;
                temp = temp->Next;
            } while (temp != head);
        }

        public void Visualizar()
        {
            // Crear un archivo DOT
            using (StreamWriter archivo = new StreamWriter("reporte.dot"))
            {
                archivo.WriteLine("digraph Repuestos {");
                archivo.WriteLine("label = \"Lista Circular (José Luis - Saloj)\";");  // Este es el encabezado que aparece como título de todo el gráfico
                archivo.WriteLine("labelloc = \"t\";");
                archivo.WriteLine("    rankdir=LR;"); // Opcional: Organiza los nodos de izquierda a derecha

                if (head == null)
                {
                    archivo.WriteLine("    vacia [label=\"La lista está vacía\", shape=box];");
                }
                else
                {
                    Node* actual = head;
                    do
                    {
                        string repuesto = new string(actual->Repuesto);
                        archivo.WriteLine($"    \"{actual->Id}\" [label=\"Id: {actual->Id}\\nRepuesto: {repuesto}\\nCosto: {actual->Costo}\"];");
                        archivo.WriteLine($"    \"{actual->Id}\" -> \"{actual->Next->Id}\";");
                        actual = actual->Next;
                    } while (actual != head); // Corregido: Utilizamos el bucle do-while para asegurarnos de que se recorra toda la lista
                }

                archivo.WriteLine("}");
            }

            Console.WriteLine("Archivo DOT generado: reporte.dot");
            // Generar el archivo PNG utilizando Graphviz
            string comandoDot = "dot -Tpng reporte.dot -o reporte.png";
            var proceso = new System.Diagnostics.Process();
            proceso.StartInfo.FileName = "cmd.exe";
            proceso.StartInfo.Arguments = "/C " + comandoDot;
            proceso.StartInfo.RedirectStandardOutput = true;
            proceso.StartInfo.UseShellExecute = false;
            proceso.StartInfo.CreateNoWindow = true;
            proceso.Start();
            proceso.WaitForExit();

            string comandoI = "start reporte.png";
            var p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C " + comandoI;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            // Verificar si el comando se ejecutó correctamente
            if (proceso.ExitCode == 0)
            {
                Console.WriteLine("Archivo PNG generado: reporte.png");
            }
            else
            {
                Console.WriteLine("Error al generar el archivo PNG");
            }
        }
    

        public void Mostrar()
        {
            if (head == null)
            {
                Console.WriteLine("Lista vacía.");
                return;
            }

            Node* temp = head;
            do
            {
                Console.WriteLine(temp->ToString());
                temp = temp->Next;
            } while (temp != head);
        }
// destructor de la clase
        ~ListaCircular()
        {
            if (head == null) return;

            Node* temp = head;
            do
            {
                Node* next = temp->Next;
                Marshal.FreeHGlobal((IntPtr)temp);
                temp = next;
            } while (temp != head);
        }
    }
}