using Gtk;
using System;
using System.Collections.Generic;
using System.Linq;
using Estructuras;
using System.IO;

public class visualizarVehiculos : Window
{
    private ListStore liststore;
    private TreeView treeview;
    private bool isDisposed = false;

    public visualizarVehiculos() : base("Visualización de Servicios")
    {
        try
        {
            // Configuración básica de la ventana
            SetDefaultSize(600, 400);
            BorderWidth = 10;
            DeleteEvent += OnWindowDelete;

            // Contenedor principal
            VBox vbox = new VBox(false, 6);
            Add(vbox);

            // Título
            Label titleLabel = new Label();
            var usuario = Datos.blockchain.BuscarUsuarioId(Datos.idUsuarioLogin);
            titleLabel.Markup = $"<span size='x-large' weight='bold'>Visualización de Vehículos\n{(usuario != null ? usuario.Nombres : "Usuario")}</span>";

            vbox.PackStart(titleLabel, false, false, 0);

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
            viewData();

            ShowAll();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al inicializar la ventana: {ex.Message}");
            Destroy();
        }
    }

    protected override void OnDestroyed()
    {
        if (!isDisposed)
        {
            // Limpiar recursos
            if (treeview != null)
            {
                treeview.Destroy();
                treeview = null;
            }

            if (liststore != null)
            {
                liststore.Dispose();
                liststore = null;
            }

            isDisposed = true;
            base.OnDestroyed();
        }
    }

    private void OnWindowDelete(object sender, DeleteEventArgs args)
    {
        Destroy();
        args.RetVal = true;
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
        // Columnas: Id, Id_Usuario, Marca, Modelo, Placa
        liststore = new ListStore(typeof(int), typeof(int), typeof(string), typeof(int), typeof(string));
        treeview = new TreeView(liststore);
        ConfigureColumns();
    }

    private void ConfigureColumns()
    {
        // Limpiar columnas existentes
        var columns = treeview.Columns.ToArray();
        foreach (var col in columns)
        {
            treeview.RemoveColumn(col);
            col.Dispose();
        }

        // Crear renderizador de texto
        CellRendererText renderer = new CellRendererText();

        // Configurar columnas
        string[] columnas = { "Id", "Id Usuario", "Marca", "Modelo", "Placa" };
        for (int i = 0; i < columnas.Length; i++)
        {
            TreeViewColumn column = new TreeViewColumn(columnas[i], renderer, "text", i);
            column.SortColumnId = i;
            column.Resizable = true;
            treeview.AppendColumn(column);
        }
    }

    private void viewData()
    {
        if (liststore == null || treeview == null) return;

        try
        {
            liststore.Clear();

            var vehiculos = Datos.vehiculosLista.ObtenerArregloNodos()?
                .Where(v => v.Id_Usuario == Datos.idUsuarioLogin)
                .ToList();

            if (vehiculos != null)
            {
                foreach (var item in vehiculos)
                {
                    liststore.AppendValues(
                        item.Id,
                        item.Id_Usuario,
                        item.Marca ?? "N/A",
                        item.Modelo,
                        item.Placa ?? "N/A"
                    );
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar datos: {ex.Message}");
        }
    }

    private void OnExitClicked(object sender, EventArgs e)
    {
        try
        {
            if (sender is Button button)
            {
                button.Clicked -= OnExitClicked;
            }

            Destroy();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al salir: {ex.Message}");
        }
    }
}
