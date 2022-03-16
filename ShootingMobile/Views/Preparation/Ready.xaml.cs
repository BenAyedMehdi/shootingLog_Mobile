using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShootingMobile.Views.Preparation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ready : ContentPage
    {
        SKBitmap resourceBitmap;
        public Ready(SKBitmap resourceBitmap)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            DependencyService.Get<IMessage>().ShortAlert("Your picture is ready, click on 'Read' if you want to continue");
            this.resourceBitmap = resourceBitmap;
            if (!BitmapMethods.IsReadable(resourceBitmap))
            {
                readButton.IsEnabled = false;
                DependencyService.Get<IMessage>().LongAlert("We cannot read the ROI!\nPlease press'Cancel' to try again");
            }
        }
        async void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            BitmapMethods.DrawBitmap(resourceBitmap, surface, info);

        }

        protected override bool OnBackButtonPressed()
        {
            return true ;
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReadImage(resourceBitmap), true);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetPhoto(),true);
        }
    }
}