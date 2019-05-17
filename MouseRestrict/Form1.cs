using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseRestrict
{
    public partial class Form1 : Form
    {
        System.Threading.Thread t;
        public Form1()
        {
            InitializeComponent();
        }

        public void TrapMouse()
        {
            bool running = true;

            var settingsfiletest = new SettingsClass();
            while (running)
            {
                //Testing xml loading.
                settingsfiletest.load();


                // Variables for top-left corner.
                int x1 = settingsfiletest._settings.topLeftX;
                int y1 = settingsfiletest._settings.topLeftY;
                // Variables for bottom-right corner.
                int x2 = settingsfiletest._settings.bottomRightX; 
                int y2 = settingsfiletest._settings.bottomRightY;

                if (Cursor.Position.X < x1)
                {
                    Cursor.Position = new Point(x1 + 1, Cursor.Position.Y);
                }
                if (Cursor.Position.Y < y1)
                {
                    Cursor.Position = new Point(Cursor.Position.X, y1 + 1);
                }

                if (Cursor.Position.X > x2)
                {
                    Cursor.Position = new Point(x2 - 1, Cursor.Position.Y);
                }
                if (Cursor.Position.Y > y2)
                {
                    Cursor.Position = new Point(Cursor.Position.X, y2 - 1);
                }
            }
        }

        // Button click Event.
        private void button1_Click(object sender, EventArgs e)
        {
            //Check settings have been set.
            var settingsfiletest = new SettingsClass();
            bool exists = settingsfiletest.exists();

            if (exists)
            {
                // Visual identifiers.
                Flag.Text = "Trap is Running";
                button1.Enabled = false;
                button1.Visible = false;
                button2.Enabled = true;
                button2.Visible = true;
                setTrapProfile.Enabled = false;

                t = new System.Threading.Thread(TrapMouse);
                t.IsBackground = true;
                t.Start();
            }
            else
            {
                MessageBox.Show("Please set trap parameters.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Flag.Text = "Trap is Not Running";
            button1.Enabled = true;
            button1.Visible = true;
            button2.Enabled = false;
            button2.Visible = false;
            setTrapProfile.Enabled = true;
            t.Abort();
            while (t.IsAlive)
            {

            }
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            // Give your threads time to exit.
            t.Abort();
            while (t.IsAlive)
            {
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Called to specify the area to trap the mouse within.
        Form2 secondform = new Form2();
        private void setTrapProfile_Click(object sender, EventArgs e)
        {
            // TODO! OPEN NEW FORM, FULL SCREEN, ALLOW FOR DRAWING AREA TO TRAP.
            secondform.Show();
        }
    }
    
}
