using GameProjectOop.Core;
using GameProjectOop.Entities;
using GameProjectOop.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace WinFormsApp1
{
    public partial class GameForm : Form
    {
        List<Bullet> bullets = new List<Bullet>();

        bool goLeft, goRight, goUp, goDown, gameOver;
        string facing = "up";
        int playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int zombiespeed = 3;
        Random randNum = new Random();
        int score;

        List<PictureBox> zombiesList = new List<PictureBox>();
        public GameForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.DoubleBuffered = true;

        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (goLeft && pictureBox1.Left > 0)
            {
                pictureBox1.Left -= speed;
            }

            if (goRight && pictureBox1.Left + pictureBox1.Width < ClientSize.Width)
            {
                pictureBox1.Left += speed;
            }

            if (goUp && pictureBox1.Top > 0)
            {
                pictureBox1.Top -= speed;
            }

            if (goDown && pictureBox1.Top + pictureBox1.Height < ClientSize.Height)
            {
                pictureBox1.Top += speed;
            }
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Update(null);

                if (!bullets[i].IsActive)
                {
                    bullets.RemoveAt(i);
                }
            }
            this.Invalidate();


        }
        private void GameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                pictureBox1.Image = GameProjectOop.Properties.Resources.left1;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                pictureBox1.Image = GameProjectOop.Properties.Resources.right1;

            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                pictureBox1.Image = GameProjectOop.Properties.Resources.up2;

            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                pictureBox1.Image = GameProjectOop.Properties.Resources.down1;
            }
        }

        private void GameKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;

            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;


            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = false;


            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;

            }

            if (e.KeyCode == Keys.Space)
            {
                ShootBullet(facing);
            }

        }

        private void ShootBullet(string direction)
        {
            

            Bullet bullet = new Bullet();

            // bullet size
            bullet.Size = new SizeF(10, 4);

            // bullet start position (player center)
            bullet.Position = new PointF(
                pictureBox1.Left + pictureBox1.Width / 2,
                pictureBox1.Top + pictureBox1.Height / 2

             );

            // direction based velocity (NO switch-case)
            if (direction == "left")
                bullet.Velocity = new PointF(-15, 0);

            if (direction == "right")
                bullet.Velocity = new PointF(15, 0);

            if (direction == "up")
                bullet.Velocity = new PointF(0, -15);

            if (direction == "down")
                bullet.Velocity = new PointF(0, 15);

            bullets.Add(bullet);
            
        }


        private void MakeZombie() { }
        private void RestartGame() { }

        private void GameForm_paint(object sender, PaintEventArgs e)
        {

            foreach (Bullet b in bullets)
            {
                b.Draw(e.Graphics);
            }
        }
    }
}
