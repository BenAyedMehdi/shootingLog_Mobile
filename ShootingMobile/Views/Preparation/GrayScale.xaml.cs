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
    public partial class GrayScale : ContentPage
    {
        SKBitmap resourceBitmap;
        public GrayScale(SKBitmap resourceBitmap)
        {
            InitializeComponent();
            popupLoadingView.IsVisible = true;
            NavigationPage.SetHasBackButton(this, false);
            this.resourceBitmap = resourceBitmap;
        }
        async void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;

            resourceBitmap = BitmapMethods.Threshold(resourceBitmap);
            BitmapMethods.DrawBitmap(resourceBitmap, surface, info);
            await Task.Delay(500);
            await Navigation.PushAsync(new Thresholded(resourceBitmap),true);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}