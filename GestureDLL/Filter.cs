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
    public abstract class Filter
    {
        /*
        *   Filter Class is aimed to separate an element from the original Image.
        *   
        *   Input: Rgb, Raw "Frame" (Image/Mat).
        *
        *   Output: Binary Image (Image<Gray,byte>), positive pixels is the element the derived filter class aimed to find, this will lead to 
        *           connecting all elements to 1 binary image by bitewise "AND" in "FilterManager" class.
        *   
        *
        *   How to use: override "Detect" funciton and add there the algorithm wich filter the image to binary image.
        */
        protected enum ENUM_skinDetection
        {
            MIN_COLOR_YCC_Y = 0, MIN_COLOR_YCC_CR = 133, MIN_COLOR_YCC_CB = 77, MAX_COLOR_YCC_Y = 255, MAX_COLOR_YCC_CR = 173, MAX_COLOR_YCC_CB = 127, DILATE_VAL = 2, ERODE_VAL = 3
        }
        protected enum ENUM_imageSubstraction
        {
            BLUR_VAL = 4, MIN_THRESHOLD_VAL = 50, MAX_THRESHOLD_VAL = 255, DILATE_VAL = 2, ERODE_VAL = 3
        }
        protected MyFrame _Input;//frame from camera.
        protected Image<Gray,byte> _output;
        public Filter(MyFrame raw)
        {
            _Input = raw;
        }
        public Filter()
        {
        }
        public virtual void UpdateInput(MyFrame raw)
        {
            _Input = raw;
        }
        public Mat ChangeColorFormat(Mat src,Emgu.CV.CvEnum.ColorConversion color)
        {
            CvInvoke.CvtColor(src, src, color);
            return src;
        }
        protected static Image<Gray, byte> MyBlur(Image<Gray, byte> img)
        {
            return img.SmoothBlur((int)ENUM_imageSubstraction.BLUR_VAL, (int)ENUM_imageSubstraction.BLUR_VAL);
        }
        public abstract Image<Gray, byte> Detect();
    }

}
