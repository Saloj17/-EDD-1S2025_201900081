namespace Estructuras
{
    public unsafe struct NodoServicio
    {
        public int Id;
        public int Id_Repuesto;
        public int Id_Vehiculo;
        public fixed char Detalles[50]; 
        public double Costo;
        public NodoServicio* Siguiente;

        public NodoServicio(int id, int id_repuesto, int id_vehiculo, string detalles, double costo)
        {
            Id = id;
            Id_Repuesto = id_repuesto;
            Id_Vehiculo = id_vehiculo;
            Costo = costo;
            Siguiente = null;

            fixed (char* ptr = Detalles)
                detalles.AsSpan().CopyTo(new Span<char>(ptr, 50));
        }
        //setters para los atributos
        public void SetId(int id)
        {
            Id = id;
        }
        public void SetIdRepuesto(int id_repuesto)
        {
            Id_Repuesto = id_repuesto;
        }
        public void SetIdVehiculo(int id_vehiculo)
        {
            Id_Vehiculo = id_vehiculo;
        }
        public void SetCosto(double costo)
        {
            Costo = costo;
        }

        public void SetDetalles(string detalles)
        {
            fixed (char* ptr = Detalles)
                detalles.AsSpan().CopyTo(new Span<char>(ptr, 50));  // Asignar a arreglo fijo
        }

        //metodo para limpiar memoria
        public void LiberarMemoria()
        {
            fixed (char* ptr = Detalles)
                for (int i = 0; i < 50; i++)
                    ptr[i] = '\0';
        }

    }
}