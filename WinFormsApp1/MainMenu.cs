using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1;

namespace GameProjectOop
{
    public partial class MainMenu : Form
    {
        SoundSystem soundSystem = new SoundSystem();
        public MainMenu()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            CustomizeButtons();
        }

        private void CustomizeButtons()
        {
            Button[] buttons = { button1, button2, button3 };
            foreach (var btn in buttons)
            {
                btn.MouseEnter += Button_MouseEnter;
                btn.MouseLeave += Button_MouseLeave;
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.Red;
                btn.ForeColor = Color.Yellow;
                btn.Cursor = Cursors.Hand;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.DarkRed;
                btn.ForeColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            soundSystem.Stop();
            soundSystem.Play(
            GameProjectOop.Properties.Resources.whoosh
               );
            GameForm gameForm = new GameForm();
            gameForm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            soundSystem.Stop();
            soundSystem.Play(
      GameProjectOop.Properties.Resources.whoosh
        );
            HardLevel level = new HardLevel();
            level.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            soundSystem.Stop();
            soundSystem.Play(
       GameProjectOop.Properties.Resources.whoosh
        );
            ModerateLevel level = new ModerateLevel();
            level.Show();
            this.Hide();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            soundSystem.PlayLoop(
                GameProjectOop.Properties.Resources.intro
            );
        }
    }
}
