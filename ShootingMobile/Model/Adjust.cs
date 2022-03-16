using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShootingMobile.Model
{
    class Adjust
    {
        SKBitmap extracted;
        int highest, lowest, topRight, topLeft;

        public Adjust(SKBitmap extracted)
        {
            this.extracted = extracted;
            highest = Highest();
            lowest = Lowest();
            topRight = TopRight();
            topLeft = TopLeft();
        }

        public int Vertical()
        {
            int low = extracted.Height - lowest;
            int averageInBothSides = (low + highest) / 2;
            return averageInBothSides - highest;
        }

        public int Horizental()
        {
            int toRight = extracted.Width - topRight;
            int averageInBothSides = (toRight + topLeft) / 2;
            return averageInBothSides - topLeft;
        }

        public int Highest()
        {
            for (int y = 0; y < extracted.Height - 1; y++)
            {
                for (int x = 0; x < extracted.Width - 1; x++)
                {
                    if (BitmapMethods.PointIsBlack(x, y, extracted))
                    {
                        return y;
                    }
                }
            }
            return 0;
        }

        public int Lowest()
        {
            for (int y = extracted.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < extracted.Width - 1; x++)
                {
                    if (BitmapMethods.PointIsBlack(x,y,extracted))
                    {
                        return y;
                    }
                }
            }
            return extracted.Height;
        }

        internal int TopRight()
        {
            for (int x = extracted.Width - 1; x >= 0; x--)
            {
                for (int y = 0; y < extracted.Height - 1; y++)
                {
                    if (BitmapMethods.PointIsBlack(x, y, extracted))
                    {
                        return x;
                    }
                }
            }
            return extracted.Width;
        }

        internal int TopLeft()
        {
            for (int x = 0; x < extracted.Width - 1; x++)
            {
                for (int y = 0; y < extracted.Height - 1; y++)
                {
                    if (BitmapMethods.PointIsBlack(x,y,extracted))
                    {
                        return x;
                    }
                }
            }
            return 0;
        }
    }
}
