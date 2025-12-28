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
            txtScore = new Label();
            label1 = new Label();
            HealthBar = new ProgressBar();
            pictureBox1 = new PictureBox();
            GameTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtammo
            // 
            txtammo.AutoSize = true;
            txtammo.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtammo.ForeColor = SystemColors.ControlLightLight;
            txtammo.Location = new Point(29, 9);
            txtammo.Name = "txtammo";
            txtammo.Size = new Size(91, 28);
            txtammo.TabIndex = 0;
            txtammo.Text = "Ammo:0";
            // 
            // txtScore
            // 
            txtScore.AutoSize = true;
            txtScore.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtScore.ForeColor = SystemColors.ControlLightLight;
            txtScore.Location = new Point(341, 9);
            txtScore.Name = "txtScore";
            txtScore.Size = new Size(69, 28);
            txtScore.TabIndex = 1;
            txtScore.Text = "Kills:0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(596, 9);
            label1.Name = "label1";
            label1.Size = new Size(80, 28);
            label1.TabIndex = 2;
            label1.Text = "Health:";
            // 
            // HealthBar
            // 
            HealthBar.Location = new Point(682, 12);
            HealthBar.Name = "HealthBar";
            HealthBar.Size = new Size(224, 28);
            HealthBar.TabIndex = 3;
            HealthBar.Value = 100;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = GameProjectOop.Properties.Resources.up2;
            pictureBox1.Location = new Point(529, 605);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(91, 110);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // GameTimer
            // 
            GameTimer.Enabled = true;
            GameTimer.Interval = 20;
            GameTimer.Tick += MainTimerEvent;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1180, 743);
            Controls.Add(pictureBox1);
            Controls.Add(HealthBar);
            Controls.Add(label1);
            Controls.Add(txtScore);
            Controls.Add(txtammo);
            Name = "GameForm";
            Text = "Enemy ShootOut Game";
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
        private Label txtScore;
        private Label label1;
        private ProgressBar HealthBar;
        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer GameTimer;
    }
}
