public class NodoServicio
{
    public int Id { get; set; }
    public int Id_Repuesto { get; set; }
    public int Id_Vehiculo { get; set; }
    public string Detalles { get; set; }
    public double Costo { get; set; }
    public string MetodoPago { get; set; }
    public NodoServicio? Izquierda { get; set; }
    public NodoServicio? Derecha { get; set; }

    public NodoServicio(int id, int id_repuesto, int id_vehiculo, string detalles, double costo, string metodoPago)
    {
        Id = id;
        Id_Repuesto = id_repuesto;
        Id_Vehiculo = id_vehiculo;
        Detalles = detalles;
        Costo = costo;
        MetodoPago = metodoPago;
        Izquierda = null;
        Derecha = null;
    }
}