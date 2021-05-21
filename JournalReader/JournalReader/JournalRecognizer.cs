using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace JournalReader
{
    class JournalRecognizer
    {
        //private LineSegment2D[] lines;
        private List<LineSegment2D> lineList = new List<LineSegment2D>();

        public void DetectGrid(Image<Bgr, byte> img, out MemoryStream streamImage)
        {
            Image<Gray, byte> edge = new Image<Gray, byte>(img.Width, img.Height, new Gray(0));

            CvInvoke.Canny(img, edge, 50, 150);

            //CvInvoke.CvtColor(edge, copyedge, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);

            using (VectorOfPointF vector = new VectorOfPointF())
            {
                CvInvoke.HoughLines(edge, vector, 1, Math.PI / 180, 230);

                for (int i = 0; i < vector.Size; i++)
                {
                    float rho = vector[i].X;
                    float theta = vector[i].Y;
                    Point pt1 = new Point();
                    Point pt2 = new Point();
                    double a = Math.Cos(theta);
                    double b = Math.Sin(theta);
                    double x0 = a * rho;
                    double y0 = b * rho;
                    pt1.X = (int)Math.Round(x0 + img.Width * (-b));
                    pt1.Y = (int)Math.Round(y0 + img.Height * a);
                    pt2.X = (int)Math.Round(x0 - img.Width * (-b));
                    pt2.Y = (int)Math.Round(y0 - img.Height * a);
                    bool condAlternationLines = CheckAlternationLines(new LineSegment2D(pt1, pt2));
                    bool condDiagonal = Math.Abs(pt1.X - pt2.X) > 10 && Math.Abs(pt1.Y - pt2.Y) > 10;
                    bool condLeftEdge = (pt1.X > 0 && pt1.X < img.Width*0.33) && !(pt1.X > img.Width*0.15 && pt1.X < img.Width*0.16);
                    bool condTopEdge = pt1.Y > 0 && pt1.Y < img.Height*0.1;
                    bool condBottomEdge = pt1.Y > img.Height * 0.92 && pt1.Y < img.Height;
                    if (condDiagonal || condLeftEdge || condTopEdge || condBottomEdge) continue;
                    if (condAlternationLines) continue;

                    lineList.Add(new LineSegment2D(pt1, pt2));
                    CvInvoke.Line(img, pt1, pt2, new Bgr(Color.Red).MCvScalar, 3, Emgu.CV.CvEnum.LineType.AntiAlias);
                }
                //lines = lineList.ToArray();
                lineList.Clear();
            }
            CvInvoke.Imwrite("imageGrid.jpg", img);
            streamImage = new MemoryStream(img.ToJpegData());
        }

        private bool CheckAlternationLines(LineSegment2D lineSegment)
        {
            for (int i = 0; i < lineList.Count; i++)
            {
                bool condX = Math.Abs(lineSegment.P1.X - lineList[i].P1.X) < 40 && Math.Abs(lineSegment.P2.X - lineList[i].P2.X) < 40;
                bool condY = Math.Abs(lineSegment.P1.Y - lineList[i].P1.Y) < 40 && Math.Abs(lineSegment.P2.Y - lineList[i].P2.Y) < 40;
                if (condX && condY) return true;
            }
            return false;
        }
    }
}
