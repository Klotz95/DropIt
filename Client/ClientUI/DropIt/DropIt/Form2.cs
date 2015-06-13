using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DropIt
{
    public partial class DownloadForm : Form
    {
        public DownloadForm()
        {
            InitializeComponent();
        }
        private void DownloadForm_Load(object sender, EventArgs e)
        {
            PictureBox OnMove = new PictureBox();
            OnMove.Height = 100;
            OnMove.Width = 100;
            OnMove.Location = new Point(-100, 25);
            OnMove.Image = Image.FromFile("Ressources/1434214536_file_document_paper_green_g11822.png");
            panel1.Controls.Add(OnMove);
            PictureBox Key = new PictureBox();
            Key.Image = Image.FromFile("Ressources/1434214741_key.png");
            Key.Height = 50;
            Key.Width = 50;
            Key.SizeMode = PictureBoxSizeMode.StretchImage;
            Key.Location = new Point(panel1.Width/2 - Key.Width/2,panel1.Height + Key.Height);
            panel1.Controls.Add(Key);
            System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
            t1.Tick += new EventHandler(t1_Tick);
            t1.Interval = 10;
            t1.Start();
        }
        bool animation1 = true;
        bool animation2 = false;
        bool animation3 = false;
        void t1_Tick(object sender, EventArgs e)
        {
            if (animation1)
            {
                if (panel1.Controls[0].Location.X < panel1.Width/2 - panel1.Controls[0].Width/2 - 10)
                {
                    panel1.Controls[0].Location = new Point(panel1.Controls[0].Location.X + 1, 25);
                }
                else
                {
                    animation1 = false;
                    animation2 = true;
                }
            }
            else if (animation2)
            {
                //second animation 
                if (panel1.Controls[1].Location.Y > panel1.Height - panel1.Controls[1].Height)
                {
                    panel1.Controls[1].Location = new Point(panel1.Width / 2 - panel1.Controls[1].Width / 2, panel1.Controls[1].Location.Y - 1);
                }
                else
                {
                    animation2 = false;
                    animation3 = true;
                    PictureBox p1 = (PictureBox)panel1.Controls[0];
                    p1.Image = Image.FromFile("Ressources/1434214509_file_document_paper_green_g9959.png");
                   
                }
            }
            else if (animation3)
            {
                if (panel1.Controls[1].Location.Y < panel1.Height + panel1.Controls[1].Height)
                {
                    panel1.Controls[1].Location = new Point(panel1.Width / 2 - panel1.Controls[1].Width / 2, panel1.Controls[1].Location.Y + 1);
                }
                else
                {
                    if (panel1.Controls[0].Location.X < panel1.Width + panel1.Controls[0].Width)
                    {
                        panel1.Controls[0].Location = new Point(panel1.Controls[0].Location.X + 1, 25);
                    }
                    else
                    {
                        //set standarts and restart the animation
                        PictureBox p1 = (PictureBox)panel1.Controls[0];
                        p1.Image = Image.FromFile("Ressources/1434214536_file_document_paper_green_g11822.png");
                        panel1.Controls[0].Location = new Point(0 - panel1.Controls[0].Width, 25);
                        animation3 = false;
                        animation1 = true;
                    }
                }
            }
        }

        
    }
}
