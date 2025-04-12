using System;
using System.Text;
class SubNodo
{
    public int Valor { get; set; }
    public SubNodo Siguiente { get; set; } = null;

    public SubNodo(int val)
    {
        Valor = val;
    }
}

class NodoGrafo
{
    public int Indice { get; set; }
    public NodoGrafo Siguiente { get; set; } = null;
    public NodoGrafo Anterior { get; set; } = null;
    public SubNodo Lista { get; set; } = null;

    public void Agregar(int val)
    {
        SubNodo nuevoNodo = new SubNodo(val);
        if (Lista == null)
        {
            Lista = nuevoNodo;
        }
        else 
        {
            SubNodo aux = Lista;
            while (aux.Siguiente != null)
            {
                aux = aux.Siguiente;
            }
            aux.Siguiente = nuevoNodo;
        }
    }

    public void Imprimir()
    {
        SubNodo aux = Lista;
        while (aux != null)
        {
            Console.Write($"{aux.Valor} ");
            aux = aux.Siguiente;
        }
        Console.WriteLine();
    }

    public string ObtenerCadena()
    {
        StringBuilder sb = new StringBuilder();
        SubNodo aux = Lista;
        while (aux != null)
        {
            sb.Append($"{aux.Valor} ");
            aux = aux.Siguiente;
        }
        return sb.ToString();
    }
}
