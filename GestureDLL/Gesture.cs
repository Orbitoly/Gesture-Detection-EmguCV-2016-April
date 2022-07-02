using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;
namespace Gestures
{

    public class Gesture
    {
        static Random rnd = new Random();//for random color

        /*
        *   Gesture Class is aimed to contatin the Points that declare the Gesture.
        */
        public string Name { get; set; }
        public Bgr Color { get; private set; }
        public List<MyPoint> Points { get; private set; }
        internal Gesture(string name,List<MyPoint> points)
        {
            Name = name;
            Points = points;
            Color = RandomColor();
        }
        internal Gesture(List<MyPoint> points)
        {
            Name = "";
            Points = points;
            Color = RandomColor();
        }
        /// <summary>
        /// Checks if all Points in the given array has a matching point in their range(distance) from the given list of MyPoint.
        /// The way the algorithem was written 
        /// </summary>
        /// <param name="otherPoints"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public bool IsInRange(Queue<MyPoint> otherPoints,double distance)
        {
            Queue<MyPoint> copyOfOther = new Queue<MyPoint>(otherPoints);
            foreach (MyPoint tempPoint in this.Points)
            {//goes over all points

                while(copyOfOther!=null&&copyOfOther.Count != 0&&!tempPoint.IsInRange(copyOfOther.Peek(),distance))
                {//finds the first point in otherPoints queue that is in range of current tempPoint
                    copyOfOther.Dequeue();
                }
                if(copyOfOther.Count==0)
                {//cant find point in range for next point
                    return false;
                }
            }
            return true;
        }
        public System.Drawing.Point[] ToDrawArr()
        {
            if(Points==null||Points.Count==0)
            {
                return null;
            }
            System.Drawing.Point[] draw = new System.Drawing.Point[Points.Count];
            for (int i = 0; i < draw.Length; i++)
            {
                draw[i] = new System.Drawing.Point(Points[i].X, Points[i].Y);
            }
            return draw;
        }
        public Bgr RandomColor()
        {//you are really bored if you came to check this function...
            double red = rnd.Next(0, 255);
            double green = rnd.Next(0, 255);
            double blue = rnd.Next(0, 255);

            return new Bgr(red, green, blue);
        }
    }
}
