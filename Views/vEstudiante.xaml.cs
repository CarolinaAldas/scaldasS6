using System.Collections.ObjectModel;
using scaldasS6.Models;
namespace scaldasS6.Views;

using Newtonsoft.Json;

public partial class vEstudiante : ContentPage
{

    private const string URL = "http://192.168.21.175/ws_estudiante/post.php";
    private readonly HttpClient client = new HttpClient();
	private ObservableCollection<Estudiante> _estud;


	public async void get()
	{
		var content = await client.GetStringAsync(URL);
		List<Estudiante> objEstudiante = JsonConvert.DeserializeObject<List<Estudiante>>(content);
		_estud= new ObservableCollection<Estudiante>(objEstudiante);
		listaEstudiante.ItemsSource=_estud;


    }


    public vEstudiante()
	{
		InitializeComponent();
		get();
	}
}