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
            raiz.Izq = Insert(raiz.Izq, nodo);
        }
        else if (nodo.Id > raiz.Id)
        {
            raiz.Drcha = Insert(raiz.Drcha, nodo);
        }

        raiz.Altura = AlturaMaxima(ObtenerAltura(raiz.Izq), ObtenerAltura(raiz.Drcha)) + 1;

        int balance = ObtenerBalance(raiz);

        if (balance > 1)
        {
            if (ObtenerBalance(raiz.Drcha) < 0)
            {
                raiz.Drcha = RotacionDerecha(raiz.Drcha);
                raiz = RotacionIzquierda(raiz);
            }
            else
            {
                raiz = RotacionIzquierda(raiz);
            }
        }

        if (balance < -1)
        {
            if (ObtenerBalance(raiz.Izq) > 0)
            {
                raiz.Izq = RotacionIzquierda(raiz.Izq);
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
        NodoRepuesto? raizDerecha = raiz.Drcha;
        NodoRepuesto? temp = raizDerecha.Izq;

        raizDerecha.Izq = raiz;
        raiz.Drcha = temp;

        raiz.Altura = AlturaMaxima(ObtenerAltura(raiz.Izq), ObtenerAltura(raiz.Drcha)) + 1;
        raizDerecha.Altura = AlturaMaxima(ObtenerAltura(raizDerecha.Izq), ObtenerAltura(raizDerecha.Drcha)) + 1;

        return raizDerecha;
    }

    public NodoRepuesto RotacionDerecha(NodoRepuesto raiz)
    {
        NodoRepuesto? raizIzquierda = raiz.Izq;
        NodoRepuesto? temp = raizIzquierda.Drcha;

        raizIzquierda.Drcha = raiz;
        raiz.Izq = temp;

        raiz.Altura = AlturaMaxima(ObtenerAltura(raiz.Izq), ObtenerAltura(raiz.Drcha)) + 1;
        raizIzquierda.Altura = AlturaMaxima(ObtenerAltura(raizIzquierda.Izq), ObtenerAltura(raizIzquierda.Drcha)) + 1;

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
        return ObtenerAltura(raiz.Drcha) - ObtenerAltura(raiz.Izq);
    }

    public void PreOrden(NodoRepuesto? raiz)
    {
        if (raiz != null)
        {
            Console.WriteLine($"ID: {raiz.Id}, Repuesto: {raiz.Repuesto}, Detalle: {raiz.Detalle}, Costo: {raiz.Costo}");
            PreOrden(raiz.Izq);
            PreOrden(raiz.Drcha);
        }
    }

    public void InOrden(NodoRepuesto? raiz)
    {
        if (raiz != null)
        {
            InOrden(raiz.Izq);
            Console.WriteLine($"ID: {raiz.Id}, Repuesto: {raiz.Repuesto}, Detalle: {raiz.Detalle}, Costo: {raiz.Costo}");
            InOrden(raiz.Drcha);
        }
    }

    public void PostOrden(NodoRepuesto? raiz, bool accion)
    {
        if (raiz != null)
        {
            PostOrden(raiz.Izq, accion);
            PostOrden(raiz.Drcha, accion);
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
        sb.AppendLine("label = \"AVL Repuestos\";\n");
        sb.AppendLine("labelloc = \"t\";");
        sb.AppendLine("fontsize = 24;");
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
            sb.AppendLine($" {raiz.Id} [label=\"Id: {raiz.Id} \\n Repuesto: {raiz.Repuesto} \\n Detalles: {raiz.Detalle} \\n Costo: {raiz.Costo}\"];");

            // Si hay subárbol izquierdo, crear una conexión
            if (raiz.Izq != null)
            {
                sb.AppendLine($"  {raiz.Id} -> {raiz.Izq.Id};");
                GenerarCodigoDotRec(raiz.Izq, sb);
            }

            // Si hay subárbol derecho, crear una conexión
            if (raiz.Drcha != null)
            {
                sb.AppendLine($"  {raiz.Id} -> {raiz.Drcha.Id};");
                GenerarCodigoDotRec(raiz.Drcha, sb);
            }
        }
    }

}