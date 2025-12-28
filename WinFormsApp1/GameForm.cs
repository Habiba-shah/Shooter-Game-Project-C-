using GameProjectOop.Core;
using GameProjectOop.Entities;
using GameProjectOop.Extensions;
using GameProjectOop.Movements;

using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace WinFormsApp1
{
    public partial class GameForm : Form
    {

        Player player;
        KeyboardMovement keyboardMovement;
        List<PowerUp> powerUps = new List<PowerUp>();
        Random rand = new Random();



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
            keyboardMovement = new KeyboardMovement
            {
                Speed = 10f
            };

            player = new Player
            {
                Movement = keyboardMovement,
                Position = new PointF(pictureBox1.Left, pictureBox1.Top)
            };
            SpawnPowerUp();

        }


        private void MainTimerEvent(object sender, EventArgs e)
        {
            // Update player (movement happens here)
            player.Update(null);

            // Sync player position to PictureBox
            pictureBox1.Left = (int)player.Position.X;
            pictureBox1.Top = (int)player.Position.Y;

            // Update bullets
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Update(null);

                if (!bullets[i].IsActive)
                    bullets.RemoveAt(i);
            }

            this.Invalidate();
            for (int i = powerUps.Count - 1; i >= 0; i--)
            {
                powerUps[i].Update(null);

                if (!powerUps[i].IsActive)
                    powerUps.RemoveAt(i);
            }
            foreach (PowerUp powerUp in powerUps)
            {
                if (player.Bounds.IntersectsWith(powerUp.Bounds))
                {
                    powerUp.OnCollision(player);
                }
            }


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

            // bullet start position (player center - LOGIC position)
            bullet.Position = new PointF(
                player.Position.X + pictureBox1.Width / 2,
                player.Position.Y + pictureBox1.Height / 2
            );

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
            foreach (PowerUp powerUp in powerUps)
            {
                powerUp.Draw(e.Graphics);
            }

        }
        private void SpawnPowerUp()
        {
            PowerUp p = new PowerUp();

            p.Size = new SizeF(30, 30);

            // center area ke andar random position
            int minX = ClientSize.Width / 4;
            int maxX = ClientSize.Width * 3 / 4;

            int minY = ClientSize.Height / 4;
            int maxY = ClientSize.Height * 3 / 4;

            p.Position = new PointF(
                rand.Next(minX, maxX),
                rand.Next(minY, maxY)
            );

            powerUps.Add(p);
        }



    }
}
