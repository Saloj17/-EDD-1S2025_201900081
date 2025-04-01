public class NodoRepuesto
{
    // Atributos privados
    public int Id { get; set; }
    public string Repuesto{ get; set; }
    public string Detalle{ get; set; }
    public double Costo{ get; set; }
    public int Altura{ get; set; }
    public NodoRepuesto? Derecha{ get; set; }
    public NodoRepuesto? Izquierda{ get; set; }

    // Constructor
    public NodoRepuesto(int id, string repuesto, string detalle, double costo)
    {
        Id = id;
        Repuesto = repuesto;
        Detalle = detalle;
        Costo = costo;
        Altura = 1;
        Derecha = null;
        Izquierda = null;
    }
}