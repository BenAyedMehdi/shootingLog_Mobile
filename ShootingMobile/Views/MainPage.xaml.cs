using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Android.Graphics;
using Xamarin.Forms;
using AndroidBitmap = Android.Graphics.Bitmap;
using SystemBitmap = System.Drawing.Bitmap;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using SkiaSharp.Views;
using AndroidX.Core.Content;
using Android;
using Android.Content.PM;
using ShootingMobile.Views;

namespace ShootingMobile
{
    public partial class MainPage : ContentPage
    {
        //string resourceID = "ShootingMobile.Images.r3.jpg";
        SKBitmap resourceBitmap;

        public MainPage(SKBitmap resourceBitmap)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
                "NTIwOTIwQDMxMzkyZTMzMmUzMEJDVkZIRHByL0VWbythdzlObk5ROWlUOVhUTGRsRUFuQVJvS0V5TzFmcW89");

            InitializeComponent();
            DependencyService.Get<IMessage>().ShortAlert("Your picture was imported, click on 'Prepare'");
            this.resourceBitmap = resourceBitmap;
            canvasView.PaintSurface += canvasView_PaintSurface;
        }
        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            canvas.DrawBitmap(resourceBitmap, info.Rect);
            
        }
        [Obsolete]
        private async void Button_Clicked(object sender, EventArgs e)
        {
            popupLoadingView.IsVisible = true;
            await Navigation.PushAsync(new PrepareBitmap(resourceBitmap),true);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetPhoto(),true);
        }
    }
}
