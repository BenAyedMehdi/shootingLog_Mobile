using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShootingMobile.Model
{
    class Recognizer
    {
        public string type { get; private set; }

        public Templates Templates;

        public Recognizer(string type)
        {
            this.type = type;
            this.Templates = new Templates(type);
        }

        public char CompareToTemplates(SKBitmap extracted)
        {

            int[] matches = new int[Templates.Count];
            for (int i = 0; i < Templates.Count; i++)
            {
                matches[i] = GetMatchRate(Templates[i], extracted);
            }
            int max = MaxOfArray(matches);

            if (matches[11] == max)
            {
                return ' ';
            }
            int pos = Array.IndexOf(matches, max);

            return ArrayValue(pos);
        }

        private int MaxOfArray(int[] matches)
        {
            int max = matches[0];
            for (int i = 1; i < matches.Length; i++)
            {
                if (matches[i]>max)
                {
                    max = matches[i];
                }
            }
            return max;
        }

        private int GetMatchRate(SKBitmap template, SKBitmap extracted)
        {
            int countBlack = 0;
            for (int x = 0; x < template.Width; x++)
            {
                for (int y = 0; y < template.Height; y++)
                {
                    if (BitmapMethods.PointIsBlack(x, y, template) &&
                        BitmapMethods.PointIsBlack(x, y, extracted))
                    {
                        countBlack++;
                    }
                }
            }
            return countBlack;
        }

        private char ArrayValue(int pos)
        {
            switch (pos)
            {
                case 0: return '0';
                case 1: return '1';
                case 2: return '2';
                case 3: return '3';
                case 4: return '4';
                case 5: return '5';
                case 6: return '6';
                case 7: return '7';
                case 8: return '8';
                case 9: return '9';
                case 10: return '-';
                case 11: return ' ';
                case 12: return ':';
                case 13: return '.';

            }
            return 'n';
        }
    }
}
