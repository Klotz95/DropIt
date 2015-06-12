using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DropIt
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        //Attribute
        string currentSeletedItem = "";
        string[,] FileList;
        private void MainMenu_Load(object sender, EventArgs e)
        {
            //create debug inforamtion
            FileList = new string[5, 3];
            FileList[0, 0] = "WordDokument";
            FileList[0, 1] = "word";
            FileList[0, 2] = "text";
            FileList[1, 0] = "HansDampf";
            FileList[1, 1] = "jpg";
            FileList[1, 2] = "picture";
            FileList[2, 0] = "Hausarbeit";
            FileList[2, 1] = "pdf";
            FileList[2, 2] = "pdf";
            FileList[3, 0] = "Test";
            FileList[3, 1] = "mp3";
            FileList[3, 2] = "ndf";
            FileList[4, 0] = "Test";
            FileList[4, 1] = "avi";
            FileList[4, 2] = "video";
            //setup the Panels
            //Panel1
            panel1.Location = new Point(0,0);
            panel1.Height = this.ClientSize.Height - 150;
            panel1.Width = this.ClientSize.Width/2;
            panel1.AutoScroll = true;
            panel1.VerticalScroll.Enabled = false;
            //draw the files
            drawFiles();

            //Panel2
            panel2.Location = new Point(panel1.Width,0);
            panel2.Height = this.ClientSize.Height - 150;
            panel2.Width = this.ClientSize.Width/2;
            //create the no item selected Label
            Label NoItem = new Label();
            NoItem.Text = "No Item selected";
            NoItem.Location = new Point((panel2.Width / 2) - 50, panel2.Height / 2);
            NoItem.Width += 100;
            panel2.Controls.Add(NoItem);
            //Panel3
            panel3.Location = new Point(0,panel1.Height);
            panel3.Width = this.ClientSize.Width;
            panel3.Height = this.ClientSize.Height - panel2.Height;
            //setup the icons
            //add File
            PictureBox add = new PictureBox();
            add.Image = Image.FromFile("Ressources/1434025104_21_Cloud_Add.png");
            add.SizeMode = PictureBoxSizeMode.StretchImage;
            add.Height = 100;
            add.Width = 100;
            add.Location = new Point(panel3.Width/4 - panel3.Width/4/2 - add.Width/2,0);
            panel3.Controls.Add(add);
            //label for add
            PictureBox addlabel = new PictureBox();
            Bitmap addText = new Bitmap(40, 30);
            Graphics g = Graphics.FromImage(addText);
            g.DrawString("add", DefaultFont, Brushes.Black, new Point(0, 0));
            addlabel.Location = new Point(add.Location.X + addText.Width/2 + 5,add.Height + 3);
            addlabel.Image = addText;
            addlabel.Width = addText.Width;
            addlabel.Height = addText.Height;
            panel3.Controls.Add(addlabel);
            //download File
            PictureBox downLoad = new PictureBox();
            downLoad.Image = Image.FromFile("Ressources/1434025203_icon-129-cloud-download.png");
            downLoad.SizeMode = PictureBoxSizeMode.StretchImage;
            downLoad.Height = 100;
            downLoad.Width = 100;
            downLoad.Location = new Point(panel3.Width / 2 - panel3.Width /8 - downLoad.Width/2, 0);
            panel3.Controls.Add(downLoad);
            //label for downLoad
            PictureBox DownLoadLabel = new PictureBox();
            Bitmap DownloadText = new Bitmap(100,30);
            g = Graphics.FromImage(DownloadText);
            g.DrawString("download",DefaultFont,Brushes.Black, new Point(0,0));
            DownLoadLabel.Location = new Point(downLoad.Location.X - DownloadText.Width/2 + 50 , downLoad.Height + 3);
            DownLoadLabel.Image = DownloadText;
            DownLoadLabel.Width = DownloadText.Width;
            DownLoadLabel.Height = DownloadText.Height;
            panel3.Controls.Add(DownLoadLabel);
            //open File
            PictureBox Open = new PictureBox();
            Open.Image = Image.FromFile("Ressources/1434025180_icon-56-document-text.png");
            Open.SizeMode = PictureBoxSizeMode.StretchImage;
            Open.Height = 100;
            Open.Width = 100;
            Open.Location = new Point((panel3.Width * 3/4) - panel3.Width/8 - Open.Width/2,0);
            panel3.Controls.Add(Open);
            //label for open
            PictureBox OpenLabel = new PictureBox();
            Bitmap OpenText = new Bitmap(60,30);
            g = Graphics.FromImage(OpenText);
            g.DrawString("Open",DefaultFont,Brushes.Black,new Point(0,0));
            OpenLabel.Location = new Point(Open.Location.X + 20, Open.Height + 3);
            OpenLabel.Image = OpenText;
            OpenLabel.Width = OpenText.Width;
            OpenLabel.Height = OpenText.Height;
            panel3.Controls.Add(OpenLabel);
            //Logout
            PictureBox Logout = new PictureBox();
            Logout.Image = Image.FromFile("Ressources/1434025251_setting.png");
            Logout.SizeMode = PictureBoxSizeMode.StretchImage;
            Logout.Height = 100;
            Logout.Width = 100;
            Logout.Location = new Point(panel3.Width - panel3.Width/8 - Logout.Width/2,0);
            panel3.Controls.Add(Logout);
            //Label for Logout
            PictureBox LogoutLabel = new PictureBox();
            Bitmap LogoutText = new Bitmap(100,30);
            g = Graphics.FromImage(LogoutText);
            g.DrawString("Logout",DefaultFont,Brushes.Black,new Point(0,0));
            LogoutLabel.Location = new Point(Logout.Location.X + LogoutText.Width/2 - 30, Logout.Height + 3);
            LogoutLabel.Image = LogoutText;
            LogoutLabel.Width = LogoutText.Width;
            LogoutLabel.Height = LogoutText.Height;
            panel3.Controls.Add(LogoutLabel);
            //set minimum Size
            this.MinimumSize = this.Size;
        }

        private void MainMenu_ClientSizeChanged(object sender, EventArgs e)
        {
            //resize the panels
            panel1.Height = this.ClientSize.Height - 150;
            //Panel2
            panel2.Location = new Point(panel1.Width, 0);
            panel2.Height = this.ClientSize.Height - 150;
            panel2.Width = this.ClientSize.Width - panel1.Width;
            //Panel3
            panel3.Location = new Point(0, panel1.Height);
            panel3.Width = this.ClientSize.Width;
            panel3.Height = this.ClientSize.Height - panel2.Height;
            //relaocte the content of panel 3
            panel3.Controls[0].Location = new Point(panel3.Width / 4 - panel3.Width / 4 / 2 - panel3.Controls[0].Width / 2, 0);
            panel3.Controls[1].Location = new Point(panel3.Controls[0].Location.X - panel3.Controls[1].Width / 2 + 50, panel3.Controls[0].Height + 3);
            panel3.Controls[2].Location = new Point(panel3.Width / 2 - panel3.Width / 8 - panel3.Controls[2].Width / 2, 0);
            panel3.Controls[3].Location = new Point(panel3.Controls[2].Location.X - panel3.Controls[3].Width / 2 + 50, panel3.Controls[2].Height + 3);
            panel3.Controls[4].Location = new Point((panel3.Width * 3 / 4) - panel3.Width / 8 - panel3.Controls[4].Width / 2, 0);
            panel3.Controls[5].Location = new Point(panel3.Controls[4].Location.X + 20, panel3.Controls[4].Height + 3);
            panel3.Controls[6].Location = new Point(panel3.Width - panel3.Width / 8 - panel3.Controls[6].Width / 2, 0);
            panel3.Controls[7].Location = new Point(panel3.Controls[6].Location.X + panel3.Controls[7].Width / 2 - 30, panel3.Controls[6].Height + 3);
            //redraw the content of panel 2
            if (currentSeletedItem == "")
            {
                //relocate the "no item selected label"
                panel2.Controls[0].Location = new Point((panel2.Width / 2) - 50, panel2.Height / 2);
            }
        }
        private void drawFiles()
        {
          int currentHeight = 0;

          for(int i = 0; i < FileList.Length/3; i++)
          {
            //create an empty panel
            PictureBox currentPanel = new PictureBox();
            Bitmap pic = new Bitmap(panel1.Width,100);
            Graphics g = Graphics.FromImage(pic);
            if (currentSeletedItem == "" || i != Convert.ToInt32(currentSeletedItem))
            {
                g.FillRectangle(Brushes.LightGray, new Rectangle(new Point(0, 0), new Size(pic.Width, 100)));
            }
            else
            {
                g.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), new Size(pic.Width, 100)));
            }
            g.DrawString(FileList[i,0] + "." + FileList[i,1],DefaultFont, Brushes.Black,new Point(90,45));
            //check the kind of the file
            if(FileList[i,2] == "picture")
            {
              Image Kind = Image.FromFile("Ressources/1434025007_icon-65-document-image.png");
              g.DrawImage(Kind,0,10,80,80);
            }
            else if(FileList[i,2] == "video")
            {
              Image Kind = Image.FromFile("Ressources/1434025023_icon-64-document-movie.png");
              g.DrawImage(Kind,0,10,80,80);
            }
            else if(FileList[i,2] == "text")
            {
              Image Kind = Image.FromFile("Ressources/1434024890_icon-113-document-file-txt.png");
              g.DrawImage(Kind,0,10,80,80);
            }
            else if(FileList[i,2] == "pdf")
            {
              Image Kind = Image.FromFile("Ressources/1434024880_icon-70-document-file-pdf.png");
              g.DrawImage(Kind,0,10,80,80);
            }
            else if(FileList[i,2] == "ndf")
            {
              Image Kind = Image.FromFile("Ressources/1434024967_icon-101-document-file-dat.png");
              g.DrawImage(Kind,0,10,80,80);
            }
            currentPanel.Image = pic;
            currentPanel.Width = pic.Width;
            currentPanel.Height = pic.Height;
            currentPanel.Location = new Point(0, currentHeight);
            currentHeight += 110;
            panel1.Controls.Add(currentPanel);
          }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            panel1.Focus();
        }
    }
}
