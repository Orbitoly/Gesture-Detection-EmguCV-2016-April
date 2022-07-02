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
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace Gestures
{
    public class MyFrame
    {
        private Image<Bgr, byte> _img;
       
        public MyFrame()
        {
            //_mat = new Mat();
        }

        public MyFrame(Mat matrix)
        {
            _img = matrix.ToImage<Bgr, byte>();
        }
        public MyFrame(Image<Bgr, byte> img)
        {
            _img = img;
            //_mat = _img.Mat;
        }
        public void Flip()
        {
            if ( _img != null)
            {
                //CvInvoke.cvFlip(_mat, _mat, Emgu.CV.CvEnum.FLIP.HORIZONTAL);
                //_img = _img.cvFlip(Emgu.CV.CvEnum.FLIP.HORIZONTAL);
                //CvInvoke.Flip(_mat, _mat, Emgu.CV.CvEnum.FlipType.Horizontal);
                _img = _img.Flip(Emgu.CV.CvEnum.FlipType.Horizontal);
            }
        }
        public Image<Bgr, byte> Image
        {
            get { return _img; }
            set
            {
                _img = value;
                //_mat = _img.Mat;
            }

        }
        public Mat Mat
        {
            get
            {
                if (_img == null)
                    return null;
                return _img.Mat;
            }
            set
            {
                if(value!= null)
                {
                    _img = value.ToImage<Bgr, byte>();
                }
            }
        }

    }
}
