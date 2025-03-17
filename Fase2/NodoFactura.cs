using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Llave
{
    public int Id { get; set; }
    public int Id_Servicio { get; set; }
    public double Total { get; set; }
    public Llave? Prev { get; set; }
    public Llave? Sig { get; set; }
    public NodoFactura? Izquierda { get; set; }
    public NodoFactura? Derecha { get; set; }

    //contructor de la clase
    public Llave(int id, int idServicio, double total)
    {
        Id = id;
        Id_Servicio = idServicio;
        Total = total;
        Prev = null;
        Sig = null;
        Izquierda = null;
        Derecha = null;
    }

    public bool TieneHijos()
    {
        return Derecha != null && Izquierda != null;
    }
}

class NodoFactura
{
    public bool Hoja { get; set; }
    public Llave? Primero { get; set; }
    public int NumeroLlaves { get; set; }

    public NodoFactura()
    {
        Hoja = true;
        Primero = null;
        NumeroLlaves = 0;
    }

    public void InsertarLlave(Llave llave)
    {
        Llave puntero = Primero;

        if (Primero == null)
        {
            Primero = llave;
            NumeroLlaves++;
        }
        else if (llave.Id < Primero.Id)
        {
            llave.Sig = Primero;
            Primero.Izquierda = llave.Derecha;
            Primero.Prev = llave;
            Primero = llave;
            NumeroLlaves++;
        }
        else
        {
            while (puntero != null)
            {
                if (llave.Id == puntero.Id)
                {
                    Console.WriteLine($"El valor {llave.Id} ya se encuentra en el árbol.");
                    break;
                }
                else if (llave.Id < puntero.Id)
                {
                    puntero.Izquierda = llave.Derecha;
                    puntero.Prev.Derecha = llave.Izquierda;

                    llave.Sig = puntero;
                    llave.Prev = puntero.Prev;
                    puntero.Prev.Sig = llave;
                    puntero.Prev = llave;

                    NumeroLlaves++;
                    break;
                }
                else if (puntero.Sig == null)
                {
                    puntero.Sig = llave;
                    llave.Prev = puntero;
                    puntero.Derecha = llave.Izquierda;

                    NumeroLlaves++;
                    break;
                }
                puntero = puntero.Sig;
            }
        }
    }

    public bool EsHoja()
    {
        return Hoja;
    }

    public void SetHoja(bool hoja)
    {
        Hoja = hoja;
    }

    public void SetPrimero(Llave primero)
    {
        Primero = primero;
    }

    public void SetNumeroLlaves(int numeroLlaves)
    {
        NumeroLlaves = numeroLlaves;
    }
}
