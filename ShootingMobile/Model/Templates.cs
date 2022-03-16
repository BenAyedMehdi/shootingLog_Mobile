using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace ShootingMobile.Model
{
    class Templates
    {
        public int templatesCount { get; private set; }
        public int height { get; private set; }
        public int width { get; private set; }
        public string type { get; private set; }
        public string fileName { get; private set; }
        public SKBitmap  extracted { get; private set; }

        public List<SKBitmap> templates;

        public SKBitmap this[int index] => templates[index];
        public int Count => templates.Count;
        public Templates(string type)
        {
            this.type = type;
            templates = new List<SKBitmap>();

            if (type.Equals("bold"))
            {
                this.fileName = "ShootingMobile.Images.boldchars.png";
                this.height = 100;
                this.width = 60;
                this.templatesCount = 14;
            }
            else
            {
                this.fileName = "ShootingMobile.Images.chars.png";
                this.height = 78;
                this.width = 40;
                this.templatesCount = 12;
            }
            ExtractTemplates();
        }


        public void ExtractTemplates()
        {
            if (fileName != null)
            {
                int x=5, y=5;
                SKBitmap extracted;
                SKBitmap chars = GetSkBitmap(fileName);
                for (int i = 0; i < templatesCount; i++)
                {
                    x = i * width+5;
                    SKRect rect = new SKRect(x, y, x+width, y+height);
                    extracted = BitmapMethods.CropSkBitmap(chars, rect);
                    Adjust adjust = new Adjust(extracted);
                    int x1 = x - adjust.Horizental();
                    int y1 = y - adjust.Vertical();
                    SKRect rect1 = new SKRect(x1, y1, x1 + width, y1 + height);
                    extracted = BitmapMethods.CropSkBitmap(chars, rect1);
                    templates.Add(extracted);
                }
            }
        }
        public SKBitmap GetSkBitmap(string fileName)
        {
            Assembly assembly = typeof(Templates).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(fileName))
            {
                SKBitmap s = SKBitmap.Decode(stream);
                return s;
            };
        }
    }
}
