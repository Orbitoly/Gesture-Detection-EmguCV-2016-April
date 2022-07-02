using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gestures;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        static int countGests;
        static Random rnd = new Random();
        List<string> myList;
        List<Gesture> miGe;
        
        public Commands _cam;
        public Form1()
        {
            InitializeComponent();
            myList = new List<string>();
            CarGame game = new CarGame();
            game.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            countGests = 0;
            miGe = new List<Gesture>();
            _cam = new Commands(CallMeMaybe,true, miGe,60);

        }
        private void CallMeMaybe(Gesture gest)
        {

            CarGame.ChangeDirection(gest.Name);

            //----------------------- Grid View --------------------------//
            if(dataGridView1!=null)
            {
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new Action(() => { dataGridView1.Rows.Add(gest.Name); }));
                }
                else
                {
                    dataGridView1.Rows.Add(gest.ToString());
                }
            }
            //-----------------------------------------------------------//



        }
        private void btnOpenCamera_Click(object sender, EventArgs e)
        {
            if(_cam.CameraCapture(_cam.GetNumOfConnectedCameras()-1))
            {
                btnListen.Enabled = true;
                btnStartRecord.Enabled = true;
                btnExit.Enabled = true;
                btnOpenCamera.Enabled = false;

            }
        }

        private void btnListen_Click(object sender, EventArgs e)
        {

            _cam.Listen();
            btnOpenCamera.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_cam!= null)
                _cam.StopListening();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (_cam != null)
            {
                _cam.StopListening();
                btnOpenCamera.Enabled = true;
                btnListen.Enabled = false;
                btnStartRecord.Enabled = false;
                btnStopRecord.Enabled = false;
                btnExit.Enabled = false;
            }
                
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }

        private void btnStartRecord_Click(object sender, EventArgs e)
        {
            if(_cam!= null)
            {
                _cam.StartRecording();
                btnListen.Enabled = false;
                btnOpenCamera.Enabled = false;
                btnExit.Enabled = false;
                btnStartRecord.Enabled = false;
                btnStopRecord.Enabled = true;
            }
        }

        private void btnStopRecord_Click(object sender, EventArgs e)
        {
            string gestName = "";
            if (_cam != null)
            {
                Gesture tempGest = _cam.StopRecording();
                switch (countGests)
                {
                    case 0:
                        gestName = "Up";
                        break;
                    case 1:
                        gestName = "Down";
                        break;
                    case 2:
                        gestName = "Left";
                        break;
                    case 3:
                        gestName = "Right";
                        break;
                }
                countGests++;
                _cam.AddGesture(tempGest, gestName);
                btnStartRecord.Enabled = true;
                btnListen.Enabled = true;
                btnExit.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
