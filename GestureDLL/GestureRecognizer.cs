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
    public delegate void CallBack(Gesture gest);

    internal class GestureRecognizer
    {
        //--------------------- Indicators ----------------------//
        private bool _working;//only one recognize task in thread pool.
        private bool _record;
        public bool Record { get { return _record; } private set { if (_record != value) { _history.Clear(); };_record = value; } }//when changing to record mode,clear history.
        //------------- Save History -----------//
        static Queue<MyPoint> _history;
        int _historyMaxLength;
        //-------------- Recognize -------------//
        static List<Gesture> _savedGestures;
        int _accuracy;
        //------------ CallBack User -----------//
        static CallBack _callRecognizedFunction;
        //-------------- Debugging -------------//
        public static event EventHandler<DebugImageEventArgs> Debug;
        //--------------------------------------//

        public GestureRecognizer(CallBack callRecognizedFunction, List<Gesture> savedGestures =null,int accuracy=40,int historyMaxLength=50)
        {

            
            _callRecognizedFunction = callRecognizedFunction;
            if(historyMaxLength<1)
            {
                _historyMaxLength = 1;
            }
            this._historyMaxLength = historyMaxLength;
            _history = new Queue<MyPoint>();
            _accuracy = accuracy;
            _savedGestures = savedGestures;
            Record = false;
            _working = false;
            if(_savedGestures == null||_savedGestures.Count==0)
            {
                //Add my random Gestures.
                //_savedGestures = new List<Gesture>();
                //Gesture tempGest = new Gesture("Gest1", new List<MyPoint>() { new MyPoint(100, 100), new MyPoint(160, 180) });
                //_savedGestures.Add(tempGest);
                //Gesture tempGest2 = new Gesture("Gest2", new List<MyPoint>() { new MyPoint(260, 200), new MyPoint(250, 210), new MyPoint(200, 290), new MyPoint(260, 300) });

                //_savedGestures.Add(tempGest2);
            }

            HandPointRecognizer.HandCenterPointWasDetected += OnHandCenterPointWasDetected;
        }
        public void UpdateHistory(MyPoint p)
        {
            if(_history!= null)
            {
                if(_history.Count==_historyMaxLength)
                {
                    _history.Dequeue();
                }
                _history.Enqueue(p);
            }
            

        }
        /*
        *Running in background and calling the user function when recognized a Gesture.
        */
        public void Recognize(Object sender)
        {
            while(_working)//waits until other task in the thread pool finishes
            { }
            _working = true;//This task is now going to work, changing to True so no one else can work also.
            //------------------------- WORK -----------------------------//
            #region
            MyPoint center = sender as MyPoint;
            if (center == null)
            {
                return;
            }
            Gesture foundGest = SearchForGestureFromHistory();
            if (foundGest != null)
            {
                if (_callRecognizedFunction != null)
                {//Tell user we found a Gesture.
                    _callRecognizedFunction(foundGest);
                    _history.Clear();//found gesture, restarts the searching.
                }
            }
            #endregion
            //------------------------------------------------------------//
            _working = false;//other Recognize tasks can now work too.
        }
        public Gesture SearchForGestureFromHistory()
        {
            Queue<MyPoint> historyGest = new Queue<MyPoint>(_history);
            foreach (Gesture tempGest in _savedGestures)
            {
                if (tempGest.IsInRange(historyGest, _accuracy))
                {
                    return tempGest;
                }
            }
            return null;
        }
        public void AddToSavedGestures(Gesture gest)
        {
            if (_savedGestures == null)
                _savedGestures = new List<Gesture>();
            if(gest.Points.Count>0)
                _savedGestures.Add(gest);
        }

        public void OnHandCenterPointWasDetected(object sender,HandCenterPointDetectedEventArgs args)
        {
            UpdateHistory(args.center);
            if (Record)
            {

            }
            else
            {//Recognize
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Recognize), args.center);
            }

           
            if (Debug != null)
            {
                Debug(this, new DebugImageEventArgs() { img = CreateDebugImage(args.debuggerContour), name = "Gestures" });
            }
        }
        public void StartRecording()
        {
            Record = true;
        }
        public Gesture StopRecording()
        {
            if (!Record)//wasn't in record mode...
                return null;
            //-------------- Searching for similar gesture ---------------//
            Gesture similarGesture = SearchForGestureFromHistory();
            if(similarGesture!= null)//found a gesture from saved gestures, cant create new gesture.
            {
                Record = false;
                return null;
            }
            //------- Creates a new Gesture and stop the recording -------//
            List<MyPoint> tempPoints = new List<MyPoint>();
            tempPoints.AddRange(_history);
            Gesture userNewGesture = new Gesture(tempPoints);
            Record = false;
            //------------------------------------------------------------//

            return userNewGesture;
        }
        public Image<Bgr, byte> CreateDebugImage(Image<Bgr, byte> withConts)
        {
            foreach (Gesture tempGest in _savedGestures)
            {
                for (int i = 0; i < tempGest.Points.Count-1; i++)
                {
                    withConts.Draw(new LineSegment2D(tempGest.Points[i].ToDrawPoint(),tempGest.Points[i+1].ToDrawPoint()), tempGest.Color, _accuracy);

                }
                
            }
            if(_history.Count>1)
            {
                withConts.Draw(new LineSegment2D(_history.ElementAt(_history.Count - 2).ToDrawPoint(), _history.Last().ToDrawPoint()), new Bgr(Color.DeepPink), 2);
            }
            return withConts;
        }
    }
}
