using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Estructuras;

public class visualizarRepuestos : Window
{
    private ListStore liststore;
    private TreeView treeview;
    private ComboBoxText orderCombo;

    public visualizarRepuestos() : base("Visualización de Repuestos")
    {
        // Configuración básica de la ventana
        SetDefaultSize(600, 400);
        BorderWidth = 10;
        
        // Contenedor principal
        VBox vbox = new VBox(false, 6);
        Add(vbox);
        
        // Título
        Label titleLabel = new Label();
        titleLabel.Markup = "<span size='x-large' weight='bold'>Visualización de Repuestos</span>";
        
        vbox.PackStart(titleLabel, false, false, 0);
        
        // Combo box para selección de orden
        orderCombo = new ComboBoxText();
        orderCombo.AppendText("Post-orden");
        orderCombo.AppendText("Pre-orden");
        orderCombo.AppendText("In-orden");
        orderCombo.Active = 0;  // Selecciona Post-orden por defecto
        orderCombo.Changed += OnOrderChanged;
        vbox.PackStart(orderCombo, false, false, 5);

        
        // Crear el TreeView
        CreateTreeView();
        
        // Scroll para el TreeView
        ScrolledWindow scrolledWindow = new ScrolledWindow();
        scrolledWindow.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
        scrolledWindow.Add(treeview);
        vbox.PackStart(scrolledWindow, true, true, 0);
        
        // Botón Salir (verde)
        Button exitButton = new Button("Salir");
        exitButton.Clicked += OnExitClicked;
        StyleButton(exitButton, new Gdk.Color(50, 180, 120));
        vbox.PackStart(exitButton, false, false, 5);
        
        // Cargar datos iniciales
        LoadPostOrderData();

        ShowAll();
    }
    private void StyleButton(Button button, Gdk.Color color)
    {
        button.SetSizeRequest(120, 35);
        button.ModifyBg(StateType.Normal, color);
        button.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255));
        button.Relief = ReliefStyle.None;
    }


    private void CreateTreeView()
    {
        liststore = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string));
        
        // Crear el TreeView
        treeview = new TreeView(liststore);
        
        // Configurar columnas
        ConfigureColumns();
    }

    private void ConfigureColumns()
    {
        // Limpiar columnas existentes
        foreach (TreeViewColumn col in treeview.Columns)
        {
            treeview.RemoveColumn(col);
        }
        
        // Crear renderizador de texto
        CellRendererText renderer = new CellRendererText();
        
        // Columna ID
        TreeViewColumn columnId = new TreeViewColumn("ID", renderer, "text", 0);
        columnId.SortColumnId = 0;
        treeview.AppendColumn(columnId);
        
        // Columna Repuesto
        TreeViewColumn columnRepuesto = new TreeViewColumn("Repuesto", renderer, "text", 1);
        columnRepuesto.SortColumnId = 1;
        treeview.AppendColumn(columnRepuesto);
        
        // Columna Detalles
        TreeViewColumn columnDetalles = new TreeViewColumn("Detalles", renderer, "text", 2);
        columnDetalles.SortColumnId = 2;
        treeview.AppendColumn(columnDetalles);

        
        
        // Columna Costo
        TreeViewColumn columnCosto = new TreeViewColumn("Costo", renderer, "text", 3);
        columnCosto.SortColumnId = 3;
        treeview.AppendColumn(columnCosto);
    }

    private void OnOrderChanged(object sender, EventArgs e)
    {
        // Limpiar datos existentes
        liststore.Clear();
        
        // Cargar datos según la opción seleccionada
        switch (orderCombo.Active)
        {
            case 0: // Post-orden
                LoadPostOrderData();
                break;
            case 1: // Pre-orden
                LoadPreOrderData();
                break;
            case 2: // In-orden
                LoadInOrderData();
                break;
        }
    }

    private void LoadPostOrderData()
    {
        Datos.repuestosArbol.PostOrdenList();
        Console.WriteLine("PostOrden:");
        foreach (var item in Datos.repuestosArbol.PostOrdenList())
        {
            Console.WriteLine(item.Id);
            liststore.AppendValues(item.Id, item.Repuesto, item.Detalle, item.Costo.ToString("N2"));
        }
    }

    private void LoadPreOrderData()
    {
        Datos.repuestosArbol.PreOrdenList();
        Console.WriteLine("PreOrden:");
        foreach (var item in Datos.repuestosArbol.PreOrdenList())
        {
            Console.WriteLine(item.Id);
            liststore.AppendValues(item.Id, item.Repuesto, item.Detalle, item.Costo.ToString("N2"));
        }
    }

    private void LoadInOrderData()
    {
        Datos.repuestosArbol.InOrdenList();
        Console.WriteLine("InOrden:");
        foreach (var item in Datos.repuestosArbol.InOrdenList())
        {
            Console.WriteLine(item.Id);
            liststore.AppendValues(item.Id, item.Repuesto, item.Detalle, item.Costo.ToString("N2"));
        }
    }

    private void OnExitClicked(object sender, EventArgs e)
    {
        Destroy();
        Application.Quit();
    }
}
