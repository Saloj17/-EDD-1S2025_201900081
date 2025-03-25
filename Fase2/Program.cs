using Gtk;
using System;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Application.Init();                  // Inicializa GTK
        new LoginWindow();                   // Crea la ventana
        Application.Run();                   // Bucle principal
    }
} 