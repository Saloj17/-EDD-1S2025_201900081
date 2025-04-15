using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Runtime.InteropServices;

namespace Estructuras
{
    public class ListaLogin
    {
        private NodoLogin? primero;

        public ListaLogin()
        {
            primero = null;
        }

        public void Vaciar()
        {
            NodoLogin? actual = primero;
            while (actual != null)
            {
                NodoLogin? temp = actual;
                actual = actual.Siguiente;
                temp.Siguiente = null;
            }
            primero = null;
        }

        // Mostrar la lista de logins
        public void MostrarLista()
        {
            NodoLogin? actual = primero;
            while (actual != null)
            {
                Console.WriteLine($"\nCorreo: {actual.Correo}\nEntrada: {actual.Entrada}\nSalida: {actual.Salida}\n");
                actual = actual.Siguiente;
            }
        }

        //Metodo para modificar por correo
        public void ModificarLogin(string correo, string salida)
        {
            NodoLogin? actual = primero;
            while (actual != null)
            {
                if (actual.Correo == correo)
                {
                    actual.Salida = salida;
                    break;
                }
                actual = actual.Siguiente;
            }
        }

        public void AgregarLogin(string correo, string entrada, string salida)
        {
            NodoLogin nuevo = new NodoLogin(correo, entrada, salida);
            if (primero == null)
            {
                primero = nuevo;
            }
            else
            {
                NodoLogin? actual = primero;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
        }

        public List<NodoLogin> ObtenerLista()
        {
            List<NodoLogin> lista = new List<NodoLogin>();
            NodoLogin? actual = primero;
            while (actual != null)
            {
                lista.Add(actual);
                actual = actual.Siguiente;
            }
            return lista;
        }

        





    private static readonly string BaseDirectory = "C:\\Users\\SALOJ\\Desktop\\[EDD-25]\\-EDD-1S2025_201900081\\Fase3\\Reportes";

    public void GenerarJson()
    {
        List<NodoLogin> lista = ObtenerLista();

        // Proyectar solo las propiedades necesarias
        var datosFiltrados = lista.Select(nodo => new
        {
            Correo = nodo.Correo,
            Entrada = nodo.Entrada,
            Salida = nodo.Salida
        }).ToList();

        string json = JsonSerializer.Serialize(datosFiltrados, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        // Crear el directorio si no existe
        Directory.CreateDirectory(BaseDirectory);
        
        // Ruta completa del archivo
        string rutaArchivo = Path.Combine(BaseDirectory, "listaLogin.json");
        
        File.WriteAllText(rutaArchivo, json);
        
        Console.WriteLine($"Archivo JSON generado en: {rutaArchivo}");
    }

    public string ObtenerFechaActualFormateada()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public void AbrirJson()
    {
        string rutaArchivo = Path.Combine(BaseDirectory, "listaLogin.json");
        
        if (File.Exists(rutaArchivo))
        {
            AbrirArchivo(rutaArchivo);
        }
        else
        {
            Console.WriteLine($"El archivo JSON no existe en: {rutaArchivo}");
        }
    }

    private void AbrirArchivo(string rutaArchivo)
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", rutaArchivo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start("explorer.exe", rutaArchivo);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", rutaArchivo);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al abrir el archivo: {ex.Message}");
        }
    }







    }
}