using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using AndroidBitmap = Android.Graphics.Bitmap;
using SystemBitmap = System.Drawing.Bitmap;

namespace ShootingMobile
{
    class BitmapConverter
    {
        public static AndroidBitmap Base64ToBitmap(string base64String)
        {

            byte[] imageAsBytes = Base64.Decode(base64String, Base64Flags.Default);
            return BitmapFactory.DecodeByteArray(imageAsBytes, 0, imageAsBytes.Length);
        }
        public static AndroidBitmap ByteToBitmap(byte[] b)
        {
            string s = Convert.ToBase64String(b);
            return Base64ToBitmap(s);
        }

        [Obsolete]
        public static AndroidBitmap SystemBitmapToAndroidBitmap(SystemBitmap b)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                b.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                BitmapDrawable bd = new BitmapDrawable(memoryStream);
                return bd.Bitmap;
            }
        }
        public static SystemBitmap AndroidBitmapToSystemBitmap(AndroidBitmap b)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                b.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, ms);
                Image image = Image.FromStream(ms);
                return new SystemBitmap(image);
            }
        }
        public static Stream RaiseImage(AndroidBitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, ms);
            return ms;
        }

        internal static SystemBitmap ByteToSystemBitmap(byte[] vs)
        {
            Image image;
            using (MemoryStream ms = new MemoryStream(vs))
            {
                image = Image.FromStream(ms);
            }

            return new SystemBitmap(image);
        }

    }
}
