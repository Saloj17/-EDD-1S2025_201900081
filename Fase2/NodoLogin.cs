public class NodoLogin
{   
    public string Correo { get; set; }
    public string Entrada { get; set; }        
    public string Salida { get; set; }
    public NodoLogin? Siguiente { get; set; }

    // Constructor del nodo para inicializar los valores
    public NodoLogin(string correo, string entrada, string salida)
    {
        Correo = correo;
        Entrada = entrada;
        Salida = salida;
        Siguiente = null;
    }
}