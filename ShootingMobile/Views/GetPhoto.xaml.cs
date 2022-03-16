using Android.Widget;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShootingMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetPhoto : ContentPage
    {
        SKBitmap resourceBitmap;
        public GetPhoto()
        {
            InitializeComponent();
            DependencyService.Get<IMessage>().ShortAlert("Please pick a picture from galery or take a photo with camera");
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please pick an image"
            });
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                resourceBitmap = SKBitmap.Decode(stream);
                await Navigation.PushAsync(new MainPage(resourceBitmap),true);
            }
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                stream.Seek(0, SeekOrigin.Begin);
                SKImage img = SKImage.FromEncodedData(stream);
                resourceBitmap= SKBitmap.FromImage(img);
                await Navigation.PushAsync(new MainPage(resourceBitmap),true);
            }
        }
    }
    
}