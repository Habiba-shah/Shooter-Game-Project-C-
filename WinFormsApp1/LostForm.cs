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
    public partial class LostForm : Form
    {

        SoundSystem soundSystem = new SoundSystem();
        public LostForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MainForm menu = new MainForm();
            menu.Show();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                soundSystem.Play(
           GameProjectOop.Properties.Resources.whoosh
       );
                MainForm game = new MainForm();
                game.Show();

                this.Close();
            }
        }
    }
}