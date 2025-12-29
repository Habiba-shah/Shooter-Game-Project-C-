using GameProjectOop;
using GameProjectOop.Core;
using GameProjectOop.Entities;
using GameProjectOop.Extensions;
using GameProjectOop.Movements;
using GameProjectOop.Systems;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace WinFormsApp1
{
    
    public partial class GameForm : Form
    {
        bool gameEnded = false;

        Player player;
        KeyboardMovement keyboardMovement;
        Random randNum = new Random();
        Label lblKills = new Label();

        List<Bullet> bullets = new List<Bullet>();
        List<Enemy> enemies = new List<Enemy>();
        List<PowerUp> powerUps = new List<PowerUp>();

        CollisionSystem collisionSystem = new CollisionSystem();
     

        string facing = "up";
        int playerLives = 3;
        int zombieSpeed = 3;
        int score = 0;
        int damageCooldown = 0;

        

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
                Position = new PointF(pictureBox1.Left, pictureBox1.Top),
                Ammo = 10
            };

            lblKills.Text = "Kills: 0";
            lblKills.AutoSize = true;
            lblKills.ForeColor = Color.White;
            lblKills.BackColor = Color.Transparent;
            lblKills.Font = new Font("Arial", 14, FontStyle.Bold);
            lblKills.Left = ClientSize.Width - 150;
            lblKills.Top = 10;
            Controls.Add(lblKills);

            txtammo.Text = "Ammo: " + player.Ammo;

            RestartGame();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            keyboardMovement.Bounds = ClientRectangle;

            player.Size = pictureBox1.Size;
            player.Update(null);

            pictureBox1.Left = (int)player.Position.X;
            pictureBox1.Top = (int)player.Position.Y;

            // BULLETS
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Update(null);

                for (int j = enemies.Count - 1; j >= 0; j--)
                {
                    if (bullets[i].Bounds.IntersectsWith(enemies[j].Bounds))
                    {
                        enemies.RemoveAt(j);
                        bullets[i].IsActive = false;

                        score++;
                        lblKills.Text = "Kills: " + score;
                        break;
                    }
                }

                if (!bullets[i].IsActive)
                    bullets.RemoveAt(i);
            }

            //  ENEMIES
            foreach (Enemy enemy in enemies)
            {
                //  KEEP DEMO INSIDE SCREEN
                if (enemy.Movement is ChaseMovement chase)
                    chase.Bounds = ClientRectangle;

                enemy.Update(null);

                if (damageCooldown <= 0 && enemy.Bounds.IntersectsWith(player.Bounds))
                {
                    playerLives--;
                    damageCooldown = 30;

                    HealthBar.Value = Math.Max(0, (int)((playerLives / 3.0) * 100));

                    //  GAME OVER
                    if (playerLives <= 0 && !gameEnded)
                    {
                        gameEnded = true;
                        GameTimer.Stop();

                        this.Hide();
                        LostForm lost = new LostForm();
                        lost.Show();
                        return;
                    }
                }
            }

            if (damageCooldown > 0)
                damageCooldown--;

            //  COLLISIONS (Player ↔ PowerUps)
            List<GameObject> allObjects = new List<GameObject> { player };
            allObjects.AddRange(enemies);
            allObjects.AddRange(powerUps);

            collisionSystem.Check(allObjects);

            //  REMOVE PICKED POWERUPS
            powerUps.RemoveAll(p => !p.IsActive);

            txtammo.Text = "Ammo: " + player.Ammo;

            //  YOU WIN (ALL DEMOS KILLED)
            if (!gameEnded && enemies.Count == 0)
            {
                gameEnded = true;
                GameTimer.Stop();

                this.Hide();
                ResultForm result = new ResultForm("YOU WIN");
                result.Show();
                return;
            }

            Invalidate();
        }


        private void GameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                facing = "left";
                pictureBox1.Image = GameProjectOop.Properties.Resources.left1;
            }
            if (e.KeyCode == Keys.Right)
            {
                facing = "right";
                pictureBox1.Image = GameProjectOop.Properties.Resources.right1;
            }
            if (e.KeyCode == Keys.Up)
            {
                facing = "up";
                pictureBox1.Image = GameProjectOop.Properties.Resources.up2;
            }
            if (e.KeyCode == Keys.Down)
            {
                facing = "down";
                pictureBox1.Image = GameProjectOop.Properties.Resources.down1;
            }
        }

        private void GameKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && player.Ammo > 0)
            {
                player.Ammo--;
                ShootBullet(facing);
                txtammo.Text = "Ammo: " + player.Ammo;

                if (player.Ammo == 0)
                    DropAmmo();
            }
        }

        private void ShootBullet(string direction)
        {
            Bullet bullet = new Bullet
            {
                Size = new SizeF(10, 4),
                Position = new PointF(
                    player.Position.X + pictureBox1.Width / 2,
                    player.Position.Y + pictureBox1.Height / 2
                )
            };

            if (direction == "left") bullet.Velocity = new PointF(-15, 0);
            if (direction == "right") bullet.Velocity = new PointF(15, 0);
            if (direction == "up") bullet.Velocity = new PointF(0, -15);
            if (direction == "down") bullet.Velocity = new PointF(0, 15);

            bullets.Add(bullet);
        }

        private void MakeZombie()
        {
            Enemy zombie = new Enemy
            {
                Sprite = GameProjectOop.Properties.Resources.demo,
                Size = new SizeF(140, 120),
                Movement = new ChaseMovement(player, zombieSpeed),
                Position = new PointF(
                    randNum.Next(0, 900),
                    randNum.Next(0, 800)
                )
            };

            enemies.Add(zombie);
        }

        private void DropAmmo()
        {
            AmmoPowerUp ammo = new AmmoPowerUp
            {
                Sprite = GameProjectOop.Properties.Resources.ammo_Image,
                Size = new SizeF(40, 40),
                Position = new PointF(
                    randNum.Next(10, ClientSize.Width - 50),
                    randNum.Next(60, ClientSize.Height - 50)
                )
            };

            powerUps.Add(ammo);
        }

        private void RestartGame()
        {
            enemies.Clear();
            bullets.Clear();
            powerUps.Clear();

            for (int i = 0; i < 6; i++)
                MakeZombie();

            playerLives = 3;
            player.Ammo = 10;
            score = 0;

            HealthBar.Value = 100;
            lblKills.Text = "Kills: 0";
            txtammo.Text = "Ammo: " + player.Ammo;

            GameTimer.Start();
        }


        //print bullets, powerups, enemies
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
  foreach (Enemy zombie in enemies)
{
    zombie.Draw(e.Graphics);
}

                }
    }
}