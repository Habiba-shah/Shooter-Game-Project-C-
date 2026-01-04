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

        SoundSystem soundSystem = new SoundSystem();
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
            button2 = new Button();
            SuspendLayout();
            // 
            // button2
            // 
            button2.BackColor = Color.WhiteSmoke;
            button2.Font = new Font("Showcard Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.Location = new Point(179, 246);
            button2.Name = "button2";
            button2.Size = new Size(192, 34);
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
            soundSystem.Play(
      GameProjectOop.Properties.Resources.whoosh
  );
            MainForm game = new MainForm();
            game.Show();

            this.Close();

        }
        private Button button2;

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm menu = new MainForm();
            menu.Show();

            this.Close();

        }
    }

}
