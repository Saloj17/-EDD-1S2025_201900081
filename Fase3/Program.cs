
using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using Estructuras;

/*
class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Application.Init();                  
        new interfazInicial();                  
        Application.Run(); 

        // Datos.vehiculosLista.AgregarVehiculoFinal(1, 1, "Toyota", 2020, "ABC123");
        // Datos.vehiculosLista.AgregarVehiculoFinal(2, 1, "Honda", 2021, "DEF456");
        // Datos.vehiculosLista.AgregarVehiculoFinal(3, 2, "Ford", 2022, "GHI789");
        // Datos.vehiculosLista.AgregarVehiculoFinal(4, 2, "Chevrolet", 2023, "JKL012");


        // NodoRepuesto nuevo1 = new NodoRepuesto(1, "frenos", "Frenos de auto", 7.0);
        // Datos.repuestosArbol.Insert(nuevo1);
        // NodoRepuesto nuevo2 = new NodoRepuesto(2, "aceite", "Aceite de motor", 5.0);
        // Datos.repuestosArbol.Insert(nuevo2);
        // NodoRepuesto nuevo3 = new NodoRepuesto(3, "filtro", "Filtro de aire", 2.5);
        // Datos.repuestosArbol.Insert(nuevo3);
        // NodoRepuesto nuevo4 = new NodoRepuesto(4, "bateria", "Bateria de auto", 10.0);
        // Datos.repuestosArbol.Insert(nuevo4);
        


        // Datos.grafoLista.Insertar(2,2);
        // Datos.grafoLista.Insertar(1,2);
        // Datos.grafoLista.Insertar(1,1);
        // Datos.grafoLista.Insertar(3,1);
        // Datos.grafoLista.Insertar(3,3);
        // Datos.grafoLista.Insertar(3,4);
        // Datos.grafoLista.GenerarGraphviz();
        // Datos.repuestosArbol.GenerarGraphviz();
        // Datos.vehiculosLista.GenerarGraphviz();
    }
}


*/

class Program
{
    static void Main(string[] args)
    {
        Application.Init();
        new interfazAdmin();
        Application.Run();
    }


}