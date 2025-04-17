using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Estructuras;

public class insertarUsuarios : Window
{
    // Variables para los controles
    private Entry idEntry;
    private Entry nombresEntry;
    private Entry apellidosEntry;
    private Entry correoEntry;
    private Entry edadEntry;
    private Entry contraseniaEntry;

    public insertarUsuarios() : base("usuario")
    {
        // Configuración básica de la ventana
        SetDefaultSize(600, 350); // Tamaño ajustado
        SetPosition(WindowPosition.Center);
        BorderWidth = 20;
        DeleteEvent += (sender, e) => this.Destroy();

        // Contenedor principal
        VBox mainBox = new VBox(false, 10);
        Add(mainBox);

        // Título
        Label titleLabel = new Label("<span font='18' weight='bold'>Insertar Usuario</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Justify = Justification.Center;
        titleLabel.ModifyFg(StateType.Normal, new Gdk.Color(50, 120, 180));
        mainBox.PackStart(titleLabel, false, false, 15);

        // Tabla para organizar los campos (4 filas, 3 columnas)
        Table formTable = new Table(6, 2, false);
        formTable.RowSpacing = 10;
        formTable.ColumnSpacing = 15;

        // Campos del formulario
        // Fila 0: ID (solo campo editable)
        CreateLabel(formTable, 0, 0, "Id");
        CreateEntry(formTable, 0, 1, out idEntry);
        
        // Fila 1: nombres (Label + Entry)
        CreateLabel(formTable, 1, 0, "Nombres");
        CreateEntry(formTable, 1, 1, out nombresEntry);
        
        // Fila 2: apellidos (Label + Entry)
        CreateLabel(formTable, 2, 0, "Apellidos");
        CreateEntry(formTable, 2, 1, out apellidosEntry);

        // Fila 3: correo (Label + Entry)
        CreateLabel(formTable, 3, 0, "Correo");
        CreateEntry(formTable, 3, 1, out correoEntry);

        // Fila 4: edad (Label + Entry)
        CreateLabel(formTable, 4, 0, "Edad");
        CreateEntry(formTable, 4, 1, out edadEntry);

        // Fila 5: contraseña (Label + Entry)
        CreateLabel(formTable, 5, 0, "Contraseña");
        CreateEntry(formTable, 5, 1, out contraseniaEntry);


        mainBox.PackStart(formTable, true, true, 0);

        // Contenedor para los botones
        HBox buttonBox = new HBox(true, 20);
        
        // Botón Guardar
        Button guardarButton = new Button("Guardar");
        StyleButton(guardarButton, new Gdk.Color(50, 180, 120));
        guardarButton.Clicked += OnGuardarClicked;
        

        // Botón Salir
        Button salirButton = new Button("Salir");
        StyleButton(salirButton, new Gdk.Color(50, 180, 120));
        salirButton.Clicked += OnSalirClicked;

        buttonBox.PackStart(guardarButton, true, true, 0);
        buttonBox.PackStart(salirButton, true, true, 0);

        mainBox.PackStart(buttonBox, false, false, 15);

        ShowAll();
    }

    private void CreateLabel(Table table, uint row, uint col, string text)
    {
        Label label = new Label(text);
        label.ModifyFont(Pango.FontDescription.FromString("Arial 12"));
        label.ModifyFg(StateType.Normal, new Gdk.Color(50, 120, 180));
        label.Xalign = 0;
        table.Attach(label, col, col+1, row, row+1, AttachOptions.Fill, AttachOptions.Fill, 0, 0);
    }


    private void CreateEntry(Table table, uint row, uint col, out Entry entry, string defaultValue = "")
    {
        entry = new Entry(defaultValue);
        entry.ModifyFont(Pango.FontDescription.FromString("Arial 12"));
        table.Attach(entry, col, col+1, row, row+1, AttachOptions.Fill | AttachOptions.Expand, AttachOptions.Fill, 0, 0);
    }

    private void StyleButton(Button button, Gdk.Color color)
    {
        button.SetSizeRequest(120, 35);
        button.ModifyBg(StateType.Normal, color);
        button.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255));
        button.Relief = ReliefStyle.None;
    }

    private void OnGuardarClicked(object sender, EventArgs e)
    {   
        // Validar campos
        if (string.IsNullOrWhiteSpace(idEntry.Text) || string.IsNullOrWhiteSpace(nombresEntry.Text) || string.IsNullOrWhiteSpace(apellidosEntry.Text) || string.IsNullOrWhiteSpace(correoEntry.Text) || string.IsNullOrWhiteSpace(edadEntry.Text) || string.IsNullOrWhiteSpace(contraseniaEntry.Text))
        {
            Datos.msg(this,"Por favor, complete todos los campos.");
            return;
        }
        if(!Datos.blockchain.ExisteUsuarioId(int.Parse(idEntry.Text))){
            
            Usuario newUser = new Usuario();
            
            newUser.ID = int.Parse(idEntry.Text);
            newUser.Nombres = nombresEntry.Text;
            newUser.Apellidos = apellidosEntry.Text;
            newUser.Correo = correoEntry.Text;
            newUser.Edad = int.Parse(edadEntry.Text);
            newUser.Contrasenia = contraseniaEntry.Text;

            newUser.Contrasenia = Datos.blockchain.EncriptacionSHa256(newUser.Contrasenia);  
            Datos.blockchain.AgregarUsuario(newUser);

            // Vaciamos los campos
            idEntry.Text = "";
            nombresEntry.Text = "";
            apellidosEntry.Text = "";
            correoEntry.Text = "";
            edadEntry.Text = "";
            contraseniaEntry.Text = "";
            Datos.msg(this,"Usuario creado exitosamente");
        }
        else{
            Datos.msg(this,$"El usuario con id: {idEntry.Text} ya existe");
            idEntry.Text = "";
            return;
        }
    }

    
    private void OnSalirClicked(object sender, EventArgs e)
    {
        this.Destroy();
        Application.Quit();
    }

}