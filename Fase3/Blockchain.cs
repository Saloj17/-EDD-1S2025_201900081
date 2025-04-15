using System.IO;
using System.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Estructuras
{
    public class Blockchain
    {
        public List<Block> Chain { get; private set; }
        public Blockchain()
        {
            Chain = new List<Block>();
            // Crear el bloque génesis
            var genesisBlock = new Block(0, new Usuario(), "0000");
            Chain.Insert(0, genesisBlock);
        }
        public void AgregarUsuario(Usuario data)
        {
            int index = Chain[0].Index + 1;
            string previousHash = Chain[0].Hash;
            var newBlock = new Block(index, data, previousHash);
            Chain.Insert(0, newBlock);

        }

        public string GenerarDot()
        {

            StringBuilder dot = new StringBuilder();
            dot.AppendLine("digraph Blockchain {");
            dot.AppendLine("    node [shape=record, style=filled, fontname=\"Arial\"];");
            dot.AppendLine("    label = \"Blockchain Usuarios\n\n\";");
            dot.AppendLine("    labelloc = \"t\";");
            dot.AppendLine("    fontsize = 24;");
            dot.AppendLine("    fontname = \"Helvetica-Bold\";");

            for (int i = 0; i < Chain.Count; i++)
            {
                Block block = Chain[i];
                Usuario user = block.Data;

                // Usamos solo partes del hash para que no sea tan largo el label
                string hashShort = block.Hash.Substring(0, 6);
                string prevHashShort = block.PreviousHash.Length >= 6 ? block.PreviousHash.Substring(0, 6) : block.PreviousHash;

                dot.AppendLine($"    Block{i} [label=\"{{ INDEX: {block.Index} \\n TIMESTAMP: {block.Timestamp} \\n Id: {user.ID} \\n Nombre: {user.Nombres} -- Apellidos: {user.Apellidos} \\n Correo: {user.Correo} \\n Edad: {user.Edad} \\nContraseña: {user.Contrasenia} \\n Nonce: {block.Nonce} \\n HASH: {hashShort} \\n PREV: {prevHashShort} }}\"      style=\"filled\" fillcolor=\"#96cbb0\" fontname=\"Arial\" fontsize = 12];");

                if (i > 0)
                {
                    dot.AppendLine($"    Block{i} -> Block{i - 1};");
                }
            }

            dot.AppendLine("}");

            return dot.ToString();
        }


        public void GenerarGraphviz()
        {
            string codigoDot = GenerarDot();

            string filePath = "Reportes/usuarios.dot";

            // Escribir el contenido en el archivo
            File.WriteAllText(filePath, codigoDot);

            Console.WriteLine($"Archivo .dot generado: {filePath}");

            //generar la imagen
            ProcessStartInfo startInfo = new ProcessStartInfo("dot")
            {
                Arguments = $"-Tpng Reportes/usuarios.dot -o Reportes/usuarios.png",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            process.WaitForExit();

            //Mostrar la imagen generada
            Process.Start(new ProcessStartInfo("Reportes\\usuarios.png") { UseShellExecute = true });

        }

        public string EncriptacionSHa256(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();

            }
        }

        // Metodo para verificar la existencia de un usuario por su correo
        public bool ExisteUsuarioCorreo(string correo)
        {
            foreach (Block block in Chain)
            {
                if (block.Data.Correo == correo)
                {
                    return true;
                }
            }
            return false; 
        }

        // Metodo para buscar usuario por su correo
        public Usuario BuscarUsuarioCorreo(string correo)
        {
            foreach (Block block in Chain)
            {
                if (block.Data.Correo == correo)
                {
                    return block.Data;
                }
            }
            return null;
        }

        // Metodo para verificar la existencia de un usuario por su ID
        public bool ExisteUsuarioId(int id)
        {
            foreach (Block block in Chain)
            {
                if (block.Data.ID == id)
                {
                    return true;
                }
            }
            return false; 
        }

        // Metodo para buscar usuario por su ID
        public Usuario BuscarUsuarioId(int id)
        {
            foreach (Block block in Chain)
            {
                if (block.Data.ID == id)
                {
                    return block.Data;
                }
            }
            return null;
        }

        // Metodo para verificar si esta vacio el blockchain
        public bool EstaVacio()
        {
            return Chain.Count == 0;
        }

        // Metodo para mostrar los usuarios en consola
        public void Mostrar()
        {
            foreach (Block block in Chain)
            {
                Console.WriteLine($"ID: {block.Data.ID} \nNombre: {block.Data.Nombres}\nApellido: {block.Data.Apellidos}\nCorreo: {block.Data.Correo}\nEdad: {block.Data.Edad}\nContraseña: {block.Data.Contrasenia}");
            }
        }
    }
}