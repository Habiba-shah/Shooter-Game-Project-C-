using GameProjectOop.Core;
using GameProjectOop.Entities;
using GameProjectOop.Extensions;
using GameProjectOop.Movements;
using GameProjectOop.Systems;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;


namespace GameProjectOop
{
    public partial class ModerateLevel : Form
    {
        Mind mind;
        int enemyKills = 0;

        SoundSystem soundSystem;
        bool gameEnded = false;

        Player player;
        KeyboardMovement keyboardMovement;
        Random randNum = new Random();
        Label lblKills = new Label();

        List<Bullet> bullets = new List<Bullet>();
        List<EnemyBullet> enemyBullets = new List<EnemyBullet>();
        List<Enemy> enemies = new List<Enemy>();
        List<PowerUp> powerUps = new List<PowerUp>();

        CollisionSystem collisionSystem = new CollisionSystem();

        string facing = "up";
        int playerLives = 3;
        int zombieSpeed = 4;
        int score = 0;
        int damageCooldown = 0;

        //constructor
        public ModerateLevel()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.DoubleBuffered = true; //smooth game
        }

        private void ModerateLevel_Load(object sender, EventArgs e)
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
            player.Update(new GameTime());

            pictureBox1.Left = (int)player.Position.X;
            pictureBox1.Top = (int)player.Position.Y;

            // BULLETS
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Update(new GameTime());

                // CHECK BOUNDS (Dynamic)
                if (!ClientRectangle.Contains(Point.Round(bullets[i].Position)))
                {
                    bullets[i].IsActive = false;
                }

                for (int j = enemies.Count - 1; j >= 0; j--)
                {
                    if (bullets[i].Bounds.IntersectsWith(enemies[j].Bounds))
                    {
                        // IF enemy is MIND (boss)
                        if (enemies[j] is Mind boss)
                        {
                            boss.OnCollision(bullets[i]); // Mind handles its own health
                            bullets[i].IsActive = false;

                            if (!boss.IsActive) // Mind dead
                            {
                                enemies.RemoveAt(j);
                                score++;
                                lblKills.Text = "Kills: " + score;
                            }
                        }

                        // NORMAL ZOMBIE
                        else
                        {
                            enemies.RemoveAt(j);
                            bullets[i].IsActive = false;

                            enemyKills++;
                            score++;
                            lblKills.Text = "Kills: " + score;

                            // 🔥 SPAWN MIND AFTER 5 ZOMBIES
                            if (enemyKills == 5)
                            {
                                SpawnMind();
                            }
                        }

                        break;
                    }

                }

                if (!bullets[i].IsActive)
                    bullets.RemoveAt(i);
            }

            // ENEMY BULLETS
            for (int i = enemyBullets.Count - 1; i >= 0; i--)
            {
                enemyBullets[i].Update(new GameTime());

                if (!ClientRectangle.Contains(Point.Round(enemyBullets[i].Position)))
                {
                    enemyBullets[i].IsActive = false;
                }

                if (enemyBullets[i].Bounds.IntersectsWith(player.Bounds) && !gameEnded)
                {
                    enemyBullets[i].IsActive = false;
                    playerLives = 0;
                    HealthBar.Value = 0;

                    gameEnded = true;
                    GameTimer.Stop();

                    // Show dead sprite
                    pictureBox1.Image = Properties.Resources.dead1;
                    pictureBox1.Refresh();

                    LostForm lost = new LostForm();
                    lost.StartPosition = FormStartPosition.CenterParent;
                    lost.ShowDialog(this);

                    this.Close();
                    return;
                }

                if (!enemyBullets[i].IsActive)
                    enemyBullets.RemoveAt(i);
            }

            //  ENEMIES
            foreach (Enemy enemy in enemies)
            {
                //  KEEP DEMO INSIDE SCREEN
                if (enemy.Movement is ChaseMovement chase)
                    chase.Bounds = ClientRectangle;

                enemy.Update(new GameTime());

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
                        pictureBox1.Image = Properties.Resources.dead1;
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
            if (!gameEnded && enemies.Count == 0 && mind != null)
            {
                gameEnded = true;
                GameTimer.Stop();

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
                pictureBox1.Image = Properties.Resources.left1;
            }
            if (e.KeyCode == Keys.Right)
            {
                facing = "right";
                pictureBox1.Image = Properties.Resources.right1;
            }
            if (e.KeyCode == Keys.Up)
            {
                facing = "up";
                pictureBox1.Image = Properties.Resources.up2;
            }
            if (e.KeyCode == Keys.Down)
            {
                facing = "down";
                pictureBox1.Image = Properties.Resources.down1;
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
                bullet.Size = new SizeF(4, 10);

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
                Sprite = Properties.Resources.demo,
                Size = new SizeF(160, 140),
                Movement = new ChaseMovement(player, zombieSpeed),
                Position = GetEnemySpawnPoint()

            };

            // Add animation frames
            zombie.LeftFrames.Add(Properties.Resources.demoleft1);
            zombie.LeftFrames.Add(Properties.Resources.demoleft2);
            zombie.LeftFrames.Add(Properties.Resources.demoleft3);

            zombie.RightFrames.Add(Properties.Resources.demoright1);
            zombie.RightFrames.Add(Properties.Resources.demoright2);
            zombie.RightFrames.Add(Properties.Resources.demoright3);

            enemies.Add(zombie);
        }

        private void DropAmmo()
        {
            AmmoPowerUp ammo = new AmmoPowerUp
            {
                Sprite = Properties.Resources.ammo_Image,
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
            enemyBullets.Clear();
            powerUps.Clear();

            for (int i = 0; i < 7; i++)
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
        private void ModerateLevel_Paint(object sender, PaintEventArgs e)
        {

            foreach (Bullet b in bullets)
            {
                b.Draw(e.Graphics);
            }
            foreach (EnemyBullet eb in enemyBullets)
            {
                eb.Draw(e.Graphics);
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

        void SpawnMind()
        {
            mind = new Mind();

            mind.Position = new PointF(300, 100);
            mind.Size = new SizeF(200, 220);

            ChaseMovement chase = new ChaseMovement(player, 4.5f);
            chase.Bounds = new RectangleF(0, 0, this.Width, this.Height);

            mind.Movement = chase;

            mind.Target = player;
            mind.OnShoot = (spawnPos, direction) =>
            {
                float bulletSpeed = 6f;
                EnemyBullet eb = new EnemyBullet();
                eb.Position = spawnPos;
                eb.Velocity = new PointF(direction.X * bulletSpeed, direction.Y * bulletSpeed);
                enemyBullets.Add(eb);
            };

            mind.Velocity = PointF.Empty; // Reset default velocity from Enemy base class

            // Animation frames (reuse Enemy system)
            mind.LeftFrames.Add(Properties.Resources.mind1_0);
            mind.LeftFrames.Add(Properties.Resources.mind2_0);
            mind.LeftFrames.Add(Properties.Resources.mind3_0);

            mind.RightFrames.Add(Properties.Resources.mind1);
            mind.RightFrames.Add(Properties.Resources.mind2);
            mind.RightFrames.Add(Properties.Resources.mind3);

            enemies.Add(mind);
        }

    }
}
