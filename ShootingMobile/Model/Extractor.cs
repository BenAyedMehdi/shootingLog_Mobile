using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShootingMobile.Model
{
    class Extractor
    {
        //Calculated from c1
        const int WidthRatio = 1035;
        const int HeightRatio = 1337;

        public List<int> XCoordinates;
        public List<int> YCoordinates;
        public List<int> XBoldCoordinates;

        public List<Coardinate> points;

        public SKBitmap ROI { get; private set; }

        public Extractor(SKBitmap roi)
        {
            this.ROI = roi;
            XCoordinates = new List<int>();
            YCoordinates = new List<int>();
            XBoldCoordinates = new List<int>();
            LoadYs();
            points =new List<Coardinate>();
        }
        public List<Coardinate> Coardinates()
        {
            string s = BoldChars() + "\n\n" + NormalChars();
            return points;
        }
        public List<Coardinate> Results()
        {
            string s = BoldResults() + "\n\n" + NormalResults();
            return points;
        }
        private string NormalResults()
        {
            Recognizer recognizer = new Recognizer("normal");
            string message = "";
            string line = "";
            char bestMatch;

            for (int y = 0; y < YCoordinates.Count; y++)
            {
                XDetector xDetector = new XDetector(ROI, YCoordinates[y], "normal", "r");
                XCoordinates = xDetector.coordinates;
                if (XCoordinates!=null)
                {
                    for (int x = 0; x < XCoordinates.Count; x++)
                    {
                        if (x == 2)
                        {
                            line = line + ".";
                        }
                        else
                        {
                            bestMatch = recognizer.CompareToTemplates(Extract(XCoordinates[x], YCoordinates[y], "normal"));
                            line = line + bestMatch;
                        }
                    }
                    message = message + line + "\n";
                    points.Add(new Coardinate(y + 2, float.Parse(line)));
                    line = "";
                }
                else
                {
                    points = null;
                    return null;
                }
            }
            return message;
        }
        private string BoldResults()
        {
            Recognizer recognizer = new Recognizer("bold");
            string message = "";
            char bestMatch;
            XDetector xDetector = new XDetector(ROI, 38, "bold", "r");
            XCoordinates = xDetector.coordinates;
            if (XCoordinates!=null)
            {
                for (int x = 0; x < XCoordinates.Count; x++)
                {
                    if (x == 2)
                    {
                        message = message + ".";
                    }
                    else
                    {
                        bestMatch = recognizer.CompareToTemplates(Extract(XCoordinates[x], 38, "bold"));
                        message = message + bestMatch;
                    }
                }
                points.Add(new Coardinate(1, float.Parse(message)));
                return message;
            }
            else
            {
                points = null;
                return null;
            }
            
        }
        public string NormalChars()
        {
            Recognizer recognizer = new Recognizer("normal");
            string message = "";
            string line = "";
            char bestMatch;

            for (int y = 0; y < YCoordinates.Count; y++)
            {
                XDetector xDetector = new XDetector(ROI, YCoordinates[y], "normal", "c");
                XCoordinates = xDetector.coordinates;
                if (XCoordinates!=null)
                {
                    for (int x = 0; x < XCoordinates.Count; x++)
                    {
                        if (x == 4)
                        {
                            line = line + ",";
                        }
                        bestMatch = recognizer.CompareToTemplates(Extract(XCoordinates[x], YCoordinates[y], "normal"));
                        line = line + bestMatch;
                    }
                    points.Add(LineToPoint(line));
                    message = message + line + "\n";
                    line = "";
                }
                else
                {
                    points = null;
                    return null;
                }
            }
            return message;
        }
        public string BoldChars()
        {
            Recognizer recognizer = new Recognizer("bold");
            string message = "";
            char bestMatch;
            XDetector xDetector = new XDetector(ROI, 38, "bold", "c");
            XCoordinates = xDetector.coordinates;
            if (XCoordinates!=null)
            {
                for (int x = 0; x < XCoordinates.Count; x++)
                {
                    if (x == 4)
                    {
                        message = message + ",";
                    }
                    bestMatch = recognizer.CompareToTemplates(Extract(XCoordinates[x], 38, "bold"));
                    message = message + bestMatch;
                }
                points.Add(LineToPoint(message));
                return message;
            }
            else
            {
                points = null;
                return null;
            }
        }
        private Coardinate LineToPoint(string line)
        {
            string[] parts = line.Trim().Split(',');
            return new Coardinate(Int16.Parse(parts[0].Trim()), Int16.Parse(parts[1].Trim()));
        }
        private SKBitmap Extract(int x, int y, string type)//use point instead of x,y, use enum not string (size)
        {
            int height, width;

            if (type == "bold")//x is from 0 to 7 and y is always 38
            {
                height = 100;
                width = 60;
            }
            else // both x and y are the real coordinates
            {
                height = 78;
                width = 40;
            }
            SKRect rect = new SKRect(x, y, x + width, y + height);
            SKBitmap extracted = BitmapMethods.CropSkBitmap(ROI,rect);
            Adjust adjust = new Adjust(extracted);
            int x1 = x - adjust.Horizental();
            int y1 = y - adjust.Vertical();
            SKRect rect1 = new SKRect(x1, y1, x1 + width, y1 + height);
            extracted = BitmapMethods.CropSkBitmap(ROI, rect1);
            return extracted;
        }
        private void LoadYs()
        {
            YCoordinates.Add((int)(171 * ROI.Height / HeightRatio));
            YCoordinates.Add((int)(249 * ROI.Height / HeightRatio));
            YCoordinates.Add((int)(327 * ROI.Height / HeightRatio));
            YCoordinates.Add((int)(405 * ROI.Height / HeightRatio));
            YCoordinates.Add((int)(483 * ROI.Height / HeightRatio));
            YCoordinates.Add((int)(561 * ROI.Height / HeightRatio));
            YCoordinates.Add((int)(639 * ROI.Height / HeightRatio));
            YCoordinates.Add((int)(717 * ROI.Height / HeightRatio));
            YCoordinates.Add((int)(795 * ROI.Height / HeightRatio));
        }
    }
}
