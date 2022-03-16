
using Android.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;
namespace ShootingMobile
{
    class BitmapMethods
    {
        public static bool verticalLineIsBlank(int x, int minY, int extra, SKBitmap image)
        {
            int maxY = minY + extra - 20;
            for (int y = minY; y < maxY; y++)
            {
                if (BitmapMethods.PointIsBlack(x, y, image))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool isCoardinates(SKBitmap roi)
        {
            if (horizentalLineIsBlank(1150, 400, 670, roi))
            {
                return true;
            }
            return false;
        }
        public static bool horizentalLineIsBlank(int y, int minx, int maxx, SKBitmap image)
        {
            for (int x = minx; x < maxx; x++)
            {
                if (BitmapMethods.PointIsBlack(x, y, image))
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool IsReadable(SKBitmap roi)
        {
            SKRect rect1 = SKRect.Create(100,190, 210, 600);
            SKBitmap b1 = CropSkBitmap(roi, rect1);
            bool c1= BitmapIsWhite(b1);
            SKRect rect2 = SKRect.Create(100, 50, 500, 100);
            SKBitmap b2 = CropSkBitmap(roi, rect2);
            bool c2 = !BitmapIsWhite(b2);
            return c1 && c2;
        }

        private static bool BitmapIsWhite(SKBitmap b)
        {
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    if (PointIsBlack(x,y,b))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool PointIsBlack(int x, int y, SKBitmap bitmap)
        {
            try
            {
                SKColor skc = bitmap.GetPixel(x, y);
                Color c = skc.ToFormsColor();
                if (c.Luminosity < 0.1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public static SKBitmap CropSkBitmap(SKBitmap resourceBitmap, SKRect cropRect)
        {
            SKBitmap croppedBitmap = new SKBitmap((int)cropRect.Width,
                                                  (int)cropRect.Height);
            SKRect dest = new SKRect(0, 0, cropRect.Width, cropRect.Height);
            SKRect source = new SKRect(cropRect.Left, cropRect.Top,
                                       cropRect.Right, cropRect.Bottom);

            using (SKCanvas canvas = new SKCanvas(croppedBitmap))
            {
                canvas.Clear();
                canvas.DrawBitmap(resourceBitmap, source, dest);
            }

            return croppedBitmap;
        }
        public static SKBitmap Threshold(SKBitmap resourceBitmap)
        {
            if (resourceBitmap == null)
            {
                return null;
            }
            SKColor[] pixels = resourceBitmap.Pixels;
            int red, green, blue;
            for (int x = 0; x < resourceBitmap.Width; x++)
            {
                for (int y = 0; y < resourceBitmap.Height; y++)
                {
                    SKColor colore = pixels[(resourceBitmap.Height * x) + y];
                    red = colore.Red > 102 ? 255 : 0;
                    green = colore.Green > 102 ? 255 : 0;
                    blue = colore.Blue > 102 ? 255 : 0;
                    SKColor newColor = new SKColor((byte)red, (byte)green, (byte)blue);
                    pixels[(resourceBitmap.Height * x) + y] = newColor;
                }
            }
            resourceBitmap.Pixels = pixels;
            return resourceBitmap;
        }
        public static void DrawBitmap(SKBitmap resourceBitmap, SKSurface surface, SKImageInfo info)
        {
            if (resourceBitmap == null || surface == null || info == null)
            {
                return;
            }
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            canvas.DrawBitmap(resourceBitmap, info.Rect);
        }
    }
}
