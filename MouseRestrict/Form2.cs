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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        // MOUSEDOWN EVENT
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.x1 = Cursor.Position.X;
            Form1.y1 = Cursor.Position.Y;
            button1.Enabled = false;
            button1.Visible = false;
            button2.Enabled = true;
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.x2 = Cursor.Position.X;
            Form1.y2 = Cursor.Position.Y;

            var settingsfiletest = new SettingsClass();
            settingsfiletest.Update(Form1.x1, Form1.y1, Form1.x2, Form1.y2);
            settingsfiletest.Save();

            button2.Enabled = false;
            button2.Visible = false;
            button1.Enabled = true;
            button1.Visible = true;
            this.Hide();
        }
    }
}
