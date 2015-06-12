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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Write an E-Mail to the admin to create a new account.\n\nE-Mail Address: NicoKotlenga@live.de", "Create New Account");
        }
        private bool checkIpValidation(string IPAddress)
        {
            //Seperate dots
            string[] currentSeperated = new string[0];
            string current = "";
            for (int i = 0; i < IPAddress.Length; i++)
            {
                if (IPAddress[i] == '.')
                {
                    string[] backup = currentSeperated;
                    currentSeperated = new string[backup.Length + 1];
                    for (int k = 0; k < backup.Length; k++)
                    {
                        currentSeperated[k] = backup[k];
                    }
                    currentSeperated[backup.Length] = current;
                    current = "";
                }
                else
                {
                    current += IPAddress[i];
                }
                if (i == IPAddress.Length - 1)
                {
                    string[] backup = currentSeperated;
                    currentSeperated = new string[backup.Length + 1];
                    for (int k = 0; k < backup.Length; k++)
                    {
                        currentSeperated[k] = backup[k];
                    }
                    currentSeperated[backup.Length] = current;
                    current = "";
                }
            }
            //now check for mistakes
            if (currentSeperated.Length != 4)
            {
                return false;
            }
            else
            {
                //check the values
                for (int i = 0; i < currentSeperated.Length; i++)
                {
                    try
                    {
                        int currentValue = Convert.ToInt32(currentSeperated[i]);
                        if (currentValue > 255 || currentValue < 0)
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkIpValidation(textBox3.Text))
            {
                MessageBox.Show("The entered IP-Address isn't valid. Please check your insert.", "Error");
            }
            else
            {
                MainMenu mn = new MainMenu();
                this.Hide();
                mn.Show();
            }
        }
    }
}
