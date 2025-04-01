public class NodoVehiculo{
    public int Id { get; set; }
    public int Id_Usuario { get; set; }
    public string Marca { get; set; }
    public int Modelo { get; set; }
    public string Placa { get; set; }
    public NodoVehiculo? Siguiente { get; set; }
    public NodoVehiculo? Anterior { get; set; }

    // Constructor del nodo para inicializar los valores
    public NodoVehiculo(int id, int id_usuario, string marca, int modelo, string placa)
    {
        Id = id;
        Id_Usuario = id_usuario;
        Marca = marca;
        Modelo = modelo;
        Placa = placa;
        Siguiente = null;
        Anterior = null;
    }

}