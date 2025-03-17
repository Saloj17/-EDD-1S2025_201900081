using System;
using System.Diagnostics;
public class ListaUsuarios
{
    // Atributo que guarda el primer nodo de la lista
    private NodoUsuario? primero;

    // Constructor de la lista de usuarios
    public ListaUsuarios()
    {
        primero = null;
    }

    // Método para agregar un nuevo nodo al final de la lista
    public void AgregarUsuario(int id, string nombre, string apellido, string correo, int edad, string contrasenia)
    {
        NodoUsuario nuevo = new NodoUsuario(id, nombre, apellido, correo, edad, contrasenia);
        if (primero == null)
        {
            primero = nuevo;
        }
        else
        {
            NodoUsuario? actual = primero;
            while (actual.Siguiente != null)
            {
                if (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                else
                {
                    break;
                }
            }
            actual.Siguiente = nuevo;
        }
    }

    // Método mostrar
    public void Mostrar()
    {
        NodoUsuario? actual = primero;
        while (actual != null)
        {
            Console.WriteLine("ID: " + actual.Id);
            Console.WriteLine("Nombre: " + actual.Nombre);
            Console.WriteLine("Apellido: " + actual.Apellido);
            Console.WriteLine("Correo: " + actual.Correo);
            Console.WriteLine("Edad: " + actual.Edad);
            Console.WriteLine("Contraseña: " + actual.Contrasenia);
            Console.WriteLine();
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
        graphviz += "        label = \"Lista Usuarios\";\n";
        graphviz += "        lalbelloc = \"t\";\n";
        graphviz += "        fontsize = 24;\n";
        graphviz += "        fontname = \"Helvetica-Bold\";\n";

        // Iterar sobre los nodos de la lista y construir la representación Graphviz
        NodoUsuario? actual = primero;
        if (actual == null)
        {
            return;
        }
        int index = 0;

        while (actual != null)
        {
            graphviz += $"        n{index} [label = \"ID: {actual.Id} \\n Nombre: {actual.Nombre} \\n Apellido: {actual.Apellido} \\n Correo: {actual.Correo} \\n Edad: {actual.Edad} \\n Contrasenia: {actual.Contrasenia} \"  style=\"filled\" fillcolor=\"#96cbb0\" fontname=\"Helvetica-Bold\" fontsize = 18];\n";
            actual = actual.Siguiente;
            index++;
        }

        // Conectar los nodos
        actual = primero;
        for (int i = 0; actual != null && actual.Siguiente != null; i++)
        {
            graphviz += $"        n{i} -> n{i + 1};\n";
            actual = actual.Siguiente;
        }

        graphviz += "    }\n";
        graphviz += "}\n";

        string filePath = "Reportes/usuarios.dot";

        // Escribir el contenido en el archivo
        File.WriteAllText(filePath, graphviz);

        Console.WriteLine($"Archivo .dot generado: {filePath}");

        //generar la imagen
        ProcessStartInfo startInfo = new ProcessStartInfo("dot")
        {
            Arguments = $"-Tpng Reportes/usuarios.dot -o Reportes/usuarios.png",
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
        Process.Start(new ProcessStartInfo("C:/Users/SALOJ/Desktop/[EDD-25]/-EDD-1S2025_201900081/Fase2/Reportes/usuarios.png") { UseShellExecute = true });
    }
    // Metodo para eliminar un nodo por id
    public void EliminarUsuarioId(int id)
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

        NodoUsuario? actual = primero;
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

    // Método para buscar un nodo por id
    public NodoUsuario? BuscarUsuarioId(int id)
    {
        NodoUsuario? actual = primero;
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

    // Metodo booleano para buscar un nodo por id
    public bool ExisteUsuarioId(int id)
    {
        NodoUsuario? actual = primero;
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

    // Método para buscar un nodo por correo
    public NodoUsuario? BuscarUsuarioCorreo(string correo)
    {
        NodoUsuario? actual = primero;
        while (actual != null)
        {
            if (actual.Correo == correo)
            {
                return actual;
            }
            actual = actual.Siguiente;
        }
        return null;
    }

    // Método booleano para buscar un nodo por correo
    public bool ExisteUsuarioCorreo(string correo)
    {
        NodoUsuario? actual = primero;
        while (actual != null)
        {
            if (actual.Correo == correo)
            {
                return true;
            }
            actual = actual.Siguiente;
        }
        return false;
    }

}






