namespace Estructuras
{
    public unsafe struct NodoSimple
    {
        public int Id;
        public fixed char Nombre[50]; 
        public fixed char Apellido[50]; 
        public fixed char Correo[50]; 
        public fixed char Contrasenia[50]; 
        public NodoSimple* Siguiente;

        public NodoSimple(int id, string nombre, string apellido, string correo, string contrasenia)
        {
            Id = id;
            Siguiente = null;

            fixed (char* ptr = Nombre)
                nombre.AsSpan().CopyTo(new Span<char>(ptr, 50));

            fixed (char* ptr = Apellido)
                apellido.AsSpan().CopyTo(new Span<char>(ptr, 50));

            fixed (char* ptr = Correo)
                correo.AsSpan().CopyTo(new Span<char>(ptr, 50));

            fixed (char* ptr = Contrasenia)
                contrasenia.AsSpan().CopyTo(new Span<char>(ptr, 50));
        }


    
    public void SetNombre(string nombre)
        {
            fixed (char* ptr = Nombre)
                nombre.AsSpan().CopyTo(new Span<char>(ptr, 50));  // Asignar a arreglo fijo
        }

        public void SetApellido(string apellido)
        {
            fixed (char* ptr = Apellido)
                apellido.AsSpan().CopyTo(new Span<char>(ptr, 50));  // Asignar a arreglo fijo
        }

        public void SetCorreo(string correo)
        {
            fixed (char* ptr = Correo)
                correo.AsSpan().CopyTo(new Span<char>(ptr, 50));  // Asignar a arreglo fijo
        }

        public void SetContrasenia(string contrasenia)
        {
            fixed (char* ptr = Contrasenia)
                contrasenia.AsSpan().CopyTo(new Span<char>(ptr, 50));  // Asignar a arreglo fijo
        }

        //metodo para limpiar memoria
        public void LiberarMemoria()
        {
            fixed (char* ptr = Nombre)
                for (int i = 0; i < 50; i++)
                    ptr[i] = '\0';

            fixed (char* ptr = Apellido)
                for (int i = 0; i < 50; i++)
                    ptr[i] = '\0';

            fixed (char* ptr = Correo)
                for (int i = 0; i < 50; i++)
                    ptr[i] = '\0';
        }
    }
}