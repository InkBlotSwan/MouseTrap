namespace MouseRestrict
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.Flag = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.setTrapProfile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 146);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "ON";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Flag
            // 
            this.Flag.AutoSize = true;
            this.Flag.Location = new System.Drawing.Point(65, 155);
            this.Flag.Name = "Flag";
            this.Flag.Size = new System.Drawing.Size(102, 13);
            this.Flag.TabIndex = 1;
            this.Flag.Text = "Trap is Not Running";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(12, 146);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(47, 30);
            this.button2.TabIndex = 2;
            this.button2.Text = "OFF";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // setTrapProfile
            // 
            this.setTrapProfile.Location = new System.Drawing.Point(12, 12);
            this.setTrapProfile.Name = "setTrapProfile";
            this.setTrapProfile.Size = new System.Drawing.Size(74, 25);
            this.setTrapProfile.TabIndex = 3;
            this.setTrapProfile.Text = "Set Trap";
            this.setTrapProfile.UseVisualStyleBackColor = true;
            this.setTrapProfile.Click += new System.EventHandler(this.setTrapProfile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Set the area to trap your cursor in.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 188);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.setTrapProfile);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Flag);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "MouseTrap!";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Flag;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button setTrapProfile;
        private System.Windows.Forms.Label label1;
    }
}

