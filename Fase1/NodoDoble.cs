namespace Estructuras
{
    public unsafe struct NodoDoble
    {
        public int Id;
        public int Id_Usuario;
        public int Modelo;
        public fixed char Marca[50]; 
        public fixed char Placa[50];  
        public NodoDoble* Siguiente;
        public NodoDoble* Anterior; 

        public NodoDoble(int id, int id_usuario, string marca, int modelo, string placa)
        {
            Id = id;
            Id_Usuario = id_usuario;
            Modelo = modelo;
            Siguiente = null;
            Anterior = null;

            fixed (char* ptr = Marca)
                marca.AsSpan().CopyTo(new Span<char>(ptr, 50));

            fixed (char* ptr = Placa)
                placa.AsSpan().CopyTo(new Span<char>(ptr, 50));
        }

    }
}