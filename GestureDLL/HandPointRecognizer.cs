using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu;
using DirectShowLib;
using Emgu.CV.UI;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.Structure;
using Emgu.CV.Features2D;
using Emgu.CV.Shape;
using Emgu.CV.Util;

namespace Gestures
{
    internal class HandCenterPointDetectedEventArgs : EventArgs
    {
        public MyPoint center { get; set; }
        public Image<Bgr, byte> debuggerContour { get; set; }
    }
    internal class HandPointRecognizer
    {

        public static event EventHandler<HandCenterPointDetectedEventArgs> HandCenterPointWasDetected;
        public static event EventHandler<DebugImageEventArgs> Debug;

        static HandPointRecognizer()
        {
            FilterManager.Filtered += OnFiltered;
        }

        /*
        Exract Contours as vectorofvectorofpoints, and then converts them to list of vectors.
        */
        public static List<Contour> GetContours(IImage _input)
        {
            if (_input == null)
                return null;

            VectorOfVectorOfPoint contoursDetected = new VectorOfVectorOfPoint();
            List<Contour> myConts = new List<Contour>();

            CvInvoke.FindContours(_input, contoursDetected, null, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            for (int i = 0; i < contoursDetected.Size; i++)
            {
                myConts.Add(new Contour(contoursDetected[i]));
            }
            myConts.Sort();
            return myConts;
        }

        public static void OnFiltered(object sender, DebugImageEventArgs args)
        {
            IImage temp = args.img;
            List<Contour> MyContours = GetContours(temp);
            Image<Gray,byte> imageWithConts= temp as Image<Gray, byte>;
            Image<Bgr, byte> onlyConts=new Image<Bgr, byte>(imageWithConts.Width,imageWithConts.Height);
            System.Drawing.Point Center;
            if (MyContours != null && MyContours.Count > 0)
            {
                Contour biggestCont = MyContours.Last();
                if (biggestCont.Area > 200)
                {

                    try
                    {
                        Center = biggestCont.FindCenter();
                        onlyConts.Draw(biggestCont.Drawing, new Bgr(Color.LimeGreen), 2);
                        onlyConts.DrawPolyline(biggestCont.ConvexHull, true, new Bgr(Color.Maroon), 2);
                        onlyConts.Draw(new CircleF(biggestCont.FindCenter(), 2), new Bgr(Color.Maroon), 2);
                        if (HandCenterPointWasDetected != null)
                        {
                            HandCenterPointWasDetected(null, new HandCenterPointDetectedEventArgs() { center = new MyPoint(biggestCont.FindCenter()), debuggerContour = onlyConts });
                        }

                        if (Debug != null)
                        {
                            Debug(null, new DebugImageEventArgs() { img = onlyConts, name = "conts" });
                        }
                    }
                    catch(Exception)
                    {//contour is not big enough

                    }
                    
                    
                }

            }

            
           
                
        }
    }
}
