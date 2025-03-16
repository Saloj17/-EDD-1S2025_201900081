public class NodoRepuesto
{
    // Atributos privados
    public int Id { get; set; }
    public string Repuesto{ get; set; }
    public string Detalle{ get; set; }
    public double Costo{ get; set; }
    public int Altura{ get; set; }
    public NodoRepuesto? Drcha{ get; set; }
    public NodoRepuesto? Izq{ get; set; }

    // Constructor
    public NodoRepuesto(int id, string repuesto, string detalle, double costo)
    {
        Id = id;
        Repuesto = repuesto;
        Detalle = detalle;
        Costo = costo;
        Altura = 1;
        Drcha = null;
        Izq = null;
    }
}