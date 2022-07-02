using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Text;
using Emgu;
using DirectShowLib;
using Emgu.CV.UI;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.Structure;
namespace Gestures
{
    internal class FrameShotEventArgs : EventArgs
    {
        public MyFrame frame { get; set; }
    }
    internal class DebugImageEventArgs : EventArgs
    {
        public IImage img { get; set; }
        public string name { get; set; }

    }
    internal class Camera
    {

        private MyFrame _frame;
        private Capture _capture;
        //-------------- Settings -------------//
        private double _setting_brightness;
        private double _setting_contrast;
        private double _setting_sharpness;
        private double _setting_exposure;
        //-------------------------------------//
        private Thread _grabbingThread;

        

        public static event EventHandler<FrameShotEventArgs> FrameWasShot;
        public static event EventHandler<DebugImageEventArgs> Debug;
        public bool IsOn { get; private set; }
        private int _camNum;
        public Camera(int camNum, int maxCams,int width,int height,bool debug=false)
        {
            if(camNum<0|| camNum>=maxCams)
            {//illegal cam id
                IsOn = false;
                throw new Exception();
            }
            _camNum = camNum;

            _setting_sharpness = -1;
            _setting_contrast = -1;
            _setting_brightness = -1;
            _setting_exposure = -1;
            _frame = new MyFrame();
            _capture = new Capture(camNum);
            Thread.Sleep(4000);//letting the camera time to adjust settings
            _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, width);
            _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, height);
            _grabbingThread = new Thread(new ThreadStart(Threaded_GrabbingFunc));
        }
        private void LoadSettings()
        {
            if (_capture == null)
                return;
            if (_setting_sharpness != _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Sharpness))
            {
                _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Sharpness, _setting_sharpness);
            }
            if (_setting_contrast != _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Contrast))
            {
                _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Contrast, _setting_contrast);
            }
            if (_setting_brightness != _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Brightness))
            {
                _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Brightness, _setting_brightness);
            }
            if(_setting_exposure!= _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Exposure))
            {
                _capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Exposure, _setting_exposure);

            }
        }
        private void SaveSettings()
        {

            if(_capture!= null)
            {
                _setting_brightness = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Brightness);
                _setting_contrast = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Contrast);
                _setting_sharpness = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Sharpness);
                _setting_exposure = _capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Exposure);
            }
            
        }
        private void Threaded_GrabbingFunc()
        {
            while (IsOn)
            {
                TakeShot();
                ShootEvents();
            }
            this._capture.Dispose();
            
        }
        public void ShootEvents()
        {
            if (_frame != null)
            {
                if (FrameWasShot != null)
                {
                    FrameWasShot(this, new FrameShotEventArgs() { frame = _frame });

                }
                if (Debug != null)
                {
                    Debug(this, new DebugImageEventArgs() { img = _frame.Image, name = "camera" });

                }
            }
        }

        public void Start()
        {
            if (this._grabbingThread != null&&this._grabbingThread.ThreadState==ThreadState.Unstarted)
            {
                IsOn = true;
                this._grabbingThread.Start();
            }
        }
        public void Stop()
        {
            this._capture.Stop();
            //this._capture.Dispose();
            IsOn = false;
        }
        private bool SettingsAreEmpty()
        {
            return _setting_sharpness == -1 && _setting_contrast == -1 && _setting_brightness == -1&&_setting_exposure==-1;
        }

        private void Flip()
        {
            if(_frame!= null)
                _frame.Flip();
        }
        private void TakeShot(bool freezeSettings = true)
        {
            if (!IsOn)
            {
                return;
            }
            if (freezeSettings)
            {
                this.LoadSettings();
            }
            Mat temp = new Mat();

            if (_capture.Grab())
            {
                _capture.Retrieve(temp);
                _frame.Mat = temp;
            }
            if (freezeSettings && this.SettingsAreEmpty())
            {
                this.SaveSettings();
            }
            Flip();
        }


        ~Camera()
        {
            //if(_camOn)
            //{
            //    _capture.Dispose();
            //}
        }

    }
}
