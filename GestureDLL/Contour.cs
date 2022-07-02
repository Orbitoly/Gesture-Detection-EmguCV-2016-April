using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu;
using Emgu.CV.UI;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.Structure;
using Emgu.CV.Features2D;
using Emgu.CV.Shape;
using Emgu.CV.Util;
namespace Gestures
{
    class Contour : IComparable
    {
        VectorOfPoint _contour;
        public double Area { get { if (_contour != null && _contour.Size > 0) { return CvInvoke.ContourArea(_contour); } return 0; } }
        public System.Drawing.Point[] Drawing { get { return _contour.ToArray(); } }
        public Contour(VectorOfPoint contour)
        {
            this._contour =new VectorOfPoint(contour.ToArray());
        }
        public System.Drawing.Point[] ConvexHull
        {
            get
            {
                if(_contour.Size>0)
                {
                    Mat outPut = new Mat();
                    try
                    {
                        if(this.contourToPointF()!=null&& this.contourToPointF().Length>0)
                        {
                            return this.PointFToDrawingPoints(CvInvoke.ConvexHull(this.contourToPointF()).ToArray());

                        }

                    }
                    catch(Exception e)
                    {

                    }
                }
                return new Point[1] { new Point() };
            }
        }
        //public System.Drawing.Point[] ConvexDefects
        //{
        //    get
        //    {
        //        Point[] polyline = PointFToDrawingPoints(this.contourToPointF());//Set or caculate the points that defines the contour
        //        Image<Bgr, byte> image = new Image<Bgr, byte>(460, 640); //Load the image here
        //        List <Point> defects = new List<Point>();
        //        using (VectorOfPoint vp = new VectorOfPoint(polyline))
        //        using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint(vp))
        //        using (VectorOfInt convexHull = new VectorOfInt())
        //        using (Mat convexityDefect = new Mat())
        //        {
        //            //Draw the contour in white thick line
        //            CvInvoke.ConvexHull(vp, convexHull);
        //            CvInvoke.ConvexityDefects(vp, convexHull, convexityDefect);

        //            //convexity defect is a four channel mat, when k rows and 1 cols, where k = the number of convexity defects. 
        //            if (!convexityDefect.IsEmpty)
        //            {
        //                //Data from Mat are not directly readable so we convert it to Matrix<>
        //                Matrix<int> m = new Matrix<int>(convexityDefect.Rows, convexityDefect.Cols, convexityDefect.NumberOfChannels);
        //                convexityDefect.CopyTo(m);

        //                for (int i = 0; i < m.Rows; i++)
        //                {
        //                    int startIdx = m.Data[i, 0];
        //                    int endIdx = m.Data[i, 1];
        //                    defects.Add(polyline[startIdx]);
        //                    Point startPoint = polyline[startIdx];
        //                    Point endPoint = polyline[endIdx];
        //                    CvInvoke.Circle(image, startPoint, 2, new MCvScalar(0, 0, 255));
        //                    //CvInvoke.Circle(image, endPoint, 2, new MCvScalar(0, 0, 255));

        //                    //draw  a line connecting the convexity defect start point and end point in thin red line
        //                    //CvInvoke.Line(image, startPoint, endPoint, new MCvScalar(0, 0, 255));
        //                }
        //            }
        //            return defects.ToArray();
        //        }
        //    }
        //}
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Contour otherContour = obj as Contour;
            if (otherContour != null)
                return this.Area.CompareTo(otherContour.Area);
            else
                throw new ArgumentException("Object is not a Contour");
        }
        private PointF[] contourToPointF()
        {
            
            if (_contour == null||_contour.Size == 0)
                return null;
            return PointsToPointsF(_contour.ToArray());
            
        }
        private System.Drawing.Point[] PointFToDrawingPoints(PointF[] points)
        {
            if (points == null || points.Length == 0)
                return new System.Drawing.Point[1] { new Point() };
            System.Drawing.Point[] drawing = new System.Drawing.Point[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                drawing[i] = new System.Drawing.Point((int)points[i].X,(int)points[i].Y);
            }
            return drawing;
        }
        private PointF[] PointsToPointsF(Point[] points)
        {
            if (points == null || points.Length == 0)
                return null;
            PointF[] temp = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                temp[i] = new PointF(points[i].X, points[i].Y);
            }
            return temp;
        }
        public System.Drawing.Point FindCenter()
        {
            if (this.ConvexHull != null && this.ConvexHull.Length > 1)
            {
                System.Drawing.Point min=new Point();
                try
                {
                   min = this.ConvexHull.First();

                }
                catch (Exception e)
                {

                }

                foreach (Point p in this.ConvexHull)
                {
                    if(p.Y < min.Y)
                    {
                        min = p;

                    }
                }

                return min;
            }
            throw new Exception();


        }
        //public System.Drawing.Point FindCenter()
        //{
        //    int countX = 0;
        //    int counY = 0;
        //    int countNums = 0;
        //    foreach (Point p in this.ConvexHull)
        //    {
        //        countX += p.X;
        //        counY += p.Y;
        //        countNums++;
        //    }
        //    if (countNums == 0)
        //    {//no points
        //        return new Point();
        //    }
        //    return new System.Drawing.Point(countX / countNums, counY / countNums);
        //}
    }
}
