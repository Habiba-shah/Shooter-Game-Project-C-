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
    public partial class MainForm : Form
    {

        SoundSystem soundSystem = new SoundSystem();
        //load krta hy form ko
        public MainForm()
        {   
            InitializeComponent();

            this.BackgroundImage = GameProjectOop.Properties.Resources.cover;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.WindowState = FormWindowState.Maximized;

            CustomizeButtons();
        }

        //buttons ko array mein store krta hy 
        private void CustomizeButtons()
        {
            Button[] buttons = { button1, button2, button3 };

            foreach (var btn in buttons)
            {
                btn.MouseEnter += Button_MouseEnter;
                btn.MouseLeave += Button_MouseLeave;
            }
        }
        //hover effect deta hy array mein store button pr
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.Red; 
                btn.ForeColor = Color.Yellow; 
                btn.Cursor = Cursors.Hand;
            }
        }
        //original color pr wapis a jata hy jb curse hataty hn
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

        private void main_Load(object sender, EventArgs e)
        {
            soundSystem.PlayLoop(
        GameProjectOop.Properties.Resources.intro
        );
        }

        private void label1_Click(object sender, EventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {
            soundSystem.Stop();
            soundSystem.Play(
      GameProjectOop.Properties.Resources.whoosh
        );
            HardLevel level = new HardLevel();
            level.Show();
           
        }
    }
}
