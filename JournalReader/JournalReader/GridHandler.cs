using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;

namespace JournalReader
{
    class GridHandler
    {
        //private LineSegment2D[] lines;
        private List<LineSegment2D> lineList = new List<LineSegment2D>();
        private List<Point> pointList = new List<Point>();

        public void DetectGrid(ref Image<Bgr, byte> image)
        {
            Image<Gray, byte> edge = new Image<Gray, byte>(image.Width, image.Height, new Gray(0));

            CvInvoke.Canny(image, edge, 50, 150);

            //CvInvoke.CvtColor(edge, copyedge, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);

            lineList.Clear();

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
                    pt1.X = (int)Math.Round(x0 + image.Width * (-b));
                    pt1.Y = (int)Math.Round(y0 + image.Height * a);
                    pt2.X = (int)Math.Round(x0 - image.Width * (-b));
                    pt2.Y = (int)Math.Round(y0 - image.Height * a);

                    bool condAlternationLines = CheckAlternationLines(new LineSegment2D(pt1, pt2));
                    bool condDiagonal = Math.Abs(pt1.X - pt2.X) > 10 && Math.Abs(pt1.Y - pt2.Y) > 10;
                    bool condLeftEdge = pt1.X > 0 && pt1.X < image.Width * 0.33; // && !(pt1.X > image.Width*0.15 && pt1.X < image.Width*0.16);
                    bool condTopEdge = pt1.Y > 0 && pt1.Y < image.Height * 0.1;
                    bool condBottomEdge = pt1.Y > image.Height * 0.92 && pt1.Y < image.Height;

                    if (condDiagonal || condLeftEdge || condTopEdge || condBottomEdge) continue;
                    if (condAlternationLines) continue;

                    lineList.Add(new LineSegment2D(pt1, pt2));
                    CvInvoke.Line(image, pt1, pt2, new Bgr(Color.Red).MCvScalar, 3, LineType.AntiAlias);
                }
                //lines = lineList.ToArray();
            }
            //CvInvoke.Imwrite("imageGrid.jpg", image);
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

        public void DetectIntersect(ref Image<Bgr, byte> image)
        {
            for (int i = 0; i < lineList.Count; i++)
            {
                for (int j = 0; j < lineList.Count; j++)
                {
                    Point intersection = Intersection(lineList[i], lineList[j]);
                    if (!intersection.Equals(new Point(0, 0)))
                    {
                        CvInvoke.Circle(image, intersection, 3, new Bgr(Color.Green).MCvScalar, 10, LineType.AntiAlias);
                        pointList.Add(intersection);
                    }
                }
            }
            CvInvoke.Imwrite("imageGrid2.jpg", image);
        }

        private Point Intersection(LineSegment2D line1, LineSegment2D line2)
        {
            double A1 = line1.P1.Y - line1.P2.Y;
            double B1 = line1.P2.X - line1.P1.X;
            double C1 = A1 * line1.P1.X + B1 * line1.P1.Y;

            double A2 = line2.P1.Y - line2.P2.Y;
            double B2 = line2.P2.X - line2.P1.X;
            double C2 = A2 * line2.P1.X + B2 * line2.P1.Y;

            double det = A1 * B2 - A2 * B1;
            if (det == 0)
            {
                return new Point(0, 0); //paralel line
            }
            else
            {
                int x = (int)((B2 * C1 - B1 * C2) / det);
                int y = (int)((A1 * C2 - A2 * C1) / det);
                return new Point(x, y);
            }
        }
    }
}
