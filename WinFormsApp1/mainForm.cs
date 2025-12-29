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
    }
}
