namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.btnOpenCamera = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GestureText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnListen = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStartRecord = new System.Windows.Forms.Button();
            this.btnStopRecord = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenCamera
            // 
            this.btnOpenCamera.Location = new System.Drawing.Point(627, 61);
            this.btnOpenCamera.Name = "btnOpenCamera";
            this.btnOpenCamera.Size = new System.Drawing.Size(109, 23);
            this.btnOpenCamera.TabIndex = 0;
            this.btnOpenCamera.Text = "Init Camera";
            this.btnOpenCamera.UseVisualStyleBackColor = true;
            this.btnOpenCamera.Click += new System.EventHandler(this.btnOpenCamera_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleName = "gvData";
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GestureText});
            this.dataGridView1.Location = new System.Drawing.Point(68, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 150);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // GestureText
            // 
            this.GestureText.HeaderText = "Name";
            this.GestureText.Name = "GestureText";
            this.GestureText.ReadOnly = true;
            // 
            // btnListen
            // 
            this.btnListen.Enabled = false;
            this.btnListen.Location = new System.Drawing.Point(627, 110);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(109, 23);
            this.btnListen.TabIndex = 2;
            this.btnListen.Text = "Listen";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // btnExit
            // 
            this.btnExit.Enabled = false;
            this.btnExit.Location = new System.Drawing.Point(627, 296);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(109, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Stop Camera";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStartRecord
            // 
            this.btnStartRecord.Enabled = false;
            this.btnStartRecord.Location = new System.Drawing.Point(627, 167);
            this.btnStartRecord.Name = "btnStartRecord";
            this.btnStartRecord.Size = new System.Drawing.Size(109, 23);
            this.btnStartRecord.TabIndex = 6;
            this.btnStartRecord.Text = "Record";
            this.btnStartRecord.UseVisualStyleBackColor = true;
            this.btnStartRecord.Click += new System.EventHandler(this.btnStartRecord_Click);
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.Enabled = false;
            this.btnStopRecord.Location = new System.Drawing.Point(627, 230);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(109, 23);
            this.btnStopRecord.TabIndex = 7;
            this.btnStopRecord.Text = "Stop recording";
            this.btnStopRecord.UseVisualStyleBackColor = true;
            this.btnStopRecord.Click += new System.EventHandler(this.btnStopRecord_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 416);
            this.Controls.Add(this.btnStopRecord);
            this.Controls.Add(this.btnStartRecord);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnListen);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnOpenCamera);
            this.Name = "Form1";
            this.Text = "Trying Gestures";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpenCamera;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.DataGridViewTextBoxColumn GestureText;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnStartRecord;
        private System.Windows.Forms.Button btnStopRecord;
    }
}

