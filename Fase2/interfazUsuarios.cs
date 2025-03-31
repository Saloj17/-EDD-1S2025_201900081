using Gtk;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Estructuras;

public class interfazUsuarios : Window
{
    public interfazUsuarios() : base("Usuarios")
    {
        // Configuración de la ventana
        SetDefaultSize(400, 350);
        SetPosition(WindowPosition.Center);
        BorderWidth = 15;
        DeleteEvent += (sender, e) => Application.Quit();

        // Contenedor principal
        VBox mainBox = new VBox(false, 8);
        Add(mainBox);

        // Título
        Label titleLabel = new Label($"<span font='18' weight='bold'>Bienvenido\n{Datos.usuariosLista.BuscarUsuarioId(Datos.idUsuarioLogin).Nombre}</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Justify = Justification.Center;
        titleLabel.MarginBottom = 15;
        mainBox.PackStart(titleLabel, false, false, 0);

        // Crear botones para cada opción
        string[] opciones = {
            "Insertar Vehículo",
            "Visualización de Servicios",
            "Visualización de Facturas",
            "Cancelar Facturas"
        };

        foreach (string opcion in opciones)
        {
            Button btn = CrearBotonOpcion(opcion);
            mainBox.PackStart(btn, false, false, 5);
        }

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
        btn.Clicked += (sender, e) => {
            Datos.salidaUsuarioLogin = Datos.loginLista.ObtenerFechaActualFormateada(); // Obtener la fecha actual formateada
            Datos.loginLista.AgregarLogin(Datos.nombreUsuarioLogin, Datos.entradaUsuarioLogin, Datos.salidaUsuarioLogin);
            
            this.Destroy(); // Cierra esta ventana
            Application.Quit(); // Cierra la aplicación completamente
        };
        return btn; 
    }

    private void AccionBoton(string opcion)
    {
        // Aquí puedes implementar la lógica para cada botón
        Console.WriteLine($"Acción: {opcion}");
        
        // Ejemplo de cómo manejar diferentes opciones:
        switch(opcion)
        {
            case "Insertar Vehículo":
                Application.Init();
                new insertarVehiculos();
                Application.Run();
                break;
            case "Visualización de Servicios":
                Application.Init();            
                new visualizarServicios();                   
                Application.Run(); 
                break;
            case "Visualización de Facturas":
                Console.WriteLine("Visualización de Facturas seleccionado.");
                // Application.Init();
                // new ActualizarRepuestoWindow();
                // Application.Run();
                break;
            case "Cancelar Facturas":
                Console.WriteLine("Cancelar Facturas seleccionado.");
                // Application.Init();
                // new visualizarRepuestos();
                // Application.Run();
                break;
            // ... otras opciones
        }
    }
}