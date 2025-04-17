using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Estructuras;

public class generarServicio : Window
{
    // Variables para los controles
    private Entry idEntry;
    private Entry idRepuestoEntry;
    private Entry idVehiculoEntry;
    private Entry detallesEntry;
    private Entry costoEntry;

    public generarServicio() : base("servicio")
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
        Label titleLabel = new Label("<span font='18' weight='bold'>Crear Servicio</span>");
        titleLabel.UseMarkup = true;
        titleLabel.Justify = Justification.Center;
        titleLabel.ModifyFg(StateType.Normal, new Gdk.Color(50, 120, 180));
        mainBox.PackStart(titleLabel, false, false, 15);

        // Tabla para organizar los campos (4 filas, 3 columnas)
        Table formTable = new Table(5, 2, false);
        formTable.RowSpacing = 10;
        formTable.ColumnSpacing = 15;

        // Campos del formulario
        // Fila 0: ID (solo campo editable)
        CreateLabel(formTable, 0, 0, "Id");
        CreateEntry(formTable, 0, 1, out idEntry);
        
        // Fila 1: id Repuesto (Label + Entry)
        CreateLabel(formTable, 1, 0, "Id Repuesto");
        CreateEntry(formTable, 1, 1, out idRepuestoEntry);
        
        // Fila 2: id vehiculos (Label + Entry)
        CreateLabel(formTable, 2, 0, "Id Vehiculo");
        CreateEntry(formTable, 2, 1, out idVehiculoEntry);

        // Fila 3: Detalles (Label + Entry)
        CreateLabel(formTable, 3, 0, "Detalles");
        CreateEntry(formTable, 3, 1, out detallesEntry);

        
        // Fila 4: Costo (Label + Entry)
        CreateLabel(formTable, 4, 0, "Costo");
        CreateEntry(formTable, 4, 1, out costoEntry);

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
        if (string.IsNullOrWhiteSpace(idEntry.Text) || string.IsNullOrWhiteSpace(idRepuestoEntry.Text) || string.IsNullOrWhiteSpace(idVehiculoEntry.Text) || string.IsNullOrWhiteSpace(detallesEntry.Text) || string.IsNullOrWhiteSpace(costoEntry.Text))
        {
            Datos.msg(this,"Por favor complete todos los campos");
            return;
        }
        if(Datos.vehiculosLista.ExisteVehiculoId(int.Parse(idVehiculoEntry.Text))){
            if(Datos.repuestosArbol.ExisteNodoPorId(int.Parse(idRepuestoEntry.Text))){
                if (!Datos.serviciosArbol.Existe(int.Parse(idEntry.Text))){
                    NodoServicio servicio = new NodoServicio(int.Parse(idEntry.Text),int.Parse(idRepuestoEntry.Text), int.Parse(idVehiculoEntry.Text), detallesEntry.Text, double.Parse(costoEntry.Text));
                    Datos.serviciosArbol.Insertar(servicio);

                    Datos.grafoLista.Insertar(int.Parse(idVehiculoEntry.Text), int.Parse(idRepuestoEntry.Text));

                    // // generar un id unico para la factura
                    // Random random = new Random();
                    // int idFactura = random.Next(1, 9999);
                    // if(!Datos.facturasArbol.BuscarFacturaBool(idFactura)){
                    //     Datos.facturasArbol.Insertar(idFactura, int.Parse(idEntry.Text), double.Parse(costoEntry.Text));
                    //     ShowMessage("Factura generada con exito, el id de la factura es: " + idFactura);
                    // }
                    // else{
                    //     ShowMessage("El id de la factura ya existe, por favor vuelva a intentarlo");
                    //     return;
                    // }

                    // vaciamos los campos
                    idEntry.Text = "";
                    idRepuestoEntry.Text = "";
                    idVehiculoEntry.Text = "";
                    detallesEntry.Text = "";
                    costoEntry.Text = "";
                    Datos.msg(this,"Servicio creado exitosamente");
                }
                else{
                    idEntry.Text = "";
                    Datos.msg(this,"El servicio ya existe");
                    return;
                }
                
            }
            else{
                idRepuestoEntry.Text = "";
                Datos.msg(this,"El repuesto no existe");
                return;
            }
        }
        else{
            idVehiculoEntry.Text = "";
            Datos.msg(this,"El vehiculo no existe");
            return;
        }
    }

    
    private void OnSalirClicked(object sender, EventArgs e)
    {
        this.Destroy();
        Application.Quit();
    }
}