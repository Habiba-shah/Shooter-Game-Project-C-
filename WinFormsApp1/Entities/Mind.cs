using GameProjectOop.Core;
using GameProjectOop.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProjectOop.Entities
{
    public class Mind : Enemy
    {

        private int alpha = 255;   // opacity (255 = visible)
        public int Health { get; private set; } = 5;
        public float ShootInterval { get; set; } = 80f; // Frames between shots
        private float _shootTimer = 0;
        public GameObject? Target { get; set; }
        public Action<PointF, PointF>? OnShoot { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Target != null && IsActive)
            {
                _shootTimer++;
                if (_shootTimer >= ShootInterval)
                {
                    _shootTimer = 0;
                    ShootAtTarget();
                }
            }
        }

        private void ShootAtTarget()
        {
            if (Target == null) return;

            // Calculate center of Mind
            PointF spawnPos = new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2);

            // Calculate direction to target center
            float dx = (Target.Position.X + Target.Size.Width / 2) - spawnPos.X;
            float dy = (Target.Position.Y + Target.Size.Height / 2) - spawnPos.Y;

            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            if (distance > 0)
            {
                PointF direction = new PointF(dx / distance, dy / distance);
                OnShoot?.Invoke(spawnPos, direction);
            }
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Bullet)
            {
                Health--;

                alpha -= 50;
                if (alpha < 0) alpha = 0;

                if (Health <= 0)
                {
                    IsActive = false;
                }
            }
        }
        public override void Draw(Graphics g)
        {
            if (Sprite == null) return;

            ImageAttributes attr = new ImageAttributes();
            ColorMatrix matrix = new ColorMatrix();
            matrix.Matrix33 = alpha / 255f;   // transparency
            attr.SetColorMatrix(matrix);

            g.DrawImage(
                Sprite,
                Rectangle.Round(Bounds),
                0, 0,
                Sprite.Width,
                Sprite.Height,
                GraphicsUnit.Pixel,
                attr
            );
        }
        }
    }

