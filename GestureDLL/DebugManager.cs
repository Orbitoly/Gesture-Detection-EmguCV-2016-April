using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using Emgu;
using Emgu.CV.UI;
using Emgu.CV;
using System.Drawing;
using Emgu.CV.Structure;
namespace Gestures
{
    internal class DebugManager 
    {
        /*
            This class controls the Debug Windows.
            currently, windows are being added manually.

                    * CONSTS *
            --------------------------
            - WINDOW_NAME_CAMERA 
            - WINDOW_NAME_CONTOURS 
            - WINDOW_NAME_FILTER_SKIN 
            - WINDOW_NAME_FILTER_SUBSTRACT
            - WINDOW_NAME_FILTER_FINAL

            ---------------------------- 

        */
        List<DebugWindow> _debugList;
        bool _debug;
        public DebugManager(bool debug=true)
        {
            if (debug == false)
                return;
            //--------------------- Init --------------------//
            _debug = debug;
            _debugList = new List<DebugWindow>();

            //------------------- Camera --------------------//
            DebugWindow tempWindow = new DebugWindow("CAMERA_WINDOW");
            Camera.Debug += tempWindow.OnDebugEvent;
            _debugList.Add(tempWindow);

            //------------------ Contours -------------------//
            //tempWindow = new DebugWindow("CONTOURS_WINDOW");
            //HandPointRecognizer.Debug += tempWindow.OnDebugEvent;
            //_debugList.Add(tempWindow);

            //------------------- Skin ---------------------//
            tempWindow = new DebugWindow("FILTER_SKIN_WINDOW");
            Filter_SkinColor.Debug += tempWindow.OnDebugEvent;
            _debugList.Add(tempWindow);

            //-------------- Filter Substract -------------//
            tempWindow = new DebugWindow("FILTER_SUBSTRACT_WINDOW");
            Filter_ImageSubstraction.Debug += tempWindow.OnDebugEvent;
            _debugList.Add(tempWindow);

            //---------------- Filter Final ---------------//
            tempWindow = new DebugWindow("FILTER_FINAL_WINDOW");
            FilterManager.Debug += tempWindow.OnDebugEvent;
            _debugList.Add(tempWindow);
            //------------------ Contours -----------------//
            tempWindow = new DebugWindow("GESTURES_WINDOW");
            GestureRecognizer.Debug += tempWindow.OnDebugEvent;
            _debugList.Add(tempWindow);
            //---------------------------------------------//
        }


        private void Win_Closing(object sender, EventArgs e)
        {
            DebugWindow temp = sender as DebugWindow;
            lock (_debugList)
            {
                //----------------- Unsubscribe ------------------//
                FilterManager.Debug -= temp.OnDebugEvent;
                HandPointRecognizer.Debug -= temp.OnDebugEvent;
                FilterManager.Debug -= temp.OnDebugEvent;
                Camera.Debug -= temp.OnDebugEvent;
                //------------------------------------------------//

                _debugList.Remove(temp);
                
            }
        }
    }
}
