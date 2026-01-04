namespace GameProjectOop
{
    partial class LostForm
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
            button2 = new Button();
            SuspendLayout();
            // 
            // button2
            // 
            button2.BackColor = Color.WhiteSmoke;
            button2.Font = new Font("Showcard Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.Black;
            button2.Location = new Point(160, 229);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(143, 27);
            button2.TabIndex = 1;
            button2.Text = "REPLAY";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // LostForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.overr;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(463, 298);
            Controls.Add(button2);
            Margin = new Padding(2);
            Name = "LostForm";
            Text = "LostForm";
            Load += LostForm_Load;
            ResumeLayout(false);
        }

        #endregion
        private Button button2;
    }
}