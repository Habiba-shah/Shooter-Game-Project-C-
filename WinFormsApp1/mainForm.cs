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
        public MainForm()
        {
            InitializeComponent();

            this.BackgroundImage = GameProjectOop.Properties.Resources.cover;
            this.BackgroundImageLayout = ImageLayout.Stretch;
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
                btn.BackColor = Color.Red; // Lighter Highlight
                btn.ForeColor = Color.Yellow; // Optional: Text pop
                btn.Cursor = Cursors.Hand;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.DarkRed; // Original
                btn.ForeColor = Color.White;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            WinFormsApp1.GameForm gameForm = new WinFormsApp1.GameForm();
            gameForm.Show();
            this.Hide();
        }

        private void main_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModerateLevel level = new ModerateLevel();
            level.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HardLevel level = new HardLevel();
            level.Show();
           
        }
    }
}
