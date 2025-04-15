
using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using Estructuras;

class Program
{
    static void Main(string[] args)
    {
        Application.Init();
        new interfazInicial();
        Application.Run();
    }
}