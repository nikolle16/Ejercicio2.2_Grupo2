using Org.Apache.Http.Authentication;
using Syncfusion.Maui.SignaturePad;

namespace Ejercicio2._2_Grupo2.Views;

public partial class PageInit : ContentPage
{

    Controllers.FirmasController controller;

    public PageInit()
    {
        controller = new Controllers.FirmasController();
        InitializeComponent();
        SfSignaturePad signaturePad = new SfSignaturePad();
    }
    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {

        ImageSource? source = signaturePad.ToImageSource();
        string base64String = await ImageSourceToBase64(source);

        string Nombres = txtNombres.Text;

        if (string.IsNullOrEmpty(Nombres))
        {
            await DisplayAlert("Error", "Porfavor ingrese el nombre", "OK");
            return;
        }

        string Descripcion = txtDescrip.Text;

        if (string.IsNullOrEmpty(Descripcion))
        {
            await DisplayAlert("Error", "Porfavor ingrese una breve descripcion", "OK");
            return;
        }

        var firma = new Models.Firmas
        {
            nombres = txtNombres.Text,
            descripcion = txtDescrip.Text,
            ImageFirma = base64String,
            Fecha = DateTime.Now
        };

        if (await App.DataBase.Store(firma) > 0)
        {
            await DisplayAlert("Aviso", "Registro ingresado con éxito", "Ok");
        }



        try
        {
            if (controller != null)
            {
                if (await controller.Store(firma) > 0)
                {
                    await DisplayAlert("Aviso", "Registro Ingresado con Exito!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio un Error", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrio un Error: {ex.Message}", "OK");
        }
    }

    private async Task<String> ImageSourceToBase64(ImageSource imageSource)
    {
        if (imageSource is StreamImageSource streamImageSource)
        {
            using (var stream = await streamImageSource.Stream(CancellationToken.None))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
        return null;
    }
}
