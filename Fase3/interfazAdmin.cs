using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using Gtk;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using Estructuras;

public class interfazAdmin : Window
{
    public interfazAdmin() : base("Administrador")
    {
        // Configuración de la ventana
        SetDefaultSize(400, 450);
        SetPosition(WindowPosition.Center);
        BorderWidth = 15;
        DeleteEvent += (sender, e) => Application.Quit();

        // Contenedor principal
        VBox mainBox = new VBox(false, 8);
        Add(mainBox);

        // Título
        Label titleLabel = new Label("<span font='18' weight='bold'>Administrador</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Justify = Justification.Center;
        titleLabel.MarginBottom = 15;
        mainBox.PackStart(titleLabel, false, false, 0);

        // Crear un contenedor para los botones con scroll
        ScrolledWindow scrolledWindow = new ScrolledWindow();
        scrolledWindow.ShadowType = ShadowType.EtchedIn;
        scrolledWindow.SetPolicy(PolicyType.Automatic, PolicyType.Automatic); // Habilitar scroll vertical y horizontal si es necesario

        // Contenedor interno para los botones (VBox)
        VBox buttonsBox = new VBox(false, 5);
        scrolledWindow.AddWithViewport(buttonsBox); // Añadir el contenedor de botones al Scroll

        // Crear botones para cada opción
        string[] opciones = {
            "Cargas Masivas",
            "Inserción de Usuarios",
            "Visualización de Usuarios",
            "Visualización de Repuestos",
            "Visualización de Logueo",
            "Generar Servicios",
            "Generar Reportes",
            "Generar Backup",
            "Cargar Backup"
        };

        foreach (string opcion in opciones)
        {
            Button btn = CrearBotonOpcion(opcion);
            buttonsBox.PackStart(btn, false, false, 5);
        }

        // Añadir el ScrolledWindow al contenedor principal
        mainBox.PackStart(scrolledWindow, true, true, 0);

        // Botón Salir
        Button btnSalir = CrearBotonSalir();
        mainBox.PackEnd(btnSalir, false, false, 10);

        ShowAll();
    }

    private Button CrearBotonOpcion(string texto)
    {
        Button btn = new Button(texto);
        btn.SetSizeRequest(300, 40);
        btn.ModifyBg(StateType.Normal, new Gdk.Color(50, 120, 180)); // Azul
        btn.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255)); // Texto blanco
        btn.ModifyBg(StateType.Prelight, new Gdk.Color(70, 140, 210)); // Azul claro al pasar mouse
        btn.Relief = ReliefStyle.None;

        // Asignar evento según la opción
        btn.Clicked += (sender, e) => AccionBoton(texto);

        return btn;
    }

    private Button CrearBotonSalir()
    {
        Button btn = new Button("Salir");
        btn.SetSizeRequest(100, 35);
        btn.ModifyBg(StateType.Normal, new Gdk.Color(0, 150, 0)); // Verde
        btn.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255)); // Texto blanco
        btn.Relief = ReliefStyle.None;
        btn.Clicked += (sender, e) =>
        {
            this.Destroy(); // Cierra esta ventana
            Application.Quit(); // Cierra la aplicación completamente
        };
        return btn;
    }

    public void Comprimir(string archivo, string salida, string arbol)
    {

        if (!File.Exists(archivo))
        {
            Console.WriteLine($"El archivo {archivo} no existe.");
            return;
        }

        string input = File.ReadAllText(archivo);
        var (compressed, root) = HuffmanCompression.CompressWithTree(input);

        File.WriteAllText(salida, compressed);
        File.WriteAllText(arbol, JsonSerializer.Serialize(root));

        Console.WriteLine("\nArchivo comprimido y árbol guardado exitosamente.");

    }

    // public void Descomprimir(){
    //     if (!File.Exists(compressedFile) || !File.Exists(treeFile))
    //         {
    //             Console.WriteLine("Archivo comprimido o árbol Huffman no encontrados.");
    //             return;
    //         }

    //         string compressed = File.ReadAllText(compressedFile);
    //         HuffmanNode root = JsonSerializer.Deserialize<HuffmanNode>(File.ReadAllText(treeFile));

    //         string decompressed = HuffmanCompression.Decompress(compressed, root);
    //         File.WriteAllText(decompressedFile, decompressed);
    //         // Guarda el archivo descomprimido
    //         Console.WriteLine("\nArchivo descomprimido exitosamente.");

    //         /// imprimir el contenido del archivo descomprimido
    //         /// 
    //         string descomprimido = File.ReadAllText(decompressedFile);
    //         Console.WriteLine("\nContenido del archivo descomprimido:");
    // }

    private void AccionBoton(string opcion)
    {
        // Aquí puedes implementar la lógica para cada botón
        Console.WriteLine($"Acción: {opcion}");

        // Ejemplo de cómo manejar diferentes opciones:
        switch (opcion)
        {
            case "Cargas Masivas":
                Application.Init();
                new CargaMasivaWindow();
                Application.Run();
                break;
            case "Inserción de Usuarios":
                Application.Init();
                new insertarUsuarios();
                Application.Run();
                break;
            case "Visualización de Usuarios":
                Application.Init();
                new visualizarUsuario();
                Application.Run();
                break;
            case "Visualización de Repuestos":
                Application.Init();
                new visualizarRepuestos();
                Application.Run();
                break;
            case "Visualización de Logueo":
                Datos.loginLista.GenerarJson();
                Application.Init();
                new visualizarLogin("Reportes\\listaLogin.json");
                Application.Run();
                break;
            case "Generar Servicios":
                Application.Init();
                new generarServicio();
                Application.Run();
                break;
            case "Generar Reportes":
                Application.Init();
                new reportes();
                Application.Run();
                break;
            case "Generar Backup":
                Datos.blockchain.GenerarJson();

                Comprimir("Vehiculos.json", "backup\\vehiculo.edd", "backup\\vhuffman_tree.json");
                Comprimir("Repuestos.json", "backup\\repuesto.edd", "backup\\rhuffman_tree.json");


                break;
            case "Cargar Backup":
                break;
        }
    }
}