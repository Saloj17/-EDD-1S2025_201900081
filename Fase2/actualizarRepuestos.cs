using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Estructuras;

public class ActualizarRepuestoWindow : Window
{
    // Variables para los controles
    private Entry idEntry;
    private Label repuestoLabel;
    private Label detallesLabel;
    private Label costoLabel;
    private Entry repuestoEntry;
    private Entry detallesEntry;
    private Entry costoEntry;

    public ActualizarRepuestoWindow() : base("actualización")
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
        Label titleLabel = new Label("<span font='18' weight='bold'>Actualización de Repuestos</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Justify = Justification.Center;
        titleLabel.ModifyFg(StateType.Normal, new Gdk.Color(50, 120, 180));
        mainBox.PackStart(titleLabel, false, false, 15);

        // Tabla para organizar los campos (4 filas, 3 columnas)
        Table formTable = new Table(4, 3, false);
        formTable.RowSpacing = 10;
        formTable.ColumnSpacing = 15;

        // Campos del formulario
        // Fila 0: ID (solo campo editable)
        CreateLabel(formTable, 0, 0, "Id");
        CreateEntry(formTable, 0, 1, out idEntry);
        
        // Fila 1: Repuesto (Label + Entry)
        CreateLabel(formTable, 1, 0, "Repuesto");
        CreateLabelField(formTable, 1, 1, out repuestoLabel);
        CreateEntry(formTable, 1, 2, out repuestoEntry);
        
        // Fila 2: Detalles (Label + Entry)
        CreateLabel(formTable, 2, 0, "Detalles");
        CreateLabelField(formTable, 2, 1, out detallesLabel);
        CreateEntry(formTable, 2, 2, out detallesEntry);
        
        // Fila 3: Costo (Label + Entry)
        CreateLabel(formTable, 3, 0, "Costo");
        CreateLabelField(formTable, 3, 1, out costoLabel);
        CreateEntry(formTable, 3, 2, out costoEntry);

        mainBox.PackStart(formTable, true, true, 0);

        // Contenedor para los botones
        HBox buttonBox = new HBox(true, 20);
        
        // Botón Buscar
        Button buscarButton = new Button("Buscar");
        StyleButton(buscarButton, new Gdk.Color(50, 180, 120));
        buscarButton.Clicked += OnBuscarClicked;
        
        // Botón Actualizar
        Button actualizarButton = new Button("Actualizar");
        StyleButton(actualizarButton, new Gdk.Color(50, 180, 120));
        actualizarButton.Clicked += OnActualizarClicked;

        // Botón Salir
        Button salirButton = new Button("Salir");
        StyleButton(salirButton, new Gdk.Color(50, 180, 120));
        salirButton.Clicked += OnSalirClicked;

        buttonBox.PackStart(buscarButton, true, true, 0);
        buttonBox.PackStart(actualizarButton, true, true, 0);
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

    private void CreateLabelField(Table table, uint row, uint col, out Label label)
    {
        label = new Label();
        label.ModifyFont(Pango.FontDescription.FromString("Arial 12"));
        label.Xalign = 0;
        label.Selectable = true;
        table.Attach(label, col, col+1, row, row+1, AttachOptions.Fill | AttachOptions.Expand, AttachOptions.Fill, 0, 0);
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

    private void OnBuscarClicked(object sender, EventArgs e)
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
        if (Datos.repuestosArbol.ExisteNodoPorId(id))
        {
            NodoRepuesto repuesto = Datos.repuestosArbol.BuscarPorId(id);
            repuestoLabel.Text = repuesto.Repuesto;
            detallesLabel.Text = repuesto.Detalle;
            costoLabel.Text = repuesto.Costo.ToString("C");
        }
        else
        {
            repuestoLabel.Text = "";
            detallesLabel.Text = "";
            costoLabel.Text = "";
            ShowMessage("No se encontró un repuesto con el ID proporcionado");
        }
    }

    private void OnActualizarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(repuestoEntry.Text) || 
            string.IsNullOrEmpty(detallesEntry.Text) || 
            string.IsNullOrEmpty(costoEntry.Text))
        {
            ShowMessage("Todos los campos de repuesto son requeridos");
            return;
        }

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

        if(Datos.repuestosArbol.ExisteNodoPorId(id))
        {
            Datos.repuestosArbol.ModificarRepuesto(id, repuestoEntry.Text, detallesEntry.Text, double.Parse(costoEntry.Text));
            ShowMessage("Repuesto actualizado correctamente");

            // Limpiar campos
            repuestoEntry.Text = "";
            detallesEntry.Text = "";
            costoEntry.Text = "";
        }
        else
        {
            ShowMessage("No se encontró un repuesto con el ID proporcionado");
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