using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class myPlayer : UserControl
    {
        string _imgLeft;
        string _imgRight;
        string _imgUp;
        string _imgDown;
        public int LeftBorder { get; set; }
        public int RightBorder { get; set; }
        public int UpBorder { get; set; }
        public int DownBorder { get; set; }

        public int Speed { get; set; }
        private string direction;
        public string Direction
        {
            get
            {
                return direction;
            }
            set
            {

                switch(value)
                {
                    case "Left":
                    case "Right":
                    case "Up":
                    case "Down":
                        direction = value;
                        pbImage.Image = Image.FromFile(@"..\..\Images\Car"+ Direction + ".png");

                        pbImage.SizeMode = PictureBoxSizeMode.CenterImage;

                        break;
                }
            }
        }
        public myPlayer(string imgLeft,string imgRight,string imgUp,string imgDown,int leftBord,int rightBord,int upBord,int downBord,int speed=4)
        {
            _imgLeft = imgLeft;
            _imgRight = imgRight;
            _imgUp = imgUp;
            _imgDown = imgDown;
            Speed = speed;
            pbImage = new PictureBox();
            LeftBorder = leftBord;
            RightBorder = rightBord;
            UpBorder = upBord;
            DownBorder = downBord;
            this.Controls.Add(pbImage);
            InitializeComponent();

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public void MyMove()
        {
            switch (Direction)
            {
                case "Up":
                    if(this.UpBorder<this.Top-Speed)
                        this.Top -= this.Speed;
                    break;
                case "Down":
                    if (this.DownBorder > this.Bottom+Speed)
                        this.Top += this.Speed;
                    break;
                case "Right":
                    if (this.RightBorder > this.Right+Speed)
                        this.Left += this.Speed;
                    break;
                case "Left":
                    if (this.LeftBorder < this.Left-Speed)
                        this.Left -= this.Speed;
                    break;
            }
        }

        private void pbImage_Click(object sender, EventArgs e)
        {

        }
    }
}
