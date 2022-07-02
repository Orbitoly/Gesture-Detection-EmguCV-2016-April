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
namespace Gestures
{
    internal class FilterManager
    {
        public static event EventHandler<DebugImageEventArgs> Debug;
        public static event EventHandler<DebugImageEventArgs> Filtered;

        static List<Filter> myFiltersList;
        static FilterManager()
        {
            myFiltersList = new List<Filter>();
            myFiltersList.Add(new Filter_SkinColor());
            myFiltersList.Add(new Filter_ImageSubstraction());
            Camera.FrameWasShot += FilterHandBinary;

        }
        public static void FilterHandBinary(object sender, FrameShotEventArgs e)
        {
            MyFrame current = e.frame;
            

            if (current == null||myFiltersList==null||myFiltersList.Count==0)
                return;
            foreach (Filter tempFilter in myFiltersList)
            {
                tempFilter.UpdateInput(current);
            }
            Image<Gray, byte> finalImage=null;
            Image<Gray, byte> tempImage = null;

            foreach (Filter tempFilter in myFiltersList)
            {
                if(finalImage== null)
                {
                    finalImage = tempFilter.Detect();
                }
                else
                {
                    tempImage= tempFilter.Detect();
                    if(tempImage!= null)
                    {
                        finalImage &= tempImage;

                    }
                }
            }
            if(Debug!=null)
            {
                //Debug(null, new DebugImageEventArgs() { img = finalImage, name = "WINDOW_NAME_FILTER_FINAL" });

            }
            if (Filtered != null)
            {
                Filtered(null, new DebugImageEventArgs() { img = finalImage, name = "WINDOW_NAME_FILTER_FINAL" });

            }
        }

    }
}
