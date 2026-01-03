using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1;

namespace GameProjectOop
{
    public partial class ResultForm : Form
    {
        public ResultForm(string message)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            GameForm game = new GameForm();
            game.Show();
            this.Close();
        }

        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.WhiteSmoke;
            button1.Font = new Font("Showcard Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(46, 250);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 0;
            button1.Text = "BACK";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.WhiteSmoke;
            button2.Font = new Font("Showcard Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(400, 250);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 1;
            button2.Text = "PLAY";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // ResultForm
            // 
            BackgroundImage = Properties.Resources.win;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(573, 312);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "ResultForm";
            Text = "ResultForm";
            Load += ResultForm_Load;
            ResumeLayout(false);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ResultForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm game = new MainForm();
            game.Show();

            this.Close();

        }

        private Button button1;
        private Button button2;

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm menu = new MainForm();
            menu.Show();

            this.Close();

        }
    }

}
