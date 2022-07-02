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
    
    internal class Filter_ImageSubstraction : Filter
    {

        public static event EventHandler<DebugImageEventArgs> Debug;

        Image<Gray, byte> _background;
        
        public Filter_ImageSubstraction()
        {
            //_background;
        }
        public Filter_ImageSubstraction(MyFrame backGround)
        {
            _background = backGround.Image.Convert<Gray, byte>();
            
        }
        public override void UpdateInput(MyFrame raw)
        {
            if (raw == null || raw.Image == null)
                return;
            if(_background== null)
            {//first image would be the background.
                this.Background = raw.Image.Convert<Gray, byte>();
                ImageViewer temp = new ImageViewer();
                temp.Image = _background;
            }
            else
            {//updating current frame.
                base.UpdateInput(raw);
            }
            
        }
        public Image<Gray, byte> Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = MyBlur(value);
            }
        }
        public override Image<Gray, byte> Detect()
        {
            if(_Input==null)
                return null;

            Image<Gray, byte> _out=new Image<Gray, byte>(this._Input.Image.Width,this._Input.Image.Height);
            Image<Gray, byte> current = this._Input.Image.Convert<Gray, byte>();

            current =MyBlur(current);//reduce noise

            _out = _background.AbsDiff(current);//actual substraction

            _out = _out.ThresholdBinary(new Gray((double)ENUM_imageSubstraction.MIN_THRESHOLD_VAL), new Gray((double)ENUM_imageSubstraction.MAX_THRESHOLD_VAL));
            _out = _out.Dilate((int)ENUM_imageSubstraction.DILATE_VAL).Erode((int)ENUM_imageSubstraction.ERODE_VAL);//improve image

            ShootEvents(_out);//events
            return _out;
        }
        public void ShootEvents(IImage _out)
        {
            if (Debug != null)
            {
                Debug(null, new DebugImageEventArgs() { img = _out, name = "WINDOW_NAME_FILTER_SUBSTRACT" });
            }
        }
    }
}
