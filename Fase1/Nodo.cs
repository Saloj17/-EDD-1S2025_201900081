using System;

namespace Estructuras
{
    public unsafe struct Node
    {
        public int Id;
        public fixed char Repuesto[50];     
        public fixed char Detalles[100];     
        public double Costo;                 
        public Node* Next;                   
        public Node(int id, string repuesto, string detalles, double costo)
        {
            Id = id;
            Costo = costo;
            Next = null;

            fixed (char* ptr = Repuesto)
                repuesto.AsSpan().CopyTo(new Span<char>(ptr, 50));

            fixed (char* ptr = Detalles)
                detalles.AsSpan().CopyTo(new Span<char>(ptr, 100));
        }

        public override string ToString()
        {
            fixed (char* ptrRepuesto = Repuesto, ptrDetalles = Detalles)
            {
                return $"ID: {Id}, Repuesto: {new string(ptrRepuesto)}, Detalles: {new string(ptrDetalles)}, Costo: {Costo}";
            }
        }
    }
}