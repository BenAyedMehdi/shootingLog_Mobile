using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShootingMobile.Model
{
    class RoiDetector
    {
        public double ratio { get; private set; }
        public SKBitmap pic { get; set; }
        public int right { get; private set; }
        public double length { get; set; }
        public int width { get; set; }

        public RoiDetector(SKBitmap pic)
        {
            ratio = (double)1338 / 1036;
            this.pic = pic;
            right = Right();
            length = VectorLength();
            width = (int)(length / ratio); 
        }
        internal SKBitmap RotateImage(float v)
        {
            SKBitmap rotatedBitmap = pic;
            double radians = Math.PI * v / 180;
            float sine = (float)Math.Abs(Math.Sin(radians));
            float cosine = (float)Math.Abs(Math.Cos(radians));
            int originalWidth = pic.Width;
            int originalHeight = pic.Height;
            int rotatedWidth = (int)(cosine * originalWidth + sine * originalHeight);
            int rotatedHeight = (int)(cosine * originalHeight + sine * originalWidth);

            rotatedBitmap = new SKBitmap(rotatedWidth, rotatedHeight);
            using (SKCanvas canvas = new SKCanvas(rotatedBitmap))
            {
                canvas.Clear();
                canvas.Translate(rotatedWidth / 2, rotatedHeight / 2);
                canvas.RotateDegrees(v);
                canvas.Translate(-originalWidth / 2, -originalHeight / 2);
                canvas.DrawBitmap(pic, new SKPoint());
            }
            return rotatedBitmap;
        }
        internal SKRect GetRectangle()
        {
            int x1 = (int)(TopRight().X) - width, y1 = (int)(TopRight().Y), x2 = (int)(TopRight().X),
                y2 = (int)(LowRight().Y);
            x2 = right;
            x2--;
            y2 = pic.Height / 2;
            while (y1 > 0 && !BitmapMethods.PointIsBlack(x2, y1, pic))
            {
                y1--;
            }
            x2 = right + 2;
            y1++;
            x1 = x2 - width;
            y2 = y1 + (int)length;
            int w = x2 - x1 + 5; int h = y2 - y1 + 6;
            if (x1<0 ||y1<0 ||w<0||h<0)
            {
                return SKRect.Create(0,0,1,1);
            }
            return SKRect.Create(x1 - 2, y1 - 3, w, h);
            //return new SKRect(x1 - 2, y1 - 3, x2 + 3, y2 + 3);
        }
        public double VectorLength()
        {
            return LowRight().Y - TopRight().Y;
        }
        public float Angle()
        {
            var lowRight = LowRight();
            var topRight = TopRight();
            var vector = lowRight - topRight;
            double opposite = Math.Abs(lowRight.X - topRight.X);
            double addjacent = lowRight.Y - topRight.Y;
            double angleTan = opposite / addjacent;
            float result = (float)(Math.Atan2(vector.X, vector.Y) * (180 / Math.PI));
            return result;
        }
        public SKPoint TopRight()//returns top Y
        {
            int x = right - 10;
            int y = pic.Height / 4 * 3;
            while (y > 0)
            {
                if (BitmapMethods.PointIsBlack(x, y, pic))
                {
                    return GoIfPossible(x, y, pic, 1);
                }
                y--;
            }
            return new SKPoint { X = pic.Width, Y = 0 };
        }
        public SKPoint LowRight()//returns lower Y
        {
            int x = right - 15;
            int y = pic.Height / 4 * 3;
            while (y < pic.Height)
            {
                if (BitmapMethods.PointIsBlack(x, y, pic))
                {
                    return GoIfPossible(x, y, pic, -1);
                }
                y++;
            }
            return new SKPoint { X = pic.Width, Y = pic.Height };
        }
        private SKPoint GoIfPossible(int x, int y, SKBitmap pic, int sign)
        {
            y = y + 5 * sign;
            while (!BitmapMethods.PointIsBlack(x, y, pic))
            {
                x++;
            }
            x--;
            while (!BitmapMethods.PointIsBlack(x, y, pic))
            {
                y = y - 1 * sign;
            }

            return new SKPoint { X = x + 1, Y = y };
        }
        private int Right()
        {
            int y = HorizentalWhiteLine() - 5;
            int x = pic.Width / 2;
            while (x < pic.Width)
            {
                if (BitmapMethods.PointIsBlack(x, y, pic))
                {
                    return x - 1;
                }
                x++;
            }
            return pic.Width - 1;
        }//returns the right side X
        public int HorizentalWhiteLine()//return Y of empty line
        {
            int startX = pic.Width / 5;
            int endX = pic.Width - startX;
            int x, y = pic.Height /4* 3;
            bool blackCrossed = false;
            while (y < pic.Height)
            {
                y++;
                x = startX;
                while (!blackCrossed && x < endX)
                {
                    x++;
                    if (BitmapMethods.PointIsBlack(x, y, pic))
                    {
                        return y;
                    }
                }
            }
            return 0;
        }

    }
}
