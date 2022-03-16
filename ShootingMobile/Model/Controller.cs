using System;
using System.Collections.Generic;
using Android.Graphics;
using System.Text;
using AndroidBitmap = Android.Graphics.Bitmap;
using SkiaSharp.Views.Forms;
using SkiaSharp;
namespace ShootingMobile
{
    class Controller 
    {
        //private static float threshold = 0.4f;
        public SKBitmap image { get; private set; }
        public SKBitmap roi { get; private set; }
        public bool isCoardinates { get; private set; }

        public Controller(SKBitmap image)
        {
            this.image = image;
            //roi = PrepareImage();
            //isCoardinates = IsCoardinates(image);
        }
        /*
        public SKBitmap PrepareImage()
        {
            //Prepare image
            this.image = ConvertToGrayScale(image);
            //this.image = Threshold(image, threshold);

            return image;
        }

        public Bitmap ConvertToGrayScale(SKBitmap bmpOriginal)
        {
            int width, height;
            height = bmpOriginal.Height;
            width = bmpOriginal.Width;

            Bitmap bmpGrayscale = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
            Canvas c = new Canvas(bmpGrayscale);
            Paint paint = new Paint();
            ColorMatrix cm = new ColorMatrix();
            cm.SetSaturation(0);
            ColorMatrixColorFilter f = new ColorMatrixColorFilter(cm);
            paint.SetColorFilter(f);
            c.DrawBitmap(bmpOriginal, 0, 0, paint);
            return bmpGrayscale;
        }
        public bool IsCoardinates(AndroidBitmap ROI)
        {
            if (BitmapMethods.PointIsBlack(10,10,ROI))
            {
                return true ;
            }
            return false;
        }*/
    }
}
