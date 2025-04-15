using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Estructuras;


public class CargaMasivaWindow : Window
{
    public CargaMasivaWindow() : base("Carga Masiva Usuario")
    {
        // Configuración básica de la ventana
        SetDefaultSize(450, 350);
        SetPosition(WindowPosition.Center);
        BorderWidth = 20;
        DeleteEvent += (sender, e) => Application.Quit();

        // Contenedor principal
        VBox mainBox = new VBox(false, 10);
        Add(mainBox);

        // Título
        Label titleLabel = new Label("<span font='18' weight='bold'>Carga Masiva Usuario</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Justify = Justification.Center;
        titleLabel.ModifyFg(StateType.Normal, new Gdk.Color(50, 120, 180));
        mainBox.PackStart(titleLabel, false, false, 15);

        // Tabla para organizar los elementos
        Table optionsTable = new Table(3, 2, false);
        optionsTable.RowSpacing = 20;
        optionsTable.ColumnSpacing = 30;

        // Botones de carga (verdes)
        CreateSection(optionsTable, 0, "Usuario", CargarUsuarios, new Gdk.Color(50, 120, 180)); // Verde
        CreateSection(optionsTable, 1, "Vehículos", CargarVehiculos, new Gdk.Color(50, 120, 180)); // Verde
        CreateSection(optionsTable, 2, "Repuestos", CargarRepuestos, new Gdk.Color(50, 120, 180)); // Verde

        mainBox.PackStart(optionsTable, true, true, 0);

        Button exitButton = new Button("Salir");
        exitButton.SetSizeRequest(120, 40);
        exitButton.ModifyBg(StateType.Normal, new Gdk.Color(0, 150, 0)); // verde
        exitButton.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255)); // Texto blanco
        exitButton.Relief = ReliefStyle.None;
        exitButton.Clicked += (sender, e) => {
            this.Destroy(); // Cierra esta ventana
            Application.Quit(); // Cierra la aplicación completamente
        };

        // Eliminamos cualquier posible doble manejo de eventos
        exitButton.CanFocus = true;
        exitButton.FocusOnClick = true;

        Alignment buttonAlign = new Alignment(1f, 0f, 0f, 0f);
        buttonAlign.Add(exitButton);
        mainBox.PackStart(buttonAlign, false, false, 15);

