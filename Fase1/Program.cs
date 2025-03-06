using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Estructuras;
using carga;


namespace carga
{


    public static class DataStorage
    {
        public static ListaSimple listaSimple = new ListaSimple();
        public static ListaDoble listaDoble = new ListaDoble();
        public static ListaCircular listaCircular = new ListaCircular();
        public static ListaServicios listaServicios = new ListaServicios();
        public static ListaFacturas listaFacturas = new ListaFacturas();

    }

    public class LoginWindow : Window
    {

        private Entry entryEmail;
        private Entry entryPassword;

        public LoginWindow() : base("Inicio de Sesión")
        {
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { Application.Quit(); };

            VBox vbox = new VBox(false, 5);
            Add(vbox);

            // Campo para el correo electrónico
            Label labelEmail = new Label("Correo electrónico:");
            entryEmail = new Entry();
            vbox.PackStart(labelEmail, false, false, 0);
            vbox.PackStart(entryEmail, false, false, 0);

            // Campo para la contraseña
            Label labelPassword = new Label("Contraseña:");
            entryPassword = new Entry();
            entryPassword.Visibility = false; // Oculta la contraseña
            vbox.PackStart(labelPassword, false, false, 0);
            vbox.PackStart(entryPassword, false, false, 0);

            // Botón de inicio de sesión
            Button btnLogin = new Button("Iniciar Sesión");
            btnLogin.Clicked += OnLoginClicked;
            vbox.PackStart(btnLogin, false, false, 0);

            ShowAll();
        }

        private void OnLoginClicked(object sender, EventArgs a)
        {
            string email = entryEmail.Text;
            string password = entryPassword.Text;

            // Validar credenciales
            if (email == "root@gmail.com" && password == "root123")
            {
                // Credenciales correctas, abrir la ventana principal
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowAll();
                this.Destroy(); // Cerrar la ventana de inicio de sesión
            }
            else
            {
                // Credenciales incorrectas, mostrar mensaje de error
                MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Correo o contraseña incorrectos.");
                md.Run();
                md.Destroy();
            }
        }

        public static void Main()
        {

            Application.Init();
            new LoginWindow();
            Application.Run();
        }
    }

    public class MainWindow : Window
    {

        public MainWindow() : base("Interfaz Sugerida")
        {
            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { Application.Quit(); };

            VBox vbox = new VBox(false, 5);
            Add(vbox);

            Button btnCargasMasivas = new Button("Cargas Masivas");
            Button btnIngresoManual = new Button("Ingreso Manual");
            Button btnGestionUsuarios = new Button("Gestión de Usuarios");
            Button btnGenerarServicio = new Button("Generar Servicio");
            Button btnCancelarFactura = new Button("Cancelar Factura");
            Button btnReportes = new Button("Reportes");
            Button btnSalir = new Button("Salir");

            vbox.PackStart(btnCargasMasivas, true, true, 0);
            vbox.PackStart(btnIngresoManual, true, true, 0);
            vbox.PackStart(btnGestionUsuarios, true, true, 0);
            vbox.PackStart(btnGenerarServicio, true, true, 0);
            vbox.PackStart(btnCancelarFactura, true, true, 0);
            vbox.PackStart(btnReportes, true, true, 0);
            vbox.PackStart(btnSalir, true, true, 0);

            btnCargasMasivas.Clicked += OnCargasMasivasClicked;
            btnIngresoManual.Clicked += OnIngresoManualClicked;
            btnGestionUsuarios.Clicked += OnGestionUsuariosClicked;
            btnGenerarServicio.Clicked += OnGenerarServicioClicked;
            btnCancelarFactura.Clicked += OnCancelarFacturaClicked;
            btnReportes.Clicked += OnReportesClicked;
            btnSalir.Clicked += (sender, e) => { Application.Quit(); };

            ShowAll();
        }

