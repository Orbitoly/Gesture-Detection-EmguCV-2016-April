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
    
    internal class Filter_SkinColor : Filter
    {
        public static event EventHandler<DebugImageEventArgs> Debug;

        public Filter_SkinColor(MyFrame raw) : base(raw)
        {
        }
        public Filter_SkinColor() : base()
        {
        }
        public override Image<Gray, byte> Detect()
        {

            if (_Input == null||_Input.Image==null)
                return null;

            Image<Ycc, byte> YCCskin = _Input.Image.Convert<Ycc, byte>();
            Image<Gray, byte> _out = new Image<Gray, byte>(YCCskin.Width, YCCskin.Height);

            //------------------- Setting Color Range --------------------//
            Ycc min = new Ycc((double)ENUM_skinDetection.MIN_COLOR_YCC_Y, (double)ENUM_skinDetection.MIN_COLOR_YCC_CR, (double)ENUM_skinDetection.MIN_COLOR_YCC_CB);
            Ycc max = new Ycc((double)ENUM_skinDetection.MAX_COLOR_YCC_Y, (double)ENUM_skinDetection.MAX_COLOR_YCC_CR, (double)ENUM_skinDetection.MAX_COLOR_YCC_CB);
            //-----------------------------------------------------------//
            _out = YCCskin.InRange(min, max);//actual range filter

            _out = _out.Dilate((int)ENUM_skinDetection.DILATE_VAL).Erode((int)ENUM_skinDetection.ERODE_VAL);//improve image
            ShootEvents(_out);
            return _out;

        }
        public void ShootEvents(IImage _out)
        {
            if (Debug != null)
            {
                Debug(null, new DebugImageEventArgs() { img = _out, name = "WINDOW_NAME_FILTER_SKIN" });
            }
        }
    }
}
