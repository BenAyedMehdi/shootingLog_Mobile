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
    public partial class Thresholded : ContentPage
    {
        SKBitmap roi;
        public Thresholded(SKBitmap resourceBitmap)
        {
            InitializeComponent();
            popupLoadingView.IsVisible = true;
            NavigationPage.SetHasBackButton(this, false);
            roi = Work(resourceBitmap);
        }

        private SKBitmap Work(SKBitmap resourceBitmap)
        {
            RoiDetector roiDetector = new RoiDetector(resourceBitmap);
            resourceBitmap = roiDetector.RotateImage(roiDetector.Angle());
            RoiDetector newRoiDetector = new RoiDetector(resourceBitmap);
            SKRect cropRect = newRoiDetector.GetRectangle();
            roi= BitmapMethods.CropSkBitmap(resourceBitmap, cropRect);
            Resizer resizer = new Resizer(roi);
            SKBitmap b = resizer.ResizeImage(1038, 1340);
            return BitmapMethods.Threshold(b);
        }

        async void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;

            BitmapMethods.DrawBitmap(roi, surface, info);

            await Navigation.PushAsync(new Ready(roi),true);
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}