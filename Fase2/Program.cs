using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Estructuras;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Application.Init();                  // Inicializa GTK
        new interfazInicial();                   // Crea la ventana
        Application.Run();                   // Bucle principal
    }
} 