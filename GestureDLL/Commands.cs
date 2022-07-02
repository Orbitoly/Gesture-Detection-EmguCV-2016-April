using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Emgu;
using DirectShowLib;
using Emgu.CV.UI;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.Structure;
namespace Gestures
{
    public class Commands
    {
        Camera myCam;
        DebugManager debugger;
        GestureRecognizer myRecognize;
        FilterManager myFilters;


        /// <summary>
        /// Initializes all other classes and sets their settings.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="debug"></param>
        /// <param name="savedGestures"></param>
        /// <param name="accuracy"></param>
        /// <param name="historyMaxLength"></param>
        public Commands(CallBack obj,bool debug=false, List<Gesture> savedGestures = null, int accuracy = 40, int historyMaxLength = 50)
        {
            myFilters = new FilterManager();
            myRecognize= new GestureRecognizer(obj,savedGestures,accuracy,historyMaxLength);
            debugger = new DebugManager(debug);
        }
        /// <summary>
        /// Find system’s connected cameras with DirectShow.Net 
        /// </summary>
        /// <returns></returns>
        public int GetNumOfConnectedCameras()
        {
            //-> Find systems cameras with DirectShow.Net dll
            DsDevice[] _SystemCamereas = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            if (_SystemCamereas == null)
                return 0;
            return _SystemCamereas.Count();
        }
        /// <summary>
        /// Request to open new Camera object with the sent camID.
        /// Returns a boolean which represents rather the camera opened successfully.
        /// </summary>
        /// <param name="camID"></param>
        public bool CameraCapture(int camID)
        {
            if (myCam!=null)//camera already in use.
                return false;
            try
            {
                myCam = new Camera(camID, GetNumOfConnectedCameras(), 1920, 1080);

            }
            catch (Exception)
            {//illeagel cam num.
                myCam = null;
                return false;
            }

            return true;
        }
        /// <summary>
        /// Starts recording a new Gesture.
        /// </summary>
        public void StartRecording()
        {
            if(myRecognize!= null&&myCam!=null&&myCam.IsOn)
            {
                if (!myRecognize.Record)
                    myRecognize.StartRecording();
            }
        }
        /// <summary>
        ///Stops the recording.
        ///Returns the new Gesture if it does not exist.
        /// </summary>
        /// <returns></returns>
        public Gesture StopRecording()
        {
            if(myRecognize!=null&&myRecognize.Record)
                return myRecognize.StopRecording();
            return null;
        }
        /// <summary>
        /// Adds a Gesture to the user’s given database
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="name"></param>
        public void AddGesture(Gesture temp,string name="")
        {
            if (temp == null)
                return;
            if(name!="")
            {
                temp.Name = name;
            }
            myRecognize.AddToSavedGestures(temp);
        }
        /// <summary>
        /// If camera is opened, starts to recognize gestures in background
        /// </summary>
        public void Listen()
        {
            if(myCam!=null && !myCam.IsOn)
            {
                myCam.Start();

            }
        }
        /// <summary>
        /// Stops listening and closes the camera.
        /// </summary>
        public void StopListening()
        {
            if(myCam!=null)
            {
                myCam.Stop();
            }
            myCam = null;
        }
    }
}
