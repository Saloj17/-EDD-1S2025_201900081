using System;

class Program
{
    static void Main()
    {
        // Crear la lista de usuarios
        ListaUsuarios lista = new ListaUsuarios();
        ListaVehiculos listaVehiculos = new ListaVehiculos();
        ArbolBinarioServicio arbol = new ArbolBinarioServicio(); 

        // Agregar usuarios
        lista.AgregarUsuario(1, "Juan", "Pérez","juan@gmail.com", 25, "1234");
        lista.AgregarUsuario(2, "Ana", "López", "ana@gmail.com", 30, "5678");
        lista.AgregarUsuario(3, "Pedro", "Gómez", "pedro@gmail.com", 35, "abcd");

        // lista.Mostrar();
        // lista.GenerarGraphviz();

        // Crear la lista de vehículos
        listaVehiculos.AgregarVehiculoFinal(1, 1, "Toyota", 2020, "ABC123");
        listaVehiculos.AgregarVehiculoFinal(2, 1, "Nissan", 2019, "DEF456");    
        listaVehiculos.AgregarVehiculoFinal(3, 2, "Chevrolet", 2018, "GHI789");
        listaVehiculos.AgregarVehiculoFinal(4, 3, "Ford", 2017, "JKL012");
        listaVehiculos.AgregarVehiculoFinal(5, 3, "Mazda", 2016, "MNO345");
        listaVehiculos.AgregarVehiculoFinal(6, 3, "Honda", 2015, "PQR678");
        listaVehiculos.AgregarVehiculoFinal(7, 3, "Kia", 2014, "STU901");
        listaVehiculos.AgregarVehiculoFinal(8, 3, "Hyundai", 2013, "VWX234");

        // listaVehiculos.Mostrar();
        // listaVehiculos.GenerarGraphviz();

        NodoServicio servicio1 = new NodoServicio(10, 101, 2001, "Cambio de aceite", 50.0);
        NodoServicio servicio2 = new NodoServicio(5, 102, 2002, "Cambio de frenos", 200.0);
        NodoServicio servicio3 = new NodoServicio(15, 103, 2003, "Reemplazo de batería", 150.0);
        NodoServicio servicio4 = new NodoServicio(3, 104, 2004, "Revisión general", 30.0);
        NodoServicio servicio5 = new NodoServicio(8, 105, 2005, "Cambio de filtro", 25.0);

        // Insertar los nodos en el árbol
        arbol.Insertar(servicio1);
        arbol.Insertar(servicio2);
        arbol.Insertar(servicio3);
        arbol.Insertar(servicio4);
        arbol.Insertar(servicio5);

        // Imprimir los recorridos
        Console.WriteLine("Recorrido en preorden:");
        arbol.RecorridoPreOrden();

        Console.WriteLine("\nRecorrido en orden:");
        arbol.RecorridoEnOrden();

        Console.WriteLine("\nRecorrido en postorden:");
        arbol.RecorridoPostOrden();

        arbol.GenerarGraphviz();


    }
}
