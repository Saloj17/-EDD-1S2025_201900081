﻿using System;

namespace ListaCircular
{
    class Program
    {
        static unsafe void Main()
        {
            ListaCircular lista = new ListaCircular();

            lista.Insertar(1, "Aceite", "Aceite de motor",30.5);
            lista.Insertar(2, "Freno", "Freno delantero", 100.5);
            lista.Insertar(3, "Bujias", "De motor", 50);
            lista.Insertar(4, "Pastillas", "Pastillas de freno", 150.5);
            lista.Insertar(5, "Llantas", "Llantas de aleación", 200.5);

            Console.WriteLine("Lista de locales:");
            lista.Mostrar();
            lista.Visualizar();

            // Console.WriteLine("\nEliminando el nodo con ID 2...");
            // lista.Eliminar(2);

            // Console.WriteLine("Lista después de eliminar:");
            // lista.Mostrar();
            // lista.Visualizar();
        }
    }
}