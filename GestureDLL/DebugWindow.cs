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
using System.Threading;
namespace Gestures
{
    internal class DebugWindow
    {
        /*
        *   DebugWindow class aimed to show the user/developer what is being proccessed in background.
        *   It opens imageviewer in a different thread and shows a new image whenever an event of "Debug" is published.
        *   
        */
        public event EventHandler DebugWindowClosing;
        private ImageViewer _window;
        private Thread _dialogThread;
        private bool _thread_neverStarted;
        public string _name { get; }
        //------------- Ctor --------------//
        public DebugWindow(string name)
        {
            _name = name;
            //------------- Window --------------//
            _window = new ImageViewer();
            _window.Text = name;
            _window.FormClosing += OnCloseEvent;
            //------------- Thread --------------//
            _dialogThread = new Thread(new ThreadStart(Show));
            _thread_neverStarted = true;
            //-----------------------------------//

        }
        public DebugWindow(string name,IImage img)
        {
            _name = name;
            //------------- Window --------------//
            _window = new ImageViewer(img);
            _window.Text = name;
            _window.Image = img;
            //------------- Events --------------//
            _window.FormClosing += OnCloseEvent;
            //------------- Thread --------------//
            _dialogThread = new Thread(new ThreadStart(Show));
            _thread_neverStarted = true;
            //-----------------------------------//

        }
        //---------- Thread Func ----------//
        private void Show()
        {
            this._window.ShowDialog();
        }
        //----------- Handlers ------------//
        public void OnCloseEvent(object sender, EventArgs e)
        {
            if(DebugWindowClosing!= null)
            {//checking for subscribers, informing that the window is closing.
                DebugWindowClosing(this, EventArgs.Empty);
            }
        }
        public void OnDebugEvent(object sender, DebugImageEventArgs e)
        {
            if(_thread_neverStarted)
            {
                _dialogThread.Start();
                _thread_neverStarted = false;
            }
            try
            {
                this._window.Image = e.img;
            }
            catch (Exception)
            {//aborted.

            }
        }


        public bool IsAlive
        {
            get
            {
                return this._window != null && !this._window.IsDisposed;
            }
        }
    }
}