        void OnCargasMasivasClicked(object sender, EventArgs a)
        {
            CargasMasivasWindow cargasMasivasWindow = new CargasMasivasWindow();
            cargasMasivasWindow.ShowAll();
        }

        public class CargasMasivasWindow : Window
        {
            public CargasMasivasWindow() : base("Carga Masiva")
            {
                SetDefaultSize(300, 200);
                SetPosition(WindowPosition.Center);
                DeleteEvent += delegate { this.Hide(); };

                VBox vbox = new VBox(false, 5);
                Add(vbox);

                Button btnVehiculos = new Button("Vehículos");
                Button btnUsuarios = new Button("Usuarios");
                Button btnRepuestos = new Button("Repuestos");
                Button btnSalir = new Button("Salir");

                vbox.PackStart(btnVehiculos, true, true, 0);
                vbox.PackStart(btnUsuarios, true, true, 0);
                vbox.PackStart(btnRepuestos, true, true, 0);
                vbox.PackStart(btnSalir, true, true, 0);

                btnVehiculos.Clicked += OnVehiculosClicked;
                btnUsuarios.Clicked += OnUsuariosClicked;
                btnRepuestos.Clicked += OnRepuestosClicked;
                btnSalir.Clicked += (sender, e) => { this.Destroy(); };
            }

