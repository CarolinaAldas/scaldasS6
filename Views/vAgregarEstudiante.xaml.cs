using scaldasS6.Models;

namespace scaldasS6.Views;

public partial class vAgregarEstudiante : ContentPage
{
    private const string URL = "http://192.168.101.6/ws_estudiante/post.php";

    public vAgregarEstudiante()
    {
        InitializeComponent();
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        try
        {
            using var cliente = new HttpClient();
            var parametros = new System.Collections.Generic.Dictionary<string, string>
            {
                { "nombre",   txtNombre.Text },
                { "apellido", txtApellido.Text },
                { "edad",     txtEdad.Text }
            };

            var content = new FormUrlEncodedContent(parametros);
            var response = await cliente.PostAsync(URL, content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Estudiante agregado correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo agregar el estudiante", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}