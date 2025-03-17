using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public class AVLRepuesto

{
    public NodoRepuesto? raiz;

    // Constructor
    public AVLRepuesto()
    {
        raiz = null;
    }

    // Métodos públicos
    public void Insert(NodoRepuesto nodo)
    {
        if (raiz == null)
        {
            raiz = nodo;
        }
        else
        {
            raiz = Insert(raiz, nodo);
        }
    }

    public void PreOrden()
    {
        PreOrden(raiz);
    }

    public void InOrden()
    {
        InOrden(raiz);
    }

    public void PostOrden()
    {
        PostOrden(raiz, false);
    }

    // Métodos privados
    public NodoRepuesto? Insert(NodoRepuesto? raiz, NodoRepuesto nodo)
    {
        if (raiz == null)
        {
            return nodo;
        }
        else if (nodo.Id < raiz.Id)
        {
            raiz.Izquierda = Insert(raiz.Izquierda, nodo);
        }
        else if (nodo.Id > raiz.Id)
        {
            raiz.Derecha = Insert(raiz.Derecha, nodo);
        }

        raiz.Altura = AlturaMaxima(ObtenerAltura(raiz.Izquierda), ObtenerAltura(raiz.Derecha)) + 1;

        int balance = ObtenerBalance(raiz);

        if (balance > 1)
        {
            if (ObtenerBalance(raiz.Derecha) < 0)
            {
                raiz.Derecha = RotacionDerecha(raiz.Derecha);
                raiz = RotacionIzquierda(raiz);
            }
            else
            {
                raiz = RotacionIzquierda(raiz);
            }
        }

        if (balance < -1)
        {
            if (ObtenerBalance(raiz.Izquierda) > 0)
            {
                raiz.Izquierda = RotacionIzquierda(raiz.Izquierda);
                raiz = RotacionDerecha(raiz);
            }
            else
            {
                raiz = RotacionDerecha(raiz);
            }
        }

        return raiz;
    }

    public NodoRepuesto RotacionIzquierda(NodoRepuesto raiz)
    {
        NodoRepuesto? raizDerecha = raiz.Derecha;
        NodoRepuesto? temp = raizDerecha.Izquierda;

        raizDerecha.Izquierda = raiz;
        raiz.Derecha = temp;

        raiz.Altura = AlturaMaxima(ObtenerAltura(raiz.Izquierda), ObtenerAltura(raiz.Derecha)) + 1;
        raizDerecha.Altura = AlturaMaxima(ObtenerAltura(raizDerecha.Izquierda), ObtenerAltura(raizDerecha.Derecha)) + 1;

        return raizDerecha;
    }

    public NodoRepuesto RotacionDerecha(NodoRepuesto raiz)
    {
        NodoRepuesto? raizIzquierda = raiz.Izquierda;
        NodoRepuesto? temp = raizIzquierda.Derecha;

        raizIzquierda.Derecha = raiz;
        raiz.Izquierda = temp;

        raiz.Altura = AlturaMaxima(ObtenerAltura(raiz.Izquierda), ObtenerAltura(raiz.Derecha)) + 1;
        raizIzquierda.Altura = AlturaMaxima(ObtenerAltura(raizIzquierda.Izquierda), ObtenerAltura(raizIzquierda.Derecha)) + 1;

        return raizIzquierda;
    }

    public int ObtenerAltura(NodoRepuesto? raiz)
    {
        if (raiz == null) return 0;
        return raiz.Altura;
    }

    public int AlturaMaxima(int izq, int drcha)
    {
        return (izq >= drcha) ? izq : drcha;
    }

    public int ObtenerBalance(NodoRepuesto raiz)
    {
        return ObtenerAltura(raiz.Derecha) - ObtenerAltura(raiz.Izquierda);
    }

    public void PreOrden(NodoRepuesto? raiz)
    {
        if (raiz != null)
        {
            Console.WriteLine($"ID: {raiz.Id}, Repuesto: {raiz.Repuesto}, Detalle: {raiz.Detalle}, Costo: {raiz.Costo}");
            PreOrden(raiz.Izquierda);
            PreOrden(raiz.Derecha);
        }
    }

    public void InOrden(NodoRepuesto? raiz)
    {
        if (raiz != null)
        {
            InOrden(raiz.Izquierda);
            Console.WriteLine($"ID: {raiz.Id}, Repuesto: {raiz.Repuesto}, Detalle: {raiz.Detalle}, Costo: {raiz.Costo}");
            InOrden(raiz.Derecha);
        }
    }

    public void PostOrden(NodoRepuesto? raiz, bool accion)
    {
        if (raiz != null)
        {
            PostOrden(raiz.Izquierda, accion);
            PostOrden(raiz.Derecha, accion);
            if (!accion)
            {
                Console.WriteLine($"ID: {raiz.Id}, Repuesto: {raiz.Repuesto}, Detalle: {raiz.Detalle}, Costo: {raiz.Costo}");
            }
            else
            {
                // Liberar memoria (no es necesario en C# debido al recolector de basura)
                raiz = null;
            }
        }
    }


// Método para generar el código DOT del árbol
    public void GenerarGraphviz()
    {
        string codigoDot = GenerarCodigoDot();

        string filePath = "Reportes/repuestos.dot";

        // Escribir el contenido en el archivo
        File.WriteAllText(filePath, codigoDot);

        Console.WriteLine($"Archivo .dot generado: {filePath}");

        //generar la imagen
        ProcessStartInfo startInfo = new ProcessStartInfo("dot")
        {
            Arguments = $"-Tpng Reportes/repuestos.dot -o Reportes/repuestos.png",
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
        Process.Start(new ProcessStartInfo("C:/Users/SALOJ/Desktop/[EDD-25]/-EDD-1S2025_201900081/Fase2/Reportes/repuestos.png") { UseShellExecute = true });
    
    }

    public string GenerarCodigoDot()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("digraph G {");
        sb.AppendLine("label = \"AVL Repuestos\n\n\";\n");
        sb.AppendLine("labelloc = \"t\";");
        sb.AppendLine("fontsize = 24;");
        sb.AppendLine("fontname = \"Helvetica-Bold\";");
        GenerarCodigoDotRec(raiz, sb);
        sb.AppendLine("}");
        return sb.ToString();
    }
    private void GenerarCodigoDotRec(NodoRepuesto? raiz, StringBuilder sb)
    {
        if (raiz != null)
        {
            // Crear un nodo para el nodo actual
            //Mostrar todos los datos del nodo
            sb.AppendLine($" {raiz.Id} [label=\"Id: {raiz.Id} \\n Repuesto: {raiz.Repuesto} \\n Detalles: {raiz.Detalle} \\n Costo: {raiz.Costo}\"        style=\"filled\" fillcolor=\"#96cbb0\" fontname=\"Helvetica-Bold\" fontsize = 18];");

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

}