using Gtk;
using System;

public class LoginWindow : Window
{
    public LoginWindow() : base("Inicio de Sesión")
    {
        // Configuración básica de la ventana
        SetDefaultSize(350, 200);
        SetPosition(WindowPosition.Center);
        BorderWidth = 20;
        DeleteEvent += (sender, e) => Application.Quit();

        // Contenedor principal (VBox)
        VBox mainBox = new VBox(false, 15);
        Add(mainBox);

        // Título centrado (como en la imagen)
        Label titleLabel = new Label("<span font='16' weight='bold'>Administrador</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Justify = Justification.Center;
        mainBox.PackStart(titleLabel, false, false, 0);

        // Contenedor para los campos (con márgenes)
        VBox fieldsBox = new VBox(false, 10);
        fieldsBox.BorderWidth = 10;

        // Campo Correo
        Label emailLabel = new Label("Correo");
        emailLabel.Halign = Align.Start;
        emailLabel.ModifyFont(Pango.FontDescription.FromString("Arial 12"));
        Entry emailEntry = new Entry();
        emailEntry.WidthRequest = 250;

        // Campo Contraseña
        Label passwordLabel = new Label("Contraseña");
        passwordLabel.Halign = Align.Start;
        passwordLabel.ModifyFont(Pango.FontDescription.FromString("Arial 12"));
        Entry passwordEntry = new Entry();
        passwordEntry.Visibility = false;
        passwordEntry.WidthRequest = 250;

        // Agregar campos al contenedor
        fieldsBox.PackStart(emailLabel, false, false, 0);
        fieldsBox.PackStart(emailEntry, false, false, 0);
        fieldsBox.PackStart(passwordLabel, false, false, 0);
        fieldsBox.PackStart(passwordEntry, false, false, 0);

        mainBox.PackStart(fieldsBox, true, true, 0);

        // Botón Validar (verde)
        Button validateButton = new Button("Validar");
        validateButton.ModifyBg(StateType.Normal, new Gdk.Color(0, 150, 0)); // Verde
        validateButton.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255)); // Texto blanco
        validateButton.SetSizeRequest(100, 35);
        validateButton.Relief = ReliefStyle.None; // Estilo plano

        // Alinear el botón a la derecha
        Alignment buttonAlign = new Alignment(1f, 0f, 0f, 0f);
        buttonAlign.Add(validateButton);
        mainBox.PackStart(buttonAlign, false, false, 0);

        // Evento del botón
        validateButton.Clicked += (sender, e) =>
        {
            if (emailEntry.Text == "admin@usac.com" && passwordEntry.Text == "admin123")
            {
                Application.Init();                  // Inicializa GTK
                new interfazAdmin();                   // Crea la ventana
                Application.Run();                  // Bucle principal   
                
                this.Destroy();
                Application.Quit();  
            }
            else
            {
                MessageDialog md = new MessageDialog(this, DialogFlags.Modal,
                    MessageType.Error, ButtonsType.Close, "Credenciales incorrectas");
                md.Run();
                md.Destroy();
            }
        };

        ShowAll();
    }
}