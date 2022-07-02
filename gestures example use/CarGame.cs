using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class CarGame : Form
    {
        static myPlayer player;
        public CarGame()
        {
            InitializeComponent();
            player = new myPlayer("", "", "", "",this.Left,this.Right,this.Top,this.Bottom,5);
            //player.Direction = "right";
            this.Controls.Add(player);
            
            tmrMove.Start();
            //player.Show();
        }

        private void tmrMove_Tick(object sender, EventArgs e)
        {
            if(player!= null)
            {
                player.MyMove();
            }
        }

        private void CarGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (player != null)
            {
                if (e.KeyCode == Keys.Up)
                    player.Direction = "up";
                if (e.KeyCode == Keys.Down)
                    player.Direction = "down";
                if (e.KeyCode == Keys.Left)
                    player.Direction = "left";
                if (e.KeyCode == Keys.Right)
                    player.Direction = "right";
            }

        }

        private void CarGame_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        public static void ChangeDirection(string direction)
        {
            if (player != null)
            {
                player.Direction = direction;

            }

        }
        private void btnUp_Click(object sender, EventArgs e)
        {
            player.Direction = "Up";

        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            player.Direction = "Down";

        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            player.Direction = "Right";

        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            player.Direction = "Left";

        }

        private void CarGame_Load(object sender, EventArgs e)
        {

        }
    }
}
