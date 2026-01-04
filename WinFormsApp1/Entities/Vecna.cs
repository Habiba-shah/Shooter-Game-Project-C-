using GameProjectOop.Core;
using GameProjectOop.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GameProjectOop.Entities
{
    public class Vecna : Enemy
    {
        public int Health { get; private set; } = 7;
        public float ShootInterval { get; set; } = 180f; // Frames between shots (Increased for less frequency)
        private float _shootTimer = 0;
        public Action<PointF, List<PointF>>? OnShootAllDirections { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsActive)
            {
                _shootTimer++;
                if (_shootTimer >= ShootInterval)
                {
                    _shootTimer = 0;
                    ShootInAllDirections();
                }
            }
        }

        private void ShootInAllDirections()
        {
            PointF spawnPos = new PointF(Position.X + Size.Width / 2, Position.Y + Size.Height / 2);
            List<PointF> directions = new List<PointF>
            {
                new PointF(0, -1),   // North
                new PointF(1, 0),    // East
                new PointF(0, 1),    // South
                new PointF(-1, 0),   // West
            };

            // Normalize diagonal directions
            for (int i = 0; i < directions.Count; i++)
            {
                float length = (float)Math.Sqrt(directions[i].X * directions[i].X + directions[i].Y * directions[i].Y);
                directions[i] = new PointF(directions[i].X / length, directions[i].Y / length);
            }

            OnShootAllDirections?.Invoke(spawnPos, directions);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Bullet)
            {
                Health--;
                if (Health <= 0)
                {
                    IsActive = false;
                }
            }
        }

        public override void Draw(Graphics g)
        {
            // Optional: Add visual feedback for health (like alpha change in Mind)
            base.Draw(g);
        }
    }
}
