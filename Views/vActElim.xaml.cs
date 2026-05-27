using scaldasS6.Models;

namespace scaldasS6.Views;

public partial class vActElim : ContentPage
{
    private const string URL = "http://192.168.101.6/ws_estudiante/post.php";
    private Estudiante _estudiante;

    public vActElim(Estudiante estudiante)
    {
        InitializeComponent();

        _estudiante = estudiante;

        // Precarga los datos
        txtCodigo.Text = estudiante.codigo.ToString();
        txtNombre.Text = estudiante.nombre;
        txtApellido.Text = estudiante.apellido;
        txtEdad.Text = estudiante.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            using var cliente = new HttpClient();
            string url = $"{URL}?codigo={txtCodigo.Text}" +
                         $"&nombre={txtNombre.Text}" +
                         $"&apellido={txtApellido.Text}" +
                         $"&edad={txtEdad.Text}";

            var request = new HttpRequestMessage(HttpMethod.Put, url);
            var response = await cliente.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Estudiante actualizado correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        bool confirmar = await DisplayAlert("Confirmar",
            $"¿Eliminar a {_estudiante.nombre} {_estudiante.apellido}?", "Sí", "No");

        if (!confirmar) return;

        try
        {
            using var cliente = new HttpClient();
            string url = $"{URL}?codigo={txtCodigo.Text}";

            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            var response = await cliente.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Estudiante eliminado correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo eliminar", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}