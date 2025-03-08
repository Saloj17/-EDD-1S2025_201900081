public class NodoUsuario
{   
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }        
    public string Correo { get; set; }
    public int Edad { get; set; }
    public string Contrasenia { get; set; }
    public NodoUsuario? Siguiente { get; set; }

    // Constructor del nodo para inicializar los valores
    public NodoUsuario(int id, string nombre, string apellido, string correo, int edad, string contrasenia)
    {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        Correo = correo;
        Edad = edad;
        Contrasenia = contrasenia;
        Siguiente = null;
    }
}

