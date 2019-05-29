using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MouseRestrict
{
    public partial class Form1 : Form
    {
        System.Threading.Thread t;
        public Form1()
        {
            InitializeComponent();
        }

        public void TrapMouse(int x1, int y1, int x2, int y2)
        {
            bool running = true;

            var settingsfiletest = new SettingsClass();
            while (running)
            {

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


        /// <summary>
        /// Process monitoring
        /// </summary>
        /// Views the currently running processes to automatically trap based on running programs.
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void monitorProcess()
        {
            Process[] listOfProcesses = Process.GetProcesses();
            foreach (Process theprocess in listOfProcesses)
            {
                Console.WriteLine(theprocess.ProcessName);
                if (theprocess.ProcessName == "Vampire")
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
                        if (firstRun)
                        {
                            var settingsfiletoload = new SettingsClass();
                            settingsfiletoload.load();
                            x1 = settingsfiletoload._settings.topLeftX;
                            y1 = settingsfiletoload._settings.topLeftY;
                            x2 = settingsfiletoload._settings.bottomRightX;
                            y2 = settingsfiletoload._settings.bottomRightY;
                            firstRun = false;
                        }
                        t = new System.Threading.Thread(() => TrapMouse(x1, y1, x2, y2));
                        t.IsBackground = true;
                        t.Start();
                    }
                    else
                    {
                        
                    }
                }
            }
        }

        bool firstRun = true;
        public static int x1;
        public static int y1;
        public static int x2;
        public static int y2;
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
                if (firstRun)
                {
                    var settingsfiletoload = new SettingsClass();
                    settingsfiletoload.load();
                    x1 = settingsfiletoload._settings.topLeftX;
                    y1 = settingsfiletoload._settings.topLeftY;
                    x2 = settingsfiletoload._settings.bottomRightX;
                    y2 = settingsfiletoload._settings.bottomRightY;
                    firstRun = false;
                }
                t = new System.Threading.Thread(() => TrapMouse(x1, y1, x2, y2));
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
            var settingsfiletest = new SettingsClass();
            settingsfiletest.Save();
            settingsfiletest.Update(x1, y1, x2, y2);
            settingsfiletest.Save();
            t.Abort();
            while (t.IsAlive)
            {
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                mouseNotifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            mouseNotifyIcon.Visible = false;
        }

        // Called to specify the area to trap the mouse within.
        Form2 secondform = new Form2();
        private void setTrapProfile_Click(object sender, EventArgs e)
        {
            // TODO! OPEN NEW FORM, FULL SCREEN, ALLOW FOR DRAWING AREA TO TRAP.
            secondform.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            monitorProcess();
        }
    }
    
}
