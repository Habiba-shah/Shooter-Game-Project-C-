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
        //int welcomeAlpha = 0;
        SoundSystem soundSystem;
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


        //constructorr
        public GameForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.DoubleBuffered = true; //smooth game
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
            lblKills.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Controls.Add(lblKills);

            txtammo.Text = "Ammo: " + player.Ammo;
            soundSystem = new SoundSystem();

            //StartWelcome();
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

                // CHECK BOUNDS (Dynamic)
                if (!ClientRectangle.Contains(Point.Round(bullets[i].Position)))
                {
                    bullets[i].IsActive = false;
                }

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

                        // Show dead sprite
                        pictureBox1.Image = GameProjectOop.Properties.Resources.dead1;
                        pictureBox1.Refresh();

                        LostForm lost = new LostForm();
                        lost.StartPosition = FormStartPosition.CenterParent;
                        lost.ShowDialog(this);

                        this.Close();
                        return;
                    }
                }
            }

            if (damageCooldown > 0)
                damageCooldown--;

            //  COLLISIONS (Player  PowerUps)
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

                gameEnded = true;

                ResultForm result = new ResultForm("YOU WIN");
                result.StartPosition = FormStartPosition.CenterParent;
                result.ShowDialog(this);

                this.Close();
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
            Bullet bullet = new Bullet();
            bullet.Size = new SizeF(10, 4);

            // Calculate spawn position based on direction so it doesn't spawn inside player
            float spawnX = player.Position.X + pictureBox1.Width / 2;
            float spawnY = player.Position.Y + pictureBox1.Height / 2;

            if (direction == "left")
            {
                bullet.Velocity = new PointF(-15, 0);
                spawnX = player.Position.X - bullet.Size.Width - 2;
            }
            if (direction == "right")
            {
                bullet.Velocity = new PointF(15, 0);
                spawnX = player.Position.X + pictureBox1.Width + 2;
            }
            if (direction == "up")
            {
                bullet.Velocity = new PointF(0, -15);
                bullet.Size = new SizeF(4, 10); // Rotate bullet for vertical
                spawnY = player.Position.Y - bullet.Size.Height - 2;
            }
            if (direction == "down")
            {
                bullet.Velocity = new PointF(0, 15);
                bullet.Size = new SizeF(4, 10); // Rotate bullet for vertical
                spawnY = player.Position.Y + pictureBox1.Height + 2;
            }

            bullet.Position = new PointF(spawnX, spawnY);

            bullets.Add(bullet);
        }
        private PointF GetEnemySpawnPoint()
        {
            int w = ClientSize.Width;
            int h = ClientSize.Height;

            int side = randNum.Next(4);

            // TOP
            if (side == 0)
            {
                return new PointF(randNum.Next(0, w), -120);
            }
            // BOTTOM
            else if (side == 1)
            {
                return new PointF(randNum.Next(0, w), h + 120);
            }
            // LEFT
            else if (side == 2)
            {
                return new PointF(-140, randNum.Next(0, h));
            }
            // RIGHT
            else
            {
                return new PointF(w + 140, randNum.Next(0, h));
            }
        }

        private void MakeZombie()
        {
            Enemy zombie = new Enemy
            {
                Sprite = GameProjectOop.Properties.Resources.demo,
                Size = new SizeF(140, 120),
                Movement = new ChaseMovement(player, zombieSpeed),
                Position = GetEnemySpawnPoint()
            };

            // Add animation frames
            zombie.LeftFrames.Add(GameProjectOop.Properties.Resources.demoleft1);
            zombie.LeftFrames.Add(GameProjectOop.Properties.Resources.demoleft2);
            zombie.LeftFrames.Add(GameProjectOop.Properties.Resources.demoleft3);

            zombie.RightFrames.Add(GameProjectOop.Properties.Resources.demoright1);
            zombie.RightFrames.Add(GameProjectOop.Properties.Resources.demoright2);
            zombie.RightFrames.Add(GameProjectOop.Properties.Resources.demoright3);

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

            for (int i = 0; i < 10; i++)
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

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }
    }
}