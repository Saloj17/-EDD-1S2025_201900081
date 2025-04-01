namespace Estructuras
{
    public unsafe struct NodoFactura
    {
        public int Id;
        public int Id_Orden;
        public double Total;
        public NodoFactura* Siguiente;

        public NodoFactura(int id, int id_orden, double total)
        {
            Id = id;
            Id_Orden = id_orden;
            Total = total;
            Siguiente = null;
        }
        //setters para los atributos
        public void SetId(int id)
        {
            Id = id;
        }
        public void SetIdOrden(int id_orden)
        {
            Id_Orden = id_orden;
        }
        public void SetTotal(double total)
        {
            Total = total;
        }

        //metodo para limpiar memoria
        public void LiberarMemoria()
        {
            Total = 0;
        }
    }
}