            void OnVehiculosClicked(object sender, EventArgs a)
            {
                Console.WriteLine("Vehículos seleccionado");
                // Crear un nuevo diálogo de archivo
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
                            Console.WriteLine($"Total de vehículos cargados: {vehiculos.Count}");
                            DataStorage.listaDoble.Vaciar();
                            // Imprimir los datos
                            foreach (var vehiculo in vehiculos)
                            {   
                                if (!DataStorage.listaDoble.Buscar(vehiculo.ID))
                                {
                                    DataStorage.listaDoble.InsertarFinal(vehiculo.ID, vehiculo.ID_Usuario, vehiculo.Marca, vehiculo.Modelo, vehiculo.Placa);
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
                // DataStorage.listaDoble.GenerarGraphviz();
                filechooser.Destroy();

                this.Destroy(); // Cierra la ventana
            }


            void OnUsuariosClicked(object sender, EventArgs a)
            {
                Console.WriteLine("Usuarios seleccionado");
                // Crear un nuevo diálogo de archivo
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
                        // en una lista de objetos de tipo Persona
                        List<Persona> personas = JsonSerializer.Deserialize<List<Persona>>(contenido);

                        // Verificar si la lista no está vacía
                        if (personas != null)
                        {
                            Console.WriteLine($"Total de personas cargadas: {personas.Count}");
                            DataStorage.listaSimple.Vaciar();
                            // Imprimir los datos
                            foreach (var persona in personas)
                            {
                                //validar si la pesona ya existe con ID

                                if (!DataStorage.listaSimple.Buscar(persona.ID))
                                {
                                    DataStorage.listaSimple.Insertar(persona.ID, persona.Nombres, persona.Apellidos, persona.Correo, persona.Contrasenia);
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
                // DataStorage.listaSimple.GenerarGraphviz();
                filechooser.Destroy();

                this.Destroy(); // Cierra la ventana
            }

            void OnRepuestosClicked(object sender, EventArgs a)
            {
                Console.WriteLine("Repuestos seleccionado");

                // Crear un nuevo diálogo de archivo
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

                        DataStorage.listaCircular.Vaciar();
                        // Intentar deserializar el JSON
                        // en una lista de objetos de tipo Repuesto
                        List<Respuesto> repuestos = JsonSerializer.Deserialize<List<Respuesto>>(contenido);

                        // Verificar si la lista no está vacía
                        if (repuestos != null)
                        {
                            Console.WriteLine($"Total de repuestos cargados: {repuestos.Count}");

                            // Imprimir los datos
                            foreach (var repuesto in repuestos)
                            {
                                if (!DataStorage.listaCircular.Existe(repuesto.ID))
                                {
                                    DataStorage.listaCircular.Insertar(repuesto.ID, repuesto.Repuesto, repuesto.Detalle, repuesto.Costo);
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
                // DataStorage.listaCircular.Visualizar();
                filechooser.Destroy();

                this.Destroy(); // Cierra la ventana
            }
        }

        void OnIngresoManualClicked(object sender, EventArgs a)
        {
            IngresoManualWindow ingresoManualWindow = new IngresoManualWindow();
            ingresoManualWindow.ShowAll();
        }

        void OnGestionUsuariosClicked(object sender, EventArgs a)
        {
            GestionUsuarioWindow gestionUsuarioWindow = new GestionUsuarioWindow();
            gestionUsuarioWindow.ShowAll();
        }

        void OnGenerarServicioClicked(object sender, EventArgs a)
        {
            IngresoServicioRepuestoWindow ingresoServicioRepuestoWindow = new IngresoServicioRepuestoWindow();
            ingresoServicioRepuestoWindow.ShowAll();
        }

        void OnCancelarFacturaClicked(object sender, EventArgs a)
        {
            Console.WriteLine("Cancelar Factura seleccionado");
        }
        void OnReportesClicked(object sender, EventArgs a)
        {
            ReportesWindow reportesWindow = new ReportesWindow();
            reportesWindow.ShowAll();
        }
    }

    public class IngresoManualWindow : Window
    {
        public IngresoManualWindow() : base("Ingreso Manual")
        {
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { this.Hide(); };

            VBox vbox = new VBox(false, 5);
            Add(vbox);

            Button btnUsuario = new Button("Usuario");
            Button btnVehiculo = new Button("Vehículo");
            Button btnRepuesto = new Button("Repuesto");
            Button btnServicio = new Button("Servicio");
            Button btnSalir = new Button("Salir");

            vbox.PackStart(btnUsuario, true, true, 0);
            vbox.PackStart(btnVehiculo, true, true, 0);
            vbox.PackStart(btnRepuesto, true, true, 0);
            vbox.PackStart(btnServicio, true, true, 0);
            vbox.PackStart(btnSalir, true, true, 0);

            btnUsuario.Clicked += OnUsuarioClicked;
            btnVehiculo.Clicked += OnVehiculoClicked;
            btnRepuesto.Clicked += OnRepuestoClicked;
            btnServicio.Clicked += OnServicioClicked;
            btnSalir.Clicked += (sender, e) => { this.Destroy(); };
        }

        void OnUsuarioClicked(object sender, EventArgs a)
        {
            IngresoUsuarioWindow ingresoUsuarioWindow = new IngresoUsuarioWindow();
            ingresoUsuarioWindow.ShowAll();
        }

        void OnVehiculoClicked(object sender, EventArgs a)
        {
            IngresoVehiculoWindow ingresoVehiculoWindow = new IngresoVehiculoWindow();
            ingresoVehiculoWindow.ShowAll();
        }

        void OnRepuestoClicked(object sender, EventArgs a)
        {
            IngresoRepuestoWindow ingresoRepuestoWindow = new IngresoRepuestoWindow();
            ingresoRepuestoWindow.ShowAll();
        }

        void OnServicioClicked(object sender, EventArgs a)
        {
            IngresoServicioWindow ingresoServicioWindow = new IngresoServicioWindow();
            ingresoServicioWindow.ShowAll();
        }
    }

    public class IngresoUsuarioWindow : Window
    {
        public IngresoUsuarioWindow() : base("Ingreso de Usuario")
        {
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { this.Hide(); };

            VBox vbox = new VBox(false, 5);
            Add(vbox);

            Entry entryId = new Entry();
            Entry entryNombres = new Entry();
            Entry entryApellidos = new Entry();
            Entry entryCorreo = new Entry();
            Entry entryContrasenla = new Entry();
            entryContrasenla.Visibility = false;

            vbox.PackStart(new Label("Id:"), false, false, 0);
            vbox.PackStart(entryId, false, false, 0);
            vbox.PackStart(new Label("Nombres:"), false, false, 0);
            vbox.PackStart(entryNombres, false, false, 0);
            vbox.PackStart(new Label("Apellidos:"), false, false, 0);
            vbox.PackStart(entryApellidos, false, false, 0);
            vbox.PackStart(new Label("Correo:"), false, false, 0);
            vbox.PackStart(entryCorreo, false, false, 0);
            vbox.PackStart(new Label("Contraseña:"), false, false, 0);
            vbox.PackStart(entryContrasenla, false, false, 0);

            Button btnGuardar = new Button("Guardar");
            btnGuardar.Clicked += (sender, e) =>
            {
                Console.WriteLine("Usuario guardado");
                //Mostrar los datos ingresados en la consola

                DataStorage.listaSimple.Insertar(int.Parse(entryId.Text), entryNombres.Text, entryApellidos.Text, entryCorreo.Text, entryContrasenla.Text);
                this.Destroy();
                

                DataStorage.listaSimple.Mostrar();
            };
            vbox.PackStart(btnGuardar, false, false, 0);

            ShowAll();
        }
    }

    public class IngresoVehiculoWindow : Window
    {
        public IngresoVehiculoWindow() : base("Ingreso de Vehículo")
        {
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { this.Hide(); };

            VBox vbox = new VBox(false, 5);
            Add(vbox);

            // Aquí puedes agregar los campos específicos para el ingreso de vehículos
            Entry entryId = new Entry();
            Entry entryIdUsuario = new Entry();
            Entry entryMarca = new Entry();
            Entry entryModelo = new Entry();
            Entry entryPlaca = new Entry();

            vbox.PackStart(new Label("Id:"), false, false, 0);
            vbox.PackStart(entryId, false, false, 0);
            vbox.PackStart(new Label("Id Usuario:"), false, false, 0);
            vbox.PackStart(entryIdUsuario, false, false, 0);
            vbox.PackStart(new Label("Marca:"), false, false, 0);
            vbox.PackStart(entryMarca, false, false, 0);
            vbox.PackStart(new Label("Modelo:"), false, false, 0);
            vbox.PackStart(entryModelo, false, false, 0);
            vbox.PackStart(new Label("Placa:"), false, false, 0);
            vbox.PackStart(entryPlaca, false, false, 0);

            Button btnGuardar = new Button("Guardar");
            btnGuardar.Clicked += (sender, e) =>
            {
                Console.WriteLine("Vehículo guardado");
                DataStorage.listaDoble.InsertarFinal(int.Parse(entryId.Text), int.Parse(entryIdUsuario.Text), entryMarca.Text, int.Parse(entryModelo.Text), entryPlaca.Text);

                DataStorage.listaDoble.Mostrar();
                this.Destroy();
            };
            vbox.PackStart(btnGuardar, false, false, 0);

            ShowAll();
        }
    }

    public class IngresoRepuestoWindow : Window
    {
        public IngresoRepuestoWindow() : base("Ingreso de Repuesto")
        {
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { this.Hide(); };

            VBox vbox = new VBox(false, 5);
            Add(vbox);

            // Aquí puedes agregar los campos específicos para el ingreso de repuestos
            Entry entryID = new Entry();
            Entry entryRepuesto = new Entry();
            Entry entryDetalle = new Entry();
            Entry entryCosto = new Entry();

            vbox.PackStart(new Label("ID:"), false, false, 0);
            vbox.PackStart(entryID, false, false, 0);
            vbox.PackStart(new Label("Repuesto:"), false, false, 0);
            vbox.PackStart(entryRepuesto, false, false, 0);
            vbox.PackStart(new Label("Detalle:"), false, false, 0);
            vbox.PackStart(entryDetalle, false, false, 0);
            vbox.PackStart(new Label("Costo:"), false, false, 0);
            vbox.PackStart(entryCosto, false, false, 0);


            Button btnGuardar = new Button("Guardar");
            btnGuardar.Clicked += (sender, e) =>
            {
                Console.WriteLine("Repuesto guardado");
                DataStorage.listaCircular.Insertar(int.Parse(entryID.Text), entryRepuesto.Text, entryDetalle.Text, double.Parse(entryCosto.Text));
                DataStorage.listaCircular.Mostrar();
                this.Destroy();
            };
            vbox.PackStart(btnGuardar, false, false, 0);

            ShowAll();
        }
    }

    public class IngresoServicioWindow : Window
    {
        public IngresoServicioWindow() : base("Ingreso de Servicio")
        {
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { this.Hide(); };

            VBox vbox = new VBox(false, 5);
            Add(vbox);

            // Aquí puedes agregar los campos específicos para el ingreso de servicios
            Entry entryNombre = new Entry();
            Entry entryDescripcion = new Entry();
            Entry entryPrecio = new Entry();

            vbox.PackStart(new Label("Nombre:"), false, false, 0);
            vbox.PackStart(entryNombre, false, false, 0);
            vbox.PackStart(new Label("Descripción:"), false, false, 0);
            vbox.PackStart(entryDescripcion, false, false, 0);
            vbox.PackStart(new Label("Precio:"), false, false, 0);
            vbox.PackStart(entryPrecio, false, false, 0);

            Button btnGuardar = new Button("Guardar");
            btnGuardar.Clicked += (sender, e) =>
            {
                Console.WriteLine("Servicio guardado");
                this.Destroy();
            };
            vbox.PackStart(btnGuardar, false, false, 0);

            ShowAll();
        }
    }
}

public class GestionUsuarioWindow : Window
{
    public GestionUsuarioWindow() : base("Usuario")
    {
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { this.Hide(); };

        VBox vbox = new VBox(false, 5);
        Add(vbox);

        Button btnVerUs = new Button("Ver");
        Button btnEditarUs = new Button("Editar");
        Button btnEliminarUs = new Button("Eliminar");
        Button btnSalir = new Button("Salir");

        vbox.PackStart(btnVerUs, true, true, 0);
        vbox.PackStart(btnEditarUs, true, true, 0);
        vbox.PackStart(btnEliminarUs, true, true, 0);
        vbox.PackStart(btnSalir, true, true, 0);

        btnVerUs.Clicked += OnVerUsClicked;
        btnEditarUs.Clicked += OnEditarUsClicked;
        btnEliminarUs.Clicked += OnEliminarUsClicked;
        btnSalir.Clicked += (sender, e) => { this.Destroy(); };
    }

    void OnVerUsClicked(object sender, EventArgs a)
    {
        VerUsuarioWindow verUsuarioWindow = new VerUsuarioWindow();
        verUsuarioWindow.ShowAll();
    }

    void OnEditarUsClicked(object sender, EventArgs a)
    {
        EditarUsuarioWindow editarUsuarioWindow = new EditarUsuarioWindow();
        editarUsuarioWindow.ShowAll();
    }

    void OnEliminarUsClicked(object sender, EventArgs a)
    {
        EliminarUsuarioWindow eliminarUsuarioWindow = new EliminarUsuarioWindow();
        eliminarUsuarioWindow.ShowAll();
    }
}
public class VerUsuarioWindow : Window
{
    public VerUsuarioWindow() : base("Visualizar Usuario")
    {
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { this.Hide(); };

        VBox vbox = new VBox(false, 5);
        Add(vbox);

        Entry entryId = new Entry();

        vbox.PackStart(new Label("Id:"), false, false, 0);
        vbox.PackStart(entryId, false, false, 0);
        Label labelNombres = new Label();
        vbox.PackStart(new Label("Nombre:"), false, false, 0);
        vbox.PackStart(labelNombres, false, false, 0);
        Label labelApellidos = new Label();
        vbox.PackStart(new Label("Apellidos:"), false, false, 0);
        vbox.PackStart(labelApellidos, false, false, 0);
        Label labelCorreo = new Label();
        vbox.PackStart(new Label("Correo:"), false, false, 0);
        vbox.PackStart(labelCorreo, false, false, 0);

        Button btnVer = new Button("Ver Datos");
        Button btnSalir = new Button("Salir");
        btnSalir.Clicked += (sender, e) => { this.Destroy(); };
        

        btnVer.Clicked += (sender, e) =>
        {
            unsafe
            {
                var nodo = DataStorage.listaSimple.BuscarNodo(int.Parse(entryId.Text));
                if (nodo != null)
                {
                    labelNombres.Text = new string(nodo->Nombre);
                    labelApellidos.Text = new string(nodo->Apellido);
                    labelCorreo.Text = new string(nodo->Correo);    

                }
                else
                {
                    labelNombres.Text = "No encontrado";
                    labelApellidos.Text = "No encontrado";
                    labelCorreo.Text = "No encontrado";
                }
            }
            
        };
        vbox.PackStart(btnVer, false, false, 0);
        vbox.PackStart(btnSalir, false, false, 0);

        ShowAll();
    }
}

public class EditarUsuarioWindow : Window
{
    public EditarUsuarioWindow() : base("Editar Usuario")
    {
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { this.Hide(); };

        VBox vbox = new VBox(false, 5);
        Add(vbox);

        

        Entry entryId = new Entry();
        Entry entryNombres = new Entry();
        Entry entryApellidos = new Entry();
        Entry entryCorreo = new Entry();
        

        vbox.PackStart(new Label("Id:"), false, false, 0);
        vbox.PackStart(entryId, false, false, 0);
        Label labelNombres = new Label();
        vbox.PackStart(new Label("Nombre:"), false, false, 0);
        vbox.PackStart(labelNombres, false, false, 0);
        vbox.PackStart(new Label("Nuevo nombre:"), false, false, 0);
        vbox.PackStart(entryNombres, false, false, 0);
        Label labelApellidos = new Label();
        vbox.PackStart(new Label("Apellidos:"), false, false, 0);
        vbox.PackStart(labelApellidos, false, false, 0);
        vbox.PackStart(new Label("Nuevo apellido:"), false, false, 0);
        vbox.PackStart(entryApellidos, false, false, 0);
        Label labelCorreo = new Label();
        vbox.PackStart(new Label("Correo:"), false, false, 0);
        vbox.PackStart(labelCorreo, false, false, 0);
        vbox.PackStart(new Label("Nuevo correo:"), false, false, 0);
        vbox.PackStart(entryCorreo, false, false, 0);

        Button btnEditar = new Button("Editar");
        Button btnBuscar = new Button("Buscar");
        Button btnSalir = new Button("Salir");
        btnSalir.Clicked += (sender, e) => { this.Destroy(); };
        

        btnBuscar.Clicked += (sender, e) =>
        {
            unsafe
            {
                var nodo = DataStorage.listaSimple.BuscarNodo(int.Parse(entryId.Text));
                if (nodo != null)
                {
                    labelNombres.Text = new string(nodo->Nombre);
                    labelApellidos.Text = new string(nodo->Apellido);
                    labelCorreo.Text = new string(nodo->Correo);    

                }
                else
                {
                    labelNombres.Text = "No encontrado";
                    labelApellidos.Text = "No encontrado";
                    labelCorreo.Text = "No encontrado";
                }
            }
            
        };
        btnEditar.Clicked += (sender, e) =>
        {
            unsafe
            {
                var nodo = DataStorage.listaSimple.BuscarNodo(int.Parse(entryId.Text));
                if (nodo != null)
                {
                    nodo->LiberarMemoria();
                    nodo->SetNombre(entryNombres.Text);
                    nodo->SetApellido(entryApellidos.Text);
                    nodo->SetCorreo(entryCorreo.Text);
                    DataStorage.listaSimple.Mostrar();
                }
                else
                {
                    Console.WriteLine("No encontrado");
                }
            }
        };
        vbox.PackStart(btnBuscar, false, false, 0);
        vbox.PackStart(btnEditar, false, false, 0);
        vbox.PackStart(btnSalir, false, false, 0);

        ShowAll();
    }
}


public class EliminarUsuarioWindow : Window
{
    public EliminarUsuarioWindow() : base("Eliminar Usuario")
    {
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { this.Hide(); };

        VBox vbox = new VBox(false, 5);
        Add(vbox);

        Entry entryId = new Entry();

        vbox.PackStart(new Label("Id:"), false, false, 0);
        vbox.PackStart(entryId, false, false, 0);
        Label labelEliminado = new Label();
        vbox.PackStart(new Label("Estado:"), false, false, 0);
        vbox.PackStart(labelEliminado, false, false, 0);

        Button btnEliminar = new Button("Eliminar");
        Button btnSalir = new Button("Salir");
        btnSalir.Clicked += (sender, e) => { this.Destroy(); };
        

        btnEliminar.Clicked += (sender, e) =>
        {
            unsafe
            {
                var nodo = DataStorage.listaSimple.BuscarNodo(int.Parse(entryId.Text));
                if (nodo != null)
                {
                    labelEliminado.Text = new string("\nEliminado\n");
                    DataStorage.listaSimple.Eliminar(int.Parse(entryId.Text));
                    DataStorage.listaSimple.Mostrar();

                }
                else
                {
                    labelEliminado.Text = "No encontrado";
                }
            }
            
        };
        vbox.PackStart(btnEliminar, false, false, 0);
        vbox.PackStart(btnSalir, false, false, 0);

        ShowAll();
    }
}
public class ReportesWindow : Window
{
    public ReportesWindow() : base("Visualizar Reportes")
    {
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { this.Hide(); };

        VBox vbox = new VBox(false, 5);
        Add(vbox);


        Button btnUsuarios = new Button("Usuarios");
        Button btnVehiculos = new Button("Vehículos");
        Button btnRepuestos = new Button("Repuestos");
        Button btnServicios = new Button("Servicios");
        Button btnFacturacion = new Button("Facturación");
        Button btnBitacora = new Button("Bitácora");
        Button btnVehiculoServicio = new Button("Vehículo con mas servicios");
        Button btnVehiculosAntiguos = new Button("Vehículos más antiguos");
        Button btnSalir = new Button("Salir");
        btnSalir.Clicked += (sender, e) => { this.Destroy(); };
        

        btnUsuarios.Clicked += (sender, e) =>
        {
            DataStorage.listaSimple.GenerarGraphviz();
        };
        vbox.PackStart(btnUsuarios, false, false, 0);

        btnVehiculos.Clicked += (sender, e) =>
        {
            DataStorage.listaDoble.GenerarGraphviz();
        };
        vbox.PackStart(btnVehiculos, false, false, 0);

        btnRepuestos.Clicked += (sender, e) =>
        {
            DataStorage.listaCircular.Visualizar();
        };
        vbox.PackStart(btnRepuestos, false, false, 0);

        btnServicios.Clicked += (sender, e) =>
        {
            DataStorage.listaServicios.GenerarGraphviz();
        };
        vbox.PackStart(btnServicios, false, false, 0);

        btnFacturacion.Clicked += (sender, e) =>
        {
            DataStorage.listaFacturas.GenerarGraphviz();
        };
        vbox.PackStart(btnFacturacion, false, false, 0);

        btnBitacora.Clicked += (sender, e) =>
        {
            Console.WriteLine("Bitácora seleccionado");
        };
        vbox.PackStart(btnBitacora, false, false, 0);

        btnVehiculoServicio.Clicked += (sender, e) =>
        {
            Console.WriteLine("Vehículo con más servicios seleccionado");
        };
        vbox.PackStart(btnVehiculoServicio, false, false, 0);

        btnVehiculosAntiguos.Clicked += (sender, e) =>
        {
            DataStorage.listaDoble.GenerarGraphvizPrimeros5();
        };
        vbox.PackStart(btnVehiculosAntiguos, false, false, 0);

        vbox.PackStart(btnSalir, false, false, 0);

        ShowAll();
    }
}






public class IngresoServicioRepuestoWindow : Window
    {
        public IngresoServicioRepuestoWindow() : base("Ingreso de Servicio")
        {
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { this.Hide(); };

            VBox vbox = new VBox(false, 5);
            Add(vbox);

            Entry entryId = new Entry();
            Entry entryId_Repuesto = new Entry();
            Entry entryId_Vehiculo = new Entry();
            Entry entryDetalles = new Entry();
            Entry entryCosto = new Entry();

            vbox.PackStart(new Label("Id:"), false, false, 0);
            vbox.PackStart(entryId, false, false, 0);
            vbox.PackStart(new Label("Id Repuesto:"), false, false, 0);
            vbox.PackStart(entryId_Repuesto, false, false, 0);
            vbox.PackStart(new Label("Id Vehiculo:"), false, false, 0);
            vbox.PackStart(entryId_Vehiculo, false, false, 0);
            vbox.PackStart(new Label("Detalles:"), false, false, 0);
            vbox.PackStart(entryDetalles, false, false, 0);
            vbox.PackStart(new Label("Costo:"), false, false, 0);
            vbox.PackStart(entryCosto, false, false, 0);


            Button btnGuardar = new Button("Guardar");
            btnGuardar.Clicked += (sender, e) =>
            {
                Console.WriteLine("Servicio guardado");
                //Buscar id de repuesto y vehiculo
                if(DataStorage.listaCircular.Existe(int.Parse(entryId_Repuesto.Text)) && DataStorage.listaDoble.Buscar(int.Parse(entryId_Vehiculo.Text)))
                {
                    DataStorage.listaServicios.Insertar(int.Parse(entryId.Text), int.Parse(entryId_Repuesto.Text), int.Parse(entryId_Vehiculo.Text), entryDetalles.Text, double.Parse(entryCosto.Text));
                
                }
                else
                {
                    Console.WriteLine("Repuesto o vehículo no encontrado");
                }

                this.Destroy();
                

                // DataStorage listaServicios.Mostrar();
            };
            vbox.PackStart(btnGuardar, false, false, 0);
            FacturaWindow facturaWindow = new FacturaWindow();
            facturaWindow.ShowAll();
            ShowAll();
        }
    }


public class FacturaWindow : Window
    {
        public FacturaWindow() : base("Ingreso de Factura")
        {
            SetDefaultSize(300, 200);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { this.Hide(); };

            VBox vbox = new VBox(false, 5);
            Add(vbox);

            Entry entryId = new Entry();
            Entry entryId_Orden = new Entry();
            Entry entryTotal = new Entry();

            vbox.PackStart(new Label("Id:"), false, false, 0);
            vbox.PackStart(entryId, false, false, 0);
            vbox.PackStart(new Label("Id Orden:"), false, false, 0);
            vbox.PackStart(entryId_Orden, false, false, 0);
            vbox.PackStart(new Label("Total:"), false, false, 0);
            vbox.PackStart(entryTotal, false, false, 0);


            Button btnGuardar = new Button("Guardar");
            btnGuardar.Clicked += (sender, e) =>
            {
                Console.WriteLine("Factura guardado");
                //Buscar id de repuesto y vehiculo
                
                DataStorage.listaFacturas.InsertarInicio(int.Parse(entryId.Text), int.Parse(entryId_Orden.Text), double.Parse(entryTotal.Text));
                this.Destroy();
                
            };
            vbox.PackStart(btnGuardar, false, false, 0);

            ShowAll();
        }
    }


















public class Persona
{
    public int ID { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Correo { get; set; }
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
public class Respuesto
{
    public int ID { get; set; }
    public string Repuesto { get; set; }
    public string Detalle { get; set; }
    public double Costo { get; set; }
}
