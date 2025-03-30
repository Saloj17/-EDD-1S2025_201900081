using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Estructuras;

public class insertarVehiculos : Window
{
    // Variables para los controles
    private Entry idEntry;
    private Entry marcaEntry;
    private Entry modeloEntry;
    private Entry placaEntry;

    public insertarVehiculos() : base("vehículos")
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
        Label titleLabel = new Label("<span font='18' weight='bold'>Insertar Vehículo</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Justify = Justification.Center;
        titleLabel.ModifyFg(StateType.Normal, new Gdk.Color(50, 120, 180));
        mainBox.PackStart(titleLabel, false, false, 15);

        // Tabla para organizar los campos (4 filas, 3 columnas)
        Table formTable = new Table(4, 2, false);
        formTable.RowSpacing = 10;
        formTable.ColumnSpacing = 15;

        // Campos del formulario
        // Fila 0: ID (solo campo editable)
        CreateLabel(formTable, 0, 0, "Id");
        CreateEntry(formTable, 0, 1, out idEntry);
        
        // Fila 1: marca (Label + Entry)
        CreateLabel(formTable, 1, 0, "Marca");
        CreateEntry(formTable, 1, 1, out marcaEntry);
        
        // Fila 2: modelo (Label + Entry)
        CreateLabel(formTable, 2, 0, "Modelo");
        CreateEntry(formTable, 2, 1, out modeloEntry);

        // Fila 3: placa (Label + Entry)
        CreateLabel(formTable, 3, 0, "Placa");
        CreateEntry(formTable, 3, 1, out placaEntry);


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
        if (string.IsNullOrWhiteSpace(idEntry.Text) || string.IsNullOrWhiteSpace(marcaEntry.Text) || string.IsNullOrWhiteSpace(modeloEntry.Text) || string.IsNullOrWhiteSpace(placaEntry.Text))
        {
            ShowMessage("Por favor, complete todos los campos.");
            return;
        }
        if(!Datos.vehiculosLista.ExisteVehiculoId(int.Parse(idEntry.Text))){
            Datos.vehiculosLista.AgregarVehiculoFinal(int.Parse(idEntry.Text),Datos.idUsuarioLogin, marcaEntry.Text, int.Parse(modeloEntry.Text), placaEntry.Text);
            Datos.vehiculosLista.Mostrar();
            // vaciamos los campos
            idEntry.Text = "";
            marcaEntry.Text = "";
            modeloEntry.Text = "";
            placaEntry.Text = "";
            ShowMessage("Vehículo creado exitosamente");
        }
        else{
            ShowMessage($"El vehículo con id: {idEntry.Text} \nYa existe");
            return;
        }
    }

    
    private void OnSalirClicked(object sender, EventArgs e)
    {
        this.Destroy();
        Application.Quit();
    }

    private void ShowMessage(string message)
    {
        using (var md = new MessageDialog(this, DialogFlags.Modal, 
              MessageType.Info, ButtonsType.Ok, message))
        {
            md.Run();
        }
    }
}