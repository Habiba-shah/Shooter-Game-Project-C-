namespace WinFormsApp1
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtammo = new Label();
            label1 = new Label();
            HealthBar = new ProgressBar();
            pictureBox1 = new PictureBox();
            GameTimer = new System.Windows.Forms.Timer(components);
            welcomeTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtammo
            // 
            txtammo.AutoSize = true;
            txtammo.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtammo.ForeColor = SystemColors.ControlLightLight;
            txtammo.Location = new Point(23, 7);
            txtammo.Margin = new Padding(2, 0, 2, 0);
            txtammo.Name = "txtammo";
            txtammo.Size = new Size(79, 23);
            txtammo.TabIndex = 0;
            txtammo.Text = "Ammo:0";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(477, 7);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(68, 23);
            label1.TabIndex = 2;
            label1.Text = "Health:";
            // 
            // HealthBar
            // 
            HealthBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            HealthBar.Location = new Point(546, 10);
            HealthBar.Margin = new Padding(2);
            HealthBar.Name = "HealthBar";
            HealthBar.Size = new Size(179, 22);
            HealthBar.TabIndex = 3;
            HealthBar.Value = 100;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = GameProjectOop.Properties.Resources.up2;
            pictureBox1.Location = new Point(415, 489);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(71, 94);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // GameTimer
            // 
            GameTimer.Enabled = true;
            GameTimer.Interval = 20;
            GameTimer.Tick += MainTimerEvent;
            // 
            // welcomeTimer
            // 
            welcomeTimer.Interval = 30;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(916, 594);
            Controls.Add(pictureBox1);
            Controls.Add(HealthBar);
            Controls.Add(label1);
            Controls.Add(txtammo);
            Margin = new Padding(2);
            Name = "GameForm";
            Text = "Enemy ShootOut Game";
            WindowState = FormWindowState.Maximized;
            Load += GameForm_Load;
            Paint += GameForm_paint;
            KeyDown += GameKeyDown;
            KeyUp += GameKeyUp;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label txtammo;
        private Label label1;
        private ProgressBar HealthBar;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Timer welcomeTimer;
    }
}