        ShowAll();
    }

    private void CreateSection(Table table, uint row, string labelText, EventHandler handler, Gdk.Color color)
    {
        // Label de la sección
        Label sectionLabel = new Label(labelText);
        sectionLabel.ModifyFont(Pango.FontDescription.FromString("Arial 12"));
        sectionLabel.ModifyFg(StateType.Normal, new Gdk.Color(50, 120, 180));
        table.Attach(sectionLabel, 0, 1, row, row+1, AttachOptions.Fill, AttachOptions.Fill, 0, 0);

        // Botón Cargar (verde)
        Button loadButton = new Button("Cargar");
        loadButton.SetSizeRequest(120, 35);
        loadButton.ModifyBg(StateType.Normal, color);
        loadButton.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255));
        loadButton.ModifyBg(StateType.Prelight, new Gdk.Color(86, 185, 90)); // Verde más claro al pasar mouse
        loadButton.Relief = ReliefStyle.None;
        loadButton.Clicked += handler;
        table.Attach(loadButton, 1, 2, row, row+1, AttachOptions.Fill, AttachOptions.Fill, 0, 0);
    }

    private void CargarUsuarios(object sender, EventArgs e)
    {
        // ShowMessage("Carga de usuarios iniciada");

        FileChooserDialog filechooser = new FileChooserDialog("Seleccione un archivo",
                    this,
                    FileChooserAction.Open,
                    "Cancelar", ResponseType.Cancel,
                    "Abrir", ResponseType.Accept);
                //mostrar el diálogo
                if (filechooser.Run() == (int)ResponseType.Accept)
                {
                    try
                    {
                        // Mostrar la ruta del archivo seleccionado
                        Console.WriteLine($"Archivo seleccionado: {filechooser.Filename}");

                        // Leer el contenido del archivo
                        string contenido = File.ReadAllText(filechooser.Filename);

                        // Intentar deserializar el JSON
                        // en una lista de objetos de tipo Usuario
                        List<Persona> personas = JsonSerializer.Deserialize<List<Persona>>(contenido);

                        // Verificar si la lista no está vacía
                        if (personas != null)
                        {
                            Console.WriteLine($"Total de personas cargados: {personas.Count}");
                            Datos.usuariosLista.Vaciar();
                            // Imprimir los datos
                            foreach (var persona in personas)
                            {   
                                if (!Datos.usuariosLista.ExisteUsuarioId(persona.ID))
                                {
                                    Datos.usuariosLista.AgregarUsuario(persona.ID, persona.Nombres, persona.Apellidos, persona.Correo, persona.Edad, persona.Contrasenia);
                                }
                                else
                                {
                                    Console.WriteLine($"El usuario con ID {persona.ID} ya existe");
                                }
                                
                            }
                        }
                        else
                        {
                            Console.WriteLine("El archivo no contiene datos válidos.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
                    }
                }


                // Cerrar el diálogo
                filechooser.Destroy();
                Datos.usuariosLista.Mostrar();



    }

    private void CargarVehiculos(object sender, EventArgs e)
    {
        // ShowMessage("Carga de vehículos iniciada");
        FileChooserDialog filechooser = new FileChooserDialog("Seleccione un archivo",
                    this,
                    FileChooserAction.Open,
                    "Cancelar", ResponseType.Cancel,
                    "Abrir", ResponseType.Accept);
                //mostrar el diálogo
                if (filechooser.Run() == (int)ResponseType.Accept)
                {
                    try
                    {
                        // Mostrar la ruta del archivo seleccionado
                        Console.WriteLine($"Archivo seleccionado: {filechooser.Filename}");

                        // Leer el contenido del archivo
                        string contenido = File.ReadAllText(filechooser.Filename);

                        // Intentar deserializar el JSON
                        // en una lista de objetos de tipo Vehiculo
                        List<Vehiculo> vehiculos = JsonSerializer.Deserialize<List<Vehiculo>>(contenido);

                        // Verificar si la lista no está vacía
                        if (vehiculos != null)
                        {
                            Console.WriteLine($"Total de vehiculos cargados: {vehiculos.Count}");
                            Datos.vehiculosLista.Vaciar();
                            // Imprimir los datos
                            foreach (var vehiculo in vehiculos)
                            {   
                                if (!Datos.vehiculosLista.ExisteVehiculoId(vehiculo.ID))
                                {
                                    Datos.vehiculosLista.AgregarVehiculoFinal(vehiculo.ID, vehiculo.ID_Usuario, vehiculo.Marca, vehiculo.Modelo, vehiculo.Placa);
                                }
                                else
                                {
                                    Console.WriteLine($"El vehículo con ID {vehiculo.ID} ya existe");
                                }
                                
                            }
                        }
                        else
                        {
                            Console.WriteLine("El archivo no contiene datos válidos.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
                    }
                }


                // Cerrar el diálogo
                filechooser.Destroy();
                Datos.vehiculosLista.Mostrar();

    }

    private void CargarRepuestos(object sender, EventArgs e)
    {
        // ShowMessage("Carga de repuestos iniciada");
        FileChooserDialog filechooser = new FileChooserDialog("Seleccione un archivo",
                    this,
                    FileChooserAction.Open,
                    "Cancelar", ResponseType.Cancel,
                    "Abrir", ResponseType.Accept);
                //mostrar el diálogo
                if (filechooser.Run() == (int)ResponseType.Accept)
                {
                    try
                    {
                        // Mostrar la ruta del archivo seleccionado
                        Console.WriteLine($"Archivo seleccionado: {filechooser.Filename}");

                        // Leer el contenido del archivo
                        string contenido = File.ReadAllText(filechooser.Filename);

                        // Intentar deserializar el JSON
                        // en una lista de objetos de tipo Repuesto
                        List<Repuestoo> Repuestos = JsonSerializer.Deserialize<List<Repuestoo>>(contenido);

                        // Verificar si la lista no está vacía
                        if (Repuestos != null)
                        {
                            Console.WriteLine($"Total de Repuestos cargados: {Repuestos.Count}");
                            // Datos.repuestosArbol.Vaciar();
                            // Imprimir los datos
                            foreach (var repuesto in Repuestos)
                            {   
                                if (!Datos.repuestosArbol.ExisteNodoPorId(repuesto.ID))
                                {
                                    NodoRepuesto nuevo = new NodoRepuesto(repuesto.ID, repuesto.Repuesto, repuesto.Detalles, repuesto.Costo);
                                    Datos.repuestosArbol.Insert(nuevo);
                                }
                                else
                                {
                                    Console.WriteLine($"El repuesto con ID {repuesto.ID} ya existe");
                                }
                                
                            }
                        }
                        else
                        {
                            Console.WriteLine("El archivo no contiene datos válidos.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al procesar el archivo: {ex.Message}");
                    }
                }


                // Cerrar el diálogo
                filechooser.Destroy();
                Datos.repuestosArbol.PreOrden();
                // Datos.repuestosArbol.GenerarGraphviz();
    }

    private void ShowMessage(string message)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.Modal, 
            MessageType.Info, ButtonsType.Ok, message);
        md.Run();
    }

}

public class Persona
{
    public int ID { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Correo { get; set; }
    public int Edad { get; set; }
    public string Contrasenia { get; set; }
}

public class Vehiculo
{
    public int ID { get; set; }
    public int ID_Usuario { get; set; }
    public string Marca { get; set; }
    public int Modelo { get; set; }
    public string Placa { get; set; }
}
public class Repuestoo
{
    public int ID { get; set; }
    public string Repuesto { get; set; }
    public string Detalles { get; set; }
    public double Costo { get; set; }
}