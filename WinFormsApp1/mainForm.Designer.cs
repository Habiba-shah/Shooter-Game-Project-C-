namespace GameProjectOop
{
    partial class MainForm
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
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(199, 9);
            label1.Name = "label1";
            label1.Size = new Size(410, 44);
            label1.TabIndex = 0;
            label1.Text = "Demo Shooter Game";
            // 
            // button1
            // 
            button1.BackColor = Color.DarkOrchid;
            button1.Font = new Font("Stencil", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(292, 103);
            button1.Name = "button1";
            button1.Size = new Size(225, 50);
            button1.TabIndex = 1;
            button1.Text = "Easy Level";
            button1.UseVisualStyleBackColor = false;
            button1.Click += this.button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.DarkOrchid;
            button2.Font = new Font("Stencil", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(292, 168);
            button2.Name = "button2";
            button2.Size = new Size(225, 45);
            button2.TabIndex = 2;
            button2.Text = "Medium Level";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.DarkOrchid;
            button3.Font = new Font("Stencil", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button3.Location = new Point(292, 236);
            button3.Name = "button3";
            button3.Size = new Size(225, 49);
            button3.TabIndex = 3;
            button3.Text = "Hard Level";
            button3.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackgroundImage = Properties.Resources.cover;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 536);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainForm";
            Text = "main";
            Load += main_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}