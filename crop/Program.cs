using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace crop
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"image.jpg");
            CropCircle(path);
            
        }

        static void CropImg(string path)
        {
            string newPath = Path.Combine(Environment.CurrentDirectory, @"imagecrop.jpg");
            Bitmap cropped;
            using (Bitmap src = Image.FromFile(path) as Bitmap)
            {
                Rectangle rect = new Rectangle(400, 500, 250, 400);

                cropped = src.Clone(rect, src.PixelFormat);
            }
            cropped.Save(newPath);
            cropped.Dispose();
        }

        static void CropCircle(string pathImg)
        {
            int radius = 100;
            int x = 400;
            int y = 500;
            string newPath = Path.Combine(Environment.CurrentDirectory, @"imagecropCircle.jpg");
            Bitmap tmp = new Bitmap(radius * 2, radius * 2);
            Graphics g = Graphics.FromImage(tmp);
            g.TranslateTransform(radius, radius);
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(-radius, -radius, 2 * radius, 2 * radius);
            Region region = new Region(path);
            g.SetClip(region, CombineMode.Replace);
            Bitmap bmp = new Bitmap(pathImg);
            g.DrawImage(bmp, new Rectangle(-radius, -radius, radius * 2, radius * 2), new Rectangle(x - radius, y - radius, radius * 2, radius * 2), GraphicsUnit.Pixel);
            tmp.Save(newPath);
        }
    }
}
