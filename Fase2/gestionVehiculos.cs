using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Estructuras;

public class VehiculoWindow : Window
{
    // Variables para mantener referencias a los controles
    private Entry idEntry;
    private Label idUsuarioLabel;
    private Label marcaLabel;
    private Label modeloLabel;
    private Label placaLabel;

    public VehiculoWindow() : base("Gestión de Vehículo")
    {
        // Configuración básica de la ventana
        SetDefaultSize(500, 300);
        SetPosition(WindowPosition.Center);
        BorderWidth = 20;
        DeleteEvent += (sender, e) => this.Destroy();

        // Contenedor principal
        VBox mainBox = new VBox(false, 10);
        Add(mainBox);

        // Título
        Label titleLabel = new Label("<span font='18' weight='bold'>Gestión de Vehículo</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Justify = Justification.Center;
        titleLabel.ModifyFg(StateType.Normal, new Gdk.Color(50, 120, 180));
        mainBox.PackStart(titleLabel, false, false, 15);

        // Tabla para organizar los campos del formulario
        Table formTable = new Table(5, 2, false);
        formTable.RowSpacing = 10;
        formTable.ColumnSpacing = 15;

        // Campos del formulario
        CreateEditableField(formTable, 0, "Id", out idEntry);
        CreateReadOnlyField(formTable, 1, "Id Usuario", out idUsuarioLabel);
        CreateReadOnlyField(formTable, 2, "Marca", out marcaLabel);
        CreateReadOnlyField(formTable, 3, "Modelo", out modeloLabel);
        CreateReadOnlyField(formTable, 4, "Placa", out placaLabel);
        
        mainBox.PackStart(formTable, true, true, 0);

        // Contenedor para los botones
        HBox buttonBox = new HBox(true, 20);
        
        // Botón Buscar
        Button buscarButton = new Button("Buscar");
        StyleButton(buscarButton, new Gdk.Color(50, 180, 120));
        buscarButton.Clicked += OnBuscarClicked;
        
        // Botón Eliminar
        Button eliminarButton = new Button("Eliminar");
        StyleButton(eliminarButton, new Gdk.Color(50, 180, 120));
        eliminarButton.Clicked += OnEliminarClicked;

        // Botón Salir
        Button salirButton = new Button("Salir");
        StyleButton(salirButton, new Gdk.Color(50, 180, 120));
        salirButton.Clicked += OnSalirClicked;

        buttonBox.PackStart(buscarButton, true, true, 0);
        buttonBox.PackStart(eliminarButton, true, true, 0);
        buttonBox.PackStart(salirButton, true, true, 0);

        mainBox.PackStart(buttonBox, false, false, 15);

        ShowAll();
    }

    private void CreateEditableField(Table table, uint row, string labelText, out Entry entry)
    {
        // Etiqueta del campo
        Label fieldLabel = new Label(labelText);
        fieldLabel.ModifyFont(Pango.FontDescription.FromString("Arial 12"));
        fieldLabel.ModifyFg(StateType.Normal, new Gdk.Color(50, 120, 180));
        fieldLabel.Xalign = 0;
        table.Attach(fieldLabel, 0, 1, row, row+1, AttachOptions.Fill, AttachOptions.Fill, 0, 0);

        // Campo de entrada editable
        entry = new Entry();
        entry.ModifyFont(Pango.FontDescription.FromString("Arial 12"));
        table.Attach(entry, 1, 2, row, row+1, AttachOptions.Fill | AttachOptions.Expand, AttachOptions.Fill, 0, 0);
    }

    private void CreateReadOnlyField(Table table, uint row, string labelText, out Label label)
    {
        // Etiqueta del campo
        Label fieldLabel = new Label(labelText);
        fieldLabel.ModifyFont(Pango.FontDescription.FromString("Arial 12"));
        fieldLabel.ModifyFg(StateType.Normal, new Gdk.Color(50, 120, 180));
        fieldLabel.Xalign = 0;
        table.Attach(fieldLabel, 0, 1, row, row+1, AttachOptions.Fill, AttachOptions.Fill, 0, 0);

        // Campo de solo lectura
        label = new Label();
        label.ModifyFont(Pango.FontDescription.FromString("Arial 12"));
        label.Xalign = 0;
        label.Selectable = true;
        table.Attach(label, 1, 2, row, row+1, AttachOptions.Fill | AttachOptions.Expand, AttachOptions.Fill, 0, 0);
    }

    private void StyleButton(Button button, Gdk.Color color)
    {
        button.SetSizeRequest(120, 35);
        button.ModifyBg(StateType.Normal, color);
        button.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255));
        button.Relief = ReliefStyle.None;
    }

    private void OnBuscarClicked(object sender, EventArgs e)
    {
        // Ejemplo de cómo cargar datos (aquí deberías implementar tu lógica real)
        int id = 0;
        try {
            id = int.Parse(idEntry.Text);
        }
        catch (FormatException) {
            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, 
                MessageType.Warning, ButtonsType.Ok, "Por favor ingrese un ID válido");
            md.Run();
            md.Destroy();
            return;
        }

        if (Datos.vehiculosLista.ExisteVehiculoId(id))
        {
            // Simulación de búsqueda
            idUsuarioLabel.Text = Datos.vehiculosLista.BuscarVehiculoId(id).Id_Usuario.ToString();
            marcaLabel.Text = Datos.vehiculosLista.BuscarVehiculoId(id).Marca;
            modeloLabel.Text = Datos.vehiculosLista.BuscarVehiculoId(id).Modelo.ToString();
            placaLabel.Text = Datos.vehiculosLista.BuscarVehiculoId(id).Placa;
        }
        else
        {
            idUsuarioLabel.Text = "";
            marcaLabel.Text = "";
            modeloLabel.Text = "";
            placaLabel.Text = "";

            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, 
                MessageType.Warning, ButtonsType.Ok, "El id ingresado no existe");
            md.Run();
            md.Destroy();
        }
    }

    private void OnEliminarClicked(object sender, EventArgs e)
    {
        int id = 0;
        try {
            id = int.Parse(idEntry.Text);
        }
        catch (FormatException) {
            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, 
                MessageType.Warning, ButtonsType.Ok, "Por favor ingrese un ID válido");
            md.Run();
            md.Destroy();
            return;
        }

        if (Datos.vehiculosLista.ExisteVehiculoId(id))
        {
            MessageDialog confirmDialog = new MessageDialog(this, DialogFlags.Modal,
                MessageType.Question, ButtonsType.YesNo, "¿Está seguro de eliminar este vehículo?");
            
            ResponseType response = (ResponseType)confirmDialog.Run();
            confirmDialog.Destroy();
            
            if (response == ResponseType.Yes)
            {
                // Lógica para eliminar el vehículo
                idUsuarioLabel.Text = "";
                marcaLabel.Text = "";
                modeloLabel.Text = "";
                placaLabel.Text = "";

                Datos.vehiculosLista.EliminarVehiculoId(id);
                
                MessageDialog md = new MessageDialog(this, DialogFlags.Modal, 
                    MessageType.Info, ButtonsType.Ok, "Vehículo eliminado correctamente");
                md.Run();
                md.Destroy();
            }
        }
        else
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.Modal, 
                MessageType.Warning, ButtonsType.Ok, "No hay datos para eliminar");
            md.Run();
            md.Destroy();
        }
    }

    private void OnSalirClicked(object sender, EventArgs e)
    {
        this.Destroy();
        Application.Quit();
    }
}