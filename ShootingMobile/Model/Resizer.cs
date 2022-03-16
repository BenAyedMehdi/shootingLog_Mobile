using SkiaSharp;
using System;

namespace ShootingMobile.Views
{
    class Resizer
    {
        private SKBitmap roi;

        public Resizer(SKBitmap roi)
        {
            this.roi = roi;
        }

        [Obsolete]
        internal SKBitmap ResizeImage(int v1, int v2)
        {
            var resized = roi.Resize(new SKImageInfo(v1, v2),SKBitmapResizeMethod.Lanczos3);
            return resized;
        }
    }
}