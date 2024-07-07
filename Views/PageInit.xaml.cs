using Syncfusion.Maui.SignaturePad;

namespace Ejercicio2._2_Grupo2.Views;

public partial class PageInit : ContentPage
{
    public PageInit()
    {
        InitializeComponent();
        SfSignaturePad signaturePad = new SfSignaturePad();
    }
    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {

        ImageSource? source = signaturePad.ToImageSource();
        string base64String = await ImageSourceToBase64(source);


        var firma = new Models.Firmas
        {
            ImageFirma = base64String,
            Fecha = DateTime.Now
        };

        if (await App.DataBase.Store(firma) > 0)
        {
            await DisplayAlert("Aviso", "Registro ingresado con éxito", "Ok");
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
