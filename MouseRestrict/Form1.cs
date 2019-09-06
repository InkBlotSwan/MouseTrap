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
using System.Management;
using System.Text.RegularExpressions;

namespace MouseRestrict
{
    public partial class Form1 : Form
    {
        System.Threading.Thread t;
        System.Threading.Thread b;
        public bool settingsHaveChanged = false;
        public Form1()
        {
            InitializeComponent();
            var settingsfiletest = new SettingsClass();
            bool exists = settingsfiletest.exists();
            if (exists)
            {
                settingsfiletest.load();
                foreach (var item in settingsfiletest._settings.listOfPrograms)
                {
                    if (item != null)
                    {
                        listBox1.Items.Add(item);
                    }
                }

                b = new System.Threading.Thread(() => monitorProcess());
                b.IsBackground = true;
                b.Start();
            }
        }

        public void TrapMouse(int x1, int y1, int x2, int y2)
        {
            bool running = true;

            var settingsfiletest = new SettingsClass();
            while (running)
            {
                if (Cursor.Position.X < x1)
                {
                    Cursor.Position = new Point(x1, Cursor.Position.Y);
                }
                if (Cursor.Position.Y < y1)
                {
                    Cursor.Position = new Point(Cursor.Position.X, y1);
                }

                if (Cursor.Position.X > x2)
                {
                    Cursor.Position = new Point(x2, Cursor.Position.Y);
                }
                if (Cursor.Position.Y > y2)
                {
                    Cursor.Position = new Point(Cursor.Position.X, y2);
                }
                System.Threading.Thread.Sleep(1);
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
            bool running = true;
            var settingsfiletest = new SettingsClass();
            settingsfiletest.load();

            while (running)
            {
                //Check if settings have changed.
                if (settingsHaveChanged)
                {
                    settingsfiletest.load();
                    settingsHaveChanged = false;
                }
                foreach (var filePath in settingsfiletest._settings.listOfPrograms)
                {
                        // Check allowing thread to close.
                        if (Flag.Text != "- Closing")
                        {
                            var wmiQueryString = "SELECT ProcessId, ExecutablePath, CommandLine FROM Win32_Process";
                            using (var searcher = new ManagementObjectSearcher(wmiQueryString))
                            using (var results = searcher.Get())
                            {
                                var query = from p in Process.GetProcesses()
                                            join mo in results.Cast<ManagementObject>()
                                            on p.Id equals (int)(uint)mo["ProcessId"]
                                            select new
                                            {
                                                Process = p,
                                                Path = (string)mo["ExecutablePath"],
                                                CommandLine = (string)mo["CommandLine"],
                                            };
                                foreach (var item in query)
                                {
                                    if (item.Path != null)
                                    {
                                        int lastIndexLocation = item.Path.LastIndexOf('\\', item.Path.Length - 1);
                                        String activProg = item.Path.Substring(lastIndexLocation + 1);

                                        if (activProg == filePath)
                                        {
                                            if (Flag.Text != "- Trap is Running")
                                            {
                                                this.Invoke(new Action(() => { button1.PerformClick(); }));
                                            }
                                        }
                                        else
                                        {

                                            System.Threading.Thread.Sleep(1);
                                        }
                                    }
                                    
                                    
                                }
                            }
                        }
                        else
                        {
                            running = false;
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
                Flag.Text = "- Trap is Running";
                button1.Enabled = false;
                button1.Visible = false;
                button3.Enabled = false;
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
            Flag.Text = "- Trap is Not Running";
            button1.Enabled = true;
            button1.Visible = true;
            button3.Enabled = true;
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
            Flag.Text = "- Closing";
            var settingsfiletest = new SettingsClass();
            settingsfiletest.Save();
            settingsfiletest.Update(x1, y1, x2, y2, settingsfiletest._settings.listOfPrograms);
            settingsfiletest.Save();
            t.Abort();
            b.Abort();
            while (t.IsAlive || b.IsAlive)
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
            bool startAutoTrap = false;
            var settingsfiletest = new SettingsClass();
            bool exists = settingsfiletest.exists();
            if (exists == false)
            {
                startAutoTrap = true;
            }
            // TODO! OPEN NEW FORM, FULL SCREEN, ALLOW FOR DRAWING AREA TO TRAP.
            secondform.Show();
            settingsfiletest.Save();

            if (startAutoTrap)
            {
                b = new System.Threading.Thread(() => monitorProcess());
                b.IsBackground = true;
                b.Start();
            }
        }
        string filePathToSave;
        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog programSearch = new OpenFileDialog();
            programSearch.InitialDirectory = @"C:\";
            programSearch.Title = "Select application to trap";
            programSearch.DefaultExt = "exe";
            programSearch.Filter = "exe files (*.exe)|*.exe";
            programSearch.ShowDialog();
            filePathToSave = programSearch.FileName;

            var settingsfiletest = new SettingsClass();
            settingsfiletest.load();
            if (filePathToSave != "")
            {
                //Refining the filePath to save to only include file name.
                int lastIndexLocation = filePathToSave.LastIndexOf('\\', filePathToSave.Length - 1);
                filePathToSave = filePathToSave.Substring(lastIndexLocation + 1);

                // Ensure no duplicate entries.
                bool saveTheFile = true;
                foreach (var item in settingsfiletest._settings.listOfPrograms)
                {
                    if (item != null)
                    {
                        if (item.ToString() == filePathToSave)
                        {
                            saveTheFile = false;
                        }
                    }
                }

                // Saving the file to the list.
                if (saveTheFile)
                {
                    Array.Resize(ref settingsfiletest._settings.listOfPrograms, settingsfiletest._settings.listOfPrograms.Length + 1);
                    settingsfiletest._settings.listOfPrograms[settingsfiletest._settings.listOfPrograms.Length - 1] = filePathToSave;
                }
                else
                {
                    MessageBox.Show("Program is already being automatically blocked.");
                }
            }

            listBox1.Items.Clear();

            //Display all items in list
            foreach (var item in settingsfiletest._settings.listOfPrograms)
            {
                Console.WriteLine(item);
                if(item != null)
                {
                    listBox1.Items.Add(item);
                }
            }
            settingsfiletest.Save();
            settingsHaveChanged = true;
        }

        // Button to remove highlighted entry from the list of program's to block.
        private void Button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                // List to replace the old settings list.
                string[] tempList = new string[0];

                var settingsfiletest = new SettingsClass();
                settingsfiletest.load();

                // Remove the program.
                foreach (var item in settingsfiletest._settings.listOfPrograms)
                {
                    if (item != null && listBox1.SelectedItem != null)
                    {
                        if (item == listBox1.SelectedItem.ToString())
                        {
                            listBox1.Items.Remove(item);
                        }
                        else
                        {
                            Array.Resize(ref tempList, tempList.Length + 1);
                            tempList[tempList.Length - 1] = item;
                        }
                    }
                }
                settingsfiletest._settings.listOfPrograms = tempList;
                settingsfiletest.Save();
                settingsHaveChanged = true;
            }
        }
    }
    
}
