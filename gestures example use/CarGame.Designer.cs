namespace WindowsFormsApplication1
{
    partial class CarGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrMove = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrMove
            // 
            this.tmrMove.Interval = 10;
            this.tmrMove.Tick += new System.EventHandler(this.tmrMove_Tick);
            // 
            // CarGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 613);
            this.Name = "CarGame";
            this.Text = "CarGame";
            this.Load += new System.EventHandler(this.CarGame_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CarGame_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CarGame_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrMove;
    }
}