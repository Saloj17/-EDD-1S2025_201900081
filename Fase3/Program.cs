
using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using Estructuras;

/*
class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Application.Init();                  
        new interfazInicial();                  
        Application.Run(); 

        // Datos.vehiculosLista.AgregarVehiculoFinal(1, 1, "Toyota", 2020, "ABC123");
        // Datos.vehiculosLista.AgregarVehiculoFinal(2, 1, "Honda", 2021, "DEF456");
        // Datos.vehiculosLista.AgregarVehiculoFinal(3, 2, "Ford", 2022, "GHI789");
        // Datos.vehiculosLista.AgregarVehiculoFinal(4, 2, "Chevrolet", 2023, "JKL012");


        // NodoRepuesto nuevo1 = new NodoRepuesto(1, "frenos", "Frenos de auto", 7.0);
        // Datos.repuestosArbol.Insert(nuevo1);
        // NodoRepuesto nuevo2 = new NodoRepuesto(2, "aceite", "Aceite de motor", 5.0);
        // Datos.repuestosArbol.Insert(nuevo2);
        // NodoRepuesto nuevo3 = new NodoRepuesto(3, "filtro", "Filtro de aire", 2.5);
        // Datos.repuestosArbol.Insert(nuevo3);
        // NodoRepuesto nuevo4 = new NodoRepuesto(4, "bateria", "Bateria de auto", 10.0);
        // Datos.repuestosArbol.Insert(nuevo4);
        


        // Datos.grafoLista.Insertar(2,2);
        // Datos.grafoLista.Insertar(1,2);
        // Datos.grafoLista.Insertar(1,1);
        // Datos.grafoLista.Insertar(3,1);
        // Datos.grafoLista.Insertar(3,3);
        // Datos.grafoLista.Insertar(3,4);
        // Datos.grafoLista.GenerarGraphviz();
        // Datos.repuestosArbol.GenerarGraphviz();
        // Datos.vehiculosLista.GenerarGraphviz();
    }
}


*/

class Program
{
    static void Main(string[] args)
    {
        // path to the file
        string path = "usuarios.json";
        // check if the file exists
        string jsonData = File.ReadAllText(path);
        //Console.WriteLine(jsonData);
        List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(jsonData);
        foreach (var usuario in usuarios)
        {
            // Encriptar la contraseña
            usuario.Contrasenia = EncriptacionSHa256(usuario.Contrasenia);
        }
        foreach (var usuario in usuarios)
        {
            Console.WriteLine($"ID: {usuario.ID}");
            Console.WriteLine($"Nombres: {usuario.Nombres}");
            Console.WriteLine($"Apellidos: {usuario.Apellidos}");
            Console.WriteLine($"Correo: {usuario.Correo}");
            Console.WriteLine($"Edad: {usuario.Edad}");
            Console.WriteLine($"Contrasenia: {usuario.Contrasenia}");
            Console.WriteLine();
        }
            
            foreach (var user in usuarios)
            {
                Datos.blockchain.AddBlock(user);
            }
            foreach (var block in Datos.blockchain.Chain)
            {
                if(block.Index == 0)
                {
                    continue; // Skip the genesis block
                }
                Console.WriteLine("----------------BLOQUE------------------------");
                Console.WriteLine($"Index: {block.Index}");
                Console.WriteLine($"Timestamp: {block.Timestamp}");
                Console.WriteLine($"ID: {block.Data.ID} {block.Data.Nombres}{block.Data.Apellidos}");
                Console.WriteLine($"Correo: {block.Data.Correo}");
                Console.WriteLine($"Edad: {block.Data.Edad}");
                Console.WriteLine($"Contrasenia: {block.Data.Contrasenia}");
                Console.WriteLine($"Data: {block.Data.Nombres} {block.Data.Apellidos}");
                Console.WriteLine($"Nonce: {block.Nonce}");
                Console.WriteLine($"PreviousHash: {block.PreviousHash}");
                Console.WriteLine($"Hash: {block.Hash}");
                Console.WriteLine();
            }
            
            Datos.blockchain.GenerarGraphviz();
    }
    // blockchain




    static string EncriptacionSHa256(string text)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower(); 
        
        }
    }
}