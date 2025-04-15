using System;
using System.IO;
using Gtk;
using Newtonsoft.Json.Linq; // Para formatear JSON

public class visualizarLogin : Window
{
    public visualizarLogin(string jsonFilePath) : base("Visualizador de JSON")
    {
        // Configuración de la ventana
        SetDefaultSize(600, 400);
        DeleteEvent += (o, args) => Application.Quit();

        // Contenedor principal (vertical)
        VBox mainBox = new VBox();
        Add(mainBox);

        Label titleLabel = new Label("<span font='18' weight='bold'>Visualización de Logueo </span>");
        titleLabel.UseMarkup = true;
        titleLabel.Justify = Justification.Center;
        titleLabel.MarginBottom = 15;
        mainBox.PackStart(titleLabel, false, false, 0);

        // TextView para mostrar el JSON (con scroll)
        TextView textView = new TextView();
        textView.WrapMode = WrapMode.Word;
        ScrolledWindow scroll = new ScrolledWindow();
        scroll.Add(textView);
        mainBox.PackStart(scroll, true, true, 0);

        // Botón Salir
        Button btnSalir = CrearBotonSalir();
        mainBox.PackEnd(btnSalir, false, false, 10);


        // Cargar el archivo al iniciar
        LoadJson(jsonFilePath, textView);

        // Mostrar todo
        ShowAll();
    }
    private Button CrearBotonSalir()
    {
        Button btn = new Button("Salir");
        btn.SetSizeRequest(100, 35);
        btn.ModifyBg(StateType.Normal, new Gdk.Color(50, 180, 120)); // Verde
        btn.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255)); // Texto blanco
        btn.Relief = ReliefStyle.None;
        btn.Clicked += (sender, e) =>
        {
            this.Destroy(); // Cierra esta ventana
            Application.Quit(); // Cierra la aplicación completamente
        };
        return btn;
    }

    private void LoadJson(string filePath, TextView textView)
    {
        try
        {
            string jsonContent = File.ReadAllText(filePath);
            string formattedJson = JToken.Parse(jsonContent).ToString();
            textView.Buffer.Text = formattedJson;
        }
        catch (Exception ex)
        {
            textView.Buffer.Text = $"Error: {ex.Message}";
        }
    }
}