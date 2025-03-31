using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

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

            string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "listaLogin.json");
            File.WriteAllText(rutaArchivo, json);
            Process.Start(new ProcessStartInfo("explorer.exe", rutaArchivo) { UseShellExecute = true });
        }

        public string ObtenerFechaActualFormateada()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //Metodo para abrir el archivo JSON
        public void AbrirJson()
        {
            string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "listaLogin.json");
            if (File.Exists(rutaArchivo))
            {
                Process.Start(new ProcessStartInfo("explorer.exe", rutaArchivo) { UseShellExecute = true });
            }
            else
            {
                Console.WriteLine("El archivo JSON no existe.");
            }
        }

    }
}