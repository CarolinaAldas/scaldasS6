using System.Collections.ObjectModel;
using scaldasS6.Models;
using Newtonsoft.Json;

namespace scaldasS6.Views;

public partial class vEstudiante : ContentPage
{
    private const string URL = "http://192.168.101.6/ws_estudiante/post.php";
    private readonly HttpClient client = new HttpClient();
    private ObservableCollection<Estudiante> _estud;

    public async void get()
    {
        var content = await client.GetStringAsync(URL);
        List<Estudiante> objEstudiante = JsonConvert.DeserializeObject<List<Estudiante>>(content);
        _estud = new ObservableCollection<Estudiante>(objEstudiante);
        listaEstudiante.ItemsSource = _estud;
    }

    public vEstudiante()
    {
        InitializeComponent();
        get();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        get();
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vAgregarEstudiante());
    }

    // metodo
    private async void listaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objEstudiante = (Estudiante)e.SelectedItem;
        await Navigation.PushAsync(new vActElim(objEstudiante));
    }
}