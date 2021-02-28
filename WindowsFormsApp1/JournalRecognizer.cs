using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace WindowsFormsApp1
{
    class JournalRecognizer
    {
        private Image<Bgr, byte> edg = null;

        public void DetectGrid(Image<Bgr, byte> img)
        {
            
            CvInvoke.Canny(img, edg, 50, 200);

            LineSegment2D[] lines;
            using (VectorOfPointF vector = new VectorOfPointF())
            {
                CvInvoke.HoughLines(img, vector, 1, Math.PI / 180, 150);

                List<LineSegment2D> lineList = new List<LineSegment2D>();
                for (int i = 0; i < vector.Size; i++)
                {
                    var rho = vector[i].X;
                    var theta = vector[i].Y;
                    var pt1 = new Point();
                    var pt2 = new Point();
                    var a = Math.Cos(theta);
                    var b = Math.Sin(theta);
                    var x0 = a * rho;
                    var y0 = b * rho;
                    pt1.X = (int)Math.Round(x0 + img.Width * (-b));
                    pt1.Y = (int)Math.Round(y0 + img.Height * (a));
                    pt2.X = (int)Math.Round(x0 + img.Width * (-b));
                    pt2.Y = (int)Math.Round(y0 + img.Height * (a));

                    lineList.Add(new LineSegment2D(pt1, pt2));
                }
                lines = lineList.ToArray();
            }
        }
    }
}
