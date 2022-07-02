using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestures
{
    public class MyPoint
    {
        public int X { get;private set; }
        public int Y { get;private set; }
        public MyPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
        public MyPoint(System.Drawing.Point otherP)
        {
            X = otherP.X;
            Y = otherP.Y;
        }
        public double Distance(MyPoint other)
        {
            return Math.Sqrt(Math.Pow(this.X - other.X, 2) + Math.Pow(this.Y - other.Y, 2));
        }

        /// <summary>
        /// Checking if there is any point in range from a given list.
        /// </summary>
        /// <param name="otherPoints"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public bool IsInRange(List<MyPoint> otherPoints,double distance)
        {
            foreach (MyPoint tempPoint in otherPoints)
            {
                if (this.Distance(tempPoint) < distance)
                    return true;//found point in range
            }
            return false;
        }
        public bool IsInRange(MyPoint otherPoint, double distance)
        {
            return (this.Distance(otherPoint) < distance);
        }

        public System.Drawing.Point ToDrawPoint()
        {
            return new System.Drawing.Point(X, Y);
        }

        public override string ToString()
        {
            return "{X: " + X + " Y: " + Y + "}";
        }
    }
}
