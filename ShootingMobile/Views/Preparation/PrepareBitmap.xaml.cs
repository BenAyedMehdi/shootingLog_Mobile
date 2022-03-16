using ShootingMobile.Model;
using ShootingMobile.Views.Preparation;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShootingMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrepareBitmap : ContentPage
    {
        SKBitmap resourceBitmap;

        [Obsolete]
        public PrepareBitmap(SkiaSharp.SKBitmap resourceBitmap)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            this.resourceBitmap = resourceBitmap;
        }
        [Obsolete]
        async void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            
            resourceBitmap = GrayScale(resourceBitmap);
            BitmapMethods.DrawBitmap(resourceBitmap, surface, info);

            await Navigation.PushAsync(new GrayScale(resourceBitmap),true);

        }
        private SKBitmap GrayScale(SKBitmap resourceBitmap)
        {
            if (resourceBitmap==null)
            {
                return null;
            }
            SKBitmap newBitmap = new SKBitmap(resourceBitmap.Width, resourceBitmap.Height);
            using (SKCanvas canvasTemp = new SKCanvas(newBitmap))
            {
                using (SKPaint paint = new SKPaint())
                {
                    paint.ColorFilter =
                        SKColorFilter.CreateColorMatrix(new float[]
                        {
                         0.3f, 0.59f, 0.11f, 0, 0,
                         0.3f, 0.59f, 0.11f, 0, 0,
                         0.3f, 0.59f, 0.11f, 0, 0,
                         0,     0,     0,     1, 0
                        });
                    canvasTemp.Clear();
                    canvasTemp.DrawBitmap(resourceBitmap, new SKPoint(), paint);
                }
            }
            return newBitmap;
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}