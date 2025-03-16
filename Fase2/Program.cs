using System;

class Program
{
    static void Main()
    {
        // Crear la lista de usuarios
        ListaUsuarios lista = new ListaUsuarios();
        ListaVehiculos listaVehiculos = new ListaVehiculos();
        ArbolBinarioServicio arbol = new ArbolBinarioServicio(); 
        AVLRepuesto arbolRepuesto = new AVLRepuesto();

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
        NodoServicio servicio6 = new NodoServicio(20, 12, 2006, "Cambio de aceite", 50.0);
        NodoServicio servicio7 = new NodoServicio(25, 13, 2007, "Cambio de aceite", 50.0);
        NodoServicio servicio8 = new NodoServicio(30, 14, 2008, "Cambio de aceite", 50.0);
        // Insertar los nodos en el árbol
        arbol.Insertar(servicio1);
        arbol.Insertar(servicio2);
        arbol.Insertar(servicio3);
        arbol.Insertar(servicio4);
        arbol.Insertar(servicio5);
        arbol.Insertar(servicio6);
        arbol.Insertar(servicio7);
        arbol.Insertar(servicio8);
        

        // Imprimir los recorridos
        // Console.WriteLine("Recorrido en preorden:");
        // arbol.RecorridoPreOrden();

        // Console.WriteLine("\nRecorrido en orden:");
        // arbol.RecorridoEnOrden();

        // Console.WriteLine("\nRecorrido en postorden:");
        // arbol.RecorridoPostOrden();

        arbol.GenerarGraphviz();

        NodoRepuesto repuesto1 = new NodoRepuesto(10, "Aceite", "Aceite sintético", 50.0);
        NodoRepuesto repuesto2 = new NodoRepuesto(5, "Frenos", "Pastillas de freno", 200.0);
        NodoRepuesto repuesto3 = new NodoRepuesto(15, "Batería", "Batería de 12V", 150.0);
        NodoRepuesto repuesto4 = new NodoRepuesto(3, "Filtro", "Filtro de aire", 25.0);
        NodoRepuesto repuesto5 = new NodoRepuesto(8, "Llantas", "Llantas de 16 pulgadas", 100.0);
        NodoRepuesto repuesto6 = new NodoRepuesto(20, "Llantas", "Llantas de 17 pulgadas", 120.0);
        NodoRepuesto repuesto7 = new NodoRepuesto(25, "Llantas", "Llantas de 18 pulgadas", 140.0);
        NodoRepuesto repuesto8 = new NodoRepuesto(30, "Llantas", "Llantas de 19 pulgadas", 160.0);

        // Insertar los nodos en el árbol
        arbolRepuesto.Insert(repuesto1);
        arbolRepuesto.Insert(repuesto2);
        arbolRepuesto.Insert(repuesto3);
        arbolRepuesto.Insert(repuesto4);
        arbolRepuesto.Insert(repuesto5);
        arbolRepuesto.Insert(repuesto6);
        arbolRepuesto.Insert(repuesto7);
        arbolRepuesto.Insert(repuesto8);

        // Imprimir los recorridos
        arbolRepuesto.GenerarGraphviz();



    }
}
